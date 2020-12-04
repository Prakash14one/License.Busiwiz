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

public partial class InventoryLocationMasterTbl : System.Web.UI.Page
{

    public int locid = 0;
    
    SqlConnection con;
    DBCommands1 dbss1 = new DBCommands1();
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
        compid = Session["Comid"].ToString();

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        /// lblBarcode.Visible = false;
        /// 
        Label1.Text = hdninvExist.Value + "-1";
        lblInvName.Visible = false;
        lblmsg.Visible = false;
        if (!IsPostBack)
        {

            Pagecontrol.dypcontrol(Page, page);

            ViewState["CSSCNm"] = "";

            hdninvExist.Value = "0";
            Label1.Text = hdninvExist.Value + "-2";

            string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + compid + "' and Status='" + 1 + "' order by name";
            SqlCommand cmdwh = new SqlCommand(strwh, con);
            SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            DataTable dtwh = new DataTable();
            adpwh.Fill(dtwh);

            ddlWarehouse.DataSource = dtwh;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "WareHouseId";
            ddlWarehouse.DataBind();

            //ddlWarehouse.Items.Insert(0, "-Select-");
            //ddlWarehouse.Items[0].Value = "0";

            ViewState["sortOrder"] = "";
            ddlInvCat.Items.Insert(0, "All");
            ddlInvCat.Items[0].Value = "0";
            ddlInvSubCat.Items.Insert(0, "All");
            ddlInvSubCat.Items[0].Value = "0";
            ddlInvSubSubCat.Items.Insert(0, "All");
            ddlInvSubSubCat.Items[0].Value = "0";
            ddlInvName.Items.Insert(0, "All");
            ddlInvName.Items[0].Value = "0";
            FillSiteDDL();
            if (RadioButtonList1.SelectedValue == "0")
            {
                pnlInvCat.Visible = true;
                pnlInvName.Visible = false;
                pnlInvDDLname.Visible = false;
                FillddlInvCat();
            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                pnlInvCat.Visible = false;
                pnlInvName.Visible = true;
                pnlInvDDLname.Visible = false;
            }
            else if (RadioButtonList1.SelectedValue == "2")
            {
                pnlInvCat.Visible = false;
                pnlInvName.Visible = false;
                pnlInvDDLname.Visible = true;
                if (ViewState["CSSCNm"].ToString() == "")
                {


                    FillDDlCatScatSScatName();
                }
                else
                {
                    FillDDlCatScatSScatName();
                    ddlCatScSscNameofInv.SelectedIndex = ddlCatScSscNameofInv.Items.IndexOf(ddlCatScSscNameofInv.Items.FindByValue(ViewState["CSSCNm"].ToString()));
                }
            }
            else
            {
                pnlInvCat.Visible = false;
                pnlInvName.Visible = false;
                pnlInvDDLname.Visible = false;
            }
            ModalPopupExtender1.Hide();
            lblInvDetail.Visible = true;
            filter();
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

    protected void FillddlInvCat()
    {
        //string strcat = "SELECT InventeroyCatId,InventoryCatName  FROM  InventoryCategoryMaster where compid= '" +compid +"'";
        string strcat = "SELECT Distinct  InventoryCategoryMaster.InventeroyCatId,InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ddlWarehouse.SelectedValue + "' order by InventoryCatName";
        SqlCommand cmdcat = new SqlCommand(strcat, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);

        ddlInvCat.DataSource = dtcat;
        ddlInvCat.DataTextField = "InventoryCatName";
        ddlInvCat.DataValueField = "InventeroyCatId";
        ddlInvCat.DataBind();
        ddlInvCat.Items.Insert(0, "All");
        ddlInvCat.Items[0].Value = "0";
        //ddlInvCat.AutoPostBack = true;
        object se = new object();
        EventArgs er = new EventArgs();
         ddlInvCat_SelectedIndexChanged(se, er);

    }
    protected void ddlInvCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlInvSubCat.Items.Clear();
        if (Convert.ToInt32(ddlInvCat.SelectedIndex) > 0)
        {
            string strsubcat = "SELECT InventorySubCatId  ,InventorySubCatName ,InventoryCategoryMasterId  FROM InventorySubCategoryMaster " +
                            " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlInvCat.SelectedValue) + " order by InventorySubCatName ";
            SqlCommand cmdsubcat = new SqlCommand(strsubcat, con);
            SqlDataAdapter adpsubcat = new SqlDataAdapter(cmdsubcat);
            DataTable dtsubcat = new DataTable();
            adpsubcat.Fill(dtsubcat);

            ddlInvSubCat.DataSource = dtsubcat;
            ddlInvSubCat.DataTextField = "InventorySubCatName";
            ddlInvSubCat.DataValueField = "InventorySubCatId";
            ddlInvSubCat.DataBind();
            ddlInvSubCat.Items.Insert(0, "All");
            ddlInvSubCat.Items[0].Value = "0";
            filter();
        }
        else
        {
            ddlInvSubCat.Items.Insert(0, "All");
            ddlInvSubCat.Items[0].Value = "0";
        }
        lblMainCat.Text = ddlInvCat.SelectedItem.Text;
        ddlInvSubCat_SelectedIndexChanged(sender, e);

    }
    protected void ddlInvSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlInvSubSubCat.Items.Clear();
        if (Convert.ToInt32(ddlInvSubCat.SelectedIndex) > 0)
        {
            string strsubsubcat = "SELECT InventorySubSubId ,InventorySubSubName  ,InventorySubCatID  FROM  InventoruSubSubCategory " +
                            " where InventorySubCatID=" + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " order by InventorySubSubName ";
            SqlCommand cmdsubsubcat = new SqlCommand(strsubsubcat, con);
            SqlDataAdapter adpsubsubcat = new SqlDataAdapter(cmdsubsubcat);
            DataTable dtsubsubcat = new DataTable();
            adpsubsubcat.Fill(dtsubsubcat);

            ddlInvSubSubCat.DataSource = dtsubsubcat;
            ddlInvSubSubCat.DataTextField = "InventorySubSubName";
            ddlInvSubSubCat.DataValueField = "InventorySubSubId";
            ddlInvSubSubCat.DataBind();
            ddlInvSubSubCat.Items.Insert(0, "All");
            ddlInvSubSubCat.Items[0].Value = "0";
            filter();
        }
        else
        {
            ddlInvSubSubCat.Items.Insert(0, "All");
            ddlInvSubSubCat.Items[0].Value = "0";
        }

