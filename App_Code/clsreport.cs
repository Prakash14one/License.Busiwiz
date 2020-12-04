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
/// <summary>
/// Summary description for clsreport
/// </summary>
public class clsreport
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(PageConn.connnn);
    DataSet fillg = new DataSet();
	public clsreport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet fillgrid(GridView grid, string query)
    {
        try
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand(query, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            
            da1.Fill(fillg);

            grid.DataSource = fillg.Tables[0];
            grid.DataBind();

            grid.EmptyDataText = "No Records Found";
            
        }
        catch (Exception ee)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
        return fillg;

    }
    public void fillddl(DropDownList ddl, string store, string field, string field1)
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(store, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = field;
            ddl.DataValueField = field1;
            ddl.DataBind();
            ddl.Items.Insert(0, "-select-");
        }
        catch (Exception ee)
        {
            ee.Message.ToString();
            throw;

        }
        finally
        {
            con.Close();
        }
    }
}
