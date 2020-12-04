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
                    public class MyCommonfile
                    {
                        SqlConnection con;                     
                        public static string companykey = "aaa"; public static string serverkey = "aaaa";
                        public MyCommonfile()
                        {
                            con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
                        }
                        public static SqlConnection licenseconn()
                        {
                            SqlConnection liceco = new SqlConnection();

                            liceco = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
                          
                            return liceco;
                        }
                        public static SqlConnection serverconnstring()
                        {
                            SqlConnection serverconnstri = new SqlConnection();
                            serverconnstri.ConnectionString = @"Data Source =C3SERVERMASTER,1401; Initial Catalog=C3SATELLITESERVER; User ID=Sa; Password=06De1963++; Persist Security Info=true;";                            
                            return serverconnstri;
                        }
                        public static DataTable selectBZ(string str)
                        {
                            SqlCommand cmdclnccdweb = new SqlCommand(str, licenseconn());
                            DataTable dtclnccdweb = new DataTable();
                            SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
                            adpclnccdweb.Fill(dtclnccdweb);
                            return dtclnccdweb;
                        }
                        public static string Companykey()
                        {                           
                            return companykey;
                        }
                        public static string Serverkey()
                        {                           
                            return serverkey;
                        }

                        public static string RandomeIntnumber(int passwordLength)
                        {
                            string allowedChars = "123456789123456789";

                            char[] chars = new char[passwordLength];
                            Random rd = new Random();
                            for (int i = 0; i < passwordLength; i++)
                            {
                                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                            }
                            return new string(chars);
                        }

                        public string CreateLicenceKey(out string HashKey)
                        {
                            string str = "";
                            string s1 = "";
                            string s2 = "";
                            string s3 = "";
                            string s4 = "";
                            s1 = DateTime.Now.ToString().GetHashCode().ToString("x").ToString();
                            if (s1.Length > 5)
                            {
                                s1 = s1.Substring(0, 5); //
                            }
                            else
                            {
                                s1 = s1 + "1";
                            }
                            s2 = DateTime.Now.Ticks.ToString("x").ToString();  //DateTime.Now.ToString().GetHashCode().ToString("x");
                            if (s2.Length > 9)
                            {
                                s2 = s2.Substring(4, 5); //
                            }
                            s3 = Guid.NewGuid().ToString().GetHashCode().ToString("x").ToString(); //DateTime.Now.ToString().GetHashCode().ToString("x");
                            if (s3.Length > 5)
                            {
                                s3 = s3.Substring(0, 5); //
                            }
                            s4 = RNGCharacterMask().ToString().Substring(0, 5); // DateTime.Now.ToString().GetHashCode().ToString("x");
                            if (s4.Length > 5)
                            {
                                s4 = s4.Substring(0, 5); //
                            }
                            string hashcode = "";
                            string s11 = "";
                            string s22 = "";
                            string s33 = "";
                            string s44 = "";
                            string s55 = "";
                            s11 = DateTime.Now.ToString().GetHashCode().ToString("x").ToString();
                            s22 = DateTime.Now.Ticks.ToString("x").ToString();  //DateTime.Now.ToString().GetHashCode().ToString("x");
                            s33 = Guid.NewGuid().ToString().GetHashCode().ToString("x").ToString(); //DateTime.Now.ToString().GetHashCode().ToString("x");
                            s44 = RNGCharacterMask().ToString(); // DateTime.Now.ToString().GetHashCode().ToString("x
                            s55 = RNGCharacterMask().ToString(); // DateTime.Now.ToString().GetHashCode().ToString("x
                            s11 = s11.Substring(s11.Length - 1, 1);
                            s22 = s22.Substring(s22.Length - 1, 1);
                            s33 = s33.Substring(s33.Length - 1, 1);
                            s44 = s44.Substring(s44.Length - 1, 1);
                            s55 = s55.Substring(s55.Length - 2, 1);
                            hashcode = s11 + s22 + s33 + s44 + s55;
                            str = s3.ToString() + "" + s2.ToString() + "" + s1.ToString() + "" + s4.ToString();
                            HashKey = hashcode.ToUpper();
                            return str;
                        }
                        private string RNGCharacterMask()
                        {
                            int maxSize = 12;
                            int minSize = 10;
                            char[] chars = new char[62];
                            string a;
                            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                            chars = a.ToCharArray();
                            int size = maxSize;
                            byte[] data = new byte[1];
                            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                            crypto.GetNonZeroBytes(data);
                            size = maxSize;
                            data = new byte[size];
                            crypto.GetNonZeroBytes(data);
                            StringBuilder result = new StringBuilder(size);
                            foreach (byte b in data)
                            { result.Append(chars[b % (chars.Length - 1)]); }
                            return result.ToString();
                        }


                        public static bool ext_Onlyzipfile_allow(string filename)
                        {
                            string[] validFileTypes = { ".zip", "zip", "Zip" };
                            string ext = System.IO.Path.GetExtension(filename);
                            bool isValidFile = true;
                            for (int i = 0; i < validFileTypes.Length; i++)
                            {

                                if (ext == "." + validFileTypes[i])
                                {
                                    isValidFile = true;
                                    break;
                                }
                            }
                            return isValidFile;
                        }

                        public static bool ext_OnlyMDFfile_allow(string filename)
                        {
                            string[] validFileTypes = { ".mdf", "MDF", "mdf" };
                            string ext = System.IO.Path.GetExtension(filename);
                            bool isValidFile = false;
                            for (int i = 0; i < validFileTypes.Length; i++)
                            {
                                if (ext == "." + validFileTypes[i])
                                {
                                    isValidFile = true;
                                    break;
                                }
                            }
                            return isValidFile;
                        }
                        public static bool ext_OnlyLDFfile_allow(string filename)
                        {
                            string[] validFileTypes = { ".ldf", "LDF", "ldf" };
                            string ext = System.IO.Path.GetExtension(filename);
                            bool isValidFile = false;
                            for (int i = 0; i < validFileTypes.Length; i++)
                            {
                                if (ext == "." + validFileTypes[i])
                                {
                                    isValidFile = true;
                                    break;
                                }
                            }
                            return isValidFile;
                        }

                    }