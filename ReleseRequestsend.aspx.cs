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
using System.Xml;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;
using System.Net;
using System.Management;
using System.Text;
using System.Net.Mail;

public partial class ReleseRequestsend : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection connserver;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["a"] != null && Request.QueryString["b"] != null && Request.QueryString["c"] != null && Request.QueryString["d"] != null && Request.QueryString["e"] != null )
            {
                string MacAddress = PageMgmt.Decrypted(Request.QueryString["a"].Replace(" ","+"));
                string Ipaddress = PageMgmt.Decrypted(Request.QueryString["b"].Replace(" ", "+"));
                string bannedmacaddress = PageMgmt.Decrypted(Request.QueryString["c"].Replace(" ", "+"));
                string bannedipaddress = PageMgmt.Decrypted(Request.QueryString["d"].Replace(" ", "+"));
                string compid = PageMgmt.Decrypted(Request.QueryString["e"].Replace(" ", "+"));
                string computername = PageMgmt.Decrypted(Request.QueryString["f"].Replace(" ", "+"));
                string computerip = PageMgmt.Decrypted(Request.QueryString["g"].Replace(" ", "+"));             

                string strftpdetail = " SELECT * from ServerMasterTbl where Status='1'";
                SqlCommand cmdftpdetail = new SqlCommand(strftpdetail, con);
                DataTable dtftpdetail = new DataTable();
                SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
                adpftpdetail.Fill(dtftpdetail);
                if (dtftpdetail.Rows.Count > 0)
                {
                    string strlicense = " update Permenatalybanmacaddressandipadress set BanIsActive='0' where MacAddress='" + MacAddress + "' and Ipaddress='" + Ipaddress + "' and computerip='" + computerip + "' and computername='" + computername + "' and bannedmacaddress='" + bannedmacaddress + "' and bannedipaddress='" + bannedipaddress + "' and BanIsActive='1' ";
                    SqlCommand cmdlicense = new SqlCommand(strlicense, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdlicense.ExecuteNonQuery();
                    con.Close();

                    if (bannedmacaddress == "1" && bannedipaddress == "0")
                    {
                        sendmailformac(compid, computername);                                                                     
                    }
                    if (bannedmacaddress == "0" && bannedipaddress == "1")
                    {
                        sendmailforip(compid, Ipaddress);
                       
                    }

                    foreach (DataRow dr in dtftpdetail.Rows)
                    {
                        string serversqlserverip = dr["sqlurl"].ToString();
                        string serversqlinstancename = dr["DefaultsqlInstance"].ToString();
                        string serversqldbname = dr["DefaultDatabaseName"].ToString();
                        string serversqlpwd = dr["Sapassword"].ToString();
                        string serversqlport = dr["port"].ToString();

                        connserver = new SqlConnection();
                        connserver.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";

                        try
                        {
                            string strupdate = "  update Permenatalybanmacaddressandipadress set BanIsActive='0' where MacAddress='" + MacAddress + "' and Ipaddress='" + Ipaddress + "' and computerip='" + computerip + "' and computername='" + computername + "' and bannedmacaddress='" + bannedmacaddress + "' and bannedipaddress='" + bannedipaddress + "'  ";
                            SqlCommand cmdupdate = new SqlCommand(strupdate, connserver);
                            if (connserver.State.ToString() != "Open")
                            {
                                connserver.Open();
                            }
                            cmdupdate.ExecuteNonQuery();
                            connserver.Close();
                        }
                        catch
                        {

                        }
                    }
                    
                }



            }
        }
    }

    public void sendmailformac(string cid, string computername)
    {
        string str = "select  PortalMasterTbl.*,StateMasterTbl.StateName,CountryMaster.CountryName,CompanyMaster.ContactPerson,CompanyMaster.CompanyName,CompanyMaster.Email as CustEmail,CompanyMaster.CompanyLoginId,CompanyMaster.AdminId,CompanyMaster.Password as PWD from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join PortalMasterTbl on PortalMasterTbl.Id=PricePlanMaster.PortalMasterId1  inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId WHERE (CompanyMaster.CompanyLoginId = '" + cid + "') ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            StringBuilder strplan = new StringBuilder();

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"> <img src=\"http://license.busiwiz.com/images/" + dt.Rows[0]["LogoPath"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
            strplan.Append("<br></table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">Dear " + dt.Rows[0]["ContactPerson"].ToString() + ",</td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">Your recent request to release ban of computer - " + computername + " for " + dt.Rows[0]["PortalName"].ToString() + " regarding company " + dt.Rows[0]["CountryName"].ToString() + " has been approved by the " + dt.Rows[0]["PortalName"].ToString() + "  support team.</td></tr> ");
            strplan.Append("<tr><td align=\"left\"> The user would now be able to login from the computer " + computername + "</td></tr> ");
            strplan.Append("<tr><td align=\"left\"> If you have any difficulty, please do not hesitate to contact again.</td></tr> ");

            strplan.Append("</table> ");

            string ext = "";
            string tollfree = "";
            string tollfreeext = "";

            if (Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != null)
            {
                ext = "ext " + dt.Rows[0]["Supportteamphonenoext"].ToString();
            }

            if (Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != null)
            {
                tollfree = dt.Rows[0]["Tollfree"].ToString();
            }

            if (Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != null)
            {
                tollfreeext = "ext " + dt.Rows[0]["Tollfreeext"].ToString();
            }


            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">Thanking you </td></tr>  ");
            strplan.Append("<tr><td align=\"left\">Sincerely ,</td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");

            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager</td></tr> ");
            strplan.Append("<tr><td align=\"left\">Support Manager</td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["PortalName"].ToString() + " </td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + "  </td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + tollfree + " " + tollfreeext + " </td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Portalmarketingwebsitename"].ToString() + " </td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Address1"].ToString() + " </td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["City"].ToString() + " " + dt.Rows[0]["StateName"].ToString() + " " + dt.Rows[0]["CountryName"].ToString() + " " + dt.Rows[0]["Zip"].ToString() + " </td></tr> ");
            strplan.Append("</table> ");


            string bodyformate = "" + strplan + "";

            try
            {

                MailAddress to = new MailAddress(dt.Rows[0]["CustEmail"].ToString(), dt.Rows[0]["ContactPerson"].ToString());
                MailAddress from = new MailAddress(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["EmailDisplayname"].ToString());
                MailMessage objEmail = new MailMessage(from, to);

                objEmail.Subject = "Release of ban of computer -" + computername + " for " + dt.Rows[0]["PortalName"].ToString() + " regarding company - " + dt.Rows[0]["CompanyName"].ToString() + "";


                objEmail.Body = bodyformate.ToString();
                objEmail.IsBodyHtml = true;
                objEmail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["Password"].ToString());
                client.Host = dt.Rows[0]["Mailserverurl"].ToString();
                client.Send(objEmail);

                Label1.Text = "An email has been successfully sent to company admin of " + dt.Rows[0]["CompanyName"].ToString() + "";
            }
            catch (Exception e)
            {

            }
        }
    }
    public void sendmailforip(string cid, string ipaddress)
    {
        string str = "select  PortalMasterTbl.*,StateMasterTbl.StateName,CountryMaster.CountryName,CompanyMaster.ContactPerson,CompanyMaster.CompanyName,CompanyMaster.Email as CustEmail,CompanyMaster.CompanyLoginId,CompanyMaster.AdminId,CompanyMaster.Password as PWD from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join PortalMasterTbl on PortalMasterTbl.Id=PricePlanMaster.PortalMasterId1  inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId WHERE (CompanyMaster.CompanyLoginId = '" + cid + "') ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            StringBuilder strplan = new StringBuilder();

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"> <img src=\"http://license.busiwiz.com/images/" + dt.Rows[0]["LogoPath"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
            strplan.Append("<br></table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">Dear " + dt.Rows[0]["ContactPerson"].ToString() + ",</td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">Your recent request to release ban of IP address - " + ipaddress + " for " + dt.Rows[0]["PortalName"].ToString() + " regarding company " + dt.Rows[0]["CountryName"].ToString() + " has been approved by the <portal> support team.</td></tr> ");
            strplan.Append("<tr><td align=\"left\"> The user would now be able to login from the IP address " + ipaddress + "</td></tr> ");
            strplan.Append("<tr><td align=\"left\"> If you have any difficulty, please do not hesitate to contact again.</td></tr> ");
            strplan.Append("</table> ");

            string ext = "";
            string tollfree = "";
            string tollfreeext = "";

            if (Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != null)
            {
                ext = "ext " + dt.Rows[0]["Supportteamphonenoext"].ToString();
            }

            if (Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != null)
            {
                tollfree = dt.Rows[0]["Tollfree"].ToString();
            }

            if (Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != null)
            {
                tollfreeext = "ext " + dt.Rows[0]["Tollfreeext"].ToString();
            }


            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">Thanking you </td></tr>  ");
            strplan.Append("<tr><td align=\"left\">Sincerely ,</td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");

            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager</td></tr> ");
            strplan.Append("<tr><td align=\"left\">Support Manager</td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["PortalName"].ToString() + " </td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + "  </td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + tollfree + " " + tollfreeext + " </td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Portalmarketingwebsitename"].ToString() + " </td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Address1"].ToString() + " </td></tr> ");
            strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["City"].ToString() + " " + dt.Rows[0]["StateName"].ToString() + " " + dt.Rows[0]["CountryName"].ToString() + " " + dt.Rows[0]["Zip"].ToString() + " </td></tr> ");
            strplan.Append("</table> ");


            string bodyformate = "" + strplan + "";

            try
            {

                MailAddress to = new MailAddress(dt.Rows[0]["CustEmail"].ToString(), dt.Rows[0]["ContactPerson"].ToString());
                MailAddress from = new MailAddress(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["EmailDisplayname"].ToString());
                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = "Release of ban of IP address -" + ipaddress + " for " + dt.Rows[0]["PortalName"].ToString() + " regarding company - " + dt.Rows[0]["CompanyName"].ToString() + "";
                objEmail.Body = bodyformate.ToString();
                objEmail.IsBodyHtml = true;
                objEmail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["Password"].ToString());
                client.Host = dt.Rows[0]["Mailserverurl"].ToString();
                client.Send(objEmail);

                Label1.Text = "An email has been successfully sent to company admin of " + dt.Rows[0]["CompanyName"].ToString() + "";
            }
            catch (Exception e)
            {

            }
        }
    }

}
