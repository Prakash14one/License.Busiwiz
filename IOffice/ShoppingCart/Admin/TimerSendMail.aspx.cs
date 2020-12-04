using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class Manage_Ip_Address_Allowed : System.Web.UI.Page
{
    SqlConnection conOADB;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        PageConn pgcon = new PageConn();
        conOADB = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        Label1.Visible = false;
        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);          
        }
    }


    protected void Timer1_Tick(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now;
        string date = now.GetDateTimeFormats('d')[0];
        Label1.Text = now.GetDateTimeFormats('t')[0];
        Label1.Text = now.ToString("HH:mm");
        string comd = " SELECT DISTINCT  * FROM dbo.ReprotmasterRrecipentEmployeeTbl ";      
        SqlCommand cmall = new SqlCommand(comd, conOADB);
        DataTable dtall = new DataTable();
        SqlDataAdapter adpall = new SqlDataAdapter(cmall);
        adpall.Fill(dtall);
        if (dtall.Rows.Count > 0)
        {
            //idofoadb = dtall.Rows[0]["Pagerepoid"].ToString();
            foreach (DataRow dr in dtall.Rows)
            {
                if (Label1.Text == dr["time"].ToString())
                {
                    string strcln = "";
                    strcln = " SELECT * From PageMaster Where  PageId='" + dr["PageId"].ToString() + "' ";
                    string finalstr = strcln ;
                    SqlCommand cmdcln = new SqlCommand(finalstr, con);
                    DataTable dtcln = new DataTable();
                    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);
                    if (dtcln.Rows.Count > 0)
                    {
                        
                        try
                        {
                            string FolderName = "" + dtcln.Rows[0]["FolderName"].ToString() + "/";//" + Request.Url.Host.ToString() + "/
                            Response.Redirect("~/" + FolderName + "" + dtcln.Rows[0]["PageName"].ToString() + "?Pageidforsendreport=" + dtcln.Rows[0]["PageId"].ToString() + "");
                        }
                        catch (Exception ex)
                        {
                            Label2.Text = "FolderName";

                        }
                        //string te = "" + FolderName + "" + dtcln.Rows[0]["PageName"].ToString() + "?Pageidforsendreport=" + dtcln.Rows[0]["PageId"].ToString() + "";
                        // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + te + "', 'height=5,width=5,status=no,toolbar=no,menubar=no,location=no, scrollbars=yes' );", true);
                        // string te = "~/ioffice/ShoppingCart/Admin/TimerSendMail.aspx";
                        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '"+te+"', 'height=5,width=5,status=no,toolbar=no,menubar=no,location=no, scrollbars=yes' );", true);
                    }
                }
            }
        }
    }



  
  
    
}
