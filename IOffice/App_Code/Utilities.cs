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


/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities
{
	public Utilities()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //public static void LogError(Exception ex)
    //{
    //    // get the current date and time
    //    string dateTime = DateTime.Now.ToLongDateString() + ", at "
    //                    + DateTime.Now.ToShortTimeString();
    //    // stores the error message
    //    string errorMessage = "Exception generated on " + dateTime;
    //    // obtain the page that generated the error
    //    System.Web.HttpContext context = System.Web.HttpContext.Current;
    //    errorMessage += "\n\n Page location: " + context.Request.RawUrl;
    //    // build the error message
    //    errorMessage += "\n\n Message: " + ex.Message;
    //    errorMessage += "\n\n Source: " + ex.Source;
    //    errorMessage += "\n\n Method: " + ex.TargetSite;
    //    errorMessage += "\n\n Stack Trace: \n\n" + ex.StackTrace;
    //    // send error email in case the option is activated in Web.Config
    //    if (SiteConfiguration.EnableErrorLogEmail)
    //    {
    //        string from = "noreply@domainname.com";
    //        string to = SiteConfiguration.ErrorLogEmail;
    //        string subject = "Error Report";
    //        string body = errorMessage;
    //        SendMail(from, to, subject, body);
    //    }
    //}


    //public static void SendMail(string from, string to, string subject, string body)
    //{
       
    //    SmtpClient mailClient = new SmtpClient(SiteConfiguration.MailServer);
    //           MailMessage mailMessage = new MailMessage(from, to, subject, body);
    //    /*
    //       // For SMTP servers that require authentication
    //       message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
    //       message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "SmtpHostUserName");
    //       message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "SmtpHostPassword");
    //      */
    //    // Send mail
    //    mailClient.Send(mailMessage);
    //}


    public static string GetPageName(string URL)
    {
        string url = URL;

        char[] patt = { '/' };
        char[] p = { '.' };
        string[] arr = url.Split(patt);
        url = arr[arr.Length - 1];

        url = url.Remove(url.IndexOf('.'));

        return url;
    }

}
