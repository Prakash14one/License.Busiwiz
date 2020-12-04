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
public partial class ShoppingCart_Admin_FAQMaster : System.Web.UI.Page
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


        Page.Title = pg.getPageTitle(page);

        Label1.Visible = false;



        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            fillwarehouse();
            ddlwarehouse_SelectedIndexChanged(sender, e);
            fillwarehousefilter();

            ViewState["sortOrder"] = "";

            lblCompany.Text = Session["Cname"].ToString();


            fillGridView1();


        }
    }


    protected void FillDDlFAQcats()
    {

        string comiid = Session["Comid"].ToString();
        DropDownList1.Items.Clear();

        string sle23 = "SELECT FAQCategoryMaster.FAQCategoryMaster_Id,FAQCategoryMaster.FAQCategoryName as FAQCategoryName FROM FAQCategoryMaster inner join WareHouseMaster on FAQCategoryMaster.Whid=WareHouseMaster.WareHouseId where FAQCategoryMaster.Compid='" + comiid + "' and FAQCategoryMaster.Whid='" + ddlwarehouse.SelectedValue + "' order by FAQCategoryMaster.FAQCategoryName ";
        SqlCommand cmd23 = new SqlCommand(sle23, con);
        SqlDataAdapter adp23 = new SqlDataAdapter(cmd23);
        DataTable dt23 = new DataTable();
        adp23.Fill(dt23);
        if (dt23.Rows.Count > 0)
        {
            DropDownList1.Items.Clear();
            DropDownList1.DataSource = dt23;
            DropDownList1.DataTextField = "FAQCategoryName";
            DropDownList1.DataValueField = "FAQCategoryMaster_Id";
            DropDownList1.DataBind();


        }
        else
        {
            DropDownList1.DataSource = null;
            DropDownList1.DataTextField = "FAQCategoryName";
            DropDownList1.DataValueField = "FAQCategoryMaster_Id";
            DropDownList1.DataBind();

        }
    }

    protected void fillwarehouse()
    {

        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlwarehouse.DataSource = ds;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void fillGridView1()
    {
        //DataTable dd = new DataTable();
        //dd = (DataTable)fillgrid2();


        string comiid = Session["Comid"].ToString();
        //DataTable ds = new DataTable();

        string sle = "";
        lblFAQCAT.Text = "All";
        string st1 = "";
        string st2 = "";
        string st3 = "";
        lblBusiness.Text = "All";
        lblstatusprint.Text = ddlstatts.SelectedItem.Text;

        if (DropDownList2.SelectedIndex > 0)
        {
            st2 = " and FAQCategoryMaster.FAQCategoryMaster_Id='" + DropDownList2.SelectedValue + "' ";
            lblFAQCAT.Text = DropDownList2.SelectedItem.Text;
        }
        if (DropDownList3.SelectedIndex > 0)
        {
            lblBusiness.Text = DropDownList3.SelectedItem.Text;
            st1 = " and FAQCategoryMaster.Whid='" + DropDownList3.SelectedValue + "'";
        }
        if (ddlstatts.SelectedIndex > 0)
        {
            st3 = "and FAQMaster.Active='" + ddlstatts.SelectedValue + "'";
        }

        sle = " FAQMaster.FAQMaster_Id, FAQCategoryMaster.Whid,FAQMaster.FAQ,case when (FAQMaster.Active='1') then 'Active' else 'Inactive' End as Statuslabel, FAQMaster.Answer, FAQMaster.FAQSort,FAQCategoryMaster.Whid as WareHouseId, FAQMaster.Active,WareHouseMaster.Name as Wname,FAQCategoryMaster.FAQCategoryName as FAQCategoryName, " +
              "FAQCategoryMaster.FAQCategoryMaster_Id FROM FAQMaster LEFT OUTER JOIN FAQCategoryMaster ON FAQMaster.FAQCategory_Id = FAQCategoryMaster.FAQCategoryMaster_Id " +
              "inner join WareHouseMaster on WareHouseMaster.WareHouseId=FAQCategoryMaster.Whid where FAQCategoryMaster.Compid='" + comiid + "' " + st2 + "" + st1 + "" + st3 + "";

        string str2 = " select count(FAQMaster.FAQMaster_Id) as ci FROM FAQMaster LEFT OUTER JOIN FAQCategoryMaster ON FAQMaster.FAQCategory_Id = FAQCategoryMaster.FAQCategoryMaster_Id " +
              "inner join WareHouseMaster on WareHouseMaster.WareHouseId=FAQCategoryMaster.Whid where FAQCategoryMaster.Compid='" + comiid + "' " + st2 + "" + st1 + "" + st3 + "";

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,FAQCategoryMaster.FAQCategoryName,FAQMaster.FAQ,FAQMaster.Answer,FAQMaster.Active ";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dd = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, sle);

            GridView1.DataSource = dd;
            DataView myDataView = new DataView();
            myDataView = dd.DefaultView;

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


    public void clear()
    {

        DropDownList1.SelectedIndex = 0;
        txtAnswer.Text = "";
        txtfaq.Text = "";
        // RadioButton1.Checked = true;


    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }

        if (e.CommandName == "Edit")
        {


            pnladd.Visible = true;
            lbllegend.Visible = true;
            lbllegend.Text = "Edit Frequently Asked Question (FAQ)";


            Button3.Visible = true;
            ImageButton1.Visible = false;
            btnadd.Visible = false;



            int dk1 = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = dk1;

            string eeed = " select FAQMaster.*,FAQCategoryMaster.Whid from FAQMaster inner join FAQCategoryMaster on FAQCategoryMaster.FAQCategoryMaster_Id=FAQMaster.FAQCategory_Id where FAQMaster.FAQMaster_Id='" + dk1 + "' ";
            SqlCommand cmdeeed = new SqlCommand(eeed, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);

            fillwarehouse();
            ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["Whid"]).ToString()));

            FillDDlFAQcats();
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["FAQCategory_Id"]).ToString()));

            txtfaq.Text = dteeed.Rows[0]["FAQ"].ToString();
            txtAnswer.Text = dteeed.Rows[0]["Answer"].ToString();
            if (Convert.ToBoolean(dteeed.Rows[0]["Active"].ToString()) == true)
            {
                RadioButton1.SelectedValue = "1";
            }
            else
            {
                RadioButton1.SelectedValue = "0";
            }




        }
        if (e.CommandName == "Delete")
        {
            ViewState["fid"] = Convert.ToInt32(e.CommandArgument);


            if (ViewState["fid"] == null || ViewState["fid"].ToString() == "")
            {

            }
            else
            {
                SqlCommand cmd = new SqlCommand("delete from FAQMaster where FAQMaster_Id= '" + Convert.ToInt32(ViewState["fid"]) + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }

                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record deleted successfully";
                fillGridView1();
                DropDownList1.Items.Clear();
                FillDDlFAQcats();
            }





        }
    }


    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {


    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


    }
    //public DataTable fillgrid2()
    //{
    //    string comiid = Session["Comid"].ToString();
    //    DataTable ds = new DataTable();

    //    string sle = "";
    //    lblFAQCAT.Text = "All";
    //    string st1 = "";
    //    string st2 = "";
    //    string st3 = "";
    //    lblBusiness.Text = "All";
    //    lblstatusprint.Text = ddlstatts.SelectedItem.Text;

    //    sle = "SELECT FAQMaster.FAQMaster_Id, FAQCategoryMaster.Whid,FAQMaster.FAQ,case when (FAQMaster.Active='1') then 'Active' else 'Inactive' End as Statuslabel, FAQMaster.Answer, FAQMaster.FAQSort,FAQCategoryMaster.Whid as WareHouseId, FAQMaster.Active,WareHouseMaster.Name as Wname,FAQCategoryMaster.FAQCategoryName as FAQCategoryName, " +
    //             " FAQCategoryMaster.FAQCategoryMaster_Id FROM FAQMaster LEFT OUTER JOIN " +
    //             " FAQCategoryMaster ON FAQMaster.FAQCategory_Id = FAQCategoryMaster.FAQCategoryMaster_Id  inner join WareHouseMaster on WareHouseMaster.WareHouseId=FAQCategoryMaster.Whid  where FAQCategoryMaster.Compid='" + comiid + "'";

    //    if (DropDownList2.SelectedIndex > 0)
    //    {
    //        st2 = " and FAQCategoryMaster.FAQCategoryMaster_Id='" + DropDownList2.SelectedValue + "' ";
    //        lblFAQCAT.Text = DropDownList2.SelectedItem.Text;
    //    }
    //    if (DropDownList3.SelectedIndex > 0)
    //    {
    //        lblBusiness.Text = DropDownList3.SelectedItem.Text;
    //        st1 = " and FAQCategoryMaster.Whid='" + DropDownList3.SelectedValue + "'";
    //    }
    //    if (ddlstatts.SelectedIndex > 0)
    //    {
    //        st3 = "and FAQMaster.Active='" + ddlstatts.SelectedValue + "'";
    //    }

    //    string sortorder = "order by WareHouseMaster.Name,FAQCategoryMaster.FAQCategoryName,FAQMaster.FAQ,FAQMaster.Answer,FAQMaster.Active ";

    //    sle = sle + st1 + st2 + st3 + sortorder;
    //    SqlCommand cmd = new SqlCommand(sle, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    adp.Fill(ds);
    //    return ds;
    //}

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //if (DropDownList2.SelectedIndex == 0)
        //{
        //    args.IsValid = false;
        //    n = 1;

        //}
        //else
        //{
        //    args.IsValid = true;
        //    n = 0;




        //}
    }
    protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //if (DropDownList1.SelectedIndex == 0)
        //{
        //    args.IsValid = false;
        //    n = 1;

        //}
        //else
        //{
        //    args.IsValid = true;
        //    n = 0;




        //}
    }
    protected void Button1_Click(object sender, EventArgs e)
    {



        try
        {
            string str11 = "SELECT * FROM FAQMaster  where FAQ='" + txtfaq.Text + "' and  FAQCategory_Id='" + DropDownList1.SelectedValue + "'";
            SqlCommand cmd11 = new SqlCommand(str11, con);
            SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
            DataTable dt11 = new DataTable();
            adp11.Fill(dt11);
            if (dt11.Rows.Count > 0)
            {
                Label1.Visible = true;
                Label1.Text = "Record already exist";
            }
            else
            {
                String str = " SELECT max(FAQSort) FROM  FAQMaster";
                SqlCommand cmd2 = new SqlCommand(str, con);
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                DataTable ds2 = new DataTable();
                adp2.Fill(ds2);

                ViewState["Id"] = ds2.Rows[0][0].ToString();
                if (ViewState["Id"].ToString() == "")
                {
                    ViewState["Id"] = 0;

                }
                ViewState["Id2"] = Convert.ToInt32(ViewState["Id"]) + 1;

                SqlCommand cmd = new SqlCommand("Sp_Insert_FAQMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FAQCategory_Id", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@FAQ", txtfaq.Text);
                cmd.Parameters.AddWithValue("@Answer", txtAnswer.Text);
                cmd.Parameters.AddWithValue("@FAQSort", ViewState["Id2"]);
                if (RadioButton1.SelectedValue == "1")
                {
                    cmd.Parameters.AddWithValue("@Active", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Active", 0);
                }
                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }

                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record inserted successfully";
                clear();
                fillGridView1();

                ImageButton1.Visible = true;
                Button3.Visible = false;
                pnladd.Visible = false;
                lbllegend.Text = "Add a Frequently Asked Question (FAQ)";
                lbllegend.Visible = false;
                btnadd.Visible = true;
                RadioButton1.SelectedIndex = 0;
            }
        }

        catch (Exception)
        {
            Label1.Visible = true;
            Label1.Text = "Error";
        }
        finally { }
        //    }

        //}
    }


    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        //    hdnsortExp.Value = e.SortExpression.ToString();
        //    hdnsortDir.Value = sortOrder;

        //    DataTable dd = new DataTable();
        //    dd = (DataTable)fillgrid2();
        //    GridView1.DataSource = dd;

        //    DataView myDataView = new DataView();
        //    myDataView = dd.DefaultView;

        //    if (hdnsortExp.Value != string.Empty)
        //    {
        //        myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //    }
        //    //GridView1.DataSource = myDataView;
        //    GridView1.DataBind();
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
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGridView1();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillGridView1();

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        DropDownList ddlBusiness = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlBusiness");
        DropDownList ddlgrdct = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlgrdcat");
        TextBox txtq = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtQue");
        TextBox txta = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtAns");
        CheckBox act = (CheckBox)GridView1.Rows[GridView1.EditIndex].FindControl("chkActive");
        Label FaqMid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblFAQMid");
        Label lblwhid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblwhid");


        string str11 = "SELECT * FROM FAQMaster inner join  FAQCategoryMaster on FAQCategoryMaster.FAQCategoryMaster_Id=FAQMaster.FAQCategory_Id inner join WareHouseMaster on FAQCategoryMaster.Whid=WareHouseMaster.WareHouseId where FAQ='" + txtq.Text + "' and  FAQCategory_Id='" + ddlgrdct.SelectedValue + "' and FAQCategoryMaster.Whid='" + ddlBusiness.SelectedValue + "' and FAQMaster_Id<>'" + FaqMid.Text + "'";
        SqlCommand cmd11 = new SqlCommand(str11, con);
        SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
        DataTable dt11 = new DataTable();
        adp11.Fill(dt11);
        if (dt11.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already used";
        }
        else
        {


            SqlCommand cmd = new SqlCommand("Sp_Update_FAQMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FAQCategory_Id", ddlgrdct.SelectedValue);
            cmd.Parameters.AddWithValue("@FAQ", txtq.Text);
            cmd.Parameters.AddWithValue("@Answer", txta.Text);
            //cmd.Parameters.AddWithValue("@FAQSort", ViewState["Id2"]);
            cmd.Parameters.AddWithValue("@Active", act.Checked);


            cmd.Parameters.AddWithValue("@FAQMaster_Id", FaqMid.Text);
            if (con.State.ToString() != "Open")
            {
                con.Open();

            }

            cmd.ExecuteNonQuery();
            con.Close();
            Label1.Visible = true;
            Label1.Text = "Record updated successfully";

            GridView1.EditIndex = -1;
            fillGridView1();

        }



    }




    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //GridView1.PageIndex = e.NewSelectedIndex;
        //fillGridView1();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillGridView1();
    }

    protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillGridView1();
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillDDlFAQcats();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfiltercat();

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clear();
        Label1.Text = "";

        ImageButton1.Visible = true;
        Button3.Visible = false;
        pnladd.Visible = false;
        lbllegend.Text = "Add a Frequently Asked Question (FAQ)";
        lbllegend.Visible = false;
        btnadd.Visible = true;
        RadioButton1.SelectedIndex = 0;
    }
    protected void fillfiltercat()
    {
        string comiid = Session["Comid"].ToString();

        DropDownList2.Items.Clear();

        if (DropDownList3.SelectedIndex > 0)
        {


            string sle231 = "SELECT FAQCategoryMaster.FAQCategoryMaster_Id,FAQCategoryMaster.FAQCategoryName as FAQCategoryName FROM FAQCategoryMaster inner join WareHouseMaster on FAQCategoryMaster.Whid=WareHouseMaster.WareHouseId where FAQCategoryMaster.Compid='" + comiid + "' and WareHouseMaster.WareHouseId='" + DropDownList3.SelectedValue + "' order by FAQCategoryMaster.FAQCategoryName";
            SqlCommand cmd231 = new SqlCommand(sle231, con);
            SqlDataAdapter adp231 = new SqlDataAdapter(cmd231);
            DataTable dt231 = new DataTable();
            adp231.Fill(dt231);
            if (dt231.Rows.Count > 0)
            {



                DropDownList2.DataSource = dt231;
                DropDownList2.DataTextField = "FAQCategoryName";
                DropDownList2.DataValueField = "FAQCategoryMaster_Id";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, "All");
                DropDownList2.Items[0].Value = "0";


            }
            else
            {
                DropDownList2.DataSource = null;
                DropDownList2.DataTextField = "FAQCategoryName";
                DropDownList2.DataValueField = "FAQCategoryMaster_Id";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, "All");
                DropDownList2.Items[0].Value = "0";

            }
        }
        else
        {

            DropDownList2.DataSource = null;
            DropDownList2.DataTextField = "FAQCategoryName";
            DropDownList2.DataValueField = "FAQCategoryMaster_Id";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "All");
            DropDownList2.Items[0].Value = "0";
        }
        fillGridView1();

    }

    protected void Button2_Click1(object sender, EventArgs e)
    {

        if (Button2.Text == "Printable Version")
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillGridView1();

            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }

        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            fillGridView1();

            Button2.Text = "Printable Version";
            Button1.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[6].Visible = true;
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
        Label1.Text = "";
    }
    protected void fillwarehousefilter()
    {

        DropDownList3.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "Name";
        DropDownList3.DataValueField = "WareHouseId";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, "All");
        DropDownList3.Items[0].Value = "0";

        fillfiltercat();


    }
    protected void Btnupdate_Click(object sender, EventArgs e)
    {


        string str11 = "SELECT * FROM FAQMaster inner join  FAQCategoryMaster on FAQCategoryMaster.FAQCategoryMaster_Id=FAQMaster.FAQCategory_Id inner join WareHouseMaster on FAQCategoryMaster.Whid=WareHouseMaster.WareHouseId where FAQ='" + txtfaq.Text + "' and  FAQCategory_Id='" + DropDownList1.SelectedValue + "' and FAQCategoryMaster.Whid='" + ddlwarehouse.SelectedValue + "' and FAQMaster_Id<>'" + ViewState["Id"] + "'";
        SqlCommand cmd11 = new SqlCommand(str11, con);
        SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
        DataTable dt11 = new DataTable();
        adp11.Fill(dt11);
        if (dt11.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exist";
        }
        else
        {


            SqlCommand cmd = new SqlCommand("Sp_Update_FAQMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FAQCategory_Id", DropDownList1.SelectedValue);
            cmd.Parameters.AddWithValue("@FAQ", txtfaq.Text);
            cmd.Parameters.AddWithValue("@Answer", txtAnswer.Text);
            //cmd.Parameters.AddWithValue("@FAQSort", ViewState["Id2"]);
            cmd.Parameters.AddWithValue("@Active", RadioButton1.SelectedValue);


            cmd.Parameters.AddWithValue("@FAQMaster_Id", ViewState["Id"]);
            if (con.State.ToString() != "Open")
            {
                con.Open();

            }

            cmd.ExecuteNonQuery();
            con.Close();
            Label1.Visible = true;
            Label1.Text = "Record updated successfully";

            GridView1.EditIndex = -1;
            fillGridView1();

            ImageButton1.Visible = true;
            Button3.Visible = false;
            pnladd.Visible = false;
            lbllegend.Text = "Add a Frequently Asked Question (FAQ)";
            lbllegend.Visible = false;
            btnadd.Visible = true;
            clear();
            RadioButton1.SelectedIndex = 0;

        }




    }
    protected void imgaddcatt_Click(object sender, ImageClickEventArgs e)
    {
        string te = "FaqCategoryMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgcattrefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillDDlFAQcats();
    }
    protected void ddlstatts_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGridView1();
    }
}
