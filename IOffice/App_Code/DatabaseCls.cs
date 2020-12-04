
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

public class DatabaseCls
{
   // public static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ToString());
    public static SqlConnection conn;
    public DatabaseCls()
    {
       
       
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable FillAdapter(SqlCommand cmd)
    {
        dy();
        cmd.Connection = conn;        
        cmd.CommandType = CommandType.StoredProcedure;
        DataTable dttable;
        SqlDataAdapter adp = new SqlDataAdapter((SqlCommand)cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds ,cmd.CommandText ) ;
        dttable  =  ds.Tables[0]; 
         
        return dttable;
    }
    public static DataTable FillAdapterque(SqlCommand cmd)
    {
        dy();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        DataTable dttable;
        SqlDataAdapter adp = new SqlDataAdapter((SqlCommand)cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds, cmd.CommandText);
        dttable = ds.Tables[0];

        return dttable;
    }
    //public static DataTable FillAdapterwithView(SqlCommand cmd)
    //{
    //    cmd.Connection = conn;
    //    cmd.CommandType = CommandType.Text ;
    //    DataTable dttable;
    //    SqlDataAdapter adp = new SqlDataAdapter((SqlCommand)cmd);
    //    DataSet ds = new DataSet();
    //    adp.Fill(ds,cmd.CommandText );
    //    dttable = ds.Tables[0];
    //    return dttable;
    //}
    //public static DbCommand CreateCommand(String str)
    //{
    //    DbCommand Dbcmd = conn.CreateCommand();
    //    opencon(conn);
    //    Dbcmd.CommandType = CommandType.StoredProcedure;
    //    Dbcmd.CommandText = str;
    //    closecon(conn);
    //    return Dbcmd;
    //}
    //public static DataTable ExecuteSelectCommand(DbCommand cmd)
    //{
    //    cmd.Connection = conn;
    //    opencon(conn);
    //    DataTable dttable;
    //    SqlDataAdapter adp = new SqlDataAdapter( (SqlCommand) cmd );
    //    DataSet ds = new DataSet();
    //  //  DbDataReader reader = cmd.ExecuteReader();
    //   // dttable = new DataTable();
    //    adp.Fill(ds  , cmd.CommandText ) ;
    //    dttable  =  ds.Tables[0];
    //    //dttable.Load(reader);
    //    //reader.Close();
    //    //reader.Dispose();
    //    //closecon(conn);
    //    return dttable;
    //}
    public static Int32 ExecuteNonQuery(DbCommand cmd)
    {
        dy();// in comment 
        SqlConnection conn = new SqlConnection();
        
        conn.ConnectionString = @"Data Source =C3\C3SERVERMASTER,30000; Initial Catalog = jobcenter.INTMSGDB; User ID=sa; Password=06De1963++; Persist Security Info=true;";

        cmd.Connection = conn;
        opencon(conn);
        Int32 rst;
       rst = cmd.ExecuteNonQuery();
        rst = (Int32)cmd.Parameters["@ReturnValue"].Value;
        closecon(conn);
        cmd.Dispose();
        return rst;
    }
    public static DbCommand    ExecuteNonQuerywithReturn( DbCommand cmd)
    {
        dy();
        cmd.Connection = conn;
        opencon(conn);
        cmd.ExecuteNonQuery();
        //  rst = (Int32)cmd.Parameters["@EmployeeId"].Value;
        closecon(conn);
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
        if (cn.State == ConnectionState.Closed) { cn.Open(); }
        return cn;
  
    }
    public static DataTable   ExecuteScalar(DbCommand cmd)
    {
        dy();
        cmd.Connection = conn;
        opencon(conn);
       
        DataTable dtTbl = new DataTable();
        dtTbl = (DataTable)  cmd.ExecuteScalar();
        //  rst = (Int32)cmd.Parameters["@EmployeeId"].Value;
        closecon(conn);
        cmd.Dispose();
        return dtTbl;
    }
    public static void dy()
    {
        if (HttpContext.Current.Session["dyconn"] != null)
        {
            conn =(SqlConnection)HttpContext.Current.Session["dyconn"];
        }
        else
        {
            PageConn pgcon = new PageConn();
            conn = pgcon.dynconn; 
        }
    }
}




//using System;
//using System.Data;
//using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using System.Web.Configuration;
//using System.Data.SqlClient;
//using System.Data.Common;

//public class DatabaseCls
//{ 
//    public static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStrOnlineAccount"].ToString());
//    public DatabaseCls()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }    
//    public static  DataTable ExecuteReader(SqlCommand cmd)
//    {          
//        cmd.Connection = conn;
//        opencon(cmd.Connection);
//        cmd.CommandType = CommandType.StoredProcedure;       
//        DataTable dttable;         
//        DbDataReader reader = cmd.ExecuteReader();
//        dttable = new DataTable();         
//        dttable.Load(reader);
//        return dttable;    
//  }      
//    public static DbCommand CreateCommand(String str)    
//    {   
//        DbCommand Dbcmd = conn.CreateCommand();        
//        opencon(conn);
//        Dbcmd.CommandType = CommandType.StoredProcedure;
//        Dbcmd.CommandText = str ;
//        closecon(conn);
//        return Dbcmd;
//    }
//    public static DataTable ExecuteSelectCommand(DbCommand cmd)
//    {
//        cmd.Connection = conn;
//        opencon(conn);
//        DataTable dttable;
//        DbDataReader reader = cmd.ExecuteReader();
//        dttable = new DataTable();
//        dttable.Load(reader);    
//        reader.Close();
//        closecon(conn);
//        return dttable;
//    }
//    public static Int32   ExecuteNonQuery(DbCommand cmd)
//    {
//        cmd.Connection = conn;
//        opencon(conn);
//        Int32  rst;
//        cmd.ExecuteNonQuery();        
//        rst = (Int32) cmd.Parameters["@ReturnValue"].Value ;        
//        closecon(conn);
//        return rst;
//    }    
//    public static SqlConnection closecon(SqlConnection cn)
//    {
//        if (cn.State == ConnectionState.Open) {cn.Close(); }
//        return cn;        
//    }
//    public static SqlConnection opencon(SqlConnection cn)
//    {
//        if (cn.State == ConnectionState.Closed ){cn.Open();}
//        return cn;
//    }
//}
