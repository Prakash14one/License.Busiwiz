using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

using System.Net;
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;

using System.Net.Security;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


public partial class ProjectViewStatus : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection conioffce;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        conioffce = pgcon.dynconn;
        if (!IsPostBack)
        {
             Session["viewid"] = Request.QueryString["id"]  ;

            if (Session["viewid"] != "" && Session["viewid"] != null)
            {
                fillviewstatus();
                fillviewdocument();
                fillsupervisorstatus();

            }
        }
        lblVersion.Visible = true;
        lblVersion.Text = "This is Version 4 Updated on 07/12/2015 by ";

    }
    public void fillviewstatus()
    {
        //DataSet ds = new DataSet();
        //adp.Fill(ds);

      // string str = "SELECT * From ProjectMaster inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id inner join ProjectStatus on  ProjectStatus.ProjectStatus_ProjectId = ProjectMaster.ProjectMaster_Id where ProjectMaster.ProjectMaster_Id='" + Session["viewid"] + "' and ProjectMaster_Active='1'";

        string str = "SELECT DISTINCT dbo.ProjectMaster.ProjectMaster_Id, dbo.ProjectMaster.ProjectMaster_DeptId, dbo.ProjectMaster.ProjectMaster_ProjectTitle, dbo.ProjectMaster.ProjectMaster_ProjectDescription, dbo.ProjectMaster.ProjectMaster_Employee_Id, dbo.ProjectMaster.ProjectMaster_StartDate, dbo.ProjectMaster.ProjectMaster_EndDate, dbo.ProjectMaster.ProjectMaster_TargetEndDate, dbo.ProjectMaster.ProjectMaster_ProjectStatus, dbo.ProjectMaster.ProjectMaster_ProjectStatus_Id, dbo.ProjectMaster.ProjectMaster_Active, dbo.DesignationMaster.Id, dbo.DesignationMaster.Name, dbo.DesignationMaster.Active, dbo.DesignationMaster.ClientId, dbo.EmployeeMaster.Id AS Expr1, dbo.EmployeeMaster.Name AS Expr2,  dbo.EmployeeMaster.FTPServerURL, dbo.EmployeeMaster.FTPPort, dbo.EmployeeMaster.FTPUserId, dbo.EmployeeMaster.FTPPassword, dbo.EmployeeMaster.SupervisorId, dbo.EmployeeMaster.DesignationId, dbo.EmployeeMaster.UserId, dbo.EmployeeMaster.Password, dbo.EmployeeMaster.Active AS Expr3, dbo.EmployeeMaster.ClientId AS Expr4, dbo.EmployeeMaster.PhoneNo, dbo.EmployeeMaster.PhoneExtension, dbo.EmployeeMaster.MobileNo, dbo.EmployeeMaster.CountryId, dbo.EmployeeMaster.StateId, dbo.EmployeeMaster.City, dbo.EmployeeMaster.Email, dbo.EmployeeMaster.Zipcode, dbo.EmployeeMaster.RoleId, dbo.EmployeeMaster.EffectiveRate, dbo.ProjectType.Type_Name, dbo.ProjectMaster.ReminderDate, dbo.ProjectMaster.TypeId, dbo.ProjectMaster.Priority FROM            dbo.ProjectMaster INNER JOIN dbo.DesignationMaster ON dbo.DesignationMaster.Id = dbo.ProjectMaster.ProjectMaster_DeptId INNER JOIN  dbo.EmployeeMaster ON dbo.EmployeeMaster.Id = dbo.ProjectMaster.ProjectMaster_Employee_Id LEFT OUTER JOIN   dbo.ProjectType ON dbo.ProjectMaster.TypeId = dbo.ProjectType.ProjectTypeID where ProjectMaster.ProjectMaster_Id ='" + Session["viewid"] + "'";
        
        //    SqlDataAdapter da = new SqlDataAdapter("SELECT * From ProjectMaster inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id inner join ProjectStatus on  ProjectStatus.ProjectStatus_ProjectId = ProjectMaster.ProjectMaster_Id where ProjectMaster.ProjectMaster_Id='" + Session["viewid"] + "'", con);
        
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //  ViewState["id"] = dt.Rows[0]["ProjectStatus_ProjectId"].ToString();Expr2
            ddlDeptName.Text = dt.Rows[0]["Name"].ToString();
            ddlemployee.Text = dt.Rows[0]["Expr2"].ToString();
            lblproname.Text = dt.Rows[0]["ProjectMaster_ProjectTitle"].ToString();
            lbl_type.Text = dt.Rows[0]["Type_Name"].ToString();
            lblprodescription.Text = dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString();
            string startdate = dt.Rows[0]["ProjectMaster_StartDate"].ToString();
            string enddate = dt.Rows[0]["ProjectMaster_EndDate"].ToString();
            string ReminderDate = dt.Rows[0]["ReminderDate"].ToString();
            if (ReminderDate != "")
            {
                DateTime endrem = new DateTime();
                endrem = Convert.ToDateTime(ReminderDate.ToString()).Date;
                lbl_reminder.Text = endrem.ToShortDateString();
            }
            string targetenddate = dt.Rows[0]["ProjectMaster_TargetEndDate"].ToString();
            DateTime start = Convert.ToDateTime(startdate.ToString()).Date;
            DateTime end = new DateTime();
            if(enddate!="")
            {
             end = Convert.ToDateTime(enddate.ToString()).Date;
             txtenddate.Text = end.ToShortDateString();
            }
           
            DateTime target = Convert.ToDateTime(targetenddate.ToString()).Date;
            txtstartdate.Text = start.ToShortDateString();
            // txtstartdate.Text = dt.Rows[0]["ProjectMaster_StartDate"].ToString();
            //txtenddate.Text = end.ToShortDateString();
            txttargetenddate.Text = target.ToShortDateString();
            // ddlselectstatus.Text = dt.Rows[0]["ProjectStatus_Status"].ToString();
            // txtstatusDesc.Text = dt.Rows[0]["ProjectStatus_Status_Description"].ToString();
            txtstatus.Text = dt.Rows[0]["ProjectMaster_ProjectStatus"].ToString();

            //--------IOffice--


            string strcln = " SELECT  dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid ,  dbo.EmployeeMaster.DeptID FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1  and dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id='" + dt.Rows[0]["ProjectMaster_Employee_Id"] + "'   ";
                    SqlCommand cmdcln = new SqlCommand(strcln, conioffce);
                    DataTable dtcln = new DataTable();
                    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);
                    if (dtcln.Rows.Count > 0)
                    {
                        ddlemployee.Text = dtcln.Rows[0]["EmployeeName"].ToString();
                    }

            //-------------
            string str12 = "SELECT   distinct  ProjectStatus.ProjectStatus_ProjectId, ProjectStatus.ProjectStatus_Date, ProjectStatus.ProjectStatus_Status_Description FROM         ProjectStatus  where ProjectStatus_ProjectId ='" + Session["viewid"] + "' order by ProjectStatus_Date DESC"; //  and ProjectStatus_Active='1'         
    //string str12 =   "SELECT * From ProjectStatus  where ProjectStatus_ProjectId ='" + Session["viewid"] + "' order by ProjectStatus_Date DESC"; //  and ProjectStatus_Active='1' 
            SqlCommand cmd12 = new SqlCommand(str12, con);
            SqlDataAdapter da12 = new SqlDataAdapter(cmd12);
            DataTable dt12 = new DataTable();
            da12.Fill(dt12);
            if (dt12.Rows.Count > 0)
            {
                grdprostatus.DataSource = dt12;
                grdprostatus.DataBind();
            }
            grdprostatus.DataSource = dt12;
            grdprostatus.DataBind();
        }
        else
        {
            lblmsg.Text = "No Status added!!!";
        }

    }

    public void fillviewdocument()
    {
        string str12 = "SELECT distinct *  From DocumentMaster where DocumentMaster_ProjectMaster_Id='" + Session["viewid"] + "' ";
        SqlCommand cmd12 = new SqlCommand(str12, con);
        SqlDataAdapter da12 = new SqlDataAdapter(cmd12);
        DataTable dt12 = new DataTable();
        da12.Fill(dt12);

        GridView1.DataSource = dt12;
        GridView1.DataBind();

    }

    protected void gridFileAttach_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view111")
        {
            string delid = Convert.ToString(e.CommandArgument);
          /*
           Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + delid);
            Response.WriteFile(Server.MapPath("~/Attach" + '/' + delid));
            Response.End();*/
           ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + "Attach/"+ delid + "');", true);
          
            
        }

        if (e.CommandName == "Download")
        {
            string delidd = Convert.ToString(e.CommandArgument);
            /*
             Response.Clear();
              Response.AddHeader("content-disposition", "attachment;filename=" + delid);
              Response.WriteFile(Server.MapPath("~/Attach" + '/' + delid));
              Response.End();*/
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + "Attach/" + delidd + "');", true);
            
        }

    }


    protected void grdprostatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            string view = Convert.ToString(e.CommandArgument);
            /*
             Response.Clear();
              Response.AddHeader("content-disposition", "attachment;filename=" + delid);
              Response.WriteFile(Server.MapPath("~/Attach" + '/' + delid));
              Response.End();*/
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + "Attach/" + view + "');", true);


        }


    }
    public void fillsupervisorstatus()
    {
        string str = "SELECT  distinct ProjectStatus_ProjectId,ProjectStatus_Date,Status,Status_Description FROM SupervisorProjectStatusReportTbl   where ProjectStatus_ProjectId='" + Session["viewid"] + "' order by ProjectStatus_Date DESC";
         SqlCommand cmd12 = new SqlCommand(str, con);
            SqlDataAdapter da12 = new SqlDataAdapter(cmd12);
            DataTable dt12 = new DataTable();
            da12.Fill(dt12);
            if (dt12.Rows.Count > 0)
            {
                GridView2.DataSource = dt12;
                GridView2.DataBind();
            }

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            string view = Convert.ToString(e.CommandArgument);
            /*
             Response.Clear();
              Response.AddHeader("content-disposition", "attachment;filename=" + delid);
              Response.WriteFile(Server.MapPath("~/Attach" + '/' + delid));
              Response.End();*/
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + "Attach/" + view + "');", true);


        }

    }
}