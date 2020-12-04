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

public partial class ShoppingCart_Admin_InventoryRackMaster11 : System.Web.UI.Page
{
    string compid;
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
        compid = Session["Comid"].ToString();

        Page.Title = pg.getPageTitle(page);




        statuslable.Visible = false;
        ModalPopupExtender1.Hide();


        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            lblCompany.Text = Session["Cname"].ToString();

            fillstore();

            fillsitename();
            FillddlRoomMaster();
            fillstorewithfilter();
            DropDownList2_SelectedIndexChanged(sender, e);



            ViewState["sortOrder"] = "";

            FillGridView1();
        }
    }
    protected void FillddlRoomMaster()
    {


        string strddlfill = "select InventoryRoomName,InventoryRoomID from InventoryRoomMasterTbl where InventorySiteID='" + ddlsitename.SelectedValue + "' order by InventoryRoomName ";
        SqlCommand cmdsd = new SqlCommand(strddlfill, con);
        SqlDataAdapter adpsd = new SqlDataAdapter(cmdsd);
        DataTable sdrtt = new DataTable();
        adpsd.Fill(sdrtt);

        ddlinvroomid.DataSource = sdrtt;
        ddlinvroomid.DataTextField = "InventoryRoomName";
        ddlinvroomid.DataValueField = "InventoryRoomID";
        ddlinvroomid.DataBind();


    }

    protected void FillGridView1()
    {
        lblCompany.Text = Session["Cname"].ToString();

        string st1 = "";
        string st2 = "";
        string st3 = "";
        string strGrd = "";

        if (DropDownList2.SelectedIndex > 0)
        {
            st1 = " and WareHouseMaster.WareHouseId='" + DropDownList2.SelectedValue + "'";
        }
        if (ddlinvsiteid.SelectedIndex > 0)
        {
            st2 = " and InventorySiteMasterTbl.InventorySiteID='" + ddlinvsiteid.SelectedValue + "'";
        }
        if (ddlfilterinventoryroom.SelectedIndex > 0)
        {
            lblroomprint.Text = ddlfilterinventoryroom.SelectedItem.Text;
            st3 = " and InventoryRoomMasterTbl.InventoryRoomID='" + ddlfilterinventoryroom.SelectedValue + "'";
        }


        strGrd = " InventorySiteMasterTbl.InventorySiteID,  InventortyRackMasterTbl.InventortyRackID, InventortyRackMasterTbl.InventortyRackName, InventortyRackMasterTbl.NumberofShelves, " +
                   " InventortyRackMasterTbl.Numberofpositionsonshelf, InventortyRackMasterTbl.SizeofRack, InventoryRoomMasterTbl.InventoryRoomID, " +
                   " InventoryRoomMasterTbl.InventoryRoomName, InventorySiteMasterTbl.InventorySiteName, WareHouseMaster.Name,WareHouseMaster.Name + ' : '  + InventorySiteMasterTbl.InventorySiteName as Name1 " +
                    "FROM InventorySiteMasterTbl INNER JOIN " +
                   " InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID inner JOIN " +
                   " InventortyRackMasterTbl ON InventoryRoomMasterTbl.InventoryRoomID = InventortyRackMasterTbl.InventoryRoomID inner JOIN" +
                   " WareHouseMaster ON InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId where WareHouseMaster.comid='" + compid + "' and  [WareHouseMaster].status = '1' " + st1 + "" + st2 + "" + st3 + "";

        string str2 = "select count(InventortyRackMasterTbl.InventortyRackID) as ci " +
                     "FROM InventorySiteMasterTbl INNER JOIN " +
                     " InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID inner JOIN " +
                     " InventortyRackMasterTbl ON InventoryRoomMasterTbl.InventoryRoomID = InventortyRackMasterTbl.InventoryRoomID inner JOIN" +
                     " WareHouseMaster ON InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId where WareHouseMaster.comid='" + compid + "' and  [WareHouseMaster].status = '1' " + st1 + "" + st2 + "" + st3 + "";

        // string st4 = " order by WareHouseMaster.Name,InventorySiteMasterTbl.InventorySiteName,InventoryRoomMasterTbl.InventoryRoomName";

        lblBus.Text = DropDownList2.SelectedItem.Text;
        lblSite.Text = ddlinvsiteid.SelectedItem.Text;
        lblroomprint.Text = ddlfilterinventoryroom.SelectedItem.Text;

        //strGrd = strGrd + st1 + st2 + st3 + st4;
        //SqlCommand cmdGrd = new SqlCommand(strGrd, con);
        //SqlDataAdapter adpGrd = new SqlDataAdapter(cmdGrd);
        //DataTable dtGrd = new DataTable();
        //adpGrd.Fill(dtGrd);

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,InventorySiteMasterTbl.InventorySiteName,InventoryRoomMasterTbl.InventoryRoomName asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dtGrd = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, strGrd);

            GridView1.DataSource = dtGrd;

            DataView myDataView = new DataView();
            myDataView = dtGrd.DefaultView;

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

    protected void clearAllControl()
    {


        txtinvrackname.Text = "";
        txtpositionshelf.Text = "";
        txtshelves.Text = "";
        txtsizeofrack.Text = "";
        //object sender = new object();
        //EventArgs e = new EventArgs();
        //ddlwarehouse_SelectedIndexChanged(sender, e);


    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Sort")
            {
                return;
            }
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int dkid = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value);
            ViewState["dleId"] = dkid;

            //if (e.CommandName == "Delete")
            //{
            //    //ModalPopupExtender1.Show();

            //}
        }
        catch (Exception)
        {
            statuslable.Visible = true;
            statuslable.Text = "Sorry,you can not edit this record";
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView1.EditIndex = e.NewEditIndex;



        //FillGridView1();

        int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        ViewState["Id"] = dk1;

        string eeed = " select InventortyRackMasterTbl.*,InventorySiteMasterTbl.WarehouseID,InventorySiteMasterTbl.InventorySiteID,InventoryRoomMasterTbl.InventoryRoomID from InventortyRackMasterTbl inner join InventoryRoomMasterTbl on InventoryRoomMasterTbl.InventoryRoomID=InventortyRackMasterTbl.InventoryRoomID inner join InventorySiteMasterTbl on InventoryRoomMasterTbl.InventorySiteID=InventorySiteMasterTbl.InventorySiteID inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventorySiteMasterTbl.WarehouseID  where InventortyRackMasterTbl.InventortyRackID='" + dk1 + "'  ";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        fillstore();
        ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["WarehouseID"]).ToString()));

        fillsitename();
        ddlsitename.SelectedIndex = ddlsitename.Items.IndexOf(ddlsitename.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["InventorySiteID"]).ToString()));

        FillddlRoomMaster();
        ddlinvroomid.SelectedIndex = ddlinvroomid.Items.IndexOf(ddlinvroomid.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["InventoryRoomID"]).ToString()));


        txtinvrackname.Text = dteeed.Rows[0]["InventortyRackName"].ToString();
        txtshelves.Text = dteeed.Rows[0]["NumberofShelves"].ToString();
        txtsizeofrack.Text = dteeed.Rows[0]["SizeofRack"].ToString();



        lbllegend.Visible = true;
        lbllegend.Text = "Edit Inventory Rack";

        ImageButton1.Visible = false;
        btnupdate.Visible = true;

        addinventoryroom.Visible = true;
        btnaddroom.Visible = false;



    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGridView1();
    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //if (GridView1.DataKeys[e.RowIndex].Value.ToString() != "")
        //{

        //    GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());

        //    ViewState["dleId"] = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);



        //    if (ViewState["dleId"] != null)
        //    {
        //        try
        //        {
        //            string strdeletecheck = "select * from InventortyRackMasterTbl where InventortyRackID='" + ViewState["dleId"].ToString() + "' ";


        //            SqlCommand cmddeletecheck = new SqlCommand(strdeletecheck, con);
        //            SqlDataAdapter adpdeletecheck = new SqlDataAdapter(cmddeletecheck);
        //            DataTable sdrdeletecheck = new DataTable();
        //            adpdeletecheck.Fill(sdrdeletecheck);
        //            if (sdrdeletecheck.Rows.Count > 0)
        //            {
        //                string grddel = " delete from InventortyRackMasterTbl where InventortyRackID='" + ViewState["dleId"].ToString() + "' ";
        //                SqlCommand cmddel = new SqlCommand(grddel, con);
        //                if (con.State.ToString() != "Open")
        //                {
        //                    con.Open();
        //                }
        //                cmddel.ExecuteNonQuery();
        //                con.Close();
        //                statuslable.Visible = true;
        //                statuslable.Text = "Record deleted successfully";
        //                FillGridView1();
        //            }
        //            else
        //            {
        //                statuslable.Visible = true;
        //                statuslable.Text = "Sorry there is no racks inserted so you can not delete this rack.";

        //            }





        //        }
        //        catch (Exception ere)
        //        {
        //            statuslable.Visible = true;
        //            statuslable.Text = "Error :" + ere.Message;
        //        }

        //    }
        //}
        //else
        //{
        //    statuslable.Visible = true;
        //    statuslable.Text = "Sorry there is no racks inserted so you can not delete this rack.";


        //}

        string strdeletecheck = "select * from InventoryLocationTbl where InventortyRackID='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";


        SqlCommand cmddeletecheck = new SqlCommand(strdeletecheck, con);
        SqlDataAdapter adpdeletecheck = new SqlDataAdapter(cmddeletecheck);
        DataTable sdrdeletecheck = new DataTable();
        adpdeletecheck.Fill(sdrdeletecheck);

        if (sdrdeletecheck.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "You are unable to delete this record from this page. Please go to " + "<a href=\"InventoryLocationMasterTbl.aspx\" style=\"font-size:14px; color:red; \" target=\"_blank\">" + "Inventory Location Master page " + "</a> if you wish to delete this record.";


        }
        else
        {
            string st2 = "delete from InventortyRackMasterTbl where InventortyRackID='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
            SqlCommand cmd2 = new SqlCommand(st2, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd2.ExecuteNonQuery();
            con.Close();



            statuslable.Visible = true;
            statuslable.Text = "Record deleted successfully";

            GridView1.EditIndex = -1;
            FillGridView1();
        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //try
        //{

        //    DropDownList dlrm = (DropDownList)(GridView1.Rows[e.RowIndex].FindControl("DropDownList1"));
        //    TextBox invRacknm = (TextBox)(GridView1.Rows[e.RowIndex].FindControl("TextBox8"));
        //    TextBox noofShelfs = (TextBox)(GridView1.Rows[e.RowIndex].FindControl("TextBox2"));
        //    // TextBox noofPosOnshelf = (TextBox)(GridView1.Rows[e.RowIndex].FindControl("TextBox3"));
        //    TextBox sizeofRack = (TextBox)(GridView1.Rows[e.RowIndex].FindControl("TextBox5"));
        //    Label rackid = (Label)(GridView1.Rows[e.RowIndex].FindControl("lblRackId"));

        //    string strddlfill = "Select InventortyRackMasterTbl.* FROM InventorySiteMasterTbl INNER JOIN " +
        //            " InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID LEFT OUTER JOIN " +
        //            " InventortyRackMasterTbl ON InventoryRoomMasterTbl.InventoryRoomID = InventortyRackMasterTbl.InventoryRoomID LEFT OUTER JOIN" +
        //            " WareHouseMaster ON InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId where InventoryRoomMasterTbl.InventoryRoomID='" + dlrm.SelectedValue + "' and InventortyRackMasterTbl.InventortyRackName='" + invRacknm.Text + "' and InventortyRackID<>'" + rackid.Text + "'  ";
        //    SqlCommand cmdsd = new SqlCommand(strddlfill, con);
        //    SqlDataAdapter adpsd = new SqlDataAdapter(cmdsd);
        //    DataTable sdrtt = new DataTable();
        //    adpsd.Fill(sdrtt);
        //    if (sdrtt.Rows.Count == 0)
        //    {
        //        string grdupdate = "UPDATE InventortyRackMasterTbl " +
        //         " SET InventortyRackName ='" + invRacknm.Text + "'  " +
        //    "  ,InventoryRoomID ='" + dlrm.SelectedValue + "'  " +
        //    "  ,NumberofShelves ='" + noofShelfs.Text + "'  " +

        //      " ,SizeofRack ='" + sizeofRack.Text + "'  " +
        //      " WHERE InventortyRackID='" + rackid.Text + "'    ";
        //        SqlCommand cmdUp = new SqlCommand(grdupdate, con);
        //        //
        //        //SqlDataAdapter adpUp = new SqlDataAdapter(cmdUp);
        //        if (con.State.ToString() != "Open")
        //        {
        //            con.Open();

        //        }

        //        cmdUp.ExecuteNonQuery();
        //        con.Close();


        //        statuslable.Text = "Record updated successfully";
        //        statuslable.Visible = true;
        //        addinventoryroom.Visible = false;
        //        btnaddroom.Visible = true;
        //    }
        //    else
        //    {
        //        statuslable.Text = "Record already used";
        //        statuslable.Visible = true;
        //    }
        //}
        //catch (Exception trrrr)
        //{
        //    statuslable.Text = "Error :" + trrrr.Message;
        //    statuslable.Visible = true;
        //}
        //GridView1.EditIndex = -1;
        //FillGridView1();
    }



    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
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

    //protected void yes_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void Button1a_Click(object sender, ImageClickEventArgs e)
    {

    }
    //protected void Buttonc1_Click(object sender, ImageClickEventArgs e)
    //{

    //}

    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlsitename.Items.Clear();
        //if (ddlwarehouse.SelectedIndex > 0)
        //{
        //    SqlCommand cmdw = new SqlCommand("select InventorySiteID,InventorySiteName from InventorySiteMasterTbl where WarehouseID='" + ddlwarehouse.SelectedValue + "'", con);
        //    SqlDataAdapter dtpw = new SqlDataAdapter(cmdw);
        //    DataTable dtw = new DataTable();
        //    dtpw.Fill(dtw);

        //    if (dtw.Rows.Count > 0)
        //    {
        //        ddlsitename.DataSource = dtw;
        //        ddlsitename.DataTextField = "InventorySiteName";
        //        ddlsitename.DataValueField = "InventorySiteID";
        //        ddlsitename.DataBind();

        //        ddlsitename.Items.Insert(0, "-Select-");
        //    }
        //    else
        //    {
        //        ddlsitename.Items.Clear();
        //        ddlsitename.Items.Insert(0, "-Select-");
        //        ddlsitename.SelectedItem.Value = "0";
        //    }
        //}
        // ddlsitename_SelectedIndexChanged(sender, e);
        fillsitename();
        FillddlRoomMaster();
    }
    protected void ddlsitename_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillddlRoomMaster();
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGridView1();
    }
    protected void yes_Click(object sender, EventArgs e)
    {
        if (ViewState["dleId"] != null)
        {
            try
            {
                string grddel = " delete from InventortyRackMasterTbl where InventortyRackID='" + ViewState["dleId"].ToString() + "' ";
                SqlCommand cmddel = new SqlCommand(grddel, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddel.ExecuteNonQuery();
                con.Close();
                statuslable.Visible = true;
                statuslable.Text = "Record deleted successfully";
                FillGridView1();
            }
            catch (Exception ere)
            {
                statuslable.Visible = true;
                statuslable.Text = "Error :" + ere.Message;
            }

        }
    }
    protected void Buttonc1_Click(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlsitename.SelectedIndex > -1)
            {
                if (ddlinvroomid.SelectedIndex > -1)
                {
                    string strddlfill = "Select InventortyRackMasterTbl.* FROM InventorySiteMasterTbl INNER JOIN " +
                            " InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID LEFT OUTER JOIN " +
                            " InventortyRackMasterTbl ON InventoryRoomMasterTbl.InventoryRoomID = InventortyRackMasterTbl.InventoryRoomID LEFT OUTER JOIN" +
                            " WareHouseMaster ON InventorySiteMasterTbl.WarehouseID = WareHouseMaster.WareHouseId where InventortyRackMasterTbl.InventortyRackName='" + txtinvrackname.Text + "' and InventorySiteMasterTbl.WarehouseID='" + ddlwarehouse.SelectedValue + "'";
                    SqlCommand cmdsd = new SqlCommand(strddlfill, con);
                    SqlDataAdapter adpsd = new SqlDataAdapter(cmdsd);
                    DataTable sdrtt = new DataTable();
                    adpsd.Fill(sdrtt);
                    if (sdrtt.Rows.Count == 0)
                    {
                        SqlCommand mycmd = new SqlCommand("Sp_Insert_InventortyRackMasterTbl", con);
                        mycmd.CommandType = CommandType.StoredProcedure;
                        mycmd.Parameters.AddWithValue("@InventortyRackName", txtinvrackname.Text);
                        mycmd.Parameters.AddWithValue("@InventoryRoomID", Convert.ToInt32(ddlinvroomid.SelectedValue));
                        mycmd.Parameters.AddWithValue("@NumberofShelves", txtshelves.Text);
                        mycmd.Parameters.AddWithValue("@Numberofpositionsonshelf", txtpositionshelf.Text);
                        mycmd.Parameters.AddWithValue("@SizeofRack", txtsizeofrack.Text);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();

                        }

                        mycmd.ExecuteNonQuery();
                        con.Close();

                        clearAllControl();
                        statuslable.Visible = true;
                        statuslable.Text = "Record inserted successfully";
                        FillGridView1();
                        lbllegend.Visible = false;
                        lbllegend.Text = "Add New Inventory Rack";

                        addinventoryroom.Visible = false;
                        btnaddroom.Visible = true;
                        selectedindexclear();
                    }
                    else
                    {
                        statuslable.Visible = true;
                        statuslable.Text = "Record already exists";
                    }
                }
                else
                {
                    statuslable.Visible = true;
                    statuslable.Text = "Please select Room Name";
                }
            }
            else
            {
                statuslable.Visible = true;
                statuslable.Text = "Please select Location Name";

            }

        }
        catch (Exception tr)
        {
            statuslable.Visible = true;
            statuslable.Text = "Error : " + tr.Message;
        }

    }
    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        clearAllControl();
        addinventoryroom.Visible = false;
        btnaddroom.Visible = true;

        lbllegend.Visible = false;
        lbllegend.Text = "Add New Inventory Rack";

        btnupdate.Visible = false;
        ImageButton1.Visible = true;

        GridView1.EditIndex = -1;
        FillGridView1();
        selectedindexclear();

    }
    protected void selectedindexclear()
    {

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlinvsiteid.Items.Clear();


        SqlCommand cmdw = new SqlCommand("select InventorySiteID,InventorySiteName from InventorySiteMasterTbl where WarehouseID='" + DropDownList2.SelectedValue + "' order by InventorySiteName", con);
        SqlDataAdapter dtpw = new SqlDataAdapter(cmdw);
        DataTable dtw = new DataTable();
        dtpw.Fill(dtw);

        if (dtw.Rows.Count > 0)
        {
            ddlinvsiteid.DataSource = dtw;
            ddlinvsiteid.DataTextField = "InventorySiteName";
            ddlinvsiteid.DataValueField = "InventorySiteID";
            ddlinvsiteid.DataBind();

            ddlinvsiteid.Items.Insert(0, "All");
            ddlinvsiteid.Items[0].Value = "0";

        }
        else
        {

            ddlinvsiteid.Items.Insert(0, "All");
            ddlinvsiteid.SelectedItem.Value = "0";
        }






        FillfilterddlRoomMaster();
        FillGridView1();
    }
    protected void ddlinvsiteid_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillfilterddlRoomMaster();
        FillGridView1();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            FillGridView1();

            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[9].Visible = false;
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
            FillGridView1();

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[8].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
        }
    }
    protected void btnaddroom_Click(object sender, EventArgs e)
    {
        if (addinventoryroom.Visible == false)
        {
            addinventoryroom.Visible = true;

        }
        else if (addinventoryroom.Visible == true)
        {
            addinventoryroom.Visible = false;

        }
        statuslable.Text = "";
        btnaddroom.Visible = false;
        lbllegend.Visible = true;
        lbllegend.Text = "Add New Inventory Rack";
    }
    protected void fillstore()
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
    protected void fillstorewithfilter()
    {
        DropDownList2.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();


        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Name";
        DropDownList2.DataValueField = "WareHouseId";
        DropDownList2.DataBind();

        DropDownList2.Items.Insert(0, "All");
        DropDownList2.Items[0].Value = "0";

    }
    protected void fillsitename()
    {
        ddlsitename.Items.Clear();
        SqlCommand cmdw = new SqlCommand("select InventorySiteID,InventorySiteName from InventorySiteMasterTbl where WarehouseID='" + ddlwarehouse.SelectedValue + "' order by InventorySiteName ", con);
        SqlDataAdapter dtpw = new SqlDataAdapter(cmdw);
        DataTable dtw = new DataTable();
        dtpw.Fill(dtw);

        ddlsitename.DataSource = dtw;
        ddlsitename.DataTextField = "InventorySiteName";
        ddlsitename.DataValueField = "InventorySiteID";
        ddlsitename.DataBind();

    }


    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (ddlsitename.SelectedIndex > -1)
        {
            if (ddlinvroomid.SelectedIndex > -1)
            {
                string strddlfill = " select * from InventortyRackMasterTbl where InventoryRoomID='" + ddlinvroomid.SelectedValue + "' and InventortyRackName='" + txtinvrackname.Text + "' and   InventortyRackID<>'" + ViewState["Id"] + "' ";
                SqlCommand cmdsd = new SqlCommand(strddlfill, con);
                SqlDataAdapter adpsd = new SqlDataAdapter(cmdsd);
                DataTable sdrtt = new DataTable();
                adpsd.Fill(sdrtt);

                if (sdrtt.Rows.Count > 0)
                {
                    statuslable.Visible = true;
                    statuslable.Text = "Record already exists";

                }
                else
                {
                    string grdupdate = " update InventortyRackMasterTbl set InventoryRoomID='" + ddlinvroomid.SelectedValue + "' ,InventortyRackName='" + txtinvrackname.Text + "',NumberofShelves='" + txtshelves.Text + "',SizeofRack='" + txtsizeofrack.Text + "' where InventortyRackID='" + ViewState["Id"] + "'  ";
                    SqlCommand cmdUp = new SqlCommand(grdupdate, con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();

                    }

                    cmdUp.ExecuteNonQuery();
                    con.Close();

                    statuslable.Text = "Record updated successfully";
                    statuslable.Visible = true;
                    addinventoryroom.Visible = false;
                    btnaddroom.Visible = true;
                    GridView1.EditIndex = -1;
                    FillGridView1();
                    ImageButton1.Visible = true;
                    btnupdate.Visible = false;
                    lbllegend.Visible = false;
                    lbllegend.Text = "Add New Inventory Rack";
                    clearAllControl();
                    selectedindexclear();
                }
            }
            else
            {
                statuslable.Visible = true;
                statuslable.Text = "Please select Room Name";
            }

        }
        else
        {
            statuslable.Visible = true;
            statuslable.Text = "Please select Location Name";
        }



    }
    protected void FillfilterddlRoomMaster()
    {

        ddlfilterinventoryroom.Items.Clear();

        if (ddlinvsiteid.SelectedIndex > 0)
        {
            string strddlfill = "select InventoryRoomName,InventoryRoomID from InventoryRoomMasterTbl where InventorySiteID='" + ddlinvsiteid.SelectedValue + "' order by InventoryRoomName ";
            SqlCommand cmdsd = new SqlCommand(strddlfill, con);
            SqlDataAdapter adpsd = new SqlDataAdapter(cmdsd);
            DataTable sdrtt = new DataTable();
            adpsd.Fill(sdrtt);

            ddlfilterinventoryroom.DataSource = sdrtt;
            ddlfilterinventoryroom.DataTextField = "InventoryRoomName";
            ddlfilterinventoryroom.DataValueField = "InventoryRoomID";
            ddlfilterinventoryroom.DataBind();

            ddlfilterinventoryroom.Items.Insert(0, "All");
            ddlfilterinventoryroom.Items[0].Value = "0";

        }
        else
        {

            ddlfilterinventoryroom.Items.Insert(0, "All");
            ddlfilterinventoryroom.Items[0].Value = "0";
        }
    }
    protected void ddlfilterinventoryroom_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridView1();
    }
}
