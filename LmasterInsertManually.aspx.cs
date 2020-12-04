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
using System.IO;
using System.Text;
using System.Net;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;

public partial class AccessRight : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static string Serverencstr = "";
    public static string verid = "";
    public static string compid = "";
    public static string Encryptkeycompsss = "";
    SqlConnection condefaultinstance = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //if (Request.QueryString["dow"].ToString() != null)
            //{
            //    
            //    string strupdate = " Update ProductMasterCodeonsatelliteserverTbl set DownloadStart='0' where ID='" + ID + "' ";
            //    SqlCommand cmdupdate = new SqlCommand(strupdate, condefaultinstance);
            //    condefaultinstance.Open();
            //    cmdupdate.ExecuteNonQuery();
            //    condefaultinstance.Close();
            //}
            if (Request.QueryString["Cid"] != null)
            {
                condefaultinstance.ConnectionString = @"Data Source =TCP:192.168.1.219,30000; Initial Catalog = C3SATELLITESERVER; User ID=sa; Password=06De1963++; Persist Security Info=true;";  
                compid = Request.QueryString["Cid"].ToString(); 
                        createwebsiteandattach(Request.QueryString["Cid"]);
                        SqlCommand cmdsel = new SqlCommand("SELECT     CompanyMaster.CompanyId, CompanyMaster.YourDomaiUrl ,CompanyMaster.AdminId,CompanyMaster.Password, CompanyMaster.Websiteurl,CompanyMaster.CompanyLoginId,CompanyMaster.active, PricePlanMaster.CheckIntervalDays, PricePlanMaster.GraceDays, ProductMaster.ProductURL,ProductMaster.ProductName,  " +
                              "CompanyMaster.ProductId, CompanyMaster.PricePlanId, PricePlanMaster.StartDate, PricePlanMaster.EndDate, PricePlanMaster.TrafficinGB,  " +
                              "PricePlanMaster.MaxUser, ProductDetail.VersionNo,ProductMaster.ProductURL,PricePlanMaster.TotalMail, PricePlanMaster.GBUsage,VersionInfoMaster.VersionInfoId ,dbo.PricePlanMaster.PortalMasterId1 " +
                              "FROM         ProductDetail INNER JOIN " +
                              "ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId RIGHT OUTER JOIN " +
                              "CompanyMaster ON ProductMaster.ProductId = CompanyMaster.ProductId LEFT OUTER JOIN  " +
                              "PricePlanMaster ON CompanyMaster.PricePlanId = PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId  " +
                              " WHERE     (CompanyMaster.CompanyLoginId = '" + Request.QueryString["Cid"] + "')", con);

                        SqlDataAdapter dtpsel = new SqlDataAdapter(cmdsel);
                        DataTable dt1 = new DataTable();
                        dtpsel.Fill(dt1);

                if (dt1.Rows.Count > 0)
                {
                            string uid = Encrypted(dt1.Rows[0]["AdminId"].ToString());
                            string pass = Encrypted(dt1.Rows[0]["Password"].ToString());
                            string compidlmaster = Encrypted(dt1.Rows[0]["CompanyLoginId"].ToString());
                            string prodid = Encrypted(dt1.Rows[0]["ProductId"].ToString());
                            string proname = Encrypted(dt1.Rows[0]["ProductName"].ToString());
                            string chdint = Encrypted(dt1.Rows[0]["CheckIntervalDays"].ToString());
                            string gradays = Encrypted(dt1.Rows[0]["GraceDays"].ToString());
                            string runcou = Encrypted(Convert.ToString(1));
                            string StartDate = Encrypted(dt1.Rows[0]["StartDate"].ToString());
                            string EndDate = Encrypted(dt1.Rows[0]["EndDate"].ToString());
                            string MaxUser = Encrypted(dt1.Rows[0]["MaxUser"].ToString());
                            string VersionNo = Encrypted(dt1.Rows[0]["VersionNo"].ToString());
                            string TotalMail = Encrypted(dt1.Rows[0]["TotalMail"].ToString());
                            string GBUsage = Encrypted(dt1.Rows[0]["GBUsage"].ToString());
                            string TrafficinGB = Encrypted(dt1.Rows[0]["TrafficinGB"].ToString());
                            string PricePlanId = Encrypted(dt1.Rows[0]["PricePlanId"].ToString());
                            string VersionInfoId = dt1.Rows[0]["VersionInfoId"].ToString();
                            string PortalMasterId1 = Encrypted(dt1.Rows[0]["PortalMasterId1"].ToString());
                            string YourdomainUrl = dt1.Rows[0]["YourDomaiUrl"].ToString();

                            SqlCommand cmdnew = new SqlCommand("SELECT LicenseMaster.CompanyId, LicenseMaster.LicenseKey,LicenseMaster.LicenseDate,LicenseMaster.LicensePeriod,CompanyMaster.CompanyName, CompanyMaster.AdminId, CompanyMaster.Password,CompanyMaster.Websiteurl,CompanyMaster.active, " +
                                        " HostDetail.SqlServerName, HostDetail.SqlServerUName, HostDetail.SqlServerUPassword, HostDetail.DatabaseName " +
                                        " FROM CompanyMaster LEFT OUTER JOIN " +
                                        " HostDetail ON CompanyMaster.CompanyId = HostDetail.CompanyId LEFT OUTER JOIN " +
                                        " LicenseMaster ON CompanyMaster.CompanyId = LicenseMaster.CompanyId where  CompanyMaster.AdminId ='" + dt1.Rows[0]["AdminId"].ToString() + "' and CompanyMaster.Password='" + dt1.Rows[0]["Password"].ToString() + "' and CompanyMaster.CompanyLoginId='" + compid + "'  and CompanyMaster.active='1'", con);
                            SqlDataAdapter dtp = new SqlDataAdapter(cmdnew);
                            DataTable dtnew = new DataTable();
                            dtp.Fill(dtnew);

                                    if (dtnew.Rows.Count > 0)
                                    {
                                        string LicenseDate = Encrypted(dtnew.Rows[0]["LicenseDate"].ToString());
                                        string LicensePeriod = Encrypted(dtnew.Rows[0]["LicensePeriod"].ToString());
                                        string lickey = Encrypted(dtnew.Rows[0]["LicenseKey"].ToString());
                                        //Insert into Satellite Server

                                        string otherup = " Delete From  LMaster Where CID='" + compidlmaster + "' ";
                                        SqlCommand cmdotherup = new SqlCommand(otherup, condefaultinstance);
                                        if (condefaultinstance.State.ToString() != "Open")
                                        {
                                            condefaultinstance.Open();
                                        }
                                        cmdotherup.ExecuteNonQuery();
                                        SqlCommand cmdinsserverdb = new SqlCommand("Insert Into LMaster(LK,UID,PWD,AT,CID,PID,PN,CHKINTDAYS,GPRDDAYS,LCHKDT,RUNTOT,TRSD,TRED,ATINGB,TRGBPRD,TU,V,MP,LUPD, LSD, LP, VN ,PORTMT,YourDomaiUrl) values " +
                                            "('" + lickey + "','" + uid + "','" + pass + "','" + dt1.Rows[0]["active"].ToString() + "', " +
                                        " '" + compidlmaster + "','" + prodid + "','" + proname + "', " +
                                        " '" + chdint + "','" + gradays + "','" + System.DateTime.Now.ToShortDateString() + "','" + runcou + "','" + StartDate + "', " +
                                        " '" + EndDate + "','" + GBUsage + "','" + TrafficinGB + "','" + MaxUser + "','" + VersionNo + "','" + TotalMail + "','" + PricePlanId + "', '" + LicenseDate + "', '" + LicensePeriod + "','" + VersionInfoId + "', '" + PortalMasterId1 + "','" + YourdomainUrl + "')", condefaultinstance);

                                        if (condefaultinstance.State.ToString() != "Open")
                                        {
                                            condefaultinstance.Open();
                                        }
                                        cmdinsserverdb.ExecuteNonQuery();
                                        condefaultinstance.Close();
                                    }
                      }
                }

            if (Request.QueryString["email"] != null)
            {
                string otherup = " Update companymaster SET email='' where email IN ('pk145@safestmail.net','n155@safestmail.net' ,'n151@safestmail.net','n152@safestmail.net','n153@safestmail.net','n154@safestmail.net','n155@safestmail.net' )";
                SqlCommand cmdotherup = new SqlCommand(otherup, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdotherup.ExecuteNonQuery();
                con.Close();

            }
            if (Request.QueryString["ban"] != null)
            {
                compid = Request.QueryString["Cidban"].ToString();
                createwebsiteandattach(Request.QueryString["Cidban"]);
                SqlCommand cmdsel = new SqlCommand("SELECT     CompanyMaster.CompanyId, CompanyMaster.YourDomaiUrl ,CompanyMaster.AdminId,CompanyMaster.Password, CompanyMaster.Websiteurl,CompanyMaster.CompanyLoginId,CompanyMaster.active, PricePlanMaster.CheckIntervalDays, PricePlanMaster.GraceDays, ProductMaster.ProductURL,ProductMaster.ProductName,  " +
                      "CompanyMaster.ProductId, CompanyMaster.PricePlanId, PricePlanMaster.StartDate, PricePlanMaster.EndDate, PricePlanMaster.TrafficinGB,  " +
                      "PricePlanMaster.MaxUser, ProductDetail.VersionNo,ProductMaster.ProductURL,PricePlanMaster.TotalMail, PricePlanMaster.GBUsage,VersionInfoMaster.VersionInfoId ,dbo.PricePlanMaster.PortalMasterId1 " +
                      "FROM         ProductDetail INNER JOIN " +
                      "ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId RIGHT OUTER JOIN " +
                      "CompanyMaster ON ProductMaster.ProductId = CompanyMaster.ProductId LEFT OUTER JOIN  " +
                      "PricePlanMaster ON CompanyMaster.PricePlanId = PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId  " +
                      " WHERE     (CompanyMaster.CompanyLoginId = '" + Request.QueryString["CidBan"] + "')", con);

                SqlDataAdapter dtpsel = new SqlDataAdapter(cmdsel);
                DataTable dt1 = new DataTable();
                dtpsel.Fill(dt1);

                if (dt1.Rows.Count > 0)
                {
                    string uid = Encrypted(dt1.Rows[0]["AdminId"].ToString());
                    string pass = Encrypted(dt1.Rows[0]["Password"].ToString());
                    string compidlmaster = Encrypted(dt1.Rows[0]["CompanyLoginId"].ToString());
                    string prodid = Encrypted(dt1.Rows[0]["ProductId"].ToString());
                    string proname = Encrypted(dt1.Rows[0]["ProductName"].ToString());
                    string chdint = Encrypted(dt1.Rows[0]["CheckIntervalDays"].ToString());
                    string gradays = Encrypted(dt1.Rows[0]["GraceDays"].ToString());
                    string runcou = Encrypted(Convert.ToString(1));
                    string StartDate = Encrypted(dt1.Rows[0]["StartDate"].ToString());
                    string EndDate = Encrypted(dt1.Rows[0]["EndDate"].ToString());
                    string MaxUser = Encrypted(dt1.Rows[0]["MaxUser"].ToString());
                    string VersionNo = Encrypted(dt1.Rows[0]["VersionNo"].ToString());
                    string TotalMail = Encrypted(dt1.Rows[0]["TotalMail"].ToString());
                    string GBUsage = Encrypted(dt1.Rows[0]["GBUsage"].ToString());
                    string TrafficinGB = Encrypted(dt1.Rows[0]["TrafficinGB"].ToString());
                    string PricePlanId = Encrypted(dt1.Rows[0]["PricePlanId"].ToString());
                    string VersionInfoId = dt1.Rows[0]["VersionInfoId"].ToString();
                    string PortalMasterId1 = Encrypted(dt1.Rows[0]["PortalMasterId1"].ToString());
                    string YourdomainUrl = dt1.Rows[0]["YourDomaiUrl"].ToString();

                    SqlCommand cmdnew = new SqlCommand("SELECT LicenseMaster.CompanyId, LicenseMaster.LicenseKey,LicenseMaster.LicenseDate,LicenseMaster.LicensePeriod,CompanyMaster.CompanyName, CompanyMaster.AdminId, CompanyMaster.Password,CompanyMaster.Websiteurl,CompanyMaster.active, " +
                                " HostDetail.SqlServerName, HostDetail.SqlServerUName, HostDetail.SqlServerUPassword, HostDetail.DatabaseName " +
                                " FROM CompanyMaster LEFT OUTER JOIN " +
                                " HostDetail ON CompanyMaster.CompanyId = HostDetail.CompanyId LEFT OUTER JOIN " +
                                " LicenseMaster ON CompanyMaster.CompanyId = LicenseMaster.CompanyId where  CompanyMaster.AdminId ='" + dt1.Rows[0]["AdminId"].ToString() + "' and CompanyMaster.Password='" + dt1.Rows[0]["Password"].ToString() + "' and CompanyMaster.CompanyLoginId='" + compid + "'  and CompanyMaster.active='1'", con);
                    SqlDataAdapter dtp = new SqlDataAdapter(cmdnew);
                    DataTable dtnew = new DataTable();
                    dtp.Fill(dtnew);

                    if (dtnew.Rows.Count > 0)
                    {
                        string LicenseDate = Encrypted(dtnew.Rows[0]["LicenseDate"].ToString());
                        string LicensePeriod = Encrypted(dtnew.Rows[0]["LicensePeriod"].ToString());
                        string lickey = Encrypted(dtnew.Rows[0]["LicenseKey"].ToString());
                        //Insert into Satellite Server

                        string otherup = " update Permenatalybanmacaddressandipadress set BanIsActive='0',bannedipaddress='0',bannedmacaddress='0' where  Ipaddress='" + Request.QueryString["ban"] + "' ";
                        SqlCommand cmdotherup = new SqlCommand(otherup, condefaultinstance);
                        if (condefaultinstance.State.ToString() != "Open")
                        {
                            condefaultinstance.Open();
                        }
                        cmdotherup.ExecuteNonQuery();
                        condefaultinstance.Close();
                    }
                }

               

            }
            if (Request.QueryString["delete"] != null)
            {
                string otherup = " delete from " + Request.QueryString["delete"] + " )";
                SqlCommand cmdotherup = new SqlCommand(otherup, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdotherup.ExecuteNonQuery();
                con.Close();

            }

        }
    }
    protected void createwebsiteandattach(string compid)
    {

        string str = " SELECT CompanyMaster.*,PortalMasterTbl.PortalName,PricePlanMaster.VersionInfoMasterId,PricePlanMaster.Producthostclientserver from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join PortalMasterTbl on PricePlanMaster.PortalMasterId1 = PortalMasterTbl.id where CompanyLoginId='" + compid + "' ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {

            string websitename = compid;
            verid = ds.Rows[0]["VersionInfoMasterId"].ToString();
            string priceplanid = ds.Rows[0]["PricePlanId"].ToString();
            string productid = ds.Rows[0]["ProductId"].ToString();
            string versionid = ds.Rows[0]["VersionInfoMasterId"].ToString();
            string portalname = ds.Rows[0]["PortalName"].ToString();
            Encryptkeycompsss = ds.Rows[0]["Encryptkeycomp"].ToString();
            
            ViewState["ownserver"] = ds.Rows[0]["Producthostclientserver"].ToString();

            string strserver = "";
            if (ViewState["ownserver"].ToString() == "True")
            {
                strserver = "select ServerMasterTbl.* from ServerMasterTbl inner join  CompanyMaster on CompanyMaster.CompanyLoginId=ServerMasterTbl.compid where CompanyMaster.CompanyLoginId='" + compid + "' and ServerMasterTbl.compid='" + compid + "'";
            }
            else
            {
                strserver = " SELECT ServerMasterTbl.* from ServerAssignmentMasterTbl inner join ServerMasterTbl on ServerMasterTbl.Id=ServerAssignmentMasterTbl.ServerId where ProductId='" + productid + "' and VersionId='" + versionid + "' and PricePlanId='" + priceplanid + "' and Active='1' ";

            }
            SqlCommand cmdserver = new SqlCommand(strserver, con);
            SqlDataAdapter adpserver = new SqlDataAdapter(cmdserver);
            DataTable dsserver = new DataTable();
            adpserver.Fill(dsserver);

            if (dsserver.Rows.Count > 0)
            {
              
                string defaultinstancename = dsserver.Rows[0]["DefaultsqlInstance"].ToString();
                string sqlserveurl = dsserver.Rows[0]["sqlurl"].ToString();
                string sqlservername = dsserver.Rows[0]["PublicIp"].ToString();
                string sqlinstancename = dsserver.Rows[0]["Sqlinstancename"].ToString();
                string sqlserverport = dsserver.Rows[0]["port"].ToString();
                string defaultdatabasename = dsserver.Rows[0]["DefaultDatabaseName"].ToString();
                 string Companymastersqlistance = dsserver.Rows[0]["PortforCompanymastersqlistance"].ToString();
                string iiswebsitepath = dsserver.Rows[0]["serverdefaultpathforiis"].ToString();
                string iismdfpath = dsserver.Rows[0]["serverdefaultpathformdf"].ToString();
                string iisldfpath = dsserver.Rows[0]["serverdefaultpathforfdf"].ToString();
                string domainuser = dsserver.Rows[0]["InDomain"].ToString();
                Serverencstr = dsserver.Rows[0]["Enckey"].ToString();
               
                // string sapassword = dsserver.Rows[0]["Sapassword"].ToString();
                string sapassword = PageMgmt.Decrypted(dsserver.Rows[0]["Sapassword"].ToString());

                condefaultinstance = new SqlConnection();
                condefaultinstance.ConnectionString = @"Data Source =" + sqlserveurl + "\\" + defaultinstancename + "," + sqlserverport + "; Initial Catalog = C3SATELLITESERVER; User ID=sa; Password=" + sapassword + "; Persist Security Info=true; ";
                condefaultinstance.ConnectionString = @"Data Source =TCP:C3_SQL.safestserver.com,30000; Initial Catalog = C3SATELLITESERVER; User ID=sa; Password=06De1963++; Persist Security Info=true;";
                if (condefaultinstance.State.ToString() != "Open")
                {
                    condefaultinstance.Open();
                }
                condefaultinstance.Close();



            }


        }

    }
    public static string Encrypted(string strText)
    {
        return Encrypt(strText, Encryptkeycompsss);      
    }
    public static string serverEncrypted(string strText)
    {
        return Encrypt(strText, Serverencstr);
        
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
  
}
