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
                    public class LBCommonFun
                    {
                        SqlConnection con;                     
                        
                        public LBCommonFun()
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
                            serverconnstri.ConnectionString = @"Data Source =C3SERVERMASTER,30000; Initial Catalog=C3SATELLITESERVER; User ID=Sa; Password=06De1963++; Persist Security Info=true;";                            
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
                     
                       
                    }