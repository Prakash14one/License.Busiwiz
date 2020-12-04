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
public partial class account_InventoryAdjustMaster : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(PageConn.connnn);
    int i;
    object com;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        com = Session["comid"];
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();
        char[] separator = new char[] { '/' };
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Page.Title = pg.getPageTitle(page);

        if (!IsPostBack)
        {
            Label5.Text = Session["Cname"].ToString();
            lblCompany.Text = Session["Cname"].ToString();
            lblUserFromSession.Text = Session["uname"].ToString();

            ViewState["sortOrder"] = "";
            txtDate.Text = System.DateTime.Now.ToShortDateString();

            fillwarehouse();
            datefill();
            fillfiletrwarehouse();
            ddlWarehouse_SelectedIndexChanged(sender, e);
            filladjustmentreason();
            account1();
            account2();

            fillgirdadjust();

            if (RadioButtonList1.SelectedValue == "0")
            {
                pnlInvCat.Visible = true;
                pnlInvName.Visible = false;


            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                pnlInvCat.Visible = false;
                pnlInvName.Visible = true;

            }

            else
            {
                pnlInvCat.Visible = false; ;
                pnlInvName.Visible = false;

            }









            if (Request.QueryString["id"] != null)
            {
                int iwhid = Convert.ToInt32(Request.QueryString["id"]);
                int did = Convert.ToInt32(Request.QueryString["id2"]);
                string strqforiw = " SELECT     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryMaster.InventoryMasterId, InventoruSubSubCategory.InventorySubSubId,  " +
                    "  InventorySubCategoryMaster.InventorySubCatId, InventoryCategoryMaster.InventeroyCatId, InventoryWarehouseMasterTbl.WareHouseId,  " +
                    "  InventoryAdjustMaster.InventoryAdjustReasonId,   InventoryAdjustMaster.Datetime, InventoryAdjustMaster.InventoryAdjustTitle " +
                     ",  InventoryAdjustMaster.InventoryAdjustMasterId, InventoryAdjustDetails.QuantityAdjusted, InventoryAdjustDetails.InventoryAdjustDetailsId,InventoryMaster.Name as InvName" +
                      " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                    "  InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                    "  InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
                    "  InventoryMaster RIGHT OUTER JOIN " +
                    "  InventoryWarehouseMasterTbl RIGHT OUTER JOIN " +
                    "  InventoryAdjustDetails ON InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = InventoryAdjustDetails.InventoryWHM_Id RIGHT OUTER JOIN " +
                    "  InventoryAdjustMaster ON InventoryAdjustDetails.InventoryAdjustMasterId = InventoryAdjustMaster.InventoryAdjustMasterId ON  " +
                    "  InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId ON  " +
                    "  InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId " +
                      " where InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=" + iwhid + " and InventoryAdjustDetails.InventoryAdjustDetailsId=" + did + " ";
                SqlCommand cmdid = new SqlCommand(strqforiw, con);
                SqlDataAdapter adpid = new SqlDataAdapter(cmdid);
                DataTable dtid = new DataTable();
                adpid.Fill(dtid);
                if (dtid.Rows.Count > 0)
                {


                    ddlInventoryAdjustReasonName.SelectedIndex = ddlInventoryAdjustReasonName.Items.IndexOf(ddlInventoryAdjustReasonName.Items.FindByValue(dtid.Rows[0]["InventoryAdjustReasonId"].ToString()));
                    ddlWarehouse.SelectedIndex = ddlInvCat.Items.IndexOf(ddlWarehouse.Items.FindByValue(dtid.Rows[0]["WareHouseId"].ToString()));
                    txtDate.Text = dtid.Rows[0]["Datetime"].ToString();
                    txtInvAdjustTitle.Text = dtid.Rows[0]["InventoryAdjustTitle"].ToString();
                    RadioButtonList1.SelectedValue = "1";
                    RadioButtonList1_SelectedIndexChanged(sender, e);
                    //InventoryMaster.Name
                    ViewState["adj"] = dtid.Rows[0]["QuantityAdjusted"].ToString();
                    txtSearchInvName.Text = dtid.Rows[0]["InvName"].ToString();
                    Session["uname"] = "";
                    btnSearchGo_Click(sender, e);



                }


            }




            DataSet ds = (DataSet)fillddl2();
            GridView1.DataSource = ds;
            DataView myDataView = new DataView();
            myDataView = ds.Tables[0].DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataBind();

        }

    }

    protected void filladjustreasongrid()
    {
        DataSet ds = (DataSet)fillddl2();
        GridView1.DataSource = ds;
        DataView myDataView = new DataView();
        myDataView = ds.Tables[0].DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();
    }
    public DataSet fillddl()
    {
        SqlCommand cmd = new SqlCommand("Sp_select_InventoryMasterForItem", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    public DataSet fillddl2()
    {
        SqlCommand cmd = new SqlCommand("Sp_Select_InventoryAdjustReasonMasterForItem", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        cmd.Parameters.AddWithValue("@compid", Session["comid"].ToString());
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //////////////if (ddlInventoryName.SelectedIndex == 0)
        //////////////{

        //////////////    args.IsValid = false;

        //////////////    i = 1;

        //////////////}
        //////////////else
        //////////////{
        //////////////    args.IsValid = true;
        //////////////    i = 0;
        //////////////}
    }
    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ddlInventoryAdjustReasonName.SelectedIndex == 0)
        {
            args.IsValid = false;

            i = 1;

        }
        else
        {
            args.IsValid = true;
            i = 0;
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Button3_Click(sender, e);

        if (ddltypeofadjustment.SelectedValue == "0")
        {
            if (ViewState["totalnormaladjustamount"] != null)
            {
                if (ViewState["totalnormaladjustamount"].ToString() != "0")
                {
                    ModalPopupExtender2.Show();
                    lblabnormalinventorylossdebitcerdit.Text = "Debit";
                    lblreductionininventorydebitcredit.Text = "Credit";

                    if (Convert.ToDouble(ViewState["totalnormaladjustamount"]) > 0)
                    {
                        string acc1id = "8000";
                        string acc2id = "9201";

                        ddlacc1.SelectedIndex = ddlacc1.Items.IndexOf(ddlacc1.Items.FindByValue(acc1id.ToString()));
                        ddlacc2.SelectedIndex = ddlacc2.Items.IndexOf(ddlacc2.Items.FindByValue(acc2id.ToString()));
                    }
                    else
                    {
                        string acc1id = "5500";
                        string acc2id = "8000";

                        ddlacc1.SelectedIndex = ddlacc1.Items.IndexOf(ddlacc1.Items.FindByValue(acc1id.ToString()));
                        ddlacc2.SelectedIndex = ddlacc2.Items.IndexOf(ddlacc2.Items.FindByValue(acc2id.ToString()));

                    }

                    double totalamount = 0;
                    totalamount = Convert.ToDouble(ViewState["totalnormaladjustamount"]);
                    double finalamount = 0;

                    if (totalamount >= 0)
                    {
                        finalamount = totalamount;

                    }
                    else
                    {
                        string strtemp1 = totalamount.ToString();
                        strtemp1 = strtemp1.Replace("-", "+");
                        finalamount = Convert.ToDouble(strtemp1);

                    }
                    lblabnormalinventorylossamount.Text = finalamount.ToString();
                    lblreductionininventoryamount.Text = finalamount.ToString();
                }
                else
                {
                    insertadjustmaster();
                }
            }


        }
        else if (ddltypeofadjustment.SelectedValue == "1")
        {

            //if (ViewState["Flag"] != null)
            //{
            //    if (ViewState["Flag"].ToString() == "1")
            //    {
            //        ModalPopupExtender1.Show();

            //    }
            //    else
            //    {
            //        insertadjustmaster();
            //    }
            //}

        }
        else if (ddltypeofadjustment.SelectedValue == "2")
        {

            if (ViewState["Flag"] != null)
            {
                if (ViewState["Flag"].ToString() == "1")
                {
                    ModalPopupExtender1.Show();

                }
                else
                {
                    insertadjustmaster();
                }
            }

        }



    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ddlInventoryAdjustReasonName.SelectedIndex = 0;

        txtDate.Text = System.DateTime.Now.ToShortDateString();
        txtInvAdjustTitle.Text = "";

        txtSearchInvName.Text = "";


        grdInvMasters.DataSource = null;
        grdInvMasters.DataBind();
        gridvi.DataSource = null;
        gridvi.DataBind();
        lblUserFromSession.Text = Session["uname"].ToString();

        grdInvMasters.EmptyDataText = "";
        ddlInvCat_SelectedIndexChanged(sender, e);

        pnladd.Visible = false;
        pnladd.Enabled = true;
        btnadd.Visible = true;
        pnlforview.Visible = false;
        Button3.Visible = false;
        ImageButton2.Visible = false;
        Panel6.Visible = false;
        Label1.Text = "";
        ImageButton3.Visible = false;

        txtadjustmentdetail.Text = "";
        txtInvAdjustTitle.Text = "";
        txtmemo1.Text = "";
        txtmemo2.Text = "";

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            pnlInvCat.Visible = true;
            pnlInvName.Visible = false;
            FillddlInvCat();
            btnSearchGo.Text = "Go";

        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            pnlInvCat.Visible = false;
            pnlInvName.Visible = true;
            btnSearchGo.Text = "Search";

        }

        else
        {
            pnlInvCat.Visible = false; ;
            pnlInvName.Visible = false;

        }
    }
    protected void FillddlInvCat()
    {


        ddlInvCat.Items.Clear();
        string strcat = "SELECT Distinct  InventoryCategoryMaster.InventeroyCatId,InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ddlWarehouse.SelectedValue + "' and InventoryCategoryMaster.CatType IS Null order by InventoryCatName";

        SqlCommand cmdcat = new SqlCommand(strcat, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);
        if (dtcat.Rows.Count > 0)
        {
            ddlInvCat.DataSource = dtcat;
            ddlInvCat.DataTextField = "InventoryCatName";
            ddlInvCat.DataValueField = "InventeroyCatId";
            ddlInvCat.DataBind();
            ddlInvCat.Items.Insert(0, "All");
            ddlInvCat.Items[0].Value = "0";
        }
        else
        {
            ddlInvCat.Items.Insert(0, "All");
            ddlInvCat.Items[0].Value = "0";
        }
        object c = new object();
        EventArgs r = new EventArgs();


        ddlInvCat_SelectedIndexChanged(c, r);




    }
    protected void ddlInvCat_SelectedIndexChanged(object sender, EventArgs e)
    {


        ddlInvSubCat.Items.Clear();

        if (Convert.ToInt32(ddlInvCat.SelectedIndex) > 0)
        {
            string strsubcat = "SELECT InventorySubCatId  ,InventorySubCatName ,InventoryCategoryMasterId  FROM InventorySubCategoryMaster " +
                            " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";
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
        }
        else
        {
            ddlInvSubCat.Items.Insert(0, "All");
            ddlInvSubCat.Items[0].Value = "0";
        }
        ddlInvSubCat_SelectedIndexChanged(sender, e);
    }
    protected void ddlInvSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlInvSubSubCat.Items.Clear();

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
            ddlInvSubSubCat.Items.Insert(0, "All");
            ddlInvSubSubCat.Items[0].Value = "0";
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
            //string strinvname = "SELECT InventoryMasterId ,Name ,InventoryDetailsId ,InventorySubSubId   ,ProductNo ,InventoryTypeId  FROM InventoryMaster " +
            //                " where InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + " and InventoryMaster.MasterActiveStatus=1  ";

            string strinvname = "SELECT InventoryMaster.Name,InventoryMaster.InventoryMasterId  FROM InventoryMaster inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId " +
                           " where InventoryMaster.InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + " and InventoryMaster.MasterActiveStatus=1 and InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "' and InventoryMaster.CatType IS Null order by InventoryMaster.Name  ";

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
        }
        else
        {
            ddlInvName.Items.Insert(0, "All");
            ddlInvName.Items[0].Value = "0";
        }


    }

    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        
            if (ddltypeofadjustment.SelectedValue == "0")
            {
                Label1.Visible = true;
                Label1.Text = "";

                grdInvMasters.Columns[8].Visible = true;
                grdInvMasters.Columns[9].Visible = true;
                grdInvMasters.Columns[10].Visible = true;
                grdInvMasters.Columns[11].Visible = false;
                grdInvMasters.Columns[12].Visible = false;
                grdInvMasters.Columns[13].Visible = false;
                grdInvMasters.Columns[14].Visible = false;
                grdInvMasters.Columns[15].Visible = false;
                CheckBox1_CheckedChanged(sender, e);
                fillgrid();

            }
            else if (ddltypeofadjustment.SelectedValue == "1")
            {
                if (CheckBox1.Checked == true)
                {
                    grdInvMasters.Columns[8].Visible = false;
                    grdInvMasters.Columns[9].Visible = false;
                    grdInvMasters.Columns[10].Visible = false;
                    grdInvMasters.Columns[11].Visible = true;
                    grdInvMasters.Columns[12].Visible = false;
                    grdInvMasters.Columns[13].Visible = true;
                    grdInvMasters.Columns[14].Visible = true;
                    grdInvMasters.Columns[15].Visible = true;
                    CheckBox1_CheckedChanged(sender, e);
                    fillgrid();
                    Label1.Visible = true;
                    Label1.Text = "";
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "You cannot increase the value of inventory, this is against the International Accounting Standard - 2.";
                    Panel6.Visible = false;

                }

            }
            else if (ddltypeofadjustment.SelectedValue == "2")
            {
                Label1.Visible = true;
                Label1.Text = "";

                grdInvMasters.Columns[8].Visible = false;
                grdInvMasters.Columns[9].Visible = false;
                grdInvMasters.Columns[10].Visible = false;
                grdInvMasters.Columns[11].Visible = true;
                grdInvMasters.Columns[12].Visible = false;
                grdInvMasters.Columns[13].Visible = true;
                grdInvMasters.Columns[14].Visible = true;
                grdInvMasters.Columns[15].Visible = false;

                CheckBox1_CheckedChanged(sender, e);

                fillgrid();

            }

         
    }
    public DataTable SeachByCat()
    {


        string strinv = " SELECT     InventoryMaster.InventoryMasterId, InventoryDetails.Inventory_Details_Id, Convert(nvarchar(50),InventoryWarehouseMasterTbl.Weight )+ ' / ' + UnitTypeMaster.Name as Weight, InventoruSubSubCategory.InventorySubSubId, " +
                     " InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, " +
                     " InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventoryMaster.Name, InventoryMaster.ProductNo, " +
                     " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName + ' : ' + InventoruSubSubCategory.InventorySubSubName " +
                     "  AS CateAndName, InventoryBarcodeMaster.Barcode,  InventoryDetails.Description, " +
                     " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.WareHouseId, " +
                     " WareHouseMaster.Name AS Warehouse " +
                     " FROM         InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                     " WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId LEFT OUTER JOIN " +
                     " InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId LEFT OUTER JOIN " +
                     " InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN " +
                     " InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id LEFT OUTER JOIN " +
                     " InventorySubCategoryMaster LEFT OUTER JOIN " +
                     " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
                     " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON " +
                      " InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId inner join UnitTypeMaster on UnitTypeMaster.UnitTypeId = InventoryDetails.UnitTypeId  " +
                      " where InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' and InventoryCategoryMaster.compid='" + com + "' and InventoryMaster.MasterActiveStatus='1' and InventoryMaster.CatType IS Null";

        string strInvId = "";
        string strInvsubsubCatId = "";
        string strInvsubcatid = "";
        string strInvCatid = "";


        if (ddlInvCat.SelectedIndex > 0)
        {
            strInvCatid = " and InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

        }
        if (ddlInvSubCat.SelectedIndex > 0)
        {
            strInvsubcatid = " and InventorySubCategoryMaster.InventorySubCatId = " + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " ";
        }
        if (ddlInvSubSubCat.SelectedIndex > 0)
        {
            strInvsubsubCatId = " and InventoruSubSubCategory.InventorySubSubId=" + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "";
        }
        if (ddlInvName.SelectedIndex > 0)
        {
            strInvId = " and InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

        }

        string orderby = " order by Warehouse,CateAndName,Name ";

        string mainStringCat = strinv + strInvCatid + strInvsubcatid + strInvsubsubCatId + strInvId + orderby;


        SqlCommand cmdcat = new SqlCommand(mainStringCat, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);
        return dtcat;

    }
    public DataTable SearchByName()
    {
        string strinvMainName = " SELECT     InventoryMaster.InventoryMasterId, InventoryDetails.Inventory_Details_Id, Convert(nvarchar(50),InventoryWarehouseMasterTbl.Weight )+ ' / ' + UnitTypeMaster.Name as Weight, InventoruSubSubCategory.InventorySubSubId, " +
                     " InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, " +
                     " InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventoryMaster.Name, InventoryMaster.ProductNo, " +
                     " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName + ' : ' + InventoruSubSubCategory.InventorySubSubName " +
                     "  AS CateAndName, InventoryBarcodeMaster.Barcode,  InventoryDetails.Description, " +
                     " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.WareHouseId, " +
                     " WareHouseMaster.Name AS Warehouse " +
                     " FROM         InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                     " WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId LEFT OUTER JOIN " +
                     " InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId LEFT OUTER JOIN " +
                     " InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN " +
                     " InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id LEFT OUTER JOIN " +
                     " InventorySubCategoryMaster LEFT OUTER JOIN " +
                     " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
                     " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON " +
                      " InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId inner join UnitTypeMaster on UnitTypeMaster.UnitTypeId = InventoryDetails.UnitTypeId  " +

                    "   WHERE    ( (InventoryMaster.Name like '%" + txtSearchInvName.Text.Replace("'", "''") + "%') " +

                    " or (InventoryBarcodeMaster.Barcode= '" + txtSearchInvName.Text + "') or (InventoryMaster.ProductNo='" + txtSearchInvName.Text + "') ) and InventoryMaster.MasterActiveStatus=1  and  WareHouseMaster.WareHouseId='" + ddlWarehouse.SelectedValue + "' and  InventoryCategoryMaster.compid='" + Session["comid"] + "' and InventoryMaster.CatType IS Null ";

        string orderby = "  order by CateAndName  ";


        string strmaininvname = strinvMainName + orderby;
        SqlCommand cmdinvname = new SqlCommand(strmaininvname, con);
        SqlDataAdapter adpinvname = new SqlDataAdapter(cmdinvname);
        DataTable dtinvname = new DataTable();

        adpinvname.Fill(dtinvname);
        return dtinvname;

    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        datefill();

        FillddlInvCat();
        account1();
        account2();
    }

    protected void grdInvMasters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }

    }
    protected void grdInvMasters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblInvWHMasterId = (Label)e.Row.FindControl("lblInvWHMasterId");
            Label lblQtyOnHand = (Label)e.Row.FindControl("lblQtyOnHand");
            Label lblavgrate = (Label)e.Row.FindControl("lblavgrate");
            Label lblvalue = (Label)e.Row.FindControl("lblvalue");

            Label lblpreviousbalanceofreduction = (Label)e.Row.FindControl("lblpreviousbalanceofreduction");

            




            // current qty,current rate ,current amt
            string strcurrent = "SELECT top(1) * FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + lblInvWHMasterId.Text + "' and DateUpdated<='" + txtDate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc";
            SqlCommand cmdcurrent = new SqlCommand(strcurrent, con);
            SqlDataAdapter adpcurrent = new SqlDataAdapter(cmdcurrent);
            DataTable dtcurrent = new DataTable();
            adpcurrent.Fill(dtcurrent);

            if (dtcurrent.Rows.Count > 0)
            {
                if (Convert.ToString(dtcurrent.Rows[0]["QtyonHand"].ToString()) != "" && Convert.ToString(dtcurrent.Rows[0]["AvgCost"].ToString()) != "")
                {
                    lblQtyOnHand.Text = dtcurrent.Rows[0]["QtyonHand"].ToString();

                    lblavgrate.Text = dtcurrent.Rows[0]["AvgCost"].ToString();

                    double totalcurrentamt = Math.Round(Convert.ToDouble(dtcurrent.Rows[0]["QtyonHand"].ToString()) * Convert.ToDouble(dtcurrent.Rows[0]["AvgCost"].ToString()), 2);
                    if (totalcurrentamt <= 0)
                    {
                        totalcurrentamt = 0;
                        lblvalue.Text = totalcurrentamt.ToString();
                    }
                    else
                    {
                        // lblamountbfr.Text = totalcurrentamt.ToString("###,###.##");
                        lblvalue.Text = totalcurrentamt.ToString();
                    }
                }
            }
            // end current qty,current rate ,current amt

            // previous amount balance

            if (ddltypeofadjustment.SelectedValue == "1")
            {

                string strdecreseamt = "SELECT sum(InventoryAdjustDetails.DecreaseAmount) as DecreaseAmount , sum(InventoryAdjustDetails.IncreaseAmount) as IncreaseAmount from InventoryAdjustDetails inner join InventoryAdjustMaster on InventoryAdjustMaster.InventoryAdjustMasterId=InventoryAdjustDetails.InventoryAdjustMasterId  where InventoryAdjustDetails.InventoryWHM_Id='" + lblInvWHMasterId.Text + "' and Datetime between '" + ViewState["StartDate"].ToString() + "' and '" + ViewState["EndDate"].ToString() + "' ";
                SqlCommand cmddecreseamt = new SqlCommand(strdecreseamt, con);
                SqlDataAdapter adpdecreseamt = new SqlDataAdapter(cmddecreseamt);
                DataTable dtdecreseamt = new DataTable();
                adpdecreseamt.Fill(dtdecreseamt);
                if (dtdecreseamt.Rows.Count > 0)
                {
                    decimal decrementamount = 0;
                    decimal incrementamount = 0;

                    if (dtdecreseamt.Rows[0]["DecreaseAmount"].ToString() != "")
                    {
                        decrementamount = Convert.ToDecimal(dtdecreseamt.Rows[0]["DecreaseAmount"].ToString());
                    }
                    if (dtdecreseamt.Rows[0]["IncreaseAmount"].ToString() != "")
                    {
                        incrementamount = Convert.ToDecimal(dtdecreseamt.Rows[0]["IncreaseAmount"].ToString());
                    }

                    decimal finalamount = decrementamount - incrementamount;

                    lblpreviousbalanceofreduction.Text = finalamount.ToString();
                }
                else
                {
                    lblpreviousbalanceofreduction.Text = "0";
                }

                if (lblpreviousbalanceofreduction.Text == "0")
                {
                    e.Row.Enabled = false;
                }

            }

            // end previous balance





        }

    }
    protected void grdInvMasters_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }



    protected void grdInvMasters_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdInvMasters_Sorted(object sender, EventArgs e)
    {

    }
    protected void grdInvMasters_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        btnSearchGo_Click(sender, e);

    }

    protected void txtAdjustQty_TextChanged(object sender, EventArgs e)
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

    protected void imgbtnAddNewCat_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImgBtnInsertReason_Click(object sender, EventArgs e)
    {
        try
        {
            string strSelectMasterId1 = "select InventoryAdjustReasonId from InventoryAdjustReasonMaster  where InventoryAdjustReasonName= '" + txtreason.Text + "'  and compid='" + Session["comid"] + "' ";
            SqlCommand cmdSelectMasterId1 = new SqlCommand(strSelectMasterId1, con);
            SqlDataAdapter adpSelectMasterId1 = new SqlDataAdapter(cmdSelectMasterId1);
            DataTable dtSelectMasterId1 = new DataTable();
            adpSelectMasterId1.Fill(dtSelectMasterId1);
            if (dtSelectMasterId1.Rows.Count == 0)
            {
                string inststr = "INSERT INTO InventoryAdjustReasonMaster (InventoryAdjustReasonName,compid) " +
                                 " VALUES('" + txtreason.Text + "','" + Session["comid"] + "') ";
                SqlCommand cmdinst = new SqlCommand(inststr, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdinst.ExecuteNonQuery();
                con.Close();
                Label2.Visible = true;
                Label2.Text = "Record inserted successfully ";
                txtreason.Text = "";
                ModalPopupExtender1333.Show();
                DataSet ds = (DataSet)fillddl2();
                GridView1.DataSource = ds;
                DataView myDataView = new DataView();
                myDataView = ds.Tables[0].DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }
                GridView1.DataBind();
            }
            else
            {
                Label2.Visible = true;
                Label2.Text = "Record alrady exist";
            }

        }
        catch (Exception erere)
        {
            Label2.Visible = true;
            Label2.Text = "error :" + erere.Message;

        }
        finally
        {




        }
        filladjustmentreason();

    }
    protected void imgbtnGotoAdjustInv_Click(object sender, EventArgs e)
    {
        filladjustmentreason();

        ModalPopupExtender1333.Hide();
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ddlInventoryAdjustReasonName.DataSource = (DataSet)fillddl2();
        ddlInventoryAdjustReasonName.DataTextField = "InventoryAdjustReasonName";
        ddlInventoryAdjustReasonName.DataValueField = "InventoryAdjustReasonId";
        ddlInventoryAdjustReasonName.DataBind();
        ddlInventoryAdjustReasonName.Items.Insert(0, "--Select--");
        ddlInventoryAdjustReasonName.Items[0].Value = "0";
        // ModalPopupExtender1333.Hide();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        // ModalPopupExtender1333.Show();
        GridView1.EditIndex = -1;
        DataSet ds = (DataSet)fillddl2();
        GridView1.DataSource = ds;
        DataView myDataView = new DataView();
        myDataView = ds.Tables[0].DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label2.Text = "";
        if (e.CommandName == "Delete1")
        {

            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sr4 = ("delete from InventoryAdjustReasonMaster where InventoryAdjustReasonId=" + id + "");
            SqlCommand cmd8 = new SqlCommand(sr4, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd8.ExecuteNonQuery();
            con.Close();

            filladjustreasongrid();

            Label2.Visible = true;
            Label2.Text = "Record deleted successfully";
            ModalPopupExtender1333.Show();

        }

        if (e.CommandName == "Edit1")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["adjustreasonid"] = id.ToString();

            string strinv = " select * from InventoryAdjustReasonMaster where InventoryAdjustReasonId='" + id + "'";

            SqlCommand cmdview = new SqlCommand(strinv, con);
            SqlDataAdapter dtpview = new SqlDataAdapter(cmdview);
            DataTable dtview = new DataTable();
            dtpview.Fill(dtview);
            if (dtview.Rows.Count > 0)
            {
                txtreason.Text = dtview.Rows[0]["InventoryAdjustReasonName"].ToString();

                ImgBtnInsertReason.Visible = false;
                Button11.Visible = true;
            }
            ModalPopupExtender1333.Show();

        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void imgbtnAddNewReason_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();

        //Response.Redirect("InventoryAdjustReasonMaster.aspx");
        DataSet ds = (DataSet)fillddl2();
        GridView1.DataSource = ds;
        DataView myDataView = new DataView();
        myDataView = ds.Tables[0].DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();
        Label2.Text = "";
        txtreason.Text = "";

        ModalPopupExtender1333.Show();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        //ViewState["Id"] = GridView1.DataKeys[e.RowIndex].Value;

        //GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());

        //string sr4 = ("delete from InventoryAdjustReasonMaster where InventoryAdjustReasonId=" + ViewState["rid"] + "");
        //SqlCommand cmd8 = new SqlCommand(sr4, con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmd8.ExecuteNonQuery();
        //con.Close();
        //ModalPopupExtender1444.Hide();
        //ModalPopupExtender1333.Show();

        //GridView1.EditIndex = -1;
        //DataSet ds = (DataSet)fillddl2();
        //GridView1.DataSource = ds;
        //DataView myDataView = new DataView();
        //myDataView = ds.Tables[0].DefaultView;

        //if (hdnsortExp.Value != string.Empty)
        //{
        //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //}
        //GridView1.DataBind();

        //Label2.Visible = true;
        //Label2.Text = "Record deleted successfully";


    }


    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void fillgirdadjust()
    {
        Label39.Text = ddlfilterbybusiness.SelectedItem.Text;

        string str = " SELECT     InventoryAdjustMaster.InventoryAdjustMasterId, InventoryAdjustMaster.AdjustDetail,WareHouseMaster.Name as Wname,convert(nvarchar(10),InventoryAdjustMaster.Datetime,101) as Datetime, InventoryAdjustMaster.UserId, InventoryAdjustMaster.InventoryAdjustReasonId, InventoryAdjustMaster.InventoryAdjustTitle,InventoryAdjustReasonMaster.InventoryAdjustReasonName, User_master.Name FROM   InventoryAdjustMaster LEFT OUTER JOIN InventoryAdjustReasonMaster ON InventoryAdjustMaster.InventoryAdjustReasonId = InventoryAdjustReasonMaster.InventoryAdjustReasonId LEFT OUTER JOIN User_master ON InventoryAdjustMaster.UserId = User_master.UserID inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryAdjustMaster.Whid where InventoryAdjustMaster.compid='" + Session["comid"] + "' ";

        string business = "";
        if (ddlfilterbybusiness.SelectedIndex > 0)
        {
            business = " and InventoryAdjustMaster.Whid='" + ddlfilterbybusiness.SelectedValue + "'  ";
        }

        string orderby = "order by Wname,InventoryAdjustMaster.InventoryAdjustTitle ";
        string finalstr = str + business + orderby;
        SqlCommand cmdadjust = new SqlCommand(finalstr, con);
        SqlDataAdapter dtpadjust = new SqlDataAdapter(cmdadjust);
        DataTable dtadjust = new DataTable();
        dtpadjust.Fill(dtadjust);



        gridviewadjust.DataSource = dtadjust;
        gridviewadjust.DataBind();

    }
    protected void gridviewadjust_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "Edit1")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["adjid"] = id.ToString();

            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView2.Visible = true;

            string strinv = " SELECT     InventoryMaster.InventoryMasterId, InventoryDetails.Inventory_Details_Id, Convert(nvarchar(50),InventoryWarehouseMasterTbl.Weight )+ ' / ' + UnitTypeMaster.Name as Weight, InventoruSubSubCategory.InventorySubSubId, " +
                    " InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, " +
                    " InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventoryMaster.Name, InventoryMaster.ProductNo, " +
                    " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName + ' : ' + InventoruSubSubCategory.InventorySubSubName " +
                    "  AS CateAndName, InventoryBarcodeMaster.Barcode,  InventoryDetails.Description, " +
                    " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.WareHouseId, " +
                    " WareHouseMaster.Name AS Warehouse,InventoryAdjustDetails.*,InventoryAdjustMaster.* " +
                    " FROM         InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                    " WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId LEFT OUTER JOIN " +
                    " InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId LEFT OUTER JOIN " +
                    " InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN " +
                    " InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id LEFT OUTER JOIN " +
                    " InventorySubCategoryMaster LEFT OUTER JOIN " +
                    " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
                    " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON " +
                     " InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId RIGHT OUTER JOIN InventoryAdjustDetails ON InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = InventoryAdjustDetails.InventoryWHM_Id RIGHT OUTER JOIN InventoryAdjustMaster ON InventoryAdjustDetails.InventoryAdjustMasterId = InventoryAdjustMaster.InventoryAdjustMasterId inner join UnitTypeMaster on UnitTypeMaster.UnitTypeId = InventoryDetails.UnitTypeId" +
                     " where  InventoryAdjustMaster.InventoryAdjustMasterId = '" + id + "' ";


            SqlCommand cmdview = new SqlCommand(strinv, con);
            SqlDataAdapter dtpview = new SqlDataAdapter(cmdview);
            DataTable dtview = new DataTable();
            dtpview.Fill(dtview);

            if (dtview.Rows.Count > 0)
            {
                GridView2.DataSource = dtview;
                GridView2.DataBind();



                fillwarehouse();
                ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByValue(dtview.Rows[0]["Whid"].ToString()));

                filladjustmentreason();
                if (dtview.Rows[0]["InventoryAdjustReasonId"].ToString() != "")
                {
                    ddlInventoryAdjustReasonName.SelectedIndex = ddlInventoryAdjustReasonName.Items.IndexOf(ddlInventoryAdjustReasonName.Items.FindByValue(dtview.Rows[0]["InventoryAdjustReasonId"].ToString()));
                }
                txtInvAdjustTitle.Text = dtview.Rows[0]["InventoryAdjustTitle"].ToString();
                txtDate.Text = "";
                txtDate.Text = dtview.Rows[0]["Datetime"].ToString();

                SqlCommand cmduser = new SqlCommand("SELECT Name, UserID FROM User_master WHERE (UserID = '" + dtview.Rows[0]["UserId"].ToString() + "')", con);
                SqlDataAdapter dtpuser = new SqlDataAdapter(cmduser);
                DataTable dtuser = new DataTable();

                dtpuser.Fill(dtuser);
                if (dtuser.Rows.Count > 0)
                {
                    lblUserFromSession.Text = dtuser.Rows[0][0].ToString();
                }
                txtadjustmentdetail.Text = dtview.Rows[0]["AdjustDetail"].ToString();

                if (dtview.Rows[0]["TypeOfAdjust"].ToString() == "0")
                {
                    ddltypeofadjustment.SelectedValue = "0";

                    GridView2.Columns[8].Visible = true;
                    GridView2.Columns[9].Visible = true;
                    GridView2.Columns[10].Visible = true;
                    GridView2.Columns[11].Visible = false;
                    GridView2.Columns[12].Visible = false;
                    GridView2.Columns[13].Visible = false;
                }
                else
                {
                    ddltypeofadjustment.SelectedValue = "1";

                    GridView2.Columns[8].Visible = false;
                    GridView2.Columns[9].Visible = false;
                    GridView2.Columns[10].Visible = false;
                    GridView2.Columns[11].Visible = true;
                    GridView2.Columns[12].Visible = true;
                    GridView2.Columns[13].Visible = true;
                }



                pnladd.Visible = true;
                pnladd.Enabled = false;
                Label6.Text = "Edit Inventory Adjustment ";
                btnadd.Visible = false;

                ImageButton3.Visible = true;
                ImageButton2.Visible = false;
                Button3.Visible = false;
                GridView2.Visible = true;

                pnlforview.Visible = true;
                Panel6.Visible = false;
            }
        }

        if (e.CommandName == "Delete1")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            string stremp = " select * from InventoryAdjustMaster where InventoryAdjustMasterId='" + id + "' ";
            SqlCommand cmdemp = new SqlCommand(stremp, con);
            SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
            DataTable dsemp = new DataTable();
            adpemp.Fill(dsemp);


            if (dsemp.Rows.Count > 0)
            {
                SqlCommand cmdtransactiondelete = new SqlCommand("Delete from Tranction_Details where Tranction_Master_Id='" + dsemp.Rows[0]["TransactionMasterId"].ToString() + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdtransactiondelete.ExecuteNonQuery();
                con.Close();

                SqlCommand cmdtransactionmasterdelete = new SqlCommand("Delete from TranctionMaster where Tranction_Master_Id='" + dsemp.Rows[0]["TransactionMasterId"].ToString() + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdtransactionmasterdelete.ExecuteNonQuery();
                con.Close();




                SqlCommand cmdadjustdetaildelete = new SqlCommand("Delete from InventoryAdjustDetails where InventoryAdjustMasterId='" + dsemp.Rows[0]["InventoryAdjustMasterId"].ToString() + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdadjustdetaildelete.ExecuteNonQuery();
                con.Close();

                SqlCommand cmdadjustmasterdelete = new SqlCommand("Delete from InventoryAdjustMaster where InventoryAdjustMasterId='" + dsemp.Rows[0]["InventoryAdjustMasterId"].ToString() + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdadjustmasterdelete.ExecuteNonQuery();
                con.Close();



                //SqlCommand cmdavgmastertbl = new SqlCommand("Delete from InventoryWarehouseMasterAvgCostTbl where AdjustMasterId='" + dsemp.Rows[0]["InventoryAdjustMasterId"].ToString() + "'", con);
                //if (con.State.ToString() != "Open")
                //{
                //    con.Open();
                //}
                //cmdavgmastertbl.ExecuteNonQuery();
                //con.Close();
                deleteandupdatedjust(Convert.ToInt32(dsemp.Rows[0]["InventoryAdjustMasterId"].ToString()));
            }


            Label1.Visible = true;
            Label1.Text = "Record deleted successfully";

            txtDate.Text = System.DateTime.Now.ToShortDateString();
            txtInvAdjustTitle.Text = "";
            txtSearchInvName.Text = "";
            fillgirdadjust();

            gridvi.DataSource = null;
            gridvi.DataBind();


        }
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ImageButton2.Visible = true;
        if (ddltypeofadjustment.SelectedValue == "0")
        {
            normalabnormalcount();
        }
        else if (ddltypeofadjustment.SelectedValue == "1")
        {
            normalvalueadjustincrease();

        }
        else if (ddltypeofadjustment.SelectedValue == "2")
        {
            normalvalueadjustdecrease();

        }
    }
    protected void gridviewadjust_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {





    }
    protected void ImageButton7_Click1(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1455454.Hide();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (gridviewadjust.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gridviewadjust.Columns[6].Visible = false;
            }
            if (gridviewadjust.Columns[7].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                gridviewadjust.Columns[7].Visible = false;
            }
        }
        else
        {



            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                gridviewadjust.Columns[6].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                gridviewadjust.Columns[7].Visible = true;
            }
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            Label6.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            Label6.Visible = false;
        }
        btnadd.Visible = false;

        Label6.Text = "Make New Inventory Adjustment";
        Label1.Text = "";

    }
    protected void fillwarehouse()
    {

        ddlWarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlWarehouse.DataSource = ds;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "WareHouseId";
        ddlWarehouse.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
    protected void filladjustmentreason()
    {
        ddlInventoryAdjustReasonName.DataSource = (DataSet)fillddl2();
        ddlInventoryAdjustReasonName.DataTextField = "InventoryAdjustReasonName";
        ddlInventoryAdjustReasonName.DataValueField = "InventoryAdjustReasonId";
        ddlInventoryAdjustReasonName.DataBind();
        ddlInventoryAdjustReasonName.Items.Insert(0, "--Select--");
        ddlInventoryAdjustReasonName.Items[0].Value = "0";

    }
    protected void datefill()
    {
        string openingdate = "select StartDate,EndDate from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' and Active='1'";
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

            ViewState["EndDate"] = t1.AddYears(+10).ToShortDateString();

        }

    }

    protected void normalabnormalcount()
    {
        double totalnormaladjustamount = 0;

        foreach (GridViewRow gdr in grdInvMasters.Rows)
        {
            double totalqty = 0;
            double qtyonhand = 0;
            double normaladjustqty = 0;
            double abnormaladjustqty = 0;



            Label lblQtyOnHand = (Label)gdr.FindControl("lblQtyOnHand");
            TextBox txtnormaladjustqty = (TextBox)gdr.FindControl("txtnormaladjustqty");
            TextBox txtabnormaladjustqty = (TextBox)gdr.FindControl("txtabnormaladjustqty");
            Label lblINewQty = (Label)gdr.FindControl("lblINewQty");
            Label lblavgrate = (Label)gdr.FindControl("lblavgrate");




            qtyonhand = Convert.ToDouble(lblQtyOnHand.Text);

            if (txtnormaladjustqty.Text != "")
            {
                normaladjustqty = Convert.ToDouble(txtnormaladjustqty.Text);
            }
            else
            {
                normaladjustqty = 0;
                txtnormaladjustqty.Text = "0";
            }
            if (txtabnormaladjustqty.Text != "")
            {
                abnormaladjustqty = Convert.ToDouble(txtabnormaladjustqty.Text);
            }
            else
            {
                abnormaladjustqty = 0;
                txtabnormaladjustqty.Text = "0";
            }

            totalqty = qtyonhand + normaladjustqty + abnormaladjustqty;

            lblINewQty.Text = totalqty.ToString();

            totalnormaladjustamount += abnormaladjustqty * Convert.ToDouble(lblavgrate.Text);

        }
        ViewState["totalnormaladjustamount"] = totalnormaladjustamount.ToString();

    }
    protected void normalvalueadjustincrease()
    {
        int flag = 0;
        double totalincreaseinamount = 0;

        foreach (GridViewRow gdr in grdInvMasters.Rows)
        {


            double qtyonhand = 0;
            double newrate = 0;
            double newvalue = 0;

            Label lblQtyOnHand = (Label)gdr.FindControl("lblQtyOnHand");
            Label lblvalue = (Label)gdr.FindControl("lblvalue");
            TextBox txtnewrate = (TextBox)gdr.FindControl("txtnewrate");
            Label lblnewvalue = (Label)gdr.FindControl("lblnewvalue");
            Label lblavgrate = (Label)gdr.FindControl("lblavgrate");
            Label lbltotalnewincreaseinvalue = (Label)gdr.FindControl("lbltotalnewincreaseinvalue");
            Label lblnewratestar = (Label)gdr.FindControl("lblnewratestar");
            Label lblpreviousbalanceofreduction = (Label)gdr.FindControl("lblpreviousbalanceofreduction");

            
            


            if (txtnewrate.Text.Length > 0 && txtnewrate.Text != "")
            {
                qtyonhand = Convert.ToDouble(lblQtyOnHand.Text);
                newrate = Convert.ToDouble(txtnewrate.Text);
                newvalue = qtyonhand * newrate;


                lbltotalnewincreaseinvalue.Text = (newvalue - Convert.ToDouble(lblvalue.Text)).ToString();


                if (flag == 0)
                {

                    if (Convert.ToDouble(txtnewrate.Text) > Convert.ToDouble(lblavgrate.Text))
                    {
                        flag = 1;
                    }
                }


            }
            if (txtnewrate.Text != "" && lblavgrate.Text != "")
            {
                if (Convert.ToDouble(txtnewrate.Text) < Convert.ToDouble(lblavgrate.Text))
                {
                    lblnewratestar.Visible = true;
                }
                else
                {
                    lblnewratestar.Visible = false;
                }
            }

            if (lblpreviousbalanceofreduction.Text != "" && lbltotalnewincreaseinvalue.Text != "")
            {
                if (Convert.ToDouble(lblpreviousbalanceofreduction.Text) < Convert.ToDouble(lbltotalnewincreaseinvalue.Text))
                {
                    lblnewratestar.Visible = true;
                }
            }

            lblnewvalue.Text = newvalue.ToString();
            if (lbltotalnewincreaseinvalue.Text != "")
            {
                totalincreaseinamount += (Convert.ToDouble(lbltotalnewincreaseinvalue.Text));
            }


        }
        ViewState["Flag"] = flag.ToString();
        ViewState["totalincreaseinamount"] = totalincreaseinamount.ToString();
    }
    protected void normalvalueadjustdecrease()
    {
        double totaldecreaseinamount = 0;

        int flag = 0;

        int decresevalidation = 0;

        foreach (GridViewRow gdr in grdInvMasters.Rows)
        {


            double qtyonhand = 0;
            double newrate = 0;
            double newvalue = 0;

            Label lblQtyOnHand = (Label)gdr.FindControl("lblQtyOnHand");
            Label lblvalue = (Label)gdr.FindControl("lblvalue");
            TextBox txtnewrate = (TextBox)gdr.FindControl("txtnewrate");
            Label lblnewvalue = (Label)gdr.FindControl("lblnewvalue");
            Label lblavgrate = (Label)gdr.FindControl("lblavgrate");
            Label lblnewratestar = (Label)gdr.FindControl("lblnewratestar");


            if (txtnewrate.Text.Length > 0 && txtnewrate.Text != "")
            {
                qtyonhand = Convert.ToDouble(lblQtyOnHand.Text);
                newrate = Convert.ToDouble(txtnewrate.Text);
                newvalue = qtyonhand * newrate;

                if (flag == 0)
                {

                    if (Convert.ToDouble(txtnewrate.Text) > Convert.ToDouble(lblavgrate.Text))
                    {
                        flag = 1;
                    }
                }
                if (Convert.ToDouble(txtnewrate.Text) > Convert.ToDouble(lblavgrate.Text))
                {
                    lblnewratestar.Visible = true;
                }
                else
                {
                    lblnewratestar.Visible = false;
                }


            }

            lblnewvalue.Text = newvalue.ToString();

            totaldecreaseinamount += (Convert.ToDouble(lblvalue.Text) - Convert.ToDouble(lblnewvalue.Text));



        }
        ViewState["Flag"] = flag.ToString();
        ViewState["totaldecreaseinamount"] = totaldecreaseinamount.ToString();
    }

    protected void ddlfilterbybusiness_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillgirdadjust();

    }
    protected void fillfiletrwarehouse()
    {

        ddlfilterbybusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlfilterbybusiness.DataSource = ds;
        ddlfilterbybusiness.DataTextField = "Name";
        ddlfilterbybusiness.DataValueField = "WareHouseId";
        ddlfilterbybusiness.DataBind();
        ddlfilterbybusiness.Items.Insert(0, "All");
        ddlfilterbybusiness.Items[0].Value = "0";
    }

    protected void insertadjustmaster()
    {
        if (grdInvMasters.Rows.Count > 0)
        {
            try
            {
                int uid = 0;
                if (Session["UserId"] != null)
                {
                    uid = Convert.ToInt32(Session["UserId"]);
                }
                string strSelectMasterId1 = "select InventoryAdjustMasterId from InventoryAdjustMaster inner join InventoryAdjustReasonMaster on InventoryAdjustReasonMaster.InventoryAdjustReasonId=InventoryAdjustMaster.InventoryAdjustReasonId where InventoryAdjustMaster.InventoryAdjustTitle= '" + txtInvAdjustTitle.Text + "'  and InventoryAdjustReasonMaster.compid='" + Session["comid"] + "' and InventoryAdjustMaster.Whid='" + ddlWarehouse.SelectedValue + "' ";
                SqlCommand cmdSelectMasterId1 = new SqlCommand(strSelectMasterId1, con);
                SqlDataAdapter adpSelectMasterId1 = new SqlDataAdapter(cmdSelectMasterId1);
                DataTable dtSelectMasterId1 = new DataTable();
                adpSelectMasterId1.Fill(dtSelectMasterId1);

                if (dtSelectMasterId1.Rows.Count == 0)
                {
                    string strInsertMasters = "INSERT INTO InventoryAdjustMaster (Datetime,UserId,InventoryAdjustReasonId,InventoryAdjustTitle,Whid,AdjustDetail,compid,TypeOfAdjust) VALUES ('" + Convert.ToDateTime(txtDate.Text) + "','" + uid + "','" + Convert.ToInt32(ddlInventoryAdjustReasonName.SelectedValue) + "','" + txtInvAdjustTitle.Text + "','" + ddlWarehouse.SelectedValue + "','" + txtadjustmentdetail.Text + "','" + Session["comid"].ToString() + "','" + ddltypeofadjustment.SelectedValue + "' )";

                    SqlCommand cmdInsertMasters = new SqlCommand(strInsertMasters, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }

                    cmdInsertMasters.ExecuteNonQuery();
                    con.Close();

                    string strSelectMasterId = "select Max(InventoryAdjustMasterId) as InventoryAdjustMasterId from InventoryAdjustMaster";
                    SqlCommand cmdSelectMasterId = new SqlCommand(strSelectMasterId, con);
                    SqlDataAdapter adpSelectMasterId = new SqlDataAdapter(cmdSelectMasterId);
                    DataTable dtSelectMasterId = new DataTable();
                    adpSelectMasterId.Fill(dtSelectMasterId);
                    ViewState["InvAdjustedMaserId"] = Convert.ToInt32(dtSelectMasterId.Rows[0]["InventoryAdjustMasterId"].ToString());


                    foreach (GridViewRow gtrin in grdInvMasters.Rows)
                    {
                        Label lblInvWHMasterId = (Label)(gtrin.FindControl("lblInvWHMasterId"));
                        Label lblWareHouseId = (Label)(gtrin.FindControl("lblWareHouseId"));
                        TextBox txtnormaladjustqty = (TextBox)(gtrin.FindControl("txtnormaladjustqty"));
                        TextBox txtabnormaladjustqty = (TextBox)(gtrin.FindControl("txtabnormaladjustqty"));
                        Label lblINewQty = (Label)(gtrin.FindControl("lblINewQty"));
                        TextBox txtNote1 = (TextBox)(gtrin.FindControl("txtNote1"));

                        Label lblQtyOnHand = (Label)(gtrin.FindControl("lblQtyOnHand"));

                        TextBox txtnewrate = (TextBox)(gtrin.FindControl("txtnewrate"));
                        Label lblnewvalue = (Label)(gtrin.FindControl("lblnewvalue"));

                        Label lblavgrate = (Label)(gtrin.FindControl("lblavgrate"));
                        Label lblvalue = (Label)(gtrin.FindControl("lblvalue"));
                        
                        




                        double FinalQtySub = Convert.ToDouble(lblQtyOnHand.Text);
                        double FinalQty = -(FinalQtySub);



                        if (ddltypeofadjustment.SelectedValue == "0")
                        {
                            string strInsertDetails = "INSERT INTO InventoryAdjustDetails (InventoryAdjustMasterId,InventoryWHM_Id,WarehouseId,Notes,NormalAdjust,AbnormalAdjust,NewQty,QtyOnHand,AvgRate) VALUES (" + Convert.ToInt32(ViewState["InvAdjustedMaserId"]) + "," + lblInvWHMasterId.Text + "," + lblWareHouseId.Text + ",'" + txtNote1.Text + "','" + txtnormaladjustqty.Text + "','" + txtabnormaladjustqty.Text + "','" + lblINewQty.Text + "','" + lblQtyOnHand.Text + "','" + lblavgrate.Text + "')  ";
                            SqlCommand cmdInsertDetails = new SqlCommand(strInsertDetails, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdInsertDetails.ExecuteNonQuery();
                            con.Close();

                            if (txtnormaladjustqty.Text.Length > 0 && txtnormaladjustqty.Text != "0")
                            {

                                updatenormaladjustplus(lblInvWHMasterId.Text, Convert.ToInt32(ViewState["InvAdjustedMaserId"]), Convert.ToDecimal(txtnormaladjustqty.Text));

                            }
                            if (txtabnormaladjustqty.Text.Length > 0 && txtabnormaladjustqty.Text != "0")
                            {

                                updateabnormaladjustplus(lblInvWHMasterId.Text, Convert.ToInt32(ViewState["InvAdjustedMaserId"]), Convert.ToDecimal(txtabnormaladjustqty.Text));


                            }

                        }
                        else if (ddltypeofadjustment.SelectedValue == "1")
                        {
                            decimal increaseamt = 0;
                            if (lblvalue.Text != "" && lblnewvalue.Text != "")
                            {
                                increaseamt = Convert.ToDecimal(lblvalue.Text) - Convert.ToDecimal(lblnewvalue.Text);
                            }

                            string strInsertDetails = "INSERT INTO InventoryAdjustDetails (InventoryAdjustMasterId,InventoryWHM_Id,WarehouseId,Notes,NewRate,NewValue,QtyOnHand,AvgRate,OldValue,IncreaseAmount) VALUES (" + Convert.ToInt32(ViewState["InvAdjustedMaserId"]) + "," + lblInvWHMasterId.Text + "," + lblWareHouseId.Text + ",'" + txtNote1.Text + "','" + txtnewrate.Text + "','" + lblnewvalue.Text + "','" + lblQtyOnHand.Text + "','" + lblavgrate.Text + "','" + lblvalue.Text + "','" + increaseamt.ToString() + "')  ";
                            SqlCommand cmdInsertDetails = new SqlCommand(strInsertDetails, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdInsertDetails.ExecuteNonQuery();
                            con.Close();

                            updatedecreaseinrate(lblInvWHMasterId.Text, Convert.ToInt32(ViewState["InvAdjustedMaserId"]), Convert.ToDecimal(txtnewrate.Text));



                        }
                        else if (ddltypeofadjustment.SelectedValue == "2")
                        {
                             decimal decreaseamt=0;
                             if (lblvalue.Text != "" && lblnewvalue.Text != "")
                             {
                               decreaseamt = Convert.ToDecimal(lblvalue.Text) - Convert.ToDecimal(lblnewvalue.Text);
                             }

                             string strInsertDetails = "INSERT INTO InventoryAdjustDetails (InventoryAdjustMasterId,InventoryWHM_Id,WarehouseId,Notes,NewRate,NewValue,QtyOnHand,AvgRate,OldValue,DecreaseAmount) VALUES (" + Convert.ToInt32(ViewState["InvAdjustedMaserId"]) + "," + lblInvWHMasterId.Text + "," + lblWareHouseId.Text + ",'" + txtNote1.Text + "','" + txtnewrate.Text + "','" + lblnewvalue.Text + "','" + lblQtyOnHand.Text + "','" + lblavgrate.Text + "','" + lblvalue.Text + "','" + decreaseamt.ToString() + "')  ";
                             SqlCommand cmdInsertDetails = new SqlCommand(strInsertDetails, con);
                             if (con.State.ToString() != "Open")
                             {
                                con.Open();
                             }
                             cmdInsertDetails.ExecuteNonQuery();
                             con.Close();

                             updatedecreaseinrate(lblInvWHMasterId.Text, Convert.ToInt32(ViewState["InvAdjustedMaserId"]), Convert.ToDecimal(txtnewrate.Text));

                            
                        }



                    }


                    //  code for loss or gain effect on account

                    if (ddltypeofadjustment.SelectedValue == "0")
                    {
                        if (ViewState["totalnormaladjustamount"] != null)
                        {

                            SqlCommand cmdmax = new SqlCommand("Select Max(EntryNumber) as eno from TranctionMaster where EntryTypeId='3' and Whid='" + ddlWarehouse.SelectedValue + "'", con);
                            SqlDataAdapter dtpmax = new SqlDataAdapter(cmdmax);
                            DataTable dtmax = new DataTable();
                            dtpmax.Fill(dtmax);

                            double db = 0;
                            if (dtmax.Rows.Count > 0)
                            {

                                if (dtmax.Rows[0]["eno"].ToString() != "")
                                {
                                    db = Convert.ToDouble(dtmax.Rows[0]["eno"].ToString()) + 1;
                                }
                                else
                                {
                                    db = 1;
                                }
                            }
                            else
                            {
                                db = 1;
                            }
                            string uid1 = "";
                            if (Session["UserId"] != null)
                            {
                                uid1 = Session["UserId"].ToString();
                            }

                            double totalamount = 0;
                            totalamount = Convert.ToDouble(ViewState["totalnormaladjustamount"]);
                            double finalamount = 0;

                            if (totalamount >= 0)
                            {
                                finalamount = totalamount;

                            }
                            else
                            {
                                string strtemp1 = totalamount.ToString();
                                strtemp1 = strtemp1.Replace("-", "+");
                                finalamount = Convert.ToDouble(strtemp1);

                            }






                            if (totalamount != 0)
                            {

                                SqlCommand cmditran = new SqlCommand("Insert into TranctionMaster(Date,EntryNumber,EntryTypeId,UserId,Tranction_Amount,compid,Whid) values('" + Convert.ToDateTime(txtDate.Text) + "','" + db + "','3','" + uid1.ToString() + "','" + finalamount + "','" + Session["comid"] + "','" + ddlWarehouse.SelectedValue + "')", con);
                                con.Open();
                                cmditran.ExecuteNonQuery();
                                con.Close();

                                SqlCommand seltrnid = new SqlCommand("Select Max(Tranction_Master_Id) as tmid from TranctionMaster where Whid='" + ddlWarehouse.SelectedValue + "'", con);
                                SqlDataAdapter dtptrnid = new SqlDataAdapter(seltrnid);
                                DataTable dttrnid = new DataTable();
                                dtptrnid.Fill(dttrnid);
                                double tmid = 0;
                                if (dttrnid.Rows.Count > 0)
                                {
                                    if (dttrnid.Rows[0]["tmid"].ToString() != "")
                                    {

                                        tmid = Convert.ToDouble(dttrnid.Rows[0]["tmid"].ToString());
                                    }
                                }



                                if (totalamount >= 0)
                                {

                                    SqlCommand cmddetail = new SqlCommand("insert into Tranction_Details values('0','" + ddlacc1.SelectedValue + "','0','" + finalamount + "','" + tmid + "','" + txtmemo1.Text + "','" + txtDate.Text + "','0','0','" + Session["comid"] + "','" + ddlWarehouse.SelectedValue + "')", con);
                                    con.Open();
                                    cmddetail.ExecuteNonQuery();
                                    con.Close();

                                    SqlCommand cmddetail1 = new SqlCommand("insert into Tranction_Details values('" + ddlacc2.SelectedValue + "' ,'0','" + finalamount + "','0','" + tmid + "','" + txtmemo2.Text + "','" + txtDate.Text + "','0','0','" + Session["comid"] + "','" + ddlWarehouse.SelectedValue + "')", con);
                                    con.Open();
                                    cmddetail1.ExecuteNonQuery();
                                    con.Close();

                                }
                                else
                                {

                                    SqlCommand cmddetail = new SqlCommand("insert into Tranction_Details values('" + ddlacc1.SelectedValue + "','0','" + finalamount + "','0','" + tmid + "','" + txtmemo1.Text + "','" + txtDate.Text + "','0','0','" + Session["comid"] + "','" + ddlWarehouse.SelectedValue + "')", con);
                                    con.Open();
                                    cmddetail.ExecuteNonQuery();
                                    con.Close();

                                    SqlCommand cmddetail1 = new SqlCommand("insert into Tranction_Details values('0','" + ddlacc2.SelectedValue + "' ,'0','" + finalamount + "','" + tmid + "','" + txtmemo2.Text + "','" + txtDate.Text + "','0','0','" + Session["comid"] + "','" + ddlWarehouse.SelectedValue + "')", con);
                                    con.Open();
                                    cmddetail1.ExecuteNonQuery();
                                    con.Close();
                                }



                                SqlCommand cmdupdateadjust = new SqlCommand(" Update InventoryAdjustMaster set TransactionMasterId='" + tmid + "' where InventoryAdjustMasterId='" + Convert.ToInt32(ViewState["InvAdjustedMaserId"]) + "'", con);
                                con.Open();
                                cmdupdateadjust.ExecuteNonQuery();
                                con.Close();


                            }



                        }
                    }
                    else if (ddltypeofadjustment.SelectedValue == "1")
                    {
                        if (ViewState["totalincreaseinamount"] != null)
                        {

                            SqlCommand cmdmax = new SqlCommand("Select Max(EntryNumber) as eno from TranctionMaster where EntryTypeId='3' and Whid='" + ddlWarehouse.SelectedValue + "'", con);
                            SqlDataAdapter dtpmax = new SqlDataAdapter(cmdmax);
                            DataTable dtmax = new DataTable();
                            dtpmax.Fill(dtmax);

                            double db = 0;
                            if (dtmax.Rows.Count > 0)
                            {

                                if (dtmax.Rows[0]["eno"].ToString() != "")
                                {
                                    db = Convert.ToDouble(dtmax.Rows[0]["eno"].ToString()) + 1;
                                }
                                else
                                {
                                    db = 1;
                                }
                            }
                            else
                            {
                                db = 1;
                            }
                            string uid1 = "";
                            if (Session["UserId"] != null)
                            {
                                uid1 = Session["UserId"].ToString();
                            }

                            double totalamount = 0;
                            totalamount = Convert.ToDouble(ViewState["totalincreaseinamount"]);
                            double finalamount = 0;

                            if (totalamount >= 0)
                            {
                                finalamount = totalamount;

                            }
                            else
                            {
                                string strtemp1 = totalamount.ToString();
                                strtemp1 = strtemp1.Replace("-", "+");
                                finalamount = Convert.ToDouble(strtemp1);

                            }


                            if (totalamount != 0)
                            {

                                SqlCommand cmditran = new SqlCommand("Insert into TranctionMaster(Date,EntryNumber,EntryTypeId,UserId,Tranction_Amount,compid,Whid) values('" + Convert.ToDateTime(txtDate.Text) + "','" + db + "','3','" + uid1.ToString() + "','" + finalamount + "','" + Session["comid"] + "','" + ddlWarehouse.SelectedValue + "')", con);
                                con.Open();
                                cmditran.ExecuteNonQuery();
                                con.Close();

                                SqlCommand seltrnid = new SqlCommand("Select Max(Tranction_Master_Id) as tmid from TranctionMaster where Whid='" + ddlWarehouse.SelectedValue + "'", con);
                                SqlDataAdapter dtptrnid = new SqlDataAdapter(seltrnid);
                                DataTable dttrnid = new DataTable();
                                dtptrnid.Fill(dttrnid);
                                double tmid = 0;
                                if (dttrnid.Rows.Count > 0)
                                {
                                    if (dttrnid.Rows[0]["tmid"].ToString() != "")
                                    {

                                        tmid = Convert.ToDouble(dttrnid.Rows[0]["tmid"].ToString());
                                    }
                                }

                                SqlCommand cmddetail = new SqlCommand("insert into Tranction_Details values('0','5502','0','" + finalamount + "','" + tmid + "','" + txtmemo1.Text + "','" + txtDate.Text + "','0','0','" + Session["comid"] + "','" + ddlWarehouse.SelectedValue + "')", con);
                                con.Open();
                                cmddetail.ExecuteNonQuery();
                                con.Close();

                                SqlCommand cmddetail1 = new SqlCommand("insert into Tranction_Details values('11000' ,'0','" + finalamount + "','0','" + tmid + "','" + txtmemo2.Text + "','" + txtDate.Text + "','0','0','" + Session["comid"] + "','" + ddlWarehouse.SelectedValue + "')", con);
                                con.Open();
                                cmddetail1.ExecuteNonQuery();
                                con.Close();

                                SqlCommand cmdupdateadjust = new SqlCommand(" Update InventoryAdjustMaster set TransactionMasterId='" + tmid + "' where InventoryAdjustMasterId='" + Convert.ToInt32(ViewState["InvAdjustedMaserId"]) + "'", con);
                                con.Open();
                                cmdupdateadjust.ExecuteNonQuery();
                                con.Close();


                            }

                        }
                    }
                    else if (ddltypeofadjustment.SelectedValue == "2")
                    {
                        if (ViewState["totaldecreaseinamount"] != null)
                        {

                            SqlCommand cmdmax = new SqlCommand("Select Max(EntryNumber) as eno from TranctionMaster where EntryTypeId='3' and Whid='" + ddlWarehouse.SelectedValue + "'", con);
                            SqlDataAdapter dtpmax = new SqlDataAdapter(cmdmax);
                            DataTable dtmax = new DataTable();
                            dtpmax.Fill(dtmax);

                            double db = 0;
                            if (dtmax.Rows.Count > 0)
                            {

                                if (dtmax.Rows[0]["eno"].ToString() != "")
                                {
                                    db = Convert.ToDouble(dtmax.Rows[0]["eno"].ToString()) + 1;
                                }
                                else
                                {
                                    db = 1;
                                }
                            }
                            else
                            {
                                db = 1;
                            }
                            string uid1 = "";
                            if (Session["UserId"] != null)
                            {
                                uid1 = Session["UserId"].ToString();
                            }

                            double totalamount = 0;
                            totalamount = Convert.ToDouble(ViewState["totaldecreaseinamount"]);
                            double finalamount = 0;

                            if (totalamount >= 0)
                            {
                                finalamount = totalamount;

                            }
                            else
                            {
                                string strtemp1 = totalamount.ToString();
                                strtemp1 = strtemp1.Replace("-", "+");
                                finalamount = Convert.ToDouble(strtemp1);

                            }


                            if (totalamount != 0)
                            {

                                SqlCommand cmditran = new SqlCommand("Insert into TranctionMaster(Date,EntryNumber,EntryTypeId,UserId,Tranction_Amount,compid,Whid) values('" + Convert.ToDateTime(txtDate.Text) + "','" + db + "','3','" + uid1.ToString() + "','" + finalamount + "','" + Session["comid"] + "','" + ddlWarehouse.SelectedValue + "')", con);
                                con.Open();
                                cmditran.ExecuteNonQuery();
                                con.Close();

                                SqlCommand seltrnid = new SqlCommand("Select Max(Tranction_Master_Id) as tmid from TranctionMaster where Whid='" + ddlWarehouse.SelectedValue + "'", con);
                                SqlDataAdapter dtptrnid = new SqlDataAdapter(seltrnid);
                                DataTable dttrnid = new DataTable();
                                dtptrnid.Fill(dttrnid);
                                double tmid = 0;
                                if (dttrnid.Rows.Count > 0)
                                {
                                    if (dttrnid.Rows[0]["tmid"].ToString() != "")
                                    {

                                        tmid = Convert.ToDouble(dttrnid.Rows[0]["tmid"].ToString());
                                    }
                                }

                                SqlCommand cmddetail = new SqlCommand("insert into Tranction_Details values('0','11000','0','" + finalamount + "','" + tmid + "','" + txtmemo1.Text + "','" + txtDate.Text + "','0','0','" + Session["comid"] + "','" + ddlWarehouse.SelectedValue + "')", con);
                                con.Open();
                                cmddetail.ExecuteNonQuery();
                                con.Close();

                                SqlCommand cmddetail1 = new SqlCommand("insert into Tranction_Details values('5501' ,'0','" + finalamount + "','0','" + tmid + "','" + txtmemo2.Text + "','" + txtDate.Text + "','0','0','" + Session["comid"] + "','" + ddlWarehouse.SelectedValue + "')", con);
                                con.Open();
                                cmddetail1.ExecuteNonQuery();
                                con.Close();

                                SqlCommand cmdupdateadjust = new SqlCommand(" Update InventoryAdjustMaster set TransactionMasterId='" + tmid + "' where InventoryAdjustMasterId='" + Convert.ToInt32(ViewState["InvAdjustedMaserId"]) + "'", con);
                                con.Open();
                                cmdupdateadjust.ExecuteNonQuery();
                                con.Close();


                            }

                        }

                    }

                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Record already exist";
                }
            }


            catch (Exception ex)
            {
                Label1.Visible = true;
                Label1.Text = "Error:" + ex.Message.ToString();

            }


        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Please insert atleast one record";
        }

        fillgirdadjust();



        ImageButton3.Visible = false;
        Label1.Visible = true;
        Label1.Text = "Record inserted successfully";

        pnladd.Visible = false;
        pnladd.Enabled = true;
        btnadd.Visible = true;
        pnlforview.Visible = false;
        Button3.Visible = false;
        ImageButton2.Visible = false;
        Panel6.Visible = false;

        txtadjustmentdetail.Text = "";
        txtInvAdjustTitle.Text = "";
        txtmemo1.Text = "";
        txtmemo2.Text = "";
    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        insertadjustmaster();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        insertadjustmaster();
    }
    protected void account1()
    {
        string str = "SELECT  distinct LEFT( GroupCompanyMaster.groupdisplayname,20)+ ':' +  LEFT( AccountMaster.AccountId,10) + ':' +  LEFT( AccountMaster.AccountName,30)  AS Account,  AccountMaster.AccountId   FROM       AccountMaster INNER JOIN  GroupCompanyMaster ON AccountMaster.GroupId = GroupCompanyMaster.GroupId  WHERE   (AccountMaster.AccountId IS NOT NULL) and AccountMaster.Status=1 and AccountMaster.compid ='" + Session["Comid"].ToString() + "' and accountmaster.Whid='" + ddlWarehouse.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlWarehouse.SelectedValue + "'  ORDER BY Account  ";
        SqlDataAdapter adpt = new SqlDataAdapter(str, con);
        DataSet ds1 = new DataSet();
        adpt.Fill(ds1);

        ddlacc1.DataSource = ds1;
        ddlacc1.DataTextField = "Account";
        ddlacc1.DataValueField = "AccountId";
        ddlacc1.DataBind();
    }
    protected void account2()
    {
        string str = "SELECT  distinct LEFT( GroupCompanyMaster.groupdisplayname,20)+ ':' +  LEFT( AccountMaster.AccountId,10) + ':' +  LEFT( AccountMaster.AccountName,30)  AS Account,  AccountMaster.AccountId   FROM       AccountMaster INNER JOIN  GroupCompanyMaster ON AccountMaster.GroupId = GroupCompanyMaster.GroupId  WHERE   (AccountMaster.AccountId IS NOT NULL) and AccountMaster.Status=1 and AccountMaster.compid ='" + Session["Comid"].ToString() + "' and accountmaster.Whid='" + ddlWarehouse.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlWarehouse.SelectedValue + "'  ORDER BY Account  ";
        SqlDataAdapter adpt = new SqlDataAdapter(str, con);
        DataSet ds1 = new DataSet();
        adpt.Fill(ds1);

        ddlacc2.DataSource = ds1;
        ddlacc2.DataTextField = "Account";
        ddlacc2.DataValueField = "AccountId";
        ddlacc2.DataBind();
    }
    protected void Button11_Click(object sender, EventArgs e)
    {

        string strSelectMasterId1 = "select InventoryAdjustReasonId from InventoryAdjustReasonMaster  where InventoryAdjustReasonName= '" + txtreason.Text + "'  and compid='" + Session["comid"] + "' and InventoryAdjustReasonId<>'" + ViewState["adjustreasonid"] + "'  ";
        SqlCommand cmdSelectMasterId1 = new SqlCommand(strSelectMasterId1, con);
        SqlDataAdapter adpSelectMasterId1 = new SqlDataAdapter(cmdSelectMasterId1);
        DataTable dtSelectMasterId1 = new DataTable();
        adpSelectMasterId1.Fill(dtSelectMasterId1);
        if (dtSelectMasterId1.Rows.Count == 0)
        {
            string strup = " update InventoryAdjustReasonMaster set InventoryAdjustReasonName='" + txtreason.Text + "' " +
                " where InventoryAdjustReasonId='" + ViewState["adjustreasonid"] + "' ";
            SqlCommand cmdup = new SqlCommand(strup, con);
            con.Open();
            cmdup.ExecuteNonQuery();
            con.Close();



            GridView1.EditIndex = -1;

            Label2.Visible = true;
            Label2.Text = "Record updated successfully";


        }
        else
        {
            Label2.Visible = true;
            Label2.Text = "Record already exist";
        }

        ImgBtnInsertReason.Visible = true;
        Button11.Visible = false;
        ModalPopupExtender1333.Show();


    }
    protected void imgbtncancel_Click(object sender, EventArgs e)
    {
        txtreason.Text = "";
        ImgBtnInsertReason.Visible = true;
        Button11.Visible = false;
        ModalPopupExtender1333.Show();
        filladjustreasongrid();

    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Show();
    }
    protected void updatenormaladjustplus(string invid, int adjustid, decimal qtyrecive)
    {

        string updateavgcos = "";
        decimal OLDavgcost = 0;
        decimal oLDqtyONHAND = 0;
        decimal Totalavgcost = 0;
        decimal Newqtyonhand = 0;

        decimal invrate = 0;


        DataTable drtinvdata = select("SELECT top(1) * FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + invid + "' and DateUpdated<='" + txtDate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

        if (drtinvdata.Rows.Count > 0)
        {
            if (Convert.ToString(drtinvdata.Rows[0]["AvgCost"]) != "")
            {
                OLDavgcost = Convert.ToDecimal(drtinvdata.Rows[0]["AvgCost"]);
            }
            if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
            {
                oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
            }
        }

        decimal Finalqtyhand = 0;

        invrate = Math.Round(OLDavgcost, 2);
        Finalqtyhand = Convert.ToDecimal(qtyrecive) + oLDqtyONHAND;

        if (Finalqtyhand > 0)
        {
            // Totalavgcost = ((invrate * Convert.ToDecimal(qtyrecive)) + (OLDavgcost * oLDqtyONHAND)) / Finalqtyhand;
            Totalavgcost = ((OLDavgcost * oLDqtyONHAND)) / Finalqtyhand;
        }
        Totalavgcost = Math.Round(Totalavgcost, 2);

        Newqtyonhand = Convert.ToDecimal(qtyrecive) + oLDqtyONHAND;
        Genetrans();
        updateavgcos = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,AdjustMasterId,Qty,Rate,DateUpdated,AvgCost,QtyonHand,Tranction_Master_Id)values('" + invid + "','" + adjustid + "','" + qtyrecive + "','" + Totalavgcost + "','" + txtDate.Text + "','" + Totalavgcost + "','" + Newqtyonhand + "','" + ViewState["tid"].ToString() + "')";
        SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmavgcost.ExecuteNonQuery();
        con.Close();

        DataTable Dataupval = select("SELECT  * FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + invid + "' and DateUpdated>'" + txtDate.Text + "' order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
        decimal changeTotalavgcost = Totalavgcost;
        decimal changeTotalonhand = Newqtyonhand;

        foreach (DataRow itm in Dataupval.Rows)
        {
            string gupd = "";
            string gupd1 = "";
            string manul = "";
            string adjustupdate = "";

            if (Convert.ToString(itm["Rate"]) == "" && Convert.ToDecimal(itm["Qty"]) < 0 && Convert.ToString(itm["AdjustMasterId"]) == "")
            {

                decimal newamt = 0;

                DataTable drsv = select("Select AmountDebit from Tranction_Details where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "' ");
                if (drsv.Rows.Count > 0)
                {
                    newamt = Convert.ToDecimal(drsv.Rows[0]["AmountDebit"]);
                }

                changeTotalavgcost = Math.Round(changeTotalavgcost, 2);

                changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);

                Finalqtyhand = changeTotalonhand;

                decimal appn = (changeTotalavgcost) * ((-1) * Convert.ToDecimal(itm["Qty"]));

                decimal appold = Convert.ToDecimal(itm["AvgCost"]) * ((-1) * Convert.ToDecimal(itm["Qty"]));

                newamt = newamt + appn - (appold);

                newamt = Math.Round(newamt, 2);



                gupd = "Update Tranction_Details Set AmountDebit='" + newamt + "' where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                gupd1 = "Update Tranction_Details Set AmountCredit='" + newamt + "' where AccountCredit='8000' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";

                SqlCommand cmdupcugs = new SqlCommand(gupd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupcugs.ExecuteNonQuery();

                con.Close();
                SqlCommand cmdupcugsin = new SqlCommand(gupd1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupcugsin.ExecuteNonQuery();
                con.Close();


                manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";



            }
            else if (Convert.ToString(itm["Tranction_Master_Id"]) != "")
            {
                Finalqtyhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                if (Finalqtyhand > 0)
                {
                    changeTotalavgcost = ((changeTotalavgcost * changeTotalonhand) + (Convert.ToDecimal(itm["Qty"]) * Convert.ToDecimal(itm["Rate"]))) / Finalqtyhand;
                }
                changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";



            }
           
            if (manul.Length > 0)
            {
                SqlCommand cmdupinv = new SqlCommand(manul, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupinv.ExecuteNonQuery();
                con.Close();
            }
            //transaction.Commit();
            //con.Close();
        }


    }
    protected void updateabnormaladjustplus(string invid, int adjustid, decimal qtyrecive)
    {

        string updateavgcos = "";
        decimal OLDavgcost = 0;
        decimal oLDqtyONHAND = 0;
        decimal Totalavgcost = 0;
        decimal Newqtyonhand = 0;

        decimal invrate = 0;


        DataTable drtinvdata = select("SELECT top(1) * FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + invid + "' and DateUpdated<='" + txtDate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

        if (drtinvdata.Rows.Count > 0)
        {
            if (Convert.ToString(drtinvdata.Rows[0]["AvgCost"]) != "")
            {
                OLDavgcost = Convert.ToDecimal(drtinvdata.Rows[0]["AvgCost"]);
            }
            if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
            {
                oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
            }
        }

        decimal Finalqtyhand = 0;

        invrate = Math.Round(OLDavgcost, 2);
        Finalqtyhand = Convert.ToDecimal(qtyrecive) + oLDqtyONHAND;

        if (Finalqtyhand > 0)
        {

            Totalavgcost = Math.Round(OLDavgcost, 2);
        }
        Totalavgcost = Math.Round(Totalavgcost, 2);

        Newqtyonhand = Convert.ToDecimal(qtyrecive) + oLDqtyONHAND;

        Genetrans();

        updateavgcos = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,AdjustMasterId,Qty,Rate,DateUpdated,AvgCost,QtyonHand,Tranction_Master_Id)values('" + invid + "','" + adjustid + "','" + qtyrecive + "','" + Totalavgcost + "','" + txtDate.Text + "','" + Totalavgcost + "','" + Newqtyonhand + "','" + ViewState["tid"].ToString() + "')";
        SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmavgcost.ExecuteNonQuery();
        con.Close();

        DataTable Dataupval = select("SELECT  * FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + invid + "' and DateUpdated>'" + txtDate.Text + "' order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
        decimal changeTotalavgcost = Totalavgcost;
        decimal changeTotalonhand = Newqtyonhand;

        foreach (DataRow itm in Dataupval.Rows)
        {
            string gupd = "";
            string gupd1 = "";
            string manul = "";
            string adjustupdate = "";

            if (Convert.ToString(itm["Rate"]) == "" && Convert.ToDecimal(itm["Qty"]) < 0 )
            {

                decimal newamt = 0;

                DataTable drsv = select("Select AmountDebit from Tranction_Details where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "' ");
                if (drsv.Rows.Count > 0)
                {
                    newamt = Convert.ToDecimal(drsv.Rows[0]["AmountDebit"]);
                }

                changeTotalavgcost = Math.Round(changeTotalavgcost, 2);

                changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);

                Finalqtyhand = changeTotalonhand;

                decimal appn = (changeTotalavgcost) * ((-1) * Convert.ToDecimal(itm["Qty"]));

                decimal appold = Convert.ToDecimal(itm["AvgCost"]) * ((-1) * Convert.ToDecimal(itm["Qty"]));

                newamt = newamt + appn - (appold);

                newamt = Math.Round(newamt, 2);



                gupd = "Update Tranction_Details Set AmountDebit='" + newamt + "' where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                gupd1 = "Update Tranction_Details Set AmountCredit='" + newamt + "' where AccountCredit='8000' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";

                SqlCommand cmdupcugs = new SqlCommand(gupd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupcugs.ExecuteNonQuery();

                con.Close();
                SqlCommand cmdupcugsin = new SqlCommand(gupd1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupcugsin.ExecuteNonQuery();
                con.Close();


                manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";



            }
            else if (Convert.ToString(itm["Tranction_Master_Id"]) != "")
            {
                Finalqtyhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                if (Finalqtyhand > 0)
                {
                    changeTotalavgcost = ((changeTotalavgcost * changeTotalonhand) + (Convert.ToDecimal(itm["Qty"]) * Convert.ToDecimal(itm["Rate"]))) / Finalqtyhand;
                }
                changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";



            }
           
            if (manul.Length > 0)
            {
                SqlCommand cmdupinv = new SqlCommand(manul, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupinv.ExecuteNonQuery();
                con.Close();
            }
            //transaction.Commit();
            //con.Close();
        }


    }

    protected void deleteandupdatedjust(int adjustid)
    {
        DataTable dtinvitem = select("SELECT  * FROM  InventoryWarehouseMasterAvgCostTbl  where AdjustMasterId='" + adjustid + "' ");

        if (dtinvitem.Rows.Count > 0)
        {
            foreach (DataRow dritem in dtinvitem.Rows)
            {
                string invid = dritem["InvWMasterId"].ToString();

                string updateavgcos = "";
                decimal OLDavgcost = 0;
                decimal oLDqtyONHAND = 0;
                decimal Totalavgcost = 0;
                decimal Newqtyonhand = 0;

                decimal invrate = 0;


                SqlCommand cmavgcost = new SqlCommand("Delete from InventoryWarehouseMasterAvgCostTbl where AdjustMasterId='" + adjustid + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmavgcost.ExecuteNonQuery();
                con.Close();


                DataTable drtinvdata = select("SELECT top(1) * FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + invid + "' and DateUpdated<='" + txtDate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

                if (drtinvdata.Rows.Count > 0)
                {
                    if (Convert.ToString(drtinvdata.Rows[0]["AvgCost"]) != "")
                    {
                        OLDavgcost = Convert.ToDecimal(drtinvdata.Rows[0]["AvgCost"]);
                    }
                    if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
                    {
                        oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
                    }
                }

                decimal Finalqtyhand = 0;
                invrate = Math.Round(OLDavgcost, 2);
                Finalqtyhand = oLDqtyONHAND;
                Totalavgcost = Math.Round(OLDavgcost, 2);
                Newqtyonhand = oLDqtyONHAND;


                DataTable Dataupval = select("SELECT  * FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + invid + "' and DateUpdated>'" + txtDate.Text + "' order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
                decimal changeTotalavgcost = Totalavgcost;
                decimal changeTotalonhand = Newqtyonhand;

                foreach (DataRow itm in Dataupval.Rows)
                {
                    string gupd = "";
                    string gupd1 = "";
                    string manul = "";
                    string adjustupdate = "";

                    if (Convert.ToString(itm["Rate"]) == "" && Convert.ToDecimal(itm["Qty"]) < 0)
                    {

                        decimal newamt = 0;

                        DataTable drsv = select("Select AmountDebit from Tranction_Details where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "' ");
                        if (drsv.Rows.Count > 0)
                        {
                            newamt = Convert.ToDecimal(drsv.Rows[0]["AmountDebit"]);
                        }

                        changeTotalavgcost = Math.Round(changeTotalavgcost, 2);

                        changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);

                        Finalqtyhand = changeTotalonhand;

                        decimal appn = (changeTotalavgcost) * ((-1) * Convert.ToDecimal(itm["Qty"]));

                        decimal appold = Convert.ToDecimal(itm["AvgCost"]) * ((-1) * Convert.ToDecimal(itm["Qty"]));

                        newamt = newamt + appn - (appold);

                        newamt = Math.Round(newamt, 2);



                        gupd = "Update Tranction_Details Set AmountDebit='" + newamt + "' where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                        gupd1 = "Update Tranction_Details Set AmountCredit='" + newamt + "' where AccountCredit='8000' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";

                        SqlCommand cmdupcugs = new SqlCommand(gupd, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupcugs.ExecuteNonQuery();

                        con.Close();
                        SqlCommand cmdupcugsin = new SqlCommand(gupd1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupcugsin.ExecuteNonQuery();
                        con.Close();


                        manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";



                    }
                    else if (Convert.ToString(itm["Tranction_Master_Id"]) != "")
                    {
                        Finalqtyhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                        if (Finalqtyhand > 0)
                        {
                            changeTotalavgcost = ((changeTotalavgcost * changeTotalonhand) + (Convert.ToDecimal(itm["Qty"]) * Convert.ToDecimal(itm["Rate"]))) / Finalqtyhand;
                        }
                        changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                        changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                        manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";



                    }
                    if (manul.Length > 0)
                    {
                        SqlCommand cmdupinv = new SqlCommand(manul, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupinv.ExecuteNonQuery();
                        con.Close();
                    }
                    //transaction.Commit();
                    //con.Close();
                }
            }
        }

    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            grdInvMasters.Columns[12].Visible = true;
        }
        else
        {
            grdInvMasters.Columns[12].Visible = false;
        }
    }
    protected void ddltypeofadjustment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltypeofadjustment.SelectedValue == "1")
        {
            Panel11.Visible = true;
        }
        else
        {
            CheckBox1.Checked = false;
            Panel11.Visible = false;
        }
    }
    protected void updatedecreaseinrate(string invid, int adjustid, decimal newavgcost)
    {

        string updateavgcos = "";
        decimal OLDavgcost = 0;
        decimal oLDqtyONHAND = 0;
        decimal Totalavgcost = 0;
        decimal Newqtyonhand = 0;

        decimal invrate = 0;


        DataTable drtinvdata = select("SELECT top(1) * FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + invid + "' and DateUpdated<='" + txtDate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

        if (drtinvdata.Rows.Count > 0)
        {
            if (Convert.ToString(drtinvdata.Rows[0]["AvgCost"]) != "")
            {
                OLDavgcost = Convert.ToDecimal(drtinvdata.Rows[0]["AvgCost"]);
            }
            if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
            {
                oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
            }
        }

        decimal Finalqtyhand = 0;

        invrate = Math.Round(newavgcost, 2);
        Finalqtyhand =  oLDqtyONHAND;


        Totalavgcost = Math.Round(newavgcost, 2);

        Newqtyonhand =  oLDqtyONHAND;

        Genetrans();

        updateavgcos = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,AdjustMasterId,Qty,Rate,DateUpdated,AvgCost,QtyonHand,Tranction_Master_Id)values('" + invid + "','" + adjustid + "','0','" + Totalavgcost + "','" + txtDate.Text + "','" + Totalavgcost + "','" + Newqtyonhand + "','" + ViewState["tid"].ToString() + "')";
        SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmavgcost.ExecuteNonQuery();
        con.Close();

        DataTable Dataupval = select("SELECT  * FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + invid + "' and DateUpdated>'" + txtDate.Text + "' order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
        decimal changeTotalavgcost = Totalavgcost;
        decimal changeTotalonhand = Newqtyonhand;

        foreach (DataRow itm in Dataupval.Rows)
        {
            string gupd = "";
            string gupd1 = "";
            string manul = "";
            string adjustupdate = "";

            if (Convert.ToString(itm["Rate"]) == "" && Convert.ToDecimal(itm["Qty"]) < 0 )
            {

                decimal newamt = 0;

                DataTable drsv = select("Select AmountDebit from Tranction_Details where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "' ");
                if (drsv.Rows.Count > 0)
                {
                    newamt = Convert.ToDecimal(drsv.Rows[0]["AmountDebit"]);
                }

                changeTotalavgcost = Math.Round(changeTotalavgcost, 2);

                changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);

                Finalqtyhand = changeTotalonhand;

                decimal appn = (changeTotalavgcost) * ((-1) * Convert.ToDecimal(itm["Qty"]));

                decimal appold = Convert.ToDecimal(itm["AvgCost"]) * ((-1) * Convert.ToDecimal(itm["Qty"]));

                newamt = newamt + appn - (appold);

                newamt = Math.Round(newamt, 2);



                gupd = "Update Tranction_Details Set AmountDebit='" + newamt + "' where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                gupd1 = "Update Tranction_Details Set AmountCredit='" + newamt + "' where AccountCredit='8000' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";

                SqlCommand cmdupcugs = new SqlCommand(gupd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupcugs.ExecuteNonQuery();

                con.Close();
                SqlCommand cmdupcugsin = new SqlCommand(gupd1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupcugsin.ExecuteNonQuery();
                con.Close();


                manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";



            }
            else if (Convert.ToString(itm["Tranction_Master_Id"]) != "")
            {
                Finalqtyhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                if (Finalqtyhand > 0)
                {
                    changeTotalavgcost = ((changeTotalavgcost * changeTotalonhand) + (Convert.ToDecimal(itm["Qty"]) * Convert.ToDecimal(itm["Rate"]))) / Finalqtyhand;
                }
                changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";



            }
            
            if (manul.Length > 0)
            {
                SqlCommand cmdupinv = new SqlCommand(manul, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupinv.ExecuteNonQuery();
                con.Close();
            }
            //transaction.Commit();
            //con.Close();
        }


    }

    protected void fillgrid()
    {
       
        Panel6.Visible = true;
        DataTable dtinvids = new DataTable();
        if (RadioButtonList1.SelectedValue == "0")
        {
            dtinvids = (DataTable)(SeachByCat());
        }

        else if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtSearchInvName.Text.Length > 0)
            {
                dtinvids = (DataTable)(SearchByName());
            }

        }

        if (dtinvids.Rows.Count > 0)
        {
            grdInvMasters.DataSource = dtinvids;
            DataView myDataView = new DataView();
            myDataView = dtinvids.DefaultView;
            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            grdInvMasters.DataBind();

        }

        else
        {
            grdInvMasters.DataSource = null;
            grdInvMasters.DataBind();
        }

        Panel6.Visible = true;
        Button3.Visible = true;
        ImageButton2.Visible = false;
        ImageButton3.Visible = true;
        gridvi.Visible = false;

        if (ddltypeofadjustment.SelectedValue == "0")
        {
            Button12.Visible = true;
        }
        else
        {
            Button12.Visible = false;
        }
    }
    protected void Genetrans()
    {
        SqlCommand cd3 = new SqlCommand("Sp_Insert_TranctionMasterRetIdentity", con);

        cd3.CommandType = CommandType.StoredProcedure;
        cd3.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text).ToShortDateString());
        cd3.Parameters.AddWithValue("@EntryNumber", Convert.ToInt32(0));
        cd3.Parameters.AddWithValue("@EntryTypeId", "0");
        cd3.Parameters.AddWithValue("@UserId", Session["userid"].ToString());
        cd3.Parameters.AddWithValue("@Tranction_Amount", Convert.ToDecimal(0));
        cd3.Parameters.AddWithValue("@whid", 0);

        cd3.Parameters.AddWithValue("@compid", Session["comid"]);


        cd3.Parameters.Add(new SqlParameter("@Tranction_Master_Id", SqlDbType.Int));
        cd3.Parameters["@Tranction_Master_Id"].Direction = ParameterDirection.Output;
        cd3.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cd3.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int Id1 = (int)cd3.ExecuteNonQuery();
        Id1 = Convert.ToInt32(cd3.Parameters["@Tranction_Master_Id"].Value);
        con.Close();
        ViewState["tid"] = Id1;
        SqlCommand csd = new SqlCommand("Delete from TranctionMaster where Tranction_Master_Id='" + ViewState["tid"] + "'", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        csd.ExecuteNonQuery();
        con.Close();
    }
}
