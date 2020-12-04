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
using AjaxControlToolkit;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Common;
using System.Xml;
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using System.Security.Cryptography;

public partial class PageAccessUser : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);  
    DataSet dt;
    SqlConnection conn;
    public SqlConnection connweb;
    string encripkey = "";
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            FillProduct();
            if (Convert.ToString(Session["succc"]) == "tn")
            {
                  Session["succc"] = null;
                  Label1.Text = "Page Access Given Successfully";
            }            
        }

       // con = PageConn.serverconn();
    }
    protected void FillProduct()
    {        
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM dbo.ClientProductTableMaster INNER JOIN dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.VersionNo = dbo.VersionInfoMaster.VersionInfoName ON dbo.ClientProductTableMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ClientProductTableMaster.Databaseid = dbo.ProductCodeDetailTbl.Id where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active =1 order  by productversion ");       
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "productversion";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");
        ddlProductname.Items[0].Value = "0";
    }
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {          
            ddlpriceplan.DataSource = null;
            Label1.Text = "";          
            FillWebsiteMaster();
            fillportal();            
        }
        catch (Exception ex)
        {
            Label1.Text = ex.ToString();
        }

    }
    protected void FillWebsiteMaster()
    {
        string strcln = "  ";//
        if (chk_activelistonly.Checked == true)
        {
            strcln = " and WebsiteMaster.Status=1 ";
        }
        DDLWebsiteC.Items.Clear();
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.WebsiteMaster.ID, dbo.WebsiteMaster.WebsiteName, dbo.WebsiteMaster.WebsiteUrl FROM dbo.WebsiteMaster INNER JOIN dbo.VersionInfoMaster ON dbo.WebsiteMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId Where VersionInfoMaster.VersionInfoId='" + ddlProductname.SelectedValue + "' " + strcln + " ");
        DDLWebsiteC.DataSource = dtcln;
        DDLWebsiteC.DataValueField = "ID";
        DDLWebsiteC.DataTextField = "WebsiteName";
        DDLWebsiteC.DataBind();
        DDLWebsiteC.Items.Insert(0, "--Select--");
        DDLWebsiteC.Items[0].Value = "0";
        DDLWebsiteC.SelectedIndex = 0;
    }
    protected void DDLWebsiteC_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMasterPageSearch();
        FillCategorysearch();
        fillmenu();
        fillsubmenu();      
    }
    protected void fillportal()
    {
        ddlportal.Items.Clear();
        if (ddlProductname.SelectedIndex > 0)
        {
            string strcln1 = " ";
            DataTable dtcln = MyCommonfile.selectBZ(" Select * from PortalMasterTbl  inner join ProductMaster on ProductMaster.ProductId= PortalMasterTbl.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId where VersionInfoMaster.VersionInfoId='" + ddlProductname.SelectedValue + "' order by PortalName ");
            ddlportal.DataSource = dtcln;
            ddlportal.DataValueField = "Id";
            ddlportal.DataTextField = "PortalName";
            ddlportal.DataBind();
        }
        ddlportal.Items.Insert(0, "-Select-");
    }
    protected void ddlportal_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        fillplancategory();
    }
    protected void fillplancategory()
    {
        ddlpriceplancatagory.Items.Clear();
        if (ddlportal.SelectedIndex > 0)
        {
            string strcln = " ";            
            DataTable dtcln = MyCommonfile.selectBZ(" SELECT distinct * FROM Priceplancategory inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId where PortalId='" + ddlportal.SelectedValue + "' order  by CategoryName ");       
            ddlpriceplancatagory.DataSource = dtcln;
            ddlpriceplancatagory.DataTextField = "CategoryName";
            ddlpriceplancatagory.DataValueField = "Id";
            ddlpriceplancatagory.DataBind();
        }
        ddlpriceplancatagory.Items.Insert(0, "-Select-");
        ddlpriceplancatagory.Items[0].Value = "0";
    }
    protected void ddlpriceplancatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";       
        FillGrid();       
        panelem.Visible = true;
    }
    protected void FillRoleDDL()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.DefaultRole.RoleId, dbo.DefaultDept.DeptName + ':' + dbo.DefaultDesignationTbl.DesignationName AS RoleName FROM dbo.DefaultRole INNER JOIN dbo.DefaultDesignationTbl ON dbo.DefaultDesignationTbl.RoleId = dbo.DefaultRole.RoleId INNER JOIN dbo.DefaultDept ON dbo.DefaultDept.DeptId = dbo.DefaultDesignationTbl.DeptId where  DefaultDept.VersionId='" + ddlProductname.SelectedValue + "' order by RoleName ");
        ddlrolemode.DataSource = dtcln;
        ddlrolemode.DataValueField = "RoleId";
        ddlrolemode.DataTextField = "RoleName";
        ddlrolemode.DataBind();
        ddlrolemode.Items.Insert(0, "Select");
        ddlrolemode.Items[0].Value = "0";
    }
    protected void ddlrolemode_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    protected void fillMasterPageSearch()
    {
        DDLmasterpageL.Items.Clear();
        if (DDLWebsiteC.SelectedIndex > 0)
        {
            string strcln1 = " ";
            DataTable dtcln = MyCommonfile.selectBZ(" SELECT dbo.MasterPageMaster.MasterPageId, dbo.MasterPageMaster.MasterPageName, dbo.MasterPageMaster.MasterPageDescription, dbo.MasterPageMaster.WebsiteSectionId FROM dbo.MasterPageMaster INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId Where WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "' ");
            DDLmasterpageL.DataSource = dtcln;
            DDLmasterpageL.DataValueField = "MasterPageId";
            DDLmasterpageL.DataTextField = "MasterPageName";
            DDLmasterpageL.DataBind();
        }
        DDLmasterpageL.Items.Insert(0, "-Select-");
    }
    protected void FilterMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCategorysearch();
        fillmenu();
        fillsubmenu();
        FillGrid();
    }
    protected void FillCategorysearch()
    {
        string strlan = "select * from Mainmenucategory where MasterPage_Id='" + DDLmasterpageL.SelectedValue + "'";
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
    protected void FilterCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillmenu();
        fillsubmenu();
        FillGrid();
    }
    protected void fillmenu()
    {
        string strcln = "";
        DropDownList1.Items.Clear();
        if (ddlProductname.SelectedIndex > 0)
        {
            if (chk_activelistonly.Checked == true)
            {
                 strcln = " and MainMenuMaster.Active=1 ";
            }
            if (DDLmasterpageL.SelectedIndex > 0)
            {
                strcln += " and MainMenuMaster.MasterPage_Id='"+DDLmasterpageL.SelectedValue+"'";
            }
            if (DDLCategoryS.SelectedIndex > 0)
            {
                strcln += " and MainMenuMaster.MainMenucatId='" + DDLCategoryS.SelectedValue + "'";
            }
            //DataTable dtcln = MyCommonfile.selectBZ(" SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId where VersionInfoMaster.VersionInfoId='" + ddlProductname.SelectedValue + "' "+strcln+" Order by MainMenuMaster.MainMenuName ");
            DataTable dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.MainMenuMaster.MainMenuId, dbo.MainMenuMaster.MainMenuName, dbo.WebsiteSection.WebsiteMasterId FROM dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId Where WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "' Order by dbo.MainMenuMaster.MainMenuName ");
            DropDownList1.DataSource = dtcln;
            DropDownList1.DataValueField = "MainMenuId";
            DropDownList1.DataTextField = "MainMenuName";
            DropDownList1.DataBind();
        }
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
      
        fillsubmenu();
        FillGrid();
    }
    protected void fillsubmenu()
    {
        string strcln = " ";
        DropDownList2.Items.Clear();
        if (DropDownList1.SelectedIndex > 0)
        {          
            if (chk_activelistonly.Checked == true)
            {
                strcln = " and SubMenuMaster.Active=1 ";
            }                   
            DataTable dtcln = MyCommonfile.selectBZ(" SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId where SubMenuMaster.MainMenuId='" + DropDownList1.SelectedValue + "' " + strcln + " Order By SubMenuMaster.SubMenuName ");       
            DropDownList2.DataSource = dtcln;
            DropDownList2.DataValueField = "SubMenuId";
            DropDownList2.DataTextField = "SubMenuName";
            DropDownList2.DataBind();
        }
        DropDownList2.Items.Insert(0, "-Select-");
        DropDownList2.Items[0].Value = "0";
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        FillGrid();
    }
    protected void FillGrid()
    {
      
    }
  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
    }
    protected void DefaultUser()
    {
    }
    protected void ddlpriceplan_SelectedIndexChanged(object sender, EventArgs e)
    {      
    }


    //------------------------------------------------------------------------------------------------------------------------------
    public string encryptstrringser(string strText)
    {
        return Encrypt(strText, encripkey);
    }
    public string decryptstringserv(string str)
    {
        return Decryptserv(str, encripkey);
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
            // throw ex;
            return "";
        }
    }
    private string Decryptserv(string strText, string strEncrypt)
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
            return "";
        }

    }
    protected void ddlMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateMenu();
    }
    private void PopulateMenu()
    {
        DataSet ds = GetDataSetForMenu();
        MenuItem catMasterhome = new MenuItem((string)"Home");
        Menu1.Items.Add(catMasterhome);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["parent"].Rows.Count > 0)
            {
                foreach (DataRow parentItem in ds.Tables["parent"].Rows)
                {
                    int f1 = 0;
                    int f3 = 0;
                    MenuItem categoryItem = new MenuItem(decryptstringserv((string)parentItem["MainMenuName"]));
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
                            MenuItem childrenItem = new MenuItem(decryptstringserv((string)childItem["SubMenuName"]));

                            String CHECK = childrenItem.Text;
                            foreach (DataRow subchildItem in childItem.GetChildRows("Children2"))
                            {

                                if (f2 == 0)
                                {
                                    categoryItem.ChildItems.Add(childrenItem);

                                    f2 += 1;
                                }
                                MenuItem childrenItem111 = new MenuItem(decryptstringserv((string)subchildItem["PageId"].ToString()));
                                MenuItem childrenItem11 = new MenuItem(decryptstringserv((string)subchildItem["PageTitle"]));
                                MenuItem childrenItem112 = new MenuItem(decryptstringserv((string)subchildItem["PageName"]));
                                string stpageid = (string)subchildItem["PageId"].ToString();
                                string catid = "";
                                string str1211f1cat = " SELECT dbo.MainMenuMaster.MainMenucatId, dbo.PageMaster.FolderName as Path FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId where PageId='" + stpageid + "'  ";
                                SqlDataAdapter da121f1cat = new SqlDataAdapter(str1211f1cat, PageConn.serverconn());
                                DataTable dt121f1cat = new DataTable();
                                da121f1cat.Fill(dt121f1cat);

                                if (dt121f1cat.Rows.Count > 0)
                                {
                                    catid = decryptstringserv(dt121f1cat.Rows[0]["MainMenucatId"].ToString());
                                }

                                childrenItem11.NavigateUrl = "~/Shoppingcart/admin/" + childrenItem112.Text + "?cat=" + catid + "";

                                CHECK = childrenItem111.Text;
                                CHECK = childrenItem11.Text;
                                CHECK = childrenItem112.Text;


                                childrenItem.ChildItems.Add(childrenItem11);
                                //********************************************************
                                //**********************************
                                //*********************
                                string str1211f1 = "SELECT Distinct PageMaster.MainMenuId as MenuId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,PageMaster.PageId FROM PageMaster inner join " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl on " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PageId=PageMaster.PageId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID WHERE PageMaster.PageName not in('AttendenceDeviations.aspx','AttendenceApproval.aspx') and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "' AND " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "' and " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PricePID='" + Session["PriceId"].ToString() + "'";
                                str1211f1 = @" SELECT  dbo.PageMaster.PageName, PageMaster.MainMenuId as MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,dbo.PageMaster.PageId
                                               FROM dbo.PageMaster INNER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId INNER JOIN dbo.DefaultRole ON dbo.DefaultProductPageRoleWiseAccess.RoleId = dbo.DefaultRole.RoleId INNER JOIN dbo.PagePricePlanCategoryAccess ON dbo.DefaultProductPageRoleWiseAccess.PageId = dbo.PagePricePlanCategoryAccess.Pageid INNER JOIN dbo.PricePlanMaster ON dbo.PagePricePlanCategoryAccess.PricePlanCategoryId = dbo.PricePlanMaster.PriceplancatId
                                               Where PricePlanMaster.PricePlanId='" + encryptstrringser(Session["PriceId"].ToString()) + "'  and dbo.DefaultRole.RoleName='" + encryptstrringser(Session["rolename"].ToString()) + "'  AND (dbo.PageMaster.Active = '" + encryptstrringser("True") + "') AND (dbo.PageMaster.ManuAccess = '" + encryptstrringser("True") + "') AND PageMaster.PageId='" + encryptstrringser(childrenItem111.Text) + "' ";//encryptstrringser(
                                SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, PageConn.serverconn());//PageConn.busclient()
                                DataTable dt121f1 = new DataTable();
                                da121f1.Fill(dt121f1);
                                if (dt121f1.Rows.Count > 0)
                                {
                                }
                                else
                                {
                                    childrenItem11.Text = "<font color='#f44242'> " + childrenItem11.Text + " </font> ";
                                }
                                //*********************
                                //**********************************
                                //********************************************************************************************************                               
                            }
                        }
                        foreach (DataRow childItem in parentItem.GetChildRows("Children3"))
                        {
                            if (f3 == 0)
                            {
                                Menu1.Items.Add(categoryItem);
                                f3 += 1;
                            }
                            MenuItem childrenItem111 = new MenuItem(decryptstringserv((string)childItem["PageId"].ToString()));
                            MenuItem childrenItem11 = new MenuItem(decryptstringserv((string)childItem["PageTitle"]));
                            MenuItem childrenItem112 = new MenuItem(decryptstringserv((string)childItem["PageName"]));

                            string stpageid = (string)childItem["PageId"].ToString();
                            string catid = "";
                            string str1211f1cat = " SELECT dbo.MainMenuMaster.MainMenucatId, dbo.PageMaster.FolderName as Path FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId where PageId='" + stpageid + "'  ";
                            SqlDataAdapter da121f1cat = new SqlDataAdapter(str1211f1cat, PageConn.serverconn());
                            DataTable dt121f1cat = new DataTable();
                            da121f1cat.Fill(dt121f1cat);
                            if (dt121f1cat.Rows.Count > 0)
                            {
                                catid = decryptstringserv(dt121f1cat.Rows[0]["MainMenucatId"].ToString());
                            }
                            childrenItem11.NavigateUrl = "~/Shoppingcart/admin/" + childrenItem112.Text + "?cat=" + catid + "";
                            categoryItem.ChildItems.Add(childrenItem11);
                            //********************************************************
                            //**********************************
                            //*********************
                            string str1211f1 = "SELECT Distinct PageMaster.MainMenuId as MenuId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,PageMaster.PageId FROM PageMaster inner join " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl on " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PageId=PageMaster.PageId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID WHERE PageMaster.PageName not in('AttendenceDeviations.aspx','AttendenceApproval.aspx') and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "' AND " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "' and " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PricePID='" + Session["PriceId"].ToString() + "'";
                            str1211f1 = @" SELECT  dbo.PageMaster.PageName, PageMaster.MainMenuId as MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,dbo.PageMaster.PageId
                                           FROM dbo.PageMaster INNER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId INNER JOIN dbo.DefaultRole ON dbo.DefaultProductPageRoleWiseAccess.RoleId = dbo.DefaultRole.RoleId INNER JOIN dbo.PagePricePlanCategoryAccess ON dbo.DefaultProductPageRoleWiseAccess.PageId = dbo.PagePricePlanCategoryAccess.Pageid INNER JOIN dbo.PricePlanMaster ON dbo.PagePricePlanCategoryAccess.PricePlanCategoryId = dbo.PricePlanMaster.PriceplancatId
                                           Where PricePlanMaster.PricePlanId='" + encryptstrringser(Session["PriceId"].ToString()) + "'  and dbo.DefaultRole.RoleName='" + encryptstrringser(Session["rolename"].ToString()) + "'  AND (dbo.PageMaster.Active = '" + encryptstrringser("True") + "') AND (dbo.PageMaster.ManuAccess = '" + encryptstrringser("True") + "') AND PageMaster.PageId='" + encryptstrringser(childrenItem111.Text) + "' ";//encryptstrringser(
                            SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, PageConn.serverconn());//PageConn.busclient()
                            DataTable dt121f1 = new DataTable();
                            da121f1.Fill(dt121f1);
                            if (dt121f1.Rows.Count > 0)
                            {

                            }
                            else
                            {
                                childrenItem11.Text = "<font color='#f44242'> " + childrenItem11.Text + " </font> ";

                            }
                            //*********************
                            //**********************************
                            //*********************************************************


                        }
                    }

                }
            }
            else
            {
                int bb = 0;
                foreach (DataRow parentItem in ds.Tables["parent"].Rows)
                {

                    MenuItem categoryItem = new MenuItem(decryptstringserv((string)parentItem["MainMenuName"]));

                    //Menu1.Items.Add(categoryItem);
                    if (categoryItem.Text != "0")
                    {
                        foreach (DataRow childItem in parentItem.GetChildRows("Children"))
                        {

                            MenuItem childrenItem = new MenuItem(decryptstringserv((string)childItem["SubMenuName"]));

                            Menu1.Items.Add(childrenItem);
                            foreach (DataRow subchildItem in childItem.GetChildRows("Children2"))
                            {

                                MenuItem childrenItem111 = new MenuItem(decryptstringserv((string)subchildItem["PageId"].ToString()));
                                MenuItem childrenItem11 = new MenuItem(decryptstringserv((string)subchildItem["PageTitle"]));
                                MenuItem childrenItem112 = new MenuItem((string)subchildItem["PageName"]);
                                string stpageid = (string)subchildItem["PageId"].ToString();
                                string catid = "";
                                string str1211f1cat = " SELECT dbo.MainMenuMaster.MainMenucatId, dbo.PageMaster.FolderName as Path FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId where PageId='" + stpageid + "'  ";
                                SqlDataAdapter da121f1cat = new SqlDataAdapter(str1211f1cat, PageConn.serverconn());
                                DataTable dt121f1cat = new DataTable();
                                da121f1cat.Fill(dt121f1cat);
                                if (dt121f1cat.Rows.Count > 0)
                                {
                                    catid = decryptstringserv(dt121f1cat.Rows[0]["MainMenucatId"].ToString());
                                }

                                childrenItem11.NavigateUrl = "~/Shoppingcart/admin/" + childrenItem112.Text + "?cat=" + catid + "";

                                childrenItem.ChildItems.Add(childrenItem11);

                                //********************************************************
                                //**********************************
                                //*********************
                                string str1211f1 = "SELECT Distinct PageMaster.MainMenuId as MenuId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,PageMaster.PageId FROM PageMaster inner join " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl on " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PageId=PageMaster.PageId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID WHERE PageMaster.PageName not in('AttendenceDeviations.aspx','AttendenceApproval.aspx') and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "' AND " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "' and " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PricePID='" + Session["PriceId"].ToString() + "'";
                                str1211f1 = @" SELECT  dbo.PageMaster.PageName, PageMaster.MainMenuId as MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,dbo.PageMaster.PageId
                                               FROM   dbo.PageMaster INNER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId INNER JOIN dbo.DefaultRole ON dbo.DefaultProductPageRoleWiseAccess.RoleId = dbo.DefaultRole.RoleId INNER JOIN dbo.PagePricePlanCategoryAccess ON dbo.DefaultProductPageRoleWiseAccess.PageId = dbo.PagePricePlanCategoryAccess.Pageid INNER JOIN dbo.PricePlanMaster ON dbo.PagePricePlanCategoryAccess.PricePlanCategoryId = dbo.PricePlanMaster.PriceplancatId
                                               Where PricePlanMaster.PricePlanId='" + encryptstrringser(Session["PriceId"].ToString()) + "'  and dbo.DefaultRole.RoleName='" + encryptstrringser(Session["rolename"].ToString()) + "'  AND (dbo.PageMaster.Active = '" + encryptstrringser("True") + "') AND (dbo.PageMaster.ManuAccess = '" + encryptstrringser("True") + "') AND PageMaster.PageId='" + encryptstrringser(childrenItem111.Text) + "' ";//encryptstrringser(
                                SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, PageConn.serverconn());//PageConn.busclient()
                                DataTable dt121f1 = new DataTable();
                                da121f1.Fill(dt121f1);
                                if (dt121f1.Rows.Count > 0)
                                {



                                }
                                else
                                {
                                    childrenItem11.Text = "<font color='#f44242'> " + childrenItem11.Text + " </font> ";

                                }
                                //*********************
                                //**********************************
                                //*********************************************************

                            }
                            if (bb == 0)
                            {
                                foreach (DataRow childItem1 in parentItem.GetChildRows("Children3"))
                                {

                                    MenuItem childrenItem111 = new MenuItem(decryptstringserv((string)childItem1["PageId"].ToString()));
                                    MenuItem childrenItem11 = new MenuItem(decryptstringserv((string)childItem1["PageTitle"].ToString()));
                                    MenuItem childrenItem112 = new MenuItem((string)childItem1["PageName"]);
                                    string stpageid = (string)childItem1["PageId"].ToString();
                                    string catid = "";
                                    string str1211f1cat = " SELECT dbo.MainMenuMaster.MainMenucatId, dbo.PageMaster.FolderName as Path FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId where PageId='" + stpageid + "'  ";
                                    SqlDataAdapter da121f1cat = new SqlDataAdapter(str1211f1cat, PageConn.serverconn());
                                    DataTable dt121f1cat = new DataTable();
                                    da121f1cat.Fill(dt121f1cat);
                                    if (dt121f1cat.Rows.Count > 0)
                                    {
                                        catid = decryptstringserv(dt121f1cat.Rows[0]["MainMenucatId"].ToString());
                                    }
                                    childrenItem11.NavigateUrl = "~/Shoppingcart/admin/" + childrenItem112.Text + "?cat=" + catid + "";


                                    childrenItem.ChildItems.Add(childrenItem11);

                                    //********************************************************
                                    //**********************************
                                    //*********************
                                    string str1211f1 = "SELECT Distinct PageMaster.MainMenuId as MenuId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,PageMaster.PageId FROM PageMaster inner join " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl on " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PageId=PageMaster.PageId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID WHERE PageMaster.PageName not in('AttendenceDeviations.aspx','AttendenceApproval.aspx') and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess=1) and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(verid) + "' AND " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "' and " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PricePID='" + Session["PriceId"].ToString() + "'";
                                    str1211f1 = @" SELECT  dbo.PageMaster.PageName, PageMaster.MainMenuId as MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId,dbo.PageMaster.PageId
                                                   FROM     dbo.PageMaster INNER JOIN dbo.PricePlanMaster INNER JOIN dbo.PagePricePlanCategoryAccess ON dbo.PricePlanMaster.PriceplancatId = dbo.PagePricePlanCategoryAccess.PricePlanCategoryId INNER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PagePricePlanCategoryAccess.Pageid = dbo.DefaultProductPageRoleWiseAccess.PageId ON  dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId INNER JOIN dbo.DefaultRole ON dbo.DefaultProductPageRoleWiseAccess.RoleId = dbo.DefaultRole.RoleId
                                                    Where dbo.PricePlanMaster.PricePlanId='" + encryptstrringser(Session["PriceId"].ToString()) + "'  and dbo.DefaultRole.RoleName='" + encryptstrringser(Session["rolename"].ToString()) + "'  AND (dbo.PageMaster.Active = '" + encryptstrringser("True") + "') AND (dbo.PageMaster.ManuAccess = '" + encryptstrringser("True") + "') AND PageMaster.PageId='" + encryptstrringser(childrenItem111.Text) + "' ";//encryptstrringser(
                                    SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, PageConn.serverconn());//PageConn.busclient()
                                    DataTable dt121f1 = new DataTable();
                                    da121f1.Fill(dt121f1);
                                    if (dt121f1.Rows.Count > 0)
                                    {



                                    }
                                    else
                                    {
                                        childrenItem11.Text = "<font color='#f44242'> " + childrenItem11.Text + " </font> ";

                                    }
                                    //*********************
                                    //**********************************
                                    //*********************************************************

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
        Menu1.Items.Clear();
        string main1 = "";
        string main2 = "";
        string main3 = "";
        string manuid = "";
        string rsubmanu = "";
        string rpagemanu = "";
        string submanuid = "";

        string mcount = "0";
        string scount = "0";

        string ssddl = "";
        if (ddlcategory.SelectedIndex > 0)
        {
            ssddl = " AND  dbo.MainMenuMaster.MainMenucatId='" + ddlcategory.SelectedValue + "'";
        }     
        Session["rolename"] = ddlrolemode.SelectedItem.Text;
        Session["verId"] = ddlProductname.SelectedItem.Text;
        string verid = Session["verId"].ToString();
        string pm = "";
        //1nd Done
        string str1211f1 = "";
        str1211f1 = @" SELECT  dbo.PageMaster.PageName, dbo.PageMaster.MainMenuId AS MenuId, CASE WHEN (PageMaster.SubMenuId IS NULL) THEN '0' ELSE PageMaster.SubMenuId END AS SubMenuId, dbo.PageMaster.PageId
                       FROM dbo.PagePricePlanCategoryAccess INNER JOIN dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId ON  dbo.PagePricePlanCategoryAccess.Pageid = dbo.PageMaster.PageId INNER JOIN dbo.Priceplancategory ON dbo.PagePricePlanCategoryAccess.PricePlanCategoryId = dbo.Priceplancategory.ID INNER JOIN dbo.DefaultProductPageRoleWiseAccess INNER JOIN dbo.DefaultRole ON dbo.DefaultProductPageRoleWiseAccess.RoleId = dbo.DefaultRole.RoleId ON dbo.PagePricePlanCategoryAccess.Pageid = dbo.DefaultProductPageRoleWiseAccess.PageId 
                       Where (dbo.PageMaster.Active = '" + encryptstrringser("True") + "') AND (dbo.PageMaster.ManuAccess = '" + encryptstrringser("True") + "') AND dbo.DefaultRole.VersionId='" + encryptstrringser(Session["verId"].ToString()) + "'   AND dbo.Priceplancategory.PortalId='" + encryptstrringser(Session["Porid"].ToString()) + "'  " + ssddl + " ";
        SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, con);
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

        if (main2.Length > 0)
        {
            main2 = main2.Remove(main2.Length - 1);
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
            //4th
            string str = "SELECT distinct MainMenuMaster.* FROM MainMenuMaster inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId  " +
                " where MainMenuMaster.MainMenuId in(" + manuid + ")    " + ssddl + "  Order by MainMenuIndex ASC";//and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "') and dbo.PageMaster.Active='" + encryptstrringser("True") + "'
            SqlDataAdapter adpcat = new SqlDataAdapter(str, con);
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
                string ax = "SELECT distinct MainMenuMaster.*,PageId FROM MainMenuMaster inner join PageMaster  on PageMaster.MainMenuId = MainMenuMaster.MainMenuId where MainMenuMaster.MainMenuId in(" + main1 + ") and  (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' ) " + ssddl + " Order by MainMenuIndex ASC";
                DataTable drtc = new DataTable();
                SqlDataAdapter asc = new SqlDataAdapter(ax, con);
                asc.Fill(drtc);
                foreach (DataRow dts in drtc.Rows)
                {
                    rpagemanu += "'" + dts["PageId"] + "',";
                }
            }
            if (rsubmanu.Length != 0)
            {
                //5th
                if (main1.Length != 0)
                {
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where   SubMenuMaster.SubMenuId in(" + rsubmanu + ") and MainMenuMaster.MainMenuId in(" + main1 + ")  Order by SubMenuIndex ASC";//and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' )
                }
                else
                {
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where   SubMenuMaster.SubMenuId in(" + rsubmanu + ")  Order by SubMenuIndex ASC";                   //and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' )
                }
            }
            else
            {
                if (main1.Length != 0)
                {
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where    MainMenuMaster.MainMenuId In( " + main1 + ")  Order by SubMenuIndex ASC";                    //and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' )
                }
                else if (manuid.Length != 0)
                {
                    str11 = " Select distinct SubMenuMaster.* from  PageMaster inner join  SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId where    MainMenuMaster.MainMenuId In(" + manuid + ")  Order by SubMenuIndex ASC";                    //and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' )
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
            if (rpagemanu.Length > 0)
            {
                rpagemanu = rpagemanu.Remove(rpagemanu.Length - 1);
            }
            string str15 = ""; //6th
            if (manuid.Length != 0 && rpagemanu.Length != 0)
            {
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  " +
                    " where     PageMaster.VersionInfoMasterId='" + encryptstrringser(verid) + "' and (MainMenuId In( " + manuid + ")) and PageMaster.PageId in(" + rpagemanu + ") and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' )  AND (SubMenuId='" + encryptstrringser("0") + "'  or SubMenuId IS NULL) order by PageIndex ASC";

                str15 = @" SELECT DISTINCT PageIndex, MainMenuId, MainMenuId , PageName, PageTitle, PageId, CASE WHEN (PageMaster.SubMenuId IS NULL) THEN '0' ELSE PageMaster.SubMenuId END AS SubMenuId FROM dbo.PageMaster 
                             where     PageMaster.VersionInfoMasterId='" + encryptstrringser(verid) + "' and (MainMenuId In( " + manuid + ")) and PageMaster.PageId in(" + rpagemanu + ") and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' )  AND (SubMenuId='" + encryptstrringser("0") + "'  or SubMenuId IS NULL) order by PageIndex ASC";
            }
            else if (main1.Length != 0)
            {
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where  PageMaster.VersionInfoMasterId='" + encryptstrringser(verid) + "'  and (MainMenuId In( " + main1 + "))  and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' ) order by PageIndex ASC";
            }
            else if (manuid.Length != 0)
            {
                str15 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId, PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where    PageMaster.VersionInfoMasterId='" + encryptstrringser(verid) + "' and (MainMenuId In( " + manuid + ")) and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' ) order by PageIndex ASC";
            }
            SqlDataAdapter adp115 = new SqlDataAdapter(str15, PageConn.serverconn());
            DataSet ds125 = new DataSet();
            adp115.Fill(ds, "leafchild1");
            string str1 = "";
            if (rpagemanu.Length > 0)
            {
                //7th
                if (submanuid.Length > 0)
                {
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where   (PageMaster.SubMenuId in(" + submanuid + ")) and (PageMaster.PageId in(" + rpagemanu + ")) and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' )  order by PageIndex  Asc";
                    str1 = @"SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM dbo.SubMenuMaster INNER JOIN dbo.PageMaster ON dbo.PageMaster.SubMenuId = dbo.SubMenuMaster.SubMenuId  where   (PageMaster.SubMenuId in(" + submanuid + ")) and (PageMaster.PageId in(" + rpagemanu + ")) and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' )  order by PageIndex  Asc";
                }
                else
                {
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where   PageMaster.VersionInfoMasterId='" + encryptstrringser(verid) + "'  and (PageMaster.PageId in(" + rpagemanu + "))  and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' ) order by PageIndex ASC";
                }
                SqlDataAdapter adp11 = new SqlDataAdapter(str1, PageConn.serverconn());
                DataSet ds12 = new DataSet();
                adp11.Fill(ds, "leafchild");
            }
            else
            {
                if (submanuid.Length > 0)
                {
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON  PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where    PageMaster.VersionInfoMasterId='" + encryptstrringser(verid) + "'  and  (PageMaster.SubMenuId in(" + submanuid + ")) and (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' ) order by PageIndex ASC";
                    SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                    DataSet ds12 = new DataSet();
                    adp11.Fill(ds, "leafchild");
                }
                else
                {
                    str1 = " SELECT distinct PageIndex, PageMaster.MainMenuId, PageMaster.MainMenuId,PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId,CASE WHEN (PageMaster.SubMenuId IS NULL)THEN '0' ELSE PageMaster.SubMenuId END as SubMenuId FROM SubMenuMaster inner join  PageMaster ON PageMaster.SubMenuId= SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.Pageid  where     (PageMaster.ManuAccess IS NULL or PageMaster.ManuAccess='" + encryptstrringser("True") + "' )  order by PageIndex ASC";                  
                    SqlDataAdapter adp11 = new SqlDataAdapter(str1, PageConn.serverconn());
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

