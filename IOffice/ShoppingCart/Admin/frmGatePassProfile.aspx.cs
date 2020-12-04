using System;
using System.Web;
using System.Windows;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
public partial class Add_Page_Role_Access : System.Web.UI.Page
{
    SqlConnection con;
    int j;
    DataTable dt1 = new DataTable();
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
            Pagecontrol.dypcontrol(Page, page);
            if (Request.QueryString["empid"].ToString() != null && Request.QueryString["req"].ToString() != null)
            {
                ViewState["emploid"] = Request.QueryString["empid"].ToString();
                ViewState["req"] = Request.QueryString["req"].ToString();
            }

            filldetail();

            fillwarehouse();
            filldepartment();
            filldesignation();
            fillEmployee();
            fillGateReq();
            filldata();
            checkvisible();
        }

    }
    protected void fillwarehouse()
    {
        string bindbusiness = " select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "'";

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(bindbusiness, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            ddBusiness.DataSource = ds1;
            ddBusiness.DataTextField = "Name";
            ddBusiness.DataValueField = "WarehouseId";
            ddBusiness.DataBind();
            //     filldata();
        }
        ddBusiness.SelectedIndex = ddBusiness.Items.IndexOf(ddBusiness.Items.FindByValue(ViewState["Whid"].ToString()));
        filldepartment();
    }
    protected void filldepartment()
    {
        ddlDepartment.Items.Clear();
        string binddept = "select id,Departmentname FROM DepartmentmasterMNC where Companyid='" + ViewState["Compid"] + "' and Whid='" + ddBusiness.SelectedValue + "' ";
        DataTable dsdept = new DataTable();
        SqlDataAdapter dadept = new SqlDataAdapter(binddept, con);
        dadept.Fill(dsdept);
        if (dsdept.Rows.Count > 0)
        {
            ddlDepartment.DataSource = dsdept;
            ddlDepartment.DataTextField = "Departmentname";
            ddlDepartment.DataValueField = "id";
            ddlDepartment.DataBind();
        }
        ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(ViewState["DeptID"].ToString()));
        filldesignation();

    }
    protected void filldesignation()
    {
        string binddesignation = "select DesignationMaster.DesignationMasterId,DesignationMaster.DesignationName FROM DesignationMaster INNER JOIN DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id where DepartmentmasterMNC.Companyid='" + ViewState["Compid"] + "' and DepartmentmasterMNC.Whid='" + ddBusiness.SelectedValue + "'";
        DataTable dsdesignation = new DataTable();
        SqlDataAdapter daddesignation = new SqlDataAdapter(binddesignation, con);
        daddesignation.Fill(dsdesignation);
        if (dsdesignation.Rows.Count > 0)
        {
            ddDesignation.DataSource = dsdesignation;
            ddDesignation.DataTextField = "DesignationName";
            ddDesignation.DataValueField = "DesignationMasterId";
            ddDesignation.DataBind();
            ddDesignation.SelectedIndex = ddDesignation.Items.IndexOf(ddDesignation.Items.FindByValue(ViewState["DesignationMasterId"].ToString()));
        }
        fillEmployee();

    }
    protected void fillEmployee()
    {
        ddEmployee.Items.Clear();
        string str = "SELECT * from EmployeeMaster where EmployeeMasterId='" + Request.QueryString["empid"] + "' ";//DesignationMasterId = '" + ddDesignation.SelectedValue + "'
        SqlCommand cmd1 = new SqlCommand(str, con);
        SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adap1.Fill(ds1);
        ddEmployee.DataSource = ds1;
        ddEmployee.DataValueField = "EmployeeMasterId";
        ddEmployee.DataTextField = "EmployeeName";
        ddEmployee.DataBind();
        ddEmployee.SelectedIndex = ddEmployee.Items.IndexOf(ddEmployee.Items.FindByValue(ViewState["emploid"].ToString()));
        ddStatus.SelectedIndex = ddStatus.Items.IndexOf(ddStatus.Items.FindByValue(ViewState["Status"].ToString()));
        fillGateReq();
    }
    protected void fillGateReq()
    {
        ddGateReq.Items.Clear();

        string strgatepass = "SELECT GatepassREQNo from GatepassTBL where EmployeeId = '" + ddEmployee.SelectedValue + "' and Approved = '" + ddStatus.SelectedValue + "'";
        SqlCommand cmd1gatepass = new SqlCommand(strgatepass, con);
        SqlDataAdapter adap1gatepass = new SqlDataAdapter(cmd1gatepass);
        DataSet dsgatepass = new DataSet();
        adap1gatepass.Fill(dsgatepass);
        ddGateReq.DataSource = dsgatepass;
        ddGateReq.DataValueField = "GatepassREQNo";
        ddGateReq.DataTextField = "GatepassREQNo";
        ddGateReq.DataBind();
        string aid = "";
        string aidgh = "select GatepassREQNo from gatepassTBL where Id = '" + ViewState["req"].ToString() + "'";
        SqlCommand cmdaidgh = new SqlCommand(aidgh, con);
        SqlDataAdapter dass = new SqlDataAdapter(cmdaidgh);
        DataTable dtd = new DataTable();
        dass.Fill(dtd);

        //aid = cmdaidgh.ExecuteScalar().ToString();

        aid = Convert.ToString(dtd.Rows[0]["GatepassREQNo"]);

        ddGateReq.SelectedIndex = ddGateReq.Items.IndexOf(ddGateReq.Items.FindByValue(aid.ToString()));
        filldata();
    }

    protected void filldata()
    {
        string str1;
        str1 = "select gatepassTBL.*,EmployeeMaster.EmployeeName from GatepassTBL  inner join EmployeeMaster on Employeemaster.EmployeeMasterId = GatepassTBL.EmployeeID where EmployeeMaster.WhId = '" + ddBusiness.SelectedValue + "'";

        if (ddlDepartment.SelectedIndex > 0)
        {
            str1 += "and Employeemaster.DeptId='" + ddlDepartment.SelectedValue + "'";
        }
        if (ddDesignation.SelectedIndex > 0)
        {
            str1 += "and EmployeeMaster.DesignationMasterId = '" + ddDesignation.SelectedValue + "'";
        }
        if (ddEmployee.SelectedIndex > 0)
        {
            str1 += "and GatePassTBL.EmployeeID = '" + ddEmployee.SelectedValue + "'";
        }
        if (ddStatus.SelectedIndex >= 0)
        {
            str1 += "and GatepassTBL.Approved = '" + ddStatus.SelectedValue + "'";
        }
        if (ddGateReq.SelectedIndex >= 0)
        {
            str1 += "and GatepassTBL.GatepassREQNo = '" + ddGateReq.SelectedValue + "'";
        }

        //  string str1 = "select gatepassTBL.*,EmployeeMaster.EmployeeName from GatepassTBL  inner join EmployeeMaster on Employeemaster.EmployeeMasterId = GatepassTBL.EmployeeID where EmployeeMaster.WhId = '"+ 158 +"' and GatepassTBL.GatepassREQNo = 1";
        lblCompany.Text = Session["Cname"].ToString();
        lblBusiness.Text = ddBusiness.SelectedItem.Text.ToString();
        lblDepartment1.Text = ddlDepartment.SelectedItem.Text.ToString();
        lblDesignation.Text = ddDesignation.SelectedItem.Text.ToString();
        lblEmployeeName.Text = ddEmployee.SelectedItem.Text.ToString();
        lblEmp1.Text = ddEmployee.SelectedItem.Text.ToString();
        lblstatus1.Text = ddStatus.SelectedItem.Text.ToString();
        lblGatepassREQ1.Text = ddGateReq.SelectedItem.Text.ToString();
        SqlCommand cmdfill = new SqlCommand(str1, con);
        int a = 0;
        SqlDataAdapter adpfill = new SqlDataAdapter(cmdfill);
        DataTable dtfill = new DataTable();
        adpfill.Fill(dtfill);
        if (dtfill.Rows.Count > 0)
        {
            ViewState["ID"] = dtfill.Rows[0]["Id"].ToString();
            lblREQ.Text = dtfill.Rows[0]["GatepassREQNo"].ToString();
            a = Convert.ToInt32(dtfill.Rows[0]["Approved"]);                
            lblDate.Text = (dtfill.Rows[0]["Date"]).ToString();
            lblEmpName.Text = dtfill.Rows[0]["EmployeeName"].ToString();
            lblExpTime.Text = dtfill.Rows[0]["ExpectedOutTime"].ToString();
            lblGatePassNo.Text = dtfill.Rows[0]["GatePassNumber"].ToString();
            lblReturnTime.Text = dtfill.Rows[0]["ExpectedInTime"].ToString();
            if (dtfill.Rows[0]["ApprovalDate"].ToString() != "")
            {
                lblApprovalTime.Text = Convert.ToDateTime(dtfill.Rows[0]["ApprovalDate"]).ToShortDateString();
                lblappdate.Text = lblApprovalTime.Text;
            }
            else
            {
                lblApprovalTime.Text = "";
                lblappdate.Text = "";
            }
            DateTime adate = new DateTime();            
            adate = Convert.ToDateTime(lblDate.Text);
            lblDate.Text = adate.ToShortDateString();

            //DateTime apdate = new DateTime();
            //apdate = Convert.ToDateTime(lblApprovalTime.Text);
            //lblApprovalTime.Text = apdate.ToShortDateString();
            //lblappdate.Text = apdate.ToShortDateString();
            fillgrid();
        }
        else
        {
            lblREQ.Text = "";
            lblApprovalBy.Text = "";
            lblApprovalNote.Text = "";
            lblApprovalStatus.Text = "";
            lblApprovalTime.Text = "";
            lblDate.Text = "";
            lblEmpName.Text = "";
            lblExpTime.Text = "";
            lblGatePassNo.Text = "";
            lblReturnTime.Text = "";
            ViewState["ID"] = "";
        }
        if (a == 2)
        {
            lblApprovalStatus.Text = "Approved";
        }
        else if (a == 3)
        {
            lblApprovalStatus.Text = "Rejected";
        }
        else if (a == 1)
        {
            lblApprovalStatus.Text = "Pending";
        }
        else
        {
            lblApprovalStatus.Text = "";
        }
        if (ViewState["ID"] != "")
        {
            string findgatepass = "select GatepassApprovalNote from GatepassApproval where GatepassId = '" + ViewState["ID"] + "'";
            SqlCommand cmdgatepass = new SqlCommand(findgatepass, con);
            SqlDataAdapter adap1gatepass = new SqlDataAdapter(cmdgatepass);
            DataSet dsgatepass = new DataSet();
            adap1gatepass.Fill(dsgatepass);

            if (dsgatepass.Tables[0].Rows.Count > 0)
            {
                lblApprovalNote.Text = Convert.ToString(dsgatepass.Tables[0].Rows[0]["GatepassApprovalNote"]);
            }
            else
            {
                lblApprovalNote.Text = "";
            }

            string strapp = "select ApprovedEmployeeId,EmployeeMaster.EmployeeName from GatepassTBL inner join EmployeeMaster on GatepassTBL.ApprovedEmployeeId = EmployeeMaster.EmployeeMasterId where GatepassTBL.GatepassREQNo ='" + ddGateReq.SelectedValue + "' and StoreID='" + ddBusiness.SelectedValue + "' and EmployeeId= '" + ViewState["emploid"].ToString() + "'";
            SqlCommand cmdname = new SqlCommand(strapp, con);
            SqlDataAdapter adpname = new SqlDataAdapter(cmdname);
            DataTable dtname = new DataTable();
            adpname.Fill(dtname);
            if (dtname.Rows.Count > 0)
            {
                lblApprovalBy.Text = dtname.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                lblApprovalBy.Text = "";
            }

        }

    }
    protected void ddDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldesignation();
    }
    protected void ddDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillEmployee();
    }
    protected void ddEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillGateReq();
    }
    protected void ddStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGateReq();
        filldata();
        checkvisible();
    }
    protected void fillgrid()
    {
        string task = "select GatepassDetails.ID,GatepassDetails.GatePassID,GatepassDetails.PartyID,GatepassDetails.PurposeofVisit,GatepassDetails.TaskID as 'RelatedTask',TaskMaster.TaskName as 'RelatedTaskName', Party_master.Compname as 'VisitParty' from GatepassDetails inner join Party_master on GatepassDetails.PartyID = Party_master.PartyID left outer join Taskmaster on Taskmaster.TaskID = GatepassDetails.TaskID  where GatePassID = '" + ViewState["ID"] + "'";
        SqlCommand cmdtask = new SqlCommand(task, con);
        SqlDataAdapter adptask = new SqlDataAdapter(cmdtask);
        DataTable dttask = new DataTable();
        adptask.Fill(dttask);
        if (dttask.Rows.Count > 0)
        {
            grdparty.DataSource = dttask;
            grdparty.DataBind();
        }
        else
        {
            grdparty.DataSource = null;
            grdparty.DataBind();
        }
    }
    protected void ddBusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldepartment();
    }
    protected void ddGateReq_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldata();
    }
    protected void filldetail()
    {
        string strdetail = "select Whid,DeptID,DesignationMasterId from EmployeeMaster where EmployeeMasterId = '" + ViewState["emploid"] + "'";
        SqlCommand cmddetail = new SqlCommand(strdetail, con);
        SqlDataAdapter adpdetail = new SqlDataAdapter(cmddetail);
        DataTable dtdetail = new DataTable();
        adpdetail.Fill(dtdetail);
        if (dtdetail.Rows.Count > 0)
        {
            ViewState["Whid"] = dtdetail.Rows[0]["Whid"].ToString();
            ViewState["DeptID"] = dtdetail.Rows[0]["Deptid"].ToString();
            ViewState["DesignationMasterId"] = dtdetail.Rows[0]["Designationmasterid"].ToString();
        }
        string strfindap = "select top(1) Approved from GatepassTBL where EmployeeId = '" + ViewState["emploid"] + "' order by GatepassREQNo DESC";

        SqlCommand cmdfindap = new SqlCommand(strfindap, con);
        SqlDataAdapter adpfindap = new SqlDataAdapter(cmdfindap);
        DataTable dtfindap = new DataTable();
        adpfindap.Fill(dtfindap);
        if (dtfindap.Rows.Count > 0)
        {
            ViewState["Status"] = dtfindap.Rows[0]["Approved"].ToString();

        }
        string findadmin = "select * from Employeemaster where SuprviserId = '" + Session["EmployeeId"].ToString() + "'";
        SqlCommand cmdfindadmin = new SqlCommand(findadmin, con);
        SqlDataAdapter adpfindadmin = new SqlDataAdapter(cmdfindadmin);
        DataTable dtfindadmin = new DataTable();
        adpfindadmin.Fill(dtfindadmin);
        if (dtfindadmin.Rows.Count > 0)
        {
            ddlDepartment.Enabled = true;
            ddBusiness.Enabled = true;
            ddDesignation.Enabled = true;
            ddEmployee.Enabled = true;
        }
        else
        {
            ddlDepartment.Enabled = false;
            ddBusiness.Enabled = false;
            ddDesignation.Enabled = false;
            ddEmployee.Enabled = false;
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button5.Visible = true;           

        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(250);

            Button1.Text = "Printable Version";
            Button5.Visible = false;
           
        }
    }
    protected void checkvisible()
    {
        if (ddStatus.SelectedItem.Text.Equals("Rejected"))
        {
            lblApprovalBylbl.Visible = false;
            lblApprovalTimelbl.Visible = false;
            lblApprovalNotelbl.Visible = false;
        }
    }
}
