using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class ShoppingCart_Admin_FaqCategoryMaster : System.Web.UI.Page
{
    // int i;
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;
    string compid;
    //string compid1;
    string companyid;
    //string wh;
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
        compid = Session["Comid"].ToString();
        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);


        companyid = Session["Comid"].ToString();

        Label1.Visible = false;
        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            lblCompany.Text = Session["Cname"].ToString();





            fillstore();
            fillstorefilter();

            ViewState["sortOrder"] = "";
            fillgrid();




        }

    }






    public void fillddlcategory()
    {

        string companyid = Session["Comid"].ToString();
        string viewcategory = " select FAQCategoryMaster_Id,FAQCategoryName from FAQCategoryMaster " +
                            "   where Compid='" + companyid + "' ORDER BY FAQCategoryName";
        //**********   Radhika Chnages
        //SqlCommand cmd = new SqlCommand("Sp_Select_FAQCategoryMaster", con);
        //**************
        SqlCommand cmd = new SqlCommand(viewcategory, con);
        //cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlfaqcategory0.DataSource = ds;
            ddlfaqcategory0.DataTextField = "FAQCategoryName";
            ddlfaqcategory0.DataValueField = "FAQCategoryMaster_Id";
            ddlfaqcategory0.DataBind();
            ddlfaqcategory0.Items.Insert(0, "--All--");
        }
    }
    protected void fillgrid()
    {
        string companyid = Session["Comid"].ToString();
        string str = "";
        string str1 = "";


        lblCompany0.Text = DropDownList1.SelectedItem.Text;

        if (DropDownList1.SelectedIndex > 0)
        {
            str1 = "and FAQCategoryMaster.Whid='" + DropDownList1.SelectedValue + "' ";
        }

        str = " FAQCategoryMaster_Id,FAQCategoryName,WareHouseMaster.WareHouseId,WareHouseMaster.Name from FAQCategoryMaster  inner join WareHouseMaster on FAQCategoryMaster.Whid=WareHouseMaster.WareHouseId where FAQCategoryMaster.Compid='" + companyid + "' " + str1 + " ";

        string str2 = "select count(FAQCategoryMaster.FAQCategoryMaster_Id) as ci from FAQCategoryMaster  inner join WareHouseMaster on FAQCategoryMaster.Whid=WareHouseMaster.WareHouseId where FAQCategoryMaster.Compid='" + companyid + "' " + str1 + " ";

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,FAQCategoryName asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable ds = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str);

            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }


        //if (ds.Rows.Count > 0)
        //{
        //    DataTable dten = GrdTempData();

        //    foreach (DataRow ddd in ds.Rows)
        //    {
        //        DataRow dtadd = dten.NewRow();

        //        dtadd["Whid"] = ddd["WareHouseId"].ToString();
        //        dtadd["Whname"] = ddd["Name"].ToString();
        //        dtadd["FAQCategoryMaster_Id"] = ddd["FAQCategoryMaster_Id"].ToString();
        //        dtadd["FAQCategoryName"] = ddd["FAQCategoryName"].ToString();
        //        dten.Rows.Add(dtadd);
        //    }
        //    if (dten.Rows.Count > 0)
        //    {
        //        DataView myDataView = new DataView();
        //        myDataView = dten.DefaultView;

        //        if (hdnsortExp.Value != string.Empty)
        //        {
        //            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //        }

        //        GridView1.DataSource = dten;
        //        GridView1.DataBind();
        //    }
        //    else
        //    {
        //        GridView1.DataSource = null;
        //        GridView1.DataBind();
        //    }


        //}
        //else
        //{
        //    GridView1.DataSource = null;
        //    GridView1.DataBind();
        //}
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

    protected DataTable GrdTempData()
    {
        DataTable dtTemp = new DataTable();


        DataColumn prd = new DataColumn();
        prd.ColumnName = "Whid";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "Whname";
        ssCatId.DataType = System.Type.GetType("System.String");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);
        DataColumn ssCatId1 = new DataColumn();
        ssCatId1.ColumnName = "FAQCategoryMaster_Id";
        ssCatId1.DataType = System.Type.GetType("System.String");
        ssCatId1.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId1);
        DataColumn ssCatId2 = new DataColumn();
        ssCatId2.ColumnName = "FAQCategoryName";
        ssCatId2.DataType = System.Type.GetType("System.String");
        ssCatId2.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId2);


        //Category

        return dtTemp;

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        //if (e.CommandName == "Sort")
        //{
        //    return;
        //}

        //if (e.CommandName == "Edit")
        //{
        //    //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);          
        //    //ViewState["Id"] = GridView1.SelectedDataKey.Value;
        //    //SqlCommand cmd = new SqlCommand("Select * from FAQCategoryMaster where [FAQCategoryMaster_Id]=" + ViewState["Id"]+"", con);
        //    //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    //DataSet ds = new DataSet();
        //    //adp.Fill(ds);
        //    //TextBox1.Text = ds.Tables[0].Rows[0]["FAQCategoryName"].ToString();
        //    //ViewState["n"] = 1;


        //}

        if (e.CommandName == "Edit")
        {
            ImageButton1.Visible = false;
            Button2.Visible = true;

            pnladd.Visible = true;
            lbllegend.Text = "Edit Frequently Asked Question Category";

            lbllegend.Visible = true;
            btnadd.Visible = false;

            int dk1 = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = dk1;

            string eeed = " select * from FAQCategoryMaster  where FAQCategoryMaster_Id='" + dk1 + "'  ";
            SqlCommand cmdeeed = new SqlCommand(eeed, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);

            fillstore();
            ddwarehouse.SelectedIndex = ddwarehouse.Items.IndexOf(ddwarehouse.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["Whid"]).ToString()));

            TextBox1.Text = dteeed.Rows[0]["FAQCategoryName"].ToString();

        }


        if (e.CommandName == "Delete")
        {

            int m = Convert.ToInt32(e.CommandArgument);

            //    GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //    ViewState["Id"] = GridView1.SelectedDataKey.Value;


            SqlCommand cmd1 = new SqlCommand("Select * from FAQMaster where FAQCategory_Id=" + m, con);
            SqlDataAdapter ado = new SqlDataAdapter(cmd1);
            DataTable dto = new DataTable();
            ado.Fill(dto);
            if (dto.Rows.Count > 0)
            {
                Label1.Visible = true;
                Label1.Text = "Sorry,First delete the record from FAQ Master before you attempt to delete this record again.";
            }
            else
            {
                SqlCommand cmd = new SqlCommand("delete  from FAQCategoryMaster where FAQCategoryMaster_Id=" + m, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record deleted successfully";
                //    GridView1.EditIndex = -1;
                fillgrid();
                fillddlcategory();
            }




            //SqlCommand cmd = new SqlCommand("delete  from FAQCategoryMaster where [FAQCategoryMaster_Id]=" + ViewState["Id"] + "", con);
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            //Label1.Visible = true;
            //Label1.Text = "Record Deleted Successfully";
            //GridView1.DataSource = (DataSet)fillgrid();
            //GridView1.DataBind();
            //TextBox1.Text = "";
            //GridView1.SelectedIndex = -1;

        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //ImageButton1.Visible = false;
        //Button2.Visible = true;

        //pnladd.Visible = true;
        //lbllegend.Text = "Edit Frequently Asked Question Category";

        //lbllegend.Visible = true;
        //btnadd.Visible = false;

        //int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        //ViewState["Id"] = dk1;

        //string eeed = " select * from FAQCategoryMaster  where FAQCategoryMaster_Id='" + dk1 + "'  ";
        //SqlCommand cmdeeed = new SqlCommand(eeed, con);
        //SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        //DataTable dteeed = new DataTable();
        //adpeeed.Fill(dteeed);

        //fillstore();
        //ddwarehouse.SelectedIndex = ddwarehouse.Items.IndexOf(ddwarehouse.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["Whid"]).ToString()));

        //TextBox1.Text = dteeed.Rows[0]["FAQCategoryName"].ToString();




    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // ModalPopupExtender1222.Show();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //i = Convert.ToInt32(ViewState["n"]);

        try
        {
            string strrrr = "select  FAQCategoryMaster_Id,FAQCategoryName from FAQCategoryMaster " +
                " where whid='" + ddwarehouse.SelectedValue + "' and FAQCategoryName= '" + TextBox1.Text + "' ";




            SqlCommand cmd1 = new SqlCommand(strrrr, con);

            SqlDataAdapter adp = new SqlDataAdapter(cmd1);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count == 0)
            {
                string companyid = Session["Comid"].ToString();
                // string companyid12 = companyid;
                SqlCommand cmd = new SqlCommand("Sp_Insert_FAQCategoryMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FAQCategoryName", TextBox1.Text);

                cmd.Parameters.AddWithValue("@Compid", companyid);
                cmd.Parameters.AddWithValue("@whid", ddwarehouse.SelectedValue.ToString());
                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }

                cmd.ExecuteNonQuery();

                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record inserted successfully";

                fillgrid();
                fillddlcategory();
                fillclear();

                pnladd.Visible = false;
                lbllegend.Text = "Add a New Frequently Asked Question Category";

                lbllegend.Visible = false;
                btnadd.Visible = true;
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "Record already used";

            }
        }
        catch (Exception)
        {
            Label1.Visible = true;
            Label1.Text = "Error";
        }
        finally { }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {


        pnladd.Visible = false;
        lbllegend.Text = "Add a New Frequently Asked Question Category";

        lbllegend.Visible = false;
        btnadd.Visible = true;
        fillclear();


    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        if (ddlfaqcategory0.SelectedIndex > 0)
        {
            ddlfaqcategory_SelectedIndexChanged(sender, e);
        }
        else
        {
            fillgrid();
        }
        //fillgrid();

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
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //// 
        ////GridView1.EditIndex = e.RowIndex;
        //string stupdatestr = ((TextBox)(GridView1.Rows[e.RowIndex].FindControl("txtfqName"))).Text;
        //// TextBox re = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtfqName");
        //Label fqid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblfqId");
        //Label whmid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblwhid");
        //DropDownList wh = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddwarehouse");


        //string strrrr = "select  FAQCategoryMaster_Id,FAQCategoryName from FAQCategoryMaster " +
        //       " where whid='" + wh.SelectedValue + "' and FAQCategoryName= '" + stupdatestr + "' and  FAQCategoryMaster_Id<>" + fqid.Text + " ";




        //SqlCommand cmd1 = new SqlCommand(strrrr, con);

        //SqlDataAdapter adp = new SqlDataAdapter(cmd1);
        //DataTable ds = new DataTable();
        //adp.Fill(ds);
        //if (ds.Rows.Count == 0)
        //{
        //    string str = "Update FAQCategoryMaster set Whid='" + wh.SelectedValue.ToString() + "',FAQCategoryName ='" + stupdatestr + "' where FAQCategoryMaster_Id=" + fqid.Text + "";
        //    SqlCommand cmd = new SqlCommand(str, con);
        //    if (con.State.ToString() != "Open")
        //    {
        //        con.Open();

        //    }

        // //   con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    Label1.Visible = true;
        //    Label1.Text = "Record updated successfully";
        //    //GridView1.DataSource = (DataSet)fillgrid();
        //    //GridView1.DataBind();

        //    GridView1.EditIndex = -1;
        //    if (ddlfaqcategory0.SelectedIndex > 0)
        //    {
        //        ddlfaqcategory_SelectedIndexChanged(sender, e);
        //    }
        //    else
        //    {
        //        fillgrid();
        //    }
        //}
        //else
        //{
        //    Label1.Visible = true;
        //    Label1.Text = "Sorry,Record already Exist";
        //}
        ////fillgrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        if (ddlfaqcategory0.SelectedIndex > 0)
        {
            ddlfaqcategory_SelectedIndexChanged(sender, e);
        }
        else
        {
            fillgrid();
        }
    }

    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlfaqcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        //if (ddlfaqcategory0.SelectedIndex > 0)
        //{
        //    SqlCommand cmdfill = new SqlCommand("Select * from FAQCategoryMaster where FAQCategoryMaster_Id='"  + ddlfaqcategory0.SelectedValue +"'",con);
        //    SqlDataAdapter dtpfill = new SqlDataAdapter(cmdfill);
        //    DataTable dtfill = new DataTable();
        //    dtpfill.Fill(dtfill);

        //    if (dtfill.Rows.Count > 0)
        //    {
        //        GridView1.DataSource = null;
        //        GridView1.DataBind();

        //        GridView1.DataSource = dtfill;
        //        GridView1.DataBind();
        //    }
        //}
    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/WizardCompanyWebsiteAddressMaster.aspx");
    }
    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzFAQMaster.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {

        if (Button1.Text == "Printable Version")
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillgrid();
           
            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[2].Visible = false;
            }
            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[3].Visible = false;
            }

        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            fillgrid();

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[2].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }

        }

    }
    protected void fillstore()
    {
        ddwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddwarehouse.DataSource = ds;
        ddwarehouse.DataTextField = "Name";
        ddwarehouse.DataValueField = "WareHouseId";
        ddwarehouse.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void fillstorefilter()
    {
        DropDownList1.Items.Clear();

        DataTable ds = ClsStore.SelectStorename();
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();


        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";




    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        string strrrr = "select  FAQCategoryMaster_Id,FAQCategoryName from FAQCategoryMaster " +
              " where whid='" + ddwarehouse.SelectedValue + "' and FAQCategoryName= '" + TextBox1.Text + "' and  FAQCategoryMaster_Id<>" + ViewState["Id"] + " ";

        SqlCommand cmd1 = new SqlCommand(strrrr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd1);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count == 0)
        {
            string str = "Update FAQCategoryMaster set Whid='" + ddwarehouse.SelectedValue.ToString() + "',FAQCategoryName ='" + TextBox1.Text + "' where FAQCategoryMaster_Id=" + ViewState["Id"] + "";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();

            }
            cmd.ExecuteNonQuery();
            con.Close();


            Label1.Visible = true;
            Label1.Text = "Record updated successfully";
            GridView1.EditIndex = -1;
            fillgrid();



            fillclear();

            ImageButton1.Visible = true;
            Button2.Visible = false;
            pnladd.Visible = false;
            lbllegend.Text = "Add a New Frequently Asked Question Category";
            lbllegend.Visible = false;
            btnadd.Visible = true;

        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }




    }
    protected void fillclear()
    {

        TextBox1.Text = "";
        ImageButton1.Visible = true;
        Button2.Visible = false;

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
        Label1.Text = "";
    }
}
