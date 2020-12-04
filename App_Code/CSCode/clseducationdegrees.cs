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

public class clseducationdegrees
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
    public int AreaofStudyID
    {
        get;
        set;
    }
    public string DegreeName
    {
        get;
        set;
    }
    public int LevelofEducationTblID
    {
        get;
        set;
    }
    public int Active
    {
        get;
        set;
    }

	public clseducationdegrees()
	{
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
	}

    public DataTable filstudy_se()
    {
        cmd = new SqlCommand("selle_fillstudy", con);
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable filedu_se()
    {
        cmd = new SqlCommand("selle_filledu", con);
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable filgrid_se()
    {
        cmd = new SqlCommand("selle_filgrid", con);
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public int executenoninsert_edu()
    {
        cmd = new SqlCommand("inserte_edu", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AreaofStudyID", SqlDbType.Int).Value = AreaofStudyID;
        cmd.Parameters.Add("@DegreeName", SqlDbType.NVarChar).Value = DegreeName;
        cmd.Parameters.Add("@LevelofEducationTblID", SqlDbType.Int).Value = LevelofEducationTblID;
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;        

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public int executenonupdat_edu()
    {
        cmd = new SqlCommand("update_edu", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
        cmd.Parameters.Add("@AreaofStudyID", SqlDbType.Int).Value = AreaofStudyID;
        cmd.Parameters.Add("@DegreeName", SqlDbType.NVarChar).Value = DegreeName;
        cmd.Parameters.Add("@LevelofEducationTblID", SqlDbType.Int).Value = LevelofEducationTblID;
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;      

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public DataTable educa_se()
    {
        cmd = new SqlCommand("sellc_edu", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }
}
