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
/// <summary>
/// Summary description for ClsDocumentProcessing
/// </summary>
public class ClsDocumentProcessing
{
	public ClsDocumentProcessing()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpDocumentProcessingGetData(string dataoperatorid, string supervisorid, string adminid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentProcessingGetData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@dataoperatorid";
        param.Value = dataoperatorid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@supervisorid";
        param.Value = supervisorid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@adminid";
        param.Value = adminid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpDocumentProcessingAddData(string dataoperatorid,string supervisorid, string adminid, string documentid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentProcessingAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@dataoperatorid";
        param.Value = dataoperatorid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@supervisorid";
        param.Value = supervisorid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@adminid";
        param.Value = adminid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@documentid";
        param.Value = documentid;
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
        // log errors if any
        //}
        // result will be 1 in case of success
        return (result != -1);
    }


    public static DataTable SpDocumentProcessingGetDataByDocumentId(string documentid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentProcessingGetDataByDocumentId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@documentid";
        param.Value = documentid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static bool SpDocumentProcessingAddData(string p, string p_2, string p_3, object p_4)
    {
        throw new Exception("The method or operation is not implemented.");
    }
}
