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
using System.Data.Sql;
using System.ServiceModel;
using System.Data.SqlTypes;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class SyncData : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    DataSet dt;
    SqlConnection conn;
    public SqlConnection connweb;
    public static string encstr = "";
    double counter = 0;


    Boolean Portconn = false;
    //Stopwatch sw = new Stopwatch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            imgrefreshstaname_Click(sender, e);
            FillServer();
            FillLBTable();
            FillFrid_Sync_Need_Logs_AtServer();
            //-----------------------------------------------------------------------------------------------------------
              
            FillFrid(); 
            Fillgrid2();
        }
    }
    protected void FillServer()
    {
        DataTable DTServer = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.ServerMasterTbl.Id,dbo.ServerMasterTbl.ServerName FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id Where  dbo.ServerMasterTbl.Status=1 and dbo.CompanyMaster.Active=1 ");//and dbo.ServerMasterTbl.Id=5
        ddlserver.DataSource = DTServer;
        ddlserver.DataTextField = "ServerName";
        ddlserver.DataValueField = "Id";
        ddlserver.DataBind();

    }
    protected void ddlserver_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlserver.SelectedIndex > 0)
        {
           
        }
        pnl_serverselect.Visible = true;
        imgrefreshstaname_Click(sender, e);       
        FillLBTable();
        FillFrid_Sync_Need_Logs_AtServer();
        FillFrid(); 
        Fillgrid2();
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
                    ClosePort();
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
            lblmsg.Text = "Sorry Connection to satellite server not possible right now .. please check whether satellite server is on and connect to internet"; 
        }
    }
    protected void btnsyncsilent_Click(object sender, EventArgs e)
    {    

        DataTable ds = MyCommonfile.selectBZ(" Select  dbo.ClientMaster.ClientURL,dbo.ServerMasterTbl.* From  dbo.ServerMasterTbl INNER JOIN  dbo.ClientMaster ON dbo.ServerMasterTbl.Id = dbo.ClientMaster.ServerId  Where dbo.ServerMasterTbl.Id='" + ddlserver.SelectedValue + "' ");
        if (ds.Rows.Count > 0)
        {
            lblmsg.Text = "";
            //Timer1.Enabled = true;
            //Paneldoc.Visible = true;

            string sqlCompport28 = ds.Rows[0]["PortforCompanymastersqlistance"].ToString();//2810
            string Comp_serverweburl = ds.Rows[0]["Busiwizsatellitesiteurl"].ToString();
            string sqlserverport = ds.Rows[0]["port"].ToString();

            string mycurrenturlX = Request.Url.AbsoluteUri;
            Random random = new Random();
            int randomNumber = random.Next(1, 10);
            string Randomkeyid = Convert.ToString(randomNumber);

            string ClientURL = ds.Rows[0]["ClientURL"].ToString();

            string pagenamemainY = "Satelliteservfunction.aspx?PO=OpenPort&PortNo=" + BZ_Common.Encrypted_satsrvencryky(sqlserverport) + "";//&SilentPageRequestTblID=" + PageMgmt.Encrypted(SilentPageRequestTblID) + "

            string SilentPageRequestTblID = CompanyRelated.Insert_SilentPageRequestTbl(ddlserver.SelectedValue, pagenamemainY, DateTime.Now.ToString(), "", false, Randomkeyid, mycurrenturlX);
            //Page X               
            string url = "";
            url = "http://" + Comp_serverweburl + "/vfysrv.aspx?licensesilentpagerequesttblid=" + BZ_Common.Encrypted_satsrvencryky(SilentPageRequestTblID) + "&pageredirecturl=" + BZ_Common.Encrypted_satsrvencryky(pagenamemainY) + "&mstrsrvky=" + BZ_Common.Encrypted_satsrvencryky(BZ_Common.satsrvencryky()) + "&returnurl=" + BZ_Common.Encrypted_satsrvencryky(ClientURL) + "";
            //Response.Redirect("" + url + "");
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + url + "');", true);

            Response.Redirect("http://license.busiwiz.com/BeforeLogin/Silent_SyncData_WithServer.aspx?sid=" + BZ_Common.BZ_Encrypted(ddlserver.SelectedValue) + "Transfer=1");
           // Response.Redirect("http://license.busiwiz.com/Silent_Sync_Need_DailySync.aspx?sid=" +BZ_Common.BZ_Encrypted(ddlserver.SelectedValue) + "");
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
        conn = ServerWizard.ServerDefaultInstanceConnetionTCP_Serverid(ddlserver.SelectedValue);
        try
        {
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            link_openport.Text = "";
            link_openport.Enabled = false;
            Portconn = true;
        }
        catch
        {
            link_openport.Text = "";
            link_openport.Enabled = true;
            Portconn = false;
        }
    }



    protected void btnsynTables(object sender, EventArgs e)
    {
        string te = "http://license.busiwiz.com/BeforeLogin/Silent_Sync_RequirDailyUpdationTable.aspx?sid=kdQMwcj0lE8=&addserjob=1";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgrefreshstaname_Click(object sender, EventArgs e)
    {
        DataTable ds = MyCommonfile.selectBZ(" Select LogId  From Sync_Need_Logs ");
        if (ds.Rows.Count > 0)
        {
            DataTable dscount = MyCommonfile.selectBZ(" Select Count(LogId) AS LogId  From Sync_Need_Logs ");
            lblnewrecordforsync.Text = dscount.Rows[0]["LogId"].ToString() + " new updation available log table click here for send as create job  ";
            imgrefreshstaname.Visible = true;
        }
        else
        {
            imgrefreshstaname.Visible = false;
            lblnewrecordforsync.Text = "";
        }
    }
    //
   
  

    protected void FillLBTable()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT dbo.ClientProductTableMaster.TableName , dbo.ClientProductTableMaster.Id, dbo.SatelliteSyncronisationrequiringTablesMaster.Id AS syncId FROM dbo.SatelliteSyncronisationrequiringTablesMaster INNER JOIN dbo.ClientProductTableMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id where ClientProductTableMaster.active=1  order by ClientProductTableMaster.TableName ");// and ClientProductTableMaster.Databaseid='" + LBproductDB + "'       
            DDLLiceTableName.DataSource = dtcln;
            DDLLiceTableName.DataValueField = "Id";
            DDLLiceTableName.DataTextField = "TableName";
            DDLLiceTableName.DataBind();
            DDLLiceTableName.Items.Insert(0, "--Select--");
            DDLLiceTableName.Items[0].Value = "0";        
    }

    protected void FillFrid_Sync_Need_Logs_AtServer()
    {
        string str = " ";
        DataTable dtTemp = new DataTable();
        dtTemp = CreateData();
        DataTable dtfindtab = MyCommonfile.selectBZ(@" SELECT  Id, ServerName FROM dbo.ServerMasterTbl WHERE (Status = 1) AND (Id IN (SELECT sid AS Id FROM dbo.Sync_Need_Logs_AtServer)) ");
        GridView1.DataSource = dtfindtab;
        GridView1.DataBind();
        foreach (GridViewRow item in GridView1.Rows)
        {
            Label lbl_sId = (Label)item.FindControl("lbl_sId");
            Label lblrecordid = (Label)item.FindControl("lblrecordid");

            DataTable dtfindtabb = MyCommonfile.selectBZ(@"SELECT Count(Id) as TotalCount FROM dbo.Sync_Need_Logs_AtServer where sid='" + lbl_sId.Text + "'");
            lblrecordid.Text = "" + dtfindtabb.Rows[0]["TotalCount"] + "";
        }      
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {       
        if (e.CommandName == "backup")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lbl_sId = row.FindControl("lbl_sId") as Label;
            lbl_sId.Text = lbl_sId.Text;
            Response.Redirect("http://license.busiwiz.com/BeforeLogin/Silent_Sync_RequirDailyUpdationTable.aspx?sid=" + BZ_Common.BZ_Encrypted(lbl_sId.Text) + "&addjob123=1");
        }       
    }  


  

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillFrid();       
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
                        //Convert.ToString(dtre.Rows[0]["Enckey"]);                                    
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
            else
            {
            }
        }
        FillFrid();
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
    //Close Port
    protected void ClosePort()
    {
        DataTable ds = MyCommonfile.selectBZ(" Select  dbo.ClientMaster.ClientURL,dbo.ServerMasterTbl.* From  dbo.ServerMasterTbl INNER JOIN  dbo.ClientMaster ON dbo.ServerMasterTbl.Id = dbo.ClientMaster.ServerId  Where dbo.ServerMasterTbl.Id='" + ddlserver.SelectedValue + "' ");
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

            string SilentPageRequestTblID = CompanyRelated.Insert_SilentPageRequestTbl(ddlserver.SelectedValue, pagenamemainY, DateTime.Now.ToString(), "", false, Randomkeyid, mycurrenturlX);

            string url = "";
            url = "http://" + Comp_serverweburl + "/vfysrv.aspx?licensesilentpagerequesttblid=" + BZ_Common.Encrypted_satsrvencryky(SilentPageRequestTblID) + "&pageredirecturl=" + BZ_Common.Encrypted_satsrvencryky(pagenamemainY) + "&mstrsrvky=" + BZ_Common.Encrypted_satsrvencryky(BZ_Common.satsrvencryky()) + "&returnurl=" + BZ_Common.Encrypted_satsrvencryky(ClientURL) + "";

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + url + "');", true);
        }
    }
    //New Table Insert Delete Update
    public static Boolean Update_Satelitte_Server_Sync_Job_Masters(string Satelitte_Server_Sync_Job_Master, Boolean JobFinishStatus, DateTime FinishDatetime)
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
            SqlCommand cmd = new SqlCommand("Satelitte_Server_Sync_Job_Master_AddDelUpdtSelect", liceco);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "UpdateFinish");
            cmd.Parameters.AddWithValue("@ID", Satelitte_Server_Sync_Job_Master);
            cmd.Parameters.AddWithValue("@JobFinishStatus", JobFinishStatus);
            cmd.Parameters.AddWithValue("@FinishDatetime", FinishDatetime);
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
    //Satellite_Server_Sync_Job_Details
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
    public static Boolean Delete_Satellite_Server_Sync_Job_Details(string Satellite_Server_Sync_Job_DetailsID)
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
            cmd.Parameters.AddWithValue("@StatementType", "DeleteJD");
            cmd.Parameters.AddWithValue("@Satelitte_Server_Sync_Job_Master_ID", Satellite_Server_Sync_Job_DetailsID);
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
    //Satelite_Server_Sync_Log_Deatils
    public Boolean Delete_Satelite_Server_Sync_Log_Deatils(string Satellite_Server_Sync_Job_Details_ID)
    {
        Boolean ReturnID = true;
        try
        {
            SqlCommand cmd = new SqlCommand("Satelite_Server_Sync_Log_Deatils_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "DeleteJD");
            cmd.Parameters.AddWithValue("@Satelitte_Server_Sync_Job_Master_ID", Satellite_Server_Sync_Job_Details_ID);
            cmd.ExecuteNonQuery();
        }
        catch
        {
            ReturnID = false;
        }
        return ReturnID;
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
    //--------------------------Sync_Need_Logs------------------------
    public Boolean DELETE__Sync_Need_Logs__AddDelUpdtSelect(string LogId)
    {
        Boolean ReturnID = true;
        try
        {
            SqlCommand cmd = new SqlCommand("Sync_Need_Logs_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@LogId", LogId);
            cmd.ExecuteNonQuery();
        }
        catch
        {
            ReturnID = false;
        }
        return ReturnID;
    }
    //--------------------------Sync_Need_Logs_AtServer_AddDelUpdtSelect------------------------
    public Boolean DELETE__Sync_Need_Logs_AtServer_AddDelUpdtSelect(string LogId)
    {
        Boolean ReturnID = true;
        try
        {
            SqlCommand cmd = new SqlCommand("Sync_Need_Logs_AtServer_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "DeleteLogId");
            cmd.Parameters.AddWithValue("@LogId", LogId);
            cmd.ExecuteNonQuery();
        }
        catch
        {
            ReturnID = false;
        }
        return ReturnID;
    }
    //Satallite_Server_Sync_RecordsDetailTbl
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


    protected void FillFrid()
    {
        string str = " ";

        str += " and dbo.ServerMasterTbl.Id=" + ddlserver.SelectedValue + "";

        if (txtfdate.Text.Length > 0 && txttodate.Text.Length > 0)
        {
            str += " and  Cast(Satelitte_Server_Sync_Job_Master.JobDateTime as Date) between '" + txtfdate.Text + "' and '" + txttodate.Text + "' ";
        }
        if (DDLLiceTableName.SelectedIndex > 0)
        {
            str += " and dbo.ClientProductTableMaster.Id=" + DDLLiceTableName.SelectedValue + "";
        }
        DataTable dtTemp = new DataTable();
        dtTemp = CreateData();
        conn = new SqlConnection();
        DataTable dtfindtab = MyCommonfile.selectBZ(@" SELECT DISTINCT dbo.ClientProductTableMaster.Id AS TableId, dbo.ServerMasterTbl.ServerName, dbo.ServerMasterTbl.serverloction, dbo.ClientProductTableMaster.TableName ,dbo.ServerMasterTbl.Id as SatelliteServerID FROM     dbo.Satellite_Server_Sync_Job_Details INNER JOIN
                                                       dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id ON dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster.TableID AND dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.ClientProductTableMaster.Id INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id INNER JOIN dbo.ServerMasterTbl ON dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID = dbo.ServerMasterTbl.Id
                                                       WHERE (dbo.Satellite_Server_Sync_Job_Details.SyncedStatus = 0 ) " + str + "  ");


        grdserver.DataSource = dtfindtab;
        grdserver.DataBind();
        foreach (GridViewRow item in grdserver.Rows)
        {
            Label lbl_TableId = (Label)item.FindControl("lbl_TableId");
            Label lblrecordid = (Label)item.FindControl("lblrecordid");

            DataTable dtfindtabb = MyCommonfile.selectBZ(@" SELECT Count(dbo.Satelite_Server_Sync_Log_Deatils.ID) as TotalCount
                                                        From    dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id ON  dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.SatelliteSyncronisationrequiringTablesMaster.TableID AND dbo.Satellite_Server_Sync_Job_Details.TableID = dbo.ClientProductTableMaster.Id INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON  dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id INNER JOIN dbo.ServerMasterTbl ON dbo.Satelitte_Server_Sync_Job_Master.SatelliteServerID = dbo.ServerMasterTbl.Id INNER JOIN dbo.Satelite_Server_Sync_Log_Deatils ON dbo.Satellite_Server_Sync_Job_Details.ID = dbo.Satelite_Server_Sync_Log_Deatils.Satellite_Server_Sync_Job_Details_ID INNER JOIN dbo.SyncActionMaster ON dbo.Satelite_Server_Sync_Log_Deatils.TypeOfOperationDone = dbo.SyncActionMaster.ID
                                                        Where  dbo.Satellite_Server_Sync_Job_Details.TableID='" + lbl_TableId.Text + "' and dbo.Satellite_Server_Sync_Job_Details.SyncedStatus=0  and ServerMasterTbl.Id='" + ddlserver.SelectedValue + "' ");//  and dbo.Satelitte_Server_Sync_Job_Master.Id='" + ViewState["JobID"] + "' and
            lblrecordid.Text = "" + dtfindtabb.Rows[0]["TotalCount"] + "";
        }
    }
    protected void Fillgrid2()
    {       
        DataTable dtTemp = new DataTable();
        dtTemp = CreateData();
        conn = new SqlConnection();
        DataTable dtfindtab = MyCommonfile.selectBZ(@" SELECT DISTINCT dbo.ClientProductTableMaster.Id AS TableId, dbo.ClientProductTableMaster.TableTitle, dbo.SatelliteSyncronisationrequiringTablesMaster.Id, dbo.ClientProductTableMaster.TableName FROM dbo.ClientProductTableMaster INNER JOIN dbo.SatelliteSyncronisationrequiringTablesMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id  ");
        GridView2.DataSource = dtfindtab;
        GridView2.DataBind();
        DataTable dtr;
        foreach (GridViewRow item in GridView2.Rows)
        {
            Label lblTableId = (Label)item.FindControl("lblTableId");
            Label lbltabname = (Label)item.FindControl("lbltabname");
            Label lbl_total_record_for_ser = (Label)item.FindControl("lbl_total_record_for_ser");
            lbl_total_record_for_ser.Text = "";
            Label lbl_total_record_in_license_table = (Label)item.FindControl("lbl_total_record_in_license_table");
            lbl_total_record_in_license_table.Text = "";
            try
            {
                DataTable DtWhere = MyCommonfile.selectBZ(" Select * FROM SatelliteSyncronisationrequiringTablesMaster_SerWhere Where TableId='" + lblTableId.Text + "'");
                if (DtWhere.Rows.Count > 0)
                {
                    //Select_Query                        
                    string PKTableName = DtWhere.Rows[0]["PKTableName"].ToString();
                    string PKIdName = DtWhere.Rows[0]["PKIdName"].ToString();
                    string Select_Query = DtWhere.Rows[0]["Select_Query"].ToString();
                    string WhereForPKID = "";
                    // WhereForPKID = " Where " + PKTableName + "." + PKIdName + "=" + RecordID;
                    string SelectWhere2 = " Where PricePlanMaster.PricePlanId IN ( Select PricePlanId From  CompanyMaster Where active=1 and ServerId=" + ddlserver.SelectedValue + ") ";
                    Select_Query = Select_Query + WhereForPKID + SelectWhere2;
                    dtr = MyCommonfile.selectBZ("" + Select_Query + " ");
                    lbl_total_record_for_ser.Text = Convert.ToString(dtr.Rows.Count);
                }
                else
                {
                    dtr = MyCommonfile.selectBZ(" Select * From " + lbltabname.Text + "");
                    lbl_total_record_for_ser.Text = Convert.ToString(dtr.Rows.Count);
                }
                dtr = MyCommonfile.selectBZ(" Select * From " + lbltabname.Text + "");
                lbl_total_record_in_license_table.Text = Convert.ToString(dtr.Rows.Count);
            }
            catch
            {
            }           
        }  
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "backup")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lblTableId = row.FindControl("lblTableId") as Label;
            lblTableId.Text = lblTableId.Text;
            Response.Redirect("http://license.busiwiz.com/Silent_Sync_Need_DailySync.aspx?sid=" + BZ_Common.BZ_Encrypted(ddlserver.SelectedValue) + "&tblid=" + BZ_Common.BZ_Encrypted(lblTableId.Text) + "");
        }
    }

    protected void Btn_GenerateScript_Click(object sender, EventArgs e)
    {
        DataTable dtr;
         
        foreach (GridViewRow item in GridView2.Rows)
        {
            string Select_Query ="";
            Label lblTableId = (Label)item.FindControl("lblTableId");
            Label lbltabname = (Label)item.FindControl("lbltabname");
            Label lbl_total_record_for_ser = (Label)item.FindControl("lbl_total_record_for_ser");
            lbl_total_record_for_ser.Text = "";
            Label lbl_total_record_in_license_table = (Label)item.FindControl("lbl_total_record_in_license_table");
            lbl_total_record_in_license_table.Text = "";
            try
            {
                //DataTable DtWhere = MyCommonfile.selectBZ(" Select * FROM SatelliteSyncronisationrequiringTablesMaster_SerWhere Where TableId='" + lblTableId.Text + "'");
                //if (DtWhere.Rows.Count > 0)
                //{
                //    //Select_Query                        
                //    string PKTableName = DtWhere.Rows[0]["PKTableName"].ToString();
                //    string PKIdName = DtWhere.Rows[0]["PKIdName"].ToString();
                //     Select_Query = DtWhere.Rows[0]["Select_Query"].ToString();
                //    string WhereForPKID = "";
                //    // WhereForPKID = " Where " + PKTableName + "." + PKIdName + "=" + RecordID;
                //    string SelectWhere2 = " Where PricePlanMaster.PricePlanId IN ( Select PricePlanId From  CompanyMaster Where active=1 and ServerId=" + ddlserver.SelectedValue + ") ";
                //    Select_Query = Select_Query + WhereForPKID + SelectWhere2;
                //    //dtr = MyCommonfile.selectBZ("" + Select_Query + " ");                   
                //}
                //else
                //{
                //  //  dtr = MyCommonfile.selectBZ(" Select * From " + lbltabname.Text + "");                   
                //    Select_Query=" Select * From " + lbltabname.Text;
                //}
                Dynamicaly_FullTable(lbltabname.Text, lblTableId.Text);
            }
            catch
            {
            }
        }  
    }

    protected void Dynamicaly_FullTable(string tanlename, string lbl_TableId)
    {
        encstr = ServerWizard.ServerEncrDecriKEY(ddlserver.SelectedValue);
        string PKname = "";
        string Temp2 = " INSERT INTO " + tanlename + "(  ";
        string Temp3val = "";
        DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        for (int k = 0; k < dts1.Rows.Count; k++)
        {
            if (k == 0)
            {
                // PKname = dts1.Rows[k]["column_name"].ToString();
            }
            Temp2 += ("" + dts1.Rows[k]["column_name"] + " ,");
        }
        Temp2 = Temp2.Remove(Temp2.Length - 1);
        Temp2 += ") VAlues";
        string InsertInto = Temp2;
        string AfterVAlues = "";
        DataTable maxiddesid = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        string QueryName = "";
        DataTable dtr;
        DataTable DtWhere = MyCommonfile.selectBZ(" Select * FROM SatelliteSyncronisationrequiringTablesMaster_SerWhere Where TableId='" + lbl_TableId + "'");
        if (DtWhere.Rows.Count > 0)
        {
            //Select_Query                        
            string PKTableName = DtWhere.Rows[0]["PKTableName"].ToString();
            string PKIdName = DtWhere.Rows[0]["PKIdName"].ToString();
            string Select_Query = DtWhere.Rows[0]["Select_Query"].ToString();
            string WhereForPKID = "";
            // WhereForPKID = " Where " + PKTableName + "." + PKIdName + "=" + RecordID;
            string SelectWhere2 = " and PricePlanMaster.PricePlanId IN ( Select PricePlanId From  CompanyMaster Where active=1 and ServerId=" + ddlserver.SelectedValue  + ") ";
            Select_Query = Select_Query + WhereForPKID + SelectWhere2;
            dtr = MyCommonfile.selectBZ("" + Select_Query + " ");
        }
        else
        {
            dtr = MyCommonfile.selectBZ(" Select * From " + tanlename + " " + QueryName + "");
        }
        try
        {
            //DataTable dtrcount = MyCommonfile.selectBZ(" Select Count(" + PKname + ") as PKname From " + tanlename + " " + QueryName + "");
            //string ss = TableRelated.AAAAAAA_Record(tanlename, dtrcount.Rows[0]["PKname"].ToString(), ddlserver.SelectedValue);
        }
        catch
        {
        }
        int c = 0;
        int Cont = 0;
        string cccd = "";

        string FilenamewithExt = "";
        string filepath =Convert.ToString(Server.MapPath(""));
        foreach (DataRow itm in dtr.Rows)
        {
             FilenamewithExt = tanlename;
            c++;
            cccd = InsertInto + " (";
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
            if (Temp3val.Length > 0 && c == 200)
            {
                Cont++;
                //if (conn.State.ToString() != "Open")
                //{
                //    conn.Open();
                //}
                //SqlCommand ccm = new SqlCommand(Temp3val, conn);
                //ccm.ExecuteNonQuery();
                //conn.Close();
                //c = 0;
                //cccd = "";
                string ss = SQLCommonfile.GenerateAndSaveFile(FilenamewithExt + Convert.ToString(Cont)+".sql", Temp3val, filepath);
                cccd = "";
            }
        }
        if (Temp3val.Length > 0)
        {
            Cont++;
            //if (conn.State.ToString() != "Open")
            //{
            //    conn.Open();
            //}
            //SqlCommand ccm = new SqlCommand(Temp3val, conn);
            //ccm.ExecuteNonQuery();
            //conn.Close();
            string ss = SQLCommonfile.GenerateAndSaveFile(FilenamewithExt + Convert.ToString(Cont)+".sql", Temp3val, filepath);
        }
    }

    protected void static_FullTable(string tanlename, string lbl_TableId)
    {
        encstr = ServerWizard.ServerEncrDecriKEY(ddlserver.SelectedValue);
        string PKname = "";
        string Temp2 = " INSERT INTO " + tanlename + "(  ";
        string Temp3val = "";
        DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        for (int k = 0; k < dts1.Rows.Count; k++)
        {
            if (k == 0)
            {
                // PKname = dts1.Rows[k]["column_name"].ToString();
            }
            Temp2 += ("" + dts1.Rows[k]["column_name"] + " ,");
        }
        Temp2 = Temp2.Remove(Temp2.Length - 1);
        Temp2 += ") VAlues";
        string InsertInto = Temp2;
        string AfterVAlues = "";
        DataTable maxiddesid = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        string QueryName = "";
        DataTable dtr;
        DataTable DtWhere = MyCommonfile.selectBZ(" Select * FROM SatelliteSyncronisationrequiringTablesMaster_SerWhere Where TableId='" + lbl_TableId + "'");
        if (DtWhere.Rows.Count > 0)
        {
            //Select_Query                        
            string PKTableName = DtWhere.Rows[0]["PKTableName"].ToString();
            string PKIdName = DtWhere.Rows[0]["PKIdName"].ToString();
            string Select_Query = DtWhere.Rows[0]["Select_Query"].ToString();
            string WhereForPKID = "";
            // WhereForPKID = " Where " + PKTableName + "." + PKIdName + "=" + RecordID;
            string SelectWhere2 = " and PricePlanMaster.PricePlanId IN ( Select PricePlanId From  CompanyMaster Where active=1 and ServerId=" + ddlserver.SelectedValue + ") ";
            Select_Query = Select_Query + WhereForPKID + SelectWhere2;
            dtr = MyCommonfile.selectBZ("" + Select_Query + " ");
        }
        else
        {
            dtr = MyCommonfile.selectBZ(" Select * From " + tanlename + " " + QueryName + "");
        }
        try
        {
            //DataTable dtrcount = MyCommonfile.selectBZ(" Select Count(" + PKname + ") as PKname From " + tanlename + " " + QueryName + "");
            //string ss = TableRelated.AAAAAAA_Record(tanlename, dtrcount.Rows[0]["PKname"].ToString(), ddlserver.SelectedValue);
        }
        catch
        {
        }
        int c = 0;
        int Cont = 0;
        string cccd = "";

        string FilenamewithExt = "";
        string filepath = Convert.ToString(Server.MapPath(""));
        foreach (DataRow itm in dtr.Rows)
        {
            FilenamewithExt = tanlename;
            c++;
            cccd = InsertInto + " (";
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
            if (Temp3val.Length > 0 && c == 200)
            {
                Cont++;
                //if (conn.State.ToString() != "Open")
                //{
                //    conn.Open();
                //}
                //SqlCommand ccm = new SqlCommand(Temp3val, conn);
                //ccm.ExecuteNonQuery();
                //conn.Close();
                //c = 0;
                //cccd = "";
                string ss = SQLCommonfile.GenerateAndSaveFile(FilenamewithExt + Convert.ToString(Cont) + ".sql", Temp3val, filepath);
                cccd = "";
            }
        }
        if (Temp3val.Length > 0)
        {
            Cont++;
            //if (conn.State.ToString() != "Open")
            //{
            //    conn.Open();
            //}
            //SqlCommand ccm = new SqlCommand(Temp3val, conn);
            //ccm.ExecuteNonQuery();
            //conn.Close();
            string ss = SQLCommonfile.GenerateAndSaveFile(FilenamewithExt + Convert.ToString(Cont) + ".sql", Temp3val, filepath);
        }
    }
}
