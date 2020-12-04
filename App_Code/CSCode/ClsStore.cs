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
/// Summary description for ClsStore
/// </summary>
public class ClsStore
{
    
    
	public ClsStore()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable SelectStorename()
    {
        SqlCommand cmd;
        DataTable dt;
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectStorename";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));

       
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeID"].Value = HttpContext.Current.Session["EmployeeId"].ToString(); // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
       
    }
    public static DataTable SelectEmployeewithIdwise()
    {
        SqlCommand cmd;
        DataTable dt;
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeewithIdwise";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeId"].Value = HttpContext.Current.Session["EmployeeId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public static DataTable SelectEmployeewithBusinessId(String Whid)
    {
        SqlCommand cmd;
        DataTable dt;
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeewithBusinessId";
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; 
        
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;



       

    }
}
