using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data.Common;
/// <summary>
/// Summary description for configuration
/// </summary>
public static class SiteConfiguration
{
    private readonly static string dbConnectionString;
    //private readonly static string dbProviderName;
    //private readonly static int cartPersistDays;
    //private readonly static string requireLogin;
    //private readonly static string payPalAPIAccountName;
    //private readonly static string payPalAPICertificationPath;
    //private readonly static string payPalAPICertificationPassword;
    //private readonly static string payPalAPIAccountPassword;
    public static SqlConnection epconn;
    static SiteConfiguration()
    {
        //cartPersistDays = Int32.Parse(ConfigurationManager.AppSettings["CartPersistDays"]);
        //requireLogin = ConfigurationManager.AppSettings["RequireLogin"];
        dbConnectionString = PageConn.connnn;
       
        //dbProviderName = ConfigurationManager.ConnectionStrings["DyProduct1.0ConnectionString"].ProviderName;
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
    //public static string PayPalAPIAccountPassword
    //{
    //    get
    //    {
    //        return payPalAPIAccountPassword;
    //    }
    //}

    //public static string PayPalAPICertificationPassword
    //{
    //    get
    //    {
    //        return payPalAPICertificationPassword;
    //    }
    //}

    //public static string PayPalAPICertificationPath
    //{
    //    get
    //    {
    //        return payPalAPICertificationPath;
    //    }
    //}


    //public static string PayPalAPIAccountName
    //{
    //    get
    //    {
    //        return payPalAPIAccountName;
    //    }
    //}

    //public static string RequireLogin
    //{
    //    get
    //    {
    //        return requireLogin;
    //    }
    //}

    //public static int CartPersistDays
    //{
    //    get
    //    {
    //        return cartPersistDays;
    //    }
    //}


    //// Returns the address of the mail server
    //public static string MailServer
    //{
    //    get
    //    {
    //        return ConfigurationManager.AppSettings["MailServer"];
    //    }
    //}

    // Returns the connection string for the BalloonShop database
    public static string DbConnectionString
    {
        get
        {
            return dbConnectionString;
        }
    }
    // Send error log emails?
    //public static bool EnableErrorLogEmail
    //{
    //    get
    //    {
    //        return bool.Parse(ConfigurationManager.AppSettings["EnableErrorLogEmail"]);
    //    }
    //}

    //// Returns the email address where to send error reports
    //public static string ErrorLogEmail
    //{
    //    get
    //    {
    //        return ConfigurationManager.AppSettings["ErrorLogEmail"];
    //    }
    //}

    

}