        ddlInvSubSubCat_SelectedIndexChanged(sender, e);

    }
    protected void ddlInvSubSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
       ddlInvName.Items.Clear();
        if (Convert.ToInt32(ddlInvSubSubCat.SelectedIndex) > 0)
        {
            string strinvname = "SELECT InventoryMasterId ,Name ,InventoryDetailsId ,InventorySubSubId   ,ProductNo ,InventoryTypeId  FROM InventoryMaster " +
                            " where InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + " and InventoryMaster.MasterActiveStatus=1 order by Name  ";
            SqlCommand cmdinvname = new SqlCommand(strinvname, con);
            SqlDataAdapter adpinvname = new SqlDataAdapter(cmdinvname);
            DataTable dtinvname = new DataTable();
            adpinvname.Fill(dtinvname);

            ddlInvName.DataSource = dtinvname;

            ddlInvName.DataTextField = "Name";
            ddlInvName.DataValueField = "InventoryMasterId";
            ddlInvName.DataBind();
            ddlInvName.Items.Insert(0, "All");
            ddlInvName.Items[0].Value = "0";
            filter();
        }
        else
        {
            ddlInvName.Items.Insert(0, "All");
            ddlInvName.Items[0].Value = "0";
        }
        ddlInvName_SelectedIndexChanged(sender, e);
    }

    protected void filter()
    {
        if (ddlWarehouse.SelectedValue != "0")
        {
            lblCompany.Text = Session["Cname"].ToString();
            lblBusiness.Text = ddlWarehouse.SelectedItem.Text;

            DataTable dtinvids = new DataTable();

            if (RadioButtonList1.SelectedValue == "0")
            {
                dtinvids = (DataTable)(SeachByCat());
                lblMainCat.Text = ddlInvCat.SelectedItem.Text;
                lblSubCat.Text = ddlInvSubCat.SelectedItem.Text;
                lblSubSubCat.Text = ddlInvSubSubCat.SelectedItem.Text;
                lbliname.Text = ddlInvName.SelectedItem.Text;
                lbltimain.Visible = true;
                lbltisub.Visible = true;
                lbltisubsub.Visible = true;
                lbltiname.Visible = true;
                txtSearchInvName.Text = "";
                Label2.Text = "";
            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                if (txtSearchInvName.Text.Length > 0 || txtBarcode.Text.Length > 0 || txtProductNo.Text.Length > 0)
                {
                    dtinvids = (DataTable)(SearchByName());                    
                }
                else
                {
                    lblInvName.Visible = true;                   
                }
                Label2.Text = "Search by : " + txtSearchInvName.Text;

                lblMainCat.Text = "";
                lblSubCat.Text = "";
                lblSubSubCat.Text = "";
                lbliname.Text = "";
                lbltimain.Visible = false;
                lbltisub.Visible = false;
                lbltisubsub.Visible = false;
                lbltiname.Visible = false;
                txtProductNo.Text = "";
            }
            else if (RadioButtonList1.SelectedValue == "2")
            {



                string strDetail = " SELECT     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.InventoryMasterId, " +
                    "  InventoryWarehouseMasterTbl.InventoryDetailsId, " +
                    "  InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.PreferredVendorId,  " +
                    "  InventoryWarehouseMasterTbl.ReorderQuantiy, InventoryWarehouseMasterTbl.NormalOrderQuantity, InventoryWarehouseMasterTbl.ReorderLevel,  " +
                    "  InventoryWarehouseMasterTbl.QtyOnDateStarted, InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.Rate,  " +
                    "  InventoryMaster.Name AS InvName, WareHouseMaster.Name AS WarehouseName, InventoryWarehouseMasterTbl.OpeningQty,  " +
                    "  InventoryWarehouseMasterTbl.OpeningRate, InventoryLocationTbl.InventoryLocationID,  case when InventoryLocationTbl.InventoryLocationName is null then '-NA-' else InventoryLocationTbl.InventoryLocationName end as InventoryLocationName ,  " +
                    "  InventortyRackMasterTbl.InventortyRackID, InventorySiteMasterTbl.InventorySiteID,case when  InventoryLocationTbl.ShelfNumber is null then '-NA-' else  InventoryLocationTbl.ShelfNumber end as   ShelfNumber  , case when  InventoryLocationTbl.Position is null then '-NA-' else InventoryLocationTbl.Position end as Position ,  " +
                    "  CASE WHEN InventortyRackMasterTbl.InventortyRackName IS NULL THEN '-NA-' ELSE InventortyRackMasterTbl.InventortyRackName END AS InventortyRackName,  " +
                    "  CASE WHEN InventorySiteMasterTbl.InventorySiteName IS NULL THEN '-NA-' ELSE InventorySiteMasterTbl.InventorySiteName END AS InventorySiteName,  " +
                    "  InventoryRoomMasterTbl.InventoryRoomID, CASE WHEN InventoryRoomMasterTbl.InventoryRoomName IS NULL  " +
                    "  THEN '-NA-' ELSE InventoryRoomMasterTbl.InventoryRoomName END AS InventoryRoomName, InventoryCategoryMaster.InventeroyCatId,  " +
                    "  InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName,  " +
                    "  InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoryDetails.Description, InventoryDetails.Weight,  " +
                    "  InventoryDetails.UnitTypeId, InventoryMaster.ProductNo, InventoryMaster.MasterActiveStatus " +
                                        " ,  Left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + Left(InventoruSubSubCategory.InventorySubSubName,15) AS CateAndName" +
                    " FROM         InventoryDetails RIGHT OUTER JOIN " +
                    "  InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId LEFT OUTER JOIN " +
                    "  InventoruSubSubCategory LEFT OUTER JOIN " +
                    "  InventorySubCategoryMaster LEFT OUTER JOIN " +
                    "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId ON  " +
                    "  InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON  " +
                    "  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId RIGHT OUTER JOIN " +
                    "  InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                    "  WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId ON " +
                    "  InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId LEFT OUTER JOIN " +
                    "  InventorySiteMasterTbl RIGHT OUTER JOIN " +
                    "  InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID RIGHT OUTER JOIN " +
                    "  InventortyRackMasterTbl ON InventoryRoomMasterTbl.InventoryRoomID = InventortyRackMasterTbl.InventoryRoomID RIGHT OUTER JOIN " +
                    "  InventoryLocationTbl ON InventortyRackMasterTbl.InventortyRackID = InventoryLocationTbl.InventortyRackID ON  " +
                    "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = InventoryLocationTbl.InventoryWHM_Id " +
                    "  where InventoryWarehouseMasterTbl.WareHouseId = '" + ddlWarehouse.SelectedValue + "' " +
                   " and     (InventoryWarehouseMasterTbl.InventoryMasterId = '" + Convert.ToInt32(ddlCatScSscNameofInv.SelectedValue) + " ') AND " +
                   "  (InventoryMaster.MasterActiveStatus = 1)  ";


                SqlCommand cmdDetail = new SqlCommand(strDetail, con);
                SqlDataAdapter adpDetail = new SqlDataAdapter(cmdDetail);
                DataTable dtDetail = new DataTable();
                adpDetail.Fill(dtDetail);
                if (dtDetail.Rows.Count > 0)
                {

                    FillSiteDDL();
                    if (dtDetail.Rows[0]["InventoryLocationID"].ToString() == "" || dtDetail.Rows[0]["InventoryLocationID"].ToString() == null)
                    {
                        ClearDetails();
                        lblInvDetail.Visible = true;
                        lblInvDetail.Text = " Insert Inventory Location Detail '" + dtDetail.Rows[0]["InvName"].ToString() + "' ";
                        ViewState["l"] = null;
                        hdninvExist.Value = "0";
                        Label1.Text = hdninvExist.Value + "-3";
                        ViewState["IWMi"] = Convert.ToInt32(dtDetail.Rows[0]["InventoryWarehouseMasterId"]);
                        locid = 0;
                    }
                    else
                    {
                        string stid = dtDetail.Rows[0]["InventorySiteID"].ToString();
                        string rmid = dtDetail.Rows[0]["InventoryRoomID"].ToString();
                        string rcid = dtDetail.Rows[0]["InventortyRackID"].ToString();
                        ddlsite.SelectedIndex = ddlsite.Items.IndexOf(ddlsite.Items.FindByValue(stid));
                        ddlroomId.SelectedIndex = ddlroomId.Items.IndexOf(ddlroomId.Items.FindByValue(rmid));
                        ddlrackid.SelectedIndex = ddlrackid.Items.IndexOf(ddlrackid.Items.FindByValue(rcid));
                        txtinvlocname.Text = dtDetail.Rows[0]["InventoryLocationName"].ToString();
                        txtsherlnumber.Text = dtDetail.Rows[0]["ShelfNumber"].ToString();
                        txtposition.Text = dtDetail.Rows[0]["Position"].ToString();

                        //txtWeight.Text = dtDetail.Rows[0]["Weight"].ToString();

                        hdninvExist.Value = isintornot(dtDetail.Rows[0]["InventoryLocationID"].ToString()).ToString();
                        Label1.Text = hdninvExist.Value + "5";
                        ViewState["IWMi"] = Convert.ToInt32(dtDetail.Rows[0]["InventoryWarehouseMasterId"]);
                        ViewState["ImLocnId"] = Convert.ToInt32(dtDetail.Rows[0]["InventoryLocationID"]);
                        //lblInvNameFromGrd.Text = dtDetail.Rows[0]["InvName"].ToString();
                        //lblWareshouseFromGrd.Text = dtDetail.Rows[0]["WarehouseName"].ToString();
                        ViewState["l"] = isintornot(dtDetail.Rows[0]["InventoryLocationID"].ToString());
                        locid = isintornot(dtDetail.Rows[0]["InventoryLocationID"].ToString());
                        lblInvDetail.Visible = true;
                        lblInvDetail.Text = " Update Inventory Location Detail '" + dtDetail.Rows[0]["InvName"].ToString() + "' ";
                    }
                }
                else
                {

                }
            }
            else
            {

            }
            
            if (dtinvids.Rows.Count > 0)
            {
                grdInvMasters.DataSource = dtinvids;
                grdInvMasters.DataBind();

                foreach (GridViewRow gdr in grdInvMasters.Rows)
                {
                    Label lblInvMasterId = (Label)gdr.FindControl("lblInvMasterId");
                    Label lblSite = (Label)gdr.FindControl("lblSite");
                    Label lblRoom = (Label)gdr.FindControl("lblRoom");
                    Label lblRack = (Label)gdr.FindControl("lblRack");
                    Label lblLocation = (Label)gdr.FindControl("lblLocation");
                    Label lblShelf = (Label)gdr.FindControl("lblShelf");
                    Label lblPosition = (Label)gdr.FindControl("lblPosition");

                    DataTable dtsit = new DataTable();
                    dtsit = (DataTable)(dtsite(Convert.ToInt32(lblInvMasterId.Text)));
                    if (dtsit.Rows.Count > 0)
                    {
                        string invsite = "";
                        string invroom = "";
                        string invrack = "";
                        string invlocation = "";
                        string invshelf = "";
                        string invposition = "";
                        foreach (DataRow dr in dtsit.Rows)
                        {
                            invsite = invsite + dr["InventorySiteName"] + ", ";
                            invroom = invroom + dr["InventoryRoomName"] + ", ";
                            invrack = invrack + dr["InventortyRackName"] + ", ";
                            invlocation = invlocation + dr["InventoryLocationName"] + ", ";
                            invshelf = invshelf + dr["ShelfNumber"] + ", ";
                            invposition = invposition + dr["Position"] + ", ";
                        }
                        lblSite.Text = invsite;
                        string st = lblSite.Text.Substring(0, lblSite.Text.Length - 2);
                        lblSite.Text = st;

                        lblRoom.Text = invroom;
                        string stroom = lblRoom.Text.Substring(0, lblRoom.Text.Length - 2);
                        lblRoom.Text = stroom;

                        lblRack.Text = invrack;
                        string strack = lblRack.Text.Substring(0, lblRack.Text.Length - 2);
                        lblRack.Text = strack;

                        lblLocation.Text = invlocation;
                        string strlocation = lblLocation.Text.Substring(0, lblLocation.Text.Length - 2);
                        lblLocation.Text = strlocation;

                        lblShelf.Text = invshelf;
                        string strshelf = lblShelf.Text.Substring(0, lblShelf.Text.Length - 2);
                        lblShelf.Text = strshelf;

                        lblPosition.Text = invposition;
                        string strposition = lblPosition.Text.Substring(0, lblPosition.Text.Length - 2);
                        lblPosition.Text = strposition;
                    }
                }
            }

            else
            {
                grdInvMasters.DataSource = null;
                grdInvMasters.DataBind();
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please Select Store Location";
        }
    }
    public DataTable dtsite(int invmasterid)
    {
        SqlDataAdapter adpsite = new SqlDataAdapter("select InventoryLocationTbl.*,InventorySiteMasterTbl.InventorySiteName,InventorySiteMasterTbl.InventorySiteID,  InventoryRoomMasterTbl.InventoryRoomID,InventoryRoomMasterTbl.InventorySiteID,InventoryRoomMasterTbl.InventoryRoomName,  InventortyRackMasterTbl.InventortyRackID,InventortyRackMasterTbl.InventortyRackName,InventortyRackMasterTbl.InventoryRoomID,  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,InventoryWarehouseMasterTbl.InventoryMasterId   from InventoryLocationTbl  left join InventorySiteMasterTbl on InventoryLocationTbl.InvSitemasterId = InventorySiteMasterTbl.InventorySiteID  left join InventoryRoomMasterTbl on InventoryLocationTbl.InvRoomId = InventoryRoomMasterTbl.InventoryRoomID  left join InventortyRackMasterTbl on InventoryLocationTbl.InventortyRackID = InventortyRackMasterTbl.InventortyRackID  inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = InventoryLocationTbl.InventoryWHM_Id  		where InventoryWarehouseMasterTbl.InventoryMasterId = '" + invmasterid + "'", con);
        //SqlDataAdapter adpsite = new SqlDataAdapter("select InventoryLocationTbl.*,InventorySiteMasterTbl.InventorySiteName,InventorySiteMasterTbl.InventorySiteID,  InventoryRoomMasterTbl.InventoryRoomID,InventoryRoomMasterTbl.InventorySiteID,InventoryRoomMasterTbl.InventoryRoomName,  InventortyRackMasterTbl.InventortyRackID,InventortyRackMasterTbl.InventortyRackName,InventortyRackMasterTbl.InventoryRoomID,  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,InventoryWarehouseMasterTbl.InventoryMasterId  from InventoryWarehouseMasterTbl left outer  join InventorySiteMasterTbl right outer join InventoryRoomMasterTbl on InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID right outer join InventortyRackMasterTbl on InventoryRoomMasterTbl.InventoryRoomID = InventortyRackMasterTbl.InventoryRoomID right outer join InventoryLocationTbl on InventortyRackMasterTbl.InventortyRackID = InventoryLocationTbl.InventortyRackID on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = InventoryLocationTbl.InventoryWHM_Id     where InventoryWarehouseMasterTbl.InventoryMasterId = '" + invmasterid + "'", con);
        DataTable dt = new DataTable();
        adpsite.Fill(dt);
        return dt;
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        if (ddlWarehouse.SelectedIndex > 0)
        {

            DataTable dtinvids = new DataTable();

            if (RadioButtonList1.SelectedValue == "0")
            {
                dtinvids = (DataTable)(SeachByCat());

            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                if (txtSearchInvName.Text.Length > 0 || txtBarcode.Text.Length > 0 || txtProductNo.Text.Length > 0)
                {
                    dtinvids = (DataTable)(SearchByName());

                    txtProductNo.Text = "";
                }
                else
                {
                    lblInvName.Visible = true;
                    //lblInvName.Text = "plese input InvntoryName atleast";

                }
            }
            else if (RadioButtonList1.SelectedValue == "2")
            {


                // FillDDlCatScatSScatName();
                string strDetail = " SELECT     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.InventoryMasterId, " +
                    "  InventoryWarehouseMasterTbl.InventoryDetailsId, " +
                    "  InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.PreferredVendorId,  " +
                    "  InventoryWarehouseMasterTbl.ReorderQuantiy, InventoryWarehouseMasterTbl.NormalOrderQuantity, InventoryWarehouseMasterTbl.ReorderLevel,  " +
                    "  InventoryWarehouseMasterTbl.QtyOnDateStarted, InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.Rate,  " +
                    "  InventoryMaster.Name AS InvName, WareHouseMaster.Name AS WarehouseName, InventoryWarehouseMasterTbl.OpeningQty,  " +
                    "  InventoryWarehouseMasterTbl.OpeningRate, InventoryLocationTbl.InventoryLocationID,  case when InventoryLocationTbl.InventoryLocationName is null then '-NA-' else InventoryLocationTbl.InventoryLocationName end as InventoryLocationName ,  " +
                    "  InventortyRackMasterTbl.InventortyRackID, InventorySiteMasterTbl.InventorySiteID,case when  InventoryLocationTbl.ShelfNumber is null then '-NA-' else  InventoryLocationTbl.ShelfNumber end as   ShelfNumber  , case when  InventoryLocationTbl.Position is null then '-NA-' else InventoryLocationTbl.Position end as Position ,  " +
                    "  CASE WHEN InventortyRackMasterTbl.InventortyRackName IS NULL THEN '-NA-' ELSE InventortyRackMasterTbl.InventortyRackName END AS InventortyRackName,  " +
                    "  CASE WHEN InventorySiteMasterTbl.InventorySiteName IS NULL THEN '-NA-' ELSE InventorySiteMasterTbl.InventorySiteName END AS InventorySiteName,  " +
                    "  InventoryRoomMasterTbl.InventoryRoomID, CASE WHEN InventoryRoomMasterTbl.InventoryRoomName IS NULL  " +
                    "  THEN '-NA-' ELSE InventoryRoomMasterTbl.InventoryRoomName END AS InventoryRoomName, InventoryCategoryMaster.InventeroyCatId,  " +
                    "  InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName,  " +
                    "  InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoryDetails.Description, InventoryDetails.Weight,  " +
                    "  InventoryDetails.UnitTypeId, InventoryMaster.ProductNo, InventoryMaster.MasterActiveStatus " +
                                        " ,  Left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + Left(InventoruSubSubCategory.InventorySubSubName,15) AS CateAndName" +
                    " FROM         InventoryDetails RIGHT OUTER JOIN " +
                    "  InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId LEFT OUTER JOIN " +
                    "  InventoruSubSubCategory LEFT OUTER JOIN " +
                    "  InventorySubCategoryMaster LEFT OUTER JOIN " +
                    "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId ON  " +
                    "  InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON  " +
                    "  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId RIGHT OUTER JOIN " +
                    "  InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                    "  WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId ON " +
                    "  InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId LEFT OUTER JOIN " +
                    "  InventorySiteMasterTbl RIGHT OUTER JOIN " +
                    "  InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID RIGHT OUTER JOIN " +
                    "  InventortyRackMasterTbl ON InventoryRoomMasterTbl.InventoryRoomID = InventortyRackMasterTbl.InventoryRoomID RIGHT OUTER JOIN " +
                    "  InventoryLocationTbl ON InventortyRackMasterTbl.InventortyRackID = InventoryLocationTbl.InventortyRackID ON  " +
                    "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = InventoryLocationTbl.InventoryWHM_Id " +
                    "  where InventoryWarehouseMasterTbl.WareHouseId = '" + ddlWarehouse.SelectedValue + "' " +
                   " and     (InventoryWarehouseMasterTbl.InventoryMasterId = '" + Convert.ToInt32(ddlCatScSscNameofInv.SelectedValue) + " ') AND " +
                   "  (InventoryMaster.MasterActiveStatus = 1)  ";


                SqlCommand cmdDetail = new SqlCommand(strDetail, con);
                SqlDataAdapter adpDetail = new SqlDataAdapter(cmdDetail);
                DataTable dtDetail = new DataTable();
                adpDetail.Fill(dtDetail);
                if (dtDetail.Rows.Count > 0)
                {

                    FillSiteDDL();
                    if (dtDetail.Rows[0]["InventoryLocationID"].ToString() == "" || dtDetail.Rows[0]["InventoryLocationID"].ToString() == null)
                    {
                        ClearDetails();
                        lblInvDetail.Visible = true;
                        lblInvDetail.Text = " Insert Inventory Location Detail '" + dtDetail.Rows[0]["InvName"].ToString() + "' ";
                        ViewState["l"] = null;
                        hdninvExist.Value = "0";
                        Label1.Text = hdninvExist.Value + "-3";
                        ViewState["IWMi"] = Convert.ToInt32(dtDetail.Rows[0]["InventoryWarehouseMasterId"]);
                        locid = 0;
                    }
                    else
                    {
                        string stid = dtDetail.Rows[0]["InventorySiteID"].ToString();
                        string rmid = dtDetail.Rows[0]["InventoryRoomID"].ToString();
                        string rcid = dtDetail.Rows[0]["InventortyRackID"].ToString();
                        ddlsite.SelectedIndex = ddlsite.Items.IndexOf(ddlsite.Items.FindByValue(stid));
                        ddlroomId.SelectedIndex = ddlroomId.Items.IndexOf(ddlroomId.Items.FindByValue(rmid));
                        ddlrackid.SelectedIndex = ddlrackid.Items.IndexOf(ddlrackid.Items.FindByValue(rcid));
                        txtinvlocname.Text = dtDetail.Rows[0]["InventoryLocationName"].ToString();
                        txtsherlnumber.Text = dtDetail.Rows[0]["ShelfNumber"].ToString();
                        txtposition.Text = dtDetail.Rows[0]["Position"].ToString();

                        //txtWeight.Text = dtDetail.Rows[0]["Weight"].ToString();

                        hdninvExist.Value = isintornot(dtDetail.Rows[0]["InventoryLocationID"].ToString()).ToString();
                        Label1.Text = hdninvExist.Value + "5";
                        ViewState["IWMi"] = Convert.ToInt32(dtDetail.Rows[0]["InventoryWarehouseMasterId"]);
                        ViewState["ImLocnId"] = Convert.ToInt32(dtDetail.Rows[0]["InventoryLocationID"]);
                        //lblInvNameFromGrd.Text = dtDetail.Rows[0]["InvName"].ToString();
                        //lblWareshouseFromGrd.Text = dtDetail.Rows[0]["WarehouseName"].ToString();
                        ViewState["l"] = isintornot(dtDetail.Rows[0]["InventoryLocationID"].ToString());
                        locid = isintornot(dtDetail.Rows[0]["InventoryLocationID"].ToString());
                        lblInvDetail.Visible = true;
                        lblInvDetail.Text = " Update Inventory Location Detail '" + dtDetail.Rows[0]["InvName"].ToString() + "' ";
                    }
                }
                else
                {

                }
            }
            else
            {

            }
            if (dtinvids.Rows.Count > 0)
            {
                grdInvMasters.DataSource = dtinvids;
                grdInvMasters.DataBind();

            }
            else
            {
                grdInvMasters.DataSource = null;
                grdInvMasters.DataBind();
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = " Please Select Store Location";
        }
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            pnlInvCat.Visible = true;
            pnlInvName.Visible = false;
            pnlInvDDLname.Visible = false;
            FillddlInvCat();

            filter();
            clearcontrolsdetail();

        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            pnlInvCat.Visible = false;
            pnlInvName.Visible = true;
            pnlInvDDLname.Visible = false;

            filter();
            clearcontrolsdetail();

        }
        else if (RadioButtonList1.SelectedValue == "2")
        {
            pnlInvCat.Visible = false;
            pnlInvName.Visible = false;
            pnlInvDDLname.Visible = true;


            if (ViewState["CSSCNm"].ToString() == "")
            {


                FillDDlCatScatSScatName();
            }
            else
            {
                FillDDlCatScatSScatName();
                ddlCatScSscNameofInv.SelectedIndex = ddlCatScSscNameofInv.Items.IndexOf(ddlCatScSscNameofInv.Items.FindByValue(ViewState["CSSCNm"].ToString()));
            }

            filter();
            clearcontrolsdetail();
        }
        else
        {
            pnlInvCat.Visible = false;
            pnlInvName.Visible = false;
            pnlInvDDLname.Visible = false;


            filter();
            clearcontrolsdetail();
        }
    }
    protected void FillDDlCatScatSScatName()
    {

        string strforall1 = "SELECT     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryMaster.Name AS InventoryName, WareHouseMaster.WareHouseId, " +
               "   WareHouseMaster.Name AS WarehouseName, InventoryMaster.InventoryMasterId, InventoryDetails.Inventory_Details_Id, InventoryDetails.Description, " +
               "   InventoryDetails.Weight, InventoryDetails.UnitTypeId, InventoryBarcodeMaster.InventoryBacodeMasterId, InventoryBarcodeMaster.Barcode, " +
               "   InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, " +
               "   InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName,  " +
               "   InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.PreferredVendorId, InventoryWarehouseMasterTbl.ReorderQuantiy, " +
               "   InventoryWarehouseMasterTbl.NormalOrderQuantity, InventoryWarehouseMasterTbl.ReorderLevel, InventoryWarehouseMasterTbl.QtyOnDateStarted,  " +
               "   InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.Rate, LEFT(InventoryCategoryMaster.InventoryCatName, 8) " +
               "   + ' : ' + LEFT(InventorySubCategoryMaster.InventorySubCatName, 8) + ' : ' + LEFT(InventoruSubSubCategory.InventorySubSubName, 8)  " +
               "   + ' : ' + InventoryMaster.Name AS CatScSscName " +
               "   FROM         WareHouseMaster RIGHT OUTER JOIN " +
               "   InventoryWarehouseMasterTbl ON WareHouseMaster.WareHouseId = InventoryWarehouseMasterTbl.WareHouseId LEFT OUTER JOIN " +
               "   InventoryCategoryMaster RIGHT OUTER JOIN " +
               "   InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
               "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
               "   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId LEFT OUTER JOIN " +
               "   InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN " +
               "   InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id ON  " +
               "   InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId " +
               "   Where InventoryWarehouseMasterTbl.WareHouseId = '" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' ";
        //string strforall1 = " SELECT Distinct    InventoryMaster.Name AS InventoryName, InventoryMaster.InventoryMasterId, InventoryDetails.Inventory_Details_Id, InventoryDetails.Description, " +
        //           "   InventoryDetails.Weight, InventoryDetails.UnitTypeId, InventoryBarcodeMaster.InventoryBacodeMasterId, InventoryBarcodeMaster.Barcode, " +
        //           "   InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, " +
        //           "   InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName,  " +
        //           "   LEFT(InventoryCategoryMaster.InventoryCatName, 8) + ' : ' + LEFT(InventorySubCategoryMaster.InventorySubCatName, 8) " +
        //           "   + ' : ' + LEFT(InventoruSubSubCategory.InventorySubSubName, 8) + ' : ' + InventoryMaster.Name AS CatScSscName " +
        //           " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
        //           "   InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
        //           "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
        //           "   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId LEFT OUTER JOIN " +
        //           "   InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN " +
        //           "   InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id " +
        //          " WHERE     (InventoryMaster.MasterActiveStatus = 1) and InventoryCategoryMaster.compid='"+Session["comid"]+"' " +
        //          " ORDER BY InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubName, InventoryName ";

        SqlCommand cmdForAll1 = new SqlCommand(strforall1, con);
        SqlDataAdapter adpForAll1 = new SqlDataAdapter(cmdForAll1);
        DataTable dtforall1 = new DataTable();
        adpForAll1.Fill(dtforall1);
        if (dtforall1.Rows.Count > 0)
        {
            ddlCatScSscNameofInv.DataSource = dtforall1;
            ddlCatScSscNameofInv.DataTextField = "CatScSscName";
            ddlCatScSscNameofInv.DataValueField = "InventoryMasterId";
            ddlCatScSscNameofInv.DataBind();
            //if (ddlCatScSscNameofInv.SelectedIndex > 0)
            //{

            //}
        }


    }


    public DataTable SeachByCat()
    {

        //string strinv = " SELECT    InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.InventoryMasterId, " +
        //            "  InventoryWarehouseMasterTbl.InventoryDetailsId, " +
        //            "  InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.PreferredVendorId,  " +
        //            "  InventoryWarehouseMasterTbl.ReorderQuantiy, InventoryWarehouseMasterTbl.NormalOrderQuantity, InventoryWarehouseMasterTbl.ReorderLevel,  " +
        //            "  InventoryWarehouseMasterTbl.QtyOnDateStarted, InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.Rate,  " +
        //            "  InventoryMaster.Name AS InvName, WareHouseMaster.Name AS WarehouseName, InventoryWarehouseMasterTbl.OpeningQty,  " +
        //            "  InventoryWarehouseMasterTbl.OpeningRate, InventoryLocationTbl.InventoryLocationID,  case when InventoryLocationTbl.InventoryLocationName is null then '-NA-' else InventoryLocationTbl.InventoryLocationName end as InventoryLocationName ,  " +
        //            "  InventortyRackMasterTbl.InventortyRackID, InventorySiteMasterTbl.InventorySiteID, case when  InventoryLocationTbl.ShelfNumber is null then '-NA-' else  InventoryLocationTbl.ShelfNumber end as   ShelfNumber  , case when  InventoryLocationTbl.Position is null then '-NA-' else InventoryLocationTbl.Position end as Position ,  " +
        //            "  CASE WHEN InventortyRackMasterTbl.InventortyRackName IS NULL THEN '-NA-' ELSE InventortyRackMasterTbl.InventortyRackName END AS InventortyRackName,  " +
        //            "  CASE WHEN InventorySiteMasterTbl.InventorySiteName IS NULL THEN '-NA-' ELSE InventorySiteMasterTbl.InventorySiteName END AS InventorySiteName,  " +
        //            "  InventoryRoomMasterTbl.InventoryRoomID, CASE WHEN InventoryRoomMasterTbl.InventoryRoomName IS NULL  " +
        //            "  THEN '-NA-' ELSE InventoryRoomMasterTbl.InventoryRoomName END AS InventoryRoomName, InventoryCategoryMaster.InventeroyCatId,  " +
        //            "  InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName,  " +
        //            "  InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoryDetails.Description, InventoryDetails.Weight,  " +
        //            "  InventoryDetails.UnitTypeId, InventoryMaster.ProductNo, InventoryMaster.MasterActiveStatus " +
        //            " ,  Left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + Left(InventoruSubSubCategory.InventorySubSubName,15) AS CateAndName" + 
        //            " FROM         InventoryDetails RIGHT OUTER JOIN " +
        //            "  InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId LEFT OUTER JOIN " +
        //            "  InventoruSubSubCategory LEFT OUTER JOIN " +
        //            "  InventorySubCategoryMaster LEFT OUTER JOIN " +
        //            "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId ON  " +
        //            "  InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON  " +
        //            "  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId RIGHT OUTER JOIN " +
        //            "  InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
        //            "  WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId ON " +
        //            "  InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId LEFT OUTER JOIN " +
        //            "  InventorySiteMasterTbl RIGHT OUTER JOIN " +
        //            "  InventoryRoomMasterTbl ON InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID RIGHT OUTER JOIN " +
        //            "  InventortyRackMasterTbl ON InventoryRoomMasterTbl.InventoryRoomID = InventortyRackMasterTbl.InventoryRoomID RIGHT OUTER JOIN " +
        //            "  InventoryLocationTbl ON InventortyRackMasterTbl.InventortyRackID = InventoryLocationTbl.InventortyRackID ON  " +
        //            "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = InventoryLocationTbl.InventoryWHM_Id " +
        //            "  where InventoryWarehouseMasterTbl.WareHouseId = '" + ddlWarehouse.SelectedValue + "' ";

        string strinv = "SELECT    InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,InventoryWarehouseMasterTbl.InventoryDetailsId,InventoryWarehouseMasterTbl.InventoryMasterId,InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active," +
                        "InventoryWarehouseMasterTbl.Rate,WareHouseMaster.Name AS WarehouseName,InventoryDetails.Description," +
                        "InventoryMaster.Name +' : '+ cast(InventoryWarehouseMasterTbl.Weight as nvarchar) +' '+ UnitTypeMaster.Name AS InvName,InventoryMaster.ProductNo,InventoryMaster.MasterActiveStatus , Left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + Left(InventoruSubSubCategory.InventorySubSubName,15) AS CateAndName " +
                        "FROM InventoryDetails RIGHT OUTER JOIN   InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId LEFT OUTER JOIN   InventoruSubSubCategory LEFT OUTER JOIN   InventorySubCategoryMaster LEFT OUTER JOIN   InventoryCategoryMaster " +
                        "ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId ON    InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON    InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId RIGHT OUTER JOIN   InventoryWarehouseMasterTbl LEFT OUTER JOIN   WareHouseMaster " +
                        "ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId ON   InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId left outer join UnitTypeMaster ON InventoryWarehouseMasterTbl.UnitTypeId = UnitTypeMaster.UnitTypeId " +
                        "where InventoryWarehouseMasterTbl.WareHouseId = '" + ddlWarehouse.SelectedValue + "' ";
        string strInvId = "";
        string strInvsubsubCatId = "";
        string strInvsubcatid = "";
        string strInvCatid = "";
        // string strInvBySerchId = "";
        //if (txtSearchInvName.Text.Length <= 0)
        //{
        if (ddlInvCat.SelectedIndex > 0)
        {
            if (ddlInvSubCat.SelectedIndex > 0)
            {
                if (ddlInvSubSubCat.SelectedIndex > 0)
                {
                    if (ddlInvName.SelectedIndex > 0)
                    {
                        strInvId = " and  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

                    }
                    else
                    {
                        strInvsubsubCatId = " and InventoruSubSubCategory.InventorySubSubId=" + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "";
                    }
                }
                else
                {
                    strInvsubcatid = " and InventorySubCategoryMaster.InventorySubCatId = " + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " ";

                }

            }
            else
            {
                strInvCatid = " and InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

                //strInvId = "where  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

            }
        }
        else
        {
            //strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

        }


        string mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + " and InventoryMaster.MasterActiveStatus=1  order by CateAndName, InvName";//+ strInvBySerchId // InventoryMaster.Name ";


        SqlCommand cmdcat = new SqlCommand(mainStringCat, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);



        return dtcat;

    }

    public DataTable SearchByName()
    {

        string strinvMainName = "SELECT    InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.InventoryMasterId,InventoryWarehouseMasterTbl.InventoryDetailsId,   InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active, " +
                               "InventoryWarehouseMasterTbl.Rate,InventoryMaster.Name,InventoryMaster.Name +' : '+ cast(InventoryWarehouseMasterTbl.Weight as nvarchar) +' '+ UnitTypeMaster.Name AS InvName, WareHouseMaster.Name AS WarehouseName, InventoryWarehouseMasterTbl.OpeningRate, " +
                               "InventoryDetails.Description,InventoryMaster.ProductNo, InventoryMaster.MasterActiveStatus,Left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + Left(InventoruSubSubCategory.InventorySubSubName,15) AS CateAndName,InventoryBarcodeMaster.Barcode FROM         InventoryDetails RIGHT OUTER JOIN   InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId inner join InventoryBarcodeMaster on InventoryBarcodeMaster.InventoryMaster_id = InventoryMaster.InventoryMasterId " +
                               "LEFT OUTER JOIN   InventoruSubSubCategory LEFT OUTER JOIN   InventorySubCategoryMaster LEFT OUTER JOIN   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId ON    InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON    InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId RIGHT OUTER JOIN   InventoryWarehouseMasterTbl LEFT OUTER JOIN   WareHouseMaster " +
                               "ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId ON   InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId left outer join UnitTypeMaster ON InventoryWarehouseMasterTbl.UnitTypeId = UnitTypeMaster.UnitTypeId   " +
                               " where InventoryWarehouseMasterTbl.WareHouseId = '" + ddlWarehouse.SelectedValue + "' and ((InventoryMaster.Name like '%" + txtSearchInvName.Text.Replace("'", "''") + "%') or (InventoryMaster.ProductNo = '%" + txtSearchInvName.Text.Replace("'", "''") + "%') or (InventoryBarcodeMaster.Barcode = '%" + txtSearchInvName.Text.Replace("'", "''") + "%'))  and InventoryMaster.MasterActiveStatus=1 order by CateAndName, InvName ";

        SqlCommand cmdinvname = new SqlCommand(strinvMainName, con);
        SqlDataAdapter adpinvname = new SqlDataAdapter(cmdinvname);
        DataTable dtinvname = new DataTable();
        adpinvname.Fill(dtinvname);
        txtBarcode.Text = "";
        txtProductNo.Text = "";
        return dtinvname;

        //string str23invname1 = "SELECT    InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.InventoryMasterId,InventoryWarehouseMasterTbl.InventoryDetailsId,   InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active, "+
        //                       "InventoryWarehouseMasterTbl.Rate,InventoryMaster.Name,InventoryMaster.Name +' : '+ cast(InventoryWarehouseMasterTbl.Weight as nvarchar) +' '+ UnitTypeMaster.Name AS InvName, WareHouseMaster.Name AS WarehouseName, InventoryWarehouseMasterTbl.OpeningRate,  "+                               
        //                       "InventoryDetails.Description,InventoryMaster.ProductNo, InventoryMaster.MasterActiveStatus,Left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + Left(InventoruSubSubCategory.InventorySubSubName,15) AS CateAndName FROM         InventoryDetails RIGHT OUTER JOIN   InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId "+
        //                       "LEFT OUTER JOIN   InventoruSubSubCategory LEFT OUTER JOIN   InventorySubCategoryMaster LEFT OUTER JOIN   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId ON    InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON    InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId RIGHT OUTER JOIN   InventoryWarehouseMasterTbl LEFT OUTER JOIN   WareHouseMaster "+
        //                       "ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId ON   InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId left outer join UnitTypeMaster ON InventoryWarehouseMasterTbl.UnitTypeId = UnitTypeMaster.UnitTypeId   "+
        //                       " where InventoryWarehouseMasterTbl.WareHouseId = '" + ddlWarehouse.SelectedValue + "' and  "+
        //                        "  (InventoryMaster.Name like '%" + txtSearchInvName.Text.Replace("'", "''") + "%') and InventoryMaster.MasterActiveStatus=1 ";
        //string strbarcode = "";
        //string strproductno = "";


        //if (txtBarcode.Text.Length > 0)
        //{
        //    strbarcode = " or (InventoryBarcodeMaster.Barcode='" + txtBarcode.Text + "')";

        //}
        //if (txtProductNo.Text.Length > 0)
        //{
        //    strproductno = " or (InventoryMaster.ProductNo='" + txtProductNo.Text + "')";
        //}
        //string str23invname = str23invname1 + strbarcode + strproductno;


        //SqlCommand cmd23invname = new SqlCommand(str23invname, con);
        //SqlDataAdapter adp23invname = new SqlDataAdapter(cmd23invname);
        //DataTable dt23invname = new DataTable();
        //adp23invname.Fill(dt23invname);

        //string strIdinvname = "";
        //string strInvAllIdsinvname = "";
        //string strtempinvname = "";
        //string strInvBySerchIdinvname = "";
        //string strInvBySerchIdinvname23 = "";
        //string strInvBySerchIdinvname2 = "";
        //if (dt23invname.Rows.Count > 0)
        //{

        //    foreach (DataRow dtrrr in dt23invname.Rows)
        //    {
        //        strIdinvname = dtrrr["InventoryMasterId"].ToString();
        //        strInvAllIdsinvname = strIdinvname + "," + strInvAllIdsinvname;
        //        strtempinvname = strInvAllIdsinvname.Substring(0, (strInvAllIdsinvname.Length - 1));
        //    }

        //    strInvBySerchIdinvname = " and InventoryMaster.InventoryMasterId in (" + strtempinvname + ") and InventoryMaster.MasterActiveStatus=1 ";

        //}
        //else
        //{
        //    txtProductNo.Text = txtSearchInvName.Text;



        //    string str23invname12 = "SELECT    InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.InventoryMasterId,InventoryWarehouseMasterTbl.InventoryDetailsId,   InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active, "+
        //                       "InventoryWarehouseMasterTbl.Rate,InventoryMaster.Name,InventoryMaster.Name +' : '+ cast(InventoryWarehouseMasterTbl.Weight as nvarchar) +' '+ UnitTypeMaster.Name AS InvName, WareHouseMaster.Name AS WarehouseName, InventoryWarehouseMasterTbl.OpeningRate, "+
        //                       "InventoryDetails.Description,InventoryMaster.ProductNo, InventoryMaster.MasterActiveStatus,Left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + Left(InventoruSubSubCategory.InventorySubSubName,15) AS CateAndName FROM         InventoryDetails RIGHT OUTER JOIN   InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId "+
        //                       "LEFT OUTER JOIN   InventoruSubSubCategory LEFT OUTER JOIN   InventorySubCategoryMaster LEFT OUTER JOIN   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId ON    InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON    InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId RIGHT OUTER JOIN   InventoryWarehouseMasterTbl LEFT OUTER JOIN   WareHouseMaster "+
        //                       "ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId ON   InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId  left outer join UnitTypeMaster ON InventoryWarehouseMasterTbl.UnitTypeId = UnitTypeMaster.UnitTypeId   "+
        //                       " where InventoryWarehouseMasterTbl.WareHouseId = '" + ddlWarehouse.SelectedValue + "' and  "+
        //                       " (InventoryMaster.ProductNo = '%" + txtProductNo.Text.Replace("'", "''") + "%') and InventoryMaster.MasterActiveStatus=1 ";

        //    SqlCommand cmd23invname2 = new SqlCommand(str23invname12, con);
        //    SqlDataAdapter adp23invname2 = new SqlDataAdapter(cmd23invname2);
        //    DataTable dt23invname2 = new DataTable();
        //    adp23invname2.Fill(dt23invname2);

        //    string strIdinvname2 = "";
        //    string strInvAllIdsinvname2 = "";
        //    string strtempinvname2 = "";

        //    if (dt23invname2.Rows.Count > 0)
        //    {

        //        foreach (DataRow dtrrr2 in dt23invname2.Rows)
        //        {
        //            strIdinvname2 = dtrrr2["InventoryMasterId"].ToString();
        //            strInvAllIdsinvname2 = strIdinvname2 + "," + strInvAllIdsinvname2;
        //            strtempinvname2 = strInvAllIdsinvname2.Substring(0, (strInvAllIdsinvname2.Length - 1));
        //        }

        //        strInvBySerchIdinvname2 = " and InventoryMaster.InventoryMasterId in (" + strtempinvname2 + ") and InventoryMaster.MasterActiveStatus=1 ";

        //    }
        //    else
        //    {

        //        txtBarcode.Text = txtSearchInvName.Text;
        //        string str23invname123 = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.InventorySubSubId, InventoryMaster.Name, InventoryMaster.ProductNo, " +
        //                 " InventoruSubSubCategory.InventorySubSubName, InventoryDetails.Description, InventoryDetails.Inventory_Details_Id, InventoryDetails.QtyOnHand, " +
        //                 " InventoryDetails.Rate, InventoryDetails.Weight, InventoruSubSubCategory.InventorySubSubId AS Expr1, InventorySizeMaster.Width, InventorySizeMaster.Height,  " +
        //                 " InventorySizeMaster.length AS Length, InventoryBarcodeMaster.Barcode, InventoryMeasurementUnit.Unit, CASE WHEN InventoryMeasurementUnit.UnitType IS NULL  " +
        //                 " THEN '1' ELSE InventoryMeasurementUnit.UnitType END AS UnitType " +
        //                 " FROM         InventoryMaster INNER JOIN " +
        //                 " InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id INNER JOIN " +
        //                 " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId LEFT OUTER JOIN " +
        //                 " InventoryMeasurementUnit ON InventoryMaster.InventoryMasterId = InventoryMeasurementUnit.InventoryMasterId LEFT OUTER JOIN " +
        //                 " InventorySizeMaster ON InventoryMaster.InventoryMasterId = InventorySizeMaster.InventoryMasterId LEFT OUTER JOIN " +
        //                 " InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id " +
        //                " WHERE     (InventoryBarcodeMaster.Barcode = '%" + txtBarcode.Text.Replace("'", "''") + "%') and InventoryMaster.MasterActiveStatus=1 ";



        //        SqlCommand cmd23invname23 = new SqlCommand(str23invname123, con);
        //        SqlDataAdapter adp23invname23 = new SqlDataAdapter(cmd23invname23);
        //        DataTable dt23invname23 = new DataTable();
        //        adp23invname23.Fill(dt23invname23);

        //        string strIdinvname23 = "";
        //        string strInvAllIdsinvname23 = "";
        //        string strtempinvname23 = "";

        //        if (dt23invname23.Rows.Count > 0)
        //        {

        //            foreach (DataRow dtrrr23 in dt23invname23.Rows)
        //            {
        //                strIdinvname23 = dtrrr23["InventoryMasterId"].ToString();
        //                strInvAllIdsinvname23 = strIdinvname23 + "," + strInvAllIdsinvname23;
        //                strtempinvname23 = strInvAllIdsinvname23.Substring(0, (strInvAllIdsinvname23.Length - 1));
        //            }

        //            strInvBySerchIdinvname23 = " and InventoryMaster.InventoryMasterId in (" + strtempinvname23 + ") and InventoryMaster.MasterActiveStatus=1 ";

        //        }
        //    }



        //}
        //string strmaininvname = strinvMainName + strInvBySerchIdinvname + strInvBySerchIdinvname2 + strInvBySerchIdinvname23;




    }

    protected void grdInvMasters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        
        if (e.CommandName == "Select1")
        {
            int indx = Convert.ToInt32(e.CommandArgument.ToString());
            grdInvMasters.SelectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            
            ModalPopupExtender1.Show();
            GridViewRow gdr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            Label invMid = (Label)gdr.FindControl("lblInvMasterId");
            Label invDid = (Label)gdr.FindControl("lblInvDetailId");
            Label InvName = (Label)gdr.FindControl("lblInvName");
            Label lblInvWMasterId = (Label)gdr.FindControl("lblInvWMasterId");
            ViewState["invDi"] = Convert.ToInt32(invDid.Text);
            ViewState["invMi"] = Convert.ToInt32(invMid.Text);
            ViewState["IWMi"] = Convert.ToInt32(lblInvWMasterId.Text);
            FillSiteDDL();
            ddlsite.Enabled = true;



            lblInvDetail.Text = " Insert Inventory Location Detail " + InvName.Text ;
            fillinvprogrid();
           
        }
    }
    protected int isintornot(string ck)
    {
        int i = 0;
        try
        {
            i = Convert.ToInt32(ck);
            return i;

        }
        catch
        {
            return i;
        }

    }
    protected void ClearDetails()
    {


        ddlsite.SelectedIndex = 0;
        if (ddlroomId.Items[0].Value == "0")
        {
            ddlroomId.SelectedIndex = 0;

        }
        if (ddlrackid.Items[0].Value == "0")
        {
            ddlrackid.SelectedIndex = 0;
        }

        txtinvlocname.Text = "";
        txtsherlnumber.Text = "";
        txtposition.Text = "";


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            string insertLocat = " INSERT INTO InventoryLocationTbl([InventoryLocationName],[InventoryWHM_Id] ,[InventortyRackID] " +
                                 " ,[ShelfNumber],[Position] ,[InvSitemasterId],InvRoomId) " +
                                  " VALUES " +
                            "  ('" + txtinvlocname.Text + "', " +
                           " '" + Convert.ToInt32(ViewState["IWMi"].ToString()) + "',   " +
                           " '" + Convert.ToInt32(ddlrackid.SelectedValue) + "',  " +
                           " '" + txtsherlnumber.Text + "',  " +
                           " '" + txtposition.Text + "' , " +
                           " '" + ddlsite.SelectedValue + "', '"+ ddlroomId.SelectedValue +"') ";

            SqlCommand mycmd = new SqlCommand(insertLocat, con);


            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            mycmd.ExecuteNonQuery();
            con.Close();

            clearcontrolsdetail();
            lblInvDetail.Visible = true;
            lblInvDetail.Text = "Record inserted successfully";



        }
        catch (Exception errrr)
        {
            lblInvDetail.Visible = true;
            lblInvDetail.Text = "Error :" + errrr.Message;

        }
        EventArgs ee = new EventArgs();

        filter();

        ModalPopupExtender1.Show();
        fillinvprogrid();

    }
    protected void fillinvprogrid()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("select InventoryLocationTbl.*,InventorySiteMasterTbl.InventorySiteName,InventorySiteMasterTbl.InventorySiteID,   InventoryRoomMasterTbl.InventoryRoomID,InventoryRoomMasterTbl.InventorySiteID,InventoryRoomMasterTbl.InventoryRoomName,    InventortyRackMasterTbl.InventortyRackID,InventortyRackMasterTbl.InventortyRackName,InventortyRackMasterTbl.InventoryRoomID, 	 InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,InventoryWarehouseMasterTbl.InventoryMasterId 	  from InventoryLocationTbl 	  left join InventorySiteMasterTbl on InventoryLocationTbl.InvSitemasterId = InventorySiteMasterTbl.InventorySiteID 	  left join InventoryRoomMasterTbl on InventoryLocationTbl.InvRoomId = InventoryRoomMasterTbl.InventoryRoomID	  left join InventortyRackMasterTbl on InventoryLocationTbl.InventortyRackID = InventortyRackMasterTbl.InventortyRackID	   inner join InventoryWarehouseMasterTbl 	   on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = InventoryLocationTbl.InventoryWHM_Id          where InventoryLocationTbl.InventoryWHM_Id = '" + ViewState["IWMi"] + "' order by InventorySiteName, InventoryRoomName, InventortyRackName", con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataSource = myDataView;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList1_SelectedIndexChanged(sender, e);

        
        filter();

    }
    protected void ddlCatScSscNameofInv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCatScSscNameofInv.SelectedIndex > 0)
        {

            ViewState["CSSCNm"] = Convert.ToInt32(ddlCatScSscNameofInv.SelectedValue);
            btnSearchGo_Click(sender, e);
        }
    }
    protected void clearcontrolsdetail()
    {
        ClearDetails();
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ClearDetails();
    }
    protected void ImgBtnSearchGo_Click(object sender, ImageClickEventArgs e)
    {
        EventArgs ee = new EventArgs();
        btnSearchGo_Click(sender, ee);
    }
    protected void FillSiteDDL()
    {
        ddlsite.Items.Clear();

        string strsite = "SELECT     InventorySiteID, InventorySiteName " +
                 " FROM         InventorySiteMasterTbl where WarehouseID='" + ddlWarehouse.SelectedValue + "' order by InventorySiteName  ";

        dbss1.FillDDL1(ddlsite, strsite, "InventorySiteID", "InventorySiteName");

        ddlsite.Items.Insert(0, "-Select-");
        ddlsite.Items[0].Value = "0";


        object ss = new object();
        EventArgs ee = new EventArgs();
        ddlsite_SelectedIndexChanged(ss, ee);

    }
    protected void ddlsite_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlroomId.Items.Clear();

        if (ddlsite.SelectedIndex > 0)
        {
            string strroom = "SELECT  InventoryRoomID ,InventoryRoomName ,InventorySiteID " +
                      "  FROM InventoryRoomMasterTbl where InventorySiteID='" + ddlsite.SelectedValue + "' order by InventoryRoomName ";
           
            dbss1.FillDDL1(ddlroomId, strroom, "InventoryRoomID", "InventoryRoomName");
        }
        ddlroomId.Items.Insert(0, "-Select-");
        ddlroomId.Items[0].Value = "0";

        ddlroomId_SelectedIndexChanged(sender, e);
        ModalPopupExtender1.Show();
    }
    protected void ddlroomId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlrackid.Items.Clear();
        if (ddlroomId.SelectedIndex > 0)
        {
            string strrack = "SELECT InventortyRackID  ,InventortyRackName      ,InventoryRoomID " +
                     " FROM InventortyRackMasterTbl where InventoryRoomID ='" + ddlroomId.SelectedValue + "' order by InventortyRackName ";
            dbss1.FillDDL1(ddlrackid, strrack, "InventortyRackID", "InventortyRackName");

        }
        ddlrackid.Items.Insert(0, "-Select-");
        ddlrackid.Items[0].Value = "0";
        ModalPopupExtender1.Show();
    }
    protected void ddlrackid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdInvMasters_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        clearcontrolsdetail();
        ModalPopupExtender1.Hide();
    }
    protected void ddlInvName_SelectedIndexChanged(object sender, EventArgs e)
    {
        filter();
    }
    protected void txtSearchInvName_TextChanged(object sender, EventArgs e)
    {
        filter();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            Panel2.ScrollBars = ScrollBars.None;
            Panel2.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (grdInvMasters.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdInvMasters.Columns[9].Visible = false;
            }

        }
        else
        {

           

            Button4.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["editHide"] != null)
            {
                grdInvMasters.Columns[9].Visible = true;
            }

        }
    }
    protected void grdInvMasters_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        grdInvMasters.PageIndex = e.NewSelectedIndex;
        filter();
    }
    protected void ImageButtondsfdsfdsf123_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Selectlocation")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            //Label invlocationid = (Label)(GridView1.Rows[index].FindControl("invlocationid"));
            ViewState["locationid"] = Convert.ToString(index);
            //SqlDataAdapter adpt = new SqlDataAdapter("select InventoryLocationTbl.*,InventorySiteMasterTbl.InventorySiteName,InventorySiteMasterTbl.InventorySiteID,  InventoryRoomMasterTbl.InventoryRoomID,InventoryRoomMasterTbl.InventorySiteID,InventoryRoomMasterTbl.InventoryRoomName, InventortyRackMasterTbl.InventortyRackName,InventortyRackMasterTbl.InventoryRoomID,  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,InventoryWarehouseMasterTbl.InventoryMasterId  from InventoryWarehouseMasterTbl left outer  join InventorySiteMasterTbl right outer join InventoryRoomMasterTbl on InventorySiteMasterTbl.InventorySiteID = InventoryRoomMasterTbl.InventorySiteID right outer join InventortyRackMasterTbl on InventoryRoomMasterTbl.InventoryRoomID = InventortyRackMasterTbl.InventoryRoomID right outer join InventoryLocationTbl on InventortyRackMasterTbl.InventortyRackID = InventoryLocationTbl.InventortyRackID on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = InventoryLocationTbl.InventoryWHM_Id     where InventoryLocationID = '" + ViewState["locationid"] + "'", con);
            SqlDataAdapter adpt = new SqlDataAdapter("select InventoryLocationTbl.*,InventorySiteMasterTbl.InventorySiteName,InventorySiteMasterTbl.InventorySiteID from  InventoryLocationTbl left outer  join InventorySiteMasterTbl on InventorySiteMasterTbl.InventorySiteID = InventoryLocationTbl.InvSitemasterId  where InventoryLocationID = '" + ViewState["locationid"] + "'", con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                FillSiteDDL();
                if (dt.Rows[0]["InvSitemasterId"].ToString() == "0" || dt.Rows[0]["InvSitemasterId"].ToString() == "1")
                {
                    ddlsite.SelectedIndex = ddlsite.Items.IndexOf(ddlsite.Items.FindByValue(dt.Rows[0]["InventorySiteID"].ToString()));
                }
                else
                {
                    ddlsite.SelectedIndex = ddlsite.Items.IndexOf(ddlsite.Items.FindByValue(dt.Rows[0]["InvSitemasterId"].ToString()));
                }
                object ss = new object();
                EventArgs ee = new EventArgs();
                ddlsite_SelectedIndexChanged(ss, ee);
                if (dt.Rows[0]["InvRoomId"].ToString() != null && dt.Rows[0]["InvRoomId"].ToString() != "")
                {
                    ddlroomId.SelectedIndex = ddlroomId.Items.IndexOf(ddlroomId.Items.FindByValue(dt.Rows[0]["InvRoomId"].ToString()));
                }
                ddlroomId_SelectedIndexChanged(ss, ee);
                ddlrackid.SelectedIndex = ddlrackid.Items.IndexOf(ddlrackid.Items.FindByValue(dt.Rows[0]["InventortyRackID"].ToString()));
                txtinvlocname.Text = dt.Rows[0]["InventoryLocationName"].ToString();
                txtsherlnumber.Text = dt.Rows[0]["ShelfNumber"].ToString();
                txtposition.Text = dt.Rows[0]["Position"].ToString();
                ddlsite.Enabled = false;
            }
            ModalPopupExtender1.Show();
            btnupdate.Visible = true;
            btnSubmit.Visible = false;
            lblInvDetail.Text = " Update Inventory Location Detail";
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (ViewState["locationid"] != null && ViewState["locationid"] != "")
        {

            string strupdatedetail = "UPDATE InventoryLocationTbl " +
                                    " SET InvSitemasterId = '" + ddlsite.SelectedValue + "', " +
                                  " InventortyRackID = '" + Convert.ToInt32(ddlrackid.SelectedValue) + "', " +
                                 " InventoryLocationName =  '" + txtinvlocname.Text + "', " +
                                  " ShelfNumber = '" + txtsherlnumber.Text + "', " +
                                  " Position ='" + txtposition.Text + "', InvRoomId = '"+ ddlroomId.SelectedValue +"'  " +
                                  " WHERE InventoryLocationID='" + ViewState["locationid"] + "'";
            SqlCommand cmdupdateDetailss = new SqlCommand(strupdatedetail, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdupdateDetailss.ExecuteNonQuery();
            con.Close();

            btnupdate.Visible = false;
            btnSubmit.Visible = true;
            clearcontrolsdetail();

            filter();
            fillinvprogrid();
            ModalPopupExtender1.Show();
            lblInvDetail.Text = "Record updated successfully";
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillinvprogrid();
        ModalPopupExtender1.Show();
    }
    protected void grdInvMasters_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdInvMasters.PageIndex = e.NewPageIndex;
        filter();
    }
}
