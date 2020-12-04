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
/// Summary description for InventoryCls
/// </summary>
public class InventoryCls
{
    SqlCommand cmd;
    //SqlDataReader rdr;
    //SqlParameter param;
    //InventoryCls clsDatabase   ; 
    //DataSet ds;
    DataTable dt;


	public InventoryCls()
	{
		//
		// TODO: Add constructor logic here
		//
	}





    // ---------------haiyal 17 - 02 - 2009 ---------------//
    
    
    
    public DataTable SelectInventoryCategoryMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryCategoryMaster";
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertInventoryCategoryMaster(String InvCategoryName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertInventoryCategoryMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvCategoryName", SqlDbType.NVarChar));
        cmd.Parameters["@InvCategoryName"].Value = InvCategoryName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        return result;
    }
    public bool UpdateInventoryCategoryMaster(Int32 InvCategoryId, String InvCategoryName)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateInventoryCategoryMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvCategoryId", SqlDbType.Int));
        cmd.Parameters["@InvCategoryId"].Value = InvCategoryId;
        cmd.Parameters.Add(new SqlParameter("@InvCategoryName", SqlDbType.NVarChar));
        cmd.Parameters["@InvCategoryName"].Value = InvCategoryName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);
    }

    public DataTable SelectInventorySubCategoryMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventorySubCategoryMaster";
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertInventorySubCategoryMaster(Int32 InvCategoryId, String InvSubCategoryName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertInventorySubCategoryMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@InvCategoryId", SqlDbType.Int));
        cmd.Parameters["@InvCategoryId"].Value = InvCategoryId;

        cmd.Parameters.Add(new SqlParameter("@InvSubCategoryName", SqlDbType.NVarChar));
        cmd.Parameters["@InvSubCategoryName"].Value = InvSubCategoryName;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        return result;
    }
    public DataTable SelectInventorySubCategoryMasterWithCategoryMaster(Int32 InvCategoryId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventorySubCategoryMasterWithCategoryMaster";
        cmd.Parameters.Add(new SqlParameter("@InvCategoryId", SqlDbType.Int));
        cmd.Parameters["@InvCategoryId"].Value = InvCategoryId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateInventorySubCategoryMaster(Int32 InvSubCategoryId, Int32 InvCategoryId, String InvSubCategoryName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateInventorySubCategoryMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvSubCategoryId", SqlDbType.Int));
        cmd.Parameters["@InvSubCategoryId"].Value = InvSubCategoryId;
        cmd.Parameters.Add(new SqlParameter("@InvCategoryId", SqlDbType.Int));
        cmd.Parameters["@InvCategoryId"].Value = InvCategoryId;
        cmd.Parameters.Add(new SqlParameter("@InvSubCategoryName", SqlDbType.NVarChar));
        cmd.Parameters["@InvSubCategoryName"].Value = InvSubCategoryName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);
    }



    public DataTable SelectInventorySubSubCategoryMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventorySubSubCategoryMaster";
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertInventorySubSubCategoryMaster(Int32 InvSubCategoryId, String InvSubSubCategoryName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertInventorySubSubCategoryMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@InvSubCategoryId", SqlDbType.Int));
        cmd.Parameters["@InvSubCategoryId"].Value = InvSubCategoryId;

        cmd.Parameters.Add(new SqlParameter("@InvSubSubCategoryName", SqlDbType.NVarChar));
        cmd.Parameters["@InvSubSubCategoryName"].Value = InvSubSubCategoryName;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        return result;
    }
    public DataTable SelectInventorySubSubCategoryMasterWithSubCategory(Int32 InvSubCategoryId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventorySubSubCategoryMasterWithSubCategory";
        cmd.Parameters.Add(new SqlParameter("@InvSubCategoryId", SqlDbType.Int));
        cmd.Parameters["@InvSubCategoryId"].Value = InvSubCategoryId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateInventorySubSubCategoryMaster(Int32 InvSubSubCategoryId, Int32 InvSubCategoryId, String InvSubSubCategoryName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateInventorySubSubCategoryMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvSubSubCategoryId", SqlDbType.Int));
        cmd.Parameters["@InvSubSubCategoryId"].Value = InvSubSubCategoryId;
        cmd.Parameters.Add(new SqlParameter("@InvSubCategoryId", SqlDbType.Int));
        cmd.Parameters["@InvSubCategoryId"].Value = InvSubCategoryId;
        cmd.Parameters.Add(new SqlParameter("@InvSubSubCategoryName", SqlDbType.NVarChar));
        cmd.Parameters["@InvSubSubCategoryName"].Value = InvSubSubCategoryName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);
    }

    public DataTable SelectInventoryTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryTypeMaster";
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertInventoryTypeMaster(String InvTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertInventoryTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@InvTypeName"].Value = InvTypeName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        return result;
    }
    public bool UpdateInventoryTypeMaster(Int32 InvTypeId, String InvTypeName)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateInventoryTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvTypeId", SqlDbType.Int));
        cmd.Parameters["@InvTypeId"].Value = InvTypeId;
        cmd.Parameters.Add(new SqlParameter("@InvTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@InvTypeName"].Value = InvTypeName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);
    }


    public DataTable SelectInventoryUnitTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryUnitTypeMaster";
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertInventoryUnitTypeMaster(String InvUnitTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertInventoryUnitTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvUnitTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@InvUnitTypeName"].Value = InvUnitTypeName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        return result;
    }
    public bool UpdateInventoryUnitTypeMaster(Int32 InvUnitTypeId, String InvUnitTypeName)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateInventoryUnitTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvUnitTypeId", SqlDbType.Int));
        cmd.Parameters["@InvUnitTypeId"].Value = InvUnitTypeId;
        cmd.Parameters.Add(new SqlParameter("@InvUnitTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@InvUnitTypeName"].Value = InvUnitTypeName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);
    }

    public DataTable SelectInventorySiteMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventorySiteMaster";
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertInventorySiteMaster(String InvSiteName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertInventorySiteMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvSiteName", SqlDbType.NVarChar));
        cmd.Parameters["@InvSiteName"].Value = InvSiteName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        return result;
    }
    public bool UpdateInventorySiteMaster(Int32 InvSiteId, String InvSiteName)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateInventorySiteMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvSiteId", SqlDbType.Int));
        cmd.Parameters["@InvSiteId"].Value = InvSiteId;
        cmd.Parameters.Add(new SqlParameter("@InvSiteName", SqlDbType.NVarChar));
        cmd.Parameters["@InvSiteName"].Value = InvSiteName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);
    }



    public DataTable SelectInventoryRoomMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryRoomMaster";
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertInventoryRoomMaster(Int32 InvSiteId, String InvRoomName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertInventoryRoomMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@InvSiteId", SqlDbType.Int));
        cmd.Parameters["@InvSiteId"].Value = InvSiteId;

        cmd.Parameters.Add(new SqlParameter("@InvRoomName", SqlDbType.NVarChar));
        cmd.Parameters["@InvRoomName"].Value = InvRoomName;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        return result;
    }
    public DataTable SelectInventoryRoomMasterWithSite(Int32 InvSiteId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryRoomMasterWithSite";
        cmd.Parameters.Add(new SqlParameter("@InvSiteId", SqlDbType.Int));
        cmd.Parameters["@InvSiteId"].Value = InvSiteId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateInventoryRoomMaster(Int32 InvRoomId, Int32 InvSiteId, String InvRoomName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateInventoryRoomMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvRoomId", SqlDbType.Int));
        cmd.Parameters["@InvRoomId"].Value = InvRoomId;
        cmd.Parameters.Add(new SqlParameter("@InvSiteId ", SqlDbType.Int));
        cmd.Parameters["@InvSiteId "].Value = InvSiteId;
        cmd.Parameters.Add(new SqlParameter("@InvRoomName", SqlDbType.NVarChar));
        cmd.Parameters["@InvRoomName"].Value = InvRoomName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);
    }


    public DataTable SelectInventoryRackMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryRackMaster";
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertInventoryRackMaster(Int32 InvRoomId, String InvRackName, Decimal NumberofShelves, Decimal NumberofPositiononShelf, String SizeofRack)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertInventoryRackMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvRoomId", SqlDbType.Int));
        cmd.Parameters["@InvRoomId"].Value = InvRoomId;
        cmd.Parameters.Add(new SqlParameter("@InvRackName", SqlDbType.NVarChar));
        cmd.Parameters["@InvRackName"].Value = InvRackName;
        cmd.Parameters.Add(new SqlParameter("@NumberofShelves", SqlDbType.Decimal));
        cmd.Parameters["@NumberofShelves"].Value = NumberofShelves;
        cmd.Parameters.Add(new SqlParameter("@NumberofPositiononShelf", SqlDbType.Decimal));
        cmd.Parameters["@NumberofPositiononShelf"].Value = NumberofPositiononShelf;
        cmd.Parameters.Add(new SqlParameter("@SizeofRack", SqlDbType.NVarChar));
        cmd.Parameters["@SizeofRack"].Value = SizeofRack;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        return result;
    }
    public DataTable SelectInventoryRackMasterWithRoom(Int32 InvRoomId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryRackMasterWithRoom";
        cmd.Parameters.Add(new SqlParameter("@InvRoomId", SqlDbType.Int));
        cmd.Parameters["@InvRoomId"].Value = InvRoomId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateInventoryRackMaster(Int32 InvRackId, Int32 InvRoomId, String InvRackName, Decimal NumberofShelves, Decimal NumberofPositiononShelf, String SizeofRack)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateInventoryRackMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvRackId", SqlDbType.Int));
        cmd.Parameters["@InvRackId"].Value = InvRackId;
        cmd.Parameters.Add(new SqlParameter("@InvRoomId", SqlDbType.Int));
        cmd.Parameters["@InvRoomId"].Value = InvRoomId;
        cmd.Parameters.Add(new SqlParameter("@InvRackName", SqlDbType.NVarChar));
        cmd.Parameters["@InvRackName"].Value = InvRackName;
        cmd.Parameters.Add(new SqlParameter("@NumberofShelves", SqlDbType.Decimal));
        cmd.Parameters["@NumberofShelves"].Value = NumberofShelves;
        cmd.Parameters.Add(new SqlParameter("@NumberofPositiononShelf", SqlDbType.Decimal));
        cmd.Parameters["@NumberofPositiononShelf"].Value = NumberofPositiononShelf;
        cmd.Parameters.Add(new SqlParameter("@SizeofRack", SqlDbType.NVarChar));
        cmd.Parameters["@SizeofRack"].Value = SizeofRack;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);
    }


    public DataTable SelectQuantityTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectQuantityTypeMaster";
        dt = DatabaseCls.FillAdapter (cmd);
        return dt;
    }

    public Int32 InsertInventoryMaster(Int32 InvTypeId, Int32 InvSubSubId, Int32 InvQuantityTypeId, String InvName, DateTime InvDate, String ProductNo, String Description, Decimal Rate, String Barcode, Int64 Qty, Int64 ReorderLevel, Int64 ReorderQty, Int64 NormalOrderQty, Int64 QtyonDateStarted, Decimal WeightinLBS, Decimal Unit, Int32 InvUnitTypeId, Boolean Active, Boolean NewArrival, Boolean Promotion, Boolean FeatureProduct, Decimal Height, Decimal Width, Decimal Length, Decimal Volume, String SmallImage, String LargeImage)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertInventoryMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@InvTypeId", SqlDbType.Int));
        cmd.Parameters["@InvTypeId"].Value = InvTypeId;
        cmd.Parameters.Add(new SqlParameter("@InvSubSubId", SqlDbType.Int));
        cmd.Parameters["@InvSubSubId"].Value = InvSubSubId;
        cmd.Parameters.Add(new SqlParameter("@InvQuantityTypeId", SqlDbType.Int));
        cmd.Parameters["@InvQuantityTypeId"].Value = InvQuantityTypeId;

        cmd.Parameters.Add(new SqlParameter("@InvName", SqlDbType.NVarChar));
        cmd.Parameters["@InvName"].Value = InvName;

        cmd.Parameters.Add(new SqlParameter("@InvDate", SqlDbType.DateTime));
        cmd.Parameters["@InvDate"].Value = InvDate;

        cmd.Parameters.Add(new SqlParameter("@ProductNo", SqlDbType.NVarChar));
        cmd.Parameters["@ProductNo"].Value = ProductNo;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;

        cmd.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Decimal));
        cmd.Parameters["@Rate"].Value = Rate;

        cmd.Parameters.Add(new SqlParameter("@Barcode", SqlDbType.NVarChar));
        cmd.Parameters["@Barcode"].Value = Barcode;

        cmd.Parameters.Add(new SqlParameter("@Qty", SqlDbType.Int));
        cmd.Parameters["@Qty"].Value = Qty;
        cmd.Parameters.Add(new SqlParameter("@ReorderLevel", SqlDbType.Int));
        cmd.Parameters["@ReorderLevel"].Value = ReorderLevel;
        cmd.Parameters.Add(new SqlParameter("@ReorderQty", SqlDbType.Int));
        cmd.Parameters["@ReorderQty"].Value = ReorderQty;
        cmd.Parameters.Add(new SqlParameter("@NormalOrderQty", SqlDbType.Int));
        cmd.Parameters["@NormalOrderQty"].Value = NormalOrderQty;
        cmd.Parameters.Add(new SqlParameter("@QtyonDateStarted", SqlDbType.Int));
        cmd.Parameters["@QtyonDateStarted"].Value = QtyonDateStarted;

        cmd.Parameters.Add(new SqlParameter("@WeightinLBS", SqlDbType.Decimal));
        cmd.Parameters["@WeightinLBS"].Value = WeightinLBS;
        cmd.Parameters.Add(new SqlParameter("@Unit", SqlDbType.Decimal));
        cmd.Parameters["@Unit"].Value = Unit;

        cmd.Parameters.Add(new SqlParameter("@InvUnitTypeId", SqlDbType.Int));
        cmd.Parameters["@InvUnitTypeId"].Value = InvUnitTypeId;

        cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit));
        cmd.Parameters["@Active"].Value = Active;
        cmd.Parameters.Add(new SqlParameter("@NewArrival", SqlDbType.Bit));
        cmd.Parameters["@NewArrival"].Value = NewArrival;
        cmd.Parameters.Add(new SqlParameter("@Promotion", SqlDbType.Bit));
        cmd.Parameters["@Promotion"].Value = Promotion;
        cmd.Parameters.Add(new SqlParameter("@FeatureProduct", SqlDbType.Bit));
        cmd.Parameters["@FeatureProduct"].Value = FeatureProduct;

        cmd.Parameters.Add(new SqlParameter("@Height", SqlDbType.Decimal));
        cmd.Parameters["@Height"].Value = Height;
        cmd.Parameters.Add(new SqlParameter("@Width", SqlDbType.Decimal));
        cmd.Parameters["@Width"].Value = Width;
        cmd.Parameters.Add(new SqlParameter("@Length", SqlDbType.Decimal));
        cmd.Parameters["@Length"].Value = Length;
        cmd.Parameters.Add(new SqlParameter("@Volume", SqlDbType.Decimal));
        cmd.Parameters["@Volume"].Value = Volume;

        cmd.Parameters.Add(new SqlParameter("@SmallImage", SqlDbType.NVarChar));
        cmd.Parameters["@SmallImage"].Value = SmallImage;
        cmd.Parameters.Add(new SqlParameter("@LargeImage", SqlDbType.NVarChar));
        cmd.Parameters["@LargeImage"].Value = LargeImage;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        return result;
    }

    public DataTable SelectInventoryMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryMaster";
        dt = DatabaseCls.FillAdapter (cmd);
        return dt;
    }




    public bool UpdateInventoryMaster(Int32 InvId, Int32 InvTypeId, Int32 InvSubSubId, Int32 InvQuantityTypeId, String InvName, DateTime InvDate, String ProductNo, String Description, Decimal Rate, String Barcode, Int64 Qty, Int64 ReorderLevel, Int64 ReorderQty, Int64 NormalOrderQty, Int64 QtyonDateStarted, Decimal WeightinLBS, Decimal Unit, Int32 InvUnitTypeId, Boolean Active, Boolean NewArrival, Boolean Promotion, Boolean FeatureProduct, Decimal Height, Decimal Width, Decimal Length, Decimal Volume, String SmallImage, String LargeImage)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateInventoryMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InvId", SqlDbType.Int));
        cmd.Parameters["@InvId"].Value = InvId;

        cmd.Parameters.Add(new SqlParameter("@InvTypeId", SqlDbType.Int));
        cmd.Parameters["@InvTypeId"].Value = InvTypeId;
        cmd.Parameters.Add(new SqlParameter("@InvSubSubId", SqlDbType.Int));
        cmd.Parameters["@InvSubSubId"].Value = InvSubSubId;
        cmd.Parameters.Add(new SqlParameter("@InvQuantityTypeId", SqlDbType.Int));
        cmd.Parameters["@InvQuantityTypeId"].Value = InvQuantityTypeId;

        cmd.Parameters.Add(new SqlParameter("@InvName", SqlDbType.NVarChar));
        cmd.Parameters["@InvName"].Value = InvName;

        cmd.Parameters.Add(new SqlParameter("@InvDate", SqlDbType.DateTime));
        cmd.Parameters["@InvDate"].Value = InvDate;

        cmd.Parameters.Add(new SqlParameter("@ProductNo", SqlDbType.NVarChar));
        cmd.Parameters["@ProductNo"].Value = ProductNo;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;

        cmd.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Decimal));
        cmd.Parameters["@Rate"].Value = Rate;

        cmd.Parameters.Add(new SqlParameter("@Barcode", SqlDbType.NVarChar));
        cmd.Parameters["@Barcode"].Value = Barcode;

        cmd.Parameters.Add(new SqlParameter("@Qty", SqlDbType.Int));
        cmd.Parameters["@Qty"].Value = Qty;
        cmd.Parameters.Add(new SqlParameter("@ReorderLevel", SqlDbType.Int));
        cmd.Parameters["@ReorderLevel"].Value = ReorderLevel;
        cmd.Parameters.Add(new SqlParameter("@ReorderQty", SqlDbType.Int));
        cmd.Parameters["@ReorderQty"].Value = ReorderQty;
        cmd.Parameters.Add(new SqlParameter("@NormalOrderQty", SqlDbType.Int));
        cmd.Parameters["@NormalOrderQty"].Value = NormalOrderQty;
        cmd.Parameters.Add(new SqlParameter("@QtyonDateStarted", SqlDbType.Int));
        cmd.Parameters["@QtyonDateStarted"].Value = QtyonDateStarted;

        cmd.Parameters.Add(new SqlParameter("@WeightinLBS", SqlDbType.Decimal));
        cmd.Parameters["@WeightinLBS"].Value = WeightinLBS;
        cmd.Parameters.Add(new SqlParameter("@Unit", SqlDbType.Decimal));
        cmd.Parameters["@Unit"].Value = Unit;

        cmd.Parameters.Add(new SqlParameter("@InvUnitTypeId", SqlDbType.Int));
        cmd.Parameters["@InvUnitTypeId"].Value = InvUnitTypeId;

        cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit));
        cmd.Parameters["@Active"].Value = Active;
        cmd.Parameters.Add(new SqlParameter("@NewArrival", SqlDbType.Bit));
        cmd.Parameters["@NewArrival"].Value = NewArrival;
        cmd.Parameters.Add(new SqlParameter("@Promotion", SqlDbType.Bit));
        cmd.Parameters["@Promotion"].Value = Promotion;
        cmd.Parameters.Add(new SqlParameter("@FeatureProduct", SqlDbType.Bit));
        cmd.Parameters["@FeatureProduct"].Value = FeatureProduct;

        cmd.Parameters.Add(new SqlParameter("@Height", SqlDbType.Decimal));
        cmd.Parameters["@Height"].Value = Height;
        cmd.Parameters.Add(new SqlParameter("@Width", SqlDbType.Decimal));
        cmd.Parameters["@Width"].Value = Width;
        cmd.Parameters.Add(new SqlParameter("@Length", SqlDbType.Decimal));
        cmd.Parameters["@Length"].Value = Length;
        cmd.Parameters.Add(new SqlParameter("@Volume", SqlDbType.Decimal));
        cmd.Parameters["@Volume"].Value = Volume;

        cmd.Parameters.Add(new SqlParameter("@SmallImage", SqlDbType.NVarChar));
        cmd.Parameters["@SmallImage"].Value = SmallImage;
        cmd.Parameters.Add(new SqlParameter("@LargeImage", SqlDbType.NVarChar));
        cmd.Parameters["@LargeImage"].Value = LargeImage;


        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);

    }
    public DataTable SelectInventoryMasterInNewPage(Int32 InvId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryMasterInNewPage";
        cmd.Parameters.Add(new SqlParameter("@InvId", SqlDbType.Int));
        cmd.Parameters["@InvId"].Value = InvId;
        dt = DatabaseCls.FillAdapter (cmd);
        return dt;
    }
     /// 9-3-09
     /// 


    public DataTable SelectInventoryCategoryforCombo()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryCategoryforCombo";
         
        dt = DatabaseCls.FillAdapter (cmd);
        return dt;
    }
    public DataTable SelectInventoryMasterSubCWise(Int32 InvSubSubCategoryId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryMasterSubCWise";
        cmd.Parameters.Add(new SqlParameter("@InvSubSubCategoryId", SqlDbType.Int));
        cmd.Parameters["@InvSubSubCategoryId"].Value = InvSubSubCategoryId;

        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectInventoryMasterIDWise(Int32 InvId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInventoryMasterIDWise";
        cmd.Parameters.Add(new SqlParameter("@InvId", SqlDbType.Int));
        cmd.Parameters["@InvId"].Value =  InvId;

        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }

}
