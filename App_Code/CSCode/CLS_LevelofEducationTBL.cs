using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for CLS_LevelofEducationTBL
/// </summary>
public class CLS_LevelofEducationTBL
{
    SqlConnection con;
    DataTable dt;
    DataSet ds;
    SqlDataAdapter da;
    SqlCommand cmd;
     

	public CLS_LevelofEducationTBL()
	{
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
	}

    public string comid
    {
        get;
        set;
    }
    public string Whid
    {
        get;
        set;
    }



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
    public string CategoryName
    {
        get;
        set;
    }

    public string SubCategoryName
    {
        get;
        set;
    }

    public string Id
    {
        get;
        set;
    }

    public string JobCategoryId
    {
        get;
        set;
    }

    public DataSet cls_levelof_edu1()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_levelof_edu1";
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public DataSet cls_levelof_edu3()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_levelof_edu3";
        
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public void cls_levelof_edu2()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_levelof_edu2";

        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;

        cmd.CommandType = CommandType.StoredProcedure;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int i = cmd.ExecuteNonQuery();

        con.Close();


    }


    public void cls_levelof_edu4()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_levelof_edu4";

        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

        cmd.CommandType = CommandType.StoredProcedure;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int i = cmd.ExecuteNonQuery();

        con.Close();


    }



}
