using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;
/// <summary>
/// Summary description for CustomerSaleCls
/// </summary>
public class CustomerSaleCls
{
    SqlCommand cmd;    
    DataTable dt;
	public CustomerSaleCls()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable SelectShippperMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectShipperMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectSalesOrderMasterTopId()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectSalesOrderMasterTopId";
        dt = DatabaseCls1.ExecuteScalar(cmd); // DatabaseCls1.ExecuteNonQuerywithReturn(cmd);
        return dt;
    }
    public Int32 InsertSalesOrderMaster(String SalesOrderNo, Int32 SalesManId, Int32 PartyId, DateTime SalesOrderDate, String BuyersPOno,
        Int32 ShippersId, DateTime ExpextedDeliveryDate, String DeliveryTerms, String PaymentsTerms, String OtherTerms, 
        String ShippingCharges, String HandlingCharges, String OtherCharges, String Discounts, String GrossAmount , Int32 ShippingAddressId , Int32 BillingAddressId ,String SODocument)
    {
           
        
        cmd = new SqlCommand();
        cmd.CommandText = "InsertSalesOrderMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@SalesOrderNo", SqlDbType.Int));
        cmd.Parameters["@SalesOrderNo"].Value = SalesOrderNo;
        cmd.Parameters.Add(new SqlParameter("@SalesManId", SqlDbType.Int));
        cmd.Parameters["@SalesManId"].Value = SalesManId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;

        cmd.Parameters.Add(new SqlParameter("@SalesOrderDate", SqlDbType.DateTime  ));
        cmd.Parameters["@SalesOrderDate"].Value = SalesOrderDate;

        cmd.Parameters.Add(new SqlParameter("@BuyersPOno", SqlDbType.NVarChar ));
        cmd.Parameters["@BuyersPOno"].Value = BuyersPOno;

        cmd.Parameters.Add(new SqlParameter("@ShippersId", SqlDbType.Int ));
        cmd.Parameters["@ShippersId"].Value = ShippersId;
        cmd.Parameters.Add(new SqlParameter("@ExpextedDeliveryDate", SqlDbType.DateTime ));
        cmd.Parameters["@ExpextedDeliveryDate"].Value = ExpextedDeliveryDate;
        cmd.Parameters.Add(new SqlParameter("@DeliveryTerms", SqlDbType.NVarChar));
        cmd.Parameters["@DeliveryTerms"].Value = DeliveryTerms;
        cmd.Parameters.Add(new SqlParameter("@PaymentsTerms", SqlDbType.NVarChar));
        cmd.Parameters["@PaymentsTerms"].Value = PaymentsTerms;
        cmd.Parameters.Add(new SqlParameter("@OtherTerms", SqlDbType.NVarChar));
        cmd.Parameters["@OtherTerms"].Value = OtherTerms;

        cmd.Parameters.Add(new SqlParameter("@ShippingCharges", SqlDbType.NVarChar));
        cmd.Parameters["@ShippingCharges"].Value = ShippingCharges;

        cmd.Parameters.Add(new SqlParameter("@HandlingCharges", SqlDbType.NVarChar));
        cmd.Parameters["@HandlingCharges"].Value = HandlingCharges;

        cmd.Parameters.Add(new SqlParameter("@OtherCharges", SqlDbType.NVarChar));
        cmd.Parameters["@OtherCharges"].Value = OtherCharges;


        cmd.Parameters.Add(new SqlParameter("@Discounts", SqlDbType.NVarChar ));
        cmd.Parameters["@Discounts"].Value = Discounts;

        cmd.Parameters.Add(new SqlParameter("@GrossAmount", SqlDbType.NVarChar));
        cmd.Parameters["@GrossAmount"].Value = GrossAmount;
        cmd.Parameters.Add(new SqlParameter("@BillingAddressId", SqlDbType.Int));
        cmd.Parameters["@BillingAddressId"].Value = BillingAddressId;

        cmd.Parameters.Add(new SqlParameter("@ShippingAddressId", SqlDbType.Int));
        cmd.Parameters["@ShippingAddressId"].Value = ShippingAddressId;

        cmd.Parameters.Add(new SqlParameter("@SODocument", SqlDbType.NVarChar));
        cmd.Parameters["@SODocument"].Value = SODocument;
        cmd.Parameters.Add(new SqlParameter("@SalesOrderId", SqlDbType.Int));
        cmd.Parameters["@SalesOrderId"].Direction = ParameterDirection.Output;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result =Convert.ToInt32( cmd.Parameters["@SalesOrderId"].Value );
        return result;
    }
    

      
            
    public Int32 InsertSalesOrderTaxDetail(Int32 SalesOrderId, Int32 TaxId ,  String Amount)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertSalesOrderTaxDetail";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@SalesOrderId", SqlDbType.Int));
        cmd.Parameters["@SalesOrderId"].Value = SalesOrderId;
            cmd.Parameters.Add(new SqlParameter("@TaxId", SqlDbType.Int));
        cmd.Parameters["@TaxId"].Value = TaxId;
        cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.NVarChar));
        cmd.Parameters["@Amount"].Value = Amount;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertSalesOrderDetail( Int32 SalesOrderId,Int32 InvId,String  CustomerPrice,String ChargedPrice, String SalesPrice,
        String  Qty,  String Amount, String  Quality, String Notes)
    {     
        cmd = new SqlCommand();
        cmd.CommandText = "InsertSalesOrderDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@SalesOrderId", SqlDbType.Int));
        cmd.Parameters["@SalesOrderId"].Value = SalesOrderId;
        cmd.Parameters.Add(new SqlParameter("@InvId", SqlDbType.Int));
        cmd.Parameters["@InvId"].Value = InvId;
        cmd.Parameters.Add(new SqlParameter("@CustomerPrice", SqlDbType.NVarChar));
        cmd.Parameters["@CustomerPrice"].Value = CustomerPrice;
        cmd.Parameters.Add(new SqlParameter("@SalesPrice", SqlDbType.NVarChar));
        cmd.Parameters["@SalesPrice"].Value = SalesPrice;
        cmd.Parameters.Add(new SqlParameter("@ChargedPrice", SqlDbType.NVarChar));
        cmd.Parameters["@ChargedPrice"].Value = ChargedPrice;
        
        cmd.Parameters.Add(new SqlParameter("@Qty", SqlDbType.NVarChar));
        cmd.Parameters["@Qty"].Value = Qty;
        //cmd.Parameters.Add(new SqlParameter("@Rate", SqlDbType.NVarChar));
        //cmd.Parameters["@Rate"].Value = Rate;
        cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.NVarChar));
        cmd.Parameters["@Amount"].Value = Amount;
        cmd.Parameters.Add(new SqlParameter("@Quality", SqlDbType.NVarChar));
        cmd.Parameters["@Quality"].Value = Quality;
        cmd.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
        cmd.Parameters["@Notes"].Value = Notes;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }

    public DataTable SelectSalesOrderMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectSalesOrderMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    ///////-------------- haiyal 10-3-2009------------------------
    public DataTable SelectWarehouseMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectWarehouseMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectWarehouseMasterWithID(Int32 WarehouseId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectWarehouseMasterWithID";
        cmd.Parameters.Add(new SqlParameter("@WarehouseId", SqlDbType.Int));
        cmd.Parameters["@WarehouseId"].Value = WarehouseId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public Int32 InsertCustomerRetailSaleMaster(Int32 WarehouseId, Int32 ShippersId, String PartyName, String PartyAddress, String BillTo, String ShipTo, Boolean ChkdlvryChallan)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertCustomerRetailSaleMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@WarehouseId", SqlDbType.Int));
        cmd.Parameters["@WarehouseId"].Value = WarehouseId;
        cmd.Parameters.Add(new SqlParameter("@ShippersId", SqlDbType.Int));
        cmd.Parameters["@ShippersId"].Value = ShippersId;

        cmd.Parameters.Add(new SqlParameter("@PartyName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyName"].Value = PartyName;
        cmd.Parameters.Add(new SqlParameter("@PartyAddress", SqlDbType.NVarChar));
        cmd.Parameters["@PartyAddress"].Value = PartyAddress;
        cmd.Parameters.Add(new SqlParameter("@BillTo", SqlDbType.NVarChar));
        cmd.Parameters["@BillTo"].Value = BillTo;

        cmd.Parameters.Add(new SqlParameter("@ShipTo", SqlDbType.NVarChar));
        cmd.Parameters["@ShipTo"].Value = ShipTo;
        cmd.Parameters.Add(new SqlParameter("@ChkdlvryChallan", SqlDbType.Bit));
        cmd.Parameters["@ChkdlvryChallan"].Value = ChkdlvryChallan;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(new SqlParameter("@CustomerRetailSaleId", SqlDbType.Int));
        cmd.Parameters["@CustomerRetailSaleId"].Direction = ParameterDirection.Output;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@CustomerRetailSaleId"].Value.ToString());
        return result;
    }
    public Int32 InsertCustomerRetailSaleItemDetail(Int32 CustomerRetailSaleId, Int32 InvId, String SalesPrice, String ChargedPrice, String Amount, String Notes, String Qty, String CustomerPrice, String CustomerQuotedPrice, String Quality)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertCustomerRetailSaleItemDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CustomerRetailSaleId", SqlDbType.Int));
        cmd.Parameters["@CustomerRetailSaleId"].Value = CustomerRetailSaleId;
        cmd.Parameters.Add(new SqlParameter("@InvId", SqlDbType.Int));
        cmd.Parameters["@InvId"].Value = InvId;

        cmd.Parameters.Add(new SqlParameter("@SalesPrice", SqlDbType.NVarChar));
        cmd.Parameters["@SalesPrice"].Value = SalesPrice;
        cmd.Parameters.Add(new SqlParameter("@ChargedPrice", SqlDbType.NVarChar));
        cmd.Parameters["@ChargedPrice"].Value = ChargedPrice;
        cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.NVarChar));
        cmd.Parameters["@Amount"].Value = Amount;
        cmd.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
        cmd.Parameters["@Notes"].Value = Notes;
        cmd.Parameters.Add(new SqlParameter("@Qty", SqlDbType.NVarChar));
        cmd.Parameters["@Qty"].Value = Qty;
        cmd.Parameters.Add(new SqlParameter("@CustomerPrice", SqlDbType.NVarChar));
        cmd.Parameters["@CustomerPrice"].Value = CustomerPrice;
        cmd.Parameters.Add(new SqlParameter("@CustomerQuotedPrice", SqlDbType.NVarChar));
        cmd.Parameters["@CustomerQuotedPrice"].Value = CustomerQuotedPrice;
        cmd.Parameters.Add(new SqlParameter("@Quality", SqlDbType.NVarChar));
        cmd.Parameters["@Quality"].Value = Quality;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }


    ///---------------------haiyal 12-3-2009------------------
    ///


    public DataTable SelectInvIdforRetailSale(String InvSubSubCategoryName, String InvName)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInvIdforRetailSale";

        cmd.Parameters.Add(new SqlParameter("@InvSubSubCategoryName", SqlDbType.NVarChar));
        cmd.Parameters["@InvSubSubCategoryName"].Value = InvSubSubCategoryName;

        cmd.Parameters.Add(new SqlParameter("@InvName", SqlDbType.NVarChar));
        cmd.Parameters["@InvName"].Value = InvName;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }



}
