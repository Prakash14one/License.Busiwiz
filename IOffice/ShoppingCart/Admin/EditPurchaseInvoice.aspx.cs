
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


public partial class ShoppingCart_Admin_EditPurchaseInvoice : System.Web.UI.Page
{
    public Int32 InventoryWarehouseMasterId;
    public Int32 InventoryWarehouseMasterIdB;
    public Int32 InventoryWarehouseMasterIdP;
    public Int32 InventoryWarehouseMasterIdN;
    Int32 warehousefinalid;
    // Int32 cashid;
    string a6;
    object com;
    string page;
    SqlConnection con = new SqlConnection(PageConn.connnn);
    SqlConnection conn = new SqlConnection(PageConn.connnn);
    public static string invtype = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        //conn = pgcon.dynconn; 


        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        //  lblcash.Visible = false;
        // ddlcashtype.Visible = false;
        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        page = strSplitArr[i - 1].ToString();
        //try
        //{ 
        //   ImageButton1.Attributes.Add("onclick", "window.open('Default6.aspx', ''," + "'width=590,height=630,top=240,left=220,right=40,bottom=20,toolbar=no fullscreen=0,titlebar=no,resizable=0')");
        Label8.Text = "";
        lblmmssgg.Text = "";
        Label2.Text = "";






