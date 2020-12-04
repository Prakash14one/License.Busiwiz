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
using System.Data;
using System.Data.SqlClient;

public partial class ShoppingCart_Admin_Master_Default : System.Web.UI.Page
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
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);

        if (!IsPostBack)
        {
            fillstore();
            fillgrid();
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

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }

    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void fillgrid()
    {
        string active = " ";
        //string str = "select MessageCenterRightsTbl.*,[DesignationMaster].[DesignationName] as Designation,DesignationMaster.DesignationMasterId,[DepartmentmasterMNC].[Departmentname] as Department,DepartmentmasterMNC.id from [DesignationMaster]  inner join [DepartmentmasterMNC] on [DepartmentmasterMNC].[id]=[DesignationMaster].[DeptID]  inner join [WareHouseMaster] on [WareHouseMaster].[WareHouseId]=[DepartmentmasterMNC].[Whid] inner join MessageCenterRightsTbl on MessageCenterRightsTbl.BusinessID=WareHouseMaster.WareHouseId  where [WareHouseMaster].[WareHouseId]='" + ddlStore.SelectedValue + "'";
        //string str = "select distinct MessageCenterRightsTbl.*,[DesignationMaster].[DesignationName] as Designation,DesignationMaster.DesignationMasterId,[DepartmentmasterMNC].[Departmentname] as Department,DepartmentmasterMNC.id from MessageCenterRightsTbl inner join [DesignationMaster] on [DesignationMaster].DesignationMasterId=MessageCenterRightsTbl.designationid inner join [DepartmentmasterMNC] on [DepartmentmasterMNC].id=[DesignationMaster].[DeptID] where MessageCenterRightsTbl.businessid='" + ddlStore.SelectedValue + "'";
        if (CheckBox3.Checked==true)
        {
            active = " AND dbo.DesignationMaster.Active='1' AND  dbo.DepartmentmasterMNC.Active='1' AND dbo.WareHouseMaster.Status='1' ";
        }
        string str = "select [DesignationMaster].[DesignationName] as Designation,DesignationMaster.DesignationMasterId,[DepartmentmasterMNC].[Departmentname] as Department,DepartmentmasterMNC.id from [DesignationMaster]  inner join [DepartmentmasterMNC] on [DepartmentmasterMNC].[id]=[DesignationMaster].[DeptID]  inner join [WareHouseMaster] on [WareHouseMaster].[WareHouseId]=[DepartmentmasterMNC].[Whid]  where [WareHouseMaster].[WareHouseId]='" + ddlStore.SelectedValue + "' " + active + " ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();


        //  fillg(GridView1);
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow grd in GridView1.Rows)
        {
            Label lbldeptid = (Label)grd.FindControl("lbldeptid");
            Label lbldesigid = (Label)grd.FindControl("lbldesigid");

            CheckBox chkbusiness = (CheckBox)grd.FindControl("chkbusiness");
            CheckBox chkadmin = (CheckBox)grd.FindControl("chkadmin");
            CheckBox chkCandidate = (CheckBox)grd.FindControl("chkCandidate");
            CheckBox chkEmployee = (CheckBox)grd.FindControl("chkEmployee");
            CheckBox chkCustomer = (CheckBox)grd.FindControl("chkCustomer");
            CheckBox chkVendor = (CheckBox)grd.FindControl("chkVendor");
            CheckBox chkOther = (CheckBox)grd.FindControl("chkOther");
            CheckBox chkVisitor = (CheckBox)grd.FindControl("chkVisitor");


            SqlDataAdapter das = new SqlDataAdapter("select * from MessageCenterRightsTbl where businessid='" + ddlStore.SelectedValue + "' and DesignationID='" + lbldesigid.Text + "'", con);
            DataTable dts = new DataTable();
            das.Fill(dts);

            if (dts.Rows.Count == 0)
            {
                string insert = "insert into MessageCenterRightsTbl values('" + Session["Comid"].ToString() + "','" + ddlStore.SelectedValue + "','" + lbldesigid.Text + "','" + chkbusiness.Checked + "','" + chkadmin.Checked + "','" + chkCandidate.Checked + "','" + chkEmployee.Checked + "','" + chkCustomer.Checked + "','" + chkVendor.Checked + "','" + chkOther.Checked + "','" + chkVisitor.Checked + "')";
                SqlCommand cmd = new SqlCommand(insert, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                lblmsg.Text = "Record inserted successfully";

            }
            else
            {
                string update = "update MessageCenterRightsTbl set [CompanyID]='" + Session["Comid"].ToString() + "',[BusinessID]='" + ddlStore.SelectedValue + "',[DesignationID]='" + lbldesigid.Text + "',[Business]='" + chkbusiness.Checked + "',[AdminRights]='" + chkadmin.Checked + "',[Candidate]='" + chkCandidate.Checked + "',[Employee]='" + chkEmployee.Checked + "',[Customer]='" + chkCustomer.Checked + "',[Vendor]='" + chkVendor.Checked + "',[Others]='" + chkOther.Checked + "',[Visitor]='" + chkVisitor.Checked + "' where [BusinessID]='" + ddlStore.SelectedValue + "' and DesignationID='" + lbldesigid.Text + "'";
                SqlCommand cmd = new SqlCommand(update, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                lblmsg.Text = "Record updated successfully";
            }
        }
    }

    protected void fillg(GridView grd1)
    {
        foreach (GridViewRow grd in grd1.Rows)
        {
            Label lbldeptid = (Label)grd.FindControl("lbldeptid");
            Label lbldesigid = (Label)grd.FindControl("lbldesigid");

            CheckBox chkbusiness = (CheckBox)grd.FindControl("chkbusiness");
            CheckBox chkadmin = (CheckBox)grd.FindControl("chkadmin");
            CheckBox chkCandidate = (CheckBox)grd.FindControl("chkCandidate");
            CheckBox chkEmployee = (CheckBox)grd.FindControl("chkEmployee");
            CheckBox chkCustomer = (CheckBox)grd.FindControl("chkCustomer");
            CheckBox chkVendor = (CheckBox)grd.FindControl("chkVendor");
            CheckBox chkOther = (CheckBox)grd.FindControl("chkOther");
            CheckBox chkVisitor = (CheckBox)grd.FindControl("chkVisitor");

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lbldeptid = (Label)e.Row.FindControl("lbldeptid");
            Label lbldesigid = (Label)e.Row.FindControl("lbldesigid");

            CheckBox chkbusiness = (CheckBox)e.Row.FindControl("chkbusiness");
            CheckBox chkadmin = (CheckBox)e.Row.FindControl("chkadmin");
            CheckBox chkCandidate = (CheckBox)e.Row.FindControl("chkCandidate");
            CheckBox chkEmployee = (CheckBox)e.Row.FindControl("chkEmployee");
            CheckBox chkCustomer = (CheckBox)e.Row.FindControl("chkCustomer");
            CheckBox chkVendor = (CheckBox)e.Row.FindControl("chkVendor");
            CheckBox chkOther = (CheckBox)e.Row.FindControl("chkOther");
            CheckBox chkVisitor = (CheckBox)e.Row.FindControl("chkVisitor");

            SqlDataAdapter daa = new SqlDataAdapter("select Business,AdminRights,Candidate,Employee,Customer,Vendor,Others,Visitor from MessageCenterRightsTbl where DesignationID='" + lbldesigid.Text + "'", con);
            DataTable dta = new DataTable();
            daa.Fill(dta);

            if (dta.Rows.Count > 0)
            {
                btnsubmit.Text = "Update";

                chkbusiness.Checked = Convert.ToBoolean(dta.Rows[0]["Business"]);
                chkadmin.Checked = Convert.ToBoolean(dta.Rows[0]["AdminRights"]);
                chkCandidate.Checked = Convert.ToBoolean(dta.Rows[0]["Candidate"]);
                chkEmployee.Checked = Convert.ToBoolean(dta.Rows[0]["Employee"]);
                chkCustomer.Checked = Convert.ToBoolean(dta.Rows[0]["Customer"]);
                chkVendor.Checked = Convert.ToBoolean(dta.Rows[0]["Vendor"]);
                chkOther.Checked = Convert.ToBoolean(dta.Rows[0]["Others"]);
                chkVisitor.Checked = Convert.ToBoolean(dta.Rows[0]["Visitor"]);
            }
            else
            {
                btnsubmit.Text = "Submit";
            }
        }
    }
  
    protected void GridView1_RowCreated1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Assign Access Rights to Send a Message to the Following Usertypes";
            HeaderCell.ColumnSpan = 7;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderGridRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
}
