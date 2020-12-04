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

public partial class Add_WareHouse_TimeZone_Master : System.Web.UI.Page
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
        ViewState["Compid"] = Session["Comid"].ToString();
        ViewState["UserName"] = Session["userid"].ToString();
        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            lblCompany.Text = Session["Cname"].ToString();

            lblmsg.Text = "";

            loadupdate();


            fillgrid();
            ModalPopupExtender1222.Hide();

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
    public void fillgrid()
    {
        lblCompany.Text = Session["Cname"].ToString();

        string str = "";
        string str2 = "";
        if (DropDownList3.SelectedIndex > 0)
        {
            str = "  WHTimeZone.ID,WHTimeZone.WHID,WareHouseMaster.Name,TimeZoneMaster.Name +':'+ TimeZoneMaster.ShortName+':'+ TimeZoneMaster.gmt as tname from WHTimeZone inner join WareHouseMaster on WareHouseMaster.WareHouseId=WHTimeZone.WHID inner join TimeZoneMaster on TimeZoneMaster.ID=WHTimeZone.TimeZone where [WareHouseMaster].Status='1' and compid='" + Session["Comid"].ToString() + "' and WHTimeZone.WHID='" + DropDownList3.SelectedValue + "'";
            //order by Name, tname";  

            str2 = " select count(WHTimeZone.ID) as ci from WHTimeZone inner join WareHouseMaster on WareHouseMaster.WareHouseId=WHTimeZone.WHID inner join TimeZoneMaster on TimeZoneMaster.ID=WHTimeZone.TimeZone where [WareHouseMaster].Status='1' and compid='" + Session["Comid"].ToString() + "' and WHTimeZone.WHID='" + DropDownList3.SelectedValue + "'";
        }
        else
        {
            str = "  WHTimeZone.ID,WHTimeZone.WHID,WareHouseMaster.Name,TimeZoneMaster.Name +':'+ TimeZoneMaster.ShortName+':'+ TimeZoneMaster.gmt as tname from WHTimeZone inner join WareHouseMaster on WareHouseMaster.WareHouseId=WHTimeZone.WHID inner join TimeZoneMaster on TimeZoneMaster.ID=WHTimeZone.TimeZone where [WareHouseMaster].Status='1' and compid='" + Session["Comid"].ToString() + "'";

            str2 = " select count(WHTimeZone.ID) as ci from WHTimeZone inner join WareHouseMaster on WareHouseMaster.WareHouseId=WHTimeZone.WHID inner join TimeZoneMaster on TimeZoneMaster.ID=WHTimeZone.TimeZone where [WareHouseMaster].Status='1' and compid='" + Session["Comid"].ToString() + "'";
        }
        lblCompany0.Text = DropDownList3.SelectedItem.Text;

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,TimeZoneMaster.Name,TimeZoneMaster.ShortName,TimeZoneMaster.gmt asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable ds = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str);

            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataSource = myDataView;
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

    protected void LinkButton4_Click(object sender, ImageClickEventArgs e)
    {

        lblmsg.Text = "";
        pnladd.Visible = true;
        lbladd.Text = "Manage Time Zone";
        Button1.Visible = true;
        //ImageButton2.Visible = false;
        ImageButton lk = (ImageButton)sender;
        //LinkButton lk = (LinkButton)sender;
        int j = Convert.ToInt32(lk.CommandArgument);
        ViewState["Id"] = j;
        Session["id"] = j;


        string str1 = " select WHTimeZone.ID,WHTimeZone.WHID,WHTimeZone.TimeZone,WareHouseMaster.Name,TimeZoneMaster.Name as tname from WHTimeZone inner join WareHouseMaster on WareHouseMaster.WareHouseId=WHTimeZone.WHID inner join TimeZoneMaster on TimeZoneMaster.ID=WHTimeZone.TimeZone Where WHTimeZone.ID='" + j + "' ";
        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        adp.Fill(dt);

        string s1 = "SELECT ID,[Name]+':'+ShortName+':'+gmt AS TimeZone FROM [TimeZoneMaster] where  TimeZoneMaster.Status ='1'";
        SqlCommand cmd1 = new SqlCommand(s1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

        fillstore();
        DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dt.Rows[0]["WHID"].ToString()));

        //DropDownList1.SelectedValue = dt.Rows[0]["WHID"].ToString();
        Session["Storeid"] = Convert.ToInt32(dt.Rows[0]["WHID"].ToString());
        ddlselectchange();



        int TimeZoneid = Convert.ToInt32(dt.Rows[0]["TimeZone"].ToString());

        DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(dt.Rows[0]["TimeZone"].ToString()));

        //DropDownList2.SelectedValue = TimeZoneid.ToString();
        Session["Timezoneid"] = DropDownList2.SelectedValue;
        //DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue( dt1.Rows[0]["ID"].ToString()));
        //SELECT distinct ID,[Name]+':'+ShortName+':'+gmt AS NAME FROM [TimeZoneMaster] where gmt is not null

    }

    protected void ddlselectchange()
    {

        string str1 = "SELECT ID,[Name]+':'+ShortName+':'+gmt AS TimeZone FROM [TimeZoneMaster] where  TimeZoneMaster.Status ='1' order by  Name";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        cmd1.Connection = con;
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.SelectCommand = cmd1;
        DataTable ds1 = new DataTable();
        da1.Fill(ds1);

        DropDownList2.DataSource = ds1;
        DropDownList2.DataTextField = "TimeZone";
        DropDownList2.DataValueField = "ID";
        DropDownList2.DataBind();

        //DropDownList2.Items.Insert(0, "--Select--");
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView1.EditIndex = e.NewEditIndex;
        //fillgrid();

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string s1;
        string st1 = "select * from BatchMaster where BatchTimeZone='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd11 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd11);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You are not allowed to delete this record, first delete child record";
        }
        else
        {
            s1 = "Delete from WHTimeZone where ID = " + GridView1.DataKeys[e.RowIndex].Value.ToString();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }

            SqlCommand cmd1 = new SqlCommand(s1, con);
            cmd1.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            GridView1.DataBind();
            fillgrid();
            // cleartext();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";

        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }

    protected void Fill_timezone()
    {

        //string str1 = " select ID,Name+':'+ShortName+':'+gmt as Name from TimeZoneMaster order by Name";
        //string str1 = "SELECT ID,[Name] FROM [TimeZoneMaster]";
        string str1 = "SELECT ID,[Name]+':'+ShortName+':'+gmt AS TimeZone FROM [TimeZoneMaster] where TimeZoneMaster.Status ='1' ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        cmd1.Connection = con;
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.SelectCommand = cmd1;
        DataTable ds1 = new DataTable();
        da1.Fill(ds1);

        DropDownList2.DataSource = ds1;
        DropDownList2.DataTextField = "TimeZone";
        DropDownList2.DataValueField = "ID";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "-Select-");
        //DropDownList2.DataBind();
        //string str2 = "insert into WHTimeZone(TimeZone)values('"+DropDownList2.SelectedValue+"') ";
        //SqlCommand cmd2 = new SqlCommand(str2,con);
        //cmd2.Connection = con;
        //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);

        //DataSet ds2 = new DataSet();
        //da2.Fill(ds2);
        //con.Open();
        //cmd1.ExecuteNonQuery();
        //con.Close();

    }


    //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void cleartext()
    {
        lblmsg.Text = "";
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
    }

    //protected void Cancel_Click(object sender, ImageClickEventArgs e)
    //{

    //}

    protected void filltimezone()
    {
        string str1 = "SELECT ID,[Name]+':'+ShortName+':'+gmt AS TimeZone FROM [TimeZoneMaster] where  TimeZoneMaster.Status ='1'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        cmd1.Connection = con;
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.SelectCommand = cmd1;
        DataTable ds1 = new DataTable();
        da1.Fill(ds1);

        DropDownList2.DataSource = ds1;
        DropDownList2.DataTextField = "TimeZone";
        DropDownList2.DataValueField = "ID";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "--Select--");

    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {

        //string st1 = "select * from [WHTimeZone] Where WHTimeZone.WHID ='" + DropDownList1.SelectedValue + "' and ID != '" + ViewState["Id"] + "' ";//d WHTimeZone.TimeZone='" + DropDownList2.SelectedValue + "'";
        string st1 = "select * from [WHTimeZone] Where WHTimeZone.WHID ='" + DropDownList1.SelectedValue + "'  ";//d WHTimeZone.TimeZone='" + DropDownList2.SelectedValue + "'";
        // [WHTimeZone][WHID]
        SqlCommand cmd = new SqlCommand(st1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Text = "Record already exist";
            return;
        }

        string strrrr = "insert into WHTimeZone(WHID,TimeZone,compid)values('" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + Session["Comid"].ToString() + "')";


        SqlCommand cmd1 = new SqlCommand(strrrr, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds = new DataSet();
        adp1.Fill(ds);

        con.Close();

        fillgrid();

        cleartext();
        lblmsg.Text = "Record inserted successfully";


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1222.Show();
        //if (ViewState["Id"] != null)
        //{
        //    string st1 = "select * from [WHTimeZone] Where WHTimeZone.WHID ='" + DropDownList1.SelectedValue + "'";
        //    //and ID != '" + ViewState["Id"] + "' ";

        //    SqlCommand cmd = new SqlCommand(st1, con);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    adp.Fill(dt);
        //    if (dt.Rows.Count > 0)
        //    {
        //        string st2 = "select * from Batchmaster Where batchmaster.BatchTimeZone ='" + dt.Rows[0]["ID"].ToString() + "'";
        //        SqlCommand cmd2 = new SqlCommand(st2, con);
        //        SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
        //        DataTable dt2 = new DataTable();
        //        adp2.Fill(dt2);
        //        if (dt2.Rows.Count > 0)
        //        {
        //            lblmsg.Text = "Sorry,you can't set another Time Zone";
        //            return;
        //        }
        //        else
        //        {

        //            string str1 = "Update  WHTimeZone SET WHID='" + DropDownList1.SelectedValue + "', TimeZone='" + DropDownList2.SelectedValue + "' WHERE ID=" + ViewState["Id"] + "";
        //            SqlCommand cmd1 = new SqlCommand(str1, con);
        //            if (con.State.ToString() != "Open")
        //            {
        //                con.Open();
        //            }
        //            cmd1.ExecuteNonQuery();
        //            con.Close();
        //            //SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        //            //DataTable dt1 = new DataTable();
        //            //adp1.Fill(dt1);
        //            fillgrid();
        //            cleartext();
        //            lblmsg.Text = "Record updated successfully";
        //            //ImageButton2.Visible = true;
        //            //Button1.Visible = false;
        //            pnladd.Visible = false;
        //            lbladd.Text = "";

        //        }   
        //    }

        //}
        //else
        //{
        //    string str1 = "Update  WHTimeZone SET WHID='" + DropDownList1.SelectedValue + "', TimeZone='" + DropDownList2.SelectedValue + "' WHERE ID=" + Convert.ToInt16(Session["ID1"]) + "";
        //    SqlCommand cmd1 = new SqlCommand(str1, con);
        //    if (con.State.ToString() != "Open")
        //    {
        //        con.Open();
        //    }
        //    cmd1.ExecuteNonQuery();
        //    con.Close();
        //    //SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        //    //DataTable dt1 = new DataTable();
        //    //adp1.Fill(dt1);
        //    fillgrid();
        //    cleartext();
        //    lblmsg.Text = "Record updated successfully";
        //    //ImageButton2.Visible = true;
        //    //Button1.Visible = false;
        //    pnladd.Visible = false;
        //    lbladd.Text = "";
        //}
        //// Label masterkey = (Label) GridView1.FindControl("lbbname");

    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        //ImageButton2.Visible = true;
        //Button1.Visible = false;
        pnladd.Visible = false;
        lbladd.Text = "";
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

        fillgrid();
    }
    protected void fillstore()
    {

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

        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "Name";
        DropDownList3.DataValueField = "WareHouseId";
        DropDownList3.DataBind();

        DropDownList3.Items.Insert(0, "All");
        DropDownList3.Items[0].Value = "0";

    }

    protected void loadupdate()
    {
        int i = 0;
        fillstore();
        ddlselectchange();

        string bus = "select top(1) * from WHTimeZone where compid = '" + ViewState["Compid"] + "'";
        SqlCommand cmdbus = new SqlCommand(bus, con);
        SqlDataAdapter adpbus = new SqlDataAdapter(cmdbus);
        DataTable dtbus = new DataTable();
        adpbus.Fill(dtbus);
        if (dtbus.Rows.Count > 0)
        {
            i = Convert.ToInt16(dtbus.Rows[0]["ID"]);
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dtbus.Rows[0]["WHID"].ToString()));
            DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(dtbus.Rows[0]["TimeZone"].ToString()));
            Session["ID1"] = i;
        }
        Button1.Visible = true;
        ImageButton2.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ViewState["Id"] != null)
        {
            string st1 = "select * from [WHTimeZone] Where WHTimeZone.WHID ='" + DropDownList1.SelectedValue + "'";
            //and ID != '" + ViewState["Id"] + "' ";

            SqlCommand cmd = new SqlCommand(st1, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //string st2 = "select * from Batchmaster Where batchmaster.BatchTimeZone ='" + dt.Rows[0]["ID"].ToString() + "'";
                //SqlCommand cmd2 = new SqlCommand(st2, con);
                //SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                //DataTable dt2 = new DataTable();
                //adp2.Fill(dt2);
                //if (dt2.Rows.Count > 0)
                //{
                //    lblmsg.Text = "Sorry,you can't set another Time Zone";
                //    return;
                //}
                //else
                //{

                string str1 = "Update  WHTimeZone SET WHID='" + DropDownList1.SelectedValue + "', TimeZone='" + DropDownList2.SelectedValue + "' WHERE ID=" + ViewState["Id"] + "";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();
                //SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                //DataTable dt1 = new DataTable();
                //adp1.Fill(dt1);
                fillgrid();
                cleartext();
                lblmsg.Text = "Record updated successfully";
                //ImageButton2.Visible = true;
                //Button1.Visible = false;
                pnladd.Visible = false;
                lbladd.Text = "";
                ModalPopupExtender1222.Hide();
                //}
            }

        }
        else
        {
            string str1 = "Update  WHTimeZone SET WHID='" + DropDownList1.SelectedValue + "', TimeZone='" + DropDownList2.SelectedValue + "' WHERE ID=" + Convert.ToInt16(Session["ID1"]) + "";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();
            //SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            //DataTable dt1 = new DataTable();
            //adp1.Fill(dt1);
            fillgrid();
            cleartext();
            lblmsg.Text = "Record updated successfully";
            ModalPopupExtender1222.Hide();
            //ImageButton2.Visible = true;
            //Button1.Visible = false;
            pnladd.Visible = false;
            lbladd.Text = "";
        }
        // Label masterkey = (Label) GridView1.FindControl("lbbname");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1222.Hide();
        cleartext();
        pnladd.Visible = false;
        lbladd.Text = "";
        lblmsg.Text = "";
    }

    protected void Button_Click(object sender, EventArgs e)
    {
        if (Button.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillgrid();

            Button.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
        }
        else
        {
            // pnlgrid.ScrollBars = ScrollBars.Vertical;
            // pnlgrid.Height = new Unit(150);

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillgrid();

            Button.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
}
