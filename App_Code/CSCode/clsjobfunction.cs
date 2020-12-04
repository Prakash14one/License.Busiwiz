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


public class clsjobfunction
{

    SqlConnection con;
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
    DataTable dt;

    public string comid
    {
        get;
        set;
    }
    public int Id
    {
        get;
        set;
    }
    public int DeptID
    {
        get;
        set;
    }
    public int Whid
    {
        get;
        set;
    }
    public int DesignationId
    {
        get;
        set;
    }
    public int JobfunctionsubcategoryId
    {
        get;
        set;
    }
    public int JobfunctioncategoryId
    {
        get;
        set;
    }
    public string Jobfunctiontitle
    {
        get;
        set;
    }
    public string Jobfunctiondescription
    {
        get;
        set;
    }
    public int Status
    {
        get;
        set;
    }
    public int JobCategoryId
    {
        get;
        set;
    }


	public clsjobfunction()
	{
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
	}

    public DataTable selec_filliwh()
    {
        cmd = new SqlCommand("seleect_fillwh", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@comid", SqlDbType.NVarChar).Value = comid;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }
    public int executeinsertquery()
    {
        cmd = new SqlCommand("insert_jobfun", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Whid", SqlDbType.Int).Value = Whid;
        cmd.Parameters.Add("@DesignationId", SqlDbType.Int).Value = DesignationId;
        cmd.Parameters.Add("@JobfunctionsubcategoryId", SqlDbType.Int).Value = JobfunctionsubcategoryId;
        cmd.Parameters.Add("@JobfunctioncategoryId", SqlDbType.Int).Value = JobfunctioncategoryId;
        cmd.Parameters.Add("@Jobfunctiontitle", SqlDbType.NVarChar).Value = Jobfunctiontitle;
        cmd.Parameters.Add("@Jobfunctiondescription", SqlDbType.NVarChar).Value = Jobfunctiondescription;
        cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = Status;
        
        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }


    public DataTable selec_jobfunct()
    {
        cmd = new SqlCommand("selec_jobfuncat", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable selec_jobfunsubct()
    {
        cmd = new SqlCommand("selec_jobfunsubcat", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@JobCategoryId", SqlDbType.Int).Value = JobCategoryId;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable selec_deprdes()
    {
        cmd = new SqlCommand("selec_deprdesg", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Companyid", SqlDbType.NVarChar).Value = comid;
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
      //  cmd.Parameters.Add("@DeptID", SqlDbType.Int).Value = DeptID;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable selec_fillinggrdvw()
    {
        cmd = new SqlCommand("seleeect_fillgrid", con);
        cmd.CommandType = CommandType.StoredProcedure;
      //  cmd.Parameters.Add("@comid", SqlDbType.NVarChar).Value = comid;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable selec_grdviewrowcomd()
    {
        cmd = new SqlCommand("rowcomd_select", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public int executeinsertqueryupdate()
    {
        cmd = new SqlCommand("btnupdddate_update", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Whid", SqlDbType.Int).Value = Whid;
        cmd.Parameters.Add("@DesignationId", SqlDbType.Int).Value = DesignationId;
        cmd.Parameters.Add("@JobfunctionsubcategoryId", SqlDbType.Int).Value = JobfunctionsubcategoryId;
        cmd.Parameters.Add("@JobfunctioncategoryId", SqlDbType.Int).Value = JobfunctioncategoryId;
        cmd.Parameters.Add("@Jobfunctiontitle", SqlDbType.NVarChar).Value = Jobfunctiontitle;
        cmd.Parameters.Add("@Jobfunctiondescription", SqlDbType.NVarChar).Value = Jobfunctiondescription;
        cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = Status;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public int executeinsertquerydelet()
    {
        cmd = new SqlCommand("delet_grdvwrowcmd", con);
        cmd.CommandType = CommandType.StoredProcedure;
     
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

}
