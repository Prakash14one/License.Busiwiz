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

public partial class Employee_Work_Status : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ClientId"] = "35";

        if (!IsPostBack)
        {
         //   fillemployee();
            if (Session["userloginname"] != null)
            {
                // ddlemployee.Enabled = false;
                // ddlemployee.SelectedItem.Text = Session["userloginname"].ToString();
            }
            else
            {
                //ddlemployee.Enabled = false;
            }
          //  txttargetdatedeve.Text = System.DateTime.Now.ToShortDateString();
            //txttargetdatedeve.Enabled = false;
            ViewState["sortOrder"] = "";

            Fill_ddlMonthlyGoal();
            Fill_WeeklyGoal();
            columndisplay();
            //    filldate();
            fillemployee();
            fillDepartment();
            fillgrid();
            //filltask();
        }
    }

    //public void filldate()
    //{
    //    string strcln = "SELECT DailyGoal_Id,DailyGoal_dailywokDescription,DailyGoal_dailywoktitle from DailyGoalMaster Where DailyGoal_date='" + Convert.ToDateTime(txttargetdatedeve.Text) + "' and DailyGoal_Employee_Id='" + ddlemployee.SelectedValue.ToString() + "'";
    //    SqlCommand cmdcln = new SqlCommand(strcln, con);
    //    DataTable dtcln = new DataTable();
    //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //    adpcln.Fill(dtcln);
    //    if (dtcln.Rows.Count > 0)
    //    {

    //        txtworktitle.Text = dtcln.Rows[0]["DailyGoal_dailywoktitle"].ToString();
    //        ViewState["taskid"] = dtcln.Rows[0]["DailyGoal_Id"].ToString();
    //    }
    //    else
    //    {
    //        txtworktitle.Text = "";
    //    }
    //}
    //public void filltask()
    //{
    //    string str1 = "SELECT * From DailyGoalMaster inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id Where DailyGoal_date between '" + txtsortdate.Text + "' and '" + txttodate.Text + "' and  DailyGoal_dailyWorksStatus= '" + ddltask.SelectedValue + "'  and DailyDailyGoal_Active='1'";
    //    SqlCommand cmdcln1 = new SqlCommand(str1, con);
    //    DataTable dtcln1 = new DataTable();
    //    SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
    //    adpcln1.Fill(dtcln1);
    //    ddltask.DataSource = dtcln1;
    //    ddltask.DataTextField = "DailyGoal_dailyWorksStatus";
    //    ddltask.DataValueField = "DailyGoal_Employee_Id";
    //    ddltask.DataBind();
    //    ddltask.Items.Insert(0, "---Select All---");


    //}
    public void fillDepartment()
    {
        string str = "select * from DesignationMaster where ClientId='" + Session["ClientId"].ToString() + "' and Active='1' order by Name ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlsortdept.DataSource = ds;
        ddlsortdept.DataTextField = "Name";
        ddlsortdept.DataValueField = "Id";
        ddlsortdept.DataBind();
        ddlsortdept.Items.Insert(0, "---Select All---");

    }
    public void fillgrid()
    {
      //old20-11  string str = " SELECT * From DailyGoalMaster inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join ProjectMaster on ProjectMaster_Id = DailyGoal_Project_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id ";
        string str = " SELECT * From DailyGoalMaster " + 
                    "inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  " + 
                    "inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id " + 
                    "inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id " + 
                    "inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id " + 
                    "inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id " + 
                    "inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id WHERE 1 = 1 ";
        //if (txtsortdate.Text.Trim().Length > 0)
        //{
        //    str += " AND DailyGoal_date='" + Convert.ToDateTime(txtsortdate.Text) + "' ";
        //}
        if (txtsortdate.Text != "" && txttodate.Text != "")
        {
            str += " and DailyGoal_date between '" + txtsortdate.Text + "' and '" + txttodate.Text + "'";
        }


        if (ddlstatus.SelectedValue.ToString() == "---Select All---")
        { }
        else
        {
            str += " AND DailyGoal_Employee_Id='" + Convert.ToInt64(ddlstatus.SelectedValue.ToString()) + "'";
        }

        if (ddlmonthlygoal.SelectedValue == "0" || ddlmonthlygoal.SelectedValue == "")
        { }
        else
        {
            str += " AND YearMaster_ID = " + ddlmonthlygoal.SelectedValue;
        }
        if (ddlWeeklyGoal.SelectedValue == "0" || ddlWeeklyGoal.SelectedValue == "")
        {
        }
        else
        {
            str += "AND DailyGoal_WeeklyGoal_Id =" + ddlWeeklyGoal.SelectedValue;
        }
        if (ddltask.SelectedValue.ToString() == "---Select All---")
        {
        }
        else
        {
            str += "AND DailyGoal_dailyWorksStatus= '" + ddltask.SelectedValue + "'";
        }
        
        //string str = "Select * From EmployeeStatus inner join EmployeeMaster on EmployeeMaster.Id = EmpDaily_Emp_Id  inner join DailyGoalMaster on DailyGoal_Id = EmpDaily_DailyGoal_Id ";
        SqlCommand cmdcln = new SqlCommand(str, con);
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
            grdWeeklygoal.DataSource = dtcln;
            grdWeeklygoal.DataBind();
        }
        else
        {
            grdWeeklygoal.DataSource = null;
            grdWeeklygoal.DataBind();
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
    public void fillemployee()
    {
        string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' and EmployeeMaster.Active='1' order by  EmployeeMaster.Name";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlstatus.DataSource = dtcln;
        ddlstatus.DataValueField = "Id";
        ddlstatus.DataTextField = "Name";
        ddlstatus.DataBind();
        ddlstatus.Items.Insert(0, "---Select All---");
    
    }
    //protected void fillemployee()
    //{

    //    string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' and EmployeeMaster.Active='1'";
    //    SqlCommand cmdcln = new SqlCommand(strcln, con);
    //    DataTable dtcln = new DataTable();
    //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //    adpcln.Fill(dtcln);

    //    ddlemployee.DataSource = dtcln;
    //    ddlemployee.DataValueField = "Id";
    //    ddlemployee.DataTextField = "Name";
    //    ddlemployee.DataBind();
    //    ddlemployee.Items.Insert(0, "---Select All---");
    //    ddlstatus.DataSource = dtcln;
    //    ddlstatus.DataValueField = "Id";
    //    ddlstatus.DataTextField = "Name";
    //    ddlstatus.DataBind();
    //    ddlstatus.Items.Insert(0, "---Select All---");

    //    //if (dtcln.Rows.Count > 0)
    //    //{


    //    //    string strclnuser = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' and UserId= '" + Session["UserId"] + "'  ";
    //    //    SqlCommand cmdclnuser = new SqlCommand(strclnuser, con);
    //    //    DataTable dtclnuser = new DataTable();
    //    //    SqlDataAdapter adpclnuser = new SqlDataAdapter(cmdclnuser);
    //    //    adpclnuser.Fill(dtclnuser);
    //    //    if (dtclnuser.Rows.Count > 0)
    //    //    {
    //    //       // ddlemployee.Enabled = false;
    //    //       // ddlemployee.SelectedValue = dtclnuser.Rows[0]["Id"].ToString();
    //    //    }
    //    //    else
    //    //    {
    //    //        ddlemployee.Enabled = true;

    //    //    }

    //    //}

    //}
    //protected void addnewpanel_Click(object sender, EventArgs e)
    //{
    //    pnlmonthgoal.Visible = true;
    //    addnewpanel.Visible = false;
    //    lbllegend.Text = " Employee Daily Work  Report";
    //    lblmsg.Text = "";
    //    //  Label19.Text = "Add New Product or Version";

    //}
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    string status = "";
    //    if (RadioButtonList1.SelectedValue == "1")
    //    {
    //        status = "Complete";
    //    }
    //    else
    //    {
    //        status = "Pending";
    //    }
    //    if (txtworktitle.Text != null && txtworktitle.Text != "")
    //    {
    //        //  string strcln = "Insert Into EmployeeStatus (EmpDaily_Emp_Id,EmpDaily_DailyGoal_Id,EmpDaily_Status,EmpDaily_StatusReport,EmpDaily_Date) Values('" + ddlemployee.SelectedValue + "','" + ViewState["taskid"] + "','" + status + "' , '" + txtworkdonereport.Text + "','" + Convert.ToDateTime(txttargetdatedeve.Text) + "')";
    //        //   Insert Into DailyGoalMaster (DailyGoal_Dept_Id,DailyGoal_Employee_Id,DailyGoal_date,DailyGoal_Project_Id,DailyGoal_MonthlyGoal_Id,DailyGoal_WeeklyGoal_Id,DailyGoal_dailywoktitle,DailyGoal_dailywokDescription,DailyDailyGoal_Active) Values ('" + ddlDeptName.SelectedValue + "','" + ddlemployee.SelectedValue + "','" + Convert.ToDateTime(txtstartdate.Text) + "','" + ddlprojectname.SelectedValue + "','" + ddlmonthlygoal.SelectedValue + "','" + ddlweeklygoal.SelectedValue + "', '" + txtDailywork.Text + "','" + txtdailyworkdesc.Text + "','"+chkactive.ToString()+"')";

    //        string strcln = "Update DailyGoalMaster Set DailyGoal_dailyworksreport='" + txtworkdonereport.Text + "',DailyGoal_dailyWorksStatus='" + status + "' Where DailyGoal_Id='" + ViewState["editid"].ToString() + "'";
    //        SqlCommand cmd = new SqlCommand(strcln, con);
    //        con.Open();
    //        ViewState.Clear();
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //        lblmsg.Visible = true;
    //        txttargetdatedeve.Text = txtworktitle.Text = txtworkdonereport.Text = string.Empty;
    //        RadioButtonList1.SelectedIndex = -1;
    //        pnlmonthgoal.Visible = false;
    //        addnewpanel.Visible = true;
    //        lbllegend.Text = "";
    //        fillgrid();
    //        lblmsg.Text = "Record has been Inserted Successfully";
    //    }

    //}
    //protected void txttargetdatedeve_TextChanged(object sender, EventArgs e)
    //{

    //    string str = "SELECT DailyGoal_Id,DailyGoal_dailywokDescription,DailyGoal_dailywoktitle from DailyGoalMaster Where DailyGoal_date='" + Convert.ToDateTime(txttargetdatedeve.Text) + "' and DailyGoal_Employee_Id='" + ddlemployee.SelectedValue.ToString() + "'";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    adp.Fill(ds);

    //    ddlemployee.DataSource = ds;
    //    ddlemployee.DataTextField = "Name";
    //    ddlemployee.DataValueField = "Id";
    //    ddlemployee.DataBind();
    //    ddlemployee.Items.Insert(0, "---Select All---");

    //    //string strcln = " SELECT DailyGoal_Id,DailyGoal_dailywokDescription,DailyGoal_dailywoktitle from DailyGoalMaster Where DailyGoal_date='" + Convert.ToDateTime(txttargetdatedeve.Text) + "' and DailyGoal_Employee_Id='" + ddlemployee.SelectedValue.ToString() + "'";
    //    //SqlCommand cmdcln = new SqlCommand(strcln, con);
    //    //DataTable dtcln = new DataTable();
    //    //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //    //adpcln.Fill(dtcln);
    //    //if (dtcln.Rows.Count > 0)
    //    //{
    //    //    //   txtmonthdesc.Enabled = true;

    //    //    txtworktitle.Text = dtcln.Rows[0]["DailyGoal_dailywoktitle"].ToString();
    //    //    ViewState["taskid"] = dtcln.Rows[0]["DailyGoal_Id"].ToString();
    //    //}
    //    //else
    //    //{
    //    //    txtworktitle.Text = "";
    //    //}
    //}

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
    }

    protected void grdWeeklygoal_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void grdWeeklygoal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        {
            if (e.CommandName == "Delete")
            {
                Int64 delid = Convert.ToInt32(e.CommandArgument);
                string str = "Delete  From EmployeeStatus  Where EmpDaily_Id='" + delid + "'";
                SqlCommand cmdcln = new SqlCommand(str, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);
                fillgrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully ";

            }
            if (e.CommandName == "Edit")
            {

                //    pnlmonthgoal.Visible = true;
                //    addnewpanel.Visible = false;
                //    // Button1.Visible = false;
                //    // Button6.Visible = true;
                //    lbllegend.Text = "Edit Employee Work Status";
                //    lblmsg.Text = "";
                //    Int64 mm1 = Convert.ToInt64(e.CommandArgument);
                //    ViewState["editid"] = mm1;
                //    // SqlDataAdapter da = new SqlDataAdapter("SELECT * From EmployeeStatus inner join EmployeeMaster on EmployeeMaster.Id = EmpDaily_Emp_Id inner join DailyGoalMaster on DailyGoal_Id = EmpDaily_DailyGoal_Id  where EmpDaily_Id='" + mm1 + "'", con);
                //    SqlDataAdapter da = new SqlDataAdapter(" SELECT * From DailyGoalMaster inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join ProjectMaster on ProjectMaster_Id = DailyGoal_Project_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id Where DailyGoal_Id='" + mm1 + "' and DailyDailyGoal_Active='1'", con);

                //    DataTable dt = new DataTable();
                //    da.Fill(dt);
                //    if (dt.Rows.Count > 0)
                //    {

                //        // ddlemployee.SelectedValue = dt.Rows[0]["EmpDaily_Emp_Id"].ToString();
                //        ddlemployee.SelectedValue = dt.Rows[0]["DailyGoal_Employee_Id"].ToString();
                //        string startdate = dt.Rows[0]["DailyGoal_date"].ToString();
                //        DateTime start = Convert.ToDateTime(startdate.ToString()).Date;
                //        txttargetdatedeve.Text = start.ToShortDateString();
                //        filldate();
                //        txtworkdonereport.Text = dt.Rows[0]["DailyGoal_dailyworksreport"].ToString();
                //        txtworktitle.Text = dt.Rows[0]["DailyGoal_dailywoktitle"].ToString();
                //        string status = "";//DailyGoal_dailyWorksStatus
                //        if (dt.Rows[0]["DailyGoal_dailyWorksStatus"].ToString() == "Complete")
                //        {
                //            RadioButtonList1.SelectedValue = "1";
                //        }
                //        if (dt.Rows[0]["DailyGoal_dailyWorksStatus"].ToString() == "Pending")
                //        {
                //            RadioButtonList1.SelectedValue = "0";
                //        }
                //    }
                //}

            }

        }
    }
    protected void grdWeeklygoal_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void grdWeeklygoal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
 
    protected void txtsortdate_TextChanged(object sender, EventArgs e)
    {
        // string str = "Select * From EmployeeStatus inner join EmployeeMaster on EmployeeMaster.Id = EmpDaily_Emp_Id  inner join DailyGoalMaster on DailyGoal_Id = EmpDaily_DailyGoal_Id Where EmpDaily_Date='" + +"'";
      //20-11  string str = " SELECT * From DailyGoalMaster inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join ProjectMaster on ProjectMaster_Id = DailyGoal_Project_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id Where DailyGoal_date='" + Convert.ToDateTime(txtsortdate.Text) + "' and DailyDailyGoal_Active='1'";
       
        
        //string str = " SELECT * From DailyGoalMaster inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id Where DailyGoal_date='" + Convert.ToDateTime(txtsortdate.Text) + "' and DailyDailyGoal_Active='1'";
       
        //SqlCommand cmdcln = new SqlCommand(str, con);
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
        //    grdWeeklygoal.DataSource = dtcln;
        //    grdWeeklygoal.DataBind();
        //}
        //else
        //{
        //    grdWeeklygoal.DataSource = null;
        //    grdWeeklygoal.DataBind();
        //}
       
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        //if (ddlstatus.SelectedValue.ToString() == "---Select All---")
        //{
        //    fillgrid();
        //}

        //else
        //{
        //  //20-11  string strcln = " SELECT * From DailyGoalMaster inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join ProjectMaster on ProjectMaster_Id = DailyGoal_Project_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id Where DailyGoal_Employee_Id='" + Convert.ToInt64(ddlstatus.SelectedValue.ToString()) + "'";
        //    string strcln = " SELECT * From DailyGoalMaster inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id Where DailyGoal_Employee_Id='" + Convert.ToInt64(ddlstatus.SelectedValue.ToString()) + "'";
           
        //    SqlCommand cmdcln = new SqlCommand(strcln, con);
        //    DataTable dtcln = new DataTable();
        //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //    adpcln.Fill(dtcln);
        //    grdWeeklygoal.DataSource = dtcln;
        //    DataView myDataView = new DataView();
        //    myDataView = dtcln.DefaultView;
        //    grdWeeklygoal.DataBind();

        //}
        Fill_ddlMonthlyGoal();
        Fill_WeeklyGoal();
        fillgrid();
    }
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    pnlmonthgoal.Visible = false;
    //}
    protected void Button5_Click(object sender, EventArgs e)
    {

        if (Button5.Text == "Select Display Columns")
        {
            Button5.Text = "Hide Display Columns";
            Panel6.Visible = true;
            columndisplay();
        }
        else
        {
            Button5.Text = "Select Display Columns";
            Panel6.Visible = false;
            columndisplay();
        }
    }

    protected void columndisplay()
    {
        if (chkdept.Checked == true)
        {
            grdWeeklygoal.Columns[0].Visible = true;
        }
        else
        {
            grdWeeklygoal.Columns[0].Visible = false;
        }
        
        if (chkdate.Checked == true)
        {
            grdWeeklygoal.Columns[2].Visible = true;
        }
        else
        {
            grdWeeklygoal.Columns[2].Visible = false;
        }

        if (chkemployee.Checked == true)
        {
            grdWeeklygoal.Columns[3].Visible = true;
        }
        else
        {
            grdWeeklygoal.Columns[3].Visible = false;

        }
        //if (chkproduct.Checked == true)
        //{
        //    grdWeeklygoal.Columns[4].Visible = true;
        //}
        //else
        //{
        //    grdWeeklygoal.Columns[4].Visible = false;
        //}

        

    }
    protected void grdWeeklygoal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdWeeklygoal.PageIndex = e.NewPageIndex;
        if (txtsortdate.Text == "" && txttodate.Text == "" && ddlstatus.SelectedValue == "---Select All---") //&& ddltask.SelectedValue == "---Select All---")
        {
            fillgrid();
        }
        else 
        {
            fillpage();
        }
        
    }

    public void fillpage()
    {
        if (ddlstatus.SelectedIndex > 0)
        {
            //20-11  string strcln = " SELECT * From DailyGoalMaster inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join ProjectMaster on ProjectMaster_Id = DailyGoal_Project_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id Where DailyGoal_Employee_Id='" + Convert.ToInt64(ddlstatus.SelectedValue.ToString()) + "'";
            string strcln = " SELECT * From DailyGoalMaster innerjoin DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id Where DailyGoal_Employee_Id='" + Convert.ToInt64(ddlstatus.SelectedValue.ToString()) + "'";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            grdWeeklygoal.DataSource = dtcln;
            DataView myDataView = new DataView();
            myDataView = dtcln.DefaultView;
            grdWeeklygoal.DataBind();


        }
        
        else if (txtsortdate.Text != "" && txttodate.Text != "")
        {
            string str = " SELECT * From DailyGoalMaster inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id Where DailyGoal_date between '" + txtsortdate.Text + "' and '" + txttodate.Text + "' and DailyDailyGoal_Active='1'";

            SqlCommand cmdcln = new SqlCommand(str, con);
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
                grdWeeklygoal.DataSource = dtcln;
                grdWeeklygoal.DataBind();
            }
            else
            {
                grdWeeklygoal.DataSource = null;
                grdWeeklygoal.DataBind();
            }
        }
        //else if (ddltask.SelectedIndex > 0)
        //{
        //    string str = " SELECT * From DailyGoalMaster inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id Where DailyGoal_date between '" + txtsortdate.Text + "' and '" + txttodate.Text + "'DailyGoal_dailyWorksStatus='" + ddltask.SelectedValue + "'and DailyDailyGoal_Active='1'";

        //    SqlCommand cmdcln = new SqlCommand(str, con);
        //    DataTable dtcln = new DataTable();
        //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //    adpcln.Fill(dtcln);
        //    grdWeeklygoal.DataSource = dtcln;
        //    DataView myDataView = new DataView();
        //    myDataView = dtcln.DefaultView;
        //    grdWeeklygoal.DataBind();

        //}

        

    }


    //public void fillpage()
    //{
    //    string strcln = " SELECT * From DailyGoalMaster inner join DesignationMaster on DesignationMaster.Id = DailyGoal_Dept_Id  inner join EmployeeMaster on EmployeeMaster.Id = DailyGoal_Employee_Id inner join WeeklyGoalMaster on WeeklyGoalMaster_Id = DailyGoal_WeeklyGoal_Id inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id = DailyGoal_MonthlyGoal_Id inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id Where DailyGoal_Employee_Id='" + Convert.ToInt64(ddlstatus.SelectedValue.ToString()) + "'";

    //    SqlCommand cmdcln = new SqlCommand(strcln, con);
    //    DataTable dtcln = new DataTable();
    //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //    adpcln.Fill(dtcln);
    //    grdWeeklygoal.DataSource = dtcln;
    //    DataView myDataView = new DataView();
    //    myDataView = dtcln.DefaultView;
    //    grdWeeklygoal.DataBind();
    //}

    protected void chkdept_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            grdWeeklygoal.Columns[0].Visible = chkdept.Checked;
            fillgrid();
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
    }
    protected void chkdate_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            grdWeeklygoal.Columns[2].Visible = chkdate.Checked;
            fillgrid();
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
    }
    protected void chkemployee_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            grdWeeklygoal.Columns[3].Visible = chkemployee.Checked;
            fillgrid();
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
    }

    private void Fill_ddlMonthlyGoal()
    {
        try
        {
            if (ddlstatus.SelectedValue.ToString() != "")
            {
                string strSQL = "SELECT YearMaster.YearMaster_Id,YearMaster.YearMaster_Emp_Id, " +
                                "YearMaster.YearMaster_Name +'-'+MonthMaster.MonthMaster_MonthName +'-'+ WeeklyGoalMaster_Title as WorkTitle, " +
                                "MonthMaster.MonthMaster_Id as id  ,WeeklyGoalMaster.* " +
                                "From WeeklyGoalMaster " +
                                "inner join DesignationMaster on DesignationMaster.Id = WeeklyGoalMaster.WeeklyGoalMaster_DeptId " +
                                "inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id=WeeklyGoalMaster_MonthlyGoalMaster_Id " +
                                "inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id " +
                                "inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id  " +
                                "inner join EmployeeMaster on EmployeeMaster.Id = WeeklyGoalMaster.WeeklyGoalMaster_EmpId  " +
                                "Where WeeklyGoalMaster_EmpId=" + ddlstatus.SelectedValue +
                                "ORDER BY YearMaster_ID DESC";
            con.Open();
            DataTable dtMonthly = new DataTable();
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtMonthly);
            con.Close();
            ddlmonthlygoal.DataValueField = dtMonthly.Columns[0].Caption;
            ddlmonthlygoal.DataTextField = dtMonthly.Columns[2].Caption;
            ddlmonthlygoal.DataSource = dtMonthly;
            ddlmonthlygoal.DataBind();
            ddlmonthlygoal.Items.Insert(0, new ListItem("---Select---", "0"));
            ddlmonthlygoal.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
    }

    private void Fill_WeeklyGoal()
    {
        try
        { 
            string strSQL = "SELECT WeeklyGoalMaster.WeeklyGoalMaster_ID, " +
                        "YearMaster.YearMaster_Name +'-'+MonthMaster.MonthMaster_MonthName +'-'+ WeeklyGoalMaster_Week + '-' + WeeklyGoalMaster_Title as WeekWork " +
                        "From WeeklyGoalMaster " +
                        "inner join DesignationMaster on DesignationMaster.Id = WeeklyGoalMaster.WeeklyGoalMaster_DeptId " +
                        "inner join MonthlyGoalMaster on MonthlyGoalMaster.MonthlyGoalMaster_Id=WeeklyGoalMaster_MonthlyGoalMaster_Id " +
                        "inner join MonthMaster on MonthMaster.MonthMaster_Id = MonthlyGoalMaster.MonthlyGoalMaster_MonthMaster_Id " +
                        "inner join YearMaster on YearMaster.YearMaster_Id = MonthMaster.MonthMaster_YearMaster_Id  " +
                        "inner join EmployeeMaster on EmployeeMaster.Id = WeeklyGoalMaster.WeeklyGoalMaster_EmpId  " +
                        "Where WeeklyGoalMaster_EmpId= "+ ddlstatus.SelectedValue +" " +
                        "ORDER BY WeeklyGOalMaster_ID DESC";
            con.Open();
            DataTable dtWeekly = new DataTable();
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtWeekly);
            con.Close();
            ddlWeeklyGoal.DataValueField = dtWeekly.Columns[0].Caption;
            ddlWeeklyGoal.DataTextField = dtWeekly.Columns[1].Caption;
            ddlWeeklyGoal.DataSource = dtWeekly;
            ddlWeeklyGoal.DataBind();
            ddlWeeklyGoal.Items.Insert(0, new ListItem("---Select---", "0"));
            ddlWeeklyGoal.Enabled = true;
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
    }
    protected void ddlmonthlygoal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlmonthlygoal.SelectedValue.ToString() == "Select")
            {
                fillgrid();
            }
            else
            {
                fillgrid();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
    }
    protected void ddlWeeklyGoal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlWeeklyGoal.SelectedValue.ToString() == "Select")
            {
                fillgrid();
            }
            else
            {
                fillgrid();
            }
    
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsortdept.SelectedValue.ToString() == "Select All")
        {
            fillgrid();
        }


        else
        {
            ddlstatus.DataSource = null;
            string str = "select * from EmployeeMaster inner join DesignationMaster on DesignationMaster.Id =EmployeeMaster.DesignationId where EmployeeMaster.DesignationId='" + ddlsortdept.SelectedValue + "' and EmployeeMaster.Active='1' order by EmployeeMaster.Name ";
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
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;
        }
        else
        {
            Button3.Text = "Printable Version";
            Button4.Visible = false;
        }
    }

    protected void txttodate_TextChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddltask_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltask.SelectedValue.ToString() == "Select All")
        {
            fillgrid();
        }
        else
        {
            fillgrid();
        }
    }
}