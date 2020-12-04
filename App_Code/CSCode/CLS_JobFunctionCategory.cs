using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CLS_JobFunctionCategory
/// </summary>
public class CLS_JobFunctionCategory
{


    SqlConnection con;
    DataTable dt;
    DataSet ds;
    SqlDataAdapter da;
    SqlCommand cmd;


	public CLS_JobFunctionCategory()
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
    public int Status
    {
        get;
        set;
    }
    public string CategoryName
    {
        get;
        set;
    }

    public string Id
    {
        get;
        set;
    }



    public DataTable cls_fillddl()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_fillddl";
        cmd.Parameters.Add("@comid", SqlDbType.NVarChar).Value = comid;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    
   

    public int cls_jobfunctioncategory1()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_jobfunctioncategory1";
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
        cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = Status;

        cmd.CommandType = CommandType.StoredProcedure;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int i = cmd.ExecuteNonQuery();

        con.Close();

        return (i);

    }


    public DataTable cls_jobfunctioncategory2()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_jobfunctioncategory2";
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
        cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = CategoryName;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataSet cls_jobfunctioncategory3()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_jobfunctioncategory3";

        cmd.Parameters.Add("@comid", SqlDbType.NVarChar).Value = comid;

        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }


    public DataSet cls_jobfunctioncategory4()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_jobfunctioncategory4";
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public DataTable cls_jobfunctioncategory5()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_jobfunctioncategory5";
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
        cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = CategoryName;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public int cls_update_jobfunction()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_update_jobfunction";
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
        cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = CategoryName;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

        cmd.CommandType = CommandType.StoredProcedure;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int i = cmd.ExecuteNonQuery();

        con.Close();

        return (i);

    }

  



}
