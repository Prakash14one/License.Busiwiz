using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Net.Security;
using System.Diagnostics;

using System.Text.RegularExpressions;
using System.Net;
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
public partial class AllWork : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection conioffce;
    protected void Page_Load(object sender, EventArgs e)
    {
       //Session["ClientId"] = "35";
        PageConn pgcon = new PageConn();
        conioffce = pgcon.dynconn;
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ChkActive.Checked = true;
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ViewState["empname"] = ddlemployee.SelectedValue.ToString();
            //ViewState["empname"] = ddls .SelectedValue.ToString();
            fillDepartment();
            fillemployee();
            ViewState["sortOrder"] = "";
           
            lblVersion.Visible = true;
            lblVersion.Text = "This is Version 5 Updated on 12/12/2015 by Priya";
            txtreportingdate.Text = System.DateTime.Now.ToShortDateString();
            txtsortdate.Text = System.DateTime.Now.ToShortDateString();
            txt_remindersearch.Text = System.DateTime.Now.ToShortDateString();
            FillGrid();
        }
        //txtreportingdate.Text = DateTime.Now.ToShortDateString();
    }
    protected void fillemployee()
    {
        //string strcln = " SELECT  dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid ,  dbo.EmployeeMaster.DeptID FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 " + strwhid + " ";
        //SqlCommand cmdcln = new SqlCommand(strcln, conioffce);
        //DataTable dtcln = new DataTable();
        //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //adpcln.Fill(dtcln);
        //ddlemployee.DataSource = dtcln;
        //ddlemployee.DataValueField = "License_Emp_id";
        //ddlemployee.DataTextField = "EmployeeName";
        //ddlemployee.DataBind();
        //ddlemployee.Items.Insert(0, "---Select All---");

      //  string strcln = " SELECT * from EmployeeMaster where Id='" + Session["EmpId"] + "'  order by Name  ";
        string strcln = " SELECT  dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid ,  dbo.EmployeeMaster.DeptID FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1  and dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id='" + Session["EmpId"] + "'   ";
        SqlCommand cmdcln = new SqlCommand(strcln, conioffce);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlemployee.DataSource = dtcln;
        ddlemployee.DataValueField = "License_Emp_id";
        ddlemployee.DataTextField = "EmployeeName";
        ddlemployee.DataBind();
        ddlstatus.DataSource = dtcln;
        ddlstatus.DataValueField = "License_Emp_id";
        ddlstatus.DataTextField = "EmployeeName";        
        ddlstatus.DataBind();
        ddlstatus.Enabled = false;


    }

    protected void FillGrid()
    {
        grdmonthlygoal.DataSource = null;
        grdmonthlygoal.DataBind();



        string strcln = "SELECT distinct * FROM ProjectMaster  INNER JOIN DesignationMaster ON DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId INNER JOIN EmployeeMaster ON EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id  where  ProjectMaster.ProjectMaster_Employee_Id='" + Session["EmpId"] + "' and DesignationMaster.ClientId='" + Session["ClientId"] + "'  ";
         if (ddlsortdept.SelectedValue.ToString() == "---Select All---")
        {
        }
        else
        {
            strcln += " AND ProjectMaster_DeptID = " + ddlsortdept.SelectedValue;
        }
         if (txt_remindersearch.Text != "")
         {
             //strcln += " AND ReminderDate ='" + txt_remindersearch.Text + "'";
             strcln += " and ProjectMaster.ReminderDate between '" + txt_remindersearch.Text + "' and '" + txt_remindersearch.Text + "'";
         }
         if (ddl_priortyadd.SelectedIndex > 0)
        {
            strcln += " AND ProjectMaster.Priority = '" + ddl_priortyadd.SelectedItem.Text + "'";// +  + "';
        }
        // if (ddlstatus.SelectedIndex > 0)
        //{
        
        //}
        //else
        //{
        //    strcln += " AND ProjectMaster.ProjectMaster_Employee_Id = " + ddlstatus.SelectedValue;
        //}
        //if (fromdate.Text != "" && txtsortdate.Text != "")
        //{
        //    strcln += " and ProjectMaster_StartDate between '" + fromdate.Text + "' and '" + txtsortdate.Text + "'";
        //}

        if (ddlsortdate.SelectedValue.ToString() == "---Select All---")
        {
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project Started Before this Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND ProjectMaster_StartDate <'" + txtsortdate.Text+ "'";
            }
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project Ended Before this Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND ProjectMaster_EndDate <'" + txtsortdate.Text + "'";
            }
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project having Target Before this Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND ProjectMaster_TargetEndDate <'" +txtsortdate.Text + "'";
            }
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project Started After this Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND  ProjectMaster_StartDate >'" + txtsortdate.Text + "'";

            }
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project Ended After this Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND ProjectMaster_EndDate >'" + txtsortdate.Text + "'";
            }
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project having Target After this Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND ProjectMaster_TargetEndDate >'" + txtsortdate.Text + "'";
            }
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project Started Selected Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND ProjectMaster_StartDate ='" + txtsortdate.Text + "'";
            }
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project Ended Selected Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND ProjectMaster_EndDate ='" +txtsortdate.Text + "'";
            }
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project having Target Selected Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND ProjectMaster_TargetEndDate ='" +txtsortdate.Text + "'";
            }
        }

        if (ddlsortmonth.SelectedValue.ToString() == "---Select All---")
        { }
        else
        {
            strcln += " AND ProjectMaster_ProjectStatus ='" + ddlsortmonth.SelectedValue.ToString() + "'";
        }
        if (txtsearch.Text != "")
        {
            strcln += " AND ProjectMaster_ProjectTitle like '%" + txtsearch.Text.Replace("'", "''") + "%'";
        }
        if (DropDownList1.SelectedValue == "1")
        {
            strcln += " AND ProjectMaster_TargetEndDate < '" + DateTime.Now.ToShortDateString() + "'";
        }
        else if (DropDownList1.SelectedValue == "2")
        {
            strcln += " AND ProjectMaster_TargetEndDate = '" + DateTime.Now.ToShortDateString() + "'";
        }
        else if (DropDownList1.SelectedValue == "3")
        {
            DateTime hh = DateTime.Now;
            hh = hh.AddDays(1);
            strcln += " AND ProjectMaster_TargetEndDate = '" + hh.ToShortDateString() + "'";
        }
        else if (DropDownList1.SelectedValue == "4")
        {
            DateTime hh = DateTime.Now;
            hh = hh.AddDays(7);
            strcln += " AND ProjectMaster_TargetEndDate between  '" + DateTime.Now.ToShortDateString() + "' and '" + hh.ToShortDateString() + "'";
        }
        else if (DropDownList1.SelectedValue == "5")
        {
            var now = DateTime.Now;
            var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            var lastDay = new DateTime(now.Year, now.Month, DaysInMonth);
            string lastDay1 = lastDay.ToString("dd-MM-yyyy");
            strcln += " AND ProjectMaster_TargetEndDate between  '" + DateTime.Now.ToShortDateString() + "' and '" + lastDay.ToShortDateString() + "'";
        }

        strcln += " ORDER BY ProjectMaster_StartDate  desc";

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            //DataTable dt_s = new DataTable();
            //for (int i = 0; i < dtcln.Rows.Count; i++)
            //{
            //    DateTime target = Convert.ToDateTime(dtcln.Rows[i]["ProjectMaster_TargetEndDate"].ToString());
            //    DateTime hh = DateTime.Now.Date;
            //    DateTime hh0 = hh.AddDays(1);

            //    DateTime hh1 = hh.AddDays(7);
            //    var now = DateTime.Now;
            //    var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            //    var lastDay = new DateTime(now.Year, now.Month, DaysInMonth);
            //    string lastDay1 = lastDay.ToString("dd-MM-yyyy");
            //    string deadline = "";
            //    if (target.Date < hh.Date)
            //    {
            //        deadline = "Overdue";
            //    }
            //    else if (target == hh)
            //    {
            //        deadline = "Due Today";
            //    }
            //    else if (target == hh0)
            //    {
            //        deadline = "Due Tomorrow";
            //    }
            //    else if (hh < target && hh1 > target)
            //    {
            //        deadline = "Due This Week";
            //    }
            //    else if (hh.Date < target.Date && target.Date <= lastDay.Date)
            //    {
            //        deadline = "Due This Month";
            //    }
            //    else
            //    {
            //        deadline = "Due next Month";
            //    }
            //    if (dt_s.Rows.Count < 1)
            //    {


            //        dt_s.Columns.Add("ProjectMaster_Id");
            //        dt_s.Columns.Add("ProjectMaster_Employee_Id");
            //        dt_s.Columns.Add("ProjectMaster_DeptID");
            //        dt_s.Columns.Add("Name");
            //        dt_s.Columns.Add("Name1");
            //        dt_s.Columns.Add("ProjectMaster_StartDate");
            //        dt_s.Columns.Add("ProjectMaster_ProjectTitle");
            //        dt_s.Columns.Add("ProjectMaster_EndDate");
            //        dt_s.Columns.Add("ProjectMaster_TargetEndDate");
            //        dt_s.Columns.Add("ProjectMaster_ProjectStatus");
            //        dt_s.Columns.Add("deadline");
            //    }
            //    DataRow dr = dt_s.NewRow();
            //    dr["ProjectMaster_Id"] = dtcln.Rows[i]["ProjectMaster_Id"].ToString();
            //    dr["ProjectMaster_Employee_Id"] = dtcln.Rows[i]["ProjectMaster_Employee_Id"].ToString();
            //    dr["ProjectMaster_DeptID"] = dtcln.Rows[i]["ProjectMaster_DeptID"].ToString();
            //    dr["Name"] = dtcln.Rows[i]["Name"].ToString();
            //    dr["Name1"] = dtcln.Rows[i]["Name1"].ToString();
            //    dr["ProjectMaster_StartDate"] = dtcln.Rows[i]["ProjectMaster_StartDate"].ToString();
            //    dr["ProjectMaster_ProjectTitle"] = dtcln.Rows[i]["ProjectMaster_ProjectTitle"].ToString();
            //    dr["ProjectMaster_EndDate"] = dtcln.Rows[i]["ProjectMaster_EndDate"].ToString();
            //    dr["ProjectMaster_TargetEndDate"] = dtcln.Rows[i]["ProjectMaster_TargetEndDate"].ToString();
            //    dr["ProjectMaster_ProjectStatus"] = dtcln.Rows[i]["ProjectMaster_ProjectStatus"].ToString();
            //    dr["deadline"] = deadline.ToString();
            //    dt_s.Rows.Add(dr);
            //}



            //DataView myDataView = new DataView();
            //myDataView = dtcln.DefaultView;

            //if (hdnsortExp.Value != string.Empty)
            //{
            //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            //}
            grdmonthlygoal.DataSource = dtcln;
            grdmonthlygoal.DataBind();
        }
        else
        {
            grdmonthlygoal.DataSource = null;
            grdmonthlygoal.DataBind();
        }
       
    }
    public void fillDepartment()
    {
        //  string str = "select * from DesignationMaster inner join (select Distinct WeeklyGoalMaster_DeptId as dept from WeeklyGoalMaster Where WeeklyGoalMaster_Active='1')  dept on dept = Id where Active='1' ";
        string str = "select  distinct DesignationMaster.Id,DesignationMaster.Name from DesignationMaster inner join EmployeeMaster on EmployeeMaster.DesignationId=DesignationMaster.Id where DesignationMaster.Active='1' and  EmployeeMaster.UserId='" + Session["Login"] + "' and DesignationMaster.ClientId='" + Session["ClientId"] + "'  ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlDeptName.DataSource = ds;
        ddlDeptName.DataTextField = "Name";
        ddlDeptName.DataValueField = "Id";
        ddlDeptName.DataBind();
        


        ddlsortdept.DataSource = ds;
        ddlsortdept.DataTextField = "Name";
        ddlsortdept.DataValueField = "Id";
        ddlsortdept.DataBind();
       
    }

    
    public void completegrid()
    {
        string strcln1 = "SELECT  distinct * From ProjectMaster  inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id  order by ProjectMaster_TargetEndDate DESC";
        //string strcln = "SELECT * From ProjectMaster  inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id Where ProjectMaster_ProjectStatus='Pending' order by ProjectMaster_TargetEndDate DESC";

        SqlCommand cmdcln = new SqlCommand(strcln1, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {

            DataView myDataView = new DataView();
            myDataView = dtcln.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            grdmonthlygoal.DataSource = dtcln;
            grdmonthlygoal.DataBind();
        }
    }
    protected void grdmonthlygoal_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void grdmonthlygoal_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void grdmonthlygoal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        pnlmonthgoal.Visible = true;
        addnewpanel.Visible = false;
        ChkActive.Visible = false;
        Label19.Text = " File Name ";
        lblmsg.Text = "";
        //  Label19.Text = "Add New Product or Version";
        //DateTime dd = DateTime.Now;
        //string formatted = dd.ToString("dd/MM/yyyy");
        //string dd1 = formatted.Replace('-', '/');
        //txtstartdate.Text = dd1;
        txtstartdate.Text = System.DateTime.Now.ToShortDateString();
        DateTime endDate = Convert.ToDateTime(this.txtstartdate.Text);
        Int64 addedDays = Convert.ToInt64(2);
        endDate = endDate.AddDays(addedDays);
        //endDate.AddDays(addedDays);
        DateTime end = endDate;
        

    }
    protected void grdmonthlygoal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Delete")
        //{
        //    grdmonthlygoal.EditIndex = -1;
        //    Int64 delid = Convert.ToInt32(e.CommandArgument);

        //    string str = "SELECT * From DailyGoalMaster  Where DailyGoal_Project_Id='" + delid + "'";


        //    SqlCommand cmdcln = new SqlCommand(str, con);
        //    DataTable dtcln = new DataTable();
        //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //    adpcln.Fill(dtcln);

        //    if (dtcln.Rows.Count > 0)
        //    {
        //        lblmsg.Visible = true;
        //        lblmsg.Text = "Please First Delete GeneralWork After Project Allocation deleted";
        //    }
        //    else
        //    {
        //        string st2 = "Delete from ProjectMaster where ProjectMaster_Id=" + delid;
        //        SqlCommand cmd2 = new SqlCommand(st2, con);
        //        con.Open();
        //        cmd2.ExecuteNonQuery();
        //        con.Close();
        //        FillGrid();
        //        lblmsg.Visible = true;
        //        lblmsg.Text = "Record deleted successfully ";
        //    }
        //}

        //if (e.CommandName == "Edit")
        //{
        //    ChkActive.Visible = true;
        //    grdmonthlygoal.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        //    grdmonthlygoal.EditIndex = -1;
        //    pnlmonthgoal.Visible = true;
        //    addnewpanel.Visible = false;
        //    Button1.Visible = false;
        //    btnupdate.Visible = true;
        //    Label19.Text = "Edit Project Allocation";
        //    lblmsg.Text = "";
        //    int mm1 = Convert.ToInt32(e.CommandArgument);
        //    ViewState["editid"] = mm1;
        //    SqlDataAdapter da = new SqlDataAdapter("SELECT * From ProjectMaster inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id where ProjectMaster.ProjectMaster_Id='" + mm1 + "'", con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    ddlDeptName.SelectedValue = dt.Rows[0]["ProjectMaster_DeptId"].ToString();
        //    ViewState["projectid"] = dt.Rows[0]["ProjectMaster_Id"].ToString();

        //    string str34 = "select DocumentTitle As PDFURL,DocumentFileName As Title ,DocumentData As AudioURL from DocumentMaster inner join ProjectMaster on ProjectMaster_Id = DocumentMaster_ProjectMaster_Id where DocumentMaster.DocumentMaster_ProjectMaster_Id='" + ViewState["projectid"] + "'";

        //    SqlCommand cmd34 = new SqlCommand(str34, con);
        //    SqlDataAdapter adp34 = new SqlDataAdapter(cmd34);
        //    DataSet ds34 = new DataSet();
        //    adp34.Fill(ds34);
        //    gridFileAttach.DataSource = ds34;
        //    gridFileAttach.DataBind();
            



        //    string str = "select * from EmployeeMaster where EmployeeMaster.DesignationId='" + ddlDeptName.SelectedValue + "' and EmployeeMaster.Active='1'  ";

        //    SqlCommand cmd = new SqlCommand(str, con);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);

        //    ddlemployee.DataSource = ds;
        //    ddlemployee.DataTextField = "Name";
        //    ddlemployee.DataValueField = "Id";
        //    ddlemployee.DataBind();
        //    ddlemployee.Items.Insert(0, "---Select All---");
        //    ddlemployee.SelectedValue = dt.Rows[0]["ProjectMaster_Employee_Id"].ToString();

        //    txtproname.Text = dt.Rows[0]["ProjectMaster_ProjectTitle"].ToString();
        //    txtedescription.Text = dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString();
        //    string startdate = dt.Rows[0]["ProjectMaster_StartDate"].ToString();
        //    DateTime start = Convert.ToDateTime(startdate.ToString()).Date;
        //    txtstartdate.Text = start.ToShortDateString();

        //    string enddate = dt.Rows[0]["ProjectMaster_EndDate"].ToString();
        //    DateTime end = Convert.ToDateTime(enddate.ToString()).Date;
        //    txtenddate.Text = end.ToShortDateString();

        //    string targetenddate = dt.Rows[0]["ProjectMaster_TargetEndDate"].ToString();
        //    DateTime target = Convert.ToDateTime(targetenddate.ToString()).Date;
        //    txttargetenddate.Text = target.ToShortDateString();

        //     txtstartdate.Text = dt.Rows[0]["ProjectMaster_StartDate"].ToString();
        //     txtenddate.Text = dt.Rows[0]["ProjectMaster_EndDate"].ToString();
        //    ViewState["monthid"] = dt.Rows[0]["ProjectMaster_Id"].ToString();
        //     txttargetenddate.Text = dt.Rows[0]["ProjectMaster_TargetEndDate"].ToString();
        //    ddlselectstatus.SelectedValue = dt.Rows[0]["ProjectMaster_ProjectStatus"].ToString();
        //    ddlselectstatus.SelectedValue = dt.Rows[0]["ProjectMaster_ProjectStatus"].ToString();
        //    if (dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString() != null && dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString() != "")
        //    {
        //        ChkDesc.Checked = true;
        //        txtedescription.Visible = true;
        //        txtedescription.Text = dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString();
        //    }

        //    if (dt.Rows[0]["ProjectMaster_Active"].ToString() == "True")
        //    {
        //        ChkActive.Checked = true;
        //    }
        //    else
        //    {
        //        ChkActive.Checked = false;
        //    }
        //    FillGrid();
        //    pnlup.Visible = true;
        //}
        if (e.CommandName == "View")
        {
            lblstsmsg.Visible = false;
            //grdmonthlygoal.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //int i = Convert.ToInt32(grdmonthlygoal.SelectedDataKey.Value);
            //grdmonthlygoal.EditIndex = -1;
            //Int64 i = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(e.CommandArgument);
            Session["proid"] = i;
            SqlDataAdapter da = new SqlDataAdapter("SELECT * From ProjectMaster inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id where ProjectMaster.ProjectMaster_Id='" + i + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Session["lbldep"] = dt.Rows[0]["ProjectMaster_DeptId"].ToString();
            Session["lblemp"] = dt.Rows[0]["ProjectMaster_Employee_Id"].ToString();

            //Label lblemp = (Label)grdmonthlygoal.Rows[grdmonthlygoal.SelectedIndex].FindControl("lblemp");
            // Session["lblemp"] =  lblemp.Text;

            // Label lbldep = (Label)grdmonthlygoal.Rows[grdmonthlygoal.SelectedIndex].FindControl("lbldep");
            // Session["lblemp"] = lbldep.Text;

            pnl_paydetail.Visible = true;
            pnlgrid.Visible = true;

            Label1.Text = "";
          
        }

       
        if (e.CommandName == "view111")
        {
           

            int mkl = Convert.ToInt32(e.CommandArgument);
            Session["viewid"] = mkl;
            string te = "ViewEmployeeProjectStatusLB.aspx?id=" + mkl;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
        if (e.CommandName == "View1")
        {
            grdmonthlygoal.EditIndex = -1;
            Session["viewid"] = e.CommandArgument;
            String js = "window.open('ViewEmployeeProjectStatusLB.aspx', '_blank');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open ViewEmployeeProjectStatus.aspx.aspx", js, true);

            // Response.Redirect("~/ProjectViewStatus.aspx");
        }
    }

    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
       

      
    }
    protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
       

    }
   
   
    protected void Button1_Click(object sender, EventArgs e)
    {

        
        string strcln = "Insert Into ProjectMaster (ProjectMaster_DeptId,ProjectMaster_ProjectTitle,ProjectMaster_ProjectDescription,ProjectMaster_Employee_Id,ProjectMaster_StartDate,ProjectMaster_EndDate,ProjectMaster_ProjectStatus,ProjectMaster_TargetEndDate,ProjectMaster_Active,insentivevalue) Values ('" + Convert.ToInt64(ddlDeptName.SelectedValue.ToString()) + "','" + txtproname.Text + "','" + txtedescription.Text + "','" + Convert.ToInt64(ddlemployee.SelectedValue.ToString()) + "','" + Convert.ToDateTime(txtstartdate.Text) + "','','Pending','" + Convert.ToDateTime(TextBox1.Text) + "','1','" + txtinsentive.Text + "')";

        SqlCommand cmd = new SqlCommand(strcln, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        string getprojectId = "select MAX(ProjectMaster_Id) As ID From ProjectMaster";
        SqlCommand cmdmonth = new SqlCommand(getprojectId, con);
        SqlDataAdapter adpmonth = new SqlDataAdapter(cmdmonth);
        DataTable dtmonth = new DataTable();
        adpmonth.Fill(dtmonth);
        if (dtmonth.Rows.Count > 0)
        {
            //dtmonth.Rows[][].ToString();
            ViewState["promaxid"] = dtmonth.Rows[0]["ID"].ToString();
        }


        if (gridFileAttach.Rows.Count > 0)
        {

            foreach (GridViewRow g1 in gridFileAttach.Rows)
            {
                string filenamedoc = (g1.FindControl("lbldoc") as Label).Text;
                string filename = (g1.FindControl("lblaudiourl") as Label).Text;
                string name = (g1.FindControl("lbltitle") as Label).Text;
                string str22 = "Insert Into DocumentMaster (DocumentMaster_ProjectMaster_Id,DocumentTitle,DocumentFileName,DocumentUploadDate,Doc) Values ('" + ViewState["promaxid"] + "','" + name + "','" + filename + "','" + DateTime.Now + "','" + filenamedoc + "')";
                //    string query = "insert into product values(" + id + ",'" + name + "'," + price + ",'" + description + "')";
                SqlCommand cmd12 = new SqlCommand(str22, con);
                con.Open();
                cmd12.ExecuteNonQuery();
                con.Close();
            }

        }

        lblmsg.Visible = true;
        lblmsg.Text = "Record has been Inserted Successfully";
        con.Close();
        txtproname.Text = txtedescription.Text = txtstartdate.Text = txtenddate.Text = TextBox1.Text = "";
        //ddlDeptName.SelectedIndex = ddlstatus.SelectedIndex = ddlemployee.SelectedIndex = ddlselectstatus.SelectedIndex = -1;
        ChkActive.Checked = ChkProDesc.Checked = false;
        txtedescription.Visible = false;
        pnlmonthgoal.Visible = false;
        addnewpanel.Visible = true;
        Label19.Text = "";

        chkupload.Checked = false;
        gridFileAttach.DataSource = null;
        gridFileAttach.DataBind();
    }

    public void clear()
    {
        txtproname.Text = txtedescription.Text = txtstartdate.Text = txtenddate.Text = txttargetenddate.Text = txtproname.Text = "";
        ddlDeptName.SelectedIndex = ddlemployee.SelectedIndex = -1;
        ChkActive.Checked = ChkProDesc.Checked = false;
        txtedescription.Visible = false;
    }




    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txt_reminder.Text) < DateTime.Now)
        {
            lblenddateerror.Text = "remainder date should not be past one";
            txt_reminder.Text = "";
        }
        else
        {
            lblenddateerror.Text = "";
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {

        string ext = "";
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "pdf", "PDF", "wma", "html", "css", "rar", "zip", "rpt" };
        string[] validFileTypes1 = { "MP3", "MP4", ".mp3","mp3", ".mp4", ".MP3", ".m4a", "m4a", ".M4A", ".wav", "wav" };
        bool isValidFile = false;
        if (Upradio.SelectedValue == "1")
        {

            ext = System.IO.Path.GetExtension(fileuploadaudio.PostedFile.FileName);
            for (int i = 0; i < validFileTypes1.Length; i++)
            {                
                if (ext == "." + validFileTypes1[i])
                {

                    isValidFile = true;

                    break;

                }

            }
        }
        else
        {
            if (fileuploadadattachment.HasFile == true)
            {

                ext = System.IO.Path.GetExtension(fileuploadadattachment.PostedFile.FileName);
                for (int i = 0; i < validFileTypes.Length; i++)
                {

                    if (ext == "." + validFileTypes[i])
                    {

                        isValidFile = true;

                        break;

                    }

                }
            }
        }
      

        if (!isValidFile)
        {

            lblmsg.Visible = true;
            if (Upradio.SelectedValue == "1")
            {
                lblmsg.Text = "Invalid File. Please upload a File with extension " +

                               string.Join(",", validFileTypes1);
            }
            else
            {
                lblmsg.Text = "Invalid File. Please upload a File with extension " +

                              string.Join(",", validFileTypes);
            }

        }

        else
        {

            String filename = "";
            String docfile = "";
            string audiofile = "";
            //PnlFileAttachLbl.Visible = true;
            if (fileuploadadattachment.HasFile || fileuploadaudio.HasFile)
            {
                if (fileuploadadattachment.HasFile)
                {
                    docfile = fileuploadadattachment.FileName;
                    filename = fileuploadadattachment.FileName;
                    fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Clientadmin\\Attach\\") + filename);
                    fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~/Clientadmin/Uploads/") + filename);
                    string file = Path.GetFileName(fileuploadadattachment.PostedFile.FileName);
                    Stream str = fileuploadadattachment.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(str);
                    Byte[] size = br.ReadBytes((int)str.Length);

                }
                if (fileuploadaudio.HasFile)
                {
                    audiofile = fileuploadaudio.FileName;
                    fileuploadaudio.PostedFile.SaveAs(Server.MapPath("~\\Clientadmin\\Attach\\") + audiofile);
                    fileuploadaudio.PostedFile.SaveAs(Server.MapPath("~/Clientadmin/Uploads/") + audiofile);
                    //Licecense.bisi/
                }
                //hdnFileName.Value = filename;
                DataTable dt = new DataTable();
                if (Session["GridFileAttach1"] == null)
                {
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "PDFURL";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);

                    DataColumn dtcom3 = new DataColumn();
                    dtcom3.DataType = System.Type.GetType("System.String");
                    dtcom3.ColumnName = "Title";
                    dtcom3.ReadOnly = false;
                    dtcom3.Unique = false;
                    dtcom3.AllowDBNull = true;
                    dt.Columns.Add(dtcom3);

                    DataColumn dtcom4 = new DataColumn();

                    dtcom4.DataType = System.Type.GetType("System.String");
                    dtcom4.ColumnName = "AudioURL";
                    dtcom4.ReadOnly = false;
                    dtcom4.Unique = false;
                    dtcom4.AllowDBNull = true;
                    dt.Columns.Add(dtcom4);

                    DataColumn dtcom45 = new DataColumn();

                    dtcom45.DataType = System.Type.GetType("System.String");
                    dtcom45.ColumnName = "Doc";
                    dtcom45.ReadOnly = false;
                    dtcom45.Unique = false;
                    dtcom45.AllowDBNull = true;
                    dt.Columns.Add(dtcom45);

                }
                else
                {
                    dt = (DataTable)Session["GridFileAttach1"];
                }
                DataRow dtrow = dt.NewRow();
                dtrow["PDFURL"] = filename;
                dtrow["Title"] = txttitlename.Text;
                dtrow["AudioURL"] = audiofile;
                dtrow["Doc"] = docfile;

                //dtrow["FileNameChanged"] = hdnFileName.Value;
                dt.Rows.Add(dtrow);
                Session["GridFileAttach1"] = dt;
                gridFileAttach.DataSource = dt;


                gridFileAttach.DataBind();
                txttitlename.Text = "";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please Attach File to Upload.";
                return;
            }
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {


        //string chkactive = "";
        //if (ChkActive.Checked)
        //{
        //    chkactive = "True";
        //}
        //else
        //{
        //    chkactive = "false";
        //}
        //string str = "update ProjectMaster set ProjectMaster.ProjectMaster_DeptId='" + ddlDeptName.SelectedValue.ToString() + "', ProjectMaster.ProjectMaster_Employee_Id ='" + ddlemployee.SelectedValue.ToString() + "' , ProjectMaster.ProjectMaster_ProjectTitle= '" + txtproname.Text + "', ProjectMaster.ProjectMaster_ProjectDescription='" + txtedescription.Text + "',ProjectMaster.ProjectMaster_StartDate='" + Convert.ToDateTime(txtstartdate.Text) + "',ProjectMaster.ProjectMaster_EndDate='" + Convert.ToDateTime(txtenddate.Text) + "' , ProjectMaster.ProjectMaster_TargetEndDate ='" + Convert.ToDateTime(txttargetenddate.Text) + "', ProjectMaster.ProjectMaster_ProjectStatus=' Completed' , ProjectMaster.ProjectMaster_Active ='" + chkactive.ToString() + "', ProjectMaster.insentivevalue ='" + txtinsentive.Text + "' From ProjectMaster  where ProjectMaster.ProjectMaster_Id='" + ViewState["editid"] + "'";
        //SqlCommand cmd = new SqlCommand(str, con);
        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();

        //grdmonthlygoal.EditIndex = -1;
        //completegrid();
        //txtproname.Text = txtedescription.Text = txtstartdate.Text = txtenddate.Text = txttargetenddate.Text = string.Empty;
        //ddlDeptName.SelectedIndex = ddlemployee.SelectedIndex = -1;
        //ChkActive.Checked = ChkProDesc.Checked = false;
        //txtedescription.Visible = false; lblmsg.Visible = true;
        //lblmsg.Text = "Record updated successfully";
        //btnupdate.Visible = false;
        //Button1.Visible = true;
        //addnewpanel.Visible = true;
        //pnlmonthgoal.Visible = false;

    }
    protected void Upradio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Upradio.SelectedValue == "1")
        {
            pnladio.Visible = true;
            pnlpdfup.Visible = false;
        }
        else
        {
            pnladio.Visible = false;
            pnlpdfup.Visible = true;
        }
    }
    protected void gridFileAttach_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete1")
        {
            gridFileAttach.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            if (Session["GridFileAttach1"] != null)
            {
                if (gridFileAttach.Rows.Count > 0)
                {
                    dt = (DataTable)Session["GridFileAttach1"];

                    dt.Rows.Remove(dt.Rows[gridFileAttach.SelectedIndex]);


                    gridFileAttach.DataSource = dt;
                    gridFileAttach.DataBind();
                    Session["GridFileAttach1"] = dt;
                }
            }

        }
    }
   
    public void ftpfile(string inputfilepath, string filename)
    {

        string strcount = " select WebsiteMaster.* from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId where ProductMaster.ProductId='" + Session["ProductId"].ToString() + "'";
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);
        if (dtcount.Rows.Count > 0)
        {
            string[] separator1 = new string[] { "/" };
            string[] strSplitArr1 = dtcount.Rows[0]["FTPWorkGuideUrl"].ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            String productno = strSplitArr1[0].ToString();
            string ftpurl = "";

            if (productno == "FTP:" || productno == "ftp:")
            {
                if (strSplitArr1.Length >= 3)
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);

                }

            }
            string ftphost = ftpurl + "/" + inputfilepath;
            // string ftphost = Convert.ToString( dtcount.Rows[0]["FTPWorkGuideUrl"]) + "/" + inputfilepath;
            FtpWebRequest FTP = (FtpWebRequest)FtpWebRequest.Create(ftphost);
            FTP.Credentials = new NetworkCredential(Convert.ToString(dtcount.Rows[0]["FTPWorkGuideUserId"]), PageMgmt.Decrypted(Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePW"])));
            FTP.UseBinary = false;
            FTP.KeepAlive = true;
            FTP.UsePassive = true;


            FTP.Method = WebRequestMethods.Ftp.UploadFile;
            FileStream fs = File.OpenRead(filename);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
            Stream ftpstream = FTP.GetRequestStream();
            ftpstream.Write(buffer, 0, buffer.Length);
            ftpstream.Close();

        }
        System.IO.File.Delete(filename);
    }

    
    protected void ChkProDesc_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkProDesc.Checked)
        {
            txtedescription.Visible = false;
        }
        else
        {
            txtedescription.Visible = true;
        }
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        clear();
        pnlmonthgoal.Visible = false;
        // completegrid();
        FillGrid();
        addnewpanel.Visible = true;
        Label19.Text = "";

    }
    protected void ddlsortdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddlsortmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
       
       


    }

    protected void grdmonthlygoal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdmonthlygoal.PageIndex = e.NewPageIndex;
        FillGrid();

        //if (txtsortdate.Text == "" && ddlstatus.SelectedValue == "---Select All---")
        //{
        //    FillGrid();
        //}
        //else
        //{
        //    FillGrid();
        //}

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button5.Visible = true;
            //if (grdmonthlygoal.Columns[7].Visible == true)
            //{
            //    ViewState["editHide"] = "tt";
            //    grdmonthlygoal.Columns[7].Visible = false;
            //}
            //if (grdmonthlygoal.Columns[8].Visible == true)
            //{
            //    ViewState["editHide"] = "tt";
            //    grdmonthlygoal.Columns[8].Visible = false;
            //}
            //if (grdmonthlygoal.Columns[9].Visible == true)
            //{
            //    ViewState["deleHide"] = "tt";
            //    grdmonthlygoal.Columns[9].Visible = false;
            //}
        }
        else
        {
            Button4.Text = "Printable Version";
            Button5.Visible = false;
            if (ViewState["editHide"] != null)
            {
                grdmonthlygoal.Columns[7].Visible = true;
            }
            if (ViewState["editHide"] != null)
            {
                grdmonthlygoal.Columns[8].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                grdmonthlygoal.Columns[9].Visible = true;
            }
        }
    }
    protected void ddlselectstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CurrentMonth = String.Format("{0:MMMM}", DateTime.Now);
        string Currentyear = String.Format("{0:yyyy}", DateTime.Now);
        string monthyear = Currentyear + " - " + CurrentMonth;
       

    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        FillGrid();
        lblmsg.Text = "";
    }


    protected void Button9_Click(object sender, EventArgs e)
    {
        pnl_paydetail.Visible = false;
        lblmsg.Text = "";
    }

    protected void ChkPro_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
          
        }
        else
        {
           
        }
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
       

        if (CheckBox1.Checked == true)
        {
        string strcln = " Insert Into ProjectStatus (ProjectStatus_Dept_Id,ProjectStatus_Emp_Id,ProjectStatus_ProjectId,ProjectStatus_Date,ProjectStatus_Status,ProjectStatus_Status_Description,ProjectStatus_Active) Values ('" + Session["lbldep"] + "','" +  Session["lblemp"] + "','" + Session["proid"]  + "','" + Convert.ToDateTime(txtreportingdate.Text) + "','Completed','" + txtprogress.Text + "','1')";
        SqlCommand cmd = new SqlCommand(strcln, con);
        con.Close();
        con.Open();
        cmd.ExecuteNonQuery();
       

        try
        {
            con.Close();
            DateTime reminderdate = Convert.ToDateTime(txt_reminder.Text);
            string strc = "Update ProjectMaster set ProjectMaster_ProjectStatus='Completed' ,ProjectMaster_EndDate='" + Convert.ToDateTime(txtreportingdate.Text) + "', ReminderDate='" + reminderdate + "' where ProjectMaster_Id='" + Session["proid"] + "'";
            SqlCommand cmd1 = new SqlCommand(strc, con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            con.Close();
            string strc = "Update ProjectMaster set ProjectMaster_ProjectStatus='Completed' ,ProjectMaster_EndDate='" + Convert.ToDateTime(txtreportingdate.Text) + "' where ProjectMaster_Id='" + Session["proid"] + "'";
            SqlCommand cmd1 = new SqlCommand(strc, con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
        }
        }
        else
        {
            string strcln = " Insert Into ProjectStatus (ProjectStatus_Dept_Id,ProjectStatus_Emp_Id,ProjectStatus_ProjectId,ProjectStatus_Date,ProjectStatus_Status,ProjectStatus_Status_Description,ProjectStatus_Active) Values ('" + Session["lbldep"] + "','" + Session["lblemp"] + "','" + Session["proid"] + "','" + Convert.ToDateTime(txtreportingdate.Text) + "','Pending','" + txtprogress.Text + "','1')";
            SqlCommand cmd = new SqlCommand(strcln, con);
            con.Close();
            con.Open();
            cmd.ExecuteNonQuery();
          

            try
            {
                DateTime reminderdate = Convert.ToDateTime(txt_reminder.Text);
                con.Close();
                string strc = "Update ProjectMaster set ProjectMaster_ProjectStatus='Pending' ,ProjectMaster_EndDate=NULL, ReminderDate='" + reminderdate + "' where ProjectMaster_Id='" + Session["proid"] + "'";
                SqlCommand cmd1 = new SqlCommand(strc, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                string strc = "Update ProjectMaster set ProjectMaster_ProjectStatus='Pending' ,ProjectMaster_EndDate=NULL where ProjectMaster_Id='" + Session["proid"] + "'";
                SqlCommand cmd1 = new SqlCommand(strc, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }

        }
        FillGrid();

        if (gridstsFileAttach.Rows.Count > 0)
        {

            foreach (GridViewRow g1 in gridstsFileAttach.Rows)
            {

                string filenamedoc = (g1.FindControl("lbldoc") as Label).Text;
                string audio = (g1.FindControl("lblstsaudiourl") as Label).Text;
                string name = (g1.FindControl("lblststitle") as Label).Text;
                string str22 = "Insert Into DocumentMaster (DocumentMaster_ProjectMaster_Id,DocumentTitle,DocumentFileName,DocumentUploadDate,Doc) Values ('" + Session["proid"] + "','" + name + "','" + audio + "','" + DateTime.Now + "','" + filenamedoc + "')";
                //    string query = "insert into product values(" + id + ",'" + name + "'," + price + ",'" + description + "')";
                SqlCommand cmd12 = new SqlCommand(str22, con);
                con.Open();
                cmd12.ExecuteNonQuery();
                con.Close();
            }

        }
        FillGrid();
        lblmsg.Visible = true;
        lblmsg.Text = "Progress report is added sucessfully";
        pnl_paydetail.Visible = false;
        con.Close();
        txtreportingdate.Text = "";
        txtprogress.Text = "";
        CheckBox1.Checked = false;
        txtststitle.Text = "";
       

      
       

        
    }
    protected void Chkupld_CheckedChanged(object sender, EventArgs e)
    {
        if (Chkupld.Checked == true)
        {
            Pnlstsup.Visible = true;
        }
        else
        {
            Pnlstsup.Visible = false;
        }
    }
    protected void rdoselct_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoselct.SelectedValue == "1")
        {
            Pnlad.Visible = true;
            Pnlof.Visible = false;
        }
        else
        {
            Pnlad.Visible = false;
            Pnlof.Visible = true;
        }
    }

    protected void Button6_Click(object sender, EventArgs e)
    {

        string ext = "";
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "pdf", "PDF", "wma", "html", "css", "rar", "zip", "rpt" };
        string[] validFileTypes1 = { "MP3", "MP4", "mp3", ".mp3", "mp3", ".mp4", ".MP3", ".m4a", "m4a", ".M4A", ".wav", "wav" };
        bool isValidFile = false;
        if (rdoselct.SelectedValue == "1")
        {

            if (FileUpload2.HasFile == true)
            {

                ext = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
                for (int i = 0; i < validFileTypes1.Length; i++)
                {
                    isValidFile = true;
                    if (ext == "." + validFileTypes1[i])
                    {

                        isValidFile = true;

                        break;

                    }

                }
            }
        }
        else
        {
            if (FileUpload1.HasFile == true)
            {

                ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                for (int i = 0; i < validFileTypes.Length; i++)
                {

                    if (ext == "." + validFileTypes[i])
                    {

                        isValidFile = true;

                        break;

                    }

                }
            }
        }
        isValidFile = true;

        if (!isValidFile)
        {

            lblstsmsg.Visible = true;
            if (rdoselct.SelectedValue == "1")
            {
                lblstsmsg.Text = "Invalid File. Please upload a File with extension " +

                               string.Join(",", validFileTypes1);
            }
            else
            {
                lblstsmsg.Text = "Invalid File. Please upload a File with extension " +

                              string.Join(",", validFileTypes);
            }

        }

        else
        {

            String filename = "";



            string audiofile = "";
            //PnlFileAttachLbl.Visible = true;
            if (FileUpload1.HasFile || FileUpload2.HasFile)
            {
                if (FileUpload1.HasFile)
                {
                    filename = FileUpload1.FileName;
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Clientadmin\\Attach\\") + filename);
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Clientadmin/Uploads/") + filename);
                    string file = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    Stream str = FileUpload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(str);
                    Byte[] size = br.ReadBytes((int)str.Length);

                }
                if (FileUpload2.HasFile)
                {
                    audiofile = FileUpload2.FileName;
                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~\\Clientadmin\\Attach\\") + audiofile);
                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~/Clientadmin/Uploads/") + audiofile);
                }
                //hdnFileName.Value = filename;
                DataTable dtsts = new DataTable();
                if (Session["GridFileAttach12"] == null)
                {
                    DataColumn dtcom21 = new DataColumn();
                    dtcom21.DataType = System.Type.GetType("System.String");
                    dtcom21.ColumnName = "PDFURL";
                    dtcom21.ReadOnly = false;
                    dtcom21.Unique = false;
                    dtcom21.AllowDBNull = true;
                    dtsts.Columns.Add(dtcom21);

                    DataColumn dtcom31 = new DataColumn();
                    dtcom31.DataType = System.Type.GetType("System.String");
                    dtcom31.ColumnName = "Title";
                    dtcom31.ReadOnly = false;
                    dtcom31.Unique = false;
                    dtcom31.AllowDBNull = true;
                    dtsts.Columns.Add(dtcom31);

                    DataColumn dtcom41 = new DataColumn();

                    dtcom41.DataType = System.Type.GetType("System.String");
                    dtcom41.ColumnName = "AudioURL";
                    dtcom41.ReadOnly = false;
                    dtcom41.Unique = false;
                    dtcom41.AllowDBNull = true;
                    dtsts.Columns.Add(dtcom41);

                    DataColumn dtcomm = new DataColumn();
                    dtcomm.DataType = System.Type.GetType("System.String");
                    dtcomm.ColumnName = "Doc";
                    dtcomm.ReadOnly = false;
                    dtcomm.Unique = false;
                    dtcomm.AllowDBNull = true;
                    dtsts.Columns.Add(dtcomm);

                }
                else
                {
                    dtsts = (DataTable)Session["GridFileAttach12"];
                }
                DataRow dtroww = dtsts.NewRow();
                dtroww["PDFURL"] = "";
                dtroww["Title"] = txtststitle.Text;
                dtroww["AudioURL"] = audiofile;
                dtroww["Doc"] = audiofile;
                //dtrow["FileNameChanged"] = hdnFileName.Value;
                dtsts.Rows.Add(dtroww);
                Session["GridFileAttach12"] = dtsts;
                gridstsFileAttach.DataSource = dtsts;


                gridstsFileAttach.DataBind();
                txtststitle.Text = "";
            }
            else
            {
                lblstsmsg.Visible = true;
                lblstsmsg.Text = "Please Attach File to Upload.";
                return;
            }
        }
    }
    protected void gridstsFileAttach_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete1")
        {
            gridstsFileAttach.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            if (Session["GridFileAttach12"] != null)
            {
                if (gridstsFileAttach.Rows.Count > 0)
                {
                    dt = (DataTable)Session["GridFileAttach12"];

                    dt.Rows.Remove(dt.Rows[gridstsFileAttach.SelectedIndex]);


                    gridstsFileAttach.DataSource = dt;
                    gridstsFileAttach.DataBind();
                    Session["GridFileAttach12"] = dt;
                }
            }

        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        string ext = "";
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "pdf", "PDF", "wma", "html", "css", "rar", "zip", "rpt" };
        string[] validFileTypes1 = { "MP3", "MP4", "mp3", ".mp3", "mp3", ".mp4", ".MP3", ".m4a", "m4a", ".M4A", ".wav", "wav" };
        bool isValidFile = false;
        if (rdoselct.SelectedValue == "1")
        {

            if (FileUpload2.HasFile == true)
            {

                ext = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
                for (int i = 0; i < validFileTypes1.Length; i++)
                {
                   
                    if (ext == "." + validFileTypes1[i])
                    {

                        isValidFile = true;

                        break;

                    }

                }
            }
        }
        else
        {
            if (FileUpload1.HasFile == true)
            {

                ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                for (int i = 0; i < validFileTypes.Length; i++)
                {

                    if (ext == "." + validFileTypes[i])
                    {

                        isValidFile = true;

                        break;

                    }

                }
            }
        }
       

        if (!isValidFile)
        {

            lblstsmsg.Visible = true;
            if (rdoselct.SelectedValue == "1")
            {
                lblstsmsg.Text = "Invalid File. Please upload a File with extension " +

                               string.Join(",", validFileTypes1);
            }
            else
            {
                lblstsmsg.Text = "Invalid File. Please upload a File with extension " +

                              string.Join(",", validFileTypes);
            }

        }

        else
        {

            String filename = "";


            string docfile = "";
            string audiofile = "";
            //PnlFileAttachLbl.Visible = true;
            if (FileUpload1.HasFile || FileUpload2.HasFile)
            {
                if (FileUpload1.HasFile)
                {
                    filename = FileUpload1.FileName;
                    docfile = FileUpload1.FileName;
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Clientadmin\\Attach\\") + filename);
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Clientadmin/Uploads/") + filename);
                    string file = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    Stream str = FileUpload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(str);
                    Byte[] size = br.ReadBytes((int)str.Length);

                }
                if (FileUpload2.HasFile)
                {
                    audiofile = FileUpload2.FileName;
                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~\\Clientadmin\\Attach\\") + audiofile);
                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~/Clientadmin/Uploads/") + audiofile);
                }
                //hdnFileName.Value = filename;
                DataTable dtsts = new DataTable();
                if (Session["GridFileAttach12"] == null)
                {
                    DataColumn dtcom21 = new DataColumn();
                    dtcom21.DataType = System.Type.GetType("System.String");
                    dtcom21.ColumnName = "PDFURL";
                    dtcom21.ReadOnly = false;
                    dtcom21.Unique = false;
                    dtcom21.AllowDBNull = true;
                    dtsts.Columns.Add(dtcom21);

                    DataColumn dtcom31 = new DataColumn();
                    dtcom31.DataType = System.Type.GetType("System.String");
                    dtcom31.ColumnName = "Title";
                    dtcom31.ReadOnly = false;
                    dtcom31.Unique = false;
                    dtcom31.AllowDBNull = true;
                    dtsts.Columns.Add(dtcom31);

                    DataColumn dtcom41 = new DataColumn();

                    dtcom41.DataType = System.Type.GetType("System.String");
                    dtcom41.ColumnName = "AudioURL";
                    dtcom41.ReadOnly = false;
                    dtcom41.Unique = false;
                    dtcom41.AllowDBNull = true;
                    dtsts.Columns.Add(dtcom41);

                    DataColumn dtcomm = new DataColumn();
                    dtcomm.DataType = System.Type.GetType("System.String");
                    dtcomm.ColumnName = "Doc";
                    dtcomm.ReadOnly = false;
                    dtcomm.Unique = false;
                    dtcomm.AllowDBNull = true;
                    dtsts.Columns.Add(dtcomm);
                }
                else
                {
                    dtsts = (DataTable)Session["GridFileAttach12"];
                }
                DataRow dtroww = dtsts.NewRow();
                dtroww["PDFURL"] = filename;
                dtroww["Title"] = txtststitle.Text;
                dtroww["AudioURL"] = audiofile;
                dtroww["Doc"] = docfile;

                //dtrow["FileNameChanged"] = hdnFileName.Value;
                dtsts.Rows.Add(dtroww);
                Session["GridFileAttach12"] = dtsts;
                gridstsFileAttach.DataSource = dtsts;


                gridstsFileAttach.DataBind();
                txtststitle.Text = "";
            }
            else
            {
                lblstsmsg.Visible = true;
                lblstsmsg.Text = "Please Attach File to Upload.";
                return;
            }
        }
    }
    protected void txttargetenddate_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButton2.Checked = false;
        if (txttargetenddate.SelectedValue == "1")
        {
            TextBox1.Text = txtstartdate.Text;
            TextBox1.Visible = false;
        }
        if (txttargetenddate.SelectedValue == "2")
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(1);
            TextBox1.Text = dt.ToString();
            TextBox1.Visible = false;

        }
        if (txttargetenddate.SelectedValue == "3")
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(7);
            TextBox1.Text = dt.ToString();
            TextBox1.Visible = false;
        }
        if (txttargetenddate.SelectedValue == "4")
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(30);
            TextBox1.Text = dt.ToString();
            TextBox1.Visible = false;
        }


    }
    protected void txtstartdate_TextChanged(object sender, EventArgs e)
    {
        DateTime dd = DateTime.Now;
        string formatted = dd.ToString("dd/MM/yyyy");
        string dd1 = formatted.Replace('-', '/');
        DateTime dd2 = Convert.ToDateTime(txtstartdate.Text);
        string formatted1 = dd2.ToString("dd/MM/yyyy");
        string dd3 = formatted1.Replace('-', '/');
        string dd4 = dd1 + dd3;
        if (txtstartdate.Text != dd1.ToString())
        {
            txttargetenddate.Visible = false;
            TextBox1.Visible = true;
        }
        else
        {
            txttargetenddate.Visible = true;
            TextBox1.Visible = false;
        }
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        TextBox1.Visible = true;
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
    }
    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {

            txtreportingdate.Text = System.DateTime.Now.ToShortDateString();
        }
        else
        {
            txtreportingdate.Text =System.DateTime.Now.ToShortDateString();
        }
    }
    protected void grdmonthlygoal_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        grdmonthlygoal.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void grdmonthlygoal_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            lblstsmsg.Visible = false;
            //grdmonthlygoal.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //int i = Convert.ToInt32(grdmonthlygoal.SelectedDataKey.Value);
            //grdmonthlygoal.EditIndex = -1;
            //Int64 i = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(e.CommandArgument);
            Session["proid"] = i;
            SqlDataAdapter da = new SqlDataAdapter("SELECT * From ProjectMaster inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id where ProjectMaster.ProjectMaster_Id='" + i + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Session["lbldep"] = dt.Rows[0]["ProjectMaster_DeptId"].ToString();
            Session["lblemp"] = dt.Rows[0]["ProjectMaster_Employee_Id"].ToString();

            //Label lblemp = (Label)grdmonthlygoal.Rows[grdmonthlygoal.SelectedIndex].FindControl("lblemp");
            // Session["lblemp"] =  lblemp.Text;

            // Label lbldep = (Label)grdmonthlygoal.Rows[grdmonthlygoal.SelectedIndex].FindControl("lbldep");
            // Session["lblemp"] = lbldep.Text;

            pnl_paydetail.Visible = true;
            pnlgrid.Visible = true;

            Label1.Text = "";

        }


        if (e.CommandName == "view111")
        {


            int mkl = Convert.ToInt32(e.CommandArgument);
            Session["viewid"] = mkl;
            string te = "ViewEmployeeProjectStatusLB.aspx?id=" + mkl;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
        if (e.CommandName == "View1")
        {
            grdmonthlygoal.EditIndex = -1;
            Session["viewid"] = e.CommandArgument;
            String js = "window.open('ViewEmployeeProjectStatusLB.aspx', '_blank');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open ViewEmployeeProjectStatus.aspx.aspx", js, true);

            // Response.Redirect("~/ProjectViewStatus.aspx");
        }
    }
    protected void grdmonthlygoal_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtsortdate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void grdmonthlygoal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblproId = (Label)e.Row.FindControl("lblproId");
            Label Label20 = (Label)e.Row.FindControl("Label20"); //date with color
            Label Label124 = (Label)e.Row.FindControl("Label124");//orginal date
            //Label Label23 = (Label)e.Row.FindControl("Label23");//overdue

            string strcln = " SELECT  * from  ProjectMaster where ProjectMaster_Id='" + lblproId.Text + "' ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            string www = dtcln.Rows[0]["ProjectMaster_TargetEndDate"].ToString();
            //string targetdate = Label124.Text;
            //Date ww = Convert.ToDateTime(targetdate.ToString());
            DateTime vv = Convert.ToDateTime(www.ToString());
            var hh1 = DateTime.Now;


            if (vv.Date < hh1.Date)
            {
                Label20.Visible = true;
                // Label23.Visible = true;

                Label124.Visible = false;
            }
            else
            {
                Label20.Visible = false;
                Label124.Visible = true;
                //Label23.Visible = false;


            }

        }
    
    }
}