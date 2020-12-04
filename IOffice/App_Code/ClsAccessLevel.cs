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
/// Summary description for ClsAccessLevel
/// </summary>
public class ClsAccessLevel
{
	public ClsAccessLevel()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //GET ALL DATA FROM AccessLevelMASTER TABLE
    public static DataTable SpAccessLevelGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccessLevelGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM AccessLevelMASTER TABLE BY accessid[PRIMARY KEY]
    public static DataTable SpAccessLevelGetDataById(string accessid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccessLevelGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@accessid";
        param.Value = accessid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM AccessLevelMASTER TABLE BY accessid[PRIMARY KEY]
    public static DataTable SpAccessLevelGetDataByDeptid(string deptid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccessLevelGetDataByDeptid";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@deptid";
        param.Value = deptid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //DELETE DATA FROM AccessLevelMASTER TABLE BY accessid[PRIMARY KEY]
    public static bool SpAccessLevelDeleteData(string accessid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccessLevelDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@accessid";
        param.Value = accessid;
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

    //ADD DATA IN AccessLevelMASTER TABLE
    public static bool SpAccessLevelAddData(string deptid,string name)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccessLevelAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@deptid";
        param.Value = deptid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
            
            param = comm.CreateParameter();
            param.ParameterName = "@name";
            param.Value = name;
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

    //UPDATE DATA IN AccessLevelMASTER TABLE
    public static bool SpAccessLevelUpdateData(string accessid, string deptid,string name)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccessLevelUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@accessid";
        param.Value = accessid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@deptid";
        param.Value = deptid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@name";
        param.Value = name;
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

    
}
