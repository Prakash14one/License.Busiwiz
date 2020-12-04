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
using System.DirectoryServices;
using System.IO.Compression;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using System.Net.Mail;


public partial class Banrelease : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection connserver;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {

            fillddlcompany();
            fillddlcomputer();
            fillddlpublicip();
            fillddlcompip();
            fillgrid();

        }


    }

    protected void fillgrid()
    {
        string str = "select *,case when ( bannedmacaddress='0') then 'No' else 'Yes' end as bannedmacaddressdisplay,case when ( bannedipaddress='0') then 'No' else 'Yes' end as bannedipaddressdisplay from Permenatalybanmacaddressandipadress where Id<>'' ";//case when ( BanIsActive='0') then 'Inactive' else 'Active' end as BanIsActivedisplay
        string strcid = "";
        string strcomputer = "";
        string strpubip = "";
        string strpriip = "";
        string strbanstatus = "";
        string strdatetime = "";

        if (ddlcompanylist.SelectedIndex > 0)
        {
            strcid = " and compid='" + ddlcompanylist.SelectedValue + "'";
        }

        if (ddlcomputername.SelectedIndex > 0)
        {
            strcomputer = " and computername =" + ddlcomputername.SelectedValue + "";
        }

        if (ddlpublicipaddress.SelectedIndex > 0)
        {
            strpubip = " and Ipaddress = " + ddlpublicipaddress.SelectedValue + " ";
        }

        if (ddlprivateip.SelectedIndex > 0)
        {
            strpriip = " and computerip=" + ddlprivateip.SelectedValue + " ";
        }

        if (ddlbanstatus.SelectedValue != "2")
        {
            strbanstatus = " and BanIsActive=" + ddlbanstatus.SelectedValue + " ";
        }
        if (txtFromDate.Text.Length > 0 && txtToDate.Text.Length > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
        {
            strdatetime = " and DateTime between '" + txtFromDate.Text + "' and '" + txtFromDate.Text + "' ";
        }


        string finalstr = str + strcid + strcomputer + strpubip + strpriip + strbanstatus + strdatetime;

        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();

       
    }
    protected void fillddlcompany()
    {
        ddlcompanylist.Items.Clear();
        string str = "select distinct compid  from Permenatalybanmacaddressandipadress where compid!='' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlcompanylist.DataSource = dt;
            ddlcompanylist.DataTextField = "compid";
            ddlcompanylist.DataValueField = "compid";
            ddlcompanylist.DataBind();
        }
        ddlcompanylist.Items.Insert(0, "All");
        ddlcompanylist.Items[0].Value = "0";
    }
    protected void fillddlcomputer()
    {
        ddlcomputername.Items.Clear();
        string str = "select distinct computername  from Permenatalybanmacaddressandipadress where computername !='' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlcomputername.DataSource = dt;
            ddlcomputername.DataTextField = "computername";
            ddlcomputername.DataValueField = "computername";
            ddlcomputername.DataBind();
        }
        ddlcomputername.Items.Insert(0, "All");
        ddlcomputername.Items[0].Value = "0";
        
    }
    protected void fillddlpublicip()
    {
        ddlpublicipaddress.Items.Clear();
        string str = "select distinct Ipaddress from Permenatalybanmacaddressandipadress where Ipaddress !='' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlpublicipaddress.DataSource = dt;
            ddlpublicipaddress.DataTextField = "Ipaddress";
            ddlpublicipaddress.DataValueField = "Ipaddress";
            ddlpublicipaddress.DataBind();
        }
        ddlpublicipaddress.Items.Insert(0, "All");
        ddlpublicipaddress.Items[0].Value = "0";



    }
    protected void fillddlcompip()
    {
        ddlprivateip.Items.Clear();
        string str = "select distinct computerip  from Permenatalybanmacaddressandipadress where computerip !='' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlprivateip.DataSource = dt;
            ddlprivateip.DataTextField = "computerip";
            ddlprivateip.DataValueField = "computerip";
            ddlprivateip.DataBind();
        }
        ddlprivateip.Items.Insert(0, "All");
        ddlprivateip.Items[0].Value = "0";

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(e.CommandArgument);

            string str = "select * from Permenatalybanmacaddressandipadress where Id='" + id + "'  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                string MacAddress = dt.Rows[0]["MacAddress"].ToString();
                string Ipaddress = dt.Rows[0]["Ipaddress"].ToString();
                string computerip = dt.Rows[0]["computerip"].ToString();
                string computername = dt.Rows[0]["computername"].ToString();
                string bannedmacaddress = dt.Rows[0]["bannedmacaddress"].ToString();
                string bannedipaddress = dt.Rows[0]["bannedipaddress"].ToString();
                string compid = dt.Rows[0]["compid"].ToString();
                string BanIsActive = dt.Rows[0]["BanIsActive"].ToString();

                string strftpdetail = " SELECT * from ServerMasterTbl where Status='1'";
                SqlCommand cmdftpdetail = new SqlCommand(strftpdetail, con);
                DataTable dtftpdetail = new DataTable();
                SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
                adpftpdetail.Fill(dtftpdetail);
                if (dtftpdetail.Rows.Count > 0)
                {
                    string strlicense = " update Permenatalybanmacaddressandipadress set BanIsActive='0',bannedipaddress='0',bannedmacaddress='0' where  Id='" + id + "'";// MacAddress='" + MacAddress + "' and Ipaddress='" + Ipaddress + "' and computerip='" + computerip + "' and computername='" + computername + "' and bannedmacaddress='" + bannedmacaddress + "' and bannedipaddress='" + bannedipaddress + "' and BanIsActive='1' ";
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
                            string strupdate = "  update Permenatalybanmacaddressandipadress set BanIsActive='0',bannedipaddress='0',bannedmacaddress='0' where  Id='" + id + "'";// MacAddress='" + MacAddress + "' and Ipaddress='" + Ipaddress + "' and computerip='" + computerip + "' and computername='" + computername + "' and bannedmacaddress='" + bannedmacaddress + "' and bannedipaddress='" + bannedipaddress + "' and BanIsActive='1' ";
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
            fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Successfully Release Ban"; 
        }
    }

    public void sendmailformac(string cid,string computername)
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
            strplan.Append("<tr><td align=\"left\">ban of computer - " + computername + " for " + dt.Rows[0]["PortalName"].ToString() + " regarding company " + dt.Rows[0]["CountryName"].ToString() + " has been approved by the <portal> support team.</td></tr> ");
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

                MailAddress to = new MailAddress(dt.Rows[0]["CustEmail"].ToString());
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
            strplan.Append("<tr><td align=\"left\">ban of ip address - " + ipaddress + " for " + dt.Rows[0]["PortalName"].ToString() + " regarding company " + dt.Rows[0]["CountryName"].ToString() + " has been approved by the <portal> support team.</td></tr> ");
            strplan.Append("<tr><td align=\"left\"> The user would now be able to login from the ip address " + ipaddress + "</td></tr> ");
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

                MailAddress to = new MailAddress(dt.Rows[0]["CustEmail"].ToString());
                MailAddress from = new MailAddress(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["EmailDisplayname"].ToString());
                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = "Release of ban of ip address -" + ipaddress + " for " + dt.Rows[0]["PortalName"].ToString() + " regarding company - " + dt.Rows[0]["CompanyName"].ToString() + "";
                objEmail.Body = bodyformate.ToString();
                objEmail.IsBodyHtml = true;
                objEmail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["Password"].ToString());
                client.Host = dt.Rows[0]["Mailserverurl"].ToString();
                client.Send(objEmail);
            }
            catch (Exception e)
            {

            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblbanstatus = (Label)e.Row.FindControl("lblbanstatus");
            Button Button2 = (Button)e.Row.FindControl("Button2");

            if (lblbanstatus.Text == "0")
            {

                Button2.Enabled = false;
            }

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;

            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }

        }
        else
        {
            Button3.Text = "Printable Version";
            Button4.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
        }
    }
}

