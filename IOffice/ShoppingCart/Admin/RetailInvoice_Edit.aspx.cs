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
using System.Text;
using System.Net;



public partial class RetailInvoice_Edit : System.Web.UI.Page
{

    protected string salesorderid = "";
    protected string name = "";
    protected string address = "";
    protected string state = "";
    protected string city = "";
    protected string zip = "";
    protected string phone = "";
    public static string prodService = "";
    //int classid = 0;
    // String qryStr;
    //  int groupid = 0;
    //  string accid = "";
    string page = "";
    SqlConnection con = new SqlConnection(PageConn.connnn);


    DBCommands1 dbss1 = new DBCommands1();
    // double finaltax = 0.00;
    double totalpartydis = 0.00;
    double totalpromtionaldis = 0.00;
    double totalvolumedis = 0.00;
    object com;

    protected void Page_Load(object sender, EventArgs e)
    {

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        page = strSplitArr[i - 1].ToString();

        Page.Title = pg.getPageTitle(page);

        Label1.Visible = false;
        //lblmsg1.Visible = false;
        lblqty.Visible = false;
        btnPrintCustomer.Visible = false;
        if (ViewState["dt"] == null)
        {
            //btnUpdate.Visible = false;
            //lblMsg.Visible = false;
        }
        ddlCash.Enabled = true;
        //pnlCash.Visible = true;
        ddlParty.Enabled = true;
        com = Session["comid"].ToString();
        if (!IsPostBack)
        {

            //int salemanid = 1;
            ddlCash.Enabled = true;
            ddlParty.Enabled = true;



            ViewState["dt"] = null;

            //   ddlCash.Items.Insert(0, "-select-");

            salesorderid = "0";

            SqlCommand cmd3 = new SqlCommand("SELECT CountryId, CountryName FROM  CountryMaster", con);
            SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);
            DataSet ds3 = new DataSet();
            adp3.Fill(ds3);
            ddlcountry.DataSource = ds3;
            ddlcountry.DataTextField = "CountryName";
            ddlcountry.DataValueField = "CountryId";
            ddlcountry.DataBind();
            ddlcountry.Items.Insert(0, "-Select-");
            ddlcountryCR.DataSource = ds3;
            ddlcountryCR.DataTextField = "CountryName";
            ddlcountryCR.DataValueField = "CountryId";
            ddlcountryCR.DataBind();
            ddlcountryCR.Items.Insert(0, "-Select-");
            ddlcountryCR.Items[0].Value = "0";
            ddlstateCR.Items.Insert(0, "-Select-");
            ddlstateCR.Items[0].Value = "0";
            ddlcityCR.Items.Insert(0, "-Select-");
            ddlcityCR.Items[0].Value = "0";
            //  rbt_pay_method.SelectedIndex = 2;
            // pnlCash.Visible = false;
            pnlCredit.Visible = false;
            Panel1.Visible = false;
            //pnlCash.Visible = true;




            txtpayduedate.Text = System.DateTime.Now.ToShortDateString();
       
            txtGoodsDate.Text = System.DateTime.Now.ToShortDateString();
            //    lblCustDisc.Text = "0.00";
            lblGTotal.Text = "0.00";


            DataTable dtwh = ClsStore.SelectStorename();
            if (dtwh.Rows.Count > 0)
            {
                ddlWarehouse.DataSource = dtwh;
                ddlWarehouse.DataValueField = "WareHouseId";
                ddlWarehouse.DataTextField = "Name";

                ddlWarehouse.DataBind();

            }

            if (Request.QueryString["docid"] != null)
            {

                string sssx11 = "SELECT WarehouseMaster.WarehouseId, DocumentMainType.DocumentMainType + '/'+DocumentSubType.DocumentSubType +'/'+ DocumentType.DocumentType as DocumentType,DocumentDate,DocumentTitle,DocumentId FROM WarehouseMaster inner join DocumentMainType on DocumentMainType.Whid=WarehouseId inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster on DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId where DocumentId='" + Request.QueryString["docid"] + "'";
                SqlDataAdapter adpt11 = new SqlDataAdapter(sssx11, con);
                DataTable dtpt11 = new DataTable();
                adpt11.Fill(dtpt11);
                if (dtpt11.Rows.Count > 0)
                {
                    Label9.Text = dtpt11.Rows[0]["DocumentId"].ToString();
                    Label10.Text = dtpt11.Rows[0]["DocumentTitle"].ToString();

                    Label11.Text = Convert.ToDateTime(dtpt11.Rows[0]["DocumentDate"]).ToShortDateString(); ;

                    Label12.Text = dtpt11.Rows[0]["DocumentType"].ToString();

                    ddlWarehouse.SelectedValue = dtpt11.Rows[0]["WarehouseId"].ToString();


                    ddlWarehouse.Enabled = false;

                    pnnl.Visible = true;

                }
             

            }
            else
            {

                DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

                if (dteeed.Rows.Count > 0)
                {
                    ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                }

            }

            if (Request.QueryString["Tid"] != null)
            {
                ViewState["Trid"] = Request.QueryString["Tid"].ToString();
                ViewState["tid"] = ViewState["Trid"];
                FillRequestedData();
            }
            else
            {


                ddlWarehouse_SelectedIndexChanged(sender, e);
                ddlParty_SelectedIndexChanged(sender, e);
                fillwaresele();
            }

        }
        


    }
    protected void FillRequestedData()
    {
        DataTable dtr = select(" Select TranctionMaster.EntryNumber,DueDay, TaxOption,TranctionMaster.Tranction_Master_Id,SalesChallanMaster.SalesChallanMasterId, GrnMaster_Id,ShippersTrackingNo,ShippingPersonId,SalesChallanMoreInfo.Terms,SalesChallanMoreInfo.PurchaseOrder,SalesOrderMaster.SalesOrderId,ShippersTrackingNo, PaymentMethodID, Tax1Id,Tax2Id,Tax3Id,CustDiscId," +
         " OrderDiscId, SalesChallanMaster.PartyID,TranctionMaster.Whid,TranctionMaster.Date, TranctionMaster.UserId,PaymentMethodID,Total,subTotal,OrderDisc,CustDisc,Tax,Tax1Amt,Tax2Amt," +
         " Tax3Amt,TaxOption from TranctionMasterSuppliment inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=TranctionMasterSuppliment.Tranction_Master_Id inner join TransactionMasterSalesChallanTbl on  TransactionMasterSalesChallanTbl.TransactionMasterId=TranctionMaster.Tranction_Master_Id " +
         "  Inner join  SalesChallanMaster on SalesChallanMaster.SalesChallanMasterId=TransactionMasterSalesChallanTbl.SalesChallanMasterId inner join SalesChallanMoreInfo on SalesChallanMoreInfo.SalesChallanMasterId=SalesChallanMaster.SalesChallanMasterId inner join  " +
          " SalesOrderMaster on  SalesOrderMaster.SalesOrderId=SalesChallanMaster.RefSalesOrderId inner join SalesOrderMasterTamp on " +
         "  SalesOrderMasterTamp.SalesOrderTempId=SalesOrderMaster.SalesOrderId inner join SalesOrderPaymentOption on SalesOrderPaymentOption.SalesOrderId=SalesOrderMasterTamp.SalesOrderTempId where TranctionMaster.Tranction_Master_Id='" + ViewState["Trid"] + "'");
        if (dtr.Rows.Count > 0)
        {
            ViewState["DId"] = Convert.ToString(dtr.Rows[0]["SalesChallanMasterId"]);
        ViewState["ordernonew"] = Convert.ToString(dtr.Rows[0]["SalesOrderId"]);
        ViewState["tid"] = Convert.ToString(dtr.Rows[0]["Tranction_Master_Id"]);
            ViewState["type"] = Convert.ToString(dtr.Rows[0]["TaxOption"]);
            lblinvoiceno.Text = Convert.ToString(dtr.Rows[0]["EntryNumber"]);
            
            if (Convert.ToString(ViewState["type"]) == "1" || Convert.ToString(ViewState["type"]) == "2")
            {
                ViewState["Acc1"] = Convert.ToString(dtr.Rows[0]["Tax1Id"]);
                ViewState["Acc2"] = Convert.ToString(dtr.Rows[0]["Tax2Id"]);
                ViewState["Acc3"] = Convert.ToString(dtr.Rows[0]["Tax3Id"]);
            }
            EventArgs e = new EventArgs();
            object sender = new object();
            ddlWarehouse.SelectedValue = Convert.ToString(dtr.Rows[0]["Whid"]);
            ddlWarehouse_SelectedIndexChanged(sender, e);
            rbt_pay_method.SelectedValue = Convert.ToString(dtr.Rows[0]["PaymentMethodID"]);
            rbt_pay_method_SelectedIndexChanged(sender, e);
            ddlParty.SelectedValue = Convert.ToString(dtr.Rows[0]["PartyID"]);
            ddlParty_SelectedIndexChanged(sender, e);
            ddlsalespersion.SelectedValue = Convert.ToString(dtr.Rows[0]["UserId"]);
            txtGoodsDate.Text = Convert.ToDateTime(dtr.Rows[0]["Date"]).ToShortDateString();
            ViewState["sdo"] = Convert.ToDateTime(dtr.Rows[0]["Date"]).ToShortDateString();
            ViewState["sdo1"] = Convert.ToDateTime(dtr.Rows[0]["Date"]).ToShortDateString();
 
            txtnumberofduedate.Text = Convert.ToString(dtr.Rows[0]["DueDay"]);
            lblGTotal.Text = Convert.ToString(dtr.Rows[0]["subTotal"]);
            lblCustDisc.Text = Convert.ToString(dtr.Rows[0]["CustDisc"]);
            lblOrderDisc.Text = Convert.ToString(dtr.Rows[0]["OrderDisc"]);
            lblTax.Text = Convert.ToString(dtr.Rows[0]["Tax"]);
            lblTotal.Text = Convert.ToString(dtr.Rows[0]["Total"]);
            txtterms.Text = Convert.ToString(dtr.Rows[0]["Terms"]);
            txtperchaseorder.Text = Convert.ToString(dtr.Rows[0]["PurchaseOrder"]);
            if (Convert.ToString(dtr.Rows[0]["GrnMaster_Id"]) != "")
            {
                txtpayduedate.Text = Convert.ToString(dtr.Rows[0]["GrnMaster_Id"]);
            }
            if ((Convert.ToString(dtr.Rows[0]["GrnMaster_Id"]) != "" && Convert.ToString(dtr.Rows[0]["GrnMaster_Id"]) != "0") || Convert.ToString(dtr.Rows[0]["GrnMaster_Id"]) != "")
            {
                chkshipinfo.Checked = true;
                ddlTransporter.SelectedIndex = ddlTransporter.Items.IndexOf(ddlTransporter.Items.FindByValue(dtr.Rows[0]["ShippingPersonId"].ToString()));
                txtTrackingNo.Text = Convert.ToString(dtr.Rows[0]["ShippersTrackingNo"]);
            }
            else
            {
                chkshipinfo.Checked = false;
                ddlTransporter.SelectedIndex = 0;
                txtTrackingNo.Text = "";

            }
            if (Convert.ToString(dtr.Rows[0]["Tax1Id"]) != "")
            {
                pnltxt1.Visible = true;

            }
            if (Convert.ToString(dtr.Rows[0]["Tax2Id"]) != "")
            {
                pnltxt2.Visible = true;

            }
            if (Convert.ToString(dtr.Rows[0]["Tax3Id"]) != "")
            {
                pnltxt3.Visible = true;

            }
            rbt_pay_method.SelectedIndex = rbt_pay_method.Items.IndexOf(rbt_pay_method.Items.FindByValue(dtr.Rows[0]["PaymentMethodID"].ToString()));
            rbt_pay_method_SelectedIndexChanged(sender, e);
            if (rbt_pay_method.SelectedValue == "5" || rbt_pay_method.SelectedValue == "2")
            {
                DataTable rdf = select("Select * from PaymentCreditCard where SaleOrderId='" + Convert.ToString(dtr.Rows[0]["SalesOrderId"]) + "'");
                if (rdf.Rows.Count > 0)
                {
                    txtFName.Text = Convert.ToString(rdf.Rows[0]["FirstName"]);
                    txtLname.Text = Convert.ToString(rdf.Rows[0]["LastName"]);
                    txtPhonenoCreditCard.Text = Convert.ToString(rdf.Rows[0]["PhoneNo"]);
                    txtCCno.Text = Convert.ToString(rdf.Rows[0]["CreditCardNo"]);
                    txtSecureCodeForCC.Text = Convert.ToString(rdf.Rows[0]["SecurityCode"]);
                    txtYear.Text = Convert.ToDateTime(rdf.Rows[0]["ExpireDate"]).ToShortDateString();
                    ddlcityCR.SelectedIndex = ddlcityCR.Items.IndexOf(ddlcityCR.Items.FindByValue(rdf.Rows[0]["Cityid"].ToString()));
                    DataTable dtstate = select("Select StateMasterTbl.StateId,StateMasterTbl.CountryId from CountryMaster inner join StateMasterTbl ON StateMasterTbl.CountryId = CountryMaster.CountryId Inner join CityMasterTbl  ON StateMasterTbl.StateId = CityMasterTbl.StateId where CityMasterTbl.CityId='" + Convert.ToString(rdf.Rows[0]["Cityid"]) + "'");
                    if (dtstate.Rows.Count > 0)
                    {
                        ddlstateCR.SelectedIndex = ddlstateCR.Items.IndexOf(ddlstateCR.Items.FindByValue(dtstate.Rows[0]["StateId"].ToString()));
                        ddlcountryCR.SelectedIndex = ddlcountryCR.Items.IndexOf(ddlcountryCR.Items.FindByValue(dtstate.Rows[0]["CountryId"].ToString()));

                    }
                    txtaddresscredit.Text = Convert.ToString(rdf.Rows[0]["Address"]);
                    txtZipcodecredit.Text = Convert.ToString(rdf.Rows[0]["ZipCode"]);

                }
            }
            else if (rbt_pay_method.SelectedValue == "3")
            {
                DataTable rdf = select("Select * from PaymentChequeDetail where SalesOrderId='" + Convert.ToString(dtr.Rows[0]["SalesOrderId"]) + "'");
                if (rdf.Rows.Count > 0)
                {
                    txtChequeNo.Text = Convert.ToString(rdf.Rows[0]["ChequeNo"]);
                    txtbankname.Text = Convert.ToString(rdf.Rows[0]["BankName"]);
                    txtTransitId.Text = Convert.ToString(rdf.Rows[0]["TransitId"]);
                    txtBranchname.Text = Convert.ToString(rdf.Rows[0]["Branchname"]);
                    txtCity.Text = Convert.ToString(rdf.Rows[0]["BranchCity"]);
                    txtZipcode.Text = Convert.ToString(rdf.Rows[0]["Zipcode"]);
                    ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(rdf.Rows[0]["BranchState"].ToString()));
                    ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(rdf.Rows[0]["BranchCountry"].ToString()));

                }
            }
            if (rbt_pay_method.SelectedValue != "4")
            {
                DataTable rdf = select("Select AccountDebit,Tranction_Details_Id from TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id where TranctionMaster.Tranction_Master_Id='" + ViewState["Trid"] + "' Order by Tranction_Details_Id ASC");
                if (rdf.Rows.Count > 0)
                {
                    ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(rdf.Rows[0]["AccountDebit"].ToString()));
                }
            }
            DataTable rdbitt = select("Select * from BillingAddress  where SalesOrderId='" + Convert.ToString(dtr.Rows[0]["SalesOrderId"]) + "'");
            if (rdbitt.Rows.Count > 0)
            {
                lblName.Text = Convert.ToString(rdbitt.Rows[0]["Name"]);
                lblShippingAdd.Text = Convert.ToString(rdbitt.Rows[0]["Address"]);
                lblEmail.Text = Convert.ToString(rdbitt.Rows[0]["Email"]);
                lblCity.Text = Convert.ToString(rdbitt.Rows[0]["City"]);
                lblState.Text = Convert.ToString(rdbitt.Rows[0]["State"]);
                lblCountry.Text = Convert.ToString(rdbitt.Rows[0]["Country"]);
                lblPhone.Text = Convert.ToString(rdbitt.Rows[0]["Phone"]);
                lblzip.Text = Convert.ToString(rdbitt.Rows[0]["Zipcode"]);
            }
            DataTable rdshi = select("Select * from ShippingAddress  where SalesOrderId='" + Convert.ToString(dtr.Rows[0]["SalesOrderId"]) + "'");
            if (rdbitt.Rows.Count > 0)
            {
                lblName1.Text = Convert.ToString(rdshi.Rows[0]["Name"]);
                lblShippingAdd1.Text = Convert.ToString(rdshi.Rows[0]["Address"]);
                lblEmail1.Text = Convert.ToString(rdshi.Rows[0]["Email"]);
                lblCity1.Text = Convert.ToString(rdshi.Rows[0]["City"]);
                lblState1.Text = Convert.ToString(rdshi.Rows[0]["State"]);
                lblCountry1.Text = Convert.ToString(rdshi.Rows[0]["Country"]);
                lblPhone1.Text = Convert.ToString(rdshi.Rows[0]["Phone"]);
                lblzip1.Text = Convert.ToString(rdshi.Rows[0]["Zipcode"]);
            }
            if (Convert.ToString(dtr.Rows[0]["SalesOrderId"]) != "")
            {
                DataTable ds1 = select("select * from OrderValueDiscountMaster where  OrderValueDiscountMasterId ='" + Convert.ToString(dtr.Rows[0]["OrderDiscId"]) + "'");


                if (ds1.Rows.Count > 0)
                {
                   
                    double discount = Convert.ToDouble(ds1.Rows[0]["ValueDiscount"].ToString());
                    string checkper = Convert.ToString(ds1.Rows[0]["IsPercentage"].ToString());
                    if (checkper == "False")
                    {

                        lblorderdiscname.Text = Convert.ToString(ds1.Rows[0]["SchemeName"]) + "    $" + discount;
                    }
                    else
                    {

                        lblorderdiscname.Text = Convert.ToString(ds1.Rows[0]["SchemeName"]) + "     " + discount + "%";
                    }


                }
            }



            lblcusdisname.Text = "";
           DataTable dpc =select( "SELECT IsPercentage,PartyCategoryName, PartyCategoryDiscount, PartyTypeCategoryMasterId " +
                        " FROM PartyTypeCategoryMasterTbl WHERE   PartyTypeCategoryMasterId='" + Convert.ToString(dtr.Rows[0]["CustDiscId"]) + "'");
         
            if (dpc.Rows.Count > 0)
            {
              
                int i = Convert.ToInt32(dpc.Rows[0]["IsPercentage"]);

                if (i == 1)
                  
                {
                    lblcusdisname.Text = Convert.ToString(dpc.Rows[0]["PartyCategoryName"])+"    " + dpc.Rows[0]["PartyCategoryDiscount"]+"%";
               
                   
                }
                else
                {
                  
                    double CustDis = Convert.ToDouble(dpc.Rows[0]["PartyCategoryDiscount"]);
                      lblcusdisname.Text = Convert.ToString(dpc.Rows[0]["PartyCategoryName"])+  "   $"+CustDis;
                  
                }
            }

            pnlinv.Visible = true;

            DataTable dt = select("SELECT Distinct VolDisc as VolumeDis,AvgCost as AvgRate,(Cast(InventoryWarehouseMasterAvgCostTbl.QtyonHand as decimal)+SalesOrderDetail.Qty) as QtyonHand,Cast((AvgCost*SalesOrderDetail.Qty) as decimal(18,2)) as AvgCost,Cast((SalesOrderDetailTemp.Total)-(AvgCost*SalesOrderDetail.Qty) as decimal(18,2)) as Markup,   PromDisc as PromoDis,Price as yourprice, SalesOrderDetail.SalesOrderId as SODetailId, TaxOption,InventoryMaster.CatType,InventoryWarehouseMasterTbl.Weight as Unit,UnitTypeMaster.Name as UnitType,Promorate as promoprice,Bulkrate as bulkprice,Price as AppliedRate,SalesOrderDetail.Qty as UnshipQty,SalesOrderDetail.Qty as OderedQty,SalesOrderDetail.Qty, SalesOrderDetail.Rate,SalesOrderDetail.Rate as Note, InventoryMaster.Name, InventoryMaster.ProductNo, " +
                     "  SalesOrderDetailTemp.Total,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,SalesOrderDetail.Tax1Id,SalesOrderDetail.Tax2Id,SalesOrderDetail.Tax3Id  " +
                     " FROM      UnitTypeMaster inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.UnitTypeId=UnitTypeMaster.UnitTypeId    Inner join " +
                     "   InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId Inner join  " +
                     "   SalesOrderDetail Inner join " +
                     "   SalesOrderMaster ON SalesOrderMaster.SalesOrderId = SalesOrderDetail.SalesOrderMasterId ON  " +
                     "   InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = SalesOrderDetail.InventoryWHM_Id inner join  SalesOrderDetailTemp on SalesOrderDetailTemp.InventoryWHM_Id= InventoryWarehouseMasterTbl.InventoryWarehouseMasterId inner join SalesOrderMasterTamp on SalesOrderMasterTamp.SalesOrderTempId=SalesOrderDetailTemp.SalesOrderTempId inner join InventoryWarehouseMasterAvgCostTbl on InventoryWarehouseMasterAvgCostTbl.InvWMasterId=InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                        " Where  InventoryWarehouseMasterAvgCostTbl.Tranction_Master_Id='"+ ViewState["tid"]+"' and InventoryMaster.CatType IS NULL and SalesOrderDetail.SalesOrderMasterId = '" + Convert.ToString(dtr.Rows[0]["SalesOrderId"]) + "' and  SalesOrderDetailTemp.SalesOrderTempId = '" + Convert.ToString(dtr.Rows[0]["SalesOrderId"]) + "'");


            if (dt.Rows.Count > 0)
            {
                pnlprod.Visible = true;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Columns[8].Visible = true;
                GridView1.Columns[7].Visible = true;
                ViewState["dt"] = dt;
                GridView1.Columns[11].Visible = false;
                GridView1.Columns[12].Visible = false;
                GridView1.Columns[10].Visible = false;
            }
            
            
             DataTable dtds = select("SELECT Distinct VolDisc as VolumeDis, PromDisc as PromoDis,Price as yourprice, SalesOrderDetail.SalesOrderId as SODetailId, TaxOption,InventoryMaster.CatType,InventoryWarehouseMasterTbl.Weight as Unit,UnitTypeMaster.Name as UnitType,Promorate as promoprice,Bulkrate as bulkprice,Price as AppliedRate,SalesOrderDetail.Qty as UnshipQty,SalesOrderDetail.Qty as OderedQty,SalesOrderDetail.Qty, SalesOrderDetail.Rate,SalesOrderDetail.Rate as Note, InventoryMaster.Name, InventoryMaster.ProductNo, " +
                     "  SalesOrderDetailTemp.Total,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,SalesOrderDetail.Tax1Id,SalesOrderDetail.Tax2Id,SalesOrderDetail.Tax3Id  " +
                     " FROM      UnitTypeMaster inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.UnitTypeId=UnitTypeMaster.UnitTypeId    Inner join " +
                     "   InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId Inner join  " +
                     "   SalesOrderDetail Inner join " +
                     "   SalesOrderMaster ON SalesOrderMaster.SalesOrderId = SalesOrderDetail.SalesOrderMasterId ON  " +
                     "   InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = SalesOrderDetail.InventoryWHM_Id inner join  SalesOrderDetailTemp on SalesOrderDetailTemp.InventoryWHM_Id= InventoryWarehouseMasterTbl.InventoryWarehouseMasterId inner join SalesOrderMasterTamp on SalesOrderMasterTamp.SalesOrderTempId=SalesOrderDetailTemp.SalesOrderTempId " +
                        " Where InventoryMaster.CatType='0' and SalesOrderDetail.SalesOrderMasterId = '" + Convert.ToString(dtr.Rows[0]["SalesOrderId"]) + "' and  SalesOrderDetailTemp.SalesOrderTempId = '" + Convert.ToString(dtr.Rows[0]["SalesOrderId"]) + "'");



             GridView3.DataSource = dtds;
            GridView3.DataBind();
           
            GridView3.Columns[7].Visible = true;
            ViewState["dt1"] = dtds;
            GridView3.Columns[11].Visible = false;
            GridView3.Columns[12].Visible = false;
            GridView3.Columns[10].Visible = false;
            if (dtds.Rows.Count > 0)
            {
                pnlserv.Visible = true;
            }

            if (dt.Rows.Count > 0)
            {

                if (Convert.ToString(dt.Rows[0]["CatType"]) == "")
                {
                    rdinvoice.SelectedIndex = 0;

                }
                else
                {
                    rdinvoice.SelectedIndex = 1;
                }
                rdinvoice_SelectedIndexChanged(sender, e);
                rbList_SelectedIndexChanged(sender, e);
                if (Convert.ToString(ViewState["type"]) == "3")
                {
                    ViewState["Acc1"] = Convert.ToString(dt.Rows[0]["Tax1Id"]);
                    ViewState["Acc2"] = Convert.ToString(dt.Rows[0]["Tax2Id"]);
                    ViewState["Acc3"] = Convert.ToString(dt.Rows[0]["Tax3Id"]);
                    gridtax();
                }
                else
                {
                    CountTax();
                    ctax();

                }
            }
            else
            {
                if (dtds.Rows.Count > 0)
                {

                    if (Convert.ToString(dtds.Rows[0]["CatType"]) == "")
                    {
                        rdinvoice.SelectedIndex = 0;

                    }
                    else
                    {
                        rdinvoice.SelectedIndex = 1;
                    }
                    rdinvoice_SelectedIndexChanged(sender, e);
                    rbList_SelectedIndexChanged(sender, e);
                    if (Convert.ToString(ViewState["type"]) == "3")
                    {
                        ViewState["Acc1"] = Convert.ToString(dtds.Rows[0]["Tax1Id"]);
                        ViewState["Acc2"] = Convert.ToString(dtds.Rows[0]["Tax2Id"]);
                        ViewState["Acc3"] = Convert.ToString(dtds.Rows[0]["Tax3Id"]);
                        gridtax();
                    }
                    else
                    {
                        CountTax();
                        ctax();

                    }
                }
            }
            filldydata();
            fillfoter();
            double sustotal = Convert.ToDouble(lblCustDisc.Text) + Convert.ToDouble(lblOrderDisc.Text);
            double nett = Convert.ToDouble(Convert.ToDouble(lblGTotal.Text) - sustotal);
            lblnettot.Text = String.Format("{0:0.00}", Math.Round(nett, 2).ToString());
            linehide();
        }
    }

    public DataView fillSalesOrder()
    {

        DataTable dt = new DataTable();
        DataColumn dtcom = new DataColumn();
        dtcom.DataType = System.Type.GetType("System.Int32");
        dtcom.ColumnName = "SalesOrderId";
        dtcom.ReadOnly = false;
        dtcom.Unique = false;
        dtcom.AllowDBNull = true;
        dt.Columns.Add(dtcom);

        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand("SELECT     SalesOrderId FROM  SalesOrderMaster where SalesOrderId not in (SELECT   RefSalesOrderId " +
                            " FROM    SalesChallanMaster)", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        adp.Fill(ds);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            DataRow dtrow = dt.NewRow();
            dtrow["SalesOrderId"] = Convert.ToInt32(dr["SalesOrderId"]);
            dt.Rows.Add(dtrow);
        }

        DataSet ds1 = new DataSet();
        SqlCommand cmd1 = new SqlCommand("SELECT distinct SalesOrderMaster.SalesOrderId FROM  StatusControl INNER JOIN   SalesOrderMaster ON StatusControl.SalesOrderId = SalesOrderMaster.SalesOrderId " +
                                        " WHERE     (StatusControl.StatusMasterId = 11) ", con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        adp1.Fill(ds1);

        foreach (DataRow dr in ds1.Tables[0].Rows)
        {
            DataRow dtrow = dt.NewRow();
            dtrow["SalesOrderId"] = Convert.ToInt32(dr["SalesOrderId"]);
            dt.Rows.Add(dtrow);
        }
        DataView dv = dt.DefaultView;
        dv.Sort = "SalesOrderId  asc";

        return dv;

    }
    public void fillwaresele()
    {
        ddlParty.DataSource = (DataSet)fillParty();
        ddlParty.DataTextField = "Compname";
        ddlParty.DataValueField = "PartyID";
        ddlParty.DataBind();
        //  ddlParty.Items.Insert(0, "-select-");
        // ddlParty.SelectedItem.Value = "0";


        ddlCash.DataSource = (DataSet)fillCashAccount();
        ddlCash.DataTextField = "AccountName";
        ddlCash.DataValueField = "AccountId";
        ddlCash.DataBind();
        if (pnlinv.Visible == true)
        {
            ddlCategory.DataSource = (DataSet)fillCategory();
            ddlCategory.DataTextField = "category";
            ddlCategory.DataValueField = "InventorySubSubId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "-Select-");
            ddlCategory.Items[0].Value = "0";
        }
    }
    public DataSet fillPaidBy()
    {
        SqlCommand cmd = new SqlCommand("SELECT AccountName, AccountId, GroupId FROM  AccountMaster WHERE  (GroupId = 1) order by AccountName", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }

    public DataSet FillFromAddress()
    {
        string str = "SELECT WareHouseId, Name FROM WareHouseMaster where comid= " +
            " '" + com + "' order by Name";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    public int FillDelNo()
    {
        string str = "SELECT MAX(SalesChallanMasterId) AS DelNo FROM  SalesChallanMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        int i = 1;
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["DelNo"].ToString() != "")
            {

                i = Convert.ToInt32(ds.Tables[0].Rows[0]["DelNo"]) + 1;
            }
            else
            {
                i = 1;
            }
        }
        else
        {
            i = 1;
        }
        //int i = Convert.ToInt32(ds.Tables[0].Rows[0]["DelNo"]) + 1;
        return i;
    }
    public DataSet fillParty()
    {
        string str = " SELECT     Party_master.PartyID, Party_master.Account, Party_master.Compname, " +
            " Party_master.Address, Party_master.City, Party_master.State, Party_master.Country,  " +
               "       User_master.Active, Party_master.PartyTypeId " +
              " FROM         Party_master Inner JOIN " +
             "         User_master ON Party_master.PartyID = User_master.PartyID " +
           " WHERE     (User_master.Active = 1) and Party_master.Whid='" + ddlWarehouse.SelectedValue + "' " +
            " ORDER BY Party_master.Contactperson ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    public DataSet fillCashAccount()
    {

        SqlCommand cmd = new SqlCommand("SELECT AccountName, AccountId, GroupId FROM  AccountMaster   WHERE  GroupId=1 and compid='" + Session["comid"] + "' and Whid='" + ddlWarehouse.SelectedValue + "' order by AccountName asc", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    public DataSet FillShippers()
    {
        string str = "SELECT  ShippersName, ShippersId FROM  ShippersMaster where compid= " +
            " '" + Session["comid"] + "' order by ShippersName";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    public DataSet fillShipperPerson()
    {
        //string str = "SELECT     User_master.UserID, User_master.Name, Party_master.PartyTypeId ,  Party_master.Compname" +
        //                " FROM         User_master INNER JOIN " +
        //              " Party_master ON User_master.PartyID = Party_master.PartyID " +
        //            " WHERE     (Party_master.PartyTypeId = 10) ";
        string str = "SELECT     User_master.UserID, User_master.Name, Party_master.PartyTypeId ,  Party_master.Compname" +
                       " FROM         User_master INNER JOIN " +
                     " Party_master ON User_master.PartyID = Party_master.PartyID ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }

    protected void ddlShippers_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void imgCal_Click(object sender, ImageClickEventArgs e)
    {
        //Calendar1.Visible = true;
    }

    protected void rbList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbList.SelectedIndex == 0)
        {
            //finalpanel.Visible = false;
            GridView2.Visible = false;
            PnlCategory.Visible = true;
            //pnlBarcod.Visible = false;
            pnlSearch.Visible = false;

            ddlCategory.DataSource = (DataSet)fillCategory();
            ddlCategory.DataTextField = "category";
            ddlCategory.DataValueField = "InventorySubSubId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "-Select-");
            ddlCategory.Items[0].Value = "0";
            ddlCategory_SelectedIndexChanged(sender, e);
        }
       
        else if (rbList.SelectedIndex == 1)
        {
            PnlCategory.Visible = false;
            //pnlBarcod.Visible = false;
            pnlSearch.Visible = true;

        }
    }
    protected void avgcost(Label abc, Label avgcost, Label onhand)
    {


        DataTable drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + abc.Text + "' and DateUpdated<='" + txtGoodsDate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

        if (drtinvdata.Rows.Count > 0)
        {
            avgcost.Text = Convert.ToString(drtinvdata.Rows[0]["AvgCost"]);
            DataTable dton = select("SELECT Qty FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + abc.Text + "' and Tranction_Master_Id='" + ViewState["tid"] + "'");

            if (dton.Rows.Count > 0)
            {
                if (Convert.ToDateTime(ViewState["sdo"]) > Convert.ToDateTime(txtGoodsDate.Text))
                {
                    onhand.Text = Convert.ToString(Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]));
                }
                else
                {
                    onhand.Text = Convert.ToString(Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]) - Convert.ToDecimal(dton.Rows[0]["Qty"]));
                }
            }
             
        }
        else
        {
            avgcost.Text = "0";
            onhand.Text = "0";
        }

    }
    protected int calca()
    {
        int fl = 0;
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            Label lblmarkup = (Label)gdr.FindControl("lblmarkup");
            Label lblavgcost = (Label)gdr.FindControl("lblmarkup");
            Label lblqtyonhand = (Label)gdr.FindControl("lblqtyonhand");
            Label lblredmask = (Label)gdr.FindControl("lblredmask");
            Label lblavgrate = (Label)gdr.FindControl("lblavgrate");
            Label lblinvwm = (Label)gdr.FindControl("lblinvwm");
            TextBox TextBox4 = (TextBox)gdr.FindControl("TextBox4");
            avgcost(lblinvwm, lblavgrate, lblqtyonhand);
            if (TextBox4.Text != "")
            {
                if (lblqtyonhand.Text == "")
                {
                    lblqtyonhand.Text = "0";
                }
                if (Convert.ToDouble(lblqtyonhand.Text) < Convert.ToDouble(TextBox4.Text))
                {
                    fl = 1;
                    lblredmask.Visible = true;
                }
                else
                {
                    lblredmask.Visible = false;
                }
                double apprate = 0;
                if (GridView1.Rows[gdr.RowIndex].Cells[9].Text != "")
                {
                    apprate = Convert.ToDouble(GridView1.Rows[gdr.RowIndex].Cells[9].Text);
                }
                else
                {
                    apprate = Convert.ToDouble(GridView1.Rows[gdr.RowIndex].Cells[6].Text);
                }
                lblavgcost.Text = Convert.ToString((Convert.ToDouble(lblavgrate.Text) * Convert.ToDouble(TextBox4.Text)));
                lblmarkup.Text = Convert.ToString((Convert.ToDouble(TextBox4.Text) * apprate) - (Convert.ToDouble(lblavgcost.Text) * Convert.ToDouble(TextBox4.Text)));

            }
        }
        if (fl == 1)
        {
            lblmsg1.Visible = true;
            lblmsg1.Text = "Order Qty must be less than Qty on hand.";
        }
        return fl;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        txtsearch.Text = "";
        GridView2.Visible = false;
        if (rbt_pay_method.SelectedIndex >= 0)
        { int  fl = calca();
        if (fl != 1)
        {
            string date = "select Convert(nvarchar,StartDate,101) as StartDate,EndDate from [ReportPeriod] where Whid = '" + ddlWarehouse.SelectedValue + "' and Active='1'";
            SqlCommand cmd = new SqlCommand(date, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //txtdate.Text = Convert.ToString(System.DateTime.Now.Date.ToShortDateString());
            if (dt.Rows.Count > 0)
            {
                if (txtYear.Text.ToString() == "")
                {
                    txtYear.Text = txtGoodsDate.Text;
                }
                if (Convert.ToDateTime(txtGoodsDate.Text) < Convert.ToDateTime(dt.Rows[0][0].ToString()) && Convert.ToDateTime(txtYear.Text) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0]["StartDate"].ToString();
                    ModalPopupExtender1222.Show();

                }

                else
                {
                    if (ViewState["dt"] == null && ViewState["dt1"] == null)
                    {
                    }
                    else
                    {
                        txtGoodsDate_TextChanged(sender, e);
                        //try
                        //{
                        if (rbt_pay_method.SelectedValue == "2")
                        {

                            if (Convert.ToDateTime(txtYear.Text) >= Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                            {
                                InsertOrigenalOrder();
                            }
                            else
                            {
                                lblmsg1.Text = "Credit Card expire date today or greter then today date";
                            }
                        }
                        else
                        {
                            InsertOrigenalOrder();
                        }

                        //btnSubmit.Enabled = false;


                        //}

                        //catch (Exception ex)
                        //{
                        //    lblmsg1.Visible = true;
                        //    lblmsg1.Text = "Error : " + ex.Message;

                        //}
                    }
                }
            }
        }
        else
        {
            lblmsg1.Visible = true;
            lblmsg1.Text = "Order Qty must be less than Qty on hand.";
        }

        }
        else
        {
            lblmsg1.Visible = true;
            lblmsg1.Text = "Please Select Payment Method";
        }
    }
    protected decimal isdecimalornot(string ck)
    {
        decimal ik = 0;
        try
        {
            ik = Convert.ToDecimal(ck);
            return ik;
        }
        catch
        {
            return ik;

        }
    }
    public void InsertTransactionDetail(int AccountDebit, int AccountCredit, decimal AmountDebit, decimal AmountCredit, int Tranction_Master_Id, string Memo, DateTime DateTimeOfTransaction, string DiscEarn, string DiscPaid)
    {
        //string str2 = "insert into Tranction_Details_Temp(AccountDebit,AccountCredit,AmountDebit,AmountCredit,Tranction_Master_Temp_Id,Memo,DateTimeOfTransaction,DiscEarn,DiscPaid)" +
        //    " values('57','" + acctcredited + "','" + Convert.ToDecimal(Session["total"]) + "','" + Convert.ToDecimal(Session["total"]) + "','" + Convert.ToInt32(ds.Tables[0].Rows[0]["id"]) + "','Pay Pal Recived " + txn_id + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "','0','0')";

        try
        {
            string str2 = "insert into Tranction_Details(AccountDebit,AccountCredit,AmountDebit,AmountCredit,Tranction_Master_Id,Memo,DateTimeOfTransaction,DiscEarn,DiscPaid,compid,whid)" +
                " values('" + AccountDebit + "','" + AccountCredit + "','" + AmountDebit + "','" + AmountCredit + "','" + Tranction_Master_Id + "','" + Memo + "','" + DateTimeOfTransaction.ToShortDateString() + "','" + DiscEarn + "','" + DiscPaid + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd2.ExecuteNonQuery();
            con.Close();
        }
        catch
        {

        }
    }
    public void insertpayOption(int salesOrderId)
    {
        string str = "insert into SalesOrderPaymentOption(SalesOrderId,PaymentMethodID) values('" + salesOrderId + "','" + rbt_pay_method.SelectedValue + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }



    public DataSet fillCategory()
    {
        string str = "SELECT distinct    left(InventoryCategoryMaster.InventoryCatName,8) + '-' + left(InventorySubCategoryMaster.InventorySubCatName,8) + '-' + left(InventoruSubSubCategory.InventorySubSubName,8) " +
                       " AS category, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubSubId " +
                        " FROM         InventorySubCategoryMaster INNER JOIN " +
                      " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                      " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryCategoryMaster.compid= '" + com + "' " +
                      " and InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "' " + prodService + "";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlItem.DataSource = (DataSet)fillItem();
        ddlItem.DataTextField = "Name";
        ddlItem.DataValueField = "InventoryWarehouseMasterId";
        ddlItem.DataBind();
        ddlItem.Items.Insert(0, "-select-");
        ddlItem.Items[0].Value = "0";
        txtProdNo.Text = "";
        txtUnit.Text = " ";
        lblRate.Text = "0.00";
        lblQtyOnHand.Text = "0.00";
        lblPromoRate.Text = "0.00";
        lblBulkDisc.Text = "0.00";
        lblBulkQty.Text = "0.00";
        //lblYourRate.Text = "0.00";
        lblQtyOnHand.Text = "0";
        //  lblCustDisc.Text = "0.00";
        //     txtNetPrice.Text = "0.00";



    }
    public DataSet fillItem()
    {

        DataSet ds = new DataSet();
        //if (ddlWarehouse.SelectedIndex > 0)
        //{
        string str = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.InventorySubSubId,UnitTypeMaster.Name AS unittype, " +
                     "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active " +
                     "   FROM      UnitTypeMaster  Inner join InventoryWarehouseMasterTbl  on " +
                         " InventoryWarehouseMasterTbl.UnitTypeId=UnitTypeMaster.UnitTypeId   Inner join " +
                     "  InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId " +
                     " where InventoryWarehouseMasterTbl.Active =1  " +
                     " and InventoryWarehouseMasterTbl.WareHouseId=" + Convert.ToInt32(ddlWarehouse.SelectedValue) + " " +
                     " and InventoryMaster.InventorySubSubId = '" + ddlCategory.SelectedValue + "' order by Name ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        adp.Fill(ds);

        //}
        return ds;

    }

    public DataSet fillUnitType()
    {
        string str = "SELECT  UnitTypeId, Name FROM  UnitTypeMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    public double GetBulkDiscount(int InventorywhID, decimal rate)
    {
        double BulkpricePerc = 0;
        double BUlkPrice = 0;
        double BulkPriceFlat = 0;
        double SDB;
        SDB = 0;
        double SDF;
        SDF = 0;
        //string str = "SELECT     max(InventoryVolumeSchemeMaster.SchemeDiscount) as Discount " +
        //            " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
        //             " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
        //            "WHERE     (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1) AND  " +
        //             " (InventoryVolumeSchemeMaster.IsPercentage = 1) ";

        //***************************



        string str = "SELECT     max(InventoryVolumeSchemeMaster.SchemeDiscount) as Discount" +
                  " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                   " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                  "WHERE   ((InventoryVolumeSchemeMaster.EffectiveStartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (InventoryVolumeSchemeMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and  (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1) and (InventoryVolumeSchemeMaster.IsPercentage = 1) ";
        //  " (InventoryVolumeSchemeMaster.IsPercentage = 1) ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);


        //**********chnages codes


        if (Convert.ToString(ds.Tables[0].Rows[0]["Discount"]) != "")
        {
            BulkpricePerc = (Convert.ToDouble(rate) * Convert.ToDouble(ds.Tables[0].Rows[0]["Discount"])) / 100;

            // totalvolumedis = totalvolumedis + BulkpricePerc;
            BUlkPrice = Convert.ToDouble(rate) - BulkpricePerc;
            SDB = Convert.ToDouble(ds.Tables[0].Rows[0]["Discount"]);
            BulkPriceFlat = BulkpricePerc;
            SDF = SDB;
        }

        else
        {

            string str12 = "SELECT     max(InventoryVolumeSchemeMaster.SchemeDiscount) as Discount" +
                             " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                              " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                             "WHERE  ((InventoryVolumeSchemeMaster.EffectiveStartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (InventoryVolumeSchemeMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and   (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1) and (InventoryVolumeSchemeMaster.IsPercentage = 0)";
            //  " (InventoryVolumeSchemeMaster.IsPercentage = 1) ";
            SqlCommand cmd12 = new SqlCommand(str12, con);
            SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
            DataSet ds12 = new DataSet();
            adp12.Fill(ds12);

            if (Convert.ToString(ds12.Tables[0].Rows[0]["Discount"]) != "")
            {
                BulkpricePerc = Convert.ToDouble(ds12.Tables[0].Rows[0]["Discount"]);
                //   totalvolumedis = totalvolumedis + BulkpricePerc;
                BUlkPrice = Convert.ToDouble(rate) - BulkpricePerc;
                SDB = Convert.ToDouble(ds12.Tables[0].Rows[0]["Discount"]);
                BulkPriceFlat = Convert.ToDouble(ds12.Tables[0].Rows[0]["Discount"]);
                SDF = SDB;

            }
        }
        //**************chnegs codes
        //string str1 = "SELECT     max(InventoryVolumeSchemeMaster.SchemeDiscount) as Discount " +
        //            " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
        //             " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
        //            "WHERE     (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1)  ";
        //             //" (InventoryVolumeSchemeMaster.IsPercentage = 0) ";
        //SqlCommand cmd1 = new SqlCommand(str1, con);
        //SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        //DataSet ds1 = new DataSet();
        //adp1.Fill(ds1);

        //if (Convert.ToString(ds1.Tables[0].Rows[0]["Discount"]) != "") //{ //    BulkpricePerc = Convert.ToDouble(ds1.Tables[0].Rows[0]["Discount"]);   //    BUlkPrice = Convert.ToDouble(rate) - BulkpricePerc;    //    SDB = Convert.ToDouble(ds1.Tables[0].Rows[0]["Discount"]);    //    //***********************
        //    BulkPriceFlat = Convert.ToDouble(ds1.Tables[0].Rows[0]["Discount"]);

        //    SDF = SDB;
        //}
        //***********************************************

        if (BUlkPrice > BulkPriceFlat)
        {

            string strB = "SELECT   InventoryVolumeSchemeMaster.MinDiscountQty as BulkQty   " +
                   " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                    " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                   "WHERE    ((InventoryVolumeSchemeMaster.EffectiveStartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (InventoryVolumeSchemeMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and  (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1) AND  " +
                    " (InventoryVolumeSchemeMaster.IsPercentage = 1) and  InventoryVolumeSchemeMaster.SchemeDiscount=" + SDB.ToString() + " ORDER BY InventoryVolumeSchemeMaster.SchemeID DESC";
            SqlCommand cmdB = new SqlCommand(strB, con);
            SqlDataAdapter adpB = new SqlDataAdapter(cmdB);
            DataSet dsB = new DataSet();
            adpB.Fill(dsB);
            if (dsB.Tables[0].Rows.Count > 0)
            {
                lblBulkQty.Text = dsB.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                string strB12 = "SELECT   InventoryVolumeSchemeMaster.MinDiscountQty as BulkQty   " +
                   " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                    " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                   "WHERE   ((InventoryVolumeSchemeMaster.EffectiveStartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (InventoryVolumeSchemeMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and   (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1) AND  " +
                    " (InventoryVolumeSchemeMaster.IsPercentage = 0) and  InventoryVolumeSchemeMaster.SchemeDiscount=" + SDB.ToString() + " ORDER BY InventoryVolumeSchemeMaster.SchemeID DESC";
                SqlCommand cmdB12 = new SqlCommand(strB12, con);
                SqlDataAdapter adpB12 = new SqlDataAdapter(cmdB12);
                DataSet dsB12 = new DataSet();
                adpB12.Fill(dsB12);
                if (dsB12.Tables[0].Rows.Count > 0)
                {
                    lblBulkQty.Text = dsB12.Tables[0].Rows[0][0].ToString();
                }

            }
            return BUlkPrice;
        }
        else
        {
            string strF = "SELECT   InventoryVolumeSchemeMaster.MinDiscountQty as BulkQty   " +
                   " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                    " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                   "WHERE   ((InventoryVolumeSchemeMaster.EffectiveStartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (InventoryVolumeSchemeMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and   (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1) AND  " +
                    " (InventoryVolumeSchemeMaster.IsPercentage = 1) and  InventoryVolumeSchemeMaster.SchemeDiscount=" + SDF.ToString() + " ORDER BY InventoryVolumeSchemeMaster.SchemeID DESC";
            SqlCommand cmdF = new SqlCommand(strF, con);
            SqlDataAdapter adpF = new SqlDataAdapter(cmdF);
            DataSet dsF = new DataSet();
            adpF.Fill(dsF);
            if (dsF.Tables[0].Rows.Count > 0)
            {
                lblBulkQty.Text = dsF.Tables[0].Rows[0][0].ToString();
            }

            else
            {

                string strF1 = "SELECT   InventoryVolumeSchemeMaster.MinDiscountQty as BulkQty   " +
                   " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                    " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                   "WHERE  ((InventoryVolumeSchemeMaster.EffectiveStartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (InventoryVolumeSchemeMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and    (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1) AND  " +
                    " (InventoryVolumeSchemeMaster.IsPercentage = 0) and  InventoryVolumeSchemeMaster.SchemeDiscount=" + SDF.ToString() + " ORDER BY InventoryVolumeSchemeMaster.SchemeID DESC";
                SqlCommand cmdF1 = new SqlCommand(strF1, con);
                SqlDataAdapter adpF1 = new SqlDataAdapter(cmdF1);
                DataSet dsF1 = new DataSet();
                adpF1.Fill(dsF1);
                if (dsF1.Tables[0].Rows.Count > 0)
                {
                    lblBulkQty.Text = dsF1.Tables[0].Rows[0][0].ToString();
                }


            }

            return BulkPriceFlat;
        }
    }
    public double GetPartyPrice(decimal rate)
    {
        if (Session["userid"] != null)
        {
            double partyrate = 0;
            double partydisc = 0;
            //string str = "SELECT     PartyTypeCategoryMasterTbl.IsPercentage, PartyTypeCategoryMasterTbl.PartyCategoryDiscount,  " +
            //          " PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
            //        " FROM         Party_master INNER JOIN " +
            //         " PartyTypeDetailTbl ON Party_master.PartyID = PartyTypeDetailTbl.PartyID INNER JOIN " +
            //         " User_master ON Party_master.PartyID = User_master.PartyID INNER JOIN " +
            //         " PartyTypeCategoryMasterTbl ON PartyTypeDetailTbl.PartyTypeCategoryMasterId = PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
            //        " WHERE     (Party_master.PartyID = '" + Convert.ToInt32(Session["userid"].ToString()) + "') AND (PartyTypeCategoryMasterTbl.Active = 1) " +
            //        " ORDER BY PartyTypeCategoryMasterTbl.EntryDate DESC ";


            string str = "SELECT     PartyTypeCategoryMasterTbl.IsPercentage,User_master.State, PartyTypeCategoryMasterTbl.PartyCategoryDiscount,  " +
                     " PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
                   " FROM         Party_master INNER JOIN " +
                    " PartyTypeDetailTbl ON Party_master.PartyID = PartyTypeDetailTbl.PartyID INNER JOIN " +
                    " User_master ON Party_master.PartyID = User_master.PartyID INNER JOIN " +
                    " PartyTypeCategoryMasterTbl ON PartyTypeDetailTbl.PartyTypeCategoryMasterId = PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
                   " WHERE     (User_master.UserID = '" + Convert.ToInt32(Session["userid"]) + " ') AND (PartyTypeCategoryMasterTbl.Active = 1) " +
                   " ORDER BY PartyTypeCategoryMasterTbl.EntryDate DESC ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0]["IsPercentage"]) != "")
                {
                    partydisc = Convert.ToDouble(rate) * Convert.ToDouble(ds.Tables[0].Rows[0]["PartyCategoryDiscount"]) / 100;
                    partyrate = Convert.ToDouble(rate) - partydisc;
                    // totalpartydis = totalpartydis + partydisc;
                    return partyrate;
                }
                else
                {
                    partyrate = Convert.ToDouble(ds.Tables[0].Rows[0]["PartyCategoryDiscount"]);
                    //totalpartydis = totalpartydis + partydisc;
                    return partyrate;

                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    public double GetPromotionDiscount(int Qty, decimal rate, int invemtoryId)
    {



        string str = " SELECT     PromotionDiscountMaster.IsPercentage " +
                    " FROM         PromotionDiscountMaster INNER JOIN " +
                  " PromotionDiscountDetail ON PromotionDiscountMaster.PromotionDiscountMasterID = PromotionDiscountDetail.PromotionDiscountMasterID " +
                    " WHERE   ((PromotionDiscountMaster.StartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (PromotionDiscountMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and   (PromotionDiscountMaster.Active = 1) AND (PromotionDiscountDetail.InventoryWHM_Id = '" + invemtoryId + "') " +
                "  ORDER BY PromotionDiscountMaster.PromotionDiscountMasterID DESC ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            int i = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPercentage"]);

            if (i == 1)
            {
                string str1 = " SELECT     PromotionDiscountMaster.IsPercentage, PromotionDiscountMaster.DiscountAmount, PromotionDiscountDetail.PromotionDiscountDetail " +
                    " FROM         PromotionDiscountMaster INNER JOIN " +
                  " PromotionDiscountDetail ON PromotionDiscountMaster.PromotionDiscountMasterID = PromotionDiscountDetail.PromotionDiscountMasterID " +
                     " WHERE   ((PromotionDiscountMaster.StartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (PromotionDiscountMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and   (PromotionDiscountMaster.Active = 1) AND (PromotionDiscountDetail.InventoryWHM_Id = '" + invemtoryId + "') " +
                "  ORDER BY PromotionDiscountMaster.PromotionDiscountMasterID DESC ";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adp1.Fill(ds1);
                Double first = Convert.ToDouble(Qty) * Convert.ToDouble(rate);
                Double PromoRate = (first * Convert.ToDouble(ds1.Tables[0].Rows[0]["DiscountAmount"])) / 100;

                totalpromtionaldis = totalpromtionaldis + PromoRate;
                Double totalcount = Convert.ToDouble(lblpromo.Text);
                totalcount = totalcount + PromoRate;
                lblpromo.Text = Convert.ToString(totalcount);


                return PromoRate;


            }
            else
            {
                string str2 = " SELECT     PromotionDiscountMaster.IsPercentage, PromotionDiscountMaster.DiscountAmount, PromotionDiscountDetail.PromotionDiscountDetail " +
                   " FROM         PromotionDiscountMaster INNER JOIN " +
                 " PromotionDiscountDetail ON PromotionDiscountMaster.PromotionDiscountMasterID = PromotionDiscountDetail.PromotionDiscountMasterID " +
                  " WHERE   ((PromotionDiscountMaster.StartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (PromotionDiscountMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and   (PromotionDiscountMaster.Active = 1) AND (PromotionDiscountDetail.InventoryWHM_Id = '" + invemtoryId + "') " +
                "  ORDER BY PromotionDiscountMaster.PromotionDiscountMasterID DESC ";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                adp2.Fill(ds2);

                double PromoRate = Convert.ToDouble(ds2.Tables[0].Rows[0]["DiscountAmount"]);
                Double totalcount = Convert.ToDouble(lblpromo.Text);
                totalcount = totalcount + PromoRate;
                lblpromo.Text = Convert.ToString(totalcount);
                totalpromtionaldis = totalpromtionaldis + PromoRate;
                return PromoRate;
            }
        }
        else
        {
            lblpromo.Text = "0";
            return 0;
        }
    }
    public double GetVolumeDiscount(int InventorywhID, int Qty, decimal rate)
    {
        string str = "SELECT     InventoryVolumeSchemeMaster.IsPercentage, InventoryVolumeSchemeDetail.InventoryWHM_Id " +
                    " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                    "  InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                    " WHERE     (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND " +
                    " ((InventoryVolumeSchemeMaster.EffectiveStartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (InventoryVolumeSchemeMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and (InventoryVolumeSchemeMaster.Active = 1) and ('" + Qty + "' between InventoryVolumeSchemeMaster.MinDiscountQty and InventoryVolumeSchemeMaster.MaxDiscountQty)" +
                    " ORDER BY InventoryVolumeSchemeMaster.SchemeID DESC ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            int i = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPercentage"]);

            if (i == 1)
            {
                string str1 = "SELECT     InventoryVolumeSchemeMaster.SchemeDiscount, InventoryVolumeSchemeDetail.InventoryWHM_Id, InventoryVolumeSchemeMaster.Active,  " +
                                          "InventoryVolumeSchemeMaster.MinDiscountQty, InventoryVolumeSchemeMaster.MaxDiscountQty,  " +
                                           " InventoryVolumeSchemeMaster.EffectiveStartDate,InventoryVolumeSchemeDetail.SchemeDetailID, InventoryVolumeSchemeMaster.IsPercentage " +
                              " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                                          " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                              " WHERE     (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND " +
                    " ((InventoryVolumeSchemeMaster.EffectiveStartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (InventoryVolumeSchemeMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and (InventoryVolumeSchemeMaster.Active = 1) and ('" + Qty + "' between InventoryVolumeSchemeMaster.MinDiscountQty and InventoryVolumeSchemeMaster.MaxDiscountQty)" +
                    " ORDER BY InventoryVolumeSchemeMaster.SchemeID DESC ";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adp1.Fill(ds1);
                double first = Convert.ToDouble(Qty) * Convert.ToDouble(rate);
                double volumeDis = (first * Convert.ToDouble(ds1.Tables[0].Rows[0]["SchemeDiscount"])) / 100;


                // double PromoRate = Convert.ToDouble(ds2.Tables[0].Rows[0]["DiscountAmount"]);

                totalvolumedis = totalvolumedis + volumeDis;
                Double totalcount = Convert.ToDouble(lblvolume.Text);
                totalcount = totalcount + volumeDis;
                lblvolume.Text = Convert.ToString(totalcount);

                Session["VolumeSchemeDetailId"] = ds1.Tables[0].Rows[0]["SchemeDetailID"];
                Session["VolumeDiscountRate"] = ds1.Tables[0].Rows[0]["SchemeDiscount"];
                Session["VolumeDiscountIsPercent"] = ds1.Tables[0].Rows[0]["IsPercentage"];
                //Session["VolumeDiscountAmount"] = ds1.Tables[0].Rows[0]["SchemeDetailID"];
                return volumeDis;

            }
            else
            {
                string str2 = "SELECT     InventoryVolumeSchemeMaster.SchemeDiscount, InventoryVolumeSchemeDetail.InventoryWHM_Id, InventoryVolumeSchemeMaster.Active,  " +
                                        "InventoryVolumeSchemeMaster.MinDiscountQty, InventoryVolumeSchemeMaster.MaxDiscountQty,  " +
                                         " InventoryVolumeSchemeMaster.EffectiveStartDate, InventoryVolumeSchemeDetail.SchemeDetailID, InventoryVolumeSchemeMaster.IsPercentage " +
                            " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                                        " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                               " WHERE     (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND " +
                    " ((InventoryVolumeSchemeMaster.EffectiveStartDate<='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "') and (InventoryVolumeSchemeMaster.EndDate>='" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "' )) and (InventoryVolumeSchemeMaster.Active = 1) and ('" + Qty + "' between InventoryVolumeSchemeMaster.MinDiscountQty and InventoryVolumeSchemeMaster.MaxDiscountQty)" +
                    " ORDER BY InventoryVolumeSchemeMaster.SchemeID DESC ";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                adp2.Fill(ds2);

                double volumeDis = Convert.ToDouble(ds2.Tables[0].Rows[0]["SchemeDiscount"]);

                Double totalcount = Convert.ToDouble(lblvolume.Text);
                totalcount = totalcount + volumeDis;
                lblvolume.Text = Convert.ToString(totalcount);

                totalvolumedis = totalvolumedis + volumeDis;
                Session["VolumeSchemeDetailId"] = ds2.Tables[0].Rows[0]["SchemeDetailID"];
                Session["VolumeDiscountRate"] = ds2.Tables[0].Rows[0]["SchemeDiscount"];
                Session["VolumeDiscountIsPercent"] = ds2.Tables[0].Rows[0]["IsPercentage"];
                return volumeDis;
            }
        }
        else
        {
            Session["VolumeSchemeDetailId"] = "0";
            Session["VolumeDiscountRate"] = "0";
            Session["VolumeDiscountIsPercent"] = "0";
            lblvolume.Text = "0";
            totalvolumedis = 0;
            return 0;
        }
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtProdNo.Text = "";
        txtUnit.Text = " ";
        lblRate.Text = "0.00";
        lblBulkQty.Text = "0.00";
        lblQtyOnHand.Text = "0.000";
        // lblCustDisc.Text ="0.00";
        // lblBulkDisc.Text ="0.00";
        // lblBulkDisc.Text = "0.00";
        //lblCustDisc.Text = "0.00";
        // txtNetPrice.Text = "0.00";         
        if (ddlItem.SelectedIndex != 0)
        {
            ddlunit.DataSource = (DataSet)fillUnitType();
            ddlunit.DataTextField = "Name";
            ddlunit.DataValueField = "UnitTypeId";
            ddlunit.DataBind();



            string str = "SELECT     UnitTypeMaster.UnitTypeId, InventoryWarehouseMasterTbl.Weight as Unit, InventoryMaster.Name, " +
                         " InventoryMaster.ProductNo, InventoryMaster.InventoryMasterId,  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId," +
                         " InventoryWarehouseMasterTbl.WareHouseId FROM   " +
                         " InventoryWarehouseMasterTbl Inner join InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId" +
                         " Inner join  UnitTypeMaster on " +
                         " UnitTypeMaster.UnitTypeId =InventoryWarehouseMasterTbl.UnitTypeId " +

                         "  WHERE     (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId ='" + ddlItem.SelectedValue + "')";



            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Int32 InvWHId = Convert.ToInt32(ds.Tables[0].Rows[0]["InventoryWarehouseMasterId"].ToString());
            txtProdNo.Text = ds.Tables[0].Rows[0]["ProductNo"].ToString();
            txtUnit.Text = ds.Tables[0].Rows[0]["Unit"].ToString();
            if (ds.Tables[0].Rows[0]["UnitTypeId"].ToString() == null | ds.Tables[0].Rows[0]["UnitTypeId"].ToString() == "")
            {
            }
            else
            {
                ddlunit.SelectedValue = ds.Tables[0].Rows[0]["UnitTypeId"].ToString();
            }
            string str1 = " select * from InventoryWarehouseMasterTbl where InventoryWarehouseMasterId =" + InvWHId;


            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            adp1.Fill(ds1);

            if (ds1.Tables[0].Rows.Count > 0)
            {
                lblRate.Text = ds1.Tables[0].Rows[0]["Rate"].ToString();

                linkprice.Text = lblRate.Text;
            }
           

                double finaltotalqty = 0;
                if (rdinvoice.SelectedIndex == 0)
                {
                    DataTable drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + InvWHId + "' and DateUpdated<='" + txtGoodsDate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

                    if (drtinvdata.Rows.Count > 0)
                    {
                        lblavgRate.Text = Convert.ToString(drtinvdata.Rows[0]["AvgCost"]);

                        DataTable dton = select("SELECT Qty FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + InvWHId + "' and Tranction_Master_Id='" + ViewState["tid"] + "'");

                        if (dton.Rows.Count > 0)
                        {
                            lblQtyOnHand.Text = Convert.ToString(Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]) - Convert.ToDecimal(drtinvdata.Rows[0]["Qty"]));
                        }
                      
                        if (lblQtyOnHand.Text != "")
                        {
                            finaltotalqty = Convert.ToDouble(lblQtyOnHand.Text);
                        }
                    }
                }
                if (finaltotalqty > 0)
                {
                    lblQtyOnHand.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    lblQtyOnHand.ForeColor = System.Drawing.Color.Red;
                }
           
            if (lblRate.Text != "")
            {
                fillpromobulk(Convert.ToString(lblRate.Text), ddlItem.SelectedValue.ToString());
            }
        }

    }
    protected void fillpromobulk(string strrate, string strinvid)
    {
        if (strrate != "")
        {
            //
            double bulkprice = 0;
            double yourrate = 0;
            double promDisc = GetPromotionDiscount(1, Convert.ToDecimal(strrate), Convert.ToInt32(strinvid));
            double promprice = Convert.ToDouble(strrate) - promDisc;
            if (promprice > 0)
            {

                if (promprice == Convert.ToDouble(strrate))
                {
                    pnlpro.Visible = false;
                }
                else
                {
                    pnlpro.Visible = true;
                }
                if (rdinvoice.SelectedIndex == 0)
                {
                    bulkprice = GetBulkDiscount(Convert.ToInt32(strinvid), Convert.ToDecimal(promprice));
                }
                else
                {
                    bulkprice = 0;
                }
                if (bulkprice > 0)
                {
                    yourrate = GetPartyPrice(Convert.ToDecimal(promprice));
                    if (rdinvoice.SelectedIndex == 0)
                    {
                        pnlbulk.Visible   = true;
                    }
                }
                else
                {
                    yourrate = promprice;
                    
                        pnlbulk.Visible = false;
                   
                }

            }
            else
            {
                if (promprice == Convert.ToDouble(strrate))
                {
                    pnlpro.Visible = false;
                }
                else
                {
                    pnlpro.Visible = true;
                }
                if (rdinvoice.SelectedIndex == 0)
                {
                    bulkprice = GetBulkDiscount(Convert.ToInt32(strinvid), Convert.ToDecimal(strrate));
                }
                else
                {
                    bulkprice = 0;
                }
               
                if (bulkprice > 0)
                {
                    yourrate = GetPartyPrice(Convert.ToDecimal(strrate));
                    if (rdinvoice.SelectedIndex == 0)
                    {
                        pnlbulk.Visible = true;
                    }
                }
                else
                {
                    yourrate = Convert.ToDouble(strrate);
                   
                        pnlbulk.Visible = false;
                   
                }
            }


            lblPromoRate.Text = String.Format("{0:n}", promprice);
            lblBulkDisc.Text = String.Format("{0:n}", bulkprice);

            if (Convert.ToDouble(lblPromoRate.Text) < Convert.ToDouble(strrate))
            {

                //lblRate.Font.Strikeout = true;
            }
            else
            {
            }


            if (Convert.ToDouble(lblBulkDisc.Text) == 0.00)
            {
                if (Convert.ToDouble(lblPromoRate.Text) != 0.00)
                {
                    ////lblBulkDisc.Text = lblPromoRate.Text;
                    //if (Convert.ToDouble(lblPromoRate.Text) > Convert.ToDouble(strrate))
                    //{
                    //    lblBulkDisc.Text = lblPromoRate.Text;
                    //}
                    //else
                    //{
                    //    lblBulkDisc.Text = strrate;
                    //}
                   
                }
                else
                {
                    lblBulkDisc.Text = strrate;

                }
            }
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {

        string str = "   SELECT    InventoryWarehouseMasterTbl.Rate, UnitTypeMaster.UnitTypeId, InventoryMaster.Name, InventoryMaster.ProductNo, InventoryMaster.InventoryMasterId,  " +
                "  LEFT(InventoryCategoryMaster.InventoryCatName, 8) + '-' + LEFT(InventorySubCategoryMaster.InventorySubCatName, 8)  " +
                "  + '-' + LEFT(InventoruSubSubCategory.InventorySubSubName, 10) AS category, UnitTypeMaster.Name AS unitname, " +
                "  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.QtyOnHand,  " +
                "  InventoryWarehouseMasterTbl.Weight AS Unit " +
                 "  FROM   UnitTypeMaster inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.UnitTypeId= UnitTypeMaster.UnitTypeId  inner join " +

                "  InventoryMaster on  InventoryMaster.InventoryMasterId=InventoryWarehouseMasterTbl.InventoryMasterId inner join InventoryBarcodeMaster ON InventoryBarcodeMaster.InventoryMaster_id = InventoryMaster.InventoryMasterId inner join InventoruSubSubCategory  ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId inner join " +
                " InventorySubCategoryMaster ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryCategoryMaster on  InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId " +

                    " where (InventoryWarehouseMasterTbl.WareHouseId=" + Convert.ToInt32(ddlWarehouse.SelectedValue) + ") " + prodService + " " +
                    "   and (InventoryWarehouseMasterTbl.Active=1) and (InventoryMaster.MasterActiveStatus=1) and  (  (InventoryMaster.Name='" + txtsearch.Text + "') " +
                    "  or (InventoryMaster.ProductNo='" + txtsearch.Text + "') or (InventoryBarcodeMaster.Barcode='" + txtsearch.Text + "'))";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            // Panel1.Visible = true;
            // finalpanel.Visible = true;
            GridView2.Visible = true;
            GridView2.DataSource = ds;
            GridView2.DataBind();
            if (rdinvoice.SelectedIndex == 0)
            {
                fillqtyonhand();
            }
        }
        else
        {
            GridView2.Visible = true;
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
        if (rdinvoice.SelectedIndex == 0)
        {

            GridView2.Columns[4].Visible = true;
            GridView2.Columns[5].Visible = true;
            GridView2.Columns[7].Visible = true;
        }
        else
        {

            GridView2.Columns[4].Visible = false;
            GridView2.Columns[5].Visible = false;
            GridView2.Columns[7].Visible = false;
        }
    }
    protected void fillqtyonhand()
    {

        foreach (GridViewRow item in GridView2.Rows)
        {

            string invid = item.Cells[0].Text;
            Label lblqtyonhand = (Label)item.FindControl("lblqtyonhand");
            Label lblavgr = (Label)item.FindControl("lblavgr");

            DataTable drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + invid + "' and DateUpdated<='" + txtGoodsDate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

            if (drtinvdata.Rows.Count > 0)
            {
                lblavgr.Text = Convert.ToString(drtinvdata.Rows[0]["AvgCost"]);
                DataTable dton = select("SELECT Qty FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + invid + "' and Tranction_Master_Id='" + ViewState["tid"] + "'");

                if (dton.Rows.Count > 0)
                {
                    lblqtyonhand.Text = Convert.ToString(Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]) - Convert.ToDecimal(drtinvdata.Rows[0]["Qty"]));
                }
              
            }

        }

    }
  
  
    protected void BtnAdd1_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "If you have changed any quantities, click on the update button to view the changes.";
        //lblmsg1.Visible = false;
        lblqty.Visible = false;
        double qtuuser, qtyonhand;
        string val12;
        if (Convert.ToDouble(lblRate.Text) <= 0)
        {
            lblmsg1.Visible = true;
            lblmsg1.Text = "Rate is not available. Please check Item.";
            return;
        }
        try
        {
            qtuuser = Convert.ToDouble(txtQty.Text);
            val12 = lblQtyOnHand.Text;
            qtyonhand = Convert.ToDouble(val12);
            if (rdinvoice.SelectedIndex == 0)
            {
                if (qtuuser > qtyonhand)
                {
                    lblqty.Visible = true;
                    lblqty.Text = "Order Quantity must be less than Quantity on hand ";

                }
                else
                {
                    string para = " AND (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId ='" + Convert.ToInt32(ddlItem.SelectedValue.ToString()) + "') ";
                    if (rdinvoice.SelectedIndex == 0)
                    {
                        SalesInv(para, Convert.ToString(txtQty.Text), Convert.ToString(lblRate.Text));
                    }
                    else
                    {
                        SalesInvServive(para, Convert.ToString(txtQty.Text), Convert.ToString(lblRate.Text));
                    }
                }
            }
            else
            {
                string para = " AND (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId ='" + Convert.ToInt32(ddlItem.SelectedValue.ToString()) + "') ";

                if (rdinvoice.SelectedIndex == 0)
                {
                    SalesInv(para, Convert.ToString(txtQty.Text), Convert.ToString(lblRate.Text));
                }
                else
                {
                    SalesInvServive(para, Convert.ToString(txtQty.Text), Convert.ToString(lblRate.Text));
                }
            }

        }
        catch (Exception Ex)
        {
            lblmsg1.Visible = true;
            lblmsg1.Text = "Error : " + Ex.Message;
            //Response.Write(Ex.Message.ToString());
        }
    }
    protected void fillhide()
    {
        //if (GridView1.Rows.Count > 0)
        //{
        //    rdinvoice.Enabled = false;
        //}
        //else
        //{
        //    rdinvoice.Enabled = true;
        //}
    }
    protected void SalesInv(string invwid, string strqty, string strrate)
    {
        int flag = 0;
        double Discount;
        Discount = 0;
        DataTable dttt = new DataTable();
        DataTable dt = new DataTable();
        DataColumn dtcom = new DataColumn();
        dtcom.DataType = System.Type.GetType("System.String");
        dtcom.ColumnName = "ProductNo";
        dtcom.ReadOnly = false;
        dtcom.Unique = false;
        dtcom.AllowDBNull = true;

        dt.Columns.Add(dtcom);

        DataColumn dtcomid = new DataColumn();
        dtcomid.DataType = System.Type.GetType("System.String");
        dtcomid.ColumnName = "InventoryWarehouseMasterId";
        dtcomid.ReadOnly = false;
        dtcomid.Unique = false;
        dtcomid.AllowDBNull = true;

        dt.Columns.Add(dtcomid);

        DataColumn dtcom12 = new DataColumn();
        dtcom12.DataType = System.Type.GetType("System.String");
        dtcom12.ColumnName = "Name";
        dtcom12.ReadOnly = false;
        dtcom12.Unique = false;
        dtcom12.AllowDBNull = true;

        dt.Columns.Add(dtcom12);




        DataColumn dtcom121 = new DataColumn();
        dtcom121.DataType = System.Type.GetType("System.String");
        dtcom121.ColumnName = "Unit";
        dtcom121.ReadOnly = false;
        dtcom121.Unique = false;
        dtcom121.AllowDBNull = true;
        dt.Columns.Add(dtcom121);

        DataColumn dtcom1 = new DataColumn();
        dtcom1.DataType = System.Type.GetType("System.String");
        dtcom1.ColumnName = "UnitType";
        dtcom1.ReadOnly = false;
        dtcom1.Unique = false;
        dtcom1.AllowDBNull = true;
        dt.Columns.Add(dtcom1);




        DataColumn dtcom2 = new DataColumn();
        dtcom2.DataType = System.Type.GetType("System.String");
        dtcom2.ColumnName = "Qty";
        dtcom2.ReadOnly = false;
        dtcom2.Unique = false;
        dtcom2.AllowDBNull = true;
        dt.Columns.Add(dtcom2);


        DataColumn dtcom3 = new DataColumn();
        dtcom3.DataType = System.Type.GetType("System.String");
        dtcom3.ColumnName = "OderedQty";
        dtcom3.ReadOnly = false;
        dtcom3.Unique = false;
        dtcom3.AllowDBNull = true;
        dt.Columns.Add(dtcom3);



        DataColumn dtcom4 = new DataColumn();
        dtcom4.DataType = System.Type.GetType("System.String");
        dtcom4.ColumnName = "Note";
        dtcom4.ReadOnly = false;
        dtcom4.Unique = false;
        dtcom4.AllowDBNull = true;
        dt.Columns.Add(dtcom4);

        DataColumn dtcomUn = new DataColumn();
        dtcomUn.DataType = System.Type.GetType("System.String");
        dtcomUn.ColumnName = "UnshipQty";
        dtcomUn.ReadOnly = false;
        dtcomUn.Unique = false;
        dtcomUn.AllowDBNull = true;
        // dtcomUn.vi
        dt.Columns.Add(dtcomUn);


        DataColumn VolumeDis = new DataColumn();
        VolumeDis.DataType = System.Type.GetType("System.String");
        VolumeDis.ColumnName = "VolumeDis";
        VolumeDis.ReadOnly = false;
        VolumeDis.Unique = false;
        VolumeDis.AllowDBNull = true;
        // dtcomUn.vi
        dt.Columns.Add(VolumeDis);

        DataColumn PromoDis = new DataColumn();
        PromoDis.DataType = System.Type.GetType("System.String");
        PromoDis.ColumnName = "PromoDis";
        PromoDis.ReadOnly = false;
        PromoDis.Unique = false;
        PromoDis.AllowDBNull = true;
        // dtcomUn.vi
        dt.Columns.Add(PromoDis);



        DataColumn dtcomTotal = new DataColumn();
        dtcomTotal.DataType = System.Type.GetType("System.String");
        dtcomTotal.ColumnName = "Total";
        dtcomTotal.ReadOnly = false;
        dtcomTotal.Unique = false;
        dtcomTotal.AllowDBNull = true;
        // dtcomUn.vi
        dt.Columns.Add(dtcomTotal);

        DataColumn dtcom41 = new DataColumn();
        dtcom41.DataType = System.Type.GetType("System.String");
        dtcom41.ColumnName = "Rate";
        dtcom41.ReadOnly = false;
        dtcom41.Unique = false;
        dtcom41.AllowDBNull = true;
        dt.Columns.Add(dtcom41);

        DataColumn dtcom5 = new DataColumn();
        dtcom5.DataType = System.Type.GetType("System.String");
        dtcom5.ColumnName = "promoprice";
        dtcom5.ReadOnly = false;
        dtcom5.Unique = false;
        dtcom5.AllowDBNull = true;

        dt.Columns.Add(dtcom5);

        DataColumn dtcom6 = new DataColumn();
        dtcom6.DataType = System.Type.GetType("System.String");
        dtcom6.ColumnName = "bulkprice";
        dtcom6.ReadOnly = false;
        dtcom6.Unique = false;
        dtcom6.AllowDBNull = true;

        dt.Columns.Add(dtcom6);

        DataColumn dtcom7 = new DataColumn();
        dtcom7.DataType = System.Type.GetType("System.String");
        dtcom7.ColumnName = "yourprice";
        dtcom7.ReadOnly = false;
        dtcom7.Unique = false;
        dtcom7.AllowDBNull = true;

        dt.Columns.Add(dtcom7);

        
        DataColumn dtcom8 = new DataColumn();
        dtcom8.DataType = System.Type.GetType("System.String");
        dtcom8.ColumnName = "AppliedRate";
        dtcom8.ReadOnly = false;
        dtcom8.Unique = false;
        dtcom8.AllowDBNull = true;

        dt.Columns.Add(dtcom8);


         DataColumn Dto = new DataColumn();
         Dto.DataType = System.Type.GetType("System.String");
         Dto.ColumnName = "SODetailId";
         Dto.ReadOnly = false;
         Dto.Unique = false;
         Dto.AllowDBNull = true;

         dt.Columns.Add(Dto);

         DataColumn dtcom9 = new DataColumn();
         dtcom9.DataType = System.Type.GetType("System.String");
         dtcom9.ColumnName = "AvgRate";
         dtcom9.ReadOnly = false;
         dtcom9.Unique = false;
         dtcom9.AllowDBNull = true;

         dt.Columns.Add(dtcom9);


         DataColumn dtcom10 = new DataColumn();
         dtcom10.DataType = System.Type.GetType("System.String");
         dtcom10.ColumnName = "AvgCost";
         dtcom10.ReadOnly = false;
         dtcom10.Unique = false;
         dtcom10.AllowDBNull = true;

         dt.Columns.Add(dtcom10);


         DataColumn dtcom11 = new DataColumn();
         dtcom11.DataType = System.Type.GetType("System.String");
         dtcom11.ColumnName = "Markup";
         dtcom11.ReadOnly = false;
         dtcom11.Unique = false;
         dtcom11.AllowDBNull = true;

         dt.Columns.Add(dtcom11);


         DataColumn dtcom13 = new DataColumn();
         dtcom13.DataType = System.Type.GetType("System.String");
         dtcom13.ColumnName = "QtyonHand";
         dtcom13.ReadOnly = false;
         dtcom13.Unique = false;
         dtcom13.AllowDBNull = true;

         dt.Columns.Add(dtcom13);

        if (ViewState["dt"] != null)
        {
            dt = (DataTable)ViewState["dt"];

        }
        double rt;
        rt = 0;
        DataRow dtrow = dt.NewRow();

        string str = "SELECT  distinct  UnitTypeMaster.Name as  UnitType, InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.ProductNo, InventoryImgMaster.Thumbnail, InventoryWarehouseMasterTbl.Weight as Unit  " +
         "    ,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.Rate,  " +
         "    InventoryWarehouseMasterTbl.WareHouseId " +
         "  FROM         InventoryMaster Left join " +
         "    InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId Inner join " +

         "    InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId Inner join  UnitTypeMaster on  UnitTypeMaster.UnitTypeId =InventoryWarehouseMasterTbl.UnitTypeId " +
          "  WHERE     (InventoryWarehouseMasterTbl.Active = 1)   " + invwid + prodService;




        SqlCommand cmddd = new SqlCommand(str, con);
        SqlDataAdapter adppp = new SqlDataAdapter(cmddd);
        DataSet dsss = new DataSet();

        adppp.Fill(dsss);
        //
        if (dt.Rows.Count > 0)
        {

            foreach (DataRow dr in dt.Rows)
            {

                if (Convert.ToInt32(dr["InventoryWarehouseMasterId"]) == Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"].ToString()))
                {

                    if (Convert.ToString(strrate) != "")
                    {

                        fillpromobulk(Convert.ToString(strrate), Convert.ToString(dr["InventoryWarehouseMasterId"]));

                    }
                    if (rdinvoice.SelectedIndex == 0)
                    {
                        dr["bulkprice"] = lblBulkDisc.Text;
                    }
                    double promdis = GetPromotionDiscount(Convert.ToInt16(strqty), Convert.ToDecimal(strrate), Convert.ToInt32(dr["InventoryWarehouseMasterId"]));
                    double volumedis = GetVolumeDiscount(Convert.ToInt32(dr["InventoryWarehouseMasterId"]), Convert.ToInt16(strqty), Convert.ToDecimal(strrate));


                    dr["VolumeDis"] = "0";
                    dr["PromoDis"] = promdis.ToString();
                    dr["promoprice"] = lblPromoRate.Text;

                    double RateMain;
                    RateMain = 0;
                    if (lblBulkQty.Text == "0.00")
                    {

                        if (Convert.ToDouble(strrate) <= Convert.ToDouble(lblPromoRate.Text))
                        {
                            RateMain = Convert.ToDouble(strrate);
                        }
                        else
                        {
                            RateMain = Convert.ToDouble(lblPromoRate.Text);
                        }


                    }
                    else
                    {
                        if (Convert.ToDouble(lblBulkQty.Text) < Convert.ToDouble(strqty))
                        {
                            RateMain = Convert.ToDouble(lblBulkDisc.Text);
                            dr["VolumeDis"] = volumedis;
                        }
                        else
                        {
                            if (Convert.ToDouble(strrate) <= Convert.ToDouble(lblPromoRate.Text))
                            {
                                RateMain = Convert.ToDouble(strrate);
                            }
                            else
                            {
                                RateMain = Convert.ToDouble(lblPromoRate.Text);
                            }


                        }
                    }
                    dr["AppliedRate"] = Convert.ToDouble(RateMain);
                    dr["Total"] = Convert.ToDouble(strqty) * RateMain;
                    dr["Rate"] = strrate;
                    dr["yourprice"] = String.Format("{0:n}", "0.00");
                    dr["OderedQty"] = Convert.ToInt32(strqty);
                    dr["AvgRate"] = lblavgRate.Text;
                    dr["AvgCost"] = Convert.ToDouble(lblavgRate.Text) * Convert.ToDouble(strqty);
                    dr["Markup"] = (Convert.ToDouble(strqty) * RateMain) - (Convert.ToDouble(lblavgRate.Text) * Convert.ToDouble(strqty));
                    dr["QtyonHand"] = lblQtyOnHand.Text;
                    dt.AcceptChanges();
                    flag = 1;

                }
                else
                {
                    if (Convert.ToString(dr["Rate"]) != "")
                    {

                        fillpromobulk(Convert.ToString(dr["Rate"]), Convert.ToString(dr["InventoryWarehouseMasterId"]));

                    }

                }

            }
            if (flag == 0)
            {
                if (Convert.ToString(strrate) != "")
                {

                    fillpromobulk(Convert.ToString(strrate), Convert.ToString(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]));

                }
                double promdis = GetPromotionDiscount(Convert.ToInt16(strqty), Convert.ToDecimal(strrate), Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]));
                double volumedis = GetVolumeDiscount(Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]), Convert.ToInt16(strqty), Convert.ToDecimal(strrate));


                dtrow["VolumeDis"] = "0";
                dtrow["PromoDis"] = promdis;
                dtrow["ProductNo"] = dsss.Tables[0].Rows[0]["ProductNo"];
                dtrow["InventoryWarehouseMasterId"] = dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"];
                dtrow["Name"] = dsss.Tables[0].Rows[0]["Name"];
                dtrow["Unit"] = dsss.Tables[0].Rows[0]["Unit"];
                dtrow["UnitType"] = dsss.Tables[0].Rows[0]["UnitType"];
                dtrow["Qty"] = 0;
                dtrow["OderedQty"] = strqty;
                dtrow["Note"] = "";
                dtrow["UnshipQty"] = strqty;
                dtrow["Rate"] = strrate;

                dtrow["VolumeDis"] = Discount.ToString();
                dtrow["promoprice"] = lblPromoRate.Text;
                dtrow["bulkprice"] = lblBulkDisc.Text;
                double RateMain;
                RateMain = 0;
                if (lblBulkQty.Text == "0.00")
                {

                    if (Convert.ToDouble(strrate.ToString()) <= Convert.ToDouble(lblPromoRate.Text))
                    {
                        RateMain = Convert.ToDouble(strrate.ToString());
                    }
                    else
                    {
                        RateMain = Convert.ToDouble(lblPromoRate.Text);
                    }

                }
                else
                {
                    if (Convert.ToDouble(lblBulkQty.Text) < Convert.ToDouble(strqty))
                    {
                        RateMain = Convert.ToDouble(lblBulkDisc.Text);
                        dtrow["VolumeDis"] = volumedis;
                    }
                    else
                    {
                        if (Convert.ToDouble(strrate.ToString()) <= Convert.ToDouble(lblPromoRate.Text))
                        {
                            RateMain = Convert.ToDouble(strrate.ToString());
                        }
                        else
                        {
                            RateMain = Convert.ToDouble(lblPromoRate.Text);
                        }
                    }
                }
                dtrow["AppliedRate"] = Convert.ToDouble(RateMain);
                dtrow["Total"] = Convert.ToDouble(strqty) * RateMain;
                dtrow["AvgRate"] = lblavgRate.Text;
                dtrow["AvgCost"] = Convert.ToDouble(lblavgRate.Text) * Convert.ToDouble(strqty);
                dtrow["Markup"] = (Convert.ToDouble(strqty) * RateMain) - (Convert.ToDouble(lblavgRate.Text) * Convert.ToDouble(strqty));
                dtrow["QtyonHand"] = lblQtyOnHand.Text;
                dtrow["yourprice"] = String.Format("{0:n}", "0.00");


            }
        }
        else
        {


            if (Convert.ToString(strrate) != "")
            {
                if (Convert.ToString(strrate) != "")
                {

                    fillpromobulk(Convert.ToString(strrate), Convert.ToString(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]));

                }
            }
            /////ne
            double promdis = GetPromotionDiscount(Convert.ToInt16(strqty), Convert.ToDecimal(strrate), Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]));
            double volumedis = GetVolumeDiscount(Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]), Convert.ToInt16(strqty), Convert.ToDecimal(strrate));


            dtrow["VolumeDis"] = "0";
            dtrow["PromoDis"] = promdis.ToString();
            dtrow["ProductNo"] = dsss.Tables[0].Rows[0]["ProductNo"];
            dtrow["InventoryWarehouseMasterId"] = dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"];
            dtrow["Name"] = dsss.Tables[0].Rows[0]["Name"];
            dtrow["Unit"] = dsss.Tables[0].Rows[0]["Unit"];
            dtrow["UnitType"] = dsss.Tables[0].Rows[0]["UnitType"];
            dtrow["Qty"] = 0;
            dtrow["OderedQty"] = strqty;
            dtrow["Note"] = "";
            dtrow["UnshipQty"] = strqty;

            if (dsss.Tables[0].Rows.Count > 0)
            {
                dtrow["Rate"] = strrate.ToString();

                if (Convert.ToString(strrate) != "")
                {
                    if (Convert.ToDouble(dtrow["Rate"].ToString()) > Convert.ToDouble(lblBulkDisc.Text))
                    {
                        rt = Convert.ToDouble(lblBulkDisc.Text);
                        dtrow["VolumeDis"] = volumedis;
                    }
                    else
                    {
                        rt = Convert.ToDouble(dtrow["Rate"].ToString());
                    }
                }
                //dtrow["Total"] = Convert.ToDouble(txtQty.Text) * rt;
            }
            else
            {
                dtrow["Total"] = "0";
            }
            dtrow["UnshipQty"] = strqty;


        }
        //


        if (flag == 0)
        {
            //if (Convert.ToString(strrate) != "")
            //{
            //    Discount = GetVolumeDiscount(Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"].ToString()), Convert.ToInt32(strqty), Convert.ToDecimal(strrate.ToString()));
            //}
            // dtrow["Total"] = Convert.ToDouble(dtrow["Total"]) - Convert.ToDouble(Discount);
            double promdis = GetPromotionDiscount(Convert.ToInt16(strqty), Convert.ToDecimal(strrate), Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]));
            double volumedis = GetVolumeDiscount(Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]), Convert.ToInt16(strqty), Convert.ToDecimal(strrate));


            dtrow["VolumeDis"] = "0";
            dtrow["PromoDis"] = promdis.ToString();
            dtrow["promoprice"] = lblPromoRate.Text;
            dtrow["bulkprice"] = lblBulkDisc.Text;
            double RateMain;
            RateMain = 0;
            if (lblBulkQty.Text == "0.00")
            {

                if (Convert.ToDouble(Convert.ToString(strrate)) <= Convert.ToDouble(lblPromoRate.Text))
                {
                    RateMain = Convert.ToDouble(strrate);
                }
                else
                {
                    RateMain = Convert.ToDouble(lblPromoRate.Text);
                }


            }
            else
            {
                if (Convert.ToDouble(lblBulkQty.Text) < Convert.ToDouble(strqty))
                {
                    RateMain = Convert.ToDouble(lblBulkDisc.Text);
                    dtrow["VolumeDis"] = volumedis;
                }
                else
                {
                    if (Convert.ToDouble(strrate.ToString()) <= Convert.ToDouble(lblPromoRate.Text))
                    {
                        RateMain = Convert.ToDouble(strrate.ToString());
                    }
                    else
                    {
                        RateMain = Convert.ToDouble(lblPromoRate.Text);
                    }


                }
            }
            dtrow["AppliedRate"] = Convert.ToDouble(RateMain);
            dtrow["Total"] = Convert.ToDouble(strqty) * RateMain;
            dtrow["AvgRate"] = lblavgRate.Text;
            dtrow["AvgCost"] = Convert.ToDouble(lblavgRate.Text) * Convert.ToDouble(strqty);
            dtrow["Markup"] = (Convert.ToDouble(strqty) * RateMain) - (Convert.ToDouble(lblavgRate.Text) * Convert.ToDouble(strqty));
            dtrow["QtyonHand"] = lblQtyOnHand.Text;
            dtrow["yourprice"] = String.Format("{0:n}", "0.00");
            dt.Rows.Add(dtrow);
        }
        else
        {
            dt.AcceptChanges();
        }
        lblmsg1.Text = "";
        //
        GridView1.Columns[8].Visible = true;
        GridView1.Columns[7].Visible = true;
        GridView1.DataSource = dt;

        GridView1.DataBind();
        filldydata();
        ViewState["dt"] = dt;
        filldata();
        pnlprod.Visible = true;

        








       
    }
    protected void SalesInvServive(string invwid, string strqty, string strrate)
    {
        int flag = 0;
        double Discount;
        Discount = 0;
        DataTable dttt = new DataTable();
        DataTable dt = new DataTable();
        DataColumn dtcom = new DataColumn();
        dtcom.DataType = System.Type.GetType("System.String");
        dtcom.ColumnName = "ProductNo";
        dtcom.ReadOnly = false;
        dtcom.Unique = false;
        dtcom.AllowDBNull = true;

        dt.Columns.Add(dtcom);

        DataColumn dtcomid = new DataColumn();
        dtcomid.DataType = System.Type.GetType("System.String");
        dtcomid.ColumnName = "InventoryWarehouseMasterId";
        dtcomid.ReadOnly = false;
        dtcomid.Unique = false;
        dtcomid.AllowDBNull = true;

        dt.Columns.Add(dtcomid);

        DataColumn dtcom12 = new DataColumn();
        dtcom12.DataType = System.Type.GetType("System.String");
        dtcom12.ColumnName = "Name";
        dtcom12.ReadOnly = false;
        dtcom12.Unique = false;
        dtcom12.AllowDBNull = true;

        dt.Columns.Add(dtcom12);




        DataColumn dtcom121 = new DataColumn();
        dtcom121.DataType = System.Type.GetType("System.String");
        dtcom121.ColumnName = "Unit";
        dtcom121.ReadOnly = false;
        dtcom121.Unique = false;
        dtcom121.AllowDBNull = true;
        dt.Columns.Add(dtcom121);

        DataColumn dtcom1 = new DataColumn();
        dtcom1.DataType = System.Type.GetType("System.String");
        dtcom1.ColumnName = "UnitType";
        dtcom1.ReadOnly = false;
        dtcom1.Unique = false;
        dtcom1.AllowDBNull = true;
        dt.Columns.Add(dtcom1);




        DataColumn dtcom2 = new DataColumn();
        dtcom2.DataType = System.Type.GetType("System.String");
        dtcom2.ColumnName = "Qty";
        dtcom2.ReadOnly = false;
        dtcom2.Unique = false;
        dtcom2.AllowDBNull = true;
        dt.Columns.Add(dtcom2);


        DataColumn dtcom3 = new DataColumn();
        dtcom3.DataType = System.Type.GetType("System.String");
        dtcom3.ColumnName = "OderedQty";
        dtcom3.ReadOnly = false;
        dtcom3.Unique = false;
        dtcom3.AllowDBNull = true;
        dt.Columns.Add(dtcom3);



        DataColumn dtcom4 = new DataColumn();
        dtcom4.DataType = System.Type.GetType("System.String");
        dtcom4.ColumnName = "Note";
        dtcom4.ReadOnly = false;
        dtcom4.Unique = false;
        dtcom4.AllowDBNull = true;
        dt.Columns.Add(dtcom4);

        DataColumn dtcomUn = new DataColumn();
        dtcomUn.DataType = System.Type.GetType("System.String");
        dtcomUn.ColumnName = "UnshipQty";
        dtcomUn.ReadOnly = false;
        dtcomUn.Unique = false;
        dtcomUn.AllowDBNull = true;
        // dtcomUn.vi
        dt.Columns.Add(dtcomUn);


        DataColumn VolumeDis = new DataColumn();
        VolumeDis.DataType = System.Type.GetType("System.String");
        VolumeDis.ColumnName = "VolumeDis";
        VolumeDis.ReadOnly = false;
        VolumeDis.Unique = false;
        VolumeDis.AllowDBNull = true;
        // dtcomUn.vi
        dt.Columns.Add(VolumeDis);

        DataColumn PromoDis = new DataColumn();
        PromoDis.DataType = System.Type.GetType("System.String");
        PromoDis.ColumnName = "PromoDis";
        PromoDis.ReadOnly = false;
        PromoDis.Unique = false;
        PromoDis.AllowDBNull = true;
        // dtcomUn.vi
        dt.Columns.Add(PromoDis);


        DataColumn dtcomTotal = new DataColumn();
        dtcomTotal.DataType = System.Type.GetType("System.String");
        dtcomTotal.ColumnName = "Total";
        dtcomTotal.ReadOnly = false;
        dtcomTotal.Unique = false;
        dtcomTotal.AllowDBNull = true;
        // dtcomUn.vi
        dt.Columns.Add(dtcomTotal);

        DataColumn dtcom41 = new DataColumn();
        dtcom41.DataType = System.Type.GetType("System.String");
        dtcom41.ColumnName = "Rate";
        dtcom41.ReadOnly = false;
        dtcom41.Unique = false;
        dtcom41.AllowDBNull = true;
        dt.Columns.Add(dtcom41);

        DataColumn dtcom5 = new DataColumn();
        dtcom5.DataType = System.Type.GetType("System.String");
        dtcom5.ColumnName = "promoprice";
        dtcom5.ReadOnly = false;
        dtcom5.Unique = false;
        dtcom5.AllowDBNull = true;

        dt.Columns.Add(dtcom5);

        DataColumn dtcom6 = new DataColumn();
        dtcom6.DataType = System.Type.GetType("System.String");
        dtcom6.ColumnName = "bulkprice";
        dtcom6.ReadOnly = false;
        dtcom6.Unique = false;
        dtcom6.AllowDBNull = true;

        dt.Columns.Add(dtcom6);

        DataColumn dtcom7 = new DataColumn();
        dtcom7.DataType = System.Type.GetType("System.String");
        dtcom7.ColumnName = "yourprice";
        dtcom7.ReadOnly = false;
        dtcom7.Unique = false;
        dtcom7.AllowDBNull = true;

        dt.Columns.Add(dtcom7);


        DataColumn dtcom8 = new DataColumn();
        dtcom8.DataType = System.Type.GetType("System.String");
        dtcom8.ColumnName = "AppliedRate";
        dtcom8.ReadOnly = false;
        dtcom8.Unique = false;
        dtcom8.AllowDBNull = true;

        dt.Columns.Add(dtcom8);

        if (ViewState["dt1"] != null)
        {
            dt = (DataTable)ViewState["dt1"];

        }
        double rt;
        rt = 0;
        DataRow dtrow = dt.NewRow();

        string str = "SELECT  distinct  UnitTypeMaster.Name as  UnitType, InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.ProductNo, InventoryImgMaster.Thumbnail, InventoryWarehouseMasterTbl.Weight as Unit  " +
         "    ,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.Rate,  " +
         "    InventoryWarehouseMasterTbl.WareHouseId " +
         "  FROM         InventoryMaster Left join " +
         "    InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId Inner join " +

         "    InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId Inner join  UnitTypeMaster on  UnitTypeMaster.UnitTypeId =InventoryWarehouseMasterTbl.UnitTypeId " +
          "  WHERE     (InventoryWarehouseMasterTbl.Active = 1)   " + invwid + prodService;




        SqlCommand cmddd = new SqlCommand(str, con);
        SqlDataAdapter adppp = new SqlDataAdapter(cmddd);
        DataSet dsss = new DataSet();

        adppp.Fill(dsss);
        //
        if (dt.Rows.Count > 0)
        {

            foreach (DataRow dr in dt.Rows)
            {

                if (Convert.ToInt32(dr["InventoryWarehouseMasterId"]) == Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"].ToString()))
                {

                    if (Convert.ToString(strrate) != "")
                    {

                        fillpromobulk(Convert.ToString(strrate), Convert.ToString(dr["InventoryWarehouseMasterId"]));

                    }
                    if (rdinvoice.SelectedIndex == 0)
                    {
                        dr["bulkprice"] = lblBulkDisc.Text;
                    }
                    double promdis = GetPromotionDiscount(Convert.ToInt16(strqty), Convert.ToDecimal(strrate), Convert.ToInt32(dr["InventoryWarehouseMasterId"]));
                    double volumedis = GetVolumeDiscount(Convert.ToInt32(dr["InventoryWarehouseMasterId"]), Convert.ToInt16(strqty), Convert.ToDecimal(strrate));


                    dr["VolumeDis"] = "0";
                    dr["PromoDis"] = promdis.ToString();
                    dr["promoprice"] = lblPromoRate.Text;

                    double RateMain;
                    RateMain = 0;
                    if (lblBulkQty.Text == "0.00")
                    {

                        if (Convert.ToDouble(strrate) <= Convert.ToDouble(lblPromoRate.Text))
                        {
                            RateMain = Convert.ToDouble(strrate);
                        }
                        else
                        {
                            RateMain = Convert.ToDouble(lblPromoRate.Text);
                        }


                    }
                    else
                    {
                        if (Convert.ToDouble(lblBulkQty.Text) < Convert.ToDouble(strqty))
                        {
                            RateMain = Convert.ToDouble(lblBulkDisc.Text);
                            dr["VolumeDis"] = volumedis;
                        }
                        else
                        {
                            if (Convert.ToDouble(strrate) <= Convert.ToDouble(lblPromoRate.Text))
                            {
                                RateMain = Convert.ToDouble(strrate);
                            }
                            else
                            {
                                RateMain = Convert.ToDouble(lblPromoRate.Text);
                            }


                        }
                    }
                    dr["AppliedRate"] = Convert.ToDouble(RateMain);
                    dr["Total"] = Convert.ToDouble(strqty) * RateMain;
                    dr["Rate"] = strrate;
                    dr["yourprice"] = String.Format("{0:n}", "0.00");
                    dr["OderedQty"] = Convert.ToInt32(strqty);
                    dt.AcceptChanges();
                    flag = 1;

                }
                else
                {
                    if (Convert.ToString(dr["Rate"]) != "")
                    {

                        fillpromobulk(Convert.ToString(dr["Rate"]), Convert.ToString(dr["InventoryWarehouseMasterId"]));

                    }

                }

            }
            if (flag == 0)
            {
                if (Convert.ToString(strrate) != "")
                {

                    fillpromobulk(Convert.ToString(strrate), Convert.ToString(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]));

                }
                double promdis = GetPromotionDiscount(Convert.ToInt16(strqty), Convert.ToDecimal(strrate), Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]));
                double volumedis = GetVolumeDiscount(Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]), Convert.ToInt16(strqty), Convert.ToDecimal(strrate));


                dtrow["VolumeDis"] = "0";
                dtrow["PromoDis"] = promdis;
                dtrow["ProductNo"] = dsss.Tables[0].Rows[0]["ProductNo"];
                dtrow["InventoryWarehouseMasterId"] = dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"];
                dtrow["Name"] = dsss.Tables[0].Rows[0]["Name"];
                dtrow["Unit"] = dsss.Tables[0].Rows[0]["Unit"];
                dtrow["UnitType"] = dsss.Tables[0].Rows[0]["UnitType"];
                dtrow["Qty"] = 0;
                dtrow["OderedQty"] = strqty;
                dtrow["Note"] = "";
                dtrow["UnshipQty"] = strqty;
                dtrow["Rate"] = strrate;

                dtrow["VolumeDis"] = Discount.ToString();
                dtrow["promoprice"] = lblPromoRate.Text;
                dtrow["bulkprice"] = lblBulkDisc.Text;
                double RateMain;
                RateMain = 0;
                if (lblBulkQty.Text == "0.00")
                {

                    if (Convert.ToDouble(strrate.ToString()) <= Convert.ToDouble(lblPromoRate.Text))
                    {
                        RateMain = Convert.ToDouble(strrate.ToString());
                    }
                    else
                    {
                        RateMain = Convert.ToDouble(lblPromoRate.Text);
                    }

                }
                else
                {
                    if (Convert.ToDouble(lblBulkQty.Text) < Convert.ToDouble(strqty))
                    {
                        RateMain = Convert.ToDouble(lblBulkDisc.Text);
                        dtrow["VolumeDis"] = volumedis;
                    }
                    else
                    {
                        if (Convert.ToDouble(strrate.ToString()) <= Convert.ToDouble(lblPromoRate.Text))
                        {
                            RateMain = Convert.ToDouble(strrate.ToString());
                        }
                        else
                        {
                            RateMain = Convert.ToDouble(lblPromoRate.Text);
                        }
                    }
                }
                dtrow["AppliedRate"] = Convert.ToDouble(RateMain);
                dtrow["Total"] = Convert.ToDouble(strqty) * RateMain;

                dtrow["yourprice"] = String.Format("{0:n}", "0.00");


            }
        }
        else
        {


            if (Convert.ToString(strrate) != "")
            {
                if (Convert.ToString(strrate) != "")
                {

                    fillpromobulk(Convert.ToString(strrate), Convert.ToString(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]));

                }
            }
            /////ne
            double promdis = GetPromotionDiscount(Convert.ToInt16(strqty), Convert.ToDecimal(strrate), Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]));
            double volumedis = GetVolumeDiscount(Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]), Convert.ToInt16(strqty), Convert.ToDecimal(strrate));


            dtrow["VolumeDis"] = "0";
            dtrow["PromoDis"] = promdis.ToString();
            dtrow["ProductNo"] = dsss.Tables[0].Rows[0]["ProductNo"];
            dtrow["InventoryWarehouseMasterId"] = dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"];
            dtrow["Name"] = dsss.Tables[0].Rows[0]["Name"];
            dtrow["Unit"] = dsss.Tables[0].Rows[0]["Unit"];
            dtrow["UnitType"] = dsss.Tables[0].Rows[0]["UnitType"];
            dtrow["Qty"] = 0;
            dtrow["OderedQty"] = strqty;
            dtrow["Note"] = "";
            dtrow["UnshipQty"] = strqty;

            if (dsss.Tables[0].Rows.Count > 0)
            {
                dtrow["Rate"] = strrate.ToString();

                if (Convert.ToString(strrate) != "")
                {
                    if (Convert.ToDouble(dtrow["Rate"].ToString()) > Convert.ToDouble(lblBulkDisc.Text))
                    {
                        rt = Convert.ToDouble(lblBulkDisc.Text);
                        dtrow["VolumeDis"] = volumedis;
                    }
                    else
                    {
                        rt = Convert.ToDouble(dtrow["Rate"].ToString());
                    }
                }
                //dtrow["Total"] = Convert.ToDouble(txtQty.Text) * rt;
            }
            else
            {
                dtrow["Total"] = "0";
            }
            dtrow["UnshipQty"] = strqty;


        }
        //


        if (flag == 0)
        {
            //if (Convert.ToString(strrate) != "")
            //{
            //    Discount = GetVolumeDiscount(Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"].ToString()), Convert.ToInt32(strqty), Convert.ToDecimal(strrate.ToString()));
            //}
            // dtrow["Total"] = Convert.ToDouble(dtrow["Total"]) - Convert.ToDouble(Discount);
            double promdis = GetPromotionDiscount(Convert.ToInt16(strqty), Convert.ToDecimal(strrate), Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]));
            double volumedis = GetVolumeDiscount(Convert.ToInt32(dsss.Tables[0].Rows[0]["InventoryWarehouseMasterId"]), Convert.ToInt16(strqty), Convert.ToDecimal(strrate));


            dtrow["VolumeDis"] = "0";
            dtrow["PromoDis"] = promdis.ToString();
            dtrow["promoprice"] = lblPromoRate.Text;
            dtrow["bulkprice"] = lblBulkDisc.Text;
            double RateMain;
            RateMain = 0;
            if (lblBulkQty.Text == "0.00")
            {

                if (Convert.ToDouble(Convert.ToString(strrate)) <= Convert.ToDouble(lblPromoRate.Text))
                {
                    RateMain = Convert.ToDouble(strrate);
                }
                else
                {
                    RateMain = Convert.ToDouble(lblPromoRate.Text);
                }


            }
            else
            {
                if (Convert.ToDouble(lblBulkQty.Text) < Convert.ToDouble(strqty))
                {
                    RateMain = Convert.ToDouble(lblBulkDisc.Text);
                    dtrow["VolumeDis"] = volumedis;
                }
                else
                {
                    if (Convert.ToDouble(strrate.ToString()) <= Convert.ToDouble(lblPromoRate.Text))
                    {
                        RateMain = Convert.ToDouble(strrate.ToString());
                    }
                    else
                    {
                        RateMain = Convert.ToDouble(lblPromoRate.Text);
                    }


                }
            }
            dtrow["AppliedRate"] = Convert.ToDouble(RateMain);
            dtrow["Total"] = Convert.ToDouble(strqty) * RateMain;

            dtrow["yourprice"] = String.Format("{0:n}", "0.00");
            dt.Rows.Add(dtrow);
        }
        else
        {
            dt.AcceptChanges();
        }
        lblmsg1.Text = "";
        //

        GridView3.Columns[7].Visible = true;
        GridView3.DataSource = dt;

        GridView3.DataBind();
        int cc = 0;


       
        ViewState["dt1"] = dt;
        filldata();
        pnlserv.Visible = true;
    }
    protected void filldata()
    {
        fillhide();
        GridView1.Visible = true;
        //gridtax();
        btnUpdate.Visible = true;
        lblMsg.Visible = true;
        double total;
        total = 0;
        double Gtotal;
        Gtotal = 0;

        foreach (GridViewRow dr in GridView1.Rows)
        {
            Label lbltot = (Label)dr.FindControl("lbltot");
            TextBox TextBox4 = (TextBox)dr.FindControl("TextBox4");
            Gtotal = Convert.ToDouble(Gtotal) + Convert.ToDouble(Convert.ToDouble(dr.Cells[6].Text.ToString()) * Convert.ToDouble(TextBox4.Text));
            total = Convert.ToDouble(total) + Convert.ToDouble(lbltot.Text);

        }
        foreach (GridViewRow dr in GridView3.Rows)
        {
            Label lbltot = (Label)dr.FindControl("lbltot");
            TextBox TextBox4 = (TextBox)dr.FindControl("TextBox4");
            Gtotal = Convert.ToDouble(Gtotal) + Convert.ToDouble(Convert.ToDouble(dr.Cells[6].Text.ToString()) * Convert.ToDouble(TextBox4.Text));
            total = Convert.ToDouble(total) + Convert.ToDouble(lbltot.Text);

        }

        lblGTotal.Text = String.Format("{0:0.00}", Gtotal);

        txtQty.Text = "1";

        DateTime saledate = Convert.ToDateTime(txtGoodsDate.Text);
        lblorderdiscname.Text = "";
        string fetchdiscount = "select * from OrderValueDiscountMaster where StartDate<='" + saledate + "' and EndDate>='" + saledate + "' and Active=1 and Whid='" + ddlWarehouse.SelectedValue + "' and ApplyRetailSales='1' order by OrderValueDiscountMasterId Desc";

        //and   ('" + saledate + "' >= StartDate and '" + saledate + "' <= EndDate)";
        DataSet ds1 = new DataSet();
        SqlDataAdapter ad1 = new SqlDataAdapter(fetchdiscount, con);
        ad1.Fill(ds1);

        if (ds1.Tables[0].Rows.Count > 0)
        {
            ViewState["OrderDesc"] = ds1.Tables[0].Rows[0]["OrderValueDiscountMasterId"].ToString();
            DateTime startdate = Convert.ToDateTime(ds1.Tables[0].Rows[0]["StartDate"].ToString());
            DateTime enddate = Convert.ToDateTime(ds1.Tables[0].Rows[0]["EndDate"].ToString());
            if (saledate >= startdate && saledate <= enddate)
            {

                double minvalu = Convert.ToDouble(ds1.Tables[0].Rows[0]["MinValue"].ToString());
                double maxvalu = Convert.ToDouble(ds1.Tables[0].Rows[0]["MaxValue"].ToString());
                double discount = Convert.ToDouble(ds1.Tables[0].Rows[0]["ValueDiscount"].ToString());
                string checkper = Convert.ToString(ds1.Tables[0].Rows[0]["IsPercentage"].ToString());
                if (Gtotal >= minvalu && Gtotal <= maxvalu)
                {
                    if (checkper == "False")
                    {
                        lblOrderDisc.Text = Convert.ToString(discount);
                        lblorderdiscname.Text = Convert.ToString(ds1.Tables[0].Rows[0]["SchemeName"]) + "    $" + discount;
                    }
                    else
                    {
                        lblOrderDisc.Text = Convert.ToString(((Gtotal) * (discount)) / 100);
                        lblorderdiscname.Text = Convert.ToString(ds1.Tables[0].Rows[0]["SchemeName"]) + "     " + discount + "%";
                    }
                }
                else
                {
                    lblOrderDisc.Text = "0.00";
                }

            }
            else
            {

                lblOrderDisc.Text = "0.00";

            }
        }
        else
        {
            lblOrderDisc.Text = "0.00";
        }

        if (Gtotal.ToString() == total.ToString())
        {
            lblCustDisc.Text = "0.00";
        }

        CountCustomerDisc();


        double maint;
        maint = 0;
        lblCustDisc.Text = String.Format("{0:0.00}", Session["custdis"]);

        lblGTotal.Text = String.Format("{0:0.00}", Math.Round(total, 2).ToString());
        GridView1.Columns[11].Visible = false;
        GridView1.Columns[12].Visible = false;
        GridView1.Columns[10].Visible = false;
        if (Convert.ToString(ViewState["type"]) == "1" || Convert.ToString(ViewState["type"]) == "2")
        {

            ctax();
        }
        else
        {
            gridtax();
        }

        double sustotal = Convert.ToDouble(lblCustDisc.Text) + Convert.ToDouble(lblOrderDisc.Text);
        double nett = Convert.ToDouble(Convert.ToDouble(lblGTotal.Text) - sustotal);
        lblnettot.Text = String.Format("{0:0.00}", Math.Round(nett, 2).ToString());
        maint = Convert.ToDouble(Convert.ToDouble(lblGTotal.Text) - sustotal) + Convert.ToDouble(lblTax.Text);

        fillfoter();
        lblTotal.Text = String.Format("{0:0.00}", Math.Round(maint, 2).ToString());

    }

    protected void filldydata()
    {
        int cc = 0;
        int cc1 = 0;
       
        foreach (GridViewRow ite in GridView1.Rows)
        {
            if (Convert.ToDouble(ite.Cells[6].Text) != Convert.ToDouble(ite.Cells[7].Text))
            {
                cc = 1;
            }
            if ((Convert.ToDouble(ite.Cells[6].Text) != Convert.ToDouble(ite.Cells[8].Text)) && (Convert.ToString(ite.Cells[8].Text) != "0.00") &&  (Convert.ToString(ite.Cells[8].Text) != "0"))
            {
                cc1 = 1;
            }
        }
       
        if (cc1 == 1)
        {
            GridView1.Columns[8].Visible = true;
        }
        else
        {
            GridView1.Columns[8].Visible = false;
        }
        if (cc == 1)
        {
            GridView1.Columns[7].Visible = true;

        }
        else
        {
            GridView1.Columns[7].Visible = false;

        }
        if (cc == 0 && cc1 == 0)
        {
            GridView1.Columns[9].Visible = false;
        }
        else
        {
            GridView1.Columns[9].Visible = true;
        }
        foreach (GridViewRow ite in GridView3.Rows)
        {
            if (Convert.ToDouble(ite.Cells[6].Text) != Convert.ToDouble(ite.Cells[7].Text))
            {
                cc = 1;
            }
            else
            {
                cc =0;
            }

        }

        if (cc == 1)
        {
            GridView3.Columns[7].Visible = true;

        }
        else
        {
            GridView3.Columns[7].Visible = false;

        }
        if (cc == 0)
        {
            GridView3.Columns[9].Visible = false;
        }
        else
        {
            GridView3.Columns[9].Visible = true;
        }
        fillhide();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add")
        {
            GridView2.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int dk = Convert.ToInt32(GridView2.DataKeys[GridView2.SelectedIndex].Value);
            TextBox txt = (TextBox)GridView2.SelectedRow.Cells[6].FindControl("txtQty");
            Label lblqtyonhand = (Label)GridView2.SelectedRow.Cells[6].FindControl("lblqtyonhand");
            TextBox txtactrate = (TextBox)GridView2.SelectedRow.FindControl("txtactrate");
            try
            {

                if (rdinvoice.SelectedIndex == 0)
                {
                    if (Convert.ToDecimal(txt.Text) > Convert.ToDecimal(lblqtyonhand.Text))
                    {
                        lblqty.Visible = true;
                        lblqty.Text = "Order Quantity must be less than Quantity on hand ";

                    }
                    else
                    {
                        lblqty.Text = "";
                        string para = " and  (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId ='" + Convert.ToInt32(dk) + "') ";
                        if (rdinvoice.SelectedIndex == 0)
                        {
                            SalesInv(para, Convert.ToString(txt.Text), Convert.ToString(txtactrate.Text));
                        }
                        else
                        {
                            SalesInvServive(para, Convert.ToString(txt.Text), Convert.ToString(txtactrate.Text));
                        }

                    }
                }
                else
                {
                    lblqty.Text = "";
                    string para = " and  (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId ='" + Convert.ToInt32(dk) + "') ";
                    if (rdinvoice.SelectedIndex == 0)
                    {
                        SalesInv(para, Convert.ToString(txt.Text), Convert.ToString(txtactrate.Text));
                    }
                    else
                    {
                        SalesInvServive(para, Convert.ToString(txt.Text), Convert.ToString(txtactrate.Text));
                    }

                }


            }

            catch (Exception Ex)
            {
                Label1.Visible = true;
                Label1.Text = "Error :" + Ex.Message;
                // Response.Write(Ex.Message.ToString());
            }

        }

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            try
            {
                TextBox txtSqty = (TextBox)gdr.Cells[7].FindControl("TextBox4");
                TextBox txtQtyDiff = (TextBox)gdr.Cells[8].FindControl("TextBox5");

                decimal i = 0;
                i = (-1) * (Convert.ToDecimal(gdr.Cells[6].Text) - Convert.ToDecimal(txtSqty.Text));
                txtQtyDiff.Text = i.ToString();
                btnSubmit.Visible = true;
            }
            catch (Exception ex)
            {
                lblmsg1.Text = "Error:" + ex.Message.ToString();
            }
        }
    }
    public void GenerateSalesInvoice(int salesOrderid)
    {
        double amountDec = 0;
        double invoiceAmt = 0;
        int trnid11;
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            Label lblinvwm = (Label)gdr.FindControl("lblinvwm");
            string str55 = "SELECT     Rate, Qty, SalesOrderMasterId, InventoryWHM_Id " +
                            " FROM         SalesOrderDetail " +
                        " WHERE     (SalesOrderMasterId = '" + Convert.ToInt32(salesOrderid) + "') AND (InventoryWHM_Id = '" + Convert.ToInt32(lblinvwm.Text) + "') ";
            SqlDataAdapter addpp = new SqlDataAdapter(str55, con);
            DataTable ddss = new DataTable();
            addpp.Fill(ddss);
            double rate = 0;
            if (ddss.Rows.Count > 0)
            {
                rate = Convert.ToDouble(ddss.Rows[0]["Rate"]);
            }

            TextBox txtShipQty = (TextBox)gdr.FindControl("TextBox4");

            //double temp = (Convert.ToDouble(gdr.Cells[6].Text) - Convert.ToDouble(txtShipQty.Text)) * rate;
            // amountDec = amountDec + temp;

        }


        SqlDataAdapter aadd = new SqlDataAdapter("SELECT     TransactionMasterMoreInfo.SalesOrderId, TranctionMaster.EntryTypeId, TranctionMaster.Tranction_Amount " +
                        " FROM         TransactionMasterMoreInfo INNER JOIN " +
                      " TranctionMaster ON TransactionMasterMoreInfo.Tranction_Master_Id = TranctionMaster.Tranction_Master_Id " +
                    " WHERE     (TransactionMasterMoreInfo.SalesOrderId ='" + Convert.ToInt32(salesOrderid) + "') AND (TranctionMaster.EntryTypeId = '26')", con);
        DataTable dss1 = new DataTable();
        aadd.Fill(dss1);

        foreach (DataRow drr in dss1.Rows)
        {
            amountDec = amountDec + Convert.ToDouble(drr["Tranction_Amount"]);
            invoiceAmt = Convert.ToDouble(drr["Tranction_Amount"]) - amountDec;

        }
        invoiceAmt = isdoubleornot(lblTotal.Text);

        ViewState["Usid"] = Session["userid"];


        string str123 = "Update  TranctionMaster Set Date='" + txtGoodsDate.Text + "',UserId='" + ddlsalespersion.SelectedValue + "',Tranction_Amount='" + invoiceAmt + "' where  Tranction_Master_Id='" + ViewState["tid"] + "'";
        SqlCommand cmd123 = new SqlCommand(str123, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd123.ExecuteNonQuery();
        con.Close();



        trnid11 = Convert.ToInt32(ViewState["tid"]);


       
       
        if (rdinvoice.SelectedIndex == 0)
        {                                   
            //string invda = "Delete from InventoryWarehouseMasterAvgCostTbl where Tranction_Master_Id='" + ViewState["tid"] + "'";

            //SqlCommand cminb = new SqlCommand(invda, con);
            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cminb.ExecuteNonQuery();
            //con.Close();
            string maininv = "";
            int ly = 0;
            double amtavgcost = 0;
            foreach (GridViewRow gdr11 in GridView1.Rows)
            {
                double FinalQtySub = 0;
                double FinalQty = 0;
             
                Label lblinvwm = (Label)gdr11.FindControl("lblinvwm");
                TextBox txtShipQty1 = (TextBox)gdr11.FindControl("TextBox4");
                FinalQtySub = Convert.ToDouble(txtShipQty1.Text);
                FinalQty = -(FinalQtySub);


                string invtype = "0";
                if (Convert.ToDateTime(ViewState["sdo"]) == Convert.ToDateTime(txtGoodsDate.Text))
                {
                    invtype = "1";
                    ViewState["sdo"] = txtGoodsDate.Text;
                }
                else if (Convert.ToDateTime(ViewState["sdo"]) >= Convert.ToDateTime(txtGoodsDate.Text))
                {
                    invtype = "2";
                    ViewState["sdo"] = txtGoodsDate.Text;
                }
                else
                {
                    invtype = "3";

                }
                if (invtype == "3" || invtype == "2")
                {
                    string updateavgcos = "Delete from  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + lblinvwm.Text + "' and Tranction_Master_Id='" + trnid11 + "'";
                    SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmavgcost.ExecuteNonQuery();
                    con.Close();
                    if (invtype == "3")
                    {
                        string upproc = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,DateUpdated,AvgCost,QtyonHand)values('" + lblinvwm.Text + "','" + trnid11 + "','" + FinalQty + "','" + txtGoodsDate.Text + "','0','0')";
                        SqlCommand cmdpro = new SqlCommand(upproc, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdpro.ExecuteNonQuery();
                        con.Close();
                    }
                }
                fillAVGCOST(trnid11.ToString(), lblinvwm.Text.ToString(),FinalQtySub.ToString(), invtype);

                if (ly > 0)
                {
                    maininv = maininv + ",";
                }
                else
                {
                    ly += 1;
                }

                maininv = maininv + lblinvwm.Text.ToString();
                DataTable datacugs = select("SELECT  QtyonHand,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + lblinvwm.Text + "' and Tranction_Master_Id='" + trnid11 + "' and DateUpdated='" + txtGoodsDate.Text + "'");
                if (datacugs.Rows.Count > 0)
                {
                    amtavgcost += Convert.ToDouble(datacugs.Rows[0]["AvgCost"]) * FinalQtySub;
                }
                amtavgcost = Math.Round(amtavgcost, 2);
            }


            ///// Add SUGS
         
            string accdebiinv = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                          " ,DateTimeOfTransaction,compid,whid )" +
                          " VALUES('8003','" + amtavgcost + "'" +
                          "  , '" + trnid11 + "','" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
            SqlCommand cdinvdeb = new SqlCommand(accdebiinv, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }

            cdinvdeb.ExecuteNonQuery();
            con.Close();
            string costgood = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                      " ,DateTimeOfTransaction,compid,whid)" +
                      " VALUES('8000','" + amtavgcost + "'" +
                      " ,'" + trnid11 + "','" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";

            SqlCommand cdcostgood = new SqlCommand(costgood, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cdcostgood.ExecuteNonQuery();
            con.Close();
            DELAVGCOST(maininv, trnid11.ToString());
                
        }




        //string strNewDCndInvRelation = "INSERT INTO TransactionMasterSalesChallanTbl  (TransactionMasterId, SalesChallanMasterId, " +
        //          " SalesOrderMasterId) VALUES     " +
        //          " ('" + trnid11 + "', '" + Convert.ToInt32(ViewState["DcNo1"]) + "', '" + Convert.ToInt32(salesOrderid) + "')";
        //SqlCommand cmMore1 = new SqlCommand(strNewDCndInvRelation, con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmMore1.ExecuteNonQuery();
        //con.Close();


        //*************
        string invtdet = "Delete from Tranction_Details where Tranction_Master_Id='" + ViewState["tid"] + "'";

        SqlCommand cminbdet = new SqlCommand(invtdet, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cminbdet.ExecuteNonQuery();
        con.Close();

        if (rbt_pay_method.SelectedValue == "4")
        {


            string straccid = "SELECT     Account, PartyID FROM Party_master where PartyID='" + ddlParty.SelectedValue + "' ";
            DataTable accid = dbss1.cmdSelect(straccid);
            if (accid.Rows.Count > 0)
            {
                int accid1 = Convert.ToInt32(accid.Rows[0]["Account"]);
                string str66 = "INSERT INTO Tranction_Details(AccountDebit, AccountCredit, AmountDebit, AmountCredit, Tranction_Master_Id, DateTimeOfTransaction,compid,whid) " +
                          " VALUES      ('" + accid1 + "' ,'0','" + Convert.ToDecimal(lblTotal.Text) + "','0','" + trnid11 + "','" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                SqlCommand cmd66 = new SqlCommand(str66, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd66.ExecuteNonQuery();
                con.Close();
            }
            // InsertTransactionDetail(dd

        }
        else
        {


            string str66 = "INSERT INTO Tranction_Details(AccountDebit, AccountCredit, AmountDebit, AmountCredit, Tranction_Master_Id, DateTimeOfTransaction,compid,whid) " +
                          " VALUES      ('" + ddlCash.SelectedValue + "' ,'0','" + Convert.ToDecimal(lblTotal.Text) + "','0','" + trnid11 + "','" + Convert.ToDateTime(txtGoodsDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
            SqlCommand cmd66 = new SqlCommand(str66, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd66.ExecuteNonQuery();
            con.Close();
        }
        //InsertTransactionDetail(0, 5000, 0, isdecimalornot(lblTotal.Text), trnid11, "", System.DateTime.Now, "0", " 0");
        //**********************
        InsertTransactionDetail(0, 5000, 0, isdecimalornot(lblGTotal.Text), trnid11, "", Convert.ToDateTime(txtGoodsDate.Text), "0", " 0");
        // InsertTransactionDetail(6000, 0, isdecimalornot(lblCustDisc.Text), 0, trnid11, "", System.DateTime.Now, "0", " 0");

        CountCustomerDisc();
        string custdis1 = Session["custdis"].ToString();
        if (isdecimalornot(lblCustDisc.Text) > 0)
        {
            InsertTransactionDetail(6000, 0, isdecimalornot(lblCustDisc.Text), 0, trnid11, "", Convert.ToDateTime(txtGoodsDate.Text), "0", " 0");
        }
        //InsertTransactionDetail(6000, 0, isdecimalornot(ltax.Text), 0, trnid11, "", System.DateTime.Now, "0", " 0");
        if (isdecimalornot(lblOrderDisc.Text) > 0)
        {
            InsertTransactionDetail(6001, 0, isdecimalornot(lblOrderDisc.Text), 0, trnid11, " ", Convert.ToDateTime(txtGoodsDate.Text), "0", " 0");
        }
        //InsertTransactionDetail(0, 3000, 0, isdecimalornot(lblTax.Text), trnid11, "", System.DateTime.Now, "0", " 0");
        //if (isdecimalornot(lblvolume.Text) > 0)
        //{
        //    InsertTransactionDetail(6003, 0, isdecimalornot(lblvolume.Text), 0, trnid11, "", System.DateTime.Now, "0", " 0");
        //}
        //if (isdecimalornot(lblpromo.Text) > 0)
        //{
        //    InsertTransactionDetail(6004, 0, isdecimalornot(lblpromo.Text), 0, trnid11, "", System.DateTime.Now, "0", " 0");
        //}

        lblvolume.Text = "0";
        lblpromo.Text = "0";

        //SqlCommand cmMore = new SqlCommand("insert into TransactionMasterMoreInfo(SalesOrderId,Tranction_Master_Id) values('" + Convert.ToInt32(salesOrderid) + "','" + trnid11 + "')", con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmMore.ExecuteNonQuery();
        //con.Close();

       // rbt_pay_method.SelectedItem.Text == "Cash" || rbt_pay_method.SelectedItem.Text == "Dc/Cr(RealTime)"
        string invtdetts = "Delete from TranctionMasterSuppliment where Tranction_Master_Id='" + ViewState["tid"] + "'";

        SqlCommand cminbdetsp = new SqlCommand(invtdetts, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cminbdetsp.ExecuteNonQuery();
        con.Close();
        string istpaya = "Delete from PaymentApplicationTbl where TranMIdPayReceived='" + ViewState["tid"] + "' or TranMIdAmtApplied='" + ViewState["tid"] + "'";

        SqlCommand cmpayap = new SqlCommand(istpaya, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmpayap.ExecuteNonQuery();
        con.Close();

        //if (rbt_pay_method.SelectedValue == "3")
        //{
        //    SqlCommand cmm09 = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
        //                                           " VALUES('" + salesOrderid + "','" + System.DateTime.Now + "','14','chq up',  '" + trnid11 + "') ", con);
        //    if (con.State.ToString() != "Open")
        //    {
        //        con.Open();
        //    }
        //    cmm09.ExecuteNonQuery();
        //    con.Close();

        //}
        //else 
        if (rbt_pay_method.SelectedValue == "4")
        {
            string str345 = "INSERT INTO TranctionMasterSuppliment " +
                       " (Tranction_Master_Id,AmountDue ,Party_MasterId,GrnMaster_Id)" +
                        "VALUES           ('" + trnid11 + "','" + invoiceAmt + "' ,'" + ddlParty.SelectedValue + "','" + txtpayduedate.Text + "' ) ";
            SqlCommand cmMore34 = new SqlCommand(str345, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmMore34.ExecuteNonQuery();
            con.Close();
            string statusid = "";
            if (Convert.ToInt32(txtnumberofduedate.Text) <= 30)
            {
                statusid = "53";
            }
            else if (Convert.ToInt32(txtnumberofduedate.Text) >= 31 && Convert.ToInt32(txtnumberofduedate.Text) <= 60)
            {
                statusid = "55";
            }
            else if (Convert.ToInt32(txtnumberofduedate.Text) >= 61 && Convert.ToInt32(txtnumberofduedate.Text) <= 90)
            {
                statusid = "56";
            }
            else if (Convert.ToInt32(txtnumberofduedate.Text) >= 91 && Convert.ToInt32(txtnumberofduedate.Text) <= 120)
            {
                statusid = "57";
            }

            else if (Convert.ToInt32(txtnumberofduedate.Text) >= 121)
            {
                statusid = "58";
            }
            //SqlCommand cmm09x = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
            //                                                 " VALUES('" + salesOrderid + "','" + System.DateTime.Now + "','21','crd up',  '" + trnid11 + "') ", con);
            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmm09x.ExecuteNonQuery();
            //con.Close();
            SqlCommand cmm09 = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                                   " VALUES('" + salesOrderid + "','" + System.DateTime.Now + "','" + statusid + "','Retail invoice Amount due status',  '" + trnid11 + "') ", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmm09.ExecuteNonQuery();
            con.Close();
        }
        else
        {

            string str345 = "INSERT INTO TranctionMasterSuppliment " +
                       " (Tranction_Master_Id,AmountDue ,Party_MasterId,GrnMaster_Id)" +
                        "VALUES           ('" + trnid11 + "','0' ,'" + ddlParty.SelectedValue + "','" + txtpayduedate.Text + "' ) ";
            SqlCommand cmMore34 = new SqlCommand(str345, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmMore34.ExecuteNonQuery();
            con.Close();

             SqlCommand cmdinspay = new SqlCommand("Insert into PaymentApplicationTbl values('" + trnid11 + "','" + trnid11 + "','" + invoiceAmt.ToString() + "','" + System.DateTime.Now.ToShortDateString() + "','0','" + salesOrderid + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdinspay.ExecuteNonQuery();
            con.Close();
        }
        ViewState["invx"] = invoiceAmt.ToString();


        string mxid99834 = "Select * from PaymentApplicationTbl where SalesOrderId='" + Convert.ToInt32(salesOrderid) + "'";
        SqlCommand cmd9982 = new SqlCommand(mxid99834, con);
        SqlDataAdapter adp9982 = new SqlDataAdapter(cmd9982);
        DataTable dt9982 = new DataTable();
        adp9982.Fill(dt9982);

        if (dt9982.Rows.Count > 0)
        {

            SqlCommand cmm09 = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                                    " VALUES('" + salesOrderid + "','" + System.DateTime.Now + "','30','Retail invoice Entry',  '" + trnid11 + "') ", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmm09.ExecuteNonQuery();
            con.Close();

        }
        else
        {
            SqlCommand cmm09 = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                                             " VALUES('" + salesOrderid + "','" + System.DateTime.Now + "','28','Retail invoice Entry',  '" + trnid11 + "') ", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmm09.ExecuteNonQuery();
            con.Close();
        }


    }
    protected void fillAVGCOST(string traid, string invid, string recqty, string invtype)
    {
        decimal inwavgid = 0;
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
                string ABCD = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,DateUpdated,AvgCost,QtyonHand)values('" + id12 + "','" + traid + "','" +(Convert.ToDouble(recqty)*(-1)) + "','" + ViewState["sdo"] + "','0','0')";
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
             Totalavgcost = OLDavgcost;
             Newqtyonhand = oLDqtyONHAND - Convert.ToDecimal(recqty);
            
            if (invtype == "1")
            {
                updateavgcos = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + Newqtyonhand + "',AvgCost='" + Totalavgcost + "' where InvWMasterId='" + id12 + "' and Tranction_Master_Id='" + traid + "'";

            }
            else
            {
                updateavgcos = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,DateUpdated,AvgCost,QtyonHand)values('" + id12 + "','" + traid + "','" + (Convert.ToDouble(recqty)*(-1)) + "','" + ViewState["sdo"] + "','" + Totalavgcost + "','" + Newqtyonhand + "')";
            }
            SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);
         
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
                    //if (Convert.ToDecimal(itm["Tranction_Master_Id"]) > Convert.ToDecimal(traid))
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
    protected void DELAVGCOST(string maininv, string traid)
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

            Finalqtyhand = oLDqtyONHAND;
            Totalavgcost = OLDavgcost;
            Totalavgcost = Math.Round(Totalavgcost, 2);
            Newqtyonhand = oLDqtyONHAND;

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
    protected void btnPrintCustomer_Click(object sender, EventArgs e)
    {

        Fillprintcopy();
       

    }
    protected void ddlunit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlParty_SelectedIndexChanged(object sender, EventArgs e)
    {

        string str = " SELECT     Party_master.PartyID, Party_master.Account, Party_master.Compname, Party_master.Contactperson, Party_master.Address," + " Party_master.City, Party_master.State, " +
       " Party_master.Country, Party_master.Website, Party_master.GSTno, Party_master.Incometaxno, Party_master.Email,  Party_master.Phoneno, Party_master.DataopID, " +
                            " Party_master.PartyTypeId, Party_master.AssignedAccountManagerId, Party_master.AssignedRecevingDepartmentInchargeId, " +
                             " Party_master.AssignedPurchaseDepartmentInchargeId, Party_master.AssignedShippingDepartmentInchargeId, Party_master.AssignedSalesDepartmentIncharge,  " +
                             " Party_master.StatusMasterId, Party_master.Fax, Party_master.AccountnameID, CountryMaster.CountryName, CountryMaster.CountryId, " +
                             " StateMasterTbl.StateName " +
       " FROM         CountryMaster INNER JOIN " +
                             " StateMasterTbl ON CountryMaster.CountryId = StateMasterTbl.CountryId RIGHT OUTER JOIN " +
                             " Party_master ON StateMasterTbl.StateId = Party_master.State AND CountryMaster.CountryId = Party_master.Country " +
       "  WHERE     (Party_master.PartyID= '" + ddlParty.SelectedValue + "')";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["state"] = ds.Tables[0].Rows[0]["State"].ToString();
                CountCustomerDisc();
                //CountTax();
                lblCustDisc.Text = String.Format("{0:0.00}",Session["custdis"].ToString());
                double sustotal = Convert.ToDouble(lblCustDisc.Text) + Convert.ToDouble(lblOrderDisc.Text);
                double nett = Convert.ToDouble(Convert.ToDouble(lblGTotal.Text) - sustotal);
                lblnettot.Text = String.Format("{0:0.00}", Math.Round(nett,2).ToString());
              double  maint = Convert.ToDouble(Convert.ToDouble(lblGTotal.Text) - sustotal) + Convert.ToDouble(lblTax.Text);
              maint = Math.Round(maint, 2);
                //fillfoter();
                lblTotal.Text = String.Format("{0:0.00}", maint.ToString());
            }
        }
        BillAdd();


    }
    protected void rbt_pay_method_SelectedIndexChanged(object sender, EventArgs e)
    {     DataTable dra=select("Select * from PaymentMethodAccountTbl where  Whid='" + ddlWarehouse.SelectedValue+ "'");
           
        btncheque.Visible = false;
        if (rbt_pay_method.SelectedValue == "4")
        {
            //submit.Visible = true;
            //ImageButton1.Visible = false;
            //   panelopt.Visible = false;
            //sendmail("alkesh.dnk@gmail.com");
            ddlCash.Enabled = false;
            Panel1.Visible = false;
            pnlCash.Visible = false;
            pnlCredit.Visible = false;
             if (dra.Rows.Count > 0)
            {
                ddlCash.SelectedIndex = ddlCash.Items.IndexOf(ddlCash.Items.FindByValue(Convert.ToString(dra.Rows[0]["CreditAccId"])));
            }
        }
        else if (rbt_pay_method.SelectedValue == "2")
        {
            //   submit.Visible = true;

            ddlCash.Enabled = true;
            Panel1.Visible = false;
            pnlCash.Visible = false;
            pnlCredit.Visible = true;
            
            if (dra.Rows.Count > 0)
            {
                ddlCash.SelectedIndex = ddlCash.Items.IndexOf(ddlCash.Items.FindByValue(Convert.ToString(dra.Rows[0]["CreditCardAccId"])));
            }
        }
        else if (rbt_pay_method.SelectedValue == "3")
        {
            ddlCash.Enabled = true;
           // Panel1.Visible = true;
            pnlCash.Visible = true;
            pnlCredit.Visible = false;
            btncheque.Visible = false;
            Panel1.Visible = true;
            //  submit.Visible = false;
            //   ImageButton1.Visible = false;
            //   panelopt.Visible = false;
            if (dra.Rows.Count > 0)
            {
                ddlCash.SelectedIndex = ddlCash.Items.IndexOf(ddlCash.Items.FindByValue(Convert.ToString(dra.Rows[0]["ChequeAccId"])));
            }
        }
        else if (rbt_pay_method.SelectedValue == "5")
        {
            ddlCash.Enabled = true;
            Panel1.Visible = false;
            pnlCash.Visible = true;
            pnlCredit.Visible = false;
            if (dra.Rows.Count > 0)
            {
                ddlCash.SelectedIndex = ddlCash.Items.IndexOf(ddlCash.Items.FindByValue(Convert.ToString(dra.Rows[0]["CreditCardoffAccId"])));
            }
        }

        else if (rbt_pay_method.SelectedValue == "6")
        {
            ddlParty.Enabled = false;
            Panel1.Visible = false;
            pnlCash.Visible = true;
            pnlCredit.Visible = false;
            if (dra.Rows.Count > 0)
            {
                ddlCash.SelectedIndex = ddlCash.Items.IndexOf(ddlCash.Items.FindByValue(Convert.ToString(dra.Rows[0]["DcCrAccId"])));
            }
        }
        else if (rbt_pay_method.SelectedValue == "7")
        {

            // ddlParty.Enabled = false;
            Panel1.Visible = false;
            pnlCash.Visible = true;
            pnlCredit.Visible = false;
            if (dra.Rows.Count > 0)
            {
                ddlCash.SelectedIndex = ddlCash.Items.IndexOf(ddlCash.Items.FindByValue(Convert.ToString(dra.Rows[0]["CashAccId"])));
            }

        }
        else
        {
            Panel1.Visible = false;
            pnlCash.Visible = true;
            pnlCredit.Visible = false;
        }
        txtGoodsDate_TextChanged(sender, e);
    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand("SELECT StateId, StateName FROM  StateMasterTbl WHERE  (CountryId = '" + ddlcountry.SelectedValue + "')", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlstate.DataSource = ds;
        ddlstate.DataTextField = "StateName";
        ddlstate.DataValueField = "StateId";
        ddlstate.DataBind();
        ddlstate.Items.Insert(0, "-Select-");
    }
    protected void btn_submitCheque_Click(object sender, ImageClickEventArgs e)
    {

    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //updateSession();
        decimal ald = 0;
        decimal newt = 0;

        foreach (GridViewRow gdr in GridView1.Rows)
        {
            TextBox txt1 = new TextBox();
            TextBox txt2 = new TextBox();
            txt1 = (TextBox)gdr.Cells[5].FindControl("TextBox4");
            Label lblinvwm = (Label)gdr.FindControl("lblinvwm");
            Label lbltot = (Label)gdr.FindControl("lbltot");
            Int32 Invid = Convert.ToInt32(lblinvwm.Text);
            decimal appl = 0;
            if (Convert.ToString(gdr.Cells[9].Text) != "")
            {
                appl = Convert.ToDecimal(gdr.Cells[9].Text);
            }
            else
            {
                appl = Convert.ToDecimal(gdr.Cells[6].Text);
            }
            string amrtt = Convert.ToString(lbltot.Text);
            ald += Convert.ToDecimal(amrtt);
            decimal amtchange = Math.Round(Convert.ToDecimal(txt1.Text) * appl, 2);
            lbltot.Text = amtchange.ToString();
            newt += amtchange;
        }
        foreach (GridViewRow gdr in GridView3.Rows)
        {
            TextBox txt1 = new TextBox();
            TextBox txt2 = new TextBox();
            txt1 = (TextBox)gdr.Cells[5].FindControl("TextBox4");
            Label lblinvwm = (Label)gdr.FindControl("lblinvwm");
            Label lbltot = (Label)gdr.FindControl("lbltot");
            Int32 Invid = Convert.ToInt32(lblinvwm.Text);

            decimal appl = Convert.ToDecimal(gdr.Cells[9].Text);
            string amrtt = Convert.ToString(lbltot.Text);
            ald += Convert.ToDecimal(amrtt);
            decimal amtchange = Math.Round(Convert.ToDecimal(txt1.Text) * appl, 2);
            lbltot.Text = amtchange.ToString();
            newt += amtchange;
        }
        if (Convert.ToString(ViewState["type"]) == "1" || Convert.ToString(ViewState["type"]) == "2")
        {

            ctax();
        }
        else
        {
            gridtax();
        }
        lblGTotal.Text = newt.ToString();
        lblMsg.Text = newt.ToString() + " RR " + ald.ToString();
        decimal deff = newt - ald;
        lblTotal.Text = Convert.ToString(Convert.ToDecimal(lblGTotal.Text) - Convert.ToDecimal(lblCustDisc.Text) - Convert.ToDecimal(lblOrderDisc.Text) + Convert.ToDecimal(lblTax.Text));
        lblnettot.Text = Convert.ToString(Convert.ToDecimal(lblGTotal.Text) - Convert.ToDecimal(lblCustDisc.Text) - Convert.ToDecimal(lblOrderDisc.Text));

        totalpartydis = 0;
        totalvolumedis = 0;
        totalpromtionaldis = 0;
        fillfoter();
        int fl = calca();
        //count cusomer



    }
    protected double isdoubleornot(string ck)
    {
        double ick = 0;
        try
        {
            ick = Convert.ToDouble(ck);
            return ick;
        }
        catch
        {

        }
        return ick;

    }
    public void CountTax()
    {
      
        ViewState["At1"] = "0";
        ViewState["At2"] = "0";
        ViewState["At3"] = "0";
       
        ViewState["Acid1"] = "";
        ViewState["Acid2"] = "";
        ViewState["Acid3"] = "";
        lbltaxName.Text = "Total Tax";
        lblTax.Text = String.Format("{0:0.00}", "0.0");

        pnltxt1.Visible = false;
        pnltxt2.Visible = false;
        pnltxt3.Visible = false;
        string taxtype = "select * from StorTaxmethodtbl where Storeid='" + ddlWarehouse.SelectedValue + "'";
        DataTable dttxt = dbss1.cmdSelect(taxtype);
        if (dttxt.Rows.Count > 0)
        {
            if (dttxt.Rows[0]["Fixedtaxforall"].ToString() == "True")
            {
                DataTable dttxmaster = select("select Top(3) Taxshortname,Name,TaxAccountMasterID,Percentage,Amount,TaxTypeMasterId as Id from [TaxTypeMaster]  where Active='1' and [Whid]='" + ddlWarehouse.SelectedValue + "' and ApplyAllsalesandretail='1'   order by TaxTypeMaster.TaxTypeMasterId Desc");



                if (dttxmaster.Rows.Count > 0)
                {
                    ViewState["type"] = "1";
                    lbltaxName.Text = "Total Tax";

                    if (dttxmaster.Rows.Count == 3)
                    {
                        pnltxt1.Visible = true;
                        pnltxt2.Visible = true;
                        pnltxt3.Visible = true;
                        double sb = Convert.ToDouble(lblGTotal.Text);
                        txt1.Text = dttxmaster.Rows[0]["Taxshortname"].ToString() + " Tax";
                        txt1rat.Text = dttxmaster.Rows[0]["Percentage"].ToString();
                        ViewState["Acc1"] = Convert.ToString(dttxmaster.Rows[0]["Id"]);
                        ViewState["Acid1"] = Convert.ToString(dttxmaster.Rows[0]["TaxAccountMasterID"]);
                        if (Convert.ToDecimal(dttxmaster.Rows[0]["Percentage"]) > 0)
                        {
                            ViewState["At1"] = "1";
                            txt1rat.Text = dttxmaster.Rows[0]["Percentage"].ToString() + "%";
                            txt1value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[0]["Percentage"]) / 100).ToString();
                        }
                        else
                        {
                            ViewState["At1"] = "0";
                            txt1rat.Text = "$" + dttxmaster.Rows[0]["Amount"].ToString();
                            txt1value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[0]["Amount"])).ToString();
                        }
                        txt2.Text = dttxmaster.Rows[1]["Taxshortname"].ToString() + " Tax";
                        txt2rat.Text = dttxmaster.Rows[1]["Percentage"].ToString();
                        ViewState["Acc2"] = Convert.ToString(dttxmaster.Rows[1]["Id"]);
                        ViewState["Acid2"] = Convert.ToString(dttxmaster.Rows[1]["TaxAccountMasterID"]);

                        if (Convert.ToDecimal(dttxmaster.Rows[1]["Percentage"]) > 0)
                        {
                            ViewState["At2"] = "1";
                            txt2rat.Text = dttxmaster.Rows[1]["Percentage"].ToString() + "%";
                            txt2value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[1]["Percentage"]) / 100).ToString();
                        }
                        else
                        {
                            ViewState["At2"] = "0";
                            txt2rat.Text = "$" + dttxmaster.Rows[1]["Amount"].ToString();
                            txt2value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[1]["Amount"])).ToString();
                        }
                        txt3.Text = dttxmaster.Rows[2]["Taxshortname"].ToString() + " Tax";
                        txt3rat.Text = dttxmaster.Rows[2]["Percentage"].ToString();
                        ViewState["Acc3"] = Convert.ToString(dttxmaster.Rows[2]["Id"]);
                        ViewState["Acid3"] = Convert.ToString(dttxmaster.Rows[2]["TaxAccountMasterID"]);

                        if (Convert.ToDecimal(dttxmaster.Rows[2]["Percentage"]) > 0)
                        {
                            ViewState["At3"] = "1";
                            txt3rat.Text = dttxmaster.Rows[2]["Percentage"].ToString() + "%";

                            txt3value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[2]["Percentage"]) / 100).ToString();
                        }
                        else
                        {
                            ViewState["At3"] = "0";
                            txt3rat.Text = "$" + dttxmaster.Rows[2]["Amount"].ToString();
                            txt3value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[2]["Amount"])).ToString();
                        }

                    }
                    else if (dttxmaster.Rows.Count == 2)
                    {
                        pnltxt1.Visible = true;
                        pnltxt2.Visible = true;
                        double sb = Convert.ToDouble(lblGTotal.Text);
                        txt1.Text = dttxmaster.Rows[0]["Taxshortname"].ToString() + " Tax";
                        txt1rat.Text = dttxmaster.Rows[0]["Percentage"].ToString();
                        ViewState["Acc1"] = Convert.ToString(dttxmaster.Rows[0]["Id"]);
                        ViewState["Acid1"] = Convert.ToString(dttxmaster.Rows[0]["TaxAccountMasterID"]);

                        if (Convert.ToDecimal(dttxmaster.Rows[0]["Percentage"]) > 0)
                        {
                            ViewState["At1"] = "1";
                            txt1rat.Text = dttxmaster.Rows[0]["Percentage"].ToString() + "%";

                            txt1value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[0]["Percentage"]) / 100).ToString();
                        }
                        else
                        {
                            ViewState["At1"] = "0";
                            txt1rat.Text = "$" + dttxmaster.Rows[0]["Amount"].ToString();
                            txt1value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[0]["Amount"])).ToString();
                        }
                        txt2.Text = dttxmaster.Rows[1]["Taxshortname"].ToString() + " Tax";
                        txt2rat.Text = dttxmaster.Rows[1]["Percentage"].ToString();
                        ViewState["Acc2"] = Convert.ToString(dttxmaster.Rows[1]["Id"]);
                        ViewState["Acid2"] = Convert.ToString(dttxmaster.Rows[1]["TaxAccountMasterID"]);

                        if (Convert.ToDecimal(dttxmaster.Rows[1]["Percentage"]) > 0)
                        {
                            ViewState["At2"] = "1";
                            txt2rat.Text = dttxmaster.Rows[1]["Percentage"].ToString() + "%";

                            txt2value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[1]["Percentage"]) / 100).ToString();
                        }
                        else
                        {
                            ViewState["At2"] = "0";
                            txt2rat.Text = "$" + dttxmaster.Rows[2]["Amount"].ToString();
                            txt2value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[1]["Amount"])).ToString();
                        }
                        pnltxt3.Visible = false;

                    }
                    else if (dttxmaster.Rows.Count == 1)
                    {
                        pnltxt1.Visible = true;
                        txt1.Text = dttxmaster.Rows[0]["Taxshortname"].ToString() + " Tax";
                        txt1rat.Text = dttxmaster.Rows[0]["Percentage"].ToString();
                        ViewState["Acc1"] = Convert.ToString(dttxmaster.Rows[0]["Id"]);
                        ViewState["Acid1"] = Convert.ToString(dttxmaster.Rows[0]["TaxAccountMasterID"]);

                        double sb = Convert.ToDouble(lblGTotal.Text);
                        if (Convert.ToDecimal(dttxmaster.Rows[0]["Percentage"]) > 0)
                        {
                            ViewState["At1"] = "1";
                            txt1rat.Text = dttxmaster.Rows[0]["Percentage"].ToString() + "%";

                            txt1value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[0]["Percentage"]) / 100).ToString();
                        }
                        else
                        {
                            ViewState["At1"] = "0";
                            txt1rat.Text = "$" + dttxmaster.Rows[0]["Amount"].ToString();
                            txt1value.Text = (Convert.ToDouble(lblGTotal.Text) + Convert.ToDouble(dttxmaster.Rows[0]["Amount"])).ToString();
                        }
                        pnltxt2.Visible = false;
                        pnltxt3.Visible = false;

                    }


                }
            }
            else if (dttxt.Rows[0]["Fixedtaxdependingonstate"].ToString() == "True")
            {
                ViewState["type"] = "2";
                DataTable dtstate = select("Select State,Country,StateName,CountryName from CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId  inner join WarehouseMaster on WarehouseMaster.WarehouseId=CompanyWebsitMaster.Whid where CompanyWebsitMaster.Whid='" + ddlWarehouse.SelectedValue + "'");
                if (dtstate.Rows.Count > 0)
                {


                    DataTable dttxmaster = select("select Top(3) Taxshortname,Id,TaxMAccountMasterID,TaxName,TaxAmt as Amount,  TaxAmtInPerc as Percentage from [TaxTypeMasterMoreInfo]  where Active='1'  and [ApplyToAllSales]='1' and [Whid]='" + ddlWarehouse.SelectedValue + "' and ([TaxTypeMasterMoreInfo].[StateId]='" + Convert.ToString(dtstate.Rows[0]["State"]) + "' or [TaxTypeMasterMoreInfo].[StateId]='0')   order by TaxTypeMasterMoreInfo.Id Desc");

                    if (dttxmaster.Rows.Count > 0)
                    {

                        lbltaxName.Text = "Total Tax";

                        if (dttxmaster.Rows.Count == 3)
                        {
                            pnltxt1.Visible = true;
                            pnltxt2.Visible = true;
                            pnltxt3.Visible = true;
                            double sb = Convert.ToDouble(lblGTotal.Text);
                            txt1.Text = dttxmaster.Rows[0]["Taxshortname"].ToString() + " Tax";
                            ViewState["Acc1"] = Convert.ToString(dttxmaster.Rows[0]["Id"]);
                            ViewState["Acid1"] = Convert.ToString(dttxmaster.Rows[0]["TaxMAccountMasterID"]);

                            if (Convert.ToString(dttxmaster.Rows[0]["Percentage"]) == "True")
                            {
                                ViewState["At1"] = "1";
                                txt1rat.Text = dttxmaster.Rows[0]["Amount"].ToString() + "%";
                                txt1value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[0]["Amount"]) / 100).ToString();
                            }
                            else
                            {
                                ViewState["At1"] = "0";
                                txt1rat.Text = "$" + dttxmaster.Rows[0]["Amount"].ToString();
                                txt1value.Text = (Convert.ToDouble(lblGTotal.Text) + Convert.ToDouble(dttxmaster.Rows[0]["Amount"])).ToString();
                            }
                            txt2.Text = dttxmaster.Rows[1]["Taxshortname"].ToString() + " Tax";
                            txt2rat.Text = dttxmaster.Rows[1]["Percentage"].ToString();
                            ViewState["Acc2"] = Convert.ToString(dttxmaster.Rows[1]["Id"]);
                            ViewState["Acid2"] = Convert.ToString(dttxmaster.Rows[1]["TaxMAccountMasterID"]);

                            if (Convert.ToString(dttxmaster.Rows[1]["Percentage"]) == "True")
                            {
                                ViewState["At2"] = "1";
                                txt2rat.Text = dttxmaster.Rows[1]["Amount"].ToString() + "%";
                                txt2value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[1]["Amount"]) / 100).ToString();
                            }
                            else
                            {
                                ViewState["At2"] = "0";
                                txt2rat.Text = "$" + dttxmaster.Rows[1]["Amount"].ToString();
                                txt2value.Text = (Convert.ToDouble(lblGTotal.Text) + Convert.ToDouble(dttxmaster.Rows[1]["Amount"])).ToString();
                            }
                            txt3.Text = dttxmaster.Rows[2]["Taxshortname"].ToString() + " Tax";
                            txt3rat.Text = dttxmaster.Rows[2]["Percentage"].ToString();
                            ViewState["Acc3"] = Convert.ToString(dttxmaster.Rows[2]["Id"]);
                            ViewState["Acid3"] = Convert.ToString(dttxmaster.Rows[2]["TaxMAccountMasterID"]);

                            if (Convert.ToString(dttxmaster.Rows[2]["Percentage"]) == "True")
                            {
                                ViewState["At3"] = "1";
                                txt3rat.Text = dttxmaster.Rows[2]["Amount"].ToString() + "%";
                                txt3value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[2]["Amount"]) / 100).ToString();
                            }
                            else
                            {
                                ViewState["At3"] = "0";
                                txt3rat.Text = "$" + dttxmaster.Rows[2]["Amount"].ToString();
                                txt3value.Text = (Convert.ToDouble(lblGTotal.Text) + Convert.ToDouble(dttxmaster.Rows[2]["Amount"])).ToString();
                            }
                        }
                        else if (dttxmaster.Rows.Count == 2)
                        {
                            pnltxt1.Visible = true;
                            pnltxt2.Visible = true;
                            double sb = Convert.ToDouble(lblGTotal.Text);
                            txt1.Text = dttxmaster.Rows[0]["Taxshortname"].ToString() + " Tax";
                            txt1rat.Text = dttxmaster.Rows[0]["Percentage"].ToString();
                            ViewState["Acc1"] = Convert.ToString(dttxmaster.Rows[0]["Id"]);
                            ViewState["Acid1"] = Convert.ToString(dttxmaster.Rows[0]["TaxMAccountMasterID"]);

                            if (Convert.ToString(dttxmaster.Rows[0]["Percentage"]) == "True")
                            {
                                ViewState["At1"] = "1";
                                txt1rat.Text = dttxmaster.Rows[0]["Amount"].ToString() + "%";
                                txt1value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[0]["Amount"]) / 100).ToString();
                            }
                            else
                            {
                                ViewState["At1"] = "0";
                                txt1rat.Text = "$" + dttxmaster.Rows[0]["Amount"].ToString();
                                txt1value.Text = (Convert.ToDouble(lblGTotal.Text) + Convert.ToDouble(dttxmaster.Rows[0]["Amount"])).ToString();
                            }
                            txt2.Text = dttxmaster.Rows[1]["Taxshortname"].ToString() + " Tax";
                            txt2rat.Text = dttxmaster.Rows[1]["Percentage"].ToString();
                            ViewState["Acc2"] = Convert.ToString(dttxmaster.Rows[1]["Id"]);
                            ViewState["Acid2"] = Convert.ToString(dttxmaster.Rows[1]["TaxMAccountMasterID"]);

                            if (Convert.ToString(dttxmaster.Rows[1]["Percentage"]) == "True")
                            {
                                ViewState["At2"] = "1";
                                txt2rat.Text = dttxmaster.Rows[1]["Amount"].ToString() + "%";
                                txt2value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[1]["Amount"]) / 100).ToString();
                            }
                            else
                            {
                                ViewState["At2"] = "0";
                                txt2rat.Text = "$" + dttxmaster.Rows[1]["Amount"].ToString();
                                txt2value.Text = (Convert.ToDouble(lblGTotal.Text) + Convert.ToDouble(dttxmaster.Rows[1]["Amount"])).ToString();
                            }
                            pnltxt3.Visible = false;
                            lblTax.Text = (Convert.ToDouble(txt1value.Text) + Convert.ToDouble(txt2value.Text)).ToString();

                        }
                        else if (dttxmaster.Rows.Count == 1)
                        {
                            ViewState["At1"] = "1";
                            pnltxt1.Visible = true;
                            txt1.Text = dttxmaster.Rows[0]["Taxshortname"].ToString() + " Tax";
                            txt1rat.Text = dttxmaster.Rows[0]["Percentage"].ToString();
                            ViewState["Acc1"] = Convert.ToString(dttxmaster.Rows[0]["Id"]);
                            ViewState["Acid1"] = Convert.ToString(dttxmaster.Rows[0]["TaxMAccountMasterID"]);

                            double sb = Convert.ToDouble(lblGTotal.Text);
                            if (Convert.ToString(dttxmaster.Rows[0]["Percentage"]) == "True")
                            {
                                ViewState["At1"] = "1";
                                txt1rat.Text = dttxmaster.Rows[0]["Percentage"].ToString() + "%";
                                txt1value.Text = (Convert.ToDouble(lblGTotal.Text) * Convert.ToDouble(dttxmaster.Rows[0]["Amount"]) / 100).ToString();
                            }
                            else
                            {
                                ViewState["At1"] = "0";
                                txt1rat.Text = "$" + dttxmaster.Rows[0]["Amount"].ToString();
                                txt1value.Text = (Convert.ToDouble(lblGTotal.Text) + Convert.ToDouble(dttxmaster.Rows[0]["Amount"])).ToString();
                            }
                            pnltxt2.Visible = false;
                            pnltxt3.Visible = false;

                        }


                    }


                }


            }
        }
        double totaltx = isdoubleornot(lblTax.Text);
        totaltx += Convert.ToDouble(txt1value.Text) + Convert.ToDouble(txt2value.Text) + Convert.ToDouble(txt3value.Text);
        lblTax.Text = String.Format("{0:0.00}", totaltx);

        Session["TaxName1"] = lbltaxName.Text;
    }
    public void CountCustomerDisc()
    {
        if (Session["userid"] != null)
        {
                Int32 partyid = Convert.ToInt32(ddlParty.SelectedValue);


            lblcusdisname.Text = "";
            string str = "SELECT     PartyTypeCategoryMasterTbl.IsPercentage,PartyCategoryName, PartyTypeCategoryMasterTbl.PartyCategoryDiscount, PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
                        " FROM         Party_master INNER JOIN " +
                         " PartyTypeDetailTbl ON Party_master.PartyID = PartyTypeDetailTbl.PartyID INNER JOIN " +
                          " User_master ON Party_master.PartyID = User_master.PartyID INNER JOIN " +
                         " PartyTypeCategoryMasterTbl ON PartyTypeDetailTbl.PartyTypeCategoryMasterId = PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
                " WHERE     (User_master.PartyID = " + partyid + ") AND (PartyTypeCategoryMasterTbl.Active = 1) " +
                " ORDER BY PartyTypeCategoryMasterTbl.EntryDate DESC ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Pcid"] = ds.Tables[0].Rows[0]["PartyTypeCategoryMasterId"].ToString();
                int i = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPercentage"]);

                if (i == 1)
                  
                {
                    lblcusdisname.Text = Convert.ToString(ds.Tables[0].Rows[0]["PartyCategoryName"])+"    " + ds.Tables[0].Rows[0]["PartyCategoryDiscount"]+"%";
               
                    double first = isdoubleornot(lblGTotal.Text);
                    double CustDis = (first * Convert.ToDouble(ds.Tables[0].Rows[0]["PartyCategoryDiscount"])) / 100;
                   
                    totalpartydis = totalpartydis + CustDis;
                    //  if (CustDis > 0)
                    {
                        Session["custdis"] = Math.Round( CustDis,2);

                    }
                    //else
                    {
                        // CustDis = 0.00;
                        Session["custdis"] = Math.Round(CustDis, 2);

                    }//lblCustDisc.Text = String.Format("{0:n}", CustDis);


                }
                else
                {
                  
                    double CustDis = Convert.ToDouble(ds.Tables[0].Rows[0]["PartyCategoryDiscount"]);
                      lblcusdisname.Text = Convert.ToString(ds.Tables[0].Rows[0]["PartyCategoryName"])+  "   $"+CustDis;
                    totalpartydis = totalpartydis + CustDis;

                    Session["custdis"] = Math.Round(CustDis, 2); ;
                    //lblCustDisc.Text = String.Format("{0:n}", CustDis);
                }

                Session["PatryTypeCategoryMasterId"] = ds.Tables[0].Rows[0]["PartyTypeCategoryMasterId"].ToString();
                Session["PartyDiscountRate"] = ds.Tables[0].Rows[0]["PartyCategoryDiscount"].ToString();
                Session["PartyDiscountIsPercent"] = ds.Tables[0].Rows[0]["IsPercentage"].ToString();
            }
            else
            {
                Session["custdis"] = 0;
                Session["PatryTypeCategoryMasterId"] = "0";
                Session["PartyDiscountRate"] = "0";
                Session["PartyDiscountIsPercent"] = 0;
            }

        }
    }
    protected void fillentno()
    {
        //DataTable dsrc = select("SELECT  max(EntryNumber) as entryno FROM TranctionMaster where EntryTypeId=30 and Whid='" + ddlWarehouse.SelectedValue + "' ");
        //if (Convert.ToString(dsrc.Rows[0]["entryno"])!="")
        //{
        //    lblinvoiceno.Text = Convert.ToString(Convert.ToInt32(dsrc.Rows[0]["entryno"]) + 1);
        //}
        //else
        //{
        //    lblinvoiceno.Text = "1";
        //}
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillentno();
        DataTable druse = select("select Username,UserID  from User_master inner join Party_master on Party_master.PartyID=User_master.PartyID where Party_master.Whid='" + ddlWarehouse.SelectedValue + "' Order by Username");
        ddlsalespersion.DataSource = druse;

        ddlsalespersion.DataTextField = "Username";
        ddlsalespersion.DataValueField = "UserID";
        ddlsalespersion.DataBind();
        ddlsalespersion.SelectedIndex = ddlsalespersion.Items.IndexOf(ddlsalespersion.Items.FindByValue(Session["UserId"].ToString()));

        filltrans();

        DataTable dtma = select("SELECT  OutGoingMailServer,WebMasterEmail,MasterEmailId, EmailMasterLoginPassword, AdminEmail, WHId " +
                           " FROM  CompanyWebsitMaster WHERE     (WHId = " + Convert.ToInt32(ddlWarehouse.SelectedValue) + ") ");

        if (dtma.Rows.Count > 0)
        {
            if (Convert.ToString(dtma.Rows[0]["MasterEmailId"]) != "" && Convert.ToString(dtma.Rows[0]["OutGoingMailServer"]) != "")
            {
                chkcust.Enabled = true;
                chkcust.ToolTip = "";
                btnaddcusm.Visible = false;
                cusmre.Visible = false;
            }
            else
            {
                btnaddcusm.Visible = true;
                cusmre.Visible = true;
                chkcust.Enabled = false;
                chkcust.ToolTip = "There is no email set for this customer.";
            }
        }
        fillwaresele();
        string cct = "";
        if (ddlWarehouse.SelectedIndex >= 0)
        {
            rbt_pay_method.Items.Clear();
            string opt1 = "Select * from Storepaymentmethod where Whid='" + ddlWarehouse.SelectedValue + "' and RetailCheck='1'";
            SqlDataAdapter adp1 = new SqlDataAdapter(opt1, con);
            DataTable dtp1 = new DataTable();
            adp1.Fill(dtp1);

            if (dtp1.Rows.Count > 0)
            {
                if (dtp1.Rows[0]["Paypal"].ToString() != "False")
                {
                    cct += "'" + dtp1.Rows[0]["Paypal"].ToString() + "',";
                }

                if (dtp1.Rows[0]["Craditcard"].ToString() != "False")
                {
                    cct += "'" + dtp1.Rows[0]["Craditcard"].ToString() + "',";
                }

                if (dtp1.Rows[0]["Cheque"].ToString() != "False")
                {
                    cct += "'" + dtp1.Rows[0]["Cheque"].ToString() + "',";
                }

                if (dtp1.Rows[0]["Credit"].ToString() != "False" || dtp1.Rows[0]["Credit"].ToString() == "False" || Convert.ToString( dtp1.Rows[0]["Credit"]) =="")
                {
                    if (Convert.ToString(dtp1.Rows[0]["Credit"]) == "" || dtp1.Rows[0]["Credit"].ToString() == "False")
                    {
                        cct += "'Credit',";
               
                    }
                    else
                    {
                        cct += "'" + dtp1.Rows[0]["Credit"].ToString() + "',";
                    }
                }

                if (dtp1.Rows[0]["Creditcart_offline"].ToString() != "False")
                {
                    cct += "'" + dtp1.Rows[0]["Creditcart_offline"].ToString() + "',";
                }

                if (dtp1.Rows[0]["Do_Co_RealTime"].ToString() != "False")
                {
                    cct += "'" + dtp1.Rows[0]["Do_Co_RealTime"].ToString() + "',";
                }

                if (dtp1.Rows[0]["Cash"].ToString() != "False")
                {
                    cct += "'" + dtp1.Rows[0]["Cash"].ToString() + "',";
                }
                cct = cct.Remove(cct.Length - 1, 1);
                rbt_pay_method.Items.Clear();
                string sqlb = "SELECT PaymentMethodID, Case when(PaymentMethodName='CreditCard') then 'Credit Card' else PaymentMethodName End as PaymentMethodName  FROM  PaymentMethodMaster where PaymentMethodName  in(" + cct + ")";
                SqlDataAdapter ad = new SqlDataAdapter(sqlb, con);
                DataSet ds = new DataSet();
                ad.Fill(ds);

                rbt_pay_method.DataSource = ds;
                rbt_pay_method.DataTextField = "PaymentMethodName";
                rbt_pay_method.DataValueField = "PaymentMethodID";
                rbt_pay_method.DataBind();
                rbt_pay_method.SelectedIndex = rbt_pay_method.Items.IndexOf(rbt_pay_method.Items.FindByText("Cash"));
                rbt_pay_method_SelectedIndexChanged(sender, e);
                for (int i = 0; i < rbt_pay_method.Items.Count; i++)
                {
                    if (rbt_pay_method.Items[i].Text == "Credit")
                    {
                        rbt_pay_method.Items[i].Text = "On Credit";
                    }
                }

            }
          

        }

       

        //CountTax();
    }

    public DataSet fillddl()
    {
        ddlcountryCR.Items.Clear();
        ddlstateCR.Items.Clear();
        ddlcityCR.Items.Clear();
        string str22 = "SELECT  CountryId      ,CountryName  as Country_Code  FROM  CountryMaster";
        SqlCommand cmd = new SqlCommand(str22, con);


        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }

    public DataSet fillddl2()
    {
        SqlCommand cmd = new SqlCommand("Sp_select_statemaster2", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CountryId", ddlcountry.SelectedValue);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    public DataSet fillddl3()
    {
        SqlCommand cmd = new SqlCommand("Sp_select_cityMaster2", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StateId", ddlstate.SelectedValue);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    protected void ddlcountryCR_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlstateCR.Items.Clear();
        ddlcityCR.Items.Clear();
        if (ddlcountryCR.SelectedIndex > 0)
        {
            string Fillstatecr = "SELECT     StateId, StateName, CountryId, State_Code " +
                " FROM StateMasterTbl WHERE     (CountryId = '" + ddlcountryCR.SelectedValue + "') ";
            dbss1.FillDDL1(ddlstateCR, Fillstatecr, "StateId", "StateName");

        }
        ddlstateCR.Items.Insert(0, "-Select-");
        //ddlcountry.Items[0].Value = "0";
        ddlstateCR.Items[0].Value = "0";
        ddlcityCR.Items.Insert(0, "-Select-");
        ddlcityCR.Items[0].Value = "0";
        // ddlcity.Items[0].Value = "0";
    }
    protected void ddlstateCR_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcityCR.Items.Clear();
        if (ddlstateCR.SelectedIndex > 0)
        {
            string Fillctcr = "SELECT      CityId, CityName, StateId " +
                            " FROM         CityMasterTbl WHERE     (StateId = '" + ddlstateCR.SelectedValue + "') ";
            dbss1.FillDDL1(ddlcityCR, Fillctcr, "CityId", "CityName");
        }
        ddlcityCR.Items.Insert(0, "--Select--");
        ddlcityCR.Items[0].Value = "0";
        //ddlcityCR.Items[0].Value = "0";
        //ddlstate.Items[0].Value = "0";
        ddlcityCR.Items[0].Value = "0";
    }
    protected void fillddata(DataTable dtn, Int32 salesorderidnew, String type)
    {
        int k = 0;
        foreach (DataRow dr in dtn.Rows)
        {

            string str45 = "SELECT  InventoryMaster.InventorySubSubId as categorySubSubId,   InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.ProductNo, InventoryImgMaster.Thumbnail, InventoryDetails.Weight,  " +
            "      InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.Rate,  " +
            "      InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.WareHouseId " +
             "  FROM         InventoryMaster LEFT OUTER JOIN " +
             "     InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN " +
             "     InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId RIGHT OUTER JOIN " +
             "     InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId " +
                  " WHERE      (InventoryWarehouseMasterTbl.Active = 1) " +
                 "  AND (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId ='" + Convert.ToInt32(Convert.ToInt32(dr["InventoryWarehouseMasterId"])) + "') ";

            SqlCommand cmd35 = new SqlCommand(str45, con);
            SqlDataAdapter adp35 = new SqlDataAdapter(cmd35);
            DataSet ds35 = new DataSet();
            adp35.Fill(ds35);
            string str50 = "";

            Label lblt1 = new Label();
            Label lblt2 = new Label();
            Label lblt3 = new Label();
            Label lblt112 = new Label();
            Label lblt122 = new Label();
            Label lblt132 = new Label();
            if (type == "PR")
            {
                lblt1 = (Label)(GridView1.Rows[k].FindControl("lbltax1"));
                lblt2 = (Label)(GridView1.Rows[k].FindControl("lbltax2"));
                lblt3 = (Label)(GridView1.Rows[k].FindControl("lbltax3"));

                lblt112 = (Label)(GridView1.Rows[k].FindControl("lbltax112"));
                lblt122 = (Label)(GridView1.Rows[k].FindControl("lbltax122"));
                lblt132 = (Label)(GridView1.Rows[k].FindControl("lbltax132"));
            }
            else
            {
                lblt1 = (Label)(GridView3.Rows[k].FindControl("lbltax1"));
                lblt2 = (Label)(GridView3.Rows[k].FindControl("lbltax2"));
                lblt3 = (Label)(GridView3.Rows[k].FindControl("lbltax3"));

                lblt112 = (Label)(GridView3.Rows[k].FindControl("lbltax112"));
                lblt122 = (Label)(GridView3.Rows[k].FindControl("lbltax122"));
                lblt132 = (Label)(GridView3.Rows[k].FindControl("lbltax132"));

            }
            k = k + 1;

            if (Convert.ToString(ViewState["type"]) == "3")
            {
                str50 = "insert into SalesOrderDetail(SalesOrderMasterId,categorySubSubId, " +
                   " InventoryWHM_Id,Qty,Rate,Amount,Quality,Notes,Tax1Id,Tax1Amt,Tax2Id,Tax2Amt,Tax3Id,Tax3Amt) " +
                   " values('" + salesorderidnew + "', " +
                   " '" + Convert.ToInt32(ds35.Tables[0].Rows[0]["categorySubSubId"]) + "', " +
                   " '" + Convert.ToInt32(dr["InventoryWarehouseMasterId"]) + "', " +
                   " '" + dr["OderedQty"].ToString() + "','" + dr["Rate"].ToString() + "', " +
                   " '" + dr["Total"].ToString() + "','1','Retail Shopping','" + Convert.ToString(ViewState["Acc1"]) + "','" + lblt112.Text + "','" + Convert.ToString(ViewState["Acc2"]) + "','" + lblt122.Text + "','" + Convert.ToString(ViewState["Acc3"]) + "','" + lblt132.Text + "')";
            }
            else
            {
                str50 = "insert into SalesOrderDetail(SalesOrderMasterId,categorySubSubId, " +
                   " InventoryWHM_Id,Qty,Rate,Amount,Quality,Notes) " +
                   " values('" + salesorderidnew + "', " +
                   " '" + Convert.ToInt32(ds35.Tables[0].Rows[0]["categorySubSubId"]) + "', " +
                   " '" + Convert.ToInt32(dr["InventoryWarehouseMasterId"]) + "', " +
                   " '" + dr["OderedQty"].ToString() + "','" + dr["Rate"].ToString() + "', " +
                   " '" + dr["Total"].ToString() + "','1','Retail Shopping')";

            }
            SqlCommand cmd50 = new SqlCommand(str50, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd50.ExecuteNonQuery();
            con.Close();



            int warehouseid1 = Convert.ToInt32(dr["InventoryWarehouseMasterId"]);

            con.Close();
            con.Open();
            double averagecost = 0;
            if (rdinvoice.SelectedIndex == 0)
            {
                string findavgcost = "select AvgCost from InventoryWarehouseMasterAvgCostTbl where InvWMasterId=" + warehouseid1 + " order by IWMAvgCostId Desc";
                SqlDataAdapter adp120 = new SqlDataAdapter(findavgcost, con);
                DataSet ds34 = new DataSet();
                adp120.Fill(ds34);
                averagecost = Convert.ToDouble(ds34.Tables[0].Rows[0][0].ToString());
            }
            int salesoredrid = salesorderidnew;

            con.Close();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }


            string cid = "select WareHouseId from InventoryWarehouseMasterTbl where InventoryWarehouseMasterId='" + dr["InventoryWarehouseMasterId"].ToString() + "'";
            SqlDataAdapter adp45 = new SqlDataAdapter(cid, con);
            DataSet d45 = new DataSet();
            adp45.Fill(d45);
            int cid12 = Convert.ToInt32(d45.Tables[0].Rows[0][0].ToString());



            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            string insertstockdetail = "insert into Salesorderbyavcost(categorySubSubId,InventoryWHM_Id,AVGcost,SalesOrderId,date,qty,price,countryid) " +
                "values('" + Convert.ToInt32(ds35.Tables[0].Rows[0]["categorySubSubId"]) + "','" + Convert.ToInt32(dr["InventoryWarehouseMasterId"]) + "'," +
                "" + averagecost + "," + salesoredrid + ",'" + System.DateTime.Today.ToShortDateString() + "','" + dr["OderedQty"].ToString() + "','" + dr["Rate"].ToString() + "'," + cid12 + ")";
            SqlCommand cm = new SqlCommand(insertstockdetail, con);
            cm.ExecuteNonQuery();
            con.Close();




            //******************************************************************
        }

    }
    public void InsertOrigenalOrder()
    {

        decimal discount1 = Convert.ToDecimal(isdecimalornot(lblCustDisc.Text) + isdecimalornot(lblOrderDisc.Text));
        string str2 = "Update SalesOrderMaster Set SalesManId='"+ddlsalespersion.SelectedValue+"',PartyId='" + Convert.ToInt32(ddlParty.SelectedValue) + "',SalesOrderDate='" + Convert.ToDateTime(txtGoodsDate.Text) + "',"+
       " ExpextedDeliveryDate='" + System.DateTime.Now.AddDays(4).ToString("MM/dd/yyyy") + "',PaymentsTerms='" + rbt_pay_method.SelectedValue + "',"+
       " OtherCharges='" + isdecimalornot(lblTax.Text) + "',Discounts='" + discount1 + "',GrossAmount='" + isdecimalornot(lblTotal.Text) + "', " +
        " Tax1Id='" + Convert.ToString(ViewState["Acc1"]) + "',Tax1Amt='" + txt1value.Text + "',Tax2Id='" + Convert.ToString(ViewState["Acc2"]) + "',Tax2Amt='" + txt2value.Text + "',Tax3Id='" + Convert.ToString(ViewState["Acc3"]) + "',Tax3Amt='" + txt3value.Text + "',TaxOption='" + Convert.ToString(ViewState["type"]) + "',DueDay='"+txtnumberofduedate.Text+"'" +
        " where SalesOrderId='"+ ViewState["ordernonew"]+"'" ;

        SqlCommand cmd2 = new SqlCommand(str2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        int salesorderidnew = Convert.ToInt32(ViewState["ordernonew"]);


        ViewState["GrsAmt"] = Convert.ToDecimal(isdecimalornot(lblTotal.Text));

        //insert selected realtime shipping for salesorder


        string str2h = "Update SalesOrderSuppliment Set AmountDue='" + Convert.ToDecimal(ViewState["GrsAmt"]) + "' where SalesOrderMasterId='" + Convert.ToInt32(ViewState["ordernonew"]) + "'";
      
        SqlCommand cmd2h = new SqlCommand(str2h, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2h.ExecuteNonQuery();
        con.Close();

        SqlCommand cmd19 = new SqlCommand("Update SalesOrderMasterTamp Set subTotal='" + isdecimalornot(lblGTotal.Text) + "',CustDisc='" + isdecimalornot(lblCustDisc.Text) + "',OrderDisc='" + isdecimalornot(lblOrderDisc.Text) + "',"+
            " Tax='" + isdecimalornot(lblTax.Text) + "',Total='" + isdecimalornot(lblTotal.Text) + "',userid='" + ddlsalespersion.SelectedValue + "',CustDiscId='"+Convert.ToString( ViewState["Pcid"])+"',OrderDiscId='"+Convert.ToString( ViewState["OrderDesc"])+"' where SalesOrderTempId='"+ViewState["ordernonew"]+"'",con);
 

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd19.ExecuteNonQuery();
        con.Close();

      
        int k = 0;
        SqlCommand cmdsald = new SqlCommand("Delete   from SalesOrderDetail where SalesOrderMasterId='" + salesorderidnew + "'", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdsald.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdsalavgh = new SqlCommand("Delete   from Salesorderbyavcost where SalesOrderId='" + salesorderidnew + "'", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdsalavgh.ExecuteNonQuery();
        con.Close();

        DataTable dtn = (DataTable)ViewState["dt"];
        if (dtn != null)
        {
            fillddata(dtn, salesorderidnew, "PR");
        }
        DataTable dtn1 = (DataTable)ViewState["dt1"];
        if (dtn1 != null)
        {
            fillddata(dtn1, salesorderidnew, "SR");

        }

        string str95sx = "Delete from StatusControl where SalesOrderId='" + salesorderidnew + "'";

         SqlCommand cmd95az = new SqlCommand(str95sx, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd95az.ExecuteNonQuery();
        con.Close();

        string statusmid = "";
        string str587 = "        SELECT    StatusId, StatusName, StatusCategoryMasterId FROM         StatusMaster " +
                        " WHERE     (StatusName = 'Fully Shipped')";
        SqlCommand cmd587 = new SqlCommand(str587, con);
        SqlDataAdapter adp587 = new SqlDataAdapter(cmd587);
        DataTable ds587 = new DataTable();
        adp587.Fill(ds587);
        if (ds587.Rows.Count > 0)
        {
            statusmid = ds587.Rows[0]["StatusId"].ToString();
        }

        string str95 = "    INSERT INTO StatusControl (StatusMasterId,Datetime  ,UserMasterId ,SalesOrderId, note) " +
                        " VALUES ('" + statusmid + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "','" + ddlsalespersion.SelectedValue + "','" + salesorderidnew + "','SalesOrder Master Entry')";
        SqlCommand cmd95 = new SqlCommand(str95, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd95.ExecuteNonQuery();
        con.Close();


        SqlCommand cmd23 = new SqlCommand("Update SalesOrderPaymentOption Set PaymentMethodID='" + rbt_pay_method.SelectedValue + "' where SalesOrderId='" + salesorderidnew + "'", con);
 

         if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd23.ExecuteNonQuery();
        con.Close();

       if(lblName.Visible == false)
        { lblName.Text= txtNameb.Text;
        lblEmail.Text = TxtEmailb.Text;
        lblShippingAdd.Text = txtShippingAddb.Text;
        lblCity.Text = txtCityb.Text;
        lblState.Text = txtStateb.Text;
        lblCountry.Text = txtCountryb.Text;
        lblPhone.Text = txtPhoneb.Text;
        lblzip.Text = txtzipb.Text;
        }
       string billaddress = "Name:" + lblName.Text + "<br>Address:" + lblShippingAdd.Text + "<br>Mail:" + lblEmail.Text + "<br>City:" + lblCity.Text + "<br>State:" + lblState.Text + "<br>Country:" + lblCountry.Text + "<br>Phone:" + lblPhone.Text + "<br>Zip:" + lblzip.Text + "<br>";
       if (lblName1.Visible == false)
       {
           lblName1.Text = txtName1.Text;
           lblEmail1.Text = txtEmail1.Text;
           lblShippingAdd1.Text = txtShippingAdd1.Text;
           lblCity1.Text = txtCity1.Text;
           lblState1.Text = txtState1.Text;
           lblCountry1.Text = txtCountry1.Text;
           lblPhone1.Text = txtPhone1.Text;
           lblzip1.Text = txtzip1.Text;
       }
       string Shipaddress = "Name:" + lblName1.Text + "<br>Address:" + lblShippingAdd1.Text + "<br>Mail:" + lblEmail1.Text + "<br>City:" + lblCity1.Text + "<br>State:" + lblState1.Text + "<br>Country:" + lblCountry1.Text + "<br>Phone:" + lblPhone1.Text + "<br>Zip:" + lblzip1.Text + "<br>";


       string strbitta = "Update  BillingAddress Set Name='" + lblName.Text + "', Email='" + lblEmail.Text + "', Address='" + lblShippingAdd.Text + "', City='" + lblCity.Text + "'," +
          " State='" + lblState.Text + "', Country='" + lblCountry.Text + "',Phone='" + lblPhone.Text + "',Zipcode='" + lblzip.Text + "' where SalesOrderId='" + salesorderidnew + "' ";
                             
     
           
       SqlCommand cmdbil = new SqlCommand(strbitta, con);
       if (con.State.ToString() != "Open")
       {
           con.Open();
       }
       cmdbil.ExecuteNonQuery();
       con.Close();


       string strship = "Update  ShippingAddress Set Name='" + lblName1.Text + "', Email='" + lblEmail1.Text + "', Address='" + lblShippingAdd1.Text + "', City='" + lblCity1.Text + "'," +
        " State='" + lblState1.Text + "', Country='" + lblCountry1.Text + "',Phone='" + lblPhone1.Text + "',Zipcode='" + lblzip1.Text + "' where SalesOrderId='" + salesorderidnew + "' ";
      
        SqlCommand cmdship = new SqlCommand(strship, con);
       if (con.State.ToString() != "Open")
       {
           con.Open();
       }
       cmdship.ExecuteNonQuery();
       con.Close();


       string str = "Update  SalesChallanMaster set PartyID='" + ddlParty.SelectedValue + "',SalesChallanDatetime='" + Convert.ToDateTime(txtGoodsDate.Text) + "', " +
    " ShipToAddress='" + Shipaddress + "', BillToAddress='" + billaddress + "' where  SalesChallanMasterId='" + ViewState["DId"] + "'";
       
        //string str = "INSERT INTO SalesChallanMaster " +
        //                             " (SalesNo,RefSalesOrderId, PartyID, SalesChallanDatetime, ShipToAddress, BillToAddress, EntryTypeId) " +
        //                           " VALUES     ('--','" + salesorderidnew + "','" + ddlParty.SelectedValue + "','" + Convert.ToDateTime(txtGoodsDate.Text) + "' ,'" + Shipaddress + "','" + billaddress + "',30)";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        ViewState["DcNo1"] = ViewState["DId"];

        string str3q = "Update  SalesChallanMoreInfo Set ShippersTrackingNo='" + txtTrackingNo.Text + "', ShippingPersonId='" + ddlTransporter.SelectedValue + "', UserId='" + Convert.ToInt32(ddlsalespersion.SelectedValue) + "'," +
         " PurchaseOrder='" + txtperchaseorder.Text + "',Terms='" + txtterms.Text + "' where SalesChallanMasterId='" + ViewState["DId"] + "' ";
           


     
        SqlCommand cmd3q = new SqlCommand(str3q, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd3q.ExecuteNonQuery();
        con.Close();


        GenerateSalesInvoice(Convert.ToInt32(ViewState["ordernonew"]));
        //}
        string str1de = "Delete from SalesChallanTransaction where SalesChallanMasterId='" + ViewState["DId"] + "'";

        SqlCommand cmd1de = new SqlCommand(str1de, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd1de.ExecuteNonQuery();
        con.Close();
        string str1dest = "Delete from SalesOrderDetailTemp where SalesOrderTempId='" + salesorderidnew + "'";

        SqlCommand cmd1dest = new SqlCommand(str1dest, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd1dest.ExecuteNonQuery();
        con.Close();
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            Label lbltot = (Label)gdr.FindControl("lbltot");
            TextBox txtqty = (TextBox)gdr.Cells[5].FindControl("TextBox4");
            // TextBox txtnotegrid = (TextBox)gdr.Cells[7].FindControl("TextBox6");
            Label lblinvwm = (Label)gdr.FindControl("lblinvwm");
            string str1 = "INSERT INTO SalesChallanTransaction " +
                      " (SalesChallanMasterId, inventoryWHM_Id, Quantity, Note) " +
                    " VALUES('" + ViewState["DId"] + "','" + Convert.ToInt32(lblinvwm.Text) + "','" + Convert.ToDecimal(txtqty.Text) + "','Sales Note Demo ')";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();

           
            Label lblPromoDis = (Label)gdr.FindControl("lblPromoDis");
            Label lblVolumeDis = (Label)gdr.FindControl("lblVolumeDis");

            string p = "0";
            string vo = "0";
            //double promdis = GetPromotionDiscount(Convert.ToInt16(txtqty.Text), Convert.ToDecimal(gdr.Cells[6].Text), Convert.ToInt32(lblinvwm.Text));
            //double volumedis = GetVolumeDiscount(Convert.ToInt32(lblinvwm.Text), Convert.ToInt16(txtqty.Text), Convert.ToDecimal(gdr.Cells[6].Text));

            p = Convert.ToString(lblPromoDis.Text);
            vo = Convert.ToString(lblVolumeDis.Text);


            SqlCommand cmd22 = new SqlCommand("insert into SalesOrderDetailTemp(SalesOrderTempId,InventoryWHM_Id, " +
          " item,Price,Qty,PromDisc,VolDisc,HandlingCharg,Total,Promorate,Bulkrate)" +
          " values('" + salesorderidnew + "','" + Convert.ToInt32(lblinvwm.Text) + "','" + Convert.ToString(gdr.Cells[2].Text) + "','" + isdoubleornot(gdr.Cells[9].Text) + "', " +
          " '" + Convert.ToDecimal(txtqty.Text) + "','" + isdoubleornot(p) + "','" + isdoubleornot(vo) + "','0','" + isdoubleornot(lbltot.Text) + "','" + isdoubleornot(gdr.Cells[7].Text) + "','" + isdoubleornot(gdr.Cells[8].Text) + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd22.ExecuteNonQuery();
            con.Close();



        }
        foreach (GridViewRow gdr in GridView3.Rows)
        {
            Label lbltot = (Label)gdr.FindControl("lbltot");
            TextBox txtqty = (TextBox)gdr.Cells[5].FindControl("TextBox4");
            // TextBox txtnotegrid = (TextBox)gdr.Cells[7].FindControl("TextBox6");
            Label lblinvwm = (Label)gdr.FindControl("lblinvwm");
            string str1 = "INSERT INTO SalesChallanTransaction " +
                      " (SalesChallanMasterId, inventoryWHM_Id, Quantity, Note) " +
                    " VALUES('" + ViewState["DId"] + "','" + Convert.ToInt32(lblinvwm.Text) + "','" + Convert.ToDecimal(txtqty.Text) + "','Sales Note Demo ')";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();


            Label lblPromoDis = (Label)gdr.FindControl("lblPromoDis");
            Label lblVolumeDis = (Label)gdr.FindControl("lblVolumeDis");

            string p = "0";
            string vo = "0";
            //double promdis = GetPromotionDiscount(Convert.ToInt16(txtqty.Text), Convert.ToDecimal(gdr.Cells[6].Text), Convert.ToInt32(lblinvwm.Text));
            //double volumedis = GetVolumeDiscount(Convert.ToInt32(lblinvwm.Text), Convert.ToInt16(txtqty.Text), Convert.ToDecimal(gdr.Cells[6].Text));

            p = Convert.ToString(lblPromoDis.Text);
            vo = Convert.ToString(lblVolumeDis.Text);


            SqlCommand cmd22 = new SqlCommand("insert into SalesOrderDetailTemp(SalesOrderTempId,InventoryWHM_Id, " +
          " item,Price,Qty,PromDisc,VolDisc,HandlingCharg,Total,Promorate,Bulkrate)" +
          " values('" + salesorderidnew + "','" + Convert.ToInt32(lblinvwm.Text) + "','" + Convert.ToString(gdr.Cells[2].Text) + "','" + isdoubleornot(gdr.Cells[9].Text) + "', " +
          " '" + Convert.ToDecimal(txtqty.Text) + "','" + isdoubleornot(p) + "','" + isdoubleornot(vo) + "','0','" + isdoubleornot(lbltot.Text) + "','" + isdoubleornot(gdr.Cells[7].Text) + "','" + isdoubleornot(gdr.Cells[8].Text) + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd22.ExecuteNonQuery();
            con.Close();



        }
        // UPDATE QTY

        //update qty in datatbase
        admin obj = new admin();
        if (ViewState["dt"] != null)
        {

            DataTable dtR = new DataTable();
            dtR = (DataTable)ViewState["dt"];
            double totamt = 0;

            ViewState["totamt"] = totamt;
        }


        //rbt_pay_method.SelectedItem.Text == "Cheque"

        string strcheq = "Delete from PaymentChequeDetail where SalesOrderId='" + salesorderidnew + "'";

        SqlCommand cmcheq = new SqlCommand(strcheq, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmcheq.ExecuteNonQuery();
        con.Close();
        string strcreca = "Delete from PaymentCreditCard where SaleOrderId='" + salesorderidnew + "'";

        SqlCommand cmcrec = new SqlCommand(strcreca, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmcrec.ExecuteNonQuery();
        con.Close();
        if (rbt_pay_method.SelectedValue == "3")
        {
            string str221 = "insert into PaymentChequeDetail(SalesOrderId,BankName,ChequeNo,TransitId,Branchname,BranchCity,BranchState,BranchCountry,Zipcode,amount,date)" +
    " values('" + Convert.ToInt32(ViewState["ordernonew"]) + "','" + txtbankname.Text + "','" + txtChequeNo.Text + "','" + txtTransitId.Text + "','" + txtBranchname.Text + "','" + txtCity.Text + "','" + ddlstate.SelectedValue + "','" + ddlcountry.SelectedValue + "','" + txtZipcode.Text + "','" + lblTotal.Text + "','" + DateTime.Now.ToShortDateString() + "')";

            SqlCommand cmd22 = new SqlCommand(str221, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd22.ExecuteNonQuery();
            con.Close();
        }
        /////rbt_pay_method.SelectedItem.Text == "CreditCard(Offline)" || rbt_pay_method.SelectedItem.Text == "CreditCard"
        if (rbt_pay_method.SelectedValue == "5" || rbt_pay_method.SelectedValue == "2")
        {
            string str221q = "INSERT INTO PaymentCreditCard (FirstName, LastName, SecurityCode, CreditCardNo, " +
                " ExpireDate, Address, Cityid, ZipCode, PhoneNo, SaleOrderId) VALUES     " +
                " ('" + txtFName.Text + "', '" + txtLname.Text + "', '" + txtSecureCodeForCC.Text + "', '" + txtCCno.Text + "', " +
                " '" + txtYear.Text + "', '" + txtaddresscredit.Text + "', " +
                " '" + ddlcityCR.SelectedValue + "','" + txtZipcodecredit.Text + "','" + txtPhonenoCreditCard.Text + "','" + Convert.ToInt32(ViewState["ordernonew"]) + "' ) ";

            SqlCommand cmd22q = new SqlCommand(str221q, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd22q.ExecuteNonQuery();
            con.Close();
        }


        if (rbt_pay_method.SelectedValue == "4")
        {

        }

        SqlDataAdapter adp1 = new SqlDataAdapter("select max(Tranction_Master_Id) as trnid from TranctionMaster where Whid='" + ddlWarehouse.SelectedValue + "'", con);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        int trnid = Convert.ToInt32(ds1.Tables[0].Rows[0]["trnid"]);

       
        if (Convert.ToString(ViewState["Acid1"]) != "")
        {
            if (Convert.ToDecimal(txt1value.Text) > 0)
            {
                string str611 = "INSERT INTO Tranction_Details(AccountDebit, AccountCredit, AmountDebit, AmountCredit, Tranction_Master_Id, DateTimeOfTransaction,compid,whid) " +
                      " VALUES      ('0', '" + Convert.ToInt32(Convert.ToString(ViewState["Acid1"])) + "', '0','" + txt1value.Text + "','" + trnid + "','" + txtGoodsDate.Text + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                SqlCommand cmd611 = new SqlCommand(str611, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd611.ExecuteNonQuery();
                con.Close();
            }
        }
        if (Convert.ToString(ViewState["Acid2"]) != "")
        {
            if (Convert.ToDecimal(txt2value.Text) > 0)
            {
                string str611 = "INSERT INTO Tranction_Details(AccountDebit, AccountCredit, AmountDebit, AmountCredit, Tranction_Master_Id, DateTimeOfTransaction,compid,whid) " +
                      " VALUES      ('0', '" + Convert.ToInt32(Convert.ToString(ViewState["Acid2"])) + "', '0','" + txt2value.Text + "','" + trnid + "','" + txtGoodsDate.Text + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                SqlCommand cmd611 = new SqlCommand(str611, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd611.ExecuteNonQuery();
                con.Close();
            }
        }
        if (Convert.ToString(ViewState["Acid3"]) != "")
        {
            if (Convert.ToDecimal(txt3value.Text) > 0)
            {
                string str611 = "INSERT INTO Tranction_Details(AccountDebit, AccountCredit, AmountDebit, AmountCredit, Tranction_Master_Id, DateTimeOfTransaction,compid,whid) " +
                      " VALUES      ('0', '" + Convert.ToInt32(Convert.ToString(ViewState["Acid3"])) + "', '0','" + txt3value.Text + "','" + trnid + "','" + txtGoodsDate.Text + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                SqlCommand cmd611 = new SqlCommand(str611, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd611.ExecuteNonQuery();
                con.Close();
            }

        }
        SqlCommand cmm = new SqlCommand("INSERT INTO StatusControl(SalesOrderId, SalesChallanMasterId, Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                      " VALUES('" + Convert.ToInt32(ViewState["ordernonew"]) + "','" + Convert.ToInt32(ViewState["DId"]) + "' ,'" + System.DateTime.Now.ToShortDateString() + "','18','Retail invoice','" + trnid.ToString() + "') ", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmm.ExecuteNonQuery();
        con.Close();




        SqlCommand cmm6 = new SqlCommand("UPDATE SalesOrderSuppliment   SET AmountDue ='0'  WHERE SalesOrderMasterId='" + Convert.ToInt32(ViewState["ordernonew"]) + "'  ", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmm6.ExecuteNonQuery();
        con.Close();
        if (chkcust.Checked == true)
        {
            sendmail();
        }
        if (Request.QueryString["docid"] != null)
        {

            inserdocatt();
        }
        lblmsg1.Text = "Record updated successfully";
      
        fillentno();
        lblmsg1.Visible = true;
        btnPrintCustomer.Visible = true;
      
        EventArgs e = new EventArgs();
        object sender = new object();
        btncancel_Click(sender, e);
       /// rbt_pay_method.SelectedItem.Text == "CreditCard(Offline)" || rbt_pay_method.SelectedItem.Text == "CreditCard"
        if (rbt_pay_method.SelectedValue == "5" || rbt_pay_method.SelectedValue == "2")
        {
            txtFName.Text = "";
            txtCCno.Text = "";
            txtLname.Text = "";
            txtSecureCodeForCC.Text = "";
            txtaddresscredit.Text = "";
            txtYear.Text = "";
            txtPhonenoCreditCard.Text = "";
            ddlcountryCR.SelectedIndex = 0;
            ddlstateCR.SelectedIndex = 0;
            ddlcityCR.SelectedIndex = 0;
            txtZipcodecredit.Text = "";

        }
           /// rbt_pay_method.SelectedItem.Text == "Cheque"
        else if (rbt_pay_method.SelectedItem.Text == "3")
        {
            txtChequeNo.Text = "";
            txtTransitId.Text = "";
            ddlcountry.SelectedIndex = 0;
            txtCity.Text = "";
            txtbankname.Text = "";
            txtBranchname.Text = "";
            ddlstate.SelectedIndex = 0;
            txtZipcode.Text = "";
        }
        Fillprintcopy();
        rdinvoice.Enabled = false;
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        lblMsg.Text = "If you have changed any quantities, click on the update button to view the changes.";
        lblcust.Text = "0";
        lblvolume.Text = "0";
        lblpromo.Text = "0";
        lblBulkDisc.Text = "0.00";
        lblGTotal.Text = "0.00";
        lblCustDisc.Text = "0.00";
        lblOrderDisc.Text = "0.00";
        lblTax.Text = "0.00";
        lblTotal.Text = "0.00";
        txtTrackingNo.Text = "0";
        txtperchaseorder.Text = "";
        txtterms.Text = "";
        ddlTransporter.SelectedIndex = 0;
        GridView1.DataSource = null;
        GridView1.DataBind();
        ViewState["dt"] = null;
        GridView1.Visible = false;
        lblMsg.Visible = true;
        //btnUpdate.Visible = false;
        GridView2.DataSource = null;
        GridView2.DataBind();

       
        btnPrintCustomer.Visible = false;
        rbList.SelectedIndex = 0;
        pnltxt1.Visible = false;
        pnltxt2.Visible = false;
        pnltxt3.Visible = false;
        lblcusdisname.Text = "";
        lblorderdiscname.Text = "";
        lblnettot.Text = "0.00";
        txt3value.Text = "0";

        txt2value.Text = "0";

        txt1value.Text = "0";
        if (chkdefchk.Checked==true)
        {
            pnlinv.Visible = true;
            if (rbList.SelectedIndex == 0)
            {
                ddlItem_SelectedIndexChanged(sender, e);
            }
        }
        else
        {
            rdinvoice.SelectedIndex = -1;
            rdinvoice.Enabled = true;
            pnlinv.Visible = false;
            lbladdinv.Text = "";
     
            txtsearch.Text = "";
            pnlSearch.Visible = false;
            PnlCategory.Visible = true;
          
            ddlCategory.SelectedIndex = 0;
            ddlCategory_SelectedIndexChanged(sender, e);
        }



    }



    //*******************chnages codes*******************
    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.Items.Clear();
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();
    }
    public DataSet fillddl(String qry)
    {
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

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


        if (e.CommandName == "remove")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            //string finaltax12 = CountTaxRemove();
            string finaltax12 = "";

            double fina12 = Convert.ToDouble(lblTax.Text);
            double minusamt = Convert.ToDouble(finaltax12);

            double ttax = fina12 - minusamt;
            lblTax.Text = string.Format("{0:0.00}", ttax);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["dt"];

            Double qty = Convert.ToDouble(dt.Rows[GridView1.SelectedIndex]["OderedQty"].ToString());

            //Double ttr = Convert.ToDouble(Convert.ToDouble(dt.Rows[GridView1.SelectedIndex]["AppliedRate"].ToString()) * Convert.ToDouble(dt.Rows[GridView1.SelectedIndex]["OderedQty"].ToString()));
            Double ttr = Convert.ToDouble(Convert.ToDouble(dt.Rows[GridView1.SelectedIndex]["Rate"].ToString()) * Convert.ToDouble(dt.Rows[GridView1.SelectedIndex]["OderedQty"].ToString()));
            Double FINALTOTAL = Convert.ToDouble(lblGTotal.Text);
            FINALTOTAL = FINALTOTAL - ttr;
            lblGTotal.Text = string.Format("{0:0.00}", FINALTOTAL);


            Double total = 0.00;
            Double Gtotal = 0.00;

            foreach (DataRow dr in dt.Rows)
            {
                ttr = Convert.ToDouble(Convert.ToDouble(dr["Rate"]) * Convert.ToDouble(dr["OderedQty"]));
                Gtotal = Convert.ToDouble(Gtotal) + ttr;
                total = Convert.ToDouble(total) + Convert.ToDouble(dr["Total"].ToString());

            }
            //*************

            double CustDis = 0.00;

            if (Session["userid"] != null)
            {
                string extrsct = "select PartyID from Party_master where Compname='" + ddlParty.SelectedItem.ToString() + "'";
                DataSet ds1235 = new DataSet();
                SqlDataAdapter dt12 = new SqlDataAdapter(extrsct, con);
                dt12.Fill(ds1235);
                Int32 partyid = Convert.ToInt32(ds1235.Tables[0].Rows[0][0].ToString());




                string str = "SELECT     PartyTypeCategoryMasterTbl.IsPercentage, PartyTypeCategoryMasterTbl.PartyCategoryDiscount, PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
                            " FROM         Party_master INNER JOIN " +
                             " PartyTypeDetailTbl ON Party_master.PartyID = PartyTypeDetailTbl.PartyID INNER JOIN " +
                              " User_master ON Party_master.PartyID = User_master.PartyID INNER JOIN " +
                             " PartyTypeCategoryMasterTbl ON PartyTypeDetailTbl.PartyTypeCategoryMasterId = PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
                    " WHERE     (User_master.PartyID = " + partyid + ") AND (PartyTypeCategoryMasterTbl.Active = 1) " +
                    " ORDER BY PartyTypeCategoryMasterTbl.EntryDate DESC ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds12 = new DataSet();
                adp.Fill(ds12);

                if (ds12.Tables[0].Rows.Count > 0)
                {
                    int i = Convert.ToInt32(ds12.Tables[0].Rows[0]["IsPercentage"]);

                    if (i == 1)
                    {

                        Double first = Convert.ToDouble(dt.Rows[GridView1.SelectedIndex]["OderedQty"].ToString()) * Convert.ToDouble(dt.Rows[GridView1.SelectedIndex]["Rate"].ToString());
                        CustDis = (first * Convert.ToDouble(ds12.Tables[0].Rows[0]["PartyCategoryDiscount"])) / 100;
                        {
                            Session["custdis"] = Math.Round( CustDis,2);

                        }


                    }
                    else
                    {

                        CustDis = Convert.ToDouble(ds12.Tables[0].Rows[0]["PartyCategoryDiscount"]);


                    }
                }

            }
            //****************

            decimal custdis = Convert.ToDecimal(CustDis);

            double promDisc = GetPromotionDiscount(Convert.ToInt32(dt.Rows[GridView1.SelectedIndex]["OderedQty"].ToString()), Convert.ToDecimal(dt.Rows[GridView1.SelectedIndex]["Rate"].ToString()), Convert.ToInt32(dt.Rows[GridView1.SelectedIndex]["InventoryWarehouseMasterId"].ToString()));
            //double promprice = Convert.ToDouble(dt.Rows[GridView1.SelectedIndex]) - promDisc;

            decimal pro = Convert.ToDecimal(promDisc);
            decimal promotional = Convert.ToDecimal(lblCustDisc.Text) - Convert.ToDecimal(pro);

            double BulkpricePerc = 0;
            double BUlkPrice = 0;



            Int32 InventorywhID = Convert.ToInt32(dt.Rows[GridView1.SelectedIndex]["InventoryWarehouseMasterId"].ToString());
            Double rate = Convert.ToDouble(dt.Rows[GridView1.SelectedIndex]["Rate"].ToString());


            string str123 = "SELECT     max(InventoryVolumeSchemeMaster.SchemeDiscount) as Discount" +
                    " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                     " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                    "WHERE     (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1) and (InventoryVolumeSchemeMaster.IsPercentage = 1) ";
            //  " (InventoryVolumeSchemeMaster.IsPercentage = 1) ";
            SqlCommand cmd123 = new SqlCommand(str123, con);
            SqlDataAdapter adp1234 = new SqlDataAdapter(cmd123);
            DataSet ds1234 = new DataSet();
            adp1234.Fill(ds1234);





            if (Convert.ToString(ds1234.Tables[0].Rows[0]["Discount"]) != "")
            {
                BulkpricePerc = (Convert.ToDouble(rate) * Convert.ToDouble(ds1234.Tables[0].Rows[0]["Discount"])) / 100;
                BUlkPrice = Convert.ToDouble(rate) - BulkpricePerc;
            }

            else
            {

                string str12 = "SELECT     max(InventoryVolumeSchemeMaster.SchemeDiscount) as Discount" +
                                 " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                                  " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                                 "WHERE     (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1) and (InventoryVolumeSchemeMaster.IsPercentage = 0)";
                //  " (InventoryVolumeSchemeMaster.IsPercentage = 1) ";
                SqlCommand cmd12 = new SqlCommand(str12, con);
                SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
                DataSet ds12 = new DataSet();
                adp12.Fill(ds12);

                if (Convert.ToString(ds12.Tables[0].Rows[0]["Discount"]) != "")
                {
                    BulkpricePerc = Convert.ToDouble(ds12.Tables[0].Rows[0]["Discount"]);
                    BUlkPrice = Convert.ToDouble(rate) - BulkpricePerc;

                }



            }


            decimal bulkprice1 = Convert.ToDecimal(BulkpricePerc);


            decimal totaldiscount = bulkprice1 + pro + custdis;

            Double FIN1 = Convert.ToDouble(totaldiscount);

            double dubdis = Convert.ToDouble(lblCustDisc.Text) - (FIN1);

            lblCustDisc.Text = String.Format("{0:0.00}", dubdis);
            //****************



            GridView1.DataSource = dt;
            GridView1.DataBind();
            //gridtax();
            ViewState["dt"] = dt;
            dt.Rows.Remove(dt.Rows[GridView1.SelectedIndex]);


            dt.AcceptChanges();


            //************
            double finaltotalgtotal = 0;

            foreach (DataRow dr in dt.Rows)
            {
                ttr = Convert.ToDouble(Convert.ToDouble(dr["Rate"]) * Convert.ToDouble(dr["OderedQty"]));
                finaltotalgtotal = Convert.ToDouble(finaltotalgtotal) + ttr;
                total = Convert.ToDouble(total) + Convert.ToDouble(dr["Total"].ToString());

            }

            decimal dis1 = 0;


            //********************* count order values discount


            DateTime saledate = Convert.ToDateTime(txtGoodsDate.Text);
            string fetchdiscount = "select * from OrderValueDiscountMaster where Whid='" + ddlWarehouse.SelectedValue + "' and Active=1 ";
            //and   ('" + saledate + "' >= StartDate and '" + saledate + "' <= EndDate)";
            DataSet ds01 = new DataSet();
            SqlDataAdapter ad01 = new SqlDataAdapter(fetchdiscount, con);
            ad01.Fill(ds01);
            if (ds01.Tables[0].Rows.Count > 0)
            {
                DateTime startdate = Convert.ToDateTime(ds01.Tables[0].Rows[0]["StartDate"].ToString());
                DateTime enddate = Convert.ToDateTime(ds01.Tables[0].Rows[0]["EndDate"].ToString());
                if (saledate >= startdate && saledate <= enddate)
                {
                    double minvalu = Convert.ToDouble(ds01.Tables[0].Rows[0]["MinValue"].ToString());
                    double maxvalu = Convert.ToDouble(ds01.Tables[0].Rows[0]["MaxValue"].ToString());
                    double discount = Convert.ToDouble(ds01.Tables[0].Rows[0]["ValueDiscount"].ToString());
                    string checkper = Convert.ToString(ds01.Tables[0].Rows[0]["IsPercentage"].ToString());
                    if (finaltotalgtotal >= minvalu && finaltotalgtotal <= maxvalu)
                    {
                        if (checkper == "False")
                        {

                            dis1 = Convert.ToDecimal(discount);
                            //lblOrderDisc.Text = Convert.ToString(discount);
                        }
                        else
                        {
                            dis1 = Convert.ToDecimal(((finaltotalgtotal) * (discount)) / 100);
                            // lblOrderDisc.Text = Convert.ToString(((Gtotal) * (discount)) / 100);
                        }
                    }
                    else
                    {
                        dis1 = 0;
                    }
                }
                else
                {
                    dis1 = 0;
                }

            }
            else
            {
                dis1 = 0;
            }



            lblOrderDisc.Text = string.Format("{0:0.00}", dis1);
            //******************
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            //ViewState["dt"] = dt;
            //dt.Rows.Remove(dt.Rows[GridView1.SelectedIndex]);

            //CountTax();
            //decimal maint = Convert.ToDecimal(Convert.ToDecimal(lblGTotal.Text) + Convert.ToDecimal(lblTax.Text));
            //lblTotal.Text = String.Format("{0:0.00}", maint.ToString());
            decimal maint = Convert.ToDecimal(Convert.ToDecimal(lblGTotal.Text) + Convert.ToDecimal(lblTax.Text));
            decimal finalmaint = maint - Convert.ToDecimal(lblCustDisc.Text);
            lblTotal.Text = String.Format("{0:0.00}", finalmaint);



            GridView1.DataSource = dt;
            GridView1.DataBind();
            //gridtax();
        }


    }
    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {

    }



    public void gridtax()
    {
        ViewState["type"] = "";
        ViewState["At1"] = "0";
        ViewState["At2"] = "0";
        ViewState["At3"] = "0";
        ViewState["Acc1"] = "";
        ViewState["Acc2"] = "";
        ViewState["Acc3"] = "";
        ViewState["Acid1"] = "";
        ViewState["Acid2"] = "";
        ViewState["Acid3"] = "";
        lbltaxName.Text = "Total Tax";
        GridView1.Columns[11].Visible = false;
        GridView1.Columns[12].Visible = false;
        GridView1.Columns[10].Visible = false;
        GridView3.Columns[11].Visible = false;
        GridView3.Columns[12].Visible = false;
        GridView3.Columns[10].Visible = false;



        pnltxt1.Visible = false;
        pnltxt2.Visible = false;
        pnltxt3.Visible = false;
        decimal sumtax = 0;
        decimal sumtax1 = 0;
        decimal sumtax2 = 0;
        DataTable dttxt = select("select * from StorTaxmethodtbl where Storeid='" + ddlWarehouse.SelectedValue + "'");

        if (dttxt.Rows.Count > 0)
        {




            //double ST = 0;

            if (dttxt.Rows[0]["Variabletax"].ToString() == "1")
            {
                ViewState["type"] = "3";
                DataTable dtstate = select("Select State,Country,StateName,CountryName from CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId  inner join WarehouseMaster on WarehouseMaster.WarehouseId=CompanyWebsitMaster.Whid where CompanyWebsitMaster.Whid='" + ddlWarehouse.SelectedValue + "'");
                if (dtstate.Rows.Count > 0)
                {

                    if (GridView1.Rows.Count > 0)
                    {


                        DataTable dtwhid = select("select Top(3) Taxname,Taxshortname,TaxInvAccountMasterID,[SalesTaxOption3TaxNameTbl].[Taxname],[SalesTaxOption3TaxNameTbl].[StateId],Id from  [SalesTaxOption3TaxNameTbl]  where [Whid]='" + ddlWarehouse.SelectedValue + "' and ([SalesTaxOption3TaxNameTbl].[StateId]='" + Convert.ToString(dtstate.Rows[0]["State"]) + "' or [SalesTaxOption3TaxNameTbl].[StateId]='0') order by SalesTaxOption3TaxNameTbl.Id Desc");

                        if (dtwhid.Rows.Count > 0)
                        {
                            if (dtwhid.Rows.Count == 3)
                            {
                                pnltxt1.Visible = true;
                                pnltxt2.Visible = true;
                                pnltxt3.Visible = true;
                                GridView1.Columns[10].Visible = true;
                                GridView1.Columns[11].Visible = true;
                                GridView1.Columns[12].Visible = true;

                                txt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                                txt2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";
                                txt3.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " Tax";

                                ViewState["Acc1"] = Convert.ToString(dtwhid.Rows[0]["Id"]); ;
                                ViewState["Acc2"] = Convert.ToString(dtwhid.Rows[1]["Id"]); ;
                                ViewState["Acc3"] = Convert.ToString(dtwhid.Rows[2]["Id"]); ;
                                ViewState["Acid1"] = Convert.ToString(dtwhid.Rows[0]["TaxInvAccountMasterID"]);
                                ViewState["Acid2"] = Convert.ToString(dtwhid.Rows[1]["TaxInvAccountMasterID"]);
                                ViewState["Acid3"] = Convert.ToString(dtwhid.Rows[2]["TaxInvAccountMasterID"]);
                                Label ht1 = (Label)GridView1.HeaderRow.FindControl("ht1");
                                Label ht2 = (Label)GridView1.HeaderRow.FindControl("ht2");
                                Label ht3 = (Label)GridView1.HeaderRow.FindControl("ht3");
                                ht1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                                ht2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";
                                ht3.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " Tax";
                                foreach (GridViewRow gtf in GridView1.Rows)
                                {
                                    Label lblinvwm = (Label)gtf.FindControl("lblinvwm");
                                    string invd = Convert.ToString(lblinvwm.Text);
                                    Label lbltot = (Label)gtf.FindControl("lbltot");
                                    string tota = Convert.ToString(lbltot.Text);
                                    Label lbltax1 = (Label)gtf.FindControl("lbltax1");
                                    Label lbltax113 = (Label)gtf.FindControl("lbltax113");
                                    Label lbltax112 = (Label)gtf.FindControl("lbltax112");


                                    Label lbltax2 = (Label)gtf.FindControl("lbltax2");
                                    Label lbltax123 = (Label)gtf.FindControl("lbltax123");
                                    Label lbltax122 = (Label)gtf.FindControl("lbltax122");


                                    Label lbltax3 = (Label)gtf.FindControl("lbltax3");
                                    Label lbltax133 = (Label)gtf.FindControl("lbltax133");
                                    Label lbltax132 = (Label)gtf.FindControl("lbltax132");

                                    DataTable dtr = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[0]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and ( ApplyToAllSales='1')");
                                    if (dtr.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax113.Text = Convert.ToString(dtr.Rows[0]["Tax%"]) + "%";
                                            lbltax112.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax113.Text = "$";
                                            lbltax112.Text = Convert.ToString(dtr.Rows[0]["Tax%"]);
                                        }
                                        sumtax += Convert.ToDecimal(lbltax112.Text);
                                    }
                                    DataTable dtr1 = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[1]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and ( ApplyToAllSales='1')");
                                    if (dtr1.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr1.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax123.Text = Convert.ToString(dtr1.Rows[0]["Tax%"]) + "%";
                                            lbltax122.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr1.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax123.Text = "$";
                                            lbltax122.Text = Convert.ToString(dtr1.Rows[0]["Tax%"]);
                                        }
                                        sumtax1 += Convert.ToDecimal(lbltax122.Text);
                                    }
                                    DataTable dtr2 = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[2]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and ( ApplyToAllSales='1')");
                                    if (dtr2.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr2.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax133.Text = Convert.ToString(dtr2.Rows[0]["Tax%"]) + "%";
                                            lbltax132.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr2.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax133.Text = "$";
                                            lbltax132.Text = Convert.ToString(dtr2.Rows[0]["Tax%"]);
                                        }
                                        sumtax2 += Convert.ToDecimal(lbltax132.Text);
                                    }
                                }
                            }
                            if (dtwhid.Rows.Count == 2)
                            {
                                GridView1.Columns[10].Visible = true;
                                GridView1.Columns[11].Visible = true;
                                pnltxt1.Visible = true;
                                pnltxt2.Visible = true;

                                txt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                                txt2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";

                                ViewState["Acc1"] = Convert.ToString(dtwhid.Rows[0]["Id"]); ;
                                ViewState["Acc2"] = Convert.ToString(dtwhid.Rows[1]["Id"]); ;

                                ViewState["Acid1"] = Convert.ToString(dtwhid.Rows[0]["TaxInvAccountMasterID"]);
                                ViewState["Acid2"] = Convert.ToString(dtwhid.Rows[1]["TaxInvAccountMasterID"]);

                                Label ht1 = (Label)GridView1.HeaderRow.FindControl("ht1");
                                Label ht2 = (Label)GridView1.HeaderRow.FindControl("ht2");
                                ht1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                                ht2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";
                                foreach (GridViewRow gtf in GridView1.Rows)
                                {
                                    Label lbltax1 = (Label)gtf.FindControl("lbltax1");
                                    Label lblinvwm = (Label)gtf.FindControl("lblinvwm");
                                    Label lbltot = (Label)gtf.FindControl("lbltot");
                                    string invd = Convert.ToString(lblinvwm.Text);
                                    string tota = Convert.ToString(lbltot.Text);

                                    Label lbltax113 = (Label)gtf.FindControl("lbltax113");
                                    Label lbltax112 = (Label)gtf.FindControl("lbltax112");


                                    Label lbltax2 = (Label)gtf.FindControl("lbltax2");
                                    Label lbltax123 = (Label)gtf.FindControl("lbltax123");
                                    Label lbltax122 = (Label)gtf.FindControl("lbltax122");



                                    DataTable dtr = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[0]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and (ApplyToallOnlineSales='1' or ApplyToAllSales='1')");
                                    if (dtr.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax113.Text = Convert.ToString(dtr.Rows[0]["Tax%"]) + "%";
                                            lbltax112.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax113.Text = "$";
                                            lbltax112.Text = Convert.ToString(dtr.Rows[0]["Tax%"]);
                                        }
                                        sumtax += Convert.ToDecimal(lbltax112.Text);
                                    }
                                    DataTable dtr1 = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[1]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and (ApplyToallOnlineSales='1' or ApplyToAllSales='1')");
                                    if (dtr1.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr1.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax123.Text = Convert.ToString(dtr1.Rows[0]["Tax%"]) + "%";
                                            lbltax122.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr1.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax123.Text = "$";
                                            lbltax122.Text = Convert.ToString(dtr1.Rows[0]["Tax%"]);
                                        }
                                        sumtax1 += Convert.ToDecimal(lbltax122.Text);
                                    }

                                }
                            }


                            if (dtwhid.Rows.Count == 1)
                            {
                                pnltxt1.Visible = true;


                                GridView1.Columns[10].Visible = true;
                                txt1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";

                                ViewState["Acc1"] = Convert.ToString(dtwhid.Rows[0]["Id"]); ;
                                ViewState["Acid1"] = Convert.ToString(dtwhid.Rows[0]["TaxInvAccountMasterID"]);

                                Label ht1 = (Label)GridView1.HeaderRow.FindControl("ht1");
                                ht1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";

                                foreach (GridViewRow gtf in GridView1.Rows)
                                {
                                    Label lblinvwm = (Label)gtf.FindControl("lblinvwm");
                                    string invd = Convert.ToString(lblinvwm.Text);
                                    Label lbltot = (Label)gtf.FindControl("lbltot");
                                    string tota = Convert.ToString(lbltot.Text);
                                    Label lbltax1 = (Label)gtf.FindControl("lbltax1");
                                    Label lbltax113 = (Label)gtf.FindControl("lbltax113");
                                    Label lbltax112 = (Label)gtf.FindControl("lbltax112");

                                    DataTable dtr = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[0]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and (ApplyToallOnlineSales='1' or ApplyToAllSales='1')");
                                    if (dtr.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax113.Text = Convert.ToString(dtr.Rows[0]["Tax%"]) + "%";
                                            lbltax112.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax113.Text = "$";
                                            lbltax112.Text = Convert.ToString(dtr.Rows[0]["Tax%"]);
                                        }
                                        sumtax += Convert.ToDecimal(lbltax112.Text);
                                    }


                                }
                            }
                        }

                    }




                }


            }
        }
        txt1value.Text = "0.00";
        if (sumtax > 0)
        {
            pnltxt1.Visible = true;
            txt1value.Text = sumtax.ToString();
        }
        txt2value.Text = "0.00";
        if (sumtax1 > 0)
        {
            pnltxt2.Visible = true;
            txt2value.Text = sumtax1.ToString();
        }
        txt3value.Text = "0.00";
        if (sumtax2 > 0)
        {
            pnltxt3.Visible = true;
            txt3value.Text = sumtax2.ToString();
        }

        lblTax.Text = "0.00";
        lblTax.Text = Convert.ToString(sumtax + sumtax1 + sumtax2);
        gridtaxser();
    }
    public void gridtaxser()
    {

        lbltaxName.Text = "Total Tax";
        GridView3.Columns[11].Visible = false;
        GridView3.Columns[12].Visible = false;
        GridView3.Columns[10].Visible = false;



        decimal sumtax = 0;
        decimal sumtax1 = 0;
        decimal sumtax2 = 0;
        DataTable dttxt = select("select * from StorTaxmethodtbl where Storeid='" + ddlWarehouse.SelectedValue + "'");

        if (dttxt.Rows.Count > 0)
        {




            //double ST = 0;

            if (dttxt.Rows[0]["Variabletax"].ToString() == "1")
            {
                ViewState["type"] = "3";
                DataTable dtstate = select("Select State,Country,StateName,CountryName from CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId  inner join WarehouseMaster on WarehouseMaster.WarehouseId=CompanyWebsitMaster.Whid where CompanyWebsitMaster.Whid='" + ddlWarehouse.SelectedValue + "'");
                if (dtstate.Rows.Count > 0)
                {

                    if (GridView3.Rows.Count > 0)
                    {


                        DataTable dtwhid = select("select Top(3) Taxname,Taxshortname,TaxInvAccountMasterID,[SalesTaxOption3TaxNameTbl].[Taxname],[SalesTaxOption3TaxNameTbl].[StateId],Id from  [SalesTaxOption3TaxNameTbl]  where [Whid]='" + ddlWarehouse.SelectedValue + "' and ([SalesTaxOption3TaxNameTbl].[StateId]='" + Convert.ToString(dtstate.Rows[0]["State"]) + "' or [SalesTaxOption3TaxNameTbl].[StateId]='0') order by SalesTaxOption3TaxNameTbl.Id Desc");

                        if (dtwhid.Rows.Count > 0)
                        {
                            if (dtwhid.Rows.Count == 3)
                            {

                                GridView3.Columns[10].Visible = true;
                                GridView3.Columns[11].Visible = true;
                                GridView3.Columns[12].Visible = true;


                                Label ht1 = (Label)GridView3.HeaderRow.FindControl("ht1");
                                Label ht2 = (Label)GridView3.HeaderRow.FindControl("ht2");
                                Label ht3 = (Label)GridView3.HeaderRow.FindControl("ht3");
                                ht1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                                ht2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";
                                ht3.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " Tax";
                                foreach (GridViewRow gtf in GridView3.Rows)
                                {
                                    Label lblinvwm = (Label)gtf.FindControl("lblinvwm");
                                    string invd = Convert.ToString(lblinvwm.Text);
                                    Label lbltot = (Label)gtf.FindControl("lbltot");
                                    string tota = Convert.ToString(lbltot.Text);
                                    Label lbltax1 = (Label)gtf.FindControl("lbltax1");
                                    Label lbltax113 = (Label)gtf.FindControl("lbltax113");
                                    Label lbltax112 = (Label)gtf.FindControl("lbltax112");


                                    Label lbltax2 = (Label)gtf.FindControl("lbltax2");
                                    Label lbltax123 = (Label)gtf.FindControl("lbltax123");
                                    Label lbltax122 = (Label)gtf.FindControl("lbltax122");


                                    Label lbltax3 = (Label)gtf.FindControl("lbltax3");
                                    Label lbltax133 = (Label)gtf.FindControl("lbltax133");
                                    Label lbltax132 = (Label)gtf.FindControl("lbltax132");

                                    DataTable dtr = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[0]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and ( ApplyToAllSales='1')");
                                    if (dtr.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax113.Text = Convert.ToString(dtr.Rows[0]["Tax%"]) + "%";
                                            lbltax112.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax113.Text = "$";
                                            lbltax112.Text = Convert.ToString(dtr.Rows[0]["Tax%"]);
                                        }
                                        sumtax += Convert.ToDecimal(lbltax112.Text);
                                    }
                                    DataTable dtr1 = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[1]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and ( ApplyToAllSales='1')");
                                    if (dtr1.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr1.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax123.Text = Convert.ToString(dtr1.Rows[0]["Tax%"]) + "%";
                                            lbltax122.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr1.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax123.Text = "$";
                                            lbltax122.Text = Convert.ToString(dtr1.Rows[0]["Tax%"]);
                                        }
                                        sumtax1 += Convert.ToDecimal(lbltax122.Text);
                                    }
                                    DataTable dtr2 = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[2]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and ( ApplyToAllSales='1')");
                                    if (dtr2.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr2.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax133.Text = Convert.ToString(dtr2.Rows[0]["Tax%"]) + "%";
                                            lbltax132.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr2.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax133.Text = "$";
                                            lbltax132.Text = Convert.ToString(dtr2.Rows[0]["Tax%"]);
                                        }
                                        sumtax2 += Convert.ToDecimal(lbltax132.Text);
                                    }
                                }
                            }
                            if (dtwhid.Rows.Count == 2)
                            {
                                GridView3.Columns[10].Visible = true;
                                GridView3.Columns[11].Visible = true;


                                Label ht1 = (Label)GridView3.HeaderRow.FindControl("ht1");
                                Label ht2 = (Label)GridView3.HeaderRow.FindControl("ht2");
                                ht1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                                ht2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";
                                foreach (GridViewRow gtf in GridView3.Rows)
                                {
                                    Label lbltax1 = (Label)gtf.FindControl("lbltax1");
                                    Label lblinvwm = (Label)gtf.FindControl("lblinvwm");
                                    string invd = Convert.ToString(lblinvwm.Text);
                                    Label lbltot = (Label)gtf.FindControl("lbltot");
                                    string tota = Convert.ToString(lbltot.Text);

                                    Label lbltax113 = (Label)gtf.FindControl("lbltax113");
                                    Label lbltax112 = (Label)gtf.FindControl("lbltax112");


                                    Label lbltax2 = (Label)gtf.FindControl("lbltax2");
                                    Label lbltax123 = (Label)gtf.FindControl("lbltax123");
                                    Label lbltax122 = (Label)gtf.FindControl("lbltax122");



                                    DataTable dtr = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[0]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and (ApplyToallOnlineSales='1' or ApplyToAllSales='1')");
                                    if (dtr.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax113.Text = Convert.ToString(dtr.Rows[0]["Tax%"]) + "%";
                                            lbltax112.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax113.Text = "$";
                                            lbltax112.Text = Convert.ToString(dtr.Rows[0]["Tax%"]);
                                        }
                                        sumtax += Convert.ToDecimal(lbltax112.Text);
                                    }
                                    DataTable dtr1 = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[1]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and (ApplyToallOnlineSales='1' or ApplyToAllSales='1')");
                                    if (dtr1.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr1.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax123.Text = Convert.ToString(dtr1.Rows[0]["Tax%"]) + "%";
                                            lbltax122.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr1.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax123.Text = "$";
                                            lbltax122.Text = Convert.ToString(dtr1.Rows[0]["Tax%"]);
                                        }
                                        sumtax1 += Convert.ToDecimal(lbltax122.Text);
                                    }

                                }
                            }


                            if (dtwhid.Rows.Count == 1)
                            {
                                pnltxt1.Visible = true;


                                GridView3.Columns[10].Visible = true;

                                Label ht1 = (Label)GridView3.HeaderRow.FindControl("ht1");
                                ht1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";

                                foreach (GridViewRow gtf in GridView3.Rows)
                                {
                                    Label lblinvwm = (Label)gtf.FindControl("lblinvwm");
                                    string invd = Convert.ToString(lblinvwm.Text);
                                    Label lbltot = (Label)gtf.FindControl("lbltot");
                                    string tota = Convert.ToString(lbltot.Text);
                                    Label lbltax1 = (Label)gtf.FindControl("lbltax1");
                                    Label lbltax113 = (Label)gtf.FindControl("lbltax113");
                                    Label lbltax112 = (Label)gtf.FindControl("lbltax112");

                                    DataTable dtr = select("select [Tax%],Taxable from [InventoryTaxability]  where Active='1' and Taxoption3id='" + dtwhid.Rows[0]["Id"] + "'  and InventoryWHM_Id='" + Convert.ToInt32(invd) + "'  and (ApplyToallOnlineSales='1' or ApplyToAllSales='1')");
                                    if (dtr.Rows.Count > 0)
                                    {

                                        if (Convert.ToString(dtr.Rows[0]["Taxable"]) == "True")
                                        {
                                            lbltax113.Text = Convert.ToString(dtr.Rows[0]["Tax%"]) + "%";
                                            lbltax112.Text = Convert.ToString(Math.Round(Convert.ToDecimal(tota) * Convert.ToDecimal(dtr.Rows[0]["Tax%"]) / 100, 2));
                                        }
                                        else
                                        {
                                            lbltax113.Text = "$";
                                            lbltax112.Text = Convert.ToString(dtr.Rows[0]["Tax%"]);
                                        }
                                        sumtax += Convert.ToDecimal(lbltax112.Text);
                                    }


                                }
                            }
                        }

                    }




                }


            }
        }
        if (sumtax > 0)
        {
            pnltxt1.Visible = true;
            txt1value.Text = Convert.ToDecimal(txt1value.Text) + sumtax.ToString();
        }
        if (sumtax1 > 0)
        {
            pnltxt2.Visible = true;
            txt2value.Text = Convert.ToDecimal(txt2value.Text) + sumtax1.ToString();
        }
        if (sumtax2 > 0)
        {
            pnltxt3.Visible = true;
            txt3value.Text = Convert.ToDecimal(txt3value.Text) + sumtax2.ToString();
        }
        lblTax.Text = Convert.ToString(Convert.ToDecimal(lblTax.Text) + sumtax + sumtax1 + sumtax2);
    }

    public void entry()
    {
        String te = "AccEntryDocUp.aspx?Tid=" + ViewState["tid"];


        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

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


    protected void btndoc_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["docid"] != null)
        {
            inserdocatt();
        }
        else
        {

            entry();
            // ModalPopupExtender1.Show();
            //filldoc();

        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1222.Hide();
    }
    protected void ddlstate_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void Fillprintcopy()
    {
        if (chkDocup.Checked == true && chkcust.Checked == true)
        {
            String temp = "RetailCustomerDeliveryChallanPrint.aspx?id=" + Convert.ToInt32(ViewState["DId"]) + "&wareid=" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "&Td=" + Convert.ToString(ViewState["tid"] + "&Dc=1&ml=752");
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);
        }
        else if (chkDocup.Checked == true)
        {
            String temp = "RetailCustomerDeliveryChallanPrint.aspx?id=" + Convert.ToInt32(ViewState["DId"]) + "&wareid=" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "&Td=" + Convert.ToString(ViewState["tid"] + "&Dc=1");
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);
        }
        else if (chkcust.Checked == true)
        {
            String temp = "RetailCustomerDeliveryChallanPrint.aspx?id=" + Convert.ToInt32(ViewState["DId"]) + "&wareid=" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "&Td=" + Convert.ToString(ViewState["tid"] + "&ml=752");
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);

        }
        else
        {
            String temp = "RetailCustomerDeliveryChallanPrint.aspx?id=" + Convert.ToInt32(ViewState["DId"]) + "&wareid=" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "&Td=" + Convert.ToString(ViewState["tid"] + "&Dc=1cv");
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);

        }
        //Session["tax"] = lblTax.Text;

    }


    protected void lblRate_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventoryMasterStorelocation.aspx?invid=" + ddlItem.SelectedValue + "&whid=" + ddlWarehouse.SelectedValue);
    }

    protected void ctax()
    {
        string tax1 = "0";
        string tax2 = "0";
        string tax3 = "0";
        if (Convert.ToString(ViewState["type"]) == "1" || Convert.ToString(ViewState["type"]) == "2")
        {
            if (Convert.ToString(ViewState["Acc1"]) != "")
            {
                tax1 = txt1rat.Text.Replace("%", "");
                tax1 = tax1.Replace("$", "");
                if (Convert.ToString(ViewState["At1"]) == "0")
                {
                    txt1value.Text = tax1;
                }
                else
                {
                    txt1value.Text = Convert.ToString(Convert.ToDecimal(lblGTotal.Text) * Convert.ToDecimal(tax1) / 100);
                }
            }
            if (Convert.ToString(ViewState["Acc2"]) != "")
            {
                tax2 = txt2rat.Text.Replace("%", "");
                tax2 = tax2.Replace("$", "");
                if (Convert.ToString(ViewState["At2"]) == "0")
                {
                    txt2value.Text = tax2;
                }
                else
                {
                    txt2value.Text = Convert.ToString(Convert.ToDecimal(lblGTotal.Text) * Convert.ToDecimal(tax2) / 100);
                }
            }
            if (Convert.ToString(ViewState["Acc3"]) != "")
            {
                tax3 = txt3rat.Text.Replace("%", "");
                tax3 = tax3.Replace("$", "");
                if (Convert.ToString(ViewState["At3"]) == "0")
                {
                    txt3value.Text = tax3;
                }
                else
                {
                    txt3value.Text = Convert.ToString(Convert.ToDecimal(lblGTotal.Text) * Convert.ToDecimal(tax3) / 100);
                }
            }
        }
        lblTax.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txt1value.Text) + Convert.ToDecimal(txt2value.Text) + Convert.ToDecimal(txt3value.Text), 2));

    }

    protected void linehide()
    {
        if (pnltxt1.Visible == true || pnltxt2.Visible == true || pnltxt3.Visible == true)
        {
            pnlline.Visible = true;
        }
        else
        {
            pnlline.Visible = false;
        }
    }

    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {
        string te = "AccountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void lnkadd0_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlWarehouse.SelectedIndex > 0)
        {
            ddlCash.DataSource = (DataSet)fillCashAccount();
            ddlCash.DataTextField = "AccountName";
            ddlCash.DataValueField = "AccountId";
            ddlCash.DataBind();
        }
    }
    protected void BillAdd()
    {
        DataTable dr=select(" SELECT     User_master.UserID, User_master.Name, User_master.Address, User_master.City as city, User_master.Country as countryid, User_master.State as stateid, User_master.Phoneno as phone, " +
                         " User_master.EmailID as email, User_master.Active, StateMasterTbl.StateName as state ,CountryMaster.CountryName as country, User_master.zipcode as zip " +
                       "    FROM         " +
                       "   StateMasterTbl INNER JOIN " +
                         " User_master INNER JOIN " +
                         " CountryMaster ON User_master.Country = CountryMaster.CountryId ON StateMasterTbl.StateId = User_master.State  " +
                           " WHERE  (User_master.PartyId = '" + ddlParty.SelectedValue + "')");
        if (dr.Rows.Count > 0)
        {
            lblName.Text =Convert.ToString(dr.Rows[0]["Name"]);
            lblShippingAdd.Text  =Convert.ToString(dr.Rows[0]["Address"]);
            lblCity.Text =Convert.ToString(dr.Rows[0]["city"]);
            lblState.Text =Convert.ToString(dr.Rows[0]["state"]);
            lblCountry.Text =Convert.ToString(dr.Rows[0]["country"]);
            lblPhone.Text  =Convert.ToString(dr.Rows[0]["phone"]);
            lblzip.Text =Convert.ToString(dr.Rows[0]["zip"]);
            lblEmail.Text =Convert.ToString(dr.Rows[0]["email"]);
            lblName1.Text = Convert.ToString(dr.Rows[0]["Name"]);
            lblShippingAdd1.Text = Convert.ToString(dr.Rows[0]["Address"]);
            lblCity1.Text = Convert.ToString(dr.Rows[0]["city"]);
            lblState1.Text = Convert.ToString(dr.Rows[0]["state"]);
            lblCountry1.Text = Convert.ToString(dr.Rows[0]["country"]);
            lblPhone1.Text = Convert.ToString(dr.Rows[0]["phone"]);
            lblzip1.Text = Convert.ToString(dr.Rows[0]["zip"]);
            lblEmail1.Text = Convert.ToString(dr.Rows[0]["email"]);
          
     
          
          
        }
      
         
    }
    protected void rdinvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdinvoice.SelectedIndex >= 0)
        {
            pnlinv.Visible = true;
            if (rdinvoice.SelectedIndex == 0)
            {
                prodService = " and InventoryMaster.CatType IS NULL";
                //  pnlunit.Visible = true;
                lblorderqty.Text = "Qty Order";
                // pnlbulk.Visible = true;
                lblitemnohead.Text = "Product No.";
                lblItemHead.Text = "Item";
                //GridView1.Columns[1].HeaderText = "Product No.";
                GridView2.Columns[3].HeaderText = "Product No.";
                lbladdinv.Text = "Add Product to Sales Invoice";
                rbList.Items[1].Text = "Name/Barcode/Product No.";
                //GridView1.Columns[5].HeaderText = "Ordered Qty";
                GridView2.Columns[6].HeaderText = "Qty";
                pnlqtyo.Visible = true;
                pnlavgcost.Visible = true;
                //GridView1.Columns[14].Visible = true;
                //GridView1.Columns[15].Visible = true;
            }
            else
            {
                //GridView1.Columns[14].Visible = false;
                //GridView1.Columns[15].Visible = false;
                prodService = " and InventoryMaster.CatType='0'";
                pnlunit.Visible = false;
                lblorderqty.Text = "Number of Services Provided/Hours Ordered";
                pnlbulk.Visible = false;
                lblitemnohead.Text = "Service No.";
                lblItemHead.Text = "Service";
                //GridView1.Columns[1].HeaderText = "Service No.";
                GridView2.Columns[3].HeaderText = "Service No.";
                //GridView1.Columns[5].HeaderText = "Number of Services Provided.";
                lbladdinv.Text = "Add Service to Sales Invoice";
                rbList.Items[1].Text = "Service Name: Service Number";
                GridView2.Columns[6].HeaderText = "Number of Services Provided";
                pnlqtyo.Visible = false;
                pnlavgcost.Visible = false;
            }
            rbList_SelectedIndexChanged(sender, e);
        }
        else
        {
            prodService = "";
            pnlinv.Visible = false;
        }

    }
    public void sendmail()
    {

        string body = "";
        StringBuilder Absemp = new StringBuilder();
        StringBuilder NameInfo = new StringBuilder();
        StringBuilder Nametot = new StringBuilder();
        try
        {


            DataTable ds = select("SELECT Distinct  StateName,CountryName,CityName,Zip,  CompanyMaster.CompanyName, CompanyWebsitMaster.Sitename, CompanyWebsitMaster.SiteUrl, CompanyWebsiteAddressMaster.Address1, CompanyWebsiteAddressMaster.Phone1, CompanyWebsiteAddressMaster.Phone2, CompanyWebsiteAddressMaster.TollFree1, CompanyWebsiteAddressMaster.TollFree2, CompanyWebsiteAddressMaster.Fax,  CompanyWebsiteAddressMaster.Email,  CompanyMaster.CompanyLogo " +
 " FROM         CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId " +
 " inner join CompanyMaster ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId  where CompanyWebsitMaster.Whid='" + ddlWarehouse.SelectedValue + "'");


            StringBuilder strAddress = new StringBuilder();
            if (ds.Rows.Count > 0)
            {
                strAddress.Append("<table width=\"100%\"> ");

                //strAddress.Append("<tr><td> <img src=\"~/ShoppingCart/Images/" + ds.Rows[0]["CompanyLogo"].ToString() + "\" \"border=\"0\"  /> </td><td align=\"center\"><b><span style=\"color: #996600\">" + ds.Rows[0]["Sitename"].ToString() + "</span></b><Br><b>" + ds.Rows[0]["CompanyName"].ToString() + "</b><Br>" + ds.Rows[0]["Address1"].ToString() + "<Br><b>TollFree:</b>" + ds.Rows[0]["TollFree1"].ToString() + "," + ds.Rows[0]["TollFree2"].ToString() + "<Br><b>Phone:</b>" + ds.Rows[0]["Phone1"].ToString() + "," + ds.Rows[0]["Phone2"].ToString() + "<Br><b>Fax:</b>" + ds.Rows[0]["Fax"].ToString() + "<Br><b>Email:</b>" + ds.Rows[0]["Email"].ToString() + "<Br><b>Website:</b>" + ds.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");


                strAddress.Append("<tr> <td width=\"70%\" style=\"padding-left:10px\" align=\"left\" > <img src=\"../images/" + Convert.ToString(ds.Rows[0]["CompanyLogo"]) + "\" \"border=\"0\" Width=\"200px\" Height=\"125px\" / > </td><td style=\"padding-left:100px\" width=\"50%\" align=\"left\"><b>" + ddlWarehouse.SelectedValue + "</b><Br>" + ds.Rows[0]["Address1"].ToString() + "<Br>" + Convert.ToString(ds.Rows[0]["CityName"]) + "," + Convert.ToString(ds.Rows[0]["StateName"]) + "," + Convert.ToString(ds.Rows[0]["CountryName"]) + "<Br>" + ds.Rows[0]["Zip"].ToString() + "<Br>" + ds.Rows[0]["Phone1"].ToString() + "<Br>" + ds.Rows[0]["Email"].ToString() + " </td></tr>  </table> ");



            }

            StringBuilder Dearinfo = new StringBuilder();
            Dearinfo.Append("<table >   <tr><td >Dear </td><td>" + ddlParty.SelectedItem.Text + ",</td></tr></table>");
            StringBuilder leadinfo = new StringBuilder();
            if (rdinvoice.SelectedIndex == 0)
            {
                Dearinfo.Append("<br><br><table><tr><td>Below, please find the invoice for the recent product you ordered.</td></tr></table><br><br>");

            }
            else
            {
                Dearinfo.Append("<br><br><table><tr><td>Below, please find the invoice for the recent service we provided.</td></tr></table><br><br>");



                //ViewState["DId"]
            }
            StringBuilder Invin = new StringBuilder();
            Invin.Append("<table >   <tr><td>Invoice Information</td></tr></table>");

            NameInfo.Append("<table >   <tr><td><b>Customer Name</b></td><td>" + ddlParty.SelectedItem.Text + "</td><td width=\"20%\"></td>");
            NameInfo.Append(" <td><b>Purchase Order </b></td><td>" + txtperchaseorder.Text + "</td></tr>");
            NameInfo.Append("<tr><td><b>Date </b></td><td>" + txtGoodsDate.Text + "</td><td width=\"20%\"></td>");
            NameInfo.Append("<td><b>Terms </b></td><td>" + txtterms.Text + "</td></tr>");


            NameInfo.Append("<tr><td><b>Invoice No.</b></td><td>" + lblinvoiceno.Text + "</td><td width=\"20%\"></td>");
            NameInfo.Append("<td><b>Payment Type</b></td><td>" + rbt_pay_method.SelectedItem.Text + "</td></tr>");
            if (chkshipinfo.Checked == true)
            {
                NameInfo.Append("<tr><td><b>Transporter Name </b></td><td>" + ddlTransporter.SelectedItem.Text + "</td><td width=\"20%\"></td>");
                NameInfo.Append("<td><b>Tracking Number</b></td><td>" + txtTrackingNo.Text + "</td></tr>");
            }
            NameInfo.Append("</table> ");
            if (pnlprod.Visible == true)
            {
                Absemp.Append("<table width=\"100%\" border=\"1\">   <tr>" +
                    "<td style=\"background-color: silver\"><strong>Name</strong> </td> <td style=\"background-color: silver\"><strong>Product No.</strong> </td>");


                Absemp.Append("<td style=\"background-color: silver\"><strong>Unit</strong> </td> " +
               "<td style=\"background-color: silver\"><strong>Unit Type</strong> </td>");

                Absemp.Append("<td style=\"background-color: silver\"><strong>Ordered Qty</strong></td>");

                Absemp.Append("<td style=\"background-color: silver\"><strong>Rate</strong></td>");
                if (GridView1.Columns[7].Visible == true)
                {
                    Absemp.Append("<td style=\"background-color: silver\"><strong>Promo Rate</strong></td>");
                }

                if (GridView1.Columns[8].Visible == true)
                {
                    Absemp.Append("<td style=\"background-color: silver\"><strong>Bulk Rate</strong></td>");
                }

                if (GridView1.Columns[9].Visible == true)
                {
                    Absemp.Append("<td style=\"background-color: silver\"><strong>Applied Rate</strong></td>");
                }
                if (GridView1.Columns[10].Visible == true)
                {
                    Label ht1 = (Label)GridView1.HeaderRow.FindControl("ht1");
                    Absemp.Append("<td style=\"background-color: silver\"><strong>" + ht1 + "</strong></td>");
                }
                if (GridView1.Columns[11].Visible == true)
                {
                    Label ht2 = (Label)GridView1.HeaderRow.FindControl("ht2");
                    Absemp.Append("<td style=\"background-color: silver\"><strong>" + ht2 + "</strong></td>");
                }
                if (GridView1.Columns[12].Visible == true)
                {
                    Label ht3 = (Label)GridView1.HeaderRow.FindControl("ht3");
                    Absemp.Append("<td style=\"background-color: silver\"><strong>" + ht3 + "</strong></td>");
                }
                Absemp.Append("<td style=\"background-color: silver\"><strong>Total</strong></td>");
                Absemp.Append("</tr>");

                foreach (GridViewRow item in GridView1.Rows)
                {
                    Label lbltot = (Label)item.FindControl("lbltot");
                    Absemp.Append("<tr><td>" + item.Cells[1].Text + "</td><td>" + item.Cells[2].Text + "</td>");

                    if (rdinvoice.SelectedIndex == 0)
                    {
                        Absemp.Append("<td>" + item.Cells[3].Text + " </td> " +
                       "<td >" + item.Cells[4].Text + "</td>");
                    }
                    TextBox TextBox4 = (TextBox)item.FindControl("TextBox4");
                    Absemp.Append("<td>" + TextBox4.Text + "</td>");
                    Absemp.Append("<td>" + item.Cells[6].Text + "</td>");
                    if (GridView1.Columns[7].Visible == true)
                    {
                        Absemp.Append("<td >" + item.Cells[7].Text + "</td>");
                    }
                    if (GridView1.Columns[8].Visible == true)
                    {
                        Absemp.Append("<td >" + item.Cells[8].Text + "</td>");
                    }
                    if (GridView1.Columns[9].Visible == true)
                    {
                        Absemp.Append("<td>" + item.Cells[9].Text + "</td>");
                    }
                    if (GridView1.Columns[10].Visible == true)
                    {
                        Label lbltax112 = (Label)item.FindControl("lbltax112");
                        Absemp.Append("<td>" + lbltax112.Text + "</td>");
                    }
                    if (GridView1.Columns[11].Visible == true)
                    {
                        Label lbltax122 = (Label)item.FindControl("lbltax122");
                        Absemp.Append("<td >" + lbltax122 + "</td>");
                    }
                    if (GridView1.Columns[12].Visible == true)
                    {
                        Label lbltax132 = (Label)item.FindControl("lbltax132");
                        Absemp.Append("<td>" + lbltax132 + "</td>");
                    }
                    Absemp.Append("<td ><strong>" + lbltot.Text + "</td>");
                    Absemp.Append("</tr>");

                }

                Absemp.Append("</table> ");
            }
            if (pnlserv.Visible == true)
            {
                StringBuilder stserv = new StringBuilder();



                stserv.Append("<table width=\"100%\" border=\"1\">   <tr>" +
                "<td style=\"background-color: silver\"><strong>Name</strong> </td> <td style=\"background-color: silver\"><strong>Service No.</strong> </td> ");



                stserv.Append("<td style=\"background-color: silver\"><strong>Number of Services Provided</strong></td>");


                stserv.Append("<td style=\"background-color: silver\"><strong>Rate</strong></td>");
                if (GridView1.Columns[7].Visible == true)
                {
                    stserv.Append("<td style=\"background-color: silver\"><strong>Promo Rate</strong></td>");
                }

                if (GridView1.Columns[8].Visible == true)
                {
                    stserv.Append("<td style=\"background-color: silver\"><strong>Bulk Rate</strong></td>");
                }

                if (GridView1.Columns[9].Visible == true)
                {
                    stserv.Append("<td style=\"background-color: silver\"><strong>Applied Rate</strong></td>");
                }
                if (GridView1.Columns[10].Visible == true)
                {
                    Label ht1 = (Label)GridView1.HeaderRow.FindControl("ht1");
                    stserv.Append("<td style=\"background-color: silver\"><strong>" + ht1 + "</strong></td>");
                }
                if (GridView1.Columns[11].Visible == true)
                {
                    Label ht2 = (Label)GridView1.HeaderRow.FindControl("ht2");
                    stserv.Append("<td style=\"background-color: silver\"><strong>" + ht2 + "</strong></td>");
                }
                if (GridView1.Columns[12].Visible == true)
                {
                    Label ht3 = (Label)GridView1.HeaderRow.FindControl("ht3");
                    stserv.Append("<td style=\"background-color: silver\"><strong>" + ht3 + "</strong></td>");
                }
                stserv.Append("<td style=\"background-color: silver\"><strong>Total</strong></td>");
                stserv.Append("</tr>");

                foreach (GridViewRow item in GridView3.Rows)
                {
                    Label lbltot = (Label)item.FindControl("lbltot");
                    stserv.Append("<tr><td>" + item.Cells[1].Text + "</td><td>" + item.Cells[2].Text + "</td>");


                    stserv.Append("<td>" + item.Cells[3].Text + " </td> " +
                   "<td >" + item.Cells[4].Text + "</td>");

                    TextBox TextBox4 = (TextBox)item.FindControl("TextBox4");
                    stserv.Append("<td>" + TextBox4.Text + "</td>");
                    stserv.Append("<td>" + item.Cells[6].Text + "</td>");
                    if (GridView1.Columns[7].Visible == true)
                    {
                        stserv.Append("<td >" + item.Cells[7].Text + "</td>");
                    }
                    if (GridView1.Columns[8].Visible == true)
                    {
                        stserv.Append("<td >" + item.Cells[8].Text + "</td>");
                    }
                    if (GridView1.Columns[9].Visible == true)
                    {
                        stserv.Append("<td>" + item.Cells[9].Text + "</td>");
                    }
                    if (GridView1.Columns[10].Visible == true)
                    {
                        Label lbltax112 = (Label)item.FindControl("lbltax112");
                        stserv.Append("<td>" + lbltax112.Text + "</td>");
                    }
                    if (GridView1.Columns[11].Visible == true)
                    {
                        Label lbltax122 = (Label)item.FindControl("lbltax122");
                        stserv.Append("<td >" + lbltax122 + "</td>");
                    }
                    if (GridView1.Columns[12].Visible == true)
                    {
                        Label lbltax132 = (Label)item.FindControl("lbltax132");
                        stserv.Append("<td>" + lbltax132 + "</td>");
                    }
                    stserv.Append("<td ><strong>" + lbltot.Text + "</td>");
                    stserv.Append("</tr>");

                }

                stserv.Append("</table> ");
            }

            Nametot.Append("<br><br><table >   <tr><td><b>Gross Total: </b></td><td>" + lblGTotal.Text + "</td></tr>");
            Nametot.Append(" <tr><td><b>Total Customer Discount: </b>" + lblcusdisname.Text + "</td><td>" + lblCustDisc.Text + "</td></tr>");
            Nametot.Append("<tr><td><b>Order Value Discount: </b>" + lblcusdisname.Text + "</td><td>" + lblOrderDisc.Text + "</td></tr>");
            Nametot.Append("<tr><td><b>Net Total: </b></td><td>" + lblnettot.Text + "</td></tr>");

            if (pnltxt1.Visible == true)
            {
                Nametot.Append("<tr><td><b>" + txt1.Text + ": </b></td><td>" + txt1value.Text + "</td></tr>");

            }
            if (pnltxt2.Visible == true)
            {
                Nametot.Append("<tr><td><b>" + txt2.Text + ":  </b></td><td>" + txt2value.Text + "</td></tr>");

            }
            if (pnltxt3.Visible == true)
            {
                Nametot.Append("<tr><td><b>" + txt3.Text + ":  </b></td><td>" + txt3value.Text + "</td></tr>");

            }
            Nametot.Append("<tr><td><b>Total Tax: </b></td><td>" + lblTax.Text + "</td></tr>");
            Nametot.Append("<tr><td><b>Total Amout Due: </b></td><td>" + lblTotal.Text + "</td></tr>");
            Nametot.Append("<td></td><td></td></tr>");
            Nametot.Append("</table> ");
            string mail = "";

            StringBuilder msdd = new StringBuilder();
            msdd.Append("<br><br><table><tr><td>If you have any questions regarding this invoice, please contact us.</td></tr></table><br><br>");
            StringBuilder thank = new StringBuilder();
            thank.Append("<br><br><table><tr><td>Thank you for your business and have a great day.</td></tr></table><br><br>");


            DataTable dtpart = select("SELECT Email FROM  Party_master WHERE     (PartyId = " + Convert.ToInt32(ddlParty.SelectedValue) + ") ");
            if (dtpart.Rows.Count > 0)
            {
                mail = Convert.ToString(dtpart.Rows[0]["Email"]);
            }

            DataTable dtma = select("SELECT  OutGoingMailServer,WebMasterEmail,MasterEmailId, EmailMasterLoginPassword, AdminEmail, WHId " +
                          " FROM  CompanyWebsitMaster WHERE     (WHId = " + Convert.ToInt32(ddlWarehouse.SelectedValue) + ") ");

            if (dtma.Rows.Count > 0)
            {

                string AdminEmail = Convert.ToString(dtma.Rows[0]["MasterEmailId"]);// TextAdminEmail.Text;

                String Password = Convert.ToString(dtma.Rows[0]["EmailMasterLoginPassword"]);// TextEmailMasterLoginPassword.Text;
                System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress(AdminEmail);

                System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(mail);
                System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage(from, to);
                //emn = "<span style=\"color: #996600\">You are receiving this email as you are on the send list: Regarding lateness at work </span><b>" + empname + "</b><br>";

                StringBuilder empmanag = new StringBuilder();
                empmanag.Append("<table><tr><td>Sincerely,</td></tr><tr><td>" + ddlWarehouse.SelectedItem.Text + "</td></tr></table>");



                body = strAddress.ToString() + Dearinfo + leadinfo + Invin + NameInfo.ToString() + Absemp.ToString() + Nametot.ToString() + msdd + thank + empmanag;
                objEmail.Subject = "Retail Invoice";
                objEmail.Body = body.ToString();
                objEmail.IsBodyHtml = true;


                objEmail.Priority = System.Net.Mail.MailPriority.High;
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

                client.Credentials = new NetworkCredential(AdminEmail, Password);
                client.Host = dtma.Rows[0]["OutGoingMailServer"].ToString(); //TextOutGoingMailServer.Text;

                client.Send(objEmail);
            }

        }
        catch (Exception e)
        {

        }





    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["dt"];

        dt.Rows.Remove(dt.Rows[e.RowIndex]);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        ViewState["dt"] = dt;
        decimal ald = 0;
        decimal newt = 0;
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            TextBox txt1 = new TextBox();
            TextBox txt2 = new TextBox();
            txt1 = (TextBox)gdr.Cells[5].FindControl("TextBox4");
            Label lblinvwm = (Label)gdr.FindControl("lblinvwm");
            Int32 Invid = Convert.ToInt32(lblinvwm.Text);
            Label lbltot = (Label)gdr.FindControl("lbltot");
             decimal appl=0;
             if (Convert.ToString(gdr.Cells[9].Text) == "")
             {
                 appl = Convert.ToDecimal(gdr.Cells[6].Text);
             }
             else
             {
                 appl = Convert.ToDecimal(gdr.Cells[9].Text);
             }
            string amrtt = Convert.ToString(lbltot.Text);
            ald += Convert.ToDecimal(amrtt);
            decimal amtchange = Math.Round(Convert.ToDecimal(txt1.Text) * appl, 2);
            lbltot.Text = amtchange.ToString();
            newt += amtchange;
        }
        if (Convert.ToString(ViewState["type"]) == "1" || Convert.ToString(ViewState["type"]) == "2")
        {

            ctax();
        }
        else
        {
            gridtax();
        }
        lblGTotal.Text = newt.ToString();
        //lblMsg.Text = newt.ToString() + " RR " + ald.ToString();
        decimal deff = newt - ald;
        lblTotal.Text = Convert.ToString(Convert.ToDecimal(lblGTotal.Text) - Convert.ToDecimal(lblCustDisc.Text) - Convert.ToDecimal(lblOrderDisc.Text) + Convert.ToDecimal(lblTax.Text));
        lblnettot.Text = Convert.ToString(Convert.ToDecimal(lblGTotal.Text) - Convert.ToDecimal(lblCustDisc.Text) - Convert.ToDecimal(lblOrderDisc.Text));

        totalpartydis = 0;
        totalvolumedis = 0;
        totalpromtionaldis = 0;
        fillfoter();
        //btnUpdate_Click(sender, e);
    }

    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["dt1"];

        dt.Rows.Remove(dt.Rows[e.RowIndex]);
        GridView3.DataSource = dt;
        GridView3.DataBind();
        ViewState["dt1"] = dt;
        decimal ald = 0;
        decimal newt = 0;
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            TextBox txt1 = new TextBox();
            TextBox txt2 = new TextBox();
            txt1 = (TextBox)gdr.Cells[5].FindControl("TextBox4");
            Label lblinvwm = (Label)gdr.FindControl("lblinvwm");
            Int32 Invid = Convert.ToInt32(lblinvwm.Text);
            Label lbltot = (Label)gdr.FindControl("lbltot");
            decimal appl = Convert.ToDecimal(gdr.Cells[9].Text);
            string amrtt = Convert.ToString(lbltot.Text);
            ald += Convert.ToDecimal(amrtt);
            decimal amtchange = Math.Round(Convert.ToDecimal(txt1.Text) * appl, 2);
            lbltot.Text = amtchange.ToString();
            newt += amtchange;
        }
        if (Convert.ToString(ViewState["type"]) == "1" || Convert.ToString(ViewState["type"]) == "2")
        {

            ctax();
        }
        else
        {
            gridtax();
        }
        lblGTotal.Text = newt.ToString();
        //lblMsg.Text = newt.ToString() + " RR " + ald.ToString();
        decimal deff = newt - ald;
        lblTotal.Text = Convert.ToString(Convert.ToDecimal(lblGTotal.Text) - Convert.ToDecimal(lblCustDisc.Text) - Convert.ToDecimal(lblOrderDisc.Text) + Convert.ToDecimal(lblTax.Text));
        lblnettot.Text = Convert.ToString(Convert.ToDecimal(lblGTotal.Text) - Convert.ToDecimal(lblCustDisc.Text) - Convert.ToDecimal(lblOrderDisc.Text));

        totalpartydis = 0;
        totalvolumedis = 0;
        totalpromtionaldis = 0;
        fillfoter();
        //btnUpdate_Click(sender, e);
    }
    protected void fillfoter()
    {
        double tota = 0;
        double t1 = 0;
        double t2 = 0;
        double t3 = 0;
        double qt = 0;
        double avgtot = 0;
        double markuptot = 0;
        if (GridView1.Rows.Count > 0)
        {
            foreach (GridViewRow item in GridView1.Rows)
            {
                Label lblavgcost = (Label)item.FindControl("lblavgcost");
                Label lblmarkup = (Label)item.FindControl("lblmarkup");
                Label lbltot = (Label)item.FindControl("lbltot");
                Label lbltax132 = (Label)item.FindControl("lbltax132");
                Label lbltax122 = (Label)item.FindControl("lbltax122");
                Label lbltax112 = (Label)item.FindControl("lbltax112");
                tota += Convert.ToDouble(lbltot.Text);
                t1 += Convert.ToDouble(lbltax112.Text);
                t2 += Convert.ToDouble(lbltax122.Text);
                t3 += Convert.ToDouble(lbltax132.Text);
                avgtot += Convert.ToDouble(lblavgcost.Text);
                markuptot += Convert.ToDouble(lblmarkup.Text);
                TextBox TextBox4 = (TextBox)item.FindControl("TextBox4");
                qt += Convert.ToDouble(TextBox4.Text);
            }
            GridView1.FooterRow.Cells[14].ForeColor = System.Drawing.Color.Black;
            GridView1.FooterRow.Cells[15].ForeColor = System.Drawing.Color.Black;
            GridView1.FooterRow.Cells[13].ForeColor = System.Drawing.Color.Black;
            GridView1.FooterRow.Cells[10].ForeColor = System.Drawing.Color.Black;
            GridView1.FooterRow.Cells[11].ForeColor = System.Drawing.Color.Black;
            GridView1.FooterRow.Cells[12].ForeColor = System.Drawing.Color.Black;
            GridView1.FooterRow.Cells[5].ForeColor = System.Drawing.Color.Black;
            GridView1.FooterRow.Cells[13].Text = Math.Round(tota, 2).ToString();
            GridView1.FooterRow.Cells[10].Text = Math.Round(t1, 2).ToString();
            GridView1.FooterRow.Cells[11].Text = Math.Round(t2, 2).ToString();
            GridView1.FooterRow.Cells[12].Text = Math.Round(t3, 2).ToString();
            GridView1.FooterRow.Cells[14].Text = Math.Round(avgtot, 2).ToString();
            GridView1.FooterRow.Cells[15].Text = Math.Round(markuptot, 2).ToString();
            GridView1.FooterRow.Cells[5].Text = qt.ToString();
        }
        tota = 0;
        t1 = 0;
        t2 = 0;
        t3 = 0;
        qt = 0;
        if (GridView3.Rows.Count > 0)
        {

            foreach (GridViewRow item in GridView3.Rows)
            {
                Label lbltot = (Label)item.FindControl("lbltot");
                Label lbltax132 = (Label)item.FindControl("lbltax132");
                Label lbltax122 = (Label)item.FindControl("lbltax122");
                Label lbltax112 = (Label)item.FindControl("lbltax112");
                tota += Convert.ToDouble(lbltot.Text);
                t1 += Convert.ToDouble(lbltax112.Text);
                t2 += Convert.ToDouble(lbltax122.Text);
                t3 += Convert.ToDouble(lbltax132.Text);
                TextBox TextBox4 = (TextBox)item.FindControl("TextBox4");
                qt += Convert.ToDouble(TextBox4.Text);
            }
            GridView3.FooterRow.Cells[13].ForeColor = System.Drawing.Color.Black;
            GridView3.FooterRow.Cells[10].ForeColor = System.Drawing.Color.Black;
            GridView3.FooterRow.Cells[11].ForeColor = System.Drawing.Color.Black;
            GridView3.FooterRow.Cells[12].ForeColor = System.Drawing.Color.Black;
            GridView3.FooterRow.Cells[5].ForeColor = System.Drawing.Color.Black;
            GridView3.FooterRow.Cells[13].Text = Math.Round(tota, 2).ToString();
            GridView3.FooterRow.Cells[10].Text = Math.Round(t1, 2).ToString();
            GridView3.FooterRow.Cells[11].Text = Math.Round(t2, 2).ToString();
            GridView3.FooterRow.Cells[12].Text = Math.Round(t3, 2).ToString();
            GridView3.FooterRow.Cells[5].Text = qt.ToString();
        }
    }
    protected void btAddBill_Click(object sender, EventArgs e)
    {
        if (txtNameb.Visible == false)
        {
            btAddBill.Text = "Save";
            txtNameb.Text = lblName.Text;
            
            txtShippingAddb.Text = lblShippingAdd.Text;
            txtCityb.Text = lblCity.Text;
            txtStateb.Text = lblState.Text;
            txtCountryb.Text = lblCountry.Text;
            txtPhoneb.Text = lblPhone.Text;
            txtzipb.Text = lblzip.Text;
            TxtEmailb.Text = lblEmail.Text;
            
            
            txtNameb.Visible = true;
            txtShippingAddb.Visible = true;
            txtCityb.Visible = true;
            txtStateb.Visible = true;
            txtCountryb.Visible = true;
            txtPhoneb.Visible = true;
            txtzipb.Visible = true;
            TxtEmailb.Visible = true;

            lblName.Text = "Name";
            lblShippingAdd.Text = "Address";
            lblCity.Text = "City";
            lblState.Text = "State";
            lblCountry.Text = "Country";
            lblPhone.Text = "Phone";
            lblzip.Text = "Zip Code";
            lblEmail.Text = "Email";
          
        }
        else
        {
            btAddBill.Text = "Change";
            lblName.Text = txtNameb.Text;
          
            lblShippingAdd.Text = txtShippingAddb.Text;
            lblCity.Text = txtCityb.Text;
            lblState.Text = txtStateb.Text;
            lblCountry.Text = txtCountryb.Text;
            lblPhone.Text = txtPhoneb.Text;
            lblzip.Text = txtzipb.Text;
            lblEmail.Text = TxtEmailb.Text;
            //lblName.Visible = true;
            //lblShippingAdd.Visible = true;
            //lblCity.Visible = true;
            //lblState.Visible = true;
            //lblCountry.Visible = true;
            //lblPhone.Visible = true;
            //lblzip.Visible = true;
            //lblEmail.Visible = true;

            txtNameb.Visible = false;
            txtShippingAddb.Visible = false;
            txtCityb.Visible = false;
            txtStateb.Visible = false;
            txtCountryb.Visible = false;
            txtPhoneb.Visible = false;
            txtzipb.Visible = false;
            TxtEmailb.Visible = false;
            chkbill_CheckedChanged(sender, e);
           
        }
    }
    protected void chkbill_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbill.Checked)
        {
            btnship.Visible = false;
            lblName1.Text = lblName.Text;

            lblShippingAdd1.Text = lblShippingAdd.Text;
            lblCity1.Text = lblCity.Text;
            lblState1.Text = lblState.Text;
            lblCountry1.Text = lblCountry.Text;
            lblPhone1.Text = lblPhone.Text;
            lblzip1.Text = lblzip.Text;
            lblEmail1.Text = lblEmail.Text;
            txtName1.Visible = false;
            txtShippingAdd1.Visible = false;
            txtCity1.Visible = false;
            txtState1.Visible = false;
            txtCountry1.Visible = false;
            txtPhone1.Visible = false;
            txtzip1.Visible = false;
            txtEmail1.Visible = false;
        }
        else
        {
            chkbill.Visible = false;
            btnship.Visible = true;
            txtName1.Visible = true;
            txtShippingAdd1.Visible = true;
            txtCity1.Visible = true;
            txtState1.Visible = true;
            txtCountry1.Visible = true;
            txtPhone1.Visible = true;
            txtzip1.Visible = true;
            txtEmail1.Visible = true;
            txtName1.Text = lblName1.Text;
            txtShippingAdd1.Text = lblShippingAdd1.Text;
            txtCity1.Text = lblCity1.Text;
            txtState1.Text = lblState1.Text;
            txtCountry1.Text = lblCountry1.Text;
            txtPhone1.Text = lblPhone1.Text;
            txtzip1.Text = lblzip1.Text;
            txtEmail1.Text = lblEmail1.Text;
            lblName1.Text = "Name";
            lblShippingAdd1.Text = "Address";
            lblCity1.Text = "City";
            lblState1.Text = "State";
            lblCountry1.Text = "Country";
            lblPhone1.Text = "Phone";
            lblzip1.Text = "Zip Code";
            lblEmail1.Text = "Email";
        }
    }
    protected void imgadddivision_Click(object sender, ImageClickEventArgs e)
    {
        string te = "VendorPartyRegister.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        filltrans();
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
    protected void chkshipinfo_CheckedChanged(object sender, EventArgs e)
    {
        if (chkshipinfo.Checked == true)
        {
            pnlshipinfo.Visible = true;
        }
        else
        {
            pnlshipinfo.Visible = false;
        }
    }
    protected void textqty_TextChanged(object sender, EventArgs e)
    {
        double qt = 0;
        foreach (GridViewRow it in GridView1.Rows)
        {
            TextBox TextBox4 = (TextBox)it.FindControl("TextBox4");
            qt += Convert.ToDouble(TextBox4.Text);

        }
         GridView1.FooterRow.Cells[5].ForeColor = System.Drawing.Color.Black;
         GridView1.FooterRow.Cells[5].Text = qt.ToString();
        
    }

    protected void btncheque_Click(object sender, EventArgs e)
    {
        if (Panel1.Visible == false)
        {
            Panel1.Visible = true;
            btncheque.Text = "Hide Cheque Details";
        }
        else
        {
            Panel1.Visible = false; btncheque.Text = "Add Cheque Details";
        }

    }
    protected void btnaddcusm_Click(object sender, ImageClickEventArgs e)
    {

        string te = "WizardCompanyWebsitMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void cusmre_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtma = select("SELECT  OutGoingMailServer,WebMasterEmail,MasterEmailId, EmailMasterLoginPassword, AdminEmail, WHId " +
                           " FROM  CompanyWebsitMaster WHERE     (WHId = " + Convert.ToInt32(ddlWarehouse.SelectedValue) + ") ");

        if (dtma.Rows.Count > 0)
        {
            chkcust.Enabled = true;
            chkcust.ToolTip = "";
            btnaddcusm.Visible = false;
            cusmre.Visible = false;
        }
        else
        {
            btnaddcusm.Visible = false;
            cusmre.Visible = false;
            chkcust.Enabled = false;
            chkcust.ToolTip = "There is no email set for this customer.";
        }
    }

    protected void btnship_Click(object sender, EventArgs e)
    {

        if (btnship.Text == "Save")
        {
            lblName1.Text = txtName1.Text;
            chkbill.Visible = true;
            lblShippingAdd1.Text = txtShippingAdd1.Text;
            lblCity1.Text = txtCity1.Text;
            lblState1.Text = txtState1.Text;
            lblCountry1.Text = txtCountry1.Text;
            lblPhone1.Text = txtPhone1.Text;
            lblzip1.Text = txtzip1.Text;
            lblEmail1.Text = txtEmail1.Text;
            btnship.Text = "Change";
            txtName1.Visible = false;
            txtShippingAdd1.Visible = false;
            txtCity1.Visible = false;
            txtState1.Visible = false;
            txtCountry1.Visible = false;
            txtPhone1.Visible = false;
            txtzip1.Visible = false;
            txtEmail1.Visible = false;
        }
        else
        {
            btnship.Text = "Save";
            lblName1.Text = "Name";
            lblShippingAdd1.Text = "Address";
            lblCity1.Text = "City";
            lblState1.Text = "State";
            lblCountry1.Text = "Country";
            lblPhone1.Text = "Phone";
            lblzip1.Text = "Zip Code";
            lblEmail1.Text = "Email";
            txtName1.Visible = true;
            txtShippingAdd1.Visible = true;
            txtCity1.Visible = true;
            txtState1.Visible = true;
            txtCountry1.Visible = true;
            txtPhone1.Visible = true;
            txtzip1.Visible = true;
            txtEmail1.Visible = true;
        }
    }
  
    protected void txtGoodsDate_TextChanged(object sender, EventArgs e)
    {
        if (txtnumberofduedate.Text == "")
        {
            txtnumberofduedate.Text = "0";
        }

        if (txtGoodsDate.Text == "")
        {
            txtGoodsDate.Text = DateTime.Now.ToShortDateString();
        }
        txtpayduedate.Text = Convert.ToDateTime(txtGoodsDate.Text).AddDays(Convert.ToInt32(txtnumberofduedate.Text)).ToShortDateString();



        if (rbt_pay_method.SelectedValue == "4")
        {
            pnlamtdue.Visible = true;
        }
        else
        {
            pnlamtdue.Visible = false;
        }
    }
}

