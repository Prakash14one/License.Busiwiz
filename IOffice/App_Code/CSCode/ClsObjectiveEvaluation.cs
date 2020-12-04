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
/// Summary description for ClsObjectiveEvaluation
/// </summary>
/// 

public struct StObjectiveEvaluation
{
    public int evaluationid;
    public int masterid;
    public int employeeid;
    public string evaluationnote;
    public string date;
    public int statusid;
    public string Actualcost;
    public int BusinessId;
    public decimal percentage;




    public int DepartmentId;
    public int storeid;
   
}

public class ClsObjectiveEvaluation
{
	public ClsObjectiveEvaluation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpObjectiveEvaluationGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpObjectiveEvaluationGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpObjectiveEvaluationGetDataById(string EvaluationId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpObjectiveEvaluationGetDataById";

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
    public static DataTable SpObjectiveEvaluationGetDataByMasterId(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpObjectiveEvaluationGetDataByMasterId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpObjectiveEvaluationGetDataByMasterIdWithEmployeeName(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpObjectiveEvaluationGetDataByMasterIdWithEmployeeName";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StObjectiveEvaluation SpObjectiveEvaluationGetDataStructById(string EvaluationId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpObjectiveEvaluationGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EvaluationId";
        param.Value = EvaluationId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StObjectiveEvaluation details = new StObjectiveEvaluation();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            if (dr["DepartmentId"] != DBNull.Value)
                details.DepartmentId = Convert.ToInt32(dr["DepartmentId"]);
            details.storeid = Convert.ToInt32(dr["Whid"]);


            details.evaluationid = Convert.ToInt32(dr["EvaluationId"]);
            details.masterid = Convert.ToInt32(dr["masterid"]);
            if (dr["employeeid"] != DBNull.Value)
                details.employeeid = Convert.ToInt32(dr["employeeid"]);
            details.evaluationnote = dr["evaluationnote"].ToString();
            details.Actualcost =Convert.ToString( dr["Actualcost"]);
            details.date = dr["date"].ToString();
            details.statusid = Convert.ToInt32(dr["statusid"]);
            details.percentage = Convert.ToDecimal(dr["percentage"]);
            if (dr["BusinessID"] != DBNull.Value)
                details.BusinessId = Convert.ToInt32(dr["BusinessID"]);
            
        }
        //return object with data
        return details;
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static int SpObjectiveEvaluationAddData(string masterid, string employeeid, string evaluationnote,string statusid,string percentage)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpObjectiveEvaluationAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@date";
        param.Value = employeeid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@evaluationnote";
        param.Value = evaluationnote;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = "@statusid";
        //param.Value = statusid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@percentage";
        param.Value = percentage;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        comm.Parameters.Add(new SqlParameter("@EvaluationId", SqlDbType.Int));
        comm.Parameters["@EvaluationId"].Direction = ParameterDirection.Output;
        comm.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        comm.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(comm);
        //if (result > 0)
        //{
        result = Convert.ToInt32(comm.Parameters["@EvaluationId"].Value);
        //}
        return (result);
    }

    //UPDATE DATA IN DepartmentMASTER TABLE
    public static int SpObjectiveEvaluationUpdateData(string EvaluationId, string date, string evaluationnote, string statusid, string percentage, string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpObjectiveEvaluationUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EvaluationId";
        param.Value = EvaluationId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@date";
        param.Value = date;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@evaluationnote";
        param.Value = evaluationnote;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = "@statusid";
        //param.Value = statusid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@percentage";
        param.Value = percentage;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
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
        return result;
    }

    //DELETE DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static bool SpObjectiveEvaluationDeleteData(string EvaluationId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpObjectiveEvaluationDeleteData";

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
