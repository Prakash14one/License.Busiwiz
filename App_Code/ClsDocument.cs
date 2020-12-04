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
/// Summary description for ClsDocument
/// </summary>
/// 

public struct StDocument
{
    public int DocumentId;
    public string Docuname;
    public string Partyname;
    public string Docudate;
    public string Docutype;
    public string Docuperiod;
    public string Accessuser;
    public string Accountname;
    public string Userid;
    public bool Isapprove;
    public bool Iscleared;
    public string Sender;
    public string Receiver;
    public string Entrydate;
}

public struct StDocumentDetail
{
    public int DocumentID;
	public string Docudesc;
	public string reffno;
	public int DataentryopID;
	public int DatasupervisorID;
	public decimal Docunetamount;
	public decimal DocuTax1;
	public decimal DocuTax2;
	public decimal DocuTax3;
	public decimal DocuTax4;
	public decimal Docuotheramt1;
	public decimal Docuotheramt2;
	public decimal Docuotheramt3;
	public decimal Docugrossamt;
	public decimal Outsandingamt;
	public decimal Docufinalamt;
	public int Approval1;
	public string Note1;
	public int Approval2 ;
	public string Note2;
	public int Approval3;
	public string Note3;
	public int Approval4;
	public string Note4;
	public int Approval5;
    public string Note5;
}
public class ClsDocument
{
	public ClsDocument()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static StDocument SpDocumentGetDataStructById(string documentid)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@documentid";
        param.Value = documentid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StDocument details = new StDocument();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.DocumentId = Convert.ToInt32(dr["DocumentId"]);
            details.Docuname = dr["Docuname"].ToString();
            details.Partyname = dr["Partyname"].ToString();
            details.Docudate = dr["Docudate"].ToString();
            details.Docutype = dr["Docutype"].ToString();
            details.Docuperiod = dr["Docuperiod"].ToString();
            details.Accessuser = dr["Accessuser"].ToString();
            details.Accountname = dr["Accountname"].ToString();
            details.Userid = dr["Userid"].ToString();
            details.Isapprove = Convert.ToBoolean(dr["Isapprove"]);
            details.Iscleared = Convert.ToBoolean(dr["Iscleared"]);
            details.Sender = dr["Sender"].ToString();
            details.Receiver = dr["Receiver"].ToString();
            details.Entrydate = dr["Entrydate"].ToString();


        }
        //return object with data
        return details;
    }

    public static StDocumentDetail SpDocumentDetailGetDataStructById(string documentid)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentDetailGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@documentid";
        param.Value = documentid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StDocumentDetail details = new StDocumentDetail();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.DocumentID = Convert.ToInt32(dr["DocumentID"]);
            details.Docudesc = dr["Docudesc"].ToString();
            details.reffno = dr["reffno"].ToString();
            details.DataentryopID = Convert.ToInt32(dr["DataentryopID"]);
            details.DatasupervisorID = Convert.ToInt32(dr["DatasupervisorID"]);
            details.Docunetamount = Convert.ToDecimal(dr["Docunetamount"]);
            details.DocuTax1 = Convert.ToDecimal(dr["DocuTax1"]);
            details.DocuTax2 = Convert.ToDecimal(dr["DocuTax2"]);
            details.DocuTax3 = Convert.ToDecimal(dr["DocuTax3"]);
            details.DocuTax4 = Convert.ToDecimal(dr["DocuTax4"]);
            details.Docuotheramt1 = Convert.ToDecimal(dr["Docuotheramt1"]);
            details.Docuotheramt2 = Convert.ToDecimal(dr["Docuotheramt2"]);
            details.Docuotheramt3 = Convert.ToDecimal(dr["Docuotheramt3"]);
            details.Docugrossamt = Convert.ToDecimal(dr["Docugrossamt"]);
            details.Outsandingamt = Convert.ToDecimal(dr["Outsandingamt"]);
            details.Docufinalamt = Convert.ToDecimal(dr["Docufinalamt"]);
            details.Approval1 = Convert.ToInt32(dr["Approval1"]);
            details.Note1 = dr["Note1"].ToString();
	        details.Approval2 = Convert.ToInt32(dr["Approval2"]);
            details.Note2 = dr["Note2"].ToString();
            details.Approval3 = Convert.ToInt32(dr["Approval3"]);
            details.Note3 = dr["Note3"].ToString();
            details.Approval4 = Convert.ToInt32(dr["Approval4"]);
            details.Note4 = dr["Note4"].ToString();
            details.Approval5 = Convert.ToInt32(dr["Approval5"]);
            details.Note5 = dr["Note5"].ToString();
        }
        //return object with data
        return details;
    }

    //GET DATA FROM DepartmentMASTER TABLE BY deptid[PRIMARY KEY]
    public static DataTable SpDocumentGetDataById(string documentid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@documentid";
        param.Value = documentid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpDocumentGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpDocumentAddData(string docuname)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@docuname";
        param.Value = docuname;
        param.DbType = DbType.String;
        param.Size = 500;
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

    //UPDATE DATA IN DepartmentMASTER TABLE
    public static bool SpDocumentUpdateData(string documentid, string partyname,string accessuser,string accountname,string userid,string isapprove,string iscleared,string sender,string receiver)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@documentid";
        param.Value = documentid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@partyname";
        param.Value = partyname;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@accessuser";
        param.Value = accessuser;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@accountname";
        param.Value = accountname;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@isapprove";
        param.Value = isapprove;
        param.DbType = DbType.Boolean;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@iscleared";
        param.Value = iscleared;
        param.DbType = DbType.Boolean;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@sender";
        param.Value = sender;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@receiver";
        param.Value = receiver;
        param.DbType = DbType.String;
        param.Size = 100;
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


    //DELETE DATA FROM DepartmentMASTER TABLE BY deptid[PRIMARY KEY]
    public static bool SpDocumentDeleteData(string documentid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentDeleteData";

        // create a new parameter
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@documentid";
        param.Value = documentid;
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

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpDocumentRefGetData(string mainid, string subid, string typeid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentRefGetData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@mainid";
        param.Value = mainid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@subid";
        param.Value = subid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@typeid";
        param.Value = typeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpDocumentRefAddData(string mainid,string subid,string typeid,string documentid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentRefAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@mainid";
        param.Value = mainid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@subid";
        param.Value = subid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@typeid";
        param.Value = typeid;
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

    //DELETE DATA FROM DepartmentMASTER TABLE BY deptid[PRIMARY KEY]
    public static bool SpDocumentRefDeleteData(string mainid, string subid, string typeid, string documentid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentRefDeleteData";

        // create a new parameter
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@mainid";
        param.Value = mainid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@subid";
        param.Value = subid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@typeid";
        param.Value = typeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@documentid";
        param.Value = documentid;
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

    //ADD DATA IN DepartmentMASTER TABLE
    public static bool SpDocumentsAddData(string docuname)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentsAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@docuname";
        param.Value = docuname;
        param.DbType = DbType.String;
        param.Size = 500;
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

    //GET DATA FROM DepartmentMASTER TABLE BY deptid[PRIMARY KEY]
    public static DataTable SpDocumentGetDataByAllocation(string userid,string accesslevel)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentGetDataByAllocation";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
            
        param = comm.CreateParameter();
        param.ParameterName = "@accesslevel";
        param.Value = accesslevel;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET ALL DATA FROM DepartmentMASTER TABLE
    public static DataTable SpDocumentGetDataToAllote()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentGetDataToAllote";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }


    //UPDATE DATA IN DepartmentMASTER TABLE
    public static bool SpDocumentUpdateAllData(string documentid, string mainid,string subid,string typeid,string docuname,string partyname,string accountname,string docudesc,string documentamount,string docutax1,string docutax2,string docutax3,string docutax4,string docugrossamt,string docuotheramt1,string docuotheramt2,string docuotheramt3,string docufinalamt,string isapprove,string approvenote,string type)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentUpdateAllData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@documentid";
        param.Value = documentid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@mainid";
        param.Value = mainid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@subid";
        param.Value = subid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@typeid";
        param.Value = typeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@docuname";
        param.Value = docuname;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@partyname";
        param.Value = partyname;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@accountname";
        param.Value = accountname;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@docudesc";
        param.Value = docudesc;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@documentamount";
        param.Value = documentamount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@docutax1";
        param.Value = docutax1;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@docutax2";
        param.Value = docutax2;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@docutax3";
        param.Value = docutax3;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@docutax4";
        param.Value = docutax4;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@docugrossamt";
        param.Value = docugrossamt;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@docuotheramt1";
        param.Value = docuotheramt1;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@docuotheramt2";
        param.Value = docuotheramt2;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@docuotheramt3";
        param.Value = docuotheramt3;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@docufinalamt";
        param.Value = docufinalamt;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@isapprove";
        param.Value = isapprove;
        param.DbType = DbType.Boolean;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@approvenote";
        param.Value = approvenote;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@type";
        param.Value = type;
        param.DbType = DbType.String;
        param.Size = 100;
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

    //GET DATA FROM DepartmentMASTER TABLE BY deptid[PRIMARY KEY]
    public static DataTable SpMasterMasterGetDataByDocumentId(string documentid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpMasterMasterGetDataByDocumentId";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@documentid";
        param.Value = documentid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM DepartmentMASTER TABLE BY deptid[PRIMARY KEY]
    public static DataTable SpDocumentGetDataByRight(string userid)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpDocumentGetDataByRight";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
}
