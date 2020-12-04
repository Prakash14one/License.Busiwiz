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

public partial class PayrollRemittanceSummary : System.Web.UI.Page
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
        DataTable dt = (DataTable)select("select Name,Report_Period_Id from [ReportPeriod] where   Whid='" + ddlwarehouse.SelectedValue + "'");
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
       // ddlYear_SelectedIndexChanged(sender, e);



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
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#8EB4BF");
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
                HeaderCell.Text = "Payroll Govt Remittance Details";

                HeaderCell.ColumnSpan =4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#8EB4BF");
                HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
                HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);


                HeaderGridRow.Cells.Add(HeaderCell);

           


            grdallemp.Controls[0].Controls.AddAt(0, HeaderGridRow);

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
        
        DataColumn srttt = new DataColumn();
        srttt.ColumnName = "Remdate";
        srttt.DataType = System.Type.GetType("System.String");
        srttt.AllowDBNull = true;
        dtTemp.Columns.Add(srttt);


        DataColumn SRT = new DataColumn();
        SRT.ColumnName = "Remamttax";
        SRT.DataType = System.Type.GetType("System.Decimal");
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

        decimal Totremit = 0;
        int coltrn = 0;
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
            Label lblremamt = (Label)item.FindControl("lblremamt");
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
            if (lblremamt.Text.Length > 0)
            {
                Totremit += Convert.ToDecimal(lblremamt.Text);
            }
            //if (coltrn == 0)
            //{
            //    grdallemp.Rows[item.RowIndex].Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCFF");
            //    grdallemp.Rows[item.RowIndex].Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCFF");

            //    grdallemp.Rows[item.RowIndex].Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCFF");

            //    grdallemp.Rows[item.RowIndex].Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCFF");

            //    grdallemp.Rows[item.RowIndex].Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCFF");

            //    grdallemp.Rows[item.RowIndex].Cells[11].BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCFF");

            //    grdallemp.Rows[item.RowIndex].Cells[12].BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCFF");

            //    grdallemp.Rows[item.RowIndex].Cells[13].BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCFF");

            //    grdallemp.Rows[item.RowIndex].Cells[14].BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCFF");
              
            //    coltrn = 1;
            //}
            //else
            //{
            //    grdallemp.Rows[item.RowIndex].Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFEFFF");
            //    grdallemp.Rows[item.RowIndex].Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFEFFF");
            //    grdallemp.Rows[item.RowIndex].Cells[3].BackColor = System.Drawing.Color.Empty;
            //    grdallemp.Rows[item.RowIndex].Cells[4].BackColor = System.Drawing.Color.Empty;
            //    grdallemp.Rows[item.RowIndex].Cells[5].BackColor = System.Drawing.Color.Empty;
            //    grdallemp.Rows[item.RowIndex].Cells[11].BackColor = System.Drawing.Color.Empty;
            //    grdallemp.Rows[item.RowIndex].Cells[12].BackColor = System.Drawing.Color.Empty;
            //    grdallemp.Rows[item.RowIndex].Cells[13].BackColor = System.Drawing.Color.Empty;
            //    grdallemp.Rows[item.RowIndex].Cells[14].BackColor = System.Drawing.Color.Empty;
            //    coltrn = 0;
            //}
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
        Label lblfootremamt = (Label)grdallemp.FooterRow.FindControl("lblfootremamt");

        lblfootremamt.Text = String.Format("{0:n}", Totremit);

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
        //lblpayperiod.Text = " Year : " + ddlYear.SelectedItem.Text;
        lblGr1.Text = "List of Payroll Deductions and Remittance Report for the Year - " + ddlYear.SelectedItem.Text;
        lbltrbal.Text = "List of Payroll Deductions and Remittance Report for the Year - " + ddlYear.SelectedItem.Text;
            pnlGrdemp.Visible = true;
            empwiserem();
       
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

        string Datat = "select Distinct CompanyWebsiteAddressMaster.Country,CompanyWebsiteAddressMaster.State from CompanyWebsiteAddressMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId  where WareHouseMaster.WareHouseId='" + ddlwarehouse.SelectedValue + "' and AddressTypeMaster.Name='Business Address' ";
        DataTable drg = select(Datat);
        if (drg.Rows.Count > 0)
        {
            if (drg.Rows.Count > 0)
            {
                countryId = Convert.ToString(drg.Rows[0]["Country"]);
                Stateid = Convert.ToString(drg.Rows[0]["State"]);
                DataTable dtRR = (DataTable)select("select StartDate,Report_Period_Id from [ReportPeriod] where   Report_Period_Id='" + ddlYear.SelectedValue + "'");
                if (dtRR.Rows.Count > 0)
                {
                    DataTable dtc = select("Select distinct TaxYear_Name,TaxYear_Id from  Tax_Year    where CountryId='" + drg.Rows[0]["Country"] + "' and TaxYear_Name='" + Convert.ToDateTime(dtRR.Rows[0]["StartDate"]).Year + "' and Active='1'");
                    if (dtc.Rows.Count > 0)
                    {
                        yearId = Convert.ToString(dtc.Rows[0]["TaxYear_Id"]);
                    }
                }
            }
        }
        if (yearId != "")
        {
            DataTable dsp = select("Select Distinct  PayperiodName,payperiodMaster.Id from SalaryMaster inner join  SalaryGovtDeduction on SalaryGovtDeduction.SalaryMasterId=SalaryMaster.Id inner join PayrolltaxMaster on  SalaryGovtDeduction.PayrollTaxId=PayrolltaxMaster.Payrolltax_id inner join payperiodMaster on payperiodMaster.Id=SalaryGovtDeduction.PayperiodId where  SalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "' and SalaryGovtDeduction.TaxYear='" + yearId + "'   Order by PayperiodName");
            if (dsp.Rows.Count > 0)
            {
                DataTable dspTax = select("Select Distinct tax_name,SortName as TaxNames,PayrollTaxId as TaxId from SalaryMaster inner join  SalaryGovtDeduction on SalaryGovtDeduction.SalaryMasterId=SalaryMaster.Id inner join PayrolltaxMaster on  SalaryGovtDeduction.PayrollTaxId=PayrolltaxMaster.Payrolltax_id  where  SalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "' and SalaryGovtDeduction.TaxYear='" + yearId + "'  Order by TaxNames");
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

                foreach (DataRow item in dsp.Rows)
                {
                    DataRow dtadd = dttotal.NewRow();
                    dtadd["EmployeeName"] = item["PayperiodName"];
                    dtadd["EmployeeId"] = item["Id"];
                    for (int i = 0; i < 5; i++)
                    {
                        dtadd["G" + (i + 1)] = "0";
                        dtadd["NG" + (i + 1)] = "0";
                    }
                    DataTable dtmb = select("Select Distinct  Sum(Cast(Totdedamt as Decimal(18,2))) as Totdedamt,PayrollTaxId as TaxId, SortName as TaxNames from SalaryMaster inner join  SalaryGovtDeduction on SalaryGovtDeduction.SalaryMasterId=SalaryMaster.Id inner join PayrolltaxMaster on  SalaryGovtDeduction.PayrollTaxId=PayrolltaxMaster.Payrolltax_id  where  SalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "' and  SalaryGovtDeduction.TaxYear='" + yearId + "' and SalaryGovtDeduction.PayperiodId='" + item["Id"] + "' Group by PayrollTaxId,SortName Order by TaxNames");
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
                                "inner join  PayRollTaxDetail on PayRollTaxDetail.Payrolltaxmasterid=PayrolltaxMaster.Payrolltax_id inner join Payrolltaxdetailemployerbracket on  Payrolltaxdetailemployerbracket.TaxdetailId=PayRollTaxDetail.Id where  PayRollTaxDetail.TaxYearId='" + yearId + "' and SalaryGovtDeduction.PayrollTaxId='" + dtmb.Rows[i]["TaxId"] + "'  and  SalaryGovtDeduction.TaxYear='" + yearId + "'");
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
                    DataTable dty = select("select * from PayrollRemittanceTbl where Whid='" + ddlwarehouse.SelectedValue + "' and PayperiodId='" + item["Id"] + "'");
                    if (dty.Rows.Count > 0)
                    {
                        dtadd["Remdate"] = Convert.ToDateTime(dty.Rows[0]["RemDate"]).ToShortDateString();
                        dtadd["Remamttax"] = Convert.ToString(dty.Rows[0]["RemAmt"]);
                        dtadd["RemNotes"] = Convert.ToString(dty.Rows[0]["Remidetail"]);
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

            }
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
   
  
   
}

