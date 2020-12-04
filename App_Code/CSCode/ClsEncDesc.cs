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
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// Summary description for ClsEncDesc
/// </summary>
public class ClsEncDesc
{
	public ClsEncDesc()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string Decrypted(string str)
    {
       
            return Decrypt(str, PageConn.strEnc);
        
    }
    public static string decryptstring(string str)
    {
        HttpContext.Current.Session["encdata"] = "NotAccept";
        if (HttpContext.Current.Session["encdata"] == "Accept")
        {  
            return Decrypt(str, PageConn.strEnc);
        }
        else
        {
            return str;
        }
    }
    private static string Decrypt(string strText, string strEncrypt)
    {
      
            strEncrypt = "3d70f5cff23ed17ip8H9";
       
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
    private static string Encrypt(string strtxt, string strtoencrypt)
    {
        strtoencrypt = "3d70f5cff23ed17ip8H9";
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
    public static string encryptstrring(string strText)
    {
        HttpContext.Current.Session["encdata"] = "NotAccept";
        if (HttpContext.Current.Session["encdata"] == "Accept")
        {
            return Encrypt(strText, PageConn.strEnc);
        }
        else
        {
            return strText;
        }

       
    }
    public static string Encrypted(string strText)
    {
        
            return Encrypt(strText,PageConn.strEnc);
        
    }



    public static string DecDyn(string str)
    {

        return Decrypt(str, PageConn.strEnc);

    }
    public static string EncDyn(string strText)
    {

        return Encrypt(strText,PageConn.strEnc);

    }

   
  
   
}
