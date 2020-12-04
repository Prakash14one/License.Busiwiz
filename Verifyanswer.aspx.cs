using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Net.Security;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

public partial class Verifyanswer : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection connCompserver = new SqlConnection();
    public static string encstr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["licensesilentpagerequesttblid"] != null && Request.QueryString["randomkeyID"] != null && Request.QueryString["sateliteserverrequesttblid"] != null && Request.QueryString["rnkendecke"] != null)//portno
        {            
            int bannn = 0;
            string rnkendecke = Request.QueryString["rnkendecke"].ToString();
            string licreqid = BZ_Common.Decrypted_DasdasdasdadynamicKey(Request.QueryString["licensesilentpagerequesttblid"].ToString().Replace(" ", "+"), rnkendecke);
            string serreqid = BZ_Common.Decrypted_DasdasdasdadynamicKey(Request.QueryString["sateliteserverrequesttblid"].ToString().Replace(" ", "+"), rnkendecke);
            string randomkeyno = BZ_Common.Decrypted_DasdasdasdadynamicKey(Request.QueryString["randomkeyID"].ToString(), rnkendecke);

           
            DataTable ds1 = MyCommonfile.selectBZ("select SilentPageServerID from SilentPageRequestTbl where ID= '" + licreqid.ToString() + "'");
            if (ds1.Rows.Count > 0)
            {
                DataTable ds = MyCommonfile.selectBZ("select ServerMasterTbl.* from ServerMasterTbl where ServerMasterTbl.Id= '" + ds1.Rows[0]["SilentPageServerID"].ToString() + "'");
                if (ds.Rows.Count > 0)
                {
                    encstr = Convert.ToString(ds.Rows[0]["Enckey"]);
                    string Comp_serversqlserverip = ds.Rows[0]["sqlurl"].ToString();
                    string Comp_serversqlinstancename = ds.Rows[0]["DefaultsqlInstance"].ToString();
                    string Comp_serversqlport = ds.Rows[0]["port"].ToString();
                    string Comp_serversqldbname = ds.Rows[0]["DefaultDatabaseName"].ToString();
                    string Comp_serversqlpwd = ds.Rows[0]["Sapassword"].ToString();
                    string Comp_serverweburl = ds.Rows[0]["Busiwizsatellitesiteurl"].ToString();
                    connCompserver.ConnectionString = @"Data Source =" + Comp_serversqlserverip + "\\" + "\\" + Comp_serversqlinstancename + "," + Comp_serversqlport + "; Initial Catalog=" + Comp_serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(Comp_serversqlpwd) + "; Persist Security Info=true;";
                }
                else
                {
                    bannn = 1;
                }
                DataTable dtstatus = MyCommonfile.selectBZ("select * from SilentPageRequestTbl where ID='" + licreqid.ToString() + "' and requestfinish='Yes'");
                if (dtstatus.Rows.Count > 0)
                {
                    bannn = 1;
                }
                else
                {
                    DataTable dtcheck = MyCommonfile.selectBZ("select * from SilentPageRequestTbl where ID='" + licreqid.ToString() + "'");
                    if (dtcheck.Rows.Count > 0)
                    {
                        DateTime dt = DateTime.Now;
                        DateTime bann = Convert.ToDateTime(dtcheck.Rows[0]["datetimeofrequest"].ToString()).AddHours(1);
                        if (dt <= bann)
                        {
                            string keyvalue = "";
                            if (randomkeyno.ToString() == "0")
                            {
                                keyvalue = BZ_Common.mstrsrvkyreply();
                            }
                            else
                            {
                                string str132 = "select Securitykey" + randomkeyno + " from Securitykeyforsilentpages where  serverid ='" + ds1.Rows[0]["SilentPageServerID"].ToString() + "'";
                                SqlCommand cgw = new SqlCommand(str132, connCompserver);
                                SqlDataAdapter adgw = new SqlDataAdapter(cgw);
                                DataTable dt2 = new DataTable();
                                adgw.Fill(dt2);
                                if (dt2.Rows.Count > 0)
                                {
                                    string mystr = Convert.ToString(DateTime.Now.Day);
                                    mystr = mystr.Substring(0, 1);
                                    keyvalue = "" + dt2.Rows[0][0].ToString() + "" + mystr + "" + DateTime.Now.ToShortDateString() + "";
                                }
                            }  
                            string hh = "update  SilentPageRequestTbl set randomkeyid='" + randomkeyno + "',dateandtimefinish='" + DateTime.Now.ToString() + "',requestfinish='Yes' where ID='" + licreqid.ToString() + "'";
                            SqlCommand cmd = new SqlCommand(hh, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("http://" + ds.Rows[0]["Busiwizsatellitesiteurl"].ToString() + "/Verifystage2.aspx?sateliteserversilentpagerequesttblid=" + BZ_Common.Encrypted_DasdasdasdadynamicKey(serreqid.ToString(), rnkendecke) + "&randomkeyvalue=" + BZ_Common.Encrypted_DasdasdasdadynamicKey(keyvalue.ToString(), rnkendecke) + "&rnkendecke=" + Request.QueryString["rnkendecke"] + "");
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("delete from  SilentPageRequestTbl where ID='" + licreqid.ToString() + "')", con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else
                    {
                        bannn = 1;
                    }
                }
            }
            else
            {
            }
        }
    }
   
    protected DataTable selectserv(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, connCompserver);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
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
    public static string Encrypted(string strText)
    {

        return Encrypt(strText, encstr);

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

    public static string Decrypted(string str)
    {

        return Decrypt(str, encstr);

    }
}