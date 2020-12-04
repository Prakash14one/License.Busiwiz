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
//using System.DirectoryServices;
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
    SqlConnection conser;
    SqlConnection connserver;
    public static string Serverencstr = "";
    public static string Encryptkeycompsss = "";
    public static string encstr = "";
    string ipaddress;
    string banid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["view"] != null)
            {
                ViewState["sortOrder"] = "";
                //fillddlcompany();
                //fillddlcomputer();
                //fillddlpublicip();
                //fillddlcompip();
                createwebsiteandattach(Request.QueryString["CompId"].ToString());

                banid = (DecryptStringServer(Request.QueryString["banid"].ToString()));

                ipaddress = Request.QueryString["view"].ToString();
                ipaddress = ipaddress.Replace(' ', '+');
                ipaddress = DecryptStringServer(ipaddress);
                //ipaddress = (DecryptStringServer(Request.QueryString["view"].ToString()));
                fillDDL();
                fillgrid();
            }
          
        }
    }         
    
    protected void fillDDL()
    {
        string strclnxx = " Select Distinct UserLoginLogID  From Permenatalybanmacaddressandipadress  where Ipaddress='" + ipaddress + "' ";//
        SqlCommand cmd = new SqlCommand(strclnxx, con);
        DataTable dtc = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dtc);
        ddlusername.DataSource = dtc;
        ddlusername.DataValueField = "UserLoginLogID";
        ddlusername.DataTextField = "UserLoginLogID";
        ddlusername.DataBind();
        ddlusername.Items.Insert(0, "Select");
    }
    protected void ddlusername_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void fillgrid()
    {
        string st1 = "";
        if (TextBox1.Text != "" && TextBox2.Text != "")
        {
            // st1 +=" and (VacancyMasterTbl.startdate >= '" + TextBox1.Text + "' and VacancyMasterTbl.startdate <= '" + TextBox2.Text + "')";
            //DateTime sdate = Convert.ToDateTime(TextBox1.Text);
            //DateTime edate = Convert.ToDateTime(TextBox2.Text);
            DateTime sdate1 = Convert.ToDateTime(TextBox1.Text);
            DateTime edate1 = Convert.ToDateTime(TextBox2.Text);
            string sdate = sdate1.GetDateTimeFormats('d')[0];
            string edate = edate1.GetDateTimeFormats('d')[0];

            st1 += "and (dbo.Permenatalybanmacaddressandipadress.DateTime >= CONVERT(DATETIME,'" + sdate + "', 102)) AND ( dbo.Permenatalybanmacaddressandipadress.DateTime <= CONVERT(DATETIME, '" + edate + "', 102))";
           // st1 += " and dbo.Permenatalybanmacaddressandipadress.DateTime between '" + sdate + "'  AND  '" + edate + "'";
        }
        if (ddlusername.SelectedIndex > 0)
        {
            st1 += "and (dbo.Permenatalybanmacaddressandipadress.UserLoginLogID='"+ddlusername.SelectedValue+"') ";
        }

        string str = " select *,case when ( bannedmacaddress='0') then 'No' else 'Yes' end as bannedmacaddressdisplay,case when ( bannedipaddress='0') then 'No' else 'Yes' end as bannedipaddressdisplay from Permenatalybanmacaddressandipadress where Id<>'' ";//case when ( BanIsActive='0') then 'Inactive' else 'Active' end as BanIsActivedisplay
        str = " select *,case when ( bannedmacaddress='0') then 'No' else 'Yes' end as bannedmacaddressdisplay,case when ( bannedipaddress='0') then 'No' else 'Yes' end as bannedipaddressdisplay, dbo.PortalMasterTbl.PortalName FROM            dbo.CompanyMaster INNER JOIN dbo.Permenatalybanmacaddressandipadress ON dbo.CompanyMaster.CompanyLoginId = dbo.Permenatalybanmacaddressandipadress.companyloinid INNER JOIN dbo.PricePlanMaster ON dbo.CompanyMaster.PricePlanId = dbo.PricePlanMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id WHERE (dbo.Permenatalybanmacaddressandipadress.Id <> '')  " + st1 + " Order By  dbo.Permenatalybanmacaddressandipadress.DateTime desc ";//and Permenatalybanmacaddressandipadress.compid='" + Request.QueryString["CompId"].ToString() + "' and  Permenatalybanmacaddressandipadress.Ipaddress='" + ipaddress + "'
        
        string strcid = "";
        string strcomputer = "";
        string strpubip = "";
        string strpriip = "";
        string strbanstatus = "";
        string strdatetime = "";

        //if (ddlcompanylist.SelectedIndex > 0)
        //{
        //    strcid = " and compid='" + ddlcompanylist.SelectedValue + "'";
        //}
        
        //if (ddlcomputername.SelectedIndex > 0)
        //{
        //    strcomputer = " and computername =" + ddlcomputername.SelectedValue + "";
        //}

        //if (ddlpublicipaddress.SelectedIndex > 0)
        //{
        //    strpubip = " and Ipaddress = " + ddlpublicipaddress.SelectedValue + " ";
        //}

        //if (ddlprivateip.SelectedIndex > 0)
        //{
        //    strpriip = " and computerip=" + ddlprivateip.SelectedValue + " ";
        //}

        //if (ddlbanstatus.SelectedValue != "2")
        //{
        //    strbanstatus = " and BanIsActive=" + ddlbanstatus.SelectedValue + " ";
        //}
        //if (txtFromDate.Text.Length > 0 && txtToDate.Text.Length > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
        //{
        //    strdatetime = " and DateTime between '" + txtFromDate.Text + "' and '" + txtFromDate.Text + "' ";
        //}


        string finalstr = str + strcid + strcomputer + strpubip + strpriip + strbanstatus + strdatetime;
        //

        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView1.DataSource = dtcln;

        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataBind();

        if (dtcln.Rows.Count > 0)
        {
            Label2.Visible = false ;
        }
        else
        {
            Label2.Visible = true; 
        }
        
        
        //
        //SqlCommand cmd = new SqlCommand(finalstr, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);
        //GridView1.DataSource = dt;
        //DataView myDataView = new DataView();
        //myDataView = dt.DefaultView;
        //if (hdnsortExp.Value != string.Empty)
        //{
        //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //}
       
        //GridView1.DataBind();

       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
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
                            string strupdate = "  update Permenatalybanmacaddressandipadress set BanIsActive='0' where MacAddress='" + MacAddress + "' and Ipaddress='" + Ipaddress + "' and computerip='" + computerip + "' and computername='" + computername + "' and bannedmacaddress='" + bannedmacaddress + "' and bannedipaddress='" + bannedipaddress + "' and BanIsActive='1'";
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
        }
    }
    protected void createwebsiteandattach(string compid)
    {
        int totnoc = 0;
        string str = " SELECT CompanyMaster.*,PortalMasterTbl.id as portlID, PortalMasterTbl.PortalName,PricePlanMaster.VersionInfoMasterId,PricePlanMaster.Producthostclientserver from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join PortalMasterTbl on PricePlanMaster.PortalMasterId1 = PortalMasterTbl.id where CompanyLoginId='" + compid + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            //string websitename = compid;
            //verid = ds.Rows[0]["VersionInfoMasterId"].ToString();
            //string priceplanid = ds.Rows[0]["PricePlanId"].ToString();
            //string productid = ds.Rows[0]["ProductId"].ToString();
           Session["PortalName"]  = ds.Rows[0]["PortalName"].ToString();
           Session["ProductId"] = ds.Rows[0]["VersionInfoMasterId"].ToString();
            string versionid = ds.Rows[0]["VersionInfoMasterId"].ToString();
            string Serverid = ds.Rows[0]["Serverid"].ToString();
            Encryptkeycompsss = ds.Rows[0]["Encryptkeycomp"].ToString();
            Session["portlID"] = ds.Rows[0]["portlID"].ToString();

            DataTable dtre = selectBZ("select * from ServerMasterTbl where Id='" + Serverid + "'");
           
            string serversqlserverip = dtre.Rows[0]["sqlurl"].ToString();
            string serversqlinstancename = dtre.Rows[0]["DefaultsqlInstance"].ToString();
            string serversqldbname = dtre.Rows[0]["DefaultDatabaseName"].ToString();
            string serversqlpwd = dtre.Rows[0]["Sapassword"].ToString();
            string serversqlport = dtre.Rows[0]["port"].ToString();

            try
            {
                totnoc = 1;
                conser = new SqlConnection();
                conser.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";
                conser = new SqlConnection(@"Data Source =TCP:192.168.6.80,40000; Initial Catalog=C3SATELLITESERVER; User ID=Sa; Password=06De1963++; Persist Security Info=true;");
                if (conser.State.ToString() != "Open")
                {
                    conser.Open();
                }
                conser.Close();
                encstr = "";
                encstr = Convert.ToString(dtre.Rows[0]["Enckey"]);
            }
            catch (Exception e1)
            {

            }
        }

    }

    protected DataTable select(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, conser);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
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

    public static string Encrypted(string strText)
    {
        return Encrypt(strText, Encryptkeycompsss);
    }

    private static string Encrypt(string strtxt, string strtoencrypt)
    {
        byte[] bykey = new byte[20];
        byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bykey = System.Text.Encoding.UTF8.GetBytes(strtoencrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strtxt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, dv), CryptoStreamMode.Write);
            cs.Write(inputArray, 0, inputArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            return strtxt;
            //  throw ex;
        }

    }


    public static string DecryptStringServer(string str)
    {
        return DecryptServServer(str, Encryptkeycompsss);
    }


    private static string DecryptServServer(string strText, string strEncrypt)
    {

        byte[] bKey = new byte[20];
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            return "";
        }

    }

    public static string serverEncrypted(string strText)
    {
        return Encrypt(strText, Serverencstr);

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //if (Button3.Text == "Printable Version")
        //{
        //    Button3.Text = "Hide Printable Version";
        //    Button4.Visible = true;

        //    if (GridView1.Columns[9].Visible == true)
        //    {
        //        ViewState["editHide"] = "tt";
        //        GridView1.Columns[9].Visible = false;
        //    }

        //}
        //else
        //{
        //    Button3.Text = "Printable Version";
        //    Button4.Visible = false;

        //    if (ViewState["editHide"] != null)
        //    {
        //        GridView1.Columns[9].Visible = true;
        //    }
        //}
    }

    //protected void fillddlcompany()
    //{
    //    ddlcompanylist.Items.Clear();
    //    string str = "select distinct compid  from Permenatalybanmacaddressandipadress  ";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlcompanylist.DataSource = dt;
    //        ddlcompanylist.DataTextField = "compid";
    //        ddlcompanylist.DataValueField = "compid";
    //        ddlcompanylist.DataBind();
    //    }
    //    ddlcompanylist.Items.Insert(0, "All");
    //    ddlcompanylist.Items[0].Value = "0";
    //}
    //protected void fillddlcomputer()
    //{
    //    ddlcomputername.Items.Clear();
    //    string str = "select distinct computername  from Permenatalybanmacaddressandipadress  ";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlcomputername.DataSource = dt;
    //        ddlcomputername.DataTextField = "computername";
    //        ddlcomputername.DataValueField = "computername";
    //        ddlcomputername.DataBind();
    //    }
    //    ddlcomputername.Items.Insert(0, "All");
    //    ddlcomputername.Items[0].Value = "0";

    //}
    //protected void fillddlpublicip()
    //{
    //    ddlpublicipaddress.Items.Clear();
    //    string str = "select distinct Ipaddress from Permenatalybanmacaddressandipadress  ";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlpublicipaddress.DataSource = dt;
    //        ddlpublicipaddress.DataTextField = "Ipaddress";
    //        ddlpublicipaddress.DataValueField = "Ipaddress";
    //        ddlpublicipaddress.DataBind();
    //    }
    //    ddlpublicipaddress.Items.Insert(0, "All");
    //    ddlpublicipaddress.Items[0].Value = "0";



    //}
    //protected void fillddlcompip()
    //{
    //    ddlprivateip.Items.Clear();
    //    string str = "select distinct computerip  from Permenatalybanmacaddressandipadress  ";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlprivateip.DataSource = dt;
    //        ddlprivateip.DataTextField = "computerip";
    //        ddlprivateip.DataValueField = "computerip";
    //        ddlprivateip.DataBind();
    //    }
    //    ddlprivateip.Items.Insert(0, "All");
    //    ddlprivateip.Items[0].Value = "0";

    //}
}

