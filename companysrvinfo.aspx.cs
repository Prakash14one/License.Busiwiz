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
using System.Text;
using System.Net;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;
public partial class busiwizlicensekeygeneration : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlConnection connCompserver = new SqlConnection();
    public StringBuilder strplan1 = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {

        string ss = BZ_Common.BZ_Decrypted("d2fu/WL5XmM=");
        string ssdd = BZ_Common.BZ_Decrypted("member");
        string ssddfdf = BZ_Common.BZ_Encrypted("member"); 

        //ABC Key Insert At Server DB
        if (Request.QueryString["comid"] != null && Request.QueryString["ABCenckey"] != null && Request.QueryString["returl"] != null)
        {
            //string hh = BZ_Common.BZ_Decrypted(Request.QueryString["comid"].ToString().Replace(" ", "+"));
            string companyid = BZ_Common.BZ_Decrypted(Request.QueryString["comid"].ToString().Replace(" ", "+"));          

            DataTable DtServid = MyCommonfile.selectBZ(" Select ServerMasterTbl.Busiwizsatellitesiteurl,CompanyMaster.CompanyLoginId,ServerMasterTbl.Id FROM dbo.ServerMasterTbl INNER JOIN dbo.CompanyMaster ON dbo.ServerMasterTbl.Id = dbo.CompanyMaster.ServerId Where CompanyMaster.CompanyLoginId='" + companyid + "' ");
            if (DtServid.Rows.Count > 0)
            {
                string Busiwizsatellitesiteurl = DtServid.Rows[0]["Busiwizsatellitesiteurl"].ToString();
                string ServerId = DtServid.Rows[0]["Id"].ToString();
                Boolean Comp__Actve__Status = CompanyWizard.Company_Active_Status(companyid);
                Boolean Comp__Licen__Active = CompanyWizard.Company_LicenseExpire_Status(companyid);
                Boolean Server__Active = ServerWizard.Server_Active_Status(companyid);
                if (Comp__Actve__Status == true && Comp__Licen__Active == true && Server__Active == true)
                {
                    Boolean Insert_Today_Key = CompKeyIns.BtnABCKey(companyid, ServerId);
                    if (Insert_Today_Key == true)
                    {
                        string Z = "";
                        string D1 = "";
                        string D2 = "";
                        string D3 = "";
                        string D4 = "";
                        string D5 = "";

                        string E1 = "";
                        string E2 = "";
                        string E3 = "";
                        string E4 = "";
                        string E5 = "";

                        string F1 = "";
                        string F2 = "";
                        string F3 = "";
                        string F4 = "";
                        string F5 = "";
                        DataTable Dt = MyCommonfile.selectBZ(" Select TOP(15)* From CompanyABCDetail Where CompanyLoginId='" + companyid + "' Order By Id Desc ");
                        foreach (DataRow drmaxdb in Dt.Rows)
                        {
                            Z += drmaxdb["Z"].ToString() + ",";

                            D1 += drmaxdb["D1"].ToString() + ",";
                            D2 += drmaxdb["D2"].ToString() + ",";
                            D3 += drmaxdb["D3"].ToString() + ",";
                            D4 += drmaxdb["D4"].ToString() + ",";
                            D5 += drmaxdb["D5"].ToString() + ",";

                            E1 += drmaxdb["E1"].ToString() + ",";
                            E2 += drmaxdb["E2"].ToString() + ",";
                            E3 += drmaxdb["E3"].ToString() + ",";
                            E4 += drmaxdb["E4"].ToString() + ",";
                            E5 += drmaxdb["E5"].ToString() + ",";

                            F1 += drmaxdb["F1"].ToString() + ",";
                            F2 += drmaxdb["F2"].ToString() + ",";
                            F3 += drmaxdb["F3"].ToString() + ",";
                            F4 += drmaxdb["F4"].ToString() + ",";
                            F5 += drmaxdb["F5"].ToString() + ",";
                        }
                        if (Dt.Rows.Count > 0)
                        {
                            Z = Z.Remove(Z.Length - 1);

                            D1 = D1.Remove(D1.Length - 1);
                            D2 = D2.Remove(D2.Length - 1);
                            D3 = D3.Remove(D3.Length - 1);
                            D4 = D4.Remove(D4.Length - 1);
                            D5 = D5.Remove(D5.Length - 1);

                            E1 = E1.Remove(E1.Length - 1);
                            E2 = E2.Remove(E2.Length - 1);
                            E3 = E3.Remove(E3.Length - 1);
                            E4 = E4.Remove(E4.Length - 1);
                            E5 = E5.Remove(E5.Length - 1);

                            F1 = F1.Remove(F1.Length - 1);
                            F2 = F2.Remove(F2.Length - 1);
                            F3 = F3.Remove(F3.Length - 1);
                            F4 = F4.Remove(F4.Length - 1);
                            F5 = F5.Remove(F5.Length - 1);

                            //Response.Redirect("http://" + Busiwizsatellitesiteurl + "/Satelliteservfunction.aspx?Compid=" + BZ_Common.BZ_Encrypted(companyid) + "&E1=" + BZ_Common.BZ_Encrypted(E1) + "&E2=" + BZ_Common.BZ_Encrypted(E2) + "&E3=" + BZ_Common.BZ_Encrypted(E3) + "&E4=" + BZ_Common.BZ_Encrypted(E4) + "&E5=" + BZ_Common.BZ_Encrypted(E5) + "&D1=" + BZ_Common.BZ_Encrypted(D1) + "&D2=" + BZ_Common.BZ_Encrypted(D2) + "&D3=" + BZ_Common.BZ_Encrypted(D3) + "&D4=" + BZ_Common.BZ_Encrypted(D4) + "&D5=" + BZ_Common.BZ_Encrypted(D5) + "&Z=" + BZ_Common.BZ_Encrypted(Z) + "&F1=" + BZ_Common.BZ_Encrypted(F1) + "&F2=" + BZ_Common.BZ_Encrypted(F2) + "&F3=" + BZ_Common.BZ_Encrypted(F3) + "&F4=" + BZ_Common.BZ_Encrypted(F4) + "&F5=" + BZ_Common.BZ_Encrypted(F5) + "");
                            Response.Redirect("http://" + Busiwizsatellitesiteurl + "/Satelliteservfunction.aspx?Compid=" + BZ_Common.BZ_Encrypted(companyid) + "&E1=" + E1 + "&E2=" + E2 + "&E3=" + E3 + "&E4=" + E4 + "&E5=" + E5 + "&D1=" + D1 + "&D2=" + D2 + "&D3=" + D3 + "&D4=" + D4 + "&D5=" + D5 + "&Z=" + Z + "&F1=" + F1 + "&F2=" + F2 + "&F3=" + F3 + "&F4=" + F4 + "&F5=" + F5 + "&returl="+Request.QueryString["returl"]+"");
                        }
                        lbl_msg.Text = "Successfully ";
                    }
                    else
                    {
                        lbl_msg.Text = "Some problem when we try to adding records in database ";
                    }
                }
                else if (Comp__Actve__Status == false)
                {
                    lbl_msg.Text = "Company not active ";
                }
                else if (Comp__Licen__Active == false)
                {
                    lbl_msg.Text = "License Expired";
                }
                else if (Server__Active == false)
                {
                    lbl_msg.Text = "Server Inactive";
                }
            }
        }
    }
    

    //public static Boolean BtnABCKey(string CompanyLoginId, string ServerId)
    //{
    //    Boolean Status = true;
    //    Int64 X = 0;
    //    Int64 Y = 0;
    //    Int64 Z = 0;      

    //    Int64 C1 = 0;
    //    Int64 C2 = 0;
    //    Int64 C3 = 0;
    //    Int64 C4 = 0;
    //    Int64 C5 = 0;      
      
    //    DataTable dt_getCompanyABCMaster = MyCommonfile.selectBZ(" Select * From CompanyABCMaster where CompanyLoginId='" + CompanyLoginId + "' ");
    //    if (dt_getCompanyABCMaster.Rows.Count > 0)
    //    {
    //        Boolean Del_CABCD = CompKeyIns.Delete_CompanyABCDetail(CompanyLoginId);
    //        Boolean Del_CABCD_Server = CompKeyIns.Delete_CompanyABCDetail_Server(CompanyLoginId);
    //        if (Del_CABCD_Server == true && Del_CABCD_Server == true)
    //        {

    //            string txt_c = dt_getCompanyABCMaster.Rows[0]["C"].ToString();

    //            string txt_c1 = dt_getCompanyABCMaster.Rows[0]["C1"].ToString();
    //            string txt_c2 = dt_getCompanyABCMaster.Rows[0]["C2"].ToString();
    //            string txt_c3 = dt_getCompanyABCMaster.Rows[0]["C3"].ToString();
    //            string txt_c4 = dt_getCompanyABCMaster.Rows[0]["C4"].ToString();
    //            string txt_c5 = dt_getCompanyABCMaster.Rows[0]["C5"].ToString();

    //            C1 = Convert.ToInt64(txt_c1.Substring(0, 4));
    //            C2 = Convert.ToInt64(txt_c2.Substring(0, 4));
    //            C3 = Convert.ToInt64(txt_c3.Substring(0, 4));
    //            C4 = Convert.ToInt64(txt_c4.Substring(0, 4));
    //            C5 = Convert.ToInt64(txt_c5.Substring(0, 4));

    //            DateTime todaydatefull = DateTime.Now;
    //            todaydatefull = todaydatefull.AddDays(-1);
    //            string strdt = todaydatefull.ToString("MM-dd-yyyy");
    //            DataTable dtfunction = MyCommonfile.selectBZ(" Select * From FunctionMaster ");
    //            DateTime startDate = DateTime.Parse(strdt);
    //            DateTime expiryDate = startDate.AddDays(2);
    //            int DayInterval = 1;
    //            while (startDate <= expiryDate && Status==true)
    //            {
    //                Random random = new Random();
    //                int randomNumberA1 = random.Next(1, 6);
    //                int randomNumberA2 = random.Next(1, 6);
    //                int randomNumberA3 = random.Next(1, 6);
    //                int randomNumberA4 = random.Next(1, 6);
    //                int randomNumberA5 = random.Next(1, 6);

    //                Int64 F1 = Convert.ToInt64(randomNumberA1);
    //                Int64 F2 = Convert.ToInt64(randomNumberA2);
    //                Int64 F3 = Convert.ToInt64(randomNumberA3);
    //                Int64 F4 = Convert.ToInt64(randomNumberA4);
    //                Int64 F5 = Convert.ToInt64(randomNumberA5);

    //                DateTime datevalue = (Convert.ToDateTime(startDate.ToString("MM-dd-yyyy")));
    //                string DD = datevalue.Day.ToString();
    //                string MM = datevalue.Month.ToString();
    //                string YYYY = datevalue.Year.ToString();
    //                string ZDate = DD + "" + MM + "" + YYYY;

    //                X = Convert.ToInt64(DD);
    //                Y = Convert.ToInt64(MM);
    //                Z = Convert.ToInt64(YYYY);

    //                string dateid = DD + "" + MM + "" + YYYY;

    //                String A1 = CompKeyIns.C1toC5GetA(C1, F1, DD, MM, YYYY);
    //                String A2 = CompKeyIns.C1toC5GetA(C2, F2, DD, MM, YYYY);
    //                String A3 = CompKeyIns.C1toC5GetA(C3, F3, DD, MM, YYYY);
    //                String A4 = CompKeyIns.C1toC5GetA(C4, F4, DD, MM, YYYY);
    //                String A5 = CompKeyIns.C1toC5GetA(C5, F5, DD, MM, YYYY);

    //                string D1 = A1.Substring(0, 1);
    //                string E1 = A1.Substring(1, A1.Length - 1);

    //                string D2 = A2.Substring(0, 1);
    //                string E2 = A2.Substring(1, A2.Length - 1);

    //                string D3 = A3.Substring(0, 1);
    //                string E3 = A3.Substring(1, A3.Length - 1);

    //                string D4 = A4.Substring(0, 1);
    //                string E4 = A4.Substring(1, A4.Length - 1);

    //                string D5 = A5.Substring(0, 1);
    //                string E5 = A5.Substring(1, A5.Length - 1);

    //                //---------------------------------
    //                //if (C1 != Convert.ToInt64(txt_c1.Substring(0, 4)))
    //                //{
    //                //}
    //                string txt_ansc = "";
    //                //----------------------------------------------------------------------------------------------------------------               
    //                Boolean Insert_CABCD_License = CompKeyIns.Insert_CompanyABCDetail(CompanyLoginId, ZDate, A1, A2, A3, A4, A5, D1, D2, D3, D4, D5, E1, E2, E3, E4, E5, Convert.ToString(F1), Convert.ToString(F2), Convert.ToString(F3), Convert.ToString(F4), Convert.ToString(F5), Convert.ToString(C1), Convert.ToString(C2), Convert.ToString(C3), Convert.ToString(C4), Convert.ToString(C5), txt_ansc, ServerId);
    //                if (Insert_CABCD_License == true)
    //                {
    //                    Boolean Insert_CABCD_Server = CompKeyIns.Insert_CompanyABCDetail_SERVER(CompanyLoginId, ZDate, D1, D2, D3, D4, D5, E1, E2, E3, E4, E5, Convert.ToString(F1), Convert.ToString(F2), Convert.ToString(F3), Convert.ToString(F4), Convert.ToString(F5));
    //                    if (Insert_CABCD_Server == true)
    //                    {
    //                        Status = false;
    //                    }
    //                }                  
    //                startDate = startDate.AddDays(DayInterval);
    //            }
    //        }
    //        else
    //        {
    //            Status = false;
    //        }
    //    }
    //    else
    //    {
    //        Status = false;
    //    }
    //    return Status;
    //}
    //-----------------------------------------------------
  






    //New Table Insert Delete Update
    //public static Int64 Insert___Satelitte_Server_Sync_Job_Master(string SatelliteServerID, string SyncJobName, DateTime JobDateTime, Boolean JobFinishStatus)
    //{
    //    Int64 ReturnID = 0;
    //    try
    //    {
    //        SqlConnection liceco = new SqlConnection();
    //        liceco = MyCommonfile.licenseconn();
    //        if (liceco.State.ToString() != "Open")
    //        {
    //            liceco.Open();
    //        }
    //        SqlCommand cmd = new SqlCommand("Satelitte_Server_Sync_Job_Master_AddDelUpdtSelect", liceco);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@StatementType", "Insert");
    //        cmd.Parameters.AddWithValue("@SatelliteServerID", SatelliteServerID);
    //        cmd.Parameters.AddWithValue("@SyncJobName", SyncJobName);
    //        cmd.Parameters.AddWithValue("@JobDateTime", JobDateTime);
    //        cmd.Parameters.AddWithValue("@JobFinishStatus", JobFinishStatus);
    //        object maxprocID = new object();
    //        maxprocID = cmd.ExecuteScalar();
    //        liceco.Close();
    //        ReturnID = Convert.ToInt64(maxprocID);
    //    }
    //    catch
    //    {
    //        ReturnID = 0;
    //    }
    //    return ReturnID;
    //}

    //Satellite_Server_Sync_Job_Details
    //public static Int64 Insert___Satellite_Server_Sync_Job_Details(string Satelitte_Server_Sync_Job_Master_ID, string TableID, Boolean SyncedStatus, Boolean CheckingStatus, DateTime CheckedDateTime, Boolean NeedTocreateTblatSatServer)
    //{
    //    Int64 ReturnID = 0;
    //    try
    //    {
    //        SqlConnection liceco = new SqlConnection();
    //        liceco = MyCommonfile.licenseconn();
    //        if (liceco.State.ToString() != "Open")
    //        {
    //            liceco.Open();
    //        }
    //        SqlCommand cmd = new SqlCommand("Satellite_Server_Sync_Job_Details_AddDelUpdtSelect", liceco);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@StatementType", "Insert");
    //        cmd.Parameters.AddWithValue("@Satelitte_Server_Sync_Job_Master_ID", Satelitte_Server_Sync_Job_Master_ID);
    //        cmd.Parameters.AddWithValue("@TableID", TableID);
    //        cmd.Parameters.AddWithValue("@SyncedStatus", SyncedStatus);
    //        cmd.Parameters.AddWithValue("@CheckingStatus", CheckingStatus);
    //        cmd.Parameters.AddWithValue("@CheckedDateTime", CheckedDateTime);
    //        cmd.Parameters.AddWithValue("@NeedTocreateTblatSatServer", NeedTocreateTblatSatServer);
    //        object maxprocID = new object();
    //        maxprocID = cmd.ExecuteScalar();
    //        liceco.Close();
    //        ReturnID = Convert.ToInt64(maxprocID);
    //    }
    //    catch
    //    {
    //        ReturnID = 0;
    //    }
    //    return ReturnID;
    //}

    //Satelite_Server_Sync_Log_Deatils
    //public Int64 Insert___Satelite_Server_Sync_Log_Deatils(string Satellite_Server_Sync_Job_Details_ID, string RecordID, DateTime Dateandtime, string TypeOfOperationDone, string TyepeOfOperationReqd, Boolean SyncedStatus)
    //{
    //    Int64 ReturnID = 0;
    //    try
    //    {

    //        SqlCommand cmd = new SqlCommand("Satelite_Server_Sync_Log_Deatils_AddDelUpdtSelect", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@StatementType", "Insert");
    //        cmd.Parameters.AddWithValue("@Satellite_Server_Sync_Job_Details_ID", Satellite_Server_Sync_Job_Details_ID);
    //        cmd.Parameters.AddWithValue("@RecordID", RecordID);
    //        cmd.Parameters.AddWithValue("@Dateandtime", Dateandtime);
    //        cmd.Parameters.AddWithValue("@TypeOfOperationDone", TypeOfOperationDone);
    //        cmd.Parameters.AddWithValue("@TyepeOfOperationReqd", TyepeOfOperationReqd);
    //        cmd.Parameters.AddWithValue("@SyncedStatus", SyncedStatus);
    //        object maxprocID = new object();
    //        maxprocID = cmd.ExecuteScalar();
    //        ReturnID = Convert.ToInt64(maxprocID);
    //    }
    //    catch
    //    {
    //        ReturnID = 0;
    //    }
    //    return ReturnID;
    //}
    ////---------------------------------------------------
    //public static Boolean Update___Satellite_Server_Sync_Job_Details(string Satellite_Server_Sync_Job_DetailsID, Boolean SyncedStatus, Boolean CheckingStatus, DateTime CheckedDateTime, Boolean JobFinishFinishStatus, DateTime JobDetailDoneDatandtime)
    //{
    //    Boolean Status = false;
    //    try
    //    {
    //        SqlConnection liceco = new SqlConnection();
    //        liceco = MyCommonfile.licenseconn();
    //        if (liceco.State.ToString() != "Open")
    //        {
    //            liceco.Open();
    //        }
    //        SqlCommand cmd = new SqlCommand("Satellite_Server_Sync_Job_Details_AddDelUpdtSelect", liceco);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@StatementType", "UpdateFinish");

    //        cmd.Parameters.AddWithValue("@ID", Satellite_Server_Sync_Job_DetailsID);
    //        cmd.Parameters.AddWithValue("@SyncedStatus", SyncedStatus);
    //        cmd.Parameters.AddWithValue("@CheckingStatus", CheckingStatus);
    //        cmd.Parameters.AddWithValue("@CheckedDateTime", CheckedDateTime);
    //        cmd.Parameters.AddWithValue("@JobFinishFinishStatus", JobFinishFinishStatus);
    //        cmd.Parameters.AddWithValue("@JobDetailDoneDatandtime", JobDetailDoneDatandtime);
    //        cmd.ExecuteNonQuery();
    //        liceco.Close();
    //        Status = true;
    //    }
    //    catch
    //    {
    //        Status = false;
    //    }

    //    return Status;
    //}
    //public static Boolean Update___Satelitte_Server_Sync_Job_Masters(string Satelitte_Server_Sync_Job_Master, Boolean JobFinishStatus, DateTime FinishDatetime)
    //{
    //    Boolean Status = false;
    //    try
    //    {
    //        SqlConnection liceco = new SqlConnection();
    //        liceco = MyCommonfile.licenseconn();
    //        if (liceco.State.ToString() != "Open")
    //        {
    //            liceco.Open();
    //        }
    //        SqlCommand cmd = new SqlCommand("Satelitte_Server_Sync_Job_Master_AddDelUpdtSelect", liceco);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@StatementType", "UpdateFinish");
    //        cmd.Parameters.AddWithValue("@ID", Satelitte_Server_Sync_Job_Master);
    //        cmd.Parameters.AddWithValue("@JobFinishStatus", JobFinishStatus);
    //        cmd.Parameters.AddWithValue("@FinishDatetime", FinishDatetime);
    //        cmd.ExecuteNonQuery();
    //        liceco.Close();
    //        Status = true;
    //    }
    //    catch
    //    {
    //        Status = false;
    //    }

    //    return Status;
    //}
    //protected void TransferAtJbTable(string ServerId)
    //{
    //    Int64 sateserver = 0;
    //    Int64 tablename = 0;
    //    Int64 recordid = 0;
    //    DataTable DTServerID = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.ServerMasterTbl.Id,dbo.ServerMasterTbl.ServerName FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id Where  dbo.ServerMasterTbl.Status=1 and dbo.CompanyMaster.Active=1 and dbo.ServerMasterTbl.Id='"+ServerId+"' ");//and dbo.ServerMasterTbl.Id=5
    //    for (int iicout = 0; iicout < DTServerID.Rows.Count; iicout++)
    //    {
    //        sateserver++;

    //        Int64 JobID = Insert___Satelitte_Server_Sync_Job_Master(DTServerID.Rows[iicout]["id"].ToString(), "Updation " + DTServerID.Rows[iicout]["ServerName"].ToString() + " on " + Convert.ToString(DateTime.Now), DateTime.Now, false);
    //        tablename = 0;

    //        DataTable dsfst = MyCommonfile.selectBZ(" Select DISTINCT TAbleId From Sync_Need_Logs_AtServer where sid='" + DTServerID.Rows[iicout]["id"].ToString() + "' Order By TAbleId ");
    //        for (int ii = 0; ii < dsfst.Rows.Count; ii++)
    //        {
    //            tablename++;
    //            Int64 JobDetailID = Insert___Satellite_Server_Sync_Job_Details(Convert.ToString(JobID), dsfst.Rows[ii]["TAbleId"].ToString(), false, false, DateTime.Now, true);
    //        }

    //        recordid = 0;
    //        DataTable dstbl = MyCommonfile.selectBZ(" Select DISTINCT TableID,ID From Satellite_Server_Sync_Job_Details where Satelitte_Server_Sync_Job_Master_ID='" + JobID + "' Order By TableID  ");
    //        for (int ii = 0; ii < dstbl.Rows.Count; ii++)
    //        {
    //            DataTable ds = MyCommonfile.selectBZ(" Select * From Sync_Need_Logs_AtServer where sid='" + DTServerID.Rows[iicout]["id"].ToString() + "' and TAbleId='" + dstbl.Rows[ii]["TableID"].ToString() + "' Order By LogId  ");
    //            for (int iii = 0; iii < ds.Rows.Count; iii++)
    //            {
    //                recordid++;
    //                string ID = ds.Rows[iii]["ID"].ToString();
    //                string LogId = ds.Rows[iii]["LogId"].ToString();
    //                string Rcordid = ds.Rows[iii]["Rcordid"].ToString();
    //                string TableId = ds.Rows[iii]["TAbleId"].ToString();

    //                // Int64 Satallite_Server_Sync_RecordsMasterTblID = Insert___Satallite_Server_Sync_RecordsMasterTbl(DTServerID.Rows[iicout]["id"].ToString(), TableId, DateTime.Now);
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
    //                Int64 ReturnID2 = Insert___Satelite_Server_Sync_Log_Deatils(dstbl.Rows[ii]["ID"].ToString(), Rcordid, DateTime.Now, action, "", false);

    //                DELETE__Sync_Need_Logs_AtServer_AddDelUpdtSelect(ID);
    //            }
    //            Boolean status = Update___Satellite_Server_Sync_Job_Details(dstbl.Rows[ii]["ID"].ToString(), false, true, DateTime.Now, true, DateTime.Now);
    //        }
    //        Boolean status2 = Update___Satelitte_Server_Sync_Job_Masters(Convert.ToString(JobID), true, DateTime.Now);
    //    }

    //    lbl_msg.Text = "In " + Convert.ToString(sateserver) + " server " + Convert.ToString(tablename) + " table's  " + recordid + " records updated for job ";
    //}
    //--------------------------Sync_Need_Logs_AtServer_AddDelUpdtSelect------------------------
    //public Int64 Insert_Sync_Need_Logs_AtServer_AddDelUpdtSelect(string LogId, string Rcordid, string ACTION, string TAbleId, Boolean IsRecordTransfer, string sid)
    //{
    //    Int64 ReturnID = 0;
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand("Sync_Need_Logs_AtServer_AddDelUpdtSelect", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@StatementType", "Insert");
    //        cmd.Parameters.AddWithValue("@LogId", LogId);
    //        cmd.Parameters.AddWithValue("@Rcordid", Rcordid);
    //        cmd.Parameters.AddWithValue("@ACTION", ACTION);
    //        cmd.Parameters.AddWithValue("@TAbleId", TAbleId);
    //        cmd.Parameters.AddWithValue("@IsRecordTransfer", IsRecordTransfer);
    //        cmd.Parameters.AddWithValue("@sid", sid);
    //        object maxprocID = new object();
    //        maxprocID = cmd.ExecuteScalar();
    //        ReturnID = Convert.ToInt64(maxprocID);
    //    }
    //    catch
    //    {
    //        ReturnID = 0;
    //    }
    //    return ReturnID;
    //}
    //public Boolean DELETE__Sync_Need_Logs_AtServer_AddDelUpdtSelect(string ID)
    //{
    //    Boolean ReturnID = true;
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand("Sync_Need_Logs_AtServer_AddDelUpdtSelect", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@StatementType", "Delete");
    //        cmd.Parameters.AddWithValue("@ID", ID);
    //        cmd.ExecuteNonQuery();
    //    }
    //    catch
    //    {
    //        ReturnID = false;
    //    }
    //    return ReturnID;
    //}
    ////-----------------------------------------------------------------------------------
    ////----------------------------------------------------
    //public Boolean DELETE__Sync_Need_Logs__AddDelUpdtSelect(string LogId)
    //{
    //    Boolean ReturnID = true;
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand("Sync_Need_Logs_AddDelUpdtSelect", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@StatementType", "Delete");
    //        cmd.Parameters.AddWithValue("@LogId", LogId);
    //        cmd.ExecuteNonQuery();
    //    }
    //    catch
    //    {
    //        ReturnID = false;
    //    }
    //    return ReturnID;
    //}
}
