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
/// Summary description for Repot
/// </summary>
public class Repot
{

    SqlConnection con;
    SqlConnection con11;
    SqlConnection con1;
    SqlCommand cmd;
    DataTable dt;
    SqlDataAdapter adp;
    SqlDataReader dr;

	public Repot()
	{


        con11 = new SqlConnection(PageConn.connnn);

        con = new SqlConnection(PageConn.connnn);
        con1 = new SqlConnection(PageConn.connnn);

	}

    public DataTable report_purchasedetails()
    {

        con.Close();
        con.Open();
        cmd = new SqlCommand("Report_Purchase_Detail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@Page_id", Page_id);
        //cmd.Parameters.AddWithValue("@ControlName", ControlName);
        //cmd.Parameters.AddWithValue("@ControlType_id", typeid1);

        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;


    }
}
