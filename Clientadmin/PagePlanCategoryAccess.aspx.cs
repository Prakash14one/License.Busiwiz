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

public partial class PageAccessUser : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
  
    DataSet dt;
    SqlConnection conn;
    public SqlConnection connweb;
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
            GridView1.DataSource = null;
            GridView1.DataBind();
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
        DataTable Dt=MyCommonfile.selectBZ(" Select * From PagePricePlanCategoryAccess Where PricePlanCategoryId='"+ddlpriceplancatagory.SelectedValue+"'");
        DataTable DtVersion = MyCommonfile.selectBZ(" SELECT dbo.PagePricePlanCategoryAccess.Id, dbo.PagePricePlanCategoryAccess.Pageid FROM dbo.Priceplancategory INNER JOIN dbo.PortalMasterTbl ON dbo.Priceplancategory.PortalId = dbo.PortalMasterTbl.Id INNER JOIN dbo.PagePricePlanCategoryAccess ON dbo.Priceplancategory.ID = dbo.PagePricePlanCategoryAccess.PricePlanCategoryId INNER JOIN dbo.VersionInfoMaster ON dbo.PortalMasterTbl.ProductId = dbo.VersionInfoMaster.ProductId Where VersionInfoMaster.VersionInfoId='"+ddlProductname.SelectedValue+"' ");
        if (Dt.Rows.Count > 0 || DtVersion.Rows.Count ==0)
        {
            Rbtn_CopyAccess.Visible = false;
            lbl_CopyAccess.Visible = false;
            ddlpriceplancatagoryCopyFrom.Visible = false;
            panelem.Visible = true;

            Label1.Text = "";                       
        }
        else
        {
            Rbtn_CopyAccess.Visible = true;
            lbl_CopyAccess.Visible = true;
            ddlpriceplancatagoryCopyFrom.Visible = false;
            panelem.Visible = Visible = true;

            if (Rbtn_CopyAccess.SelectedValue == "1")
            {
                ddlpriceplancatagoryCopyFrom.Visible = true;
                fillplancategoryCopyFrom();
            }            
        }
        FillGrid();
    }
    protected void Rbtn_CopyAccess_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpriceplancatagoryCopyFrom.Visible = false;
        if (Rbtn_CopyAccess.SelectedValue == "1")
        {
            ddlpriceplancatagoryCopyFrom.Visible = true;
            fillplancategoryCopyFrom();
        }
    }
    protected void fillplancategoryCopyFrom()
    {
        ddlpriceplancatagoryCopyFrom.Items.Clear();
        if (ddlportal.SelectedIndex > 0)
        {
            string strcln = " ";
            DataTable dtcln = MyCommonfile.selectBZ(" SELECT distinct Priceplancategory.Id ,Priceplancategory.CategoryName FROM  dbo.Priceplancategory INNER JOIN dbo.PortalMasterTbl ON dbo.PortalMasterTbl.Id = dbo.Priceplancategory.PortalId INNER JOIN dbo.PagePricePlanCategoryAccess ON dbo.Priceplancategory.ID = dbo.PagePricePlanCategoryAccess.PricePlanCategoryId where PortalId='" + ddlportal.SelectedValue + "' order  by CategoryName ");
            ddlpriceplancatagoryCopyFrom.DataSource = dtcln;
            ddlpriceplancatagoryCopyFrom.DataTextField = "CategoryName";
            ddlpriceplancatagoryCopyFrom.DataValueField = "Id";
            ddlpriceplancatagoryCopyFrom.DataBind();
        }
        ddlpriceplancatagoryCopyFrom.Items.Insert(0, "-Select-");
        ddlpriceplancatagoryCopyFrom.Items[0].Value = "0";
    }
    protected void ddlpriceplancatagoryCopyFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        FillGrid();
        panelem.Visible = true;
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
        string st11 = "";
        string cateid="";
        if (ddlpriceplancatagory.SelectedIndex>0)
        {
          cateid="  and PricePlanMaster.PriceplancatId='" + ddlpriceplancatagory.SelectedValue + "'";
        }
        if (DDLWebsiteC.SelectedIndex > 0)
        {
            st11 += " and WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "'";
        }
        if (DDLmasterpageL.SelectedIndex > 0)
        {
            st11 += " and MainMenuMaster.MasterPage_Id='" + DDLmasterpageL.SelectedValue + "'";
        }
        if (DropDownList1.SelectedIndex > 0)
        {
            st11 += " and PageMaster.MainMenuId='" + DropDownList1.SelectedValue + "'";
        }
        if (DropDownList2.SelectedIndex > 0)
        {
            st11 += " and PageMaster.SubMenuId='" + DropDownList2.SelectedValue + "'";
        }
        if (DDLCategoryS.SelectedIndex > 0)
        {
            st11 += " and MainMenuMaster.MainMenucatId='" + DDLCategoryS.SelectedValue + "'";
        }
        string prid = "";
        if (ddlpriceplancatagory.SelectedIndex > 0)
        {
            if (Rbtn_CopyAccess.SelectedValue == "1")
            {
                prid = " and PagePricePlanCategoryAccess.Priceplancategoryid=(select Top(1) ID as Priceplancategoryid from Priceplancategory where ID='" + ddlpriceplancatagoryCopyFrom.SelectedValue + "')";
            }
            else
            {
                prid = " and PagePricePlanCategoryAccess.Priceplancategoryid=(select Top(1) ID as Priceplancategoryid from Priceplancategory where ID='" + ddlpriceplancatagory.SelectedValue + "')";
            }
        }
        if (ddlstatus.SelectedValue=="1")
        {
            st11 += " and  PageMaster.Active=1";
        }
        if (ddlstatus.SelectedValue == "0")
        {
            st11 += " and  PageMaster.Active=0";
        }
        if (chk_activelistonly.Checked == true)
        {
          
        }
        string strcln = " ";
        if (DDL_Accesspages.SelectedValue == "2")
        {
            strcln = @" SELECT DISTINCT dbo.PageMaster.PageId, dbo.MasterPageMaster.MasterPageName, dbo.ProductMaster.ProductName,  dbo.VersionInfoMaster.VersionInfoName, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageDescription, dbo.PageMaster.SubMenuId,  dbo.PageMaster.MainMenuId, dbo.MainMenuMaster.MainMenuName, dbo.SubMenuMaster.SubMenuName, dbo.Mainmenucategory.MainMenuCatName,  dbo.PagePricePlanCategoryAccess.Id,CASE WHEN (PagePricePlanCategoryAccess.Id IS NOT NULL) THEN CAST('1' AS bit) else CAST('0' AS bit) END AS pag "+
                       " FROM   dbo.Mainmenucategory RIGHT OUTER JOIN dbo.PagePricePlanCategoryAccess RIGHT OUTER JOIN dbo.MasterPageMaster INNER JOIN dbo.MainMenuMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.PageMaster ON dbo.PageMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId ON  dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId ON dbo.PagePricePlanCategoryAccess.Pageid = dbo.PageMaster.PageId  " + prid + "  ON  dbo.Mainmenucategory.MainMenucatId = dbo.MainMenuMaster.MainMenucatId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.PageMaster.SubMenuId = dbo.SubMenuMaster.SubMenuId "+
                        "    Where  WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "' and PageMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' " + st11 + "";
        }
        else if (DDL_Accesspages.SelectedValue == "1")
        {
            strcln = @" SELECT DISTINCT dbo.PageMaster.PageId,dbo.MasterPageMaster.MasterPageName,  dbo.ProductMaster.ProductName,  dbo.VersionInfoMaster.VersionInfoName, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageDescription, dbo.PageMaster.SubMenuId,  dbo.PageMaster.MainMenuId, dbo.MainMenuMaster.MainMenuName, dbo.SubMenuMaster.SubMenuName, dbo.Mainmenucategory.MainMenuCatName,  dbo.PagePricePlanCategoryAccess.Id,CASE WHEN (PagePricePlanCategoryAccess.Id IS NOT NULL) THEN CAST('1' AS bit) else CAST('0' AS bit) END AS pag "+                       
                       " FROM   dbo.Mainmenucategory RIGHT OUTER JOIN dbo.PagePricePlanCategoryAccess RIGHT OUTER JOIN dbo.MasterPageMaster INNER JOIN dbo.MainMenuMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.PageMaster ON dbo.PageMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId ON  dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId ON dbo.PagePricePlanCategoryAccess.Pageid = dbo.PageMaster.PageId  "+prid+" ON  dbo.Mainmenucategory.MainMenucatId = dbo.MainMenuMaster.MainMenucatId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.PageMaster.SubMenuId = dbo.SubMenuMaster.SubMenuId "+
                       " Where  WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "' and  PageMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' and PagePricePlanCategoryAccess.Id IS NOT NULL " + st11 + "";            
        }
        else if (DDL_Accesspages.SelectedValue == "0")
        {
            strcln = @" SELECT DISTINCT dbo.PageMaster.PageId,  dbo.MasterPageMaster.MasterPageName, dbo.ProductMaster.ProductName,  dbo.VersionInfoMaster.VersionInfoName, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageDescription, dbo.PageMaster.SubMenuId,  dbo.PageMaster.MainMenuId, dbo.MainMenuMaster.MainMenuName, dbo.SubMenuMaster.SubMenuName, dbo.Mainmenucategory.MainMenuCatName,  dbo.PagePricePlanCategoryAccess.Id,CASE WHEN (PagePricePlanCategoryAccess.Id IS NOT NULL) THEN CAST('1' AS bit) else CAST('0' AS bit) END AS pag "+
                                   " FROM   dbo.Mainmenucategory RIGHT OUTER JOIN dbo.PagePricePlanCategoryAccess RIGHT OUTER JOIN dbo.MasterPageMaster INNER JOIN dbo.MainMenuMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.PageMaster ON dbo.PageMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId ON  dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId ON dbo.PagePricePlanCategoryAccess.Pageid = dbo.PageMaster.PageId "+prid+" ON  dbo.Mainmenucategory.MainMenucatId = dbo.MainMenuMaster.MainMenucatId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.PageMaster.SubMenuId = dbo.SubMenuMaster.SubMenuId "+
                                   " Where WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "' and PageMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' and PagePricePlanCategoryAccess.Id IS NULL " + st11 + "";            
        }
        
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
        if (GridView1.Rows.Count > 0)
        {
            Button1.Visible = true;
        }
        else
        {
            Button1.Visible = false;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string rolid = "";
        string deptid = "";
        if (ddlpriceplancatagory.SelectedIndex >0)
        {
            if (GridView1.Rows.Count > 0)
            { 
                foreach (GridViewRow gg1 in GridView1.Rows)
                {
                    CheckBox ac = (CheckBox)(gg1.FindControl("Cbitem"));
                    Label lbl_PageId = (Label)(gg1.FindControl("lblpageid"));
                    string PageId = lbl_PageId.Text;
                  
                        string selepagea1 = "select * from PagePricePlanCategoryAccess where PageAccess='True' and Pageid='" + PageId + "' and PricePlanCategoryId='" + ddlpriceplancatagory.SelectedValue + "'";
                        SqlDataAdapter adsel1 = new SqlDataAdapter(selepagea1, con);
                        DataSet dtsel1 = new DataSet();
                        adsel1.Fill(dtsel1);
                        if (dtsel1.Tables[0].Rows.Count == 0 && ac.Checked == true)
                        {
                            string spt = "insert into PagePricePlanCategoryAccess(Pageid,PricePlanCategoryId,PageAccess)values('" + PageId + "','" + ddlpriceplancatagory.SelectedValue + "','" + ac.Checked + "')";
                            SqlCommand cmd1 = new SqlCommand(spt, con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                        }
                        else if (ac.Checked == false && dtsel1.Tables[0].Rows.Count > 0)
                        {
                            string sptt = " delete from PagePricePlanCategoryAccess where Pageid='" + PageId + "'  and PricePlanCategoryId='" + ddlpriceplancatagory.SelectedValue + "'";
                            SqlCommand cmd11 = new SqlCommand(sptt, con);
                            con.Open();
                            cmd11.ExecuteNonQuery();
                            con.Close();
                        }
                        
                }         
                Label1.Text = "Page Access Given Successfully";            
            }
        }
    }
    protected void DefaultUser()
    {
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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGrid();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
 
  
    protected void ch1_chachedChanged(object sender, EventArgs e)
    {
        //GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        //int rinrow = row.RowIndex;
        foreach (GridViewRow item in GridView1.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }

    protected void ddlpriceplan_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
  

   
   

   


   

  

  
}

