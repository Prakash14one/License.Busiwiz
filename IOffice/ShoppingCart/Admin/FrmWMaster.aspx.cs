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
public partial class Admin_FrmWMaster : System.Web.UI.Page
{
    static int temp;
    static DataTable dt;
    DataByCompany obj = new DataByCompany();
    DocumentCls1 clsDocument = new DocumentCls1();
    SqlConnection con;
    DateTime lastday;
    string currentmonth = System.DateTime.Now.Month.ToString();
    string currentdate = System.DateTime.Now.ToShortDateString();
    string currentyear = System.DateTime.Now.Year.ToString();
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        pagetitleclass pg = new pagetitleclass();


        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        compid = Session["Comid"].ToString();
        string[] strSplitArr = strData.Split(separator);

        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!IsPostBack)
        {
            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }


            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["Comid"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                ViewState["sortOrder"] = "";



                fillstore();


                filyear();
                RadioButtonList1_SelectedIndexChanged(sender, e);
                fillteryear();
                RadioButtonList2_SelectedIndexChanged(sender, e);
            }


            //ddlweek.DataSource = obj.Tablemaster("Select Week.Id, Year.Name+ ' -> ' + Month.Name+ ' -> ' + Week.Name AS yeayrmonth from  dbo.Week inner join  dbo.Month on dbo.Month.Id=dbo.Week.Mid INNER JOIN dbo.Year ON dbo.Month.Yid = dbo.Year.Id Order by Year.Name,Cast(Month.Name as nvarchar(50)),Cast(Week.Name as nvarchar(50)) ");
            //ddlweek.DataMember = "yeayrmonth";
            //ddlweek.DataTextField = "yeayrmonth";
            //ddlweek.DataValueField = "Id";
            //ddlweek.DataBind();
            //ddlweek.Items.Insert(0, "Select");
            //ddlweek.Items[0].Value = "0";       


        }

    }





    protected void btnsubmit_Click(object sender, EventArgs e)
    {

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


        int currentyear = Convert.ToInt32(System.DateTime.Now.Year);
        int currentmonth = Convert.ToInt32(System.DateTime.Now.Month);

        int noofdays = DateTime.DaysInMonth(currentyear, currentmonth);

        lastday = new DateTime(currentyear, currentmonth, noofdays);

        ViewState["lastday"] = lastday;
        string startdate = System.DateTime.Now.ToShortDateString();
        string enddate = lastday.ToShortDateString();



        string insert1 = "";
        string insertproject = "";
        int success = 0;

        //   int success = ClsWMaster.SpWMasterAddData(Convert.ToString(ddly.SelectedValue), txttitle.Text.ToString(), txtdescription.Text.ToString(), Convert.ToString(ddlweek.SelectedValue), txtbudgetedamount.Text);
        // Response.Write(success);

        if (RadioButtonList1.SelectedValue == "0")
        {
            insert1 = "insert into WMaster (BusinessID,title,mmasterid,description,week,budgetedcost,StatusId) values('" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(DropDownList5.SelectedValue) + "','" + txtbudgetedamount.Text + "',192)";

            if (CheckBox1.Checked == true)
            {
                insertproject = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txttitle.Text + "', 'Pending', '" + startdate + " ','" + enddate + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + Convert.ToString(em) + "','0','" + ddlStore.SelectedValue + "','false', '" + part + "',1)";
            }
        
        }

        if (RadioButtonList1.SelectedValue == "1")
        {
            int int1 = Convert.ToInt32(ddlStore.SelectedValue);
            SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlStore.SelectedValue + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (RadioButtonList3.SelectedValue == "0")
            {
                insert1 = "insert into WMaster (BusinessID,DepartmentID,title,mmasterid,description,week,budgetedcost,StatusId) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(DropDownList5.SelectedValue) + "','" + txtbudgetedamount.Text + "',192)";
            }
            if (RadioButtonList3.SelectedValue == "1")
            {
                insert1 = "insert into WMaster (BusinessID,DepartmentID,title,parentmonthlygoalid,description,week,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddlbusimonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(DropDownList5.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Busi')";
            }
            if (CheckBox1.Checked == true)
            {
                insertproject = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txttitle.Text + "', 'Pending', '" + startdate + " ','" + enddate + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + Convert.ToString(em) + "','" + ddlStore.SelectedValue + "','" + dt1.Rows[0]["WareHouseId"].ToString() + "','false', '" + part + "',1)";
            }
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            int int1 = Convert.ToInt32(ddlStore.SelectedValue);
            SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId,DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlStore.SelectedValue + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (RadioButtonList4.SelectedValue == "0")
            {
                insert1 = "insert into WMaster (BusinessID,DepartmentID,Divisionid,title,mmasterid,description,week,budgetedcost,StatusId) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + dt1.Rows[0]["id"].ToString() + "','" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(DropDownList5.SelectedValue) + "','" + txtbudgetedamount.Text + "',192)";
            }
            if (RadioButtonList4.SelectedValue == "1")
            {
                if (DropDownList6.SelectedValue == "0")
                {
                    insert1 = "insert into WMaster (BusinessID,DepartmentID,Divisionid,title,parentmonthlygoalid,description,week,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + dt1.Rows[0]["id"].ToString() + "','" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddlbusimonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(DropDownList5.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Busi')";
                }
                if (DropDownList6.SelectedValue == "1")
                {
                    insert1 = "insert into WMaster (BusinessID,DepartmentID,Divisionid,title,parentmonthlygoalid,description,week,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + dt1.Rows[0]["id"].ToString() + "','" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddldeptmonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(DropDownList5.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Dept')";
                }
            }

            if (CheckBox1.Checked == true)
            {
                insertproject = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txttitle.Text + "', 'Pending', '" + startdate + " ','" + enddate + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + Convert.ToString(em) + "','" + dt1.Rows[0]["id"].ToString() + "','" + dt1.Rows[0]["WareHouseId"].ToString() + "','false', '" + part + "',1)";
            }

        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            int int1 = Convert.ToInt32(ddlStore.SelectedValue);
            SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlStore.SelectedValue + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (RadioButtonList5.SelectedValue == "0")
            {
                insert1 = "insert into WMaster (BusinessID,DepartmentID,Divisionid,Employeeid,title,mmasterid,description,week,budgetedcost,StatusId) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "',0,'" + ddlemployee.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(DropDownList5.SelectedValue) + "','" + txtbudgetedamount.Text + "',192)";
            }
            if (RadioButtonList5.SelectedValue == "1")
            {
                if (DropDownList7.SelectedValue == "0")
                {
                    insert1 = "insert into WMaster (BusinessID,DepartmentID,Divisionid,Employeeid,title,parentmonthlygoalid,description,week,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "',0,'" + ddlemployee.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddlbusimonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(DropDownList5.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Busi')";
                }
                if (DropDownList7.SelectedValue == "1")
                {
                    insert1 = "insert into WMaster (BusinessID,DepartmentID,Divisionid,Employeeid,title,parentmonthlygoalid,description,week,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "',0,'" + ddlemployee.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddldeptmonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(DropDownList5.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Dept')";
                }
                if (DropDownList7.SelectedValue == "2")
                {
                    insert1 = "insert into WMaster (BusinessID,DepartmentID,Divisionid,Employeeid,title,parentmonthlygoalid,description,week,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "',0,'" + ddlemployee.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddldivimonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(DropDownList5.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Divi')";
                }
            }


            if (CheckBox1.Checked == true)
            {
                insertproject = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txttitle.Text + "', 'Pending', '" + startdate + " ','" + enddate + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + ddlemployee.SelectedValue + "','" + ddlStore.SelectedValue + "','" + dt1.Rows[0]["WareHouseId"].ToString() + "','false', '" + part + "',1)";
                SqlCommand cmd1 = new SqlCommand(insertproject, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();
            
            
            }


        }


        SqlCommand cmd = new SqlCommand(insert1, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();



       



        SqlDataAdapter daf = new SqlDataAdapter("select max(MasterId) as MasterId from WMaster", con);
        DataTable dtf = new DataTable();
        daf.Fill(dtf);

        if (dtf.Rows.Count > 0)
        {
            success = Convert.ToInt32(dtf.Rows[0]["MasterId"]);
        }

        if (success > 0)
        {
            statuslable.Text = "Record inserted successfully";
            if (chk.Checked == true)
            {

                string whid = "";
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    whid = ddlStore.SelectedValue;
                }
                else
                {
                    DataTable df = MainAcocount.SelectWhidwithdeptid(ddlStore.SelectedValue);
                    whid = Convert.ToString(df.Rows[0]["Whid"]);

                }
                string te = "AddDocMaster.aspx?Wem=" + success + "&storeid=" + whid + "";



                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


            }
            BindGrid();
        }
        else if (success == 0)
        {
            statuslable.Text = "Record already existed";
        }
        else
        {
            statuslable.Text = "Record not inserted successfully";
        }
        EmptyControls();
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        //     lbllegend.Text = "";
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        int success = 0;

        if (RadioButtonList1.SelectedValue == "0")
        {
            string update1 = " update WMaster set BusinessID='" + ddlStore.SelectedValue + "',title='" + txttitle.Text + "',mmasterid='" + Convert.ToString(ddly.SelectedValue) + "',description='" + txtdescription.Text + "',week='" + Convert.ToString(DropDownList5.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "' where masterid='" + ViewState["updateid"].ToString() + "'";

            SqlCommand cmd = new SqlCommand(update1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            success = Convert.ToInt32(ViewState["updateid"].ToString());
        }

        //if (RadioButtonList1.SelectedValue == "0")
        //{

        //    success = ClsWMaster.SpWMasterUpdateData(hdnid.Value, Convert.ToString(ddly.SelectedValue), txttitle.Text.ToString(), txtdescription.Text.ToString(), Convert.ToString(DropDownList5.SelectedValue), txtbudgetedamount.Text);
        //}

        if (RadioButtonList1.SelectedValue == "1")
        {
            string update1 = "";

            int int1 = Convert.ToInt32(ddlStore.SelectedValue);
            SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlStore.SelectedValue + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (RadioButtonList3.SelectedValue == "0")
            {
                update1 = " update WMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "',title='" + txttitle.Text + "',mmasterid='" + Convert.ToString(ddly.SelectedValue) + "',description='" + txtdescription.Text + "',week='" + Convert.ToString(DropDownList5.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "' where masterid='" + ViewState["updateid"].ToString() + "'";
            }
            if (RadioButtonList3.SelectedValue == "1")
            {
                update1 = " update WMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "',title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddlbusimonthly.SelectedValue) + "',description='" + txtdescription.Text + "',week='" + Convert.ToString(DropDownList5.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "' where masterid='" + ViewState["updateid"].ToString() + "'";
            }

            SqlCommand cmd = new SqlCommand(update1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            success = Convert.ToInt32(ViewState["updateid"].ToString());
        }

        if (RadioButtonList1.SelectedValue == "2")
        {
            string update1 = "";

            int int1 = Convert.ToInt32(ddlStore.SelectedValue);
            SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId,DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlStore.SelectedValue + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (RadioButtonList4.SelectedValue == "0")
            {
                update1 = " update WMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + dt1.Rows[0]["id"].ToString() + "',Divisionid='" + ddlStore.SelectedValue + "',title='" + txttitle.Text + "',MMasterId='" + Convert.ToString(ddly.SelectedValue) + "',description='" + txtdescription.Text + "',week='" + Convert.ToString(DropDownList5.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "' where masterid='" + ViewState["updateid"].ToString() + "'";
            }
            if (RadioButtonList4.SelectedValue == "1")
            {
                if (DropDownList6.SelectedValue == "0")
                {
                    update1 = " update WMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + dt1.Rows[0]["id"].ToString() + "',Divisionid='" + ddlStore.SelectedValue + "',title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddlbusimonthly.SelectedValue) + "',description='" + txtdescription.Text + "',week='" + Convert.ToString(DropDownList5.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "',TypeofMonthlyGoal='Busi' where masterid='" + ViewState["updateid"].ToString() + "'";
                }
                if (DropDownList6.SelectedValue == "1")
                {
                    update1 = " update WMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + dt1.Rows[0]["id"].ToString() + "',Divisionid='" + ddlStore.SelectedValue + "',title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddldeptmonthly.SelectedValue) + "',description='" + txtdescription.Text + "',week='" + Convert.ToString(DropDownList5.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "',TypeofMonthlyGoal='Dept' where masterid='" + ViewState["updateid"].ToString() + "'";
                }
            }

            SqlCommand cmd = new SqlCommand(update1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            success = Convert.ToInt32(ViewState["updateid"].ToString());
        }

        if (RadioButtonList1.SelectedValue == "3")
        {
            string update1 = "";

            int int1 = Convert.ToInt32(ddlStore.SelectedValue);
            SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlStore.SelectedValue + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (RadioButtonList5.SelectedValue == "0")
            {
                update1 = " update WMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "',Employeeid='" + ddlemployee.SelectedValue + "',title='" + txttitle.Text + "',MMasterId='" + Convert.ToString(ddly.SelectedValue) + "',description='" + txtdescription.Text + "',week='" + Convert.ToString(DropDownList5.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "' where masterid='" + ViewState["updateid"].ToString() + "'";
            }
            if (RadioButtonList5.SelectedValue == "1")
            {
                if (DropDownList7.SelectedValue == "0")
                {
                    update1 = " update WMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "',Employeeid='" + ddlemployee.SelectedValue + "',title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddlbusimonthly.SelectedValue) + "',description='" + txtdescription.Text + "',week='" + Convert.ToString(DropDownList5.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "',TypeofMonthlyGoal='Busi' where masterid='" + ViewState["updateid"].ToString() + "'";
                }
                if (DropDownList7.SelectedValue == "1")
                {
                    update1 = " update WMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "',Employeeid='" + ddlemployee.SelectedValue + "',title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddldeptmonthly.SelectedValue) + "',description='" + txtdescription.Text + "',week='" + Convert.ToString(DropDownList5.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "',TypeofMonthlyGoal='Dept' where masterid='" + ViewState["updateid"].ToString() + "'";
                }
                if (DropDownList7.SelectedValue == "2")
                {
                    update1 = " update WMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "',Employeeid='" + ddlemployee.SelectedValue + "',title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddldivimonthly.SelectedValue) + "',description='" + txtdescription.Text + "',week='" + Convert.ToString(DropDownList5.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "',TypeofMonthlyGoal='Divi' where masterid='" + ViewState["updateid"].ToString() + "'";
                }
            }
            SqlCommand cmd = new SqlCommand(update1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            success = Convert.ToInt32(ViewState["updateid"].ToString());
        }

        if (success > 0)
        {
            if (chk.Checked == true)
            {

                string whid = "";
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    whid = ddlStore.SelectedValue;
                }
                else
                {
                    DataTable df = MainAcocount.SelectWhidwithdeptid(ddlStore.SelectedValue);
                    whid = Convert.ToString(df.Rows[0]["Whid"]);

                }
                string te = "AddDocMaster.aspx?wem=" + hdnid.Value + "&storeid=" + whid + "";



                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


            }
            statuslable.Text = "Record updated successfully";
            BindGrid();
        }
        else
        {
            statuslable.Text = "Record already existed";
        }
        EmptyControls();

        btncancel.Visible = true;
        btnupdate.Visible = false;

        btnsubmit.Visible = true;
        btnreset.Visible = false;
        Pnladdnew.Visible = false;
        btnadd.Visible = true;

        //   lbllegend.Text = "";
    }


    protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //store selected row id into id
        //string id = grid.DataKeys[e.NewSelectedIndex].Value.ToString();

        ////create object of structure


        //StWMaster st;
        ////give data source to object of structure
        //st = ClsWMaster.SpWMasterGetDataStructById(id);




        //hdnid.Value = st.masterid.ToString();
        //ddlbusiness.Items.Clear();
        //ddlemployee.Items.Clear();
        //pnlemployee.Visible = false;
        //pnldivision.Visible = false;

        //if (Convert.ToInt32(st.departmentId) > 0)
        //{
        //    lblwname0.Text = "Business-Department Name : ";
        //    RadioButtonList1.SelectedIndex = 1;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        fillDepartment();
        //    }

        //    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue((st.departmentId).ToString()));

        //}
        //else
        //{
        //    RadioButtonList1.SelectedIndex = 0;
        //    lblwname0.Text = "Business Name : ";
        //    if (Convert.ToString(ViewState["cd"]) != "1")
        //    {
        //        fillstore();
        //    }
        //    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(st.Whid.ToString()));

        //}


        //if ((st.employeeid.ToString() != "0"))
        //{
        //    fillemployee();
        //    pnlemployee.Visible = true;
        //    RadioButtonList1.SelectedIndex = 3;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        fillDepartment();
        //    }
        //    fillemployee();

        //    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(st.employeeid.ToString()));
        //    // ddlEmp_SelectedIndexChanged(sender, e);
        //}
        //else if (Convert.ToString(st.businessid) != "0")
        //{
        //    pnldivision.Visible = true;
        //    // PnlDp.Visible = false;
        //    RadioButtonList1.SelectedIndex = 2;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        fillDepartment();
        //    }

        //    fillDivision();
        //    ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(st.businessid.ToString()));
        //    ddlbusiness_SelectedIndexChanged(sender, e);
        //}
        //if (Convert.ToString(st.description) != "")
        //{
        //    Button2.Checked = true;
        //    Pnl1.Visible = true;
        //    txtdescription.Text = st.description.ToString();
        //}
        //else
        //{
        //    Button2.Checked = false;
        //    Pnl1.Visible = false;
        //    txtdescription.Text = "";
        //}


        //filltgmain();
        //ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(st.mmasterid.ToString()));

        //txttitle.Text = st.title.ToString();

        //ddly_SelectedIndexChanged(sender, e);


        //txtdescription.Text = st.description.ToString();
        //ddlweek.SelectedIndex =ddlweek.Items.IndexOf(ddlweek.Items.FindByValue(st.week.ToString()));
        //txtbudgetedamount.Text = st.budgetedcost.ToString();

        //btncancel.Visible = true;
        //btnupdate.Visible = true;

        //btnsubmit.Visible = false;
        //btnreset.Visible = false;
        //Pnladdnew.Visible = true;
        //btnadd.Visible = false;

    }

    // DELETE DATA FROM BRANDMASTER BY BID[PRIMARY KEY]
    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string id = grid.DataKeys[e.RowIndex].Value.ToString();

        string strwh = "select * from WDetail where MasterId='" + id + "' ";
        SqlCommand cmdwh = new SqlCommand(strwh, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dts = new DataTable();
        adpwh.Fill(dts);

        if (dts.Rows.Count == 0)
        {
            string strwh1 = "select * from WEvaluation where MasterId='" + id + "' ";
            SqlCommand cmdwh1 = new SqlCommand(strwh1, con);
            SqlDataAdapter adpwh1 = new SqlDataAdapter(cmdwh1);
            DataTable dts1 = new DataTable();
            adpwh1.Fill(dts1);

            if (dts1.Rows.Count == 0)
            {
                string strwh2 = "select * from ProjectMaster where WTMasterId='" + id + "' ";
                SqlCommand cmdwh2 = new SqlCommand(strwh2, con);
                SqlDataAdapter adpwh2 = new SqlDataAdapter(cmdwh2);
                DataTable dts2 = new DataTable();
                adpwh2.Fill(dts2);

                if (dts2.Rows.Count == 0)
                {
                    string strwh3 = "select * from StrategyMaster where StrategygoalId='" + id + "' ";
                    SqlCommand cmdwh3 = new SqlCommand(strwh3, con);
                    SqlDataAdapter adpwh3 = new SqlDataAdapter(cmdwh3);
                    DataTable dts3 = new DataTable();
                    adpwh3.Fill(dts3);

                    if (dts3.Rows.Count == 0)
                    {
                        bool success = ClsWMaster.SpWMasterDeleteData(id);
                        if (Convert.ToString(success) == "True")
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
                        statuslable.Visible = true;
                        statuslable.Text = "sorry,you are not able to delete this record as child record exist using this record.";
                    }
                }
                else
                {
                    statuslable.Visible = true;
                    statuslable.Text = "sorry,you are not able to delete this record as child record exist using this record.";
                }
            }
            else
            {
                statuslable.Visible = true;
                statuslable.Text = "sorry,you are not able to delete this record as child record exist using this record.";
            }
        }
        else
        {
            statuslable.Visible = true;
            statuslable.Text = "sorry,you are not able to delete this record as child record exist using this record.";
        }
    }
    //GRID PAGINATION FUNCTION
    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid.PageIndex = e.NewPageIndex;
        BindGrid();
    }


    public new void RegisterOnSubmitStatement(string key, string script)
    {
        ScriptManager.RegisterOnSubmitStatement(this, typeof(Page), key, script);
    }
    [Obsolete]
    public override void RegisterStartupScript(string key, string script)
    {
        string newScript = script.Replace("FTB_AddEvent(window,'load',function () {", "").Replace("});", "");
        ScriptManager.RegisterStartupScript(this, typeof(Page), key, newScript, false);
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

    protected void fillstorewithfilter()
    {
        ddlsearchByStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();

        ddlsearchByStore.DataSource = ds;
        ddlsearchByStore.DataTextField = "Name";
        ddlsearchByStore.DataValueField = "WareHouseId";
        ddlsearchByStore.DataBind();
        //ddlsearchByStore.Items.Insert(0, "All");
        //ddlsearchByStore.Items[0].Value = "0";

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            //ddlsearchByStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
    protected void fillstore()
    {
        ddlStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlStore.DataSource = ds;
        ddlStore.DataTextField = "Name";
        ddlStore.DataValueField = "WareHouseId";
        ddlStore.DataBind();

        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            // ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    public void fillDepartment()
    {


        ddlStore.Items.Clear();

        ViewState["cd"] = "2";
        DataTable dsemp = MainAcocount.SelectDepartmentmasterMNCwithCID();
        if (dsemp.Rows.Count > 0)
        {
            ddlStore.DataSource = dsemp;
            ddlStore.DataTextField = "Departmentname";
            ddlStore.DataValueField = "id";
            ddlStore.DataBind();
        }
        //ddlstore.Items.Insert(0, "-Select-");
        //ddlstore.Items[0].Value = "0";
        //ddlDepartment_SelectedIndexChanged(sender, e);

    }

    public void FilterDepartment()
    {
        ddlsearchByStore.Items.Clear();


        DataTable dsemp = MainAcocount.SelectDepartmentmasterMNCwithCID();
        if (dsemp.Rows.Count > 0)
        {
            ddlsearchByStore.DataSource = dsemp;
            ddlsearchByStore.DataTextField = "Departmentname";
            ddlsearchByStore.DataValueField = "id";
            ddlsearchByStore.DataBind();
        }

        ddlsearchByStore.Items.Insert(0, "-Select-");
        ddlsearchByStore.Items[0].Value = "0";
    }
    protected void fillemployee()
    {
        ddlemployee.Items.Clear();
        if (ddlStore.SelectedIndex > -1)
        {
            DataTable ds12 = clsDocument.SelectEmployeeMasterwithDivId("0", 1, ddlStore.SelectedValue);

            ddlemployee.DataSource = ds12;
            ddlemployee.DataTextField = "EmployeeName";
            ddlemployee.DataValueField = "EmployeeMasterID";
            ddlemployee.DataBind();
        }
        //ddlEmp.Items.Insert(0, "-Select-");
        //ddlEmp.Items[0].Value = "0";


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
        DropDownList3.Items.Insert(0, "-Select-");
        DropDownList3.Items[0].Value = "0";

    }
    public void fillDivision()
    {
        ddlbusiness.Items.Clear();
        if (ddlStore.SelectedIndex > 0)
        {

            DataTable dt2 = clsDocument.SelectDivisionwithStoreIdanddeptId(ddlStore.SelectedValue, "0", 1);

            ddlbusiness.DataSource = dt2;
            ddlbusiness.DataMember = "businessname";
            ddlbusiness.DataTextField = "businessname";
            ddlbusiness.DataValueField = "BusinessID";
            ddlbusiness.DataBind();
        }
        //ddlbusiness.Items.Insert(0, "-Select-");
        //ddlbusiness.Items[0].Value = "0";


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
        DropDownList2.Items.Insert(0, "-Select-");
        DropDownList2.Items[0].Value = "0";
    }

    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            fillbusinessmonth();
        }

        if (RadioButtonList1.SelectedValue == "1")
        {
            if (RadioButtonList3.SelectedValue == "0")
            {
                filldepartmentmonth();
            }
            if (RadioButtonList3.SelectedValue == "1")
            {
                fillbusinessweek();
            }
        }

        if (RadioButtonList1.SelectedValue == "2")
        {
            if (RadioButtonList4.SelectedValue == "0")
            {
                filldivisionmonth();
            }
            if (RadioButtonList4.SelectedValue == "1")
            {
                if (DropDownList6.SelectedValue == "0")
                {
                    fillbusinessweek11();
                }
                if (DropDownList6.SelectedValue == "1")
                {
                    filldepartmentweek11();
                }
            }
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            fillemployee();

            if (RadioButtonList5.SelectedValue == "0")
            {
                fillemployeemonth();
            }
            if (RadioButtonList5.SelectedValue == "1")
            {
                if (DropDownList7.SelectedValue == "0")
                {
                    fillbusinessweek();
                }
                if (DropDownList7.SelectedValue == "1")
                {
                    filldepartmentweek();
                }
                if (DropDownList7.SelectedValue == "2")
                {
                    filldivisionweek();
                }
            }
        }
        DropDownList1_SelectedIndexChanged1(sender, e);
        DropDownList4_SelectedIndexChanged(sender, e);
        //ddly_SelectedIndexChanged(sender, e);
    }


    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {

        filltgmain();
        ddly_SelectedIndexChanged(sender, e);
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillemployeeyear();

        //ddly_SelectedIndexChanged(sender, e);
        if (RadioButtonList5.SelectedValue == "0")
        {
            fillemployeemonth();
        }
        if (RadioButtonList5.SelectedValue == "1")
        {
            if (DropDownList7.SelectedValue == "0")
            {
                fillbusinessweek();
            }
            if (DropDownList7.SelectedValue == "1")
            {
                filldepartmentweek();
            }
            if (DropDownList7.SelectedValue == "2")
            {
                filldivisionweek();
            }
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



    protected void ddlsearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "4")
        {
            filterfillbusinessmonth();
        }
        if (RadioButtonList2.SelectedValue == "0")
        {
            if (dropdowndepartment.SelectedValue == "0")
            {

            }
            if (dropdowndepartment.SelectedValue == "1")
            {
                filterfilldepartmentmonth();
            }
            if (dropdowndepartment.SelectedValue == "2")
            {
                filterfillbusinessweek();
            }
        }

        if (RadioButtonList2.SelectedValue == "1")
        {
            if (dropdowndivision.SelectedValue == "0")
            {

            }
            if (dropdowndivision.SelectedValue == "1")
            {
                filterfilldivisionmonth();
            }
            if (dropdowndivision.SelectedValue == "2")
            {
                filterfillbusinessweek11();
            }
            if (dropdowndivision.SelectedValue == "3")
            {
                filterfilldepartmentweek11();
            }
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
            Filteremployee();

            if (dropdownemployee.SelectedValue == "0")
            {

            }
            if (dropdownemployee.SelectedValue == "1")
            {
                filterfillemployeemonth();
            }
            if (dropdownemployee.SelectedValue == "2")
            {
                filterfillbusinessweek();
            }
            if (dropdownemployee.SelectedValue == "3")
            {
                filterfilldepartmentweek();
            }
            if (dropdownemployee.SelectedValue == "4")
            {
                filterfilldivisionweek();
            }

        }
        ddlyear_SelectedIndexChanged(sender, e);
        ddlmonth_SelectedIndexChanged(sender, e);
        BindGrid();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        filltg();
        BindGrid();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        filltg();
        BindGrid();
    }
    protected void filltgmain()
    {
        ddly.Items.Clear();
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
            ds12 = ClsWDetail.SelectMddfilter(ddlStore.SelectedValue, "0", bus, em, flag);
        }
        else
        {
            ds12 = ClsWDetail.SelectMddfilter("0", ddlStore.SelectedValue, bus, em, flag);

        }
        if (ds12.Rows.Count > 0)
        {
            ddly.DataSource = ds12;

            ddly.DataMember = "Title1";
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "masterid";


            ddly.DataBind();


        }



    }
    protected void filltg()
    {

        ddlyfilter.Items.Clear();
        int flag = 0;
        if (RadioButtonList2.SelectedIndex == 0)
        {
            flag = 0;
        }
        else if (RadioButtonList2.SelectedIndex == 1)
        {
            flag = 1;
        }
        else if (RadioButtonList2.SelectedIndex == 2)
        {
            flag = 2;
        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            flag = 3;
        }
        DataTable ds12 = new DataTable();
        if (RadioButtonList2.SelectedIndex == 0)
        {
            ds12 = ClsWDetail.SelectMddfilter(ddlsearchByStore.SelectedValue, "0", DropDownList2.SelectedValue, DropDownList3.SelectedValue, flag);
        }
        else
        {
            ds12 = ClsWDetail.SelectMddfilter("0", ddlsearchByStore.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, flag);

        }
        if (ds12.Rows.Count > 0)
        {
            ddlyfilter.DataSource = ds12;

            ddlyfilter.DataMember = "Title1";
            ddlyfilter.DataTextField = "Title1";
            ddlyfilter.DataValueField = "masterid";


            ddlyfilter.DataBind();


        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";


    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropdownemployee.SelectedValue == "0")
        {

        }
        if (dropdownemployee.SelectedValue == "1")
        {
            filterfillemployeemonth();
        }
        if (dropdownemployee.SelectedValue == "2")
        {
            filterfillbusinessweek();
        }
        if (dropdownemployee.SelectedValue == "3")
        {
            filterfilldepartmentweek();
        }
        if (dropdownemployee.SelectedValue == "4")
        {
            filterfilldivisionweek();
        }
        BindGrid();
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

    protected void btnreset_Click(object sender, EventArgs e)
    {
        EmptyControls();
        //    lbllegend.Text = "";
        statuslable.Text = "";
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
    }


    protected void btncancel_Click(object sender, EventArgs e)
    {
        EmptyControls();
        BindGrid();
        //   lbllegend.Text = "";
        statuslable.Text = "";
        btncancel.Visible = true;
        btnupdate.Visible = false;

        btnsubmit.Visible = true;
        btnreset.Visible = false;
        Pnladdnew.Visible = false;
        btnadd.Visible = true;

    }


    private void EmptyControls()
    {
        txttitle.Text = "";
        chk.Checked = false;
        Button2.Checked = false;
        Pnl1.Visible = false;
        txtdescription.Text = "";
        txtbudgetedamount.Text = "0";
    }


    private void BindGrid()
    {
        string strw = "";
        string st1 = "";
        lblBusiness.Text = "";
        lblBusiness1.Text = "";
        lblDepartmemnt.Text = "";
        lblDivision.Text = "";
        lblEmp.Text = "";

        if (RadioButtonList2.SelectedIndex == 0)
        {
            lblcompany.Text = Session["Comid"].ToString();
            lblBusiness.Text = ddlsearchByStore.SelectedItem.Text;
            lblBusiness1.Text = "Business Name : " + ddlsearchByStore.SelectedItem.Text;
            if (ddlsearchByStore.SelectedIndex > -1)
            {
                st1 = " and ObjectiveMaster.StoreId='" + ddlsearchByStore.SelectedValue + "' and ObjectiveMaster.DepartmentId='0' and objectivemaster.businessid='0' and objectiveMaster.EmployeeId='0'";
            }
            else
            {
                st1 = " and  ObjectiveMaster.DepartmentId='0' and objectivemaster.businessid='0' and objectiveMaster.EmployeeId='0'";

            }
        }
        else
        {
            if (ddlsearchByStore.SelectedIndex > 0)
            {
                string[] separator1 = new string[] { ":" };
                string[] strSplitArr1 = ddlsearchByStore.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                lblcompany.Text = Session["Comid"].ToString();
                lblBusiness.Text = strSplitArr1[0].ToString();
                // lblDepartmemnt.Text = "Department : " + strSplitArr1[1].ToString();
            }
            else
            {
                lblcompany.Text = Session["Comid"].ToString();
                lblBusiness.Text = ddlsearchByStore.SelectedItem.Text;

            }
        }
        if (RadioButtonList2.SelectedIndex == 1)
        {


            lblDepartmemnt.Text = "Business:Department - " + Convert.ToString(ddlsearchByStore.SelectedItem.Text);
            if (ddlsearchByStore.SelectedValue == "0")
            {
                st1 = " and WMaster.DepartmentId>0  and WMaster.divisionid IS NULL and WMaster.EmployeeId IS NULL";

            }
            else
            {
                st1 = " and WMaster.DepartmentId='" + ddlsearchByStore.SelectedValue + "'  and WMaster.divisionid IS NULL and WMaster.EmployeeId IS NULL";

            }
        }
        else if (RadioButtonList2.SelectedIndex == 2)
        {


            lblDivision.Text = "Business:Department:Division  - " + ddlsearchByStore.SelectedItem.Text;

            if (ddlsearchByStore.SelectedValue == "0")
            {
                st1 = " and WMaster.divisionid>0 and WMaster.EmployeeId IS NULL";
            }
            else
            {
                st1 = " and WMaster.divisionid='" + ddlsearchByStore.SelectedValue + "' and WMaster.EmployeeId IS NULL";
            }


        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            lblEmp.Text = "Employee " + "   :  " + DropDownList3.SelectedItem.Text;
            if (DropDownList3.SelectedValue == "0")
            {

                if (Convert.ToInt32(ddlsearchByStore.SelectedValue) > 0)
                {
                    st1 = " and WMaster.DepartmentId='" + ddlsearchByStore.SelectedValue + "' and WMaster.EmployeeId>'0'";


                }
                else
                {
                    st1 = " and WMaster.EmployeeId>0";
                }
            }
            else
            {
                st1 = " and WMaster.EmployeeId='" + DropDownList3.SelectedValue + "'";

            }
        }
        // }
        lblyeartext.Text = "Monthly Goal Name : All";
        //if (ddlfilterobj.SelectedIndex > 0)
        //{
        //    st1 += " and objectiveMaster.masterId='" + ddlfilterobj.SelectedValue + "'";
        //}
        if (ddlyfilter.SelectedIndex > 0)
        {
            lblyeartext.Text = "Monthly Goal Name : " + ddlyfilter.SelectedItem.Text;

            st1 += " and WMaster.MMasterId='" + ddlyfilter.SelectedValue + "'";
        }

        if (ddlyear.SelectedIndex > 0)
        {
            st1 += " and year.id='" + ddlyear.SelectedValue + "'";
        }

        if (ddlmonth.SelectedIndex > 0)
        {
            st1 += " and Month.id='" + ddlmonth.SelectedValue + "'";
        }
        if (DropDownList8.SelectedIndex > 0)
        {
            st1 += " and week.id='" + DropDownList8.SelectedValue + "'";
        }
        if (DropDownList9.SelectedIndex > 0)
        {
            st1 += " and WMaster.parentmonthlygoalid='" + DropDownList9.SelectedValue + "'";
        }
        if (DropDownList10.SelectedIndex > 0)
        {
            st1 += " and WMaster.parentmonthlygoalid='" + DropDownList10.SelectedValue + "'";
        }
        if (DropDownList11.SelectedIndex > 0)
        {
            st1 += " and WMaster.parentmonthlygoalid='" + DropDownList11.SelectedValue + "'";
        }

        string filc = "";

        string str2 = "";

        if (RadioButtonList2.SelectedIndex == 0)
        {
            filc = "WareHouseMaster.Name as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname,";
            grid.Columns[0].HeaderText = "Business";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = false;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = false;

            strw = "  " + filc + " WMaster.MasterId,WMaster.BudgetedCost as bdcost,WMaster.ActualCost, StatusMaster.statusname,MMaster.Title as monthname,Week.Name as Week, WMaster.title,WMaster.MMasterId, WMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId, objectivemaster.DepartmentId as Dept_Id from Week inner join WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year inner join STGMaster on STGMaster.MasterId=YMaster.StgMasterId  inner join LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId left outer join   BusinessMaster  on objectivemaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectivemaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ObjectiveMaster.StoreId   left outer join    StatusMaster   on StatusMaster.StatusId=MMaster.StatusId where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

            str2 = " select Count(WMaster.MasterId) as ci from Week inner join WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year inner join STGMaster on STGMaster.MasterId=YMaster.StgMasterId  inner join LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId left outer join   BusinessMaster  on objectivemaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectivemaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ObjectiveMaster.StoreId   left outer join    StatusMaster   on StatusMaster.StatusId=MMaster.StatusId where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
        }
        else if (RadioButtonList2.SelectedIndex == 1)
        {
            //  filc = "  LEFT(WareHouseMaster.Name,4) as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname,";
            grid.Columns[0].HeaderText = "Busi";
            grid.Columns[1].HeaderText = "Department";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = true;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = false;

            if (dropdowndepartment.SelectedValue == "0")
            {
                strw = " WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,MMaster.Title as monthname,Week.Name as Week, WMaster.title,WMaster.MMasterId, WMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Week inner join  WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(WMaster.MasterId) as ci from Week inner join  WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
            }
            if (dropdowndepartment.SelectedValue == "1")
            {
                strw = "  WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,Month.Name + ':' + MMaster.Title as monthname,Week.Name as Week, WMaster.title,WMaster.MMasterId, WMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Week inner join  WMaster on WMaster.Week=Week.Id inner join  MMaster on MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join year on year.Id=Month.Yid left outer join BusinessMaster  on WMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=WMaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(WMaster.MasterId) as ci from Week inner join  WMaster on WMaster.Week=Week.Id inner join  MMaster on MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join year on year.Id=Month.Yid left outer join BusinessMaster  on WMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=WMaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=WMaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=WMaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
            }
            if (dropdowndepartment.SelectedValue == "2")
            {
                strw = " WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,Week.Name + ':' + W.Title as monthname,Week.Name as Week,WMaster.title,WMaster.MMasterId, WMaster.budgetedcost, EmployeeMaster.EmployeeMasterID as DocumentId from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(WMaster.MasterId) as ci from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
            }

        }
        else if (RadioButtonList2.SelectedIndex == 2)
        {
            //  filc = "  LEFT(WareHouseMaster.Name,4) as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,LEFT(DepartmentmasterMNC.Departmentname,4)as Departmentname,";
            grid.Columns[0].HeaderText = "Busi";
            grid.Columns[1].HeaderText = "Dept";
            grid.Columns[2].HeaderText = "Division";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = false;
            grid.Columns[2].Visible = true;
            grid.Columns[3].Visible = false;

            if (dropdowndivision.SelectedValue == "0")
            {
                strw = " WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,MMaster.Title as monthname,Week.Name as Week, WMaster.title,WMaster.MMasterId, WMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Week inner join  WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(WMaster.MasterId) as ci from Week inner join  WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
            }
            if (dropdowndivision.SelectedValue == "1")
            {
                strw = " WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,Month.Name + ':' + MMaster.Title as monthname,Week.Name as Week, WMaster.title,WMaster.MMasterId, WMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Week inner join  WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(WMaster.MasterId) as ci from Week inner join  WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
            }
            if (dropdowndivision.SelectedValue == "2")
            {
                strw = " WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,Week.Name + ':' + W.Title as monthname,Week.Name as Week,WMaster.title,WMaster.MMasterId, WMaster.budgetedcost, EmployeeMaster.EmployeeMasterID as DocumentId from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and wmaster.TypeofMonthlyGoal='Busi'";

                str2 = " select Count(WMaster.MasterId) as ci from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and wmaster.TypeofMonthlyGoal='Busi'";
            }
            if (dropdowndivision.SelectedValue == "3")
            {
                strw = " WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,Week.Name + ':' + W.Title as monthname,Week.Name as Week,WMaster.title,WMaster.MMasterId, WMaster.budgetedcost, EmployeeMaster.EmployeeMasterID as DocumentId from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and wmaster.TypeofMonthlyGoal='Dept'";

                str2 = " select Count(WMaster.MasterId) as ci from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and wmaster.TypeofMonthlyGoal='Dept'";
            }

        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            //  filc = "  LEFT(WareHouseMaster.Name,4) as Wname, LEFT(BusinessMaster.BusinessName,4) as businessname,EmployeeMaster.EmployeeName,LEFT(DepartmentmasterMNC.Departmentname,4)as Departmentname,";
            grid.Columns[0].HeaderText = "Busi";
            grid.Columns[1].HeaderText = "Dept";
            grid.Columns[2].HeaderText = "Divi";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = false;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = true;

            if (dropdownemployee.SelectedValue == "0")
            {
                strw = " WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,MMaster.Title as monthname,Week.Name as Week, WMaster.title,WMaster.MMasterId, WMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Week inner join  WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(WMaster.MasterId) as ci from Week inner join  WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
            }
            if (dropdownemployee.SelectedValue == "1")
            {
                strw = " WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,Month.Name + ':' + MMaster.Title as monthname,Week.Name as Week, WMaster.title,WMaster.MMasterId, WMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Week inner join  WMaster on WMaster.Week=Week.Id inner join MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join year on year.id=Month.Yid left outer join BusinessMaster  on  MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=MMaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(WMaster.MasterId) as ci from Week inner join  WMaster on WMaster.Week=Week.Id inner join MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join month on month.id=mmaster.month inner join year on year.id=Month.Yid left outer join BusinessMaster  on  MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=MMaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
            }
            if (dropdownemployee.SelectedValue == "2")
            {
                strw = " WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,Week.Name + ':' + W.Title as monthname,Week.Name as Week,WMaster.title,WMaster.MMasterId, WMaster.budgetedcost, EmployeeMaster.EmployeeMasterID as DocumentId from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and wmaster.TypeofMonthlyGoal='Busi'";

                str2 = " select Count(WMaster.MasterId) as ci from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and wmaster.TypeofMonthlyGoal='Busi'";
            }
            if (dropdownemployee.SelectedValue == "3")
            {
                strw = " WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,Week.Name + ':' + W.Title as monthname,Week.Name as Week,WMaster.title,WMaster.MMasterId, WMaster.budgetedcost, EmployeeMaster.EmployeeMasterID as DocumentId from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and wmaster.TypeofMonthlyGoal='Dept'";

                str2 = " select Count(WMaster.MasterId) as ci from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and wmaster.TypeofMonthlyGoal='Dept'";
            }
            if (dropdownemployee.SelectedValue == "4")
            {
                strw = " WareHouseMaster.Name as Wname,WMaster.BudgetedCost as bdcost,WMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,Week.Name + ':' + W.Title as monthname,Week.Name as Week,WMaster.title,WMaster.MMasterId, WMaster.budgetedcost, EmployeeMaster.EmployeeMasterID as DocumentId from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and wmaster.TypeofMonthlyGoal='Divi'";

                str2 = " select Count(WMaster.MasterId) as ci from WMaster as W inner join WMaster on W.MasterId=WMaster.parentmonthlygoalid inner join Week on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId= BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC  on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=WMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and wmaster.TypeofMonthlyGoal='Divi'";
            }
        }

        //  DataTable dt2 = ClsWDetail.SpWMasterGridfill(st1, filc);
        decimal d111 = 0;
        decimal d222 = 0;
        decimal d33 = 0;

        grid.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,WMaster.Title asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt2 = GetDataPage(grid.PageIndex, grid.PageSize, sortExpression, strw);

            DataView myDataView = new DataView();
            myDataView = dt2.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            grid.DataSource = myDataView;

            for (int rowindex = 0; rowindex < dt2.Rows.Count; rowindex++)
            {


                dt2.Rows[rowindex]["Departmentname"] = dt2.Rows[rowindex]["Departmentname"].ToString();

                DataTable dtcrNew11 = ClsWDetail.SelectOfficeManagerDocumentsforWID(Convert.ToString(dt2.Rows[rowindex]["MasterId"]));

                dt2.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];

                if (RadioButtonList2.SelectedValue == "2")
                {

                    SqlDataAdapter dar = new SqlDataAdapter("select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where WMaster.MasterId='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr = new DataTable();
                    dar.Fill(dtr);

                    dt2.Rows[rowindex]["ActualCost"] = dtr.Rows[0]["cost"];

                    if (dtr.Rows[0]["cost"].ToString() != "")
                    {
                        d111 += Convert.ToDecimal(dtr.Rows[0]["cost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "4")
                {

                    SqlDataAdapter dar = new SqlDataAdapter("select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Busi' and parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr = new DataTable();
                    dar.Fill(dtr);

                    dt2.Rows[rowindex]["ActualCost"] = dtr.Rows[0]["cost"];

                    if (dtr.Rows[0]["cost"].ToString() != "")
                    {
                        d111 += Convert.ToDecimal(dtr.Rows[0]["cost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "0")
                {

                    SqlDataAdapter dar = new SqlDataAdapter("select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Dept' and parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr = new DataTable();
                    dar.Fill(dtr);

                    dt2.Rows[rowindex]["ActualCost"] = dtr.Rows[0]["cost"];

                    if (dtr.Rows[0]["cost"].ToString() != "")
                    {
                        d111 += Convert.ToDecimal(dtr.Rows[0]["cost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "1")
                {

                    SqlDataAdapter dar = new SqlDataAdapter("select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Divi' and parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr = new DataTable();
                    dar.Fill(dtr);

                    dt2.Rows[rowindex]["ActualCost"] = dtr.Rows[0]["cost"];

                    if (dtr.Rows[0]["cost"].ToString() != "")
                    {
                        d111 += Convert.ToDecimal(dtr.Rows[0]["cost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "2")
                {

                    SqlDataAdapter dar1 = new SqlDataAdapter("select sum(cast(TaskMaster.Rate as float)) as rcost from TaskMaster inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where WMaster.MasterId='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr1 = new DataTable();
                    dar1.Fill(dtr1);

                    dt2.Rows[rowindex]["bdcost"] = dtr1.Rows[0]["rcost"];

                    if (dtr1.Rows[0]["rcost"].ToString() != "")
                    {
                        d222 += Convert.ToDecimal(dtr1.Rows[0]["rcost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "4")
                {
                    string stttt = "select sum(cast(TaskMaster.Rate as float)) as rcost from TaskMaster inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Busi' and parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'";
                    SqlDataAdapter dar1 = new SqlDataAdapter(stttt, con);
                    DataTable dtr1 = new DataTable();
                    dar1.Fill(dtr1);

                    dt2.Rows[rowindex]["bdcost"] = dtr1.Rows[0]["rcost"];

                    if (dtr1.Rows[0]["rcost"].ToString() != "")
                    {
                        d222 += Convert.ToDecimal(dtr1.Rows[0]["rcost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "0")
                {
                    SqlDataAdapter dar1 = new SqlDataAdapter("select sum(cast(TaskMaster.Rate as float)) as rcost from TaskMaster inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Dept' and parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr1 = new DataTable();
                    dar1.Fill(dtr1);

                    dt2.Rows[rowindex]["bdcost"] = dtr1.Rows[0]["rcost"];

                    if (dtr1.Rows[0]["rcost"].ToString() != "")
                    {
                        d222 += Convert.ToDecimal(dtr1.Rows[0]["rcost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "1")
                {
                    SqlDataAdapter dar1 = new SqlDataAdapter("select sum(cast(TaskMaster.Rate as float)) as rcost from TaskMaster inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId where TypeofMonthlyGoal='Divi' and parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr1 = new DataTable();
                    dar1.Fill(dtr1);

                    dt2.Rows[rowindex]["bdcost"] = dtr1.Rows[0]["rcost"];

                    if (dtr1.Rows[0]["rcost"].ToString() != "")
                    {
                        d222 += Convert.ToDecimal(dtr1.Rows[0]["rcost"]);
                    }
                }


                string d3 = dt2.Rows[rowindex]["BudgetedCost"].ToString();
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
            lblfooter.Text = d33.ToString("###,###.##");
            lblfooter2.Text = d111.ToString("###,###.##");
            lblfooter1.Text = d222.ToString("###,###.##");
        }

        //  totalbudgetedcost();

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


    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        BindGrid();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlmonthly1.Visible = false;
        pnlmonthly2.Visible = false;
        pnlmonthly3.Visible = false;

        pnlradio.Visible = false;
        pnlradio1.Visible = false;
        pnlradio2.Visible = false;

        Panel14.Visible = false;
        Panel15.Visible = false;

        lblwname0.Text = "Business-Department Name  ";
        imgadddept.Visible = false;
        imgdeptrefresh.Visible = false;

        imgadddivision.Visible = false;
        imgdivisionrefresh.Visible = false;

        if (RadioButtonList1.SelectedValue == "0")
        {
            pnldivision.Visible = false;
            pnlemployee.Visible = false;
            lblwname0.Text = "Business Name ";

            ddlemployee.Items.Clear();
            ddlbusiness.Items.Clear();
            //if (Convert.ToString(ViewState["cd"]) != "1")
            //{
          //  fillstore();
            // }
            //    fillbusinessyear();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            pnlradio.Visible = true;

            pnldivision.Visible = false;
            imgadddept.Visible = true;
            imgdeptrefresh.Visible = true;
            pnlemployee.Visible = false;

            RadioButtonList3.SelectedValue = "0";
            RadioButtonList3_SelectedIndexChanged(sender, e);

            ddlemployee.Items.Clear();
            ddlbusiness.Items.Clear();
            //if (Convert.ToString(ViewState["cd"]) != "2")
            //{
            fillDepartment();
            //   }
            //   filldepartmentssyear();
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            pnlradio1.Visible = true;

            //  pnldivision.Visible = true;
            lblwname0.Text = "Business-Department-Division Name  ";

            RadioButtonList4.SelectedValue = "0";
            RadioButtonList4_SelectedIndexChanged(sender, e);

            imgadddivision.Visible = true;
            imgdivisionrefresh.Visible = true;

            pnlemployee.Visible = false;
            ddlemployee.Items.Clear();

            //if (Convert.ToString(ViewState["cd"]) != "2")
            //{
            filldevesion();
            //  fillDepartment();
            // }
            //    filldivisionyear();
            //   fillDivision();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            pnlradio2.Visible = true;

            pnlemployee.Visible = true;
            pnldivision.Visible = false;
            ddlbusiness.Items.Clear();

            RadioButtonList5.SelectedValue = "0";
            RadioButtonList5_SelectedIndexChanged(sender, e);

            //if (Convert.ToString(ViewState["cd"]) != "2")
            //{
            fillDepartment();
            // }
            fillemployee();
            //     fillemployeeyear();
        }

        if (RadioButtonList1.SelectedValue == "")
        {
            pnldivision.Visible = false;

            pnlemployee.Visible = false;

        }
        DropDownList1_SelectedIndexChanged1(sender, e);
        DropDownList4_SelectedIndexChanged(sender, e);
        // filltgmain();
        //ddly_SelectedIndexChanged(sender, e);
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel12.Visible = false;
        paneldepartment.Visible = false;
        paneldivision.Visible = false;
        panelemployee.Visible = false;
        Panel18.Visible = false;
        Panel19.Visible = false;
        Panel20.Visible = false;

        lblwnamefilter.Text = "Business-Department Name  ";
        if (RadioButtonList2.SelectedValue == "0")
        {
            paneldepartment.Visible = true;

            Panel3.Visible = false;
            Panel4.Visible = false;


            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{
            FilterDepartment();
            //   }
            //  filterfilldepartmentssyear();
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            paneldivision.Visible = true;

            //  pnlradio.Visible = true;

            lblwnamefilter.Text = "Business-Department-Division Name  ";

            //  Panel3.Visible = true;
            Panel4.Visible = false;
            DropDownList3.Items.Clear();

            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{
            filterfilldevesion();
            //  }
            //  filterfilldivisionyear();
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
            panelemployee.Visible = true;

            //  pnlradio1.Visible = true;

            Panel3.Visible = false;
            Panel4.Visible = true;
            DropDownList2.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{
            FilterDepartment();
            // }
            Filteremployee();
            //  filterfillemployeeyear();
        }
        if (RadioButtonList2.SelectedValue == "4")
        {
            Panel12.Visible = true;

            //  pnlradio2.Visible = true;

            Panel3.Visible = false;
            Panel4.Visible = false;
            lblwnamefilter.Text = "Business Name  ";

            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            DropDownList11.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "1")
            //{
            fillstorewithfilter();
            // }
            //  filterfillbusinessyear();
        }
        ddlyear_SelectedIndexChanged(sender, e);
        ddlmonth_SelectedIndexChanged(sender, e);
        //   filltg();
        BindGrid();
    }
    protected void ddlfilterobj_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "Departmentaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        fillDepartment();
        filltgmain();
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
    protected void imgobjadd_Click(object sender, ImageClickEventArgs e)
    {

        string te = "frmMMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgdivisionrefresh_Click(object sender, ImageClickEventArgs e)
    {
        //fillDivision();
        //filltgmain();
        filldevesion();
        filldivisionyear();
    }
    protected void imgemprefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillemployee();
        filltgmain();
    }
    protected void imgobjreshresh_Click(object sender, ImageClickEventArgs e)
    {
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
        if (RadioButtonList1.SelectedValue == "3")
        {
            fillemployeeyear();
        }

        //filltgmain();
    }
    protected void Button2_CheckedChanged(object sender, EventArgs e)
    {
        if (!Pnl1.Visible)
            Pnl1.Visible = true;
        else
            Pnl1.Visible = false;
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

            if (grid.Columns[12].Visible == true)
            {
                ViewState["docs"] = "tt";
                grid.Columns[12].Visible = false;
            }
            if (grid.Columns[13].Visible == true)
            {
                ViewState["edith"] = "tt";
                grid.Columns[13].Visible = false;
            }
            if (grid.Columns[14].Visible == true)
            {
                ViewState["deleteh"] = "tt";
                grid.Columns[14].Visible = false;
            }
            if (grid.Columns[11].Visible == true)
            {
                ViewState["viewm"] = "tt";
                grid.Columns[11].Visible = false;
            }
        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;

            grid.AllowPaging = true;
            grid.PageSize = 10;
            BindGrid();

            if (ViewState["docs"] != null)
            {
                grid.Columns[12].Visible = true;
            }
            if (ViewState["edith"] != null)
            {
                grid.Columns[13].Visible = true;
            }
            if (ViewState["deleteh"] != null)
            {
                grid.Columns[14].Visible = true;
            }
            if (ViewState["viewm"] != null)
            {
                grid.Columns[11].Visible = true;
            }
        }
    }



    protected void ddlyfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Send")
        {
            //grid.SelectedIndex = 
            ////ViewState["MissionId"] = grid.SelectedDataKey.Value;

            //int index = grid.SelectedIndex;

            //Label MId = (Label)grid.Rows[index].FindControl("lblMasterId");
            ViewState["MissionId"] = Convert.ToInt32(e.CommandArgument);

            // DataTable dtcrNew11 = ClsSTGMaster.SelectOfficedocwithstgid(Convert.ToString(ViewState["MissionId"]));
            DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["MissionId"]), 6);

            GridView1.DataSource = dtcrNew11;
            GridView1.DataBind();



            ModalPopupExtenderAddnew.Show();



        }
        else if (e.CommandName == "view")
        {

            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "frmWMasterReport.aspx?Wid=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }

        if (e.CommandName == "Edit")
        {
            //string id = grid.DataKeys[e.NewSelectedIndex].Value.ToString();

            //create object of structure
            // lbllegend.Text = "Edit Weekly Goal";
            statuslable.Text = "";
            Panel22.Visible = false;
            string id = Convert.ToString(e.CommandArgument);
            ViewState["updateid"] = id;

            if (RadioButtonList2.SelectedValue == "4")
            {
                //  string dfd = " Select WareHouseMaster.Name as Wname,WMaster.Businessid,WMaster.Description,WMaster.week as ip,YMaster.Departmentid,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,WMaster.MasterId, StatusMaster.statusname,MMaster.Title as monthname,Week.Name as Week, WMaster.title,WMaster.MMasterId, WMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Week inner join  WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join YMaster on YMaster.MasterId=MMaster.YMasterId left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and WMaster.MasterId=" + id;
                string dfd = "Select * from WMaster where WMaster.MasterId=" + id;
                SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                DataTable dtfg = new DataTable();
                dafg.Fill(dtfg);

                RadioButtonList1.SelectedValue = "0";
                RadioButtonList1_SelectedIndexChanged(sender, e);

                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["Businessid"].ToString()));

                //fillbusinessmonth();

                //ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["MMasterid"].ToString()));

                SqlDataAdapter dls = new SqlDataAdapter("select year.name as Names,month.id from year inner join month on month.yid=year.id inner join week on week.mid=month.id where week.id='" + dtfg.Rows[0]["week"].ToString() + "'", con);
                DataTable dts = new DataTable();
                dls.Fill(dts);


                DropDownList1.Items.Clear();
                DropDownList1.DataSource = obj.Tablemaster("Select * from Year where Name>='" + System.DateTime.Now.Year.ToString() + "'");
                DropDownList1.DataMember = "Name";
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "Id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "-Select-");
                DropDownList1.Items[0].Value = "0";

                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dts.Rows[0]["Names"].ToString()));

                filmonthlyuper();

                DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dts.Rows[0]["id"].ToString()));
                DropDownList4_SelectedIndexChanged(sender, e);

                fillbusinessmonth();

                ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["MMasterid"].ToString()));

                //fillweeklyuper();

                DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dtfg.Rows[0]["week"].ToString()));

                txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                if (Convert.ToString(dtfg.Rows[0]["Description"]) != "")
                {
                    Button2.Checked = true;
                    Pnl1.Visible = true;
                    txtdescription.Text = dtfg.Rows[0]["Description"].ToString();
                }

            }

            if (RadioButtonList2.SelectedValue == "0")
            {
                pnlradio.Visible = true;

                SqlDataAdapter da0 = new SqlDataAdapter("select * from WMaster where Masterid=" + id, con);
                DataTable dt0 = new DataTable();
                da0.Fill(dt0);

                if (dt0.Rows[0]["MMasterId"].ToString() == "")
                {
                    string dfd = "select * from WMaster where Masterid=" + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "1";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["Departmentid"].ToString()));

                    RadioButtonList3.SelectedValue = "1";

                    pnlmonthly1.Visible = true;
                    Panel5.Visible = false;

                    SqlDataAdapter dls = new SqlDataAdapter("select year.name as Names,month.id from year inner join month on month.yid=year.id inner join week on week.mid=month.id where week.id='" + dtfg.Rows[0]["week"].ToString() + "'", con);
                    DataTable dts = new DataTable();
                    dls.Fill(dts);

                    DropDownList1.Items.Clear();
                    DropDownList1.DataSource = obj.Tablemaster("Select * from Year where Name>='" + System.DateTime.Now.Year.ToString() + "'");
                    DropDownList1.DataMember = "Name";
                    DropDownList1.DataTextField = "Name";
                    DropDownList1.DataValueField = "Id";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, "-Select-");
                    DropDownList1.Items[0].Value = "0";

                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dts.Rows[0]["Names"].ToString()));

                    filmonthlyuper();

                    DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dts.Rows[0]["id"].ToString()));
                    DropDownList4_SelectedIndexChanged(sender, e);

                    fillbusinessweek();
                    ddlbusimonthly.SelectedIndex = ddlbusimonthly.Items.IndexOf(ddlbusimonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));

                    //filldepartmentmonth();
                    //ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["MMasterid"].ToString()));

                    DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dtfg.Rows[0]["week"].ToString()));

                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }
                }
                else
                {
                    RadioButtonList3.SelectedValue = "0";

                    pnlmonthly1.Visible = false;
                    Panel5.Visible = true;

                    string dfd = "select * from WMaster where Masterid=" + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "1";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["Departmentid"].ToString()));

                    SqlDataAdapter dls = new SqlDataAdapter("select year.name as Names,month.id from year inner join month on month.yid=year.id inner join week on week.mid=month.id where week.id='" + dtfg.Rows[0]["week"].ToString() + "'", con);
                    DataTable dts = new DataTable();
                    dls.Fill(dts);

                    DropDownList1.Items.Clear();
                    DropDownList1.DataSource = obj.Tablemaster("Select * from Year where Name>='" + System.DateTime.Now.Year.ToString() + "'");
                    DropDownList1.DataMember = "Name";
                    DropDownList1.DataTextField = "Name";
                    DropDownList1.DataValueField = "Id";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, "-Select-");
                    DropDownList1.Items[0].Value = "0";

                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dts.Rows[0]["Names"].ToString()));

                    filmonthlyuper();

                    DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dts.Rows[0]["id"].ToString()));
                    DropDownList4_SelectedIndexChanged(sender, e);

                    filldepartmentmonth();
                    ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["MMasterid"].ToString()));

                    DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dtfg.Rows[0]["week"].ToString()));

                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }
                }
            }


            if (RadioButtonList2.SelectedValue == "1")
            {
                pnlradio1.Visible = true;

                SqlDataAdapter da0 = new SqlDataAdapter("select * from WMaster where Masterid=" + id, con);
                DataTable dt0 = new DataTable();
                da0.Fill(dt0);

                if (dt0.Rows[0]["MMasterId"].ToString() == "")
                {
                    string dfd = "select * from WMaster where Masterid=" + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "2";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["divisionid"].ToString()));

                    RadioButtonList4.SelectedValue = "1";

                    Panel15.Visible = true;
                    Panel5.Visible = false;

                    SqlDataAdapter dls = new SqlDataAdapter("select year.name as Names,month.id from year inner join month on month.yid=year.id inner join week on week.mid=month.id where week.id='" + dtfg.Rows[0]["week"].ToString() + "'", con);
                    DataTable dts = new DataTable();
                    dls.Fill(dts);

                    DropDownList1.Items.Clear();
                    DropDownList1.DataSource = obj.Tablemaster("Select * from Year where Name>='" + System.DateTime.Now.Year.ToString() + "'");
                    DropDownList1.DataMember = "Name";
                    DropDownList1.DataTextField = "Name";
                    DropDownList1.DataValueField = "Id";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, "-Select-");
                    DropDownList1.Items[0].Value = "0";

                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dts.Rows[0]["Names"].ToString()));

                    filmonthlyuper();

                    DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dts.Rows[0]["id"].ToString()));
                    DropDownList4_SelectedIndexChanged(sender, e);

                    DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dtfg.Rows[0]["week"].ToString()));

                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }

                    if (dtfg.Rows[0]["TypeofMonthlyGoal"].ToString() == "Busi")
                    {
                        DropDownList6.SelectedValue = "0";
                        pnlmonthly1.Visible = true;
                        pnlmonthly2.Visible = false;

                        fillbusinessweek11();
                        ddlbusimonthly.SelectedIndex = ddlbusimonthly.Items.IndexOf(ddlbusimonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));
                    }

                    if (dtfg.Rows[0]["TypeofMonthlyGoal"].ToString() == "Dept")
                    {
                        DropDownList6.SelectedValue = "1";
                        pnlmonthly1.Visible = false;
                        pnlmonthly2.Visible = true;

                        filldepartmentweek11();
                        ddldeptmonthly.SelectedIndex = ddldeptmonthly.Items.IndexOf(ddldeptmonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));
                    }
                }
                else
                {
                    string dfd = "select * from WMaster where Masterid=" + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "2";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["divisionid"].ToString()));

                    RadioButtonList4.SelectedValue = "0";

                    Panel15.Visible = false;
                    Panel5.Visible = true;

                    SqlDataAdapter dls = new SqlDataAdapter("select year.name as Names,month.id from year inner join month on month.yid=year.id inner join week on week.mid=month.id where week.id='" + dtfg.Rows[0]["week"].ToString() + "'", con);
                    DataTable dts = new DataTable();
                    dls.Fill(dts);

                    DropDownList1.Items.Clear();
                    DropDownList1.DataSource = obj.Tablemaster("Select * from Year where Name>='" + System.DateTime.Now.Year.ToString() + "'");
                    DropDownList1.DataMember = "Name";
                    DropDownList1.DataTextField = "Name";
                    DropDownList1.DataValueField = "Id";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, "-Select-");
                    DropDownList1.Items[0].Value = "0";

                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dts.Rows[0]["Names"].ToString()));

                    filmonthlyuper();

                    DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dts.Rows[0]["id"].ToString()));
                    DropDownList4_SelectedIndexChanged(sender, e);

                    filldivisionmonth();
                    ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["MMasterid"].ToString()));

                    DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dtfg.Rows[0]["week"].ToString()));

                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }
                }
            }

            if (RadioButtonList2.SelectedValue == "2")
            {
                pnlradio2.Visible = true;

                SqlDataAdapter da0 = new SqlDataAdapter("select * from WMaster where Masterid=" + id, con);
                DataTable dt0 = new DataTable();
                da0.Fill(dt0);

                if (dt0.Rows[0]["MMasterId"].ToString() == "")
                {
                    string dfd = "select * from WMaster where Masterid=" + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "3";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["departmentid"].ToString()));

                    fillemployee();
                    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dtfg.Rows[0]["employeeid"].ToString()));

                    RadioButtonList5.SelectedValue = "1";

                    Panel14.Visible = true;
                    Panel5.Visible = false;

                    SqlDataAdapter dls = new SqlDataAdapter("select year.name as Names,month.id from year inner join month on month.yid=year.id inner join week on week.mid=month.id where week.id='" + dtfg.Rows[0]["week"].ToString() + "'", con);
                    DataTable dts = new DataTable();
                    dls.Fill(dts);

                    DropDownList1.Items.Clear();
                    DropDownList1.DataSource = obj.Tablemaster("Select * from Year where Name>='" + System.DateTime.Now.Year.ToString() + "'");
                    DropDownList1.DataMember = "Name";
                    DropDownList1.DataTextField = "Name";
                    DropDownList1.DataValueField = "Id";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, "-Select-");
                    DropDownList1.Items[0].Value = "0";

                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dts.Rows[0]["Names"].ToString()));

                    filmonthlyuper();

                    DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dts.Rows[0]["id"].ToString()));
                    DropDownList4_SelectedIndexChanged(sender, e);

                    DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dtfg.Rows[0]["week"].ToString()));

                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }

                    if (dtfg.Rows[0]["TypeofMonthlyGoal"].ToString() == "Busi")
                    {
                        DropDownList7.SelectedValue = "0";
                        pnlmonthly1.Visible = true;
                        pnlmonthly2.Visible = false;
                        pnlmonthly3.Visible = false;

                        fillbusinessweek();
                        ddlbusimonthly.SelectedIndex = ddlbusimonthly.Items.IndexOf(ddlbusimonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));
                    }

                    if (dtfg.Rows[0]["TypeofMonthlyGoal"].ToString() == "Dept")
                    {
                        DropDownList7.SelectedValue = "1";
                        pnlmonthly1.Visible = false;
                        pnlmonthly2.Visible = true;
                        pnlmonthly3.Visible = false;

                        filldepartmentweek();
                        ddldeptmonthly.SelectedIndex = ddldeptmonthly.Items.IndexOf(ddldeptmonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));
                    }

                    if (dtfg.Rows[0]["TypeofMonthlyGoal"].ToString() == "Divi")
                    {
                        DropDownList7.SelectedValue = "2";
                        pnlmonthly1.Visible = false;
                        pnlmonthly2.Visible = false;
                        pnlmonthly3.Visible = true;

                        filldivisionweek();
                        ddldivimonthly.SelectedIndex = ddldivimonthly.Items.IndexOf(ddldivimonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));
                    }
                }
                else
                {
                    string dfd = "select * from WMaster where Masterid=" + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "3";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["departmentid"].ToString()));


                    fillemployee();
                    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dtfg.Rows[0]["employeeid"].ToString()));

                    RadioButtonList5.SelectedValue = "0";

                    Panel14.Visible = false;
                    Panel5.Visible = true;

                    SqlDataAdapter dls = new SqlDataAdapter("select year.name as Names,month.id from year inner join month on month.yid=year.id inner join week on week.mid=month.id where week.id='" + dtfg.Rows[0]["week"].ToString() + "'", con);
                    DataTable dts = new DataTable();
                    dls.Fill(dts);

                    DropDownList1.Items.Clear();
                    DropDownList1.DataSource = obj.Tablemaster("Select * from Year where Name>='" + System.DateTime.Now.Year.ToString() + "'");
                    DropDownList1.DataMember = "Name";
                    DropDownList1.DataTextField = "Name";
                    DropDownList1.DataValueField = "Id";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, "-Select-");
                    DropDownList1.Items[0].Value = "0";

                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dts.Rows[0]["Names"].ToString()));

                    filmonthlyuper();

                    DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dts.Rows[0]["id"].ToString()));
                    DropDownList4_SelectedIndexChanged(sender, e);

                    fillemployeemonth();
                    ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["MMasterid"].ToString()));

                    DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dtfg.Rows[0]["week"].ToString()));

                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }
                }
            }


            btncancel.Visible = true;
            btnupdate.Visible = true;

            btnsubmit.Visible = false;
            btnreset.Visible = false;
            Pnladdnew.Visible = true;
            btnadd.Visible = false;
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtenderAddnew.Hide();
    }
    protected void ddly_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlweek.Items.Clear();
        //if (ddly.SelectedIndex != -1)
        //{
        //    string[] separator1 = new string[] { ":" };
        //    string[] strSplitArr1 = ddly.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
        //    string Year = strSplitArr1[0].ToString();

        //    string given = "select month.monthid from month inner join mmaster on MMaster.Month=Month.Id where mmaster.masterid='" + ddly.SelectedValue + "'";
        //    SqlDataAdapter da = new SqlDataAdapter(given, con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    string debut = "";

        //    if (dt.Rows.Count > 0)
        //    {
        //        debut = Convert.ToString(dt.Rows[0]["monthid"]);
        //    }

        //    if (debut != currentmonth)
        //    {

        //        ddlweek.DataSource = ClsWDetail.SelctWeekonmonth(Year);
        //        ddlweek.DataMember = "yeayrmonth";
        //        ddlweek.DataTextField = "yeayrmonth";
        //        ddlweek.DataValueField = "Id";
        //        ddlweek.DataBind();

        //    }

        //    if (debut == currentmonth)
        //    {
        //        SqlDataAdapter dafa = new SqlDataAdapter("Select Week.Id,Week.Name AS yeayrmonth from dbo.Week INNER JOIN dbo.Month ON dbo.Week.Mid = dbo.Month.Id where Month.Name='" + Year + "'  and month.monthid='" + currentmonth + "' and week.lastdate1>='" + currentdate + "'  Order by Month.Name,Cast(Week.Name as nvarchar(50))", con);
        //        DataTable dtfa = new DataTable();
        //        dafa.Fill(dtfa);

        //        ddlweek.DataSource = dtfa;
        //        ddlweek.DataMember = "yeayrmonth";
        //        ddlweek.DataTextField = "yeayrmonth";
        //        ddlweek.DataValueField = "Id";
        //        ddlweek.DataBind();
        //    }
        //    ddlweek.Items.Insert(0, "-Select-");
        //    ddlweek.Items[0].Value = "0";

        //   }

    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        Pnladdnew.Visible = true;
        if (Pnladdnew.Visible == true)
        {
            btnadd.Visible = false;
        }
        statuslable.Text = "";
        //  lbllegend.Text = "Add Weekly Goal";
        RadioButtonList1.SelectedValue = "0";
        RadioButtonList1_SelectedIndexChanged(sender, e);
    }

    protected void fillbusinessyear()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.businessid='" + ddlStore.SelectedValue + "' and YMaster.DepartmentId IS NULL and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfillbusinessyear()
    {
        ddlyfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.businessid='" + ddlsearchByStore.SelectedValue + "' and YMaster.DepartmentId IS NULL and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "Title1";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();
            ddlyfilter.Items.Insert(0, "-Select-");
            ddlyfilter.Items[0].Value = "0";
        }
    }

    protected void filldepartmentssyear()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.DepartmentId='" + ddlStore.SelectedValue + "' and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfilldepartmentssyear()
    {
        ddlyfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.DepartmentId='" + ddlsearchByStore.SelectedValue + "' and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "Title1";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();

        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";
    }

    protected void totalbudgetedcost()
    {
        string temp = "";
        string totalsum = "";
        if (RadioButtonList2.SelectedValue == "4")
        {
            if (ddlsearchByStore.SelectedIndex > -1)
            {
                totalsum = "Select sum(WMaster.budgetedcost) as TotalBudgtedCost from Week inner join WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year inner join month on month.id=mmaster.month inner join STGMaster on STGMaster.MasterId=YMaster.StgMasterId  inner join LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId left outer join   BusinessMaster  on  objectivemaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectivemaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=ObjectiveMaster.StoreId   left outer join    StatusMaster   on StatusMaster.StatusId=MMaster.StatusId where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and ObjectiveMaster.StoreId='" + ddlsearchByStore.SelectedValue + "' and ObjectiveMaster.DepartmentId='0' and objectivemaster.businessid='0' and objectiveMaster.EmployeeId='0'";
            }
            else
            {
                totalsum = "Select sum(WMaster.budgetedcost) as TotalBudgtedCost from Week inner join WMaster on WMaster.Week=Week.Id inner join  MMaster on  MMaster.MasterId=Wmaster.MMasterId inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year inner join month on month.id=mmaster.month inner join STGMaster on STGMaster.MasterId=YMaster.StgMasterId  inner join LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId left outer join   BusinessMaster  on  objectivemaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectivemaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=ObjectiveMaster.StoreId   left outer join    StatusMaster   on StatusMaster.StatusId=MMaster.StatusId where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and ObjectiveMaster.DepartmentId='0' and objectivemaster.businessid='0' and objectiveMaster.EmployeeId='0'";
            }
            if (ddlyfilter.SelectedIndex > 0)
            {
                totalsum += " and WMaster.MMasterId='" + ddlyfilter.SelectedValue + "'";
            }
            if (ddlyear.SelectedIndex > 0)
            {
                totalsum += " and year.Id='" + ddlyear.SelectedValue + "'";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                totalsum += " and month.Id='" + ddlmonth.SelectedValue + "'";
            }
            if (DropDownList8.SelectedIndex > 0)
            {
                totalsum += " and week.Id='" + DropDownList8.SelectedValue + "'";
            }
        }

        if (RadioButtonList2.SelectedValue == "0")
        {
            if (ddlsearchByStore.SelectedIndex > 0)
            {
                totalsum = "Select sum(WMaster.budgetedcost) as TotalBudgtedCost from Week inner join  WMaster on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=wmaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and wmaster.DepartmentId='" + ddlsearchByStore.SelectedValue + "' and wmaster.divisionid IS NULL and wmaster.EmployeeId IS NULL";
            }
            else
            {
                totalsum = "Select sum(WMaster.budgetedcost) as TotalBudgtedCost from Week inner join  WMaster on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=wmaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and wmaster.DepartmentId>0 and wmaster.divisionid IS NULL and wmaster.EmployeeId IS NULL";
            }
            if (ddlyfilter.SelectedIndex > 0)
            {
                totalsum += " and WMaster.MMasterId='" + ddlyfilter.SelectedValue + "'";
            }
            if (ddlyear.SelectedIndex > 0)
            {
                totalsum += " and year.Id='" + ddlyear.SelectedValue + "'";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                totalsum += " and month.Id='" + ddlmonth.SelectedValue + "'";
            }
            if (DropDownList8.SelectedIndex > 0)
            {
                totalsum += " and week.Id='" + DropDownList8.SelectedValue + "'";
            }
            if (DropDownList9.SelectedIndex > 0)
            {
                totalsum += " and WMaster.parentmonthlygoalid='" + DropDownList9.SelectedValue + "'";
            }
        }

        if (RadioButtonList2.SelectedValue == "1")
        {
            if (ddlsearchByStore.SelectedIndex > 0)
            {
                totalsum = "Select sum(WMaster.budgetedcost) as TotalBudgtedCost from Week inner join  WMaster on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=wmaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and wmaster.divisionid='" + ddlsearchByStore.SelectedValue + "' and wmaster.EmployeeId IS NULL";

            }
            else
            {
                totalsum = "Select sum(WMaster.budgetedcost) as TotalBudgtedCost from Week inner join  WMaster on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=wmaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and wmaster.divisionid>0 and wmaster.EmployeeId IS NULL";
            }
            if (ddlyfilter.SelectedIndex > 0)
            {
                totalsum += " and WMaster.MMasterId='" + ddlyfilter.SelectedValue + "'";
            }
            if (ddlyear.SelectedIndex > 0)
            {
                totalsum += " and year.Id='" + ddlyear.SelectedValue + "'";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                totalsum += " and month.Id='" + ddlmonth.SelectedValue + "'";
            }
            if (DropDownList8.SelectedIndex > 0)
            {
                totalsum += " and week.Id='" + DropDownList8.SelectedValue + "'";
            }
            if (DropDownList9.SelectedIndex > 0)
            {
                totalsum += " and WMaster.parentmonthlygoalid='" + DropDownList9.SelectedValue + "'";
            }
            if (DropDownList10.SelectedIndex > 0)
            {
                totalsum += " and WMaster.parentmonthlygoalid='" + DropDownList10.SelectedValue + "'";
            }
        }

        if (RadioButtonList2.SelectedValue == "2")
        {
            if (ddlsearchByStore.SelectedIndex > 0)
            {
                totalsum = "Select sum(WMaster.budgetedcost) as TotalBudgtedCost from Week inner join  WMaster on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=wmaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and wmaster.departmentid='" + ddlsearchByStore.SelectedValue + "' ";

            }
            else
            {
                totalsum = "Select sum(WMaster.budgetedcost) as TotalBudgtedCost from Week inner join  WMaster on WMaster.Week=Week.Id inner join month on month.id=week.mid inner join year on year.id=month.yid left outer join BusinessMaster  on wmaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster]  on  [EmployeeMaster].EmployeeMasterID=wmaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=wmaster.DepartmentId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=wmaster.businessid left outer join StatusMaster on StatusMaster.StatusId=wmaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and wmaster.employeeid>0";
            }
            if (DropDownList3.SelectedIndex > 0)
            {
                totalsum += "and wMaster.EmployeeID='" + DropDownList3.SelectedValue + "'";
            }
            if (ddlyfilter.SelectedIndex > 0)
            {
                totalsum += " and WMaster.MMasterId='" + ddlyfilter.SelectedValue + "'";
            }
            if (ddlyear.SelectedIndex > 0)
            {
                totalsum += " and year.Id='" + ddlyear.SelectedValue + "'";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                totalsum += " and month.Id='" + ddlmonth.SelectedValue + "'";
            }
            if (DropDownList8.SelectedIndex > 0)
            {
                totalsum += " and week.Id='" + DropDownList8.SelectedValue + "'";
            }
            if (DropDownList9.SelectedIndex > 0)
            {
                totalsum += " and WMaster.parentmonthlygoalid='" + DropDownList9.SelectedValue + "'";
            }
            if (DropDownList10.SelectedIndex > 0)
            {
                totalsum += " and WMaster.parentmonthlygoalid='" + DropDownList10.SelectedValue + "'";
            }
            if (DropDownList11.SelectedIndex > 0)
            {
                totalsum += " and WMaster.parentmonthlygoalid='" + DropDownList11.SelectedValue + "'";
            }
        }
        SqlDataAdapter damil = new SqlDataAdapter(totalsum, con);
        DataTable dtmil = new DataTable();
        damil.Fill(dtmil);

        decimal t1 = 0;
        if (dtmil.Rows.Count > 0)
        {
            if (Convert.ToString(dtmil.Rows[0]["TotalBudgtedCost"]) != "")
            {
                //  t1 = Convert.ToDecimal(Convert.ToString(dtmil.Rows[0]["TotalBudgtedCost"]));
                t1 = Math.Round(Convert.ToDecimal(dtmil.Rows[0]["TotalBudgtedCost"]), 2);
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
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void filldevesion()
    {
        string dev = "select  distinct BusinessMaster.BusinessID,BusinessName,WareHouseMaster.Name,DepartmentmasterMNC.Departmentname, WareHouseMaster.Name +' : '+ DepartmentmasterMNC.Departmentname +' : '+ BusinessMaster.BusinessName  as Divisionname  from  BusinessMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid    where  BusinessMaster.company_id='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName";
        SqlDataAdapter ddev = new SqlDataAdapter(dev, con);
        DataTable dtdev = new DataTable();
        ddev.Fill(dtdev);

        if (dtdev.Rows.Count > 0)
        {
            ddlStore.DataSource = dtdev;
            ddlStore.DataTextField = "Divisionname";
            ddlStore.DataValueField = "BusinessID";
            ddlStore.DataBind();
        }
    }

    protected void filterfilldevesion()
    {
        string dev = "select  distinct BusinessMaster.BusinessID,BusinessName,WareHouseMaster.Name,DepartmentmasterMNC.Departmentname, WareHouseMaster.Name +' : '+ DepartmentmasterMNC.Departmentname +' : '+ BusinessMaster.BusinessName  as Divisionname  from  BusinessMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid    where  BusinessMaster.company_id='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName";
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
        ddlsearchByStore.Items.Insert(0, "-Select-");
        ddlsearchByStore.Items[0].Value = "0";
    }

    protected void filldivisionyear()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.DivisionID='" + ddlStore.SelectedValue + "' and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfilldivisionyear()
    {
        ddlyfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.DivisionID='" + ddlsearchByStore.SelectedValue + "' and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "Title1";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();

        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";
    }

    protected void fillemployeeyear()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.EmployeeId='" + ddlemployee.SelectedValue + "' order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfillemployeeyear()
    {
        ddlyfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.EmployeeId='" + DropDownList3.SelectedValue + "' order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "Title1";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();

        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";
    }
    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        filmonthlyuper();
        if (RadioButtonList1.SelectedValue == "0")
        {
            fillbusinessmonth();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            filldepartmentmonth();
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            filldivisionmonth();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            fillemployeemonth();
        }
    }
    protected void filmonthlyuper()
    {
        DropDownList4.Items.Clear();
        if (DropDownList1.SelectedIndex != -1)
        {

            if (DropDownList1.SelectedItem.Text == currentyear)
            {
                DropDownList4.DataSource = ClsMMaster.SelctMonthonYear(currentyear, currentmonth);
                DropDownList4.DataMember = "yeayrmonth";
                DropDownList4.DataTextField = "yeayrmonth";
                DropDownList4.DataValueField = "Id";
                DropDownList4.DataBind();
            }
            if (DropDownList1.SelectedItem.Text != currentyear)
            {
                SqlDataAdapter dafa = new SqlDataAdapter("Select Month.Id, Year.Name+ ' -> ' + Month.Name AS yeayrmonth from dbo.Month INNER JOIN dbo.Year ON dbo.Month.Yid = dbo.Year.Id where Year.Name='" + DropDownList1.SelectedItem.Text + "' Order by Year.Name, Month.Id", con);
                DataTable dtfa = new DataTable();
                dafa.Fill(dtfa);

                DropDownList4.DataSource = dtfa;
                DropDownList4.DataMember = "yeayrmonth";
                DropDownList4.DataTextField = "yeayrmonth";
                DropDownList4.DataValueField = "Id";
                DropDownList4.DataBind();
            }
            DropDownList4.Items.Insert(0, "-Select-");
            DropDownList4.Items[0].Value = "0";

        }
    }

    protected void filmonthly()
    {
        ddlmonth.Items.Clear();

        if (ddlyear.SelectedIndex != -1)
        {
            SqlDataAdapter dafa = new SqlDataAdapter("Select Month.Id,Month.MonthId, Year.Name+ ' -> ' + Month.Name AS yeayrmonth from dbo.Month INNER JOIN dbo.Year ON dbo.Month.Yid = dbo.Year.Id where Year.Name='" + ddlyear.SelectedItem.Text + "' Order by Year.Name, Month.Id", con);
            DataTable dtfa = new DataTable();
            dafa.Fill(dtfa);

            ddlmonth.DataSource = dtfa;
            ddlmonth.DataMember = "yeayrmonth";
            ddlmonth.DataTextField = "yeayrmonth";
            ddlmonth.DataValueField = "Id";
            ddlmonth.DataBind();

            ddlmonth.Items.Insert(0, "-Select-");
            ddlmonth.Items[0].Value = "0";

            //  ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(System.DateTime.Now.Month.ToString()));
        }
    }

    protected void fillbusinessmonth()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.businessid='" + ddlStore.SelectedValue + "' and YMaster.DepartmentId IS NULL and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL";
            //order by Month.Name,MMaster.Title asc";
            if (DropDownList1.SelectedIndex > 0)
            {
                y11 += " and year.name='" + DropDownList1.SelectedItem.Text + "'";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and month.id='" + DropDownList4.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfillbusinessmonth()
    {
        ddlyfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.businessid='" + ddlsearchByStore.SelectedValue + "' and YMaster.DepartmentId IS NULL and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL";
            // order by Month.Name,MMaster.Title asc
            if (ddlyear.SelectedIndex > 0)
            {
                y11 += " and year.name='" + ddlyear.SelectedItem.Text + "'";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and month.id='" + ddlmonth.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "Title1";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();
        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";

    }

    protected void filldepartmentmonth()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.DepartmentId='" + ddlStore.SelectedValue + "' and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL";
            //order by Month.Name,MMaster.Title asc";

            if (DropDownList1.SelectedIndex > 0)
            {
                y11 += " and year.name='" + DropDownList1.SelectedItem.Text + "'";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and month.id='" + DropDownList4.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfilldepartmentmonth()
    {
        ddlyfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.DepartmentId='" + ddlsearchByStore.SelectedValue + "' and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL";
            //order by Month.Name,MMaster.Title asc";

            if (ddlyear.SelectedIndex > 0)
            {
                y11 += " and year.name='" + ddlyear.SelectedItem.Text + "'";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and month.id='" + ddlmonth.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "Title1";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();
        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";

    }

    protected void filldivisionmonth()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.DivisionID='" + ddlStore.SelectedValue + "' and YMaster.EmployeeId IS NULL";
            //order by Month.Name,MMaster.Title asc";
            if (DropDownList1.SelectedIndex > 0)
            {
                y11 += " and year.name='" + DropDownList1.SelectedItem.Text + "'";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and month.id='" + DropDownList4.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfilldivisionmonth()
    {
        ddlyfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.DivisionID='" + ddlsearchByStore.SelectedValue + "' and YMaster.EmployeeId IS NULL";
            //order by Month.Name,MMaster.Title asc";
            if (ddlyear.SelectedIndex > 0)
            {
                y11 += " and year.name='" + ddlyear.SelectedItem.Text + "'";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and month.id='" + ddlmonth.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "Title1";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();
        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";

    }

    protected void fillemployeemonth()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.EmployeeId='" + ddlemployee.SelectedValue + "'";
            //order by Month.Name,MMaster.Title asc";
            if (DropDownList1.SelectedIndex > 0)
            {
                y11 += " and year.name='" + DropDownList1.SelectedItem.Text + "'";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and month.id='" + DropDownList4.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfillemployeemonth()
    {
        ddlyfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.EmployeeId='" + DropDownList3.SelectedValue + "'";
            //order by Month.Name,MMaster.Title asc";
            if (ddlyear.SelectedIndex > 0)
            {
                y11 += " and year.name='" + ddlyear.SelectedItem.Text + "'";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and month.id='" + ddlmonth.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "Title1";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();
        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";

    }

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillweeklyuper();
        if (RadioButtonList1.SelectedValue == "0")
        {
            fillbusinessmonth();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            if (RadioButtonList3.SelectedValue == "0")
            {
                filldepartmentmonth();
            }
            if (RadioButtonList3.SelectedValue == "1")
            {
                fillbusinessweek();
            }
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            filldivisionmonth();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            fillemployeemonth();
        }
    }
    protected void fillweeklyuper()
    {
        DropDownList5.Items.Clear();

        if (DropDownList4.SelectedIndex != -1)
        {
            string myweek = "Select Week.Id,Week.Name AS yeayrmonth from dbo.Week INNER JOIN dbo.Month ON dbo.Week.Mid = dbo.Month.Id inner join year on year.id=month.yid where year.Name='" + DropDownList1.SelectedItem.Text + "' and month.id='" + DropDownList4.SelectedValue + "'";

            SqlDataAdapter dafa = new SqlDataAdapter(myweek, con);
            DataTable dtfa = new DataTable();
            dafa.Fill(dtfa);

            DropDownList5.DataSource = dtfa;
            DropDownList5.DataMember = "yeayrmonth";
            DropDownList5.DataTextField = "yeayrmonth";
            DropDownList5.DataValueField = "Id";
            DropDownList5.DataBind();

            DropDownList5.Items.Insert(0, "-Select-");
            DropDownList5.Items[0].Value = "0";
        }
    }

    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList3.SelectedValue == "0")
        {
            Panel5.Visible = true;
            pnlmonthly1.Visible = false;
            filldepartmentmonth();
        }
        if (RadioButtonList3.SelectedValue == "1")
        {
            pnlmonthly1.Visible = true;
            Panel5.Visible = false;
            fillbusinessweek();
            //filbusinessmonths();
        }
    }

    protected void filyear()
    {
        DropDownList1.Items.Clear();

        // ddlyear.DataSource = obj.Tablemaster("Select * from Year where Name>='" + currentyear + "'");
        DropDownList1.DataSource = obj.Tablemaster("Select * from Year");
        DropDownList1.DataMember = "Name";
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "Id";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";
        DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(System.DateTime.Now.Year.ToString()));
    }

    //protected void filllweek()
    //{
    //    ddlm.Items.Clear();

    //    string y11 = "";

    //    if (ddly.SelectedIndex > 0)
    //    {
    //        string given = "select month.monthid from month inner join mmaster on MMaster.Month=Month.Id where mmaster.masterid='" + ddly.SelectedValue + "'";
    //        SqlDataAdapter da = new SqlDataAdapter(given, con);
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);

    //        string debut = "";

    //        if (dt.Rows.Count > 0)
    //        {
    //            debut = Convert.ToString(dt.Rows[0]["monthid"]);
    //        }

    //        if (debut != currentmonth)
    //        {
    //            string st1 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId where WMaster.MMasterId='" + ddly.SelectedValue + "'";

    //            if (DropDownList5.SelectedIndex > 0)
    //            {
    //                st1 += " and week.id='" + DropDownList5.SelectedValue + "'";
    //            }

    //            SqlDataAdapter dafa = new SqlDataAdapter(st1, con);
    //            DataTable dtfa = new DataTable();
    //            dafa.Fill(dtfa);

    //            ddlm.DataSource = dtfa;
    //            ddlm.DataTextField = "Title1";
    //            ddlm.DataValueField = "MasterId";
    //            ddlm.DataBind();

    //        }

    //        if (debut == currentmonth)
    //        {
    //            string st2 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId where WMaster.MMasterId='" + ddly.SelectedValue + "' and week.lastdate1>='" + currentdate + "'";

    //            if (DropDownList5.SelectedIndex > 0)
    //            {
    //                st2 += " and week.id='" + DropDownList5.SelectedValue + "'";
    //            }

    //            SqlDataAdapter dafa = new SqlDataAdapter(st2, con);
    //            DataTable dtfa = new DataTable();
    //            dafa.Fill(dtfa);

    //            ddlm.DataSource = dtfa;
    //            ddlm.DataTextField = "Title1";
    //            ddlm.DataValueField = "MasterId";
    //            ddlm.DataBind();
    //        }
    //    }
    //    ddlm.Items.Insert(0, "-Select-");
    //    ddlm.Items[0].Value = "0";
    //}


    protected void fillbusinessweek()
    {
        ddlbusimonthly.Items.Clear();

        string y11 = "";

        if (ddlStore.SelectedIndex > -1)
        {
            string deped = "select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (DropDownList1.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and  Year.Name='" + DropDownList1.SelectedItem.Text + "' and YMaster.Departmentid IS NULL and YMaster.Divisionid IS NULL and YMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and YMaster.Departmentid IS NULL and YMaster.Divisionid IS NULL and YMaster.Employeeid IS NULL";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and Month.id='" + DropDownList4.SelectedValue + "'";
            }
            if (DropDownList5.SelectedIndex > 0)
            {
                y11 += " and week.id='" + DropDownList5.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlbusimonthly.DataSource = dt;
            ddlbusimonthly.DataTextField = "Title1";
            ddlbusimonthly.DataValueField = "MasterId";
            ddlbusimonthly.DataBind();
        }
        ddlbusimonthly.Items.Insert(0, "-Select-");
        ddlbusimonthly.Items[0].Value = "0";
    }

    protected void filterfillbusinessweek()
    {
        DropDownList9.Items.Clear();

        string y11 = "";

        if (ddlsearchByStore.SelectedIndex > 0)
        {
            string deped = "select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlsearchByStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (ddlyear.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "' and YMaster.Departmentid IS NULL and YMaster.Divisionid IS NULL and YMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and YMaster.Departmentid IS NULL and YMaster.Divisionid IS NULL and YMaster.Employeeid IS NULL";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and Month.id='" + ddlmonth.SelectedValue + "'";
            }
            if (DropDownList8.SelectedIndex > 0)
            {
                y11 += " and week.id='" + DropDownList8.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DropDownList9.DataSource = dt;
            DropDownList9.DataTextField = "Title1";
            DropDownList9.DataValueField = "MasterId";
            DropDownList9.DataBind();
        }
        DropDownList9.Items.Insert(0, "-Select-");
        DropDownList9.Items[0].Value = "0";
    }

    protected void filterfillbusinessweek11()
    {
        DropDownList9.Items.Clear();

        string y11 = "";

        if (ddlsearchByStore.SelectedIndex > 0)
        {
            string deped = "select DepartmentmasterMNC.id,WareHouseMaster.WareHouseId from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlsearchByStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (ddlyear.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "'";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "'";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and Month.id='" + ddlmonth.SelectedValue + "'";
            }
            if (DropDownList8.SelectedIndex > 0)
            {
                y11 += " and week.id='" + DropDownList8.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DropDownList9.DataSource = dt;
            DropDownList9.DataTextField = "Title1";
            DropDownList9.DataValueField = "MasterId";
            DropDownList9.DataBind();
        }
        DropDownList9.Items.Insert(0, "-Select-");
        DropDownList9.Items[0].Value = "0";
    }


    protected void fillbusinessweek11()
    {
        ddlbusimonthly.Items.Clear();

        string y11 = "";

        if (ddlStore.SelectedIndex > -1)
        {
            string deped = "select DepartmentmasterMNC.id,WareHouseMaster.WareHouseId from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (DropDownList1.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where WMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and  Year.Name='" + DropDownList1.SelectedItem.Text + "'";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where WMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "'";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and Month.id='" + DropDownList4.SelectedValue + "'";
            }
            if (DropDownList5.SelectedIndex > 0)
            {
                y11 += " and week.id='" + DropDownList5.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlbusimonthly.DataSource = dt;
            ddlbusimonthly.DataTextField = "Title1";
            ddlbusimonthly.DataValueField = "MasterId";
            ddlbusimonthly.DataBind();
        }
        ddlbusimonthly.Items.Insert(0, "-Select-");
        ddlbusimonthly.Items[0].Value = "0";
    }


    protected void filldepartmentweek11()
    {
        ddldeptmonthly.Items.Clear();

        string y11 = "";

        if (ddlStore.SelectedIndex > -1)
        {
            string deped = "select DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);


            if (DropDownList1.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + dt3.Rows[0]["id"].ToString() + "' and  Year.Name='" + DropDownList1.SelectedItem.Text + "' and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + dt3.Rows[0]["id"].ToString() + "' and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and Month.id='" + DropDownList4.SelectedValue + "'";
            }
            if (DropDownList5.SelectedIndex > 0)
            {
                y11 += " and week.id='" + DropDownList5.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddldeptmonthly.DataSource = dt;
            ddldeptmonthly.DataTextField = "Title1";
            ddldeptmonthly.DataValueField = "MasterId";
            ddldeptmonthly.DataBind();
        }
        ddldeptmonthly.Items.Insert(0, "-Select-");
        ddldeptmonthly.Items[0].Value = "0";
    }

    protected void filterfilldepartmentweek11()
    {
        DropDownList10.Items.Clear();

        string y11 = "";

        if (ddlsearchByStore.SelectedIndex > 0)
        {
            string deped = "select DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlsearchByStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);


            if (ddlyear.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + dt3.Rows[0]["id"].ToString() + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "' and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + dt3.Rows[0]["id"].ToString() + "' and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and Month.id='" + ddlmonth.SelectedValue + "'";
            }
            if (DropDownList8.SelectedIndex > 0)
            {
                y11 += " and week.id='" + DropDownList8.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DropDownList10.DataSource = dt;
            DropDownList10.DataTextField = "Title1";
            DropDownList10.DataValueField = "MasterId";
            DropDownList10.DataBind();
        }
        DropDownList10.Items.Insert(0, "-Select-");
        DropDownList10.Items[0].Value = "0";
    }

    protected void filterfilldepartmentweek()
    {
        DropDownList10.Items.Clear();

        string y11 = "";

        if (ddlsearchByStore.SelectedIndex > 0)
        {
            if (ddlyear.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + ddlsearchByStore.SelectedValue + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "' and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + ddlsearchByStore.SelectedValue + "' and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and Month.id='" + ddlmonth.SelectedValue + "'";
            }
            if (DropDownList8.SelectedIndex > 0)
            {
                y11 += " and week.id='" + DropDownList8.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DropDownList10.DataSource = dt;
            DropDownList10.DataTextField = "Title1";
            DropDownList10.DataValueField = "MasterId";
            DropDownList10.DataBind();
        }
        DropDownList10.Items.Insert(0, "-Select-");
        DropDownList10.Items[0].Value = "0";
    }

    protected void filldepartmentweek()
    {
        ddldeptmonthly.Items.Clear();

        string y11 = "";

        if (ddlStore.SelectedIndex > -1)
        {
            if (DropDownList1.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + ddlStore.SelectedValue + "' and  Year.Name='" + DropDownList1.SelectedItem.Text + "' and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + ddlStore.SelectedValue + "' and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and Month.id='" + DropDownList4.SelectedValue + "'";
            }
            if (DropDownList5.SelectedIndex > 0)
            {
                y11 += " and week.id='" + DropDownList5.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddldeptmonthly.DataSource = dt;
            ddldeptmonthly.DataTextField = "Title1";
            ddldeptmonthly.DataValueField = "MasterId";
            ddldeptmonthly.DataBind();
        }
        ddldeptmonthly.Items.Insert(0, "-Select-");
        ddldeptmonthly.Items[0].Value = "0";
    }


    protected void filldivisionweek()
    {
        ddldivimonthly.Items.Clear();

        string y11 = "";

        if (ddlStore.SelectedIndex > -1)
        {
            if (DropDownList1.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + ddlStore.SelectedValue + "' and Year.Name='" + DropDownList1.SelectedItem.Text + "'  and WMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner join Year on Month.Yid = Year.Id where WMaster.Departmentid='" + ddlStore.SelectedValue + "' and WMaster.Employeeid IS NULL";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and Month.id='" + DropDownList4.SelectedValue + "'";
            }
            if (DropDownList5.SelectedIndex > 0)
            {
                y11 += " and week.id='" + DropDownList5.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddldivimonthly.DataSource = dt;
            ddldivimonthly.DataTextField = "Title1";
            ddldivimonthly.DataValueField = "MasterId";
            ddldivimonthly.DataBind();
        }
        ddldivimonthly.Items.Insert(0, "-Select-");
        ddldivimonthly.Items[0].Value = "0";
    }


    protected void filterfilldivisionweek()
    {
        DropDownList11.Items.Clear();

        string y11 = "";

        if (ddlsearchByStore.SelectedIndex > 0)
        {
            if (ddlyear.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner join Year on Month.Yid = Year.Id where WMaster.DepartmentID='" + ddlsearchByStore.SelectedValue + "' and Year.Name='" + ddlyear.SelectedItem.Text + "'  and WMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id inner join Year on Month.Yid = Year.Id where WMaster.DepartmentID='" + ddlsearchByStore.SelectedValue + "' and WMaster.Employeeid IS NULL";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and Month.id='" + ddlmonth.SelectedValue + "'";
            }
            if (DropDownList8.SelectedIndex > 0)
            {
                y11 += " and week.id='" + DropDownList8.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DropDownList11.DataSource = dt;
            DropDownList11.DataTextField = "Title1";
            DropDownList11.DataValueField = "MasterId";
            DropDownList11.DataBind();
        }
        DropDownList11.Items.Insert(0, "-Select-");
        DropDownList11.Items[0].Value = "0";
    }



    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            fillbusinessweek();
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            if (DropDownList6.SelectedValue == "0")
            {
                fillbusinessweek11();
            }
            if (DropDownList6.SelectedValue == "1")
            {
                filldepartmentweek11();
            }
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            if (DropDownList7.SelectedValue == "0")
            {
                fillbusinessweek();
            }
            if (DropDownList7.SelectedValue == "1")
            {
                filldepartmentweek();
            }
            if (DropDownList7.SelectedValue == "2")
            {
                filldivisionweek();
            }
        }
    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        filmonthly();

        if (RadioButtonList2.SelectedValue == "4")
        {
            filterfillbusinessmonth();
        }
        if (RadioButtonList2.SelectedValue == "0")
        {
            filldepartmentmonth();
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            filldivisionmonth();
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
            fillemployeemonth();
        }
        BindGrid();
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillweekly();
        if (RadioButtonList2.SelectedValue == "4")
        {
            filterfillbusinessmonth();
        }
        if (RadioButtonList2.SelectedValue == "0")
        {
            filterfilldepartmentmonth();
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            filterfilldivisionmonth();
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
            filterfillemployeemonth();
        }
        BindGrid();
    }

    protected void fillweekly()
    {
        DropDownList8.Items.Clear();

        if (ddlmonth.SelectedIndex != -1)
        {
            string myweek = "Select Week.Id,Week.Name AS yeayrmonth from dbo.Week INNER JOIN dbo.Month ON dbo.Week.Mid = dbo.Month.Id inner join year on year.id=month.yid where year.Name='" + ddlyear.SelectedItem.Text + "' and month.id='" + ddlmonth.SelectedValue + "'";

            SqlDataAdapter dafa = new SqlDataAdapter(myweek, con);
            DataTable dtfa = new DataTable();
            dafa.Fill(dtfa);

            DropDownList8.DataSource = dtfa;
            DropDownList8.DataMember = "yeayrmonth";
            DropDownList8.DataTextField = "yeayrmonth";
            DropDownList8.DataValueField = "Id";
            DropDownList8.DataBind();

            DropDownList8.Items.Insert(0, "-Select-");
            DropDownList8.Items[0].Value = "0";
        }
    }

    protected void fillteryear()
    {
        ddlyear.Items.Clear();

        // ddlyear.DataSource = obj.Tablemaster("Select * from Year where Name>='" + currentyear + "'");
        ddlyear.DataSource = obj.Tablemaster("Select * from Year");
        ddlyear.DataMember = "Name";
        ddlyear.DataTextField = "Name";
        ddlyear.DataValueField = "Id";
        ddlyear.DataBind();
        ddlyear.Items.Insert(0, "-Select-");
        ddlyear.Items[0].Value = "0";
        ddlyear.SelectedIndex = ddlyear.Items.IndexOf(ddlyear.Items.FindByText(System.DateTime.Now.Year.ToString()));
    }
    protected void RadioButtonList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList4.SelectedValue == "0")
        {
            Panel5.Visible = true;
            Panel15.Visible = false;
            pnlmonthly1.Visible = false;
            pnlmonthly2.Visible = false;
            filldivisionmonth();
        }
        if (RadioButtonList4.SelectedValue == "1")
        {
            Panel15.Visible = true;
            Panel5.Visible = false;
            pnlmonthly1.Visible = true;
            DropDownList6_SelectedIndexChanged(sender, e);
        }
    }
    protected void RadioButtonList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList5.SelectedValue == "0")
        {
            Panel5.Visible = true;
            Panel14.Visible = false;
            pnlmonthly1.Visible = false;
            pnlmonthly2.Visible = false;
            pnlmonthly3.Visible = false;
            fillemployeemonth();
        }
        if (RadioButtonList5.SelectedValue == "1")
        {
            Panel5.Visible = false;
            Panel14.Visible = true;
            pnlmonthly1.Visible = true;
            DropDownList7_SelectedIndexChanged(sender, e);
        }
    }
    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList6.SelectedValue == "0")
        {
            pnlmonthly1.Visible = true;
            pnlmonthly2.Visible = false;
            fillbusinessweek11();
        }
        if (DropDownList6.SelectedValue == "1")
        {
            pnlmonthly2.Visible = true;
            pnlmonthly1.Visible = false;
            filldepartmentweek11();
        }
    }
    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList7.SelectedValue == "0")
        {
            pnlmonthly1.Visible = true;
            pnlmonthly2.Visible = false;
            pnlmonthly3.Visible = false;
            fillbusinessweek();
        }
        if (DropDownList7.SelectedValue == "1")
        {
            pnlmonthly2.Visible = true;
            pnlmonthly1.Visible = false;
            pnlmonthly3.Visible = false;
            filldepartmentweek();
        }
        if (DropDownList7.SelectedValue == "2")
        {
            pnlmonthly2.Visible = false;
            pnlmonthly1.Visible = false;
            pnlmonthly3.Visible = true;
            filldivisionweek();
        }
    }
    protected void dropdowndepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropdowndepartment.SelectedValue == "1")
        {
            Panel12.Visible = true;
            Panel18.Visible = false;
            filterfilldepartmentmonth();
            DropDownList9.Items.Clear();
            BindGrid();
        }
        if (dropdowndepartment.SelectedValue == "2")
        {
            Panel12.Visible = false;
            Panel18.Visible = true;
            filterfillbusinessweek();
            ddlyfilter.Items.Clear();
            BindGrid();
        }
        if (dropdowndepartment.SelectedValue == "0")
        {
            Panel12.Visible = false;
            Panel18.Visible = false;
            DropDownList9.Items.Clear();
            ddlyfilter.Items.Clear();
            BindGrid();
        }
    }
    protected void dropdowndivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropdowndivision.SelectedValue == "1")
        {
            Panel12.Visible = true;
            Panel18.Visible = false;
            Panel19.Visible = false;
            filterfilldivisionmonth();
            DropDownList9.Items.Clear();
            DropDownList10.Items.Clear();
            BindGrid();
        }
        if (dropdowndivision.SelectedValue == "2")
        {
            Panel12.Visible = false;
            Panel18.Visible = true;
            Panel19.Visible = false;
            filterfillbusinessweek11();
            ddlyfilter.Items.Clear();
            DropDownList10.Items.Clear();
            BindGrid();
        }
        if (dropdowndivision.SelectedValue == "3")
        {
            Panel12.Visible = false;
            Panel18.Visible = false;
            Panel19.Visible = true;
            filterfilldepartmentweek11();
            ddlyfilter.Items.Clear();
            DropDownList9.Items.Clear();
            BindGrid();
        }
        if (dropdowndivision.SelectedValue == "0")
        {
            Panel12.Visible = false;
            Panel18.Visible = false;
            Panel19.Visible = false;
            DropDownList9.Items.Clear();
            ddlyfilter.Items.Clear();
            DropDownList10.Items.Clear();
            BindGrid();
        }
    }
    protected void dropdownemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropdownemployee.SelectedValue == "1")
        {
            Panel12.Visible = true;
            Panel18.Visible = false;
            Panel19.Visible = false;
            Panel20.Visible = false;
            filterfillemployeemonth();
            DropDownList9.Items.Clear();
            DropDownList10.Items.Clear();
            DropDownList11.Items.Clear();
            BindGrid();
        }
        if (dropdownemployee.SelectedValue == "2")
        {
            Panel12.Visible = false;
            Panel18.Visible = true;
            Panel19.Visible = false;
            Panel20.Visible = false;
            filterfillbusinessweek();
            ddlyfilter.Items.Clear();
            DropDownList10.Items.Clear();
            DropDownList11.Items.Clear();
            BindGrid();
        }
        if (dropdownemployee.SelectedValue == "3")
        {
            Panel12.Visible = false;
            Panel18.Visible = false;
            Panel19.Visible = true;
            Panel20.Visible = false;
            filterfilldepartmentweek();
            ddlyfilter.Items.Clear();
            DropDownList9.Items.Clear();
            DropDownList11.Items.Clear();
            BindGrid();
        }
        if (dropdownemployee.SelectedValue == "4")
        {
            Panel12.Visible = false;
            Panel18.Visible = false;
            Panel19.Visible = false;
            Panel20.Visible = true;
            filterfilldivisionweek();
            ddlyfilter.Items.Clear();
            DropDownList10.Items.Clear();
            DropDownList9.Items.Clear();
            BindGrid();
        }
        if (dropdownemployee.SelectedValue == "0")
        {
            Panel12.Visible = false;
            Panel18.Visible = false;
            Panel19.Visible = false;
            Panel20.Visible = false;
            DropDownList9.Items.Clear();
            ddlyfilter.Items.Clear();
            DropDownList10.Items.Clear();
            DropDownList11.Items.Clear();
            BindGrid();
        }
    }
    protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "0")
        {
            if (dropdowndepartment.SelectedValue == "2")
            {
                filterfillbusinessweek();
            }
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            if (dropdowndivision.SelectedValue == "2")
            {
                filterfillbusinessweek11();
            }
            if (dropdowndivision.SelectedValue == "3")
            {
                filterfilldepartmentweek11();
            }
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
            if (dropdownemployee.SelectedValue == "2")
            {
                filterfillbusinessweek();
            }
            if (dropdownemployee.SelectedValue == "3")
            {
                filterfilldepartmentweek();
            }
            if (dropdownemployee.SelectedValue == "4")
            {
                filterfilldivisionweek();
            }

        }
        BindGrid();
    }
    protected void DropDownList9_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void DropDownList11_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}
