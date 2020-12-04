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
using System.Data.SqlClient;
using System.DirectoryServices;
using System.IO.Compression;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Security.Cryptography;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;


public partial class ProductUpdateLastt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection conn;
    SqlConnection connserver;
    SqlConnection connNewDB = new SqlConnection();

    SqlConnection conmaster = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static string encstr = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //*************
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
           
            ViewState["sortOrder"] = "";
            if (Request.QueryString["ClientId"] == null )
            {
                //Session["ClientId"] = Request.QueryString["Clid"];              
            }
           
                FillProduct();
                FillDatabaseGrid();
                fillcodetypecategory();
                fillCodetype();

                fillfilterproduct();
                fillfiltercodetype();
                
                ddlcodetype_SelectedIndexChanged(sender, e);
                FillGrid();

                FilFTP();
            
        }
    }
    protected void Btncancle_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;       
        txtoutputsourcepath.Text = "";
        txtmdffilename.Text = "";
        txtldffilename.Text = "";
        txtldffilepath.Text = "";
        txttemppath.Text = "";
        txtoutputsourcepath.Text = "";
        FillProduct();
        fillcodetypecategory();
        fillCodetype();
        fillfilterproduct();
        fillfiltercodetype();
        ddlcodetype_SelectedIndexChanged(sender, e);
        lblmsg.Text = "";      
    }

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
    protected void FillProduct()
    {
        string strcln = " SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName,ProductMaster.ProductName +':'+ VersionInfoMaster.VersionInfoName as aa,VersionInfoMaster.VersionInfoId  FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  and VersionInfoMaster.Active='1'  order  by ProductName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlproductversion.DataSource = dtcln;
        ddlproductversion.DataValueField = "VersionInfoId";
        ddlproductversion.DataTextField = "ProductName";
        ddlproductversion.DataBind();            
    }  
    protected void ddlproductversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCodetype();
        fillpath();
        FillDatabaseGrid();        
    }
    protected void fillpath()
    {
        DataTable dtcln = MyCommonfile.selectBZ("SELECT  dbo.ProductMaster.Description,dbo.ProductMaster.ProductName,dbo.VersionInfoMaster.ClientFTPRootPath, dbo.VersionInfoMaster.VersionInfoId, dbo.VersionInfoMaster.VersionInfoName, dbo.VersionInfoMaster.ProductId, dbo.VersionInfoMaster.Active, dbo.VersionInfoMaster.ProductURL, dbo.VersionInfoMaster.PricePlanURL, dbo.VersionInfoMaster.MasterCodeSourcePath, dbo.VersionInfoMaster.TemporaryPublishPath, dbo.VersionInfoMaster.DestinationPath FROM dbo.VersionInfoMaster INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId where VersionInfoId='" + ddlproductversion.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            // txtsourcepath.Text = dtcln.Rows[0]["MasterCodeSourcePath"].ToString();
            // txttemppath.Text = dtcln.Rows[0]["TemporaryPublishPath"].ToString();
            // txtoutputsourcepath.Text = dtcln.Rows[0]["DestinationPath"].ToString();
            //  txtsourcepath.Text = dtcln.Rows[0]["ClientFTPRootPath"].ToString() + "\\" + dtcln.Rows[0]["ProductName"].ToString();
            txt_prod_desc.Text = dtcln.Rows[0]["Description"].ToString();
        }
    }
    protected void fillcodetypecategory()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" select * from CodeTypeCategory where CodeMasterNo In ('2') ");
        ddlcodetypecatefory.DataSource = dtcln;
        ddlcodetypecatefory.DataValueField = "CodeMasterNo";
        ddlcodetypecatefory.DataTextField = "CodeTypeCategory";
        ddlcodetypecatefory.DataBind();

        ddlCategorySearch.DataSource = dtcln;
        ddlCategorySearch.DataValueField = "CodeMasterNo";
        ddlCategorySearch.DataTextField = "CodeTypeCategory";
        ddlCategorySearch.DataBind();
    }
    protected void ddlcodetypecatefory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCodetype();
        // fillcodeversion();
    }
    
    protected void fillCodetype()
    {
        string strcln = "";
        strcln = " SELECT DISTINCT TOP (100) PERCENT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName,dbo.CodeTypeTbl.CodeTypeCategoryId FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id  where  ProductCodeDetailTbl.Active='1' and CodeTypeTbl.ProductVersionId='" + ddlproductversion.SelectedValue + "' and CodeTypeCategory.Id='" + ddlcodetypecatefory.SelectedValue + "'  order  by dbo.ProductCodeDetailTbl.CodeTypeName ";
        // SELECT DISTINCT TOP (100) PERCENT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName,dbo.CodeTypeTbl.CodeTypeCategoryId FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id WHERE (dbo.ProductCodeDetailTbl.Active = '1') AND (dbo.CodeTypeTbl.CodeTypeCategoryId = '2') ORDER BY dbo.ProductCodeDetailTbl.CodeTypeName
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlcodetype.DataSource = dtcln;
        ddlcodetype.DataValueField = "Id";
        ddlcodetype.DataTextField = "CodeTypeName";
        ddlcodetype.DataBind();
        ddlcodetype.Items.Insert(0, "--Select--");
        ddlcodetype.Items[0].Value = "0";
        ddlcodetype.SelectedIndex = 0;

    }
    protected void ddlcodetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcodeversion();
        FillDatabaseMDFMDFFile();
        CheckDAtabaseConn(ddlcodetype.SelectedValue);
    }
    protected void CheckDAtabaseConn(string ProductCodeDetailId)
    {
        string strwebsitewe = " SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ProductCodeDetailId + "'";
        SqlCommand cmd12webwe = new SqlCommand(strwebsitewe, con);
        SqlDataAdapter adp12webwe = new SqlDataAdapter(cmd12webwe);
        DataTable ds12webwe = new DataTable();
        adp12webwe.Fill(ds12webwe);
        if (ds12webwe.Rows.Count > 0)
        {
            string serversqlinstancename = ds12webwe.Rows[0]["Instancename"].ToString();
            string serverid = ds12webwe.Rows[0]["ServerId"].ToString();
            try
            {
                //lbl_databaseid.Text = databaseid;
                string finalstr = " SELECT * FROM dbo.ServerMasterTbl Where Id='" + serverid + "'";
                SqlCommand cmd = new SqlCommand(finalstr, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    string serversqlserverip = ds.Rows[0]["sqlurl"].ToString();
                    //string serversqlinstancename = ddlinstance.SelectedItem.Text;
                    string serversqldbname = ddlcodetype.SelectedItem.Text;
                    string serversqlpwd = ds.Rows[0]["Sapassword"].ToString();
                    string serversqlport = ds.Rows[0]["port"].ToString();

                    string Sqlinstancename = ds.Rows[0]["Sqlinstancename"].ToString();
                    string DefaultsqlInstance = ds.Rows[0]["DefaultsqlInstance"].ToString();

                   
                    if (serversqlinstancename == Sqlinstancename)
                    {                        
                        connNewDB.ConnectionString = @"Data Source=C3\\BUSIWIZSQL1;Initial Catalog=License.Busiwiz;Integrated Security=True";
                        connNewDB.ConnectionString = @"Data Source=" + serversqlserverip + "\\" + Sqlinstancename + ";Initial Catalog=" + serversqldbname + ";Integrated Security=True";
                        //connNewDB = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);                        
                    }
                    else
                    {
                        connNewDB.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";
                    }
                   // connNewDB.ConnectionString = "Data Source =192.168.2.100,40000; Initial Catalog = " + serversqldbname + "; User ID=Sa; password=06De1963++; Persist Security Info=true;";
                    try
                    {
                        if (connNewDB.State.ToString() != "Open")
                        {
                            connNewDB.Open();
                        }
                        lblconne.Text = "√";
                        lblconne.ForeColor = System.Drawing.Color.Green;
                    }
                    catch (Exception ex)
                    {
                        lblconne.Text = "X";
                        lblconne.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception e1)
            {
            }
        }    

    }
    protected void FillDatabaseMDFMDFFile()
    {
        string strsearch = "";
        string strcln1 = " select * from CodeTypeTbl where ProductCodeDetailId=" + ddlcodetype.SelectedValue + " ORDER BY dbo.CodeTypeTbl.ID";
        SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
        DataTable dtcln1 = new DataTable();
        SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
        adpcln1.Fill(dtcln1);
        if (dtcln1.Rows.Count > 0)
        {
            txttemppath.Text = dtcln1.Rows[0]["Temppath"].ToString();
            txtoutputsourcepath.Text = dtcln1.Rows[0]["Outputpath"].ToString();
            txtmdffile.Text = dtcln1.Rows[0]["FileLocationPath"].ToString();
            txtmdffilename.Text = dtcln1.Rows[0]["FileName"].ToString();


            txtldffilepath.Text = dtcln1.Rows[1]["FileLocationPath"].ToString();
            txtldffilename.Text = dtcln1.Rows[1]["FileName"].ToString();
        }
    }

    //-***********Website Grid web
    //-***********Website Grid web
    protected void FillDatabaseGrid()
    {
        string strsearch = "";
        strsearch = " ";
        string strcln = " SELECT distinct ID, WebsiteName,Case when(ID IS NULL) then  cast ('1' as bit) else  cast('0' as bit) end as chk From dbo.WebsiteMaster INNER JOIN dbo.VersionInfoMaster ON dbo.WebsiteMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId  Where dbo.VersionInfoMaster.Active=1 " + strsearch + "  ";
        strcln = " SELECT DISTINCT dbo.ProductCodeDetailTbl.CodeTypeName, CASE WHEN (WebsiteID IS NULL) THEN CAST('1' AS bit) ELSE CAST('0' AS bit) END AS chk, dbo.ProductCodeDetailTbl.Id FROM dbo.CodeTypeTbl INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id Where ProductCodeDetailTbl.Active='1' and  dbo.ProductCodeDetailTbl.ProductId=" + ddlproductversion.SelectedValue + " and dbo.CodeTypeTbl.CodeTypeCategoryId='" + ddlcodetypecatefory.SelectedValue + "' order  by dbo.ProductCodeDetailTbl.CodeTypeName  ";
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
    protected void linkdow_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        int data = Convert.ToInt32(GridView2.DataKeys[rinrow].Value);  
      
        ddlcodetype.SelectedValue =Convert.ToString(data);
        ddlcodetype_SelectedIndexChanged(sender, e);
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
                      
            DataTable ds12web = MyCommonfile.selectBZ(" SELECT   TOP (1) dbo.ProductMasterCodeTbl.ID, dbo.ProductMasterCodeTbl.VersionDate, dbo.CodeTypeTbl.WebsiteID FROM dbo.ProductMasterCodeTbl INNER JOIN dbo.CodeTypeTbl ON dbo.ProductMasterCodeTbl.CodeTypeID = dbo.CodeTypeTbl.ID Where dbo.ProductMasterCodeTbl.CodeTypeID='" + lbl_codetypeid.Text + "'   ORDER BY dbo.ProductMasterCodeTbl.ID DESC ");
            if (ds12web.Rows.Count > 0)
            {
                DataTable ds12webVer = MyCommonfile.selectBZ(" SELECT TOP (1) dbo.PageMaster.PageId, dbo.PageMaster.PageTypeId, dbo.PageMaster.PageName, dbo.WebsiteSection.WebsiteMasterId, dbo.PageVersionTbl.Date, dbo.PageVersionTbl.Id " +
                     " FROM dbo.PageVersionTbl INNER JOIN dbo.PageMaster ON dbo.PageVersionTbl.PageMasterId = dbo.PageMaster.PageId INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId Where dbo.WebsiteSection.WebsiteMasterId='" + lblwebid.Text + "' ORDER BY dbo.PageVersionTbl.Id DESC ");
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
        FillDatabaseGrid();
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillDatabaseGrid();
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
   
    //-***************************
   
    //-***************************
    //---
    protected void FillGrid()
    {
        string stcper = "";
        if (FilterProductname.SelectedIndex > 0)
        {
            stcper += stcper + " and ProductMasterCodeTbl.ProductVerID ='" + FilterProductname.SelectedValue + "'";
        }
        if (ddlctype.SelectedIndex > 0)
        {
            stcper += stcper + " and dbo.ProductCodeDetailTbl.Id ='" + ddlctype.SelectedValue + "'";
        }
        stcper += " and  dbo.CodeTypeTbl.CodeTypeCategoryId=" + ddlCategorySearch.SelectedValue + "";
       DataTable dtsvr = MyCommonfile.selectBZ("SELECT DISTINCT TOP (200) dbo.ProductCodeDetailTbl.Id AS Expr1, dbo.ProductMasterCodeTbl.codeversionnumber AS CodeVersion, dbo.ProductMasterCodeTbl.ID, dbo.ProductMasterCodeTbl.codeversionnumber, dbo.ProductMasterCodeTbl.filename, dbo.ProductMasterCodeTbl.physicalpath AS FileLocation, dbo.ProductMasterCodeTbl.TemporaryPath, dbo.ProductMaster.ProductName, dbo.ProductMaster.ProductName + ':' + dbo.VersionInfoMaster.VersionInfoName AS VersionInfoName, dbo.ProductCodeDetailTbl.CodeTypeName " +
                             " FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ProductMasterCodeTbl ON dbo.ProductMasterCodeTbl.ProductVerID = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.CodeTypeTbl ON dbo.CodeTypeTbl.ID = dbo.ProductMasterCodeTbl.CodeTypeID INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id Where ProductMaster.ClientMasterId='" + Session["ClientId"] + "'" + stcper);
        GridView1.DataSource = dtsvr;
        GridView1.DataBind();
    }
    //***********************
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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
                lblstatus.ToolTip = "File not available at here :" + lbl_latestpath.Text;
            }
        }
    }
    //Filters
    protected void fillfilterproduct()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName,ProductMaster.ProductName +':'+ VersionInfoMaster.VersionInfoName as aa,VersionInfoMaster.VersionInfoId  FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  and VersionInfoMaster.Active='1'  order  by ProductName");            
        FilterProductname.DataSource = dtcln;
        FilterProductname.DataValueField = "VersionInfoId";
        FilterProductname.DataTextField = "ProductName";
        FilterProductname.DataBind();
        FilterProductname.Items.Insert(0, "All");
        FilterProductname.Items[0].Value = "0";
    }
    protected void FilterProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfiltercodetype();
        FillGrid();
    }
  
   protected void fillfiltercodetype()
    {
        DataTable dtcln =MyCommonfile.selectBZ("SELECT DISTINCT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where  ProductCodeDetailTbl.Active='1' and CodeTypeTbl.ProductVersionId='" + FilterProductname.SelectedValue + "' and dbo.CodeTypeTbl.CodeTypeCategoryId='"+ddlCategorySearch.SelectedValue +"'  order  by dbo.ProductCodeDetailTbl.CodeTypeName ");
        //strcln = " SELECT DISTINCT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where  ProductCodeDetailTbl.Active='1' and CodeTypeTbl.ProductVersionId='" + FilterProductname.SelectedValue + "' and CodeTypeCategory.Id='2' and dbo.ProductCodeDetailTbl.Id IN(Select CodeTypeID as id From ProductCodeDatabasDetail) order  by dbo.ProductCodeDetailTbl.CodeTypeName ";
        ddlctype.DataSource = dtcln;
        ddlctype.DataValueField = "Id";
        ddlctype.DataTextField = "CodeTypeName";
        ddlctype.DataBind();
        ddlctype.Items.Insert(0, "All");
        ddlctype.Items[0].Value = "0";
    }
   
   
    protected void Button2_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lblmsg.Text = "";
        lbllegend.Text = "";
    }
   
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        lbllegend.Text = "Add New Product Update";
        lblmsg.Text = "";
    }
    
  
    protected void fillcodeversion()
    {
        string code_detailtypeid = "";
        DataTable dtCodeVersionID =MyCommonfile.selectBZ("Select * From CodeTypeTbl Where ProductCodeDetailId=" + ddlcodetype.SelectedValue + "");
        if (dtCodeVersionID.Rows.Count > 0)
        {
            code_detailtypeid = dtCodeVersionID.Rows[0]["Id"].ToString();
        }
        DataTable dtv = MyCommonfile.selectBZ("select Max(codeversionnumber) as codeversionnumber from ProductMasterCodeTbl where CodeTypeID='" + code_detailtypeid + "' and ProductVerID='" + ddlproductversion.SelectedValue + "'");
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
    protected void Button2_Click1(object sender, EventArgs e)
    {
        FillGrid();
    }
   
   
    

    protected void btndosyncro_Clickpop(object sender, EventArgs e)
    {
        ModernpopSync.Show();
    }
    protected void btndosyncro_Click(object sender, EventArgs e)
    {
        int transf = 0;
        DataTable dt1 =MyCommonfile.selectBZ(" SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ( ClientProductTableMaster.TableName='CompanyProductUpdateStatusTbl' OR ClientProductTableMaster.TableName='ProductMasterLatestcodeversioninfoTBl'  )");
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string datetim = DateTime.Now.ToString();
                string arqid = dt1.Rows[i]["Id"].ToString();

                string str22 = "Insert Into SyncronisationrequiredTbl(SatelliteSyncronisationrequiringTablesMasterID,DateandTime)Values('" + arqid + "','" + Convert.ToDateTime(datetim) + "')";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmn = new SqlCommand(str22, con);
                cmn.ExecuteNonQuery();
                con.Close();

                DataTable dt121 = MyCommonfile.selectBZ("SELECT Max(ID) as ID from SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='" + arqid + "'");

                if (Convert.ToString(dt121.Rows[0]["ID"]) != "")
                {
                    DataTable dtcln = MyCommonfile.selectBZ("SELECT Distinct ServerMasterTbl.Id FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId    where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' ");

                    for (int j = 0; j < dtcln.Rows.Count; j++)
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }

                        string str223 = "Insert Into SateliteServerRequiringSynchronisationMasterTbl(SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + dt121.Rows[0]["ID"] + "','" + dtcln.Rows[j]["Id"] + "','0','" + DateTime.Now.ToString() + "')";
                        SqlCommand cmn3 = new SqlCommand(str223, con);
                        cmn3.ExecuteNonQuery();
                        con.Close();
                        transf = Convert.ToInt32(rdsync.SelectedValue);
                    }
                }


            }

        }
        else
        {

        }
        if (transf > 0)
        {
            string te = "SyncData.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
   
    //****************************************************************************************************************
    //****************************************************************************************************************
    //****************************************************************************************************************
    //****************************************************************************************************************
    

    protected void btn_VersionCreate_Click(object sender, EventArgs e)
    {        
        string MDF_OriginalPath = txtmdffile.Text + "\\" + txtmdffilename.Text;
        string LDF_OriginalPath = txtldffilepath.Text + "\\" + txtldffilename.Text;

        string MDF_Temppathforcopy = txttemppath.Text + "\\" + txtmdffilename.Text;       
        string LDF_Temppathforcopy = txttemppath.Text + "\\" + txtldffilename.Text;

        string MDF_OutputPath = txtoutputsourcepath.Text + "\\" + txtmdffilename.Text;
        string LDF_OutputPath = txtoutputsourcepath.Text + "\\" + txtldffilename.Text;
        int stepcount = 0;
          try
          {
                 Boolean Bool_DetachDAtabase= Detechdatabase(ddlcodetype.SelectedItem.Text,ddlcodetype.SelectedValue);
                 if (Bool_DetachDAtabase == true)
                 {
                     if (!Directory.Exists(txttemppath.Text))
                     {
                         Directory.CreateDirectory(txttemppath.Text);
                     }
                     if (!Directory.Exists(txttemppath.Text))
                     {
                         Directory.CreateDirectory(txttemppath.Text);
                     }
                     if (!Directory.Exists(txttemppath.Text))
                     {
                         Directory.CreateDirectory(txttemppath.Text);
                     }
                     try
                     {
                         File.Copy(txtmdffile.Text + "\\" + txtmdffilename.Text, txttemppath.Text + "\\" + txtmdffilename.Text, true);
                         File.Copy(txtldffilepath.Text + "\\" + txtldffilename.Text, txttemppath.Text + "\\" + txtldffilename.Text, true);
                         stepcount++;
                     }
                     catch(Exception ex)
                     {
                         lblmsg.Text += ex.ToString();
                     }
                     try
                     {
                         if(stepcount==1)
                         {
                         Boolean Bool_AttachDB = AttachDatabase(ddlcodetype.SelectedItem.Text, ddlcodetype.SelectedValue, txtmdffile.Text + "\\"  ,txtmdffilename.Text, txtldffilepath.Text + "\\" , txtldffilename.Text);
                         if (Bool_AttachDB == true)
                         {
                             stepcount++;
                         }
                         }                     
                     }
                     catch (Exception ex)
                     {
                         lblmsg.Text += ex.ToString();
                     }
                     try
                     {
                         if (stepcount == 2)
                         {
                             Boolean Bool_AttachDBTemp = AttachDatabase(ddlcodetype.SelectedItem.Text + "_temp", ddlcodetype.SelectedValue, txttemppath.Text + "\\", txtmdffilename.Text, txttemppath.Text + "\\", txtldffilename.Text);
                             if (Bool_AttachDBTemp == true)
                             {
                               //  DeleteextraTAbleRecord(ddlcodetype.SelectedValue);
                                 stepcount++;
                             }
                         }                     
                     }
                     catch (Exception ex)
                     {
                         lblmsg.Text += ex.ToString();
                     }
                     try
                     {
                         if (stepcount == 3)
                         {
                             Boolean Bool_DetachDAtabaseTemp = Detechdatabase(ddlcodetype.SelectedItem.Text + "_temp", ddlcodetype.SelectedValue);
                             stepcount++;
                         }
                     }
                     catch (Exception ex)
                     {
                         lblmsg.Text += ex.ToString();
                     }
                     try
                     {
                         if (stepcount == 4)
                         {
                             File.Copy(txttemppath.Text + "\\" + txtmdffilename.Text, txtoutputsourcepath.Text + "\\" + txtmdffilename.Text, true);
                             File.Copy(txttemppath.Text + "\\" + txtldffilename.Text, txtoutputsourcepath.Text + "\\" + txtldffilename.Text, true);
                             stepcount++;
                         }
                     }
                     catch (Exception ex)
                     {
                         lblmsg.Text += ex.ToString();
                     }                  
                 }
                 else
                 {
                     lblmsg.Text += "Problem when deteching database please try again";
                 }
                 if (stepcount == 5)
                 {
                     DataTable dtcln = MyCommonfile.selectBZ("SELECT  dbo.ServerMasterTbl.folderpathformastercode FROM  dbo.ClientMaster INNER JOIN dbo.ServerMasterTbl ON dbo.ClientMaster.ServerId = dbo.ServerMasterTbl.Id where ClientMaster.ClientMasterId='" + Session["ClientId"].ToString() + "'");
                     if (dtcln.Rows.Count > 0)
                     {
                         string defaltdrivepath = dtcln.Rows[0]["folderpathformastercode"].ToString() + "\\" + ddlproductversion.SelectedValue;
                         // string companyname = dtcln.Rows[0]["CompanyName"].ToString();
                         string mastersourcefilepath = defaltdrivepath;                      
                         defaltdrivepath = defaltdrivepath.Replace("\\\\", "\\");
                         string MDF_Mastersourcefilepath = defaltdrivepath + "\\" + txtmdffilename.Text;
                         string LDF_Mastersourcefilepath = defaltdrivepath + "\\" + txtldffilename.Text;
                         // Copy files
                         try
                         {
                             if (!Directory.Exists(defaltdrivepath))
                             {
                                 Directory.CreateDirectory(defaltdrivepath);
                             }
                             File.Copy(MDF_OutputPath, defaltdrivepath + "\\" + txtmdffilename.Text, true);
                             File.Copy(LDF_OutputPath, defaltdrivepath + "\\" + txtldffilename.Text, true);
                         }
                         catch (Exception ex)
                         {
                             lblmsg.Text = ex.ToString();
                         }
                         // end copy files  
                         try
                         {
                             string filename = "";
                             string codetypeid = "";
                             string MDFOrLDF = "";
                             DataTable dtsedata = MyCommonfile.selectBZ("Select * From CodeTypeTbl Where ProductCodeDetailId=" + ddlcodetype.SelectedValue + "");
                             foreach (DataRow item in dtsedata.Rows)
                             {
                                 //DataRow dtr2 = dtTemp.NewRow();
                                 filename = Convert.ToString(item["Name"]);
                                 codetypeid = Convert.ToString(item["Id"]);
                                 MDFOrLDF = GetLast3String(filename, 3);
                                 if (MDFOrLDF == "MDF")
                                 {
                                     InsertTAbleDetail(codetypeid, txtmdffilename.Text, MDF_OutputPath, MDF_Mastersourcefilepath);
                                 }
                                 if (MDFOrLDF == "LDF")
                                 {
                                     InsertTAbleDetail(codetypeid, txtldffilename.Text, LDF_OutputPath, LDF_Mastersourcefilepath);
                                 }
                             }
                             FillGrid();
                             fillcodeversion();
                             lblmsg.Visible = true;
                             Btncancle_Click(sender, e);
                             lblmsg.Text = "New version created successfully";
                         }
                         catch (Exception ex)
                         {
                             lblmsg.Text = ex.ToString();
                         }
                     }
                 }                 
           }
          catch (Exception ex)
          {
              lblmsg.Text = ex.ToString();
          }        
    }

    protected void DeleteextraTAbleRecord(string ProductCodeDetailId)
    {
        string strwebsitewe = " SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ProductCodeDetailId + "'";
        SqlCommand cmd12webwe = new SqlCommand(strwebsitewe, con);
        SqlDataAdapter adp12webwe = new SqlDataAdapter(cmd12webwe);
        DataTable ds12webwe = new DataTable();
        adp12webwe.Fill(ds12webwe);
        if (ds12webwe.Rows.Count > 0)
        {
            string serversqlinstancename = ds12webwe.Rows[0]["Instancename"].ToString();
            string serverid = ds12webwe.Rows[0]["ServerId"].ToString();
              string serversqldbname = ddlcodetype.SelectedItem.Text+"_Temp";
            try
            {                
                    SqlConnection connNewDB = new SqlConnection();
                    connNewDB = ServerWizard.ServerDatabaseFromInstanceTCP(serverid, serversqlinstancename, serversqldbname);                    
                    try
                    {
                        if (connNewDB.State.ToString() != "Open")
                        {
                            connNewDB.Open();
                        }
                        string strwebsite = " Select   dbo.ClientProductTableMaster.Id, dbo.ClientProductTableMaster.VersionInfoId, dbo.ClientProductTableMaster.TableName From ClientProductTableMaster Where Databaseid='" + ProductCodeDetailId + "' and id Not IN (Select TAbleid From ProductDatabaseRequiredDataInTable)";
                        SqlCommand cmd12web = new SqlCommand(strwebsite, con);
                        SqlDataAdapter adp12web = new SqlDataAdapter(cmd12web);
                        DataTable ds12web = new DataTable();
                        adp12web.Fill(ds12web);
                        foreach (DataRow dr in ds12web.Rows)
                        {
                            string TableName = dr["TableName"].ToString();
                            try
                            {
                                string st2 = " Delete from " + TableName + "";
                                SqlCommand cmd2 = new SqlCommand(st2, connNewDB);
                                if (connNewDB.State.ToString() != "Open")
                                {
                                    connNewDB.Open();
                                }
                                cmd2.ExecuteNonQuery();
                                connNewDB.Close();
                            }
                            catch
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }                
            }
            catch (Exception e1)
            {
            }
        }    

    }
    //Deteched Database
     //
    protected Boolean Detechdatabase(string databasename, string ProductCodeDetailId)
    {
        Boolean status = true;
        DataTable ds12webwe = MyCommonfile.selectBZ(" SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ProductCodeDetailId + "'");          
        if (ds12webwe.Rows.Count > 0)
        {
            string serversqlinstancename = ds12webwe.Rows[0]["Instancename"].ToString();
            string serverid = ds12webwe.Rows[0]["ServerId"].ToString();
            connNewDB = ServerWizard.ServerTwoInstanceTCPConncection(serverid,serversqlinstancename); 
            try
            {
                if (connNewDB.State.ToString() != "Open")
                {
                    connNewDB.Open();
                }
                //cmd = new SqlCommand(" DROP DATABASE [" + databasename + "] ", connNewDB);
                //cmd.ExecuteNonQuery();
                connNewDB.Close();


                if (connNewDB.State.ToString() != "Open")
                {
                    connNewDB.Open();
                }
                String sqlCommandText2 = @" alter database [" + databasename + "] set offline with rollback immediate ";
                SqlCommand sqlCommand2 = new SqlCommand(sqlCommandText2, connNewDB);
                sqlCommand2.ExecuteNonQuery();
                connNewDB.Close();

                if (connNewDB.State.ToString() != "Open")
                {
                    connNewDB.Open();
                }
                String sqlCommandText1 = @"DROP DATABASE [" + databasename + "]";
                SqlCommand sqlCommand1 = new SqlCommand(sqlCommandText1, connNewDB);
                sqlCommand1.ExecuteNonQuery();
                connNewDB.Close();
            }
            catch(Exception ex)
            {               
                connNewDB.Close();
                status = false;
                lblmsg.Text = ex.ToString(); 
            }
            }
        return status;
    }
    //Attched Database
    protected Boolean AttachDatabase(string databasename, string ProductCodeDetailId, string mdffilepath,string mdffile, string ldffilepath,string ldffile)
    {
        Boolean status = true;   
        try
        {      
             
             string strwebsitewe = " SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ProductCodeDetailId + "'";
             SqlCommand cmd12webwe = new SqlCommand(strwebsitewe, con);
             SqlDataAdapter adp12webwe = new SqlDataAdapter(cmd12webwe);
             DataTable ds12webwe = new DataTable();
             adp12webwe.Fill(ds12webwe);
             if (ds12webwe.Rows.Count > 0)
             {
                 string serversqlinstancename = ds12webwe.Rows[0]["Instancename"].ToString();
                 string serverid = ds12webwe.Rows[0]["ServerId"].ToString();
                 connNewDB = ServerWizard.ServerTwoInstanceTCPConncection(serverid, serversqlinstancename);
                 if (connNewDB.State.ToString() != "Open")
                 {
                     connNewDB.Open();
                 }
                 connNewDB.Close();

                 SqlCommand cmd = new SqlCommand("CREATE DATABASE [" + databasename + "] ON ( FILENAME = N'" + mdffilepath + mdffile + "' ),( FILENAME = N'" + ldffilepath + ldffile + "' ) FOR ATTACH", connNewDB);
                 if (connNewDB.State.ToString() != "Open")
                 {
                     connNewDB.Open();
                 }
                 cmd.ExecuteNonQuery();
                 connNewDB.Close();

                 if (connNewDB.State.ToString() != "Open")
                 {
                     connNewDB.Open();
                 }
                 String sqlCommandText = @"alter database [" + databasename + "] set online with rollback immediate ";
                 SqlCommand sqlCommand = new SqlCommand(sqlCommandText, connNewDB);
                 sqlCommand.ExecuteNonQuery();
                 connNewDB.Close();
             }
             else
             {
                 status = false;
             }
        }
        catch
        {
            status = false;
        }
      return  status;
    }
  

    protected void InsertTAbleDetail(string codetypeid,string filnames, string mastersourcefilepath, string filepath)
    {
        insert(Convert.ToInt32(ddlproductversion.SelectedValue), Convert.ToInt32(codetypeid), Convert.ToInt32(lblnewcodetypeNo.Text), filnames, mastersourcefilepath, filepath);

        SqlCommand cmdsq = new SqlCommand("Insert into ProductMasterLatestcodeversioninfoTBl(Productveriontbl,CodeVersion,CodeTypeID)Values('" + ddlproductversion.SelectedValue + "','" + lblnewcodetypeNo.Text + "','" + codetypeid + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdsq.ExecuteNonQuery();
        con.Close();


        DataTable dtrsc = MyCommonfile.selectBZ("Select Max(Id) as Id,Productveriontbl,CodeTypeID,CodeVersion  from ProductMasterLatestcodeversioninfoTBl where Productveriontbl='" + ddlproductversion.SelectedValue + "' and  CodeVersion='" + lblnewcodetypeNo.Text + "' and CodeTypeID='" + codetypeid + "' Group by Productveriontbl,CodeTypeID,CodeVersion");//ddlcodetypecatefory
        string strgetserverid = "select distinct ServerId from ServerAssignmentMasterTbl where VersionId='" + ddlproductversion.SelectedValue + "'";
        SqlCommand cmdgetserverid = new SqlCommand(strgetserverid, con);
        DataTable dtgetserverid = new DataTable();
        SqlDataAdapter adpgetserverid = new SqlDataAdapter(cmdgetserverid);
        adpgetserverid.Fill(dtgetserverid);
        if (dtgetserverid.Rows.Count > 0)
        {
            foreach (DataRow dr in dtgetserverid.Rows)
            {
                string serverid = dr["ServerId"].ToString();

                string strftpdetail = " SELECT * from ServerMasterTbl where Id='" + serverid + "'";
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

                    try
                    {
                        SqlCommand cmdsx = new SqlCommand("Insert into ProductMasterLatestcodeversioninfoTBl(Id,Productveriontbl,CodeVersion,CodeTypeID)Values('" + Convert.ToString(dtrsc.Rows[0]["Id"]) + "','" + Convert.ToString(dtrsc.Rows[0]["Productveriontbl"]) + "','" + Convert.ToString(dtrsc.Rows[0]["CodeVersion"]) + "','" + Convert.ToString(dtrsc.Rows[0]["CodeTypeID"]) + "')", connserver);
                        if (connserver.State.ToString() != "Open")
                        {
                            connserver.Open();
                        }
                        cmdsx.ExecuteNonQuery();
                        connserver.Close();
                    }
                    catch (Exception ex)
                    {

                    }
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
       
    //----------
    protected void insert(int ProductVerID, int CodeTypeID, int codeversionnumber, string filename, string physicalpath, string TemporaryPath)
    {
        Boolean successfully = true;
        Boolean usingcompiler = true;
        Insert_ProductMasterCodeTbl(ProductVerID, CodeTypeID, codeversionnumber, filename, physicalpath, TemporaryPath, successfully, usingcompiler);
        
        string strmaxid = "select Max(ID) as ID from ProductMasterCodeTbl where CodeTypeID='" + CodeTypeID + "' ";
        SqlCommand cmdmaxid = new SqlCommand(strmaxid, con);
        DataTable dtmaxid = new DataTable();
        SqlDataAdapter adpmaxid = new SqlDataAdapter(cmdmaxid);
        adpmaxid.Fill(dtmaxid);
        if (dtmaxid.Rows.Count > 0)
        {
            string strgetserverid = "select distinct ServerId from ServerAssignmentMasterTbl where VersionId='" + ProductVerID + "'";
            SqlCommand cmdgetserverid = new SqlCommand(strgetserverid, con);
            DataTable dtgetserverid = new DataTable();
            SqlDataAdapter adpgetserverid = new SqlDataAdapter(cmdgetserverid);
            adpgetserverid.Fill(dtgetserverid);
            if (dtgetserverid.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgetserverid.Rows)
                {
                    string serverid = dr["ServerId"].ToString();

                    string strftpdetail = " SELECT * from ServerMasterTbl where Id='" + serverid + "'";
                    SqlCommand cmdftpdetail = new SqlCommand(strftpdetail, con);
                    DataTable dtftpdetail = new DataTable();
                    SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
                    adpftpdetail.Fill(dtftpdetail);

                    if (dtftpdetail.Rows.Count > 0)
                    {
                        string ftpphysicalpath = dtftpdetail.Rows[0]["folderpathformastercode"].ToString() + "\\" + filename;

                        string serversqlserverip = dtftpdetail.Rows[0]["sqlurl"].ToString();
                        string serversqlinstancename = dtftpdetail.Rows[0]["DefaultsqlInstance"].ToString();
                        string serversqldbname = dtftpdetail.Rows[0]["DefaultDatabaseName"].ToString();
                        string serversqlpwd = dtftpdetail.Rows[0]["Sapassword"].ToString();
                        string serversqlport = dtftpdetail.Rows[0]["port"].ToString();

                        connserver = new SqlConnection();
                        connserver.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";

                        try
                        {
                            string strsatelliteserverinsert = "Insert into ProductMasterCodeonsatelliteserverTbl(ProductMastercodeID,ServerID,Successfullyuploadedtoserver,Physicalpath,filename) values ('" + dtmaxid.Rows[0]["ID"].ToString() + "','" + serverid + "','0','" + ftpphysicalpath + "','" + filename + "')";
                            SqlCommand cmdsatelliteserverinsert = new SqlCommand(strsatelliteserverinsert, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdsatelliteserverinsert.ExecuteNonQuery();
                            con.Close();

                            DataTable dtrsc =MyCommonfile.selectBZ("Select Max(ID) as ID  from ProductMasterCodeonsatelliteserverTbl where ServerID='" + serverid + "' and ProductMastercodeID='" + dtmaxid.Rows[0]["ID"].ToString() + "' ");

                            string strserverinsert = "Insert into ProductMasterCodeonsatelliteserverTbl(ID,ProductMastercodeID,ServerID,Successfullyuploadedtoserver,Physicalpath,filename,DownloadStart,DownloadFinish) values ('" + dtrsc.Rows[0]["ID"].ToString() + "','" + dtmaxid.Rows[0]["ID"].ToString() + "','" + serverid + "','0','" + ftpphysicalpath + "','" + filename + "','0','0')";
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
    public static string GetLast3String(string source, int last)
    {
        return last >= source.Length ? source : source.Substring(source.Length - last);
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
