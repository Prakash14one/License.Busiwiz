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

public partial class Employeepayrollsummarywithupp : System.Web.UI.Page
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
    public static string CPP = "0", EI = "0", PI = "0", P = "0", I = "0", F = "0", F1 = "0", F2 = "0", U1 = "0", HD = "0", L = "0", T = "0", A = "0", A1 = "0", B = "0", B1 = "0", C = "0", D = "0", D1 = "0", E = "0", E1 = "0", F3 = "0", F4 = "0", I1 = "0", IE = "0", K = "0", KP = "0", K1 = "0", K1P = "0", K2P = "0", K2 = "0", K2Q = "0", K3 = "0", K3P = "0", K4 = "0", K4P = "0";
    decimal paymonth = 0;
    decimal payweek = 0;
    public static int GovDisp = 0;
    public static int PayNo = 0;
    MathFunctions.MathParser mp = new MathFunctions.MathParser();
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
            btncalvd.Visible = true;
            fillwarehouse();
            if (Request.QueryString["SL"] != null)
            {
                btncalvd.Visible = false;
                ViewState["dk"] = Request.QueryString["SL"];
                pnlGS.Visible = false;
                ddlwarehouse.SelectedValue = Request.QueryString["Wid"];
                ddlperiodtype.Items.Clear();
                fillrelac();
                DataTable dsp = select("Select Distinct payperiodtype.Id,payperiodtype.Name from SalaryMaster  inner join payperiodMaster on payperiodMaster.Id=SalaryMaster.payperiodtypeId inner join payperiodtype  on payperiodtype.Id=payperiodMaster.PayperiodTypeID   where  SalaryMaster.Id='" + Request.QueryString["SL"] + "';");
                if (dsp.Rows.Count > 0)
                {
                    ddlperiodtype.DataSource = dsp;
                    ddlperiodtype.DataTextField = "Name";
                    ddlperiodtype.DataValueField = "Id";
                    ddlperiodtype.DataBind();

                }

                DataTable dttotal = (DataTable)select("Select distinct payperiodMaster.ID,PayperiodName +' : '+Convert(nvarchar,PayperiodStartDate,101) +' : '+ Convert(nvarchar,PayperiodEndDate,101) as period, payperiodMaster.Id from  SalaryMaster inner join payperiodMaster on " +
                " payperiodMaster.Id=SalaryMaster.payperiodtypeId where SalaryMaster.Id='" + Request.QueryString["SL"] + "'");
                ddlpayperiod.DataSource = dttotal;
                ddlpayperiod.DataTextField = "period";
                ddlpayperiod.DataValueField = "ID";
                ddlpayperiod.DataBind();


                ddlpayperiod.Items.Insert(0, "Select");
                ddlpayperiod.Items[0].Value = "0";
                ddlpayperiod.SelectedValue = Convert.ToString(dttotal.Rows[0]["ID"]);
                string[] separator1 = new string[] { " : " };
                string[] strSplitArr1 = ddlpayperiod.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                if (strSplitArr1.Length > 1)
                {
                    sdate = strSplitArr1[1].ToString();
                    edate = strSplitArr1[2].ToString();
                }
                else
                {
                    edate = DateTime.Now.AddDays(-1).ToShortDateString();
                }
                DataTable dspC = select("Select Distinct EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterId from EmployeeMaster inner join  SalaryMaster on SalaryMaster.EmployeeId=EmployeeMasterId  inner join payperiodMaster on payperiodMaster.Id=SalaryMaster.payperiodtypeId inner join payperiodtype  on payperiodtype.Id=payperiodMaster.PayperiodTypeID   where  SalaryMaster.Id='" + Request.QueryString["SL"] + "'");
                ddlemp.DataSource = dspC;
                ddlemp.DataTextField = "EmployeeName";
                ddlemp.DataValueField = "EmployeeMasterId";
                ddlemp.DataBind();


                Label1.Text = "";
                ViewState["paycy"] = "";
                Nullable();
                DataTable dt1 = (DataTable)select(" Select distinct  SalaryMaster.* from TranctionMaster inner join SalaryMaster on SalaryMaster.Tid=TranctionMaster.Tranction_Master_Id inner join " +
               " SalaryRemuneration on SalaryRemuneration.SalaryMasterId=SalaryMaster.Id  Left join SalaryDeduction on SalaryDeduction.SalaryMasterId=SalaryMaster.Id where  SalaryMaster.Id='" + Request.QueryString["SL"] + "'");

                if (dt1.Rows.Count > 0)
                {
                    Recordcheck(dt1);
                    pnlcau.Visible = true;

                    //FillAllEmpData(Convert.ToString(dt1.Rows[0]["Id"]), "");

                }
            }
            else
            {
                //fillyearid();
                ddlwarehouse_SelectedIndexChanged(sender, e);
            }
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
        DataTable dsp = select("select Id as payperiodtypeId,Name from payperiodtype order by Name");
        if (dsp.Rows.Count > 0)
        {
            ddlperiodtype.DataSource = dsp;
            ddlperiodtype.DataTextField = "Name";
            ddlperiodtype.DataValueField = "payperiodtypeId";
            ddlperiodtype.DataBind();
            ddlperiodtype.SelectedValue = "5";
        }
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
        try
        {
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter dtp = new SqlDataAdapter(cmd);

            dtp.Fill(dt);

        }
        catch (Exception er)
        {
            lblmsg.Visible = true;
            lblmsg.Text = str + " :: " + er;
        }
        return dt;

    }
    public DataTable HourlyRemu()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prdi = new DataColumn();
        prdi.ColumnName = "Id";
        prdi.DataType = System.Type.GetType("System.String");
        prdi.AllowDBNull = true;
        dtTemp.Columns.Add(prdi);
        DataColumn prd = new DataColumn();
        prd.ColumnName = "RemunarationName";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "Rate";
        ssCatId.DataType = System.Type.GetType("System.String");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);


        DataColumn catname = new DataColumn();
        catname.ColumnName = "perunitname";
        catname.DataType = System.Type.GetType("System.String");
        catname.AllowDBNull = true;
        dtTemp.Columns.Add(catname);

        DataColumn barcode = new DataColumn();
        barcode.ColumnName = "totalunit";
        barcode.DataType = System.Type.GetType("System.String");
        barcode.AllowDBNull = true;
        dtTemp.Columns.Add(barcode);

        DataColumn remperunit = new DataColumn();
        remperunit.ColumnName = "totalunitpay";
        remperunit.DataType = System.Type.GetType("System.String");
        remperunit.AllowDBNull = true;
        dtTemp.Columns.Add(remperunit);

        DataColumn remptotamt = new DataColumn();
        remptotamt.ColumnName = "Totalamt";
        remptotamt.DataType = System.Type.GetType("System.String");
        remptotamt.AllowDBNull = true;
        dtTemp.Columns.Add(remptotamt);

        //DataColumn EffectiveStartDate = new DataColumn();
        //EffectiveStartDate.ColumnName = "EffectiveStartDate";
        //EffectiveStartDate.DataType = System.Type.GetType("System.String");
        //EffectiveStartDate.AllowDBNull = true;
        //dtTemp.Columns.Add(EffectiveStartDate);

        //DataColumn EffectiveEndDate = new DataColumn();
        //EffectiveEndDate.ColumnName = "EffectiveEndDate";
        //EffectiveEndDate.DataType = System.Type.GetType("System.String");
        //EffectiveEndDate.AllowDBNull = true;
        //dtTemp.Columns.Add(EffectiveEndDate);

        DataColumn dtover = new DataColumn();
        dtover.ColumnName = "OverTime";
        dtover.DataType = System.Type.GetType("System.String");
        dtover.AllowDBNull = true;
        dtTemp.Columns.Add(dtover);
        return dtTemp;
    }

    public DataTable Daily()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prdi = new DataColumn();
        prdi.ColumnName = "Id";
        prdi.DataType = System.Type.GetType("System.String");
        prdi.AllowDBNull = true;
        dtTemp.Columns.Add(prdi);
        DataColumn prd = new DataColumn();
        prd.ColumnName = "RemunarationName";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "Rate";
        ssCatId.DataType = System.Type.GetType("System.String");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);


        DataColumn catname = new DataColumn();
        catname.ColumnName = "perunitname";
        catname.DataType = System.Type.GetType("System.String");
        catname.AllowDBNull = true;
        dtTemp.Columns.Add(catname);

        DataColumn barcode = new DataColumn();
        barcode.ColumnName = "totalunit";
        barcode.DataType = System.Type.GetType("System.String");
        barcode.AllowDBNull = true;
        dtTemp.Columns.Add(barcode);

        DataColumn remperunit = new DataColumn();
        remperunit.ColumnName = "totalunitpay";
        remperunit.DataType = System.Type.GetType("System.String");
        remperunit.AllowDBNull = true;
        dtTemp.Columns.Add(remperunit);

        DataColumn remptotamt = new DataColumn();
        remptotamt.ColumnName = "Totalamt";
        remptotamt.DataType = System.Type.GetType("System.String");
        remptotamt.AllowDBNull = true;
        dtTemp.Columns.Add(remptotamt);

        return dtTemp;
    }
    public DataTable Monthly()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prdi = new DataColumn();
        prdi.ColumnName = "Id";
        prdi.DataType = System.Type.GetType("System.String");
        prdi.AllowDBNull = true;
        dtTemp.Columns.Add(prdi);
        DataColumn prd = new DataColumn();
        prd.ColumnName = "RemunarationName";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "Rate";
        ssCatId.DataType = System.Type.GetType("System.String");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);


        DataColumn catname = new DataColumn();
        catname.ColumnName = "perunitname";
        catname.DataType = System.Type.GetType("System.String");
        catname.AllowDBNull = true;
        dtTemp.Columns.Add(catname);


        DataColumn completemonth = new DataColumn();
        completemonth.ColumnName = "completemonth";
        completemonth.DataType = System.Type.GetType("System.String");
        completemonth.AllowDBNull = true;
        dtTemp.Columns.Add(completemonth);

        DataColumn completedmonthamt = new DataColumn();
        completedmonthamt.ColumnName = "completedmonthamt";
        completedmonthamt.DataType = System.Type.GetType("System.String");
        completedmonthamt.AllowDBNull = true;
        dtTemp.Columns.Add(completedmonthamt);


        DataColumn barcode = new DataColumn();
        barcode.ColumnName = "totalunit";
        barcode.DataType = System.Type.GetType("System.String");
        barcode.AllowDBNull = true;
        dtTemp.Columns.Add(barcode);

        DataColumn remperunit = new DataColumn();
        remperunit.ColumnName = "totalunitpay";
        remperunit.DataType = System.Type.GetType("System.String");
        remperunit.AllowDBNull = true;
        dtTemp.Columns.Add(remperunit);

        DataColumn remptotamt = new DataColumn();
        remptotamt.ColumnName = "Totalamt";
        remptotamt.DataType = System.Type.GetType("System.String");
        remptotamt.AllowDBNull = true;
        dtTemp.Columns.Add(remptotamt);

        return dtTemp;
    }
    protected void fillincome(DataTable dt111, DataTable dtovertime)
    {
        if (grdcal.Rows.Count <= 0)
        {
            ViewState["TempDataTable"] = null;

            pnlcau.Visible = false;
        }
        else
        {
            btnaddnewrem.Visible = true;
            pnlcau.Visible = true;
        }

        DataTable dtTemp = new DataTable();
        if (ViewState["TempDataTable"] == null)
        {
            dtTemp = HourlyRemu();

        }
        else
        {
            dtTemp = (DataTable)ViewState["TempDataTable"];
        }
        DataRow dtadd = dtTemp.NewRow();
        if (ViewState["TempDataTable"] != null)
        {
            dtadd["Id"] = "0";
            dtadd["RemunarationName"] = "";
            dtadd["Rate"] = "0";
            dtadd["perunitname"] = "Hour";

            dtadd["totalunit"] = "0";
            dtadd["totalunitpay"] = "0";
            dtadd["totalamt"] = "0";
            dtadd["OverTime"] = "Ex";

            dtTemp.Rows.Add(dtadd);

            ViewState["TempDataTable"] = dtTemp;
            grdcal.DataSource = dtTemp;
            grdcal.DataBind();
        }
        else
        {
            foreach (DataRow dts in dt111.Rows)
            {
                if (Convert.ToString(dts["perunitname"]) == "Hour")
                {
                    decimal totpayhour = 0;
                    decimal totovertimehoue = 0;
                    decimal payamt = 0;
                    decimal totbatho = 0;

                    //Overtime in allowed attandance rules..
                    // DataTable dtovertime = (DataTable)select("Select * from AttandanceRule where StoreId='" + ddlwarehouse.SelectedValue + "'");



                    //  DataTable dt111 = (DataTable)select("Select distinct convert(nvarchar,EffectiveStartDate,101) as EffectiveStartDate,convert(nvarchar,EffectiveEndDate,101) as EffectiveEndDate, RemunerationMaster.Id,EmployeeID, RemunerationMaster.RemunerationName as RemunarationName,CASE WHEN (RM1.RemunerationName IS NULL)THEN '-' ELSE RM1.RemunerationName END AS  remperunit,CASE WHEN (Amount IS NULL)THEN '0' ELSE Amount END AS  Rate,case when (Period_name IS NULL) then '--' Else Period_name End as perunitname,CASE WHEN (Percentage IS NULL)Then '0'Else Percentage End as Perchantage,CASE WHEN (IsPercentRemunerationId IS NULL) THEN '0' ELSE IsPercentRemunerationId END AS perunitrate from  RemunerationMaster inner join EmployeeSalaryMaster on EmployeeSalaryMaster.Remuneration_Id=RemunerationMaster.Id Left join PeriodMaster12 on PeriodMaster12.Id=EmployeeSalaryMaster.PayablePer_PeriodMasterId  Left join  RemunerationMaster AS RM1 on EmployeeSalaryMaster.IsPercentRemunerationId=RM1.Id where EmployeeID='" + ddlemp.SelectedValue + "' and(EmployeeSalaryMaster.EffectiveEndDate>='" + sdate + "' or EmployeeSalaryMaster.EffectiveStartDate>='" + sdate + "') and (EmployeeSalaryMaster.EffectiveEndDate<='" + edate + "' or EmployeeSalaryMaster.EffectiveStartDate<='" + edate + "') order by EmployeeID, perunitname Desc");
                    //foreach (DataRow dts in dt111.Rows)
                    //{
                    dtadd["Id"] = Convert.ToString(dts["Id"]);
                    dtadd["RemunarationName"] = Convert.ToString(dts["RemunarationName"]);
                    dtadd["Rate"] = Convert.ToString(dts["Rate"]);
                    dtadd["perunitname"] = Convert.ToString(dts["perunitname"]);
                    dtadd["totalunit"] = "0";
                    dtadd["totalunitpay"] = "0";
                    dtadd["totalamt"] = "0";
                    dtadd["OverTime"] = "-";

                    dtTemp.Rows.Add(dtadd);


                    // }

                    ViewState["TempDataTable"] = dtTemp;

                    DataTable dthou = (DataTable)select("Select distinct BatchMaster.Id from BatchMaster inner join BatchTiming on BatchTiming.BatchMasterId=BatchMaster.Id inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchTiming.BatchMasterId where EmployeeBatchMaster.Employeeid='" + ddlemp.SelectedValue + "' and BatchMaster.Status='1' order by BatchMaster.Id Desc");
                    if (dthou.Rows.Count > 0)
                    {
                        DataTable dtspay111 = (DataTable)select("Select distinct BatchTiming.Totalhours,Cast(DateMasterTbl.Date as date) from DateMasterTbl inner join  BatchWorkingDays on BatchWorkingDays.DateMasterID= DateMasterTbl.DateId inner join BatchTiming on BatchTiming.BatchMasterId=BatchWorkingDays.BatchID inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchTiming.BatchMasterId where BatchTiming.BatchMasterId='" + dthou.Rows[0]["Id"] + "' and EmployeeBatchMaster.Employeeid='" + ddlemp.SelectedValue + "' and Cast(Date as Date) Between '" + sdate + "' and '" + edate + "'");
                        foreach (DataRow dtsb in dtspay111.Rows)
                        {
                            decimal bhour = 0;
                            string[] separbm = new string[] { ":" };
                            string[] strSplitArrbm = dtsb["Totalhours"].ToString().Split(separbm, StringSplitOptions.RemoveEmptyEntries);
                            bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));

                            totbatho += bhour;
                        }
                    }
                    // DataTable dtspay = (DataTable)select("Select  Payabledays, TotalHourWork as totalpay, cast(left(TotalHourWork,2) as Decimal) as payhour,cast(Right(TotalHourWork,2) as Decimal) as payminites,BatchRequiredhours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + sdate + "' and '" + edate + "' and TotalHourWork IS not Null and OutTimeforcalculation<>'00:00' ");
                    DataTable dtspay = (DataTable)select("Select  Payabledays, BatchRequiredhours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + sdate + "' and '" + edate + "' and BatchRequiredhours IS not Null and OutTimeforcalculation<>'00:00' and Varify='1' ");

                    foreach (DataRow dtsb in dtspay.Rows)
                    {
                        if (Convert.ToString(dtsb["Payabledays"]) != "0" && Convert.ToString(dtsb["BatchRequiredhours"]) != "")
                        {
                            // decimal bhour = 0;
                            //decimal punit = 0;
                            string[] separbm = new string[] { ":" };
                            string[] strSplitArrbm = dtsb["BatchRequiredhours"].ToString().Split(separbm, StringSplitOptions.RemoveEmptyEntries);

                            if (Convert.ToString(dtsb["Payabledays"]) == "1")
                            {
                                totpayhour += ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));
                            }
                            else
                            {
                                totpayhour += ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)) / 2);

                            }

                        }
                    }
                    DataTable alter = (DataTable)(ViewState["TempDataTable"]);
                    if (alter.Rows.Count > 0)
                    {
                        foreach (DataRow ds in alter.Rows)
                        {
                            ds["totalunit"] = Math.Round(totbatho, 2);
                            ds["totalunitpay"] = Math.Round(totpayhour, 2);
                            ds["totalamt"] = Math.Round(((totpayhour) * (Convert.ToDecimal(ds["Rate"]))), 2);
                            payamt += Math.Round(((totpayhour) * (Convert.ToDecimal(ds["Rate"]))), 2);
                            //ds["totalunit"] = Math.Round(totbatho, 2);
                            //ds["totalunitpay"] = Math.Round(totpayhour, 2);
                            //ds["totalamt"] = Math.Round(((totpayhour) * (Convert.ToDecimal(ds["Rate"]))), 2);
                            //payamt += Math.Round(((totpayhour) * (Convert.ToDecimal(ds["Rate"]))), 2);
                        }
                        totovertimehoue = totalover(dtovertime);
                        if (totovertimehoue > 0)
                        {
                            totovertimehoue = Math.Round(totovertimehoue, 2);
                            //dtadd1["Id"]= Convert.ToString(alter.Rows[0]["Id"]);
                            ViewState["TempDataTable"] = alter;
                            DataRow dtadd1 = dtTemp.NewRow();
                            dtTemp = (DataTable)ViewState["TempDataTable"];
                            dtadd1["Id"] = "0";
                            dtadd1["RemunarationName"] = "Salary OverTime";
                            decimal acrate = Convert.ToDecimal(alter.Rows[0]["Rate"]);

                            if (Convert.ToString(dtovertime.Rows[0]["overtimeruleno"]) == "3")
                            {
                                if (Convert.ToString(dtovertime.Rows[0]["OvertimepaymentRate"]) != "")
                                {
                                    acrate = Math.Round(acrate * Convert.ToDecimal(dtovertime.Rows[0]["OvertimepaymentRate"]) / 100, 2);
                                }
                            }
                            dtadd1["Rate"] = acrate;
                            dtadd1["perunitname"] = "Hour";
                            dtadd1["OverTime"] = "Over";
                            dtadd1["totalunit"] = Math.Round(totovertimehoue, 2);
                            dtadd1["totalunitpay"] = Math.Round(totovertimehoue, 2);
                            dtadd1["totalamt"] = Math.Round(((totovertimehoue) * (acrate)), 2);

                            payamt += Math.Round(((totovertimehoue) * (acrate)), 2);

                            dtTemp.Rows.Add(dtadd1);
                            ViewState["TempDataTable"] = dtTemp;
                        }
                        grdcal.DataSource = dtTemp;
                        grdcal.DataBind();
                        txttotincome.Text = payamt.ToString();
                        lbltotremunration.Text = payamt.ToString();
                        if (grdcal.Rows.Count <= 0)
                        {
                            ViewState["TempDataTable"] = null;

                            pnlcau.Visible = false;
                            pnlhourly.Visible = false;
                        }
                        else
                        {
                            btnaddnewrem.Visible = true;
                            pnlhourly.Visible = true;
                            pnlcau.Visible = true;
                        }
                    }

                }
            }
        }
    }

    protected void filldaily(DataTable dt111, DataTable dtovertime)
    {
        if (grddaily.Rows.Count <= 0)
        {
            ViewState["Tempdaily"] = null;

            pnlcau.Visible = false;
            pnldaily.Visible = false;
        }
        else
        {
            btndaily.Visible = true;
            pnlcau.Visible = true;
            pnldaily.Visible = true;
        }

        DataTable dtTemp = new DataTable();
        if (ViewState["Tempdaily"] == null)
        {
            dtTemp = Daily();

        }
        else
        {
            dtTemp = (DataTable)ViewState["Tempdaily"];
        }
        DataRow dtadd = dtTemp.NewRow();
        if (ViewState["Tempdaily"] != null)
        {
            dtadd["Id"] = "0";
            dtadd["RemunarationName"] = "";
            dtadd["Rate"] = "0";
            dtadd["perunitname"] = "Day";

            dtadd["totalunit"] = "0";
            dtadd["totalunitpay"] = "0";
            dtadd["totalamt"] = "0";


            dtTemp.Rows.Add(dtadd);

            ViewState["Tempdaily"] = dtTemp;
            grddaily.DataSource = dtTemp;
            grddaily.DataBind();
        }
        else
        {
            foreach (DataRow dts in dt111.Rows)
            {
                if (Convert.ToString(dts["perunitname"]) == "Day")
                {
                    decimal totpayhour = 0;
                    decimal totovertimehoue = 0;
                    decimal payamt = 0;
                    decimal totbatho = 0;

                    dtadd["Id"] = Convert.ToString(dts["Id"]);
                    dtadd["RemunarationName"] = Convert.ToString(dts["RemunarationName"]);
                    dtadd["Rate"] = Convert.ToString(dts["Rate"]);
                    dtadd["perunitname"] = Convert.ToString(dts["perunitname"]);
                    dtadd["totalunit"] = "0";
                    dtadd["totalunitpay"] = "0";
                    dtadd["totalamt"] = "0";


                    dtTemp.Rows.Add(dtadd);

                    ViewState["Tempdaily"] = dtTemp;
                    DataTable dtspay111 = (DataTable)select("Select Count(Distinct DateMasterID) from DateMasterTbl inner join  BatchWorkingDays on BatchWorkingDays.DateMasterID= DateMasterTbl.DateId inner join BatchTiming on BatchTiming.BatchMasterId=BatchWorkingDays.BatchID inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchTiming.BatchMasterId where EmployeeBatchMaster.Employeeid='" + ddlemp.SelectedValue + "' and Cast(Date as Date) Between '" + sdate + "' and '" + edate + "'");
                    if (dtspay111.Rows[0][0] != "")
                    {
                        totbatho = Convert.ToDecimal(dtspay111.Rows[0][0]);

                        //decimal bhour = 0;
                        //string[] separbm = new string[] { ":" };
                        //string[] strSplitArrbm = dtsb["Totalhours"].ToString().Split(separbm, StringSplitOptions.RemoveEmptyEntries);
                        //bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));

                        //totbatho += bhour;
                        //}
                    }
                    decimal bhour = 0;
                    DataTable dtspay = (DataTable)select("Select  EmployeeId,Date, Payabledays, TotalHourWork as totalpay, cast(left(TotalHourWork,2) as Decimal) as payhour,cast(Right(TotalHourWork,2) as Decimal) as payminites,BatchRequiredhours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + sdate + "' and '" + edate + "' and TotalHourWork IS not Null and OutTimeforcalculation<>'00:00' and Varify='1'  order by totalpay Desc");
                    decimal totalday = 0;
                    string dateco = "";
                    foreach (DataRow dtsb in dtspay.Rows)
                    {
                        if (Convert.ToString(dtsb["Payabledays"]) != "0" && Convert.ToString(dtsb["BatchRequiredhours"]) != "")
                        {
                            if (Convert.ToString(dtsb["Date"]) != dateco)
                            {
                                totalday += (Convert.ToDecimal(dtsb["Payabledays"]));
                                dateco = Convert.ToString(dtsb["Date"]);
                            }
                            // decimal punit = 0;
                            string[] separbm = new string[] { ":" };
                            string[] strSplitArrbm = dtsb["BatchRequiredhours"].ToString().Split(separbm, StringSplitOptions.RemoveEmptyEntries);
                            bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));


                        }
                    }
                    DataTable alter = (DataTable)(ViewState["Tempdaily"]);
                    if (alter.Rows.Count > 0)
                    {
                        foreach (DataRow ds in alter.Rows)
                        {

                            ds["totalunit"] = Math.Round(totbatho, 2);
                            ds["totalunitpay"] = Math.Round(totalday, 2);
                            ds["totalamt"] = Math.Round(((totalday) * (Convert.ToDecimal(ds["Rate"]))), 2);
                            payamt += Math.Round(((totalday) * (Convert.ToDecimal(ds["Rate"]))), 2);
                        }
                        totovertimehoue = totalover(dtovertime);
                        if (totovertimehoue > 0)
                        {
                            if (grdcal.Rows.Count <= 0)
                            {
                                ViewState["TempDataTable"] = null;

                                pnlcau.Visible = false;
                            }
                            else
                            {

                                pnlcau.Visible = true;
                            }

                            DataTable dtTempover = new DataTable();
                            if (ViewState["TempDataTable"] == null)
                            {
                                dtTempover = HourlyRemu();

                            }
                            else
                            {
                                dtTempover = (DataTable)ViewState["TempDataTable"];
                            }

                            DataRow dtadd1 = dtTempover.NewRow();

                            dtadd1["Id"] = "0";
                            dtadd1["RemunarationName"] = "Salary OverTime";
                            decimal acrate = Convert.ToDecimal(alter.Rows[0]["Rate"]) / Convert.ToDecimal(bhour);

                            if (Convert.ToString(dtovertime.Rows[0]["overtimeruleno"]) == "3")
                            {
                                if (Convert.ToString(dtovertime.Rows[0]["OvertimepaymentRate"]) != "")
                                {
                                    acrate = Math.Round(acrate * Convert.ToDecimal(dtovertime.Rows[0]["OvertimepaymentRate"]) / 100, 2);
                                }
                            }

                            dtadd1["Rate"] = acrate;
                            dtadd1["perunitname"] = "Hour";
                            dtadd1["OverTime"] = "Over";
                            dtadd1["totalunit"] = Math.Round(totovertimehoue, 2);
                            dtadd1["totalunitpay"] = Math.Round(totovertimehoue, 2);
                            totovertimehoue = Math.Round(totovertimehoue, 2);
                            dtadd1["totalamt"] = Math.Round(((totovertimehoue) * (acrate)), 2);
                            payamt += Math.Round(((totovertimehoue) * (acrate)), 2);
                            dtTempover.Rows.Add(dtadd1);
                            ViewState["TempDataTable"] = dtTempover;
                            grdcal.DataSource = dtTempover;
                            grdcal.DataBind();
                        }
                        grddaily.DataSource = dtTemp;
                        grddaily.DataBind();
                        if (grddaily.Rows.Count > 0)
                        {
                            pnlhourly.Visible = true;
                        }
                        else
                        {
                            pnlhourly.Visible = false;
                        }

                        txttotincome.Text = payamt.ToString();
                        lbltotremunration.Text = payamt.ToString();
                        if (grddaily.Rows.Count <= 0)
                        {
                            ViewState["Tempdaily"] = null;

                            pnlcau.Visible = false;
                            pnldaily.Visible = false;
                        }
                        else
                        {
                            btndaily.Visible = true;
                            pnldaily.Visible = true;
                            pnlcau.Visible = true;
                        }
                    }

                }
            }
        }
    }
    protected void fillmonth(DataTable dt111, DataTable dtovertime)
    {
        if (grdmonth.Rows.Count <= 0)
        {
            ViewState["Tempmonth"] = null;

            pnlcau.Visible = false;
            pnlmonth.Visible = false;
        }
        else
        {
            btnmonth.Visible = true;
            pnlcau.Visible = true;
            pnlmonth.Visible = true;
        }

        DataTable dtTemp = new DataTable();
        if (ViewState["Tempmonth"] == null)
        {
            dtTemp = Monthly();

        }
        else
        {
            dtTemp = (DataTable)ViewState["Tempmonth"];
        }
        DataRow dtadd = dtTemp.NewRow();
        if (ViewState["Tempmonth"] != null)
        {
            dtadd["Id"] = "0";
            dtadd["RemunarationName"] = "";
            dtadd["Rate"] = "0";
            dtadd["perunitname"] = dtTemp.Rows[0][3].ToString();
            dtadd["completemonth"] = "0";
            dtadd["completedmonthamt"] = "0";
            dtadd["totalunit"] = "0";
            dtadd["totalunitpay"] = "0";
            dtadd["totalamt"] = "0";


            dtTemp.Rows.Add(dtadd);

            ViewState["Tempmonth"] = dtTemp;
            grdmonth.DataSource = dtTemp;
            grdmonth.DataBind();
        }
        else
        {
            foreach (DataRow dts in dt111.Rows)
            {
                if (Convert.ToString(dts["perunitname"]) == "Month" || Convert.ToString(dts["perunitname"]) == "Semi Month" || Convert.ToString(dts["perunitname"]) == "Week" || Convert.ToString(dts["perunitname"]) == "Bi-Week")
                {
                    //decimal totpayhour = 0;
                    decimal totovertimehoue = 0;
                    decimal payamt = 0;
                    decimal totbatho = 0;

                    dtadd["Id"] = Convert.ToString(dts["Id"]);
                    dtadd["RemunarationName"] = Convert.ToString(dts["RemunarationName"]);
                    dtadd["Rate"] = Convert.ToString(dts["Rate"]);
                    dtadd["perunitname"] = Convert.ToString(dts["perunitname"]);
                    dtadd["totalunit"] = "0";
                    dtadd["totalunitpay"] = "0";
                    dtadd["totalamt"] = "0";
                    dtadd["completemonth"] = "0";
                    dtadd["completedmonthamt"] = "0";

                    dtTemp.Rows.Add(dtadd);

                    ViewState["Tempmonth"] = dtTemp;
                    DataTable dtspay111 = (DataTable)select("Select Count(Distinct DateMasterID) from DateMasterTbl inner join  BatchWorkingDays on BatchWorkingDays.DateMasterID= DateMasterTbl.DateId inner join BatchTiming on BatchTiming.BatchMasterId=BatchWorkingDays.BatchID inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchTiming.BatchMasterId where EmployeeBatchMaster.Employeeid='" + ddlemp.SelectedValue + "' and Cast(Date as Date) Between '" + sdate + "' and '" + edate + "'");
                    if (dtspay111.Rows[0][0] != "")
                    {
                        totbatho = Convert.ToDecimal(dtspay111.Rows[0][0]);

                        //decimal bhour = 0;
                        //string[] separbm = new string[] { ":" };
                        //string[] strSplitArrbm = dtsb["Totalhours"].ToString().Split(separbm, StringSplitOptions.RemoveEmptyEntries);
                        //bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));

                        //totbatho += bhour;
                        //}
                    }
                    decimal bhour = 0;
                    DataTable dtspay = (DataTable)select("Select  EmployeeId,Date, Payabledays, TotalHourWork as totalpay, cast(left(TotalHourWork,2) as Decimal) as payhour,cast(Right(TotalHourWork,2) as Decimal) as payminites,BatchRequiredhours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + sdate + "' and '" + edate + "' and TotalHourWork IS not Null and OutTimeforcalculation<>'00:00' and Varify='1'  order by totalpay Desc");
                    decimal totalday = 0;
                    string dateco = "";
                    // string emd="";
                    foreach (DataRow dtsb in dtspay.Rows)
                    {
                        if (Convert.ToString(dtsb["Payabledays"]) != "0" && Convert.ToString(dtsb["BatchRequiredhours"]) != "")
                        {
                            if (Convert.ToString(dtsb["Date"]) != dateco)
                            {
                                totalday += (Convert.ToDecimal(dtsb["Payabledays"]));
                                dateco = Convert.ToString(dtsb["Date"]);
                            }
                            // decimal punit = 0;
                            string[] separbm = new string[] { ":" };
                            string[] strSplitArrbm = dtsb["BatchRequiredhours"].ToString().Split(separbm, StringSplitOptions.RemoveEmptyEntries);
                            bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));

                            // emd=Convert.ToString(dtsb["EmployeeId"]);


                        }
                    }
                    DataTable alter = (DataTable)(ViewState["Tempmonth"]);
                    if (alter.Rows.Count > 0)
                    {
                        foreach (DataRow ds in alter.Rows)
                        {

                            decimal payunit = 0;
                            if (Convert.ToString(dts["perunitname"]) == "Month")
                            {
                                payunit = paymonth;
                                grdmonth.Columns[2].HeaderText = "Per Month";
                                grdmonth.Columns[3].HeaderText = "Total Completed Month";
                                grdmonth.Columns[4].HeaderText = "Maximum Salary Payable for completed month";
                                grdmonth.Columns[5].HeaderText = "Total Working Day in Completed Month";
                                grdmonth.Columns[6].HeaderText = "Actual Day Work in Completed Month";
                                Label2.Text = "Monthly Salary";
                            }
                            else if (Convert.ToString(dts["perunitname"]) == "Semi Month")
                            {
                                payunit = Math.Round((paymonth / 2), 2);

                                grdmonth.Columns[2].HeaderText = "Per Semi Month";
                                grdmonth.Columns[3].HeaderText = "Total Completed Semi Month";
                                grdmonth.Columns[4].HeaderText = "Maximum Salary Payable for completed Semi month";
                                grdmonth.Columns[5].HeaderText = "Total Working Day in Completed Semi Month";
                                grdmonth.Columns[6].HeaderText = "Actual Day Work in Completed Semi Month";
                                Label2.Text = "Semi Monthly Salary";
                            }
                            else if (Convert.ToString(dts["perunitname"]) == "Week")
                            {
                                payunit = Math.Round((payweek), 2);

                                grdmonth.Columns[2].HeaderText = "Per Week";
                                grdmonth.Columns[3].HeaderText = "Total Completed Week";
                                grdmonth.Columns[4].HeaderText = "Maximum Salary Payable for completed Week";
                                grdmonth.Columns[5].HeaderText = "Total Working Day in Completed Week";
                                grdmonth.Columns[6].HeaderText = "Actual Day Work in Completed Week";
                                Label2.Text = "Weekly Salary";
                            }
                            else if (Convert.ToString(dts["perunitname"]) == "Bi-Week")
                            {
                                payunit = Math.Round((payweek / 2), 2);

                                grdmonth.Columns[2].HeaderText = "Per Semi Bi-Week";
                                grdmonth.Columns[3].HeaderText = "Total Completed Bi-Week";
                                grdmonth.Columns[4].HeaderText = "Maximum Salary Payable for completed Bi-Week";
                                grdmonth.Columns[5].HeaderText = "Total Working Day in Completed Bi-Week";
                                grdmonth.Columns[6].HeaderText = "Actual Day Work in Completed Bi-Week";
                                Label2.Text = "Bi-Weekly Salary";
                            }

                            dtadd["completemonth"] = payunit;
                            dtadd["completedmonthamt"] = Math.Round(((payunit) * (Convert.ToDecimal(ds["Rate"]))), 2);
                            ds["totalunit"] = Math.Round(totbatho, 2);
                            ds["totalunitpay"] = Math.Round(totalday, 2);
                            decimal oriamt = Math.Round((Convert.ToDecimal(dtadd["completedmonthamt"]) * totalday / totbatho), 2);
                            ds["totalamt"] = oriamt;
                            payamt += oriamt;
                        }
                        totovertimehoue = totalover(dtovertime);
                        if (totovertimehoue > 0)
                        {
                            if (grdcal.Rows.Count <= 0)
                            {
                                ViewState["TempDataTable"] = null;

                                pnlcau.Visible = false;
                            }
                            else
                            {

                                pnlcau.Visible = true;
                            }

                            DataTable dtTempover = new DataTable();
                            if (ViewState["TempDataTable"] == null)
                            {
                                dtTempover = HourlyRemu();

                            }
                            else
                            {
                                dtTempover = (DataTable)ViewState["TempDataTable"];
                            }

                            DataRow dtadd1 = dtTempover.NewRow();

                            dtadd1["Id"] = "0";
                            dtadd1["RemunarationName"] = "Salary OverTime";
                            decimal payovertimerate = Math.Round(Convert.ToDecimal(alter.Rows[0]["completedmonthamt"]) / (Convert.ToDecimal(alter.Rows[0]["totalunit"]) * Convert.ToDecimal(bhour)), 2);
                            if (Convert.ToString(dtovertime.Rows[0]["overtimeruleno"]) == "3")
                            {
                                if (Convert.ToString(dtovertime.Rows[0]["OvertimepaymentRate"]) != "")
                                {
                                    payovertimerate = Math.Round(payovertimerate * Convert.ToDecimal(dtovertime.Rows[0]["OvertimepaymentRate"]) / 100, 2);
                                }
                            }


                            decimal acrate = Math.Round(payovertimerate, 2);
                            dtadd1["Rate"] = acrate;
                            totovertimehoue = Math.Round(totovertimehoue, 2);
                            dtadd1["perunitname"] = "Hour";
                            dtadd1["OverTime"] = "Over";
                            dtadd1["totalunit"] = Math.Round(totovertimehoue, 2);
                            dtadd1["totalunitpay"] = Math.Round(totovertimehoue, 2);
                            dtadd1["totalamt"] = Math.Round(((totovertimehoue) * (acrate)), 2);

                            payamt += Math.Round(((totovertimehoue) * (acrate)), 2);
                            dtTempover.Rows.Add(dtadd1);
                            ViewState["TempDataTable"] = dtTempover;
                            grdcal.DataSource = dtTempover;
                            grdcal.DataBind();
                        }
                        grdmonth.DataSource = dtTemp;
                        grdmonth.DataBind();
                        if (grdcal.Rows.Count > 0)
                        {
                            pnlhourly.Visible = true;
                        }
                        else
                        {
                            pnlhourly.Visible = false;
                        }

                        txttotincome.Text = payamt.ToString();
                        lbltotremunration.Text = payamt.ToString();
                        if (grdmonth.Rows.Count <= 0)
                        {
                            ViewState["Tempmonth"] = null;

                            pnlcau.Visible = false;
                            pnlmonth.Visible = false;
                        }
                        else
                        {
                            btnmonth.Visible = true;
                            pnlmonth.Visible = true;
                            pnlcau.Visible = true;
                        }
                    }

                    //  }
                }
            }
        }
    }
    protected decimal totalover(DataTable dtovertime)
    {
        decimal totovertimehoue = 0;

        if (dtovertime.Rows.Count > 0)
        {
            if (Convert.ToString(dtovertime.Rows[0]["overtimeruleno"]) == "2" || Convert.ToString(dtovertime.Rows[0]["overtimeruleno"]) == "3")
            {
                string app = "";
                if (Convert.ToString(dtovertime.Rows[0]["overtomeapproval"]) != "")
                {
                    if (Convert.ToBoolean(dtovertime.Rows[0]["overtomeapproval"]) == true)
                    {
                        app = " and Overtimeapprove='1'";
                    }
                }
                DataTable dt;

                dt = (DataTable)select("Select distinct AttendanceId,Overtime  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + sdate + "' and '" + edate + "' and Overtime IS NOT NULL " + app);
                foreach (DataRow dr in dt.Rows)
                {

                    string[] separbm = new string[] { ":" };
                    string[] strSplitArrbm = dr["Overtime"].ToString().Split(separbm, StringSplitOptions.RemoveEmptyEntries);
                    totovertimehoue += ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));

                }


            }
            else
            {
                totovertimehoue = 0;
            }
        }
        return totovertimehoue;
    }
    public DataTable Issale()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd = new DataColumn();
        prd.ColumnName = "Remuname";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "per";
        ssCatId.DataType = System.Type.GetType("System.String");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);

        //DataColumn EffectiveStartDate = new DataColumn();
        //EffectiveStartDate.ColumnName = "per";
        //EffectiveStartDate.DataType = System.Type.GetType("System.String");
        //EffectiveStartDate.AllowDBNull = true;
        //dtTemp.Columns.Add(EffectiveStartDate);

        DataColumn EffectiveEndDate = new DataColumn();
        EffectiveEndDate.ColumnName = "perof";
        EffectiveEndDate.DataType = System.Type.GetType("System.String");
        EffectiveEndDate.AllowDBNull = true;
        dtTemp.Columns.Add(EffectiveEndDate);

        DataColumn Effecttotal = new DataColumn();
        Effecttotal.ColumnName = "baseamt";
        Effecttotal.DataType = System.Type.GetType("System.String");
        Effecttotal.AllowDBNull = true;
        dtTemp.Columns.Add(Effecttotal);

        DataColumn Effecttotalt = new DataColumn();
        Effecttotalt.ColumnName = "Totamt";
        Effecttotalt.DataType = System.Type.GetType("System.String");
        Effecttotal.AllowDBNull = true;
        dtTemp.Columns.Add(Effecttotalt);
        return dtTemp;
    }
    public DataTable Isperc()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd = new DataColumn();
        prd.ColumnName = "Remuname";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "per";
        ssCatId.DataType = System.Type.GetType("System.String");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);

        //DataColumn EffectiveStartDate = new DataColumn();
        //EffectiveStartDate.ColumnName = "per";
        //EffectiveStartDate.DataType = System.Type.GetType("System.String");
        //EffectiveStartDate.AllowDBNull = true;
        //dtTemp.Columns.Add(EffectiveStartDate);

        DataColumn EffectiveEndDate = new DataColumn();
        EffectiveEndDate.ColumnName = "perof";
        EffectiveEndDate.DataType = System.Type.GetType("System.String");
        EffectiveEndDate.AllowDBNull = true;
        dtTemp.Columns.Add(EffectiveEndDate);

        DataColumn Effecttotal = new DataColumn();
        Effecttotal.ColumnName = "baseamt";
        Effecttotal.DataType = System.Type.GetType("System.String");
        Effecttotal.AllowDBNull = true;
        dtTemp.Columns.Add(Effecttotal);

        DataColumn Effecttotalt = new DataColumn();
        Effecttotalt.ColumnName = "Totamt";
        Effecttotalt.DataType = System.Type.GetType("System.String");
        Effecttotal.AllowDBNull = true;
        dtTemp.Columns.Add(Effecttotalt);
        return dtTemp;
    }
    public DataTable Isencash()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd = new DataColumn();
        prd.ColumnName = "LeaveType";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "perleaveno";
        ssCatId.DataType = System.Type.GetType("System.String");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);



        DataColumn Effecttotal = new DataColumn();
        Effecttotal.ColumnName = "txtleaverate";
        Effecttotal.DataType = System.Type.GetType("System.String");
        Effecttotal.AllowDBNull = true;
        dtTemp.Columns.Add(Effecttotal);

        DataColumn Effecttotalt = new DataColumn();
        Effecttotalt.ColumnName = "Totamt";
        Effecttotalt.DataType = System.Type.GetType("System.String");
        Effecttotal.AllowDBNull = true;
        dtTemp.Columns.Add(Effecttotalt);
        return dtTemp;
    }
    public DataTable CreateDatatable()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd = new DataColumn();
        prd.ColumnName = "DeductionName";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "Amount";
        ssCatId.DataType = System.Type.GetType("System.String");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);

        DataColumn EffectiveStartDate = new DataColumn();
        EffectiveStartDate.ColumnName = "per";
        EffectiveStartDate.DataType = System.Type.GetType("System.String");
        EffectiveStartDate.AllowDBNull = true;
        dtTemp.Columns.Add(EffectiveStartDate);

        DataColumn EffectiveEndDate = new DataColumn();
        EffectiveEndDate.ColumnName = "perof";
        EffectiveEndDate.DataType = System.Type.GetType("System.String");
        EffectiveEndDate.AllowDBNull = true;
        dtTemp.Columns.Add(EffectiveEndDate);

        DataColumn Effecttotal = new DataColumn();
        Effecttotal.ColumnName = "Totamt";
        Effecttotal.DataType = System.Type.GetType("System.String");
        Effecttotal.AllowDBNull = true;
        dtTemp.Columns.Add(Effecttotal);

        DataColumn RRSp = new DataColumn();
        RRSp.ColumnName = "RUDed";
        RRSp.DataType = System.Type.GetType("System.String");
        RRSp.AllowDBNull = true;
        dtTemp.Columns.Add(RRSp);
        return dtTemp;
    }

    public DataTable Govttax()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd = new DataColumn();
        prd.ColumnName = "DeductionName";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "Amount";
        ssCatId.DataType = System.Type.GetType("System.String");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);

        DataColumn EffectiveStartDate = new DataColumn();
        EffectiveStartDate.ColumnName = "CrAccId";
        EffectiveStartDate.DataType = System.Type.GetType("System.String");
        EffectiveStartDate.AllowDBNull = true;
        dtTemp.Columns.Add(EffectiveStartDate);

        DataColumn EffectiveEndDate = new DataColumn();
        EffectiveEndDate.ColumnName = "DrAccId";
        EffectiveEndDate.DataType = System.Type.GetType("System.String");
        EffectiveEndDate.AllowDBNull = true;
        dtTemp.Columns.Add(EffectiveEndDate);

        DataColumn Effecttotal = new DataColumn();
        Effecttotal.ColumnName = "PayrolltaxMasterId";
        Effecttotal.DataType = System.Type.GetType("System.String");
        Effecttotal.AllowDBNull = true;
        dtTemp.Columns.Add(Effecttotal);

        return dtTemp;
    }
    protected void fillperc(DataTable dt, int step)
    {
        decimal totdid = 0;
        DataTable dtTemp = new DataTable();
        if (ViewState["Isperc"] == null)
        {
            dtTemp = Isperc();
        }

        else
        {
            dtTemp = (DataTable)ViewState["Isperc"];
        }




        foreach (DataRow dts in dt.Rows)
        {
            if (Convert.ToString(dts["IsPercent_IsAmount"]) == "1")
            {
                DataRow dtadd = dtTemp.NewRow();
                dtadd["Id"] = Convert.ToString(dts["ID"]);

                dtadd["Remuname"] = Convert.ToString(dts["RemunarationName"]); ;

                dtadd["per"] = Convert.ToString(dts["Perchantage"]);
                dtadd["perof"] = Convert.ToString(dts["remperunit"]);

                if (Convert.ToString(dts["Perchantage"]) != "")
                {



                    if (grdcal.Rows.Count > 0)
                    {

                        foreach (GridViewRow grd in grdcal.Rows)
                        {

                            TextBox txttotal = (TextBox)grd.FindControl("txttotal");
                            Label lblover = (Label)grd.FindControl("lblover");
                            if (lblover.Text != "Over")
                            {

                                dtadd["BaseAmt"] = Math.Round(Convert.ToDecimal(txttotal.Text), 2);
                                dtadd["Totamt"] = Math.Round(((Convert.ToDecimal(txttotal.Text) * Convert.ToDecimal(dtadd["per"])) / 100), 2);
                            }
                        }
                    }
                    if (grddaily.Rows.Count > 0)
                    {

                        foreach (GridViewRow grd in grddaily.Rows)
                        {

                            TextBox txttotal = (TextBox)grd.FindControl("txttotal");
                            dtadd["BaseAmt"] = Math.Round(Convert.ToDecimal(txttotal.Text), 2);
                            dtadd["Totamt"] = Math.Round(((Convert.ToDecimal(txttotal.Text) * Convert.ToDecimal(dtadd["per"])) / 100), 2);

                        }
                    }
                    if (grdmonth.Rows.Count > 0)
                    {

                        foreach (GridViewRow grd in grdmonth.Rows)
                        {

                            TextBox txttotal = (TextBox)grd.FindControl("txttotal");
                            dtadd["BaseAmt"] = Math.Round(Convert.ToDecimal(txttotal.Text), 2);
                            dtadd["Totamt"] = Math.Round(((Convert.ToDecimal(txttotal.Text) * Convert.ToDecimal(dtadd["per"])) / 100), 2);

                        }
                    }
                }
                else
                {

                }
                txttotincome.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txttotincome.Text) + (Convert.ToDecimal(dtadd["Totamt"])), 2));
                lbltotremperc.Text = Convert.ToString(Math.Round(Convert.ToDecimal(lbltotremperc.Text) + (Convert.ToDecimal(dtadd["Totamt"])), 2));
            
                dtTemp.Rows.Add(dtadd);

                ViewState["Isperc"] = dtTemp;
                grdispercentage.DataSource = dtTemp;
                grdispercentage.DataBind();
                txttotded.Text = Math.Round(totdid, 2).ToString();
                txtnettotal.Text = (Convert.ToDecimal(txttotincome.Text) - Convert.ToDecimal(txttotded.Text)).ToString();
                if (grdispercentage.Rows.Count <= 0)
                {
                    ViewState["Isperc"] = null;

                    pnlcau.Visible = false;
                    pnlispercentage.Visible = false;
                }
                else
                {

                    pnlcau.Visible = true;
                    pnlispercentage.Visible = true;
                }
            }
        }
    }
    protected void fillsales(DataTable dt, int step)
    {

        DataTable dtTemp = new DataTable();
        if (ViewState["Issales"] == null)
        {
            dtTemp = Issale();
        }

        else
        {
            dtTemp = (DataTable)ViewState["Issales"];
        }




        foreach (DataRow dts in dt.Rows)
        {
            if (Convert.ToString(dts["IsPercent_IsAmount"]) == "2")
            {
                DataRow dtadd = dtTemp.NewRow();
                dtadd["Id"] = Convert.ToString(dts["ID"]);

                dtadd["Remuname"] = Convert.ToString(dts["RemunarationName"]); ;

                dtadd["per"] = Convert.ToString(dts["SalesPercentage"]);
                DataTable dtdr = new DataTable();
                DataTable dtcr = new DataTable();
                if (Convert.ToString(dts["PercentageOfSalesId"]) == "0")
                {
                    dtadd["perof"] = "Sales of Employee";
                }
                else if (Convert.ToString(dts["PercentageOfSalesId"]) == "1")
                {
                    dtadd["perof"] = "Sales of Employee & Subordinates";
                }
                else if (Convert.ToString(dts["PercentageOfSalesId"]) == "2")
                {
                    dtadd["perof"] = "Sales of Business";
                }
                if (Convert.ToString(dts["PayableOfSales"]) == "1")
                {

                    string datebett = " and TranctionMaster.Date Between '" + sdate + "' and '" + edate + "'";

                    if (Convert.ToString(dts["PercentageOfSalesId"]) == "0")
                    {


                        dtdr = select("Select Sum(distinct AmountDebit) from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_Master " +
                         "  on User_Master.PartyId=Party_Master.PartyId inner join TranctionMaster on TranctionMaster.UserId=User_Master.UserId inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id" +
                        " where TranctionMaster.EntryTypeId='30' and  AccountDebit='5000' and Tranction_Details.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.EmployeeMasterId='" + ddlemp.SelectedValue + "'" + datebett);

                        dtcr = select("Select Sum(distinct AmountCredit) from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_Master " +
                        " on User_Master.PartyId=Party_Master.PartyId inner join TranctionMaster on TranctionMaster.UserId=User_Master.UserId inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id" +
                        " where TranctionMaster.EntryTypeId='30' and  AccountCredit='5000' and Tranction_Details.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.EmployeeMasterId='" + ddlemp.SelectedValue + "'" + datebett);

                    }
                    else if (Convert.ToString(dts["PercentageOfSalesId"]) == "1")
                    {

                        dtdr = select("Select Sum(distinct AmountDebit) from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_Master " +
                        "  on User_Master.PartyId=Party_Master.PartyId inner join TranctionMaster on TranctionMaster.UserId=User_Master.UserId inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id" +
                       " where TranctionMaster.EntryTypeId='30' and  AccountDebit='5000' and Tranction_Details.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.EmployeeMasterId In(Select EmployeeMasterId from EmployeeMaster where SuprviserId='" + ddlemp.SelectedValue + "') and EmployeeMaster.EmployeeMasterId='" + ddlemp.SelectedValue + "'" + datebett);

                        dtcr = select("Select Sum(distinct AmountCredit) from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_Master " +
                        " on User_Master.PartyId=Party_Master.PartyId inner join TranctionMaster on TranctionMaster.UserId=User_Master.UserId inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id" +
                        " where TranctionMaster.EntryTypeId='30' and AccountCredit='5000'  and Tranction_Details.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.EmployeeMasterId In(Select EmployeeMasterId from EmployeeMaster where SuprviserId='" + ddlemp.SelectedValue + "') and EmployeeMaster.EmployeeMasterId='" + ddlemp.SelectedValue + "'");

                    }
                    else if (Convert.ToString(dts["PercentageOfSalesId"]) == "2")
                    {

                        dtdr = select("Select Sum(distinct AmountDebit) from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_Master " +
                        "  on User_Master.PartyId=Party_Master.PartyId inner join TranctionMaster on TranctionMaster.UserId=User_Master.UserId inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id" +
                       " where  AccountDebit='5000' and Tranction_Details.Whid='" + ddlwarehouse.SelectedValue + "'");

                        dtcr = select("Select Sum(distinct AmountCredit) from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_Master " +
                        " on User_Master.PartyId=Party_Master.PartyId inner join TranctionMaster on TranctionMaster.UserId=User_Master.UserId inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id" +
                        " where  AccountCredit='5000' and Tranction_Details.Whid='" + ddlwarehouse.SelectedValue + "'");


                    }

                    if (Convert.ToString(dts["SalesPercentage"]) != "")
                    {


                        decimal totaodebi = 0;
                        decimal totaocredit = 0;
                        decimal opnbal1 = 0;


                        if (Convert.ToString(dtdr.Rows[0][0]) != "")
                        {
                            totaodebi = Math.Round(Convert.ToDecimal(dtdr.Rows[0][0]), 2);
                        }

                        if (Convert.ToString(dtcr.Rows[0][0]) != "")
                        {
                            totaocredit = Math.Round(Convert.ToDecimal(dtcr.Rows[0][0]), 2);
                        }
                        opnbal1 = totaodebi - totaocredit;
                        if (opnbal1 < 0)
                        {
                            opnbal1 = opnbal1 * (-1);
                        }
                        dtadd["BaseAmt"] = opnbal1.ToString();
                        dtadd["Totamt"] = Math.Round((opnbal1 * Convert.ToDecimal(dtadd["per"])) / 100, 2);

                    }
                }
                else
                {
                    dtadd["BaseAmt"] = "0";
                    dtadd["Totamt"] = "0";
                }
                txttotincome.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txttotincome.Text) + (Convert.ToDecimal(dtadd["Totamt"])), 2));
                lbltotsales.Text = Convert.ToString(Math.Round(Convert.ToDecimal(lbltotsales.Text) + (Convert.ToDecimal(dtadd["Totamt"])), 2));
              
                dtTemp.Rows.Add(dtadd);

                ViewState["Issales"] = dtTemp;
                grdsales.DataSource = dtTemp;
                grdsales.DataBind();
                //txttotded.Text = Math.Round(totdid, 2).ToString();
                txtnettotal.Text = (Convert.ToDecimal(txttotincome.Text) - Convert.ToDecimal(txttotded.Text)).ToString();
                if (grdsales.Rows.Count <= 0)
                {
                    ViewState["Issales"] = null;

                    pnlcau.Visible = false;
                    pnlsales.Visible = false;
                }
                else
                {

                    pnlcau.Visible = true;
                    pnlsales.Visible = true;
                }
            }
        }
    }


    protected void fillencash()
    {

        DataTable dtTemp = new DataTable();
        if (ViewState["Isencash"] == null)
        {
            dtTemp = Isencash();
        }

        else
        {
            dtTemp = (DataTable)ViewState["Isencash"];
        }

        DataTable dtsts = select("select Distinct EmployeeLeaveType.Id,EmployeeLeaveTypeName  from  LeaveEarnedTblDeatail INNER JOIN EmployeeLeaveType on EmployeeLeaveType.Id=LeaveEarnedTblDeatail.LeaveType WHERE  EmployeeLeaveType.Whid='" + ddlwarehouse.SelectedValue + "' and LeaveEarnedTblDeatail.employeeid='" + ddlemp.SelectedValue + "' and (Date>= '" + Convert.ToDateTime(sdate).ToShortDateString() + "') and (Date<= '" + Convert.ToDateTime(edate).ToShortDateString() + "')");
        foreach (DataRow dts in dtsts.Rows)
        {
            DataTable dtsss = select("select   Sum(Cast(UsedLeave as int))  from  LeaveEarnedTblDeatail INNER JOIN EmployeeLeaveType on EmployeeLeaveType.Id=LeaveEarnedTblDeatail.LeaveType WHERE  EmployeeLeaveType.Whid='" + ddlwarehouse.SelectedValue + "' and LeaveEarnedTblDeatail.employeeid='" + ddlemp.SelectedValue + "' and (Date>= '" + Convert.ToDateTime(sdate).ToShortDateString() + "') and (Date<= '" + Convert.ToDateTime(edate).ToShortDateString() + "')");

            if (Convert.ToString(dtsss.Rows[0][0]) != "")
            {

                DataRow dtadd = dtTemp.NewRow();


                dtadd["LeaveType"] = Convert.ToString(dts["EmployeeLeaveTypeName"]); ;

                dtadd["perleaveno"] = Convert.ToString(dtsss.Rows[0][0]);


                if (grdcal.Rows.Count > 0)
                {

                    foreach (GridViewRow grd in grdcal.Rows)
                    {
                        int id = Convert.ToInt32(grdcal.DataKeys[grd.RowIndex].Value);
                        TextBox txtrate = (TextBox)grd.FindControl("txtrate");
                        Label lblover = (Label)grd.FindControl("lblover");

                        if (lblover.Text != "Over")
                        {
                            DataTable dtspay111 = (DataTable)select("Select distinct BatchTiming.Totalhours from BatchTiming  inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchTiming.BatchMasterId where EmployeeBatchMaster.Employeeid='" + ddlemp.SelectedValue + "'");
                            if (dtspay111.Rows.Count > 0)
                            {
                                decimal bhour = 0;
                                string[] separbm = new string[] { ":" };
                                string[] strSplitArrbm = dtspay111.Rows[0]["Totalhours"].ToString().Split(separbm, StringSplitOptions.RemoveEmptyEntries);
                                bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));
                                dtadd["txtleaverate"] = Math.Round(Convert.ToDecimal(txtrate.Text) * bhour, 2);
                                dtadd["Totamt"] = Math.Round((Convert.ToDecimal(dtadd["txtleaverate"]) * Convert.ToDecimal(dtadd["perleaveno"])), 2);
                                dtadd["Id"] = id;
                            }

                        }
                    }
                }
                if (grddaily.Rows.Count > 0)
                {

                    foreach (GridViewRow grd in grddaily.Rows)
                    {
                        TextBox txtrate = (TextBox)grd.FindControl("txtrate");
                        int id = Convert.ToInt32(grddaily.DataKeys[grd.RowIndex].Value);

                        dtadd["Id"] = id;
                        dtadd["txtleaverate"] = Math.Round(Convert.ToDecimal(txtrate.Text), 2);
                        dtadd["Totamt"] = Math.Round((Convert.ToDecimal(dtadd["txtleaverate"]) * Convert.ToDecimal(dtadd["perleaveno"])), 2);

                    }
                }
                if (grdmonth.Rows.Count > 0)
                {

                    foreach (GridViewRow grd in grdmonth.Rows)
                    {
                        int id = Convert.ToInt32(grdmonth.DataKeys[grd.RowIndex].Value);

                        dtadd["Id"] = id;
                        TextBox txtrate = (TextBox)grd.FindControl("txtrate");
                        TextBox txtcompletemonthamt = (TextBox)grd.FindControl("txtcompletemonthamt");
                        TextBox txttotunit = (TextBox)grd.FindControl("txttotunit");
                        decimal pedr = Convert.ToDecimal(txtcompletemonthamt.Text) / Convert.ToDecimal(txttotunit.Text);
                        dtadd["txtleaverate"] = Math.Round(pedr, 2);
                        dtadd["Totamt"] = Math.Round((Convert.ToDecimal(dtadd["txtleaverate"]) * Convert.ToDecimal(dtadd["perleaveno"])), 2);

                    }
                }
                ////Encash
                txttotincome.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txttotincome.Text) + (Convert.ToDecimal(dtadd["Totamt"])), 2));
                dtTemp.Rows.Add(dtadd);

                ViewState["Isencash"] = dtTemp;
                grdleaveencash.DataSource = dtTemp;
                grdleaveencash.DataBind();

                txtnettotal.Text = (Convert.ToDecimal(txttotincome.Text) - Convert.ToDecimal(txttotded.Text)).ToString();
                if (grdleaveencash.Rows.Count <= 0)
                {
                    ViewState["Isencash"] = null;

                    pnlleaveencash.Visible = false;

                }
                else
                {

                    pnlleaveencash.Visible = true;

                }
            }

        }
    }
    protected void fillded()
    {
        decimal totdid = 0;
        DataTable dtTemp = new DataTable();
        if (ViewState["Tempded"] == null)
        {
            dtTemp = CreateDatatable();
        }

        else
        {
            dtTemp = (DataTable)ViewState["Tempded"];
        }



        DataTable dt1 = new DataTable();
        dt1 = (DataTable)select("Select Distinct DeductionName, payrolldeductionotherstbl.ID,RemunerationName,FixedAmount,PercentageOf from Deductionbydefault inner join  payrolldeductionotherstbl on payrolldeductionotherstbl.Id=Deductionbydefault.DeductionId LEFT join RemunerationMaster  on RemunerationMaster.Id=payrolldeductionotherstbl.PercentageOfRemunerationtypeId  where payrolldeductionotherstbl.Status='1' and Deductionbydefault.EmployeeId='" + ddlemp.SelectedValue + "' and payrolldeductionotherstbl.Whid='" + ddlwarehouse.SelectedValue + "' and StartDate<='" + Convert.ToDateTime(sdate).ToShortDateString() + "' and EndDate>='" + Convert.ToDateTime(edate).ToShortDateString() + "'");
        //  dt1 = (DataTable)select("Select Distinct DeductionName, payrolldeductionotherstbl.ID,RemunerationName,FixedAmount,PercentageOf from Deductionbydefault inner join  payrolldeductionotherstbl on payrolldeductionotherstbl.Id=Deductionbydefault.DeductionId LEFT join RemunerationMaster  on RemunerationMaster.Id=payrolldeductionotherstbl.PercentageOfRemunerationtypeId  where payrolldeductionotherstbl.Status='1' and Deductionbydefault.EmployeeId='" + ddlemp.SelectedValue + "' and payrolldeductionotherstbl.Whid='" + ddlwarehouse.SelectedValue + "' and StartDate<='" + DateTime.Now.ToShortDateString() + "' and EndDate>='" + DateTime.Now.ToShortDateString() + "'");

        foreach (DataRow dts in dt1.Rows)
        {
            DataRow dtadd = dtTemp.NewRow();
            dtadd["Id"] = Convert.ToString(dts["ID"]);
            dtadd["RUDed"] = "0";
            dtadd["DeductionName"] = Convert.ToString(dts["DeductionName"]); ;
            if (Convert.ToString(dts["FixedAmount"]) != "")
            {
                dtadd["Amount"] = Convert.ToString(dts["FixedAmount"]);
                dtadd["Totamt"] = Math.Round(Convert.ToDecimal(dts["FixedAmount"]), 2).ToString();
            }
            else
            {
                dtadd["Amount"] = "0";
            }
            if (Convert.ToString(dts["PercentageOf"]) != "")
            {



                if (grdcal.Rows.Count > 0)
                {

                    foreach (GridViewRow grd in grdcal.Rows)
                    {
                        TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                        TextBox txttotal = (TextBox)grd.FindControl("txttotal");
                        Label lblover = (Label)grd.FindControl("lblover");
                        if (lblover.Text != "Over")
                        {
                            dtadd["per"] = Convert.ToString(dts["PercentageOf"]);
                            dtadd["perof"] = txtremu.Text;
                            dtadd["Totamt"] = Math.Round(((Convert.ToDecimal(txttotal.Text) * Convert.ToDecimal(dts["PercentageOf"])) / 100), 2);
                        }
                    }
                }
                if (grddaily.Rows.Count > 0)
                {

                    foreach (GridViewRow grd in grddaily.Rows)
                    {
                        TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                        TextBox txttotal = (TextBox)grd.FindControl("txttotal");


                        dtadd["per"] = Convert.ToString(dts["PercentageOf"]);
                        dtadd["perof"] = txtremu.Text;
                        dtadd["Totamt"] = Math.Round(((Convert.ToDecimal(txttotal.Text) * Convert.ToDecimal(dts["PercentageOf"])) / 100), 2);

                    }
                }
                if (grdmonth.Rows.Count > 0)
                {

                    foreach (GridViewRow grd in grdmonth.Rows)
                    {
                        TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                        TextBox txttotal = (TextBox)grd.FindControl("txttotal");


                        dtadd["per"] = Convert.ToString(dts["PercentageOf"]);
                        dtadd["perof"] = txtremu.Text;
                        dtadd["Totamt"] = Math.Round(((Convert.ToDecimal(txttotal.Text) * Convert.ToDecimal(dts["PercentageOf"])) / 100), 2);

                    }
                }
            }
            else
            {
                dtadd["per"] = "0";

                dtadd["perof"] = "-";
            }
            totdid += Math.Round(Convert.ToDecimal(dtadd["Totamt"]), 2);
            dtTemp.Rows.Add(dtadd);
        }
        DataTable dtflast = select("Select Max(Id) as Id from WithholdingRequestMasterTbl where WithholdingRequestMasterTbl.EmployeeID='" + ddlemp.SelectedValue + "' ");

        if (dtflast.Rows.Count > 0)
        {
            if (Convert.ToString(dtflast.Rows[0]["Id"]) != "")
            {
                DataTable dtsm = select("SELECT DISTINCT Top(1) RRSPRecurring, WithholdingRequestMasterTbl.ID,PaycycleID,RRSPCotributionREcurringAMT   FROM  WithholdingRequestMasterTbl " +
                                    " where WithholdingRequestMasterTbl.EmployeeID='" + ddlemp.SelectedValue + "' and ((RRSPRecurring='1' and (RequestDate Between '" + sdate + "' and '" + edate + "')) or (RRSPRecurring='0' and PaycycleID= '" + Convert.ToString(ViewState["paycy"]) + "' and RequestDate<= '" + edate + "')) and Id='" + Convert.ToString(dtflast.Rows[0]["Id"]) + "'  order by WithholdingRequestMasterTbl.Id Desc ");
                if (dtsm.Rows.Count > 0)
                {
                    if (Convert.ToString(dtsm.Rows[0]["RRSPCotributionREcurringAMT"]) != "")
                    {
                        DataRow dtadd = dtTemp.NewRow();
                        decimal fam = Convert.ToDecimal(dtsm.Rows[0]["RRSPCotributionREcurringAMT"]);
                        dtadd["Id"] = Convert.ToString(dtsm.Rows[0]["Id"]);
                        dtadd["RUDed"] = "1";
                        dtadd["DeductionName"] = Convert.ToString("RRSP");
                        dtadd["Totamt"] = Convert.ToString(fam);
                        dtadd["perof"] = "";
                        totdid += Math.Round(fam, 2);
                        dtTemp.Rows.Add(dtadd);
                    }
                }
                DataTable dtsU1 = select("SELECT DISTINCT Top(1) UnionDuesRecurring, WithholdingRequestMasterTbl.ID,PaycycleID,UnionDuesRecurringAMT   FROM  WithholdingRequestMasterTbl " +
               " where WithholdingRequestMasterTbl.EmployeeID='" + ddlemp.SelectedValue + "' and ((UnionDuesRecurring='1'and (RequestDate Between '" + sdate + "' and '" + edate + "')) or (UnionDuesRecurring='0' and PaycycleID= '" + Convert.ToString(ViewState["paycy"]) + "' and RequestDate<= '" + edate + "')) and Id='" + Convert.ToString(dtflast.Rows[0]["Id"]) + "'  order by WithholdingRequestMasterTbl.Id Desc ");//and Date RequestDate Between '"+sdate+"' and '"+edate+"'
                if (dtsU1.Rows.Count > 0)
                {
                    if (Convert.ToString(dtsU1.Rows[0]["UnionDuesRecurringAMT"]) != "")
                    {

                        string vals = Convert.ToString(dtsU1.Rows[0]["UnionDuesRecurringAMT"]);
                        DataRow dtadd = dtTemp.NewRow();

                        dtadd["Id"] = Convert.ToString(dtsU1.Rows[0]["Id"]);
                        dtadd["RUDed"] = "2";
                        dtadd["perof"] = "";
                        dtadd["DeductionName"] = Convert.ToString("Union Dues");
                        dtadd["Totamt"] = Convert.ToString(vals);
                        totdid += Math.Round(Convert.ToDecimal(vals), 2);
                        dtTemp.Rows.Add(dtadd);
                    }

                }
            }
        }
        ViewState["Tempded"] = dtTemp;
        grdded.DataSource = dtTemp;
        grdded.DataBind();

        btnded.Visible = true;

        if (txttotincome.Text == "")
        {
            txttotincome.Text = "0";
        }

        txttotded.Text = Math.Round(totdid, 2).ToString();
        txtnettotal.Text = (Convert.ToDecimal(txttotincome.Text) - Convert.ToDecimal(txttotded.Text)).ToString();
    }
    protected void btnaddnewrem_Click(object sender, EventArgs e)
    {
        DataTable dts = new DataTable();
        fillincome(dts, dts);
        if (grdcal.Rows.Count > 0)
        {

            foreach (GridViewRow grd in grdcal.Rows)
            {
                TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                Label txtperunitname = (Label)grd.FindControl("txtperunitname");
                DropDownList ddldailyremu = (DropDownList)grd.FindControl("ddldailyremu");
                if (txtremu.Text == "")
                {
                    DataTable dt111 = new DataTable();
                    DataTable dt11 = (DataTable)select("  Select * from EmployeePayrollMaster  where EmpId='" + ddlemp.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "'");
                    if (dt11.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt11.Rows[0]["EmployeePaidAsPerDesignation"]) != true)
                        {
                            dt111 = (DataTable)select("Select distinct RemunerationMaster.Id,EmployeeID, RemunerationMaster.RemunerationName as RemunarationName from  RemunerationMaster inner join EmployeeSalaryMaster on EmployeeSalaryMaster.Remuneration_Id=RemunerationMaster.Id Left join PeriodMaster12 on PeriodMaster12.Id=EmployeeSalaryMaster.PayablePer_PeriodMasterId  Left join  RemunerationMaster AS RM1 on EmployeeSalaryMaster.IsPercentRemunerationId=RM1.Id where EmployeeID='" + ddlemp.SelectedValue + "' and(EmployeeSalaryMaster.EffectiveEndDate>='" + sdate + "' or EmployeeSalaryMaster.EffectiveStartDate>='" + sdate + "') and (EmployeeSalaryMaster.EffectiveEndDate<='" + edate + "' or EmployeeSalaryMaster.EffectiveStartDate<='" + edate + "') order by RemunarationName");

                        }
                        else
                        {
                            dt111 = (DataTable)select("Select distinct RemunerationMaster.Id, RemunerationMaster.RemunerationName as RemunarationName from  RemunerationMaster inner join RemunerationByDesignation on RemunerationByDesignation.Remuneration_Id=RemunerationMaster.Id Left join PeriodMaster12 on PeriodMaster12.Id=RemunerationByDesignation.PayablePer_PeriodMasterId  Left join  RemunerationMaster AS RM1 on RemunerationByDesignation.IsPercentRemunerationId=RM1.Id where DesignationId=(Select DesignationMasterId from EmployeeMaster where EmployeeMasterId='" + ddlemp.SelectedValue + "')  and(RemunerationByDesignation.EffectiveEndDate>='" + sdate + "' or RemunerationByDesignation.EffectiveStartDate>='" + sdate + "') and (RemunerationByDesignation.EffectiveEndDate<='" + edate + "' or RemunerationByDesignation.EffectiveStartDate<='" + edate + "') order by RemunarationName");



                        }
                    }
                    if (dt111.Rows.Count > 0)
                    {
                        ddldailyremu.DataSource = dt111;
                        ddldailyremu.DataTextField = "RemunarationName";
                        ddldailyremu.DataValueField = "Id";
                        ddldailyremu.DataBind();
                        ddldailyremu.Visible = true;
                        txtremu.Visible = false;
                        txtremu.Text = "0";
                    }
                }
                else
                {
                    ddldailyremu.Visible = false;
                    txtremu.Visible = true;
                }
            }
        }
    }
    protected void btnded_Click(object sender, EventArgs e)
    {
        //fillded();
        DataTable dtTemp = new DataTable();
        if (ViewState["Tempded"] == null)
        {
            dtTemp = CreateDatatable();
        }

        else
        {
            dtTemp = (DataTable)ViewState["Tempded"];
        }
        DataRow dtadd = dtTemp.NewRow();

        dtadd["Id"] = "0";

        dtadd["DeductionName"] = "";
        dtadd["RUDed"] = "0";
        dtadd["Amount"] = "0";

        dtadd["per"] = "0";

        dtadd["perof"] = "0";
        dtadd["Totamt"] = "0";
        dtTemp.Rows.Add(dtadd);
        grdded.DataSource = dtTemp;
        grdded.DataBind();
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


        fillpayperiod();
        fillrelac();

        Label1.Text = "";
    }
    protected void fillrelac()
    {
        ddlrelacc.Items.Clear();
        DataTable dtliab = select("SELECT Distinct (AccountMaster.AccountName)as Classgroup,AccountMaster.AccountId  FROM  GroupCompanyMaster  inner join AccountMaster  on AccountMaster.GroupId=GroupCompanyMaster.GroupId  where AccountMaster.GroupId='1'  and  AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlwarehouse.SelectedValue + "' order by Classgroup");

        ddlrelacc.DataSource = dtliab;
        ddlrelacc.DataTextField = "Classgroup";
        ddlrelacc.DataValueField = "AccountId";
        ddlrelacc.DataBind();
    }
    protected void fillpayperiod()
    {
        ddlpayperiod.Items.Clear();
        DataTable dt = new DataTable();
        dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,Convert(nvarchar,EndDate,101)   as EndDate from [ReportPeriod] where  Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            DataTable dt1 = new DataTable();
            // dt1 = (DataTable)select("Select Distinct ID,PayperiodName +' : '+Convert(nvarchar,PayperiodStartDate,101) +' : '+ Convert(nvarchar,PayperiodEndDate,101) as period from payperiodMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodMaster.Id where Whid='"+ddlwarehouse.SelectedValue+"' and PayperiodStartDate>='" + dt.Rows[0]["StartDate"] + "' and PayperiodEndDate<='" + dt.Rows[0]["EndDate"] + "'");

            dt1 = (DataTable)select("Select Distinct  payperiodMaster.ID,PayperiodName +' : '+Convert(nvarchar,PayperiodStartDate,101) +' : '+ Convert(nvarchar,PayperiodEndDate,101) as period,PayperiodStartDate from  payperiodtype inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id where payperiodMaster.PayperiodTypeID='" + ddlperiodtype.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "' and PayperiodStartDate>='" + Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToShortDateString() + "' and PayperiodEndDate<='" + Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToShortDateString() + "'  order by PayperiodStartDate");
            //dt1 = (DataTable)select("Select Distinct Count(EmpId) as EMD, payperiodMaster.ID,PayperiodName +' : '+Convert(nvarchar,PayperiodStartDate,101) +' : '+ Convert(nvarchar,PayperiodEndDate,101) as period,PayperiodStartDate from  payperiodtype inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id where payperiodMaster.PayperiodTypeID='" + ddlperiodtype.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "' and PayperiodStartDate>='" + Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToShortDateString() + "' and PayperiodEndDate<='" + Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToShortDateString() + "' Group by payperiodMaster.ID, PayperiodName,PayperiodStartDate,PayperiodEndDate order by PayperiodStartDate");

            if (dt1.Rows.Count > 0)
            {
                grdcal.DataSource = null;
                grdcal.DataBind();
                ViewState["TempDataTable"] = null;
                txttotincome.Text = "0";
                lbltotremunration.Text = "0";
                lbltotsales.Text = "0";
                lbltotremperc.Text = "0";
                grdded.DataSource = null;
                grdded.DataBind();
                ViewState["Tempded"] = null;
                txttotded.Text = "0";
                txtnettotal.Text = "0";

                //foreach (DataRow item in dt1.Rows)
                //{

                //    DataTable Dtb=select(

                //}


                ddlpayperiod.DataSource = dt1;
                ddlpayperiod.DataTextField = "period";
                ddlpayperiod.DataValueField = "ID";
                ddlpayperiod.DataBind();

            }
        }
        ddlpayperiod.Items.Insert(0, "Select");
        ddlpayperiod.Items[0].Value = "0";
        EventArgs e = new EventArgs();
        object sender = new object();
        ddlpayperiod_SelectedIndexChanged(sender, e);
    }
    protected void Nullable()
    {
        pnlcau.Visible = false;
        grdcal.DataSource = null;
        grdcal.DataBind();
        grddaily.DataSource = null;
        grddaily.DataBind();
        grdmonth.DataSource = null;
        grdmonth.DataBind();
        grdispercentage.DataSource = null;
        grdispercentage.DataBind();
        grdsales.DataSource = null;
        grdsales.DataBind();
        ViewState["Issales"] = null;
        ViewState["TempDataTable"] = null;
        ViewState["Tempded"] = null;
        ViewState["Tempdaily"] = null;
        ViewState["Isperc"] = null;
        ViewState["Tempmonth"] = null;
        ViewState["Isencash"] = null;


        ViewState["TempGovt"] = null;
        grdgovded.DataBind();
        grdgovded.DataSource = null;

        grdleaveencash.DataBind();
        grdleaveencash.DataSource = null;
        grdded.DataSource = null;
        grdded.DataBind();
        grdtaxbenifit.DataSource = null;
        grdtaxbenifit.DataBind();
    }
    protected void ddlemp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ISFill"] = null;
        grdallemp.DataSource = null;
        grdallemp.DataBind();
        if (ddlemp.Items.Count > 0)
        {
            if (sdate != "")
            {
                fillGridhead();
            }
            for (int i = 0; i < ddlemp.Items.Count; i++)
            {
                ddlemp.SelectedIndex = i; ;

                int save = fillsaldata();
                if (save == 0)
                {
                    if (pnlcau.Visible == true)
                    {
                        pnlcau.Visible = false;

                        FillAllEmpData("", "");
                    }
                }
            }
            pnlinsertdata.Visible = false;
        }
    }
    protected void fillGridhead()
    {
        CheckBoxList1.Items[0].Text = "NGov1";
        CheckBoxList1.Items[1].Text = "NGov2";
        CheckBoxList1.Items[2].Text = "NGov3";
        CheckBoxList1.Items[3].Text = "NGov4";
        CheckBoxList1.Items[4].Text = "NGov Other";
        CheckBoxList1.Items[0].Enabled = false;
        CheckBoxList1.Items[1].Enabled = false;
        CheckBoxList1.Items[2].Enabled = false;
        CheckBoxList1.Items[3].Enabled = false;
        CheckBoxList1.Items[4].Enabled = false;


        CheckBoxList1.Items[5].Text = "Gov1";
        CheckBoxList1.Items[6].Text = "Gov2";
        CheckBoxList1.Items[7].Text = "Gov3";
        CheckBoxList1.Items[8].Text = "Gov4";
        CheckBoxList1.Items[9].Text = "Gov Other";
        CheckBoxList1.Items[5].Enabled = false;
        CheckBoxList1.Items[6].Enabled = false;
        CheckBoxList1.Items[7].Enabled = false;
        CheckBoxList1.Items[8].Enabled = false;
        CheckBoxList1.Items[9].Enabled = false;
        string emppera = "Select Distinct EmployeeMasterID from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID inner join payperiodtype on payperiodtype.Id=EmployeePayrollMaster.PayPeriodMasterId  inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID " +
             " where  EmployeePayrollMaster.Whid='" + ddlwarehouse.SelectedValue + "' and payperiodMaster.Id='" + ddlpayperiod.SelectedValue + "' and EmployeeMasterID Not in(Select EmployeeId from SalaryMaster where payperiodtypeId='" + ddlpayperiod.SelectedValue + "')";
        int RRP = 0;
        int UNDUE = 0;
        DataTable dt1 = (DataTable)select("Select Distinct Count(DeductionName) as DedName, DeductionName from Deductionbydefault inner join  payrolldeductionotherstbl on payrolldeductionotherstbl.Id=Deductionbydefault.DeductionId LEFT join RemunerationMaster  on RemunerationMaster.Id=payrolldeductionotherstbl.PercentageOfRemunerationtypeId " +
            "where payrolldeductionotherstbl.Status='1'  and payrolldeductionotherstbl.Whid='" + ddlwarehouse.SelectedValue + "' and " +
            " StartDate<='" + Convert.ToDateTime(sdate).ToShortDateString() + "' and EndDate>='" + Convert.ToDateTime(edate).ToShortDateString() + "' group by DeductionName order by DedName Desc,DeductionName Asc");

        DataTable dtsm = select("SELECT  DISTINCT Count(WithholdingRequestMasterTbl.ID) as CM   FROM  WithholdingRequestMasterTbl " +
                                  " where ((RRSPRecurring='1' and (RequestDate Between '" + sdate + "' and '" + edate + "')) or (RRSPRecurring='0' and PaycycleID= '" + Convert.ToString(ddlperiodtype.SelectedValue) + "' and RequestDate<= '" + edate + "')) and Id in(Select Max(Id) as Id from WithholdingRequestMasterTbl  where WithholdingRequestMasterTbl.BusinessID='" + ddlwarehouse.SelectedValue + "' and EmployeeID In(" + emppera + ") Group by EmployeeID) ");
        if (dtsm.Rows.Count > 0)
        {
            RRP = Convert.ToInt32(dtsm.Rows[0]["CM"]);
        }
        DataTable dtsU1 = select("SELECT DISTINCT Count(WithholdingRequestMasterTbl.ID) as CM   FROM  WithholdingRequestMasterTbl " +
              " where  ((UnionDuesRecurring='1'and (RequestDate Between '" + sdate + "' and '" + edate + "')) or (UnionDuesRecurring='0' and PaycycleID= '" + Convert.ToString(ddlperiodtype.SelectedValue) + "' and RequestDate<= '" + edate + "')) and UnionDuesRecurringAMT<>''  and Id in(Select Max(Id) as Id from WithholdingRequestMasterTbl  where WithholdingRequestMasterTbl.BusinessID='" + ddlwarehouse.SelectedValue + "' Group by EmployeeID)");//and Date RequestDate Between '"+sdate+"' and '"+edate+"'
        if (dtsU1.Rows.Count > 0)
        {
            UNDUE = Convert.ToInt32(dtsm.Rows[0]["CM"]);
        }
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            int kj = 0;
            if (Convert.ToInt32(dt1.Rows[i]["DedName"]) < RRP)
            {
                kj = 1;
                CheckBoxList1.Items[i].Enabled = true;

                CheckBoxList1.Items[i].Text = "RRSP";
                grdallemp.Columns[11 + i].HeaderText = "RRSP";

            }
            if (Convert.ToInt32(dt1.Rows[i]["DedName"]) < UNDUE)
            {
                kj = 1;
                CheckBoxList1.Items[i].Enabled = true;

                CheckBoxList1.Items[i].Text = "Union Dues";
                grdallemp.Columns[11 + i].HeaderText = "Union Dues";

            }
            if (kj == 0)
            {
                if (i < 4)
                {
                    CheckBoxList1.Items[i].Enabled = true;

                    CheckBoxList1.Items[i].Text = Convert.ToString(dt1.Rows[i]["DeductionName"]);
                    grdallemp.Columns[11 + i].HeaderText = Convert.ToString(dt1.Rows[i]["DeductionName"]);
                }
                else
                {
                    CheckBoxList1.Items[11 + i].Enabled = true;
                    break;
                }
            }
        }
        if (dt1.Rows.Count == 0)
        {
            int km = 0;
            if (RRP > 0)
            {
                CheckBoxList1.Items[km].Enabled = true;

                CheckBoxList1.Items[km].Text = "RRSP";
                grdallemp.Columns[11 + km].HeaderText = "RRSP";
                km += 1;
            }
            if (UNDUE > 0)
            {
                CheckBoxList1.Items[km].Enabled = true;

                CheckBoxList1.Items[km].Text = "Union Dues";
                grdallemp.Columns[11 + km].HeaderText = "Union Dues";

            }
        }
        DataTable dts = select("Select Distinct Top(4) PayrollGovtDeductionTbl.*,PayrolltaxMaster.Payrolltax_id, PayrolltaxMaster.Sortname from PayrollGovtDeductionTbl inner join  PayrolltaxMaster on PayrollGovtDeductionTbl.Payrolltaxmasterid=PayrolltaxMaster.Payrolltax_id inner join PayRollTaxDetail  on PayRollTaxDetail.Payrolltaxmasterid=PayrolltaxMaster.Payrolltax_id" +
            " where  PayrollGovtDeductionTbl.Whid='" + ddlwarehouse.SelectedValue + "' and(PayRollTaxDetail.EffectiveStartDate<='" + sdate + "' and PayRollTaxDetail.EffectiveEndDate>='" + edate + "') order by PayrolltaxMaster.Payrolltax_id ASC");
        for (int i = 0; i < dts.Rows.Count; i++)
        {
            if (i < 4)
            {
                CheckBoxList1.Items[5 + i].Enabled = true;

                CheckBoxList1.Items[5 + i].Text = Convert.ToString(dts.Rows[i]["Sortname"]);
                grdallemp.Columns[17 + i].HeaderText = Convert.ToString(dts.Rows[i]["Sortname"]);
            }
            else
            {
                CheckBoxList1.Items[5 + i].Enabled = true;
                break;
            }
        }
    }
    protected int fillsaldata()
    {
        int save = 0;
        Label1.Text = "";
        ViewState["paycy"] = "";
        Nullable();

        if (ddlemp.SelectedIndex != 0)
        {
            lbltotremunration.Text = "0";
            lbltotsales.Text = "0";
            lbltotremperc.Text = "0";
            txttotincome.Text = "0";
            lblmsg.Text = "";
            btnUpdate.Visible = false;
            btnEdit.Visible = false;

            pnlcau.Enabled = true;
            btnsubmit.Enabled = true;
            btnsubmit.Visible = true;
            //Overtime in allowed attandance rules..
            DataTable dtovertime = (DataTable)select("Select * from AttandanceRule where StoreId='" + ddlwarehouse.SelectedValue + "'");

            DataTable dt11 = new DataTable();
            DataTable dt111 = new DataTable();
            string[] separator1 = new string[] { " : " };
            string[] strSplitArr1 = ddlpayperiod.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
            sdate = strSplitArr1[1].ToString();
            edate = strSplitArr1[2].ToString();
            int step = 0;
            dt11 = (DataTable)select("Select EmployeePayrollMaster.*,payperiodtype.NoOfPayperiodsinayear from EmployeePayrollMaster inner join payperiodtype on payperiodtype.Id=EmployeePayrollMaster.PayPeriodMasterId  where EmployeePayrollMaster.EmpId='" + ddlemp.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "'");
            if (dt11.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt11.Rows[0]["EmployeePaidAsPerDesignation"]) != true)
                {
                    step = 0;
                    dt111 = (DataTable)select("Select distinct PercentageOfSalesId,SalesPercentage,PayableOfSales, IsPercent_IsAmount, convert(nvarchar,EffectiveStartDate,101) as EffectiveStartDate,convert(nvarchar,EffectiveEndDate,101) as EffectiveEndDate, RemunerationMaster.Id,EmployeeID, RemunerationMaster.RemunerationName as RemunarationName,CASE WHEN (RM1.RemunerationName IS NULL)THEN '-' ELSE RM1.RemunerationName END AS  remperunit,CASE WHEN (Amount IS NULL)THEN '0' ELSE Amount END AS  Rate,case when (Period_name IS NULL) then '--' Else Period_name End as perunitname,CASE WHEN (Percentage IS NULL)Then '0'Else Percentage End as Perchantage,CASE WHEN (IsPercentRemunerationId IS NULL) THEN '0' ELSE IsPercentRemunerationId END AS perunitrate from  RemunerationMaster inner join EmployeeSalaryMaster on EmployeeSalaryMaster.Remuneration_Id=RemunerationMaster.Id Left join PeriodMaster12 on PeriodMaster12.Id=EmployeeSalaryMaster.PayablePer_PeriodMasterId  Left join  RemunerationMaster AS RM1 on EmployeeSalaryMaster.IsPercentRemunerationId=RM1.Id where EmployeeID='" + ddlemp.SelectedValue + "' and(EmployeeSalaryMaster.EffectiveEndDate>='" + sdate + "' or EmployeeSalaryMaster.EffectiveStartDate>='" + sdate + "') and (EmployeeSalaryMaster.EffectiveEndDate<='" + edate + "' or EmployeeSalaryMaster.EffectiveStartDate<='" + edate + "') order by EmployeeID, perunitname Desc");

                }
                else
                {
                    dt111 = (DataTable)select("Select distinct PercentageOfSalesId,SalesPercentage,PayableOfSales, IsPercent_IsAmount, convert(nvarchar,EffectiveStartDate,101) as EffectiveStartDate,convert(nvarchar,EffectiveEndDate,101) as EffectiveEndDate, RemunerationMaster.Id, RemunerationMaster.RemunerationName as RemunarationName,CASE WHEN (RM1.RemunerationName IS NULL)THEN '-' ELSE RM1.RemunerationName END AS  remperunit,CASE WHEN (Amount IS NULL)THEN '0' ELSE Amount END AS  Rate,case when (Period_name IS NULL) then '--' Else Period_name End as perunitname,CASE WHEN (Percentage IS NULL)Then '0'Else Percentage End as Perchantage,CASE WHEN (IsPercentRemunerationId IS NULL) THEN '0' ELSE IsPercentRemunerationId END AS perunitrate from  RemunerationMaster inner join RemunerationByDesignation on RemunerationByDesignation.Remuneration_Id=RemunerationMaster.Id Left join PeriodMaster12 on PeriodMaster12.Id=RemunerationByDesignation.PayablePer_PeriodMasterId  Left join  RemunerationMaster AS RM1 on RemunerationByDesignation.IsPercentRemunerationId=RM1.Id where DesignationId=(Select DesignationMasterId from EmployeeMaster where EmployeeMasterId='" + ddlemp.SelectedValue + "')  and(RemunerationByDesignation.EffectiveEndDate>='" + sdate + "' or RemunerationByDesignation.EffectiveStartDate>='" + sdate + "') and (RemunerationByDesignation.EffectiveEndDate<='" + edate + "' or RemunerationByDesignation.EffectiveStartDate<='" + edate + "') order by perunitname Desc");
                    step = 1;


                }
                if (Convert.ToString(dt11.Rows[0]["NoOfPayperiodsinayear"]) != "")
                {
                    ViewState["paycy"] = Convert.ToString(dt11.Rows[0]["PayPeriodMasterId"]); ;
                    P = Convert.ToString(dt11.Rows[0]["NoOfPayperiodsinayear"]);
                }
            }
            if (dt111.Rows.Count > 0)
            {
                foreach (DataRow dtc in dt111.Rows)
                {
                    if (Convert.ToString(dtc["IsPercent_IsAmount"]) == "0")
                    {
                        if (Convert.ToString(dtc["perunitname"]) == "Hour")
                        {
                            fillincome(dt111, dtovertime);
                        }
                        if (Convert.ToString(dtc["perunitname"]) == "Day")
                        {
                            filldaily(dt111, dtovertime);
                        }
                        if (Convert.ToString(dtc["perunitname"]) == "Month" || Convert.ToString(dtc["perunitname"]) == "Semi Month" || Convert.ToString(dtc["perunitname"]) == "Week" || Convert.ToString(dtc["perunitname"]) == "Bi-Week")
                        {
                            if (Convert.ToString(dtc["perunitname"]) == "Week" || Convert.ToString(dtc["perunitname"]) == "Bi-Week")
                            {
                                payweek = weekcount();
                            }
                            else
                            {
                                paymonth = moofmonth();
                            }
                            fillmonth(dt111, dtovertime);
                        }
                    }
                    else if (Convert.ToString(dtc["IsPercent_IsAmount"]) == "1")
                    {
                        fillperc(dt111, step);
                    }
                    else if (Convert.ToString(dtc["IsPercent_IsAmount"]) == "2")
                    {
                        fillsales(dt111, step);
                    }

                }
                fillencash();
                filltaxablebenifit();
                //I = txtbenifitgrossamt.Text;
                fillded();
                filltax();
            }

        }
        else
        {
            grdcal.DataSource = null;
            grdcal.DataBind();
            ViewState["TempDataTable"] = null;
            txttotincome.Text = "0";
            lbltotremunration.Text = "0";
            lbltotsales.Text = "0";
            lbltotremperc.Text = "0";
            grdded.DataSource = null;
            grdded.DataBind();
            ViewState["Tempded"] = null;

            grdgovded.DataSource = null;
            grdgovded.DataBind();
            ViewState["TempGovt"] = null;
            txtgovtottax.Text = "0";
            txttotded.Text = "0";
            txtnettotal.Text = "0";
            //btnaddnewrem.Visible = false;
            //btncalculate.Visible = false;
            //btnsubmit.Visible = false;
            pnlcau.Visible = false;

        }

        return save;
    }
    protected void filltaxablebenifit()
    {

        txtbenifitgrossamt.Text = txttotincome.Text;

        DataTable dts = select("SELECT dISTINCT TaxablebenifitforemployeeTbl.ID,A1.Taxablebenifitname,Convert(Nvarchar,TaxablebenifitforemployeeTbl.Date,101) as Date,Amount   FROM  TaxablebenifitforemployeeTbl " +
   " inner join TaxablebenifitMasterTbl as A1 on A1.Id=TaxablebenifitforemployeeTbl.TaxablebanifilId " +
   " where TaxablebenifitforemployeeTbl.EmployeeId='" + ddlemp.SelectedValue + "' and (RecrringType='1' or TaxablebenifitforemployeeTbl.Date between '" + sdate + "' and '" + edate + "' )  ORDER BY A1.Taxablebenifitname dESC");
        //DataTable dts = select("Select Distinct PayRollTaxDetail.Id, PayRollTaxDetail.TaxDetailName from   PayrolltaxMaster inner join PayRollTaxDetail  on PayRollTaxDetail.Payrolltaxmasterid=PayrolltaxMaster.Payrolltax_id" +
        //" inner join PayrollTaxDetailDetail on PayrollTaxDetailDetail.Payrolltaxdetail_id=PayRollTaxDetail.Id inner join TaxExemptionMaster on TaxExemptionMaster.Id=PayrollTaxDetailDetail.TaxCodeExemption_Id where PayrolltaxMaster.[State_id]='" + dtstate.Rows[0]["State"] + "'");


        if (dts.Rows.Count > 0)
        {
            grdtaxbenifit.DataSource = dts;
            grdtaxbenifit.DataBind();
            foreach (DataRow dtr in dts.Rows)
            {
                txtbenifitgrossamt.Text = (Convert.ToDecimal(txtbenifitgrossamt.Text) + Convert.ToDecimal(dtr["Amount"])).ToString();

            }
        }




    }
    protected decimal filltaxvar(string TaxdedId)
    {

        CPP = "0"; EI = "0"; PI = "0"; F = "0"; F1 = "0"; F2 = "0"; U1 = "0"; HD = "0"; L = "0"; T = "0"; A = "0"; A1 = "0"; B = "0"; B1 = "0"; C = "0"; D = "0"; D1 = "0"; E = "0"; E1 = "0"; F3 = "0"; F4 = "0"; I1 = "0"; IE = "0"; K = "0"; KP = "0"; K1 = "0"; K1P = "0"; K2P = "0"; K2 = "0"; K2Q = "0"; K3 = "0"; K3P = "0"; K4 = "0"; K4P = "0";

        decimal finalamt = 0; ;

        Temp1 = "";
        Temp1val = "";
        if (yearId != "" && countryId != "" && Stateid != "")
        {
            string strdelePurD = " delete from EmployeePayrollVariableValuesTbl where VariableCodeID in(Select Id from PayrollTaxFormula where  PayrollMasterId='" + TaxdedId + "') and PayperiodId='" + ddlpayperiod.SelectedValue + "' and Employeeid = '" + ddlemp.SelectedValue + "' ";
            SqlCommand cmddeltrans = new SqlCommand(strdelePurD, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddeltrans.ExecuteNonQuery();
            con.Close();


            DataTable dtpr = select("Select Distinct PayrollTaxFormula.*,PriorityNo from PayrollTaxFormulaPriority inner join PayrollTaxFormula on " +
                "PayrollTaxFormula.Id=PayrollTaxFormulaPriority.TaxformulaId where  " +
                "PayrollTaxFormula.PayrollMasterId='" + TaxdedId + "' and Taxyear='" + yearId + "'  order by PriorityNo Asc");

            foreach (DataRow item in dtpr.Rows)
            {
                string Orderpera = "";
                if (Convert.ToString(item["VarType"]) == "2")
                {
                    string Selcond = "";
                    if (Convert.ToString(item["ConType"]) == "2")
                    {
                        Orderpera = " Order by Id Desc";
                        Selcond = "Top(1) " + Convert.ToString(item["TableField"]) + " as Valueamt";
                    }
                    else
                    {
                        Selcond = " Sum(Cast(" + Convert.ToString(item["TableField"]) + " as Decimal(18,2))) as Valueamt";
                    }
                    string fillpera = "";
                    if (Convert.ToString(item["TableFieEmp"]) != "0" && Convert.ToString(item["TableFieEmp"]) != "")
                    {
                        fillpera += " where " + Convert.ToString(item["TableFieEmp"]) + "='" + ddlemp.SelectedValue + "'";
                    }
                    if (Convert.ToString(item["TableFieYear"]) != "0" && Convert.ToString(item["TableFieYear"]) != "")
                    {
                        if (fillpera == "")
                        {
                            fillpera += " where " + Convert.ToString(item["TableFieYear"]) + "='" + yearId + "'";
                        }
                        else
                        {
                            fillpera += " And " + Convert.ToString(item["TableFieYear"]) + "='" + yearId + "'";
                        }
                    }

                    if (Convert.ToString(item["TableFiePayperiod"]) != "0" && Convert.ToString(item["TableFiePayperiod"]) != "")
                    {
                        if (fillpera == "")
                        {
                            fillpera += " where " + Convert.ToString(item["TableFiePayperiod"]) + "='" + ddlpayperiod.SelectedValue + "'";

                        }
                        else
                        {
                            fillpera += " And " + Convert.ToString(item["TableFiePayperiod"]) + "='" + ddlpayperiod.SelectedValue + "'";
                        }
                    }
                    if (Convert.ToString(item["TableFieOther"]) != "0" && Convert.ToString(item["ReratedVarId"]) != "0" && Convert.ToString(item["ReratedVarId"]) != "")
                    {
                        if (fillpera == "")
                        {
                            fillpera += " where " + Convert.ToString(item["TableFieOther"]) + "='" + Convert.ToString(item["ReratedVarId"]) + "'";

                        }
                        else
                        {
                            fillpera += " And " + Convert.ToString(item["TableFieOther"]) + "='" + Convert.ToString(item["ReratedVarId"]) + "'";
                        }
                    }
                    else
                    {
                        if (Convert.ToString(item["TableFieOther"]) != "0" && Convert.ToString(item["TableFieOther"]) != "")
                        {
                            if (fillpera == "")
                            {
                                fillpera += " where " + Convert.ToString(item["TableFieOther"]) + "='" + TaxdedId + "'";

                            }
                            else
                            {
                                fillpera += " And " + Convert.ToString(item["TableFieOther"]) + "='" + TaxdedId + "'";
                            }
                        }
                    }
                    string strdynstat = "";
                    try
                    {
                        strdynstat = "Select " + Selcond + "  from " + Convert.ToString(item["TableValue"]) + " " + fillpera + Orderpera;
                    }
                    catch
                    {
                        strdynstat = "Select " + Selcond + "  from " + Convert.ToString(item["TableValue"]) + " " + fillpera;
                    }
                    decimal dyamtv = 0;
                    DataTable dtbn = select(strdynstat);
                    if (dtbn.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtbn.Rows[0]["Valueamt"]) != "")
                        {
                            dyamtv = Convert.ToDecimal(dtbn.Rows[0]["Valueamt"]);
                        }

                    }
                    fillVAriablevalues(item["Id"].ToString(), Convert.ToString(dyamtv));

                }
                else
                {
                    string formvarname = Convert.ToString(item["VariableCode"]);
                    string Formvarst = Convert.ToString(item["Formula11"]);
                    //if (Convert.ToString(item["VariableCode"]).Replace(" ", "") == "P")
                    //{
                    //    fillVAriablevalues(item["Id"].ToString(), P);
                    //}
                    //else if (Convert.ToString(item["VariableCode"]).Replace(" ", "") == "I")
                    //{
                    //    fillVAriablevalues(item["Id"].ToString(), I);
                    //}

                    if (Convert.ToString(item["Formula11"]) != "")
                    {
                        DataTable dtv = select("select * from PayrollTaxFormulaCondition where PayrollTaxFormulaId='" + item["Id"] + "' Order by Id");
                        if (dtv.Rows.Count > 0)
                        {
                            if (dtv.Rows.Count == 1 && Convert.ToString(dtv.Rows[0]["ValueorCode"]) != "")
                            {  ///// find only single value code ex.  K=L or L Equal to L this way
                                string valamt = "";
                                if (Convert.ToBoolean(dtv.Rows[0]["Allowedcodevar"]) == true)
                                {
                                    valamt = addforval(Convert.ToString(dtv.Rows[0]["ValueorCode"]), TaxdedId);
                                }
                                else
                                {
                                    valamt = Convert.ToString(dtv.Rows[0]["ValueorCode"]);
                                }
                                fillVAriablevalues(item["Id"].ToString(), valamt);
                            }
                            else
                            {
                                int lessorof = 0;
                                int higherof = 0;
                                int conand = 0;
                                int ifcon = 0;
                                int conor = 0;
                                int thencon = 0;
                                for (int i = 0; i < dtv.Rows.Count; i++)
                                {
                                    if (Convert.ToString(dtv.Rows[i]["ConditionValue"]) == "1")
                                    {///// find Lessar of condition
                                        lessorof += 1;
                                    }
                                    else if (Convert.ToString(dtv.Rows[i]["ConditionValue"]) == "2")
                                    {///// find higher of condition
                                        higherof += 1;
                                    }
                                    else if (Convert.ToString(dtv.Rows[i]["ConditionValue"]) == "10")
                                    {   ///// find And condition
                                        conand += 1;
                                    }
                                    else if (Convert.ToString(dtv.Rows[i]["ConditionValue"]) == "8")
                                    {///// find If condition
                                        ifcon += 1;
                                    }
                                    else if (Convert.ToString(dtv.Rows[i]["ConditionValue"]) == "15")
                                    {///// find If condition
                                        conor += 1;
                                    }
                                    else if (Convert.ToString(dtv.Rows[i]["ConditionValue"]) == "9")
                                    {///// find then condition
                                        thencon += 1;
                                    }
                                }

                                decimal result = 0;
                                decimal result1 = 0;
                                if ((lessorof == 1 || higherof == 1) && conand == 1 && ifcon <= 1)
                                {
                                    DataTable dtb = select(" select Distinct Valueamt,ValueorCode,EmployeePayrollVariableValuesTbl.Id from EmployeePayrollVariableValuesTbl inner join  PayrollTaxFormula on PayrollTaxFormula.Id=EmployeePayrollVariableValuesTbl.VariableCodeID inner join PayrollTaxFormulaCondition on  PayrollTaxFormula.VariableCode= PayrollTaxFormulaCondition.ValueorCode where PayrollTaxFormulaCondition.PayrollTaxFormulaId='" + item["Id"].ToString() + "' and PayrollTaxFormula.PayrollMasterId='" + TaxdedId + "'  and Employeeid='" + ddlemp.SelectedValue + "' and PayperiodId='" + ddlpayperiod.SelectedValue + "' and PayrollMasterId='" + TaxdedId + "' and Allowedcodevar='1' order by ValueorCode Desc");

                                    string formulacon = Convert.ToString(item["Formula11"]).Replace("Lesser of", "");
                                    formulacon = Convert.ToString(formulacon).Replace("Higher of", "");
                                    string[] separator1 = new string[] { "And" };
                                    string[] strSplitArr1 = formulacon.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

                                    int i111 = Convert.ToInt32(strSplitArr1.Length);
                                    if (i111 == 2)
                                    {
                                        if (Convert.ToString(strSplitArr1[0]) != "")
                                        {
                                            result = calfunc(Convert.ToString(strSplitArr1[0]), dtb);
                                        } if (Convert.ToString(strSplitArr1[1]) != "")
                                        {
                                            result1 = calfunc(Convert.ToString(strSplitArr1[1]), dtb);
                                        }
                                        if (lessorof == 1)
                                        {
                                            if (result < result1)
                                            {
                                                finalamt = Convert.ToDecimal(result);
                                            }
                                            else
                                            {
                                                finalamt = Convert.ToDecimal(result1);

                                            }
                                        }
                                        else if (higherof == 1)
                                        {
                                            if (result > result1)
                                            {
                                                finalamt = Convert.ToDecimal(result);
                                            }
                                            else
                                            {
                                                finalamt = Convert.ToDecimal(result1);

                                            }
                                        }
                                        fillVAriablevalues(item["Id"].ToString(), finalamt.ToString());
                                    }
                                }
                                else if (ifcon > 0 && thencon > 0)
                                {
                                    DataTable dtb = select(" select Distinct Valueamt,ValueorCode,EmployeePayrollVariableValuesTbl.Id from EmployeePayrollVariableValuesTbl inner join  PayrollTaxFormula on PayrollTaxFormula.Id=EmployeePayrollVariableValuesTbl.VariableCodeID inner join PayrollTaxFormulaCondition on  PayrollTaxFormula.VariableCode= PayrollTaxFormulaCondition.ValueorCode where PayrollTaxFormulaCondition.PayrollTaxFormulaId='" + item["Id"].ToString() + "' and PayrollTaxFormula.PayrollMasterId='" + TaxdedId + "'  and Employeeid='" + ddlemp.SelectedValue + "' and PayperiodId='" + ddlpayperiod.SelectedValue + "' and PayrollMasterId='" + TaxdedId + "' and Allowedcodevar='1' order by ValueorCode Desc");
                                    if (dtb.Rows.Count > 0)
                                    {

                                        if (formvarname == "K")
                                        {
                                        }
                                        decimal Retamt = multiif(item["Formula11"].ToString(), dtb);
                                        fillVAriablevalues(item["Id"].ToString(), Retamt.ToString());
                                    }
                                    //string formulacon = Convert.ToString(item["Formula11"]).Replace("Lesser of ", "");
                                    //formulacon = Convert.ToString(formulacon).Replace("Higher of", "");

                                }
                                else if (lessorof == 0 && higherof == 0 && conand == 0 && ifcon == 0)
                                {

                                    DataTable dtb = select(" select Distinct Valueamt,ValueorCode,EmployeePayrollVariableValuesTbl.Id from EmployeePayrollVariableValuesTbl inner join  PayrollTaxFormula on PayrollTaxFormula.Id=EmployeePayrollVariableValuesTbl.VariableCodeID inner join PayrollTaxFormulaCondition on  PayrollTaxFormula.VariableCode= PayrollTaxFormulaCondition.ValueorCode where  PayrollTaxFormulaCondition.PayrollTaxFormulaId='" + item["Id"].ToString() + "' and PayrollTaxFormula.PayrollMasterId='" + TaxdedId + "'  and Employeeid='" + ddlemp.SelectedValue + "' and PayperiodId='" + ddlpayperiod.SelectedValue + "' and PayrollMasterId='" + TaxdedId + "' and Allowedcodevar='1' order by ValueorCode Desc");

                                    string formulacon = Convert.ToString(item["Formula11"]).Replace("Equal to", "");
                                    result = calfunc(Convert.ToString(formulacon), dtb);
                                    fillVAriablevalues(item["Id"].ToString(), result.ToString());
                                    finalamt = result;
                                }
                            }

                        }
                    }
                    else if (Convert.ToString(item["Formula11"]) == "")
                    {
                        fillVAriablevalues(item["Id"].ToString(), "0");
                    }


                }
            }

        }
        return finalamt;
    }
    protected decimal multiif(string formula, DataTable dtb)
    {
        decimal Retamt = 0;

        formula = formula.Replace("If", "&");
        formula = formula.Replace("Then", "#");
        formula = formula.Replace("And", "@");
        formula = formula.Replace("Or", "|");
        foreach (DataRow Tbt in dtb.Rows)
        {
            bool allcomp = formula.Contains(Convert.ToString(Tbt["ValueorCode"]));
            if (allcomp == true)
            {
                formula = formula.Replace(Convert.ToString(Tbt["ValueorCode"]), Convert.ToString(Tbt["Valueamt"]));
            }
        }
        formula = formula.Replace("&", "If");
        formula = formula.Replace("#", "Then");
        formula = formula.Replace("@", "And");
        formula = formula.Replace("|", "Or");

        string[] separator1 = new string[] { "If" };
        string[] strSplitArr1 = formula.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

        int i111 = Convert.ToInt32(strSplitArr1.Length);

        for (int i = 0; i < i111; i++)
        {
            bool mormalcon = false;
            string conamt = "0";
            bool contval = false;
            bool contvalSec = false;
            string[] spthen = new string[] { "Then" };
            string[] spthenArr = strSplitArr1[i].Split(spthen, StringSplitOptions.RemoveEmptyEntries);

            string[] validFileTypes = { "And", "Or" };
            for (int jk = 0; jk < validFileTypes.Length; jk++)
            {
                bool Sym = spthenArr[0].Contains(validFileTypes[jk]);
                if (Sym == true)
                {
                    mormalcon = true;
                    string[] spANDCo = new string[] { validFileTypes[jk] };
                    string[] spANDCoArr = spthenArr[0].Split(spANDCo, StringSplitOptions.RemoveEmptyEntries);
                    int noofv = Convert.ToInt32(spANDCoArr.Length);

                    if (noofv == 2)
                    {
                        int kd = 0;
                        string[] Validco = { "<=", ">=", "<", ">" };
                        //spANDCoArr[0] = spANDCoArr[0].Replace(" ", "");
                        for (int mn = 0; mn < Validco.Length; mn++)
                        {

                            string[] SpCon = new string[] { Validco[mn] };
                            string[] SpConArr = spANDCoArr[0].Split(SpCon, StringSplitOptions.RemoveEmptyEntries);
                            int noofco = Convert.ToInt32(SpConArr.Length);
                            if (noofco > 1)
                            {
                                conamt = SpConArr[0];
                                if (Validco[mn] == "<=")
                                {
                                    kd = 1;
                                    if (Convert.ToDecimal(conamt) <= Convert.ToDecimal(SpConArr[1]))
                                    {
                                        contval = true;
                                    }
                                }
                                else if (Validco[mn] == "<")
                                {
                                    kd = 1;

                                    if (Convert.ToDecimal(conamt) < Convert.ToDecimal(SpConArr[1]))
                                    {
                                        contval = true;
                                    }
                                }
                                else if (Validco[mn] == ">=")
                                {
                                    kd = 1;

                                    if (Convert.ToDecimal(conamt) >= Convert.ToDecimal(SpConArr[1]))
                                    {
                                        contval = true;
                                    }
                                }
                                else if (Validco[mn] == ">" || Validco[mn] == "> " || Validco[mn] == " > ")
                                {
                                    kd = 1;
                                    try
                                    {
                                        if (Convert.ToDecimal(conamt) > Convert.ToDecimal(SpConArr[1]))
                                        {
                                            contval = true;
                                        }
                                    }
                                    catch (Exception er)
                                    {
                                        lblmsg.Visible = true;
                                        lblmsg.Text = conamt + " :: " + Convert.ToString(SpConArr[1]) + er;
                                    }
                                }
                                if (contval == true)
                                {
                                    contval = false;
                                    string[] Validco1 = { "<=", ">=", "<", ">" };
                                    for (int mn1 = 0; mn1 < Validco1.Length; mn1++)
                                    {
                                        string[] SpCon11 = new string[] { Validco1[mn1] };
                                        string[] SpConArr11 = spANDCoArr[1].Split(SpCon11, StringSplitOptions.RemoveEmptyEntries);
                                        int noofco11 = Convert.ToInt32(SpConArr11.Length);
                                        if (noofco11 > 1)
                                        {
                                            decimal Amtfv = 0;
                                            SpConArr11[0] = SpConArr11[0].Replace(" ", "");
                                            if (Convert.ToString(SpConArr11[0]) == "")
                                            {
                                                Amtfv = Convert.ToDecimal(SpConArr11[1]);
                                            }
                                            else
                                            {
                                                Amtfv = Convert.ToDecimal(SpConArr11[0]);
                                            }
                                            if (Validco1[mn1] == "<=")
                                            {
                                                if (Convert.ToDecimal(conamt) <= Amtfv)
                                                {
                                                    contvalSec = true;
                                                    contval = true;
                                                }

                                            }
                                            else if (Validco1[mn1] == "<")
                                            {
                                                if (Convert.ToDecimal(conamt) < Amtfv)
                                                {
                                                    contvalSec = true;
                                                    contval = true;
                                                }

                                            }
                                            else if (Validco1[mn1] == ">=")
                                            {
                                                if (Convert.ToDecimal(conamt) >= Amtfv)
                                                {
                                                    contvalSec = true;
                                                    contval = true;
                                                }

                                            }
                                            else if (Validco1[mn1] == ">")
                                            {
                                                if (Convert.ToDecimal(conamt) > Amtfv)
                                                {
                                                    contvalSec = true;
                                                    contval = true;
                                                }

                                            }
                                            break;

                                        }

                                    }
                                }
                                if (contvalSec == true)
                                {
                                    break;
                                }

                            }
                            if (kd == 1)
                            {
                                break;
                            }

                        }
                    }

                }

            }
            if (mormalcon == false)
            {
                string[] Validco = { "<=", ">=", "<", ">" };
                for (int mn = 0; mn < Validco.Length; mn++)
                {
                    string[] SpCon = new string[] { Validco[mn] };
                    string[] SpConArr = spthenArr[0].Split(SpCon, StringSplitOptions.RemoveEmptyEntries);
                    int noofco = Convert.ToInt32(SpConArr.Length);
                    if (noofco > 1)
                    {
                        conamt = SpConArr[0];
                        if (Validco[mn] == "<=")
                        {
                            try
                            {

                                if (Convert.ToDecimal(conamt) <= Convert.ToDecimal(SpConArr[1]))
                                {
                                    contval = true;
                                }
                            }
                            catch (Exception er)
                            {
                                lblmsg.Visible = true;
                                lblmsg.Text = conamt + " :: " + Convert.ToString(SpConArr[1]) + er;
                            }
                        }
                        else if (Validco[mn] == "<")
                        {

                            if (Convert.ToDecimal(conamt) < Convert.ToDecimal(SpConArr[1]))
                            {
                                contval = true;
                            }
                        }
                        else if (Validco[mn] == ">=")
                        {

                            if (Convert.ToDecimal(conamt) >= Convert.ToDecimal(SpConArr[1]))
                            {
                                contval = true;
                            }
                        }
                        else if (Validco[mn] == ">")
                        {

                            if (Convert.ToDecimal(conamt) > Convert.ToDecimal(SpConArr[1]))
                            {
                                contval = true;
                            }
                        }
                        break;
                    }
                }
            }
            if (contval == true)
            {
                Retamt = Convert.ToDecimal(spthenArr[1]);
                break;
            }

        }
        return Retamt;
    }
    protected decimal calfunc(string formula, DataTable dt)
    {
        decimal result = 0;

        formula = formula.Replace("-", " - ");
        formula = formula.Replace("*", " * ");
        formula = formula.Replace("/", " / ");
        formula = formula.Replace("(", " ( ");
        formula = formula.Replace(")", " ) ");
        formula = formula.Replace("+", " + ");

        formula = formula.Replace("[", " ( ");
        formula = formula.Replace("]", " ) ");

        foreach (DataRow item in dt.Rows)
        {
            bool allcomp = formula.Contains(Convert.ToString(item["ValueorCode"]));
            if (allcomp == true)
            {
                formula = formula.Replace(Convert.ToString(item["ValueorCode"]), Convert.ToString(item["Valueamt"]));
            }
        }

        try
        {

            result = mp.Calculate(formula);
        }
        catch
        {
        }
        return Math.Round(result, 4);
    }
    protected string addforval(string varty, string TaxdedId)
    {
        string amt = "0";
        Temp1 = "INSERT INTO EmployeePayrollVariableValuesTbl(TaxYear,VariableCodeID,Valueamt" +
                                ",Employeeid,PayperiodId)Values";

        DataTable dtcv = select("select Valueamt from EmployeePayrollVariableValuesTbl inner join PayrollTaxFormula on EmployeePayrollVariableValuesTbl.VariableCodeID= " +
       " PayrollTaxFormula.Id where VariableCode='" + varty + "' and Employeeid='" + ddlemp.SelectedValue + "' and PayperiodId='" + ddlpayperiod.SelectedValue + "' and PayrollMasterId='" + TaxdedId + "'");

        if (dtcv.Rows.Count > 0)
        {
            amt = Convert.ToString(dtcv.Rows[0]["Valueamt"]);

        }
        return amt;
    }

    protected void fillVAriablevalues(string varid, string amt)
    {
        if (Convert.ToDecimal(amt) < 0)
        {
            amt = "0.00";
        }
        Temp1 = "INSERT INTO EmployeePayrollVariableValuesTbl(TaxYear,VariableCodeID,Valueamt" +
                                ",Employeeid,PayperiodId)Values" +
       "('" + yearId + "','" + varid + "','" + amt + "','" + ddlemp.SelectedValue + "','" + ddlpayperiod.SelectedValue + "')";
        SqlCommand cd4 = new SqlCommand(Temp1, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cd4.ExecuteNonQuery();
        con.Close();
    }
    protected void filltax()
    {
        if (yearId == "")
        {
            fillyearid();
        }
        if (yearId != "")
        {


            DataTable dtTemp = new DataTable();
            if (ViewState["TempGovt"] == null)
            {
                dtTemp = Govttax();
            }

            else
            {
                dtTemp = (DataTable)ViewState["TempGovt"];
            }


            //I = (Convert.ToDecimal(txtbenifitgrossamt.Text) - Convert.ToDecimal(txttotded.Text)).ToString();
            I = txtbenifitgrossamt.Text;
            DataTable dt1 = new DataTable();


            DataTable dts = select("Select Distinct PayrollGovtDeductionTbl.*, PayrolltaxMaster.tax_name+' : '+PayrolltaxMaster.Sortname as TaxDetailName,PayrolltaxMaster.Payrolltax_id from PayrollGovtDeductionTbl inner join  PayrolltaxMaster on PayrollGovtDeductionTbl.Payrolltaxmasterid=PayrolltaxMaster.Payrolltax_id inner join PayRollTaxDetail  on PayRollTaxDetail.Payrolltaxmasterid=PayrolltaxMaster.Payrolltax_id" +
            " where  PayrollGovtDeductionTbl.Whid='" + ddlwarehouse.SelectedValue + "' and(PayRollTaxDetail.EffectiveStartDate<='" + sdate + "' and PayRollTaxDetail.EffectiveEndDate>='" + edate + "') order by PayrolltaxMaster.Payrolltax_id ASC");
            decimal totgovde = 0;
            if (dts.Rows.Count > 0)
            {
                string strdelePurD = " delete from PayrollTaxTempVariableTbl where TaxYearId='" + yearId + "' and payperiodId='" + ddlpayperiod.SelectedValue + "' and EmployeeId = '" + ddlemp.SelectedValue + "' ";
                SqlCommand cmddeltrans = new SqlCommand(strdelePurD, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddeltrans.ExecuteNonQuery();
                con.Close();
                string RRSPamt = "0";
                string Unionduestr = "0";
                foreach (GridViewRow item in grdded.Rows)
                {
                    Label lblrrsug = (Label)item.FindControl("lblrrsug");
                    TextBox Totamt = (TextBox)item.FindControl("Totamt");
                    if (lblrrsug.Text == "1")
                    {
                        RRSPamt = Totamt.Text;
                    }
                    if (lblrrsug.Text == "2")
                    {
                        Unionduestr = Totamt.Text;
                    }
                }


                Temp1 = "INSERT INTO PayrollTaxTempVariableTbl(RRSPAmount,UnionDueAmt,GrossSalaryAmt" +
                                   ",NoofPayperiod,TaxYearId,payperiodId,EmployeeId)Values" +
                  "('" + RRSPamt + "','" + Unionduestr + "','" + I + "','" + P + "','" + yearId + "','" + ddlpayperiod.SelectedValue + "','" + ddlemp.SelectedValue + "')";
                SqlCommand cd4 = new SqlCommand(Temp1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cd4.ExecuteNonQuery();
                con.Close();

                foreach (DataRow item in dts.Rows)
                {


                    DataRow dtadd = dtTemp.NewRow();
                    dtadd["Id"] = Convert.ToString(item["PayrolltaxMasterId"]);
                    dtadd["DeductionName"] = Convert.ToString(item["TaxDetailName"]);
                    decimal amt = filltaxvar(Convert.ToString(item["PayrolltaxMasterId"]));
                    amt = Math.Round(amt, 2);
                    dtadd["Amount"] = amt;
                    totgovde += amt;
                    dtadd["CrAccId"] = Convert.ToString(item["CrAccId"]);
                    dtadd["DrAccId"] = Convert.ToString(item["DrAccId"]);
                    dtadd["PayrolltaxMasterId"] = Convert.ToString(item["PayrolltaxMasterId"]);
                    dtTemp.Rows.Add(dtadd);
                }
            }
            ViewState["TempGovt"] = dtTemp;
            grdgovded.DataSource = dtTemp;
            grdgovded.DataBind();
            txtgovtottax.Text = totgovde.ToString();
            txtnettotal.Text = (Convert.ToDecimal(txtnettotal.Text) - totgovde).ToString();
        }
    }
    protected void Recordcheck(DataTable dt1)
    {



        if (dt1.Rows.Count > 0)
        {
            btnaddnewrem.Visible = false;
            btndaily.Visible = false;
            btnmonth.Visible = false;
            btnded.Visible = false;
            btnEdit.Visible = true;
            btncalculate.Visible = false;
            pnlcau.Enabled = false;
            txttotincome.Text = Convert.ToString(dt1.Rows[0]["TotalIncome"]);
            //txttotded.Text = Convert.ToString(dt1.Rows[0]["TotalDeduction"]);
            txtnettotal.Text = Convert.ToString(dt1.Rows[0]["NetTotal"]);
            txtbenifitgrossamt.Text = Convert.ToString(dt1.Rows[0]["GrossRemu"]);
            txttotded.Text = Convert.ToString(dt1.Rows[0]["NonGovdedamt"]);
            txtgovtottax.Text = Convert.ToString(dt1.Rows[0]["Govdedamt"]);
            lbltotremunration.Text = Convert.ToString(dt1.Rows[0]["RemTotal"]);
            lbltotremperc.Text = Convert.ToString(dt1.Rows[0]["RemPerc"]);
            lbltotsales.Text = Convert.ToString(dt1.Rows[0]["RemSales"]);

            ViewState["dk"] = Convert.ToString(dt1.Rows[0]["Id"]);
            btnUpdate.Visible = false;
            btnsubmit.Visible = false;
            DataTable dt2 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname='Hour' and SalaryMasterId='" + dt1.Rows[0]["Id"] + "' order by Id ASC");

            if (dt2.Rows.Count > 0)
            {
                grdcal.DataSource = dt2;
                grdcal.DataBind();
                pnlcau.Visible = true;
                pnlhourly.Visible = true;
            }
            DataTable dt4 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname='Day' and SalaryMasterId='" + dt1.Rows[0]["Id"] + "' order by Id ASC");

            if (dt4.Rows.Count > 0)
            {
                grddaily.DataSource = dt4;
                grddaily.DataBind();
                pnlcau.Visible = true;
                pnldaily.Visible = true;
            }
            DataTable dt5 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName,Totalcomunitmonth as completemonth,maxcompete as completedmonthamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Month','Semi Month','Bi-Week','Week') and SalaryMasterId='" + dt1.Rows[0]["Id"] + "' order by Id ASC");

            if (dt5.Rows.Count > 0)
            {
                grdmonth.DataSource = dt5;
                grdmonth.DataBind();
                pnlcau.Visible = true;
                pnlmonth.Visible = true;
            }
            DataTable dt6 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as per, SalaryRemuneration.perunitname,totalunit,Actualpayunit as baseamt,remamt as Totamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as Remuname,Totalcomunitmonth as perof,maxcompete as baseamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Is Percentage') and SalaryMasterId='" + dt1.Rows[0]["Id"] + "' order by Id ASC");

            if (dt6.Rows.Count > 0)
            {
                grdispercentage.DataSource = dt6;
                grdispercentage.DataBind();
                pnlcau.Visible = true;
                pnlispercentage.Visible = true;
            }
            DataTable dt7 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as per, SalaryRemuneration.perunitname,totalunit,Actualpayunit as baseamt,remamt as Totamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as Remuname,Totalcomunitmonth as perof,maxcompete as baseamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Is Percentage of Sales') and SalaryMasterId='" + dt1.Rows[0]["Id"] + "' order by Id ASC");

            if (dt7.Rows.Count > 0)
            {
                grdsales.DataSource = dt7;
                grdsales.DataBind();
                pnlcau.Visible = true;
                pnlsales.Visible = true;
            }
            DataTable dt8 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as txtleaverate, SalaryRemuneration.perunitname ,totalunit,Actualpayunit as perleaveno,remamt as Totamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as LeaveType,Totalcomunitmonth as perof,maxcompete as baseamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Leave Encash') and SalaryMasterId='" + dt1.Rows[0]["Id"] + "' order by Id ASC");

            if (dt8.Rows.Count > 0)
            {
                grdleaveencash.DataSource = dt8;
                grdleaveencash.DataBind();
                pnlleaveencash.Visible = true;

            }
            // DataTable dt3 = (DataTable)select(" Select distinct RUDed,  SalaryDeduction.Id,Deductionamt as Amount, deductionper as per,deductionperof as perof,dedamt as Totamt, payrolldeductionotherstbl.DeductionName as DeductionName from SalaryDeduction LEFT Join payrolldeductionotherstbl on payrolldeductionotherstbl.Id=SalaryDeduction.DeductionId where  SalaryMasterId='" + dt1.Rows[0]["Id"] + "'");
            DataTable dt3 = (DataTable)select(" Select distinct RUDed, SalaryDeduction.Id,Deductionamt as Amount, deductionper as per,deductionperof as perof,dedamt as Totamt,DeductionName,RUDed from SalaryDeduction  where  SalaryMasterId='" + dt1.Rows[0]["Id"] + "'");

            if (dt3.Rows.Count > 0)
            {
                grdded.DataSource = dt3;
                grdded.DataBind();
            }
            DataTable dtgovte = (DataTable)select(" Select distinct SalaryGovtDeduction.Id as CrAccId, SalaryGovtDeduction.Id,Totdedamt as Amount,PayrolltaxMaster.tax_name+' : '+PayrolltaxMaster.Sortname as DeductionName from  SalaryGovtDeduction inner join  PayrolltaxMaster on PayrolltaxMaster.Payrolltax_id=SalaryGovtDeduction.PayrollTaxId where  SalaryMasterId='" + dt1.Rows[0]["Id"] + "'");

            //DataTable dtgovte = (DataTable)select(" Select distinct  CrAccId, SalaryGovtDeduction.Id,Totdedamt as Amount,PayrolltaxMaster.tax_name+' : '+PayrolltaxMaster.Sortname as DeductionName from PayrollGovtDeductionTbl inner join SalaryGovtDeduction on SalaryGovtDeduction.PayrollTaxId=SalaryGovtDeduction.PayrolltaxMasterId   inner join  PayrolltaxMaster on PayrolltaxMaster.Payrolltax_id=SalaryGovtDeduction.PayrollTaxId where  SalaryMasterId='" + dt1.Rows[0]["Id"] + "'");

            if (dtgovte.Rows.Count > 0)
            {
                grdgovded.DataSource = dtgovte;
                grdgovded.DataBind();
            }
            DataTable dttben = (DataTable)select(" Select distinct SalaryTaxableBenifit.Id,TaxableBenName as Taxablebenifitname,TaxbenDate as Date, TaxbenAmt as Amount from  SalaryTaxableBenifit where  SalaryMasterId='" + dt1.Rows[0]["Id"] + "'");

            if (dttben.Rows.Count > 0)
            {
                grdtaxbenifit.DataSource = dttben;
                grdtaxbenifit.DataBind();
            }
            pnlamtpaid.Visible = false;
            DataTable dtc = select("Select distinct Cheqno,PaidAmt,RelatedACId from SalaryRelatedPayTbl  where  SalaryRelatedPayTbl.SalaryMasterId='" + dt1.Rows[0]["Id"] + "'");
            if (dtc.Rows.Count > 0)
            {
                chkpaida.Checked = true;
                pnlamtpaid.Visible = true;
                ddlrelacc.SelectedIndex = ddlrelacc.Items.IndexOf(ddlrelacc.Items.FindByValue(Convert.ToString(dtc.Rows[0]["RelatedACId"])));
                txtrelcheque.Text = Convert.ToString(dtc.Rows[0]["Cheqno"]);
                txtrelpaidamr.Text = Convert.ToString(dtc.Rows[0]["PaidAmt"]);

            }

        }

    }
    protected decimal moofmonth()
    {
        int countno = 0;
        DateTime fsd = Convert.ToDateTime(sdate);
        DateTime fed = Convert.ToDateTime(edate);
        paymonth = 0;
        Decimal totalmonth = Convert.ToDecimal(System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(fsd, fed)) + 1;
        for (int i = 0; i < totalmonth + 1; i++)
        {

            if (fsd <= fed)
            {
                countno += 1;
                fsd = fsd.AddMonths(1);
            }
            else
            {
                countno -= 1;
                break;
            }
        }
        fsd = fsd.AddMonths(-1);
        int noyearfrist = fsd.Year;

        int nomonthfrist = fsd.Month;

        int noi = fed.Day;

        int nodayfrist = fsd.Day;
        int numberofdaymonth = DateTime.DaysInMonth(noyearfrist, nomonthfrist);
        int nooffirstdaydiff = noi - nodayfrist;
        nooffirstdaydiff += 1;
        int nomonthend = fed.Month;
        int nodayend = fed.Day;
        int noyearend = fsd.Year;
        int numberofdaymonthend = DateTime.DaysInMonth(noyearend, nomonthend);
        int noofenddaydiff = numberofdaymonthend - nodayend;

        //for (int i = 0; i < totalmonth; i++)
        //{
        //if (countno == 0)
        //{
        //countno += 1;
        //paymonth = mcal(countno, nomonthfrist, nooffirstdaydiff, numberofdaymonth);
        paymonth = mcal(countno, nomonthfrist, nooffirstdaydiff, numberofdaymonth);
        //}
        //else if (countno == totalmonth - 1)
        //{
        //paymonth = mcal(countno, nomonthend, nodayend, numberofdaymonthend);
        //}
        //else
        //{
        //   // countno += 1;
        //    paymonth += 1;
        //}
        //}
        return Math.Round(paymonth, 2);





        ///original

        //DateTime fsd = Convert.ToDateTime(sdate);
        //DateTime fed = Convert.ToDateTime(edate);
        //paymonth = 0;
        //Decimal totalmonth = Convert.ToDecimal(System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(fsd, fed)) + 1;
        //int noyearfrist = fsd.Year;

        //int nomonthfrist = fsd.Month;



        //int nodayfrist = fsd.Day;
        //int numberofdaymonth = DateTime.DaysInMonth(noyearfrist, nomonthfrist);
        //int nooffirstdaydiff = numberofdaymonth - nodayfrist;

        //int nomonthend = fed.Month;
        //int nodayend = fed.Day;
        //int noyearend = fsd.Year;
        //int numberofdaymonthend = DateTime.DaysInMonth(noyearend, nomonthend);
        //int noofenddaydiff = numberofdaymonthend - nodayend;
        //int countno = 0;
        //for (int i = 0; i < totalmonth; i++)
        //{
        //    if (countno == 0)
        //    {
        //        countno += 1;
        //        paymonth = mcal(paymonth, nomonthfrist, nooffirstdaydiff, numberofdaymonth);

        //    }
        //    else if (countno == totalmonth - 1)
        //    {
        //        paymonth = mcal(paymonth, nomonthend, nodayend, numberofdaymonthend);
        //    }
        //    else
        //    {
        //        countno += 1;
        //        paymonth += 1;
        //    }
        //}
        //return Math.Round(paymonth, 2);

    }
    protected decimal mcal(decimal paymonth, int nomonthfrist, int nodayfrist, int noday)
    {

        paymonth += Convert.ToDecimal(nodayfrist) / noday;

        return paymonth;
    }
    protected decimal weekcount()
    {
        payweek = 0;
        decimal countweek = 0;

        DataTable dtss = new DataTable();
        dtss = (DataTable)select("Select BatchWorkingDay.Batchmasterid,LastDayOftheWeek from EmployeeBatchMaster inner join BatchWorkingDay on BatchWorkingDay.BatchMasterId=EmployeeBatchMaster.Batchmasterid where Employeeid='" + ddlemp.SelectedValue + "'");
        if (dtss.Rows.Count > 0)
        {
            DateTime dtstart = Convert.ToDateTime(sdate);
            DateTime dtend = Convert.ToDateTime(edate);
            DataTable dts1 = new DataTable();
            for (int i = 0; i < 7; i++)
            {


                dts1 = (DataTable)select("select DateId,Date,day from  DateMasterTbl where [Date]='" + dtstart.ToShortDateString() + "'");
                if (dts1.Rows.Count > 0)
                {


                    if (Convert.ToString(dts1.Rows[0]["day"]) == Convert.ToString(dtss.Rows[0]["LastDayOftheWeek"]))
                    {

                        dtstart = dtstart.AddDays(7);
                        countweek += 1;
                        break;
                    }
                    else
                    {
                        dtstart = dtstart.AddDays(1);


                    }


                }
            }

            for (int i = 0; i < countweek; i++)
            {
                if (dtstart <= dtend)
                {

                    dtstart = dtstart.AddDays(7);
                    countweek += 1;
                }
                else
                {
                    break;
                }

            }


        }
        return countweek;
    }
    protected void fillremcal()
    {
        string[] separator1 = new string[] { " : " };
        string[] strSplitArr1 = ddlpayperiod.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
        sdate = strSplitArr1[1].ToString();
        edate = strSplitArr1[2].ToString();
        DataTable dt11 = new DataTable();
        dt11 = (DataTable)select("  Select * from EmployeePayrollMaster  where EmpId='" + ddlemp.SelectedValue + "'");
        if (dt11.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt11.Rows[0]["EmployeePaidAsPerDesignation"]) == true)
            {

            }
            else
            {
                DataTable dt111 = new DataTable();
                //dt111 = (DataTable)select("Select distinct EmployeeSalaryMaster.Id, RemunerationName as RemunarationName,Amount as Rate,Period_name as perunitname,Percentage as Perchantage,CASE WHEN (IsPercentRemunerationId IS NULL) THEN '0' ELSE IsPercentRemunerationId END AS perunitrate from RemunerationMaster inner join EmployeeSalaryMaster on EmployeeSalaryMaster.Remuneration_Id=RemunerationMaster.Id inner join PeriodMaster12 on PeriodMaster12.Id=EmployeeSalaryMaster.PayablePer_PeriodMasterId where EmployeeSalaryMaster.EffectiveStartDate>='" + sdate + "' and  EmployeeSalaryMaster.EffectiveEndDate<='" + edate + "' and EmployeeSalaryMaster.EmployeeId='" + ddlemp.SelectedValue + "'");

                //"Select distinct EmployeeSalaryMaster.Id,EmployeeID, RemunerationName as RemunarationName,CASE WHEN (Amount IS NULL)THEN '0' ELSE Amount END AS  Rate,case when (Period_name IS NULL) then '-' Else Period_name End as perunitname,CASE WHEN (Percentage IS NULL)Then '0'Else Percentage End as Perchantage,CASE WHEN (IsPercentRemunerationId IS NULL) THEN '0' ELSE IsPercentRemunerationId END AS perunitrate from RemunerationMaster inner join EmployeeSalaryMaster on EmployeeSalaryMaster.Remuneration_Id=RemunerationMaster.Id Left join PeriodMaster12 on PeriodMaster12.Id=EmployeeSalaryMaster.PayablePer_PeriodMasterId where EmployeeID='" + ddlemp.SelectedValue + "' order by EmployeeID, perunitname Desc";
                dt111 = (DataTable)select("Select distinct convert(nvarchar,EffectiveStartDate,101) as EffectiveStartDate,convert(nvarchar,EffectiveEndDate,101) as EffectiveEndDate, EmployeeSalaryMaster.Id,EmployeeID, RemunerationMaster.RemunerationName as RemunarationName,CASE WHEN (RM1.RemunerationName IS NULL)THEN '-' ELSE RM1.RemunerationName END AS  remperunit,CASE WHEN (Amount IS NULL)THEN '0' ELSE Amount END AS  Rate,case when (Period_name IS NULL) then '--' Else Period_name End as perunitname,CASE WHEN (Percentage IS NULL)Then '0'Else Percentage End as Perchantage,CASE WHEN (IsPercentRemunerationId IS NULL) THEN '0' ELSE IsPercentRemunerationId END AS perunitrate from  RemunerationMaster inner join EmployeeSalaryMaster on EmployeeSalaryMaster.Remuneration_Id=RemunerationMaster.Id Left join PeriodMaster12 on PeriodMaster12.Id=EmployeeSalaryMaster.PayablePer_PeriodMasterId  Left join  RemunerationMaster AS RM1 on EmployeeSalaryMaster.IsPercentRemunerationId=RM1.Id where EmployeeID='" + ddlemp.SelectedValue + "' and(EmployeeSalaryMaster.EffectiveEndDate>='" + sdate + "' or EmployeeSalaryMaster.EffectiveStartDate>='" + sdate + "') and (EmployeeSalaryMaster.EffectiveEndDate<='" + edate + "' or EmployeeSalaryMaster.EffectiveStartDate<='" + edate + "') order by EmployeeID, perunitname Desc");
                if (dt111.Rows.Count > 0)
                {
                    grdcal.DataSource = dt111;
                    grdcal.DataBind();
                    ViewState["TempDataTable"] = dt111;
                    fillcalg();
                }
                else
                {
                    grdcal.DataSource = null;
                    grdcal.DataBind();
                    ViewState["TempDataTable"] = null;
                }
            }
        }
    }
    protected void fillcalg()
    {
        string[] separator1 = new string[] { " : " };
        string[] strSplitArr1 = ddlpayperiod.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
        string sdate = strSplitArr1[1].ToString();
        string edate = strSplitArr1[2].ToString();
        double totinc = 0;
        Decimal totlhr = 0;

        double totsal = 0;
        int k = 0;
        int j = 0;
        foreach (GridViewRow grd in grdcal.Rows)
        {


            TextBox txtrate = (TextBox)grd.FindControl("txtrate");
            TextBox txtperunitname = (TextBox)grd.FindControl("txtperunitname");
            TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");
            TextBox txttotunit = (TextBox)grd.FindControl("txttotunit");
            TextBox txtamt = (TextBox)grd.FindControl("txtamt");
            TextBox txttotal = (TextBox)grd.FindControl("txttotal");
            TextBox txtperc = (TextBox)grd.FindControl("txtperc");
            TextBox txtperunitrate = (TextBox)grd.FindControl("txtperunitrate");
            TextBox txtamount = (TextBox)grd.FindControl("txtamount");

            Label lblefsdate = (Label)grd.FindControl("lblefsdate");
            Label lblefedate = (Label)grd.FindControl("lblefedate");
            TextBox ffind = (TextBox)grdcal.Rows[0].FindControl("txtperunitname");
            DataTable dts = new DataTable();
            if (lblefsdate.Text.ToString() != "")
            {
                if (Convert.ToDateTime(lblefsdate.Text) < Convert.ToDateTime(sdate))
                {
                    lblefsdate.Text = sdate;
                }
                if (Convert.ToDateTime(lblefedate.Text) > Convert.ToDateTime(edate))
                {
                    lblefedate.Text = edate;
                }
                if (txtperunitname.Text == "Hour" || ffind.Text == "Hour")
                {

                    if (txtperunitname.Text != "--")
                    {
                        dts = (DataTable)select("Select Sum(cast(Left(Payablehours,2) as Decimal)) as Payablehours ,Sum(cast(Left(BatchRequiredhours,2) as Decimal)) as BatchRequiredhours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                        //dts = (DataTable)select("Select format(Sum(Payablehours),'hh:mm') as Payablehours, format(Sum(BatchRequiredhours),'hh:mm') as BatchRequiredhours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");

                        if (dts.Rows.Count > 0)
                        {

                            txtqlifie.Text = Convert.ToString(dts.Rows[0]["Payablehours"]);
                            txttotunit.Text = Convert.ToString(dts.Rows[0]["BatchRequiredhours"]);
                            if (txtqlifie.Text != "")
                            {
                                txtamt.Text = (Convert.ToDecimal(txtqlifie.Text) * Convert.ToDecimal(Convert.ToDecimal(txtrate.Text))).ToString();
                                txttotal.Text = (Convert.ToDouble(txttotal.Text) + Convert.ToDouble(txtamt.Text)).ToString();
                                totinc = totinc + Convert.ToDouble(txtamt.Text);
                                totsal = totsal + Convert.ToDouble(txtamt.Text);
                                k += 1;
                            }
                        }

                    }
                    else if (txtperc.Text != "0")
                    {
                        double totho = 0;
                        double tothr = 0;
                        double couday = 0;

                        double countdif = 0;
                        for (int g = 0; g < k; g++)
                        {

                            Label lblefsdate1 = (Label)grdcal.Rows[g].FindControl("lblefsdate");
                            Label lblefedate1 = (Label)grdcal.Rows[g].FindControl("lblefedate");
                            TextBox txtrate1 = (TextBox)grdcal.Rows[g].FindControl("txtrate");
                            TextBox txtperunitname1 = (TextBox)grdcal.Rows[g].FindControl("txtperunitname");
                            TextBox txtqlifie1 = (TextBox)grdcal.Rows[g].FindControl("txtqlifie");
                            TextBox txtamt1 = (TextBox)grdcal.Rows[g].FindControl("txtamt");


                            TimeSpan td = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate1.Text));
                            if ((Convert.ToDateTime(lblefsdate.Text) != Convert.ToDateTime(lblefsdate1.Text)))
                            {
                                if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {
                                    dts = (DataTable)select("Select Sum(Cast(Payablehours as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }
                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) <= Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate.Text) >= Convert.ToDateTime(lblefsdate1.Text)))
                                {
                                    TimeSpan td1 = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate1.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;

                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) >= Convert.ToDateTime(lblefedate.Text)))
                                {

                                    dts = (DataTable)select("Select Sum(Cast(Payablehours as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }

                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) >= Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {

                                    dts = (DataTable)select("Select Sum(Cast(Payablehours as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }

                                }



                            }
                            else if ((Convert.ToDateTime(lblefedate.Text) != Convert.ToDateTime(lblefedate1.Text)))
                            {
                                if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {
                                    dts = (DataTable)select("Select Sum(Cast(Payabledays as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }
                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) <= Convert.ToDateTime(lblefedate1.Text)) && (Convert.ToDateTime(lblefedate.Text) >= Convert.ToDateTime(lblefedate.Text)))
                                {
                                    TimeSpan td1 = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;

                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) >= Convert.ToDateTime(lblefedate.Text)))
                                {

                                    dts = (DataTable)select("Select Sum(Cast(Payablehours as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }


                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) >= Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {

                                    dts = (DataTable)select("Select Sum(Cast(Payablehours as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }


                                }


                            }
                            else
                            {
                                totho = (((Convert.ToDouble(txtrate1.Text) * Convert.ToDouble(txtqlifie1.Text)) / 100));
                                tothr += Math.Round(totho, 4); ;

                            }
                            txtamount.Text = tothr.ToString();
                            txttotal.Text = txtamount.Text;
                        }


                        totinc = totinc + Convert.ToDouble(txtamount.Text);
                        j += 1;

                    }

                }
                else if (txtperunitname.Text == "Day" || ffind.Text == "Day")
                {
                    if (txtperunitname.Text != "--")
                    {
                        dts = (DataTable)select("Select Sum(Cast(Payabledays as int)) as Payablehours,Sum(Cast(BatchRequiredhours as int)) as BatchRequiredhours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                        if (dts.Rows.Count > 0)
                        {

                            txtqlifie.Text = Convert.ToString(dts.Rows[0]["Payablehours"]);
                            txttotunit.Text = Convert.ToString(dts.Rows[0]["BatchRequiredhours"]);
                            if (txtqlifie.Text != "")
                            {
                                txtamt.Text = (Convert.ToDecimal(txtqlifie.Text) * Convert.ToDecimal(Convert.ToDecimal(txtrate.Text))).ToString();
                                txttotal.Text = (Convert.ToDouble(txttotal.Text) + Convert.ToDouble(txtamt.Text)).ToString();
                                totinc = totinc + Convert.ToDouble(txtamt.Text);
                                totsal = totsal + Convert.ToDouble(txtamt.Text);
                                k += 1;
                            }
                        }

                    }
                    else if (txtperc.Text != "0")
                    {
                        double totho = 0;
                        double tothr = 0;
                        double couday = 0;

                        double countdif = 0;
                        for (int g = 0; g < k; g++)
                        {

                            Label lblefsdate1 = (Label)grdcal.Rows[g].FindControl("lblefsdate");
                            Label lblefedate1 = (Label)grdcal.Rows[g].FindControl("lblefedate");
                            TextBox txtrate1 = (TextBox)grdcal.Rows[g].FindControl("txtrate");
                            TextBox txtperunitname1 = (TextBox)grdcal.Rows[g].FindControl("txtperunitname");
                            TextBox txtqlifie1 = (TextBox)grdcal.Rows[g].FindControl("txtqlifie");
                            TextBox txtamt1 = (TextBox)grdcal.Rows[g].FindControl("txtamt");


                            TimeSpan td = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate1.Text));
                            if ((Convert.ToDateTime(lblefsdate.Text) != Convert.ToDateTime(lblefsdate1.Text)))
                            {

                                if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {
                                    dts = (DataTable)select("Select Sum(Cast(Payabledays as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }
                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) <= Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate.Text) >= Convert.ToDateTime(lblefsdate1.Text)))
                                {
                                    TimeSpan td1 = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate1.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;

                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) >= Convert.ToDateTime(lblefedate.Text)))
                                {

                                    dts = (DataTable)select("Select Sum(Cast(Payablehours as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }

                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) >= Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {
                                    dts = (DataTable)select("Select Sum(Cast(Payablehours as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }
                                }



                            }
                            else if ((Convert.ToDateTime(lblefedate.Text) != Convert.ToDateTime(lblefedate1.Text)))
                            {
                                if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {
                                    dts = (DataTable)select("Select Sum(Cast(Payabledays as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }
                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) <= Convert.ToDateTime(lblefedate1.Text)) && (Convert.ToDateTime(lblefedate.Text) >= Convert.ToDateTime(lblefedate.Text)))
                                {
                                    TimeSpan td1 = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;

                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) >= Convert.ToDateTime(lblefedate.Text)))
                                {

                                    dts = (DataTable)select("Select Sum(Cast(Payablehours as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }


                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) >= Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {

                                    dts = (DataTable)select("Select Sum(Cast(Payablehours as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    if (dts.Rows.Count > 0)
                                    {

                                        double dd = Convert.ToDouble(dts.Rows[0]["Payablehours"]);
                                        totho = ((dd) * Convert.ToDouble(txtrate1.Text));
                                        totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                        tothr += Math.Round(totho, 4); ;
                                    }

                                }





                            }
                            else
                            {
                                totho = (((Convert.ToDouble(txtrate1.Text) * Convert.ToDouble(txtqlifie1.Text)) / 100));
                                tothr += Math.Round(totho, 4); ;

                            }
                            txtamount.Text = tothr.ToString();
                            txttotal.Text = txtamount.Text;
                        }


                        totinc = totinc + Convert.ToDouble(txtamount.Text);
                        j += 1;

                    }

                }
                else if (txtperunitname.Text == "Week" || ffind.Text == "Week")
                {
                    if (txtperunitname.Text != "--")
                    {
                        if (txtqlifie.Text == "0")
                        {

                            double counter = 0;
                            double counter1 = 0;
                            double totcounter = 0;
                            double totunit = 0;
                            double totalpayunit = 0;
                            double rat = 0;
                            double totra = 0;
                            DataTable dtss = new DataTable();
                            dtss = (DataTable)select("Select BatchWorkingDay.Batchmasterid,LastDayOftheWeek from EmployeeBatchMaster inner join BatchWorkingDay on BatchWorkingDay.BatchMasterId=EmployeeBatchMaster.Batchmasterid where Employeeid='" + ddlemp.SelectedValue + "'");
                            if (dtss.Rows.Count > 0)
                            {
                                DateTime dtp = Convert.ToDateTime(lblefsdate.Text);
                                DataTable dts1 = new DataTable();
                                for (int i = 0; i < 7; i++)
                                {


                                    dts1 = (DataTable)select("select DateId,Date,day from  DateMasterTbl where [Date]='" + dtp.ToShortDateString() + "'");
                                    if (dts1.Rows.Count > 0)
                                    {


                                        if (Convert.ToString(dts1.Rows[0]["day"]) == Convert.ToString(dtss.Rows[0]["LastDayOftheWeek"]))
                                        {
                                            i = 7;
                                            dtp = dtp.AddDays(-7);
                                        }
                                        else
                                        {
                                            dtp = dtp.AddDays(1);
                                            counter += 1;

                                        }


                                    }
                                }
                                for (int i = 0; i < 7; i++)
                                {
                                    dts1 = (DataTable)select("select DateId,Date,day from  DateMasterTbl where [Date]='" + dtp.ToShortDateString() + "'");
                                    if (dts1.Rows.Count > 0)
                                    {
                                        if (dtp != Convert.ToDateTime(lblefedate.Text))
                                        {

                                            if (i == 6)
                                            {

                                                i = 0;
                                                totunit += totcounter;
                                                totalpayunit += counter1;
                                                rat = Math.Round((counter1 / totcounter), 4);
                                                totra += rat;

                                                totcounter = 0;
                                                counter1 = 0;

                                            }

                                            DataTable dts11 = new DataTable();
                                            dts11 = (DataTable)select("select * from  BatchWorkingDays  where BatchID='" + dtss.Rows[0]["Batchmasterid"] + "' and DateMasterID='" + dts1.Rows[0]["DateId"] + "'");
                                            if (dts11.Rows.Count > 0)
                                            {
                                                totcounter += 1;
                                                DataTable dts11a = new DataTable();
                                                dts11a = (DataTable)select("Select Date from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date= '" + dtp.ToShortDateString() + "' ");
                                                if (dts11a.Rows.Count > 0)
                                                {
                                                    counter1 += 1;
                                                }
                                            }
                                            dtp = dtp.AddDays(1);
                                        }
                                        else
                                        {
                                            i = 7;

                                            txtqlifie.Text = Convert.ToString(totra);
                                            txttotunit.Text = Convert.ToString(totcounter);
                                            if (txtqlifie.Text != "")
                                            {
                                                txtamt.Text = (Convert.ToDecimal(txtqlifie.Text) * Convert.ToDecimal(Convert.ToDecimal(txtrate.Text))).ToString();
                                                txttotal.Text = (Convert.ToDouble(txttotal.Text) + Convert.ToDouble(txtamt.Text)).ToString();
                                                totinc = totinc + Convert.ToDouble(txtamt.Text);
                                                totsal = totsal + Convert.ToDouble(txtamt.Text);
                                                k += 1;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                    else if (txtperc.Text != "0")
                    {
                        double totho = 0;
                        double tothr = 0;
                        double couday = 0;

                        double countdif = 0;
                        for (int g = 0; g < k; g++)
                        {

                            Label lblefsdate1 = (Label)grdcal.Rows[g].FindControl("lblefsdate");
                            Label lblefedate1 = (Label)grdcal.Rows[g].FindControl("lblefedate");
                            TextBox txtrate1 = (TextBox)grdcal.Rows[g].FindControl("txtrate");
                            TextBox txtperunitname1 = (TextBox)grdcal.Rows[g].FindControl("txtperunitname");
                            TextBox txtqlifie1 = (TextBox)grdcal.Rows[g].FindControl("txtqlifie");
                            TextBox txtamt1 = (TextBox)grdcal.Rows[g].FindControl("txtamt");


                            TimeSpan td = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate1.Text));
                            if ((Convert.ToDateTime(lblefsdate.Text) != Convert.ToDateTime(lblefsdate1.Text)))
                            {

                                if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {
                                    //dts = (DataTable)select("Select Sum(Cast(Payabledays as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    //if (dts.Rows.Count > 0)
                                    //{

                                    TimeSpan td1 = Convert.ToDateTime(lblefedate.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;

                                    //}
                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) <= Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate.Text) >= Convert.ToDateTime(lblefsdate1.Text)))
                                {
                                    TimeSpan td1 = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate1.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;

                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) >= Convert.ToDateTime(lblefedate.Text)))
                                {

                                    TimeSpan td1 = Convert.ToDateTime(lblefedate.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;


                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) >= Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {

                                    TimeSpan td1 = Convert.ToDateTime(lblefedate.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;


                                }


                            }
                            else if ((Convert.ToDateTime(lblefedate.Text) != Convert.ToDateTime(lblefedate1.Text)))
                            {
                                if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {
                                    TimeSpan td1 = Convert.ToDateTime(lblefedate.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;
                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) <= Convert.ToDateTime(lblefedate1.Text)) && (Convert.ToDateTime(lblefedate.Text) >= Convert.ToDateTime(lblefedate.Text)))
                                {
                                    TimeSpan td1 = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;

                                }




                            }
                            else
                            {
                                totho = (((Convert.ToDouble(txtrate1.Text) * Convert.ToDouble(txtqlifie1.Text)) / 100));
                                tothr += Math.Round(totho, 4); ;

                            }
                            txtamount.Text = tothr.ToString();
                            txttotal.Text = txtamount.Text;
                        }


                        totinc = totinc + Convert.ToDouble(txtamount.Text);
                        j += 1;

                    }

                }
                else if (txtperunitname.Text == "Month" || ffind.Text == "Month")
                {
                    if (txtperunitname.Text != "--")
                    {
                        if (txtqlifie.Text == "0")
                        {
                            k += 1;
                            if (Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(sdate))
                            {
                                string dtsd = sdate;
                            }
                            if (Convert.ToDateTime(lblefedate.Text) < Convert.ToDateTime(edate))
                            {
                                string dted = edate;
                            }
                            double counter11 = 0;
                            double totcounter11 = 0;
                            double counter1 = 0;
                            double totcounter = 0;
                            double totunit = 0;
                            double totalpayunit = 0;
                            double rat = 0;
                            double totra = 0;
                            DataTable dtss = new DataTable();
                            dtss = (DataTable)select("Select BatchWorkingDay.Batchmasterid,LastDayOftheWeek from EmployeeBatchMaster inner join BatchWorkingDay on BatchWorkingDay.BatchMasterId=EmployeeBatchMaster.Batchmasterid where Employeeid='" + ddlemp.SelectedValue + "'");
                            if (dtss.Rows.Count > 0)
                            {
                                DateTime dtp = Convert.ToDateTime(lblefsdate.Text);
                                string stdd = (Convert.ToDateTime(lblefedate.Text).AddDays(1).Day.ToString());
                                string stdm = Convert.ToDateTime(lblefedate.Text).Month.ToString();
                                string stdy = Convert.ToDateTime(lblefedate.Text).Year.ToString();

                                for (int i = 0; i < 32; i++)
                                {
                                    DataTable dts1 = new DataTable();
                                    dts1 = (DataTable)select("select DateId,Date,day from  DateMasterTbl where [Date]='" + dtp.ToShortDateString() + "'");
                                    if (dts1.Rows.Count > 0)
                                    {
                                        totcounter11 += 1;
                                        DataTable dts11 = new DataTable();
                                        dts11 = (DataTable)select("select * from  BatchWorkingDays  where BatchID='" + dtss.Rows[0]["Batchmasterid"] + "' and DateMasterID='" + dts1.Rows[0]["DateId"] + "'");
                                        if (dts11.Rows.Count > 0)
                                        {
                                            totcounter += 1;
                                            DataTable dts11a = new DataTable();
                                            dts11a = (DataTable)select("Select Date from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date= '" + dtp.ToShortDateString() + "' ");
                                            if (dts11a.Rows.Count > 0)
                                            {
                                                counter1 += 1;
                                            }
                                        }
                                        dtp = dtp.AddDays(1);
                                        string sttt = dtp.Day.ToString();
                                        if (stdd == sttt)
                                        {
                                            totunit += totcounter;
                                            totalpayunit += counter1;

                                            counter11 += Math.Round((totcounter / totcounter11), 4);
                                            rat = Math.Round((counter1 / totcounter), 4);
                                            totra += rat;
                                            totcounter = 0;
                                            counter1 = 0;
                                            if (Convert.ToDateTime(lblefedate.Text) <= dtp)
                                            {
                                                i = 32;
                                            }

                                        }

                                    }
                                }
                                txtqlifie.Text = Convert.ToString(totra);
                                txttotunit.Text = Convert.ToString(counter11);
                                if (txtqlifie.Text != "")
                                {
                                    txtamt.Text = (Convert.ToDecimal(txtqlifie.Text) * Convert.ToDecimal(Convert.ToDecimal(txtrate.Text))).ToString();
                                    txttotal.Text = (Convert.ToDouble(txttotal.Text) + Convert.ToDouble(txtamt.Text)).ToString();
                                    totinc = totinc + Convert.ToDouble(txtamt.Text);
                                    totsal = totsal + Convert.ToDouble(txtamt.Text);

                                }
                            }

                        }
                    }
                    else if (txtperc.Text != "0")
                    {
                        double totho = 0;
                        double tothr = 0;
                        double couday = 0;

                        double countdif = 0;
                        for (int g = 0; g < k; g++)
                        {

                            Label lblefsdate1 = (Label)grdcal.Rows[g].FindControl("lblefsdate");
                            Label lblefedate1 = (Label)grdcal.Rows[g].FindControl("lblefedate");
                            TextBox txtrate1 = (TextBox)grdcal.Rows[g].FindControl("txtrate");
                            TextBox txtperunitname1 = (TextBox)grdcal.Rows[g].FindControl("txtperunitname");
                            TextBox txtqlifie1 = (TextBox)grdcal.Rows[g].FindControl("txtqlifie");
                            TextBox txtamt1 = (TextBox)grdcal.Rows[g].FindControl("txtamt");


                            TimeSpan td = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate1.Text));
                            if ((Convert.ToDateTime(lblefsdate.Text) != Convert.ToDateTime(lblefsdate1.Text)))
                            {

                                if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {
                                    //dts = (DataTable)select("Select Sum(Cast(Payabledays as int)) as Payablehours  from AttendenceEntryMaster where EmployeeID='" + ddlemp.SelectedValue + "' and Date Between '" + lblefsdate.Text + "' and '" + lblefedate.Text + "' ");
                                    //if (dts.Rows.Count > 0)
                                    //{

                                    TimeSpan td1 = Convert.ToDateTime(lblefedate.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;

                                    //}
                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) <= Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate.Text) >= Convert.ToDateTime(lblefsdate1.Text)))
                                {
                                    TimeSpan td1 = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate1.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;

                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) >= Convert.ToDateTime(lblefedate.Text)))
                                {

                                    TimeSpan td1 = Convert.ToDateTime(lblefedate.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;


                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) >= Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {

                                    TimeSpan td1 = Convert.ToDateTime(lblefedate.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;


                                }


                            }
                            else if ((Convert.ToDateTime(lblefedate.Text) != Convert.ToDateTime(lblefedate1.Text)))
                            {
                                if ((Convert.ToDateTime(lblefsdate.Text) > Convert.ToDateTime(lblefsdate1.Text)) && (Convert.ToDateTime(lblefedate1.Text) > Convert.ToDateTime(lblefedate.Text)))
                                {
                                    TimeSpan td1 = Convert.ToDateTime(lblefedate.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;
                                }
                                else if ((Convert.ToDateTime(lblefsdate.Text) <= Convert.ToDateTime(lblefedate1.Text)) && (Convert.ToDateTime(lblefedate.Text) >= Convert.ToDateTime(lblefedate.Text)))
                                {
                                    TimeSpan td1 = Convert.ToDateTime(lblefedate1.Text).Subtract(Convert.ToDateTime(lblefsdate.Text));
                                    couday = Convert.ToDouble(td1.TotalDays);
                                    couday += 1;
                                    countdif = Convert.ToDouble(td.TotalDays);

                                    countdif += 1;
                                    totho = (((Convert.ToDouble(txtqlifie1.Text) * couday) / (countdif)) * Convert.ToDouble(txtrate1.Text));
                                    totho = totho * (Convert.ToDouble(txtperc.Text)) / 100;
                                    tothr += Math.Round(totho, 4); ;

                                }




                            }
                            else
                            {
                                totho = (((Convert.ToDouble(txtrate1.Text) * Convert.ToDouble(txtqlifie1.Text)) / 100));
                                tothr += Math.Round(totho, 4); ;

                            }
                            txtamount.Text = tothr.ToString();
                            txttotal.Text = txtamount.Text;
                        }


                        totinc = totinc + Convert.ToDouble(txtamount.Text);
                        j += 1;

                    }

                }
                txttotincome.Text = totinc.ToString();
                lbltotremunration.Text = totinc.ToString();
            }

        }

    }

    protected void calded()
    {
        foreach (GridViewRow dtp in grdded.Rows)
        {

        }
    }
    protected void ddlpayperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblpayperiodtypelbl.Text = "";
        if (ddlpayperiod.SelectedIndex > 0)
        {
            lblpayperiodtypelbl.Text = ddlpayperiod.SelectedItem.Text;
        }

        lblmccc.Visible = false;
        btnSuball.Visible = false;
        pnlinsertdata.Visible = true;
        hidesalesname = "";
        hideotherrem = "";
        hide1tded = "";
        hide2ded = "";
        hideotherded = "";
        Label1.Text = "";
        lblmsg.Text = "";
        hide3ded = "";

        lblBusiness.Text = "Business : " + ddlwarehouse.SelectedItem.Text;
        lblbusna.Text = "Business : " + ddlwarehouse.SelectedItem.Text;
        lblpaypt.Text = "Period Type : " + ddlperiodtype.SelectedItem.Text;
        lblpayt.Text = " Period Type: " + ddlperiodtype.SelectedItem.Text;
        lblpayper.Text = "Payperiod : " + ddlpayperiod.SelectedItem.Text;
        lblpayperiod.Text = "Payperiod : " + ddlpayperiod.SelectedItem.Text;
        string[] separator1 = new string[] { " : " };

        string[] strSplitArr1 = ddlpayperiod.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
        if (strSplitArr1.Length > 1)
        {
            sdate = strSplitArr1[1].ToString();
            edate = strSplitArr1[2].ToString();
        }
        else
        {
            edate = DateTime.Now.AddDays(-1).ToShortDateString();
        }
        if (Convert.ToDateTime(edate) < DateTime.Now)
        {
            string Perame = "";
            if (lbltempnotallempstore1.Text.Length > 0)
            {
                Perame = " and EmployeeMasterID in(" + lbltempnotallempstore1.Text+ ")";
                lbltempnotallempstore1.Text = "";
                lbltempnotallempstore.Text = "";
            }
            DataTable dt11 = new DataTable();
            dt11 = (DataTable)select(" Select EmployeeMasterID,EmployeeName from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID inner join payperiodtype on payperiodtype.Id=EmployeePayrollMaster.PayPeriodMasterId  inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID " +
             " where  EmployeePayrollMaster.Whid='" + ddlwarehouse.SelectedValue + "' and payperiodMaster.Id='" + ddlpayperiod.SelectedValue + "' and EmployeeMasterID Not in(Select EmployeeId from SalaryMaster where payperiodtypeId='" + ddlpayperiod.SelectedValue + "') " + Perame);
            if (dt11.Rows.Count > 0)
            {
                grdcal.DataSource = null;
                grdcal.DataBind();
                ViewState["TempDataTable"] = null;
                txttotincome.Text = "0";
                lbltotremunration.Text = "0";
                lbltotsales.Text = "0";
                lbltotremperc.Text = "0";
                grdded.DataSource = null;
                grdded.DataBind();
                ViewState["Tempded"] = null;
                txttotded.Text = "0";
                txtnettotal.Text = "0";
                ddlemp.DataSource = dt11;
                ddlemp.DataTextField = "EmployeeName";
                ddlemp.DataValueField = "EmployeeMasterID";
                ddlemp.DataBind();

            }
            else
            {
                if (ddlpayperiod.SelectedIndex > 0)
                {
                    lblmsg.Text = "No employee exists for the selected pay period.";
                }
                ddlemp.Items.Clear();

            }
            ddlemp.Items.Insert(0, "Select");
            ddlemp.Items[0].Value = "0";

            ddlemp_SelectedIndexChanged(sender, e);
        }
        else
        {
            pnlinsertdata.Visible = false;
            ddlemp.Items.Clear();
            ddlemp_SelectedIndexChanged(sender, e);
            Nullable();
            lblmsg.Text = "The selected pay period is not completed yet, so payroll can not be calculated.";
        }

    }
    protected void btncalculate_Click(object sender, EventArgs e)
    {
        // fillcalg();
        calculation();
        ViewState["TempGovt"] = null;
        grdgovded.DataSource = null;
        grdgovded.DataBind();
        //I = txtbenifitgrossamt.Text;
        filltax();
    }
    protected void calculation()
    {
        decimal totalamt = 0;
        foreach (GridViewRow grd in grdcal.Rows)
        {


            TextBox txtrate = (TextBox)grd.FindControl("txtrate");
            Label txtperunitname = (Label)grd.FindControl("txtperunitname");
            TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");
            TextBox txttotunit = (TextBox)grd.FindControl("txttotunit");

            TextBox txttotal = (TextBox)grd.FindControl("txttotal");
            if ((txtrate.Text != "" && txtrate.Text != "0") && (txtqlifie.Text != "0" && txtqlifie.Text != ""))
            {
                txttotal.Text = Math.Round((Convert.ToDecimal(txtrate.Text)) * (Convert.ToDecimal(txtqlifie.Text)), 2).ToString(); ;
            }

            totalamt += Math.Round(Convert.ToDecimal(txttotal.Text), 2);
        }
        foreach (GridViewRow grd in grddaily.Rows)
        {


            TextBox txtrate = (TextBox)grd.FindControl("txtrate");
            Label Label = (Label)grd.FindControl("txtperunitname");
            TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");
            TextBox txttotunit = (TextBox)grd.FindControl("txttotunit");

            TextBox txttotal = (TextBox)grd.FindControl("txttotal");
            if ((txtrate.Text != "" && txtrate.Text != "0") && (txtqlifie.Text != "0" && txtqlifie.Text != ""))
            {
                txttotal.Text = Math.Round((Convert.ToDecimal(txtrate.Text)) * (Convert.ToDecimal(txtqlifie.Text)), 2).ToString(); ;
            }

            totalamt += Math.Round(Convert.ToDecimal(txttotal.Text), 2);
        }
        foreach (GridViewRow grd in grdmonth.Rows)
        {

            TextBox txtcompletemonth = (TextBox)grd.FindControl("txtcompletemonth");
            TextBox txtcompletemonthamt = (TextBox)grd.FindControl("txtcompletemonthamt");

            TextBox txtrate = (TextBox)grd.FindControl("txtrate");
            Label txtperunitname = (Label)grd.FindControl("txtperunitname");
            TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");
            TextBox txttotunit = (TextBox)grd.FindControl("txttotunit");

            TextBox txttotal = (TextBox)grd.FindControl("txttotal");
            txtcompletemonthamt.Text = Convert.ToString(Math.Round((Convert.ToDecimal(txtrate.Text)) * (Convert.ToDecimal(txtcompletemonth.Text)), 2));
            if ((txtcompletemonthamt.Text != "" && txtcompletemonthamt.Text != "0") && (txtrate.Text != "" && txtrate.Text != "0") && (txtqlifie.Text != "0" && txtqlifie.Text != "") && (txttotunit.Text != "0" && txttotunit.Text != ""))
            {
                txtcompletemonthamt.Text = Convert.ToString(Math.Round((Convert.ToDecimal(txtcompletemonth.Text) * (Convert.ToDecimal(txtrate.Text))), 2));

                txttotal.Text = Convert.ToString(Math.Round((Convert.ToDecimal(txtcompletemonthamt.Text) * Convert.ToDecimal(txtqlifie.Text) / Convert.ToDecimal(txttotunit.Text)), 2));

            }

            totalamt += Math.Round(Convert.ToDecimal(txttotal.Text), 2);
        }
        lbltotremunration.Text = totalamt.ToString();
        decimal remperc = 0;
        foreach (GridViewRow grdper in grdispercentage.Rows)
        {
            TextBox PercentageOf = (TextBox)grdper.FindControl("PercentageOf");
            TextBox perof = (TextBox)grdper.FindControl("perof");
            TextBox txtbaseamt = (TextBox)grdper.FindControl("txtbaseamt");
            TextBox txtpaamt = (TextBox)grdper.FindControl("txtpaamt");
            if (grdcal.Rows.Count > 0)
            {

                foreach (GridViewRow grd in grdcal.Rows)
                {
                    TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                    TextBox txttotal = (TextBox)grd.FindControl("txttotal");
                    Label lblover = (Label)grd.FindControl("lblover");

                    if (txtremu.Text == perof.Text)
                    {
                        txtbaseamt.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txttotal.Text), 2));
                        txtpaamt.Text = Convert.ToString(Math.Round(((Convert.ToDecimal(txtbaseamt.Text) * Convert.ToDecimal(PercentageOf.Text)) / 100), 2));
                        break;
                    }
                }
            }
            if (grddaily.Rows.Count > 0)
            {

                foreach (GridViewRow grd in grddaily.Rows)
                {
                    TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                    TextBox txttotal = (TextBox)grd.FindControl("txttotal");
                    Label lblover = (Label)grd.FindControl("lblover");

                    if (txtremu.Text == perof.Text)
                    {
                        txtbaseamt.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txttotal.Text), 2));
                        txtpaamt.Text = Convert.ToString(Math.Round(((Convert.ToDecimal(txtbaseamt.Text) * Convert.ToDecimal(PercentageOf.Text)) / 100), 2));
                        break;
                    }
                }
            }
            if (grdmonth.Rows.Count > 0)
            {

                foreach (GridViewRow grd in grdmonth.Rows)
                {

                    TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                    TextBox txttotal = (TextBox)grd.FindControl("txttotal");
                    Label lblover = (Label)grd.FindControl("lblover");

                    if (txtremu.Text == perof.Text)
                    {
                        txtbaseamt.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txttotal.Text), 2));
                        txtpaamt.Text = Convert.ToString(Math.Round(((Convert.ToDecimal(txtbaseamt.Text) * Convert.ToDecimal(PercentageOf.Text)) / 100), 2));
                        break;
                    }
                }
            }
            totalamt += Math.Round(Convert.ToDecimal(txtpaamt.Text), 2);
            remperc = Math.Round(Convert.ToDecimal(txtpaamt.Text), 2);
        }
        lbltotremperc.Text = remperc.ToString();
        decimal remsales = 0;
        foreach (GridViewRow grdper in grdsales.Rows)
        {
            TextBox PercentageOf = (TextBox)grdper.FindControl("PercentageOf");
            TextBox perof = (TextBox)grdper.FindControl("perof");
            TextBox txtbaseamt = (TextBox)grdper.FindControl("txtbaseamt");
            TextBox txtpaamt = (TextBox)grdper.FindControl("txtpaamt");

            txtpaamt.Text = Convert.ToString(Math.Round((Convert.ToDecimal(txtbaseamt.Text) * Convert.ToDecimal(PercentageOf.Text)) / 100, 2));

            totalamt += Math.Round(Convert.ToDecimal(txtpaamt.Text), 2);
            remsales = Math.Round(Convert.ToDecimal(txtpaamt.Text), 2);
        }
        lbltotsales.Text = remsales.ToString();
        foreach (GridViewRow grdleavee in grdleaveencash.Rows)
        {
            TextBox txtnoofleave = (TextBox)grdleavee.FindControl("txtnoofleave");
            TextBox txtleaverate = (TextBox)grdleavee.FindControl("txtleaverate");

            TextBox txtpaamt = (TextBox)grdleavee.FindControl("txtpaamt");

            txtpaamt.Text = Convert.ToString(Math.Round((Convert.ToDecimal(txtnoofleave.Text) * Convert.ToDecimal(txtleaverate.Text)), 2));

            totalamt += Math.Round(Convert.ToDecimal(txtpaamt.Text), 2);
        }


        txttotincome.Text = totalamt.ToString();
        txtbenifitgrossamt.Text = totalamt.ToString();
        foreach (GridViewRow grd in grdtaxbenifit.Rows)
        {
            Label txtamt = (Label)grd.FindControl("txtamt");
            txtbenifitgrossamt.Text = (Convert.ToDecimal(txtbenifitgrossamt.Text) + Convert.ToDecimal(txtamt.Text)).ToString();
        }
        decimal totdid = 0;
        foreach (GridViewRow grd in grdded.Rows)
        {



            TextBox FixedAmount = (TextBox)grd.FindControl("FixedAmount");
            TextBox PercentageOf = (TextBox)grd.FindControl("PercentageOf");
            TextBox perof = (TextBox)grd.FindControl("perof");

            TextBox Totamt = (TextBox)grd.FindControl("Totamt");
            if (Totamt.Text == "0" && PercentageOf.Text == "0")
            {
                if (Convert.ToString(FixedAmount.Text) != "0")
                {
                    Totamt.Text = Math.Round(Convert.ToDecimal(FixedAmount.Text), 2).ToString();

                }
                else
                {
                    FixedAmount.Text = "0";
                }
            }
            else
            {
                if (PercentageOf.Text == "0")
                {
                    Totamt.Text = Math.Round(Convert.ToDecimal(FixedAmount.Text), 2).ToString();
                }
            }
            TextBox txtremu = new TextBox();
            TextBox txttotal = new TextBox();
            if (grdcal.Rows.Count > 0)
            {

                Label lblover = (Label)grd.FindControl("lblover");
                if (lblover != null)
                {
                    if (lblover.Text != "Over")
                    {
                        txtremu = (TextBox)grdcal.Rows[0].FindControl("txtremu");
                        txttotal = (TextBox)grdcal.Rows[0].FindControl("txttotal");

                    }
                }
            }
            if (grddaily.Rows.Count > 0)
            {


                txtremu = (TextBox)grddaily.Rows[0].FindControl("txtremu");
                txttotal = (TextBox)grddaily.Rows[0].FindControl("txttotal");


            }
            if (grdmonth.Rows.Count > 0)
            {


                txtremu = (TextBox)grdmonth.Rows[0].FindControl("txtremu");
                txttotal = (TextBox)grdmonth.Rows[0].FindControl("txttotal");


            }


            if (Totamt.Text == "0" && FixedAmount.Text == "0")
            {
                if (Convert.ToString(PercentageOf.Text) != "")
                {
                    if (txttotal.Text.Length == 0)
                    {
                        txttotal.Text = "0";
                    }
                    perof.Text = txtremu.Text;
                    decimal ad = Convert.ToDecimal(PercentageOf.Text);
                    Totamt.Text = Math.Round(((Convert.ToDecimal(txttotal.Text) * ad) / 100), 2).ToString();
                }
                else
                {
                    PercentageOf.Text = "0";

                    perof.Text = "-";
                }
            }
            else
            {
                if (FixedAmount.Text == "0")
                {
                    perof.Text = txtremu.Text;
                    decimal ad = Convert.ToDecimal(PercentageOf.Text);
                    Totamt.Text = Math.Round(((Convert.ToDecimal(txttotal.Text) * ad) / 100), 2).ToString();
                }
            }
            totdid += Math.Round(Convert.ToDecimal(Totamt.Text), 2);

        }

        txttotded.Text = Math.Round(totdid, 2).ToString();
        txtnettotal.Text = (Convert.ToDecimal(txttotincome.Text) - Convert.ToDecimal(txttotded.Text)).ToString();

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        MasterSubmit("");
    }
    protected void MasterSubmit(string index)
    {

        SqlCommand cmdse = new SqlCommand();
        DataTable dt = new DataTable();
        cmdse.CommandText = "SelectSalaryMaster";
        cmdse.Parameters.Add(new SqlParameter("@payperiodtypeId", SqlDbType.NVarChar));
        cmdse.Parameters["@payperiodtypeId"].Value = ddlpayperiod.SelectedValue;

        cmdse.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.NVarChar));
        cmdse.Parameters["@EmployeeId"].Value = ddlemp.SelectedValue; // CompanyLoginId;
        cmdse.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
        cmdse.Parameters["@Whid"].Value = ddlwarehouse.SelectedValue; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmdse);

        if (dt.Rows.Count <= 0)
        {
            int k = 0;
            DataTable ds9 = MainAcocount.SelectEntrynumber("1", ddlwarehouse.SelectedValue);

            if (ds9.Rows.Count > 0)
            {
                if (ds9.Rows[0]["Id"].ToString() != "")
                {
                    k = Convert.ToInt32(ds9.Rows[0]["Id"]) + 1;
                }
                else
                {
                    k = 1;

                }
            }
            else
            {
                k = 1;
            }
            // int id = 0;
            int idd = MainAcocount.InsertTransactionMaster(Convert.ToDateTime(DateTime.Now.ToString()), k.ToString(), "3", Convert.ToInt32(Session["userid"]), Convert.ToDecimal(txttotincome.Text), ddlwarehouse.SelectedValue);
            if (idd > 0)
            {
                decimal totded = 0;
                if (txttotded.Text.Length > 0)
                {
                    totded = Convert.ToDecimal(txttotded.Text);
                }
                if (txtgovtottax.Text.Length > 0)
                {
                    totded += Convert.ToDecimal(txtgovtottax.Text);
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InsertSalaryMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@payperiodtypeId", SqlDbType.Int));
                cmd.Parameters["@payperiodtypeId"].Value = ddlpayperiod.SelectedValue;
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
                cmd.Parameters["@EmployeeId"].Value = ddlemp.SelectedValue;
                cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
                cmd.Parameters["@Whid"].Value = ddlwarehouse.SelectedValue;
                cmd.Parameters.Add(new SqlParameter("@TotalIncome", SqlDbType.NVarChar));
                cmd.Parameters["@TotalIncome"].Value = txttotincome.Text;
                cmd.Parameters.Add(new SqlParameter("@TotalDeduction", SqlDbType.NVarChar));
                cmd.Parameters["@TotalDeduction"].Value = totded;
                cmd.Parameters.Add(new SqlParameter("@NetTotal", SqlDbType.NVarChar));
                cmd.Parameters["@NetTotal"].Value = txtnettotal.Text;

                cmd.Parameters.Add(new SqlParameter("@GrossRemu", SqlDbType.NVarChar));
                cmd.Parameters["@GrossRemu"].Value = txtbenifitgrossamt.Text;

                cmd.Parameters.Add(new SqlParameter("@TaxYearId", SqlDbType.NVarChar));
                cmd.Parameters["@TaxYearId"].Value = yearId;

                cmd.Parameters.Add(new SqlParameter("@NoofPayperiodinYear", SqlDbType.NVarChar));
                cmd.Parameters["@NoofPayperiodinYear"].Value = P;

                cmd.Parameters.Add(new SqlParameter("@NonGovdedamt", SqlDbType.NVarChar));
                cmd.Parameters["@NonGovdedamt"].Value = txttotded.Text;

                cmd.Parameters.Add(new SqlParameter("@Govdedamt", SqlDbType.NVarChar));
                cmd.Parameters["@Govdedamt"].Value = txtgovtottax.Text;

                cmd.Parameters.Add(new SqlParameter("@RemTotal", SqlDbType.NVarChar));
                cmd.Parameters["@RemTotal"].Value = lbltotremunration.Text;

                cmd.Parameters.Add(new SqlParameter("@RemPerc", SqlDbType.NVarChar));
                cmd.Parameters["@RemPerc"].Value = lbltotremperc.Text;

                cmd.Parameters.Add(new SqlParameter("@RemSales", SqlDbType.NVarChar));
                cmd.Parameters["@RemSales"].Value = lbltotsales.Text;



                cmd.Parameters.Add(new SqlParameter("@Tid", SqlDbType.NVarChar));
                cmd.Parameters["@Tid"].Value = idd;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
                Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
                result = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                if (result > 0)
                {
                    foreach (GridViewRow grd in grdcal.Rows)
                    {

                        int Id = Convert.ToInt32(grdcal.DataKeys[grd.RowIndex].Value);
                        TextBox txtrate = (TextBox)grd.FindControl("txtrate");
                        Label txtperunitname = (Label)grd.FindControl("txtperunitname");
                        TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");
                        TextBox txttotunit = (TextBox)grd.FindControl("txttotunit");

                        //  int remId = Convert.ToInt32(grdcal.DataKeys[grd.RowIndex].Value);


                        TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                        DropDownList ddldailyremu = (DropDownList)grd.FindControl("ddldailyremu");
                        Label lblover = (Label)grd.FindControl("lblover");
                        TextBox txttotal = (TextBox)grd.FindControl("txttotal");
                        if (ddldailyremu.Visible == true)
                        {
                            Id = Convert.ToInt32(ddldailyremu.SelectedValue);
                            txtremu.Text = ddldailyremu.SelectedItem.Text;
                        }
                        if (ddldailyremu.Visible == false)
                        {
                            if (lblover.Text != "Over")
                            {
                                DataTable dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.Id where RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                                if (dtreaccid.Rows.Count == 0)
                                {
                                    dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                                }
                                if (dtreaccid.Rows.Count > 0)
                                {

                                    int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(dtreaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txttotal.Text), 0, idd, Convert.ToString("Salary Account"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                                }
                            }

                            else if (lblover.Text == "Over")
                            {
                                DataTable dtoverccid = select("Select AccountMaster.AccountId from AccountMaster Where Whid='" + ddlwarehouse.SelectedValue + "' and AccountName='Office Salary-Overtime'");

                                if (dtoverccid.Rows.Count > 0)
                                {
                                    int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(dtoverccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txttotal.Text), 0, idd, Convert.ToString("Salary OverTime"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                                }

                            }
                        }
                        else
                        {
                            DataTable dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.Id where RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                            if (dtreaccid.Rows.Count == 0)
                            {
                                dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                            }
                            if (dtreaccid.Rows.Count > 0)
                            {

                                int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(dtreaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txttotal.Text), 0, idd, Convert.ToString("Extra Account"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                            }
                        }

                        SqlCommand cmdrem = new SqlCommand();
                        cmdrem.CommandText = "InsertSalaryRemuneration";
                        cmdrem.CommandType = CommandType.StoredProcedure;

                        cmdrem.Parameters.Add(new SqlParameter("@SalaryMasterId", SqlDbType.Int));
                        cmdrem.Parameters["@SalaryMasterId"].Value = result;
                        cmdrem.Parameters.Add(new SqlParameter("@RemunerationId", SqlDbType.Int));
                        cmdrem.Parameters["@RemunerationId"].Value = Id;
                        cmdrem.Parameters.Add(new SqlParameter("@Rate", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Rate"].Value = txtrate.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@perunitname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@perunitname"].Value = txtperunitname.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@totalunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@totalunit"].Value = txttotunit.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@Actualpayunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Actualpayunit"].Value = txtqlifie.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@Remunerationname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Remunerationname"].Value = txtremu.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@remamt", SqlDbType.NVarChar));
                        cmdrem.Parameters["@remamt"].Value = txttotal.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@Totalcomunitmonth", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Totalcomunitmonth"].Value = "0";

                        cmdrem.Parameters.Add(new SqlParameter("@maxcompete", SqlDbType.NVarChar));
                        cmdrem.Parameters["@maxcompete"].Value = "0";
                        Int32 resultrem = DatabaseCls1.ExecuteNonQueryep(cmdrem);
                    }
                    foreach (GridViewRow grd in grddaily.Rows)
                    {

                        int Id = Convert.ToInt32(grddaily.DataKeys[grd.RowIndex].Value);
                        TextBox txtrate = (TextBox)grd.FindControl("txtrate");
                        Label txtperunitname = (Label)grd.FindControl("txtperunitname");
                        TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");
                        TextBox txttotunit = (TextBox)grd.FindControl("txttotunit");
                        TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                        TextBox txttotal = (TextBox)grd.FindControl("txttotal");


                        DropDownList ddldailyremu = (DropDownList)grd.FindControl("ddldailyremu");
                        if (ddldailyremu.Visible == true)
                        {
                            Id = Convert.ToInt32(ddldailyremu.SelectedValue);
                            txtremu.Text = ddldailyremu.SelectedItem.Text;
                        }

                        if (ddldailyremu.Visible == false)
                        {
                            DataTable dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.Id where RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                            if (dtreaccid.Rows.Count == 0)
                            {
                                dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                            }
                            if (dtreaccid.Rows.Count > 0)
                            {

                                int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(dtreaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txttotal.Text), 0, idd, Convert.ToString("Salary Account"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                            }
                        }
                        else
                        {
                            DataTable dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.Id where RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                            if (dtreaccid.Rows.Count == 0)
                            {
                                dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");

                            }
                            if (dtreaccid.Rows.Count > 0)
                            {

                                int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(dtreaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txttotal.Text), 0, idd, Convert.ToString("Extra Account"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                            }
                        }


                        SqlCommand cmdrem = new SqlCommand();
                        cmdrem.CommandText = "InsertSalaryRemuneration";
                        cmdrem.CommandType = CommandType.StoredProcedure;

                        cmdrem.Parameters.Add(new SqlParameter("@SalaryMasterId", SqlDbType.Int));
                        cmdrem.Parameters["@SalaryMasterId"].Value = result;
                        cmdrem.Parameters.Add(new SqlParameter("@RemunerationId", SqlDbType.Int));
                        cmdrem.Parameters["@RemunerationId"].Value = Id;
                        cmdrem.Parameters.Add(new SqlParameter("@Rate", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Rate"].Value = txtrate.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@perunitname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@perunitname"].Value = txtperunitname.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@totalunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@totalunit"].Value = txttotunit.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@Actualpayunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Actualpayunit"].Value = txtqlifie.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@Remunerationname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Remunerationname"].Value = txtremu.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@remamt", SqlDbType.NVarChar));
                        cmdrem.Parameters["@remamt"].Value = txttotal.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@Totalcomunitmonth", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Totalcomunitmonth"].Value = "0";

                        cmdrem.Parameters.Add(new SqlParameter("@maxcompete", SqlDbType.NVarChar));
                        cmdrem.Parameters["@maxcompete"].Value = "0";
                        Int32 resultrem = DatabaseCls1.ExecuteNonQueryep(cmdrem);
                    }
                    foreach (GridViewRow grd in grdmonth.Rows)
                    {

                        int Id = Convert.ToInt32(grdmonth.DataKeys[grd.RowIndex].Value);
                        TextBox txtrate = (TextBox)grd.FindControl("txtrate");
                        Label txtperunitname = (Label)grd.FindControl("txtperunitname");
                        TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");
                        TextBox txttotunit = (TextBox)grd.FindControl("txttotunit");
                        TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                        TextBox txttotal = (TextBox)grd.FindControl("txttotal");

                        TextBox txtcompletemonth = (TextBox)grd.FindControl("txtcompletemonth");
                        TextBox txtcompletemonthamt = (TextBox)grd.FindControl("txtcompletemonthamt");

                        DropDownList ddldailyremu = (DropDownList)grd.FindControl("ddldailyremu");
                        if (ddldailyremu.Visible == true)
                        {
                            Id = Convert.ToInt32(ddldailyremu.SelectedValue);
                            txtremu.Text = ddldailyremu.SelectedItem.Text;
                        }


                        if (ddldailyremu.Visible == false)
                        {
                            DataTable dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.Id where RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                            if (dtreaccid.Rows.Count == 0)
                            {
                                dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");


                            }
                            if (dtreaccid.Rows.Count > 0)
                            {

                                int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(dtreaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txttotal.Text), 0, idd, Convert.ToString("Salary Account"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                            }
                        }
                        else
                        {
                            DataTable dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.Id where RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                            if (dtreaccid.Rows.Count == 0)
                            {
                                dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");


                            }
                            if (dtreaccid.Rows.Count > 0)
                            {

                                int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(dtreaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txttotal.Text), 0, idd, Convert.ToString("Extra Account"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                            }
                        }



                        SqlCommand cmdrem = new SqlCommand();
                        cmdrem.CommandText = "InsertSalaryRemuneration";
                        cmdrem.CommandType = CommandType.StoredProcedure;

                        cmdrem.Parameters.Add(new SqlParameter("@SalaryMasterId", SqlDbType.Int));
                        cmdrem.Parameters["@SalaryMasterId"].Value = result;
                        cmdrem.Parameters.Add(new SqlParameter("@RemunerationId", SqlDbType.Int));
                        cmdrem.Parameters["@RemunerationId"].Value = Id;
                        cmdrem.Parameters.Add(new SqlParameter("@Rate", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Rate"].Value = txtrate.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@perunitname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@perunitname"].Value = txtperunitname.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@totalunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@totalunit"].Value = txttotunit.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@Actualpayunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Actualpayunit"].Value = txtqlifie.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@Remunerationname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Remunerationname"].Value = txtremu.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@remamt", SqlDbType.NVarChar));
                        cmdrem.Parameters["@remamt"].Value = txttotal.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@Totalcomunitmonth", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Totalcomunitmonth"].Value = txtcompletemonth.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@maxcompete", SqlDbType.NVarChar));
                        cmdrem.Parameters["@maxcompete"].Value = txtcompletemonthamt.Text;
                        Int32 resultrem = DatabaseCls1.ExecuteNonQueryep(cmdrem);
                    }


                    foreach (GridViewRow grd in grdispercentage.Rows)
                    {

                        int Id = Convert.ToInt32(grdispercentage.DataKeys[grd.RowIndex].Value);
                        TextBox PercentageOf = (TextBox)grd.FindControl("PercentageOf");

                        TextBox txtbaseamt = (TextBox)grd.FindControl("txtbaseamt");

                        TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                        TextBox txtpaamt = (TextBox)grd.FindControl("txtpaamt");


                        TextBox perof = (TextBox)grd.FindControl("perof");

                        DataTable dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.Id where RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                        if (dtreaccid.Rows.Count == 0)
                        {
                            dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");


                        }
                        if (dtreaccid.Rows.Count > 0)
                        {

                            int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(dtreaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txtpaamt.Text), 0, idd, Convert.ToString("Remunaration By Percentage"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                        }
                        SqlCommand cmdrem = new SqlCommand();
                        cmdrem.CommandText = "InsertSalaryRemuneration";
                        cmdrem.CommandType = CommandType.StoredProcedure;

                        cmdrem.Parameters.Add(new SqlParameter("@SalaryMasterId", SqlDbType.Int));
                        cmdrem.Parameters["@SalaryMasterId"].Value = result;
                        cmdrem.Parameters.Add(new SqlParameter("@RemunerationId", SqlDbType.Int));
                        cmdrem.Parameters["@RemunerationId"].Value = Id;
                        cmdrem.Parameters.Add(new SqlParameter("@Rate", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Rate"].Value = PercentageOf.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@perunitname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@perunitname"].Value = "Is Percentage";

                        cmdrem.Parameters.Add(new SqlParameter("@totalunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@totalunit"].Value = "0";
                        cmdrem.Parameters.Add(new SqlParameter("@Actualpayunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Actualpayunit"].Value = txtbaseamt.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@Remunerationname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Remunerationname"].Value = txtremu.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@remamt", SqlDbType.NVarChar));
                        cmdrem.Parameters["@remamt"].Value = txtpaamt.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@Totalcomunitmonth", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Totalcomunitmonth"].Value = perof.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@maxcompete", SqlDbType.NVarChar));
                        cmdrem.Parameters["@maxcompete"].Value = "0";
                        Int32 resultrem = DatabaseCls1.ExecuteNonQueryep(cmdrem);
                    }
                    foreach (GridViewRow grd in grdsales.Rows)
                    {

                        int Id = Convert.ToInt32(grdsales.DataKeys[grd.RowIndex].Value);
                        TextBox PercentageOf = (TextBox)grd.FindControl("PercentageOf");

                        TextBox txtbaseamt = (TextBox)grd.FindControl("txtbaseamt");

                        TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                        TextBox txtpaamt = (TextBox)grd.FindControl("txtpaamt");


                        TextBox perof = (TextBox)grd.FindControl("perof");

                        DataTable dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.Id where RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                        if (dtreaccid.Rows.Count == 0)
                        {
                            dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");


                        }
                        if (dtreaccid.Rows.Count > 0)
                        {

                            int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(dtreaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txtpaamt.Text), 0, idd, Convert.ToString("Remunaration By Percentage of sales"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                        }
                        SqlCommand cmdrem = new SqlCommand();
                        cmdrem.CommandText = "InsertSalaryRemuneration";
                        cmdrem.CommandType = CommandType.StoredProcedure;

                        cmdrem.Parameters.Add(new SqlParameter("@SalaryMasterId", SqlDbType.Int));
                        cmdrem.Parameters["@SalaryMasterId"].Value = result;
                        cmdrem.Parameters.Add(new SqlParameter("@RemunerationId", SqlDbType.Int));
                        cmdrem.Parameters["@RemunerationId"].Value = Id;
                        cmdrem.Parameters.Add(new SqlParameter("@Rate", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Rate"].Value = PercentageOf.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@perunitname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@perunitname"].Value = "Is Percentage of Sales";

                        cmdrem.Parameters.Add(new SqlParameter("@totalunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@totalunit"].Value = "0";
                        cmdrem.Parameters.Add(new SqlParameter("@Actualpayunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Actualpayunit"].Value = txtbaseamt.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@Remunerationname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Remunerationname"].Value = txtremu.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@remamt", SqlDbType.NVarChar));
                        cmdrem.Parameters["@remamt"].Value = txtpaamt.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@Totalcomunitmonth", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Totalcomunitmonth"].Value = perof.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@maxcompete", SqlDbType.NVarChar));
                        cmdrem.Parameters["@maxcompete"].Value = "0";
                        Int32 resultrem = DatabaseCls1.ExecuteNonQueryep(cmdrem);
                    }
                    foreach (GridViewRow grd in grdleaveencash.Rows)
                    {

                        int Id = Convert.ToInt32(grdleaveencash.DataKeys[grd.RowIndex].Value);

                        TextBox txtleaverate = (TextBox)grd.FindControl("txtleaverate");
                        TextBox txtnoofleave = (TextBox)grd.FindControl("txtnoofleave");

                        Label txtremu = (Label)grd.FindControl("lblleavet");
                        TextBox txtpaamt = (TextBox)grd.FindControl("txtpaamt");


                        DataTable dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.Id where RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");
                        if (dtreaccid.Rows.Count == 0)
                        {
                            dtreaccid = select("Select AccountId from AccountMaster inner join  RemunerationMaster on  RemunerationMaster.AccountMasterId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Whid='" + ddlwarehouse.SelectedValue + "' and RemunerationMaster.Id='" + Id + "'");


                        }
                        if (dtreaccid.Rows.Count > 0)
                        {

                            int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(dtreaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txtpaamt.Text), 0, idd, Convert.ToString("Leave Encash"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                        }
                        SqlCommand cmdrem = new SqlCommand();
                        cmdrem.CommandText = "InsertSalaryRemuneration";
                        cmdrem.CommandType = CommandType.StoredProcedure;

                        cmdrem.Parameters.Add(new SqlParameter("@SalaryMasterId", SqlDbType.Int));
                        cmdrem.Parameters["@SalaryMasterId"].Value = result;
                        cmdrem.Parameters.Add(new SqlParameter("@RemunerationId", SqlDbType.Int));
                        cmdrem.Parameters["@RemunerationId"].Value = Id;
                        cmdrem.Parameters.Add(new SqlParameter("@Rate", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Rate"].Value = txtleaverate.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@perunitname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@perunitname"].Value = "Leave Encash";

                        cmdrem.Parameters.Add(new SqlParameter("@totalunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@totalunit"].Value = txtnoofleave.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@Actualpayunit", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Actualpayunit"].Value = txtnoofleave.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@Remunerationname", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Remunerationname"].Value = txtremu.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@remamt", SqlDbType.NVarChar));
                        cmdrem.Parameters["@remamt"].Value = txtpaamt.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@Totalcomunitmonth", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Totalcomunitmonth"].Value = "0";

                        cmdrem.Parameters.Add(new SqlParameter("@maxcompete", SqlDbType.NVarChar));
                        cmdrem.Parameters["@maxcompete"].Value = "0";
                        Int32 resultrem = DatabaseCls1.ExecuteNonQueryep(cmdrem);
                    }
                    foreach (GridViewRow grd in grdded.Rows)
                    {


                        int Id = Convert.ToInt32(grdded.DataKeys[grd.RowIndex].Value);
                        TextBox FixedAmount = (TextBox)grd.FindControl("FixedAmount");
                        TextBox PercentageOf = (TextBox)grd.FindControl("PercentageOf");
                        TextBox perof = (TextBox)grd.FindControl("perof");

                        TextBox Totamt = (TextBox)grd.FindControl("Totamt");
                        TextBox txtdedremname = (TextBox)grd.FindControl("txtdedremname");

                        Label lblrrsug = (Label)grd.FindControl("lblrrsug");

                        int dedId = Convert.ToInt32(grdded.DataKeys[0].Value);
                        DataTable dtdedaccid = new DataTable();
                        string dedTypemsg = "";
                        if (lblrrsug.Text == "1")
                        {
                            dedTypemsg = "RRSP Ded";
                            dtdedaccid = select("Select AccountId from AccountMaster inner join WithholdingRequestMasterTbl on WithholdingRequestMasterTbl.RRSPAc=AccountMaster.Id where WithholdingRequestMasterTbl.Id='" + dedId + "'");

                        }
                        else if (lblrrsug.Text == "2")
                        {
                            dedTypemsg = "Union Due Ded";
                            dtdedaccid = select("Select AccountId from AccountMaster inner join WithholdingRequestMasterTbl on WithholdingRequestMasterTbl.UnionDueAc=AccountMaster.Id where WithholdingRequestMasterTbl.Id='" + dedId + "'");

                        }
                        else
                        {
                            dedTypemsg = "Salary Ded";
                            dtdedaccid = select("Select AccountId from AccountMaster inner join payrolldeductionotherstbl on payrolldeductionotherstbl.MappedAccountId=AccountMaster.Id where payrolldeductionotherstbl.Whid='" + ddlwarehouse.SelectedValue + "' and payrolldeductionotherstbl.Id='" + dedId + "'");


                            if (dtdedaccid.Rows.Count == 0)
                            {
                                dtdedaccid = select("Select AccountId from AccountMaster inner join payrolldeductionotherstbl on payrolldeductionotherstbl.MappedAccountId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and payrolldeductionotherstbl.Whid='" + ddlwarehouse.SelectedValue + "' and payrolldeductionotherstbl.Id='" + dedId + "'");

                            }
                        }
                        if (dtdedaccid.Rows.Count > 0)
                        {
                            // TextBox Totamt = (TextBox)grdded.Rows[0].FindControl("Totamt");
                            int tdid = MainAcocount.Sp_Insert_Tranction_Details1(0, Convert.ToInt32(dtdedaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(Totamt.Text), idd, dedTypemsg, Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                        }


                        SqlCommand cmdrem = new SqlCommand();
                        cmdrem.CommandText = "InsertSalaryDeduction";
                        cmdrem.CommandType = CommandType.StoredProcedure;

                        cmdrem.Parameters.Add(new SqlParameter("@SalaryMasterId", SqlDbType.Int));
                        cmdrem.Parameters["@SalaryMasterId"].Value = result;
                        cmdrem.Parameters.Add(new SqlParameter("@DeductionId", SqlDbType.Int));
                        cmdrem.Parameters["@DeductionId"].Value = Id;
                        cmdrem.Parameters.Add(new SqlParameter("@Deductionamt", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Deductionamt"].Value = FixedAmount.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@deductionper", SqlDbType.NVarChar));
                        cmdrem.Parameters["@deductionper"].Value = PercentageOf.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@deductionperof", SqlDbType.NVarChar));
                        cmdrem.Parameters["@deductionperof"].Value = perof.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@DeductionName", SqlDbType.NVarChar));
                        cmdrem.Parameters["@DeductionName"].Value = txtdedremname.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@dedamt", SqlDbType.NVarChar));
                        cmdrem.Parameters["@dedamt"].Value = Totamt.Text;


                        cmdrem.Parameters.Add(new SqlParameter("@RUDed", SqlDbType.NVarChar));
                        cmdrem.Parameters["@RUDed"].Value = lblrrsug.Text;

                        Int32 resultrem = DatabaseCls1.ExecuteNonQueryep(cmdrem);
                    }

                    foreach (GridViewRow grd in grdgovded.Rows)
                    {
                        int Id = Convert.ToInt32(grdgovded.DataKeys[grd.RowIndex].Value);
                        Label txtgovtamt = (Label)grd.FindControl("txtgovtamt");
                        Label lblcracc = (Label)grd.FindControl("lblcracc");
                        SqlCommand cmdrem = new SqlCommand();
                        cmdrem.CommandText = "InsertSalaryGovtDeduction";
                        cmdrem.CommandType = CommandType.StoredProcedure;

                        cmdrem.Parameters.Add(new SqlParameter("@SalaryMasterId", SqlDbType.Int));
                        cmdrem.Parameters["@SalaryMasterId"].Value = result;
                        cmdrem.Parameters.Add(new SqlParameter("@PayrollTaxId", SqlDbType.Int));
                        cmdrem.Parameters["@PayrollTaxId"].Value = Id;
                        cmdrem.Parameters.Add(new SqlParameter("@Employeeid", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Employeeid"].Value = ddlemp.SelectedValue;
                        cmdrem.Parameters.Add(new SqlParameter("@PayperiodId", SqlDbType.NVarChar));
                        cmdrem.Parameters["@PayperiodId"].Value = ddlpayperiod.SelectedValue;

                        cmdrem.Parameters.Add(new SqlParameter("@TaxYear", SqlDbType.NVarChar));
                        cmdrem.Parameters["@TaxYear"].Value = yearId;
                        cmdrem.Parameters.Add(new SqlParameter("@Totdedamt", SqlDbType.NVarChar));
                        cmdrem.Parameters["@Totdedamt"].Value = txtgovtamt.Text;

                        Int32 resultrem = DatabaseCls1.ExecuteNonQueryep(cmdrem);
                        int tdid = MainAcocount.Sp_Insert_Tranction_Details1(0, Convert.ToInt32(lblcracc.Text), 0, Convert.ToDecimal(txtgovtamt.Text), idd, txtgovtamt.Text + "Ded", Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                    }
                    DataTable dtempaccid = select("Select AccountMaster.AccountId from AccountMaster inner join EmployeeMaster on EmployeeMaster.AccountId=AccountMaster.Id where EmployeeMaster.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.EmployeeMasterID='" + ddlemp.SelectedValue + "'");
                    if (dtempaccid.Rows.Count == 0)
                    {
                        dtempaccid = select("Select AccountMaster.AccountId from AccountMaster inner join EmployeeMaster on EmployeeMaster.AccountId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.EmployeeMasterID='" + ddlemp.SelectedValue + "'");

                    }
                    if (dtempaccid.Rows.Count > 0)
                    {
                        //  TextBox Totamt = (TextBox)grdded.Rows[0].FindControl("Totamt");
                        int tdid = MainAcocount.Sp_Insert_Tranction_Details1(0, Convert.ToInt32(dtempaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txtnettotal.Text), idd, Convert.ToString("Emp Salary"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);
                    }

                    foreach (GridViewRow grd in grdtaxbenifit.Rows)
                    {
                        int Id = Convert.ToInt32(grdtaxbenifit.DataKeys[grd.RowIndex].Value);
                        Label lbltaxben = (Label)grd.FindControl("lbltaxben");
                        Label lbltaxbn = (Label)grd.FindControl("lbltaxbn");
                        Label txtamt = (Label)grd.FindControl("txtamt");
                        SqlCommand cmdrem = new SqlCommand();
                        cmdrem.CommandText = "InsertSalaryTaxableBenifit";
                        cmdrem.CommandType = CommandType.StoredProcedure;

                        cmdrem.Parameters.Add(new SqlParameter("@SalaryMasterId", SqlDbType.Int));
                        cmdrem.Parameters["@SalaryMasterId"].Value = result;
                        cmdrem.Parameters.Add(new SqlParameter("@TaxablebanifitId", SqlDbType.Int));
                        cmdrem.Parameters["@TaxablebanifitId"].Value = Id;
                        cmdrem.Parameters.Add(new SqlParameter("@TaxableBenName", SqlDbType.NVarChar));
                        cmdrem.Parameters["@TaxableBenName"].Value = lbltaxben.Text;
                        cmdrem.Parameters.Add(new SqlParameter("@TaxbenDate", SqlDbType.NVarChar));
                        cmdrem.Parameters["@TaxbenDate"].Value = lbltaxbn.Text;

                        cmdrem.Parameters.Add(new SqlParameter("@TaxbenAmt", SqlDbType.NVarChar));
                        cmdrem.Parameters["@TaxbenAmt"].Value = txtamt.Text;

                        Int32 resultrem = DatabaseCls1.ExecuteNonQueryep(cmdrem);
                    }
                    fillRelatedAc(result.ToString(), index);
                    sendmail(ddlemp.SelectedValue);
                    if (index != "")
                    {
                        RemoveRowtempG(index);
                    }
                    else
                    {
                        index = REMOVEEMPG();
                    }
                    // FillAllEmpData(result.ToString(), index);
                    lblmsg.Text = "Record inserted successfully";
                    lblmsg.Visible = true;
                    grdcal.DataSource = null;
                    grdcal.DataBind();
                    grdded.DataSource = null;
                    grdded.DataBind();
                    txtnettotal.Text = "0";
                    txttotded.Text = "0";
                    txttotincome.Text = "0";
                    lbltotremunration.Text = "0";
                    lbltotsales.Text = "0";
                    lbltotremperc.Text = "0";
                    pnlcau.Visible = false;
                    pnlinsertdata.Visible = false;
                    // ddlemp.Items.Clear();
                    //  ddlpayperiod.SelectedIndex = 0;

                }

            }


        }
        else
        {
            lblmsg.Text = "Record already exist";
            lblmsg.Visible = true;
        }
    }
    protected void fillRelatedAc(string salId, string Index)
    {


        if (Index == "")
        {
            if (chkpaida.Checked == true)
            {
                foreach (GridViewRow item in grdallemp.Rows)
                {
                    Label lblEmployeeId = (Label)item.FindControl("lblEmployeeId");
                    if (Convert.ToString(ddlemp.SelectedValue) == Convert.ToString(lblEmployeeId.Text))
                    {
                        Index = item.RowIndex.ToString();
                        break;
                    }
                }
                if (txtrelpaidamr.Text.Length > 0 && ddlrelacc.Items.Count > 0)
                {
                    Datapaid(salId, Index, txtrelpaidamr, ddlrelacc, txtrelcheque);
                }
            }
        }
        else
        {
            
            if (grdallemp.Columns[25].Visible == true)
            {
                DropDownList ddlaccouts = (DropDownList)grdallemp.Rows[Convert.ToInt32(Index)].FindControl("ddlaccouts");
                TextBox txtchkno = (TextBox)grdallemp.Rows[Convert.ToInt32(Index)].FindControl("txtchkno");
                TextBox txtpaidamt = (TextBox)grdallemp.Rows[Convert.ToInt32(Index)].FindControl("txtpaidamt");

                if (txtpaidamt.Text.Length > 0 && ddlaccouts.Items.Count > 0)
                {
                    Datapaid(salId, Index, txtpaidamt, ddlaccouts, txtchkno);
                }
            }
        }
    }
    protected void Datapaid(string salId, string Index, TextBox txtpaidamt, DropDownList ddlaccouts, TextBox txtchkno)
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

        int idd = MainAcocount.InsertTransactionMaster(Convert.ToDateTime(DateTime.Now.ToString()), db.ToString(), "1", Convert.ToInt32(Session["userid"]), Convert.ToDecimal(txtpaidamt.Text), ddlwarehouse.SelectedValue);
        if (idd > 0)
        {
            SqlCommand cmdrem = new SqlCommand();
            cmdrem.CommandText = "InsertSalaryRelatedPayTbl";
            cmdrem.CommandType = CommandType.StoredProcedure;

            cmdrem.Parameters.Add(new SqlParameter("@SalaryMasterId", SqlDbType.Int));
            cmdrem.Parameters["@SalaryMasterId"].Value = salId;
            cmdrem.Parameters.Add(new SqlParameter("@RelatedACId", SqlDbType.Int));
            cmdrem.Parameters["@RelatedACId"].Value = ddlaccouts.SelectedValue;
            cmdrem.Parameters.Add(new SqlParameter("@Cheqno", SqlDbType.NVarChar));
            cmdrem.Parameters["@Cheqno"].Value = txtchkno.Text;
            cmdrem.Parameters.Add(new SqlParameter("@PaidAmt", SqlDbType.NVarChar));
            cmdrem.Parameters["@PaidAmt"].Value = txtpaidamt.Text;
            cmdrem.Parameters.Add(new SqlParameter("@TrId", SqlDbType.NVarChar));
            cmdrem.Parameters["@TrId"].Value = idd;
            Int32 resultrem = DatabaseCls1.ExecuteNonQueryep(cmdrem);


            DataTable dtempaccid = select("Select AccountMaster.AccountId from AccountMaster inner join EmployeeMaster on EmployeeMaster.AccountId=AccountMaster.Id where EmployeeMaster.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.EmployeeMasterID='" + ddlemp.SelectedValue + "'");
            if (dtempaccid.Rows.Count == 0)
            {
                dtempaccid = select("Select AccountMaster.AccountId from AccountMaster inner join EmployeeMaster on EmployeeMaster.AccountId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.EmployeeMasterID='" + ddlemp.SelectedValue + "'");

            }
            if (dtempaccid.Rows.Count > 0)
            {
                //  TextBox Totamt = (TextBox)grdded.Rows[0].FindControl("Totamt");

                int tdidA = MainAcocount.Sp_Insert_Tranction_Details1(0, Convert.ToInt32(ddlaccouts.SelectedValue), 0, Convert.ToDecimal(txtpaidamt.Text), idd, Convert.ToString(txtchkno.Text), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);

                int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(dtempaccid.Rows[0]["AccountId"]), 0, Convert.ToDecimal(txtpaidamt.Text), 0, idd, Convert.ToString("Emp Acc Salary for Dr"), Convert.ToDateTime(DateTime.Now.ToString()), ddlwarehouse.SelectedValue);


                //decimal amt;
                //if (Math.Round(Convert.ToDecimal(txtnettotal.Text), 0, MidpointRounding.AwayFromZero) <=Math.Round( Convert.ToDecimal(txtpaidamt.Text),0, MidpointRounding.AwayFromZero))
                //{
                //    amt = 0;
                //}
                //else
                //{
                //    amt = Convert.ToDecimal(txtnettotal.Text) - Convert.ToDecimal(txtpaidamt.Text);
                //}

                SqlCommand cmdsupp = new SqlCommand("insert into TranctionMasterSuppliment(Tranction_Master_Id,Memo,Party_MasterId) values ('" + idd.ToString() + "','" + txtchkno.Text + "','0')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdsupp.ExecuteNonQuery();
                con.Close();
                SqlCommand cmdinspay = new SqlCommand("Insert into PaymentApplicationTbl values('" + idd.ToString() + "','0','" + Math.Round(Convert.ToDecimal(txtpaidamt.Text), 2, MidpointRounding.AwayFromZero) + "','" + DateTime.Now.ToShortDateString() + "','" + Session["UserId"] + "','0')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdinspay.ExecuteNonQuery();
                con.Close();

            }
        }
    }
    protected void grdallemp_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        DataTable dttotal = (DataTable)ViewState["ISFill"];
        if (dttotal.Rows.Count > 0)
        {
            fillgd(dttotal);
        }
    }

    protected void grdallemp_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Edit1")
        {
            Nullable();

            string dk = Convert.ToString(e.CommandArgument);

            ViewState["dk"] = dk.ToString();
            string salid = "";
            foreach (GridViewRow item in grdallemp.Rows)
            {
                Label lblEmployeeId = (Label)item.FindControl("lblEmployeeId");
                ImageButton llinkbb = (ImageButton)item.FindControl("llinkbb");
                if (Convert.ToString(dk) == Convert.ToString(lblEmployeeId.Text))
                {
                    salid = llinkbb.CommandArgument;
                    break;
                }
            }
            if (salid != "")
            {
                ViewState["dk"] = salid;
                dk = salid;
                DataTable dt1 = (DataTable)select("Select distinct  SalaryMaster.* from TranctionMaster inner join SalaryMaster on SalaryMaster.Tid=TranctionMaster.Tranction_Master_Id inner join " +
                " SalaryRemuneration on SalaryRemuneration.SalaryMasterId=SalaryMaster.Id  Left join SalaryDeduction on SalaryDeduction.SalaryMasterId=SalaryMaster.Id where  SalaryMaster.ID='" + salid + "'");

                if (dt1.Rows.Count > 0)
                {

                    ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(dt1.Rows[0]["Whid"].ToString()));
                    // fillpayperiod();
                    ddlpayperiod.SelectedIndex = ddlpayperiod.Items.IndexOf(ddlpayperiod.Items.FindByValue(dt1.Rows[0]["payperiodtypeId"].ToString()));
                    // ddlpayperiod_SelectedIndexChanged(sender, e);
                    ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(dt1.Rows[0]["EmployeeId"].ToString()));
                    txttotincome.Text = Convert.ToString(dt1.Rows[0]["TotalIncome"]);
                    //txttotded.Text = Convert.ToString(dt1.Rows[0]["TotalDeduction"]);
                    txtnettotal.Text = Convert.ToString(dt1.Rows[0]["NetTotal"]);
                    txtbenifitgrossamt.Text = Convert.ToString(dt1.Rows[0]["GrossRemu"]);
                    DataTable dt2 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname='Hour' and SalaryMasterId='" + dk + "' order by Id ASC");
                    txttotded.Text = Convert.ToString(dt1.Rows[0]["NonGovdedamt"]);
                    txtgovtottax.Text = Convert.ToString(dt1.Rows[0]["Govdedamt"]);

                    lbltotremunration.Text = Convert.ToString(dt1.Rows[0]["RemTotal"]);
                    lbltotremperc.Text = Convert.ToString(dt1.Rows[0]["RemPerc"]);
                    lbltotsales.Text = Convert.ToString(dt1.Rows[0]["RemSales"]);

                    if (dt2.Rows.Count > 0)
                    {
                        grdcal.DataSource = dt2;
                        grdcal.DataBind();
                        pnlcau.Visible = true;
                        pnlhourly.Visible = true;
                    }
                    DataTable dt4 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname='Day' and SalaryMasterId='" + dk + "' order by Id ASC");

                    if (dt4.Rows.Count > 0)
                    {
                        grddaily.DataSource = dt4;
                        grddaily.DataBind();
                        pnlcau.Visible = true;
                        pnldaily.Visible = true;
                    }
                    DataTable dt5 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName,Totalcomunitmonth as completemonth,maxcompete as completedmonthamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Month','Semi Month','Bi-Week','Week') and SalaryMasterId='" + dk + "' order by Id ASC");

                    if (dt5.Rows.Count > 0)
                    {
                        grdmonth.DataSource = dt5;
                        grdmonth.DataBind();
                        pnlcau.Visible = true;
                        pnlmonth.Visible = true;
                    }
                    DataTable dt6 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as per, SalaryRemuneration.perunitname,totalunit,Actualpayunit as baseamt,remamt as Totamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as Remuname,Totalcomunitmonth as perof,maxcompete as baseamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Is Percentage') and SalaryMasterId='" + dk + "' order by Id ASC");

                    if (dt6.Rows.Count > 0)
                    {
                        grdispercentage.DataSource = dt6;
                        grdispercentage.DataBind();
                        pnlcau.Visible = true;
                        pnlispercentage.Visible = true;
                    }
                    DataTable dt7 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as per, SalaryRemuneration.perunitname,totalunit,Actualpayunit as baseamt,remamt as Totamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as Remuname,Totalcomunitmonth as perof,maxcompete as baseamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Is Percentage of Sales') and SalaryMasterId='" + dk + "' order by Id ASC");

                    if (dt7.Rows.Count > 0)
                    {
                        grdsales.DataSource = dt7;
                        grdsales.DataBind();
                        pnlcau.Visible = true;
                        pnlsales.Visible = true;
                    }
                    DataTable dt8 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as txtleaverate, SalaryRemuneration.perunitname ,totalunit,Actualpayunit as perleaveno,remamt as Totamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as LeaveType,Totalcomunitmonth as perof,maxcompete as baseamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Leave Encash') and SalaryMasterId='" + dk + "' order by Id ASC");

                    if (dt8.Rows.Count > 0)
                    {
                        grdleaveencash.DataSource = dt8;
                        grdleaveencash.DataBind();
                        pnlleaveencash.Visible = true;

                    }
                    DataTable dt3 = (DataTable)select(" Select distinct RUDed, SalaryDeduction.Id,Deductionamt as Amount, deductionper as per,deductionperof as perof,dedamt as Totamt,DeductionName,RUDed from SalaryDeduction  where  SalaryMasterId='" + dk + "'");

                    if (dt3.Rows.Count > 0)
                    {
                        grdded.DataSource = dt3;
                        grdded.DataBind();
                    }
                    // DataTable dtgovte = (DataTable)select(" Select distinct  CrAccId, SalaryGovtDeduction.Id,Totdedamt as Amount,PayrolltaxMaster.tax_name+' : '+PayrolltaxMaster.Sortname as DeductionName from SalaryGovtDeduction  inner join  PayrolltaxMaster on SalaryGovtDeduction.PayrollTaxId=PayrolltaxMaster.Payrolltax_id where  SalaryMasterId='" + dk + "'");
                    DataTable dtgovte = (DataTable)select("Select distinct SalaryGovtDeduction.Id as CrAccId, SalaryGovtDeduction.Id,Totdedamt as Amount,PayrolltaxMaster.tax_name+' : '+PayrolltaxMaster.Sortname as DeductionName from  SalaryGovtDeduction inner join  PayrolltaxMaster on PayrolltaxMaster.Payrolltax_id=SalaryGovtDeduction.PayrollTaxId where  SalaryMasterId='" + dk + "'");

                    if (dtgovte.Rows.Count > 0)
                    {
                        grdgovded.DataSource = dtgovte;
                        grdgovded.DataBind();
                    }
                    DataTable dttben = (DataTable)select(" Select distinct SalaryTaxableBenifit.Id,TaxableBenName as Taxablebenifitname,TaxbenDate as Date, TaxbenAmt as Amount from  SalaryTaxableBenifit where  SalaryMasterId='" + dk + "'");

                    if (dttben.Rows.Count > 0)
                    {
                        grdtaxbenifit.DataSource = dttben;
                        grdtaxbenifit.DataBind();
                    }

                }
                btnUpdate.Visible = false;
                btnsubmit.Visible = false;
                btncalculate.Visible = false;
                btnaddnewrem.Visible = false;
                btndaily.Visible = false;
                btnmonth.Visible = false;
                btnded.Visible = false;
                btnEdit.Visible = true;
                pnlcau.Enabled = false;
            }
            else
            {
                ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(Convert.ToString(ViewState["dk"])));
                int kl = fillsaldata();
                btncalculate.Visible = true;
                pnlcau.Enabled = true;
            }
            lblempn.Text = "Employee : " + ddlemp.SelectedItem.Text;
            lblfilemp.Text = "Salary of " + ddlemp.SelectedItem.Text;
            pnlcau.Visible = true;
            pnlinsertdata.Visible = true;
        }
       
    }
    protected string REMOVEEMPG()
    {
        string inno = "";
        foreach (GridViewRow item in grdallemp.Rows)
        {
            Label lblEmployeeId = (Label)item.FindControl("lblEmployeeId");
            if (Convert.ToString(ddlemp.SelectedValue) == Convert.ToString(lblEmployeeId.Text))
            {
                inno = item.RowIndex.ToString();
                RemoveRowtempG(item.RowIndex.ToString());
                break;
            }
        }
        return inno;
    }
    protected void RemoveRowtempG(string index)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["ISFill"];

        dt.Rows.Remove(dt.Rows[Convert.ToInt32(index)]);

        grdallemp.DataSource = dt;
        grdallemp.DataBind();
        ViewState["ISFill"] = dt;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)select("Select * from  SalaryMaster where payperiodtypeId='" + ddlpayperiod.SelectedValue + "' and EmployeeId='" + ddlemp.SelectedValue + "' and  SalaryMaster.Id<>'" + ViewState["dk"] + "'");

        if (dt.Rows.Count <= 0)
        {

            DataTable dt1 = (DataTable)select("Select distinct  SalaryMaster.*,TranctionMaster.Tranction_Master_Id,Tranction_Details.Tranction_Details_Id  from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id inner join SalaryMaster on SalaryMaster.Tid=TranctionMaster.Tranction_Master_Id Where   SalaryMaster.Id='" + ViewState["dk"] + "' order by Tranction_Details_Id ASC ");

            if (dt1.Rows.Count > 0)
            {
                string strn = "update   TranctionMaster set " +
                    "   Tranction_Amount= '" + Math.Round(Convert.ToDecimal(txttotincome.Text), 2) + "' " +
                            " where Tranction_Master_Id=" + Convert.ToInt32(dt1.Rows[0]["Tranction_Master_Id"]) + " ";
                SqlCommand cmdn = new SqlCommand(strn, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdn.ExecuteNonQuery();
                con.Close();

                string strsm = "Update SalaryMaster Set RemTotal='" + lbltotremunration.Text + "',RemPerc='" + lbltotremperc.Text + "',RemSales='" + lbltotsales.Text + "',  NonGovdedamt='" + txttotded.Text + "',Govdedamt='" + txtgovtottax.Text + "', GrossRemu='" + txtbenifitgrossamt.Text + "', TotalIncome='" + txttotincome.Text + "',TotalDeduction='" + txttotded.Text + "',NetTotal='" + txtnettotal.Text + "' where Id='" + ViewState["dk"] + "'";
                SqlCommand cmdsm = new SqlCommand(strsm, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                int cinsm = cmdsm.ExecuteNonQuery();
                con.Close();
                int datacount = 0;
                foreach (GridViewRow grd in grdcal.Rows)
                {

                    int Idc = Convert.ToInt32(grdcal.DataKeys[grd.RowIndex].Value);
                    TextBox txttotal = (TextBox)grd.FindControl("txttotal");
                    TextBox txtrate = (TextBox)grd.FindControl("txtrate");
                    Label txtperunitname = (Label)grd.FindControl("txtperunitname");
                    TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");
                    TextBox txttotunit = (TextBox)grd.FindControl("txttotunit");
                    TextBox txtremu = (TextBox)grd.FindControl("txtremu");

                    string str1 = "Update Tranction_Details Set AmountDebit='" + Convert.ToDecimal(txttotal.Text) + "'where Tranction_Details_Id='" + dt1.Rows[datacount]["Tranction_Details_Id"] + "'";
                    SqlCommand cmdsal = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cin = cmdsal.ExecuteNonQuery();
                    con.Close();

                    string strrem = "Update SalaryRemuneration Set Rate='" + txtrate.Text + "',perunitname='" + txtperunitname.Text + "',totalunit='" + txttotunit.Text + "',Actualpayunit='" + txtqlifie.Text + "',Remunerationname='" + txtremu.Text + "',remamt='" + txttotal.Text + "' where Id='" + Idc + "'";
                    SqlCommand cmdrem = new SqlCommand(strrem, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cinre = cmdrem.ExecuteNonQuery();
                    con.Close();
                    datacount += 1;
                }
                foreach (GridViewRow grd in grddaily.Rows)
                {

                    int Idc = Convert.ToInt32(grddaily.DataKeys[grd.RowIndex].Value);
                    TextBox txttotal = (TextBox)grd.FindControl("txttotal");
                    TextBox txtrate = (TextBox)grd.FindControl("txtrate");
                    Label txtperunitname = (Label)grd.FindControl("txtperunitname");
                    TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");
                    TextBox txttotunit = (TextBox)grd.FindControl("txttotunit");
                    TextBox txtremu = (TextBox)grd.FindControl("txtremu");

                    string str1 = "Update Tranction_Details Set AmountDebit='" + Convert.ToDecimal(txttotal.Text) + "'where Tranction_Details_Id='" + dt1.Rows[datacount]["Tranction_Details_Id"] + "'";
                    SqlCommand cmdsal = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cin = cmdsal.ExecuteNonQuery();
                    con.Close();

                    string strrem = "Update SalaryRemuneration Set Rate='" + txtrate.Text + "',perunitname='" + txtperunitname.Text + "',totalunit='" + txttotunit.Text + "',Actualpayunit='" + txtqlifie.Text + "',Remunerationname='" + txtremu.Text + "',remamt='" + txttotal.Text + "' where Id='" + Idc + "'";
                    SqlCommand cmdrem = new SqlCommand(strrem, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cinre = cmdrem.ExecuteNonQuery();
                    con.Close();
                    datacount += 1;
                }
                foreach (GridViewRow grd in grdmonth.Rows)
                {

                    int Idc = Convert.ToInt32(grdmonth.DataKeys[grd.RowIndex].Value);
                    TextBox txttotal = (TextBox)grd.FindControl("txttotal");
                    TextBox txtrate = (TextBox)grd.FindControl("txtrate");
                    Label txtperunitname = (Label)grd.FindControl("txtperunitname");
                    TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");
                    TextBox txttotunit = (TextBox)grd.FindControl("txttotunit");
                    TextBox txtremu = (TextBox)grd.FindControl("txtremu");

                    TextBox txtcompletemonth = (TextBox)grd.FindControl("txtcompletemonth");
                    TextBox txtcompletemonthamt = (TextBox)grd.FindControl("txtcompletemonthamt");

                    string str1 = "Update Tranction_Details Set AmountDebit='" + Convert.ToDecimal(txttotal.Text) + "'where Tranction_Details_Id='" + dt1.Rows[datacount]["Tranction_Details_Id"] + "'";
                    SqlCommand cmdsal = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cin = cmdsal.ExecuteNonQuery();
                    con.Close();

                    string strrem = "Update SalaryRemuneration Set Totalcomunitmonth='" + txtcompletemonth.Text + "',maxcompete='" + txtcompletemonthamt.Text + "',  Rate='" + txtrate.Text + "',perunitname='" + txtperunitname.Text + "',totalunit='" + txttotunit.Text + "',Actualpayunit='" + txtqlifie.Text + "',Remunerationname='" + txtremu.Text + "',remamt='" + txttotal.Text + "' where Id='" + Idc + "'";
                    SqlCommand cmdrem = new SqlCommand(strrem, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cinre = cmdrem.ExecuteNonQuery();
                    con.Close();
                    datacount += 1;
                }
                foreach (GridViewRow grd in grdispercentage.Rows)
                {

                    int Idc = Convert.ToInt32(grdispercentage.DataKeys[grd.RowIndex].Value);
                    TextBox PercentageOf = (TextBox)grd.FindControl("PercentageOf");

                    TextBox txtbaseamt = (TextBox)grd.FindControl("txtbaseamt");

                    TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                    TextBox txtpaamt = (TextBox)grd.FindControl("txtpaamt");


                    TextBox perof = (TextBox)grd.FindControl("perof");


                    string str1 = "Update Tranction_Details Set AmountDebit='" + Convert.ToDecimal(txtpaamt.Text) + "'where Tranction_Details_Id='" + dt1.Rows[datacount]["Tranction_Details_Id"] + "'";
                    SqlCommand cmdsal = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cin = cmdsal.ExecuteNonQuery();
                    con.Close();

                    string strrem = "Update SalaryRemuneration Set Totalcomunitmonth='" + perof.Text + "', Rate='" + PercentageOf.Text + "',Actualpayunit='" + txtbaseamt.Text + "',Remunerationname='" + txtremu.Text + "',remamt='" + txtpaamt.Text + "' where Id='" + Idc + "'";
                    SqlCommand cmdrem = new SqlCommand(strrem, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cinre = cmdrem.ExecuteNonQuery();
                    con.Close();
                    datacount += 1;
                }
                foreach (GridViewRow grd in grdsales.Rows)
                {

                    int Idc = Convert.ToInt32(grdsales.DataKeys[grd.RowIndex].Value);
                    TextBox PercentageOf = (TextBox)grd.FindControl("PercentageOf");

                    TextBox txtbaseamt = (TextBox)grd.FindControl("txtbaseamt");

                    TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                    TextBox txtpaamt = (TextBox)grd.FindControl("txtpaamt");


                    TextBox perof = (TextBox)grd.FindControl("perof");


                    string str1 = "Update Tranction_Details Set AmountDebit='" + Convert.ToDecimal(txtpaamt.Text) + "'where Tranction_Details_Id='" + dt1.Rows[datacount]["Tranction_Details_Id"] + "'";
                    SqlCommand cmdsal = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cin = cmdsal.ExecuteNonQuery();
                    con.Close();

                    string strrem = "Update SalaryRemuneration Set Totalcomunitmonth='" + perof.Text + "', Rate='" + PercentageOf.Text + "',Actualpayunit='" + txtbaseamt.Text + "',Remunerationname='" + txtremu.Text + "',remamt='" + txtpaamt.Text + "' where Id='" + Idc + "'";
                    SqlCommand cmdrem = new SqlCommand(strrem, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cinre = cmdrem.ExecuteNonQuery();
                    con.Close();
                    datacount += 1;
                }
                foreach (GridViewRow grd in grdleaveencash.Rows)
                {

                    int Idc = Convert.ToInt32(grdleaveencash.DataKeys[grd.RowIndex].Value);
                    TextBox txtleaverate = (TextBox)grd.FindControl("txtleaverate");

                    TextBox txtnoofleave = (TextBox)grd.FindControl("txtnoofleave");

                    Label txtremu = (Label)grd.FindControl("lblleavet");
                    TextBox txtpaamt = (TextBox)grd.FindControl("txtpaamt");





                    string str1 = "Update Tranction_Details Set AmountDebit='" + Convert.ToDecimal(txtpaamt.Text) + "'where Tranction_Details_Id='" + dt1.Rows[datacount]["Tranction_Details_Id"] + "'";
                    SqlCommand cmdsal = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cin = cmdsal.ExecuteNonQuery();
                    con.Close();

                    string strrem = "Update SalaryRemuneration Set  Rate='" + txtleaverate.Text + "',totalunit='" + txtnoofleave.Text + "',Actualpayunit='" + txtnoofleave.Text + "',Remunerationname='" + txtremu.Text + "',remamt='" + txtpaamt.Text + "' where Id='" + Idc + "'";
                    SqlCommand cmdrem = new SqlCommand(strrem, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cinre = cmdrem.ExecuteNonQuery();
                    con.Close();
                    datacount += 1;
                }
                foreach (GridViewRow grd in grdded.Rows)
                {


                    int Id = Convert.ToInt32(grdded.DataKeys[grd.RowIndex].Value);
                    TextBox Deductionamt = (TextBox)grd.FindControl("FixedAmount");
                    TextBox PercentageOf = (TextBox)grd.FindControl("PercentageOf");
                    TextBox perof = (TextBox)grd.FindControl("perof");

                    TextBox Totamt = (TextBox)grd.FindControl("Totamt");
                    TextBox txtdedremname = (TextBox)grd.FindControl("txtdedremname");
                    if (Convert.ToDecimal(Totamt.Text) > 0)
                    {
                        string str1 = "Update Tranction_Details Set AmountCredit='" + Convert.ToDecimal(Totamt.Text) + "'where Tranction_Details_Id='" + dt1.Rows[datacount]["Tranction_Details_Id"] + "'";
                        SqlCommand cmdsal = new SqlCommand(str1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        int cin = cmdsal.ExecuteNonQuery();
                        con.Close();

                        string strrem = "Update SalaryDeduction Set DeductionName='" + txtdedremname.Text + "',Deductionamt='" + Deductionamt.Text + "',deductionper='" + PercentageOf.Text + "',deductionperof='" + perof.Text + "',dedamt='" + Totamt.Text + "'where Id='" + Id + "'";
                        SqlCommand cmdrem = new SqlCommand(strrem, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        int cinre = cmdrem.ExecuteNonQuery();
                        con.Close();

                        datacount += 1;
                    }
                }
                foreach (GridViewRow grd in grdgovded.Rows)
                {


                    int Id = Convert.ToInt32(grdgovded.DataKeys[grd.RowIndex].Value);

                    Label txtgovtamt = (Label)grd.FindControl("txtgovtamt");
                    if (Convert.ToDecimal(txtgovtamt.Text) > 0)
                    {
                        string str1 = "Update Tranction_Details Set AmountCredit='" + Convert.ToDecimal(txtgovtamt.Text) + "'where Tranction_Details_Id='" + dt1.Rows[datacount]["Tranction_Details_Id"] + "'";
                        SqlCommand cmdsal = new SqlCommand(str1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        int cin = cmdsal.ExecuteNonQuery();
                        con.Close();

                        string strrem = "Update SalaryGovtDeduction Set Totdedamt='" + txtgovtamt.Text + "' where Id='" + Id + "'";
                        SqlCommand cmdrem = new SqlCommand(strrem, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        int cinre = cmdrem.ExecuteNonQuery();
                        con.Close();

                        datacount += 1;
                    }
                }
                if ((dt1.Rows.Count - 1) == datacount)
                {
                    string str11 = "Update Tranction_Details Set AmountCredit='" + Convert.ToDecimal(txtnettotal.Text) + "'where Tranction_Details_Id='" + dt1.Rows[datacount]["Tranction_Details_Id"] + "'";
                    SqlCommand cmdsal1 = new SqlCommand(str11, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int cin1 = cmdsal1.ExecuteNonQuery();
                    con.Close();
                }

                if (chkpaida.Checked == true)
                {
                    DataTable Dtre = (DataTable)select("Select distinct  SalaryRelatedPayTbl.Id,TranctionMaster.Tranction_Master_Id,Tranction_Details.Tranction_Details_Id  from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id inner join SalaryRelatedPayTbl on SalaryRelatedPayTbl.TrId=TranctionMaster.Tranction_Master_Id Where   SalaryRelatedPayTbl.SalaryMasterId='" + ViewState["dk"] + "' order by Tranction_Details_Id ASC ");

                    if (Dtre.Rows.Count > 0)
                    {
                        string strrelsal = "update   TranctionMaster set " +
                            "   Tranction_Amount= '" + Math.Round(Convert.ToDecimal(txtrelpaidamr.Text), 2) + "' " +
                                    " where Tranction_Master_Id=" + Convert.ToInt32(Dtre.Rows[0]["Tranction_Master_Id"]) + " ";
                        SqlCommand cmrelsal = new SqlCommand(strrelsal, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmrelsal.ExecuteNonQuery();
                        con.Close();
                        string str11 = "Update Tranction_Details Set AccountCredit='" + ddlrelacc.SelectedValue + "', AmountCredit='" + Convert.ToDecimal(txtrelpaidamr.Text) + "',Memo='" + txtrelcheque.Text + "' where Tranction_Details_Id='" + dt1.Rows[0]["Tranction_Details_Id"] + "'";
                        SqlCommand cmdsal1 = new SqlCommand(str11, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        int cin1 = cmdsal1.ExecuteNonQuery();
                        con.Close();
                        string str1 = "Update Tranction_Details Set AmountDebit='" + Convert.ToDecimal(txtrelpaidamr.Text) + "',Memo='" + txtrelcheque.Text + "' where Tranction_Details_Id='" + dt1.Rows[1]["Tranction_Details_Id"] + "'";
                        SqlCommand cmdsal = new SqlCommand(str1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        int cin = cmdsal.ExecuteNonQuery();
                        con.Close();



                        string strrem = "Update SalaryRelatedPayTbl Set RelatedACId='" + ddlrelacc.SelectedValue + "',Cheqno='" + txtrelcheque.Text + "',PaidAmt='" + txtrelpaidamr.Text + "' where Id='" + Dtre.Rows[0]["Id"] + "'";
                        SqlCommand cmdrem = new SqlCommand(strrem, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        int cinre = cmdrem.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        if (txtrelpaidamr.Text.Length > 0 && ddlrelacc.Items.Count > 0)
                        {
                            Datapaid(ViewState["dk"].ToString(), "0", txtrelpaidamr, ddlrelacc, txtrelcheque);
                        }
                    }
                }


                lblmsg.Visible = true;
                //string ind = REMOVEEMPG();

                // FillAllEmpData(ViewState["dk"].ToString(), ind);
                lblmsg.Text = "Record Updated successfully";
                btnUpdate.Visible = false;
                btnsubmit.Visible = true;
                grdcal.DataSource = null;
                grdcal.DataBind();
                grdded.DataSource = null;
                grdded.DataBind();
                txtnettotal.Text = "0";
                txttotded.Text = "0";
                txttotincome.Text = "0";
                lbltotremunration.Text = "0";
                lbltotsales.Text = "0";
                lbltotremperc.Text = "0";
                pnlcau.Visible = false;
                //ddlemp.Items.Clear();
                //ddlpayperiod.SelectedIndex = 0;
                pnlinsertdata.Visible = false;
              
               //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";
        }
    }
    protected void btndaily_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        filldaily(dt, dt);
        if (grddaily.Rows.Count > 0)
        {

            foreach (GridViewRow grd in grddaily.Rows)
            {
                TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                Label txtperunitname = (Label)grd.FindControl("txtperunitname");
                DropDownList ddldailyremu = (DropDownList)grd.FindControl("ddldailyremu");
                if (txtremu.Text == "")
                {
                    DataTable dt111 = new DataTable();
                    DataTable dt11 = (DataTable)select("  Select * from EmployeePayrollMaster  where EmpId='" + ddlemp.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "'");
                    if (dt11.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt11.Rows[0]["EmployeePaidAsPerDesignation"]) != true)
                        {
                            dt111 = (DataTable)select("Select distinct RemunerationMaster.Id,EmployeeID, RemunerationMaster.RemunerationName as RemunarationName from  RemunerationMaster inner join EmployeeSalaryMaster on EmployeeSalaryMaster.Remuneration_Id=RemunerationMaster.Id Left join PeriodMaster12 on PeriodMaster12.Id=EmployeeSalaryMaster.PayablePer_PeriodMasterId  Left join  RemunerationMaster AS RM1 on EmployeeSalaryMaster.IsPercentRemunerationId=RM1.Id where EmployeeID='" + ddlemp.SelectedValue + "' and(EmployeeSalaryMaster.EffectiveEndDate>='" + sdate + "' or EmployeeSalaryMaster.EffectiveStartDate>='" + sdate + "') and (EmployeeSalaryMaster.EffectiveEndDate<='" + edate + "' or EmployeeSalaryMaster.EffectiveStartDate<='" + edate + "') order by RemunarationName");

                        }
                        else
                        {
                            dt111 = (DataTable)select("Select distinct RemunerationMaster.Id, RemunerationMaster.RemunerationName as RemunarationName from  RemunerationMaster inner join RemunerationByDesignation on RemunerationByDesignation.Remuneration_Id=RemunerationMaster.Id Left join PeriodMaster12 on PeriodMaster12.Id=RemunerationByDesignation.PayablePer_PeriodMasterId  Left join  RemunerationMaster AS RM1 on RemunerationByDesignation.IsPercentRemunerationId=RM1.Id where DesignationId=(Select DesignationMasterId from EmployeeMaster where EmployeeMasterId='" + ddlemp.SelectedValue + "')  and(RemunerationByDesignation.EffectiveEndDate>='" + sdate + "' or RemunerationByDesignation.EffectiveStartDate>='" + sdate + "') and (RemunerationByDesignation.EffectiveEndDate<='" + edate + "' or RemunerationByDesignation.EffectiveStartDate<='" + edate + "') order by RemunarationName");



                        }
                    }
                    if (dt111.Rows.Count > 0)
                    {
                        ddldailyremu.DataSource = dt111;
                        ddldailyremu.DataTextField = "RemunarationName";
                        ddldailyremu.DataValueField = "Id";
                        ddldailyremu.DataBind();
                        ddldailyremu.Visible = true;
                        txtremu.Text = "0";
                        txtremu.Visible = false;
                    }
                }
                else
                {
                    ddldailyremu.Visible = false;
                    txtremu.Visible = true;
                }
            }
        }
    }



    protected void btnmonth_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        fillmonth(dt, dt);
        if (grdmonth.Rows.Count > 0)
        {

            foreach (GridViewRow grd in grdmonth.Rows)
            {
                TextBox txtremu = (TextBox)grd.FindControl("txtremu");
                Label txtperunitname = (Label)grd.FindControl("txtperunitname");
                DropDownList ddldailyremu = (DropDownList)grd.FindControl("ddldailyremu");
                if (txtremu.Text == "")
                {
                    DataTable dt111 = new DataTable();
                    DataTable dt11 = (DataTable)select("  Select * from EmployeePayrollMaster  where EmpId='" + ddlemp.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "'");
                    if (dt11.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt11.Rows[0]["EmployeePaidAsPerDesignation"]) != true)
                        {
                            dt111 = (DataTable)select("Select distinct RemunerationMaster.Id,EmployeeID, RemunerationMaster.RemunerationName as RemunarationName from  RemunerationMaster inner join EmployeeSalaryMaster on EmployeeSalaryMaster.Remuneration_Id=RemunerationMaster.Id Left join PeriodMaster12 on PeriodMaster12.Id=EmployeeSalaryMaster.PayablePer_PeriodMasterId  Left join  RemunerationMaster AS RM1 on EmployeeSalaryMaster.IsPercentRemunerationId=RM1.Id where EmployeeID='" + ddlemp.SelectedValue + "' and(EmployeeSalaryMaster.EffectiveEndDate>='" + sdate + "' or EmployeeSalaryMaster.EffectiveStartDate>='" + sdate + "') and (EmployeeSalaryMaster.EffectiveEndDate<='" + edate + "' or EmployeeSalaryMaster.EffectiveStartDate<='" + edate + "') order by RemunarationName");

                        }
                        else
                        {
                            dt111 = (DataTable)select("Select distinct RemunerationMaster.Id, RemunerationMaster.RemunerationName as RemunarationName from  RemunerationMaster inner join RemunerationByDesignation on RemunerationByDesignation.Remuneration_Id=RemunerationMaster.Id Left join PeriodMaster12 on PeriodMaster12.Id=RemunerationByDesignation.PayablePer_PeriodMasterId  Left join  RemunerationMaster AS RM1 on RemunerationByDesignation.IsPercentRemunerationId=RM1.Id where DesignationId=(Select DesignationMasterId from EmployeeMaster where EmployeeMasterId='" + ddlemp.SelectedValue + "')  and(RemunerationByDesignation.EffectiveEndDate>='" + sdate + "' or RemunerationByDesignation.EffectiveStartDate>='" + sdate + "') and (RemunerationByDesignation.EffectiveEndDate<='" + edate + "' or RemunerationByDesignation.EffectiveStartDate<='" + edate + "') order by RemunarationName");



                        }
                    }
                    if (dt111.Rows.Count > 0)
                    {
                        ddldailyremu.DataSource = dt111;
                        ddldailyremu.DataTextField = "RemunarationName";
                        ddldailyremu.DataValueField = "Id";
                        ddldailyremu.DataBind();
                        ddldailyremu.Visible = true;
                        txtremu.Visible = false;
                        txtremu.Text = "0";
                    }
                }
                else
                {
                    ddldailyremu.Visible = false;
                    txtremu.Visible = true;
                }
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnEdit.Visible = false;
        btnUpdate.Visible = true;
        btncancel.Visible = true;
        btncalculate.Visible = true;
        pnlcau.Enabled = true;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Nullable();
        pnlinsertdata.Visible = false;
        btnUpdate.Visible = false;
        btnEdit.Visible = false;
        btncalculate.Visible = false;
        pnlcau.Enabled = true;
        btnsubmit.Enabled = false;
        ddlemp.SelectedIndex = 0;
    }
    protected void imgAdd2_Click(object sender, ImageClickEventArgs e)
    {
        string te = "Taxablebenifitforemployee.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgRefresh2_Click(object sender, ImageClickEventArgs e)
    {
        filltaxablebenifit();
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        if (btncancel0.Text == "Printable Version")
        {
            btncancel0.Text = "Hide Printable Version";
            if (btnEdit.Visible == true)
            {
                ViewState["vs"] = "vs";
            }
            else
            {
                ViewState["vs"] = "";
            }
            Button7.Visible = true;
            btnaddnewrem.Visible = false;
            btndaily.Visible = false;
            btnmonth.Visible = false;
            btnded.Visible = false;
            btncalculate.Visible = false;
            btncancel.Visible = false;
            btnsubmit.Visible = false;
            btnUpdate.Visible = false;
            btnEdit.Visible = false;




        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;
            btncalculate.Visible = true;
            btncancel.Visible = true;
            if (Convert.ToString(ViewState["vs"]) == "vs")
            {
                btnaddnewrem.Visible = true;
                btndaily.Visible = true;
                btnmonth.Visible = true;
                btnded.Visible = true;
                if (pnlcau.Enabled == false)
                {
                    btnEdit.Visible = true;
                }
                btnUpdate.Visible = true;
            }
            else
            {
                btnsubmit.Visible = true;
            }

        }
    }
    public DataTable TotalCalc()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prdi = new DataColumn();
        prdi.ColumnName = "RemId";
        prdi.DataType = System.Type.GetType("System.String");
        prdi.AllowDBNull = true;
        dtTemp.Columns.Add(prdi);

        DataColumn peid = new DataColumn();
        peid.ColumnName = "EmployeeId";
        peid.DataType = System.Type.GetType("System.String");
        peid.AllowDBNull = true;
        dtTemp.Columns.Add(peid);


        DataColumn prdem = new DataColumn();
        prdem.ColumnName = "EmployeeName";
        prdem.DataType = System.Type.GetType("System.String");
        prdem.AllowDBNull = true;
        dtTemp.Columns.Add(prdem);

        DataColumn prd = new DataColumn();
        prd.ColumnName = "Remorig";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "remunit";
        ssCatId.DataType = System.Type.GetType("System.Decimal");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);


        DataColumn catname = new DataColumn();
        catname.ColumnName = "remrate";
        catname.DataType = System.Type.GetType("System.String");
        catname.AllowDBNull = true;
        dtTemp.Columns.Add(catname);


        DataColumn completemonth = new DataColumn();
        completemonth.ColumnName = "remamt";
        completemonth.DataType = System.Type.GetType("System.Decimal");
        completemonth.AllowDBNull = true;
        dtTemp.Columns.Add(completemonth);

        DataColumn completedmonthamt = new DataColumn();
        completedmonthamt.ColumnName = "remsalesname";
        completedmonthamt.DataType = System.Type.GetType("System.String");
        completedmonthamt.AllowDBNull = true;
        dtTemp.Columns.Add(completedmonthamt);



        DataColumn barcodeId = new DataColumn();
        barcodeId.ColumnName = "remsalesperc";
        barcodeId.DataType = System.Type.GetType("System.Decimal");
        barcodeId.AllowDBNull = true;
        dtTemp.Columns.Add(barcodeId);


        DataColumn remperunit = new DataColumn();
        remperunit.ColumnName = "remsalesamt";
        remperunit.DataType = System.Type.GetType("System.Decimal");
        remperunit.AllowDBNull = true;
        dtTemp.Columns.Add(remperunit);

        DataColumn remperunitId = new DataColumn();
        remperunitId.ColumnName = "Remother";
        remperunitId.DataType = System.Type.GetType("System.Decimal");
        remperunitId.AllowDBNull = true;
        dtTemp.Columns.Add(remperunitId);

        DataColumn dedother = new DataColumn();
        dedother.ColumnName = "Remtotal";
        dedother.DataType = System.Type.GetType("System.Decimal");
        dedother.AllowDBNull = true;
        dtTemp.Columns.Add(dedother);


        DataColumn dedotherId = new DataColumn();
        dedotherId.ColumnName = "Ded1";
        dedotherId.DataType = System.Type.GetType("System.Decimal");
        dedotherId.AllowDBNull = true;
        dtTemp.Columns.Add(dedotherId);

        DataColumn Dedmulti = new DataColumn();
        Dedmulti.ColumnName = "Ded2";
        Dedmulti.DataType = System.Type.GetType("System.Decimal");
        Dedmulti.AllowDBNull = true;
        dtTemp.Columns.Add(Dedmulti);



        DataColumn Dedmultiv1 = new DataColumn();
        Dedmultiv1.ColumnName = "Actunit";
        Dedmultiv1.DataType = System.Type.GetType("System.Decimal");
        Dedmultiv1.AllowDBNull = true;
        dtTemp.Columns.Add(Dedmultiv1);



        DataColumn remnetamt = new DataColumn();
        remnetamt.ColumnName = "TotalDed";
        remnetamt.DataType = System.Type.GetType("System.Decimal");
        remnetamt.AllowDBNull = true;
        dtTemp.Columns.Add(remnetamt);

        DataColumn remptotamt = new DataColumn();
        remptotamt.ColumnName = "remnetamt";
        remptotamt.DataType = System.Type.GetType("System.Decimal");
        remptotamt.AllowDBNull = true;
        dtTemp.Columns.Add(remptotamt);


        DataColumn remover = new DataColumn();
        remover.ColumnName = "remPaidamt";
        remover.DataType = System.Type.GetType("System.Decimal");
        remover.AllowDBNull = true;
        dtTemp.Columns.Add(remover);

        DataColumn remper = new DataColumn();
        remper.ColumnName = "SalId";
        remper.DataType = System.Type.GetType("System.String");
        remper.AllowDBNull = true;
        dtTemp.Columns.Add(remper);

        DataColumn reap = new DataColumn();
        reap.ColumnName = "repa";
        reap.DataType = System.Type.GetType("System.Boolean");
        reap.AllowDBNull = true;
        dtTemp.Columns.Add(reap);

        DataColumn rep = new DataColumn();
        rep.ColumnName = "repab";
        rep.DataType = System.Type.GetType("System.Boolean");
        rep.AllowDBNull = true;
        dtTemp.Columns.Add(rep);

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
        NGother.ColumnName = "NGother";
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
        Gdedoothe.ColumnName = "Gother";
        Gdedoothe.DataType = System.Type.GetType("System.Decimal");
        Gdedoothe.AllowDBNull = true;
        dtTemp.Columns.Add(Gdedoothe);

        return dtTemp;
    }
    protected void FillAllEmpData(string SalId, string inno)
    {

        DataTable dttotal = new DataTable();
        if (ViewState["ISFill"] == null)
        {
            dttotal = TotalCalc();
        }

        else
        {
            dttotal = (DataTable)ViewState["ISFill"];
        }
        DataRow dtadd = dttotal.NewRow();
        dtadd["EmployeeName"] = ddlemp.SelectedItem.Text;
        dtadd["EmployeeId"] = ddlemp.SelectedValue;
        TextBox txttotal = new TextBox();
        if (SalId == "")
        {
            dtadd["repa"] = false;
            dtadd["repab"] = true;
            dtadd["remPaidamt"] = "0";
            dtadd["SalId"] = "";
        }
        else
        {
            dtadd["repa"] = true;
            dtadd["repab"] = false;
            dtadd["remPaidamt"] = txtnettotal.Text;
            dtadd["SalId"] = SalId;
        }
        if (Convert.ToString(dtadd["remamt"]) == "")
        {
            dtadd["remamt"] = "0";
        }
        dtadd["Actunit"] = "0";
        foreach (GridViewRow grd in grdcal.Rows)
        {
            int Id = Convert.ToInt32(grdcal.DataKeys[grd.RowIndex].Value);

            Label lblover = (Label)grd.FindControl("lblover");
            txttotal = (TextBox)grd.FindControl("txttotal");
            TextBox txtrate = (TextBox)grd.FindControl("txtrate");
            TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");

            if (lblover.Text != "Over")
            {
                // dtadd["remunit"] = "Hour";
                dtadd["remrate"] = String.Format("{0:n}", Convert.ToDecimal(txtrate.Text)) + " per hour";

            }
            else
            {

                //dtadd["Remover"] = "1";
            }
            dtadd["RemId"] = Id.ToString();
            dtadd["Actunit"] = Convert.ToDecimal(dtadd["Actunit"]) + Convert.ToDecimal(txtqlifie.Text);
            dtadd["remamt"] = Convert.ToDecimal(dtadd["remamt"]) + Convert.ToDecimal(txttotal.Text);

        }
        foreach (GridViewRow grd in grddaily.Rows)
        {
            int Id = Convert.ToInt32(grddaily.DataKeys[grd.RowIndex].Value);

            txttotal = (TextBox)grd.FindControl("txttotal");
            TextBox txtrate = (TextBox)grd.FindControl("txtrate");
            TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");

            if (Convert.ToString(dtadd["remunit"]) == "")
            {
                //   dtadd["remunit"] = "Day";
                dtadd["remrate"] = String.Format("{0:n}", Convert.ToDecimal(txtrate.Text)) + " per day"; ;

            }
            dtadd["RemId"] = Id.ToString();
            dtadd["Actunit"] = txtqlifie.Text;
            dtadd["remamt"] = Convert.ToDecimal(dtadd["remamt"]) + Convert.ToDecimal(txttotal.Text);

        }

        foreach (GridViewRow grd in grdmonth.Rows)
        {
            int Id = Convert.ToInt32(grdmonth.DataKeys[grd.RowIndex].Value);

            Label perunitname = (Label)grd.FindControl("txtperunitname");
            txttotal = (TextBox)grd.FindControl("txttotal");
            TextBox txtrate = (TextBox)grd.FindControl("txtrate");
            TextBox txtqlifie = (TextBox)grd.FindControl("txtqlifie");

            if (Convert.ToString(dtadd["remunit"]) == "")
            {
                // dtadd["remunit"] = perunitname.Text;
                dtadd["remrate"] = String.Format("{0:n}", Convert.ToDecimal(txtrate.Text)) + " per " + perunitname.Text.ToLower();

            }
            dtadd["RemId"] = Id.ToString();
            dtadd["Actunit"] = txtqlifie.Text;
            dtadd["remamt"] = Convert.ToDecimal(dtadd["remamt"]) + Convert.ToDecimal(txttotal.Text);

        }
        if (Convert.ToString(dtadd["Remother"]) == "")
        {
            dtadd["Remother"] = "0";
        }

        foreach (GridViewRow grd in grdispercentage.Rows)
        {

            TextBox txtpaamt = (TextBox)grd.FindControl("txtpaamt");

            dtadd["Remother"] = Convert.ToDecimal(dtadd["Remother"]) + Convert.ToDecimal(txtpaamt.Text);
            hideotherrem = "1";
        }
        foreach (GridViewRow grd in grdleaveencash.Rows)
        {

            TextBox txtpaamt = (TextBox)grd.FindControl("txtpaamt");
            hideotherrem = "1";
            dtadd["Remother"] = Convert.ToDecimal(dtadd["Remother"]) + Convert.ToDecimal(txtpaamt.Text);
        }
        if (Convert.ToString(dtadd["remsalesamt"]) == "")
        {
            dtadd["remsalesamt"] = "0";
        }
        if (Convert.ToString(dtadd["remunit"]) == "")
        {
            dtadd["remunit"] = "0";
        }
        foreach (GridViewRow grd in grdsales.Rows)
        {
            int Id = Convert.ToInt32(grdsales.DataKeys[grd.RowIndex].Value);
            TextBox txtremu = (TextBox)grd.FindControl("txtremu");
            TextBox PercentageOf = (TextBox)grd.FindControl("PercentageOf");
            TextBox perof = (TextBox)grd.FindControl("perof");
            TextBox txtpaamt = (TextBox)grd.FindControl("txtpaamt");
            dtadd["remsalesname"] = txtremu.Text;
            if (PercentageOf.Text != "")
            {
                dtadd["remsalesperc"] = PercentageOf.Text;
            }
            else
            {
                dtadd["remsalesperc"] = perof.Text;
            }
            TextBox txtbaseamt = (TextBox)grd.FindControl("txtbaseamt");
            dtadd["remunit"] = Convert.ToDecimal(dtadd["remunit"]) + Convert.ToDecimal(txtbaseamt.Text);

            dtadd["remsalesamt"] = Convert.ToDecimal(dtadd["remsalesamt"]) + Convert.ToDecimal(txtpaamt.Text);
            hidesalesname = "1";

        }

        foreach (GridViewRow grd in grdtaxbenifit.Rows)
        {

            Label txttaxba = (Label)grd.FindControl("txtamt");
            hideotherrem = "1";
            dtadd["Remother"] = Convert.ToDecimal(dtadd["Remother"]) + Convert.ToDecimal(txttaxba.Text);
        }

        if (Convert.ToString(txttotded.Text) == "")
        {

            txttotded.Text = "0";
        }
        if (Convert.ToString(txtgovtottax.Text) == "")
        {

            txtgovtottax.Text = "0";
        }
        dtadd["Ded1"] = txttotded.Text;
        dtadd["Ded2"] = txtgovtottax.Text;
        if (Convert.ToString(dtadd["Ded1"]) == "")
        {
            hide1tded = "1";
            dtadd["Ded1"] = "0";
        }
        if (Convert.ToString(dtadd["Ded2"]) == "")
        {
            // hide2tded = "1";
            dtadd["Ded2"] = "0";
        }
        dtadd["TotalDed"] = Convert.ToString(Convert.ToDecimal(dtadd["Ded1"]) + Convert.ToDecimal(dtadd["Ded2"]));
        if (txtbenifitgrossamt.Text != "0")
        {
            dtadd["Remtotal"] = txtbenifitgrossamt.Text;
        }
        else
        {
            dtadd["Remtotal"] = txttotincome.Text;
        }
        // dtadd["TotalDed"] = txttotded.Text;
        dtadd["remnetamt"] = txtnettotal.Text;

        dtadd["NG1"] = "0";
        dtadd["NG2"] = "0";
        dtadd["NG3"] = "0";
        dtadd["NG4"] = "0";
        dtadd["NGother"] = "0";
        foreach (GridViewRow grd in grdded.Rows)
        {
            TextBox txtdedremname = (TextBox)grd.FindControl("txtdedremname");
            TextBox Totamt = (TextBox)grd.FindControl("Totamt");
            int gh = 0;
            for (int i = 0; i < 4; i++)
            {
                if (CheckBoxList1.Items[i].Text == txtdedremname.Text)
                {
                    gh = 1;
                    dtadd["NG" + (i + 1)] = Totamt.Text;
                    break;
                }

            }
            if (gh == 0)
            {
                dtadd["NGother"] = Totamt.Text;
            }
        }
        dtadd["G1"] = "0";
        dtadd["G2"] = "0";
        dtadd["G3"] = "0";
        dtadd["G4"] = "0";
        dtadd["Gother"] = "0";
        foreach (GridViewRow grd in grdgovded.Rows)
        {
            Label lblgovtded = (Label)grd.FindControl("lblgovtded");
            Label txtgovtamt = (Label)grd.FindControl("txtgovtamt");
            int gh = 0;
            for (int i = 0; i < 4; i++)
            {
                // string[] separator1 = new string[] { ":" };

                //string[] strSplitArr1 = lblgovtded.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                //string varcon = "";
                //if (strSplitArr1.Length > 1)
                //{
                //    varcon = strSplitArr1[1];
                //}
                //else
                //{
                //    varcon = strSplitArr1[0];
                //}
                bool allcomp = lblgovtded.Text.Contains(Convert.ToString(" : " + CheckBoxList1.Items[5 + i].Text));
                if (allcomp == true)
                {

                    //if (CheckBoxList1.Items[5 + i].Text == lblgovtded.Text)
                    //{
                    gh = 1;
                    dtadd["G" + (i + 1)] = txtgovtamt.Text;
                    break;
                    //}
                }

            }
            if (gh == 0)
            {
                dtadd["Gother"] = txtgovtamt.Text;
            }

        }

        if (inno != "")
        {
            dttotal.Rows.InsertAt(dtadd, Convert.ToInt32(inno));
        }
        else
        {
            dttotal.Rows.Add(dtadd);
        }
        ViewState["ISFill"] = dttotal;
        if (hidesalesname != "")
        {
            grdallemp.Columns[5].Visible = true;
            grdallemp.Columns[6].Visible = true;
            grdallemp.Columns[7].Visible = true;
            grdallemp.Columns[8].Visible = true;
        }
        else
        {
            grdallemp.Columns[5].Visible = false;
            grdallemp.Columns[6].Visible = false;
            grdallemp.Columns[7].Visible = false;
            grdallemp.Columns[8].Visible = false;
        }
        if (hideotherrem != "")
        {
            grdallemp.Columns[9].Visible = true;

        }
        else
        {
            grdallemp.Columns[9].Visible = false;

        }


        if (dttotal.Rows.Count > 0)
        {
            fillgd(dttotal);
            btnSuball.Visible = true;
        }
    }
    protected void btnck()
    {

        decimal footremunit = 0;
        decimal footremamt = 0;
        decimal footsalesamt = 0;
        decimal footremsalamount = 0;
        decimal foototherrem = 0;
        decimal footRemtotal = 0;
        decimal footnongovttotded = 0;
        decimal footgovtotalded = 0;
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

            LinkButton lblnetamt = (LinkButton)item.FindControl("lblnetamt");
            LinkButton lblded2 = (LinkButton)item.FindControl("lblded2");
            LinkButton lblded1 = (LinkButton)item.FindControl("lblded1");
            LinkButton lbltotrem = (LinkButton)item.FindControl("lbltotrem");
            LinkButton lblotherrem = (LinkButton)item.FindControl("lblotherrem");
            LinkButton lblunit = (LinkButton)item.FindControl("lblunit");
            LinkButton lblsaleremamt = (LinkButton)item.FindControl("lblsaleremamt");
            LinkButton lblremamt = (LinkButton)item.FindControl("lblremamt");
            LinkButton lblactunit = (LinkButton)item.FindControl("lblactunit");

            // Label txtpaidamt = (Label)item.FindControl("txtpaidamt");
            LinkButton lblng1 = (LinkButton)item.FindControl("lblng1");
            LinkButton lblng2 = (LinkButton)item.FindControl("lblng2");
            LinkButton lblng3 = (LinkButton)item.FindControl("lblng3");
            LinkButton lblng4 = (LinkButton)item.FindControl("lblng4");
            LinkButton lblng5 = (LinkButton)item.FindControl("lblng5");

            LinkButton lblg1 = (LinkButton)item.FindControl("lblg1");
            LinkButton lblg2 = (LinkButton)item.FindControl("lblg2");
            LinkButton lblg3 = (LinkButton)item.FindControl("lblg3");
            LinkButton lblg4 = (LinkButton)item.FindControl("lblg4");
            LinkButton lblg5 = (LinkButton)item.FindControl("lblg5");




            footremunit += Convert.ToDecimal(lblactunit.Text);
            footremamt += Convert.ToDecimal(lblremamt.Text);
            if (lblsaleremamt.Text.Length > 0)
            {
                footsalesamt += Convert.ToDecimal(lblsaleremamt.Text);
            }
            if (lblunit.Text.Length > 0)
            {
                footremsalamount += Convert.ToDecimal(lblunit.Text);
            }
            if (lblotherrem.Text.Length > 0)
            {
                foototherrem += Convert.ToDecimal(lblotherrem.Text);
            }
            footRemtotal += Convert.ToDecimal(lbltotrem.Text);
            if (lblded1.Text.Length > 0)
            {
                footnongovttotded += Convert.ToDecimal(lblded1.Text);
            }
            if (lblded2.Text.Length > 0)
            {
                footgovtotalded += Convert.ToDecimal(lblded2.Text);
            }

            footnetpay += Convert.ToDecimal(lblnetamt.Text);
            /////
            // Paidamt += Convert.ToDecimal(txtpaidamt.Text);

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
        Label lblfootremunit = (Label)grdallemp.FooterRow.FindControl("lblfootremunit");
        Label lblfootremamt = (Label)grdallemp.FooterRow.FindControl("lblfootremamt");
        Label lblfootsalesamt = (Label)grdallemp.FooterRow.FindControl("lblfootsalesamt");

        Label lblfootremsalamount = (Label)grdallemp.FooterRow.FindControl("lblfootremsalamount");

        Label lblfoototherrem = (Label)grdallemp.FooterRow.FindControl("lblfoototherrem");

        Label lblfootRemtotal = (Label)grdallemp.FooterRow.FindControl("lblfootRemtotal");
        Label lblfootnongovttotded = (Label)grdallemp.FooterRow.FindControl("lblfootnongovttotded");
        Label lblfootgovtotalded = (Label)grdallemp.FooterRow.FindControl("lblfootgovtotalded");
        Label lblfootnetpay = (Label)grdallemp.FooterRow.FindControl("lblfootnetpay");

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

        //  Label lblfootPaidamt = (Label)grdallemp.FooterRow.FindControl("lblfootPaidamt");

        lblfootremunit.Text = String.Format("{0:n}", footremunit);
        lblfootremamt.Text = String.Format("{0:n}", footremamt);
        lblfootsalesamt.Text = String.Format("{0:n}", footsalesamt);
        lblfootremsalamount.Text = String.Format("{0:n}", footremsalamount);
        lblfoototherrem.Text = String.Format("{0:n}", foototherrem);
        lblfootRemtotal.Text = String.Format("{0:n}", footRemtotal);
        lblfootnongovttotded.Text = String.Format("{0:n}", footnongovttotded);
        lblfootgovtotalded.Text = String.Format("{0:n}", footgovtotalded);
        lblfootnetpay.Text = String.Format("{0:n}", footnetpay);

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
        pnlGS.Visible = true;
        //lblfootPaidamt.Text = String.Format("{0:n}", Paidamt);
    }
    protected void btnSuball_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow item in grdallemp.Rows)
        {
            Label lblEmployeeId = (Label)item.FindControl("lblEmployeeId");
            CheckBox chkpaid = (CheckBox)item.FindControl("chkpaid");
            if (CheckBoxList1.Items[0].Selected == true)
            {
                if (chkpaid.Checked == true)
                {
                    ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(Convert.ToString(lblEmployeeId.Text)));
                    int kl = fillsaldata();
                    MasterSubmit(item.RowIndex.ToString());
                }
            }
            else
            {
                ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(Convert.ToString(lblEmployeeId.Text)));
                int kl = fillsaldata();
                MasterSubmit(item.RowIndex.ToString());
            }

        }
    }
    protected void grdallemp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

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




            HeaderCell = new TableCell();
            HeaderCell.Text = "Actual Remuneration";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);

            if (grdallemp.Columns[6].Visible == true)
            {
                HeaderCell = new TableCell();
                HeaderCell.Text = "Sales based remuneration";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
                HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                HeaderGridRow.Cells.Add(HeaderCell);
            }
            int totde = 2;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            if (grdallemp.Columns[9].Visible == true)
            {
                HeaderCell.ColumnSpan = 2;
            }


            HeaderCell.HorizontalAlign = HorizontalAlign.Center;


            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Less : Deductions";
            HeaderCell.ColumnSpan = GovDisp + totde;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Balance";
            if (PayNo == 0)
            {
                HeaderCell.ColumnSpan = 3;
            }
            else
            {
                HeaderCell.ColumnSpan = 1;
            }
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);


            HeaderGridRow.Cells.Add(HeaderCell);

            if (PayNo > 0)
            {
                HeaderCell = new TableCell();
                HeaderCell.Text = "Payment Details";

                HeaderCell.ColumnSpan = PayNo + 2;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
                HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);


                HeaderGridRow.Cells.Add(HeaderCell);

            }


            grdallemp.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnmpr_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        if (btnmpr.Text == "Printable Version")
        {
            btnmpr.Text = "Hide Printable Version";
            btnpm.Visible = true;
            btnSuball.Visible = false;
            if (grdallemp.Columns[18].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdallemp.Columns[18].Visible = false;
            }
            if (grdallemp.Columns[19].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                grdallemp.Columns[19].Visible = false;
            }


        }
        else
        {
            btnmpr.Text = "Printable Version";

            btnpm.Visible = false;
            btnck();

            if (ViewState["editHide"] != null)
            {
                grdallemp.Columns[18].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                grdallemp.Columns[19].Visible = true;
            }

        }
    }
    protected void ddlperiodtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlwarehouse_SelectedIndexChanged(sender, e);
    }
    public void sendmail(String EmpId)
    {
        try
        {
            string filename = "Sal_" + System.DateTime.Today.Day + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second;

            GeneratePDF(filename);
            filename = filename + ".pdf";
            DataTable dsEmp = select("Select * from EmployeeMaster where EmployeeMasterID='" + EmpId + "'");
            StringBuilder HeadingTable = new StringBuilder();
            if (Convert.ToString(dsEmp.Rows[0]["Email"]) != "")
            {
                HeadingTable.Append("<table width=\"100%\"> ");

                DataTable ds = select("select CompanyWebsiteAddressMaster.*,WareHouseMaster.Name as BName,CityMasterTbl.CityName,StateMasterTbl.Statename,CountryMaster.CountryName from CompanyWebsiteAddressMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId inner join CountryMaster on " +
                          "CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country inner join StateMasterTbl on " +
                          "StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CityMasterTbl on " +
                          "CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City where WareHouseMaster.WareHouseId='" + ddlwarehouse.SelectedValue + "' and AddressTypeMaster.Name='Business Address' ");
                if (ds.Rows.Count > 0)
                {
                    DataTable dtlog = select("select CompanyWebsitMaster.* from CompanyWebsitMaster where whid='" + ddlwarehouse.SelectedValue + "'");

                    HeadingTable.Append("<tr><td width=\"50%\" style=\" align=\"left\" > <img src=\"http://" + Request.Url.Host.ToString() + "/Shoppingcart/images/" + dtlog.Rows[0]["LogoUrl"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td></tr>  ");

                    HeadingTable.Append("</table> ");

                    StringBuilder strheadA = new StringBuilder();
                    strheadA.Append("<table > ");
                    strheadA.Append("<tr><td colspan=\"2\" align=\"left\">Dear  " + dsEmp.Rows[0]["EmployeeName"] + "</td></tr>");
                    strheadA.Append("<tr><td colspan=\"2\" align=\"left\">Please find attached here with pay slip for the pay period from " + sdate + " - to " + edate + " </td></tr>");

                    strheadA.Append("<tr><td colspan=\"2\"></td></tr>");
                    strheadA.Append("<tr><td colspan=\"2\" align=\"left\">The following are the main details of your pay slip for the pay period from " + sdate + " - to " + edate + "</td></tr>");
                    strheadA.Append("<tr><td colspan=\"2\"></td></tr>");
                    strheadA.Append("<tr><td  align=\"left\">Gross Remuneration</td><td  align=\"left\">" + txtbenifitgrossamt.Text + "</td></tr>");
                    strheadA.Append("<tr><td  align=\"left\">Non - Govt Deduction</td><td  align=\"left\">" + txttotded.Text + "</td></tr>");
                    strheadA.Append("<tr><td  align=\"left\">Govt Deduction</td><td  align=\"left\">" + txtgovtottax.Text + "</td></tr>");
                    strheadA.Append("<tr><td  align=\"left\">Net Remuneartion Payable</td><td  align=\"left\">" + txtnettotal.Text + "</td></tr>");
                    strheadA.Append("<tr><td colspan=\"2\"></td></tr>");

                    string Payrolman = "";
                    DataTable dtc = select("  select EmployeeMaster.EmployeeName,DesignationMaster.DesignationName,EmployeeMaster.Email from AttandanceRule inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=AttandanceRule.SeniorEmployeeID inner join " +

                    " DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId  where EmployeeMaster.EmployeeMasterID='" + EmpId + "' and AttandanceRule.StoreId='" + ddlwarehouse.SelectedValue + "'");
                    if (dtc.Rows.Count > 0)
                    {
                        Payrolman = Convert.ToString(dtc.Rows[0]["EmployeeName"]) + "-" + Convert.ToString(dtc.Rows[0]["DesignationName"]) + "-" + Convert.ToString(dtc.Rows[0]["Email"]);
                    }

                    strheadA.Append("<tr><td colspan=\"2\" align=\"left\">If you have any question regarding this pay slip please contact " + Payrolman + "</td></tr>");
                    strheadA.Append("<tr><td colspan=\"2\"></td></tr>");
                    strheadA.Append("<tr><td colspan=\"2\">Thanking you</td></tr>");
                    strheadA.Append("<tr><td colspan=\"2\">" + Convert.ToString(ds.Rows[0]["BName"]) + "</td></tr>");
                    strheadA.Append("<tr><td colspan=\"2\">" + Convert.ToString(ds.Rows[0]["Address1"]) + "</td></tr>");
                    strheadA.Append("<tr><td colspan=\"2\">" + Convert.ToString(ds.Rows[0]["CityName"]) + "," + Convert.ToString(ds.Rows[0]["Statename"]) + "," + Convert.ToString(ds.Rows[0]["CountryName"]) + "," + Convert.ToString(ds.Rows[0]["Zip"]) + "</td></tr>");
                    strheadA.Append("<tr><td colspan=\"2\">" + Convert.ToString(ds.Rows[0]["Phone1"]) + "</td></tr>");
                    strheadA.Append("<tr><td colspan=\"2\"></td></tr>");
                    strheadA.Append("</table> ");

                    if (dtlog.Rows[0]["MasterEmailId"].ToString() != "" && dtlog.Rows[0]["EmailSentDisplayName"].ToString() != "")
                    {
                        MailAddress to = new MailAddress(dsEmp.Rows[0]["Email"].ToString());
                        MailAddress from = new MailAddress("" + dtlog.Rows[0]["MasterEmailId"] + "", "" + dtlog.Rows[0]["EmailSentDisplayName"] + "");
                        MailMessage objEmail = new MailMessage(from, to);

                        objEmail.Subject = "Your Pay Slip for the pay period from " + sdate + " - to " + edate + "";

                        objEmail.Body = strheadA.ToString();
                        objEmail.IsBodyHtml = true;

                        objEmail.Priority = MailPriority.High;
                        string path2 = Server.MapPath("~\\ShoppingCart\\Admin\\TempDoc\\" + filename);

                        Attachment attachFile = new Attachment(path2);
                        objEmail.Attachments.Add(attachFile);

                        SmtpClient client = new SmtpClient();
                        client.Credentials = new NetworkCredential("" + dtlog.Rows[0]["MasterEmailId"] + "", "" + dtlog.Rows[0]["EmailMasterLoginPassword"] + "");
                        client.Host = dtlog.Rows[0]["OutGoingMailServer"].ToString();
                        client.Send(objEmail);
                    }

                }
            }
        }
        catch (Exception e)
        {
            lblmsg.Text = e.ToString();
        }

    }
    protected void GeneratePDF(string filename)
    {
        //this.EnableViewState = false;

        Response.Charset = string.Empty;
        Document document = new Document(PageSize.A4, 0f, 0f, 30f, 30f);


        PdfWriter.GetInstance(document, new FileStream(HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".pdf"), FileMode.Create));

        System.IO.MemoryStream msReport = new System.IO.MemoryStream();

        try
        {
            int rown = 6;
            if (grdmonth.Rows.Count > 0)
            {
                rown = 8;
            }
            else
            {
            }
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddSubject("Export to PDF");
            document.Open();
            iTextSharp.text.Table datatable = new iTextSharp.text.Table(rown);

            datatable.Padding = 1;
            datatable.Spacing = 1;
            datatable.Width = 95;
            float[] headerwidths = new float[rown];
            if (rown == 8)
            {
                headerwidths[0] = 20;
                headerwidths[6] = 10;
                headerwidths[7] = 10;
            }
            else
            {
                headerwidths[0] = 30;

            }
            headerwidths[1] = 7;
            headerwidths[2] = 8;
            headerwidths[3] = 10;
            headerwidths[4] = 14;
            headerwidths[5] = 10;


            datatable.Widths = headerwidths;


            Cell cell = new Cell(new Phrase("Business Name : " + ddlwarehouse.SelectedItem.Text, FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = rown;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            datatable.DefaultCellBorderWidth = 1;
            datatable.DefaultHorizontalAlignment = 1;



            Cell cell2 = new Cell(new Phrase("Employee Name : " + ddlemp.SelectedItem.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Colspan = rown;
            cell2.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell2);

            Cell cell4 = new Cell(new Phrase("Pay Type: " + ddlperiodtype.SelectedItem.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            cell4.Colspan = rown;
            cell4.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell4);

            Cell cell5 = new Cell(new Phrase("Pay Period : " + ddlpayperiod.SelectedItem.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;
            cell5.Colspan = rown;
            cell5.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell5);

            Cell cell6 = new Cell(new Phrase("Employee Salary Slip", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD)));
            cell6.HorizontalAlignment = Element.ALIGN_CENTER;
            cell6.Colspan = rown;
            cell6.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell6);

            datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            if (grdmonth.Rows.Count > 0)
            {
                Cell celltxbg = new Cell(new Phrase(Label2.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltxbg.HorizontalAlignment = Element.ALIGN_LEFT;
                celltxbg.Colspan = rown;
                celltxbg.Border = Rectangle.NO_BORDER;
                datatable.AddCell(celltxbg);

                datatable.AddCell(new Cell(new Phrase(grdmonth.Columns[0].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                datatable.AddCell(new Cell(new Phrase(grdmonth.Columns[1].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grdmonth.Columns[2].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grdmonth.Columns[3].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grdmonth.Columns[4].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grdmonth.Columns[5].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grdmonth.Columns[6].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grdmonth.Columns[7].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                for (int i = 0; i < grdmonth.Rows.Count; i++)
                {

                    TextBox txtremu = (TextBox)grdmonth.Rows[i].FindControl("txtremu");
                    TextBox txtrate = (TextBox)grdmonth.Rows[i].FindControl("txtrate");
                    Label txtperunitname = (Label)grdmonth.Rows[i].FindControl("txtperunitname");
                    TextBox txtcompletemonth = (TextBox)grdmonth.Rows[i].FindControl("txtcompletemonth");

                    TextBox txtcompletemonthamt = (TextBox)grdmonth.Rows[i].FindControl("txtcompletemonthamt");
                    TextBox txttotunit = (TextBox)grdmonth.Rows[i].FindControl("txttotunit");
                    TextBox txtqlifie = (TextBox)grdmonth.Rows[i].FindControl("txtqlifie");
                    TextBox txttotal = (TextBox)grdmonth.Rows[i].FindControl("txttotal");



                    datatable.AddCell(txtremu.Text);
                    datatable.AddCell(txtrate.Text);
                    datatable.AddCell(txtperunitname.Text);
                    datatable.AddCell(txtcompletemonth.Text);
                    datatable.AddCell(txtcompletemonthamt.Text);
                    datatable.AddCell(txttotunit.Text);
                    datatable.AddCell(txtqlifie.Text);
                    datatable.AddCell(txttotal.Text);

                }
            }


            if (grdcal.Rows.Count > 0)
            {
                Cell celltxbg = new Cell(new Phrase(lblhourlycount.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltxbg.HorizontalAlignment = Element.ALIGN_LEFT;
                celltxbg.Colspan = rown;
                celltxbg.Border = Rectangle.NO_BORDER;
                datatable.AddCell(celltxbg);
                datatable.AddCell(new Cell(new Phrase(grdcal.Columns[0].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                datatable.AddCell(new Cell(new Phrase(grdcal.Columns[1].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grdcal.Columns[2].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grdcal.Columns[3].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grdcal.Columns[4].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grdcal.Columns[5].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                //datatable.AddCell(new Cell(new Phrase("Debit(Increase)", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                //datatable.AddCell(new Cell(new Phrase("Balance", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                for (int i = 0; i < grdcal.Rows.Count; i++)
                {

                    TextBox txtremu = (TextBox)grdcal.Rows[i].FindControl("txtremu");
                    TextBox txtrate = (TextBox)grdcal.Rows[i].FindControl("txtrate");
                    Label txtperunitname = (Label)grdcal.Rows[i].FindControl("txtperunitname");
                    TextBox txttotunit = (TextBox)grdcal.Rows[i].FindControl("txttotunit");

                    TextBox txtqlifie = (TextBox)grdcal.Rows[i].FindControl("txtqlifie");
                    TextBox txttotal = (TextBox)grdcal.Rows[i].FindControl("txttotal");

                    datatable.AddCell(txtremu.Text);
                    datatable.AddCell(txtrate.Text);
                    datatable.AddCell(txtperunitname.Text);
                    datatable.AddCell(txttotunit.Text);
                    datatable.AddCell(txtqlifie.Text);
                    datatable.AddCell(txttotal.Text);


                }
            }
            if (grddaily.Rows.Count > 0)
            {
                Cell celltxbg = new Cell(new Phrase(Label1.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltxbg.HorizontalAlignment = Element.ALIGN_LEFT;
                celltxbg.Colspan = rown;
                celltxbg.Border = Rectangle.NO_BORDER;
                datatable.AddCell(celltxbg);
                datatable.AddCell(new Cell(new Phrase(grddaily.Columns[0].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                datatable.AddCell(new Cell(new Phrase(grddaily.Columns[1].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grddaily.Columns[2].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grddaily.Columns[3].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grddaily.Columns[4].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase(grddaily.Columns[5].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                for (int i = 0; i < grddaily.Rows.Count; i++)
                {

                    TextBox txtremu = (TextBox)grddaily.Rows[i].FindControl("txtremu");
                    TextBox txtrate = (TextBox)grddaily.Rows[i].FindControl("txtrate");
                    Label txtperunitname = (Label)grddaily.Rows[i].FindControl("txtperunitname");
                    TextBox txttotunit = (TextBox)grddaily.Rows[i].FindControl("txttotunit");

                    TextBox txtqlifie = (TextBox)grddaily.Rows[i].FindControl("txtqlifie");
                    TextBox txttotal = (TextBox)grddaily.Rows[i].FindControl("txttotal");

                    datatable.AddCell(txtremu.Text);
                    datatable.AddCell(txtrate.Text);
                    datatable.AddCell(txtperunitname.Text);
                    datatable.AddCell(txttotunit.Text);
                    datatable.AddCell(txtqlifie.Text);
                    datatable.AddCell(txttotal.Text);


                }
            }

            ////////table Perce Sales

            iTextSharp.text.Table dtapersal = new iTextSharp.text.Table(5);

            dtapersal.Padding = 1;
            dtapersal.Spacing = 1;
            dtapersal.Width = 95;
            float[] heapersalwidth = new float[5];
            heapersalwidth[0] = 30;
            heapersalwidth[1] = 10;
            heapersalwidth[2] = 10;
            heapersalwidth[3] = 10;
            heapersalwidth[4] = 10;
            //headerwidths[5] = 10;
            dtapersal.Widths = heapersalwidth;
            if (grdispercentage.Rows.Count > 0)
            {
                Cell celltxbg = new Cell(new Phrase(Label3.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltxbg.HorizontalAlignment = Element.ALIGN_LEFT;
                celltxbg.Colspan = 5;
                celltxbg.Border = Rectangle.NO_BORDER;
                dtapersal.AddCell(celltxbg);
                dtapersal.AddCell(new Cell(new Phrase(grdispercentage.Columns[0].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                dtapersal.AddCell(new Cell(new Phrase(grdispercentage.Columns[1].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dtapersal.AddCell(new Cell(new Phrase(grdispercentage.Columns[2].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dtapersal.AddCell(new Cell(new Phrase(grdispercentage.Columns[3].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dtapersal.AddCell(new Cell(new Phrase(grdispercentage.Columns[4].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                //datatable.AddCell(new Cell(new Phrase(grddaily.Columns[5].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                for (int i = 0; i < grdispercentage.Rows.Count; i++)
                {

                    TextBox txtremu = (TextBox)grdispercentage.Rows[i].FindControl("txtremu");
                    TextBox PercentageOf = (TextBox)grdispercentage.Rows[i].FindControl("PercentageOf");
                    Label perof = (Label)grdispercentage.Rows[i].FindControl("perof");
                    TextBox txtbaseamt = (TextBox)grdispercentage.Rows[i].FindControl("txtbaseamt");

                    TextBox txtpaamt = (TextBox)grdispercentage.Rows[i].FindControl("txtpaamt");

                    dtapersal.AddCell(txtremu.Text);
                    dtapersal.AddCell(PercentageOf.Text);
                    dtapersal.AddCell(perof.Text);
                    dtapersal.AddCell(txtbaseamt.Text);
                    dtapersal.AddCell(txtpaamt.Text);



                }
            }
            if (grdsales.Rows.Count > 0)
            {
                Cell celltxbg = new Cell(new Phrase(Label4.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltxbg.HorizontalAlignment = Element.ALIGN_LEFT;
                celltxbg.Colspan = 5;
                celltxbg.Border = Rectangle.NO_BORDER;
                dtapersal.AddCell(celltxbg);
                dtapersal.AddCell(new Cell(new Phrase(grdsales.Columns[0].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                dtapersal.AddCell(new Cell(new Phrase(grdsales.Columns[1].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dtapersal.AddCell(new Cell(new Phrase(grdsales.Columns[2].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dtapersal.AddCell(new Cell(new Phrase(grdsales.Columns[3].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dtapersal.AddCell(new Cell(new Phrase(grdsales.Columns[4].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                //datatable.AddCell(new Cell(new Phrase(grddaily.Columns[5].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                for (int i = 0; i < grdsales.Rows.Count; i++)
                {

                    TextBox txtremu = (TextBox)grdsales.Rows[i].FindControl("txtremu");
                    TextBox PercentageOf = (TextBox)grdsales.Rows[i].FindControl("PercentageOf");
                    Label perof = (Label)grdsales.Rows[i].FindControl("perof");
                    TextBox txtbaseamt = (TextBox)grdsales.Rows[i].FindControl("txtbaseamt");

                    TextBox txtpaamt = (TextBox)grdsales.Rows[i].FindControl("txtpaamt");

                    dtapersal.AddCell(txtremu.Text);
                    dtapersal.AddCell(PercentageOf.Text);
                    dtapersal.AddCell(perof.Text);
                    dtapersal.AddCell(txtbaseamt.Text);
                    dtapersal.AddCell(txtpaamt.Text);

                }
            }

            ////////table LeaveEnca

            iTextSharp.text.Table dtLeaveenc = new iTextSharp.text.Table(4);

            dtLeaveenc.Padding = 1;
            dtLeaveenc.Spacing = 1;
            dtLeaveenc.Width = 95;
            float[] Leaveencwidth = new float[4];
            Leaveencwidth[0] = 70;
            Leaveencwidth[1] = 10;
            Leaveencwidth[2] = 10;
            Leaveencwidth[3] = 10;

            dtLeaveenc.Widths = Leaveencwidth;
            if (grdleaveencash.Rows.Count > 0)
            {
                Cell celltxbg = new Cell(new Phrase(lbll.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltxbg.HorizontalAlignment = Element.ALIGN_LEFT;
                celltxbg.Colspan = 4;
                celltxbg.Border = Rectangle.NO_BORDER;
                dtLeaveenc.AddCell(celltxbg);
                dtLeaveenc.AddCell(new Cell(new Phrase(grdleaveencash.Columns[0].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                dtLeaveenc.AddCell(new Cell(new Phrase(grdleaveencash.Columns[1].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dtLeaveenc.AddCell(new Cell(new Phrase(grdleaveencash.Columns[2].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dtLeaveenc.AddCell(new Cell(new Phrase(grdleaveencash.Columns[3].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                for (int i = 0; i < grdleaveencash.Rows.Count; i++)
                {

                    Label lblleavet = (Label)grdleaveencash.Rows[i].FindControl("lblleavet");
                    TextBox txtleaverate = (TextBox)grdleaveencash.Rows[i].FindControl("txtleaverate");
                    TextBox txtnoofleave = (TextBox)grdleaveencash.Rows[i].FindControl("txtnoofleave");

                    TextBox txtpaamt = (TextBox)grdleaveencash.Rows[i].FindControl("txtpaamt");

                    dtLeaveenc.AddCell(lblleavet.Text);
                    dtLeaveenc.AddCell(txtleaverate.Text);
                    dtLeaveenc.AddCell(txtnoofleave.Text);
                    dtLeaveenc.AddCell(txtpaamt.Text);

                }

            }

            ////////table Taxable ben

            iTextSharp.text.Table dttaxbane = new iTextSharp.text.Table(3);

            dttaxbane.Padding = 1;
            dttaxbane.Spacing = 1;
            dttaxbane.Width = 95;
            float[] taxbenwidth = new float[3];
            taxbenwidth[0] = 80;
            taxbenwidth[1] = 10;
            taxbenwidth[2] = 10;

            dttaxbane.Widths = taxbenwidth;
            if (grdtaxbenifit.Rows.Count > 0)
            {
                Cell celltxbg = new Cell(new Phrase(lbltaxbenlbl.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltxbg.HorizontalAlignment = Element.ALIGN_LEFT;
                celltxbg.Colspan = 3;
                celltxbg.Border = Rectangle.NO_BORDER;
                dttaxbane.AddCell(celltxbg);
                dttaxbane.AddCell(new Cell(new Phrase(grdtaxbenifit.Columns[0].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                dttaxbane.AddCell(new Cell(new Phrase(grdtaxbenifit.Columns[1].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dttaxbane.AddCell(new Cell(new Phrase(grdtaxbenifit.Columns[2].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                for (int i = 0; i < grdtaxbenifit.Rows.Count; i++)
                {

                    Label lbltaxben = (Label)grdtaxbenifit.Rows[i].FindControl("lbltaxben");
                    Label lbltaxbn = (Label)grdtaxbenifit.Rows[i].FindControl("lbltaxbn");
                    Label txtamt = (Label)grdtaxbenifit.Rows[i].FindControl("txtamt");

                    dttaxbane.AddCell(lbltaxben.Text);
                    dttaxbane.AddCell(lbltaxbn.Text);
                    dttaxbane.AddCell(txtamt.Text);
                }
                Cell celltx = new Cell(new Phrase(lbltin.Text + " " + txtbenifitgrossamt.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltx.HorizontalAlignment = Element.ALIGN_RIGHT;
                celltx.Colspan = 3;
                celltx.Border = Rectangle.NO_BORDER;
                dttaxbane.AddCell(celltx);
            }


            Cell cell7v = new Cell(new Phrase(lblcs.Text + " " + txttotincome.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
            cell7v.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell7v.Colspan = 3;
            cell7v.Border = Rectangle.NO_BORDER;
            dttaxbane.AddCell(cell7v);



            ////////table Nonegovdis

            iTextSharp.text.Table dtnongovt = new iTextSharp.text.Table(5);

            dtnongovt.Padding = 1;
            dtnongovt.Spacing = 1;
            dtnongovt.Width = 95;
            float[] nonGovtded = new float[5];
            nonGovtded[0] = 60;
            nonGovtded[1] = 10;
            nonGovtded[2] = 10;
            nonGovtded[3] = 10;
            nonGovtded[4] = 10;
            dtnongovt.Widths = nonGovtded;
            if (grdded.Rows.Count > 0)
            {
                Cell celltxbg = new Cell(new Phrase(lblnonglab.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltxbg.HorizontalAlignment = Element.ALIGN_LEFT;
                celltxbg.Colspan = 5;
                celltxbg.Border = Rectangle.NO_BORDER;
                dtnongovt.AddCell(celltxbg);
                dtnongovt.AddCell(new Cell(new Phrase(grdded.Columns[0].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                dtnongovt.AddCell(new Cell(new Phrase(grdded.Columns[1].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dtnongovt.AddCell(new Cell(new Phrase(grdded.Columns[2].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dtnongovt.AddCell(new Cell(new Phrase(grdded.Columns[3].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));
                dtnongovt.AddCell(new Cell(new Phrase(grdded.Columns[4].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                for (int i = 0; i < grdded.Rows.Count; i++)
                {

                    TextBox txtdedremname = (TextBox)grdded.Rows[i].FindControl("txtdedremname");
                    TextBox FixedAmount = (TextBox)grdded.Rows[i].FindControl("FixedAmount");
                    TextBox PercentageOf = (TextBox)grdded.Rows[i].FindControl("PercentageOf");
                    TextBox perof = (TextBox)grdded.Rows[i].FindControl("perof");
                    TextBox Totamt = (TextBox)grdded.Rows[i].FindControl("Totamt");

                    dtnongovt.AddCell(txtdedremname.Text);
                    dtnongovt.AddCell(FixedAmount.Text);
                    dtnongovt.AddCell(PercentageOf.Text);
                    dtnongovt.AddCell(perof.Text);
                    dtnongovt.AddCell(Totamt.Text);
                }
                Cell celltaxnde = new Cell(new Phrase(lblnongovtlabltot.Text + "" + txttotded.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltaxnde.HorizontalAlignment = Element.ALIGN_RIGHT;
                celltaxnde.Colspan = 5;
                celltaxnde.Border = Rectangle.NO_BORDER;
                dtnongovt.AddCell(celltaxnde);
            }

            ////////table Govdis

            iTextSharp.text.Table dtngovt = new iTextSharp.text.Table(2);

            dtngovt.Padding = 1;
            dtngovt.Spacing = 1;
            dtngovt.Width = 95;
            float[] Govtded = new float[2];
            Govtded[0] = 90;
            Govtded[1] = 10;

            dtngovt.Widths = Govtded;
            if (grdgovded.Rows.Count > 0)
            {
                Cell celltxbg = new Cell(new Phrase(lblgovlable.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltxbg.HorizontalAlignment = Element.ALIGN_LEFT;
                celltxbg.Colspan = 2;
                celltxbg.Border = Rectangle.NO_BORDER;
                dtngovt.AddCell(celltxbg);
                dtngovt.AddCell(new Cell(new Phrase(grdgovded.Columns[0].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                dtngovt.AddCell(new Cell(new Phrase(grdgovded.Columns[1].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))));

                for (int i = 0; i < grdgovded.Rows.Count; i++)
                {

                    Label txtdedremname = (Label)grdgovded.Rows[i].FindControl("lblgovtded");
                    Label FixedAmount = (Label)grdgovded.Rows[i].FindControl("txtgovtamt");

                    dtngovt.AddCell(txtdedremname.Text);
                    dtngovt.AddCell(FixedAmount.Text);


                }
                Cell celltaxnde = new Cell(new Phrase(lblgovtottaxlbl.Text + "" + txtgovtottax.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celltaxnde.HorizontalAlignment = Element.ALIGN_RIGHT;
                celltaxnde.Colspan = 2;
                celltaxnde.Border = Rectangle.NO_BORDER;
                dtngovt.AddCell(celltaxnde);
            }

            iTextSharp.text.Table dtlab = new iTextSharp.text.Table(2);

            dtlab.Padding = 1;
            dtlab.Spacing = 1;
            dtlab.Width = 95;
            float[] dtlabwidth = new float[2];
            dtlabwidth[0] = 90;
            dtlabwidth[1] = 10;

            dtlab.Widths = dtlabwidth;

            Cell cell7 = new Cell(new Phrase(lblletlbl.Text + " " + txtnettotal.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
            cell7.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell7.Colspan = 2;
            cell7.Border = Rectangle.NO_BORDER;
            dtlab.AddCell(cell7);

            document.Add(datatable);
            document.Add(dtapersal);
            document.Add(dttaxbane);
            document.Add(dtnongovt);
            document.Add(dtngovt);
            document.Add(dtlab);
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
        document.Close();
        //document.Close();
        //Response.Clear();
        //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(msReport.ToArray());
        //Response.End();
    }

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /* Verifies that the control is rendered */
    //}

    protected void btnSelectColums_Click(object sender, EventArgs e)
    {
        if (btnSelectColums.Text == "Show more columns")
        {
            lbldiscolumn.Visible = true;
            pnldiscolumn.Visible = true;
            btnSelectColums.Text = "Hide more Columns";
        }
        else
        {
            lbldiscolumn.Visible = false;
            pnldiscolumn.Visible = false;
            btnSelectColums.Text = "Show more columns";
        }
    }
    protected void btnselcolgo_Click(object sender, EventArgs e)
    {
        GovDisp = 0;
        PayNo = 0;
        grdallemp.Columns[11].Visible = false;
        grdallemp.Columns[12].Visible = false;
        grdallemp.Columns[13].Visible = false;
        grdallemp.Columns[14].Visible = false;
        grdallemp.Columns[15].Visible = false;


        grdallemp.Columns[17].Visible = false;
        grdallemp.Columns[18].Visible = false;
        grdallemp.Columns[19].Visible = false;
        grdallemp.Columns[20].Visible = false;
        grdallemp.Columns[21].Visible = false;
        grdallemp.Columns[24].Visible = false;
        grdallemp.Columns[25].Visible = false;
        grdallemp.Columns[26].Visible = false;
        grdallemp.Columns[27].Visible = false;
        if (CheckBoxList1.Items[10].Selected == true)
        {
            PayNo = 1;
            grdallemp.Columns[24].Visible = true;
        }
        if (CheckBoxList1.Items[11].Selected == true)
        {
            PayNo += 1;
            grdallemp.Columns[25].Visible = true;
        }
        if (CheckBoxList1.Items[12].Selected == true)
        {
            PayNo += 1;
            grdallemp.Columns[26].Visible = true;
        }
        if (CheckBoxList1.Items[13].Selected == true)
        {
            PayNo += 1;
            grdallemp.Columns[27].Visible = true;
        }
        ////nongT

        if (CheckBoxList1.Items[0].Selected == true)
        {
            GovDisp += 1;
            grdallemp.Columns[11].Visible = true;
        }
        if (CheckBoxList1.Items[1].Selected == true)
        {
            GovDisp += 1;
            grdallemp.Columns[12].Visible = true;
        }

        if (CheckBoxList1.Items[2].Selected == true)
        {
            GovDisp += 1;
            grdallemp.Columns[13].Visible = true;
        }

        if (CheckBoxList1.Items[3].Selected == true)
        {
            GovDisp += 1;
            grdallemp.Columns[14].Visible = true;
        }

        if (CheckBoxList1.Items[4].Selected == true)
        {
            GovDisp += 1;
            grdallemp.Columns[15].Visible = true;
        }
        ////gT
        if (CheckBoxList1.Items[5].Selected == true)
        {
            GovDisp += 1;
            grdallemp.Columns[17].Visible = true;
        }
        if (CheckBoxList1.Items[6].Selected == true)
        {
            GovDisp += 1;
            grdallemp.Columns[18].Visible = true;
        }

        if (CheckBoxList1.Items[7].Selected == true)
        {
            GovDisp += 1;
            grdallemp.Columns[19].Visible = true;
        }

        if (CheckBoxList1.Items[8].Selected == true)
        {
            GovDisp += 1;
            grdallemp.Columns[20].Visible = true;
        }

        if (CheckBoxList1.Items[9].Selected == true)
        {
            GovDisp += 1;
            grdallemp.Columns[21].Visible = true;
        }
        DataTable dtv = (DataTable)ViewState["ISFill"];
        if (dtv != null)
        {
            if (dtv.Rows.Count > 0)
            {
                fillgd(dtv);
            }
        }

        if (CheckBoxList1.Items[11].Selected == true)
        {
            DataTable dtliab = select("SELECT Distinct (AccountMaster.AccountName)as Classgroup,AccountMaster.AccountId  FROM  GroupCompanyMaster  inner join AccountMaster  on AccountMaster.GroupId=GroupCompanyMaster.GroupId  where AccountMaster.GroupId='1'  and  AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlwarehouse.SelectedValue + "' order by Classgroup");
            foreach (GridViewRow item in grdallemp.Rows)
            {
                DropDownList ddlaccouts = (DropDownList)item.FindControl("ddlaccouts");
                if (ddlaccouts.Items.Count == 0)
                {
                    ddlaccouts.DataSource = dtliab;
                    ddlaccouts.DataTextField = "Classgroup";
                    ddlaccouts.DataValueField = "AccountId";
                    ddlaccouts.DataBind();
                }
                else
                {
                    break;
                }
            }
        }
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
    protected void lblfe1_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");

        DataTable dtempaccid = select("Select AccountMaster.AccountId from AccountMaster inner join EmployeeMaster on EmployeeMaster.AccountId=AccountMaster.Id where EmployeeMaster.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.EmployeeMasterID='" + empid.Text + "'");
        if (dtempaccid.Rows.Count == 0)
        {
            dtempaccid = select("Select AccountMaster.AccountId from AccountMaster inner join EmployeeMaster on EmployeeMaster.AccountId=AccountMaster.AccountId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeMaster.EmployeeMasterID='" + empid.Text + "'");

        }
        if (dtempaccid.Rows.Count > 0)
        {
            string te = "GeneralLedger.aspx?Aid=" + Convert.ToString(dtempaccid.Rows[0]["AccountId"]) + "&Wid=" + ddlwarehouse.SelectedValue + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void lblrate_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");
        string te = "EmployeeMaster.aspx?Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void lblActunit_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");
        string te = "EmployeeAttendanceReport.aspx?Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void lblSlip_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        String Sald = grdallemp.DataKeys[rinrow].Value.ToString();
        Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");
        string te = "MyPaySlip.aspx?Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void chkpaida_CheckedChanged(object sender, EventArgs e)
    {
        pnlamtpaid.Visible = false;
        if (chkpaida.Checked == true)
        {
            pnlamtpaid.Visible = true;
        }
    }
    protected void btncalvd_Click(object sender, EventArgs e)
    {
        rdempallow1.Checked = false;
        rdempallow2.Checked = false;

      bool ab=popupemp();
      if (ab == true)
      {
          lbltempnotallempstore.Text = "";
          lbltempnotallempstore1.Text = "";
          ddlpayperiod_SelectedIndexChanged(sender, e);
      }
      else
      {
          ModalPopupExtender4.Show();
      }
    }
    protected bool popupemp()
    {
        bool ab = true;
        if (yearId == "")
        {
            fillyearid();
        }
        if(yearId != "")
        {
            DataTable dt1 = (DataTable)select(" Select Distinct EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID inner join payperiodtype on payperiodtype.Id=EmployeePayrollMaster.PayPeriodMasterId  inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID " +
                 " where  EmployeePayrollMaster.Whid='" + ddlwarehouse.SelectedValue + "' and payperiodMaster.Id='" + ddlpayperiod.SelectedValue + "' and EmployeeMasterID Not in(Select EmployeeId from SalaryMaster where Whid='" + ddlwarehouse.SelectedValue + "' and payperiodtypeId='" + ddlpayperiod.SelectedValue + "')");
 
           string  allemp= " Select Distinct EmployeePayrollSetupTbl.EmpId from EmployeePayrollSetupTbl inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeePayrollSetupTbl.EmpId inner join payperiodtype on payperiodtype.Id=EmployeePayrollMaster.PayPeriodMasterId  inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID " +
               " where EmployeePayrollSetupTbl.YearId='"+yearId+"' and  EmployeePayrollMaster.Whid='" + ddlwarehouse.SelectedValue + "' and payperiodMaster.Id='" + ddlpayperiod.SelectedValue + "' and EmployeeMasterID Not in(Select EmployeeId from SalaryMaster where Whid='" + ddlwarehouse.SelectedValue + "' and payperiodtypeId='" + ddlpayperiod.SelectedValue + "')";
           lbltempnotallempstore.Text = allemp;
           DataTable dt11 = (DataTable)select(" Select Distinct DesignationName,Departmentname, EmployeeMaster.EmployeeMasterID as EmployeeId,EmployeeMaster.EmployeeName from EmployeeMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.Id=EmployeeMaster.DeptId inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID inner join payperiodtype on payperiodtype.Id=EmployeePayrollMaster.PayPeriodMasterId  inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID " +
                 " where  EmployeePayrollMaster.Whid='" + ddlwarehouse.SelectedValue + "' and payperiodMaster.Id='" + ddlpayperiod.SelectedValue + "' and EmployeeMasterID Not in(Select EmployeeId from SalaryMaster where Whid='" + ddlwarehouse.SelectedValue + "' and payperiodtypeId='" + ddlpayperiod.SelectedValue + "')  and EmployeeMasterID Not in("+allemp+")");
           if (dt11.Rows.Count == 0)
           {
               ab = true;
           }
           else
           {
               ab = false;
               grdemplist.DataSource = dt11;
               grdemplist.DataBind();
               lblnooffemp.Text = Convert.ToString(dt11.Rows.Count);
               lblempnopo.Text =((dt1.Rows.Count) - (dt11.Rows.Count)).ToString();
           }
        }
        return ab;
    }




    protected void rdempallow1_CheckedChanged(object sender, EventArgs e)
    {
        if (rdempallow1.Checked == true)
        {
            lbltempnotallempstore1.Text = lbltempnotallempstore.Text;
            ddlpayperiod_SelectedIndexChanged(sender, e);
            ModalPopupExtender4.Hide();
        }
        else if (rdempallow2.Checked == true)
        {
            lbltempnotallempstore1.Text = "";
            lbltempnotallempstore.Text = "";
            ModalPopupExtender4.Hide();
            string te = "EmployeePayrollSetup.aspx?Whid=" + Convert.ToString(ddlwarehouse.SelectedValue);
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
}

