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

/// <summary>
/// Summary description for ifilecabinet
/// </summary>
public class ifilecabinet
{
    SqlConnection con = new SqlConnection(PageConn.connnn);
	public ifilecabinet()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void addUserInMainSite(string comp,string userid,string password)
    {
        string str1 = "select  CompanyId from CompanyMaster where redirect='"+ comp +"' ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        adp1.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            int id = Convert.ToInt32(dt.Rows[0]["CompanyId"]);

            string str2 = "INSERT INTO CompanyUserMaster " +
                         " (CompanyId, Username, Password) " +
                           " VALUES     ('" + id + "' ,'" + userid.ToString() + "','" + password.ToString() + "')";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
        }
        

    }
    public void UpdateUserLoginInMainSite(string comp, string userid, string password)
    {
        string str1 = "select  CompanyId from CompanyMaster where redirect='" + comp + "' ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        adp1.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            int id = Convert.ToInt32(dt.Rows[0]["CompanyId"]);

            string str2 = "UPdate CompanyUserMaster " +
                           " set    Password='" + password.ToString() + "'"+
                           " where CompanyId='" + id + "' and Username='" + userid.ToString() + "'";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
        }


    }
}
