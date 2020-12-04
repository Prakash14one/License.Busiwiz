using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class ClsMainType
{
    public ClsMainType()
	{
	}

    //GET ALL DATA FROM CATEGORYMASTER TABLE
    public static DataTable SpMainTypeGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMainTypeGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM CATEGORYMASTER BY id[PRIMARY KEY]
    public static DataTable SpMainTypeGetDataById(string id)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMainTypeGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpSelectPartyWithPeriodeInfo(string Documentid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSelectPartyWithPeriodeInfo";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Documentid";
        param.Value = Documentid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable SpSelectPartyWithPeriode(string PartyID,string FromDate,string ToDate)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpSelectPartyWithPeriode";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@PartyID";
        param.Value = PartyID;
        param.DbType = DbType.Int32 ;
        comm.Parameters.Add(param);

        DbParameter param1 = comm.CreateParameter();
        param1.ParameterName = "@FromDate";
        param1.Value = FromDate;
        param1.DbType = DbType.DateTime;
        comm.Parameters.Add(param1);

        DbParameter param2 = comm.CreateParameter();
        param2.ParameterName = "@ToDate";
        param2.Value = ToDate;
        param2.DbType = DbType.DateTime;
        comm.Parameters.Add(param2);

        //DbParameter param3 = comm.CreateParameter();
        //param.ParameterName = "@Doctitle";
        //param.Value = Doctitle;
        //param.DbType = DbType.String;
        //param.Size = 50;
        //comm.Parameters.Add(param3);

        //DbParameter param4 = comm.CreateParameter();
        //param.ParameterName = "@DocumentID";
        //param.Value = DocumentID;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param4);

        

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }



   
    //ADD DATA IN CATEGORYMASTER TABLE
    public static bool SpMainTypeAddData(string name)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMainTypeAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@name";
        param.Value = name;
        param.DbType = DbType.String;
        param.Size = 50;
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

    //UPDATE DATA IN CATEGORYMASTER TABLE
    public static bool SpMainTypeUpdateData(string id, string name)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMainTypeUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@name";
        param.Value = name;
        param.DbType = DbType.String;
        param.Size = 100;
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

    //DELETE DATA FROM CATEGORYMASTER TABLE BY id[PRIMARY KEY]
    public static bool SpMainTypeDeleteData(string id)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMainTypeDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
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
}
