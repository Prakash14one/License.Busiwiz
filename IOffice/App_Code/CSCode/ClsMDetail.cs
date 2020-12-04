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
/// Summary description for ClsMDetail
/// </summary>
/// 

public struct StMDetail
{
    public int detailid;
    public int masterid;
    public int employeeid;
    public string detail;
    public string date;
    public int userid;
    public string StoreId;
    public string Deptid;
    public string BusId;
    public string Yid;
}

public class ClsMDetail
{
	public ClsMDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpMDetailGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMDetailGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpMDetailGetDataById(string detailid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMDetailGetDataById";

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
    public static DataTable SpMDetailGetDataByMasterId(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMDetailGetDataByMasterId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpMDetailGetDataByMasterIdWithEmployeeName(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMDetailGetDataByMasterIdWithEmployeeName";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StMDetail SpMDetailGetDataStructById(string detailid)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMDetailGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@detailid";
        param.Value = detailid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StMDetail details = new StMDetail();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.detailid = Convert.ToInt32(dr["detailid"]);
            details.masterid = Convert.ToInt32(dr["masterid"]);
        
            details.detail = dr["detail"].ToString();
            details.date = dr["date"].ToString();
            details.Yid = Convert.ToString(dr["YMasterId"]);
            details.Deptid = Convert.ToString(dr["DepartmentId"]);
            details.StoreId = Convert.ToString(dr["StoreId"]);

            details.BusId = dr["BusinessId"].ToString();
            details.employeeid =Convert.ToInt32( dr["EmployeeId"]);
            details.userid = Convert.ToInt32(dr["userid"]);
           
        }
        //return object with data
        return details;
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static int SpMDetailAddData(string masterid,String date, string detail, string userid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMDetailAddData";

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
        param.DbType = DbType.String;
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
    public static int SpMDetailUpdateData(string detailid, string date,String masterid, string detail)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMDetailUpdateData";

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
        param.ParameterName = "masterid";
        param.Value = masterid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);



        param = comm.CreateParameter();
        param.ParameterName = "@detail";
        param.Value = detail;
        param.DbType = DbType.String;
       
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
    public static bool SpMDetailDeleteData(string detailid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMDetailDeleteData";

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
    public static DataTable SpMonthMasterGetDataByymasterid(string ymasterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMonthMasterGetDataByymasterid";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ymasterid";
        param.Value = ymasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable SpMDetailGridfill(String para, string filc)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SpMDetailGridfill";
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
    public static DataTable SelectOfficeManagerDocumentsforMDetailID(string MDetail)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentsforMDetailID";
        cmd.Parameters.Add(new SqlParameter("@MDetail", SqlDbType.NVarChar));
        cmd.Parameters["@MDetail"].Value = MDetail; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

}
