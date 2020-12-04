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
using System.IO;
using System.Text;
using System.Data.Common;
using System.Drawing;
using System.ServiceProcess;
using System.Diagnostics;
using System.Windows;


using System.Runtime.InteropServices;
using System.Net;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class Admin_Admin_files_FrmProjectMaster : System.Web.UI.Page
{

    static int temp;
    static DataTable dt;
    DataView dv;
    DataByCompany obj = new DataByCompany();

    DocumentCls1 clsDocument = new DocumentCls1();
    SqlConnection con;

    DateTime lastday;
    EmployeeCls clsEmployee = new EmployeeCls();

    string currentmonth = System.DateTime.Now.Month.ToString();
    string currentdate = System.DateTime.Now.ToShortDateString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int ik = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[ik - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);


        if (!IsPostBack)
        {

            Pagecontrol.dypcontrol(Page, page);
            lblBusiness.Text = "All";
            ViewState["sortOrder"] = "";


            int currentyear = Convert.ToInt32(System.DateTime.Now.Year);
            int currentmonth = Convert.ToInt32(System.DateTime.Now.Month);

            int noofdays = DateTime.DaysInMonth(currentyear, currentmonth);

            lastday = new DateTime(currentyear, currentmonth, noofdays);

            ViewState["lastday"] = lastday;
            txtestartdate.Text = System.DateTime.Now.ToShortDateString();
            txteenddate.Text = lastday.ToShortDateString();

            //   int noofdays1 = DateTime.DaysInMonth(currentyear, currentmonth);


            txtbudgetedamount.Text = "0";
            lblcompany.Text = Session["Comid"].ToString();
            ddlbusiness.Enabled = true;

            btnupdate.Visible = false;
            btncancel.Visible = true;
            RadioButtonList1_SelectedIndexChanged(sender, e);

            RadioButtonList2_SelectedIndexChanged(sender, e);
            filldatebyperiod();
            BindGrid();

        }

    }
    protected void filldatebyperiod()
    {
        string Today, Yesterday, ThisYear;
        Today = Convert.ToString(System.DateTime.Today.ToShortDateString());
        Yesterday = Convert.ToString(System.DateTime.Today.AddDays(-1).ToShortDateString());
        ThisYear = Convert.ToString(System.DateTime.Today.Year.ToString());


        DateTime d1, d2, d3, d4, d5, d6, d7;
        DateTime weekstart, weekend;
        d1 = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
        d2 = Convert.ToDateTime(System.DateTime.Today.AddDays(-1).ToShortDateString());
        d3 = Convert.ToDateTime(System.DateTime.Today.AddDays(-2).ToShortDateString());
        d4 = Convert.ToDateTime(System.DateTime.Today.AddDays(-3).ToShortDateString());
        d5 = Convert.ToDateTime(System.DateTime.Today.AddDays(-4).ToShortDateString());
        d6 = Convert.ToDateTime(System.DateTime.Today.AddDays(-5).ToShortDateString());
        d7 = Convert.ToDateTime(System.DateTime.Today.AddDays(-6).ToShortDateString());

        string ThisWeek = (System.DateTime.Today.DayOfWeek.ToString());
        if (ThisWeek.ToString() == "Monday")
        {
            weekstart = d1;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(ThisWeek) == "Tuesday")
        {
            weekstart = d2;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Wednesday")
        {
            weekstart = d3;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Thursday")
        {
            weekstart = d4;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Friday")
        {
            weekstart = d5;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Saturday")
        {
            weekstart = d6;
            weekend = weekstart.Date.AddDays(+6);

        }
        else
        {
            weekstart = d7;
            weekend = weekstart.Date.AddDays(+6);
        }
        string thisweekstart = weekstart.ToShortDateString();
        ViewState["thisweekstart"] = thisweekstart;
        string thisweekend = weekend.ToShortDateString();
        ViewState["thisweekend"] = thisweekend;

        //.................this week .....................


        DateTime d17, d8, d9, d10, d11, d12, d13;
        DateTime lastweekstart, lastweekend;

        d17 = Convert.ToDateTime(System.DateTime.Today.AddDays(-7).ToShortDateString());
        d8 = Convert.ToDateTime(System.DateTime.Today.AddDays(-8).ToShortDateString());
        d9 = Convert.ToDateTime(System.DateTime.Today.AddDays(-9).ToShortDateString());
        d10 = Convert.ToDateTime(System.DateTime.Today.AddDays(-10).ToShortDateString());
        d11 = Convert.ToDateTime(System.DateTime.Today.AddDays(-11).ToShortDateString());
        d12 = Convert.ToDateTime(System.DateTime.Today.AddDays(-12).ToShortDateString());
        d13 = Convert.ToDateTime(System.DateTime.Today.AddDays(-13).ToShortDateString());
        string thisday = (System.DateTime.Today.DayOfWeek.ToString());
        if (thisday.ToString() == "Monday")
        {
            lastweekstart = d17;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            lastweekstart = d8;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            lastweekstart = d9;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            lastweekstart = d10;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            lastweekstart = d11;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            lastweekstart = d12;
            lastweekend = lastweekstart.Date.AddDays(+6);

        }
        else
        {
            lastweekstart = d13;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        string lastweekstartdate = lastweekstart.ToShortDateString();
        ViewState["lastweekstart"] = lastweekstartdate;
        string lastweekenddate = lastweekend.ToShortDateString();
        ViewState["lastweekend"] = lastweekenddate;

        //.................last week .....................

        DateTime d14, d15, d16, d171, d18, d19, d20;
        DateTime last2weekstart, last2weekend;

        d14 = Convert.ToDateTime(System.DateTime.Today.AddDays(-14).ToShortDateString());
        d15 = Convert.ToDateTime(System.DateTime.Today.AddDays(-15).ToShortDateString());
        d16 = Convert.ToDateTime(System.DateTime.Today.AddDays(-16).ToShortDateString());
        d171 = Convert.ToDateTime(System.DateTime.Today.AddDays(-17).ToShortDateString());
        d18 = Convert.ToDateTime(System.DateTime.Today.AddDays(-18).ToShortDateString());
        d19 = Convert.ToDateTime(System.DateTime.Today.AddDays(-19).ToShortDateString());
        d20 = Convert.ToDateTime(System.DateTime.Today.AddDays(-20).ToShortDateString());

        //string thisday = (System.DateTime.Today.DayOfWeek.ToString());
        if (thisday.ToString() == "Monday")
        {
            last2weekstart = d14;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            last2weekstart = d15;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            last2weekstart = d16;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            last2weekstart = d171;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            last2weekstart = d18;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            last2weekstart = d19;
            last2weekend = last2weekstart.Date.AddDays(+6);

        }
        else
        {
            last2weekstart = d20;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }

        string last2weekstartdate = last2weekstart.ToShortDateString();
        ViewState["last2weekstart"] = last2weekstartdate;
        //string last2weekenddate = last2weekend.ToShortDateString();
        //ViewState["last2week"] = last2weekenddate;



        //------------------this month period-----------------

        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        string thismonthstartdate = thismonthstart.ToShortDateString();
        ViewState["thismonthstartdate"] = thismonthstartdate;
        string thismonthenddate = Today.ToString();
        ViewState["thismonthenddate"] = thismonthenddate;

        //------------------this month period end................



        //-----------------last month period start ---------------

        // int last2monthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 2;



        int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        DateTime lastmonth = Convert.ToDateTime(lastmonthNumber.ToString() + "/1/" + ThisYear.ToString());
        string lastmonthstart = lastmonth.ToShortDateString();
        string lastmonthend = "";

        if (lastmonthNumber == "1" || lastmonthNumber == "3" || lastmonthNumber == "5" || lastmonthNumber == "7" || lastmonthNumber == "8" || lastmonthNumber == "10" || lastmonthNumber == "12")
        {
            lastmonthend = lastmonthNumber + "/31/" + ThisYear.ToString();
        }
        else if (lastmonthNumber == "4" || lastmonthNumber == "6" || lastmonthNumber == "9" || lastmonthNumber == "11")
        {
            lastmonthend = lastmonthNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastmonthend = lastmonthNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                lastmonthend = lastmonthNumber + "/28/" + ThisYear.ToString();
            }
        }

        string lastmonthstartdate = lastmonthstart.ToString();
        ViewState["lastmonthstartdate"] = lastmonthstartdate;
        string lastmonthenddate = lastmonthend.ToString();
        ViewState["lastmonthenddate"] = lastmonthenddate;

        //-----------------last month period end -----------------------

        int last2monthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 2;
        string last2monthNumber = Convert.ToString(last2monthno.ToString());
        DateTime last2month = Convert.ToDateTime(last2monthNumber.ToString() + "/1/" + ThisYear.ToString());
        string last2monthstart = last2month.ToShortDateString();
        ViewState["last2monthstart"] = last2monthstart;

        //-----------------last 2 month period end -----------------------


        //--------------this year period start----------------------


        DateTime thisyearstart = Convert.ToDateTime("1/1/" + ThisYear.ToString());
        DateTime thisyearend = Convert.ToDateTime("12/31/" + ThisYear.ToString());

        string thisyearstartdate = thisyearstart.ToShortDateString();
        ViewState["thisyearstartdate"] = thisyearstartdate;
        string thisyearenddate = thisyearend.ToShortDateString();
        ViewState["thisyearenddate"] = thisyearenddate;

        //---------------this year period end-------------------



        //--------------last year period start----------------------


        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());

        string lastyearstartdate = lastyearstart.ToShortDateString();
        ViewState["lastyearstartdate"] = lastyearstartdate;
        string lastyearenddate = lastyearend.ToShortDateString();
        ViewState["lastyearenddate"] = lastyearenddate;



        //---------------last year period end-------------------

        DateTime last2yearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-2).Year.ToString());
        string last2yearstartdate = last2yearstart.ToShortDateString();
        ViewState["last2yearstartdate"] = last2yearstartdate;

        //---------------last 2 year period -------------------
    }
    protected void ddlreminder_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void fillemp()
    {
        ddlemployee.Items.Clear();
        if (ddlstore.SelectedIndex > -1)
        {
            DataTable ds12 = clsDocument.SelectEmployeeMasterwithDivId("0", 1, ddlstore.SelectedValue);

            ddlemployee.DataSource = ds12;
            ddlemployee.DataTextField = "employeename";
            ddlemployee.DataValueField = "EmployeeMasterID";
            ddlemployee.DataBind();
        }

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string whid = "";
        Int32 success = 0;
        string bus = "0";
        string em = "0";
        statuslable.Text = "";
        if (ddlemployee.SelectedIndex != -1)
        {
            em = ddlemployee.SelectedValue;
        }
        else
        {
            em = "0";
        }
        if (ddlbusiness.SelectedIndex != -1)
        {
            bus = ddlbusiness.SelectedValue;
        }
        else
        {
            bus = "0";
        }
        string part = "0";
        string wg = "0";
        string mg = "0";
        if (ddlparty.SelectedIndex != -1)
        {
            part = ddlparty.SelectedValue;
        }
        else
        {
            part = "0";
        }
        if (ddlw.SelectedIndex != -1)
        {
            wg = ddlw.SelectedValue;
        }
        else
        {
            wg = "0";
        }
        if (ddlm.SelectedIndex != -1)
        {
            mg = ddlm.SelectedValue;
        }
        else
        {
            mg = "0";
        }
        string forkey;
        if (ddlstatus.SelectedValue == "Pending")
        {
            forkey = "0";
        }
        else
        {
            forkey = "1";
        }


        if (RadioButtonList1.SelectedIndex == 0)
        {
            whid = ddlstore.SelectedValue;

            bool access = UserAccess.Usercon("ProjectMaster", forkey, "ProjectId", "", "", "WareHouseMaster.comid", " ProjectMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=ProjectMaster.Whid");
            if (access == true)
            {
                success = ClsProject.SpProjectMasterAddData(Convert.ToString(bus), txtprojectname.Text, ddlstatus.SelectedItem.Text, txtestartdate.Text, txteenddate.Text, Convert.ToString(mg), Convert.ToString(wg), txtdescription.Text, txtbudgetedamount.Text, Convert.ToString(em), "0", ddlstore.SelectedValue, Convert.ToBoolean(chkjob.Checked), part);
                ViewState["success"] = success;
            }
            else
            {
                statuslable.Text = "Sorry, You are not permitted for greater record to Priceplan";
            }

        }
        if (RadioButtonList1.SelectedIndex == 1)
        {
            DataTable df = MainAcocount.SelectWhidwithdeptid(ddlstore.SelectedValue);
            whid = Convert.ToString(df.Rows[0]["Whid"]);
            bool access = UserAccess.Usercon("ProjectMaster", forkey, "ProjectId", "", "", "WareHouseMaster.comid", " ProjectMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=ProjectMaster.Whid");
            if (access == true)
            {
                success = ClsProject.SpProjectMasterAddData(Convert.ToString(bus), txtprojectname.Text, ddlstatus.SelectedItem.Text, txtestartdate.Text, txteenddate.Text, Convert.ToString(mg), Convert.ToString(wg), txtdescription.Text, txtbudgetedamount.Text, Convert.ToString(em), ddlstore.SelectedValue, Convert.ToString(df.Rows[0]["Whid"]), Convert.ToBoolean(chkjob.Checked), part);
                ViewState["success"] = success;
            }
            else
            {
                statuslable.Text = "Sorry, You are not permitted for greater record to Priceplan";
            }
        }
        if (RadioButtonList1.SelectedIndex == 2)
        {
            DataTable df = MainAcocount.SelectWhidwithdeptid(ddlstore.SelectedValue);
            whid = Convert.ToString(df.Rows[0]["Whid"]);
            bool access = UserAccess.Usercon("ProjectMaster", forkey, "ProjectId", "", "", "WareHouseMaster.comid", " ProjectMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=ProjectMaster.Whid");
            if (access == true)
            {
                success = ClsProject.SpProjectMasterAddData(Convert.ToString(bus), txtprojectname.Text, ddlstatus.SelectedItem.Text, txtestartdate.Text, txteenddate.Text, Convert.ToString(mg), Convert.ToString(wg), txtdescription.Text, txtbudgetedamount.Text, Convert.ToString(em), ddlstore.SelectedValue, Convert.ToString(df.Rows[0]["Whid"]), Convert.ToBoolean(chkjob.Checked), part);
                ViewState["success"] = success;
            }
            else
            {
                statuslable.Text = "Sorry, You are not permitted for greater record to Priceplan";
            }
        }

        if (RadioButtonList1.SelectedIndex == 3)
        {
            DataTable df = MainAcocount.SelectWhidwithdeptid(ddlstore.SelectedValue);
            whid = Convert.ToString(df.Rows[0]["Whid"]);
            bool access = UserAccess.Usercon("ProjectMaster", forkey, "ProjectId", "", "", "WareHouseMaster.comid", " ProjectMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=ProjectMaster.Whid");
            if (access == true)
            {
                string insert = "";
                if (ddlempgoaltype.SelectedValue == "1" || ddlempgoaltype.SelectedValue == "2")
                {
                    insert = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txtprojectname.Text + "', '" + ddlstatus.SelectedItem.Text + "', '" + txtestartdate.Text + "','" + txteenddate.Text + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + Convert.ToString(em) + "','" + ddlstore.SelectedValue + "','" + Convert.ToString(df.Rows[0]["Whid"]) + "','" + Convert.ToBoolean(chkjob.Checked) + "', '" + part + "',1)";
                }

                if (ddlempgoaltype.SelectedValue == "3" || ddlempgoaltype.SelectedValue == "4")
                {
                    insert = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txtprojectname.Text + "', '" + ddlstatus.SelectedItem.Text + "', '" + txtestartdate.Text + "','" + txteenddate.Text + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + Convert.ToString(em) + "','" + ddlstore.SelectedValue + "','" + Convert.ToString(df.Rows[0]["Whid"]) + "','" + Convert.ToBoolean(chkjob.Checked) + "', '" + part + "',2)";
                }

                if (ddlempgoaltype.SelectedValue == "5" || ddlempgoaltype.SelectedValue == "6")
                {
                    insert = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txtprojectname.Text + "', '" + ddlstatus.SelectedItem.Text + "', '" + txtestartdate.Text + "','" + txteenddate.Text + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + Convert.ToString(em) + "','" + ddlstore.SelectedValue + "','" + Convert.ToString(df.Rows[0]["Whid"]) + "','" + Convert.ToBoolean(chkjob.Checked) + "', '" + part + "',3)";
                }

                if (ddlempgoaltype.SelectedValue == "7" || ddlempgoaltype.SelectedValue == "8")
                {
                    insert = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txtprojectname.Text + "', '" + ddlstatus.SelectedItem.Text + "', '" + txtestartdate.Text + "','" + txteenddate.Text + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + Convert.ToString(em) + "','" + ddlstore.SelectedValue + "','" + Convert.ToString(df.Rows[0]["Whid"]) + "','" + Convert.ToBoolean(chkjob.Checked) + "', '" + part + "',4)";
                }

                SqlCommand cmd1 = new SqlCommand(insert, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();

                SqlDataAdapter da = new SqlDataAdapter("select max(ProjectId) as ProjectId from ProjectMaster", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                success = Convert.ToInt32(dt.Rows[0]["ProjectId"].ToString());
                ViewState["success"] = success;
            }
            else
            {
                statuslable.Text = "Sorry, You are not permitted for greater record to Priceplan";
            }
        }

        if (success > 0)
        {
            if (chkjob.Checked == true)
            {
                fillwork(whid);
            }

            statuslable.Text = "Record inserted successfully";
            if (chk.Checked == true)
            {
               // string te = "AddDocMaster.aspx?Proid=" + success + "&storeid=" + whid + "";
               // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                btnuplo_Click(sender, e);

            }
            BindGrid();
        }
        else if (success == 0)
        {
            statuslable.Text = "Record already exist";
        }

        else
        {



            statuslable.Text = "Record not inserted successfully";
        }


        EmptyControls();
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
    }

    protected void fillwork1(string Whid)
    {
        SqlDataAdapter dajoje = new SqlDataAdapter("select jobid from JobProjectMasterTbl where projectid='" + ViewState["jobb"].ToString() + "'", con);
        DataTable dtjoje = new DataTable();
        dajoje.Fill(dtjoje);

        if (dtjoje.Rows.Count > 0)
        {
            SqlDataAdapter dass = new SqlDataAdapter("select * from JobMaster where Id='" + Convert.ToString(dtjoje.Rows[0]["jobid"]) + "'", con);
            DataTable dtss = new DataTable();
            dass.Fill(dtss);

            string MasterIns = "Update JobMaster set JobNumber='" + dtss.Rows[0]["JobNumber"].ToString() + "',JobName='" + txtprojectname.Text + "',JobReferenceNo='" + dtss.Rows[0]["JobReferenceNo"].ToString() + "',Note='" + txtdescription.Text + "',JobStartDate='" + txtestartdate.Text + "',TargetDate='" + txteenddate.Text + "',JobEndDate='" + txteenddate.Text + "',StatusId='185',PartyId='" + ddlparty.SelectedValue + "',Whid='" + Whid + "',compid='" + Session["Comid"].ToString() + "',InvWMasterId='" + dtss.Rows[0]["InvWMasterId"].ToString() + "' where Id='" + dtss.Rows[0]["Id"].ToString() + "'";
            SqlCommand cmdmaster = new SqlCommand(MasterIns, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdmaster.ExecuteNonQuery();
            con.Close();
        }
    }

    protected void fillwork(string Whid)
    {
        //string catname = "insert into InventoryCategoryMaster(InventoryCatName,Activestatus,compid) values('" + txtprojectname.Text + "_Cat" + "','" + 1 + "','" + Session["comid"] + "')";
        //SqlCommand mycmd = new SqlCommand(catname, con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //mycmd.ExecuteNonQuery();
        //con.Close();
        //string str123 = "select Max(InventeroyCatId) as InventeroyCatId from InventoryCategoryMaster where compid='" + Session["comid"] + "'";
        //SqlCommand cmd123 = new SqlCommand(str123, con);
        //SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
        //DataTable dt123 = new DataTable();
        //adp123.Fill(dt123);
        //double mainid = Convert.ToDouble(dt123.Rows[0]["InventeroyCatId"].ToString());
        //string queryinsert = "insert into  InventorySubCategoryMaster(InventorySubCatName,InventoryCategoryMasterId,Activestatus)  values('" + txtprojectname.Text + "_SCat" + "'," + mainid + ",'" + 1 + "')";
        //SqlCommand mycmdS = new SqlCommand(queryinsert, con);

        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //mycmdS.ExecuteNonQuery();
        //con.Close();
        //string str123S = "select Max(InventorySubCatId) as InventorySubCatId from InventorySubCategoryMaster where InventoryCategoryMasterId='" + mainid + "'";
        //SqlCommand cmd123S = new SqlCommand(str123S, con);
        //SqlDataAdapter adp123S = new SqlDataAdapter(cmd123S);
        //DataTable dt123S = new DataTable();
        //adp123S.Fill(dt123S);
        //double subcatid = Convert.ToDouble(dt123S.Rows[0]["InventorySubCatId"].ToString());
        //string qurty = "insert into InventoruSubSubCategory(InventorySubSubName,InventorySubCatID,Activestatus)values('" + txtprojectname.Text + "_SSCat" + "'," + subcatid + ",'" + 1 + "')";
        //SqlCommand mycmdSS = new SqlCommand(qurty, con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //mycmdSS.ExecuteNonQuery();
        //con.Close();

        //string str123SS = "select Max(InventorySubSubId) as InventorySubSubId from InventoruSubSubCategory where InventorySubCatID='" + subcatid + "'";
        //SqlCommand cmd123SS = new SqlCommand(str123SS, con);
        //SqlDataAdapter adp123SS = new SqlDataAdapter(cmd123SS);
        //DataTable dt123SS = new DataTable();
        //adp123SS.Fill(dt123SS);
        //double subsubcatid = Convert.ToDouble(dt123SS.Rows[0]["InventorySubSubId"].ToString());

        lblmain.Text = "Work In Process";

        string sgw = "select InventoryCatName,InventeroyCatId from InventoryCategoryMaster where " +
                           " InventoryCatName='" + lblmain.Text + "' and compid='" + Session["comid"] + "' and InventoryCategoryMaster.CatType IS NULL ";
        SqlCommand cgw = new SqlCommand(sgw, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dtgw = new DataTable();
        adgw.Fill(dtgw);

        if (dtgw.Rows.Count > 0)
        {

        }
        else
        {
            try
            {
                string catname = "insert into InventoryCategoryMaster(InventoryCatName,Activestatus,compid) values('" + lblmain.Text + "','" + 1 + "','" + Session["comid"] + "')";
                SqlCommand mycmd = new SqlCommand(catname, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                mycmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ererer)
            {
            }
        }



        double mainid = 0;

        if (dtgw.Rows.Count > 0)
        {
            mainid = Convert.ToDouble(dtgw.Rows[0]["InventeroyCatId"].ToString());

        }
        else
        {
            string str123 = "select Max(InventeroyCatId) as InventeroyCatId from InventoryCategoryMaster";
            SqlCommand cmd123 = new SqlCommand(str123, con);
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
            DataTable dt123 = new DataTable();
            adp123.Fill(dt123);
            mainid = Convert.ToDouble(dt123.Rows[0]["InventeroyCatId"].ToString());
        }

        //lblsubcat.Text = "WIP" + ddlStoreName.Text;
        lblsubcat.Text = "Work In Process";

        string sgw1 = "select InventorySubCatName,InventorySubCatId from InventorySubCategoryMaster where  " +
            " InventorySubCatName='" + lblsubcat.Text + "' and InventoryCategoryMasterId='" + mainid + "' ";
        SqlCommand cgw1 = new SqlCommand(sgw1, con);
        SqlDataAdapter adgw1 = new SqlDataAdapter(cgw1);
        DataTable dtgw1 = new DataTable();
        adgw1.Fill(dtgw1);
        if (dtgw1.Rows.Count > 0)
        {

        }
        else
        {
            string queryinsert = "insert into  InventorySubCategoryMaster(InventorySubCatName,InventoryCategoryMasterId,Activestatus)  values('" + lblsubcat.Text + "'," + mainid + ",'" + 1 + "')";
            SqlCommand mycmd = new SqlCommand(queryinsert, con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            mycmd.ExecuteNonQuery();
            con.Close();
        }

        double subcatid = 0;

        if (dtgw1.Rows.Count > 0)
        {
            subcatid = Convert.ToDouble(dtgw1.Rows[0]["InventorySubCatId"].ToString());

        }
        else
        {
            string str123 = "select Max(InventorySubCatId) as InventorySubCatId from InventorySubCategoryMaster";
            SqlCommand cmd123 = new SqlCommand(str123, con);
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
            DataTable dt123 = new DataTable();
            adp123.Fill(dt123);
            subcatid = Convert.ToDouble(dt123.Rows[0]["InventorySubCatId"].ToString());
        }

        //subsub
        //lblsubsub.Text = lblsubcat.Text + txtJobno.Text;
        lblsubsub.Text = "Work In Process";

        string sgw12 = "select InventorySubSubName,InventorySubSubId from InventoruSubSubCategory where " +
           " InventorySubSubName='" + lblsubsub.Text + "' and InventorySubCatID='" + subcatid + "'";

        SqlCommand cgw12 = new SqlCommand(sgw12, con);
        SqlDataAdapter adgw12 = new SqlDataAdapter(cgw12);
        DataTable dtgw12 = new DataTable();
        adgw12.Fill(dtgw12);
        if (dtgw12.Rows.Count > 0)
        {

        }
        else
        {
            string qurty = "insert into InventoruSubSubCategory(InventorySubSubName,InventorySubCatID,Activestatus)values('" + lblsubsub.Text + "'," + subcatid + ",'" + 1 + "')";
            SqlCommand mycmd = new SqlCommand(qurty, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            mycmd.ExecuteNonQuery();
            con.Close();
        }
        double subsubcatid = 0;

        if (dtgw12.Rows.Count > 0)
        {
            subsubcatid = Convert.ToDouble(dtgw12.Rows[0]["InventorySubSubId"].ToString());
        }
        else
        {
            string str123 = "select Max(InventorySubSubId) as InventorySubSubId from InventoruSubSubCategory";
            SqlCommand cmd123 = new SqlCommand(str123, con);
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
            DataTable dt123 = new DataTable();
            adp123.Fill(dt123);
            subsubcatid = Convert.ToDouble(dt123.Rows[0]["InventorySubSubId"].ToString());
        }

        string insrDetails = "INSERT INTO InventoryDetails (DateStarted,QtyTypeMasterId,UnitTypeId,Weight) " +
                                      " VALUES ('" + Convert.ToDateTime(txtestartdate.Text).ToShortDateString() + "',1,'" + 1 + "','" + 1 + "') ";
        SqlCommand cmdDetails = new SqlCommand(insrDetails, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdDetails.ExecuteNonQuery();
        con.Close();

        SqlCommand mycmd2 = new SqlCommand("select max(Inventory_Details_Id) as Inventory_Details_Id from InventoryDetails", con);
        mycmd2.CommandType = CommandType.Text;

        SqlDataAdapter adp2 = new SqlDataAdapter(mycmd2);
        DataSet ds2 = new DataSet();
        adp2.Fill(ds2);
        ViewState["InvDId"] = ds2.Tables[0].Rows[0][0].ToString();


        string insrMasters = "INSERT INTO InventoryMaster (Name,InventoryDetailsId,InventorySubSubId,MasterActiveStatus,CatType) " +
                        " VALUES ('" + txtprojectname.Text + "_INVM" + "'," + Convert.ToInt32(ViewState["InvDId"]) + ",  " +
                         " " + subsubcatid + ",'" + 1 + "','0') ";
        SqlCommand cmdMasters = new SqlCommand(insrMasters, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdMasters.ExecuteNonQuery();
        con.Close();

        string st2 = "Select Max(InventoryMasterId)from InventoryMaster where InventorySubSubId='" + subsubcatid + "'";
        SqlCommand cmd6 = new SqlCommand(st2, con);
        SqlDataAdapter adp6 = new SqlDataAdapter(cmd6);
        DataSet ds6 = new DataSet();
        adp6.Fill(ds6);
        ViewState["InvMId"] = ds6.Tables[0].Rows[0][0].ToString();
        string s23New = "INSERT INTO InventoryMeasurementUnit(InventoryMasterId,Unit,UnitType) " +
                                               " VALUES   ('" + Convert.ToInt32(ViewState["InvMId"].ToString()) + "' ,'" + 1 + "','" + 1 + "')";


        SqlCommand cmd3New = new SqlCommand(s23New, con);

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd3New.ExecuteNonQuery();
        con.Close();

        string insertBarcode = "Insert Into InventoryBarcodeMaster (InventoryMaster_id)values(" + Convert.ToInt32(ViewState["InvMId"].ToString()) + ")";
        SqlCommand cmdBarcode = new SqlCommand(insertBarcode, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdBarcode.ExecuteNonQuery();
        con.Close();
        RateInsert(Convert.ToString(ViewState["InvMId"]), Whid);
        string strmax = "select Max(InventoryWarehouseMasterId) as InventoryWarehouseMasterId from InventoryWarehouseMasterTbl where  WareHouseId='" + Whid + "'";
        SqlCommand cmdmax = new SqlCommand(strmax, con);
        SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
        DataTable dsmax = new DataTable();
        adpmax.Fill(dsmax);
        if (dsmax.Rows.Count > 0)
        {
            int jobno = 0;
            double inwmasterid = Convert.ToDouble(dsmax.Rows[0]["InventoryWarehouseMasterId"].ToString());
            string str = "Select Max(JobNumber) as JobNumber from JobMaster where Whid='" + Whid + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                if (Convert.ToString(dt.Rows[0]["JobNumber"]) != "")
                {
                    jobno = Convert.ToInt32(dt.Rows[0]["JobNumber"]) + 1;
                    jobno.ToString();
                }
                else
                {
                    jobno = 1;

                }
            }

            string MasterIns = "Insert Into JobMaster(JobNumber,JobName,JobReferenceNo,Note,JobStartDate,TargetDate,JobEndDate,StatusId,PartyId,Whid,compid,InvWMasterId) Values('" + jobno + "','" + txtprojectname.Text + "','" + (jobno) + "Refn" + "','" + txtdescription.Text + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','" + txteenddate.Text + "','185','" + ddlparty.SelectedValue + "','" + Whid + "','" + Session["Comid"].ToString() + "','" + inwmasterid + "') ";
            SqlCommand cmdmaster = new SqlCommand(MasterIns, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdmaster.ExecuteNonQuery();
            con.Close();

            SqlDataAdapter dajo = new SqlDataAdapter("select max(Id) as ID from JobMaster", con);
            DataTable dtjo = new DataTable();
            dajo.Fill(dtjo);

            if (dtjo.Rows.Count > 0)
            {
                SqlCommand cmdjo = new SqlCommand("insert into JobProjectMasterTbl values('" + Convert.ToString(dtjo.Rows[0]["ID"]) + "','" + ViewState["success"].ToString() + "')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdjo.ExecuteNonQuery();
                con.Close();
            }
        }
    }
    protected void RateInsert(string invm, string whid)
    {
        ViewState["invMi"] = invm;

        string st33 = "Select InventoryDetailsId ,InventorySubSubId from InventoryMaster where InventoryMasterId='" + ViewState["invMi"] + "'";
        SqlCommand cmd33 = new SqlCommand(st33, con);
        SqlDataAdapter adp33 = new SqlDataAdapter(cmd33);
        DataTable ds33 = new DataTable();
        adp33.Fill(ds33);
        ViewState["invDi"] = ds33.Rows[0]["InventoryDetailsId"].ToString();

        int i = Convert.ToInt32(ViewState["invMi"]);
        int j = Convert.ToInt32(whid);
        ViewState["CSSCNm"] = i;

        double opeqty = 0;
        double openingrate = 0;
        double total = opeqty * openingrate;
        int s = Convert.ToInt32(ViewState["invMi"]);
        SqlCommand cmd1 = new SqlCommand("select Max(InventoryWarehouseMasterId) as finaltotal from     InventoryWarehouseMasterTbl", con);
        con.Open();
        int k = Convert.ToInt32(cmd1.ExecuteScalar());
        k = k + 1;
        con.Close();
        string insertdetail = "INSERT INTO InventoryWarehouseMasterTbl  (InventoryWarehouseMasterId,InventoryMasterId ,InventoryDetailsId,WareHouseId,Active, ReorderQuantiy,NormalOrderQuantity,ReorderLevel ,QtyOnDateStarted ,QtyOnHand,Rate,OpeningQty,OpeningRate,Total,Weight,UnitTypeId)  VALUES ('" + k + "'," + Convert.ToInt32(ViewState["CSSCNm"].ToString()) + "," + Convert.ToInt32(ViewState["invDi"].ToString()) + ", ' " + Convert.ToInt32(whid) + "','" + 1 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + txtestartdate.Text + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','1','1')";


        SqlCommand insertDetailss = new SqlCommand(insertdetail, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        insertDetailss.ExecuteNonQuery();
        con.Close();


    }

    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderAddnew.Hide();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {

        string whid = "";
        int success = 0;
        string bus = "0";
        string em = "0";
        if (ddlemployee.SelectedIndex != -1)
        {
            em = ddlemployee.SelectedValue;
        }
        else
        {
            em = "0";
        }
        if (ddlbusiness.SelectedIndex != -1)
        {
            bus = ddlbusiness.SelectedValue;

        }
        else
        {
            bus = "0";
        }
        string part = "0";
        string wg = "0";
        string mg = "0";
        if (ddlparty.SelectedIndex != -1)
        {
            part = ddlparty.SelectedValue;
        }
        else
        {
            part = "0";
        }
        if (ddlw.SelectedIndex != -1)
        {
            wg = ddlw.SelectedValue;
        }
        else
        {
            wg = "0";
        }
        if (ddlm.SelectedIndex != -1)
        {
            mg = ddlm.SelectedValue;
        }
        else
        {
            mg = "0";
        }
        string forkey;
        if (ddlstatus.SelectedValue == "Pending")
        {
            forkey = "0";
        }
        else
        {
            forkey = "1";
        }

        if (RadioButtonList1.SelectedIndex == 0)
        {
            whid = ddlstore.SelectedValue;
            bool access = UserAccess.Usercon("ProjectMaster", forkey, "ProjectId", "", "", "WareHouseMaster.comid", " ProjectMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=ProjectMaster.Whid");
            if (access == true)
            {
                success = ClsProject.SpProjectMasterUpdateData(hdnid.Value, Convert.ToString(bus), txtprojectname.Text, ddlstatus.SelectedItem.Text, txtestartdate.Text, txteenddate.Text, mg, wg, txtdescription.Text.ToString(), txtbudgetedamount.Text, em, "0", ddlstore.SelectedValue, Convert.ToBoolean(chkjob.Checked), part);
                ViewState["jobb"] = hdnid.Value;
            }
            else
            {
                statuslable.Text = "Sorry, You are not permitted for greater record to Priceplan";
            }
        }
        if (RadioButtonList1.SelectedIndex == 1)
        {
            DataTable df = MainAcocount.SelectWhidwithdeptid(ddlstore.SelectedValue);
            whid = Convert.ToString(df.Rows[0]["Whid"]);
            bool access = UserAccess.Usercon("ProjectMaster", forkey, "ProjectId", "", "", "WareHouseMaster.comid", " ProjectMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=ProjectMaster.Whid");
            if (access == true)
            {
                success = ClsProject.SpProjectMasterUpdateData(hdnid.Value, Convert.ToString(bus), txtprojectname.Text, ddlstatus.SelectedItem.Text, txtestartdate.Text, txteenddate.Text, mg, wg, txtdescription.Text.ToString(), txtbudgetedamount.Text, em, ddlstore.SelectedValue, Convert.ToString(df.Rows[0]["Whid"]), Convert.ToBoolean(chkjob.Checked), part);
                ViewState["jobb"] = hdnid.Value;
            }
            else
            {
                statuslable.Text = "Sorry, You are not permitted for greater record to Priceplan";
            }

        }
        if (RadioButtonList1.SelectedIndex == 2)
        {
            DataTable df = MainAcocount.SelectWhidwithdeptid(ddlstore.SelectedValue);
            whid = Convert.ToString(df.Rows[0]["Whid"]);
            bool access = UserAccess.Usercon("ProjectMaster", forkey, "ProjectId", "", "", "WareHouseMaster.comid", " ProjectMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=ProjectMaster.Whid");
            if (access == true)
            {
                success = ClsProject.SpProjectMasterUpdateData(hdnid.Value, Convert.ToString(bus), txtprojectname.Text, ddlstatus.SelectedItem.Text, txtestartdate.Text, txteenddate.Text, mg, wg, txtdescription.Text.ToString(), txtbudgetedamount.Text, em, ddlstore.SelectedValue, Convert.ToString(df.Rows[0]["Whid"]), Convert.ToBoolean(chkjob.Checked), part);
                ViewState["jobb"] = hdnid.Value;
            }
            else
            {
                statuslable.Text = "Sorry, You are not permitted for greater record to Priceplan";
            }

        }
        if (RadioButtonList1.SelectedIndex == 3)
        {
            DataTable df = MainAcocount.SelectWhidwithdeptid(ddlstore.SelectedValue);
            whid = Convert.ToString(df.Rows[0]["Whid"]);
            bool access = UserAccess.Usercon("ProjectMaster", forkey, "ProjectId", "", "", "WareHouseMaster.comid", " ProjectMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=ProjectMaster.Whid");
            if (access == true)
            {
                string update = "";
                if (ddlempgoaltype.SelectedValue == "1" || ddlempgoaltype.SelectedValue == "2")
                {
                    // update = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txtprojectname.Text + "', '" + ddlstatus.SelectedItem.Text + "', '" + txtestartdate.Text + "','" + txteenddate.Text + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + Convert.ToString(em) + "','" + ddlstore.SelectedValue + "','" + Convert.ToString(df.Rows[0]["Whid"]) + "','" + Convert.ToBoolean(chkjob.Checked) + "', '" + part + "',1)";
                    update = "update projectmaster  set businessid='" + Convert.ToString(bus) + "',projectname='" + txtprojectname.Text + "',status='" + ddlstatus.SelectedItem.Text + "',estartdate='" + txtestartdate.Text + "',eenddate='" + txteenddate.Text + "',mgmasterid='" + mg + "' ,wtmasterid='" + wg + "' , description='" + txtdescription.Text.ToString() + "',budgetedamount='" + txtbudgetedamount.Text + "',EmployeeID='" + em + "',DeptId='" + ddlstore.SelectedValue + "',Whid='" + Convert.ToString(df.Rows[0]["Whid"]) + "',Addjob='" + Convert.ToBoolean(chkjob.Checked) + "',PartyId='" + part + "',RelatedProjectID=1 where projectid='" + ViewState["updateid"].ToString() + "'";
                }

                if (ddlempgoaltype.SelectedValue == "3" || ddlempgoaltype.SelectedValue == "4")
                {
                    update = "update projectmaster  set businessid='" + Convert.ToString(bus) + "',projectname='" + txtprojectname.Text + "',status='" + ddlstatus.SelectedItem.Text + "',estartdate='" + txtestartdate.Text + "',eenddate='" + txteenddate.Text + "',mgmasterid='" + mg + "' ,wtmasterid='" + wg + "' , description='" + txtdescription.Text.ToString() + "',budgetedamount='" + txtbudgetedamount.Text + "',EmployeeID='" + em + "',DeptId='" + ddlstore.SelectedValue + "',Whid='" + Convert.ToString(df.Rows[0]["Whid"]) + "',Addjob='" + Convert.ToBoolean(chkjob.Checked) + "',PartyId='" + part + "',RelatedProjectID=2 where projectid='" + ViewState["updateid"].ToString() + "'";
                }

                if (ddlempgoaltype.SelectedValue == "5" || ddlempgoaltype.SelectedValue == "6")
                {
                    update = "update projectmaster  set businessid='" + Convert.ToString(bus) + "',projectname='" + txtprojectname.Text + "',status='" + ddlstatus.SelectedItem.Text + "',estartdate='" + txtestartdate.Text + "',eenddate='" + txteenddate.Text + "',mgmasterid='" + mg + "' ,wtmasterid='" + wg + "' , description='" + txtdescription.Text.ToString() + "',budgetedamount='" + txtbudgetedamount.Text + "',EmployeeID='" + em + "',DeptId='" + ddlstore.SelectedValue + "',Whid='" + Convert.ToString(df.Rows[0]["Whid"]) + "',Addjob='" + Convert.ToBoolean(chkjob.Checked) + "',PartyId='" + part + "',RelatedProjectID=3 where projectid='" + ViewState["updateid"].ToString() + "'";
                }

                if (ddlempgoaltype.SelectedValue == "7" || ddlempgoaltype.SelectedValue == "8")
                {
                    update = "update projectmaster  set businessid='" + Convert.ToString(bus) + "',projectname='" + txtprojectname.Text + "',status='" + ddlstatus.SelectedItem.Text + "',estartdate='" + txtestartdate.Text + "',eenddate='" + txteenddate.Text + "',mgmasterid='" + mg + "' ,wtmasterid='" + wg + "' , description='" + txtdescription.Text.ToString() + "',budgetedamount='" + txtbudgetedamount.Text + "',EmployeeID='" + em + "',DeptId='" + ddlstore.SelectedValue + "',Whid='" + Convert.ToString(df.Rows[0]["Whid"]) + "',Addjob='" + Convert.ToBoolean(chkjob.Checked) + "',PartyId='" + part + "',RelatedProjectID=4 where projectid='" + ViewState["updateid"].ToString() + "'";
                }

                SqlCommand cmd1 = new SqlCommand(update, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();

                //SqlDataAdapter da = new SqlDataAdapter("select max(ProjectId) as ProjectId from ProjectMaster", con);
                //DataTable dt = new DataTable();
                //da.Fill(dt);

                success = Convert.ToInt32(ViewState["updateid"]);
                ViewState["jobb"] = success;
            }
            else
            {
                statuslable.Text = "Sorry, You are not permitted for greater record to Priceplan";
            }

        }


        // Response.Write(success);
        //BindGrid();

        if (success > 0)
        {


            if (chkjob.Checked == true)
            {
                fillwork1(whid);
            }

            statuslable.Text = "Record updated successfully";
            if (chk.Checked == true)
            {
                string te = "AddDocMaster.aspx?Proid=" + hdnid.Value.ToString() + "&storeid=" + whid + "";

                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            }
            BindGrid();
        }
        else
        {
            statuslable.Text = "Record alredy exist";
        }


        EmptyControls();

        btncancel.Visible = true;
        btnupdate.Visible = false;

        btnsubmit.Visible = true;

        ddlbusiness.Enabled = true;
        Pnladdnew.Visible = false;
        btnadd.Visible = true;

        lbllegend.Text = "";
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        EmptyControls();
        statuslable.Text = "";
        Pnl1.Visible = false;
        Pnladdnew.Visible = false;
    }


    protected void btncancel_Click(object sender, EventArgs e)
    {
        EmptyControls();
        BindGrid();
        lbllegend.Text = "";

        btncancel.Visible = true;
        btnupdate.Visible = false;

        btnsubmit.Visible = true;

        statuslable.Text = "";
        ddlbusiness.Enabled = true;
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
    }


    private void EmptyControls()
    {
        txtprojectname.Text = "";
        txtdescription.Text = "";
        txtestartdate.Text = DateTime.Now.ToShortDateString();
        txteenddate.Text = ViewState["lastday"].ToString();
        txtbudgetedamount.Text = "0";
        ddlstatus.SelectedIndex = 0;
        chk.Checked = false;
        chkjob.Checked = false;
        Panelempgoal.Visible = false;
    }

    private void BindGrid()
    {
        string st1 = "";

        lblBusiness.Text = "";
        lblBusiness1.Text = "";
        lblDepartmemnt.Text = "";
        lblDivision.Text = "";
        lblEmp.Text = "";
        if (RadioButtonList2.SelectedIndex == 0)
        {
            lblBusiness.Text = ddlsearchByStore.SelectedItem.Text;
            lblBusiness1.Text = "Business Name : " + ddlsearchByStore.SelectedItem.Text;
            if (ddlsearchByStore.SelectedIndex > -1)
            {
                st1 = " and projectmaster.Whid='" + ddlsearchByStore.SelectedValue + "' and projectmaster.DeptId='0' and projectmaster.businessid='0' and projectmaster.EmployeeId='0'";
            }
            else
            {
                st1 = " and  projectmaster.DeptId='0' and projectmaster.businessid='0' and projectmaster.EmployeeId='0'";

            }
        }
        else
        {
            if (ddlsearchByStore.SelectedIndex > 0)
            {
                string[] separator1 = new string[] { ":" };
                string[] strSplitArr1 = ddlsearchByStore.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                lblBusiness.Text = strSplitArr1[0].ToString();

                // lblDepartmemnt.Text = "Department : " + strSplitArr1[1].ToString();
            }
            else
            {
                lblBusiness.Text = ddlsearchByStore.SelectedItem.Text;

            }
        }
        //if (ddlsearchByStore.SelectedIndex > 0)
        //{


        if (RadioButtonList2.SelectedIndex == 1)
        {


            lblDepartmemnt.Text = "Business:Department - " + Convert.ToString(ddlsearchByStore.SelectedItem.Text);

            if (ddlsearchByStore.SelectedValue == "0")
            {
                st1 = " and projectmaster.DeptId>0  and projectmaster.businessid='0' and projectmaster.EmployeeId='0'";


            }
            else
            {
                st1 = " and projectmaster.DeptId='" + ddlsearchByStore.SelectedValue + "'  and projectmaster.businessid='0' and projectmaster.EmployeeId='0'";

            }
        }
        else if (RadioButtonList2.SelectedIndex == 2)
        {

            //lblDepartmemnt.Text = "";
            lblDepartmemnt.Text = "Business:Department - " + ddlsearchByStore.SelectedItem.Text;
            lblDivision.Text = "Division : " + DropDownList2.SelectedItem.Text;

            if (DropDownList2.SelectedValue == "0")
            {
                if (Convert.ToInt32(ddlsearchByStore.SelectedValue) > 0)
                {
                    st1 = " and projectmaster.DeptId='" + ddlsearchByStore.SelectedValue + "' and projectmaster.businessid>0 and projectmaster.EmployeeId='0'";

                }
                else
                {
                    st1 = " and projectmaster.businessid>0 and projectmaster.EmployeeId='0'";
                }
            }
            else
            {
                st1 = " and projectmaster.businessid='" + DropDownList2.SelectedValue + "' and projectmaster.EmployeeId='0'";

            }
        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            lblDepartmemnt.Text = "Business:Department - " + ddlsearchByStore.SelectedItem.Text;
            lblEmp.Text = "Employee  : " + DropDownList3.SelectedItem.Text;
            if (DropDownList3.SelectedValue == "0")
            {
                if (Convert.ToInt32(ddlsearchByStore.SelectedValue) > 0)
                {
                    st1 = " and projectmaster.DeptId='" + ddlsearchByStore.SelectedValue + "' and projectmaster.EmployeeId>'0'";


                }
                else
                {
                    st1 = " and  projectmaster.EmployeeId>0";
                }

            }
            else
            {
                st1 = " and projectmaster.EmployeeId='" + DropDownList3.SelectedValue + "'";

            }
        }
        lblst.Text = "Status  : " + ddlstatusfilter.SelectedItem.Text;
        if (ddlstatusfilter.SelectedIndex > 0)
        {
            st1 += " and projectmaster.Status='" + ddlstatusfilter.SelectedItem.Text + "'";
        }
        // }
        string filc = "";
        if (RadioButtonList2.SelectedIndex == 0)
        {
            filc = "WareHouseMaster.Name as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname,";
            grid.Columns[0].HeaderText = "Business";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = false;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = false;
        }
        else if (RadioButtonList2.SelectedIndex == 1)
        {
            filc = "  LEFT(WareHouseMaster.Name,4) as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname,";
            grid.Columns[0].HeaderText = "Busi";
            grid.Columns[1].HeaderText = "Department";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = true;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = false;
        }
        else if (RadioButtonList2.SelectedIndex == 2)
        {
            filc = "  LEFT(WareHouseMaster.Name,4) as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,LEFT(DepartmentmasterMNC.Departmentname,4)as Departmentname,";
            grid.Columns[0].HeaderText = "Busi";
            grid.Columns[1].HeaderText = "Dept";
            grid.Columns[2].HeaderText = "Division";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = false;
            grid.Columns[2].Visible = true;
            grid.Columns[3].Visible = false;

        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            filc = "  LEFT(WareHouseMaster.Name,4) as Wname, LEFT(BusinessMaster.BusinessName,4) as businessname,EmployeeMaster.EmployeeName,LEFT(DepartmentmasterMNC.Departmentname,4)as Departmentname,";
            grid.Columns[0].HeaderText = "Busi";
            grid.Columns[1].HeaderText = "Dept";
            grid.Columns[2].HeaderText = "Divi";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = false;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = true;
        }
        // DataTable dtt = ClsProject.SpProjectMasterGridfill(st1, filc);

        decimal d111 = 0;
        decimal d222 = 0;
        decimal d33 = 0;

        string str2 = "";

        string streminder = "";
        //----------------------
        if (ddl_reminder.SelectedValue == "1")//Today
        {
            streminder += " and projectmaster.ProjectId IN ( Select ProjectId  From ProjectEvaluation Where ProjectEvaluation.Reminderdate between '" + System.DateTime.Now.ToShortDateString() + "' and  '" + System.DateTime.Now.ToShortDateString() + "' ) ";
        }
        if (ddl_reminder.SelectedValue == "2")
        {
            streminder += "  and projectmaster.ProjectId IN ( Select ProjectId From ProjectEvaluation Where (ProjectEvaluation.Reminderdate<='" + ViewState["thisweekstart"] + "' OR ProjectEvaluation.Reminderdate<='" + ViewState["thisweekend"] + "')   )";
        }
        if (ddl_reminder.SelectedValue == "3")
        {
            streminder += " and projectmaster.ProjectId IN ( Select ProjectId From ProjectEvaluation Where (ProjectEvaluation.Reminderdate<='" + ViewState["last2weekstart"] + "' OR ProjectEvaluation.Reminderdate<='" + ViewState["lastweekend"] + "')  ) ";
        }
        ////////////////////////////////////////
        ////////////////////////////////////


        string strw = "  " + filc + " ProjectId,projectmaster.EmployeeId, projectmaster.Reminder ,projectname,Convert(nvarchar(50),[estartdate],101) as EStartDate,Convert(nvarchar(50),[eenddate],101) as  EEndDate,projectmaster.[Whid],projectmaster.budgetedamount as BudgetedCost,projectmaster.budgetedamount as bdcost,projectmaster.[Addjob],projectmaster.ActualAmount as ActualCost,projectmaster.[status],EmployeeMaster.EmployeeMasterID as DocumentId,DepartmentmasterMNC.id as Dept_Id from projectmaster Left outer join BusinessMaster on projectmaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=projectmaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=projectmaster.DeptId inner join WareHouseMaster on WareHouseMaster.WarehouseId=projectmaster.Whid  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + streminder + " " + st1 + "";
        //order by  projectmaster.ProjectId Desc";

        str2 = " select Count(ProjectMaster.ProjectId) as ci from projectmaster Left outer join BusinessMaster on projectmaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=projectmaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=projectmaster.DeptId inner join WareHouseMaster on WareHouseMaster.WarehouseId=projectmaster.Whid  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
        //order by  projectmaster.ProjectId Desc";   

        grid.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " projectmaster.ProjectId Desc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dtt = GetDataPage(grid.PageIndex, grid.PageSize, sortExpression, strw);

            DataView myDataView = new DataView();
            myDataView = dtt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            grid.DataSource = myDataView;


            for (int rowindex = 0; rowindex < dtt.Rows.Count; rowindex++)
            {

                DataTable dtcrNew11 = ClsProject.SelectOfficeManagerDocumentswithprid(Convert.ToString(dtt.Rows[rowindex]["ProjectId"]));

                dtt.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];

                string stum = "select Addjob from ProjectMaster where projectid='" + Convert.ToString(dtt.Rows[rowindex]["ProjectId"]) + "'";
                SqlDataAdapter da = new SqlDataAdapter(stum, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (Convert.ToString(dt.Rows[0]["Addjob"]) != "")
                {
                    if (Convert.ToBoolean(dt.Rows[0]["Addjob"]) == true)
                    {
                        SqlDataAdapter dax = new SqlDataAdapter("select jobid from JobProjectMasterTbl where projectid='" + dtt.Rows[rowindex]["ProjectId"] + "'", con);
                        DataTable dtx = new DataTable();
                        dax.Fill(dtx);

                        if (dtx.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtx.Rows[0]["jobid"]) != "")
                            {
                                ViewState["rid"] = Convert.ToString(dtx.Rows[0]["jobid"]);

                                fillmaterialissue();
                                overheadallocation();
                                fillgridtemp();

                                Double Workordercost = (Convert.ToDouble(lblTotalSum.Text) + Convert.ToDouble(lbltotaloverheadbyall.Text) + Convert.ToDouble(lbldailyworktotal.Text));
                                lblMyfinal.Text = Workordercost.ToString();

                                dtt.Rows[rowindex]["ActualCost"] = lblMyfinal.Text;
                                dtt.Rows[rowindex]["bdcost"] = "0";
                            }
                        }
                        else
                        {
                            dtt.Rows[rowindex]["ActualCost"] = "0";
                            dtt.Rows[rowindex]["bdcost"] = "0";
                        }
                    }
                    else
                    {
                        string strr1 = "select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join ProjectMaster on ProjectMaster.ProjectId=TaskMaster.ProjectId where ProjectMaster.ProjectId='" + Convert.ToString(dtt.Rows[rowindex]["ProjectId"]) + "'";
                        SqlDataAdapter dar = new SqlDataAdapter(strr1, con);
                        DataTable dtr = new DataTable();
                        dar.Fill(dtr);

                        dtt.Rows[rowindex]["ActualCost"] = dtr.Rows[0]["cost"];

                        if (dtr.Rows[0]["cost"].ToString() != "")
                        {
                            d111 += Convert.ToDecimal(dtr.Rows[0]["cost"]);
                        }

                        SqlDataAdapter dar1 = new SqlDataAdapter("select sum(cast(TaskMaster.Rate as float)) as rcost from TaskMaster inner join ProjectMaster on ProjectMaster.ProjectId=TaskMaster.ProjectId where ProjectMaster.ProjectId='" + Convert.ToString(dtt.Rows[rowindex]["ProjectId"]) + "'", con);
                        DataTable dtr1 = new DataTable();
                        dar1.Fill(dtr1);

                        dtt.Rows[rowindex]["bdcost"] = dtr1.Rows[0]["rcost"];

                        if (dtr1.Rows[0]["rcost"].ToString() != "")
                        {
                            d222 += Convert.ToDecimal(dtr1.Rows[0]["rcost"]);
                        }
                    }
                }

                string d3 = dtt.Rows[rowindex]["BudgetedCost"].ToString();
                d33 += Convert.ToDecimal(d3);
            }



            grid.DataBind();


        }
        else
        {
            grid.DataSource = null;
            grid.DataBind();
        }


        GridViewRow dr = (GridViewRow)grid.FooterRow;

        if (grid.Rows.Count > 0)
        {

            Label lblfooter = (Label)dr.FindControl("lblfooter");
            Label lblfooter1 = (Label)dr.FindControl("lblfooter1");
            Label lblfooter2 = (Label)dr.FindControl("lblfooter2");

            lblfooter.Text = String.Format("{0:n}", Convert.ToDecimal(d33));
            lblfooter2.Text = String.Format("{0:n}", Convert.ToDecimal(d111));
            lblfooter1.Text = String.Format("{0:n}", Convert.ToDecimal(d222));
            //lblfooter2.Text = d111.ToString("###,###.##");
            //lblfooter1.Text = d222.ToString("###,###.##");

        }

        //totalbudgetedcost();
        //if (Convert.ToString(Session["encdata"]) == "Accept")
        //{
        //    foreach (GridViewRow drt in grid.Rows)
        //    {
        //        Label lblmissionname = (Label)drt.FindControl("lblmissionname");

        //        Label lblbudcost = (Label)drt.FindControl("lblbudcost");
        //        Label lbltargetdate = (Label)drt.FindControl("lbltargetdate");
        //        lblmissionname.Text = ClsEncDesc.decryptstring(lblmissionname.Text);
        //        lblbudcost.Text = ClsEncDesc.decryptstring(lblbudcost.Text);
        //        lbltargetdate.Text = ClsEncDesc.decryptstring(lbltargetdate.Text);
        //    }
        //}

    }

    private int GetRowCount(string str)
    {
        int count = 0;
        DataTable dte = new DataTable();
        dte = select(str);
        if (dte.Rows.Count > 0)
        {
            count += Convert.ToInt32(dte.Rows[0]["ci"]);
        }
        ViewState["count"] = count;
        return count;
    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression, string query)
    {
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));
        dt.Columns.Remove("ROW_NUM");
        return dt;

        ViewState["dt"] = dt;
    }

    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
    }

    public struct StObjectiveMaster
    {
        public int deptid;
        public int WId;

        public int desgnid;
        public int masterid;
        public int businessid;
        public int employeeid;
        public string objectivename;
        public string description;
        public string estartdate;
        public string eenddate;
        public string aenddate;
        public decimal budgetedcost;
        public decimal actualcost;
        public decimal shortageexcess;

    }



    //on click on edit button i.e. select index chage of grid
    protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //store selected row id into id
        //statuslable.Text = "";
        //string id = grid.DataKeys[e.NewSelectedIndex].Value.ToString();

        ////create object of structure


        //StObjectiveMaster st;
        ////give data source to object of structure
        //// st = SpObjectiveMasterGetDataStructById(id);
        //DataTable dr = ClsProject.SpProjectMasterGetDataStructById(id);

        //hdnid.Value = dr.Rows[0]["ProjectId"].ToString();
        //txtprojectname.Text = dr.Rows[0]["ProjectName"].ToString();

        //ddlbusiness.Items.Clear();
        //ddlemployee.Items.Clear();
        //Panel5.Visible = false;
        //Panel6.Visible = false;


        //if (Convert.ToInt32(dr.Rows[0]["DeptId"]) > 0)
        //{
        //    lblwname.Text = "Business-Department Name : ";
        //    RadioButtonList1.SelectedIndex = 1;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        ddldept();
        //    }

        //    ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(Convert.ToInt32(dr.Rows[0]["DeptId"]).ToString()));

        //}
        //else
        //{
        //    RadioButtonList1.SelectedIndex = 0;
        //    lblwname.Text = "Business Name : ";
        //    if (Convert.ToString(ViewState["cd"]) != "1")
        //    {
        //        fillstore();
        //    }
        //    ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dr.Rows[0]["Whid"].ToString()));

        //}

        //if (Convert.ToInt32(dr.Rows[0]["employeeid"]) > 0)
        //{
        //    Panel6.Visible = true;


        //    RadioButtonList1.SelectedIndex = 3;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        ddldept();
        //    }
        //    fillemp();
        //    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(Convert.ToInt32(dr.Rows[0]["employeeid"]).ToString()));
        //}
        //else if (Convert.ToInt32(dr.Rows[0]["businessid"]) > 0)
        //{

        //    Panel5.Visible = true;

        //    RadioButtonList1.SelectedIndex = 2;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        ddldept();
        //    }

        //    fillbusiness();
        //    ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(Convert.ToInt32(dr.Rows[0]["businessid"]).ToString()));
        //}

        //if (Convert.ToString(dr.Rows[0]["PartyId"]) != "0")
        //{
        //    chkparty.Checked = true;
        //    chkparty_CheckedChanged(sender, e);
        //    ddlparty.SelectedIndex = ddlparty.Items.IndexOf(ddlparty.Items.FindByValue((dr.Rows[0]["PartyId"].ToString())));

        //}
        //if (Convert.ToString(dr.Rows[0]["WTMasterId"]) != "0")
        //{
        //    chkgoal.Checked = true;

        //    ddltype.SelectedIndex = 1;
        //    chkgoal_CheckedChanged(sender, e);
        //    ddlw.SelectedIndex = ddlw.Items.IndexOf(ddlw.Items.FindByValue((dr.Rows[0]["WTMasterId"].ToString())));
        //}
        //if (Convert.ToString(dr.Rows[0]["MGMasterId"]) != "0")
        //{
        //    chkgoal.Checked = true;

        //    ddltype.SelectedIndex = 0;
        //    chkgoal_CheckedChanged(sender, e);

        //    ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByValue((dr.Rows[0]["MGMasterId"].ToString())));
        //}
        //chkjob.Checked = Convert.ToBoolean(dr.Rows[0]["Addjob"]);

        //ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue((dr.Rows[0]["Status"].ToString())));

        //    txtdescription.Text = Convert.ToString(dr.Rows[0]["description"]);


        //    txtestartdate.Text = Convert.ToDateTime(dr.Rows[0]["Estartdate"].ToString()).ToShortDateString();
        //    txteenddate.Text = Convert.ToDateTime(dr.Rows[0]["Eenddate"].ToString()).ToShortDateString();
        //    txtbudgetedamount.Text = dr.Rows[0]["BudgetedAmount"].ToString();





        //btncancel.Visible = true;
        //btnupdate.Visible = true;

        //btnsubmit.Visible = false;

        //Pnladdnew.Visible = true;
        //btnadd.Visible = false;

    }

    // DELETE DATA FROM BRANDMASTER BY BID[PRIMARY KEY]
    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string id = grid.DataKeys[e.RowIndex].Value.ToString();

        DataTable dts = ClsProject.DeleteProjectbyId(id);

        if (dts.Rows.Count == 0)
        {
            int success = ClsProject.SpProjectMasterDeleteData(id);

            SqlDataAdapter dajoje = new SqlDataAdapter("select jobid from JobProjectMasterTbl where projectid='" + id + "'", con);
            DataTable dtjoje = new DataTable();
            dajoje.Fill(dtjoje);

            if (dtjoje.Rows.Count > 0)
            {
                string MasterIns = "delete from JobMaster where Id='" + dtjoje.Rows[0]["jobid"].ToString() + "'";
                SqlCommand cmdmaster = new SqlCommand(MasterIns, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdmaster.ExecuteNonQuery();
                con.Close();
            }

            if (success > 0)
            {
                statuslable.Text = "Record deleted successfully";
            }
            else
            {
                statuslable.Text = "Record not deleted successfully";
            }

            grid.EditIndex = -1;

            BindGrid();
        }
        else
        {
            statuslable.Text = "Sorry, you are not allow to delete this record";
        }
    }


    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        BindGrid();

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



    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    // This code in main page class... for freetextbox//


    protected void fillstorewithfilter()
    {
        ddlsearchByStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();

        ViewState["cdf"] = "1";
        ddlsearchByStore.DataSource = ds;
        ddlsearchByStore.DataTextField = "Name";
        ddlsearchByStore.DataValueField = "WareHouseId";
        ddlsearchByStore.DataBind();
        //  ddlsearchByStore.Items.Insert(0, "All");
        //   ddlsearchByStore.Items[0].Value = "0";

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlsearchByStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
    protected void fillstore()
    {
        ddlstore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlstore.DataSource = ds;
        ddlstore.DataTextField = "Name";
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataBind();
        //ddlstore.Items.Insert(0, "Select");

        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            //ddlstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "2")
        {
            fillbusiness();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
           
            DDLDepartment();            
            // fillbusiness();
           // fillemp();
            DDLDivision();
            DDLemployeefill();
        }
        if (chkgoal.Checked == true)
        {
            if (RadioButtonList1.SelectedValue == "3")
            {
                ddlempgoaltype_SelectedIndexChanged(sender, e);
            }
            else
            {
                ddltype_SelectedIndexChanged(sender, e);
            }
        }
        if (chkparty.Checked == true)
        {
            fillparty();
        }

    }
    protected void DDLDepartmentfill_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (RadioButtonList1.SelectedValue == "3")
        //{                     
        //    DDLDivision();
        //    DDLemployeefill();
        //}
    }
    protected void ddldept()
    {
        ddlstore.Items.Clear();

        ViewState["cd"] = "2";
        DataTable dsemp = MainAcocount.SelectDepartmentmasterMNCwithCID();
        if (dsemp.Rows.Count > 0)
        {
            ddlstore.DataSource = dsemp;
            ddlstore.DataTextField = "Departmentname";
            ddlstore.DataValueField = "id";
            ddlstore.DataBind();
        }

       
        //ddlstore.Items.Insert(0, "-Select-");
        //ddlstore.Items[0].Value = "0";
        //ddlDepartment_SelectedIndexChanged(sender, e);
    }
    protected void fillbusiness()
    {
        //ddlbusiness.Items.Clear();
        //if (ddldepartment.SelectedIndex > 0)
        //{
        //    DataTable ds1 = clsDocument.SelectDivisionwithStoreIdanddeptId(ddldepartment.SelectedValue, "0", 1);
        //    ddlbusiness.DataSource = ds1;
        //    ddlbusiness.DataTextField = "BusinessName";
        //    ddlbusiness.DataValueField = "BusinessID";
        //    ddlbusiness.DataBind();
        //}
    }


    public void DDLwarehous()
    {
        string finalstr = " SELECT DISTINCT  dbo.WareHouseMaster.Name, dbo.WareHouseMaster.WareHouseId, dbo.WareHouseMaster.comid FROM dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid WHERE (dbo.WareHouseMaster.Status = 1) AND (dbo.DepartmentmasterMNC.Active = 1) AND dbo.WareHouseMaster.comid='"+Session["comid"] +"' ORDER BY dbo.WareHouseMaster.Name ";       
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlstore.DataSource = dtcln;
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataTextField = "Name";
        ddlstore.DataBind();
        ddlstore.Items.Insert(0, "--Select--");
        ddlstore.Items[0].Value = "0";

    }
    public void DDLDepartment()
    {
        DDLDepartmentfill.Items.Clear();
        if (ddlstore.SelectedIndex > 0)
        {
            string finalstr = " SELECT dbo.DepartmentmasterMNC.Departmentid, dbo.DepartmentmasterMNC.Departmentname ,dbo.WareHouseMaster.comid FROM dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid WHERE (dbo.WareHouseMaster.Status = 1) AND (dbo.DepartmentmasterMNC.Active = 1) AND dbo.WareHouseMaster.comid='" + Session["comid"] + "' AND  dbo.WareHouseMaster.WareHouseId='" + ddlstore.SelectedValue + "' ORDER BY dbo.DepartmentmasterMNC.Departmentname ";
            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            DataTable dtcln = new DataTable();
            adpcln.Fill(dtcln);
            DDLDepartmentfill.DataSource = dtcln;
            DDLDepartmentfill.DataValueField = "Departmentid";
            DDLDepartmentfill.DataTextField = "Departmentname";
            DDLDepartmentfill.DataBind();
            DDLDepartmentfill.Items.Insert(0, "--Select--");
            DDLDepartmentfill.Items[0].Value = "0";
        }
        else
        {
            DDLDepartmentfill.Items.Insert(0, "--Select--");
            DDLDepartmentfill.Items[0].Value = "0";
        }
        
        

    }

    public void DDLDivision()
    {
        ddlbusiness.Items.Clear();
        if (DDLDepartmentfill.SelectedIndex > 0)
        {
            string finalstr = " Select distinct BusinessMaster.BusinessID,BusinessMaster.BusinessName as businessname,DepartmentmasterMNC.id from BusinessMaster   inner join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId where  BusinessMaster.DepartmentId='" + DDLDepartmentfill.SelectedValue + "'  and DepartmentmasterMNC.active=1 order by businessname   ";
            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            DataTable dtcln = new DataTable();
            adpcln.Fill(dtcln);
            ddlbusiness.DataSource = dtcln;
            ddlbusiness.DataValueField = "BusinessID";
            ddlbusiness.DataTextField = "BusinessName";
            ddlbusiness.DataBind();
        }
        ddlbusiness.Items.Insert(0, "--Select--");
        ddlbusiness.Items[0].Value = "0";


    }
    public void DDLemployeefill()
    {
        //EmployeeMaster.DeptID=@Whid and
        string str = "";
        if (DDLDepartmentfill.SelectedIndex > 0)
        {
            str += " AND EmployeeMaster.DeptID='" + DDLDepartmentfill.SelectedValue + "'";
        }
        if (ddlstore.SelectedIndex > 0)
        {
            str += " AND EmployeeMaster.Whid='" + ddlstore.SelectedValue + "'";
        }
        
            ddlemployee.Items.Clear();
            string finalstr = " select distinct EmployeeMaster.EmployeeMasterID, EmployeeMaster.EmployeeName as EmployeeName from  EmployeeMaster  inner join DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID where  EmployeeMaster.Active='1' and DepartmentmasterMNC.active='1' " + str + " order by  EmployeeName  ";
            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            DataTable dtcln = new DataTable();
            adpcln.Fill(dtcln);
            ddlemployee.DataSource = dtcln;
            ddlemployee.DataValueField = "EmployeeMasterID";
            ddlemployee.DataTextField = "EmployeeName";
            ddlemployee.DataBind();

            ddlemployee.Items.Insert(0, "--Select--");
            ddlemployee.Items[0].Value = "0";     

    }
     

    public void FilterDepartment()
    {
        ddlsearchByStore.Items.Clear();

        ViewState["cdf"] = "2";
        DataTable dsemp = MainAcocount.SelectDepartmentmasterMNCwithCID();
        if (dsemp.Rows.Count > 0)
        {
            ddlsearchByStore.DataSource = dsemp;
            ddlsearchByStore.DataTextField = "Departmentname";
            ddlsearchByStore.DataValueField = "id";
            ddlsearchByStore.DataBind();
        }

        ddlsearchByStore.Items.Insert(0, "All");
        ddlsearchByStore.Items[0].Value = "0";


    }

    public void filterDivision()
    {

        DropDownList2.Items.Clear();
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            DataTable dt2 = clsDocument.SelectDivisionwithStoreIdanddeptId(ddlsearchByStore.SelectedValue, "0", 1);

            DropDownList2.DataSource = dt2;
            DropDownList2.DataMember = "businessname";
            DropDownList2.DataTextField = "businessname";
            DropDownList2.DataValueField = "BusinessID";
            DropDownList2.DataBind();
        }
        DropDownList2.Items.Insert(0, "All");
        DropDownList2.Items[0].Value = "0";
    }
    protected void Filteremployee()
    {

        DropDownList3.Items.Clear();
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            DataTable ds12 = clsDocument.SelectEmployeeMasterwithDivId("0", 1, ddlsearchByStore.SelectedValue);

            DropDownList3.DataSource = ds12;
            DropDownList3.DataTextField = "EmployeeName";
            DropDownList3.DataValueField = "EmployeeMasterID";
            DropDownList3.DataBind();
        }
        DropDownList3.Items.Insert(0, "All");
        DropDownList3.Items[0].Value = "0";

    }
    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }


    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "vie")
        {
            int mmc = Convert.ToInt32(e.CommandArgument);

            SqlDataAdapter da = new SqlDataAdapter("select Addjob from ProjectMaster where projectid=" + mmc, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (Convert.ToString(dt.Rows[0]["Addjob"]) != "")
            {
                if (Convert.ToBoolean(dt.Rows[0]["Addjob"]) == true)
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("select jobid from JobProjectMasterTbl where projectid=" + mmc, con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    int mm1 = Convert.ToInt32(dt1.Rows[0]["jobid"]);

                    SqlDataAdapter da2 = new SqlDataAdapter("select partyid from ProjectMaster where projectid=" + mmc, con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);

                    int mm2 = Convert.ToInt32(dt2.Rows[0]["partyid"]);

                    string te = "JobProfile.aspx?Id=" + mm1 + "&pid=" + mm2 + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }
                else
                {
                    string te = "frmprojectmasterreport.aspx?projectid=" + mmc;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }
            }
        }

        if (e.CommandName == "Send")
        {

            ViewState["MissionId"] = Convert.ToInt32(e.CommandArgument);


            DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["MissionId"]), 10);
            GridView1.DataSource = dtcrNew11;
            GridView1.DataBind();
            ModalPopupExtenderAddnew.Show();

        }
        else if (e.CommandName == "view")
        {
            int mmc = Convert.ToInt32(e.CommandArgument);

            SqlDataAdapter da = new SqlDataAdapter("select Addjob from ProjectMaster where projectid=" + mmc, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (Convert.ToString(dt.Rows[0]["Addjob"]) != "")
            {
                if (Convert.ToBoolean(dt.Rows[0]["Addjob"]) == true)
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("select jobid from JobProjectMasterTbl where projectid=" + mmc, con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    int mm1 = Convert.ToInt32(dt1.Rows[0]["jobid"]);

                    SqlDataAdapter da2 = new SqlDataAdapter("select partyid from ProjectMaster where projectid=" + mmc, con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);

                    int mm2 = Convert.ToInt32(dt2.Rows[0]["partyid"]);

                    string te = "JobProfile.aspx?Id=" + mm1 + "&pid=" + mm2 + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }
                else
                {
                    string te = "frmprojectmasterreport.aspx?projectid=" + mmc;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }
            }

        }

        if (e.CommandName == "Edit")
        {
            statuslable.Text = "";
            lbllegend.Text = "Edit Project";

            chkgoal.Checked = false;
            chkparty.Checked = false;
            pnlparty.Visible = false;

            pnlgoal.Visible = false;
            pnlmonth.Visible = false;
            pnlweek.Visible = false;
            Panelempgoal.Visible = false;

            //string id = grid.DataKeys[e.NewSelectedIndex].Value.ToString();

            //create object of structure

            string id = Convert.ToString(e.CommandArgument);

            ViewState["updateid"] = id;

            StObjectiveMaster st;
            //give data source to object of structure
            // st = SpObjectiveMasterGetDataStructById(id);
            DataTable dr = ClsProject.SpProjectMasterGetDataStructById(id);

            hdnid.Value = dr.Rows[0]["ProjectId"].ToString();
            txtprojectname.Text = dr.Rows[0]["ProjectName"].ToString();

            ddlbusiness.Items.Clear();
            ddlemployee.Items.Clear();
            Panel5.Visible = false;
            Panel6.Visible = false;


            if (Convert.ToInt32(dr.Rows[0]["DeptId"]) > 0)
            {
                lblwname.Text = "Business-Department Name  ";
                RadioButtonList1.SelectedIndex = 1;
                if (Convert.ToString(ViewState["cd"]) != "2")
                {
                    ddldept();
                }

                ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(Convert.ToInt32(dr.Rows[0]["DeptId"]).ToString()));

            }
            else
            {
                RadioButtonList1.SelectedIndex = 0;
                lblwname.Text = "Business Name  ";
                if (Convert.ToString(ViewState["cd"]) != "1")
                {
                    fillstore();
                }
                ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dr.Rows[0]["Whid"].ToString()));

            }

            if (Convert.ToInt32(dr.Rows[0]["employeeid"]) > 0)
            {
                Panel6.Visible = true;
                Panel5.Visible = true;

                RadioButtonList1.SelectedIndex = 3;
                if (Convert.ToString(ViewState["cd"]) != "2")
                {
                    ddldept();
                }
                fillbusiness();
                ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(Convert.ToInt32(dr.Rows[0]["businessid"]).ToString()));

                fillemp();
                ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(Convert.ToInt32(dr.Rows[0]["employeeid"]).ToString()));
            }
            else if (Convert.ToInt32(dr.Rows[0]["businessid"]) > 0)
            {

                Panel5.Visible = true;

                RadioButtonList1.SelectedIndex = 2;
                if (Convert.ToString(ViewState["cd"]) != "2")
                {
                    ddldept();
                }

                fillbusiness();
                ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(Convert.ToInt32(dr.Rows[0]["businessid"]).ToString()));
            }

            if (Convert.ToString(dr.Rows[0]["PartyId"]) != "0")
            {
                chkparty.Checked = true;
                chkparty_CheckedChanged(sender, e);
                ddlparty.SelectedIndex = ddlparty.Items.IndexOf(ddlparty.Items.FindByValue((dr.Rows[0]["PartyId"].ToString())));

            }
            if (Convert.ToString(dr.Rows[0]["WTMasterId"]) != "0")
            {
                chkgoal.Checked = true;

                if (RadioButtonList1.SelectedValue == "3")
                {
                    if (dr.Rows[0]["RelatedProjectID"].ToString() == "1")
                    {
                        ddlempgoaltype.SelectedIndex = 1;
                        chkgoal_CheckedChanged(sender, e);
                        fillbusinessweekforemployee();
                        ddlw.SelectedIndex = ddlw.Items.IndexOf(ddlw.Items.FindByValue((dr.Rows[0]["WTMasterId"].ToString())));
                    }
                    if (dr.Rows[0]["RelatedProjectID"].ToString() == "2")
                    {
                        ddlempgoaltype.SelectedIndex = 3;
                        chkgoal_CheckedChanged(sender, e);
                        filldepartmentweek();
                        ddlw.SelectedIndex = ddlw.Items.IndexOf(ddlw.Items.FindByValue((dr.Rows[0]["WTMasterId"].ToString())));
                    }
                    if (dr.Rows[0]["RelatedProjectID"].ToString() == "3")
                    {
                        ddlempgoaltype.SelectedIndex = 5;
                        chkgoal_CheckedChanged(sender, e);
                        filldivisionweek();
                        ddlw.SelectedIndex = ddlw.Items.IndexOf(ddlw.Items.FindByValue((dr.Rows[0]["WTMasterId"].ToString())));
                    }
                    if (dr.Rows[0]["RelatedProjectID"].ToString() == "4")
                    {
                        ddlempgoaltype.SelectedIndex = 7;
                        chkgoal_CheckedChanged(sender, e);
                        fillemployeeweek();
                        ddlw.SelectedIndex = ddlw.Items.IndexOf(ddlw.Items.FindByValue((dr.Rows[0]["WTMasterId"].ToString())));
                    }
                }
                else
                {
                    ddltype.SelectedIndex = 1;
                    chkgoal_CheckedChanged(sender, e);
                    ddlw.SelectedIndex = ddlw.Items.IndexOf(ddlw.Items.FindByValue((dr.Rows[0]["WTMasterId"].ToString())));

                }
            }
            if (Convert.ToString(dr.Rows[0]["MGMasterId"]) != "0")
            {
                chkgoal.Checked = true;
                if (RadioButtonList1.SelectedValue == "3")
                {
                    if (dr.Rows[0]["RelatedProjectID"].ToString() == "1")
                    {
                        ddlempgoaltype.SelectedIndex = 0;
                        chkgoal_CheckedChanged(sender, e);
                        fillbusinessyearforemployee();
                        ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByValue((dr.Rows[0]["MGMasterId"].ToString())));
                    }
                    if (dr.Rows[0]["RelatedProjectID"].ToString() == "2")
                    {
                        ddlempgoaltype.SelectedIndex = 2;
                        chkgoal_CheckedChanged(sender, e);
                        filldepartmentssyear();
                        ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByValue((dr.Rows[0]["MGMasterId"].ToString())));
                    }
                    if (dr.Rows[0]["RelatedProjectID"].ToString() == "3")
                    {
                        ddlempgoaltype.SelectedIndex = 4;
                        chkgoal_CheckedChanged(sender, e);
                        filldivisionyear();
                        ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByValue((dr.Rows[0]["MGMasterId"].ToString())));
                    }
                    if (dr.Rows[0]["RelatedProjectID"].ToString() == "4")
                    {
                        ddlempgoaltype.SelectedIndex = 6;
                        chkgoal_CheckedChanged(sender, e);
                        fillemployeeyear();
                        ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByValue((dr.Rows[0]["MGMasterId"].ToString())));
                    }
                }

                else
                {
                    ddltype.SelectedIndex = 0;
                    chkgoal_CheckedChanged(sender, e);
                    ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByValue((dr.Rows[0]["MGMasterId"].ToString())));
                }
            }
            chkjob.Checked = Convert.ToBoolean(dr.Rows[0]["Addjob"]);

            ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue((dr.Rows[0]["Status"].ToString())));

            txtdescription.Text = Convert.ToString(dr.Rows[0]["description"]);


            txtestartdate.Text = Convert.ToDateTime(dr.Rows[0]["Estartdate"].ToString()).ToShortDateString();
            txteenddate.Text = Convert.ToDateTime(dr.Rows[0]["Eenddate"].ToString()).ToShortDateString();
            txtbudgetedamount.Text = dr.Rows[0]["BudgetedAmount"].ToString();

            btncancel.Visible = true;
            btnupdate.Visible = true;

            btnsubmit.Visible = false;

            Pnladdnew.Visible = true;
            btnadd.Visible = false;
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDoucmentMasterByIDwithobj(Convert.ToInt32(e.CommandArgument));

            string docname = dt.Rows[0]["DocumentName"].ToString();
            string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);
            string name = docname.Trim();
            string extension = name.Substring(name.Length - 3);
            if (Convert.ToString(extension) == "pdf")
            {
                Session["ABCDE"] = "ABCDE";

                //                    string popupScript = "<script language='javascript'>" +
                //"newWindow=window.open('ViewDocument.aspx?id='" + e.CommandArgument + ", 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" + "</script>";
                int docid = 0;
                docid = Convert.ToInt32(e.CommandArgument);

                //                    Page.RegisterClientScriptBlock("newWindow", popupScript);
                //LinkButton lnkbtn = (LinkButton)Gridreqinfo.FindControl("LinkButton1");
                //lnkbtn.Attributes.Add("onclick", "window.open('ViewDocument.aspx?id='" + e.CommandArgument + ",, 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')");


                String temp = "ViewDocument.aspx?id=" + docid + "&Siddd=VHDS";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);


                //    Response.Redirect("ViewDocument.aspx?id=" + docid + "&Siddd=VHDS");
            }
            else
            {
                FileInfo file = new FileInfo(filepath);

                if (file.Exists)
                {
                    String temp = "http://license.busiwiz.com/ioffice/Account/" + Session["comid"] + "/UploadedDocuments//" + docname + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('http://license.busiwiz.com/Account/" + Session["comid"] + "/UploadedDocuments/" + docname + "');", true);
                    //Response.ClearContent();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = ReturnExtension(file.Extension.ToLower());
                    //Response.TransmitFile(file.FullName);

                    //Response.End();
                   

                }
            }
        }
    }

    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblwnamefilter.Text = "Business-Department Name  ";
        if (RadioButtonList2.SelectedValue == "0")
        {
            Panel2.Visible = false;
            Panel3.Visible = false;


            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{
            FilterDepartment();
            //  }

        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            Panel2.Visible = true;
            Panel3.Visible = false;

            DropDownList3.Items.Clear();

            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{

            FilterDepartment();
            //  }
            filterDivision();
        }
        if (RadioButtonList2.SelectedValue == "2")
        {

            Panel2.Visible = false;
            Panel3.Visible = true;

            DropDownList2.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{
            FilterDepartment();
            //  }
            Filteremployee();

        }
        if (RadioButtonList2.SelectedValue == "4")
        {

            Panel2.Visible = false;
            Panel3.Visible = false;
            lblwnamefilter.Text = "Business Name  ";

            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "1")
            //{
            fillstorewithfilter();
            // }
        }

        BindGrid();
    }
    protected void ddlsearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "1")
        {

            filterDivision();
        }
        if (RadioButtonList2.SelectedValue == "2")
        {

            Filteremployee();
        }
        BindGrid();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblwname.Text = "Business-Department Name  ";
        imgadddept.Visible = false;
        imgdeptrefresh.Visible = false;

        //    imgadddivision.Visible = false;
        //     imgdivisionrefresh.Visible = false;

        if (RadioButtonList1.SelectedValue == "0")
        {
            lblwname.Text = "Business Name  ";
            Panel8.Visible = true;
            //Panel4.Visible = false;
            Panel5.Visible = false;
            Panel6.Visible = false;
            ddlbusiness.Items.Clear();
            ddlemployee.Items.Clear();
            //if (Convert.ToString(ViewState["cd"]) != "1")
            //{
            fillstore();
            //  }
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            imgadddept.Visible = true;
            imgdeptrefresh.Visible = true;

            Panel8.Visible = true;
            //Panel4.Visible = true;
            Panel5.Visible = false;
            Panel6.Visible = false;
            ddlbusiness.Items.Clear();
            ddlemployee.Items.Clear();
            //if (Convert.ToString(ViewState["cd"]) != "2")
            //{
            ddldept();
            //  }
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            //    imgadddivision.Visible = true;
            //    imgdivisionrefresh.Visible = true;

            lblwname.Text = "Business-Department-Division Name  ";

            Panel8.Visible = true;
            //Panel4.Visible = true;
            Panel5.Visible = true;
            Panel6.Visible = false;
            ddlemployee.Items.Clear();
            //if (Convert.ToString(ViewState["cd"]) != "2")
            //{
            //  filldevesion();
            ddldept();
            //}
           
            fillbusiness();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            pnlgoal.Visible = false;
            if (chkgoal.Checked == true)
            {
                Panelempgoal.Visible = true;
            }
            Panel8.Visible = true;
            ddlbusiness.Items.Clear();
            
            Panel5.Visible = true;
            Panel6.Visible = true;
            DDLwarehous();
            DDLDepartment();
            DDLemployeefill();
           // ddldept();
           // fillbusiness();            
           // fillemp();
        }
        if (chkgoal.Checked == true)
        {
            ddltype_SelectedIndexChanged(sender, e);

        }
        if (chkparty.Checked == true)
        {
            fillparty();
        }

    }
    protected void Button2_CheckedChanged(object sender, EventArgs e)
    {
        if (!Pnl1.Visible)
        {
            Pnl1.Visible = true;
        }
        else
        {
            Pnl1.Visible = false;
        }
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "Departmentaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        ddldept();

    }


    protected void imgadddivision_Click(object sender, ImageClickEventArgs e)
    {
        string te = "FrmBusinessMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgempadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "EmployeeMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }

    protected void imgdivisionrefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillbusiness();

    }
    protected void imgemprefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillemp();

    }


    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;

            grid.AllowPaging = false;
            grid.PageSize = 1000;
            BindGrid();

            if (grid.Columns[14].Visible == true)
            {
                ViewState["Docs"] = "tt";
                grid.Columns[14].Visible = false;
            }
            if (grid.Columns[11].Visible == true)
            {
                ViewState["edith"] = "tt";
                grid.Columns[11].Visible = false;
            }
            if (grid.Columns[12].Visible == true)
            {
                ViewState["deleteh"] = "tt";
                grid.Columns[12].Visible = false;
            }
            if (grid.Columns[13].Visible == true)
            {
                ViewState["viewm"] = "tt";
                grid.Columns[13].Visible = false;
            }
        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;

            grid.AllowPaging = true;
            grid.PageSize = 10;
            BindGrid();

            if (ViewState["Docs"] != null)
            {
                grid.Columns[14].Visible = true;
            }
            if (ViewState["edith"] != null)
            {
                grid.Columns[11].Visible = true;
            }
            if (ViewState["deleteh"] != null)
            {
                grid.Columns[12].Visible = true;
            }
            if (ViewState["viewm"] != null)
            {
                grid.Columns[13].Visible = true;
            }
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {

        Pnladdnew.Visible = true;
        if (Pnladdnew.Visible == true)
        {
            btnadd.Visible = false;
        }
        statuslable.Text = "";
        lbllegend.Text = "Add Project";

        RadioButtonList1.SelectedValue = "0";
        RadioButtonList1_SelectedIndexChanged(sender, e);

        chkgoal.Checked = false;
        chkparty.Checked = false;

        pnlgoal.Visible = false;
        pnlmonth.Visible = false;
        pnlweek.Visible = false;
        pnlparty.Visible = false;

        //txtestartdate.Text = System.DateTime.Now.ToShortDateString();
        //txteenddate.Text = lastday.ToShortDateString();

    }
    protected void chkgoal_CheckedChanged(object sender, EventArgs e)
    {
        Panelempgoal.Visible = false;
        if (chkgoal.Checked == true)
        {
            pnlgoal.Visible = true;
            if (RadioButtonList1.SelectedValue == "3")
            {
                Panelempgoal.Visible = true;
                pnlgoal.Visible = false;
                ddlempgoaltype_SelectedIndexChanged(sender, e);
            }
            else
            {
                ddltype_SelectedIndexChanged(sender, e);
            }
        }
        else
        {
            pnlgoal.Visible = false;
            pnlmonth.Visible = false;
            pnlweek.Visible = false;
            Panelempgoal.Visible = false;
        }

    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlm.Items.Clear();
        ddlw.Items.Clear();
        if (chkgoal.Checked == true)
        {
            if (ddltype.SelectedIndex == 0)
            {
                pnlmonth.Visible = true;
                pnlweek.Visible = false;
            }
            else
            {
                pnlmonth.Visible = false;
                pnlweek.Visible = true;
            }
        }
        fillgoal();
    }
    protected void chkparty_CheckedChanged(object sender, EventArgs e)
    {
        if (chkparty.Checked == true)
        {
            pnlparty.Visible = true;
            fillparty();

        }
        else
        {
            pnlparty.Visible = false;
        }
    }
    protected void fillparty()
    {
        ddlparty.Items.Clear();
        string party = "";
        party = "Select Party_master.PartyId,PartytTypeMaster.PartType+':'+Party_master.Compname as Partyname from Party_master inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId";
        if (RadioButtonList1.SelectedValue == "0")
        {
            party = party + " Where Party_master.Whid='" + ddlstore.SelectedValue + "' and [PartytTypeMaster].[PartType] = 'Customer' order by Partyname";
        }
        else
        {
            party = party + " Where Party_master.Whid in(Select Whid from  DepartmentmasterMNC where Id='" + ddlstore.SelectedValue + "') and [PartytTypeMaster].[PartType] = 'Customer' order by Partyname ";
        }

        SqlDataAdapter adp = new SqlDataAdapter(party, con);
        DataTable dts = new DataTable();
        adp.Fill(dts);
        if (dts.Rows.Count > 0)
        {
            ddlparty.DataSource = dts;
            ddlparty.DataTextField = "Partyname";
            ddlparty.DataValueField = "PartyId";
            ddlparty.DataBind();

        }
        ddlparty.Items.Insert(0, "Select");
        ddlparty.Items[0].Value = "0";


    }
    protected void fillgoal()
    {
        if (ddltype.SelectedIndex == 0)
        {
            //   fillddbymonth();
            if (RadioButtonList1.SelectedValue == "0")
            {
                fillbusinessyear();
            }
            if (RadioButtonList1.SelectedValue == "1")
            {
                filldepartmentssyear();
            }
            if (RadioButtonList1.SelectedValue == "2")
            {
                filldivisionyear();
            }
        }
        if (ddltype.SelectedIndex == 1)
        {
            // fillddbyweek();
            if (RadioButtonList1.SelectedValue == "0")
            {
                fillbusinessweek();
            }
            if (RadioButtonList1.SelectedValue == "1")
            {
                filldepartmentweek();
            }
            if (RadioButtonList1.SelectedValue == "2")
            {
                filldivisionweek();
            }
        }

        if (RadioButtonList1.SelectedValue == "3")
        {
            if (ddlempgoaltype.SelectedIndex == 0)
            {
                fillbusinessyearforemployee();

            }
            if (ddlempgoaltype.SelectedIndex == 1)
            {
                fillbusinessweekforemployee();
            }
            if (ddlempgoaltype.SelectedIndex == 3)
            {
                filldepartmentweek();
            }
            if (ddlempgoaltype.SelectedIndex == 2)
            {
                filldepartmentssyear();
            }
            if (ddlempgoaltype.SelectedIndex == 5)
            {
                filldivisionweek();
            }
            if (ddlempgoaltype.SelectedIndex == 4)
            {
                filldivisionyear();
            }
            if (ddlempgoaltype.SelectedIndex == 7)
            {
                fillemployeeweek();
            }
            if (ddlempgoaltype.SelectedIndex == 6)
            {
                fillemployeeyear();
            }
        }
    }
    protected void fillddbymonth()
    {

        string bus = "0";
        string em = "0";
        if (ddlemployee.SelectedIndex != -1)
        {
            em = ddlemployee.SelectedValue;
        }
        else
        {
            em = "0";
        }
        if (ddlbusiness.SelectedIndex != -1)
        {
            bus = ddlbusiness.SelectedValue;
        }
        else
        {
            bus = "0";
        }

        int flag = 0;
        if (RadioButtonList1.SelectedIndex == 0)
        {
            flag = 0;
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            flag = 1;
        }
        else if (RadioButtonList1.SelectedIndex == 2)
        {
            flag = 2;
        }
        else if (RadioButtonList1.SelectedIndex == 3)
        {
            flag = 3;
        }
        DataTable ds12 = new DataTable();
        if (RadioButtonList1.SelectedIndex == 0)
        {
            ds12 = ClsWDetail.SelectMddfilter(ddlstore.SelectedValue, "0", bus, em, flag);
        }
        else
        {
            ds12 = ClsWDetail.SelectMddfilter("0", ddlstore.SelectedValue, bus, em, flag);

        }
        if (ds12.Rows.Count > 0)
        {
            ddlm.DataSource = ds12;

            ddlm.DataMember = "Title";
            ddlm.DataTextField = "Title";
            ddlm.DataValueField = "masterid";
            ddlm.DataBind();


        }
        ddlm.Items.Insert(0, "Select");
        ddlm.Items[0].Value = "0";

    }
    protected void fillddbyweek()
    {
        //ddlw.Items.Clear();
        string bus = "0";
        string em = "0";
        if (ddlemployee.SelectedIndex != -1)
        {
            em = ddlemployee.SelectedValue;
        }
        else
        {
            em = "0";
        }
        if (ddlbusiness.SelectedIndex != -1)
        {
            bus = ddlbusiness.SelectedValue;
        }
        else
        {
            bus = "0";
        }

        int flag = 0;
        if (RadioButtonList1.SelectedIndex == 0)
        {
            flag = 0;
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            flag = 1;
        }
        else if (RadioButtonList1.SelectedIndex == 2)
        {
            flag = 2;
        }
        else if (RadioButtonList1.SelectedIndex == 3)
        {
            flag = 3;
        }
        DataTable ds12 = new DataTable();
        if (RadioButtonList1.SelectedIndex == 0)
        {
            ds12 = ClsProject.SelectWddfilter(ddlstore.SelectedValue, "0", bus, em, flag);
        }
        else
        {
            ds12 = ClsProject.SelectWddfilter("0", ddlstore.SelectedValue, bus, em, flag);

        }
        if (ds12.Rows.Count > 0)
        {
            ddlw.DataSource = ds12;

            ddlw.DataMember = "Title";
            ddlw.DataTextField = "Title";
            ddlw.DataValueField = "masterid";
            ddlw.DataBind();


        }
        ddlw.Items.Insert(0, "Select");
        ddlw.Items[0].Value = "0";

    }


    protected void ddlstatusfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void lnklob_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        //foreach (GridViewRow grd in grid.Rows)
        //{
        //    CheckBox chkjob = (CheckBox)grd.FindControl("chkjob");
        //    Label Label38 = (Label)grd.FindControl("Label38");
        //    int mm = Convert.ToInt32(Label38.Text);

        //    if (chkjob.Checked == true)
        //    {
        //        string te = "JobProfile.aspx";
        //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        //    }
        //    else
        //    {
        //        string te = "frmprojectmasterreport.aspx?projectid=" + mm;
        //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        //    }
        //}

    }
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void fillbusinessyear()
    {
        ddlm.Items.Clear();

        string y11 = "";
        if (ddlstore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.businessid='" + ddlstore.SelectedValue + "' and YMaster.DepartmentId IS NULL and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlm.DataSource = dt;
            ddlm.DataTextField = "Title1";
            ddlm.DataValueField = "MasterId";
            ddlm.DataBind();
            ddlm.Items.Insert(0, "-Select-");
            ddlm.Items[0].Value = "0";
        }
    }

    protected void filldepartmentssyear()
    {
        ddlm.Items.Clear();

        string y11 = "";
        if (ddlstore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.DepartmentId='" + ddlstore.SelectedValue + "' and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlm.DataSource = dt;
            ddlm.DataTextField = "Title1";
            ddlm.DataValueField = "MasterId";
            ddlm.DataBind();
            ddlm.Items.Insert(0, "-Select-");
            ddlm.Items[0].Value = "0";
        }
    }

    protected void filldivisionyear()
    {
        ddlm.Items.Clear();

        string y11 = "";
        if (ddlbusiness.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.DivisionID='" + ddlbusiness.SelectedValue + "' and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlm.DataSource = dt;
            ddlm.DataTextField = "Title1";
            ddlm.DataValueField = "MasterId";
            ddlm.DataBind();
            ddlm.Items.Insert(0, "-Select-");
            ddlm.Items[0].Value = "0";
        }
    }

    protected void filldevesion()
    {
        string dev = "select  distinct BusinessMaster.BusinessID,BusinessName,WareHouseMaster.Name,DepartmentmasterMNC.Departmentname, WareHouseMaster.Name +' : '+ DepartmentmasterMNC.Departmentname +' : '+ BusinessMaster.BusinessName  as Divisionname  from  BusinessMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid    where  WarehouseMaster.Status=1 AND BusinessMaster.company_id='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName";
        SqlDataAdapter ddev = new SqlDataAdapter(dev, con);
        DataTable dtdev = new DataTable();
        ddev.Fill(dtdev);

      
            ddlstore.DataSource = dtdev;
            ddlstore.DataTextField = "Divisionname";
            ddlstore.DataValueField = "BusinessID";
            ddlstore.DataBind();
        
    }

    protected void filterfilldevesion()
    {
        string dev = "select  distinct BusinessMaster.BusinessID,BusinessName,WareHouseMaster.Name,DepartmentmasterMNC.Departmentname, WareHouseMaster.Name +' : '+ DepartmentmasterMNC.Departmentname +' : '+ BusinessMaster.BusinessName  as Divisionname  from  BusinessMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid    where WarehouseMaster.Status=1 AND BusinessMaster.company_id='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName";
        SqlDataAdapter ddev = new SqlDataAdapter(dev, con);
        DataTable dtdev = new DataTable();
        ddev.Fill(dtdev);

        if (dtdev.Rows.Count > 0)
        {
            ddlsearchByStore.DataSource = dtdev;
            ddlsearchByStore.DataTextField = "Divisionname";
            ddlsearchByStore.DataValueField = "BusinessID";
            ddlsearchByStore.DataBind();
        }
        ddlsearchByStore.Items.Insert(0, "All");
        ddlsearchByStore.Items[0].Value = "0";
    }

    protected void fillbusinessweek()
    {
        ddlw.Items.Clear();

        string y11 = "";
        if (ddlstore.SelectedIndex > -1)
        {
            y11 = " select distinct WMaster.MasterId ,Week.Name +':'+ WMaster.Title as Title from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId   inner join  YMaster on YMaster.MasterId=MMaster.YMasterId    where YMaster.BusinessID='" + ddlstore.SelectedValue + "' and week.lastdate1>='" + currentdate + "' and YMaster.DepartmentId IS NULL and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Title asc";

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlw.DataSource = dt;
            ddlw.DataTextField = "Title";
            ddlw.DataValueField = "MasterId";
            ddlw.DataBind();
            ddlw.Items.Insert(0, "-Select-");
            ddlw.Items[0].Value = "0";
        }
    }

    protected void filldepartmentweek()
    {
        ddlw.Items.Clear();

        string y11 = "";
        if (ddlstore.SelectedIndex > -1)
        {
            y11 = " select distinct WMaster.MasterId ,Week.Name +':'+ WMaster.Title as Title from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId   inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.DepartmentId='" + ddlstore.SelectedValue + "' and week.lastdate1>='" + currentdate + "' and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Title asc";

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlw.DataSource = dt;
            ddlw.DataTextField = "Title";
            ddlw.DataValueField = "MasterId";
            ddlw.DataBind();
            ddlw.Items.Insert(0, "-Select-");
            ddlw.Items[0].Value = "0";
        }
    }

    protected void filldivisionweek()
    {
        ddlw.Items.Clear();

        string y11 = "";
        if (ddlbusiness.SelectedIndex > -1)
        {
            y11 = " select distinct WMaster.MasterId ,Week.Name +':'+ WMaster.Title as Title from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId   inner join  YMaster on YMaster.MasterId=MMaster.YMasterId where YMaster.divisionid='" + ddlbusiness.SelectedValue + "' and week.lastdate1>='" + currentdate + "' and YMaster.EmployeeId IS NULL order by Title asc";

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlw.DataSource = dt;
            ddlw.DataTextField = "Title";
            ddlw.DataValueField = "MasterId";
            ddlw.DataBind();
            ddlw.Items.Insert(0, "-Select-");
            ddlw.Items[0].Value = "0";
        }
    }
    protected void ddlempgoaltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlm.Items.Clear();
        ddlw.Items.Clear();
        if (chkgoal.Checked == true)
        {
            if (ddlempgoaltype.SelectedIndex == 0)
            {
                pnlmonth.Visible = true;
                pnlweek.Visible = false;
            }
            if (ddlempgoaltype.SelectedIndex == 1)
            {
                pnlmonth.Visible = false;
                pnlweek.Visible = true;
            }
            if (ddlempgoaltype.SelectedIndex == 2)
            {

                pnlmonth.Visible = true;
                pnlweek.Visible = false;
            }
            if (ddlempgoaltype.SelectedIndex == 3)
            {
                pnlmonth.Visible = false;
                pnlweek.Visible = true;

            }
            if (ddlempgoaltype.SelectedIndex == 4)
            {
                pnlmonth.Visible = true;
                pnlweek.Visible = false;
            }
            if (ddlempgoaltype.SelectedIndex == 5)
            {
                pnlmonth.Visible = false;
                pnlweek.Visible = true;
            }
            if (ddlempgoaltype.SelectedIndex == 6)
            {
                pnlmonth.Visible = true;
                pnlweek.Visible = false;
            }
            if (ddlempgoaltype.SelectedIndex == 7)
            {
                pnlmonth.Visible = false;
                pnlweek.Visible = true;
            }
        }
        fillgoal();
    }

    protected void fillemployeeyear()
    {
        ddlm.Items.Clear();

        string y11 = "";
        if (ddlemployee.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.EmployeeId='" + ddlemployee.SelectedValue + "' order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlm.DataSource = dt;
            ddlm.DataTextField = "Title1";
            ddlm.DataValueField = "MasterId";
            ddlm.DataBind();
            ddlm.Items.Insert(0, "-Select-");
            ddlm.Items[0].Value = "0";
        }
    }

    protected void fillemployeeweek()
    {
        ddlw.Items.Clear();

        string y11 = "";
        if (ddlemployee.SelectedIndex > -1)
        {
            y11 = " select distinct WMaster.MasterId ,Week.Name +':'+ WMaster.Title as Title from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId   inner join  YMaster on YMaster.MasterId=MMaster.YMasterId where YMaster.EmployeeId='" + ddlemployee.SelectedValue + "' and week.lastdate1>='" + currentdate + "' order by Title asc";

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlw.DataSource = dt;
            ddlw.DataTextField = "Title";
            ddlw.DataValueField = "MasterId";
            ddlw.DataBind();
            ddlw.Items.Insert(0, "-Select-");
            ddlw.Items[0].Value = "0";
        }
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlempgoaltype.SelectedIndex == 7)
        {
            fillemployeeweek();
        }
        if (ddlempgoaltype.SelectedIndex == 6)
        {
            fillemployeeyear();
        }
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlempgoaltype.SelectedIndex == 5)
        {
            filldivisionweek();
        }
        if (ddlempgoaltype.SelectedIndex == 4)
        {
            filldivisionyear();
        }
        if (ddltype.SelectedIndex == 0)
        {
            filldivisionyear();
        }
        if (ddltype.SelectedIndex == 1)
        {
            filldivisionweek();
        }
    }

    protected void fillbusinessyearforemployee()
    {
        ddlm.Items.Clear();
        string y11 = "";
        if (ddlstore.SelectedIndex > -1)
        {
            // int intt = Convert.ToInt32(ddlStore.SelectedValue);
            SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where WarehouseMaster.Status=1 AND DepartmentmasterMNC.id='" + ddlstore.SelectedValue + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.businessid='" + dt1.Rows[0]["WareHouseId"].ToString() + "' and YMaster.DepartmentId IS NULL and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlm.DataSource = dt;
                ddlm.DataTextField = "Title1";
                ddlm.DataValueField = "MasterId";
                ddlm.DataBind();
            }
            ddlm.Items.Insert(0, "-Select-");
            ddlm.Items[0].Value = "0";
        }
    }

    protected void fillbusinessweekforemployee()
    {
        ddlw.Items.Clear();
        string y11 = "";
        if (ddlstore.SelectedIndex > -1)
        {
            // int intt = Convert.ToInt32(ddlStore.SelectedValue);
            SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where WarehouseMaster.Status=1 AND DepartmentmasterMNC.id='" + ddlstore.SelectedValue + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                y11 = " select distinct WMaster.MasterId ,Week.Name +':'+ WMaster.Title as Title from Week inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId   inner join  YMaster on YMaster.MasterId=MMaster.YMasterId where YMaster.BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "' and YMaster.DepartmentId IS NULL and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Title asc";

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlw.DataSource = dt;
                ddlw.DataTextField = "Title";
                ddlw.DataValueField = "MasterId";
                ddlw.DataBind();
            }
            ddlw.Items.Insert(0, "-Select-");
            ddlw.Items[0].Value = "0";
        }
    }
    protected void chkjob_CheckedChanged(object sender, EventArgs e)
    {
        if (chkjob.Checked == true)
        {
            chkparty.Checked = true;
            chkparty_CheckedChanged(sender, e);
            ddlparty.SelectedIndex = ddlparty.Items.IndexOf(ddlparty.Items.FindByValue(ViewState["ID"].ToString()));
        }
        else
        {
            chkparty.Checked = false;
            chkparty_CheckedChanged(sender, e);
        }
    }

    protected void totalbudgetedcost()
    {
        string totalsum = "";
        string temp = "";

        if (RadioButtonList2.SelectedValue == "4")
        {

            if (ddlsearchByStore.SelectedIndex > -1)
            {
                totalsum = "select sum(projectmaster.BudgetedAmount) as TotalBudgtedCost from projectmaster Left outer join BusinessMaster on projectmaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=projectmaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=projectmaster.DeptId inner join WareHouseMaster on WareHouseMaster.WarehouseId=projectmaster.Whid  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "'  and projectmaster.Whid='" + ddlsearchByStore.SelectedValue + "' and projectmaster.DeptId='0' and projectmaster.businessid='0' and projectmaster.EmployeeId='0'";

            }
            else
            {
                totalsum = "select sum(projectmaster.BudgetedAmount) as TotalBudgtedCost from projectmaster Left outer join BusinessMaster on projectmaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=projectmaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=projectmaster.DeptId inner join WareHouseMaster on WareHouseMaster.WarehouseId=projectmaster.Whid  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and  projectmaster.DeptId='0' and projectmaster.businessid='0' and projectmaster.EmployeeId='0'";
            }
            if (ddlstatusfilter.SelectedIndex > 0)
            {
                totalsum += " and projectmaster.Status='" + ddlstatusfilter.SelectedItem.Text + "'";
            }
        }


        if (RadioButtonList2.SelectedValue == "0")
        {

            if (ddlsearchByStore.SelectedIndex > 0)
            {
                totalsum = "select sum(projectmaster.BudgetedAmount) as TotalBudgtedCost from projectmaster Left outer join BusinessMaster on projectmaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=projectmaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=projectmaster.DeptId inner join WareHouseMaster on WareHouseMaster.WarehouseId=projectmaster.Whid  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and projectmaster.DeptId='" + ddlsearchByStore.SelectedValue + "'  and projectmaster.businessid='0' and projectmaster.EmployeeId='0'";
            }
            else
            {
                totalsum = "select sum(projectmaster.BudgetedAmount) as TotalBudgtedCost from projectmaster Left outer join BusinessMaster on projectmaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=projectmaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=projectmaster.DeptId inner join WareHouseMaster on WareHouseMaster.WarehouseId=projectmaster.Whid  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and  projectmaster.DeptId>0 and projectmaster.businessid='0' and projectmaster.EmployeeId='0'";
            }
            if (ddlstatusfilter.SelectedIndex > 0)
            {
                totalsum += " and projectmaster.Status='" + ddlstatusfilter.SelectedItem.Text + "'";
            }
        }

        if (RadioButtonList2.SelectedValue == "1")
        {

            if (ddlsearchByStore.SelectedIndex > 0)
            {
                totalsum = "select sum(projectmaster.BudgetedAmount) as TotalBudgtedCost from projectmaster Left outer join BusinessMaster on projectmaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=projectmaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=projectmaster.DeptId inner join WareHouseMaster on WareHouseMaster.WarehouseId=projectmaster.Whid  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and projectmaster.DeptId='" + ddlsearchByStore.SelectedValue + "' and projectmaster.businessid>0 and projectmaster.EmployeeId='0'";
            }
            else
            {
                totalsum = "select sum(projectmaster.BudgetedAmount) as TotalBudgtedCost from projectmaster Left outer join BusinessMaster on projectmaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=projectmaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=projectmaster.DeptId inner join WareHouseMaster on WareHouseMaster.WarehouseId=projectmaster.Whid  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and projectmaster.businessid>0 and projectmaster.EmployeeId='0'";
            }
            if (DropDownList2.SelectedIndex > 0)
            {
                totalsum += "and projectmaster.businessid='" + DropDownList2.SelectedValue + "'";
            }

            if (ddlstatusfilter.SelectedIndex > 0)
            {
                totalsum += " and projectmaster.Status='" + ddlstatusfilter.SelectedItem.Text + "'";
            }
        }

        if (RadioButtonList2.SelectedValue == "2")
        {

            if (ddlsearchByStore.SelectedIndex > 0)
            {
                totalsum = "select sum(projectmaster.BudgetedAmount) as TotalBudgtedCost from projectmaster Left outer join BusinessMaster on projectmaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=projectmaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=projectmaster.DeptId inner join WareHouseMaster on WareHouseMaster.WarehouseId=projectmaster.Whid  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and projectmaster.DeptId='" + ddlsearchByStore.SelectedValue + "' and projectmaster.EmployeeId>0";
            }
            else
            {
                totalsum = "select sum(projectmaster.BudgetedAmount) as TotalBudgtedCost from projectmaster Left outer join BusinessMaster on projectmaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=projectmaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=projectmaster.DeptId inner join WareHouseMaster on WareHouseMaster.WarehouseId=projectmaster.Whid  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and projectmaster.EmployeeId>0 ";
            }
            if (DropDownList3.SelectedIndex > 0)
            {
                totalsum += "and projectmaster.EmployeeId='" + DropDownList3.SelectedValue + "'";
            }

            if (ddlstatusfilter.SelectedIndex > 0)
            {
                totalsum += " and projectmaster.Status='" + ddlstatusfilter.SelectedItem.Text + "'";
            }
        }


        SqlDataAdapter da11 = new SqlDataAdapter(totalsum, con);
        DataTable dt11 = new DataTable();
        da11.Fill(dt11);
        decimal t1 = 0;
        if (dt11.Rows.Count > 0)
        {
            if (Convert.ToString(dt11.Rows[0]["TotalBudgtedCost"]) != "")
            {
                t1 = Math.Round(Convert.ToDecimal(dt11.Rows[0]["TotalBudgtedCost"]), 2);
                temp = t1.ToString("###,###.##");

            }
        }

        if (grid.Rows.Count > 0)
        {
            GridViewRow dr = (GridViewRow)grid.FooterRow;
            Label lblfooter = (Label)dr.FindControl("lblfooter");
            lblfooter.Text = temp;
        }

    }
    protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ID"] = Convert.ToInt32(ddlparty.SelectedValue);
    }

    protected void fillmaterialissue()
    {
        string str123t = "select distinct MaterialIssueMasterTbl.Id, MaterialIssueDetail.InvWMasterId,MaterialIssueDetail.Rate,MaterialIssueDetail.Qty,InventoryMaster.Name,MaterialIssueMasterTbl.JobMasterId  from MaterialIssueMasterTbl inner join MaterialIssueDetail on MaterialIssueDetail.MaterialMasterId=MaterialIssueMasterTbl.Id " +
                        " inner join  InventoryWarehouseMasterTbl on  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=MaterialIssueDetail.InvWMasterId " +
                        " inner join InventoryMaster on InventoryMaster.InventoryMasterId=InventoryWarehouseMasterTbl.InventoryMasterId " +
                        " where MaterialIssueMasterTbl.JobMasterId='" + ViewState["rid"] + "' ";

        SqlCommand cmd = new SqlCommand(str123t, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        grdMaterialIssue.DataSource = dt;
        grdMaterialIssue.DataBind();

        double totalmaterialcost = 0;
        if (grdMaterialIssue.Rows.Count > 0)
        {
            foreach (GridViewRow gdr in grdMaterialIssue.Rows)
            {

                double materialcost = 0;

                Label lblmaterialmasterid = (Label)gdr.FindControl("lblmaterialmasterid");
                Label InvWMasterId = (Label)gdr.FindControl("lblitemnameid");
                Label lbldate124 = (Label)gdr.FindControl("lbldate124");
                Label lbltotalcost = (Label)gdr.FindControl("lblCost");
                Label lblRate = (Label)gdr.FindControl("lblRate");
                Label lblqty = (Label)gdr.FindControl("lblqty");

                string str123tmaterial = "select * from InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + InvWMasterId.Text + "' and MaterialIssueMasterTblId='" + lblmaterialmasterid.Text + "' ";
                SqlCommand cmdmaterial = new SqlCommand(str123tmaterial, con);
                SqlDataAdapter adpmaterial = new SqlDataAdapter(cmdmaterial);
                DataTable dtmaterial = new DataTable();
                adpmaterial.Fill(dtmaterial);

                if (dtmaterial.Rows.Count > 0)
                {
                    lbldate124.Text = Convert.ToDateTime(Convert.ToString(dtmaterial.Rows[0]["DateUpdated"].ToString())).ToShortDateString();

                    double qty = 0;
                    double rate = 0;
                    double totalcost = 0;
                    if (lblqty.Text != "")
                    {
                        qty = Convert.ToDouble(lblqty.Text);
                    }
                    if (lblRate.Text != "")
                    {
                        rate = Convert.ToDouble(lblRate.Text);
                    }
                    totalcost = qty * rate;

                    lbltotalcost.Text = totalcost.ToString();
                    materialcost += totalcost;

                    totalmaterialcost += materialcost;
                    lblTotalSum.Text = totalmaterialcost.ToString();
                }
            }
        }
        else
        {
            lblTotalSum.Text = "0";
        }
    }

    public void overheadallocation()
    {
        string st178 = " select distinct OverHeadAllocationMaster.* from OverHeadAllocationMaster inner join OverHeadAllocationJobDetail on OverHeadAllocationJobDetail.OverHeadMasterId=OverHeadAllocationMaster.Id where OverHeadAllocationJobDetail.JobMasterId='" + ViewState["rid"] + "'";
        SqlCommand cmd178 = new SqlCommand(st178, con);
        SqlDataAdapter adp178 = new SqlDataAdapter(cmd178);
        DataTable dt178 = new DataTable();
        adp178.Fill(dt178);

        grdoverhead.DataSource = dt178;
        grdoverhead.DataBind();

        double totaloverheadcosting = 0;

        if (grdoverhead.Rows.Count > 0)
        {
            foreach (GridViewRow gdr in grdoverhead.Rows)
            {

                Label lbloverheadmasterid = (Label)gdr.FindControl("lbloverheadmasterid");
                Label lblstartdate789 = (Label)gdr.FindControl("lblstartdate789");
                Label lblenddate789 = (Label)gdr.FindControl("lblenddate789");
                Label lblohbymaterial789 = (Label)gdr.FindControl("lblohbymaterial789");

                Label lbldirectlabour789 = (Label)gdr.FindControl("lbldirectlabour789");
                Label lblnoofdays789 = (Label)gdr.FindControl("lblnoofdays789");
                Label ohbyequal789 = (Label)gdr.FindControl("ohbyequal789");
                Label lblOhAllocationtotal789 = (Label)gdr.FindControl("lblOhAllocationtotal789");

                string Avgcost = "select OverHeadAllocationJobDetail.* from OverHeadAllocationJobDetail   where  OverHeadMasterId='" + lbloverheadmasterid.Text + "' and Active='1' ";
                SqlCommand cmd1451 = new SqlCommand(Avgcost, con);
                SqlDataAdapter adp1451 = new SqlDataAdapter(cmd1451);
                DataTable ds1451 = new DataTable();
                adp1451.Fill(ds1451);

                double overheadbymate = 0;
                double overheadbymalabour = 0;
                double overheadbymapd = 0;
                double overheadbymaequal = 0;
                double totaloverhead = 0;

                double overheadbymate1 = 0;
                double overheadbymalabour1 = 0;
                double overheadbymapd1 = 0;
                double overheadbymaequal1 = 0;
                double totaloverhead1 = 0;

                if (dt178.Rows.Count > 0)
                {

                    foreach (DataRow dtr132 in ds1451.Rows)
                    {
                        overheadbymate = Convert.ToDouble(dtr132["OhByMaterial"].ToString());
                        overheadbymalabour = Convert.ToDouble(dtr132["OhByLabour"].ToString());
                        overheadbymapd = Convert.ToDouble(dtr132["OhByDays"].ToString());
                        overheadbymaequal = Convert.ToDouble(dtr132["Ohbyequal"].ToString());
                        totaloverhead = Convert.ToDouble(dtr132["OhAllocationtotal"].ToString());

                        overheadbymate1 += overheadbymate;
                        lblohbymaterial789.Text = overheadbymate1.ToString();

                        overheadbymalabour1 += overheadbymalabour;
                        lbldirectlabour789.Text = overheadbymalabour1.ToString();

                        overheadbymapd1 += overheadbymapd;
                        lblnoofdays789.Text = overheadbymapd1.ToString();

                        overheadbymaequal1 += overheadbymaequal;
                        ohbyequal789.Text = overheadbymaequal1.ToString();

                        totaloverhead1 += totaloverhead;
                        lblOhAllocationtotal789.Text = totaloverhead1.ToString();

                        totaloverheadcosting += totaloverhead1;
                        lbltotaloverheadbyall.Text = totaloverheadcosting.ToString();

                    }
                }
            }
        }
        else
        {
            lbltotaloverheadbyall.Text = "0";
        }
    }

    protected void fillgridtemp()
    {
        string st132 = "select distinct JobEmployeeDailyTaskTbl.*,EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterID,JobEmployeeDailyTaskDetail.JobMasterId from JobEmployeeDailyTaskTbl inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobDailyTaskMasterId=JobEmployeeDailyTaskTbl.Id inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=JobEmployeeDailyTaskTbl.EmployeeId  where JobEmployeeDailyTaskDetail.JobMasterId='" + ViewState["rid"] + "' ";
        SqlCommand cmd = new SqlCommand(st132, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        grddailywork.DataSource = ds;
        grddailywork.DataBind();

        double TotalCost = 0;

        if (grddailywork.Rows.Count > 0)
        {
            foreach (GridViewRow gdr in grddailywork.Rows)
            {
                Label lblEmployee = (Label)gdr.FindControl("lblEmployee");
                Label lblmasterid = (Label)gdr.FindControl("lblmasterid");
                Label lblhours = (Label)gdr.FindControl("lblhours");
                Label lblcost = (Label)gdr.FindControl("lblcost");

                string Avgcost = "Select sum(datepart(hour,convert(datetime,Hrs)))  AS TotalHours,sum(datepart(minute,convert(datetime,Hrs))) AS TotalMinutes,SUM(cast(Cost as Decimal )) as Cost from JobEmployeeDailyTaskDetail    where  JobEmployeeDailyTaskDetail.JobDailyTaskMasterId='" + lblmasterid.Text + "' ";

                SqlCommand cmd1451 = new SqlCommand(Avgcost, con);
                SqlDataAdapter adp1451 = new SqlDataAdapter(cmd1451);
                DataTable ds1451 = new DataTable();
                adp1451.Fill(ds1451);

                if (ds1451.Rows.Count > 0)
                {
                    string FinalTime = "";

                    string TotalHour = ds1451.Rows[0]["TotalHours"].ToString();
                    string TotalMinutes = ds1451.Rows[0]["TotalMinutes"].ToString();

                    Int32 in1 = Convert.ToInt32(TotalHour.ToString());
                    Int32 HourtoMinute1 = in1 * 60;
                    Int32 Minute1 = Convert.ToInt32(TotalMinutes.ToString());

                    Int32 TotalMinutes132 = (HourtoMinute1) + (Minute1);

                    Int32 FinalHours = (TotalMinutes132 / 60);
                    Int32 FinalMinute = (TotalMinutes132 % 60);

                    FinalTime = FinalHours + ":" + FinalMinute;
                    lblhours.Text = FinalTime.ToString();

                    double Cost = Convert.ToDouble(ds1451.Rows[0]["Cost"].ToString());
                    lblcost.Text = Cost.ToString();

                    TotalCost += Cost;
                    lbldailyworktotal.Text = TotalCost.ToString();
                }
            }
        }
        else
        {
            lbldailyworktotal.Text = "0";
        }
    }

    //------------------NEw------------------------
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        if (chk.Checked == true)
        {
            ViewState["storeid"] = ddlstore.SelectedValue;  

            Paneldoc.Visible = true;
            flaganddoc();
            FillParty();
            FillDocumentType();
            pnlinserdoc.Visible = true;
            pnlfileup.Visible = false;
            TxtDocDate.Text = DateTime.Now.ToShortDateString();
        }
        else
        {
            Paneldoc.Visible = false;
        }
    }

    protected void rdpop_SelectedIndexChanged(object sender, EventArgs e)
    {
        statuslable.Text = "";
        if (rdpop.SelectedValue == "1")
        {
            DateTime fristdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            txtfrom.Text = fristdayofmonth.ToShortDateString();
            DateTime lastdaymonth = fristdayofmonth.AddMonths(1).AddDays(-1);
            txtto.Text = lastdaymonth.ToShortDateString();
            FillDocumentType();
            pnlfileup.Visible = true;
            pnlinserdoc.Visible = false;

        }
        else if (rdpop.SelectedValue == "2")
        {
            FillDocumentType();
            pnlinserdoc.Visible = true;
            pnlfileup.Visible = false;
            TxtDocDate.Text = DateTime.Now.ToShortDateString();
            //Response.Redirect("DocumentFastUpload.aspx?tid=" + ViewState["tid"].ToString() + "");
        }
    }

    protected void FillDocumentType()
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocTypeAll(Convert.ToString(ddlstore.SelectedValue));//ViewState["storeid"]
        if (rdpop.SelectedValue == "1")
        {

            ddltypeofdoc.DataTextField = "doctype";
            ddltypeofdoc.DataValueField = "DocumentTypeId";
            ddltypeofdoc.DataSource = dt;
            ddltypeofdoc.DataBind();
            ddltypeofdoc.SelectedIndex = ddltypeofdoc.Items.IndexOf(ddltypeofdoc.Items.FindByText("GENERAL - General - GENERAL"));
        }
        else if (rdpop.SelectedValue == "2")
        {
            ddlindocfil.DataTextField = "doctype";
            ddlindocfil.DataValueField = "DocumentTypeId";
            ddlindocfil.DataSource = dt;
            ddlindocfil.DataBind();
            ddlindocfil.SelectedIndex = ddlindocfil.Items.IndexOf(ddlindocfil.Items.FindByText("MANAGMENT - MASTER DOCUMENTS - MISSION DOCUMENTS"));
        }        
    }
    protected void ddldt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldt.SelectedItem.Text == "Credit Invoice" || ddldt.SelectedItem.Text == "Cash Invoice" || ddldt.SelectedItem.Text == "Cash Voucher" || ddldt.SelectedItem.Text == "Credit Voucher")
        {
            RequicmnredFieldValidator2.Visible = true;
        }
        else
        {
            RequicmnredFieldValidator2.Visible = false;
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        flaganddoc();
    }
    protected void ImageButton49_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentSubSubType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton48_Click(object sender, ImageClickEventArgs e)
    {
        FillDocumentType();
    }
    protected void ImageButton50_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PartyMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton51_Click(object sender, ImageClickEventArgs e)
    {
        FillParty();
    }
    protected void FillParty()
    {
        DataTable dt = new DataTable();
        dt = clsDocument.selectparty(Convert.ToString(ViewState["storeid"]));
        ddlpartyname.DataSource = dt;
        ddlpartyname.DataTextField = "PartyName";
        ddlpartyname.DataValueField = "PartyId";
        ddlpartyname.DataBind();
    }
    protected void imgbtnAdd_Click(object sender, EventArgs e)
    {
        int flagpd = 0;
        statuslable.Text = "";
        //if (ddldt.SelectedItem.Text == "Credit Invoice" || ddldt.SelectedItem.Text == "Cash Invoice" || ddldt.SelectedItem.Text == "Cash Voucher" || ddldt.SelectedItem.Text == "Credit Voucher")
        //{
        if (txtpartdocrefno.Text.Length > 0)
        {
            DataTable dtsc = select("select PartyDocrefno from DocumentMaster where   CID='" + Session["Comid"] + "' and PartyDocrefno='" + txtpartdocrefno.Text + "'");
            if (dtsc.Rows.Count == 0)
            {
                if (Convert.ToString(ViewState["data"]) != "")
                {
                    DataTable dtc = (DataTable)ViewState["data"];
                    foreach (DataRow item in dtc.Rows)
                    {
                        if (Convert.ToString(item["PRN"]) == Convert.ToString(txtpartdocrefno.Text))
                        {
                            flagpd = 1;
                            statuslable.Text = "Please enter unique party document reference number.";
                            break;
                        }
                        else
                        {

                        }
                    }
                }
            }
            else
            {
                flagpd = 1;
                statuslable.Text = "Please enter unique party document reference number.";
            }
        }

        if (flagpd == 0)
        {
            FillGrid();
            if (GridView2.Rows.Count > 0)
            {
                btnuplo.Visible = true;
                imgbtnreset.Visible = true;
            }
            else
            {
                btnuplo.Visible = false;
                imgbtnreset.Visible = false;
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        UploadDocumetsTempforTask();
        ViewState["data"] = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
        btnuplo.Visible = false;
        imgbtnreset.Visible = false;
        //fillpop();

    }
    protected void btnuplo_Click(object sender, EventArgs e)
    {
        bool access = UserAccess.Usercon("DocumentMaster", "", "DocumentId", "", "", "CID", "DocumentMaster");
        if (access == true)
        {
            UploadDocumets();
            ViewState["data"] = null;
            GridView1.DataSource = null;
            GridView1.DataBind();
            btnuplo.Visible = false;
            imgbtnreset.Visible = false;
            fillpop();
        }
        else
        {
            statuslable.Visible = true;
            statuslable.Text = "Sorry, You are not permitted for greater record to Priceplan";
        }
    }
    protected void imgbtnreset_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void clear()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        ViewState["data"] = null;
    }
    protected void btn1pop_Click(object sender, EventArgs e)
    {        

        if (rdpop.SelectedValue == "1")
        {
            Response.Redirect("DocumentSearch.aspx?tid=" + ViewState["tid"].ToString() + "");

        }
        else if (rdpop.SelectedValue == "2")
        {
            Response.Redirect("DocumentFastUpload.aspx?tid=" + ViewState["tid"].ToString() + "");
        }
    }
    //-----------------

    protected void fillpop()
    {

        grd.DataSource = null;
        grd.DataBind();
        DataTable ds58 = new DataTable();
        if (Convert.ToInt32(ViewState["MissionId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["MissionId"]), 1);
        }
        else if (Convert.ToInt32(ViewState["employeemmid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["employeemmid"]), 500);
        }
        else if (Convert.ToInt32(ViewState["Mdetail"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["Mdetail"]), 11);
        }
        else if (Convert.ToInt32(ViewState["Mevalution"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["Mevalution"]), 12);
        }
        else if (Convert.ToInt32(ViewState["ltgm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["ltgm"]), 2);
        }
        else if (Convert.ToInt32(ViewState["ltgd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["ltgd"]), 21);
        }
        else if (Convert.ToInt32(ViewState["ltge"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["ltge"]), 22);
        }


        else if (Convert.ToInt32(ViewState["stgm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["stgm"]), 3);
        }
        else if (Convert.ToInt32(ViewState["stgd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["stgd"]), 31);
        }
        else if (Convert.ToInt32(ViewState["stge"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["stge"]), 32);
        }


        else if (Convert.ToInt32(ViewState["yem"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["yem"]), 4);
        }

        else if (Convert.ToInt32(ViewState["annam"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["annam"]), 44);
        }

        else if (Convert.ToInt32(ViewState["yed"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["yed"]), 41);
        }
        else if (Convert.ToInt32(ViewState["yee"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["yee"]), 42);
        }

        else if (Convert.ToInt32(ViewState["mom"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["mom"]), 5);
        }
        else if (Convert.ToInt32(ViewState["mod"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["mod"]), 51);
        }
        else if (Convert.ToInt32(ViewState["moe"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["moe"]), 52);
        }

        else if (Convert.ToInt32(ViewState["wem"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["wem"]), 6);
        }
        else if (Convert.ToInt32(ViewState["wed"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["wed"]), 61);
        }
        else if (Convert.ToInt32(ViewState["wee"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["wee"]), 62);
        }

        else if (Convert.ToInt32(ViewState["stram"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["stram"]), 7);
        }
        else if (Convert.ToInt32(ViewState["strad"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["strad"]), 71);
        }
        else if (Convert.ToInt32(ViewState["strae"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["strae"]), 72);
        }

        else if (Convert.ToInt32(ViewState["tectm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["tectm"]), 8);
        }
        else if (Convert.ToInt32(ViewState["tectd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["tectd"]), 81);
        }
        else if (Convert.ToInt32(ViewState["tecte"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["tecte"]), 82);
        }
        else if (Convert.ToInt32(ViewState["takid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["takid"]), 9);
        }
        else if (Convert.ToInt32(ViewState["proid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["proid"]), 10);
        }
        else if (Convert.ToInt32(ViewState["Proevaid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["Proevaid"]), 116);
        }
        else if (Convert.ToInt32(ViewState["commid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["commid"]), 111);
        }
        else if (Convert.ToInt32(ViewState["InvCountId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["InvCountId"]), 112);
        }
        else if (Convert.ToInt32(ViewState["CandidateId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["CandidateId"]), 113);
        }
        if (ds58.Rows.Count > 0)
        {
            grd.DataSource = ds58;
            grd.DataBind();
            // Label13.Text = "The list of document to be attach to the above mention entry.";
        }   
    }
    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delete1")
        {
            int Id = Convert.ToInt32(e.CommandArgument);

            string scpt = "Delete  from OfficeManagerDocuments where Id='" + Id + "'";
            SqlCommand cmd1 = new SqlCommand(scpt, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            int k = cmd1.ExecuteNonQuery();
            con.Close();
            if (k > 0)
            {
                statuslable.Text = "Record deleted sucessfully  ";
            }
            else
            {
                statuslable.Text = "Record not deleted sucessfully ";
            }
            fillpop();


        }
        else if (e.CommandName == "Send")
        {
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDoucmentMasterByID(Convert.ToInt32(e.CommandArgument));
            string docname = dt.Rows[0]["DocumentName"].ToString();
            string filepath = Server.MapPath("Account//" + Session["comid"] + "//UploadedDocuments//" + docname);


            string name = docname.Trim();
            string extension = name.Substring(name.Length - 3);
            if (Convert.ToString(extension) == "pdf")
            {
                Session["ABCDE"] = "ABCDE";

                string te = "ViewDocument.aspx?id=" + e.CommandArgument + "&Siddd=VHDS";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

            }
            else
            {
                FileInfo file = new FileInfo(filepath);

                if (file.Exists)
                {
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = file.Extension.ToLower();
                    Response.TransmitFile(file.FullName);

                    Response.End();

                }
            }
        }
    }

    protected void flaganddoc()
    {

        DataTable dts1 = select("select Id,name from DocumentTypenm where  active='1' Order by name");
        ddldt.DataSource = dts1;
        ddldt.DataTextField = "name";
        ddldt.DataValueField = "Id";
        ddldt.DataBind();
        ddldt.SelectedIndex = ddldt.Items.IndexOf(ddldt.Items.FindByText("Document"));
        //ddldt.Items.Insert(0, "Select");
        //ddldt.Items[0].Value = "0";
    }
    protected void UploadDocumets()
    {
        int i1 = 0;
        try
        {
            //foreach (GridViewRow gdr in Gridreqinfo.Rows)
            if (GridView2.Rows.Count > 0)
            {
                do
                {
                    string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + GridView2.Rows[i1].Cells[4].Text.Replace(" ", "_");
                    //string path1 = Server.MapPath("..\\Account\\TempDoc\\" + gdr.Cells[1].Text);
                    string path1 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\" + GridView2.Rows[i1].Cells[4].Text);
                    string path2 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + filename.ToString());
                    if (System.IO.File.Exists(path2))
                    {
                    }
                    else
                    {
                        File.Copy(path1, path2);
                    }
                    string filexten = Path.GetExtension(path2);

                    Label lbldocdate = (Label)GridView2.Rows[i1].Cells[6].FindControl("lbldocdate");

                    Label lbldocrefno = (Label)GridView2.Rows[i1].Cells[7].FindControl("lbldocrefno");
                    Label lbldocamt = (Label)GridView2.Rows[i1].Cells[8].FindControl("lbldocamt");
                    Label lblpid = (Label)GridView2.Rows[i1].Cells[1].FindControl("lblpid");
                    Label lbldoctid = (Label)GridView2.Rows[i1].FindControl("lbldoctid");
                    Label lblprn = (Label)GridView2.Rows[i1].FindControl("lblprn");

                    if (lbldocamt.Text == "")
                    {
                        lbldocamt.Text = "0";
                    }
                    Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(GridView2.DataKeys[i1].Value), 2, Convert.ToDateTime(System.DateTime.Now.ToString()), filename.ToString(), GridView2.Rows[i1].Cells[3].Text, "", Convert.ToInt32(lblpid.Text), lbldocrefno.Text, Convert.ToDecimal(lbldocamt.Text), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(lbldocdate.Text), filexten, lbldoctid.Text, lblprn.Text);
                    if (rst > 0)
                    {
                        bool dcaprv = true;
                        bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);
                        int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                       Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);
                        string str12 = "Insert into OfficeManagerDocuments(MissionId,MissioninstructionId,MissionevId,LtgId,Ltgdetail,Ltgevalution,StgId,Stgdetail,Stgevalution,YgId,annid,Ydetail,Yeevalution,MgId,Mdetail,Mevalution,WgId,Wdetail,Wevalution,StrategyId,Strategydetail,Strategevaution,tectm,tectd,texte,taskid,projectid,projectevaid,DocumentId,StoreId,CommunicationId,InvCountId,EmployeeID,CandidateId)" +

                                     "values('" + ViewState["MissionId"] + "','" + ViewState["Mdetail"] + "','" + ViewState["Mevalution"] + "'," +
                                    "'" + ViewState["ltgm"] + "','" + ViewState["ltgd"] + "','" + ViewState["ltge"] + "'," +
                                     "'" + ViewState["stgm"] + "','" + ViewState["stgd"] + "','" + ViewState["stge"] + "'," +
                                     "'" + ViewState["yem"] + "','" + ViewState["annam"] + "','" + ViewState["yed"] + "','" + ViewState["yee"] + "'," +
                                     "'" + ViewState["mom"] + "','" + ViewState["mod"] + "','" + ViewState["moe"] + "'," +
                                     "'" + ViewState["wem"] + "','" + ViewState["wed"] + "','" + ViewState["wee"] + "'," +

                                      "'" + ViewState["stram"] + "','" + ViewState["strad"] + "','" + ViewState["strae"] + "'," +
                                       "'" + ViewState["tectm"] + "','" + ViewState["tectd"] + "','" + ViewState["tecte"] + "'," +
                                        "'" + ViewState["takid"] + "','" + ViewState["proid"] + "','" + ViewState["Proevaid"] + "','" + rst + "','" + ViewState["storeid"] + "','" + ViewState["commid"] + "','" + ViewState["InvCountId"] + "','" + ViewState["employeemmid"] + "','" + ViewState["CandidateId"] + "')";

                        SqlCommand cmd12 = new SqlCommand(str12, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd12.ExecuteNonQuery();
                        con.Close();

                    }
                    //string Location = Server.MapPath(@"~/Account/TempDoc/");
                    string Location = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocuments/");
                    string Location2 = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocumentsTemp/");
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
                    if (filexten == ".pdf")
                    {
                        foreach (System.IO.FileInfo f in dir.GetFiles(filename))
                        {

                            string Location1 = Server.MapPath(@"~/Account/" + Session["comid"] + "/DocumentImage/");
                            ;

                            //if (System.IO.File.GetAttributes(Location + f.Name).ToString() != System.IO.FileAttributes.Hidden.ToString())
                            //{

                            string filepath = Server.MapPath("~//Account//pdftoimage.exe");
                            System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);
                            //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
                            //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");

                            //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                            pti.Arguments = filepath + " -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                            filepath += " " + "-r" + " " + "AGPVCWBRYBCDEPFD";

                            pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");

                            pti.UseShellExecute = false;
                            pti.RedirectStandardOutput = true;
                            pti.RedirectStandardInput = true;
                            pti.RedirectStandardError = true;
                            //pti.WorkingDirectory = "D:\\Capman.ifilecabinet.com1\\Account\\";
                            // pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");
                            System.Diagnostics.Process ps = Process.Start(pti);

                            if (System.IO.File.Exists(Location2 + f.Name))
                            {
                            }
                            else
                            {
                                System.IO.File.Copy(Location + f.Name, Location2 + f.Name);
                            }
                            System.IO.File.SetAttributes(Location2 + f.Name, System.IO.FileAttributes.Hidden);

                            //}
                        }


                        int ii = 0;
                        string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + filename);
                        using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                        {
                            Regex regex = new Regex(@"/Type\s*/Page[^s]");
                            MatchCollection match = regex.Matches(st.ReadToEnd());
                            ii = match.Count;
                        }

                        int length = filename.Length;
                        string docnameIn = filename.Substring(0, length - 4);


                        for (int kk = 1; kk <= ii; kk++)
                        {
                            string scpf = docnameIn;
                            if (kk >= 1 && kk < 10)
                            {
                                scpf = scpf + "0000" + kk + ".jpg";
                            }
                            else if (kk >= 10 && kk < 100)
                            {
                                scpf = scpf + "000" + kk + ".jpg";
                            }
                            else if (kk >= 100)
                            {
                                scpf = scpf + "00" + kk + ".jpg";
                            }
                            clsEmployee.InserDocumentImageMaster(rst, scpf);
                        }
                    }
                    statuslable.Visible = true;
                    statuslable.Text = "Message : All PDFs Are Converted Successfully!!!";
                    i1 = i1 + 1;
                } while (i1 <= GridView2.Rows.Count - 1);
            }
            //=========================== update status in view state
            ViewState["data"] = null;
            statuslable.Text = "Documents uploaded successfully.";
        }
        catch (Exception ex)
        {
            statuslable.Text = ex.Message.ToString();

        }
    }

    protected void UploadDocumetsTempforTask()
    {
        int i1 = 0;
        try
        {
            //foreach (GridViewRow gdr in Gridreqinfo.Rows)
            if (GridView1.Rows.Count > 0)
            {
                do
                {
                    string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + GridView1.Rows[i1].Cells[4].Text.Replace(" ", "_");
                    //string path1 = Server.MapPath("..\\Account\\TempDoc\\" + gdr.Cells[1].Text);
                    string path1 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\" + GridView1.Rows[i1].Cells[4].Text);
                    string path2 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + filename.ToString());

                    if (System.IO.File.Exists(path2))
                    {
                    }
                    else
                    {
                        File.Copy(path1, path2);
                    }
                    string filexten = Path.GetExtension(path2);

                    Label lbldocdate = (Label)GridView1.Rows[i1].Cells[6].FindControl("lbldocdate");

                    Label lbldocrefno = (Label)GridView1.Rows[i1].Cells[7].FindControl("lbldocrefno");
                    Label lbldocamt = (Label)GridView1.Rows[i1].Cells[8].FindControl("lbldocamt");
                    Label lblpid = (Label)GridView1.Rows[i1].Cells[1].FindControl("lblpid");

                    Int32 rst = clsDocument.InsertDocumentMasterForTempTask(Convert.ToInt32(GridView1.DataKeys[i1].Value), 2, Convert.ToDateTime(System.DateTime.Now.ToString()), filename.ToString(), GridView1.Rows[i1].Cells[3].Text, "", Convert.ToInt32(lblpid.Text), lbldocrefno.Text, Convert.ToDecimal(lbldocamt.Text), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(lbldocdate.Text), filexten, Convert.ToInt32(ViewState["takid"].ToString()));

                    if (rst > 0)
                    {

                    }
                    //string Location = Server.MapPath(@"~/Account/TempDoc/");
                    string Location = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocuments/");
                    string Location2 = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocumentsTemp/");
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
                    if (filexten == ".pdf")
                    {
                        foreach (System.IO.FileInfo f in dir.GetFiles(filename))
                        {

                            string Location1 = Server.MapPath(@"~/Account/" + Session["comid"] + "/DocumentImage/");
                            ;

                            //if (System.IO.File.GetAttributes(Location + f.Name).ToString() != System.IO.FileAttributes.Hidden.ToString())
                            //{

                            string filepath = Server.MapPath("~//Account//pdftoimage.exe");
                            System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);
                            //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
                            //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");
                            pti.UseShellExecute = false;
                            //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                            pti.Arguments = filepath + " -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";

                            pti.RedirectStandardOutput = true;
                            pti.RedirectStandardInput = true;
                            pti.RedirectStandardError = true;
                            //pti.WorkingDirectory = "D:\\Capman.ifilecabinet.com1\\Account\\";
                            pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");
                            System.Diagnostics.Process ps = Process.Start(pti);

                            if (System.IO.File.Exists(Location2 + f.Name))
                            {
                            }
                            else
                            {
                                System.IO.File.Copy(Location + f.Name, Location2 + f.Name);
                            }
                            System.IO.File.SetAttributes(Location2 + f.Name, System.IO.FileAttributes.Hidden);

                            //}
                        }

                    }


                    statuslable.Text = "Message : All PDFs Are Converted Successfully!!!";
                    statuslable.Visible = true;


                    i1 = i1 + 1;

                } while (i1 <= GridView1.Rows.Count - 1);

            }

            //=========================== update status in view state


            ViewState["data"] = null;


            statuslable.Text = "Documents uploaded successfully.";
        }
        catch (Exception ex)
        {
            statuslable.Text = ex.Message.ToString();

        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["data"];
            dt.Rows[Convert.ToInt32(GridView1.SelectedIndex.ToString())].Delete();
            dt.AcceptChanges();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            ViewState["data"] = dt;
            if (GridView1.Rows.Count == 0)
            {
                btnuplo.Visible = false;
                imgbtnreset.Visible = false;
            }
            statuslable.Text = "Record deleted successfully.";
        }
    }
    protected void FillGrid()
    {
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["data"]) == "")
        {
            dt = CreateDatatable();
        }
        else
        {
            dt = (DataTable)ViewState["data"];
        }
        if (FileUpload1.HasFile == true)
        {

            DataRow Drow = dt.NewRow();
            Drow["documentname"] = FileUpload1.FileName;
            Drow["documenttype"] = ddlindocfil.SelectedValue;
            Drow["status"] = "Not Uploaded";

            Drow["Businessname"] = ddlstore.SelectedItem.Text;// ViewState["Wname"].ToString();
            Drow["DocType"] = ddlindocfil.SelectedItem.Text;
            Drow["DocumentTitle"] = txtdoctitle.Text;

            Drow["PartyId"] = ddlpartyname.SelectedValue;

            Drow["docdate"] = Convert.ToDateTime(TxtDocDate.Text).ToShortDateString();

            Drow["docrefno"] = txtdocrefnmbr.Text;
            Drow["docamt"] = txtnetamount.Text;
            Drow["Docty"] = ddldt.SelectedItem.Text;
            Drow["DoctyId"] = ddldt.SelectedValue;
            Drow["PRN"] = txtpartdocrefno.Text;
            dt.Rows.Add(Drow);
        }
        ViewState["data"] = dt;
        GridView2.DataSource = dt;
        GridView2.DataBind();

        if (FileUpload1.HasFile == true)
        {
            if (Directory.Exists(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc")) == false)
            {
                Directory.CreateDirectory(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc"));
            }
            string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\") + filename);
        }
    }
    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "documentname";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "documenttype";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "DocumentTitle";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "status";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "Businessname";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;


        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "PartyId";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;


        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "DocType";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "docdate";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;


        DataColumn Dcom9 = new DataColumn();
        Dcom9.DataType = System.Type.GetType("System.String");
        Dcom9.ColumnName = "docrefno";
        Dcom9.AllowDBNull = true;
        Dcom9.Unique = false;
        Dcom9.ReadOnly = false;

        DataColumn Dcom10 = new DataColumn();
        Dcom10.DataType = System.Type.GetType("System.String");
        Dcom10.ColumnName = "docamt";
        Dcom10.AllowDBNull = true;
        Dcom10.Unique = false;
        Dcom10.ReadOnly = false;


        DataColumn Dcom4a = new DataColumn();
        Dcom4a.DataType = System.Type.GetType("System.String");
        Dcom4a.ColumnName = "Docty";
        Dcom4a.AllowDBNull = true;
        Dcom4a.Unique = false;
        Dcom4a.ReadOnly = false;

        DataColumn Dcom5a = new DataColumn();
        Dcom5a.DataType = System.Type.GetType("System.String");
        Dcom5a.ColumnName = "DoctyId";
        Dcom5a.AllowDBNull = true;
        Dcom5a.Unique = false;
        Dcom5a.ReadOnly = false;
        DataColumn Dcom6a = new DataColumn();
        Dcom6a.DataType = System.Type.GetType("System.String");
        Dcom6a.ColumnName = "PRN";
        Dcom6a.AllowDBNull = true;
        Dcom6a.Unique = false;
        Dcom6a.ReadOnly = false;


        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);

        dt.Columns.Add(Dcom8);
        dt.Columns.Add(Dcom9);
        dt.Columns.Add(Dcom10);
        dt.Columns.Add(Dcom4a);
        dt.Columns.Add(Dcom5a);
        dt.Columns.Add(Dcom6a);
        return dt;
    }



    protected void btnsearchgo_Click(object sender, EventArgs e)
    {
        string monter = "";
        if (RadioButtonList1.SelectedIndex == 0)
        {
            if ((txtfrom.Text.Length > 0) && (txtto.Text.Length > 0))
            {
                monter = " and (DocumentMaster.DocumentDate between '" + Convert.ToDateTime(txtfrom.Text).AddDays(-1) + "' and '" + Convert.ToDateTime(txtto.Text).AddDays(1) + "')";
            }
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            if ((txtfrom.Text.Length > 0) && (txtto.Text.Length > 0))
            {
                monter = " and (DocumentMaster.DocumentUploadDate between '" + Convert.ToDateTime(txtfrom.Text).AddDays(-1) + "' and '" + Convert.ToDateTime(txtto.Text).AddDays(1) + "')";
            }
        }
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentAccessRigthsByDesignationIDGene(Convert.ToString(ViewState["storeid"]));
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        int flag = 1;
        foreach (DataRow dr in dt.Rows)
        {
            dt1 = select("SELECT  distinct   DocumentMaster.DocumentId, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentName, DocumentMaster.DocumentUploadDate,DocumentMaster.DocumentDate, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId, DocumentMaster.DocumentRefNo, DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId,DocumentMaster.FileExtensionType, DocumentType.DocumentType,Party_master.Compname as PartyName FROM  DocumentMainType inner join  DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId INNER JOIN DocumentMaster  ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId LEFT OUTER JOIN DocumentProcessing ON DocumentMaster.DocumentId = DocumentProcessing.DocumentId LEFT OUTER JOIN Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId WHERE   (DocumentMainType.Whid='" + Convert.ToString(ViewState["storeid"]) + "') and (DocumentMaster.DocumentTypeId = '" + Convert.ToInt32(ddltypeofdoc.SelectedValue) + "') and DocumentMaster.DocumentId in ( SELECT     DocumentId FROM         DocumentProcessing WHERE     (Approve = 1) )  AND DocumentMaster.DocumentId not in ( SELECT DocumentId FROM         DocumentProcessing WHERE     (Approve = 0)OR (Approve  is null)) and DocumentMaster.CID='" + Session["Comid"] + "'  and (DocumentMaster.DocumentTypeId = '" + Convert.ToInt32(ddltypeofdoc.SelectedValue) + "')" + monter);
        }
        if (flag == 1)
        {
            dt2 = dt1.Clone();
            flag = 0;
        }
        foreach (DataRow r in dt1.Rows)
        {
            dt2.ImportRow(r);
        }
        DataView myDataView = new DataView();
        myDataView = dt2.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        Gridreqinfo.DataSource = dt2;
        Gridreqinfo.DataBind();
        if (Gridreqinfo.Rows.Count > 0)
        {
            btnatt.Visible = true;
        }
        else
        {
            btnatt.Visible = true;
        }
    }


    //--------------------------

    protected void Gridreqinfo_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        btnsearchgo_Click(sender, e);
    }
  
    protected void Gridreqinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void Gridreqinfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gridreqinfo.PageIndex = e.NewPageIndex;
        btnsearchgo_Click(sender, e);
    }

    protected void btnatt_Click(object sender, EventArgs e)
    {
        inserdocatt();
    }
    public void inserdocatt()
    {


        int k = 0;

        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            CheckBox chk1 = (CheckBox)gdr.FindControl("chkaccentry");

            LinkButton LinkButton1 = (LinkButton)gdr.FindControl("LinkButton1");
            int docid = Convert.ToInt32(LinkButton1.Text);


            if (chk1.Checked == true)
            {
                DataTable dcr = condc(docid);
                if (dcr.Rows.Count <= 0)
                {
                    k = k + 1;
                    string str12 = "Insert into OfficeManagerDocuments(MissionId,MissioninstructionId,MissionevId,LtgId,Ltgdetail,Ltgevalution,StgId,Stgdetail,Stgevalution,YgId,annid,Ydetail,Yeevalution,MgId,Mdetail,Mevalution,WgId,Wdetail,Wevalution,StrategyId,Strategydetail,Strategevaution,tectm,tectd,texte,taskid,projectid,projectevaid,DocumentId,StoreId,CommunicationId,InvCountId,EmployeeID,CandidateId)" +

                                     "values('" + ViewState["MissionId"] + "','" + ViewState["Mdetail"] + "','" + ViewState["Mevalution"] + "'," +
                                    "'" + ViewState["ltgm"] + "','" + ViewState["ltgd"] + "','" + ViewState["ltge"] + "'," +
                                     "'" + ViewState["stgm"] + "','" + ViewState["stgd"] + "','" + ViewState["stge"] + "'," +
                                     "'" + ViewState["yem"] + "','" + ViewState["annam"] + "','" + ViewState["yed"] + "','" + ViewState["yee"] + "'," +
                                     "'" + ViewState["mom"] + "','" + ViewState["mod"] + "','" + ViewState["moe"] + "'," +
                                     "'" + ViewState["wem"] + "','" + ViewState["wed"] + "','" + ViewState["wee"] + "'," +

                                     "'" + ViewState["stram"] + "','" + ViewState["strad"] + "','" + ViewState["strae"] + "'," +
                                     "'" + ViewState["tectm"] + "','" + ViewState["tectd"] + "','" + ViewState["tecte"] + "'," +
                                     "'" + ViewState["takid"] + "','" + ViewState["Proid"] + "','" + ViewState["Proevaid"] + "','" + LinkButton1.Text + "','" + ViewState["storeid"] + "','" + ViewState["commid"] + "','" + ViewState["InvCountId"] + "','" + ViewState["employeemmid"] + "','" + ViewState["CandidateId"] + "')";

                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();
                }

            }
        }
        if (k > 0)
        {
            statuslable.Text = "Record inserted successfully";
            statuslable.Visible = true;
            fillpop();
        }
        else
        {
            statuslable.Text = "Record already exist";
            statuslable.Visible = true;
        }
    }
    protected DataTable condc(int docid)
    {
        DataTable ds58 = new DataTable();
        if (Convert.ToInt32(ViewState["MissionId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["MissionId"]), 1, docid);
        }
        else if (Convert.ToInt32(ViewState["employeemmid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["employeemmid"]), 1, docid);
        }
        else if (Convert.ToInt32(ViewState["Mdetail"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["Mdetail"]), 11, docid);
        }
        else if (Convert.ToInt32(ViewState["Mevalution"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["Mevalution"]), 12, docid);
        }
        else if (Convert.ToInt32(ViewState["ltgm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["ltgm"]), 2, docid);
        }
        else if (Convert.ToInt32(ViewState["ltgd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["ltgd"]), 21, docid);
        }
        else if (Convert.ToInt32(ViewState["ltge"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["ltge"]), 22, docid);
        }


        else if (Convert.ToInt32(ViewState["stgm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["stgm"]), 3, docid);
        }
        else if (Convert.ToInt32(ViewState["stgd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["stgd"]), 31, docid);
        }
        else if (Convert.ToInt32(ViewState["stge"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["stge"]), 32, docid);
        }


        else if (Convert.ToInt32(ViewState["yem"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["yem"]), 4, docid);
        }
        else if (Convert.ToInt32(ViewState["annam"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["annam"]), 44, docid);
        }
        else if (Convert.ToInt32(ViewState["yed"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["yed"]), 41, docid);
        }
        else if (Convert.ToInt32(ViewState["yee"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["yee"]), 42, docid);
        }

        else if (Convert.ToInt32(ViewState["mom"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["mom"]), 5, docid);
        }
        else if (Convert.ToInt32(ViewState["mod"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["mod"]), 51, docid);
        }
        else if (Convert.ToInt32(ViewState["moe"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["moe"]), 52, docid);
        }

        else if (Convert.ToInt32(ViewState["wem"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["wem"]), 6, docid);
        }
        else if (Convert.ToInt32(ViewState["wed"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["wed"]), 61, docid);
        }
        else if (Convert.ToInt32(ViewState["wee"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["wee"]), 62, docid);
        }

        else if (Convert.ToInt32(ViewState["stram"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["stram"]), 7, docid);
        }
        else if (Convert.ToInt32(ViewState["strad"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["strad"]), 71, docid);
        }
        else if (Convert.ToInt32(ViewState["strae"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["strae"]), 72, docid);
        }

        else if (Convert.ToInt32(ViewState["tectm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["tectm"]), 8, docid);
        }
        else if (Convert.ToInt32(ViewState["tectd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["tectd"]), 81, docid);
        }
        else if (Convert.ToInt32(ViewState["tecte"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["tecte"]), 82, docid);
        }
        else if (Convert.ToInt32(ViewState["takid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["takid"]), 9, docid);
        }
        else if (Convert.ToInt32(ViewState["Proid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["Proid"]), 10, docid);
        }
        else if (Convert.ToInt32(ViewState["Proevaid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["Proevaid"]), 116, docid);
        }
        else if (Convert.ToInt32(ViewState["Proid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["commid"]), 111, docid);
        }
        else if (Convert.ToInt32(ViewState["InvCountId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["InvCountId"]), 112, docid);
        }
        else if (Convert.ToInt32(ViewState["CandidateId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["CandidateId"]), 113, docid);
        }

        return ds58;
    }
}
