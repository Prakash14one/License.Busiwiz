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
/// Summary description for ClsTask
/// </summary>
/// 

public struct StTask
{
    public int taskid;
    public string taskname;
    public int projectid;
    public string estartdate;
    public string eenddate;
    public int eunitsalloted;
    public string astartdate;
    public string aenddate;
    public decimal percentage;
    public string status;
}

public class ClsTask
{
	public ClsTask()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpTaskMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpTaskMasterGetDataWithProjectName()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskMasterGetDataWithProjectName";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY TaskId[PRIMARY KEY]
    public static DataTable SpTaskMasterGetDataById(string TaskId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@TaskId";
        param.Value = TaskId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpTaskMasterGetDataByIdWithEmployee(string TaskId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskMasterGetDataByIdWithEmployee";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@TaskId";
        param.Value = TaskId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpAllTaskGetDataByProjectId(string ProjectId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAllTaskGetDataByProjectId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProjectId";
        param.Value = ProjectId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StTask SpTaskMasterGetDataStructById(string TaskId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskMasterGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@TaskId";
        param.Value = TaskId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StTask details = new StTask();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.taskid = Convert.ToInt32(dr["TaskId"]);
            details.taskname = dr["taskname"].ToString();
            details.projectid = Convert.ToInt32(dr["projectid"]);
            details.estartdate = dr["estartdate"].ToString();
            details.eenddate = dr["eenddate"].ToString();
            details.eunitsalloted = Convert.ToInt32(dr["eunitsalloted"]);
            details.astartdate = dr["astartdate"].ToString();
            details.aenddate = dr["aenddate"].ToString();
            details.percentage = Convert.ToDecimal(dr["percentage"]);
            details.status = dr["status"].ToString();
            
        }
        //return object with data
        return details;
    }


    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpTaskMasterAddData(string taskname,string projectid,string estartdate,string eenddate,string eunitsalloted,string astartdate,string aenddate,string percentage,string status)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskMasterAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@taskname";
        param.Value = taskname;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@projectid";
        param.Value = projectid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@estartdate";
        param.Value = estartdate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@eenddate";
        param.Value = eenddate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@eunitsalloted";
        param.Value = eunitsalloted;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@astartdate";
        param.Value = astartdate;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@aenddate";
        param.Value = aenddate  ;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@percentage";
        param.Value = percentage;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        param.Size = 100;
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

    //UPDATE DATA IN DepartmentMASTER TABLE
    public static bool SpTaskMasterUpdateData(string TaskId, string taskname, string projectid, string estartdate, string eenddate, string eunitsalloted, string astartdate, string aenddate, string percentage, string status)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@TaskId";
        param.Value = TaskId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@taskname";
        param.Value = taskname;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@projectid";
        param.Value = projectid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@estartdate";
        param.Value = estartdate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@eenddate";
        param.Value = eenddate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);


        
        param = comm.CreateParameter();
        param.ParameterName = "@eunitsalloted";
        param.Value = eunitsalloted;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@astartdate";
        param.Value = astartdate;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@aenddate";
        param.Value = aenddate;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@percentage";
        param.Value = percentage;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        param.Size = 100;
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
    public static bool SpTaskMasterUpdateDataForunAllocated(string TaskId, string taskname, string projectid, string estartdate, string eenddate, string eunitsalloted,string percentage, string status)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskMasterUpdateDataForunAllocated";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@TaskId";
        param.Value = TaskId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@taskname";
        param.Value = taskname;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@projectid";
        param.Value = projectid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@estartdate";
        param.Value = estartdate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@eenddate";
        param.Value = eenddate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);



        param = comm.CreateParameter();
        param.ParameterName = "@eunitsalloted";
        param.Value = eunitsalloted;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@percentage";
        param.Value = percentage;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        param.Size = 100;
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

    //DELETE DATA FROM DepartmentMASTER TABLE BY TaskId[PRIMARY KEY]
    public static bool SpTaskMasterDeleteData(string TaskId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTaskMasterDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@TaskId";
        param.Value = TaskId;
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
