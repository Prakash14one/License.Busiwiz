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


public partial class UserContorls_UC_Header : System.Web.UI.UserControl
{
    string ClientId1;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    string page;
    protected string verid;
    protected void Page_Load(object sender, EventArgs e)
    {
        

        string strData = Request.Url.LocalPath.ToString();

        char[] separator = new char[] { '/' };
        string version = "VersionFolder";
        Boolean isvesionfolder = false;
        string[] strSplitArr = strData.Split(separator);

        int i = Convert.ToInt32(strSplitArr.Length);
        page = strSplitArr[i - 1].ToString();
        Session["pagename"] = page.ToString();
        foreach (string x in strSplitArr)
        {
            if (x.Contains(version))
            {
                isvesionfolder = true;
            }
        }
        string DomainName1 = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

        Uri myUri = new Uri("http://license.busiwiz.com/");
        string host = myUri.Host;

        string domainName = host.Split(new char[] { '.', '.' })[1];
        string domainName1 = host.Split(new char[] { '.', '.' })[2];
        string gg = "" + domainName + "." + domainName1 + "";

        string str = " select distinct PortalMasterTbl.*,ClientMaster.CompanyName ,StateMasterTbl.StateName,CountryMaster.CountryName from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId=ProductMaster.ProductId inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  where Upper(PortalMasterTbl.PortalName)='" + gg + "'  ";//3-15 


        //  string str = " select ClientMaster.*,ProductMaster.ProductId,PortalMasterTbl.LogoPath  from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId where ProductMaster.ProductId='" + Request.QueryString["id"] + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            Image1.ImageUrl = "~/images/" + dt.Rows[0]["LogoPath"].ToString() + " ";
            //Label7.Text = dt.Rows[0]["Supportteamphoneno"].ToString();

            Label10.Text = dt.Rows[0]["Supportteamemailid"].ToString();
        }


      


            if (isvesionfolder == false)
            {

            }
        
    
      if (!IsPostBack)
        {
           
            if (Request.Url.ToString().ToLower().Contains("productprofile.aspx"))
            {
                
            }
        }
    }

    
  
}
