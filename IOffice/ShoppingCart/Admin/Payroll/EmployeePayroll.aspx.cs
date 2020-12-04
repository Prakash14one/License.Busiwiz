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

public partial class EmployeePayroll : System.Web.UI.Page
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


            if (Request.QueryString["PID"] != "" && Request.QueryString["Wid"] != "")
            {
               
                ddlwarehouse.SelectedValue = Request.QueryString["Wid"];
                ddlwarehouse_SelectedIndexChanged(sender, e);
                fillsaldata(Request.QueryString["PID"]);
               
            }

            else if (Request.QueryString["EID"] != "" && Request.QueryString["Wid"] != "")
            {

                ddlwarehouse.SelectedValue = Request.QueryString["Wid"];
                ddlwarehouse_SelectedIndexChanged(sender, e);
                fillsaldata("");

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
           
        }
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
        DataTable dt = (DataTable)select("select * from [ReportPeriod] where   Whid='" + ddlwarehouse.SelectedValue + "'");
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
       
       
        Label1.Text = "";
    }
   
    protected void Nullable()
    {

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
  
    protected void fillsaldata(string PayperiodId)
    {
        GovDisp = 0;
        PayNo = 0;
        hidesalesname = "";
        hideotherrem = "";
        hide1tded = "";
        hide2ded = "";
        hideotherded = "";
        Label1.Text = "";
     
        hide3ded = "";

        ViewState["ISFill"] = null;
        grdallemp.DataSource = null;
        grdallemp.DataBind();
        string peram = "";
        string pnamefield = "";

        string perach = "";
        lblGr1.Text = "Summary of payroll for the year " + ddlYear.SelectedItem.Text;
        if (Request.QueryString["PID"] !=null)
        {
            grdallemp.Columns[0].HeaderText = "Employee Name";
            pnamefield = "EmployeeMaster.EmployeeName as EmployeeName ";
            peram = " and SalaryMaster.payperiodtypeId='" + PayperiodId + "'";
            perach = " where SalaryGovtDeduction.PayperiodId='" + PayperiodId + "'";
            DataTable dtv = select("Select payperiodMaster.PayperiodName+' - '+Convert(nvarchar,PayperiodStartDate,101) +' - '+ Convert(nvarchar,PayperiodEndDate,101) as PayperiodName from payperiodMaster where ID='" + Request.QueryString["PID"] + "'");
            if (dtv.Rows.Count > 0)
            {
                Label12.Text = "Summary of employee payroll for the pay period - " + Convert.ToString(dtv.Rows[0]["PayperiodName"]);
            }
        }
        else if(Request.QueryString["EID"] !=null)
        {
            DataTable dtv = select("Select EmployeeName from EmployeeMaster where EmployeeMasterId='" + Request.QueryString["EID"] + "'");
            if (dtv.Rows.Count > 0)
            {
                Label12.Text = "The following is the summary of employee payroll for the employee : " + Convert.ToString(dtv.Rows[0]["EmployeeName"]);
            }
            grdallemp.Columns[0].HeaderText = "Pay Period";
            pnamefield = "payperiodMaster.PayperiodName as EmployeeName ";
            DataTable dtxz = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,Convert(nvarchar,EndDate,101)   as EndDate from [ReportPeriod] where  Report_Period_Id='" + ddlYear.SelectedValue + "'");
            if (dtxz.Rows.Count > 0)
            {

                peram = " and PayperiodStartDate>='" + Convert.ToDateTime(dtxz.Rows[0]["StartDate"]).ToShortDateString() + "' and PayperiodEndDate<='" + Convert.ToDateTime(dtxz.Rows[0]["EndDate"]).ToShortDateString() + "'";

            }
            peram += " and SalaryMaster.EmployeeId='" + Request.QueryString["EID"] + "'";
            perach = " where SalaryGovtDeduction.Employeeid='" + Request.QueryString["EID"] + "'";
        }

        string strdata = "Select distinct  SalaryMaster.Id from TranctionMaster inner join SalaryMaster on SalaryMaster.Tid=TranctionMaster.Tranction_Master_Id inner join " +
       " SalaryRemuneration on SalaryRemuneration.SalaryMasterId=SalaryMaster.Id  inner join EmployeeMaster on EmployeeMaster.EmployeeMasterId=SalaryMaster.EmployeeId inner join payperiodMaster on payperiodMaster.Id=SalaryMaster.payperiodtypeId where  SalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "'" + peram;

        DataTable dt1 = (DataTable)select(" Select distinct  SalaryMaster.*, " + pnamefield + " from TranctionMaster inner join SalaryMaster on SalaryMaster.Tid=TranctionMaster.Tranction_Master_Id inner join " +
        " SalaryRemuneration on SalaryRemuneration.SalaryMasterId=SalaryMaster.Id  inner join EmployeeMaster on EmployeeMaster.EmployeeMasterId=SalaryMaster.EmployeeId inner join payperiodMaster on payperiodMaster.Id=SalaryMaster.payperiodtypeId where  SalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "'" + peram);

        if (dt1.Rows.Count > 0)
        {
            pnlcolh.Visible = true;
            Panel1.Visible = true;
            Paydisplay(perach, strdata);
            EventArgs e=new EventArgs();
            object sender=new object();
            btnselcolgo_Click(sender, e);
            foreach (DataRow item in dt1.Rows)
            {


                Label1.Text = "";
                ViewState["paycy"] = "";
                Nullable();
                DataTable dt11 = (DataTable)select(" Select distinct  SalaryMaster.* from TranctionMaster inner join SalaryMaster on SalaryMaster.Tid=TranctionMaster.Tranction_Master_Id inner join " +
               " SalaryRemuneration on SalaryRemuneration.SalaryMasterId=SalaryMaster.Id inner join payperiodMaster on payperiodMaster.Id=SalaryMaster.payperiodtypeId   where SalaryMaster.payperiodtypeId='" + item["payperiodtypeId"] + "' and SalaryMaster.EmployeeId='" + item["EmployeeId"] + "' and  SalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "'" + peram);
                if (dt11.Rows.Count > 0)
                {
                    Recordcheck(dt1);
                    FillAllEmpData(Convert.ToString(item["Id"]), "", Convert.ToString(item["EmployeeName"]), Convert.ToString(item["EmployeeId"]));
                }
               
            }
           
        }

        
    }

    protected void Paydisplay(string per,string noofsal)
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


        DataTable dtbX = select("Select Top(4)  Count(DeductionName) as DedName,DeductionName from SalaryDeduction where SalaryMasterId in(" + noofsal + ") group by DeductionName order by DedName Desc,DeductionName Asc");
        for (int i = 0; i < dtbX.Rows.Count; i++)
        {
            if (i < 4)
            {
                CheckBoxList1.Items[i].Enabled = true;
                
                CheckBoxList1.Items[i].Text = Convert.ToString(dtbX.Rows[i]["DeductionName"]);
                grdallemp.Columns[11 + i].HeaderText = Convert.ToString(dtbX.Rows[i]["DeductionName"]);
            }
            else
            {
                CheckBoxList1.Items[11 + i].Enabled = true;
                break;
            }
        }

        DataTable dtb = select("Select distinct Top(4) Count(SalaryGovtDeduction.PayrollTaxId) as NoG,PayrolltaxMaster.Sortname from  SalaryGovtDeduction inner join " +
        " PayrolltaxMaster on PayrolltaxMaster.Payrolltax_id=SalaryGovtDeduction.PayrollTaxId   " + per + " Group by Sortname order by NoG  Desc,Sortname");
        for (int i = 0; i < dtb.Rows.Count; i++)
        {
            if (i < 4)
            {
                CheckBoxList1.Items[5 + i].Enabled = true;

                CheckBoxList1.Items[5 + i].Text = Convert.ToString(dtb.Rows[i]["Sortname"]);
                grdallemp.Columns[17 + i].HeaderText = Convert.ToString(dtb.Rows[i]["Sortname"]);
            }
            else
            {
                CheckBoxList1.Items[5 + i].Enabled = true;
                break;
            }
        }
    
    }


    protected void Recordcheck(DataTable dt1)
    {



        if (dt1.Rows.Count > 0)
        {
            txttotincome.Text = Convert.ToString(dt1.Rows[0]["TotalIncome"]);
            //txttotded.Text = Convert.ToString(dt1.Rows[0]["TotalDeduction"]);
            txtnettotal.Text = Convert.ToString(dt1.Rows[0]["NetTotal"]);
            txtbenifitgrossamt.Text = Convert.ToString(dt1.Rows[0]["GrossRemu"]);
            txttotded.Text = Convert.ToString(dt1.Rows[0]["NonGovdedamt"]);
            txtgovtottax.Text = Convert.ToString(dt1.Rows[0]["Govdedamt"]);

            ViewState["dk"] = Convert.ToString(dt1.Rows[0]["Id"]);

            DataTable dt2 = (DataTable)select("Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname='Hour' and SalaryMasterId='" + dt1.Rows[0]["Id"] + "' order by Id ASC");

            if (dt2.Rows.Count > 0)
            {
                grdcal.DataSource = dt2;
                grdcal.DataBind();

                pnlhourly.Visible = true;
            }
            DataTable dt4 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname='Day' and SalaryMasterId='" + dt1.Rows[0]["Id"] + "' order by Id ASC");

            if (dt4.Rows.Count > 0)
            {
                grddaily.DataSource = dt4;
                grddaily.DataBind();

                pnldaily.Visible = true;
            }
            DataTable dt5 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName,Totalcomunitmonth as completemonth,maxcompete as completedmonthamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Month','Semi Month','Bi-Week','Week') and SalaryMasterId='" + dt1.Rows[0]["Id"] + "' order by Id ASC");

            if (dt5.Rows.Count > 0)
            {
                grdmonth.DataSource = dt5;
                grdmonth.DataBind();

                pnlmonth.Visible = true;
            }
            DataTable dt6 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as per, SalaryRemuneration.perunitname,totalunit,Actualpayunit as baseamt,remamt as Totamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as Remuname,Totalcomunitmonth as perof,maxcompete as baseamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Is Percentage') and SalaryMasterId='" + dt1.Rows[0]["Id"] + "' order by Id ASC");

            if (dt6.Rows.Count > 0)
            {
                grdispercentage.DataSource = dt6;
                grdispercentage.DataBind();

                pnlispercentage.Visible = true;
            }
            DataTable dt7 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as per, SalaryRemuneration.perunitname,totalunit,Actualpayunit as baseamt,remamt as Totamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as Remuname,Totalcomunitmonth as perof,maxcompete as baseamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Is Percentage of Sales') and SalaryMasterId='" + dt1.Rows[0]["Id"] + "' order by Id ASC");

            if (dt7.Rows.Count > 0)
            {
                grdsales.DataSource = dt7;
                grdsales.DataBind();

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
            DataTable dtgovte = (DataTable)select(" Select distinct SalaryGovtDeduction.Id as CrAccId, SalaryGovtDeduction.Id,Totdedamt as Amount,PayrolltaxMaster.Sortname as DeductionName from  SalaryGovtDeduction inner join  PayrolltaxMaster on PayrolltaxMaster.Payrolltax_id=SalaryGovtDeduction.PayrollTaxId where  SalaryMasterId='" + dt1.Rows[0]["Id"] + "'");

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


        ///Paid info
        DataColumn Paidst = new DataColumn();
        Paidst.ColumnName = "Paid";
        Paidst.DataType = System.Type.GetType("System.String");
        Paidst.AllowDBNull = true;
        dtTemp.Columns.Add(Paidst);
        DataColumn PAc = new DataColumn();
        PAc.ColumnName = "PAC";
        PAc.DataType = System.Type.GetType("System.String");
        PAc.AllowDBNull = true;
        dtTemp.Columns.Add(PAc);

        DataColumn PCQ = new DataColumn();
        PCQ.ColumnName = "PCQ";
        PCQ.DataType = System.Type.GetType("System.String");
        PCQ.AllowDBNull = true;
        dtTemp.Columns.Add(PCQ);

        DataColumn PAMT = new DataColumn();
        PAMT.ColumnName = "PAMT";
        PAMT.DataType = System.Type.GetType("System.Decimal");
        PAMT.AllowDBNull = true;
        dtTemp.Columns.Add(PAMT);


        return dtTemp;
    }
    protected void FillAllEmpData(string SalId, string inno,string empname,string empId)
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
        dtadd["EmployeeName"] = empname;
        dtadd["EmployeeId"] = empId;
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

               // dtadd["Remover"] = "1";
            }
            dtadd["RemId"] = Id.ToString();
            dtadd["Actunit"] = Convert.ToDecimal(dtadd["Actunit"]) + Convert.ToDecimal(txtqlifie.Text);
    
            //dtadd["Actunit"] = txtqlifie.Text;
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
                if (CheckBoxList1.Items[5 + i].Text == lblgovtded.Text)
                {
                    gh =1;
                    dtadd["G" + (i + 1)] = txtgovtamt.Text;
                    break;
                }
               
            }
            if (gh == 0)
            {
                dtadd["Gother"] = txtgovtamt.Text;
            }
                
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
        dtadd["PAMT"] = "0";
        DataTable dtc = select("Select distinct Cast(PaidAmt as Decimal(18,0)) as PaidAmt,AccountMaster.AccountName,Cheqno,PaidAmt from SalaryRelatedPayTbl inner join  AccountMaster on AccountMaster.AccountId=SalaryRelatedPayTbl.RelatedACId where AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and  SalaryRelatedPayTbl.SalaryMasterId='" + SalId + "'");
        if (dtc.Rows.Count > 0)
        {
            dtadd["PAC"] = Convert.ToString(dtc.Rows[0]["AccountName"]);
            dtadd["PCQ"] = Convert.ToString(dtc.Rows[0]["Cheqno"]);
            dtadd["PAMT"] = Convert.ToString(dtc.Rows[0]["PaidAmt"]);
            if (Convert.ToString(dtc.Rows[0]["PaidAmt"]) != "")
            {
                if (Convert.ToDecimal(dtc.Rows[0]["PaidAmt"]) > 0)
                {
                    decimal Av = Math.Round(Convert.ToDecimal(txttotincome.Text), 0);
                    if (Av == Convert.ToDecimal(dtc.Rows[0]["PaidAmt"]))
                    {
                        dtadd["Paid"] = "Fully Paid";
                    }
                    else
                    {
                        dtadd["Paid"] = "Partially Paid";
                    }
                }
                else
                {
                    dtadd["Paid"] = "Unpaid";
                }
            }
        }
        else
        {
            dtadd["Paid"] = "Unpaid";
        }
       //////////////////////
        
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

        decimal Paidamt = 0;

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

            Label txtpaidamt = (Label)item.FindControl("txtpaidamt");
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
            Paidamt += Convert.ToDecimal(txtpaidamt.Text);

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

        Label lblfootPaidamt = (Label)grdallemp.FooterRow.FindControl("lblfootPaidamt");

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

        lblfootPaidamt.Text = String.Format("{0:n}", Paidamt);
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
            HeaderCell.ColumnSpan =GovDisp+ totde;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);
           

            
            HeaderCell = new TableCell();
            HeaderCell.Text = "Balance";
           
             HeaderCell.ColumnSpan =1;
            
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);


            HeaderGridRow.Cells.Add(HeaderCell);

            if (PayNo>0)
            {
                HeaderCell = new TableCell();
                HeaderCell.Text = "Payment Details";

                HeaderCell.ColumnSpan = PayNo;
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
        lblpayperiod.Text = Label12.Text;
        Label1.Text = "";
        if (btnmpr.Text == "Printable Version")
        {
            btnmpr.Text = "Hide Printable Version";
            btnpm.Visible = true;
           


        }
        else
        {
            btnmpr.Text = "Printable Version";

            btnpm.Visible = false;
            pnlGrdemp.Width = 920;

            
        }
    }
   

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
        DataTable dtv=(DataTable)ViewState["ISFill"];
        if (dtv != null)
        {
            if (dtv.Rows.Count > 0)
            {
                fillgd(dtv);
            }
        }
    
    }
 
    protected void lblrate_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");
        string te = "EmployeeSalaryMaster.aspx?Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void lblActunit_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");
        string te = "EmployeeAttendancereportdetail.aspx?Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void lblSlip_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        string salid = grdallemp.DataKeys[rinrow].Value.ToString();

        Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");
        string te = "MyPaySlip.aspx?SL=" + salid + "&Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        

    }
  
}

