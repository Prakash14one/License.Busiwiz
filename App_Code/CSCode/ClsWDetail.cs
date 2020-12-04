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
using System.Data.Common;
using System.Data.SqlClient;
/// <summary>
/// Summary description for ClsWDetail
/// </summary>
/// 

public struct StWDetail
{
    public int bid;
    public int week;
    public int Mid;
    public int wid;
    public int detailid;
    public int masterid;
    public int employeeid;
    public string detail;
    public string date;
    public int userid;
    public string StoreId;
    public string Deptid;
    public string BusId;
}


public class ClsWDetail
{
	public ClsWDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpWDetailGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWDetailGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpWDetailGetDataById(string detailid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWDetailGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@detailid";
        param.Value = detailid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpWDetailGetDataByMasterId(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWDetailGetDataByMasterId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpWDetailGetDataByMasterIdWithEmployeeName(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWDetailGetDataByMasterIdWithEmployeeName";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StWDetail SpWDetailGetDataStructById(string detailid)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWDetailGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@detailid";
        param.Value = detailid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StWDetail details = new StWDetail();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
          
            details.Mid = Convert.ToInt32(dr["MMasterId"]);
           

            details.detailid = Convert.ToInt32(dr["detailid"]);
            details.masterid = Convert.ToInt32(dr["masterid"]);
            details.employeeid = Convert.ToInt32(dr["employeeid"]);
            details.detail = dr["detail"].ToString();
            details.date = dr["date"].ToString();
            details.userid = Convert.ToInt32(dr["userid"]);
            details.Deptid = Convert.ToString(dr["DepartmentId"]);
            details.StoreId = Convert.ToString(dr["StoreId"]);

            details.BusId = dr["BusinessId"].ToString();
          
        }
        //return object with data
        return details;
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static int SpWDetailAddData(string masterid,string date, string detail, string userid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWDetailAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@date";
        param.Value = date;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@detail";
        param.Value = detail;
        param.DbType = DbType.String;
      
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        comm.Parameters.Add(new SqlParameter("@DetailId", SqlDbType.Int));
        comm.Parameters["@DetailId"].Direction = ParameterDirection.Output;
        comm.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        comm.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(comm);
        //if (result > 0)
        //{
        result = Convert.ToInt32(comm.Parameters["@DetailId"].Value);
        //}

        return (result);
    }

    //UPDATE DATA IN DepartmentMASTER TABLE
    public static int SpWDetailUpdateData(string detailid, string date,string masterid, string detail)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWDetailUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@detailid";
        param.Value = detailid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@date";
        param.Value = date;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@detail";
        param.Value = detail;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);


        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = DataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // log errors if any
        }
        // result will be 1 in case of success 
        return result;
    }

    //DELETE DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static bool SpWDetailDeleteData(string detailid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWDetailDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@detailid";
        param.Value = detailid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        //try
        //{
        // execute the stored procedure
        result = DataAccess.ExecuteNonQuery(comm);
        //}
        //catch
        //{
        //    // log errors if any
        //}
        // result will be 1 in case of success
        return (result != -1);
    }
    public static DataTable SpWMasterGridfill(String para, string filc)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SpWMasterGridfill";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc;
        // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        //cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        //cmd.Parameters["@BusinessID"].Value = BusinessID;
        //cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        //cmd.Parameters["@Whid"].Value = Whid;
        //cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        //cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectMddfilter(String Whid, String Deptid, String BusinessID, String EmployeemasterId, int flag)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectMddfilter";
        cmd.Parameters.Add(new SqlParameter("@Deptid", SqlDbType.NVarChar));
        cmd.Parameters["@Deptid"].Value = Deptid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        cmd.Parameters["@BusinessID"].Value = BusinessID;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectOfficeManagerDocumentsforWID(string MgId)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentsforWID";
        cmd.Parameters.Add(new SqlParameter("@MgId", SqlDbType.NVarChar));
        cmd.Parameters["@MgId"].Value = MgId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable SelctWeekonmonth(string Year)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelctWeekonmonth";
        cmd.Parameters.Add(new SqlParameter("@Year", SqlDbType.NVarChar));
        cmd.Parameters["@Year"].Value = Year; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable SpWeekMasterGetDataBymmasterid(string Mmasterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWeekMasterGetDataBymmasterid";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Mmasterid";
        param.Value = Mmasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable SpWDetailGridfill(String para, string filc)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SpWDetailGridfill";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc;
        // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        //cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        //cmd.Parameters["@BusinessID"].Value = BusinessID;
        //cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        //cmd.Parameters["@Whid"].Value = Whid;
        //cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        //cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectOfficeManagerDocumentsforWDetailID(string MgId)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentsforWDetailID";
        cmd.Parameters.Add(new SqlParameter("@MgId", SqlDbType.NVarChar));
        cmd.Parameters["@MgId"].Value = MgId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
}
