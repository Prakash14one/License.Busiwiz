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

public partial class UserRolePageAccess : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);  
    public static string defaRoleid = "";
    public static string gtype = ""; 
    DataSet dt;
    SqlConnection conn;
    public SqlConnection connweb;
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            FillProduct();          
        }
    }
    protected void ClrGrid()
    {
        grdmain.DataSource = null;
        grdmain.DataBind();

        GV_SubMenu.DataSource = null;
        GV_SubMenu.DataBind();

        GV_PageAccess.DataSource = null;
        GV_PageAccess.DataBind();
    }
    protected void ClrPlushMinus()
    {
        defaRoleid = "";
        pnl_PlushMinush.Visible = false;

        pnlmain.Visible = false;
        imgmainmanu.ImageUrl = "Images/plus.png";
        imgmainmanu.AlternateText = "Plush";

        pnlpage.Visible = false;
        imgsubm.ImageUrl = "Images/plus.png";
        imgsubm.AlternateText = "Plush";

        pnlsubmanu.Visible = false;
        imgpage.ImageUrl = "Images/plus.png";
        imgpage.AlternateText = "Plush";
    }
    protected void ShowGrid()
    {
        ClrGrid();
        ClrPlushMinus();    
        rdmode.SelectedValue = "2";     
        gtype = "";       
        pnldisp.Visible = true;
        pnl_PlushMinush.Visible = false;
        pnl_btnsubmit.Visible = false;       
        if (ddlrolemode.SelectedIndex > 0)
        {
            defaRoleid = ddlrolemode.SelectedValue;
            DataTable dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.DefaultProductPageRoleWiseAccess.Id, dbo.DefaultProductPageRoleWiseAccess.PageId, dbo.DefaultProductPageRoleWiseAccess.RoleId, dbo.DefaultProductPageRoleWiseAccess.Edit_Right, dbo.DefaultProductPageRoleWiseAccess.Delete_Right, dbo.DefaultProductPageRoleWiseAccess.Download_Right, dbo.DefaultProductPageRoleWiseAccess.Insert_Right, dbo.DefaultProductPageRoleWiseAccess.Update_Right, dbo.DefaultProductPageRoleWiseAccess.View_Right, dbo.DefaultProductPageRoleWiseAccess.Go_Right, dbo.DefaultProductPageRoleWiseAccess.SendMail_Right, dbo.DefaultProductPageRoleWiseAccess.AccessRight FROM dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId INNER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId where RoleId='" + ddlrolemode.SelectedValue + "'");
            DataTable DtanyRoleAvail = MyCommonfile.selectBZ("SELECT DISTINCT dbo.DefaultRole.RoleId FROM dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId INNER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId INNER JOIN dbo.DefaultRole ON dbo.DefaultProductPageRoleWiseAccess.RoleId = dbo.DefaultRole.RoleId Where DefaultRole.VersionId=" + ddlProductname.SelectedValue + " ");
            if (dtcln.Rows.Count > 0 || DtanyRoleAvail.Rows.Count ==0)
            {
                Rbtn_CopyAccess.SelectedValue = "0";
                Rbtn_CopyAccess.Visible = false;
                DDLCopyFromDesignetion.Visible = false;
                lbl_CopyAccess.Visible = false;
                pnl_DisplayMode.Visible = true;
                FillData1();
                Rbtn_CopyAccess.Visible = false;
            }
            else
            {
                Rbtn_CopyAccess.Visible = true;
                lbl_CopyAccess.Visible = true;
                pnl_DisplayMode.Visible = false;
                DDLCopyFromDesignetion.Visible = false;
                if (Rbtn_CopyAccess.SelectedValue == "1")
                {
                    DDLCopyFromDesignetion.Visible = true;
                }
                FillCopyFrom();
            }
        }
    }
    protected void FillData1()
    {
        pnldisp.Visible = true;
        pnl_PlushMinush.Visible = true;
        GridMainMenu();
        if (Rbtn_CopyAccess.SelectedValue == "0")
        {
            if (rdmode.SelectedValue == "3")
            {
                gtype = "";
                ClrPlushMinus();
                pnlmain.Visible = true;
                pnlpage.Visible = true;
                pnlsubmanu.Visible = true;
                FillSubMenu();
                FillGV_PageAccess_UsingMainMenu();
                pnl_btnsubmit.Visible = false;
                pnl_PlushMinush.Enabled = false;
                pnl_PlushMinush.Visible = true;

            }
            else if (rdmode.SelectedValue == "2")
            {
                pnl_btnsubmit.Visible = false;
                btnsub.Text = "Update";
                ClrPlushMinus();
                pnl_PlushMinush.Enabled = true;
                pnl_PlushMinush.Visible = true;
                pnl_btnsubmit.Visible = true;
            }
        }
        else if (Rbtn_CopyAccess.SelectedValue == "1")
        {
            DDLCopyFromDesignetion.Visible = true;
            lbl_desig.Visible = true;
           // FillRoleDDLCopyFrom();
            defaRoleid = DDLCopyFromDesignetion.SelectedValue;
            PagesGridShowPlush();
        }        
    }
    protected void FillCopyFrom()
    {
        pnl_btnsubmit.Visible = true;
        if (Rbtn_CopyAccess.SelectedValue == "0")
        {
            pnl_DisplayMode.Visible = true;
            DDLCopyFromDesignetion.Visible = false;
            lbl_desig.Visible = false;
            FillData1();
        }
        if (Rbtn_CopyAccess.SelectedValue == "1")
        {
            DDLCopyFromDesignetion.Visible = true;
            lbl_desig.Visible = true;
            FillRoleDDLCopyFrom();
            defaRoleid = DDLCopyFromDesignetion.SelectedValue;
            PagesGridShowPlush();
        }
    }
    protected void FillProduct()
    {
        string strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion";
         strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM dbo.ClientProductTableMaster INNER JOIN dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.VersionNo = dbo.VersionInfoMaster.VersionInfoName ON dbo.ClientProductTableMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ClientProductTableMaster.Databaseid = dbo.ProductCodeDetailTbl.Id where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active =1 order  by productversion ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "productversion";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");        
    }
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {   
        FillWebsiteMaster();
    }
    protected void FillWebsiteMaster()
    {
        string strcln = "  ";//
       
            strcln = " and WebsiteMaster.Status=1 ";
        
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
        FillRoleDDL();
    }



    protected void FillRoleDDLCopyFrom()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.DefaultRole.RoleId, dbo.DefaultDept.DeptName + ':' + dbo.DefaultDesignationTbl.DesignationName AS RoleName FROM    dbo.DefaultRole INNER JOIN dbo.DefaultDesignationTbl ON dbo.DefaultDesignationTbl.RoleId = dbo.DefaultRole.RoleId INNER JOIN dbo.DefaultDept ON dbo.DefaultDept.DeptId = dbo.DefaultDesignationTbl.DeptId INNER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.DefaultDesignationTbl.RoleId = dbo.DefaultProductPageRoleWiseAccess.RoleId where  DefaultDept.VersionId='" + ddlProductname.SelectedValue + "' order by RoleName ");
       

        DDLCopyFromDesignetion.DataSource = dtcln;
        DDLCopyFromDesignetion.DataValueField = "RoleId";
        DDLCopyFromDesignetion.DataTextField = "RoleName";
        DDLCopyFromDesignetion.DataBind();
        DDLCopyFromDesignetion.Items.Insert(0, "Select");
        DDLCopyFromDesignetion.Items[0].Value = "0";
    }
    protected void DDLCopyFromDesignetion_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillData1();   
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
        ShowGrid();
    }                
    protected void rdDisplay_Mode_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillData1();
    }
    protected void Rbtn_CopyAccess_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowGrid();
    }





    protected void imgmainmanu_Click(object sender, ImageClickEventArgs e)
    {
        pnlpage.Visible = false;
        pnlsubmanu.Visible = false;
        if (imgmainmanu.AlternateText == "Plush")
        {
            gtype = "1";
            imgmainmanu.ImageUrl = "Images/minus.png";
            imgmainmanu.AlternateText = "Minus";
            pnlmain.Visible = true;
            pnlpage.Visible = false;
            pnlsubmanu.Visible = false;
            imgsubm.ImageUrl = "Images/plus.png";
            imgsubm.AlternateText = "Plush";
            imgpage.ImageUrl = "Images/plus.png";
            imgpage.AlternateText = "Plush";           
            fillaccessgridA();
        }
        else
        {
            imgmainmanu.ImageUrl = "Images/plus.png";
            imgmainmanu.AlternateText = "Plush";
            pnlmain.Visible = false;
        }
        if (pnlpage.Visible == true || pnlmain.Visible == true || pnlsubmanu.Visible == true)
        {
            pnl_btnsubmit.Visible = true;
        }
        else
        {
            pnl_btnsubmit.Visible = false;
        }
    }
    protected void imgsubm_Click(object sender, ImageClickEventArgs e)
    {
        pnlmain.Visible = false;
        pnlpage.Visible = false;
        if (imgsubm.AlternateText == "Plush")
        {
            imgsubm.ImageUrl = "Images/minus.png";
            imgsubm.AlternateText = "Minus";
            pnlsubmanu.Visible = true;
            imgmainmanu.ImageUrl = "Images/plus.png";
            imgmainmanu.AlternateText = "Plush";
            imgpage.ImageUrl = "Images/plus.png";
            imgpage.AlternateText = "Plush";
            FillCategorysearch();
            FilterMainMenu();
            FillSubMenu();
            gtype = "2";
            fillaccessgrid();
        }
        else
        {
            imgsubm.ImageUrl = "Images/plus.png";
            imgsubm.AlternateText = "Plush";
            pnlsubmanu.Visible = false;
            gtype = "1";
        }
        if (pnlpage.Visible == true || pnlmain.Visible == true || pnlsubmanu.Visible == true)
        {
            pnl_btnsubmit.Visible = true;
        }
        else
        {
            pnl_btnsubmit.Visible = false;
        }
    }
    protected void Img_PageAccessPlush_Click(object sender, ImageClickEventArgs e)
    {
       PagesGridShowPlush();
    }
    protected void PagesGridShowPlush()
    {
         pnlmain.Visible = false;
        pnlsubmanu.Visible = false;
        if (imgpage.AlternateText == "Plush")
        {
            imgpage.ImageUrl = "Images/minus.png";
            imgpage.AlternateText = "Minus";
            pnlpage.Visible = true;
            FillCategorysearch();
            FilterMainMenu();
            imgmainmanu.ImageUrl = "Images/plus.png";
            imgmainmanu.AlternateText = "Plush";
            imgsubm.ImageUrl = "Images/plus.png";
            imgsubm.AlternateText = "Plush";
            if (gtype == "2")
            {
                fillPageSub();
            }
            else
            {
                FillGV_PageAccess_UsingMainMenu();
            }
            fillaccessgrid2A();
        }
        else
        {
            imgpage.ImageUrl = "Images/plus.png";
            imgpage.AlternateText = "Plush";
            pnlpage.Visible = false;
        }
        if (pnlpage.Visible == true || pnlmain.Visible == true || pnlsubmanu.Visible == true)
        {
            pnl_btnsubmit.Visible = true;
        }
        else
        {
            pnl_btnsubmit.Visible = false;
        }
    }


    protected void FillCategorysearch()
    {
        string strlan = " Select MainMenucatId, MainMenuCatName  FROM dbo.Mainmenucategory INNER JOIN dbo.MasterPageMaster ON dbo.Mainmenucategory.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId where WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "'";
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
        FilterMainMenu();
        FilterSubMenu();
        FillGV_PageAccess_UsingMainMenu();
    }
    protected void FilterMainMenu()
    {
        string cat = "";
        if (DDLCategoryS.SelectedIndex > 0)
        {
            cat = " and MainMenuMaster.MainMenucatId='" + DDLCategoryS.SelectedValue + "'";
        }
        ddlmainfilter.Items.Clear();
        string strcln = " SELECT DISTINCT dbo.MainMenuMaster.MainMenuId, dbo.MainMenuMaster.MainMenuName , dbo.WebsiteSection.WebsiteMasterId FROM dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId Where WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "' " + cat + " Order by dbo.MainMenuMaster.MainMenuName ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlmainfilter.DataSource = dtcln;
        ddlmainfilter.DataValueField = "MainMenuId";
        ddlmainfilter.DataTextField = "MainMenuName";
        ddlmainfilter.DataBind();
        ddlmainfilter.Items.Insert(0, "All");
        ddlmainfilter.Items[0].Value = "0";

        ddlmailpagefilter.DataSource = dtcln;
        ddlmailpagefilter.Items.Clear();
        ddlmailpagefilter.DataValueField = "MainMenuId";
        ddlmailpagefilter.DataTextField = "MainMenuName";
        ddlmailpagefilter.DataBind();
        ddlmailpagefilter.Items.Insert(0, "All");
        ddlmailpagefilter.Items[0].Value = "0";
        ddlsubpagefilter.Items.Clear();
        ddlsubpagefilter.Items.Insert(0, "All");
        ddlsubpagefilter.Items[0].Value = "0";
    }
    protected void ddlmainfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSubMenu();
        FillGV_PageAccess_UsingMainMenu();
    }
    protected void ddlmailpagefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterSubMenu();
        ddlsubpagefilter_SelectedIndexChanged(sender, e);
    }
    protected void FilterSubMenu()
    {
        ddlsubpagefilter.Items.Clear();
        string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId  where  SubMenuMaster.MainMenuId='" + ddlmailpagefilter.SelectedValue + "' Order by SubMenuMaster.SubMenuName  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlsubpagefilter.DataSource = dtcln;

        ddlsubpagefilter.DataValueField = "SubMenuId";
        ddlsubpagefilter.DataTextField = "SubMenuName";
        ddlsubpagefilter.DataBind();

        ddlsubpagefilter.Items.Insert(0, "All");
        ddlsubpagefilter.Items[0].Value = "0";
    }
    protected void ddlsubpagefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gtype == "2")
        {
            fillPageSub();
            FillGV_PageAccess_UsingMainMenu();
        }
        else
        {
            FillGV_PageAccess_UsingMainMenu();
        }
    }


    protected void GridMainMenu()
    {
        string par = "";
        DataTable dtTemp = new DataTable();
        DataTable dts = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.MainMenuMaster.MainMenuId, dbo.MainMenuMaster.MainMenuName FROM dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId  where  WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "' " + par + " order by MainMenuMaster.MainMenuName ");
        if (dts.Rows.Count > 0)
        {
            dtTemp = CreatedataManu();
            for (int i = 0; i < dts.Rows.Count; i++)
            {
                DataRow dtadd = dtTemp.NewRow();
                dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                if (rdmode.SelectedValue == "2" || rdmode.SelectedValue == "3")
                {
                    DataTable dtcln1 = MyCommonfile.selectBZ(" SELECT COUNT(DISTINCT dbo.PageMaster.PageId) AS CD FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId where  WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "' and PageMaster.MainMenuId='" + dts.Rows[i]["MainMenuId"] + "' and PageMaster.Active=1 ");
                    DataTable dtcln = new DataTable();
                    dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT COUNT(DISTINCT dbo.DefaultProductPageRoleWiseAccess.PageId) AS CDAccess FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId INNER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId where  PageMaster.MainMenuId='" + dts.Rows[i]["MainMenuId"] + "' and RoleId='" + defaRoleid + "'");
                    if (Convert.ToInt32(dtcln.Rows[0]["CDAccess"]) == Convert.ToInt32(dtcln1.Rows[0]["CD"]) & Convert.ToInt32(dtcln.Rows[0]["CDAccess"]) !=0)
                    {
                        dtadd["AccessRight"] = "1";
                        dtadd["Edit_Right"] = Convert.ToBoolean(1);
                        dtadd["Delete_Right"] = Convert.ToBoolean(1);
                        dtadd["Download_Right"] = Convert.ToBoolean(1);
                        dtadd["Insert_Right"] = Convert.ToBoolean(1);
                        dtadd["Update_Right"] = Convert.ToBoolean(1);
                        dtadd["View_Right"] = Convert.ToBoolean(1);
                        dtadd["Go_Right"] = Convert.ToBoolean(1);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(1);
                    }
                    else if (Convert.ToInt32(dtcln.Rows[0]["CDAccess"]) > 0)
                    {
                        dtadd["AccessRight"] = "2";
                        dtadd["Edit_Right"] = Convert.ToBoolean(0);
                        dtadd["Delete_Right"] = Convert.ToBoolean(0);
                        dtadd["Download_Right"] = Convert.ToBoolean(0);
                        dtadd["Insert_Right"] = Convert.ToBoolean(0);
                        dtadd["Update_Right"] = Convert.ToBoolean(0);
                        dtadd["View_Right"] = Convert.ToBoolean(0);
                        dtadd["Go_Right"] = Convert.ToBoolean(0);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                    }
                    else
                    {
                        dtadd["AccessRight"] = "0";
                        dtadd["Edit_Right"] = Convert.ToBoolean(0);
                        dtadd["Delete_Right"] = Convert.ToBoolean(0);
                        dtadd["Download_Right"] = Convert.ToBoolean(0);
                        dtadd["Insert_Right"] = Convert.ToBoolean(0);
                        dtadd["Update_Right"] = Convert.ToBoolean(0);
                        dtadd["View_Right"] = Convert.ToBoolean(0);
                        dtadd["Go_Right"] = Convert.ToBoolean(0);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                    }
                   
                }               
                dtTemp.Rows.Add(dtadd);
            }
        }
        grdmain.DataSource = dtTemp;
        if (dtTemp.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = dtTemp.DefaultView;
            hdnsortExp.Value = "MainMenuName";
            hdnsortDir.Value = "Asc";
            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            ViewState["Maing"] = dtTemp;
        }
        grdmain.DataBind();

    }  
    protected void fillg(GridView grd)
    {
        foreach (GridViewRow gd in grd.Rows)
        {
            RadioButtonList rdlist = (RadioButtonList)gd.FindControl("RadioButtonList1");
            CheckBox Checksendmail_Allow = (CheckBox)gd.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)gd.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)gd.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)gd.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)gd.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)gd.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)gd.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)gd.FindControl("CheckBoxGo1");
            CheckBox CheckSendMail_Allow = (CheckBox)gd.FindControl("CheckBoxSendMail1");
            if (rdlist.SelectedValue == "0")
            {
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Enabled = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Enabled = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {                
                Checksendmail_Allow.Enabled = true;
                CheckEdit_Allow.Enabled = true;
                CheckDelete_Allow.Enabled = true;
                CheckDownload_Allow.Enabled = true;
                CheckInsert_Allows.Enabled = true;
                CheckUpdate_Allow.Enabled = true;
                CheckView_Allow.Enabled = true;
                CheckSendMail_Allow.Enabled = true;
                CheckGo_Allow.Enabled = true;
            }
        }
    }
   
   
  
   
  
    
   
   
  

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((RadioButtonList)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");
        RadioButtonList rdlist;
        rdlist = (RadioButtonList)(grdmain.Rows[rinrow].FindControl("RadioButtonList1"));
        CheckBox Checksendmail_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxSendMail1");
        CheckBox CheckEdit_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxEdit1");
        CheckBox CheckDelete_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxDelete1");
        CheckBox CheckDownload_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxDownload1");
        CheckBox CheckInsert_Allows = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxInsert1");
        CheckBox CheckUpdate_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxUpdate1");
        CheckBox CheckView_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxView1");
        CheckBox CheckGo_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxGo1");
        CheckBox CheckSendMail_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxSendMail1");
        if (rdlist.SelectedValue == "0")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "1")
        {
            Checksendmail_Allow.Checked = true;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = true;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = true;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = true;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = true;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = true;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = true;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = true;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = true;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "2")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = true;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = true;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = true;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = true;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = true;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = true;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = true;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = true;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = true;
        }


    }      
   
    protected void RadioButtonListgrdmain_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((RadioButtonList)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;        // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");
        RadioButtonList rdlist;
        rdlist = (RadioButtonList)(grdmain.Rows[rinrow].FindControl("RadioButtonList1"));
        CheckBox Checksendmail_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxSendMail1");
        CheckBox CheckEdit_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxEdit1");
        CheckBox CheckDelete_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxDelete1");
        CheckBox CheckDownload_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxDownload1");
        CheckBox CheckInsert_Allows = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxInsert1");
        CheckBox CheckUpdate_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxUpdate1");
        CheckBox CheckView_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxView1");
        CheckBox CheckGo_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxGo1");
        CheckBox CheckSendMail_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxSendMail1");
        if (rdlist.SelectedValue == "0")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "1")
        {
            Checksendmail_Allow.Checked = true;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = true;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = true;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = true;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = true;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = true;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = true;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = true;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = true;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "2")
        {

            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = true;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = true;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = true;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = true;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = true;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = true;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = true;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = true;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = true;
        }
    }

    protected void fillaccessgrid()
    {



        foreach (GridViewRow dsc in grdmain.Rows)
        {
            RadioButtonList rdlist;

            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");

            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");

            if (rdlist.SelectedValue == "0")
            {
                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {
                Checksendmail_Allow.Checked = true;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = true;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = true;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = true;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = true;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = true;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = true;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = true;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = true;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {

                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = true;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = true;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = true;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = true;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = true;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = true;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = true;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = true;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = true;
            }
        }

    }
 

    protected void grdmain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblma = (Label)e.Row.FindControl("lblma");
            lblma.Text = (lblma.Text);
        }
    }
    protected void Grdsub_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblptypename = (Label)e.Row.FindControl("lblptypename");
            Label lblsubmanuname = (Label)e.Row.FindControl("lblsubmanuname");

            lblptypename.Text = (lblptypename.Text);
            lblsubmanuname.Text = (lblsubmanuname.Text);

        }
    }
    protected void grdpage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblptypename = (Label)e.Row.FindControl("lblptypename");
            Label lblsubmanuname = (Label)e.Row.FindControl("lblsubmanuname");
            Label lblpagename = (Label)e.Row.FindControl("lblpagename");
            lblptypename.Text = (lblptypename.Text);
            lblsubmanuname.Text = (lblsubmanuname.Text);
            lblpagename.Text = (lblpagename.Text);
        }
    }
   
   
    protected void FillSubMenu()
    {
        DataTable dtTemp = CreatedataSubManu();
        string strmanuid = "";
        string filmanu = "";
        if (ddlmainfilter.SelectedIndex > 0)
        {
            filmanu = " and SubMenuMaster.MainMenuId='" + ddlmainfilter.SelectedValue + "'";
        }
        if (gtype != "")
        {
            foreach (GridViewRow item in grdmain.Rows)
            {
                RadioButtonList RadioButtonList1 = (RadioButtonList)item.FindControl("RadioButtonList1");
                Label lblmainmanu = (Label)item.FindControl("lblmainmanu");
                int flg = 0;
                CheckBox CheckEdit_Allow = (CheckBox)item.FindControl("CheckBoxEdit1");
                CheckBox CheckDelete_Allow = (CheckBox)item.FindControl("CheckBoxDelete1");
                CheckBox CheckDownload_Allow = (CheckBox)item.FindControl("CheckBoxDownload1");
                CheckBox CheckInsert_Allows = (CheckBox)item.FindControl("CheckBoxInsert1");
                CheckBox CheckUpdate_Allow = (CheckBox)item.FindControl("CheckBoxUpdate1");
                CheckBox CheckView_Allow = (CheckBox)item.FindControl("CheckBoxView1");
                CheckBox CheckGo_Allow = (CheckBox)item.FindControl("CheckBoxGo1");
                CheckBox CheckSendMail_Allow = (CheckBox)item.FindControl("CheckBoxSendMail1");
                if (filmanu.Length != 0)
                {
                    if (lblmainmanu.Text.ToString() == ddlmainfilter.SelectedValue.ToString())
                    {
                        flg = 1;
                    }
                }
                else
                {
                    flg = 1;
                }
                if (flg == 1)
                {
                    if (RadioButtonList1.SelectedValue == "1" || RadioButtonList1.SelectedValue == "2")
                    {
                        DataTable dts = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.SubMenuMaster.SubMenuName, dbo.MainMenuMaster.MainMenuName + ':' + dbo.SubMenuMaster.SubMenuName AS MainMenuName, dbo.SubMenuMaster.SubMenuId, dbo.MainMenuMaster.MainMenuId FROM dbo.MainMenuMaster INNER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.PageMaster ON dbo.PageMaster.SubMenuId = dbo.SubMenuMaster.SubMenuId where  PageMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' and SubMenuMaster.MainMenuId='" + lblmainmanu.Text + "'  order by SubMenuName ");
                        if (dts.Rows.Count > 0)
                        {
                            for (int i = 0; i < dts.Rows.Count; i++)
                            {
                                DataRow dtadd = dtTemp.NewRow();
                                dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                                dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                                dtadd["SubMenuId"] = Convert.ToString(dts.Rows[i]["SubMenuId"]);
                                dtadd["SubMenuName"] = Convert.ToString(dts.Rows[i]["SubMenuName"]);

                                dtadd["AccessRight"] = RadioButtonList1.SelectedValue;
                                dtadd["Edit_Right"] = CheckEdit_Allow.Checked;
                                dtadd["Delete_Right"] = CheckDelete_Allow.Checked;
                                dtadd["Download_Right"] = CheckDownload_Allow.Checked;
                                dtadd["Insert_Right"] = CheckInsert_Allows.Checked;
                                dtadd["Update_Right"] = CheckUpdate_Allow.Checked;
                                dtadd["View_Right"] = CheckView_Allow.Checked;
                                dtadd["Go_Right"] = CheckGo_Allow.Checked;
                                dtadd["SendMail_Right"] = CheckSendMail_Allow.Checked;

                                dtTemp.Rows.Add(dtadd);
                            }

                        }
                    }
                    else
                    {
                        if (strmanuid.Length != 0)
                        {
                            strmanuid = strmanuid + ",";
                        }
                        strmanuid = strmanuid + "'" + lblmainmanu.Text + "'";
                    }
                }
            }
        }
        if (strmanuid.Length > 0 || gtype == "")
        {
            string RoleD = "";
            if (rdmode.SelectedValue == "3")
            {
               //
            }
            string parm = "";
            if (gtype != "")
            {
                parm = "  and SubMenuMaster.MainMenuId in(" + strmanuid + ")";
            }
            DataTable dts = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.SubMenuMaster.SubMenuName, dbo.MainMenuMaster.MainMenuName + ':' + dbo.SubMenuMaster.SubMenuName AS MainMenuName,  dbo.SubMenuMaster.SubMenuId, dbo.MainMenuMaster.MainMenuId FROM dbo.MainMenuMaster INNER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.PageMaster ON dbo.PageMaster.SubMenuId = dbo.SubMenuMaster.SubMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId Where WebsiteSection.WebsiteMasterId='"+DDLWebsiteC.SelectedValue+"' " + parm + "  order by MainMenuName");
            if (dts.Rows.Count > 0)
            {
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    DataRow dtadd = dtTemp.NewRow();
                    dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                    dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                    dtadd["SubMenuId"] = Convert.ToString(dts.Rows[i]["SubMenuId"]);
                    dtadd["SubMenuName"] = Convert.ToString(dts.Rows[i]["SubMenuName"]);
                    if (rdmode.SelectedValue == "2" || rdmode.SelectedValue == "3")
                    {
                        int accesno1 = 0;
                        int accesno2 = 0;
                        int Edit_Right = 0;
                        int Delete_Right = 0;
                        int Download_Right = 0;
                        int Insert_Right = 0;
                        int Update_Right = 0;
                        int View_Right = 0;
                        int Go_Right = 0;
                        int SendMail_Right = 0;
                        DataTable dtcln1 = MyCommonfile.selectBZ(" SELECT COUNT(DISTINCT dbo.PageMaster.PageId) AS CD FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId where PageMaster.SubMenuId='" + dts.Rows[i]["SubMenuId"] + "'  ");
                        DataTable dtcln = new DataTable();
                        dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.DefaultProductPageRoleWiseAccess.Id, dbo.DefaultProductPageRoleWiseAccess.PageId, dbo.DefaultProductPageRoleWiseAccess.RoleId, dbo.DefaultProductPageRoleWiseAccess.Edit_Right, dbo.DefaultProductPageRoleWiseAccess.Delete_Right, dbo.DefaultProductPageRoleWiseAccess.Download_Right, dbo.DefaultProductPageRoleWiseAccess.Insert_Right, dbo.DefaultProductPageRoleWiseAccess.Update_Right, dbo.DefaultProductPageRoleWiseAccess.View_Right, dbo.DefaultProductPageRoleWiseAccess.Go_Right, dbo.DefaultProductPageRoleWiseAccess.SendMail_Right, dbo.DefaultProductPageRoleWiseAccess.AccessRight FROM dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId INNER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId where  PageMaster.SubMenuId='" + dts.Rows[i]["SubMenuId"] + "' and RoleId='" + defaRoleid + "'");
                        foreach (DataRow item in dtcln.Rows)
                        {
                            if (Convert.ToString(item["AccessRight"]) == "1")
                            {
                                accesno1 = accesno1 + 1;
                            }
                            else if (Convert.ToString(item["AccessRight"]) == "2")
                            {
                                accesno2 = accesno2 + 1;
                            }
                            if (Convert.ToString(item["Edit_Right"]) == "True")
                            {
                                Edit_Right = Edit_Right + 1;
                            }
                            if (Convert.ToString(item["Delete_Right"]) == "True")
                            {
                                Delete_Right = Delete_Right + 1;
                            }
                            if (Convert.ToString(item["Download_Right"]) == "True")
                            {
                                Download_Right = Download_Right + 1;
                            }
                            if (Convert.ToString(item["Insert_Right"]) == "True")
                            {
                                Insert_Right = Insert_Right + 1;
                            }
                            if (Convert.ToString(item["Update_Right"]) == "True")
                            {
                                Update_Right = Update_Right + 1;
                            }
                            if (Convert.ToString(item["View_Right"]) == "True")
                            {
                                View_Right = View_Right + 1;
                            }
                            if (Convert.ToString(item["Go_Right"]) == "True")
                            {
                                Go_Right = Go_Right + 1;
                            }
                            if (Convert.ToString(item["SendMail_Right"]) == "True")
                            {
                                SendMail_Right = SendMail_Right + 1;
                            }

                        }
                        if (accesno1 == Convert.ToInt32(dtcln1.Rows[0]["CD"]) && accesno1 !=0)
                        {
                            dtadd["AccessRight"] = "1";
                            dtadd["Edit_Right"] = Convert.ToBoolean(1);
                            dtadd["Delete_Right"] = Convert.ToBoolean(1);
                            dtadd["Download_Right"] = Convert.ToBoolean(1);
                            dtadd["Insert_Right"] = Convert.ToBoolean(1);
                            dtadd["Update_Right"] = Convert.ToBoolean(1);
                            dtadd["View_Right"] = Convert.ToBoolean(1);
                            dtadd["Go_Right"] = Convert.ToBoolean(1);
                            dtadd["SendMail_Right"] = Convert.ToBoolean(1);

                        }
                        else
                        {
                            if (accesno2 == Convert.ToInt32(dtcln1.Rows[0]["CD"]) && accesno1 !=0)
                            {
                                dtadd["AccessRight"] = "2";
                                if (Edit_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Edit_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Edit_Right"] = Convert.ToBoolean(0);
                                }
                                if (Delete_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Delete_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Delete_Right"] = Convert.ToBoolean(0);
                                }
                                if (Download_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Download_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Download_Right"] = Convert.ToBoolean(0);
                                }
                                if (Insert_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Insert_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Insert_Right"] = Convert.ToBoolean(0);
                                }
                                if (Update_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Update_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Update_Right"] = Convert.ToBoolean(0);
                                }
                                if (View_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["View_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["View_Right"] = Convert.ToBoolean(0);
                                }
                                if (Go_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Go_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Go_Right"] = Convert.ToBoolean(0);
                                }
                                if (SendMail_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["SendMail_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                                }
                            }
                            else
                            {

                                dtadd["AccessRight"] = "0";
                                dtadd["Edit_Right"] = Convert.ToBoolean(0);
                                dtadd["Delete_Right"] = Convert.ToBoolean(0);
                                dtadd["Download_Right"] = Convert.ToBoolean(0);
                                dtadd["Insert_Right"] = Convert.ToBoolean(0);
                                dtadd["Update_Right"] = Convert.ToBoolean(0);
                                dtadd["View_Right"] = Convert.ToBoolean(0);
                                dtadd["Go_Right"] = Convert.ToBoolean(0);
                                dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                            }
                        }
                    }
                    else
                    {
                        dtadd["AccessRight"] = "0";
                        dtadd["Edit_Right"] = Convert.ToBoolean(0);
                        dtadd["Delete_Right"] = Convert.ToBoolean(0);
                        dtadd["Download_Right"] = Convert.ToBoolean(0);
                        dtadd["Insert_Right"] = Convert.ToBoolean(0);
                        dtadd["Update_Right"] = Convert.ToBoolean(0);
                        dtadd["View_Right"] = Convert.ToBoolean(0);
                        dtadd["Go_Right"] = Convert.ToBoolean(0);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                    }
                    dtTemp.Rows.Add(dtadd);
                }
            }
        }
        GV_SubMenu.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;
        hdnsortExp.Value = "MainMenuName";
        hdnsortDir.Value = "Asc";
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        ViewState["Subg"] = dtTemp;
        GV_SubMenu.DataBind();
    }
    protected void FillGV_PageAccess_UsingMainMenu()
    {
        string prid = "";
        if (ddlrolemode.SelectedIndex > 0)
        {
            if (Rbtn_CopyAccess.SelectedValue == "1")
            {
                prid = " and DefaultProductPageRoleWiseAccess.RoleId=(SELECT Top(1) dbo.DefaultRole.RoleId as RoleId from DefaultRole where RoleId='" + DDLCopyFromDesignetion.SelectedValue + "')";
            }
            else
            {
                prid = " and DefaultProductPageRoleWiseAccess.RoleId=(SELECT Top(1) dbo.DefaultRole.RoleId as RoleId from DefaultRole where RoleId='" + ddlrolemode.SelectedValue + "')";
            }
        }
        DataTable dtTemp = CreatedataPage();
        string strmanuid = "";
        string filmanu = "";
        if (ddlmailpagefilter.SelectedIndex > 0)
        {
            filmanu = " and PageMaster.MainMenuId='" + ddlmailpagefilter.SelectedValue + "'";
        }
        string subd = "";
        if (ddlsubpagefilter.SelectedIndex > 0)
        {
            subd = " and SubMenuMaster.SubMenuId='" + ddlsubpagefilter.SelectedValue + "'";
        }
        subd = filmanu + subd;
        if (gtype != "")
        {
            foreach (GridViewRow item in grdmain.Rows)
            {
                RadioButtonList RadioButtonList1 = (RadioButtonList)item.FindControl("RadioButtonList1");
                Label lblmainmanu = (Label)item.FindControl("lblmainmanu");
                int flg = 0;
                CheckBox CheckEdit_Allow = (CheckBox)item.FindControl("CheckBoxEdit1");
                CheckBox CheckDelete_Allow = (CheckBox)item.FindControl("CheckBoxDelete1");
                CheckBox CheckDownload_Allow = (CheckBox)item.FindControl("CheckBoxDownload1");
                CheckBox CheckInsert_Allows = (CheckBox)item.FindControl("CheckBoxInsert1");
                CheckBox CheckUpdate_Allow = (CheckBox)item.FindControl("CheckBoxUpdate1");
                CheckBox CheckView_Allow = (CheckBox)item.FindControl("CheckBoxView1");
                CheckBox CheckGo_Allow = (CheckBox)item.FindControl("CheckBoxGo1");
                CheckBox CheckSendMail_Allow = (CheckBox)item.FindControl("CheckBoxSendMail1");
                if (filmanu.Length != 0)
                {
                    if (ddlsubpagefilter.SelectedIndex == 0)
                    {
                        if (lblmainmanu.Text.ToString() == ddlmailpagefilter.SelectedValue.ToString())
                        {
                            flg = 1;
                        }
                    }
                    if (ddlsubpagefilter.SelectedIndex > 0)
                    {
                        if (lblmainmanu.Text.ToString() == ddlmailpagefilter.SelectedValue.ToString())
                        {
                            subd = "and SubMenuMaster.SubMenuId='" + ddlsubpagefilter.SelectedValue + "'";
                            flg = 1;
                        }
                    }
                }
                else
                {
                    flg = 1;
                }
                if (flg == 1)
                {
                    if (RadioButtonList1.SelectedValue == "1" || RadioButtonList1.SelectedValue == "2")
                    {
                        DataTable dts = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.SubMenuMaster.SubMenuName, dbo.MainMenuMaster.MainMenuName + ':' + dbo.SubMenuMaster.SubMenuName AS MainMenuName, dbo.SubMenuMaster.SubMenuId, dbo.MainMenuMaster.MainMenuId, dbo.PageMaster.PageName, dbo.PageMaster.PageId, dbo.PageMaster.PageTitle FROM  dbo.MainMenuMaster INNER JOIN  dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId where WebsiteSection.WebsiteMasterId='"+DDLWebsiteC.SelectedValue+"'  and PageMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' and PageMaster.MainMenuId='" + lblmainmanu.Text + "' " + subd + " order by PageMaster.PageName ");
                        if (dts.Rows.Count > 0)
                        {
                            for (int i = 0; i < dts.Rows.Count; i++)
                            {
                                DataRow dtadd = dtTemp.NewRow();
                                dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                                dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                                dtadd["SubMenuId"] = Convert.ToString(dts.Rows[i]["SubMenuId"]);
                                dtadd["SubMenuName"] = Convert.ToString(dts.Rows[i]["SubMenuName"]);
                                dtadd["PageId"] = Convert.ToString(dts.Rows[i]["PageId"]);
                                dtadd["PageName"] = Convert.ToString(dts.Rows[i]["PageName"]);
                                dtadd["PageTitle"] = Convert.ToString(dts.Rows[i]["PageTitle"]);

                                dtadd["AccessRight"] = RadioButtonList1.SelectedValue;
                                dtadd["Edit_Right"] = CheckEdit_Allow.Checked;
                                dtadd["Delete_Right"] = CheckDelete_Allow.Checked;
                                dtadd["Download_Right"] = CheckDownload_Allow.Checked;
                                dtadd["Insert_Right"] = CheckInsert_Allows.Checked;
                                dtadd["Update_Right"] = CheckUpdate_Allow.Checked;
                                dtadd["View_Right"] = CheckView_Allow.Checked;
                                dtadd["Go_Right"] = CheckGo_Allow.Checked;
                                dtadd["SendMail_Right"] = CheckSendMail_Allow.Checked;
                                dtTemp.Rows.Add(dtadd);
                            }
                        }
                    }
                    else
                    {
                        if (strmanuid.Length != 0)
                        {
                            strmanuid = strmanuid + ",";
                        }
                        strmanuid = strmanuid + "'" + lblmainmanu.Text + "'";
                    }
                }
            }
        }       
           DataTable  dtsa = MyCommonfile.selectBZ(@" SELECT DISTINCT dbo.SubMenuMaster.SubMenuName, dbo.MainMenuMaster.MainMenuName + ':' + dbo.SubMenuMaster.SubMenuName AS MainMenuName,  dbo.SubMenuMaster.SubMenuId, dbo.MainMenuMaster.MainMenuId, dbo.PageMaster.PageName, dbo.PageMaster.PageId, dbo.PageMaster.PageTitle,  CASE WHEN (DefaultProductPageRoleWiseAccess.AccessRight IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.AccessRight END AS AccessRight,  CASE WHEN (DefaultProductPageRoleWiseAccess.Edit_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Edit_Right END AS Edit_Right,  CASE WHEN (DefaultProductPageRoleWiseAccess.Delete_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Delete_Right END AS Delete_Right,  CASE WHEN (DefaultProductPageRoleWiseAccess.Download_Right IS NULL)  THEN '0' ELSE DefaultProductPageRoleWiseAccess.Download_Right END AS Download_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.Insert_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Insert_Right END AS Insert_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.Update_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Update_Right END AS Update_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.View_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.View_Right END AS View_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.Go_Right IS NULL) THEN '0' ELSE dbo.DefaultProductPageRoleWiseAccess.Go_Right END AS Go_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.SendMail_Right IS NULL)  THEN '0' ELSE dbo.DefaultProductPageRoleWiseAccess.SendMail_Right END AS SendMail_Right
                                               FROM    dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteSection.WebsiteMasterId = dbo.WebsiteMaster.ID LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId LEFT OUTER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId "+prid+" "+
                                               " Where WebsiteSection.WebsiteMasterId='"+DDLWebsiteC.SelectedValue+"' and  WebsiteMaster.ID='" + DDLWebsiteC.SelectedValue + "' and PageMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' " + subd + "  order by PageMaster.PageName ");
            if (dtsa.Rows.Count > 0)
            {
                for (int i = 0; i < dtsa.Rows.Count; i++)
                {
                    DataRow dtadd = dtTemp.NewRow();
                    dtadd["MainMenuId"] = Convert.ToString(dtsa.Rows[i]["MainMenuId"]);
                    dtadd["MainMenuName"] = Convert.ToString(dtsa.Rows[i]["MainMenuName"]);
                    dtadd["SubMenuId"] = Convert.ToString(dtsa.Rows[i]["SubMenuId"]);
                    dtadd["SubMenuName"] = Convert.ToString(dtsa.Rows[i]["SubMenuName"]);
                    dtadd["PageId"] = Convert.ToString(dtsa.Rows[i]["PageId"]);
                    dtadd["PageName"] = Convert.ToString(dtsa.Rows[i]["PageName"]);
                    dtadd["PageTitle"] = Convert.ToString(dtsa.Rows[i]["PageTitle"]);

                    if (rdmode.SelectedValue == "2" || rdmode.SelectedValue == "3")
                    {
                        dtadd["AccessRight"] = Convert.ToString(dtsa.Rows[i]["AccessRight"]);
                        dtadd["Edit_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Edit_Right"]);
                        dtadd["Delete_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Delete_Right"]);
                        dtadd["Download_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Download_Right"]);
                        dtadd["Insert_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Insert_Right"]);
                        dtadd["Update_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Update_Right"]);
                        dtadd["View_Right"] = Convert.ToBoolean(dtsa.Rows[i]["View_Right"]);
                        dtadd["Go_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Go_Right"]);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(dtsa.Rows[i]["SendMail_Right"]);
                    }
                    else if (rdmode.SelectedValue == "1")
                    {
                        dtadd["AccessRight"] = Convert.ToString("0");
                        dtadd["Edit_Right"] = Convert.ToBoolean(0);
                        dtadd["Delete_Right"] = Convert.ToBoolean(0);
                        dtadd["Download_Right"] = Convert.ToBoolean(0);
                        dtadd["Insert_Right"] = Convert.ToBoolean(0);
                        dtadd["Update_Right"] = Convert.ToBoolean(0);
                        dtadd["View_Right"] = Convert.ToBoolean(0);
                        dtadd["Go_Right"] = Convert.ToBoolean(0);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                    }
                    dtTemp.Rows.Add(dtadd);
                }
            }
        GV_PageAccess.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;
        hdnsortExp.Value = "MainMenuName,Pagename";
        hdnsortDir.Value = "Asc";
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        ViewState["dtp"] = dtTemp;
        GV_PageAccess.DataBind();
    }
    protected void fillPageSub()
    {
        DataTable dtTemp = CreatedataPage();
        string strmanuid = "";
        string filmanu = "";
        if (ddlmailpagefilter.SelectedIndex > 0)
        {
            filmanu = " and PageMaster.MainMenuId='" + ddlmailpagefilter.SelectedValue + "'";
        }
        string subd = "";
        foreach (GridViewRow item in GV_SubMenu.Rows)
        {
            RadioButtonList RadioButtonList1 = (RadioButtonList)item.FindControl("RadioButtonList1");
            Label lblsubmanuid = (Label)item.FindControl("lblsubmanuid");
            Label lblmainmanu = (Label)item.FindControl("lblpid");
            int flg = 0;
            CheckBox CheckEdit_Allow = (CheckBox)item.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)item.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)item.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)item.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)item.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)item.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)item.FindControl("CheckBoxGo1");
            CheckBox CheckSendMail_Allow = (CheckBox)item.FindControl("CheckBoxSendMail1");
            if (filmanu.Length != 0)
            {
                if (ddlsubpagefilter.SelectedIndex == 0)
                {
                    if (lblmainmanu.Text.ToString() == ddlmailpagefilter.SelectedValue.ToString())
                    {
                        flg = 1;
                    }
                }
                if (ddlsubpagefilter.SelectedIndex > 0)
                {
                    if (lblsubmanuid.Text.ToString() == ddlsubpagefilter.SelectedValue.ToString())
                    {
                        subd = "and SubMenuMaster.SubMenuId='" + ddlsubpagefilter.SelectedValue + "'";
                        flg = 1;
                    }
                }
            }
            else
            {
                flg = 1;
            }
            if (flg == 1)
            {
                if (RadioButtonList1.SelectedValue == "1" || RadioButtonList1.SelectedValue == "2")
                {
                    DataTable dts = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.SubMenuMaster.SubMenuName, dbo.MainMenuMaster.MainMenuName + ':' + dbo.SubMenuMaster.SubMenuName AS MainMenuName, dbo.SubMenuMaster.SubMenuId, dbo.MainMenuMaster.MainMenuId, dbo.PageMaster.PageName, dbo.PageMaster.PageId, dbo.PageMaster.PageTitle FROM dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId LEFT OUTER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId where WebsiteSection.WebsiteMasterId='"+DDLWebsiteC.SelectedValue+"' and  PageMaster.SubMenuId='" + lblsubmanuid.Text + "'  order by PageMaster.PageName ");
                    if (dts.Rows.Count > 0)
                    {
                        for (int i = 0; i < dts.Rows.Count; i++)
                        {
                            DataRow dtadd = dtTemp.NewRow();
                            dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                            dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                            dtadd["SubMenuId"] = Convert.ToString(dts.Rows[i]["SubMenuId"]);
                            dtadd["SubMenuName"] = Convert.ToString(dts.Rows[i]["SubMenuName"]);
                            dtadd["PageId"] = Convert.ToString(dts.Rows[i]["PageId"]);
                            dtadd["PageName"] = Convert.ToString(dts.Rows[i]["PageName"]);
                            dtadd["PageTitle"] = Convert.ToString(dts.Rows[i]["PageTitle"]);

                            dtadd["AccessRight"] = RadioButtonList1.SelectedValue;
                            dtadd["Edit_Right"] = CheckEdit_Allow.Checked;
                            dtadd["Delete_Right"] = CheckDelete_Allow.Checked;
                            dtadd["Download_Right"] = CheckDownload_Allow.Checked;
                            dtadd["Insert_Right"] = CheckInsert_Allows.Checked;
                            dtadd["Update_Right"] = CheckUpdate_Allow.Checked;
                            dtadd["View_Right"] = CheckView_Allow.Checked;
                            dtadd["Go_Right"] = CheckGo_Allow.Checked;
                            dtadd["SendMail_Right"] = CheckSendMail_Allow.Checked;
                            dtTemp.Rows.Add(dtadd);
                        }
                    }
                }
                else
                {
                    if (strmanuid.Length != 0)
                    {
                        strmanuid = strmanuid + ",";
                    }
                    strmanuid = strmanuid + "'" + lblsubmanuid.Text + "'";
                }
            }
        }
        if (strmanuid.Length > 0)
        {
            string prid = "";
            if (ddlrolemode.SelectedIndex > 0)
            {
                if (Rbtn_CopyAccess.SelectedValue == "1")
                {
                    prid = " and DefaultProductPageRoleWiseAccess.RoleId=(SELECT Top(1) dbo.DefaultRole.RoleId as RoleId from DefaultRole where RoleId='" + DDLCopyFromDesignetion.SelectedValue + "')";
                }
                else
                {
                    prid = " and DefaultProductPageRoleWiseAccess.RoleId=(SELECT Top(1) dbo.DefaultRole.RoleId as RoleId from DefaultRole where RoleId='" + ddlrolemode.SelectedValue + "')";
                }
            }


            DataTable dts = MyCommonfile.selectBZ(@" SELECT DISTINCT dbo.SubMenuMaster.SubMenuName, dbo.MainMenuMaster.MainMenuName + ':' + dbo.SubMenuMaster.SubMenuName AS MainMenuName, dbo.SubMenuMaster.SubMenuId, dbo.MainMenuMaster.MainMenuId, dbo.PageMaster.PageName, dbo.PageMaster.PageId, dbo.PageMaster.PageTitle, CASE WHEN (DefaultProductPageRoleWiseAccess.AccessRight IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.AccessRight END AS AccessRight, CASE WHEN (DefaultProductPageRoleWiseAccess.Edit_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Edit_Right END AS Edit_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.Delete_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Delete_Right END AS Delete_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.Download_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Download_Right END AS Download_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.Insert_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Insert_Right END AS Insert_Right,  CASE WHEN (DefaultProductPageRoleWiseAccess.Update_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Update_Right END AS Update_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.View_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.View_Right END AS View_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.Go_Right IS NULL) THEN '0' ELSE dbo.DefaultProductPageRoleWiseAccess.Go_Right END AS Go_Right,  CASE WHEN (DefaultProductPageRoleWiseAccess.SendMail_Right IS NULL) THEN '0' ELSE dbo.DefaultProductPageRoleWiseAccess.SendMail_Right END AS SendMail_Right
                         FROM  dbo.MainMenuMaster INNER JOIN   dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId LEFT OUTER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId "+prid+""+
                           " where WebsiteSection.WebsiteMasterId='"+DDLWebsiteC.SelectedValue+"' and PageMaster.SubMenuId in(" + strmanuid + ")  order by PageMaster.PageName");


            if (dts.Rows.Count > 0)
            {
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    DataRow dtadd = dtTemp.NewRow();
                    dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                    dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                    dtadd["SubMenuId"] = Convert.ToString(dts.Rows[i]["SubMenuId"]);
                    dtadd["SubMenuName"] = Convert.ToString(dts.Rows[i]["SubMenuName"]);
                    dtadd["PageId"] = Convert.ToString(dts.Rows[i]["PageId"]);
                    dtadd["PageName"] = Convert.ToString(dts.Rows[i]["PageName"]);
                    dtadd["PageTitle"] = Convert.ToString(dts.Rows[i]["PageTitle"]);
                    if (rdmode.SelectedValue == "2")
                    {
                        dtadd["AccessRight"] = Convert.ToString(dts.Rows[i]["AccessRight"]);
                        dtadd["Edit_Right"] = Convert.ToBoolean(dts.Rows[i]["Edit_Right"]);
                        dtadd["Delete_Right"] = Convert.ToBoolean(dts.Rows[i]["Delete_Right"]);
                        dtadd["Download_Right"] = Convert.ToBoolean(dts.Rows[i]["Download_Right"]);
                        dtadd["Insert_Right"] = Convert.ToBoolean(dts.Rows[i]["Insert_Right"]);
                        dtadd["Update_Right"] = Convert.ToBoolean(dts.Rows[i]["Update_Right"]);
                        dtadd["View_Right"] = Convert.ToBoolean(dts.Rows[i]["View_Right"]);
                        dtadd["Go_Right"] = Convert.ToBoolean(dts.Rows[i]["Go_Right"]);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(dts.Rows[i]["SendMail_Right"]);
                    }
                    else if (rdmode.SelectedValue == "1")
                    {
                        dtadd["AccessRight"] = Convert.ToString("0");
                        dtadd["Edit_Right"] = Convert.ToBoolean(0);
                        dtadd["Delete_Right"] = Convert.ToBoolean(0);
                        dtadd["Download_Right"] = Convert.ToBoolean(0);
                        dtadd["Insert_Right"] = Convert.ToBoolean(0);
                        dtadd["Update_Right"] = Convert.ToBoolean(0);
                        dtadd["View_Right"] = Convert.ToBoolean(0);
                        dtadd["Go_Right"] = Convert.ToBoolean(0);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                    }

                    dtTemp.Rows.Add(dtadd);
                }

            }
        }
        GV_PageAccess.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;
        hdnsortExp.Value = "MainMenuName,Pagename";
        hdnsortDir.Value = "Asc";
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        ViewState["dtp"] = dtTemp;
        GV_PageAccess.DataBind();
    }


    protected void FillPagesGrid()
    {
        DataTable dtTemp = CreatedataPage();
        string strmanuid = "";
        string filmanu = "";
        if (ddlmailpagefilter.SelectedIndex > 0)
        {
            filmanu = " and PageMaster.MainMenuId='" + ddlmailpagefilter.SelectedValue + "'";
        }       
        DataTable dts = MyCommonfile.selectBZ(@" SELECT DISTINCT dbo.SubMenuMaster.SubMenuName, dbo.MainMenuMaster.MainMenuName + ':' + dbo.SubMenuMaster.SubMenuName AS MainMenuName, dbo.SubMenuMaster.SubMenuId, dbo.MainMenuMaster.MainMenuId, dbo.PageMaster.PageName, dbo.PageMaster.PageId, dbo.PageMaster.PageTitle, CASE WHEN (DefaultProductPageRoleWiseAccess.AccessRight IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.AccessRight END AS AccessRight, CASE WHEN (DefaultProductPageRoleWiseAccess.Edit_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Edit_Right END AS Edit_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.Delete_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Delete_Right END AS Delete_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.Download_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Download_Right END AS Download_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.Insert_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Insert_Right END AS Insert_Right,  CASE WHEN (DefaultProductPageRoleWiseAccess.Update_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.Update_Right END AS Update_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.View_Right IS NULL) THEN '0' ELSE DefaultProductPageRoleWiseAccess.View_Right END AS View_Right, CASE WHEN (DefaultProductPageRoleWiseAccess.Go_Right IS NULL) THEN '0' ELSE dbo.DefaultProductPageRoleWiseAccess.Go_Right END AS Go_Right,  CASE WHEN (DefaultProductPageRoleWiseAccess.SendMail_Right IS NULL) THEN '0' ELSE dbo.DefaultProductPageRoleWiseAccess.SendMail_Right END AS SendMail_Right FROM dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId LEFT OUTER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId where  PageMaster.SubMenuId in(" + strmanuid + ")  order by PageMaster.PageName");
        GV_PageAccess.DataSource = dts;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;
        hdnsortExp.Value = "MainMenuName,Pagename";
        hdnsortDir.Value = "Asc";
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        ViewState["dtp"] = dtTemp;
        GV_PageAccess.DataBind();
    }
    protected void fillaccessgridA()
    {
        foreach (GridViewRow dsc in grdmain.Rows)
        {
            RadioButtonList rdlist;
            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");
            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            if (rdlist.SelectedValue == "0")
            {
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Enabled = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Enabled = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {
                Checksendmail_Allow.Enabled = true;
                CheckEdit_Allow.Enabled = true;
                CheckDelete_Allow.Enabled = true;
                CheckDownload_Allow.Enabled = true;
                CheckInsert_Allows.Enabled = true;
                CheckUpdate_Allow.Enabled = true;
                CheckView_Allow.Enabled = true;
                CheckSendMail_Allow.Enabled = true;
                CheckGo_Allow.Enabled = true;
            }
        }
    }
  
    protected void fillaccessgrid2A()
    {
        foreach (GridViewRow dsc in GV_PageAccess.Rows)
        {
            RadioButtonList rdlist;
            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");

            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");

            if (rdlist.SelectedValue == "0")
            {

                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Enabled = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Enabled = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {
                Checksendmail_Allow.Enabled = true;
                CheckEdit_Allow.Enabled = true;
                CheckDelete_Allow.Enabled = true;
                CheckDownload_Allow.Enabled = true;
                CheckInsert_Allows.Enabled = true;
                CheckUpdate_Allow.Enabled = true;
                CheckView_Allow.Enabled = true;
                CheckSendMail_Allow.Enabled = true;
                CheckGo_Allow.Enabled = true;
            }
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    { 
            string roleid;// = Dataavail.DataKeys[item.ItemIndex].ToString();
            roleid = ddlrolemode.SelectedValue;                                      
                if (pnlmain.Visible == true)
                {
                    foreach (GridViewRow grd in grdmain.Rows)
                    {
                        #region                       
                        Label lblmainmanu = (Label)grd.FindControl("lblmainmanu");
                        RadioButtonList rd1 = (RadioButtonList)grd.FindControl("RadioButtonList1");
                        CheckBox Checksendmail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");
                        CheckBox CheckEdit_Allow = (CheckBox)grd.FindControl("CheckBoxEdit1");
                        CheckBox CheckDelete_Allow = (CheckBox)grd.FindControl("CheckBoxDelete1");
                        CheckBox CheckDownload_Allow = (CheckBox)grd.FindControl("CheckBoxDownload1");
                        CheckBox CheckInsert_Allows = (CheckBox)grd.FindControl("CheckBoxInsert1");
                        CheckBox CheckUpdate_Allow = (CheckBox)grd.FindControl("CheckBoxUpdate1");
                        CheckBox CheckView_Allow = (CheckBox)grd.FindControl("CheckBoxView1");
                        CheckBox CheckGo_Allow = (CheckBox)grd.FindControl("CheckBoxGo1");
                        CheckBox CheckSendMail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");                      
                        #endregion                       
                        #region 
                        DataTable dts = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.MainMenuMaster.MainMenuId, dbo.PageMaster.PageId, dbo.PageMaster.VersionInfoMasterId FROM  dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId where  dbo.PageMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' and PageMaster.MainMenuId='" + lblmainmanu.Text + "'");
                                            for (int k1 = 0; k1 < dts.Rows.Count; k1++)
                                            {
                                            if (rd1.SelectedValue == "1" || rd1.SelectedValue == "2")
                                            {
                                                DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultProductPageRoleWiseAccess where PageId='" + dts.Rows[k1]["PageId"] + "'  and RoleId='" + roleid + "' ");
                                                if (Dts.Rows.Count == 0)
                                                {
                                                    string SelectQur = " INSERT INTO DefaultProductPageRoleWiseAccess(PageId,RoleId,AccessRight,Edit_Right,Delete_Right,Download_Right,Insert_Right,Update_Right,View_Right,Go_Right,SendMail_Right) Values ('" + dts.Rows[k1]["PageId"] + "','" + roleid + "','" + rd1.SelectedValue + "','" + CheckEdit_Allow.Checked + "','" + CheckDelete_Allow.Checked + "','" + CheckDownload_Allow.Checked + "','" + CheckInsert_Allows.Checked + "','" + CheckUpdate_Allow.Checked + "','" + CheckView_Allow.Checked + "','" + CheckGo_Allow.Checked + "','" + CheckSendMail_Allow.Checked + "')";
                                                    SqlCommand cd4 = new SqlCommand(SelectQur, con);
                                                    con.Open();
                                                    cd4.ExecuteNonQuery();
                                                    con.Close();
                                                }                                              
                                            }
                                            if (rd1.SelectedValue == "0")
                                            {
                                                DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultProductPageRoleWiseAccess where PageId='" + dts.Rows[k1]["PageId"] + "'  and RoleId='" + roleid + "' ");
                                                if (Dts.Rows.Count > 0)
                                                {
                                                    string delete = "delete from DefaultProductPageRoleWiseAccess where PageId='" + dts.Rows[k1]["PageId"] + "' and RoleId='" + roleid + "' ";
                                                    SqlCommand ccmm = new SqlCommand(delete, con);
                                                    con.Open();
                                                    ccmm.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                            }
                                            }                                                  
                            #endregion 
                    }                    
                }
                else if (pnlsubmanu.Visible == true)
                {                    
                    foreach (GridViewRow grd in GV_SubMenu.Rows)
                    {
                        #region
                        Label lblsubmanuid = (Label)grd.FindControl("lblsubmanuid");
                        RadioButtonList rd1 = (RadioButtonList)grd.FindControl("RadioButtonList1");
                        CheckBox Checksendmail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");
                        CheckBox CheckEdit_Allow = (CheckBox)grd.FindControl("CheckBoxEdit1");
                        CheckBox CheckDelete_Allow = (CheckBox)grd.FindControl("CheckBoxDelete1");
                        CheckBox CheckDownload_Allow = (CheckBox)grd.FindControl("CheckBoxDownload1");
                        CheckBox CheckInsert_Allows = (CheckBox)grd.FindControl("CheckBoxInsert1");
                        CheckBox CheckUpdate_Allow = (CheckBox)grd.FindControl("CheckBoxUpdate1");
                        CheckBox CheckView_Allow = (CheckBox)grd.FindControl("CheckBoxView1");
                        CheckBox CheckGo_Allow = (CheckBox)grd.FindControl("CheckBoxGo1");
                        CheckBox CheckSendMail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");
                        #endregion
                        #region                        
                            DataTable dts = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.SubMenuMaster.SubMenuId, dbo.MainMenuMaster.MainMenuId, dbo.PageMaster.PageId FROM dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId where  PageMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' and PageMaster.SubMenuId='" + lblsubmanuid.Text + "'");
                            for (int k1 = 0; k1 < dts.Rows.Count; k1++)
                            {
                                if (rd1.SelectedValue == "1" || rd1.SelectedValue == "2")
                                {
                                    DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultProductPageRoleWiseAccess where PageId='" + dts.Rows[k1]["PageId"] + "' and RoleId='" + roleid + "' ");
                                    if (Dts.Rows.Count == 0)
                                    {
                                        string SelectQur = " INSERT INTO DefaultProductPageRoleWiseAccess(PageId,RoleId,AccessRight,Edit_Right,Delete_Right,Download_Right,Insert_Right,Update_Right,View_Right,Go_Right,SendMail_Right) Values ('" + dts.Rows[k1]["PageId"] + "','" + roleid + "','" + rd1.SelectedValue + "','" + CheckEdit_Allow.Checked + "','" + CheckDelete_Allow.Checked + "','" + CheckDownload_Allow.Checked + "','" + CheckInsert_Allows.Checked + "','" + CheckUpdate_Allow.Checked + "','" + CheckView_Allow.Checked + "','" + CheckGo_Allow.Checked + "','" + CheckSendMail_Allow.Checked + "')";
                                        SqlCommand cd4 = new SqlCommand(SelectQur, con);
                                        con.Open();
                                        cd4.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                                if (rd1.SelectedValue == "0")
                                {
                                    DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultProductPageRoleWiseAccess where PageId='" + dts.Rows[k1]["PageId"] + "'   and RoleId='" + roleid + "' ");
                                    if (Dts.Rows.Count > 0)
                                    {
                                        string delete = "delete from DefaultProductPageRoleWiseAccess where PageId='" + dts.Rows[k1]["PageId"] + "'  and RoleId='" + roleid + "' ";
                                        SqlCommand ccmm = new SqlCommand(delete, con);
                                        con.Open();
                                        ccmm.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }                        
                        #endregion                        
                    }                                
                }
                else if (pnlpage.Visible == true)
                {
                    foreach (GridViewRow grd in GV_PageAccess.Rows)
                    {
                        Label lblpageid = (Label)grd.FindControl("lblpageid");
                        RadioButtonList rd1 = (RadioButtonList)grd.FindControl("RadioButtonList1");
                        CheckBox Checksendmail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");
                        CheckBox CheckEdit_Allow = (CheckBox)grd.FindControl("CheckBoxEdit1");
                        CheckBox CheckDelete_Allow = (CheckBox)grd.FindControl("CheckBoxDelete1");
                        CheckBox CheckDownload_Allow = (CheckBox)grd.FindControl("CheckBoxDownload1");
                        CheckBox CheckInsert_Allows = (CheckBox)grd.FindControl("CheckBoxInsert1");
                        CheckBox CheckUpdate_Allow = (CheckBox)grd.FindControl("CheckBoxUpdate1");
                        CheckBox CheckView_Allow = (CheckBox)grd.FindControl("CheckBoxView1");
                        CheckBox CheckGo_Allow = (CheckBox)grd.FindControl("CheckBoxGo1");
                        CheckBox CheckSendMail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");
                        #region 
                            if (rd1.SelectedValue == "1" || rd1.SelectedValue == "2")
                            {
                                DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultProductPageRoleWiseAccess where PageId='" + lblpageid.Text + "'  and RoleId='" + roleid + "' ");
                                if (Dts.Rows.Count == 0)
                                {
                                    string SelectQur = " INSERT INTO DefaultProductPageRoleWiseAccess(PageId,RoleId,AccessRight,Edit_Right,Delete_Right,Download_Right,Insert_Right,Update_Right,View_Right,Go_Right,SendMail_Right) Values ('" + lblpageid.Text + "','" + roleid + "','" + rd1.SelectedValue + "','" + CheckEdit_Allow.Checked + "','" + CheckDelete_Allow.Checked + "','" + CheckDownload_Allow.Checked + "','" + CheckInsert_Allows.Checked + "','" + CheckUpdate_Allow.Checked + "','" + CheckView_Allow.Checked + "','" + CheckGo_Allow.Checked + "','" + CheckSendMail_Allow.Checked + "')";
                                    SqlCommand cd4 = new SqlCommand(SelectQur, con);
                                    con.Open();
                                    cd4.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                            if (rd1.SelectedValue == "0")
                            {
                                DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultProductPageRoleWiseAccess where PageId='" + lblpageid.Text + "'  and RoleId='" + roleid + "' ");
                                if (Dts.Rows.Count > 0)
                                {
                                    string delete = "delete from DefaultProductPageRoleWiseAccess where PageId='" + lblpageid.Text + "' and RoleId='" + roleid + "' ";
                                    SqlCommand ccmm = new SqlCommand(delete, con);
                                    con.Open();
                                    ccmm.ExecuteNonQuery();
                                    con.Close();
                                }
                            }                        
                        #endregion                        
                    }                            
                }                   
        Label1.Text = "Record inserted successfully";
        Label1.Visible = true;
        FillRoleDDL();
        ddlrolemode_SelectedIndexChanged(sender,e);
        ShowGrid();
    }
   
  
   
    protected void grdmain_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        DataTable dtTemp = (DataTable)ViewState["Maing"];
        grdmain.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        grdmain.DataBind();
    }
   
    //GB Common
    protected void rlheader_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList chk;
        if (pnlmain.Visible == true)
        {
            foreach (GridViewRow rowitem in grdmain.Rows)
            {
                chk = (RadioButtonList)(rowitem.FindControl("RadioButtonList1"));
                chk.SelectedValue = ((RadioButtonList)sender).SelectedValue;
                fillaccessgrid();

            }
        }
        if (pnlsubmanu.Visible == true)
        {
            foreach (GridViewRow rowitem in GV_SubMenu.Rows)
            {
                chk = (RadioButtonList)(rowitem.FindControl("RadioButtonList1"));
                chk.SelectedValue = ((RadioButtonList)sender).SelectedValue;
                FillAccessCHK_GV_SubMenu();
            }
        }
        if (pnlpage.Visible == true)
        {
            foreach (GridViewRow rowitem in GV_PageAccess.Rows)
            {
                chk = (RadioButtonList)(rowitem.FindControl("RadioButtonList1"));
                chk.SelectedValue = ((RadioButtonList)sender).SelectedValue;
                FillAccessCHK_GV_PageAccess();
            }
        }
    }
    protected void chkedit_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxEdit1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in GV_SubMenu.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxEdit1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in GV_PageAccess.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxEdit1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void chkdelete_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDelete1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in GV_SubMenu.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDelete1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in GV_PageAccess.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDelete1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void chkDownload_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDownload1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in GV_SubMenu.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDownload1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in GV_PageAccess.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDownload1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void chkInsert_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxInsert1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in GV_SubMenu.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxInsert1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in GV_PageAccess.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxInsert1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }


    }
    protected void chkUpdate_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxUpdate1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in GV_SubMenu.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxUpdate1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in GV_PageAccess.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxUpdate1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkView_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxView1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in GV_SubMenu.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxView1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in GV_PageAccess.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxView1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkGo_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxGo1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in GV_SubMenu.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxView1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in GV_PageAccess.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxView1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkSendMail_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxSendMail1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in GV_SubMenu.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxSendMail1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in GV_PageAccess.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxSendMail1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }

    }
  

    //GV_MainMenu
    //GV_SubMenu   
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((RadioButtonList)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");
        RadioButtonList rdlist;
        rdlist = (RadioButtonList)(GV_SubMenu.Rows[rinrow].FindControl("RadioButtonList1"));
        CheckBox Checksendmail_Allow = (CheckBox)GV_SubMenu.Rows[rinrow].FindControl("CheckBoxSendMail1");
        CheckBox CheckEdit_Allow = (CheckBox)GV_SubMenu.Rows[rinrow].FindControl("CheckBoxEdit1");
        CheckBox CheckDelete_Allow = (CheckBox)GV_SubMenu.Rows[rinrow].FindControl("CheckBoxDelete1");
        CheckBox CheckDownload_Allow = (CheckBox)GV_SubMenu.Rows[rinrow].FindControl("CheckBoxDownload1");
        CheckBox CheckInsert_Allows = (CheckBox)GV_SubMenu.Rows[rinrow].FindControl("CheckBoxInsert1");
        CheckBox CheckUpdate_Allow = (CheckBox)GV_SubMenu.Rows[rinrow].FindControl("CheckBoxUpdate1");
        CheckBox CheckView_Allow = (CheckBox)GV_SubMenu.Rows[rinrow].FindControl("CheckBoxView1");
        CheckBox CheckGo_Allow = (CheckBox)GV_SubMenu.Rows[rinrow].FindControl("CheckBoxGo1");
        CheckBox CheckSendMail_Allow = (CheckBox)GV_SubMenu.Rows[rinrow].FindControl("CheckBoxSendMail1");
        if (rdlist.SelectedValue == "0")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "1")
        {
            Checksendmail_Allow.Checked = true;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = true;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = true;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = true;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = true;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = true;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = true;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = true;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = true;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "2")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = true;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = true;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = true;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = true;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = true;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = true;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = true;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = true;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = true;
        }
    }
    protected void FillAccessCHK_GV_SubMenuEnable()
    {
        foreach (GridViewRow dsc in GV_SubMenu.Rows)
        {
            RadioButtonList rdlist;
            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");
            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            if (rdlist.SelectedValue == "0")
            {
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Enabled = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Enabled = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {
                Checksendmail_Allow.Enabled = true;
                CheckEdit_Allow.Enabled = true;
                CheckDelete_Allow.Enabled = true;
                CheckDownload_Allow.Enabled = true;
                CheckInsert_Allows.Enabled = true;
                CheckUpdate_Allow.Enabled = true;
                CheckView_Allow.Enabled = true;
                CheckSendMail_Allow.Enabled = true;
                CheckGo_Allow.Enabled = true;
            }
        }
    }
    protected void FillAccessCHK_GV_SubMenu()
    {
        foreach (GridViewRow dsc in GV_SubMenu.Rows)
        {
            RadioButtonList rdlist;
            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");
            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            if (rdlist.SelectedValue == "0")
            {
                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {
                Checksendmail_Allow.Checked = true;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = true;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = true;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = true;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = true;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = true;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = true;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = true;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = true;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {
                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = true;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = true;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = true;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = true;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = true;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = true;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = true;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = true;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = true;
            }
        }
    }
    protected void GV_SubMenu_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        DataTable dtTemp = (DataTable)ViewState["Subg"];
        GV_SubMenu.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GV_SubMenu.DataBind();
    }


    //GV_PageAccess  
    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((RadioButtonList)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");
        RadioButtonList rdlist;
        rdlist = (RadioButtonList)(GV_PageAccess.Rows[rinrow].FindControl("RadioButtonList1"));
        CheckBox Checksendmail_Allow = (CheckBox)GV_PageAccess.Rows[rinrow].FindControl("CheckBoxSendMail1");
        CheckBox CheckEdit_Allow = (CheckBox)GV_PageAccess.Rows[rinrow].FindControl("CheckBoxEdit1");
        CheckBox CheckDelete_Allow = (CheckBox)GV_PageAccess.Rows[rinrow].FindControl("CheckBoxDelete1");
        CheckBox CheckDownload_Allow = (CheckBox)GV_PageAccess.Rows[rinrow].FindControl("CheckBoxDownload1");
        CheckBox CheckInsert_Allows = (CheckBox)GV_PageAccess.Rows[rinrow].FindControl("CheckBoxInsert1");
        CheckBox CheckUpdate_Allow = (CheckBox)GV_PageAccess.Rows[rinrow].FindControl("CheckBoxUpdate1");
        CheckBox CheckView_Allow = (CheckBox)GV_PageAccess.Rows[rinrow].FindControl("CheckBoxView1");
        CheckBox CheckGo_Allow = (CheckBox)GV_PageAccess.Rows[rinrow].FindControl("CheckBoxGo1");
        CheckBox CheckSendMail_Allow = (CheckBox)GV_PageAccess.Rows[rinrow].FindControl("CheckBoxSendMail1");
        if (rdlist.SelectedValue == "0")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "1")
        {
            Checksendmail_Allow.Checked = true;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = true;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = true;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = true;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = true;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = true;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = true;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = true;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = true;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "2")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = true;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = true;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = true;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = true;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = true;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = true;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = true;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = true;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = true;
        }
    }
    protected void GV_PageAccess_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        DataTable dtTemp = (DataTable)ViewState["dtp"];
        GV_PageAccess.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GV_PageAccess.DataBind();
    }   
    protected void FillAccessCHK_GV_PageAccess()
    {
        foreach (GridViewRow dsc in GV_PageAccess.Rows)
        {
            RadioButtonList rdlist;
            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");
            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            if (rdlist.SelectedValue == "0")
            {
                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {
                Checksendmail_Allow.Checked = true;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = true;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = true;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = true;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = true;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = true;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = true;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = true;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = true;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {
                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = true;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = true;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = true;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = true;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = true;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = true;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = true;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = true;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = true;
            }
        }
    }
   
  
















    public DataTable CreatedataManu()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "MainMenuName";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "MainMenuId";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "AccessRight";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);

        /////1 
        DataColumn prd1f = new DataColumn();
        prd1f.ColumnName = "Edit_Right";
        prd1f.DataType = System.Type.GetType("System.Boolean");
        prd1f.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f);

        DataColumn ptrc = new DataColumn();
        ptrc.ColumnName = "Delete_Right";
        ptrc.DataType = System.Type.GetType("System.Boolean");
        ptrc.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc);

        DataColumn prd1c = new DataColumn();
        prd1c.ColumnName = "Download_Right";
        prd1c.DataType = System.Type.GetType("System.Boolean");
        prd1c.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c);

        /////2
        DataColumn prd1f2 = new DataColumn();
        prd1f2.ColumnName = "Insert_Right";
        prd1f2.DataType = System.Type.GetType("System.Boolean");
        prd1f2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f2);

        DataColumn ptrc2 = new DataColumn();
        ptrc2.ColumnName = "Update_Right";
        ptrc2.DataType = System.Type.GetType("System.Boolean");
        ptrc2.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc2);

        DataColumn prd1c2 = new DataColumn();
        prd1c2.ColumnName = "View_Right";
        prd1c2.DataType = System.Type.GetType("System.Boolean");
        prd1c2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c2);

        /////3
        DataColumn prd1f3 = new DataColumn();
        prd1f3.ColumnName = "Go_Right";
        prd1f3.DataType = System.Type.GetType("System.Boolean");
        prd1f3.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f3);

        DataColumn ptrc3 = new DataColumn();
        ptrc3.ColumnName = "SendMail_Right";
        ptrc3.DataType = System.Type.GetType("System.Boolean");
        ptrc3.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc3);


        return dtTemp;
    }
    public DataTable CreatedataSubManu()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "MainMenuName";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "MainMenuId";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd1x = new DataColumn();
        prd1x.ColumnName = "SubMenuName";
        prd1x.DataType = System.Type.GetType("System.String");
        prd1x.AllowDBNull = true;
        dtTemp.Columns.Add(prd1x);

        DataColumn prd11z = new DataColumn();
        prd11z.ColumnName = "SubMenuId";
        prd11z.DataType = System.Type.GetType("System.String");
        prd11z.AllowDBNull = true;
        dtTemp.Columns.Add(prd11z);


        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "AccessRight";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);
        /////1 
        DataColumn prd1f = new DataColumn();
        prd1f.ColumnName = "Edit_Right";
        prd1f.DataType = System.Type.GetType("System.Boolean");
        prd1f.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f);

        DataColumn ptrc = new DataColumn();
        ptrc.ColumnName = "Delete_Right";
        ptrc.DataType = System.Type.GetType("System.Boolean");
        ptrc.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc);

        DataColumn prd1c = new DataColumn();
        prd1c.ColumnName = "Download_Right";
        prd1c.DataType = System.Type.GetType("System.Boolean");
        prd1c.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c);

        /////2
        DataColumn prd1f2 = new DataColumn();
        prd1f2.ColumnName = "Insert_Right";
        prd1f2.DataType = System.Type.GetType("System.Boolean");
        prd1f2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f2);

        DataColumn ptrc2 = new DataColumn();
        ptrc2.ColumnName = "Update_Right";
        ptrc2.DataType = System.Type.GetType("System.Boolean");
        ptrc2.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc2);

        DataColumn prd1c2 = new DataColumn();
        prd1c2.ColumnName = "View_Right";
        prd1c2.DataType = System.Type.GetType("System.Boolean");
        prd1c2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c2);

        /////3
        DataColumn prd1f3 = new DataColumn();
        prd1f3.ColumnName = "Go_Right";
        prd1f3.DataType = System.Type.GetType("System.Boolean");
        prd1f3.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f3);

        DataColumn ptrc3 = new DataColumn();
        ptrc3.ColumnName = "SendMail_Right";
        ptrc3.DataType = System.Type.GetType("System.Boolean");
        ptrc3.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc3);
        return dtTemp;
    }
    public DataTable CreatedataPage()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "MainMenuName";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "MainMenuId";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd1x = new DataColumn();
        prd1x.ColumnName = "SubMenuName";
        prd1x.DataType = System.Type.GetType("System.String");
        prd1x.AllowDBNull = true;
        dtTemp.Columns.Add(prd1x);

        DataColumn prd11z = new DataColumn();
        prd11z.ColumnName = "SubMenuId";
        prd11z.DataType = System.Type.GetType("System.String");
        prd11z.AllowDBNull = true;
        dtTemp.Columns.Add(prd11z);


        DataColumn prd1xo = new DataColumn();
        prd1xo.ColumnName = "PageName";
        prd1xo.DataType = System.Type.GetType("System.String");
        prd1xo.AllowDBNull = true;
        dtTemp.Columns.Add(prd1xo);

        DataColumn prd11zp = new DataColumn();
        prd11zp.ColumnName = "PageId";
        prd11zp.DataType = System.Type.GetType("System.String");
        prd11zp.AllowDBNull = true;
        dtTemp.Columns.Add(prd11zp);

        DataColumn prd11ti = new DataColumn();
        prd11ti.ColumnName = "PageTitle";
        prd11ti.DataType = System.Type.GetType("System.String");
        prd11ti.AllowDBNull = true;
        dtTemp.Columns.Add(prd11ti);


        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "AccessRight";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);
        /////1 
        DataColumn prd1f = new DataColumn();
        prd1f.ColumnName = "Edit_Right";
        prd1f.DataType = System.Type.GetType("System.Boolean");
        prd1f.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f);

        DataColumn ptrc = new DataColumn();
        ptrc.ColumnName = "Delete_Right";
        ptrc.DataType = System.Type.GetType("System.Boolean");
        ptrc.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc);

        DataColumn prd1c = new DataColumn();
        prd1c.ColumnName = "Download_Right";
        prd1c.DataType = System.Type.GetType("System.Boolean");
        prd1c.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c);

        /////2
        DataColumn prd1f2 = new DataColumn();
        prd1f2.ColumnName = "Insert_Right";
        prd1f2.DataType = System.Type.GetType("System.Boolean");
        prd1f2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f2);

        DataColumn ptrc2 = new DataColumn();
        ptrc2.ColumnName = "Update_Right";
        ptrc2.DataType = System.Type.GetType("System.Boolean");
        ptrc2.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc2);

        DataColumn prd1c2 = new DataColumn();
        prd1c2.ColumnName = "View_Right";
        prd1c2.DataType = System.Type.GetType("System.Boolean");
        prd1c2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c2);

        /////3
        DataColumn prd1f3 = new DataColumn();
        prd1f3.ColumnName = "Go_Right";
        prd1f3.DataType = System.Type.GetType("System.Boolean");
        prd1f3.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f3);

        DataColumn ptrc3 = new DataColumn();
        ptrc3.ColumnName = "SendMail_Right";
        ptrc3.DataType = System.Type.GetType("System.Boolean");
        ptrc3.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc3);


        return dtTemp;
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
    //protected void chkrole_chachedChanged(object sender, EventArgs e)
    //{
    //    CheckBox ch = (CheckBox)sender;
    //    DataListItem row = (DataListItem)ch.NamingContainer;
    //    int rinrow = row.ItemIndex;
    //    string MasterRId = Dataavail.DataKeys[rinrow].ToString();
    //    // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");
    //    if (rdmode.SelectedValue == "2")
    //    {
    //        foreach (DataListItem item in Dataavail.Items)
    //        {
    //            CheckBox chkde = (CheckBox)(Dataavail.Items[item.ItemIndex].FindControl("lbllist"));
    //            chkde.Enabled = false;
    //        }
    //    }
    //    if (pnl_PlushMinush.Visible == false)
    //    {
    //        pnl_PlushMinush.Visible = true;
    //        defaRoleid = MasterRId;
    //        FillGrid();            
    //    }
    //}




    //protected void FillMainMenuGridOLD()
    //{
    //    string par = "";
    //    DataTable dtTemp = new DataTable();
    //    DataTable dts = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.MainMenuMaster.MainMenuId, dbo.MainMenuMaster.MainMenuName FROM dbo.MainMenuMaster INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId  where  WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "' " + par + " order by MainMenuMaster.MainMenuName ");
    //    if (dts.Rows.Count > 0)
    //    {
    //        dtTemp = CreatedataManu();
    //        for (int i = 0; i < dts.Rows.Count; i++)
    //        {
    //            DataRow dtadd = dtTemp.NewRow();
    //            dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
    //            dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
    //            if (rdmode.SelectedValue == "2" || rdmode.SelectedValue == "3")
    //            {
    //                int accesno1 = 0;
    //                int accesno2 = 0;
    //                int Edit_Right = 0;
    //                int Delete_Right = 0;
    //                int Download_Right = 0;
    //                int Insert_Right = 0;
    //                int Update_Right = 0;
    //                int View_Right = 0;
    //                int Go_Right = 0;
    //                int SendMail_Right = 0;
    //                DataTable dtcln1 = MyCommonfile.selectBZ(" SELECT COUNT(DISTINCT dbo.PageMaster.PageId) AS CD FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId where PageMaster.MainMenuId='" + dts.Rows[i]["MainMenuId"] + "'");
    //                DataTable dtcln = new DataTable();
    //                dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.DefaultProductPageRoleWiseAccess.Id, dbo.DefaultProductPageRoleWiseAccess.PageId, dbo.DefaultProductPageRoleWiseAccess.RoleId,  dbo.DefaultProductPageRoleWiseAccess.Edit_Right, dbo.DefaultProductPageRoleWiseAccess.Delete_Right,  dbo.DefaultProductPageRoleWiseAccess.Download_Right, dbo.DefaultProductPageRoleWiseAccess.Insert_Right, dbo.DefaultProductPageRoleWiseAccess.Update_Right, dbo.DefaultProductPageRoleWiseAccess.View_Right, dbo.DefaultProductPageRoleWiseAccess.Go_Right, dbo.DefaultProductPageRoleWiseAccess.SendMail_Right, dbo.DefaultProductPageRoleWiseAccess.AccessRight FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId INNER JOIN dbo.DefaultProductPageRoleWiseAccess ON dbo.PageMaster.PageId = dbo.DefaultProductPageRoleWiseAccess.PageId where  PageMaster.MainMenuId='" + dts.Rows[i]["MainMenuId"] + "' and RoleId='" + defaRoleid + "'");
    //                foreach (DataRow item in dtcln.Rows)
    //                {
    //                    if (Convert.ToString(item["AccessRight"]) == "1")
    //                    {
    //                        accesno1 = accesno1 + 1;
    //                    }
    //                    else if (Convert.ToString(item["AccessRight"]) == "2")
    //                    {
    //                        accesno2 = accesno2 + 1;
    //                    }
    //                    if (Convert.ToString(item["Edit_Right"]) == "True")
    //                    {
    //                        Edit_Right = Edit_Right + 1;
    //                    }
    //                    if (Convert.ToString(item["Delete_Right"]) == "True")
    //                    {
    //                        Delete_Right = Delete_Right + 1;
    //                    }
    //                    if (Convert.ToString(item["Download_Right"]) == "True")
    //                    {
    //                        Download_Right = Download_Right + 1;
    //                    }
    //                    if (Convert.ToString(item["Insert_Right"]) == "True")
    //                    {
    //                        Insert_Right = Insert_Right + 1;
    //                    }
    //                    if (Convert.ToString(item["Update_Right"]) == "True")
    //                    {
    //                        Update_Right = Update_Right + 1;
    //                    }
    //                    if (Convert.ToString(item["View_Right"]) == "True")
    //                    {
    //                        View_Right = View_Right + 1;
    //                    }
    //                    if (Convert.ToString(item["Go_Right"]) == "True")
    //                    {
    //                        Go_Right = Go_Right + 1;
    //                    }
    //                    if (Convert.ToString(item["SendMail_Right"]) == "True")
    //                    {
    //                        SendMail_Right = SendMail_Right + 1;
    //                    }
    //                }
    //                if (accesno1 == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
    //                {
    //                    dtadd["AccessRight"] = "1";
    //                    dtadd["Edit_Right"] = Convert.ToBoolean(1);
    //                    dtadd["Delete_Right"] = Convert.ToBoolean(1);
    //                    dtadd["Download_Right"] = Convert.ToBoolean(1);
    //                    dtadd["Insert_Right"] = Convert.ToBoolean(1);
    //                    dtadd["Update_Right"] = Convert.ToBoolean(1);
    //                    dtadd["View_Right"] = Convert.ToBoolean(1);
    //                    dtadd["Go_Right"] = Convert.ToBoolean(1);
    //                    dtadd["SendMail_Right"] = Convert.ToBoolean(1);
    //                }
    //                else
    //                {
    //                    if (accesno2 == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
    //                    {
    //                        dtadd["AccessRight"] = "2";
    //                        if (Edit_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
    //                        {
    //                            dtadd["Edit_Right"] = Convert.ToBoolean(1);
    //                        }
    //                        else
    //                        {
    //                            dtadd["Edit_Right"] = Convert.ToBoolean(0);
    //                        }
    //                        if (Delete_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
    //                        {
    //                            dtadd["Delete_Right"] = Convert.ToBoolean(1);
    //                        }
    //                        else
    //                        {
    //                            dtadd["Delete_Right"] = Convert.ToBoolean(0);
    //                        }
    //                        if (Download_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
    //                        {
    //                            dtadd["Download_Right"] = Convert.ToBoolean(1);
    //                        }
    //                        else
    //                        {
    //                            dtadd["Download_Right"] = Convert.ToBoolean(0);
    //                        }
    //                        if (Insert_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
    //                        {
    //                            dtadd["Insert_Right"] = Convert.ToBoolean(1);
    //                        }
    //                        else
    //                        {
    //                            dtadd["Insert_Right"] = Convert.ToBoolean(0);
    //                        }
    //                        if (Update_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
    //                        {
    //                            dtadd["Update_Right"] = Convert.ToBoolean(1);
    //                        }
    //                        else
    //                        {
    //                            dtadd["Update_Right"] = Convert.ToBoolean(0);
    //                        }
    //                        if (View_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
    //                        {
    //                            dtadd["View_Right"] = Convert.ToBoolean(1);
    //                        }
    //                        else
    //                        {
    //                            dtadd["View_Right"] = Convert.ToBoolean(0);
    //                        }
    //                        if (Go_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
    //                        {
    //                            dtadd["Go_Right"] = Convert.ToBoolean(1);
    //                        }
    //                        else
    //                        {
    //                            dtadd["Go_Right"] = Convert.ToBoolean(0);
    //                        }
    //                        if (SendMail_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
    //                        {
    //                            dtadd["SendMail_Right"] = Convert.ToBoolean(1);
    //                        }
    //                        else
    //                        {
    //                            dtadd["SendMail_Right"] = Convert.ToBoolean(0);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        dtadd["AccessRight"] = "0";
    //                        dtadd["Edit_Right"] = Convert.ToBoolean(0);
    //                        dtadd["Delete_Right"] = Convert.ToBoolean(0);
    //                        dtadd["Download_Right"] = Convert.ToBoolean(0);
    //                        dtadd["Insert_Right"] = Convert.ToBoolean(0);
    //                        dtadd["Update_Right"] = Convert.ToBoolean(0);
    //                        dtadd["View_Right"] = Convert.ToBoolean(0);
    //                        dtadd["Go_Right"] = Convert.ToBoolean(0);
    //                        dtadd["SendMail_Right"] = Convert.ToBoolean(0);
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                dtadd["AccessRight"] = "0";
    //                dtadd["Edit_Right"] = Convert.ToBoolean(0);
    //                dtadd["Delete_Right"] = Convert.ToBoolean(0);
    //                dtadd["Download_Right"] = Convert.ToBoolean(0);
    //                dtadd["Insert_Right"] = Convert.ToBoolean(0);
    //                dtadd["Update_Right"] = Convert.ToBoolean(0);
    //                dtadd["View_Right"] = Convert.ToBoolean(0);
    //                dtadd["Go_Right"] = Convert.ToBoolean(0);
    //                dtadd["SendMail_Right"] = Convert.ToBoolean(0);
    //            }
    //            dtTemp.Rows.Add(dtadd);
    //        }
    //    }
    //    grdmain.DataSource = dtTemp;
    //    if (dtTemp.Rows.Count > 0)
    //    {
    //        DataView myDataView = new DataView();
    //        myDataView = dtTemp.DefaultView;
    //        hdnsortExp.Value = "MainMenuName";
    //        hdnsortDir.Value = "Asc";
    //        if (hdnsortExp.Value != string.Empty)
    //        {
    //            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
    //        }
    //        ViewState["Maing"] = dtTemp;
    //    }
    //    grdmain.DataBind();

    //}
}

