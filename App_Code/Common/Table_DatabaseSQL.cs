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
                    public class Table_DatabaseSQL
                    {
                        SqlConnection con;                     
                        public static string companykey = "aaa"; public static string serverkey = "aaaa";
                        public Table_DatabaseSQL()
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
                        public static string Delete_Temp_Table(string tablename)
                        {
                            SqlConnection con = MyCommonfile.licenseconn(); 
                            SqlCommand cmdrX = new SqlCommand("Drop table Temp_" + tablename, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdrX.ExecuteNonQuery();
                            con.Close();
                            return tablename;
                        }
                        public static string Create_Temp_Table_Design_With_Change_Datatype(string Ori_tablename, string Temp_tablename)
                        {
                            SqlConnection con = MyCommonfile.licenseconn();
                            string st1 = "CREATE TABLE " + Temp_tablename + "(";
                            DataTable dts1 = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + Ori_tablename + "'");
                            for (int k = 0; k < dts1.Rows.Count; k++)
                            {
                                if (k == 0)
                                {                                   
                                    st1 += ("" + dts1.Rows[k]["column_name"] + " nvarchar(500),");
                                }
                                else
                                {
                                    st1 += ("" + dts1.Rows[k]["column_name"] + " " + dts1.Rows[k]["data_type"] + "(" + dts1.Rows[k]["CHARACTER_MAXIMUM_LENGTH"] + "),");
                                }
                            }
                            if (dts1.Rows.Count > 0)
                            {
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
                                DataTable DTBC = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + Temp_tablename + "'");
                                if (DTBC.Rows.Count <= 0)
                                {
                                    SqlCommand cmdr = new SqlCommand(st1, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdr.ExecuteNonQuery();
                                    con.Close();
                                }
                                else
                                {
                                    string strBC = " CREATE TABLE " + Temp_tablename + "(";                                   
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
                                    strBC = strBC.Replace("nvarchar(50)", "nvarchar(500)");
                                    strBC = strBC.Replace("decimal()", "nvarchar(500)");
                                    strBC = strBC.Replace("decimal", "nvarchar(500)");
                                    strBC = strBC.Replace("bit()", "bit");
                                    strBC = strBC.Replace("nvarchar(20)", "nvarchar(500)");
                                    strBC = strBC.Replace("nvarchar(10)", "nvarchar(500)");
                                    strBC = strBC.Replace("nvarchar(100)", "nvarchar(500)");

                                    DataTable DTRecord = MyCommonfile.selectBZ("select * From " + Temp_tablename + "");
                                    if (strBC != st1 || DTRecord.Rows.Count==0)
                                    {
                                        SqlCommand cmdrX = new SqlCommand("Drop table " + Temp_tablename, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdrX.ExecuteNonQuery();
                                        con.Close();
                                        //Create Table
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        SqlCommand cmdr = new SqlCommand(st1, con);
                                        cmdr.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                            return st1;
                        }

                        public static string Create_Static_Table_Design_Sync_Need_Logs_AtServer(string Ori_tablename, string Temp_tablename)
                        {
                            SqlConnection con = MyCommonfile.licenseconn();
                            string st1 = "";
                            DataTable DTBC = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + Temp_tablename + "'");
                            if (DTBC.Rows.Count <= 0)
                            {
                                SqlCommand cmdrX = new SqlCommand("CREATE TABLE "+Temp_tablename+"([Id] [int] IDENTITY(1,1) NOT NULL,[LogId] [int] NULL,[Rcordid] [int] NULL,[TAbleId] [int] NULL,[IsRecordTransfer] [bit] NULL,[sid] [int] NULL,[ACTION] [nvarchar](50) NULL,[TakenForTemp] [bit] NULL) ON [PRIMARY]", con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdrX.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                DataTable DTBCX = MyCommonfile.selectBZ(" Select * From " + Temp_tablename + "");
                                if (DTBCX.Rows.Count == 0)
                                {
                                    SqlCommand cmdrXDrop = new SqlCommand("Drop table " + Temp_tablename, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdrXDrop.ExecuteNonQuery();
                                    con.Close();

                                    SqlCommand cmdrX = new SqlCommand(" CREATE TABLE " + Temp_tablename + "([Id] [int] IDENTITY(1,1) NOT NULL,[LogId] [int] NULL,[Rcordid] [int] NULL,[TAbleId] [int] NULL,[IsRecordTransfer] [bit] NULL,[sid] [int] NULL,[ACTION] [nvarchar](50) NULL,[TakenForTemp] [bit] NULL) ON [PRIMARY] ", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdrX.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                            return st1;
                        }

                        public static string Create_Dynmc_Table_Design_With_Change_Datatype(string tablename)
                        {
                            SqlConnection con = MyCommonfile.licenseconn();
                            string st1 = "CREATE TABLE " + tablename + "(";
                            DataTable dts1 = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
                            for (int k = 0; k < dts1.Rows.Count; k++)
                            {
                                if (k == 0)
                                {
                                    st1 += ("" + dts1.Rows[k]["column_name"] + " nvarchar(500),");
                                }
                                else
                                {
                                    st1 += ("" + dts1.Rows[k]["column_name"] + " " + dts1.Rows[k]["data_type"] + "(" + dts1.Rows[k]["CHARACTER_MAXIMUM_LENGTH"] + "),");
                                }
                            }
                            if (dts1.Rows.Count > 0)
                            {
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
                                DataTable dts = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='Temp_" + tablename + "'");
                                if (dts.Rows.Count <= 0)
                                {
                                    SqlCommand cmdr = new SqlCommand(st1, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdr.ExecuteNonQuery();
                                    con.Close();
                                }
                                else
                                {
                                    string strBC = " CREATE TABLE Temp_" + tablename + "(";
                                    DataTable DTBC = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='Temp_" + tablename + "'");
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
                                    strBC = strBC.Replace("nvarchar(50)", "nvarchar(500)");
                                    strBC = strBC.Replace("decimal()", "nvarchar(500)");
                                    strBC = strBC.Replace("decimal", "nvarchar(500)");
                                    strBC = strBC.Replace("bit()", "bit");
                                    strBC = strBC.Replace("nvarchar(20)", "nvarchar(500)");
                                    strBC = strBC.Replace("nvarchar(10)", "nvarchar(500)");
                                    strBC = strBC.Replace("nvarchar(100)", "nvarchar(500)");
                                    if (strBC != st1)
                                    {
                                        SqlCommand cmdrX = new SqlCommand("Drop table Temp_" + tablename, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdrX.ExecuteNonQuery();
                                        //Create Table
                                        SqlCommand cmdr = new SqlCommand(st1, con);
                                        cmdr.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                            return st1;
                        }

                        public static Int64 All_Table_All_Record__InsertAtLog2(string TableName, string TableId, string ServerID, string PKTableName, string PKIdName, string Select_Query)
                        {
                            Int64 Count = 0;
                            string WhereForPKID = "";
                            //WhereForPKID = " Where " + PKTableName + "." + PKIdName + "=" + RecordID;                                   
                            string SelectWhere3 = " Where " + TableName + "." + PKIdName + " NOT IN (Select Rcordid as Rcordid From Sync_Need_Logs_AtServer Where TAbleId='" + TableId + "' and sid=" + ServerID + ") ";
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
                                    Syncro_Tables.Insert___Sync_Need_Logs_AtServer(RecordID, RecordID, "1", TableId, false, ServerID);                                    
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
                                    Syncro_Tables.Insert___Sync_Need_Logs_AtServer(RecordID, RecordID, "1", TableId, false, ServerID);                                    
                                }
                             //   Syncro_Tables.Insert___Sync_Need_Logs_AtServer(RecordID, RecordID, "1", TableId, false, ServerID);
                            }
                            return Count;                           
                        }
                        public static Int64 All_Table_All_Record_SyncroniceSERID(string Sync_Need_Logs_AtServerSErID, string TableName, string TableId, string ServerID, string PKTableName, string PKIdName, string Select_Query)
                        {
                            Int64 Count = 0;
                            string WhereForPKID = "";
                            //WhereForPKID = " Where " + PKTableName + "." + PKIdName + "=" + RecordID;                                   
                            string SelectWhere3 = " Where  " + TableName + "." + PKIdName + "!='' and " + TableName + "." + PKIdName + " NOT IN (Select Rcordid as Rcordid From " + Sync_Need_Logs_AtServerSErID + " Where TAbleId='" + TableId + "' and sid=" + ServerID + ") ";
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
                                    Syncro_Tables.Insert___Sync_Need_Logs_AtServerSERID(Sync_Need_Logs_AtServerSErID, RecordID, RecordID, "1", TableId, false, ServerID);
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
                                    Syncro_Tables.Insert___Sync_Need_Logs_AtServerSERID(Sync_Need_Logs_AtServerSErID, RecordID, RecordID, "1", TableId, false, ServerID);
                                }
                                //   Syncro_Tables.Insert___Sync_Need_Logs_AtServer(RecordID, RecordID, "1", TableId, false, ServerID);
                            }
                            return Count;
                        }



                        //public static string Dynamicaly_FullTable(string TableName, string TableId,string  PricePlanId,string VersionId,string ServerID)
                        //{
                        //    string PKname = "";
                        //    string InsertInto = " INSERT INTO " + TableName + "(  ";
                        //    string Temp3val = "";
                        //    DataTable dts1 = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + TableName + "'");
                        //    for (int k = 0; k < dts1.Rows.Count; k++)
                        //    {
                        //        if (k == 0)
                        //        {
                        //            // PKname = dts1.Rows[k]["column_name"].ToString();
                        //        }
                        //        InsertInto += ("" + dts1.Rows[k]["column_name"] + " ,");
                        //    }
                        //    InsertInto = InsertInto.Remove(InsertInto.Length - 1);
                        //    InsertInto += ") VAlues";
                        //    string AfterVAlues = "";
                        //    DataTable maxiddesid = MyCommonfile.selectBZ(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + TableName + "'");
                        //    string QueryName = "";

                        //    PKname = TableRelated.SatelliteSyncronisationrequiringTablesMaster_WherePKIDName(TableId);
                        //    String WhereID = TableRelated.SatelliteSyncronisationrequiringTablesMaster_WhereWhereID(TableId);
                        //    QueryName = TableRelated.SatelliteSyncronisationrequiringTablesMaster_Where(TableId);
                        //    if (QueryName.Length > 0)
                        //    {
                        //        if (WhereID == "1")
                        //        {
                        //            QueryName = " Where " + PKname + " IN ( " + QueryName + "=" + PricePlanId + ")";
                        //        }
                        //        if (WhereID == "2")
                        //        {
                        //            QueryName = " Where " + PKname + " IN ( " + QueryName + "=" + VersionId + ")";
                        //        }
                        //    }

                        //    DataTable dtr = MyCommonfile.selectBZ(" Select * From " + TableName + " " + QueryName + "");
                        //    try
                        //    {
                        //        DataTable dtrcount = MyCommonfile.selectBZ(" Select Count(" + PKname + ") as PKname From " + TableName + " " + QueryName + "");
                        //        string ss = TableRelated.AAAAAAA_Record(TableName, dtrcount.Rows[0]["PKname"].ToString(), ServerID);
                        //    }
                        //    catch
                        //    {
                        //    }
                        //    int c = 0;
                        //    foreach (DataRow itm in dtr.Rows)
                        //    {
                        //        string cccd = InsertInto + " (";
                        //        DataTable dtsccc = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + TableName + "'");
                        //        for (int k = 0; k < dtsccc.Rows.Count; k++)
                        //        {
                        //            cccd += "'" + Encrypted(Convert.ToString(itm["" + dtsccc.Rows[k]["column_name"] + ""])) + "' ,";
                        //        }
                        //        cccd = cccd.Remove(cccd.Length - 1);
                        //        cccd += " )";
                        //        if (Temp3val.Length > 0)
                        //        {
                        //            // Temp3val += ",";
                        //        }
                        //        Temp3val += cccd;
                        //    }
                        //    if (Temp3val.Length > 0)
                        //    {
                        //        if (conn.State.ToString() != "Open")
                        //        {
                        //            conn.Open();
                        //        }
                        //        SqlCommand ccm = new SqlCommand(Temp3val, conn);
                        //        ccm.ExecuteNonQuery();
                        //        conn.Close();
                        //    }
                        //}


                    }