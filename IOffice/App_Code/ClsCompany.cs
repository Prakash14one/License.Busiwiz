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
/// Summary description for ClsCompany
/// </summary>
/// 

public struct StCompany
{
    public int CompanyId;
    public string CompanyName;
    public string Address1;
    public string Address2;
    public string City;
    public string State;
    public string Country;
    public string Zipcode;
    public string Phone1;
    public string Phone2;
    public string Fax;
    public string Email;
    public string AdminName;
}

public class ClsCompany
{
    

	public ClsCompany()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM CompanyMASTER TABLE
    public static DataTable SpCompanyGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpCompanyGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //GET DATA FROM CompanyMASTER TABLE BY CompanyId[PRIMARY KEY]
    public static DataTable SpCompanyGetDataById(string CompanyId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpCompanyGetDataById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CompanyId";
        param.Value = CompanyId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }

    //DELETE DATA FROM CompanyMASTER TABLE BY CompanyId[PRIMARY KEY]
    public static bool SpCompanyDeleteData(string CompanyId)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpCompanyDeleteData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CompanyId";
        param.Value = CompanyId;
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
    
    //ADD DATA IN CompanyMASTER TABLE
    public static bool SpCompanyAddData(string companyname,string address1, string address2,string city,string state,string country,string zipcode,string phone1,string phone2,string fax,string email,string adminname)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpCompanyAddData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@companyname";
        param.Value = companyname;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@address1";
        param.Value = address1;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@address2";
        param.Value = address2;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@city";
        param.Value = city;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@state";
        param.Value = state;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = country;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@zipcode";
        param.Value = zipcode;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@phone1";
        param.Value = phone1;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@phone2";
        param.Value = phone2;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@fax";
        param.Value = fax;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@adminname";
        param.Value = adminname;
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

    //UPDATE DATA IN CompanyMASTER TABLE
    public static bool SpCompanyUpdateData(string CompanyId, string companyname, string address1, string address2, string city, string state, string country, string zipcode, string phone1, string phone2, string fax, string email, string adminname)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpCompanyUpdateData";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CompanyId";
        param.Value = CompanyId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@companyname";
        param.Value = companyname;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@address1";
        param.Value = address1;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@address2";
        param.Value = address2;
        param.DbType = DbType.String;
        param.Size = 80000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@city";
        param.Value = city;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@state";
        param.Value = state;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = country;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@zipcode";
        param.Value = zipcode;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@phone1";
        param.Value = phone1;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@phone2";
        param.Value = phone2;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@fax";
        param.Value = fax;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@adminname";
        param.Value = adminname;
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

    public static StCompany SpCompanyGetDataStructById(string companyid)
    {   // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpCompanyGetDataStructById";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@companyid";
        param.Value = companyid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //execute procedure and store data in table
        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //create object of structure
        StCompany details = new StCompany();

        //check there is row in table or not
        if (table.Rows.Count > 0)
        {  //create data reader
            DataRow dr = table.Rows[0];

            //store individually each field into object
            details.CompanyId = Convert.ToInt32(dr["CompanyId"]);
            details.CompanyName = dr["CompanyName"].ToString();
            details.Address1 = dr["Address1"].ToString();
            details.Address2 = dr["Address2"].ToString();
            details.City = dr["City"].ToString();
            details.State = dr["State"].ToString();
            details.Country = dr["Country"].ToString();
            details.Zipcode = dr["Zipcode"].ToString();
            details.Phone1 = dr["Phone1"].ToString();
            details.Phone2 = dr["Phone2"].ToString();
            details.Fax = dr["Fax"].ToString();
            details.Email = dr["Email"].ToString();
            details.AdminName = dr["AdminName"].ToString();

            
        }
        //return object with data
        return details;
    }


}
