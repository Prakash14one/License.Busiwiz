﻿using System;
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
//using System.ServiceProcess;
using System.Diagnostics;
using System.Windows;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Text;
using System.Net;
using System.Net.Mail;


public partial class ShoppingCart_Admin_frmSTGMasterReport : System.Web.UI.Page
{
    static int temp;
    static DataTable dt;
    DataByCompany obj = new DataByCompany();
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();

        if (!IsPostBack)
        {
            pageMailAccess();

            DropDownList1_SelectedIndexChanged1(sender, e);

            if (Request.QueryString["Wid"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Wid"]);

                string strwh = "select * from wmaster where MasterId='" + id + "'";

                SqlCommand cmdwh = new SqlCommand(strwh, con);
                SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
                DataTable dtwh = new DataTable();
                adpwh.Fill(dtwh);

                if (dtwh.Rows.Count > 0)
                {
                    if (Convert.ToString(dtwh.Rows[0]["EmployeeId"].ToString()) != "")
                    {
                        Label13.Visible = true;
                        ddlempgoal.Visible = true;

                        DropDownList1.SelectedIndex = 3;
                        singlefilterbyemployee();

                        ddlbusunesswithdept.SelectedIndex = ddlbusunesswithdept.Items.IndexOf(ddlbusunesswithdept.Items.FindByValue(dtwh.Rows[0]["EmployeeId"].ToString()));


                        if (Convert.ToString(dtwh.Rows[0]["MMasterId"]) != "")
                        {
                            ddlempgoal.SelectedValue = "0";
                        }
                        if (Convert.ToString(dtwh.Rows[0]["MMasterId"]) == "" && Convert.ToString(dtwh.Rows[0]["TypeofMonthlyGoal"]) == "Busi")
                        {
                            ddlempgoal.SelectedValue = "1";
                        }
                        if (Convert.ToString(dtwh.Rows[0]["MMasterId"]) == "" && Convert.ToString(dtwh.Rows[0]["TypeofMonthlyGoal"]) == "Dept")
                        {
                            ddlempgoal.SelectedValue = "2";
                        }
                        if (Convert.ToString(dtwh.Rows[0]["MMasterId"]) == "" && Convert.ToString(dtwh.Rows[0]["TypeofMonthlyGoal"]) == "Divi")
                        {
                            ddlempgoal.SelectedValue = "3";
                        }

                        if (ddlempgoal.SelectedValue == "0")
                        {
                            fillemployeemonth();
                            Label6.Text = "Employee Monthly Goal";

                            ddlfilterltg.SelectedIndex = ddlfilterltg.Items.IndexOf(ddlfilterltg.Items.FindByValue(dtwh.Rows[0]["mmasterid"].ToString()));
                        }
                        if (ddlempgoal.SelectedValue == "1")
                        {
                            fillbusinessweek56();
                            Label6.Text = "Business Weekly Goal";

                            ddlfilterltg.SelectedIndex = ddlfilterltg.Items.IndexOf(ddlfilterltg.Items.FindByValue(dtwh.Rows[0]["parentmonthlygoalid"].ToString()));
                        }
                        if (ddlempgoal.SelectedValue == "2")
                        {
                            filldepartmentweek();
                            Label6.Text = "Department Weekly Goal";

                            ddlfilterltg.SelectedIndex = ddlfilterltg.Items.IndexOf(ddlfilterltg.Items.FindByValue(dtwh.Rows[0]["parentmonthlygoalid"].ToString()));
                        }
                        if (ddlempgoal.SelectedValue == "3")
                        {
                            filldivisionweek();
                            Label6.Text = "Division Weekly Goal";

                            ddlfilterltg.SelectedIndex = ddlfilterltg.Items.IndexOf(ddlfilterltg.Items.FindByValue(dtwh.Rows[0]["parentmonthlygoalid"].ToString()));
                        }

                        ddlfilterbymission_SelectedIndexChanged(sender, e);

                        ddlstgfilter.SelectedIndex = ddlstgfilter.Items.IndexOf(ddlstgfilter.Items.FindByValue(id.ToString()));

                        fillemptask();
                    }
                    else if (Convert.ToString(dtwh.Rows[0]["DivisionID"].ToString()) != "" && Convert.ToString(dtwh.Rows[0]["EmployeeId"].ToString()) == "")
                    {
                        Label10.Visible = true;
                        ddldivigoal.Visible = true;

                        DropDownList1.SelectedIndex = 2;
                        singlefilterbydivision();

                        ddlbusunesswithdept.SelectedIndex = ddlbusunesswithdept.Items.IndexOf(ddlbusunesswithdept.Items.FindByValue(dtwh.Rows[0]["DivisionId"].ToString()));

                        if (Convert.ToString(dtwh.Rows[0]["MMasterId"]) != "")
                        {
                            ddldivigoal.SelectedValue = "0";
                        }
                        if (Convert.ToString(dtwh.Rows[0]["MMasterId"]) == "" && Convert.ToString(dtwh.Rows[0]["TypeofMonthlyGoal"]) == "Busi")
                        {
                            ddldivigoal.SelectedValue = "1";
                        }
                        if (Convert.ToString(dtwh.Rows[0]["MMasterId"]) == "" && Convert.ToString(dtwh.Rows[0]["TypeofMonthlyGoal"]) == "Dept")
                        {
                            ddldivigoal.SelectedValue = "2";
                        }

                        if (ddldivigoal.SelectedValue == "0")
                        {
                            filldivisionmonth();
                            Label6.Text = "Division Monthly Goal";

                            ddlfilterltg.SelectedIndex = ddlfilterltg.Items.IndexOf(ddlfilterltg.Items.FindByValue(dtwh.Rows[0]["mmasterid"].ToString()));
                        }
                        if (ddldivigoal.SelectedValue == "1")
                        {
                            fillbusinessweek11();
                            Label6.Text = "Business Weekly Goal";

                            ddlfilterltg.SelectedIndex = ddlfilterltg.Items.IndexOf(ddlfilterltg.Items.FindByValue(dtwh.Rows[0]["parentmonthlygoalid"].ToString()));
                        }
                        if (ddldivigoal.SelectedValue == "2")
                        {
                            filldepartmentweek11();
                            Label6.Text = "Department Weekly Goal";

                            ddlfilterltg.SelectedIndex = ddlfilterltg.Items.IndexOf(ddlfilterltg.Items.FindByValue(dtwh.Rows[0]["parentmonthlygoalid"].ToString()));
                        }

                        ddlfilterbymission_SelectedIndexChanged(sender, e);

                        ddlstgfilter.SelectedIndex = ddlstgfilter.Items.IndexOf(ddlstgfilter.Items.FindByValue(id.ToString()));

                        filldivisiontask();
                    }

                    else if (Convert.ToString(dtwh.Rows[0]["DepartmentId"].ToString()) != "" && Convert.ToString(dtwh.Rows[0]["DivisionID"].ToString()) == "" && Convert.ToString(dtwh.Rows[0]["EmployeeId"].ToString()) == "")
                    {
                        Label9.Visible = true;
                        ddldeptgoal.Visible = true;

                        DropDownList1.SelectedIndex = 1;
                        singlefilterbydepartment();

                        ddlbusunesswithdept.SelectedIndex = ddlbusunesswithdept.Items.IndexOf(ddlbusunesswithdept.Items.FindByValue(dtwh.Rows[0]["DepartmentId"].ToString()));

                        if (Convert.ToString(dtwh.Rows[0]["MMasterId"]) != "")
                        {
                            ddldeptgoal.SelectedValue = "0";
                        }
                        else
                        {
                            ddldeptgoal.SelectedValue = "1";
                        }
                        if (ddldeptgoal.SelectedValue == "0")
                        {
                            filldepartmentmonth();
                            Label6.Text = "Department Monthly Goal";

                            ddlfilterltg.SelectedIndex = ddlfilterltg.Items.IndexOf(ddlfilterltg.Items.FindByValue(dtwh.Rows[0]["mmasterid"].ToString()));
                        }
                        if (ddldeptgoal.SelectedValue == "1")
                        {
                            fillbusinessweek();
                            Label6.Text = "Business Weekly Goal";

                            ddlfilterltg.SelectedIndex = ddlfilterltg.Items.IndexOf(ddlfilterltg.Items.FindByValue(dtwh.Rows[0]["parentmonthlygoalid"].ToString()));
                        }

                        ddlfilterbymission_SelectedIndexChanged(sender, e);

                        ddlstgfilter.SelectedIndex = ddlstgfilter.Items.IndexOf(ddlstgfilter.Items.FindByValue(id.ToString()));

                        filldepartmenttask();
                    }

                    else if (Convert.ToString(dtwh.Rows[0]["BusinessId"].ToString()) != "" && Convert.ToString(dtwh.Rows[0]["DepartmentId"].ToString()) == "" && Convert.ToString(dtwh.Rows[0]["DivisionID"].ToString()) == "" && Convert.ToString(dtwh.Rows[0]["EmployeeId"].ToString()) == "")
                    {
                        DropDownList1.SelectedIndex = 0;
                        singlefilterbybusiness();

                        ddlbusunesswithdept.SelectedIndex = ddlbusunesswithdept.Items.IndexOf(ddlbusunesswithdept.Items.FindByValue(dtwh.Rows[0]["BusinessId"].ToString()));

                        fillbusinessmonth();
                        ddlfilterltg.SelectedIndex = ddlfilterltg.Items.IndexOf(ddlfilterltg.Items.FindByValue(dtwh.Rows[0]["MMasterId"].ToString()));

                        ddlfilterbymission_SelectedIndexChanged(sender, e);

                        ddlstgfilter.SelectedIndex = ddlstgfilter.Items.IndexOf(ddlstgfilter.Items.FindByValue(id.ToString()));

                        fillbusinesstask();
                    }

                    //fillfiltermission();
                    //ddlfilterltg.SelectedIndex = ddlfilterltg.Items.IndexOf(ddlfilterltg.Items.FindByValue(dtwh.Rows[0]["MID"].ToString()));


                }
                fillmaster();
                fillinstruction();
                fillevaluation();
                fillattach1();
                fillmasterdocs();
                fillinstructiondocs();
                fillevaluationdocs();
            }
        }
    }
    protected void singlefilterbybusiness()
    {
        DataTable ds = ClsStore.SelectStorename();
        ddlbusunesswithdept.DataSource = ds;
        ddlbusunesswithdept.DataTextField = "Name";
        ddlbusunesswithdept.DataValueField = "WareHouseId";
        ddlbusunesswithdept.DataBind();

        //ddlbusunesswithdept.Items.Insert(0, "All");
        //ddlbusunesswithdept.Items[0].Value = "0";        

    }
    protected void singlefilterbydepartment()
    {

        string str12 = " select WareHouseMaster.Name, DepartmentmasterMNC.Departmentname as Dept,WareHouseMaster.Name +'/'+DepartmentmasterMNC.Departmentname as Departmentname,DepartmentmasterMNC.id as Id from WareHouseMaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.Whid=WareHouseMaster.WareHouseId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname ";
        SqlCommand cmd1 = new SqlCommand(str12, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);

        ddlbusunesswithdept.DataSource = ds1;
        ddlbusunesswithdept.DataTextField = "Departmentname";
        ddlbusunesswithdept.DataValueField = "Id";
        ddlbusunesswithdept.DataBind();


        //ddlbusunesswithdept.Items.Insert(0, "All");
        //ddlbusunesswithdept.Items[0].Value = "0";

        //   fillfiltermission();

    }
    protected void singlefilterbydivision()
    {
        string str12 = " Select WareHouseMaster.Name+'/'+DepartmentmasterMNC.Departmentname+'/'+BusinessMaster.BusinessName as BusinessName ,BusinessMaster.BusinessID from BusinessMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=BusinessMaster.Whid where BusinessMaster.company_id='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName";

        SqlCommand cmd1 = new SqlCommand(str12, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);

        ddlbusunesswithdept.DataSource = ds1;
        ddlbusunesswithdept.DataTextField = "BusinessName";
        ddlbusunesswithdept.DataValueField = "BusinessID";
        ddlbusunesswithdept.DataBind();

        //ddlbusunesswithdept.Items.Insert(0, "All");
        //ddlbusunesswithdept.Items[0].Value = "0";



    }
    protected void singlefilterbyemployee()
    {
        string str12 = " select Left(WareHouseMaster.Name,4)+'/'+Left(DepartmentmasterMNC.Departmentname,4)+'/'+ EmployeeMaster.EmployeeName as EmployeeName,EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID inner join WareHouseMaster on WareHouseMaster.WareHouseId=EmployeeMaster.Whid  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname,EmployeeMaster.EmployeeName";

        SqlCommand cmd1 = new SqlCommand(str12, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);

        ddlbusunesswithdept.DataSource = ds1;
        ddlbusunesswithdept.DataTextField = "EmployeeName";
        ddlbusunesswithdept.DataValueField = "EmployeeMasterID";
        ddlbusunesswithdept.DataBind();


        //ddlbusunesswithdept.Items.Insert(0, "All");
        //ddlbusunesswithdept.Items[0].Value = "0";
    }
    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        Label9.Visible = false;
        ddldeptgoal.Visible = false;

