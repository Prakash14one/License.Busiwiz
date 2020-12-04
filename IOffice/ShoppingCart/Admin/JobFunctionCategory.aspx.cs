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

public partial class ShoppingCart_Admin_Default2 : System.Web.UI.Page
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

            fillDDl(ddlbusiness, "Name", "WareHouseId");

            test();

            fillDDl(ddlBusinessSearch, "Name", "WareHouseId");

            ddlBusinessSearch.Items.Insert(0, "All");

            fillgrid();

            ViewState["sortOrder"] = "";

        }

    }


    protected void fillDDl(DropDownList ddl, string Textfild, string valfild)
    {

        CLS_JobFunctionCategory jfc = new CLS_JobFunctionCategory();
        jfc.comid = Session["comid"].ToString();

        DataTable dt11 = jfc.cls_fillddl();

        ddl.DataSource = dt11;
        ddl.DataTextField = Textfild;
        ddl.DataValueField = valfild;
        ddl.DataBind();

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //string str1 = "SELECT * from JobFunctionCategory  where CategoryName='" + txtCategoryName.Text + "' and Whid='" + ddlbusiness.SelectedValue + "'";
        //SqlCommand cmd1 = new SqlCommand(str1, con);
        //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);

        if (btnSubmit.Text == "Submit")
        {
            CLS_JobFunctionCategory jfc = new CLS_JobFunctionCategory();
            jfc.Whid = ddlbusiness.SelectedValue;
            jfc.CategoryName = txtCategoryName.Text;

            DataTable dt1 = jfc.cls_jobfunctioncategory2();


            if (dt1.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Category already exist";
            }

            else
            {

                jfc.Name = txtCategoryName.Text;
                jfc.Whid = ddlbusiness.SelectedValue;
                jfc.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                int i = jfc.cls_jobfunctioncategory1();
                if (i > 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully";
                    fillgrid();
                }
            }
        }
        if (btnSubmit.Text == "Update")
        {
            SqlCommand cmd = new SqlCommand("update JobFunctionCategory set CategoryName='" + txtCategoryName.Text + "',Whid='" + ddlbusiness.SelectedValue + "',Status='" + ddlStatus.SelectedValue + "' where id='" + ViewState["EID"] + "'", con);
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

        txtCategoryName.Text = "";
        ddlStatus.SelectedIndex = 0;
        pnladdd.Visible = false;
        btnnewcat.Visible = true;
        lbllegend.Text = "";
    }


    protected void fillgrid()
    {
        string strr = "";

        if (ddlBusinessSearch.SelectedIndex > 0)
        {
            strr = " and j.Whid='" + ddlBusinessSearch.SelectedValue + "'";
        }
        if (ddlstatus_search.SelectedIndex > 0)
        {
            strr += " and j.Status='" + ddlstatus_search.SelectedValue + "'";
        }

        String s = "select Id,Name,CategoryName,case when(j.Status = '1') then 'Active' else 'Inactive' end as Status from JobFunctionCategory j,WareHouseMaster w where w.comid = '" + Session["Comid"].ToString() + "' and j.Whid = w.WareHouseId " + strr + "";

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

        lblBus.Text = ddlBusinessSearch.SelectedItem.Text;
    }


    protected void ddlBusinessSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlBusinessSearch.SelectedIndex > 0)
        //{
        //    //String s = "select Id,Name,CategoryName,j.Status from JobFunctionCategory j,WareHouseMaster w where j.Whid = w.WareHouseId and j.Whid = " + ddlBusinessSearch.SelectedValue + "";
        //    //SqlDataAdapter da = new SqlDataAdapter(s, con);
        //    //ds = new DataSet();
        //    //da.Fill(ds);
        //    CLS_JobFunctionCategory jfc = new CLS_JobFunctionCategory();
        //    jfc.Whid = ddlBusinessSearch.SelectedValue;
        //    DataSet ds = jfc.cls_jobfunctioncategory4();

        //    GridView1.DataSource = ds;
        //    GridView1.DataBind();
        //}
        //else
        //{
        fillgrid();
        //}

    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtCategoryName.Text = "";
        ddlStatus.SelectedIndex = 0;
        pnladdd.Visible = false;
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

            String s = "select * from JobFunctionSubCategory where JobCategoryId = " + i;
            SqlDataAdapter da = new SqlDataAdapter(s, con);
            ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, you are unable to delete this category as there are sub categories exist for this category.";
            }
            else
            {
                String s1 = "delete from JobFunctionCategory where Id = " + i;

                cmd = new SqlCommand(s1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();

                con.Close();

                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully";

                fillgrid();

            }
        }

        if (e.CommandName == "Edit")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            ViewState["EID"] = i;

            String s = "select * from JobFunctionCategory where Id = " + i;
            SqlDataAdapter da = new SqlDataAdapter(s, con);
            DataTable dtf = new DataTable();
            da.Fill(dtf);

            if (dtf.Rows.Count > 0)
            {
                btnSubmit.Text = "Update";

                lbllegend.Text = "Edit Job Category";
                lblmsg.Text = "";

                pnladdd.Visible = true;
                btnnewcat.Visible = false;

                txtCategoryName.Text = dtf.Rows[0]["CategoryName"].ToString();

                if (Convert.ToString(dtf.Rows[0]["Status"]) == "True")
                {
                    ddlStatus.SelectedValue = "1";
                }
                else
                {
                    ddlStatus.SelectedValue = "0";
                }

                fillDDl(ddlbusiness, "Name", "WareHouseId");

                ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dtf.Rows[0]["Whid"].ToString()));
            }
        }
    }


    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //if (key == 0)
        //{
        //    GridView1.EditIndex = e.NewEditIndex;

        //    Label lblBusiness1 = (Label)(GridView1.Rows[e.NewEditIndex].Cells[0].FindControl("lblBusiness"));

        //    fillgrid();

        //    DropDownList ddl = (DropDownList)(GridView1.Rows[e.NewEditIndex].Cells[0].FindControl("ddlBusiness1"));

        //    fillDDl(ddl, "Name", "WareHouseId");

        //    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(lblBusiness1.Text));

        //}
    }


    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);


            TextBox cat = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCategory1");

            DropDownList wh = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlBusiness1");

            CheckBox cb = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("cbxStatus1");

            //string str1 = "SELECT * from JobFunctionCategory  where CategoryName ='" + cat.Text + "' and Whid='" + wh.SelectedValue + "' and Id<>'" + dk + "'";
            //SqlCommand cmd1 = new SqlCommand(str1, con);
            //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //DataTable dt1 = new DataTable();
            //da1.Fill(dt1);
            CLS_JobFunctionCategory jfc = new CLS_JobFunctionCategory();
            jfc.Whid = wh.SelectedValue;
            jfc.CategoryName = cat.Text;
            jfc.Id = dk.ToString();

            DataTable dt1 = jfc.cls_jobfunctioncategory5();

            if (dt1.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Category already exist";
            }

            else
            {
                int i;
                if (cb.Checked == true)
                    i = 1;
                else
                    i = 0;

                string sr51 = ("update JobFunctionCategory set CategoryName ='" + cat.Text + "',Whid='" + wh.SelectedValue + "',Status = " + i + " where Id='" + dk + "' ");
                SqlCommand cmd801 = new SqlCommand(sr51, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd801.ExecuteNonQuery();
                con.Close();
                //jfc.Whid = wh.SelectedValue;
                //jfc.CategoryName = cat.Text;
                //jfc.Id = dk.ToString();
                //jfc.cls_update_jobfunction();

                GridView1.EditIndex = -1;
                fillgrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";
                //ddl();
            }


        }
        catch (Exception ert)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error :" + ert.Message;

        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

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



    protected void btnPrintVersion_Click(object sender, EventArgs e)
    {
        if (btnPrintVersion.Text == "Printable Version")
        {

            btnPrintVersion.Text = "Hide Printable Version";
            btnPrint.Visible = true;

            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[3].Visible = false;
            }
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }

        }
        else
        {
            btnPrintVersion.Text = "Printable Version";
            btnPrint.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }

        }
    }

    protected void btnnewcat_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = true;
        lblmsg.Visible = false;
        btnnewcat.Visible = false;
        lbllegend.Text = "Add New Job Category";
        fillDDl(ddlbusiness, "Name", "WareHouseId");
        test();
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