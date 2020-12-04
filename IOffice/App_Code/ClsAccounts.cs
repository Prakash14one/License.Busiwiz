using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
/// <summary>
/// Summary description for ClsAccount
/// </summary>
/// 

public struct StAccounts
{
    public int AccountId;
    public int ClassId;
    public int GroupId;
    public string AccountName;
    public string Description;
    public decimal Balance;
    public string Date;
}

public class ClsAccounts
{
	public ClsAccounts()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpAccountMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccountMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY accountid[PRIMARY KEY]
    public static DataTable SpAccountMasterGetDataById(string accountid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccountMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@accountid";
        param.Value = accountid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StAccounts SpAccountMasterGetDataStructById(string AccountId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccountMasterGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@AccountId";
        param.Value = AccountId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StAccounts details = new StAccounts();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.AccountId = Convert.ToInt32(dr["AccountnameId"]);
            details.ClassId = Convert.ToInt32(dr["ClassId"]);
            details.GroupId = Convert.ToInt32(dr["GroupId"]);
            details.AccountName = dr["Name"].ToString();
            details.Description = dr["Description"].ToString();
            details.Balance = Convert.ToDecimal(dr["amount"]);
            details.Date = dr["date"].ToString();
        }
        //return object with data
        return details;
    }

    //GET DATA FROM DepartmentMASTER TABLE BY accountid[PRIMARY KEY]
    public static DataTable SpAccountMasterGetDataByClassIdGroupId(string classid,string groupid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccountMasterGetDataByClassIdGroupId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@classid";
        param.Value = classid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@groupid";
        param.Value = groupid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpAccountMasterAddData(string classid,string groupid,string accountname,string description,string balance)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccountMasterAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@classid";
        param.Value = classid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@groupid";
        param.Value = groupid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@accountname";
        param.Value = accountname;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@description";
        param.Value = description;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@balance";
        param.Value = balance;
        param.DbType = DbType.Decimal;
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
    public static bool SpAccountMasterUpdateData(string accountid, string accountname,string description,string balance)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccountMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@accountid";
        param.Value = accountid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@accountname";
        param.Value = accountname;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@description";
        param.Value = description;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@balance";
        param.Value = balance;
        param.DbType = DbType.Decimal;
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

    //DELETE DATA FROM DepartmentMASTER TABLE BY accountid[PRIMARY KEY]
    public static bool SpAccountMasterDeleteData(string accountid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpAccountMasterDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@accountid";
        param.Value = accountid;
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
