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
/// Summary description for ClsUser
/// </summary>
/// 

public struct StUser
{
    public int userid;
    public string username;
    public string password;
    public int roleid;
    public int employeeid;
}

public class ClsUser
{
	public ClsUser()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpUserMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpUserMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY UserId[PRIMARY KEY]
    public static DataTable SpUserMasterGetDataById(string UserId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpUserMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserId";
        param.Value = UserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StUser SpUserMasterGetDataStructById(string UserId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpUserMasterGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserId";
        param.Value = UserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StUser details = new StUser();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.userid = Convert.ToInt32(dr["userid"]);
            details.roleid = Convert.ToInt32(dr["roleid"]);
            details.username = dr["username"].ToString();
            details.password = dr["password"].ToString();
            details.employeeid = Convert.ToInt32(dr["employeeid"]);
        }
        //return object with data
        return details;
    }


    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpUserMasterAddData(string username, string password, string roleid,string employeeid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpUserMasterAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@password";
        param.Value = password;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@roleid";
        param.Value = roleid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@employeeid";
        param.Value = employeeid;
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

    //UPDATE DATA IN DepartmentMASTER TABLE
    public static bool SpUserMasterUpdateData(string userid, string username, string password, string roleid,string employeeid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpUserMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@password";
        param.Value = password;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@roleid";
        param.Value = roleid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@employeeid";
        param.Value = employeeid;
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

    //DELETE DATA FROM DepartmentMASTER TABLE BY UserId[PRIMARY KEY]
    public static bool SpUserMasterDeleteData(string UserId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpUserMasterDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserId";
        param.Value = UserId;
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
