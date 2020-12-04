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
/// Summary description for ClsBusiness
/// </summary>
/// 

public struct StBusiness
{
    public int BusinessId;
    //public int EmployeeId;
    public string BusinessName;
}

public class ClsBusiness
{
	public ClsBusiness()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpBusinessMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpBusinessMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable SpBusinessMasterGetDataByAllocation(int EmployeeId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpBusinessMasterGetDataByAllocate";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    public static int getEmployeeIdByUserId(int userid)
    {
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpGetEmployeeIdByUseId";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserId";
        param.Value = userid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable dt = new DataTable();
        dt = DataAccess.ExecuteSelectCommand(comm);
        return Convert.ToInt32(dt.Rows[0]["EmployeeId"]);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY BusinessId[PRIMARY KEY]

    public static DataTable spProjectMasterGetDataByBusinessId(String BusinessId)
    {
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectMasterGetDataByBusinessId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BusinessId";
        param.Value = BusinessId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    
    }

    public static DataTable SpAllocationTaskReportData(string ProjectId)
    {
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAllocationTaskReportData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProjectId";
        param.Value = ProjectId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable SpProjectMasterGetDataByBusinessAndStatusId(String BusinessId , String Status)
    {
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectMasterGetDataByBusinessAndStatusId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BusinessId";
        param.Value = BusinessId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DbParameter param1 = comm.CreateParameter();
        param1.ParameterName = "@Status";
        param1.Value = Status;
        param1.DbType = DbType.String ;
        comm.Parameters.Add(param1);



        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);

    }

    
    
    public static DataTable SpBusinessMasterGetDataById(string BusinessId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpBusinessMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BusinessId";
        param.Value = BusinessId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StBusiness SpBusinessMasterGetDataStructById(string BusinessId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpBusinessMasterGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BusinessId";
        param.Value = BusinessId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StBusiness details = new StBusiness();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.BusinessId = Convert.ToInt32(dr["BusinessId"]);
            //details.EmployeeId = Convert.ToInt32(dr["EmployeeName"]);
            details.BusinessName = dr["BusinessName"].ToString();
        }
        //return object with data
        return details;
    }


    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpBusinessMasterAddData(string businessname)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpBusinessMasterAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@businessname";
        param.Value = businessname;
        param.DbType = DbType.String;
        param.Size = 100;
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
        return (result != -1);
    }

    //UPDATE DATA IN DepartmentMASTER TABLE
    public static bool SpBusinessMasterUpdateData(string BusinessId, string businessname)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpBusinessMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BusinessId";
        param.Value = BusinessId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@businessname";
        param.Value = businessname;
        param.DbType = DbType.String;
        param.Size = 100;
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
        return (result != -1);
    }

    //DELETE DATA FROM DepartmentMASTER TABLE BY BusinessId[PRIMARY KEY]
    public static bool SpBusinessMasterDeleteData(string BusinessId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpBusinessMasterDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BusinessId";
        param.Value = BusinessId;
        param.DbType = DbType.Int32;
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
        return (result != -1);
    }
    public static bool spInsertBusinessEmployee(int BusinessId, int RoleId, int EmployeeId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpBusinessEmployeeAddDate";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BusinessId";
        param.Value = BusinessId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DbParameter param1 = comm.CreateParameter();
        param1.ParameterName = "@RoleId";
        param1.Value = RoleId;
        param1.DbType = DbType.Int32;
        comm.Parameters.Add(param1);

        DbParameter param2 = comm.CreateParameter();
        param2.ParameterName = "@EmployeeId";
        param2.Value = EmployeeId;
        param2.DbType = DbType.Int32;
        comm.Parameters.Add(param2);

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
        return (result != -1);

    }
    public static bool spDeleteBusinessEmployee(int BusinessId, int RoleId, int EmployeeId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpBusinessEmployeeDeleteDate";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BusinessId";
        param.Value = BusinessId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DbParameter param1 = comm.CreateParameter();
        param1.ParameterName = "@RoleId";
        param1.Value = RoleId;
        param1.DbType = DbType.Int32;
        comm.Parameters.Add(param1);

        DbParameter param2 = comm.CreateParameter();
        param2.ParameterName = "@EmployeeId";
        param2.Value = EmployeeId;
        param2.DbType = DbType.Int32;
        comm.Parameters.Add(param2);

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
        return (result != -1);

    }

    public  static DataTable GetBusinessEmployeeData(int BusinessId)
    {
        DbCommand comm = DataAccess.CreateCommand();
        comm.CommandText = "SpBusinessEmployeeGetDataByBusiness";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BusinessId";
        param.Value = BusinessId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable SpGetEmployeeBySupervisor(int SupervisorId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpGetEmployeeBySupervisor";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@SupervisorId";
        param.Value = SupervisorId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    
}
