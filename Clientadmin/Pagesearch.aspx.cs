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
public partial class MainMenuMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
      //  ModalPopupExtender3.Show();
        if (!IsPostBack)
        {
            if (Session["pagesearch"] != null)
            {
                ViewState["sortOrder"] = "";
                TextBox5.Text = Session["pagesearch"].ToString();
                lbltext.Text =" Search as: "+ Session["pagesearch"].ToString();
                lbltext.Text = "Page Search Result"; 
                filterproduct();
                ModalPopupExtender3.Show(); 
            }
        }
    }

    protected void BtnGo_Click(object sender, EventArgs e)
    {
        try
        {
           // TextBox5.Text = TextBox5.Text;
            FilterGrid();
        }
        catch (Exception ex)
        {
        }
    }

    protected void chkupload_CheckedChanged1(object sender, EventArgs e)
    {
        
            FilterProduct_SelectedIndexChanged(sender, e);        
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["pagesearch"] = null;
            ModalPopupExtender3.Hide();    
            //Response.Redirect(Request.QueryString["page"].ToString());
        }
        catch (Exception ex)
        {
               
        }        
    }
    protected void filterproduct()
    {
        string stractive;
        if (chk_activefilter.Checked == true)
        {
            stractive = "";
        }
        string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,  ProductMaster.ProductName + ':' +   VersionInfoMaster.VersionInfoName + ' : ' + WebsiteMaster.WebsiteName + ':' +   WebsiteSection.SectionName + ':' +   MasterPageMaster.MasterPageName  as MasterPage_Name  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where VersionInfoMaster.VersionInfoId='32'   order  by MasterPage_Name";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);


        FilterProduct.DataSource = dtcln;
        FilterProduct.DataValueField = "MasterPageId";
        FilterProduct.DataTextField = "MasterPage_Name";
        FilterProduct.DataBind();

        //FilterProduct.Items.Insert(0, "All");
        //FilterProduct.Items[0].Value = "0";

       
        if (dtcln.Rows.Count > 0)
        {
            ViewState["versioninfo"] = dtcln.Rows[0]["VersionInfoId"].ToString();
            FilterProduct.SelectedValue = dtcln.Rows[0]["MasterPageId"].ToString();

            FillterCategorysearch();
            filtermainmenu();
            filtersubmenu();
            functionality();            
            FilterGrid();
        }
        //string strcln1 = " SELECT * From TheLastSearchedPageMasterTBL where UserID='" + Session["EmpId"] + "' and Pagename='PageMasterNew' Order By ID Desc";
        //SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
        //DataTable dtcln1 = new DataTable();
        //SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
        //adpcln1.Fill(dtcln1);
        //if (dtcln1.Rows.Count > 0)
        //{
        //    FilterProduct.SelectedValue = dtcln1.Rows[0]["ProductID"].ToString();
            
        //}


    }
    protected void FillterCategorysearch()
    {
        string stractive="";
        if (chk_activefilter.Checked == true)
        {
            stractive = " and dbo.Mainmenucategory.Active='1' ";
        }
        string strlan = "select * from Mainmenucategory where MasterPage_Id='" + FilterProduct.SelectedValue + "'"  + stractive ;
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
        string stractive="";
        if (chk_activefilter.Checked == true)
        {
            stractive = " and dbo.MainMenuMaster.Active='1'";
        }
        string filter = "";
        if (DDLCategoryS.SelectedIndex > 0)
        {
            filter = " and MainMenuMaster.MainMenucatId='" + DDLCategoryS.SelectedValue + "' ";
        }
        string strcln = " SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "' " + filter + " " + stractive + " order By MainMenuMaster.MainMenuTitle ";
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
        string stractive = "";
        if (chk_activefilter.Checked == true)
        {
            stractive = " and dbo.SubMenuMaster.Active='1'";
        }
        string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "' and SubMenuMaster.MainMenuId='" + FilterMenu.SelectedValue + "' " + stractive + " Order By SubMenuMaster.SubMenuName ";
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
        string stractive="";
        if (chk_activefilter.Checked == true)
        {
            stractive = " and dbo.FunctionalityMasterTbl.Active='1' ";
        }
        string functionality = "select ID,FunctionalityTitle from FunctionalityMasterTbl where VersionID='" + ViewState["versioninfo"] + "' " + stractive + " Order By FunctionalityTitle ";
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


        strcln = " select distinct TOP(100)  SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId, PageMaster.PageId,PageMaster.FolderName,MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,PageMaster.Active, dbo.PageMaster.ManuAccess, ProductMaster.ProductName,VersionInfoMaster.VersionInfoName, PageMaster.PageName,PageMaster.PageTitle,  WebsiteSection.SectionName + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name , dbo.Mainmenucategory.MainMenuCatName,dbo.MainMenuMaster.MainMenucatId , dbo.PageMaster.PageDescription from   dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteMasterId = dbo.WebsiteMaster.ID INNER JOIN dbo.MasterPageMaster ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN dbo.MainMenuMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.PageMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId LEFT OUTER JOIN dbo.Mainmenucategory ON dbo.MainMenuMaster.MainMenucatId = dbo.Mainmenucategory.MainMenucatId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.PageMaster.SubMenuId = dbo.SubMenuMaster.SubMenuId  where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' ";

       
            str1 = "and MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "' ";
        
        if (DDLCategoryS.SelectedIndex > 0)
        {
            str2 = " and MainMenuMaster.MainMenucatId='" + DDLCategoryS.SelectedValue + "'";
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
        if (TextBox5.Text != "")
        {
            str4 += "  and  (PageMaster.PageName Like '%" + TextBox5.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox5.Text + "%' OR dbo.Mainmenucategory.MainMenuCatName Like '%" + TextBox5.Text + "%' OR MainMenuMaster.MainMenuName Like '%" + TextBox5.Text + "%' OR SubMenuMaster.SubMenuName Like '%" + TextBox5.Text + "%' )";
        }
        string orderby = " ORDER BY dbo.Mainmenucategory.MainMenuCatName, dbo.MainMenuMaster.MainMenuName, dbo.SubMenuMaster.SubMenuName, dbo.PageMaster.PageName, dbo.PageMaster.Active DESC, dbo.PageMaster.ManuAccess DESC ";


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
        Session["pagesearch"] =null;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblpageid = (Label)e.Row.FindControl("lblpageid");
                ImageButton imgaccess = (ImageButton)e.Row.FindControl("imgaccess");
                ImageButton imgnotaccess = (ImageButton)e.Row.FindControl("imgnotaccess");
                Label lblDesi_right = (Label)e.Row.FindControl("lblDesi_right");
                LinkButton LinkButton2 = (LinkButton)e.Row.FindControl("LinkButton2");
                
                DataTable dtbackupmaster = Slectdata(" SELECT DISTINCT dbo.RoleMaster.Role_id, dbo.RoleMaster.Role_name FROM dbo.RoleMaster INNER JOIN  dbo.RolePageAccessRightTbl ON dbo.RoleMaster.Role_id = dbo.RolePageAccessRightTbl.RoleId INNER JOIN dbo.EmployeeMaster ON dbo.RoleMaster.Role_id = dbo.EmployeeMaster.RoleId where RolePageAccessRightTbl.PageId='" + lblpageid.Text + "' and dbo.EmployeeMaster.Id='" + Session["EmpId"] + "'");
                if (dtbackupmaster.Rows.Count > 0)
                {
                    imgaccess.Visible = true;
                    imgnotaccess.Visible = false;
                    LinkButton2.Enabled = true;
                }
                else
                {
                    imgnotaccess.Visible = true ;
                    imgaccess.Visible = false;
                    LinkButton2.Enabled = false;
                    LinkButton2.ToolTip = "No Page Access";
                    LinkButton2.CssClass = "tooltip"; 
                }

                DataTable dtbackupmasteremplo = Slectdata(" SELECT DISTINCT dbo.RoleMaster.Role_id, dbo.RoleMaster.Role_name FROM   dbo.RoleMaster INNER JOIN dbo.RolePageAccessRightTbl ON dbo.RoleMaster.Role_id = dbo.RolePageAccessRightTbl.RoleId where RolePageAccessRightTbl.PageId='" + lblpageid.Text + "' and RoleMaster.ActiveDeactive='1' ");
                if (dtbackupmasteremplo.Rows.Count > 0)
                {
                    string rolename = "";
                    foreach (DataRow dts in dtbackupmasteremplo.Rows)
                    {
                        rolename += "" + dts["Role_name"] + ",";
                    }
                    if (rolename.Length > 0)
                    {
                        rolename = rolename.Remove(rolename.Length - 1);
                    }
                    lblDesi_right.Text = rolename;
                }
                else
                {
                    imgnotaccess.Visible = false;
                }

                
            }
        }

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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
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
    protected DataTable Slectdata(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
    protected void link2_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        Label lbl_pagename_grid1 = (Label)GridView1.Rows[rinrow].FindControl("lbl_pagename_grid1");
        Label lbl_foldername = (Label)GridView1.Rows[rinrow].FindControl("lbl_foldername");
        Label lbl_MainMenucatId = (Label)GridView1.Rows[rinrow].FindControl("lbl_MainMenucatId");
        Label lblpageid = (Label)GridView1.Rows[rinrow].FindControl("lblpageid");
        //if (File.Exists("~/" + lbl_foldername.Text + "/" + lbl_pagename_grid1.Text + ""))
        //{
        //    Response.Redirect("~//Shoppingcart//Admin//" + lbl_pagename_grid1.Text + "?cat=" + lbl_MainMenucatId.Text + "");
        //}

        string StrPath = "";
        string str1211f1 = " SELECT dbo.MainMenuMaster.MainMenucatId, dbo.PageMaster.FolderName as Path FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId where PageId='" + lblpageid.Text  + "'  ";
        SqlDataAdapter da121f1 = new SqlDataAdapter(str1211f1, con);
        DataTable dt121f1 = new DataTable();
        da121f1.Fill(dt121f1);
        if (dt121f1.Rows.Count > 0)
        {
            StrPath = dt121f1.Rows[0]["Path"].ToString();
            StrPath += "/";
            string pagepath = "~/" + StrPath + "" + lbl_pagename_grid1.Text + ""; ;
            string filepath = Server.MapPath("~//" + StrPath + "//" + lbl_pagename_grid1.Text);
            FileInfo file = new FileInfo(filepath);
            if (file.Exists)
            {           
                Response.Redirect(pagepath + "?cat=" + dt121f1.Rows[0]["MainMenucatId"].ToString() + "");
            }
            else
            {

                lblmsg.Text = "file not exist (" + pagepath + ")"; 
                    //pagepath
            //    Response.Redirect(pagepath + "?cat=" + dt121f1.Rows[0]["MainMenucatId"].ToString() + "");
            }
        }
      
    }
}
