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
using Microsoft.SqlServer.Management.Smo.Agent;
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
    SqlConnection conn;
    public static double size = 0;
    int StepId = 1;
    string allstring = "";   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Timer1.Enabled = false;
            img_loading.Visible = false;  
            //--Part3------------------------Add Job3 Table To Server Database--------------------------------------------------------------------------------  
            if (Request.QueryString["sid"] != null && Request.QueryString["Transfer"] == "1")
            {
                string sid = Request.QueryString["sid"].Replace(" ", "+"); //kdQMwcj0lE8= "5"; //
                sid = BZ_Common.BZ_Decrypted(sid);
                ViewState["sid"] = sid;    
                Timer1.Enabled = true;
                img_loading.Visible = true;
                Count_Job3_Record(ViewState["sid"].ToString());               
            }
        }
    }
    protected void Count_Job3_Record(string SId)
    {
        DataTable dtfindtab = MyCommonfile.selectBZ(@" SELECT Count(dbo.Satelite_Server_Sync_Log_Deatils.ID) as TotalCount
                                                        From    dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id ON  dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster.TableID AND dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.ClientProductTableMaster.Id INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON  dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id INNER JOIN dbo.ServerMasterTbl ON dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID = dbo.ServerMasterTbl.Id INNER JOIN dbo.Satelite_Server_Sync_Log_Deatils ON dbo.Satellite_Server_Sync_Job_Details.ID = dbo.Satelite_Server_Sync_Log_Deatils.Satellite_Server_Sync_Job_Details_ID INNER JOIN dbo.SyncActionMaster ON dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone = dbo.SyncActionMaster.ID
                                                        Where  dbo.Satellite_Server_Sync_Job_Details.SyncedStatus=0  and ServerMasterTbl.Id='" + SId + "' ");//  and dbo.Satelitte_Server_Sync_Job_Master.Id='" + ViewState["JobID"] + "' and
        lbl_Hmsg.Text = " Please waite for some moment we syncronice " + dtfindtab.Rows[0]["TotalCount"] + " record at sattellite server ";
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {      
        //--Part3----------------------Syncronice At Server Database-----------------------------------------------------------------------------
        if (ViewState["sid"] != null && Request.QueryString["Transfer"] == "1")
        {
            InsertAtServer____FromJob3(ViewState["sid"].ToString());           
        }
        //-------------------------------------------------------------------------------------------------------------------------
    }
 
    protected void btnRefresh1_OnClick(object sender, EventArgs e)
    {
        lbl_Msg.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff");
    }


    protected void InsertAtServer____FromJob3(string SId)
    {
        Boolean connnection = false;
        int c = 0;
        string Delete_From = "";
        string PKname = "";
        string deleteJob3ID = "";
        conn = new SqlConnection();
        try
        {
            conn = ServerWizard.ServerDefaultInstanceConnetionTCPWithPublicIP_Serverid(SId);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            conn.Close();
            connnection = true;
        }
        catch (Exception ex)
        {
        }

        if (connnection == true)
        {
            int totalrec = 0;
            string Bunchinsert = "";
            string seridstatus = "";
            string Insert_IntoCommon = "";
            DataTable dtcln = new DataTable();
            DataTable dtJob1 = MyCommonfile.selectBZ(@" SELECT DISTINCT Satelitte_Server_Sync_Job_Master.Id AS Job1ID, Satelitte_Server_Sync_Job_Master.SatelliteServerID,ServerMasterTbl.ServerName,ServerMasterTbl.serverloction, dbo.Satelitte_Server_Sync_Job_Master.JobDateTime FROM dbo.Satelitte_Server_Sync_Job_Master INNER JOIN dbo.ServerMasterTbl ON dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID = dbo.ServerMasterTbl.Id Where Satelitte_Server_Sync_Job_Master.SatelliteServerID='" + SId + "'");
            if (dtJob1.Rows.Count > 0)
            {
                encstr = ServerWizard.ServerEncrDecriKEY(SId);
                for (int i1 = 0; i1 < dtJob1.Rows.Count; i1++)
                {
                    string Job1ID = dtJob1.Rows[i1]["Job1ID"].ToString();
                    string ServerName = dtJob1.Rows[i1]["ServerName"].ToString();
                    DataTable dtJob2 = MyCommonfile.selectBZ(@" SELECT TOP(1) Satellite_Server_Sync_Job_Details.ID AS Job2ID,Satellite_Server_Sync_Job_Details.TableID, dbo.ClientProductTableMaster.Id, dbo.ClientProductTableMaster.TableName FROM dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id ON dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster.TableID Where Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID='" + Job1ID + "' ");
                    if (dtJob2.Rows.Count > 0)
                    {
                        for (int i2 = 0; i2 < dtJob2.Rows.Count; i2++)
                        {
                            string Job2ID = dtJob2.Rows[i2]["Job2ID"].ToString();
                            string TableName = dtJob2.Rows[i2]["TableName"].ToString();
                            string TableId = dtJob2.Rows[i2]["TableID"].ToString();
                            // CreateTableDesign(TableName);
                            DataTable dtJob3 = MyCommonfile.selectBZ(@" SELECT TOP(200) Satelite_Server_Sync_Log_Deatils.ID AS Job3ID,Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone , Satelite_Server_Sync_Log_Deatils.RecordID, SyncActionMaster.ActionName FROM Satelite_Server_Sync_Log_Deatils INNER JOIN dbo.SyncActionMaster ON dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone = dbo.SyncActionMaster.ID Where Satelite_Server_Sync_Log_Deatils.Satellite_Server_Sync_Job_Details_ID='" + Job2ID + "'  ");
                            if (dtJob3.Rows.Count > 0)
                            {
                                Insert_IntoCommon = " INSERT INTO " + TableName + "(  ";
                                DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + TableName + "'");
                                for (int k = 0; k < dts1.Rows.Count; k++)
                                {
                                    Insert_IntoCommon += ("" + dts1.Rows[k]["column_name"] + " ,");
                                }
                                Insert_IntoCommon = Insert_IntoCommon.Remove(Insert_IntoCommon.Length - 1);
                                Insert_IntoCommon += ") Values ";

                                // DataTable maxiddesid = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
                                PKname = TableRelated.SatelliteSyncronisationrequiringTablesMaster_WherePKIDName(TableId);
                                if (PKname.Length > 0)
                                {
                                }
                                else
                                {
                                    PKname = dts1.Rows[0]["column_name"].ToString();
                                }
                                for (int iii = 0; iii < dtJob3.Rows.Count; iii++)
                                {
                                    string Job3ID = dtJob3.Rows[iii]["Job3ID"].ToString();
                                    string RecordID = dtJob3.Rows[iii]["RecordID"].ToString();
                                    string Typeoperation = dtJob3.Rows[iii]["TypeOfOperationDone"].ToString();
                                    deleteJob3ID += Job3ID + ",";
                                    try
                                    {
                                        if (Typeoperation == "2" || Typeoperation == "3")
                                        {
                                            SqlCommand ccm = new SqlCommand("Delete From " + TableName + " where " + PKname + "='" + Encrypted(RecordID) + "'", conn);
                                            if (conn.State.ToString() != "Open")
                                            {
                                                conn.Open();
                                            }
                                            ccm.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                        if (Typeoperation == "1" || Typeoperation == "2")
                                        {
                                            DataTable dtr = MyCommonfile.selectBZ(" Select * From " + TableName + " where " + PKname + "=" + RecordID + " ");
                                            if (dtr.Rows.Count == 0)
                                            {
                                                SqlCommand ccmm = new SqlCommand(" Delete From Satelite_Server_Sync_Log_Deatils Where ID IN (" + Job3ID + ") ", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                ccmm.ExecuteNonQuery();
                                                con.Close();
                                            }
                                            foreach (DataRow itm in dtr.Rows)
                                            {
                                                c++;
                                                string Insert_Into = "";
                                                Insert_Into += " (";
                                                for (int k = 0; k < dts1.Rows.Count; k++)
                                                {
                                                    Insert_Into += "'" + Encrypted(Convert.ToString(itm["" + dts1.Rows[k]["column_name"] + ""])) + "' ,";
                                                }
                                                Insert_Into = Insert_Into.Remove(Insert_Into.Length - 1);
                                                Insert_Into += " ),";
                                                Bunchinsert += Insert_Into;
                                                if (c == 200)
                                                {
                                                    Bunchinsert = Bunchinsert.Remove(Bunchinsert.Length - 1);
                                                    string ss = Insert_IntoCommon + Bunchinsert;
                                                    SqlCommand ccm = new SqlCommand(ss, conn);
                                                    if (conn.State.ToString() != "Open")
                                                    {
                                                        conn.Open();
                                                    }
                                                    ccm.ExecuteNonQuery();
                                                    conn.Close();

                                                    deleteJob3ID = deleteJob3ID.Remove(deleteJob3ID.Length - 1);
                                                    SqlCommand ccmm = new SqlCommand(" Delete From Satelite_Server_Sync_Log_Deatils Where ID IN (" + deleteJob3ID + ") ", con);
                                                    if (con.State.ToString() != "Open")
                                                    {
                                                        con.Open();
                                                    }
                                                    ccmm.ExecuteNonQuery();
                                                    con.Close();

                                                    deleteJob3ID = "";
                                                    Bunchinsert = "";
                                                    c = 0;
                                                }
                                            }
                                        }                                       
                                        totalrec++;
                                        //Permanent Record Functionality
                                        //----****
                                        //
                                        // lblmsg.Text = " Successfully synchronization ";//" + totalrec + " records for " + lblname.Text + " server record " + lbl_RecordID.Text + "<br><br>";
                                    }
                                    catch
                                    {
                                        seridstatus = SId;
                                        Bunchinsert = "";
                                        lbl_Msg.Text = " Some problem when synchronization with " + ServerName + " server <br><br>";//e1.ToString()+"<br>";
                                    }
                                }//Job1 Loop
                                if (Bunchinsert.Length > 0)
                                {
                                    Bunchinsert = Bunchinsert.Remove(Bunchinsert.Length - 1);
                                    string ss = Insert_IntoCommon + Bunchinsert;
                                    SqlCommand ccm = new SqlCommand(ss, conn);
                                    if (conn.State.ToString() != "Open")
                                    {
                                        conn.Open();
                                    }
                                    ccm.ExecuteNonQuery();
                                    conn.Close();

                                    deleteJob3ID = deleteJob3ID.Remove(deleteJob3ID.Length - 1);
                                    SqlCommand ccmm = new SqlCommand(" Delete From Satelite_Server_Sync_Log_Deatils Where ID IN (" + deleteJob3ID + ")", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    ccmm.ExecuteNonQuery();
                                    con.Close();

                                    deleteJob3ID = "";
                                    Bunchinsert = "";
                                    c = 0;
                                }
                                //  Boolean DeletestatusJob2 = Syncro_Tables.DeleteJob2____Satelite_Server_Sync_Log_Deatils(Job2ID);
                                //Test
                                DataTable dtfindtab = MyCommonfile.selectBZ(@" SELECT Count(dbo.Satelite_Server_Sync_Log_Deatils.ID) as TotalCount
                                                        From    dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id ON  dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster.TableID AND dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.ClientProductTableMaster.Id INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON  dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id INNER JOIN dbo.ServerMasterTbl ON dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID = dbo.ServerMasterTbl.Id INNER JOIN dbo.Satelite_Server_Sync_Log_Deatils ON dbo.Satellite_Server_Sync_Job_Details.ID = dbo.Satelite_Server_Sync_Log_Deatils.Satellite_Server_Sync_Job_Details_ID INNER JOIN dbo.SyncActionMaster ON dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone = dbo.SyncActionMaster.ID
                                                        Where ServerMasterTbl.Id='" + SId + "' ");
                                DataTable dtfindtab1 = MyCommonfile.selectBZ(@" SELECT COUNT(dbo.Satellite_Server_Sync_Job_Details.TableID) AS TableTotalCount FROM dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id ON dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster.TableID INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id 
                                                        Where Satelitte_Server_Sync_Job_Master.SatelliteServerID='" + SId + "' ");
                                lbl_Msg.Text = " Please waite for some moment we syncronice " + dtfindtab1.Rows[0]["TableTotalCount"] + " tables and " + dtfindtab.Rows[0]["TotalCount"] + " records at server ";

                            }
                            else
                            {
                                //Norecord in Job3 for Job2
                                Boolean DeletestatusJob2 = Syncro_Tables.DeleteJob2____Satellite_Server_Sync_Job_Details(Job2ID);
                            }
                        }//Job2 Lopp
                        // Boolean DeletestatusJob1 = Syncro_Tables.DeleteJob1____Satelitte_Server_Sync_Job_Master(Job1ID);                       
                    }
                    else
                    {
                        //No Record in Job2 for selected job1
                        Boolean DeletestatusJob1 = Syncro_Tables.DeleteJob1____Satelitte_Server_Sync_Job_Master(Job1ID);
                    }
                }//Job1 Loop               
            }
            else
            {
                img_loading.Visible = false;
                lbl_Msg.Text = "<br> Job Done Successfully " + DateTime.Now.ToString();
                Timer1.Enabled = false;
                //if (Request.QueryString["sid"] != null || Request.QueryString["comid"] != null)
                //{
                //    Response.Redirect("http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + Request.QueryString["comid"].ToString() + "&step13=yes");
                //}
            }
        }
        else
        {
            lbl_Msg.Text = " No connection possible with server <br> you are tying to connect database with this connection string ( " + conn.ConnectionString + " )";//e1.ToString()+"<br>";
        }
    }










    public static string Encrypted(string strText)
    {
        return Encrypt(strText, encstr);
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
