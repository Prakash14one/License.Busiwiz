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
using System.Data;
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

public partial class passwordrecovery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string portalid="3018";
        string strcheck = "  Select * From MasteradminLogin where Emailaddress='"+ TextBox1.Text +"'";
        SqlCommand cmdcheck = new SqlCommand(strcheck, PageConn.licenseconn());
        SqlDataAdapter adpcheck = new SqlDataAdapter(cmdcheck);
        DataTable dtcheck = new DataTable();
        adpcheck.Fill(dtcheck);
        if (dtcheck.Rows.Count > 0)
        {
            string str21 = "  select distinct  PortalMasterTbl.* from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId  inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE(PortalMasterTbl.Id=7)  ";
            str21 = " Select * From PortalMasterTbl where id='"+ portalid +"' ";
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
            if (dt21.Rows.Count > 0)
            {

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


                aa = "" + dt21.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager";
                bb = "" + dt21.Rows[0]["PortalName"].ToString() + " ";
                cc = "" + dt21.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + " ";
                dd = "" + tollfree + " " + tollfreeext + " ";
                ee = "" + dt21.Rows[0]["Portalmarketingwebsitename"].ToString() + "";
                // ff = "" + dt21.Rows[0]["City"].ToString() + " " + dt21.Rows[0]["StateName"].ToString() + " " + dt21.Rows[0]["CountryName"].ToString() + " " + dt21.Rows[0]["Zip"].ToString() + " ";
            }

            string file = "busiwiz.png";
            string name = "";
            string body1 = "<br>  <img src=\"http://license.busiwiz.com//images/" + file + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > <br> Dear Master Admin, <br><br>  You Are requested For User Id and Password Are Below<br<br>" +
            "  <br/><br/> Link : <a href=http://license.busiwiz.com/Admin/AdminLogin.aspx target=_blank>Login Here</a>  <br>User ID : " + dtcheck.Rows[0]["MasterUserid"].ToString() + " <br>Password : " + dtcheck.Rows[0]["MasterPassword"].ToString() + "<br>Thank you,<br>" + aa + "<br>" + bb + "<br>" + cc + "<br>" + ee + "";
            if (dt21.Rows.Count > 0)
            {
                try
                {
                    string email = Convert.ToString(dt21.Rows[0]["UserIdtosendmail"]);
                    string displayname = Convert.ToString("LicenseTeam");
                    string password = Convert.ToString(dt21.Rows[0]["Password"]);
                    string outgo = Convert.ToString(dt21.Rows[0]["Mailserverurl"]);
                    string body = body1;
                    string Subject = "Information of MasterAdmin Login Information of Licnese.busiwiz.com";


                    MailAddress to = new MailAddress(""+ TextBox1.Text  +"");//(tomail);//info@ijobcenter.com("company12@safestmail.net");//
                    MailAddress from = new MailAddress(email, "LicenseTeam");
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = Subject.ToString();
                    objEmail.Body = body.ToString();

                    objEmail.IsBodyHtml = true;
                    objEmail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential(email, password);
                    client.Host = outgo;
                    client.Send(objEmail);
                    lbl_msg.Visible = true;
                    lbl_msg.Text = "You will then receive an e-mail message with your forgotten User IDs";
                }
                catch {
                    lbl_msg.Visible = true;
                    lbl_msg.Text = "Main Not Send Check Internet Connetion or Try Again";
                }



            }

        }
    }
}
