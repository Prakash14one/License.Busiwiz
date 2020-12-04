using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.Net.Mail;



public partial class Errorpage : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
       page_error();
     
    }
    public void page_error()
    {

        if (Session["Comid"]!=null)
        {
            string err = "";
            lblerrms.Text = "Sorry this page has some error.Kindly report to webmaster.You need to login again to the application";

            string Strerr = "select ProductMaster.ProductName,PricePlanName, CompanyMaster.PlanId from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId  where CompanyLoginId='" + Session["Comid"].ToString() + "'";
            SqlCommand cidco = new SqlCommand(Strerr, PageConn.licenseconn());
            SqlDataAdapter adc = new SqlDataAdapter();
            DataTable dts = new DataTable();
            if (dts.Rows.Count > 0)
            {
                err += err + "<br><b>Product Name :</b>" + dts.Rows[0]["ProductName"];
                err += err + "<br><b>Price Name :</b>" + dts.Rows[0]["PricePlanName"];
                err += err + "<br><b>Company Id :</b>" + Session["Comid"];
            }
             err =err+ "<br><b>Error Caught in Page :</b>" + Request.Url.ToString();
            string bodytext = "<br><br><span style=\"font-size:14px; font-family:Calibri; text-align:left\">Thanks</span>";

            StringBuilder support = new StringBuilder();
            support.Append("<table style=\"font-size:14px; font-family:Calibri; text-align:left\" width=\"100%\"> ");
            support.Append("<tr>Support Team</span></b>");
            support.Append("<tr>Busiwiz.com</span></b>");
            support.Append("<br></table> ");

            string bodyformate = "" + err + "<br>" + bodytext + "<br>" + support;
           // Server.ClearError();
            try
            {
                MailAddress to = new MailAddress("mp@onlinemanagers.com");
                MailAddress from = new MailAddress("jk@aacpa.us");
                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = "Page Error Message";

                objEmail.Body = bodyformate.ToString();
                objEmail.IsBodyHtml = true;
                objEmail.Priority = MailPriority.High;

                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential("jk@aacpa.us", "Jk2012++");
                client.Host = "mail.aacpa.us";
                client.Send(objEmail);
               
            }
            catch (Exception e)
            {
            }
            Session.Clear();
            Session.Abandon();
            Response.AddHeader("Pragma", "no-cache");
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Expires = -1;
        }
        else
        {
            lblerrms.Text = "Your session timeout limit has been reached login required.....";
        }

      //  Exception objerr = Server.GetLastError().GetBaseException();
      //  string err = "<b> Error Caught in Page_Error event</b><br><br>" +
      //      "<br><b>Error In :</b>" + Request.Url.ToString() +
      //          "<br><b>Error Message:</b>" + objerr.Message.ToString() +
      //          "<br><b>Stack Trace :</b><br>" +
      //          objerr.StackTrace.ToString();
      ////  Response.Write(err.ToString());
      //  Server.ClearError();
      
     
    }
   
    protected void btnsignin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/ShoppingCartLogin.aspx");
       
      
    }
   
}
