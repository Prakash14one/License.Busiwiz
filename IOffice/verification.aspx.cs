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

public partial class verification : System.Web.UI.Page
{
    SqlConnection conn;
   
    SqlConnection connectioninsert;
    SqlConnection busiclient;

    protected void Page_Load(object sender, EventArgs e)
    {
        string[] separator1 = new string[] { "." };
        string[] strSplitArr1 = Request.Url.Host.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

        if (strSplitArr1[0].ToString() != "")
        {
            Session["Comid"] = strSplitArr1[0].ToString();
        }
        else
        {
            Session["Comid"] = "jobcenter";
        }
        Session["Comid"] = "jobcenter";
        //Session["Comid"] = "1133";

        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        connectioninsert = PageConn.UserLog();
        PageConn PCX = new PageConn();
        busiclient = PageConn.busclient();

        //string strdeleteoldest = "delete from NonspecificIpforuser ";
        //SqlCommand cmddeleteoldest = new SqlCommand(strdeleteoldest, conn);
        //if (conn.State.ToString() != "Open")
        //{
        //    conn.Open();
        //}
        //cmddeleteoldest.ExecuteNonQuery();
       

        if (!IsPostBack)
        {
            if (Request.QueryString["ip"] != null && Request.QueryString["maxid"] != null && Request.QueryString["name"] != null)
            {


                string ip = Convert.ToString(Request.QueryString["ip"]);
                ViewState["ip"] = ClsEncDesc.Decrypted(ip.ToString());
              

                string str = ViewState["ip"].ToString();

                string firsttwo = str.Substring(0, 2);
                string middletwo = str.Substring(2, str.Length - 4);
                string newonestr = "";
                foreach (char single in middletwo)
                {
                    if (single.ToString() != ":")
                    {
                        newonestr += "*";
                    }
                    else
                    {
                        newonestr += single.ToString();
                    }
                }
                string lasttwo = str.Substring(str.Length - 2, 2);
                string finaldisplay = firsttwo + newonestr + lasttwo;
                ViewState["finaldisplay"] = finaldisplay.ToString();


                string name = Convert.ToString(Request.QueryString["name"]);
                ViewState["name"] = ClsEncDesc.Decrypted(name.ToString());

                string maxid = (Request.QueryString["maxid"]);
                ViewState["maxid"] = maxid.ToString();




                DataTable dtchek = findmaxidrecord(maxid.ToString());

                if (dtchek.Rows.Count > 0)
                {
                    int requesttype = Convert.ToInt32(dtchek.Rows[0]["Requesttype"].ToString());

                    if (requesttype == 1)
                    {
                        DataTable ipstatus = ipaddressverificationstatus(maxid.ToString());

                        if (ipstatus.Rows.Count > 0)
                        {
                            Label4.Text = "This IP address " + ViewState["finaldisplay"].ToString() + " is already in the list of allowed IP addresses. ";
                        }
                        else
                        {

                            DataTable dtuserdetail = finduserdetail(dtchek.Rows[0]["emailgenerateduserid"].ToString());
                            if (dtuserdetail.Rows.Count > 0)
                            {
                                insertipforuser(dtchek.Rows[0]["emailgenerateduserid"].ToString(), ViewState["ip"].ToString(), ViewState["name"].ToString());
                                mailgeneteate(dtchek.Rows[0]["verifieduserid"].ToString(), dtchek.Rows[0]["emailgenerateduserid"].ToString(), dtuserdetail.Rows[0]["Name"].ToString(), requesttype, dtuserdetail.Rows[0]["Email"].ToString(), dtuserdetail.Rows[0]["Compname"].ToString());
                                Label4.Text = "You have successfully added the following IP address to the allowed list.";
                                Label2.Text = dtuserdetail.Rows[0]["Compname"].ToString();
                                Label6.Text = dtuserdetail.Rows[0]["PartType"].ToString();
                                Label8.Text = dtuserdetail.Rows[0]["Username"].ToString() ;
                                Label10.Text = ViewState["finaldisplay"].ToString();
                                Label10.Text = PageMgmt.Decrypted(ViewState["finaldisplay"].ToString());
                              
                            }
                            else
                            {
                             
                            }


                        }
                    }
                    else
                    {
                        DataTable ipstatus = macaddressverificationstatus(maxid.ToString());
                        if (ipstatus.Rows.Count > 0)
                        {
                            Label4.Text = "This computer " + ViewState["finaldisplay"].ToString() + " is already in the list of allowed computers. ";
                        }
                        else
                        {

                            DataTable dtuserdetail = finduserdetail(dtchek.Rows[0]["emailgenerateduserid"].ToString());
                            if (dtuserdetail.Rows.Count > 0)
                            {
                                insertmacforuser(dtchek.Rows[0]["emailgenerateduserid"].ToString(), ViewState["ip"].ToString(), ViewState["name"].ToString());

                                mailgeneteate(dtchek.Rows[0]["verifieduserid"].ToString(), dtchek.Rows[0]["emailgenerateduserid"].ToString(), dtuserdetail.Rows[0]["Name"].ToString(), requesttype, dtuserdetail.Rows[0]["Email"].ToString(), dtuserdetail.Rows[0]["Compname"].ToString());

                                Label4.Text = "You have successfully added the following computer to the allowed list.";

                                Label2.Text = dtuserdetail.Rows[0]["Compname"].ToString();
                                Label6.Text = dtuserdetail.Rows[0]["PartType"].ToString();
                                Label8.Text = dtuserdetail.Rows[0]["Username"].ToString();
                                Label10.Text = ViewState["finaldisplay"].ToString();
                                Label10.Text = PageMgmt.Decrypted(ViewState["finaldisplay"].ToString());
                               

                            }
                            else
                            {
                               

                            }


                        }

                    }



                }

            }



        }

    }
    public void sendmail(string To, string whid, string MasterEmailId, string MasterEmailIdpassword, string OutGoingMailServer, string Displayname, string userid, string empbusinessname, string userbusinessname, int requesttype, string username)
    {



        string ADDRESSEX = "SELECT distinct CompanyMaster.CompanyLogo, CompanyMaster.CompanyName,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsitMaster.OutGoingMailServer, CompanyWebsitMaster.EmailSentDisplayName,CompanyWebsitMaster.SiteUrl,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1, CompanyWebsiteAddressMaster.Phone2, CompanyWebsiteAddressMaster.TollFree1, CompanyWebsiteAddressMaster.Fax,CompanyWebsiteAddressMaster.Email,CompanyMaster.CompanyId,CompanyWebsitMaster.WHid FROM  CompanyMaster LEFT OUTER JOIN AddressTypeMaster RIGHT OUTER JOIN CompanyWebsiteAddressMaster ON AddressTypeMaster.AddressTypeMasterId = CompanyWebsiteAddressMaster.AddressTypeMasterId RIGHT OUTER JOIN CompanyWebsitMaster ON CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = CompanyWebsitMaster.CompanyWebsiteMasterId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId where CompanyWebsitMaster.WHId='" + whid.ToString() + "'";
        SqlCommand cmd = new SqlCommand(ADDRESSEX, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        SqlDataAdapter dalogo = new SqlDataAdapter("select logourl from CompanyWebsitMaster where whid='" + whid.ToString() + "'", conn);
        DataTable dtlogo = new DataTable();
        dalogo.Fill(dtlogo);


        StringBuilder HeadingTable = new StringBuilder();

        HeadingTable.Append("<table width=\"100%\"> ");
        HeadingTable.Append("<tr><td width=\"50%\" style=\"padding-left:0px\" align=\"left\" > <img src=\"http://" + Request.Url.Host.ToString() + "/Shoppingcart/images/" + dtlogo.Rows[0]["logourl"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td></tr>  ");
        HeadingTable.Append("</table> ");

        string currentdate = " <span style=\"font-size: 10pt; color: #000000; font-family: Arial\">Dear " + username + " : " + userbusinessname + "<br> </span>";
        string AccountInfo1 = "";
        string type = "";
        string subjectline = "";

        if (requesttype == 1)
        {
            AccountInfo1 = "<span style=\"font-size: 10pt; color: #000000; font-family: Arial\">Your recent request to add " + ViewState["name"] +" - " + ViewState["ip"].ToString() + " to the list of allowed IP address for you has been approved by the security admin of " + empbusinessname + ".<br></span>";
            type = "Ip Address " + ViewState["name"] + " - " + ViewState["ip"].ToString();
            subjectline = "IP Address  Approval - " + username + " : " + userbusinessname + " ";
        }
        else
        {
            AccountInfo1 = "<span style=\"font-size: 10pt; color: #000000; font-family: Arial\">Your recent request to add " + ViewState["name"] + " - " + ViewState["finaldisplay"].ToString() + " to the list of allowed computer for you has been approved by the security admin of " + empbusinessname + ".<br></span>";
            type = "computer " + ViewState["name"] + " - " + ViewState["finaldisplay"].ToString();
            subjectline = "Computer  Approval - " + username + " : " + userbusinessname + " ";

        }


        string str1 = " <span style=\"font-size: 10pt; color: #000000; font-family: Arial\">When you log in next time, you should be able to <a href=http://" + Request.Url.Host.ToString() + "/Shoppingcart/Admin/Shoppingcartlogin.aspx > login </a> from this " + type + ". </span>";

        string body =  HeadingTable + "<br>" + currentdate + "<br>  " + AccountInfo1.ToString() + "<br>  " + str1 + "  <br> <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"><br>Sincerely, <br>System Security Team <br> " + empbusinessname + " <br><br>" + ds.Rows[0]["Address1"].ToString() + "<br>" + ds.Rows[0]["Address2"].ToString() + "Toll Free:" + ds.Rows[0]["TollFree1"].ToString() + "<br>Phone:" + ds.Rows[0]["Phone1"].ToString() + "<br>Fax:" + ds.Rows[0]["Fax"].ToString() + "<br>Email:" + ds.Rows[0]["Email"].ToString() + "<br>Website:" + ds.Rows[0]["SiteUrl"].ToString() + "</span><br><strong><span style=\"color: #996600\"> ";


        try
        {

            MailAddress to = new MailAddress(To);
            MailAddress from = new MailAddress("" + MasterEmailId + "", "" + Displayname + "");
            MailMessage objEmail = new MailMessage(from, to);
            objEmail.Subject = subjectline.ToString();
            objEmail.Body = body.ToString();
            objEmail.IsBodyHtml = true;
            objEmail.Priority = MailPriority.Normal;
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("" + MasterEmailId + "", "" + MasterEmailIdpassword + "");
            client.Host = OutGoingMailServer;
            client.Send(objEmail);
        }
        catch (Exception ex)
        {
        }

    }

    protected void mailgeneteate(string empid, string userid, string userbusinessname, int requesttype, string To, string username)
    {

        string stremployeeemailid = "select EmployeeMaster.*,WareHouseMaster.Name,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsitMaster.OutGoingMailServer,CompanyWebsitMaster.EmailSentDisplayName from EmployeeMaster inner join CompanyWebsitMaster on CompanyWebsitMaster.WHId=EmployeeMaster.Whid inner join WareHouseMaster on WareHouseMaster.WareHouseId=EmployeeMaster.Whid where EmployeeMaster.EmployeeMasterID='" + empid + "'  ";
        SqlDataAdapter adpemployeeemailid = new SqlDataAdapter(stremployeeemailid, conn);
        DataTable dsemployeeemailid = new DataTable();
        adpemployeeemailid.Fill(dsemployeeemailid);



        if (dsemployeeemailid.Rows.Count > 0)
        {
            //string To = dsemployeeemailid.Rows[0]["Email"].ToString();

            string whid = dsemployeeemailid.Rows[0]["Whid"].ToString();
            string MasterEmailId = dsemployeeemailid.Rows[0]["MasterEmailId"].ToString();
            string MasterEmailIdpassword = dsemployeeemailid.Rows[0]["EmailMasterLoginPassword"].ToString();
            string OutGoingMailServer = dsemployeeemailid.Rows[0]["OutGoingMailServer"].ToString();
            string Displayname = dsemployeeemailid.Rows[0]["EmailSentDisplayName"].ToString();
            string empbusinessname = dsemployeeemailid.Rows[0]["Name"].ToString();


            if (MasterEmailId != "" && MasterEmailIdpassword != "" && OutGoingMailServer != "" && To != "" && whid != "")
            {
                sendmail(To, whid, MasterEmailId, MasterEmailIdpassword, OutGoingMailServer, Displayname, userid, empbusinessname, userbusinessname, requesttype, username);

               
            }
           

        }
       
    }

    protected DataTable findmaxidrecord(string id)
    {
        string chekeit = "Select * from Ipaddresschangerequesttbl where Id='" + id.ToString() + "' ";
        SqlCommand cmdchek = new SqlCommand(chekeit, conn);
        SqlDataAdapter adptchek = new SqlDataAdapter(cmdchek);
        DataTable dtchek = new DataTable();
        adptchek.Fill(dtchek);
        return dtchek;
    }
    protected DataTable ipaddressverificationstatus(string id)
    {
        string chekeit = "Select * from Ipaddresschangerequesttbl where Id='" + id.ToString() + "' and ipaddresssuceessfullyadded='1' ";
        SqlCommand cmdchek = new SqlCommand(chekeit, conn);
        SqlDataAdapter adptchek = new SqlDataAdapter(cmdchek);
        DataTable dtchek = new DataTable();
        adptchek.Fill(dtchek);
        return dtchek;
    }

    protected DataTable macaddressverificationstatus(string id)
    {
        string chekeit = "Select * from Ipaddresschangerequesttbl where Id='" + id.ToString() + "' and MacAddressaddesdsucessfully='1' ";
        SqlCommand cmdchek = new SqlCommand(chekeit, conn);
        SqlDataAdapter adptchek = new SqlDataAdapter(cmdchek);
        DataTable dtchek = new DataTable();
        adptchek.Fill(dtchek);
        return dtchek;
    }

    protected void insertipforuser(string userid, string ipaddress,string computername)
    {
        DataTable dt = findipaddressbyuser(userid, ipaddress);

        if (dt.Rows.Count > 0)
        {
            
        }
        else
        {
            string strinsert = "insert into NonspecificIpforuser (UserId,Ipaddress,ComputerName) values ('" + userid.ToString() + "','" + ipaddress.ToString() + "','" + computername.ToString() + "') ";

            SqlCommand cmdinsert = new SqlCommand(strinsert, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmdinsert.ExecuteNonQuery();
            conn.Close();

            


        }
    }
    protected DataTable findipaddressbyuser(string userid, string ipaddress)
    {
        string strquestion = "Select * from NonspecificIpforuser where UserId='" + userid + "' and Ipaddress='" + ipaddress + "' ";
        SqlCommand cmdquestion = new SqlCommand(strquestion, conn);
        SqlDataAdapter adptquestion = new SqlDataAdapter(cmdquestion);
        DataTable dsquestion = new DataTable();
        adptquestion.Fill(dsquestion);
        return dsquestion;
    }
    protected DataTable finduserdetail(string userid)
    {
        string strquestion = "select Party_master.*,WareHouseMaster.Name,PartytTypeMaster.PartType,User_master.Username from User_master inner join Party_master on Party_master.PartyID=User_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where User_master.UserID='" + userid + "' ";
        SqlCommand cmdquestion = new SqlCommand(strquestion, conn);
        SqlDataAdapter adptquestion = new SqlDataAdapter(cmdquestion);
        DataTable dsquestion = new DataTable();
        adptquestion.Fill(dsquestion);
        return dsquestion;
    }

    protected void insertmacforuser(string userid, string mac,string computername)
    {
        DataTable dt = findmacaddressbyuser(userid, mac);
        if (dt.Rows.Count > 0)
        {
           
        }
        else
        {
            string strinsert = "insert into UserMacAddressMasterTbl (UserId,MacAddress,ComputerRealName) values ('" + userid.ToString() + "','" + mac.ToString() + "','" + computername + "') ";
            SqlCommand cmdinsert = new SqlCommand(strinsert, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmdinsert.ExecuteNonQuery();
            conn.Close();


            

        }
    }

    protected DataTable findmacaddressbyuser(string userid, string mac)
    {
        string strquestion = "Select * from UserMacAddressMasterTbl where UserId='" + userid.ToString() + "' and MacAddress='" + mac.ToString() + "' ";
        SqlCommand cmdquestion = new SqlCommand(strquestion, conn);
        SqlDataAdapter adptquestion = new SqlDataAdapter(cmdquestion);
        DataTable dsquestion = new DataTable();
        adptquestion.Fill(dsquestion);
        return dsquestion;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/Shoppingcartlogin.aspx");
        
    }
}
