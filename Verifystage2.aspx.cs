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

public partial class Verifystage2 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection connCompserver;
    public static string encstr = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["rankey"] != null && Request.QueryString["seid"] != null && Request.QueryString["prodid"] != null && Request.QueryString["coid"] != null && Request.QueryString["paname"] != null && Request.QueryString["portid"] != null)//portid
        {

            string str1321 = "select * from ServerMasterTbl where Id= '" + Request.QueryString["seid"].ToString() + "' ";
            SqlCommand cgw1 = new SqlCommand(str1321, con);//license
            SqlDataAdapter adgw1 = new SqlDataAdapter(cgw1);
            DataTable dt1 = new DataTable();
            adgw1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                encstr = dt1.Rows[0]["Enckey"].ToString();
                string portid = Decrypted(Request.QueryString["portid"].ToString());
                string productid = Decrypted(Request.QueryString["prodid"].ToString());
                string codeid = Decrypted(Request.QueryString["coid"].ToString());
                string page = Request.QueryString["paname"].ToString().Replace(" ", "+");
                string pagename = Decrypted(page.ToString());
                if (portid.ToString() == "1")
                {
                    string port = dt1.Rows[0]["FTPportfordefaultIISpath"].ToString();
                    try
                    {
                        delectport(port);
                    }
                    catch
                    {
                    }
                    ExecuteBatFile(port);

                    string strServerstatusmaster = " Insert Into PortSecurityTbl (serverid,portnumber,portopendatetime)  values ('" + Request.QueryString["seid"].ToString() + "','" + port + "','" + DateTime.Now.ToShortDateString() + "')";
                  SqlCommand  cmd = new SqlCommand(strServerstatusmaster, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Redirect("http://" + dt1.Rows[0]["Busiwizsatellitesiteurl"].ToString() + "/" + pagename.ToString() + "?ftpurl=" + PageMgmt.Encrypted(dt1.Rows[0]["FTPforMastercode"].ToString()) + "&userid=" + PageMgmt.Encrypted(dt1.Rows[0]["FTPuseridforDefaultIISpath"].ToString()) + "&password=" + PageMgmt.Encrypted(dt1.Rows[0]["FtpPasswordforDefaultIISpath"].ToString()) + "&portno=" + PageMgmt.Encrypted(dt1.Rows[0]["FTPportfordefaultIISpath"].ToString()) + "&productid=" + PageMgmt.Encrypted(productid.ToString()) + "&serverid=" + PageMgmt.Encrypted(Request.QueryString["seid"].ToString()) + "&codeid=" + PageMgmt.Encrypted(codeid.ToString()) + "");

                }
                else if (portid.ToString() == "2")
                {
                    string port = dt1.Rows[0]["Default_FTPPort"].ToString();
                    try
                    {
                        delectport(port);
                    }
                    catch
                    {
                    }
                    ExecuteBatFile(port);

                    string strServerstatusmaster = " Insert Into PortSecurityTbl (serverid,portnumber,portopendatetime)  values ('" + Request.QueryString["seid"].ToString() + "','" + port + "','" + DateTime.Now.ToShortDateString() + "')";
                    SqlCommand cmd = new SqlCommand(strServerstatusmaster, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Redirect("http://" + dt1.Rows[0]["Busiwizsatellitesiteurl"].ToString() + "/" + pagename.ToString() + "?ftpurl=" + PageMgmt.Encrypted(dt1.Rows[0]["Default_FTPUrl"].ToString()) + "&userid=" + PageMgmt.Encrypted(dt1.Rows[0]["Default_FTPUserId"].ToString()) + "&password=" + PageMgmt.Encrypted(dt1.Rows[0]["Default_FTPPassword"].ToString()) + "&portno=" + PageMgmt.Encrypted(dt1.Rows[0]["Default_FTPPort"].ToString()) + "&productid=" + PageMgmt.Encrypted(productid.ToString()) + "&serverid=" + PageMgmt.Encrypted(Request.QueryString["seid"].ToString()) + "&codeid=" + PageMgmt.Encrypted(codeid.ToString()) + "");

               
                }
                else if (portid.ToString() == "3")
                {
                    string port = dt1.Rows[0]["MDF_FTPPort"].ToString();
                    try
                    {
                        delectport(port);
                    }
                    catch
                    {
                    }
                    ExecuteBatFile(port);
                    string strServerstatusmaster = " Insert Into PortSecurityTbl (serverid,portnumber,portopendatetime)  values ('" + Request.QueryString["seid"].ToString() + "','" + port + "','" + DateTime.Now.ToShortDateString() + "')";
                    SqlCommand cmd = new SqlCommand(strServerstatusmaster, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();


                    Response.Redirect("http://" + dt1.Rows[0]["Busiwizsatellitesiteurl"].ToString() + "/" + pagename.ToString() + "?ftpurl=" + PageMgmt.Encrypted(dt1.Rows[0]["MDF_FTPUrl"].ToString()) + "&userid=" + PageMgmt.Encrypted(dt1.Rows[0]["MDF_FTPUserId"].ToString()) + "&password=" + PageMgmt.Encrypted(dt1.Rows[0]["MDF_FTPPassword"].ToString()) + "&portno=" + PageMgmt.Encrypted(dt1.Rows[0]["MDF_FTPPort"].ToString()) + "&productid=" + PageMgmt.Encrypted(productid.ToString()) + "&serverid=" + PageMgmt.Encrypted(Request.QueryString["seid"].ToString()) + "&codeid=" + PageMgmt.Encrypted(codeid.ToString()) + "");

               
                }
                else if (portid.ToString() == "4")
                {
                    string port = dt1.Rows[0]["LDF_FTPPort"].ToString();
                    try
                    {
                        delectport(port);
                    }
                    catch
                    {
                    }
                    ExecuteBatFile(port);

                    string strServerstatusmaster = " Insert Into PortSecurityTbl (serverid,portnumber,portopendatetime)  values ('" + Request.QueryString["seid"].ToString() + "','" + port + "','" + DateTime.Now.ToShortDateString() + "')";
                    SqlCommand cmd = new SqlCommand(strServerstatusmaster, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Redirect("http://" + dt1.Rows[0]["Busiwizsatellitesiteurl"].ToString() + "/" + pagename.ToString() + "?ftpurl=" + PageMgmt.Encrypted(dt1.Rows[0]["LDF_FTPUrl"].ToString()) + "&userid=" + PageMgmt.Encrypted(dt1.Rows[0]["LDF_FTPUserId"].ToString()) + "&password=" + PageMgmt.Encrypted(dt1.Rows[0]["LDF_FTPPassword"].ToString()) + "&portno=" + PageMgmt.Encrypted(dt1.Rows[0]["LDF_FTPPort"].ToString()) + "&productid=" + PageMgmt.Encrypted(productid.ToString()) + "&serverid=" + PageMgmt.Encrypted(Request.QueryString["seid"].ToString()) + "&codeid=" + PageMgmt.Encrypted(codeid.ToString()) + "");

               
                }
                else if (portid.ToString() == "5")
                {
                    string port = dt1.Rows[0]["FTPportfordefaultIISpath"].ToString();
                    try
                    {
                        delectport(port);
                    }
                    catch
                    {
                    }
                    ExecuteBatFile(port);
                    string strServerstatusmaster = " Insert Into PortSecurityTbl (serverid,portnumber,portopendatetime)  values ('" + Request.QueryString["seid"].ToString() + "','" + port + "','" + DateTime.Now.ToShortDateString() + "')";
                    SqlCommand cmd = new SqlCommand(strServerstatusmaster, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();


                    Response.Redirect("http://" + dt1.Rows[0]["Busiwizsatellitesiteurl"].ToString() + "/" + pagename.ToString() + "?ftpurl=" + PageMgmt.Encrypted(dt1.Rows[0]["FTPforMastercode"].ToString()) + "&userid=" + PageMgmt.Encrypted(dt1.Rows[0]["FTPuseridforDefaultIISpath"].ToString()) + "&password=" + PageMgmt.Encrypted(dt1.Rows[0]["FtpPasswordforDefaultIISpath"].ToString()) + "&portno=" + PageMgmt.Encrypted(dt1.Rows[0]["FTPportfordefaultIISpath"].ToString()) + "&productid=" + PageMgmt.Encrypted(productid.ToString()) + "&serverid=" + PageMgmt.Encrypted(Request.QueryString["seid"].ToString()) + "&codeid=" + PageMgmt.Encrypted(codeid.ToString()) + "");

               
                }
                else if (portid.ToString() == "6")
                {
                    string port = dt1.Rows[0]["FTPportfordefaultIISpath"].ToString();
                    try
                    {
                        delectport(port);
                    }
                    catch
                    {
                    }
                    ExecuteBatFile(port);

                    string strServerstatusmaster = " Insert Into PortSecurityTbl (serverid,portnumber,portopendatetime)  values ('" + Request.QueryString["seid"].ToString() + "','" + port + "','" + DateTime.Now.ToShortDateString() + "')";
                    SqlCommand cmd = new SqlCommand(strServerstatusmaster, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Redirect("http://" + dt1.Rows[0]["Busiwizsatellitesiteurl"].ToString() + "/" + pagename.ToString() + "?ftpurl=" + PageMgmt.Encrypted(dt1.Rows[0]["FTPforMastercode"].ToString()) + "&userid=" + PageMgmt.Encrypted(dt1.Rows[0]["FTPuseridforDefaultIISpath"].ToString()) + "&password=" + PageMgmt.Encrypted(dt1.Rows[0]["FtpPasswordforDefaultIISpath"].ToString()) + "&portno=" + PageMgmt.Encrypted(dt1.Rows[0]["FTPportfordefaultIISpath"].ToString()) + "&productid=" + PageMgmt.Encrypted(productid.ToString()) + "&serverid=" + PageMgmt.Encrypted(Request.QueryString["seid"].ToString()) + "&codeid=" + PageMgmt.Encrypted(codeid.ToString()) + "");

               
                }
                
            }
           // ExecuteBatFile();
        }
    }
    public void ExecuteBatFile(string port)
    {

        string setupname = "netsh";
        string smdstring = "advfirewall firewall  add rule name="+port+"port action=allow protocol=TCP dir=in localport="+port+" ";
        Process p = new Process();
        ProcessStartInfo psi = new ProcessStartInfo();
        //string currentpath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        psi.FileName = setupname;
        psi.Arguments = "" + smdstring + "";
        p.StartInfo = psi;
        p.Start();

        //Process proc = null;

        //string _batDir = Server.MapPath("~\\port\\"); //string.Format(@"E:\");
        //proc = new Process();
        //proc.StartInfo.WorkingDirectory = _batDir;
        //proc.StartInfo.FileName = "21portopen.bat";
        //proc.StartInfo.CreateNoWindow = false;
        //proc.Start();
        //proc.WaitForExit();
        ////ExitCode = proc.ExitCode;
        //proc.Close();
        // MessageBox.Show("Bat file executed...");
    }
    public void delectport(string port)
    {

        string setupname = "netsh";
        string smdstring = "advfirewall firewall delete rule name=" + port + "port";
        Process p = new Process();
        ProcessStartInfo psi = new ProcessStartInfo();
        //string currentpath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        psi.FileName = setupname;
        psi.Arguments = "" + smdstring + "";
        p.StartInfo = psi;
        p.Start();
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