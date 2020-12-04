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
using System.Data.SqlClient;

/// <summary>
/// Summary description for Connection1
/// </summary>
public class Connection1
{
	public Connection1()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public SqlConnection ConnInit()
    {
        //connection to 100  Iwebshop(30-Jan-10)3
       // SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlConnection cn;
        PageConn pgcon = new PageConn();
        cn = pgcon.dynconn;
        return cn;
    }
    public SqlConnection ConnInit1()
    {
        //connection to 221 Iwebshop(30-Jan-10)3
       // SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        SqlConnection cn;
        PageConn pgcon = new PageConn();
        cn = pgcon.dynconn;
        return cn;
    }
    public SqlConnection ConnInit2()
    {
        //connection to 221 shopusa.indiaauthentic.com
       // SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        SqlConnection cn;
        PageConn pgcon = new PageConn();
        cn = pgcon.dynconn;
        return cn;
    }
    public void ConnConnect(SqlConnection con)
    {
        //bool i = false;
        
        if (con.State == ConnectionState.Closed)
        {
            con.Open();          
        }
        else
        {
            con.Close();
        }



        //return con;
    }

}
