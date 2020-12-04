using System;
using System.Windows;
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

public partial class Add_frmgatepass_approval : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        ViewState["Compid"] = Session["Comid"].ToString();
        ViewState["UserName"] = Session["userid"].ToString();

   

        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            txtfromdate.Text = System.DateTime.Now.ToShortDateString();
            txttodate.Text = System.DateTime.Now.ToShortDateString();

            Pagecontrol.dypcontrol(Page, page);
            fillwarehouse();

            if (ddlwarehouse.SelectedItem.Text.Equals("All"))
            {
                string qryStr1 = "Select id,Departmentname from DepartmentmasterMNC  where Companyid='" + ViewState["Compid"] + "'  order by Departmentname";
                ddlDept.DataSource = (DataSet)fillddl(qryStr1);
                fillddlOther(ddlDept, "Departmentname", "id");
                ddlDept.Items.Insert(0, "All");
                ddlDept.Items[0].Value = "0";

            }
            else
            {
                //    fillemploy();
                string qryStr = "Select id,Departmentname from DepartmentmasterMNC  where Companyid='" + ViewState["Compid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "' order by Departmentname";
                ddlDept.DataSource = (DataSet)fillddl(qryStr);
                fillddlOther(ddlDept, "Departmentname", "id");
                ddlDept.Items.Insert(0, "All");
                ddlDept.Items[0].Value = "0";
            }


            ddlDesignation.Items.Insert(0, "All");
            ddlDesignation.Items[0].Value = "0";
            ddlEmployeeName.Items.Insert(0, "All");
            ddlEmployeeName.Items[0].Value = "0";
            ddlproject.Items.Insert(0, "All");
            ddlproject.Items[0].Value = "0";
            ddltask.Items.Insert(0, "All");
            ddltask.Items[0].Value = "0";

            fillgriddata();



        }

    }
    protected void fillwarehouse()
    {
        //string str1 = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "'";

        //DataTable ds1 = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(str1, con);
        //da.Fill(ds1);
        //if (ds1.Rows.Count > 0)
        //{
        //    ddlwarehouse.DataSource = ds1;
        //    ddlwarehouse.DataTextField = "Name";
        //    ddlwarehouse.DataValueField = "WarehouseId";
        //    ddlwarehouse.DataBind();
        //}

        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();


            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
        }

    }
    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();
        //ddl.Items.Insert(0, "-Select-");
        //ddl.Items[0].Value = "0";
    }
    public DataSet fillddl(String qry)
    {
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldesignation();
        fillgriddata();

    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //   fillemploy();
        fillfilterdepartment();
        fillgriddata();

    }
    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployee();
        ddlEmployeeName_SelectedIndexChanged(sender, e);
        fillgriddata();

    }
    protected void fillfilterdepartment()
    {

        ddlDept.Items.Clear();
        string str = "select * from  DepartmentmasterMNC where Whid='" + ddlwarehouse.SelectedValue + "'";
        SqlCommand cmdfilterdept = new SqlCommand(str, con);
        SqlDataAdapter adpfilterdept = new SqlDataAdapter(cmdfilterdept);
        DataTable dtfilterdept = new DataTable();
        adpfilterdept.Fill(dtfilterdept);

        if (dtfilterdept.Rows.Count > 0)
        {
            ddlDept.DataSource = dtfilterdept;
            ddlDept.DataTextField = "Departmentname";
            ddlDept.DataValueField = "id";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, "All");
            ddlDept.Items[0].Value = "0";
        }

        ddlDept.Items.Insert(0, "All");
        ddlDept.Items[0].Value = "0";
        ddlEmployeeName.Items.Insert(0, "All");
        ddlEmployeeName.Items[0].Value = "0";

        //fillemployee();
    }
    protected void filldesignation()
    {
        ddlDesignation.Items.Clear();

        string str = "select * from  DesignationMaster where DeptID='" + ddlDept.SelectedValue + "'";
        SqlCommand cmdfilterdept = new SqlCommand(str, con);
        SqlDataAdapter adpfilterdept = new SqlDataAdapter(cmdfilterdept);
        DataTable dtfilterdept = new DataTable();
        adpfilterdept.Fill(dtfilterdept);

        if (dtfilterdept.Rows.Count > 0)
        {
            ddlDesignation.DataSource = dtfilterdept;
            ddlDesignation.DataTextField = "DesignationName";
            ddlDesignation.DataValueField = "DesignationMasterId";
            ddlDesignation.DataBind();
        }
        ddlDesignation.Items.Insert(0, "All");
        ddlDesignation.Items[0].Value = "0";
        //fillemployee();


    }

    protected void fillemploy()
    {
        ddlEmployeeName.Items.Clear();

        string qryemployee = "SELECT * from EmployeeMaster where Whid = '" + ddlwarehouse.SelectedValue + "' and SuprviserId = '" + Session["EmployeeId"].ToString() + "'";
        qryemployee = "select EmployeeMaster.EmployeeName , EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join EmployeeBatchMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where  dbo.EmployeeMaster.Whid='" + ddlwarehouse.SelectedValue + "'";
        SqlCommand cmdemployee = new SqlCommand(qryemployee, con);
        SqlDataAdapter adap1emp = new SqlDataAdapter(cmdemployee);
        DataSet ds1emp = new DataSet();
        adap1emp.Fill(ds1emp);

        ddlEmployeeName.DataSource = ds1emp;
        ddlEmployeeName.DataValueField = "EmployeeMasterId";
        ddlEmployeeName.DataTextField = "EmployeeName";
        ddlEmployeeName.DataBind();

        ddlEmployeeName.Items.Insert(0, "All");
        ddlEmployeeName.Items[0].Value = "0"; ;
    }

    protected void fillemployee()
    {
        ddlEmployeeName.Items.Clear();

        if (ddlDesignation.SelectedItem.Text.Equals("All"))
        {
            string qryemployee = "SELECT * from EmployeeMaster where DeptId = '" + ddlDept.SelectedValue + "' and SuprviserId = '" + Session["EmployeeId"].ToString() + "'";
            SqlCommand cmdemployee = new SqlCommand(qryemployee, con);
            SqlDataAdapter adap1emp = new SqlDataAdapter(cmdemployee);
            DataSet ds1emp = new DataSet();
            adap1emp.Fill(ds1emp);
            ddlEmployeeName.DataSource = ds1emp;
            ddlEmployeeName.DataValueField = "EmployeeMasterId";
            ddlEmployeeName.DataTextField = "EmployeeName";
            ddlEmployeeName.DataBind();
            ddlEmployeeName.Items.Insert(0, "All");
            ddlEmployeeName.Items[0].Value = "0"; ;
            //SqlCommand cmdfilteremp = new SqlCommand(str, con);
            //SqlDataAdapter adpfilteremp = new SqlDataAdapter(cmdfilteremp);
            //DataTable dtfilteremp = new DataTable();
            //adpfilteremp.Fill(dtfilteremp);

            //if (dtfilteremp.Rows.Count > 0)
            //{
            //    ddlEmployeeName.DataSource = dtfilteremp;
            //    ddlEmployeeName.DataTextField = "EmployeeName";
            //    ddlEmployeeName.DataValueField = "EmployeeMasterId";
            //    ddlEmployeeName.DataBind();
            //}
        }
        else
        {
            string str = "SELECT * from EmployeeMaster where DesignationMasterId = '" + ddlDesignation.SelectedValue + "'";
            SqlCommand cmd1 = new SqlCommand(str, con);
            SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            adap1.Fill(ds1);
            ddlEmployeeName.DataSource = ds1;
            ddlEmployeeName.DataValueField = "EmployeeMasterId";
            ddlEmployeeName.DataTextField = "EmployeeName";
            ddlEmployeeName.DataBind();
            ddlEmployeeName.Items.Insert(0, "All");
            ddlEmployeeName.Items[0].Value = "0";
            SqlCommand cmdfilteremp = new SqlCommand(str, con);
            SqlDataAdapter adpfilteremp = new SqlDataAdapter(cmdfilteremp);
            DataTable dtfilteremp = new DataTable();
            adpfilteremp.Fill(dtfilteremp);

            if (dtfilteremp.Rows.Count > 0)
            {
                ddlEmployeeName.DataSource = dtfilteremp;
                ddlEmployeeName.DataTextField = "EmployeeName";
                ddlEmployeeName.DataValueField = "EmployeeMasterId";
                ddlEmployeeName.DataBind();
            }
        }


    }
    protected void fillgriddata()
    {
        string str1 = "";
        //  string str = " select distinct GatepassTBL.Id,GatepassTBL.GatepassREQNo,GatepassTBL.Date,GatepassTBL.ExpectedOutTime,GatepassTBL.ExpectedInTime,EmployeeMaster.EmployeeName  from GatepassTBL  inner join GatepassDetails  on GatepassDetails.GatePassID=GatepassTBL.Id  inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=GatepassTBL.EmployeeID where GatepassTBL.StoreID='" + ddlwarehouse.SelectedValue + "' and GatePassTBL.EmployeeID = '" + ddlEmployeeName.SelectedValue + "' and GatepassTBL.Approved = '"+ ddlStatus.SelectedValue +"' ";
        string st1 = "";

        if (ddlproject.SelectedIndex > 0)
        {
            st1 += " and GatepassDetails.projectid='" + ddlproject.SelectedValue + "'";
        }
        if (ddltask.SelectedIndex > 0)
        {
            st1 += " and GatepassDetails.taskid='" + ddltask.SelectedValue + "'";
        }
        if (!ddlwarehouse.SelectedItem.Text.Equals("All"))
        {
           //WithPrijectID str1 = "select distinct GatepassTBL.Id,GatepassTBL.Approved,GatepassDetails.PurposeofVisit,GatepassApproval.GatePassApprovalnote,GatepassTBL.GatepassREQNo,GatepassTBL.Date,GatepassTBL.ExpectedOutTime,GatepassTBL.ExpectedInTime,EmployeeMaster.EmployeeName  from GatepassTBL  inner join GatepassDetails  on GatepassDetails.GatePassID=GatepassTBL.Id left outer join GatepassApproval on GatepassApproval.GatePassID=GatepassTBL.Id inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=GatepassTBL.EmployeeID left join projectmaster on projectmaster.ProjectId=GatepassDetails.ProjectID left join TaskMaster on TaskMaster.TaskId=GatepassDetails.TaskID where GatepassTBL.Approved = '" + ddlStatus.SelectedValue + "' and GatepassTBL.StoreID='" + ddlwarehouse.SelectedValue + "' and cast(GatepassTBL.Date as date) >= '" + txtfromdate.Text + "' and cast(GatepassTBL.Date as date) <= '" + txttodate.Text + "' " + st1 + "";
            str1 = "select distinct GatepassTBL.Id,GatepassTBL.Approved,GatepassDetails.PurposeofVisit,GatepassApproval.GatePassApprovalnote,GatepassTBL.GatepassREQNo,GatepassTBL.Date,GatepassTBL.ExpectedOutTime,GatepassTBL.ExpectedInTime,EmployeeMaster.EmployeeName  from  dbo.GatepassTBL INNER JOIN dbo.GatepassDetails ON dbo.GatepassDetails.GatePassID = dbo.GatepassTBL.Id LEFT OUTER JOIN dbo.GatepassApproval ON dbo.GatepassApproval.GatePassID = dbo.GatepassTBL.Id INNER JOIN dbo.EmployeeMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.GatepassTBL.EmployeeID LEFT OUTER JOIN dbo.TaskMaster ON dbo.TaskMaster.TaskId = dbo.GatepassDetails.TaskID where GatepassTBL.Approved = '" + ddlStatus.SelectedValue + "' and GatepassTBL.StoreID='" + ddlwarehouse.SelectedValue + "' and cast(GatepassTBL.Date as date) >= '" + txtfromdate.Text + "' and cast(GatepassTBL.Date as date) <= '" + txttodate.Text + "' " + st1 + ""; 
           
        }
        if (!ddlwarehouse.SelectedItem.Text.Equals("All") && (!ddlDesignation.SelectedItem.Text.Equals("All")))
        {
          //With PID  str1 = "select distinct GatepassTBL.Id,GatepassTBL.Approved,GatepassDetails.PurposeofVisit,GatepassApproval.GatePassApprovalnote,GatepassTBL.GatepassREQNo,GatepassTBL.Date,GatepassTBL.ExpectedOutTime,GatepassTBL.ExpectedInTime,EmployeeMaster.EmployeeName  from GatepassTBL  inner join GatepassDetails  on GatepassDetails.GatePassID=GatepassTBL.Id left outer join GatepassApproval on GatepassApproval.GatePassID=GatepassTBL.Id inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=GatepassTBL.EmployeeID left join projectmaster on projectmaster.ProjectId=GatepassDetails.ProjectID left join TaskMaster on TaskMaster.TaskId=GatepassDetails.TaskID where GatepassTBL.Approved = '" + ddlStatus.SelectedValue + "' and GatepassTBL.StoreID='" + ddlwarehouse.SelectedValue + "' and cast(GatepassTBL.Date as date) >= '" + txtfromdate.Text + "' and cast(GatepassTBL.Date as date) <= '" + txttodate.Text + "' " + st1 + "";
            str1 = "select distinct GatepassTBL.Id,GatepassTBL.Approved,GatepassDetails.PurposeofVisit,GatepassApproval.GatePassApprovalnote,GatepassTBL.GatepassREQNo,GatepassTBL.Date,GatepassTBL.ExpectedOutTime,GatepassTBL.ExpectedInTime,EmployeeMaster.EmployeeName  from  dbo.GatepassTBL INNER JOIN dbo.GatepassDetails ON dbo.GatepassDetails.GatePassID = dbo.GatepassTBL.Id LEFT OUTER JOIN dbo.GatepassApproval ON dbo.GatepassApproval.GatePassID = dbo.GatepassTBL.Id INNER JOIN dbo.EmployeeMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.GatepassTBL.EmployeeID LEFT OUTER JOIN dbo.TaskMaster ON dbo.TaskMaster.TaskId = dbo.GatepassDetails.TaskID where GatepassTBL.Approved = '" + ddlStatus.SelectedValue + "' and GatepassTBL.StoreID='" + ddlwarehouse.SelectedValue + "' and cast(GatepassTBL.Date as date) >= '" + txtfromdate.Text + "' and cast(GatepassTBL.Date as date) <= '" + txttodate.Text + "' " + st1 + "";
        }

        if ((!ddlwarehouse.SelectedItem.Text.Equals("All")) && (!ddlEmployeeName.SelectedItem.Text.Equals("All")))
        {
            //With PID str1 = "select distinct GatepassTBL.Id,GatepassTBL.Approved,GatepassDetails.PurposeofVisit,GatepassApproval.GatePassApprovalnote,GatepassTBL.GatepassREQNo,GatepassTBL.Date,GatepassTBL.ExpectedOutTime,GatepassTBL.ExpectedInTime,EmployeeMaster.EmployeeName  from GatepassTBL  inner join GatepassDetails  on GatepassDetails.GatePassID=GatepassTBL.Id left outer join GatepassApproval on GatepassApproval.GatePassID=GatepassTBL.Id inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=GatepassTBL.EmployeeID left join projectmaster on projectmaster.ProjectId=GatepassDetails.ProjectID left join TaskMaster on TaskMaster.TaskId=GatepassDetails.TaskID where GatepassTBL.StoreID='" + ddlwarehouse.SelectedValue + "' and GatepassTBL.EmployeeID = '" + ddlEmployeeName.SelectedValue + "' and GatepassTBL.Approved >= '" + ddlStatus.SelectedValue + "' and cast(GatepassTBL.Date as date) <= '" + txtfromdate.Text + "' and GatepassTBL.Date>= '" + txttodate.Text + "' " + st1 + "";
            str1 = "select distinct GatepassTBL.Id,GatepassTBL.Approved,GatepassDetails.PurposeofVisit,GatepassApproval.GatePassApprovalnote,GatepassTBL.GatepassREQNo,GatepassTBL.Date,GatepassTBL.ExpectedOutTime,GatepassTBL.ExpectedInTime,EmployeeMaster.EmployeeName  from  dbo.GatepassTBL INNER JOIN dbo.GatepassDetails ON dbo.GatepassDetails.GatePassID = dbo.GatepassTBL.Id LEFT OUTER JOIN dbo.GatepassApproval ON dbo.GatepassApproval.GatePassID = dbo.GatepassTBL.Id INNER JOIN dbo.EmployeeMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.GatepassTBL.EmployeeID LEFT OUTER JOIN dbo.TaskMaster ON dbo.TaskMaster.TaskId = dbo.GatepassDetails.TaskID where GatepassTBL.StoreID='" + ddlwarehouse.SelectedValue + "' and GatepassTBL.EmployeeID = '" + ddlEmployeeName.SelectedValue + "' and GatepassTBL.Approved >= '" + ddlStatus.SelectedValue + "' and cast(GatepassTBL.Date as date) <= '" + txtfromdate.Text + "' and GatepassTBL.Date>= '" + txttodate.Text + "' " + st1 + "";
        }

        SqlCommand cmd = new SqlCommand(str1, con);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            ViewState["GatePassId"] = Convert.ToInt32(ds.Rows[0]["Id"].ToString());
        }

        if (ds.Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
            //   btnSubmit.Visible = true;
            //   btnCancel.Visible = true;
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            btnSubmit.Visible = false;
            btnCancel.Visible = false;
        }
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            Label lblmasterid123 = (Label)gdr.FindControl("lblmasterid123");

            Label lblEmployeeName = (Label)gdr.FindControl("lblEmployeeName");
            Label lblDate = (Label)gdr.FindControl("lblDate");
            Label txtOuttime = (Label)gdr.FindControl("txtOuttime");
            Label txtInTime = (Label)gdr.FindControl("txtInTime");
            Label txtApprovalNote = (Label)gdr.FindControl("txtApprovalNote");
            //TextBox txtOuttime = (TextBox)gdr.FindControl("txtOuttime");
            //TextBox txtInTime = (TextBox)gdr.FindControl("txtInTime");
            //TextBox txtApprovalNote = (TextBox)gdr.FindControl("txtApprovalNote");

            Label ddlApprovalStatus = (Label)gdr.FindControl("ddlApprovalStatus");
            // DropDownList ddlApprovalStatus = (DropDownList)gdr.FindControl("ddlApprovalStatus");

            if (ddlApprovalStatus.Text == "1")
            {
                ddlApprovalStatus.Text = "Pending";
            }
            if (ddlApprovalStatus.Text == "2")
            {
                ddlApprovalStatus.Text = "Approved";
            }
            if (ddlApprovalStatus.Text == "3")
            {
                ddlApprovalStatus.Text = "Rejected";
            }


            // string acco = "select GatepassDetails.*,Party_master.Compname as 'partyname' from dbo.Party_master inner join GatepassDetails on Party_master.PartyID = GatepassDetails.PartyID  where GatePassID='" + lblmasterid123.Text + "' ";
            string acco = "select GatepassDetails.*,Party_master.Compname as partyname,Projectmaster.ProjectName,TaskMaster.TaskName from Party_master inner join GatepassDetails on Party_master.PartyID = GatepassDetails.PartyID left join projectmaster on projectmaster.ProjectId=GatepassDetails.ProjectID left join TaskMaster on TaskMaster.TaskId=GatepassDetails.TaskID  where GatePassID='" + lblmasterid123.Text + "' ";
            acco = "select GatepassDetails.*,Party_master.Compname as partyname,TaskMaster.TaskName from  dbo.Party_master INNER JOIN dbo.GatepassDetails ON dbo.Party_master.PartyID = dbo.GatepassDetails.PartyID LEFT OUTER JOIN  dbo.TaskMaster ON dbo.TaskMaster.TaskId = dbo.GatepassDetails.TaskID  where GatePassID='" + lblmasterid123.Text + "' ";
            

            SqlCommand cmd1 = new SqlCommand(acco, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            adp1.Fill(ds1);
            string partyname1 = "";
            if (ds1.Rows.Count > 0)
            {

                foreach (DataRow dr in ds1.Rows)
                {
                    // string txtApprovalNote = Convert.ToString(dr["GatePassApprovalnote"]);
                    string partyname2 = Convert.ToString(dr["PurposeofVisit"]);
                    string partyname3 = "";// Convert.ToString(dr["ProjectName"]);
                    string partyname4 = Convert.ToString(dr["TaskName"]);
                    string partyname5 = Convert.ToString(dr["partyname"]);

                    if (partyname2 != "" && partyname5 != "")
                    {
                        partyname1 = partyname5 + " : " + partyname3 + " : " + partyname4 + " : " + partyname2;
                        partyname1 = partyname1 + "," + "<br>";
                        //partyname1 + " : " + 
                    }
                }
                Label lblPartyName1 = (Label)gdr.FindControl("lblPartyName");

                lblPartyName1.Text = partyname1;
            }
        }
        //fillstatusdd();       
    }


    protected void ddlEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillproject();
        filltask();
        fillgriddata();
    }

    protected void fillproject()
    {
        ddlproject.Items.Clear();

        if (ddlEmployeeName.SelectedItem.Text != "All")
        {
            string str = "select Left(ProjectMaster.ProjectName,40) as ProjectName,ProjectMaster.ProjectId from ProjectMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=ProjectMaster.EmployeeID where  ProjectMaster.Whid='" + ddlwarehouse.SelectedValue + "' and ProjectMaster.EmployeeID='" + ddlEmployeeName.SelectedValue + "' and Status='Pending' ";
            SqlCommand cmdtask = new SqlCommand(str, con);
            SqlDataAdapter adptask = new SqlDataAdapter(cmdtask);
            DataTable dttask = new DataTable();
            adptask.Fill(dttask);

            if (dttask.Rows.Count > 0)
            {
                ddlproject.DataSource = dttask;
                ddlproject.DataTextField = "ProjectName";
                ddlproject.DataValueField = "ProjectId";
                ddlproject.DataBind();
            }
            ddlproject.Items.Insert(0, "All");
            ddlproject.Items[0].Value = "0";
        }
        else
        {
            ddlproject.Items.Insert(0, "All");
            ddlproject.Items[0].Value = "0";
        }
    }

    protected void filltask()
    {
        ddltask.Items.Clear();

        if (ddlEmployeeName.SelectedItem.Text != "All")
        {
            string str = "SELECT TaskAllocationMaster.TaskId,TaskMaster.TaskName ,TaskMaster.Status ,StatusMaster.StatusName, EmployeeMaster.EmployeeMasterID  FROM  TaskAllocationMaster INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId  INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID left outer join StatusMaster on StatusMaster.StatusId=TaskMaster.Status where TaskMaster.Whid= '" + ddlwarehouse.SelectedValue + "' and TaskAllocationMaster.EmployeeId='" + ddlEmployeeName.SelectedValue + "'";
            //' and TaskAllocationMaster.TaskAllocationDate='" + lblDateTime.Text + "'
            SqlCommand cmdtask = new SqlCommand(str, con);
            SqlDataAdapter adptask = new SqlDataAdapter(cmdtask);
            DataTable dttask = new DataTable();
            adptask.Fill(dttask);

            if (dttask.Rows.Count > 0)
            {
                ddltask.DataSource = dttask;
                ddltask.DataTextField = "TaskName";
                ddltask.DataValueField = "TaskId";
                ddltask.DataBind();
            }
            ddltask.Items.Insert(0, "All");
            ddltask.Items[0].Value = "0";
        }
        else
        {
            ddltask.Items.Insert(0, "All");
            ddltask.Items[0].Value = "0";
        }
    }

    //protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    //{

    //    GridView1.EditIndex = e.NewEditIndex;
    //    FillGrid1();

    //    Label intime = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("ExpectedOutTime");
    //    Label outtime = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("ExpectedOutTime");
    //    DropDownList ddlgrdct = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlApprovalStatus");



    //    //adp11.Fill(dt11);

    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Visible = false;

        foreach (GridViewRow gdr in GridView1.Rows)
        {
            Label lblGatePass = (Label)gdr.FindControl("lblmasterid123");
            Label txtApprovalNote = (Label)gdr.FindControl("txtApprovalNote");
            //TextBox txtApprovalNote = (TextBox)gdr.FindControl("txtApprovalNote");
            Label ddlApprovalStatus = (Label)gdr.FindControl("ddlApprovalStatus");
            //DropDownList ddlapprovalstatus = (DropDownList)gdr.FindControl("ddlApprovalStatus");
            Label txtOuttime = (Label)gdr.FindControl("txtOuttime");
            Label txtInTime = (Label)gdr.FindControl("txtInTime");
            //TextBox txtOuttime = (TextBox)gdr.FindControl("txtOuttime");
            //TextBox txtInTime = (TextBox)gdr.FindControl("txtInTime");
            string s;

            string a = "select GatePassId,GatePassApprovalnote from GatepassApproval where GatePassId = '" + lblGatePass.Text + "'";
            SqlCommand cmd1 = new SqlCommand(a, con);

            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            adp1.Fill(ds1);
            if (ds1.Rows.Count > 0 && txtApprovalNote.Text != null)
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                string abc = "update GatepassApproval set GatePassApprovalnote = '" + txtApprovalNote.Text + "' where GatePassId = " + lblGatePass.Text + "";
                SqlCommand cmd2 = new SqlCommand(abc, con);

                cmd2.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                if (txtApprovalNote.Text != "")
                {
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    s = "insert into GatepassApproval values ('" + lblGatePass.Text + "','" + txtApprovalNote.Text + "')";

                    SqlCommand cmd = new SqlCommand(s, con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            if (txtOuttime.Text != "" && txtInTime.Text != "")
            {
                string updatetiming = "update GatepassTBL set ExpectedOutTime = '" + txtOuttime.Text + "',ExpectedInTime = '" + txtInTime.Text + "' where Id = '" + lblGatePass.Text + "'";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmdupdatetime = new SqlCommand(updatetiming, con);
                cmdupdatetime.ExecuteNonQuery();
                con.Close();
            }
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            string update1 = "update GatepassTBL  set ApprovedEmployeeId = " + Convert.ToInt32(Session["Employeeid"]) + ", ApprovalDate = '" + System.DateTime.Now.ToShortDateString() + "' ,Approved = " + ddlApprovalStatus.Text + " where Id = " + Convert.ToInt32(lblGatePass.Text) + "";
            SqlCommand cmdgate = new SqlCommand(update1, con);
            cmdgate.ExecuteNonQuery();
            con.Close();

            int count = findREQ();

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            string updatereq = "update GatepassTBL set GatePassNumber = " + count + " where Approved = " + 2 + " and Id = " + Convert.ToInt32(lblGatePass.Text) + "";
            SqlCommand cmdreq = new SqlCommand(updatereq, con);
            cmdreq.ExecuteNonQuery();
            con.Close();

            txtApprovalNote.Text = "";
        }
        lblMsg.Visible = true;
        fillgriddata();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {

            int i = Convert.ToInt32(e.CommandArgument);

            string te = ("ExternalVisitRequest.aspx?var=" + i);

            //string te = ("ReminderMaster.aspx?bvalue=" + businessvalue + "&evalue=" + employeename);
            //Session["gid"] = null;
            //Session["gid"] = i;
            ////Response.Redirect("frmGatePass.aspx");
            //string te = "frmGatePass.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
    protected int findREQ()
    {
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        string find = "select count(*) from gatepassTBL where Approved = '2' and StoreID = '" + ddlwarehouse.SelectedValue + "'";
        SqlCommand cmdfind = new SqlCommand(find, con);
        int a = 0;
        a = Convert.ToInt32(cmdfind.ExecuteScalar());
        con.Close();

        return a;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void fillstatusdd()
    {
        //    if (ddlStatus.SelectedValue.Equals("2") || ddlStatus.SelectedValue.Equals("3"))
        //    {
        //        btnSubmit.Visible = false;
        //        btnCancel.Visible = false;
        //        GridView1.Columns[9].Visible = false;
        //        foreach (GridViewRow gdr in GridView1.Rows)
        //        {
        //            if (ddlStatus.SelectedItem.Text.Equals("Approved"))
        //            {

        //                DropDownList ddStatus = (DropDownList)gdr.FindControl("ddlApprovalStatus");
        //                ddStatus.SelectedItem.Text = "Approved";
        //            }
        //            if (ddlStatus.SelectedItem.Text.Equals("Rejected"))
        //            {
        //                DropDownList ddStatus = (DropDownList)gdr.FindControl("ddlApprovalStatus");

        //                ddStatus.SelectedItem.Text = "Rejected";
        //            }
        //            Label lblmasterid123 = (Label)gdr.FindControl("lblmasterid123");
        //            Button btnedit = (Button)gdr.FindControl("btnedit");

        //            btnedit.Visible = false;

        //        }
        //    }
        //    else
        //    {
        //       // btnSubmit.Visible = true;
        //       // btnCancel.Visible = true;
        //        //GridView1.Columns[9].Visible = true;
        //        foreach (GridViewRow gdr in GridView1.Rows)
        //        {
        //            Label lblmasterid123 = (Label)gdr.FindControl("lblmasterid123");
        //            Button btnedit = (Button)gdr.FindControl("btnedit");

        //            btnedit.Visible = true;
        //        }
        //        if (GridView1.Rows.Count > 0 && ddlStatus.SelectedItem.Text.Equals("Pending"))
        //        {
        //           // btnSubmit.Visible = true;
        //          //  btnCancel.Visible = true;
        //        }
        //        else
        //        {
        //            btnSubmit.Visible = false;
        //            btnCancel.Visible = false;
        //        }
        //    }
    }


    protected void ddlproject_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void ddltask_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgriddata();
    }
}
