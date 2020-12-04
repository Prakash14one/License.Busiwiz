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
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.DirectoryServices;
using System.IO.Compression;
using Ionic.Zip;
using System.Security.Cryptography;

using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Xml;

using System.Net;

using System.Net.Mail;

public partial class CustomerList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection connserver;
    public static string encstr = "";
    public static string Serverencstr = "";
    public static string verid = "";
    public static string macaddresss = "";
    string ComputerName = "";
    SqlConnection condefaultinstance = new SqlConnection();
    SqlConnection concompanyinstance = new SqlConnection();
    SqlConnection condefaultdatabase = new SqlConnection();

   
    private string sDomain = "TheSafestserver.com";
    private string sDefaultRootOU = "DC=TheSafestserver,DC=com";
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!IsPostBack)
        {   
            if (IsPostBack != true)
            {
                filldatebyperiod();
                fillPortal();
                fillServerName();
                fillnewgrid();
                ViewState["sortOrder"] = "";
                if (Request.QueryString["Delet"] != null)
                {
                    lblcompanyid.Text = Request.QueryString["Delet"].ToString();
                    lblserverid.Text = Request.QueryString["sid"].ToString();
                    btndelete_Click(sender, e);

                    pnl_popupdeleterestore.Visible = true;
                    pnldeletereson.Visible = false;
                    pnlGridcodeDatabase.Visible = true;

                    btnconformDelete.Visible = false;
                    lblmsgdeleterestore.Text = "Backup history";
                }
                if (Request.QueryString["RestoreCom"] != null)
                {
                    lblcompanyid.Text = Request.QueryString["RestoreCom"].ToString();
                    lblserverid.Text = Request.QueryString["sid"].ToString();
                    lblmsg.Text = " Successfully Restore " + lblcompanyid.Text + "";                    
                }                
            }            
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
        string lastyesr = ThisYear.ToString();
        if (lastmonthno == 0)
        {
            lastmonthno = 1;
            lastyesr = System.DateTime.Today.AddYears(-1).Year.ToString();
        }
        string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        DateTime lastmonth = Convert.ToDateTime(lastmonthNumber.ToString() + "/1/" + lastyesr);
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
        lastyesr = ThisYear.ToString();
        if (last2monthno == 0)
        {
            last2monthno = 12;
            lastyesr = System.DateTime.Today.AddYears(-1).Year.ToString();
        }
        if (last2monthno == -1)
        {
            last2monthno = 11;
            lastyesr = System.DateTime.Today.AddYears(-1).Year.ToString();
        }
        string last2monthNumber = Convert.ToString(last2monthno.ToString());
        DateTime last2month = Convert.ToDateTime(last2monthNumber.ToString() + "/1/" + lastyesr);
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
    public void fillnewgrid()
    {

                string st = "";
                string status = "";
                if (ddlfilters.SelectedIndex > 0)
                {
                    st += " and CompanyMaster.active=" + ddlfilters.SelectedValue + " ";
                }

        if (ddlportal.SelectedIndex > 0)
        {
            st += " and PortalMasterTbl.Id ='" + ddlportal.SelectedValue.ToString() + "' ";
        }

        if (ddlsortPlan.SelectedIndex > 0)
        {
            st += " and PricePlanMaster.PricePlanId='" + ddlsortPlan.SelectedValue + "' ";
        }
        if (ddlfillservernm.SelectedIndex > 0)
        {
            st += "  and  CompanyMaster.ServerId='" + ddlfillservernm .SelectedValue+ "' ";
        }
        if (txtsortsearch.Text != "")
        {
            st += " and ( CompanyMaster.CompanyName LIKE '%" + txtsortsearch.Text + "%' OR  CompanyMaster.CompanyLoginId LIKE '%" + txtsortsearch.Text + "%' ) ";
        }
        //----------------------Company Register Date-------------------------------------------------
        if (ddl_Companies_registered.SelectedItem.Text == "Today")
        {
            st += " and CompanyMaster.date = '" + System.DateTime.Now.ToShortDateString() + "'";
        }
        if (ddl_Companies_registered.SelectedItem.Text == "This Week")
        {
            st += " and CompanyMaster.date between '" + ViewState["thisweekstart"] + "' and '" + ViewState["thisweekend"] + "'";
        }
        if (ddl_Companies_registered.SelectedItem.Text == "This Month")
        {
            st += " and CompanyMaster.date between '" + ViewState["thismonthstartdate"] + "' and '" + ViewState["thismonthenddate"] + "'";
        }
        if (ddl_Companies_registered.SelectedItem.Text == "This Year")
        {            
            st += " and CompanyMaster.date between '" + ViewState["thisyearstartdate"] + "' and '" + ViewState["thisyearenddate"] + "'";
        }
        if (txtfrom.Text != "" && txtto.Text != "")
        {
          //  st += " and CompanyMaster.date >='" + Convert.ToDateTime(txtfrom.Text) + "' and CompanyMaster.date <='" + Convert.ToDateTime(txtto.Text) + "' ";
            st += " and (CompanyMaster.date between '" + Convert.ToDateTime(txtfrom.Text).ToShortDateString() + "' and '" + Convert.ToDateTime(txtto.Text).ToShortDateString() + "')";
        }
        //-----------------------------------------------------------------------------------

        //-----------------License Date----------------------------------------------------------

        if (ddllicensestart.SelectedItem.Text == "Today")
        {
            st += " and dbo.LicenseMaster.LicenseDate = '" + System.DateTime.Now.ToShortDateString() + "'";
        }
        if (ddllicensestart.SelectedItem.Text == "This Week")
        {
            st += " and dbo.LicenseMaster.LicenseDate between '" + ViewState["thisweekstart"] + "' and '" + ViewState["thisweekend"] + "'";
        }
        if (ddllicensestart.SelectedItem.Text == "This Month")
        {
            st += " and dbo.LicenseMaster.LicenseDate between '" + ViewState["thismonthstartdate"] + "' and '" + ViewState["thismonthenddate"] + "'";
        }
        if (ddllicensestart.SelectedItem.Text == "This Year")
        {
            st += " and dbo.LicenseMaster.LicenseDate between '" + ViewState["thisyearstartdate"] + "' and '" + ViewState["thisyearenddate"] + "'";
        }
        //---------------------------------------------------------------------------
        if (TextBox1.Text != "" && TextBox2.Text != "")
        {

            st += " and (dbo.LicenseMaster.LicenseDate between '" + Convert.ToDateTime(txtfrom.Text).ToShortDateString() + "' and '" + Convert.ToDateTime(txtto.Text).ToShortDateString() + "')";
        }
      
        if (ddlActive.SelectedIndex > 0)
        {
            st += " and CompanyMaster.active='" + ddlActive.SelectedValue + "' ";
        }

        string str = "  "; // ";
        str = @" SELECT DISTINCT dbo.ProductMaster.ProductId, dbo.ProductMaster.ClientMasterId, dbo.ProductMaster.ProductName, CompanyMaster.ServerId,dbo.PricePlanMaster.PricePlanId,  dbo.PricePlanMaster.PricePlanName, dbo.PricePlanMaster.active, dbo.PricePlanMaster.amountperOrder, dbo.CompanyMaster.CompanyName,  dbo.CompanyMaster.Email, dbo.CompanyMaster.CompanyId, dbo.CompanyMaster.CompanyLoginId,dbo.CompanyMaster.ContactPerson,dbo.CompanyMaster.phone, dbo.PortalMasterTbl.Id, dbo.PortalMasterTbl.PortalName, dbo.LicenseMaster.LicenseDate, dbo.LicenseMaster.LicensePeriod  FROM dbo.CompanyMaster INNER JOIN dbo.ProductMaster INNER JOIN dbo.PricePlanMaster ON dbo.ProductMaster.ProductId = dbo.PricePlanMaster.ProductId ON  dbo.CompanyMaster.PricePlanId = dbo.PricePlanMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.LicenseMaster ON dbo.CompanyMaster.CompanyId = dbo.LicenseMaster.CompanyId where dbo.CompanyMaster.CompanyLoginId !='' " + st + " and dbo.PortalMasterTbl.CompanyCreationOption='1'  ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);       
        GridView1.DataSource = ds;
        GridView1.DataBind();
        
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        fillPortal();
        fillServerName();
        fillnewgrid();
    }
    public void fillServerName()
    {
        
        // Status=1"
        string status = "";
        if (ddlfilters.SelectedIndex>0)
        {
            status = " and Status=" + ddlfilters.SelectedValue + " "; 
        }

        string str = "select * from ServerMasterTbl where ServerName !='' " + status + " ";

        // string str = "select * from CompanyMaster Case CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName where ClientId='" + Session["ClientId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlfillservernm.DataSource = ds;
        ddlfillservernm.DataTextField = "ServerName";
        ddlfillservernm.DataValueField = "Id";
        ddlfillservernm.DataBind();
        ddlfillservernm.Items.Insert(0, "---All Server---");

    }
    public void fillPortal()
    {
        //Status
        string status = "";
        if (ddlfilters.SelectedIndex > 0)
        {
            status = " and Status=" + ddlfilters.SelectedValue+ " ";
        }
        string str = " select * from PortalMasterTbl Where PortalMasterTbl.PortalName !='' " + status + " ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();
            foreach (DataRow dtRow in ds.Rows)
            {
                if (hTable.Contains(dtRow["PortalName"]))
                    duplicateList.Add(dtRow);
                else
                    hTable.Add(dtRow["PortalName"], string.Empty);
            }
            foreach (DataRow dtRow in duplicateList)
                ds.Rows.Remove(dtRow);


            if (ds.Rows.Count > 0)
            {
                ddlportal.DataSource = ds;
                ddlportal.DataTextField = "PortalName";
                ddlportal.DataValueField = "Id";
                ddlportal.DataBind();
                ddlportal.Items.Insert(0, "---Select All---");
            }
        }
    }
    public void fillPricePlan()
    {
        // CompanyMaster.active='1' and PricePlanMaster.active='1' 
        string status = "";
        if (ddlfilters.SelectedIndex > 0)
        {
            status = " and PricePlanMaster.active=" + ddlfilters .SelectedValue+ " ";
        }
        if (ddlportal.SelectedIndex >0)
        {
            ddlsortPlan.Items.Clear();
            
            string str22 = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PricePlanMaster.PricePlanName AS planname,PricePlanMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where   PortalMasterTbl.Id ='" + ddlportal.SelectedValue.ToString() + "' "+status+" ";
             str22 = "select * from PricePlanMaster  inner join PortalMasterTbl on PortalMasterTbl.Id= PortalMasterId1  Where PortalMasterTbl.Id ='" + ddlportal.SelectedValue.ToString() + "'  " + status + "";
             SqlCommand cmd = new SqlCommand(str22, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();
                foreach (DataRow dtRow in ds.Rows)
                {
                    if (hTable.Contains(dtRow["PricePlanName"]))
                        duplicateList.Add(dtRow);
                    else
                        hTable.Add(dtRow["PricePlanName"], string.Empty);
                }
                foreach (DataRow dtRow in duplicateList)
                    ds.Rows.Remove(dtRow);
                if (ds.Rows.Count > 0)
                {
                    ddlsortPlan.DataSource = ds;
                    ddlsortPlan.DataTextField = "PricePlanName";
                    ddlsortPlan.DataValueField = "PricePlanId";
                    ddlsortPlan.DataBind();
                    ddlsortPlan.Items.Insert(0, "---Select All---");
                }
            }
        }
        else
        {
            ddlsortPlan.Items.Clear();
            ddlsortPlan.Items.Insert(0, "---Select All---");
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing1(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {         
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lblcompany = row.FindControl("lblcompanlogin") as Label;
            lblcompanyid.Text = lblcompany.Text;           


            lblmsgdeleterestore.Text = " Are you sure to delete this company's code and database ? ";            
            pnl_popupdeleterestore.Visible = true;
            pnldeletereson.Visible = true;
            pnlGridcodeDatabase.Visible = false;
            lblbackup.Text = "no";
        }
        if (e.CommandName == "backup")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lblcompany = row.FindControl("lblcompanlogin") as Label;
            lblcompanyid.Text = lblcompany.Text;

            lblmsgdeleterestore.Text = " Are you sure to Backup this company's code and database ? ";
            pnl_popupdeleterestore.Visible = true;
            pnldeletereson.Visible = true;
            pnlGridcodeDatabase.Visible = false;
            lblbackup.Text = "yes";

            btndelete_Click(sender, e);
            lblmsgdeleterestore.Text = " Are you sure to Backup this company ";
        }
        if (e.CommandName == "Restore")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lblcompany_grid = row.FindControl("lblcompanlogin") as Label;
            Label lblserverid_grid = row.FindControl("lblserverid") as Label;

            lblcompanyid.Text = lblcompany_grid.Text;
            lblserverid.Text = lblserverid_grid.Text;
            // Restordatabaseweb("" + lblcompany.Text + "");                        
            // lbltitle.Text = " View Detal ";
            btndelete_Click(sender, e);
            lblmsgdeleterestore.Text = " Are you sure to restore this company ";
           
           // string strdetailgrd = " select dbo.CompanyBackupMasterTbl.Id, dbo.CompanyBackupMasterTbl.BackupStarted, dbo.CompanyBackupMasterTbl.BackupFinished, dbo.CompanyBackupMasterTbl.BackupStatus, dbo.CompanyBackupMasterTbl.Error, dbo.CompanyBackupMasterTbl.CompanyID, dbo.CompanyBackupMasterTbl.Date, dbo.CompanyBackupMasterTbl.Time, dbo.CompanyBackupMasterTbl.BackupFinishedDate, dbo.CompanyBackupMasterTbl.BackupFinishedTime, dbo.CompanyBackupMasterTbl.FolderName, dbo.CompanyBackupMasterTbl.BackupServerFtpId, dbo.Server_clientBackupFTP.FTPUserId, dbo.Server_clientBackupFTP.FTPfolder From dbo.CompanyBackupMasterTbl INNER JOIN dbo.Server_clientBackupFTP ON dbo.CompanyBackupMasterTbl.BackupServerFtpId = dbo.Server_clientBackupFTP.id Where CompanyBackupMasterTbl.CompanyID='" + lblcompany_grid.Text + "' ";
            string strdetailgrd = " SELECT dbo.CompanyBackupMasterTbl.Id, dbo.CompanyBackupMasterTbl.BackupStarted, dbo.CompanyBackupMasterTbl.BackupFinished, dbo.CompanyBackupMasterTbl.BackupStatus, dbo.CompanyBackupMasterTbl.Error, dbo.CompanyBackupMasterTbl.CompanyID, dbo.CompanyBackupMasterTbl.Date, dbo.CompanyBackupMasterTbl.Time, dbo.CompanyBackupMasterTbl.BackupFinishedDate, dbo.CompanyBackupMasterTbl.BackupFinishedTime, dbo.CompanyBackupMasterTbl.FolderName, dbo.CompanyBackupMasterTbl.BackupServerFtpId, dbo.Server_clientBackupFTP.FTPUserId, dbo.Server_clientBackupFTP.FTPfolder, dbo.ServerMasterTbl.ServerName FROM dbo.CompanyBackupMasterTbl INNER JOIN dbo.Server_clientBackupFTP ON dbo.CompanyBackupMasterTbl.BackupServerFtpId = dbo.Server_clientBackupFTP.id INNER JOIN dbo.ServerMasterTbl ON dbo.Server_clientBackupFTP.serverid = dbo.ServerMasterTbl.Id Where CompanyBackupMasterTbl.CompanyID='" + lblcompany_grid.Text + "'  ";
            SqlCommand cmddetailgrd = new SqlCommand(strdetailgrd, con);
            DataTable dtdetailgrd = new DataTable();
            SqlDataAdapter adpdetailgrd = new SqlDataAdapter(cmddetailgrd);
            adpdetailgrd.Fill(dtdetailgrd);           
            gvbackupstatus.DataSource = dtdetailgrd;
            gvbackupstatus.DataBind();

            pnlbackupstatus.Visible = true;

            lblbackup.Text = "no";

        }
         if (e.CommandName == "viewplan")
        {
            lbltitle.Text = "PricePlan Detail";
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lbl_planid = row.FindControl("lbl_planid") as Label;
          //  GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
           // int CompanyId = Convert.ToInt32(GridView1.SelectedDataKey.Value);
          //  Label lbl_planid1 = (Label)GridView1.Rows[GridView1.SelectedIndex].FindControl("lbl_planid");
            //pnldelete.Visible = false;
            pnlview.Visible = true;
            string strcln = " SELECT distinct PricePlanMaster.*,PortalMasterTbl.PortalName,  case when  (PricePlanMaster.active='1') then 'Active' else 'Inactive' end as Active1,  case when  PricePlanMaster.AllowIPTrack IS NULL     then 'No' else 'Yes' end as GBUSage1 , ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as ProductName " +
                        " FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join PricePlanMaster  " +
                        "  ON PricePlanMaster.VersionInfoMasterId= VersionInfoMaster.VersionInfoId inner join PortalMasterTbl on PortalMasterTbl.Id=PricePlanMaster.PortalMasterId1 " +
                        " where ClientMasterId= " + Session["ClientId"].ToString() + " and PricePlanMaster.PricePlanId='"+lbl_planid.Text+"' ";
            strcln = " SELECT DISTINCT dbo.PricePlanMaster.PricePlanId, dbo.PricePlanMaster.PricePlanName, dbo.PricePlanMaster.PricePlanDesc, dbo.PricePlanMaster.StartDate,  dbo.PricePlanMaster.EndDate, dbo.PricePlanMaster.PricePlanAmount, dbo.PricePlanMaster.DurationMonth, dbo.PricePlanMaster.AllowIPTrack,  dbo.PricePlanMaster.GBUsage, dbo.PricePlanMaster.MaxUser, dbo.PricePlanMaster.TrafficinGB, dbo.PricePlanMaster.TotalMail,  dbo.PricePlanMaster.CheckIntervalDays, dbo.PricePlanMaster.GraceDays, dbo.PricePlanMaster.PayperOrderPlan, dbo.PricePlanMaster.amountperOrder,  dbo.PricePlanMaster.FreeIntialOrders, dbo.PricePlanMaster.MinimumDeposite, dbo.PricePlanMaster.Maxamount, dbo.PricePlanMaster.PriceplancatId, dbo.PricePlanMaster.Producthostclientserver, dbo.PricePlanMaster.IsItFreeTryOutPlan, dbo.PricePlanMaster.BasePrice, dbo.PricePlanMaster.Plancatedefault, dbo.PricePlanMaster.Defaultpriceplanincategory, dbo.PortalMasterTbl.PortalName,  dbo.Priceplancategory.CategoryName FROM dbo.PricePlanMaster INNER JOIN dbo.PortalMasterTbl ON dbo.PortalMasterTbl.Id = dbo.PricePlanMaster.PortalMasterId1 INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID where  PricePlanMaster.PricePlanId='" + lbl_planid.Text + "' ";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            if (dtcln.Rows.Count > 0)
            {
                lblportalname.Text = dtcln.Rows[0]["PortalName"].ToString();
                lblcategory.Text = dtcln.Rows[0]["CategoryName"].ToString();
                lbl_planname.Text = dtcln.Rows[0]["PricePlanName"].ToString();
                pnl_paydetail.Visible = true;
                gvpopup.DataSource = null;
                gvpopup.DataBind();
                DataTable dtcln1 = select(" Select Distinct PriceplanrestrictionTbl.Id, NameofRest +'\n Maximum up to '+ Priceplanrestrecordallowtbl.RecordsAllowed as NameofRest,Priceplanrestrecordallowtbl.RecordsAllowed  ,NameofRest as NameofRest1, TextofQueinSelection,Priceaddingroup,Restingroup,  PriceplanrestrictionTbl.portalid ,PriceplanrestrictionTbl.id from PriceplanrestrictionTbl left join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id  inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where    Priceplanrestrecordallowtbl.PricePlanId='" + lbl_planid.Text + "' Order by PriceplanrestrictionTbl.Id ASC ");
                gvpopup.DataSource = dtcln1;
                gvpopup.DataBind();
                gvpopup.Visible = true;
            }

        }
       
        if (e.CommandName == "ReceivePayment")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int  CompanyLoginId =Convert.ToInt32(GridView1.SelectedDataKey.Value);
           // Label Orderid = (Label)GridView1.Rows[GridView1.SelectedIndex].FindControl("lblOrderId");
           // Session["Orderid"] = Orderid.Text;
         //   DeletewebsiteAttach(Request.QueryString["cid"].ToString());   
        }
    }
  
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_noofday = (Label)e.Row.FindControl("lbl_noofday");
                Label lbllicensedathide = (Label)e.Row.FindControl("lbllicensedathide");
                Label lbl_enddate = (Label)e.Row.FindControl("lbl_enddate");
                Label lblcompanlogin = (Label)e.Row.FindControl("lblcompanlogin");
                ImageButton imgbtndelete = (ImageButton)e.Row.FindControl("imgbtndelete");
                ImageButton imgbtnrestore = (ImageButton)e.Row.FindControl("imgbtnrestore");
                ImageButton imgbtnbackup = (ImageButton)e.Row.FindControl("imgbtnbackup");
                ImageButton imgbcant = (ImageButton)e.Row.FindControl("imgbcant");
                ImageButton imgbtnrestoremanual = (ImageButton)e.Row.FindControl("imgbtnrestoremanual");
                ImageButton imgbcantrest = (ImageButton)e.Row.FindControl("imgbcantrest");
                
                
                //DataTable dtbackupmaster = selectbackupdb(" select * from CompanyBackupMasterTbl where CompanyID='" + lblcompanlogin.Text + "' and BackupFinished='1' ");
                DataTable dtDeletecom = selectbackupdb(" select * from CompanyMasterDeletedByMasterAdmin where CompanyLoginid='" + lblcompanlogin.Text + "'");
                if (dtDeletecom.Rows.Count > 0)
                 {
                     imgbtnrestore.Visible = true;
                     imgbtndelete.Visible = false;

                     imgbcant.Visible = true;
                     imgbtnbackup.Visible = false; 
                 }
                DataTable dtbackupmaster = selectbackupdb(" select * from CompanyBackupMasterTbl where CompanyID='" + lblcompanlogin.Text + "' and BackupFinished='1'");
                if (dtbackupmaster.Rows.Count > 0)
                {
                    imgbtnrestoremanual.Visible = true;
                    imgbcantrest.Visible = false; 
                }
                
                DateTime dt =Convert.ToDateTime(lbllicensedathide.Text);
                // As String-----------------------------
                int noofday = Convert.ToInt32(lbl_noofday.Text);
                lbl_enddate.Text = dt.AddDays(noofday).ToString("MM/dd/yyyy");
                string  todaydate=System.DateTime.Now.ToShortDateString();
                if (Convert.ToDateTime(lbl_enddate.Text) < Convert.ToDateTime(todaydate))
                {
                   // lbl_enddate.CssClass = "btnFillGreen";
                    lbl_enddate.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillnewgrid();
    }
   
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        // fillgrid();
        fillnewgrid();
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
  
    protected void ddlfillservernm_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillnewgrid(); 
    }
    protected void ddlportal_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillPricePlan();
    }

    protected void ddlActive_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillnewgrid();
    }
    protected void ddl_Companies_registered_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Companies_registered.SelectedValue == "5")
        {
            pnlcompanyregisterdate.Visible = true; 
        }
        else
        {
            pnlcompanyregisterdate.Visible = false; 
            fillnewgrid();
        }
    }
    protected void ddllicensestart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllicensestart.SelectedValue == "5")
        {
            pnllicensedate.Visible = true; 
        }
        else
        {
            pnllicensedate.Visible = false; 
            fillnewgrid();
        }
    }

    
    //Popup
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
    }
    protected void fillddldataPopup()
    {     
    }

    protected void gvpopup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "GetRow")
        {
        }
    }

    protected void gvdatabase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "GetRow")
        {
        }
    }

   
    protected void gvcode_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "GetRow")
        {
        }
    }
    protected void gvcode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Request.QueryString["Ftpid"] != null)
                {
                    Label lblcom = (Label)e.Row.FindControl("lblcom");
                    Label lblid = (Label)e.Row.FindControl("lblid");
                    Label lblcode = (Label)e.Row.FindControl("lblcode");
                    ImageButton imgbtnsuccess = (ImageButton)e.Row.FindControl("imgbtnsuccess");
                    ImageButton imgbtnunsucccess = (ImageButton)e.Row.FindControl("imgbtnunsucccess");
                    DataTable dtbackupid = selectbackupdb(" SELECT dbo.CompanyBackupMasterTbl.CompanyID, dbo.CompanyBackupDetailTbl.CodeType, dbo.CompanyBackupMasterTbl.BackupFinished, dbo.CompanyBackupDetailTbl.BackupFinished AS Expr1 FROM dbo.CompanyBackupDetailTbl INNER JOIN dbo.CompanyBackupMasterTbl ON dbo.CompanyBackupDetailTbl.CompanyBackupMasterTblId = dbo.CompanyBackupMasterTbl.Id Where dbo.CompanyBackupMasterTbl.CompanyID='" + lblcom.Text + "' and  dbo.CompanyBackupDetailTbl.CodeType='" + lblcode.Text + "' and CompanyBackupDetailTbl.BackupFinished='1' and dbo.CompanyBackupMasterTbl.BackupServerFtpId=" + Request.QueryString["Ftpid"] + " ");
                    if (dtbackupid.Rows.Count > 0)
                    {
                        imgbtnsuccess.Visible = true;
                    }
                }
            }
        }
    }
    protected void gvdatabase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblcode = (Label)e.Row.FindControl("lblcode");
                Label lblid = (Label)e.Row.FindControl("lblid");
                Label lblcom = (Label)e.Row.FindControl("lblcom");
                ImageButton imgbtnsuccess = (ImageButton)e.Row.FindControl("imgbtnsuccess");  
                DataTable dtbackupid = selectbackupdb(" SELECT dbo.CompanyBackupMasterTbl.CompanyID, dbo.CompanyBackupDetailTbl.CodeType, dbo.CompanyBackupMasterTbl.BackupFinished, dbo.CompanyBackupDetailTbl.BackupFinished AS Expr1 FROM dbo.CompanyBackupDetailTbl INNER JOIN dbo.CompanyBackupMasterTbl ON dbo.CompanyBackupDetailTbl.CompanyBackupMasterTblId = dbo.CompanyBackupMasterTbl.Id Where dbo.CompanyBackupMasterTbl.CompanyID='" + lblcom.Text + "' and  dbo.CompanyBackupDetailTbl.CodeType='" + lblcode.Text + "' and CompanyBackupDetailTbl.BackupFinished='1' ");
                if (dtbackupid.Rows.Count > 0)
                {
                    imgbtnsuccess.Visible = true; 
                }               
            }
        }
    }
   

    public void feelpage()
    {
        
            string hostname = "";
            if (ddlfillservernm.SelectedValue == "User Server")
            {
                hostname = "True";
            }
            else
            {
                hostname = "false";
            }
            string active = "";
            if (ddlActive.SelectedValue.ToString() == "True")
            {
                active = "True";
            }
            else
            {
                active = "false";
            }
            string str = "";
            if (ddlportal.SelectedValue.ToString() != "---Select All---")
            {
                if (ddlsortPlan.SelectedValue.ToString() != "---Select All---")
                {
                    str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + ddlActive.SelectedValue + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' and PortalMasterTbl.Id ='" + ddlportal.SelectedValue.ToString() + "' and PricePlanMaster.PricePlanId='" + ddlsortPlan.SelectedValue + "' Order by CompanyMaster.date DESC";
                }
                else
                {
                    str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + ddlActive.SelectedValue + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' and PortalMasterTbl.Id ='" + ddlportal.SelectedValue.ToString() + "' Order by CompanyMaster.date DESC";

                }
            }
            else
            {
                str = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where CompanyMaster.active='" + active + "' and PricePlanMaster.active='1' and  CompanyMaster.HostId='" + hostname.ToString() + "' Order by CompanyMaster.date DESC";

            }

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        
    }
    protected void btngodate_Click(object sender, EventArgs e)
    {
      //  date();
        fillnewgrid(); 
    }
    
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
 
    protected void Button4_Click(object sender, EventArgs e)
    {
        pnl_paydetail.Visible = false;  
    }
    protected void Btn_popupdeleterestore_Click(object sender, EventArgs e)
    {
        pnl_popupdeleterestore.Visible = false;
    }

    protected void Btn_pnlbackupstatus_Click(object sender, EventArgs e)
    {
        pnlbackupstatus.Visible = false;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //------Delete Backup ------------------------------------------------------
    protected void btndelete_Click(object sender, EventArgs e)
    {      

        lblmsgdeleterestore.Text = " Are you sure to delete this company's code and database ?";
        lblpnldeleteresonmsg.Text = "";
        DataTable ds = MyCommonfile.selectBZ(" SELECT TOP (1) dbo.PortalMasterTbl.Id AS portlID, dbo.PortalMasterTbl.PortalName, dbo.PricePlanMaster.VersionInfoMasterId,dbo.PricePlanMaster.Producthostclientserver,dbo.ClientMaster.ClientURL, dbo.ClientMaster.ServerId AS ClientServerid, dbo.ServerMasterTbl.*, dbo.CompanyMaster.ServerId FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id  where CompanyLoginId='" + lblcompanyid.Text + "' ");
        if (ds.Rows.Count > 0)
        {
            string ComServerId = ds.Rows[0]["ServerId"].ToString();
            string Comp_serverweburl = ds.Rows[0]["Busiwizsatellitesiteurl"].ToString();
            string sqlserverport = ds.Rows[0]["port"].ToString();

            string sqlCompport28 = ds.Rows[0]["PortforCompanymastersqlistance"].ToString();//2810
            string ClientServerid = ds.Rows[0]["ClientServerid"].ToString();
            string ClientURL = ds.Rows[0]["ClientURL"].ToString();

            bool SerConnstatust = false;
            int timeconncheck = 1;

            condefaultinstance = ServerWizard.ServerDefaultInstanceConnetionTCP(lblcompanyid.Text);
            string Compserverconnnstr = condefaultinstance.ConnectionString;
            while ((!SerConnstatust) && (timeconncheck < 2))
            {
                try
                {
                    if (condefaultinstance.State.ToString() != "Open")
                    {
                        condefaultinstance.Open();
                        SerConnstatust = true;
                    }
                    else
                    {
                        SerConnstatust = true;
                    }
                }
                catch
                {
                    timeconncheck++;
                    //Port Open
                    //Page Y
                    string pagenamemainY = "Satelliteservfunction.aspx?PO=OpenPort&PortNo=" + BZ_Common.Encrypted_satsrvencryky(sqlserverport) + "&Port2No=" + BZ_Common.Encrypted_satsrvencryky(sqlCompport28) + "";//&SilentPageRequestTblID=" + PageMgmt.Encrypted(SilentPageRequestTblID) + "
                    //Page X
                    string mycurrenturlX = Request.Url.AbsoluteUri;
                    Random random = new Random();
                    int randomNumber = random.Next(1, 10);
                    string Randomkeyid = Convert.ToString(randomNumber);
                    string SilentPageRequestTblID = CompanyRelated.Insert_SilentPageRequestTbl(ComServerId, pagenamemainY, DateTime.Now.ToString(), "", false, Randomkeyid, mycurrenturlX);
                    string url = "";
                    url = "http://" + Comp_serverweburl + "/vfysrv.aspx?licensesilentpagerequesttblid=" + BZ_Common.Encrypted_satsrvencryky(SilentPageRequestTblID) + "&pageredirecturl=" + BZ_Common.Encrypted_satsrvencryky(pagenamemainY) + "&mstrsrvky=" + BZ_Common.Encrypted_satsrvencryky(BZ_Common.satsrvencryky()) + "&returnurl=" + BZ_Common.Encrypted_satsrvencryky(ClientURL) + "";
                    Response.Redirect("" + url + "");
                }
            }
            if (SerConnstatust == true)//if conn open (port open)
            {
                string strmaxiddatabase = " select * From  CompanyDatabaseDetailTbl Where CompanyLoginId='" + lblcompanyid.Text + "' ";
                SqlCommand cmdmaxiddatabase = new SqlCommand(strmaxiddatabase, condefaultinstance);
                SqlDataAdapter adpmaxiddatabase = new SqlDataAdapter(cmdmaxiddatabase);
                DataTable dsmaxiddatabase = new DataTable();
                adpmaxiddatabase.Fill(dsmaxiddatabase);
                gvdatabase.DataSource = dsmaxiddatabase;
                gvdatabase.DataBind();

                string strmaxidweb = " select * From  CompanyWebsiteDetailTbl Where CompanyLoginId='" + lblcompanyid.Text + "' ";
                SqlCommand cmdmaxidweb = new SqlCommand(strmaxidweb, condefaultinstance);
                SqlDataAdapter adpmaxiweb = new SqlDataAdapter(cmdmaxidweb);
                DataTable dsmaxiweb = new DataTable();
                adpmaxiweb.Fill(dsmaxiweb);
                gvcode.DataSource = dsmaxiweb;
                gvcode.DataBind();
                if (dsmaxiddatabase.Rows.Count > 0 && dsmaxiweb.Rows.Count > 0)
                {
                    pnldeletereson.Visible = false;
                    pnlGridcodeDatabase.Visible = true;
                    btnconformDelete.Visible = true;
                    btnrestore.Visible = false;
                    lblpnldeleteresonmsg.Text = "";
                    lblpnldeleteresonmsg.Visible = false;
                }
                else
                {
                    lblpnldeleteresonmsg.Visible = true;
                    lblpnldeleteresonmsg.Text = " No Data available for backup ";
                }
            }
            else
            {
                lblpnldeleteresonmsg.Text = "Some problem when going to connection at server";
            }
        }
       
       

        //string str = " SELECT CompanyMaster.*,PortalMasterTbl.PortalName,PricePlanMaster.VersionInfoMasterId,PricePlanMaster.Producthostclientserver from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join PortalMasterTbl on PricePlanMaster.PortalMasterId1 = PortalMasterTbl.id where CompanyLoginId='" + lblcompanyid.Text + "' ";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable ds = new DataTable();
        //adp.Fill(ds);
        //if (ds.Rows.Count > 0)
        //{
        //    string websitename = lblcompanyid.Text;
        //    websitename = ds.Rows[0]["CompanyLoginId"].ToString();
        //    verid = ds.Rows[0]["VersionInfoMasterId"].ToString();
        //    string priceplanid = ds.Rows[0]["PricePlanId"].ToString();
        //    string productid = ds.Rows[0]["ProductId"].ToString();
        //    string versionid = ds.Rows[0]["VersionInfoMasterId"].ToString();
        //    string portalname = ds.Rows[0]["PortalName"].ToString();

        //    ViewState["ownserver"] = ds.Rows[0]["Producthostclientserver"].ToString();

        //    string strserver = "";
        //    if (ViewState["ownserver"].ToString() == "True")
        //    {
        //        strserver = "select ServerMasterTbl.* from ServerMasterTbl inner join  CompanyMaster on CompanyMaster.CompanyLoginId=ServerMasterTbl.compid where CompanyMaster.CompanyLoginId='" + lblcompanyid.Text + "' and ServerMasterTbl.compid='" + lblcompanyid.Text + "'";
        //    }
        //    else
        //    {
        //        strserver = " SELECT ServerMasterTbl.* from ServerAssignmentMasterTbl inner join ServerMasterTbl on ServerMasterTbl.Id=ServerAssignmentMasterTbl.ServerId where ProductId='" + productid + "' and VersionId='" + versionid + "' and PricePlanId='" + priceplanid + "' and Active='1' ";
        //    }
        //    SqlCommand cmdserver = new SqlCommand(strserver, con);
        //    SqlDataAdapter adpserver = new SqlDataAdapter(cmdserver);
        //    DataTable dsserver = new DataTable();
        //    adpserver.Fill(dsserver);
        //    if (dsserver.Rows.Count > 0)
        //    {
        //        string defaultinstancename = dsserver.Rows[0]["DefaultsqlInstance"].ToString();
        //        string sqlserveurl = dsserver.Rows[0]["sqlurl"].ToString();
        //        string sqlinstancename = dsserver.Rows[0]["Sqlinstancename"].ToString();
        //        string sqlserverport = dsserver.Rows[0]["port"].ToString();
        //        string defaultdatabasename = dsserver.Rows[0]["DefaultDatabaseName"].ToString();
        //        string Companymastersqlistance = dsserver.Rows[0]["PortforCompanymastersqlistance"].ToString();
        //        string iiswebsitepath = dsserver.Rows[0]["serverdefaultpathforiis"].ToString();
        //        string iismdfpath = dsserver.Rows[0]["serverdefaultpathformdf"].ToString();
        //        string iisldfpath = dsserver.Rows[0]["serverdefaultpathforfdf"].ToString();
        //        Serverencstr = dsserver.Rows[0]["Enckey"].ToString();
        //        string sid = dsserver.Rows[0]["Id"].ToString();
        //        string sapassword = PageMgmt.Decrypted(dsserver.Rows[0]["Sapassword"].ToString());
        //        SqlConnection condefaultinstance = new SqlConnection();
        //        condefaultinstance = new SqlConnection();
        //        condefaultinstance.ConnectionString = @"Data Source =" + sqlserveurl + "\\" + defaultinstancename + "," + sqlserverport + "; Initial Catalog=" + defaultdatabasename + "; User ID=Sa; Password=" + sapassword + "; Persist Security Info=true;";
        //        if (condefaultinstance.State.ToString() != "Open")
        //        {
        //            condefaultinstance.Open();
        //        }
        //        condefaultinstance.Close();
        //    }           
        //}
      
    }

    protected void btnconformDelete_Click(object sender, EventArgs e)
    {         
        string strserver = " SELECT dbo.CompanyMaster.ServerId, dbo.CompanyMaster.CompanyLoginId, dbo.ServerMasterTbl.Busiwizsatellitesiteurl FROM dbo.CompanyMaster INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id  where CompanyMaster.CompanyLoginId='" + lblcompanyid.Text + "' ";
        SqlCommand cmdserver = new SqlCommand(strserver, con);
        SqlDataAdapter adpserver = new SqlDataAdapter(cmdserver);
        DataTable dsserver = new DataTable();
        adpserver.Fill(dsserver);
            if (dsserver.Rows.Count > 0)
            {
                DataTable dtcln = selectbackupdb(" SELECT dbo.Server_clientBackupFTP.id, dbo.Server_clientBackupFTP.FTPfolder,dbo.Server_clientBackupFTP.serverid, dbo.Server_clientBackupFTP.FTPurl, dbo.Server_clientBackupFTP.FTPPort, dbo.Server_clientBackupFTP.FTPUserId, dbo.Server_clientBackupFTP.FTPPassword, dbo.Server_clientBackupFTP.Description, dbo.Server_clientBackupFTP.location, dbo.Server_clientBackupFTP.selectdefauly, dbo.Server_clientBackupFTP.active, dbo.ServerMasterTbl.Id AS serveridd, dbo.ServerMasterTbl.ServerName FROM dbo.Server_clientBackupFTP INNER JOIN dbo.ServerMasterTbl ON dbo.Server_clientBackupFTP.serverid = dbo.ServerMasterTbl.Id Where  dbo.Server_clientBackupFTP.serverid=" + dsserver.Rows[0]["ServerId"].ToString() + " and selectdefauly='1' ");
                if (dtcln.Rows.Count > 0)
                {
                    if (lblbackup.Text != "yes")
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        SqlCommand cmda = new SqlCommand("Insert_CompanyMasterDeletedByMasterAdmin", con);
                        cmda.CommandType = CommandType.StoredProcedure;
                        cmda.Parameters.AddWithValue("@DeleteDate", System.DateTime.Now.ToShortDateString());
                        cmda.Parameters.AddWithValue("@CompanyLoginid", lblcompanyid.Text);
                        cmda.Parameters.AddWithValue("@ResonForDelete", txtreson.Text);
                        cmda.ExecuteNonQuery();
                        con.Close();
                    }
                    Response.Redirect("http://" + dsserver.Rows[0]["Busiwizsatellitesiteurl"].ToString() + "/ServerBackupDeleteRestoreCompany.aspx?DelCom=" + BZ_Common.BZ_Encrypted(lblcompanyid.Text) + "&sid=" + BZ_Common.BZ_Encrypted(dsserver.Rows[0]["ServerId"].ToString()) + "&backup=" + BZ_Common.BZ_Encrypted(lblbackup.Text) + "");
                }
                else
                {
                    lblpnldeleteresonmsg.Text = " No Any FTP account set for server  ";
                }
            }

        //createwebsiteandattach(""+lblcompanyid.Text+"");
        //btndelete_Click(sender ,e);
       
        Button3.Visible = false; 
    }
   
   //-----------------------------------------------------------------------------------

    //----------Restore

    protected void btnrestor_Click(object sender, EventArgs e)
    {
        lblmsgdeleterestore.Text = " Are you sure to restore this company ";
        pnldeletereson.Visible = false;
        pnlGridcodeDatabase.Visible = true;
        btnconformDelete.Visible = false;
        btnrestore.Visible = true; 
        string str = " delete from CompanyMasterDeletedByMasterAdmin where CompanyLoginid='" + lblcompanyid.Text + "'";
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand(str, con);
        cmd.ExecuteNonQuery();
        con.Close();

        string strserver = " SELECT dbo.CompanyMaster.ServerId, dbo.CompanyMaster.CompanyLoginId, dbo.ServerMasterTbl.Busiwizsatellitesiteurl FROM            dbo.CompanyMaster INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id  where CompanyMaster.CompanyLoginId='" + lblcompanyid.Text + "' ";
        SqlCommand cmdserver = new SqlCommand(strserver, con);
        SqlDataAdapter adpserver = new SqlDataAdapter(cmdserver);
        DataTable dsserver = new DataTable();
        adpserver.Fill(dsserver);
            if (dsserver.Rows.Count > 0)
            {
                Response.Redirect("http://" + dsserver.Rows[0]["Busiwizsatellitesiteurl"].ToString() + "/ServerBackupDeleteRestoreCompany.aspx?RestoreCom=" + lblcompanyid.Text + "&sid=" + dsserver.Rows[0]["ServerId"].ToString() + "&backup="+lblbackup.Text+"");
            }
        //createwebsiteandattach(lblcompanyid.Text,lblserverid.Text);

        
    }
    

    //-------------------------------------
    //-----------------------------

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
            //if (GridView1.Columns[6].Visible == true)
            //{
            //    ViewState["deleHide"] = "tt";
            //    GridView1.Columns[6].Visible = false;
            //}
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
            //if (ViewState["deleHide"] != null)
            //{
            //    GridView1.Columns[6].Visible = true;
            //}
        }
    }

    protected DataTable selectbackupdb(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
    //--------------------------BACKUP GRID------
    protected void gvbackupstatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Restore")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lblcomid = row.FindControl("lblcomid") as Label;
            Label lblrestoid = row.FindControl("lblrestoid") as Label;
            Label lblftpId = row.FindControl("lblftpId") as Label;
            
            //lblmsgdeleterestore.Text = " Are you sure to restore this company ";
            //pnldeletereson.Visible = false;
            //pnlGridcodeDatabase.Visible = true;
            //btnconformDelete.Visible = false;
            //btnrestore.Visible = true;
            string str = " delete from CompanyMasterDeletedByMasterAdmin where CompanyLoginid='" + lblcomid.Text + "'";
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            con.Close();

            string strserver = " SELECT dbo.CompanyMaster.ServerId, dbo.CompanyMaster.CompanyLoginId, dbo.ServerMasterTbl.Busiwizsatellitesiteurl FROM            dbo.CompanyMaster INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id  where CompanyMaster.CompanyLoginId='" + lblcomid.Text + "' ";
            SqlCommand cmdserver = new SqlCommand(strserver, con);
            SqlDataAdapter adpserver = new SqlDataAdapter(cmdserver);
            DataTable dsserver = new DataTable();
            adpserver.Fill(dsserver);
            if (dsserver.Rows.Count > 0)
            {
                Response.Redirect("http://" + dsserver.Rows[0]["Busiwizsatellitesiteurl"].ToString() + "/ServerBackupDeleteRestoreCompany.aspx?RestoreCom=" + lblcomid.Text + "&sid=" + dsserver.Rows[0]["ServerId"].ToString() + "&backupid=" + lblrestoid.Text + "&backup=" + lblbackup.Text + "&ftpid="+lblftpId.Text+"");
            }

        }
    }
    protected void gvbackupstatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblcom = (Label)e.Row.FindControl("lblcom");
                //Label lblid = (Label)e.Row.FindControl("lblid");
                //Label lblcode = (Label)e.Row.FindControl("lblcode");
                //ImageButton imgbtnsuccess = (ImageButton)e.Row.FindControl("imgbtnsuccess");
                //ImageButton imgbtnunsucccess = (ImageButton)e.Row.FindControl("imgbtnunsucccess");
                //DataTable dtbackupid = selectbackupdb(" SELECT dbo.CompanyBackupMasterTbl.CompanyID, dbo.CompanyBackupDetailTbl.CodeType, dbo.CompanyBackupMasterTbl.BackupFinished, dbo.CompanyBackupDetailTbl.BackupFinished AS Expr1 FROM dbo.CompanyBackupDetailTbl INNER JOIN dbo.CompanyBackupMasterTbl ON dbo.CompanyBackupDetailTbl.CompanyBackupMasterTblId = dbo.CompanyBackupMasterTbl.Id Where dbo.CompanyBackupMasterTbl.CompanyID='" + lblcom.Text + "' and  dbo.CompanyBackupDetailTbl.CodeType='" + lblcode.Text + "' and CompanyBackupDetailTbl.BackupFinished='1' ");
                //if (dtbackupid.Rows.Count > 0)
                //{
                //    imgbtnsuccess.Visible = true;
                //}
            }
        }
    }    
    //--------Close Backup Grid
}
