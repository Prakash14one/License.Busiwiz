using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CLS_DesignationJobProfile
/// </summary>
public class CLS_DesignationJobProfile
{

    SqlConnection con;
    DataTable dt;
    DataSet ds;
    SqlDataAdapter da;
    SqlCommand cmd;

	public CLS_DesignationJobProfile()
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


    public string Companyid
    {
        get;
        set;
    }

    public string DesignationId
    {
        get;
        set;
    }

    public DataSet cls_select_designation()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_select_designation";

        cmd.Parameters.Add("@comid", SqlDbType.NVarChar).Value = comid;


        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }



    public DataSet cls_select_designation1()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_select_designation1";

        cmd.Parameters.Add("@Companyid", SqlDbType.NVarChar).Value = Companyid;
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;

        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }



    public DataSet cls_select_designation2()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_select_designation2";
        
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public DataSet cls_select_designation3()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_select_designation3";

        cmd.Parameters.Add("@DesignationId", SqlDbType.Int).Value = DesignationId;
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;

        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }



    public DataSet cls_select_designation4()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_select_designation4";

        cmd.Parameters.Add("@DesignationId", SqlDbType.Int).Value = DesignationId;
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
        cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = Status;

        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }







}
