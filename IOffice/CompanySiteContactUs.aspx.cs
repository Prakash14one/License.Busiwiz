using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class CompanySiteContactUs : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        PageConn.strEnc = "3d70f5cff23ed17ip8H9";

        //var subdomain = Request.Url.Host.Replace("testpace.net", "").TrimEnd('.');
        //string rawURL = Request.Url.Host;
        //string domainName = rawURL.Split(new char[] { '.', '.' })[0];
        //string strUserAgent = Request.UserAgent.ToString().ToLower();
        //bool MobileDevice = Request.Browser.IsMobileDevice;
        if (!IsPostBack)
        {
            //Session["Comid"] = domainName;
            //domainName;
           

        }
    }
   
   
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["Captcha"] != null)
        {
            if (TextBox6.Text == Session["Captcha"].ToString())
            {
                con.Open();
                string insert = " insert into CompanyWebsiteSendMessageTBL(CompanyID,Sender_Name,Sender_Company_Name,Phone,Email,Message,Date_and_Time,Status) values(" +
                                  "'" + Session["Comid"] + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + DateTime.Now.ToShortDateString() + "','Unapproved by Master Admin')";
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.ExecuteNonQuery();
                con.Close();
                string sele1 = "select max(ID) from CompanyWebsiteSendMessageTBL";
                SqlDataAdapter adpp = new SqlDataAdapter(sele1, con);
                DataTable dsp = new DataTable();
                adpp.Fill(dsp);
                ViewState["id"] = dsp.Rows[0][0].ToString();

                sendmail();
            }
        }
    }
    public void sendmail()
    {
        string str21 = "  select distinct  PortalMasterTbl.* from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId  inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE(PortalMasterTbl.Id=7)  ";
        SqlCommand cmd45 = new SqlCommand(str21, PageConn.licenseconn());
        SqlDataAdapter adp45 = new SqlDataAdapter(cmd45);
        DataTable dt21 = new DataTable();
        adp45.Fill(dt21);

        string aa = "";
        string bb = "";
        string cc = "";
        string ff = "";
        string ee = "";
        string dd = "";
        string ext = "";
        string tollfree = "";
        string tollfreeext = "";


        if (Convert.ToString(dt21.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt21.Rows[0]["Supportteamphonenoext"].ToString()) != null)
        {
            ext = "ext " + dt21.Rows[0]["Supportteamphonenoext"].ToString();
        }

        if (Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != null)
        {
            tollfree = dt21.Rows[0]["Tollfree"].ToString();
        }

        if (Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != null)
        {
            tollfreeext = "ext " + dt21.Rows[0]["Tollfreeext"].ToString();
        }
        if (dt21.Rows.Count > 0)
        {

            aa = "" + dt21.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager";
            bb = "" + dt21.Rows[0]["PortalName"].ToString() + " ";
            cc = "" + dt21.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + " ";
            dd = "" + tollfree + " " + tollfreeext + " ";
            ee = "" + dt21.Rows[0]["Portalmarketingwebsitename"].ToString() + "";
        }
        if (dt21.Rows.Count > 0)
        {
            //ViewState["vacid"] = Session["vacanyid"].ToString();
            string file = "job-center-logo.jpg";
            string body1 = "<br>  <img src=\"http://members.ijobcenter.com/images/" + file + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > <br>Dear Admin, <br><br>The following message was posted to " + TextBox2.Text + " by " + TextBox1.Text + ". Please review the particulars of the message for authorization.  <br><br>Name: " + TextBox1.Text + " <br>Company Name: " + TextBox2.Text + "<br>Phone No.: " + TextBox3.Text + "<br>Email: " + TextBox4.Text + "<br>Message : " + TextBox5.Text + "<br><br>" +
            " If you wish to approve this message and permit its posting click <a href=http://www.ijobcenter.com/MessageAuthorizationConfirmation.aspx?id=" + ClsEncDesc.Encrypted(ViewState["id"].ToString()) + " target=_blank > here </a>  or copy and paste the following URL into your internet browser.<br><br>http://www.ijobcenter.com/MessageAuthorizationConfirmation.aspx?id=" + ClsEncDesc.Encrypted(ViewState["id"].ToString()) + " <br><br>If you wish to reject this message and discard its posting click <a href=http://www.ijobcenter.com/MessageAuthorizationConfirmation.aspx?id1=" + ClsEncDesc.Encrypted(ViewState["id"].ToString()) + " target=_blank > here </a>   or copy and paste the following URL into your internet browser.<br><br>http://www.ijobcenter.com/MessageAuthorizationConfirmation.aspx?id1=" + ClsEncDesc.Encrypted(ViewState["id"].ToString()) + " <br><br>Thank you,</span><br><br>IJobCenter Support Team<br>" + aa + "<br>" + bb + "<br>" + cc + "<br>" + ee + "";

            string email = Convert.ToString(dt21.Rows[0]["UserIdtosendmail"]);
            string displayname = Convert.ToString("IJobCenter");
            string password = Convert.ToString(dt21.Rows[0]["Password"]);
            string outgo = Convert.ToString(dt21.Rows[0]["Mailserverurl"]);
            string body = body1;
            string Subject = "Enquiry For Ijobcenter";

            MailAddress to = new MailAddress("support@ijobcenter.com");
            MailAddress from = new MailAddress(email, displayname);
            MailMessage objEmail = new MailMessage(from, to);
            objEmail.Subject = Subject.ToString();
            objEmail.Body = body.ToString();
            objEmail.IsBodyHtml = true;
            objEmail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(email, password);
            client.Host = outgo;
            client.Send(objEmail);
            clr();
           // Response.Write("<script>alert('Successfully Receive your Message!!')</script> ");
        }


    }
    public void clr()
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
    }
}