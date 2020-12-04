using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
//using System.DirectoryServices;
using System.IO.Compression;
using System.IO;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using System.Configuration;
using System.Data;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;

public partial class AccessRight : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static string Serverencstr = "";
    public static string verid = "";
    public static string compid = "";
    public static string Encryptkeycompsss = "";
    public static string encstr = "";
    SqlConnection condefaultinstance = new SqlConnection();
    SqlConnection conser;
    public static double size = 0;
    int StepId = 1;
    SqlConnection connMasterserver;
    SqlConnection connCompserver = new SqlConnection();
    string allstring = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string aaaa = PageMgmt.Encrypted("asasas");

            if (Request.QueryString["compid"] != null || Request.QueryString["ProdMasCodeId"] != null || Request.QueryString["CodeVersionId"] != null || Request.QueryString["CodeTypeID"] != null)
            {                   
                    lbl_codetypeid.Text =PageMgmt.Encrypted(Request.QueryString["CodeTypeID"].ToString());
                    lbl_codeversionno.Text = PageMgmt.Encrypted(Request.QueryString["CodeVersionId"].ToString());
                    string compid =PageMgmt.Encrypted(Request.QueryString["compid"]);
                    lbl_ProdMasCodeId.Text =PageMgmt.Encrypted(Request.QueryString["ProdMasCodeId"].ToString());
                    string str = " SELECT CompanyMaster.*,PortalMasterTbl.id as portlID, PortalMasterTbl.PortalName,PricePlanMaster.VersionInfoMasterId,PricePlanMaster.Producthostclientserver, dbo.ClientMaster.ServerId AS ClientServerid from dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId  where CompanyLoginId='" + compid + "' ";
                    SqlCommand cmd = new SqlCommand(str, con);        
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable ds = new DataTable();
                    adp.Fill(ds);
                    if (ds.Rows.Count > 0)
                    {   
                        lbl_CompServerID.Text = ds.Rows[0]["ServerId"].ToString();   
                        lbl_ClientServerID.Text = ds.Rows[0]["ClientServerid"].ToString();   
                        lbl_versionid.Text = ds.Rows[0]["VersionInfoMasterId"].ToString();
                        ServerConn1(ds.Rows[0]["ServerId"].ToString(), ds.Rows[0]["ClientServerid"].ToString(), lbl_ProdMasCodeId.Text, ds.Rows[0]["Encryptkeycomp"].ToString(), compid, ds.Rows[0]["VersionInfoMasterId"].ToString());                      
                    }                      
            } 
        }
    }
    protected void WebsitePublishLicense(string sid,string versionid,string compid)
    {
        DataTable dsmaxidget = selectBZ("select ProductMasterCodeTbl.ProductVerID,ProductMasterCodeTbl.CodeTypeID,Max(ProductMasterCodeTbl.codeversionnumber) as codeversionnumber  from ProductMasterCodeTbl inner join ProductMasterCodeonsatelliteserverTbl on ProductMasterCodeonsatelliteserverTbl.ProductMastercodeID=ProductMasterCodeTbl.ID inner join CodeTypeTbl on CodeTypeTbl.ID=ProductMasterCodeTbl.CodeTypeID inner join CodeTypeCategory on CodeTypeCategory.CodeMasterNo=CodeTypeTbl.CodeTypeCategoryId  where ProductMasterCodeTbl.ProductVerID='" + versionid + "' and ProductMasterCodeonsatelliteserverTbl.ServerID='" + sid + "' and ProductMasterCodeonsatelliteserverTbl.Successfullyuploadedtoserver='1' and CodeTypeCategory.CodeMasterNo In ('1') group by ProductMasterCodeTbl.ProductVerID,ProductMasterCodeTbl.CodeTypeID ");
        if (dsmaxidget.Rows.Count > 0)
        {
            foreach (DataRow drmax in dsmaxidget.Rows)
            {
                DataTable dsmastersourcepath = selectBZ("select ProductMasterCodeTbl.ID,ProductMasterCodeTbl.ProductVerID,ProductMasterCodeTbl.CodeTypeID,ProductMasterCodeTbl.codeversionnumber,ProductMasterCodeonsatelliteserverTbl.Physicalpath,ProductMasterCodeonsatelliteserverTbl.filename,ProductCodeDetailTbl.CodeTypeName,CodeTypeTbl.CodeTypeCategoryId ,ProductCodeDetailTbl.AdditionalPageInserted from ProductMasterCodeTbl inner join ProductMasterCodeonsatelliteserverTbl on ProductMasterCodeonsatelliteserverTbl.ProductMastercodeID=ProductMasterCodeTbl.ID inner join CodeTypeTbl on CodeTypeTbl.ID=ProductMasterCodeTbl.CodeTypeID inner join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=CodeTypeTbl.ProductCodeDetailId where ProductMasterCodeTbl.ProductVerID='" + drmax["ProductVerID"].ToString() + "' and ProductMasterCodeTbl.CodeTypeID='" + drmax["CodeTypeID"].ToString() + "' and ProductMasterCodeTbl.codeversionnumber='" + drmax["codeversionnumber"].ToString() + "' and ProductMasterCodeonsatelliteserverTbl.ServerID='" + sid + "' and ProductMasterCodeonsatelliteserverTbl.Successfullyuploadedtoserver='1' ");
                if (dsmastersourcepath.Rows.Count > 0)
                {                   
                    string ProdMasCodeId = dsmastersourcepath.Rows[0]["ProdMasCodeId"].ToString();
                    string str = " SELECT CompanyMaster.*,PortalMasterTbl.id as portlID, PortalMasterTbl.PortalName,PricePlanMaster.VersionInfoMasterId,PricePlanMaster.Producthostclientserver, dbo.ClientMaster.ServerId AS ClientServerid from dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId  where CompanyLoginId='" + compid + "' ";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable ds = new DataTable();
                    adp.Fill(ds);
                    if (ds.Rows.Count > 0)
                    {
                        //lbl_CompServerID.Text = ds.Rows[0]["ServerId"].ToString();
                        lbl_ClientServerID.Text = ds.Rows[0]["ClientServerid"].ToString();
                        lbl_versionid.Text = ds.Rows[0]["VersionInfoMasterId"].ToString();
                        ServerConn1(ds.Rows[0]["ServerId"].ToString(), ds.Rows[0]["ClientServerid"].ToString(), ProdMasCodeId, ds.Rows[0]["Encryptkeycomp"].ToString(), compid, ds.Rows[0]["VersionInfoMasterId"].ToString());
                    }
                   //Response.Redirect("http://license.busiwiz.com/Silent_CompanyCodeCreateTransfer.aspx?compid=" + PageMgmt.Decrypted(compid) + "&ProdMasCodeId=" + PageMgmt.Decrypted(ProdMasCodeId) + "&CodeVersionId=" + PageMgmt.Decrypted(drmax["codeversionnumber"].ToString()) + "&CodeTypeID=" + PageMgmt.Decrypted(CodeTypeID) + "");
                }
            }
        }
    }



    protected void ServerConn1(string Compserverid,string ClientServerid, string ProdMasCodeId, string Encryptkeycomp, string comploginid, string VersionInfoMasterId)
    {
        DataTable dt_MasterServer = selectBZ("SELECT * from ServerMasterTbl where Id='" + ClientServerid + "'");
        if (dt_MasterServer.Rows.Count > 0)
        {
            DataTable dt_CompServer = selectBZ("SELECT * from ServerMasterTbl where Id='" + Compserverid + "'");
            DataTable dt_Codeversion = selectBZ(" select * from ProductMasterCodeTbl where ID='" + ProdMasCodeId + "' and CodeTypeID=" + lbl_codetypeid.Text + " ");
            if (dt_CompServer.Rows.Count > 0 && dt_Codeversion.Rows.Count > 0)
            {
              //Start--------Company's Server Connectionstring
                string Comp_ServEnckey = dt_CompServer.Rows[0]["Enckey"].ToString();
                string Comp_serversqlserverip = dt_CompServer.Rows[0]["sqlurl"].ToString();
                string Comp_serversqlinstancename = dt_CompServer.Rows[0]["DefaultsqlInstance"].ToString();
                string Comp_serversqlport = dt_CompServer.Rows[0]["port"].ToString();
                string Comp_serversqldbname = dt_CompServer.Rows[0]["DefaultDatabaseName"].ToString();
                string Comp_serversqlpwd = dt_CompServer.Rows[0]["Sapassword"].ToString();
                string Comp_serverweburl = dt_CompServer.Rows[0]["Busiwizsatellitesiteurl"].ToString();
                connCompserver.ConnectionString = @"Data Source =" + Comp_serversqlserverip + "\\" + "\\" + Comp_serversqlinstancename + "," + Comp_serversqlport + "; Initial Catalog=" + Comp_serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(Comp_serversqlpwd) + "; Persist Security Info=true;";
                string serverconnnstr = lbl_serverconnnstr.Text = connCompserver.ConnectionString;
                string iispath = dt_CompServer.Rows[0]["serverdefaultpathforiis"].ToString();
              //Close-------Company's ConnectionString
              //Start-------Company's Server MasterCodePath 
                string TOMasterServer_TemppathFor_Comp = lbl_TOMasterServer_TemppathFor_Comp.Text = dt_MasterServer.Rows[0]["folderpathformastercode"].ToString() + "\\" + comploginid + "\\Temp";//ex:  D:\ALLNEWCOMPANYMASTERCOPY\jobcenter\Temp
                string TOMasterServer_PublishFor_Comp = lbl_TOMasterServer_PublishFor_Comp.Text = dt_MasterServer.Rows[0]["folderpathformastercode"].ToString() + "\\" + comploginid + "\\Publish";//ex: D:\ALLNEWCOMPANYMASTERCOPY\jobcenter\Publish
                 DeleteCreateFolder(TOMasterServer_TemppathFor_Comp);
                 DeleteCreateFolder(TOMasterServer_PublishFor_Comp); 
              //Close-------Company's Server MasterCodePath
                // Start----Client Code Path
                 string Client_latestcodeZipFilePath = lbl_Client_latestcodeZipFilePath.Text = dt_Codeversion.Rows[0]["TemporaryPath"].ToString().Replace("\\\\", "\\");//ex: D:\ALLNEWCOMPANYMASTERCOPY\35\IJobcenterMaster\WWW\PublishCode\2692017183317\10090_1_9078_1_262017918309.zip
                 string Client_CodePath =lbl_Client_CodePath.Text= Client_latestcodeZipFilePath.Replace(".zip", "");
                 lbl_codetypeid.Text = dt_Codeversion.Rows[0]["CodeTypeID"].ToString();
                 lbl_codeversionno.Text = dt_Codeversion.Rows[0]["codeversionnumber"].ToString();
                 //Close----Client Code Path
                 if (File.Exists(Client_latestcodeZipFilePath))
                 {
                     string CodeFolderNameZIP = lbl_CodeFolderNameZIP.Text = Path.GetFileName(Client_latestcodeZipFilePath.TrimEnd(Path.DirectorySeparatorChar));//ex: 10090_1_9078_1_262017918309.zip
                     string CodeFolderName = lbl_CodeFolderName.Text = Path.GetFileName(Client_CodePath.TrimEnd(Path.DirectorySeparatorChar));//ex: 10090_1_9078_1_262017918309
                     string AppcodefolderPath = lbl_AppcodefolderPath.Text = TOMasterServer_TemppathFor_Comp + "\\" + CodeFolderName;//ex: D:\ALLNEWCOMPANYMASTERCOPY\jobcenter\Temp\10090_1_9078_1_262017918309\10090_1_9078_1_262017918309
                     string PublishfolderPath = lbl_PublishfolderPath.Text = TOMasterServer_PublishFor_Comp + "\\" + CodeFolderName;//ex: D:\ALLNEWCOMPANYMASTERCOPY\jobcenter\Publish\10090_1_9078_1_262017918309\10090_1_9078_1_262017918309                  
                         //start----Zip File Extract at temp master code path
                         using (ZipFile zip = ZipFile.Read(Client_latestcodeZipFilePath))
                         {
                             zip.ExtractAll(TOMasterServer_TemppathFor_Comp, ExtractExistingFileAction.OverwriteSilently);
                         }
                         //Close----Zip File Extract at temp master code path
                         if (Directory.Exists("" + AppcodefolderPath + "\\App_Code"))
                         {
                             ClassFileCreating(PublishfolderPath, AppcodefolderPath + "\\App_Code", serverconnnstr, Encryptkeycomp, Comp_ServEnckey);
                             try
                             {
                                 PublishCode(AppcodefolderPath, CodeFolderName, PublishfolderPath + "\\" + CodeFolderName);
                                 CreateZIP(PublishfolderPath, PublishfolderPath);
                                 // double size = GetSizeDirectory(sourceDir);
                                 // double twpercsize = 0.20 * size;
                             }
                             catch (Exception ex)
                             {
                                 CreateZIP(TOMasterServer_TemppathFor_Comp, PublishfolderPath);
                             }
                             insertcodeversiondetail(Compserverid, ClientServerid, comploginid, VersionInfoMasterId, ProdMasCodeId, lbl_codetypeid.Text, lbl_codeversionno.Text, Comp_serverweburl, Client_latestcodeZipFilePath);                                           
                         }
                         else
                         {
                             lblmsg.Text = " folder not available at this path " + "" + AppcodefolderPath + "\\App_Code";
                         }                        
                        
                }
                else
                {
                    lblmsg.Text = " File not available at this path " + Client_latestcodeZipFilePath;  
                }
            }
            else
            {
                lblmsg.Text = " Client's server or ProductMasterCodeTbl Table records not available";
            }
        }
        else
        {
            lblmsg.Text = " Company's server not available";
        }       
    }

    protected void CopyFile(string fname, string destnationpath, string ID, string serverid,string filename)
    {
        string strftpdetail = " SELECT * from ServerMasterTbl where Id='" + serverid + "'";
        SqlCommand cmdftpdetail = new SqlCommand(strftpdetail, con);
        DataTable dtftpdetail = new DataTable();
        SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
        adpftpdetail.Fill(dtftpdetail);
        if (dtftpdetail.Rows.Count > 0)
        {
             string serversqlserverip = dtftpdetail.Rows[0]["sqlurl"].ToString();
            string serversqlinstancename = dtftpdetail.Rows[0]["DefaultsqlInstance"].ToString();
            string serversqldbname = dtftpdetail.Rows[0]["DefaultDatabaseName"].ToString();
            string serversqlpwd = dtftpdetail.Rows[0]["Sapassword"].ToString();
            string serversqlport = dtftpdetail.Rows[0]["port"].ToString();
            SqlCommand cmdclientdetail = new SqlCommand("Insert into CompanyProductUpdateStatusTbl(ProductVersionId,CodeTypeId,CodeVersionNo,CompanyLoginId,DateTime,Successfullystatus,CompanyCodeCreated,DownloadCompanyCode,UpdateCompanyCode)Values('" + Request.QueryString["versionid"] + "','" + Request.QueryString["codetypeid"] + "','" + Request.QueryString["codeversionno"] + "','" + Request.QueryString["comid"] + "','','1',1,1,0)", connCompserver);
            if (connCompserver.State.ToString() != "Open")
            {
                connCompserver.Open();
            }
            cmdclientdetail.ExecuteNonQuery();
            connCompserver.Close();
            lblmsg.Text = "Successfully created code for company";            
            // Response.Redirect("http://" + serverurl + "/CompanyCodeDownload.aspx?ClientServerID=" + ClientServerid + "&CompServerID=" + serverid + "&comid=" + compid + "&ProdMasCodeId=" + ProdMasCodeId + "");                                   
        }
        else
        {
            lblmsg.Text = "Server not found";          
        }
    }
    protected void insertcodeversiondetail(string serverid,string ClientServerid,string  compid, string versionid,string ProdMasCodeId, string codetypeid, string codeversionno,string serverurl,string companyzipfile)
    {      
           
                DataTable dt_CompServer = selectBZ("SELECT * from ServerMasterTbl where Id='" + serverid + "'");                
                if (dt_CompServer.Rows.Count > 0)
                {
                    //Start--------Company's Server Connectionstring
                    string Comp_ServEnckey = dt_CompServer.Rows[0]["Enckey"].ToString();
                    string Comp_serversqlserverip = dt_CompServer.Rows[0]["sqlurl"].ToString();
                    string Comp_serversqlinstancename = dt_CompServer.Rows[0]["DefaultsqlInstance"].ToString();
                    string Comp_serversqlport = dt_CompServer.Rows[0]["port"].ToString();
                    string Comp_serversqldbname = dt_CompServer.Rows[0]["DefaultDatabaseName"].ToString();
                    string Comp_serversqlpwd = dt_CompServer.Rows[0]["Sapassword"].ToString();
                    string Comp_serverweburl = dt_CompServer.Rows[0]["Busiwizsatellitesiteurl"].ToString();
                    string folderpathformastercode = dt_CompServer.Rows[0]["folderpathformastercode"].ToString();
                    connCompserver.ConnectionString = @"Data Source =" + Comp_serversqlserverip + "\\" + "\\" + Comp_serversqlinstancename + "," + Comp_serversqlport + "; Initial Catalog=" + Comp_serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(Comp_serversqlpwd) + "; Persist Security Info=true;";
                    string iispath = dt_CompServer.Rows[0]["serverdefaultpathforiis"].ToString();

                    string zipfolder = Path.GetFileName(companyzipfile.TrimEnd(Path.DirectorySeparatorChar));
                    string mastersourcepath = companyzipfile;
                    string tocopyfilename = Server.MapPath("~/Attach/Code/" + compid + "/" + zipfolder);
                    bool exists = System.IO.Directory.Exists(Server.MapPath("~/Attach/Code/" + compid));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Attach/Code/" + compid));
                    }
                    File.Copy(mastersourcepath, tocopyfilename, true);
                    SqlCommand cmdclientdetail = new SqlCommand("Insert into CompanyProductUpdateStatusTbl (ProductVersionId,CodeTypeId,CodeVersionNo,CompanyLoginId,DateTime,Successfullystatus,CompanyCodeCreated,DownloadCompanyCode,UpdateCompanyCode)Values('" + versionid + "','" + codetypeid + "','" + codeversionno + "','" + compid + "','','0',1,0,0)", connCompserver);
                    // connCompserver.ConnectionString = lbl_serverconnnstr.Text;
                    if (connCompserver.State.ToString() != "Open")
                    {
                        connCompserver.Open();
                    }
                    cmdclientdetail.ExecuteNonQuery();
                    connCompserver.Close();
                    lblmsg.Text = "Successfully created code for " + compid + " company";
                    Response.Redirect("http://" + serverurl + "/CompanyCodeDownload.aspx?ClientServerID=" + PageMgmt.Decrypted(ClientServerid) + "&CompServerID=" + PageMgmt.Decrypted(serverid) + "&comid=" + PageMgmt.Decrypted(compid) + "&ProdMasCodeId=" + PageMgmt.Decrypted(ProdMasCodeId) + "&versionid=" + PageMgmt.Decrypted(versionid) + "&codetypeid=" + PageMgmt.Decrypted(codetypeid) + "&codeversionno=" + PageMgmt.Decrypted(codeversionno) + "&ServerTemppath=" + PageMgmt.Decrypted(folderpathformastercode) + "&iispath=" + PageMgmt.Decrypted(iispath) + "");
                }
            //}
            //catch(Exception ex)
            //{
            //    lblmsg.Text   = ex.ToString()+ "Some problem in creting new version";
            //}
    }
    
    protected void ClassFileCreating(string companytemppath,string appcodepath, string serconn, string compenckey, string serkey)
    {
        string HashKey = "";
        //encstr = CreateLicenceKey(out HashKey);

        string fileLoc = appcodepath + "\\Myfile.cs";
        using (StreamWriter sw = new StreamWriter(fileLoc))
            sw.Write
                (@" using System;
                    using System.Data;
                    using System.Configuration;
                    using System.Web;
                    using System.Web.Security;
                    using System.Web.UI;
                    using System.Web.UI.WebControls;
                    using System.Web.UI.WebControls.WebParts;
                    using System.Web.UI.HtmlControls;
                    using System.Data.SqlClient;
                    using System.Data.Common;
                    public class Myfile
                    {                       
                        public static string serverconn=""" + serconn + "" + "\";" +
                        @"public static string companykey=""" + compenckey + "" + "\";" +
                        @"public static string serverkey=""" + serkey + "" + "\";" +
                        @"    
                        public Myfile()
                        {
                            
                        }   
                        public static SqlConnection serverconnstring()
                        {     
                            SqlConnection serverconnstri = new SqlConnection();
                            serverconnstri.ConnectionString =serverconn;                    
                            return serverconnstri;
                        }
                        public static string Companykey()
                        {                           
                            return companykey;
                        }
                        public static string Serverkey()
                        {                           
                            return serverkey;
                        } 
                    }");
    }  
    static double GetSizeDirectory(DirectoryInfo source)
    {

        FileInfo[] files = source.GetFiles();
        foreach (FileInfo file in files)
        {
            size += file.Length;
        }
        // Process subdirectories.
        DirectoryInfo[] dirs = source.GetDirectories();

        foreach (DirectoryInfo dir in dirs)
        {

            GetSizeDirectory(dir);
        }
        return size;
    }
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
    protected void DeleteCreateFolder(string CompanyServerTemppath)
    {
        if (!Directory.Exists(CompanyServerTemppath))
        {
            Directory.CreateDirectory(CompanyServerTemppath);
        }
        else
        {
            try
            {
                var dir = new DirectoryInfo(CompanyServerTemppath);
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete();
                if (!Directory.Exists(CompanyServerTemppath))
                {
                    Directory.CreateDirectory(CompanyServerTemppath);
                }
            }
            catch (IOException ex)
            {
            }
        } 
    }
    protected void PublishCode(string AppcodefolderPath, string CodeFolderName, string PublishfolderPath)
    {
        DirectoryInfo sourceDir = new DirectoryInfo(AppcodefolderPath);
        string mspath = "C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\";
        string mscompiler = "aspnet_compiler.exe";
        string fullcompilerpath = Path.Combine(mspath, mscompiler);
        ProcessStartInfo startinfo = new ProcessStartInfo();
        string argument = "-p " + AppcodefolderPath + " -v " + CodeFolderName + " -u -f " + PublishfolderPath + " -fixednames -errorstack";
        Process.Start(fullcompilerpath, argument);
    }
    protected void CreateZIP(string Directorypathname,string zipsavepath)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AddDirectory(Directorypathname);
            zip.Save(zipsavepath + ".zip");
        }   
    }
}
