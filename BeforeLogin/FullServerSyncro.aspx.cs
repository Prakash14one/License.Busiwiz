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
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.DirectoryServices;
using System.IO.Compression;
using Ionic.Zip;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Xml;
using System.Net;
using System.Net.Mail;

public partial class BeforeLogin_Syncro : System.Web.UI.Page
{
    SqlConnection conn;
    public static string encstr = "";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            Timer1.Enabled = false;
            img_loading.Visible = false;
            if (Request.QueryString["sid"] != null && Request.QueryString["1"] == "Job3")
            {
                ViewState["ServerID"] = Request.QueryString["sid"].ToString();
                Create___Job1_Job2(ViewState["ServerID"].ToString());
                Timer1.Enabled = true;
                img_loading.Visible = true;
              
            }
            if (Request.QueryString["sid"] != null && Request.QueryString["1"] == "Transfer")
            {
                ViewState["ServerID"] = Request.QueryString["sid"].ToString();
                Count_Job3_Record(ViewState["ServerID"].ToString());
                Timer1.Enabled = true;
                img_loading.Visible = true;
               
            }
        }
    }   
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Timer1.Enabled = false;
        if (Request.QueryString["sid"] != null && Request.QueryString["1"] == "Job3")
        {
            ViewState["ServerID"] = Request.QueryString["sid"].ToString();
            //Insert At Job3 Table
            Create___Job3_From_Job2(ViewState["ServerID"].ToString(), lbl_jo1ID.Text);
        }
        if (Request.QueryString["sid"] != null && Request.QueryString["1"] == "Transfer")
        {
            ViewState["ServerID"] = Request.QueryString["sid"].ToString();
            InsertAtServer____FromJob3(ViewState["ServerID"].ToString());
        }
        Timer1.Enabled = true;
    }
    protected void Create___Job1_Job2(string SId)
    {
        Int64 sateserver = 0;
        Int64 totaltable = 0;
        DataTable DTServerID = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.ServerMasterTbl.Id,dbo.ServerMasterTbl.ServerName FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id Where  dbo.CompanyMaster.Active=1 and dbo.ServerMasterTbl.Id=" + SId + " ");//      
        if (DTServerID.Rows.Count > 0)
        {
            sateserver++;
            Int64 Job1RecordID = Syncro_Tables.InsertJob1___Satelitte_Server_Sync_Job_Master(DTServerID.Rows[0]["id"].ToString(), "Updation " + DTServerID.Rows[0]["ServerName"].ToString() + " on " + Convert.ToString(DateTime.Now), DateTime.Now, false);
            lbl_jo1ID.Text=Convert.ToString(Job1RecordID);
            totaltable = 0;
            //DataTable dsfst = MyCommonfile.selectBZ(" Select DISTINCT TAbleId From Sync_Need_Logs_AtServer where sid='" + DTServerID.Rows[iicout]["id"].ToString() + "' Order By TAbleId ");
            DataTable DTTable = MyCommonfile.selectBZ(@" SELECT DISTINCT dbo.ClientProductTableMaster.Id AS TableId FROM dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere.TableId Order By TAbleId ");
            for (int ii = 0; ii < DTTable.Rows.Count; ii++)
            {
                totaltable++;
                Int64 Job2RecordID = Syncro_Tables.InsertJob2___Satellite_Server_Sync_Job_Details(Convert.ToString(Job1RecordID), DTTable.Rows[ii]["TAbleId"].ToString(), false, false, DateTime.Now, true);
            }
            lbl_Msg.Text = "Total " + totaltable + " table need create Job for synchronization record for " + DTServerID.Rows[0]["ServerName"].ToString() + " server. ";
        }               
    }
    protected void Create___Job3_From_Job2(string SId,string  Job1ID)
    {
        Int64 recordid = 0;
        DataTable dstbl = MyCommonfile.selectBZ(" SELECT DISTINCT TOP(1)  dbo.Satellite_Server_Sync_Job_Details.TableID, dbo.Satellite_Server_Sync_Job_Details.ID FROM dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id where Satelitte_Server_Sync_Job_Master.SatelliteServerID='" + SId + "' and CheckingStatus=0 Order By TableID  ");
        if (dstbl.Rows.Count > 0)
        {
            DataTable DTTable = MyCommonfile.selectBZ(@" SELECT DISTINCT dbo.ClientProductTableMaster.Id AS TableId, dbo.ClientProductTableMaster.TableName, dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere.PKTableName, dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere.PKIdName, dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere.Select_Query FROM dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere.TableId Where dbo.ClientProductTableMaster.Id='" + dstbl.Rows[0]["TableId"].ToString() + "' ");
            if (dstbl.Rows.Count > 0)
            {
                recordid++;
                CreateTableDesign(DTTable.Rows[0]["TableName"].ToString(),SId);
                Int64 RecordInserted = Syncro_Tables.InsertJob3____AllRecord__Satelite_Server_Sync_Log_Deatils(dstbl.Rows[0]["ID"].ToString(), DTTable.Rows[0]["TableName"].ToString(), DTTable.Rows[0]["TableId"].ToString(), SId, DTTable.Rows[0]["PKTableName"].ToString(), DTTable.Rows[0]["PKIdName"].ToString(), DTTable.Rows[0]["Select_Query"].ToString());
                lbl_Msg.Text += "<br> From " + DTTable.Rows[0]["TableName"].ToString() + " Total " + RecordInserted + " Record Are Inseted In Job Table ";
                Boolean jobc2heckstatus = Syncro_Tables.UpdateJob2___Satellite_Server_Sync_Job_Details(dstbl.Rows[0]["ID"].ToString(), false, true, DateTime.Now, false, DateTime.Now);
            }
        }
        else
        {
            Boolean status2 = Syncro_Tables.UpdateJob1___Satelitte_Server_Sync_Job_Master(Convert.ToString(Job1ID), true, DateTime.Now);
            img_loading.Visible = false;
            lbl_Msg.Text += "<br> Job Done Successfully "+DateTime.Now.ToString();
            Timer1.Enabled = false;
            if (Request.QueryString["2"] == "Transfer" || Request.QueryString["comid"] != null)
            {
                Response.Redirect("Syncro.aspx?comid=" + Request.QueryString["comid"] + "&sid=" + Request.QueryString["sid"].ToString() + "&1=Transfer");
            }
        }
    }






    protected void Count_Job3_Record(string SId)
    {
        DataTable dtfindtab = MyCommonfile.selectBZ(@" SELECT Count(dbo.Satelite_Server_Sync_Log_Deatils.ID) as TotalCount
                                                        From    dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id ON  dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster.TableID AND dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.ClientProductTableMaster.Id INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON  dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id INNER JOIN dbo.ServerMasterTbl ON dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID = dbo.ServerMasterTbl.Id INNER JOIN dbo.Satelite_Server_Sync_Log_Deatils ON dbo.Satellite_Server_Sync_Job_Details.ID = dbo.Satelite_Server_Sync_Log_Deatils.Satellite_Server_Sync_Job_Details_ID INNER JOIN dbo.SyncActionMaster ON dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone = dbo.SyncActionMaster.ID
                                                        Where  dbo.Satellite_Server_Sync_Job_Details.SyncedStatus=0  and ServerMasterTbl.Id='" + SId + "' ");//  and dbo.Satelitte_Server_Sync_Job_Master.Id='" + ViewState["JobID"] + "' and
        lbl_Msg.Text = " Please waite for some moment we syncronice " + dtfindtab.Rows[0]["TotalCount"] + " record at server ";
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
            DataTable dtJob1 = MyCommonfile.selectBZ(@" SELECT DISTINCT Satelitte_Server_Sync_Job_Master.Id AS Job1ID, Satelitte_Server_Sync_Job_Master.SatelliteServerID,ServerMasterTbl.ServerName,ServerMasterTbl.serverloction, dbo.Satelitte_Server_Sync_Job_Master.JobDateTime FROM dbo.Satelitte_Server_Sync_Job_Master INNER JOIN dbo.ServerMasterTbl ON dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID = dbo.ServerMasterTbl.Id Where Satelitte_Server_Sync_Job_Master.SatelliteServerID='"+SId+"'");
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
                                        if (Typeoperation == "1")
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
                lbl_Msg.Text = "<br> Job Done Successfully "+DateTime.Now.ToString();
                Timer1.Enabled = false;

                if (Request.QueryString["sid"] != null || Request.QueryString["comid"] != null)
                {
                    Response.Redirect("http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + Request.QueryString["comid"].ToString() + "&step13=yes");
                }
            }        
        }
        else
        {
            lbl_Msg.Text = " No connection possible with server <br> you are tying to connect database with this connection string ( " + conn.ConnectionString + " )";//e1.ToString()+"<br>";
        }
    }

    protected void CreateTableDesign(string tablename,string SId)
    {
        Boolean connnection = false;
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
            Timer1.Enabled = true;
            lbl_Msg.Text = conn.ConnectionString+ " Conetiong at here but some problem";
            return;
        }
        if (connnection == true)
        {
            string st1 = "CREATE TABLE " + tablename + "(";
            DataTable dts1 = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
            for (int k = 0; k < dts1.Rows.Count; k++)
            {
                if (k == 0)
                {
                    //st1 += ("" + dts1.Rows[k]["column_name"] + " int Identity(1,1),");
                    st1 += ("" + dts1.Rows[k]["column_name"] + " nvarchar(500),");
                }
                else
                {
                    st1 += ("" + dts1.Rows[k]["column_name"] + " " + dts1.Rows[k]["data_type"] + "(" + dts1.Rows[k]["CHARACTER_MAXIMUM_LENGTH"] + "),");
                }
            }
            st1 = st1.Remove(st1.Length - 1);
            st1 += ")";
            //st1 = st1.Replace("int()", "int");
            st1 = st1.Replace("bigint()", "nvarchar(500)");
            st1 = st1.Replace("int()", "nvarchar(500)");
            st1 = st1.Replace("(-1)", "(MAX)");
            st1 = st1.Replace("datetime()", "nvarchar(500)");
            st1 = st1.Replace("nvarchar(50)", "nvarchar(500)");
            st1 = st1.Replace("decimal()", "nvarchar(500)");
            st1 = st1.Replace("decimal", "nvarchar(500)");
            st1 = st1.Replace("bit()", "nvarchar(500)");//st1 = st1.Replace("bit()", "bit");
            st1 = st1.Replace("nvarchar(20)", "nvarchar(500)");
            st1 = st1.Replace("nvarchar(10)", "nvarchar(500)");
            st1 = st1.Replace("nvarchar(100)", "nvarchar(500)");
            DataTable dts = selectSer("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
            if (dts.Rows.Count <= 0)
            {
                SqlCommand cmdr = new SqlCommand(st1, conn);
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                cmdr.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                string strBC = " CREATE TABLE " + tablename + "(";
                DataTable DTBC = selectSer("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
                for (int k = 0; k < DTBC.Rows.Count; k++)
                {
                    if (k == 0)
                    {
                        strBC += ("" + DTBC.Rows[k]["column_name"] + " nvarchar(500),");
                    }
                    else
                    {
                        strBC += ("" + DTBC.Rows[k]["column_name"] + " " + DTBC.Rows[k]["data_type"] + "(" + DTBC.Rows[k]["CHARACTER_MAXIMUM_LENGTH"] + "),");

                    }
                }
                strBC = strBC.Remove(strBC.Length - 1);
                strBC += ")";
                st1 = st1.Replace("bigint()", "nvarchar(500)");
                st1 = st1.Replace("int()", "nvarchar(500)");
                st1 = st1.Replace("(-1)", "(MAX)");
                st1 = st1.Replace("datetime()", "nvarchar(500)");
                st1 = st1.Replace("nvarchar(50)", "nvarchar(500)");
                st1 = st1.Replace("decimal()", "nvarchar(500)");
                st1 = st1.Replace("decimal", "nvarchar(500)");
                st1 = st1.Replace("bit()", "nvarchar(500)");
                st1 = st1.Replace("nvarchar(20)", "nvarchar(500)");
                st1 = st1.Replace("nvarchar(10)", "nvarchar(500)");
                st1 = st1.Replace("nvarchar(100)", "nvarchar(500)");
                if (strBC != st1)
                {
                    SqlCommand cmdrX = new SqlCommand("Drop table " + tablename, conn);
                    if (conn.State.ToString() != "Open")
                    {
                        conn.Open();
                    }
                    cmdrX.ExecuteNonQuery();
                    //Create Table
                    SqlCommand cmdr = new SqlCommand(st1, conn);
                    cmdr.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    //SqlCommand cmdrX = new SqlCommand("Delete  from  " + tablename, conn);
                    //if (conn.State.ToString() != "Open")
                    //{
                    //    conn.Open();
                    //}
                    //cmdrX.ExecuteNonQuery();               
                }
            }
        }
     
    }
    protected DataTable selectSer(string str)
    {
        if (conn.State.ToString() != "Open")
        {
            conn.Open();
        }
        SqlCommand cmdclnccdweb = new SqlCommand(str, conn);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        conn.Close();
        return dtclnccdweb;
        
           
        
    }

    protected void permenet()
    {
        try
        {
            // Boolean statuss = Update_Satellite_Server_Sync_Job_Details(lbl_jobdetail.Text, true, true, DateTime.Now, true, DateTime.Now);
            //DataTable Satallite_Server_Sync_RecordsMasterTblID = MyCommonfile.selectBZ(" Select Id as SatalliteServerSyncTblTecordStatusID From Satallite_Server_Sync_RecordsMasterTbl Where TableId='" + lbl_TableId.Text + "' and ServerID='" + lblseid.Text + "' ");
            //if (lbl_typeoperation.Text == "1")
            //{
            //    Insert___Satallite_Server_Sync_RecordsDetailTbl(Satallite_Server_Sync_RecordsMasterTblID.Rows[0]["Satallite_Server_Sync_RecordsMasterTblID"].ToString(), lbl_RecordID.Text, DateTime.Now, lbl_typeoperation.Text, lbl_typeoperation.Text);
            //}
            //if (lbl_typeoperation.Text == "3")
            //{
            //    Delete___Satallite_Server_Sync_RecordsDetailTbl(Satallite_Server_Sync_RecordsMasterTblID.Rows[0]["Satallite_Server_Sync_RecordsMasterTblID"].ToString(), lbl_RecordID.Text);
            //}
        }
        catch
        {
        }
    }

    // protected void Create__Job3_From_Log2(string SId,string Job1ID)
    //{
    //    Int64 recordid = 0;
    //        recordid = 0;
    //        DataTable dstbl = MyCommonfile.selectBZ(" SELECT DISTINCT TOP (1) dbo.Satellite_Server_Sync_Job_Details.TableID, dbo.Satellite_Server_Sync_Job_Details.ID, dbo.ClientProductTableMaster.TableName FROM dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.ClientProductTableMaster ON dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.ClientProductTableMaster.Id where Satelitte_Server_Sync_Job_Master_ID='" + Job1ID + "' and CheckingStatus=0 Order By TableID  ");
    //        if (dstbl.Rows.Count>0)
    //        {
    //            DataTable ds = MyCommonfile.selectBZ(" Select * From Sync_Need_Logs_AtServer where sid='" + SId + "' and TAbleId='" + dstbl.Rows[0]["TableID"].ToString() + "' Order By LogId ");
    //            for (int iii = 0; iii < ds.Rows.Count; iii++)
    //            {
    //                recordid++;
    //                string ID = ds.Rows[iii]["ID"].ToString();
    //                string LogId = ds.Rows[iii]["LogId"].ToString();
    //                string Rcordid = ds.Rows[iii]["Rcordid"].ToString();
    //                string TableId = ds.Rows[iii]["TAbleId"].ToString();                    
    //                string action = "1";
    //                if (ds.Rows[iii]["ACTION"].ToString() == "INSERT")
    //                {
    //                    action = "1";
    //                }
    //                else if (ds.Rows[iii]["ACTION"].ToString() == "Updated")
    //                {
    //                    action = "2";
    //                }
    //                else if (ds.Rows[iii]["ACTION"].ToString() == "Deleted")
    //                {
    //                    action = "3";
    //                }
    //                // Int64 ReturnID = Insert_Satallite_Server_Sync_RecordsDetailTbl(Convert.ToString(Satallite_Server_Sync_RecordsMasterTblID), Rcordid, DateTime.Now, action, action);
    //                Int64 ReturnID2 =Syncro_Tables.InsertJob3___Satelite_Server_Sync_Log_Deatils(dstbl.Rows[0]["ID"].ToString(), Rcordid, DateTime.Now, action, "", false);                    
    //                Syncro_Tables.DELETE___Sync_Need_Logs_AtServer(ID);
    //            }  
    //             lbl_Msg.Text += "<br> Total " + recordid + " Record Are Inseted In " + dstbl.Rows[0]["TableName"].ToString() + " Table ";
    //            Boolean jobc2heckstatus = Syncro_Tables.UpdateJob2___Satellite_Server_Sync_Job_Details(dstbl.Rows[0]["ID"].ToString(), false, true, DateTime.Now, false, DateTime.Now);
    //        }
    //        else
    //        {
    //            img_loading.Visible = false;  
    //            lbl_Msg.Text += "<br> Job Done Successfully ";
    //            Boolean status2 = Syncro_Tables.UpdateJob1___Satelitte_Server_Sync_Job_Master(Convert.ToString(Job1ID), true, DateTime.Now);
    //            Timer1.Enabled = false;
    //        } 
    //}

















    protected void GetSync_Create_Serv_List___InsertAtLog2(string ServerID)
    {
        DataTable DTTable = MyCommonfile.selectBZ(@" SELECT DISTINCT dbo.ClientProductTableMaster.Id AS TableId, dbo.ClientProductTableMaster.TableName, dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere.PKTableName, dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere.PKIdName, dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere.Select_Query FROM dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster_SerWhere.TableId ");
        for (int iicouts = 0; iicouts < DTTable.Rows.Count; iicouts++)
        {
            Int64 RecordInserted = Table_DatabaseSQL.All_Table_All_Record__InsertAtLog2(DTTable.Rows[iicouts]["TableName"].ToString(), DTTable.Rows[iicouts]["TableId"].ToString(), ServerID, DTTable.Rows[iicouts]["PKTableName"].ToString(), DTTable.Rows[iicouts]["PKIdName"].ToString(), DTTable.Rows[iicouts]["Select_Query"].ToString());
            lbl_Msg.Text = "Total " + RecordInserted + " Record Are Inseted In " + DTTable.Rows[iicouts]["TableName"].ToString() + " Table ";
            //pnl_loading.Visible = true;
            //Timer1.Enabled = true;
        }
        lblContentThreeDate.Text = "All Table Record Inserted Successfully";
        // Response.Redirect("Silent_Synchronoze.aspx?sid=" + ServerID + "&job=yes");
    }
    protected void btnRefresh1_OnClick(object sender, EventArgs e)
    {
        lbl_Msg.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff");
    }
    protected void btnRefresh2_OnClick(object sender, EventArgs e)
    {
        lblContentThreeDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff");
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