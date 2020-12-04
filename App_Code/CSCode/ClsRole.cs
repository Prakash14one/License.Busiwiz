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
/// Summary description for ClsRole
/// </summary>
public class ClsRole
{
	public ClsRole()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpRoleMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpRoleMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY RoleId[PRIMARY KEY]
    public static DataTable SpRoleMasterGetDataById(string RoleId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpRoleMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@RoleId";
        param.Value = RoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }


    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpRoleMasterAddData(string RoleName)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpRoleMasterAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@RoleName";
        param.Value = RoleName;
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
    public static bool SpRoleMasterUpdateData(string RoleId, string RoleName)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpRoleMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@RoleId";
        param.Value = RoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@RoleName";
        param.Value = RoleName;
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

    //DELETE DATA FROM DepartmentMASTER TABLE BY RoleId[PRIMARY KEY]
    public static bool SpRoleMasterDeleteData(string RoleId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpRoleMasterDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@RoleId";
        param.Value = RoleId;
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
