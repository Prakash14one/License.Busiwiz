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
/// Summary description for ClsLTGEvaluation
/// </summary>
/// 

public struct StLTGEvaluation
{

    public int storeid;
    public int DepartmentId;
    public int BusinessId;
    public int LtgId;

    public int evaluationid;
    public int masterid;
    public int employeeid;
    public string evaluationnote;
    public string date;
    public int statusid;
    public decimal percentage;
}

public class ClsLTGEvaluation
{
	public ClsLTGEvaluation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpLTGEvaluationGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGEvaluationGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpLTGEvaluationGetDataById(string EvaluationId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGEvaluationGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EvaluationId";
        param.Value = EvaluationId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpLTGEvaluationGetDataByMasterId(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGEvaluationGetDataByMasterId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpLTGEvaluationGetDataByMasterIdWithEmployeeName(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGEvaluationGetDataByMasterIdWithEmployeeName";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StLTGEvaluation SpLTGEvaluationGetDataStructById(string EvaluationId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGEvaluationGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EvaluationId";
        param.Value = EvaluationId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StLTGEvaluation details = new StLTGEvaluation();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.DepartmentId = Convert.ToInt32(dr["DepartmentId"]);
            details.BusinessId = Convert.ToInt32(dr["BusinessID"]);
            details.storeid = Convert.ToInt32(dr["Whid"]);
            details.evaluationid = Convert.ToInt32(dr["EvaluationId"]);
            details.masterid = Convert.ToInt32(dr["ObjectiveMasterId"]);
            details.LtgId = Convert.ToInt32(dr["LTGMaterId"]);
            details.employeeid = Convert.ToInt32(dr["employeeid"]);
            details.evaluationnote = dr["evaluationnote"].ToString();
            details.date = dr["date"].ToString();
            details.statusid = Convert.ToInt32(dr["statusid"]);
            details.percentage = Convert.ToDecimal(dr["percentage"]);
        }
        //return object with data
        return details;
    }

    //ADD DATA IN DepartmentMASTER TABLE
    //public static bool SpLTGEvaluationAddData(string masterid, string employeeid, string evaluationnote, string statusid, string percentage)
    //{
    //    // get a configured DbCommand object
    //    DbCommand comm = DataAccess.CreateCommand();

    //    // set the stored procedure name
    //    comm.CommandText = "SpLTGEvaluationAddData";

    //    // create a new parameter
    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = "@masterid";
    //    param.Value = masterid;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = "@employeeid";
    //    param.Value = employeeid;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);


    //    param = comm.CreateParameter();
    //    param.ParameterName = "@evaluationnote";
    //    param.Value = evaluationnote;
    //    param.DbType = DbType.String;
    //    param.Size = 80000;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = "@statusid";
    //    param.Value = statusid;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);


    //    param = comm.CreateParameter();
    //    param.ParameterName = "@percentage";
    //    param.Value = percentage;
    //    param.DbType = DbType.Decimal;
    //    comm.Parameters.Add(param);

    //    // result will represent the number of changed rows
    //    int result = -1;
    //    //try
    //    //{
    //    // execute the stored procedure
    //    result = DataAccess.ExecuteNonQuery(comm);
    //    //}
    //    //catch
    //    //{
    //    //    // log errors if any
    //    //}
    //    // result will be 1 in case of success
    //    return (result != -1);
    //}

    //UPDATE DATA IN DepartmentMASTER TABLE


    public static Int32 SpLTGEvaluationAddData(string masterid, string detail, string date, string Percentage)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGEvaluationAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);




        param = comm.CreateParameter();
        param.ParameterName = "@EvaluationNote";
        param.Value = detail;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Date";
        param.Value = date;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Percentage";
        param.Value = Percentage;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        comm.Parameters.Add(new SqlParameter("@EvaluationId", SqlDbType.Int));
        comm.Parameters["@EvaluationId"].Direction = ParameterDirection.Output;
        comm.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        comm.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(comm);

        result = Convert.ToInt32(comm.Parameters["@EvaluationId"].Value);


        return (result);
    }
    public static bool SpLTGEvaluationUpdateData(string EvaluationId, string MasterId, string evaluationnote, string Date, string percentage)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGEvaluationUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EvaluationId";
        param.Value = EvaluationId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@evaluationnote";
        param.Value = evaluationnote;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Date";
        param.Value = Date;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@percentage";
        param.Value = percentage;
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
    public static bool SpLTGEvaluationDeleteData(string EvaluationId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpLTGEvaluationDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EvaluationId";
        param.Value = EvaluationId;
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
}
