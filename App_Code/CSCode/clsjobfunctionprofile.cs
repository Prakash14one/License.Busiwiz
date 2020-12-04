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

public class clsjobfunctionprofile
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


    
	public clsjobfunctionprofile()
	{

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

	}


    public DataTable selecet_filljobpro()
    {
        cmd = new SqlCommand("selece_filljobprofie", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }


    public DataTable selecet_filldeprdesg()
    {
        cmd = new SqlCommand("selece_filldeprdesg", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Companyid", SqlDbType.NVarChar).Value = comid;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable selecet_fillcategory()
    {
        cmd = new SqlCommand("selece_fillcategory", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable selecet_fillsubcategory()
    {
        cmd = new SqlCommand("selece_fillsubcategory", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable selecet_filljobfunctiontitle()
    {
        cmd = new SqlCommand("selece_filljobfunctiontitle", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

    public DataTable selecet_filljobfunctiondesc()
    {
        cmd = new SqlCommand("selece_filljobfunctiondesc", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    }

}
