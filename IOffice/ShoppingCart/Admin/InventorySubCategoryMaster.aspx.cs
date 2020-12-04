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


public partial class InventorySubCategoryMaster : System.Web.UI.Page
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con;
    string comp;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        comp = Session["comid"].ToString();
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        statuslable.Visible = false;
        if (!IsPostBack)
        {

            Pagecontrol.dypcontrol(Page, page);


            ViewState["sortOrder"] = "";
         
            ddlCat.DataSource = getall();
            ddlCat.DataValueField = "InventeroyCatId";
            ddlCat.DataTextField = "InventoryCatName";
            ddlCat.DataBind();
           
            ddlCat.Items.Insert(0, "All");
            ddlCat.SelectedItem.Value = "0";

            cat();
            FillGrid1();
        }
    }

    protected void cat()
    {
        ddlInventoryCategoryMasterId.Items.Clear();
        ddlInventoryCategoryMasterId.DataSource = getall();
        ddlInventoryCategoryMasterId.DataValueField = "InventeroyCatId";
        ddlInventoryCategoryMasterId.DataTextField = "InventoryCatName";
        ddlInventoryCategoryMasterId.DataBind();
       
        object sen = new object();
        EventArgs gg = new EventArgs();

        ddlCat_SelectedIndexChanged(sen, gg);
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
    public DataSet getall()
    {

        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        //Mycommand = new SqlCommand(" Sp_Select_InventoryCategoryMaster ", con);
        string str = "select InventeroyCatId, inventorycatname from inventorycategorymaster where compid='" + comp + "' and Activestatus=1 order by inventorycatname";

        if (con.State.ToString() != "Open")
        {
            con.Open();

        }


        //Mycommand.CommandType = CommandType.Text;
        //Mycommand.Connection.Open();
        MyDataAdapter = new SqlDataAdapter(str, con);
        MyDataAdapter.Fill(ds);
        con.Close();
        return ds;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }
        if (e.CommandName == "Delete1")
        {


        }


        if (e.CommandName == "TotalItem")
        {


            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            Session["ID9"] = GridView1.SelectedDataKey.Value;



        }
        if (e.CommandName == "Edit")
        {
            ViewState["editid"] = Convert.ToInt32(e.CommandArgument);

            //  ViewState["editid"] = Convert.ToInt32(e.CommandArgument);
            string str = "Select * from InventorySubCategoryMaster where InventorySubCatId='" + ViewState["editid"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                cat();
                ddlInventoryCategoryMasterId.SelectedIndex = ddlInventoryCategoryMasterId.Items.IndexOf(ddlInventoryCategoryMasterId.Items.FindByValue(dt.Rows[0]["InventoryCategoryMasterId"].ToString()));
                txtInventorySubCatName.Text = dt.Rows[0]["InventorySubCatName"].ToString();

                string chk = dt.Rows[0]["Activestatus"].ToString();

                if (chk == "True")
                {
                    //chk123.Checked = true;
                    ddlstatus.SelectedValue = "1";
                }
                else
                {
                    //chk123.Checked = false;
                    ddlstatus.SelectedValue = "0";
                }
                pnladd.Visible = true;
                lbllegend.Visible = true;
                lbllegend.Text = "Edit Sub Category for Your Inventory";
                btnadd.Visible = false;
                Button3.Visible = false;
                btnupdate.Visible = true;
            }

        }
    }

    //protected void btnsubmit_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        string sgw = "select InventorySubCatName from InventorySubCategoryMaster where  " +
    //            " InventorySubCatName='" + txtInventorySubCatName.Text + "' and InventoryCategoryMasterId='" + ddlInventoryCategoryMasterId.SelectedValue + "' ";
    //        SqlCommand cgw = new SqlCommand(sgw, con);
    //        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
    //        DataTable dtgw = new DataTable();
    //        adgw.Fill(dtgw);
    //        if (dtgw.Rows.Count > 0)
    //        {
    //            statuslable.Visible = true;
    //            statuslable.Text = "Record Already Exist";


    //        }
    //        else
    //        {

    //            try
    //            {
    //                int val1234 = Convert.ToInt32(ddlInventoryCategoryMasterId.SelectedValue.ToString());
    //                // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    //                //            SqlCommand mycmd = new SqlCommand("Sp_Insert_InventorySubCategoryMaster", con);
    //                //            mycmd.CommandType = CommandType.StoredProcedure;

    //                //            mycmd.Parameters.AddWithValue("@InventorySubCatName", txtInventorySubCatName.Text);
    //                //            mycmd.Parameters.AddWithValue("@InventoryCategoryMasterId", int.Parse(ddlInventoryCategoryMasterId.SelectedValue.ToString()));
    //                //            con.Open();
    //                //            mycmd.ExecuteNonQuery();
    //                //            con.Close();
    //                //            ddlInventoryCategoryMasterId.SelectedIndex = -1;
    //                //            txtInventorySubCatName.Text = "";
    //                //            statuslable.Visible = true;
    //                //            statuslable.Text = "Record Inserted Successfully";
    //                //            //GridView1.DataBind();
    //                //            //cat();
    //                //            FillGrid1();
    //                //            btGo_Click(sender, e);
    //                //            object sen = new object();
    //                //            EventArgs gg = new EventArgs();

    //                //            ddlCat_SelectedIndexChanged(sen, gg);
    //                //        }
    //                //        catch (Exception erererer)
    //                //        {
    //                //            statuslable.Visible = true;
    //                //            statuslable.Text = "Error : " + erererer.Message;
    //                //        }
    //                //        finally { }
    //                //    }
    //                //}
    //                //catch (Exception ert)
    //                //{
    //                //    statuslable.Visible = true;
    //                //    statuslable.Text = "error";
    //                //}
    //                //}
    //                con.Close();
    //                string queryinsert = "insert into  InventorySubCategoryMaster(InventorySubCatName,InventoryCategoryMasterId,Activestatus)  values('" + txtInventorySubCatName.Text + "'," + val1234 + ",'" + chk123.Checked + "')";
    //                SqlCommand mycmd = new SqlCommand(queryinsert, con);
    //                con.Open();
    //                mycmd.ExecuteNonQuery();
    //                con.Close();
    //                ddlInventoryCategoryMasterId.SelectedIndex = -1;
    //                txtInventorySubCatName.Text = "";
    //                statuslable.Visible = true;
    //                statuslable.Text = "Record Inserted Successfully";
    //                //GridView1.DataBind();
    //                //cat();
    //                FillGrid1();
    //                btGo_Click(sender, e);
    //                object sen = new object();
    //                EventArgs gg = new EventArgs();

    //                ddlCat_SelectedIndexChanged(sen, gg);
    //            }
    //            catch (Exception erererer)
    //            {
    //                statuslable.Visible = true;
    //                statuslable.Text = "Error : " + erererer.Message;
    //            }
    //            finally { }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        statuslable.Visible = true;
    //        statuslable.Text = "error";
    //    }

    //}

    public DataSet getSubSub()
    {
        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
        // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        Mycommand = new SqlCommand("SELECT     InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, " +
              "        InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubSubId " +
" FROM         InventorySubCategoryMaster LEFT OUTER JOIN  " +
  "                    InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
 " where  InventorySubCategoryMaster.InventorySubCatId=" + Session["ID9"] + " ", con);
        Mycommand.CommandType = CommandType.Text;
        Mycommand.Connection.Open();
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
        Mycommand = new SqlCommand("select * from InventorySubCategoryMaster inner join InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where compid='" + Session["comid"] + "' Order by InventorySubCatName  ", con);
        Mycommand.CommandType = CommandType.Text;
        Mycommand.Connection.Open();
        MyDataAdapter = new SqlDataAdapter(Mycommand);
        MyDataAdapter.Fill(ds);
        con.Close();
        return ds;
    }
    protected void ImgBtnMove_Click(object sender, EventArgs e)
    {
        //if (MessageBox.Show("You sure, you want to Delete! ", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        //{
        ViewState["dle"] = 2;
        //ModalPopupExtender1222.Show();
        yes_Click(sender, e);
        FillGrid1();
        //GridView1.DataBind();
        // System.Threading.Thread.Sleep(1000);
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    //protected void ImageButton4_Click(object sender, EventArgs e)
    //{
    //    ModalPopupExtender1.Hide();
    //}
    protected void FillGrid1()
    {
        string strfillgrid = "";
        string str2="";

        if (ddlActive.SelectedIndex > 0)
        {
            lblStatus.Text = ddlActive.SelectedItem.Text;
            if (ddlCat.SelectedIndex > 0)
            {
                lblCate.Text = ddlCat.SelectedItem.Text;

                strfillgrid = " InventorySubCategoryMaster.Activestatus , case when (InventorySubCategoryMaster.Activestatus='1') then 'Active' else 'Inactive' End as Statuslabel,InventorySubCategoryMaster.InventorySubCatId,InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId,  " +
                              " InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster RIGHT OUTER JOIN " +
                              "InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId where compid='" + comp + "' and InventoryCategoryMaster.Activestatus = '1' and InventoryCategoryMaster.InventeroyCatId ='" + ddlCat.SelectedValue + "' and InventorySubCategoryMaster.Activestatus = '" + ddlActive.SelectedValue + "' ";

                str2 = "SELECT count(InventorySubCategoryMaster.InventorySubCatId) as ci FROM InventoryCategoryMaster RIGHT OUTER JOIN InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId where compid='" + comp + "' and InventoryCategoryMaster.Activestatus = '1' and InventoryCategoryMaster.InventeroyCatId ='" + ddlCat.SelectedValue + "' and InventorySubCategoryMaster.Activestatus = '" + ddlActive.SelectedValue + "' ";
            }
            else
            {
                strfillgrid = " InventorySubCategoryMaster.Activestatus ,case when (InventorySubCategoryMaster.Activestatus='1') then 'Active' else 'Inactive' End as Statuslabel, InventorySubCategoryMaster.InventorySubCatId,InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId,  " +
                              " InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster RIGHT OUTER JOIN " +
                              " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId where compid='" + comp + "' and InventoryCategoryMaster.Activestatus = '1'  and InventorySubCategoryMaster.Activestatus = '" + ddlActive.SelectedValue + "' ";

                str2 = "SELECT count(InventorySubCategoryMaster.InventorySubCatId) as ci FROM InventoryCategoryMaster RIGHT OUTER JOIN InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId where compid='" + comp + "' and InventoryCategoryMaster.Activestatus = '1'  and InventorySubCategoryMaster.Activestatus = '" + ddlActive.SelectedValue + "' ";
            }
        }
        else
        {
            if (ddlCat.SelectedIndex > 0)
            {
                strfillgrid = " InventorySubCategoryMaster.Activestatus ,case when (InventorySubCategoryMaster.Activestatus='1') then 'Active' else 'Inactive' End as Statuslabel, InventorySubCategoryMaster.InventorySubCatId,InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId,  " +
                              " InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster RIGHT OUTER JOIN " +
                              " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId where compid='" + comp + "'  and InventoryCategoryMaster.InventeroyCatId ='" + ddlCat.SelectedValue + "' and InventoryCategoryMaster.Activestatus = '1' ";

                str2 = "SELECT count(InventorySubCategoryMaster.InventorySubCatId) as ci FROM InventoryCategoryMaster RIGHT OUTER JOIN InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId where compid='" + comp + "'  and InventoryCategoryMaster.InventeroyCatId ='" + ddlCat.SelectedValue + "' and InventoryCategoryMaster.Activestatus = '1' ";
            }
            else
            {
                strfillgrid = " InventorySubCategoryMaster.Activestatus ,case when (InventorySubCategoryMaster.Activestatus='1') then 'Active' else 'Inactive' End as Statuslabel, InventorySubCategoryMaster.InventorySubCatId,InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId,  " +
                              " InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster RIGHT OUTER JOIN " +
                              " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId where compid='" + comp + "'  and InventoryCategoryMaster.Activestatus = '1'  ";

                str2 = "SELECT count(InventorySubCategoryMaster.InventorySubCatId) as ci FROM InventoryCategoryMaster RIGHT OUTER JOIN InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId where compid='" + comp + "'  and InventoryCategoryMaster.Activestatus = '1'  ";
            }
        }

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " InventoryCatName asc";


        lblCompany.Text = Session["Cname"].ToString();
        lblStatus.Text = ddlActive.SelectedItem.Text;
        lblCate.Text = ddlCat.SelectedItem.Text;

        //string st2 = "";
        //st2 = " order by InventoryCatName";
        //strfillgrid = strfillgrid + st2;

        //SqlCommand cmdmain = new SqlCommand(strfillgrid, con);
        //SqlDataAdapter adpmain = new SqlDataAdapter(cmdmain);
        //DataTable dtmain = new DataTable();
        //adpmain.Fill(dtmain);


        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dtmain = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, strfillgrid);

            GridView1.DataSource = dtmain;

            DataView myDataView = new DataView();
            myDataView = dtmain.DefaultView;

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
        //string str = "Select * from InventorySubCategoryMaster where InventorySubCatId='" + ViewState["editid"] + "'";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    cat();
        //    ddlInventoryCategoryMasterId.SelectedIndex = ddlInventoryCategoryMasterId.Items.IndexOf(ddlInventoryCategoryMasterId.Items.FindByValue(dt.Rows[0]["InventoryCategoryMasterId"].ToString()));
        //    txtInventorySubCatName.Text = dt.Rows[0]["InventorySubCatName"].ToString();
        //    string chk = dt.Rows[0]["Activestatus"].ToString();

        //    if (chk == "True")
        //    {
        //        //chk123.Checked = true;
        //        ddlstatus.SelectedValue = "1";
        //    }
        //    else
        //    {
        //        //chk123.Checked = false;
        //        ddlstatus.SelectedValue = "0";
        //    }
        //    pnladd.Visible = true;
        //    lbllegend.Visible = true;
        //    lbllegend.Text = "Edit Sub Category for your Inventory";
        //    btnadd.Visible = false;
        //    Button3.Visible = false;
        //    btnupdate.Visible = true;
        //}
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGrid1();

    }

    protected void ddlCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid1();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //try
        //{

        //    int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);

        //    DropDownList ddlgrdct = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlgrdcat");
        //    Label catid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblgrdCatid");
        //    CheckBox chk12 = (CheckBox)GridView1.Rows[GridView1.EditIndex].FindControl("chkinvMasterStatus");
        //    TextBox subctnm = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtsubcatname");
        //    string sgw = "select InventorySubCatName from InventorySubCategoryMaster where  " +
        //       " InventorySubCatName='" + subctnm.Text + "' and InventoryCategoryMasterId='" + ddlgrdct.SelectedValue + "' and  InventorySubCatID<>" + dk + "";
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

        //        string fetchcatergory = "select * from InventoruSubSubCategory where InventorySubCatID=" + dk;
        //        SqlDataAdapter adp = new SqlDataAdapter(fetchcatergory, con);
        //        DataSet ds = new DataSet();
        //        adp.Fill(ds);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (chk12.Checked == false)
        //            {
        //                statuslable.Visible = true;
        //                statuslable.Text = " This is Active Records";
        //            }
        //            else
        //            {
        //                bool access = UserAccess.Usercon("InventorySubCategoryMaster", "", "InventorySubCatId", "", "", "InventoryCategoryMaster.compid", " InventorySubCategoryMaster inner join InventoryCategoryMaster on  InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId");
        //                if (access == true)
        //                {
        //                    bool access1 = UserAccess.Usercon("InventorySubCategoryMaster", ddlgrdct.SelectedValue, "InventorySubCatId", "", "", "InventoryCategoryMaster.compid", " InventorySubCategoryMaster inner join InventoryCategoryMaster on  InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId");
        //                    if (access1 == true)
        //                    {
        //                        string update = "update  InventorySubCategoryMaster set InventorySubCatName='" + subctnm.Text + "', Activestatus='" + chk12.Checked + "'," +
        //                       " InventoryCategoryMasterId='" + ddlgrdct.SelectedValue + "' where InventorySubCatId='" + dk.ToString() + "' ";

        //                        SqlCommand cmdupate = new SqlCommand(update, con);
        //                        //SqlDataAdapter adpupdate = new SqlDataAdapter(cmdupate);
        //                        con.Open();
        //                        cmdupate.ExecuteNonQuery();
        //                        con.Close();
        //                        statuslable.Text = "Record Updated Successfully";
        //                        statuslable.Visible = true;
        //                    }
        //                    else
        //                    {
        //                        statuslable.Visible = true;
        //                        statuslable.Text = "Sorry, You don't permited greter record to this category into priceplan";
        //                    }
        //                }
        //                else
        //                {
        //                    statuslable.Visible = true;
        //                    statuslable.Text = "Sorry, You don't permited greter record to priceplan";
        //                }
        //            }
        //        }



        //        else
        //        {
        //            bool access = UserAccess.Usercon("InventorySubCategoryMaster", "", "InventorySubCatId", "", "", "InventoryCategoryMaster.compid", " InventorySubCategoryMaster inner join InventoryCategoryMaster on  InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId");
        //            if (access == true)
        //            {
        //                bool access1 = UserAccess.Usercon("InventorySubCategoryMaster", ddlgrdct.SelectedValue, "InventorySubCatId", "", "", "InventoryCategoryMaster.compid", " InventorySubCategoryMaster inner join InventoryCategoryMaster on  InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId");
        //                if (access1 == true)
        //                {
        //                    string update = "update  InventorySubCategoryMaster set InventorySubCatName='" + subctnm.Text + "', Activestatus='" + chk12.Checked + "'," +
        //                        " InventoryCategoryMasterId='" + ddlgrdct.SelectedValue + "' where InventorySubCatId='" + dk.ToString() + "' ";

        //                    SqlCommand cmdupate = new SqlCommand(update, con);
        //                    //SqlDataAdapter adpupdate = new SqlDataAdapter(cmdupate);
        //                    con.Open();
        //                    cmdupate.ExecuteNonQuery();
        //                    con.Close();
        //                    statuslable.Text = "Record Updated Successfully";
        //                    statuslable.Visible = true;
        //                }
        //                else
        //                {
        //                    statuslable.Visible = true;
        //                    statuslable.Text = "Sorry, You don't permited greter record to this category into priceplan";
        //                }
        //            }
        //            else
        //            {
        //                statuslable.Visible = true;
        //                statuslable.Text = "Sorry, You don't permited greter record to priceplan";
        //            }

        //        }
        //    }
        //}
        //catch (Exception erf)
        //{
        //    statuslable.Text = "Error :" + erf.Message;
        //    statuslable.Visible = true;
        //}
        //GridView1.EditIndex = -1;
        //FillGrid1();



    }
    protected void ddlInventoryCategoryMasterId_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void yes_Click(object sender, EventArgs e)
    {
        if (ViewState["dle"].ToString() == "1")
        {
            string sr4 = ("delete from InventorySubCategoryMaster where InventorySubCatId=" + Session["ID9"] + "");
            SqlCommand cmd8 = new SqlCommand(sr4, con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd8.ExecuteNonQuery();
            con.Close();
            FillGrid1();
            statuslable.Visible = true;
            statuslable.Text = "Record deleted successfully";

        }
        else if (ViewState["dle"].ToString() == "2")
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
                    string st = "Update InventoruSubSubCategory set InventorySubCatID=" + drp.SelectedValue + " where InventorySubSubId=" + Convert.ToInt32(GridView2.DataKeys[gdr.RowIndex].Value) + "";
                    SqlCommand Mycommand = new SqlCommand(st, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    Mycommand.ExecuteNonQuery();
                    con.Close();
                    FillGrid1();
                }

            }
            //}
            if (i == 0)
            {
                string sr43 = ("delete from InventorySubCategoryMaster where InventorySubCatId=" + Session["ID9"] + "");
                SqlCommand cmd83 = new SqlCommand(sr43, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd83.ExecuteNonQuery();
                con.Close();
                FillGrid1();
                statuslable.Visible = true;
                statuslable.Text = "Record deleted successfully";
            }
            else
            {
                statuslable.Visible = true;
                statuslable.Text = "Sorry,You have no other sub category to move so you cannot delete this sub category ";
            }
            ModalPopupExtender1.Hide();


        }
        FillGrid1();
        object sen = new object();
        EventArgs gg1 = new EventArgs();
        ddlCat_SelectedIndexChanged(sen, gg1);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ViewState["dle"].ToString() == "1")
        {

        }
        else if (ViewState["dle"].ToString() == "2")
        {

        }
        //ModalPopupExtender1222.Hide();
    }
    //protected void ddlCat_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (ddlCat.SelectedIndex > 0)
    //    {
    //        ddlsubcat.Items.Clear();
    //        //string str = "SELECT     InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId,  " +
    //        //              " InventoryCategoryMaster.InventoryCatName " +
    //        //              " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //        //              "  InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId " +
    //        //           " where InventoryCategoryMaster.InventeroyCatId ='" + ddlCat.SelectedValue + "' ";
    //        string str = " SELECT     InventorySubCategoryMaster.InventorySubCatId, InventoryCategoryMaster.InventoryCatName,  " +
    //           " InventorySubCategoryMaster.InventorySubCatName " +
    //           "From InventorySubCategoryMaster INNER JOIN  InventoryCategoryMaster ON " +
    //        " InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId  " +
    //          " Where InventoryCategoryMaster.InventeroyCatId = '" + ddlCat.SelectedValue + "' and " + " InventoryCategoryMaster.compid ='" + comp + "'" +
    //        " ORDER BY InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName  ";
    //        SqlCommand cmd = new SqlCommand(str, con);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataTable ds = new DataTable();
    //        da.Fill(ds);

    //        ddlsubcat.DataSource = ds;
    //        ddlsubcat.DataTextField = "InventorySubCatName";
    //        ddlsubcat.DataValueField = "InventorySubCatId";
    //        ddlsubcat.DataBind();

    //        //ddlsubcat.Items.Insert(0, "--Select--");
    //        //ddlsubcat.SelectedItem.Value = "0";
    //    }
    //    else
    //    {
    //        //ddlCat.Items.Clear();
    //        //ddlCat.Items.Insert(0, "--Select--");
    //        //ddlCat.SelectedItem.Value = "0";

    //        ddlsubcat.Items.Clear();
    //        //ddlsubcat.Items.Insert(0, "--Select--");
    //        //ddlsubcat.SelectedItem.Value = "0";
    //        FillGrid1();
    //    }
    //    ddlsubcat_SelectedIndexChanged(sender, e);

    //    //ImageClickEventArgs ie = new ImageClickEventArgs(0, 0);
    //    //btGo_Click(sender, ie);
    //}
    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {

        //btGo_Click(sender,



        GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        Session["ID9"] = GridView1.SelectedIndex;



        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);


        string s = "select InventorySubSubId  from InventoruSubSubCategory where InventorySubCatID= " + Session["ID9"] + "";
        SqlCommand c = new SqlCommand(s, con);
        c.CommandType = CommandType.Text;
        SqlDataAdapter d = new SqlDataAdapter(c);
        DataSet ds0 = new DataSet();
        d.Fill(ds0);
        ViewState["dle"] = 0;


        int a = Convert.ToInt32(ds0.Tables[0].Rows.Count);
        if (a == 0)
        {
            //if (MessageBox.Show("You sure, You want to Delete!", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //{



            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            ViewState["dle"] = 1;
            // ModalPopupExtender1222.Show();
            yes_Click(sender, e);
            //GridView1.DataBind();
            //}
            // a = 0;
        }
        else
        {
            GridView2.DataSource = (DataSet)getSubSub();
            GridView2.DataBind();
            lblTotal.Text = a.ToString();

            if (lblTotal.Text == "1")
            {
                Label15.Text = "sub sub category. Please move it before deleting.";
            }
            else
            {
                Label15.Text = "sub sub categories. Please move them before deleting.";

            }

            int i = 0;
            foreach (GridViewRow gdr in GridView2.Rows)
            {
                DropDownList drp = (DropDownList)gdr.Cells[0].FindControl("DropDownList2");

                drp.DataSource = (DataSet)fillddl();
                drp.DataTextField = "InventorySubCatName";
                drp.DataValueField = "InventorySubCatId";
                drp.DataBind();

                DataSet ds = new DataSet();
                ds = (DataSet)getSubSub();
                drp.SelectedIndex = drp.Items.IndexOf(drp.Items.FindByValue(ds.Tables[0].Rows[0]["InventorySubCatId"].ToString()));
                drp.Items.RemoveAt(drp.SelectedIndex);
                if (drp.Items.Count <= 0)
                {
                    i = 1;
                }

            }
            if (i == 1)
            {
                ImgBtnMove.Enabled = false;
            }
            else
            {
                ImgBtnMove.Enabled = true;
            }
            ModalPopupExtender1.Show();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ImageClickEventArgs ie = new ImageClickEventArgs(0, 0);
        btGo_Click(sender, ie);
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        ImageClickEventArgs ie = new ImageClickEventArgs(0, 0);
        btGo_Click(sender, ie);
    }
    //protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    //{
    //    ddlInventoryCategoryMasterId.SelectedIndex = -1;
    //    txtInventorySubCatName.Text = "";
    //    statuslable.Visible = false;
    //}
    protected void ddlsubcat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid1();
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string sgw = "select InventorySubCatName from InventorySubCategoryMaster where  " +
                " InventorySubCatName='" + txtInventorySubCatName.Text + "' and InventoryCategoryMasterId='" + ddlInventoryCategoryMasterId.SelectedValue + "' ";
            SqlCommand cgw = new SqlCommand(sgw, con);
            SqlDataAdapter adgw = new SqlDataAdapter(cgw);
            DataTable dtgw = new DataTable();
            adgw.Fill(dtgw);
            if (dtgw.Rows.Count > 0)
            {
                statuslable.Visible = true;
                statuslable.Text = "Record already exists";


            }
            else
            {
                bool access = UserAccess.Usercon("InventorySubCategoryMaster", "", "InventorySubCatId", "", "", "InventoryCategoryMaster.compid", " InventorySubCategoryMaster inner join InventoryCategoryMaster on  InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId");
                if (access == true)
                {
                    bool access1 = UserAccess.Usercon("InventorySubCategoryMaster", ddlInventoryCategoryMasterId.SelectedValue, "InventorySubCatId", "", "", "InventoryCategoryMaster.compid", " InventorySubCategoryMaster inner join InventoryCategoryMaster on  InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId");
                    if (access1 == true)
                    {
                        try
                        {
                            int val1234 = Convert.ToInt32(ddlInventoryCategoryMasterId.SelectedValue.ToString());
                            
                            con.Close();
                            string queryinsert = "insert into  InventorySubCategoryMaster(InventorySubCatName,InventoryCategoryMasterId,Activestatus)  values('" + txtInventorySubCatName.Text + "'," + val1234 + ",'" + ddlstatus.SelectedValue + "')";
                            SqlCommand mycmd = new SqlCommand(queryinsert, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();

                            }
                           
                            mycmd.ExecuteNonQuery();
                            con.Close();
                            ddlInventoryCategoryMasterId.SelectedIndex = -1;
                            txtInventorySubCatName.Text = "";
                            statuslable.Visible = true;
                            statuslable.Text = "Record inserted successfully";
                           
                            
                            FillGrid1();
                            btGo_Click(sender, e);
                            object sen = new object();
                            EventArgs gg = new EventArgs();

                            ddlCat_SelectedIndexChanged(sen, gg);

                            pnladd.Visible = false;
                            lbllegend.Visible = false;
                            btnadd.Visible = true;
                            ddlstatus.SelectedIndex = 0;
                            lbllegend.Text = "Add a New Sub Category for Your Inventory";


                        }
                        catch (Exception erererer)
                        {
                            statuslable.Visible = true;
                            statuslable.Text = "Error : " + erererer.Message;
                        }
                        finally { }
                    }
                    else
                    {
                        statuslable.Visible = true;
                        statuslable.Text = "Sorry, You don't permitted greater record to this category into priceplan";
                    }
                }
                else
                {
                    statuslable.Visible = true;
                    statuslable.Text = "Sorry, You don't permitted greater record to priceplan";
                }
            }
        }
        catch (Exception)
        {
            statuslable.Visible = true;
            statuslable.Text = "error";
        }
    }
    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        ddlInventoryCategoryMasterId.SelectedIndex = -1;
        txtInventorySubCatName.Text = "";
        statuslable.Visible = false;
        pnladd.Visible = false;
        lbllegend.Visible = false;
        btnadd.Visible = true;

        Button3.Visible = true;
        btnupdate.Visible = false;
        lbllegend.Text = "Add a New Sub Category for Your Inventory";
        ddlstatus.SelectedIndex = 0;
        GridView1.EditIndex = -1;
        FillGrid1();

    }
    protected void btGo_Click(object sender, EventArgs e)
    {
        //SqlDataSource1.SelectCommand = "SELECT InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventoryCategoryMasterId FROM InventorySubCategoryMaster INNER JOIN InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryCategoryMaster.InventeroyCatId = '" + ddlCat.SelectedValue + "'";
        //   SqlDataSource1.SelectCommand = "   SELECT     InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName, " +
        //                   " COUNT(InventoruSubSubCategory.InventorySubCatID) AS TotalItem, InventorySubCategoryMaster.InventoryCategoryMasterId " +
        //                    "  FROM         InventorySubCategoryMaster INNER JOIN  " +
        // "                   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId LEFT OUTER JOIN  " +
        //  "                  InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID  " +
        //"where InventoryCategoryMaster.InventeroyCatId = '" + ddlCat.SelectedValue + "' " +
        //  " GROUP BY InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName,  " +
        //                    " InventorySubCategoryMaster.InventoryCategoryMasterId, InventoryCategoryMaster.InventeroyCatId  ";

        //   GridView1.DataBind();
        FillGrid1();
    }

    protected void ddlActive_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid1();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            FillGrid1();

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
            //pnlgrid.Height = new Unit(300);

            Button1.Text = "Printable Version";
            Button7.Visible = false;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            FillGrid1();

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
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            string sgw = "select InventorySubCatName from InventorySubCategoryMaster where  " +
                   " InventorySubCatName='" + txtInventorySubCatName.Text + "' and InventoryCategoryMasterId='" + ddlInventoryCategoryMasterId.SelectedValue + "' and  InventorySubCatID<>" + ViewState["editid"] + "";
            SqlCommand cgw = new SqlCommand(sgw, con);
            SqlDataAdapter adgw = new SqlDataAdapter(cgw);
            DataTable dtgw = new DataTable();
            adgw.Fill(dtgw);
            if (dtgw.Rows.Count > 0)
            {
                statuslable.Visible = true;
                statuslable.Text = "Record already exists";


            }
            else
            {

                string fetchcatergory = "select * from InventoruSubSubCategory where InventorySubCatID=" + ViewState["editid"];
                SqlDataAdapter adp = new SqlDataAdapter(fetchcatergory, con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ddlstatus.SelectedValue == "0")
                    {
                        statuslable.Visible = true;
                        statuslable.Text = " This is active record";
                    }
                    else
                    {
                        bool access = UserAccess.Usercon("InventorySubCategoryMaster", "", "InventorySubCatId", "", "", "InventoryCategoryMaster.compid", " InventorySubCategoryMaster inner join InventoryCategoryMaster on  InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId");
                        if (access == true)
                        {
                            bool access1 = UserAccess.Usercon("InventorySubCategoryMaster", ddlInventoryCategoryMasterId.SelectedValue, "InventorySubCatId", "", "", "InventoryCategoryMaster.compid", " InventorySubCategoryMaster inner join InventoryCategoryMaster on  InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId");
                            if (access1 == true)
                            {
                                string update = "update  InventorySubCategoryMaster set InventorySubCatName='" + txtInventorySubCatName.Text + "', Activestatus='" + ddlstatus.SelectedValue + "'," +
                               " InventoryCategoryMasterId='" + ddlInventoryCategoryMasterId.SelectedValue + "' where InventorySubCatId='" + ViewState["editid"] + "' ";

                                SqlCommand cmdupate = new SqlCommand(update, con);
                               
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdupate.ExecuteNonQuery();
                                con.Close();
                                statuslable.Text = "Record updated successfully";
                                ddlInventoryCategoryMasterId.SelectedIndex = -1;
                                txtInventorySubCatName.Text = "";
                                statuslable.Visible = true;
                                pnladd.Visible = false;
                                lbllegend.Visible = false;
                                lbllegend.Text = "Add a New Sub Category for Your Inventory";
                                btnadd.Visible = true;
                                btnupdate.Visible = false;
                                Button3.Visible = true;
                                GridView1.EditIndex = -1;
                                FillGrid1();
                                ddlstatus.SelectedIndex = 0;
                                
                            }
                            else
                            {
                                statuslable.Visible = true;
                                statuslable.Text = "Sorry, You don't permitted greater record to this category as per price plan";
                            }
                        }
                        else
                        {
                            statuslable.Visible = true;
                            statuslable.Text = "Sorry, You don't permitted greater record to this category as per price plan";
                        }
                    }
                }

                else
                {
                    bool access = UserAccess.Usercon("InventorySubCategoryMaster", "", "InventorySubCatId", "", "", "InventoryCategoryMaster.compid", " InventorySubCategoryMaster inner join InventoryCategoryMaster on  InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId");
                    if (access == true)
                    {
                        bool access1 = UserAccess.Usercon("InventorySubCategoryMaster", ddlInventoryCategoryMasterId.SelectedValue, "InventorySubCatId", "", "", "InventoryCategoryMaster.compid", " InventorySubCategoryMaster inner join InventoryCategoryMaster on  InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId");
                        if (access1 == true)
                        {
                            string update = "update  InventorySubCategoryMaster set InventorySubCatName='" + txtInventorySubCatName.Text + "', Activestatus='" + ddlstatus.SelectedValue + "'," +
                                " InventoryCategoryMasterId='" + ddlInventoryCategoryMasterId.SelectedValue + "' where InventorySubCatId='" + ViewState["editid"] + "' ";

                            SqlCommand cmdupate = new SqlCommand(update, con);
                            //SqlDataAdapter adpupdate = new SqlDataAdapter(cmdupate);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdupate.ExecuteNonQuery();
                            con.Close();
                            statuslable.Text = "Record updated successfully";
                            ddlInventoryCategoryMasterId.SelectedIndex = -1;
                            txtInventorySubCatName.Text = "";
                            statuslable.Visible = true;
                            pnladd.Visible = false;
                            lbllegend.Visible = false;
                            btnadd.Visible = true;
                            btnupdate.Visible = false;
                            lbllegend.Text = "Add a New Sub Category for Your Inventory";
                            Button3.Visible = true;
                            GridView1.EditIndex = -1;
                            FillGrid1();
                            ddlstatus.SelectedIndex = 0;
                        }
                        else
                        {
                            statuslable.Visible = true;
                            statuslable.Text = "Sorry, You don't have permission to insert a new record as per your price plan";
                        }
                    }
                    else
                    {
                        statuslable.Visible = true;
                        statuslable.Text = "Sorry, You don't have permission to insert a new record as per your price plan";
                    }

                }
            }
        }

        catch (Exception erf)
        {
            statuslable.Text = "Error :" + erf.Message;
            statuslable.Visible = true;
        }

        //FillGrid1();
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
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "InventoryCategoryMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        cat();

    }

}
