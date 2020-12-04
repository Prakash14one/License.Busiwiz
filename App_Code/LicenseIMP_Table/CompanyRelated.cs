using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;
                    public class CompanyRelated
                    {  
                        public CompanyRelated()
                        {
                           
                        }
                        public static SqlConnection licenseconn()
                        {
                            SqlConnection liceco = new SqlConnection();

                            liceco = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
                          
                            return liceco;
                        }
                        public static SqlConnection serverconnstring()
                        {
                            SqlConnection serverconnstri = new SqlConnection();
                            serverconnstri.ConnectionString = @"Data Source =C3SERVERMASTER,30000; Initial Catalog=C3SATELLITESERVER; User ID=Sa; Password=06De1963++; Persist Security Info=true;";                            
                            return serverconnstri;
                        }
                        public static Boolean Insert_CompanyCreationNeedCode(string comploginid, string codetypeid, string CodeTypeCategoryId, string ProductCodeDetailId, string codeversionno, string VersionInfoMasterId, string Physicalpath, string zipfolder, string dnsname, string CodeTypeName, Boolean AdditionalPageInserted, string ClientMasterURL, Boolean IsDefaultDatabase, Boolean BusiwizSynchronization, string sqlservernameip, string sqlinstancename, string Successfullyuploadedtoserver, string ProductMastercodeID, string DNSserverid, bool DNSAddingStatus, string Comp_IISPath)
                        {
                            SqlConnection liceco = new SqlConnection();
                            Boolean Status = true;
                                                    
                                liceco = MyCommonfile.licenseconn();
                                if (liceco.State.ToString() != "Open")
                                {
                                    liceco.Open();
                                }
                                SqlCommand cmd = new SqlCommand("CompanyCreationNeedCode_AddDelUpdtSelect", liceco);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                                cmd.Parameters.AddWithValue("@CompanyLoginId", comploginid);
                                cmd.Parameters.AddWithValue("@CodeTypeId", codetypeid);
                                cmd.Parameters.AddWithValue("@CodeTypeCategoryId", CodeTypeCategoryId);
                                cmd.Parameters.AddWithValue("@CodeDetailId", ProductCodeDetailId);
                                cmd.Parameters.AddWithValue("@CodeVersionNo", codeversionno);
                                cmd.Parameters.AddWithValue("@ProductVersionId", VersionInfoMasterId);
                                cmd.Parameters.AddWithValue("@Physicalpath", Physicalpath);
                                cmd.Parameters.AddWithValue("@filename", zipfolder);
                                cmd.Parameters.AddWithValue("@DNSname", dnsname);
                                cmd.Parameters.AddWithValue("@CodeTypeName", CodeTypeName);
                                cmd.Parameters.AddWithValue("@YourDomaiUrl", "");
                                cmd.Parameters.AddWithValue("@IsDefaultDatabase", IsDefaultDatabase);
                                cmd.Parameters.AddWithValue("@AdditionalPageInserted", AdditionalPageInserted);
                                cmd.Parameters.AddWithValue("@Successfullystatus", false);
                                cmd.Parameters.AddWithValue("@DownloadCompanyCode", false);
                                cmd.Parameters.AddWithValue("@ClientMasterURL", ClientMasterURL);
                                cmd.Parameters.AddWithValue("@BusiwizSynchronization", BusiwizSynchronization);
                                cmd.Parameters.AddWithValue("@Successfullyuploadedtoserver", Successfullyuploadedtoserver);
                                cmd.Parameters.AddWithValue("@ProductMastercodeID", ProductMastercodeID);
                                cmd.Parameters.AddWithValue("@DNSserid", DNSserverid);
                                cmd.Parameters.AddWithValue("@DNSAddingStatus", DNSAddingStatus);
                                cmd.Parameters.AddWithValue("@Comp_IISPath", Comp_IISPath);                                  
                                cmd.ExecuteNonQuery();                                
                                liceco.Close();
                                Status = true;
                           
                            return Status;
                        }


                        public static string Insert_SilentPageRequestTbl(string SilentPageServerID,string PageName, string Datetimeofrequest, string Dateandtimefinish, Boolean Requestfinish, string Randomkeyid,string destinetionUrl)
                        {
                            string  maxid = "0";
                            SqlConnection liceco = new SqlConnection();
                            liceco = MyCommonfile.licenseconn();
                            if (liceco.State.ToString() != "Open")
                            {
                                liceco.Open();
                            }
                            SqlCommand cmd = new SqlCommand("SilentPageRequestTbl_AddDelUpdtSelect", liceco);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Insert");
                            //cmd.Parameters.AddWithValue("@Id", comploginid);
                            cmd.Parameters.AddWithValue("@SilentPageServerID", SilentPageServerID);
                            cmd.Parameters.AddWithValue("@PageName", PageName);
                            cmd.Parameters.AddWithValue("@Datetimeofrequest", Datetimeofrequest);
                            cmd.Parameters.AddWithValue("@Dateandtimefinish", Dateandtimefinish);
                            cmd.Parameters.AddWithValue("@Requestfinish", false);
                            cmd.Parameters.AddWithValue("@Randomkeyid", Randomkeyid);
                            cmd.Parameters.AddWithValue("@destinetionUrl", destinetionUrl);                           
                          //  cmd.ExecuteNonQuery();                           
                            object obj = cmd.ExecuteScalar();
                            liceco.Close();
                            return obj.ToString();
                        }



                        public static bool Insert_CompanyCreationStep4SubStatustbl(string StatusMasterId, string CompanyLoginId, Boolean FinishStatus)
                        {
                            bool status= true;
                            SqlConnection liceco = new SqlConnection();
                            try
                            {
                               
                                liceco = MyCommonfile.licenseconn();
                                if (liceco.State.ToString() != "Open")
                                {
                                    liceco.Open();
                                }
                                SqlCommand cmd = new SqlCommand("CompanyCreationStep4SubStatustbl_AddDelUpdtSelect", liceco);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                                cmd.Parameters.AddWithValue("@StatusMasterId", StatusMasterId);
                                cmd.Parameters.AddWithValue("@CompanyLoginId", CompanyLoginId);
                                cmd.Parameters.AddWithValue("@FinishStatus", FinishStatus);
                                cmd.Parameters.AddWithValue("@DateTime", DateTime.Now.ToString());
                                cmd.ExecuteNonQuery();
                                liceco.Close();                            
                            }
                            catch
                            {
                                status = false;
                                liceco.Close();           
                            }
                            return status;
                        }

                        public static bool Update_CompanyCreationNeedCode(string id, bool DNSAddingStatus)
                        {
                            bool status = true;
                            SqlConnection liceco = new SqlConnection();
                            try
                            {                              
                                liceco = MyCommonfile.licenseconn();
                                if (liceco.State.ToString() != "Open")
                                {
                                    liceco.Open();
                                }
                                SqlCommand cmd = new SqlCommand("CompanyCreationNeedCode_AddDelUpdtSelect", liceco);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@StatementType", "Update_DNSAddingStatus");
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.Parameters.AddWithValue("@DNSAddingStatus", DNSAddingStatus);                              
                                cmd.ExecuteNonQuery();
                                liceco.Close();
                            }
                            catch
                            {
                                status = false;
                                liceco.Close();
                            }
                            return status;
                        }
                    }