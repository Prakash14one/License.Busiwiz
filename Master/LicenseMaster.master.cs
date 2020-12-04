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
using System.IO;
using System.Text;
using System.Net;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Diagnostics;

public partial class customer_CustomerMaster : System.Web.UI.MasterPage
{
    string ClientId1;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    string page;
    int filterprodctid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string strData = Request.Url.LocalPath.ToString();

        //char[] separator = new char[] { '/' };

        //string[] strSplitArr = strData.Split(separator);
        //int i = Convert.ToInt32(strSplitArr.Length);
        // page = strSplitArr[i - 1].ToString();
        //Session["pagename"] = page.ToString();

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

        string cidv = " select distinct VersionInfoMaster.VersionInfoId from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId where  WebsiteMaster.compid='" + Session["ClientId"] + "'";
        SqlCommand cidcov = new SqlCommand(cidv, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        object objcov = cidcov.ExecuteScalar();
        con.Close();

        Session["verId"] = objcov.ToString();


        if (Session["ClientName"] != null)
        {
            lblclientname.Text = Session["ClientName"].ToString();
        }
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            DataTable drt = select("SELECT Distinct PageMaster.MainMenuId as MenuId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,PageMaster.PageId FROM dbo.PageMaster INNER JOIN dbo.RolePageAccessRightTbl ON dbo.RolePageAccessRightTbl.PageId = dbo.PageMaster.PageId INNER JOIN dbo.User_Role ON dbo.RolePageAccessRightTbl.RoleId = dbo.User_Role.Role_id INNER JOIN  dbo.EmployeeMaster ON dbo.User_Role.User_id = dbo.EmployeeMaster.Id INNER JOIN  dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId WHERE  PageMaster.ManuAccess=1 and EmployeeMaster.ClientId ='" + Session["ClientId"] + "' and User_Role.User_id=" + Session["EmpId"] + " and AccessRight<>'0' and PageMaster.pagename='" + Session["pagename"] + "'  ");
            if (drt.Rows.Count <= 0)
            {
                if (page.ToString()== "TimerSendMail.aspx" ||  page.ToString() == "Afterlogin.aspx" || page.ToString() == "frmafterloginforSuper.aspx" || page.ToString() == "employeeafterlogin.aspx")
                {
                }
                else if (isvesionfolder==false)
                {
                    //FormsAuthentication.SignOut();
                    //Session.Clear();
                    //Session.Abandon();
                    //Response.Redirect("http://license.busiwiz.com/Login.aspx?Noaccess=" + page + "");
                }
            }        
         else
         {
            Session["PageId"] = drt.Rows[0]["PageId"].ToString();
         }
         if (Session["report"] != null)
            {
                Session["report"] = null;
                string te = "TimerSendMail.aspx";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + te + "','_blank' ,'height=5,width=5,status=no,toolbar=no,menubar=no,location=no, scrollbars=yes' );", true); ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + te + "', 'height=5,width=5,status=no,toolbar=no,menubar=no,location=no, scrollbars=yes' );", true);
            }
            FillCategory();
            try
            {
                if (Convert.ToInt32(Request.QueryString["cat"]) > 0)
                {
                    ddlcategory.SelectedValue = Request.QueryString["cat"];
                    ddlMaster_SelectedIndexChanged(sender, e);
                   
                }
                else
                {
                    ddlcategory.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
            }
            if (isvesionfolder == false)
            {
                PopulateMenu();
            }
        }

    }
    protected DataTable select(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
    protected void ddlMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateMenu();               
    }
    protected void FillCategory()
    {
        try
        {
            string userid = "";
            if (Convert.ToString(Session["EmpId"]) != "")
            {
                userid = " and User_Role.User_id=" + Session["EmpId"];
            }

            string strlan = @" SELECT DISTINCT dbo.Mainmenucategory.MainMenucatId, dbo.Mainmenucategory.MainMenuCatName
                            FROM  dbo.RoleMenuAccessRightTbl INNER JOIN dbo.User_Role ON dbo.RoleMenuAccessRightTbl.RoleId = dbo.User_Role.Role_id INNER JOIN dbo.EmployeeMaster ON dbo.User_Role.User_id = dbo.EmployeeMaster.Id INNER JOIN dbo.MainMenuMaster ON dbo.RoleMenuAccessRightTbl.MenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.Mainmenucategory ON dbo.MainMenuMaster.MainMenucatId = dbo.Mainmenucategory.MainMenucatId
                            WHERE   dbo.Mainmenucategory.Active=1 AND  (dbo.EmployeeMaster.ClientId = '" + Session["ClientId"] + "') " + userid + " AND (dbo.RoleMenuAccessRightTbl.AccessRight <> '0') ";


            SqlCommand cmdlan = new SqlCommand(strlan, PageConn.licenseconn());
            SqlDataAdapter adplan = new SqlDataAdapter(cmdlan);
            DataSet dslan = new DataSet();
            adplan.Fill(dslan);
            ddlcategory.DataSource = dslan;
            ddlcategory.DataTextField = "MainMenuCatName";
            ddlcategory.DataValueField = "MainMenucatId";
            ddlcategory.DataBind();
            ddlcategory.Items.Insert(0, "-Select-");
            ddlcategory.Items[0].Value = "0";
            ddlcategory.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
        }

    }
    
    protected void Lnkbtn1_Click(object sender, EventArgs e)
    {
        System.Diagnostics.ProcessStartInfo pinfo = new System.Diagnostics.ProcessStartInfo("cmd.exe");
        pinfo.RedirectStandardError = true;
        pinfo.RedirectStandardInput = true;
        pinfo.UseShellExecute = false;
        pinfo.RedirectStandardOutput = true;

        pinfo.FileName = "C:\\windows\\system32\\cmd.exe";
        System.Diagnostics.Process console = System.Diagnostics.Process.Start(pinfo);

        console.WaitForExit();
     
    }
    private void PopulateMenu()
    {
        
        Menu1.Items.Clear();
        DataSet ds = GetDataSetForMenu();
        //Menu menu = new Menu();

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["parent"].Rows.Count > 0)
            {
                foreach (DataRow parentItem in ds.Tables["parent"].Rows)
                {
                    int f1 = 0;

                    int f3 = 0;
                    MenuItem categoryItem = new MenuItem((string)parentItem["MainMenuName"]);
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
                            MenuItem childrenItem = new MenuItem((string)childItem["SubMenuName"]);


                            foreach (DataRow subchildItem in childItem.GetChildRows("Children2"))
                            {

                                if (f2 == 0)
                                {
                                    categoryItem.ChildItems.Add(childrenItem);

                                    f2 += 1;
                                }
                                MenuItem childrenItem111 = new MenuItem((string)subchildItem["PageId"].ToString());
                                MenuItem childrenItem11 = new MenuItem((string)subchildItem["PageTitle"]);
                                MenuItem childrenItem112 = new MenuItem((string)subchildItem["PageName"]);

                                string stpageid = (string)subchildItem["PageId"].ToString();
                                string StrPath = "";
                                string str1211f1 = " SELECT dbo.MainMenuMaster.MainMenucatId, dbo.PageMaster.FolderName as Path FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId where PageId='" + stpageid + "'  ";
                                SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, con);
                                DataTable dt121f1 = new DataTable();
                                da121f1.Fill(dt121f1);
                                if (dt121f1.Rows.Count > 0)
                                {
                                    StrPath = dt121f1.Rows[0]["Path"].ToString();
                                    StrPath += "/";
                                }
                                // StrPath += childrenItem112.Text;
                                childrenItem11.NavigateUrl = "~/" + StrPath + "" + childrenItem112.Text + "?cat=" + dt121f1.Rows[0]["MainMenucatId"].ToString() + "";

                                // childrenItem11.NavigateUrl = "~/" + childrenItem112.Text;

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
                            MenuItem childrenItem11 = new MenuItem((string)childItem["PageTitle"]);
                            MenuItem childrenItem112 = new MenuItem((string)childItem["PageName"]);
                            string stpageid = (string)childItem["PageId"].ToString();
                            string StrPath = "";
                            string str1211f1 = " SELECT dbo.MainMenuMaster.MainMenucatId, dbo.PageMaster.FolderName as Path FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId where PageId='" + stpageid + "' ";
                            SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, con);
                            DataTable dt121f1 = new DataTable();
                            da121f1.Fill(dt121f1);
                            if (dt121f1.Rows.Count > 0)
                            {
                                StrPath = dt121f1.Rows[0]["Path"].ToString();
                                StrPath += "/";
                            }
                            // StrPath += childrenItem112.Text;
                            childrenItem11.NavigateUrl = "~/" + StrPath + "" + childrenItem112.Text + "?cat=" + dt121f1.Rows[0]["MainMenucatId"].ToString() + "";

                            // childrenItem11.NavigateUrl = "~/" + childrenItem112.Text;

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

                    MenuItem categoryItem = new MenuItem((string)parentItem["MainMenuName"]);

                    //Menu1.Items.Add(categoryItem);
                    if (categoryItem.Text != "0")
                    {

                        foreach (DataRow childItem in parentItem.GetChildRows("Children"))
                        {

                            MenuItem childrenItem = new MenuItem((string)childItem["SubMenuName"]);

                            Menu1.Items.Add(childrenItem);
                            foreach (DataRow subchildItem in childItem.GetChildRows("Children2"))
                            {





                                MenuItem childrenItem111 = new MenuItem((string)subchildItem["PageId"].ToString());
                                MenuItem childrenItem11 = new MenuItem((string)subchildItem["PageTitle"]);
                                MenuItem childrenItem112 = new MenuItem((string)subchildItem["PageName"]);
                                string stpageid = (string)subchildItem["PageId"].ToString();
                                string StrPath = "";
                                string str1211f1 = " SELECT dbo.MainMenuMaster.MainMenucatId, dbo.PageMaster.FolderName as Path FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId where PageId='" + stpageid + "'  ";
                                SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, con);
                                DataTable dt121f1 = new DataTable();
                                da121f1.Fill(dt121f1);
                                if (dt121f1.Rows.Count > 0)
                                {
                                    StrPath = dt121f1.Rows[0]["Path"].ToString();
                                    StrPath += "/";
                                }
                                // StrPath += childrenItem112.Text;
                                childrenItem11.NavigateUrl = "~/" + StrPath + "" + childrenItem112.Text + "?cat=" + dt121f1.Rows[0]["MainMenucatId"].ToString() + "";

                                childrenItem.ChildItems.Add(childrenItem11);

                            }
                            if (bb == 0)
                            {
                                foreach (DataRow childItem1 in parentItem.GetChildRows("Children3"))
                                {

                                    MenuItem childrenItem111 = new MenuItem((string)childItem1["PageId"].ToString());
                                    MenuItem childrenItem11 = new MenuItem((string)childItem1["PageTitle"]);
                                    MenuItem childrenItem112 = new MenuItem((string)childItem1["PageName"]);
                                    string stpageid = (string)childItem1["PageId"].ToString();
                                    string StrPath = "";
                                    string str1211f1 = " SELECT dbo.MainMenuMaster.MainMenucatId, dbo.PageMaster.FolderName as Path FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId where PageId='" + stpageid + "'  ";
                                    SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, con);
                                    DataTable dt121f1 = new DataTable();
                                    da121f1.Fill(dt121f1);
                                    if (dt121f1.Rows.Count > 0)
                                    {
                                        StrPath = dt121f1.Rows[0]["Path"].ToString();
                                        StrPath += "/";
                                    }
                                    // StrPath += childrenItem112.Text;
                                    childrenItem11.NavigateUrl = "~/" + StrPath + "" + childrenItem112.Text + "?cat=" + dt121f1.Rows[0]["MainMenucatId"].ToString() + ""; ;

                                    //  childrenItem11.NavigateUrl = "~/" + childrenItem112.Text;

                                    childrenItem.ChildItems.Add(childrenItem11);

                                }
                                bb += 1;
                            }

                        }


                    }

                }
            }

        }
    }
    private DataSet GetDataSetForMenu()
    {
        string main1 = "";
        string main2 = "";
        string main3 = "";
        string manuid = "";
        string rsubmanu = "";
        string rpagemanu = "";
        string submanuid = "";

        int mcount = 0;
        int scount = 0;
        string userid = "";
        if (Convert.ToString(Session["EmpId"]) != "")
        {
          //  userid = " and User_Role.User_id=" + Session["EmpId"];
        }
        string category = "";
        if (ddlcategory.SelectedIndex > 0)
        {
            category += " AND  dbo.MainMenuMaster.MainMenucatId='" + ddlcategory.SelectedValue + "'";//
        }
        string str121f = "SELECT Distinct RoleMenuAccessRightTbl.MenuId FROM RoleMenuAccessRightTbl INNER JOIN User_Role ON RoleMenuAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN EmployeeMaster ON User_Role.User_id = EmployeeMaster.Id WHERE  EmployeeMaster.ClientId ='" + Session["ClientId"] + "'" + userid + " and RoleMenuAccessRightTbl.AccessRight<>'0' ";
        str121f = " SELECT Distinct RoleMenuAccessRightTbl.MenuId FROM dbo.RoleMenuAccessRightTbl INNER JOIN dbo.User_Role ON dbo.RoleMenuAccessRightTbl.RoleId = dbo.User_Role.Role_id INNER JOIN dbo.EmployeeMaster ON dbo.User_Role.User_id = dbo.EmployeeMaster.Id INNER JOIN  dbo.MainMenuMaster ON dbo.RoleMenuAccessRightTbl.MenuId = dbo.MainMenuMaster.MainMenuId WHERE  EmployeeMaster.ClientId ='" + Session["ClientId"] + "'" + userid + "" + category + " and RoleMenuAccessRightTbl.AccessRight<>'0' ";                

        SqlDataAdapter da121f = new SqlDataAdapter(str121f, con);
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

        string str1211f1 = "SELECT Distinct PageMaster.MainMenuId as MenuId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,PageMaster.PageId FROM PageMaster inner join RolePageAccessRightTbl on RolePageAccessRightTbl.PageId=PageMaster.PageId INNER JOIN User_Role ON RolePageAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN EmployeeMaster ON User_Role.User_id = EmployeeMaster.Id WHERE  PageMaster.ManuAccess=1 and EmployeeMaster.ClientId ='" + Session["ClientId"] + "'" + userid + " " + category + " and AccessRight<>'0' ";
        str1211f1 = " SELECT Distinct PageMaster.MainMenuId as MenuId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,PageMaster.PageId FROM dbo.PageMaster INNER JOIN dbo.RolePageAccessRightTbl ON dbo.RolePageAccessRightTbl.PageId = dbo.PageMaster.PageId INNER JOIN dbo.User_Role ON dbo.RolePageAccessRightTbl.RoleId = dbo.User_Role.Role_id INNER JOIN  dbo.EmployeeMaster ON dbo.User_Role.User_id = dbo.EmployeeMaster.Id INNER JOIN  dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId WHERE  PageMaster.ManuAccess=1 and EmployeeMaster.ClientId ='" + Session["ClientId"] + "'" + userid + " " + category + " and AccessRight<>'0' ";
        

        SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, con);
        DataTable dt121f1 = new DataTable();
        da121f1.Fill(dt121f1);
        foreach (DataRow dts in dt121f1.Rows)
        {
            if (mcount != Convert.ToInt32(dts["MenuId"]))
            {
                mcount = Convert.ToInt32(dts["MenuId"]);
                main2 += "'" + dts["MenuId"] + "',";
                manuid += "'" + dts["MenuId"] + "',";

            }
            if (scount != Convert.ToInt32(dts["SubMenuId"]))
            {
                scount = Convert.ToInt32(dts["SubMenuId"]);

                rsubmanu += "'" + dts["SubMenuId"] + "',";
            }
            rpagemanu += "'" + dts["PageId"] + "',";

        }
        if (main2.Length > 0)
        {

            main2 = main2.Remove(main2.Length - 1);

        }


        string str1211f11 = " SELECT Distinct SubMenuMaster.MainMenuId as MenuId,SubMenuMaster.SubMenuId FROM SubMenuMaster inner join RoleSubMenuAccessRightTbl on RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId INNER JOIN User_Role ON RoleSubMenuAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN  EmployeeMaster ON User_Role.User_id = EmployeeMaster.Id WHERE   EmployeeMaster.ClientId ='" + Session["ClientId"] + "'" + userid + "  and AccessRight<>'0'" + userid; //Add as MenuId at 11-28-2014
        str1211f11 = " SELECT Distinct SubMenuMaster.MainMenuId as MenuId,SubMenuMaster.SubMenuId FROM dbo.SubMenuMaster INNER JOIN dbo.RoleSubMenuAccessRightTbl ON dbo.RoleSubMenuAccessRightTbl.SubMenuId = dbo.SubMenuMaster.SubMenuId INNER JOIN dbo.User_Role ON dbo.RoleSubMenuAccessRightTbl.RoleId = dbo.User_Role.Role_id INNER JOIN dbo.EmployeeMaster ON dbo.User_Role.User_id = dbo.EmployeeMaster.Id INNER JOIN dbo.MainMenuMaster ON dbo.SubMenuMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId WHERE  ( EmployeeMaster.ClientId ='" + Session["ClientId"] + "'" + userid + "  and AccessRight<>'0'" + userid + " " + category + " )   "; 
         

        SqlDataAdapter da121f11 = new SqlDataAdapter(str1211f11, con);
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
        {
            string str = "Select Distinct MainMenuMaster.* from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId where MainMenuMaster.MainMenuId in(" + manuid + ") and WebsiteMaster.compid='" + Session["ClientId"] + "' Order by MainMenuIndex";

            // string str = "SELECT distinct * FROM MainMenuMaster where MainMenuId in(" + manuid + ") Order by MainMenuIndex";

            SqlDataAdapter adpcat = new SqlDataAdapter(str, con);

            adpcat.Fill(ds, "parent");


            if (main1.Length == 0)
            {
                if (manuid.Length > 0)
                {
                    main1 = manuid;
                }
            }
            else
            {
                string ax = "Select Distinct MainMenuMaster.*,PageId from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId where PageMaster.ManuAccess=1 and MainMenuMaster.MainMenuId in(" + manuid + ") and WebsiteMaster.compid='" + Session["ClientId"] + "' Order by MainMenuIndex";
                ax = "Select Distinct MainMenuMaster.*,PageMaster.PageId from    dbo.WebsiteMaster INNER JOIN dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteMasterId = dbo.WebsiteMaster.ID INNER JOIN dbo.MasterPageMaster ON dbo.WebsiteSection.WebsiteSectionId = dbo.MasterPageMaster.WebsiteSectionId INNER JOIN dbo.MainMenuMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.RolePageAccessRightTbl ON dbo.WebsiteMaster.ID = dbo.RolePageAccessRightTbl.Id INNER JOIN dbo.RolePageAccessRightTbl AS RolePageAccessRightTbl_1 ON dbo.PageMaster.PageId = RolePageAccessRightTbl_1.PageId INNER JOIN dbo.User_Role ON RolePageAccessRightTbl_1.RoleId = dbo.User_Role.Role_id where PageMaster.ManuAccess=1 and MainMenuMaster.MainMenuId in(" + manuid + ") and WebsiteMaster.compid='" + Session["ClientId"] + "' " + userid + " Order by MainMenuIndex";
                 
                DataTable drtc = new DataTable();
                SqlDataAdapter asc = new SqlDataAdapter(ax, con);
                asc.Fill(drtc);
                foreach (DataRow dts in drtc.Rows)
                {

                    rpagemanu += "'" + dts["PageId"] + "',";
                }

                //check page accesss or not 
                string axAccess = "Select Distinct MainMenuMaster.*,PageId from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId where MainMenuMaster.MainMenuId in(" + manuid + ") and WebsiteMaster.compid='" + Session["ClientId"] + "' and PageMaster.PageName='" + page.ToString() + "' and PageMaster.ManuAccess=1  Order by MainMenuIndex";
                DataTable drtcAccess = new DataTable();
                SqlDataAdapter ascAccess = new SqlDataAdapter(axAccess, con);
                ascAccess.Fill(drtcAccess);
                //foreach (DataRow dts in drtc.Rows)
                //{

                //    rpagemanu += "'" + dts["PageId"] + "',";
                //}
            }
            string str11 = "";
            string cdrt = "";
            if (rpagemanu.Length != 0)
            {
                cdrt = " or PageMaster.PageId in(" + rpagemanu + ")";
            }
            if (rpagemanu.Length > 0)
            {

                rpagemanu = rpagemanu.Remove(rpagemanu.Length - 1);

            }
            if (rsubmanu.Length != 0)
            {
                if (main1.Length != 0)
                {
                    str11 = " Select Distinct SubMenuMaster.* from PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId  inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  MainMenuMaster.MainMenuId In( " + main1 + ")  Order By  SubMenuMaster.SubMenuIndex  ";//
                    str11 = " SELECT DISTINCT  dbo.SubMenuMaster.SubMenuId, dbo.SubMenuMaster.MainMenuId, dbo.SubMenuMaster.SubMenuName, dbo.SubMenuMaster.SubMenuIndex, dbo.SubMenuMaster.Active, dbo.SubMenuMaster.LanguageId, dbo.RoleSubMenuAccessRightTbl.AccessRight FROM dbo.PageMaster INNER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId INNER JOIN dbo.MainMenuMaster ON dbo.MainMenuMaster.MainMenuId = dbo.SubMenuMaster.MainMenuId INNER JOIN dbo.RoleSubMenuAccessRightTbl ON dbo.SubMenuMaster.SubMenuId = dbo.RoleSubMenuAccessRightTbl.SubMenuId INNER JOIN dbo.User_Role ON dbo.RoleSubMenuAccessRightTbl.RoleId = dbo.User_Role.Role_id WHERE  ( MainMenuMaster.MainMenuId In( " + main1 + ")  AND (dbo.RoleSubMenuAccessRightTbl.AccessRight <> '0') " + userid + ")  ORDER BY dbo.SubMenuMaster.SubMenuIndex ";
                }
                else
                {
                    str11 = " Select Distinct SubMenuMaster.* from PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where   SubMenuMaster.SubMenuId in(" + rsubmanu + ") Order By  SubMenuMaster.SubMenuIndex  ";// 
                }
            }
            else
            {
                if (main1.Length != 0)
                {
                    str11 = " Select Distinct SubMenuMaster.* from PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId  inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  MainMenuMaster.MainMenuId In( " + main1 + ")" + cdrt;
                }
                else if (manuid.Length != 0)
                {
                    str11 = " Select Distinct SubMenuMaster.* from PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId  inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where  MainMenuMaster.MainMenuId In( " + manuid + ")" + cdrt;
                }
            }


            SqlDataAdapter adpProduct = new SqlDataAdapter(str11, con);
            adpProduct.Fill(ds, "child");
            foreach (DataRow dts in ds.Tables["child"].Rows)
            {
                submanuid += "'" + dts["SubMenuId"] + "',";

            }
            if (submanuid.Length != 0)
            {
                submanuid = submanuid.Remove(submanuid.Length - 1);

            }

            string str15 = "";
            if (manuid.Length != 0 && rpagemanu.Length != 0)
            {
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster   where  (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + manuid + ")) and PageMaster.PageId in(" + rpagemanu + ") order by PageIndex ASC";
            }
            else if (main1.Length != 0)
            {

                str15 = " SELECT  distinct PageIndex, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster  where (SubMenuId='0' or SubMenuId IS NULL)  and (MainMenuId In( " + main1 + "))order by PageIndex ASC";

            }

            else if (manuid.Length != 0)
            {
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster where (SubMenuId='0' or SubMenuId IS NULL) and (MainMenuId In( " + manuid + ")) order by PageIndex ASC";
            }            
            SqlDataAdapter adp115 = new SqlDataAdapter(str15, con);
            DataSet ds125 = new DataSet();
            adp115.Fill(ds, "leafchild1");
            string str1 = "";

            if (rpagemanu.Length > 0)
            {

                if (submanuid.Length > 0)
                {
                    str1 = " SELECT  distinct PageIndex, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster  where (SubMenuId in(" + submanuid + ")) and(PageMaster.PageId in(" + rpagemanu + "))order by PageIndex ASC ";

                }
                else
                {
                    str1 = " SELECT  distinct PageIndex, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster  where (PageMaster.PageId in(" + rpagemanu + "))order by PageIndex ASC ";

                }
                SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                DataSet ds12 = new DataSet();
                adp11.Fill(ds, "leafchild");
            }
            else
            {
                if (submanuid.Length > 0)
                {
                    str1 = " SELECT   distinct PageIndex,PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster   where  (SubMenuId in(" + submanuid + ")) order by PageIndex ASC";

                    str1 = "SELECT DISTINCT dbo.PageMaster.PageIndex, dbo.PageMaster.MainMenuId, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageId, CASE WHEN (PageMaster.SubMenuId IS NULL) THEN '0' ELSE PageMaster.SubMenuId END AS SubMenuId, dbo.RolePageAccessRightTbl.AccessRight,   dbo.User_Role.ActiveDeactive, dbo.User_Role.User_id FROM dbo.PageMaster INNER JOIN dbo.RolePageAccessRightTbl ON dbo.PageMaster.PageId = dbo.RolePageAccessRightTbl.PageId INNER JOIN  dbo.User_Role ON dbo.RolePageAccessRightTbl.RoleId = dbo.User_Role.Role_id WHERE (SubMenuId in(" + submanuid + "))  AND  dbo.RolePageAccessRightTbl.AccessRight <>'0' " + userid + " ORDER BY dbo.PageMaster.PageIndex ";
                    SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                    DataSet ds12 = new DataSet();
                    adp11.Fill(ds, "leafchild");
                }
                else
                {
                    str1 = " SELECT  distinct PageIndex, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster   where  (SubMenuId in(" + 00 + ")) and (PageMaster.PageId =0)order by PageIndex ASC";
                    SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                    DataSet ds12 = new DataSet();
                    adp11.Fill(ds, "leafchild");
                    ds.Tables["leafchild"].Rows.Add(0, 0, 0, 0, 0);
                }
            }

            ds.Tables["parent"].Rows.Add(0, 0, 0, 0, 0);
            ds.Tables["child"].Rows.Add(0, 0, 0, 0, 0);
            try
            {
                ds.Relations.Add("Children", ds.Tables["parent"].Columns["MainMenuId"], ds.Tables["child"].Columns["MainMenuId"]);
                ds.Relations.Add("Children2", ds.Tables["child"].Columns["SubMenuId"], ds.Tables["leafchild"].Columns["SubMenuId"]);
                ds.Relations.Add("Children3", ds.Tables["parent"].Columns["MainMenuId"], ds.Tables["leafchild1"].Columns["MainMenuId"]);
            }
            catch
            {
            }
            
        }
        return ds;
    }



    //-----------Filter pages----------------------------------------------------------------------------------
    protected void BtnGo_Click(object sender, EventArgs e)
    {
        try
        {
            Session["pagesearch"] = null;
            if (TextBox5.Text != "")
            {
                Session["pagesearch"] = TextBox5.Text;
                Response.Redirect("~/clientadmin/pagesearch.aspx?page=" + Request.Url.Host.ToString() + "");
                //TextBox5.Text = TextBox1.Text;
                //ModalPopupExtender3.Show(); 
                //filterproduct();
                //FillterCategorysearch();
                //filtermainmenu();
                //filtersubmenu();          
                //FilterGrid();
            }
            else
            {
                Session["pagesearch"] = null;
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void filterproduct()
    {



        string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,  ProductMaster.ProductName + ':' +   VersionInfoMaster.VersionInfoName + ' : ' + WebsiteMaster.WebsiteName + ':' +   WebsiteSection.SectionName + ':' +   MasterPageMaster.MasterPageName  as MasterPage_Name  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by MasterPage_Name";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);


        FilterProduct.DataSource = dtcln;
        FilterProduct.DataValueField = "MasterPageId";
        FilterProduct.DataTextField = "MasterPage_Name";
        FilterProduct.DataBind();

        FilterProduct.Items.Insert(0, "All");
        FilterProduct.Items[0].Value = "0";


        if (dtcln.Rows.Count > 0)
        {
            ViewState["versioninfo"] = dtcln.Rows[0]["VersionInfoId"].ToString();
        }
        string strcln1 = " SELECT * From TheLastSearchedPageMasterTBL where UserID='" + Session["EmpId"] + "' and Pagename='PageMasterNew' Order By ID Desc";
        SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
        DataTable dtcln1 = new DataTable();
        SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
        adpcln1.Fill(dtcln1);
        if (dtcln1.Rows.Count > 0)
        {
            FilterProduct.SelectedValue = dtcln1.Rows[0]["ProductID"].ToString();
            filterprodctid = 1;
        }


    }
    protected void FillterCategorysearch()
    {

        string strlan = "select * from Mainmenucategory where MasterPage_Id='" + FilterProduct.SelectedValue + "'";
        SqlCommand cmdlan = new SqlCommand(strlan, con);
        SqlDataAdapter adplan = new SqlDataAdapter(cmdlan);
        DataSet dslan = new DataSet();
        adplan.Fill(dslan);
        DDLCategoryS.DataSource = dslan;
        DDLCategoryS.DataTextField = "MainMenuCatName";
        DDLCategoryS.DataValueField = "MainMenucatId";
        DDLCategoryS.DataBind();
        DDLCategoryS.Items.Insert(0, "-Select-");
        DDLCategoryS.Items[0].Value = "0";
    }

    
    protected void FilterCategorysearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        filtermainmenu();
        filtersubmenu();
    }
   
    protected void filtermainmenu()
    {
        string filter = "";
        if (DDLCategoryS.SelectedIndex > 0)
        {
            filter = " and MainMenuMaster.MainMenucatId='" + DDLCategoryS.SelectedValue + "' ";
        }
        string strcln = " SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "' " + filter + " order By MainMenuMaster.MainMenuTitle ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        FilterMenu.DataSource = dtcln;

        FilterMenu.DataValueField = "MainMenuId";
        FilterMenu.DataTextField = "Page_Name";
        FilterMenu.DataBind();
        FilterMenu.Items.Insert(0, "All");
        FilterMenu.Items[0].Value = "0";

    }

    protected void filtersubmenu()
    {
        string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "' and SubMenuMaster.MainMenuId='" + FilterMenu.SelectedValue + "'  Order By SubMenuMaster.SubMenuName ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        FilterSubMenu.DataSource = dtcln;

        FilterSubMenu.DataValueField = "SubMenuId";
        FilterSubMenu.DataTextField = "SubMenuName";
        FilterSubMenu.DataBind();

        FilterSubMenu.Items.Insert(0, "All");
        FilterSubMenu.Items[0].Value = "0";       

    }
     protected void FilterMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        filtersubmenu();
        //  FillGrid();
    }

     public void functionality()
     {
         string functionality = "select ID,FunctionalityTitle from FunctionalityMasterTbl where VersionID='" + ViewState["versioninfo"] + "' Order By FunctionalityTitle ";
         SqlCommand cmdcln = new SqlCommand(functionality, con);
         DataTable dtcln = new DataTable();
         SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
         adpcln.Fill(dtcln);
         ddlfuncti.DataSource = dtcln;

         ddlfuncti.DataValueField = "ID";
         ddlfuncti.DataTextField = "FunctionalityTitle";
         ddlfuncti.DataBind();
         ddlfuncti.Items.Insert(0, "-Select-");
         ddlfuncti.Items[0].Value = "0";
     }
     protected void FilterProduct_SelectedIndexChanged(object sender, EventArgs e)
     {
         string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,  ProductMaster.ProductName + ':' +   VersionInfoMaster.VersionInfoName + ' : ' + WebsiteMaster.WebsiteName + ':' +   WebsiteSection.SectionName + ':' +   MasterPageMaster.MasterPageName  as MasterPage_Name  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "' and VersionInfoMaster.Active ='True' order  by VersionInfoMaster.VersionInfoId Asc";
         //string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName + ':' + 'MASTER PAGE' + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' order  by VersionInfoMaster.VersionInfoId Asc";
         SqlCommand cmdcln = new SqlCommand(strcln, con);
         DataTable dtcln = new DataTable();
         SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
         adpcln.Fill(dtcln);
         if (dtcln.Rows.Count > 0)
         {
             ViewState["versioninfo"] = dtcln.Rows[0]["VersionInfoId"].ToString();
         }

         FillterCategorysearch();
         functionality();
         filtermainmenu();
         filtersubmenu();
     }
     protected void ddlfuncti_SelectedIndexChanged(object sender, EventArgs e)
     {
         FilterGrid();
     }
    
     public void FilterSubMenu1()
     {
         FillGrid();
     }
     public string sortOrder
     {
         get
         {
             if (ViewState["sortOrder"].ToString() == "desc")
             {
                 ViewState["sortOrder"] = "asc";
             }
             else
             {
                 ViewState["sortOrder"] = "desc";
             }

             return ViewState["sortOrder"].ToString();
         }
         set
         {
             ViewState["sortOrder"] = value;
         }
     }
    protected void FilterGrid()
    {
        Session["SelectedIndexS"] = FilterProduct.SelectedIndex; 
        string str1 = "";
        string str2 = "";
        string str3 = "";
        string str4 = "";
        string strcln = "";

        //strcln = "select distinct SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId, PageMaster.PageId,PageMaster.FolderName,LanguageMaster.Name,LanguageMaster.Id,MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,PageMaster.Active, ProductMaster.ProductName,VersionInfoMaster.VersionInfoName, PageMaster.PageName,PageMaster.PageTitle,  WebsiteSection.SectionName + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name   from  ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId  inner join PageMaster on PageMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId  left outer join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId left outer join SubMenuMaster  on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join ProductDetail  on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster  on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection  on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster  on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId  left outer join LanguageMaster on LanguageMaster.Id = PageMaster.LanguageId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and PageMaster.VersionInfoMasterId='" + ViewState["versioninfo"] + "' ";


        strcln = " select distinct SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId, PageMaster.PageId,PageMaster.FolderName,MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,PageMaster.Active, ProductMaster.ProductName,VersionInfoMaster.VersionInfoName, PageMaster.PageName,PageMaster.PageTitle,  WebsiteSection.SectionName + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name , dbo.Mainmenucategory.MainMenuCatName from   dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteMasterId = dbo.WebsiteMaster.ID INNER JOIN dbo.MasterPageMaster ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN dbo.MainMenuMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.Mainmenucategory ON dbo.MainMenuMaster.MainMenucatId = dbo.Mainmenucategory.MainMenucatId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.PageMaster.SubMenuId = dbo.SubMenuMaster.SubMenuId  where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' ";

        if (FilterProduct.SelectedIndex > 0)
        {
            str1 = "and MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "' ";
        }
       
        if (FilterMenu.SelectedIndex > 0)
        {
            str2 = " and MainMenuMaster.MainMenuId='" + FilterMenu.SelectedValue + "'";
        }
        if (FilterSubMenu.SelectedIndex > 0)
        {
            str3 = "and SubMenuMaster.SubMenuId='" + FilterSubMenu.SelectedValue + "'";
        }
        if (ddlAct.SelectedValue == "0")
        {
            str4 = "and PageMaster.Active='0";
        }
        if (ddlAct.SelectedValue == "1")
        {
            str4 = "and PageMaster.Active='1'";
        }
        if (ddlfuncti.SelectedIndex > 0)
        {
            str4 += " and PageMaster.PageId in (Select PagemasterID From FunctionalityPageOrderTbl where FunctionalityMasterTblID ='" + ddlfuncti.SelectedValue + "') ";
        }
        if (TextBox5.Text !="")
        {
            str4 += "  and  (PageMaster.PageName Like '%" + TextBox1.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox1.Text + "%' OR dbo.Mainmenucategory.MainMenuCatName Like '%" + TextBox1.Text + "%' OR MainMenuMaster.MainMenuName Like '%" + TextBox1.Text + "%' OR SubMenuMaster.SubMenuName Like '%" + TextBox1.Text + "%' )"; 
        }
        string orderby = "order by  PageMaster.PageTitle";

      
        strcln = strcln + str1 + str2 + str3 + str4 + orderby;
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView1.DataSource = dtcln;
        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());
            ViewState["pageid"] = i.ToString();

            string strcln = "select pagemaster.*,MainMenuMaster.MasterPage_Id  from pagemaster inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId where pageid='" + i + "'";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
           
            }
        }
        if (e.CommandName == "Delete")
        {
          
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FilterGrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


    }
    protected void FillGrid()
    {

        string strcln = "";

        strcln = "select distinct SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId, PageMaster.PageId,PageMaster.FolderName,LanguageMaster.Id,LanguageMaster.Name,MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,PageMaster.Active, ProductMaster.ProductName,VersionInfoMaster.VersionInfoName, PageMaster.PageName,PageMaster.PageTitle,  WebsiteSection.SectionName + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name   from  ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId  inner join PageMaster on PageMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId  left outer join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId left outer join LanguageMaster on LanguageMaster.Id = SubMenuMaster.LanguageId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and PageMaster.VersionInfoMasterId='" + FilterProduct.SelectedValue + "' and ProductDetail.Active='1' ";
       
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView1.DataSource = dtcln;

        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataBind();
       

    }
}
