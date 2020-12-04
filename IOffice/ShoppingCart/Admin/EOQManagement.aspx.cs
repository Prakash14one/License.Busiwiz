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

public partial class ShoppingCart_Admin_EOQManagement : System.Web.UI.Page
{
    SqlConnection con;
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/ShoppingCart/Admin/ShoppingCartLogin.aspx");
        }
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        compid = Session["comid"].ToString();
        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            Fillwarehouse();
            fillsite();
            fillvendor();
            Fillfilterstore();
            Fillfiltervendor();
            Gridfill();
        }


    }
    protected void Fillwarehouse()
    {

        ddlstrname.Items.Clear();
        string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "' and [WareHouseMaster].Status='1' order by Name ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlstrname.DataSource = ds;
            ddlstrname.DataTextField = "Name";
            ddlstrname.DataValueField = "WareHouseId";
            ddlstrname.DataBind();
            ViewState["cd"] = "1";
        }
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstrname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

        }

    }
    protected void Fillfilterstore()
    {

        ddlsearchByStore.Items.Clear();
        string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "' and [WareHouseMaster].Status='1' order by Name";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlsearchByStore.DataSource = ds;
            ddlsearchByStore.DataTextField = "Name";
            ddlsearchByStore.DataValueField = "WareHouseId";
            ddlsearchByStore.DataBind();
        }
        ddlsearchByStore.Items.Insert(0, "All");
        ddlsearchByStore.Items[0].Value = "0";

    }
    protected void Fillfiltervendor()
    {
        ddlfiltervendor.Items.Clear();
        string str = "SELECT distinct Party_master.PartyID, Party_master.Contactperson,Party_master.Id,PartytTypeMaster.PartType FROM PartytTypeMaster inner join Party_master ON Party_master.PartyTypeId=PartytTypeMaster.PartyTypeId inner join User_Master on User_Master.PartyId=Party_Master.PartyId left join StatusControl on StatusControl.UserMasterId=User_Master.UserId left join StatusMaster on StatusMaster.StatusId=StatusControl.StatusMasterId where PartytTypeMaster.PartType='Vendor' and Party_master.id='" + Session["Comid"] + "'  and Party_master.Whid='" + ddlsearchByStore.SelectedValue + "' order by Contactperson";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlfiltervendor.DataSource = ds;
            ddlfiltervendor.DataTextField = "Contactperson";
            ddlfiltervendor.DataValueField = "PartyID";
            ddlfiltervendor.DataBind();
        }
        ddlfiltervendor.Items.Insert(0, "All");
        ddlfiltervendor.Items[0].Value = "0";

    }
    protected void fillsite()
    {
        ddlsite.Items.Clear();
        string str = "select InventorySiteID,InventorySiteName,WarehouseID from InventorySiteMasterTbl where WarehouseID='" + ddlstrname.SelectedValue + "' order by InventorySiteName ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlsite.DataSource = ds;
            ddlsite.DataTextField = "InventorySiteName";
            ddlsite.DataValueField = "InventorySiteID";
            ddlsite.DataBind();
        }
        ddlsite.Items.Insert(0, "-Select-");
        ddlsite.Items[0].Value = "0";
    }
    protected void fillvendor()
    {
        ddlvendor.Items.Clear();
        string str = "SELECT distinct Party_master.PartyID, Party_master.Contactperson,Party_master.Id,PartytTypeMaster.PartType FROM PartytTypeMaster inner join Party_master ON Party_master.PartyTypeId=PartytTypeMaster.PartyTypeId inner join User_Master on User_Master.PartyId=Party_Master.PartyId left join StatusControl on StatusControl.UserMasterId=User_Master.UserId left join StatusMaster on StatusMaster.StatusId=StatusControl.StatusMasterId where PartytTypeMaster.PartType='Vendor' and Party_master.id='" + Session["Comid"] + "'  and Party_master.Whid='" + ddlstrname.SelectedValue + "' and  Party_master.PartyID Not In (select VendorId  from EOQMaster ) order by Contactperson";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlvendor.DataSource = ds;
            ddlvendor.DataTextField = "Contactperson";
            ddlvendor.DataValueField = "PartyID";
            ddlvendor.DataBind();
        }
        ddlvendor.Items.Insert(0, "-Select-");
        ddlvendor.Items[0].Value = "0";
    }
    protected void getAvgWhCost()
    {
        string str = "Select * from InventorySiteMasterTbl where WarehouseID='" + ddlstrname.SelectedValue + "' and InventorySiteID='" + ddlsite.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adpt.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            Double totalWhcost = 0.0;
            Double totalvolume = 0.0;
            if (ds.Rows[0]["Totalwarehousecost"].ToString() != "")
            {
                totalWhcost = Convert.ToDouble(ds.Rows[0]["Totalwarehousecost"].ToString());
            }
            if (ds.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString() != "")
            {
                totalvolume = Convert.ToDouble(ds.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString());
            }
            Double avgcost = 0;
            if (totalWhcost != 0.0 && totalvolume != 0.0)
            {
                avgcost = totalWhcost / totalvolume;
            }
            lblavgwhcost.Text = avgcost.ToString();
            if (ds.Rows[0]["InterestRate"].ToString() != "" && ds.Rows[0]["InterestRate"].ToString() != null)
            {
                txtintperyear.Text = ds.Rows[0]["InterestRate"].ToString();
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //if(ddlstrname.SelectedIndex > 0)
        //{
        //    if (ddlsite.SelectedIndex > 0)
        //    {
        //        if (ddlvendor.SelectedIndex > 0)
        //        {
        string str = "insert into EOQMaster(SiteId,AvgCostPerCubicFeet,IntRatePerYr,VendorId,NumOfProduct,VolumeSize,OrderAndReqCost,NumOfDays,FreightCost,PerSizeCost,ProcessCost,TotalCost,TotalCostperProduct,Whid,Compid)" +
        "values ('" + ddlsite.SelectedValue + "','" + lblavgwhcost.Text + "','" + txtintperyear.Text + "','" + ddlvendor.SelectedValue + "','" + txtnumofproduct.Text + "','" + txtvolumesize.Text + "','" + txtreqcost.Text + "','" + txtnumofdays.Text + "','" + txtfreightcost.Text + "','" + txtsizecost.Text + "','" + txtinvoicecost.Text + "','" + lbltotalcost.Text + "','" + lbltotalcostperproduct.Text + "','" + ddlstrname.SelectedValue + "','" + Session["Comid"] + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        statuslable.Text = "Record inserted successfully";
        clearall();
        Gridfill();
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";
        fillvendor();
        //    }
        //    else
        //    {
        //        statuslable.Text = "Please select vendor";
        //    }
        //}
        //else
        //{
        //    statuslable.Text = "Please select site";
        //}
        // }
    }
    protected void ddlstrname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillsite();
        fillvendor();
    }
    protected void ddlsite_SelectedIndexChanged(object sender, EventArgs e)
    {
        getAvgWhCost();
    }
    protected void btncalc_Click(object sender, EventArgs e)
    {
        Double totalcost = 0;
        Double totalcostperproduct = 0;
        totalcost = Convert.ToDouble(txtreqcost.Text) + Convert.ToDouble(txtfreightcost.Text) + Convert.ToDouble(txtsizecost.Text) + Convert.ToDouble(txtinvoicecost.Text);
        totalcostperproduct = totalcost / Convert.ToInt32(txtnumofproduct.Text);
        lbltotalcost.Text = Math.Round(totalcost, 2).ToString();
        if (totalcostperproduct.ToString() == "NaN")
        {
            lbltotalcostperproduct.Text = "0.00";
        }
        else
        {
            lbltotalcostperproduct.Text = Math.Round(totalcostperproduct, 2).ToString();
        }
    }
    protected void clearall()
    {
        ddlsite.SelectedIndex = 0;
        lblavgwhcost.Text = "0";
        txtintperyear.Text = "";
        ddlvendor.SelectedIndex = 0;
        txtnumofproduct.Text = "0";
        txtvolumesize.Text = "0";
        txtreqcost.Text = "0";
        txtnumofdays.Text = "0";
        txtfreightcost.Text = "0";
        txtsizecost.Text = "0";
        txtinvoicecost.Text = "0";
        lbltotalcost.Text = "0";
        lbltotalcostperproduct.Text = "0";

    }
    protected void Gridfill()
    {
        string str1 = "";

        if (ddlsearchByStore.SelectedIndex > 0)
        {
            str1 = " and EOQMaster.Whid=" + ddlsearchByStore.SelectedValue;
            if (ddlfiltervendor.SelectedIndex > 0)
            {
                str1 = " and EOQMaster.Whid='" + ddlsearchByStore.SelectedValue + "' and EOQMaster.VendorId = '" + ddlfiltervendor.SelectedValue + "'";
            }
        }

        string str = " EOQMaster.*,Party_master.PartyID,Party_master.Contactperson from EOQMaster inner join Party_master on Party_master.PartyID=EOQMaster.VendorId where EOQMaster.Compid='" + Session["Comid"] + "' " + str1 + "";

        string str2 = " select count(EOQMaster.Id) as ci from EOQMaster inner join Party_master on Party_master.PartyID=EOQMaster.VendorId where EOQMaster.Compid='" + Session["Comid"] + "' " + str1 + "";

        lblcompany.Text = Session["Cname"].ToString();
        lblbusiness.Text = ddlsearchByStore.SelectedItem.Text;
        lblcostbyven.Text = ddlfiltervendor.SelectedItem.Text;


        //str = str + str1 + " order by Contactperson"; 
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //DataTable ds = new DataTable();
        //adpt.Fill(ds);

        grid.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " Contactperson asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable ds = GetDataPage(grid.PageIndex, grid.PageSize, sortExpression, str);

            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            grid.DataSource = myDataView;
            grid.DataBind();
        }
        else
        {
            grid.DataSource = null;
            grid.DataBind();
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

    protected void ddlsearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillfiltervendor();
        Gridfill();
    }
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ViewState["id"] = Convert.ToInt32(grid.DataKeys[e.NewEditIndex].Value.ToString());
        grid.SelectedIndex = Convert.ToInt32(grid.DataKeys[e.NewEditIndex].Value.ToString());
        btnupdate.Visible = true;
        btncancel.Visible = true;
        btnsubmit.Visible = false;
        btnreset.Visible = false;
        string str = "select * from EOQMaster where Id='" + grid.SelectedIndex + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adpt.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            Fillwarehouse();
            ddlstrname.SelectedIndex = ddlstrname.Items.IndexOf(ddlstrname.Items.FindByValue(ds.Rows[0]["Whid"].ToString()));
            fillsite();
            ddlsite.SelectedIndex = ddlsite.Items.IndexOf(ddlsite.Items.FindByValue(ds.Rows[0]["SiteId"].ToString()));
            lblavgwhcost.Text = ds.Rows[0]["AvgCostPerCubicFeet"].ToString();
            txtintperyear.Text = ds.Rows[0]["IntRatePerYr"].ToString();
            //fillvendor();
            string str1 = "SELECT distinct Party_master.PartyID, Party_master.Contactperson,Party_master.Id,PartytTypeMaster.PartType FROM PartytTypeMaster inner join Party_master ON Party_master.PartyTypeId=PartytTypeMaster.PartyTypeId inner join User_Master on User_Master.PartyId=Party_Master.PartyId left join StatusControl on StatusControl.UserMasterId=User_Master.UserId left join StatusMaster on StatusMaster.StatusId=StatusControl.StatusMasterId where PartytTypeMaster.PartType='Vendor' and Party_master.id='" + Session["Comid"] + "'  and Party_master.Whid='" + ddlstrname.SelectedValue + "' and  (Party_master.PartyID ='" + ds.Rows[0]["VendorId"].ToString() + "' OR  Party_master.PartyID Not In (select VendorId  from EOQMaster ))";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            adpt1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlvendor.DataSource = ds1;
                ddlvendor.DataTextField = "Contactperson";
                ddlvendor.DataValueField = "PartyID";
                ddlvendor.DataBind();
            }
            ddlvendor.Items.Insert(0, "-Select-");
            ddlvendor.Items[0].Value = "0";
            ddlvendor.SelectedIndex = ddlvendor.Items.IndexOf(ddlvendor.Items.FindByValue(ds.Rows[0]["VendorId"].ToString()));
            txtnumofproduct.Text = ds.Rows[0]["NumOfProduct"].ToString();
            txtvolumesize.Text = ds.Rows[0]["VolumeSize"].ToString();
            txtreqcost.Text = ds.Rows[0]["OrderAndReqCost"].ToString();
            txtnumofdays.Text = ds.Rows[0]["NumOfDays"].ToString();
            txtfreightcost.Text = ds.Rows[0]["FreightCost"].ToString();
            txtsizecost.Text = ds.Rows[0]["PerSizeCost"].ToString();
            txtinvoicecost.Text = ds.Rows[0]["ProcessCost"].ToString();
            lbltotalcost.Text = ds.Rows[0]["TotalCost"].ToString();
            lbltotalcostperproduct.Text = ds.Rows[0]["TotalCostperProduct"].ToString();
            statuslable.Text = "";
            pnladd.Visible = true;
            btnadd.Visible = false;
            lbladd.Text = "Edit Cost For the Vendor";
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        //if (ddlstrname.SelectedIndex > 0)
        //{
        //    if (ddlsite.SelectedIndex > 0)
        //    {
        //        if (ddlvendor.SelectedIndex > 0)
        //        {
        string str = "update EOQMaster set SiteId = '" + ddlsite.SelectedValue + "',AvgCostPerCubicFeet = '" + lblavgwhcost.Text + "',IntRatePerYr = '" + txtintperyear.Text + "',VendorId = '" + ddlvendor.SelectedValue + "',NumOfProduct = '" + txtnumofproduct.Text + "',VolumeSize = '" + txtvolumesize.Text + "',OrderAndReqCost = '" + txtreqcost.Text + "',NumOfDays = '" + txtnumofdays.Text + "',FreightCost = '" + txtfreightcost.Text + "',PerSizeCost  = '" + txtsizecost.Text + "',ProcessCost = '" + txtinvoicecost.Text + "',TotalCost = '" + lbltotalcost.Text + "',TotalCostperProduct = '" + lbltotalcostperproduct.Text + "',Whid = '" + ddlstrname.SelectedValue + "',Compid = '" + Session["Comid"] + "' where Id= '" + ViewState["id"] + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        statuslable.Text = "Record updated successfully";
        clearall();
        Gridfill();
        btnsubmit.Visible = true;
        btnreset.Visible = true;
        btnupdate.Visible = false;
        btncancel.Visible = false;
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";
        fillvendor();
        //        }
        //        else
        //        {
        //            statuslable.Text = "Please select vendor";
        //        }
        //    }
        //    else
        //    {
        //        statuslable.Text = "Please select site";
        //    }
        //}
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {

        btnsubmit.Visible = true;
        btnreset.Visible = true;
        btnupdate.Visible = false;
        btncancel.Visible = false;
        statuslable.Text = "";
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";
        fillvendor();
        clearall();
    }
    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ViewState["id"] = Convert.ToInt32(grid.DataKeys[e.RowIndex].Value.ToString());
        string str = "Delete from EOQMaster where Id='" + ViewState["id"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        statuslable.Text = "Record deleted successfully";
        Gridfill();
        fillvendor();
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        clearall();
        statuslable.Text = "";
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";
    }
    protected void ddlfiltervendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gridfill();
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            grid.AllowPaging = false;
            grid.PageSize = 1000;
            Gridfill();

            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (grid.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grid.Columns[9].Visible = false;
            }
            if (grid.Columns[10].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                grid.Columns[10].Visible = false;
            }
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(150);

            grid.AllowPaging = true;
            grid.PageSize = 10;
            Gridfill();

            btncancel0.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                grid.Columns[9].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                grid.Columns[10].Visible = true;
            }
        }
    }
    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid.PageIndex = e.NewPageIndex;
        Gridfill();
    }
    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        Gridfill();
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
    protected void btnadd_Click(object sender, EventArgs e)
    {
        statuslable.Text = "";
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            btnadd.Visible = false;
            lbladd.Text = "Add Cost For the Vendor";
        }
        else
        {
            pnladd.Visible = false;
            btnadd.Visible = true;
            lbladd.Text = "";

        }
    }
    protected void ImageButton50_Click(object sender, ImageClickEventArgs e)
    {
        string te = "InventorySiteMasterTbl.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string te = "vendorpartyregister.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ImageButton51_Click1(object sender, ImageClickEventArgs e)
    {
        fillsite();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        fillvendor();
    }
}
