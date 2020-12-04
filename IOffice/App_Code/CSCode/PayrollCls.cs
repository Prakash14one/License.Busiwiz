using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;
/// <summary>
/// Summary description for PayrollCls
/// </summary>
public class PayrollCls
{
    SqlCommand cmd;
    DataTable dt;
	public PayrollCls()
	{
		 
	}

    public DataTable SelectPayFrequencyMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectACCOUNTINGDesg";
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
}