        if (!IsPostBack)
        {
            if (RadioButtonList2.SelectedIndex < 0)
            {
                RadioButtonList2.SelectedIndex = 3;
            }
            DataTable ds = ClsStore.SelectStorename();
            ddlWarehouse.DataSource = ds;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "WareHouseId";
            ddlWarehouse.DataBind();

            if (Request.QueryString["Purchase_Details_Id"] != null)
            {
                ViewState["detailid"] = Convert.ToInt32(Request.QueryString["Purchase_Details_Id"]);

                FillDetailIdsGrid();




                pnlInv.Visible = false;
                pnlPO.Visible = true;

                pnlName.Visible = false;
                getProductNo();
                PnlMainBarcode.Visible = false;



                CheckBox1.Checked = true;
                CheckBox1.Enabled = false;

                ddlItemname.Items.Insert(0, "-Select-");
                ddlItemname.SelectedItem.Value = "0";
                ddlItemname.DataBind();

                // hdnCat.Value = "0" ;
                if (ddlCategory.SelectedIndex < 0)
                {
                    hdnCat.Value = "0";
                    InventoryWarehouseMasterId = 0;

                }
                else
                {
                    hdnCat.Value = ddlCategory.SelectedItem.Value.ToString();

                    InventoryWarehouseMasterId = Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString());
                }
                hdnPNo.Value = "0";
                if (ddlProductNo.SelectedIndex <= 0)
                {
                    hdnPNo.Value = "0";
                    InventoryWarehouseMasterIdP = 0;

                }
                else
                {
                    hdnPNo.Value = ddlProductNo.SelectedItem.Value.ToString();

                    InventoryWarehouseMasterIdP = Convert.ToInt32(ddlProductNo.SelectedItem.Value.ToString());
                }
                hdnNm.Value = "0";
                if (ddlItemname.SelectedIndex <= 0)
                {
                    hdnNm.Value = "0";
                    InventoryWarehouseMasterIdN = Convert.ToInt32(hdnNm.Value);

                }
                else
                {
                    hdnNm.Value = ddlItemname.SelectedItem.Value.ToString();
                    if (ddlItemname.SelectedItem.Value.Length > 0)
                    {
                    }
                    else
                    {
                        InventoryWarehouseMasterIdN = Convert.ToInt32(hdnNm.Value);
                    }
                }

                hdnBarcode.Value = "0";

            }




        }

    }


    protected void FillDetailIdsGrid()
    {
        EventArgs e = new EventArgs();
        object sender = new object();
        string str = "SELECT     Purchase_Details_Id,PartyName,AcseprateTax,WareHouseMasterId,TaxoptionType,Pertax1Id,Pertax2Id,Pertax3Id, PurchaseDetails.PartyName,Date, EntryNumer, PurchaseInvoiceNumber, NetInvoiceNumber, TexAmount1,Transporterid, texAmount2,texAmount3,ShippingTrackingNumber" +
                  " ,shippingandHandlingCharges, TotalAmt,PurchaseDetails.Invpertax " +
            "FROM         PurchaseDetails " +

                " where Purchase_Details_Id='" + Convert.ToInt32(ViewState["detailid"]) + "'  ";
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            ddlWarehouse.SelectedValue = ds.Rows[0]["WareHouseMasterId"].ToString();
            ddlWarehouse_SelectedIndexChanged(sender, e);
            txtDate.Text = Convert.ToDateTime(ds.Rows[0]["Date"]).ToShortDateString();
            ViewState["sdo"] = Convert.ToDateTime(ds.Rows[0]["Date"]).ToShortDateString();
            ViewState["sdo1"] = Convert.ToDateTime(ds.Rows[0]["Date"]).ToShortDateString();
            if (Convert.ToString(ds.Rows[0]["PartyName"]) != "0")
            {
                ddlpartyName.SelectedValue = ds.Rows[0]["PartyName"].ToString();
            }

            lblEntryNo.Text = ds.Rows[0]["EntryNumer"].ToString();
            txtDate.Text = Convert.ToDateTime(ds.Rows[0]["Date"]).ToShortDateString();
            txtNetAmount.Text = ds.Rows[0]["TotalAmt"].ToString();
            txtShippCharge.Text = ds.Rows[0]["shippingandHandlingCharges"].ToString();
            txtTrackingNo.Text = Convert.ToString(ds.Rows[0]["ShippingTrackingNumber"]);
            txtInvNo.Text = Convert.ToString(ds.Rows[0]["PurchaseInvoiceNumber"]);
            txtTax1.Text = ds.Rows[0]["TexAmount1"].ToString();
            txtTax2.Text = ds.Rows[0]["texAmount2"].ToString();
            txtTax3.Text = ds.Rows[0]["texAmount3"].ToString();
            ddlTransporter.SelectedValue = Convert.ToString(ds.Rows[0]["Transporterid"]);
            ddlpartyName.SelectedValue = Convert.ToString(ds.Rows[0]["PartyName"]);
            rdl1.SelectedIndex = Convert.ToInt32(ds.Rows[0]["AcseprateTax"]);
            CheckBox2.SelectedIndex = Convert.ToInt32(ds.Rows[0]["Invpertax"]);
            lbltxtop.Text = Convert.ToString(ds.Rows[0]["TaxoptionType"]);
            lbltaxper1.Text = Convert.ToString(ds.Rows[0]["Pertax1Id"]);
            lbltaxper2.Text = Convert.ToString(ds.Rows[0]["Pertax2Id"]);
            lbltaxper3.Text = Convert.ToString(ds.Rows[0]["Pertax3Id"]);


            DataTable dsrc = select(" SELECT  distinct Purchase_Details_Id,  TranctionMaster.Tranction_Master_Id, TranctionMaster.EntryNumber " +
                     " FROM PurchaseDetails INNER JOIN " +
                     " WareHouseMaster ON PurchaseDetails.WareHouseMasterId = WareHouseMaster.WareHouseId INNER JOIN " +
                     " TranctionMaster ON PurchaseDetails.TransId = TranctionMaster.Tranction_Master_Id " +
                " where PurchaseDetails.Purchase_Details_Id = '" + Convert.ToString(ds.Rows[0]["Purchase_Details_Id"]) + "' and TranctionMaster.Whid='" + ddlWarehouse.SelectedValue + "' and PurchaseDetails.WareHouseMasterId='" + ddlWarehouse.SelectedValue + "'  and EntryTypeId='27'  ");
            if (dsrc.Rows.Count > 0)
            {
                ViewState["Pid"] = Convert.ToString(dsrc.Rows[0]["Purchase_Details_Id"]);
                ViewState["Tid"] = Convert.ToString(dsrc.Rows[0]["Tranction_Master_Id"]);
                lblEntryNo.Text = Convert.ToString(dsrc.Rows[0]["EntryNumber"]);
            }
            DataTable dttrand = select("select * from TranctionMasterSuppliment where Tranction_Master_Id='" + ViewState["Tid"] + "'");
            if (dttrand.Rows.Count > 0)
            {
                if (Convert.ToString(dttrand.Rows[0]["GrnMaster_Id"]) != "")
                {
                    txtpayduedate.Text = Convert.ToDateTime(dttrand.Rows[0]["GrnMaster_Id"]).ToShortDateString();
                }


                if (Convert.ToString(dttrand.Rows[0]["AmountDue"]) != "")
                {
                    if (Convert.ToInt32(dttrand.Rows[0]["AmountDue"]) == 0)
                    {
                        RadioButtonList1.SelectedIndex = 1;
                    }
                    else
                    {
                        RadioButtonList1.SelectedIndex = 0;
                    }
                }
                else
                {
                    RadioButtonList1.SelectedIndex = 1;
                }
                RadioButtonList3_SelectedIndexChanged(sender, e);
                if (RadioButtonList1.SelectedIndex == 1)
                {
                    DataTable dtacfrompjid = select(" SELECT AccountCredit FROM  Tranction_Details where Tranction_Master_Id='" + ViewState["Tid"] + "' order by Tranction_Details_Id ASC");

                    if (dtacfrompjid.Rows.Count > 0)
                    {
                        if (dtacfrompjid.Rows[0]["AccountCredit"].ToString() != "")
                        {
                            ddlcashtype.SelectedIndex = ddlcashtype.Items.IndexOf(ddlcashtype.Items.FindByValue(Convert.ToString(dtacfrompjid.Rows[0]["AccountCredit"])));
                        }
                    }
                }

            }

        }



        string str2 = "    SELECT   Tax1,Tax2,Tax3,   InventoryCategoryMaster.InventoryCatName + ':' + InventorySubCategoryMaster.InventorySubCatName + ':' + InventoruSubSubCategory.InventorySubSubName AS category, " +
        "     InventoryMaster.Name, InventoryBarcodeMaster.Barcode, PurchaseMaster.BatchNo, PurchaseMaster.ExpiryDate, PurchaseMaster.InvoiceQty as InvQty,cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+ ' ' + UnitTypeMaster.Name AS UnitValues,  " +
        "    PurchaseMaster.RatePerUnit as Rate, PurchaseMaster.ReceivedQTy as RecdQty, PurchaseDetails.Purchase_Details_Id, PurchaseDetails.PurchaseInvoiceNumber, " +
        "    PurchaseMaster.Purchase_Master_Id, InventoryMaster.ProductNo, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, ReceivedQTy as Shortage, RatePerUnit as ShortageAmt," +
        "    InventoryWarehouseMasterTbl.WareHouseId,PurchaseMaster.Tax1,PurchaseMaster.Tax2, ReceivedQTy as Totals" +
        "   FROM         PurchaseDetails RIGHT OUTER JOIN " +
        "     PurchaseMaster LEFT OUTER JOIN " +
        "    InventoryCategoryMaster RIGHT OUTER JOIN " +
        "    InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
         "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
         "   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId LEFT OUTER JOIN " +
         "   InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id RIGHT OUTER JOIN " +
         "   InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId ON  " +
         "    PurchaseMaster.InventoryWHM_Id = InventoryWarehouseMasterTbl.InventoryWarehouseMasterId ON  " +
        "     PurchaseDetails.Purchase_Details_Id = PurchaseMaster.Purchase_Details_Id inner Join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId " +
          "  WHERE     (PurchaseDetails.Purchase_Details_Id = '" + Convert.ToInt32(ViewState["detailid"]) + "') " +
      " ORDER BY PurchaseMaster.Purchase_Master_Id ";


        SqlDataAdapter adp2 = new SqlDataAdapter(str2, con);
        DataTable ds2 = new DataTable();
        adp2.Fill(ds2);

        if (GridView1.Rows.Count > 0)
        {
            //chk.Enabled = false;
            CheckBox2.Enabled = false;
        }
        else
        {
            //chk.Enabled = true;
            CheckBox2.Enabled = true;
        }
        pnlitem.Visible = true;
        pnlmain.Visible = true;

        foreach (DataRow item in ds2.Rows)
        {

            item["Shortage"] = Convert.ToString(Convert.ToDecimal(item["InvQty"]) - Convert.ToDecimal(item["RecdQty"]));
            Decimal samt = Convert.ToDecimal(item["Shortage"]) * Convert.ToDecimal(item["Rate"]);
            item["ShortageAmt"] = Math.Round(samt, 2).ToString();
            item["Totals"] = Convert.ToString(Convert.ToDouble(item["RecdQty"]) * Convert.ToDouble(item["Rate"])); ;
        }
        GridView1.DataSource = ds2;
        GridView1.DataBind();

        ViewState["dt"] = ds2;
        //if (CheckBox2.Checked == true)
        //{
        //    dtrow["Totals"] = Convert.ToDecimal(txtTotals.Text) + Convert.ToDecimal(txtt1.Text) + Convert.ToDecimal(txtt2.Text) + Convert.ToDecimal(txtt3.Text);
        //}
        //else
        //{
        //    dtrow["Totals"] = txtTotals.Text;
        //}
        gridtax();
        GridView1.Columns[13].Visible = false;
        GridView1.Columns[14].Visible = false;
        GridView1.Columns[15].Visible = false;
        if (CheckBox2.SelectedIndex == 0)
        {
            if (Convert.ToString(ViewState["Taxallo"]) == "3")
            {
                GridView1.Columns[13].HeaderText = lblt1.Text;
                GridView1.Columns[14].HeaderText = lblt2.Text;
                GridView1.Columns[15].HeaderText = lblt3.Text;
                GridView1.Columns[13].Visible = true;
                GridView1.Columns[14].Visible = true;
                GridView1.Columns[15].Visible = true;
            }
            else if (Convert.ToString(ViewState["Taxallo"]) == "2")
            {
                GridView1.Columns[13].HeaderText = lblt1.Text;
                GridView1.Columns[14].HeaderText = lblt2.Text;
                GridView1.Columns[13].Visible = true;
                GridView1.Columns[14].Visible = true;
            }
            else if (Convert.ToString(ViewState["Taxallo"]) == "1")
            {
                GridView1.Columns[13].HeaderText = lblt1.Text;

                GridView1.Columns[13].Visible = true;

            }
        }
        recal();



    }

    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtBar.Text = "";
        Panel1.Visible = false;
        string selectcmd1 = "SELECT   distinct  InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName + ' : ' + InventoruSubSubCategory.InventorySubSubName + ' : ' + InventoryMaster.Name + ' : ' + Case when( InventoryMaster.ProductNo IS NULL) then '-' else InventoryMaster.ProductNo End+ ' : ' + cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+ ' : ' + UnitTypeMaster.Name AS Category, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name, InventoryMaster.InventoryMasterId, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active FROM         InventoryMaster INNER JOIN InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId Inner Join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId WHERE  InventoryCategoryMaster.CatType IS NULL and   (InventoryWarehouseMasterTbl.Active = 1) and InventoryMaster.MasterActiveStatus=1 and (InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "') ORDER BY Category ";

        SqlCommand cmd111 = new SqlCommand(selectcmd1, con);
        SqlDataAdapter adp111 = new SqlDataAdapter(cmd111);
        DataTable dt111 = new DataTable();
        adp111.Fill(dt111);
        ddlCategory.DataSource = dt111;
        ddlCategory.DataTextField = "Category";
        ddlCategory.DataValueField = "InventoryWarehouseMasterId";
        ddlCategory.Items.Insert(0, "-Select-");
        ddlCategory.SelectedItem.Value = "0";
        ddlCategory.DataBind();

        lblmmssgg.Visible = false;

        PnlCategory.Visible = false;

        string selectcmd2111 = " SELECT distinct   InventoryMaster.Name + ' : ' + Case when( InventoryMaster.ProductNo IS NULL) then '-' else InventoryMaster.ProductNo End+ ' : ' + cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+ ' : ' + UnitTypeMaster.Name AS Category, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName, " +
                        "  InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name, InventoryMaster.InventoryMasterId,  " +
                        "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active " +
                        "  FROM         InventoryMaster INNER JOIN " +
                        "  InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                        "  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                        "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN " +
                        "  InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId Inner Join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId " +
                         "  WHERE  InventoryCategoryMaster.CatType IS NULL and   (InventoryWarehouseMasterTbl.Active = 1) and InventoryMaster.MasterActiveStatus=1 and (InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "') " +
                        " ORDER BY " +
                        "  Category";

        SqlCommand cmd1111 = new SqlCommand(selectcmd2111, con);
        SqlDataAdapter adp1111 = new SqlDataAdapter(cmd1111);
        DataTable dt1111 = new DataTable();
        adp1111.Fill(dt1111);
        ddlItemname.DataSource = dt1111;
        ddlItemname.DataTextField = "Category";
        ddlItemname.DataValueField = "InventoryWarehouseMasterId";
        ddlItemname.Items.Insert(0, "-Select-");
        ddlItemname.SelectedItem.Value = "0";
        ddlItemname.DataBind();

        getProductNo();
        if (RadioButtonList2.SelectedValue == "0") //cat
        {
            PnlCategory.Visible = true;

            Panel1.Visible = false;

            PnlMainBarcode.Visible = false;
            //Label7.Visible = false;
            // txtBar.Visible = false;
            pnlproduct.Visible = false;
            //ddlProductNo.Visible = false;
            // lblProductno.Visible = false;
            pnlInv.Visible = true;
            pnlPO.Visible = false;

            pnlName.Visible = false;
        }
        else if (RadioButtonList2.SelectedValue == "1") // name
        {
            Panel1.Visible = true;
            //Label6.Visible = true;
            //ddlItemname.Visible = true;
            PnlMainBarcode.Visible = false;
            //Label7.Visible = false;
            //txtBar.Visible = false;
            PnlCategory.Visible = false;
            //Label5.Visible = false;
            //ddlCategory.Visible = false;
            pnlproduct.Visible = false;
            //ddlProductNo.Visible = false;
            // lblProductno.Visible = false;
            pnlName.Visible = true;
            pnlInv.Visible = false;
            pnlPO.Visible = false;

            PnlMainBarcode.Visible = false;
        }
        else if (RadioButtonList2.SelectedValue == "2") //br
        {
            PnlMainBarcode.Visible = true;
            //Label7.Visible = true;
            //txtBar.Visible = true;
            Panel1.Visible = false;
            //Label6.Visible = false;
            //ddlItemname.Visible = false;
            PnlCategory.Visible = false;
            //Label5.Visible = false;
            //ddlCategory.Visible = false;
            pnlproduct.Visible = false;
            //ddlProductNo.Visible = false;
            //lblProductno.Visible = false;

            pnlInv.Visible = false;
            pnlPO.Visible = false;
            PnlMainBarcode.Visible = true;
            lblInvname1.Visible = false;
            pnlName.Visible = false;
        }
        else if (RadioButtonList2.SelectedValue == "3") // po
        {
            PnlMainBarcode.Visible = false;
            //Label7.Visible = false;
            //txtBar.Visible = false;
            Panel1.Visible = false;
            //Label6.Visible = false;
            //ddlItemname.Visible = false;
            PnlCategory.Visible = false;
            //Label5.Visible = false;
            //ddlCategory.Visible = false;
            pnlproduct.Visible = true;
            //ddlProductNo.Visible = true;
            //lblProductno.Visible = true;
            pnlInv.Visible = false;
            pnlPO.Visible = true;

            PnlMainBarcode.Visible = false;
            pnlName.Visible = false;
        }


    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        btnCalCase_Click(sender, e);
        btSubmit.Visible = false;
        btCal.Visible = true;
        GridView1.Enabled = true;
        Panel1.Visible = false;
        //  Response.Write("aaaa");
        string s = "", s1 = "", s2 = "", s4 = "", s5 = "";
        if (RadioButtonList2.SelectedIndex == 0)
        {

            Session["warehousmasterid"] = Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString());
            string str1 = "SELECT    Left(InventoryCategoryMaster.InventoryCatName,8) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,8) + ' : ' +Left( InventoruSubSubCategory.InventorySubSubName,8) as Category,  InventoryMaster.Name, InventoryMaster.InventoryMasterId, InventoryMaster.ProductNo as Barcode," +
                 " cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+ ' ' + UnitTypeMaster.Name AS UnitValues, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, " +
                  " InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active " +
                  "  FROM         InventoryMaster INNER JOIN " +
                 " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                 " InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                 " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN " +
                 " InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id INNER JOIN " +
                  " InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId inner Join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId" +
                  " WHERE  InventoryMaster.CatType IS NULL and    (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = '" + Convert.ToInt32(ddlCategory.SelectedValue) + "') AND (InventoryWarehouseMasterTbl.Active = 1)";


            SqlCommand cm1 = new SqlCommand(str1, con);
            cm1.CommandType = CommandType.Text;
            SqlDataAdapter da1 = new SqlDataAdapter(cm1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);


            s = ddlCategory.SelectedValue.ToString();
            s1 = ds1.Tables[0].Rows[0]["Category"].ToString();
            s2 = ds1.Tables[0].Rows[0]["Name"].ToString();
            s4 = ds1.Tables[0].Rows[0]["Barcode"].ToString();
            s5 = ds1.Tables[0].Rows[0]["UnitValues"].ToString();


        }
        else if (RadioButtonList2.SelectedIndex == 1)
        {

            Session["warehousmasterid"] = Convert.ToInt32(ddlItemname.SelectedItem.Value.ToString());
            string str12 = " SELECT     Left(InventoryCategoryMaster.InventoryCatName,8) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,8) + ' : ' +Left( InventoruSubSubCategory.InventorySubSubName,8) as Category, InventoryCategoryMaster.InventoryCatName, InventoryMaster.Name, InventoryMaster.InventoryMasterId, " +
                 "cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+ ' ' + UnitTypeMaster.Name AS UnitValues, InventoryMaster.ProductNo as Barcode, InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.WareHouseId, " +
                 " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                 "  FROM         InventoryMaster INNER JOIN " +
                 " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                 " InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                 " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN " +
                 " InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id INNER JOIN " +
                 "  InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId inner Join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId " +
                 " where  InventoryMaster.CatType IS NULL and  (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = '" + Convert.ToInt32(ddlItemname.SelectedValue) + "') " +
                 " and (InventoryWarehouseMasterTbl.Active=1) and InventoryMaster.MasterActiveStatus=1 ";

            SqlCommand cm12 = new SqlCommand(str12, con);
            cm12.CommandType = CommandType.Text;
            SqlDataAdapter da12 = new SqlDataAdapter(cm12);
            DataSet ds12 = new DataSet();
            da12.Fill(ds12);

            s = ddlItemname.SelectedValue.ToString();
            s1 = ds12.Tables[0].Rows[0]["Category"].ToString();

            s2 = ds12.Tables[0].Rows[0]["Name"].ToString();
            s4 = ds12.Tables[0].Rows[0]["Barcode"].ToString();
            s5 = ds12.Tables[0].Rows[0]["UnitValues"].ToString();

        }

        else if (RadioButtonList2.SelectedIndex == 2)
        {
            if (txtBar.Text != "")
            {
                if (ddlWarehouse.SelectedIndex >= 0)
                {
                    Session["warehouseid1"] = Convert.ToInt32(ddlWarehouse.SelectedItem.Value.ToString());
                    string str = " SELECT     Left(InventoryCategoryMaster.InventoryCatName,8) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,8) + ' : ' +Left( InventoruSubSubCategory.InventorySubSubName,8) as Category, InventoryCategoryMaster.InventoryCatName, InventoryMaster.Name, InventoryMaster.InventoryMasterId, " +
                " InventoryMaster.ProductNo as Barcode, InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.WareHouseId, " +
                         " cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+ ' ' + UnitTypeMaster.Name AS UnitValues, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                        " FROM         InventoryMaster INNER JOIN " +
                         "  InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                         "  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                         "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN " +
                         "  InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id INNER JOIN " +
                         "  InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId inner Join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId " +
                         " WHERE   InventoryMaster.CatType IS NULL and    (InventoryBarcodeMaster.Barcode = '" + txtBar.Text + "') and InventoryMaster.MasterActiveStatus=1 AND (InventoryWarehouseMasterTbl.Active = 1) " +
                         " and (InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "') ";


                    SqlCommand cm = new SqlCommand(str, con);
                    cm.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        s = ds.Tables[0].Rows[0]["InventoryWarehouseMasterId"].ToString();
                        Session["warehousmasterid"] = s;
                        s1 = ds.Tables[0].Rows[0]["Category"].ToString();
                        s2 = ds.Tables[0].Rows[0]["Name"].ToString();
                        s4 = txtBar.Text;
                        s5 = ds.Tables[0].Rows[0]["UnitValues"].ToString();

                    }
                    else
                    {
                        Label2.Text = "Invalid Barcode.";
                        Label2.Visible = true;
                        return;
                    }
                }
            }
            else
            {
                Label2.Text = "Please enter Barcode.";
                Label2.Visible = true;
                return;
            }

        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            //Response.Write(ddlProductNo.SelectedItem.Value.ToString());
            if (ddlProductNo.SelectedIndex > 0)
            {

                Session["warehousmasterid"] = Convert.ToInt32(ddlProductNo.SelectedItem.Value.ToString());
                string str12 = "Select Left(InventoryCategoryMaster.InventoryCatName,8) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,8) + ' : ' +Left( InventoruSubSubCategory.InventorySubSubName,8) as Category, " +
                        " InventoryCategoryMaster.InventoryCatName,InventoryMaster.ProductNo as Barcode, InventoryMaster.Name, InventoryMaster.InventoryMasterId, " +
                         "  InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.WareHouseId, " +
                         "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+ ' ' + UnitTypeMaster.Name AS UnitValues " +
                        " FROM         InventoryMaster INNER JOIN " +
                         "  InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                         "  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                         "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN " +
                         "  InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id INNER JOIN " +
                         "  InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId inner Join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId  " +
                         " WHERE   InventoryMaster.CatType IS NULL and   (InventoryWarehouseMasterTbl.Active = 1) and InventoryMaster.MasterActiveStatus=1 " +
                         " and (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + ddlProductNo.SelectedValue + "') ";



                SqlCommand cm12 = new SqlCommand(str12, con);
                cm12.CommandType = CommandType.Text;
                SqlDataAdapter da12 = new SqlDataAdapter(cm12);
                DataSet ds12 = new DataSet();
                da12.Fill(ds12);
                if (ds12.Tables[0].Rows.Count > 0)
                {
                    s = ddlProductNo.SelectedValue.ToString();
                    s2 = ds12.Tables[0].Rows[0]["Name"].ToString();
                    s1 = ds12.Tables[0].Rows[0]["Category"].ToString();
                    s4 = ds12.Tables[0].Rows[0]["Barcode"].ToString();
                    s5 = ds12.Tables[0].Rows[0]["UnitValues"].ToString();

                }
            }
            else
            {
                if (ddlProductNo.SelectedItem.Value.ToString() == "0")
                {
                    Label2.Text = "Please select Item.";
                    Label2.Visible = true;
                    return;
                }
            }
        }
        else
        {
            if (ddlProductNo.SelectedIndex > 0)
            {

                Session["warehousmasterid"] = Convert.ToInt32(ddlProductNo.SelectedItem.Value.ToString());
                string str12 = "Select Left(InventoryCategoryMaster.InventoryCatName,8) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,8) + ' : ' +Left( InventoruSubSubCategory.InventorySubSubName,8) as Category, " +
                       " InventoryCategoryMaster.InventoryCatName,InventoryMaster.ProductNo as Barcode, InventoryMaster.Name, InventoryMaster.InventoryMasterId, " +
                        "  InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.WareHouseId, " +
                        "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+ ' ' + UnitTypeMaster.Name AS UnitValues " +
                       " FROM      InventoryMaster INNER JOIN " +
                         "  InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                         "  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                         "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN " +
                         "  InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id INNER JOIN " +
                         "  InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId inner Join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId  " +
                         " WHERE   InventoryMaster.CatType IS NULL and   (InventoryWarehouseMasterTbl.Active = 1) and InventoryMaster.MasterActiveStatus=1 " +
                         " and (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + ddlProductNo.SelectedValue + "') ";




                SqlCommand cm12 = new SqlCommand(str12, con);
                cm12.CommandType = CommandType.Text;
                SqlDataAdapter da12 = new SqlDataAdapter(cm12);
                DataSet ds12 = new DataSet();
                da12.Fill(ds12);
                if (ds12.Tables[0].Rows.Count > 0)
                {
                    s = ddlProductNo.SelectedValue.ToString();
                    s2 = ds12.Tables[0].Rows[0]["Name"].ToString();
                    s1 = ds12.Tables[0].Rows[0]["Category"].ToString();
                    s4 = ds12.Tables[0].Rows[0]["Barcode"].ToString();
                    s5 = ds12.Tables[0].Rows[0]["UnitValues"].ToString();

                }
            }
            else
            {
                if (ddlProductNo.SelectedItem.Value.ToString() == "0")
                {
                    Label2.Text = "Please select Item.";
                    Label2.Visible = true;
                    return;
                }
            }
        }
        DataTable dt = new DataTable();

        DataColumn dtcom35 = new DataColumn();
        dtcom35.DataType = System.Type.GetType("System.String");
        dtcom35.ColumnName = "InventoryWarehouseMasterId";
        dtcom35.ReadOnly = false;
        dtcom35.Unique = false;
        dtcom35.AllowDBNull = true;

        dt.Columns.Add(dtcom35);



        DataColumn dtcom = new DataColumn();
        dtcom.DataType = System.Type.GetType("System.String");
        dtcom.ColumnName = "Category";
        dtcom.ReadOnly = false;
        dtcom.Unique = false;
        dtcom.AllowDBNull = true;

        dt.Columns.Add(dtcom);

        DataColumn dtcompi = new DataColumn();
        dtcompi.DataType = System.Type.GetType("System.String");
        dtcompi.ColumnName = "Purchase_Details_Id";
        dtcompi.ReadOnly = false;
        dtcompi.Unique = false;
        dtcompi.AllowDBNull = true;

        dt.Columns.Add(dtcompi);
        DataColumn dtcom12 = new DataColumn();
        dtcom12.DataType = System.Type.GetType("System.String");
        dtcom12.ColumnName = "Name";
        dtcom12.ReadOnly = false;
        dtcom12.Unique = false;
        dtcom12.AllowDBNull = true;

        dt.Columns.Add(dtcom12);





        DataColumn dtcom1 = new DataColumn();
        dtcom1.DataType = System.Type.GetType("System.String");
        dtcom1.ColumnName = "Barcode";
        dtcom1.ReadOnly = false;
        dtcom1.Unique = false;
        dtcom1.AllowDBNull = true;
        dt.Columns.Add(dtcom1);




        DataColumn dtcom2 = new DataColumn();
        dtcom2.DataType = System.Type.GetType("System.String");
        dtcom2.ColumnName = "BatchNo";
        dtcom2.ReadOnly = false;
        dtcom2.Unique = false;
        dtcom2.AllowDBNull = true;
        dt.Columns.Add(dtcom2);


        DataColumn dtcom3 = new DataColumn();
        dtcom3.DataType = System.Type.GetType("System.String");
        dtcom3.ColumnName = "ExpiryDate";
        dtcom3.ReadOnly = false;
        dtcom3.Unique = false;
        dtcom3.AllowDBNull = true;
        dt.Columns.Add(dtcom3);




        DataColumn dtcom4 = new DataColumn();
        dtcom4.DataType = System.Type.GetType("System.String");
        dtcom4.ColumnName = "InvQty";
        dtcom4.ReadOnly = false;
        dtcom4.Unique = false;
        dtcom4.AllowDBNull = true;
        dt.Columns.Add(dtcom4);

        DataColumn dtcom5 = new DataColumn();
        dtcom5.DataType = System.Type.GetType("System.String");
        dtcom5.ColumnName = "Rate";
        dtcom5.ReadOnly = false;
        dtcom5.Unique = false;
        dtcom5.AllowDBNull = true;
        dt.Columns.Add(dtcom5);

        DataColumn dtcom6 = new DataColumn();
        dtcom6.DataType = System.Type.GetType("System.String");
        dtcom6.ColumnName = "RecdQty";
        dtcom6.ReadOnly = false;
        dtcom6.Unique = false;
        dtcom6.AllowDBNull = true;
        dt.Columns.Add(dtcom6);

        DataColumn dtcomsh = new DataColumn();
        dtcomsh.DataType = System.Type.GetType("System.String");
        dtcomsh.ColumnName = "Shortage";
        dtcomsh.ReadOnly = false;
        dtcomsh.Unique = false;
        dtcomsh.AllowDBNull = true;
        dt.Columns.Add(dtcomsh);

        DataColumn dtcomsha = new DataColumn();
        dtcomsha.DataType = System.Type.GetType("System.String");
        dtcomsha.ColumnName = "ShortageAmt";
        dtcomsha.ReadOnly = false;
        dtcomsha.Unique = false;
        dtcomsha.AllowDBNull = true;
        dt.Columns.Add(dtcomsha);

        DataColumn dtcomshuv = new DataColumn();
        dtcomshuv.DataType = System.Type.GetType("System.String");
        dtcomshuv.ColumnName = "UnitValues";
        dtcomshuv.ReadOnly = false;
        dtcomshuv.Unique = false;
        dtcomshuv.AllowDBNull = true;
        dt.Columns.Add(dtcomshuv);
        DataColumn dtcom10 = new DataColumn();
        dtcom10.DataType = System.Type.GetType("System.String");
        dtcom10.ColumnName = "Tax1";
        dtcom10.ReadOnly = false;
        dtcom10.Unique = false;
        dtcom10.AllowDBNull = true;
        dt.Columns.Add(dtcom10);

        DataColumn dtcom11 = new DataColumn();
        dtcom11.DataType = System.Type.GetType("System.String");
        dtcom11.ColumnName = "Tax2";
        dtcom11.ReadOnly = false;
        dtcom11.Unique = false;
        dtcom11.AllowDBNull = true;
        dt.Columns.Add(dtcom11);

        DataColumn dtcom11ta = new DataColumn();
        dtcom11ta.DataType = System.Type.GetType("System.String");
        dtcom11ta.ColumnName = "Tax3";
        dtcom11ta.ReadOnly = false;
        dtcom11ta.Unique = false;
        dtcom11ta.AllowDBNull = true;
        dt.Columns.Add(dtcom11ta);

        DataColumn dtcom7 = new DataColumn();
        dtcom7.DataType = System.Type.GetType("System.String");
        dtcom7.ColumnName = "Totals";
        dtcom7.ReadOnly = false;
        dtcom7.Unique = false;
        dtcom7.AllowDBNull = true;
        dt.Columns.Add(dtcom7);

        if (ViewState["dt"] != null)
        {
            dt = (DataTable)ViewState["dt"];

        }
        DataRow dtrow = dt.NewRow();
        dtrow["InventoryWarehouseMasterId"] = s;
        dtrow["Category"] = s1;
        dtrow["Name"] = s2;
        dtrow["Barcode"] = s4;
        dtrow["UnitValues"] = s5;
        dtrow["BatchNo"] = txtBatch.Text;
        dtrow["ExpiryDate"] = txtExDate.Text;
        dtrow["InvQty"] = txtInvQty.Text;
        dtrow["Rate"] = txtRate11.Text;
        dtrow["Purchase_Details_Id"] = "0";
        dtrow["Tax1"] = txtt1.Text;
        dtrow["Tax2"] = txtt2.Text;
        dtrow["Tax3"] = txtt3.Text;
        if (CheckBox2.SelectedIndex == 0 || rdl1.SelectedIndex == 0)
        {
            if (Convert.ToString(txtt1.Text) == "")
            {
                txtt1.Text = "0";
            }
            txtTax1.Text = (Convert.ToString(Convert.ToDouble(txtTax1.Text) + Convert.ToDouble(txtt1.Text)));
            if (Convert.ToString(txtt2.Text) == "")
            {
                txtt2.Text = "0";
            }
            txtTax2.Text = (Convert.ToString(Convert.ToDouble(txtTax2.Text) + Convert.ToDouble(txtt2.Text)));
            if (Convert.ToString(txtt3.Text) == "")
            {
                txtt3.Text = "0";
            }
            txtTax3.Text = (Convert.ToString(Convert.ToDouble(txtTax3.Text) + Convert.ToDouble(txtt3.Text)));

        }
        if (txtRecdQty.Text != "")
        {
            dtrow["RecdQty"] = txtRecdQty.Text;
        }
        else
        {
            dtrow["RecdQty"] = txtInvQty.Text;

        }
        dtrow["Shortage"] = Convert.ToInt32(dtrow["InvQty"]) - Convert.ToInt32(dtrow["RecdQty"]);
        Decimal samt = Convert.ToDecimal(dtrow["Shortage"]) * Convert.ToDecimal(dtrow["Rate"]);
        dtrow["ShortageAmt"] = Math.Round(samt, 2);
        //if (CheckBox2.Checked == true)
        //{
        //    dtrow["Totals"] = Convert.ToDecimal(txtTotals.Text) + Convert.ToDecimal(txtt1.Text) + Convert.ToDecimal(txtt2.Text) + Convert.ToDecimal(txtt3.Text);
        //}
        //else
        //{
        //    dtrow["Totals"] = txtTotals.Text;
        //}
        dtrow["Totals"] = txtTotals.Text;
        dt.Rows.Add(dtrow);

        if (dt.Rows.Count > 0)
        {
            //if (CheckBox2.Checked == true)
            //{
            //    GridView1.Columns[10].Visible = true;
            //    GridView1.Columns[11].Visible = true;
            //    GridView1.Columns[12].Visible = true;
            //}
            //else
            //{
            //    GridView1.Columns[10].Visible = false;
            //    GridView1.Columns[11].Visible = false;
            //    GridView1.Columns[12].Visible = false;
            //}
            GridView1.Columns[13].Visible = false;
            GridView1.Columns[14].Visible = false;
            GridView1.Columns[15].Visible = false;
            if (CheckBox2.SelectedIndex == 0)
            {
                if (Convert.ToString(ViewState["Taxallo"]) == "3")
                {
                    GridView1.Columns[13].HeaderText = lblt1.Text;
                    GridView1.Columns[14].HeaderText = lblt2.Text;
                    GridView1.Columns[15].HeaderText = lblt3.Text;
                    GridView1.Columns[13].Visible = true;
                    GridView1.Columns[14].Visible = true;
                    GridView1.Columns[15].Visible = true;
                }
                else if (Convert.ToString(ViewState["Taxallo"]) == "2")
                {
                    GridView1.Columns[13].HeaderText = lblt1.Text;
                    GridView1.Columns[14].HeaderText = lblt2.Text;
                    GridView1.Columns[13].Visible = true;
                    GridView1.Columns[14].Visible = true;
                }
                else if (Convert.ToString(ViewState["Taxallo"]) == "1")
                {
                    GridView1.Columns[13].HeaderText = lblt1.Text;

                    GridView1.Columns[13].Visible = true;

                }
            }
        }
        GridView1.DataSource = dt;
        ViewState["dt"] = dt;
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            //chk.Enabled = false;
            CheckBox2.Enabled = false;
        }
        else
        {
            //chk.Enabled = true;
            CheckBox2.Enabled = true;
        }
        recal();
        //btCal_Click(sender, e);
        Clear();

    }
    protected void Clear()
    {
        txtBatch.Text = "";
        txtExDate.Text = "";
        txtInvNoCase.Text = "";
        txtUnitPrCase.Text = "";
        txtPricePrCase.Text = "";
        txtRecNoCase.Text = "";
        txtInvQty.Text = "";
        txtRecdQty.Text = "";
        txtRate11.Text = "";
        txtFreeCases.Text = "";
        txtt1.Text = "";
        txtt2.Text = "";
        txtt3.Text = "";
        if (txtBar.Text != "")
        {
            txtBatch.Text = "";
        }
        lblInvName.Text = "";
        lblInvname1.Visible = false;
        txtTotals.Text = "";
    }


    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        //Calendar2.Visible = true;
    }

    protected void ddlpartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string str123 = " SELECT     PartyID, Account,City ,State      ,Country, Compname, PartyTypeId, Address FROM         Party_master " +
        //                 " Where PartyID = '" + ddlpartyName.SelectedValue + "'";
        //SqlCommand cm133 = new SqlCommand(str123, con);
        //cm133.CommandType = CommandType.Text;
        //SqlDataAdapter da133 = new SqlDataAdapter(cm133);
        //DataSet ds133 = new DataSet();
        //da133.Fill(ds133);

        //if (ds133.Tables[0].Rows.Count > 0)
        //{
        //    txtAddress.Text = ds133.Tables[0].Rows[0]["Address"].ToString();

        //    string strconu = " SELECT  CountryId       ,CountryName      ,Country_Code  FROM CountryMaster where CountryId=" + Convert.ToInt32(isintornot(ds133.Tables[0].Rows[0]["Country"].ToString())) + " ";
        //    string Countr = returnCSCt(strconu, "CountryName");
        //    string ct3 = " SELECT  CityId      ,CityName      ,StateId  FROM CityMasterTbl where CityId=" + Convert.ToInt32(isintornot(ds133.Tables[0].Rows[0]["City"].ToString()));
        //    string STatecin = "SELECT  StateId      ,StateName,CountryId  ,State_Code  FROM StateMasterTbl where StateId=" + Convert.ToInt32(isintornot(ds133.Tables[0].Rows[0]["State"].ToString()));
        //    string st = returnCSCt(STatecin, "StateName");
        //    string ct = returnCSCt(ct3, "CityName");

        //    txtAddress.Text += "<br/>" + Countr + "<br/>" + st + "<br/>" + ct + "<br/>";


        //}
    }
    protected string returnCSCt(string strrr, string rtSTT)
    {
        SqlCommand cmdstrr = new SqlCommand(strrr, con);
        SqlDataAdapter adprrr = new SqlDataAdapter(cmdstrr);
        DataTable dtrrr = new DataTable();
        adprrr.Fill(dtrrr);
        string SCTST = "";
        if (dtrrr.Rows.Count > 0)
        {
            SCTST = dtrrr.Rows[0][rtSTT].ToString();
        }
        return SCTST;

    }

    protected int isintornot(string ck)
    {
        int ihk = 0;
        try
        {
            ihk = Convert.ToInt32(ck);
            return ihk;
        }
        catch
        {
            return ihk;
        }
        //return ihk;

    }
    protected void btCal_Click(object sender, EventArgs e)
    {
          int abcd = 0;
          string maininv = "";
            int ly = 0;
            foreach (GridViewRow itm in GridView1.Rows)
            {
                Label lblIWMid = (Label)itm.FindControl("lblIWMid");
                TextBox txtRecdQty = (TextBox)itm.FindControl("txtRecdQty");
                if (ly > 0)
                {
                    maininv = maininv + ",";
                }
                else
                {
                    ly += 1;
                }

                maininv = maininv + lblIWMid.Text;

              
                if (Convert.ToDateTime(ViewState["sdo"]) == Convert.ToDateTime(txtDate.Text))
                {
                    invtype = "1";
                    ViewState["sdo"] = txtDate.Text;
                }
                else if (Convert.ToDateTime(ViewState["sdo"])>= Convert.ToDateTime(txtDate.Text))
                {
                    invtype = "2";
                    ViewState["sdo"] = txtDate.Text;
                }
                else
                {
                    invtype = "3";

                }

                 abcd = FillCountQty(ViewState["Tid"].ToString(), lblIWMid.Text, txtRecdQty.Text, invtype);
                 if (abcd == 0)
                 {
                     if (abcd == 1)
                     {
                         Label8.Visible = true;
                         Label8.Text = "You cannot delete this entry as it may result into negative quantity in this item because of sale entry made after the date of this invoice";
                         break;
                     }
                 }
                 else
                 {
                     if (abcd == 1)
                     {
                         Label8.Visible = true;
                         Label8.Text = "You cannot delete this entry as it may result into negative quantity in this item because of sale entry made after the date of this invoice";
                         break;
                     }
                 }
            }
          
            if (abcd == 0)
            {
                abcd = delqtycount(maininv, ViewState["Tid"].ToString());
                if (abcd == 1)
                {
                    Label8.Visible = true;
                    Label8.Text = "You cannot delete this entry as it may result into negative quantity in this item because of sale entry made after the date of this invoice";

                }
            }
            if (abcd == 0)
            {
               
                recal();
                if (GridView1.Rows.Count > 0)
                {
                    btSubmit.Visible = true;
                    GridView1.Enabled = false;
                    btCal.Visible = false;
                }
                else
                {
                    btSubmit.Visible = false;
                    GridView1.Enabled = true;
                    btCal.Visible = true;
                }
            }
    }
    protected int FillCountQty(string traid, string invid,  string recqty, string invtype)
    {
        int Anr = 0;
        decimal inwavgid = 0;
        string id12 = invid;
       
        decimal oLDqtyONHAND = 0;
         decimal Finalqtyhand = 0;
        if (invtype == "1")
        {
            DataTable Datfi = select("SELECT IWMAvgCostId  FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and Tranction_Master_Id='" + traid + "'");
            if (Datfi.Rows.Count > 0)
            {
                inwavgid = Convert.ToDecimal(Datfi.Rows[0]["IWMAvgCostId"]);
            }
           
        }
        if (invtype == "1" || invtype == "2")
        {
            DataTable drtinvdata = new DataTable();
            if (invtype == "1")
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<='" + ViewState["sdo"] + "' and Tranction_Master_Id<'" + traid + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
                if (drtinvdata.Rows.Count == 0)
                {
                    drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<'" + ViewState["sdo"] + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
             
                }
            }
            else
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<='" + ViewState["sdo"] + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
            }
            if (drtinvdata.Rows.Count > 0)
            {
              
                if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
                {
                    oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
                }

            }
           
            Finalqtyhand = Convert.ToDecimal(recqty) + oLDqtyONHAND;
           
        }
        string pera = "";
        if (invtype == "1")
        {
            pera = "  and DateUpdated>='" + ViewState["sdo"] + "' ";
        }
        else
        {
            pera = "  and DateUpdated>'" + ViewState["sdo"] + "'";
        }

        DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' " + pera + " order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");

        string ABC = "";
        foreach (DataRow itm in Dataupval.Rows)
        {
            if (ABC == "")
            {
                if (invtype == "1")
                {
                   
                    if((Convert.ToDateTime(ViewState["sdo"]) == Convert.ToDateTime(itm["DateUpdated"]))&& (Convert.ToDecimal(itm["Tranction_Master_Id"]) > Convert.ToDecimal(traid)))
                    {
                        ABC = "13";
                    }
                    else if (Convert.ToDateTime(ViewState["sdo"]) < Convert.ToDateTime(itm["DateUpdated"]))
                    {
                        ABC = "13";
                    }
                }
                else
                {
                    ABC = "12";
                }
            }
            if (ABC != "")
            {
                if (Convert.ToDateTime(ViewState["sdo"]) <= Convert.ToDateTime(itm["DateUpdated"]))
                {
                    if ((Convert.ToDateTime(ViewState["sdo"]) == Convert.ToDateTime(itm["DateUpdated"])) && (Convert.ToDecimal(traid) < Convert.ToDecimal(itm["Tranction_Master_Id"])))
                    {
                        Finalqtyhand += Convert.ToDecimal(recqty);
                    }
                    else
                    {
                        Finalqtyhand += Convert.ToDecimal(recqty);
                    }
                }
                    Finalqtyhand = Finalqtyhand + Convert.ToDecimal(itm["Qty"]);
                    if (Finalqtyhand < 0)
                    {
                        Anr = 1;
                        break;
                    }
            }

        }
        return Anr;
    }
    protected int delqtycount(string maininv, string traid)
    {
        int Anr = 0;
        DataTable Datfi = select("SELECT IWMAvgCostId,InvWMasterId  FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId NOT IN(" + maininv + ") and Tranction_Master_Id='" + traid + "'");
        foreach (DataRow intvay in Datfi.Rows)
        {
         
            decimal oLDqtyONHAND = 0;
            
            DataTable drtinvdata = new DataTable();
             drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated<='" + ViewState["sdo1"] + "' and Tranction_Master_Id<'" + traid + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
            if (drtinvdata.Rows.Count == 0)
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated<'" + ViewState["sdo1"] + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

            }
            if (drtinvdata.Rows.Count > 0)
            {
              
                if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
                {
                    oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
                }
            }
            decimal Finalqtyhand = 0;

            Finalqtyhand = oLDqtyONHAND;
           
            DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated>='" + ViewState["sdo1"] + "' order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
             string ABC = "";
             foreach (DataRow itm in Dataupval.Rows)
             {
                 if (ABC == "")
                 {
                     if (Convert.ToDateTime(itm["DateUpdated"]) == Convert.ToDateTime(ViewState["sdo1"]))
                     {
                         if (Convert.ToDecimal(itm["Tranction_Master_Id"]) > Convert.ToDecimal(traid))
                         {
                             ABC = "13";
                         }
                     }
                     else
                     {
                         ABC = "12";
                     }
                 }
                 if (ABC != "")
                 {

                     Finalqtyhand = Finalqtyhand + Convert.ToDecimal(itm["Qty"]);
                     if (Finalqtyhand < 0)
                     {
                         Anr = 1;
                         break;

                     }
                 }
             }
        }
        return Anr;
    }
    protected void recal()
    {
        if (GridView1.Rows.Count > 0)
        {
            btSubmit.Visible = false;
            btCal.Visible = true;

            Decimal samt = 0;
            string s1 = "", s2 = "";
            decimal d1 = 0, d2 = 0;
            foreach (GridViewRow gdr in GridView1.Rows)
            {
                if (s1 == "" && s2 == "")
                {
                    TextBox t = (TextBox)gdr.Cells[6].FindControl("txtInvQty");
                    TextBox t1 = (TextBox)gdr.Cells[7].FindControl("txtRate");
                    TextBox t2 = (TextBox)gdr.Cells[8].FindControl("txtRecdQty");
                    TextBox lblshortage = (TextBox)gdr.Cells[8].FindControl("lblshortage");
                    samt += Convert.ToDecimal(lblshortage.Text) * Convert.ToDecimal(t1.Text);
                    d1 = Convert.ToDecimal(t.Text) * Convert.ToDecimal(t1.Text);
                    d2 = Convert.ToDecimal(t2.Text) * Convert.ToDecimal(t1.Text);

                    s1 = d1.ToString();
                    s2 = d2.ToString();
                }
                else
                {
                    TextBox t = (TextBox)gdr.Cells[6].FindControl("txtInvQty");
                    TextBox t1 = (TextBox)gdr.Cells[7].FindControl("txtRate");
                    TextBox t2 = (TextBox)gdr.Cells[8].FindControl("txtRecdQty");
                    TextBox lblshortage = (TextBox)gdr.Cells[8].FindControl("lblshortage");
                    samt += Convert.ToDecimal(lblshortage.Text) * Convert.ToDecimal(t1.Text);

                    d1 += Convert.ToDecimal(t.Text) * Convert.ToDecimal(t1.Text);
                    d2 += Convert.ToDecimal(t2.Text) * Convert.ToDecimal(t1.Text);

                    s1 = d1.ToString();
                    s2 = d2.ToString();
                }
            }
            // txtInvAmt.Text = s1;

            // txtInvAmt.Text = s2;
            if (txtTax1.Text.Length <= 0)
            {
                txtTax1.Text = "0";
            }
            if (txtTax2.Text.Length <= 0)
            {
                txtTax2.Text = "0";
            }
            if (txtTax3.Text.Length <= 0)
            {
                txtTax3.Text = "0";
            }
            if (txtShippCharge.Text.Length <= 0)
            {
                txtShippCharge.Text = "0";
            }
            txtShort.Text = Math.Round(Convert.ToDecimal(samt), 2).ToString();
            txtInvAmt.Text = Math.Round(Convert.ToDecimal(s2), 2).ToString();
            // txtValurRecd.Text = s2;
            decimal stc = samt + Convert.ToDecimal(s2);
            //txtValurRecd.Text = Math.Round(Convert.ToDecimal(s2), 2).ToString(); ;
            txtValurRecd.Text = Math.Round(Convert.ToDecimal(stc), 2).ToString();

            decimal d5 = 0;
            if (CheckBox2.SelectedIndex == 0)
            {
                d5 = Convert.ToDecimal(txtInvAmt.Text) + Convert.ToDecimal(txtShippCharge.Text);
            }
            else
            {

                d5 = Convert.ToDecimal(txtInvAmt.Text) + Convert.ToDecimal(txtTax1.Text) + Convert.ToDecimal(txtTax2.Text) + Convert.ToDecimal(txtTax3.Text) + Convert.ToDecimal(txtShippCharge.Text);

            }
            //txtNetAmount.Text = d5.ToString();
            txtNetAmount.Text = Math.Round(Convert.ToDecimal(d5), 2).ToString();

        }
        else
        {
            txtNetAmount.Text = "0";
            txtInvAmt.Text = "0";
            txtValurRecd.Text = "0";
            txtTax1.Text = "0";
            txtTax2.Text = "0";
            txtTax3.Text = "0";
            txtShippCharge.Text = "0";
            txtShort.Text = "0";
        }
    }
    protected void btSubmit_Click(object sender, EventArgs e)
    {

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }

        SqlTransaction transaction = con.BeginTransaction();

        try
        {

            string a1 = " Update PurchaseDetails set  ShippingTrackingNumber='" + txtTrackingNo.Text + "', PurchaseInvoiceNumber='" + txtInvNo.Text + "', Date='" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "', PartyName='" + ddlpartyName.SelectedValue + "', TexAmount1= '" + txtTax1.Text + "',texAmount2 = '" + txtTax2.Text + "'," +
            " TexAmount3='" + txtTax3.Text + "',Transporterid='" + ddlTransporter.SelectedValue + "', shippingandHandlingCharges= '" + txtShippCharge.Text + "',TotalAmt= '" + Convert.ToDecimal(txtNetAmount.Text) + "',InvType='" + RadioButtonList1.SelectedIndex + "'  " +
            " where Purchase_Details_Id='" + Convert.ToInt32(ViewState["Pid"]) + "' ";

            SqlCommand cd = new SqlCommand(a1, con);

            cd.Transaction = transaction;
            cd.ExecuteNonQuery();
            int Id = Convert.ToInt32(ViewState["Pid"]);


            DataTable dt1 = new DataTable();
            dt1 = (DataTable)ViewState["dt"];

            string str123 = "Update  TranctionMaster Set Date='" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "',UserId='" + Session["userid"] + "',Tranction_Amount='" + Convert.ToDecimal(txtNetAmount.Text) + "' where  Tranction_Master_Id='" + ViewState["Tid"] + "'";

            SqlCommand cd3 = new SqlCommand(str123, con);

            cd3.Transaction = transaction;
            cd3.ExecuteNonQuery();
            int Id1 = Convert.ToInt32(ViewState["Tid"]);

            ViewState["tid"] = Id1;
            string invtdetts = "Delete from TranctionMasterSuppliment where Tranction_Master_Id='" + ViewState["tid"] + "'";
            SqlCommand cmtrdel = new SqlCommand(invtdetts, con);

            cmtrdel.Transaction = transaction;
            cmtrdel.ExecuteNonQuery();
            string invtdet = "Delete from Tranction_Details where Tranction_Master_Id='" + ViewState["tid"] + "'";

            SqlCommand cmtrdeld = new SqlCommand(invtdet, con);

            cmtrdeld.Transaction = transaction;
            cmtrdeld.ExecuteNonQuery();
            if (RadioButtonList1.SelectedIndex == 0)
            {
                string ggg = " INSERT INTO  TranctionMasterSuppliment  (Tranction_Master_Id  ,AmountDue " +
                             " ,Memo  ,Party_MasterId,GrnMaster_Id) " +
                            "  VALUES('" + Convert.ToInt32(Id1) + "','" + Convert.ToDecimal(txtNetAmount.Text) + "', " +
                            "   '     ' , '" + Convert.ToInt32(ddlpartyName.SelectedValue) + "','" + txtpayduedate.Text + "' )";
                SqlCommand cd45r = new SqlCommand(ggg, con);
                cd45r.CommandType = CommandType.Text;
                cd45r.Transaction = transaction;
                cd45r.ExecuteNonQuery();
            }
            else
            {
                string ggg = " INSERT INTO  TranctionMasterSuppliment  (Tranction_Master_Id  ,AmountDue " +
                            " ,Memo  ,Party_MasterId,GrnMaster_Id ) " +
                           "  VALUES('" + Convert.ToInt32(Id1) + "','" + Convert.ToDecimal(0) + "', " +
                           "   '     ' , '" + Convert.ToInt32(ddlpartyName.SelectedValue) + "','" + txtpayduedate.Text + "' )";
                SqlCommand cd45r = new SqlCommand(ggg, con);
                cd45r.CommandType = CommandType.Text;
                cd45r.Transaction = transaction;
                cd45r.ExecuteNonQuery();
            }




            string acFormPjid = " SELECT Account FROM  Party_master where PartyID='" + ddlpartyName.SelectedValue + "' and id='" + Session["comid"] + "' and Whid='" + ddlWarehouse.SelectedValue + "' ";
            SqlCommand cmdacFrompjid = new SqlCommand(acFormPjid, conn);
            SqlDataAdapter adpAcfrompjid = new SqlDataAdapter(cmdacFrompjid);
            DataTable dtacfrompjid = new DataTable();
            adpAcfrompjid.Fill(dtacfrompjid);
            int accntidd = 0;
            if (dtacfrompjid.Rows.Count > 0)
            {
                if (dtacfrompjid.Rows[0]["Account"].ToString() != "")
                {
                    accntidd = Convert.ToInt32(dtacfrompjid.Rows[0]["Account"]);
                }
            }


            if (RadioButtonList1.SelectedIndex == 0)
            {
                a6 = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                     " ,DateTimeOfTransaction,compid,whid)" +
                     " VALUES('" + Convert.ToInt32(accntidd) + "','" + Convert.ToDecimal(txtNetAmount.Text) + "'" +
                     " ,'" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
            }
            else
            {
                a6 = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                    " ,DateTimeOfTransaction,compid,whid)" +
                    " VALUES('" + ddlcashtype.SelectedValue + "','" + Convert.ToDecimal(txtNetAmount.Text) + "'" +
                    " ,'" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";

            }

            SqlCommand cd4 = new SqlCommand(a6, con);
            cd4.CommandType = CommandType.Text;
            cd4.Transaction = transaction;
            cd4.ExecuteNonQuery();
            decimal totalInvamt = 0;
            if (rdl1.SelectedIndex == 0)
            {
                totalInvamt = Convert.ToDecimal(txtInvAmt.Text);

            }
            else
            {
                totalInvamt = Convert.ToDecimal(txtInvAmt.Text) + Convert.ToDecimal(txtTax1.Text) + Convert.ToDecimal(txtTax2.Text) + Convert.ToDecimal(txtTax3.Text);

            }

            string a7 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                                " ,DateTimeOfTransaction,compid,whid)" +
                                " VALUES('8005','" + totalInvamt + "'" +
                                " , '" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";

            SqlCommand cd5 = new SqlCommand(a7, con);
            cd5.CommandType = CommandType.Text;
            cd5.Transaction = transaction;
            cd5.ExecuteNonQuery();



            if (rdl1.SelectedIndex == 0)
            {
                string a8 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                          " ,DateTimeOfTransaction,compid,whid)" +
                          " VALUES('" + lbltaxper1.Text + "','" + Convert.ToDecimal(txtTax1.Text) + "'" +
                          "  , '" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                SqlCommand cd6 = new SqlCommand(a8, con);
                cd6.CommandType = CommandType.Text;
                cd6.Transaction = transaction;
                cd6.ExecuteNonQuery();


                string a9 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                          " ,DateTimeOfTransaction,compid,whid )" +
                          " VALUES('" + lbltaxper1.Text + "','" + Convert.ToDecimal(txtTax2.Text) + "'" +
                          "  , '" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                SqlCommand cd7 = new SqlCommand(a9, con);
                cd7.CommandType = CommandType.Text;
                cd7.Transaction = transaction;
                cd7.ExecuteNonQuery();

                string a9t = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                         " ,DateTimeOfTransaction,compid,whid )" +
                         " VALUES('" + lbltaxper3.Text + "','" + Convert.ToDecimal(txtTax3.Text) + "'" +
                         "  , '" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                SqlCommand cd7t = new SqlCommand(a9t, con);
                cd7t.CommandType = CommandType.Text;
                cd7t.Transaction = transaction;
                cd7t.ExecuteNonQuery();
            }

            string a10 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                        " ,DateTimeOfTransaction,compid,whid )" +
                        " VALUES('8004','" + Convert.ToDecimal(txtShippCharge.Text) + "'" +
                        "  , '" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
            SqlCommand cd8 = new SqlCommand(a10, con);
            cd8.CommandType = CommandType.Text;
            cd8.Transaction = transaction;
            cd8.ExecuteNonQuery();
          
            
            string accdebiinv = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                           " ,DateTimeOfTransaction,compid,whid )" +
                           " VALUES('8000','" + Convert.ToDecimal(txtNetAmount.Text) + "'" +
                           "  , '" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
            SqlCommand cdinvdeb = new SqlCommand(accdebiinv, con);
            cdinvdeb.CommandType = CommandType.Text;
            cdinvdeb.Transaction = transaction;
            cdinvdeb.ExecuteNonQuery();

            string costgood = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                      " ,DateTimeOfTransaction,compid,whid)" +
                      " VALUES('8003','" + Convert.ToDecimal(txtNetAmount.Text) + "'" +
                      " ,'" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";

            SqlCommand cdcostgood = new SqlCommand(costgood, con);
            cdcostgood.CommandType = CommandType.Text;
            cdcostgood.Transaction = transaction;
            cdcostgood.ExecuteNonQuery();


            string innpurchse = "Delete from PurchaseMaster where Purchase_Details_Id='" + Id + "'";

            SqlCommand cmpurder = new SqlCommand(innpurchse, con);

            cmpurder.Transaction = transaction;
            cmpurder.ExecuteNonQuery();


            transaction.Commit();
            con.Close();
            //string inwid = "Delete from InventoryWarehouseMasterAvgCostTbl where Tranction_Master_Id='" + Id1 + "'";

            //SqlCommand cmdinv = new SqlCommand(inwid, con);

            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}

            string maininv = "";
            int ly = 0;
            foreach (DataRow dr in dt1.Rows)
            {
                int i = 0;
                if (dr["InventoryWarehouseMasterId"].ToString() != "")
                {
                    i = Convert.ToInt32(dr["InventoryWarehouseMasterId"]);
                    //warehousefinalid = Convert.ToInt32(dr["InventoryWarehouseMasterId"]);
                }
                else
                {

                }

                decimal divtxttt1;
                decimal divtxttt2;
                decimal divtxttt3;
                decimal divshiopcharge;

                decimal invrate;

                if (Convert.ToString(txtShippCharge.Text) != "")
                {
                    divshiopcharge = ((Convert.ToDecimal(dr["Totals"]) * Convert.ToDecimal(txtShippCharge.Text)) / Convert.ToDecimal(txtInvAmt.Text));
                    divshiopcharge = Math.Round(divshiopcharge, 2);
                }
                else
                {
                    divshiopcharge = 0;

                }
                if (CheckBox2.SelectedIndex == 0)
                {
                    if (Convert.ToString(dr["Tax1"]) != "")
                    {
                        divtxttt1 = Convert.ToDecimal(dr["Tax1"]);
                        divtxttt1 = Math.Round(divtxttt1, 2);
                    }
                    else
                    {
                        divtxttt1 = 0;
                    }
                    if (Convert.ToString(dr["Tax2"]) != "")
                    {
                        divtxttt2 = Convert.ToDecimal(dr["Tax2"]);
                        divtxttt2 = Math.Round(divtxttt2, 2);
                    }
                    else
                    {
                        divtxttt2 = 0;
                    }
                    if (Convert.ToString(dr["Tax3"]) != "")
                    {
                        divtxttt3 = Convert.ToDecimal(dr["Tax3"]);
                        divtxttt3 = Math.Round(divtxttt3, 2);
                    }
                    else
                    {
                        divtxttt3 = 0;
                    }

                }
                else
                {
                    divtxttt1 = ((Convert.ToDecimal(dr["Totals"]) * Convert.ToDecimal(txtTax1.Text)) / Convert.ToDecimal(txtInvAmt.Text));
                    divtxttt2 = ((Convert.ToDecimal(dr["Totals"]) * Convert.ToDecimal(txtTax2.Text)) / Convert.ToDecimal(txtInvAmt.Text));
                    divtxttt3 = ((Convert.ToDecimal(dr["Totals"]) * Convert.ToDecimal(txtTax3.Text)) / Convert.ToDecimal(txtInvAmt.Text));

                    divtxttt1 = Math.Round(divtxttt1, 2);
                    divtxttt2 = Math.Round(divtxttt2, 2);
                    divtxttt3 = Math.Round(divtxttt3, 2);
                }

                if (rdl1.SelectedIndex == 0)
                {
                    invrate = ((Convert.ToDecimal(dr["Totals"]) + divshiopcharge) / (Convert.ToDecimal(dr["RecdQty"])));

                }
                else
                {
                    invrate = ((Convert.ToDecimal(dr["Totals"]) + divtxttt1 + divtxttt2 + divtxttt3 + divshiopcharge) / (Convert.ToDecimal(dr["RecdQty"])));

                }
                invrate = Math.Round(invrate, 2);

                string a2;
                if (dr["ExpiryDate"].ToString() != "")
                {
                    a2 = "INSERT INTO dbo.PurchaseMaster(Purchase_Details_Id,InventoryWHM_Id,InvoiceQty ,RatePerUnit " +
                             " ,BatchNo,ExpiryDate,ReceivedQTy,Tax1,Tax2,ShippingCarges,Tax3)" +
                             " VALUES ('" + Id + "' ,'" + i + "' ,'" + Convert.ToDecimal(dr["InvQty"]) + "' " +
                             "  ,'" + Convert.ToDecimal(dr["Rate"]) + "','" + dr["BatchNo"] + "','" + Convert.ToDateTime(dr["ExpiryDate"]) + "','" + Convert.ToDecimal(dr["RecdQty"]) + "','" + divtxttt1 + "','" + divtxttt2 + "','" + divshiopcharge + "','" + divtxttt3 + "')";

                }
                else
                {
                    a2 = "INSERT INTO dbo.PurchaseMaster(Purchase_Details_Id,InventoryWHM_Id,InvoiceQty ,RatePerUnit " +
                            " ,BatchNo,ReceivedQTy,Tax1,Tax2,ShippingCarges,Tax3)" +
                            " VALUES ('" + Id + "' ,'" + i + "' ,'" + Convert.ToDecimal(dr["InvQty"]) + "' " +
                            "  ,'" + Convert.ToDecimal(dr["Rate"]) + "','" + dr["BatchNo"] + "','" + Convert.ToDecimal(dr["RecdQty"]) + "','" + divtxttt1 + "','" + divtxttt2 + "','" + divshiopcharge + "','" + divtxttt3 + "')";

                }

                SqlCommand cd1 = new SqlCommand(a2, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cd1.ExecuteNonQuery();
                con.Close();
               
                if (invtype == "3" || invtype == "2")
                {
                    string updateavgcos = "Delete from  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + i.ToString() + "' and Tranction_Master_Id='" + Id1 + "'";
                    SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmavgcost.ExecuteNonQuery();
                    con.Close();
                    if (invtype == "3")
                    {
                        string upproc = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,Rate,DateUpdated,AvgCost,QtyonHand)values('" + i.ToString() + "','" + Id1 + "','" + dr["RecdQty"] + "','" + invrate + "','" + txtDate.Text + "','0','0')";

                        //  string updateavgcos = "Update  InventoryWarehouseMasterAvgCostTbl set Qty='" + dr["RecdQty"] + "',Rate='" + invrate + "',DateUpdated='" + txtDate.Text + "',AvgCost='0',QtyonHand='0' where InvWMasterId='" + i.ToString() + "' and Tranction_Master_Id='" + Id1 + "'";
                        SqlCommand cmdpro = new SqlCommand(upproc, con);

                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdpro.ExecuteNonQuery();
                        con.Close();
                    }
                }
                fillAVGCOST(Id1.ToString(), i.ToString(), invrate, dr["RecdQty"].ToString(), invtype);

                if (ly > 0)
                {
                    maininv =maininv+ ",";
                }
                else
                {
                    ly += 1;
                }

                maininv = maininv + i.ToString();
            }
            DELAVGCOST(maininv, Id1.ToString());
            // transaction.Commit();



            ClearAll();
            Label8.Visible = true;
            Label8.Text = "Record updated successfully";
            if (Request.QueryString["docid"] != null)
            {
                inserdocatt();
            }
            else
            {
                if (chkdoc.Checked == true)
                {
                    entry();
                    //  ModalPopupExtender1.Show();
                    //filldoc();
                }
            }

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Label8.Visible = true;
            Label8.Text = "Error :" + ex.Message;
        }
        finally
        {
            con.Close();
        }


        string s121b = " SELECT     EntryTypeId, Max(EntryNumber) as Number FROM  TranctionMaster " +
                          " Where EntryTypeId = 27 and Whid='" + ddlWarehouse.SelectedValue + "' Group by EntryTypeId ";
        SqlCommand cm131b = new SqlCommand(s121b, con);
        cm131b.CommandType = CommandType.Text;
        SqlDataAdapter da131b = new SqlDataAdapter(cm131b);
        DataSet ds131b = new DataSet();
        da131b.Fill(ds131b);

        if (ds131b.Tables[0].Rows.Count > 0)
        {

            int q = Convert.ToInt32(ds131b.Tables[0].Rows[0]["Number"]) + 1;
            lblEntryNo.Text = q.ToString();
        }
        else
        {
            lblEntryNo.Text = "1";
        }
        // end

    }
    protected void fillAVGCOST(string traid, string invid, decimal invrate, string recqty, string invtype)
    {
        decimal inwavgid=0;
        string id12 = invid;
        string updateavgcos = "";
        decimal OLDavgcost = 0;
        decimal oLDqtyONHAND = 0;
        decimal Totalavgcost = 0;
        decimal Newqtyonhand = 0;
        decimal Finalqtyhand = 0;
        if (invtype == "1")
        {
               DataTable Datfi = select("SELECT IWMAvgCostId  FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and Tranction_Master_Id='" + traid + "'");
               if (Datfi.Rows.Count > 0)
               {
                   inwavgid = Convert.ToDecimal(Datfi.Rows[0]["IWMAvgCostId"]);
               }
               else
               {
                  string  ABCD = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,Rate,DateUpdated,AvgCost,QtyonHand)values('" + id12 + "','" + traid + "','" + recqty + "','" + invrate + "','" + ViewState["sdo"] + "','" + Totalavgcost + "','" + Newqtyonhand + "')";
                  SqlCommand cmdadd = new SqlCommand(ABCD, con);
                
                  if (con.State.ToString() != "Open")
                  {
                      con.Open();
                  }
                  cmdadd.ExecuteNonQuery();
                  con.Close();
               }
        }
        if (invtype == "1" || invtype == "2")
        {
            DataTable drtinvdata = new DataTable();
            if (invtype == "1")
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<='" + ViewState["sdo"] + "' and Tranction_Master_Id<'" + traid + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
                if (drtinvdata.Rows.Count == 0)
                {
                    drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<'" + ViewState["sdo"] + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
        
                }
            }
            else
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<='" + ViewState["sdo"] + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
            }
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
            invrate = Math.Round(invrate, 2);
            Finalqtyhand = Convert.ToDecimal(recqty) + oLDqtyONHAND;
            if (Finalqtyhand > 0)
            {
                Totalavgcost = ((invrate * Convert.ToDecimal(recqty)) + (OLDavgcost * oLDqtyONHAND)) / Finalqtyhand;
            }
            Totalavgcost = Math.Round(Totalavgcost, 2);
            Newqtyonhand = Convert.ToDecimal(recqty) + oLDqtyONHAND;
            if (invtype == "1")
            {
                updateavgcos = "Update InventoryWarehouseMasterAvgCostTbl Set  Rate='" + invrate + "', QtyonHand='" + Newqtyonhand + "',AvgCost='" + Totalavgcost + "' where InvWMasterId='" + id12 + "' and Tranction_Master_Id='" + traid + "'";

            }
            else
            {
                updateavgcos = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,Rate,DateUpdated,AvgCost,QtyonHand)values('" + id12 + "','" + traid + "','" + recqty + "','" + invrate + "','" + ViewState["sdo"] + "','" + Totalavgcost + "','" + Newqtyonhand + "')";
            }
            SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);
            //cmavgcost.CommandType = CommandType.Text;
            //cmavgcost.Transaction = transaction;
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmavgcost.ExecuteNonQuery();
            con.Close();
        }
        string pera = "";
        if (invtype == "1")
        {
            pera = "  and DateUpdated>='" + ViewState["sdo"] + "' ";
        }
        else
        {
            pera = "  and DateUpdated>'" + ViewState["sdo"] + "'";
        }


        DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' " + pera + " order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
        decimal changeTotalavgcost = Totalavgcost;
        decimal changeTotalonhand = Newqtyonhand;
        string ABC = "";
        foreach (DataRow itm in Dataupval.Rows)
        {
            string gupd = "";
            string gupd1 = "";
            string manul = "";
            if (ABC == "")
            {
                if (invtype == "1")
                {
                    if ((Convert.ToDateTime(ViewState["sdo"]) == Convert.ToDateTime(itm["DateUpdated"])) && (Convert.ToDecimal(itm["Tranction_Master_Id"]) > Convert.ToDecimal(traid)))
                    {
                        ABC = "13";
                    }
                    else if (Convert.ToDateTime(ViewState["sdo"]) < Convert.ToDateTime(itm["DateUpdated"]))
                    {
                        ABC = "13";
                    }
                    //if (Convert.ToDecimal(itm["Tranction_Master_Id"]) >Convert.ToDecimal(traid))
                    //{
                    //    ABC = "13";
                    //}
                }
                else
                {
                    ABC = "12";
                }
            }
            if (ABC != "")
            {
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
                    manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";
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
            }

        }

    }
    protected void DELAVGCOST(string maininv,string traid)
    {
        DataTable Datfi = select("SELECT IWMAvgCostId,InvWMasterId  FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId NOT IN(" + maininv + ") and Tranction_Master_Id='" + traid + "'");
        foreach (DataRow intvay in Datfi.Rows)
        {
            string updel = "Delete from  InventoryWarehouseMasterAvgCostTbl where IWMAvgCostId='" + intvay["IWMAvgCostId"] + "'";
              SqlCommand cmdels = new SqlCommand(updel, con);

              if (con.State.ToString() != "Open")
              {
                  con.Open();
              }
              cmdels.ExecuteNonQuery();
              con.Close();
           
              decimal OLDavgcost = 0;
              decimal oLDqtyONHAND = 0;
              decimal Totalavgcost = 0;
              decimal Newqtyonhand = 0;
              DataTable drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated<='" + ViewState["sdo1"] + "' and Tranction_Master_Id<'" + traid + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
              if (drtinvdata.Rows.Count == 0)
              {
                  drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated<'" + ViewState["sdo1"] + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

              }
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
           
              Finalqtyhand =  oLDqtyONHAND;
              Totalavgcost = OLDavgcost;
              Totalavgcost = Math.Round(Totalavgcost, 2);
              Newqtyonhand =  oLDqtyONHAND;

              DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated>='" + ViewState["sdo1"] + "' order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
              decimal changeTotalavgcost = Totalavgcost;
              decimal changeTotalonhand = Newqtyonhand;
                string ABC = "";
                foreach (DataRow itm in Dataupval.Rows)
                {
                    string gupd = "";
                    string gupd1 = "";
                    string manul = "";

                    if (ABC == "")
                    {
                        if (Convert.ToDateTime(itm["DateUpdated"]) == Convert.ToDateTime(ViewState["sdo1"]))
                        {
                            if (Convert.ToDecimal(itm["Tranction_Master_Id"]) > Convert.ToDecimal(traid))
                            {
                                ABC = "13";
                            }
                        }
                        else
                        {
                            ABC = "12";
                        }
                    }
                    if (ABC != "")
                    {




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
                            manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";
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
    public void inserdocatt()
    {

        string sqlselect = "select * from DocumentMaster where DocumentId='" + Request.QueryString["docid"] + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(sqlselect, con);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);
        if (dtpt.Rows.Count > 0)
        {

            SqlCommand cmdi = new SqlCommand("InsertAttachmentMaster", con);

            cmdi.CommandType = CommandType.StoredProcedure;
            cmdi.Parameters.Add(new SqlParameter("@Titlename", SqlDbType.NVarChar));
            cmdi.Parameters["@Titlename"].Value = dtpt.Rows[0]["DocumentTitle"].ToString();
            cmdi.Parameters.Add(new SqlParameter("@Filename", SqlDbType.NVarChar));
            cmdi.Parameters["@Filename"].Value = dtpt.Rows[0]["DocumentName"].ToString();

            cmdi.Parameters.Add(new SqlParameter("@Datetime", SqlDbType.DateTime));
            cmdi.Parameters["@Datetime"].Value = dtpt.Rows[0]["DocumentUploadDate"].ToString(); ;
            cmdi.Parameters.Add(new SqlParameter("@RelatedtablemasterId", SqlDbType.NVarChar));
            cmdi.Parameters["@RelatedtablemasterId"].Value = "5";
            cmdi.Parameters.Add(new SqlParameter("@RelatedTableId", SqlDbType.NVarChar));
            cmdi.Parameters["@RelatedTableId"].Value = ViewState["tid"].ToString();
            cmdi.Parameters.Add(new SqlParameter("@IfilecabinetDocId", SqlDbType.NVarChar));
            cmdi.Parameters["@IfilecabinetDocId"].Value = dtpt.Rows[0]["DocumentId"].ToString();
            cmdi.Parameters.Add(new SqlParameter("@Attachment", SqlDbType.Int));
            cmdi.Parameters["@Attachment"].Direction = ParameterDirection.Output;

            cmdi.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
            cmdi.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            Int32 result = cmdi.ExecuteNonQuery();
            result = Convert.ToInt32(cmdi.Parameters["@Attachment"].Value);
            con.Close();
        }
    }



    protected void btnCalCase_Click(object sender, EventArgs e)
    {
        decimal invqty = 0;
        if (txtFreeCases.Text == "")
        {
            txtFreeCases.Text = "0";
        }
        if (txtRecNoCase.Text == "")
        {
            txtRecNoCase.Text = "0";
        }
        if (ckhq.Checked == true)
        {
            txtRecNoCase.Text = txtInvNoCase.Text;
        }

        decimal recievconcal = Convert.ToDecimal(txtFreeCases.Text) + Convert.ToDecimal(txtInvNoCase.Text);
        if (recievconcal >= Convert.ToDecimal(txtRecNoCase.Text))
        {
            lblmmssgg.Text = "";
            if (txtFreeCases.Text.Length > 0)
            {
                invqty = Convert.ToDecimal(Convert.ToDecimal(txtInvNoCase.Text) + Convert.ToDecimal(txtFreeCases.Text)) * Convert.ToDecimal(txtUnitPrCase.Text);
            }
            else
            {
                invqty = Convert.ToInt32(txtInvNoCase.Text) * Convert.ToDecimal(txtUnitPrCase.Text);
            }
            double rate = (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(txtInvNoCase.Text) * Convert.ToDouble(txtPricePrCase.Text))) / (Convert.ToDouble(recievconcal) * Convert.ToDouble(txtUnitPrCase.Text)));

            txtInvQty.Text = Math.Round(invqty, 2).ToString();

            // txtInvQty.Text = invqty.ToString();
            txtRate11.Text = Math.Round(rate, 2).ToString();

            if (txtRecNoCase.Text != "")
            {
                Decimal recqty = Convert.ToDecimal(txtRecNoCase.Text) * Convert.ToDecimal(txtUnitPrCase.Text);
                //txtRecdQty.Text = recqty.ToString();
                txtRecdQty.Text = Math.Round(recqty, 2).ToString();
                txtTotals.Text = Convert.ToString(Convert.ToDouble(txtRecdQty.Text) * Convert.ToDouble(txtRate11.Text));

                txtTotals.Text = Math.Round(Convert.ToDecimal(txtTotals.Text), 2).ToString();
            }

        }
        else
        {
            lblmmssgg.Visible = true;
            lblmmssgg.Text = "Please note that you are unable to submit this form if you have received an excess quantity than is stated on the invoice, plus the free goods, as per the invoice.";
        }
        txtBatch.Focus();
    }

    public void getProductNo()
    {
        string str = "SELECT  " +
                    " Case when( InventoryMaster.ProductNo IS NULL) then '-' else InventoryMaster.ProductNo End+ ' : ' + InventoryMaster.Name + ' : ' + InventoryMaster.ProductNo+ ' : ' + cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+ ' : ' + UnitTypeMaster.Name AS Categary, InventoryMaster.InventoryMasterId," +
     " InventoryMaster.Name, InventoryWarehouseMasterTbl.Active, " +
                    " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, " +
       "InventoryWarehouseMasterTbl.WareHouseId, InventoryMaster.MasterActiveStatus " +
                    "FROM         InventoryMaster INNER JOIN " +
                    "InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId " +
                    " =InventoryWarehouseMasterTbl.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId Inner Join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId" +
                    " WHERE  InventoryMaster.CatType IS NULL and   " +
  "   (InventoryMaster.MasterActiveStatus = 1) and WareHouseMaster.WareHouseId='" + ddlWarehouse.SelectedValue + "'  order BY Categary";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        ddlProductNo.DataSource = ds;
        ddlProductNo.DataTextField = "Categary";
        ddlProductNo.DataValueField = "InventoryWarehouseMasterId";
        ddlProductNo.DataBind();
        ddlProductNo.Items.Insert(0, "-Select-");
        ddlProductNo.Items[0].Value = "0";
        ddlProductNo.SelectedIndex = 0;

        //DataTable dt = new DataTable();

        //DataColumn dtcom1 = new DataColumn();
        //dtcom1.DataType = System.Type.GetType("System.String");
        //dtcom1.ColumnName = "productno";
        //dtcom1.ReadOnly = false;
        //dtcom1.Unique = false;
        //dtcom1.AllowDBNull = true;

        //dt.Columns.Add(dtcom1);
        //DataColumn dtcom2 = new DataColumn();
        //dtcom2.DataType = System.Type.GetType("System.String");
        //dtcom2.ColumnName = "invwhid";
        //dtcom2.ReadOnly = false;
        //dtcom2.Unique = false;
        //dtcom2.AllowDBNull = true;

        //dt.Columns.Add(dtcom2);

        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
        //    DataRow dtrow = dt.NewRow();
        //    string strData = dr["ProductNo"].ToString();

        //    char[] separator = new char[] { '_' };

        //    string[] strSplitArr = strData.Split(separator);
        //    int i = Convert.ToInt32(strSplitArr.Length);
        //    if (i >= 2)
        //    {
        //        string itemno = strSplitArr[i - 2].ToString();
        //        int len = itemno.Length;
        //        int j = len - 5;
        //        string no = itemno.Substring(j);
        //        dtrow["productno"] = no.ToString() + ":" + dr["Name"];
        //        dtrow["invwhid"] = dr["InventoryWarehouseMasterId"].ToString();
        //        warehousefinalid =Convert.ToInt32(dr["InventoryWarehouseMasterId"].ToString());
        //       //  Session["warehouseid1"] = warehousefinalid.ToString();
        //        dt.Rows.Add(dtrow);
        //    }
        //    else
        //    {
        //        dtrow["productno"] = dr["ProductNo"].ToString() + ":" + dr["Name"];
        //        dtrow["invwhid"] = dr["InventoryWarehouseMasterId"].ToString();
        //      //  Session["warehouseid1"] = Convert.ToInt32(dr["InventoryWarehouseMasterId"].ToString());
        //        dt.Rows.Add(dtrow);
        //    }
        //}
        //DataView dv = dt.DefaultView;
        //dv.Sort = "productno asc";
        //ddlProductNo.DataSource = dv;
        //ddlProductNo.DataTextField = "productno";
        //ddlProductNo.DataValueField = "invwhid";
        //ddlProductNo.DataBind();
        //ddlProductNo.Items.Insert(0, "-Select-");
        //ddlProductNo.Items[0].Value = "0";
        //ddlProductNo.SelectedIndex = 0;
        //ddlProductNo.SelectedItem.Value = "0";

        //}
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void ddlItemname_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void ddlProductNo_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void btnUpdateBarcode_Click(object sender, EventArgs e)
    {
        if (txtBar.Text.Length > 0)
        {
            string str = " SELECT     InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName + ' : ' + InventoruSubSubCategory.InventorySubSubName  " +
                                  " + ' : ' + InventoryMaster.Name AS Category, InventoryCategoryMaster.InventoryCatName, InventoryMaster.Name, InventoryMaster.InventoryMasterId,   " +
                                 " InventoryBarcodeMaster.Barcode  " +
                                 " FROM         InventoryMaster INNER JOIN  " +
                                 " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN  " +
                                 " InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN  " +
                                 " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN " +
                                 " InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id " +
                                 " Where InventoryBarcodeMaster.Barcode = '" + txtBar.Text + "' and InventoryMaster.MasterActiveStatus=1 ";


            SqlCommand cm = new SqlCommand(str, con);
            cm.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string s;
            s = ds.Tables[0].Rows[0]["InventoryWarehouseMasterId"].ToString();
            hdnBarcode.Value = s.ToString();

            //   Response.Redirect("")
        }
        else
        {
            Label2.Text = "Please enter BarCode.";
        }
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        filltrans();
        fillpartyddl();

        RadioButtonList2_SelectedIndexChanged(sender, e);

    }
    protected void ClearAll()
    {
        //rdl1.Enabled = true;
        pnlitem.Visible = false;
        pnlmain.Visible = false;
        rdl1.SelectedIndex = -1;
        CheckBox2.SelectedIndex = -1;
        txtBar.Text = "";
        txtBatch.Text = "";
        //txtDate.Text = "";
        txtExDate.Text = "";
        txtInvAmt.Text = "0";
        txtInvNo.Text = "0";
        txtInvNoCase.Text = "";
        txtInvQty.Text = "";
        txtNetAmount.Text = "0";
        txtPricePrCase.Text = "";
        txtRate11.Text = "";
        txtRecdQty.Text = "";
        txtRecNoCase.Text = "";
        txtShippCharge.Text = "0";
        txtShort.Text = "0";
        txtTax1.Text = "0";
        txtTax2.Text = "0";
        txtTax3.Text = "0";
        txtTrackingNo.Text = "0";
        txtUnitPrCase.Text = "";
        txtValurRecd.Text = "0";
        //lblEntryNo.Text = "";
        lblInvName.Text = "";
        lblInvname1.Text = "";
        lblmmssgg.Text = "";

        ddlCategory.SelectedIndex = 0;
        ddlItemname.SelectedIndex = 0;
        //ddlpartyName.SelectedIndex = 0;
        ddlProductNo.SelectedIndex = 0;
        if (ddlTransporter.SelectedIndex >= 0)
        {
            ddlTransporter.SelectedIndex = 0;
        }

        //ddlWarehouse.SelectedIndex = 0;

        GridView1.DataSource = null;
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            //chk.Enabled = false;
            CheckBox2.Enabled = false;
        }
        else
        {
            //chk.Enabled = true;
            CheckBox2.Enabled = true;
        }
        ViewState["dt"] = null;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "remove")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["dt"];
            dt.Rows.Remove(dt.Rows[GridView1.SelectedIndex]);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            ViewState["dt"] = dt;
        }
    }

    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 1)
        {
            string cashtype = "select AccountName,AccountId from AccountMaster where GroupId=1 and ClassId=1 and compid='" + Session["comid"] + "' and Whid='" + ddlWarehouse.SelectedValue + "' order by AccountName";
            SqlDataAdapter d1 = new SqlDataAdapter(cashtype, con);
            DataSet d12 = new DataSet();
            d1.Fill(d12);
            ddlcashtype.DataSource = d12;
            ddlcashtype.DataTextField = "AccountName";
            ddlcashtype.DataValueField = "AccountId";
            ddlcashtype.Items.Insert(0, "-Select-");
            ddlcashtype.SelectedItem.Value = "0";
            ddlcashtype.DataBind();
            pnlcash.Visible = true;

        }
        else
        {


            pnlcash.Visible = false;

        }
    }


    public void entry()
    {
        String te = "AccEntryDocUp.aspx?Tid=" + ViewState["tid"];


        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void fillpartyddl()
    {
        //string s12 = " SELECT     Party_master.PartyID, Party_master.Account, Party_master.Contactperson+':'+Party_master.Compname as Compname, Party_master.PartyTypeId, User_master.Active " +
        string s12 = " SELECT     Party_master.PartyID, Party_master.Account,Party_master.Compname+':'+Party_master.Contactperson  as Compname, Party_master.PartyTypeId, User_master.Active " +
     " FROM     [PartytTypeMaster] inner join    Party_master on Party_master.PartyTypeId= [PartytTypeMaster].[PartyTypeId] INNER JOIN " +
     "                  User_master ON Party_master.PartyID = User_master.PartyID " +
    " WHERE     (User_master.Active = 1) and [PartytTypeMaster].compid='" + Session["comid"] + "' and Party_master.Whid='" + ddlWarehouse.SelectedValue + "' and [PartType]='Vendor' order by Compname "; // + //" Where PartytypeId = 1 and Compname <> 'yyyyy'";
        SqlCommand cm13 = new SqlCommand(s12, con);
        cm13.CommandType = CommandType.Text;
        SqlDataAdapter da13 = new SqlDataAdapter(cm13);
        DataSet ds13 = new DataSet();
        da13.Fill(ds13);


        ddlpartyName.DataSource = ds13;
        ddlpartyName.DataTextField = "Compname";
        ddlpartyName.DataValueField = "PartyID";

        ddlpartyName.DataBind();
        ddlpartyName.Items.Insert(0, "-Select-");

        ddlpartyName.Items[0].Value = "0";




    }

    protected void filltrans()
    {
        ddlTransporter.Items.Clear();
        //string s12 = " SELECT     Party_master.PartyID, Party_master.Account, Party_master.Contactperson+':'+Party_master.Compname as Compname, Party_master.PartyTypeId, User_master.Active " +
        string s12 = " SELECT     Party_master.PartyID, Party_master.Account,Party_master.Compname+':'+Party_master.Contactperson  as Compname, Party_master.PartyTypeId, User_master.Active " +
     " FROM     [PartytTypeMaster] inner join    Party_master on Party_master.PartyTypeId= [PartytTypeMaster].[PartyTypeId] INNER JOIN " +
     "                  User_master ON Party_master.PartyID = User_master.PartyID " +
    " WHERE     (User_master.Active = 1) and [PartytTypeMaster].compid='" + Session["comid"] + "' and Party_master.Whid='" + ddlWarehouse.SelectedValue + "' and [PartType]='Vendor' order by Compname "; // + //" Where PartytypeId = 1 and Compname <> 'yyyyy'";
        SqlCommand cm13 = new SqlCommand(s12, con);
        cm13.CommandType = CommandType.Text;
        SqlDataAdapter da13 = new SqlDataAdapter(cm13);
        DataTable ds13 = new DataTable();
        da13.Fill(ds13);
        if (ds13.Rows.Count > 0)
        {



            ddlTransporter.DataSource = ds13;
            ddlTransporter.DataTextField = "Compname";
            ddlTransporter.DataValueField = "PartyID";

            ddlTransporter.DataBind();
        }
        ddlTransporter.Items.Insert(0, "Select");
        ddlTransporter.Items[0].Value = "0";

    }

    protected void CheckBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBox2.SelectedIndex == 0)
        {
            //lblt1.Visible = true;
            //lblt2.Visible = true;
            //lblt3.Visible = true;
            //txtt1.Visible = true;
            //txtt2.Visible = true;
            //txtt3.Visible = true;

            txtBatch.Width = 50;

            txtInvNoCase.Width = 50;
            txtFreeCases.Width = 50;
            txtUnitPrCase.Width = 50;
            txtPricePrCase.Width = 50;
            txtRecNoCase.Width = 50;

            txtTax1.Enabled = false;
            txtTax2.Enabled = false;
            txtTax3.Enabled = false;
            txtt1.Text = "0";
            txtt2.Text = "0";
            txtt3.Text = "0";

            if (lbltxtop.Text == "0")
            {
                pnltsh.Visible = false;
                ModalPopupExtender12222.Show();
            }
            else
            {
                pnltsh.Visible = true;
            }
        }
        else
        {

            lblt1.Visible = false;
            lblt3.Visible = false;
            lblt2.Visible = false;
            txtt1.Visible = false;
            txtt2.Visible = false;
            txtt3.Visible = false;
            txtInvNoCase.Width = 50;
            txtFreeCases.Width = 50;
            txtUnitPrCase.Width = 50;
            txtPricePrCase.Width = 50;
            txtRecNoCase.Width = 50;
            txtBatch.Width = 50;
            txtTax1.Enabled = true;
            txtTax2.Enabled = true;
            txtTax3.Enabled = true;
            pnltsh.Visible = false;
        }
        pnlmain.Visible = true;
    }

    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "remove")
        //{
        //    GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        //    DataTable dt = new DataTable();
        //    dt = (DataTable)ViewState["dt"];
        //    dt.Rows.Remove(dt.Rows[GridView1.SelectedIndex]);

        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //    ViewState["dt"] = dt;
        //}
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //  GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["dt"];
        //  dt.Rows.Remove(dt.Rows[GridView1.SelectedIndex]);
        dt.Rows.Remove(dt.Rows[e.RowIndex]);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        ViewState["dt"] = dt;
        //btCal_Click(sender, e);
        recal();
    }


    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;


    }
    public void gridtax()
    {
        pnltax1.Visible = false;
        pnltax2.Visible = false;
        pnltax3.Visible = false;
        lblt1.Visible = false;
        txtt1.Visible = false;
        lblt2.Visible = false;
        txtt2.Visible = false;
        lblt3.Visible = false;
        txtt3.Visible = false;
        string PurId = "";
        if (lbltaxper1.Text != "0" && lbltaxper3.Text != "0" && lbltaxper3.Text != "0")
        {
            PurId = "  and PurchaseTaxAccountMasterID In('" + lbltaxper1.Text + "','" + lbltaxper2.Text + "','" + lbltaxper3.Text + "')";
        }
        else if (lbltaxper1.Text != "0" && lbltaxper2.Text != "0")
        {
            PurId = "  and PurchaseTaxAccountMasterID In('" + lbltaxper1.Text + "','" + lbltaxper2.Text + "')";

        }
        else if (lbltaxper1.Text != "0")
        {
            PurId = "   `  and PurchaseTaxAccountMasterID In('" + lbltaxper1.Text + "')";

        }

        if (PurId != "")
        {
            if (Convert.ToString(lbltxtop.Text) == "3")
            {

                DataTable dtwhid = select("select Taxname,Taxshortname,PurchaseTaxAccountMasterID,[SalesTaxOption3TaxNameTbl].[Taxname],[SalesTaxOption3TaxNameTbl].[StateId] from  [SalesTaxOption3TaxNameTbl]  where [Whid]='" + ddlWarehouse.SelectedValue + "' " + PurId + "   SalesTaxOption3TaxNameTbl.Id Desc");

                if (dtwhid.Rows.Count > 0)
                {

                    if (dtwhid.Rows.Count == 3)
                    {
                        ViewState["Taxallo"] = "3";
                        if (CheckBox2.SelectedIndex == 0)
                        {
                            lblt1.Visible = true;
                            txtt1.Visible = true;
                            lblt2.Visible = true;
                            txtt2.Visible = true;
                            lblt3.Visible = true;
                            txtt3.Visible = true;
                        }
                        lblt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        lblt2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";

                        lblt3.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " Tax";
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["Taxname"]) + ")";
                        txttinfo2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[1]["Taxname"]) + ")";
                        txttinfo3.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " =(" + Convert.ToString(dtwhid.Rows[2]["Taxname"]) + ")";
                        Label23.Text = lblt1.Text;
                        Label24.Text = lblt2.Text;
                        Label60.Text = lblt3.Text;
                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        lbltaxper2.Text = Convert.ToString(dtwhid.Rows[1]["PurchaseTaxAccountMasterID"]);
                        lbltaxper3.Text = Convert.ToString(dtwhid.Rows[2]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                        pnltax2.Visible = true;
                        pnltax3.Visible = true;
                        txtt1.Width = 40;
                        txtt2.Width = 40;
                        txtt3.Width = 40;
                    }
                    else if (dtwhid.Rows.Count == 2)
                    {
                        ViewState["Taxallo"] = "2";
                        if (CheckBox2.SelectedIndex == 0)
                        {
                            lblt1.Visible = true;
                            txtt1.Visible = true;
                            lblt2.Visible = true;
                            txtt2.Visible = true;
                        }
                        lblt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        lblt2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";
                        Label23.Text = lblt1.Text;
                        Label24.Text = lblt2.Text;
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["Taxname"]) + ")";
                        txttinfo2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[1]["Taxname"]) + ")";
                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        lbltaxper2.Text = Convert.ToString(dtwhid.Rows[1]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                        pnltax2.Visible = true;
                        txtt1.Width = 40;
                        txtt2.Width = 40;

                    }
                    else if (dtwhid.Rows.Count == 1)
                    {
                        ViewState["Taxallo"] = "1";
                        if (CheckBox2.SelectedIndex == 0)
                        {
                            lblt1.Visible = true;
                            txtt1.Visible = true;
                        }
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["Taxname"]) + ")";
                        lblt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        Label23.Text = lblt1.Text;
                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                        txtt1.Width = 40;

                    }
                }

                else
                {
                    ViewState["Taxallo"] = "0";
                }



            }
            else if (Convert.ToString(lbltxtop.Text) == "2")
            {


                DataTable dtwhid = select("select Taxshortname,PurchaseTaxAccountMasterID,TaxName from [TaxTypeMasterMoreInfo]  where Active='1' and [Whid]='" + ddlWarehouse.SelectedValue + "' " + PurId + "    order by TaxTypeMasterMoreInfo.Id Desc");

                if (dtwhid.Rows.Count > 0)
                {

                    lbltxtop.Text = "2";
                    if (dtwhid.Rows.Count == 3)
                    {
                        ViewState["Taxallo"] = "3";
                        if (CheckBox2.SelectedIndex == 0)
                        {
                            lblt1.Visible = true;
                            txtt1.Visible = true;
                            lblt2.Visible = true;
                            txtt2.Visible = true;
                            lblt3.Visible = true;
                            txtt3.Visible = true;
                        }
                        lblt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        lblt2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";

                        lblt3.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " Tax";
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["TaxName"]) + ")";
                        txttinfo2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[1]["TaxName"]) + ")";
                        txttinfo3.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[2]["TaxName"]) + ")";
                        Label23.Text = lblt1.Text;
                        Label24.Text = lblt2.Text;
                        Label60.Text = lblt3.Text;
                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        lbltaxper2.Text = Convert.ToString(dtwhid.Rows[1]["PurchaseTaxAccountMasterID"]);
                        lbltaxper3.Text = Convert.ToString(dtwhid.Rows[2]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                        pnltax2.Visible = true;
                        pnltax3.Visible = true;
                        txtt1.Width = 40;
                        txtt2.Width = 40;
                        txtt3.Width = 40;
                    }
                    else if (dtwhid.Rows.Count == 2)
                    {
                        ViewState["Taxallo"] = "2";
                        if (CheckBox2.SelectedIndex == 0)
                        {
                            lblt1.Visible = true;
                            txtt1.Visible = true;
                            lblt2.Visible = true;
                            txtt2.Visible = true;
                        }
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["TaxName"]) + ")";
                        txttinfo2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[1]["TaxName"]) + ")";
                        lblt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        lblt2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";
                        Label23.Text = lblt1.Text;
                        Label24.Text = lblt2.Text;
                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        lbltaxper2.Text = Convert.ToString(dtwhid.Rows[1]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                        pnltax2.Visible = true;
                        txtt1.Width = 40;
                        txtt2.Width = 40;

                    }
                    else if (dtwhid.Rows.Count == 1)
                    {
                        ViewState["Taxallo"] = "1";
                        if (CheckBox2.SelectedIndex == 0)
                        {
                            lblt1.Visible = true;
                            txtt1.Visible = true;
                        }
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["TaxName"]) + ")";
                        lblt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        Label23.Text = lblt1.Text;
                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                        txtt1.Width = 40;

                    }

                }
                else
                {
                    ViewState["Taxallo"] = "0";
                }

            }
            else if (Convert.ToString(lbltxtop.Text) == "3")
            {

                DataTable dtwhid = select("select Top(3) Taxshortname,Name,PurchaseTaxAccountMasterID from [TaxTypeMaster]  where Active='1' and [Whid]='" + ddlWarehouse.SelectedValue + "' " + PurId + "   order by TaxTypeMaster.TaxTypeMasterId Desc");

                if (dtwhid.Rows.Count > 0)
                {

                    if (dtwhid.Rows.Count == 3)
                    {
                        lbltxtop.Text = "1";
                        ViewState["Taxallo"] = "3";
                        if (CheckBox2.SelectedIndex == 0)
                        {
                            lblt1.Visible = true;
                            txtt1.Visible = true;
                            lblt2.Visible = true;
                            txtt2.Visible = true;
                            lblt3.Visible = true;
                            txtt3.Visible = true;
                        }
                        lblt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        lblt2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";

                        lblt3.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " Tax";
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["Name"]) + ")";
                        txttinfo2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[1]["Name"]) + ")";
                        txttinfo3.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[2]["Name"]) + ")";
                        Label23.Text = lblt1.Text;
                        Label24.Text = lblt2.Text;
                        Label60.Text = lblt3.Text;
                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        lbltaxper2.Text = Convert.ToString(dtwhid.Rows[1]["PurchaseTaxAccountMasterID"]);
                        lbltaxper3.Text = Convert.ToString(dtwhid.Rows[2]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                        pnltax2.Visible = true;
                        pnltax3.Visible = true;
                        txtt1.Width = 40;
                        txtt2.Width = 40;
                        txtt3.Width = 40;
                    }
                    else if (dtwhid.Rows.Count == 2)
                    {
                        ViewState["Taxallo"] = "2";
                        if (CheckBox2.SelectedIndex == 0)
                        {
                            lblt1.Visible = true;
                            txtt1.Visible = true;
                            lblt2.Visible = true;
                            txtt2.Visible = true;
                        }
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["Name"]) + ")";
                        txttinfo2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[1]["Name"]) + ")";
                        lblt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        lblt2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";
                        Label23.Text = lblt1.Text;
                        Label24.Text = lblt2.Text;
                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        lbltaxper2.Text = Convert.ToString(dtwhid.Rows[1]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                        pnltax2.Visible = true;
                        txtt1.Width = 40;
                        txtt2.Width = 40;

                    }
                    else if (dtwhid.Rows.Count == 1)
                    {
                        ViewState["Taxallo"] = "1";
                        if (CheckBox2.SelectedIndex == 0)
                        {
                            lblt1.Visible = true;
                            txtt1.Visible = true;
                        }
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["Name"]) + ")";
                        lblt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        Label23.Text = lblt1.Text;
                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                        txtt1.Width = 40;


                    }

                }
                else
                {
                    ViewState["Taxallo"] = "0";
                }
            }

        }
    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        chkfreeq.Checked = false;
        pnlfreeq.Visible = false;
        ckhq.Checked = true;
        pnlquarrectqua.Visible = false;
        if (RadioButtonList2.SelectedIndex == 0)
        {
            if (ddlCategory.SelectedIndex < 0)
            {
                hdnCat.Value = "0";
                InventoryWarehouseMasterId = 0;

            }
            else
            {
                hdnCat.Value = ddlCategory.SelectedItem.Value.ToString();

                InventoryWarehouseMasterId = Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString());


                Session["warehousmasterid"] = InventoryWarehouseMasterId.ToString();

                Panel2.Visible = true;
            }

            btCal.Visible = true;
        }
        else if (RadioButtonList2.SelectedIndex == 1)
        {
            btCal.Visible = true;

            if (ddlItemname.SelectedIndex <= 0)
            {
                hdnNm.Value = "0";
                InventoryWarehouseMasterIdN = 0;

            }
            else
            {
                hdnNm.Value = ddlItemname.SelectedItem.Value.ToString();

                InventoryWarehouseMasterIdN = Convert.ToInt32(ddlItemname.SelectedItem.Value.ToString());
                Session["warehousmasterid"] = InventoryWarehouseMasterIdP;
                Panel1.Visible = true;
            }
        }
        else if (RadioButtonList2.SelectedIndex == 2)
        {

            if (txtBar.Text != "")
            {
                if (ddlWarehouse.SelectedIndex >= 0)
                {
                    Session["warehouseid1"] = Convert.ToInt32(ddlWarehouse.SelectedItem.Value.ToString());
                    string str = " SELECT     InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName + ' : ' + InventoruSubSubCategory.InventorySubSubName + ' : ' + InventoryMaster.Name AS Category, " +
                        " InventoryCategoryMaster.InventoryCatName,Case when( InventoryMaster.ProductNo IS NULL) then '-' else InventoryMaster.ProductNo End as ProductNo, InventoryMaster.Name, InventoryMaster.InventoryMasterId, " +
                         "  InventoryBarcodeMaster.Barcode, InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.WareHouseId, " +
                         "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                        " FROM         InventoryMaster INNER JOIN " +
                         "  InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                         "  InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                         "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN " +
                         "  InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id INNER JOIN " +
                         "  InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId " +
                         " WHERE   InventoryMaster.CatType IS NULL and    (InventoryBarcodeMaster.Barcode = '" + txtBar.Text + "') and InventoryMaster.MasterActiveStatus=1 AND (InventoryWarehouseMasterTbl.Active = 1) " +
                         " and (InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "') ";


                    SqlCommand cm = new SqlCommand(str, con);
                    cm.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["warehousmasterid"] = ds.Tables[0].Rows[0]["InventoryWarehouseMasterId"].ToString();
                        Panel2.Visible = true;
                    }
                    else
                    {
                        Label2.Text = "Invalid Barcode.";
                        Label2.Visible = true;
                        return;
                    }
                }
            }
            else
            {
                Label2.Text = "Please enter Barcode.";
                Label2.Visible = true;
                return;
            }

        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            btCal.Visible = true;

            if (ddlProductNo.SelectedIndex == 0)
            {
                hdnPNo.Value = "0";
                InventoryWarehouseMasterIdP = 0;

            }
            else
            {
                hdnPNo.Value = ddlProductNo.SelectedItem.Value.ToString();

                InventoryWarehouseMasterIdP = Convert.ToInt32(ddlProductNo.SelectedItem.Value.ToString());
                Session["warehousmasterid"] = InventoryWarehouseMasterIdP;
                Panel2.Visible = true;
            }
        }
        if (CheckBox2.SelectedIndex == 0)
        {
            gridtax();
        }
    }

    protected void btnyes_Click(object sender, EventArgs e)
    {
        String te = "StoreTaxmethodtbl.aspx?Wid=" + ddlWarehouse.SelectedValue;


        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgadddivision_Click(object sender, ImageClickEventArgs e)
    {
        string te = "VendorPartyRegister.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButton3_Click(object sender, ImageClickEventArgs e)
    {

        fillpartyddl();

    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        filltrans();
    }
    protected void ckhq_CheckedChanged(object sender, EventArgs e)
    {
        if (ckhq.Checked == true)
        {
            pnlquarrectqua.Visible = false;
        }
        else
        {
            pnlquarrectqua.Visible = true;
        }
    }
    protected void chkfreeq_CheckedChanged(object sender, EventArgs e)
    {
        if (chkfreeq.Checked == true)
        {
            pnlfreeq.Visible = true;
            //lblrnocase.Text = "Received No. of cases";
            // lblrqty.Text = "Received Qty";
        }
        else
        {
            txtFreeCases.Text = "0";
            pnlfreeq.Visible = false;
        }
    }





    protected void lnkmore_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void btnnewinv_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnk_Click(object sender, EventArgs e)
    {

        rdl1.ToolTip = "If you wish to make changes in the selection of this option, you must refresh this page from the top right corner. Please note, all information that you added will be deleted. You will have to re-enter this information again by hitting the create new invoice button. ";
        rdl1.Enabled = false;
        if (rdl1.SelectedIndex == 0)
        {
            pnlitem.Visible = true;
            pnlmain.Visible = false;
        }
        else
        {
            CheckBox2.SelectedIndex = 1;
            pnlitem.Visible = false;
            pnlmain.Visible = true;
        }
        ModalPopupExtender2.Hide();
    }
    protected void btnccc_Click(object sender, EventArgs e)
    {
        rdl1.SelectedIndex = -1;
    }
    protected void rdl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
    protected void butonupd_Click(object sender, EventArgs e)
    {

    }

}
