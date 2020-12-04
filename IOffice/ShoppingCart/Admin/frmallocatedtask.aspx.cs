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

public partial class Admin_FrmAllocatedtask : System.Web.UI.Page
{
    static DataTable dt;
    DataByCompany obj = new DataByCompany();
    SqlConnection con;
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((String)Session["RoleName"] == "")
        //{
        //    Response.Redirect(ResolveUrl("~/") + "\\Admin\\default.aspx");
        //}
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
            //if ((String)Session["RoleName"] == "Supervisor")
            //{
            //    int EmployeeId = ClsBusiness.getEmployeeIdByUserId(Convert.ToInt32(Session["UserId"]));

            //    ddlBusiness.DataSource = ClsBusiness.SpBusinessMasterGetDataByAllocation(EmployeeId);
            //    ddlBusiness.DataMember = "businessname";
            //    ddlBusiness.DataTextField = "businessname";
            //    ddlBusiness.DataValueField = "businessid";
            //    ddlBusiness.DataBind();
            //    ddlBusiness.Items.Insert(0, "--Select--");

            //}
            //else
            //{

            //    ddlBusiness.DataSource = ClsBusiness.SpBusinessMasterGetData();

            //    ddlBusiness.DataSource = obj.selectBusinessByCompany(Int32.Parse(Session["Comid"].ToString()));
            //    ddlBusiness.DataMember = "BusinessMaster";
            //    ddlBusiness.DataTextField = "BusinessName";
            //    ddlBusiness.DataValueField = "Businessid";
            //    ddlBusiness.DataBind();
            //    ddlBusiness.Items.Insert(0, "--Select--");
            //}



            string str = "select Name,WareHouseId from WareHouseMaster where comid='" + Session["comid"] + "' and Status='1'";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();

            ad.Fill(ds);
            ddlStore.DataSource = ds;
            ddlStore.DataTextField = "Name";
            ddlStore.DataValueField = "WareHouseId";
            ddlStore.DataBind();
            ddlStore.Items.Insert(0, "-select-");
        }

    }
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ddlProject.SelectedValue) != "")
        {
            BindGrid();
        }
        else
        {
            grid.DataBind();
        }
       
    }

    protected void BindGrid()
    {

        //grid.DataSource = ClsTaskAllocation.SpAllocatedTaskGetDataByProjectId(Convert.ToString(ddlProject.SelectedValue));
        //grid.DataBind();

        DataView dv;
        DataTable dt = new DataTable();
        if (ddlProject.SelectedIndex > 0)
        {
            dt = ClsTaskAllocation.SpAllocatedTaskGetDataByProjectId(Convert.ToString(ddlProject.SelectedValue));
        //}
        //else
        //{
        //    dt = ClsTaskAllocation.SpTaskAllocationGetData();
        }
        dv = dt.DefaultView;
        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();

        }

        grid.DataSource = dv;
        grid.DataBind();
        if (grid.Rows.Count > 0)
        {
            
                lblcname.Text = Session["Comid"].ToString();
                lblprname.Visible = true;
           
        }
        else
        {
            lblcname.Text = "";
            lblprname.Visible = false;
        }


    }
    protected void ddlBusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProject.DataSource = ClsBusiness.spProjectMasterGetDataByBusinessId(Convert.ToString(ddlBusiness.SelectedValue));
        ddlProject.DataMember = "ProjectMaster";
        ddlProject.DataTextField = "ProjectName";
        ddlProject.DataValueField = "ProjectId";
        ddlProject.DataBind();
        ddlProject.Items.Insert(0, "------Select-------");
    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "More")
        {
            grid.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            Session["Moreinfo"] = grid.SelectedDataKey.Value;
            Session["Project"] = ddlProject.SelectedValue;
            Session["id1"] = 2;
            Response.Redirect("FrmUnAllocatedTaskDetail.Aspx");
        }
    }
    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;

        if (ViewState["sortdirection"] == null)
        {
            ViewState["sortdirection"] = "asc";
        }

        else
        {
            if (ViewState["sortdirection"].ToString() == "asc")
            {
                ViewState["sortdirection"] = "desc";
            }
            else
            {
                ViewState["sortdirection"] = "asc";
            }
        }


        DataView dv = dt.DefaultView;

        //Cache["dt"] = dv;

        if (ViewState["sortexpression"] != null)
        {
            dv.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();

        }

        grid.DataSource = dv;
        grid.DataBind();

    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStore.SelectedIndex > 0)
        {
            ddlBusiness.Items.Clear();
            ddlBusiness.DataSource = ClsBusiness.SpBusinessMasterGetData();

            //ddlBusiness.DataSource = obj.selectBusinessByCompany(Int32.Parse(Session["Comid"].ToString()));
            string k1 = "SELECT BusinessMaster.BusinessID as businessid,BusinessMaster.BusinessName + ' : ' +DepartmentmasterMNC.Departmentname as businessname, BusinessMaster.DepartmentId FROM BusinessMaster inner join DepartmentmasterMNC on  DepartmentmasterMNC.id=BusinessMaster.DepartmentId WHERE BusinessMaster.company_id = '" + Session["Comid"] + "' ORDER BY BusinessMaster.BusinessName";

            SqlCommand cmd1 = new SqlCommand(k1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dte = new DataTable();
            adp1.Fill(dte);
            ddlBusiness.DataSource = dte;
            ddlBusiness.DataMember = "BusinessMaster";
            ddlBusiness.DataTextField = "BusinessName";
            ddlBusiness.DataValueField = "Businessid";
            ddlBusiness.DataBind();
            ddlBusiness.Items.Insert(0, "--Select--");
        }
    }
}
