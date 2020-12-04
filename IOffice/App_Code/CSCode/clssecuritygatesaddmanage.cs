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


public class clssecuritygatesaddmanage
{
    SqlConnection con;
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
    DataTable dt;

    public int Id
    {
        get;
        set;
    }

    public string Name
    {
        get;
        set;
    }
    public string Location
    {
        get;
        set;
    }
    public int Active
    {
        get;
        set;
    }
    public int BusinessID
    {
        get;
        set;
    }
    public string comid
    {
        get;
        set;
    }

	public clssecuritygatesaddmanage()
	{
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
	}

    public int executeinsert()
    {
        cmd = new SqlCommand("InsertSecurityGateMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@BusinessID", SqlDbType.Int).Value = BusinessID;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
        cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Location;
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public DataTable filgrid_security()
    {
        cmd = new SqlCommand("SelectSecurityGateMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@comid", SqlDbType.NVarChar).Value = comid;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public int executedelete()
    {
        cmd = new SqlCommand("DeleteSecurityGateMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;       

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public DataTable filgrid_securityedit()
    {
        cmd = new SqlCommand("EditSecurityGateMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;       
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public int executeupdate()
    {
        cmd = new SqlCommand("UpdateSecurityGateMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Id",SqlDbType.Int).Value = Id;
        cmd.Parameters.Add("@BusinessID", SqlDbType.Int).Value = BusinessID;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
        cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Location;
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public DataTable filtergrid_security(char s)
    {
        cmd = new SqlCommand("SelectSecurityGateMasterwithBusinessID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@comid", SqlDbType.NVarChar).Value = comid;
        cmd.Parameters.Add("@BusinessID", SqlDbType.Int).Value = BusinessID;
        cmd.Parameters.Add("@flag", SqlDbType.Char).Value = s;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }
}
