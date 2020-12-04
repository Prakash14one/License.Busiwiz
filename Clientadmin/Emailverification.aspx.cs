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

public partial class ShoppingCart_Admin_Emailverification : System.Web.UI.Page
{
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
         if (!IsPostBack)
        {
            if (Request.QueryString["cid"] != null && Request.QueryString["uid"] != null && Request.QueryString["ip"] != null && Request.QueryString["option"] != null && Request.QueryString["maxid"] != null)
            {
                string cid = ClsEncDesc.Decrypted(Convert.ToString(Request.QueryString["cid"]));
                ViewState["cid"] = cid.ToString();

                string uid = Convert.ToString(Request.QueryString["uid"]);
                ViewState["uid"] = uid.ToString();

                string stripa = "select Login_master.*,Party_master.Compname from  User_master inner join  Login_master on Login_master.UserID=User_master.UserID inner join Party_master on Party_master.PartyID=User_master.PartyID where Login_master.UserID='" + ViewState["uid"].ToString() + "'  ";
                SqlCommand cmdipa = new SqlCommand(stripa, conn);
                SqlDataAdapter adpipa = new SqlDataAdapter(cmdipa);
                DataTable dsipa = new DataTable();
                adpipa.Fill(dsipa);

                if (dsipa.Rows.Count > 0)
                {                    
                    ViewState["partyname"] = dsipa.Rows[0]["Compname"].ToString();
                }

               // ViewState["uid"] = uid.ToString();

                string ip = Convert.ToString(Request.QueryString["ip"]);
                ViewState["ip"] = ip.ToString();

                string option = Convert.ToString(Request.QueryString["option"]);
                ViewState["option"] = option.ToString();

                string maxid = (Request.QueryString["maxid"]);
                ViewState["maxid"] = maxid.ToString();

                string chekeit = "Select * from Ipaddresschangerequesttbl where Id='" + maxid.ToString() + "' and ipaddresssuceessfullyadded='1'";
                SqlCommand cmdchek = new SqlCommand(chekeit, conn);
                SqlDataAdapter adptchek = new SqlDataAdapter(cmdchek);
                DataTable dtchek = new DataTable();
                adptchek.Fill(dtchek);
                if (dtchek.Rows.Count > 0)
                {
                    lblmsg1.Text = "You have already added  " + ViewState["ip"].ToString() + " IP address ";
                    lblmsg2.Text = "";
                    lblmsg3.Text = "";                   
                }
                else
                {

                    string strhour = "Select * from Ipaddresschangerequesttbl where Id='" + ViewState["maxid"].ToString() + "'";
                    SqlCommand cmdhour = new SqlCommand(strhour, conn);
                    SqlDataAdapter adphour = new SqlDataAdapter(cmdhour);
                    DataTable dthour = new DataTable();
                    adphour.Fill(dthour);

                   TimeSpan t1 = (Convert.ToDateTime(dthour.Rows[0]["datetimeemailgenerated"].ToString()) - Convert.ToDateTime((System.DateTime.Now.ToString())));
                  

                    if (t1.Hours <= -48)
                    {
                       
                        lblmsg1.Text = "You can not use this link as this link is valid up to 48 Hours from the time you you have generated the request. Please do the ip address add process further. ";
                        lblmsg2.Text = "";
                        lblmsg3.Text = "";
                    }

                    else
                    {

                    string strfinal = "select * from CompanyMaster where Compid='" + cid + "'";
                    SqlDataAdapter adp = new SqlDataAdapter(strfinal, conn);
                    DataTable ds = new DataTable();
                    adp.Fill(ds);
                    if (ds.Rows.Count > 0)
                    {
                        ViewState["CompanyName"] = ds.Rows[0]["CompanyName"].ToString();

                        if (option.ToString() == "0")
                        {
                            pnloptionforcompany.Visible = false;
                            pnloption1user.Visible = true;
                            lbloption1companyname.Text = ViewState["CompanyName"].ToString();
                            lbloption1username.Text = ViewState["uid"].ToString();



                            string stripmasterid = " select * from IpControlMastertbl where CID='" + ViewState["cid"] + "' ";
                            SqlDataAdapter adpipmasterid = new SqlDataAdapter(stripmasterid, conn);
                            DataTable dsipmasterid = new DataTable();
                            adpipmasterid.Fill(dsipmasterid);

                            if (dsipmasterid.Rows.Count > 0)
                            {
                                string strinsert = "update Ipaddresschangerequesttbl set datetimeemailverified='" + System.DateTime.Now.ToString() + "' ,verifieduserid='" + ViewState["uid"].ToString() + "',ipaddresssuceessfullyadded='1' where Id='" + ViewState["maxid"].ToString() + "'";
                                SqlCommand cmdinsert = new SqlCommand(strinsert, conn);
                                if (conn.State.ToString() != "Open")
                                {
                                    conn.Open();
                                }
                                cmdinsert.ExecuteNonQuery();
                                conn.Close();



                                string strdupliip = " select * from IpControldetailtbl where IpcontrolId='" + dsipmasterid.Rows[0]["IpcontrolId"].ToString() + "' and Cidwise='0' and Userwise='1' and UserId='" + ViewState["uid"].ToString() + "' and Ipaddress='" + ViewState["ip"].ToString() + "'   ";
                                SqlDataAdapter adpdupliip = new SqlDataAdapter(strdupliip, conn);
                                DataTable dsdupliip = new DataTable();
                                adpdupliip.Fill(dsdupliip);

                                if (dsdupliip.Rows.Count > 0)
                                {
                                    lblmsg1.Text = "The following IP address";
                                    lblmsg2.Text = ViewState["ip"].ToString();
                                    lblmsg3.Text = ", is already on the list of allowed IP addresses to access the website. ";



                                }
                                else
                                {
                                    string insertip = "insert into IpControldetailtbl (IpcontrolId,Cidwise,Userwise,UserId,Ipaddress) values ('" + dsipmasterid.Rows[0]["IpcontrolId"].ToString() + "','0','1','" + ViewState["uid"].ToString() + "','" + ViewState["ip"].ToString() + "')  ";

                                    SqlCommand cmdinsertip = new SqlCommand(insertip, conn);
                                    if (conn.State.ToString() != "Open")
                                    {
                                        conn.Open();
                                    }
                                    cmdinsertip.ExecuteNonQuery();
                                    conn.Close();


                                    lblmsg1.Text = "The following IP address";
                                    lblmsg2.Text = ViewState["ip"].ToString();
                                    lblmsg3.Text = ", has been successfully added to access the website ";

                                    mailgeneteate();
                                   


                                }
                            }

                        }
                        else if (option.ToString() == "1")
                        {
                            pnloptionforcompany.Visible = true;
                            pnloption1user.Visible = false;

                            lblcompanyname.Text = ViewState["CompanyName"].ToString();


                            string strinsert = "update Ipaddresschangerequesttbl set datetimeemailverified='" + System.DateTime.Now.ToString() + "' ,verifieduserid='" + ViewState["uid"].ToString() + "',ipaddresssuceessfullyadded='1' where Id='" + ViewState["maxid"].ToString() + "'";
                            SqlCommand cmdinsert = new SqlCommand(strinsert, conn);
                            if (conn.State.ToString() != "Open")
                            {
                                conn.Open();
                            }
                            cmdinsert.ExecuteNonQuery();
                            conn.Close();

                            string stripmasterid = " select * from IpControlMastertbl where CID='" + ViewState["cid"] + "' ";
                            SqlDataAdapter adpipmasterid = new SqlDataAdapter(stripmasterid, conn);
                            DataTable dsipmasterid = new DataTable();
                            adpipmasterid.Fill(dsipmasterid);

                            if (dsipmasterid.Rows.Count > 0)
                            {


                                string strdupliip = " select * from IpControldetailtbl where IpcontrolId='" + dsipmasterid.Rows[0]["IpcontrolId"].ToString() + "' and Cidwise='1' and Userwise='0' and UserId='0' and Ipaddress='" + ViewState["ip"] + "'   ";
                                SqlDataAdapter adpdupliip = new SqlDataAdapter(strdupliip, conn);
                                DataTable dsdupliip = new DataTable();
                                adpdupliip.Fill(dsdupliip);

                                if (dsdupliip.Rows.Count > 0)
                                {
                                    lblmsg1.Text = "The following IP address";
                                    lblmsg2.Text = ViewState["ip"].ToString();
                                    lblmsg3.Text = ", is already on the list of allowed IP addresses to access the website. ";



                                }
                                else
                                {
                                    string insertip = "insert into IpControldetailtbl (IpcontrolId,Cidwise,Userwise,UserId,Ipaddress) values ('" + dsipmasterid.Rows[0]["IpcontrolId"].ToString() + "','1','0','0','" + ViewState["ip"] + "')  ";

                                    SqlCommand cmdinsertip = new SqlCommand(insertip, conn);
                                    if (conn.State.ToString() != "Open")
                                    {
                                        conn.Open();
                                    }
                                    cmdinsertip.ExecuteNonQuery();
                                    conn.Close();


                                    lblmsg1.Text = "The following IP address";
                                    lblmsg2.Text = ViewState["ip"].ToString();
                                    lblmsg3.Text = ", has been successfully added to access the website ";

                                    mailgeneteate();


                                }
                            }

                        }
                        else
                        {
                            lblmsg1.Text = "Sorry worng data provided.";
                            lblmsg2.Text = "";
                            lblmsg3.Text = "";

                        }

                    }
                }
                }
            }



        }

    }
    public void sendmail(string To, string whid, string MasterEmailId, string MasterEmailIdpassword, string OutGoingMailServer, string Displayname)
    {
        string strbus = "select Name from WarehouseMaster where WarehouseID='" + whid.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strbus, conn);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["Name"]) != "")
            {
                ViewState["buss"] = Convert.ToString(dt.Rows[0]["Name"]);
            }
            else
            {
                ViewState["buss"] = "";
            }
        }

        string ADDRESSEX = "SELECT distinct CompanyMaster.CompanyLogo, CompanyMaster.CompanyName,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsitMaster.OutGoingMailServer, CompanyWebsitMaster.EmailSentDisplayName,CompanyWebsitMaster.SiteUrl,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Phone1, CompanyWebsiteAddressMaster.Phone2, CompanyWebsiteAddressMaster.TollFree1, CompanyWebsiteAddressMaster.Fax,CompanyWebsiteAddressMaster.Email,CompanyMaster.CompanyId,CompanyWebsitMaster.WHid FROM  CompanyMaster LEFT OUTER JOIN AddressTypeMaster RIGHT OUTER JOIN CompanyWebsiteAddressMaster ON AddressTypeMaster.AddressTypeMasterId = CompanyWebsiteAddressMaster.AddressTypeMasterId RIGHT OUTER JOIN CompanyWebsitMaster ON CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = CompanyWebsitMaster.CompanyWebsiteMasterId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId where CompanyMaster.Compid='" + ViewState["cid"] + "' and WHId='" + whid.ToString() + "'";
        SqlCommand cmd = new SqlCommand(ADDRESSEX, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        StringBuilder HeadingTable = new StringBuilder();
        HeadingTable.Append("<table width=\"100%\"> ");

        SqlDataAdapter dalogo = new SqlDataAdapter("select logourl from CompanyWebsitMaster where whid='" + whid.ToString() + "'", conn);
        DataTable dtlogo = new DataTable();
        dalogo.Fill(dtlogo);

        HeadingTable.Append("<tr><td width=\"50%\" style=\"padding-left:10px\" align=\"left\" > <img src=\"../images/" + dtlogo.Rows[0]["logourl"].ToString() + "\" \"border=\"0\" Width=\"200px\" Height=\"125px\" / > </td><td style=\"padding-left:100px\" width=\"50%\" align=\"left\"><b><span style=\"color: #996600\">" + ds.Rows[0]["CompanyName"].ToString() + "</span></b><Br>" + ds.Rows[0]["Address1"].ToString() + "<Br>" + ds.Rows[0]["Address2"].ToString() + "<Br>Toll Free : " + ds.Rows[0]["TollFree1"].ToString() + "<Br>Phone : " + ds.Rows[0]["Phone1"].ToString() + "<Br>Fax :" + ds.Rows[0]["Fax"].ToString() + "<Br>Email :" + ds.Rows[0]["Email"].ToString() + "<Br>Website :" + ds.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");

        HeadingTable.Append("</table> ");

        string welcometext = getWelcometext();      

        string currentdate = " <span style=\"font-size: 10pt; color: #000000; font-family: Arial\">" + System.DateTime.Now.ToShortDateString() + " </span>";

        string AccountInfo1 = "<span style=\"font-size: 10pt; color: #000000; font-family: Arial\"> Your recent request to add " + ViewState["ip"] + " to the list of allowed IP address for you has been approved by the administrator of " + ViewState["buss"].ToString() + ".</span>";

        string str1 = " <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"> You will now be able to <a href=http://" + Request.Url.Host.ToString() + " > login </a> from this IP address. </span>";

        string body = "<br>" + HeadingTable + "<br>Dear <strong><span style=\"color: #996600\"> " + ViewState["buss"].ToString() + " </span></strong> ,<br><br>" + welcometext.ToString() + " <br>" + AccountInfo1.ToString() + "<br> " + str1 + "  <br> <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"><br>Thank you, <br><br>Security Department<br> " + ViewState["buss"].ToString() + "<Br>" + ds.Rows[0]["Address1"].ToString() + "<Br>" + ds.Rows[0]["Address2"].ToString() + "<Br>Toll Free : " + ds.Rows[0]["TollFree1"].ToString() + "<Br>Phone : " + ds.Rows[0]["Phone1"].ToString() + "<Br>Fax :" + ds.Rows[0]["Fax"].ToString() + "<Br>Email :" + ds.Rows[0]["Email"].ToString() + "<Br>Website :" + ds.Rows[0]["SiteUrl"].ToString() + "</span><br><strong><span style=\"color: #996600\"> ";

        MailAddress to = new MailAddress(To);

        MailAddress from = new MailAddress("" + MasterEmailId + "", "" + Displayname + "");

        MailMessage objEmail = new MailMessage(from, to);

        objEmail.Subject = "IP Address Approval - " + ViewState["buss"].ToString() + " ";
        objEmail.Body = body.ToString();
        objEmail.IsBodyHtml = true;
        objEmail.Priority = MailPriority.Normal;
        SmtpClient client = new SmtpClient();
        client.Credentials = new NetworkCredential("" + MasterEmailId + "", "" + MasterEmailIdpassword + "");
        client.Host = OutGoingMailServer;
        client.Send(objEmail);





    }

    public string getWelcometext()
    {


        string str = "SELECT EmailContentMaster.EmailContent, EmailContentMaster.EntryDate, CompanyWebsitMaster.SiteUrl, EmailTypeMaster.EmailTypeId " +
                    " FROM CompanyWebsitMaster INNER JOIN " +
                      " EmailContentMaster ON CompanyWebsitMaster.CompanyWebsiteMasterId = EmailContentMaster.CompanyWebsiteMasterId INNER JOIN " +
                      " EmailTypeMaster ON EmailContentMaster.EmailTypeId = EmailTypeMaster.EmailTypeId " +
                    " WHERE     (EmailTypeMaster.EmailTypeId = 1)  and (EmailTypeMaster.Compid='" + ViewState["cid"] + "')" +
                    " ORDER BY EmailContentMaster.EntryDate DESC ";
        SqlCommand cmd = new SqlCommand(str, conn);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        string welcometext = "";
        if (ds.Rows.Count > 0)
        {
            welcometext = ds.Rows[0]["EmailContent"].ToString();

        } return welcometext;

    }

    protected void mailgeneteate()
    {

        string stremployeeemailid = " select EmployeeMaster.* from EmployeeMaster inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID  inner join User_master on User_master.PartyID=Party_master.PartyID  where Party_master.id='" + ViewState["cid"] + "' and User_master.UserID='" + ViewState["uid"].ToString() + "' ";
        SqlDataAdapter adpemployeeemailid = new SqlDataAdapter(stremployeeemailid, conn);
        DataTable dsemployeeemailid = new DataTable();
        adpemployeeemailid.Fill(dsemployeeemailid);

       

        if (dsemployeeemailid.Rows.Count > 0)
        {
            string To = dsemployeeemailid.Rows[0]["Email"].ToString();
            string whid = dsemployeeemailid.Rows[0]["Whid"].ToString();

            if (To != "" && whid != "")
            {
                string strmasteremailid = "select * from CompanyWebsitMaster  where WHId='" + whid + "' ";
                SqlDataAdapter adpmasteremailid = new SqlDataAdapter(strmasteremailid, conn);
                DataTable dsmasteremailid = new DataTable();
                adpmasteremailid.Fill(dsmasteremailid);

                if (dsmasteremailid.Rows.Count > 0)
                {
                    string MasterEmailId = dsmasteremailid.Rows[0]["MasterEmailId"].ToString();
                    string MasterEmailIdpassword = dsmasteremailid.Rows[0]["EmailMasterLoginPassword"].ToString();
                    string OutGoingMailServer = dsmasteremailid.Rows[0]["OutGoingMailServer"].ToString();
                    string Displayname = dsmasteremailid.Rows[0]["EmailSentDisplayName"].ToString();

                    if (MasterEmailId != "" && MasterEmailIdpassword != "" && OutGoingMailServer != "")
                    {
                        sendmail(To, whid, MasterEmailId, MasterEmailIdpassword, OutGoingMailServer, Displayname);
                        

                    }
                }
            }


        }
    }
}
