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
/// Summary description for ClsWEvaluation
/// </summary>
/// 

public struct StWEvaluation
{
    public int evaluationid;
    public int masterid;
    public int employeeid;
    public string evaluationnote;
    public string date;
    public string statusid;
    public decimal percentage;
    public string Description;
    public string StoreId;
    public string Deptid;
    public string BusId;
    public string Mid;
}

public class ClsWEvaluation
{
	public ClsWEvaluation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpWEvaluationGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWEvaluationGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpWEvaluationGetDataById(string EvaluationId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWEvaluationGetDataById";

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
    public static DataTable SpWEvaluationGetDataByMasterId(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWEvaluationGetDataByMasterId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpWEvaluationGetDataByMasterIdWithEmployeeName(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWEvaluationGetDataByMasterIdWithEmployeeName";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StWEvaluation SpWEvaluationGetDataStructById(string EvaluationId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWEvaluationGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EvaluationId";
        param.Value = EvaluationId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StWEvaluation details = new StWEvaluation();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.evaluationid = Convert.ToInt32(dr["EvaluationId"]);
            details.masterid = Convert.ToInt32(dr["masterid"]);
            details.employeeid = Convert.ToInt32(dr["employeeid"]);
            details.evaluationnote = dr["evaluationnote"].ToString();
            details.date = dr["date"].ToString();
            details.statusid = Convert.ToString(dr["statusid"]);
            details.percentage = Convert.ToDecimal(dr["percentage"]);
            details.Description = dr["Evaluationnote"].ToString();
            
            details.Mid = Convert.ToString(dr["MMasterId"]);
            details.Deptid = Convert.ToString(dr["DepartmentId"]);
            details.StoreId = Convert.ToString(dr["StoreId"]);

            details.BusId = dr["BusinessId"].ToString();
        }
        //return object with data
        return details;
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static int SpWEvaluationAddData(string masterid, string date, string evaluationnote, string statusid, string percentage)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWEvaluationAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
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
       
        comm.Parameters.Add(param);


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
    public static int SpWEvaluationUpdateData(string EvaluationId, string date, string masterid, string evaluationnote, string percentage)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWEvaluationUpdateData";

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
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@evaluationnote";
        param.Value = evaluationnote;
        param.DbType = DbType.String;
       
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
        return result;
    }

    //DELETE DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static bool SpWEvaluationDeleteData(string EvaluationId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpWEvaluationDeleteData";

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
    public static DataTable SpWEvalutionGridfill(String para, string filc)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SpWEvalutionGridfill";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc;
        // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        //cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        //cmd.Parameters["@BusinessID"].Value = BusinessID;
        //cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        //cmd.Parameters["@Whid"].Value = Whid;
        //cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        //cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectOfficeManagerDocumentsforWevaID(string Mevid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentsforWevaID";
        cmd.Parameters.Add(new SqlParameter("@Mevid", SqlDbType.NVarChar));
        cmd.Parameters["@Mevid"].Value = Mevid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable SelectobjActualcostforWMaster(string MissionId)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectobjActualcostforWMaster";
        cmd.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.NVarChar));
        cmd.Parameters["@MasterId"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static int UpdateobjactualcostforWMaster(string MissionId, decimal ActualCost, decimal ShortageExcess, int StatusId)
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UpdateobjactualcostforWMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@masterid", SqlDbType.NVarChar));
        cmd.Parameters["@masterid"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@ActualCost", SqlDbType.Decimal));
        cmd.Parameters["@ActualCost"].Value = ActualCost; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.BigInt));
        cmd.Parameters["@StatusId"].Value = StatusId;

        cmd.Parameters.Add(new SqlParameter("@ShortageExcess", SqlDbType.Decimal));
        cmd.Parameters["@ShortageExcess"].Value = ShortageExcess; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        int I = DatabaseCls1.ExecuteNonQueryep(cmd); //.FillAdapter(cmd);
        return I;
    }

}
