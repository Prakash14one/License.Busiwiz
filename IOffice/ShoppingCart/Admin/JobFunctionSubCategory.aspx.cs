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
using System.Net.NetworkInformation;

public partial class ShoppingCart_Admin_Default : System.Web.UI.Page
{

    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    int key = 0;
    DataSet ds;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;


    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        if (!IsPostBack)
        {
            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
            }

            fillddl_business();
            test();

            fillddl_category();
            fillddl_business_search();
            fillddl_category_search();

            fillgrid();
            ViewState["sortOrder"] = "";
        }

    }
    protected void fillddl_category_search()
    {
        ddlCategorySearch.Items.Clear();

        if (ddlBusinessSearch.SelectedIndex > 0)
        {
            String s = "select JobFunctionCategory.CategoryName,JobFunctionCategory.Id from JobFunctionCategory inner join WareHouseMaster on WareHouseMaster.WareHouseId=JobFunctionCategory.Whid where JobFunctionCategory.Status='1' and WareHouseMaster.comid='" + Session["Comid"] + "' and WareHouseMaster.WareHouseId='" + ddlBusinessSearch.SelectedValue + "' ";
            da = new SqlDataAdapter(s, con);
            ds = new DataSet();
            da.Fill(ds);

            ddlCategorySearch.DataSource = ds;
            ddlCategorySearch.DataTextField = "CategoryName";
            ddlCategorySearch.DataValueField = "Id";
            ddlCategorySearch.DataBind();
        }
        ddlCategorySearch.Items.Insert(0, "All");
    }

    protected void fillddl_category()
    {
        ddlcategory.Items.Clear();

        String s = "select JobFunctionCategory.CategoryName,JobFunctionCategory.Id from JobFunctionCategory inner join WareHouseMaster on WareHouseMaster.WareHouseId=JobFunctionCategory.Whid where JobFunctionCategory.Status='1' and WareHouseMaster.comid='" + Session["Comid"] + "' and WareHouseMaster.WareHouseId='" + ddlbusiness.SelectedValue + "'";
        da = new SqlDataAdapter(s, con);
        ds = new DataSet();
        da.Fill(ds);

        ddlcategory.DataSource = ds;
        ddlcategory.DataTextField = "CategoryName";
        ddlcategory.DataValueField = "Id";
        ddlcategory.DataBind();

        ddlcategory.Items.Insert(0, "-Select-");
        ddlcategory.Items[0].Value = "0";
    }

    protected void fillddl_business_search()
    {
        ddlBusinessSearch.Items.Clear();

        String s = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["Comid"].ToString() + "' and WareHouseMaster.Status='1' and EmployeeWarehouseRights.AccessAllowed='True' order by name";
        da = new SqlDataAdapter(s, con);
        ds = new DataSet();
        da.Fill(ds);

        ddlBusinessSearch.DataSource = ds;
        ddlBusinessSearch.DataTextField = "Name";
        ddlBusinessSearch.DataValueField = "WareHouseId";
        ddlBusinessSearch.DataBind();

        ddlBusinessSearch.Items.Insert(0, "All");
    }

    protected void ddlBusinessSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddl_category_search();
        fillgrid();
    }

    protected void fillgrid()
    {
        lblBus.Text = ddlBusinessSearch.SelectedItem.Text;

        string str = "";

        if (ddlBusinessSearch.SelectedIndex > 0)
        {
            str += " and JobFunctionCategory.Whid='" + ddlBusinessSearch.SelectedValue + "'";
        }
        if (ddlCategorySearch.SelectedIndex > 0)
        {
            str += " and JobFunctionSubCategory.JobCategoryId='" + ddlCategorySearch.SelectedValue + "'";
        }
        if (ddlstatus_search.SelectedIndex > 0)
        {
            str += " and JobFunctionSubCategory.Status='" + ddlstatus_search.SelectedValue + "'";
        }

        String s = " select JobFunctionSubCategory.Id,WareHouseMaster.Name,JobFunctionCategory.CategoryName,SubCategoryName,case when(JobFunctionSubCategory.Status = '1') then 'Active' else 'Inactive' end as Status from JobFunctionSubCategory inner join JobFunctionCategory on JobFunctionCategory.Id=JobFunctionSubCategory.JobCategoryId inner join WareHouseMaster on WareHouseMaster.WareHouseId=JobFunctionCategory.Whid where WareHouseMaster.comid = '" + Session["Comid"] + "' " + str + " ";
        da = new SqlDataAdapter(s, con);
        ds = new DataSet();
        da.Fill(ds);

        dt = ds.Tables[0];
        GridView1.DataSource = dt;

        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataBind();
    }

    protected void ddlCategorySearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {

            String s = "select * from JobFunctionSubCategory where SubCategoryName = '" + txtSubCategory.Text + "' and JobCategoryId = " + ddlcategory.SelectedValue + "";
            da = new SqlDataAdapter(s, con);
            ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Subcategory name already exist!";
            }
            else
            {
                String s1 = "insert into JobFunctionSubCategory values('" + txtSubCategory.Text + "'," + ddlcategory.SelectedValue + "," + ddlStatus.SelectedValue + ")";
                cmd = new SqlCommand(s1, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
            }
        }

        if (btnSubmit.Text == "Update")
        {
            SqlCommand cmd = new SqlCommand("update JobFunctionSubCategory set SubCategoryName='" + txtSubCategory.Text + "',JobCategoryId='" + ddlcategory.SelectedValue + "',Status='" + ddlStatus.SelectedValue + "' where id='" + ViewState["EID"] + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";
            fillgrid();

            btnSubmit.Text = "Submit";
        }

        ddlcategory.SelectedIndex = 0;
        txtSubCategory.Text = "";
        ddlStatus.SelectedIndex = 0;
        Panel4.Visible = false;
        btnnewcat.Visible = true;

        fillgrid();
        lbllegend.Text = "";

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlcategory.SelectedIndex = 0;
        txtSubCategory.Text = "";
        ddlStatus.SelectedIndex = 0;
        Panel4.Visible = false;
        lblmsg.Visible = false;
        btnnewcat.Visible = true;
        lbllegend.Text = "";
        btnSubmit.Text = "Submit";
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int i = Convert.ToInt32(e.CommandArgument);

            String s = "select * from JobFunction where JobfunctionsubcategoryId = " + i;
            SqlDataAdapter da = new SqlDataAdapter(s, con);
            ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, you are unable to delete this sub category as there are job functions exist using this sub category.";
            }
            else
            {
                CLS_JobFunctionSubCategory jfs = new CLS_JobFunctionSubCategory();
                jfs.Id = i.ToString();
                jfs.cls_delete();

                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully";
                fillgrid();
            }
        }


        if (e.CommandName == "Edit")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            ViewState["EID"] = i;

            String s = "select JobFunctionSubCategory.*,WareHouseMaster.Name,JobFunctionCategory.Whid from JobFunctionSubCategory inner join JobFunctionCategory on JobFunctionCategory.Id=JobFunctionSubCategory.JobCategoryId inner join WareHouseMaster on WareHouseMaster.WareHouseId=JobFunctionCategory.Whid where JobFunctionSubCategory.Id = " + i;
            SqlDataAdapter da = new SqlDataAdapter(s, con);
            DataTable dtf = new DataTable();
            da.Fill(dtf);

            if (dtf.Rows.Count > 0)
            {
                btnSubmit.Text = "Update";

                lbllegend.Text = "Edit Job Function SubCategory";
                lblmsg.Text = "";

                Panel4.Visible = true;
                btnnewcat.Visible = false;

                txtSubCategory.Text = dtf.Rows[0]["SubCategoryName"].ToString();

                if (Convert.ToString(dtf.Rows[0]["Status"]) == "True")
                {
                    ddlStatus.SelectedValue = "1";
                }
                else
                {
                    ddlStatus.SelectedValue = "0";
                }

                fillddl_business();
                test();
                ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dtf.Rows[0]["Whid"].ToString()));

                fillddl_category();
                ddlcategory.SelectedIndex = ddlcategory.Items.IndexOf(ddlcategory.Items.FindByValue(dtf.Rows[0]["JobCategoryId"].ToString()));
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //if (key == 0)
        //{
        //    GridView1.EditIndex = e.NewEditIndex;

        //    Label lblBusiness1 = (Label)(GridView1.Rows[e.NewEditIndex].Cells[0].FindControl("lblBusiness"));

        //    Label lblCategory1 = (Label)(GridView1.Rows[e.NewEditIndex].Cells[0].FindControl("lblCategory"));

        //    fillgrid();

        //    DropDownList ddl = (DropDownList)(GridView1.Rows[e.NewEditIndex].Cells[0].FindControl("ddlBusiness1"));

        //    DropDownList ddlcat2 = (DropDownList)(GridView1.Rows[e.NewEditIndex].Cells[0].FindControl("ddlcat1"));


        //    //filling ddl


        //    //String s = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["Comid"].ToString() + "' and WareHouseMaster.Status='1' and EmployeeWarehouseRights.AccessAllowed='True' order by name";
        //    //da = new SqlDataAdapter(s, con);

        //    //ds = new DataSet();
        //    //da.Fill(ds);

        //    CLS_JobFunctionSubCategory jfs = new CLS_JobFunctionSubCategory();
        //    jfs.comid = Session["Comid"].ToString();
        //    ds = jfs.cls_sp_select_warehouse();

        //    ddl.DataSource = ds;
        //    ddl.DataTextField = "Name";
        //    ddl.DataValueField = "WareHouseId";
        //    ddl.DataBind();
        //    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(lblBusiness1.Text));

        //    /// filling ddlcat2

        //    String s1 = "select * from JobFunctionCategory where Whid = '" + ddl.SelectedValue + "' and Status = 1";
        //    da = new SqlDataAdapter(s1, con);

        //    ds = new DataSet();
        //    da.Fill(ds);

        //    ddlcat2.DataSource = ds;
        //    ddlcat2.DataTextField = "CategoryName";
        //    ddlcat2.DataValueField = "Id";
        //    ddlcat2.DataBind();
        //    ddlcat2.SelectedIndex = ddlcat2.Items.IndexOf(ddlcat2.Items.FindByText(lblCategory1.Text));

        //}
    }
    protected void ddlBusiness1_SelectedIndexChanged(object sender, EventArgs e)
    {


        DropDownList ddlBusiness11 = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlBusiness1");
        //  Label whid = (Label)gridocsubsubtype.Rows[gridocsubsubtype.EditIndex].FindControl("lblw");

        DropDownList ddlcat11 = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlcat1");
        //        Label lblCategory11 = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblCategory");

        //String s = "select * from JobFunctionCategory where Whid = '" + ddlBusiness11.SelectedValue + "'";
        //da = new SqlDataAdapter(s, con);

        //ds = new DataSet();
        //da.Fill(ds);

        CLS_JobFunctionSubCategory jfs = new CLS_JobFunctionSubCategory();
        jfs.Whid = ddlBusiness11.SelectedValue;
        ds = jfs.cls_select1();

        ddlcat11.DataSource = ds;
        ddlcat11.DataTextField = "CategoryName";
        ddlcat11.DataValueField = "Id";
        ddlcat11.DataBind();

    }


    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        //if (DropDownList1.SelectedIndex > 0)
        //{
        //    DropDownList1_SelectedIndexChanged(sender, e);
        //}
        //else
        //{
        fillgrid();
        //}

        lblmsg.Visible = false;
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);

            TextBox txtcat = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtSubcat1");

            CheckBox cb = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("cbxStatus1");

            DropDownList ddlBusiness11 = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlBusiness1");

            DropDownList ddlcat11 = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlcat1");

            //string str1 = "SELECT * from JobFunctionSubCategory  where SubCategoryName ='" + txtcat.Text + "' and JobCategoryId ='" + ddlcat11.SelectedValue + "' and Id<>'" + dk + "'";
            //SqlCommand cmd1 = new SqlCommand(str1, con);
            //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //DataTable dt1 = new DataTable();
            //da1.Fill(dt1);
            CLS_JobFunctionSubCategory jfs = new CLS_JobFunctionSubCategory();
            jfs.SubCategoryName = txtcat.Text;
            jfs.JobCategoryId = ddlcat11.SelectedValue;
            jfs.Id = dk.ToString();
            DataTable dt1 = jfs.cls_selectsubcategory();

            if (dt1.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "SubCategory already exist";
            }

            else
            {
                int i;
                if (cb.Checked == true)
                    i = 1;
                else
                    i = 0;

                string sr51 = ("update JobFunctionSubCategory set SubCategoryName ='" + txtcat.Text + "',JobCategoryId ='" + ddlcat11.SelectedValue + "',Status = " + i + "  where Id='" + dk + "' ");
                SqlCommand cmd801 = new SqlCommand(sr51, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd801.ExecuteNonQuery();
                con.Close();
                //jfs.SubCategoryName = txtcat.Text;
                // jfs.JobCategoryId = ddlcat11.SelectedValue;
                //jfs.Id = dk.ToString();
                //jfs.cls_update();

                GridView1.EditIndex = -1;
                fillgrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";

            }
        }
        catch (Exception ert)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error :" + ert.Message;

        }
    }
    protected void btnPrintVersion_Click(object sender, EventArgs e)
    {
        if (btnPrintVersion.Text == "Printable Version")
        {

            btnPrintVersion.Text = "Hide Printable Version";
            btnPrint.Visible = true;
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }

        }
        else
        {

            btnPrintVersion.Text = "Printable Version";
            btnPrint.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }

        }
    }


    protected void btnnewcat_Click(object sender, EventArgs e)
    {
        Panel4.Visible = true;
        lblmsg.Visible = false;
        btnnewcat.Visible = false;
        lbllegend.Text = "Add New Sub Category";
        fillddl_business();
        test();

        fillddl_category();
    }

    protected void fillddl_business()
    {
        String s = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["Comid"].ToString() + "' and WareHouseMaster.Status='1' and EmployeeWarehouseRights.AccessAllowed='True' order by name";
        da = new SqlDataAdapter(s, con);

        ds = new DataSet();
        da.Fill(ds);

        ddlbusiness.DataSource = ds;
        ddlbusiness.DataTextField = "Name";
        ddlbusiness.DataValueField = "WareHouseId";
        ddlbusiness.DataBind();

    }
    protected void test()
    {
        string ee = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
        SqlCommand cmdeeed = new SqlCommand(ee, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }

    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddl_category();
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


    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}










