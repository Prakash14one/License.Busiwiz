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
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Management;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Net.Security;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.DirectoryServices;
using System.Web.Configuration;
using System.Linq;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Xml.Linq;
using Ionic.Zip;
using ZipFramework;

public partial class _Default : System.Web.UI.Page
{
   // SqlConnection con = new SqlConnection();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)          
    {
        //using (ZipFile zip = ZipFile.Read(physicalpath + ".zip"))
        //{
        //    zip.ExtractAll(temppath, ExtractExistingFileAction.OverwriteSilently);
        //}

        //ZIPFile zip = new ZIPFile("D://ZipTest//Test.zip");
        //FileEntry file = new FileEntry("D://ZipTest//Test.txt", Method.Deflated);
        //zip.AddFile(file);
        //zip.CreateZIP();
        
        
        
        
        
        
        
       // Script();

        
       
     
        //string remoteUrl = "http://localhost:53402/C3server/Test.aspx";
        //string firstName = "Mudassar";
        //string lastName = "Khan";
        //ASCIIEncoding encoding = new ASCIIEncoding();
        //string data = string.Format("FirstName={0}&LastName={1}", firstName, lastName);
        //byte[] bytes = encoding.GetBytes(data);
        //HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
        //httpRequest.Method = "POST";
        //httpRequest.ContentType = "application/x-www-form-urlencoded";
        //httpRequest.ContentLength = bytes.Length;
        //using (Stream stream = httpRequest.GetRequestStream())
        //{
        //    stream.Write(bytes, 0, bytes.Length);
        //    stream.Close();
        //}
        //string text = "Hello";
        //Label4.Text = "Good";






       //con = Myfile.serverconnstring();
       //con.Open(); 
        //1. Return the "text" variable to the client that requested this page
        //ServerConn1();       
        //Response.ContentType = "text/plain";
        //Response.BufferOutput = false;
        //Response.BinaryWrite(GetBytes(text));
        //Response.Flush();
        //Response.Close();
        //Response.End();
       //string str = " update BusiwizMasterInfoTbl SET PaymentNotifyURL='http://license.busiwiz.com/busiwizlicensekeygeneration.aspx?' Where PaymentNotifyURL='http://licence.busiwiz.com/busiwizlicensekeygeneration.aspx?' ";
       //S1();
       //S2();
       //S3();           
       // FTPCALL1();
      //  con.ConnectionString = @"Data Source =TCP:192.168.1.219,30000; Initial Catalog = jobcenter.BUSICONTROLDB; User ID=sa; Password=06De1963++; Persist Security Info=true;";
      ////  con = PageConn.licenseconn(); 
      //  //con.ConnectionString = @"Data Source =TCP:C3_SQL.safestserver.com,30000; Initial Catalog = C3SATELLITESERVER; User ID=sa; Password=06De1963++; Persist Security Info=true;";             
    }


















