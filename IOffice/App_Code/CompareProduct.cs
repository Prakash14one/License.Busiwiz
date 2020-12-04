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

public class CompareProduct
{
   // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
   // SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString1"].ConnectionString);
   // SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["busiwizclient"].ConnectionString);
    SqlConnection con;
    SqlConnection connect;
    public CompareProduct()
	{
		
	}
    public string encryptstrring(string strText)
    {
        return Encrypt(strText, "&%#@?,:*");
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
    //
    public string decryptstring(string str)
    {
        return Decrypt(str, "&%#@?,:*");
    }
    public Boolean RestrictProduct()
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        string str = "SELECT MP, CID, PID, V FROM Lmaster ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        string str1 = "Select MAX(InventoryMasterId) From InventoryMaster";
        SqlDataAdapter da1 = new SqlDataAdapter(str1, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt.Rows[0][0].ToString() != "")
        {
            if (dt1.Rows[0][0].ToString() != "")
            {
                int LMastlimit = Convert.ToInt32(decryptstring(dt.Rows[0][0].ToString()));
                if (dt.Rows[0][0].ToString() != "Unlimited")
                {
                    // int LMastlimit = Convert.ToInt32(decryptstring(dt.Rows[0][0].ToString()));
                    //int LMastlimit = Convert.ToInt16(dt.Rows[0][0].ToString());

                    //string cid = decryptstring(dtsel.Rows[0]["CID"].ToString());
                    //int LMastlimit = Convert.ToInt16(dt.Rows[0][0].ToString());
                    int InvMasterVal = Convert.ToInt16(dt1.Rows[0][0].ToString());

                    if (InvMasterVal <= LMastlimit)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    public string PrdMsg(string compid)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        connect = PageConn.licenseconn();
        string strc = "SELECT CompanyId from CompanyMaster where CompanyLoginId='" + compid + "'";
        SqlCommand dac = new SqlCommand(strc, connect);
        connect.Open();
        object enccompid = dac.ExecuteScalar();
        connect.Close();
        enccompid = encryptstrring(enccompid.ToString());
        //string str = "SELECT MP, CID, PID, V FROM Lmaster where CID='" + enccompid + "'";
     
        //SqlDataAdapter da = new SqlDataAdapter(str, conn);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //string str1 = "Select MAX(InventoryMasterId) FROM InventoryMaster inner join InventoruSubSubCategory on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId INNER JOIN InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryCategoryMaster.compid='" + compid + "'";
        //SqlDataAdapter da1 = new SqlDataAdapter(str1, con);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);
        //if (dt.Rows[0][0].ToString() != "")
        //{
        //    if (dt1.Rows[0][0].ToString() != "")
        //    {
        //        if (dt.Rows[0][0].ToString() != "Unlimited")
        //        {
        //            // int LMastlimit = Convert.ToInt16(dt.Rows[0][0].ToString());
        //            int LMastlimit = Convert.ToInt32(decryptstring(dt.Rows[0][0].ToString()));
        //            LMastlimit = LMastlimit - 10;
        //            int InvMasterVal = Convert.ToInt16(dt1.Rows[0][0].ToString());

        //            if (InvMasterVal >= LMastlimit)
        //            {
        //                return "Your Inventory Limit is : " + (LMastlimit + 10) + "Now !! You can enter " + ((LMastlimit + 10) - InvMasterVal) + " products.";
        //            }
        //            else
        //            {
        //                return "";
        //            }
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        //else
        //{
        //    return "";
        //}

        return ""; 
    }
   

   
   
}
