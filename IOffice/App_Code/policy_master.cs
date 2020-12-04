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
using System.Data.Common;
/// <summary>
/// Summary description for policy_master
/// </summary>
public class policy_master
{
    SqlConnection con;
    SqlCommand cmd;
    DataTable dt;
    SqlDataAdapter adp;

	public policy_master()
	{
        con = new SqlConnection(PageConn.connnn);
	}

    public void InsertNewPolicy(String PolicyName, int Dept_Id, int categary_Id, int Company_id, int Resposible_Emp_Id, int Responsible_Desig_Id, string Active, DateTime Start_date, DateTime End_date, string Version)
    {

        try
        {
            con.Close();
            cmd = new SqlCommand("InsertNewPolicy", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PolicyName", PolicyName);
            cmd.Parameters.AddWithValue("@Dept_Id", Dept_Id);
            cmd.Parameters.AddWithValue("@categary_Id", categary_Id);
            cmd.Parameters.AddWithValue("@Company_id", Company_id);
            cmd.Parameters.AddWithValue("@Resposible_Emp_Id", Resposible_Emp_Id);
            cmd.Parameters.AddWithValue("@Responsible_Desig_Id", Responsible_Desig_Id);
            cmd.Parameters.AddWithValue("@Active", Active);
            cmd.Parameters.AddWithValue("@Start_date", Start_date);
            cmd.Parameters.AddWithValue("@End_date", End_date);
            cmd.Parameters.AddWithValue("@Version", Version);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();



            return;
        }
        catch (Exception ex)
        {
            return;
        }
    }
}
