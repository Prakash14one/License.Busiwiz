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
        PageConn pgcon = new PageConn();
        conioffce = pgcon.dynconn;
       // Session["ClientId"] = "35";
       
        if (!IsPostBack)
        {
            ChkActive.Visible = false;
            Session["GridFileAttach1"] = null;

            lblVersion.Visible = true;
            lblVersion.Text = "This is Version  6 Updated on 12/12/2015 by";
            txtreportingdate.Text = System.DateTime.Now.ToShortDateString();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ChkActive.Checked = true;
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ViewState["sortOrder"] = "";
            
            //ViewState["empname"] = ddls .SelectedValue.ToString();
            DDLwarehous();
            fillDepartment();
            fillemployee();

            ViewState["empname"] = ddlemployee.SelectedValue.ToString();

            DDLwarehousFilter();
            fillddldepartmentFilter();
            EmployeeFilter();
            FillGrid();
            
            
            
        }
    }

    public void DDLwarehous()
    {
        string finalstr = " SELECT DISTINCT  dbo.WareHouseMaster.Name, dbo.WareHouseMaster.WareHouseId, dbo.WareHouseMaster.comid FROM dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid WHERE (dbo.WareHouseMaster.Status = 1) AND (dbo.DepartmentmasterMNC.Active = 1) AND dbo.WareHouseMaster.comid='" + Session["comid"] + "' ORDER BY dbo.WareHouseMaster.Name ";
        SqlCommand cmdcln = new SqlCommand(finalstr, conioffce);
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
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddldepartment();
        fillemployee(); //SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.Whid FROM dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id
    }
    public void fillddldepartment()
    {
        string strfillgrid1 = "SELECT Departmentname as Departmentname,ID from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where Companyid='" + Session["comid"] + "' and DepartmentmasterMNC.Whid='" + ddlstore.SelectedValue + "' and  [WareHouseMaster].status = '1' order by Departmentname";
        SqlCommand cmdfillgrid1 = new SqlCommand(strfillgrid1, conioffce);
        SqlDataAdapter adpfillgrid1 = new SqlDataAdapter(cmdfillgrid1);
        DataTable dtfill1 = new DataTable();
        adpfillgrid1.Fill(dtfill1);
        ddldesignation.DataSource = dtfill1;
        ddldesignation.DataValueField = "ID";
        ddldesignation.DataTextField = "Departmentname";
        ddldesignation.DataBind();
        ddldesignation.Items.Insert(0, "-Select-");
        ddldesignation.SelectedItem.Value = "0";
    }
    protected void ddldesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployee(); 
    }
    protected void ddlddlemployee_SelectedIndexChangedEmp(object sender, EventArgs e)
    {
        if (ddlemployee.SelectedIndex > 0)
        {
        
        string strcln = " SELECT * from EmployeeMaster where id='" + ddlemployee.SelectedValue + "' ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            string ss = dtcln.Rows[0]["DesignationId"].ToString();
             ddlDeptName.SelectedValue=ss;
        }
        }        
        FillType();
        try
        {
            ddl_empfortype.SelectedValue = ddlemployee.SelectedValue;
            FillGridtype();
        }
        catch (Exception ex)
        {
        }
        
    }
    protected void fillemployee()
    {
        //string empid = "";
        //string strcon = "";
        //string depart = "";
        //string strdepa = "";
        //string allemp = "";
        //string strall = "";
        //if (ddldesignation.SelectedIndex > 0)
        //{

        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.DepartmentmasterMNC.id FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   dbo.DepartmentmasterMNC.id='" + ddldesignation.SelectedValue + "' and EmployeeMaster.Active=1 ";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            depart += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (depart.Length > 0)
        //        {
        //            depart = depart.Remove(depart.Length - 1);
        //            strdepa = " And Id IN (" + depart + ") ";
        //        }
        //    }
        //}
        //if (ddlstore.SelectedIndex > 0)
        //{

        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.Whid  FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where  dbo.EmployeeMaster.Whid='" + ddlstore.SelectedValue + "' and EmployeeMaster.Active=1 ";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            empid += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (empid.Length > 0)
        //        {
        //            empid = empid.Remove(empid.Length - 1);
        //            strcon = " And Id IN (" + empid + ") ";
        //        }
        //    }
        //}
        //else
        //{
        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.DepartmentmasterMNC.id FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 ";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            allemp += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (allemp.Length > 0)
        //        {
        //            allemp = allemp.Remove(allemp.Length - 1);
        //            strall = " And Id IN (" + allemp + ") ";
        //        }
        //    }
        //}
        string strwhid = "";
        if (ddlstore.SelectedIndex > 0)
        {
            strwhid += " and dbo.EmployeeMaster.Whid='" + ddlstore.SelectedValue + "' ";
        }
        if (ddldesignation.SelectedIndex > 0)
        {
            strwhid += " and dbo.EmployeeMaster.DeptID='" + ddldesignation.SelectedValue + "' ";
        }
           
        //SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.DeptID, dbo.EmployeeMaster.Whid FROM dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id WHERE (dbo.EmployeeMaster.Active = 1)
        //string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "'" + strall + " " + strcon + "" + strdepa + " order by Name  ";
        string strcln = " SELECT DISTINCT dbo.EmployeeMaster.EmployeeMasterID, dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid ,  dbo.EmployeeMaster.DeptID FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 " + strwhid + " ";
        SqlCommand cmdcln = new SqlCommand(strcln, conioffce);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlemployee.DataSource = dtcln;
        ddlemployee.DataValueField = "License_Emp_id";
        ddlemployee.DataTextField = "EmployeeName";
        ddlemployee.DataBind();
        ddlemployee.Items.Insert(0, "---Select All---");
       
        ddl_empfortype.DataSource = dtcln;
        ddl_empfortype.DataValueField = "License_Emp_id";
        ddl_empfortype.DataTextField = "EmployeeName";
        ddl_empfortype.DataBind();
        ddl_empfortype.Items.Insert(0, "---Select All---");
    }

    //---Filter-----------------------

    public void DDLwarehousFilter()
    {
        string finalstr = " SELECT DISTINCT  dbo.WareHouseMaster.Name, dbo.WareHouseMaster.WareHouseId, dbo.WareHouseMaster.comid FROM dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid WHERE (dbo.WareHouseMaster.Status = 1) AND (dbo.DepartmentmasterMNC.Active = 1) AND dbo.WareHouseMaster.comid='" + Session["comid"] + "' ORDER BY dbo.WareHouseMaster.Name ";
        SqlCommand cmdcln = new SqlCommand(finalstr, conioffce);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlbusinessfilter.DataSource = dtcln;
        ddlbusinessfilter.DataValueField = "WareHouseId";
        ddlbusinessfilter.DataTextField = "Name";
        ddlbusinessfilter.DataBind();
        ddlbusinessfilter.Items.Insert(0, "--Select--");
        ddlbusinessfilter.Items[0].Value = "0";
    }
    protected void ddlbusinessfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddldepartmentFilter();
        EmployeeFilter(); 
    }
    public void fillddldepartmentFilter()
    {
        string strfillgrid1 = "SELECT Departmentname as Departmentname,ID from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where Companyid='" + Session["comid"] + "' and DepartmentmasterMNC.Whid='" + ddlbusinessfilter.SelectedValue + "' and  [WareHouseMaster].status = '1' order by Departmentname";
        SqlCommand cmdfillgrid1 = new SqlCommand(strfillgrid1, conioffce);
        SqlDataAdapter adpfillgrid1 = new SqlDataAdapter(cmdfillgrid1);
        DataTable dtfill1 = new DataTable();
        adpfillgrid1.Fill(dtfill1);
        ddldepartmentfilter.DataSource = dtfill1;
        ddldepartmentfilter.DataValueField = "ID";
        ddldepartmentfilter.DataTextField = "Departmentname";
        ddldepartmentfilter.DataBind();
        ddldepartmentfilter.Items.Insert(0, "-Select-");
        ddldepartmentfilter.SelectedItem.Value = "0";
    }
    protected void ddldepartmentfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        EmployeeFilter();
    }
    public void EmployeeFilter()
    {
        //string empid = "";
        //string strcon = "";
        //string depart = "";
        //string strdepa = "";
        //string allemp = "";
        //string strall = "";
        //if (ddldepartmentfilter.SelectedIndex > 0)
        //{
        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.DepartmentmasterMNC.id FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   dbo.DepartmentmasterMNC.id='" + ddldepartmentfilter.SelectedValue + "'";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            depart += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (depart.Length > 0)
        //        {
        //            depart = depart.Remove(depart.Length - 1);
        //            strdepa = " And Id IN (" + depart + ") ";
        //        }
        //    }
        //}
        //if (ddlbusinessfilter.SelectedIndex > 0)
        //{

        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.Whid FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where  dbo.EmployeeMaster.Whid='" + ddlbusinessfilter.SelectedValue + "'";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            empid += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (empid.Length > 0)
        //        {
        //            empid = empid.Remove(empid.Length - 1);
        //            strcon = " And Id IN (" + empid + ") ";
        //        }
        //    }
        //}
        //else
        //{
        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.DepartmentmasterMNC.id FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 ";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            allemp += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (allemp.Length > 0)
        //        {
        //            allemp = allemp.Remove(allemp.Length - 1);
        //            strall = " And Id IN (" + allemp + ") ";
        //        }
        //    }
        //}
        string strwhid = "";
        if (ddlbusinessfilter.SelectedIndex > 0)
        {
            strwhid += " and dbo.EmployeeMaster.Whid='" + ddlbusinessfilter.SelectedValue + "' ";
        }
        if (ddldepartmentfilter.SelectedIndex > 0)
        {
            strwhid += " and dbo.EmployeeMaster.DeptID='" + ddldepartmentfilter.SelectedValue + "' ";
        }
        string strcln = " SELECT DISTINCT dbo.EmployeeMaster.EmployeeMasterID, dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid ,  dbo.EmployeeMaster.DeptID FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 " + strwhid + " ";
        SqlCommand cmd = new SqlCommand(strcln, conioffce);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlstatus.DataSource = ds;
        ddlstatus.DataTextField = "EmployeeName";
        ddlstatus.DataValueField = "License_Emp_id";
        ddlstatus.DataBind();
        ddlstatus.Items.Insert(0, "---Select All---");
    }
    //------------
    //---Popup Employee Project Type Add
    protected void FillGridtype()
    {
        if (ddl_empfortype.SelectedIndex > 0)
        {
            string strcln = " Select * From ProjectType where EmpId='" + ddl_empfortype.SelectedValue + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                GridView1.DataSource = dtcln;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
    }
    protected void ddl_empfortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridtype();
    }
    protected void btngo_Clicktype1(object sender, EventArgs e)
    {
        pnl_addtype.Visible = false;
    }
    protected void btngo_Clicktype(object sender, EventArgs e)
    {
        if (Button6.Text == "Add")
        {
            string strcln = " Select * From ProjectType where Type_Name='" + txtaddtype.Text + "' and EmpId='" + ddl_empfortype.SelectedValue + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count == 0)
            {
                SqlCommand cmdsq = new SqlCommand("Insert into ProjectType(EmpId,Type_Name,Active)Values('" + ddl_empfortype.SelectedValue + "', '" + txtaddtype.Text + "' ,'True')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdsq.ExecuteNonQuery();
                con.Close();
                txtaddtype.Text = "";
                lblmsg.Text = "Record Inserted successfully";
                pnl_addtype.Visible = false;
                FillType();
                txtaddtype.Text = "";
            }
            else
            {
                lblmsg.Text = "Record are already exist ";
            }          
        }
        if (Button6.Text == "Update")
        {
            string str = "Update ProjectType Set EmpId='" + ddl_empfortype.SelectedValue + "',Type_Name='" + txtaddtype.Text + "' where ProjectTypeID='" + ViewState["Id"] + "'";
            SqlCommand cmd112 = new SqlCommand(str, con);
            con.Open();
            cmd112.ExecuteNonQuery();
            con.Close();
            Button6.Text = "Add";
            FillGridtype();
            FillType();
            txtaddtype.Text = "";
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGridtype();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "vi")
        {
            addnewpanel.Visible = false;

            lblmsg.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = GridView1.SelectedDataKey.Value;
            Button6.Text = "Update";
            string str12 = "select * from ProjectType where ProjectTypeID='" + ViewState["Id"] + "'";
            SqlCommand cmd1 = new SqlCommand(str12, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            adp1.Fill(ds1);
            if (ds1.Rows.Count > 0)
            {
                txtaddtype.Text = ds1.Rows[0]["Type_Name"].ToString();
                ddl_empfortype.SelectedValue = ds1.Rows[0]["EmpId"].ToString();
            }
        }
        if (e.CommandName == "Delete")
        {
            ViewState["DID"] = Convert.ToInt32(e.CommandArgument);
            con.Open();
            string st2 = "Delete from ProjectType where ProjectTypeID=" + ViewState["DID"];
            SqlCommand cmd2 = new SqlCommand(st2, con);
            cmd2.ExecuteNonQuery();
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from ProjectType where ProjectTypeID='" + ViewState["DID"] + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        con.Close();
        con.Open();
        cmd2.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        FillGridtype();
        lblmsg.Visible = true;
        lblmsg.Text = "Record deleted succesfully";
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGridtype();
    }
    //---------------
   
   
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
 
    protected void imgrefreshdepart_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void imgadddepart_Click(object sender, ImageClickEventArgs e)
    {
        pnl_addtype.Visible = true; 
    }
    protected void FillGrid()
    {
        grdmonthlygoal.DataSource = null;
        grdmonthlygoal.DataBind();

        //  string strcln = "SELECT * From ProjectMaster  inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id  order by ProjectMaster_TargetEndDate DESC";
        //string strcln = "SELECT ProjectMaster.ProjectMaster_Id,ProjectMaster.ProjectMaster_DeptId,ProjectMaster.ProjectMaster_ProjectTitle ,ProjectMaster.ProjectMaster_ProjectDescription ,ProjectMaster.ProjectMaster_Employee_Id, CAST(ProjectMaster.ProjectMaster_StartDate AS DATE) AS ProjectMaster_StartDate ,ProjectMaster.ProjectMaster_EndDate,ProjectMaster.ProjectMaster_TargetEndDate,ProjectMaster.ProjectMaster_ProjectStatus,ProjectMaster.ProjectMaster_ProjectStatus_Id ,ProjectMaster.ProjectMaster_Active,ProjectMaster.insentivevalue ,DesignationMaster.Name,EmployeeMaster.Name FROM ProjectMaster  " +
        string strcln = "SELECT ProjectMaster.ProjectMaster_Id,ProjectMaster.ProjectMaster_DeptId,ProjectMaster.ProjectMaster_ProjectTitle ,ProjectMaster.ProjectMaster_ProjectDescription ,ProjectMaster.ProjectMaster_Employee_Id, ProjectMaster.ProjectMaster_StartDate  AS ProjectMaster_StartDate ,ProjectMaster.ProjectMaster_EndDate,ProjectMaster.ProjectMaster_TargetEndDate,ProjectMaster.ProjectMaster_ProjectStatus,ProjectMaster.ProjectMaster_ProjectStatus_Id ,ProjectMaster.ProjectMaster_Active,ProjectMaster.insentivevalue , ProjectMaster.Priority , DesignationMaster.Name,EmployeeMaster.Name FROM ProjectMaster  " +
                        "INNER JOIN DesignationMaster ON DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId " +
                       "INNER JOIN EmployeeMaster ON EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id " +
                        "WHERE EmployeeMaster.Active=1  ";


        if (ddlsortdept.SelectedIndex >1)
        {
            strcln += " AND ProjectMaster_DeptID = " + ddlsortdept.SelectedValue;
        }
        
        if (ddl_priortyadd.SelectedIndex > 0)
        {
           // strcln += " AND ProjectMaster.Priority = " + ddl_priortyadd.SelectedItem.Text;
        }
        if (txtsortdate.Text != "")
        {
            strcln += " AND ProjectMaster.ProjectMaster_StartDate between  '" + txtsortdate.Text + "' and '" + txtsortdate.Text + "'  ";
            //and ProjectMaster.ReminderDate between '" + txt_remindersearch.Text + "' and '" + txt_remindersearch.Text + "'

        }
        if (txt_remindersearch.Text != "")
        {
            //strcln += " AND ReminderDate ='" + txt_remindersearch.Text + "'";
            strcln += " and ProjectMaster.ReminderDate between '" + txt_remindersearch.Text + "' and '" + txt_remindersearch.Text + "'";
        }
        if (ddlstatus.SelectedIndex >0)
        {
            strcln += " AND ProjectMaster.ProjectMaster_Employee_Id = " + ddlstatus.SelectedValue;
        }
        
        if (ddlsortdate.SelectedValue.ToString() == "---Select All---")
        {
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project Started Before this Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND ProjectMaster_StartDate <'" + txtsortdate.Text + "'";
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
                strcln += " AND ProjectMaster_TargetEndDate <'" + txtsortdate.Text + "'";
            }
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project Started After this Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND  ProjectMaster_StartDate >'" +txtsortdate.Text + "'";

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
                strcln += " AND ProjectMaster_EndDate ='" + Convert.ToDateTime(txtsortdate.Text) + "'";
            }
        }
        else if (ddlsortdate.SelectedValue.ToString() == "All Project having Target Selected Date")
        {
            if (txtsortdate.Text != null && txtsortdate.Text != "")
            {
                strcln += " AND ProjectMaster_TargetEndDate ='" + Convert.ToDateTime(txtsortdate.Text) + "'";
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
           
            grdmonthlygoal.DataSource = dtcln;
            grdmonthlygoal.DataBind();
        }
        else
        {
            grdmonthlygoal.DataSource = null;
            grdmonthlygoal.DataBind();
        }
    }

    protected void FillType()
    {
        if (ddlemployee.SelectedIndex > 0)
        {
            string strcln = " SELECT * from ProjectType where active='1' and EmpId='" + ddlemployee.SelectedValue + "' order by Type_Name  ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddl_projectype.DataSource = dtcln;
            ddl_projectype.DataValueField = "ProjectTypeID";
            ddl_projectype.DataTextField = "Type_Name";
            ddl_projectype.DataBind();
            ddl_projectype.Items.Insert(0, "---Select Type---");

        }
        else
        {
            ddlemployee.DataSource = null;
            ddl_projectype.Items.Insert(0, "---Select Type---");

        }

        

    }
  
 
    public void fillDepartment()
    {
        //  string str = "select * from DesignationMaster inner join (select Distinct WeeklyGoalMaster_DeptId as dept from WeeklyGoalMaster Where WeeklyGoalMaster_Active='1')  dept on dept = Id where Active='1' ";
        string str = "select * from DesignationMaster where Active='1' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlDeptName.DataSource = ds;
        ddlDeptName.DataTextField = "Name";
        ddlDeptName.DataValueField = "Id";
        ddlDeptName.DataBind();
        ddlDeptName.Items.Insert(0, "---Select Department---");


        ddlsortdept.DataSource = ds;
        ddlsortdept.DataTextField = "Name";
        ddlsortdept.DataValueField = "Id";
        ddlsortdept.DataBind();
        ddlsortdept.Items.Insert(0, "---Select All---");
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "select * from EmployeeMaster where active='1' and EmployeeMaster.DesignationId='" + ddlsortdept.SelectedValue + "' and EmployeeMaster.Active='1'  ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlstatus.DataSource = ds;
        ddlstatus.DataTextField = "Name";
        ddlstatus.DataValueField = "Id";
        ddlstatus.DataBind();
        ddlstatus.Items.Insert(0, "---Select All---");
    }
    public void completegrid()
    {
        string strcln1 = "SELECT * From ProjectMaster  inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id  order by ProjectMaster_TargetEndDate DESC";
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



    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        txtedescription.Visible = true;
        Button1.Visible = true;
        btnupdate.Visible = false;
        txtinsentive.Text = "";
        Label18.Visible = false;
        txtenddate.Visible = false;
        pnlmonthgoal.Visible = true;
        addnewpanel.Visible = false;
        ChkActive.Visible = false;
        TextBox1.Visible = false;
        Label19.Text = " File Name ";
        lblmsg.Text = "";
        gridFileAttach.DataSource = null;
        gridFileAttach.DataBind();
        Session["GridFileAttach12"] = null;
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
        //this.txttargetenddate.Text = end.ToShortDateString();

        //DateTime _date = Convert.ToDateTime(txtstartdate.Text);
        //_date = _date.AddDays(2);
        //txttargetenddate.Text =  DateTime.Now.AddDays(2);

        //DateTime dt =  DateTime.Now.ToShortDateString();
        //dt = dt.AddDays(2);
        //txttargetenddate.Text = dt.ToString();

    }
    protected void grdmonthlygoal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            // grdmonthlygoal.EditIndex = -1;
            Int64 delid = Convert.ToInt32(e.CommandArgument);

            string str = "SELECT * From DailyGoalMaster  Where DailyGoal_Project_Id='" + delid + "'";


            SqlCommand cmdcln = new SqlCommand(str, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            if (dtcln.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please First Delete GeneralWork After Project Allocation deleted";
            }
            else
            {
                con.Open();
                string st2 = "Delete from ProjectMaster where ProjectMaster_Id=" + delid;
                SqlCommand cmd2 = new SqlCommand(st2, con);
                cmd2.ExecuteNonQuery();

                string st3 = "Delete from DocumentMaster where DocumentMaster_ProjectMaster_Id='" + delid + "'";
                SqlCommand cmd3 = new SqlCommand(st3, con);
                cmd3.ExecuteNonQuery();
                con.Close();
                FillGrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully ";
            }
        }

        if (e.CommandName == "Edit")
        {
            ChkActive.Visible = false;
            grdmonthlygoal.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //grdmonthlygoal.EditIndex = -1;
            pnlmonthgoal.Visible = true;
            addnewpanel.Visible = false;
            Button1.Visible = false;
            btnupdate.Visible = true;
            Label19.Text = "Edit Project Allocation";
            lblmsg.Text = "";
            int mm1 = Convert.ToInt32(e.CommandArgument);
            ViewState["editid"] = mm1;
            SqlDataAdapter da = new SqlDataAdapter("SELECT * From ProjectMaster inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id where ProjectMaster.ProjectMaster_Id='" + mm1 + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlDeptName.SelectedValue = dt.Rows[0]["ProjectMaster_DeptId"].ToString();
            ViewState["projectid"] = dt.Rows[0]["ProjectMaster_Id"].ToString();

            string str34 = "select DocumentTitle As PDFURL,DocumentFileName As Title ,DocumentData As AudioURL,Doc from DocumentMaster inner join ProjectMaster on ProjectMaster_Id = DocumentMaster_ProjectMaster_Id where DocumentMaster.DocumentMaster_ProjectMaster_Id='" + ViewState["projectid"] + "'";

            SqlCommand cmd34 = new SqlCommand(str34, con);
            SqlDataAdapter adp34 = new SqlDataAdapter(cmd34);
            DataSet ds34 = new DataSet();
            adp34.Fill(ds34);
            gridFileAttach.DataSource = ds34;
            gridFileAttach.DataBind();
            Session["GridFileAttach1"] = ds34;
            //string str = "select * from EmployeeMaster inner join  (select Distinct WeeklyGoalMaster_EmpId as emp from WeeklyGoalMaster) emp on emp = Id  where EmployeeMaster.DesignationId='" + ddlDeptName.SelectedValue + "' and EmployeeMaster.Active='1'  ";
            //SqlCommand cmd = new SqlCommand(str, con);
            //SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //adp.Fill(ds);

            //ddlemployee.DataSource = ds;
            //ddlemployee.DataTextField = "Name";
            //ddlemployee.DataValueField = "Id";
            //ddlemployee.DataBind();
            //ddlemployee.Items.Insert(0, "---Select All---");
            string str = "select * from EmployeeMaster where EmployeeMaster.DesignationId='" + ddlDeptName.SelectedValue + "' and EmployeeMaster.Active='1'  ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            ddlemployee.DataSource = ds;
            ddlemployee.DataTextField = "Name";
            ddlemployee.DataValueField = "Id";
            ddlemployee.DataBind();
            ddlemployee.Items.Insert(0, "---Select All---");
            ddlemployee.SelectedValue = dt.Rows[0]["ProjectMaster_Employee_Id"].ToString();
            ddlDeptName_SelectedIndexChangedEmp(sender, e);
            try
            {           

                    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.DepartmentmasterMNC.id ,dbo.EmployeeMaster.Whid FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where  dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id='" + dt.Rows[0]["ProjectMaster_Employee_Id"].ToString() +"'";
                    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
                    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
                    DataTable dtclnn = new DataTable();
                    adpclnn.Fill(dtclnn);
                    if (dtclnn.Rows.Count > 0)
                    {
                        DDLwarehous();                       
                        ddlstore.SelectedValue = dt.Rows[0]["Whid"].ToString();
                        fillDepartment();
                        ddldesignation.SelectedValue = dt.Rows[0]["id"].ToString();
                    }                
            }
            catch (Exception ex)
            {
            }

            try
            {
                ddl_projectype.SelectedValue = dt.Rows[0]["TypeId"].ToString();
            }
            catch (Exception ex)
            {
            }
            
            txtproname.Text = dt.Rows[0]["ProjectMaster_ProjectTitle"].ToString();
            txtedescription.Text = dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString();
            string startdate = dt.Rows[0]["ProjectMaster_StartDate"].ToString();
            DateTime start = Convert.ToDateTime(startdate.ToString()).Date;

            txtinsentive.Text = dt.Rows[0]["insentivevalue"].ToString();
            string formatted = start.ToString("dd/MM/yyyy");
            string dd1 = formatted.Replace('-', '/');
            txtstartdate.Text = dd1;



            string enddate = dt.Rows[0]["ProjectMaster_EndDate"].ToString();
            DateTime end = Convert.ToDateTime(enddate.ToString()).Date;
            //txtenddate.Text = end.ToShortDateString();

            string targetenddate = dt.Rows[0]["ProjectMaster_TargetEndDate"].ToString();
            DateTime target = Convert.ToDateTime(targetenddate.ToString()).Date;
            TextBox1.Text = target.ToShortDateString();
            TextBox1.Visible = true;
            // txtstartdate.Text = dt.Rows[0]["ProjectMaster_StartDate"].ToString();
            //  txtenddate.Text = dt.Rows[0]["ProjectMaster_EndDate"].ToString();
            ViewState["monthid"] = dt.Rows[0]["ProjectMaster_Id"].ToString();
            //  txttargetenddate.Text = dt.Rows[0]["ProjectMaster_TargetEndDate"].ToString();
            // ddlselectstatus.SelectedValue = dt.Rows[0]["ProjectMaster_ProjectStatus"].ToString();
            // ddlselectstatus.SelectedValue = dt.Rows[0]["ProjectMaster_ProjectStatus"].ToString();
            if (dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString() != null && dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString() != "")
            {
                // ChkDesc.Checked = true;
                txtedescription.Visible = true;
                txtedescription.Text = dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString();
            }

            if (dt.Rows[0]["ProjectMaster_Active"].ToString() == "True")
            {
                ChkActive.Checked = true;
            }
            else
            {
                ChkActive.Checked = false;
            }
            FillGrid();
            //pnlup.Visible = true;
        }
        if (e.CommandName == "View")
        {
            lblstsmsg.Visible = false;

            int i = Convert.ToInt32(e.CommandArgument);
            Session["proid"] = i;
            SqlDataAdapter da = new SqlDataAdapter("SELECT * From ProjectMaster inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id where ProjectMaster.ProjectMaster_Id='" + i + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Session["lbldep"] = dt.Rows[0]["ProjectMaster_DeptId"].ToString();
            Session["lblemp"] = dt.Rows[0]["ProjectMaster_Employee_Id"].ToString();



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
            // grdmonthlygoal.EditIndex = -1;
            Session["viewid"] = e.CommandArgument;
            String js = "window.open('ViewEmployeeProjectStatusLB.aspx', '_blank');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open ViewEmployeeProjectStatusLB.aspx", js, true);

            // Response.Redirect("~/ProjectViewStatus.aspx");
        }
    }

    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void ddlDeptName_SelectedIndexChangedEmp(object sender, EventArgs e)
    {
        FillType();
        try
        {
            ddl_empfortype.SelectedValue = ddlemployee.SelectedValue;
            FillGridtype(); 
        }
        catch (Exception ex)
        {
        }
  
    }
   
    
    protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "select * from EmployeeMaster where active='1' and EmployeeMaster.DesignationId='" + ddlDeptName.SelectedValue + "' and EmployeeMaster.Active='1'  ";

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
    protected void Button1_Click(object sender, EventArgs e)
    {

        //if (txttargetenddate.SelectedValue == "0")
        //{
        //    DateTime d = DateTime.Now;
        //    d = d.AddDays(-1);

        //}
        //string getprojectname = "select ProjectMaster_Id As ID From ProjectMaster Where ProjectMaster_ProjectTitle='" + txtproname.Text + "' and ProjectMaster_StartDate='"+txtstartdate.Text +"' ";
        //SqlCommand cmdmonthname = new SqlCommand(getprojectname, con);
        //SqlDataAdapter adpmonthname = new SqlDataAdapter(cmdmonthname);
        //DataTable dtmonthname = new DataTable();
        //adpmonthname.Fill(dtmonthname);
        //if (dtmonthname.Rows.Count > 0)
        //{
        //    lblmsg.Text = "Project already allocate to same employee same date";
        //}
        //else
        //{
            string strcln = "Insert Into ProjectMaster (ProjectMaster_DeptId,ProjectMaster_ProjectTitle,ProjectMaster_ProjectDescription,ProjectMaster_Employee_Id,ProjectMaster_StartDate,ProjectMaster_EndDate,ProjectMaster_ProjectStatus,ProjectMaster_TargetEndDate,ProjectMaster_Active,insentivevalue, Priority, TypeId) Values ('" + ddlDeptName.SelectedValue + "','" + txtproname.Text + "','" + txtedescription.Text + "','" + ddlemployee.SelectedValue + "','" + txtstartdate.Text + "',Null,'Pending','" + TextBox1.Text + "','1','" + txtinsentive.Text + "','" + ddl_priortyadd.SelectedItem.Text + "' ,'" + ddl_projectype.SelectedValue + "')";//'" + Convert.ToDateTime(txtenddate .Text)+ "'

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
                    string audio = (g1.FindControl("lblaudiourl") as Label).Text;
                    string name = (g1.FindControl("lbltitle") as Label).Text;

                    string str22 = "Insert Into DocumentMaster (DocumentMaster_ProjectMaster_Id,DocumentTitle,DocumentFileName,DocumentUploadDate,Doc) Values ('" + ViewState["promaxid"] + "','" + name + "','" + audio + "','" + DateTime.Now + "','" + filenamedoc + "')";
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
            txtproname.Text = txtedescription.Text = txtstartdate.Text = TextBox1.Text = "";//= txtenddate.Text 
            //ddlDeptName.SelectedIndex = ddlstatus.SelectedIndex = ddlemployee.SelectedIndex = ddlselectstatus.SelectedIndex = -1;
            ChkActive.Checked = ChkProDesc.Checked = false;
            txtedescription.Visible = false;
            pnlmonthgoal.Visible = false;
            addnewpanel.Visible = true;
            Label19.Text = "";
            FillGrid();
            chkupload.Checked = false;
            gridFileAttach.DataSource = null;
            gridFileAttach.DataBind();
       // }

    }

    public void clear()
    {
        txtproname.Text = txtedescription.Text = txtstartdate.Text = TextBox1.Text = txtproname.Text = "";//= txtenddate.Text
        ddlDeptName.SelectedIndex = ddlemployee.SelectedIndex = -1;
        ChkActive.Checked = ChkProDesc.Checked = false;
        txtedescription.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        string ext = "";
        string[] validFileTypes = { "bmp", "gif", "png",".doc", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "pdf", "PDF",  "wma", "html", "css", "rar", "zip", "rpt" };
        string[] validFileTypes1 = { "MP3", "MP4", "mp3", ".mp3", "mp3", ".mp4", ".MP3", ".m4a", "m4a", ".M4A", ".wav", "wav" };
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
       // isValidFile = true;
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
            lblstsmsg.Text = "";
           // String filename = "";
            String docfile = "";
            string audiofile = "";
            //PnlFileAttachLbl.Visible = true;
            if (fileuploadadattachment.HasFile || fileuploadaudio.HasFile)
            {
                if (fileuploadadattachment.HasFile)
                {
                    docfile = fileuploadadattachment.FileName;
                //    filename = fileuploadadattachment.FileName;

                    fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\ClientAdmin\\Attach\\") + docfile);
                    fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~/ClientAdmin/Uploads/") + docfile);
                    string file = Path.GetFileName(fileuploadadattachment.PostedFile.FileName);
                    Stream str = fileuploadadattachment.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(str);
                    Byte[] size = br.ReadBytes((int)str.Length);

                }
                if (fileuploadaudio.HasFile)
                {
                    audiofile = fileuploadaudio.FileName;
                    docfile = "";
                    //filename = fileuploadaudio.FileName;
                    fileuploadaudio.PostedFile.SaveAs(Server.MapPath("~\\ClientAdmin\\Attach\\") + audiofile);
                    fileuploadaudio.PostedFile.SaveAs(Server.MapPath("~/ClientAdmin/Uploads/") + audiofile);
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
                dtrow["PDFURL"] = "";
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
        string chkactive = "";
        if (ChkActive.Checked)
        {
            chkactive = "True";
        }
        else
        {
            chkactive = "false";
        }
        string formatted1 = "";
        string formatted12 = "";
        
        string str = "update ProjectMaster set ProjectMaster.ProjectMaster_DeptId='" + ddlDeptName.SelectedValue.ToString() + "', ProjectMaster.ProjectMaster_Employee_Id ='" + ddlemployee.SelectedValue.ToString() + "' , " +
                     " ProjectMaster.ProjectMaster_ProjectTitle= '" + txtproname.Text + "', ProjectMaster.ProjectMaster_ProjectDescription='" + txtedescription.Text + "',ProjectMaster.ProjectMaster_StartDate='" + txtstartdate.Text + "'," +
                     "  ProjectMaster.ProjectMaster_TargetEndDate ='" + TextBox1.Text + "'," +
                     "   ProjectMaster.ProjectMaster_Active ='" + chkactive.ToString() + "', " +
                     " ProjectMaster.insentivevalue ='" + txtinsentive.Text + "' , Priority='" + ddl_priortyadd.SelectedItem.Text + "', TypeId='" + ddl_projectype.SelectedValue + "' From ProjectMaster  where ProjectMaster.ProjectMaster_Id='" + ViewState["editid"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        con.Close();
        con.Open();
        string st3 = "Delete from DocumentMaster where DocumentMaster_ProjectMaster_Id='" + ViewState["editid"] + "'";
        SqlCommand cmd3 = new SqlCommand(st3, con);
        cmd3.ExecuteNonQuery();
        con.Close();

        if (gridFileAttach.Rows.Count > 0)
        {

            foreach (GridViewRow g1 in gridFileAttach.Rows)
            {
                
                string filenamedoc = (g1.FindControl("lbldoc") as Label).Text;
                string audio = (g1.FindControl("lblaudiourl") as Label).Text;
                string name = (g1.FindControl("lbltitle") as Label).Text;
                string str22 = "Insert Into DocumentMaster (DocumentMaster_ProjectMaster_Id,DocumentTitle,DocumentFileName,DocumentUploadDate,Doc) Values ('" + ViewState["editid"] + "','" + name + "','" + audio + "','" + DateTime.Now + "','" + filenamedoc + "')";
                        //    string query = "insert into product values(" + id + ",'" + name + "'," + price + ",'" + description + "')";
                        SqlCommand cmd12 = new SqlCommand(str22, con);
                        con.Open();
                        cmd12.ExecuteNonQuery();
                        con.Close();
            }

        }

        FillGrid();
        txtproname.Text = txtedescription.Text = txtstartdate.Text = TextBox1.Text = "";//txtenddate.Text = 
        ddlDeptName.SelectedIndex = ddlemployee.SelectedIndex = -1;
        ChkActive.Checked = ChkProDesc.Checked = false;
        txtedescription.Visible = false; lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully";
        btnupdate.Visible = false;
        Button1.Visible = true;
        addnewpanel.Visible = true;
        pnlmonthgoal.Visible = false;

    }
    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }
    protected void Upradio_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnluplod.Visible = true;  
        if (Upradio.SelectedValue == "1")
        {
           // pnladio.Visible = true;
          //  pnlpdfup.Visible = false;
            fileuploadaudio.Visible = true;  
            fileuploadadattachment.Visible = false;
            Label43.Text = "Audio File"; 
        }
        else
        {
            fileuploadaudio.Visible = false;
            fileuploadadattachment.Visible = true;
            Label43.Text = "Other File"; 

          //  pnladio.Visible = false;
          //  pnlpdfup.Visible = true;
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
            //Label24.Visible = true;
            /*     Label3.Visible = true;
                 Label4.Visible = true;
                 Label7.Visible = true;*/
            //Chksortstatus.Visible = true;
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button5.Visible = true;
            if (grdmonthlygoal.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdmonthlygoal.Columns[9].Visible = false;
            }
            if (grdmonthlygoal.Columns[10].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdmonthlygoal.Columns[10].Visible = false;
            }
            if (grdmonthlygoal.Columns[11].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                grdmonthlygoal.Columns[11].Visible = false;
            }
        }
        else
        {
            Button4.Text = "Printable Version";
            Button5.Visible = false;
            if (ViewState["editHide"] != null)
            {
                grdmonthlygoal.Columns[9].Visible = true;
            }
            if (ViewState["editHide"] != null)
            {
                grdmonthlygoal.Columns[10].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                grdmonthlygoal.Columns[11].Visible = true;
            }
        }
    }
    protected void ddlselectstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CurrentMonth = String.Format("{0:MMMM}", DateTime.Now);
        string Currentyear = String.Format("{0:yyyy}", DateTime.Now);
        string monthyear = Currentyear + " - " + CurrentMonth;
        //if (ddlselectstatus.SelectedValue == "Complete")
        //{
        //    txtenddate.Visible = true;
        //    ImageButton1.Visible = true;
        //    //txtenddate.Text = monthyear;
        //}
        //else
        //{
        //    txtenddate.Visible = false;
        //}

    }
 
    protected void btngo_Click(object sender, EventArgs e)
    {
        FillGrid();
    }


    protected void Button9_Click(object sender, EventArgs e)
    {
        pnl_paydetail.Visible = false;
        txtprogress.Text = "";
        txtststitle.Text = "";
        Session["GridFileAttach12"] = null;
        CheckBox1.Checked = false;
    }

    protected void ChkPro_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            //txtcompltdate.Visible = true;
            //ImageButtoncompl.Visible = true;
        }
        else
        {
            //txtcompltdate.Visible = false;
            //ImageButtoncompl.Visible = false;
        }
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            string strcln = "Insert Into ProjectStatus (ProjectStatus_Dept_Id,ProjectStatus_Emp_Id,ProjectStatus_ProjectId,ProjectStatus_Date,ProjectStatus_Status,ProjectStatus_Status_Description,ProjectStatus_Active,ReminderDate)  Values ('" + Session["lbldep"] + "','" + Session["lblemp"] + "','" + Session["proid"] + "','" + Convert.ToDateTime(txtreportingdate.Text) + "','Completed','" + txtprogress.Text + "','1','" + Convert.ToDateTime(txtreportingdate.Text) + "')";
            SqlCommand cmd = new SqlCommand(strcln, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            try
            {
                DateTime reminderdate = Convert.ToDateTime(txt_reminder.Text);
                string strc = "Update ProjectMaster set ProjectMaster_ProjectStatus='Completed' ,ProjectMaster_EndDate='" + txtreportingdate.Text + "', ReminderDate='" + txt_reminder.Text + "' where ProjectMaster_Id='" + Session["proid"] + "'";
                SqlCommand cmd1 = new SqlCommand(strc, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
               // DateTime reminderdate = Convert.ToDateTime(txt_reminder.Text);
                string strc = "Update ProjectMaster set ProjectMaster_ProjectStatus='Completed' ,ProjectMaster_EndDate='" + txtreportingdate.Text + "' where ProjectMaster_Id='" + Session["proid"] + "'";
                SqlCommand cmd1 = new SqlCommand(strc, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }

            
           
        }
        else
        {
            try
            {
                string strcln = "Insert Into ProjectStatus (ProjectStatus_Dept_Id,ProjectStatus_Emp_Id,ProjectStatus_ProjectId,ProjectStatus_Date,ProjectStatus_Status,ProjectStatus_Status_Description,ProjectStatus_Active)  Values ('" + Session["lbldep"] + "','" + Session["lblemp"] + "','" + Session["proid"] + "','" + Convert.ToDateTime(txtreportingdate.Text) + "','Pending','" + txtprogress.Text + "','1')";
                SqlCommand cmd = new SqlCommand(strcln, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                string strcln = "Insert Into ProjectStatus (ProjectStatus_Dept_Id,ProjectStatus_Emp_Id,ProjectStatus_ProjectId,ProjectStatus_Status,ProjectStatus_Status_Description,ProjectStatus_Active)  Values ('" + Session["lbldep"] + "','" + Session["lblemp"] + "','" + Session["proid"] + "','Pending','" + txtprogress.Text + "','1')";
                SqlCommand cmd = new SqlCommand(strcln, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            
           

            try
            {
              //  DateTime reminderdate = Convert.ToDateTime(txt_reminder.Text);
                string strc = "Update ProjectMaster set ProjectMaster_ProjectStatus='Pending' ,ReminderDate='" + txt_reminder.Text + "'  where ProjectMaster_Id='" + Session["proid"] + "'";
                SqlCommand cmd1 = new SqlCommand(strc, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {  
                string strc = "Update ProjectMaster set ProjectMaster_ProjectStatus='Pending'  where ProjectMaster_Id='" + Session["proid"] + "'";
                SqlCommand cmd1 = new SqlCommand(strc, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }

        }


        if (gridstsFileAttach.Rows.Count > 0)
        {

            foreach (GridViewRow g1 in gridstsFileAttach.Rows)
            {

                string lblstsaudiourl = (g1.FindControl("lblstsaudiourl") as Label).Text;
                string filenamedoc = (g1.FindControl("lbldoc") as Label).Text;
                string filename = (g1.FindControl("lblstsaudiourl") as Label).Text;
                string name = (g1.FindControl("lblststitle") as Label).Text;
                string str22 = "Insert Into DocumentMaster (DocumentMaster_ProjectMaster_Id,DocumentTitle,DocumentFileName,DocumentUploadDate,Doc) Values ('" + Session["proid"] + "','" + name + "','" + filename + "','" + DateTime.Now + "','" + filenamedoc + "')"; 
                //    string query = "insert into product values(" + id + ",'" + name + "'," + price + ",'" + description + "')";
                SqlCommand cmd12 = new SqlCommand(str22, con);
                con.Open();
                cmd12.ExecuteNonQuery();
                con.Close();
            }

        }
        FillGrid();
        lblmsg.Visible = true;
        lblmsg.Text = "Progress Report Is Added Successfully";
        //Label1.Visible = true;
        //Label1.Text = "Record has been Inserted Successfully";
        con.Close();


        txtreportingdate.Text = "";
        txtprogress.Text = "";
        CheckBox1.Checked = false;
        txtststitle.Text = "";
        pnl_paydetail.Visible = false;



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
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "pdf", "PDF",  "wma", "html", "css", "rar", "zip", "rpt" };
        string[] validFileTypes1 = { "MP3", "MP4", ".mp3", ".mp4", "mp3", ".MP3", ".m4a", "m4a", ".M4A", ".wav", "wav" };
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

            lblstsmsg.Text = "";

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

                }
                else
                {
                    dtsts = (DataTable)Session["GridFileAttach12"];
                }
                DataRow dtroww = dtsts.NewRow();
                dtroww["PDFURL"] = filename;
                dtroww["Title"] = txtststitle.Text;
                dtroww["AudioURL"] = audiofile;

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
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "pdf", "PDF",  "wma", "html", "css", "rar", "zip", "rpt" ,"PDF", "pdf"};
        string[] validFileTypes1 = { "MP3", "MP4", ".mp3", "mp3", ".mp4", ".MP3", ".m4a", "m4a", ".M4A", ".wav", "wav" };
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
            lblstsmsg.Text = ""; 
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
                dtroww["PDFURL"] = "";
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
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        txttargetenddate.ClearSelection(); 
        TextBox1.Visible = true;
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
        if (txtstartdate.Text != dd4.ToString())
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
   

    protected void grdmonthlygoal_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {

            Int64 delid = Convert.ToInt32(e.CommandArgument);

            string str = "SELECT * From DailyGoalMaster  Where DailyGoal_Project_Id='" + delid + "'";


            SqlCommand cmdcln = new SqlCommand(str, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            if (dtcln.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please First Delete GeneralWork After Project Allocation deleted";
            }
            else
            {
                con.Open();
                string st2 = "Delete from ProjectMaster where ProjectMaster_Id=" + delid;
                SqlCommand cmd2 = new SqlCommand(st2, con);
                cmd2.ExecuteNonQuery();

                string st3 = "Delete from DocumentMaster where DocumentMaster_ProjectMaster_Id='" + delid + "'";
                SqlCommand cmd3 = new SqlCommand(st3, con);
                cmd3.ExecuteNonQuery();
                con.Close();
                FillGrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully ";
            }
        }

        if (e.CommandName == "Edit")
        {
            ChkActive.Visible = false;
            grdmonthlygoal.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            Label18.Visible = true;
            txtenddate.Visible = true;
            Session["GridFileAttach12"] = null;
            pnlmonthgoal.Visible = true;
            addnewpanel.Visible = false;
            Button1.Visible = false;
            btnupdate.Visible = true;
            Label19.Text = "Edit Project Allocation";
            lblmsg.Text = "";
            int mm1 = Convert.ToInt32(e.CommandArgument);
            ViewState["editid"] = mm1;
            SqlDataAdapter da = new SqlDataAdapter("SELECT * From ProjectMaster inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id where ProjectMaster.ProjectMaster_Id='" + mm1 + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlDeptName.SelectedValue = dt.Rows[0]["ProjectMaster_DeptId"].ToString();
            ViewState["projectid"] = dt.Rows[0]["ProjectMaster_Id"].ToString();

            string str34 = "select DocumentTitle As PDFURL,DocumentTitle As Title ,DocumentFileName As AudioURL,Doc from DocumentMaster inner join ProjectMaster on ProjectMaster_Id = DocumentMaster_ProjectMaster_Id where DocumentMaster.DocumentMaster_ProjectMaster_Id='" + ViewState["projectid"] + "'";

            SqlCommand cmd34 = new SqlCommand(str34, con);
            SqlDataAdapter adp34 = new SqlDataAdapter(cmd34);
            DataTable ds34 = new DataTable();
            adp34.Fill(ds34);
            gridFileAttach.DataSource = ds34;
            gridFileAttach.DataBind();
            Session["GridFileAttach1"] = null;
            Session["GridFileAttach1"] = ds34;

            string str = "select * from EmployeeMaster where EmployeeMaster.DesignationId='" + ddlDeptName.SelectedValue + "' and EmployeeMaster.Active='1'  ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            ddlemployee.DataSource = ds;
            ddlemployee.DataTextField = "Name";
            ddlemployee.DataValueField = "Id";
            ddlemployee.DataBind();
            ddlemployee.Items.Insert(0, "---Select All---");
            ddlemployee.SelectedValue = dt.Rows[0]["ProjectMaster_Employee_Id"].ToString();
            ddlDeptName_SelectedIndexChangedEmp(sender, e);

            try
            {

                string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.DepartmentmasterMNC.id ,dbo.EmployeeMaster.Whid FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where  dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id='" + dt.Rows[0]["ProjectMaster_Employee_Id"].ToString() + "'";
                SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
                SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
                DataTable dtclnn = new DataTable();
                adpclnn.Fill(dtclnn);
                if (dtclnn.Rows.Count > 0)
                {
                    DDLwarehous();
                    ddlstore.SelectedValue = dtclnn.Rows[0]["Whid"].ToString();
                    fillDepartment();
                    ddldesignation.SelectedValue = dtclnn.Rows[0]["id"].ToString();
                }
            }
            catch (Exception ex)
            {
            }

            try
            {
                ddl_projectype.SelectedValue = dt.Rows[0]["TypeId"].ToString();
            }
            catch (Exception ex)
            {
            }
            

            txtproname.Text = dt.Rows[0]["ProjectMaster_ProjectTitle"].ToString();
            txtedescription.Text = dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString();
            string startdate = dt.Rows[0]["ProjectMaster_StartDate"].ToString();

            
            DateTime start = Convert.ToDateTime(startdate.ToString()).Date;

            txtinsentive.Text = dt.Rows[0]["insentivevalue"].ToString();
            string formatted = start.ToString("dd/MM/yyyy");
            string dd1 = formatted.Replace('-', '/');
            txtstartdate.Text = dd1;

            try
            {
                txtstartdate.Text = dt.Rows[0]["ProjectMaster_StartDate"].ToString();
            }
            catch (Exception ex)
            {
            }
            

            // string enddate = dt.Rows[0]["ProjectMaster_EndDate"].ToString();
            //DateTime end = Convert.ToDateTime(enddate.ToString()).Date;
            //txtenddate.Text = end.ToShortDateString();

            string targetenddate = dt.Rows[0]["ProjectMaster_TargetEndDate"].ToString();
            DateTime target = Convert.ToDateTime(targetenddate.ToString()).Date;
            TextBox1.Text = target.ToShortDateString();
            TextBox1.Visible = true;


            try
            {
                txtstartdate.Text = dt.Rows[0]["ProjectMaster_TargetEndDate"].ToString();
            }
            catch (Exception ex)
            {
            }

            ViewState["monthid"] = dt.Rows[0]["ProjectMaster_Id"].ToString();

            if (dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString() != null && dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString() != "")
            {

                txtedescription.Visible = true;
                txtedescription.Text = dt.Rows[0]["ProjectMaster_ProjectDescription"].ToString();
            }

            if (dt.Rows[0]["ProjectMaster_Active"].ToString() == "True")
            {
                ChkActive.Checked = true;
            }
            else
            {
                ChkActive.Checked = false;
            }


        }
        if (e.CommandName == "View")
        {
            lblstsmsg.Visible = false;

            int i = Convert.ToInt32(e.CommandArgument);
            Session["proid"] = i;
            SqlDataAdapter da = new SqlDataAdapter("SELECT * From ProjectMaster inner join DesignationMaster on DesignationMaster.Id = ProjectMaster.ProjectMaster_DeptId inner join EmployeeMaster on EmployeeMaster.Id = ProjectMaster.ProjectMaster_Employee_Id where ProjectMaster.ProjectMaster_Id='" + i + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Session["lbldep"] = dt.Rows[0]["ProjectMaster_DeptId"].ToString();
            Session["lblemp"] = dt.Rows[0]["ProjectMaster_Employee_Id"].ToString();



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

            Session["viewid"] = e.CommandArgument;
            String js = "window.open('ViewEmployeeProjectStatusLB.aspx', '_blank');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open ViewEmployeeProjectStatus.aspx.aspx", js, true);


        }
    }
    protected void grdmonthlygoal_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void grdmonthlygoal_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void grdmonthlygoal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {

            txtreportingdate.Text = System.DateTime.Now.ToShortDateString();
        }
        else
        {
            txtreportingdate.Text = System.DateTime.Now.ToShortDateString();
        }
    }
    protected void grdmonthlygoal_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        grdmonthlygoal.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void grdmonthlygoal_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtenddate_TextChanged(object sender, EventArgs e)
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
    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TextBox1.Text != "" && txtstartdate.Text != "")
            {
                if (Convert.ToDateTime(TextBox1.Text) < Convert.ToDateTime(txtstartdate.Text))
                {
                    lblenddateerror.Text = "Invalid Date Range";
                    TextBox1.Text = "";
                }
                else
                {
                    lblenddateerror.Text = "";
                }
            }       
        }
        catch (Exception ex)
        {
        }
        
    }
}

//else if (target == hh)
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