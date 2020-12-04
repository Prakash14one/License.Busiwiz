
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.Common;

public class DatabaseCls1
{
   // public static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStrOnlineAccount"].ToString());

  //  public static SqlConnection conIfile = new SqlConnection(ConfigurationManager.ConnectionStrings["ifilecabinateConnectionString"].ConnectionString);
 //   public static SqlConnection epconn = new SqlConnection(ConfigurationManager.ConnectionStrings["infinal"].ConnectionString);
    public static SqlConnection epconn;


    public DatabaseCls1()
    {
        //PageConn pgcon = new PageConn();
        //epconn = pgcon.dynconn; 
        //
        // TODO: Add constructor logic here
        //
    }
    public static void dy()
    {
        if (HttpContext.Current.Session["dyconn"] != null)
        {
            epconn = (SqlConnection)HttpContext.Current.Session["dyconn"];
        }
        else
        {
            PageConn pgcon = new PageConn();
            epconn = pgcon.dynconn;
            
        }
    }
    public static Int32 ExecuteNonQueryep(DbCommand cmd)
    {
        dy();
        cmd.Connection = epconn;
        opencon(epconn);
        Int32 rst;
        rst = cmd.ExecuteNonQuery();
        //rst = (Int32)cmd.Parameters["@ReturnValue"].Value;
        closecon(epconn);
        cmd.Dispose();
        return rst;
    }
    public static DataTable FillepAdapter(SqlCommand cmd)
    {
        dy();
        cmd.Connection = epconn;
        cmd.CommandType = CommandType.Text;
        DataTable dttable;
        SqlDataAdapter adp = new SqlDataAdapter((SqlCommand)cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds, cmd.CommandText);
        dttable = ds.Tables[0];

        return dttable;
    }
    public static DataTable FilleppAdapter(SqlCommand cmd)
    {
        dy();
        cmd.Connection = epconn;
        cmd.CommandType = CommandType.StoredProcedure;
        DataTable dttable;
        SqlDataAdapter adp = new SqlDataAdapter((SqlCommand)cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds, cmd.CommandText);
        dttable = ds.Tables[0];

        return dttable;
    }
    public static DataTable FillAdapter(SqlCommand cmd)
    {

        dy();
        cmd.Connection = epconn;
        cmd.CommandType = CommandType.StoredProcedure;
        DataTable dttable;
        SqlDataAdapter adp = new SqlDataAdapter((SqlCommand)cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds, cmd.CommandText);
        dttable = ds.Tables[0];

        return dttable;
    }
    public static DataTable FillAdapterIfile(SqlCommand cmd)
    {
       
        //cmd.Connection = conIfile;
        //cmd.CommandType = CommandType.Text;
        DataTable dttable;
        SqlDataAdapter adp = new SqlDataAdapter((SqlCommand)cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds, cmd.CommandText);
        dttable = ds.Tables[0];

        return dttable;
    }
    public static DataTable FillAdapterwithText(SqlCommand cmd)
    {
        dy();
        cmd.Connection = epconn;
        cmd.CommandType = CommandType.Text;
        DataTable dttable;
        SqlDataAdapter adp = new SqlDataAdapter((SqlCommand)cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds, cmd.CommandText);
        dttable = ds.Tables[0];

        return dttable;
    }
  
   
    public static Int32 ExecuteNonQuery(DbCommand cmd)
    {
         dy();
        cmd.Connection = epconn;

        opencon(epconn);
        Int32 rst;
        rst = Convert.ToInt32(cmd.ExecuteNonQuery());
        //rst = (Int32)cmd.Parameters["@ReturnValue"].Value;
        closecon(epconn);
        cmd.Dispose();
        return rst;
    }
    public static DbCommand ExecuteNonQuerywithReturn(DbCommand cmd)
    {
        dy();
        cmd.Connection = epconn;

        opencon(epconn);
        cmd.ExecuteNonQuery();
        //  rst = (Int32)cmd.Parameters["@EmployeeId"].Value;
        closecon(epconn);
        cmd.Dispose();
        return (DbCommand)cmd; // rst;
    }
    public static SqlConnection closecon(SqlConnection cn)
    {
        if (cn.State == ConnectionState.Open) { cn.Close(); }
        return cn;
    }
    public static SqlConnection opencon(SqlConnection cn)
    {
        if (cn.State == ConnectionState.Closed) 
        {
            cn.Open(); 
        }
        return cn;

    }
    public static DataTable ExecuteScalar(DbCommand cmd)
    {
        dy();
        cmd.Connection = epconn;
        DataTable dtTbl = new DataTable();
        dtTbl = (DataTable)cmd.ExecuteScalar();
        //  rst = (Int32)cmd.Parameters["@EmployeeId"].Value;
        closecon(epconn);
        cmd.Dispose();
        return dtTbl;
    }



}





