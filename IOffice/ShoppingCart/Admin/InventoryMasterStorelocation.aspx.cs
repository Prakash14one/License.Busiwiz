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

public partial class ShoppingCart_Admin_InventoryMasterStorelocation : System.Web.UI.Page
{
    string compid;
    decimal oldQtyonHand = 0, oldOpqty = 0, oldOpRt = 0;
    //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);

        /// lblBarcode.Visible = false;
        lblInvName.Visible = false;

        lblInvDetail.Visible = false;
        if (!IsPostBack)
        {
            lblmsg.Text = "";
            Pagecontrol.dypcontrol(Page, page);
            txtqtyondatestarted.Text = System.DateTime.Now.Date.ToShortDateString();
            ViewState["CSSCNm"] = "";
            lblCompany.Text = Session["Cname"].ToString();

            hdninvExist.Value = "0";
            string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + compid + "'and [WareHouseMaster].[Status]='1'";
            SqlCommand cmdwh = new SqlCommand(strwh, con);
            SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            DataTable dtwh = new DataTable();
            adpwh.Fill(dtwh);

            ddlWarehouse.DataSource = dtwh;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "WareHouseId";
            ddlWarehouse.DataBind();

            datefill();


            if (Request.QueryString["invid"] != null && Request.QueryString["Whid"] != null)
            {

                ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByValue(Request.QueryString["Whid"]));

            }
            ddlWarehouse_SelectedIndexChanged(sender, e);
            string strunit = " SELECT UnitTypeId,Name  FROM UnitTypeMaster where UnitTypeId in (1,2,3,4) order by Name";
            SqlCommand cmdunit = new SqlCommand(strunit, con);
            SqlDataAdapter adpunit = new SqlDataAdapter(cmdunit);
            DataTable dtunit = new DataTable();
            adpunit.Fill(dtunit);

