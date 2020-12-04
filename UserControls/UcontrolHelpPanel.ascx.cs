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

public partial class ShoppingCart_Admin_UserControls_UC_Title : System.Web.UI.UserControl
{
   // MasterCls clsMaster = new MasterCls();
    DataTable dt;
    //SqlConnection con;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

       
        if (Session["PageName"] != null)
        {
            string pagename = Session["PageName"].ToString();
            if (Session["verId"] != null)
            {
                string str121f = " SELECT PageMaster.PageId,  PageMaster.PageName, PageMaster.PageTitle, PageMaster.PageDescription from PageMaster  where    PageMaster.PageName='" + Session["PageName"].ToString() + "'  and PageMaster.VersionInfoMasterId= '" + Session["verId"].ToString() + "' ";
                SqlDataAdapter da121f = new SqlDataAdapter(str121f, con);
                DataTable dt121f = new DataTable();
                da121f.Fill(dt121f);



                if (dt121f.Rows.Count > 0)
                {
                    lblDetail.Text = dt121f.Rows[0]["PageDescription"].ToString();
                    lbltitle.Text = dt121f.Rows[0]["PageTitle"].ToString();
                    pnlhelp.Visible = true;
                    PNLTITLE.Visible = true;

                }
                else
                {
                    lbltitle.Text = "";
                    lblDetail.Text = "";
                    PNLTITLE.Visible = false;
                    pnlhelp.Visible = false;
                }
            }
        }
        else
        {
            lblDetail.Text = "";
            pnlhelp.Visible = false;
        }
        
        
    }
  
   }
