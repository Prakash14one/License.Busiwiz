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
/// Summary description for ClsYDetail
/// </summary>
/// 

public struct StYDetail
{
    public int detailid;
    public int masterid;
    public int employeeid;
    public string detail;
    public string date;
    public int userid;
}

public class ClsYDetail
{
	public ClsYDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpYDetailGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpYDetailGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY MasterId[PRIMARY KEY]
    public static DataTable SpYDetailGetDataById(string detailid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpYDetailGetDataById";

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
    public static DataTable SpYDetailGetDataByMasterId(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpYDetailGetDataByMasterId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpYDetailGetDataByMasterIdWithEmployeeName(string masterid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpYDetailGetDataByMasterIdWithEmployeeName";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@masterid";
        param.Value = masterid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static StYDetail SpYDetailGetDataStructById(string detailid)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpYDetailGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@detailid";
        param.Value = detailid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StYDetail details = new StYDetail();

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
    //public static Int32 SpYDetailAddData(string MasterId, string Date, string Detail)
    //{
    //    // get a configured DbCommand object
    //    DbCommand comm = DataAccess.CreateCommand();

    //    // set the stored procedure name
    //    comm.CommandText = "SpYDetailAddData";

    //    // create a new parameter

    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = "@MasterId";
    //    param.Value = MasterId;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);




    //    param = comm.CreateParameter();
    //    param.ParameterName = "@Date";
    //    param.Value = Date;
    //    param.DbType = DbType.DateTime;
    //    comm.Parameters.Add(param);

        

       

    //    param = comm.CreateParameter();
    //    param.ParameterName = "@Detail";
    //    param.Value = Detail;
    //    param.DbType = DbType.String;
    //    comm.Parameters.Add(param);

    //    comm.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.Int));
    //    comm.Parameters["@MasterId"].Direction = ParameterDirection.Output;
    //    comm.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
    //    comm.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
    //    Int32 result = DatabaseCls1.ExecuteNonQueryep(comm);
    //    //if (result > 0)
    //    //{
    //    result = Convert.ToInt32(comm.Parameters["@MasterId"].Value);
    //    //}

    //    return (result);
    //}
    public static Int32 SpYDetailAddData(string MasterId, string Date, string Detail)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpYDetailAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
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
    public static bool SpYDetailUpdateData(string DetailId, string MasterId, string Date, string detail)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpYDetailUpdateData";

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
        param.ParameterName = "@detail";
        param.Value = detail;
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
    public static bool SpYDetailDeleteData(string detailid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpYDetailDeleteData";

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
}
