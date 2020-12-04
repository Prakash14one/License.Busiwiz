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
using System.Data.Sql;
using System.Text;
using System.Net;

public partial class CustomerDCEdit : System.Web.UI.Page
{
    object com;
    
    SqlConnection con=new SqlConnection( PageConn.connnn);
    double FINALANSWER = 0.00;
    double volumediscount12 = 0.00;
    double prodis = 0.00;
    double hdcharges = 0.00;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        //      PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
            pagetitleclass pg = new pagetitleclass();
            string strData = Request.Url.ToString();

            char[] separator = new char[] { '/' };

            string[] strSplitArr = strData.Split(separator);
            int i = Convert.ToInt32(strSplitArr.Length);
            string page = strSplitArr[i - 1].ToString();


            Page.Title = pg.getPageTitle(page);
            
            lblDate.Text = System.DateTime.Now.ToShortDateString();
        
        if (!Page.IsPostBack)
        {
            com = Session["comid"];
            ViewState["attachment"] = "no attachmnet";
           

                hdnWHid.Value = "0";
               
                 
                    fillwarehouse(); 
                
            
            string qry="select distinct StatusCategory.StatusCategory,StatusMaster.StatusName,StatusMaster.StatusId from StatusCategory inner join StatusMaster on StatusMaster.StatusCategoryMasterId=StatusCategory.StatusCategoryMasterId  where StatusCategory.StatusCategoryMasterId  in('31') order by StatusMaster.StatusName";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            ddlSalesOrdStatus.DataSource = ds;
            ddlSalesOrdStatus.DataBind();
            ddlSalesOrdStatus.DataTextField = "StatusName";
            ddlSalesOrdStatus.DataValueField = "StatusId";
            ddlSalesOrdStatus.DataBind();
            ddlSalesOrdStatus.Items.Insert(0, "All");
           
            ddlSalesOrdStatus.Items[0].Value = "0";
         
           
          
         


          

         

            if (Request.QueryString["id1"] != null)
            {
                ViewState["DcNo1"] = Request.QueryString["id1"];
                DataTable drt = select("select TransactionMasterSalesChallanTbl.SalesChallanMasterId,SalesChallanDatetime,TransactionMasterId,SalesOrderMasterId from SalesChallanMaster inner join  TransactionMasterSalesChallanTbl ON TransactionMasterSalesChallanTbl.SalesChallanMasterId=SalesChallanMaster.SalesChallanMasterId where TransactionMasterSalesChallanTbl.SalesChallanMasterId='" + ViewState["DcNo1"] + "'");
                if (drt.Rows.Count > 0)
                {
                    ViewState["tid"] = Convert.ToString(drt.Rows[0]["TransactionMasterId"]);
                     ViewState["ordNo"] = Convert.ToString(drt.Rows[0]["SalesOrderMasterId"]);
                     DataTable frr = select("SELECT  max(EntryNumber) as entrynoInv FROM TranctionMaster where Tranction_Master_Id='" + ViewState["tid"] + "'");
                     if (frr.Rows.Count > 0)
                     {

                         ViewState["InNo1"] = Convert.ToString(frr.Rows[0]["entrynoInv"]);
                     }
                     DataTable frred = select("SELECT  Whid FROM TranctionMaster where Tranction_Master_Id='" + ViewState["tid"] + "'");
                     if (frred.Rows.Count > 0)
                     {

                         ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByValue(frred.Rows[0]["Whid"].ToString()));
               
                     }
                    ddlWarehouse_SelectedIndexChanged(sender, e);
            
                     fillorder();
                    ddlRefSellno.SelectedIndex = ddlRefSellno.Items.IndexOf(ddlRefSellno.Items.FindByValue(ViewState["ordNo"].ToString()));
                     ddlRefSellno_SelectedIndexChanged(sender, e);
                     lblDate.Text = Convert.ToDateTime(drt.Rows[0]["SalesChallanDatetime"]).ToShortDateString();
                     ViewState["sdo"] = lblDate.Text;
                     ViewState["sdo1"] = lblDate.Text;
 
                     DataTable dfs = select(" select Notes,SalesChallanMoreInfo.* from SalesChallanDetail inner join SalesChallanMoreInfo on SalesChallanMoreInfo.SalesChallanMasterId=SalesChallanDetail.SalesChallanMasterId  where SalesChallanDetail.SalesChallanMasterId='" + ViewState["DcNo1"] + "'");
                     if (dfs.Rows.Count > 0)
                     {
                        

                         ddlShippers.DataSource = (DataTable)FillShippers();
                         ddlShippers.DataTextField = "ShippersName";
                         ddlShippers.DataValueField = "ShippersId";
                         ddlShippers.DataBind();
                         ddlShippers.Items.Insert(0, "-select-");
                         ddlShippers.SelectedItem.Value = "0";
                         ddlShippers.SelectedIndex = ddlShippers.Items.IndexOf(ddlShippers.Items.FindByValue(dfs.Rows[0]["ShippersId"].ToString()));
                         ddlShippers_SelectedIndexChanged(sender, e);
                         ddlshipoption.SelectedIndex = ddlshipoption.Items.IndexOf(ddlshipoption.Items.FindByValue(dfs.Rows[0]["ShippersShipOptionMasterId"].ToString()));
                         ddlshipoption_SelectedIndexChanged(sender, e);
                         ddlShippingPerson.SelectedIndex = ddlShippingPerson.Items.IndexOf(ddlShippingPerson.Items.FindByValue(dfs.Rows[0]["ShippingPersonId"].ToString()));
                         ddlShippingPerson_SelectedIndexChanged(sender, e);
                         txtTrckNo.Text = Convert.ToString(dfs.Rows[0]["ShippersTrackingNo"]);
                         txtRecieptNo.Text = Convert.ToString(dfs.Rows[0]["RecieptNo"]);
                         txtNote.Text = Convert.ToString(dfs.Rows[0]["Notes"]);
                         txtGoodsDate.Text = Convert.ToString(dfs.Rows[0]["Terms"]);
                         txtShippersDocNo.Text = Convert.ToString(dfs.Rows[0]["PurchaseOrder"]);


                     }
                    
                     DataTable frr1 = select("select MAX(SalesNo) as SalesNo from SalesChallanMaster  where SalesChallanMasterId='" + ViewState["DcNo1"] + "'");
                     if (frr1.Rows.Count > 0)
                     {

                         Session["salesno"] = Convert.ToString(frr1.Rows[0]["SalesNo"]);
                     }
                }

            }

           
        }


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
    public DataTable select(string str)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(str, con);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        adp.Fill(dt);
        return dt;
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

        DataColumn dtcom1 = new DataColumn();
        dtcom1.DataType = System.Type.GetType("System.String");
        dtcom1.ColumnName = "SalesOrderName";
        dtcom1.ReadOnly = false;
        dtcom1.Unique = false;
        dtcom1.AllowDBNull = true;
        dt.Columns.Add(dtcom1);

       

        String qryStr = "";

        String salodert = "";
        if (ddlSalesordertype.SelectedIndex == 1)
        {
            salodert = " and SalesOrderMaster.OrderType IS NULL";
        }
        else if(ddlSalesordertype.SelectedIndex == 2)
        {
            salodert = " and SalesOrderMaster.OrderType='0'";
        }
       
            DataTable dts = select(" select distinct top(20) SalesOrderMaster.SalesOrderId FROM  SalesOrderMaster INNER JOIN " +
            " Party_master ON SalesOrderMaster.PartyId = Party_master.PartyID inner join StatusControl " +
            " on StatusControl.SalesOrderId=SalesOrderMaster.SalesOrderId inner join StatusMaster on" +
            " StatusMaster.StatusId=StatusControl.StatusMasterId where SalesOrderMaster.SalesOrderId='" + ViewState["ordNo"] + "' and " +
 "  StatusMaster.StatusCategoryMasterId='31'" + salodert +
            "  order by SalesOrderMaster.SalesOrderId Desc");
            foreach (DataRow item in dts.Rows)
            {
                DataTable ds = new DataTable();
                qryStr = "select distinct top(1) StatusControlId, SalesOrderMaster.SalesOrderId,convert(nvarchar(10),SalesOrderMaster.SalesOrderNo,101)+' : '+Left(Party_master.Compname,20)+' : '+ convert(nvarchar(10),SalesOrderMaster.SalesOrderDate,101)+' : '+StatusMaster.StatusName AS SalOrdMst, Party_master.Compname, SalesOrderMaster.PartyId FROM  SalesOrderMaster INNER JOIN Party_master ON SalesOrderMaster.PartyId = Party_master.PartyID inner join StatusControl  on StatusControl.SalesOrderId=SalesOrderMaster.SalesOrderId inner join StatusMaster on StatusMaster.StatusId=StatusControl.StatusMasterId where SalesOrderMaster.SalesOrderId='" + item["SalesOrderId"] + "' AND StatusMaster.StatusCategoryMasterId='31'   order by StatusControlId Desc ";
                SqlCommand cmd = new SqlCommand(qryStr, con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(ds);
                foreach (DataRow dr in ds.Rows)
                {
                    DataRow dtrow = dt.NewRow();
                    dtrow["SalesOrderId"] = Convert.ToInt32(dr["SalesOrderId"]);
                    dtrow["SalesOrderName"] = Convert.ToString(dr["SalOrdMst"]);
                    dt.Rows.Add(dtrow);
                }
            }
            
        
        DataView dv = dt.DefaultView;
        dv.Sort = "SalesOrderId  Desc";

        return dv;

    }

    protected bool isfromOnlineSaleorno(int soid)
    {
        try
        {
            
            string strfromtrm = "SELECT     TranctionMaster.EntryTypeId, TransactionMasterMoreInfo.SalesOrderId "+
                    " FROM         TranctionMaster INNER JOIN "+
                    "  TransactionMasterMoreInfo ON TranctionMaster.Tranction_Master_Id = TransactionMasterMoreInfo.Tranction_Master_Id "+
                    " WHERE     (TranctionMaster.EntryTypeId = 26) AND (TransactionMasterMoreInfo.SalesOrderId = '" + soid + "')";
            SqlCommand cmd123 = new SqlCommand(strfromtrm, con);
            SqlDataAdapter dbss117 = new SqlDataAdapter(cmd123);
            DataTable chekisitornot = new DataTable();
            dbss117.Fill(chekisitornot);
            //DataTable chekisitornot = dbss1.cmdSelect(strfromtrm);
            if (chekisitornot.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }

    }

    public DataTable fillPaidBy()
    {
        SqlCommand cmd = new SqlCommand("SELECT AccountName, AccountId, GroupId " +
            " FROM  AccountMaster WHERE  (GroupId = 1) order by AccountName", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;

    }
    public DataTable fillParty()
    {
        string str = "SELECT     PartyID, Account, Compname, Address, City, State, Country FROM  Party_master where id='"+Session["comid"]+"' and Whid='"+ddlWarehouse.SelectedValue+"' order by Compname";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;

    }
    public DataTable FillFromAddress()
    {
        string str = "SELECT WareHouseId, Name FROM WareHouseMaster where comid  " +
            " ='" + com +"' order by Name";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;

    }
    public int FillDelNo()
    {
        int i = 1;
        string str = "SELECT MAX(SalesChallanMasterId) AS DelNo FROM  SalesChallanMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            if (ds.Rows[0]["DelNo"].ToString() != "")
            {


                i = Convert.ToInt32(ds.Rows[0]["DelNo"]) + 1;
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
        return i;
    }
    public DataTable FillShippers()
    {
        string str = "SELECT  ShippersName, ShippersId FROM  ShippersMaster where compid= "+
            " '" + Session["comid"] +"' order by ShippersName";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;
    }
    public DataTable fillShipperPerson()
    {
       
        string str = "SELECT     User_master.UserID, User_master.Name, Party_master.PartyTypeId ,  Party_master.Compname" +
                        " FROM         User_master INNER JOIN " +
                      " Party_master ON User_master.PartyID = Party_master.PartyID " +
                    " WHERE     Party_master.id='" + Session["comid"] + "' and Party_master.Whid='" + ddlWarehouse.SelectedValue + "' order by User_master.Name ";
      
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;

    }
    protected void ddlShippers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlShippers.SelectedIndex != 0)
        {
            string str = "SELECT  ShippersShipOptionMasterId, OptionName, ShippersId FROM   ShippersShipOptionMaster " +
                      " WHERE     (ShippersId = '" + ddlShippers.SelectedValue + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            ddlshipoption.DataSource = ds;
            ddlshipoption.DataTextField = "OptionName";
            ddlshipoption.DataValueField = "ShippersShipOptionMasterId";
            ddlshipoption.DataBind();
            ddlshipoption.Items.Insert(0, "-select-");
            ddlshipoption.Items[0].Value = "0";
        }
            else
        {
            ddlshipoption.Items.Insert(0, "-select-");
            ddlshipoption.Items[0].Value = "0";
            ddlshipoption.Items.Clear();
        }

        

    }
    public DataTable fillShippingOption()
    {
        string str = "SELECT  ShippersShipOptionMasterId, OptionName, ShippersId FROM   ShippersShipOptionMaster " +
                      " WHERE     (ShippersId = '" + ddlShippers.SelectedValue + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        ddlshipoption.DataSource = ds;
        ddlshipoption.DataTextField = "OptionName";
        ddlshipoption.DataValueField = "ShippersShipOptionMasterId";
        ddlshipoption.DataBind();
        ddlshipoption.Items.Insert(0, "-select-");
        ddlshipoption.Items[0].Value = "0";


        return ds;

    }
    protected int isintornot(string ckkk)
    {
        int i =0;
        try
        {
            i = Convert.ToInt32(ckkk);
            return i;
        }
        catch
        {
            return i;
        }
    }
    protected void ddlRefSellno_SelectedIndexChanged(object sender, EventArgs e)
    {
        int fl = 0;
       
        lblmsg.Text = "";
        if (ddlRefSellno.SelectedIndex > 0)
        {
            string str = " SELECT  Distinct  OrderType, Party_master.PartyID,Party_master.Contactperson, Party_master.Compname, ShippingAddress.ShippingAddressID, ShippingAddress.Name, ShippingAddress.Email, ShippingAddress.Address,  " +
                          " ShippingAddress.City, ShippingAddress.State, ShippingAddress.Country, ShippingAddress.Phone, ShippingAddress.Zipcode, BillingAddress.BillingAddressId,  " +
                          " BillingAddress.Name AS bname, BillingAddress.Email AS BEmail, BillingAddress.Address AS BAddress, BillingAddress.City AS BCity, BillingAddress.State AS BState, " +
                          " BillingAddress.Country AS BCountry, BillingAddress.Phone AS BPhone, BillingAddress.Zipcode AS BZipcode, SalesOrderMaster.SalesOrderId " +
     " FROM         SalesOrderMaster LEFT OUTER JOIN " +
                           " Party_master ON SalesOrderMaster.PartyId = Party_master.PartyID LEFT OUTER JOIN " +
                          "  BillingAddress ON SalesOrderMaster.SalesOrderId = BillingAddress.SalesOrderId LEFT OUTER JOIN " +
                       " ShippingAddress ON SalesOrderMaster.SalesOrderId = ShippingAddress.SalesOrderId " +
   " WHERE     (SalesOrderMaster.SalesOrderId = '" + ddlRefSellno.SelectedValue + "') and Party_master.id " + "   ='" + Session["comid"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                if (Convert.ToString(ds.Rows[0]["OrderType"]) == "")
                {
                    pnlcustprefer.Visible = true;
                    pnlcustpresh.Visible = true;
                }
                else
                {
                    pnlcustprefer.Visible = false;
                    pnlcustpresh.Visible = false;
                }
                ViewState["pp"] = ds.Rows[0]["PartyID"].ToString();
                ddlParty.SelectedIndex = ddlParty.Items.IndexOf(ddlParty.Items.FindByValue(ds.Rows[0]["PartyID"].ToString()));
                lblPartyName.Text = ddlParty.SelectedItem.Text;
                lblcpers.Text = Convert.ToString(ds.Rows[0]["Contactperson"]);
                //string billaddress = "Name:" + ds.Rows[0]["bname"].ToString() + "<br>Address:" + ds.Rows[0]["BAddress"].ToString() + "<br>Mail:" + ds.Rows[0]["BEmail"].ToString() + "<br>City:" + ds.Rows[0]["BCity"].ToString() + "<br>State:" + ds.Rows[0]["BState"].ToString() + "<br>Country:" + ds.Rows[0]["BCountry"].ToString() + "<br>Phone:" + ds.Rows[0]["BPhone"].ToString() + "<br>Zip:" + ds.Rows[0]["BZipcode"].ToString() + "<br>";
                //string Shipaddress = "Name:" + ds.Rows[0]["Name"].ToString() + "<br>Address:" + ds.Rows[0]["Address"].ToString() + "<br>Mail:" + ds.Rows[0]["Email"].ToString() + "<br>City:" + ds.Rows[0]["City"].ToString() + "<br>State:" + ds.Rows[0]["State"].ToString() + "<br>Country:" + ds.Rows[0]["Country"].ToString() + "<br>Phone:" + ds.Rows[0]["Phone"].ToString() + "<br>Zip:" + ds.Rows[0]["Zipcode"].ToString() + "<br>";
                string billaddress = ds.Rows[0]["BAddress"].ToString() + "<br>" + ds.Rows[0]["BCity"].ToString() + ", " + ds.Rows[0]["BState"].ToString() + ", " + ds.Rows[0]["BCountry"].ToString() + "<br>" + ds.Rows[0]["BZipcode"].ToString() + "<br>" + ds.Rows[0]["BPhone"].ToString() + "<br>" + ds.Rows[0]["BEmail"].ToString() + "<br>";
                string Shipaddress = ds.Rows[0]["Address"].ToString() + "<br>" + ds.Rows[0]["City"].ToString() + ", " + ds.Rows[0]["State"].ToString() + ", " + ds.Rows[0]["Country"].ToString() + "<br>" + ds.Rows[0]["Zipcode"].ToString() + "<br>" + ds.Rows[0]["Phone"].ToString() + "<br>" + ds.Rows[0]["Email"].ToString() + "<br>";

                string getidofcity = " SELECT     ShippingAddress.ShippingAddressID, ShippingAddress.SalesOrderId, ShippingAddress.Name, " +
                    " ShippingAddress.Email, ShippingAddress.Address,  " +
                         "  ShippingAddress.City, ShippingAddress.State, ShippingAddress.Country, ShippingAddress.Phone, ShippingAddress.Zipcode," +
                         " CityMasterTbl.CityId,  " +
                      "     StateMasterTbl.CountryId, StateMasterTbl.StateId " +
                     " FROM         ShippingAddress LEFT OUTER JOIN " +
                       "    StateMasterTbl ON ShippingAddress.State = StateMasterTbl.StateName LEFT OUTER JOIN " +
                       "    CityMasterTbl ON ShippingAddress.City = CityMasterTbl.CityName " +
                      "  WHERE     (ShippingAddress.SalesOrderId = '" + ddlRefSellno.SelectedValue + "') ";
                SqlCommand cmd11111 = new SqlCommand(getidofcity, con);
                SqlDataAdapter dbss11 = new SqlDataAdapter(cmd11111);
                //DataTable dtiiiiirefid = dbss1.cmdSelect(getidofcity);
                DataTable dtiiiiirefid = new DataTable();
                dbss11.Fill(dtiiiiirefid);
                int cttttid = 0;
                int stiiid = 0;
                int countriiid = 0;
                if (dtiiiiirefid.Rows.Count > 0)
                {
                    cttttid = isintornot(dtiiiiirefid.Rows[0]["CityId"].ToString());
                    stiiid = isintornot(dtiiiiirefid.Rows[0]["StateId"].ToString());
                    countriiid = isintornot(dtiiiiirefid.Rows[0]["CountryId"].ToString());
                }

                Session["city"] = cttttid;
                Session["state"] = stiiid;
                Session["country"] = countriiid;




                lblBillAddress.Text = billaddress.ToString();
                lblShipAddress.Text = Shipaddress.ToString();

            }
            DataTable dt = new DataTable();
            DataTable dts = new DataTable();
            DataColumn dtcom = new DataColumn();
            DataColumn dtcom1s = new DataColumn();
            dtcom.DataType = System.Type.GetType("System.String");
            dtcom.ColumnName = "ProductNo";
            dtcom.ReadOnly = false;
            dtcom.Unique = false;
            dtcom.AllowDBNull = true;

            dtcom1s.DataType = System.Type.GetType("System.String");
            dtcom1s.ColumnName = "ProductNo";
            dtcom1s.ReadOnly = false;
            dtcom1s.Unique = false;
            dtcom1s.AllowDBNull = true;
            dt.Columns.Add(dtcom);
            dts.Columns.Add(dtcom1s);


            DataColumn dtcomst = new DataColumn();
            DataColumn dtcom1sst = new DataColumn();
            dtcomst.DataType = System.Type.GetType("System.String");
            dtcomst.ColumnName = "QtyShort";
            dtcomst.ReadOnly = false;
            dtcomst.Unique = false;
            dtcomst.AllowDBNull = true;

            dtcom1sst.DataType = System.Type.GetType("System.String");
            dtcom1sst.ColumnName = "QtyShort";
            dtcom1sst.ReadOnly = false;
            dtcom1sst.Unique = false;
            dtcom1sst.AllowDBNull = true;
            dt.Columns.Add(dtcomst);
            dts.Columns.Add(dtcom1sst);



            DataColumn dtcomid = new DataColumn();
            DataColumn dtcomid1s = new DataColumn();
            dtcomid.DataType = System.Type.GetType("System.String");
            dtcomid.ColumnName = "InventoryWarehouseMasterId";
            dtcomid.ReadOnly = false;
            dtcomid.Unique = false;
            dtcomid.AllowDBNull = true;


            dtcomid1s.DataType = System.Type.GetType("System.String");
            dtcomid1s.ColumnName = "InventoryWarehouseMasterId";
            dtcomid1s.ReadOnly = false;
            dtcomid1s.Unique = false;
            dtcomid1s.AllowDBNull = true;

            dt.Columns.Add(dtcomid);
            dts.Columns.Add(dtcomid1s);

            DataColumn dtcom12 = new DataColumn();
            DataColumn dtcom1211 = new DataColumn();
            dtcom12.DataType = System.Type.GetType("System.String");
            dtcom12.ColumnName = "Name";
            dtcom12.ReadOnly = false;
            dtcom12.Unique = false;
            dtcom12.AllowDBNull = true;

            dtcom1211.DataType = System.Type.GetType("System.String");
            dtcom1211.ColumnName = "Name";
            dtcom1211.ReadOnly = false;
            dtcom1211.Unique = false;
            dtcom1211.AllowDBNull = true;
            dt.Columns.Add(dtcom12);
            dts.Columns.Add(dtcom1211);



            DataColumn dtcom121 = new DataColumn();
            DataColumn dtcom121e = new DataColumn();
            dtcom121.DataType = System.Type.GetType("System.String");
            dtcom121.ColumnName = "Unit";
            dtcom121.ReadOnly = false;
            dtcom121.Unique = false;
            dtcom121.AllowDBNull = true;

            dtcom121e.DataType = System.Type.GetType("System.String");
            dtcom121e.ColumnName = "Unit";
            dtcom121e.ReadOnly = false;
            dtcom121e.Unique = false;
            dtcom121e.AllowDBNull = true;
            dt.Columns.Add(dtcom121);
            dts.Columns.Add(dtcom121e);

            DataColumn dtcom1 = new DataColumn();
            DataColumn dtcom1s1 = new DataColumn();
            dtcom1.DataType = System.Type.GetType("System.String");
            dtcom1.ColumnName = "UnitType";
            dtcom1.ReadOnly = false;
            dtcom1.Unique = false;
            dtcom1.AllowDBNull = true;

            dtcom1s1.DataType = System.Type.GetType("System.String");
            dtcom1s1.ColumnName = "UnitType";
            dtcom1s1.ReadOnly = false;
            dtcom1s1.Unique = false;
            dtcom1s1.AllowDBNull = true;
            dt.Columns.Add(dtcom1);
            dts.Columns.Add(dtcom1s1);




            DataColumn dtcom2 = new DataColumn();
            DataColumn dtcom2s = new DataColumn();
            dtcom2.DataType = System.Type.GetType("System.String");
            dtcom2.ColumnName = "Qty";
            dtcom2.ReadOnly = false;
            dtcom2.Unique = false;
            dtcom2.AllowDBNull = true;

            dtcom2s.DataType = System.Type.GetType("System.String");
            dtcom2s.ColumnName = "Qty";
            dtcom2s.ReadOnly = false;
            dtcom2s.Unique = false;
            dtcom2s.AllowDBNull = true;
            dt.Columns.Add(dtcom2);
            dts.Columns.Add(dtcom2s);


            DataColumn dtcom3 = new DataColumn();
            DataColumn dtcom3s = new DataColumn();
            dtcom3.DataType = System.Type.GetType("System.String");
            dtcom3.ColumnName = "OderedQty";
            dtcom3.ReadOnly = false;
            dtcom3.Unique = false;
            dtcom3.AllowDBNull = true;

            dtcom3s.DataType = System.Type.GetType("System.String");
            dtcom3s.ColumnName = "OderedQty";
            dtcom3s.ReadOnly = false;
            dtcom3s.Unique = false;
            dtcom3s.AllowDBNull = true;
            dt.Columns.Add(dtcom3);
            dts.Columns.Add(dtcom3s);

            DataColumn dtcom4 = new DataColumn();
            DataColumn dtcom4s = new DataColumn();
            dtcom4.DataType = System.Type.GetType("System.String");
            dtcom4.ColumnName = "Note";
            dtcom4.ReadOnly = false;
            dtcom4.Unique = false;
            dtcom4.AllowDBNull = true;
            dt.Columns.Add(dtcom4);
         

            dtcom4s.DataType = System.Type.GetType("System.String");
            dtcom4s.ColumnName = "Note";
            dtcom4s.ReadOnly = false;
            dtcom4s.Unique = false;
            dtcom4s.AllowDBNull = true;
       
            dts.Columns.Add(dtcom4s);


            DataColumn dtcomUn = new DataColumn();
            DataColumn dtcomUn1 = new DataColumn();
            dtcomUn.DataType = System.Type.GetType("System.String");
            dtcomUn.ColumnName = "UnshipQty";
            dtcomUn.ReadOnly = false;
            dtcomUn.Unique = false;
            dtcomUn.AllowDBNull = true;

            dtcomUn1.DataType = System.Type.GetType("System.String");
            dtcomUn1.ColumnName = "UnshipQty";
            dtcomUn1.ReadOnly = false;
            dtcomUn1.Unique = false;
            dtcomUn1.AllowDBNull = true;


            dt.Columns.Add(dtcomUn);
            dts.Columns.Add(dtcomUn1);
            int salesChallanid = 0;
           
            SqlDataAdapter addp = new SqlDataAdapter("SELECT     RefSalesOrderId, SalesChallanMasterId, SalesChallanDatetime FROM SalesChallanMaster WHERE (RefSalesOrderId = '" + ddlRefSellno.SelectedValue + "') ", con);
            DataTable ddss = new DataTable();
            addp.Fill(ddss);
            if (ddss.Rows.Count > 0)
            {
                salesChallanid = Convert.ToInt32(ddss.Rows[0]["SalesChallanMasterId"]);


                string str1 = " SELECT     InventoryMaster.Name, InventoryMaster.ProductNo, InventoryMaster.InventoryMasterId, SalesOrderDetail.SalesOrderMasterId, cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+' '+UnitTypeMaster.Name AS Unit, " +
                       "    UnitTypeMaster.Name AS UnitType, SalesOrderDetail.Qty,  " +
                      "     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId " +
                    "  FROM         SalesOrderDetail INNER JOIN " +
                       "   InventoryWarehouseMasterTbl ON SalesOrderDetail.InventoryWHM_Id = InventoryWarehouseMasterTbl.InventoryWarehouseMasterId LEFT OUTER JOIN " +
                       "   UnitTypeMaster RIGHT OUTER JOIN " +
                       "   InventoryDetails ON UnitTypeMaster.UnitTypeId = InventoryDetails.UnitTypeId RIGHT OUTER JOIN " +
                       "   InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId ON  " +
                       "   InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId " +
                         " WHERE    InventoryMaster.CatType IS NULL and (SalesOrderDetail.SalesOrderMasterId = '" + ddlRefSellno.SelectedValue + "') ";


                SqlDataAdapter adp45 = new SqlDataAdapter(str1, con);
                DataTable ds45 = new DataTable();
                adp45.Fill(ds45);

                SqlDataAdapter adp46 = new SqlDataAdapter("SELECT     SalesChallanTransaction.inventoryWHM_Id,SalesChallanMaster.SalesChallanMasterId, SalesChallanTransaction.Quantity FROM   SalesChallanMaster INNER JOIN    SalesChallanTransaction ON SalesChallanMaster.SalesChallanMasterId = SalesChallanTransaction.SalesChallanMasterId " +
                                    " inner join InventoryWarehouseMasterTbl  on  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=SalesChallanTransaction.inventoryWHM_Id inner join InventoryMaster on InventoryMaster.InventoryMasterId=InventoryWarehouseMasterTbl.InventoryMasterId  WHERE InventoryMaster.CatType IS NULL and    (SalesChallanMaster.RefSalesOrderId = '" + ddlRefSellno.SelectedValue + "')", con);
                DataTable ds46 = new DataTable();
                adp46.Fill(ds46);

                foreach (DataRow dr in ds45.Rows)
                {

                    DataRow dtrow = dt.NewRow();

                    dtrow["ProductNo"] = dr["ProductNo"].ToString();
                    dtrow["InventoryWarehouseMasterId"] = dr["InventoryWarehouseMasterId"].ToString();
                    dtrow["Name"] = dr["Name"].ToString();
                    dtrow["Unit"] = dr["Unit"].ToString();
                    dtrow["UnitType"] = dr["UnitType"].ToString();
                    dtrow["Qty"] = dr["Qty"].ToString();
                    dtrow["OderedQty"] = dr["Qty"].ToString();
                    dtrow["UnshipQty"] = dr["Qty"].ToString();
                    decimal remQty = Convert.ToDecimal(dr["Qty"]);
                    foreach (DataRow dr1 in ds46.Rows)
                    {
                        if (Convert.ToInt32(dr["InventoryWarehouseMasterId"]) == Convert.ToInt32(dr1["inventoryWHM_Id"]))
                        {
                            if (Convert.ToString(dr1["SalesChallanMasterId"]) == Convert.ToString(ViewState["DcNo1"]))
                            {
                                dtrow["OderedQty"] = Convert.ToDecimal(dr1["Quantity"]);
                             
                                //remQty = remQty - Convert.ToDecimal(dr1["Quantity"]);
                                //dtrow["UnshipQty"] = remQty.ToString();
                                //dtrow["OderedQty"] = remQty;
                            }
                            else
                            {
                                remQty = remQty - Convert.ToDecimal(dr1["Quantity"]);
                                dtrow["UnshipQty"] = remQty.ToString();
                            }
                            
                        }
                       
                        

                        
                    }
                    decimal ab=0;
                     ab=Convert.ToDecimal(dtrow["UnshipQty"])-Convert.ToDecimal(dtrow["OderedQty"]);
                    dtrow["QtyShort"] = ab.ToString();
                    if (ab > 0)
                    {
                        fl = 1;
                    }
                    // dtrow["UnshipQty"] = dr["Qty"].ToString();
                    dtrow["Note"] = "";
                    dt.Rows.Add(dtrow);

                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
                ViewState["dt"] = dt;


                ////Servicess
              DataTable dserv=select( " SELECT     InventoryMaster.Name, InventoryMaster.ProductNo, InventoryMaster.InventoryMasterId, SalesOrderDetail.SalesOrderMasterId, cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+' '+UnitTypeMaster.Name AS Unit, " +
                       "    UnitTypeMaster.Name AS UnitType, SalesOrderDetail.Qty,  " +
                      "     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId " +
                    "  FROM         SalesOrderDetail INNER JOIN " +
                       "   InventoryWarehouseMasterTbl ON SalesOrderDetail.InventoryWHM_Id = InventoryWarehouseMasterTbl.InventoryWarehouseMasterId LEFT OUTER JOIN " +
                       "   UnitTypeMaster RIGHT OUTER JOIN " +
                       "   InventoryDetails ON UnitTypeMaster.UnitTypeId = InventoryDetails.UnitTypeId RIGHT OUTER JOIN " +
                       "   InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId ON  " +
                       "   InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId " +
                         " WHERE    InventoryMaster.CatType='0' and (SalesOrderDetail.SalesOrderMasterId = '" + ddlRefSellno.SelectedValue + "') ");



              SqlDataAdapter Asd = new SqlDataAdapter("SELECT     SalesChallanTransaction.inventoryWHM_Id,SalesChallanMaster.SalesChallanMasterId, SalesChallanTransaction.Quantity FROM   SalesChallanMaster INNER JOIN    SalesChallanTransaction ON SalesChallanMaster.SalesChallanMasterId = SalesChallanTransaction.SalesChallanMasterId " +
                                    " inner join InventoryWarehouseMasterTbl  on  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=SalesChallanTransaction.inventoryWHM_Id inner join InventoryMaster on InventoryMaster.InventoryMasterId=InventoryWarehouseMasterTbl.InventoryMasterId  WHERE InventoryMaster.CatType='0' and  (SalesChallanMaster.RefSalesOrderId = '" + ddlRefSellno.SelectedValue + "')", con);
                DataTable Dar = new DataTable();
                Asd.Fill(Dar);

                foreach (DataRow dr in dserv.Rows)
                {

                    DataRow dtrow = dts.NewRow();

                    dtrow["ProductNo"] = dr["ProductNo"].ToString();
                    dtrow["InventoryWarehouseMasterId"] = dr["InventoryWarehouseMasterId"].ToString();
                    dtrow["Name"] = dr["Name"].ToString();
                    dtrow["Unit"] = dr["Unit"].ToString();
                    dtrow["UnitType"] = dr["UnitType"].ToString();
                    dtrow["Qty"] = dr["Qty"].ToString();
                    dtrow["OderedQty"] = dr["Qty"].ToString();
                    dtrow["UnshipQty"] = dr["Qty"].ToString();
                    decimal remQty = Convert.ToDecimal(dr["Qty"]);
                    foreach (DataRow dr1 in Dar.Rows)
                    {
                        if (Convert.ToInt32(dr["InventoryWarehouseMasterId"]) == Convert.ToInt32(dr1["inventoryWHM_Id"]))
                        {
                            if (Convert.ToString(dr1["SalesChallanMasterId"]) == Convert.ToString(ViewState["DcNo1"]))
                            {

                                dtrow["OderedQty"] = Convert.ToDecimal(dr1["Quantity"]);

                                //remQty = remQty - Convert.ToDecimal(dr1["Quantity"]);
                                //dtrow["UnshipQty"] = remQty.ToString();
                                //dtrow["OderedQty"] = remQty;
                            }
                            else
                            {
                                remQty = remQty - Convert.ToDecimal(dr1["Quantity"]);
                                dtrow["UnshipQty"] = remQty.ToString();
                            }
                           
                        }
                        
                    }
                    decimal ab = 0;
                    ab = Convert.ToDecimal(dtrow["UnshipQty"]) - Convert.ToDecimal(dtrow["OderedQty"]);
                    dtrow["QtyShort"] = ab.ToString();
                    if (ab > 0)
                    {
                        fl = 1;
                    }
                    // dtrow["UnshipQty"] = dr["Qty"].ToString();
                    dtrow["Note"] = "";
                    dts.Rows.Add(dtrow);

                }

                GridView3.DataSource = dts;
                GridView3.DataBind();
                ViewState["dt1"] = dts;

            }
           

            lblshippingType.Visible = true;
            string str2hwex = " select SalesOrderMsterId ,ShippersShipOption from SalesOrderShippingOptionInfoTbl " +
                              "  where  SalesOrderMsterId='" + ddlRefSellno.SelectedValue + "' ";

            SqlCommand cmd2hwex = new SqlCommand(str2hwex, con);
            SqlDataAdapter adp2hmn = new SqlDataAdapter(cmd2hwex);
            DataTable dt2mh = new DataTable();
            adp2hmn.Fill(dt2mh);
            if (dt2mh.Rows.Count > 0)
            {
                Label3.Visible = true;
                lblshippingType.Text = dt2mh.Rows[0]["ShippersShipOption"].ToString();
            }
            else
            {
                Label3.Visible = true;
                lblshippingType.Text = "";
            }
           
            qtyonhand();
            shippingCharges();
           // Panel2.Visible = true;
            fillSalesOrderAmts();
            //FillSaleInvoiceAmts();
            pnlsh.Visible = true;
        }
        else
        {
            pnlsh.Visible = false;
        }
        if (GridView1.Rows.Count > 0)
        {
            pnlSearch.Visible = true;
           
        }
        else
        {
            pnlSearch.Visible = false;
        }
        if (GridView3.Rows.Count > 0)
        {
            pnlservices.Visible = true;
          
        }
        else
        {
            pnlservices.Visible = false;
        }

        RadioButtonList1.Items.Clear();
        if (fl == 1)
        {
            RadioButtonList1.Items.Insert(0, "Order Partially Shipped but considered Complete");
            RadioButtonList1.Items[0].Value = "1";
            RadioButtonList1.Items.Insert(1, "Partially Shipped and on Backorder");
            RadioButtonList1.Items[1].Value = "2";
            RadioButtonList1.Items.Insert(2, "Partially Shipped");
            RadioButtonList1.Items[2].Value = "3";
            RadioButtonList1.SelectedValue = "3";
        }
        else
        {
            RadioButtonList1.Items.Insert(0, "Fully Shipped");
            RadioButtonList1.Items[0].Value = "4";

        }
    }
    protected void qtyonhand()
    {

        foreach (GridViewRow it in GridView1.Rows)
        {
            Label lblqtyonhand = (Label)it.FindControl("lblqtyonhand");
            Label lblavgrate = (Label)it.FindControl("lblavgrate");
            Label lblinvW = (Label)it.FindControl("lblinvW");


            double finaltotalqty = 0;

            DataTable drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + lblinvW.Text + "' and DateUpdated<='" + lblDate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

            if (drtinvdata.Rows.Count > 0)
            {
                lblavgrate.Text = Convert.ToString(drtinvdata.Rows[0]["AvgCost"]);

                DataTable dton = select("SELECT Qty FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + lblinvW.Text + "' and Tranction_Master_Id='" + ViewState["tid"] + "'");

                if (dton.Rows.Count > 0)
                {
                    lblqtyonhand.Text = Convert.ToString(Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]) - Convert.ToDecimal(drtinvdata.Rows[0]["Qty"]));
                }
              
                if (lblqtyonhand.Text != "")
                {
                    finaltotalqty = Convert.ToDouble(drtinvdata.Rows[0]["QtyonHand"]);
                }
                else
                {
                    lblqtyonhand.Text = "0";
                }

            }


            if (finaltotalqty > 0)
            {
                lblqtyonhand.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                lblqtyonhand.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
    protected void fillSalesOrderAmts()
    {


        ViewState["amttotpay"] = "0";
        string findtotal = "select Qty,Rate,Amount from  SalesOrderDetail where SalesOrderMasterId='" + ddlRefSellno.SelectedValue + "'";
        SqlDataAdapter f1 = new SqlDataAdapter(findtotal,con);
        DataSet d12 = new DataSet();
        f1.Fill(d12);
        for (int i = 0; i < d12.Tables[0].Rows.Count; i++)
        {
            double qty12=Convert.ToDouble(d12.Tables[0].Rows[0]["Qty"].ToString());
            double rate12 = Convert.ToDouble(d12.Tables[0].Rows[0]["Rate"].ToString());
            FINALANSWER = FINALANSWER + (qty12 * rate12);
        }

        lblSubTotalSO.Text = Convert.ToString(FINALANSWER);
        FINALANSWER = 0.00;

        string findvolumedis = "select VolDisc,PromDisc,HandlingCharg from  SalesOrderDetailTemp where SalesOrderTempId='" + ddlRefSellno.SelectedValue + "'";
        SqlDataAdapter v1 = new SqlDataAdapter(findvolumedis,con);
        DataSet dv1 = new DataSet();
        v1.Fill(dv1);
        for (int i = 0; i < dv1.Tables[0].Rows.Count;i++ )
        {
            volumediscount12 = volumediscount12 + Convert.ToDouble(dv1.Tables[0].Rows[i]["VolDisc"].ToString());
            prodis = prodis + Convert.ToDouble(dv1.Tables[0].Rows[i]["PromDisc"].ToString());
            hdcharges = hdcharges = Convert.ToDouble(dv1.Tables[0].Rows[i]["HandlingCharg"].ToString());
        }



        Session["dis"] = volumediscount12;
        Session["pro"] = prodis;
        Session["hd"] = hdcharges;

        //************************
        string strAmts ="SELECT     SalesOrderMasterTamp.SalesOrderTempId, SalesOrderMasterTamp.subTotal, SalesOrderMasterTamp.CustDisc, SalesOrderMasterTamp.OrderDisc,  "+
                   "   SalesOrderMasterTamp.Tax, SalesOrderMasterTamp.HandlingCharg, SalesOrderMasterTamp.ShippingCharge, SalesOrderMasterTamp.Total, "+
                   "   SalesOrderMasterTamp.userid, TranctionMaster.EntryTypeId, SalesOrderDetail.Notes, TranctionMaster.EntryNumber "+
                     " FROM         TransactionMasterMoreInfo LEFT OUTER JOIN "+
                    "  SalesOrderDetail ON TransactionMasterMoreInfo.SalesOrderId = SalesOrderDetail.SalesOrderId LEFT OUTER JOIN "+
                    "  TranctionMaster ON TransactionMasterMoreInfo.Tranction_Master_Id = TranctionMaster.Tranction_Master_Id RIGHT OUTER JOIN "+
                    "  SalesOrderMasterTamp ON TransactionMasterMoreInfo.SalesOrderId = SalesOrderMasterTamp.SalesOrderTempId "+
                    "  WHERE     (SalesOrderMasterTamp.SalesOrderTempId = '"+ddlRefSellno.SelectedValue+"')  "+
                    "  order by SalesOrderMasterTamp.SalesOrderTempId desc ";
       
        
        SqlCommand cmd11111 = new SqlCommand(strAmts, con);
        SqlDataAdapter dbss12 = new SqlDataAdapter(cmd11111);
        //DataTable dt22Amts = dbss1.cmdSelect(strAmts);
        DataTable dt22Amts = new DataTable();
        dbss12.Fill(dt22Amts);
        if (dt22Amts.Rows.Count > 0)
        {
            //***********chnages codes
          //lblSubTotalSO.Text = dt22Amts.Rows[0]["subTotal"].ToString();
            //***********************
            
            lblCustDisSO.Text = dt22Amts.Rows[0]["CustDisc"].ToString();
            lblHandChrgSO.Text = dt22Amts.Rows[0]["HandlingCharg"].ToString();
            lblShipChrgSO.Text = dt22Amts.Rows[0]["ShippingCharge"].ToString();
            lblTaxSO.Text = dt22Amts.Rows[0]["Tax"].ToString();
            lblTotalSO.Text = dt22Amts.Rows[0]["Total"].ToString();
            lblValueDisSO.Text = dt22Amts.Rows[0]["OrderDisc"].ToString();
            if (dt22Amts.Rows[0]["EntryTypeId"].ToString() == "26")
            {
                lblSalesOrderType.Text = "Online SaleOrder";
            }
            else if (dt22Amts.Rows[0]["EntryTypeId"].ToString() == "30")
            {
                lblSalesOrderType.Text = "Reatil SaleOrder";
            }
            else
            {
                lblSalesOrderType.Text = "Online SaleOrder";

            }
            lblSubTotalSI.Text = lblSubTotalSO.Text;
            lblCustDisSI.Text = lblCustDisSO.Text;
            lblHandChrgSI.Text = lblHandChrgSO.Text;
            lblShipChrgSI.Text = lblShipChrgSO.Text;
            lblTaxSI.Text = lblTaxSO.Text;

            lblValueDisSI.Text = lblValueDisSO.Text;
            lblTotalSI.Text = lblTotalSO.Text;

        }
    }
    protected void FillSaleInvoiceAmts()
    {
       

        double unshipqty = 0;
        double invoqty = 0;
        foreach (GridViewRow gg8 in GridView1.Rows)
        {
            TextBox tth = (TextBox)gg8.FindControl("TextBox4");
            //double uq =Convert.ToDouble(gg8.Cells[6].Text);
            //double shipinvq = Convert.ToDouble(tth.Text);
            //unshipqty = unshipqty + uq;
            //invoqty = invoqty + shipinvq;
            Label lblqty = (Label)gg8.FindControl("lblqty");
            unshipqty = unshipqty + Convert.ToDouble(tth.Text);
            invoqty = invoqty + Convert.ToDouble(lblqty.Text);
           
        }
        foreach (GridViewRow gg8 in GridView3.Rows)
        {
            TextBox tth = (TextBox)gg8.FindControl("TextBox4");
            //double uq =Convert.ToDouble(gg8.Cells[6].Text);
            //double shipinvq = Convert.ToDouble(tth.Text);
            //unshipqty = unshipqty + uq;
            //invoqty = invoqty + shipinvq;
            Label lblqty = (Label)gg8.FindControl("lblqty");
            unshipqty = unshipqty + Convert.ToDouble(tth.Text);
            invoqty = invoqty + Convert.ToDouble(lblqty.Text);

        }
        if (invoqty == unshipqty)
        {
            lblSubTotalSI.Text = lblSubTotalSO.Text;
            lblCustDisSI.Text = lblCustDisSO.Text;
            lblHandChrgSI.Text = lblHandChrgSO.Text;
            lblShipChrgSI.Text = lblShipChrgSO.Text;
            lblTaxSI.Text = lblTaxSO.Text;
            lblValueDisSI.Text = lblValueDisSO.Text;
            lblTotalSI.Text = lblTotalSO.Text;
        }
        else  
        {
            double chandgSubttl = Convert.ToDouble((isdoubleornot(lblSubTotalSO.Text) * isdoubleornot(unshipqty.ToString())) / isdoubleornot(invoqty.ToString()));
            //double chandgSubttl = Convert.ToDouble((isdoubleornot(lblSubTotalSO.Text) * isdoubleornot(unshipqty.ToString())) / isdoubleornot(invoqty.ToString()));
          
            lblSubTotalSI.Text = String.Format("{0:n}", chandgSubttl);

            double changecustD = Convert.ToDouble((isdoubleornot(lblCustDisSO.Text) * isdoubleornot(unshipqty.ToString())) / isdoubleornot(invoqty.ToString()));
            lblCustDisSI.Text = String.Format("{0:n}", changecustD);

            double changeHndCharg = Convert.ToDouble((isdoubleornot(lblHandChrgSO.Text) * isdoubleornot(unshipqty.ToString())) / isdoubleornot(invoqty.ToString()));
            lblHandChrgSI.Text = String.Format("{0:n}", changeHndCharg);

            double changeShipcrg = Convert.ToDouble((isdoubleornot(lblShipChrgSO.Text) * isdoubleornot(unshipqty.ToString())) / isdoubleornot(invoqty.ToString()));
            lblShipChrgSI.Text = String.Format("{0:n}", changeShipcrg);

            double changeVD = Convert.ToDouble((isdoubleornot(lblValueDisSO.Text) * isdoubleornot(unshipqty.ToString())) / isdoubleornot(invoqty.ToString()));
            lblValueDisSI.Text = String.Format("{0:n}", changeVD);


            double changeTx = Convert.ToDouble((isdoubleornot(lblTaxSO.Text) * isdoubleornot(unshipqty.ToString())) / isdoubleornot(invoqty.ToString()));
            lblTaxSI.Text = String.Format("{0:n}", changeTx);



            double vd = Convert.ToDouble((isdoubleornot(Session["dis"].ToString()) * isdoubleornot(unshipqty.ToString())) / isdoubleornot(invoqty.ToString()));
        Session["dis"] = String.Format("{0:n}", vd);
       double payamt = Convert.ToDouble((isdoubleornot(ViewState["amttotpay"].ToString()) * isdoubleornot(invoqty.ToString())) / isdoubleornot(invoqty.ToString()));

      ViewState["amttotpay"] = String.Format("{0:n}", payamt);
       double pd = Convert.ToDouble((isdoubleornot(Session["pro"].ToString()) * isdoubleornot(unshipqty.ToString())) / isdoubleornot(invoqty.ToString()));
        Session["pro"] = String.Format("{0:n}", pd);
        double chandss = Convert.ToDouble((isdoubleornot(lblTotalSO.Text) * isdoubleornot(unshipqty.ToString())) / isdoubleornot(invoqty.ToString()));
       // double ttlchage = ((chandgSubttl - (changecustD + changeHndCharg)) + changeShipcrg + changeTx + changeHndCharg);

         // double ttlchage = ((chandss - (changecustD + changeHndCharg)) + changeShipcrg + changeTx + changeHndCharg);
                 //double ttlchage = ((chandss - (changecustD + changeHndCharg)) + changeShipcrg + changeTx + changeHndCharg);
     
           // lblTotalSI.Text = String.Format("{0:n}", ttlchage);
        lblTotalSI.Text = String.Format("{0:n}", chandss);

        }
        
    }
    protected void ddlParty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlParty.SelectedIndex != 0)
        {
          
            DataTable ds1 = new DataTable();
            DataTable ds2 = new DataTable();
            //foreach (DataRow dr in ds.Rows)
            //{
            //string str1 = " SELECT   +'Zip-'+Zipcode as Zipcode, BillingAddressId  FROM BillingAddress " +
            //                " WHERE  (SalesOrderId = '" + Convert.ToInt32(dr["SalesOrderId"]) + "') ";
            string str1 = "SELECT    +'Zip-'+ ShippingAddress.Zipcode as Zipcode, MAX(ShippingAddress.ShippingAddressID) AS ShippingAddressID " +
                        " FROM         SalesOrderMaster INNER JOIN " +
                      " ShippingAddress ON SalesOrderMaster.SalesOrderId = ShippingAddress.SalesOrderId " +
                    " WHERE     (SalesOrderMaster.PartyId = '" + ddlParty.SelectedValue + "') " +
                    " GROUP BY ShippingAddress.Zipcode";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            adp1.Fill(ds1);

            string str2 = " SELECT     + 'Zip-' + BillingAddress.Zipcode AS Zipcode, MAX(BillingAddress.BillingAddressId) AS BillingAddressId " +
                            " FROM         SalesOrderMaster INNER JOIN " +
                  " BillingAddress ON SalesOrderMaster.SalesOrderId = BillingAddress.SalesOrderId " +
                " WHERE     (SalesOrderMaster.PartyId = 144) " +
                " GROUP BY BillingAddress.Zipcode ";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
            adp2.Fill(ds2);
            //}
            if (ds2.Rows.Count > 0)
            {
                ddlBillTo.DataSource = ds2;
                ddlBillTo.DataTextField = "Zipcode";
                ddlBillTo.DataValueField = "BillingAddressId";
                ddlBillTo.DataBind();
                ddlBillTo.Items.Insert(0, "-select-");
            }
            if (ds1.Rows.Count > 0)
            {
                ddlShipTo.DataSource = ds1;
                ddlShipTo.DataTextField = "Zipcode";
                ddlShipTo.DataValueField = "ShippingAddressID";
                ddlShipTo.DataBind();
                ddlShipTo.Items.Insert(0, "-select-");
            }



        }
    }
    protected void ddlBillTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBillTo.SelectedIndex != 0)
        {
            string str = " SELECT     Name, Address, Email, City, State, Country, Phone, Zipcode, BillingAddressId " +
                           " FROM         BillingAddress " +
                           "  WHERE     (BillingAddressId = '" + ddlBillTo.SelectedValue + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            string billaddress = "Name:" + ds.Rows[0]["Name"].ToString() + "<br>Address:" + ds.Rows[0]["Address"].ToString() + "<br>Mail:" + ds.Rows[0]["Email"].ToString() + "<br>City:" + ds.Rows[0]["City"].ToString() + "<br>State:" + ds.Rows[0]["State"].ToString() + "<br>Country:" + ds.Rows[0]["Country"].ToString() + "<br>Phone:" + ds.Rows[0]["Phone"].ToString() + "<br>Zip:" + ds.Rows[0]["Zipcode"].ToString() + "<br>";
            lblBillAddress.Text = billaddress.ToString();
        }
    }
    protected void ddlShipTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlShipTo.SelectedIndex != 0)
        {
            string str = " SELECT     Name, Address, Email, City, State, Country, Phone, Zipcode, ShippingAddressID " +
                     " FROM         ShippingAddress " +
                     "  WHERE     (ShippingAddressID = '" + ddlShipTo.SelectedValue + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            string Shipaddress = "Name:" + ds.Rows[0]["Name"].ToString() + "<br>Address:" + ds.Rows[0]["Address"].ToString() + "<br>Mail:" + ds.Rows[0]["Email"].ToString() + "<br>City:" + ds.Rows[0]["City"].ToString() + "<br>State:" + ds.Rows[0]["State"].ToString() + "<br>Country:" + ds.Rows[0]["Country"].ToString() + "<br>Phone:" + ds.Rows[0]["Phone"].ToString() + "<br>Zip:" + ds.Rows[0]["Zipcode"].ToString() + "<br>";

            lblShipAddress.Text = Shipaddress.ToString();
        }
    }
  
   
    protected void imgCal_Click(object sender, EventArgs e)
    {
        //Calendar1.Visible = true;
    }
  
   
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    if(ddlWarehouse.SelectedIndex>0)
    //    {
    //    ddlParty.DataSource = (DataTable)fillParty();
    //    ddlParty.DataTextField = "Compname";
    //    ddlParty.DataValueField = "PartyID";
    //    ddlParty.DataBind();
    //    ddlParty.Items.Insert(0, "-select-");
    //        }
    //}
    protected void ddlShippingPerson_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  
    
    public DataTable fillCategory()
    {
        string str = "SELECT     InventoryCategoryMaster.InventoryCatName + '-' + InventorySubCategoryMaster.InventorySubCatName + '-' + InventoruSubSubCategory.InventorySubSubName " +
                       " AS category, InventoruSubSubCategory.InventorySubSubName, InventoruSubSubCategory.InventorySubSubId " +
                        " FROM         InventorySubCategoryMaster INNER JOIN " +
                      " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId INNER JOIN " +
                      " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryCategoryMaster.compid='" + com + "' " +
                      " and InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "' order by category";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;

    }
  
  
    public DataTable fillUnitType()
    {
        string str = "SELECT  UnitTypeId, Name FROM  UnitTypeMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;

    }
  
 
  
    public void GenerateSalesInvoice(int salesOrderid)
    {
      
        double invoiceAmt = 0;
       
                    invoiceAmt = Convert.ToDouble(isdecimalornot(lblTotalSI.Text));

                    string str123 = "Update  TranctionMaster Set Date='" + Convert.ToDateTime(lblDate.Text).ToShortDateString() + "',UserId='" + Session["Userid"] + "',Tranction_Amount='" + invoiceAmt + "' where  Tranction_Master_Id='" + ViewState["tid"] + "'";

                    SqlCommand cmd123 = new SqlCommand(str123, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd123.ExecuteNonQuery();
                    con.Close();



                    ViewState["trmid1"] = ViewState["tid"];
          
            //insert transaction master more info table
                    string deltec = "Delete from TransactionMasterSalesChallanTbl where TransactionMasterId='" + ViewState["trmid1"] + "'";

                    SqlCommand deletrv = new SqlCommand(deltec, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    deletrv.ExecuteNonQuery();
                    int masterid =Convert.ToInt32( ViewState["tid"]);

            string strNewDCndInvRelation = "INSERT INTO TransactionMasterSalesChallanTbl  (TransactionMasterId, SalesChallanMasterId, " +
                " SalesOrderMasterId) VALUES     " +
                " ('" + ViewState["trmid1"] + "', '" + Convert.ToInt32(ViewState["DcNo1"]) + "', '" + Convert.ToInt32(salesOrderid) + "')";
            SqlCommand cmMore1 = new SqlCommand(strNewDCndInvRelation, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmMore1.ExecuteNonQuery();
            con.Close();


            SqlCommand cmdamtdues = new SqlCommand("select AmountDue from SalesOrderSuppliment where SalesOrderMasterId='" + salesOrderid + "' and (AmountDue > 0 )", con);
            SqlDataAdapter dtpamtdues = new SqlDataAdapter(cmdamtdues);
            DataTable dtamtdues = new DataTable();
            dtpamtdues.Fill(dtamtdues);

            if (dtamtdues.Rows.Count > 0)
            {
                if (Convert.ToDouble(dtamtdues.Rows[0]["AmountDue"].ToString()) > 0)
                {
                    string invtdetts = "Delete from TranctionMasterSuppliment where Tranction_Master_Id='" + masterid + "'";

                    SqlCommand cminbdetsp = new SqlCommand(invtdetts, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    string statusid = "";
                    cminbdetsp.ExecuteNonQuery();
                    con.Close();
                    DataTable dsr = select("Select DueDay from SalesOrderMaster where SalesOrderId='" + salesOrderid + "'");
                    if (dsr.Rows.Count > 0)
                    {
                        if (Convert.ToString(dsr.Rows[0]["DueDay"]) != "")
                        {
                            if (Convert.ToInt32(dsr.Rows[0]["DueDay"]) <= 30)
                            {
                                statusid = "53";
                            }
                            else if (Convert.ToInt32(dsr.Rows[0]["DueDay"]) >= 31 && Convert.ToInt32(dsr.Rows[0]["DueDay"]) <= 60)
                            {
                                statusid = "55";
                            }
                            else if (Convert.ToInt32(dsr.Rows[0]["DueDay"]) >= 61 && Convert.ToInt32(dsr.Rows[0]["DueDay"]) <= 90)
                            {
                                statusid = "56";
                            }
                            else if (Convert.ToInt32(dsr.Rows[0]["DueDay"]) >= 91 && Convert.ToInt32(dsr.Rows[0]["DueDay"]) <= 120)
                            {
                                statusid = "57";
                            }
                            else if (Convert.ToInt32(dsr.Rows[0]["DueDay"]) >= 121)
                            {
                                statusid = "58";
                            }

                            SqlCommand cmm09 = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                                                   " VALUES('" + salesOrderid + "','" + System.DateTime.Now + "','" + statusid + "','Retail invoice Amount due status',  '" + masterid + "') ", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmm09.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                  
                    string str345 = "INSERT INTO TranctionMasterSuppliment " +
                    " (Tranction_Master_Id           ,AmountDue           ,Memo           ,Party_MasterId)" +
                     "VALUES           ('" + masterid + "','" + invoiceAmt + "' ,'" + txtNote.Text + "','" + ViewState["pp"].ToString() + "' ) ";
                    SqlCommand cmMore34 = new SqlCommand(str345, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmMore34.ExecuteNonQuery();
                    con.Close();
                    //}
                }
            }

            //*****************discount

            string invtdet = "Delete from Tranction_Details where Tranction_Master_Id='" + ViewState["tid"] + "'";

            SqlCommand cminbdet = new SqlCommand(invtdet, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cminbdet.ExecuteNonQuery();
            con.Close();

            string dis12 = Session["dis"].ToString();
            string voo12 = Session["pro"].ToString();
            InsertTransactionDetail(6003, 0, isdecimalornot(dis12.ToString()), 0, masterid, "", System.DateTime.Now, "0", " 0");
            InsertTransactionDetail(6004, 0, isdecimalornot(voo12.ToString()), 0, masterid, "", System.DateTime.Now, "0", " 0");

            //******************
            InsertTransactionDetail(0, 5000, 0, isdecimalornot(lblSubTotalSI.Text), masterid, "", System.DateTime.Now, "0", " 0");

            //   InsertTransactionDetail(0, 5000, 0,isdecimalornot( lblSubTotalSI.Text), masterid, "", System.DateTime.Now, "0", " 0");
            InsertTransactionDetail(6000, 0, isdecimalornot(lblCustDisSI.Text), 0, masterid, "", System.DateTime.Now, "0", " 0");
            InsertTransactionDetail(6001, 0, isdecimalornot(lblValueDisSI.Text), 0, masterid, " ", System.DateTime.Now, "0", " 0");
            InsertTransactionDetail(0, 5001, 0, isdecimalornot(lblHandChrgSI.Text), masterid, " ", System.DateTime.Now, "0", " 0");
            InsertTransactionDetail(0, 5002, 0, isdecimalornot(lblShipChrgSI.Text), masterid, " ", System.DateTime.Now, "0", " 0");
            InsertTransactionDetail(0, 3000, 0, isdecimalornot(lblTaxSI.Text), masterid, "", System.DateTime.Now, "0", " 0");


            string sertaccidfrompartyid = "SELECT     PartyID, Account FROM  Party_master WHERE     (PartyID = '" + Convert.ToInt32(ViewState["pp"]) + "')";
            SqlCommand cmd11111 = new SqlCommand(sertaccidfrompartyid, con);
            SqlDataAdapter dbss13 = new SqlDataAdapter(cmd11111);
            //DataTable dbaccidfrompartyid = dbss1.cmdSelect(sertaccidfrompartyid);
            DataTable dbaccidfrompartyid = new DataTable();
            dbss13.Fill(dbaccidfrompartyid);
            int accidpart = Convert.ToInt32(dbaccidfrompartyid.Rows[0]["Account"].ToString());
            // InsertTransactionDetail(accidpart, 0, isdecimalornot(ViewState["amttotpay"].ToString()), 0, masterid, "", System.DateTime.Now, "0", " 0");

            InsertTransactionDetail(accidpart, 0, isdecimalornot(lblTotalSI.Text), 0, masterid, "", System.DateTime.Now, "0", " 0");



            ViewState["invx"] = invoiceAmt.ToString();


            string str9990 = "  SELECT     PaymentAppTblId, TranMIdPayReceived, TranMIdAmtApplied, AmtApplied, Date, " +
             " SalesOrderId, UserId FROM         PaymentApplicationTbl " +
                 " where SalesOrderId='" + Convert.ToInt32(salesOrderid) + "' and TranMIdPayReceived is not null and TranMIdPayReceived <>0 order by PaymentAppTblId desc ";
            SqlCommand cmd9990 = new SqlCommand(str9990, con);
            SqlDataAdapter adp9990 = new SqlDataAdapter(cmd9990);
            DataTable dt9990 = new DataTable();
            adp9990.Fill(dt9990);

            double A = 0;
            if (dt9990.Rows.Count > 0)
            {
                A = Convert.ToDouble(dt9990.Rows[0]["AmtApplied"]);
            }

            string str9991 = "  SELECT     PaymentAppTblId, TranMIdPayReceived, TranMIdAmtApplied, AmtApplied, Date, " +
            " SalesOrderId, UserId FROM         PaymentApplicationTbl " +
                " where SalesOrderId='" + Convert.ToInt32(salesOrderid) + "' and TranMIdAmtApplied is not null and TranMIdAmtApplied <>0 order by PaymentAppTblId desc";
            SqlCommand cmd9991 = new SqlCommand(str9991, con);
            SqlDataAdapter adp9991 = new SqlDataAdapter(cmd9991);
            DataTable dt9991 = new DataTable();
            adp9991.Fill(dt9991);

            double B = 0;
            if (dt9991.Rows.Count > 0)
            {
                B = Convert.ToDouble(dt9991.Rows[0]["AmtApplied"]);
            }

            double C = A - B;
            double D = Convert.ToDouble(invoiceAmt);
            if (C >= D)
            {
                string str998 = " INSERT INTO PaymentApplicationTbl           " +
                                "(TranMIdAmtApplied,AmtApplied ,Date,SalesOrderId) " +
                                " VALUES ('" + ViewState["trmid1"].ToString() + "','" + invoiceAmt.ToString() + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "', '" + Convert.ToInt32(salesOrderid) + "' )";
                SqlCommand cmd998 = new SqlCommand(str998, con);
                con.Open();
                cmd998.ExecuteNonQuery();
                con.Close();

            }
            else if (C < D)
            {
                string str998 = " INSERT INTO PaymentApplicationTbl           " +
                                "(TranMIdAmtApplied,AmtApplied ,Date,SalesOrderId) " +
                                " VALUES ('" + ViewState["trmid1"].ToString() + "','" + C + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "', '" + Convert.ToInt32(salesOrderid) + "' )";
                SqlCommand cmd998 = new SqlCommand(str998, con);
                con.Open();
                cmd998.ExecuteNonQuery();
                con.Close();
            }
            else
            {

            }
            DataTable dtappg = select("Select * from StatusControl where  TranctionMasterId='" + ViewState["trmid1"].ToString() + "' and SalesOrderId='" + ddlRefSellno.SelectedValue + "' and StatusMasterId='30'");



            if (dtappg.Rows.Count == 0)
            {
                string mxid99834 = "Select Max(AmtApplied) as AmtApplied from PaymentApplicationTbl where  SalesOrderId='" + ddlRefSellno.SelectedValue + "'";
                SqlCommand cmd9982 = new SqlCommand(mxid99834, con);
                SqlDataAdapter adp9982 = new SqlDataAdapter(cmd9982);
                DataTable dt9982 = new DataTable();
                adp9982.Fill(dt9982);

                if (dt9982.Rows.Count > 0)
                {
                    double P = Convert.ToDouble(dt9982.Rows[0]["AmtApplied"]);
                    double Q = Convert.ToDouble(invoiceAmt);

                      if (P== 0)
                    {
                        //Unpaid

                        SqlCommand cmm09 = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                                                " VALUES('" + ddlRefSellno.SelectedValue + "','" + System.DateTime.Now.ToShortDateString() + "','28','" + txtNote.Text + "','" + ViewState["trmid1"].ToString() + "') ", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmm09.ExecuteNonQuery();
                        con.Close();


                    }
                    else if (P < Q)
                    {
                        //Partly Paid
                        SqlCommand cmm09 = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                                                " VALUES('" + ddlRefSellno.SelectedValue + "','" + System.DateTime.Now.ToShortDateString() + "','29','" + txtNote.Text + "','" + ViewState["trmid1"].ToString() + "') ", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmm09.ExecuteNonQuery();
                        con.Close();



                    }
                    else if (P != Q)
                    {
                        //Unpaid

                        SqlCommand cmm09 = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                                                " VALUES('" + ddlRefSellno.SelectedValue + "','" + System.DateTime.Now.ToShortDateString() + "','28','" + txtNote.Text + "','" + ViewState["trmid1"].ToString() + "') ", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmm09.ExecuteNonQuery();
                        con.Close();


                    }
                    else
                    {
                        //P>Q and P=Q  // Fully Paid

                        SqlCommand cmm09 = new SqlCommand("INSERT INTO StatusControl(SalesOrderId,  Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                                                " VALUES('" + ddlRefSellno.SelectedValue + "','" + System.DateTime.Now.ToShortDateString() + "','30','" + txtNote.Text + "','" + ViewState["trmid1"].ToString() + "') ", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmm09.ExecuteNonQuery();
                        con.Close();
                    }


                }
            }

            string maininv = "";
            int ly = 0;
            double amtavgcost = 0;
        foreach (GridViewRow gdr11 in GridView1.Rows)
        {
            double FinalQtySub = 0;
            double FinalQty = 0;
            Label lblinvwm = (Label)gdr11.FindControl("iwhmid");
            TextBox txtShipQty1 = (TextBox)gdr11.FindControl("TextBox4");
            FinalQtySub = Convert.ToDouble(txtShipQty1.Text);
            FinalQty = -(FinalQtySub);

            if (Convert.ToDecimal(txtShipQty1.Text) > 0)
            {
                string invtype = "0";
                if (Convert.ToDateTime(ViewState["sdo"]) == Convert.ToDateTime(lblDate.Text))
                {
                    invtype = "1";
                  //  ViewState["sdo"] = txtGoodsDate.Text;
                }
                else if (Convert.ToDateTime(ViewState["sdo"]) <= Convert.ToDateTime(lblDate.Text))
                {
                    invtype = "2";
                   // ViewState["sdo"] = txtGoodsDate.Text;
                }
                else
                {
                    invtype = "3";



                }
                if (invtype == "3" || invtype == "2")
                {
                    string updateavgcos = "Delete from  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + lblinvwm.Text + "' and Tranction_Master_Id='" + ViewState["trmid1"] + "'";
                    SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmavgcost.ExecuteNonQuery();
                    con.Close();
                    if (invtype == "3")
                    {
                        string upproc = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,DateUpdated,AvgCost,QtyonHand)values('" + lblinvwm.Text + "','" + ViewState["trmid1"] + "','" + FinalQty + "','" + lblDate.Text + "','0','0')";
                        SqlCommand cmdpro = new SqlCommand(upproc, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdpro.ExecuteNonQuery();
                        con.Close();
                    }
                }
                fillAVGCOST(ViewState["trmid1"].ToString(), lblinvwm.Text.ToString(), FinalQtySub.ToString(), invtype);

                if (ly > 0)
                {
                    maininv = maininv + ",";
                }
                else
                {
                    ly += 1;
                }

                maininv = maininv + lblinvwm.Text.ToString();
                DataTable datacugs = select("SELECT  QtyonHand,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + lblinvwm.Text + "' and Tranction_Master_Id='" + ViewState["trmid1"] + "' and DateUpdated='" + lblDate.Text + "'");
                if (datacugs.Rows.Count > 0)
                {
                    amtavgcost += Convert.ToDouble(datacugs.Rows[0]["AvgCost"]) * FinalQtySub;
                }
                amtavgcost = Math.Round(amtavgcost, 2);
            }

        }
        ///// Add SUGS
        
        string accdebiinv = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                      " ,DateTimeOfTransaction,compid,whid )" +
                      " VALUES('8003','" + amtavgcost + "'" +
                      "  , '" + ViewState["trmid1"] + "','" + Convert.ToDateTime(lblDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
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
                  " ,'" + ViewState["trmid1"] + "','" + Convert.ToDateTime(lblDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";

        SqlCommand cdcostgood = new SqlCommand(costgood, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cdcostgood.ExecuteNonQuery();
        con.Close();
        DELAVGCOST(maininv, ViewState["trmid1"].ToString());
        
      
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
                string ABCD = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,DateUpdated,AvgCost,QtyonHand)values('" + id12 + "','" + traid + "','" + (Convert.ToDouble(recqty) * (-1)) + "','" + ViewState["sdo"] + "','0','0')";
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
                updateavgcos = "Update InventoryWarehouseMasterAvgCostTbl Set Qty='" + (Convert.ToDouble(recqty) * (-1)) + "', QtyonHand='" + Newqtyonhand + "',AvgCost='" + Totalavgcost + "' where InvWMasterId='" + id12 + "' and Tranction_Master_Id='" + traid + "'";

            }
            else
            {
                updateavgcos = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,DateUpdated,AvgCost,QtyonHand)values('" + id12 + "','" + traid + "','" + (Convert.ToDouble(recqty) * (-1)) + "','" + ViewState["sdo"] + "','" + Totalavgcost + "','" + Newqtyonhand + "')";
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

    protected decimal isdecimalornot(string ck)
    {
        decimal i = 0;
        try
        {
            i = Convert.ToDecimal(ck);
            return i;
        }
        catch
        {
            return i;
        }
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        ddlSalesOrdStatus.SelectedIndex = 0;
        ddlSalesOrdStatus_SelectedIndexChanged(sender, e);
      
           ddlParty.DataSource = (DataTable)fillParty();
            ddlParty.DataTextField = "Compname";
            ddlParty.DataValueField = "PartyID";
            ddlParty.DataBind();
            ddlParty.Items.Insert(0, "-select-");
        ddlRefSellno_SelectedIndexChanged(sender,e);
        ddlShippingPerson.DataSource = (DataTable)fillShipperPerson();
        ddlShippingPerson.DataTextField = "Compname";
        ddlShippingPerson.DataValueField = "UserID";
        ddlShippingPerson.DataBind();
        ddlShippingPerson.Items.Insert(0, "-Select-");
        ddlShippingPerson.SelectedItem.Value = "0";
        ddlShippingPerson.Items[0].Value = "0";
  
            //            txtDelAdd.Text = "";
            txtGoodsDate.Text = "";
            txtNote.Text = "";
          
            txtRecieptNo.Text = "";
          
            txtShipCost.Text = "";
            txtShippersDocNo.Text = "";
            txtTrckNo.Text = "";
          
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            ViewState["dt"] = null;
            hdnWHid.Value = "0";

        //    ddlWarehouse_SelectedIndexChanged(sender, e);
        //}
    }


 
    protected void ddlshipoption_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int arf = 1;
        int fl = 0;
                    int flagtest = 0;
                    foreach (GridViewRow grd in GridView1.Rows)
                    {
                        Label lblqtyonhand = ((Label)(grd.FindControl("lblqtyonhand")));
                        Label lblqty = ((Label)(grd.FindControl("lblqty")));
                       double one = Convert.ToDouble(grd.Cells[6].Text);//order qty
                      
                        TextBox txt = ((TextBox)(grd.FindControl("TextBox4")));
                        if (txt.Text == null || txt.Text == "" || txt.Text == "0.00")
                        {
                            txt.Text = "0.00";
                        }
                        double two = Convert.ToDouble(txt.Text);
                        if (Convert.ToDecimal(txt.Text) > 0)
                        {
                            fl = 1;

                        }
                        if (Convert.ToDecimal(lblqtyonhand.Text) <= 0)
                        {
                            arf = 0;
                        }
                        else if (Convert.ToDecimal(lblqtyonhand.Text) < Convert.ToDecimal(txt.Text))
                        {
                            arf = 0;
                        }
                        if (one > two)
                        {
                            flagtest = 1;
                            break;
                        }
                      
                    }
                    foreach (GridViewRow grd in GridView3.Rows)
                    {
                        Label lblqty = ((Label)(grd.FindControl("lblqty")));
                        double one = Convert.ToDouble(grd.Cells[6].Text);//order qty

                        TextBox txt = ((TextBox)(grd.FindControl("TextBox4")));
                        if (txt.Text == null || txt.Text == "" || txt.Text == "0.00")
                        {
                            txt.Text = "0.00";
                        }
                        if (Convert.ToDecimal(txt.Text) > 0)
                        {
                            fl = 1;

                        }
                        double two = Convert.ToDouble(txt.Text);
                        if (one > two)
                        {
                            flagtest = 1;
                            break;
                        }
                        
                    }
                    if (arf == 1)
                    {
                        if (fl == 1)
                        {
                            //if (flagtest == 1 && RadioButtonList1.SelectedValue == "3")
                            //{
                            //    ModalPopupExtender1.Show();
                            //}
                            //else
                            //{


                                YesNoSubmit();


                           // }
                        }
                        else
                        {
                            lblmsg.Text = "Sorry,Shipped quantity more then unshipped quantity not allowed";
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Quantity on hand must be greater than the shipped quantity";
                        //lblmsg.Text = "This order has already been shipped";
                       
                    }
              
        int k = (int)FillDelNo();
        lblDelNo.Text = k.ToString();
      

    }


    
    public void InsertTransactionDetail(int AccountDebit, int AccountCredit, decimal AmountDebit, decimal AmountCredit, int Tranction_Master_Id, string Memo, DateTime DateTimeOfTransaction, string DiscEarn, string DiscPaid)
    {

        string str2 = "insert into Tranction_Details(AccountDebit,AccountCredit,AmountDebit,AmountCredit,Tranction_Master_Id,Memo,DateTimeOfTransaction,DiscEarn,DiscPaid,compid,Whid)" +
            " values('" + AccountDebit + "','" + AccountCredit + "','" + AmountDebit + "','" + AmountCredit + "','" + Tranction_Master_Id + "','" + Memo + "','" + DateTimeOfTransaction.ToShortDateString() + "','" + DiscEarn + "','" + DiscPaid + "','"+Session["comid"]+"','"+ddlWarehouse.SelectedValue+"')";
        SqlCommand cmd2 = new SqlCommand(str2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();
    }
    public int FillDelNo2()
    {
        int i = 1;
        string str = "SELECT MAX(EntryNumber) AS invNo FROM  TranctionMaster where EntryTypeId=26 and Whid='"+ddlWarehouse.SelectedValue+"' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            if (ds.Rows[0]["invNo"].ToString() != "")
            {


                i = Convert.ToInt32(ds.Rows[0]["invNo"]) + 1;
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
        return i;
    }
    protected void clear()
    {
        txtTrckNo.Text = "";
        btprintpak.Text = "Email Packing Slip";
        btnprintcreditinv.Text = "Email Invoice";
        ddlShippers.SelectedIndex = -1;
        ddlshipoption.SelectedIndex = -1;
        object sender = new object();
        EventArgs e = new EventArgs();
        txtShippersDocNo.Text = "";
        txtNote.Text = "";
        txtGoodsDate.Text = "";
        txtRecieptNo.Text = "";
     
        ddlShippingPerson.SelectedIndex = -1;
        ddlSalesOrdStatus.SelectedIndex = -1;
        
        lblPartyName.Text = "";
        lblBillAddress.Text = "";
        lblShipAddress.Text = "";
        //ddlSalesOrdStatus.SelectedIndex = -1;
        ddlRefSellno.SelectedIndex = 0;
        btnprintcreditinv.Enabled = true;
        btprintpak.Enabled = true;
        //ddlSalesOrdStatus_SelectedIndexChanged(sender, e);
        ddlShippers_SelectedIndexChanged(sender, e);
        pnlsh.Visible = false;
       // FileUpload1.Visible = true;
       // lblfilename1.Text = "";

    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlShippers.SelectedIndex != 0)
            {
                if (ddlshipoption.SelectedIndex != 0)
                {
                    if (ddlShippingPerson.SelectedIndex != 0)
                    {
                        int flagtest = 0;
                        foreach (GridViewRow grd in GridView1.Rows)
                        {
                            Label lblqty = ((Label)(grd.FindControl("lblqty")));

                          //  double one = Convert.ToDouble(lblqty.Text);//order qty
                            double one = Convert.ToDouble(grd.Cells[6].Text);//order qty
                            //int two = Convert.ToInt32(((TextBox)grd.FindControl("TextBox3")).Text);//shipped qty
                            TextBox txt = ((TextBox)(grd.FindControl("TextBox4")));
                            if (txt.Text == null || txt.Text == "" || txt.Text == "0.00")
                            {
                                txt.Text = "0.00";
                                

                            }
                            double two = Convert.ToDouble(txt.Text);
                            if (one > two)
                            {
                                flagtest = 1;
                                break;
                            }
                        }
                        if (flagtest == 1 && RadioButtonList1.SelectedValue == "4")
                        {
                            //ModalPopupExtender2.Show();
                            lblmsg.Text = "Invoice and packing slip are already existed for this Order";
                        }
                        else
                        {

                            
                            
                            YesNoSubmit();
                           
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Select Shipping Person.";
                    }
                }
                else
                {
                    lblmsg.Text = "Select Shippers option.";
                }

            }
            else
            {
                lblmsg.Text = "Select Shippers.";
            }
            int k = (int)FillDelNo();
            lblDelNo.Text = k.ToString();

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
       // ModalPopupExtender2.Show();
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        RadioButtonList1.SelectedIndex = 1;
        try
        {
            if (ddlShippers.SelectedIndex != 0)
            {
                if (ddlshipoption.SelectedIndex != 0)
                {
                    if (ddlShippingPerson.SelectedIndex != 0)
                    {
                        int flagtest = 0;
                        foreach (GridViewRow grd in GridView1.Rows)
                        {
                            double one = Convert.ToDouble(grd.Cells[5].Text);//order qty
                            //int two = Convert.ToInt32(((TextBox)grd.FindControl("TextBox3")).Text);//shipped qty
                            TextBox txt = ((TextBox)(grd.FindControl("TextBox4")));
                            if (txt.Text == null || txt.Text == "" || txt.Text == "0.00")
                            {
                                txt.Text = "0.00";
                            }
                            double two = Convert.ToDouble(txt.Text);
                            if (one > two)
                            {
                                flagtest = 1;
                                break;
                            }
                        }
                        if (flagtest == 1 && RadioButtonList1.SelectedValue == "4")
                        {
                            ModalPopupExtender2.Show();
                            lblmsg.Text = "Invoice and packing slip are already existed for this Order";
                        }
                        else
                        {

                            YesNoSubmit();
                            
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Select Shipping Person.";
                    }
                }
                else
                {
                    lblmsg.Text = "Select Shippers option.";
                }

            }
            else
            {
                lblmsg.Text = "Select Shippers.";
            }
            int k = (int)FillDelNo();
            lblDelNo.Text = k.ToString();

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }

       // ModalPopupExtender2.Show();
    }
    protected void ddlSalesOrdStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        //fillorder();
    }
    protected void fillorder()
    {
        EventArgs e=new EventArgs();
        object sender=new object();
        DataView rrr = (DataView)fillSalesOrder();

        ddlRefSellno.DataSource = rrr;

        ddlRefSellno.DataTextField = "SalesOrderName";
        ddlRefSellno.DataValueField = "SalesOrderId";
        ddlRefSellno.DataBind();
        ddlRefSellno.Items.Insert(0, "-Select-");
        ddlRefSellno.SelectedItem.Value = "0";
      
           
    }
    protected void imgbtnPrintCreditInv_Click(object sender,EventArgs e)
    {
        btnprintcreditinv.Enabled = true;
            String temp = "SalesInvoiceShow.aspx?id=" + ddlRefSellno.SelectedValue + "&id2=" + Convert.ToInt32(ViewState["InNo1"]) + "&id3=" + Convert.ToInt32(ViewState["DcNo1"]);
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);
            ModalPopupExtender2.Show();
          
            
    }
    protected void imgbtnPrintPackingSlip_Click(object sender, EventArgs e)
    {
        btprintpak.Enabled = true;
        String temp = "SalesRelatedReport2.aspx?wid=" + ddlWarehouse.SelectedValue + "&id=" + ddlRefSellno.SelectedValue + "&id2=" + Session["salesno"] + "&id3=" + Convert.ToInt32(ViewState["InNo1"]);
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);
            ModalPopupExtender2.Show();
          
            
    }
   



    public decimal countWeight()
    {
        
            decimal TotalWeg = 0;
            decimal temp = 0;
            foreach (GridViewRow dr in GridView1.Rows)
            {
                string stridw = " SELECT     InventoryDetails.Weight, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                        " FROM         InventoryDetails RIGHT OUTER JOIN " +
                      "  InventoryMaster ON InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId RIGHT OUTER JOIN " +
                      " InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId " +
                      " where InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + dr.Cells[0].Text + "' ";
                SqlCommand cmdidw = new SqlCommand(stridw, con);
                SqlDataAdapter adpidw = new SqlDataAdapter(cmdidw);
                DataTable dtidw = new DataTable();
                adpidw.Fill(dtidw);
                TextBox qt =(TextBox)dr.FindControl("TextBox4");
                double qty = isdoubleornot(qt.Text);
                temp = Convert.ToDecimal(qty) * Convert.ToDecimal(dtidw.Rows[0]["Weight"]);
                TotalWeg = TotalWeg + temp;
            }
            return TotalWeg;
         
    }
    public int CountQty()
    {
         
            int TotalQty = 0;
            foreach (GridViewRow dr in GridView1.Rows)
            {
                TextBox qt = (TextBox)dr.FindControl("TextBox4");
                double qty = isdoubleornot(qt.Text);
                TotalQty = TotalQty + Convert.ToInt32(qty);
            }
            return TotalQty;
       

    }
    public double countHandlingCharg(int Qty, decimal Weight, int SubCatID, decimal rate)
    {
        string str = "SELECT  Amount, PackhandMasterId, IsPercent, IsPerUnit, IsPerFlatOrder, InvSubSubCategoryId " +
                    " FROM PackingHandlingChargesMaster " +
                    " WHERE ('" + Weight + "' BETWEEN MinOrderWeight AND MaxOrderWeight) AND ('" + Qty + "' BETWEEN MinitemNo AND MaxItemNo) AND (InvSubSubCategoryId = '" + SubCatID + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            if (ds.Rows[0]["IsPercent"].ToString() == "True")
            {
                double first = Convert.ToDouble(Qty) * Convert.ToDouble(rate);
                double HandlingCharges = (first * Convert.ToDouble(ds.Rows[0]["Amount"])) / 100;


                Session["PackHandlingMasterId"] = ds.Rows[0]["PackhandMasterId"].ToString();
                Session["PackHandlingRate"] = ds.Rows[0]["Amount"].ToString();
                Session["PackHandlingIsPercent"] = ds.Rows[0]["IsPercent"].ToString();
                Session["PackHandlingIsPerUnit"] = ds.Rows[0]["IsPerUnit"].ToString();
                Session["PackHandlingIsFlatAmount"] = ds.Rows[0]["IsPerFlatOrder"].ToString();

                return HandlingCharges;
            }
            else if (ds.Rows[0]["IsPerUnit"].ToString() == "True")
            {

                double HandlingCharges = Convert.ToDouble(Qty) * Convert.ToDouble(ds.Rows[0]["Amount"]);

                Session["PackHandlingMasterId"] = ds.Rows[0]["PackhandMasterId"].ToString();
                Session["PackHandlingRate"] = ds.Rows[0]["Amount"].ToString();
                Session["PackHandlingIsPercent"] = ds.Rows[0]["IsPercent"].ToString();
                Session["PackHandlingIsPerUnit"] = ds.Rows[0]["IsPerUnit"].ToString();
                Session["PackHandlingIsFlatAmount"] = ds.Rows[0]["IsPerFlatOrder"].ToString();

                return HandlingCharges;
            }
            else if (ds.Rows[0]["IsPerFlatOrder"].ToString() == "True")
            {
                double HandlingCharges = Convert.ToDouble(ds.Rows[0]["Amount"]);


                Session["PackHandlingMasterId"] = ds.Rows[0]["PackhandMasterId"].ToString();
                Session["PackHandlingRate"] = ds.Rows[0]["Amount"].ToString();
                Session["PackHandlingIsPercent"] = ds.Rows[0]["IsPercent"].ToString();
                Session["PackHandlingIsPerUnit"] = ds.Rows[0]["IsPerUnit"].ToString();
                Session["PackHandlingIsFlatAmount"] = ds.Rows[0]["IsPerFlatOrder"].ToString();

                return HandlingCharges;
            }
            else
            {

                Session["PackHandlingMasterId"] = "0";
                Session["PackHandlingRate"] = "0";
                Session["PackHandlingIsPercent"] = "0";
                Session["PackHandlingIsPerUnit"] = "0";
                Session["PackHandlingIsFlatAmount"] = "0";
                return 0;
            }



        }
        else
        {
            Session["PackHandlingMasterId"] = "0";
            Session["PackHandlingRate"] = "0";
            Session["PackHandlingIsPercent"] = "0";
            Session["PackHandlingIsPerUnit"] = "0";
            Session["PackHandlingIsFlatAmount"] = "0";
            return 0;
        }

    }
    
    public void CalculateCatesAndSubCates()
    {
           try
            {
                string CatIds = "";
                string ScatIds = "";
                string SScatIds = "";

                
                 
                    foreach (GridViewRow dtrr in GridView1.Rows)
                    {
                        string getCsCssC = "SELECT     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoruSubSubCategory.InventorySubSubId, " +
                       "     InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName,  " +
                       "    InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventoryMaster.InventoryMasterId, InventoryMaster.Name " +
                       "  FROM         InventorySubCategoryMaster LEFT OUTER JOIN " +
                        "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
                        "  InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
                        "  InventoryWarehouseMasterTbl RIGHT OUTER JOIN " +
                        "  InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId ON  " +
                        "  InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId where InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + dtrr.Cells[0].Text + "' ";
                        SqlCommand cmd11111 = new SqlCommand(getCsCssC, con);
                        SqlDataAdapter dbss14 = new SqlDataAdapter(cmd11111);
                        //DataTable dtgetids = dbss1.cmdSelect(getCsCssC);
                        DataTable dtgetids = new DataTable();
                        dbss14.Fill(dtgetids);
                        if (dtgetids.Rows.Count > 0)
                        {
                            CatIds += dtgetids.Rows[0]["InventeroyCatId"].ToString() + ",";
                            ScatIds += dtgetids.Rows[0]["InventorySubCatId"].ToString() + ",";
                            SScatIds += dtgetids.Rows[0]["InventorySubSubId"].ToString() + ",";
                        }
                    }

                 
                if (CatIds == "" || ScatIds == "" || SScatIds == "")
                {

                }
                else
                {
                    String Cids = CatIds.Substring(0, CatIds.Length - 1);
                    String SCids = ScatIds.Substring(0, ScatIds.Length - 1);
                    String SSCids = SScatIds.Substring(0, SScatIds.Length - 1);
                    ViewState["Cids"] = Cids;
                    ViewState["SCids"] = SCids;
                    ViewState["SSCids"] = SSCids;
                }
            }
            catch
            {

            }


       



    }
    public void countTotalHandlingCharg()
    {

        try
        {

            decimal Weight = countWeight();
            int Qty = CountQty();
            //DataTable dt = new DataTable();
            //dt = (DataTable)Session["cart"];
            // double HandlingCharges = countHandlingCharg(Convert.ToInt32(dr["Qty"]), Convert.ToDecimal(dr["Weight"]), Convert.ToInt32(dr["InventorySubSubId"]), Convert.ToDecimal(dr["price"]));
            string strhc1 = "SELECT  Amount, PackhandMasterId, IsPercent, IsPerUnit,  IsPerFlatOrder, MinOrderValue, MaxOrderValue, MinOrderWeight, MaxOrderWeight, " +
                   "   MinitemNo, MaxItemNo, InvCategoryId, InvSubCategoryId, EntryDate, Name, InvSubSubCategoryId " +
                     " FROM PackingHandlingChargesMaster " +
                     " WHERE(compid='"+Session["comid"]+"')and ('" + Weight + "' BETWEEN MinOrderWeight AND MaxOrderWeight) AND ('" + Qty + "' BETWEEN MinitemNo AND MaxItemNo)   and ('" + Convert.ToDecimal(lblSubTotal.Text) + "' between  MinOrderValue and MaxOrderValue  )";
            SqlCommand cmd11111 = new SqlCommand(strhc1, con);
            SqlDataAdapter dbss15 = new SqlDataAdapter(cmd11111);
           // DataTable dthc1 = dbss1.cmdSelect(strhc1 + " and InvCategoryId=0 ");
            DataTable dthc1 = new DataTable(strhc1 + " and InvCategoryId=0 ");
            dbss15.Fill(dthc1);
            if (dthc1.Rows.Count > 0)
            {
                if (dthc1.Rows[0]["IsPercent"].ToString() == "True")
                {
                    double percqtyhc1 = Convert.ToDouble((Qty * isdoubleornot(dthc1.Rows[0]["Amount"].ToString())) / 100);
                    lblOVHCharge.Text = String.Format("{0:n}", percqtyhc1);         //dthc1.Rows[0]["Amount"].ToString();
                }
                else if (dthc1.Rows[0]["IsPerUnit"].ToString() == "True")
                {
                    double perunithc1 = Convert.ToDouble(Qty * isdoubleornot(dthc1.Rows[0]["Amount"].ToString()));
                    lblOVHCharge.Text = String.Format("{0:n}", perunithc1);
                }
                else
                {
                    double perunithc1 = isdoubleornot(dthc1.Rows[0]["Amount"].ToString());
                    lblOVHCharge.Text = String.Format("{0:n}", perunithc1);
                }

                // lblHandlingChrg.Text = dthc1.Rows[0]["Amount"].ToString();
            }
            else
            {
                CalculateCatesAndSubCates();
                //ViewState["Cids"] = Cids;
                //   ViewState["SCids"] = SCids;
                //   ViewState["SSCids"] = SSCids;
                if (ViewState["Cids"] == null || ViewState["Cids"].ToString() == "" || ViewState["SCids"] == null || ViewState["SCids"].ToString() == "" || ViewState["SSCids"] == null || ViewState["SSCids"].ToString() == "")
                {

                }
                else
                {
                    SqlCommand cmd22222 = new SqlCommand(strhc1, con);
                    SqlDataAdapter dbss16 = new SqlDataAdapter(cmd22222);
                    DataTable dthc2 = new DataTable(strhc1 + " and InvCategoryId in ('" + ViewState["Cids"].ToString() + "') and InvSubCategoryId=0 ");
                    dbss16.Fill(dthc2);
                    // DataTable dthc2 = dbss1.cmdSelect(strhc1 + " and InvCategoryId in ('" + ViewState["Cids"].ToString() + "') and InvSubCategoryId=0 ");
                    if (dthc2.Rows.Count > 0)
                    {
                        if (dthc2.Rows[0]["IsPercent"].ToString() == "True")
                        {
                            double percqtyhc1 = Convert.ToDouble((Qty * isdoubleornot(dthc2.Rows[0]["Amount"].ToString())) / 100);
                            lblOVHCharge.Text = String.Format("{0:n}", percqtyhc1);         //dthc2.Rows[0]["Amount"].ToString();
                        }
                        else if (dthc2.Rows[0]["IsPerUnit"].ToString() == "True")
                        {
                            double perunithc1 = Convert.ToDouble(Qty * isdoubleornot(dthc2.Rows[0]["Amount"].ToString()));
                            lblOVHCharge.Text = String.Format("{0:n}", perunithc1);
                        }
                        else
                        {
                            double perunithc1 = isdoubleornot(dthc2.Rows[0]["Amount"].ToString());
                            lblOVHCharge.Text = String.Format("{0:n}", perunithc1);
                        }
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand(strhc1, con);
                        SqlDataAdapter dbss17 = new SqlDataAdapter(cmd);
                        DataTable dthc3 = new DataTable(strhc1 + " and InvSubCategoryId in ('" + ViewState["SCids"].ToString() + "')   ");
                        dbss17.Fill(dthc3);
                        //DataTable dthc3 = dbss1.cmdSelect(strhc1 + " and InvSubCategoryId in ('" + ViewState["SCids"].ToString() + "')   ");
                        if (dthc3.Rows.Count > 0)
                        {
                            if (dthc3.Rows[0]["IsPercent"].ToString() == "True")
                            {
                                double percqtyhc1 = Convert.ToDouble((Qty * isdoubleornot(dthc3.Rows[0]["Amount"].ToString())) / 100);
                                lblOVHCharge.Text = String.Format("{0:n}", percqtyhc1);         //dthc3.Rows[0]["Amount"].ToString();
                            }
                            else if (dthc3.Rows[0]["IsPerUnit"].ToString() == "True")
                            {
                                double perunithc1 = Convert.ToDouble(Qty * isdoubleornot(dthc3.Rows[0]["Amount"].ToString()));
                                lblOVHCharge.Text = String.Format("{0:n}", perunithc1);
                            }
                            else
                            {
                                double perunithc1 = isdoubleornot(dthc3.Rows[0]["Amount"].ToString());
                                lblOVHCharge.Text = String.Format("{0:n}", perunithc1);
                            }
                        }
                        else
                        {
                            SqlCommand cmd3333 = new SqlCommand(strhc1, con);
                            SqlDataAdapter dbss18 = new SqlDataAdapter(cmd3333);
                            //DataTable dthc4 = dbss1.cmdSelect(strhc1 + " and     InvSubSubCategoryId in ('" + ViewState["SSCids"].ToString() + "')  ");
                            DataTable dthc4 = new DataTable(strhc1 + " and     InvSubSubCategoryId in ('" + ViewState["SSCids"].ToString() + "')  ");
                            dbss18.Fill(dthc4);
                            if (dthc4.Rows.Count > 0)
                            {
                                if (dthc4.Rows[0]["IsPercent"].ToString() == "True")
                                {
                                    double percqtyhc1 = Convert.ToDouble((Qty * isdoubleornot(dthc4.Rows[0]["Amount"].ToString())) / 100);
                                    lblOVHCharge.Text = String.Format("{0:n}", percqtyhc1);         //dthc4.Rows[0]["Amount"].ToString();
                                }
                                else if (dthc4.Rows[0]["IsPerUnit"].ToString() == "True")
                                {
                                    double perunithc1 = Convert.ToDouble(Qty * isdoubleornot(dthc4.Rows[0]["Amount"].ToString()));
                                    lblOVHCharge.Text = String.Format("{0:n}", perunithc1);
                                }
                                else
                                {
                                    double perunithc1 = isdoubleornot(dthc4.Rows[0]["Amount"].ToString());
                                    lblOVHCharge.Text = String.Format("{0:n}", perunithc1);
                                }
                            }
                        }
                    }
                }
            }
        }

        catch
        {

        }

    }


 

    public void CountCustomerDisc()
    {
        int uid = 0;
        String temp = ddlRefSellno.SelectedValue.ToString(); 
        
        string strpid = "select SalesOrderNo,SalesManId,PartyId,SalesOrderDate from SalesOrderMaster where SalesOrderId='" + ddlRefSellno.SelectedValue + "' ";
        SqlCommand cmdtt = new SqlCommand(strpid, con);
        SqlDataAdapter adptr = new SqlDataAdapter(cmdtt);
        DataTable dttr = new DataTable();
        adptr.Fill(dttr);

        string pid = dttr.Rows[0]["PartyId"].ToString();

        if (pid != "")
        {
            string str = "SELECT     PartyTypeCategoryMasterTbl.IsPercentage, PartyTypeCategoryMasterTbl.PartyCategoryDiscount, PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
                        " FROM         Party_master INNER JOIN " +
                         " PartyTypeDetailTbl ON Party_master.PartyID = PartyTypeDetailTbl.PartyID INNER JOIN " +
                          " User_master ON Party_master.PartyID = User_master.PartyID INNER JOIN " +
                         " PartyTypeCategoryMasterTbl ON PartyTypeDetailTbl.PartyTypeCategoryMasterId = PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId " +
                " WHERE (Party_master.Whid='"+ddlWarehouse.SelectedValue+"') and (User_master.UserID = '" + pid + "') AND (PartyTypeCategoryMasterTbl.Active = 1) " +
                " ORDER BY PartyTypeCategoryMasterTbl.EntryDate DESC ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                int i = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPercentage"]);

                if (i == 1)
                {
                    
                    double first = Convert.ToDouble(lblSubTotal.Text);
                    double CustDis = (first * Convert.ToDouble(ds.Tables[0].Rows[0]["PartyCategoryDiscount"])) / 100;

                    lblCDisc.Text = String.Format("{0:n}", CustDis);


                }
                else
                {
                    
                    double CustDis = Convert.ToDouble(ds.Tables[0].Rows[0]["PartyCategoryDiscount"]);
                    lblCDisc.Text = String.Format("{0:n}", CustDis);
                }

                Session["PatryTypeCategoryMasterId"] = ds.Tables[0].Rows[0]["PartyTypeCategoryMasterId"].ToString();
                Session["PartyDiscountRate"] = ds.Tables[0].Rows[0]["PartyCategoryDiscount"].ToString();
                Session["PartyDiscountIsPercent"] = ds.Tables[0].Rows[0]["IsPercentage"].ToString();
            }
            else
            {
                Session["PatryTypeCategoryMasterId"] = "0";
                Session["PartyDiscountRate"] = "0";
                Session["PartyDiscountIsPercent"] = 0;
            }

        }
    }

  

    public void CountOrderValue()
    {
        try
        {
            double Value = Convert.ToDouble(lblSubTotal.Text)  ;
            string str = "SELECT     IsPercentage, Active, OrderValueDiscountMasterId, ValueDiscount FROM   OrderValueDiscountMaster " +
                        " where(compid='"+Session["comid"]+"')and (active =1) and ( '" + Value + "'  between MinValue and MaxValue) " +
                        " ORDER BY StartDate DESC ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                int i = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPercentage"]);

                if (i == 1)
                {
                    string str1 = " SELECT  IsPercentage, Active, StartDate, ValueDiscount FROM OrderValueDiscountMaster " +
                                    " WHERE (compid='" + Session["comid"] + "')and  (Active = 1) AND ('" + Value + "' BETWEEN MinValue AND MaxValue) " +
                                     " ORDER BY StartDate DESC ";
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    adp1.Fill(ds1);
                    double first = Value;
                    double OrderDisc = (first * Convert.ToDouble(ds1.Tables[0].Rows[0]["ValueDiscount"])) / 100;

                    lblVDis.Text = String.Format("{0:n}", OrderDisc);

                }
                else
                {
                    string str2 = " SELECT  IsPercentage, Active, StartDate, ValueDiscount FROM OrderValueDiscountMaster " +
                                   " WHERE (compid='" + Session["comid"] + "')and  (Active = 1) AND ('" + Value + "' BETWEEN MinValue AND MaxValue) " +
                                    " ORDER BY StartDate DESC ";
                    SqlCommand cmd2 = new SqlCommand(str2, con);
                    SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    adp2.Fill(ds2);

                    double OrderDisc = Convert.ToDouble(ds2.Tables[0].Rows[0]["ValueDiscount"]);
                    lblVDis.Text = String.Format("{0:n}", OrderDisc);
                }
                Session["OrderValueDiscountMasterId"] = ds.Tables[0].Rows[0]["OrderValueDiscountMasterId"].ToString();
                Session["OrderValueDiscountRate"] = ds.Tables[0].Rows[0]["ValueDiscount"].ToString();
                Session["OrderValueDicountIsPercent"] = ds.Tables[0].Rows[0]["IsPercentage"].ToString();

            }
            else
            {
                Session["OrderValueDiscountMasterId"] = "0";
                Session["OrderValueDiscountRate"] = "0";
                Session["OrderValueDicountIsPercent"] = "0";
                lblVDis.Text = "0";
            }
        }
        catch (Exception tr)
        {

        }
    

    }

  


    public void shippingCharges()
    {
        
        
        
        //lblshippingType.Text

        string strmainstr123 = lblshippingType.Text;

        char[] separator = new char[] { ':' };

        string[] strSplitArr = strmainstr123.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);

        int ikno = strSplitArr.Length;
        if (ikno >= 2)
        {
            string shipcharge = strSplitArr[i - 1].ToString();

            double sch = isdoubleornot(shipcharge);

            txtShipCost.Text = String.Format("{0:n}", sch);
            lblShipCharge.Text = txtShipCost.Text;




        }
        else
        {
            txtShipCost.Text = String.Format("{0:n}", "0.0");
            lblShipCharge.Text = txtShipCost.Text;
        }
    }
    protected double isdoubleornot(string ck)
    {
        double i = 0;
        try
        {
            i = Convert.ToDouble(ck);
            return i;
        }
        catch
        {
            return i;
        }
    }
    protected void imgbtnCancel_Click(object sender, EventArgs e)
    {
        pnlsh.Visible = false;
        fillorder();
        clear();

    }


    protected void YesNoSubmit()
    {

        string str = "Update  SalesChallanMaster set PartyID='" + ddlParty.SelectedValue + "',SalesChallanDatetime='" + Convert.ToDateTime(lblDate.Text).ToShortDateString() + "', " +
     " ShipToAddress='" + lblShipAddress.Text + "', BillToAddress='" + lblBillAddress.Text + "' where  SalesChallanMasterId='" + ViewState["DcNo1"] + "'";
  

        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

      
        SqlDataAdapter aadpp = new SqlDataAdapter("SELECT  max(EntryNumber) as entryno FROM TranctionMaster where EntryTypeId=26 and Whid='"+ddlWarehouse.SelectedValue+"' ", con);
        DataSet ddss = new DataSet();
        aadpp.Fill(ddss);
        int entryno = 1;
        if (ddss.Tables[0].Rows[0]["entryno"].ToString() != "")
        {
            entryno = Convert.ToInt32(ddss.Tables[0].Rows[0]["entryno"]) + 1;
        }
        else
        {
            entryno = 1;
        }
        string str2 = "Update SalesChallanDetail set Notes='" + txtNote.Text + "' where SalesChallanMasterId= '" + Convert.ToInt32(ViewState["DcNo1"]) + "'";
                 
                    SqlCommand cmd2 = new SqlCommand(str2, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    string str3 = "Update  SalesChallanMoreInfo Set ShippersShipOptionMasterId='" + ddlshipoption.SelectedValue + "', ShippersId='" + ddlShippers.SelectedValue + "', ShippersTrackingNo='" + txtTrckNo.Text + "', ShippingPersonId='" + ddlShippingPerson.SelectedValue + "', UserId='" + Convert.ToInt32(Session["userid"]) + "'," +
                       " PurchaseOrder='" + txtShippersDocNo.Text + "',Terms='" + txtGoodsDate.Text + "' where SalesChallanMasterId='" + ViewState["DcNo1"] + "' ";
           

              SqlCommand cmd3 = new SqlCommand(str3, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd3.ExecuteNonQuery();
        con.Close();

        string str1de = "Delete from SalesChallanTransaction where SalesChallanMasterId='" + ViewState["DcNo1"] + "'";

        SqlCommand cmd1de = new SqlCommand(str1de, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd1de.ExecuteNonQuery();
        con.Close();
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            TextBox txtqty = (TextBox)gdr.FindControl("TextBox4");
            TextBox txtnotegrid = (TextBox)gdr.FindControl("TextBox6");
            if (Convert.ToDecimal(txtqty.Text) > 0)
            {
                string str1 = "INSERT INTO SalesChallanTransaction " +
                          " (SalesChallanMasterId, inventoryWHM_Id, Quantity, Note) " +
                        " VALUES('" + ViewState["DcNo1"] + "','" + Convert.ToInt32(gdr.Cells[0].Text) + "','" + Convert.ToDecimal(txtqty.Text) + "','" + txtnotegrid.Text + "')";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();
            }
        }
        foreach (GridViewRow gdr in GridView3.Rows)
        {
             TextBox txtqty = (TextBox)gdr.FindControl("TextBox4");
                TextBox txtnotegrid = (TextBox)gdr.FindControl("TextBox6");
                if (Convert.ToDecimal(txtqty.Text) > 0)
                {

                    string str1 = "INSERT INTO SalesChallanTransaction " +
                              " (SalesChallanMasterId, inventoryWHM_Id, Quantity, Note) " +
                            " VALUES('" + ViewState["DcNo1"] + "','" + Convert.ToInt32(gdr.Cells[0].Text) + "','" + Convert.ToDecimal(txtqty.Text) + "','" + txtnotegrid.Text + "')";
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
        }
        FillSaleInvoiceAmts();
        SqlDataAdapter adpp = new SqlDataAdapter("SELECT Account, PartyID FROM Party_master WHERE PartyID = '" + ViewState["pp"].ToString() + "' and  Whid='"+ddlWarehouse.SelectedValue+"'", con);
        DataTable dspp = new DataTable();
        adpp.Fill(dspp);

        int partyAccount = Convert.ToInt32(dspp.Rows[0]["Account"]);
        decimal TotalSub = Convert.ToDecimal(lblSubTotalSI.Text);
        decimal CustomerDisc = Convert.ToDecimal(lblCustDisSI.Text);
        decimal OrderValueDisc = Convert.ToDecimal(lblValueDisSI.Text);
        decimal OverallHandCharg = Convert.ToDecimal(lblHandChrgSI.Text);
        decimal shippingChrg = Convert.ToDecimal(lblShipCharge.Text);
        decimal tax = Convert.ToDecimal(lblTaxSI.Text);
        decimal GrossTotal = Convert.ToDecimal(lblTotalSI.Text);

        Session["ST"] = TotalSub;
        Session["CD"] = CustomerDisc;
        Session["OVD"] = OrderValueDisc;
        Session["HC"] = OverallHandCharg;
        Session["SC"] = shippingChrg;
        Session["Tax"] = tax;
        Session["GT"] = GrossTotal;

        string stsalod = "Delete from StatusControl where TranctionMasterId='" + ViewState["tid"] + "'";

        SqlCommand cdfdf = new SqlCommand(stsalod, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cdfdf.ExecuteNonQuery();
        con.Close();
        string stpayapp = "Delete from PaymentApplicationTbl where  TranMIdPayReceived='" + ViewState["tid"] + "' or TranMIdAmtApplied='" + ViewState["tid"] + "'";

        SqlCommand dfpay = new SqlCommand(stpayapp, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        dfpay.ExecuteNonQuery();
        con.Close();
        if (checkGnrInvoice.Checked == true)
        {
            GenerateSalesInvoice(Convert.ToInt32(ddlRefSellno.SelectedValue));
            //**************
            Session["salesordermasterid"] = Convert.ToString(Convert.ToInt32(ddlRefSellno.SelectedValue));
        
        //********************
        }
        int trnid = Convert.ToInt32(ViewState["trmid1"]);


       

        int flag = 0;
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            Label txtQtyDiff = (Label)gdr.FindControl("TextBox5");
            if (Convert.ToDecimal(txtQtyDiff.Text) != 0)
            {
                flag = 1;
            }

        }
        foreach (GridViewRow gdr in GridView3.Rows)
        {
            Label txtQtyDiff = (Label)gdr.FindControl("TextBox5");
            if (Convert.ToDecimal(txtQtyDiff.Text) != 0)
            {
                flag = 1;
            }

        }
        if (flag == 0)
        {
            if (RadioButtonList1.SelectedValue == "1")
            {
                SqlCommand cmm = new SqlCommand("INSERT INTO StatusControl(SalesOrderId, SalesChallanMasterId, Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                            " VALUES('" + ddlRefSellno.SelectedValue + "','" + Convert.ToInt32(lblDelNo.Text) + "' ,'" + System.DateTime.Now.ToShortDateString() + "','31','" + txtNote.Text + "','" + trnid.ToString() + "') ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm.ExecuteNonQuery();
                con.Close();


                SqlCommand cmm6 = new SqlCommand("UPDATE SalesOrderSuppliment   SET AmountDue ='0'  WHERE SalesOrderMasterId='" + ddlRefSellno.SelectedValue + "'  ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm6.ExecuteNonQuery();
                con.Close();


            }
            else if (RadioButtonList1.SelectedValue == "2")
            {
                SqlCommand cmm = new SqlCommand("INSERT INTO StatusControl(SalesOrderId, SalesChallanMasterId, Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                            " VALUES('" + ddlRefSellno.SelectedValue + "','" + Convert.ToInt32(lblDelNo.Text) + "' ,'" + System.DateTime.Now.ToShortDateString() + "','32','" + txtNote.Text + "','" + trnid.ToString() + "') ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm.ExecuteNonQuery();
                con.Close();

                SqlCommand cmm6 = new SqlCommand("UPDATE SalesOrderSuppliment   SET AmountDue ='" + Convert.ToDouble(GrossTotal) + "'  WHERE SalesOrderMasterId='" + ddlRefSellno.SelectedValue + "'  ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm6.ExecuteNonQuery();
                con.Close();

                string sgt66 = "SELECT     TranctionMaster.EntryNumber, TranctionMaster.Date, TranctionMaster.Tranction_Master_Id, TranctionMaster.Tranction_Amount, TranctionMaster.UserId,  " +
                    "  TranctionMaster.EntryTypeId, TransactionMasterMoreInfo.SalesOrderId " +
                    " FROM         TransactionMasterMoreInfo INNER JOIN " +
                    "   TranctionMaster ON TransactionMasterMoreInfo.Tranction_Master_Id = TranctionMaster.Tranction_Master_Id " +
                    " WHERE     (TranctionMaster.EntryTypeId = 26) AND (TransactionMasterMoreInfo.SalesOrderId = '" + ddlRefSellno.SelectedValue + "' ) ";
                SqlCommand cd66 = new SqlCommand(sgt66, con);
                SqlDataAdapter adp66 = new SqlDataAdapter(cd66);
                DataTable dt66 = new DataTable();
                adp66.Fill(dt66);
                double amt = 0;
                if (dt66.Rows.Count > 0)
                {
                    foreach (DataRow dgh in dt66.Rows)
                    {
                        double amteach = Convert.ToDouble(dgh["Tranction_Amount"]);
                        amt = amt + amteach;
                    }
                }

                Double AA = amt;


                string sgt99 = " SELECT    SalesOrderNo, SalesManId, PartyId, SalesOrderDate, BuyersPOno, ShippersId, ExpextedDeliveryDate, PaymentsTerms, OtherTerms, ShippingCharges,  "+
                     " HandlingCharges, OtherCharges, Discounts, GrossAmount, SalesOrderId FROM         SalesOrderMaster  WHERE     (SalesOrderId = '"+ddlRefSellno.SelectedValue+"' ) ";
                SqlCommand cd99 = new SqlCommand(sgt99, con);
                SqlDataAdapter adp99 = new SqlDataAdapter(cd99);
                DataTable dt99 = new DataTable();
                adp99.Fill(dt99);
                double amtBB = 0;
                if (dt99.Rows.Count > 0)
                {
                    amtBB =Convert.ToDouble( dt99.Rows[0]["GrossAmount"]);
                }

                Double BB = amtBB;



                 string sgt33 = " SELECT     PaymentAppTblId, TranMIdPayReceived, TranMIdAmtApplied, AmtApplied, Date, UserId, SalesOrderId "+
                                "    FROM         PaymentApplicationTbl "+
                "   WHERE     (SalesOrderId = '"+ddlRefSellno.SelectedValue+"') AND (AmtApplied IS NULL or  TranMIdAmtApplied = 0) ";
                SqlCommand cd33 = new SqlCommand(sgt33, con);
                SqlDataAdapter adp33 = new SqlDataAdapter(cd33);
                DataTable dt33 = new DataTable();
                adp33.Fill(dt33);
                double amtCC = 0;
                if (dt33.Rows.Count > 0)
                {
                    amtCC = Convert.ToDouble(dt33.Rows[0]["AmtApplied"]);
                }
                //   record zero for this entry in PaymentApplication tbl for this condtion
                Double CC = amtCC;
   
		
                

            }
            else if (RadioButtonList1.SelectedValue == "3")
            {
                SqlCommand cmm = new SqlCommand("INSERT INTO StatusControl(SalesOrderId, SalesChallanMasterId, Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                            " VALUES('" + ddlRefSellno.SelectedValue + "','" + Convert.ToInt32(lblDelNo.Text) + "' ,'" + System.DateTime.Now.ToShortDateString() + "','24','" + txtNote.Text + "','" + trnid.ToString() + "') ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm.ExecuteNonQuery();
                con.Close();

                SqlCommand cmm6 = new SqlCommand("UPDATE SalesOrderSuppliment   SET AmountDue ='" + Convert.ToDouble(GrossTotal) + "'  WHERE SalesOrderMasterId='" + ddlRefSellno.SelectedValue + "'  ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm6.ExecuteNonQuery();
                con.Close();

                string sgt66 = "SELECT     TranctionMaster.EntryNumber, TranctionMaster.Date, TranctionMaster.Tranction_Master_Id, TranctionMaster.Tranction_Amount, TranctionMaster.UserId,  " +
                    "  TranctionMaster.EntryTypeId, TransactionMasterMoreInfo.SalesOrderId " +
                    " FROM         TransactionMasterMoreInfo INNER JOIN " +
                    "   TranctionMaster ON TransactionMasterMoreInfo.Tranction_Master_Id = TranctionMaster.Tranction_Master_Id " +
                    " WHERE     (TranctionMaster.EntryTypeId = 26) AND (TransactionMasterMoreInfo.SalesOrderId = '" + ddlRefSellno.SelectedValue + "' ) ";
                SqlCommand cd66 = new SqlCommand(sgt66, con);
                SqlDataAdapter adp66 = new SqlDataAdapter(cd66);
                DataTable dt66 = new DataTable();
                adp66.Fill(dt66);
                double amt = 0;
                if (dt66.Rows.Count > 0)
                {
                    foreach (DataRow dgh in dt66.Rows)
                    {
                        double amteach = Convert.ToDouble(dgh["Tranction_Amount"]);
                        amt = amt + amteach;
                    }
                }

                Double AA = amt;


                string sgt99 = " SELECT    SalesOrderNo, SalesManId, PartyId, SalesOrderDate, BuyersPOno, ShippersId, ExpextedDeliveryDate, PaymentsTerms, OtherTerms, ShippingCharges,  " +
                     " HandlingCharges, OtherCharges, Discounts, GrossAmount, SalesOrderId FROM         SalesOrderMaster  WHERE     (SalesOrderId = '" + ddlRefSellno.SelectedValue + "' ) ";
                SqlCommand cd99 = new SqlCommand(sgt99, con);
                SqlDataAdapter adp99 = new SqlDataAdapter(cd99);
                DataTable dt99 = new DataTable();
                adp99.Fill(dt99);
                double amtBB = 0;
                if (dt99.Rows.Count > 0)
                {
                    amtBB = Convert.ToDouble(dt99.Rows[0]["GrossAmount"]);
                }

                Double BB = amtBB;



                string sgt33 = " SELECT     PaymentAppTblId, TranMIdPayReceived, TranMIdAmtApplied, AmtApplied, Date, UserId, SalesOrderId " +
                               "    FROM         PaymentApplicationTbl " +
               "   WHERE     (SalesOrderId = '" + ddlRefSellno.SelectedValue + "') AND (AmtApplied IS NULL or  TranMIdAmtApplied = 0) ";
                SqlCommand cd33 = new SqlCommand(sgt33, con);
                SqlDataAdapter adp33 = new SqlDataAdapter(cd33);
                DataTable dt33 = new DataTable();
                adp33.Fill(dt33);
                double amtCC = 0;
                if (dt33.Rows.Count > 0)
                {
                    amtCC = Convert.ToDouble(dt33.Rows[0]["AmtApplied"]);
                }
                //   record zero for this entry in PaymentApplication tbl for this condtion
                Double CC = amtCC;




            }
            else
            {

                SqlCommand cmm = new SqlCommand("INSERT INTO StatusControl(SalesOrderId, SalesChallanMasterId, Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                            " VALUES('" + ddlRefSellno.SelectedValue + "','" + Convert.ToInt32(lblDelNo.Text) + "' ,'" + System.DateTime.Now.ToShortDateString() + "','18','" + txtNote.Text + "','" + trnid.ToString() + "') ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm.ExecuteNonQuery();
                con.Close();


                SqlCommand cmm6 = new SqlCommand("UPDATE SalesOrderSuppliment   SET AmountDue ='0'  WHERE SalesOrderMasterId='" + ddlRefSellno.SelectedValue + "'  ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm6.ExecuteNonQuery();
                con.Close();





            }


         

        }
        else
        {
          
            if (RadioButtonList1.SelectedValue == "1")
            {
                SqlCommand cmm = new SqlCommand("INSERT INTO StatusControl(SalesOrderId, SalesChallanMasterId, Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                            " VALUES('" + ddlRefSellno.SelectedValue + "','" + Convert.ToInt32(lblDelNo.Text) + "' ,'" + System.DateTime.Now.ToShortDateString() + "','31','" + txtNote.Text + "','" + trnid.ToString() + "') ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm.ExecuteNonQuery();
                con.Close();


                SqlCommand cmm6 = new SqlCommand("UPDATE SalesOrderSuppliment   SET AmountDue ='0'  WHERE SalesOrderMasterId='" + ddlRefSellno.SelectedValue + "'  ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm6.ExecuteNonQuery();
                con.Close();


            }
            else if (RadioButtonList1.SelectedValue == "2")
            {
                SqlCommand cmm = new SqlCommand("INSERT INTO StatusControl(SalesOrderId, SalesChallanMasterId, Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                            " VALUES('" + ddlRefSellno.SelectedValue + "','" + Convert.ToInt32(lblDelNo.Text) + "' ,'" + System.DateTime.Now.ToShortDateString() + "','32','" + txtNote.Text + "','" + trnid.ToString() + "') ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm.ExecuteNonQuery();
                con.Close();

                SqlCommand cmm6 = new SqlCommand("UPDATE SalesOrderSuppliment   SET AmountDue ='" + Convert.ToDouble(GrossTotal) + "'  WHERE SalesOrderMasterId='" + ddlRefSellno.SelectedValue + "'  ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm6.ExecuteNonQuery();
                con.Close();


               


            }
            else if (RadioButtonList1.SelectedValue == "3")
            {
                SqlCommand cmm = new SqlCommand("INSERT INTO StatusControl(SalesOrderId, SalesChallanMasterId, Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                            " VALUES('" + ddlRefSellno.SelectedValue + "','" + Convert.ToInt32(ViewState["DcNo1"]) + "' ,'" + System.DateTime.Now.ToShortDateString() + "','24','" + txtNote.Text + "','" + trnid.ToString() + "') ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm.ExecuteNonQuery();
                con.Close();

                SqlCommand cmm6 = new SqlCommand("UPDATE SalesOrderSuppliment   SET AmountDue ='" + Convert.ToDouble(GrossTotal) + "'  WHERE SalesOrderMasterId='" + ddlRefSellno.SelectedValue + "'  ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm6.ExecuteNonQuery();
                con.Close();





            }
            else
            {

                SqlCommand cmm = new SqlCommand("INSERT INTO StatusControl(SalesOrderId, SalesChallanMasterId, Datetime, StatusMasterId,  note,TranctionMasterId) " +
                                            " VALUES('" + ddlRefSellno.SelectedValue + "','" + Convert.ToInt32(lblDelNo.Text) + "' ,'" + System.DateTime.Now.ToShortDateString() + "','18','" + txtNote.Text + "','" + trnid.ToString() + "') ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm.ExecuteNonQuery();
                con.Close();


                SqlCommand cmm6 = new SqlCommand("UPDATE SalesOrderSuppliment   SET AmountDue ='0'  WHERE SalesOrderMasterId='" + ddlRefSellno.SelectedValue + "'  ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm6.ExecuteNonQuery();
                con.Close();





            }

        }
        if (chkdoc.Checked == true)
        {
            entry();
        }
        if (Request.QueryString["docid"] != null)
        {

            inserdocatt();
        }
        lblmsg.Visible = true;
        lblmsg.Text = "Record Updated Successfully";
        //btnPrintCustomer.Visible = true;
       // clear();
    
        ModalPopupExtender2.Show();
        pnlsh.Visible = false;
    }

    public void CountSubTotal()
    {

        ////DataTable dt = new DataTable();
        ////dt = (DataTable)ViewState["dt"];
        ////double subTotal = 0;
        ////foreach (DataRow dr in dt.Rows)
        ////{
        ////    int invwhidfromgrid = Convert.ToInt32(dr["InventoryWarehouseMasterId"]);
        ////    string str000 = "SELECT     SalesOrderMasterId, categorySubSubId, InventoryWHM_Id, Qty, Rate, Amount, Quality, Notes " +
        ////                        " FROM         SalesOrderDetail where SalesOrderMasterId='" + ddlRefSellno.SelectedValue + "' and InventoryWHM_Id='" + invwhidfromgrid + "' ";
        ////    SqlCommand cmd000 = new SqlCommand(str000, con);
        ////    SqlDataAdapter adp000 = new SqlDataAdapter(cmd000);
        ////    DataTable dt000 = new DataTable();
        ////    adp000.Fill(dt000);
        ////    if (dt000.Rows.Count > 0)
        ////    {

        ////        subTotal = subTotal + Convert.ToDouble(dr["Total"]);
        ////    }
        ////}
        ////foreach (GridViewRow gdr in GridView1.Rows)
        ////    ////{

        ////    ////    d = d + Convert.ToDecimal(gdr.Cells[6].Text);
        ////    ////}
        double sbt = 0;
        foreach (GridViewRow ghgh in GridView1.Rows)
        {
            TextBox unttn = (TextBox)(ghgh.FindControl("TextBox4"));
            int invwhidfromgrid = Convert.ToInt32(ghgh.Cells[0].Text);//["InventoryWarehouseMasterId"]);
            string str000 = "SELECT     SalesOrderMasterId, categorySubSubId, InventoryWHM_Id, Qty, Rate, Amount, Quality, Notes " +
                                " FROM         SalesOrderDetail where SalesOrderMasterId='" + ddlRefSellno.SelectedValue + "' and InventoryWHM_Id='" + invwhidfromgrid + "' ";
            SqlCommand cmd000 = new SqlCommand(str000, con);
            SqlDataAdapter adp000 = new SqlDataAdapter(cmd000);
            DataTable dt000 = new DataTable();
            adp000.Fill(dt000);
            if (dt000.Rows.Count > 0)
            {
                double rt = Convert.ToDouble(dt000.Rows[0]["Rate"]);
                //double unt = Convert.ToDouble(ghgh.Cells[6].Text);
                double unt = Convert.ToDouble(unttn.Text);
                double subtt = rt * unt;
                sbt = sbt + subtt;
                
            }


        }

        lblSubTotal.Text = String.Format("{0:n}", sbt);
        
    }
  
    protected void btnPrintCustomer_Click(object sender, EventArgs e)
    {
        imgbtnPrintPackingSlip_Click(sender, e);
    }
    //protected void Button3_Click(object sender, EventArgs e)
    //{
    //    EventArgs eee2 = new EventArgs();
    //    Button3_Click(sender, eee2);
       
    //}
   
    protected void txtShipCost_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void checkGnrInvoice_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

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
  

   
    protected void ddlSalesordertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        //fillorder();
        
    }
    protected void textqty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((TextBox)sender).Parent.Parent as GridViewRow;
         int rinrow = row.RowIndex;
                TextBox txtSqty = (TextBox)GridView1.Rows[rinrow].FindControl("TextBox4");
                Label txtQtyDiff = (Label)GridView1.Rows[rinrow].FindControl("TextBox5");
                Label lblqtyonhand = (Label)GridView1.Rows[rinrow].FindControl("lblqtyonhand");
      
                if (txtSqty.Text == "")
                {
                    txtSqty.Text = "0";
                }
                if (Convert.ToDecimal(lblqtyonhand.Text) < Convert.ToDecimal(txtSqty.Text))
                {
                    txtSqty.Text = lblqtyonhand.Text;
                }
                  
                    decimal i = 0;
                    if (Convert.ToString(GridView1.Rows[rinrow].Cells[6].Text) != "0.00")
                    {
                        if (Convert.ToDecimal(txtSqty.Text) > Convert.ToDecimal(GridView1.Rows[rinrow].Cells[6].Text))
                        {
                            txtSqty.Text = GridView1.Rows[rinrow].Cells[6].Text.ToString();
                        }
                        //i = (-1) * (Convert.ToDecimal(GridView1.Rows[rinrow].Cells[6].Text) - Convert.ToDecimal(txtSqty.Text));
                        i =  (Convert.ToDecimal(GridView1.Rows[rinrow].Cells[6].Text) - Convert.ToDecimal(txtSqty.Text));
                     
                            txtQtyDiff.Text = i.ToString();
                            RadioButtonList1.Items.Clear();
                           
                             if (Convert.ToDecimal(txtSqty.Text) < Convert.ToDecimal(GridView1.Rows[rinrow].Cells[6].Text))
                             {
                                 RadioButtonList1.Items.Insert(0, "Order Partially Shipped but considered Complete");
                                 RadioButtonList1.Items[0].Value = "1";
                                 RadioButtonList1.Items.Insert(1, "Partially Shipped and on Backorder");
                                 RadioButtonList1.Items[1].Value = "2";
                                 RadioButtonList1.Items.Insert(2, "Partially Shipped");
                                 RadioButtonList1.Items[2].Value = "3";
                                 RadioButtonList1.SelectedValue = "3";
                             }
                             else if ((Convert.ToDecimal(txtSqty.Text) == Convert.ToDecimal(GridView1.Rows[rinrow].Cells[6].Text)))
                             {
                                 RadioButtonList1.Items.Insert(0, "Fully Shipped");
                                 RadioButtonList1.Items[0].Value = "4";
                                
                             }
                        //FillSaleInvoiceAmts();
                    }
                
               
        
    }
    protected void textqty2_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((TextBox)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        TextBox txtSqty = (TextBox)GridView3.Rows[rinrow].FindControl("TextBox4");
        Label txtQtyDiff = (Label)GridView3.Rows[rinrow].FindControl("TextBox5");
        Label lblqtyonhand = (Label)GridView3.Rows[rinrow].FindControl("lblqtyonhand");

        if (txtSqty.Text == "")
        {
            txtSqty.Text = "0";
        }
      
            decimal i = 0;
            if (Convert.ToString(GridView3.Rows[rinrow].Cells[6].Text) != "0.00")
            {
                if (Convert.ToDecimal(txtSqty.Text) > Convert.ToDecimal(GridView3.Rows[rinrow].Cells[6].Text))
                {
                    txtSqty.Text = GridView3.Rows[rinrow].Cells[6].Text.ToString();
                }
                //i = (-1) * (Convert.ToDecimal(GridView3.Rows[rinrow].Cells[6].Text) - Convert.ToDecimal(txtSqty.Text));
                i =  (Convert.ToDecimal(GridView3.Rows[rinrow].Cells[6].Text) - Convert.ToDecimal(txtSqty.Text));
                txtQtyDiff.Text = i.ToString();
                txtQtyDiff.Text = i.ToString();
                RadioButtonList1.Items.Clear();
               
                if (Convert.ToDecimal(txtSqty.Text) < Convert.ToDecimal(GridView3.Rows[rinrow].Cells[6].Text))
                {
                    RadioButtonList1.Items.Insert(0, "Order Partially Shipped but considered Complete");
                    RadioButtonList1.Items[0].Value = "1";
                    RadioButtonList1.Items.Insert(1, "Partially Shipped and on Backorder");
                    RadioButtonList1.Items[1].Value = "2";
                    RadioButtonList1.Items.Insert(2, "Partially Shipped");
                    RadioButtonList1.Items[2].Value = "3";
                    RadioButtonList1.SelectedValue = "3";
                }
                else if ((Convert.ToDecimal(txtSqty.Text) == Convert.ToDecimal(GridView3.Rows[rinrow].Cells[6].Text)))
                {
                    RadioButtonList1.Items.Insert(0, "Fully Shipped");
                    RadioButtonList1.Items[0].Value = "4";
                    RadioButtonList1.SelectedValue = "4";

                }
            }
        


    }
    public void sendmail(String tn, int kl)
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

            leadinfo.Append("<br><br><table><tr><td>Thank you for your recent order.</td></tr>");
            leadinfo.Append("<tr><td><br></td></tr>");
            string asd = "";

            string asdmsa = "";
            if (kl == 2)
            {
                asd = " packing slip ";
                asdmsa = " Packing Slip ";
            }
            else
            {
                asd = " invoice ";
                asdmsa = " Invoice ";
            }

            leadinfo.Append("<tr><td>Below, please find the " + asd + " with your order details. </td></tr></table><br><br>");

            StringBuilder Invin = new StringBuilder();
            Invin.Append("<table >   <tr><td>" + asdmsa + " Information:</td></tr></table>");

            NameInfo.Append("<table >   <tr><td><b>Customer Name</b></td><td>" + ddlParty.SelectedItem.Text + "</td><td width=\"20%\"></td>");
            NameInfo.Append(" <td><b>Purchase Order </b></td><td>" + lblDate.Text + "</td></tr>");
            DataTable rds = new DataTable();
            if (kl == 2)
            {
                rds = select("select SalesNo from SalesChallanMaster where SalesChallanMasterId='" + ViewState["DcNo1"] + "'");
            }
            else
            {
                rds = select("SELECT  max(EntryNumber) as SalesNo FROM TranctionMaster where Tranction_Master_Id='" + ViewState["tid"] + "'");
            }



            NameInfo.Append(" <td><b>" + asdmsa + " No. </b></td><td>" + rds.Rows[0]["SalesNo"] + "</td></tr>");
            //NameInfo.Append("<tr><td><b>Date </b></td><td>" + lblDate.Text + "</td><td width=\"20%\"></td>");
            //NameInfo.Append("<td><b>Terms </b></td><td>" + txtterms.Text + "</td></tr>");



            NameInfo.Append("</table> <br>");
            if (GridView1.Rows.Count > 0)
            {
                Absemp.Append("<table width=\"100%\" border=\"1\">   <tr>" +
                    "<td style=\"background-color: silver\"><strong>Name</strong> </td> <td style=\"background-color: silver\"><strong>Product No.</strong> </td>");

                Absemp.Append("<td style=\"background-color: silver\"><strong>Weight/Unit</strong> </td>");

                //Absemp.Append("<td style=\"background-color: silver\"><strong>Qty On Hand</strong></td>");

                Absemp.Append("<td style=\"background-color: silver\"><strong>Ordered Qty</strong></td>");

                Absemp.Append("<td style=\"background-color: silver\"><strong>Unshipped Qty</strong></td>");


                Absemp.Append("<td style=\"background-color: silver\"><strong>Qty Shortage</strong></td>");



                Absemp.Append("<td style=\"background-color: silver\"><strong>Note</strong></td>");


                Absemp.Append("</tr>");

                foreach (GridViewRow item in GridView1.Rows)
                {

                    Absemp.Append("<tr><td>" + item.Cells[1].Text + "</td><td>" + item.Cells[2].Text + "</td>");


                    Absemp.Append("<td>" + item.Cells[3].Text + "</td>");

                    Label lblqtyonhand = (Label)item.FindControl("lblqtyonhand");
                    Label lblqty = (Label)item.FindControl("lblqty");
                    TextBox TextBox4 = (TextBox)item.FindControl("TextBox4");
                    Label TextBox5 = (Label)item.FindControl("TextBox5");
                    TextBox TextBox6 = (TextBox)item.FindControl("TextBox6");
                    //Absemp.Append("<td>" + lblqtyonhand.Text + "</td>");
                    Absemp.Append("<td>" + lblqty.Text + "</td>");
                    Absemp.Append("<td >" + TextBox4.Text + "</td>");


                    Absemp.Append("<td >" + TextBox5.Text + "</td>");

                    Absemp.Append("<td>" + TextBox6.Text + "</td>");



                    Absemp.Append("</tr>");

                }

                Absemp.Append("</table> ");
            }
            StringBuilder stserv = new StringBuilder();

            if (GridView3.Rows.Count > 0)
            {
                stserv.Append("<table width=\"100%\" border=\"1\">   <tr>" +
                    "<td style=\"background-color: silver\"><strong>Name</strong> </td> <td style=\"background-color: silver\"><strong>Sevices No.</strong> </td>");





                stserv.Append("<td style=\"background-color: silver\"><strong>Number of Services/Hours Ordered</strong></td>");

                stserv.Append("<td style=\"background-color: silver\"><strong>Number of Services/Hours Yet to be Provided</strong></td>");


                stserv.Append("<td style=\"background-color: silver\"><strong>Number of Services/Hours Provided Shortage</strong></td>");



                stserv.Append("<td style=\"background-color: silver\"><strong>Note</strong></td>");


                stserv.Append("</tr>");

                foreach (GridViewRow item in GridView3.Rows)
                {

                    stserv.Append("<tr><td>" + item.Cells[1].Text + "</td><td>" + item.Cells[2].Text + "</td>");


                    Label lblqty = (Label)item.FindControl("lblqty");
                    TextBox TextBox4 = (TextBox)item.FindControl("TextBox4");
                    Label TextBox5 = (Label)item.FindControl("TextBox5");
                    TextBox TextBox6 = (TextBox)item.FindControl("TextBox6");

                    stserv.Append("<td>" + lblqty.Text + "</td>");
                    stserv.Append("<td >" + TextBox4.Text + "</td>");


                    stserv.Append("<td >" + TextBox5.Text + "</td>");

                    stserv.Append("<td>" + TextBox6.Text + "</td>");



                    stserv.Append("</tr>");

                }

                stserv.Append("</table> ");
            }

            Nametot.Append("<br><br><table >   <tr><td><b>Gross Total: </b></td><td>" + lblSubTotalSI.Text + "</td></tr>");
            Nametot.Append(" <tr><td><b>Total Customer Discount: </b></td><td>" + lblCustDisSI.Text + "</td></tr>");
            Nametot.Append("<tr><td><b>Order Value Discount: </b></td><td>" + lblValueDisSI.Text + "</td></tr>");

            Nametot.Append("<tr><td><b>Total Tax: </b></td><td>" + lblTaxSI.Text + "</td></tr>");
            Nametot.Append("<tr><td><b>Total Amount: </b></td><td>" + lblTotalSI.Text + "</td></tr>");
            Nametot.Append("<td></td><td></td></tr>");
            Nametot.Append("</table> ");
            string mail = "";

            StringBuilder msdd = new StringBuilder();
            msdd.Append("<br><br><table><tr><td>If you have any questions regarding this " + asd.Replace(" ", "") + ", please contact us.</td></tr></table>");
            StringBuilder thank = new StringBuilder();
            thank.Append("<br><br><table><tr><td>Thank you for your business and have a great day.</td></tr></table><br><br>");


            DataTable dtpart = select("SELECT Email FROM  Party_master WHERE     (PartyId = " + Convert.ToInt32(ddlParty.SelectedValue) + ") ");
            if (dtpart.Rows.Count > 0)
            {
                mail = Convert.ToString(dtpart.Rows[0]["Email"]);
            }

            DataTable dtma = select("SELECT OutGoingMailServer,WebMasterEmail,MasterEmailId, EmailMasterLoginPassword, AdminEmail, WHId " +
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

                DataTable sqldb = select(" select Email,ContactNo1,CompanyMaster.CompanyName from CompanyMaster inner join CompanyAddressMaster on CompanyAddressMaster.CompanyMasterId=CompanyMaster.CompanyId where Compid='" + Session["Comid"] + "'");
                empmanag.Append("<table><tr><td>Sincerely,</td></tr><tr><td>" + sqldb.Rows[0]["CompanyName"] + "</td></tr><tr><td>" + sqldb.Rows[0]["ContactNo1"] + "</td></tr><tr><td>" + sqldb.Rows[0]["Email"] + "</td></tr></table>");



                body = strAddress.ToString() + Dearinfo + leadinfo + Invin + NameInfo.ToString() + Absemp.ToString() + Nametot.ToString() + msdd + thank + empmanag;
                objEmail.Subject = tn;
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
    protected void btprintpak_Click(object sender, EventArgs e)
    {
        btprintpak.Enabled = false;
        ModalPopupExtender2.Show();
        sendmail(" Packing slip ", 2);
        btprintpak.Text = "Packing Slip Email Sent";
    }
    protected void btnprintcreditinv_Click(object sender, EventArgs e)
    {
        btnprintcreditinv.Enabled = false;
        ModalPopupExtender2.Show();
        sendmail(" Invoice ", 1);
        btnprintcreditinv.Text = "Invoice Email Sent";
    }
}
