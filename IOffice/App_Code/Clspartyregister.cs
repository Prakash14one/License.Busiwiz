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
/// Summary description for Clspartyregister
/// </summary>
public class Clspartyregister
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(PageConn.connnn);
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

        }
        catch
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }
    public int insert_update_delete(string query)
    {

        con.Open();

        try
        {

            SqlTransaction trn;
            trn = con.BeginTransaction();

            SqlCommand cmd1 = new SqlCommand(query, con);
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = query;
            cmd1.Connection = con;
            cmd1.Transaction = trn;
            cmd1.ExecuteNonQuery();
            trn.Commit();
            return 1;
        }
        catch
        {

            throw;

        }
        finally
        {
            con.Close();
        }
    }
	public Clspartyregister()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