    protected void S1()
    {
     string   str = "Delete From Satellite_Server_Sync_Job_Details";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void S2()
    {
        
        string str = "Delete From Satelite_Server_Sync_Log_Deatils";
        
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void S3()
    {        
        string str = "Delete From Satelitte_Server_Sync_Job_Master";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }
    static byte[] GetBytes(string str)
    {
        byte[] bytes = new byte[str.Length * sizeof(char)];
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }
    //public static void FTPCALL1()
    //{
    //    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://192.186.117.11:21/");
    //    ftpRequest.Credentials = new NetworkCredential("FTPupload", "Om2012++");
    //    ftpRequest.UseBinary = false;
    //    ftpRequest.UsePassive = true;

    //    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
    //    FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
    //    StreamReader streamReader = new StreamReader(response.GetResponseStream());

    //    List<string> directories = new List<string>();
    //    string line = streamReader.ReadLine();
    //    while (!string.IsNullOrEmpty(line))
    //    {
    //        directories.Add(line);
    //        line = streamReader.ReadLine();
    //    }
    //    streamReader.Close();
    //    //response.Close();
    //    //ftpRequest = null;

    //    using (WebClient ftpClient = new WebClient())
    //    {
    //        ftpClient.Credentials = new System.Net.NetworkCredential("FTPupload", "Om2012++");
    //        //string strDesPath = @"D:\Test\";
    //        string strFTPPath = "ftp://192.186.117.11:21/";
    //        List<string> directories2 = new List<string>();

    //        for (int i = 0; i < directories.Count; i++)
    //        {
    //            string strFile = string.Empty;
    //            strFile = directories[i].ToString();
    //            if (strFile.Length > 3)
    //            {
    //                string strDesPath = @"D:\" + strFile;

    //                if (directories[i].Contains("."))
    //                {
    //                    FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.Create(strFTPPath.ToString() + strFile.ToString());

    //                    oFTP.Credentials = new NetworkCredential("FTPupload", "Om2012++");
    //                    oFTP.UseBinary = false;
    //                    oFTP.UsePassive = true;
    //                    oFTP.Method = WebRequestMethods.Ftp.DownloadFile;

    //                    FtpWebResponse response1 = null;
    //                    response1 = (FtpWebResponse)oFTP.GetResponse();
    //                    Stream responseStream = response1.GetResponseStream();


    //                    if (File.Exists(strDesPath))
    //                    {
    //                        //File.Delete(strDesPath);
    //                        System.IO.File.Delete(strDesPath);
    //                    }

    //                    FileStream fs = new FileStream(strDesPath, FileMode.CreateNew);
    //                    Byte[] buffer = new Byte[2047];
    //                    int read = 1;
    //                    while (read != 0)
    //                    {
    //                        read = responseStream.Read(buffer, 0, buffer.Length);
    //                        fs.Write(buffer, 0, read);
    //                    }

    //                    responseStream.Close();
    //                    fs.Flush();
    //                    fs.Close();
    //                    responseStream.Close();
    //                    response1.Close();

    //                    oFTP = null;
    //                }
    //            }
    //        }
    //    }
    //    //response.Close();
    //    //ftpRequest = null;
    //}


    protected void ServerConn1()
    {
        string serverid = "5";
        string strftpdetail = " SELECT * from ServerMasterTbl where Id='" + serverid + "'";
        SqlCommand cmdftpdetail = new SqlCommand(strftpdetail, con);
        DataTable dtftpdetail = new DataTable();
        SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
        adpftpdetail.Fill(dtftpdetail);
        if (dtftpdetail.Rows.Count > 0)
        {
            // string ftpphysicalpath = dtftpdetail.Rows[0]["folderpathformastercode"].ToString() + "\\" + filename;
            string serversqlserverip = dtftpdetail.Rows[0]["sqlurl"].ToString();
            string serversqlinstancename = dtftpdetail.Rows[0]["DefaultsqlInstance"].ToString();
            string serversqldbname = dtftpdetail.Rows[0]["DefaultDatabaseName"].ToString();
            string serversqlpwd = dtftpdetail.Rows[0]["Sapassword"].ToString();
            string serversqlport = dtftpdetail.Rows[0]["port"].ToString();
            SqlConnection connserver = new SqlConnection();
            connserver.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";
            string aa = connserver.ConnectionString;
            fileClassGenerated("E:\\PRAKASH\\AHM LOCAL SERVER\\License.busiwiz.com_AhmServer", aa, "aaa", "aaaa");
        }
    }


    protected void fileClassGenerated(string filepath, string serconn, string compenckey, string serkey)
    {
        string HashKey = "";
        //encstr = CreateLicenceKey(out HashKey);

        string fileLoc = filepath + "\\Myfile.cs";
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
    protected void Script()
    {
        FileInfo file = new FileInfo("I:\\script1.sql");
        string script = file.OpenText().ReadToEnd();      
        SqlCommand command = new SqlCommand(script, con);
        command.Connection.Open();
        command.ExecuteNonQuery();
        command.Connection.Close();
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        //string dbName = txtdbname.text;
        //string strCreatecmd = "create database " + dbName + "";
        //SqlCommand cmd = new SqlCommand(strCreatecmd, con);
        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();
        //// Code to execute SQL script file .i.e.(create tables / storedprocedure /views on Ms-SQL)
        //// generatescript.sql is sql script generated
        //// placed under Add_data folder in my application
        //FileInfo file = new FileInfo(Server.MapPath("App_Datageneratescript.sql"));
        //string strscript = file.OpenText().ReadToEnd();
        //string strupdatescript = strscript.Replace("[databaseOldnameWhileSriptgenerate]", strdbname);
        //Server server = new Server(new ServerConnection(con));
        //server.ConnectionContext.ExecuteNonQuery(strupdatescript);
        //con.Close();
    }
}