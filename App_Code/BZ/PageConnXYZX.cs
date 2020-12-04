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
public class PageConnXYZX
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
   
    public PageConnXYZX()
    {
        dynconn = new SqlConnection();
    }
    public static string GetC(String CompanyLoginId)
    {      
        string Valuesof_C = "";
        DateTime todaydatefull = DateTime.Now;
        string strdt = todaydatefull.ToString("MM-dd-yyyy");
        DateTime startDate = DateTime.Parse(strdt);
        DateTime datevalue = (Convert.ToDateTime(startDate.ToString("MM-dd-yyyy")));
        String DD = datevalue.Day.ToString();
        String MM = datevalue.Month.ToString();
        String YYYY = datevalue.Year.ToString();
        string dateid = DD + "" + MM + "" + YYYY;
        string strcln = " Select * From CompanyABCDetail Where CompanyLoginId='" + CompanyLoginId + "' and Z='" + dateid + "' and F1 IS NOT NULL and F2 IS NOT NULL and F3 IS NOT NULL and F4 IS NOT NULL and F5 IS NOT NULL and E1 IS NOT NULL and E2 IS NOT NULL and E3 IS NOT NULL and E4 IS NOT NULL and E5 IS NOT NULL  ";
        SqlCommand cmdcln = new SqlCommand(strcln,PageConn.licenseconn());
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            string Z = dtcln.Rows[0]["Z"].ToString();
            string C1 = PageConnXYZXYZ.BtoE(dtcln.Rows[0]["D1"].ToString(), dtcln.Rows[0]["E1"].ToString(), Convert.ToInt64(dtcln.Rows[0]["F1"].ToString()), DD,MM,YYYY);
            string C2 = PageConnXYZXYZ.BtoE(dtcln.Rows[0]["D2"].ToString(), dtcln.Rows[0]["E2"].ToString(), Convert.ToInt64(dtcln.Rows[0]["F2"].ToString()), DD, MM, YYYY);
            string C3 = PageConnXYZXYZ.BtoE(dtcln.Rows[0]["D3"].ToString(), dtcln.Rows[0]["E3"].ToString(), Convert.ToInt64(dtcln.Rows[0]["F3"].ToString()), DD, MM, YYYY);
            string C4 = PageConnXYZXYZ.BtoE(dtcln.Rows[0]["D4"].ToString(), dtcln.Rows[0]["E4"].ToString(), Convert.ToInt64(dtcln.Rows[0]["F4"].ToString()), DD, MM, YYYY);
            string C5 = PageConnXYZXYZ.BtoE(dtcln.Rows[0]["D5"].ToString(), dtcln.Rows[0]["E5"].ToString(), Convert.ToInt64(dtcln.Rows[0]["F5"].ToString()), DD, MM, YYYY);
            Valuesof_C = C1 + C2 + C3 + C4 + C5;
        }
        else
        {

        }
        return Valuesof_C;
    }

    public static string GetA(String CompanyLoginId)
    {
        string Valuesof_C = "";
        DateTime todaydatefull = DateTime.Now;
        string strdt = todaydatefull.ToString("MM-dd-yyyy");
        DateTime startDate = DateTime.Parse(strdt);
        DateTime datevalue = (Convert.ToDateTime(startDate.ToString("MM-dd-yyyy")));
        String DD = datevalue.Day.ToString();
        String MM = datevalue.Month.ToString();
        String YYYY = datevalue.Year.ToString();
        string dateid = DD + "" + MM + "" + YYYY;
        string strcln = " Select * From CompanyABCDetail Where CompanyLoginId='" + CompanyLoginId + "' and Z='" + dateid + "' and F1 IS NOT NULL and F2 IS NOT NULL and F3 IS NOT NULL and F4 IS NOT NULL and F5 IS NOT NULL and E1 IS NOT NULL and E2 IS NOT NULL and E3 IS NOT NULL and E4 IS NOT NULL and E5 IS NOT NULL  ";
        SqlCommand cmdcln = new SqlCommand(strcln, PageConn.licenseconn());
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            string Z = dtcln.Rows[0]["Z"].ToString();
            string C1 = PageConnXYZXYZ.BtoE(dtcln.Rows[0]["D1"].ToString(), dtcln.Rows[0]["E1"].ToString(), Convert.ToInt64(dtcln.Rows[0]["F1"].ToString()), DD, MM, YYYY);
            string C2 = PageConnXYZXYZ.BtoE(dtcln.Rows[0]["D2"].ToString(), dtcln.Rows[0]["E2"].ToString(), Convert.ToInt64(dtcln.Rows[0]["F2"].ToString()), DD, MM, YYYY);
            string C3 = PageConnXYZXYZ.BtoE(dtcln.Rows[0]["D3"].ToString(), dtcln.Rows[0]["E3"].ToString(), Convert.ToInt64(dtcln.Rows[0]["F3"].ToString()), DD, MM, YYYY);
            string C4 = PageConnXYZXYZ.BtoE(dtcln.Rows[0]["D4"].ToString(), dtcln.Rows[0]["E4"].ToString(), Convert.ToInt64(dtcln.Rows[0]["F4"].ToString()), DD, MM, YYYY);
            string C5 = PageConnXYZXYZ.BtoE(dtcln.Rows[0]["D5"].ToString(), dtcln.Rows[0]["E5"].ToString(), Convert.ToInt64(dtcln.Rows[0]["F5"].ToString()), DD, MM, YYYY);
            Valuesof_C = C1 + C2 + C3 + C4 + C5;
        }
        else
        {

        }
        return Valuesof_C;
    }



    
}
