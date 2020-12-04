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

public partial class BanProcess : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection connserver;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["a"] != null && Request.QueryString["b"] != null && Request.QueryString["c"] != null && Request.QueryString["d"] != null && Request.QueryString["e"] != null && Request.QueryString["f"] != null && Request.QueryString["g"] != null && Request.QueryString["h"] != null && Request.QueryString["i"] != null)
            {
                string MacAddress = Request.QueryString["a"];
                string Ipaddress = Request.QueryString["b"];
                string computerip = Request.QueryString["c"];
                string computername = Request.QueryString["d"];
                string bannedmacaddress = Request.QueryString["e"];
                string bannedipaddress = Request.QueryString["f"];
                string compid = Request.QueryString["g"];
                string UserNameLastAccess = Request.QueryString["h"];
                string UserLoginLogID = Request.QueryString["i"];
                string BanIsActive = "1";

                string strftpdetail = " SELECT * from ServerMasterTbl where Status='1'";
                SqlCommand cmdftpdetail = new SqlCommand(strftpdetail, con);
                DataTable dtftpdetail = new DataTable();
                SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
                adpftpdetail.Fill(dtftpdetail);

                if (dtftpdetail.Rows.Count > 0)
                {
                    string strlicense = " Insert into Permenatalybanmacaddressandipadress (UserLoginLogID,MacAddress,Ipaddress,computerip,computername,bannedmacaddress,bannedipaddress,compid,DateTime,UserNameLastAccess,BanIsActive) " +
                           " values ('" + UserLoginLogID + "','" + MacAddress + "','" + Ipaddress + "','" + computerip + "','" + computername + "','" + bannedmacaddress + "','" + bannedipaddress + "','" + compid + "','" + DateTime.Now.ToString() + "','" + UserNameLastAccess + "','" + BanIsActive + "') ";
                    SqlCommand cmdlicense = new SqlCommand(strlicense, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdlicense.ExecuteNonQuery();
                    con.Close();


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
                            string strupdate = " Insert into Permenatalybanmacaddressandipadress (UserLoginLogID,MacAddress,Ipaddress,computerip,computername,bannedmacaddress,bannedipaddress,compid,DateTime,UserNameLastAccess,BanIsActive) " +
                            " values ('" + UserLoginLogID + "','" + MacAddress + "','" + Ipaddress + "','" + computerip + "','" + computername + "','" + bannedmacaddress + "','" + bannedipaddress + "','" + compid + "','" + DateTime.Now.ToString() + "','" + UserNameLastAccess + "','" + BanIsActive + "') ";
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

                 
                    if (bannedmacaddress == "1" && bannedipaddress == "0")
                    {
                        Label1.Text = "Unfortunately your Computer has been permanently banned. You may contact your company administrator to remove the ban.";
                    }
                    if (bannedmacaddress == "0" && bannedipaddress == "1")
                    {
                        Label1.Text = "Unfortunately your IP Address has been permanently banned. You may contact your company administrator to remove the ban.";
                        
                    }
                    




                }

            }
        }
    }

}
