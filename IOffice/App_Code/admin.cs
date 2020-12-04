using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Summary description for admin
/// </summary>
public class admin
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
      SqlCommand cmd;
      public SqlConnection con;
    DataTable dt;
    public admin()
    {
        
		//
		// TODO: Add constructor logic here
		//
	}

    //public DataSet getControl(string page)
    //{
    //    SqlCommand cmd115 = new SqlCommand("Sp_Select_PageId", con);
    //    cmd115.CommandType = CommandType.StoredProcedure;
    //    cmd115.Parameters.AddWithValue("@Pagename", page);
    //    SqlDataAdapter adp115 = new SqlDataAdapter(cmd115);
    //    DataSet ds115 = new DataSet();
    //    adp115.Fill(ds115);

       

    //    SqlCommand cmd11 = new SqlCommand("Sp_Select_MaxDate1", con);
    //    cmd11.CommandType = CommandType.StoredProcedure;
    //    cmd11.Parameters.AddWithValue("@PageMasterId", Convert.ToInt32(ds115.Tables[0].Rows[0][0].ToString()));
    //    SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
    //    DataSet ds11 = new DataSet();
    //    adp11.Fill(ds11);

    //    SqlCommand cmd = new SqlCommand("Sp_Select_PageContentsByMaxDate", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Date",Convert.ToDateTime(ds11.Tables[0].Rows[0][0].ToString()) );
    //    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(ds115.Tables[0].Rows[0][0].ToString()));
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    adp.Fill(ds);
    //    return ds;
    //}

    public DataSet getcontrolIdAll(string page )
    {
        //SqlCommand cmd12 = new SqlCommand("Sp_Select_PageId", con);
        //cmd12.CommandType = CommandType.StoredProcedure;
        //cmd12.Parameters.AddWithValue("@Pagename", page);
        PageConn pagecon = new PageConn();
        con = pagecon.dynconn;
        string str1 = "select * from PageMaster where PageName='" + page + "' ";

        SqlDataAdapter adp12 = new SqlDataAdapter(str1,con);
        DataSet ds12 = new DataSet();
        DataSet ds21 = new DataSet();
        adp12.Fill(ds12);
        if (ds12.Tables.Count > 0)
        {
            if (ds12.Tables[0].Rows.Count > 0)
            {
                string str = "SELECT     PageMaster.PageId, PageMaster.Pagename, ControlMaster.ControlMasterId, ControlMaster.ControlName " +
                            " FROM         ControlMaster INNER JOIN " +
                              " PageMaster ON ControlMaster.PageMasterId = PageMaster.PageId " +
                            " WHERE     (PageMaster.PageId = '" + Convert.ToInt32(ds12.Tables[0].Rows[0]["PageId"]) + "') " +
                            " order by ControlMaster.ControlMasterId asc ";


                SqlCommand cmd21 = new SqlCommand(str, con);
                SqlDataAdapter adp21 = new SqlDataAdapter(cmd21);
               
                adp21.Fill(ds21);
                if (ds21.Tables.Count > 0)
                {
                    if (ds21.Tables[0].Rows.Count > 0)
                    {
                        return ds21;
                    }
                }
                return ds21;
            }
        }
        return ds21;
    }

    public DataSet getcontrolIdAll1(string page,string comp)
    {
        //SqlCommand cmd12 = new SqlCommand("Sp_Select_PageId", con);
        //cmd12.CommandType = CommandType.StoredProcedure;
        //cmd12.Parameters.AddWithValue("@Pagename", page);
        PageConn pagecon = new PageConn();
        con = pagecon.dynconn;
        string str1 = "select * from PageMaster where PageName='" + page + "' and " +
            " compid='" + comp + "'";

        SqlDataAdapter adp12 = new SqlDataAdapter(str1, con);
        DataSet ds12 = new DataSet();
        DataSet ds21 = new DataSet();
        adp12.Fill(ds12);
        if (ds12.Tables.Count > 0)
        {
            if (ds12.Tables[0].Rows.Count > 0)
            {
                string str = "SELECT     PageMaster.PageId, PageMaster.Pagename, ControlMaster.ControlMasterId, ControlMaster.ControlName " +
                            " FROM         ControlMaster INNER JOIN " +
                              " PageMaster ON ControlMaster.PageMasterId = PageMaster.PageId " +
                            " WHERE     (PageMaster.PageId = '" + Convert.ToInt32(ds12.Tables[0].Rows[0]["PageId"]) + "') " +
                            " order by ControlMaster.ControlMasterId asc ";
 //               string str = "SELECT  PageControl_id,Page_id,ControlName,ControlType_id " +
 //" FROM PageControlMaster where  Page_id= '" + Convert.ToInt32(ds12.Tables[0].Rows[0]["PageId"]) + "' order by PageControl_id ";

                SqlCommand cmd21 = new SqlCommand(str, con);
                SqlDataAdapter adp21 = new SqlDataAdapter(cmd21);

                adp21.Fill(ds21);
                if (ds21.Tables.Count > 0)
                {
                    if (ds21.Tables[0].Rows.Count > 0)
                    {
                        return ds21;
                    }
                }
                return ds21;
            }
        }
        return ds21;
    }

    public DataSet getControlText(int id)
    {
        PageConn pagecon = new PageConn();
        con = pagecon.dynconn;
        string str = "SELECT     ControlMasterId, PageContentsId, Date, Text, ImageUrl, CASE WHEN VisibleMasterid = '1' THEN 'True' ELSE 'False' END AS VisibleMasterid, " +
                      "ImageHeight, ImageWidth " +
                "FROM         PageContentsMaster " +
                "WHERE     (ControlMasterId ='"+ Convert.ToInt32(id) +"') " +
                   " ORDER BY Date DESC ";


        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    public static void CreateMessageAlert(System.Web.UI.Page senderPage, string alertMsg, string alertKey) 
    { 
        
        string strScript = "<script language=JavaScript>alert('" + alertMsg + "')</script>"; 
        if (!(senderPage.IsStartupScriptRegistered(alertKey)))
            senderPage.RegisterStartupScript(alertKey, strScript);
    }
    public Int32 InsertIPAddress(String IPAddress)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertIPMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IPAddress", SqlDbType.NVarChar));
        cmd.Parameters["@IPAddress"].Value = IPAddress;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public DataTable selectCompanyMaster()
    {
        cmd = new SqlCommand();
        DataTable dtr = new DataTable();
        cmd.CommandText = "selectCompanyMaster";
        cmd.Parameters.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar));
        cmd.Parameters["@CompanyName"].Value = HttpContext.Current.Session["comid"].ToString(); // CompanyLoginId;
        dtr = DatabaseCls.FillAdapter(cmd);
        return dtr;
    }
    public Int32 DeleteIPAddress(Int32 IPId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteIPMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IPId", SqlDbType.Int));
        cmd.Parameters["@IPId"].Value = IPId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }

    public DataTable selectIPMaster(string comid)
    {
        cmd = new SqlCommand();
        DataTable dtIp = new DataTable();

        cmd.CommandText = "selectIPMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = comid; // CompanyLoginId;
        dtIp = DatabaseCls.FillAdapter(cmd);
        return dtIp;
    }

    public void updateqty(int inventorywhid, int qty)
    {
        //string str = "SELECT     InventoryDetails.Inventory_Details_Id, InventoryDetails.QtyOnHand, InventoryMaster.InventoryMasterId " + 
        //            " FROM         InventoryMaster INNER JOIN "+
        //              " InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id "+
        //            " WHERE     (InventoryMaster.InventoryMasterId = '"+ inventorywhid +"')";
        PageConn pagecon = new PageConn();
        con = pagecon.dynconn;
        string str =" SELECT     InventoryWarehouseMasterId, QtyOnHand, InventoryMasterId, "+
                    " WareHouseId, Active, QtyOnDateStarted FROM         InventoryWarehouseMasterTbl "+
                     "  where (InventoryWarehouseMasterId = '" + inventorywhid + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        int RemQty = Convert.ToInt32(ds.Tables[0].Rows[0]["QtyOnHand"]) - qty;

        SqlCommand cmd1 = new SqlCommand("update InventoryWarehouseMasterTbl set QtyOnHand='" + RemQty + "' "+
                                         " where InventoryWarehouseMasterId='" + inventorywhid + "'", con);
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close();

    }
    public DataTable selectpagetype(string pagename)
    {
       
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText="SelectPageTypeName";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PageName", SqlDbType.NVarChar));
        cmd.Parameters["@PageName"].Value = pagename;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
    public Int32 UpdateCompanyMasterforIP(bool IPStatus)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCompanyMasterforIP";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IP", SqlDbType.Bit));
        cmd.Parameters["@IP"].Value = IPStatus;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 UpdateCompanymansterAllowIp(bool IPStatus)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCompanymansterAllowIp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IP", SqlDbType.Bit));
        cmd.Parameters["@IP"].Value = IPStatus;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;

    }

}
