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


/// <summary>
/// Summary description for DBCommands1
/// </summary>
public class DBCommands1
{
    Connection1 con = new Connection1();
   
	public DBCommands1()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable cmdSelect(string str)
    {

        SqlConnection con1 = con.ConnInit();
        

        DataTable dt = new DataTable();
        //string str1 = "";
        SqlCommand cmd = new SqlCommand(str, con1);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd);
        adp1.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            return dt;
        }
        return dt;
    }
    public void FillDDL1(DropDownList ddl,string str3, string idfield, string txfield)
    {
       
        DataTable dt3 = (DataTable)cmdSelect(str3);
        if (dt3.Rows.Count > 0)
        {
            ddl.DataSource = dt3;
            ddl.DataTextField = txfield;
            ddl.DataValueField = idfield;
            ddl.DataBind();
        }
         
    }
    public bool cmdInsUpdateDelete(string str2)
    {
        //SqlConnection con
        SqlConnection con1 = con.ConnInit();
        SqlCommand cmd2 = new SqlCommand(str2, con1);
        

      
        try
        {
            con1.Close();
            con.ConnConnect(con1);
            cmd2.ExecuteNonQuery();
            con.ConnConnect(con1);
             
            return true;
        }
        catch
        {
           return false;
        }
     
    }
    

}