            ddllbs.DataSource = dtunit;
            ddllbs.DataTextField = "Name";
            ddllbs.DataValueField = "UnitTypeId";
            ddllbs.DataBind();
            ViewState["sortOrder"] = "";



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
            if (Request.QueryString["invid"] != null && Request.QueryString["Whid"] != null)
            {







                string strDetail = " SELECT     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.InventoryMasterId, InventoryWarehouseMasterTbl.InventoryDetailsId, " +
                        " InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.PreferredVendorId,  " +
                       " InventoryWarehouseMasterTbl.ReorderQuantiy, InventoryWarehouseMasterTbl.NormalOrderQuantity, InventoryWarehouseMasterTbl.ReorderLevel,  " +
                       " InventoryWarehouseMasterTbl.QtyOnDateStarted, InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.Rate,InventoryWarehouseMasterTbl.Weight,InventoryWarehouseMasterTbl.UnitTypeId,     " +
                       " InventoryMaster.Name AS InvName, WareHouseMaster.Name AS WarehouseName ,InventoryWarehouseMasterTbl.OpeningQty,InventoryWarehouseMasterTbl.OpeningRate " +
                       " FROM         InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                       " WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId LEFT OUTER JOIN " +
                       " InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId " +
                       " where InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=" + Convert.ToInt32(Request.QueryString["invid"]) + " " +
                       " and InventoryMaster.MasterActiveStatus=1 and InventoryWarehouseMasterTbl.WareHouseId=" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "  ";

                SqlCommand cmdDetail = new SqlCommand(strDetail, con);
                SqlDataAdapter adpDetail = new SqlDataAdapter(cmdDetail);
                DataTable dtDetail = new DataTable();
                adpDetail.Fill(dtDetail);
                if (dtDetail.Rows.Count > 0)
                {
                    ViewState["invDi"] = dtDetail.Rows[0]["InventoryDetailsId"].ToString();
                    ViewState["invMi"] = dtDetail.Rows[0]["InventoryMasterId"].ToString();

                    txtRate.Text = dtDetail.Rows[0]["Rate"].ToString();
                    txtreorderquantiy.Text = dtDetail.Rows[0]["ReorderQuantiy"].ToString();
                    txtreorderlevel.Text = dtDetail.Rows[0]["ReorderLevel"].ToString();
                    txtqtyondatestarted.Text = dtDetail.Rows[0]["QtyOnDateStarted"].ToString();
                    txtotyonhand.Text = dtDetail.Rows[0]["QtyOnHand"].ToString();
                    // txtnormalorderquantity.Text = dtDetail.Rows[0]["NormalOrderQuantity"].ToString();
                    string pviddd = dtDetail.Rows[0]["PreferredVendorId"].ToString();
                    ddlPreferedVendor.SelectedIndex = ddlPreferedVendor.Items.IndexOf(ddlPreferedVendor.Items.FindByValue(pviddd));
                    ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(dtDetail.Rows[0]["Active"].ToString()));
                    txtOpeingQty.Text = dtDetail.Rows[0]["OpeningQty"].ToString();
                    txtOpeingRAte.Text = dtDetail.Rows[0]["OpeningRate"].ToString();
                    txtWeight.Text = dtDetail.Rows[0]["Weight"].ToString();
                    if (txtWeight.Text == "")
                    {
                        txtWeight.Text = "0";
                    }
                    ddllbs.SelectedIndex = ddllbs.Items.IndexOf(ddllbs.Items.FindByValue(dtDetail.Rows[0]["UnitTypeId"].ToString()));
                    hdninvExist.Value = "1";
                    ViewState["IWMi"] = Convert.ToInt32(dtDetail.Rows[0]["InventoryWarehouseMasterId"]);
                    lblInvDetail.Visible = true;
                    lblInvDetail.Text = " Update Inventory Master " + dtDetail.Rows[0]["InvName"].ToString() + " at " + dtDetail.Rows[0]["WarehouseName"].ToString() + " Location";
                    btnSubmit.Text = "Update";

                }

            }

        }

    }
    //protected void btngo_Click(object sender, EventArgs e)
    //{
    //    FilterInventoryId();

    //}

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
        ddlInvCat.DataSource = getall1();
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
    public DataSet getall1()
    {
        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();


        //string str = "select  InventeroyCatId,  InventoryCatName from InventoryCategorymaster where compid='" + compid  + "'";
        string str = "SELECT Distinct  InventoryCategoryMaster.InventeroyCatId,InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ddlWarehouse.SelectedValue + "' and InventoryCategoryMaster.CatType IS NULL and InventoryCategoryMaster.compid='" + Session["comid"] + "'";
        MyDataAdapter = new SqlDataAdapter(str, con);
        MyDataAdapter.Fill(ds);

        return ds;
    }
    protected void ddlInvCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlInvCat.DataSource = null;
        //ddlInvCat.DataBind();
        ddlInvSubCat.DataSource = null;
        ddlInvSubCat.DataBind();
        ddlInvSubCat.Items.Clear();
        ddlInvSubSubCat.DataSource = null;
        ddlInvSubSubCat.DataBind();
        ddlInvSubSubCat.Items.Clear();
        ddlInvName.DataSource = null;
        ddlInvName.DataBind();
        ddlInvName.Items.Clear();

        if (Convert.ToInt32(ddlInvCat.SelectedIndex) > 0)
        {
            string strsubcat = "SELECT InventorySubCatId  ,InventorySubCatName ,InventoryCategoryMasterId  FROM InventorySubCategoryMaster " +
                            " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlInvCat.SelectedValue) + " and [InventorySubCategoryMaster].[Activestatus]='1'";
            SqlCommand cmdsubcat = new SqlCommand(strsubcat, con);
            SqlDataAdapter adpsubcat = new SqlDataAdapter(cmdsubcat);
            DataTable dtsubcat = new DataTable();
            adpsubcat.Fill(dtsubcat);

            ddlInvSubCat.DataSource = dtsubcat;

            ddlInvSubCat.DataTextField = "InventorySubCatName";
            ddlInvSubCat.DataValueField = "InventorySubCatId";
            ddlInvSubCat.DataBind();

        }
        else
        {
            ddlInvSubCat.DataSource = null;


            ddlInvSubCat.DataBind();
        }
        ddlInvSubCat.Items.Insert(0, "All");
        ddlInvSubCat.Items[0].Value = "0";
        ddlInvSubSubCat.DataSource = null;
        ddlInvSubSubCat.DataBind();
        ddlInvSubCat_SelectedIndexChanged(sender, e);
        //ddlInvSubCat.AutoPostBack = true;
    }
    protected void ddlInvSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlInvSubCat.DataSource = null;
        //ddlInvSubCat.DataBind();
        ddlInvSubSubCat.DataSource = null;
        ddlInvSubSubCat.DataBind();
        ddlInvSubSubCat.Items.Clear();
        ddlInvName.DataSource = null;
        ddlInvName.DataBind();
        ddlInvName.Items.Clear();


        if (Convert.ToInt32(ddlInvSubCat.SelectedIndex) > 0)
        {
            string strsubsubcat = "SELECT InventorySubSubId ,InventorySubSubName  ,InventorySubCatID  FROM  InventoruSubSubCategory " +
                            " where InventorySubCatID=" + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " ";
            SqlCommand cmdsubsubcat = new SqlCommand(strsubsubcat, con);
            SqlDataAdapter adpsubsubcat = new SqlDataAdapter(cmdsubsubcat);
            DataTable dtsubsubcat = new DataTable();
            adpsubsubcat.Fill(dtsubsubcat);

            ddlInvSubSubCat.DataSource = dtsubsubcat;
            ddlInvSubSubCat.DataTextField = "InventorySubSubName";
            ddlInvSubSubCat.DataValueField = "InventorySubSubId";
            ddlInvSubSubCat.DataBind();

        }
        else
        {
            ddlInvSubSubCat.DataSource = null;
            ddlInvSubSubCat.DataBind();
        }

        ddlInvSubSubCat.Items.Insert(0, "All");
        ddlInvSubSubCat.Items[0].Value = "0";

        ddlInvName.DataSource = null;
        ddlInvName.DataBind();

        ddlInvSubSubCat_SelectedIndexChanged(sender, e);
        // ddlInvSubSubCat.AutoPostBack = true;


    }
    protected void ddlInvSubSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {


        ddlInvName.DataSource = null;
        ddlInvName.DataBind();
        ddlInvName.Items.Clear();
        if (Convert.ToInt32(ddlInvSubSubCat.SelectedIndex) > 0)
        {
            string strinvname = "SELECT InventoryMasterId ,Name ,InventoryDetailsId ,InventorySubSubId   ,ProductNo ,InventoryTypeId  FROM InventoryMaster " +
                            " where InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + " and InventoryMaster.MasterActiveStatus=1  ";
            SqlCommand cmdinvname = new SqlCommand(strinvname, con);
            SqlDataAdapter adpinvname = new SqlDataAdapter(cmdinvname);
            DataTable dtinvname = new DataTable();
            adpinvname.Fill(dtinvname);

            ddlInvName.DataSource = dtinvname;

            ddlInvName.DataTextField = "Name";
            ddlInvName.DataValueField = "InventoryMasterId";
            ddlInvName.DataBind();

        }
        else
        {
            ddlInvName.DataSource = null;
            ddlInvName.DataBind();
        }
        ddlInvName.Items.Insert(0, "All");
        ddlInvName.Items[0].Value = "0";
    }

    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblmsg.Visible = false;
        Panel1.Visible = true;
        DataTable dtinvids = new DataTable();

        if (RadioButtonList1.SelectedValue == "0")
        {
            dtinvids = (DataTable)(SeachByCat());
            Panel2.Visible = true;
        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtSearchInvName.Text.Length > 0 || txtBarcode.Text.Length > 0 || txtProductNo.Text.Length > 0)
            {
                dtinvids = (DataTable)(SearchByName());
                Panel2.Visible = true;
            }
            else
            {
                lblInvName.Visible = true;
                lblInvName.Text = "plese input InvntoryName atleast";
            }
        }
        else if (RadioButtonList1.SelectedValue == "2")
        {
            Panel2.Visible = false;

            // FillDDlCatScatSScatName();

            string strDetail2 = " SELECT     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.InventoryMasterId, InventoryWarehouseMasterTbl.InventoryDetailsId, " +
                 " InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.PreferredVendorId,  " +
                " InventoryWarehouseMasterTbl.ReorderQuantiy, InventoryWarehouseMasterTbl.NormalOrderQuantity, InventoryWarehouseMasterTbl.ReorderLevel,  " +
                " InventoryWarehouseMasterTbl.QtyOnDateStarted, InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.Rate,InventoryWarehouseMasterTbl.Weight,InventoryWarehouseMasterTbl.UnitTypeId,     " +
                " InventoryMaster.Name AS InvName,InventoryMaster.ProductNo, WareHouseMaster.Name AS WarehouseName, InventoryWarehouseMasterTbl.OpeningQty,InventoryWarehouseMasterTbl.OpeningRate  " +
                " FROM         InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                " WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId and WareHouseMaster.comid='" + compid + "' LEFT OUTER JOIN " +
                " InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId " +
                " where InventoryWarehouseMasterTbl.InventoryMasterId=" + Convert.ToInt32(ddlCatScSscNameofInv.SelectedValue) + " and InventoryMaster.MasterActiveStatus=1 " +
                " and InventoryWarehouseMasterTbl.WareHouseId=" + Convert.ToInt32(ddlWarehouse.SelectedValue) + " ";

            SqlCommand cmdDetail2 = new SqlCommand(strDetail2, con);
            SqlDataAdapter adpDetail2 = new SqlDataAdapter(cmdDetail2);
            DataTable dtDetail2 = new DataTable();
            adpDetail2.Fill(dtDetail2);
            if (dtDetail2.Rows.Count > 0)
            {
                txtWeight.Text = dtDetail2.Rows[0]["Weight"].ToString();
                if (txtWeight.Text == "")
                {
                    txtWeight.Text = "0";
                }
                txtRate.Text = dtDetail2.Rows[0]["Rate"].ToString();
                txtreorderquantiy.Text = dtDetail2.Rows[0]["ReorderQuantiy"].ToString();
                txtreorderlevel.Text = dtDetail2.Rows[0]["ReorderLevel"].ToString();
                txtqtyondatestarted.Text = dtDetail2.Rows[0]["QtyOnDateStarted"].ToString();
                txtotyonhand.Text = dtDetail2.Rows[0]["QtyOnHand"].ToString();
                //  txtnormalorderquantity.Text = dtDetail2.Rows[0]["NormalOrderQuantity"].ToString();
                string pviddd = dtDetail2.Rows[0]["PreferredVendorId"].ToString();
                ddlPreferedVendor.SelectedIndex = ddlPreferedVendor.Items.IndexOf(ddlPreferedVendor.Items.FindByValue(dtDetail2.Rows[0]["PreferredVendorId"].ToString()));
                ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(dtDetail2.Rows[0]["Active"].ToString()));
                ddllbs.SelectedIndex = ddllbs.Items.IndexOf(ddllbs.Items.FindByValue(dtDetail2.Rows[0]["UnitTypeId"].ToString()));
                txtOpeingQty.Text = dtDetail2.Rows[0]["OpeningQty"].ToString();
                txtOpeingRAte.Text = dtDetail2.Rows[0]["OpeningRate"].ToString();
                hdninvExist.Value = "1";
                //ViewState["IWMi"] = Convert.ToInt32(dtDetail2.Rows[0]["InventoryWarehouseMasterId"]);
                //lblInvNameFromGrd.Text = dtDetail.Rows[0]["InvName"].ToString();
                //lblWareshouseFromGrd.Text = dtDetail.Rows[0]["WarehouseName"].ToString();


                lblInvDetail.Visible = true;
                lblInvDetail.Text = " Update Inventory Master " + dtDetail2.Rows[0]["InvName"].ToString() + " at " + dtDetail2.Rows[0]["WarehouseName"].ToString() + " Location";


            }
            else
            {


                ClearDetails();
                lblInvDetail.Visible = true;
                lblInvDetail.Text = " Insert Inventory Master " + ddlCatScSscNameofInv.SelectedItem.Text + " at " + Convert.ToString(ddlWarehouse.SelectedItem.Text) + " Location";

                hdninvExist.Value = "0";
            }

        }
        else
        {

        }
        if (dtinvids.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = dtinvids.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            grdInvMasters.DataSource = dtinvids;
            grdInvMasters.DataBind();

        }
        else
        {
            grdInvMasters.DataSource = null;
            grdInvMasters.DataBind();
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
            ImgBtnSearchGo_Click(sender, e);
            clearcontrolsdetail();

        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            pnlInvCat.Visible = false;
            pnlInvName.Visible = true;
            pnlInvDDLname.Visible = false;
            // ImgBtnSearchGo_Click(sender, e);
            clearcontrolsdetail();
            Panel1.Visible = false;
        }
        else if (RadioButtonList1.SelectedValue == "2")
        {
            pnlInvCat.Visible = false;
            pnlInvName.Visible = false;
            pnlInvDDLname.Visible = true;
            Panel1.Visible = false;
            //clearcontrolsdetail()
            ddlCatScSscNameofInv.Items.Clear();
            if (ViewState["CSSCNm"].ToString() == "")
            {


                FillDDlCatScatSScatName();
            }
            else
            {
                FillDDlCatScatSScatName();
                ddlCatScSscNameofInv.SelectedIndex = ddlCatScSscNameofInv.Items.IndexOf(ddlCatScSscNameofInv.Items.FindByValue(ViewState["CSSCNm"].ToString()));
            }
            ImgBtnSearchGo_Click(sender, e);
            clearcontrolsdetail();
        }
        else
        {
            pnlInvCat.Visible = false;
            pnlInvName.Visible = false;
            pnlInvDDLname.Visible = false;
            Panel1.Visible = false;
            //btnSearchGo_Click(sender, e);
            clearcontrolsdetail();
        }
    }
    protected void FillDDlCatScatSScatName()
    {

        //////string strforall1 = "SELECT     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryMaster.Name AS InventoryName, WareHouseMaster.WareHouseId, "+
        //////       "   WareHouseMaster.Name AS WarehouseName, InventoryMaster.InventoryMasterId, InventoryDetails.Inventory_Details_Id, InventoryDetails.Description, "+
        //////       "   InventoryDetails.Weight, InventoryDetails.UnitTypeId, InventoryBarcodeMaster.InventoryBacodeMasterId, InventoryBarcodeMaster.Barcode, "+
        //////       "   InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, "+
        //////       "   InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName,  "+
        //////       "   InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.PreferredVendorId, InventoryWarehouseMasterTbl.ReorderQuantiy, "+
        //////       "   InventoryWarehouseMasterTbl.NormalOrderQuantity, InventoryWarehouseMasterTbl.ReorderLevel, InventoryWarehouseMasterTbl.QtyOnDateStarted,  "+
        //////       "   InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.Rate, LEFT(InventoryCategoryMaster.InventoryCatName, 8) "+
        //////       "   + ' : ' + LEFT(InventorySubCategoryMaster.InventorySubCatName, 8) + ' : ' + LEFT(InventoruSubSubCategory.InventorySubSubName, 8)  "+
        //////       "   + ' : ' + InventoryMaster.Name AS CatScSscName "+
        //////       "   FROM         WareHouseMaster RIGHT OUTER JOIN "+
        //////       "   InventoryWarehouseMasterTbl ON WareHouseMaster.WareHouseId = InventoryWarehouseMasterTbl.WareHouseId LEFT OUTER JOIN "+
        //////       "   InventoryCategoryMaster RIGHT OUTER JOIN "+
        //////       "   InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN "+
        //////       "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN "+
        //////       "   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId LEFT OUTER JOIN "+
        //////       "   InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN "+
        //////       "   InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id ON  "+
        //////       "   InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId "+
        //////       "   Where InventoryWarehouseMasterTbl.WareHouseId = '" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' 
        string strforall1 = " SELECT     InventoryMaster.Name AS InventoryName, InventoryMaster.InventoryMasterId, InventoryDetails.Inventory_Details_Id, InventoryDetails.Description, " +
                   "   InventoryDetails.Weight, InventoryDetails.UnitTypeId, InventoryBarcodeMaster.InventoryBacodeMasterId, InventoryBarcodeMaster.Barcode, " +
                   "   InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatId, " +
                   "   InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName,  " +
                   "   LEFT(InventoryCategoryMaster.InventoryCatName, 15) + ' : ' + LEFT(InventorySubCategoryMaster.InventorySubCatName, 15) " +
                   "   + ' : ' + LEFT(InventoruSubSubCategory.InventorySubSubName, 15) + ' : ' + InventoryMaster.Name AS CatScSscName " +
                   " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                   "   InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                   "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
                   "   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId LEFT OUTER JOIN " +
                   "   InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN " +
                   "   InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id " +
                  " WHERE     (InventoryMaster.MasterActiveStatus = 1) and InventoryCategoryMaster.compid = '" + compid + "' and  InventoryCategoryMaster.CatType IS NULL " +
                  " ORDER BY InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubName, InventoryName ";

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
    {//InventoryWarehouseMasterTbl inner join  InventoryDetails on InventoryDetails.Inventory_Details_Id=InventoryWarehouseMasterTbl.InventoryDetailsId
        lblBusiness.Text = ddlWarehouse.SelectedItem.Text;
        string strinv = " SELECT Distinct    InventoryMaster.InventoryMasterId,InventoryDetails.Inventory_Details_Id,InventoryWarehouseMasterTbl.Weight, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, " +
                    "  InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId, " +
                    "  InventoryCategoryMaster.InventoryCatName, left(InventoryMaster.Name,60) as Name, InventoryMaster.ProductNo, " +
                    "  left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + left(InventoruSubSubCategory.InventorySubSubName,15) " +
            //  "   + ' : <br/>' + InventoryMaster.Name  
        " AS CateAndName, InventoryBarcodeMaster.Barcode,InventoryDetails.Inventory_Details_Id, InventoryDetails.Description,cast(InventoryWarehouseMasterTbl.Weight as nvarchar) +' '+ UnitTypeMaster.Name AS UnitTypeName, InventoryWarehouseMasterTbl.UnitTypeId " +
                    "  FROM InventoryWarehouseMasterTbl  LEFT OUTER JOIN " +
                       " UnitTypeMaster ON InventoryWarehouseMasterTbl.UnitTypeId = UnitTypeMaster.UnitTypeId " +
                    " inner join InventoryDetails on InventoryDetails.Inventory_Details_Id = InventoryWarehouseMasterTbl.InventoryDetailsId RIGHT OUTER JOIN " +
                    "  InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId LEFT OUTER JOIN " +
                    "  InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id LEFT OUTER JOIN " +
                    "  InventorySubCategoryMaster LEFT OUTER JOIN " +
                    "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
                    "  InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON " +
                    "  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId where InventoryCategoryMaster.compid='" + Session["comid"] + "' and  InventoryCategoryMaster.CatType IS NULL ";
        string strInvId = "";
        string strInvsubsubCatId = "";
        string strInvsubcatid = "";
        string strInvCatid = "";
        // string strInvBySerchId = "";
        //if (txtSearchInvName.Text.Length <= 0)
        //{
        if (ddlInvCat.SelectedIndex > 0)
        {
            lblMainCat.Text = ddlInvCat.SelectedItem.Text;
            if (ddlInvSubCat.SelectedIndex > 0)
            {
                lblSubCat.Text = ddlInvSubCat.SelectedItem.Text;
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


        string mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + " and InventoryMaster.MasterActiveStatus=1  order by InventoryMaster.InventoryMasterId";//+ strInvBySerchId // InventoryMaster.Name ";


        SqlCommand cmdcat = new SqlCommand(mainStringCat, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);



        return dtcat;

    }

    //public DataTable SeachByCat()
    //{
    //    string strinv = " SELECT     InventoryMaster.InventoryMasterId,InventoryDetails.Inventory_Details_Id,InventoryDetails.Weight, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, " +
    //                "  InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId, "+
    //                "  InventoryCategoryMaster.InventoryCatName, InventoryMaster.Name, InventoryMaster.ProductNo, "+
    //                "  InventoryCategoryMaster.InventoryCatName + ' : <br/>' + InventorySubCategoryMaster.InventorySubCatName + ' : <br/>' + InventoruSubSubCategory.InventorySubSubName "+
    //              //  "   + ' : <br/>' + InventoryMaster.Name  
    //    " AS CateAndName, InventoryBarcodeMaster.Barcode,InventoryDetails.Inventory_Details_Id, InventoryDetails.Description,UnitTypeMaster.Name AS UnitTypeName, InventoryDetails.UnitTypeId " +
    //                "  FROM         InventoryDetails  LEFT OUTER JOIN "+
    //                   " UnitTypeMaster ON InventoryDetails.UnitTypeId = UnitTypeMaster.UnitTypeId   RIGHT OUTER JOIN "+
    //                "  InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId LEFT OUTER JOIN "+
    //                "  InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id LEFT OUTER JOIN " +
    //                "  InventorySubCategoryMaster LEFT OUTER JOIN "+
    //                "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN "+
    //                "  InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON "+
    //                "  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId ";
    //    string strInvId = "";
    //    string strInvsubsubCatId = "";
    //    string strInvsubcatid = "";
    //    string strInvCatid = "";
    //   // string strInvBySerchId = "";
    //    //if (txtSearchInvName.Text.Length <= 0)
    //    //{
    //    if (ddlInvCat.SelectedIndex > 0)
    //    {
    //        if (ddlInvSubCat.SelectedIndex > 0)
    //        {
    //            if (ddlInvSubSubCat.SelectedIndex > 0)
    //            {
    //                if (ddlInvName.SelectedIndex > 0)
    //                {
    //                    strInvId = "where  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

    //                }
    //                else
    //                {
    //                    strInvsubsubCatId = "where InventoruSubSubCategory.InventorySubSubId=" + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "";
    //                }
    //            }
    //            else
    //            {
    //                strInvsubcatid = "where InventorySubCategoryMaster.InventorySubCatId = " + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " ";

    //            }

    //        }
    //        else
    //        {
    //            strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

    //            //strInvId = "where  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

    //        }
    //    }
    //    else
    //    {
    //        //strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

    //    }


    //    string mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + " and InventoryMaster.MasterActiveStatus=1  order by InventoryMaster.InventoryMasterId";//+ strInvBySerchId // InventoryMaster.Name ";


    //    SqlCommand cmdcat = new SqlCommand(mainStringCat, con);
    //    SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
    //    DataTable dtcat = new DataTable();
    //    adpcat.Fill(dtcat);



    //    return dtcat;

    //}

    public DataTable SearchByName()
    {
        lblBusiness.Text = ddlWarehouse.SelectedItem.Text;
        string str23invname1 = " SELECT     InventoryMaster.InventoryMasterId,InventoryDetails.Inventory_Details_Id,InventoryWarehouseMasterTbl.Weight, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, " +
                    "  InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId, " +
                    "  InventoryCategoryMaster.InventoryCatName, left(InventoryMaster.Name,60) as Name, InventoryMaster.ProductNo, " +
                    "  left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + left(InventoruSubSubCategory.InventorySubSubName,15) " +
            //"   + ' : <br/>' + InventoryMaster.Name 
                    " AS CateAndName, InventoryBarcodeMaster.Barcode, InventoryDetails.Description,cast(InventoryWarehouseMasterTbl.Weight as nvarchar) +' '+ UnitTypeMaster.Name AS UnitTypeName, InventoryWarehouseMasterTbl.UnitTypeId " +
                    "  FROM   InventoryWarehouseMasterTbl      LEFT OUTER JOIN " +
                     "  UnitTypeMaster ON InventoryWarehouseMasterTbl.UnitTypeId = UnitTypeMaster.UnitTypeId " +
                    " inner join InventoryDetails on InventoryDetails.Inventory_Details_Id = InventoryWarehouseMasterTbl.InventoryDetailsId RIGHT OUTER JOIN " +
                    "  InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId LEFT OUTER JOIN " +
                    "  InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id LEFT OUTER JOIN " +
                    "  InventorySubCategoryMaster LEFT OUTER JOIN " +
                    "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
                    "  InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON " +
                    "  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId where InventoryCategoryMaster.compid = '" + compid + "' and InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "'   and     InventoryMaster.CatType IS NULL ";


        //string str23invname1 = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.InventorySubSubId, InventoryMaster.Name, InventoryMaster.ProductNo, " +
        //             " InventoruSubSubCategory.InventorySubSubName, InventoryDetails.Description, InventoryDetails.Inventory_Details_Id, InventoryDetails.QtyOnHand, " +
        //             " InventoryDetails.Rate, InventoryWarehouseMasterTbl.Weight, InventoruSubSubCategory.InventorySubSubId AS Expr1, InventorySizeMaster.Width, InventorySizeMaster.Height,  " +
        //             " InventorySizeMaster.length AS Length, InventoryBarcodeMaster.Barcode, InventoryMeasurementUnit.Unit, CASE WHEN InventoryMeasurementUnit.UnitType IS NULL  " +
        //             " THEN '1' ELSE InventoryMeasurementUnit.UnitType END AS UnitType " +
        //             " FROM      InventoryCategoryMaster inner join InventorySubCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId inner join  InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId INNER JOIN  InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id INNER JOIN  " +
        //             " InventoryMeasurementUnit ON InventoryMaster.InventoryMasterId = InventoryMeasurementUnit.InventoryMasterId LEFT OUTER JOIN " +
        //             " InventorySizeMaster ON InventoryMaster.InventoryMasterId = InventorySizeMaster.InventoryMasterId LEFT OUTER JOIN " +
        //             " InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id " +
        //            " WHERE  InventoryCategoryMaster.compid='" + Session["comid"] + "' and InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "' and   (InventoryMaster.Name like '%" + txtSearchInvName.Text.Replace("'", "''") + "%') and InventoryMaster.MasterActiveStatus=1 and  InventoryMaster.CatType IS NULL   ";



        string strbarcode = "";
        string strproductno = "";
        string strname = "";

        if (txtSearchInvName.Text.Length > 0)
        {

            strname = "and   (InventoryMaster.Name like '%" + txtSearchInvName.Text.Replace("'", "''") + "%')";
        }

        if (txtBarcode.Text.Length > 0)
        {
            if (txtSearchInvName.Text.Length > 0)
            {
                strbarcode = "or (InventoryBarcodeMaster.Barcode='" + txtBarcode.Text + "')";
            }
            else
            {
                strbarcode = "and (InventoryBarcodeMaster.Barcode='" + txtBarcode.Text + "')";
            }

        }
        if (txtProductNo.Text.Length > 0)
        {
            if (txtSearchInvName.Text.Length > 0)
            {
                strproductno = " or (InventoryMaster.ProductNo='" + txtProductNo.Text + "')";
            }
            else
            {
                strproductno = " and (InventoryMaster.ProductNo='" + txtProductNo.Text + "')";
            }
        }
        string str23invname = str23invname1 + strname + strbarcode + strproductno;


        SqlCommand cmd23invname = new SqlCommand(str23invname, con);
        SqlDataAdapter adp23invname = new SqlDataAdapter(cmd23invname);
        DataTable dt23invname = new DataTable();
        adp23invname.Fill(dt23invname);

        string strIdinvname = "";
        string strInvAllIdsinvname = "";
        string strtempinvname = "";
        string strInvBySerchIdinvname = "";
        string strInvBySerchIdinvname23 = "";
        string strInvBySerchIdinvname2 = "";
        if (dt23invname.Rows.Count > 0)
        {

            foreach (DataRow dtrrr in dt23invname.Rows)
            {
                strIdinvname = dtrrr["InventoryMasterId"].ToString();
                strInvAllIdsinvname = strIdinvname + "," + strInvAllIdsinvname;
                strtempinvname = strInvAllIdsinvname.Substring(0, (strInvAllIdsinvname.Length - 1));
            }

            strInvBySerchIdinvname = " and InventoryMaster.InventoryMasterId in (" + strtempinvname + ") and InventoryMaster.MasterActiveStatus=1 ";
            //string mainstring = strinv + "  order by SalesChallanMaster.RefSalesOrderId ";
        }
        else
        {
            ////txtProductNo.Text = txtSearchInvName.Text;
            ////txtSearchInvName.Text = txtBarcode.Text;
            //string str23invname12 = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.InventorySubSubId, InventoryMaster.Name, InventoryMaster.ProductNo, " +
            //         " InventoruSubSubCategory.InventorySubSubName, InventoryDetails.Description, InventoryDetails.Inventory_Details_Id, InventoryDetails.QtyOnHand, " +
            //         " InventoryDetails.Rate, InventoryWarehouseMasterTbl.Weight, InventoruSubSubCategory.InventorySubSubId AS Expr1, InventorySizeMaster.Width, InventorySizeMaster.Height,  " +
            //         " InventorySizeMaster.length AS Length, InventoryBarcodeMaster.Barcode, InventoryMeasurementUnit.Unit, CASE WHEN InventoryMeasurementUnit.UnitType IS NULL  " +
            //         " THEN '1' ELSE InventoryMeasurementUnit.UnitType END AS UnitType " +
            //         " FROM    InventoryCategoryMaster inner join InventorySubCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId inner join  InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId INNER JOIN  InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id INNER JOIN  " + " InventoryMeasurementUnit ON InventoryMaster.InventoryMasterId = InventoryMeasurementUnit.InventoryMasterId LEFT OUTER JOIN " +
            //         " InventorySizeMaster ON InventoryMaster.InventoryMasterId = InventorySizeMaster.InventoryMasterId LEFT OUTER JOIN " +
            //         " InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id " +
            //        " WHERE    InventoryCategoryMaster.compid='" + Session["comid"] + "' and (InventoryMaster.ProductNo = '" + txtProductNo.Text + "') and InventoryMaster.MasterActiveStatus=1 and  InventoryMaster.CatType IS NULL ";
            ////string str23invname2 = str23invname12 + strbarcode + strproductno;


            //SqlCommand cmd23invname2 = new SqlCommand(str23invname12, con);
            //SqlDataAdapter adp23invname2 = new SqlDataAdapter(cmd23invname2);
            //DataTable dt23invname2 = new DataTable();
            //adp23invname2.Fill(dt23invname2);

            //string strIdinvname2 = "";
            //string strInvAllIdsinvname2 = "";
            //string strtempinvname2 = "";

            //if (dt23invname2.Rows.Count > 0)
            //{

            //    foreach (DataRow dtrrr2 in dt23invname2.Rows)
            //    {
            //        strIdinvname2 = dtrrr2["InventoryMasterId"].ToString();
            //        strInvAllIdsinvname2 = strIdinvname2 + "," + strInvAllIdsinvname2;
            //        strtempinvname2 = strInvAllIdsinvname2.Substring(0, (strInvAllIdsinvname2.Length - 1));
            //    }

            //    strInvBySerchIdinvname2 = " and InventoryMaster.InventoryMasterId in (" + strtempinvname2 + ") and InventoryMaster.MasterActiveStatus=1 ";

            //}
            //else
            //{
            ////txtSearchInvName.Text = txtProductNo.Text;
            ////txtBarcode.Text = txtSearchInvName.Text;
            //string str23invname123 = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.InventorySubSubId, InventoryMaster.Name, InventoryMaster.ProductNo, " +
            //         " InventoruSubSubCategory.InventorySubSubName, InventoryDetails.Description, InventoryDetails.Inventory_Details_Id, InventoryDetails.QtyOnHand, " +
            //         " InventoryDetails.Rate, InventoryWarehouseMasterTbl.Weight, InventoruSubSubCategory.InventorySubSubId AS Expr1, InventorySizeMaster.Width, InventorySizeMaster.Height,  " +
            //         " InventorySizeMaster.length AS Length, InventoryBarcodeMaster.Barcode, InventoryMeasurementUnit.Unit, CASE WHEN InventoryMeasurementUnit.UnitType IS NULL  " +
            //         " THEN '1' ELSE InventoryMeasurementUnit.UnitType END AS UnitType " +
            //         " FROM          InventoryCategoryMaster inner join InventorySubCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId inner join  InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId  INNER JOIN  InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id INNER JOIN  " + " InventoryMeasurementUnit ON InventoryMaster.InventoryMasterId = InventoryMeasurementUnit.InventoryMasterId  " +
            //         " LEFT OUTER JOIN " +
            //         " InventorySizeMaster ON InventoryMaster.InventoryMasterId = InventorySizeMaster.InventoryMasterId LEFT OUTER JOIN " +
            //         " InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id " +
            //        " WHERE   InventoryCategoryMaster.compid='" + Session["comid"] + "' and  (InventoryBarcodeMaster.Barcode = '" + txtBarcode.Text + "') and InventoryMaster.MasterActiveStatus=1 and  InventoryMaster.CatType IS NULL";
            ////string str23invname2 = str23invname12;


            //SqlCommand cmd23invname23 = new SqlCommand(str23invname123, con);
            //SqlDataAdapter adp23invname23 = new SqlDataAdapter(cmd23invname23);
            //DataTable dt23invname23 = new DataTable();
            //adp23invname23.Fill(dt23invname23);

            //string strIdinvname23 = "";
            //string strInvAllIdsinvname23 = "";
            //string strtempinvname23 = "";

            //if (dt23invname23.Rows.Count > 0)
            //{

            //    foreach (DataRow dtrrr23 in dt23invname23.Rows)
            //    {
            //        strIdinvname23 = dtrrr23["InventoryMasterId"].ToString();
            //        strInvAllIdsinvname23 = strIdinvname23 + "," + strInvAllIdsinvname23;
            //        strtempinvname23 = strInvAllIdsinvname23.Substring(0, (strInvAllIdsinvname23.Length - 1));
            //    }

            //    strInvBySerchIdinvname23 = " and InventoryMaster.InventoryMasterId in (" + strtempinvname23 + ") and InventoryMaster.MasterActiveStatus=1 ";

            //}
            //  }



        }

        string strmaininvname = str23invname1 + strname + strbarcode + strproductno + strInvBySerchIdinvname;
        SqlCommand cmdinvname = new SqlCommand(strmaininvname, con);
        SqlDataAdapter adpinvname = new SqlDataAdapter(cmdinvname);
        DataTable dtinvname = new DataTable();
        adpinvname.Fill(dtinvname);

        return dtinvname;

    }

    protected void grdInvMasters_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "Select1")
        {
            int indx = Convert.ToInt32(e.CommandArgument.ToString());
            grdInvMasters.Rows[indx].BackColor = System.Drawing.Color.Yellow;
            Label invMid = (Label)(grdInvMasters.Rows[indx].FindControl("lblInvMasterId"));
            Label invDid = (Label)(grdInvMasters.Rows[indx].FindControl("lblInvDetailId"));
            Label InvName = (Label)(grdInvMasters.Rows[indx].FindControl("lblInvName"));



            ViewState["invDi"] = Convert.ToInt32(invDid.Text);
            ViewState["invMi"] = Convert.ToInt32(invMid.Text);

            string strDetail = " SELECT     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,InventoryWarehouseMasterTbl.SafetyStock, InventoryWarehouseMasterTbl.LeadDays,InventoryWarehouseMasterTbl.LeadDaysUsage,InventoryWarehouseMasterTbl.InventoryMasterId, InventoryWarehouseMasterTbl.InventoryDetailsId, " +
                       " InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.PreferredVendorId,  " +
                      " InventoryWarehouseMasterTbl.ReorderQuantiy, InventoryWarehouseMasterTbl.NormalOrderQuantity, InventoryWarehouseMasterTbl.ReorderLevel,  " +
                      " InventoryWarehouseMasterTbl.QtyOnDateStarted, InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.Rate,InventoryWarehouseMasterTbl.Weight,InventoryWarehouseMasterTbl.UnitTypeId,   " +
                      " InventoryMaster.Name AS InvName, WareHouseMaster.Name AS WarehouseName ,InventoryWarehouseMasterTbl.OpeningQty,InventoryWarehouseMasterTbl.OpeningRate " +
                      " FROM         InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                      " WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId LEFT OUTER JOIN " +
                      " InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId " +
                      " where InventoryWarehouseMasterTbl.InventoryMasterId=" + Convert.ToInt32(invMid.Text) + " " +
                      " and InventoryMaster.MasterActiveStatus=1 and InventoryWarehouseMasterTbl.WareHouseId=" + Convert.ToInt32(ddlWarehouse.SelectedValue) + " and WareHouseMaster.comid = '" + compid + "' ";

            SqlCommand cmdDetail = new SqlCommand(strDetail, con);
            SqlDataAdapter adpDetail = new SqlDataAdapter(cmdDetail);
            DataTable dtDetail = new DataTable();
            adpDetail.Fill(dtDetail);
            if (dtDetail.Rows.Count > 0)
            {



                string strcurrent = "SELECT top(1) * FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + dtDetail.Rows[0]["InventoryWarehouseMasterId"].ToString() + "' and DateUpdated<'" + System.DateTime.Now.ToShortDateString() + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc";
                SqlCommand cmdcurrent = new SqlCommand(strcurrent, con);
                SqlDataAdapter adpcurrent = new SqlDataAdapter(cmdcurrent);
                DataTable dtcurrent = new DataTable();
                adpcurrent.Fill(dtcurrent);

                if (dtcurrent.Rows.Count > 0)
                {
                    if (dtcurrent.Rows[0]["QtyonHand"].ToString() != "")
                    {
                        txtotyonhand.Text = dtcurrent.Rows[0]["QtyonHand"].ToString();
                    }
                    else
                    {
                        txtotyonhand.Text = "0";
                    }
                }

                // eoq 

                string streoq = " select * from  EoqCalculation where invwhid='" + dtDetail.Rows[0]["InventoryWarehouseMasterId"].ToString() + "' and Whid='" + ddlWarehouse.SelectedValue + "' and Accountyearid='" + ViewState["Report_Period_Id"].ToString() + "'";
                SqlCommand cmdeoq = new SqlCommand(streoq, con);
                SqlDataAdapter adpeoq = new SqlDataAdapter(cmdeoq);
                DataTable dteoq = new DataTable();
                adpeoq.Fill(dteoq);

                if (dteoq.Rows.Count > 0)
                {
                    lbleoq.Text = dteoq.Rows[0]["Eoq"].ToString();

                }
                else
                {
                    lbleoq.Text = "0";
                }

                // end eoq

                // last purchase order qty and rate
                string strlastpur = "select InventoryWarehouseMasterAvgCostTbl.* from InventoryWarehouseMasterAvgCostTbl inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=InventoryWarehouseMasterAvgCostTbl.Tranction_Master_Id  where TranctionMaster.EntryTypeId='27' and TranctionMaster.Whid='" + ddlWarehouse.SelectedValue + "' and InventoryWarehouseMasterAvgCostTbl.InvWMasterId='" + dtDetail.Rows[0]["InventoryWarehouseMasterId"].ToString() + "' and InventoryWarehouseMasterAvgCostTbl.DateUpdated between '" + ViewState["StartDate"].ToString() + "' and '" + ViewState["EndDate"].ToString() + "'  order by DateUpdated desc ";
                SqlCommand cmdlastpur = new SqlCommand(strlastpur, con);
                SqlDataAdapter adplastpur = new SqlDataAdapter(cmdlastpur);
                DataTable dslastpur = new DataTable();
                adplastpur.Fill(dslastpur);


                if (dslastpur.Rows.Count > 0)
                {

                    lblqtylastpurchased.Text = dslastpur.Rows[0]["Qty"].ToString();
                    lblrateqtylastpurchased.Text = dslastpur.Rows[0]["Rate"].ToString();
                }
                else
                {
                    lblqtylastpurchased.Text = "";
                    lblrateqtylastpurchased.Text = "";
                }

                //End last purchase order qty and rate


                // opening stock
                string stropening = "SELECT  * FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + dtDetail.Rows[0]["InventoryWarehouseMasterId"].ToString() + "' and Tranction_Master_Id ='00000'";
                SqlCommand cmdopening = new SqlCommand(stropening, con);
                SqlDataAdapter adpopening = new SqlDataAdapter(cmdopening);
                DataTable dtopening = new DataTable();
                adpopening.Fill(dtopening);

                if (dtopening.Rows.Count > 0)
                {

                    txtOpeingQty.Text = dtopening.Rows[0]["Qty"].ToString();
                    txtOpeingRAte.Text = dtopening.Rows[0]["Rate"].ToString();


                }

                // end opening stock



                txtsafetystock.Text = dtDetail.Rows[0]["SafetyStock"].ToString();
                txtleaddays.Text = dtDetail.Rows[0]["LeadDays"].ToString();
                if (dtDetail.Rows[0]["LeadDaysUsage"].ToString() != "")
                {
                    decimal leadusage = Math.Round(Convert.ToDecimal(dtDetail.Rows[0]["LeadDaysUsage"].ToString()), 0);
                    txtleaddaysusage.Text = leadusage.ToString();
                }

                txtreorderquantiy.Text = dtDetail.Rows[0]["ReorderQuantiy"].ToString();
                txtreorderlevel.Text = dtDetail.Rows[0]["ReorderLevel"].ToString();

                txtqtyondatestarted.Text = dtDetail.Rows[0]["QtyOnDateStarted"].ToString();
                txtRate.Text = dtDetail.Rows[0]["Rate"].ToString();

                string pviddd = dtDetail.Rows[0]["PreferredVendorId"].ToString();
                ddlPreferedVendor.SelectedIndex = ddlPreferedVendor.Items.IndexOf(ddlPreferedVendor.Items.FindByValue(pviddd));
                ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(dtDetail.Rows[0]["Active"].ToString()));

                txtWeight.Text = dtDetail.Rows[0]["Weight"].ToString();
                if (txtWeight.Text == "")
                {
                    txtWeight.Text = "0";
                }
                ddllbs.SelectedIndex = ddllbs.Items.IndexOf(ddllbs.Items.FindByValue(dtDetail.Rows[0]["UnitTypeId"].ToString()));
                hdninvExist.Value = "1";
                ViewState["IWMi"] = Convert.ToInt32(dtDetail.Rows[0]["InventoryWarehouseMasterId"]);
                lblInvDetail.Visible = true;
                lblInvDetail.Text = " Update Inventory Master " + dtDetail.Rows[0]["InvName"].ToString() + " at " + dtDetail.Rows[0]["WarehouseName"].ToString() + " Location";
                btnSubmit.Text = "Update";
                updatepnl.Visible = true;

            }
            else
            {

                updatepnl.Visible = false;

                lblInvDetail.Visible = true;
                lblInvDetail.Text = " Insert Inventory Master " + InvName.Text + " at " + Convert.ToString(ddlWarehouse.SelectedItem.Text) + " Location";
                btnSubmit.Text = "Submit";
                hdninvExist.Value = "0";
                ClearDetails();

                txtotyonhand.Text = "0";
                txtRate.Text = "0";
                txtreorderlevel.Text = "0";
                txtreorderquantiy.Text = "0";

                txtOpeingQty.Text = "0";
                txtOpeingRAte.Text = "0";
            }

        }
    }
    protected void ClearDetails()
    {
        txtRate.Text = "";
        txtreorderquantiy.Text = "0";
        txtreorderlevel.Text = "0";
        //  txtqtyondatestarted.Text = "";
        txtotyonhand.Text = "0";
        //txtnormalorderquantity.Text = "0";
        //string pviddd = "";
        ddlPreferedVendor.SelectedIndex = 0;
        // chkActive.Checked = false;
        hdninvExist.Value = "0";
        ViewState["IWMi"] = null;
        //lblInvNameFromGrd.Text = "";
        //lblWareshouseFromGrd.Text = "";
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {




        string strDetail = " select * from InventoryWarehouseMasterTbl where InventoryMasterId='" + ViewState["invMi"].ToString() + "' and WareHouseId='" + ddlWarehouse.SelectedValue + "' ";
        SqlCommand cmdDetail = new SqlCommand(strDetail, con);
        SqlDataAdapter adpDetail = new SqlDataAdapter(cmdDetail);
        DataTable dtDetail = new DataTable();
        adpDetail.Fill(dtDetail);

        if (dtDetail.Rows.Count > 0)
        {
            string strIns = "Update  InventoryWarehouseMasterTbl SET PreferredVendorId=" + ddlPreferedVendor.SelectedValue + " ,ReorderQuantiy='" + txtreorderquantiy.Text + "',ReorderLevel='" + txtreorderlevel.Text + "',Rate='" + txtRate.Text + "',Weight='" + txtWeight.Text + "',UnitTypeId='" + ddllbs.SelectedValue + "',SafetyStock='" + txtsafetystock.Text + "',LeadDays='" + txtleaddays.Text + "',LeadDaysUsage='" + txtleaddaysusage.Text + "' WHERE InventoryWarehouseMasterId=" + ViewState["IWMi"].ToString() + "";
            SqlCommand updateDetailss = new SqlCommand(strIns, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            updateDetailss.ExecuteNonQuery();
            con.Close();

           
            clearcontrolsdetail();
            btnSubmit.Text = "Submit";
            updatepnl.Visible = false;

            ImgBtnSearchGo_Click(sender, e);
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";
        }
        else
        {
            string insertdetail = "INSERT INTO InventoryWarehouseMasterTbl (InventoryMasterId ,InventoryDetailsId,WareHouseId,Active,PreferredVendorId,ReorderQuantiy,NormalOrderQuantity,ReorderLevel ,QtyOnDateStarted ,Rate,Weight,UnitTypeId,SafetyStock,LeadDays,LeadDaysUsage) VALUES (" + Convert.ToInt32(ViewState["invMi"].ToString()) + "," + Convert.ToInt32(ViewState["invDi"].ToString()) + "," + Convert.ToInt32(ddlWarehouse.SelectedValue) + ",'" + ddlstatus.SelectedValue + "', " + Convert.ToInt32(ddlPreferedVendor.SelectedValue) + ",'" + txtreorderquantiy.Text + "','0' ,'" + txtreorderlevel.Text + "','" + Convert.ToDateTime(txtqtyondatestarted.Text) + "' ,'" + Convert.ToDecimal(txtRate.Text) + "','" + txtWeight.Text + "','" + ddllbs.SelectedValue + "','"+txtsafetystock.Text+"','"+txtleaddays.Text+"','"+txtleaddaysusage.Text+"') ";
            SqlCommand cmdinsertinv = new SqlCommand(insertdetail, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdinsertinv.ExecuteNonQuery();
            con.Close();

            SqlCommand cmdmax = new SqlCommand("select Max(InventoryWarehouseMasterId) as id from InventoryWarehouseMasterTbl", con);
            SqlDataAdapter dtpmax = new SqlDataAdapter(cmdmax);
            DataTable dtmax = new DataTable();
            dtpmax.Fill(dtmax);

            if (dtmax.Rows.Count > 0)
            {


                string str1_insert = "Insert into InventoryWarehouseMasterAvgCostTbl ([InvWMasterId],[AvgCost],[DateUpdated],Qty,Rate,Tranction_Master_Id) VALUES(" + Convert.ToInt32(dtmax.Rows[0]["id"].ToString()) + ",'0',,'" + ViewState["StartDate"].ToString() + "','0','0','00000')";
                SqlCommand cmd1_insert = new SqlCommand(str1_insert, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1_insert.ExecuteNonQuery();
                con.Close();
            }
           
            clearcontrolsdetail();
            btnSubmit.Text = "Submit";
            updatepnl.Visible = false;
            ImgBtnSearchGo_Click(sender, e);
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";

        }


        
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        datefill();
        RadioButtonList1_SelectedIndexChanged(sender, e);
        Panel1.Visible = false;
        string strpv = " SELECT     Party_master.PartyID, Party_master.Account,Party_master.Compname+':'+Party_master.Contactperson  as Compname, Party_master.PartyTypeId, User_master.Active " +
                       " FROM     [PartytTypeMaster] inner join    Party_master on Party_master.PartyTypeId= [PartytTypeMaster].[PartyTypeId] INNER JOIN " +
                         "                  User_master ON Party_master.PartyID = User_master.PartyID " +
                        " WHERE     (User_master.Active = 1) and [PartytTypeMaster].compid='" + Session["comid"] + "' and Party_master.Whid='" + ddlWarehouse.SelectedValue + "' and [PartType]='Vendor' order by Compname "; // + //" Where PartytypeId = 1 and Compname <> 'yyyyy'";

        // string strpv = "SELECT Party_master.PartyID, Party_master.Compname, Party_master.PartyTypeId FROM Party_master inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where PartytTypeMaster.PartType='Vender' and Whid='" + ddlWarehouse.SelectedValue + "'";
        SqlCommand cmdpv = new SqlCommand(strpv, con);
        SqlDataAdapter adppv = new SqlDataAdapter(cmdpv);
        DataTable dtpv = new DataTable();
        adppv.Fill(dtpv);
        Panel1.Visible = false;
        if (dtpv.Rows.Count > 0)
        {
            ddlPreferedVendor.DataSource = dtpv;
            ddlPreferedVendor.DataTextField = "Compname";
            ddlPreferedVendor.DataValueField = "PartyID";
            ddlPreferedVendor.DataBind();
            ddlPreferedVendor.Items.Insert(0, "-Select-");
            ddlPreferedVendor.Items[0].Value = "0";
        }
        else
        {
            ddlPreferedVendor.Items.Clear();
            ddlPreferedVendor.Items.Insert(0, "-Select-");
            ddlPreferedVendor.Items[0].Value = "0";
        }
        //btnSearchGo_Click(sender, e);
        ImgBtnSearchGo_Click(sender, e);

    }
    protected void ddlCatScSscNameofInv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCatScSscNameofInv.SelectedIndex > 0)
        {

            ViewState["CSSCNm"] = Convert.ToInt32(ddlCatScSscNameofInv.SelectedValue);
            ImgBtnSearchGo_Click(sender, e);
        }
    }
    protected void clearcontrolsdetail()
    {
        txtreorderquantiy.Text = "0";
        txtreorderlevel.Text = "0";
        //  txtqtyondatestarted.Text = "";
        txtotyonhand.Text = "0";
        // txtnormalorderquantity.Text = "0";

        //string pviddd = dtDetail.Rows[0]["PreferredVendorId"].ToString();
        ddlPreferedVendor.SelectedIndex = 0;
        // chkActive.Checked = true;
        hdninvExist.Value = "0";
        txtRate.Text = "";
        txtOpeingQty.Text = "0";
        txtOpeingRAte.Text = "0";
        txtWeight.Text = "0";
        ddllbs.SelectedIndex = 0;
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        txtreorderquantiy.Text = "0";
        txtreorderlevel.Text = "0";
        //txtqtyondatestarted.Text = "0";
        txtotyonhand.Text = "0";
        // txtnormalorderquantity.Text = "0";
        //string pviddd = dtDetail.Rows[0]["PreferredVendorId"].ToString();
        ddlPreferedVendor.SelectedIndex = 0;
        //chkActive.Checked = true;
        hdninvExist.Value = "0";
        txtRate.Text = "";
        txtOpeingQty.Text = "0";
        txtOpeingRAte.Text = "0";
        updatepnl.Visible = false;
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzInventoryMaster.aspx");
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzInventoryImgMaster.aspx");
    }

    void calc()                                   //Calculation for finding newAvgCost
    {



        decimal newAvgCost;

        //for old Avg Cost
        string str1 = "SELECT  IWMAvgCostId, AvgCost FROM InventoryWarehouseMasterAvgCostTbl INNER JOIN InventoryWarehouseMasterTbl ON InventoryWarehouseMasterAvgCostTbl.InvWMasterId = InventoryWarehouseMasterTbl.InventoryWarehouseMasterId WHERE InvWMasterId=" + ViewState["IWMi"] + " and Tranction_Master_Id='00000'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        decimal oldAvgCost;
        if (dt1.Rows.Count > 0)
        {
            oldAvgCost = Convert.ToDecimal(dt1.Rows[0]["AvgCost"]);
        }
        else
        {
            oldAvgCost = 0;
            string str1_insert = "Insert into InventoryWarehouseMasterAvgCostTbl ([InvWMasterId],[AvgCost],[DateUpdated],Qty,Rate,Tranction_Master_Id) VALUES(" + ViewState["IWMi"] + ",'" + Convert.ToDecimal(txtOpeingRAte.Text) + "', getdate(),'" + Convert.ToDecimal(txtOpeingQty.Text) + "','" + Convert.ToDecimal(txtOpeingRAte.Text) + "','00000')";
            SqlCommand cmd1_insert = new SqlCommand(str1_insert, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1_insert.ExecuteNonQuery();
            con.Close();

        }
        //for  OpeningQty, OpeningRate
        decimal newOpqty = Convert.ToDecimal(txtOpeingQty.Text);
        decimal newOpRt = Convert.ToDecimal(txtOpeingRAte.Text);

        //For New QtyOnHand
        decimal newQtyonHand = (oldQtyonHand + newOpqty) - oldOpqty;

        //Calculation of  new Avg Cost

        if (newQtyonHand == 0)
        {
            newAvgCost = 0;
        }
        else
        {

            newAvgCost = ((oldAvgCost * oldQtyonHand) - (oldOpqty * oldOpRt) + (newOpqty * newOpRt)) / newQtyonHand;
        }
        string strupdateAvgCost = "Update InventoryWarehouseMasterAvgCostTbl set AvgCost=" + newAvgCost + ",Qty='" + newOpqty + "',Rate='" + newOpRt + "' WHERE InvWMasterId=" + ViewState["IWMi"] + " and Tranction_Master_Id='0'";
        SqlCommand updateAvgCostss = new SqlCommand(strupdateAvgCost, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        updateAvgCostss.ExecuteNonQuery();
        con.Close();

    }
    protected void ImgBtnSearchGo_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        Panel1.Visible = true;
        DataTable dtinvids = new DataTable();
        lblCompany.Text = Session["Cname"].ToString();
        lblBusiness.Text = ddlWarehouse.SelectedItem.Text;
        if (ddlInvCat.SelectedIndex > 0)
        {
            lblMainCat.Text = ddlInvCat.SelectedItem.Text;
        }
        if (ddlInvSubCat.SelectedIndex > 0)
        {
            lblSubCat.Text = ddlInvSubCat.SelectedItem.Text;
        }
        if (RadioButtonList1.SelectedValue == "0")
        {
            dtinvids = (DataTable)(SeachByCat());

            Session["data"] = dtinvids;
            Panel2.Visible = true;
            lblMainCat.Visible = true;
            lblSubCat.Visible = true;
            lblSubsubCat.Visible = true;
            lblname.Visible = false;
            lblbarcode.Visible = false;
            lblproductnum.Visible = false;
            lblddlname.Visible = false;
            lblMainCat.Text = "Main Category : " + ddlInvCat.SelectedItem.Text;
            lblSubCat.Text = "Sub Category : " + ddlInvSubCat.SelectedItem.Text;
            lblSubsubCat.Text = "Sub Sub Category : " + ddlInvSubCat.SelectedItem.Text;
        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtSearchInvName.Text.Length > 0 || txtBarcode.Text.Length > 0 || txtProductNo.Text.Length > 0)
            {

                dtinvids = (DataTable)(SearchByName());
                Session["data"] = dtinvids;
                Panel2.Visible = true;
                lblname.Visible = true;
                lblbarcode.Visible = true;
                lblproductnum.Visible = true;
                lblMainCat.Visible = false;
                lblSubCat.Visible = false;
                lblSubsubCat.Visible = false;
                lblddlname.Visible = false;
                lblname.Text = "Name : " + txtSearchInvName.Text;
                lblbarcode.Text = "Barcode : " + txtBarcode.Text;
                lblproductnum.Text = "Product No :" + txtProductNo.Text;
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please input Inventory Name";
                Panel1.Visible = false;
            }
        }
        else if (RadioButtonList1.SelectedValue == "2")
        {
            //Panel2.Visible = false;
            Panel1.Visible = true;
            // FillDDlCatScatSScatName();

            string strDetail2 = " SELECT    InventoryWarehouseMasterTbl.*,InventoryMaster.InventoryMasterId,InventoryDetails.Inventory_Details_Id,InventoryWarehouseMasterTbl.Weight, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, " +
                    "  InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId, " +
                    "  InventoryCategoryMaster.InventoryCatName, left(InventoryMaster.Name,60) as Name , InventoryMaster.ProductNo, WareHouseMaster.Name AS WarehouseName ," +
                    "  left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + left(InventoruSubSubCategory.InventorySubSubName,15) " +
                //"   + ' : <br/>' + InventoryMaster.Name 
                    " AS CateAndName, InventoryBarcodeMaster.Barcode, InventoryDetails.Description,cast(InventoryWarehouseMasterTbl.Weight as nvarchar) +' '+ UnitTypeMaster.Name AS UnitTypeName, InventoryWarehouseMasterTbl.UnitTypeId " +
                    "  FROM    InventoryWarehouseMasterTbl     LEFT OUTER JOIN " +
                    "  UnitTypeMaster ON InventoryWarehouseMasterTbl.UnitTypeId = UnitTypeMaster.UnitTypeId  " +
                    "   inner join InventoryDetails on InventoryDetails.Inventory_Details_Id = InventoryWarehouseMasterTbl.InventoryDetailsId RIGHT OUTER JOIN " +
                    "  InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId inner join WareHouseMaster on WareHouseMaster.WareHouseId = InventoryWarehouseMasterTbl.WareHouseId LEFT OUTER JOIN " +
                    "  InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id LEFT OUTER JOIN " +
                    "  InventorySubCategoryMaster LEFT OUTER JOIN " +
                    "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
                    "  InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON " +
                    "  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId " +
                " where InventoryWarehouseMasterTbl.InventoryMasterId=" + Convert.ToInt32(ddlCatScSscNameofInv.SelectedValue) + " and InventoryMaster.MasterActiveStatus=1 " +
                " and InventoryWarehouseMasterTbl.WareHouseId=" + Convert.ToInt32(ddlWarehouse.SelectedValue) + " ";

            SqlCommand cmdDetail2 = new SqlCommand(strDetail2, con);
            SqlDataAdapter adpDetail2 = new SqlDataAdapter(cmdDetail2);
            DataTable dtDetail2 = new DataTable();
            adpDetail2.Fill(dtinvids);
            if (dtinvids.Rows.Count > 0)
            {
                //txtWeight.Text = dtDetail.Rows[0]["Weight"].ToString();
                //  txtRate.Text = dtinvids.Rows[0]["Rate"].ToString();
                //  txtreorderquantiy.Text = dtinvids.Rows[0]["ReorderQuantiy"].ToString();
                //  txtreorderlevel.Text = dtinvids.Rows[0]["ReorderLevel"].ToString();
                //  txtqtyondatestarted.Text = dtDetail2.Rows[0]["QtyOnDateStarted"].ToString();
                //  txtotyonhand.Text = dtinvids.Rows[0]["QtyOnHand"].ToString();
                //  txtnormalorderquantity.Text = dtinvids.Rows[0]["NormalOrderQuantity"].ToString();
                //string pviddd = dtinvids.Rows[0]["PreferredVendorId"].ToString();
                //ddlPreferedVendor.SelectedIndex = ddlPreferedVendor.Items.IndexOf(ddlPreferedVendor.Items.FindByValue(dtinvids.Rows[0]["PreferredVendorId"].ToString()));
                //chkActive.Checked = Convert.ToBoolean(dtinvids.Rows[0]["Active"]);
                //txtOpeingQty.Text = dtinvids.Rows[0]["OpeningQty"].ToString();
                //txtOpeingRAte.Text = dtinvids.Rows[0]["OpeningRate"].ToString();
                //hdninvExist.Value = "1";
                //ViewState["IWMi"] = Convert.ToInt32(dtDetail2.Rows[0]["InventoryWarehouseMasterId"]);
                //lblInvNameFromGrd.Text = dtDetail.Rows[0]["InvName"].ToString();
                //lblWareshouseFromGrd.Text = dtDetail.Rows[0]["WarehouseName"].ToString();


                //lblInvDetail.Visible = true;
                //lblInvDetail.Text = " Update Inventory Master " + dtinvids.Rows[0]["Name"].ToString() + " at " + dtinvids.Rows[0]["WarehouseName"].ToString() + " Location";
                //lblddlname.Visible = true;
                //lblddlname.Text = "Search by Name :" + ddlCatScSscNameofInv.SelectedItem.Text;
                DataView myDataView = new DataView();
                myDataView = dtinvids.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }

                grdInvMasters.DataSource = myDataView;
                grdInvMasters.DataBind();

            }
            else
            {


                ClearDetails();
                lblInvDetail.Visible = true;
                lblInvDetail.Text = " Insert Inventory Master " + ddlCatScSscNameofInv.SelectedItem.Text + " at " + Convert.ToString(ddlWarehouse.SelectedItem.Text) + " Location";

                hdninvExist.Value = "0";
            }

        }
        else
        {

        }
        if (dtinvids.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = dtinvids.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            grdInvMasters.DataSource = dtinvids;
            grdInvMasters.DataBind();

            //grdInvMasters.DataSource = dtinvids;
            //grdInvMasters.DataBind();

        }
        else
        {
            // Panel1.Visible = false;
            grdInvMasters.DataSource = null;
            grdInvMasters.DataBind();
            //grdInvMasters.EmptyDataText = "No Record";


        }
        //EventArgs ee = new EventArgs();
        //btnSearchGo_Click(sender, ee);
    }
    protected void txtotyonhand_TextChanged(object sender, EventArgs e)
    {

    }
    protected void grdInvMasters_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;

        ImgBtnSearchGo_Click(sender, e);

    }
    protected void grdInvMasters_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            Panel1.ScrollBars = ScrollBars.None;
            Panel1.Height = new Unit("100%");

            btncancel0.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (grdInvMasters.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdInvMasters.Columns[6].Visible = false;
            }


        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(300);

            btncancel0.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["editHide"] != null)
            {
                grdInvMasters.Columns[6].Visible = true;
            }


        }
    }
    protected void datefill()
    {
        string openingdate = "select * from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' and Active='1'";
        SqlCommand cmd22221 = new SqlCommand(openingdate, con);
        SqlDataAdapter adp22221 = new SqlDataAdapter(cmd22221);
        DataTable ds112221 = new DataTable();
        adp22221.Fill(ds112221);

        if (ds112221.Rows.Count > 0)
        {
            DateTime t1;
            DateTime t2;

            t1 = Convert.ToDateTime(ds112221.Rows[0]["StartDate"].ToString());
            t2 = Convert.ToDateTime(ds112221.Rows[0]["EndDate"].ToString());

            ViewState["StartDate"] = t1.ToShortDateString();
            ViewState["EndDate"] = t2.ToShortDateString();

            ViewState["Report_Period_Id"] = ds112221.Rows[0]["Report_Period_Id"].ToString();


        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        DateTime d1 = Convert.ToDateTime(ViewState["StartDate"].ToString());
        DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        int day = d2.Subtract(d1).Days;

        string strcurrent = " select sum(cast(Qty as decimal)) as Qty from InventoryWarehouseMasterAvgCostTbl where cast(Qty as decimal)<0 and InvWMasterId='" + ViewState["IWMi"].ToString() + "' and DateUpdated between '" + ViewState["StartDate"].ToString() + "' and '" + DateTime.Now.ToShortDateString() + "' ";
        SqlCommand cmdcurrent = new SqlCommand(strcurrent, con);
        SqlDataAdapter adpcurrent = new SqlDataAdapter(cmdcurrent);
        DataTable dtcurrent = new DataTable();
        adpcurrent.Fill(dtcurrent);

        double finalamount = 0;
        double totalusage = 0;

        if (dtcurrent.Rows.Count > 0)
        {
            string strtemp1 = dtcurrent.Rows[0]["Qty"].ToString();
            strtemp1 = strtemp1.Replace("-", "+");
            finalamount = Convert.ToDouble(strtemp1);
            totalusage = Math.Round(finalamount / day, 0);

        }
        txtleaddaysusage.Text = totalusage.ToString();



        if (txtleaddays.Text != "" && txtleaddaysusage.Text != "" && txtsafetystock.Text != "")
        {
            decimal reorderlevel = Math.Round((Convert.ToDecimal(txtleaddays.Text) * Convert.ToDecimal(txtleaddaysusage.Text)) + Convert.ToDecimal(txtsafetystock.Text), 0);
            txtreorderlevel.Text = reorderlevel.ToString();
        }
        else
        {
            txtreorderlevel.Text = "0";
        }
    }
}

