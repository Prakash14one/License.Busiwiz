using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;


public partial class EmpProjectStatusDetailsfinal : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["fildepid"] = ddlsortdept.SelectedValue.ToString();
            fillDepartment();
            fill_proje();
            fillemp_Project();
            Fill_ddlProjectName();
            FillGrid();
            ChkActive.Checked = true;
        }
    }
    protected void fillemp_Project()
    {
        string str = "select * from EmployeeMaster inner join  (select Distinct ProjectMaster_Employee_Id as emp from ProjectMaster Where ProjectMaster_Active='1') emp on emp = Id  where EmployeeMaster.DesignationId='" + ddlsortdept.SelectedValue + "' and EmployeeMaster.Active='1'  ";
        str = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' and ID='" + Session["Id"] + "' order by Name  ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlstatus.DataSource = ds;
        ddlstatus.DataTextField = "Name";
        ddlstatus.DataValueField = "Id";
        ddlstatus.DataBind();
        if (Convert.ToInt16(Session["id"]) > 0)
        {
            ddlstatus.SelectedValue = Convert.ToString(Session["id"]);
            ddlstatus.Enabled = false;
        }
        ddlstatus.Items.Insert(0, "---Select All---");


        ddlemployee.DataSource = ds;
        ddlemployee.DataTextField = "Name";
        ddlemployee.DataValueField = "Id";
        ddlemployee.DataBind();
        if (Convert.ToInt16(Session["id"]) > 0)
        {
            ddlemployee.SelectedValue = Convert.ToString(Session["id"]);
            ddlemployee.Enabled = false;
        }
        ddlemployee.Items.Insert(0, "---Select All---");



       
    }
    protected void fill_proje()
    {
        string str1 = "select * from ProjectMaster  Where ProjectMaster_Employee_Id='" + Session["id"] + "' and ProjectMaster_Active='1'";
        str1 = "select * from ProjectMaster  Where ProjectMaster_Employee_Id='" + Session["id"] + "' ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);

        ddlproname.DataSource = ds1;
        ddlproname.DataTextField = "ProjectMaster_ProjectTitle";
        ddlproname.DataValueField = "ProjectMaster_Id";
        ddlproname.DataBind();
        ddlproname.Items.Insert(0, "---Select All---");
        
    }
    protected void FillGrid()
    {
        string strcln = "SELECT ProjectMaster.*,DesignationMaster.*,EmployeeMaster.*,ProjectStatus.* From ProjectStatus inner join DesignationMaster on DesignationMaster.Id = ProjectStatus.ProjectStatus_Dept_Id inner join EmployeeMaster on EmployeeMaster.Id = ProjectStatus.ProjectStatus_Emp_Id inner join ProjectMaster on ProjectMaster_Id = ProjectStatus_ProjectId WHERE 1=1  And EmployeeMaster.Id = ' " + Session["id"] + "'";
        if (ddlsortdept.SelectedValue.ToString() == "---Select All---" || ddlsortdept.SelectedValue.ToString() == "---Select Department---")
        {

        }
        else
        {
            strcln += " AND ProjectStatus_Dept_Id ='" + ddlsortdept.SelectedValue.ToString() + "'";
        }
        if (ddlProjectName.SelectedValue == "0")
        { }
        else
        {
            strcln += " AND ProjectMaster.ProjectMaster_ID = " + ddlProjectName.SelectedValue;
        }

        if (txtFromDT.Text.Trim().Length > 0 && txtToDT.Text.Trim().Length > 0)
        {
            strcln += " AND ProjectStatus.ProjectStatus_Date BETWEEN '" + txtFromDT.Text.Trim() + "' AND '" + txtToDT.Text.Trim() + "'";
        }
        strcln += " order by ProjectStatus_Date DESC";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        grdmonthlygoal.DataSource = dtcln;
        grdmonthlygoal.DataBind();

    }

    public void fillDepartment()
    {
        //  string str = "select * from DesignationMaster inner join (select Distinct WeeklyGoalMaster_DeptId as dept from WeeklyGoalMaster Where WeeklyGoalMaster_Active='1')  dept on dept = Id where Active='1' ";

        string str = "select * from DesignationMaster inner join (select Distinct ProjectMaster_DeptId as dept from ProjectMaster Where ProjectMaster_Active='1')  dept on dept = Id where Active='1' ";
        str = "SELECT        DesignationMaster.Id, DesignationMaster.Name, DesignationMaster.Active, DesignationMaster.ClientId, dept.dept, EmployeeMaster.Id AS Expr1 FROM            DesignationMaster INNER JOIN            (SELECT DISTINCT ProjectMaster_DeptId AS dept         FROM            ProjectMaster             WHERE        (ProjectMaster_Active = '1')) AS dept ON dept.dept = DesignationMaster.Id INNER JOIN                EmployeeMaster ON DesignationMaster.Id = EmployeeMaster.DesignationId WHERE        (DesignationMaster.Active = '1') and EmployeeMaster.ID='" + Session["Id"] + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlDeptName.DataSource = ds;
        ddlDeptName.DataTextField = "Name";
        ddlDeptName.DataValueField = "Id";
        ddlDeptName.DataBind();
        ddlDeptName.Items.Insert(0, "---Select Department---");
        if (Convert.ToInt16(Session["id"]) > 0)
        {
            ddlDeptName.SelectedIndex=1;
            ddlDeptName.Enabled = false;
        }

        ddlsortdept.DataSource = ds;
        ddlsortdept.DataTextField = "Name";
        ddlsortdept.DataValueField = "Id";
        ddlsortdept.DataBind();
        ddlsortdept.Items.Insert(0, "---Select All---");
        if (Convert.ToInt16(Session["id"]) > 0)
        {
            ddlsortdept.SelectedIndex = 1;
            ddlsortdept.Enabled = false;
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        pnlmonthgoal.Visible = true;
        addnewpanel.Visible = false;
        Label19.Text = "Weekly Goal for Employee";
        lblmsg.Text = "";
        //  Label19.Text = "Add New Product or Version";
    }
    protected void Chkprodesc_CheckedChanged(object sender, EventArgs e)
    {

        if (Chkprodesc.Checked)
        {
            txtprodescription.Visible = true;
        }
        else
        {
            txtprodescription.Visible = false;
        }
    }
    protected void ChkStatus_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkStatus.Checked)
        {
            txtstatusDesc.Visible = true;
        }
        else
        {
            txtstatusDesc.Visible = false;
        }

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string chkactive = "";
        if (ChkActive.Checked)
        {
            chkactive = "True";
        }
        else
        {
            chkactive = "false";
        }
        //  string strcln = "Insert Into ProjectMaster (ProjectMaster_DeptId,ProjectMaster_ProjectTitle,ProjectMaster_ProjectDescription,ProjectMaster_Employee_Id,ProjectMaster_StartDate,ProjectMaster_EndDate,ProjectMaster_TargetEndDate,ProjectMaster_ProjectStatus,ProjectMaster_ProjectStatus_Description,ProjectMaster_Active) Values ('" + Convert.ToInt64(ddlDeptName.SelectedValue.ToString()) + "','" + txtproname.Text + "','" + txtedescription.Text + "','" + Convert.ToInt64(ddlemployee.SelectedValue.ToString()) + "','" + Convert.ToDateTime(txtstartdate.Text) + "','" + Convert.ToDateTime(txtenddate.Text) + "','" + Convert.ToDateTime(txttargetenddate.Text) + "','" + ddlselectstatus.SelectedValue.ToString() + "','" + txtstatusDesc.Text + "','" + chkactive.ToString() + "')";
        string strcln = "Insert Into ProjectStatus (ProjectStatus_Dept_Id,ProjectStatus_Emp_Id,ProjectStatus_ProjectId,ProjectStatus_Date,ProjectStatus_Status,ProjectStatus_Status_Description,ProjectStatus_Active) Values ('" + Convert.ToInt64(ddlDeptName.SelectedValue.ToString()) + "','" + Convert.ToInt64(ddlemployee.SelectedValue.ToString()) + "','" + Convert.ToInt64(ddlproname.SelectedValue.ToString()) + "','" + Convert.ToDateTime(txtstartdate.Text) + "','" + ddlselectstatus.SelectedValue.ToString() + "','" + txtstatusDesc.Text + "','" + chkactive.ToString() + "')";

        SqlCommand cmd = new SqlCommand(strcln, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        string getMonth = "select MAX(ProjectStatus_Id) As ID From ProjectStatus";
        SqlCommand cmdmonth = new SqlCommand(getMonth, con);
        SqlDataAdapter adpmonth = new SqlDataAdapter(cmdmonth);
        DataTable dtmonth = new DataTable();
        adpmonth.Fill(dtmonth);
        if (dtmonth.Rows.Count > 0)
        {
            ViewState["promaxid"] = dtmonth.Rows[0]["ID"].ToString();
        }

        string str = "update ProjectMaster set ProjectMaster_ProjectStatus_Id='" + ViewState["promaxid"] + "' From ProjectMaster  where ProjectMaster.ProjectMaster_Id='" + ddlproname.SelectedValue.ToString() + "'";
        SqlCommand cmd12 = new SqlCommand(str, con);
        con.Open();
        cmd12.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record has been Inserted Successfully";
        con.Close();
        FillGrid();
        txtprodescription.Text = txtstatusDesc.Text = txtstartdate.Text = string.Empty;
        ddlDeptName.SelectedIndex = ddlselectstatus.SelectedIndex = ddlemployee.SelectedIndex = ddlproname.SelectedIndex = -1;
        ChkActive.Checked = Chkprodesc.Checked = false;
        txtprodescription.Visible = txtstatusDesc.Visible = false;
        // txtedescription.Visible = false;
        pnlmonthgoal.Visible = false;
        addnewpanel.Visible = true;
        Label19.Text = "";
    }
    protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "select * from EmployeeMaster inner join  (select Distinct ProjectMaster_Employee_Id as emp from ProjectMaster Where ProjectMaster_Active='1') emp on emp = Id  where EmployeeMaster.DesignationId='" + ddlDeptName.SelectedValue + "' and EmployeeMaster.Active='1'  ";
        str = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' and ID='" + Session["Id"] + "' order by Name  ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlemployee.DataSource = ds;
        ddlemployee.DataTextField = "Name";
        ddlemployee.DataValueField = "Id";
        ddlemployee.DataBind();
        ddlemployee.Items.Insert(0, "---Select All---");

    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "select * from ProjectMaster  Where ProjectMaster_Employee_Id='" + Session["id"] + "' and ProjectMaster_Active='1'";
        str = "select * from ProjectMaster  Where ProjectMaster_Employee_Id='" + Session["id"] + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlproname.DataSource = ds;
        ddlproname.DataTextField = "ProjectMaster_ProjectTitle";
        ddlproname.DataValueField = "ProjectMaster_Id";
        ddlproname.DataBind();
        ddlproname.Items.Insert(0, "---Select All---");

    }
    protected void ddlproname_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "select * from ProjectMaster  Where ProjectMaster_Employee_Id='" + ddlemployee.SelectedValue + "' and ProjectMaster_ProjectTitle='" + ddlproname.SelectedValue + "' and ProjectMaster_Active='1'";
        SqlCommand cmdcln = new SqlCommand(str, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            if (dtcln.Rows[0]["ProjectMaster_ProjectDescription"].ToString() != "" && dtcln.Rows[0]["ProjectMaster_ProjectDescription"].ToString() != null)
            {
                txtprodescription.Text = dtcln.Rows[0]["ProjectMaster_ProjectDescription"].ToString();
            }
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        txtprodescription.Text = txtstatusDesc.Text = txtstartdate.Text = string.Empty;
        ddlDeptName.SelectedIndex = ddlselectstatus.SelectedIndex = ddlemployee.SelectedIndex = ddlproname.SelectedIndex = -1;
        ChkActive.Checked = Chkprodesc.Checked = false;
        txtprodescription.Visible = txtstatusDesc.Visible = false;
        // txtedescription.Visible = false;
        pnlmonthgoal.Visible = false;
        addnewpanel.Visible = true;
        Label19.Text = "";
    }
    protected void grdmonthlygoal_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void grdmonthlygoal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdmonthlygoal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            Int64 delid = Convert.ToInt32(e.CommandArgument);

            string st2 = "Delete from ProjectStatus where ProjectStatus_Id=" + delid;
            SqlCommand cmd2 = new SqlCommand(st2, con);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
            FillGrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully ";
        }
        if (e.CommandName == "Edit")
        {
            pnlmonthgoal.Visible = true;
            addnewpanel.Visible = false;
            BtnSubmit.Visible = false;
            Button5.Visible = true;
            Label19.Text = "Edit Project Status";
            lblmsg.Text = "";
            Int64 mm1 = Convert.ToInt64(e.CommandArgument);
            ViewState["editid"] = mm1;
            SqlDataAdapter da = new SqlDataAdapter("SELECT * From ProjectStatus inner join DesignationMaster on DesignationMaster.Id = ProjectStatus.ProjectStatus_Dept_Id inner join EmployeeMaster on EmployeeMaster.Id = ProjectStatus.ProjectStatus_Emp_Id inner join ProjectMaster on ProjectMaster.ProjectMaster_Id = ProjectStatus.ProjectStatus_ProjectId  where ProjectStatus.ProjectStatus_Id='" + mm1 + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                // fillDepartment();
                ddlDeptName.SelectedValue = dt.Rows[0]["ProjectStatus_Dept_Id"].ToString();

                string str = "select * from EmployeeMaster inner join  (select Distinct ProjectMaster_Employee_Id as emp from ProjectMaster Where ProjectMaster_Active='1') emp on emp = Id  where EmployeeMaster.DesignationId='" + ddlDeptName.SelectedValue + "' and EmployeeMaster.Active='1'  ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                ddlemployee.DataSource = ds;
                ddlemployee.DataTextField = "Name";
                ddlemployee.DataValueField = "Id";
                ddlemployee.DataBind();
                ddlemployee.Items.Insert(0, "---Select All---");
                ddlemployee.SelectedValue = dt.Rows[0]["ProjectStatus_Emp_Id"].ToString();
                string startdate = dt.Rows[0]["ProjectStatus_Date"].ToString();
                DateTime start = Convert.ToDateTime(startdate.ToString()).Date;
                txtstartdate.Text = start.ToShortDateString();


                ddlselectstatus.SelectedItem.Text = dt.Rows[0]["ProjectStatus_Status"].ToString();

                string str12 = "select * from ProjectMaster  Where ProjectMaster_Employee_Id='" + ddlemployee.SelectedValue + "' and ProjectMaster_Active='1'";
                SqlCommand cmd12 = new SqlCommand(str12, con);
                SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
                DataSet ds12 = new DataSet();
                adp12.Fill(ds12);

                ddlproname.DataSource = ds12;
                ddlproname.DataTextField = "ProjectMaster_ProjectTitle";
                ddlproname.DataValueField = "ProjectMaster_Id";
                ddlproname.DataBind();

                ddlproname.SelectedItem.Text = dt.Rows[0]["ProjectMaster_ProjectTitle"].ToString();

                if (dt.Rows[0]["ProjectStatus_Status_Description"].ToString() != null && dt.Rows[0]["ProjectStatus_Status_Description"].ToString() != "")
                {
                    ChkStatus.Checked = true;
                    txtstatusDesc.Visible = true;
                    txtstatusDesc.Text = dt.Rows[0]["ProjectStatus_Status_Description"].ToString();
                }
                ViewState["monthid"] = dt.Rows[0]["ProjectStatus_Id"].ToString();
                //   txttargetenddate.Text = dt.Rows[0]["ProjectMaster_TargetEndDate"].ToString();
                // ddlselectstatus.SelectedValue = dt.Rows[0]["ProjectMaster_ProjectStatus"].ToString();
                if (dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString() != null && dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString() != "")
                {
                    Chkprodesc.Checked = true;
                    txtprodescription.Visible = true;
                    txtprodescription.Text = dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString();
                }

                if (dt.Rows[0]["ProjectStatus_Active"].ToString() == "True")
                {
                    ChkActive.Checked = true;
                }
                else
                {
                    ChkActive.Checked = false;
                }
            }
        }
    }
    protected void grdmonthlygoal_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        string chkactive = "";
        if (ChkActive.Checked)
        {
            chkactive = "True";
        }
        else
        {
            chkactive = "false";
        }
        string str = "update ProjectStatus set ProjectStatus_Dept_Id='" + ddlDeptName.SelectedValue.ToString() + "', ProjectStatus_Emp_Id ='" + ddlemployee.SelectedValue.ToString() + "' , ProjectStatus_Status= '" + ddlselectstatus.SelectedValue + "', ProjectStatus_Status_Description='" + txtstatusDesc.Text + "', ProjectStatus_ProjectId='" + Convert.ToInt64(ddlproname.SelectedValue.ToString()) + "',ProjectStatus_Date='" + Convert.ToDateTime(txtstartdate.Text) + "',ProjectStatus_Active='" + chkactive.ToString() + "' From ProjectMaster  where ProjectStatus.ProjectStatus_Id='" + ViewState["editid"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        grdmonthlygoal.EditIndex = -1;
        FillGrid();
        txtstatusDesc.Text = txtstartdate.Text = txtprodescription.Text = string.Empty;
        ddlemployee.SelectedIndex = ddlemployee.SelectedIndex = ddlselectstatus.SelectedIndex = ddlproname.SelectedIndex = -1;
        txtstatusDesc.Visible = txtprodescription.Visible = false;
        ChkActive.Checked = Chkprodesc.Checked = ChkStatus.Checked = false;
        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully";
        Button5.Visible = false;
        BtnSubmit.Visible = true;
        addnewpanel.Visible = true;
        pnlmonthgoal.Visible = false;
    }
    protected void ddlsortmonth_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlsortmonth.SelectedValue.ToString() == "---Select All---")
        {
            FillGrid();
        }
        else if (ddlsortmonth.SelectedValue.ToString() != "---Select All---" && ddlsortmonth.SelectedValue.ToString() != "Pending")
        {
            if (ddlsortmonth.SelectedValue.ToString() == "--Select All--")
            {
                FillGrid();
            }
            else
            {
                char[] split = { '-' };
                string month = ddlsortmonth.SelectedItem.Text;
                string[] mn = month.Split(split);
                string selectstatus = mn[0].ToString();
                string selecttitle = mn[1].ToString();
                Label2.Visible = true;
                lbltitle.Visible = true;
                lbltitle.Enabled = false;
                lbltitle.Text = selecttitle.ToString();
                string strcln = "";
                if (ddlstatus.SelectedIndex > 0)
                {
                    strcln = "SELECT * From ProjectStatus inner join DesignationMaster on DesignationMaster.Id = ProjectStatus.ProjectStatus_Dept_Id inner join EmployeeMaster on EmployeeMaster.Id = ProjectStatus.ProjectStatus_Emp_Id inner join ProjectMaster on ProjectMaster.ProjectMaster_Id = ProjectStatus.ProjectStatus_ProjectId Where ProjectStatus.ProjectStatus_Emp_Id ='" + ddlstatus.SelectedValue.ToString() + "' and ProjectStatus.ProjectStatus_Status ='" + selectstatus.ToString() + "' and ProjectMaster.ProjectMaster_ProjectTitle='" + selecttitle.ToString() + "' order by ProjectStatus_Date DESC ";

                }
                else
                {
                    strcln = "SELECT * From ProjectStatus inner join DesignationMaster on DesignationMaster.Id = ProjectStatus.ProjectStatus_Dept_Id inner join EmployeeMaster on EmployeeMaster.Id = ProjectStatus.ProjectStatus_Emp_Id inner join ProjectMaster on ProjectMaster.ProjectMaster_Id = ProjectStatus.ProjectStatus_ProjectId Where ProjectStatus.ProjectStatus_Status ='" + selectstatus.ToString() + "' and ProjectMaster.ProjectMaster_ProjectTitle='" + selecttitle.ToString() + "' order by ProjectStatus_Date DESC ";
                }
                //   string str = " Select ProjectStatus.ProjectStatus_Status +'-'+ ProjectMaster.ProjectMaster_ProjectTitle  as WorkTitle,ProjectStatus.ProjectStatus_Id as id ,ProjectMaster_ProjectTitle from ProjectStatus inner join ProjectMaster on ProjectMaster.ProjectMaster_Id = ProjectStatus_ProjectId ";
                SqlCommand cmd = new SqlCommand(strcln, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtcln = new DataTable();
                adp.Fill(dtcln);
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
        }
        else
        {
            string strcln = "";
            if (ddlstatus.SelectedIndex > 0)
            {
                strcln = "SELECT * From ProjectStatus inner join DesignationMaster on DesignationMaster.Id = ProjectStatus.ProjectStatus_Dept_Id inner join EmployeeMaster on EmployeeMaster.Id = ProjectStatus.ProjectStatus_Emp_Id inner join ProjectMaster on ProjectMaster_Id = ProjectStatus_ProjectId Where ProjectStatus_Status ='" + ddlsortmonth.SelectedValue.ToString() + "' and ProjectStatus.ProjectStatus_Emp_Id ='" + ddlstatus.SelectedValue.ToString() + "' order by ProjectStatus_Date DESC ";
            }
            else
            {
                strcln = "SELECT * From ProjectStatus inner join DesignationMaster on DesignationMaster.Id = ProjectStatus.ProjectStatus_Dept_Id inner join EmployeeMaster on EmployeeMaster.Id = ProjectStatus.ProjectStatus_Emp_Id inner join ProjectMaster on ProjectMaster_Id = ProjectStatus_ProjectId Where ProjectStatus_Status ='" + ddlsortmonth.SelectedValue.ToString() + "' order by ProjectStatus_Date DESC ";
            }
            SqlCommand cmdcln = new SqlCommand(strcln, con);
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


    }
    protected void ddlsortdept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_ddlProjectName();
        if (ddlsortdept.SelectedValue.ToString() == "---Select All---")
        {
            FillGrid();
        }
        else
        {
            FillGrid();
            //string strcln = "SELECT * From ProjectStatus inner join DesignationMaster on DesignationMaster.Id = ProjectStatus.ProjectStatus_Dept_Id inner join EmployeeMaster on EmployeeMaster.Id = ProjectStatus.ProjectStatus_Emp_Id inner join ProjectMaster on ProjectMaster_Id = ProjectStatus_ProjectId Where ProjectStatus_Dept_Id ='" + ddlsortdept.SelectedValue.ToString() + "' order by ProjectStatus_Date DESC ";
            //SqlCommand cmdcln = new SqlCommand(strcln, con);
            //DataTable dtcln = new DataTable();
            //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            //adpcln.Fill(dtcln);
            //if (dtcln.Rows.Count > 0)
            //{

            //    DataView myDataView = new DataView();
            //    myDataView = dtcln.DefaultView;

            //    if (hdnsortExp.Value != string.Empty)
            //    {
            //        myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            //    }

            //}
            //grdmonthlygoal.DataSource = dtcln;
            //grdmonthlygoal.DataBind();

            string str = "select * from EmployeeMaster inner join  (select Distinct ProjectMaster_Employee_Id as emp from ProjectMaster Where ProjectMaster_Active='1') emp on emp = Id  where EmployeeMaster.DesignationId='" + ddlsortdept.SelectedValue + "' and EmployeeMaster.Active='1'  ";
            str = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' and ID='" + Session["Id"] + "' order by Name  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            ddlstatus.DataSource = ds;
            ddlstatus.DataTextField = "Name";
            ddlstatus.DataValueField = "Id";
            ddlstatus.DataBind();
            if (Convert.ToInt16(Session["id"]) > 0)
            {
                ddlstatus.SelectedValue = Convert.ToString(Session["id"]);
                ddlstatus.Enabled = false;
            }
        }

    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstatus.SelectedValue.ToString() == "---Select All---")
        {
            FillGrid();
        }
        else
        {

            string strcln = "SELECT * From ProjectStatus inner join DesignationMaster on DesignationMaster.Id = ProjectStatus.ProjectStatus_Dept_Id inner join EmployeeMaster on EmployeeMaster.Id = ProjectStatus.ProjectStatus_Emp_Id inner join ProjectMaster on ProjectMaster_Id = ProjectStatus_ProjectId Where ProjectStatus_Emp_Id ='" + ddlstatus.SelectedValue.ToString() + "' order by ProjectStatus_Date DESC ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
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

            }
            grdmonthlygoal.DataSource = dtcln;
            grdmonthlygoal.DataBind();
            //   string strcln22 = " select YearMaster.YearMaster_Id,YearMaster.YearMaster_Emp_Id,YearMaster.YearMaster_Name +'-'+MonthMaster.MonthMaster_MonthName as WorkTitle, MonthMaster.MonthMaster_Id as id  from MonthMaster inner join YearMaster on YearMaster.YearMaster_Id= MonthMaster.MonthMaster_YearMaster_Id  Where YearMaster.YearMaster_Emp_Id='" + ddlstatus.SelectedValue.ToString() + "'";
        }
    }
    protected void Chksortstatus_CheckedChanged(object sender, EventArgs e)
    {
        if (Chksortstatus.Checked == true)
        {
            string str = "";
            if (ddlstatus.SelectedIndex > 0)
            {
                str = " Select ProjectStatus.ProjectStatus_Status +'-'+ ProjectMaster.ProjectMaster_ProjectTitle  as WorkTitle,ProjectStatus.ProjectStatus_Id as id ,ProjectMaster_ProjectTitle from ProjectStatus inner join ProjectMaster on ProjectMaster.ProjectMaster_Id = ProjectStatus_ProjectId Where ProjectStatus.ProjectStatus_Emp_Id ='" + ddlstatus.SelectedValue.ToString() + "'";

            }
            else
            {
                str = " Select ProjectStatus.ProjectStatus_Status +'-'+ ProjectMaster.ProjectMaster_ProjectTitle  as WorkTitle,ProjectStatus.ProjectStatus_Id as id ,ProjectMaster_ProjectTitle from ProjectStatus inner join ProjectMaster on ProjectMaster.ProjectMaster_Id = ProjectStatus_ProjectId";
            }
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dtcln = new DataTable();
            adp.Fill(dtcln);
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();
            foreach (DataRow dtRow in dtcln.Rows)
            {
                if (hTable.Contains(dtRow["WorkTitle"]))
                    duplicateList.Add(dtRow);
                else
                    hTable.Add(dtRow["WorkTitle"], string.Empty);
            }
            foreach (DataRow dtRow in duplicateList)
                dtcln.Rows.Remove(dtRow);
            ddlsortmonth.DataSource = dtcln;
            ddlsortmonth.DataTextField = "WorkTitle";
            ddlsortmonth.DataValueField = "id";
            ddlsortmonth.DataBind();
            ddlsortmonth.Items.Insert(0, "--Select All--");



        }
        else
        {
            ddlsortmonth.Items.Clear();
            ddlsortmonth.Items.Insert(0, "---Select All---");
            ddlsortmonth.Items.Insert(1, "Pending");
            Label2.Visible = false;
            lbltitle.Visible = false;
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            Label24.Visible = true;
            //Label3.Visible = true;
            //Label4.Visible = true;
            //Label7.Visible = true;
            Chksortstatus.Visible = true;
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;
            if (grdmonthlygoal.Columns[7].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdmonthlygoal.Columns[7].Visible = false;
            }
            if (grdmonthlygoal.Columns[6].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                grdmonthlygoal.Columns[6].Visible = false;
            }
        }
        else
        {
            Button3.Text = "Printable Version";
            Button4.Visible = false;
            if (ViewState["editHide"] != null)
            {
                grdmonthlygoal.Columns[7].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                grdmonthlygoal.Columns[6].Visible = true;
            }
        }
    }
    private void Fill_ddlProjectName()
    {
        try
        {
            string strSQL = "SELECT ProjectMaster_Id, ProjectMaster_ProjectTitle FROM ProjectMaster " +
                            "WHERE  ProjectMaster_Employee_Id="+ Session["id"]  +"";
            if (ddlsortdept.SelectedValue == "---Select All---" || ddlsortdept.SelectedValue == "---Select Department---")
            {

            }
            else
            {
                strSQL += " AND ProjectMaster_DeptID = " + ddlsortdept.SelectedValue;
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtProjectName = new DataTable();
            da.Fill(dtProjectName);
            con.Close();
            ddlProjectName.DataTextField = dtProjectName.Columns["ProjectMaster_ProjectTitle"].Caption;
            ddlProjectName.DataValueField = dtProjectName.Columns["ProjectMaster_Id"].Caption;
            ddlProjectName.DataSource = dtProjectName;
            ddlProjectName.DataBind();
            ddlProjectName.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
    }
    protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
    }
    protected void txtFromDT_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDT.Text.Trim().Length > 0 && txtToDT.Text.Trim().Length > 0)
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
    }
}