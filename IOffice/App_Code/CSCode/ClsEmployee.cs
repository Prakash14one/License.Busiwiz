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
/// Summary description for ClsEmployee
/// </summary>
/// 

public struct StEmployee
{
    public int EmployeeId;
    public string EmployeeName;
    public string address;
    public string city;
    public string state;
    public string country;
    public string phone;
    public string mobile;
    public string email;
    public int rtypeid;
    public int rutypeid;
    public decimal ctc;
    public decimal otctc;
    public string doj;
    public int supervisorid;
}


public class ClsEmployee
{
	public ClsEmployee()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpEmployeeMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpEmployeeMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }


    //GET DATA FROM DepartmentMASTER TABLE BY EmployeeId[PRIMARY KEY]
    public static DataTable SpEmployeeMasterGetDataById(string EmployeeId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpEmployeeMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StEmployee SpEmployeeMasterGetDataStructById(string EmployeeId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpEmployeeMasterGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StEmployee details = new StEmployee();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.EmployeeId = Convert.ToInt32(dr["EmployeeMasterId"]);
            details.EmployeeName = dr["EmployeeName"].ToString();
            details.address = dr["address"].ToString();
            details.city = dr["city"].ToString();
            details.state = dr["StateId"].ToString();
            details.country = dr["CountryId"].ToString();
            details.phone = dr["ContactNo"].ToString();
            // details.mobile = dr["mobile"].ToString();
            details.email = dr["email"].ToString();
            //details.rtypeid = Convert.ToInt32(dr["rtypeid"]);
            //details.rutypeid = Convert.ToInt32(dr["rutypeid"]);
            //details.ctc = Convert.ToDecimal(dr["ctc"]);
            //details.otctc = Convert.ToDecimal(dr["otctc"]);
            details.doj = dr["DateOfJoin"].ToString();
            if ((dr["SuprviserId"]) != DBNull.Value)
            {
                details.supervisorid = Convert.ToInt32(dr["SuprviserId"]);
            }
            //return object with data
           
        }
        return details;
    }


    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpEmployeeMasterAddData(string employeename,string address,string city,string state,string country,string phone,string mobile,string email,string rtypeid,string rutypeid,string ctc,string otctc,string doj,string supervisorid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpEmployeeMasterAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@employeename";
        param.Value = employeename;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@address";
        param.Value = address;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@city";
        param.Value = city;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@state";
        param.Value = state;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = country;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@phone";
        param.Value = phone;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@mobile";
        param.Value = mobile;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@rtypeid";
        param.Value = rtypeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@rutypeid";
        param.Value = rutypeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ctc";
        param.Value = ctc;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@otctc";
        param.Value = otctc;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@doj";
        param.Value = doj;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@supervisorid";
        param.Value = supervisorid;
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
    public static bool SpEmployeeMasterUpdateData(string EmployeeId, string employeename, string address, string city, string state, string country, string phone, string mobile, string email, string rtypeid, string rutypeid, string ctc, string otctc, string doj,string supervisorid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpEmployeeMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@employeename";
        param.Value = employeename;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@address";
        param.Value = address;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@city";
        param.Value = city;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@state";
        param.Value = state;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = country;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@phone";
        param.Value = phone;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@mobile";
        param.Value = mobile;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@rtypeid";
        param.Value = rtypeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@rutypeid";
        param.Value = rutypeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ctc";
        param.Value = ctc;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@otctc";
        param.Value = otctc;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@doj";
        param.Value = doj;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@supervisorid";
        param.Value = supervisorid;
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

    //DELETE DATA FROM DepartmentMASTER TABLE BY EmployeeId[PRIMARY KEY]
    public static bool SpEmployeeMasterDeleteData(string EmployeeId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpEmployeeMasterDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EmployeeId";
        param.Value = EmployeeId;
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

    public static DataTable SpEmployeeMasterGetDataByRoleId(string RoleId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpEmployeeMasterGetDataByRoleId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@RoleId";
        param.Value = RoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }


}
