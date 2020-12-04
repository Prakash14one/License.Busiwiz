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

public partial class DNSentry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection condefaultinstance = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        //string comid = BZ_Common.BZ_Encrypted("zzzzpp");
        if (!IsPostBack)
        {
            if (Request.QueryString["uid"]  != null && Request.QueryString["pwd"] != null && Request.QueryString["cid"] != null)
            {
                //http://license.busiwiz.com/DNSentry.aspx?uid=6CAPiQfUhC7BELibxx9a8Q==&pwd=6CAPiQfUhC7BELibxx9a8Q==&cid=6CAPiQfUhC7BELibxx9a8Q==
                string cid = Request.QueryString["cid"].ToString().ToString().Replace(" ", "+");
               // cid = BZ_Common.BZ_Decrypted(cid);

                string uid = Request.QueryString["uid"].ToString().ToString().Replace(" ", "+");
               // uid = BZ_Common.BZ_Decrypted(uid);

                string pwd = Request.QueryString["pwd"].ToString().ToString().Replace(" ", "+");
               // pwd = BZ_Common.BZ_Decrypted(pwd);


                if (Request.QueryString["Id"] != null && Request.QueryString["status"] != null)
                {
                    string idNeedCode = Request.QueryString["Id"].ToString().ToString().Replace(" ", "+");
                   // idNeedCode = BZ_Common.BZ_Encrypted(cid);
                    SqlCommand cmd = new SqlCommand(" update CompanyCreationNeedCode  set DNSAddingStatus='" + Request.QueryString["status"] + "' where id='" + idNeedCode + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }


                string ipaddress = "";// "72.38.84.98";
                string dnsServerName = Environment.MachineName; // c3
                string dnsentryservername = "";//c3.safestserver.com
                DataTable dtserver = MyCommonfile.selectBZ("Select ServerMasterTbl.* from CompanyMaster inner join ServerMasterTbl on ServerMasterTbl.Id=CompanyMaster.ServerId where CompanyMaster.CompanyLoginId='" + cid + "'");   //serverdetail(cid);
                if (dtserver.Rows.Count > 0)
                {
                    ipaddress = dtserver.Rows[0]["ipaddress"].ToString();//Publicip
                    dnsentryservername = dtserver.Rows[0]["Busiwizsatellitesiteurl"].ToString();

                    string sqlservername = dtserver.Rows[0]["sqlurl"].ToString();
                    string defaultinstancename = dtserver.Rows[0]["DefaultsqlInstance"].ToString();
                    string sqlserverport = dtserver.Rows[0]["port"].ToString();
                    string defaultdatabasename = dtserver.Rows[0]["DefaultDatabaseName"].ToString();
                    string sapassword = PageMgmt.Decrypted(dtserver.Rows[0]["Sapassword"].ToString());
                    condefaultinstance = new SqlConnection();

                    condefaultinstance.ConnectionString = @"Data Source =" + sqlservername + "\\" + defaultinstancename + "," + sqlserverport + "; Initial Catalog = " + defaultdatabasename + "; User ID=sa; Password=" + sapassword + "; Persist Security Info=true; ";
                    condefaultinstance.Close();
                   // condefaultinstance.ConnectionString = @"Data Source =192.168.2.100,40000; Initial Catalog = C3SATELLITESERVER; User ID=sa; Password=06De1963++; Persist Security Info=true; ";
                    if (condefaultinstance.State.ToString() != "Open")
                    {
                        condefaultinstance.Open();
                    }
                    condefaultinstance.Close();
                    ipaddress = dtserver.Rows[0]["PublicIp"].ToString();
                    DataTable dtzoneweb = MyCommonfile.selectBZ(" Select Top(1) * From CompanyCreationNeedCode where CodeTypeCategoryId=1 and CompanyLoginId='" + cid + "' and (DNSAddingStatus IS NULL OR  DNSAddingStatus=0)");// SearchDNS(cid);
                    foreach (DataRow dr in dtzoneweb.Rows)
                    {
                        string CodeDetailId = dr["CodeDetailId"].ToString();
                        DataTable DNSWebsiteName = MyCommonfile.selectBZ(" SELECT Top(1) dbo.WebsiteMaster.ID, dbo.ClientMaster.ClientURL,dbo.ServerMasterTbl.Id as sid, dbo.CodeTypeTbl.ID AS CodeTypeID, dbo.ProductCodeDetailTbl.Id AS CodeDetailID, dbo.WebsiteMaster.DNSserver, dbo.WebsiteMaster.DNSname, dbo.ServerMasterTbl.Busiwizsatellitesiteurl,dbo.ServerMasterTbl.PublicIp,dbo.ServerMasterTbl.Ipaddress FROM dbo.VersionInfoMaster INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.WebsiteMaster INNER JOIN dbo.CodeTypeTbl ON dbo.WebsiteMaster.ID = dbo.CodeTypeTbl.WebsiteID INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id INNER JOIN dbo.ServerMasterTbl ON dbo.WebsiteMaster.DNSserver = dbo.ServerMasterTbl.Id ON dbo.VersionInfoMaster.VersionInfoId = dbo.CodeTypeTbl.ProductVersionId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId Where ProductCodeDetailTbl.ID='" + CodeDetailId + "' ");
                        if (DNSWebsiteName.Rows.Count > 0)
                        {
                            string ds_Com_ServerId = DNSWebsiteName.Rows[0]["sid"].ToString();
                            string domainName = DNSWebsiteName.Rows[0]["DNSname"].ToString();
                            string arecord = cid;
                            string ipDestination = DNSWebsiteName.Rows[0]["PublicIp"].ToString();
                            string ipaddres = DNSWebsiteName.Rows[0]["Ipaddress"].ToString();
                            string Comp_serverweburl = DNSWebsiteName.Rows[0]["Busiwizsatellitesiteurl"].ToString();
                            string sateliteserversilentpagerequesttblid = "0";
                            //bool update = CompanyRelated.Update_CompanyCreationNeedCode(dr["Id"].ToString(), true);

                            string pagename = "http://" + Comp_serverweburl + "/Satelliteservfunction.aspx?domainName=" + BZ_Common.BZ_Encrypted(domainName) + "&arecord=" + BZ_Common.BZ_Encrypted(arecord) + "&ipDestination=" + BZ_Common.BZ_Encrypted(ipDestination) + "&ipaddres=" + BZ_Common.BZ_Encrypted(ipaddres) + "&sateliteserversilentpagerequesttblid=" + sateliteserversilentpagerequesttblid + "&uid=" + Request.QueryString["uid"] + "&pwd=" + Request.QueryString["pwd"] + "&cid=" + Request.QueryString["cid"] + "&Id=" + dr["Id"].ToString() + "";
                            // Response.Redirect("" + pagename + "");

                            string ClientURL = DNSWebsiteName.Rows[0]["ClientURL"].ToString() + "//Silent_CompanyCreationDNSInfoUpdate.aspx?p2=" + dr["Id"].ToString();
                            string pagenamemainY = "Satelliteservfunction.aspx?domainName=" + BZ_Common.BZ_Encrypted(domainName) + "&arecord=" + BZ_Common.BZ_Encrypted(arecord) + "&ipDestination=" + BZ_Common.BZ_Encrypted(ipDestination) + "&ipaddres=" + BZ_Common.BZ_Encrypted(ipaddres) + "&sateliteserversilentpagerequesttblid=" + BZ_Common.BZ_Encrypted(sateliteserversilentpagerequesttblid) + "&uid=" + Request.QueryString["uid"] + "&pwd=" + Request.QueryString["pwd"] + "&cid=" + Request.QueryString["cid"] + "&Id="+dr["Id"].ToString()+"";
                            //Page X
                            string mycurrenturlX = Request.Url.AbsoluteUri;
                            Random random = new Random();
                            int randomNumber = random.Next(1, 10);
                            string Randomkeyid = Convert.ToString(randomNumber);
                            string SilentPageRequestTblID = CompanyRelated.Insert_SilentPageRequestTbl(ds_Com_ServerId, pagenamemainY, DateTime.Now.ToString(), "", false, Randomkeyid, mycurrenturlX);
                            string url = "";
                            //Decrypted_DasdasdasdadynamicKey
                            url = "http://" + Comp_serverweburl + "/vfysrv.aspx?licensesilentpagerequesttblid=" + BZ_Common.Encrypted_DasdasdasdadynamicKey(SilentPageRequestTblID, Randomkeyid) + "&pageredirecturl=" + BZ_Common.Encrypted_DasdasdasdadynamicKey(pagenamemainY, Randomkeyid) + "&mstrsrvky=" + BZ_Common.Encrypted_DasdasdasdadynamicKey(BZ_Common.satsrvencryky(), Randomkeyid) + "&returnurl=" +ClientURL+"&rnkendecke=" + Randomkeyid + "";
                            Response.Redirect("" + pagename + "");

                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + pagename + "');", true);
                        }
                        else
                        {
                            lblmsg.Text = "Need to refress again ";
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "No Record found for " + cid + " compay";
                }
                DataTable dtzoneweb1 = MyCommonfile.selectBZ(" Select Top(1) * From CompanyCreationNeedCode where AdditionalPageInserted=1 and CompanyLoginId='" + cid + "'");// SearchDNS(cid);
                foreach (DataRow dr in dtzoneweb1.Rows)
                {
                    string CodeDetailId = dr["CodeDetailId"].ToString();
                    string DNSname = dr["DNSname"].ToString();
                    DataTable DNSWebsiteName2 = MyCommonfile.selectBZ(" SELECT Top(1) dbo.WebsiteMaster.ID, dbo.ClientMaster.ClientURL,dbo.ServerMasterTbl.Id as sid, dbo.CodeTypeTbl.ID AS CodeTypeID, dbo.ProductCodeDetailTbl.Id AS CodeDetailID, dbo.WebsiteMaster.DNSserver, dbo.WebsiteMaster.DNSname, dbo.ServerMasterTbl.Busiwizsatellitesiteurl,dbo.ServerMasterTbl.PublicIp,dbo.ServerMasterTbl.Ipaddress FROM dbo.VersionInfoMaster INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.WebsiteMaster INNER JOIN dbo.CodeTypeTbl ON dbo.WebsiteMaster.ID = dbo.CodeTypeTbl.WebsiteID INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id INNER JOIN dbo.ServerMasterTbl ON dbo.WebsiteMaster.DNSserver = dbo.ServerMasterTbl.Id ON dbo.VersionInfoMaster.VersionInfoId = dbo.CodeTypeTbl.ProductVersionId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId Where ProductCodeDetailTbl.ID='" + CodeDetailId + "' ");
                    DataTable DNSWebsiteName = MyCommonfile.selectBZ(" SELECT dbo.CompanyMaster.CompanyName, dbo.CompanyMaster.YourDomaiUrl,  dbo.CompanyMaster.CompanyLoginId, dbo.PortalMasterTbl.PortalName FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID INNER JOIN dbo.PortalMasterTbl ON dbo.Priceplancategory.PortalId = dbo.PortalMasterTbl.Id where  CompanyMaster.CompanyLoginId='" + cid + "'");
                    if (DNSWebsiteName.Rows.Count > 0)
                    {
                        string PortalName = DNSWebsiteName.Rows[0]["PortalName"].ToString();

                    }
                }
             
                //string str = " SELECT CompanyMaster.*, PortalMasterTbl.PortalName,PricePlanMaster.VersionInfoMasterId,PricePlanMaster.Producthostclientserver from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join PortalMasterTbl on PricePlanMaster.PortalMasterId1 = PortalMasterTbl.id where CompanyLoginId='" + cid + "' ";
                //SqlCommand cmd = new SqlCommand(str, con);
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //DataTable ds = new DataTable();
                //adp.Fill(ds);
                //string portalnamezone = ds.Rows[0]["PortalName"].ToString();                
                //DataTable dtzone = companydetail(cid);
                //if (dtzone.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dtzone.Rows)
                //    {
                //        string zone = dr["WebsiteURL"].ToString();//
                //        try
                //        {
                //            AddARecord(cid.ToString(), zone, ipaddress, dnsServerName, dnsentryservername);
                //            AddARecordportal(cid.ToString(), portalnamezone, ipaddress, dnsServerName, dnsentryservername);
                //        }
                //        catch
                //        {
                //        }
                //    }
                //}
                //DataTable dtmainurl = maincompanyurl(cid);
                //if (dtmainurl.Rows.Count > 0)
                //{
                //    string compnayurl = dtmainurl.Rows[0]["CodeType"].ToString();                  
                //}
                insertstatus("8", "1", cid);
                Response.Redirect("http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + PageMgmt.Encrypted(cid) + "");
            }
            else
            {
                lblmsg.Text = "Need to refress again ";
            }
        }
    }


    protected DataTable SearchDNS(string cid)
    {
        string str132 = " Select * From CompanyCreationNeedCode where CodeTypeCategoryId=1 and CompanyLoginId='" + cid + "' and (DNSAddingStatus IS NULL OR  DNSAddingStatus=0)";
        SqlCommand cgw = new SqlCommand(str132, condefaultinstance);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);
        return dt;
    }

    public void AddARecord(string hostName, string zone, string iPAddress, string dnsServerName, string ServerName)
    {
        //ManagementScope scope = new ManagementScope(@"\\" + dnsServerName + "\\root\\MicrosoftDNS");
        //scope.Connect();
        //ManagementClass cmiClass = new ManagementClass(scope, new ManagementPath("MicrosoftDNS_AType"), null);
        //ManagementBaseObject inParams = cmiClass.GetMethodParameters("CreateInstanceFromPropertyData");                                         
        //inParams["DnsServerName"] = ServerName;
        //inParams["ContainerName"] = zone;
        //inParams["OwnerName"] = zone;// hostName + "." + zone;
        //inParams["IPAddress"] = iPAddress;
        //cmiClass.InvokeMethod("CreateInstanceFromPropertyData", inParams, null);      
    }
    public void AddARecordportal(string hostName, string zoneportalname, string iPAddress, string dnsServerName, string ServerName)
    {
        //ManagementScope scope = new ManagementScope(@"\\" + dnsServerName + "\\root\\MicrosoftDNS");
        //scope.Connect();
        //ManagementClass cmiClass = new ManagementClass(scope, new ManagementPath("MicrosoftDNS_AType"), null);
        //ManagementBaseObject inParams = cmiClass.GetMethodParameters("CreateInstanceFromPropertyData");
        //inParams["DnsServerName"] = ServerName;
        //inParams["ContainerName"] = zoneportalname;
        //inParams["OwnerName"] = hostName + "." + zoneportalname;
        //inParams["IPAddress"] = iPAddress;
        //cmiClass.InvokeMethod("CreateInstanceFromPropertyData", inParams, null);
       
    }
    protected DataTable companydetail(string cid)
    {
        string str132 = " select * from CompanyWebsiteDetailTbl where CompanyLoginId='" + cid + "'";
        SqlCommand cgw = new SqlCommand(str132, condefaultinstance);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);
        return dt;
    }
   
    protected DataTable maincompanyurl(string cid)
    {
        string str132 = " select * from CompanyWebsiteDetailTbl  where CompanyWebsiteDetailTbl.CompanyLoginId='" + cid + "' and CodeType='onlineaccounts.net' OR (AdditionalPageInserted=1) ";
        SqlCommand cgw = new SqlCommand(str132, condefaultinstance);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);
        return dt;
    }
    protected void insertstatus(string statusid, string status,string cid)
    {
        string strmaincmp = " SELECT * from Companycreationstatustbl where StatusMasterId='" + statusid + "' and Status='" + status + "' and CompanyID='" + cid + "'   ";
        SqlCommand cmdmaincmp = new SqlCommand(strmaincmp, con);
        SqlDataAdapter adpmaincmp = new SqlDataAdapter(cmdmaincmp);
        DataTable dsmaincmp = new DataTable();
        adpmaincmp.Fill(dsmaincmp);
        if (dsmaincmp.Rows.Count == 0)
        {
            string str = " insert into Companycreationstatustbl(StatusMasterId,Status,DateTime,CompanyID) values ('" + statusid + "','" + status + "','" + DateTime.Now.ToString() + "','" + cid + "') ";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
