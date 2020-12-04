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

public class clspassinggrade
{
    SqlConnection con;
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
    DataTable dt;

    public int ID
    {
        get;
        set;
    }

    public string Name
    {
        get;
        set;
    }
    public string EquivallentGPA
    {
        get;
        set;
    }
    public int Active
    {
        get;
        set;
    }




	public clspassinggrade()
	{
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
	}

    public int executenoninsert_pass()
    {
        cmd = new SqlCommand("inser_passgrade", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
        cmd.Parameters.Add("@EquivallentGPA", SqlDbType.NVarChar).Value = EquivallentGPA;      
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public int executenonupdate_pass()
    {
        cmd = new SqlCommand("updat_passgrade", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
        cmd.Parameters.Add("@EquivallentGPA", SqlDbType.NVarChar).Value = EquivallentGPA;
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public DataTable filgrid_pas()
    {
        cmd = new SqlCommand("sele_passgrade", con);
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable filgrid_pas2()
    {
        cmd = new SqlCommand("sele2_passgrade", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID",SqlDbType.Int).Value = ID;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }
}
