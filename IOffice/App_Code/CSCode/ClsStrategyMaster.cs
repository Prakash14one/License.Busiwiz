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
/// Summary description for ClsStrategyMaster
/// </summary>
/// 

public struct StStrategyMaster
{
    public int masterid;
    public int businessid;
    public int objectivemasterid;
    public int ltgmasterid;
    public int stgmasterid;
    public int ymasterid;
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

public class ClsStrategyMaster
{
	public ClsStrategyMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpStrategyMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpStrategyMasterGetDataById(string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpStrategyMasterGetDataByltgmasterid(string ltgmasterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyMasterGetDataByltgmasterid";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ltgmasterid";
        param.Value = ltgmasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpStrategyMasterGetDataBystgmasterid(string stgmasterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyMasterGetDataBystgmasterid";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@stgmasterid";
        param.Value = stgmasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpStrategyMasterGetDataByymasterid(string ymasterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyMasterGetDataByymasterid";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ymasterid";
        param.Value = ymasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StStrategyMaster SpStrategyMasterGetDataStructById(string MasterId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyMasterGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StStrategyMaster details = new StStrategyMaster();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.masterid = Convert.ToInt32(dr["masterid"]);
            details.businessid = Convert.ToInt32(dr["businessid"]);
            details.objectivemasterid = Convert.ToInt32(dr["objectivemasterid"]);
            details.ltgmasterid = Convert.ToInt32(dr["ltgmasterid"]);
            details.stgmasterid = Convert.ToInt32(dr["stgmasterid"]);
            details.ymasterid = Convert.ToInt32(dr["ymasterid"]);
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


    public static Int32 SpStrategyMasterAddData(string StrategygoalId, string StrategytypeId, string Title,  string BudgetedCost, string Description)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyMasterAddData";

        // create a new parameter

        DbParameter param = comm.CreateParameter();



        param = comm.CreateParameter();
        param.ParameterName = "@StrategygoalId";
        param.Value = StrategygoalId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@StrategytypeId";
        param.Value = StrategytypeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);




        param = comm.CreateParameter();
        param.ParameterName = "@Title";
        param.Value = Title;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@BudgetedCost";
        param.Value = BudgetedCost;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Description";
        param.Value = Description;
        param.DbType = DbType.String;
        param.Size = 80000;
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
    public static bool SpStrategyMasterUpdateData(string MasterId, string Title,  string BudgetedCost, string Description, string StrategytypeId, string StrategygoalId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Title";
        param.Value = Title;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

       

        param = comm.CreateParameter();
        param.ParameterName = "@BudgetedCost";
        param.Value = BudgetedCost;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Description";
        param.Value = Description;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@StrategytypeId";
        param.Value = StrategytypeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@StrategygoalId";
        param.Value = StrategygoalId;
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

    //DELETE DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static bool SpStrategyMasterDeleteData(string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyMasterDeleteData";

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

    public static DataTable SpStrategyMasterGetDataByBusinessId(string businessid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyMasterGetDataByBusinessId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@businessid";
        param.Value = businessid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpStrategyMasterGetDataByStatusId(string statusid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyMasterGetDataByStatusId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@statusid";
        param.Value = statusid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    //mehul 20-8-2012
    public static DataTable SpStrategyMasterGridfillforstrategy(String para, string filc, int flag)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SpStrategyMasterGridfillforstrategy";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag;


        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
}
