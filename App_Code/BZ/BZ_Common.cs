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
                    public class BZ_Common
                    {
                        SqlConnection con; 
                        public BZ_Common()
                        {
                           
                        }
                    


                        //------------------------------------------------------                       
                        public static string Decrypted_mstrsrvky(string str)
                        {                            
                            return Decrypt(str, mstrsrvky());
                        }
                        public static string Encrypted_mstrsrvky(string strText)
                        {
                            return Encrypt(strText, mstrsrvky());
                        }
                        public static string mstrsrvky()
                        {
                            string mstrsrvky = "nsnW1MQh4UNRbn3d7xxX";
                            return mstrsrvky;
                        }                       
                        //-------------------------------------------------------------
                        public static string Decrypted_satsrvencryky(string str)
                        {
                            return Decrypt(str, satsrvencryky());
                        }
                        public static string Encrypted_satsrvencryky(string strText)
                        {
                            return Encrypt(strText, satsrvencryky());
                        }
                        public static string satsrvencryky()
                        {
                            string satsrvencryky = "9Qip5t3dVOSSbThsR4p8";
                            return satsrvencryky;
                        }
                        //--------------------------------------------------------                      
                        public static string Decrypted_mstrsrvkyreply(string str)
                        {
                            return Decrypt(str, mstrsrvkyreply());
                        }
                        public static string Encrypted_mstrsrvkyreply(string strText)
                        {
                            return Encrypt(strText, mstrsrvkyreply());
                        }
                        public static string mstrsrvkyreply()
                        {
                            string mstrsrvkyreply = "U8mKVo4tCri6mFeXOP3y";
                            return mstrsrvkyreply;
                        }
                        //--------------------License--------------------
                        public static string BZ_Encrypted(string strText)
                        {
                            return Encrypt(strText, "&%#@?,:*");
                        }
                        public static string BZ_Decrypted(string str)
                        {
                            return Decrypt(str, "&%#@?,:*");
                        }

                        //------------------------------------------------------------------------------------------------------------------------
                        public static string Decrypted_DasdasdasdadynamicKey(string str, string key)
                        {
                            return Decrypt(str, DasdasdasdadynamicKey(key));
                        }
                        public static string Encrypted_DasdasdasdadynamicKey(string strText, string key)
                        {
                            return Encrypt(strText, DasdasdasdadynamicKey(key));
                        }
                        public static string DasdasdasdadynamicKey(string kssekeertryid)
                        {
                            string mstrsrvky = "";
                            if (kssekeertryid == "1")
                            {
                                mstrsrvky = "prsjfMQh4UNRbn3d7xxX";
                            }
                            if (kssekeertryid == "2")
                            {
                                mstrsrvky = "nsnW1MGTrtNRbn3d7xxX";
                            }
                            if (kssekeertryid == "3")
                            {
                                mstrsrvky = "nsnW1MQh4UOytred7xxX";
                            }
                            if (kssekeertryid == "4")
                            {
                                mstrsrvky = "nsnW1MQh4UNRbn3as789";
                            }
                            if (kssekeertryid == "5")
                            {
                                mstrsrvky = "nsnW1MQh4UNRbn3d7xxX";
                            }
                            if (kssekeertryid == "6")
                            {
                                mstrsrvky = "nsnW1MQh4gfdrt3d7xxX";
                            }
                            if (kssekeertryid == "7")
                            {
                                mstrsrvky = "tryytMQh4UNRbn3d7xxX";
                            }
                            if (kssekeertryid == "8")
                            {
                                mstrsrvky = "hjkkjMQh4UNRbn3d7xxX";
                            }
                            if (kssekeertryid == "9")
                            {
                                mstrsrvky = "nsnW1MQh4UNRbn3d7xxX";
                            }
                            if (kssekeertryid == "10")
                            {
                                mstrsrvky = "zxccxMQh4UNRbn3d7xxX";
                            }
                            return mstrsrvky;
                        }
                        //----------------------------------------------
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
                      
    }