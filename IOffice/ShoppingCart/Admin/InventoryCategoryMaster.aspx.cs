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
//using System.Windows.Forms;

public partial class InventoryCategoryMaster : System.Web.UI.Page
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
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


        Page.Title = pg.getPageTitle(page);

        statuslable.Visible = false;
        //Session["pagename"] = "WizardInventoryCategoryMaster.aspx";
        //Session["pnl1"] = "8";

        if (!IsPostBack)
        {

            Pagecontrol.dypcontrol(Page, page);

            ViewState["sortOrder"] = "";


            ModalPopupExtender1.Hide();

            FillGridView1();
        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {


        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

        FillGridView1();


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



    protected void btnsubmit_Click1(object sender, ImageClickEventArgs e)
    {

        string sgw = "select InventoryCatName from InventoryCategoryMaster where " +
            " InventoryCatName='" + txtInventoryCatName.Text + "' and compid='" + Session["comid"] + "' and InventoryCategoryMaster.CatType IS NULL ";
        SqlCommand cgw = new SqlCommand(sgw, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dtgw = new DataTable();
        adgw.Fill(dtgw);

        if (dtgw.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "This record already exists.";


        }
        else
        {
            try
            {
                string catname = "insert into InventoryCategoryMaster(InventoryCatName,Activestatus,compid) values('" + txtInventoryCatName.Text + "','" + ddlstatus.SelectedValue + "','" + Session["comid"] + "')";
                SqlCommand mycmd = new SqlCommand(catname, con);

                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                //SqlCommand mycmd = new SqlCommand("Sp_Insert_InventoryCategoryMaster", con);
                //mycmd.CommandType = CommandType.StoredProcedure;
                //mycmd.Parameters.AddWithValue("@InventoryCatName", txtInventoryCatName.Text);
                con.Open();
                mycmd.ExecuteNonQuery();
                con.Close();
                FillGridView1();
                //GridView1.DataBind();
                txtInventoryCatName.Text = "";
                // CheckBox1.Checked = false;
                statuslable.Visible = true;
                statuslable.Text = "Record inserted successfully";
            }
            catch (Exception ererer)
            {
                statuslable.Visible = true;
                statuslable.Text = "error ;" + ererer.Message;

            }
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Sort")
        {
            return;
        }

        if (e.CommandName == "del")
        {


            ViewState["dle"] = 0;

            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            Session["ID9"] = GridView1.SelectedDataKey.Value;



            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);


            string s = "SELECT     InventorySubCatId FROM     InventorySubCategoryMaster WHERE  InventoryCategoryMasterId = " + Session["ID9"] + "";
            SqlCommand c = new SqlCommand(s, con);
            c.CommandType = CommandType.Text;
            SqlDataAdapter d = new SqlDataAdapter(c);
            DataSet ds0 = new DataSet();
            d.Fill(ds0);


            int a = Convert.ToInt32(ds0.Tables[0].Rows.Count);
            if (a == 0)
            {

                ViewState["dle"] = 1;
                DeleteStatus.Value = "1";

            }
            else
            {
                GridView2.DataSource = (DataSet)getSub();
                GridView2.DataBind();

                lblTotal.Text = a.ToString();

                if (lblTotal.Text == "1")
                {
                    Label10.Text = "sub category. Please move it before deleting.";
                }
                else
                {
                    Label10.Text = "sub categories. Please move them before deleting.";

                }

                foreach (GridViewRow gdr in GridView2.Rows)
                {
                    DropDownList drp = (DropDownList)gdr.Cells[0].FindControl("DropDownList2");

                    drp.DataSource = (DataSet)fillddl();
                    drp.DataTextField = "InventoryCatName";
                    drp.DataValueField = "InventeroyCatId";
                    drp.DataBind();


                    DataSet ds = new DataSet();
                    ds = (DataSet)getSub();



                    drp.SelectedIndex = drp.Items.IndexOf(drp.Items.FindByValue(ds.Tables[0].Rows[0]["InventeroyCatId"].ToString()));

                }
                ViewState["dle"] = 2;
                DeleteStatus.Value = "2";
                ModalPopupExtender1.Show();
            }


        }
        if (e.CommandName == "Edit")
        {

            ViewState["editid"] = Convert.ToInt32(e.CommandArgument);

            ddlcattype.Enabled = false;

            string str = "select * from InventoryCategoryMaster where InventeroyCatId='" + ViewState["editid"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                txtInventoryCatName.Text = dt.Rows[0]["InventoryCatName"].ToString();
                string chk = dt.Rows[0]["Activestatus"].ToString();

                if (chk == "True")
                {

                    ddlstatus.SelectedValue = "1";
                }
                else
                {
                    ddlstatus.SelectedValue = "0";
                }
                if (Convert.ToString(dt.Rows[0]["CatType"]) != "")
                {
                    ddlcattype.SelectedIndex = 1;
                }
                else
                {
                    ddlcattype.SelectedIndex = 0;
                }


                pnladd.Visible = true;
                lbllegend.Visible = true;
                lbllegend.Text = "Edit Category For Your Inventory";
                btnadd.Visible = false;
                Label3.Text = "Edit Category";

                btnupdate.Visible = true;
                ImageButton1.Visible = false;

            }


        }
    }

    public DataSet getSub()
    {
        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
        // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        Mycommand = new SqlCommand("SELECT     InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId, " +
                      " InventoryCategoryMaster.InventoryCatName " +
                        " FROM         InventorySubCategoryMaster INNER JOIN " +
                      " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
                    " WHERE     (InventoryCategoryMaster.InventeroyCatId = " + Session["ID9"] + "  ) order by InventoryCategoryMaster.InventoryCatName ", con);

        Mycommand.CommandType = CommandType.Text;
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }

        MyDataAdapter = new SqlDataAdapter(Mycommand);
        MyDataAdapter.Fill(ds);
        con.Close();
        return ds;
    }
    public DataSet fillddl()
    {
        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
        // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        Mycommand = new SqlCommand("select * from InventoryCategoryMaster where compid='" + Session["comid"] + "'  and InventoryCategoryMaster.CatType IS NULL  order by InventoryCatName", con);
        Mycommand.CommandType = CommandType.Text;
        Mycommand.Connection.Open();
        MyDataAdapter = new SqlDataAdapter(Mycommand);
        MyDataAdapter.Fill(ds);
        con.Close();
        return ds;
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //System.Windows.Forms.MessageBox.Show(
        //       "Deleting " + "2" + " row(s). Continue ?",
        //       "Delete rows?",
        //       System.Windows.Forms.MessageBoxButtons.YesNo,
        //       System.Windows.Forms.MessageBoxIcon.Question);
        ////GridView1.EditIndex = -1;
        ////FillGridView1();
        ViewState["dle"] = 0;

        GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());

        Session["ID9"] = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string s = "SELECT     InventorySubCatId FROM     InventorySubCategoryMaster WHERE  InventoryCategoryMasterId = " + Session["ID9"] + "";
        SqlCommand c = new SqlCommand(s, con);
        c.CommandType = CommandType.Text;
        SqlDataAdapter d = new SqlDataAdapter(c);
        DataSet ds0 = new DataSet();
        d.Fill(ds0);


        int a = Convert.ToInt32(ds0.Tables[0].Rows.Count);
        if (a == 0)
        {
            ViewState["dle"] = 1;
            DeleteStatus.Value = "1";
            // ModalPopupExtender1222.Show();
            yes_Click(sender, e);
        }
        else
        {
            GridView2.DataSource = (DataSet)getSub();
            GridView2.DataBind();
            lblTotal.Text = a.ToString();

            if (lblTotal.Text == "1")
            {
                Label10.Text = "sub category. Please move it before deleting.";
            }
            else
            {
                Label10.Text = "sub categories. Please move them before deleting.";

            }
            int i = 0;
            foreach (GridViewRow gdr in GridView2.Rows)
            {
                DropDownList drp = (DropDownList)gdr.Cells[0].FindControl("DropDownList2");

                drp.DataSource = (DataSet)fillddl();
                drp.DataTextField = "InventoryCatName";
                drp.DataValueField = "InventeroyCatId";
                drp.DataBind();


                DataSet ds = new DataSet();
                ds = (DataSet)getSub();
                //drp.SelectedValue = ;


                drp.SelectedIndex = drp.Items.IndexOf(drp.Items.FindByValue(ds.Tables[0].Rows[0]["InventeroyCatId"].ToString()));
                drp.Items.RemoveAt(drp.SelectedIndex);
                //Session["index"] = drp.SelectedIndex;

                if (drp.Items.Count <= 0)
                {
                    i = 1;
                }
            }
            ViewState["dle"] = 2;
            DeleteStatus.Value = "2";
            if (i == 1)
            {
                Button3.Enabled = false;
            }
            else
            {
                Button3.Enabled = true;
            }
            ModalPopupExtender1.Show();
        }
    }
    protected void HiddenButton_Click(object sender, EventArgs e)
    {

    }


    protected void FillGridView1()
    {
        lblCompany.Text = Session["Cname"].ToString();
        string st1 = "";

        if (ddlActive.SelectedIndex > 0)
        {
            st1 = " and InventoryCategoryMaster.Activestatus = '" + ddlActive.SelectedValue + "'";
        }

        string strfillgrid = "";

        strfillgrid = " InventoryCategoryMaster.*, case when (InventoryCategoryMaster.Activestatus='1') then 'Active' else 'Inactive' End as Statuslabel,case when (InventoryCategoryMaster.CatType IS NULL) then 'Inventory' else 'Service' End as CatType1 FROM InventoryCategoryMaster  where compid='" + Session["comid"] + "' " + st1 + "";

        string str2 = "SELECT count(InventoryCategoryMaster.InventeroyCatId) as ci FROM InventoryCategoryMaster  where compid='" + Session["comid"] + "' " + st1 + "";

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " InventoryCatName asc";


        lblstatus.Text = ddlActive.SelectedItem.Text;

        //strfillgrid = strfillgrid + st1 + st2;        
        //SqlCommand cmdfillgrid = new SqlCommand(strfillgrid, con);
        //SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
        //DataTable dtfill = new DataTable();
        //adpfillgrid.Fill(dtfill);

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dtfill = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, strfillgrid);

            GridView1.DataSource = dtfill;
            DataView myDataView = new DataView();
            myDataView = dtfill.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
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

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //ViewState["editid"] = GridView1.DataKeys[e.NewEditIndex].Value.ToString();

        //string str = "select * from InventoryCategoryMaster where InventeroyCatId='" + ViewState["editid"] + "'";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{

        //    txtInventoryCatName.Text = dt.Rows[0]["InventoryCatName"].ToString();
        //    string chk = dt.Rows[0]["Activestatus"].ToString();

        //    if (chk == "True")
        //    {

        //        ddlstatus.SelectedValue = "1";
        //    }
        //    else
        //    {
        //        ddlstatus.SelectedValue = "0";
        //    }



        //    pnladd.Visible = true;
        //    lbllegend.Visible = true;
        //    lbllegend.Text = "Edit Category For Your Inventory";
        //    Label3.Text = "Edit Category";

        //    btnupdate.Visible = true;
        //    ImageButton1.Visible = false;
        //    btnadd.Visible = false;
        //}
        //FillGridView1();

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGridView1();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        //try
        //{
        //    int i;
        //    TextBox catname = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCatName");
        //    CheckBox chk1 = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("CheckBox2");
        //    if (chk1.Checked == true)
        //    {
        //        i = 1;
        //    }
        //    else
        //    {
        //        i = 0;
        //    }
        //    Label catid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblCatId");
        //    string sgw = "select InventoryCatName from InventoryCategoryMaster where " +
        //        " InventoryCatName='" + catname.Text + "' and InventeroyCatId<>'" + catid.Text + "' and InventoryCategoryMaster.CatType IS NULL and InventoryCategoryMaster.compid='"+Session["comid"]+"' ";
        //    SqlCommand cgw = new SqlCommand(sgw, con);
        //    SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        //    DataTable dtgw = new DataTable();
        //    adgw.Fill(dtgw);

        //    if (dtgw.Rows.Count > 0)
        //    {
        //        statuslable.Visible = true;
        //        statuslable.Text = "Record Already Exist";


        //    }
        //    else
        //    {
        //        bool access = UserAccess.Usercon("InventoryCategoryMaster", "", "InventeroyCatId", "", "", "InventoryCategoryMaster.compid", "InventoryCategoryMaster");
        //        if (access == true)
        //        {
        //            string sr51 = ("update InventoryCategoryMaster set Activestatus='" + i + "',InventoryCatName='" + catname.Text + "'  where InventeroyCatId='" + catid.Text + "' ");
        //            SqlCommand cmd801 = new SqlCommand(sr51, con);

        //            con.Open();
        //            cmd801.ExecuteNonQuery();
        //            con.Close();
        //            GridView1.EditIndex = -1;
        //            FillGridView1();
        //            statuslable.Visible = true;
        //            statuslable.Text = "Record Update Successfylly";
        //        }
        //        else
        //        {
        //            statuslable.Visible = true;
        //            statuslable.Text = "Sorry, You don't permited greter record to priceplan";
        //        }
        //    }
        //}
        //catch (Exception ert)
        //{
        //    statuslable.Visible = true;
        //    statuslable.Text = "Error :" + ert.Message;

        //}
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGridView1();
    }

    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        txtInventoryCatName.Text = "";
        // CheckBox1.Checked = false;
        statuslable.Visible = false;
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        string sgw = "select InventoryCatName from InventoryCategoryMaster where " +
            " InventoryCatName='" + txtInventoryCatName.Text + "' and compid='" + Session["comid"] + "' and InventoryCategoryMaster.CatType IS NULL ";
        SqlCommand cgw = new SqlCommand(sgw, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dtgw = new DataTable();
        adgw.Fill(dtgw);

        if (dtgw.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "This record already exists.";


        }
        else
        {
            bool access = UserAccess.Usercon("InventoryCategoryMaster", "", "InventeroyCatId", "", "", "InventoryCategoryMaster.compid", "InventoryCategoryMaster ");
            if (access == true)
            {
                try
                {
                    string catname = "";
                    if (ddlcattype.SelectedIndex == 0)
                    {
                        catname = "insert into InventoryCategoryMaster(InventoryCatName,Activestatus,compid) values('" + txtInventoryCatName.Text + "','" + ddlstatus.SelectedValue + "','" + Session["comid"] + "')";
                    }
                    else
                    {
                        catname = "insert into InventoryCategoryMaster(InventoryCatName,Activestatus,compid,CatType) values('" + txtInventoryCatName.Text + "','" + ddlstatus.SelectedValue + "','" + Session["comid"] + "','0')";

                    }
                    SqlCommand mycmd = new SqlCommand(catname, con);

                    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    //SqlCommand mycmd = new SqlCommand("Sp_Insert_InventoryCategoryMaster", con);
                    //mycmd.CommandType = CommandType.StoredProcedure;
                    //mycmd.Parameters.AddWithValue("@InventoryCatName", txtInventoryCatName.Text);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();

                    }

                    mycmd.ExecuteNonQuery();
                    con.Close();
                    FillGridView1();
                    //GridView1.DataBind();
                    txtInventoryCatName.Text = "";

                    //  CheckBox1.Checked = false;

                    statuslable.Visible = true;
                    statuslable.Text = "Record inserted successfully";

                    pnladd.Visible = false;
                    lbllegend.Visible = false;
                    btnadd.Visible = true;
                    lbllegend.Text = "Add a New Category For Your Inventory";
                    Label3.Text = "Add Category";

                    ddlstatus.SelectedIndex = 0;
                }
                catch (Exception ererer)
                {
                    statuslable.Visible = true;
                    statuslable.Text = "error ;" + ererer.Message;

                }
            }
            else
            {
                statuslable.Visible = true;
                statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
            }
        }
    }
    protected void ImageButton6_Click1(object sender, EventArgs e)
    {
        txtInventoryCatName.Text = "";
        //CheckBox1.Checked = true;
        statuslable.Visible = false;
        if (btnupdate.Visible == true)
        {
            ImageButton1.Visible = true;
            btnupdate.Visible = false;
        }
        ddlcattype.Enabled = true;
        pnladd.Visible = false;
        lbllegend.Text = "Add a New Category For Your Inventory";
        Label3.Text = "Add Category";
        lbllegend.Visible = false;
        btnadd.Visible = true;
        GridView1.EditIndex = -1;
        FillGridView1();
        ddlstatus.SelectedIndex = 0;
    }
    protected void ImgBtnMove_Click(object sender, EventArgs e)
    {

        yes_Click(sender, e);

    }
    //protected void ImageButton4_Click(object sender, EventArgs e)
    //{
    //    ModalPopupExtender1.Hide();
    //}
    protected void yes_Click(object sender, EventArgs e)
    {
        if (ViewState["dle"].ToString() == "1")
        {
            string sr4 = ("delete from InventoryCategoryMaster where InventeroyCatId=" + Session["ID9"] + "");
            SqlCommand cmd8 = new SqlCommand(sr4, con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd8.ExecuteNonQuery();
            con.Close();

            statuslable.Visible = true;
            statuslable.Text = "Record deleted successfully";
            FillGridView1();
            //GridView1.DataSource = SqlDataSource1;
            // GridView1.DataBind();
            //ModalPopupExtender1222.Hide();
            // Page_Load(sender, e);
        }
        if (ViewState["dle"].ToString() == "2")
        {



            try
            {
                int i = 0;
                foreach (GridViewRow gdr in GridView2.Rows)
                {

                    DropDownList drp = (DropDownList)gdr.Cells[0].FindControl("DropDownList2");

                    if (drp.Items.Count <= 0)
                    {
                        i = 1;

                    }
                    if (i == 0)
                    {
                        string st = "Update InventorySubCategoryMaster set InventoryCategoryMasterId=" + drp.SelectedValue + " where InventorySubCatId=" + Convert.ToInt32(GridView2.DataKeys[gdr.RowIndex].Value) + "";
                        SqlCommand Mycommand = new SqlCommand(st, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        Mycommand.ExecuteNonQuery();
                        con.Close();
                    }

                }
                if (i == 0)
                {
                    string s = "SELECT     InventorySubCatId FROM     InventorySubCategoryMaster WHERE  InventoryCategoryMasterId = " + Session["ID9"] + "";
                    SqlCommand c = new SqlCommand(s, con);
                    c.CommandType = CommandType.Text;
                    SqlDataAdapter d = new SqlDataAdapter(c);
                    DataTable ds0 = new DataTable();
                    d.Fill(ds0);
                    if (ds0.Rows.Count <= 0)
                    {
                        string sr5 = ("delete from InventoryCategoryMaster where InventeroyCatId=" + Session["ID9"] + "");
                        SqlCommand cmd80 = new SqlCommand(sr5, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd80.ExecuteNonQuery();
                        con.Close();
                        statuslable.Visible = true;
                        statuslable.Text = "Record deleted successfully";
                    }
                    FillGridView1();
                }
                else
                {
                    statuslable.Visible = true;
                    statuslable.Text = "Sorry,You have no other category to move so you cannot delete this category ";
                }
            }
            catch (Exception ery)
            {
                statuslable.Visible = true;
                statuslable.Text = "Error : " + ery.Message;
            }
            finally
            {


            }

            ModalPopupExtender1.Hide();
            FillGridView1();

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ViewState["dle"].ToString() == "1")
        {

        }
        if (ViewState["dle"].ToString() == "2")
        {
            ModalPopupExtender1.Show();
        }
    }
    protected void ddlActive_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridView1();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            FillGridView1();

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
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

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(250);

            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            FillGridView1();

            Button1.Text = "Printable Version";
            Button7.Visible = false;

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
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            lbllegend.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            lbllegend.Visible = false;
        }
        btnadd.Visible = false;
        statuslable.Text = "";
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string sgw = "select InventoryCatName from InventoryCategoryMaster where " +
                " InventoryCatName='" + txtInventoryCatName.Text + "' and InventeroyCatId<>'" + ViewState["editid"] + "' and InventoryCategoryMaster.CatType IS NULL and InventoryCategoryMaster.compid='" + Session["comid"] + "' ";
        SqlCommand cgw = new SqlCommand(sgw, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dtgw = new DataTable();
        adgw.Fill(dtgw);

        if (dtgw.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "This record already exists.";


        }
        else
        {
            bool access = UserAccess.Usercon("InventoryCategoryMaster", "", "InventeroyCatId", "", "", "InventoryCategoryMaster.compid", "InventoryCategoryMaster");
            if (access == true)
            {
                string sr51 = "";
                if (ddlcattype.SelectedIndex == 0)
                {
                    sr51 = ("update InventoryCategoryMaster set Activestatus='" + ddlstatus.SelectedValue + "',InventoryCatName='" + txtInventoryCatName.Text + "'  where InventeroyCatId='" + ViewState["editid"] + "' ");
                }
                else
                {
                    sr51 = ("update InventoryCategoryMaster set Activestatus='" + ddlstatus.SelectedValue + "',InventoryCatName='" + txtInventoryCatName.Text + "',CatType='0'  where InventeroyCatId='" + ViewState["editid"] + "' ");

                }
                SqlCommand cmd801 = new SqlCommand(sr51, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd801.ExecuteNonQuery();
                con.Close();

                FillGridView1();
                ddlcattype.Enabled = true;

                txtInventoryCatName.Text = "";
                // CheckBox1.Checked = false;
                statuslable.Visible = true;
                statuslable.Text = "Record updated successfully";
                btnupdate.Visible = false;
                ImageButton1.Visible = true;
                pnladd.Visible = false;
                lbllegend.Visible = false;
                lbllegend.Text = "Add a New Category For Your Inventory";
                Label3.Text = "Add Category";
                btnadd.Visible = true;
                GridView1.EditIndex = -1;
                FillGridView1();
                ddlstatus.SelectedIndex = 0;
            }
            else
            {
                statuslable.Visible = true;
                statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
            }
        }
    }
}
