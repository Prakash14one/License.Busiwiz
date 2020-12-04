using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CLS_SpecialisedSubject
/// </summary>
public class CLS_SpecialisedSubject
{
    SqlConnection con;
    DataTable dt;
    DataSet ds;
    SqlDataAdapter da;
    SqlCommand cmd;
	public CLS_SpecialisedSubject()
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
    public string EmployeeMasterId
    {
        get;
        set;
    }

    public string SubjectName
    {
        get;
        set;
    }
    public string Studyid
    {
        get;
        set;
    }
    public string imp
    {
        get;
        set;
    }

    public DataTable cls_emp()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_emp";
        cmd.Parameters.Add("@EmployeeMasterId", SqlDbType.Int).Value = EmployeeMasterId;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public DataSet cls_sp_warehouse1()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_warehouse1";
        cmd.Parameters.Add("@comid", SqlDbType.NVarChar).Value = comid;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public DataSet cls_areaofstudy1()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_areaofstudy1";
        
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public DataSet cls_SpecialisedSubject1()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_SpecialisedSubject1";
        cmd.Parameters.Add("@SubjectName", SqlDbType.VarChar).Value = SubjectName;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }
    public void cls_SpecialisedSubject2()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_SpecialisedSubject2";

        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
        cmd.Parameters.Add("@comid", SqlDbType.NVarChar).Value = comid;
        cmd.Parameters.Add("@SubjectName", SqlDbType.VarChar).Value = SubjectName;
        cmd.Parameters.Add("@Studyid", SqlDbType.Int).Value = Studyid;

        cmd.CommandType = CommandType.StoredProcedure;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int i = cmd.ExecuteNonQuery();

        con.Close();
    }
    public DataSet cls_SpecialisedSubject3()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_SpecialisedSubject3";
        
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }
    public void cls_SpecialisedSubject4()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_SpecialisedSubject4";
        cmd.Parameters.Add("@i", SqlDbType.Int).Value = imp;
        cmd.CommandType = CommandType.StoredProcedure;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int i = cmd.ExecuteNonQuery();

        con.Close();


    }

    public DataTable cls_SpecialisedSubject5()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_SpecialisedSubject5";
        cmd.Parameters.Add("@SubjectName", SqlDbType.VarChar).Value = SubjectName;
        cmd.Parameters.Add("@Studyid", SqlDbType.Int).Value = Studyid;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }



    public void cls_SpecialisedSubject6()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_SpecialisedSubject6";

        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
        cmd.Parameters.Add("@SubjectName", SqlDbType.VarChar).Value = SubjectName;
        cmd.Parameters.Add("@Studyid", SqlDbType.Int).Value = Studyid;

        cmd.CommandType = CommandType.StoredProcedure;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int i = cmd.ExecuteNonQuery();

        con.Close();


    }


}
