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

public partial class InventoruSubSubCategory : System.Web.UI.Page
{
    string comp;

    SqlConnection con = new SqlConnection(PageConn.connnn);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn; 
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        comp = Session["comid"].ToString();
        statuslable.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);

            ViewState["sortOrder"] = "";

            ddlSubcat.DataSource = (DataSet)getall();
            ddlSubcat.DataValueField = "InventorySubCatId";
            ddlSubcat.DataTextField = "category";
            ddlSubcat.DataBind();
            ddlSubcat.Items.Insert(0, "All");
            ddlSubcat.Items[0].Value = "0";
            ddlInventorySubCatID.DataSource = (DataSet)getall();
            ddlInventorySubCatID.DataValueField = "InventorySubCatId";
            ddlInventorySubCatID.DataTextField = "category";
            ddlInventorySubCatID.DataBind();
            fillgrid();

        }
    }

    public DataSet getall1()
    {
        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        con.Open();
        string str = "select  InventeroyCatId,  InventoryCatName from InventoryCategorymaster where compid='" +
         comp + "'   and [Activestatus]='1' order by InventoryCatName";

        MyDataAdapter = new SqlDataAdapter(str, con);
        MyDataAdapter.Fill(ds);
        con.Close();
        return ds;
    }

    public void fillgrid()
    {
        lblCompany.Text = Session["Cname"].ToString();

        string str1 = "";

        string str2 = "";

        if (ddlActive.SelectedIndex > 0)
        {
            lblStatus.Text = ddlActive.SelectedItem.Text;
            if (ddlSubcat.SelectedIndex > 0)
            {
                lblSubCat.Text = ddlSubcat.SelectedItem.Text;

                str1 = "  InventoryCategoryMaster.InventeroyCatId, InventorySubCategoryMaster.InventorySubCatId, InventoruSubSubCategory.InventorySubSubId, " +
                      "InventoruSubSubCategory.InventorySubSubName,InventoruSubSubCategory.Activestatus,case when (InventoruSubSubCategory.Activestatus='1') then 'Active' else 'Inactive' End as Statuslabel, " +
                     " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName AS CatSubCat,InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName " +

                      "FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                      "InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                      "InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
                        " where  InventoryCategoryMaster.compid ='" + comp + "' and InventorySubCategoryMaster.InventorySubCatId='" + ddlSubcat.SelectedValue + "'  and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1 and InventoruSubSubCategory.Activestatus='" + ddlActive.SelectedValue + "'  ";

                str2 = "select count(InventoruSubSubCategory.InventorySubSubId) as ci " +
                         "FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                      "InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                      "InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
                        " where  InventoryCategoryMaster.compid ='" + comp + "' and InventorySubCategoryMaster.InventorySubCatId='" + ddlSubcat.SelectedValue + "'  and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1 and InventoruSubSubCategory.Activestatus='" + ddlActive.SelectedValue + "'  ";

            }
            else
            {

                str1 = "  InventoryCategoryMaster.InventeroyCatId, InventorySubCategoryMaster.InventorySubCatId, InventoruSubSubCategory.InventorySubSubId, " +
                     "InventoruSubSubCategory.InventorySubSubName,InventoruSubSubCategory.Activestatus,case when (InventoruSubSubCategory.Activestatus='1') then 'Active' else 'Inactive' End as Statuslabel, " +
                    " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName AS CatSubCat,InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName " +

                     "FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                     "InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                     "InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
                       " where   InventoryCategoryMaster.Activestatus=1  and InventoryCategoryMaster.compid ='" + comp + "'  and InventorySubCategoryMaster.Activestatus=1  and InventoruSubSubCategory.Activestatus='" + ddlActive.SelectedValue + "'";

                str2 = "select count(InventoruSubSubCategory.InventorySubSubId) as ci " +
                         "FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                     "InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                     "InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
                       " where   InventoryCategoryMaster.Activestatus=1  and InventoryCategoryMaster.compid ='" + comp + "'  and InventorySubCategoryMaster.Activestatus=1  and InventoruSubSubCategory.Activestatus='" + ddlActive.SelectedValue + "'";
            }
        }
        else
        {
            if (ddlSubcat.SelectedIndex > 0)
            {
                lblSubCat.Text = ddlSubcat.SelectedItem.Text;

                str1 = "  InventoryCategoryMaster.InventeroyCatId, InventorySubCategoryMaster.InventorySubCatId, InventoruSubSubCategory.InventorySubSubId, " +
                      "InventoruSubSubCategory.InventorySubSubName,InventoruSubSubCategory.Activestatus,case when (InventoruSubSubCategory.Activestatus='1') then 'Active' else 'Inactive' End as Statuslabel, " +
                     " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName AS CatSubCat,InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName " +

                      "FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                      "InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                      "InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
                        " where  InventoryCategoryMaster.compid ='" + comp + "' and InventorySubCategoryMaster.InventorySubCatId='" + ddlSubcat.SelectedValue + "'  and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1  ";

                str2 = "select count(InventoruSubSubCategory.InventorySubSubId) as ci " +
                         "FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                      "InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                      "InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
                        " where  InventoryCategoryMaster.compid ='" + comp + "' and InventorySubCategoryMaster.InventorySubCatId='" + ddlSubcat.SelectedValue + "'  and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1  ";

            }
            else
            {

                str1 = "  InventoryCategoryMaster.InventeroyCatId, InventorySubCategoryMaster.InventorySubCatId, InventoruSubSubCategory.InventorySubSubId, " +
                     "InventoruSubSubCategory.InventorySubSubName,InventoruSubSubCategory.Activestatus,case when (InventoruSubSubCategory.Activestatus='1') then 'Active' else 'Inactive' End as Statuslabel, " +
                    " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName AS CatSubCat,InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName " +

                     "FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                     "InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                     "InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
                       " where   InventoryCategoryMaster.Activestatus=1  and InventoryCategoryMaster.compid ='" + comp + "'  and InventorySubCategoryMaster.Activestatus=1 ";

                str2 = "select count(InventoruSubSubCategory.InventorySubSubId) as ci " +
                         "FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                     "InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                     "InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
                       " where   InventoryCategoryMaster.Activestatus=1  and InventoryCategoryMaster.compid ='" + comp + "'  and InventorySubCategoryMaster.Activestatus=1 ";
            }
        }

        lblSubCat.Text = ddlSubcat.SelectedItem.Text;
        lblStatus.Text = ddlActive.SelectedItem.Text;

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " InventoryCatName asc";

        //DataTable ds1 = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(str1, con);
        //da.Fill(ds1);


        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable ds1 = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str1);

            GridView1.DataSource = ds1;

            DataView myDataView = new DataView();
            myDataView = ds1.DefaultView;

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

    public DataSet getall()
    {
        ddlInventorySubCatID.Items.Clear();
        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
        // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        Mycommand = new SqlCommand(" SELECT     InventorySubCategoryMaster.InventorySubCatId,  " +
            " (InventoryCategoryMaster.InventoryCatName + ':'+InventorySubCategoryMaster.InventorySubCatName) as category " +
            " FROM       InventorySubCategoryMaster INNER JOIN  InventoryCategoryMaster  " +
            " ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where compid='" + comp + "' and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1  " +
            " ORDER BY InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName ", con);
        // Mycommand.CommandType = CommandType.Text;
        //  Mycommand.Connection.Open();
        MyDataAdapter = new SqlDataAdapter(Mycommand);
        DataSet ds1 = new DataSet();
        MyDataAdapter.Fill(ds1);
        con.Close();
        return ds1;
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string sgw = "select InventorySubSubName from InventoruSubSubCategory where " +
            " InventorySubSubName='" + txtInventorySubSubName.Text + "' and InventorySubCatID='" + ddlInventorySubCatID.SelectedValue + "'";
        //SELECT     InventorySubSubId, InventorySubSubName, InventorySubCatID FROM         InventoruSubSubCategory
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
            bool access = UserAccess.Usercon("InventoruSubSubCategory", "", "InventorySubSubId", "", "", "InventoryCategoryMaster.compid", "InventoruSubSubCategory   inner join  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId inner join  InventoryCategoryMaster ON  InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId");
            if (access == true)
            {
                bool access1 = UserAccess.Usercon("InventoruSubSubCategory", ddlInventorySubCatID.SelectedValue, "InventorySubSubId", "", "", "InventoryCategoryMaster.compid", "InventoruSubSubCategory   inner join  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId inner join  InventoryCategoryMaster ON  InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId");
                if (access1 == true)
                {
                    try
                    {
                        //if (ddlInventorySubCatID.SelectedIndex > 0)
                        //{
                        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                        //            SqlCommand mycmd = new SqlCommand("Sp_Insert_InventorySubSubCategoryMaster", con);
                        //            mycmd.CommandType = CommandType.StoredProcedure;
                        //            mycmd.Parameters.AddWithValue("@InventorySubSubName", txtInventorySubSubName.Text);
                        //            mycmd.Parameters.AddWithValue("@InventorySubCatID", int.Parse(ddlInventorySubCatID.SelectedValue.ToString()));
                        //            con.Open();
                        //            mycmd.ExecuteNonQuery();
                        //            con.Close();
                        //            // GridView1.DataBind();
                        //            //fillInventorycategory();
                        //            fillgrid();
                        //            btngo_Click(sender, e);

                        //            object sen = new object();
                        //            EventArgs gg = new EventArgs();
                        //            ddlSubCat_SelectedIndexChanged(sen, gg);
                        //            //ddlCat_SelectedIndexChanged(sen, gg);
                        //            fillgrid();




                        //            statuslable.Visible = true;
                        //            statuslable.Text = "Record Inserted Successfully";
                        //            txtInventorySubSubName.Text = "";
                        //            ddlInventorySubCatID.SelectedIndex = 0;
                        //            //statuslable.Visible = false;

                        //        }
                        //        else
                        //        {
                        //            statuslable.Visible = false;
                        //            statuslable.Text = "Please Select Sub Category";
                        //        }

                        //    }
                        //    catch (Exception ey)
                        //    {
                        //        statuslable.Visible = true;
                        //        statuslable.Text = "Error : " + ey.Message;
                        //    }
                        //}
                        con.Close();
                        string qurty = "insert into InventoruSubSubCategory(InventorySubSubName,InventorySubCatID,Activestatus)values('" + txtInventorySubSubName.Text + "'," + int.Parse(ddlInventorySubCatID.SelectedValue.ToString()) + ",'" + ddlstatus.SelectedValue + "')";
                        SqlCommand mycmd = new SqlCommand(qurty, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();

                        }
                        // con.Open();
                        mycmd.ExecuteNonQuery();
                        con.Close();
                        // GridView1.DataBind();
                        //fillInventorycategory();
                        fillgrid();
                        //  btngo_Click(sender, e);

                        object sen = new object();
                        EventArgs gg = new EventArgs();
                        ddlSubCat_SelectedIndexChanged(sen, gg);

                        //ddlCat_SelectedIndexChanged(sen, gg);
                        fillgrid();




                        statuslable.Visible = true;
                        statuslable.Text = "Record inserted successfully";
                        txtInventorySubSubName.Text = "";
                        ddlInventorySubCatID.SelectedIndex = 0;
                        pnladd.Visible = false;
                        lbllegend.Visible = false;

                        lbllegend.Text = "Add a New Sub Sub Category For Your Inventory";

                        btnadd.Visible = true;
                        ddlstatus.SelectedIndex = 0;


                    }
                    catch (Exception ey)
                    {
                        statuslable.Visible = true;
                        statuslable.Text = "Error : " + ey.Message;
                    }
                }
                else
                {
                    statuslable.Visible = true;
                    statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
                }
            }
            else
            {
                statuslable.Visible = true;
                statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
            }
        }
    }

    //protected void ddlCat_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //  ddlSubCat.ClearSelection();
    //    if (ddlCat.SelectedIndex > 1)
    //    {
    //        ddlSubCat.Items.Clear();
    //        SqlCommand Mycommand = new SqlCommand();


    //        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
    //        Mycommand = new SqlCommand(" SELECT     InventorySubCategoryMaster.InventorySubCatId, " +
    //            //" (InventoryCategoryMaster.InventoryCatName + " +
    //            // " ':'+  "+
    //          " InventorySubCategoryMaster.InventorySubCatName  as category FROM      " +
    //          "     InventorySubCategoryMaster INNER JOIN  InventoryCategoryMaster ON InventorySubCategoryMaster. " +
    //          "         InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId  " +
    //         " Where InventoryCategoryMaster.InventeroyCatId = '" + ddlCat.SelectedValue + "' and  " + "InventoryCategoryMaster.compid ='" + comp + "' and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1  and InventoryCategoryMaster.CatType IS NULL " +
    //       " ORDER BY InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName  ", con);

    //        MyDataAdapter = new SqlDataAdapter(Mycommand);
    //        DataSet ds1 = new DataSet();
    //        MyDataAdapter.Fill(ds1);
    //        con.Close();


    //        ddlSubcat.DataSource = ds1;
    //        ddlSubcat.DataValueField = "InventorySubCatId";
    //        ddlSubcat.DataTextField = "category";
    //        ddlSubcat.DataBind();

    //        ddlSubcat.Items.Insert(0, "All");


    //    }
    //    else
    //    {
    //        ddlSubcat.Items.Clear();
    //        ddlSubcat.Items.Insert(0, "All");


    //        // ddlsubsubcat.Items.Insert(0, "--Select--");
    //        //ddlsubsubcat.SelectedItem.Value = "0";
    //        fillgrid();
    //    }
    //    ddlSubCat_SelectedIndexChanged(sender, e);

    //    //btngo_Click(sender, e);
    //    // ddlSubCat.SelectedIndex = 0;
    //}
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

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


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "Sort")
        {
            return;
        }
        if (e.CommandName == "Edit")
        {
            ViewState["editid"] = Convert.ToInt32(e.CommandArgument);

            //ViewState["editid"] = GridView1.DataKeys[e.NewEditIndex].Value.ToString();

            string str = "select * from InventoruSubSubCategory where InventorySubSubId='" + ViewState["editid"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlInventorySubCatID.Items.Clear();

                ddlInventorySubCatID.DataSource = (DataSet)getall();
                ddlInventorySubCatID.DataValueField = "InventorySubCatId";
                ddlInventorySubCatID.DataTextField = "category";
                ddlInventorySubCatID.DataBind();

                ddlInventorySubCatID.SelectedIndex = ddlInventorySubCatID.Items.IndexOf(ddlInventorySubCatID.Items.FindByValue(dt.Rows[0]["InventorySubCatID"].ToString()));
                txtInventorySubSubName.Text = dt.Rows[0]["InventorySubSubName"].ToString();

                string chk = dt.Rows[0]["Activestatus"].ToString();
                if (chk == "True")
                {
                    //chk12.Checked = true;
                    ddlstatus.SelectedValue = "1";
                }
                else
                {
                    // chk12.Checked = false;
                    ddlstatus.SelectedValue = "0";
                }
                pnladd.Visible = true;
                lbllegend.Visible = true;
                lbllegend.Text = "Edit Sub Sub Category For Your Inventory";
                btnadd.Visible = false;
                btnupdate.Visible = true;
                ImageButton2.Visible = false;
                statuslable.Text = "";
            }


        }
        else if (e.CommandName == "Total")
        {


            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            Session["ID9"] = GridView1.SelectedDataKey.Value;


            Response.Redirect("EditSubSubCategory.aspx");
        }

        else if (e.CommandName == "Delete")
        {
            ////ViewState["dle"] = 0;
            ////GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ////Session["ID9"] = GridView1.SelectedDataKey.Value;



            //            string s = " SELECT     InventoruSubSubCategory.InventorySubSubId, Count(InventoryMaster.InventoryMasterId)as TotalItem "+
            //" FROM         InventoruSubSubCategory INNER JOIN "+
            //  "                    InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN "+
            //   "                   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN "+
            //    "                  InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId "+
            // "where InventoryMaster.InventorySubSubId=" + Session["ID9"] + " Group by InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID, " +
            //                     " InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName";




            //            string s = "   SELECT     InventoruSubSubCategory.InventorySubSubId, COUNT(InventoryMaster.InventoryMasterId) AS TotalItem, " +
            //                     " InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name " +
            // "  FROM         InventorySubCategoryMaster INNER JOIN " +
            //    "                  InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID INNER JOIN " +
            //     "                 InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId " +
            //"  where InventoryMaster.InventorySubSubId=" + Session["ID9"] + "  GROUP BY InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID, " +
            //                      " InventorySubCategoryMaster.InventorySubCatName, InventoryMaster.Name ";
            ////            string s = " SELECT     InventoruSubSubCategory.InventorySubSubId, COUNT(InventoryMaster.InventoryMasterId) AS TotalItem, " +
            //                     " InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name   " +
            //"   FROM         InventoruSubSubCategory LEFT OUTER JOIN  " +
            //   "                  InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId   " +
            //"  where InventoryMaster.InventorySubSubId=" + Session["ID9"] + "   GROUP BY InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID, InventoryMaster.Name";
            //string s = "Select * from InventoryMaster where  InventorySubSubId=" + Session["ID9"] + "";
            ////    string s = "    SELECT   InventoruSubSubCategory.InventorySubSubId, COUNT(InventoryMaster.InventoryMasterId) AS TotalItem, InventorySubCategoryMaster.InventorySubCatName,  " +
            ////            "    InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId,  " +
            ////            "    InventoryCategoryMaster.InventoryCatName,InventoryMaster.InventoryMasterId,  InventorySubCategoryMaster.InventorySubCatId " +
            ////            "   FROM         InventorySubCategoryMaster INNER JOIN " +
            ////            "    InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID INNER JOIN " +
            ////            "    InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId INNER JOIN " +
            ////            "    InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
            ////            " WHERE     (InventoryMaster.InventorySubSubId = " + Session["ID9"] + ") " +
            ////           " GROUP BY InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID,  " +
            ////            "    InventorySubCategoryMaster.InventorySubCatName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName,InventoryMaster.InventoryMasterId,  InventorySubCategoryMaster.InventorySubCatId ";

            ////    SqlCommand c = new SqlCommand(s, con);
            ////    c.CommandType = CommandType.Text;
            ////    SqlDataAdapter d = new SqlDataAdapter(c);
            ////    DataSet ds0 = new DataSet();
            ////    d.Fill(ds0);

            ////    ////////
            ////    int a = Convert.ToInt32(ds0.Tables[0].Rows.Count);
            ////    if (a == 0)
            ////    {
            ////        //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ////        //Session["ID9"] = GridView1.SelectedDataKey.Value;

            ////        //if (System.Windows.Forms.MessageBox.Show(" Are you Sure, You want to Delete ?", "Confirm Delete!", System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)//"System.Windows.Forms.MessageBoxButtons.YesNo")
            ////        //{
            ////        ViewState["dle"] = 1;
            ////        ModalPopupExtender1222.Show();
            ////        //}
            ////        // a = 0;
            ////    }
            ////    else
            ////    {
            ////        ////a = 1;
            ////        ////return;

            ////        GridView2.DataSource = ds0;
            ////        GridView2.DataBind();
            ////        lblTotal.Text = a.ToString();
            ////        foreach (GridViewRow gdr in GridView2.Rows)
            ////        {
            ////            DropDownList drp = (DropDownList)gdr.Cells[0].FindControl("DropDownList2");
            ////            Label insubid = (Label)gdr.Cells[0].FindControl("insubid");
            ////            string strdddlsscat = " select InventorySubSubName,InventorySubSubId from InventoruSubSubCategory where InventorySubCatID='" + insubid.Text + "'  ";
            ////            SqlCommand cmdddlsscat = new SqlCommand(strdddlsscat, con);
            ////            SqlDataAdapter adpddlsscat = new SqlDataAdapter(cmdddlsscat);
            ////            DataSet dsss = new DataSet();

            ////            adpddlsscat.Fill(dsss);

            ////            drp.DataSource = (dsss);
            ////            drp.DataTextField = "InventorySubSubName";
            ////            drp.DataValueField = "InventorySubSubId";
            ////            drp.DataBind();

            ////            string s12 = "    SELECT     InventoruSubSubCategory.InventorySubSubId, COUNT(InventoryMaster.InventoryMasterId) AS TotalItem, InventorySubCategoryMaster.InventorySubCatName,  " +
            ////         "    InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId,InventoryMaster.InventoryMasterId,  " +
            ////         "    InventoryCategoryMaster.InventoryCatName " +
            ////         "   FROM         InventorySubCategoryMaster INNER JOIN " +
            ////         "    InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID INNER JOIN " +
            ////         "    InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId INNER JOIN " +
            ////         "    InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
            ////         " WHERE     (InventoryMaster.InventorySubSubId = " + Session["ID9"] + ") and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1 " +
            ////        " GROUP BY InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID,  " +
            ////         "    InventorySubCategoryMaster.InventorySubCatName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName,InventoryMaster.InventoryMasterId ";
            ////            SqlCommand cmd123 = new SqlCommand(s12, con);
            ////            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
            ////            DataTable dt23 = new DataTable();

            ////            adp123.Fill(dt23);
            ////            if (dt23.Rows.Count > 0)
            ////            {
            ////                drp.SelectedIndex = drp.Items.IndexOf(drp.Items.FindByValue(dt23.Rows[0]["InventorySubSubId"].ToString()));
            ////            }
            ////        }
            ////        ModalPopupExtender1.Show();
            ////    }

            ////}
            ////////

            //int a = Convert.ToInt32(ds0.Tables[0].Rows[0]["TotalItem"]);
            //if (a > 0)
            //{

            //    return;
            //}
            //else
            //{
            //    GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //    Session["ID9"] = GridView1.SelectedDataKey.Value;
            //    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //    string sr4 = ("delete from InventoruSubSubCategory where InventorySubCatId=" + Session["ID9"] + "");
            //    SqlCommand cmd8 = new SqlCommand(sr4, con);

            //    con.Open();
            //    cmd8.ExecuteNonQuery();
            //    con.Close();

        }



    }
    protected DataSet FillSubCategory()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        string s = "SELECT InventorySubCatName, InventoryCategoryMasterId, InventorySubCatId FROM InventorySubCategoryMaster";


        SqlCommand c = new SqlCommand(s, con);
        c.CommandType = CommandType.Text;
        SqlDataAdapter d = new SqlDataAdapter(c);
        DataSet ds0 = new DataSet();
        d.Fill(ds0);
        return ds0;

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        //Response.Write("Updating");
        //DropDownList dlstSubCName = (DropDownList)GridView1.Rows[e.RowIndex ].FindControl("cmbSubCategory");
        //GridView1.EditIndex = e;
        ////EventArgs gg = new EventArgs();
        ////btngo_Click(sender, gg);


        DropDownList subcat = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("cmbSubCategory");

        //Int32 SubSubCatId = Int32.Parse(GridView1.DataKeys[GridView1.EditIndex].Value);
        int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);
        CheckBox ch123 = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("chkinvMasterStatus");
        TextBox name = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtInventorySubSubName");
        DropDownList dlst = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("cmbSubCategory");
        //Int32 SubCid = Int32.Parse(dlst.SelectedValue);
        string sgw = "select InventorySubSubName from InventoruSubSubCategory where " +
          " InventorySubSubName='" + name.Text + "' and InventorySubCatID='" + dlst.SelectedValue + "' and InventorySubSubId<>" + dk + "";
        //SELECT     InventorySubSubId, InventorySubSubName, InventorySubCatID FROM         InventoruSubSubCategory
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
            string fetchcatergory = "select * from InventoryMaster where InventorySubSubId=" + dk;
            SqlDataAdapter adp = new SqlDataAdapter(fetchcatergory, con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ch123.Checked == false)
                {
                    statuslable.Visible = true;
                    statuslable.Text = " This is active record";
                }
                else
                {
                    bool access = UserAccess.Usercon("InventoruSubSubCategory", "", "InventorySubSubId", "", "", "InventoryCategoryMaster.compid", "InventoruSubSubCategory   inner join  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId inner join  InventoryCategoryMaster ON  InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId");
                    if (access == true)
                    {
                        bool access1 = UserAccess.Usercon("InventoruSubSubCategory", subcat.SelectedValue, "InventorySubSubId", "", "", "InventoryCategoryMaster.compid", "InventoruSubSubCategory   inner join  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId inner join  InventoryCategoryMaster ON  InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId");
                        if (access1 == true)
                        {
                            string s1 = "UPDATE InventoruSubSubCategory SET [InventorySubSubName] ='" + name.Text + "' , [Activestatus]='" + ch123.Checked + "', " +
                                    " [InventorySubCatID] = '" + subcat.SelectedValue + "' WHERE [InventorySubSubId] ='" + dk.ToString() + "'";
                            SqlCommand cmd333 = new SqlCommand(s1, con);
                            con.Open();
                            cmd333.ExecuteNonQuery();
                            con.Close();

                            statuslable.Visible = true;
                            statuslable.Text = "Record updated successfully";
                            GridView1.EditIndex = -1;
                            fillgrid();
                        }
                        else
                        {
                            statuslable.Visible = true;
                            statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
                        }
                    }
                    else
                    {
                        statuslable.Visible = true;
                        statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
                    }
                }
            }
            else
            {
                bool access = UserAccess.Usercon("InventoruSubSubCategory", "", "InventorySubSubId", "", "", "InventoryCategoryMaster.compid", "InventoruSubSubCategory   inner join  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId inner join  InventoryCategoryMaster ON  InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId");
                if (access == true)
                {
                    bool access1 = UserAccess.Usercon("InventoruSubSubCategory", subcat.SelectedValue, "InventorySubSubId", "", "", "InventoryCategoryMaster.compid", "InventoruSubSubCategory   inner join  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId inner join  InventoryCategoryMaster ON  InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId");
                    if (access1 == true)
                    {
                        string s1 = "UPDATE InventoruSubSubCategory SET [InventorySubSubName] ='" + name.Text + "' , [Activestatus]='" + ch123.Checked + "', " +
                            " [InventorySubCatID] = '" + subcat.SelectedValue + "' WHERE [InventorySubSubId] ='" + dk.ToString() + "'";

                        SqlCommand cmd333 = new SqlCommand(s1, con);
                        con.Open();
                        cmd333.ExecuteNonQuery();
                        con.Close();

                        statuslable.Visible = true;
                        statuslable.Text = "Record updated successfully";
                        GridView1.EditIndex = -1;
                        fillgrid();
                    }
                    else
                    {
                        statuslable.Visible = true;
                        statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
                    }
                }
                else
                {
                    statuslable.Visible = true;
                    statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
                }
            }
            // FillStateMethod();
        }
    }
    //protected void ImgBtnMove_Click(object sender, ImageClickEventArgs e)
    //{
    //    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    //    //if (System.Windows.Forms.MessageBox.Show("Are you sure, You want to Delete ?", "Confirm Delete!", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
    //    //{
    //    ViewState["dle"] = 2;

    //    ModalPopupExtender1222.Show();

    //    //EventArgs gg = new EventArgs();
    //    //btngo_Click(sender, gg);

    //    //GridView1.DataBind();
    //}
    //protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    //{
    //    ModalPopupExtender1.Hide();
    //}
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        //ViewState["editid"] = GridView1.DataKeys[e.NewEditIndex].Value.ToString();

        //string str = "select * from InventoruSubSubCategory where InventorySubSubId='" + ViewState["editid"] + "'";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adpt.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    ddlInventorySubCatID.Items.Clear();

        //    ddlInventorySubCatID.DataSource = (DataSet)getall();
        //    ddlInventorySubCatID.DataValueField = "InventorySubCatId";
        //    ddlInventorySubCatID.DataTextField = "category";
        //    ddlInventorySubCatID.DataBind();

        //    ddlInventorySubCatID.SelectedIndex = ddlInventorySubCatID.Items.IndexOf(ddlInventorySubCatID.Items.FindByValue(dt.Rows[0]["InventorySubCatID"].ToString()));
        //    txtInventorySubSubName.Text = dt.Rows[0]["InventorySubSubName"].ToString();

        //    string chk = dt.Rows[0]["Activestatus"].ToString();
        //    if (chk == "True")
        //    {
        //        //chk12.Checked = true;
        //        ddlstatus.SelectedValue = "1";
        //    }
        //    else
        //    {
        //       // chk12.Checked = false;
        //        ddlstatus.SelectedValue = "0";
        //    }
        //    pnladd.Visible = true;
        //    lbllegend.Visible = true;
        //    lbllegend.Text = "Edit Sub Sub Category For Your Inventory";
        //    btnadd.Visible = false;
        //    btnupdate.Visible = true;
        //    ImageButton2.Visible = false;
        //    statuslable.Text = "";
        //}




    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void ddlSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    //protected void btnsubmit_Click(object sender, ImageClickEventArgs e)
    //{


    //}
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        ViewState["dle"] = 0;
        GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        Session["ID9"] = GridView1.SelectedIndex;
        string s = "    SELECT   InventoruSubSubCategory.InventorySubSubId, COUNT(InventoryMaster.InventoryMasterId) AS TotalItem, InventorySubCategoryMaster.InventorySubCatName,  " +
                    "    InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId,  " +
                    "    InventoryCategoryMaster.InventoryCatName,InventoryMaster.InventoryMasterId,  InventorySubCategoryMaster.InventorySubCatId " +
                    "   FROM         InventorySubCategoryMaster INNER JOIN " +
                    "    InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID INNER JOIN " +
                    "    InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId INNER JOIN " +
                    "    InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
                    " WHERE     (InventoryMaster.InventorySubSubId = " + Session["ID9"] + ") " +
                   " GROUP BY InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID,  " +
                    "    InventorySubCategoryMaster.InventorySubCatName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName,InventoryMaster.InventoryMasterId,  InventorySubCategoryMaster.InventorySubCatId ";

        SqlCommand c = new SqlCommand(s, con);
        c.CommandType = CommandType.Text;
        SqlDataAdapter d = new SqlDataAdapter(c);
        DataSet ds0 = new DataSet();
        d.Fill(ds0);

        ////////
        int a = Convert.ToInt32(ds0.Tables[0].Rows.Count);
        if (a == 0)
        {
            //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //Session["ID9"] = GridView1.SelectedDataKey.Value;

            //if (System.Windows.Forms.MessageBox.Show(" Are you Sure, You want to Delete ?", "Confirm Delete!", System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)//"System.Windows.Forms.MessageBoxButtons.YesNo")
            //{
            ViewState["dle"] = 1;
            // ModalPopupExtender1222.Show();
            yes_Click(sender, e);
            //}
            // a = 0;
        }
        else
        {
            ////a = 1;
            ////return;

            GridView2.DataSource = ds0;
            GridView2.DataBind();
            lblTotal.Text = a.ToString();

            if (lblTotal.Text == "1")
            {
                Label13.Text = "inventory. Please move it before deleting.";
            }
            else
            {
                Label13.Text = "inventories. Please move them before deleting.";

            }

            int i = 0;
            foreach (GridViewRow gdr in GridView2.Rows)
            {
                DropDownList drp = (DropDownList)gdr.Cells[0].FindControl("DropDownList2");

                Label insubid = (Label)gdr.Cells[0].FindControl("insubid");
                string strdddlsscat = " select InventorySubSubName,InventorySubSubId from InventoruSubSubCategory where InventorySubCatID='" + insubid.Text + "'  ";
                SqlCommand cmdddlsscat = new SqlCommand(strdddlsscat, con);
                SqlDataAdapter adpddlsscat = new SqlDataAdapter(cmdddlsscat);
                DataSet dsss = new DataSet();

                adpddlsscat.Fill(dsss);

                drp.DataSource = (dsss);
                drp.DataTextField = "InventorySubSubName";
                drp.DataValueField = "InventorySubSubId";
                drp.DataBind();

                string s12 = "    SELECT     InventoruSubSubCategory.InventorySubSubId, COUNT(InventoryMaster.InventoryMasterId) AS TotalItem, InventorySubCategoryMaster.InventorySubCatName,  " +
             "    InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId,InventoryMaster.InventoryMasterId,  " +
             "    InventoryCategoryMaster.InventoryCatName " +
             "   FROM         InventorySubCategoryMaster INNER JOIN " +
             "    InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID INNER JOIN " +
             "    InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId INNER JOIN " +
             "    InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
             " WHERE     (InventoryMaster.InventorySubSubId = " + Session["ID9"] + ") and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1 " +
            " GROUP BY InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID,  " +
             "    InventorySubCategoryMaster.InventorySubCatName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName,InventoryMaster.InventoryMasterId ";
                SqlCommand cmd123 = new SqlCommand(s12, con);
                SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
                DataTable dt23 = new DataTable();

                adp123.Fill(dt23);
                if (dt23.Rows.Count > 0)
                {
                    drp.SelectedIndex = drp.Items.IndexOf(drp.Items.FindByValue(dt23.Rows[0]["InventorySubSubId"].ToString()));
                    drp.Items.RemoveAt(drp.SelectedIndex);
                }
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
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void ddlInventorySubCatID_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();//Page_Load(sender, e);
        statuslable.Visible = false;
    }
    //protected void yes_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    //protected void Button661_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    //protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    //{
    //    statuslable.Visible = false;
    //    ddlInventorySubCatID.SelectedIndex = -1;
    //    txtInventorySubSubName.Text = "";
    //}
    //protected void ddlsubsubcat_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //}
    //    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    //    {
    //        if (ddlCat.SelectedIndex > 0 && ddlSubCat.SelectedIndex == 0 && ddlsubsubcat.SelectedIndex == 0)
    //        {
    //            //SqlDataSource1.SelectCommand = "SELECT InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName FROM InventoruSubSubCategory INNER JOIN InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryCategoryMaster.InventeroyCatId = '" + ddlCat.SelectedValue +"'";
    //            //       SqlDataSource1.SelectCommand = "      SELECT     InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatName, COUNT(InventoryMaster.InventoryMasterId)  "+
    //            //                  "    AS TotalItem, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubCatID, "+
    //            //                   "   InventorySubCategoryMaster.InventoryCategoryMasterId "+
    //            //"   FROM         InventoryMaster RIGHT OUTER JOIN "+
    //            //    "                  InventoruSubSubCategory INNER JOIN "+
    //            //     "                 InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON "+
    //            //      "                InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId "+
    //            //"   WHERE     InventorySubCategoryMaster.InventoryCategoryMasterId="+ddlCat.SelectedValue+" "+
    //            //"  GROUP BY InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatName, "+
    //            //                     " InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubCatID, InventorySubCategoryMaster.InventoryCategoryMasterId";

    //            //SqlDataSource1.SelectCommand = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, " +
    //            //         "  InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName " +
    //            //         " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //            //        "   InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId LEFT OUTER JOIN " +
    //            //        "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
    //            //        "   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId "+
    //            //        " WHERE     InventorySubCategoryMaster.InventoryCategoryMasterId=" +Convert.ToInt32( ddlCat.SelectedValue) + " ";
    //            string str1 = " SELECT     InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, " +
    //                     " InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, " +
    //                     " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName AS CatSubCat " +
    //                     " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //                     " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
    //                     " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
    //                        " where InventoryCategoryMaster.InventeroyCatId = '" + ddlCat.SelectedValue + "' and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1";
    //            SqlCommand cmdddd232 = new SqlCommand(str1, con);
    //            SqlDataAdapter adpasd = new SqlDataAdapter(cmdddd232);
    //            DataTable dtadsta = new DataTable();
    //            adpasd.Fill(dtadsta);
    //            if (dtadsta.Rows.Count > 0)
    //            {
    //                GridView1.DataSource = dtadsta;
    //                DataView myDataView = new DataView();

    //                myDataView = dtadsta.DefaultView;

    //                if (hdnsortExp.Value != string.Empty)
    //                {
    //                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
    //                }

    //                GridView1.DataBind();
    //            }
    //            else
    //            {
    //                GridView1.DataSource = null;
    //                GridView1.DataBind();
    //            }
    //            //SqlDataSource1.DataBind();

    //        }
    //        else if (ddlCat.SelectedIndex == 0 && ddlSubCat.SelectedIndex > 0 && ddlsubsubcat.SelectedIndex > 0)
    //        {
    //            //SqlDataSource1.SelectCommand = "SELECT InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName FROM InventoruSubSubCategory INNER JOIN InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryCategoryMaster.InventeroyCatId = '" + ddlCat.SelectedValue +"'";
    //            //       SqlDataSource1.SelectCommand = "      SELECT     InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatName, COUNT(InventoryMaster.InventoryMasterId)  "+
    //            //                  "    AS TotalItem, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubCatID, "+
    //            //                   "   InventorySubCategoryMaster.InventoryCategoryMasterId "+
    //            //"   FROM         InventoryMaster RIGHT OUTER JOIN "+
    //            //    "                  InventoruSubSubCategory INNER JOIN "+
    //            //     "                 InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON "+
    //            //      "                InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId "+
    //            //"   WHERE     InventorySubCategoryMaster.InventoryCategoryMasterId="+ddlCat.SelectedValue+" "+
    //            //"  GROUP BY InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatName, "+
    //            //                     " InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubCatID, InventorySubCategoryMaster.InventoryCategoryMasterId";

    //            //SqlDataSource1.SelectCommand = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, " +
    //            //         "  InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName " +
    //            //         " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //            //        "   InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId LEFT OUTER JOIN " +
    //            //        "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
    //            //        "   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId "+
    //            //        " WHERE     InventorySubCategoryMaster.InventoryCategoryMasterId=" +Convert.ToInt32( ddlCat.SelectedValue) + " ";
    //            string str13 = " SELECT     InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, " +
    //                     " InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, " +
    //                     " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName AS CatSubCat " +
    //                     " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //                     " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
    //                     " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
    //                     " where InventorySubCategoryMaster.InventorySubCatId = '" + ddlSubCat.SelectedValue + "' and InventoruSubSubCategory.InventorySubSubI='" + ddlsubsubcat.SelectedValue + "' and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1";
    //            SqlCommand cmdddd2323 = new SqlCommand(str13, con);
    //            SqlDataAdapter adpasd3 = new SqlDataAdapter(cmdddd2323);
    //            DataTable dtadsta3 = new DataTable();
    //            adpasd3.Fill(dtadsta3);
    //            if (dtadsta3.Rows.Count > 0)
    //            {
    //                GridView1.DataSource = dtadsta3;
    //                //ataView myDataView = new DataView();
    //                DataView myDataView = new DataView();
    //                myDataView = dtadsta3.DefaultView;

    //                if (hdnsortExp.Value != string.Empty)
    //                {
    //                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
    //                }

    //                GridView1.DataBind();
    //            }
    //            else
    //            {
    //                GridView1.DataSource = null;
    //                GridView1.DataBind();
    //            }
    //            //SqlDataSource1.DataBind();

    //        }
    //        //        else if(ddlSubCat.SelectedIndex > 0)
    //        //        {
    //        //            //SqlDataSource1.SelectCommand = "SELECT InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName FROM InventoruSubSubCategory INNER JOIN InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventorySubCategoryMaster.InventorySubCatId = '" + ddlSubCat.SelectedValue + "'";

    ////    ////        SqlDataSource1.SelectCommand = "    SELECT     InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatName, COUNT(InventoryMaster.InventoryMasterId) " +
    //        //    ////                        "AS TotalItem, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubCatID " +
    //        //    ////"  FROM         InventoryMaster RIGHT OUTER JOIN " +
    //        //    ////   "                   InventoruSubSubCategory INNER JOIN " +
    //        //    ////    "                  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON  " +
    //        //    ////     "                 InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId " +
    //        //    ////  "  where InventoruSubSubCategory.InventorySubCatID="+ddlSubCat.SelectedValue+"" +
    //        //    ////  "  GROUP BY InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatName,  " +
    //        //    ////     "                 InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubCatID ";



    ////            //SqlDataSource1.SelectCommand = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, " +
    //        //            //        "  InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName " +
    //        //            //        " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //        //            //       "   InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId LEFT OUTER JOIN " +
    //        //            //       "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
    //        //            //       "   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId " +
    //        //            SqlDataSource1.SelectCommand = "SELECT     InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, " +
    //        //                      " InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId, " +
    //        //"                      InventoruSubSubCategory.InventorySubSubName" +
    //        //" FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //        //                      " InventorySubCategoryMaster ON " +
    //        //                      " InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId LEFT OUTER JOIN " +
    //        //"                      InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +

    ////                   " WHERE     InventorySubCategoryMaster.InventoryCategoryMasterId=" + Convert.ToInt32(ddlSubCat.SelectedValue) + " ";
    //        //           // SqlDataSource1.DataBind();
    //        //            SqlDataSource1.DataBind();
    //        //        }
    //        else if (ddlCat.SelectedIndex > 0 && ddlSubCat.SelectedIndex > 0 && ddlsubsubcat.SelectedIndex == 0)
    //        {
    //            ////////         SqlDataSource1.SelectCommand = "    SELECT     InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatName, COUNT(InventoryMaster.InventoryMasterId) " +
    //            ////////                        "AS TotalItem, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubCatID " +
    //            ////////"  FROM         InventoryMaster RIGHT OUTER JOIN " +
    //            ////////   "                   InventoruSubSubCategory INNER JOIN " +
    //            ////////    "                  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON  " +
    //            ////////     "                 InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId " +
    //            ////////  "  where InventoruSubSubCategory.InventorySubCatID=" + ddlSubCat.SelectedValue + "" +
    //            ////////  "  GROUP BY InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatName,  " +
    //            ////////     "                 InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubCatID ";
    //            ////////         SqlDataSource1.DataBind();


    //            //SqlDataSource1.SelectCommand = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, " +
    //            //       "  InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName " +
    //            //       " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //            //      "   InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId LEFT OUTER JOIN " +
    //            //      "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
    //            //      "   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId " +
    //            //      " WHERE     InventorySubCategoryMaster.InventoryCategoryMasterId=" + Convert.ToInt32(ddlSubCat.SelectedValue) + " ";
    //            // " WHERE     InventoruSubSubCategory.InventorySubCatID=" + Convert.ToInt32(ddlSubCat.SelectedValue) + " ";

    //            string str111 = " SELECT     InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, " +
    //                     " InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, " +
    //                     " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName AS CatSubCat " +
    //                     " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //                     " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
    //                     " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
    //                     " where  InventoryCategoryMaster.InventeroyCatId = '" + ddlCat.SelectedValue + "' and InventorySubCategoryMaster.InventorySubCatId=" + Convert.ToInt32(ddlSubCat.SelectedValue) + " and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1";
    //            // SqlDataSource1.DataBind();
    //            //SqlDataSource1.DataBind();
    //            SqlCommand cmd111 = new SqlCommand(str111, con);
    //            SqlDataAdapter ad111 = new SqlDataAdapter(cmd111);
    //            DataTable dt111 = new DataTable();
    //            ad111.Fill(dt111);
    //            if (dt111.Rows.Count > 0)
    //            {
    //                GridView1.DataSource = dt111;
    //                DataView myDataView = new DataView();
    //                myDataView = dt111.DefaultView;

    //                if (hdnsortExp.Value != string.Empty)
    //                {
    //                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
    //                }

    //                GridView1.DataBind();
    //            }
    //            else
    //            {
    //                GridView1.DataSource = null;
    //                GridView1.DataBind();
    //            }
    //        }
    //        else if (ddlCat.SelectedIndex > 0 && ddlSubCat.SelectedIndex > 0 && ddlsubsubcat.SelectedIndex > 0)
    //        {
    //            ////////         SqlDataSource1.SelectCommand = "    SELECT     InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatName, COUNT(InventoryMaster.InventoryMasterId) " +
    //            ////////                        "AS TotalItem, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubCatID " +
    //            ////////"  FROM         InventoryMaster RIGHT OUTER JOIN " +
    //            ////////   "                   InventoruSubSubCategory INNER JOIN " +
    //            ////////    "                  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON  " +
    //            ////////     "                 InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId " +
    //            ////////  "  where InventoruSubSubCategory.InventorySubCatID=" + ddlSubCat.SelectedValue + "" +
    //            ////////  "  GROUP BY InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatName,  " +
    //            ////////     "                 InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubCatID ";
    //            ////////         SqlDataSource1.DataBind();


    //            //SqlDataSource1.SelectCommand = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, " +
    //            //       "  InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName " +
    //            //       " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //            //      "   InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId LEFT OUTER JOIN " +
    //            //      "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
    //            //      "   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId " +
    //            //      " WHERE     InventorySubCategoryMaster.InventoryCategoryMasterId=" + Convert.ToInt32(ddlSubCat.SelectedValue) + " ";
    //            // " WHERE     InventoruSubSubCategory.InventorySubCatID=" + Convert.ToInt32(ddlSubCat.SelectedValue) + " ";
    //            string strrr1 = " SELECT     InventoryCategoryMaster.InventeroyCatId, InventorySubCategoryMaster.InventorySubCatId, InventoruSubSubCategory.InventorySubSubId, " +
    //                     "InventoruSubSubCategory.InventorySubSubName, " +
    //                    " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName AS CatSubCat, " +
    //                     "InventoruSubSubCategory.InventorySubCatID " +
    //                     "FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //                     "InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
    //                     "InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
    //                      " where InventoruSubSubCategory.InventorySubSubId = '" + ddlsubsubcat.SelectedValue + "' and InventoryCategoryMaster.InventeroyCatId = '" + ddlCat.SelectedValue + "' and InventorySubCategoryMaster.InventorySubCatId='" + ddlSubCat.SelectedValue + "' and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1";
    //            //string strrr1 = " SELECT     InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, " +
    //            //         " InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, " +
    //            //         " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName AS CatSubCat " +
    //            //         " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //            //         " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
    //            //         " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID " +
    //            //         " where InventoruSubSubCategory.InventorySubSubId= " + Convert.ToInt32(ddlsubsubcat.SelectedValue) + " and InventoryCategoryMaster.InventeroyCatId = '" + ddlCat.SelectedValue + "' and InventorySubCategoryMaster.InventorySubCatId='" + ddlSubCat.SelectedValue + "'";
    //            // SqlDataSource1.DataBind();
    //            //SqlDataSource1.DataBind();
    //            SqlCommand cmdddd2322 = new SqlCommand(strrr1, con);
    //            SqlDataAdapter adpasd2 = new SqlDataAdapter(cmdddd2322);
    //            DataTable dtadsta2 = new DataTable();
    //            adpasd2.Fill(dtadsta2);
    //            if (dtadsta2.Rows.Count > 0)
    //            {
    //                GridView1.DataSource = dtadsta2;
    //                DataView myDataView = new DataView();
    //                myDataView = dtadsta2.DefaultView;

    //                if (hdnsortExp.Value != string.Empty)
    //                {
    //                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
    //                }

    //                GridView1.DataBind();
    //            }

    //            else
    //            {
    //                GridView1.DataSource = null;
    //                GridView1.DataBind();
    //            }

    //        }

    //        else
    //        {
    //            string str132 = " SELECT     InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, " +
    //                     " InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, " +
    //                     " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName AS CatSubCat " +
    //                     " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
    //                     " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
    //                     " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID  where InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1";

    //            SqlCommand cmdddd23232 = new SqlCommand(str132, con);
    //            SqlDataAdapter adpasd32 = new SqlDataAdapter(cmdddd23232);
    //            DataTable dtadsta32 = new DataTable();
    //            adpasd32.Fill(dtadsta32);
    //            if (dtadsta32.Rows.Count > 0)
    //            {
    //                GridView1.DataSource = dtadsta32;
    //                DataView myDataView = new DataView();
    //                myDataView = dtadsta32.DefaultView;

    //                if (hdnsortExp.Value != string.Empty)
    //                {
    //                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
    //                }

    //                GridView1.DataBind();
    //            }
    //            else
    //            {
    //                GridView1.DataSource = null;
    //                GridView1.DataBind();
    //            }

    //        }
    //    }


    //protected void ImageButton5_Click(object sender, EventArgs e)
    //{
    //    if (ViewState["dle"].ToString() == "1")
    //    {

    //        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    //        string sr4 = ("delete from InventoruSubSubCategory where InventorySubSubId=" + Session["ID9"] + "");
    //        SqlCommand cmd8 = new SqlCommand(sr4, con);

    //        con.Open();
    //        cmd8.ExecuteNonQuery();
    //        con.Close();
    //        statuslable.Visible = true;
    //        statuslable.Text = "Record Delete Successfilly";
    //        GridView1.EditIndex = -1;
    //        fillgrid();
    //        //object sen = new object();
    //        //EventArgs gg = new EventArgs();
    //        //ddlSubCat_SelectedIndexChanged(sen, gg);


    //    }
    //    else if (ViewState["dle"].ToString() == "2")
    //    {
    //        foreach (GridViewRow gdr in GridView2.Rows)
    //        {
    //            DropDownList drp = (DropDownList)gdr.Cells[0].FindControl("DropDownList2");
    //            Label invmid = (Label)gdr.FindControl("lblinvMid");


    //            string st = "Update InventoryMaster set InventorySubSubId=" + drp.SelectedValue + " where InventoryMasterId=" + Convert.ToInt32(invmid.Text) + "";
    //            SqlCommand Mycommand = new SqlCommand(st, con);
    //            con.Open();
    //            Mycommand.ExecuteNonQuery();
    //            con.Close();
    //            //fillgrid();
    //            // Label1.Visible = true;
    //            // Label1.Text = "Record Update Successfilly";
    //        }
    //        //    }
    //        //}
    //        //else
    //        //{

    //        string sr = "    SELECT     InventoruSubSubCategory.InventorySubSubId, COUNT(InventoryMaster.InventoryMasterId) AS TotalItem, InventorySubCategoryMaster.InventorySubCatName,  " +
    //             "    InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId, InventoryMaster.InventoryMasterId, " +
    //             "    InventoryCategoryMaster.InventoryCatName " +
    //             "   FROM         InventorySubCategoryMaster INNER JOIN " +
    //             "    InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID INNER JOIN " +
    //             "    InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId INNER JOIN " +
    //             "    InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
    //             " WHERE     (InventoryMaster.InventorySubSubId = " + Session["ID9"] + ") and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1 " +
    //            " GROUP BY InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID,  " +
    //             "    InventorySubCategoryMaster.InventorySubCatName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName,InventoryMaster.InventoryMasterId ";

    //        SqlCommand cr = new SqlCommand(sr, con);
    //        cr.CommandType = CommandType.Text;
    //        SqlDataAdapter dr = new SqlDataAdapter(cr);
    //        DataTable ds02 = new DataTable();
    //        dr.Fill(ds02);
    //        if (ds02.Rows.Count > 0)
    //        {
    //            statuslable.Visible = true;
    //            statuslable.Text = ds02.Rows.Count + " : Record Left for - '" + ds02.Rows[0]["InventorySubSubName"].ToString() + "' Sub sub Category ";
    //            fillgrid();
    //        }
    //        else
    //        {
    //            string sr4 = ("delete from InventoruSubSubCategory where InventorySubSubId=" + Session["ID9"] + "");
    //            SqlCommand cmd8 = new SqlCommand(sr4, con);

    //            con.Open();
    //            cmd8.ExecuteNonQuery();
    //            con.Close();

    //            statuslable.Visible = true;
    //            statuslable.Text = "Record Delete Successfilly";
    //            // fillgrid();
    //            fillgrid();
    //        }




    //    }
    //    else
    //    {

    //    }

    //    // EventArgs gg1 = new EventArgs();
    //    // btngo_Click(sender,gg1);
    //    //}
    //    ModalPopupExtender1.Hide();
    //    ModalPopupExtender1222.Hide();
    //}
    //protected void ImageButton6_Click(object sender, EventArgs e)
    //{

    //}

    protected void ImgBtnMove_Click(object sender, EventArgs e)
    {
        ViewState["dle"] = 2;
        yes_Click(sender, e);
        // ModalPopupExtender1222.Show();
    }
    //protected void ImageButton4_Click(object sender, EventArgs e)
    //{
    //    ModalPopupExtender1.Hide();
    //}
    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        statuslable.Visible = false;
        ddlInventorySubCatID.SelectedIndex = -1;
        txtInventorySubSubName.Text = "";
        statuslable.Visible = false;
        pnladd.Visible = false;
        lbllegend.Visible = false;
        lbllegend.Text = "Add a New Sub Sub Category For Your Inventory";
        btnadd.Visible = true;

        ImageButton2.Visible = true;
        btnupdate.Visible = false;
        GridView1.EditIndex = -1;
        fillgrid();

    }
    protected void yes_Click(object sender, EventArgs e)
    {
        if (ViewState["dle"].ToString() == "1")
        {

            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            string sr4 = ("delete from InventoruSubSubCategory where InventorySubSubId=" + Session["ID9"] + "");
            SqlCommand cmd8 = new SqlCommand(sr4, con);

            con.Open();
            cmd8.ExecuteNonQuery();
            con.Close();
            statuslable.Visible = true;
            statuslable.Text = "Record deleted successfully";
            GridView1.EditIndex = -1;
            fillgrid();



        }
        else if (ViewState["dle"].ToString() == "2")
        {
            int i = 0;

            foreach (GridViewRow gdr in GridView2.Rows)
            {
                DropDownList drp = (DropDownList)gdr.Cells[0].FindControl("DropDownList2");
                Label invmid = (Label)gdr.FindControl("lblinvMid");
                if (drp.Items.Count <= 0)
                {
                    i = 1;
                }
                if (i == 0)
                {

                    string st = "Update InventoryMaster set InventorySubSubId=" + drp.SelectedValue + " where InventoryMasterId=" + Convert.ToInt32(invmid.Text) + "";
                    SqlCommand Mycommand = new SqlCommand(st, con);
                    con.Open();
                    Mycommand.ExecuteNonQuery();
                    con.Close();
                }

            }
            if (i == 0)
            {

                string sr = "    SELECT     InventoruSubSubCategory.InventorySubSubId, COUNT(InventoryMaster.InventoryMasterId) AS TotalItem, InventorySubCategoryMaster.InventorySubCatName,  " +
                     "    InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId, InventoryMaster.InventoryMasterId, " +
                     "    InventoryCategoryMaster.InventoryCatName " +
                     "   FROM         InventorySubCategoryMaster INNER JOIN " +
                     "    InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID INNER JOIN " +
                     "    InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId INNER JOIN " +
                     "    InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
                     " WHERE     (InventoryMaster.InventorySubSubId = " + Session["ID9"] + ") and InventoryCategoryMaster.Activestatus=1 and InventorySubCategoryMaster.Activestatus=1 " +
                    " GROUP BY InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubCatID,  " +
                     "    InventorySubCategoryMaster.InventorySubCatName, InventoryMaster.Name, InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName,InventoryMaster.InventoryMasterId ";

                SqlCommand cr = new SqlCommand(sr, con);
                cr.CommandType = CommandType.Text;
                SqlDataAdapter dr = new SqlDataAdapter(cr);
                DataTable ds02 = new DataTable();
                dr.Fill(ds02);
                if (ds02.Rows.Count > 0)
                {
                    statuslable.Visible = true;
                    statuslable.Text = ds02.Rows.Count + " : Record Left for - '" + ds02.Rows[0]["InventorySubSubName"].ToString() + "' Sub sub Category ";
                    fillgrid();
                }
                else
                {
                    string sr4 = ("delete from InventoruSubSubCategory where InventorySubSubId=" + Session["ID9"] + "");
                    SqlCommand cmd8 = new SqlCommand(sr4, con);

                    con.Open();
                    cmd8.ExecuteNonQuery();
                    con.Close();

                    statuslable.Visible = true;
                    statuslable.Text = "Record deleted successfully";
                    // fillgrid();
                    fillgrid();
                }
            }
            else
            {
                statuslable.Visible = true;
                statuslable.Text = "Sorry,You have no other sub sub category to move so you cannot delete this sub sub category ";
            }



        }
        else
        {

        }

        // EventArgs gg1 = new EventArgs();
        // btngo_Click(sender,gg1);
        //}
        ModalPopupExtender1.Hide();
        // ModalPopupExtender1222.Hide();

    }

    protected void ddlActive_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void Button661_Click(object sender, EventArgs e)
    {
        // ModalPopupExtender1222.Hide();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillgrid();

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
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

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(300);

            Button1.Text = "Printable Version";
            Button7.Visible = false;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            fillgrid();

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
        string sgw = "select InventorySubSubName from InventoruSubSubCategory where " +
          " InventorySubSubName='" + txtInventorySubSubName.Text + "' and InventorySubCatID='" + ddlInventorySubCatID.SelectedValue + "' and InventorySubSubId<>" + ViewState["editid"] + "";

        SqlCommand cgw = new SqlCommand(sgw, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dtgw = new DataTable();
        adgw.Fill(dtgw);
        if (dtgw.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "This record already exists";
        }
        else
        {
            string fetchcatergory = "select * from InventoryMaster where InventorySubSubId=" + ViewState["editid"];
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
                    bool access = UserAccess.Usercon("InventoruSubSubCategory", "", "InventorySubSubId", "", "", "InventoryCategoryMaster.compid", "InventoruSubSubCategory   inner join  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId inner join  InventoryCategoryMaster ON  InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId");
                    if (access == true)
                    {
                        bool access1 = UserAccess.Usercon("InventoruSubSubCategory", ddlInventorySubCatID.SelectedValue, "InventorySubSubId", "", "", "InventoryCategoryMaster.compid", "InventoruSubSubCategory   inner join  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId inner join  InventoryCategoryMaster ON  InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId");
                        if (access1 == true)
                        {
                            string s1 = "UPDATE InventoruSubSubCategory SET [InventorySubSubName] ='" + txtInventorySubSubName.Text + "' , [Activestatus]='" + ddlstatus.SelectedValue + "', " +
                                    " [InventorySubCatID] = '" + ddlInventorySubCatID.SelectedValue + "' WHERE [InventorySubSubId] ='" + ViewState["editid"] + "'";
                            SqlCommand cmd333 = new SqlCommand(s1, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd333.ExecuteNonQuery();
                            con.Close();



                            pnladd.Visible = false;
                            lbllegend.Visible = false;
                            lbllegend.Text = "Add a New Sub Sub Category For Your Inventory";
                            btnadd.Visible = true;
                            btnupdate.Visible = false;
                            ImageButton2.Visible = true;
                            statuslable.Visible = false;
                            ddlInventorySubCatID.SelectedIndex = -1;
                            txtInventorySubSubName.Text = "";
                            GridView1.EditIndex = -1;
                            fillgrid();
                            statuslable.Visible = true;
                            statuslable.Text = "Record updated successfully";
                            ddlstatus.SelectedIndex = 0;
                        }
                        else
                        {
                            statuslable.Visible = true;
                            statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
                        }
                    }
                    else
                    {
                        statuslable.Visible = true;
                        statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
                    }
                }
            }
            else
            {
                bool access = UserAccess.Usercon("InventoruSubSubCategory", "", "InventorySubSubId", "", "", "InventoryCategoryMaster.compid", "InventoruSubSubCategory   inner join  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId inner join  InventoryCategoryMaster ON  InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId");
                if (access == true)
                {
                    bool access1 = UserAccess.Usercon("InventoruSubSubCategory", ddlInventorySubCatID.SelectedValue, "InventorySubSubId", "", "", "InventoryCategoryMaster.compid", "InventoruSubSubCategory   inner join  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId inner join  InventoryCategoryMaster ON  InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId");
                    if (access1 == true)
                    {
                        string s1 = "UPDATE InventoruSubSubCategory SET [InventorySubSubName] ='" + txtInventorySubSubName.Text + "' , [Activestatus]='" + ddlstatus.SelectedValue + "', " +
                            " [InventorySubCatID] = '" + ddlInventorySubCatID.SelectedValue + "' WHERE [InventorySubSubId] ='" + ViewState["editid"] + "'";

                        SqlCommand cmd333 = new SqlCommand(s1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd333.ExecuteNonQuery();
                        con.Close();



                        pnladd.Visible = false;
                        lbllegend.Visible = false;
                        btnadd.Visible = true;
                        btnupdate.Visible = false;
                        ImageButton2.Visible = true;
                        statuslable.Visible = false;
                        ddlInventorySubCatID.SelectedIndex = -1;
                        txtInventorySubSubName.Text = "";
                        GridView1.EditIndex = -1;
                        fillgrid();
                        statuslable.Visible = true;
                        statuslable.Text = "Record updated successfully";

                    }
                    else
                    {
                        statuslable.Visible = true;
                        statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
                    }
                }
                else
                {
                    statuslable.Visible = true;
                    statuslable.Text = "Sorry, You are not allowed to insert any more records as per your Price Plan";
                }
            }

        }
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "InventorySubCategoryMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        ddlInventorySubCatID.DataSource = (DataSet)getall();
        ddlInventorySubCatID.DataValueField = "InventorySubCatId";
        ddlInventorySubCatID.DataTextField = "category";
        ddlInventorySubCatID.DataBind();

    }
}
