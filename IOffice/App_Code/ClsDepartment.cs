using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class ClsDepartment
{
    public ClsDepartment()
	{
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpDepartmentGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();
        
        // set the stored procedure name
        comm.CommandText = "SpDepartmentGetData";
        
        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY deptid[PRIMARY KEY]
    public static DataTable SpDepartmentGetDataById(string deptid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDepartmentGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@deptid";
        param.Value = deptid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    
    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpDepartmentAddData(string deptname)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDepartmentAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@deptname";
        param.Value = deptname;
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
    public static bool SpDepartmentUpdateData(string deptid, string deptname)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDepartmentUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@deptid";
        param.Value = deptid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@deptname";
        param.Value = deptname;
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

    //DELETE DATA FROM DepartmentMASTER TABLE BY deptid[PRIMARY KEY]
    public static bool SpDepartmentDeleteData(string deptid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDepartmentDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@deptid";
        param.Value = deptid;
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

}
