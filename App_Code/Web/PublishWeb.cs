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
                    public class PublishWeb
                    {
                        public static string serverconn = "Data Source =192.168.2.100,40000; Initial Catalog=C3SATELLITESERVER; User ID=Sa; Password=06De1963++; Persist Security Info=true;"; public static string companykey = "3d70f5cff23ed17ip8H9"; public static string serverkey = "c7171b1e96fc3bbZ8wAS";
                        public static double size = 0;
                        public PublishWeb()
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
                        public static Boolean Serverkey(string temppath, string temppathvirtualfilename, string outputpath, string keypath)
                        {
                            try
                            {
                                 //temppath = "I:\\DElete\\New\\Man";
                                 //temppathvirtualfilename = "Man";
                                 //outputpath = "I:\\DElete\\New\\Pub";
                                 //keypath = "C:\\Program Files (x86)\\Microsoft Visual Studio 9.0\\VC\\123.snk";
                                //-keyfile " + keypath + "
                                DirectoryInfo sourceDir = new DirectoryInfo("" + temppath + "");
                                double size = GetSizeDirectory(sourceDir);
                                double twpercsize = 0.20 * size;
                                string mspath = "C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\";
                                string mscompiler = "aspnet_compiler.exe";
                                string aptca = "I:\\DElete\\New\\A";
                                string fullcompilerpath = Path.Combine(mspath, mscompiler);
                                ProcessStartInfo startinfo = new ProcessStartInfo();
                                //   string virtualfilename = "/" + dateformat;
                                string argument = "-p " + temppath + " -v " + temppathvirtualfilename + " -u  -f " + outputpath + "  -errorstack ";// -aptca " + aptca + "
                                Process.Start(fullcompilerpath, argument);//.WaitForExit()
                                return true;
                            }
                            catch (Exception ex)
                            {
                                // lblmsg.Text = ex.ToString();
                                return false;
                            }  
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
                        public static DataTable selectserver(string str)
                        {
                            //SqlCommand cmdclnccdweb = new SqlCommand(str, Myfile.serverconnstring());
                            DataTable dtclnccdweb = new DataTable();
                            //SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
                            //adpclnccdweb.Fill(dtclnccdweb);
                            return dtclnccdweb;
                        }
                    }