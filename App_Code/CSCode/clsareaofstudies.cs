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

public class clsareaofstudies
{
    SqlConnection con;
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
    DataTable dt;


    public string Name
    {
        get;
        set;
    }
    public int Active
    {
        get;
        set;
    }
    public int ID
    {
        get;
        set;
    }

    public clsareaofstudies()
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
    }

    public int executenoninsert()
    {
        cmd = new SqlCommand("bttnsubmit1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public DataTable filgridi_sel()
    {
        cmd = new SqlCommand("filgrd_selc", con);
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable rowcmod_selc()
    {
        cmd = new SqlCommand("grdrwcmod_selc", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public int executenonupdte()
    {
        cmd = new SqlCommand("bttnupdate1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

}