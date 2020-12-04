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
/// Summary description for ClsMMaster
/// </summary>
/// 

public struct StMMaster
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
    public int month;
    public string aenddate;
    public decimal budgetedcost;
    public decimal actualcost;
    public decimal shortageexcess;
    public string Whid;
    public string departmentId;
}


public class ClsMMaster
{
	public ClsMMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpMMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpMMasterGetDataById(string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpMMasterGetDataByymasterid(string ymasterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMMasterGetDataByymasterid";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ymasterid";
        param.Value = ymasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    
    public static DataTable SpMMasterGetDataByBusinessIdbyProjected(string businessid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMMasterGetDataByBusinessIdbyProjected";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@businessid";
        param.Value = businessid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    public static StMMaster SpMMasterGetDataStructById(string MasterId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMMasterGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StMMaster details = new StMMaster();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.masterid = Convert.ToInt32(dr["masterid"]);
            details.businessid = Convert.ToInt32(dr["businessid"]);
            //details.objectivemasterid = Convert.ToInt32(dr["objectivemasterid"]);
            //details.ltgmasterid = Convert.ToInt32(dr["ltgmasterid"]);
            //details.stgmasterid = Convert.ToInt32(dr["stgmasterid"]);
            details.ymasterid = Convert.ToInt32(dr["ymasterid"]);
            details.employeeid = Convert.ToInt32(dr["employeeid"]);
            details.title = dr["title"].ToString();
            details.description = dr["description"].ToString();
            details.month = Convert.ToInt32(dr["month"]);
            details.aenddate = dr["aenddate"].ToString();
            details.budgetedcost = Convert.ToDecimal(dr["budgetedcost"]);
            details.actualcost = Convert.ToDecimal(dr["actualcost"]);
            details.shortageexcess = Convert.ToDecimal(dr["shortageexcess"]);
            details.Whid = Convert.ToString(dr["storeid"]);
            details.departmentId = Convert.ToString(dr["DepartmentId"]);
        }
        //return object with data
        return details;
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static int SpMMasterAddData(string ymasterid, string title, string description, string month, string budgetedcost)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMMasterAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
       

        param = comm.CreateParameter();
        param.ParameterName = "@ymasterid";
        param.Value = ymasterid;
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
        param.ParameterName = "@month";
        param.Value = month;
        param.DbType = DbType.Int32;
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
    public static int SpMMasterUpdateData(String MasterId, string ymasterid, string title, string description, string month, string budgetedcost)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        

        param = comm.CreateParameter();
        param.ParameterName = "@ymasterid";
        param.Value = ymasterid;
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
        param.ParameterName = "@month";
        param.Value = month;
        param.DbType = DbType.Int32;
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
    public static bool SpMMasterDeleteData(string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMMasterDeleteData";

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

    public static DataTable SpMMasterGetDataByBusinessId(string businessid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMMasterGetDataByBusinessId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@businessid";
        param.Value = businessid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //DbParameter param2 = comm.CreateParameter();
        //param2.ParameterName = "@Mid";
        //param2.Value = businessid;
        //param2.DbType = DbType.Int32;
        //comm.Parameters.Add(param2);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpMMasterGetDataByMonth(string month)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMMasterGetDataByMonth";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@month";
        param.Value = month;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpMMasterGetDataByStatusId(string statusid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMMasterGetDataByStatusId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@statusid";
        param.Value = statusid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable SpMMasterGridfill(String para, string filc)
    {
       SqlCommand cmd = new SqlCommand();
     DataTable   dt = new DataTable();
     cmd.CommandText = "SpMMasterGridfill";
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
    public static DataTable Selectyddfilter(String Whid, String Deptid, String BusinessID, String EmployeemasterId, int flag)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "Selectyddfilter";
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
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectOfficeManagerDocumentsforMID(string MgId)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentsforMID";
        cmd.Parameters.Add(new SqlParameter("@MgId", SqlDbType.NVarChar));
        cmd.Parameters["@MgId"].Value = MgId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable SelctMonthonYear(string Year, string currentmonth)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelctMonthonYear";
        cmd.Parameters.Add(new SqlParameter("@Year", SqlDbType.NVarChar));
        cmd.Parameters["@Year"].Value = Year; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@currentmonth", SqlDbType.NVarChar));
        cmd.Parameters["@currentmonth"].Value = currentmonth;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
}
