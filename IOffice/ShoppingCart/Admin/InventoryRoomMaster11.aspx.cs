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

public partial class ShoppingCart_Admin_InventoryRoomMaster11 : System.Web.UI.Page
{
    string compid;
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
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

        compid = Session["Comid"].ToString();

        Page.Title = pg.getPageTitle(page);

        Session["pnlM"] = "4";
        Session["pnl4"] = "42";

        statuslable.Text = "";
        statuslable.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            lblCompany.Text = Session["Cname"].ToString();
            ViewState["sortOrder"] = "";
            lblCompany.Text = Session["Cname"].ToString();


            fillstore();

            fillstorefilter();
            FillDDlSite1();

            FillGridView1();
            ddlinvsiteid.Items.Insert(0, "All");
            ddlinvsiteid.Items[0].Value = "0";



        }
    }

    protected void FillDDlSite()
    {
        ddlinvsiteid.Items.Clear();
        if (ddlwarehouse.SelectedIndex > 0)
        {

            string strmain3 = "   SELECT     InventorySiteID, InventorySiteName   FROM         InventorySiteMasterTbl where WarehouseID='" + ddlwarehouse.SelectedValue + "' order by InventorySiteName ";

            SqlCommand cmd35 = new SqlCommand(strmain3, con);
            SqlDataAdapter adp35 = new SqlDataAdapter(cmd35);
            DataTable dth35 = new DataTable();
            adp35.Fill(dth35);
            if (dth35.Rows.Count > 0)
            {
                ddlinvsiteid.DataSource = dth35;
                ddlinvsiteid.DataTextField = "InventorySiteName";
                ddlinvsiteid.DataValueField = "InventorySiteID";

                ddlinvsiteid.DataBind();
                ddlinvsiteid.Items.Insert(0, "All");
                ddlinvsiteid.Items[0].Value = "0";
            }
            else
            {

                ddlinvsiteid.Items.Insert(0, "All");
                ddlinvsiteid.Items[0].Value = "0";
            }
        }
        else
        {
            ddlinvsiteid.Items.Insert(0, "All");
            ddlinvsiteid.Items[0].Value = "0";
        }
    }
    protected void FillDDlSite1()
    {
        ddlinvsiteid0.Items.Clear();
        if (ddlwarehouse0.SelectedIndex > -1)
        {

            string strmain3 = "   SELECT     InventorySiteID, InventorySiteName   FROM         InventorySiteMasterTbl where WarehouseID='" + ddlwarehouse0.SelectedValue + "' order by InventorySiteName ";

            SqlCommand cmd35 = new SqlCommand(strmain3, con);
            SqlDataAdapter adp35 = new SqlDataAdapter(cmd35);
            DataTable dth35 = new DataTable();
            adp35.Fill(dth35);
            if (dth35.Rows.Count > 0)
            {
                ddlinvsiteid0.DataSource = dth35;
                ddlinvsiteid0.DataTextField = "InventorySiteName";
                ddlinvsiteid0.DataValueField = "InventorySiteID";

                ddlinvsiteid0.DataBind();
                ddlinvsiteid0.Items.Insert(0, "-Select-");
                ddlinvsiteid0.Items[0].Value = "0";
            }
            else
            {

                ddlinvsiteid0.Items.Insert(0, "-Select-");
                ddlinvsiteid0.Items[0].Value = "0";
            }
        }
    }
    protected void FillGridView1()
    {
        string main = "";
        lblCompany.Text = Session["Cname"].ToString();
        if (ddlwarehouse.SelectedIndex > 0)
        {
            lblBusiness.Text = ddlwarehouse.SelectedItem.Text;
        }
        else
        {
            lblBusiness.Text = "All";
        }
        lblSite.Text = "All";
        Label10.Text = "All";

        string str2 = "";

        if (ddlwarehouse.SelectedIndex <= 0)
        {

            main = " InventoryRoomMasterTbl.InventoryRoomName,WareHouseMaster.Name + ':' + InventorySiteMasterTbl.InventorySiteName as InventorySiteName, " +
                      " InventoryRoomMasterTbl.InventoryRoomID, InventorySiteMasterTbl.InventorySiteID, InventorySiteMasterTbl.WarehouseID " +
                      " FROM InventorySiteMasterTbl INNER JOIN " +
                      " InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID LEFT OUTER JOIN " +
                      " WareHouseMaster ON InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId where WareHouseMaster.comid='" + compid + "' and  [WareHouseMaster].status = '1'";
            //order by WareHouseMaster.Name,InventorySiteMasterTbl.InventorySiteName,InventoryRoomMasterTbl.InventoryRoomName ";

            str2 = "select count(InventoryRoomMasterTbl.InventoryRoomID) as ci " +
                    " FROM InventorySiteMasterTbl INNER JOIN " +
                    " InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID LEFT OUTER JOIN " +
                    " WareHouseMaster ON InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId where WareHouseMaster.comid='" + compid + "' and  [WareHouseMaster].status = '1'";
        }

        else if (ddlwarehouse.SelectedIndex > 0)
        {
            if (ddlinvsiteid.SelectedIndex > 0)
            {
                lblSite.Text = ddlinvsiteid.SelectedItem.Text;
                Label10.Text = ddlinvsiteid.SelectedItem.Text;

                main = " InventoryRoomMasterTbl.InventoryRoomName,WareHouseMaster.Name + ':' + InventorySiteMasterTbl.InventorySiteName as InventorySiteName, " +
                          " InventoryRoomMasterTbl.InventoryRoomID, InventorySiteMasterTbl.InventorySiteID, InventorySiteMasterTbl.WarehouseID " +
                          " FROM InventorySiteMasterTbl INNER JOIN " +
                          " InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID LEFT OUTER JOIN " +
                          " WareHouseMaster ON InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId" +
                          " where InventorySiteMasterTbl.InventorySiteID='" + ddlinvsiteid.SelectedValue + "' and InventorySiteMasterTbl.WarehouseID ='" + ddlwarehouse.SelectedValue + "' and  [WareHouseMaster].status = '1'";

                str2 = "select count(InventoryRoomMasterTbl.InventoryRoomID) as ci " +
                        " FROM InventorySiteMasterTbl INNER JOIN " +
                          " InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID LEFT OUTER JOIN " +
                          " WareHouseMaster ON InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId" +
                          " where InventorySiteMasterTbl.InventorySiteID='" + ddlinvsiteid.SelectedValue + "' and InventorySiteMasterTbl.WarehouseID ='" + ddlwarehouse.SelectedValue + "' and  [WareHouseMaster].status = '1'";
            }
            else
            {
                main = " InventoryRoomMasterTbl.InventoryRoomName,WareHouseMaster.Name + ':' + InventorySiteMasterTbl.InventorySiteName as InventorySiteName, " +
                          " InventoryRoomMasterTbl.InventoryRoomID, InventorySiteMasterTbl.InventorySiteID, InventorySiteMasterTbl.WarehouseID " +
                          " FROM InventorySiteMasterTbl INNER JOIN " +
                          " InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID LEFT OUTER JOIN " +
                          " WareHouseMaster ON InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId" +
                          " where  [WareHouseMaster].status = '1' and InventorySiteMasterTbl.WarehouseID ='" + ddlwarehouse.SelectedValue + "'";

                str2 = "select count(InventoryRoomMasterTbl.InventoryRoomID) as ci " +
                        " FROM InventorySiteMasterTbl INNER JOIN " +
                          " InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID LEFT OUTER JOIN " +
                          " WareHouseMaster ON InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId" +
                          " where  [WareHouseMaster].status = '1' and InventorySiteMasterTbl.WarehouseID ='" + ddlwarehouse.SelectedValue + "'";
            }
        }


        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,InventorySiteMasterTbl.InventorySiteName,InventoryRoomMasterTbl.InventoryRoomName asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dth5 = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, main);
            GridView1.DataSource = dth5;

            DataView myDataView = new DataView();
            myDataView = dth5.DefaultView;

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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }

        if (e.CommandName == "Edit")
        {
            addinventoryroom.Visible = true;
            btnaddroom.Visible = false;
            lbllegend.Visible = true;
            btnupdate.Visible = true;
            ImageButton1.Visible = false;

            lbllegend.Text = "Edit Inventory Room";

            int mm = Convert.ToInt32(e.CommandArgument);
            ViewState["IDS"] = mm;

            string eeed = " SELECT     InventoryRoomMasterTbl.InventoryRoomName,WareHouseMaster.Name + ':' + InventorySiteMasterTbl.InventorySiteName as InventorySiteName,InventoryRoomMasterTbl.InventoryRoomID, InventorySiteMasterTbl.InventorySiteID, InventorySiteMasterTbl.WarehouseID FROM InventorySiteMasterTbl INNER JOIN InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID LEFT OUTER JOIN WareHouseMaster ON InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId where WareHouseMaster.comid='" + compid + "' and  [WareHouseMaster].status = '1' and InventoryRoomID='" + mm + "'  ";
            SqlCommand cmdeeed = new SqlCommand(eeed, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);

            fillstore();

            ddlwarehouse0.SelectedIndex = ddlwarehouse0.Items.IndexOf(ddlwarehouse0.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["WarehouseID"]).ToString()));

            FillDDlSite1();

            ddlinvsiteid0.SelectedIndex = ddlinvsiteid0.Items.IndexOf(ddlinvsiteid0.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["InventorySiteID"]).ToString()));

            txtinvroomname.Text = dteeed.Rows[0]["InventoryRoomName"].ToString();

        }


        if (e.CommandName == "Delete")
        {
            //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ////Session["ID9"] =GridView1.DataKeys[e.CommandArgument.Value;
            //int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value);
            //Session["ID9"] = dk;

            ////SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //string s = "select InventortyRackID  from InventortyRackMasterTbl where InventoryRoomID= " + Session["ID9"] + "";
            //SqlCommand c = new SqlCommand(s, con);
            //c.CommandType = CommandType.Text;
            //SqlDataAdapter d = new SqlDataAdapter(c);
            //DataSet ds0 = new DataSet();
            //d.Fill(ds0);

            //ViewState["dle"] = 0;
            //int a = Convert.ToInt32(ds0.Tables[0].Rows.Count);
            //if (a == 0)
            //{

            //    //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //    //Session["ID9"] = GridView1.SelectedDataKey.Value;
            //    ViewState["dle"] = 1;
            //    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //    ModalPopupExtender1222.Show();

            //    // a = 0;
            //}
            //else
            //{
            //    GridView2.DataSource = (DataSet)getSubSub();
            //    GridView2.DataBind();
            //    lblTotal.Text = a.ToString();

            //    ViewState["dle"] = 2;

            //    foreach (GridViewRow gdr in GridView2.Rows)
            //    {
            //        DropDownList drp = (DropDownList)gdr.FindControl("DropDownList3");

            //        drp.DataSource = (DataSet)fillddl();
            //        drp.DataTextField = "InventoryRoomName";
            //        drp.DataValueField = "InventoryRoomID";
            //        drp.DataBind();
            //        //drp.Items.in
            //        DataSet ds = new DataSet();
            //        ds = (DataSet)getSubSub();
            //        drp.SelectedValue = ds.Tables[0].Rows[0]["InventoryRoomID"].ToString();

            //    }
            //    ModalPopupExtender1.Show();
            //}

        }
    }
    public DataSet getSubSub()
    {


        string strinComm = "SELECT     InventoryRoomMasterTbl.InventoryRoomID, InventoryRoomMasterTbl.InventoryRoomName, " +
               "        InventortyRackMasterTbl.InventortyRackName, InventortyRackMasterTbl.InventortyRackID " +
                        " FROM         InventoryRoomMasterTbl LEFT OUTER JOIN  " +
                          "                    InventortyRackMasterTbl ON InventoryRoomMasterTbl.InventoryRoomID = InventortyRackMasterTbl.InventoryRoomID " +
                     " where  InventoryRoomMasterTbl.InventoryRoomID=" + Session["ID9"] + " order by InventoryRoomMasterTbl.InventoryRoomName,InventortyRackMasterTbl.InventortyRackName ";

        SqlCommand Mycommand = new SqlCommand(strinComm, con);
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter(Mycommand);
        MyDataAdapter.Fill(ds);
        return ds;

    }
    public DataSet fillddl()
    {
        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
        // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        Mycommand = new SqlCommand("SELECT  InventoryRoomMasterTbl.InventoryRoomID,InventorySiteMasterTbl.InventorySiteName+' : '+InventoryRoomMasterTbl.InventoryRoomName as InventoryRoomName  FROM InventoryRoomMasterTbl  inner join  InventorySiteMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID  inner join WarehouseMaster on WarehouseMaster.WarehouseId= InventorySiteMasterTbl.WarehouseID where WarehouseMaster.comid='" + Session["comid"] + "' order by InventoryRoomName ", con);
        //Mycommand = new SqlCommand("SELECT * FROM InventoryRoomMasterTbl  inner join  InventorySiteMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID  inner join WarehouseMaster on WarehouseMaster.WarehouseId= InventorySiteMasterTbl.WarehouseID where WarehouseMaster.comid='" + Session["comid"] + "'", con);

        //InventoryRoomMasterTbl.InventoryRoomID,InventorySiteMasterTb.InventorySiteName+':'+InventoryRoomMasterTbl.InventoryRoomName
        Mycommand.CommandType = CommandType.Text;

        if (Mycommand.Connection.State.ToString() != "Open")
        {
            Mycommand.Connection.Open();

        }
        MyDataAdapter = new SqlDataAdapter(Mycommand);
        MyDataAdapter.Fill(ds);
        con.Close();
        return ds;
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillGridView1();
    }


    public void cleartext()
    {
        ddlwarehouse0.ClearSelection();
        ddlinvsiteid0.ClearSelection();
        txtinvroomname.Text = "";

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



    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // GridView1.EditIndex = e.NewEditIndex;
        // FillGridView1();

        // DropDownList ddlcttt = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("DropDownList2");
        // //Label3
        // Label Label32 = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("Label3");


        //// string strmain3 = "   SELECT     InventorySiteID, InventorySiteName   FROM         InventorySiteMasterTbl ";
        // string strmain3 = "SELECT InventorySiteID,(WareHouseMaster.Name+':'+InventorySiteMasterTbl.InventorySiteName) as InventorySiteName ,WareHouseMaster.comid FROM InventorySiteMasterTbl inner join WareHouseMaster on  InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId   where WareHouseMaster.comid = '" + compid + "' and  [WareHouseMaster].status = '1'";
        // SqlCommand cmd35 = new SqlCommand(strmain3, con);
        // SqlDataAdapter adp35 = new SqlDataAdapter(cmd35);
        // DataTable dth35 = new DataTable();
        // adp35.Fill(dth35);
        // if (dth35.Rows.Count > 0)
        // {
        //     ddlcttt.DataSource = dth35;
        //     ddlcttt.DataTextField = "InventorySiteName";
        //     ddlcttt.DataValueField = "InventorySiteID";

        //     ddlcttt.DataBind();
        //     ddlcttt.Items.Insert(0, "-Select-");
        //     ddlcttt.Items[0].Value = "0";
        //     ddlcttt.SelectedIndex = ddlcttt.Items.IndexOf(ddlcttt.Items.FindByValue(Label32.Text));
        // }


        // DropDownList dlldd = (DropDownList)GridView1
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {


        GridView1.EditIndex = -1;
        FillGridView1();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);


        //DropDownList ddlcttt = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("DropDownList2");
        ////Label3
        //TextBox Label32 = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtRoomName");


        //., " +
        //                "    

        //string sr4 = "Update InventoryRoomMasterTbl set InventorySiteID=" + ddlinvsiteid0.SelectedValue + ", " +
        //    " InventoryRoomName='" + txtinvroomname.Text + "'  " +
        //" where InventoryRoomID='" + ViewState["IDS"] + "' ";
        //SqlCommand cmd8 = new SqlCommand(sr4, con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmd8.ExecuteNonQuery();
        //con.Close();

        //statuslable.Text = "Record updated successfully";
        //statuslable.Visible = true;


        //string st = "Update InventortyRackMasterTbl set InventoryRoomID=" + drp.SelectedValue + " where InventortyRackID='" + rackid.Text + "' ";
        //SqlCommand Mycommand = new SqlCommand(st, con);
        //con.Open();
        //Mycommand.ExecuteNonQuery();
        //con.Close();




        //  GridView1.EditIndex = -1;
        FillGridView1();
    }
    //protected void yes_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void ddlinvsiteid_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridView1();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        //Session["ID9"] =GridView1.DataKeys[e.CommandArgument.Value;

        GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        int dk = GridView1.SelectedIndex;

        Session["ID90"] = dk;

        Session["ID9"] = dk;

        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string s = "select InventortyRackID  from InventortyRackMasterTbl where InventoryRoomID= " + Session["ID9"] + "";
        SqlCommand c = new SqlCommand(s, con);
        c.CommandType = CommandType.Text;
        SqlDataAdapter d = new SqlDataAdapter(c);
        DataSet ds0 = new DataSet();
        d.Fill(ds0);

        ViewState["dle"] = 0;
        int a = Convert.ToInt32(ds0.Tables[0].Rows.Count);
        if (a == 0)
        {

            //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //Session["ID9"] = GridView1.SelectedDataKey.Value;
            ViewState["dle"] = 1;
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //  ModalPopupExtender1222.Show();
            yes_Click(sender, e);

            // a = 0;
        }
        else
        {
            GridView2.DataSource = (DataSet)getSubSub();
            GridView2.DataBind();

            lblTotal.Text = a.ToString();

            if (lblTotal.Text == "1")
            {
                Label15.Text = "rack. Please move it before deleting.";
            }
            else
            {
                Label15.Text = "racks. Please move them before deleting.";

            }

            ViewState["dle"] = 2;
            int i = 0;

            foreach (GridViewRow gdr in GridView2.Rows)
            {
                DropDownList drp = (DropDownList)gdr.FindControl("DropDownList3");

                drp.DataSource = (DataSet)fillddl();
                drp.DataTextField = "InventoryRoomName";
                drp.DataValueField = "InventoryRoomID";
                drp.DataBind();
                //drp.Items.in
                DataSet ds = new DataSet();
                ds = (DataSet)getSubSub();
                drp.SelectedIndex = drp.Items.IndexOf(drp.Items.FindByValue(ds.Tables[0].Rows[0]["InventoryRoomID"].ToString()));
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
    //protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCompany.Text = Session["Cname"].ToString();
        if (ddlwarehouse.SelectedIndex > 0)
        {
            lblBusiness.Text = ddlwarehouse.SelectedItem.Text;
        }
        else
        {
            lblBusiness.Text = "ALL";
        }
        FillDDlSite();
        FillGridView1();
    }
    //protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzInventorySiteMasterTbl.aspx");
    }
    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzInventortyRackMaster.aspx");
    }
    protected void ddlwarehouse0_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDlSite1();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGridView1();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        SqlCommand cmdche = new SqlCommand("Select InventoryRoomName from InventoryRoomMasterTbl where InventoryRoomName='" + txtinvroomname.Text + "' and InventorySiteID='" + ddlinvsiteid0.SelectedValue + "  '", con);
        SqlDataAdapter dtpche = new SqlDataAdapter(cmdche);
        DataSet dsche = new DataSet();
        dtpche.Fill(dsche);
        //con.Open();
        //SqlDataReader dtpre = cmdche.ExecuteReader();
        if (dsche.Tables[0].Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "This Room Name already exists for this location";
        }
        else
        {
            SqlCommand mycmd = new SqlCommand("Sp_Insert_InventoryRoomMasterTbl", con);
            mycmd.CommandType = CommandType.StoredProcedure;
            mycmd.Parameters.AddWithValue("@InventoryRoomName", txtinvroomname.Text);
            mycmd.Parameters.AddWithValue("@InventorySiteID", Convert.ToInt32(ddlinvsiteid0.SelectedValue));
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            mycmd.ExecuteNonQuery();
            con.Close();
            statuslable.Visible = true;
            statuslable.Text = "Record inserted successfully";
            FillGridView1();
            ddlwarehouse.SelectedIndex = 0;
            ddlinvsiteid.SelectedIndex = -1;
            txtinvroomname.Text = "";
            cleartext();
            FillDDlSite();


            addinventoryroom.Visible = false;
            lbllegend.Visible = false;
            btnaddroom.Visible = true;




        }

    }
    protected void ImageButton6_Click(object sender, EventArgs e)
    {
        ddlwarehouse0.SelectedIndex = 0;
        ddlinvsiteid0.SelectedIndex = -1;
        txtinvroomname.Text = "";
        statuslable.Visible = false;
        FillDDlSite1();
        addinventoryroom.Visible = false;
        lbllegend.Visible = false;
        btnaddroom.Visible = true;
        btnupdate.Visible = false;
        ImageButton1.Visible = true;


    }
    protected void yes_Click(object sender, EventArgs e)
    {
        if (ViewState["dle"].ToString() == "1")
        {
            string sr4 = ("delete from InventoryRoomMasterTbl where InventoryRoomID=" + Session["ID9"] + "");
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
        }



        else if (ViewState["dle"].ToString() == "2")
        {
            foreach (GridViewRow gdr in GridView2.Rows)
            {
                DropDownList drp = (DropDownList)gdr.Cells[0].FindControl("DropDownList3");

                Label rackid = (Label)gdr.Cells[0].FindControl("lblrackid");


                string st = "Update InventortyRackMasterTbl set InventoryRoomID=" + drp.SelectedValue + " where InventortyRackID='" + rackid.Text + "' ";
                SqlCommand Mycommand = new SqlCommand(st, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                Mycommand.ExecuteNonQuery();
                con.Close();


                string sr40 = ("delete from InventoryRoomMasterTbl where InventoryRoomID=" + Session["ID9"] + "");
                SqlCommand cmd80 = new SqlCommand(sr40, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd80.ExecuteNonQuery();
                con.Close();
                FillGridView1();

                statuslable.Visible = true;
                statuslable.Text = "Record deleted successfully";


            }
        }
        else
        {

        }
    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {

    }
    protected void ImgBtnMove_Click(object sender, EventArgs e)
    {


        ViewState["dle"] = 2;

        yes_Click(sender, e);

        ModalPopupExtender1.Hide();
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
    protected void btnaddroom_Click(object sender, EventArgs e)
    {
        fillstore();
        ddlwarehouse0_SelectedIndexChanged(sender, e);

        if (addinventoryroom.Visible == false)
        {
            addinventoryroom.Visible = true;
            lbllegend.Visible = true;
            lbllegend.Text = "Add Inventory Room";

        }
        else if (addinventoryroom.Visible == true)
        {
            addinventoryroom.Visible = false;
            lbllegend.Visible = false;
        }
        statuslable.Text = "";
        btnaddroom.Visible = false;
    }
    protected void fillstore()
    {
        ddlwarehouse0.Items.Clear();

        DataTable ds = ClsStore.SelectStorename();

        ddlwarehouse0.DataSource = ds;
        ddlwarehouse0.DataTextField = "Name";
        ddlwarehouse0.DataValueField = "WareHouseId";
        ddlwarehouse0.DataBind();
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();
        if (dteeed.Rows.Count > 0)
        {
            ddlwarehouse0.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }



    }
    protected void fillstorefilter()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlwarehouse.DataSource = ds;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();

        ddlwarehouse.Items.Insert(0, "All");
        ddlwarehouse.Items[0].Value = "0";




    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string sr4 = "Update InventoryRoomMasterTbl set InventorySiteID=" + ddlinvsiteid0.SelectedValue + ", " +
            " InventoryRoomName='" + txtinvroomname.Text + "'  " +
        " where InventoryRoomID='" + ViewState["IDS"] + "' ";
        SqlCommand cmd8 = new SqlCommand(sr4, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd8.ExecuteNonQuery();
        con.Close();

        statuslable.Text = "Record updated successfully";
        statuslable.Visible = true;

        FillGridView1();

        btnupdate.Visible = false;
        ImageButton1.Visible = true;
        btnaddroom.Visible = true;
        lbllegend.Visible = false;
        addinventoryroom.Visible = false;

        cleartext();
    }
}
