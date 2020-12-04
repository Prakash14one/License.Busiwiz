using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
//using System.DirectoryServices;
using System.IO.Compression;
using System.IO;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using System.Configuration;
using System.Data;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;
using System.Xml;
public partial class Publish_Uncompiled_ : System.Web.UI.Page
{
   
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
        SqlConnection conn;
        SqlConnection connserver;
        public static string encstr = "";
        public static double size = 0;
        public static double sizeout = 0;
        string code_typeid = "";
        bool gg;
        int StepId = 1;
        protected void Page_Load(object sender, EventArgs e)
        {          
            Create_WebConfig("E:\\");
            if (!Page.IsPostBack)
            {
                if (Session["Error"] != null)
                {
                    lblmsg.Text = Session["Error"].ToString();
                }
               // Move("E:\\PRAKASH\\2820 SERVER PROJECT\\License.busiwiz.com\\Bib\\Web1.Config", "E:\\PRAKASH\\2820 SERVER PROJECT\\License.busiwiz.com\\Web1.Config");
               
                FillProduct();
                fillcodetypecategory();
                fillcodetype();

                FillWebsiteGrid();

                FillProductSearch();                
                fillgrid();
                FilFTP();
               
            }
        }
    
    //****************************************************************
        protected void FilFTP()
        {
            string finalstr = " Select * From ClientMaster Where ClientMasterId='" + Session["ClientId"] + "'";
            SqlCommand cmd = new SqlCommand(finalstr, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                string ftp = ds.Rows[0]["FTP"].ToString();
                string user = ds.Rows[0]["FTPUserName"].ToString();
                string pass = ds.Rows[0]["FTPPassword"].ToString();
                string port = ds.Rows[0]["FTPPort"].ToString();
                string ftpuseurl = ds.Rows[0]["FTP"].ToString();
                string username = ds.Rows[0]["FTPUserName"].ToString();
                ftpservename.Text = ftpuseurl;
                ftpuser.Text = username;
            }
            else
            {
            }
        }
        protected void clear()
        {
            FillProduct();
            fillcodetypecategory();
            fillcodetype();
            FillWebsiteGrid();
            FillProductSearch();
            fillgrid();

            addnewpanel.Visible = true;
            pnladdnew.Visible = false;
        }
        protected void FillProduct()
        {
            string strcln = " SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName,ProductMaster.ProductName +':'+ VersionInfoMaster.VersionInfoName as aa,VersionInfoMaster.VersionInfoId  FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  and VersionInfoMaster.Active='1'  order  by ProductName";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddlproductname.DataSource = dtcln;
            ddlproductname.DataValueField = "VersionInfoId";
            ddlproductname.DataTextField = "ProductName";
            ddlproductname.DataBind();
            fillpath();
        }
        protected void ddlproductname_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillpath();
            fillcodetype();
            FillWebsiteGrid();
        }
        protected void fillcodetypecategory()
        {
            DataTable dtcln =MyCommonfile.selectBZ(" select * from CodeTypeCategory where CodeMasterNo='1' ");//
            ddlcodetypecatefory.DataSource = dtcln;
            ddlcodetypecatefory.DataValueField = "CodeMasterNo";
            ddlcodetypecatefory.DataTextField = "CodeTypeCategory";
            ddlcodetypecatefory.DataBind();


        }
        protected void fillcodetype()
        {
            string strcln = "select CodeTypeTbl.* from CodeTypeTbl inner join CodeTypeCategory on CodeTypeCategory.CodeMasterNo=CodeTypeTbl.CodeTypeCategoryId where CodeTypeTbl.ProductVersionId='" + ddlproductname.SelectedValue + "' and CodeTypeTbl.CodeTypeCategoryId='" + ddlcodetypecatefory.SelectedValue + "' order  by CodeTypeTbl.Name";
            strcln = " SELECT DISTINCT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where  ProductCodeDetailTbl.Active='1' and CodeTypeTbl.ProductVersionId='" + ddlproductname.SelectedValue + "' and CodeTypeCategory.Id='" + ddlcodetypecatefory.SelectedValue + "'  order  by dbo.ProductCodeDetailTbl.CodeTypeName ";

            strcln = " SELECT dbo.WebsiteMaster.WebsiteName, dbo.WebsiteMaster.WebsiteName + '-' + dbo.CodeTypeTbl.Name AS CodeTypeName,  dbo.CodeTypeTbl.Name, dbo.CodeTypeTbl.CodeTypeCategoryId,  dbo.CodeTypeTbl.ProductCodeDetailId,  dbo.CodeTypeTbl.WebsiteID, dbo.CodeTypeTbl.FileName,  dbo.CodeTypeTbl.Instancename FROM dbo.WebsiteMaster INNER JOIN dbo.CodeTypeTbl ON dbo.WebsiteMaster.ID = dbo.CodeTypeTbl.WebsiteID INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id Where ProductCodeDetailTbl.Active='1' and CodeTypeTbl.ProductVersionId='" + ddlproductname.SelectedValue + "' and dbo.CodeTypeTbl.CodeTypeCategoryId='" + ddlcodetypecatefory.SelectedValue + "'  order  by dbo.ProductCodeDetailTbl.CodeTypeName  ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddlcodetype.DataSource = dtcln;
            ddlcodetype.DataValueField = "ProductCodeDetailId";
            ddlcodetype.DataTextField = "WebsiteName";
            ddlcodetype.DataBind();
            ddlcodetype.Items.Insert(0, "--Select--");
            ddlcodetype.Items[0].Value = "0";
            ddlcodetype.SelectedIndex = 0;
            findnewcodeversion();

            DataTable dtCodeVersionID = MyCommonfile.selectBZ("Select * From CodeTypeTbl Where ProductCodeDetailId=" + ddlcodetype.SelectedValue + "");
            if (dtCodeVersionID.Rows.Count > 0)
            {
                code_typeid = dtCodeVersionID.Rows[0]["Id"].ToString();  
            }
        }
        protected void fillpath()
        {
            DataTable dtcln = MyCommonfile.selectBZ("SELECT  dbo.ProductMaster.Description,dbo.ProductMaster.ProductName,dbo.VersionInfoMaster.ClientFTPRootPath, dbo.VersionInfoMaster.VersionInfoId, dbo.VersionInfoMaster.VersionInfoName, dbo.VersionInfoMaster.ProductId, dbo.VersionInfoMaster.Active, dbo.VersionInfoMaster.ProductURL, dbo.VersionInfoMaster.PricePlanURL, dbo.VersionInfoMaster.MasterCodeSourcePath, dbo.VersionInfoMaster.TemporaryPublishPath, dbo.VersionInfoMaster.DestinationPath FROM dbo.VersionInfoMaster INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId where VersionInfoId='" + ddlproductname.SelectedValue + "' ");
            if (dtcln.Rows.Count > 0)
            {
                // txtsourcepath.Text = dtcln.Rows[0]["MasterCodeSourcePath"].ToString();
                // txttemppath.Text = dtcln.Rows[0]["TemporaryPublishPath"].ToString();
                // txtoutputsourcepath.Text = dtcln.Rows[0]["DestinationPath"].ToString();
                //  txtsourcepath.Text = dtcln.Rows[0]["ClientFTPRootPath"].ToString() + "\\" + dtcln.Rows[0]["ProductName"].ToString();
                txt_prod_desc.Text = dtcln.Rows[0]["Description"].ToString();
            }
        }
        protected void findnewcodeversion()
        {
            DataTable dtCodeVersionID = MyCommonfile.selectBZ("Select * From CodeTypeTbl Where ProductCodeDetailId=" + ddlcodetype.SelectedValue + "");
            if (dtCodeVersionID.Rows.Count > 0)
            {
                code_typeid = dtCodeVersionID.Rows[0]["Id"].ToString();
            }
            DataTable dtv = MyCommonfile.selectBZ("select Max(codeversionnumber) as codeversionnumber from ProductMasterCodeTbl where CodeTypeID='" + code_typeid + "' and ProductVerID='" + ddlproductname.SelectedValue + "'");
            if (dtv.Rows.Count > 0)
            {
                if (Convert.ToString(dtv.Rows[0]["codeversionnumber"]) != "")
                {
                    lblnewcodetypeNo.Text = (Convert.ToInt32(dtv.Rows[0]["codeversionnumber"]) + 1).ToString();
                }
                else
                {
                    lblnewcodetypeNo.Text = "1";
                }
            }
            else
            {
                lblnewcodetypeNo.Text = "1";
            }
        }
        //-----------Search Fill**--------------
        protected void FillProductSearch()
        {
            string strcln = " SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName,ProductMaster.ProductName +':'+ VersionInfoMaster.VersionInfoName as aa,VersionInfoMaster.VersionInfoId  FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  and VersionInfoMaster.Active='1'  order  by ProductName";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            DDLProductSearch.DataSource = dtcln;
            DDLProductSearch.DataValueField = "VersionInfoId";
            DDLProductSearch.DataTextField = "ProductName";
            DDLProductSearch.DataBind();
            DDLProductSearch.Items.Insert(0, "--Select--");
            DDLProductSearch.Items[0].Value = "0";
            DDLProductSearch.SelectedIndex = 0;
        }
        protected void DDLProductSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillwebsite();
            fillgrid();
        }

        protected void fillwebsite()
        {
            string strcln = "select * from WebsiteMaster where VersionInfoId='" + DDLProductSearch .SelectedValue+ "'";
           
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            DropDownList1.DataSource = dtcln;
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataTextField = "WebsiteName";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "All");
            DropDownList1.Items[0].Value = "0";
            DropDownList1.SelectedIndex = 0;
        }

        //-***********Website Grid web
        protected void FillWebsiteGrid()
        {
            string strsearch = "";          
                strsearch = " ";            
            string strcln = " SELECT distinct ID, WebsiteName,Case when(ID IS NULL) then  cast ('1' as bit) else  cast('0' as bit) end as chk From dbo.WebsiteMaster INNER JOIN dbo.VersionInfoMaster ON dbo.WebsiteMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId  Where dbo.VersionInfoMaster.Active=1 " + strsearch + "  ";
            strcln = " SELECT dbo.WebsiteMaster.WebsiteName + '-' + dbo.CodeTypeTbl.Name AS CodeTypeName,dbo.WebsiteMaster.WebsiteName,Case when(WebsiteID IS NULL) then  cast ('1' as bit) else  cast('0' as bit) end as chk,  dbo.CodeTypeTbl.Name, dbo.CodeTypeTbl.CodeTypeCategoryId,  dbo.CodeTypeTbl.ProductCodeDetailId,  dbo.CodeTypeTbl.WebsiteID, dbo.CodeTypeTbl.ID FROM dbo.WebsiteMaster INNER JOIN dbo.CodeTypeTbl ON dbo.WebsiteMaster.ID = dbo.CodeTypeTbl.WebsiteID INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id Where ProductCodeDetailTbl.Active='1' and dbo.WebsiteMaster.VersionInfoId=" + ddlproductname.SelectedValue + "  order  by dbo.ProductCodeDetailTbl.CodeTypeName  ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            GridView2.DataSource = dtcln;
            GridView2.DataBind();
        }
        protected void ch1_chachedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow item in GridView2.Rows)
            {
                CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
                cbItem1.Checked = ((CheckBox)sender).Checked;
            }
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //VersionDate
                Label lbl_codetypeid = (Label)e.Row.FindControl("lbl_codetypeid");
                Label lblwebid = (Label)e.Row.FindControl("lblwebid");
                Label lblcheck = (Label)e.Row.FindControl("lblcheck");
                
                CheckBox cbItem = (CheckBox)(e.Row.FindControl("cbItem"));
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                string strwebsite = " SELECT   TOP (1) dbo.ProductMasterCodeTbl.ID, dbo.ProductMasterCodeTbl.VersionDate, dbo.CodeTypeTbl.WebsiteID FROM dbo.ProductMasterCodeTbl INNER JOIN dbo.CodeTypeTbl ON dbo.ProductMasterCodeTbl.CodeTypeID = dbo.CodeTypeTbl.ID Where dbo.ProductMasterCodeTbl.CodeTypeID='" + lbl_codetypeid.Text + "'   ORDER BY dbo.ProductMasterCodeTbl.ID DESC ";
                SqlCommand cmd12web = new SqlCommand(strwebsite, con);
                SqlDataAdapter adp12web = new SqlDataAdapter(cmd12web);
                DataTable ds12web = new DataTable();
                adp12web.Fill(ds12web);
                if (ds12web.Rows.Count > 0)
                {
                    string strwebsiteVersion = " SELECT TOP (1) dbo.PageMaster.PageId, dbo.PageMaster.PageTypeId, dbo.PageMaster.PageName, dbo.WebsiteSection.WebsiteMasterId, dbo.PageVersionTbl.Date, dbo.PageVersionTbl.Id " +
                         " FROM dbo.PageVersionTbl INNER JOIN dbo.PageMaster ON dbo.PageVersionTbl.PageMasterId = dbo.PageMaster.PageId INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId Where dbo.WebsiteSection.WebsiteMasterId='" + lblwebid.Text + "' ORDER BY dbo.PageVersionTbl.Id DESC ";
                    SqlCommand cmd12webVer = new SqlCommand(strwebsiteVersion, con);
                    SqlDataAdapter adp12webVer = new SqlDataAdapter(cmd12webVer);
                    DataTable ds12webVer = new DataTable();
                    adp12webVer.Fill(ds12webVer);
                    if (ds12webVer.Rows.Count > 0)
                    {
                        try
                        {
                            DateTime dt1 = DateTime.Parse("" + ds12web.Rows[0]["VersionDate"] + "");
                            DateTime dt2 = DateTime.Parse("" + ds12webVer.Rows[0]["Date"] + "");
                            if (dt1.Date > dt2.Date)
                            {
                                //It's a later date
                                cbItem.Checked = true;
                                lblcheck.Text = "Recommanded to create new version";
                            }
                            else
                            {
                                //It's an earlier or equal date
                                cbItem.Checked = false;
                                lblcheck.Text = "No Need";
                            }
                        }
                        catch
                        {
                        }                       
                    }
                    else
                    {
                        //It's an earlier or equal date
                        cbItem.Checked = false;
                        lblcheck.Text = "No Need";
                    }       
                }
                else
                {
                    //It's an earlier or equal date
                    cbItem.Checked = false;
                    lblcheck.Text = "No Need";
                }
            }
        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            FillWebsiteGrid();
        }
        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            hdnsortExp.Value = e.SortExpression.ToString();
            hdnsortDir.Value = sortOrder;
            FillWebsiteGrid();
        }
        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
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
        //-***************************
        //      
        protected void fillgrid()
        {
            string strsearch = "";
            if (DDLProductSearch.SelectedIndex > 0)
            {
                strsearch += " and dbo.ProductMasterCodeTbl.ProductVerID=" + DDLProductSearch.SelectedValue + "";
            }           
            if (DDLCodeTypeSearch.SelectedIndex > 0)
            {
                //strsearch += " and dbo.ProductCodeDetailTbl.Id=" + DDLCodeTypeSearch.SelectedValue + "";
            }
            if (DropDownList1.SelectedIndex > 0)
            {
                strsearch += " and dbo.CodeTypeTbl.WebsiteID=" + DropDownList1.SelectedValue + "";
            }
            if (DropDownList2.SelectedValue== "1")
            {
                DateTime today = DateTime.Now;
                DateTime week = today.AddDays(7);
                strsearch += " and dbo.ProductMasterCodeTbl.VersionDate BETWEEN '" + today.ToShortDateString() + "' and '" + week.ToShortDateString() + "' ";
            }
            if (DropDownList2.SelectedValue == "1")
            {
                DateTime today = DateTime.Now;                
                var thisMonthStart = today.AddDays(1 - today.Day);
                var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
                strsearch += " and dbo.ProductMasterCodeTbl.VersionDate BETWEEN '" + thisMonthStart.ToShortDateString() + "' and '" + thisMonthEnd.ToShortDateString() + "' ";
            }
            if (ddlCategorySearch.SelectedValue == "1")
            {
                strsearch += " and dbo.ProductMasterCodeTbl.CreatedUsingCompliler=1";
            }
            else if (ddlCategorySearch.SelectedValue== "0")
            {
                strsearch += " and dbo.ProductMasterCodeTbl.CreatedUsingCompliler=0";
            }
            if (DDLCodeTypeSearch.SelectedValue == "1")
            {
                strsearch += " and dbo.ProductMasterCodeTbl.Successfullycreated=1";
            }
            else if (DDLCodeTypeSearch.SelectedValue == "0")
            {
                strsearch += " and dbo.ProductMasterCodeTbl.Successfullycreated=0";
            }            
            if (TextBox1.Text != "")
            {
                strsearch += " and (dbo.ProductCodeDetailTbl.CodeTypeName like '%" + TextBox1.Text.Replace("'", "''") + "%')";
            }
            string strcln = " select TOP(100) WebsitePublish.*,ProductMaster.ProductName,ProductMaster.ProductName +':'+ VersionInfoMaster.VersionInfoName as ProductNamever,CodeTypeTbl.Name from WebsitePublish inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsitePublish.ProductVersionId inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join CodeTypeTbl on CodeTypeTbl.ID=WebsitePublish.CodeTypeId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' " + strsearch + "";
           // strcln = " SELECT DISTINCT dbo.ProductCodeDetailTbl.Id AS Expr1, dbo.WebsitePublish.Id, dbo.WebsitePublish.CodeTypeId, dbo.WebsitePublish.ProductVersionId, dbo.WebsitePublish.DateTime, dbo.WebsitePublish.SourcePath, dbo.WebsitePublish.OutputPath, dbo.WebsitePublish.OutPutfileZipName, dbo.WebsitePublish.codeversionnumber, dbo.ProductMaster.ProductName, dbo.ProductCodeDetailTbl.CodeTypeName,dbo.ProductMasterCodeTbl.Complile,dbo.ProductMasterCodeTbl.Successfullycreated  FROM   dbo.WebsitePublish INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsitePublish.ProductVersionId INNER JOIN dbo.ProductMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.CodeTypeTbl ON dbo.CodeTypeTbl.ID = dbo.WebsitePublish.CodeTypeId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeTbl.CodeTypeCategoryId = dbo.CodeTypeCategory.Id left join ProductMasterCodeTbl on ProductMasterCodeTbl.ProductVerID=ProductCodeDetailTbl.ProductId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' " + strsearch + "";
            strcln = " SELECT DISTINCT TOP (100) dbo.ProductMasterCodeTbl.ID, dbo.ProductMasterCodeTbl.ProductVerID, dbo.ProductMasterCodeTbl.CodeTypeID, dbo.ProductMasterCodeTbl.codeversionnumber, dbo.ProductMasterCodeTbl.filename, dbo.ProductMasterCodeTbl.physicalpath, dbo.ProductMasterCodeTbl.TemporaryPath, case when CreatedUsingCompliler = 1 then 'YES' when CreatedUsingCompliler = 0 then 'NO' else 'UNDEFINED' end AS CreatedUsingCompliler , case when Successfullycreated = 1 then 'YES' when Successfullycreated = 0 then 'NO' else 'UNDEFINED' end AS Successfullycreated  , dbo.ProductMasterCodeTbl.VersionDate, dbo.ProductMaster.ProductName, dbo.CodeTypeTbl.Name  " +
                    "  FROM dbo.CodeTypeTbl INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeTbl.CodeTypeCategoryId = dbo.CodeTypeCategory.Id INNER JOIN dbo.ProductMasterCodeTbl ON dbo.CodeTypeTbl.ID = dbo.ProductMasterCodeTbl.CodeTypeID INNER JOIN dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON  dbo.ProductMasterCodeTbl.ProductVerID = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.WebsitePublish ON dbo.ProductMasterCodeTbl.CodeTypeID = dbo.WebsitePublish.CodeTypeId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' " + strsearch + " ORDER BY dbo.ProductMasterCodeTbl.ID DESC ";           
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            grdcompiledproduct.DataSource = dtcln;
            grdcompiledproduct.DataBind();
            for (int rowindex = 0; rowindex < dtcln.Rows.Count; rowindex++)
            {
               
                try
                {
                    DataTable dateyu = select("SELECT DISTINCT  dbo.WebsitePublish.Id, dbo.WebsitePublish.CodeTypeId, dbo.WebsitePublish.ProductVersionId, dbo.WebsitePublish.DateTime, dbo.WebsitePublish.SourcePath, dbo.WebsitePublish.OutputPath, dbo.WebsitePublish.OutPutfileZipName, dbo.WebsitePublish.codeversionnumber, dbo.ProductMaster.ProductName, dbo.ProductCodeDetailTbl.CodeTypeName,dbo.ProductMasterCodeTbl.Complile,dbo.ProductMasterCodeTbl.Successfullycreated FROM   dbo.WebsitePublish INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsitePublish.ProductVersionId INNER JOIN dbo.ProductMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.CodeTypeTbl ON dbo.CodeTypeTbl.ID = dbo.WebsitePublish.CodeTypeId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeTbl.CodeTypeCategoryId = dbo.CodeTypeCategory.Id left join ProductMasterCodeTbl on ProductMasterCodeTbl.ProductVerID=ProductCodeDetailTbl.ProductId where dbo.WebsitePublish.Id =" + dtcln.Rows[rowindex]["Id"] + "");
                    if (dateyu.Rows.Count > 0)
                    {
                        if (dateyu.Rows[rowindex]["Complile"] == "1")
                        {
                            dateyu.Rows[rowindex]["Complile"] = "Yes";
                        }
                        else
                        {
                            dateyu.Rows[rowindex]["Complile"] = "No";                        
                        }
                        if (dateyu.Rows[rowindex]["Successfullycreated"] == "1")
                        {
                            dateyu.Rows[rowindex]["Successfullycreated"] = "Yes";
                        }
                        else
                        {
                            dateyu.Rows[rowindex]["Successfullycreated"] = "No";
                        }
                    }
                }
                catch { }
            }
        }
        protected void grdcompiledproduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_latestpath = (Label)e.Row.FindControl("lbl_latestpath");
                Label lblstatus = (Label)e.Row.FindControl("lblstatus");
                //CheckBox cbItem = (CheckBox)(e.Row.FindControl("cbItem"));
                if (File.Exists(lbl_latestpath.Text))
                {
                    lblstatus.Text = "Yes";
                    lblstatus.ToolTip = "File available at here :" + lbl_latestpath.Text;
                }
                else
                {
                    lblstatus.Text = "No";
                    lblstatus.ToolTip = "File not available at here :"+lbl_latestpath.Text; 
                }              
            }
        }
        protected DataTable select(string qu)
        {
            SqlCommand cmd = new SqlCommand(qu, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }       
        protected void addnewpanel_Click(object sender, EventArgs e)
        {
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;
            //lbllegend.Text = "Add New Product Update";
            lblmsg.Text = "";
        }
        protected void btn_syncser_Click(object sender, EventArgs e)
        {
            SyncroniceLastRecord();
        }
        protected void Btncancle_Click(object sender, EventArgs e)
        {
            addnewpanel.Visible = true;
            pnladdnew.Visible = false;
            txtoutputsourcepath.Text = "";
            txtsourcepath.Text = "";
            txttemppath.Text = "";
            txtoutputsourcepath.Text = "";
            txttemppath.Text = "";
            txtoutputsourcepath.Text = "";
            FillProduct();
            fillcodetypecategory();
           
            FillProduct();
            fillcodetypecategory();
            fillcodetype();
            ddlcodetype_SelectedIndexChanged(sender, e);
            lblmsg.Text = "";
        }
        protected void btn_VersionCreate_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            StepId = 1;
            string pathcheck = txtsourcepath.Text;
            if (Directory.Exists(pathcheck))
            {
                DirectoryInfo sourceDir = new DirectoryInfo(pathcheck);
                double size = GetSizeDirectory(sourceDir);
                double twpercsize = 0.20 * size;
                if (twpercsize < 100)
                {
                    StepId = 0;
                    lblmsg.Visible = true;
                    lblmsg.Text = "website folder is empty (" + pathcheck + ")";
                    return;
                }
            }
            else
            {
                StepId = 0;
                lblmsg.Visible = true;
                lblmsg.Text = "website define folder not available (" + pathcheck + ")";
                return;
            }
            //Step1----Get CodeTypeID--------------------------------------------------------------------------------------------------------------------------------------------            
            if (StepId == 1)
            {
                try
                {
                    DataTable dtCodeVersionID = MyCommonfile.selectBZ("Select * From CodeTypeTbl Where ProductCodeDetailId=" + ddlcodetype.SelectedValue + "");
                    if (dtCodeVersionID.Rows.Count > 0)
                    {
                        code_typeid = dtCodeVersionID.Rows[0]["Id"].ToString();
                    }
                    StepId++;
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.ToString();
                    return;
                }
            }
            //Step-2----Create Folder Name--------------------------------------------------------------------------------------------------------------------------------------------
            string productversionid = ddlproductname.SelectedValue;
            string codecategoryno = ddlcodetypecatefory.SelectedValue;
            string codetypeid = code_typeid;
            string codeversionno = lblnewcodetypeNo.Text;
            string codename = ddlcodetype.SelectedItem.Text;
            string newfoldername = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string dateformat = DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string insidefoldername = "";
            if (StepId == 2)
            {               
                StepId++;
            }
            //Step-3-----Create Folder Name--------------------------------------------------------------------------------------------------------------------------------------------
            if (StepId == 3)
            {
                try
                {
                    string strbasefilename = " select * from ProductMasterCodeTbl where ProductVerID='" + ddlproductname.SelectedValue + "' and CodeTypeID='" + codetypeid + "' and codeversionnumber=(select Min(codeversionnumber) from ProductMasterCodeTbl where ProductVerID='" + ddlproductname.SelectedValue + "' and CodeTypeID='" + codetypeid + "') ";
                    SqlCommand cmdbasefilename = new SqlCommand(strbasefilename, con);
                    DataTable dtbasefilename = new DataTable();
                    SqlDataAdapter adpbasefilename = new SqlDataAdapter(cmdbasefilename);
                    adpbasefilename.Fill(dtbasefilename);
                    if (dtbasefilename.Rows.Count > 0)
                    {
                        string tempfilename = dtbasefilename.Rows[0]["filename"].ToString();
                        string unzipfilename = tempfilename.Replace(".zip", "");
                        insidefoldername = unzipfilename;
                    }
                    else
                    {
                        insidefoldername = productversionid + "_" + codename + "_" + codetypeid + "_" + codeversionno;
                    }
                    StepId++;
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.ToString();
                    return;
                }
            }
            string filename = insidefoldername + ".zip";
            //Step-4----Create Directory in Output path--------------------------------------------------------------------------------------------------------------------------------------------
            if (StepId == 4)
            {
                try
                {
                    if (!Directory.Exists(txtoutputsourcepath.Text + "\\" + newfoldername))
                    {
                        Directory.CreateDirectory(txtoutputsourcepath.Text + "\\" + newfoldername);//D:\ALLNEWCOMPANYMASTERCOPY\35\IJobcenterMaster\WWW\PublishCode\   1112017112657
                    }
                    if (!Directory.Exists(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername))//D:\ALLNEWCOMPANYMASTERCOPY\35\IJobcenterMaster\WWW\PublishCode\   1112017112657  10090_WWW_14133_1
                    {
                        Directory.CreateDirectory(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername);
                    }
                    if (!Directory.Exists(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + "\\" + insidefoldername))//D:\ALLNEWCOMPANYMASTERCOPY\35\IJobcenterMaster\WWW\PublishCode\   1112017112657 10090_WWW_14133_1  10090_WWW_14133_1
                    {
                        Directory.CreateDirectory(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + "\\" + insidefoldername);
                    }                    
                    StepId++;
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.ToString();
                    return;
                }
            }
            //Step-5----Website original path coverting into zip file--------------------------------------------------------------------------------------------------------------------------------------------
            string physicalpath = txtsourcepath.Text; //I:\CompanyIISCode\35\IJobcenterMaster\WWW
            if (StepId == 5)
            {               
                try
                {
                    if (File.Exists(physicalpath + ".zip"))
                    {
                        File.Delete(physicalpath + ".zip");
                    }
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddDirectory(physicalpath);
                        zip.Save(physicalpath + ".zip");
                    }
                    StepId++;
                }
                catch (Exception ex)
                {
                   // StepId++;
                    lblmsg.Text = ex.ToString();
                    return;
                }
            }
            //Step-6----Create Directory in temp path --------------------------------------------------------------------------------------------------------------------------------------------           
            if (StepId == 6)
            {
                try
                {
                    if (!Directory.Exists(txttemppath.Text + "\\" + newfoldername)) //D:\ALLNEWCOMPANYMASTERCOPY\35\IJobcenterMaster\WWW\Temp\  1112017115255
                    {
                        Directory.CreateDirectory(txttemppath.Text + "\\" + newfoldername);
                    }
                    StepId++;
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.ToString();
                    return;
                }
            }
            string temppath = txttemppath.Text + newfoldername; //D:\ALLNEWCOMPANYMASTERCOPY\35\IJobcenterMaster\WWW\Temp\1112017115255
            string outputpath = txtoutputsourcepath.Text + newfoldername + "\\" + insidefoldername + "\\" + insidefoldername;//D:\ALLNEWCOMPANYMASTERCOPY\35\IJobcenterMaster\WWW\PublishCode\1112017115255\10090_WWW_14133_1\10090_WWW_14133_1
            outputpath = outputpath.Replace("\\\\","\\"); 

            //Step-7----Compile Option--------------------------------------------------------------------------------------------------------------------------------------------
            if (StepId == 7)
            {
                StepId++;
                if (chk_compiler.Checked == true)
                {                           //StepA 8 To 12
                    WithCompilerCreateVersion(filename, newfoldername, codetypeid, physicalpath, outputpath, temppath, insidefoldername, dateformat);
                }
                else
                {                           //StepB 8 To 12
                    WithoutCompilerCreateVersion(filename, newfoldername, codetypeid, physicalpath, outputpath, temppath, insidefoldername, dateformat);
                }
            }
            //Step-13--Insert Version Table----------------------------------------------------------------------------------------------------------------------------------------------
            if (StepId == 13)
            {
                try
                {
                    if (System.IO.File.Exists(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + ".zip"))
                    {
                        InsertInVersionTable(filename, newfoldername, codetypeid, physicalpath, txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + ".zip");
                        lblmsg.Text = "New version created successfully";
                        clear();
                        StepId++;
                       // btn_syncser.Visible = true;
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Error in compilation";
                    }
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.ToString();
                    return;
                }
            }           
        }
        protected void InsertInVersionTable(string filename, string newfoldername, string codetypeid, string physicalpath, string outputpath)
        {
           //Step-14----From ClientMaster table get ClientSourcepath------------------------------------------------------------------------------------------  
            string strcln = " SELECT  dbo.ServerMasterTbl.folderpathformastercode FROM  dbo.ClientMaster INNER JOIN dbo.ServerMasterTbl ON dbo.ClientMaster.ServerId = dbo.ServerMasterTbl.Id where ClientMaster.ClientMasterId='" + Session["ClientId"].ToString() + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                string defaltdrivepath = dtcln.Rows[0]["folderpathformastercode"].ToString() + "\\" + ddlproductname.SelectedValue;
               // string companyname = dtcln.Rows[0]["CompanyName"].ToString();
                string mastersourcefilepath = defaltdrivepath;
                string Latestcodeclientpath = "";
                //Step-15----Copy File at ClientSourcepath----------------------------------------------------------------------------------------------------   
                try
                {
                    defaltdrivepath = defaltdrivepath.Replace("\\\\","\\"); 
                    if (!Directory.Exists(mastersourcefilepath))
                    {
                        Directory.CreateDirectory(mastersourcefilepath);
                    }                    
                    string outputpathforcopy = txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + filename;
                     Latestcodeclientpath = mastersourcefilepath + "\\" + filename;
                    //File.Copy(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + filename, mastersourcefilepath, true);
                    File.Copy(outputpathforcopy, Latestcodeclientpath, true);                     
                }
                catch
                {
                   // return;
                }
                //Step-16----Insert In Table----------------------------------------------------------------------------------------------------   
                try
                {
                    Insert_ProductMasterCodeTbl_and_ProductMasterCodeonsatelliteserverTbl_WithServerLevel(Convert.ToInt32(ddlproductname.SelectedValue), Convert.ToInt32(codetypeid), Convert.ToInt32(lblnewcodetypeNo.Text), filename, mastersourcefilepath, Latestcodeclientpath);//txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + filename
                }
                catch
                {
                   // return;
                }
                try
                {
                    Insert_ProductMasterLatestcodeversioninfoTBl_and_WebsitePublish_and_ProductMasterLatestcodeversioninfoTBl_WithServerLevel(Convert.ToInt32(ddlproductname.SelectedValue), Convert.ToInt32(codetypeid), Convert.ToInt32(lblnewcodetypeNo.Text), filename, mastersourcefilepath, txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + filename, Latestcodeclientpath);
                }
                catch
                {
                   // return;
                }
            }        
        }

        protected void WithCompilerCreateVersion(string filename, string newfoldername, string codetypeid, string physicalpath, string outputpath, string temppath, string insidefoldername, string dateformat)
        {
            //Step-8-A---Orininath path created .zip file extract to temp path define location--------------------------------------------------------------------------           
            if (StepId == 8)
            {
                try
                {
                    using (ZipFile zip = ZipFile.Read(physicalpath + ".zip"))
                    {
                        zip.ExtractAll(temppath, ExtractExistingFileAction.OverwriteSilently);
                    }
                    File.Delete(physicalpath + ".zip");
                    StepId++;
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.ToString();
                    return;
                }
            }
            //Step-9-A---Delete Unwanted_Extra_Folder At temp location-------------------------------------------------------------------------------------------------
            if (StepId == 9)
            {
                try
                {
                    Delet_Unwanted_Extra_Folder(temppath, ddlcodetype.SelectedValue);
                    StepId++;
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.ToString();
                    return;
                }
            }
            //Step-10-A---Compile Temp folder and copy at output path location-----------------------------------------------------------------------------------------
            if (StepId == 10)
            {
                try
                {
                    string keypath = " -keyfile  C:\\Program Files (x86)\\Microsoft Visual Studio 9.0\\VC\\123.snk";
                    string aptca = " -aptca I:\\DElete\\New\\A";
                    DirectoryInfo sourceDir = new DirectoryInfo(txttemppath.Text + "\\" + newfoldername);
                    double size = GetSizeDirectory(sourceDir);
                    double twpercsize = 0.20 * size;
                    string mspath = "C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\";
                    string mscompiler = "aspnet_compiler.exe";
                    string fullcompilerpath = Path.Combine(mspath, mscompiler);
                    ProcessStartInfo startinfo = new ProcessStartInfo();
                    string virtualfilename = "/" + dateformat;
                    string argument = "-p " + temppath + " -v " + virtualfilename + " -u -f " + outputpath + " -fixednames -errorstack";
                   // Process.Start(fullcompilerpath, argument).WaitForExit();//.WaitForExit()   
                    //Process p = new Process(); 
                    //p.StartInfo.FileName = fullcompilerpath;
                    //p.StartInfo.Arguments = argument;
                    //p.Start();
                    //p.WaitForExit();

                    /*
                     
                     */
                    Process process = new Process();
                    StringBuilder outputStringBuilder = new StringBuilder();

                    try
                    {
                        process.StartInfo.FileName = fullcompilerpath;                       
                        process.StartInfo.Arguments = argument;
                        process.StartInfo.RedirectStandardError = true;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.UseShellExecute = false;
                        process.EnableRaisingEvents = false;
                        process.OutputDataReceived += (sender, eventArgs) => outputStringBuilder.AppendLine(eventArgs.Data);
                        process.ErrorDataReceived += (sender, eventArgs) => outputStringBuilder.AppendLine(eventArgs.Data);
                        process.Start();
                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();
                        var processExited = process.WaitForExit(1000 * 60 * 1);
                        if (processExited == false) // we timed out...
                        {
                            process.Kill();                           
                        }
                        else if (process.ExitCode != 0)
                        {
                            var output = outputStringBuilder.ToString();
                            var prefixMessage = "";

                            Label6.Text="Process exited with non-zero exit code of: " + process.ExitCode + Environment.NewLine +"<br>  Output from process: <br>" + outputStringBuilder.ToString();
                            Session["Error"] = "Process exited with non-zero exit code of: " + process.ExitCode + Environment.NewLine + "<br>  Output from process: <br>" + outputStringBuilder.ToString();
                            Response.Redirect("ProductCodeVersionUpdate.aspx");  
                            //throw new Exception("Process exited with non-zero exit code of: " + process.ExitCode + Environment.NewLine +
                            //"Output from process: " + outputStringBuilder.ToString());

                        }
                    }
                    catch
                    {
                        process.Close();
                    }
                    







                    DirectoryInfo outdir = new DirectoryInfo(outputpath);
                    double outfoldersize = GetSizeOutDirectory(outdir);
                    double twpercsizepub =  outfoldersize / 1024;
                    twpercsizepub =  outfoldersize / 1024;
                    twpercsizepub = outfoldersize / 1024;                    

                    if (twpercsizepub < 1)
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "website publish folder is empty problem in compiletion project (" + outputpath + ")";
                        return;
                    }
                    else
                    {
                        StepId++;
                        lblmsg.Text = "";
                    }                    
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.ToString();
                    return;
                }
            }
            //Step-11-A--Change web.config file-------------------------------------------------------------------------------------------------------------------------
            if (StepId == 11)
            {
                try
                {
                    //File.Delete(outputpath + "\\Web.Config");
                   //Create_WebConfig(outputpath);
                    AddUpdateConnectionString("WebconfigCompanyServerConnectionstring",outputpath);
                    //File.Move("I:\\CompanyIISCode\\35\\MasterWebConfig\\Web.Config", outputpath + "\\Web.Config");
                    StepId++;
                }
                catch (Exception ex)
                {
                    StepId++;
                }
            }
           //Step-12-A--After Compile output folder convert into zip file-----------------------------------------------------------------------------------------------
            if (StepId == 12)
            {
                try
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddDirectory(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername);
                        zip.Save(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + ".zip");
                    }                    
                    StepId++;
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.ToString();
                    return;
                }
            }            
        }
       //public double GetSizeOutDirectory(DirectoryInfo source)
       // {

       //     FileInfo[] files = source.GetFiles();
       //     foreach (FileInfo file in files)
       //     {
       //         sizeout += file.Length;
       //     }          
       //     DirectoryInfo[] dirs = source.GetDirectories();
       //     foreach (DirectoryInfo dir in dirs)
       //     {

       //         GetSizeOutDirectory(dir);
       //     }
       //     return sizeout;
       // }
      
      
        protected void Create_WebConfig(string appcodepath)
        {
            string HashKey = "";

            string fileLoc = appcodepath + "\\Web.Config";

    
                                    using (StreamWriter sw = new StreamWriter(fileLoc))
                                        sw.Write
                                            (@" <?xml version='"+"1.0"+"'?> "+
                            " <configuration> "+
                            "  <configSections> "+
                             "   <section name='"+"secureWebPages"+"' type='"+"Hyper.Web.Security.SecureWebPageSectionHandler,WebPageSecurity"+"' allowLocation='"+"false"+"' /> "+
                             " </configSections> "+
                             " <secureWebPages enabled='"+"true"+"'> "+
                             "   <directory path='"+"ShoppingCart"+"' /> "+
                              "  <file path='"+"ShoppingCart/default1.aspx"+"' /> "+
                               " <file path='"+"ShoppingCart/default1.aspx"+"' ignore='"+"True"+"' /> "+
                              "</secureWebPages> "+
                              "<appSettings> "+
                               " <add key='"+"CrystalImageCleaner-AutoStart"+"' value='"+"true"+"' /> "+
                                "<add key='"+"CrystalImageCleaner-Sleep"+"' value='"+"60000"+"' /> "+
                                "<add key='"+"CrystalImageCleaner-Age"+"' value='"+"120000"+"' /> "+
                                "<add key='"+"aspnet:MaxHttpCollectionKeys"+"' value='"+"5001"+"' /> "+
                                "<add key='"+"encriptcomid"+"' value='"+"gdgdfgdgfdgfdggdgdg"+"' /> "+
                                "<add key='"+"encriptserid"+"' value='"+"gdfgdfgfdgdfgdgdgg"+"' /> "+
                              "</appSettings> "+
                              "<connectionStrings> "+
                               " <add name='"+"WebconfigCompanyServerConnectionstring"+"' connectionString='"+"Data Source=C3\\gdfgdfgdgdg,fgdfgdg;Initial Catalog=gfgdfgdgdfgdfg;Integrated Security=False;User ID=sa;Password=gfdgdgdg"+"' providerName='"+"System.Data.SqlClient"+"' /> "+
                              "</connectionStrings>   "+
                              "<system.web> "+
                                "<customErrors mode='"+"Off"+"' defaultRedirect='"+"~/Errorpage.aspx"+"'>   "+
                                "</customErrors> "+
                                "<sessionState mode='"+"InProc"+"' cookieless='"+"false"+"' timeout='"+"10000"+"' regenerateExpiredSessionId='"+"true"+"' /> "+
                                "<xhtmlConformance mode='"+"Transitional"+"' /> "+
                                "<compilation debug='"+"true"+"' targetFramework='"+"4.5"+"'> "+
                                  "<assemblies> "+
                                    "<add assembly='"+"Microsoft.SqlServer.ConnectionInfoExtended, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"+"' /> "+
                                    "<add assembly='"+"Microsoft.SqlServer.ConnectionInfo, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"+"' />   "+
                                    "<add assembly='"+"Microsoft.SqlServer.Management.Sdk.Sfc, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"+"' /> "+
                                    "<add assembly='"+"Microsoft.SqlServer.Smo, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"+"' /> "+
                                    "<add assembly='"+"Microsoft.Build.Conversion.v3.5, Version=3.5.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"+"' /> "+
                                    "<add assembly='"+"Microsoft.Build.Tasks, Version=2.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"+"' /> "+
                                    "<add assembly='"+"Microsoft.Build.Utilities, Version=2.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"+"' /> "+
                                    "<add assembly='"+"Microsoft.Build.Utilities.v3.5, Version=3.5.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"+"' /> "+
                                    "<add assembly='"+"Microsoft.Office.Interop.Word, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71E9BCE111E9429C"+"' /> "+
                                    "<add assembly='"+"System.Data.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089"+"' /> "+
                                    "<add assembly='"+"System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"+"' /> "+
                                    "<add assembly='"+"System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"+"' /> "+
                                    "<add assembly='"+"System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089"+"' /> "+
                                    "<add assembly='"+"ISymWrapper, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"+"' /> "+
                                    "<add assembly='"+"Microsoft.Build.Engine, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"+"' /> "+
                                    "<add assembly='"+"Microsoft.Build.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"+"' /> "+
                                    "<add assembly='"+"Microsoft.JScript, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"+"' /> "+
                                    "<add assembly='"+"System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089"+"' /> "+
                                  "</assemblies> "+
                                  "<buildProviders> "+
                                    "<add extension='"+".rdlc"+"' type='"+"Microsoft.Reporting.RdlBuildProvider, Microsoft.ReportViewer.Common, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"+"' /> "+
                                  "</buildProviders> "+
                                "</compilation> "+
                                "<pages validateRequest='"+"false"+"' enableEventValidation='"+"false"+"' controlRenderingCompatibilityVersion='"+"3.5"+"' clientIDMode='"+"AutoID"+"' /> "+
                                "<httpHandlers> "+
                                  "<add path='"+"Reserved.ReportViewerWebControl.axd"+"' verb='"+"*"+"' type='"+"Microsoft.Reporting.WebForms.HttpHandler, Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"+"' validate='"+"false"+"' /> "+
                                  "<add path='"+"IndigoEightPdf.axd"+"' verb='"+"GET,POST"+"' type='"+"IndigoEight.Web.UI.AjaxPdfWriter"+"' validate='"+"false"+"' /> "+
                               " </httpHandlers> "+
                              "  <httpModules> "+
                              "  </httpModules> "+
                              "  <httpRuntime executionTimeout='"+"1200"+"' maxRequestLength='"+"102400"+"' useFullyQualifiedRedirectUrl='"+"false"+"' minFreeThreads='"+"8"+"' minLocalRequestFreeThreads='"+"4"+"' appRequestQueueLimit='"+"100"+"' /> "+ 
                               " <authentication mode='"+"Windows"+"' /> "+ 
                             " </system.web> "+
                             " <system.webServer> "+
                             "   <validation validateIntegratedModeConfiguration='"+"false"+"' /> "+
                             "   <handlers> "+
                                "  <add name='"+"CrystalImageHandler.aspx_GET"+"' verb='"+"GET"+"' path='"+"CrystalImageHandler.aspx"+"' type='"+"CrystalDecisions.Web.CrystalImageHandler, CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"+"' preCondition='"+"integratedMode"+"' /> "+
                             "   </handlers> "+
                             "   <defaultDocument enabled='"+"true"+"'> "+
                             "     <files> "+
                              "      <clear /> "+
                               "     <add value='"+"Shoppingcart/Admin/ShoppingcartLogin.aspx"+"' /> "+
                                "  </files> "+
                             "   </defaultDocument> "+
                             "   <tracing> "+
                              "    <traceFailedRequests> "+
                            "      </traceFailedRequests> "+
                               " </tracing>   "+
                             " </system.webServer>   "+
                             " <runtime>   "+
                            "  </runtime>   "+
                              "<system.net> "+
                                "<mailSettings> "+      
                                "</mailSettings> "+
                              "</system.net> "+
                            "</configuration> ");
                    
        }
        private void AddUpdateConnectionString(string name,string pathe)
        {
            bool isNew = false;
            string pathh = pathe + "//Web.Config";
            XmlDocument doc = new XmlDocument();
            doc.Load(pathh);
            XmlNodeList list = doc.DocumentElement.SelectNodes(string.Format("connectionStrings/add[@name='{0}']", name));
            XmlNode node;
            isNew = list.Count == 0;
            if (isNew)
            {
                node = doc.CreateNode(XmlNodeType.Element, "add", null);
                XmlAttribute attribute = doc.CreateAttribute("name");
                attribute.Value = name;
                node.Attributes.Append(attribute);

                attribute = doc.CreateAttribute("connectionString");
                attribute.Value = "";
                node.Attributes.Append(attribute);

                attribute = doc.CreateAttribute("providerName");
                attribute.Value = "System.Data.SqlClient";
                node.Attributes.Append(attribute);
            }
            else
            {
                node = list[0];
            }
            string conString = node.Attributes["connectionString"].Value;
            SqlConnectionStringBuilder conStringBuilder = new SqlConnectionStringBuilder(conString);
            conStringBuilder.InitialCatalog = "";
            conStringBuilder.DataSource = "";
            conStringBuilder.IntegratedSecurity = false;
            conStringBuilder.UserID = "";
            conStringBuilder.Password = "";

            node.Attributes["connectionString"].Value = conStringBuilder.ConnectionString;
            if (isNew)
            {
                doc.DocumentElement.SelectNodes("connectionStrings")[0].AppendChild(node);
            }
            doc.Save(pathh);
        }
        protected void WithoutCompilerCreateVersion(string filename, string newfoldername, string codetypeid, string physicalpath, string outputpath, string temppath, string insidefoldername, string dateformat)
        {
            //Step-8-B---Orininath path created .zip file extract to Output path define location--------------------------------------------------------------------------           
            if (StepId == 8)
            {
                try
                {
                    using (ZipFile zip = ZipFile.Read(physicalpath + ".zip"))
                    {
                        //zip.ExtractAll(temppath, ExtractExistingFileAction.OverwriteSilently);
                        zip.ExtractAll(outputpath, ExtractExistingFileAction.OverwriteSilently);
                    }
                    File.Delete(physicalpath + ".zip");
                    StepId++;
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.ToString();
                    return;
                }
            }
            //Step-9-B---Delete Unwanted_Extra_Folder At output location-------------------------------------------------------------------------------------------------
           if (StepId == 9)
            {
            try
            {
                Delet_Unwanted_Extra_Folder(outputpath, ddlcodetype.SelectedValue);
                StepId++;
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.ToString();
            }   
           }
            //Step-10-B---Skip Compile-------------------------------------------------------------------------------------------------------------------------------------------------------            
           if (StepId == 10)
           {
               try
               {
                   StepId++;
               }
               catch (Exception ex)
               {

               }
           }
            //Step-11-B--Change web.config file-------------------------------------------------------------------------------------------------------------------------            
           if (StepId == 11)
           {
               try
               {
                  //File.Delete(outputpath + "\\Web.Config");
                  //Create_WebConfig(outputpath);     
                   AddUpdateConnectionString("WebconfigCompanyServerConnectionstring", outputpath);
                   StepId++;
               }
               catch (Exception ex)
               {
                   StepId++;
               }
           }
            //Step-12-B--After Compile output folder convert into zip file-----------------------------------------------------------------------------------------------
           if (StepId == 12)
           {
               try
               {
                   using (ZipFile zip = new ZipFile())
                   {
                       zip.AddDirectory(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername);
                       zip.Save(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + ".zip");
                   }
                   StepId++;
               }
               catch (Exception ex)
               {
                   lblmsg.Text = ex.ToString();
                   return;
               }
           }
            //double outfoldersize = GetSizeOutDirectory(outdir);           
            //DirectoryInfo outdir = new DirectoryInfo(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername);           
        }
       
    
    //protected DataTable selectBZ(string str)
    //    {
    //        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
    //        DataTable dtclnccdweb = new DataTable();
    //        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
    //        adpclnccdweb.Fill(dtclnccdweb);
    //        return dtclnccdweb;
    //    }

      
       
        protected void ddlcodetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillpath();
            findnewcodeversion();
            if (ddlcodetypecatefory.SelectedValue == "1")
            {
                string strcln1 = " SELECT dbo.WebsiteMaster.WebsiteName + '-' + dbo.CodeTypeTbl.Name AS names, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.Name, dbo.CodeTypeTbl.CodeTypeCategoryId, dbo.CodeTypeTbl.ProductVersionId, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.CodeTypeTbl.Active, dbo.CodeTypeTbl.Temppath, dbo.CodeTypeTbl.Outputpath, dbo.CodeTypeTbl.WebsiteID, dbo.CodeTypeTbl.FileLocationPath, dbo.CodeTypeTbl.FileName, dbo.CodeTypeTbl.IsforDownloadonly, dbo.CodeTypeTbl.Instancename FROM dbo.WebsiteMaster INNER JOIN dbo.CodeTypeTbl ON dbo.WebsiteMaster.ID = dbo.CodeTypeTbl.WebsiteID INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id Where dbo.ProductCodeDetailTbl.Id=" + ddlcodetype.SelectedValue + " ";
                SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
                DataTable dtcln1 = new DataTable();
                SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
                adpcln1.Fill(dtcln1);
                if (dtcln1.Rows.Count > 0)
                {
                    txtsourcepath.Text = dtcln1.Rows[0]["FileLocationPath"].ToString();
                    txttemppath.Text = dtcln1.Rows[0]["Temppath"].ToString();
                    txtoutputsourcepath.Text = dtcln1.Rows[0]["Outputpath"].ToString();

                    txtoutputsourcepath.Text = txtoutputsourcepath.Text.Replace("\\\\", "\\");
                    txtsourcepath.Text = txtsourcepath.Text.Replace("\\\\", "\\");
                    txttemppath.Text = txttemppath.Text.Replace("\\\\", "\\");
                    ViewState["id"] = dtcln1.Rows[0]["id"].ToString();
                    try
                    {
                       //  = "";
                        DirectoryInfo sourceDir = new DirectoryInfo(txtsourcepath.Text);
                        double size = GetSizeDirectory(sourceDir);
                        double twpercsize =size / 1024;
                        twpercsize = twpercsize / 1024;
                        lblsize.Text = Convert.ToString(twpercsize) + "Mb";  
                    }
                    catch
                    {
                    }
                }
            }            
        }
        public void Insert_ProductMasterCodeTbl(int ProductVerID, int CodeTypeID, int codeversionnumber, string filename, string physicalpath, string TemporaryPath, Boolean successfully, Boolean CreatedUsingCompliler)
        {
            try
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("ProductMasterCodeTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                cmd.Parameters.AddWithValue("@ProductVerID", ProductVerID);
                cmd.Parameters.AddWithValue("@CodeTypeID", CodeTypeID);
                cmd.Parameters.AddWithValue("@codeversionnumber", codeversionnumber);
                cmd.Parameters.AddWithValue("@filename", filename);
                cmd.Parameters.AddWithValue("@physicalpath", physicalpath);
                cmd.Parameters.AddWithValue("@TemporaryPath", TemporaryPath);
                cmd.Parameters.AddWithValue("@Successfullycreated", successfully);
                cmd.Parameters.AddWithValue("@VersionDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreatedUsingCompliler", CreatedUsingCompliler);
                cmd.ExecuteNonQuery();
               
            }
            catch
            {
                
            }
        }
        protected void Insert_ProductMasterCodeTbl_and_ProductMasterCodeonsatelliteserverTbl_WithServerLevel(int ProductVerID, int CodeTypeID, int codeversionnumber, string filename, string physicalpath, string TemporaryPath)
        {
            TemporaryPath = TemporaryPath.Replace("\\\\","\\");
            physicalpath = physicalpath.Replace("\\\\", "\\"); 
            Boolean successfully = true;
            Insert_ProductMasterCodeTbl(ProductVerID, CodeTypeID, codeversionnumber, filename, physicalpath, TemporaryPath, successfully, chk_compiler.Checked);
           
        }
      
        protected void Insert_ProductMasterLatestcodeversioninfoTBl_and_WebsitePublish_and_ProductMasterLatestcodeversioninfoTBl_WithServerLevel(int ProductVerID, int codetypeid, int codeversionnumber, string filename, string physicalpath, string TemporaryPath, string outputpath)
        {
            TemporaryPath = TemporaryPath.Replace("\\\\", "\\");
            physicalpath = physicalpath.Replace("\\\\", "\\"); 

                 SqlCommand cmdsq = new SqlCommand("Insert into ProductMasterLatestcodeversioninfoTBl(Productveriontbl,CodeVersion,CodeTypeID)Values('" + ddlproductname.SelectedValue + "','" + lblnewcodetypeNo.Text + "','" + codetypeid + "')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdsq.ExecuteNonQuery();
                con.Close();

                SqlCommand cmdsxwebpub = new SqlCommand("Insert into WebsitePublish(CodeTypeId,ProductVersionId,DateTime,SourcePath,OutputPath,OutPutfileZipName,codeversionnumber) Values('" + codetypeid + "','" + ddlproductname.SelectedValue + "','" + DateTime.Now.ToString() + "','" + physicalpath + "','" + outputpath + "','" + filename + "','" + lblnewcodetypeNo.Text + "')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdsxwebpub.ExecuteNonQuery();
                con.Close();
              
        }

        protected void SyncroniceLastRecord()
        {
            DataTable dtrsc_PMC = MyCommonfile.selectBZ(" select Max(ID) as ID from ProductMasterCodeTbl ");//where CodeTypeID='" + CodeTypeID + "' 
            if (dtrsc_PMC.Rows.Count > 0)
            {               
                DataTable dtgetserverid = MyCommonfile.selectBZ(" SELECT id as ServerId  from ServerMasterTbl where Status=1  and id IN (select ServerId as id From CompanyMaster Where Active=1) ");
                if (dtgetserverid.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtgetserverid.Rows)
                    {
                        string serverid = dr["ServerId"].ToString();                        
                        DataTable dtftpdetail = MyCommonfile.selectBZ(" SELECT * from ServerMasterTbl where Id='" + serverid + "' and Status=1  and id IN (select ServerId as id From CompanyMaster Where Active=1 and ServerId=" + serverid + ") ");
                        if (dtftpdetail.Rows.Count > 0)
                        {
                            string ftpphysicalpath = dtftpdetail.Rows[0]["folderpathformastercode"].ToString();
                            string serversqlserverip = dtftpdetail.Rows[0]["sqlurl"].ToString();
                            string serversqlinstancename = dtftpdetail.Rows[0]["DefaultsqlInstance"].ToString();
                            string serversqldbname = dtftpdetail.Rows[0]["DefaultDatabaseName"].ToString();
                            string serversqlpwd = dtftpdetail.Rows[0]["Sapassword"].ToString();
                            string serversqlport = dtftpdetail.Rows[0]["port"].ToString();
                            connserver = new SqlConnection();
                            connserver.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";
                            try
                            {

                                 string strcln = " SELECT  * From ProductMasterCodeonsatelliteserverTbl where ProductMastercodeID='" + dtrsc_PMC.Rows[0]["ID"].ToString() + "' ";
                                 SqlCommand cmdcln = new SqlCommand(strcln, connserver);
                                 DataTable dtcln = new DataTable();
                                 SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                                 adpcln.Fill(dtcln);
                                 if (dtcln.Rows.Count > 0)
                                 {
                                     ftpphysicalpath = ftpphysicalpath + "\\" + dtrsc_PMC.Rows[0]["filename"].ToString();
                                     string strsatelliteserverinsert = " Insert into ProductMasterCodeonsatelliteserverTbl(ProductMastercodeID,ServerID,Successfullyuploadedtoserver,Physicalpath,filename) values ('" + dtrsc_PMC.Rows[0]["ID"].ToString() + "','" + serverid + "','0','" + ftpphysicalpath + "','" + dtrsc_PMC.Rows[0]["filename"].ToString() + "')";
                                     SqlCommand cmdsatelliteserverinsert = new SqlCommand(strsatelliteserverinsert, con);
                                     if (con.State.ToString() != "Open")
                                     {
                                         con.Open();
                                     }
                                     cmdsatelliteserverinsert.ExecuteNonQuery();
                                     con.Close();
                                 } 

                                DataTable dtrsc = MyCommonfile.selectBZ("Select Max(ID) as ID  from ProductMasterCodeonsatelliteserverTbl where ServerID='" + serverid + "' and ProductMastercodeID='" + dtrsc_PMC.Rows[0]["ID"].ToString() + "' ");
                                string strserverinsert = " Insert into ProductMasterCodeonsatelliteserverTbl(ID,ProductMastercodeID,ServerID,Successfullyuploadedtoserver,Physicalpath,filename,DownloadStart,DownloadFinish) values ('" + dtrsc.Rows[0]["ID"].ToString() + "','" + dtrsc_PMC.Rows[0]["ID"].ToString() + "','" + serverid + "','0','" + ftpphysicalpath + "','" + dtrsc_PMC.Rows[0]["filename"].ToString() + "','0','0')";
                                SqlCommand cmdserverinsert = new SqlCommand(strserverinsert, connserver);
                                if (connserver.State.ToString() != "Open")
                                {
                                    connserver.Open();
                                }
                                cmdserverinsert.ExecuteNonQuery();
                                connserver.Close();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }


            //-----------------------------------------------------------------------------------------------------------------------------------------------
            DataTable dtrsc_PMCINFO = MyCommonfile.selectBZ(" Select Max(Id) as Id,Productveriontbl,CodeTypeID,CodeVersion  from ProductMasterLatestcodeversioninfoTBl   ");          //where Productveriontbl='" + ddlproductname.SelectedValue + "' and  CodeVersion='" + lblnewcodetypeNo.Text + "' and CodeTypeID='" + CodeTypeID + "' Group by Productveriontbl,CodeTypeID,CodeVersion
            DataTable dtgetserverid_serid = MyCommonfile.selectBZ("SELECT id as ServerId  from ServerMasterTbl where  Status=1  and id IN (select ServerId as id From CompanyMaster Where Active=1)  ");
            if (dtgetserverid_serid.Rows.Count > 0 && dtrsc_PMCINFO.Rows.Count > 0 && Convert.ToString(dtrsc_PMCINFO.Rows[0]["Id"]) != "")
            {
                foreach (DataRow dr in dtgetserverid_serid.Rows)
                {
                    string serverid = dr["ServerId"].ToString();
                    string strftpdetail = " SELECT * from ServerMasterTbl where Id='" + serverid + "' and Status=1  and id IN (select ServerId as id From CompanyMaster Where Active=1 and ServerId=" + serverid + ")  ";
                    SqlCommand cmdftpdetail = new SqlCommand(strftpdetail, con);
                    DataTable dtftpdetail = new DataTable();
                    SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
                    adpftpdetail.Fill(dtftpdetail);
                    if (dtftpdetail.Rows.Count > 0)
                    {
                        string serversqlserverip = dtftpdetail.Rows[0]["sqlurl"].ToString();
                        string serversqlinstancename = dtftpdetail.Rows[0]["DefaultsqlInstance"].ToString();
                        string serversqldbname = dtftpdetail.Rows[0]["DefaultDatabaseName"].ToString();
                        string serversqlpwd = dtftpdetail.Rows[0]["Sapassword"].ToString();
                        string serversqlport = dtftpdetail.Rows[0]["port"].ToString();

                        connserver = new SqlConnection();
                        connserver.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";
                        ddlproductname.SelectedIndex = ddlcodetype.SelectedIndex = ddlcodetypecatefory.SelectedIndex = -1;
                        txtsourcepath.Text = txttemppath.Text = txtoutputsourcepath.Text = lblnewcodetypeNo.Text = "";
                        try
                        {
                            SqlCommand cmdsx = new SqlCommand("Insert into ProductMasterLatestcodeversioninfoTBl(Id,Productveriontbl,CodeVersion,CodeTypeID)Values('" + Convert.ToString(dtrsc_PMCINFO.Rows[0]["Id"]) + "','" + Convert.ToString(dtrsc_PMCINFO.Rows[0]["Productveriontbl"]) + "','" + Convert.ToString(dtrsc_PMCINFO.Rows[0]["CodeVersion"]) + "','" + Convert.ToString(dtrsc_PMCINFO.Rows[0]["CodeTypeID"]) + "')", connserver);
                            if (connserver.State.ToString() != "Open")
                            {
                                connserver.Open();
                            }
                            cmdsx.ExecuteNonQuery();
                            connserver.Close();
                        }
                        catch (Exception ex)
                        {
                            // lblmsg.Text = ex.ToString();
                        }
                    }
                }
            }
        }
        private static string Encrypt(string strtxt, string strtoencrypt)
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
                return strtxt;
                //  throw ex;
            }

        }
        public static string Encrypted(string strText)
        {

            return Encrypt(strText, encstr);

        }
        
        protected void ddlcodetypecatefory_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillpath();
            fillcodetype();
        }

        static double GetSizeDirectory(DirectoryInfo source)
        {

            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }
            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();

            foreach (DirectoryInfo dir in dirs)
            {

                GetSizeDirectory(dir);
            }
            return size;
        }

        static double GetSizeOutDirectory(DirectoryInfo source)
        {

            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                sizeout += file.Length;
            }
            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();

            foreach (DirectoryInfo dir in dirs)
            {

                GetSizeOutDirectory(dir);
            }
            return sizeout;
        }


    //-----

        private bool isValidConnection(string url, string user, string password, string port)
        {
            try
            {
                string[] separator1 = new string[] { "/" };
                string[] strSplitArr1 = url.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

                String productno = strSplitArr1[0].ToString();
                string ftpurl = "";

                if (productno == "FTP:" || productno == "ftp:")
                {
                    if (strSplitArr1.Length >= 3)
                    {
                        ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(port);
                        for (int i = 2; i < strSplitArr1.Length; i++)
                        {
                            ftpurl += "/" + strSplitArr1[i].ToString();
                        }
                    }
                    else
                    {
                        ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(port);

                    }
                }
                else
                {
                    if (strSplitArr1.Length >= 2)
                    {
                        ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(port);
                        for (int i = 1; i < strSplitArr1.Length; i++)
                        {
                            ftpurl += "/" + strSplitArr1[i].ToString();
                        }
                    }
                    else
                    {
                        ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(port);

                    }

                }
                string ftphost = ftpurl;



                // FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftphost);

                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(user, password);
                request.GetResponse();
            }
            catch (WebException ex)
            {
                return false;
            }
            return true;
        }


        protected void Delet_Unwanted_Extra_Folder(string temppath,string codetypeid)
        {
            try
            {
                DataTable dtsedata = MyCommonfile.selectBZ("Select * From ProductCodeVersionDetailDeleteFolder Where CodeTypeID='" + codetypeid + "'");
                foreach (DataRow item in dtsedata.Rows)
                {
                    String foldername = item["VersionDeleteFolderPath"].ToString();
                    
                    try
                    {
                        Directory.Delete(temppath + "\\" + foldername, true);
                    }
                    catch
                    {
                    }
                    try
                    {
                        Directory.Delete(temppath + foldername, true);
                    }
                    catch
                    {
                    }
                    
                }
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\ShoppingCart\\Admin\\VersionFolder", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\ShoppingCart\\Developer", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\Party\\VersionFolder", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\_vti_cnf", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\Account\\1133", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\VersionFolder", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\Attach", true);
                //File.Delete(txttemppath.Text + "\\" + newfoldername + "\\bin\\businesssetup.dll");
                //File.Delete(txttemppath.Text + "\\" + newfoldername + "\\bin\\documentrule.dll");
                //File.Delete(txttemppath.Text + "\\" + newfoldername + "\\bin\\presensenote.dll");
                //Directory.Delete(temppath + "\\ShoppingCart\\Admin\\VersionFolder", true);
                //Directory.Delete(temppath + "\\ShoppingCart\\Developer", true);
                //Directory.Delete(temppath + "\\Party\\VersionFolder", true);
                //Directory.Delete(temppath + "\\_vti_cnf", true);
                //Directory.Delete(temppath + "\\Account", true);
                //Directory.Delete(temppath + "\\VersionFolder", true);
                //Directory.Delete(temppath + "\\Attach", true);
                //Directory.Delete(temppath + "\\_vti_cnf", true);

            }
            catch (Exception ex)
            {
                // lblmsg.Text = ex.ToString();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            fillgrid();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
            int j = Convert.ToInt32(row.RowIndex);
            Label assess = (Label)grdcompiledproduct.Rows[j].FindControl("lblproductcodeid");
            ViewState["assid"] = assess.Text;
            
            string te = "Codeversionprofile.aspx?ID=" + ViewState["assid"] + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillgrid();

        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillgrid();
        }


        protected void chkday_CheckedChanged(object sender, EventArgs e)
        {
            if (chkday.Checked == true)
            {
                txt_day.Visible = true;
            }
            else
            {
                txt_day.Visible = false;
            }
        }
        protected void btn_showdetail_Click(object sender, EventArgs e)
        {
            //
            if (btn_showdetail.Text == "Show More Details")
            {
                pnl_showdetail.Visible = true;
                btn_showdetail.Text = "Hide More Details";
            }
            else if (btn_showdetail.Text == "Hide More Details")
            {
                pnl_showdetail.Visible = false;
                btn_showdetail.Text = "Show More Details";
            }
        }


}