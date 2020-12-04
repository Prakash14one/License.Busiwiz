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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
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
    public static double size = 0;
    int StepId = 1;
    SqlConnection connMasterserver;
    SqlConnection connCompserver = new SqlConnection();
    string allstring = "";
    SqlConnection conn;
    protected void Page_Load(object sender, EventArgs e)
    {

       // Statik_FullTable();
        string sidd = BZ_Common.BZ_Encrypted("5");        
        if (!IsPostBack)
        {
            if (Request.QueryString["sid"] != null && Request.QueryString["tblid"] == null)
            {
                string sid = BZ_Common.BZ_Decrypted(Request.QueryString["sid"].ToString()); //kdQMwcj0lE8=              "5"; //
                ViewState["sid"] = sid;
                //----------------------------------------------------------------------------------------------------------              
                //-----------------------------------------------------------------------------------------------------------
                if (!IsPostBack)
                { 
                        btnsync_Click(sender, e);                    
                }
                DataTable dtfindtab = MyCommonfile.selectBZ(@" SELECT Count(dbo.Satelite_Server_Sync_Log_Deatils.ID) as TotalCount
                                                        From    dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id ON  dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster.TableID AND dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.ClientProductTableMaster.Id INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON  dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id INNER JOIN dbo.ServerMasterTbl ON dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID = dbo.ServerMasterTbl.Id INNER JOIN dbo.Satelite_Server_Sync_Log_Deatils ON dbo.Satellite_Server_Sync_Job_Details.ID = dbo.Satelite_Server_Sync_Log_Deatils.Satellite_Server_Sync_Job_Details_ID INNER JOIN dbo.SyncActionMaster ON dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone = dbo.SyncActionMaster.ID
                                                        Where  dbo.Satellite_Server_Sync_Job_Details.SyncedStatus=0  and ServerMasterTbl.Id='" + ViewState["sid"] + "' ");//  and dbo.Satelitte_Server_Sync_Job_Master.Id='" + ViewState["JobID"] + "' and
                lblportmsg.Text = "Please waite for some moment we syncronice " + dtfindtab.Rows[0]["TotalCount"] + " record at server ";
            }
            else
            {
            }
            //Any 1 Table Full Data Transfer 
            if (Request.QueryString["sid"] != null && Request.QueryString["tblid"] != null)
            {
                string sid = BZ_Common.BZ_Decrypted(Request.QueryString["sid"].ToString().Replace(" ", "+"));
                string tblid = BZ_Common.BZ_Decrypted(Request.QueryString["tblid"].ToString().Replace(" ", "+"));
                ViewState["sid"] = sid;
                ViewState["tblid"] = tblid;
                FullTableSync();
            }
            //All Table All Data Transfer
            if (Request.QueryString["sid"] != null && Request.QueryString["alltable"] != null)
            {
                string sid = BZ_Common.BZ_Decrypted(Request.QueryString["sid"].ToString().Replace(" ", "+"));
                ViewState["sid"] = sid;
               // string tblid = BZ_Common.BZ_Decrypted(Request.QueryString["tblid"].ToString().Replace(" ", "+"));               
                DataTable dts1 = MyCommonfile.selectBZ(" SELECT dbo.SatelliteSyncronisationrequiringTablesMaster.* FROM dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id WHERE (dbo.SatelliteSyncronisationrequiringTablesMaster.Status = '1' and ClientProductTableMaster.Active=1) ");
                for (int k = 0; k < dts1.Rows.Count; k++)
                {
                    ViewState["tblid"] = dts1.Rows[k]["TableID"].ToString();                    
                    FullTableSync();                    
                }              
            }
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (ViewState["sid"] != null )
        {
            //pnl_loading.Visible = true;
            Timer1.Enabled = true;
            ViewState["sid"] = ViewState["sid"];
            
            Int64 counter = Convert.ToInt64(lbl_Counter.Text);
            Int64 lbl5time = Convert.ToInt64(lbl_che5time.Text);
            FillFrid();
            SeprateDatabase();
            //lbltimemsg.Text += Convert.ToString(counter);
            counter++;
            lbl_Counter.Text = Convert.ToString(counter);
        }
        else
        {
            Timer1.Enabled = false;
            pnl_loading.Visible = false;
        }

    }

    protected void btnsync_Click(object sender, EventArgs e)
    {
        FillFrid();
       // SeprateDatabase();
       // FillFrid(ViewState["sid"].ToString());
    }

    protected void FillFrid()
    {      
        DataTable dtTemp = new DataTable();
        dtTemp = CreateData();
      
        DataTable dtfindtab = MyCommonfile.selectBZ(@" SELECT TOP(500) dbo.ClientProductTableMaster.Id AS TableId, dbo.ServerMasterTbl.ServerName,  dbo.ServerMasterTbl.serverloction , dbo.ClientProductTableMaster.TableName,  dbo.ServerMasterTbl.serverloction, dbo.Satellite_Server_Sync_Job_Details.ID,dbo.Satelitte_Server_Sync_Job_Master.JobDateTime,   dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID, dbo.Satellite_Server_Sync_Job_Details.JobFinishFinishStatus, dbo.Satellite_Server_Sync_Job_Details.SyncedStatus, dbo.Satellite_Server_Sync_Job_Details.CheckingStatus ,  dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone, dbo.Satelite_Server_Sync_Log_Deatils.ID AS JobReordTableID,dbo.Satelite_Server_Sync_Log_Deatils.RecordID,dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone, dbo.SyncActionMaster.ActionName
                                                        From    dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id ON  dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster.TableID AND dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.ClientProductTableMaster.Id INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON  dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id INNER JOIN dbo.ServerMasterTbl ON dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID = dbo.ServerMasterTbl.Id INNER JOIN dbo.Satelite_Server_Sync_Log_Deatils ON dbo.Satellite_Server_Sync_Job_Details.ID = dbo.Satelite_Server_Sync_Log_Deatils.Satellite_Server_Sync_Job_Details_ID INNER JOIN dbo.SyncActionMaster ON dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone = dbo.SyncActionMaster.ID
                                                        Where  dbo.Satellite_Server_Sync_Job_Details.SyncedStatus=0  and ServerMasterTbl.Id='" + ViewState["sid"] + "' ");//  and dbo.Satelitte_Server_Sync_Job_Master.Id='" + ViewState["JobID"] + "' and
        grdserver.DataSource = dtfindtab;
        grdserver.DataBind();
        if (dtfindtab.Rows.Count == 0)
        {
           // Boolean status2 = Update_Satelitte_Server_Sync_Job_Masters(Convert.ToString(ViewState["JobID"]), true, DateTime.Now);          
            pnltransst.Visible = false;
            lblmsg.Text = "synchronization successfully";
            Timer1.Enabled = false;
            pnl_loading.Visible = false;
        }
        else
        {
            pnltransst.Visible = false;
            Timer1.Enabled = true;
            pnl_loading.Visible = true;
        }       
    } 
    protected void SeprateDatabase()
    {
        Boolean connnection = false;
        string Delete_From = ""; 
        string PKname = "";
        conn = new SqlConnection();
        try
        {          
            conn = ServerWizard.ServerDefaultInstanceConnetionTCP_Serverid(ViewState["sid"].ToString());
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
            string pcateid = "";
            string seridstatus = "";
            conn = new SqlConnection();
            DataTable dtcln = new DataTable();
            foreach (GridViewRow item in grdserver.Rows)
            {
                string syncreqid = grdserver.DataKeys[item.RowIndex].Value.ToString();
                Label lbltabname = (Label)item.FindControl("lbltabname");
                Label lblseid = (Label)item.FindControl("lblseid");
                Label lblattempt = (Label)item.FindControl("lblattempt");
                CheckBox cbItem = (CheckBox)item.FindControl("cbItem");
                Label lbl_TableId = (Label)item.FindControl("lbl_TableId");
                Label lbl_jobdetail = (Label)item.FindControl("lbl_jobdetail");
                Label lbl_typeoperation = (Label)item.FindControl("lbl_typeoperation");
                Label lbl_JobReordTableID = (Label)item.FindControl("lbl_JobReordTableID");
                Label lbl_RecordID = (Label)item.FindControl("lbl_RecordID");
                Label lblname = (Label)item.FindControl("lblname");
                Boolean serconn = true;
                conn = ServerWizard.ServerDefaultInstanceConnetionTCP_Serverid(lblseid.Text);
                if (cbItem.Checked == true)
                {
                    if (lblseid.Text != seridstatus)
                    {
                        try
                        {
                            encstr = ServerWizard.ServerEncrDecriKEY(lblseid.Text);
                            string tablename = lbltabname.Text;
                            if (lbl_typeoperation.Text == "2" || lbl_typeoperation.Text == "3")
                            {
                              //  DeleteTableRecord(tablename, lbl_RecordID.Text);
                                //((((((((((--Need To  Delete Column))))))))))))))))))))
                                Delete_From = " Delete From " + tablename + " Where ";
                                string Temp3val = "";
                                DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
                                if (dts1.Rows.Count > 0)
                                {                                   
                                    PKname = TableRelated.SatelliteSyncronisationrequiringTablesMaster_WherePKIDName(lbl_TableId.Text);
                                    if (PKname.Length > 0)
                                    {
                                        Delete_From += "" + PKname + "=" + "'" + Encrypted(lbl_RecordID.Text) + "' ";
                                    }
                                    else
                                    {
                                        Delete_From += "" + dts1.Rows[0]["column_name"] + "=" + "'" + Encrypted(lbl_RecordID.Text) + "' ";
                                    }
                                }
                                try
                                {
                                    SqlCommand ccm = new SqlCommand(Delete_From, conn);
                                    if (conn.State.ToString() != "Open")
                                    {
                                        conn.Open();
                                    }
                                    ccm.ExecuteNonQuery();
                                    conn.Close();
                                }
                                catch
                                {
                                }
                                //"""""""""""""--CLOSE Need To  Delete Column"""""""""""""""""
                            }



                            if (lbl_typeoperation.Text == "1" || lbl_typeoperation.Text == "2")
                            {
                                //DynamicalyTable(tablename, lbl_RecordID.Text);
                                //((((((((((--Need To  Insert Column))))))))))))))))))))
                                string Insert_Into = " INSERT INTO " + tablename + "(  ";
                                string Temp3val = "";
                                DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
                                for (int k = 0; k < dts1.Rows.Count; k++)
                                {
                                    Insert_Into += ("" + dts1.Rows[k]["column_name"] + " ,");
                                }
                                Insert_Into = Insert_Into.Remove(Insert_Into.Length - 1);
                                Insert_Into += ") Values ";
                                DataTable maxiddesid = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");                                
                                PKname = TableRelated.SatelliteSyncronisationrequiringTablesMaster_WherePKIDName(lbl_TableId.Text);
                                if (PKname.Length > 0)
                                {
                                }
                                else
                                {
                                    PKname = dts1.Rows[0]["column_name"].ToString();
                                }
                                DataTable dtr = MyCommonfile.selectBZ(" Select * From " + tablename + " where " + PKname + "=" + lbl_RecordID.Text + " ");
                                foreach (DataRow itm in dtr.Rows)
                                {
                                    int c = 0;
                                    Insert_Into += " (";
                                    DataTable dtsccc = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
                                    for (int k = 0; k < dtsccc.Rows.Count; k++)
                                    {
                                        Insert_Into += "'" + Encrypted(Convert.ToString(itm["" + dtsccc.Rows[k]["column_name"] + ""])) + "' ,";
                                    }
                                    Insert_Into = Insert_Into.Remove(Insert_Into.Length - 1);
                                    Insert_Into += " )";
                                }

                                if (Insert_Into.Length > 0)
                                {
                                    SqlCommand ccm = new SqlCommand(Insert_Into, conn);
                                    if (conn.State.ToString() != "Open")
                                    {
                                        conn.Open();
                                    }
                                    ccm.ExecuteNonQuery();
                                    conn.Close();
                                    lblmsg.Text = "";
                                }
                                //"""""""""""""--CLOSE Need To  Insert Column"""""""""""""""""
                            }
                            totalrec++;
                            
                           // Boolean statuss = Update_Satellite_Server_Sync_Job_Details(lbl_jobdetail.Text, true, true, DateTime.Now, true, DateTime.Now);
                            try
                            {
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
                           // lblmsg.Text = " Successfully synchronization ";//" + totalrec + " records for " + lblname.Text + " server record " + lbl_RecordID.Text + "<br><br>";
                        }
                        catch
                        {
                            seridstatus = lblseid.Text;
                            lblmsg.Text = " Some problem when synchronization with " + lblname.Text + " server <br><br>";//e1.ToString()+"<br>";
                        }
                        Boolean Deletestatus = Delete_Only_ID_Satelite_Server_Sync_Log_Deatils(lbl_JobReordTableID.Text);
                    }
                }
                else
                {
                }
            }
        }
        else
        {
            lblmsg.Text = " No connection possible with server <br> you are tying to connect database with this connection string ( " + conn.ConnectionString + " )" ;//e1.ToString()+"<br>";
        }
    }
  




    protected void DeleteTableRecord(string tanlename, string recordid)
    {
        string Temp2 = " Delete From " + tanlename + " Where ";
        string Temp3val = "";
        DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        if (dts1.Rows.Count > 0)
        {
            Temp2 += "" + dts1.Rows[0]["column_name"]+"="+"'"+Encrypted(recordid)+"'";
        }       
        try
        {
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            ccm.ExecuteNonQuery();           
            conn.Close();
        }
        catch
        {
        }        
    }
    protected void DynamicalyTable(string tanlename,string recordid)
    {
        string Temp2 = " INSERT INTO " + tanlename + "(  ";
        string Temp3val = "";
        DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        for (int k = 0; k < dts1.Rows.Count; k++)
        {
            Temp2 += ("" + dts1.Rows[k]["column_name"] + " ,");
        }
        Temp2 = Temp2.Remove(Temp2.Length - 1);
        Temp2 += ") Values";

        DataTable maxiddesid = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        DataTable dtr = MyCommonfile.selectBZ(" Select * From " + tanlename + " where " + dts1.Rows[0]["column_name"] + "="+recordid+" ");
        int c = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            string cccd = "(";
            DataTable dtsccc = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
            for (int k = 0; k < dtsccc.Rows.Count; k++)
            {
                cccd += "'" + Encrypted(Convert.ToString(itm["" + dtsccc.Rows[k]["column_name"] + ""])) + "' ,";
            }
            cccd = cccd.Remove(cccd.Length - 1);
            cccd += " )";
            c++;
            if (c == 199)
            {
                c = 0;
                if (Temp3val.Length > 0)
                {
                    Temp3val += ",";
                }
                Temp3val += cccd;
                if (Temp3val.Length > 0)
                {
                    string tempstr = Temp2 + Temp3val;
                    SqlCommand ccm = new SqlCommand(tempstr, conn);
                    if (conn.State.ToString() != "Open")
                    {
                        conn.Open();
                    }
                    ccm.ExecuteNonQuery();                  
                    conn.Close();
                    

                }
                cccd = "";
                Temp3val = "";
            }
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }
            Temp3val += cccd;           
        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }          
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    public static string Encrypted(string strText)
    {

        return  Encrypt(strText, encstr);

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
    protected void SatelliteSyncronisationrequiringTablesMaster(string verid,string TableID)
    {
        SqlCommand cmdrX = new SqlCommand(" Delete  from  SatelliteSyncronisationrequiringTablesMaster where TableID='" + TableID + "'", conn);
        conn.Open();
        cmdrX.ExecuteNonQuery();
        conn.Close();
        string Temp3val = "";
        string temp = "";
        string Temp2 = "INSERT INTO SatelliteSyncronisationrequiringTablesMaster(Id,ProductVersionID,TableID,Name,Status,TableName)Values ";
        DataTable dtr = MyCommonfile.selectBZ(" Select Distinct * from SatelliteSyncronisationrequiringTablesMaster where status='1' and ProductVersionID='" + verid + "'");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }
            Temp3val += "('" + Convert.ToString(itm["Id"]) + "','" + Convert.ToString(itm["ProductVersionID"]) + "','" + Convert.ToString(itm["TableID"]) + "'," +
               " '" + Convert.ToString(itm["Name"]) + "','" + Convert.ToString(itm["Status"]) + "','" + Convert.ToString(itm["TableName"]) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }
        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SyncronisationrequiredTbl(string verid, string TableId)
    {
        SqlCommand cmdrX = new SqlCommand(" Delete  from  SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='"+TableId+"' ", conn);
        conn.Open();
        cmdrX.ExecuteNonQuery();
        conn.Close();
        string Temp3val = "";
        string temp = "";
        string Temp2 = " INSERT INTO SyncronisationrequiredTbl (ID,SatelliteSyncronisationrequiringTablesMasterID,DateandTime) Values ";
        DataTable dtr = MyCommonfile.selectBZ(" Select Distinct SyncronisationrequiredTbl.*   from  SyncronisationrequiredTbl inner join SatelliteSyncronisationrequiringTablesMaster on SatelliteSyncronisationrequiringTablesMaster.Id=SyncronisationrequiredTbl.SatelliteSyncronisationrequiringTablesMasterID where ProductVersionID='" + verid + "' and SyncronisationrequiredTbl.SatelliteSyncronisationrequiringTablesMasterID='" + TableId + "' ");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }
            Temp3val += "('" + Convert.ToString(itm["ID"]) + "','" + Convert.ToString(itm["SatelliteSyncronisationrequiringTablesMasterID"]) + "','" + Convert.ToString(itm["DateandTime"]) + "')";
            if (jk > 700)
            {
                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;
            }
            else
            {
                jk += 1;
            }
        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    public DataTable CreateData()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

         DataColumn prd1tid = new DataColumn();
         prd1tid.ColumnName = "TableId";
         prd1tid.DataType = System.Type.GetType("System.String");
         prd1tid.AllowDBNull = true;
         dtTemp.Columns.Add(prd1tid);
        


        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "ServerName";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "serverloction";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);

        DataColumn prd111c = new DataColumn();
        prd111c.ColumnName = "syncreq";
        prd111c.DataType = System.Type.GetType("System.String");
        prd111c.AllowDBNull = true;
        dtTemp.Columns.Add(prd111c);


        DataColumn prd111vx = new DataColumn();
        prd111vx.ColumnName = "servermasterID";
        prd111vx.DataType = System.Type.GetType("System.String");
        prd111vx.AllowDBNull = true;
        dtTemp.Columns.Add(prd111vx);
        DataColumn prd111v = new DataColumn();

        DataColumn prd1111v = new DataColumn();
        prd1111v.ColumnName = "DateandTime";
        prd1111v.DataType = System.Type.GetType("System.String");
        prd1111v.AllowDBNull = true;
        dtTemp.Columns.Add(prd1111v);
        DataColumn prd111vv = new DataColumn();
        prd111vv.ColumnName = "TableName";
        prd111vv.DataType = System.Type.GetType("System.String");
        prd111vv.AllowDBNull = true;
        dtTemp.Columns.Add(prd111vv);

        DataColumn ptd = new DataColumn();
        ptd.ColumnName = "PortalId";
        ptd.DataType = System.Type.GetType("System.String");
        ptd.AllowDBNull = true;
        dtTemp.Columns.Add(ptd);

        DataColumn ptdv = new DataColumn();
        ptdv.ColumnName = "tabdesname";
        ptdv.DataType = System.Type.GetType("System.String");
        ptdv.AllowDBNull = true;
        dtTemp.Columns.Add(ptdv);

        DataColumn ptdvv = new DataColumn();
        ptdvv.ColumnName = "Attempt";
        ptdvv.DataType = System.Type.GetType("System.String");
        ptdvv.AllowDBNull = true;
        dtTemp.Columns.Add(ptdvv);
        DataColumn ptdvv1 = new DataColumn();
        ptdvv1.ColumnName = "Msg";
        ptdvv1.DataType = System.Type.GetType("System.String");
        ptdvv1.AllowDBNull = true;
        dtTemp.Columns.Add(ptdvv1);
        return dtTemp;
    }
    protected DataTable selectSer(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, conn);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }   
    //New Table Insert Delete Update 
    public Boolean Update_Satelitte_Server_Sync_Job_Masters(string Satelitte_Server_Sync_Job_Master, Boolean JobFinishStatus, DateTime FinishDatetime)
    {
        Boolean Status = false;      
        try
        {
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
        }
        catch
        {
            Status = false;
            con.Close();
        }
        return Status;
    }
    //Satellite_Server_Sync_Job_Details  
   
    public Boolean Delete_Satellite_Server_Sync_Job_Details(string Satellite_Server_Sync_Job_DetailsID)
    {
        Boolean Status = false;
       
        try
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Satellite_Server_Sync_Job_Details_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "DeleteJD");
            cmd.Parameters.AddWithValue("@Satelitte_Server_Sync_Job_Master_ID", Satellite_Server_Sync_Job_DetailsID);
            cmd.ExecuteNonQuery();
            con.Close();
            Status = true;
        }
        catch
        {
            Status = false;
            con.Close();
        }
        return Status;
    }
    //Satelite_Server_Sync_Log_Deatils   
    public Boolean Delete_Satelite_Server_Sync_Log_Deatils(string Satellite_Server_Sync_Job_Details_ID)
    {
        Boolean ReturnID = true;       
        try
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Satelite_Server_Sync_Log_Deatils_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "DeleteJD");
            cmd.Parameters.AddWithValue("@Satelitte_Server_Sync_Job_Master_ID", Satellite_Server_Sync_Job_Details_ID);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch
        {
            ReturnID = false;
            con.Close();
        }
        return ReturnID;
    }
    public Boolean Delete_Only_ID_Satelite_Server_Sync_Log_Deatils(string ID)
    {
        Boolean ReturnID = true;
      
        try
        {
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
        }
        catch
        {
            ReturnID = false;
            con.Close();
        }
        return ReturnID;
    }
    //--------------------------Sync_Need_Logs------------------------
    public Boolean DELETE__Sync_Need_Logs__AddDelUpdtSelect(string LogId)
    {
        Boolean ReturnID = true;
     
        try
        {
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
        }
        catch
        {
            ReturnID = false;
            con.Close();
        }
        return ReturnID;
    }
    //--------------------------Sync_Need_Logs_AtServer_AddDelUpdtSelect------------------------  
    public Boolean DELETE__Sync_Need_Logs_AtServer_AddDelUpdtSelect(string LogId)
    {
        Boolean ReturnID = true;
        SqlConnection liceco = new SqlConnection();
        liceco = MyCommonfile.licenseconn();
        try
        {          
            if (liceco.State.ToString() != "Open")
            {
                liceco.Open();
            }
            SqlCommand cmd = new SqlCommand("Sync_Need_Logs_AtServer_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "DeleteLogId");
            cmd.Parameters.AddWithValue("@LogId", LogId);
            cmd.ExecuteNonQuery();
            liceco.Close();
        }
        catch
        {
            ReturnID = false;
            liceco.Close();
        }
        return ReturnID;
    }
    //Satallite_Server_Sync_RecordsDetailTbl
    public Int64 Insert___Satallite_Server_Sync_RecordsDetailTbl(string SatalliteServerSyncTblTecordStatusID, string RecordId, DateTime LastSynDateTime, string TypeofOperationDone, string TyepeOfOperationReqd)
    {
        Int64 ReturnID = 0;      
        try
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Satallite_Server_Sync_RecordsDetailTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            cmd.Parameters.AddWithValue("@SatalliteServerSyncTblTecordStatusID", SatalliteServerSyncTblTecordStatusID);
            cmd.Parameters.AddWithValue("@RecordId", RecordId);
            cmd.Parameters.AddWithValue("@LastSynDateTime", LastSynDateTime);
            cmd.Parameters.AddWithValue("@TypeofOperationDone", TypeofOperationDone);
            cmd.Parameters.AddWithValue("@TyepeOfOperationReqd", TyepeOfOperationReqd);
            object maxprocID = new object();
            maxprocID = cmd.ExecuteScalar();
            ReturnID = Convert.ToInt64(maxprocID);
            con.Close();
        }
        catch
        {
            ReturnID = 0;
            con.Close();
        }
        return ReturnID;
    }
    public Boolean Delete___Satallite_Server_Sync_RecordsDetailTbl(string SatalliteServerSyncTblTecordStatusID, string RecordId)
    {
        Boolean ReturnID = true;      
        try
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Satallite_Server_Sync_RecordsDetailTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "DeleteRecordId");
            cmd.Parameters.AddWithValue("@SatalliteServerSyncTblTecordStatusID", SatalliteServerSyncTblTecordStatusID);
            cmd.Parameters.AddWithValue("@RecordId", RecordId);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch
        {
            ReturnID = false;
            con.Close();
        }
        return ReturnID;
    }



    protected void FullTableSync()
    {
        Boolean connnection = false;
        string Delete_From = "";
        string PKname = "";
        conn = new SqlConnection();
        try
        {
            conn = ServerWizard.ServerDefaultInstanceConnetionTCP_Serverid(ViewState["sid"].ToString());
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
            encstr = ServerWizard.ServerEncrDecriKEY(ViewState["sid"].ToString());
            DataTable dt = MyCommonfile.selectBZ("Select * From ClientProductTableMaster where Id=" + ViewState["tblid"] + "");
            if (dt.Rows.Count > 0)
            {
                CreateTableDesign("" + dt.Rows[0]["TableName"].ToString() + "");
                try
                {
                    SqlCommand ccm = new SqlCommand(" Delete From " + dt.Rows[0]["TableName"].ToString() + " ", conn);
                    if (conn.State.ToString() != "Open")
                    {
                        conn.Open();
                    }
                    ccm.ExecuteNonQuery();
                    conn.Close();
                }
                catch
                {
                    conn.Close();
                }
                Dynamicaly_FullTable(dt.Rows[0]["TableName"].ToString(), dt.Rows[0]["Id"].ToString());   
            }
        }
    }
    protected void CreateTableDesign(string tablename)
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
                    //  strBC += ("" + DTBC.Rows[k]["column_name"] + " int Identity(1,1),");
                    strBC += ("" + DTBC.Rows[k]["column_name"] + " nvarchar(500),");
                }
                else
                {
                    strBC += ("" + DTBC.Rows[k]["column_name"] + " " + DTBC.Rows[k]["data_type"] + "(" + DTBC.Rows[k]["CHARACTER_MAXIMUM_LENGTH"] + "),");

                }
            }
            strBC = strBC.Remove(strBC.Length - 1);
            strBC += ")";
            strBC = strBC.Replace("bigint()", "nvarchar(500)");
            strBC = strBC.Replace("int()", "nvarchar(500)");
            strBC = strBC.Replace("(-1)", "(MAX)");
            strBC = strBC.Replace("datetime()", "nvarchar(500)");
            st1 = st1.Replace("nvarchar(50)", "nvarchar(500)");
            st1 = st1.Replace("decimal()", "nvarchar(500)");
            st1 = st1.Replace("decimal", "nvarchar(500)");
            strBC = strBC.Replace("bit()", "bit");
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
    protected void Dynamicaly_FullTable(string tanlename, string lbl_TableId)
    {
        string PKname = "";
        string InsertInto = " INSERT INTO " + tanlename + "(  ";
        string Temp3val = "";
        DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        for (int k = 0; k < dts1.Rows.Count; k++)
        {
            if (k == 0)
            {
                // PKname = dts1.Rows[k]["column_name"].ToString();
            }
            InsertInto += ("" + dts1.Rows[k]["column_name"] + " ,");
        }
        InsertInto = InsertInto.Remove(InsertInto.Length - 1);
        InsertInto += ") VAlues";        
        string AfterVAlues = "";
        DataTable maxiddesid = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        string QueryName = "";


        //PKname = TableRelated.SatelliteSyncronisationrequiringTablesMaster_WherePKIDName(lbl_TableId);
        //String WhereID = TableRelated.SatelliteSyncronisationrequiringTablesMaster_WhereWhereID(lbl_TableId);
        //QueryName = TableRelated.SatelliteSyncronisationrequiringTablesMaster_Where(lbl_TableId);
        //if (QueryName.Length > 0)
        //{
        //    if (WhereID == "1")
        //    {
        //        QueryName = " Where " + PKname + " IN ( " + QueryName + "=" + lbl_priceplanid.Text + ")";
        //    }
        //    if (WhereID == "2")
        //    {
        //        QueryName = " Where " + PKname + " IN ( " + QueryName + "=" + lbl_version.Text + ")";
        //    }
        //}

        DataTable dtr = MyCommonfile.selectBZ(" Select * From " + tanlename + " " + QueryName + "");
        try
        {
            DataTable dtrcount = MyCommonfile.selectBZ(" Select Count(" + PKname + ") as PKname From " + tanlename + " " + QueryName + "");
            string ss = TableRelated.AAAAAAA_Record(tanlename, dtrcount.Rows[0]["PKname"].ToString(), lbl_serverid.Text);
        }
        catch
        {
        }
        int c = 0;
        foreach (DataRow itm in dtr.Rows)
        {
            string cccd = InsertInto + " (";
            DataTable dtsccc = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
            for (int k = 0; k < dtsccc.Rows.Count; k++)
            {
                cccd += "'" + Encrypted(Convert.ToString(itm["" + dtsccc.Rows[k]["column_name"] + ""])) + "' ,";
            }
            cccd = cccd.Remove(cccd.Length - 1);
            cccd += " )";
            if (Temp3val.Length > 0)
            {
                // Temp3val += ",";
            }
            Temp3val += cccd;
        }
        if (Temp3val.Length > 0)
        {
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            SqlCommand ccm = new SqlCommand(Temp3val, conn);
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }


    protected void Statik_FullTable()
    {

        //Table_DatabaseSQL.Create_Temp_Table_Design_With_Change_Datatype("pageplaneaccesstbl");
        return;
        encstr = "c7171b1e96fc3bbZ8wAS";
        string PKname = "";
        string InsertInto = " INSERT INTO pageplaneaccesstbl1 (Id,Pageid,Pagename,Priceplanid,pageaccess) VAlues";
        string Temp3val = "";
        DataTable dtr = MyCommonfile.selectBZ(" Select * From pageplaneaccesstbl");        
        int c = 0;
        foreach (DataRow itm in dtr.Rows)
        {
            c++;
            string cccd = "";
            cccd += "('" + Encrypted(Convert.ToString(itm["Id"])) + "' ,'" + Encrypted(Convert.ToString(itm["Pageid"])) + "','" + Encrypted(Convert.ToString(itm["Pagename"])) + "','" + Encrypted(Convert.ToString(itm["Priceplanid"])) + "','" + Encrypted(Convert.ToString(itm["pageaccess"])) + "')";                                    
            Temp3val += cccd +",";
            if (Temp3val.Length > 0 && c == 200)
            {
                Temp3val = Temp3val.Remove(Temp3val.Length - 1);
                string q = InsertInto + Temp3val;
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand ccm = new SqlCommand(q, con);
                ccm.ExecuteNonQuery();
                con.Close();
                c = 0;
                cccd = "";
                Temp3val = "";
            }
        }
        if (Temp3val.Length > 0)
        {
            Temp3val = Temp3val.Remove(Temp3val.Length - 1);
            string q = InsertInto + Temp3val;
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand ccm = new SqlCommand(q, con);
            ccm.ExecuteNonQuery();
            con.Close();
        }
    }
}
