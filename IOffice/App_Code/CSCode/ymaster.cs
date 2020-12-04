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
/// Summary description for ymaster
/// </summary>
public class ymaster
{
    SqlConnection con;
    SqlCommand cmd;
    DataTable dt;
    SqlDataAdapter adp;

	public ymaster()
	{
        con = new SqlConnection(PageConn.connnn);
	}


    public DataTable SelectYMaster(String stgmasterid)
    {
        try
        {
            con.Close();
            cmd = new SqlCommand("YMasterGetDataBy_stgmasterid", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stgmasterid", Int16.Parse(stgmasterid));
            con.Open();
            adp = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

}
