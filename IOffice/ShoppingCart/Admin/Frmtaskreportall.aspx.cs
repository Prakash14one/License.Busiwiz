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

public partial class Admin_FrmTaskReportAll : System.Web.UI.Page
{
   // SqlConnection con =new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    SqlConnection con;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        Session["cname"] = Session["Comid"].ToString();
        if (!IsPostBack)
        {
            txtStartDate.Text = System.DateTime.Now.ToShortDateString();
            txtEndDate.Text = System.DateTime.Now.ToShortDateString();
            fillstore();
            fillemployee();

            lblcmpny.Text = Session["cname"].ToString();
           
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        ddlemp.Visible = true;
        EmpName.Visible = true;
        //btnPrint.Visible = true;
        bindgrid();
    }
    public void bindgrid()
    {
        
        //string str="SELECT     EmployeeMaster.EmployeeName, TaskAllocationMaster.TaskAllocationDate, ProjectMaster.ProjectName, TaskAllocationMaster.TaskName,  "+
        //              " TaskAllocationMaster.TaskReport,TaskAllocationMaster.supervisornote, TaskAllocationMaster.EUnitsAlloted, TaskAllocationMaster.UnitsUsed " +
        //              "   FROM         TaskAllocationMaster INNER JOIN "+
        //              " TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId INNER JOIN "+
        //              " ProjectMaster ON TaskMaster.ProjectId = ProjectMaster.ProjectId INNER JOIN "+
        //              "  EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeID "+
        //              " INNER JOIN Company_Employee ON Company_Employee.EmployeeID = EmployeeMaster.EmployeeID " +
        //              " WHERE  Company_Employee.company_id = " + Int16.Parse(Session["Company_id"].ToString()) + "  and (TaskAllocationMaster.TaskAllocationDate between '" + Convert.ToDateTime(txtStartDate.Text) + "' and '" + Convert.ToDateTime(txtEndDate.Text) + "' ) " +
        //              " order by EmployeeMaster.EmployeeName ";

        if (ddlemp.SelectedItem.Text == "ALL")
        {

            string str = "SELECT     EmployeeMaster.EmployeeName, TaskAllocationMaster.TaskAllocationDate, ProjectMaster.ProjectName, TaskMaster.TaskName,  " +
                         " TaskAllocationMaster.TaskReport,TaskAllocationMaster.supervisornote, TaskAllocationMaster.EUnitsAlloted, TaskAllocationMaster.UnitsUsed " +
                         " FROM       TaskAllocationMaster  " +
                         " INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId  " +
                         " Left outer JOIN ProjectMaster ON TaskMaster.ProjectId = ProjectMaster.ProjectId  " +
                         " INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID " +
                         " INNER JOIN Party_master ON Party_master.PartyID = EmployeeMaster.PartyID " +

                         " WHERE  Party_master.id = '" + Session["Comid"].ToString() + "'  and (TaskAllocationMaster.TaskAllocationDate between '" + Convert.ToDateTime(txtStartDate.Text) + "' and '" + Convert.ToDateTime(txtEndDate.Text) + "' ) " +
                         " order by EmployeeMaster.EmployeeName ";
            
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
           
            string str = "SELECT     EmployeeMaster.EmployeeName, TaskAllocationMaster.TaskAllocationDate, ProjectMaster.ProjectName, TaskMaster.TaskName,  " +
                    " TaskAllocationMaster.TaskReport,TaskAllocationMaster.supervisornote, TaskAllocationMaster.EUnitsAlloted, TaskAllocationMaster.UnitsUsed " +
                    " FROM       TaskAllocationMaster  " +
                    " INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId  " +
                    " Left outer JOIN ProjectMaster ON TaskMaster.ProjectId = ProjectMaster.ProjectId  " +
                    " INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID " +
                    " INNER JOIN Party_master ON Party_master.PartyID = EmployeeMaster.PartyID " +


                      " WHERE  Party_master.id = '" + Session["Comid"].ToString() + "' and TaskAllocationMaster.EmployeeId='" + ddlemp.SelectedValue + "' and (TaskAllocationMaster.TaskAllocationDate between '" + Convert.ToDateTime(txtStartDate.Text) + "' and '" + Convert.ToDateTime(txtEndDate.Text) + "' ) " +
                      " order by EmployeeMaster.EmployeeName ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            GridView1.DataSource = ds;
            GridView1.DataBind();
 
        }
        //if (GridView1.Rows.Count > 0)
        //{
        //    

        //}
    }
    protected void ddlemp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlemp.SelectedItem.Text == "ALL")
        //{
        //    bindgrid();
    
        //}
        //else
        //{

            //string str = "SELECT     EmployeeMaster.EmployeeName,TaskAllocationMaster.supervisornote, TaskAllocationMaster.TaskAllocationDate, ProjectMaster.ProjectName, TaskAllocationMaster.TaskName,  " +
            //             " TaskAllocationMaster.TaskReport, TaskAllocationMaster.EUnitsAlloted, TaskAllocationMaster.UnitsUsed " +
            //               "   FROM         TaskAllocationMaster INNER JOIN " +
            //            " TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId INNER JOIN " +
            //            " ProjectMaster ON TaskMaster.ProjectId = ProjectMaster.ProjectId INNER JOIN " +
            //           "  EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeID " +
            //           " INNER JOIN Company_Employee ON Company_Employee.EmployeeID = EmployeeMaster.EmployeeID " +
           
        //string str = "SELECT     EmployeeMaster.EmployeeName, TaskAllocationMaster.TaskAllocationDate, ProjectMaster.ProjectName, TaskAllocationMaster.TaskName,  " +
        //             " TaskAllocationMaster.TaskReport,TaskAllocationMaster.supervisornote, TaskAllocationMaster.EUnitsAlloted, TaskAllocationMaster.UnitsUsed " +
        //             " FROM       TaskAllocationMaster  " +
        //             " INNER JOIN TaskMaster ON TaskAllocationMaster.TaskId = TaskMaster.TaskId  " +
        //             " INNER JOIN ProjectMaster ON TaskMaster.ProjectId = ProjectMaster.ProjectId  " +
        //             " INNER JOIN EmployeeMaster ON TaskAllocationMaster.EmployeeId = EmployeeMaster.EmployeeMasterID " +
        //             " INNER JOIN Party_master ON Party_master.PartyID = EmployeeMaster.PartyID " +


        //               " WHERE  Party_master.id = " + Session["Comid"].ToString() + " and TaskAllocationMaster.EmployeeId='" + ddlemp.SelectedValue + "' and (TaskAllocationMaster.TaskAllocationDate between '" + Convert.ToDateTime(txtStartDate.Text) + "' and '" + Convert.ToDateTime(txtEndDate.Text) + "' ) " +
        //               " order by EmployeeMaster.EmployeeName ";

        //    SqlCommand cmd = new SqlCommand(str, con);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);

        //    GridView1.DataSource = ds;
        //    GridView1.DataBind();
        //    if (GridView1.Rows.Count > 0)
        //    {
        //        lblcname.Text = Session["cname"].ToString();

        //    }
        //}
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindgrid();
    }
    protected void fillstore()
    {
        string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["comid"].ToString() + "'and [WareHouseMaster].Status='1' order by Name";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        ddlstore.DataSource = ds;
        ddlstore.DataTextField = "Name";
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataBind();

    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        fillemployee();

    }
    protected void fillemployee()
    {

        string str12 = " select EmployeeMaster.* from EmployeeMaster inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID where Party_master.id='" + Session["comid"].ToString() + "' and EmployeeMaster.Whid='" + ddlstore.SelectedValue + "'  ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        ddlemp.DataSource = ds12;
        ddlemp.DataTextField = "EmployeeName";
        ddlemp.DataValueField = "EmployeeMasterID";
        ddlemp.DataBind();
        ddlemp.DataBind();
        ddlemp.Items.Insert(0, "ALL");


    }
    protected void btnprintableversion_Click(object sender, EventArgs e)
    {
        if (btnprintableversion.Text == "Printable Version")
        {
            btnprintableversion.Text = "Hide Printable Version";
            Button7.Visible = true;



            //if (GridView1.Columns[3].Visible == true)
            //{
            //    ViewState["docth"] = "tt";
            //    GridView1.Columns[3].Visible = false;
            //}
            //if (GridView1.Columns[4].Visible == true)
            //{
            //    ViewState["edith"] = "tt";
            //    GridView1.Columns[4].Visible = false;
            //}

        }
        else
        {
            btnprintableversion.Text = "Printable Version";
            Button7.Visible = false;
            //if (ViewState["docth"] != null)
            //{
            //    GridView1.Columns[3].Visible = true;
            //}
            //if (ViewState["edith"] != null)
            //{
            //    GridView1.Columns[4].Visible = true;
            //}

        }
    }
}
