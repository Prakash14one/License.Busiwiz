using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// Summary description for DataAccess
/// </summary>
public class DataAccess
{
    public static SqlConnection epconn;
	public DataAccess()
	{
		
	}

    public static DataTable ExecuteSelectCommand(DbCommand command)
    {
        
        DataTable table;
     
        try
        
        {
            // Open the data connection 
            if (command.Connection.State.ToString() != "Open")
            {
                command.Connection.Open();
            }
            // Execute the command and save the results in a DataTable
            DbDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);
            // Close the reader 
            reader.Close();

            command.Connection.Close();
            return table;
        }
        catch (Exception e)
        {
            e.ToString();
            return null;
        }
        
    }

    // execute an update, delete, or insert command 
    // and return the number of affected rows
    public static int ExecuteNonQuery(DbCommand command)
    {
        // The number of affected rows 
        int affectedRows = -1;
        // Execute the command making sure the connection gets closed in the end
        //try
        //{
            // Open the connection of the command
        if (command.Connection.State.ToString() != "Open")
        {
            command.Connection.Open();
        }
            // Execute the command and get the number of affected rows
            affectedRows = command.ExecuteNonQuery();
        //}
        //catch (Exception ex)
        //{
        //    // Log eventual errors and rethrow them
        //    Utilities.LogError(ex);
        //    throw ex;
        //}
        //finally
        //{
            // Close the connection
            command.Connection.Close();
        //}
        // return the number of affected rows
        return affectedRows;
    }

    // execute a select command and return a single result as a string
    public static string ExecuteScalar(DbCommand command)
    {
        // The value to be returned 
        string value = "";
      
           
            command.Connection.Open();
            // Execute the command and get the number of affected rows
            value = command.ExecuteScalar().ToString();
       
      
            command.Connection.Close();
       
        return value;
    }

    // creates and prepares a new DbCommand object on a new connection
    public static DbCommand CreateCommand()
    {
      //  string connectionString = SiteConfiguration.DbConnectionString;
        // Set the connection string
      //  System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionString);
        dy();
        System.Data.SqlClient.SqlConnection conn = epconn;
        // Create a database specific command object
        DbCommand comm = conn.CreateCommand();
        // Set the command type to stored procedure
        comm.CommandType = CommandType.StoredProcedure;
        // Return the initialized command object
        return comm;
    }
    public static void dy()
    {
        if (HttpContext.Current.Session["dyconn"] != null)
        {
            epconn = (SqlConnection)HttpContext.Current.Session["dyconn"];
        }
        else
        {
            PageConn pgcon = new PageConn();
            epconn = pgcon.dynconn;

        }
    }
    public static IDataReader ExecuteReder(DbCommand command)
    {
        // The DataTable to be returned 
        IDataReader rdr = null;
        // Execute the command making sure the connection gets closed in the end
        try
        {
            // Open the data connection 
            if (command.Connection.State.ToString() != "Open")
            {
                command.Connection.Open();
            }
            // Execute the command and save the results in a DataTable
            rdr = command.ExecuteReader();
          
            // Close the reader 
            rdr.Close();
        }
        catch (Exception ex)
        {
            //Utilities.LogError(ex);
            throw ex;
        }
        finally
        {
            // Close the connection
            command.Connection.Close();
        }
        return rdr;
    }
}
