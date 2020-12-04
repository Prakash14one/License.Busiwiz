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
using System.IO;
using System .Collections .Generic ;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{

    //
    //Server=BR1\sqlserver2550;Database=TMS;User ID=sa;Password=BarodaBarodaSQL12++
    //SqlConnection cn = new SqlConnection(@"Data Source=BR1\sqlserver,2550;Initial Catalog=TMS;User ID=sa;Password=BarodaBarodaSQL12++");
    SqlConnection cn = new SqlConnection(PageConn.connnn);


    //SqlConnection cn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Policymanagement.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
	public Class1()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void fire(string s)
    {
        try
        {

            cn.Open();
            cmd = new SqlCommand(s, cn);
            cmd.ExecuteNonQuery();

        }
        catch (SqlException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            cn.Close();
        }
    }


    public  DataSet fill(String s)
    {

        try
        {
            da = new SqlDataAdapter(s, cn);
            ds = new DataSet();
            da.Fill(ds);        
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            cn.Close();
        }

        return ds;
    }
   
}
