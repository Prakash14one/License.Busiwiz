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

/// <summary>
/// Summary description for PageMgmt
/// </summary>
public class ProductCodeVersion
{
    SqlConnection con;
    SqlConnection con11;
    SqlConnection con1;
    SqlCommand cmd;
    DataTable dt;
    SqlDataAdapter adp;
    SqlDataReader dr;

    public ProductCodeVersion()
	{
        con11 = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ToString());
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);        
	}



    public void Insert_ProductMasterCodeTbl(int ProductVerID, int CodeTypeID, int codeversionnumber, string filename, string physicalpath, string TemporaryPath, Boolean successfully, Boolean CreatedUsingCompliler)
    {
        try
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("ProductMasterCodeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            cmd.Parameters.AddWithValue("@ProductVerID", ProductVerID);
            cmd.Parameters.AddWithValue("@CodeTypeID", CodeTypeID);
            cmd.Parameters.AddWithValue("@codeversionnumber", codeversionnumber);
            cmd.Parameters.AddWithValue("@filename", filename);
            cmd.Parameters.AddWithValue("@physicalpath", physicalpath);
            cmd.Parameters.AddWithValue("@TemporaryPath", TemporaryPath);
            cmd.Parameters.AddWithValue("@Successfullycreated", successfully);
            cmd.Parameters.AddWithValue("@VersionDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedUsingCompliler", CreatedUsingCompliler);
            cmd.ExecuteNonQuery();
          
        }
        catch
        {
           
        }
    }

    public DataTable fillPageName(string compid)
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT PageId,PageName FROM PageMaster left outer join Plan_page_Access on  Plan_page_Access.Page_id=PageMaster.PageId  order by PageName";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }
   
  
    public void insertPagecontrolmaster(int Page_id, String ControlName, int Active, int typeid1)
    {
        
            con.Close();
            con.Open();
            cmd = new SqlCommand("InsertPageControlMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Page_id",Page_id );
            cmd.Parameters.AddWithValue("@ControlName", ControlName);
            cmd.Parameters.AddWithValue("@ControlType_id", typeid1);
            if (Active == 1)
            {
                cmd.Parameters.AddWithValue("@ActiveDeactive", true);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ActiveDeactive", false);
            }
            cmd.ExecuteNonQuery();
            con.Close();
            
       
    }
    public SqlDataReader SelectPageControls(int id,string compid)
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT PageControlMaster.PageControl_id , PageControlMaster.ControlName, PageControlMaster.ActiveDeactive, PageControlMaster.Page_id, PageMaster.PageName,PageControlMaster.ControlType_id FROM PageControlMaster INNER JOIN PageMaster ON PageControlMaster.Page_id = PageMaster.PageId where PageControlMaster.PageControl_id ="+id+"";
        SqlCommand cmdremhr = new SqlCommand(k1, con);
        SqlDataReader dr = cmdremhr.ExecuteReader();        
        return dr;
    }
    
   
   
   
  
