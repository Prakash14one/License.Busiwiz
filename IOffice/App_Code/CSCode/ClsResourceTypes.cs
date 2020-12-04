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
/// Summary description for ClsResourceTypes
/// </summary>
public class ClsResourceTypes
{
    public ClsResourceTypes()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpResourceTypesGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpResourceTypesGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY ResourceTypeId[PRIMARY KEY]
    public static DataTable SpResourceTypesGetDataById(string ResourceTypeId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpResourceTypesGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ResourceTypeId";
        param.Value = ResourceTypeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }


    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpResourceTypesAddData(string ResourceType)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpResourceTypesAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ResourceType";
        param.Value = ResourceType;
        param.DbType = DbType.String;
        param.Size = 500;
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
    public static bool SpResourceTypesUpdateData(string ResourceTypeId, string ResourceType)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpResourceTypesUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ResourceTypeId";
        param.Value = ResourceTypeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ResourceType";
        param.Value = ResourceType;
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

    //DELETE DATA FROM DepartmentMASTER TABLE BY ResourceTypeId[PRIMARY KEY]
    public static bool SpResourceTypesDeleteData(string ResourceTypeId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpResourceTypesDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ResourceTypeId";
        param.Value = ResourceTypeId;
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
