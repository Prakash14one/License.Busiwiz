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
        if (!IsPostBack)
        {
            if (Request.QueryString["sid"] != null)
            {               
                //----------------------------------------------------------------------------------------------------------   
                string sid = BZ_Common.BZ_Decrypted(Request.QueryString["sid"].ToString()); //kdQMwcj0lE8=              "5"; //
                ViewState["sid"] = sid;     
                //-----------------------------------------------------------------------------------------------------------
                Int64 Count = 0;
                DataTable ds = MyCommonfile.selectBZ(" Select * From Sync_Need_Logs  Order By LogId ");
                for (int iicouts = 0; iicouts < ds.Rows.Count; iicouts++)
                {
                    Count++;
                    DataTable DTServerID = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.ServerMasterTbl.Id FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id Where  dbo.ServerMasterTbl.Status=1 and dbo.CompanyMaster.Active=1 ");
                    for (int iicout = 0; iicout < DTServerID.Rows.Count; iicout++)
                    {
                        Insert_Sync_Need_Logs_AtServer_AddDelUpdtSelect(ds.Rows[iicouts]["LogId"].ToString(), ds.Rows[iicouts]["RecordID"].ToString(), ds.Rows[iicouts]["ACTION"].ToString(), ds.Rows[iicouts]["TableName"].ToString(), false, DTServerID.Rows[iicout]["Id"].ToString());
                    }
                    DELETE__Sync_Need_Logs__AddDelUpdtSelect(ds.Rows[iicouts]["LogId"].ToString());                     
                }
                lblmsg.Text = Convert.ToString(Count) + " total record updated at Sync_Need_Logs_AtServer";
                TransferAtJbTable(ViewState["sid"].ToString());              
            }
        }
    }
    protected void TransferAtJbTable(string SId)
    {
        Int64 sateserver = 0;
        Int64 tablename = 0;
        Int64 recordid = 0;
        DataTable DTServerID = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.ServerMasterTbl.Id,dbo.ServerMasterTbl.ServerName FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id Where  dbo.ServerMasterTbl.Status=1 and dbo.CompanyMaster.Active=1 and dbo.ServerMasterTbl.Id="+SId+" ");//
        for (int iicout = 0; iicout < DTServerID.Rows.Count; iicout++)
        {
            sateserver++;

            Int64 JobID = Insert___Satelitte_Server_Sync_Job_Master(DTServerID.Rows[iicout]["id"].ToString(), "Updation " + DTServerID.Rows[iicout]["ServerName"].ToString() + " on " + Convert.ToString(DateTime.Now), DateTime.Now, false);
            tablename = 0;

            DataTable dsfst = MyCommonfile.selectBZ(" Select DISTINCT TAbleId From Sync_Need_Logs_AtServer where sid='" + DTServerID.Rows[iicout]["id"].ToString() + "' Order By TAbleId ");
            for (int ii = 0; ii < dsfst.Rows.Count; ii++)
            {
                tablename++;
                Int64 JobDetailID = Insert___Satellite_Server_Sync_Job_Details(Convert.ToString(JobID), dsfst.Rows[ii]["TAbleId"].ToString(), false, false, DateTime.Now, true);
            }

            recordid = 0;
            DataTable dstbl = MyCommonfile.selectBZ(" Select DISTINCT TableID,ID From Satellite_Server_Sync_Job_Details where Satelitte_Server_Sync_Job_Master_ID='" + JobID + "' Order By TableID  ");
            for (int ii = 0; ii < dstbl.Rows.Count; ii++)
            {
                DataTable ds = MyCommonfile.selectBZ(" Select * From Sync_Need_Logs_AtServer where sid='" + DTServerID.Rows[iicout]["id"].ToString() + "' and TAbleId='" + dstbl.Rows[ii]["TableID"].ToString() + "' Order By LogId  ");
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
                   // Int64 ReturnID = Insert_Satallite_Server_Sync_RecordsDetailTbl(Convert.ToString(Satallite_Server_Sync_RecordsMasterTblID), Rcordid, DateTime.Now, action, action);
                    Int64 ReturnID2 = Insert___Satelite_Server_Sync_Log_Deatils(dstbl.Rows[ii]["ID"].ToString(), Rcordid, DateTime.Now, action, "", false);

                    DELETE__Sync_Need_Logs_AtServer_AddDelUpdtSelect(ID);
                }
                Boolean status = Update___Satellite_Server_Sync_Job_Details(dstbl.Rows[ii]["ID"].ToString(), false, true, DateTime.Now, true, DateTime.Now);
            }
            Boolean status2 = Update___Satelitte_Server_Sync_Job_Masters(Convert.ToString(JobID), true, DateTime.Now);
        }

        lblmsg1.Text = "In " + Convert.ToString(sateserver) + " server " + Convert.ToString(tablename) + " table's  " + recordid + " records updated for job ";     
          
      
    }

    //protected void TransferAtJbTable()
    //{
    //    DataTable DTServerID = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.ServerMasterTbl.Id,dbo.ServerMasterTbl.ServerName FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id Where  dbo.ServerMasterTbl.Status=1 and dbo.CompanyMaster.Active=1 ");//and dbo.ServerMasterTbl.Id=5
    //    for (int iicout = 0; iicout < DTServerID.Rows.Count; iicout++)
    //    {
    //        DataTable dsfst = MyCommonfile.selectBZ(" Select DISTINCT TAbleId From Sync_Need_Logs_AtServer where sid='" + DTServerID.Rows[iicout]["id"].ToString() + "' Order By LogId ");

    //        DataTable ds = MyCommonfile.selectBZ(" Select * From Sync_Need_Logs_AtServer where sid='" + DTServerID.Rows[iicout]["id"].ToString() + "' Order By LogId  ");//Where LogId > " + DTmaxtable.Rows[0]["LogId"].ToString() + "
    //        if (ds.Rows.Count > 0)
    //        {
    //            Int64 JobID = Insert_Satelitte_Server_Sync_Job_Master(DTServerID.Rows[iicout]["id"].ToString(), "Updation " + DTServerID.Rows[iicout]["ServerName"].ToString() + " on " + Convert.ToString(DateTime.Now), DateTime.Now, false);

    //            // ViewState["JobID"] = JobID; 
    //            for (int ii = 0; ii < ds.Rows.Count; ii++)
    //            {
    //                string LogId = ds.Rows[ii]["LogId"].ToString();
    //                string Rcordid = ds.Rows[ii]["Rcordid"].ToString();
    //                string TableId = ds.Rows[ii]["TAbleId"].ToString();

    //                Int64 JobDetailID = Insert_Satellite_Server_Sync_Job_Details(Convert.ToString(JobID), TableId, false, false, DateTime.Now, true);

    //                Int64 Satallite_Server_Sync_RecordsMasterTblID = Insert_Satallite_Server_Sync_RecordsMasterTbl(DTServerID.Rows[iicout]["id"].ToString(), TableId, DateTime.Now);
    //                string action = "1";
    //                if (ds.Rows[ii]["ACTION"].ToString() == "INSERT")
    //                {
    //                    action = "1";
    //                }
    //                else if (ds.Rows[ii]["ACTION"].ToString() == "Updated")
    //                {
    //                    action = "2";
    //                }
    //                else if (ds.Rows[ii]["ACTION"].ToString() == "Deleted")
    //                {
    //                    action = "3";
    //                }
    //                Int64 ReturnID = Insert_Satallite_Server_Sync_RecordsDetailTbl(Convert.ToString(Satallite_Server_Sync_RecordsMasterTblID), Rcordid, DateTime.Now, action, action);
    //                Int64 ReturnID2 = Insert_Satelite_Server_Sync_Log_Deatils(Convert.ToString(JobDetailID), Rcordid, DateTime.Now, action, action, false);

    //                Boolean status = Update_Satellite_Server_Sync_Job_Details(Convert.ToString(JobDetailID), false, true, DateTime.Now, true, DateTime.Now);

    //                DELETE__Sync_Need_Logs_AtServer_AddDelUpdtSelect(LogId);
    //            }
    //            Boolean status2 = Update_Satelitte_Server_Sync_Job_Masters(Convert.ToString(JobID), true, DateTime.Now);
    //        }
    //    }

    //}

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
    public Int64 Insert_Sync_Need_Logs_AtServer_AddDelUpdtSelect(string LogId, string Rcordid, string ACTION, string TAbleId, Boolean IsRecordTransfer, string sid)
    {
        Int64 ReturnID = 0;
        try
        {
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
        }
        catch
        {
            ReturnID = 0;
            con.Close();
        }
        return ReturnID;
    }
    public Boolean DELETE__Sync_Need_Logs_AtServer_AddDelUpdtSelect(string ID)
    {
        Boolean ReturnID = true;
        try
        {
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
        }
        catch
        {
            ReturnID = false;
            con.Close();
        }
        return ReturnID;
    }
    //-----------------------------------------------------------------------------------
  



   
    protected void DeleteTableRecord(string tanlename, string recordid)
    {
        string Temp2 = " Delete From " + tanlename + " Where";
        string Temp3val = "";
        DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        if (dts1.Rows.Count > 0)
        {
            Temp2 += ("" + dts1.Rows[0]["column_name"] + "="+Encrypted(recordid));
        }       
        try
        {
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
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
        Temp2 += ") VAlues";

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
                    conn.Open();
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
            conn.Open();
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
                conn.Open();
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
            conn.Open();
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
                conn.Open();
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
            conn.Open();
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
    public Int64 Insert___Satelitte_Server_Sync_Job_Master(string SatelliteServerID, string SyncJobName, DateTime JobDateTime, Boolean JobFinishStatus)
    {
        Int64 ReturnID = 0;
        try
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Satelitte_Server_Sync_Job_Master_AddDelUpdtSelect",  con);
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
        }
        catch
        {
            con.Close();
            ReturnID = 0;
        }
        return ReturnID;
    }
    public Boolean Update___Satelitte_Server_Sync_Job_Masters(string Satelitte_Server_Sync_Job_Master, Boolean JobFinishStatus, DateTime FinishDatetime)
    {
        Boolean Status = false;
        try
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Satelitte_Server_Sync_Job_Master_AddDelUpdtSelect",  con);
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
            con.Close();
            Status = false;
        }

        return Status;
    }

    //Satellite_Server_Sync_Job_Details
    public Int64 Insert___Satellite_Server_Sync_Job_Details(string Satelitte_Server_Sync_Job_Master_ID, string TableID, Boolean SyncedStatus, Boolean CheckingStatus, DateTime CheckedDateTime, Boolean NeedTocreateTblatSatServer)
    {
        Int64 ReturnID = 0;
        try
        {
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
        }
        catch
        {
            con.Close();
            ReturnID = 0;
        }
        return ReturnID;
    }
    public Boolean Update___Satellite_Server_Sync_Job_Details(string Satellite_Server_Sync_Job_DetailsID, Boolean SyncedStatus, Boolean CheckingStatus, DateTime CheckedDateTime, Boolean JobFinishFinishStatus, DateTime JobDetailDoneDatandtime)
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
            cmd.Parameters.AddWithValue("@StatementType", "UpdateFinish");

            cmd.Parameters.AddWithValue("@ID", Satellite_Server_Sync_Job_DetailsID);
            cmd.Parameters.AddWithValue("@SyncedStatus", SyncedStatus);
            cmd.Parameters.AddWithValue("@CheckingStatus", CheckingStatus);
            cmd.Parameters.AddWithValue("@CheckedDateTime", CheckedDateTime);
            cmd.Parameters.AddWithValue("@JobFinishFinishStatus", JobFinishFinishStatus);
            cmd.Parameters.AddWithValue("@JobDetailDoneDatandtime", JobDetailDoneDatandtime);
            cmd.ExecuteNonQuery();
            con.Close();
            Status = true;
        }
        catch
        {
            con.Close();
            Status = false;
        }

        return Status;
    }
    public Boolean Delete___Satellite_Server_Sync_Job_Details(string Satellite_Server_Sync_Job_DetailsID)
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
    public Int64 Insert___Satelite_Server_Sync_Log_Deatils(string Satellite_Server_Sync_Job_Details_ID, string RecordID, DateTime Dateandtime, string TypeOfOperationDone, string TyepeOfOperationReqd, Boolean SyncedStatus)
    {
        Int64 ReturnID = 0;
        try
        {
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
        }
        catch
        {
            ReturnID = 0;
            con.Close();
        }
        return ReturnID;
    }
    public Boolean Delete___Satelite_Server_Sync_Log_Deatils(string Satellite_Server_Sync_Job_Details_ID)
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

    public Boolean Delete___Only_ID_Satelite_Server_Sync_Log_Deatils(string ID)
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

    //Satallite_Server_Sync_RecordsMasterTbl
    public Int64 Insert___Satallite_Server_Sync_RecordsMasterTbl(string TableID, string ServerID, DateTime LastSynDateTime)
    {
        Int64 ReturnID = 0;
        try
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Satallite_Server_Sync_RecordsMasterTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            cmd.Parameters.AddWithValue("@TableId", TableID);
            cmd.Parameters.AddWithValue("@ServerID", ServerID);
            cmd.Parameters.AddWithValue("@LastSynDateTime", LastSynDateTime);
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
 public Boolean Delete___Satallite_Server_Sync_RecordsMasterTbl(string TableID, string ServerID)
    {
        Boolean ReturnID = true;
        try
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Satallite_Server_Sync_RecordsMasterTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@TableId", TableID);
            cmd.Parameters.AddWithValue("@ServerID", ServerID);
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
   
   

  
}
