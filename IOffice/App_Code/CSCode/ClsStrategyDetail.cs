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
/// Summary description for ClsStrategyDetail
/// </summary>
/// 

public struct StStrategyDetail
{
    public int detailid;
    public int masterid;
    public int employeeid;
    public string detail;
    public string date;
    public int userid;
}
public class ClsStrategyDetail
{
	public ClsStrategyDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpStrategyDetailGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyDetailGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpStrategyDetailGetDataById(string detailid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyDetailGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@detailid";
        param.Value = detailid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpStrategyDetailGetDataByMasterId(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyDetailGetDataByMasterId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpStrategyDetailGetDataByMasterIdWithEmployeeName(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyDetailGetDataByMasterIdWithEmployeeName";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StStrategyDetail SpStrategyDetailGetDataStructById(string detailid)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyDetailGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@detailid";
        param.Value = detailid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StStrategyDetail details = new StStrategyDetail();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.detailid = Convert.ToInt32(dr["detailid"]);
            details.masterid = Convert.ToInt32(dr["masterid"]);
            details.employeeid = Convert.ToInt32(dr["employeeid"]);
            details.detail = dr["detail"].ToString();
            details.date = dr["date"].ToString();
            details.userid = Convert.ToInt32(dr["userid"]);
        }
        //return object with data
        return details;
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static Int32 SpStrategyDetailAddData(string MasterId, string Date, string Detail)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyDetailAddData";

        // create a new parameter

        DbParameter param = comm.CreateParameter();



        param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

     





        param = comm.CreateParameter();
        param.ParameterName = "@Date";
        param.Value = Date;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

     
        param = comm.CreateParameter();
        param.ParameterName = "@Detail";
        param.Value = Detail;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        comm.Parameters.Add(new SqlParameter("@DetailId", SqlDbType.Int));
        comm.Parameters["@DetailId"].Direction = ParameterDirection.Output;
        comm.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        comm.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(comm);
        //if (result > 0)
        //{
        result = Convert.ToInt32(comm.Parameters["@DetailId"].Value);
        //}

        return (result);






       
    }

    //UPDATE DATA IN DepartmentMASTER TABLE


    public static bool SpStrategyDetailUpdateData(string DetailId, string MasterId, string Date, string Detail)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyDetailUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@DetailId";
        param.Value = DetailId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@MasterId";
        param.Value = MasterId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Date";
        param.Value = Date;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

    

        param = comm.CreateParameter();
        param.ParameterName = "@Detail";
        param.Value = Detail;
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

    //DELETE DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static bool SpStrategyDetailDeleteData(string detailid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpStrategyDetailDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@detailid";
        param.Value = detailid;
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
    public static DataTable SpStrategyDetailGridfillforstrategy(String para, string filc, int flag)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SpStrategyDetailGridfillforstrategy";
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