///////////////////////Select usrid , name//////////////


   


    public void insertUserRoleMaster(int User_id,int Role_id, int Active)
    {
        con.Close();
        con.Open();
        cmd = new SqlCommand("insertUserRoleMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@User_id", User_id);
        cmd.Parameters.AddWithValue("@Role_id", Role_id);
        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }

   


    public DataTable SelectPageRoleAccess(int id,string compid)
    {
        DataTable dt = new DataTable();
        con.Open();
        string k1;
        k1 = " SELECT [Role_Page_Access].[id],[Role_Page_Access].[Page_id],[Role_Page_Access].[Role_id],[Role_Page_Access].[ActiveDeactive] FROM [Role_Page_Access] left outer join PageMaster on  Role_Page_Access.Page_id=PageMaster.PageId where [Role_id] = " + id + " ";        
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        con.Close();
        adp.Fill(dt);
        DataTable dt2 = new DataTable();
        dt2 = fillPageName(compid);

        if ((dt != null) && (dt.Rows.Count > 0))
        {
            if (dt.Rows.Count != dt2.Rows.Count)
            {
                for (int m = 0; m < dt2.Rows.Count; m++)
                {
                   int page_id;
                   page_id = Int16.Parse(dt2.Rows[m]["PageId"].ToString());
                   int flg = 0;
                   for (int s = 0; s < dt.Rows.Count; s++)
                   {
                       if (page_id == Int16.Parse(dt.Rows[s]["Page_id"].ToString()))
                       {
                           flg = 1;
                           break;
                       }                      
                   }
                   if (flg == 0)
                   {
                       con.Open();
                       cmd = new SqlCommand("insertRolePageAccess", con);
                       cmd.CommandType = CommandType.StoredProcedure;
                       cmd.Parameters.AddWithValue("@Page_id", page_id);
                       cmd.Parameters.AddWithValue("@Role_id", id);
                       cmd.Parameters.AddWithValue("@ActiveDeactive", true);
                       cmd.ExecuteNonQuery();
                       con.Close();
                   }
                }
            }
        }
        else
        {           
            for (int i = 0; i < dt2.Rows.Count; i++)
            {                
                int page_id;
                page_id = Int16.Parse(dt2.Rows[i]["PageId"].ToString());
                con.Open();
                cmd = new SqlCommand("insertRolePageAccess", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Page_id", page_id);
                cmd.Parameters.AddWithValue("@Role_id", id);              
                cmd.Parameters.AddWithValue("@ActiveDeactive", true);
                cmd.ExecuteNonQuery();
                con.Close();
            }           
        }
        return dt;
    }


   

   

    


   


   
   

   
    public DataTable selectRolebypage(int role_id)
    {

        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT  distinct Role_Page_Access.Page_id, PageMaster.PageName FROM Role_Page_Access INNER JOIN PageMaster ON Role_Page_Access.Page_id = PageMaster.PageId  order by PageMaster.PageName";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }

    
    public DataTable getControlnameBypage(int page_id,string compid)
    {


        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT  ControlName, PageControl_id FROM PageControlMaster inner join PageMaster on  PageControlMaster.PageControl_id=PageMaster.PageId WHERE (Page_id = " + page_id + ")";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }

    public DataTable getControlnameBypage1(int page_id)
    {


        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT  ControlName, PageControl_id FROM PageControlMaster inner join PageMaster on  PageControlMaster.PageControl_id=PageMaster.PageId WHERE (Page_id = " + page_id + ")";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }



    public DataTable selectrolepagecontrolAceess(string compid)
    {
        con.Close();
        con.Open();

        string k1;
        k1 = "SELECT Role_Page_Contreol_Access.id, PageMaster.PageName, PageControlMaster.ControlName, RoleMaster.Role_name, Role_Page_Contreol_Access.ActiveDeactive FROM PageControlMaster INNER JOIN Role_Page_Contreol_Access INNER JOIN RoleMaster ON Role_Page_Contreol_Access.Role_id = RoleMaster.Role_id INNER JOIN PageMaster ON Role_Page_Contreol_Access.Page_id = PageMaster.PageId ON PageControlMaster.PageControl_id = Role_Page_Contreol_Access.Page_Control_id";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }

    public void InsertrolepagecontrolAccess(int Role_id, int Page_Control_id, int Page_id, int Active)
    {

        con.Close();
        con.Open();
        cmd = new SqlCommand("InsertrolepagecontrolAccess", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Role_id", Role_id);
        cmd.Parameters.AddWithValue("@Page_Control_id", Page_Control_id);
        cmd.Parameters.AddWithValue("@Page_id", Page_id);
        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }
        

        cmd.ExecuteNonQuery();
        con.Close();


    }



    public void UpdaterolepagecontrolAccess(int Role_id, int Page_Control_id, int Page_id, int Active,int id)
    {

        con.Close();
        con.Open();
        cmd = new SqlCommand("UpdaterolepagecontrolAccess", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Role_id", Role_id);
        cmd.Parameters.AddWithValue("@Page_Control_id", Page_Control_id);
        cmd.Parameters.AddWithValue("@Page_id", Page_id);
        cmd.Parameters.AddWithValue("@id", id);
        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }


        cmd.ExecuteNonQuery();
        con.Close();


    }

    public SqlDataReader selectrolepagecontrolAceess_1(int id)
    {
        con.Close();
        con.Open();
        string k1;
        //k1 = "SELECT Role_Page_Contreol_Access.id, PageMaster.PageName, PageControlMaster.ControlName, RoleMaster.Role_name, Role_Page_Contreol_Access.ActiveDeactive FROM PageControlMaster INNER JOIN Role_Page_Contreol_Access INNER JOIN RoleMaster ON Role_Page_Contreol_Access.Role_id = RoleMaster.Role_id INNER JOIN PageMaster ON Role_Page_Contreol_Access.Page_id = PageMaster.PageId ON PageControlMaster.PageControl_id = Role_Page_Contreol_Access.Page_Control_id ";
        k1 = "SELECT id, ActiveDeactive, Page_Control_id, Page_id, Role_id FROM Role_Page_Contreol_Access where id = " + id;
        SqlCommand cmdremhr = new SqlCommand(k1, con);
        SqlDataReader dr = cmdremhr.ExecuteReader();      
        return dr;
    }
    

    public DataTable selectGriddata_plan()
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT PricePlanId,PricePlanName FROM PricePlanMaster";
        cmd = new SqlCommand(k1, con11);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
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

        return Encrypt(strText, "&%#@?,:*");

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

        return Decrypt(str, "&%#@?,:*");

    }


}
