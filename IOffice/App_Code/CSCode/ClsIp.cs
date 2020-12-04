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
/// Summary description for ClsIp
/// </summary>
public class ClsIp
{
	public ClsIp()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static int InsertIpControlMastertbl(Boolean Ipcontroll)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "InsertIpControlMastertbl";
        cmd.CommandType = CommandType.StoredProcedure;
        
        cmd.Parameters.AddWithValue("@Ipcontroll", Ipcontroll);
        cmd.Parameters.AddWithValue("@CID", HttpContext.Current.Session["Comid"].ToString());

        //cmd.Parameters.Add(new SqlParameter("@IpcontrolId", SqlDbType.Int));
        //cmd.Parameters["@IpcontrolId"].Direction = ParameterDirection.Output;

        //cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        //if (result > 0)
        //{
        //    result = Convert.ToInt32(cmd.Parameters["@IpcontrolId"].Value);
        //}
        return result;
    }
    public static DataTable SelectUserbywhid(String Whid)
    {

        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectUserbywhid";
      
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static int InsertIpAddress(Int32 IpcontrolId, Boolean Cidwise, Boolean Userwise, String UserId, String Ipaddress)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "InsertIpAddress";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@IpcontrolId", IpcontrolId);
        cmd.Parameters.AddWithValue("@Cidwise", Cidwise);
        cmd.Parameters.AddWithValue("@Userwise", Userwise);
        cmd.Parameters.AddWithValue("@UserId", UserId);
        cmd.Parameters.AddWithValue("@Ipaddress", Ipaddress);
        cmd.Parameters.AddWithValue("@CID", HttpContext.Current.Session["Comid"].ToString());
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
       
        return result;
    }
    public static int DeleteIpAddress(Int32 Id)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "DeleteIpAddress";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Id", Id);
       

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);

        return result;
    }
    public static DataTable SelctIpControlMastertblId()
    {

        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelctIpControlMastertblId";

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable SelctIpGridfill(String Whid, int flag,String UserId)
    {

        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelctIpGridfill";

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int));
        cmd.Parameters["@flag"].Value = @flag; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar));
        cmd.Parameters["@UserId"].Value = UserId; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable selectIPrestriction(String Ipaddress, int flag, String username, String password, String CID)
    {

        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "selectIPrestriction";

        cmd.Parameters.Add(new SqlParameter("@Ipaddress", SqlDbType.NVarChar));
        cmd.Parameters["@Ipaddress"].Value = Ipaddress; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int));
        cmd.Parameters["@flag"].Value = @flag; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar));
        cmd.Parameters["@username"].Value = @username; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar));
        cmd.Parameters["@password"].Value = password; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = CID;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable selectIPrestrictionallow(String CID)
    {

        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "selectIPrestrictionallow";

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = CID; // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable selectIPrest(int flag, String username, String password, String CID)
    {

        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "selectIPrest";

      
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int));
        cmd.Parameters["@flag"].Value = @flag; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar));
        cmd.Parameters["@username"].Value = @username; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar));
        cmd.Parameters["@password"].Value = password; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = CID;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable selectmultiIP(String Ip1, String Ipz1, String Ip2, String Ipz2, int cip, int uip, String username, String password, String CID)
    {

        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "selectmultiIP";

        cmd.Parameters.Add(new SqlParameter("@Ip1", SqlDbType.NVarChar));
        cmd.Parameters["@Ip1"].Value = Ip1; // CompanyLoginId;


        cmd.Parameters.Add(new SqlParameter("@Ipz1", SqlDbType.NVarChar));
        cmd.Parameters["@Ipz1"].Value = Ipz1; // CompanyLoginId;


        cmd.Parameters.Add(new SqlParameter("@Ip2", SqlDbType.NVarChar));
        cmd.Parameters["@Ip2"].Value = Ip2; // CompanyLoginId;


        cmd.Parameters.Add(new SqlParameter("@Ipz2", SqlDbType.NVarChar));
        cmd.Parameters["@Ipz2"].Value = Ipz2; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@cip", SqlDbType.Int));
        cmd.Parameters["@cip"].Value = cip; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@uip", SqlDbType.Int));
        cmd.Parameters["@uip"].Value = uip; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar));
        cmd.Parameters["@username"].Value = @username; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar));
        cmd.Parameters["@password"].Value = password; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = CID;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
  
}
