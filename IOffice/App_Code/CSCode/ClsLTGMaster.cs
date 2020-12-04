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
/// Summary description for ClsLTGMaster
/// </summary>
/// 

public struct StLTGMaster
{
    public int deptid;
    public int WId;
    public int masterid;
    public int businessid;
    public int objectivemasterid;
    public int employeeid;
    public string title;
    public string description;
    public string estartdate;
    public string eenddate;
    public string aenddate;
    public decimal budgetedcost;
    public decimal actualcost;
    public decimal shortageexcess;

}

public class ClsLTGMaster
{
	public ClsLTGMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpLTGMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpLTGMasterGetDataById(string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpLTGMasterGetDataByobjectivemasterid(string objectivemasterid, string dateid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGMasterGetDataByobjectivemasterid";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@objectivemasterid";
        param.Value = objectivemasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@dateid";
        param.Value = dateid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }


    public static StLTGMaster SpLTGMasterGetDataStructById(string MasterId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGMasterGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StLTGMaster details = new StLTGMaster();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.masterid = Convert.ToInt32(dr["masterid"]);
            details.businessid = Convert.ToInt32(dr["businessid"]);
            details.objectivemasterid = Convert.ToInt32(dr["objectivemasterid"]);
            details.employeeid = Convert.ToInt32(dr["employeeid"]);
            details.title = dr["title"].ToString();
            details.description = dr["description"].ToString();
            details.estartdate = dr["estartdate"].ToString();
            details.eenddate = dr["eenddate"].ToString();
            details.aenddate = dr["aenddate"].ToString();
            details.budgetedcost = Convert.ToDecimal(dr["budgetedcost"]);
            details.actualcost = Convert.ToDecimal(dr["actualcost"]);
            details.shortageexcess = Convert.ToDecimal(dr["shortageexcess"]);
        }
        //return object with data
        return details;
    }

    //ADD DATA IN DepartmentMASTER TABLE
    //public static Int32 SpLTGMasterAddData(string businessid, string objectivemasterid, string employeeid, string title, string description, string estartdate, string eenddate, string budgetedcost)
     public static Int32 SpLTGMasterAddData( string objectivemasterid,  string title, string description, string estartdate, string eenddate, string budgetedcost)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGMasterAddData";

        // create a new parameter

        DbParameter param = comm.CreateParameter();

        

        param = comm.CreateParameter();
        param.ParameterName = "@objectivemasterid";
        param.Value = objectivemasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

       


        param = comm.CreateParameter();
        param.ParameterName = "@title";
        param.Value = title;
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
        param.ParameterName = "@budgetedcost";
        param.Value = budgetedcost;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

       
        comm.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.Int));
        comm.Parameters["@MasterId"].Direction = ParameterDirection.Output;
        comm.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        comm.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(comm);
        //if (result > 0)
        //{
        result = Convert.ToInt32(comm.Parameters["@MasterId"].Value);
        //}

        return (result);
    }

    //UPDATE DATA IN DepartmentMASTER TABLE
    public static bool SpLTGMasterUpdateData(string MasterId, string objectivemasterid,  string title, string description, string estartdate, string eenddate, string budgetedcost)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@objectivemasterid";
        param.Value = objectivemasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

       


        param = comm.CreateParameter();
        param.ParameterName = "@title";
        param.Value = title;
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
        param.ParameterName = "@budgetedcost";
        param.Value = budgetedcost;
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

    //DELETE DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static bool SpLTGMasterDeleteData(string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGMasterDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
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

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpLTGMasterGetDataByYear(string year)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGMasterGetDataByYear";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@year";
        param.Value = year;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpLTGMasterGetDataByStatusId(string statusid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGMasterGetDataByStatusId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@statusid";
        param.Value = statusid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpLTGMasterGetDataByBusinessId(string businessid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGMasterGetDataByBusinessId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@businessid";
        param.Value = businessid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpLTGMasterGetDataByBusinessIdCom(string businessid,string comid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGMasterGetDataByBusinessIdCom";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@businessid";
        param.Value = businessid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        DbParameter param1 = comm.CreateParameter();
        param.ParameterName = "@comid";
        param.Value = comid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param1);
        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
}
