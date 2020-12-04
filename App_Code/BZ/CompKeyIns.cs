using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

using System.Data.Common;
                    public class CompKeyIns
                    {
                      
                        public SqlConnection  con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
                        public static string companykey = "aaa"; public static string serverkey = "aaaa";
                        public CompKeyIns()
                        {
                           // Liceconn = new SqlConnection();
                            con = new SqlConnection();                            
                        }
                        public static string C1toC5GetC(String AA, Int64 F, String XX, String YY, String ZZ)
                        {
                            string CC = "";
                            Int64 C = 0;
                            //txt_date.Text = dy + "" + mn + "" + yy;
                            Int64 A =Convert.ToInt64(AA);
                            //String ZZ = ZDate;
                            //DateTime startDate = DateTime.Parse(ZZ);
                            //DateTime datevalue = (Convert.ToDateTime(startDate.ToString("MM-dd-yyyy")));

                            String DD = XX;
                            String MM = YY;
                            String YYYY = ZZ;


                            Int64 X = Convert.ToInt64(DD); ;
                            Int64 Y = Convert.ToInt64(MM); ;
                            Int64 Z = Convert.ToInt64(YYYY); ;

                            if (F == 1)
                            {
                                C = A + (X + Y + Z);
                            }
                            //-------------------------

                            if (F == 2)
                            {
                                C = A + (X + Y - Z);
                            }
                            //--------------------------

                            if (F == 3)
                            {
                                C = A + (X - Y + Z);
                            }
                            //--------------------------

                            if (F == 4)
                            {
                                C = A - (X + Y + Z);
                            }
                            //--------------------------

                            if (F == 5)
                            {
                                C = A - (X - Y - Z);
                            }
                            //--------------------------          

                            if (F == 6)
                            {
                                C = A + (X - Y - Z);
                            }

                            CC = Convert.ToString(C);
                            return CC;
                        }

                        public static string C1toC5GetA(Int64 CCC, Int64 F, String XX, String YY, String ZZ)
                        {
                            string AAAA = "";
                            Double A = 0;

                            Int64 C = Convert.ToInt64(CCC);                           

                            String DD = XX;
                            String MM = YY;
                            String YYYY = ZZ;


                            Int64 X = Convert.ToInt64(DD); ;
                            Int64 Y = Convert.ToInt64(MM); ;
                            Int64 Z = Convert.ToInt64(YYYY); ;

                            if (F == 1)
                            {
                                A = C - (X + Y + Z);
                            }
                            //-------------------------

                            if (F == 2)
                            {
                                A = C - (X + Y - Z);
                            }
                            //--------------------------

                            if (F == 3)
                            {
                                A = C - (X - Y + Z);
                            }
                            //--------------------------

                            if (F == 4)
                            {
                                A = C + (X + Y + Z);
                            }
                            //--------------------------

                            if (F == 5)
                            {
                                A = C + (X - Y - Z);
                            }
                            //--------------------------          

                            if (F == 6)
                            {
                                A = C - (X - Y - Z);
                            }

                            AAAA = Convert.ToString(A);
                            return AAAA;
                        }

                        public static string RandomeIntnumber(int passwordLength)
                        {
                            string allowedChars = "0123456789989898989898";

                            char[] chars = new char[passwordLength];
                            Random rd = new Random();
                            for (int i = 0; i < passwordLength; i++)
                            {
                                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                            }
                            return new string(chars);
                        }
                                              
                        public static Boolean Insert_CompanyABCDetail(string CompanyLoginId, string Z, string A1, string A2, string A3, string A4, string A5, string D1, string D2, string D3, string D4, string D5, string E1, string E2, string E3, string E4, string E5, string F1, string F2, string F3, string F4, string F5, string txt_ansc1, string txt_ansc2, string txt_ansc3, string txt_ansc4, string txt_ansc5, string txt_ansc, string ServerId)
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
                                SqlCommand cmd = new SqlCommand("CompanyABCDetail_AddDelUpdtSelect", liceco);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                                cmd.Parameters.AddWithValue("@CompanyLoginId", CompanyLoginId);
                                cmd.Parameters.AddWithValue("@Z", Z);
                                cmd.Parameters.AddWithValue("@A1", A1);
                                cmd.Parameters.AddWithValue("@A2", A2);
                                cmd.Parameters.AddWithValue("@A3", A3);
                                cmd.Parameters.AddWithValue("@A4", A4);
                                cmd.Parameters.AddWithValue("@A5", A5);

                                cmd.Parameters.AddWithValue("@D1", D1);
                                cmd.Parameters.AddWithValue("@D2", D2);
                                cmd.Parameters.AddWithValue("@D3", D3);
                                cmd.Parameters.AddWithValue("@D4", D4);
                                cmd.Parameters.AddWithValue("@D5", D5);

                                cmd.Parameters.AddWithValue("@E1", E1);
                                cmd.Parameters.AddWithValue("@E2", E2);
                                cmd.Parameters.AddWithValue("@E3", E3);
                                cmd.Parameters.AddWithValue("@E4", E4);
                                cmd.Parameters.AddWithValue("@E5", E5);

                                cmd.Parameters.AddWithValue("@F1", F1);
                                cmd.Parameters.AddWithValue("@F2", F2);
                                cmd.Parameters.AddWithValue("@F3", F3);
                                cmd.Parameters.AddWithValue("@F4", F4);
                                cmd.Parameters.AddWithValue("@F5", F5);

                                cmd.Parameters.AddWithValue("@ansc1", txt_ansc1);
                                cmd.Parameters.AddWithValue("@ansc2", txt_ansc2);
                                cmd.Parameters.AddWithValue("@ansc3", txt_ansc3);
                                cmd.Parameters.AddWithValue("@ansc4", txt_ansc4);
                                cmd.Parameters.AddWithValue("@ansc5", txt_ansc5);

                                cmd.Parameters.AddWithValue("@ansc", txt_ansc);

                                cmd.Parameters.AddWithValue("@ServerId", ServerId);

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
                        public static Boolean Insert_CompanyABCMaster(string CompanyLoginId, string C, string C1, string C2, string C3, string C4, string C5, String ServerId)
                        {
                             SqlConnection liceco = new SqlConnection();
                            liceco =MyCommonfile.licenseconn();
                            if (liceco.State.ToString() != "Open")
                            {
                                liceco.Open();
                            }
                            SqlCommand cmd = new SqlCommand("CompanyABCMaster_AddDelUpdtSelect", liceco);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Insert");
                            cmd.Parameters.AddWithValue("@CompanyLoginId", CompanyLoginId);
                            cmd.Parameters.AddWithValue("@C", C);
                            cmd.Parameters.AddWithValue("@C1", C1);
                            cmd.Parameters.AddWithValue("@C2", C2);
                            cmd.Parameters.AddWithValue("@C3", C3);
                            cmd.Parameters.AddWithValue("@C4", C4);
                            cmd.Parameters.AddWithValue("@C5", C5);

                            cmd.Parameters.AddWithValue("@ServerId", ServerId);                            
                            cmd.ExecuteNonQuery();
                            liceco.Close();

                            return true;
                        }
                        public static Boolean Delete_CompanyABCDetail(string CompanyLoginId)
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
                                SqlCommand cmdDel = new SqlCommand("CompanyABCDetail_AddDelUpdtSelect", liceco);
                                cmdDel.CommandType = CommandType.StoredProcedure;
                                cmdDel.Parameters.AddWithValue("@StatementType", "Delete");
                                cmdDel.Parameters.AddWithValue("@CompanyLoginId", CompanyLoginId);
                                cmdDel.ExecuteNonQuery();
                                Status= true;
                            }
                            catch
                            {
                                Status= false;
                            }
                            return Status;
                        }
                        public static Boolean Delete_CompanyABCMaster(string companyid)
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
                                SqlCommand cmdDel = new SqlCommand("CompanyABCMaster_AddDelUpdtSelect", liceco);
                                cmdDel.CommandType = CommandType.StoredProcedure;
                                cmdDel.Parameters.AddWithValue("@StatementType", "Delete");
                                cmdDel.Parameters.AddWithValue("@CompanyLoginId", companyid);
                                cmdDel.ExecuteNonQuery();
                                Status = true;
                            }
                            catch
                            {
                                Status = false;
                            }
                            return Status;
                        }


                        public static Boolean Delete_CompanyABCDetail_Server(string CompanyLoginId)
                        {
                            Boolean Status = false;
                            try
                            {
                                SqlConnection connCompserver = new SqlConnection();
                                connCompserver = ServerWizard.ServerDefaultInstanceConnetionTCP(CompanyLoginId);                               
                                if (connCompserver.State.ToString() != "Open")
                                {
                                    connCompserver.Open();
                                }
                                SqlCommand cmdDel = new SqlCommand("CompanyABCDetail_AddDelUpdtSelect", connCompserver);
                                cmdDel.CommandType = CommandType.StoredProcedure;
                                cmdDel.Parameters.AddWithValue("@StatementType", "Delete");
                                cmdDel.Parameters.AddWithValue("@CompanyLoginId", CompanyLoginId);
                                cmdDel.ExecuteNonQuery();
                                Status= true;
                            }
                            catch
                            {
                                Status= false;
                            }
                            return Status;
                        }
                        public static Boolean Insert_CompanyABCDetail_SERVER(string CompanyLoginId, string Z, string D1, string D2, string D3, string D4, string D5, string E1, string E2, string E3, string E4, string E5, string F1, string F2, string F3, string F4, string F5)
                        {
                            Boolean Status = false;
                            try
                            {
                                SqlConnection connCompserver = new SqlConnection();
                                connCompserver = ServerWizard.ServerDefaultInstanceConnetionTCP(CompanyLoginId);
                                if (connCompserver.State.ToString() != "Open")
                                {
                                    connCompserver.Open();
                                }
                                SqlCommand cmd = new SqlCommand("CompanyABCDetail_AddDelUpdtSelect", connCompserver);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                                cmd.Parameters.AddWithValue("@CompanyLoginId", CompanyLoginId);
                                cmd.Parameters.AddWithValue("@Z", Z);

                                cmd.Parameters.AddWithValue("@D1", D1);
                                cmd.Parameters.AddWithValue("@D2", D1);
                                cmd.Parameters.AddWithValue("@D3", D3);
                                cmd.Parameters.AddWithValue("@D4", D4);
                                cmd.Parameters.AddWithValue("@D5", D5);

                                cmd.Parameters.AddWithValue("@E1", E1);
                                cmd.Parameters.AddWithValue("@E2", E1);
                                cmd.Parameters.AddWithValue("@E3", E3);
                                cmd.Parameters.AddWithValue("@E4", E4);
                                cmd.Parameters.AddWithValue("@E5", E5);

                                cmd.Parameters.AddWithValue("@F1", F1);
                                cmd.Parameters.AddWithValue("@F2", F1);
                                cmd.Parameters.AddWithValue("@F3", F3);
                                cmd.Parameters.AddWithValue("@F4", F4);
                                cmd.Parameters.AddWithValue("@F5", F5);
                                cmd.ExecuteNonQuery();
                                connCompserver.Close();
                                Status = true;
                            }
                            catch
                            {
                                 Status  =false;
                            }
                            return Status;
                        }

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
                               // Boolean Del_CABCD_Server = CompKeyIns.Delete_CompanyABCDetail_Server(CompanyLoginId);
                                if (Del_CABCD == true )
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
                                    DateTime expiryDate = startDate.AddDays(15);
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

                                       
                                        string txt_ansc = "";
                                        //----------------------------------------------------------------------------------------------------------------               
                                        Boolean Insert_CABCD_License = CompKeyIns.Insert_CompanyABCDetail(CompanyLoginId, ZDate, A1, A2, A3, A4, A5, D1, D2, D3, D4, D5, E1, E2, E3, E4, E5, Convert.ToString(F1), Convert.ToString(F2), Convert.ToString(F3), Convert.ToString(F4), Convert.ToString(F5), Convert.ToString(C1), Convert.ToString(C2), Convert.ToString(C3), Convert.ToString(C4), Convert.ToString(C5), txt_ansc, ServerId);
                                        if (Insert_CABCD_License == true)
                                        {
                                            //Boolean Insert_CABCD_Server = CompKeyIns.Insert_CompanyABCDetail_SERVER(CompanyLoginId, ZDate, D1, D2, D3, D4, D5, E1, E2, E3, E4, E5, Convert.ToString(F1), Convert.ToString(F2), Convert.ToString(F3), Convert.ToString(F4), Convert.ToString(F5));
                                            //if (Insert_CABCD_Server == true)
                                            //{
                                            //    Status = false;
                                            //}
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

                        public static Boolean BtnABCKeyAtLicenseServer(string CompanyLoginId, string ServerId)
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
                                                Status = false;
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
                        public static string Encrypted_withkey(string strText, string enckey)
                        {

                            return Encrypt(strText, enckey);

                        }

                        private static string Decrypt(string strText, string strEncrypt)
                        {
                            byte[] bKey = new byte[20];
                            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                            try
                            {
                                bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                                Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
                                MemoryStream ms = new MemoryStream();
                                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
                                cs.Write(inputByteArray, 0, inputByteArray.Length);
                                cs.FlushFinalBlock();
                                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                                return encoding.GetString(ms.ToArray());
                            }
                            catch (Exception ex)
                            {
                                return strText;
                                //throw ex;
                            }
                        }

                        public static string Decrypted_withkey(string str, string enckey)
                        {

                            return Decrypt(str, enckey);

                        }

 }