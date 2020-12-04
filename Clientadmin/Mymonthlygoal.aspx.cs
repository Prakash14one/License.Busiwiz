using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Mymonthlygoal : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ClientId"] = "35";
            fillemployee();
            filldepartment();
            fillyear();
            if (Session["userloginname"] != null)
            {
                ddlstatus.Enabled = false;
                ddlstatus.SelectedItem.Text = Session["userloginname"].ToString();
                string strcln = " SELECT * from EmployeeMaster where UserId ='" + ddlstatus.SelectedItem.Text + "'";
                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);
                ddlstatus.SelectedItem.Text = Session["userloginname"].ToString(); ;
                // ddlsortdept.SelectedItem.Value = dtcln.Rows[0][""].ToString();
                Session["empid"] = dtcln.Rows[0]["Id"].ToString();
            }
            else
            {
                //ddlemployee.Enabled = false;
            }

            FillGrid();
            ViewState["sortOrder"] = "";

        }
    }

    public void fillyear()
    {
      //  string strcln22 = " select YearMaster.YearMaster_Id,YearMaster.YearMaster_Emp_Id,YearMaster.YearMaster_Name +'-'+MonthMaster.MonthMaster_MonthName as WorkTitle, MonthMaster.MonthMaster_Id as id  from MonthMaster inner join YearMaster on YearMaster.YearMaster_Id= MonthMaster.MonthMaster_YearMaster_Id  Where YearMaster.YearMaster_Emp_Id='" + ddlstatus.SelectedValue.ToString() + "'";
        string strcln22 = "SELECT YearMaster.YearMaster_Id,YearMaster.YearMaster_Emp_Id,YearMaster.YearMaster_Name +'-'+MonthMaster.MonthMaster_MonthName as WorkTitle, MonthMaster.MonthMaster_Id as id, MonthlyGoalMaster.* From  MonthlyGoalMaster inner join DesignationMaster on DesignationMaster.Id = MonthlyGoalMaster_DeptId inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster_YearMaster_Id  inner join EmployeeMaster on EmployeeMaster.Id = MonthlyGoalMaster.MonthlyGoalMaster_EmpId Where MonthlyGoalMaster_EmpId='" + Session["empid"] + "'";
         
        
        SqlCommand cmdcln1 = new SqlCommand(strcln22, con);
        DataTable dtcln1 = new DataTable();
        SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
        adpcln1.Fill(dtcln1);

        ddlsortyear.DataSource = dtcln1;
        ddlsortyear.DataValueField = "id";
        ddlsortyear.DataTextField = "WorkTitle";
        ddlsortyear.DataBind();
        ddlsortyear.Items.Insert(0, "---Select All---");
    }
    protected void ddlsortyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsortyear.SelectedValue.ToString() == "---Select All---")
        {
            FillGrid();
        }
        else
        {
            char[] split = { '-' };
            string month = ddlsortyear.SelectedItem.Text;
            string[] mn = month.Split(split);
            string selectyear = mn[0].ToString();
            string selectmonth = mn[1].ToString();
            string strcln = "SELECT * From  MonthlyGoalMaster inner join DesignationMaster on DesignationMaster.Id = MonthlyGoalMaster_DeptId inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster_YearMaster_Id  inner join EmployeeMaster on EmployeeMaster.Id = MonthlyGoalMaster.MonthlyGoalMaster_EmpId Where YearMaster.YearMaster_Name='" + selectyear.ToString() + "' and MonthMaster.MonthMaster_MonthName='" + selectmonth.ToString() + "' and MonthlyGoalMaster_EmpId ='" + Session["empid"] + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            grdmonthlygoal.DataSource = dtcln;
            adpcln.Fill(dtcln);
            DataView myDataView = new DataView();
            myDataView = dtcln.DefaultView;
            grdmonthlygoal.DataBind();
        }
    }
    public void filldepartment()
    {
        string str = "select * from DesignationMaster where ClientId='" + Session["ClientId"].ToString() + "' and Active='1' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlsortdept.DataSource = ds;
        ddlsortdept.DataTextField = "Name";
        ddlsortdept.DataValueField = "Id";
        ddlsortdept.DataBind();
        // ddlsortdept.Items.Insert(0, "---Select Department---");
    }
    protected void fillemployee()
    {

        string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "'  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlstatus.DataSource = dtcln;
        ddlstatus.DataValueField = "Id";
        ddlstatus.DataTextField = "Name";
        ddlstatus.DataBind();
        if (dtcln.Rows.Count > 0)
        {
            string strclnuser = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' and UserId= '" + Session["UserId"] + "'  ";
            SqlCommand cmdclnuser = new SqlCommand(strclnuser, con);
            DataTable dtclnuser = new DataTable();
            SqlDataAdapter adpclnuser = new SqlDataAdapter(cmdclnuser);
            adpclnuser.Fill(dtclnuser);
            if (dtclnuser.Rows.Count > 0)
            {
                ddlstatus.Enabled = false;
                ddlstatus.SelectedValue = dtclnuser.Rows[0]["Id"].ToString();
            }
            else
            {
                ddlstatus.Enabled = true;
            }

        }

    }
    protected void FillGrid()
    {
        grdmonthlygoal.EditIndex = -1;
        //grdmonthlygoal.DataSource = null;
        //grdmonthlygoal.DataBind();
        // string strcln = "SELECT DesignationMaster.Name,YearMaster.YearMaster_Name,MonthMaster.MonthMaster_MonthName,EmployeeMaster.Name,MonthlyGoalMaster_MonthlyGoalTitle,MonthlyGoalMaster_Status From MonthlyGoalMaster inner join DesignationMaster on DesignationMaster.Id = MonthlyGoalMaster_DeptId inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster_YearMaster_Id  inner join EmployeeMaster on EmployeeMaster.Id = MonthlyGoalMaster_EmpId ";
        //cmp string strcln = "SELECT YearMaster.YearMaster_Name,MonthMaster.MonthMaster_MonthName,DesignationMaster.Name,EmployeeMaster.Name,MonthlyGoalMaster_MonthlyGoalTitle,MonthlyGoalMaster_Status,MonthlyGoalMaster_Status_Description From MonthlyGoalMaster inner join DesignationMaster on DesignationMaster.Id = MonthlyGoalMaster_DeptId inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster_YearMaster_Id  inner join EmployeeMaster on EmployeeMaster.Id = MonthlyGoalMaster_EmpId ";
        string strcln1 = "SELECT * From MonthlyGoalMaster inner join DesignationMaster on DesignationMaster.Id = MonthlyGoalMaster_DeptId inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster_YearMaster_Id  inner join EmployeeMaster on EmployeeMaster.Id = MonthlyGoalMaster_EmpId where MonthlyGoalMaster_EmpId ='" + Session["empid"] + "' ";

        SqlCommand cmdcln = new SqlCommand(strcln1, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);


        if (dtcln.Rows.Count > 0)
        {
            ddlsortdept.SelectedItem.Text = dtcln.Rows[0]["Name"].ToString();
            Session["deptid"] = dtcln.Rows[0]["Id"].ToString();
            DataView myDataView = new DataView();
            myDataView = dtcln.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            grdmonthlygoal.DataSource = dtcln;
            grdmonthlygoal.DataBind();

        }
        // grdmonthlygoal.DataSource = dtcln;
        //DataView myDataView = new DataView();
        // myDataView = dtcln.DefaultView;
        // grdmonthlygoal.DataBind();
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlstatus.SelectedValue.ToString() == "---Select All---")
    //    {
    //        FillGrid();
    //    }
    //    //else if (ddlsortdept.SelectedValue.ToString() == "---Select All---")
    //    //{
    //    //    string strcln = "SELECT * From  MonthlyGoalMaster inner join DesignationMaster on DesignationMaster.Id = MonthlyGoalMaster_DeptId inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster_YearMaster_Id  inner join EmployeeMaster on EmployeeMaster.Id = MonthlyGoalMaster.MonthlyGoalMaster_EmpId  Where MonthlyGoalMaster_EmpId='" + Convert.ToInt64(ddlstatus.SelectedValue.ToString()) + "' ";
    //    //    SqlCommand cmdcln = new SqlCommand(strcln, con);
    //    //    DataTable dtcln = new DataTable();
    //    //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //    //    adpcln.Fill(dtcln);
    //    //    grdmonthlygoal.DataSource = dtcln;
    //    //    DataView myDataView = new DataView();
    //    //    myDataView = dtcln.DefaultView;
    //    //    grdmonthlygoal.DataBind();
    //    //}
    //    else
    //    {
    //        string strcln = "SELECT * From  MonthlyGoalMaster inner join DesignationMaster on DesignationMaster.Id = MonthlyGoalMaster_DeptId inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster_YearMaster_Id  inner join EmployeeMaster on EmployeeMaster.Id = MonthlyGoalMaster.MonthlyGoalMaster_EmpId Where MonthlyGoalMaster_EmpId='" + Convert.ToInt64(ddlstatus.SelectedValue.ToString()) + "'";
    //        SqlCommand cmdcln = new SqlCommand(strcln, con);
    //        DataTable dtcln = new DataTable();
    //        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //        adpcln.Fill(dtcln);
    //        grdmonthlygoal.DataSource = dtcln;
    //        DataView myDataView = new DataView();
    //        myDataView = dtcln.DefaultView;
    //        grdmonthlygoal.DataBind();


    //        string strcln22 = " select YearMaster.YearMaster_Id,YearMaster.YearMaster_Emp_Id,YearMaster.YearMaster_Name +'-'+MonthMaster.MonthMaster_MonthName as WorkTitle, MonthMaster.MonthMaster_Id as id  from MonthMaster inner join YearMaster on YearMaster.YearMaster_Id= MonthMaster.MonthMaster_YearMaster_Id  Where YearMaster.YearMaster_Emp_Id='" + ddlstatus.SelectedValue.ToString() + "'";
    //        string strcln25 = " select YearMaster.YearMaster_Id,YearMaster.YearMaster_Emp_Id,YearMaster.YearMaster_Name +'-'+MonthMaster.MonthMaster_MonthName as WorkTitle, MonthMaster.MonthMaster_Id as id  from YearMaster inner join MonthMaster on MonthMaster.MonthMaster_YearMaster_Id= YearMaster_Emp_Id  Where YearMaster_Emp_Id='" + ddlstatus.SelectedValue.ToString() + "'";


    //        //  string strcln1 = " SELECT * from  YearMaster Where YearMaster_Emp_Id='" + Convert.ToInt64(ddlstatus.SelectedValue.ToString()) + "'";
    //        SqlCommand cmdcln1 = new SqlCommand(strcln22, con);
    //        DataTable dtcln1 = new DataTable();
    //        SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
    //        adpcln1.Fill(dtcln1);

    //        ddlsortyear.DataSource = dtcln1;
    //        ddlsortyear.DataValueField = "id";
    //        ddlsortyear.DataTextField = "WorkTitle";
    //        ddlsortyear.DataBind();
    //        ddlsortyear.Items.Insert(0, "---Select All---");
    //    }

    //}
    protected void btngo_Click(object sender, EventArgs e)
    {
        //if (ddlDeptName.SelectedValue >= 0 && ddlstatus.SelectedIndex >= 0 && ddlsortmonth.SelectedIndex >= 0 )
        //{
        //  string strcln = " SELECT MonthlyGoalMaster_Id,Name,MonthlyGoalMaster_MonthlyGoalTitle,MonthlyGoalMaster_MonthlyGoalDescription From MonthlyGoalMaster inner join EmployeeMaster on EmployeeMaster.Id = MonthlyGoalMaster.MonthlyGoalMaster_EmpId inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id   Where MonthlyGoalMaster_EmpId='" + ddlstatus.SelectedValue.ToString() + "'and MonthlyGoalMaster_MonthMaster_Id='" + ddlsortmonth.SelectedValue.ToString() + "' ";//        
        char[] split = { '-' };
        string month = ddlsortyear.SelectedItem.Text;
        string[] mn = month.Split(split);
        string selectyear = mn[0].ToString();
        string selectmonth = mn[1].ToString();
        ddlsortdept.SelectedValue.ToString();
       // ddlstatus.SelectedValue

        string strcln = "SELECT * From MonthlyGoalMaster inner join DesignationMaster on DesignationMaster.Id = MonthlyGoalMaster_DeptId  inner join EmployeeMaster on EmployeeMaster.Id = MonthlyGoalMaster.MonthlyGoalMaster_EmpId inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id  inner join YearMaster on YearMaster.YearMaster_Id=MonthMaster.MonthMaster_YearMaster_Id Where MonthlyGoalMaster.MonthlyGoalMaster_EmpId='" + Session["empid"].ToString() + "'and MonthlyGoalMaster.MonthlyGoalMaster_Status='" + ddlsortmonth.SelectedValue.ToString() + "' and MonthlyGoalMaster.MonthlyGoalMaster_DeptId='" + Session["deptid"].ToString() + "' and YearMaster.YearMaster_Name='" + selectyear.ToString() + "' and MonthMaster.MonthMaster_MonthName='" + selectmonth.ToString() + "'";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        grdmonthlygoal.DataSource = dtcln;
        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;
        grdmonthlygoal.DataBind();
        //  }
    }
    protected void ddlsortmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsortmonth.SelectedValue.ToString() == "---Select All---")
        {
            FillGrid();
        }
        else
        {
            string status = ddlsortmonth.SelectedValue.ToString();
            string strcln = "SELECT * From MonthlyGoalMaster inner join DesignationMaster on DesignationMaster.Id = MonthlyGoalMaster_DeptId inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster_YearMaster_Id  inner join EmployeeMaster on EmployeeMaster.Id = MonthlyGoalMaster.MonthlyGoalMaster_EmpId Where MonthlyGoalMaster_Status ='" + ddlsortmonth.SelectedValue.ToString() + "' and MonthlyGoalMaster_EmpId ='" + Session["empid"] + "'  ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            grdmonthlygoal.DataSource = dtcln;
            grdmonthlygoal.DataBind();
        }

    }
   
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGrid();
    }
    protected void grdmonthlygoal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "monthgoal")
        {
            grdmonthlygoal.EditIndex = -1;
            Session["monthlygoalid"] = e.CommandArgument;
            String js = "window.open('ViewMyMonthlyGoal.aspx', '_blank');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open ViewMyMonthlyGoal.aspx", js, true);
        }
    }

   
}