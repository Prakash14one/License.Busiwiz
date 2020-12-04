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
/// Summary description for ClsSTGMaster
/// </summary>
/// 

public struct StSTGMaster
{
    public int DepartmentId;
    public int StoreId;


    public int masterid;
    public int businessid;
    public int objectivemasterid;
    public int ltgmasterid;
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

public class ClsSTGMaster
{
	public ClsSTGMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpSTGMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSTGMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpSTGMasterGetDataById(string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSTGMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpSTGMasterGetDataByltgmasterid(string ltgmasterid, string dateid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSTGMasterGetDataByltgmasterid";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ltgmasterid";
        param.Value = ltgmasterid;
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


    public static StSTGMaster SpSTGMasterGetDataStructById(string MasterId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSTGMasterGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StSTGMaster details = new StSTGMaster();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.DepartmentId = Convert.ToInt32(dr["DepartmentId"]);
            details.StoreId = Convert.ToInt32(dr["StoreId"]);
            details.objectivemasterid = Convert.ToInt32(dr["objectivemasterid"]);

            details.masterid = Convert.ToInt32(dr["masterid"]);
            details.businessid = Convert.ToInt32(dr["businessid"]);
          
            details.ltgmasterid = Convert.ToInt32(dr["ltgmasterid"]);
            details.employeeid = Convert.ToInt32(dr["employeeid"]);
            details.title = dr["title"].ToString();
            details.description = dr["description"].ToString();
            details.estartdate = dr["estartdate"].ToString();
            details.eenddate = dr["eenddate"].ToString();
            //details.aenddate = dr["aenddate"].ToString();
            details.budgetedcost = Convert.ToDecimal(dr["budgetedcost"]);
            details.actualcost = Convert.ToDecimal(dr["actualcost"]);
            details.shortageexcess = Convert.ToDecimal(dr["shortageexcess"]);
        }
        //return object with data
        return details;
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static int SpSTGMasterAddData(string ltgmasterid, string title, string description, string estartdate, string eenddate, string budgetedcost)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSTGMasterAddData";

        // create a new parameter
        //DbParameter param = comm.CreateParameter();
        //param.ParameterName = "@businessid";
        //param.Value = businessid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        DbParameter param = comm.CreateParameter();
        //param.ParameterName = "@objectivemasterid";
        //param.Value = objectivemasterid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        param.ParameterName = "@ltgmasterid";
        param.Value = ltgmasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = "@employeeid";
        //param.Value = employeeid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);


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
    public static bool SpSTGMasterUpdateData(string MasterId, string businessid, string objectivemasterid,string ltgmasterid, string employeeid, string title, string description, string estartdate, string eenddate, string budgetedcost)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSTGMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = "@businessid";
        //param.Value = businessid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = "@objectivemasterid";
        //param.Value = objectivemasterid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ltgmasterid";
        param.Value = ltgmasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = "@employeeid";
        //param.Value = employeeid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);


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
    public static bool SpSTGMasterDeleteData(string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSTGMasterDeleteData";

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

    public static DataTable SpSTGMasterGetDataByBusinessId(string businessid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSTGMasterGetDataByBusinessId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@businessid";
        param.Value = businessid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    
    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpSTGMasterGetDataByYear(string year)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSTGMasterGetDataByYear";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@year";
        param.Value = year;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpSTGMasterGetDataByStatusId(string statusid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSTGMasterGetDataByStatusId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@statusid";
        param.Value = statusid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable selectLTGbyobj(String Whid, String Deptid, String BusinessID, String EmployeemasterId)
    {
        SqlCommand cmd = new SqlCommand();
       DataTable dt = new DataTable();
        cmd.CommandText = "selectLTGbyobj";
        cmd.Parameters.Add(new SqlParameter("@Deptid", SqlDbType.NVarChar));
        cmd.Parameters["@Deptid"].Value = Deptid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        cmd.Parameters["@BusinessID"].Value = BusinessID;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable Selectltgddfilter(String Whid, String Deptid, String BusinessID, String EmployeemasterId, int flag, String dateid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "Selectltgddfilter";
        cmd.Parameters.Add(new SqlParameter("@Deptid", SqlDbType.NVarChar));
        cmd.Parameters["@Deptid"].Value = Deptid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        cmd.Parameters["@BusinessID"].Value = BusinessID;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@dateid", SqlDbType.NVarChar));
        cmd.Parameters["@dateid"].Value = dateid;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectOfficeManagerDocumentsforSTGID(string StgId)
    {
      SqlCommand  cmd = new SqlCommand();
      DataTable  dt = new DataTable();
      cmd.CommandText = "SelectOfficeManagerDocumentsforSTGID";
      cmd.Parameters.Add(new SqlParameter("@StgId", SqlDbType.NVarChar));
      cmd.Parameters["@StgId"].Value = StgId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable SelectOfficedocwithstgid(string StgId)
    {
        SqlCommand  cmd = new SqlCommand();
       DataTable  dt = new DataTable();
         cmd.CommandText = "SelectOfficedocwithstgid";
        cmd.Parameters.Add(new SqlParameter("@StgId", SqlDbType.NVarChar));
        cmd.Parameters["@StgId"].Value = StgId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable SelectOfficeManagerDocumentsforSTGDetailID(string STGDetail)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentsforSTGDetailID";
        cmd.Parameters.Add(new SqlParameter("@STGDetail", SqlDbType.NVarChar));
        cmd.Parameters["@STGDetail"].Value = STGDetail; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public static DataTable SelectOfficeManagerDocumentsforSTGEvaID(string STGeva)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentsforSTGEvaID";
        cmd.Parameters.Add(new SqlParameter("@STGeva", SqlDbType.NVarChar));
        cmd.Parameters["@STGeva"].Value = STGeva; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

}
