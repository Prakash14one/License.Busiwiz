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
    public static double size = 0;
    int StepId = 1;
    string allstring = "";   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Timer1.Enabled = false;
            img_loading.Visible = false;
            if (Request.QueryString["sid"] != null && Request.QueryString["addserjob"] == "1")
            {
                //----------------------------------------------------------------------------------------------------------   
                string sid =Request.QueryString["sid"].Replace(" ", "+"); //kdQMwcj0lE8= "5"; //
                sid = BZ_Common.BZ_Decrypted(sid);
                ViewState["sid"] = sid;
                DataTable ds = MyCommonfile.selectBZ(" Select * From Sync_Need_Logs  Order By LogId ");               
                if (ds.Rows.Count > 0 )
                {
                    Timer1.Enabled = true;
                    img_loading.Visible = true;
                }
                else
                {
                    lbl_Msg.Text = "No any record available for syncronice";
                }
                //-----------------------------------------------------------------------------------------------------------               
            }


            if (Request.QueryString["sid"] != null && Request.QueryString["addjob123"] == "1")
            {
                string sid = Request.QueryString["sid"].Replace(" ", "+"); //kdQMwcj0lE8= "5"; //
                sid = BZ_Common.BZ_Decrypted(sid);
                ViewState["sid"] = sid;                
                DataTable ds1 = MyCommonfile.selectBZ(" Select * From Sync_Need_Logs_AtServer where sid='" + ViewState["sid"].ToString() + "'  Order By LogId  ");
                if (ds1.Rows.Count > 0)
                {
                    TransferAtJob1Job2(sid);
                    Timer1.Enabled = true;
                    img_loading.Visible = true;
                }
                else
                {
                    lbl_Msg.Text = "No any record available for create job for syncronice ";
                }
            }
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Request.QueryString["sid"] != null && Request.QueryString["addserjob"] == "1")
        {
            CreateServerJob();
        }
        if (Request.QueryString["sid"] != null && Request.QueryString["addjob123"] == "1")
        {            
            CreateJob3(ViewState["sid"].ToString());
        }       
    }
    protected void CreateServerJob()
    {
        Int64 Count = 0;
        DataTable ds = MyCommonfile.selectBZ(" Select TOP(50)* From Sync_Need_Logs  Order By LogId ");
        for (int iicouts = 0; iicouts < ds.Rows.Count; iicouts++)
        {
            DataTable DTServerID = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.ServerMasterTbl.Id FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id Where  dbo.ServerMasterTbl.Status=1 and dbo.CompanyMaster.Active=1 ");
            for (int iicout = 0; iicout < DTServerID.Rows.Count; iicout++)
            {
                string RecordID = ds.Rows[iicouts]["RecordID"].ToString();
                DataTable DtWhere = MyCommonfile.selectBZ(" Select * FROM SatelliteSyncronisationrequiringTablesMaster_SerWhere Where TableId='" + ds.Rows[iicouts]["TableName"].ToString() + "'");
                if (DtWhere.Rows.Count > 0)
                {
                    //Select_Query                        
                    string PKTableName = DtWhere.Rows[0]["PKTableName"].ToString();
                    string PKIdName = DtWhere.Rows[0]["PKIdName"].ToString();
                    string Select_Query = DtWhere.Rows[0]["Select_Query"].ToString();
                    string WhereForPKID = " Where " + PKTableName + "." + PKIdName + "=" + RecordID;
                    string SelectWhere2 = " and PricePlanMaster.PricePlanId IN ( Select PricePlanId From  CompanyMaster Where active=1 and ServerId=" + ViewState["sid"] + ") ";
                    Select_Query = Select_Query + WhereForPKID + SelectWhere2;
                    DataTable DtWhereC = MyCommonfile.selectBZ("" + Select_Query + "");
                    if (DtWhereC.Rows.Count > 0)
                    {
                        Count++;
                        Syncro_Tables.Insert___Sync_Need_Logs_AtServer(ds.Rows[iicouts]["LogId"].ToString(), ds.Rows[iicouts]["RecordID"].ToString(), ds.Rows[iicouts]["ACTION"].ToString(), ds.Rows[iicouts]["TableName"].ToString(), false, DTServerID.Rows[iicout]["Id"].ToString());
                    }
                }
                else
                {
                    Syncro_Tables.Insert___Sync_Need_Logs_AtServer(ds.Rows[iicouts]["LogId"].ToString(), ds.Rows[iicouts]["RecordID"].ToString(), ds.Rows[iicouts]["ACTION"].ToString(), ds.Rows[iicouts]["TableName"].ToString(), false, DTServerID.Rows[iicout]["Id"].ToString());
                }
                //SELECT DISTINCT dbo.ClientProductTableMaster.Id FROM dbo.PricePlanMaster INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID INNER JOIN dbo.PriceplanrestrictionTbl INNER JOIN dbo.PortalMasterTbl ON dbo.PriceplanrestrictionTbl.PortalId = dbo.PortalMasterTbl.Id ON dbo.Priceplancategory.PortalId = dbo.PortalMasterTbl.Id LEFT OUTER JOIN dbo.ClientProductTableMaster ON dbo.PriceplanrestrictionTbl.TableId = dbo.ClientProductTableMaster.Id  Where  dbo.ClientProductTableMaster.Id='"+ RecordID +"' and PricePlanMaster.PricePlanId IN(Select PricePlanId From  CompanyMaster Where active=1 and ServerId='"+ViewState["sid"]+"')                        
            }
            Syncro_Tables.DELETE__Sync_Need_Logs__AddDelUpdtSelect(ds.Rows[iicouts]["LogId"].ToString());
        }
        DataTable ds1 = MyCommonfile.selectBZ(" Select * From Sync_Need_Logs  Order By LogId ");
        if (ds1.Rows.Count == 0)
        {
            Timer1.Enabled = false;
            img_loading.Visible = false;
        }
        lbl_Msg.Text = Convert.ToString(Count) + " total record updated at Sync_Need_Logs_AtServer table";
    }
    protected void btnRefresh1_OnClick(object sender, EventArgs e)
    {
        lbl_Msg.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff");
    }


    protected void TransferAtJob1Job2(string SId)
    {
        Int64 tablename = 0;
        Int64 JobID = Syncro_Tables.InsertJob1___Satelitte_Server_Sync_Job_Master(SId, "Updation " + SId + " on " + Convert.ToString(DateTime.Now), DateTime.Now, false);
        tablename = 0;
        DataTable dsfst = MyCommonfile.selectBZ(" Select DISTINCT TAbleId From Sync_Need_Logs_AtServer where sid='" + SId + "' Order By TAbleId ");
        for (int ii = 0; ii < dsfst.Rows.Count; ii++)
        {
            tablename++;
            Int64 JobDetailID = Syncro_Tables.InsertJob2___Satellite_Server_Sync_Job_Details(Convert.ToString(JobID), dsfst.Rows[ii]["TAbleId"].ToString(), false, false, DateTime.Now, true);
        }
        lbl_Msg.Text = Convert.ToString(tablename) + " tables added for create job ";
    }
    protected void CreateJob3(string SId)
    {
        Int64 recordid = 0;
        recordid = 0;
        DataTable ds2 = MyCommonfile.selectBZ(" Select TOP(1)* From Sync_Need_Logs_AtServer where sid='" + SId + "' Order By LogId  ");
        if (ds2.Rows.Count > 0)
        {
            DataTable dstbl = MyCommonfile.selectBZ(" SELECT DISTINCT TOP(1) dbo.Satellite_Server_Sync_Job_Details.TableID, dbo.Satellite_Server_Sync_Job_Details.ID, Satelitte_Server_Sync_Job_Master.Id  FROM            dbo.Satellite_Server_Sync_Job_Details INNER JOIN dbo.Satelitte_Server_Sync_Job_Master ON dbo.Satellite_Server_Sync_Job_Details.Satelitte_Server_Sync_Job_Master_ID = dbo.Satelitte_Server_Sync_Job_Master.Id where Satelitte_Server_Sync_Job_Master.SatelliteServerID='" + SId + "' and Satellite_Server_Sync_Job_Details.TableID='" + ds2.Rows[0]["TableID"].ToString() + "'  ");
            for (int ii = 0; ii < dstbl.Rows.Count; ii++)
            {
                DataTable ds = MyCommonfile.selectBZ(" Select TOP(100)* From Sync_Need_Logs_AtServer where sid='" + SId + "' and TAbleId='" + dstbl.Rows[ii]["TableID"].ToString() + "' Order By LogId  ");
                for (int iii = 0; iii < ds.Rows.Count; iii++)
                {
                    recordid++;
                    string ID = ds.Rows[iii]["ID"].ToString();
                    string LogId = ds.Rows[iii]["LogId"].ToString();
                    string Rcordid = ds.Rows[iii]["Rcordid"].ToString();
                    string TableId = ds.Rows[iii]["TAbleId"].ToString();
                    // Int64 Satallite_Server_Sync_RecordsMasterTblID = Insert___Satallite_Server_Sync_RecordsMasterTbl(DTServerID.Rows[iicout]["id"].ToString(), TableId, DateTime.Now);
                    string action = "1";
                    if (ds.Rows[iii]["ACTION"].ToString() == "INSERT")
                    {
                        action = "1";
                    }
                    else if (ds.Rows[iii]["ACTION"].ToString() == "Updated")
                    {
                        action = "2";
                    }
                    else if (ds.Rows[iii]["ACTION"].ToString() == "Deleted")
                    {
                        action = "3";
                    }
                    Int64 Job3DID = Syncro_Tables.InsertJob3___Satelite_Server_Sync_Log_Deatils(dstbl.Rows[ii]["ID"].ToString(), Rcordid, DateTime.Now, action, "", false);
                    Syncro_Tables.DELETE___Sync_Need_Logs_AtServer(ID);
                }
                // Boolean status = Syncro_Tables.DeleteJob2____Satellite_Server_Sync_Job_Details(dstbl.Rows[ii]["ID"].ToString());
                // Boolean status2 = Syncro_Tables.UpdateJob1___Satelitte_Server_Sync_Job_Master(Convert.ToString(dstbl.Rows[ii]["Id"].ToString()), true, DateTime.Now);                
            }
        }
        DataTable ds1 = MyCommonfile.selectBZ(" Select TOP(1)* From Sync_Need_Logs_AtServer where sid='" + SId + "' ");
        if (ds1.Rows.Count == 0)
        {
            Timer1.Enabled = false;
            img_loading.Visible = false;
        }       
    }
  
   













   

  
}