        Label10.Visible = false;
        ddldivigoal.Visible = false;

        Label13.Visible = false;
        ddlempgoal.Visible = false;

        if (DropDownList1.SelectedValue == "0")
        {
            Label6.Text = "Monthly Goal Name";

            Label3.Text = "Business Name";
            singlefilterbybusiness();
            fillbusinessmonth();

            fillbusinesstask();
        }
        if (DropDownList1.SelectedValue == "1")
        {
            Label9.Visible = true;
            ddldeptgoal.Visible = true;

            Label3.Text = "Business/Department";
            singlefilterbydepartment();

            if (ddldeptgoal.SelectedValue == "0")
            {
                filldepartmentmonth();
                Label6.Text = "Department Monthly Goal";
            }
            if (ddldeptgoal.SelectedValue == "1")
            {
                fillbusinessweek();
                Label6.Text = "Business Weekly Goal";
            }

            filldepartmenttask();
        }
        if (DropDownList1.SelectedValue == "2")
        {
            Label10.Visible = true;
            ddldivigoal.Visible = true;

            Label3.Text = "Business/Department/Division";
            singlefilterbydivision();

            if (ddldivigoal.SelectedValue == "0")
            {
                filldivisionmonth();
                Label6.Text = "Division Monthly Goal";
            }
            if (ddldivigoal.SelectedValue == "1")
            {
                fillbusinessweek11();
                Label6.Text = "Business Weekly Goal";
            }
            if (ddldivigoal.SelectedValue == "2")
            {
                filldepartmentweek11();
                Label6.Text = "Department Weekly Goal";
            }
            filldivisiontask();
        }
        if (DropDownList1.SelectedValue == "3")
        {
            Label13.Visible = true;
            ddlempgoal.Visible = true;

            Label3.Text = "Business/Department/Employee";
            singlefilterbyemployee();

            if (ddlempgoal.SelectedValue == "0")
            {
                fillemployeemonth();
                Label6.Text = "Employee Monthly Goal";
            }
            if (ddlempgoal.SelectedValue == "1")
            {
                fillbusinessweek56();
                Label6.Text = "Business Weekly Goal";
            }
            if (ddlempgoal.SelectedValue == "2")
            {
                filldepartmentweek();
                Label6.Text = "Department Weekly Goal";
            }
            if (ddlempgoal.SelectedValue == "3")
            {
                filldivisionweek();
                Label6.Text = "Division Weekly Goal";
            }
            fillemptask();
        }
        //fillfiltermission();
        ddlfilterbymission_SelectedIndexChanged(sender, e);
    }

    protected void fillfiltermission()
    {

        int flag = 0;
        ddlfilterltg.Items.Clear();
        DataTable ds12 = new DataTable();
        if (DropDownList1.SelectedValue == "0")
        {
            flag = 0;
            ds12 = ClsWDetail.SelectMddfilter(ddlbusunesswithdept.SelectedValue, "0", "0", "0", flag);


        }
        else if (DropDownList1.SelectedValue == "1")
        {
            flag = 1;
            ds12 = ClsWDetail.SelectMddfilter("0", ddlbusunesswithdept.SelectedValue, "0", "0", flag);

        }
        else if (DropDownList1.SelectedValue == "2")
        {
            flag = 2;
            ds12 = ClsWDetail.SelectMddfilter("0", "0", ddlbusunesswithdept.SelectedValue, "0", flag);


        }
        else if (DropDownList1.SelectedValue == "3")
        {
            flag = 3;

            ds12 = ClsWDetail.SelectMddfilter("0", "0", "0", ddlbusunesswithdept.SelectedValue, flag);

        }




        if (ds12.Rows.Count > 0)
        {
            ddlfilterltg.DataSource = ds12;

            ddlfilterltg.DataMember = "Title";
            ddlfilterltg.DataTextField = "Title";
            ddlfilterltg.DataValueField = "masterid";
            ddlfilterltg.DataBind();

        }
        ddlfilterltg.Items.Insert(0, "All");
        ddlfilterltg.Items[0].Value = "0";


        //ddlfilterltg.Items.Clear();
        //string bus = "0";
        //string em = "0";
        //if (ddlemployee.SelectedIndex != -1)
        //{
        //    em = ddlemployee.SelectedValue;
        //}
        //else
        //{
        //    em = "0";
        //}
        //if (ddlbusiness.SelectedIndex != -1)
        //{
        //    bus = ddlbusiness.SelectedValue;
        //}
        //else
        //{
        //    bus = "0";
        //}

        //int flag = 0;
        //if (RadioButtonList1.SelectedIndex == 0)
        //{
        //    flag = 0;
        //}
        //else if (RadioButtonList1.SelectedIndex == 1)
        //{
        //    flag = 1;
        //}
        //else if (RadioButtonList1.SelectedIndex == 2)
        //{
        //    flag = 2;
        //}
        //else if (RadioButtonList1.SelectedIndex == 3)
        //{
        //    flag = 3;
        //}
        //DataTable ds12 = new DataTable();
        //if (RadioButtonList1.SelectedIndex == 0)
        //{
        //    ds12 = ClsWDetail.SelectMddfilter(ddlStore.SelectedValue, "0", bus, em, flag);
        //}
        //else
        //{
        //    ds12 = ClsWDetail.SelectMddfilter("0", ddlStore.SelectedValue, bus, em, flag);

        //}
        //if (ds12.Rows.Count > 0)
        //{
        //    ddly.DataSource = ds12;

        //    ddly.DataMember = "Title";
        //    ddly.DataTextField = "Title";
        //    ddly.DataValueField = "masterid";


        //    ddly.DataBind();


        //}




    }
    protected void ddlbusunesswithdept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "0")
        {
            fillbusinessmonth();
            fillbusinesstask();
        }

        if (DropDownList1.SelectedValue == "1")
        {
            if (ddldeptgoal.SelectedValue == "0")
            {
                filldepartmentmonth();
                Label6.Text = "Department Monthly Goal";
            }
            if (ddldeptgoal.SelectedValue == "1")
            {
                fillbusinessweek();
                Label6.Text = "Business Weekly Goal";
            }

            filldepartmenttask();
        }

        if (DropDownList1.SelectedValue == "2")
        {
            if (ddldivigoal.SelectedValue == "0")
            {
                filldivisionmonth();
                Label6.Text = "Division Monthly Goal";
            }
            if (ddldivigoal.SelectedValue == "1")
            {
                fillbusinessweek11();
                Label6.Text = "Business Weekly Goal";
            }
            if (ddldivigoal.SelectedValue == "2")
            {
                filldepartmentweek11();
                Label6.Text = "Department Weekly Goal";
            }
            filldivisiontask();
        }

        if (DropDownList1.SelectedValue == "3")
        {
            if (ddlempgoal.SelectedValue == "0")
            {
                fillemployeemonth();
                Label6.Text = "Employee Monthly Goal";
            }
            if (ddlempgoal.SelectedValue == "1")
            {
                fillbusinessweek56();
                Label6.Text = "Business Weekly Goal";
            }
            if (ddlempgoal.SelectedValue == "2")
            {
                filldepartmentweek();
                Label6.Text = "Department Weekly Goal";
            }
            if (ddlempgoal.SelectedValue == "3")
            {
                filldivisionweek();
                Label6.Text = "Division Weekly Goal";
            }

            fillemptask();
        }
        //fillemptask();
        //fillfiltermission();
        ddlfilterbymission_SelectedIndexChanged(sender, e);
    }

    protected void fillemptask()
    {
        string str2 = " Select EmployeeMaster.Whid from WareHouseMaster inner join EmployeeMaster on EmployeeMaster.Whid=WareHouseMaster.WareHouseId where  EmployeeMaster.EmployeeMasterID= '" + ddlbusunesswithdept.SelectedValue + "'  ";

        SqlCommand cmd2 = new SqlCommand(str2, con);
        SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
        DataTable dtt2 = new DataTable();
        adp2.Fill(dtt2);

        if (dtt2.Rows.Count > 0)
        {
            if (ddlstgfilter.SelectedIndex != -1)
            {
                string str = "TaskAllocationMaster.*,TaskMaster.TaskName as TaskMasterName,TaskMaster.Rate,EmployeeMaster.EmployeeName as Employee FROM TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status where TaskMaster.Whid='" + Convert.ToString(dtt2.Rows[0]["Whid"]) + "' and TaskAllocationMaster.EmployeeId='" + ddlbusunesswithdept.SelectedValue + "' and TaskMaster.WeeklygoalId='" + ddlstgfilter.SelectedValue + "'";

                string str3 = "select Count(TaskAllocationMaster.TaskAllocationId) as ci FROM TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status where TaskMaster.Whid='" + Convert.ToString(dtt2.Rows[0]["Whid"]) + "' and TaskAllocationMaster.EmployeeId='" + ddlbusunesswithdept.SelectedValue + "' and TaskMaster.WeeklygoalId='" + ddlstgfilter.SelectedValue + "'";

                GridView6.VirtualItemCount = GetRowCount2(str3);

                string sortExpression = " TaskAllocationMaster.TaskAllocationDate";

                if (Convert.ToInt32(ViewState["count"]) > 0)
                {
                    GridView6.DataSource = GetDataPage2(GridView6.PageIndex, GridView6.PageSize, sortExpression, str);
                    GridView6.DataBind();

                    Panel8.Visible = true;
                    Label14.Visible = true;
                }
                else
                {
                    GridView6.DataSource = null;
                    GridView6.DataBind();

                    Panel8.Visible = false;
                    Label14.Visible = false;
                }
            }
            else
            {
                GridView6.DataSource = null;
                GridView6.DataBind();

                Panel8.Visible = false;
                Label14.Visible = false;
            }
        }
        else
        {
            GridView6.DataSource = null;
            GridView6.DataBind();

            Panel8.Visible = false;
            Label14.Visible = false;
        }
    }

    protected void fillbusinesstask()
    {
        if (ddlstgfilter.SelectedIndex != -1)
        {
            string str = "TaskAllocationMaster.*,TaskMaster.TaskName as TaskMasterName,TaskMaster.Rate,EmployeeMaster.EmployeeName as Employee FROM TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status where TaskMaster.Whid='" + ddlbusunesswithdept.SelectedValue + "' and TypeofMonthlyGoal='Busi' and WMaster.parentmonthlygoalid='" + ddlstgfilter.SelectedValue + "'";

            string str2 = "select Count(TaskAllocationMaster.TaskAllocationId) as ci FROM TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status where TaskMaster.Whid='" + ddlbusunesswithdept.SelectedValue + "' and TypeofMonthlyGoal='Busi' and WMaster.parentmonthlygoalid='" + ddlstgfilter.SelectedValue + "'";

            GridView6.VirtualItemCount = GetRowCount2(str2);

            string sortExpression = " TaskAllocationMaster.TaskAllocationDate";

            if (Convert.ToInt32(ViewState["count"]) > 0)
            {
                GridView6.DataSource = GetDataPage2(GridView6.PageIndex, GridView6.PageSize, sortExpression, str);
                GridView6.DataBind();

                Panel8.Visible = true;
                Label14.Visible = true;
            }
            else
            {
                GridView6.DataSource = null;
                GridView6.DataBind();

                Panel8.Visible = false;
                Label14.Visible = false;
            }
        }
        else
        {
            GridView6.DataSource = null;
            GridView6.DataBind();

            Panel8.Visible = false;
            Label14.Visible = false;
        }
    }

    protected void filldepartmenttask()
    {
        SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlbusunesswithdept.SelectedValue + "'", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {

            if (ddlstgfilter.SelectedIndex != -1)
            {
                string str = "TaskAllocationMaster.*,TaskMaster.TaskName as TaskMasterName,TaskMaster.Rate,EmployeeMaster.EmployeeName as Employee FROM TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status where TaskMaster.Whid='" + Convert.ToString(dt1.Rows[0]["Warehouseid"]) + "' and TypeofMonthlyGoal='Dept' and WMaster.parentmonthlygoalid='" + ddlstgfilter.SelectedValue + "'";

                string str2 = "select Count(TaskAllocationMaster.TaskAllocationId) as ci FROM TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status where TaskMaster.Whid='" + Convert.ToString(dt1.Rows[0]["Warehouseid"]) + "' and TypeofMonthlyGoal='Dept' and WMaster.parentmonthlygoalid='" + ddlstgfilter.SelectedValue + "'";

                GridView6.VirtualItemCount = GetRowCount2(str2);

                string sortExpression = " TaskAllocationMaster.TaskAllocationDate";

                if (Convert.ToInt32(ViewState["count"]) > 0)
                {
                    GridView6.DataSource = GetDataPage2(GridView6.PageIndex, GridView6.PageSize, sortExpression, str);
                    GridView6.DataBind();

                    Panel8.Visible = true;
                    Label14.Visible = true;
                }
                else
                {
                    GridView6.DataSource = null;
                    GridView6.DataBind();

                    Panel8.Visible = false;
                    Label14.Visible = false;
                }
            }
            else
            {
                GridView6.DataSource = null;
                GridView6.DataBind();

                Panel8.Visible = false;
                Label14.Visible = false;
            }
        }
        else
        {
            GridView6.DataSource = null;
            GridView6.DataBind();

            Panel8.Visible = false;
            Label14.Visible = false;
        }
    }

    protected void filldivisiontask()
    {
        SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId,DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlbusunesswithdept.SelectedValue + "'", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {

            if (ddlstgfilter.SelectedIndex != -1)
            {
                string str = "TaskAllocationMaster.*,TaskMaster.TaskName as TaskMasterName,TaskMaster.Rate,EmployeeMaster.EmployeeName as Employee FROM TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status where TaskMaster.Whid='" + Convert.ToString(dt1.Rows[0]["Warehouseid"]) + "' and TypeofMonthlyGoal='Dept' and WMaster.parentmonthlygoalid='" + ddlstgfilter.SelectedValue + "'";

                string str2 = "select Count(TaskAllocationMaster.TaskAllocationId) as ci FROM TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status where TaskMaster.Whid='" + Convert.ToString(dt1.Rows[0]["Warehouseid"]) + "' and TypeofMonthlyGoal='Dept' and WMaster.parentmonthlygoalid='" + ddlstgfilter.SelectedValue + "'";

                GridView6.VirtualItemCount = GetRowCount2(str2);

                string sortExpression = " TaskAllocationMaster.TaskAllocationDate";

                if (Convert.ToInt32(ViewState["count"]) > 0)
                {
                    GridView6.DataSource = GetDataPage2(GridView6.PageIndex, GridView6.PageSize, sortExpression, str);
                    GridView6.DataBind();

                    Panel8.Visible = true;
                    Label14.Visible = true;
                }
                else
                {
                    GridView6.DataSource = null;
                    GridView6.DataBind();

                    Panel8.Visible = false;
                    Label14.Visible = false;
                }
            }
            else
            {
                GridView6.DataSource = null;
                GridView6.DataBind();

                Panel8.Visible = false;
                Label14.Visible = false;
            }
        }
        else
        {
            GridView6.DataSource = null;
            GridView6.DataBind();

            Panel8.Visible = false;
            Label14.Visible = false;
        }
    }

    protected void ddlfilterbymission_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlstgfilter.Items.Clear();

        if (ddlfilterltg.SelectedIndex != -1)
        {
            if (DropDownList1.SelectedValue == "0")
            {
                string str = "select * from WMaster where Mmasterid ='" + ddlfilterltg.SelectedValue + "'";
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlstgfilter.DataSource = dt;
                //ddlstgfilter.DataSource = ClsWDetail.SpWeekMasterGetDataBymmasterid(Convert.ToString(ddlfilterltg.SelectedValue));
                ddlstgfilter.DataMember = "title";
                ddlstgfilter.DataTextField = "title";
                ddlstgfilter.DataValueField = "masterid";
                ddlstgfilter.DataBind();

                fillbusinesstask();
            }

            if (DropDownList1.SelectedValue == "1")
            {
                if (ddldeptgoal.SelectedValue == "0")
                {
                    string str = "select * from WMaster where Mmasterid ='" + ddlfilterltg.SelectedValue + "'";
                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ddlstgfilter.DataSource = dt;
                    ddlstgfilter.DataMember = "title";
                    ddlstgfilter.DataTextField = "title";
                    ddlstgfilter.DataValueField = "masterid";
                    ddlstgfilter.DataBind();
                }
                if (ddldeptgoal.SelectedValue == "1")
                {
                    string str = "select * from WMaster where parentmonthlygoalid ='" + ddlfilterltg.SelectedValue + "'";
                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ddlstgfilter.DataSource = dt;
                    ddlstgfilter.DataMember = "title";
                    ddlstgfilter.DataTextField = "title";
                    ddlstgfilter.DataValueField = "masterid";
                    ddlstgfilter.DataBind();
                }

                filldepartmenttask();
            }

            if (DropDownList1.SelectedValue == "2")
            {
                if (ddldivigoal.SelectedValue == "0")
                {
                    string str = "select * from WMaster where Mmasterid ='" + ddlfilterltg.SelectedValue + "'";
                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ddlstgfilter.DataSource = dt;
                    ddlstgfilter.DataMember = "title";
                    ddlstgfilter.DataTextField = "title";
                    ddlstgfilter.DataValueField = "masterid";
                    ddlstgfilter.DataBind();
                }
                if (ddldivigoal.SelectedValue == "1" || ddldivigoal.SelectedValue == "2")
                {
                    string str = "select * from WMaster where parentmonthlygoalid ='" + ddlfilterltg.SelectedValue + "'";
                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ddlstgfilter.DataSource = dt;
                    ddlstgfilter.DataMember = "title";
                    ddlstgfilter.DataTextField = "title";
                    ddlstgfilter.DataValueField = "masterid";
                    ddlstgfilter.DataBind();
                }

                filldivisiontask();
            }

            if (DropDownList1.SelectedValue == "3")
            {
                if (ddlempgoal.SelectedValue == "0")
                {
                    string str = "select * from WMaster where Mmasterid ='" + ddlfilterltg.SelectedValue + "'";
                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ddlstgfilter.DataSource = dt;
                    ddlstgfilter.DataMember = "title";
                    ddlstgfilter.DataTextField = "title";
                    ddlstgfilter.DataValueField = "masterid";
                    ddlstgfilter.DataBind();
                }
                if (ddlempgoal.SelectedValue == "1" || ddlempgoal.SelectedValue == "2" || ddlempgoal.SelectedValue == "3")
                {
                    string str = "select * from WMaster where parentmonthlygoalid ='" + ddlfilterltg.SelectedValue + "'";
                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ddlstgfilter.DataSource = dt;
                    ddlstgfilter.DataMember = "title";
                    ddlstgfilter.DataTextField = "title";
                    ddlstgfilter.DataValueField = "masterid";
                    ddlstgfilter.DataBind();
                }
                fillemptask();
            }

        }

        ddlstgfilter_SelectedIndexChanged(sender, e);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Print and Export")
        {
            Button1.Text = "Hide Print and Export";
            Button7.Visible = true;

            ddlExport.Visible = true;

           

            GridView2.AllowPaging = false;
            GridView2.PageSize = 1000;
            filterfillinstructionon();  
                                
            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            filterfillevaluationon();

            GridView6.AllowPaging = false;
            GridView6.PageSize = 1000;
            if (DropDownList1.SelectedValue == "0")
            {
                fillbusinesstask();
            }
            if (DropDownList1.SelectedValue == "1")
            {
                filldepartmenttask();
            }
            if (DropDownList1.SelectedValue == "2")
            {
                filldivisiontask();
            }
            if (DropDownList1.SelectedValue == "3")
            {
                fillemptask();
            }

            imgadddept.Visible = false;
            imgadddivision.Visible = false;

            imgdeptrefresh.Visible = false;
            imgdivisionrefresh.Visible = false;
            ImgFIle.Visible = false;

            lblBusiness.Text = ddlbusunesswithdept.SelectedItem.Text;

        }
        else
        {
            Button1.Text = "Print and Export";
            Button7.Visible = false;

            ddlExport.Visible = false;

            //if (GridView2.Rows.Count > 0)
            //{
            //    imgadddept.Visible = true;
            //    imgadddivision.Visible = true;
            //}
            //if (GridView1.Rows.Count > 0)
            //{
            //    imgdeptrefresh.Visible = true;
            //    imgdivisionrefresh.Visible = true;
            //}
            ImgFIle.Visible = true;

            GridView2.AllowPaging = true;
            GridView2.PageSize = 5;
            filterfillinstructionon();

            GridView1.AllowPaging = true;
            GridView1.PageSize = 5;
            filterfillevaluationon();

            GridView6.AllowPaging = true;
            GridView6.PageSize = 5;
            if (DropDownList1.SelectedValue == "0")
            {
                fillbusinesstask();
            }
            if (DropDownList1.SelectedValue == "1")
            {
                filldepartmenttask();
            }
            if (DropDownList1.SelectedValue == "2")
            {
                filldivisiontask();
            }
            if (DropDownList1.SelectedValue == "3")
            {
                fillemptask();
            }

        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            DataTable dt = new DataTable();
            dt = SelectDoucmentMasterByID(Convert.ToInt32(e.CommandArgument));

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
                    //Response.ClearContent();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = ReturnExtension(file.Extension.ToLower());
                    //Response.TransmitFile(file.FullName);
                    //Response.End();
                    String temp = "http://license.busiwiz.com/ioffice/Account/" + Session["comid"] + "/UploadedDocuments//" + docname + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('http://license.busiwiz.com/Account/" + Session["comid"] + "/UploadedDocuments/" + docname + "');", true);
                }
            }
        }
    }
    public DataTable SelectDoucmentMasterByID(int DocumentId)
    {

        SqlCommand cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByID";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        DataTable dttable;
        SqlDataAdapter adp = new SqlDataAdapter((SqlCommand)cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds, cmd.CommandText);
        dttable = ds.Tables[0];

        adp.Fill(dttable);

        return dttable;
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
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            DataTable dt = new DataTable();
            dt = SelectDoucmentMasterByID(Convert.ToInt32(e.CommandArgument));

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
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = ReturnExtension(file.Extension.ToLower());
                    Response.TransmitFile(file.FullName);

                    Response.End();

                }
            }
        }

    }
    protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            DataTable dt = new DataTable();
            dt = SelectDoucmentMasterByID(Convert.ToInt32(e.CommandArgument));

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
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = ReturnExtension(file.Extension.ToLower());
                    Response.TransmitFile(file.FullName);

                    Response.End();

                }
            }
        }

    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "FrmWDetail.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        fillinstruction();

    }
    protected void imgadddivision_Click(object sender, ImageClickEventArgs e)
    {
        string te = "FrmWEvaluation.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgdivisionrefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillevaluation();
    }
    protected void fillmaster()
    {
        string strmaster = "Select WMaster.*,StatusMaster.StatusName from WMaster Left join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WMaster.MasterId='" + Request.QueryString["Wid"].ToString() + "' ";
        SqlCommand cmdmaster = new SqlCommand(strmaster, con);
        SqlDataAdapter adpmaster = new SqlDataAdapter(cmdmaster);
        DataTable dtmaster = new DataTable();
        adpmaster.Fill(dtmaster);
        if (dtmaster.Rows.Count > 0)
        {
            lblltgtitile.Text = dtmaster.Rows[0]["Title"].ToString();
            lblstatus.Text = dtmaster.Rows[0]["StatusName"].ToString();
            lbldescription.Text = dtmaster.Rows[0]["Description"].ToString();
            lblbudgetedcost.Text = dtmaster.Rows[0]["BudgetedCost"].ToString();
        }

        string stract = "";

        if (DropDownList1.SelectedValue == "3")
        {
            stract = "select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where WMaster.MasterId='" + Convert.ToString(Request.QueryString["Wid"]) + "'";
        }

        if (DropDownList1.SelectedValue == "0")
        {
            stract = "select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Busi' and parentmonthlygoalid='" + Convert.ToString(Request.QueryString["Wid"]) + "'";
        }

        if (DropDownList1.SelectedValue == "1")
        {
            stract = "select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Dept' and parentmonthlygoalid='" + Convert.ToString(Request.QueryString["Wid"]) + "'";
        }

        if (DropDownList1.SelectedValue == "2")
        {
            stract = "select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Divi' and parentmonthlygoalid='" + Convert.ToString(Request.QueryString["Wid"]) + "'";
        }

        SqlDataAdapter dar = new SqlDataAdapter(stract, con);
        DataTable dtr = new DataTable();
        dar.Fill(dtr);

        if (dtr.Rows.Count > 0)
        {
            if (Convert.ToString(dtr.Rows[0]["cost"]) != "")
            {
                lblactualcost.Text = dtr.Rows[0]["cost"].ToString();
            }
            else
            {
                lblactualcost.Text = "0";
            }
        }
        else
        {
            lblactualcost.Text = "0";
        }

        double value = Convert.ToDouble(lblbudgetedcost.Text) - Convert.ToDouble(lblactualcost.Text);
        lblshortageexcess.Text = value.ToString();

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

    private int GetRowCount1(string str)
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

    private int GetRowCount2(string str)
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

    private DataTable GetDataPage1(int pageIndex, int pageSize, string sortExpression, string query)
    {
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));
        dt.Columns.Remove("ROW_NUM");
        return dt;

        ViewState["dt"] = dt;
    }

    private DataTable GetDataPage2(int pageIndex, int pageSize, string sortExpression, string query)
    {
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));
        dt.Columns.Remove("ROW_NUM");
        return dt;

        ViewState["dt"] = dt;
    }

    protected void fillinstruction()
    {
        //string str = "Select WDetail.*,WMaster.BusinessID as DocumentId from WDetail inner join WMaster on WDetail.MasterId=WMaster.MasterId where WMaster.MasterId='" + Request.QueryString["Wid"].ToString() + "' ";

        string str = "WDetail.*,WMaster.BusinessID as DocumentId from WDetail inner join WMaster on WDetail.MasterId=WMaster.MasterId where WMaster.MasterId='" + Request.QueryString["Wid"].ToString() + "' ";

        string str2 = "select Count(WDetail.DetailId) as ci from WDetail inner join WMaster on WDetail.MasterId=WMaster.MasterId where WMaster.MasterId='" + Request.QueryString["Wid"].ToString() + "'";

        GridView2.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WDetail.Date";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(GridView2.PageIndex, GridView2.PageSize, sortExpression, str);

            GridView2.DataSource = dt;

            for (int rowindex = 0; rowindex < dt.Rows.Count; rowindex++)
            {
                DataTable dtcrNew11 = ClsWDetail.SelectOfficeManagerDocumentsforWDetailID(Convert.ToString(dt.Rows[rowindex]["DetailId"]));

                dt.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];
            }

            GridView2.DataBind();

            lblinstruction.Visible = true;
            Panel3.Visible = true;

            imgadddept.Visible = true;
            imgdeptrefresh.Visible = true;
        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();

            lblinstruction.Visible = false;
            Panel3.Visible = false;

            imgadddept.Visible = false;
            imgdeptrefresh.Visible = false;
        }
    }
    protected void fillevaluation()
    {
        string str = "WEvaluation.*,WMaster.BusinessID as DocumentId from WEvaluation inner join WMaster on WEvaluation.MasterId=WMaster.MasterId where WMaster.MasterId='" + Request.QueryString["Wid"].ToString() + "' ";

        string str2 = "select Count(WEvaluation.EvaluationId) as ci from WEvaluation inner join WMaster on WEvaluation.MasterId=WMaster.MasterId where WMaster.MasterId='" + Request.QueryString["Wid"].ToString() + "' ";

        GridView1.VirtualItemCount = GetRowCount1(str2);

        string sortExpression = " WEvaluation.Date";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage1(GridView1.PageIndex, GridView1.PageSize, sortExpression, str);

            GridView1.DataSource = dt;

            for (int rowindex = 0; rowindex < dt.Rows.Count; rowindex++)
            {
                DataTable dtcrNew11 = ClsWEvaluation.SelectOfficeManagerDocumentsforWevaID(Convert.ToString(dt.Rows[rowindex]["EvaluationId"]));

                dt.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];
            }

            GridView1.DataBind();

            Panel1.Visible = true;
            lblevaluation.Visible = true;

            imgdivisionrefresh.Visible = true;
            imgadddivision.Visible = true;
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            lblevaluation.Visible = false;
            Panel1.Visible = false;

            imgdivisionrefresh.Visible = false;
            imgadddivision.Visible = false;
        }

        //string strevaluation = "Select WEvaluation.*,WMaster.BusinessID as DocumentId from WEvaluation inner join WMaster on WEvaluation.MasterId=WMaster.MasterId where WMaster.MasterId='" + Request.QueryString["Wid"].ToString() + "' ";
        //SqlCommand cmdevelution = new SqlCommand(strevaluation, con);
        //SqlDataAdapter adpeve = new SqlDataAdapter(cmdevelution);
        //DataTable dteve = new DataTable();
        //adpeve.Fill(dteve);

        //for (int rowindex = 0; rowindex < dteve.Rows.Count; rowindex++)
        //{
        //    DataTable dtcrNew11 = ClsWEvaluation.SelectOfficeManagerDocumentsforWevaID(Convert.ToString(dteve.Rows[rowindex]["EvaluationId"]));

        //    dteve.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];
        //}

        //if (dteve.Rows.Count > 0)
        //{
        //    GridView1.DataSource = dteve;
        //    GridView1.DataBind();

        //    Panel1.Visible = true;
        //    lblevaluation.Visible = true;

        //    imgdivisionrefresh.Visible = true;
        //    imgadddivision.Visible = true;
        //}
        //else
        //{
        //    GridView1.DataSource = null;
        //    GridView1.DataBind();

        //    lblevaluation.Visible = false;
        //    Panel1.Visible = false;

        //    imgdivisionrefresh.Visible = false;
        //    imgadddivision.Visible = false;
        //}

    }
    protected void fillmasterdocs()
    {
        string strltgdoc = "Select  OfficeManagerDocuments.Id,DocumentMaster.DocumentName, DocumentMaster.DocumentId,CONVERT(nvarchar, DocumentMaster.DocumentDate,101) as DocumentDate,DocumentMaster.DocumentTitle  from OfficeManagerDocuments inner join DocumentMaster on DocumentMaster.DocumentId=OfficeManagerDocuments.DocumentId  where OfficeManagerDocuments.WgId='" + Request.QueryString["Wid"].ToString() + "' ";
        SqlCommand cmdltgdoc = new SqlCommand(strltgdoc, con);
        SqlDataAdapter adpltgdoc = new SqlDataAdapter(cmdltgdoc);
        DataTable dtdoc = new DataTable();
        adpltgdoc.Fill(dtdoc);
        if (dtdoc.Rows.Count > 0)
        {
            lblltgdocs.Visible = false;
            //Panel2.Visible = true;

            GridView3.DataSource = dtdoc;
            GridView3.DataBind();
        }
    }
    protected void fillinstructiondocs()
    {
        string avgmaterial = "  ";
        SqlCommand cmdavgmat = new SqlCommand(avgmaterial, con);
        SqlDataAdapter adpavgmat = new SqlDataAdapter(cmdavgmat);
        DataTable dsavgma = new DataTable();
        adpavgmat.Fill(dsavgma);

        string materialin = "";

        materialin = " where OfficeManagerDocuments.Wdetail In (select DetailId from Wdetail where MasterId='" + Request.QueryString["Wid"].ToString() + "') ";

        string strltgdetaildoc = "Select  OfficeManagerDocuments.Id,DocumentMaster.DocumentName, DocumentMaster.DocumentId,CONVERT(nvarchar, DocumentMaster.DocumentDate,101) as DocumentDate,DocumentMaster.DocumentTitle  from OfficeManagerDocuments inner join DocumentMaster on DocumentMaster.DocumentId=OfficeManagerDocuments.DocumentId " + materialin + "   ";
        SqlCommand cmdltgdetaildoc = new SqlCommand(strltgdetaildoc, con);
        SqlDataAdapter adpltgdetaildoc = new SqlDataAdapter(cmdltgdetaildoc);
        DataTable dtdetaildoc = new DataTable();
        adpltgdetaildoc.Fill(dtdetaildoc);
        if (dtdetaildoc.Rows.Count > 0)
        {
            lblinstructiondocs.Visible = false;
            //Panel4.Visible = true;

            GridView4.DataSource = dtdetaildoc;
            GridView4.DataBind();

        }
    }
    protected void fillevaluationdocs()
    {
        string materialin1 = "";


        materialin1 = " where OfficeManagerDocuments.Wevalution In (select distinct EvaluationId from WEvaluation where MasterId='" + Request.QueryString["Wid"].ToString() + "') ";

        string strltgeveldoc = "Select  OfficeManagerDocuments.Id,DocumentMaster.DocumentName, DocumentMaster.DocumentId,CONVERT(nvarchar, DocumentMaster.DocumentDate,101) as DocumentDate,DocumentMaster.DocumentTitle  from OfficeManagerDocuments inner join DocumentMaster on DocumentMaster.DocumentId=OfficeManagerDocuments.DocumentId  " + materialin1 + " ";
        SqlCommand cmdltgeveldoc = new SqlCommand(strltgeveldoc, con);
        SqlDataAdapter adpltgeveldoc = new SqlDataAdapter(cmdltgeveldoc);
        DataTable dteveldoc = new DataTable();
        adpltgeveldoc.Fill(dteveldoc);
        if (dteveldoc.Rows.Count > 0)
        {
            lblevaluationdocs.Visible = false;
            //Panel5.Visible = true;

            GridView5.DataSource = dteveldoc;
            GridView5.DataBind();

        }

    }
    protected void ddlstgfilter_SelectedIndexChanged(object sender, EventArgs e)
    {

        filterfillmasteron();
        filterfillinstructionon();
        filterfillevaluationon();
        fillattach123();
        filterfillmasterdocson();
        filterfillinstructiondocson();
        filterfillevaluationdocson();

        if (DropDownList1.SelectedValue == "0")
        {
            fillbusinesstask();
        }
        if (DropDownList1.SelectedValue == "1")
        {
            filldepartmenttask();
        }
        if (DropDownList1.SelectedValue == "2")
        {
            filldivisiontask();
        }
        if (DropDownList1.SelectedValue == "3")
        {
            fillemptask();
        }

    }
    protected void filterfillmasteron()
    {
        if (ddlstgfilter.SelectedIndex != -1)
        {

            string strmaster = "Select WMaster.*,StatusMaster.StatusName from WMaster Left join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WMaster.MasterId='" + ddlstgfilter.SelectedValue + "' ";
            SqlCommand cmdmaster = new SqlCommand(strmaster, con);
            SqlDataAdapter adpmaster = new SqlDataAdapter(cmdmaster);
            DataTable dtmaster = new DataTable();
            adpmaster.Fill(dtmaster);

            if (dtmaster.Rows.Count > 0)
            {
                lblltgtitile.Text = dtmaster.Rows[0]["Title"].ToString();
                lblstatus.Text = dtmaster.Rows[0]["StatusName"].ToString();
                lbldescription.Text = dtmaster.Rows[0]["Description"].ToString();
                lblbudgetedcost.Text = dtmaster.Rows[0]["BudgetedCost"].ToString();
            }
            else
            {
                lblltgtitile.Text = "";
                lblstatus.Text = "";
                lbldescription.Text = "";
                lblbudgetedcost.Text = "0";
            }
        }
        else
        {
            lblltgtitile.Text = "";
            lblstatus.Text = "";
            lbldescription.Text = "";
            lblbudgetedcost.Text = "0";
        }

        string stract = "";

        if (DropDownList1.SelectedValue == "3")
        {
            stract = "select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where WMaster.MasterId='" + Convert.ToString(ddlstgfilter.SelectedValue) + "'";
        }

        if (DropDownList1.SelectedValue == "0")
        {
            stract = "select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Busi' and parentmonthlygoalid='" + Convert.ToString(ddlstgfilter.SelectedValue) + "'";
        }

        if (DropDownList1.SelectedValue == "1")
        {
            stract = "select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Dept' and parentmonthlygoalid='" + Convert.ToString(ddlstgfilter.SelectedValue) + "'";
        }

        if (DropDownList1.SelectedValue == "2")
        {
            stract = "select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Divi' and parentmonthlygoalid='" + Convert.ToString(ddlstgfilter.SelectedValue) + "'";
        }

        SqlDataAdapter dar = new SqlDataAdapter(stract, con);
        DataTable dtr = new DataTable();
        dar.Fill(dtr);

        if (dtr.Rows.Count > 0)
        {
            if (Convert.ToString(dtr.Rows[0]["cost"]) != "")
            {
                lblactualcost.Text = dtr.Rows[0]["cost"].ToString();
            }
            else
            {
                lblactualcost.Text = "0";
            }
        }
        else
        {
            lblactualcost.Text = "0";
        }

        double value = Convert.ToDouble(lblbudgetedcost.Text) - Convert.ToDouble(lblactualcost.Text);
        lblshortageexcess.Text = value.ToString();

    }
    protected void filterfillinstructionon()
    {

        string str = "WDetail.*,WMaster.BusinessID as DocumentId from WDetail inner join WMaster on WDetail.MasterId=WMaster.MasterId where WMaster.MasterId='" + ddlstgfilter.SelectedValue + "' ";

        string str2 = "select Count(WDetail.DetailId) as ci from WDetail inner join WMaster on WDetail.MasterId=WMaster.MasterId where WMaster.MasterId='" + ddlstgfilter.SelectedValue + "' ";

        GridView2.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WDetail.Date";



        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(GridView2.PageIndex, GridView2.PageSize, sortExpression, str);

            GridView2.DataSource = dt;

            for (int rowindex = 0; rowindex < dt.Rows.Count; rowindex++)
            {
                DataTable dtcrNew11 = ClsWDetail.SelectOfficeManagerDocumentsforWDetailID(Convert.ToString(dt.Rows[rowindex]["DetailId"]));

                dt.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];
            }

            GridView2.DataBind();

            lblinstruction.Visible = true;
            Panel3.Visible = true;

            imgadddept.Visible = true;
            imgdeptrefresh.Visible = true;
        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();

            lblinstruction.Visible = false;
            Panel3.Visible = false;

            imgadddept.Visible = false;
            imgdeptrefresh.Visible = false;
        }

        //string str = "Select WDetail.*,WMaster.BusinessId as DocumentId from WDetail inner join WMaster on WDetail.MasterId=WMaster.MasterId where WMaster.MasterId='" + ddlstgfilter.SelectedValue + "' ";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);

        //for (int rowindex = 0; rowindex < dt.Rows.Count; rowindex++)
        //{
        //    DataTable dtcrNew11 = ClsWDetail.SelectOfficeManagerDocumentsforWDetailID(Convert.ToString(dt.Rows[rowindex]["DetailId"]));

        //    dt.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];
        //}

        //if (dt.Rows.Count > 0)
        //{
        //    GridView2.DataSource = dt;
        //    GridView2.DataBind();

        //    lblinstruction.Visible = true;
        //    Panel3.Visible = true;

        //    imgadddept.Visible = true;
        //    imgdeptrefresh.Visible = true;
        //}
        //else
        //{
        //    GridView2.DataSource = null;
        //    GridView2.DataBind();

        //    lblinstruction.Visible = false;
        //    Panel3.Visible = false;

        //    imgadddept.Visible = false;
        //    imgdeptrefresh.Visible = false;
        //}

    }
    protected void filterfillevaluationon()
    {
        string str = "WEvaluation.*,WMaster.BusinessID as DocumentId from WEvaluation inner join WMaster on WEvaluation.MasterId=WMaster.MasterId where WMaster.MasterId='" + ddlstgfilter.SelectedValue + "' ";

        string str2 = "select Count(WEvaluation.EvaluationId) as ci from WEvaluation inner join WMaster on WEvaluation.MasterId=WMaster.MasterId where WMaster.MasterId='" + ddlstgfilter.SelectedValue + "' ";

        GridView1.VirtualItemCount = GetRowCount1(str2);

        string sortExpression = " WEvaluation.Date";



        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage1(GridView1.PageIndex, GridView1.PageSize, sortExpression, str);

            GridView1.DataSource = dt;

            for (int rowindex = 0; rowindex < dt.Rows.Count; rowindex++)
            {
                DataTable dtcrNew11 = ClsWEvaluation.SelectOfficeManagerDocumentsforWevaID(Convert.ToString(dt.Rows[rowindex]["EvaluationId"]));

                dt.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];
            }

            GridView1.DataBind();

            Panel1.Visible = true;
            lblevaluation.Visible = true;

            imgdivisionrefresh.Visible = true;
            imgadddivision.Visible = true;
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            lblevaluation.Visible = false;
            Panel1.Visible = false;

            imgdivisionrefresh.Visible = false;
            imgadddivision.Visible = false;
        }

        //string strevaluation = "Select WEvaluation.*,WMaster.BusinessID as DocumentId from WEvaluation inner join WMaster on WEvaluation.MasterId=WMaster.MasterId where WMaster.MasterId='" + ddlstgfilter.SelectedValue + "' ";
        //SqlCommand cmdevelution = new SqlCommand(strevaluation, con);
        //SqlDataAdapter adpeve = new SqlDataAdapter(cmdevelution);
        //DataTable dteve = new DataTable();
        //adpeve.Fill(dteve);

        //for (int rowindex = 0; rowindex < dteve.Rows.Count; rowindex++)
        //{
        //    DataTable dtcrNew11 = ClsWEvaluation.SelectOfficeManagerDocumentsforWevaID(Convert.ToString(dteve.Rows[rowindex]["EvaluationId"]));

        //    dteve.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];
        //}

        //if (dteve.Rows.Count > 0)
        //{
        //    GridView1.DataSource = dteve;
        //    GridView1.DataBind();
        //    Panel1.Visible = true;
        //    lblevaluation.Visible = true;

        //    imgdivisionrefresh.Visible = true;
        //    imgadddivision.Visible = true;
        //}
        //else
        //{
        //    GridView1.DataSource = null;
        //    GridView1.DataBind();
        //    lblevaluation.Visible = false;
        //    Panel1.Visible = false;

        //    imgdivisionrefresh.Visible = false;
        //    imgadddivision.Visible = false;
        //}

    }
    protected void filterfillmasterdocson()
    {
        string strltgdoc = "Select  OfficeManagerDocuments.Id,DocumentMaster.DocumentName, DocumentMaster.DocumentId,CONVERT(nvarchar, DocumentMaster.DocumentDate,101) as DocumentDate,DocumentMaster.DocumentTitle  from OfficeManagerDocuments inner join DocumentMaster on DocumentMaster.DocumentId=OfficeManagerDocuments.DocumentId  where OfficeManagerDocuments.WgId='" + ddlstgfilter.SelectedValue + "' ";
        SqlCommand cmdltgdoc = new SqlCommand(strltgdoc, con);
        SqlDataAdapter adpltgdoc = new SqlDataAdapter(cmdltgdoc);
        DataTable dtdoc = new DataTable();
        adpltgdoc.Fill(dtdoc);
        if (dtdoc.Rows.Count > 0)
        {
            lblltgdocs.Visible = false;
            //Panel2.Visible = true;

            GridView3.DataSource = dtdoc;
            GridView3.DataBind();

        }

    }
    protected void filterfillinstructiondocson()
    {
        string avgmaterial = "  ";
        SqlCommand cmdavgmat = new SqlCommand(avgmaterial, con);
        SqlDataAdapter adpavgmat = new SqlDataAdapter(cmdavgmat);
        DataTable dsavgma = new DataTable();
        adpavgmat.Fill(dsavgma);

        string materialin = "";

        materialin = " where OfficeManagerDocuments.Wdetail In (select DetailId from Wdetail where MasterId='" + ddlstgfilter.SelectedValue + "') ";

        string strltgdetaildoc = "Select  OfficeManagerDocuments.Id,DocumentMaster.DocumentName, DocumentMaster.DocumentId,CONVERT(nvarchar, DocumentMaster.DocumentDate,101) as DocumentDate,DocumentMaster.DocumentTitle  from OfficeManagerDocuments inner join DocumentMaster on DocumentMaster.DocumentId=OfficeManagerDocuments.DocumentId " + materialin + "   ";
        SqlCommand cmdltgdetaildoc = new SqlCommand(strltgdetaildoc, con);
        SqlDataAdapter adpltgdetaildoc = new SqlDataAdapter(cmdltgdetaildoc);
        DataTable dtdetaildoc = new DataTable();
        adpltgdetaildoc.Fill(dtdetaildoc);
        if (dtdetaildoc.Rows.Count > 0)
        {
            lblinstructiondocs.Visible = false;
            //Panel4.Visible = true;

            GridView4.DataSource = dtdetaildoc;
            GridView4.DataBind();

        }

    }
    protected void filterfillevaluationdocson()
    {
        string materialin1 = "";


        materialin1 = " where OfficeManagerDocuments.Wevalution In (select distinct EvaluationId from WEvaluation where MasterId='" + ddlstgfilter.SelectedValue + "') ";

        string strltgeveldoc = "Select  OfficeManagerDocuments.Id,DocumentMaster.DocumentName, DocumentMaster.DocumentId,CONVERT(nvarchar, DocumentMaster.DocumentDate,101) as DocumentDate,DocumentMaster.DocumentTitle  from OfficeManagerDocuments inner join DocumentMaster on DocumentMaster.DocumentId=OfficeManagerDocuments.DocumentId  " + materialin1 + " ";
        SqlCommand cmdltgeveldoc = new SqlCommand(strltgeveldoc, con);
        SqlDataAdapter adpltgeveldoc = new SqlDataAdapter(cmdltgeveldoc);
        DataTable dteveldoc = new DataTable();
        adpltgeveldoc.Fill(dteveldoc);
        if (dteveldoc.Rows.Count > 0)
        {
            lblevaluationdocs.Visible = false;
            //Panel5.Visible = true;

            GridView5.DataSource = dteveldoc;
            GridView5.DataBind();
        }
    }

    protected void ddlExport_SelectedIndexChanged(object sender, EventArgs e)
    {

        //Button1.Text = "Printable Version";
        //Button1_Click(sender, e);
        //Button7.Visible = false;

        if (ddlExport.SelectedValue == "1")
        {
            System.IO.MemoryStream msReport = new System.IO.MemoryStream();

            Document document = new Document(PageSize.A2, 0f, 0f, 30f, 30f);

            PdfWriter writer = PdfWriter.GetInstance(document, msReport);

            this.EnableViewState = false;

            Response.Charset = string.Empty;

            document.AddSubject("Export to PDF");
            document.Open();

            iTextSharp.text.Table datatable4 = new iTextSharp.text.Table(3);

            datatable4.Padding = 2;
            datatable4.Spacing = 1;
            datatable4.Width = 90;

            float[] headerwidths4 = new float[3];

            headerwidths4[0] = 10;
            headerwidths4[1] = 80;
            headerwidths4[2] = 10;


            datatable4.Widths = headerwidths4;

            Cell cell = new Cell(new Phrase("Business Name :" + ddlbusunesswithdept.SelectedItem.Text, FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;

            cell.Colspan = 3;
            cell.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell);

            datatable4.DefaultCellBorderWidth = 1;
            datatable4.DefaultHorizontalAlignment = 1;

            Cell cell3 = new Cell(new Phrase("Weekly Master Report", FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;

            cell3.Colspan = 3;
            cell3.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell3);



            Cell cell31 = new Cell(new Phrase("Title : " + lblltgtitile.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell31.HorizontalAlignment = Element.ALIGN_LEFT;

            cell31.Colspan = 3;
            cell31.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell31);


            Cell cell310 = new Cell(new Phrase("Status : " + lblstatus.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell310.HorizontalAlignment = Element.ALIGN_LEFT;

            cell310.Colspan = 3;
            cell310.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell310);



            Cell cell311 = new Cell(new Phrase("Description : " + lbldescription.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell311.HorizontalAlignment = Element.ALIGN_LEFT;

            cell311.Colspan = 3;
            cell311.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell311);

            Cell cell312 = new Cell(new Phrase("Attachments : " + LinkButton2.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell312.HorizontalAlignment = Element.ALIGN_LEFT;

            cell312.Colspan = 3;
            cell312.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell312);


            Cell cell313 = new Cell(new Phrase("Budgeted Cost : " + lblbudgetedcost.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell313.HorizontalAlignment = Element.ALIGN_LEFT;

            cell313.Colspan = 3;
            cell313.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell313);


            Cell cell314 = new Cell(new Phrase("Actual Cost : " + lblactualcost.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell314.HorizontalAlignment = Element.ALIGN_LEFT;

            cell314.Colspan = 3;
            cell314.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell314);


            Cell cell315 = new Cell(new Phrase("Shortage/Excess : " + lblshortageexcess.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
            cell315.HorizontalAlignment = Element.ALIGN_LEFT;

            cell315.Colspan = 3;
            cell315.Border = Rectangle.NO_BORDER;

            datatable4.AddCell(cell315);


            document.Add(datatable4);


            if (GridView2.Rows.Count > 0)
            {
                iTextSharp.text.Table datatable = new iTextSharp.text.Table(3);
                datatable.Padding = 2;
                datatable.Spacing = 1;
                datatable.Width = 90;

                float[] headerwidths = new float[3];

                headerwidths[0] = 10;
                headerwidths[1] = 80;
                headerwidths[2] = 10;

                datatable.Widths = headerwidths;

                Cell cell1 = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;

                cell1.Colspan = 3;
                cell1.Border = Rectangle.NO_BORDER;

                datatable.AddCell(cell1);

                Cell cell2 = new Cell(new Phrase("Instructions", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                cell2.Colspan = 3;
                cell2.Border = Rectangle.TOP_BORDER;

                datatable.AddCell(cell2);

                datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                datatable.AddCell(new Cell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase("Instruction Note", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase("Docs", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                //datatable.AddCell(new Cell(new Phrase("Shortage/Excess", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));


                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    Label lblmasterdate = (Label)GridView2.Rows[i].FindControl("lblmasterdate");
                    Label lblinstruction123 = (Label)GridView2.Rows[i].FindControl("lblinstruction123");
                    LinkButton LinkButton1 = (LinkButton)GridView2.Rows[i].FindControl("LinkButton1");
                    //Label lShortageExcesste123 = (Label)GridView1.Rows[i].FindControl("lShortageExcesste123");

                    datatable.AddCell(lblmasterdate.Text);
                    datatable.AddCell(lblinstruction123.Text);
                    datatable.AddCell(LinkButton1.Text);
                    //datatable.AddCell(lShortageExcesste123.Text);

                }
                document.Add(datatable);

            }


            if (GridView1.Rows.Count > 0)
            {

                iTextSharp.text.Table datatable1 = new iTextSharp.text.Table(3);

                datatable1.Padding = 2;
                datatable1.Spacing = 1;
                datatable1.Width = 90;

                float[] headerwidths1 = new float[3];

                headerwidths1[0] = 10;
                headerwidths1[1] = 80;
                headerwidths1[2] = 10;


                datatable1.Widths = headerwidths1;

                Cell cell1x = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                cell1x.HorizontalAlignment = Element.ALIGN_LEFT;

                cell1x.Colspan = 3;
                cell1x.Border = Rectangle.NO_BORDER;

                datatable1.AddCell(cell1x);


                Cell cello2 = new Cell(new Phrase("Status Notes", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                cello2.HorizontalAlignment = Element.ALIGN_LEFT;
                cello2.Colspan = 3;
                cello2.Border = Rectangle.TOP_BORDER;

                datatable1.AddCell(cello2);

                datatable1.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                datatable1.AddCell(new Cell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable1.AddCell(new Cell(new Phrase("Status Note", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable1.AddCell(new Cell(new Phrase("Docs", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {

                    Label lblmasterdate1 = (Label)GridView1.Rows[i].FindControl("lblmasterdate1");
                    Label lblevaluationnote123 = (Label)GridView1.Rows[i].FindControl("lblevaluationnote123");
                    LinkButton LinkButton1er = (LinkButton)GridView1.Rows[i].FindControl("LinkButton1er");

                    datatable1.AddCell(lblmasterdate1.Text);
                    datatable1.AddCell(lblevaluationnote123.Text);
                    datatable1.AddCell(LinkButton1er.Text);
                }
                document.Add(datatable1);

            }

            if (GridView6.Rows.Count > 0)
            {

                iTextSharp.text.Table datatable2 = new iTextSharp.text.Table(8);

                datatable2.Padding = 2;
                datatable2.Spacing = 1;
                datatable2.Width = 90;

                float[] headerwidths2 = new float[8];

                headerwidths2[0] = 7;
                headerwidths2[1] = 13;
                headerwidths2[2] = 15;
                headerwidths2[3] = 20;
                headerwidths2[4] = 8;
                headerwidths2[5] = 8;
                headerwidths2[6] = 7;
                headerwidths2[7] = 7;

                datatable2.Widths = headerwidths2;

                Cell cell1x = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                cell1x.HorizontalAlignment = Element.ALIGN_LEFT;

                cell1x.Colspan = 8;
                cell1x.Border = Rectangle.NO_BORDER;

                datatable2.AddCell(cell1x);


                Cell cello2 = new Cell(new Phrase("Task Done", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                cello2.HorizontalAlignment = Element.ALIGN_LEFT;
                cello2.Colspan = 8;
                cello2.Border = Rectangle.TOP_BORDER;

                datatable2.AddCell(cello2);

                datatable2.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                datatable2.AddCell(new Cell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Employee Name", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Task", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Task Report", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Budgeted Minute", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Actual Minute", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Actual Cost", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable2.AddCell(new Cell(new Phrase("Employee Avg Rate", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));


                for (int i = 0; i < GridView6.Rows.Count; i++)
                {

                    Label lblmasterdate2 = (Label)GridView6.Rows[i].FindControl("lblmasterdate2");
                    Label lblempname = (Label)GridView6.Rows[i].FindControl("lblempname");
                    Label lblTask = (Label)GridView6.Rows[i].FindControl("lblTask");
                    Label lblTaskReport = (Label)GridView6.Rows[i].FindControl("lblTaskReport");
                    Label lblbudgetedminute123 = (Label)GridView6.Rows[i].FindControl("lblbudgetedminute123");
                    Label lblunitsused = (Label)GridView6.Rows[i].FindControl("lblunitsused");
                    Label lblactempcost = (Label)GridView6.Rows[i].FindControl("lblactempcost");
                    Label lblavgrate = (Label)GridView6.Rows[i].FindControl("lblavgrate");


                    datatable2.AddCell(lblmasterdate2.Text);
                    datatable2.AddCell(lblempname.Text);
                    datatable2.AddCell(lblTask.Text);
                    datatable2.AddCell(lblTaskReport.Text);
                    datatable2.AddCell(lblbudgetedminute123.Text);
                    datatable2.AddCell(lblunitsused.Text);
                    datatable2.AddCell(lblactempcost.Text);
                    datatable2.AddCell(lblavgrate.Text);

                }
                document.Add(datatable2);
            }

            document.Close();

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(msReport.ToArray());

            Response.End();

        }
        else if (ddlExport.SelectedValue == "2")
        {

            Response.Clear();

            Response.Buffer = true;

            Response.AddHeader("content-disposition",

            "attachment;filename=GridViewExport.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            if (GridView2.Rows.Count > 0)
            {
                GridView2.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    GridViewRow row = GridView2.Rows[i];

                    row.BackColor = System.Drawing.Color.White;

                    row.Attributes.Add("class", "textmode");
                }
            }

            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow row = GridView1.Rows[i];

                    row.BackColor = System.Drawing.Color.White;

                    row.Attributes.Add("class", "textmode");
                }
            }


            if (GridView6.Rows.Count > 0)
            {

                GridView6.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < GridView6.Rows.Count; i++)
                {
                    GridViewRow row = GridView6.Rows[i];

                    row.BackColor = System.Drawing.Color.White;

                    row.Attributes.Add("class", "textmode");
                }
            }

            pnlgrid.RenderControl(hw);

            string style = @"<style> .textmode { mso-number-format:\@; } </style>";

            Response.Write(style);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();
        }
        else if (ddlExport.SelectedValue == "3")
        {
            Response.Clear();

            Response.Buffer = true;

            Response.AddHeader("content-disposition",

            "attachment;filename=GridViewExport.doc");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-word ";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            pnlgrid.RenderControl(hw);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();

        }
        else if (ddlExport.SelectedValue == "4")
        {

            Document document = new Document(PageSize.A2, 0f, 0f, 30f, 30f);
            string filename = "GrdM_" + System.DateTime.Today.Day + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second;
            Session["Emfile"] = filename + ".pdf";
            Session["GrdmailA"] = null;
            PdfWriter.GetInstance(document, new FileStream(HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".pdf"), FileMode.Create));

            try
            {
                document.AddSubject("Export to PDF");
                document.Open();


                iTextSharp.text.Table datatable4 = new iTextSharp.text.Table(3);

                datatable4.Padding = 2;
                datatable4.Spacing = 1;
                datatable4.Width = 90;

                float[] headerwidths4 = new float[3];

                headerwidths4[0] = 10;
                headerwidths4[1] = 80;
                headerwidths4[2] = 10;

                datatable4.Widths = headerwidths4;

                Cell cell = new Cell(new Phrase("Business Name :" + ddlbusunesswithdept.SelectedItem.Text, FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;

                cell.Colspan = 3;
                cell.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell);

                datatable4.DefaultCellBorderWidth = 1;
                datatable4.DefaultHorizontalAlignment = 1;

                Cell cell3 = new Cell(new Phrase("Weekly Master Report", FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;

                cell3.Colspan = 3;
                cell3.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell3);

                Cell cell31 = new Cell(new Phrase("Title : " + lblltgtitile.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell31.HorizontalAlignment = Element.ALIGN_LEFT;

                cell31.Colspan = 3;
                cell31.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell31);

                Cell cell310 = new Cell(new Phrase("Status : " + lblstatus.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell310.HorizontalAlignment = Element.ALIGN_LEFT;

                cell310.Colspan = 3;
                cell310.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell310);

                Cell cell311 = new Cell(new Phrase("Description : " + lbldescription.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell311.HorizontalAlignment = Element.ALIGN_LEFT;

                cell311.Colspan = 3;
                cell311.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell311);

                Cell cell312 = new Cell(new Phrase("Attachments : " + LinkButton2.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell312.HorizontalAlignment = Element.ALIGN_LEFT;

                cell312.Colspan = 3;
                cell312.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell312);


                Cell cell313 = new Cell(new Phrase("Budgeted Cost : " + lblbudgetedcost.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell313.HorizontalAlignment = Element.ALIGN_LEFT;

                cell313.Colspan = 3;
                cell313.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell313);


                Cell cell314 = new Cell(new Phrase("Actual Cost : " + lblactualcost.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell314.HorizontalAlignment = Element.ALIGN_LEFT;

                cell314.Colspan = 3;
                cell314.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell314);


                Cell cell315 = new Cell(new Phrase("Shortage/Excess : " + lblshortageexcess.Text, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
                cell315.HorizontalAlignment = Element.ALIGN_LEFT;

                cell315.Colspan = 3;
                cell315.Border = Rectangle.NO_BORDER;

                datatable4.AddCell(cell315);

                document.Add(datatable4);

                if (GridView2.Rows.Count > 0)
                {
                    iTextSharp.text.Table datatable = new iTextSharp.text.Table(3);
                    datatable.Padding = 2;
                    datatable.Spacing = 1;
                    datatable.Width = 90;

                    float[] headerwidths = new float[3];

                    headerwidths[0] = 10;
                    headerwidths[1] = 80;
                    headerwidths[2] = 10;

                    datatable.Widths = headerwidths;

                    Cell cell1 = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell1.Colspan = 3;
                    cell1.Border = Rectangle.NO_BORDER;

                    datatable.AddCell(cell1);

                    Cell cell2 = new Cell(new Phrase("Instructions", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                    cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell2.Colspan = 3;
                    cell2.Border = Rectangle.TOP_BORDER;

                    datatable.AddCell(cell2);

                    datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                    datatable.AddCell(new Cell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable.AddCell(new Cell(new Phrase("Instruction Note", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable.AddCell(new Cell(new Phrase("Docs", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    //datatable.AddCell(new Cell(new Phrase("Shortage/Excess", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));


                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        Label lblmasterdate = (Label)GridView2.Rows[i].FindControl("lblmasterdate");
                        Label lblinstruction123 = (Label)GridView2.Rows[i].FindControl("lblinstruction123");
                        LinkButton LinkButton1 = (LinkButton)GridView2.Rows[i].FindControl("LinkButton1");
                        //Label lShortageExcesste123 = (Label)GridView1.Rows[i].FindControl("lShortageExcesste123");

                        datatable.AddCell(lblmasterdate.Text);
                        datatable.AddCell(lblinstruction123.Text);
                        datatable.AddCell(LinkButton1.Text);
                        //datatable.AddCell(lShortageExcesste123.Text);

                    }
                    document.Add(datatable);
                }

                if (GridView1.Rows.Count > 0)
                {

                    iTextSharp.text.Table datatable1 = new iTextSharp.text.Table(3);

                    datatable1.Padding = 2;
                    datatable1.Spacing = 1;
                    datatable1.Width = 90;

                    float[] headerwidths1 = new float[3];

                    headerwidths1[0] = 10;
                    headerwidths1[1] = 80;
                    headerwidths1[2] = 10;


                    datatable1.Widths = headerwidths1;

                    Cell cell1x = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                    cell1x.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell1x.Colspan = 3;
                    cell1x.Border = Rectangle.NO_BORDER;

                    datatable1.AddCell(cell1x);


                    Cell cello2 = new Cell(new Phrase("Status Notes", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                    cello2.HorizontalAlignment = Element.ALIGN_LEFT;
                    cello2.Colspan = 3;
                    cello2.Border = Rectangle.TOP_BORDER;

                    datatable1.AddCell(cello2);

                    datatable1.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                    datatable1.AddCell(new Cell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable1.AddCell(new Cell(new Phrase("Status Note", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable1.AddCell(new Cell(new Phrase("Docs", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {

                        Label lblmasterdate1 = (Label)GridView1.Rows[i].FindControl("lblmasterdate1");
                        Label lblevaluationnote123 = (Label)GridView1.Rows[i].FindControl("lblevaluationnote123");
                        LinkButton LinkButton1er = (LinkButton)GridView1.Rows[i].FindControl("LinkButton1er");

                        datatable1.AddCell(lblmasterdate1.Text);
                        datatable1.AddCell(lblevaluationnote123.Text);
                        datatable1.AddCell(LinkButton1er.Text);
                    }
                    document.Add(datatable1);
                }

                if (GridView6.Rows.Count > 0)
                {

                    iTextSharp.text.Table datatable2 = new iTextSharp.text.Table(8);

                    datatable2.Padding = 2;
                    datatable2.Spacing = 1;
                    datatable2.Width = 90;

                    float[] headerwidths2 = new float[8];

                    headerwidths2[0] = 7;
                    headerwidths2[1] = 13;
                    headerwidths2[2] = 15;
                    headerwidths2[3] = 20;
                    headerwidths2[4] = 8;
                    headerwidths2[5] = 8;
                    headerwidths2[6] = 7;
                    headerwidths2[7] = 7;

                    datatable2.Widths = headerwidths2;

                    Cell cell1x = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                    cell1x.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell1x.Colspan = 8;
                    cell1x.Border = Rectangle.NO_BORDER;

                    datatable2.AddCell(cell1x);


                    Cell cello2 = new Cell(new Phrase("Task Done", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                    cello2.HorizontalAlignment = Element.ALIGN_LEFT;
                    cello2.Colspan = 8;
                    cello2.Border = Rectangle.TOP_BORDER;

                    datatable2.AddCell(cello2);

                    datatable2.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                    datatable2.AddCell(new Cell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Employee Name", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Task", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Task Report", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Budgeted Minute", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Actual Minute", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Actual Cost", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable2.AddCell(new Cell(new Phrase("Employee Avg Rate", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));


                    for (int i = 0; i < GridView6.Rows.Count; i++)
                    {

                        Label lblmasterdate2 = (Label)GridView6.Rows[i].FindControl("lblmasterdate2");
                        Label lblempname = (Label)GridView6.Rows[i].FindControl("lblempname");
                        Label lblTask = (Label)GridView6.Rows[i].FindControl("lblTask");
                        Label lblTaskReport = (Label)GridView6.Rows[i].FindControl("lblTaskReport");
                        Label lblbudgetedminute123 = (Label)GridView6.Rows[i].FindControl("lblbudgetedminute123");
                        Label lblunitsused = (Label)GridView6.Rows[i].FindControl("lblunitsused");
                        Label lblactempcost = (Label)GridView6.Rows[i].FindControl("lblactempcost");
                        Label lblavgrate = (Label)GridView6.Rows[i].FindControl("lblavgrate");


                        datatable2.AddCell(lblmasterdate2.Text);
                        datatable2.AddCell(lblempname.Text);
                        datatable2.AddCell(lblTask.Text);
                        datatable2.AddCell(lblTaskReport.Text);
                        datatable2.AddCell(lblbudgetedminute123.Text);
                        datatable2.AddCell(lblunitsused.Text);
                        datatable2.AddCell(lblactempcost.Text);
                        datatable2.AddCell(lblavgrate.Text);

                    }
                    document.Add(datatable2);
                }

                document.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
            document.Close();
            string te = "MessageComposeExt.aspx?ema=Azxcvyute";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }

        //}

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected DataTable selectbcon(string str)
    {
        SqlCommand cmd = new SqlCommand(str, PageConn.licenseconn());
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;

    }
    protected void pageMailAccess()
    {
        ddlExport.Items.Insert(0, "Export Type");
        ddlExport.Items[0].Value = "0";
        ddlExport.Items.Insert(1, "Export to PDF");
        ddlExport.Items[1].Value = "1";
        ddlExport.Items.Insert(2, "Export to Excel");
        ddlExport.Items[2].Value = "2";
        ddlExport.Items.Insert(3, "Export to Word");
        ddlExport.Items[3].Value = "3";


        string avfr = "  and PageMaster.PageName='" + ClsEncDesc.EncDyn("MessageCompose.aspx") + "'";
        DataTable drt = selectbcon("SELECT distinct " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId,PageMaster.PageName FROM MainMenuMaster inner join " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=" + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'" + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");
        if (drt.Rows.Count <= 0)
        {

            drt = selectbcon("SELECT PageMaster.PageName FROM PageMaster inner join " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl on " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PageId=PageMaster.PageId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' " + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");
            if (drt.Rows.Count <= 0)
            {
                drt = selectbcon("SELECT distinct PageMaster.PageName FROM MainMenuMaster inner join  SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId inner join PageMaster on PageMaster.SubMenuId=" + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'" + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");


                if (drt.Rows.Count <= 0)
                {


                }
                else
                {
                    ddlExport.Items.Insert(4, "Email with PDF");
                    ddlExport.Items[4].Value = "4";
                }

            }
            else
            {
                ddlExport.Items.Insert(4, "Email with PDF");
                ddlExport.Items[4].Value = "4";

            }


        }
        else
        {

            ddlExport.Items.Insert(4, "Email with PDF");
            ddlExport.Items[4].Value = "4";

        }
    }

    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Send")
        {
            ViewState["DetailId"] = Convert.ToInt32(e.CommandArgument);
            DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["DetailId"]), 61);
            GridView4.DataSource = dtcrNew11;
            GridView4.DataBind();

            ModalPopupExtenderAddnew.Show();
        }
    }
    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Send")
        {
            ViewState["MEvaluation"] = Convert.ToInt32(e.CommandArgument);

            DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["MEvaluation"]), 62);

            GridView5.DataSource = dtcrNew11;
            GridView5.DataBind();
            ModalPopupExtender1.Show();
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        if (LinkButton2.Text == "0")
        {
            GridView3.DataSource = null;
            GridView3.DataBind();
        }
        else
        {
            ViewState["MissionId"] = ViewState["att"];

            DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["MissionId"]), 6);

            GridView3.DataSource = dtcrNew11;
            GridView3.DataBind();
        }
        ModalPopupExtender2.Show();
    }

    protected void fillattach1()
    {
        DataTable dtcrNew11 = ClsWDetail.SelectOfficeManagerDocumentsforWID(Convert.ToString(Request.QueryString["Wid"]));

        LinkButton2.Text = dtcrNew11.Rows[0]["DocumentCount"].ToString();

        ViewState["att"] = Convert.ToString(Request.QueryString["Wid"].ToString());
    }

    protected void fillattach123()
    {
        DataTable dtcrNew11 = new DataTable();

        if (ddlstgfilter.SelectedIndex != -1)
        {
            dtcrNew11 = ClsWDetail.SelectOfficeManagerDocumentsforWID(Convert.ToString(ddlstgfilter.SelectedValue));

            LinkButton2.Text = dtcrNew11.Rows[0]["DocumentCount"].ToString();

            ViewState["att"] = Convert.ToString(ddlstgfilter.SelectedValue);
        }

        else
        {
            ViewState["att"] = "0";

            LinkButton2.Text = "0";
        }
    }

    protected void fillbusinessmonth()
    {
        ddlfilterltg.Items.Clear();

        string y11 = "";

        if (ddlbusunesswithdept.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.businessid='" + ddlbusunesswithdept.SelectedValue + "' and YMaster.DepartmentId IS NULL and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL";

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlfilterltg.DataSource = dt;
            ddlfilterltg.DataTextField = "Title1";
            ddlfilterltg.DataValueField = "MasterId";
            ddlfilterltg.DataBind();

        }
    }
    protected void ddldeptgoal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldeptgoal.SelectedValue == "0")
        {
            filldepartmentmonth();
            Label6.Text = "Department Monthly Goal";
        }
        if (ddldeptgoal.SelectedValue == "1")
        {
            fillbusinessweek();
            Label6.Text = "Business Weekly Goal";
        }
        ddlfilterbymission_SelectedIndexChanged(sender, e);
    }


    protected void filldepartmentmonth()
    {
        ddlfilterltg.Items.Clear();

        string y11 = "";

        if (ddlbusunesswithdept.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join year on year.id=Month.yid  where MMaster.DepartmentId='" + ddlbusunesswithdept.SelectedValue + "' and MMaster.divisionid IS NULL and MMaster.EmployeeId IS NULL";

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlfilterltg.DataSource = dt;
            ddlfilterltg.DataTextField = "Title1";
            ddlfilterltg.DataValueField = "MasterId";
            ddlfilterltg.DataBind();
        }
    }

    protected void fillbusinessweek()
    {
        ddlfilterltg.Items.Clear();

        string y11 = "";

        if (ddlbusunesswithdept.SelectedIndex > -1)
        {
            string deped = "select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlbusunesswithdept.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and YMaster.Departmentid IS NULL and YMaster.Divisionid IS NULL and YMaster.Employeeid IS NULL";

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlfilterltg.DataSource = dt;
            ddlfilterltg.DataTextField = "Title1";
            ddlfilterltg.DataValueField = "MasterId";
            ddlfilterltg.DataBind();
        }
    }

    protected void filldivisionmonth()
    {
        ddlfilterltg.Items.Clear();

        string y11 = "";

        if (ddlbusunesswithdept.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join year on year.id=Month.Yid  where MMaster.DivisionID='" + ddlbusunesswithdept.SelectedValue + "' and MMaster.EmployeeId IS NULL";

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlfilterltg.DataSource = dt;
            ddlfilterltg.DataTextField = "Title1";
            ddlfilterltg.DataValueField = "MasterId";
            ddlfilterltg.DataBind();

        }
    }

    protected void fillbusinessweek11()
    {
        ddlfilterltg.Items.Clear();

        string y11 = "";

        if (ddlbusunesswithdept.SelectedIndex > -1)
        {
            string deped = "select DepartmentmasterMNC.id,WareHouseMaster.WareHouseId from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlbusunesswithdept.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where WMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "'";

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlfilterltg.DataSource = dt;
            ddlfilterltg.DataTextField = "Title1";
            ddlfilterltg.DataValueField = "MasterId";
            ddlfilterltg.DataBind();
        }
    }

    protected void filldepartmentweek11()
    {
        ddlfilterltg.Items.Clear();

        string y11 = "";

        if (ddlbusunesswithdept.SelectedIndex > -1)
        {
            string deped = "select DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlbusunesswithdept.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);


            y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + dt3.Rows[0]["id"].ToString() + "' and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL";

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlfilterltg.DataSource = dt;
            ddlfilterltg.DataTextField = "Title1";
            ddlfilterltg.DataValueField = "MasterId";
            ddlfilterltg.DataBind();
        }

    }

    protected void ddldivigoal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivigoal.SelectedValue == "0")
        {
            filldivisionmonth();
            Label6.Text = "Division Monthly Goal";
        }
        if (ddldivigoal.SelectedValue == "1")
        {
            fillbusinessweek11();
            Label6.Text = "Business Weekly Goal";
        }
        if (ddldivigoal.SelectedValue == "2")
        {
            filldepartmentweek11();
            Label6.Text = "Department Weekly Goal";
        }

        ddlfilterbymission_SelectedIndexChanged(sender, e);
    }

    protected void fillemployeemonth()
    {
        ddlfilterltg.Items.Clear();

        string y11 = "";

        if (ddlbusunesswithdept.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join year on year.id=MONTH.yid where MMaster.EmployeeId='" + ddlbusunesswithdept.SelectedValue + "'";


            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlfilterltg.DataSource = dt;
            ddlfilterltg.DataTextField = "Title1";
            ddlfilterltg.DataValueField = "MasterId";
            ddlfilterltg.DataBind();

        }
    }

    protected void fillbusinessweek56()
    {
        ddlfilterltg.Items.Clear();

        string y11 = "";

        if (ddlbusunesswithdept.SelectedIndex > -1)
        {
            string deped = "select WareHouseMaster.WareHouseId from  employeemaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=employeemaster.Whid where employeemaster.employeemasterid='" + ddlbusunesswithdept.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and YMaster.Departmentid IS NULL and YMaster.Divisionid IS NULL and YMaster.Employeeid IS NULL";

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlfilterltg.DataSource = dt;
            ddlfilterltg.DataTextField = "Title1";
            ddlfilterltg.DataValueField = "MasterId";
            ddlfilterltg.DataBind();
        }
    }

    protected void filldepartmentweek()
    {
        ddlfilterltg.Items.Clear();

        string y11 = "";

        if (ddlbusunesswithdept.SelectedIndex > -1)
        {
            string deped = "select employeemaster.DeptID as departmentid from employeemaster where employeemasterid='" + ddlbusunesswithdept.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {

                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + dt3.Rows[0]["departmentid"].ToString() + "' and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL";

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlfilterltg.DataSource = dt;
                ddlfilterltg.DataTextField = "Title1";
                ddlfilterltg.DataValueField = "MasterId";
                ddlfilterltg.DataBind();
            }
        }
    }

    protected void filldivisionweek()
    {
        ddlfilterltg.Items.Clear();

        string y11 = "";

        if (ddlbusunesswithdept.SelectedIndex > -1)
        {
            string deped = "select employeemaster.DeptID as departmentid from employeemaster where employeemasterid='" + ddlbusunesswithdept.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner join Year on Month.Yid = Year.Id where WMaster.departmentid='" + dt3.Rows[0]["departmentid"].ToString() + "' and WMaster.DivisionID IS NOT NULL and WMaster.Employeeid IS NULL";

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlfilterltg.DataSource = dt;
                ddlfilterltg.DataTextField = "Title1";
                ddlfilterltg.DataValueField = "MasterId";
                ddlfilterltg.DataBind();
            }
        }
    }


    protected void ddlempgoal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlempgoal.SelectedValue == "0")
        {
            fillemployeemonth();
            Label6.Text = "Employee Monthly Goal";
        }
        if (ddlempgoal.SelectedValue == "1")
        {
            fillbusinessweek56();
            Label6.Text = "Business Weekly Goal";
        }
        if (ddlempgoal.SelectedValue == "2")
        {
            filldepartmentweek();
            Label6.Text = "Department Weekly Goal";
        }
        if (ddlempgoal.SelectedValue == "3")
        {
            filldivisionweek();
            Label6.Text = "Division Weekly Goal";
        }

        ddlfilterbymission_SelectedIndexChanged(sender, e);
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        filterfillinstructionon();

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        filterfillevaluationon();
    }
    protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView6.PageIndex = e.NewPageIndex;

        if (DropDownList1.SelectedValue == "0")
        {
            fillbusinesstask();
        }
        if (DropDownList1.SelectedValue == "1")
        {
            filldepartmenttask();
        }
        if (DropDownList1.SelectedValue == "2")
        {
            filldivisiontask();
        }
        if (DropDownList1.SelectedValue == "3")
        {
            fillemptask();
        }
    }
}
