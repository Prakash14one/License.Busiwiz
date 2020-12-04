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




public partial class AccessRight : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static string Serverencstr = "";
    public static string compid = "";
    public static string Encryptkeycompsss = "";
    public static string encstr = "";
    SqlConnection condefaultinstance = new SqlConnection();
    SqlConnection conser;

    string allstring = "";
    SqlConnection conn;

    Boolean Portconn = false;
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
           // string sidd = BZ_Common.BZ_Encrypted("5");
            if (Request.QueryString["Cid"] != null)
            {
                string companyid = Request.QueryString["Cid"];
                lblcid.Text = companyid;
                Boolean Comp__Actve__Status = CompanyWizard.Company_Active_Status(companyid);
                Boolean Comp__Licen__Active = CompanyWizard.Company_LicenseExpire_Status(companyid);
                Boolean Server__Active = ServerWizard.Server_Active_Status(companyid);

                if (Comp__Actve__Status == true && Comp__Licen__Active == true && Server__Active == true)
                {
                    DataTable DTGetSid =CompanyWizard.SelectCompanyInfo(companyid);
                    string sid = DTGetSid.Rows[0]["ServerId"].ToString();
                    lblsid.Text = sid;
                    FillFrid(lblsid.Text);                  
                }
                else if (Comp__Actve__Status == false)
                {
                    lblmsg.Text = "Company not active ";
                }
                else if (Comp__Licen__Active == false)
                {
                    lblmsg.Text = "License Expired";
                }
                else if (Server__Active == false)
                {
                    lblmsg.Text = "Server Inactive";
                }
               
            }
            else
            {
                lblmsg.Text = "No any recrd for company"; 
            }
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Label6.Text == "1")
        {
           PortConnectionCheck();
        }
        Int64 counter = Convert.ToInt64(Label6.Text);
        Int64 lbl5time = Convert.ToInt64(lbl_che5time.Text);
        counter++;
        if (lbl_che5time.Text != "5")
        {
            while (counter == 30 || Portconn == true)
            {
                Timer1.Enabled = false;
                PortConnectionCheck();
                if (Portconn == true)
                {
                    Paneldoc.Visible = false;
                    SyncroniceData();
                    Boolean Insert_Today_Key = BtnABCKey(lblcid.Text, lblsid.Text);
                    if (Insert_Today_Key == true)
                    {
                        lblmsg.Text = "Successfully ";
                        ClosePort();
                        //string pg = Request.QueryString["pname"].ToString();
                        string scp = Request.QueryString["url23"].ToString();
                        //string scp1 = scp.Substring(scp.LastIndexOf('/') + 1);
                        //scp = scp.Replace(scp1, "");
                        //string url = scp + pg.ToString();
                        Response.Redirect(scp);
                    }
                    else
                    {
                        lblmsg.Text = "Some problem when we try to adding records in database ";
                    }
                }
                else
                {
                    lbl5time++;
                    Label6.Text = "1";
                    Timer1.Enabled = true;
                    lbl_che5time.Text = Convert.ToString(lbl5time);
                }
                return;
            }
            Label6.Text = Convert.ToString(counter);
        }
        else
        {
            Paneldoc.Visible = false;
            Timer1.Enabled = false;
            //lblportmsg.Text = "";
            lblmsg.Text = "Sorry Connection to satellite server not possible right now.. please check whether satellite server is on and connect to internet";
        }
    }
    protected void openconne(string sid)
    {
        DataTable ds = MyCommonfile.selectBZ(" Select  dbo.ClientMaster.ClientURL,dbo.ServerMasterTbl.* From  dbo.ServerMasterTbl INNER JOIN  dbo.ClientMaster ON dbo.ServerMasterTbl.Id = dbo.ClientMaster.ServerId  Where dbo.ServerMasterTbl.Id='" + sid + "' ");
        if (ds.Rows.Count > 0)
        {
            lblmsg.Text = "";
            Timer1.Enabled = true;
            Paneldoc.Visible = true;


            string sqlCompport28 = ds.Rows[0]["PortforCompanymastersqlistance"].ToString();//2810
            string Comp_serverweburl = ds.Rows[0]["Busiwizsatellitesiteurl"].ToString();
            string sqlserverport = ds.Rows[0]["port"].ToString();


            string mycurrenturlX = Request.Url.AbsoluteUri;
            Random random = new Random();
            int randomNumber = random.Next(1, 10);
            string Randomkeyid = Convert.ToString(randomNumber);

            string ClientURL = ds.Rows[0]["ClientURL"].ToString();

            string pagenamemainY = "Satelliteservfunction.aspx?PO=OpenPort&PortNo=" + BZ_Common.Encrypted_satsrvencryky(sqlserverport) + "";//&SilentPageRequestTblID=" + PageMgmt.Encrypted(SilentPageRequestTblID) + "

            string SilentPageRequestTblID = CompanyRelated.Insert_SilentPageRequestTbl(sid, pagenamemainY, DateTime.Now.ToString(), "", false, Randomkeyid, mycurrenturlX);

            //Page X               
            string url = "";
            url = "http://" + Comp_serverweburl + "/vfysrv.aspx?licensesilentpagerequesttblid=" + BZ_Common.Encrypted_satsrvencryky(SilentPageRequestTblID) + "&pageredirecturl=" + BZ_Common.Encrypted_satsrvencryky(pagenamemainY) + "&mstrsrvky=" + BZ_Common.Encrypted_satsrvencryky(BZ_Common.satsrvencryky()) + "&returnurl=" + BZ_Common.Encrypted_satsrvencryky(ClientURL) + "";
            //Response.Redirect("" + url + "");
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + url + "');", true);
        }
        else
        {
            lblmsg.Text = "Server no available"; 
        }
    
    }

    protected void PortConnectionCheck()
    {
        Portconn = false;
        conn = new SqlConnection();
        conn = ServerWizard.ServerDefaultInstanceConnetionTCP_Serverid(lblsid.Text);
        try
        {
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }         
            Portconn = true;
        }
        catch
        {          
            Portconn = false;
        }
    }




    //Close Port
    protected void ClosePort()
    {
        DataTable ds = MyCommonfile.selectBZ(" Select  dbo.ClientMaster.ClientURL,dbo.ServerMasterTbl.* From  dbo.ServerMasterTbl INNER JOIN  dbo.ClientMaster ON dbo.ServerMasterTbl.Id = dbo.ClientMaster.ServerId  Where dbo.ServerMasterTbl.Id='" + lblsid.Text + "' ");
        if (ds.Rows.Count > 0)
        {

            string sqlCompport28 = ds.Rows[0]["PortforCompanymastersqlistance"].ToString();//2810
            string Comp_serverweburl = ds.Rows[0]["Busiwizsatellitesiteurl"].ToString();
            string sqlserverport = ds.Rows[0]["port"].ToString();


            string mycurrenturlX = Request.Url.AbsoluteUri;
            Random random = new Random();
            int randomNumber = random.Next(1, 10);
            string Randomkeyid = Convert.ToString(randomNumber);

            string ClientURL = ds.Rows[0]["ClientURL"].ToString();

            string pagenamemainY = "Satelliteservfunction.aspx?CP=ClosePort&PortNo=" + BZ_Common.Encrypted_satsrvencryky(sqlserverport) + "";//&SilentPageRequestTblID=" + PageMgmt.Encrypted(SilentPageRequestTblID) + "

            string SilentPageRequestTblID = CompanyRelated.Insert_SilentPageRequestTbl(lblsid.Text, pagenamemainY, DateTime.Now.ToString(), "", false, Randomkeyid, mycurrenturlX);

            string url = "";
            url = "http://" + Comp_serverweburl + "/vfysrv.aspx?licensesilentpagerequesttblid=" + BZ_Common.Encrypted_satsrvencryky(SilentPageRequestTblID) + "&pageredirecturl=" + BZ_Common.Encrypted_satsrvencryky(pagenamemainY) + "&mstrsrvky=" + BZ_Common.Encrypted_satsrvencryky(BZ_Common.satsrvencryky()) + "&returnurl=" + BZ_Common.Encrypted_satsrvencryky(ClientURL) + "";

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + url + "');", true);
        }
    }





    protected void FillFrid(string sid)
    {
        string str = " ";
        str += " and dbo.ServerMasterTbl.Id=" + sid + "";      
        DataTable dtTemp = new DataTable();
        dtTemp = CreateData();
        conn = new SqlConnection();
        DataTable dtfindtab = MyCommonfile.selectBZ(@" SELECT DISTINCT dbo.ClientProductTableMaster.Id AS TableId, dbo.ServerMasterTbl.ServerName,  dbo.ServerMasterTbl.serverloction 
                                                     , dbo.ClientProductTableMaster.TableName,  dbo.ServerMasterTbl.serverloction, dbo.Satellite_Server_Sync_Job_Details.ID,dbo.Satelitte_Server_Sync_Job_Master.JobDateTime,   dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID, dbo.Satellite_Server_Sync_Job_Details.JobFinishFinishStatus, dbo.Satellite_Server_Sync_Job_Details.SyncedStatus, dbo.Satellite_Server_Sync_Job_Details.CheckingStatus ,  dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone, dbo.Satelite_Server_Sync_Log_Deatils.ID AS JobReordTableID,dbo.Satelite_Server_Sync_Log_Deatils.RecordID,dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone, dbo.SyncActionMaster.ActionName
                                                        From    dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.ClientProductTableMaster INNER JOIN
                                                        dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id ON 
                                                        dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster.TableID AND dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.ClientProductTableMaster.Id INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON 
                                                        dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id INNER JOIN dbo.ServerMasterTbl ON dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID = dbo.ServerMasterTbl.Id INNER JOIN
                                                        dbo.Satelite_Server_Sync_Log_Deatils ON dbo.Satellite_Server_Sync_Job_Details.ID = dbo.Satelite_Server_Sync_Log_Deatils.Satellite_Server_Sync_Job_Details_ID INNER JOIN dbo.SyncActionMaster ON dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone = dbo.SyncActionMaster.ID
                                                        Where  dbo.Satellite_Server_Sync_Job_Details.SyncedStatus=0  " + str + "");// and ServerMasterTbl.Id='" + serid + "' and dbo.Satelitte_Server_Sync_Job_Master.Id='" + ViewState["JobID"] + "' and
        
        grdserver.DataSource = dtfindtab;
        grdserver.DataBind();
        if (dtfindtab.Rows.Count == 0)
        {
            lblmsg.Text = "No any recrd for syncronice";
        }
        else
        {
            openconne(sid);
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






    protected void SyncroniceData()
    {
        string portopenserID = "";
        lblmsg.Text = "";
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
            if (cbItem.Checked == true)
            {
                if (lblseid.Text != seridstatus)
                {
                    if (portopenserID != lblseid.Text)
                    {
                        portopenserID = lblseid.Text;
                    }
                    try
                    {
                        conn = new SqlConnection();
                        conn = ServerWizard.ServerDefaultInstanceConnetionTCP_Serverid(lblseid.Text);
                        if (conn.State.ToString() != "Open")
                        {
                            conn.Open();
                        }
                        encstr = ServerWizard.ServerEncrDecriKEY(lblseid.Text);
                          string tablename = lbltabname.Text;

                        if (lbl_typeoperation.Text == "2" || lbl_typeoperation.Text == "3")
                        {
                            DeleteTableRecord(tablename, lbl_RecordID.Text);
                        }
                        if (lbl_typeoperation.Text == "1" || lbl_typeoperation.Text == "2")
                        {
                            DynamicalyTable(tablename, lbl_RecordID.Text);
                        }
                        totalrec++;
                        Boolean Deletestatus = Delete_Only_ID_Satelite_Server_Sync_Log_Deatils(lbl_JobReordTableID.Text);
                        Boolean statuss = Update_Satellite_Server_Sync_Job_Details(lbl_jobdetail.Text, true, true, DateTime.Now, true, DateTime.Now);
                        try
                        {
                            DataTable Satallite_Server_Sync_RecordsMasterTblID = MyCommonfile.selectBZ(" Select Id as SatalliteServerSyncTblTecordStatusID From Satallite_Server_Sync_RecordsMasterTbl Where TableId='" + lbl_TableId.Text + "' and ServerID='" + lblseid.Text + "' ");
                            if (lbl_typeoperation.Text == "1")
                            {
                                Insert___Satallite_Server_Sync_RecordsDetailTbl(Satallite_Server_Sync_RecordsMasterTblID.Rows[0]["Satallite_Server_Sync_RecordsMasterTblID"].ToString(), lbl_RecordID.Text, DateTime.Now, lbl_typeoperation.Text, lbl_typeoperation.Text);
                            }
                            if (lbl_typeoperation.Text == "3")
                            {
                                Delete___Satallite_Server_Sync_RecordsDetailTbl(Satallite_Server_Sync_RecordsMasterTblID.Rows[0]["Satallite_Server_Sync_RecordsMasterTblID"].ToString(), lbl_RecordID.Text);
                            }
                        }
                        catch
                        {
                        }
                        lblmsg.Text = " Successfully synchronization server record ";
                    }
                    catch
                    {
                        seridstatus = lblseid.Text;
                        lblmsg.Text = " some problem when synchronization with server ";//e1.ToString()+"<br>";
                    }
                }
            }           
        }
    }

    protected void DeleteTableRecord(string tanlename, string recordid)
    {
        string Temp2 = " Delete From " + tanlename + " Where ";
        string Temp3val = "";
        DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        if (dts1.Rows.Count > 0)
        {
            Temp2 += "" + dts1.Rows[0]["column_name"] + "=" + "'" + Encrypted(recordid) + "'";
        }
        try
        {
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            ccm.ExecuteNonQuery();
        }
        catch
        {
        }
    }
    protected void DynamicalyTable(string tanlename, string recordid)
    {
        string Temp2 = " INSERT INTO " + tanlename + "(  ";
        string Temp3val = "";
        DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        for (int k = 0; k < dts1.Rows.Count; k++)
        {
            Temp2 += ("" + dts1.Rows[k]["column_name"] + " ,");
        }
        Temp2 = Temp2.Remove(Temp2.Length - 1);
        Temp2 += ") VAlues";

        DataTable maxiddesid = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        DataTable dtr = MyCommonfile.selectBZ(" Select * From " + tanlename + " where " + dts1.Rows[0]["column_name"] + "=" + recordid + " ");
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
                    ccm.ExecuteNonQuery();

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
            ccm.ExecuteNonQuery();
        }
    }

    public Boolean Delete_Only_ID_Satelite_Server_Sync_Log_Deatils(string ID)
    {
        Boolean ReturnID = true;
        try
        {
            SqlCommand cmd = new SqlCommand("Satelite_Server_Sync_Log_Deatils_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.ExecuteNonQuery();
        }
        catch
        {
            ReturnID = false;
        }
        return ReturnID;
    }
    public static Boolean Update_Satellite_Server_Sync_Job_Details(string Satellite_Server_Sync_Job_DetailsID, Boolean SyncedStatus, Boolean CheckingStatus, DateTime CheckedDateTime, Boolean JobFinishFinishStatus, DateTime JobDetailDoneDatandtime)
    {
        Boolean Status = false;
        try
        {
            SqlConnection liceco = new SqlConnection();
            liceco = MyCommonfile.licenseconn();
            if (liceco.State.ToString() != "Open")
            {
                liceco.Open();
            }
            SqlCommand cmd = new SqlCommand("Satellite_Server_Sync_Job_Details_AddDelUpdtSelect", liceco);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "UpdateFinish");

            cmd.Parameters.AddWithValue("@ID", Satellite_Server_Sync_Job_DetailsID);
            cmd.Parameters.AddWithValue("@SyncedStatus", SyncedStatus);
            cmd.Parameters.AddWithValue("@CheckingStatus", CheckingStatus);
            cmd.Parameters.AddWithValue("@CheckedDateTime", CheckedDateTime);
            cmd.Parameters.AddWithValue("@JobFinishFinishStatus", JobFinishFinishStatus);
            cmd.Parameters.AddWithValue("@JobDetailDoneDatandtime", JobDetailDoneDatandtime);
            cmd.ExecuteNonQuery();
            liceco.Close();
            Status = true;
        }
        catch
        {
            Status = false;
        }

        return Status;
    }
    public Int64 Insert___Satallite_Server_Sync_RecordsDetailTbl(string SatalliteServerSyncTblTecordStatusID, string RecordId, DateTime LastSynDateTime, string TypeofOperationDone, string TyepeOfOperationReqd)
    {
        Int64 ReturnID = 0;
        try
        {
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
        }
        catch
        {
            ReturnID = 0;
        }
        return ReturnID;
    }
    public Boolean Delete___Satallite_Server_Sync_RecordsDetailTbl(string SatalliteServerSyncTblTecordStatusID, string RecordId)
    {
        Boolean ReturnID = true;
        try
        {
            SqlCommand cmd = new SqlCommand("Satallite_Server_Sync_RecordsDetailTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "DeleteRecordId");
            cmd.Parameters.AddWithValue("@SatalliteServerSyncTblTecordStatusID", SatalliteServerSyncTblTecordStatusID);
            cmd.Parameters.AddWithValue("@RecordId", RecordId);
            cmd.ExecuteNonQuery();

        }
        catch
        {
            ReturnID = false;
        }
        return ReturnID;
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

    //ABC--------------------------------------------------------------------------------
    //ABC--------------------------------------------------------------------------------
    //ABC--------------------------------------------------------------------------------
    
    public static Boolean BtnABCKey(string CompanyLoginId, string ServerId)
    {
        Boolean Status = true;
        Int64 X = 0;
        Int64 Y = 0;
        Int64 Z = 0;

        Int64 C1 = 0;
        Int64 C2 = 0;
        Int64 C3 = 0;
        Int64 C4 = 0;
        Int64 C5 = 0;

        DataTable dt_getCompanyABCMaster = MyCommonfile.selectBZ(" Select * From CompanyABCMaster where CompanyLoginId='" + CompanyLoginId + "' ");
        if (dt_getCompanyABCMaster.Rows.Count > 0)
        {
            Boolean Del_CABCD = CompKeyIns.Delete_CompanyABCDetail(CompanyLoginId);
            Boolean Del_CABCD_Server = CompKeyIns.Delete_CompanyABCDetail_Server(CompanyLoginId);
            if (Del_CABCD_Server == true && Del_CABCD_Server == true)
            {
                string txt_c = dt_getCompanyABCMaster.Rows[0]["C"].ToString();

                string txt_c1 = dt_getCompanyABCMaster.Rows[0]["C1"].ToString();
                string txt_c2 = dt_getCompanyABCMaster.Rows[0]["C2"].ToString();
                string txt_c3 = dt_getCompanyABCMaster.Rows[0]["C3"].ToString();
                string txt_c4 = dt_getCompanyABCMaster.Rows[0]["C4"].ToString();
                string txt_c5 = dt_getCompanyABCMaster.Rows[0]["C5"].ToString();

                C1 = Convert.ToInt64(txt_c1.Substring(0, 4));
                C2 = Convert.ToInt64(txt_c2.Substring(0, 4));
                C3 = Convert.ToInt64(txt_c3.Substring(0, 4));
                C4 = Convert.ToInt64(txt_c4.Substring(0, 4));
                C5 = Convert.ToInt64(txt_c5.Substring(0, 4));

                DateTime todaydatefull = DateTime.Now;
                todaydatefull = todaydatefull.AddDays(-1);
                string strdt = todaydatefull.ToString("MM-dd-yyyy");
                DataTable dtfunction = MyCommonfile.selectBZ(" Select * From FunctionMaster ");
                DateTime startDate = DateTime.Parse(strdt);
                DateTime expiryDate = startDate.AddDays(2);
                int DayInterval = 1;
                while (startDate <= expiryDate && Status == true)
                {
                    Random random = new Random();
                    int randomNumberA1 = random.Next(1, 6);
                    int randomNumberA2 = random.Next(1, 6);
                    int randomNumberA3 = random.Next(1, 6);
                    int randomNumberA4 = random.Next(1, 6);
                    int randomNumberA5 = random.Next(1, 6);

                    Int64 F1 = Convert.ToInt64(randomNumberA1);
                    Int64 F2 = Convert.ToInt64(randomNumberA2);
                    Int64 F3 = Convert.ToInt64(randomNumberA3);
                    Int64 F4 = Convert.ToInt64(randomNumberA4);
                    Int64 F5 = Convert.ToInt64(randomNumberA5);

                    DateTime datevalue = (Convert.ToDateTime(startDate.ToString("MM-dd-yyyy")));
                    string DD = datevalue.Day.ToString();
                    string MM = datevalue.Month.ToString();
                    string YYYY = datevalue.Year.ToString();
                    string ZDate = DD + "" + MM + "" + YYYY;

                    X = Convert.ToInt64(DD);
                    Y = Convert.ToInt64(MM);
                    Z = Convert.ToInt64(YYYY);

                    string dateid = DD + "" + MM + "" + YYYY;

                    String A1 = CompKeyIns.C1toC5GetA(C1, F1, DD, MM, YYYY);
                    String A2 = CompKeyIns.C1toC5GetA(C2, F2, DD, MM, YYYY);
                    String A3 = CompKeyIns.C1toC5GetA(C3, F3, DD, MM, YYYY);
                    String A4 = CompKeyIns.C1toC5GetA(C4, F4, DD, MM, YYYY);
                    String A5 = CompKeyIns.C1toC5GetA(C5, F5, DD, MM, YYYY);

                    string D1 = A1.Substring(0, 1);
                    string E1 = A1.Substring(1, A1.Length - 1);

                    string D2 = A2.Substring(0, 1);
                    string E2 = A2.Substring(1, A2.Length - 1);

                    string D3 = A3.Substring(0, 1);
                    string E3 = A3.Substring(1, A3.Length - 1);

                    string D4 = A4.Substring(0, 1);
                    string E4 = A4.Substring(1, A4.Length - 1);

                    string D5 = A5.Substring(0, 1);
                    string E5 = A5.Substring(1, A5.Length - 1);

                    //---------------------------------
                    //if (C1 != Convert.ToInt64(txt_c1.Substring(0, 4)))
                    //{
                    //}
                    string txt_ansc = "";
                    //----------------------------------------------------------------------------------------------------------------               
                    Boolean Insert_CABCD_License = CompKeyIns.Insert_CompanyABCDetail(CompanyLoginId, ZDate, A1, A2, A3, A4, A5, D1, D2, D3, D4, D5, E1, E2, E3, E4, E5, Convert.ToString(F1), Convert.ToString(F2), Convert.ToString(F3), Convert.ToString(F4), Convert.ToString(F5), Convert.ToString(C1), Convert.ToString(C2), Convert.ToString(C3), Convert.ToString(C4), Convert.ToString(C5), txt_ansc, ServerId);
                    if (Insert_CABCD_License == true)
                    {
                        Boolean Insert_CABCD_Server = CompKeyIns.Insert_CompanyABCDetail_SERVER(CompanyLoginId, ZDate, D1, D2, D3, D4, D5, E1, E2, E3, E4, E5, Convert.ToString(F1), Convert.ToString(F2), Convert.ToString(F3), Convert.ToString(F4), Convert.ToString(F5));
                        if (Insert_CABCD_Server == true)
                        {
                            Status = true;
                        }
                    }
                    startDate = startDate.AddDays(DayInterval);
                }
            }
            else
            {
                Status = false;
            }
        }
        else
        {
            Status = false;
        }
        return Status;
    }
}
