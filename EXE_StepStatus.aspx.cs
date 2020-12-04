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
using System.Xml;

public partial class CreateCompanyconnectionString : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static string strEnc = "";
    public static string strser = "";
    public static string  datasource= "";
    public static string catalog = "";
    public static string userid = "";
    public static string password = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string ipaddress = "";
            ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (Request.QueryString["comid"] != null)
            {
                try
                {
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("EXE_ServerSetupStatus_AddDelUpdtSelect", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StatementType", "Insert");
                    cmd.Parameters.AddWithValue("@Companyloginid", Request.QueryString["comid"]);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToShortTimeString());
                    cmd.Parameters.AddWithValue("@ServerID", Request.QueryString["serid"]);
                    cmd.Parameters.AddWithValue("@IP_Address", ipaddress);
                    cmd.Parameters.AddWithValue("@status", Request.QueryString["status"]);
                    cmd.Parameters.AddWithValue("@StepComplited", Request.QueryString["stepcomplite"]);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {
                    Response.Redirect("http://license.busiwiz.com/");
                }
            }
            else
            {
                Response.Redirect("http://license.busiwiz.com/"); 
            }
            Response.Redirect("http://license.busiwiz.com/");
        }
        
    }

   
   

}