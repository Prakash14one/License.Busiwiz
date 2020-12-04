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

public class LBAllEmail
{
   public static string connnn;
    public SqlConnection dynconn;
    public static string strEnc = "";
    public static string bidname = "";
    public static string busdatabase = "";
    public static string Userrnamedb = "";
    public static string intmsg11 = "";
    public static string extmsg11 = "";
    public LBAllEmail()
    {
        dynconn = new SqlConnection();
    }
    public static string GetC(String CompanyLoginId)
    {      
        string Valuesof_C = "";
       
        return Valuesof_C;
    }
    public static bool PortOpenStatusCheck(string HostURI, int PortNumber)
    {
        try
        {            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
