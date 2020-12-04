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
/// <summary>
/// Summary description for ClsTaskAllocation
/// </summary>
/// 

public struct StTaskAllocation
{
    public int taskallocationid;
    public int taskid;
    public int employeeid;
    public int eunitsalloted;
    public int unitsused;
    public int otunitsused;
    public string taskallocationdate;
    public string taskreport;
    public string supervisornote;
    public int freeunits;
    public string taskname;
}

public class ClsTaskAllocation
{
	public ClsTaskAllocation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpTaskAllocationGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpAllocationTaskReportDataWithEmployee(string ProjectId)
    {
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAllocationTaskReportDataWithEmployee";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProjectId";
        param.Value = ProjectId ;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }




    //GET DATA FROM DepartmentMASTER TABLE BY TaskInstructionId[PRIMARY KEY]
    public static DataTable SpTaskAllocationGetDataById(string TaskInstructionId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@TaskInstructionId";
        param.Value = TaskInstructionId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY TaskInstructionId[PRIMARY KEY]
    public static DataTable SpTaskAllocationGetDataByEmployeeId(string EmployeeId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationGetDataByEmployeeId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable SpTaskAllocationGetDataByEmployeeIdMonthly(string EmployeeId, string CurrentDate, string EndDate)
    {
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationGetDataByEmployeeIdMonthly";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DbParameter param1 = comm.CreateParameter();
        param1.ParameterName = "@CurrentDate";
        param1.Value = CurrentDate;
        param1.DbType = DbType.DateTime ;
        comm.Parameters.Add(param1);

        DbParameter param2 = comm.CreateParameter();
        param2.ParameterName = "@EndDate";
        param2.Value = EndDate;
        param2.DbType = DbType.DateTime;
        comm.Parameters.Add(param2);



        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable SpTaskAllocationGetDataByEmployeeIdInfo(string EmployeeId, string CurrentDate)
    {
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationGetDataByEmployeeIdInfo";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DbParameter param1 = comm.CreateParameter();
        param1.ParameterName = "@CurrentDate";
        param1.Value = CurrentDate;
        param1.DbType = DbType.DateTime;
        comm.Parameters.Add(param1);  

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }



    //GET DATA FROM DepartmentMASTER TABLE BY TaskInstructionId[PRIMARY KEY]
    public static DataTable SpTaskAllocationGetDataByEmployeeIdWithData(string EmployeeId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationGetDataByEmployeeIdWithData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY TaskInstructionId[PRIMARY KEY]
    public static DataTable SpTaskAllocationGetDataByEmployeeIdToday(string EmployeeId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationGetDataByEmployeeIdToday";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY TaskInstructionId[PRIMARY KEY]
    public static DataTable SpTaskAllocationGetDataByEmployeeIdWithInstruction(string EmployeeId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationGetDataByEmployeeIdWithInstruction";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpUnAllocatedTaskGetDataByProjectId(string ProjectId)
    {
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpUnAllocatedTaskGetDataByProjectId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProjectId";
        param.Value = ProjectId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable SpAllocatedTaskGetDataByProjectId(string ProjectId)
    {
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAllocatedTaskGetDataByProjectId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProjectId";
        param.Value = ProjectId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }



    public static StTaskAllocation SpTaskAllocationGetDataStructById(string TaskAllocationId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@TaskAllocationId";
        param.Value = TaskAllocationId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StTaskAllocation details = new StTaskAllocation();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.taskallocationid = Convert.ToInt32(dr["taskallocationid"]);
            details.taskid = Convert.ToInt32(dr["taskid"]);
            details.employeeid = Convert.ToInt32(dr["employeeid"]);
            details.eunitsalloted = Convert.ToInt32(dr["eunitsalloted"]);
            details.unitsused = Convert.ToInt32(dr["unitsused"]);
            details.otunitsused = Convert.ToInt32(dr["otunitsused"]);
            details.taskallocationdate = dr["taskallocationdate"].ToString();
            details.taskreport = dr["taskreport"].ToString();
            details.supervisornote = dr["supervisornote"].ToString();
            details.freeunits = Convert.ToInt32(dr["freeunits"]);
            details.taskname = dr["taskname"].ToString();
        }
        //return object with data
        return details;
    }

    //DELETE DATA FROM DepartmentMASTER TABLE BY TaskInstructionId[PRIMARY KEY]
    public static bool SpTaskAllocationDeleteData(string TaskAllocationId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@TaskAllocationId";
        param.Value = TaskAllocationId;
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

    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpTaskAllocationAddData(string employeeid,string taskallocationdate)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@employeeid";
        param.Value = employeeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@taskallocationdate";
        param.Value = taskallocationdate;
        param.DbType = DbType.DateTime;
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
    public static bool SpTaskAllocationUpdateData(string TaskAllocationId, string taskid,string unitsused,string otunitsused,string taskreport,string supervisornote)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@TaskAllocationId";
        param.Value = TaskAllocationId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@taskid";
        param.Value = taskid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@unitsused";
        param.Value = unitsused;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@otunitsused";
        param.Value = otunitsused;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@taskreport";
        param.Value = taskreport;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@supervisornote";
        param.Value = supervisornote;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        //try
        //{
        //    // execute the stored procedure
            result = DataAccess.ExecuteNonQuery(comm);
        //}
        //catch
        //{
        //    // log errors if any
        //}
        // result will be 1 in case of success 
        return (result != -1);
    }


    public static bool SpTaskAllocationUpdateDataForReport(string EmployeeId, string CurrentDate, string taskreport, string supervisornote)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationUpdateDataForReport";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Currentdate";
        param.Value = CurrentDate;
        param.DbType = DbType.DateTime ;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@taskreport";
        param.Value = taskreport;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@supervisornote";
        param.Value = supervisornote;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        //try
        //{
        //    // execute the stored procedure
        result = DataAccess.ExecuteNonQuery(comm);
        //}
        //catch
        //{
        //    // log errors if any
        //}
        // result will be 1 in case of success 
        return (result != -1);
    }





    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static string SpTaskAllocationGetMaxDate(string employeeid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationGetMaxDate";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@employeeid";
        param.Value = employeeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        // execute the stored procedure
        return DataAccess.ExecuteScalar(comm);
    }


    public static bool SpTaskAllocationAssignData(string taskid,string employeeid,string taskallocationdate,string unitsalloted)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskAllocationAssignData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@taskid";
        param.Value = taskid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@employeeid";
        param.Value = employeeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@taskallocationdate";
        param.Value = taskallocationdate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@unitsalloted";
        param.Value = unitsalloted;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        //try
        //{
        //    // execute the stored procedure
        result = DataAccess.ExecuteNonQuery(comm);
        //}
        //catch
        //{
        //    // log errors if any
        //}
        // result will be 1 in case of success 
        return (result != -1);
    }

}
