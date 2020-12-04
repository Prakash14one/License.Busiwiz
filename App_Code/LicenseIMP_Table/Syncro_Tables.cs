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
                    public class Syncro_Tables
                    {
                        SqlConnection con;                     
                        public static string companykey = "aaa"; public static string serverkey = "aaaa";
                        public Syncro_Tables()
                        {
                            con = MyCommonfile.licenseconn(); 
                        }
                        public static string GenerateAndSaveFile(string FilenamewithExt, string TextInFile, string appcodepath)
                        {
                            string str = "";
                            string HashKey = "";

                            string fileLoc = appcodepath + "\\" + FilenamewithExt;

                            using (StreamWriter sw = new StreamWriter(fileLoc))
                                sw.Write
                                    (@" " + TextInFile + " ");
                            return str;
                        }
                        //--------------------------Sync_Need_Logs------------------------
                        public static Boolean DELETE__Sync_Need_Logs__AddDelUpdtSelect(string LogId)
                        {
                            Boolean ReturnID = true;
                            SqlConnection con = MyCommonfile.licenseconn();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Sync_Need_Logs_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Delete");
                            cmd.Parameters.AddWithValue("@LogId", LogId);
                            cmd.ExecuteNonQuery();
                            con.Close();                          
                            return ReturnID;
                        }
                        public static Int64 Insert___Sync_Need_Logs_AtServer(string LogId, string Rcordid, string ACTION, string TAbleId, Boolean IsRecordTransfer, string sid)
                        {
                            Int64 ReturnID = 0;
                           SqlConnection con = MyCommonfile.licenseconn();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Sync_Need_Logs_AtServer_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Insert");
                            cmd.Parameters.AddWithValue("@LogId", LogId);
                            cmd.Parameters.AddWithValue("@Rcordid", Rcordid);
                            cmd.Parameters.AddWithValue("@ACTION", ACTION);
                            cmd.Parameters.AddWithValue("@TAbleId", TAbleId);
                            cmd.Parameters.AddWithValue("@IsRecordTransfer", IsRecordTransfer);
                            cmd.Parameters.AddWithValue("@sid", sid);
                            object maxprocID = new object();
                            maxprocID = cmd.ExecuteScalar();
                            ReturnID = Convert.ToInt64(maxprocID);
                            con.Close();
                            return ReturnID;
                        }
                        public static Int64 Insert___Sync_Need_Logs_AtServerSERID(string Sync_Need_Logs_AtServer, string LogId, string Rcordid, string ACTION, string TAbleId, Boolean IsRecordTransfer, string sid)
                        {
                            Int64 ReturnID = 0;
                            SqlConnection con = MyCommonfile.licenseconn();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmdrXDrop = new SqlCommand(" INSERT INTO " + Sync_Need_Logs_AtServer + " (LogId,Rcordid,TAbleId,IsRecordTransfer,sid,ACTION,TakenForTemp) Values ('" + LogId + "','" + Rcordid + "','" + TAbleId + "','" + IsRecordTransfer + "','" + sid + "','" + ACTION + "','0')", con);                           
                            object maxprocID = new object();
                            maxprocID = cmdrXDrop.ExecuteScalar();
                            con.Close();  
                            ReturnID = Convert.ToInt64(maxprocID);                          
                            return ReturnID;
                        }
                        public static Boolean DELETE___Sync_Need_Logs_AtServer(string ID)
                        {
                            Boolean ReturnID = true;
                            SqlConnection con = MyCommonfile.licenseconn();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Sync_Need_Logs_AtServer_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Delete");
                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            return ReturnID;
                        }

                        //Job1
                        public static Int64 InsertJob1___Satelitte_Server_Sync_Job_Master(string SatelliteServerID, string SyncJobName, DateTime JobDateTime, Boolean JobFinishStatus)
                        {
                            Int64 ReturnID = 0;
                            SqlConnection con = MyCommonfile.licenseconn();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Satelitte_Server_Sync_Job_Master_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Insert");
                            cmd.Parameters.AddWithValue("@SatelliteServerID", SatelliteServerID);
                            cmd.Parameters.AddWithValue("@SyncJobName", SyncJobName);
                            cmd.Parameters.AddWithValue("@JobDateTime", JobDateTime);
                            cmd.Parameters.AddWithValue("@JobFinishStatus", JobFinishStatus);
                            object maxprocID = new object();
                            maxprocID = cmd.ExecuteScalar();
                            con.Close();
                            ReturnID = Convert.ToInt64(maxprocID);
                            return ReturnID;
                        }
                        public static Boolean UpdateJob1___Satelitte_Server_Sync_Job_Master(string Satelitte_Server_Sync_Job_Master, Boolean JobFinishStatus, DateTime FinishDatetime)
                        {
                            Boolean Status = false;
                            SqlConnection con = MyCommonfile.licenseconn();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Satelitte_Server_Sync_Job_Master_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "UpdateFinish");
                            cmd.Parameters.AddWithValue("@ID", Satelitte_Server_Sync_Job_Master);
                            cmd.Parameters.AddWithValue("@JobFinishStatus", JobFinishStatus);
                            cmd.Parameters.AddWithValue("@FinishDatetime", FinishDatetime);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            Status = true;                           
                            return Status;
                        }
                        public static Boolean DeleteJob1____Satelitte_Server_Sync_Job_Master(string Job1Id)
                        {
                            Boolean ReturnID = true;
                            SqlConnection con = MyCommonfile.licenseconn();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Satelitte_Server_Sync_Job_Master_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Delete");
                            cmd.Parameters.AddWithValue("@ID", Job1Id);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            return ReturnID;
                        }
                        //DELETE FROM Satelitte_Server_Sync_Job_Master WHERE ID =@ID
                        //Job2---
                        public static Int64 InsertJob2___Satellite_Server_Sync_Job_Details(string Satelitte_Server_Sync_Job_Master_ID, string TableID, Boolean SyncedStatus, Boolean CheckingStatus, DateTime CheckedDateTime, Boolean NeedTocreateTblatSatServer)
                        {
                            Int64 ReturnID = 0;
                            SqlConnection con = MyCommonfile.licenseconn();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Satellite_Server_Sync_Job_Details_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Insert");
                            cmd.Parameters.AddWithValue("@Satelitte_Server_Sync_Job_Master_ID", Satelitte_Server_Sync_Job_Master_ID);
                            cmd.Parameters.AddWithValue("@TableID", TableID);
                            cmd.Parameters.AddWithValue("@SyncedStatus", SyncedStatus);
                            cmd.Parameters.AddWithValue("@CheckingStatus", CheckingStatus);
                            cmd.Parameters.AddWithValue("@CheckedDateTime", CheckedDateTime);
                            cmd.Parameters.AddWithValue("@NeedTocreateTblatSatServer", NeedTocreateTblatSatServer);
                            object maxprocID = new object();
                            maxprocID = cmd.ExecuteScalar();
                            con.Close();
                            ReturnID = Convert.ToInt64(maxprocID);
                            return ReturnID;
                        }
                        public static Boolean UpdateJob2___Satellite_Server_Sync_Job_Details(string Job2Id, Boolean SyncedStatus, Boolean CheckingStatus, DateTime CheckedDateTime, Boolean JobFinishFinishStatus, DateTime JobDetailDoneDatandtime)
                        {
                            Boolean Status = false;
                            SqlConnection con = MyCommonfile.licenseconn();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Satellite_Server_Sync_Job_Details_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "UpdateFinish");
                            cmd.Parameters.AddWithValue("@ID", Job2Id);
                            cmd.Parameters.AddWithValue("@SyncedStatus", SyncedStatus);
                            cmd.Parameters.AddWithValue("@CheckingStatus", CheckingStatus);
                            cmd.Parameters.AddWithValue("@CheckedDateTime", CheckedDateTime);
                            cmd.Parameters.AddWithValue("@JobFinishFinishStatus", JobFinishFinishStatus);
                            cmd.Parameters.AddWithValue("@JobDetailDoneDatandtime", JobDetailDoneDatandtime);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            Status = true;                            
                            return Status;
                        }
                        public static Boolean DeleteJob2____Satellite_Server_Sync_Job_Details(string ID)
                        {
                            Boolean ReturnID = true;
                            SqlConnection con = MyCommonfile.licenseconn();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Satellite_Server_Sync_Job_Details_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Delete");
                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            return ReturnID;
                        }

                        //Job3-----
                        public static Int64 InsertJob3___Satelite_Server_Sync_Log_Deatils(string Satellite_Server_Sync_Job_Details_ID, string RecordID, DateTime Dateandtime, string TypeOfOperationDone, string TyepeOfOperationReqd, Boolean SyncedStatus)
                        {
                            SqlConnection con = MyCommonfile.licenseconn();
                            Int64 ReturnID = 0;
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Satelite_Server_Sync_Log_Deatils_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Insert");
                            cmd.Parameters.AddWithValue("@Satellite_Server_Sync_Job_Details_ID", Satellite_Server_Sync_Job_Details_ID);
                            cmd.Parameters.AddWithValue("@RecordID", RecordID);
                            cmd.Parameters.AddWithValue("@Dateandtime", Dateandtime);
                            cmd.Parameters.AddWithValue("@TypeOfOperationDone", TypeOfOperationDone);
                            cmd.Parameters.AddWithValue("@TyepeOfOperationReqd", TyepeOfOperationReqd);
                            cmd.Parameters.AddWithValue("@SyncedStatus", SyncedStatus);
                            object maxprocID = new object();
                            maxprocID = cmd.ExecuteScalar();
                            ReturnID = Convert.ToInt64(maxprocID);
                            con.Close();
                            return ReturnID;
                        }
                        public static Boolean DeleteJob3____Satelite_Server_Sync_Log_Deatils(string ID)
                        {
                            Boolean ReturnID = true;
                            SqlConnection con = MyCommonfile.licenseconn();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Satelite_Server_Sync_Log_Deatils_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Delete");
                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            return ReturnID;
                        }
                        public static Int64 InsertJob3____AllRecord__Satelite_Server_Sync_Log_Deatils(string Job2ID, string TableName, string TableId, string ServerID, string PKTableName, string PKIdName, string Select_Query)
                        {
                            Int64 Count = 0;
                            string WhereForPKID = "";
                            //WhereForPKID = " Where " + PKTableName + "." + PKIdName + "=" + RecordID;                                   
                            string SelectWhere3 = " Where " + TableName + "." + PKIdName + " NOT IN (Select RecordID as Rcordid FROM   dbo.Satelite_Server_Sync_Log_Deatils INNER JOIN dbo.Satellite_Server_Sync_Job_Details ON dbo.Satelite_Server_Sync_Log_Deatils.Satellite_Server_Sync_Job_Details_ID = dbo.Satellite_Server_Sync_Job_Details.ID INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id Where Satellite_Server_Sync_Job_Details.TableID='" + TableId + "' and Satelitte_Server_Sync_Job_Master.SatelliteServerID=" + ServerID + ")";
                            string SelectWhere2 = " and PricePlanMaster.PricePlanId IN ( Select PricePlanId From  CompanyMaster Where active=1 and ServerId=" + ServerID + ") ";
                            string FinalSelect_Query = Select_Query + WhereForPKID + SelectWhere3 + SelectWhere2;
                            if (Select_Query.Length > 0)
                            {
                                DataTable DtWhereC = MyCommonfile.selectBZ("" + FinalSelect_Query + "");
                                for (int iicouts = 0; iicouts < DtWhereC.Rows.Count; iicouts++)
                                {
                                    string RecordID;
                                    RecordID = DtWhereC.Rows[iicouts][0].ToString();
                                    if (PKIdName.Length > 0)
                                    {
                                        RecordID = DtWhereC.Rows[iicouts][PKIdName].ToString();
                                    }
                                    Count++;                                   
                                    Int64 ReturnID2 = Syncro_Tables.InsertJob3___Satelite_Server_Sync_Log_Deatils(Job2ID, RecordID, DateTime.Now, "1", "", false);                    
                                }
                            }
                            else
                            {
                                Count++;
                                DataTable DtWhereC = MyCommonfile.selectBZ(" Select * From " + TableName + " " + SelectWhere3);
                                for (int iicouts = 0; iicouts < DtWhereC.Rows.Count; iicouts++)
                                {
                                    string RecordID;
                                    RecordID = DtWhereC.Rows[iicouts][0].ToString();
                                    if (PKIdName.Length > 0)
                                    {
                                        RecordID = DtWhereC.Rows[iicouts][PKIdName].ToString();
                                    }
                                    Count++;
                                    Int64 ReturnID2 = Syncro_Tables.InsertJob3___Satelite_Server_Sync_Log_Deatils(Job2ID, RecordID, DateTime.Now, "1", "", false); 
                                }
                                //Syncro_Tables.Insert___Sync_Need_Logs_AtServer(RecordID, RecordID, "1", TableId, false, ServerID);
                            }
                            return Count;
                        }


                        //Log2

                        //Satelite_Server_Sync_Log_Deatils
                        //public static Int64 InsertLog2___Satelite_Server_Sync_Log_Deatils(string Satellite_Server_Sync_Job_Details_ID, string RecordID, DateTime Dateandtime, string TypeOfOperationDone, string TyepeOfOperationReqd, Boolean SyncedStatus)
                        //{
                        //    Int64 ReturnID = 0;
                        //    SqlConnection con = MyCommonfile.licenseconn();
                        //    if (con.State.ToString() != "Open")
                        //    {
                        //        con.Open();
                        //    }
                        //    SqlCommand cmd = new SqlCommand("Satelite_Server_Sync_Log_Deatils_AddDelUpdtSelect", con);
                        //    cmd.CommandType = CommandType.StoredProcedure;
                        //    cmd.Parameters.AddWithValue("@StatementType", "Insert");
                        //    cmd.Parameters.AddWithValue("@Satellite_Server_Sync_Job_Details_ID", Satellite_Server_Sync_Job_Details_ID);
                        //    cmd.Parameters.AddWithValue("@RecordID", RecordID);
                        //    cmd.Parameters.AddWithValue("@Dateandtime", Dateandtime);
                        //    cmd.Parameters.AddWithValue("@TypeOfOperationDone", TypeOfOperationDone);
                        //    cmd.Parameters.AddWithValue("@TyepeOfOperationReqd", TyepeOfOperationReqd);
                        //    cmd.Parameters.AddWithValue("@SyncedStatus", SyncedStatus);
                        //    object maxprocID = new object();
                        //    maxprocID = cmd.ExecuteScalar();
                        //    ReturnID = Convert.ToInt64(maxprocID);
                        //    con.Close();                           
                        //    return ReturnID;
                        //}
                    }