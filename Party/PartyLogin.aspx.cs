using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.Linq;
using System.Xml;
using System.IO;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Common;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using System.Text.RegularExpressions;

using System.Net.Mail;
using System.IO.Compression;

using System.Runtime.InteropServices;
public partial class PartyLogin : System.Web.UI.Page
{
    SqlConnection con;
    public delegate Int32 PDF2ImageCallback(int mode, string msg, IntPtr user_data);

    // The following lines import the PDF2Image functions from pdf2image.dll (via PInvoke).
    [DllImport("pdf2image.dll")]
    public static extern int PDF2ImageInit(string user_name, string company, string license_key);

    [DllImport("pdf2image.dll")]
    public static extern int PDF2ImageRun(string command_str, PDF2ImageCallback funct, IntPtr user_data);

    // Return codes for PDF2ImageRun function.
    const int PDF2IMAGE_OK = 0;  // No errors 
    const int PDF2IMAGE_ERR = 1;  // Unspecified error 
    const int PDF2IMAGE_ERR_BADKEY = 2;  // Bad license key 
    const int PDF2IMAGE_ERR_DIRCREATE = 3;  // Failed to create the output file/directory 
    const int PDF2IMAGE_ERR_READINGPDF = 4;  // Failed to read input document 
    const int PDF2IMAGE_ERR_PASSWORD = 5;  // The password required to open PDF is incorrect 
    const int PDF2IMAGE_ERR_CONVERT = 6;  // A conversion error 

    // You can modify the following lines with your registration information.
    const string username = "John Doe";
    const string company = "My Company";
    const string lic_key = "AGPVCWBRYBCDEPFD";

    // 'mode' identifier passed in PDF2ImageCallback.
    const int PDF2IMAGE_ERROR = 1;    // Show the error message
    const int PDF2IMAGE_MSG = 2;      // Report the message
    const int PDF2IMAGE_GETPASS = 3;  // Get the password
    const int PDF2IMAGE_OUT_FILENAME = 4; //Get the output filenames

    //1.6 Windows API definitions.
    //==============================================================================================================
    [DllImport("kernel32.dll")]
    public static extern int GetTickCount();
    [DllImport("kernel32.dll")]
    public static extern void CopyMemory(Byte[] dest, int Source, Int32 length);

    const int TRUE = 1;
    const int FALSE = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!IsPostBack)
        {


        }



    }


    protected void btnsignin_Click(object sender, EventArgs e)
    {
        Session["Comid"] = txtcompanyid.Text;
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        PageConn.busclient();
        string str = "select party_master.PartyID,PartytTypeMaster.PartType,User_master.State,party_master. Whid,party_master.Account,party_master. id,Login_master.UserID,Login_master.password,Login_master.username from Party_master inner join User_master on User_master.PartyID=Party_master.PartyID  inner join Login_master on Login_master.UserID = User_master.UserID inner join PartytTypeMaster on  PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId  where (Login_master.username = '" + txtuname.Text + "') AND (Login_master.password = '" + ClsEncDesc.Encrypted(txtpass.Text) + "')and(Party_master.id='" + txtcompanyid.Text + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        int recordcount = ds.Tables[0].Rows.Count;
        if (recordcount == 0)
        {
            lblError.Text = "User Name or Password  Incorrect";
            lblError.Visible = true;
           

        }
        else
        {
            Session["sid"] = ds.Tables[0].Rows[0]["State"].ToString();

            lblError.Visible = false;
            Session["userid"] = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"].ToString());
            Session["Username"] = txtuname.Text;
            Session["Whid"] = Convert.ToInt32(ds.Tables[0].Rows[0]["Whid"].ToString());
            Session["PartyID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["PartyID"].ToString());
            Session["CompanyId"] = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
            Session["PartyId"] = Convert.ToInt32(ds.Tables[0].Rows[0]["PartyID"].ToString());
            Session["Account"] = Convert.ToInt32(ds.Tables[0].Rows[0]["Account"].ToString());
            Session["WH"] = Convert.ToInt32(ds.Tables[0].Rows[0]["Whid"].ToString());
            Session["PartType"] = Convert.ToInt32(ds.Tables[0].Rows[0]["Whid"].ToString());
           
            Session["EmployeeId"] = "1850";

            

            Response.Redirect("PartyAfterLogin.aspx");


           

           
        }

        

    }
    private string Decrypt(string strText, string strEncrypt)
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
            throw ex;
        }
    }
    public string decryptstring(string str)
    {
        return Decrypt(str, "&%#@?,:*");
    }
    private string Encrypt(string strtxt, string strtoencrypt)
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
            throw ex;
        }

    }
    public string encryptstrring(string strText)
    {
        return Encrypt(strText, "&%#@?,:*");
    }
    
}
