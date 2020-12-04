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
/// Summary description for ClsTacticMaster
/// </summary>
/// 

public struct StTacticMaster
{
    public int masterid;
    public int businessid;
    public int strategymasterid;
    public int mmasterid;
    public int wmasterid;
    public int employeeid;
    public string title;
    public string description;
    public decimal budgetedcost;
    public decimal actualcost;
    public decimal shortageexcess;

}

public class ClsTacticMaster
{
	public ClsTacticMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpTacticMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTacticMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpTacticMasterGetDataById(string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTacticMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpTacticMasterGetDataByStrategymasterId(string strategymasterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTacticMasterGetDataByStrategymasterId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@strategymasterid";
        param.Value = strategymasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpTacticMasterGetDataByMmasterid(string mmasterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTacticMasterGetDataByMmasterid";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@mmasterid";
        param.Value = mmasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpTacticMasterGetDataByWmasterid(string wmasterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTacticMasterGetDataByWmasterid";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@wmasterid";
        param.Value = wmasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpTacticMasterGetDataStructById(string MasterId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTacticMasterGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        ////create object of structure
        //StTacticMaster details = new StTacticMaster();

        ////check there is row in table or not
        //if (table.Rows.Count > 0)
        //{  //create data reader
        //    DataRow dr = table.Rows[0];

        //    //store individually each field into object
        //    details.masterid = Convert.ToInt32(dr["masterid"]);
        //    details.businessid = Convert.ToInt32(dr["businessid"]);
        //    details.strategymasterid = Convert.ToInt32(dr["strategymasterid"]);
        //    details.mmasterid = Convert.ToInt32(dr["mmasterid"]);
        //    details.wmasterid = Convert.ToInt32(dr["wmasterid"]);
        //    details.employeeid = Convert.ToInt32(dr["employeeid"]);
        //    details.title = dr["title"].ToString();
        //    details.description = dr["description"].ToString();
        //    details.budgetedcost = Convert.ToDecimal(dr["budgetedcost"]);
        //    details.actualcost = Convert.ToDecimal(dr["actualcost"]);
        //    details.shortageexcess = Convert.ToDecimal(dr["shortageexcess"]);
        //}
        ////return object with data
        //return details;
        return table;
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static int SpTacticMasterAddData(string strategymasterid,string title, string description, string budgetedcost)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTacticMasterAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
       
        param.ParameterName = "@strategymasterid";
        param.Value = strategymasterid;
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
    public static int SpTacticMasterUpdateData(string MasterId, string strategymasterid, string title, string description, string budgetedcost)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTacticMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@strategymasterid";
        param.Value = strategymasterid;
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
        return result;
    }

    //DELETE DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static bool SpTacticMasterDeleteData(string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTacticMasterDeleteData";

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

    public static DataTable SpTacticMasterGetDataByBusinessId(string businessid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTacticMasterGetDataByBusinessId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@businessid";
        param.Value = businessid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpTacticMasterGetDataByStatusId(string statusid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpTacticMasterGetDataByStatusId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@statusid";
        param.Value = statusid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable SelectOfficeManagerDocumentsfortecticmID(string StgId)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentsfortecticmID";
        cmd.Parameters.Add(new SqlParameter("@StgId", SqlDbType.NVarChar));
        cmd.Parameters["@StgId"].Value = StgId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable SelectTecticmasterGridfill(String para, string filc, int flag)
    {
      SqlCommand  cmd = new SqlCommand();
      DataTable  dt = new DataTable();
      cmd.CommandText = "SelectTecticmasterGridfill";
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
