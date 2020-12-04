using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Common;
using System.Xml;
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using System.Security.Cryptography;

public partial class CandidateMain : System.Web.UI.MasterPage
{
    //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        string strData = Request.Url.LocalPath.ToString();
        char[] separator = new char[] { '/' };
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["pagename"] = page.ToString();
       
        if (!IsPostBack)
        {
            

         //   FillLogos();

        }
        
    }
    //protected void FillLogos()
    //{
    //    string strMainRedirect = " SELECT     CompanyWebsitMaster.Sitename, CompanyAddressMaster.WebSite, CompanyMaster.CompanyId, CompanyMaster.CompanyLogo, " +
    //                 "  CompanyWebsiteAddressMaster.LiveChatUrl,CompanyMaster.CompanyName " +
    //                 " FROM         CompanyWebsitMaster LEFT OUTER JOIN " +
    //                 " CompanyWebsiteAddressMaster ON  " +
    //                 " CompanyWebsitMaster.CompanyWebsiteMasterId = CompanyWebsiteAddressMaster.CompanyWebsiteMasterId RIGHT OUTER JOIN " +
    //                 " CompanyMaster ON CompanyWebsitMaster.CompanyId = CompanyMaster.CompanyId LEFT OUTER JOIN " +
    //                 " CompanyAddressMaster ON CompanyMaster.CompanyId = CompanyAddressMaster.CompanyMasterId where CompanyMaster.compid='" + Session["comid"] + "'  ";
    //    SqlCommand cmdRedirect = new SqlCommand(strMainRedirect, con);
    //    SqlDataAdapter adpRedirect = new SqlDataAdapter(cmdRedirect);
    //    DataTable dtRedirect = new DataTable();
    //    adpRedirect.Fill(dtRedirect);
    //    if (dtRedirect.Rows.Count > 0)
    //    {

    //        mainloginlogo.ImageUrl = "~/ShoppingCart/images/" + dtRedirect.Rows[0]["CompanyLogo"].ToString();
    //        // RightImage.ImageUrl = "~/ShoppingCart/images/"+dtRedirect.Rows[0]["LiveChatUrl"].ToString();

    //    }
    //    else
    //    {

    //        mainloginlogo.ImageUrl = "#";
    //        // RightImage.ImageUrl = "#";

    //    }

    //}
   
}
