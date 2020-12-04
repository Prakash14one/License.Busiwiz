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

public partial class ShoppingCart_Admin_EmailTypeMaster : System.Web.UI.Page
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;
    string compid;
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

        //Session["pagename"] = "EmailTypeMaster.aspx";
        // Session["pnl1"] = "8";
        Session["pnlM"] = "1";
        Session["pnl1"] = "17";
        Label1.Visible = false;
        compid = Session["comid"].ToString();
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            fillwarehouse();
            ViewState["sortOrder"] = "";
            fillgrid();
        }
    }
    protected void fillgrid()
    {
        string strwh = "";

        if (ddshorting.SelectedIndex > 0)
        {
            lblCompany.Text = Session["Cname"].ToString();
            lblBusiness.Text = ddshorting.SelectedItem.Text;
            strwh = "SELECT WareHouseId,WareHouseMaster.Name as Whname,EmailTypeId,Whid,EmailTypeMaster.Name as ename FROM WareHouseMaster inner join EmailTypeMaster on EmailTypeMaster.Whid=WareHouseMaster.WareHouseId where EmailTypeMaster.compid = '" + Session["comid"].ToString() + "' and WarehouseMaster.Status='" + 1 + "' and  EmailTypeMaster.Whid='" + ddshorting.SelectedValue + "' order by WarehouseMaster.Name,EmailTypeMaster.Name ";
        }
        else
        {
            lblCompany.Text = Session["Cname"].ToString();
            lblBusiness.Text = "All";
            strwh = "SELECT WareHouseId,WareHouseMaster.Name as Whname,EmailTypeId,Whid,EmailTypeMaster.Name as ename FROM WareHouseMaster inner join EmailTypeMaster on EmailTypeMaster.Whid=WareHouseMaster.WareHouseId where EmailTypeMaster.compid = '" + Session["comid"].ToString() + "' and WarehouseMaster.Status='" + 1 + "'  order by WarehouseMaster.Name,EmailTypeMaster.Name ";
        }
        SqlCommand cmdwh = new SqlCommand(strwh, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);




        GridView1.DataSource = dtwh;
        DataView myDataView = new DataView();
        myDataView = dtwh.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }


        GridView1.DataBind();


    }
    protected void fillwarehouse()
    {
        //string str1 = "select * from WarehouseMaster where comid='" + Session["comid"] + "'and Status='" + 1 + "' order by name";

        //DataTable ds1 = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(str1, con);
        //da.Fill(ds1);
        //if (ds1.Rows.Count > 0)
        //{
        //    DropDownList1.DataSource = ds1;
        //    DropDownList1.DataTextField = "Name";
        //    DropDownList1.DataValueField = "WarehouseId";
        //    DropDownList1.DataBind();

        //    ddshorting.DataSource = ds1;
        //    ddshorting.DataTextField = "Name";
        //    ddshorting.DataValueField = "WarehouseId";
        //    ddshorting.DataBind();
        //    ddshorting.Items.Insert(0, "ALL");
        //    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText("Eplaza Store"));
        //}


        DropDownList1.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            DropDownList1.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        ddshorting.DataSource = ds;
        ddshorting.DataTextField = "Name";
        ddshorting.DataValueField = "WarehouseId";
        ddshorting.DataBind();
        ddshorting.Items.Insert(0, "All");
        ddshorting.Items[0].Value = "0";

    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
        string compid = Session["Comid"].ToString();
        string strwh = "SELECT * FROM EmailTypeMaster where  Name='" + Textname.Text + "' and Whid='" + DropDownList1.SelectedValue + "'";
        SqlCommand cmdwh = new SqlCommand(strwh, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);
        if (dtwh.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already used";
        }
        else
        {

            SqlCommand mycmd = new SqlCommand("Sp_Insert_EmailTypeMaster", con);
            mycmd.CommandType = CommandType.StoredProcedure;

            mycmd.Parameters.AddWithValue("@Name", Textname.Text);
            mycmd.Parameters.AddWithValue("@compid", compid);
            mycmd.Parameters.AddWithValue("@Whid", DropDownList1.SelectedValue);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }

            mycmd.ExecuteNonQuery();
            con.Close();
            Label1.Visible = true;
            Label1.Text = "Record inserted successfully";
            fillgrid();
            Textname.Text = "";
            addemail.Visible = false;
            btnadd.Visible = true;
            title.Visible = false;


            string strmax = "SELECT Max(EmailTypeId) as EmailTypeId FROM EmailTypeMaster ";
            SqlCommand cmdmax = new SqlCommand(strmax, con);
            SqlDataAdapter damax = new SqlDataAdapter(cmdmax);
            DataTable dtmax = new DataTable();
            damax.Fill(dtmax);
            if (dtmax.Rows.Count > 0)
            {
                if (CheckBox1.Checked == true)
                {
                    ViewState["EtypeID"] = dtmax.Rows[0]["EmailTypeId"].ToString();
                    string te = "EmailContentMaster.aspx?Id=" + ViewState["EtypeID"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


                }
            }
            CheckBox1.Checked = true;
        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        Label1.Visible = false;
        DropDownList1.SelectedIndex = 0;
        Textname.Text = "";
        addemail.Visible = false;
        btnadd.Visible = true;
        title.Visible = false;
        btnupdate.Visible = false;
        ImageButton1.Visible = true;
        CheckBox1.Visible = true;
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzEmailContentMaster.aspx");
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/WzFAQMaster.aspx");
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        ViewState["Id"] = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        ImageButton2_Click(sender, e);

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {


        }
        if (e.CommandName == "Edit")
        {
            ViewState["Id"] = Convert.ToInt32(e.CommandArgument);

            string strwh = "SELECT WareHouseId,WareHouseMaster.Name as Whname,EmailTypeId,Whid,EmailTypeMaster.Name as ename FROM WareHouseMaster inner join EmailTypeMaster on EmailTypeMaster.Whid=WareHouseMaster.WareHouseId where EmailTypeMaster.compid ='" + compid + "' and WarehouseMaster.Status=1   and EmailTypeMaster. EmailTypeId='" + ViewState["Id"] + "' order by WarehouseMaster.Name";
            SqlCommand cmdwh = new SqlCommand(strwh, con);
            SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            DataTable dtwh = new DataTable();
            adpwh.Fill(dtwh);

            Textname.Enabled = true;
            DropDownList1.Enabled = true;
            Textname.Text = dtwh.Rows[0]["ename"].ToString();
            fillwarehouse();
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dtwh.Rows[0]["Whname"].ToString()));
            btnupdate.Visible = true;
            addemail.Visible = true;
            btnadd.Visible = false;
            title.Visible = true;
            title.Text = "Edit Email Format Name";
            ImageButton1.Visible = false;
            CheckBox1.Visible = false;

        }
        //if (e.CommandName == "View")
        //{
        //    ViewState["Id"] = Convert.ToInt32(e.CommandArgument);
        //    string strwh = "SELECT WareHouseId,WareHouseMaster.Name as Whname,EmailTypeId,Whid,EmailTypeMaster.Name as ename FROM WareHouseMaster inner join EmailTypeMaster on EmailTypeMaster.Whid=WareHouseMaster.WareHouseId where EmailTypeMaster.compid ='" + compid + "' and WarehouseMaster.Status=1   and EmailTypeMaster. EmailTypeId='" + ViewState["Id"] + "' order by WarehouseMaster.Name";
        //    SqlCommand cmdwh = new SqlCommand(strwh, con);
        //    SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        //    DataTable dtwh = new DataTable();
        //    adpwh.Fill(dtwh);

        //    Textname.Text = dtwh.Rows[0]["ename"].ToString();
        //    Textname.Enabled = false;
        //    fillwarehouse();
        //    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dtwh.Rows[0]["Whname"].ToString()));
        //    DropDownList1.Enabled = false;
        //    addemail.Visible = true;
        //    btnadd.Visible = false;
        //    title.Visible = true;
        //    title.Text = "View Email Format Name";
        //    ImageButton1.Visible = false;
        //    //CheckBox1.Enabled = false;

           
        //}
        if (e.CommandName == "viewandmanage")
        {
           

            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "EmailContentMaster.aspx?ViewManageId=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void yes_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton6_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1222.Hide();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    { }
    //    ViewState["Id"] = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
    //    Label whmid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblwhid");

    //    DropDownList wh = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlwarehouse");

    //    TextBox ename = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox1");
    //    string strwh = "SELECT * FROM EmailTypeMaster where  Name='" + ename.Text + "' and Whid='" + wh.SelectedValue + "' and  EmailTypeId<> " + ViewState["Id"] + "";
    //    SqlCommand cmdwh = new SqlCommand(strwh, con);
    //    SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
    //    DataTable dtwh = new DataTable();
    //    adpwh.Fill(dtwh);
    //    if (dtwh.Rows.Count > 0)
    //    {
    //        Label1.Visible = true;
    //        Label1.Text = "Record already used";
    //    }
    //    else
    //    {

    //        SqlCommand cmd = new SqlCommand("Update  EmailTypeMaster Set Whid='" + wh.SelectedValue + "',Name='" + ename.Text + "' where EmailTypeId= " + ViewState["Id"] + "", con);
    //        if (con.State.ToString() != "Open")
    //        {
    //            con.Open();
    //        }
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //        Label1.Visible = true;
    //        Label1.Text = "Record updated successfully";

    //        GridView1.EditIndex = -1;
    //        fillgrid();
    //    }
    //}
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {



    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();
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
    protected void ddshorting_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["viewHide"] = "tt";
                GridView1.Columns[3].Visible = false;
            }
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
            //pnlgrid.Height = new Unit(100);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["viewHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }
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

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        //string st3 = "select * from EmailContentMaster where EmailTypeId='" + ViewState["Id"] + "'";
        //SqlCommand cmd3 = new SqlCommand(st3, con);
        //SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);
        //DataTable dt3 = new DataTable();
        //adp3.Fill(dt3);
        //if (dt3.Rows.Count > 0)
        //{
        //    Label1.Visible = true;
        //    Label1.Text = "Sorry,First delete records from Email Content to delete this record. ";

        //}
        //else
        //{

        SqlCommand cmd = new SqlCommand("delete from EmailTypeMaster where EmailTypeId= " + ViewState["Id"] + "", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        SqlCommand emailcontentcmd = new SqlCommand("delete from EmailContentMaster where EmailTypeId= " + ViewState["Id"] + "", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        emailcontentcmd.ExecuteNonQuery();
        con.Close();


        Label1.Visible = true;
        Label1.Text = "Record deleted successfully";

        GridView1.EditIndex = -1;
        fillgrid();
        //   }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Textname.Enabled = true;
        DropDownList1.Enabled = true;
        addemail.Visible = true;
        btnadd.Visible = false;
        Label1.Visible = false;
        title.Visible = true;
        title.Text = "Add Pre-Formatted Email Name";
        Label1.Text = "";
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string strwh = "SELECT * FROM EmailTypeMaster where  Name='" + Textname.Text + "' and Whid='" + DropDownList1.SelectedValue + "' and  EmailTypeId<> " + ViewState["Id"] + "";
        SqlCommand cmdwh = new SqlCommand(strwh, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);
        if (dtwh.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }
        else
        {

            SqlCommand cmd = new SqlCommand("Update  EmailTypeMaster Set Whid='" + DropDownList1.SelectedValue + "',Name='" + Textname.Text + "' where EmailTypeId= " + ViewState["Id"] + "", con);
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
            addemail.Visible = false;
            btnadd.Visible = true;
            title.Visible = false;
            btnupdate.Visible = false;
            ImageButton1.Visible = true;
            CheckBox1.Visible = true;
            Textname.Text = "";

            //if (CheckBox1.Checked == true)
            //{

            //    string te = "EmailContentMaster.aspx?Id=" + ViewState["Id"].ToString();
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


            //}




        }

    }
}
