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
/// Summary description for PageConn
/// </summary>
public class PageConnXYZXYZ
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString1"].ConnectionString);

    //SqlConnection con;
    public static string connnn;
    public SqlConnection dynconn;
    public static string strEnc = "";
    public static string bidname = "";
    public static string busdatabase = "";
    public static string Userrnamedb = "";
    public static string intmsg11 = "";
    public static string extmsg11 = "";
   // public SqlConnection servermasterconn;
    public static string commonip = "TCP:192.168.2.100,40000";
    public PageConnXYZXYZ()
    {
        dynconn = new SqlConnection();
       
       
    }
 
    public static string BtoE(String B,String E,Int64 F , String DD,String MM,String YYYY)
    {
        string A="";
        A = B + E;
        String C = CompKeyIns.C1toC5GetC(A, F,DD,MM,YYYY);
        return C;
    }

    
}
