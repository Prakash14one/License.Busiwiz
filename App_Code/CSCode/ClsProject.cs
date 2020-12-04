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
/// Summary description for ClsProject
/// </summary>
/// 

public struct StProject
{
    public int projectid;
    public int businessid;
    public string projectname;
    public string status;
    public string estartdate;
    public string eenddate;
    public int percentage;
    public int ltgmasterid;
    public int stgmasterid;
    public int ygmasterid;
    public int mgmasterid;
    public int wtmasterid;
    public int strategyid;
    public int tacticid;
    public string description;
    public string statusnote;
    public decimal budgetedamount;
    public decimal allotedamount;
    public decimal actualamount;
}

public class ClsProject
{
	public ClsProject()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpProjectMasterGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectMasterGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY ProjectId[PRIMARY KEY]
    public static DataTable SpProjectMasterGetDataById(string ProjectId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectMasterGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProjectId";
        param.Value = ProjectId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static  DataTable SpProjectMasterGetDataStructById(string ProjectId)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectMasterGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProjectId";
        param.Value = ProjectId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);


        return table;
    }

    //GET DATA FROM DepartmentMASTER TABLE BY ProjectId[PRIMARY KEY]
    public static DataTable SpProjectMasterGetDataByStatus(string status)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectMasterGetDataByStatus";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }


    //ADD DATA IN DepartmentMASTER TABLE
    public static int SpProjectMasterAddData(string businessid, string projectname, string status, string estartdate, string eenddate, string mgmasterid, string wtmasterid, string descritpion, string budgetedamount, string EmployeeID, String DeptId, String Whid, Boolean Addjob,String PartyId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectMasterAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@businessid";
        param.Value = businessid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@projectname";
        param.Value = projectname;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        param.Size = 100;
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
        param.ParameterName = "@percentage";
        param.Value = 0;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ltgmasterid";
        param.Value = 0;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@stgmasterid";
        param.Value =0;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ygmasterid";
        param.Value = 0;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@mgmasterid";
        param.Value = mgmasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@wtmasterid";
        param.Value = wtmasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@strategyid";
        param.Value = 0;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@tacticid";
        param.Value = 0;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@description";
        param.Value = descritpion;
        param.DbType = DbType.String;
       
        comm.Parameters.Add(param);


        //param = comm.CreateParameter();
        //param.ParameterName = "@statusnote";
        //param.Value = statusnote;
        //param.DbType = DbType.String;
        //param.Size = 80000;
       // comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@budgetedamount";
        param.Value = budgetedamount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@EmployeeID";
        param.Value = EmployeeID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@DeptId";
        param.Value = DeptId;
        param.DbType = DbType.String;

        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Whid";
        param.Value = Whid;
        param.DbType = DbType.String;

        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Addjob";
        param.Value = Addjob;
        param.DbType = DbType.String;

        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@PartyId";
        param.Value = PartyId;
        param.DbType = DbType.String;

        comm.Parameters.Add(param);

        comm.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int));
        comm.Parameters["@ProjectId"].Direction = ParameterDirection.Output;
        comm.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        comm.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(comm);
        //if (result > 0)
        //{
        result = Convert.ToInt32(comm.Parameters["@ProjectId"].Value);
        //}

        return (result);
    }



    //UPDATE DATA IN DepartmentMASTER TABLE
    public static int SpProjectMasterUpdateData(string ProjectId, string businessid, string projectname, string status, string estartdate, string eenddate, string mgmasterid, string wtmasterid, string descritpion, string budgetedamount, string EmployeeID, String DeptId, String Whid, Boolean Addjob, String PartyId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectMasterUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProjectId";
        param.Value = ProjectId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@businessid";
        param.Value = businessid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@projectname";
        param.Value = projectname;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        param.Size = 100;
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

        //param = comm.CreateParameter();
        //param.ParameterName = "@percentage";
        //param.Value = percentage;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = "@ltgmasterid";
        //param.Value = ltgmasterid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = "@stgmasterid";
        //param.Value = stgmasterid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = "@ygmasterid";
        //param.Value = ygmasterid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@mgmasterid";
        param.Value = mgmasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@wtmasterid";
        param.Value = wtmasterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = "@strategyid";
        //param.Value = strategyid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = "@tacticid";
        //param.Value = tacticid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@description";
        param.Value = descritpion;
        param.DbType = DbType.String;
      
        comm.Parameters.Add(param);


        //param = comm.CreateParameter();
        //param.ParameterName = "@statusnote";
        //param.Value = statusnote;
        //param.DbType = DbType.String;
        //param.Size = 80000;
        //comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@budgetedamount";
        param.Value = budgetedamount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@EmployeeID";
        param.Value = EmployeeID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@DeptId";
        param.Value = DeptId;
        param.DbType = DbType.String;

        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Whid";
        param.Value = Whid;
        param.DbType = DbType.String;

        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Addjob";
        param.Value = Addjob;
        param.DbType = DbType.String;

        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@PartyId";
        param.Value = PartyId;
        param.DbType = DbType.String;

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

    public static bool SpProjectMasterUpdateDataForReport(string ProjectId, string projectname, string status, string descritpion)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectMasterUpdateDataForReport";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProjectId";
        param.Value = ProjectId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@projectname";
        param.Value = projectname;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
       
        param = comm.CreateParameter();
        param.ParameterName = "@description";
        param.Value = descritpion;
        param.DbType = DbType.String;
        param.Size = 80000;
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





    //DELETE DATA FROM DepartmentMASTER TABLE BY ProjectId[PRIMARY KEY]
    public static int SpProjectMasterDeleteData(string ProjectId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectMasterDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProjectId";
        param.Value = ProjectId;
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
        return result;
    }
    public static DataTable SelectWddfilter(String Whid, String Deptid, String BusinessID, String EmployeemasterId, int flag)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectWddfilter";
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
    public static DataTable SpProjectMasterGridfill(String para, string filc)
    {
      SqlCommand  cmd = new SqlCommand();
      DataTable  dt = new DataTable();
      cmd.CommandText = "SpProjectMasterGridfill";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //
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
    public static DataTable SelectOfficeManagerDocumentswithprid(string MissionId)
    {
       SqlCommand cmd = new SqlCommand();
       DataTable dt = new DataTable();
       cmd.CommandText = "SelectOfficeManagerDocumentswithprid";
        cmd.Parameters.Add(new SqlParameter("@MissionId", SqlDbType.NVarChar));
        cmd.Parameters["@MissionId"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public static DataTable SelectOfficeManagerDocumentswithprevaid(string MissionId)
    {
       SqlCommand cmd = new SqlCommand();
       DataTable dt = new DataTable();
       cmd.CommandText = "SelectOfficeManagerDocumentswithprevaid";
        cmd.Parameters.Add(new SqlParameter("@MissionId", SqlDbType.NVarChar));
        cmd.Parameters["@MissionId"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    
    public static DataTable DeleteProjectbyId(string ProjectId)
    {



        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "DeleteProjectbyId";
        cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.NVarChar));
        cmd.Parameters["@ProjectId"].Value = ProjectId;
        //cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.BigInt));
        //cmd.Parameters["@flag"].Value = flag;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static DataTable SpProjectbydd(String Whid, String Deptid, String BusinessID, String EmployeemasterId)
    {
       SqlCommand cmd = new SqlCommand();
       DataTable dt = new DataTable();
       cmd.CommandText = "SpProjectbydd";
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
    public static DataTable SpSpProjectddfilter(String Whid, String Deptid, String BusinessID, String EmployeemasterId, int flag)
    {
       SqlCommand  cmd = new SqlCommand();
       DataTable dt = new DataTable();
       cmd.CommandText = "SpSpProjectddfilter";
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
    public static DataTable Selectactulcostproject(string MissionId)
    {
      SqlCommand  cmd = new SqlCommand();
      DataTable  dt = new DataTable();
      cmd.CommandText = "Selectactulcostproject";
        cmd.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.NVarChar));
        cmd.Parameters["@MasterId"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public static int Updateactuccostproject(string MissionId, decimal ActualCost, decimal ShortageExcess, String StatusId, String reminder)
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "Updateactuccostproject";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@masterid", SqlDbType.NVarChar));
        cmd.Parameters["@masterid"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@ActualCost", SqlDbType.Decimal));
        cmd.Parameters["@ActualCost"].Value = ActualCost; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.NVarChar));
        cmd.Parameters["@StatusId"].Value = StatusId;

        cmd.Parameters.Add(new SqlParameter("@ShortageExcess", SqlDbType.Decimal));        
        cmd.Parameters["@ShortageExcess"].Value = ShortageExcess; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@Reminder", SqlDbType.NVarChar));
        cmd.Parameters["@Reminder"].Value = reminder;

        int I = DatabaseCls1.ExecuteNonQueryep(cmd); //.FillAdapter(cmd);
        return I;
    }
    public static int SpProjectEvaluationAddData(string masterid, string employeeid, string evaluationnote, string percentage, string reminderdate,string remindernote )
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectEvaluationAddData";

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
      
        comm.Parameters.Add(param);

       

        param = comm.CreateParameter();
        param.ParameterName = "@percentage";
        param.Value = percentage;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ReminderNote";
        param.Value = remindernote;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Reminderdate";
        param.Value = reminderdate;
        param.DbType = DbType.DateTime;
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
    public static int SpProjectEvaluationUpdateData(string EvaluationId, string date, string evaluationnote, string percentage, string MasterId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectEvaluationUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@EvaluationId";
        param.Value = EvaluationId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@StatusDate";
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

        param = comm.CreateParameter();
        param.ParameterName = "@ProjectId";
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
    public static DataTable SpProjectEvaluationGetDataStructById(string evaluationid)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectEvaluationGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@evaluationid";
        param.Value = evaluationid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);


        return table;
    }


    public static DataTable SpProjectEvaluationfillGrid(String para, string filc)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SpProjectEvaluationfillGrid";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //
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
    public static int SpProjectEvaluationDeleteData(string evaluationid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpProjectEvaluationDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@evaluationid";
        param.Value = evaluationid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;
      
        result = DataAccess.ExecuteNonQuery(comm);
       
        return result;
    }
    public static DataTable SpProjectbyddnotcomplete(String Whid, String Deptid, String BusinessID, String EmployeemasterId)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SpProjectbyddnotcomplete";
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
  

}
