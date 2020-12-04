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
/// Summary description for ClsCountry
/// </summary>
public class ClsCountry
{
	public ClsCountry()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET ALL DATA FROM CompanyMASTER TABLE
    public static DataTable SpCountryGetData()
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "SpCountryGetData";

        // execute the stored procedure
        return DataAccess.ExecuteSelectCommand(comm);
    }
}
