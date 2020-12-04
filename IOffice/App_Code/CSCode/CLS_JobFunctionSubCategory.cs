using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for JobFunctionSubCategory
/// </summary>
public class CLS_JobFunctionSubCategory
{

    SqlConnection con;
    DataTable dt;
    DataSet ds;
    SqlDataAdapter da;
    SqlCommand cmd;
     
    
	public CLS_JobFunctionSubCategory()
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

    

    public DataSet cls_sp_select()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_select";
        
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public DataSet cls_sp_select_warehouse()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_select_warehouse";
        cmd.Parameters.Add("@comid", SqlDbType.NVarChar).Value = comid;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public DataSet cls_select1()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_select1";
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public DataSet cls_select2()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_select2";
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value =  Name;
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    
        public DataSet cls_select3()
    {
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "sp_select3";
        cmd.Parameters.Add("@comid", SqlDbType.NVarChar).Value = comid;
        
        cmd.CommandType = CommandType.StoredProcedure;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

        public DataSet cls_select4()
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_select4";

            cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = CategoryName;
                       
            
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet cls_select5()
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_select5";

            cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = CategoryName;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;

            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet cls_jobsubcategory1()
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_jobsubcategory1";

            cmd.Parameters.Add("@SubCategoryName", SqlDbType.VarChar).Value = SubCategoryName;
            cmd.Parameters.Add("@JobCategoryId", SqlDbType.Int).Value = JobCategoryId;

            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        public void cls_insert()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_insert";
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = Status;
            cmd.Parameters.Add("@SubCategoryName", SqlDbType.VarChar).Value = SubCategoryName;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            
            cmd.CommandType = CommandType.StoredProcedure;

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            int i = cmd.ExecuteNonQuery();

            con.Close();
                       

        }


        public void cls_delete()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_delete";
            
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

            cmd.CommandType = CommandType.StoredProcedure;

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            int i = cmd.ExecuteNonQuery();

            con.Close();


        }

        public DataTable cls_selectsubcategory()
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_selectsubcategory";

            cmd.Parameters.Add("@SubCategoryName", SqlDbType.VarChar).Value = SubCategoryName;
            cmd.Parameters.Add("@JobCategoryId", SqlDbType.Int).Value = JobCategoryId;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


        public void cls_update()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_update";

            cmd.Parameters.Add("@SubCategoryName", SqlDbType.VarChar).Value = SubCategoryName;
            cmd.Parameters.Add("@JobCategoryId", SqlDbType.Int).Value = JobCategoryId;
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
