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
using System.Data.SqlClient;
using System.Management;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Net;
using System.Security.Cryptography;
using System.Text;
                    public class SQLCommonfile
                    {
                        SqlConnection con;                     
                        public static string companykey = "aaa"; public static string serverkey = "aaaa";
                        public SQLCommonfile()
                        {
                            con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
                        }
                        public static string GenerateAndSaveFile(string FilenamewithExt, string TextInFile, string appcodepath)
                        {
                            string str = "";
                            string fileLoc = appcodepath + "\\" + FilenamewithExt;
                            using (StreamWriter sw = new StreamWriter(fileLoc))
                                sw.Write
                                    (@" " + TextInFile + " ");
                            return str;
                        }                      
                    }