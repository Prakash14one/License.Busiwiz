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

public partial class Master_mp_Admin : System.Web.UI.MasterPage
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ToString());
    // SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString1"].ToString());
    SqlConnection con = new SqlConnection(PageConn.connnn);
    SqlConnection con1;
    private Control myC;
    protected string priceid;
    protected string verid;
    string pageiddd;
    //HttpCookieCollection cook;
    protected void Page_Load(object sender, EventArgs e)
    {
        imgsitel.ImageUrl = "~/images/onlineaccounts.jpg";
        int mast = 0;
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        if (con.ConnectionString == "" || con.ConnectionString == null)
        {
            PageConn pgcon = new PageConn();
            con = pgcon.dynconn;
        }
        if (PageConn.busdatabase == "" || PageConn.busdatabase == null)
        {
            PageConn.licenseconn();
        }
        con1 = PageConn.licenseconn();

        if (PageConn.bidname == "" || PageConn.bidname == null)
        {
            PageConn.busclient();
        }
        ModalPopupExtender1.Hide();
        ModalPopupExtender1222.Hide();
        string strData;
      
             strData = Request.Url.LocalPath.ToString();
             char[] separator = new char[] { '/' };

             string[] strSplitArr = strData.Split(separator);
             int i = Convert.ToInt32(strSplitArr.Length);
             string page = strSplitArr[i - 1].ToString();
             Session["pagename"] = page.ToString();
      


        String pageurl = Request.Url.AbsoluteUri;
        try
        {
            if (Session["purl"] != null)
            {
                string rr = Session["purl"].ToString();
                string rr1 = Request.Url.AbsoluteUri.ToString();
            }
            if (!IsPostBack)
            {
                imgsitel.ImageUrl = Convert.ToString(Session["ownur"]);
                string cid = "select CompanyMaster.PricePlanId from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId  where CompanyLoginId='" + Session["Comid"].ToString() + "'";


                SqlCommand cidco = new SqlCommand(cid, con1);
                if (con1.State.ToString() != "Open")
                {
                    con1.Open();
                }
                object objco = cidco.ExecuteScalar();

                Session["PriceId"] = objco.ToString();
                if (Session["PriceId"] != "")
                {
                }
                else
                {
                    Response.Redirect("http://members.ijobcenter.com/PricePlan.aspx");  
                    return; 
                }
                string cidv = "select distinct VersionInfoId FROM CompanyMaster inner join ProductMaster on ProductMaster.ProductId=CompanyMaster.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId where CompanyMaster.CompanyLoginId='" + Session["Comid"].ToString() + "'";
                SqlCommand cidcov = new SqlCommand(cidv, con1);
                if (con1.State.ToString() != "Open")
                {
                    con1.Open();
                }
                object objcov = cidcov.ExecuteScalar();
                con1.Close();
                verid = objcov.ToString();
                Session["verId"] = objcov.ToString();
            }
        }
        catch
        {
        }



        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(60));
        Response.Cache.SetValidUntilExpires(true);
        //RestrictPriceplan(page);
        RestrictionRole(page);

        RestrictPriceplan1(page);
     
        // PopulateMenu();
        if (!IsPostBack)
        {

            // ViewState["ret"] = Request.UrlReferrer.ToString();
            if (Request.UrlReferrer != null)
            {

                ViewState["p1"] = Request.UrlReferrer.ToString();

            }
          

        }
        SqlConnection con141 =new SqlConnection();
        con141.ConnectionString = "Data Source =TCP:72.38.84.230,30000; Initial Catalog = jobcenter.OADB; User ID=Sa; Password=06De1963++; Persist Security Info=true;";
         string str141 = " Select distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.DesignationMasterId from DesignationMaster inner join EmployeeMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join " + PageConn.busdatabase + ".dbo.User_master on " + PageConn.busdatabase + ".dbo.User_master.PartyID=Party_master.PartyID where " + PageConn.busdatabase + ".dbo.User_master.UserID='" + Session["userid"] + "'";
         str141 = " Select distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.DesignationMasterId from DesignationMaster inner join EmployeeMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join [jobcenter.OADB].dbo.User_master on [jobcenter.OADB].dbo.User_master.PartyID=Party_master.PartyID where [jobcenter.OADB].dbo.User_master.UserID='" + Session["userid"] + "'";
         SqlCommand cmd141 = new SqlCommand(str141, con141);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmd141);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            Session["EmployeeId"] = Convert.ToString(dteeed.Rows[0]["EmployeeMasterID"]);
            Session["DesignationId"] = Convert.ToString(dteeed.Rows[0]["DesignationMasterId"]);
        }
    }
    private void PopulateMenu()
    {
        DataSet ds = new DataSet();
       
             ds = GetDataSetForMenu();
        //Menu menu = new Menu();
        MenuItem catMasterhome = new MenuItem((string)"Home");

        Menu1.Items.Add(catMasterhome);

        //MenuItem childrenItem = new MenuItem(ClsEncDesc.DecDyn((string)childItem["SubMenuName"]));

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["parent"].Rows.Count > 0)
            {
                foreach (DataRow parentItem in ds.Tables["parent"].Rows)
                {
                    int f1 = 0;

                    int f3 = 0;
                    MenuItem categoryItem = new MenuItem(ClsEncDesc.DecDyn((string)parentItem["MainMenuName"]));
                    //Menu1.Items.Add(categoryItem);
                    if (categoryItem.Text != "0")
                    {
                        foreach (DataRow childItem in parentItem.GetChildRows("Children"))
                        {
                            int f2 = 0;
                            if (f1 == 0)
                            {
                                Menu1.Items.Add(categoryItem);
                                f3 += 1;
                                f1 += 1;
                            }
                            MenuItem childrenItem = new MenuItem(ClsEncDesc.DecDyn((string)childItem["SubMenuName"]));


                            foreach (DataRow subchildItem in childItem.GetChildRows("Children2"))
                            {

                                if (f2 == 0)
                                {
                                    categoryItem.ChildItems.Add(childrenItem);

                                    f2 += 1;
                                }
                                MenuItem childrenItem111 = new MenuItem((string)subchildItem["PageId"].ToString());
                                MenuItem childrenItem11 = new MenuItem(ClsEncDesc.DecDyn((string)subchildItem["PageTitle"]));
                                MenuItem childrenItem112 = new MenuItem((string)subchildItem["PageName"]);
                                childrenItem11.NavigateUrl = "~/Shoppingcart/admin/" + ClsEncDesc.DecDyn(childrenItem112.Text);

                                childrenItem.ChildItems.Add(childrenItem11);

                            }

                        }
                        foreach (DataRow childItem in parentItem.GetChildRows("Children3"))
                        {
                            if (f3 == 0)
                            {
                                Menu1.Items.Add(categoryItem);
                                f3 += 1;
                            }
                            MenuItem childrenItem111 = new MenuItem((string)childItem["PageId"].ToString());
                            MenuItem childrenItem11 = new MenuItem(ClsEncDesc.DecDyn((string)childItem["PageTitle"]));
                            MenuItem childrenItem112 = new MenuItem((string)childItem["PageName"]);
                            childrenItem11.NavigateUrl = "~/Shoppingcart/admin/" + ClsEncDesc.DecDyn(childrenItem112.Text);

                            categoryItem.ChildItems.Add(childrenItem11);

                        }
                    }

                }
            }
            else
            {
                int bb = 0;
                foreach (DataRow parentItem in ds.Tables["parent"].Rows)
                {

                    MenuItem categoryItem = new MenuItem(ClsEncDesc.DecDyn((string)parentItem["MainMenuName"]));

                    //Menu1.Items.Add(categoryItem);
                    if (categoryItem.Text != "0")
                    {
                        foreach (DataRow childItem in parentItem.GetChildRows("Children"))
                        {

                            MenuItem childrenItem = new MenuItem(ClsEncDesc.DecDyn((string)childItem["SubMenuName"]));

                            Menu1.Items.Add(childrenItem);
                            foreach (DataRow subchildItem in childItem.GetChildRows("Children2"))
                            {





                                MenuItem childrenItem111 = new MenuItem((string)subchildItem["PageId"].ToString());
                                MenuItem childrenItem11 = new MenuItem(ClsEncDesc.DecDyn((string)subchildItem["PageTitle"]));
                                MenuItem childrenItem112 = new MenuItem((string)subchildItem["PageName"]);
                                childrenItem11.NavigateUrl = "~/Shoppingcart/admin/" + ClsEncDesc.DecDyn(childrenItem112.Text);

                                childrenItem.ChildItems.Add(childrenItem11);

                            }
                            if (bb == 0)
                            {
                                foreach (DataRow childItem1 in parentItem.GetChildRows("Children3"))
                                {

                                    MenuItem childrenItem111 = new MenuItem((string)childItem1["PageId"].ToString());
                                    MenuItem childrenItem11 = new MenuItem(ClsEncDesc.DecDyn((string)childItem1["PageTitle"].ToString()));
                                    MenuItem childrenItem112 = new MenuItem((string)childItem1["PageName"]);
                                    childrenItem11.NavigateUrl = "~/Shoppingcart/admin/" + ClsEncDesc.DecDyn(childrenItem112.Text);

                                    childrenItem.ChildItems.Add(childrenItem11);

                                }
                                bb += 1;
                            }

                        }

                    }

                }
            }

        }
        MenuItem catMasterLogout = new MenuItem((string)"Log Out");

        Menu1.Items.Add(catMasterLogout);
    }



    private DataSet GetDataSetForMenu()
    {
        int masterpageid = 2031;
        string main1 = "";
        string main2 = "";
        string main3 = "";
        string manuid = "";
        string rsubmanu = "";
        string rpagemanu = "";
        string submanuid = "";

        string mcount = "0";
        string scount = "0";
        // Check
      
       
        string str141 = "Select * From  Party_master where PartyID='" + Session["PartyId"] + "'";
        SqlDataAdapter adp141 = new SqlDataAdapter(str141, con);
        DataTable da141 = new DataTable();
        adp141.Fill(da141);
        if (da141.Rows.Count > 0)
        {
            Session["partyno"] = da141.Rows[0]["PartyTypeCategoryNo"].ToString();
            string str142 = "SELECT  [PartyTypeId] ,[PartType]  ,[compid]  ,[PartyCategoryId] FROM [jobcenter.OADB].[dbo].[PartytTypeMaster] where PartyCategoryId='" + Session["partyno"] + "' and compid='" + Session["Comid"] + "'";
            SqlDataAdapter adp142 = new SqlDataAdapter(str142, con);
            DataTable da142 = new DataTable();
            adp142.Fill(da142);
            if (da142.Rows.Count > 0)
            {
                lbl_admin.Text = da142.Rows[0]["PartType"].ToString();
                string str143 = " SELECT  [RoleId]  ,[RoleName]  ,[VersionId] FROM [License.Busiwiz].[dbo].[DefaultRole] where Rolename='" + lbl_admin.Text + "' and VersionId='" + verid + "'";
                SqlDataAdapter adp143 = new SqlDataAdapter(str143, PageConn.licenseconn());
                DataTable da143 = new DataTable();
                adp143.Fill(da143);
                if (da143.Rows.Count > 0)
                {
                    Session["RoleID"] = da143.Rows[0]["RoleId"].ToString();
                   
                }
            }
        }
       
        //string str121f = "SELECT " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId FROM WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId=MainMenuMaster.MainMenuId INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID WHERE WebsiteMaster.VersionInfoId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.AccessRight<>'0' ";
        //SqlDataAdapter da121f = new SqlDataAdapter(str121f, PageConn.busclient());
        //DataTable dt121f = new DataTable();
        //da121f.Fill(dt121f);
        //1st
        string str121f = "SELECT " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId FROM WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId=MainMenuMaster.MainMenuId INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID WHERE   " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "' and " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.AccessRight<>'0' ";
       
        SqlDataAdapter da121f = new SqlDataAdapter(str121f, PageConn.busclient());  // PageConn.busclient()
        DataTable dt121f = new DataTable();
        da121f.Fill(dt121f);

        foreach (DataRow dts in dt121f.Rows)
        {
            main1 += "'" + dts["MenuId"] + "',";
            manuid += "'" + dts["MenuId"] + "',";
        }
        if (main1.Length > 0)
        {
            main1 = main1.Remove(main1.Length - 1);

        }
        string pm = "";
        
     
     //2nd
        string str1211f1 = "SELECT dbo.PageMaster.MainMenuId AS MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL) THEN '0' ELSE PageMaster.SubMenuId END AS SubMenuId, dbo.PageMaster.PageId, dbo.PageMaster.VersionInfoMasterId FROM dbo.DefaultRolewisePageAccess INNER JOIN  dbo.PageMaster ON dbo.DefaultRolewisePageAccess.PageId = dbo.PageMaster.PageId WHERE        (dbo.DefaultRolewisePageAccess.PriceplanId = '" + Session["PriceId"] + "') AND (dbo.DefaultRolewisePageAccess.RoleId = '" + Session["RoleID"] + "')";//ppid5405
        str1211f1 = " SELECT dbo.PageMaster.MainMenuId AS MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL) THEN '0' ELSE PageMaster.SubMenuId END AS SubMenuId, dbo.PageMaster.PageId, dbo.PageMaster.VersionInfoMasterId, dbo.PageMaster.PageTypeId, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageDescription, dbo.PageMaster.PageIndex, dbo.PageMaster.FolderName, dbo.PageMaster.Active, dbo.PageMaster.SubMenuId AS Expr1, dbo.PageMaster.LanguageId, dbo.PageMaster.ManuAccess, dbo.MainMenuMaster.MasterPage_Id FROM   dbo.DefaultRolewisePageAccess INNER JOIN   dbo.PageMaster ON dbo.DefaultRolewisePageAccess.PageId = dbo.PageMaster.PageId INNER JOIN  dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId WHERE   (dbo.DefaultRolewisePageAccess.PriceplanId = '" + Session["PriceId"] + "') AND (dbo.DefaultRolewisePageAccess.RoleId = '" + Session["RoleID"] + "') AND dbo.MainMenuMaster.MasterPage_Id='" + masterpageid + "' ";
        str1211f1 = " SELECT dbo.PageMaster.MainMenuId AS MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL) THEN '0' ELSE PageMaster.SubMenuId END AS SubMenuId, dbo.PageMaster.PageId, dbo.PageMaster.VersionInfoMasterId, dbo.PageMaster.PageTypeId, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageDescription, dbo.PageMaster.PageIndex, dbo.PageMaster.FolderName, dbo.PageMaster.Active, dbo.PageMaster.SubMenuId AS Expr1, dbo.PageMaster.LanguageId, dbo.PageMaster.ManuAccess, dbo.MainMenuMaster.MasterPage_Id FROM   dbo.DefaultRolewisePageAccess INNER JOIN   dbo.PageMaster ON dbo.DefaultRolewisePageAccess.PageId = dbo.PageMaster.PageId INNER JOIN  dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId WHERE   (dbo.DefaultRolewisePageAccess.PriceplanId = '" + Session["PriceId"] + "') AND (dbo.DefaultRolewisePageAccess.RoleId = '" + Session["RoleID"] + "') AND dbo.MainMenuMaster.MasterPage_Id='" + masterpageid + "' and dbo.PageMaster.Active='1' and dbo.PageMaster.ManuAccess='1' Order By dbo.PageMaster.PageIndex   ";
        SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, PageConn.licenseconn());//PageConn.busclient());
        DataTable dt121f1 = new DataTable();
        da121f1.Fill(dt121f1);
        foreach (DataRow dts in dt121f1.Rows)
        {
            if (mcount != Convert.ToString(dts["MenuId"]))
            {
                mcount = Convert.ToString(dts["MenuId"]);
                main2 += "'" + dts["MenuId"] + "',";
                manuid += "'" + dts["MenuId"] + "',";

            }
            if (scount != Convert.ToString(dts["SubMenuId"]))
            {
                scount = Convert.ToString(dts["SubMenuId"]);

                rsubmanu += "'" + dts["SubMenuId"] + "',";
            }
            rpagemanu += "'" + dts["PageId"] + "',";

        }
        //rpagemanu = "";
        if (main2.Length > 0)
        {

            main2 = main2.Remove(main2.Length - 1);

        }
        //if (rpagemanu.Length > 0)
        //{

        //    rpagemanu = rpagemanu.Remove(rpagemanu.Length - 1);

        //}
        //3rd
        //19-5-15       string str1211f11 = " SELECT SubMenuMaster.MainMenuId as MenuId,SubMenuMaster.SubMenuId FROM WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join  SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId  inner join " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID WHERE    " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'";
       //20-5-15 string str1211f11 = "SELECT        dbo.SubMenuMaster.MainMenuId AS MenuId, dbo.SubMenuMaster.SubMenuId FROM            dbo.WebsiteMaster INNER JOIN                         dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteMasterId = dbo.WebsiteMaster.ID INNER JOIN                         dbo.MasterPageMaster ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN                         dbo.MainMenuMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN                         dbo.SubMenuMaster ON dbo.SubMenuMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId WHERE        (dbo.MainMenuMaster.MainMenuId IN ('2186', '4206', '2188', '2184', '2182'))";
      
        string str1211f11 = "SELECT      distinct  dbo.PageMaster.MainMenuId AS MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL) THEN '0' ELSE PageMaster.SubMenuId END AS SubMenuId FROM            dbo.DefaultRolewisePageAccess INNER JOIN                         dbo.PageMaster ON dbo.DefaultRolewisePageAccess.PageId = dbo.PageMaster.PageId WHERE        (dbo.DefaultRolewisePageAccess.PriceplanId = '"+ Session["PriceId"]  +"') AND (dbo.DefaultRolewisePageAccess.RoleId = '" + Session["RoleID"] + "') AND  dbo.PageMaster.MainMenuId !=12322";//ppid5405"
        str1211f11 = " SELECT  dbo.PageMaster.MainMenuId AS MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL) THEN '0' ELSE PageMaster.SubMenuId END AS SubMenuId, dbo.PageMaster.PageId, dbo.PageMaster.VersionInfoMasterId, dbo.PageMaster.PageTypeId, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageDescription, dbo.PageMaster.PageIndex, dbo.PageMaster.FolderName, dbo.PageMaster.Active, dbo.PageMaster.SubMenuId AS Expr1, dbo.PageMaster.LanguageId, dbo.PageMaster.ManuAccess, dbo.MainMenuMaster.MasterPage_Id FROM   dbo.DefaultRolewisePageAccess INNER JOIN   dbo.PageMaster ON dbo.DefaultRolewisePageAccess.PageId = dbo.PageMaster.PageId INNER JOIN  dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId WHERE   (dbo.DefaultRolewisePageAccess.PriceplanId = '" + Session["PriceId"] + "') AND (dbo.DefaultRolewisePageAccess.RoleId = '" + Session["RoleID"] + "') AND dbo.MainMenuMaster.MasterPage_Id='" + masterpageid + "' ";
        str1211f11 = " SELECT DISTINCT dbo.PageMaster.MainMenuId AS MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL) THEN '0' ELSE PageMaster.SubMenuId END AS SubMenuId, dbo.MainMenuMaster.MasterPage_Id FROM dbo.DefaultRolewisePageAccess INNER JOIN dbo.PageMaster ON dbo.DefaultRolewisePageAccess.PageId = dbo.PageMaster.PageId INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId WHERE (dbo.DefaultRolewisePageAccess.PriceplanId = '" + Session["PriceId"] + "') AND (dbo.DefaultRolewisePageAccess.RoleId = '" + Session["RoleID"] + "') AND (dbo.MainMenuMaster.MasterPage_Id = '" + masterpageid + "' and  dbo.PageMaster.Active='1' and dbo.PageMaster.ManuAccess='1' ) ";
        SqlDataAdapter da121f11 = new SqlDataAdapter(str1211f11,PageConn.licenseconn());// PageConn.busclient());
        DataTable dt121f11 = new DataTable();
        da121f11.Fill(dt121f11);
        foreach (DataRow dts in dt121f11.Rows)
        {
            main3 += "'" + dts["MenuId"] + "',";
            manuid += "'" + dts["MenuId"] + "',";
            rsubmanu += "'" + dts["SubMenuId"] + "',";

        }
        if (main3.Length > 0)
        {
            main3 = main3.Remove(main3.Length - 1);

        }
        if (manuid.Length > 0)
        {
            manuid = manuid.Remove(manuid.Length - 1);

        }
        if (rsubmanu.Length > 0)
        {
            rsubmanu = rsubmanu.Remove(rsubmanu.Length - 1);

        }
        DataSet ds = new DataSet();
        if (manuid.Length > 0)
        //4th

        {
            string str = "SELECT distinct MainMenuMaster.* FROM MainMenuMaster inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId where MainMenuMaster.MainMenuId in(" + manuid + ") and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1)  Order by MainMenuIndex ASC";
             str = "SELECT distinct MainMenuMaster.* FROM MainMenuMaster inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId where MainMenuMaster.MainMenuId in(" + manuid + ") and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1)  and dbo.PageMaster.Active='1'  Order by MainMenuIndex ASC";

            SqlDataAdapter adpcat = new SqlDataAdapter(str, PageConn.licenseconn());// PageConn.busclient());

            adpcat.Fill(ds, "parent");



            string str11 = "";
            if (main1.Length == 0)
            {
                if (manuid.Length > 0)
                {
                    main1 = manuid;
                }
            }
            else
            {
                string ax = "SELECT distinct MainMenuMaster.*,PageId FROM MainMenuMaster inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId where MainMenuMaster.MainMenuId in(" + main1 + ") and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) Order by MainMenuIndex ASC";

                DataTable drtc = new DataTable();
                SqlDataAdapter asc = new SqlDataAdapter(ax, PageConn.busclient());
                asc.Fill(drtc);
                foreach (DataRow dts in drtc.Rows)
                {

                    rpagemanu += "'" + dts["PageId"] + "',";
                }
            }
            if (rsubmanu.Length != 0)
            {
                if (main1.Length != 0)
                {
                    //5th
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and  (MainMenuMaster.MainMenuId In( " + main1 + ") or SubMenuMaster.SubMenuId in(" + rsubmanu + ")) Order by SubMenuIndex ASC";
                    
                }
                else
                {
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and  SubMenuMaster.SubMenuId in(" + rsubmanu + ") Order by SubMenuIndex ASC";
                }
            }
            else
            {
                if (main1.Length != 0)
                {
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and  MainMenuMaster.MainMenuId In( " + main1 + ") Order by SubMenuIndex ASC";
                }
                else if (manuid.Length != 0)
                {
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and  MainMenuMaster.MainMenuId In(" + manuid + ") Order by SubMenuIndex ASC";

                }
            }

            SqlDataAdapter adpProduct = new SqlDataAdapter(str11, PageConn.licenseconn());// PageConn.busclient());
            adpProduct.Fill(ds, "child");
            foreach (DataRow dts in ds.Tables["child"].Rows)
            {
                submanuid += "'" + dts["SubMenuId"] + "',";

            }
            if (submanuid.Length != 0)
            {
                submanuid = submanuid.Remove(submanuid.Length - 1);

            }


            if (rpagemanu.Length > 0)
            {

                rpagemanu = rpagemanu.Remove(rpagemanu.Length - 1);

            }
            string str15 = "";
            if (manuid.Length != 0 && rpagemanu.Length != 0)
            {
                //6th
               // str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' and  PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "' AND (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + manuid + ")) and PageMaster.PageId in(" + rpagemanu + ") order by PageIndex ASC";
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' and  PageMaster.VersionInfoMasterId='" + verid + "' AND (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + manuid + ")) and PageMaster.PageId in(" + rpagemanu + ") order by PageIndex ASC";
            }
            else if (main1.Length != 0)
            {
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "' AND (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + main1 + ")) order by PageIndex ASC";

            }

            else if (manuid.Length != 0)
            {
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' and  PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "' AND (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + manuid + ")) order by PageIndex ASC";
            }
            SqlDataAdapter adp115 = new SqlDataAdapter(str15, PageConn.licenseconn());// PageConn.busclient());
            DataSet ds125 = new DataSet();
            adp115.Fill(ds, "leafchild1");
            string str1 = "";

            if (rpagemanu.Length > 0)
            {

                if (submanuid.Length > 0)
                {
                //    7th
                   //15-5-15 str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'AND (PageMaster.SubMenuId in(" + submanuid + ")) and (PageMaster.PageId in(" + rpagemanu + "))order by PageIndex  Asc";
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + Session["PriceId"].ToString() + "'AND (PageMaster.SubMenuId in(" + submanuid + ")) and (PageMaster.PageId in(" + rpagemanu + "))order by PageIndex  Asc";

                }
                else
                {
                    //15-5-15 str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'AND PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "'  and (PageMaster.PageId in(" + rpagemanu + ")) order by PageIndex ASC";
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + Session["PriceId"].ToString() + "'AND PageMaster.VersionInfoMasterId='" + verid + "'  and (PageMaster.PageId in(" + rpagemanu + ")) order by PageIndex ASC";

                }
                SqlDataAdapter adp11 = new SqlDataAdapter(str1,PageConn.licenseconn());// PageConn.busclient());
                DataSet ds12 = new DataSet();
                adp11.Fill(ds, "leafchild");
            }
            else
            {
                if (submanuid.Length > 0)
                {
                   //15-5-15 str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON  PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' AND PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "'  and  (PageMaster.SubMenuId in(" + submanuid + ")) order by PageIndex ASC";
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON  PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" +Session["PriceId"].ToString() + "' AND PageMaster.VersionInfoMasterId='" + verid + "'  and  (PageMaster.SubMenuId in(" + submanuid + ")) order by PageIndex ASC";
                    SqlDataAdapter adp11 = new SqlDataAdapter(str1, PageConn.licenseconn());
                    DataSet ds12 = new DataSet();
                    adp11.Fill(ds, "leafchild");
                }
                else
                {
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' AND PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "'  and   (PageMaster.SubMenuId in(" + 00 + ")) and (PageMaster.PageId =0) order by PageIndex ASC";
                    SqlDataAdapter adp11 = new SqlDataAdapter(str1, PageConn.licenseconn());// PageConn.busclient());
                    DataSet ds12 = new DataSet();
                    adp11.Fill(ds, "leafchild");
                    ds.Tables["leafchild"].Rows.Add(0, 0, 0, 0, 0);
                }

            }

            ds.Tables["parent"].Rows.Add(0, 0, 0, 0, 0);
            ds.Tables["child"].Rows.Add(0, 0, 0, 0, 0);

            // ds.Tables["leafchild1"].Rows.Add(0, 0, 0, 0, 0);
            ds.Relations.Add("Children", ds.Tables["parent"].Columns["MainMenuId"], ds.Tables["child"].Columns["MainMenuId"]);
            ds.Relations.Add("Children2", ds.Tables["child"].Columns["SubMenuId"], ds.Tables["leafchild"].Columns["SubMenuId"]);
            ds.Relations.Add("Children3", ds.Tables["parent"].Columns["MainMenuId"], ds.Tables["leafchild1"].Columns["MainMenuId"]);
        }
        return ds;
        
    }

    private DataSet GetDataSetForMenu1()
    {

        string main1 = "";
        string main2 = "";
        string main3 = "";
        string manuid = "";
        string rsubmanu = "";
        string rpagemanu = "";
        string submanuid = "";

        string mcount = "0";
        string scount = "0";
        // Check
        SqlConnection licenc = new SqlConnection();
        licenc.ConnectionString = "Data Source=192.168.1.219,2810;Initial Catalog=License.Busiwiz;Integrated Security=True;";
        SqlConnection conoadb = new SqlConnection();
         // conoadb.ConnectionString = "Data Source =C3\C3SERVERMASTER; Initial Catalog = jobcenter.OADB; User ID=Sa; Password=06De1963++; Persist Security Info=true;";
        conoadb .ConnectionString = "Data Source =192.168.1.219\\C3SERVERMASTER,30000; Initial Catalog = jobcenter.OADB; User ID=sa; Password=06De1963++; Persist Security Info=true;";

            //Data Source =C3\C3SERVERMASTER; Initial Catalog = jobcenter.OADB; User ID=Sa; Password=06De1963++; Persist Security Info=true;
        string str141 = "Select * From  Party_master where PartyID='" + Session["PartyId"] + "'";
        SqlDataAdapter adp141 = new SqlDataAdapter(str141, con);
        DataTable da141 = new DataTable();
        adp141.Fill(da141);
        if (da141.Rows.Count > 0)
        {
            Session["partyno"] = da141.Rows[0]["PartyTypeCategoryNo"].ToString();

            string str142 = " SELECT  [PartyTypeId] ,[PartType]  ,[compid]  ,[PartyCategoryId] FROM [jobcenter.OADB].[dbo].[PartytTypeMaster] where PartyCategoryId='" + Session["partyno"] + "' and compid='" + Session["Comid"] + "'";
        SqlDataAdapter adp142 = new SqlDataAdapter(str142, con);
        DataTable da142 = new DataTable();
        adp142.Fill(da142);
        if (da142.Rows.Count > 0)
        {
            lbl_admin.Text = da142.Rows[0]["PartType"].ToString();
            string str143 = " SELECT  [RoleId]  ,[RoleName]  ,[VersionId] FROM [License.Busiwiz].[dbo].[DefaultRole] where Rolename='" + lbl_admin.Text + "' and VersionId='" + verid + "'";
            SqlDataAdapter adp143 = new SqlDataAdapter(str143, PageConn.licenseconn());
            DataTable da143 = new DataTable();
            adp143.Fill(da143);
            if (da143.Rows.Count > 0)
            {
                Session["RoleID"] = da143.Rows[0]["RoleId"].ToString();
            }
        }
        }
       
             //string str121f = "SELECT " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId FROM WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId=MainMenuMaster.MainMenuId INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID WHERE WebsiteMaster.VersionInfoId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.AccessRight<>'0' ";
        //SqlDataAdapter da121f = new SqlDataAdapter(str121f, PageConn.busclient());
        //DataTable dt121f = new DataTable();
        //da121f.Fill(dt121f);
        //1st
        string str121f = "SELECT " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId FROM WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId=MainMenuMaster.MainMenuId INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID WHERE   " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "' and " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.AccessRight<>'0' ";
        SqlDataAdapter da121f = new SqlDataAdapter(str121f, PageConn.busclient());
        DataTable dt121f = new DataTable();
        da121f.Fill(dt121f);

        foreach (DataRow dts in dt121f.Rows)
        {
            main1 += "'" + dts["MenuId"] + "',";
            manuid += "'" + dts["MenuId"] + "',";
        }
        if (main1.Length > 0)
        {
            main1 = main1.Remove(main1.Length - 1);

        }
        string pm = "";
       
        //18-5-15 string str1211f1 = "SELECT PageMaster.MainMenuId as MenuId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,PageMaster.PageId FROM PageMaster inner join " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl on " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PageId=PageMaster.PageId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID WHERE PageMaster.PageName not in('AttendenceDeviations.aspx','AttendenceApproval.aspx') and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "' AND " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'";
        //2nd
        string str1211f1 = "SELECT        dbo.PageMaster.MainMenuId AS MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL) THEN '0' ELSE PageMaster.SubMenuId END AS SubMenuId, dbo.PageMaster.PageId,                          dbo.PageMaster.VersionInfoMasterId FROM            dbo.DefaultRolewisePageAccess INNER JOIN                          dbo.PageMaster ON dbo.DefaultRolewisePageAccess.PageId = dbo.PageMaster.PageId WHERE        (dbo.DefaultRolewisePageAccess.PriceplanId = '5402') AND (dbo.DefaultRolewisePageAccess.RoleId = '4265')";//ppid5405
        SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, PageConn.licenseconn());//PageConn.busclient());
        DataTable dt121f1 = new DataTable();
        da121f1.Fill(dt121f1);
        foreach (DataRow dts in dt121f1.Rows)
        {
            if (mcount != Convert.ToString(dts["MenuId"]))
            {
                mcount = Convert.ToString(dts["MenuId"]);
                main2 += "'" + dts["MenuId"] + "',";
                manuid += "'" + dts["MenuId"] + "',";

            }
            if (scount != Convert.ToString(dts["SubMenuId"]))
            {
                scount = Convert.ToString(dts["SubMenuId"]);

                rsubmanu += "'" + dts["SubMenuId"] + "',";
            }
            rpagemanu += "'" + dts["PageId"] + "',";

        }
        //rpagemanu = "";
        if (main2.Length > 0)
        {

            main2 = main2.Remove(main2.Length - 1);

        }
        //if (rpagemanu.Length > 0)
        //{

        //    rpagemanu = rpagemanu.Remove(rpagemanu.Length - 1);

        //}
        //3rd
        //19-5-15       string str1211f11 = " SELECT SubMenuMaster.MainMenuId as MenuId,SubMenuMaster.SubMenuId FROM WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join  SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId  inner join " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID WHERE    " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'";
        string str1211f11 = "SELECT        dbo.SubMenuMaster.MainMenuId AS MenuId, dbo.SubMenuMaster.SubMenuId FROM            dbo.WebsiteMaster INNER JOIN                         dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteMasterId = dbo.WebsiteMaster.ID INNER JOIN                         dbo.MasterPageMaster ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN                         dbo.MainMenuMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN                         dbo.SubMenuMaster ON dbo.SubMenuMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId WHERE        (dbo.MainMenuMaster.MainMenuId IN ('2186', '4206', '2188', '2184', '2182'))";
        SqlDataAdapter da121f11 = new SqlDataAdapter(str1211f11, PageConn.licenseconn());// PageConn.busclient());
        DataTable dt121f11 = new DataTable();
        da121f11.Fill(dt121f11);
        foreach (DataRow dts in dt121f11.Rows)
        {
            main3 += "'" + dts["MenuId"] + "',";
            manuid += "'" + dts["MenuId"] + "',";
            rsubmanu += "'" + dts["SubMenuId"] + "',";

        }
        if (main3.Length > 0)
        {
            main3 = main3.Remove(main3.Length - 1);

        }
        if (manuid.Length > 0)
        {
            manuid = manuid.Remove(manuid.Length - 1);

        }
        if (rsubmanu.Length > 0)
        {
            rsubmanu = rsubmanu.Remove(rsubmanu.Length - 1);

        }
        DataSet ds = new DataSet();
        if (manuid.Length > 0)
        //4th
        {
            string str = "SELECT distinct MainMenuMaster.* FROM MainMenuMaster inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId where MainMenuMaster.MainMenuId in(" + manuid + ") and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) Order by MainMenuIndex ASC";

            SqlDataAdapter adpcat = new SqlDataAdapter(str, PageConn.licenseconn());// PageConn.busclient());

            adpcat.Fill(ds, "parent");



            string str11 = "";
            if (main1.Length == 0)
            {
                if (manuid.Length > 0)
                {
                    main1 = manuid;
                }
            }
            else
            {
                string ax = "SELECT distinct MainMenuMaster.*,PageId FROM MainMenuMaster inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId where MainMenuMaster.MainMenuId in(" + main1 + ") and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) Order by MainMenuIndex ASC";

                DataTable drtc = new DataTable();
                SqlDataAdapter asc = new SqlDataAdapter(ax, PageConn.busclient());
                asc.Fill(drtc);
                foreach (DataRow dts in drtc.Rows)
                {

                    rpagemanu += "'" + dts["PageId"] + "',";
                }
            }
            if (rsubmanu.Length != 0)
            {
                if (main1.Length != 0)
                {
                    //5th
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and  (MainMenuMaster.MainMenuId In( " + main1 + ") or SubMenuMaster.SubMenuId in(" + rsubmanu + ")) Order by SubMenuIndex ASC";
                }
                else
                {
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and  SubMenuMaster.SubMenuId in(" + rsubmanu + ") Order by SubMenuIndex ASC";
                }
            }
            else
            {
                if (main1.Length != 0)
                {
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and  MainMenuMaster.MainMenuId In( " + main1 + ") Order by SubMenuIndex ASC";
                }
                else if (manuid.Length != 0)
                {
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and  MainMenuMaster.MainMenuId In(" + manuid + ") Order by SubMenuIndex ASC";

                }
            }

            SqlDataAdapter adpProduct = new SqlDataAdapter(str11, PageConn.licenseconn());// PageConn.busclient());
            adpProduct.Fill(ds, "child");
            foreach (DataRow dts in ds.Tables["child"].Rows)
            {
                submanuid += "'" + dts["SubMenuId"] + "',";

            }
            if (submanuid.Length != 0)
            {
                submanuid = submanuid.Remove(submanuid.Length - 1);

            }


            if (rpagemanu.Length > 0)
            {

                rpagemanu = rpagemanu.Remove(rpagemanu.Length - 1);

            }
            string str15 = "";
            if (manuid.Length != 0 && rpagemanu.Length != 0)
            {
                //6th
                // str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' and  PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "' AND (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + manuid + ")) and PageMaster.PageId in(" + rpagemanu + ") order by PageIndex ASC";
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' and  PageMaster.VersionInfoMasterId='" + verid + "' AND (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + manuid + ")) and PageMaster.PageId in(" + rpagemanu + ") order by PageIndex ASC";
            }
            else if (main1.Length != 0)
            {
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "' AND (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + main1 + ")) order by PageIndex ASC";

            }

            else if (manuid.Length != 0)
            {
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' and  PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "' AND (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + manuid + ")) order by PageIndex ASC";
            }
            SqlDataAdapter adp115 = new SqlDataAdapter(str15, PageConn.licenseconn());// PageConn.busclient());
            DataSet ds125 = new DataSet();
            adp115.Fill(ds, "leafchild1");
            string str1 = "";

            if (rpagemanu.Length > 0)
            {

                if (submanuid.Length > 0)
                {
                    //    7th
                    //15-5-15 str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'AND (PageMaster.SubMenuId in(" + submanuid + ")) and (PageMaster.PageId in(" + rpagemanu + "))order by PageIndex  Asc";
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + Session["PriceId"].ToString() + "'AND (PageMaster.SubMenuId in(" + submanuid + ")) and (PageMaster.PageId in(" + rpagemanu + "))order by PageIndex  Asc";

                }
                else
                {
                    //15-5-15 str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'AND PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "'  and (PageMaster.PageId in(" + rpagemanu + ")) order by PageIndex ASC";
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + Session["PriceId"].ToString() + "'AND PageMaster.VersionInfoMasterId='" + verid + "'  and (PageMaster.PageId in(" + rpagemanu + ")) order by PageIndex ASC";

                }
                SqlDataAdapter adp11 = new SqlDataAdapter(str1, PageConn.licenseconn());// PageConn.busclient());
                DataSet ds12 = new DataSet();
                adp11.Fill(ds, "leafchild");
            }
            else
            {
                if (submanuid.Length > 0)
                {
                    //15-5-15 str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON  PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' AND PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "'  and  (PageMaster.SubMenuId in(" + submanuid + ")) order by PageIndex ASC";
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON  PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + Session["PriceId"].ToString() + "' AND PageMaster.VersionInfoMasterId='" + verid + "'  and  (PageMaster.SubMenuId in(" + submanuid + ")) order by PageIndex ASC";
                    SqlDataAdapter adp11 = new SqlDataAdapter(str1, PageConn.busclient());
                    DataSet ds12 = new DataSet();
                    adp11.Fill(ds, "leafchild");
                }
                else
                {
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' AND PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "'  and   (PageMaster.SubMenuId in(" + 00 + ")) and (PageMaster.PageId =0) order by PageIndex ASC";
                    SqlDataAdapter adp11 = new SqlDataAdapter(str1, PageConn.licenseconn());// PageConn.busclient());
                    DataSet ds12 = new DataSet();
                    adp11.Fill(ds, "leafchild");
                    ds.Tables["leafchild"].Rows.Add(0, 0, 0, 0, 0);
                }

            }

            ds.Tables["parent"].Rows.Add(0, 0, 0, 0, 0);
            ds.Tables["child"].Rows.Add(0, 0, 0, 0, 0);

            // ds.Tables["leafchild1"].Rows.Add(0, 0, 0, 0, 0);
            ds.Relations.Add("Children", ds.Tables["parent"].Columns["MainMenuId"], ds.Tables["child"].Columns["MainMenuId"]);
            ds.Relations.Add("Children2", ds.Tables["child"].Columns["SubMenuId"], ds.Tables["leafchild"].Columns["SubMenuId"]);
            ds.Relations.Add("Children3", ds.Tables["parent"].Columns["MainMenuId"], ds.Tables["leafchild1"].Columns["MainMenuId"]);
        }
        return ds;
    }
    string GetBackground()
    {
        return Request.Cookies["Background"].Value;
    }
    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
    protected DataTable selectbusdy(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, PageConn.busclient());
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
    protected void lnk5_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/ShoppingCart/Admin/ShoppingCartLogin.aspx");
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //Session["ctrl"] = Panel123;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

    }
    protected void FillLogos()
    {


        string strMainRedirect = "select warehousemaster.warehouseid,employeemaster.EmployeeName from warehousemaster inner join employeemaster on employeemaster.whid=warehousemaster.warehouseid where employeemaster.employeemasterid='" + Session["EmployeeId"] + "'";
        SqlDataAdapter adpRedirect = new SqlDataAdapter(strMainRedirect, con);
        DataTable dtRedirect = new DataTable();
        adpRedirect.Fill(dtRedirect);

        if (dtRedirect.Rows.Count > 0)
        {
            SqlDataAdapter dafff = new SqlDataAdapter("select LogoUrl,SiteUrl from CompanyWebsitMaster where whid='" + Convert.ToString(dtRedirect.Rows[0]["warehouseid"]) + "'", con);
            DataTable dtfff = new DataTable();
            dafff.Fill(dtfff);

            if (dtfff.Rows.Count > 0)
            {
                mainloginlogo.ImageUrl = "~/ShoppingCart/images/" + dtfff.Rows[0]["LogoUrl"].ToString();
            }
            else
            {
                // mainloginlogo.ImageUrl = imgsitel.ImageUrl;

                mainloginlogo.ImageUrl = "~/images/OALOGO.jpg";

                //   mainloginlogo.ImageUrl = "~/ShoppingCart/images/timekeeperlogo.jpg";
            }

            Label2.Text = "" + Convert.ToString(dtRedirect.Rows[0]["EmployeeName"]);
            string straddr = "select Distinct CompanyWebsiteAddressMaster.*,WareHouseMaster.Name as BName,CityMasterTbl.CityName,StateMasterTbl.Statename,CountryMaster.CountryName from CompanyWebsiteAddressMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId inner join CountryMaster on " +
                   "CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country inner join StateMasterTbl on " +
                   "StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CityMasterTbl on " +
                   "CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City where CompanyWebsiteAddressMaster.CompanyWebsiteMasterId='" + Convert.ToString(dtRedirect.Rows[0]["warehouseid"]) + "' and AddressTypeMaster.Name='Business Address' ";
            SqlDataAdapter adpaddr = new SqlDataAdapter(straddr, con);
            DataTable dtaddr = new DataTable();
            adpaddr.Fill(dtaddr);

            if (dtaddr.Rows.Count > 0)
            {
                busn.Text ="ID: "+ Convert.ToString(dtaddr.Rows[0]["BName"]);
                lbladdr.Text = "Account Type: " + Convert.ToString(dtaddr.Rows[0]["BName"]) + ", " + Convert.ToString(dtaddr.Rows[0]["Address1"]) + ", " + Convert.ToString(dtaddr.Rows[0]["CityName"]) + ", " + Convert.ToString(dtaddr.Rows[0]["Statename"]) + ", " + Convert.ToString(dtaddr.Rows[0]["CountryName"]);

                if (Convert.ToString(dtaddr.Rows[0]["Zip"]) != "")
                {
                    lbladdr.Text = lbladdr.Text + ", " + Convert.ToString(dtaddr.Rows[0]["Zip"]);
                }

                if (Convert.ToString(dtaddr.Rows[0]["Phone1"]) != "")
                {
                    lbladdr.Text = lbladdr.Text + ", " + Convert.ToString(dtaddr.Rows[0]["Phone1"]);
                }
                if (Convert.ToString(dtaddr.Rows[0]["Email"]) != "")
                {
                    lbladdr.Text = lbladdr.Text + ", " + Convert.ToString(dtaddr.Rows[0]["Email"]);
                }
                if (Convert.ToString(dtfff.Rows[0]["SiteUrl"]) != "")
                {
                    lbladdr.Text = lbladdr.Text + ", " + Convert.ToString(dtfff.Rows[0]["SiteUrl"]);
                }
            }


        }
    }
    public void GetControls(Control c, string FindControl)
    {


        foreach (Control cc in c.Controls)
        {

            if (cc.ID == FindControl)
            {
                myC = cc;
                break;

            }

            if (cc.Controls.Count > 0)
                GetControls(cc, FindControl);


        }

    }
    private string Decrypt(string strText, string strEncrypt)
    {
        byte[] bKey = new byte[20];
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string decryptstring(string str)
    {
        return Decrypt(str, "&%#@?,:*");
    }
    public void RestrictionRole(string pg)
    {
        //PricePlanMasterID;
        if (!IsPostBack)
        {

            if (Session.Count > 0 || Session["userid"].ToString() != "")
            {

                //if (Request.UrlReferrer.ToString() != "")
                //{
                if (Request.UrlReferrer != null)
                {
                    ViewState["p1"] = Request.UrlReferrer.ToString();

                    string str11 = "Select PageId from PageMaster where PageName='" + ClsEncDesc.EncDyn(pg) + "'";
                    SqlCommand cmd = new SqlCommand(str11, PageConn.busclient());
                    SqlDataAdapter da11 = new SqlDataAdapter(cmd);
                    DataTable dt11 = new DataTable();
                    
                    da11.Fill(dt11);
                    try
                    {
                        if (dt11.Rows.Count > 0)
                        {
                            if (Session.Count > 0 || Session["userid"].ToString() != "")
                            {

                                string userroledeactive = "SELECT ActiveDeactive, Role_id, User_id, " + PageConn.busdatabase + ".dbo.User_Role.User_Role_id FROM " + PageConn.busdatabase + ".dbo.User_Role WHERE User_id ='" + Session["userid"] + "'";
                                SqlCommand cmd1 = new SqlCommand(userroledeactive, con);
                                SqlDataAdapter da111 = new SqlDataAdapter(cmd1);
                                DataTable dt111 = new DataTable();
                                da111.Fill(dt111);
                                if (dt111.Rows.Count > 0)
                                {
                                    if (dt111.Rows[0][0].ToString() == "True")
                                    {

                                        lblpagemsg.Text = pg.ToString() + " Page";
                                        if (dt11.Rows.Count > 0)
                                        {
                                            string str1211 = "";
                                            SqlDataAdapter da1211;
                                            DataTable dt1211;
                                            str1211 = "SELECT distinct  " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.*, PageControlMaster.PageControl_id,Control_type_Master.Type_id, Control_type_Master.Type_name,PageControlMaster.ControlName FROM " + PageConn.busdatabase + ".dbo.User_Role inner join  " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl on " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.RoleId=" + PageConn.busdatabase + ".dbo.User_Role.Role_id inner join PageMaster on PageMaster.PageId=" + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PageId Inner join   PageControlMaster on PageControlMaster.Page_id=PageMaster.PageId INNER JOIN Control_type_Master ON PageControlMaster.ControlType_id = Control_type_Master.Type_id where  PageMaster.PageId='" + dt11.Rows[0][0] + "' and " + PageConn.busdatabase + ".dbo.User_Role.User_id='" + Session["userid"] + "'";
                                            int kb = 0;
                                            da1211 = new SqlDataAdapter(str1211, PageConn.busclient());
                                            dt1211 = new DataTable();
                                            da1211.Fill(dt1211);
                                            if (dt1211.Rows.Count > 0)
                                            {
                                                kb = 1;
                                            }
                                            if (kb == 0)
                                            {
                                                str1211 = "SELECT distinct  " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.*,PageControlMaster.PageControl_id,Control_type_Master.Type_id, Control_type_Master.Type_name,PageControlMaster.ControlName FROM " + PageConn.busdatabase + ".dbo.User_Role inner join  " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.RoleId=" + PageConn.busdatabase + ".dbo.User_Role.Role_id inner join PageMaster on PageMaster.SubMenuId=" + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId Inner join   PageControlMaster on PageControlMaster.Page_id=PageMaster.PageId INNER JOIN Control_type_Master ON PageControlMaster.ControlType_id = Control_type_Master.Type_id where  PageMaster.PageId='" + dt11.Rows[0][0] + "' and " + PageConn.busdatabase + ".dbo.User_Role.User_id='" + Session["userid"] + "'";

                                                da1211 = new SqlDataAdapter(str1211, PageConn.busclient());
                                                dt1211 = new DataTable();
                                                da1211.Fill(dt1211);
                                                if (dt1211.Rows.Count > 0)
                                                {
                                                    kb = 1;
                                                }
                                            }
                                            if (kb == 0)
                                            {
                                                str1211 = "SELECT   distinct  " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.*, PageControlMaster.PageControl_id,Control_type_Master.Type_id, Control_type_Master.Type_name,PageControlMaster.ControlName FROM " + PageConn.busdatabase + ".dbo.User_Role inner join  " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.RoleId=" + PageConn.busdatabase + ".dbo.User_Role.Role_id inner join PageMaster on PageMaster.MainMenuId=" + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId Inner join   PageControlMaster on PageControlMaster.Page_id=PageMaster.PageId INNER JOIN Control_type_Master ON PageControlMaster.ControlType_id = Control_type_Master.Type_id where  PageMaster.PageId='" + dt11.Rows[0][0] + "' and " + PageConn.busdatabase + ".dbo.User_Role.User_id='" + Session["userid"] + "'";

                                                da1211 = new SqlDataAdapter(str1211, PageConn.busclient());
                                                dt1211 = new DataTable();
                                                da1211.Fill(dt1211);
                                                if (dt1211.Rows.Count > 0)
                                                {
                                                    kb = 1;
                                                }
                                            }

                                            if (kb != 0)
                                            {
                                                int i1;
                                                for (i1 = 0; i1 <= dt1211.Rows.Count - 1; i1++)
                                                {
                                                    //string str1311 = "Select Type_Name from Control_type_Master where Type_id=" + dt1211.Rows[i1]["Type_id"];
                                                    //SqlDataAdapter da1311 = new SqlDataAdapter(str1311, con);
                                                    //DataTable dt1311 = new DataTable();
                                                    //da1311.Fill(dt1311);

                                                    String StrVal = dt1211.Rows[i1]["Type_Name"].ToString();
                                                    String StrName = dt1211.Rows[i1]["ControlName"].ToString();
                                                    //if (StrVal == "GridView")
                                                    //{
                                                    //    GetControls(this, StrName);
                                                    //    GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));

                                                    //    string stacess = "SELECT * from PageSubControl inner join Role_Page_Contreol_Access on Role_Page_Contreol_Access.Id=PageSubControl.Role_Page_Contreol_Access_Id where PageSubControl.Role_Page_Contreol_Access_Id='" + dt1211.Rows[i1][4] + "'";
                                                    //    SqlCommand cmdaccess = new SqlCommand(stacess, con);
                                                    //    SqlDataAdapter daaccess = new SqlDataAdapter(cmdaccess);
                                                    //    DataTable dsaccess = new DataTable();
                                                    //    daaccess.Fill(dsaccess);
                                                    //    int ii1;
                                                    //    for (ii1 = 0; ii1 <= dsaccess.Rows.Count - 1; ii1++)
                                                    //    {

                                                    //        if (dsaccess.Rows[ii1]["Active"].ToString() == "False")
                                                    //        {
                                                    //            for (int hr = 0; hr < myLabel.Columns.Count; hr++)
                                                    //            {
                                                    //                if (myLabel.Columns[hr].HeaderText == dsaccess.Rows[ii1]["SubControlName"].ToString())
                                                    //                {

                                                    //                    myLabel.Columns[hr].Visible = false;
                                                    //                }
                                                    //            }
                                                    //        }

                                                    //    }
                                                    //    myLabel.Enabled = true;

                                                    //}


                                                    if (dt1211.Rows[i1]["Insert_Right"].ToString() == "False" && dt1211.Rows[i1]["Type_Name"].ToString() == "Insert")
                                                    {
                                                        GetControls(this, StrName);
                                                        Control c = ContentPlaceHolder1.FindControl(StrName);
                                                        if (c is Button)
                                                        {

                                                            Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is LinkButton)
                                                        {
                                                            LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is ImageButton)
                                                        {
                                                            ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is GridView)
                                                        {
                                                            GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                for (int hr = 0; hr < myLabel.Columns.Count; hr++)
                                                                {
                                                                    if (myLabel.Columns[hr].HeaderText == Convert.ToString(dt1211.Rows[i1]["Type_Name"]))
                                                                    {

                                                                        myLabel.Columns[hr].Visible = false;
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }


                                                    else if (dt1211.Rows[i1]["Edit_Right"].ToString() == "False" && dt1211.Rows[i1]["Type_Name"].ToString() == "Edit")
                                                    {
                                                        GetControls(this, StrName);
                                                        Control c = ContentPlaceHolder1.FindControl(StrName);
                                                        if (c is Button)
                                                        {

                                                            Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is LinkButton)
                                                        {
                                                            LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is ImageButton)
                                                        {
                                                            ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is GridView)
                                                        {
                                                            GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                for (int hr = 0; hr < myLabel.Columns.Count; hr++)
                                                                {
                                                                    if (myLabel.Columns[hr].HeaderText.ToString() == dt1211.Rows[i1]["Type_Name"].ToString())
                                                                    {

                                                                        myLabel.Columns[hr].Visible = false;
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }
                                                    else if (dt1211.Rows[i1]["Delete_Right"].ToString() == "False" && dt1211.Rows[i1]["Type_Name"].ToString() == "Delete")
                                                    {

                                                        GetControls(this, StrName);
                                                        Control c = ContentPlaceHolder1.FindControl(StrName);
                                                        if (c is Button)
                                                        {

                                                            Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is LinkButton)
                                                        {
                                                            LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is ImageButton)
                                                        {
                                                            ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is GridView)
                                                        {
                                                            GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                for (int hr = 0; hr < myLabel.Columns.Count; hr++)
                                                                {
                                                                    if (myLabel.Columns[hr].HeaderText.ToString() == Convert.ToString(dt1211.Rows[i1]["Type_Name"]))
                                                                    {

                                                                        myLabel.Columns[hr].Visible = false;
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }

                                                    else if (dt1211.Rows[i1]["Download_Right"].ToString() == "False" && dt1211.Rows[i1]["Type_Name"].ToString() == "Download")
                                                    {

                                                        GetControls(this, StrName);
                                                        Control c = ContentPlaceHolder1.FindControl(StrName);
                                                        if (c is Button)
                                                        {

                                                            Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is LinkButton)
                                                        {
                                                            LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is ImageButton)
                                                        {
                                                            ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is GridView)
                                                        {
                                                            GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                for (int hr = 0; hr < myLabel.Columns.Count; hr++)
                                                                {
                                                                    if (myLabel.Columns[hr].HeaderText == Convert.ToString(dt1211.Rows[i1]["Type_Name"]))
                                                                    {

                                                                        myLabel.Columns[hr].Visible = false;
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }
                                                    else if (dt1211.Rows[i1]["Update_Right"].ToString() == "False" && dt1211.Rows[i1]["Type_Name"].ToString() == "Update")
                                                    {

                                                        GetControls(this, StrName);
                                                        Control c = ContentPlaceHolder1.FindControl(StrName);
                                                        if (c is Button)
                                                        {

                                                            Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is LinkButton)
                                                        {
                                                            LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is ImageButton)
                                                        {
                                                            ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is GridView)
                                                        {
                                                            GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                for (int hr = 0; hr < myLabel.Columns.Count; hr++)
                                                                {
                                                                    if (myLabel.Columns[hr].HeaderText == Convert.ToString(dt1211.Rows[i1]["Type_Name"]))
                                                                    {

                                                                        myLabel.Columns[hr].Visible = false;
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }
                                                    else if (dt1211.Rows[i1]["View_Right"].ToString() == "False" && dt1211.Rows[i1]["Type_Name"].ToString() == "View")
                                                    {

                                                        GetControls(this, StrName);
                                                        Control c = ContentPlaceHolder1.FindControl(StrName);
                                                        if (c is Button)
                                                        {

                                                            Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is LinkButton)
                                                        {
                                                            LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is ImageButton)
                                                        {
                                                            ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is GridView)
                                                        {
                                                            GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                for (int hr = 0; hr < myLabel.Columns.Count; hr++)
                                                                {
                                                                    if (myLabel.Columns[hr].HeaderText == Convert.ToString(dt1211.Rows[i1]["Type_Name"]))
                                                                    {

                                                                        myLabel.Columns[hr].Visible = false;
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }
                                                    else if (dt1211.Rows[i1]["Go_Right"].ToString() == "False" && dt1211.Rows[i1]["Type_Name"].ToString() == "Go")
                                                    {
                                                        GetControls(this, StrName);
                                                        Control c = ContentPlaceHolder1.FindControl(StrName);
                                                        if (c is Button)
                                                        {

                                                            Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is LinkButton)
                                                        {
                                                            LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is ImageButton)
                                                        {
                                                            ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is GridView)
                                                        {
                                                            GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                for (int hr = 0; hr < myLabel.Columns.Count; hr++)
                                                                {
                                                                    if (myLabel.Columns[hr].HeaderText == Convert.ToString(dt1211.Rows[i1]["Type_Name"]))
                                                                    {

                                                                        myLabel.Columns[hr].Visible = false;
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }
                                                    else if (dt1211.Rows[i1]["SendMail_Right"].ToString() == "False" && dt1211.Rows[i1]["Type_Name"].ToString() == "SendMail")
                                                    {

                                                        GetControls(this, StrName);
                                                        Control c = ContentPlaceHolder1.FindControl(StrName);
                                                        if (c is Button)
                                                        {

                                                            Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is LinkButton)
                                                        {
                                                            LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is ImageButton)
                                                        {
                                                            ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                myLabel.Enabled = false;

                                                            }
                                                        }
                                                        else if (c is GridView)
                                                        {
                                                            GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                                                            if (myLabel.ID == Convert.ToString(StrName))
                                                            {
                                                                for (int hr = 0; hr < myLabel.Columns.Count; hr++)
                                                                {
                                                                    if (myLabel.Columns[hr].HeaderText == Convert.ToString(dt1211.Rows[i1]["Type_Name"]))
                                                                    {

                                                                        myLabel.Columns[hr].Visible = false;
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }
                                                }

                                            }

                                        }



                                        else
                                        {
                                            Panel2.BackColor = System.Drawing.Color.Lavender;

                                            Label1.ForeColor = System.Drawing.Color.Black;
                                            Label4.ForeColor = System.Drawing.Color.Black;
                                            // LinkButton15.BackColor = System.Drawing.Color.Blue;
                                            // ImageButton3.Visible = false;
                                            ModalPopupExtender1.Show();
                                        }
                                    }

                                    else
                                    {
                                        Response.Redirect("ShoppingCartLogin.aspx");
                                    }

                                }

                            }

                            else
                            {
                                Response.Redirect("ShoppingCartLogin.aspx");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }




                }

                else
                {
                    //Session.Remove("userid");
                    //Session["userid"] = "";
                    //Session.Clear();
                    //Session.Abandon();
                    //Session.RemoveAll();
                    //Response.Redirect("ShoppingCartLogin.aspx");


                }
            }


        }


    }

    public void RestrictPriceplan(string pg)
    {

        if (!IsPostBack)
        {
            if (Session.Count > 0 || Session["userid"].ToString() != "")
            {


                if (Request.UrlReferrer != null)
                {
                    ViewState["p1"] = Request.UrlReferrer.ToString();
                    string str = "SELECT  LUPD,MP, CID, PID, V FROM Lmaster";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    da.Fill(dt);

                    //int PPlan;







                    {
                        string str11 = "Select PageId from PageMaster where PageName='" + pg + "'";
                        SqlCommand cmd = new SqlCommand(str11, con);
                        SqlDataAdapter da11 = new SqlDataAdapter(cmd);
                        DataTable dt11 = new DataTable();
                        da11.Fill(dt11);

                        if (dt11.Rows.Count > 0)
                        {

                            {





                                {

                                    {

                                    }

                                    {
                                        string str1211 = "Select PageControl_id,ControlName,ActiveDeactive,ControlType_id from PageControlMaster where  Page_id=" + dt11.Rows[0][0];
                                        SqlDataAdapter da1211 = new SqlDataAdapter(str1211, con);
                                        DataTable dt1211 = new DataTable();
                                        da1211.Fill(dt1211);




                                        int i1;
                                        for (i1 = 0; i1 <= dt1211.Rows.Count - 1; i1++)
                                        {


                                            if (dt1211.Rows[i1][2].ToString() == "False")
                                            {
                                                string str1311 = "Select Type_Name from Control_type_Master where Type_id=" + dt1211.Rows[i1][3];
                                                SqlDataAdapter da1311 = new SqlDataAdapter(str1311, con);
                                                DataTable dt1311 = new DataTable();
                                                da1311.Fill(dt1311);

                                                String StrVal = dt1311.Rows[0][0].ToString();
                                                String StrName = dt1211.Rows[i1][1].ToString();
                                                GetControls(this, StrName);
                                                if (StrVal == "RadioButtonList")
                                                {
                                                    RadioButtonList myLabel = (RadioButtonList)Convert.ChangeType(myC, typeof(RadioButtonList));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ListBox")
                                                {
                                                    ListBox myLabel = (ListBox)Convert.ChangeType(myC, typeof(ListBox));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "HyperLink")
                                                {
                                                    HyperLink myLabel = (HyperLink)Convert.ChangeType(myC, typeof(HyperLink));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "LinkButton")
                                                {
                                                    LinkButton myLabel = (LinkButton)Convert.ChangeType(myC, typeof(LinkButton));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ImageButton")
                                                {
                                                    ImageButton myLabel = (ImageButton)Convert.ChangeType(myC, typeof(ImageButton));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "TextBox")
                                                {
                                                    TextBox myLabel = (TextBox)Convert.ChangeType(myC, typeof(TextBox));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "Button")
                                                {
                                                    Button myLabel = (Button)Convert.ChangeType(myC, typeof(Button));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "Panel")
                                                {
                                                    Panel myLabel = (Panel)Convert.ChangeType(myC, typeof(Panel));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CheckBox")
                                                {
                                                    CheckBox myLabel = (CheckBox)Convert.ChangeType(myC, typeof(CheckBox));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CheckBoxList")
                                                {
                                                    CheckBoxList myLabel = (CheckBoxList)Convert.ChangeType(myC, typeof(CheckBoxList));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "Label")
                                                {
                                                    Label myLabel = (Label)Convert.ChangeType(myC, typeof(Label));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "Image")
                                                {
                                                    Image myLabel = (Image)Convert.ChangeType(myC, typeof(Image));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ImageMap")
                                                {
                                                    ImageMap myLabel = (ImageMap)Convert.ChangeType(myC, typeof(ImageMap));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "RadioButton")
                                                {
                                                    RadioButton myLabel = (RadioButton)Convert.ChangeType(myC, typeof(RadioButton));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DropDownList")
                                                {
                                                    DropDownList myLabel = (DropDownList)Convert.ChangeType(myC, typeof(DropDownList));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "FileUpload")
                                                {
                                                    FileUpload myLabel = (FileUpload)Convert.ChangeType(myC, typeof(FileUpload));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "PlaceHolder")
                                                {
                                                    PlaceHolder myLabel = (PlaceHolder)Convert.ChangeType(myC, typeof(PlaceHolder));
                                                    myLabel.Visible = false;

                                                }
                                                else if (StrVal == "GridView")
                                                {
                                                    GridView myLabel = (GridView)Convert.ChangeType(myC, typeof(GridView));
                                                    myLabel.Enabled = false;

                                                }

                                                else if (StrVal == "DataList")
                                                {
                                                    DataList myLabel = (DataList)Convert.ChangeType(myC, typeof(DataList));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "DetailsView")
                                                {
                                                    DetailsView myLabel = (DetailsView)Convert.ChangeType(myC, typeof(DetailsView));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "FormView")
                                                {
                                                    FormView myLabel = (FormView)Convert.ChangeType(myC, typeof(FormView));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "Repeater")
                                                {
                                                    Repeater myLabel = (Repeater)Convert.ChangeType(myC, typeof(Repeater));
                                                    myLabel.Visible = false;

                                                }
                                                else if (StrVal == "ListView")
                                                {
                                                    ListView myLabel = (ListView)Convert.ChangeType(myC, typeof(ListView));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "DataPager")
                                                {
                                                    DataPager myLabel = (DataPager)Convert.ChangeType(myC, typeof(DataPager));
                                                    myLabel.Visible = false;

                                                }
                                                else if (StrVal == "LinqDataSource")
                                                {
                                                    LinqDataSource myLabel = (LinqDataSource)Convert.ChangeType(myC, typeof(LinqDataSource));
                                                    myLabel.Visible = false;

                                                }
                                                else if (StrVal == "RangeValidator")
                                                {
                                                    RangeValidator myLabel = (RangeValidator)Convert.ChangeType(myC, typeof(RangeValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "RangeValidator")
                                                {
                                                    RangeValidator myLabel = (RangeValidator)Convert.ChangeType(myC, typeof(RangeValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "RegularExpressionValidator")
                                                {
                                                    RegularExpressionValidator myLabel = (RegularExpressionValidator)Convert.ChangeType(myC, typeof(RegularExpressionValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "RequiredFieldValidator")
                                                {
                                                    RequiredFieldValidator myLabel = (RequiredFieldValidator)Convert.ChangeType(myC, typeof(RequiredFieldValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "CompareValidator")
                                                {
                                                    CompareValidator myLabel = (CompareValidator)Convert.ChangeType(myC, typeof(CompareValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "CustomValidator")
                                                {
                                                    CustomValidator myLabel = (CustomValidator)Convert.ChangeType(myC, typeof(CustomValidator));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "ValidationSummary")
                                                {
                                                    ValidationSummary myLabel = (ValidationSummary)Convert.ChangeType(myC, typeof(ValidationSummary));
                                                    myLabel.Enabled = false;

                                                }
                                                else if (StrVal == "SiteMapPath")
                                                {
                                                    SiteMapPath myLabel = (SiteMapPath)Convert.ChangeType(myC, typeof(SiteMapPath));
                                                    myLabel.Enabled = false;
                                                }

                                                else if (StrVal == "TreeView")
                                                {
                                                    TreeView myLabel = (TreeView)Convert.ChangeType(myC, typeof(TreeView));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "WebPart")
                                                {
                                                    WebPart myLabel = (WebPart)Convert.ChangeType(myC, typeof(WebPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ProxyWebPart")
                                                {
                                                    ProxyWebPart myLabel = (ProxyWebPart)Convert.ChangeType(myC, typeof(ProxyWebPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "WebPartZone")
                                                {
                                                    WebPartZone myLabel = (WebPartZone)Convert.ChangeType(myC, typeof(WebPartZone));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CatalogPart")
                                                {
                                                    CatalogPart myLabel = (CatalogPart)Convert.ChangeType(myC, typeof(CatalogPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CatalogZone")
                                                {
                                                    CatalogZone myLabel = (CatalogZone)Convert.ChangeType(myC, typeof(CatalogZone));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DeclarativeCatalogPart")
                                                {
                                                    DeclarativeCatalogPart myLabel = (DeclarativeCatalogPart)Convert.ChangeType(myC, typeof(DeclarativeCatalogPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "PageCatalogPart")
                                                {
                                                    PageCatalogPart myLabel = (PageCatalogPart)Convert.ChangeType(myC, typeof(PageCatalogPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ImportCatalogPart")
                                                {
                                                    ImportCatalogPart myLabel = (ImportCatalogPart)Convert.ChangeType(myC, typeof(ImportCatalogPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "EditorZone")
                                                {
                                                    EditorZone myLabel = (EditorZone)Convert.ChangeType(myC, typeof(EditorZone));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "AppearanceEditorPart")
                                                {
                                                    AppearanceEditorPart myLabel = (AppearanceEditorPart)Convert.ChangeType(myC, typeof(AppearanceEditorPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "BehaviorEditorPart")
                                                {
                                                    BehaviorEditorPart myLabel = (BehaviorEditorPart)Convert.ChangeType(myC, typeof(BehaviorEditorPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "LayoutEditorPart")
                                                {
                                                    LayoutEditorPart myLabel = (LayoutEditorPart)Convert.ChangeType(myC, typeof(LayoutEditorPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "PropertyGridEditorPart")
                                                {
                                                    PropertyGridEditorPart myLabel = (PropertyGridEditorPart)Convert.ChangeType(myC, typeof(PropertyGridEditorPart));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ConnectionsZone")
                                                {
                                                    ConnectionsZone myLabel = (ConnectionsZone)Convert.ChangeType(myC, typeof(ConnectionsZone));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "UpdateProgress")
                                                {
                                                    UpdateProgress myLabel = (UpdateProgress)Convert.ChangeType(myC, typeof(UpdateProgress));
                                                    myLabel.Visible = false;
                                                }
                                                else if (StrVal == "Timer")
                                                {
                                                    Timer myLabel = (Timer)Convert.ChangeType(myC, typeof(Timer));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "UpdatePanel")
                                                {
                                                    UpdatePanel myLabel = (UpdatePanel)Convert.ChangeType(myC, typeof(UpdatePanel));
                                                    myLabel.Visible = false;
                                                }
                                                else if (StrVal == "Accordion")
                                                {
                                                    Accordion myLabel = (Accordion)Convert.ChangeType(myC, typeof(Accordion));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "UserControl")
                                                {
                                                    UserControl myLabel = (UserControl)Convert.ChangeType(myC, typeof(UserControl));
                                                    myLabel.Visible = false;
                                                }
                                                else if (StrVal == "AccordionPane")
                                                {
                                                    AccordionPane myLabel = (AccordionPane)Convert.ChangeType(myC, typeof(AccordionPane));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "AlwaysVisibleControlExtender")
                                                {
                                                    AlwaysVisibleControlExtender myLabel = (AlwaysVisibleControlExtender)Convert.ChangeType(myC, typeof(AlwaysVisibleControlExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "AnimationExtender")
                                                {
                                                    AnimationExtender myLabel = (AnimationExtender)Convert.ChangeType(myC, typeof(AnimationExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "AutoCompleteExtender")
                                                {
                                                    AutoCompleteExtender myLabel = (AutoCompleteExtender)Convert.ChangeType(myC, typeof(AutoCompleteExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CalendarExtender")
                                                {
                                                    CalendarExtender myLabel = (CalendarExtender)Convert.ChangeType(myC, typeof(CalendarExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CascadingDropDown")
                                                {
                                                    CascadingDropDown myLabel = (CascadingDropDown)Convert.ChangeType(myC, typeof(CascadingDropDown));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "CollapsiblePanelExtender")
                                                {
                                                    CollapsiblePanelExtender myLabel = (CollapsiblePanelExtender)Convert.ChangeType(myC, typeof(CollapsiblePanelExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ConfirmButtonExtender")
                                                {
                                                    ConfirmButtonExtender myLabel = (ConfirmButtonExtender)Convert.ChangeType(myC, typeof(ConfirmButtonExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DragPanelExtender")
                                                {
                                                    DragPanelExtender myLabel = (DragPanelExtender)Convert.ChangeType(myC, typeof(DragPanelExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DropDownExtender")
                                                {
                                                    DropDownExtender myLabel = (DropDownExtender)Convert.ChangeType(myC, typeof(DropDownExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DropShadowExtender")
                                                {
                                                    DropShadowExtender myLabel = (DropShadowExtender)Convert.ChangeType(myC, typeof(DropShadowExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "DynamicPopulateExtender")
                                                {
                                                    DynamicPopulateExtender myLabel = (DynamicPopulateExtender)Convert.ChangeType(myC, typeof(DynamicPopulateExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "FilteredTextBoxExtender")
                                                {
                                                    FilteredTextBoxExtender myLabel = (FilteredTextBoxExtender)Convert.ChangeType(myC, typeof(FilteredTextBoxExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "HoverExtender")
                                                {
                                                    HoverExtender myLabel = (HoverExtender)Convert.ChangeType(myC, typeof(HoverExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "HoverMenuExtender")
                                                {
                                                    HoverMenuExtender myLabel = (HoverMenuExtender)Convert.ChangeType(myC, typeof(HoverMenuExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ListSearchExtender")
                                                {
                                                    ListSearchExtender myLabel = (ListSearchExtender)Convert.ChangeType(myC, typeof(ListSearchExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "MaskedEditExtender")
                                                {
                                                    MaskedEditExtender myLabel = (MaskedEditExtender)Convert.ChangeType(myC, typeof(MaskedEditExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "MaskedEditValidator")
                                                {
                                                    MaskedEditValidator myLabel = (MaskedEditValidator)Convert.ChangeType(myC, typeof(MaskedEditValidator));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ModalPopupExtender")
                                                {
                                                    ModalPopupExtender myLabel = (ModalPopupExtender)Convert.ChangeType(myC, typeof(ModalPopupExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "MutuallyExclusiveCheckBoxExtender")
                                                {
                                                    MutuallyExclusiveCheckBoxExtender myLabel = (MutuallyExclusiveCheckBoxExtender)Convert.ChangeType(myC, typeof(MutuallyExclusiveCheckBoxExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "NoBot")
                                                {
                                                    NoBot myLabel = (NoBot)Convert.ChangeType(myC, typeof(NoBot));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "NumericUpDownExtender")
                                                {
                                                    NumericUpDownExtender myLabel = (NumericUpDownExtender)Convert.ChangeType(myC, typeof(NumericUpDownExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "PagingBulletedListExtender")
                                                {
                                                    PagingBulletedListExtender myLabel = (PagingBulletedListExtender)Convert.ChangeType(myC, typeof(PagingBulletedListExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "PasswordStrength")
                                                {
                                                    PasswordStrength myLabel = (PasswordStrength)Convert.ChangeType(myC, typeof(PasswordStrength));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "PopupControlExtender")
                                                {
                                                    PopupControlExtender myLabel = (PopupControlExtender)Convert.ChangeType(myC, typeof(PopupControlExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "Rating")
                                                {
                                                    Rating myLabel = (Rating)Convert.ChangeType(myC, typeof(Rating));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ReorderList")
                                                {
                                                    ReorderList myLabel = (ReorderList)Convert.ChangeType(myC, typeof(ReorderList));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ResizableControlExtender")
                                                {
                                                    ResizableControlExtender myLabel = (ResizableControlExtender)Convert.ChangeType(myC, typeof(ResizableControlExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "RoundedCornersExtender")
                                                {
                                                    RoundedCornersExtender myLabel = (RoundedCornersExtender)Convert.ChangeType(myC, typeof(RoundedCornersExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "SliderExtender")
                                                {
                                                    SliderExtender myLabel = (SliderExtender)Convert.ChangeType(myC, typeof(SliderExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "SlideShowExtender")
                                                {
                                                    SlideShowExtender myLabel = (SlideShowExtender)Convert.ChangeType(myC, typeof(SlideShowExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "TabContainer")
                                                {
                                                    TabContainer myLabel = (TabContainer)Convert.ChangeType(myC, typeof(TabContainer));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "TextBoxWatermarkExtender")
                                                {
                                                    TextBoxWatermarkExtender myLabel = (TextBoxWatermarkExtender)Convert.ChangeType(myC, typeof(TextBoxWatermarkExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ToggleButtonExtender")
                                                {
                                                    ToggleButtonExtender myLabel = (ToggleButtonExtender)Convert.ChangeType(myC, typeof(ToggleButtonExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "UpdatePanelAnimationExtender")
                                                {
                                                    UpdatePanelAnimationExtender myLabel = (UpdatePanelAnimationExtender)Convert.ChangeType(myC, typeof(UpdatePanelAnimationExtender));
                                                    myLabel.Enabled = false;
                                                }
                                                else if (StrVal == "ValidatorCalloutExtender")
                                                {
                                                    ValidatorCalloutExtender myLabel = (ValidatorCalloutExtender)Convert.ChangeType(myC, typeof(ValidatorCalloutExtender));
                                                    myLabel.Enabled = false;
                                                }


                                            }

                                        }



                                    }
                                }


                            }

                        }




                    }
                }
            }



        }

    }


    protected void ImageButton51_Click(object sender, ImageClickEventArgs e)
    {
        object p11 = ViewState["p1"].ToString();
        if (p11 != null)
        {
            Response.Redirect((string)p11);
        }
        ModalPopupExtender1222.Hide();
        //pnlMain.Enabled = false;


    }

    protected void ImageButton511_Click(object sender, ImageClickEventArgs e)
    {
        object p11 = ViewState["p1"].ToString();
        if (p11 != null)
        {
            Response.Redirect((string)p11);
        }
        ModalPopupExtender1.Hide();
        ModalPopupExtender1222.Hide();
    }
    protected void LinkButton20_Click(object sender, EventArgs e)
    {

    }

    public void RestrictPriceplan1(string pg)
    {
        try
        {



            ViewState["p1"] = Request.UrlReferrer.ToString();
            string str11 = "Select Priceplanid from PageMaster inner join pageplaneaccesstbl on PageMaster.PageId=pageplaneaccesstbl.PageId  where PageMaster.PageName='" + ClsEncDesc.EncDyn(pg) + "' and pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' ";
            SqlCommand cmd = new SqlCommand(str11, PageConn.busclient());
            SqlDataAdapter da11 = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            da11.Fill(dt11);

            if (dt11.Rows.Count <= 0)
            {
                lblpagemsg.Text = pg.ToString();
                //ModalPopupExtender1.Show();

                // Response.Redirect("http://license.busiwiz.com/AccessRight.aspx?pid=" + Session["PriceId"] + "&pname=" + pg + "");

            }


        }
        catch (Exception)
        {


        }

    }
    private string Encrypt(string strtxt, string strtoencrypt)
    {
        byte[] bykey = new byte[20];
        byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bykey = System.Text.Encoding.UTF8.GetBytes(strtoencrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strtxt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, dv), CryptoStreamMode.Write);
            cs.Write(inputArray, 0, inputArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public string encryptstrring(string strText)
    {
        return Encrypt(strText, "&%#@?,:*");
    }


    protected void ImageButton47_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.UrlReferrer.ToString());
        //ModalPopupExtender1.Hide();
    }



    protected override void AddedControl(Control control, int index)
    {
        if (Request.ServerVariables["http_user_agent"]
        .IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
        {
            this.Page.ClientTarget = "uplevel";
        }

        base.AddedControl(control, index);
    }
    protected void grdpversion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {

            string dk = Convert.ToString(e.CommandArgument);
            string te = "VersionFolder/" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            ModalPopupExtenderdeveloper.Show();
        }
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {

    }


    public long GetFolderSize(string dPath, bool includeSubFolders)
    {
        try
        {
            long size = 0;
            DirectoryInfo diBase = new DirectoryInfo(dPath);
            FileInfo[] files = null;
            if (includeSubFolders)
            {
                files = diBase.GetFiles("*", SearchOption.AllDirectories);
            }
            else
            {
                files = diBase.GetFiles("*", SearchOption.TopDirectoryOnly);
            }
            System.Collections.IEnumerator ie = files.GetEnumerator();

            for (int i = 0; i < files.Length - 1; i++)
            {

                size += files[i].Length;
            }

            return size;


        }
        catch (Exception ex)
        {


            return -1;
        }
    }
    protected void btnsizere_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://Members.busiwiz.com");
    }
    protected void imgsize_Click(object sender, ImageClickEventArgs e)
    {
        Session["GBC"] = "1";
    }
    protected void Menu1_MenuItemClick1(object sender, MenuEventArgs e)
    {
        if (Menu1.Items.Count > 0)
        {
            if (Menu1.SelectedItem.Text == "Home")
            {
                if (Session["Pnori"] == null)
                {
                    Response.Redirect("~/ShoppingCart/Admin/frmafterloginforSuper.aspx");
                }
                else
                {
                    Response.Redirect("~/ShoppingCart/Admin/" + Session["Pnori"] + "");
                }
            }
            else if (Menu1.SelectedItem.Text == "Log Out")
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Response.AddHeader("Pragma", "no-cache");
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.Expires = -1;
                Response.Redirect("~/ShoppingCart/Admin/ShoppingCartLogin.aspx");
            }
        }

    }
}

