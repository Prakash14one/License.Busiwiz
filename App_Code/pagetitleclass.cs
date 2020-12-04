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
/// Summary description for pagetitleclass
/// </summary>
public class pagetitleclass
{
   // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TestHelpWizardBusiwizconnectionstring"].ConnectionString);

  //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;
    public pagetitleclass()
	{

    

	}
    public string getPageTitle(string PgName)
    {
        PageConn pagecon = new PageConn();
        con = pagecon.dynconn;
        string str = "Select PageTitle from PageMaster where PageName='" + ClsEncDesc.EncDyn(PgName) + "'";
        SqlCommand cmd = new SqlCommand(str, PageConn.busclient());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        string pgTitle;
        if (dt.Rows.Count > 0)
        {

            pgTitle = ClsEncDesc.DecDyn(dt.Rows[0]["PageTitle"].ToString());
        }
        else
        {
            pgTitle = "Iwebshop Version1.1";
        }
        return pgTitle;
        
    }

}
