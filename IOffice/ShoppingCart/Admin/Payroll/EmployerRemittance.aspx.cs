using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;
using System.Net;

using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;

public partial class EmployerRemittance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);

    public static string hidesalesname = "";
    public static string Temp1val = "";
    public static string Temp1 = "";
    public static string hideotherrem = "";
    public static string hide1tded = "";
    public static string hide2ded = "";
    public static string hide3ded = "";
    public static string hideotherded = "";
    public static string yearId = "";
    public static string countryId = "";
    public static string Stateid = "";
    public static string sdate = "";
    public static string edate = "";
    public static int GovDisp = 0;
    public static int PayNo = 0;
    decimal paymonth = 0;
    decimal payweek = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        if (!IsPostBack)
        {


            ViewState["sortOrder"] = "";
            fillwarehouse();
            ddlwarehouse_SelectedIndexChanged(sender, e);
        }
    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
    protected void fillwarehouse()
    {

        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();


            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
        }

    }
    protected DataTable select(string str)
    {
        DataTable dt = new DataTable();
        //try
        //{
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);

        dtp.Fill(dt);

        //}
        //catch (Exception er)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = str + " :: " + er;
        //}
        return dt;

    }




    protected void fillyearid()
    {
        string Datat = "select Distinct CompanyWebsiteAddressMaster.Country,CompanyWebsiteAddressMaster.State from CompanyWebsiteAddressMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId  where WareHouseMaster.WareHouseId='" + ddlwarehouse.SelectedValue + "' and AddressTypeMaster.Name='Business Address' ";
        DataTable drg = select(Datat);
        if (drg.Rows.Count > 0)
        {
            if (drg.Rows.Count > 0)
            {
                countryId = Convert.ToString(drg.Rows[0]["Country"]);
                Stateid = Convert.ToString(drg.Rows[0]["State"]);
                DataTable dtc = select("Select distinct TaxYear_Name,TaxYear_Id from  Tax_Year    where CountryId='" + drg.Rows[0]["Country"] + "' and TaxYear_Name='" + DateTime.Now.Year.ToString() + "' and Active='1'");
                if (dtc.Rows.Count > 0)
                {
                    yearId = Convert.ToString(dtc.Rows[0]["TaxYear_Id"]);
                }
            }
        }
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        yearId = "";
        Stateid = "";
        countryId = "";
        DataTable dt = (DataTable)select("select Name,Report_Period_Id,Startdate from [ReportPeriod] where   Whid='" + ddlwarehouse.SelectedValue + "' order by Startdate ");
        if (dt.Rows.Count > 0)
        {
            ddlYear.DataSource = dt;
            ddlYear.DataTextField = "Name";
            ddlYear.DataValueField = "Report_Period_Id";
            ddlYear.DataBind();
        }
        DataTable dtC = (DataTable)select("select Report_Period_Id from [ReportPeriod] where  Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");
        if (dtC.Rows.Count > 0)
        {
            ddlYear.SelectedValue = Convert.ToString(dtC.Rows[0]["Report_Period_Id"]);
        }
        ddlYear_SelectedIndexChanged(sender, e);



    }


    protected void grdallemp_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);

            if (PayNo > 0)
            {

                HeaderCell = new TableCell();
                HeaderCell.Text = "Employee Contribution/Deduction";
                HeaderCell.ColumnSpan = PayNo;
                HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#8EB4BF");
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
                HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Employer Contribution";
                HeaderCell.ColumnSpan = PayNo;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
                HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                HeaderGridRow.Cells.Add(HeaderCell);
            }





            HeaderCell = new TableCell();
            HeaderCell.Text = "";

            HeaderCell.ColumnSpan = 1;
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#8EB4BF");
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);


            HeaderGridRow.Cells.Add(HeaderCell);




            grdallemp.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlperiod.Items.Clear();
        DataTable dt11 = new DataTable();
        DataTable dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,Convert(nvarchar,EndDate,101)   as EndDate from [ReportPeriod] where  Report_Period_Id='" + ddlYear.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            DataTable dsp = select("Select Distinct payperiodMaster.Id,PayperiodName,PayperiodStartDate from SalaryMaster  inner join payperiodMaster on payperiodMaster.Id=SalaryMaster.payperiodtypeId inner join payperiodtype  on payperiodtype.Id=payperiodMaster.PayperiodTypeID   where  SalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "' and PayperiodStartDate>='" + Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToShortDateString() + "' and PayperiodEndDate<='" + Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToShortDateString() + "' Order by PayperiodStartDate");
            if (dsp.Rows.Count > 0)
            {
                ddlperiod.DataSource = dsp;
                ddlperiod.DataTextField = "PayperiodName";
                ddlperiod.DataValueField = "Id";
                ddlperiod.DataBind();

            }
        }


    }
    public DataTable Totalemp()
    {
        DataTable dtTemp = new DataTable();


        DataColumn dedotherv = new DataColumn();
        dedotherv.ColumnName = "EmployeeId";
        dedotherv.DataType = System.Type.GetType("System.Decimal");
        dedotherv.AllowDBNull = true;
        dtTemp.Columns.Add(dedotherv);

        DataColumn dedother = new DataColumn();
        dedother.ColumnName = "EmployeeName";
        dedother.DataType = System.Type.GetType("System.String");
        dedother.AllowDBNull = true;
        dtTemp.Columns.Add(dedother);


        ///Ded
        ///
        DataColumn NG1 = new DataColumn();
        NG1.ColumnName = "NG1";
        NG1.DataType = System.Type.GetType("System.Decimal");
        NG1.AllowDBNull = true;
        dtTemp.Columns.Add(NG1);

        DataColumn NG2 = new DataColumn();
        NG2.ColumnName = "NG2";
        NG2.DataType = System.Type.GetType("System.Decimal");
        NG2.AllowDBNull = true;
        dtTemp.Columns.Add(NG2);


        DataColumn NG3 = new DataColumn();
        NG3.ColumnName = "NG3";
        NG3.DataType = System.Type.GetType("System.Decimal");
        NG3.AllowDBNull = true;
        dtTemp.Columns.Add(NG3);

        DataColumn NG4 = new DataColumn();
        NG4.ColumnName = "NG4";
        NG4.DataType = System.Type.GetType("System.Decimal");
        NG4.AllowDBNull = true;
        dtTemp.Columns.Add(NG4);
        DataColumn NGother = new DataColumn();
        NGother.ColumnName = "NG5";
        NGother.DataType = System.Type.GetType("System.Decimal");
        NGother.AllowDBNull = true;
        dtTemp.Columns.Add(NGother);

        DataColumn Gded1 = new DataColumn();
        Gded1.ColumnName = "G1";
        Gded1.DataType = System.Type.GetType("System.Decimal");
        Gded1.AllowDBNull = true;
        dtTemp.Columns.Add(Gded1);

        DataColumn Gded2 = new DataColumn();
        Gded2.ColumnName = "G2";
        Gded2.DataType = System.Type.GetType("System.Decimal");
        Gded2.AllowDBNull = true;
        dtTemp.Columns.Add(Gded2);

        DataColumn Gded3 = new DataColumn();
        Gded3.ColumnName = "G3";
        Gded3.DataType = System.Type.GetType("System.Decimal");
        Gded3.AllowDBNull = true;
        dtTemp.Columns.Add(Gded3);
        DataColumn Gded4 = new DataColumn();
        Gded4.ColumnName = "G4";
        Gded4.DataType = System.Type.GetType("System.Decimal");
        Gded4.AllowDBNull = true;
        dtTemp.Columns.Add(Gded4);

        DataColumn Gdedoothe = new DataColumn();
        Gdedoothe.ColumnName = "G5";
        Gdedoothe.DataType = System.Type.GetType("System.Decimal");
        Gdedoothe.AllowDBNull = true;
        dtTemp.Columns.Add(Gdedoothe);



        DataColumn Paidst = new DataColumn();
        Paidst.ColumnName = "TotRemreq";
        Paidst.DataType = System.Type.GetType("System.Decimal");
        Paidst.AllowDBNull = true;
        dtTemp.Columns.Add(Paidst);

        return dtTemp;
    }
    public DataTable TotalCalc()
    {
        DataTable dtTemp = new DataTable();

        DataColumn peid = new DataColumn();
        peid.ColumnName = "TaxId";
        peid.DataType = System.Type.GetType("System.String");
        peid.AllowDBNull = true;
        dtTemp.Columns.Add(peid);


        DataColumn prdem = new DataColumn();
        prdem.ColumnName = "TaxNames";
        prdem.DataType = System.Type.GetType("System.String");
        prdem.AllowDBNull = true;
        dtTemp.Columns.Add(prdem);



        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "EmpContamt";
        ssCatId.DataType = System.Type.GetType("System.Decimal");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);


        DataColumn catname = new DataColumn();
        catname.ColumnName = "Deddate";
        catname.DataType = System.Type.GetType("System.String");
        catname.AllowDBNull = true;
        dtTemp.Columns.Add(catname);


        DataColumn completemonth = new DataColumn();
        completemonth.ColumnName = "ContRate";
        completemonth.DataType = System.Type.GetType("System.Decimal");
        completemonth.AllowDBNull = true;
        dtTemp.Columns.Add(completemonth);

        DataColumn completedmonthamt = new DataColumn();
        completedmonthamt.ColumnName = "ContAmt";
        completedmonthamt.DataType = System.Type.GetType("System.Decimal");
        completedmonthamt.AllowDBNull = true;
        dtTemp.Columns.Add(completedmonthamt);



        DataColumn barcodeId = new DataColumn();
        barcodeId.ColumnName = "totremreqi";
        barcodeId.DataType = System.Type.GetType("System.Decimal");
        barcodeId.AllowDBNull = true;
        dtTemp.Columns.Add(barcodeId);


        DataColumn remperunitId = new DataColumn();
        remperunitId.ColumnName = "amtremited";
        remperunitId.DataType = System.Type.GetType("System.Decimal");
        remperunitId.AllowDBNull = true;
        dtTemp.Columns.Add(remperunitId);
        ////
        DataColumn srttt = new DataColumn();
        srttt.ColumnName = "Remdate";
        srttt.DataType = System.Type.GetType("System.String");
        srttt.AllowDBNull = true;
        dtTemp.Columns.Add(srttt);


        DataColumn SRT = new DataColumn();
        SRT.ColumnName = "Remamttax";
        SRT.DataType = System.Type.GetType("System.String");
        SRT.AllowDBNull = true;
        dtTemp.Columns.Add(SRT);

        DataColumn TYU = new DataColumn();
        TYU.ColumnName = "RemNotes";
        TYU.DataType = System.Type.GetType("System.String");
        TYU.AllowDBNull = true;
        dtTemp.Columns.Add(TYU);
        return dtTemp;
    }

    protected void fillgd(DataTable dttotal)
    {
        DataView myDataView = new DataView();
        myDataView = dttotal.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        grdallemp.DataSource = myDataView;
        grdallemp.DataBind();
        btnck();
    }
    protected void btnck()
    {
        decimal footnetpay = 0;

        decimal NG1 = 0;
        decimal NG2 = 0;
        decimal NG3 = 0;
        decimal NG4 = 0;
        decimal NG5 = 0;
        decimal G1 = 0;
        decimal G2 = 0;
        decimal G3 = 0;
        decimal G4 = 0;
        decimal G5 = 0;


        foreach (GridViewRow item in grdallemp.Rows)
        {
            Label lbltotremreq = (Label)item.FindControl("lbltotremreq");
            Label lblng1 = (Label)item.FindControl("lblng1");
            Label lblng2 = (Label)item.FindControl("lblng2");
            Label lblng3 = (Label)item.FindControl("lblng3");
            Label lblng4 = (Label)item.FindControl("lblng4");
            Label lblng5 = (Label)item.FindControl("lblng5");

            Label lblg1 = (Label)item.FindControl("lblg1");
            Label lblg2 = (Label)item.FindControl("lblg2");
            Label lblg3 = (Label)item.FindControl("lblg3");
            Label lblg4 = (Label)item.FindControl("lblg4");
            Label lblg5 = (Label)item.FindControl("lblg5");

            footnetpay += Convert.ToDecimal(lbltotremreq.Text);
            /////

            G1 += Convert.ToDecimal(lblg1.Text);
            G2 += Convert.ToDecimal(lblg2.Text);
            G3 += Convert.ToDecimal(lblg3.Text);
            G4 += Convert.ToDecimal(lblg4.Text);
            G5 += Convert.ToDecimal(lblg5.Text);

            NG1 += Convert.ToDecimal(lblng1.Text);
            NG2 += Convert.ToDecimal(lblng2.Text);
            NG3 += Convert.ToDecimal(lblng3.Text);
            NG4 += Convert.ToDecimal(lblng4.Text);
            NG5 += Convert.ToDecimal(lblng5.Text);
        }

        Label lblfootremreq = (Label)grdallemp.FooterRow.FindControl("lblfootremreq");

        Label lblfootg1 = (Label)grdallemp.FooterRow.FindControl("lblfootg1");
        Label lblfootg2 = (Label)grdallemp.FooterRow.FindControl("lblfootg2");
        Label lblfootg3 = (Label)grdallemp.FooterRow.FindControl("lblfootg3");
        Label lblfootg4 = (Label)grdallemp.FooterRow.FindControl("lblfootg4");
        Label lblfootg5 = (Label)grdallemp.FooterRow.FindControl("lblfootg5");

        Label lblfootng1 = (Label)grdallemp.FooterRow.FindControl("lblfootng1");
        Label lblfootng2 = (Label)grdallemp.FooterRow.FindControl("lblfootng2");
        Label lblfootng3 = (Label)grdallemp.FooterRow.FindControl("lblfootng3");
        Label lblfootng4 = (Label)grdallemp.FooterRow.FindControl("lblfootng4");
        Label lblfootng5 = (Label)grdallemp.FooterRow.FindControl("lblfootng5");


        lblfootremreq.Text = String.Format("{0:n}", footnetpay);

        lblfootg1.Text = String.Format("{0:n}", G1);
        lblfootg2.Text = String.Format("{0:n}", G2);
        lblfootg3.Text = String.Format("{0:n}", G3);
        lblfootg4.Text = String.Format("{0:n}", G4);
        lblfootg5.Text = String.Format("{0:n}", G5);
        lblfootng1.Text = String.Format("{0:n}", NG1);
        lblfootng2.Text = String.Format("{0:n}", NG2);
        lblfootng3.Text = String.Format("{0:n}", NG3);
        lblfootng4.Text = String.Format("{0:n}", NG4);
        lblfootng5.Text = String.Format("{0:n}", NG5);


    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        pnlsh.Visible = true;
        lblmsg.Text = "";
        lblbusnn.Text = " Business : " + ddlwarehouse.SelectedItem.Text;
        lblpayperiod.Text = " Pay Period : " + ddlperiod.SelectedItem.Text;
        if (chkemployee.Checked == false)
        {
            pnlGrdemp.Visible = false;
            pnlgpay.Visible = true;
            fillallemprem();
        }
        else
        {
            // pnlgpay.Visible = false;
            fillallemprem();
            pnlGrdemp.Visible = true;
            empwiserem();
        }
    }
    protected void fillallemprem()
    {
        ViewState["ISFill"] = null;
        DataTable dttotal = new DataTable();
        if (ViewState["ISFill"] == null)
        {
            dttotal = TotalCalc();
        }
        else
        {
            dttotal = (DataTable)ViewState["ISFill"];
        }
        decimal Totalempcon = 0;
        decimal TotalContamt = 0;
        decimal Totalfincont = 0;

        DataTable dsp = select("Select Distinct Sum(Cast(Totdedamt as Decimal(18,2))) as EmpContamt,tax_name+':'+ Sortname as TaxNames,PayrollTaxId as TaxId from SalaryMaster inner join  SalaryGovtDeduction on SalaryGovtDeduction.SalaryMasterId=SalaryMaster.Id inner join PayrolltaxMaster on  SalaryGovtDeduction.PayrollTaxId=PayrolltaxMaster.Payrolltax_id  where  SalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "' and SalaryGovtDeduction.PayperiodId='" + ddlperiod.SelectedValue + "' Group by tax_name,Sortname,PayrollTaxId Order by TaxNames");
        if (dsp.Rows.Count > 0)
        {
            string Datat = "select Distinct CompanyWebsiteAddressMaster.Country,CompanyWebsiteAddressMaster.State from CompanyWebsiteAddressMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId  where WareHouseMaster.WareHouseId='" + ddlwarehouse.SelectedValue + "' and AddressTypeMaster.Name='Business Address' ";
            DataTable drg = select(Datat);
            if (drg.Rows.Count > 0)
            {
                if (drg.Rows.Count > 0)
                {
                    countryId = Convert.ToString(drg.Rows[0]["Country"]);
                    Stateid = Convert.ToString(drg.Rows[0]["State"]);
                    DataTable dth = (DataTable)select("select StartDate,Report_Period_Id from [ReportPeriod] where   Report_Period_Id='" + ddlYear.SelectedValue + "'");
                    if (dth.Rows.Count > 0)
                    {
                        DataTable dtc = select("Select distinct TaxYear_Name,TaxYear_Id from  Tax_Year    where CountryId='" + drg.Rows[0]["Country"] + "' and TaxYear_Name='" + Convert.ToDateTime(dth.Rows[0]["StartDate"]).Year + "' and Active='1'");
                        if (dtc.Rows.Count > 0)
                        {
                            yearId = Convert.ToString(dtc.Rows[0]["TaxYear_Id"]);
                        }
                    }
                }
            }
            foreach (DataRow item in dsp.Rows)
            {
                DataRow dtadd = dttotal.NewRow();
                dtadd["TaxNames"] = item["TaxNames"];
                dtadd["TaxId"] = item["TaxId"];
                dtadd["EmpContamt"] = item["EmpContamt"];
                Totalempcon += Convert.ToDecimal(item["EmpContamt"]);
                DataTable dtyd = select("select Distinct * from PayrollRemittanceTaxDetailTbl where TaxID='" + item["TaxId"] + "' and Whid='" + ddlwarehouse.SelectedValue + "' and PayperiodId='" + ddlperiod.SelectedValue + "'");
                if (dtyd.Rows.Count > 0)
                {
                    dtadd["Remamttax"] = Convert.ToString(dtyd.Rows[0]["RemAmt"]);
                    dtadd["Remdate"] = Convert.ToString(dtyd.Rows[0]["RemDate"]);
                    dtadd["RemNotes"] = Convert.ToString(dtyd.Rows[0]["Remidetail"]);
                }

                decimal baseamt = 0;
                decimal stamt = 0;
                decimal cfinamt = 0;
                bool perc = true;
                DataTable dspb = select("Select Distinct Top(1) SalaryDate as Deddate,PayrolltaxMaster.Type from SalaryMaster inner join  SalaryGovtDeduction on SalaryGovtDeduction.SalaryMasterId=SalaryMaster.Id inner join PayrolltaxMaster on  SalaryGovtDeduction.PayrollTaxId=PayrolltaxMaster.Payrolltax_id  where  SalaryGovtDeduction.PayrollTaxId='" + item["TaxId"] + "'");
                if (dspb.Rows.Count > 0)
                {
                    if (Convert.ToString(dspb.Rows[0]["Deddate"]) != "")
                    {
                        dtadd["Deddate"] = Convert.ToDateTime(dspb.Rows[0]["Deddate"]).ToShortDateString();
                    }
                    else
                    {
                        dtadd["Deddate"] = "-";
                    }

                    if (Convert.ToString(dspb.Rows[0]["Type"]) == "2")
                    {
                        if (yearId != "")
                        {


                            DataTable dspcont = select("Select Distinct Payrolltaxdetailemployerbracket.*,EmpContribution from PayRollTaxDetail inner join Payrolltaxdetailemployerbracket on  Payrolltaxdetailemployerbracket.TaxdetailId=PayRollTaxDetail.Id  where  Payrolltaxmasterid='" + item["TaxId"] + "' and TaxYearId='" + yearId + "'");
                            foreach (DataRow itm in dspcont.Rows)
                            {
                                if ((Convert.ToDecimal(itm["TaxBracketStartAmtEmployee"]) <= Convert.ToDecimal(item["EmpContamt"])) && (Convert.ToDecimal(itm["TaxBracketEndAmtEmployee"]) >= Convert.ToDecimal(item["EmpContamt"])))
                                {
                                    baseamt = Convert.ToDecimal(itm["BaseTaxAmtEmployee"]);
                                    stamt = Convert.ToDecimal(itm["Tax_percentageEmployee"]);
                                    perc = Convert.ToBoolean(itm["EmpContribution"]);
                                    break;
                                }
                            }
                            if (baseamt > 0 || stamt > 0)
                            {
                                if (perc == true)
                                {
                                    cfinamt = (baseamt) + ((stamt * Convert.ToDecimal(item["EmpContamt"])) / (100));
                                }
                                else
                                {
                                    cfinamt = (baseamt) + (stamt);
                                }
                            }
                            TotalContamt += cfinamt;
                        }
                    }
                }

                dtadd["ContRate"] = stamt;
                dtadd["ContAmt"] = cfinamt;
                dtadd["totremreqi"] = cfinamt + Convert.ToDecimal(item["EmpContamt"]);
                Totalfincont += cfinamt + Convert.ToDecimal(item["EmpContamt"]); ;
                dttotal.Rows.Add(dtadd);
            }
            DataView myDataView = new DataView();
            myDataView = dttotal.DefaultView;
            // hdnsortExp.Value = "";
            ViewState["ISFill"] = dttotal;
            if (hdnsortExp11.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp11.Value, hdnsortDir11.Value);
            }

            GridView1.DataSource = myDataView;
            GridView1.DataBind();
            Label lblfootempcontemt = (Label)GridView1.FooterRow.FindControl("lblfootempcontemt");
            lblfootempcontemt.Text = String.Format("{0:n}", Totalempcon);
            Label lblfootcontamt = (Label)GridView1.FooterRow.FindControl("lblfootcontamt");
            lblfootcontamt.Text = String.Format("{0:n}", TotalContamt);
            Label lblfoottotremireq = (Label)GridView1.FooterRow.FindControl("lblfoottotremireq");
            TextBox lblfootremamt = (TextBox)GridView1.FooterRow.FindControl("lblfootremamt");
            lblfoottotremireq.Text = String.Format("{0:n}", Totalfincont);
            TextBox txtremcondate = (TextBox)GridView1.FooterRow.FindControl("txtremcondate");
            txtremcondate.Text = DateTime.Now.ToShortDateString();
            TextBox txtdetail = (TextBox)GridView1.FooterRow.FindControl("txtdetail");
            //DropDownList ddlcashac = (DropDownList)GridView1.FooterRow.FindControl("ddlcashac");

            txtdetail.Text = "Add Remittance Detail";
            lblfootremamt.Text = "0.00";
            DataTable dty = select("select * from PayrollRemittanceTbl where Whid='" + ddlwarehouse.SelectedValue + "' and PayperiodId='" + ddlperiod.SelectedValue + "'");
            if (dty.Rows.Count > 0)
            {
                lblfootremamt.Text = Convert.ToString(dty.Rows[0]["RemAmt"]);
                txtdetail.Text = Convert.ToString(dty.Rows[0]["Remidetail"]);
                txtremcondate.Text = Convert.ToDateTime(dty.Rows[0]["RemDate"]).ToShortDateString();

            }
            foreach (GridViewRow item in GridView1.Rows)
            {
                string taxid = GridView1.DataKeys[item.RowIndex].Value.ToString();
                DropDownList ddlcashac = (DropDownList)item.FindControl("ddlcashac");
                Label lblcracc = (Label)item.FindControl("lblcracc");
                Label lblempacccId = (Label)item.FindControl("lblempacccId");
                TextBox lblremamt = (TextBox)item.FindControl("lblremamt");
               // lblremamt.Enabled = false;
                DataTable DTYC = select("select CrAccId,CrAccumulateLiab,DrAccId from PayrollGovtDeductionTbl where    Whid='" + ddlwarehouse.SelectedValue + "' and PayrolltaxMasterId='" + taxid + "'");
                if (DTYC.Rows.Count > 0)
                {
                    //if (Convert.ToInt32(DTYC.Rows[0]["DrAccId"]) > 0)
                    //{
                    //    lblremamt.Enabled = true;
                    //}
                    lblcracc.Text = Convert.ToString(DTYC.Rows[0]["CrAccId"]);
                    lblempacccId.Text = Convert.ToString(DTYC.Rows[0]["CrAccumulateLiab"]);
                }
                DataTable dtall = select("SELECT Distinct (AccountMaster.AccountName)as Classgroup,AccountMaster.AccountId  FROM  GroupCompanyMaster  inner join AccountMaster  on AccountMaster.GroupId=GroupCompanyMaster.GroupId  where AccountMaster.GroupId='1'  and  AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlwarehouse.SelectedValue + "' order by Classgroup");
                if (dtall.Rows.Count > 0)
                {
                    ddlcashac.DataSource = dtall;
                    ddlcashac.DataTextField = "Classgroup";
                    ddlcashac.DataValueField = "AccountId";
                    ddlcashac.DataBind();
                }
                DataTable dt1 = (DataTable)select("Select distinct  Tranction_Details.AccountCredit,Tranction_Details_Id from AccountMaster inner join Tranction_Details on Tranction_Details.AccountCredit=AccountMaster.AccountId inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id inner join PayrollRemittanceTaxDetailTbl on PayrollRemittanceTaxDetailTbl.TID=TranctionMaster.Tranction_Master_Id Where TaxID='"+taxid+"' and Tranction_Details.AccountCredit>0 and AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and Tranction_Details.whid='" + ddlwarehouse.SelectedValue + "' and PayperiodId='" + ddlperiod.SelectedValue + "' order by Tranction_Details_Id Desc ");

                if (dt1.Rows.Count > 0)
                {
                    ddlcashac.SelectedIndex = ddlcashac.Items.IndexOf(ddlcashac.Items.FindByValue(Convert.ToString(dt1.Rows[0]["AccountCredit"])));
                }
            }

        }
    }

    protected void fillgriddisp(Boolean t)
    {
        PayNo = 0;
        grdallemp.Columns[1].Visible = t;
        grdallemp.Columns[2].Visible = t;
        grdallemp.Columns[3].Visible = t;
        grdallemp.Columns[4].Visible = t;
        grdallemp.Columns[5].Visible = t;
        grdallemp.Columns[6].Visible = t;
        grdallemp.Columns[7].Visible = t;
        grdallemp.Columns[8].Visible = t;
        grdallemp.Columns[9].Visible = t;
        grdallemp.Columns[10].Visible = t;
    }
    protected void empwiserem()
    {
        fillgriddisp(false);
        ViewState["ISFillA"] = null;
        DataTable dttotal = new DataTable();
        if (ViewState["ISFillA"] == null)
        {
            dttotal = Totalemp();
        }
        else
        {
            dttotal = (DataTable)ViewState["ISFillA"];
        }

        decimal TotalContamt = 0;


        DataTable dsp = select("Select Distinct EmployeeName,SalaryGovtDeduction.EmployeeId,SalaryDate from SalaryMaster inner join  SalaryGovtDeduction on SalaryGovtDeduction.SalaryMasterId=SalaryMaster.Id inner join PayrolltaxMaster on  SalaryGovtDeduction.PayrollTaxId=PayrolltaxMaster.Payrolltax_id inner join EmployeeMaster on EmployeeMaster.EmployeeMasterid=SalaryGovtDeduction.Employeeid where  SalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "' and SalaryGovtDeduction.PayperiodId='" + ddlperiod.SelectedValue + "'  Order by EmployeeName");
        if (dsp.Rows.Count > 0)
        {
            DataTable dspTax = select("Select Distinct tax_name,Sortname as TaxNames,PayrollTaxId as TaxId from SalaryMaster inner join  SalaryGovtDeduction on SalaryGovtDeduction.SalaryMasterId=SalaryMaster.Id inner join PayrolltaxMaster on  SalaryGovtDeduction.PayrollTaxId=PayrolltaxMaster.Payrolltax_id  where  SalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "' and SalaryGovtDeduction.PayperiodId='" + ddlperiod.SelectedValue + "'  Order by TaxNames");
            if (dspTax.Rows.Count > 0)
            {
                PayNo = dspTax.Rows.Count;
                for (int i = 0; i < dspTax.Rows.Count; i++)
                {
                    grdallemp.Columns[(i + 1)].Visible = true;
                    grdallemp.Columns[(i + 6)].Visible = true;
                    grdallemp.Columns[(i + 1)].HeaderText = Convert.ToString(dspTax.Rows[i]["TaxNames"]);
                    grdallemp.Columns[(i + 6)].HeaderText = Convert.ToString(dspTax.Rows[i]["TaxNames"]); ;
                }
            }
            string Datat = "select Distinct CompanyWebsiteAddressMaster.Country,CompanyWebsiteAddressMaster.State from CompanyWebsiteAddressMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId  where WareHouseMaster.WareHouseId='" + ddlwarehouse.SelectedValue + "' and AddressTypeMaster.Name='Business Address' ";
            DataTable drg = select(Datat);
            if (drg.Rows.Count > 0)
            {
                if (drg.Rows.Count > 0)
                {
                    countryId = Convert.ToString(drg.Rows[0]["Country"]);
                    Stateid = Convert.ToString(drg.Rows[0]["State"]);
                    DataTable dth = (DataTable)select("select StartDate,Report_Period_Id from [ReportPeriod] where   Report_Period_Id='" + ddlYear.SelectedValue + "'");
                    if (dth.Rows.Count > 0)
                    {
                        DataTable dtc = select("Select distinct TaxYear_Name,TaxYear_Id from  Tax_Year    where CountryId='" + drg.Rows[0]["Country"] + "' and TaxYear_Name='" + Convert.ToDateTime(dth.Rows[0]["StartDate"]).Year + "' and Active='1'");
                        if (dtc.Rows.Count > 0)
                        {
                            yearId = Convert.ToString(dtc.Rows[0]["TaxYear_Id"]);
                        }
                    }
                }
            }
            foreach (DataRow item in dsp.Rows)
            {
                DataRow dtadd = dttotal.NewRow();
                dtadd["EmployeeName"] = item["EmployeeName"];
                dtadd["EmployeeId"] = item["EmployeeId"];
                for (int i = 0; i < 5; i++)
                {
                    dtadd["G" + (i + 1)] = "0";
                    dtadd["NG" + (i + 1)] = "0";
                }
                DataTable dtmb = select("Select Distinct Totdedamt, PayrollTaxId as TaxId, tax_name,Sortname  as TaxNames from SalaryMaster inner join  SalaryGovtDeduction on SalaryGovtDeduction.SalaryMasterId=SalaryMaster.Id inner join PayrolltaxMaster on  SalaryGovtDeduction.PayrollTaxId=PayrolltaxMaster.Payrolltax_id  where  SalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "' and  SalaryGovtDeduction.EmployeeId='" + item["EmployeeId"] + "' and SalaryGovtDeduction.PayperiodId='" + ddlperiod.SelectedValue + "'  Order by TaxNames");
                if (dtmb.Rows.Count > 0)
                {

                    for (int i = 0; i < dtmb.Rows.Count; i++)
                    {
                        dtadd["G" + (i + 1)] = Convert.ToString(dtmb.Rows[i]["Totdedamt"]);

                        TotalContamt = TotalContamt + Convert.ToDecimal(dtmb.Rows[i]["Totdedamt"]);
                        decimal baseamt = 0;
                        decimal stamt = 0;
                        decimal cfinamt = 0;
                        bool perc = true;
                        DataTable dspb = select("Select Distinct Payrolltaxdetailemployerbracket.*,EmpContribution,PayrolltaxMaster.Type from SalaryMaster inner join  SalaryGovtDeduction on SalaryGovtDeduction.SalaryMasterId=SalaryMaster.Id inner join PayrolltaxMaster on  SalaryGovtDeduction.PayrollTaxId=PayrolltaxMaster.Payrolltax_id  " +
                            "inner join  PayRollTaxDetail on PayRollTaxDetail.Payrolltaxmasterid=PayrolltaxMaster.Payrolltax_id inner join Payrolltaxdetailemployerbracket on  Payrolltaxdetailemployerbracket.TaxdetailId=PayRollTaxDetail.Id where  PayRollTaxDetail.TaxYearId='" + yearId + "' and SalaryGovtDeduction.PayrollTaxId='" + dtmb.Rows[i]["TaxId"] + "' and SalaryGovtDeduction.PayperiodId='" + ddlperiod.SelectedValue + "' and  SalaryGovtDeduction.EmployeeId='" + item["EmployeeId"] + "'");
                        if (dspb.Rows.Count > 0)
                        {
                            if (Convert.ToString(dspb.Rows[0]["Type"]) == "2")
                            {
                                if (yearId != "")
                                {
                                    //DataTable dspcont = select("Select Distinct  from PayRollTaxDetail inner join Payrolltaxdetailemployerbracket on  Payrolltaxdetailemployerbracket.TaxdetailId=PayRollTaxDetail.Id  where  Payrolltaxmasterid='" + item["TaxId"] + "' and TaxYearId='" + yearId + "'");
                                    foreach (DataRow itm in dspb.Rows)
                                    {
                                        if ((Convert.ToDecimal(itm["TaxBracketStartAmtEmployee"]) <= Convert.ToDecimal(dtmb.Rows[i]["Totdedamt"])) && (Convert.ToDecimal(itm["TaxBracketEndAmtEmployee"]) >= Convert.ToDecimal(dtmb.Rows[i]["Totdedamt"])))
                                        {
                                            baseamt = Convert.ToDecimal(itm["BaseTaxAmtEmployee"]);
                                            stamt = Convert.ToDecimal(itm["Tax_percentageEmployee"]);
                                            perc = Convert.ToBoolean(itm["EmpContribution"]);
                                            break;
                                        }
                                    }
                                    if (baseamt > 0 || stamt > 0)
                                    {
                                        if (perc == true)
                                        {
                                            cfinamt = (baseamt) + ((stamt * Convert.ToDecimal(dtmb.Rows[i]["Totdedamt"])) / (100));
                                        }
                                        else
                                        {
                                            cfinamt = (baseamt) + (stamt);
                                        }
                                    }
                                    TotalContamt += cfinamt;
                                    dtadd["NG" + (i + 1)] = cfinamt;
                                }
                            }
                        }

                    }
                }
                dtadd["TotRemreq"] = TotalContamt;



                dttotal.Rows.Add(dtadd);
            }
            DataView myDataView = new DataView();
            myDataView = dttotal.DefaultView;
            hdnsortExp.Value = "";
            ViewState["ISFillA"] = dttotal;
            fillgd(dttotal);
            //if (hdnsortExp.Value != string.Empty)
            //{
            //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            //}

            //grdallemp.DataSource = myDataView;
            //grdallemp.DataBind();
            // btnck();
        }
    }
    protected void grdallemp_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        DataTable dttotal = (DataTable)ViewState["ISFillA"];
        if (dttotal.Rows.Count > 0)
        {
            fillgd(dttotal);
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#8EB4BF");
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Employee Deductions";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Employer Contribution";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#8EB4BF");
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Payroll Govt Remittance Details";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);






            //HeaderCell = new TableCell();
            //HeaderCell.Text = "";

            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            //HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);


            // HeaderGridRow.Cells.Add(HeaderCell);




            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnmpr_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        //Label1.Text = "";
        if (btnmpr.Text == "Printable Version")
        {
            btnmpr.Text = "Hide Printable Version";
            btnpm.Visible = true;
            //btnSuball.Visible = false;
            //if (grdallemp.Columns[18].Visible == true)
            //{
            //    ViewState["editHide"] = "tt";
            //    grdallemp.Columns[18].Visible = false;
            //}
            //if (grdallemp.Columns[19].Visible == true)
            //{
            //    ViewState["deleteHide"] = "tt";
            //    grdallemp.Columns[19].Visible = false;
            //}


        }
        else
        {
            btnmpr.Text = "Printable Version";

            btnpm.Visible = false;
            //btnck();

            //if (ViewState["editHide"] != null)
            //{
            //    grdallemp.Columns[18].Visible = true;
            //}
            //if (ViewState["deleteHide"] != null)
            //{
            //    grdallemp.Columns[19].Visible = true;
            //}

        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp11.Value = e.SortExpression.ToString();
        hdnsortDir11.Value = sortOrder; // sortOrder;
        // DataTable dttotal = (DataTable)ViewState["ISFillA"];
        fillallemprem();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        string str1gg = "delete from PayrollRemittanceTaxDetailTbl where PayperiodId='" + ddlperiod.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "'";
        SqlCommand cmdsalgg = new SqlCommand(str1gg, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int cingg = cmdsalgg.ExecuteNonQuery();
        con.Close();
        Label lblfoottotremireq = (Label)GridView1.FooterRow.FindControl("lblfoottotremireq");
        TextBox lblfootremamt = (TextBox)GridView1.FooterRow.FindControl("lblfootremamt");
        TextBox txtremcondate = (TextBox)GridView1.FooterRow.FindControl("txtremcondate");
        TextBox txtdetail = (TextBox)GridView1.FooterRow.FindControl("txtdetail");

        string status = "Unpaid";
        if (Convert.ToString(lblfootremamt.Text) != "")
        {
            if (Convert.ToString(lblfoottotremireq.Text) != "")
            {
                if (Convert.ToDecimal(lblfootremamt.Text) > 0)
                {
                    decimal Av = Math.Round(Convert.ToDecimal(lblfootremamt.Text), 0);
                    if (Av == Convert.ToDecimal(lblfoottotremireq.Text))
                    {
                        status = "Fully Paid";
                    }
                    else
                    {
                        status = "Partially Paid";
                    }
                }
                else
                {
                    status = "Unpaid";
                }
            }
            if (txtremcondate.Text.Length <= 0)
            {
                txtremcondate.Text = DateTime.Now.ToShortDateString();
            }
            DataTable dtgn = select("select * from PayrollRemittanceTbl  where PayperiodId='" + ddlperiod.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "'");
            if (dtgn.Rows.Count == 0)
            {

                string str1 = "Insert into PayrollRemittanceTbl(PayperiodId,RemAmt,RemDate,Remidetail,Paidstatus,Whid,TID)Values('" + ddlperiod.SelectedValue + "','" + lblfootremamt.Text + "','" + txtremcondate.Text + "','" + txtdetail.Text + "','" + status + "','" + ddlwarehouse.SelectedValue + "')";
                SqlCommand cmdsal = new SqlCommand(str1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                int cin = cmdsal.ExecuteNonQuery();
                con.Close();

            }
            else
            {

                string str1v = "Update PayrollRemittanceTbl set  RemAmt='" + lblfootremamt.Text + "',RemDate='" + txtremcondate.Text + "',Remidetail='" + txtdetail.Text + "',Paidstatus='" + status + "' where Id='" + dtgn.Rows[0]["Id"] + "'";
                SqlCommand cmdsalv = new SqlCommand(str1v, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                int cinv = cmdsalv.ExecuteNonQuery();
                con.Close();
            }
            lblmsg.Text = "Record save successfully";
        }
        else
        {

        }
        foreach (GridViewRow item in GridView1.Rows)
        {
            string typc = "Unpaid";
            Label lbltotremireq = (Label)item.FindControl("lbltotremireq");
            TextBox lblremamt = (TextBox)item.FindControl("lblremamt");
            TextBox lblremdatetax = (TextBox)item.FindControl("lblremdatetax");

            TextBox lblremnotes = (TextBox)item.FindControl("lblremnotes");
            DropDownList ddlcashac = (DropDownList)item.FindControl("ddlcashac");
            Label lblcracc = (Label)item.FindControl("lblcracc");
            Label lblempacccId = (Label)item.FindControl("lblempacccId");

            Label lblempcontemt = (Label)item.FindControl("lblempcontemt");
            Label lblcontamt = (Label)item.FindControl("lblcontamt");

            string taxid = GridView1.DataKeys[item.RowIndex].Value.ToString();
            if (Convert.ToString(lblremamt.Text) != "")
            {
                if (Convert.ToDecimal(lblremamt.Text) > 0)
                {
                    decimal Av = Math.Round(Convert.ToDecimal(lbltotremireq.Text), 0);
                    if (Av == Convert.ToDecimal(lblremamt.Text))
                    {
                        typc = "Fully Paid";
                    }
                    else
                    {
                        typc = "Partially Paid";
                    }
                }
                else
                {
                    typc = "Unpaid";
                }

                if (lblremdatetax.Text.Length <= 0)
                {
                    lblremdatetax.Text = DateTime.Now.ToShortDateString();
                }
                DataTable dtgn = select("select PayrollRemittanceTaxDetailTbl.* from PayrollRemittanceTaxDetailTbl inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=PayrollRemittanceTaxDetailTbl.TID where TaxID='" + taxid + "' and PayperiodId='" + ddlperiod.SelectedValue + "' and PayrollRemittanceTaxDetailTbl.Whid='" + ddlwarehouse.SelectedValue + "'");

                if (dtgn.Rows.Count > 0)
                {

                    string str1g = "Update PayrollRemittanceTaxDetailTbl Set RemAmt='" + lblremamt.Text + "',RemDate='" + lblremdatetax.Text + "',Remidetail='" + lblremnotes.Text + "',Paidstatus='" + typc + "' where Id='" + dtgn.Rows[0]["Id"] + "'";
                    SqlCommand cmdsalg = new SqlCommand(str1g, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cing = cmdsalg.ExecuteNonQuery();

                    string str1gF = "Update TranctionMaster Set Tranction_Amount='" + lblremamt.Text + "',Date='" + lblremdatetax.Text + "' where Tranction_Master_Id='" + dtgn.Rows[0]["TID"] + "'";
                    SqlCommand cmdsalgF = new SqlCommand(str1gF, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cingF = cmdsalgF.ExecuteNonQuery();
                    int idd = Convert.ToInt32(dtgn.Rows[0]["TID"]);
                    trndelete(idd.ToString());
                    filldatadataval(ddlcashac, lblremamt, idd, lblremnotes, lblremdatetax, lblcracc, lblempcontemt, lblempacccId, lblcontamt);

                }
                else
                {
                    double casn = Cashno();
                    int idd = 0;
                    idd = MainAcocount.InsertTransactionMaster(Convert.ToDateTime(lblremdatetax.Text), casn.ToString(), "1", Convert.ToInt32(Session["userid"]), Convert.ToDecimal(lblremamt.Text), ddlwarehouse.SelectedValue);
                    if (idd > 0)
                    {
                        DataTable dtv = select("select * from PayrollRemittanceTaxDetailTbl where TaxID='" + taxid + "' and PayperiodId='" + ddlperiod.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "'");
                        if (dtv.Rows.Count == 0)
                        {
                            string str1g = "Insert into PayrollRemittanceTaxDetailTbl(PayperiodId,TaxID,RemAmt,RemDate,Remidetail,Paidstatus,Whid,TID)Values('" + ddlperiod.SelectedValue + "','" + taxid + "','" + lblremamt.Text + "','" + lblremdatetax.Text + "','" + lblremnotes.Text + "','" + typc + "','" + ddlwarehouse.SelectedValue + "','" + idd + "')";
                            SqlCommand cmdsalg = new SqlCommand(str1g, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            int cing = cmdsalg.ExecuteNonQuery();
                        }
                        else
                        {
                            string str1g = "Update PayrollRemittanceTaxDetailTbl Set RemAmt='" + lblremamt.Text + "',RemDate='" + lblremdatetax.Text + "',Remidetail='" + lblremnotes.Text + "',Paidstatus='" + typc + "',TID='" + idd + "' where Id='" + dtv.Rows[0]["Id"] + "'";
                            SqlCommand cmdsalg = new SqlCommand(str1g, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            int cing = cmdsalg.ExecuteNonQuery();
                        }
                        con.Close();
                      
                        filldatadataval(ddlcashac, lblremamt, idd, lblremnotes, lblremdatetax, lblcracc, lblempcontemt, lblempacccId, lblcontamt);

                    }
                }

            }
            else
            {
                DataTable dtgn = select("select * from PayrollRemittanceTaxDetailTbl where TaxID='" + taxid + "' and PayperiodId='" + ddlperiod.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "'");
                if (dtgn.Rows.Count > 0)
                {
                    string str1ggg = "delete from PayrollRemittanceTaxDetailTbl where Id='" + dtgn.Rows[0]["Id"] + "'";
                    SqlCommand cmdsalggg = new SqlCommand(str1ggg, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cinggg = cmdsalggg.ExecuteNonQuery();
                    con.Close();

                    trndelete(dtgn.Rows[0]["TID"].ToString());
                    string STY = "delete from TranctionMaster where Tranction_Master_Id='" + dtgn.Rows[0]["TID"] + "'";
                    SqlCommand CmPs = new SqlCommand(STY, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    CmPs.ExecuteNonQuery();
                    con.Close();
                }
            }

        }


    }
    protected void trndelete(string trnasid)
    {
        string stpayapTDi = "delete from TranctionMasterSuppliment where Tranction_Master_Id='" + trnasid + "'";
        SqlCommand cmpayappTdi = new SqlCommand(stpayapTDi, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmpayappTdi.ExecuteNonQuery();
        con.Close();
        string stpayap = "delete from PaymentApplicationTbl where TranMIdPayReceived='" + trnasid + "'";
        SqlCommand cmpayapp = new SqlCommand(stpayap, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int paya = cmpayapp.ExecuteNonQuery();
        con.Close();
        string Sttdi = "delete from Tranction_Details where Tranction_Master_Id='" + trnasid + "'";
        SqlCommand cmtfi = new SqlCommand(Sttdi, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmtfi.ExecuteNonQuery();
        con.Close();

    }
    protected void filldatadataval(DropDownList ddlcashac, TextBox lblremamt, int idd, TextBox lblremnotes, TextBox lblremdatetax, Label lblcracc, Label lblempcontemt, Label lblempacccId, Label lblcontamt)
    {
        int tdidA = MainAcocount.Sp_Insert_Tranction_Details1(0, Convert.ToInt32(ddlcashac.SelectedValue), 0, Convert.ToDecimal(lblremamt.Text), idd, Convert.ToString(lblremnotes.Text), Convert.ToDateTime(lblremdatetax.Text.ToString()), ddlwarehouse.SelectedValue);
        SqlCommand cmdsupp = new SqlCommand("insert into TranctionMasterSuppliment(Tranction_Master_Id,Memo,Party_MasterId) values ('" + idd.ToString() + "','" + lblremnotes.Text + "','0')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdsupp.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdinspay = new SqlCommand("Insert into PaymentApplicationTbl values('" + idd.ToString() + "','0','" + Math.Round(Convert.ToDecimal(lblremamt.Text), 2, MidpointRounding.AwayFromZero) + "','" + Convert.ToDateTime(lblremdatetax.Text.ToString()).ToShortDateString() + "','" + Session["UserId"] + "','0')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdinspay.ExecuteNonQuery();
        con.Close();

        int tg = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(lblcracc.Text), 0, Convert.ToDecimal(lblremamt.Text), 0, idd, Convert.ToString(lblremnotes.Text), Convert.ToDateTime(lblremdatetax.Text.ToString()), ddlwarehouse.SelectedValue);
       // int tg1 = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(lblempacccId.Text), 0, Convert.ToDecimal(lblremamt.Text), 0, idd, Convert.ToString(lblremnotes.Text), Convert.ToDateTime(lblremdatetax.Text.ToString()), ddlwarehouse.SelectedValue);

    }
    protected double Cashno()
    {
        double db = 0;
        SqlCommand cmd511 = new SqlCommand("Sp_Select_CashPayment", con);
        cmd511.CommandType = CommandType.StoredProcedure;
        // cmd.Parameters.AddWithValue("@GroupId", Convert.ToInt32(ddlGroupName1.SelectedValue));
        cmd511.Parameters.AddWithValue("@compid", Session["comid"]);
        cmd511.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
        SqlDataAdapter adp511 = new SqlDataAdapter(cmd511);
        DataSet ds511 = new DataSet();
        adp511.Fill(ds511);
        if (ds511.Tables[0].Rows.Count > 0)
        {
            if (ds511.Tables[0].Rows[0]["EntryNumber"].ToString() != "")
            {
                ViewState["entryId"] = ds511.Tables[0].Rows[0][0].ToString();
                db = Convert.ToDouble(ViewState["entryId"]);
                db = (db + 1);
                ViewState["eid"] = db;
            }
            else
            {

                db = 1;
            }
        }
        else
        {
            db = 1;
        }
        return db;
    }
    protected void txtbas1_TextChanged(object sender, EventArgs e)
    {
        TextBox lblfootremamt = (TextBox)GridView1.FooterRow.FindControl("lblfootremamt");
        lblfootremamt.Text = "0.00";
        foreach (GridViewRow item in GridView1.Rows)
        {
            TextBox lblremamt = (TextBox)item.FindControl("lblremamt");
            if (lblremamt.Text.Length > 0)
            {
                lblfootremamt.Text = (Convert.ToDecimal(lblfootremamt.Text) + Convert.ToDecimal(lblremamt.Text)).ToString();
            }
        }
    }

}

