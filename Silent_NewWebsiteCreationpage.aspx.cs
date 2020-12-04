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


using System.Net.Security;
using System.Text.RegularExpressions;



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

    string sid = "";
    string clientServerfolderpathformastercode = "";
    string FTP = "";
    string FTPUserName = "";
    string FTPPassword = "";
    string FTPport = "";

    string Comp_serverweburl = "";
    string sqlserverport = "";
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            if (Request.QueryString["sid"] != null  && Request.QueryString["compid"] !=null)
            {
                string sid = Request.QueryString["sid"].ToString().Replace(" ", "+");
                sid = PageMgmt.Decrypted(sid);
               // string versionid = PageMgmt.Decrypted(Request.QueryString["versionid"]);
              //  string compid =PageMgmt.Decrypted(Request.QueryString["compid"].ToString());
                string compidaa = Request.QueryString["compid"].ToString().Replace(" ", "+");
                string compid = PageMgmt.Decrypted(compidaa.ToString());
                //sid = "5";
                //compid = "aswathy";
                DataTable ds = MyCommonfile.selectBZ(" SELECT TOP (1) dbo.PortalMasterTbl.Id AS portlID, dbo.PortalMasterTbl.PortalName, dbo.PricePlanMaster.VersionInfoMasterId,dbo.PricePlanMaster.Producthostclientserver,dbo.ClientMaster.ClientURL, dbo.ClientMaster.ServerId AS ClientServerid, dbo.ServerMasterTbl.*, dbo.CompanyMaster.ServerId FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id  where CompanyLoginId='" + compid + "' "); 
                if (ds.Rows.Count > 0)
                {
                    string ComServerId = ds.Rows[0]["ServerId"].ToString();
                    string versionid = ds.Rows[0]["VersionInfoMasterId"].ToString();

                    lbl_serverurl.Text = ds.Rows[0]["Busiwizsatellitesiteurl"].ToString(); 
                    string sqlservernameip = ds.Rows[0]["PublicIp"].ToString();
                    string Comp_serversqlinstancename = ds.Rows[0]["DefaultsqlInstance"].ToString();
                    string sqlinstancename = ds.Rows[0]["Sqlinstancename"].ToString();


                    string DefaultMdfpath = ds.Rows[0]["DefaultMdfpath"].ToString();
                    string DefaultLdfpath = ds.Rows[0]["DefaultLdfpath"].ToString();
                    string iispath = ds.Rows[0]["serverdefaultpathforiis"].ToString();                                                         
                  
                    string Comp_Ser_Masterpath = ds.Rows[0]["folderpathformastercode"].ToString();
                    string Comp_Ser_iispath = ds.Rows[0]["serverdefaultpathforiis"].ToString();
                    string Comp_ServEnckey = ds.Rows[0]["Enckey"].ToString();
                    string Comp_serversqlserverip = ds.Rows[0]["sqlurl"].ToString();
                   
                   // string Comp_serversqlport = ds.Rows[0]["port"].ToString();
                    string Comp_serversqldbname = ds.Rows[0]["DefaultDatabaseName"].ToString();
                  //  string Comp_serversqlpwd = ds.Rows[0]["Sapassword"].ToString();
                    Comp_serverweburl = ds.Rows[0]["Busiwizsatellitesiteurl"].ToString();
                    sqlserverport = ds.Rows[0]["port"].ToString();

                     string sqlCompport28 = ds.Rows[0]["PortforCompanymastersqlistance"].ToString();//2810
                    string ClientServerid = ds.Rows[0]["ClientServerid"].ToString();
                    string ClientURL = ds.Rows[0]["ClientURL"].ToString();

                   

                    DataTable DTclientServerid = MyCommonfile.selectBZ(" Select * From ServerMasterTbl Where id='" + ClientServerid + "'");
                    clientServerfolderpathformastercode = DTclientServerid.Rows[0]["folderpathformastercode"].ToString();
                    FTP = DTclientServerid.Rows[0]["FTPforMastercode"].ToString();
                    FTPUserName = DTclientServerid.Rows[0]["FTPuseridforDefaultIISpath"].ToString();
                    FTPPassword = DTclientServerid.Rows[0]["FtpPasswordforDefaultIISpath"].ToString();
                    FTPport = DTclientServerid.Rows[0]["FTPportfordefaultIISpath"].ToString();

                    bool SerConnstatust = false;
                    int timeconncheck = 1;

                    connCompserver = ServerWizard.ServerDefaultInstanceConnetionTCP(compid);
                    string Compserverconnnstr = connCompserver.ConnectionString;
                    while ((!SerConnstatust) && (timeconncheck < 2))
                    {
                        try
                        {
                            if (connCompserver.State.ToString() != "Open")
                            {
                                connCompserver.Open();
                                SerConnstatust = true;
                            }
                            else
                            {
                                SerConnstatust = true;
                            }
                        }
                        catch
                        {
                            timeconncheck++;
                            //Port Open
                            //Page Y
                            string pagenamemainY = "Satelliteservfunction.aspx?PO=OpenPort&PortNo=" + BZ_Common.Encrypted_satsrvencryky(sqlserverport) + "&Port2No=" + BZ_Common.Encrypted_satsrvencryky(sqlCompport28) + "";//&SilentPageRequestTblID=" + PageMgmt.Encrypted(SilentPageRequestTblID) + "
                            //Page X
                            string mycurrenturlX = Request.Url.AbsoluteUri;
                            Random random = new Random();
                            int randomNumber = random.Next(1, 10);
                            string Randomkeyid = Convert.ToString(randomNumber);
                            string SilentPageRequestTblID = CompanyRelated.Insert_SilentPageRequestTbl(ComServerId, pagenamemainY, DateTime.Now.ToString(), "", false, Randomkeyid, mycurrenturlX);
                            string url = "";
                            url = "http://" + Comp_serverweburl + "/vfysrv.aspx?licensesilentpagerequesttblid=" + BZ_Common.Encrypted_satsrvencryky(SilentPageRequestTblID) + "&pageredirecturl=" + BZ_Common.Encrypted_satsrvencryky(pagenamemainY) + "&mstrsrvky=" + BZ_Common.Encrypted_satsrvencryky(BZ_Common.satsrvencryky()) + "&returnurl=" + BZ_Common.Encrypted_satsrvencryky(ClientURL) + "";
                            Response.Redirect("" + url + "");
                        }
                    }                     
                     
                    if (SerConnstatust == true)//if conn open (port open)
                    {                       
                        InsertServerMaster(sid);  
                        
                        Database_Latest_Code_Insert(compid, sid, versionid, Compserverconnnstr, Comp_Ser_iispath, Comp_Ser_Masterpath, Comp_serverweburl, ClientServerid, DefaultMdfpath, DefaultLdfpath, sqlservernameip, sqlinstancename);                                              
                       
                        Website_Latest_Code_Insert(sid, versionid, compid, Compserverconnnstr, iispath, Comp_ServEnckey, Comp_Ser_Masterpath, Comp_serverweburl, iispath);
                      
                        lbl_compid.Text = compid;
                        string pagenames = "";
                        pagenames = "http://" + Comp_serverweburl + "/Satelliteservfunction.aspx?ftpurl=" + BZ_Common.BZ_Encrypted(FTP) + "&userid=" + BZ_Common.BZ_Encrypted(FTPUserName) + "&password=" + BZ_Common.BZ_Encrypted(FTPPassword) + "&portno=" + BZ_Common.BZ_Encrypted(FTPport) + "&Compid=" + BZ_Common.BZ_Encrypted(compid) + "&ServerID=" + BZ_Common.BZ_Encrypted(sid) + "&PortSecID=1";
                        Response.Redirect("" + pagenames + "");
                       // FillGrid_ComCreNeedCode();
                    }
                    else
                    {
                        lblmsg.Text = "Connecting to server database problem";
                    }
                }
                else
                {
                    lblmsg.Text = "No record available for  ";
                }
            }           
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
               bool SerConnstatust = false;
        int timeconncheck = 1;
        while ((!SerConnstatust) && (timeconncheck < 2))
        {           
            try
            { 
                if (connCompserver.State.ToString() != "Open")
                {
                    connCompserver.Open();
                    SerConnstatust = true;
                }
                else
                {
                    SerConnstatust = true;
                }
            }
            catch
            {
                timeconncheck++;
            }
        }
       
    }

    //-----------------------------------------------------------------------------------
    protected void Website_Latest_Code_Insert(string comsid, string VersionInfoMasterId, string compid, string STRconnCompserver, string iispath, string Comp_ServEnckey, string Comp_Ser_Masterpath, string Comp_serverweburl, string Comp_IISPath)
    {
        string ftpphysicalpath = "";        
        
        DataTable dsmaxidget = MyCommonfile.selectBZ(" SELECT   dbo.ProductMasterCodeTbl.ProductVerID, dbo.ProductMasterCodeTbl.CodeTypeID, MAX(dbo.ProductMasterCodeTbl.codeversionnumber) AS codeversionnumber, dbo.ProductCodeDetailTbl.CodeTypeName FROM dbo.ProductMasterCodeTbl INNER JOIN dbo.CodeTypeTbl ON dbo.CodeTypeTbl.ID = dbo.ProductMasterCodeTbl.CodeTypeID INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ProductCodeDetailTbl.Id = dbo.CodeTypeTbl.ProductCodeDetailId where ProductMasterCodeTbl.ProductVerID='" + VersionInfoMasterId + "'  and CodeTypeCategory.CodeMasterNo In ('1')  GROUP BY dbo.ProductMasterCodeTbl.ProductVerID, dbo.ProductMasterCodeTbl.CodeTypeID, dbo.ProductCodeDetailTbl.CodeTypeName ");       
        if (dsmaxidget.Rows.Count > 0)
        {
            foreach (DataRow drmaxdb in dsmaxidget.Rows)
            {                
                DataTable dtmastcode = MyCommonfile.selectBZ(" SELECT * From ProductMasterCodeTbl Where codeversionnumber='" + drmaxdb["codeversionnumber"].ToString() + "' and CodeTypeID='" + drmaxdb["CodeTypeID"].ToString() + "' ");
                if (dtmastcode.Rows.Count > 0)
                {
                string filename = dtmastcode.Rows[0]["filename"].ToString();
                ftpphysicalpath = Comp_Ser_Masterpath + "\\" + filename;
                DataTable dtsercheck = MyCommonfile.selectBZ(" SELECT * From ProductMasterCodeonsatelliteserverTbl Where ProductMastercodeID='" + dtmastcode.Rows[0]["ID"].ToString() + "' and dbo.ProductMasterCodeonsatelliteserverTbl.ServerID='" + comsid + "' ");
                #region                
                if (dtsercheck.Rows.Count == 0)
                {                   
                    string strsatelliteserverinsert = " Insert into ProductMasterCodeonsatelliteserverTbl(ProductMastercodeID,ServerID,Successfullyuploadedtoserver,Physicalpath,filename) values ('" + dtmastcode.Rows[0]["ID"].ToString() + "','" + comsid + "','0','" + ftpphysicalpath + "','" + filename + "')";
                    SqlCommand cmdsatelliteserverinsert = new SqlCommand(strsatelliteserverinsert, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdsatelliteserverinsert.ExecuteNonQuery();
                    con.Close();

                    DataTable maxproductcodeid = MyCommonfile.selectBZ(" SELECT max(id) as maxcodeid  From ProductMasterCodeonsatelliteserverTbl ");

                    string strftpdetail = " Select * From ProductMasterCodeonsatelliteserverTbl Where Id='" + maxproductcodeid.Rows[0]["maxcodeid"].ToString() + "'  ";
                    SqlCommand cmdftpdetail = new SqlCommand(strftpdetail, connCompserver);
                    DataTable dtftpdetail = new DataTable();
                    SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
                    adpftpdetail.Fill(dtftpdetail);
                    if (dtftpdetail.Rows.Count == 0)
                    {
                        string strserverinsert = " Insert into ProductMasterCodeonsatelliteserverTbl (ID,ProductMastercodeID,ServerID,Successfullyuploadedtoserver,Physicalpath,filename,DownloadStart,DownloadFinish) values  ('" + maxproductcodeid.Rows[0]["maxcodeid"].ToString() + "','" + dtmastcode.Rows[0]["ID"].ToString() + "','" + comsid + "','0','" + ftpphysicalpath + "','" + filename + "','0','0')";//,'" + sqlservernameip + "','" + sqlinstancename + "'
                        SqlCommand cmdserverinsert = new SqlCommand(strserverinsert, connCompserver);
                        if (connCompserver.State.ToString() != "Open")
                        {
                            connCompserver.Open();
                        }
                        cmdserverinsert.ExecuteNonQuery();
                        connCompserver.Close();
                    }
                    else
                    {
                        string strProdMastServ = " Update ProductMasterCodeonsatelliteserverTbl SET Successfullyuploadedtoserver='" + dtftpdetail.Rows[0]["Successfullyuploadedtoserver"].ToString() + "' where ID='" + dtftpdetail.Rows[0]["ID"].ToString() + "'";
                        SqlCommand cmdProdMastServ = new SqlCommand(strProdMastServ, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdProdMastServ.ExecuteNonQuery();
                        con.Close();
                    }
                }
                #endregion

                DataTable dtlice = MyCommonfile.selectBZ(" SELECT dbo.ProductMasterCodeonsatelliteserverTbl.ID,dbo.CodeTypeTbl.CodeTypeCategoryId, dbo.ProductMasterCodeonsatelliteserverTbl.ProductMastercodeID, dbo.ProductMasterCodeonsatelliteserverTbl.ServerID, dbo.ProductMasterCodeonsatelliteserverTbl.Successfullyuploadedtoserver, dbo.ProductMasterCodeonsatelliteserverTbl.Physicalpath, dbo.ProductMasterCodeonsatelliteserverTbl.filename, dbo.ProductMasterCodeTbl.ProductVerID, dbo.ProductMasterCodeTbl.CodeTypeID, dbo.ProductMasterCodeTbl.codeversionnumber, dbo.ProductCodeDetailTbl.Id AS CodedetailID ,dbo.ProductCodeDetailTbl.CodeTypeName,dbo.ProductCodeDetailTbl.CompanyDefaultData,dbo.ProductCodeDetailTbl.BusiwizSynchronization, dbo.ProductCodeDetailTbl.AdditionalPageInserted,dbo.WebsiteMaster.DNSserver, dbo.WebsiteMaster.DNSname FROM  dbo.ProductMasterCodeonsatelliteserverTbl INNER JOIN dbo.ProductMasterCodeTbl ON dbo.ProductMasterCodeonsatelliteserverTbl.ProductMastercodeID = dbo.ProductMasterCodeTbl.ID INNER JOIN dbo.CodeTypeTbl ON dbo.ProductMasterCodeTbl.CodeTypeID = dbo.CodeTypeTbl.ID INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id INNER JOIN dbo.WebsiteMaster ON dbo.CodeTypeTbl.WebsiteID = dbo.WebsiteMaster.ID where  dbo.ProductMasterCodeTbl.ID='" + dtmastcode.Rows[0]["ID"].ToString() + "'  and dbo.WebsiteMaster.VersionInfoId='" + VersionInfoMasterId + "' and dbo.ProductMasterCodeonsatelliteserverTbl.ServerID='" + comsid + "'  ");
                if (dtlice.Rows.Count > 0)
                {
                    string ProductCodeDetailId = dtlice.Rows[0]["CodedetailID"].ToString();
                    string CodeTypeCategoryId = dtlice.Rows[0]["CodeTypeCategoryId"].ToString();
                    string CodeTypeName = dtlice.Rows[0]["CodeTypeName"].ToString();
                    string dnsname = dtlice.Rows[0]["DNSname"].ToString(); 
                    string DNSserverid = dtlice.Rows[0]["DNSserver"].ToString();
                  
                    string ProductMastercodeID = dtlice.Rows[0]["ProductMastercodeID"].ToString();
                    string YourDomaiUrl = "";                    
                    string str = " SELECT CompanyMaster.*,PortalMasterTbl.id as portlID, PortalMasterTbl.PortalName,PricePlanMaster.VersionInfoMasterId,PricePlanMaster.Producthostclientserver, dbo.ClientMaster.ServerId AS ClientServerid from dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId  where CompanyLoginId='" + compid + "' ";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable ds = new DataTable();
                    adp.Fill(ds);
                    if (ds.Rows.Count > 0)
                    {  
                        Boolean AdditionalPageInserted = false;
                        Boolean CompanyDefaultData = false;
                        Boolean BusiwizSynchronization = false; 
                        try
                        {
                            AdditionalPageInserted = Convert.ToBoolean(dtlice.Rows[0]["AdditionalPageInserted"].ToString());                                
                        }
                        catch
                        {
                        }
                        #region
                        string codetypeid = dtmastcode.Rows[0]["CodeTypeID"].ToString();
                        string codeversionno = dtmastcode.Rows[0]["codeversionnumber"].ToString();
                        string serverconnnstr = connCompserver.ConnectionString;//Start--------Company's Server Connectionstring
                        //Start ............................Client Code Path
                        string Client_latestcodeZipFilePath = dtmastcode.Rows[0]["TemporaryPath"].ToString().Replace("\\\\", "\\");//ex: D:\ALLNEWCOMPANYMASTERCOPY\35\IJobcenterMaster\WWW\PublishCode\2692017183317\10090_1_9078_1_262017918309.zip
                        string zipfolder = Path.GetFileName(Client_latestcodeZipFilePath.TrimEnd(Path.DirectorySeparatorChar));
                        if (File.Exists(Client_latestcodeZipFilePath))
                        {
                            DataTable DTNeedcode = MyCommonfile.selectBZ(" SELECT * From CompanyCreationNeedCode Where CodeTypeId='" + codetypeid + "' and CodeVersionNo='" + codeversionno + "' and CompanyLoginId='" + compid + "' and ProductVersionId='" + VersionInfoMasterId + "' ");
                            if (DTNeedcode.Rows.Count == 0)
                            {
                                bool DNSAddingStatus = false;
                                CompanyRelated.Insert_CompanyCreationNeedCode(compid, codetypeid, CodeTypeCategoryId, ProductCodeDetailId, codeversionno, VersionInfoMasterId, ftpphysicalpath, zipfolder, dnsname, CodeTypeName, AdditionalPageInserted, Client_latestcodeZipFilePath, CompanyDefaultData, BusiwizSynchronization, "", "", dtlice.Rows[0]["Successfullyuploadedtoserver"].ToString(), ProductMastercodeID, DNSserverid, DNSAddingStatus, Comp_IISPath);
                                DataTable Dmaxcodeid = selectSer(" SELECT max(Id) as maxid  From CompanyCreationNeedCode ");
                                if (DTNeedcode.Rows.Count == 0)
                                {
                                    Insert_CompanyCreationNeedCode_Server(compid, codetypeid, CodeTypeCategoryId, ProductCodeDetailId, codeversionno, VersionInfoMasterId, ftpphysicalpath, zipfolder, dnsname, CodeTypeName, AdditionalPageInserted, Client_latestcodeZipFilePath, CompanyDefaultData, BusiwizSynchronization, "", "", dtlice.Rows[0]["Successfullyuploadedtoserver"].ToString(), ProductMastercodeID, Dmaxcodeid.Rows[0]["maxid"].ToString(), DNSserverid, DNSAddingStatus, Comp_IISPath);
                                }
                            }
                        }
                        else
                        {
                            lblmsg.Text = " File not available at this path " + Client_latestcodeZipFilePath;
                        }
                        #endregion
                    }                   
                }
                }
                else
                {
                    lblmsg.Text = " Client's server or ProductMasterCodeTbl Table records not available";
                }
            }           
        }
    }
    protected void Database_Latest_Code_Insert(string compid, string comsid, string VersionInfoMasterId, string STRconnCompserver, string Comp_Ser_iispath, string Comp_Ser_Masterpath, string serverurl, string ClientServerid, string DefaultMdfpath, string DefaultLdfpath, string sqlservernameip, string sqlinstancename)
    {
        // database        
        // Find All Product code and versionno here ie. OADB , USERLOG, EXTMSGDB Etc
        DataTable dsmaxiddatabase = MyCommonfile.selectBZ(" SELECT   dbo.ProductMasterCodeTbl.ProductVerID, dbo.ProductMasterCodeTbl.CodeTypeID, MAX(dbo.ProductMasterCodeTbl.codeversionnumber) AS codeversionnumber, dbo.ProductCodeDetailTbl.CodeTypeName FROM dbo.ProductMasterCodeTbl INNER JOIN dbo.CodeTypeTbl ON dbo.CodeTypeTbl.ID = dbo.ProductMasterCodeTbl.CodeTypeID INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ProductCodeDetailTbl.Id = dbo.CodeTypeTbl.ProductCodeDetailId where ProductCodeDetailTbl.Active=1 and ProductMasterCodeTbl.ProductVerID='" + VersionInfoMasterId + "'  and CodeTypeCategory.CodeMasterNo In ('2')  GROUP BY dbo.ProductMasterCodeTbl.ProductVerID, dbo.ProductMasterCodeTbl.CodeTypeID, dbo.ProductCodeDetailTbl.CodeTypeName  ");       
        if (dsmaxiddatabase.Rows.Count > 0)
        {
            foreach (DataRow drmaxdb in dsmaxiddatabase.Rows)
            {
                DataTable dtmastcode = MyCommonfile.selectBZ(" SELECT * From ProductMasterCodeTbl Where codeversionnumber='" + drmaxdb["codeversionnumber"].ToString() + "' and CodeTypeID='" + drmaxdb["CodeTypeID"].ToString() + "' ");
                DataTable dtsercheck = MyCommonfile.selectBZ(" SELECT * From ProductMasterCodeonsatelliteserverTbl Where ProductMastercodeID='" + dtmastcode.Rows[0]["ID"].ToString() + "' and dbo.ProductMasterCodeonsatelliteserverTbl.ServerID='" + comsid + "' ");
                string filename = dtmastcode.Rows[0]["filename"].ToString();
                string ftpphysicalpath = Comp_Ser_Masterpath + "\\" + filename;
                if (dtsercheck.Rows.Count == 0)
                {
                    string strsatelliteserverinsert = " Insert into ProductMasterCodeonsatelliteserverTbl(ProductMastercodeID,ServerID,Successfullyuploadedtoserver,Physicalpath,filename) values ('" + dtmastcode.Rows[0]["ID"].ToString() + "','" + comsid + "','0','" + ftpphysicalpath + "','" + filename + "')";
                    SqlCommand cmdsatelliteserverinsert = new SqlCommand(strsatelliteserverinsert, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdsatelliteserverinsert.ExecuteNonQuery();
                    con.Close();
                }                
                //dtsercheck = MyCommonfile.selectBZ(" SELECT max(id) as maxcodeid  From ProductMasterCodeonsatelliteserverTbl ");
                dtsercheck = MyCommonfile.selectBZ(" SELECT * From ProductMasterCodeonsatelliteserverTbl Where ProductMastercodeID='" + dtmastcode.Rows[0]["ID"].ToString() + "' and dbo.ProductMasterCodeonsatelliteserverTbl.ServerID='" + comsid + "' ");
                string strftpdetails = " Select * From ProductMasterCodeonsatelliteserverTbl Where Id='" + dtsercheck.Rows[0]["Id"].ToString() + "'  ";
                SqlCommand cmdftpdetail = new SqlCommand(strftpdetails, connCompserver);
                    DataTable dtftpdetail = new DataTable();
                    SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
                    adpftpdetail.Fill(dtftpdetail);
                    if (dtftpdetail.Rows.Count == 0)
                    {
                        string strserverinsert = " Insert into ProductMasterCodeonsatelliteserverTbl (ID,ProductMastercodeID,ServerID,Successfullyuploadedtoserver,Physicalpath,filename,DownloadStart,DownloadFinish) values  ('" + dtsercheck.Rows[0]["Id"].ToString() + "','" + dtmastcode.Rows[0]["ID"].ToString() + "','" + comsid + "','0','" + ftpphysicalpath + "','" + filename + "','0','0')";//,'" + sqlservernameip + "','" + sqlinstancename + "'
                        SqlCommand cmdserverinsert = new SqlCommand(strserverinsert, connCompserver);
                        if (connCompserver.State.ToString() != "Open")
                        {
                            connCompserver.Open();
                        }
                        cmdserverinsert.ExecuteNonQuery();
                        connCompserver.Close();
                    }
                    else
                    {
                        //For License Table Updating Status
                        string strProdMastServ = " Update ProductMasterCodeonsatelliteserverTbl SET Successfullyuploadedtoserver='" + dtftpdetail.Rows[0]["Successfullyuploadedtoserver"].ToString() + "' where ID='" + dtftpdetail.Rows[0]["ID"].ToString()  + "'";
                        SqlCommand cmdProdMastServ = new SqlCommand(strProdMastServ, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdProdMastServ.ExecuteNonQuery();
                        con.Close();
                    }                
                
                DataTable dtlice = MyCommonfile.selectBZ(" SELECT dbo.ProductMasterCodeonsatelliteserverTbl.ID,dbo.CodeTypeTbl.CodeTypeCategoryId, dbo.ProductMasterCodeonsatelliteserverTbl.ProductMastercodeID, dbo.ProductMasterCodeonsatelliteserverTbl.ServerID, dbo.ProductMasterCodeonsatelliteserverTbl.Successfullyuploadedtoserver, dbo.ProductMasterCodeonsatelliteserverTbl.Physicalpath, dbo.ProductMasterCodeonsatelliteserverTbl.filename, dbo.ProductMasterCodeTbl.ProductVerID, dbo.ProductMasterCodeTbl.CodeTypeID, dbo.ProductMasterCodeTbl.codeversionnumber, dbo.ProductCodeDetailTbl.Id AS CodedetailID ,dbo.ProductCodeDetailTbl.CodeTypeName,dbo.ProductCodeDetailTbl.CompanyDefaultData,dbo.ProductCodeDetailTbl.BusiwizSynchronization, dbo.ProductCodeDetailTbl.AdditionalPageInserted FROM  dbo.ProductMasterCodeonsatelliteserverTbl INNER JOIN dbo.ProductMasterCodeTbl ON dbo.ProductMasterCodeonsatelliteserverTbl.ProductMastercodeID = dbo.ProductMasterCodeTbl.ID INNER JOIN dbo.CodeTypeTbl ON dbo.ProductMasterCodeTbl.CodeTypeID = dbo.CodeTypeTbl.ID INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where  dbo.ProductMasterCodeTbl.ID='" + dtmastcode.Rows[0]["ID"].ToString() + "' and dbo.CodeTypeTbl.ProductVersionId='" + VersionInfoMasterId + "' and dbo.ProductMasterCodeonsatelliteserverTbl.ServerID='" + comsid + "' ");              
                if (dtlice.Rows.Count > 0)
                {
                    
                    string Client_latestcodeZipFilePath = dtmastcode.Rows[0]["TemporaryPath"].ToString().Replace("\\\\", "\\");
                    string strftpdetail = "  ";
                    string codetypeid = drmaxdb["CodeTypeID"].ToString();
                    string codeversionno = drmaxdb["codeversionnumber"].ToString();
                   
                    string dnsname = "";
                    string tocopymdfldffilenameOnSer = "";
                    string ProductCodeDetailId = dtlice.Rows[0]["CodedetailID"].ToString();
                    string CodeTypeCategoryId = dtlice.Rows[0]["CodeTypeCategoryId"].ToString(); //"2" //Database
                    string CodeTypeName = dtlice.Rows[0]["CodeTypeName"].ToString();
                    Boolean CompanyDefaultData = Convert.ToBoolean(dtlice.Rows[0]["CompanyDefaultData"].ToString());
                    Boolean AdditionalPageInserted = Convert.ToBoolean(dtlice.Rows[0]["AdditionalPageInserted"].ToString());
                    Boolean BusiwizSynchronization = Convert.ToBoolean(dtlice.Rows[0]["BusiwizSynchronization"].ToString());
                    // string ldffilexten = Path.GetExtension("2056_2_2_172_201672933747982056_2_3_6_201310372975512056_2_3_4_2013812102621554blankcopytest_log.ldf");
                    string ldffilexten = dtlice.Rows[0]["filename"].ToString();
                    ldffilexten = Path.GetExtension(ldffilexten);
                    string mastersourcepathOnSer = dtlice.Rows[0]["Physicalpath"].ToString();
                    if (ldffilexten == ".LDF" || ldffilexten == ".ldf")
                    {
                        tocopymdfldffilenameOnSer = DefaultLdfpath;
                    }
                    if (ldffilexten == ".MDF" || ldffilexten == ".mdf")
                    {
                        tocopymdfldffilenameOnSer = DefaultMdfpath;
                    }
                    DataTable DTNeedcode = MyCommonfile.selectBZ(" SELECT * From CompanyCreationNeedCode Where CodeTypeId='" + codetypeid + "' and CodeVersionNo='" + codeversionno + "' and CompanyLoginId='" + compid + "' and ProductVersionId='" + VersionInfoMasterId + "' ");
                    if (DTNeedcode.Rows.Count == 0)
                    {
                        CompanyRelated.Insert_CompanyCreationNeedCode(compid, codetypeid, CodeTypeCategoryId, ProductCodeDetailId, codeversionno, VersionInfoMasterId, Comp_Ser_Masterpath + "\\" + dtmastcode.Rows[0]["filename"].ToString(), dtmastcode.Rows[0]["filename"].ToString(), dnsname, CodeTypeName, AdditionalPageInserted, Client_latestcodeZipFilePath, CompanyDefaultData, BusiwizSynchronization, sqlservernameip, sqlinstancename, dtlice.Rows[0]["Successfullyuploadedtoserver"].ToString(), dtmastcode.Rows[0]["ID"].ToString(), "", false, tocopymdfldffilenameOnSer);
                        DataTable Dmaxcodeid = selectSer(" SELECT max(Id) as maxid  From CompanyCreationNeedCode ");
                        if (DTNeedcode.Rows.Count == 0)
                        {
                            Insert_CompanyCreationNeedCode_Server(compid, codetypeid, CodeTypeCategoryId, ProductCodeDetailId, codeversionno, VersionInfoMasterId, Comp_Ser_Masterpath + "\\" + dtmastcode.Rows[0]["filename"].ToString(), dtmastcode.Rows[0]["filename"].ToString(), dnsname, CodeTypeName, AdditionalPageInserted, Client_latestcodeZipFilePath, CompanyDefaultData, BusiwizSynchronization, sqlservernameip, sqlinstancename, dtlice.Rows[0]["Successfullyuploadedtoserver"].ToString(), dtmastcode.Rows[0]["ID"].ToString(), Dmaxcodeid.Rows[0]["maxid"].ToString(), "", false, tocopymdfldffilenameOnSer);
                        }
                    }                   
                }
            }
        }
    }


    protected void Insert_CompanyCreationNeedCode_Server(string comploginid, string codetypeid, string CodeTypeCategoryId, string ProductCodeDetailId, string codeversionno, string VersionInfoMasterId, string Physicalpath, string zipfolder, string dnsname, string CodeTypeName, Boolean AdditionalPageInserted, string ClientMasterURL, Boolean IsDefaultDatabase, Boolean BusiwizSynchronization, string sqlservernameip, string sqlinstancename, string Successfullyuploadedtoserver, string ProductMastercodeID, string Id, string DNSserid, bool DNSAddingStatus, string Comp_IISPath)
    {
        if (connCompserver.State.ToString() != "Open")
        {
            connCompserver.Open();
        }
        SqlCommand cmd = new SqlCommand("CompanyCreationNeedCode_AddDelUpdtSelect", connCompserver);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StatementType", "Insert");
        cmd.Parameters.AddWithValue("@Id", Id);
        cmd.Parameters.AddWithValue("@CompanyLoginId", comploginid);
        cmd.Parameters.AddWithValue("@CodeTypeId", codetypeid);
        cmd.Parameters.AddWithValue("@CodeTypeCategoryId", CodeTypeCategoryId);
        cmd.Parameters.AddWithValue("@CodeDetailId", ProductCodeDetailId);
        cmd.Parameters.AddWithValue("@CodeVersionNo", codeversionno);
        cmd.Parameters.AddWithValue("@ProductVersionId", VersionInfoMasterId);
        cmd.Parameters.AddWithValue("@Physicalpath", Physicalpath);
        cmd.Parameters.AddWithValue("@filename", zipfolder);       
        cmd.Parameters.AddWithValue("@DNSname", dnsname);
        cmd.Parameters.AddWithValue("@CodeTypeName", CodeTypeName);
        cmd.Parameters.AddWithValue("@YourDomaiUrl", "");
        cmd.Parameters.AddWithValue("@IsDefaultDatabase", IsDefaultDatabase);
        cmd.Parameters.AddWithValue("@AdditionalPageInserted", AdditionalPageInserted);
        cmd.Parameters.AddWithValue("@Successfullystatus", false);
        cmd.Parameters.AddWithValue("@DownloadCompanyCode", false);
        cmd.Parameters.AddWithValue("@ClientMasterURL", ClientMasterURL);
        cmd.Parameters.AddWithValue("@BusiwizSynchronization", BusiwizSynchronization);
        cmd.Parameters.AddWithValue("@Successfullyuploadedtoserver", Successfullyuploadedtoserver);
        cmd.Parameters.AddWithValue("@ProductMastercodeID", ProductMastercodeID);
        cmd.Parameters.AddWithValue("@DNSserid", DNSserid);
        cmd.Parameters.AddWithValue("@DNSAddingStatus", DNSAddingStatus);
        cmd.Parameters.AddWithValue("@Comp_IISPath", Comp_IISPath);        
        cmd.ExecuteNonQuery();
        connCompserver.Close();
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
            }
            catch (IOException ex)
            {
            }
        }
    }  
    protected void CreateZIP(string Directorypathname, string zipsavepath)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AddDirectory(Directorypathname);
            zip.Save(zipsavepath + ".zip");
        }
    }

    protected void InsertServerMaster(string sid)
    {
        DataTable dsserver = MyCommonfile.selectBZ(" Select * From ServerMasterTbl Where id='" + sid + "'");
        if (dsserver.Rows.Count > 0)
        {
            string strftpdetail = " SELECT * From  CompanyServer Where ServerID='" + sid + "' ";
            SqlCommand cmdftpdetail = new SqlCommand(strftpdetail, connCompserver);
            DataTable dtftpdetail = new DataTable();
            SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
            adpftpdetail.Fill(dtftpdetail);
            if (dtftpdetail.Rows.Count > 0)
            {
                //Settellite Server Table
                string inpayper = " Update CompanyServer Set servername='" + dsserver.Rows[0]["servername"].ToString() + "', port='" + dsserver.Rows[0]["port"].ToString() + "', serverdefaultpathforiis='" + dsserver.Rows[0]["serverdefaultpathforiis"].ToString() + "' ,serverdefaultpathformdf='" + dsserver.Rows[0]["serverdefaultpathformdf"].ToString() + "',serverdefaultpathforfdf='" + dsserver.Rows[0]["serverdefaultpathforfdf"].ToString() + "',folderpathformastercode='" + dsserver.Rows[0]["folderpathformastercode"].ToString() + "' ,PublicIp='" + dsserver.Rows[0]["PublicIp"].ToString() + "', Sapassword='" + dsserver.Rows[0]["Sapassword"].ToString() + "', InDomain='" + dsserver.Rows[0]["InDomain"].ToString() + "', DomainName='" + dsserver.Rows[0]["DomainName"].ToString() + "', DomainGroupName='" + dsserver.Rows[0]["DomainGroupName"].ToString() + "',ComputerName='" + dsserver.Rows[0]["ComputerName"].ToString() + "',PortforCompanymastersqlistance='" + dsserver.Rows[0]["PortforCompanymastersqlistance"].ToString() + "' ,Sqlinstancename='" + dsserver.Rows[0]["Sqlinstancename"].ToString() + "',DefaultDatabaseName='" + dsserver.Rows[0]["DefaultDatabaseName"].ToString() + "',DefaultsqlInstance='" + dsserver.Rows[0]["DefaultsqlInstance"].ToString() + "',sqlurl='" + dsserver.Rows[0]["sqlurl"].ToString() + "',Enckey='" + dsserver.Rows[0]["Enckey"].ToString() + "'  where ServerID='" + dsserver.Rows[0]["Id"].ToString() + "'";
                SqlCommand cmdinpa = new SqlCommand(inpayper, connCompserver);
                if (connCompserver.State.ToString() != "Open")
                {
                    connCompserver.Open();
                }
                cmdinpa.ExecuteNonQuery();
                connCompserver.Close();
            }
            else
            {
                //Settellite Server Table
                string strserverinsert = " Insert into CompanyServer (servername ,port,serverdefaultpathforiis,serverdefaultpathformdf,serverdefaultpathforfdf, folderpathformastercode, PublicIp,Sapassword,InDomain,DomainName,DomainGroupName,ServerID,ComputerName,PortforCompanymastersqlistance ,Sqlinstancename ,DefaultDatabaseName, DefaultsqlInstance ,sqlurl,Enckey) values " +
                       " ('" + dsserver.Rows[0]["servername"].ToString() + "','" + dsserver.Rows[0]["port"].ToString() + "','" + dsserver.Rows[0]["serverdefaultpathforiis"].ToString() + "','" + dsserver.Rows[0]["serverdefaultpathformdf"].ToString() + "','" + dsserver.Rows[0]["serverdefaultpathforfdf"].ToString() + "','" + dsserver.Rows[0]["folderpathformastercode"].ToString() + "','" + dsserver.Rows[0]["PublicIp"].ToString() + "','" + dsserver.Rows[0]["Sapassword"].ToString() + "','" + dsserver.Rows[0]["InDomain"].ToString() + "','" + dsserver.Rows[0]["DomainName"].ToString() + "','" + dsserver.Rows[0]["DomainGroupName"].ToString() + "','" + dsserver.Rows[0]["Id"].ToString() + "','" + dsserver.Rows[0]["ComputerName"].ToString() + "','" + dsserver.Rows[0]["PortforCompanymastersqlistance"].ToString() + "','" + dsserver.Rows[0]["Sqlinstancename"].ToString() + "','" + dsserver.Rows[0]["DefaultDatabaseName"].ToString() + "','" + dsserver.Rows[0]["DefaultsqlInstance"].ToString() + "','" + dsserver.Rows[0]["sqlurl"].ToString() + "','" + dsserver.Rows[0]["Enckey"].ToString() + "')";
                SqlCommand cmdserverinsert = new SqlCommand(strserverinsert, connCompserver);
                if (connCompserver.State.ToString() != "Open")
                {
                    connCompserver.Open();
                }
                cmdserverinsert.ExecuteNonQuery();
                connCompserver.Close();
            }
        }
    }

    protected DataTable selectSer(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, connCompserver);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
  

    //GV
    protected void FillGrid_ComCreNeedCode()
    {
        String st1 = "";
        string strneedcode = "";
        strneedcode = " SELECT dbo.CompanyCreationNeedCode.CompanyLoginId, dbo.CompanyCreationNeedCode.Id, dbo.CompanyCreationNeedCode.Successfullyuploadedtoserver, dbo.CodeTypeTbl.Name, dbo.CompanyCreationNeedCode.filename FROM dbo.CompanyCreationNeedCode INNER JOIN dbo.CodeTypeTbl ON dbo.CompanyCreationNeedCode.CodeTypeId = dbo.CodeTypeTbl.ID INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id Where CompanyLoginId='" + lbl_compid.Text + "' ";//and Successfullyuploadedtoserver='0' 
        SqlCommand cmdgrid = new SqlCommand(strneedcode, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlDataAdapter dtpgrid = new SqlDataAdapter(cmdgrid);
        DataTable dtgrid = new DataTable();
        dtpgrid.Fill(dtgrid);       
        if (dtgrid.Rows.Count > 0)
        {
            Gv_Com_Cre_Need_Code.DataSource = dtgrid;
            Gv_Com_Cre_Need_Code.DataBind();
            Gv_Com_Cre_Need_Code.Visible = true;
            pnl_downloadstatus.Visible = true;
        }
        else
        {
            Gv_Com_Cre_Need_Code.DataSource = null;
            Gv_Com_Cre_Need_Code.DataBind();
            pnl_downloadstatus.Visible = false;
        }
    }
    protected void Gv_Com_Cre_Need_Code_RowEditing1(object sender, GridViewEditEventArgs e)
    {

    }
    protected void Gv_Com_Cre_Need_Code_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_Com_Cre_Need_Code.PageIndex = e.NewPageIndex;
        FillGrid_ComCreNeedCode();
    }
    protected void Gv_Com_Cre_Need_Code_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void Gv_Com_Cre_Need_Code_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
    }
    protected void Gv_Com_Cre_Need_Code_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl_id = (Label)e.Row.FindControl("lbl_id");
            Label lbl_CompanyLoginId = (Label)e.Row.FindControl("lbl_CompanyLoginId");            
            DataTable dtneedcode =MyCommonfile.selectBZ(" SELECT * From CompanyCreationNeedCode Where ID='" + lbl_id.Text + "'");
            if (dtneedcode.Rows.Count > 0)
            {
                Boolean successfullyuploaded = Convert.ToBoolean(dtneedcode.Rows[0]["Successfullyuploadedtoserver"].ToString());
                         
            }
        }
    }
    //----------------------------------------------------
  


    



}
