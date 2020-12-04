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
/// Summary description for clscompititor
/// </summary>
public class clscompititor
{

    SqlConnection con = new SqlConnection(PageConn.connnn);

   
	public clscompititor()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet fillgrid(GridView grid, string store)
    {
        try
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand(store, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataSet dsfillgrid = new DataSet();
            da.Fill(dsfillgrid);

            grid.DataSource = dsfillgrid.Tables[0];
            grid.DataBind();
            return dsfillgrid;
        }
        catch (Exception ee)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
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
        catch
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }
    
    public DataSet fillddlsearch(string query)
    {
        try
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataSet ds1 = new DataSet();
            da.Fill(ds1);
            return ds1;

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
        
            con.Open ();
        
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

}
