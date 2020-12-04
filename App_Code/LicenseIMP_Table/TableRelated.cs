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
                    public class TableRelated
                    {
                        SqlConnection con;

                        public TableRelated()
                        {
                            con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
                        }
                        public static SqlConnection licenseconn()
                        {
                            SqlConnection liceco = new SqlConnection();
                            liceco = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);                          
                            return liceco;
                        }
                      
                        public static string GetForeinkeyName(String TableId)
                        {
                            string ForeinKey_PricplanID = GetTableID("PricePlanMaster");
                            string ForeignFieldWhere = "";
                            string strcln = " Select * From tablefielddetail Where TableId='" + TableId + "' and foreignkeytblid='" + ForeinKey_PricplanID + "' ";
                            SqlCommand cmdcln = new SqlCommand(strcln, PageConn.licenseconn());
                            DataTable dtcln = new DataTable();
                            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                            adpcln.Fill(dtcln);
                            if (dtcln.Rows.Count > 0)
                            {
                                ForeignFieldWhere = dtcln.Rows[0]["feildname"].ToString();   
                            }
                            return ForeignFieldWhere;
                        }

                        public static string GetTableID(String TableName)
                        {
                            string foreignwhere = "";
                            string foreignkeytblid = "";
                            string strcln = " Select * From ClientProductTableMaster Where TableName='" + TableName + "'  and VersionInfoId='32'";
                            SqlCommand cmdcln = new SqlCommand(strcln, PageConn.licenseconn());
                            DataTable dtcln = new DataTable();
                            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                            adpcln.Fill(dtcln);
                            if (dtcln.Rows.Count > 0)
                            {
                                foreignkeytblid = dtcln.Rows[0]["Id"].ToString();   
                            }
                            else
                            {
                            }
                            return foreignkeytblid;
                        }

                        public static string SatelliteSyncronisationrequiringTablesMaster_Where(String TableId)
                        {
                            string foreignwhere = "";
                            string foreignkeytblid = "";
                            string strcln = " Select * From SatelliteSyncronisationrequiringTablesMaster_Where Where TableId='" + TableId + "' ";
                            SqlCommand cmdcln = new SqlCommand(strcln, PageConn.licenseconn());
                            DataTable dtcln = new DataTable();
                            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                            adpcln.Fill(dtcln);
                            if (dtcln.Rows.Count > 0)
                            {
                                foreignkeytblid = dtcln.Rows[0]["Select_Query"].ToString() + " and " + dtcln.Rows[0]["WhereCondi"].ToString();   
                            }
                            else
                            {
                            }
                            return foreignkeytblid;
                        }
                        //
                        public static string SatelliteSyncronisationrequiringTablesMaster_WherePKIDName(String TableId)
                        {
                            string PKIDName = "";
                            string foreignkeytblid = "";
                            string strcln = " Select * From SatelliteSyncronisationrequiringTablesMaster_SerWhere Where TableId='" + TableId + "' ";
                            SqlCommand cmdcln = new SqlCommand(strcln, PageConn.licenseconn());
                            DataTable dtcln = new DataTable();
                            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                            adpcln.Fill(dtcln);
                            if (dtcln.Rows.Count > 0)
                            {
                                PKIDName = dtcln.Rows[0]["PKIdName"].ToString();
                            }
                            else
                            {
                            }
                            return PKIDName;
                        }
                        public static string SatelliteSyncronisationrequiringTablesMaster_WhereWhereID(String TableId)
                        {
                            string WhereID = "";
                            string foreignkeytblid = "";
                            string strcln = " Select * From SatelliteSyncronisationrequiringTablesMaster_SerWhere Where TableId='" + TableId + "' ";
                            SqlCommand cmdcln = new SqlCommand(strcln, PageConn.licenseconn());
                            DataTable dtcln = new DataTable();
                            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                            adpcln.Fill(dtcln);
                            if (dtcln.Rows.Count > 0)
                            {
                                WhereID = "";
                            }
                            else
                            {
                            }
                            return WhereID;
                        }
                        public static string AAAAAAA_Record(String TAbleName, string Record, string ServerId)
                        {
                            string foreignwhere = "";
                            SqlConnection liceco = new SqlConnection();
                            liceco = PageConn.licenseconn();
                            if (liceco.State.ToString() != "Open")
                            {
                                liceco.Open();
                            }

                            string str = " insert into  Satelite_ServerFristTimeInsertedRecord(TAbleName,Record,ServerId) values ('" + TAbleName + "','" + Record + "','" + ServerId + "') ";
                            SqlCommand cmd = new SqlCommand(str, liceco);
                            cmd.ExecuteNonQuery();
                            liceco.Close();
                            return foreignwhere;
                        }                       
                    }