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
using System.IO;

using System.Net;
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;

using System.Net.Security;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.DirectoryServices;
public partial class MyWork : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_PreInit(object sender, EventArgs e)
    {
       // this.MasterPageFile = "Master/Blank.master";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblVersion.Text = "This is V11 Updated on 15Dec2015 by ";
        if (!IsPostBack)
        {
           //Do Something
           fillemployee();
           //FillProducts();
           fillproduct();
           
           //fillpagload();                    
           //fillproduct();
         

           DateTime dto = DateTime.Now;
           string s_today = dto.ToString("MM/dd/yyyy"); // As String
           TextBox2.Text = Convert.ToString(s_today);
           // txtTo.Text = System.DateTime.Now.ToShortDateString();
           DateTime d = DateTime.Now;
         
           d = d.AddMonths(-1);

           string s_todaybe = d.ToString("MM/dd/yyyy"); // As String
           TextBox1.Text = Convert.ToString(s_todaybe);


           string strcln = " SELECT top(1) * from Mywork_SelectType where Employeeid='" + Session["EmpId"] + "' Order By id Desc  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            ddltypeofwork.SelectedValue = dtcln.Rows[0]["SelectValues"].ToString();   
        }

            fillgrid();
        }

    }
    protected void fillemployee()
    {
        string strcln = " SELECT * from EmployeeMaster where id='" + Session["EmpId"] + "'  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlemployee.DataSource = dtcln;
        ddlemployee.DataValueField = "Id";
        ddlemployee.DataTextField = "Name";
        ddlemployee.DataBind();
        ddlemployee.Enabled = false;
        //ddlemployee.Items.Insert(0, "-Select-");
        //ddlemployee.Items[0].Value = "0";
       // Session["id"] = 16043;

       // 16043 Nitya
        /*        16044 Sahee
        17045 Organi
         18047 Santos
        14043 manan
        12035 pk
        16046 fele
          */
        //if (Convert.ToInt32(Session["EmpId"]) > 0)
        //{
        //    string ss = Session["EmpId"].ToString();
        //   // ddlemployee.SelectedValue = ss;
        //    //ddlemployee.Enabled = false;
        //}
    }
    protected void ddlDeptName_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //FillVersion();
    }
    protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
       // Fillweb();
    }

    protected void fillproduct()
    {
        //string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMasterId, VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName  as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' order  by productversion ";
        // string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMasterId, VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName  as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' order  by productversion ";
        //string strcln = "SELECT  distinct WebsiteMaster.ID as WebsiteMasterId, VersionInfoMaster.VersionInfoId,'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId  inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName  inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId  where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' order  by productversion";

        string strcln = "SELECT DISTINCT WebsiteMaster.ID AS WebsiteMasterId, VersionInfoMaster.VersionInfoId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' + VersionInfoMaster.VersionInfoName + ' : ' + 'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName AS productversion FROM PageWorkTbl INNER JOIN PageVersionTbl ON PageWorkTbl.PageVersionTblId = PageVersionTbl.Id INNER JOIN ProductMaster INNER JOIN VersionInfoMaster ON ProductMaster.ProductId = VersionInfoMaster.ProductId INNER JOIN ProductDetail ON ProductDetail.VersionNo = VersionInfoMaster.VersionInfoName INNER JOIN WebsiteMaster ON WebsiteMaster.VersionInfoId = VersionInfoMaster.VersionInfoId INNER JOIN PageMaster ON VersionInfoMaster.VersionInfoId = PageMaster.VersionInfoMasterId ON PageVersionTbl.PageMasterId = PageMaster.PageId WHERE ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and    (PageWorkTbl.EpmloyeeID_AssignedSupervisor = '" + ddlemployee.SelectedValue + "') OR (PageWorkTbl.EpmloyeeID_AssignedTester = '" + ddlemployee.SelectedValue + "') OR (PageWorkTbl.EpmloyeeID_AssignedDeveloper = '" + ddlemployee.SelectedValue + "') order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlwebsite.DataSource = dtcln;
        ddlwebsite.DataValueField = "WebsiteMasterId";
        ddlwebsite.DataTextField = "productversion";
        ddlwebsite.DataBind();
        ddlwebsite.Items.Insert(0, "All");
    }
    //protected void FillProducts()
    //{

    //    string strcln2 = " SELECT        ProductId, ProductName FROM  dbo.ProductMaster WHERE        (ClientMasterId = '35')";
    //    SqlCommand cmdcln2 = new SqlCommand(strcln2, con);
    //    DataTable dtcln2 = new DataTable();
    //    SqlDataAdapter adpcln2 = new SqlDataAdapter(cmdcln2);
    //    adpcln2.Fill(dtcln2);

    //    ddl_product .DataSource = dtcln2;
    //    ddl_product.DataValueField = "ProductId";
    //    ddl_product.DataTextField = "ProductName";
    //    ddl_product.DataBind();
    //    ddl_product.Items.Insert(0, "---Select Product---");
    //}
    //protected void FillVersion()
    //{
    //    string strcln1 = "SELECT    VersionInfoId,  dbo.VersionInfoMaster.ProductId,  VersionInfoName,  VersionInfoName AS Expr1 FROM dbo.VersionInfoMaster WHERE  dbo.VersionInfoMaster.ProductId  ='" + ddl_product.SelectedValue + "'  and (dbo.VersionInfoMaster.Active = 'True')";
    //    SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
    //    DataTable dtcln1 = new DataTable();
    //    SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
    //    adpcln1.Fill(dtcln1);

    //    ddl_version.DataSource = dtcln1;
    //    ddl_version.DataValueField = "VersionInfoId";
    //    ddl_version.DataTextField = "Expr1";
    //    ddl_version.DataBind();
    //    ddl_version.Items.Insert(0, "---Select VersionName---");


    //}
    //protected void Fillweb()
    //{
    //    string strcln2 = "SELECT ID, WebsiteName FROM dbo.WebsiteMaster where VersionInfoId ='" + ddl_version.SelectedValue + "'";
    //    SqlCommand cmdcln2 = new SqlCommand(strcln2, con);
    //    DataTable dtcln2 = new DataTable();
    //    SqlDataAdapter adpcln2 = new SqlDataAdapter(cmdcln2);
    //    adpcln2.Fill(dtcln2);

    //    ddlwebsite.DataSource = dtcln2;
    //    ddlwebsite.DataValueField = "ID";
    //    ddlwebsite.DataTextField = "WebsiteName";
    //    ddlwebsite.DataBind();
    //    ddlwebsite.Items.Insert(0, "---Select WebsiteName---");

    //}
    //protected void fillproduct()
    //{
    //    //string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMasterId, VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName  as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' order  by productversion ";
    //    // string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMasterId, VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName  as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' order  by productversion ";
    //    string strcln = "SELECT  distinct WebsiteMaster.ID as WebsiteMasterId, VersionInfoMaster.VersionInfoId,'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId  inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName  inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId  where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' order  by productversion";
    //    SqlCommand cmdcln = new SqlCommand(strcln, con);
    //    DataTable dtcln = new DataTable();
    //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //    adpcln.Fill(dtcln);

    //    ddlwebsite.DataSource = dtcln;
    //    ddlwebsite.DataValueField = "WebsiteMasterId";
    //    ddlwebsite.DataTextField = "productversion";
    //    ddlwebsite.DataBind();

       
     
        
    
    
    //}
    protected void fillgridold()
    {
        
        string me="";

        if (ddlwebsite.SelectedValue.ToString() == "All")
        {

            string strcln = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId , dbo.PageMaster.PageName , dbo.PageMaster.FolderName,  dbo.WebsiteMaster.VersionFolderUrl from PageWorkTbl " +
                           " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                           "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
                           " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
                           " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
                           " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
                           " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddlemployee.SelectedValue + "' ";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                GridView2.DataSource = dtcln;
                GridView2.DataBind();

            }
        }
        else
        {
            string strcln = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId , dbo.PageMaster.PageName , dbo.PageMaster.FolderName,  dbo.WebsiteMaster.VersionFolderUrl from PageWorkTbl " +
                            " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                            "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
                            " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
                            " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
                            " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
                            " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddlemployee.SelectedValue + "' and WebsiteMaster.ID='" + ddlwebsite.SelectedValue + "' and PageWorkTbl.DevelopmentDone='" + ddlstatus.SelectedIndex + "' " + me + "  ";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                GridView2.DataSource = dtcln;
                GridView2.DataBind();

            }
        }

        // developer
        if ((TextBox1.Text == "" || TextBox2.Text == "") && ddlwebsite.SelectedValue.ToString() != "All")
        {
            string strcln = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId , dbo.PageMaster.PageName , dbo.PageMaster.FolderName,  dbo.WebsiteMaster.VersionFolderUrl from PageWorkTbl " +
                            " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                            "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
                            " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
                            " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
                            " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
                            " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddlemployee.SelectedValue + "' and WebsiteMaster.ID='" + ddlwebsite.SelectedValue + "' and PageWorkTbl.DevelopmentDone='" + ddlstatus.SelectedIndex + "'";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                GridView2.DataSource = dtcln;
                GridView2.DataBind();

            }

        }
        else if ((TextBox1.Text == "" || TextBox2.Text == "") && ddlwebsite.SelectedValue.ToString() != "All")
        {
            string strcln = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId , dbo.PageMaster.PageName , dbo.PageMaster.FolderName,  dbo.WebsiteMaster.VersionFolderUrl from PageWorkTbl " +
                             " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                             "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
                             " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
                             " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
                             " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
                             " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddlemployee.SelectedValue + "' and PageWorkTbl.TargetDateDeveloper between '" + TextBox1.Text + "' and '" + TextBox2.Text + "' and WebsiteMaster.ID='" + ddlwebsite.SelectedValue + "' and PageWorkTbl.DevelopmentDone='" + ddlstatus.SelectedIndex + "'  ";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                GridView2.DataSource = dtcln;
                GridView2.DataBind();

            }

        }


        if ((TextBox1.Text == "" || TextBox2.Text == "") && ddlwebsite.SelectedValue.ToString() != "All")
        {
            //tester
            string strcln12 = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId , dbo.PageMaster.PageName , dbo.PageMaster.FolderName , dbo.WebsiteMaster.VersionFolderUrl from PageWorkTbl " +
            " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
            "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
            " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
            " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
            " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
            " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.EpmloyeeID_AssignedTester='" + ddlemployee.SelectedValue + "'  and WebsiteMaster.ID='" + ddlwebsite.SelectedValue + "' and PageWorkTbl.TestingDone='" + ddlstatus.SelectedIndex + "'  ";

            SqlCommand cmdcln12 = new SqlCommand(strcln12, con);
            DataTable dtcln12 = new DataTable();
            SqlDataAdapter adpcln12 = new SqlDataAdapter(cmdcln12);
            adpcln12.Fill(dtcln12);

            if (dtcln12.Rows.Count > 0)
            {
                GridView2.DataSource = dtcln12;
                GridView2.DataBind();

            }

        }
        else if ((TextBox1.Text == "" || TextBox2.Text == "") && ddlwebsite.SelectedValue.ToString() != "All")
        {
            //tester
            string strcln12 = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId , dbo.PageMaster.PageName , dbo.PageMaster.FolderName , dbo.WebsiteMaster.VersionFolderUrl from PageWorkTbl " +
            " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
            "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
            " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
            " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
            " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
            " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.EpmloyeeID_AssignedTester='" + ddlemployee.SelectedValue + "' and PageWorkTbl.TargetDateTester between '" + TextBox1.Text + "' and '" + TextBox2.Text + "' and WebsiteMaster.ID='" + ddlwebsite.SelectedValue + "' and PageWorkTbl.TestingDone='" + ddlstatus.SelectedIndex + "'  ";

            SqlCommand cmdcln12 = new SqlCommand(strcln12, con);
            DataTable dtcln12 = new DataTable();
            SqlDataAdapter adpcln12 = new SqlDataAdapter(cmdcln12);
            adpcln12.Fill(dtcln12);

            if (dtcln12.Rows.Count > 0)
            {
                GridView2.DataSource = dtcln12;
                GridView2.DataBind();
               
            }


        }


        if ((TextBox1.Text == "" || TextBox2.Text == "") && ddlwebsite.SelectedValue.ToString() != "All")
        {
            //supervisor
            string strcln1234 = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId, dbo.PageMaster.PageName , dbo.PageMaster.FolderName , dbo.WebsiteMaster.VersionFolderUrl from PageWorkTbl " +
               " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
               "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
               " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
               " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
               " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
               " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.EpmloyeeID_AssignedSupervisor='" + ddlemployee.SelectedValue + "'  and WebsiteMaster.ID='" + ddlwebsite.SelectedValue + "' and PageWorkTbl.SupervisorCheckingDone='" + ddlstatus.SelectedIndex + "'    ";
            //dbo.PageVersionTbl.TesterOk=1 and 

            SqlCommand cmdcln1234 = new SqlCommand(strcln1234, con);
            DataTable dtcln1234 = new DataTable();
            SqlDataAdapter adpcln1234 = new SqlDataAdapter(cmdcln1234);
            adpcln1234.Fill(dtcln1234);

            if (dtcln1234.Rows.Count > 0)
            {
                GridView2.DataSource = dtcln1234;
                GridView2.DataBind();
                GridView2.Columns[9].Visible = false;
              

            }
        }
        else if ((TextBox1.Text == "" || TextBox2.Text == "") && ddlwebsite.SelectedValue.ToString() != "All")
        {
            //supervisor

            string strcln1234 = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId , dbo.PageMaster.PageName , dbo.PageMaster.FolderName,  dbo.WebsiteMaster.VersionFolderUrl from PageWorkTbl " +
               " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
               "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
               " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
               " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
               " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
               " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where  PageWorkTbl.EpmloyeeID_AssignedSupervisor='" + ddlemployee.SelectedValue + "' and PageWorkTbl.TargetDateSuperviserApproval between '" + TextBox1.Text + "' and '" + TextBox2.Text + "' and WebsiteMaster.ID='" + ddlwebsite.SelectedValue + "' and PageWorkTbl.SupervisorCheckingDone='" + ddlstatus.SelectedIndex + "'    ";

            SqlCommand cmdcln1234 = new SqlCommand(strcln1234, con);
            DataTable dtcln1234 = new DataTable();
            SqlDataAdapter adpcln1234 = new SqlDataAdapter(cmdcln1234);
            adpcln1234.Fill(dtcln1234);
            // dbo.PageVersionTbl.TesterOk=1 and 
            if (dtcln1234.Rows.Count > 0)
            {
                GridView2.DataSource = dtcln1234;
                GridView2.DataBind();
                GridView2.Columns[9].Visible = false;
               

            }

        }
        
        foreach (GridViewRow gdr in GridView2.Rows)
        {
            Label lblmasterId = (Label)gdr.FindControl("lblmasterId");
            Label lbldate12345 = (Label)gdr.FindControl("lbldate12345");
            Label lblbudgetd132 = (Label)gdr.FindControl("lblbudgetd132");
            Label lblactualhour = (Label)gdr.FindControl("lblactualhour");

            Label lblfileupload = (Label)gdr.FindControl("lblfileupload");


            string str123 = "select PageWorkTbl.* from PageWorkTbl  inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId   inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId    inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId    inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id    inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId    inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId  where PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddlemployee.SelectedValue + "' AND PageWorkTbl.ID = " + lblmasterId.Text;
            SqlCommand cmd123 = new SqlCommand(str123, con);
            DataTable dt123 = new DataTable();
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
            adp123.Fill(dt123);
            if (dt123.Rows.Count > 0)
            {
                DateTime MainStartDate;
                MainStartDate = Convert.ToDateTime(dt123.Rows[0]["TargetDateDeveloper"].ToString());


                lbldate12345.Text = MainStartDate.ToShortDateString();

                lblbudgetd132.Text = dt123.Rows[0]["BudgetedHourDevelopment"].ToString();

            }

            string str456 = "select PageWorkTbl.* from PageWorkTbl  inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId   inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId    inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId    inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id    inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId    inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId  where PageWorkTbl.EpmloyeeID_AssignedTester='" + ddlemployee.SelectedValue + "' AND PageWorkTbl.ID = " + lblmasterId.Text;
            SqlCommand cmd456 = new SqlCommand(str456, con);
            DataTable dt456 = new DataTable();
            SqlDataAdapter adp456 = new SqlDataAdapter(cmd456);
            adp456.Fill(dt456);
            if (dt456.Rows.Count > 0)
            {
                DateTime MainStartDate1;
                MainStartDate1 = Convert.ToDateTime(dt456.Rows[0]["TargetDateTester"].ToString());


                lbldate12345.Text = MainStartDate1.ToShortDateString();


                //  lbldate12345.Text = dt123.Rows[0]["TargetDateTester"].ToString();

                lblbudgetd132.Text = dt456.Rows[0]["BudgetedHourTesting"].ToString();

            }

            string str789 = "select PageWorkTbl.* from PageWorkTbl  inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId   inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId    inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId    inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id    inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId    inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId  where PageWorkTbl.EpmloyeeID_AssignedSupervisor='" + ddlemployee.SelectedValue + "' AND PageWorkTbl.ID = " + lblmasterId.Text;
            SqlCommand cmd789 = new SqlCommand(str789, con);
            DataTable dt789 = new DataTable();
            SqlDataAdapter adp789 = new SqlDataAdapter(cmd789);
            adp789.Fill(dt789);
            if (dt789.Rows.Count > 0)
            {
                DateTime MainStartDate2;
                MainStartDate2 = Convert.ToDateTime(dt789.Rows[0]["TargetDateSuperviserApproval"].ToString());

                //lbldate12345.Text = dt123.Rows[0]["BudgetedHourSupervisorChecking"].ToString();
                lbldate12345.Text = MainStartDate2.ToShortDateString();

                // lbldate12345.Text = dt123.Rows[0]["TargetDateSuperviserApproval"].ToString();
                lblbudgetd132.Text = dt789.Rows[0]["BudgetedHourSupervisorChecking"].ToString();

            }
            string stractual = " select sum(datepart(hour,convert(datetime,HourSpent)))  AS TotalHours,sum(datepart(minute,convert(datetime,HourSpent))) AS TotalMinutes from MyDailyWorkReport where PageWorkTblId='" + lblmasterId.Text + "' and EmployeeId='" + ddlemployee.SelectedValue + "' ";
            SqlCommand cmdactual = new SqlCommand(stractual, con);
            DataTable dtactual = new DataTable();
            SqlDataAdapter adpactual = new SqlDataAdapter(cmdactual);
            adpactual.Fill(dtactual);
            if (dtactual.Rows.Count > 0)
            {
                string FinalTime = "";
                Int32 in1 = 0;
                Int32 HourtoMinute1 = 0;
                Int32 Minute1 = 0;
                Int32 TotalMinutes132 = 0;
                Int32 FinalHours = 0;
                Int32 FinalMinute = 0;


                string TotalHour = dtactual.Rows[0]["TotalHours"].ToString();
                string TotalMinutes = dtactual.Rows[0]["TotalMinutes"].ToString();

                if (TotalHour == "")
                {

                }
                else
                {
                    in1 = Convert.ToInt32(TotalHour.ToString());
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(TotalMinutes.ToString());

                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                }


                FinalHours = (TotalMinutes132 / 60);
                FinalMinute = (TotalMinutes132 % 60);

                FinalTime = FinalHours + ":" + FinalMinute;
                lblactualhour.Text = FinalTime.ToString();

            }
            string strcount = " select COUNT(Id) As Count from PageFinishWorkUploadTbl where PageWorkTblId='" + lblmasterId.Text + "' ";
            SqlCommand cmdcount = new SqlCommand(strcount, con);
            DataTable dtcount = new DataTable();
            SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
            adpcount.Fill(dtcount);
            if (dtcount.Rows.Count > 0)
            {
                // lblfileupload.Text = dtcount.Rows[0]["Count"].ToString();
            }




        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView2.DataSource = null;
        GridView2.DataBind();

        string Insert = "Insert into Mywork_SelectType(Employeeid,SelectValues) values ('" + ddlemployee.SelectedValue + "','" + ddltypeofwork.SelectedValue + "')";
        SqlCommand cmdinsert = new SqlCommand(Insert, con);
        con.Open();
        cmdinsert.ExecuteNonQuery();
        con.Close();

        fillgrid();
    }
    protected void link1_Click(object sender, EventArgs e)
    {
        Session["empname"] = ddlemployee.SelectedValue;
        Session["txt1"] = TextBox1.Text;
        Session["txt2"] = TextBox2.Text;

        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;



        Label lblmasterId = (Label)GridView2.Rows[rinrow].FindControl("lblmasterId");
        Label lblworktitle12345 = (Label)GridView2.Rows[rinrow].FindControl("lblworktitle12345");
        Label lblbujtedhr = (Label)GridView2.Rows[rinrow].FindControl("lblbudgetd132");

        string strSQL = "SELECT PWT.EpmloyeeID_AssignedDeveloper, PWT.EpmloyeeID_AssignedSupervisor, PWT.EpmloyeeID_AssignedTester FROM PageWorkTbl AS PWT " +
                            "INNER JOIN PageVersionTbl AS PVT ON PVT.ID = PWT.PageVersionTblID " +
                            "WHERE PWT.ID = " + lblmasterId.Text;
        con.Open();
        SqlCommand cmd = new SqlCommand(strSQL, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtRoles = new DataTable();
        da.Fill(dtRoles);
        con.Close();
        string empid = "";
        string Emp_Developer = ""; string Emp_Supervisor = ""; string Emp_Tester = "";
        if (dtRoles.Rows.Count > 0)
        {
            empid = Convert.ToString(Session["id"]);
            Emp_Developer = Convert.ToString(dtRoles.Rows[0]["EpmloyeeID_AssignedDeveloper"]);
            Emp_Supervisor = Convert.ToString(dtRoles.Rows[0]["EpmloyeeID_AssignedSupervisor"]);
            Emp_Tester = Convert.ToString(dtRoles.Rows[0]["EpmloyeeID_AssignedTester"]);
        }

        string Emp_Role = "";
        if (Emp_Developer == empid)
        {
            Emp_Role = "Developer";
        }
        else if (Emp_Tester == empid)
        {
            Emp_Role = "Tester";
        }
        else if (Emp_Supervisor == empid)
        {
            Emp_Role = "Supervisor";
        }
        ViewState["UserType"] = Emp_Role;

        ViewState["Pageworktblid"] = lblmasterId.Text;

        string strverid = "select PageVersionTblID from PageWorkTbl WHERE ID = '" + lblmasterId.Text + "'";
        SqlCommand cmdver = new SqlCommand(strverid, con);
        DataTable dtver = new DataTable();
        SqlDataAdapter adpver = new SqlDataAdapter(cmdver);
        adpver.Fill(dtver);
        if (dtver.Rows.Count > 0)
        {
            ViewState["PageVersionTblID"] = dtver.Rows[0]["PageVersionTblID"].ToString();
        }

        lblpageworkId.Text = lblmasterId.Text;
        lblworltitleatreport.Text = lblworktitle12345.Text;
        lblnewbujtedhr.Text = lblbujtedhr.Text;
        //GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        //int rinrow = row.RowIndex;


        //Label lblmasterId = (Label)GridView2.Rows[rinrow].FindControl("lblmasterId");


        lblpageworkmasterId.Text = lblmasterId.Text;


        string strcount = " select WebsiteMaster.*,PageMaster.PageId,WebsiteSection.WebsiteSectionId,PageMaster.FolderName from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.Id='" + lblmasterId.Text + "'";
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);
        if (dtcount.Rows.Count > 0)
        {
            lblftpurl123.Text = dtcount.Rows[0]["FTP_Url"].ToString();
            lblftpport123.Text = dtcount.Rows[0]["FTP_Port"].ToString();
            lblftpuserid.Text = dtcount.Rows[0]["FTP_UserId"].ToString();
            lblftppassword123.Text = PageMgmt.Decrypted(dtcount.Rows[0]["FTP_Password"].ToString());
            ViewState["folder"] = dtcount.Rows[0]["FolderName"].ToString();
            ViewState["WebsiteSectionId"] = dtcount.Rows[0]["WebsiteSectionId"].ToString();
            ViewState["PageId"] = dtcount.Rows[0]["PageId"].ToString();
        }
        else
        {
            lblftpurl123.Text = "";
            lblftpport123.Text = "";
            lblftpuserid.Text = "";
            lblftppassword123.Text = "";
            ViewState["folder"] = "";
        }
        Session["GridFileAttach1"] = null;
        gridFileAttach.DataSource = null;
        gridFileAttach.DataBind();

        Session["GridFileDocu"] = null;
        Griddoc.DataSource = null;
        Griddoc.DataBind();

        TextBox3.Text = System.DateTime.Now.ToShortDateString();

        //ModalPopupExtender2.Show();





    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Button10_Click(Button10, e);

        string Insert = "Insert into MyDailyWorkReport(PageWorkTblId,Date,HourSpent,WorkDoneReport,EmployeeId,WorkDone,EmpRequestHour) values ('" + lblpageworkId.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + ddlemployee.SelectedValue + "','" + CheckBox1.Checked + "','" + txtactualhourrequired.Text + "')";
        SqlCommand cmdinsert = new SqlCommand(Insert, con);
        con.Open();
        cmdinsert.ExecuteNonQuery();
        con.Close();
        fillgrid();
        Label1.Visible = true;
        Label1.Text = "Record Inserted Succesfully";
        ModalPopupExtender2.Hide();
        lblMsg1.Text = "Record Inserted Succesfully";
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void link2_Click1(object sender, EventArgs e)
    {
        
    }
    protected void link2_Click(object sender, EventArgs e)
    {
        lblpaged.Visible = true;
        lblpaged.Text = "";
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;


        Label lblmasterId = (Label)GridView2.Rows[rinrow].FindControl("lblmasterId");
        Label lbldate12345 = (Label)GridView2.Rows[rinrow].FindControl("lbldate12345");
        Label lblbudgetd132 = (Label)GridView2.Rows[rinrow].FindControl("lblbudgetd132");
        Label lblactualhour = (Label)GridView2.Rows[rinrow].FindControl("lblactualhour");


        // string strcount = " select * from PageWorkTbl where Id='" + lblmasterId.Text + "'";
        string strcount = "select PageWorkTbl.*,WebsiteMaster.WebsiteName,PageMaster.PageTitle, PageMaster.PageName,PageVersionTbl.PageName as NewVersionName,PageVersionTbl.VersionNo from PageWorkTbl  inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId   inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId    inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId    inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id    inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId    inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.Id='" + lblmasterId.Text + "'";

        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);
        if (dtcount.Rows.Count > 0)
        {
            lblwebsitenamedetail.Text = dtcount.Rows[0]["WebsiteName"].ToString();
            lblpagenamedetail.Text = dtcount.Rows[0]["PageTitle"].ToString();
            lblworktitledetail.Text = dtcount.Rows[0]["WorkRequirementTitle"].ToString();

            lblnewpageversion.Text = dtcount.Rows[0]["VersionNo"].ToString();
            //lblnewpagedetaildetail.Text = dtcount.Rows[0]["NewVersionName"].ToString();
            lblnewpagedetaildetail.Text = dtcount.Rows[0]["PageName"].ToString();
            lblworkdescriptiondetail.Text = dtcount.Rows[0]["WorkRequirementDescription"].ToString();

            lblbudgetedhourdetail.Text = lblbudgetd132.Text;

            lbltargatedatedetail.Text = lbldate12345.Text;
            lblactualhourdetail.Text = lblactualhour.Text;

            string str1231 = " select * from PageWorkGuideUploadTbl where PageWorkTblId='" + lblmasterId.Text + "'";

            SqlCommand cmd1231 = new SqlCommand(str1231, con);
            DataTable dt1231 = new DataTable();
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd1231);
            adp123.Fill(dt1231);

            if (dt1231.Rows.Count > 0)
            {
                GridView1.DataSource = dt1231;
                GridView1.DataBind();
            }

            string strcount2 = " select PDSA.Id,Convert(nvarchar,PDSA.Date,101) as Date, 'pageversion' + RTRIM(PageVersionTbl.VersionNo) + '/' + PDSA.OriginalFileName AS PName, PDSA.FileName as upfile,PageMaster.FolderName,PageMaster.PageTitle,PageVersionTbl.VersionNo,PageVersionTbl.PageName from PageDevelopmentSourceCodeAllocateTable AS PDSA inner join PageWorkTbl on PageWorkTbl.Id=PDSA.PageWorkTblId inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.Id='" + lblmasterId.Text + "' order by PDSA.Id Desc";
            SqlCommand cmdcount2 = new SqlCommand(strcount2, con);
            DataTable dtcount2 = new DataTable();
            SqlDataAdapter adpcount2 = new SqlDataAdapter(cmdcount2);
            adpcount2.Fill(dtcount2);
            if (dtcount2.Rows.Count > 0)
            {

                grdsourcefile.DataSource = dtcount2;
                grdsourcefile.DataBind();
            }
        }

        ModalPopupExtender1.Show();

    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "mp3", "wma" };

        string ext = System.IO.Path.GetExtension(fileuploadadattachment.PostedFile.FileName);

        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + validFileTypes[i])
            {

                isValidFile = true;

                break;

            }

        }

        if (!isValidFile)
        {

            Label2.Visible = true;

            Label2.Text = "Invalid File. Please upload a File with extension " +

                           string.Join(",", validFileTypes);
            ModalPopupExtender2.Show();
        }

        else
        {


            String filename = "";

            if (fileuploadadattachment.HasFile)
            {
                filename = fileuploadadattachment.FileName;

                string strSQL = "SELECT FileName FROM PageDevelopmentSourceCodeAllocateTable WHERE PageWorkTblID = " + lblpageworkmasterId.Text;
                con.Open();
                DataTable dtFiles = new DataTable();
                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtFiles);
                con.Close();

                //if (filename.EndsWith(".aspx"))
                //{
                //    string NewFileName = Convert.ToString(dtFiles.Rows[0]["FileName"]);
                //    if (filename.ToString() == NewFileName.ToString())
                //    {
                //        Label2.Visible = false;
                //    }
                //    else
                //    {
                //        Label2.Visible = true;
                //        Label2.Text = "Filename should be the same file as you have downloaded.";
                //        ModalPopupExtender2.Show();
                //        return;
                //    }
                //}

                //if (filename.EndsWith(".aspx.cs"))
                //{
                //    string NewFIleName = Convert.ToString(dtFiles.Rows[1]["FileName"]);
                //    if (filename.ToString() == NewFIleName.ToString())
                //    {
                //        Label2.Visible = false;
                //    }
                //    else
                //    {
                //        Label2.Visible = true;
                //        Label2.Text = "Filename should be the same file as you have downloaded.";
                //        ModalPopupExtender2.Show();
                //        return;
                //    }
                //}

                //filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fileuploadadattachment.FileName;
                fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Attachment\\") + filename);




                DataTable dt = new DataTable();
                if (Session["GridFileAttach1"] == null)
                {
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "PDFURL";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);



                }
                else
                {
                    dt = (DataTable)Session["GridFileAttach1"];
                }
                DataRow dtrow = dt.NewRow();
                dtrow["PDFURL"] = filename;



                dt.Rows.Add(dtrow);
                Session["GridFileAttach1"] = dt;
                gridFileAttach.DataSource = dt;


                gridFileAttach.DataBind();
                ModalPopupExtender2.Show();
            }
            else
            {
                Label2.Visible = true;
                Label2.Text = "Please Attach File to Upload.";
                return;
            }
        }
    }
    
    public void ftpfile(string inputfilepath, string filename)
    {
        // string ftphost = "ftp://FTP.Eparcel.us/OnlineAcc/" + inputfilepath;


        string[] separator1 = new string[] { "/" };
        string[] strSplitArr1 = lblftpurl123.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

        String productno = strSplitArr1[0].ToString();
        string ftpurl = "";

        if (productno == "FTP:" || productno == "ftp:")
        {
            if (strSplitArr1.Length >= 3)
            {
                ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;
                for (int i = 2; i < strSplitArr1.Length; i++)
                {
                    ftpurl += "/" + strSplitArr1[i].ToString();
                }
            }
            else
            {
                ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;

            }
        }
        else
        {
            if (strSplitArr1.Length >= 2)
            {
                ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;
                for (int i = 1; i < strSplitArr1.Length; i++)
                {
                    ftpurl += "/" + strSplitArr1[i].ToString();
                }
            }
            else
            {
                ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;

            }

        }

        string strSQL = "SELECT * FROM PageDevelopmentSourceCodeAllocateTable WHERE PageWorkTblID = " + lblpageworkId.Text.Trim();
        con.Open();
        DataTable dtTemp = new DataTable();
        SqlCommand cmd = new SqlCommand(strSQL, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtTemp);
        con.Close();

        string data = "";
        if (inputfilepath.EndsWith(".aspx"))
        {
            data = Convert.ToString(dtTemp.Rows[0]["ID"]);
        }
        else
        {
            data = Convert.ToString(dtTemp.Rows[1]["ID"]);
        }

        string strcount = " select PDSA.FileName, PDSA.PageWorkTblId as PageWorkMasterId, PDSA.OriginalFileName, PageVersionTbl.VersionNo, " +
                        "WebsiteMaster.*,PageMaster.FolderName from PageDevelopmentSourceCodeAllocateTable AS PDSA " +
                        "inner join  PageWorkTbl  on PageWorkTbl.id=PDSA.PageWorkTblId " +
                        "inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                        "inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId " +
                        "inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  " +
                        "inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  " +
                        "inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  " +
                        "inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PDSA.Id='" + data + "'";
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);

        if (lblftpurl123.Text.Length > 0)
        {
            string[] seperator_RootPath = new string[] { "/" };
            string RootPath = Convert.ToString(dtcount.Rows[0]["RootFolderPath"]);
            string[] RootPathArray = RootPath.Split(seperator_RootPath, StringSplitOptions.RemoveEmptyEntries);
            string FolderName = "";
            for (int k = 2; k < RootPathArray.Length; k++)
            {
                FolderName += RootPathArray[k].ToString() + "/";
            }
            ViewState["folder"] = FolderName.ToString().Substring(0, FolderName.Length - 1);
            // string ftphost = lblftpurl123.Text + "/" + inputfilepath;
            //string ftphost =ftpurl + "/" + inputfilepath;
            string ftphost = ftpurl + "/" + ViewState["folder"] + "/Attachment/" + inputfilepath;
            FtpWebRequest FTP = (FtpWebRequest)FtpWebRequest.Create(ftphost);
            FTP.Credentials = new NetworkCredential(lblftpuserid.Text, lblftppassword123.Text);
            FTP.UseBinary = true;
            FTP.KeepAlive = true;
            FTP.UsePassive = true;

            FTP.Method = WebRequestMethods.Ftp.UploadFile;
            FileStream fs = File.OpenRead(filename);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
            Stream ftpstream = FTP.GetRequestStream();
            ftpstream.Write(buffer, 0, buffer.Length);
            ftpstream.Close();

            if (inputfilepath.EndsWith(".aspx") || inputfilepath.EndsWith(".aspx.cs"))
            {
                string VersionNo = Convert.ToString(dtcount.Rows[0]["VersionNo"]);
                string VersionFolderName = "pageversion" + VersionNo.ToString().Trim();
                string NewFileName = "";
                if (inputfilepath.EndsWith(".aspx"))
                {
                    NewFileName = Convert.ToString(dtTemp.Rows[0]["OriginalFileName"]);
                }
                else
                {
                    NewFileName = Convert.ToString(dtTemp.Rows[1]["OriginalFileName"]);
                }
                string ftphost_Ver = ftpurl + "/" + ViewState["folder"] + "/VersionFolder/" + VersionFolderName + "/" + NewFileName;
                FtpWebRequest FTP_Ver = (FtpWebRequest)FtpWebRequest.Create(ftphost_Ver);
                FTP_Ver.Credentials = new NetworkCredential(lblftpuserid.Text, lblftppassword123.Text);
                FTP_Ver.UseBinary = true;
                FTP_Ver.KeepAlive = true;
                FTP_Ver.UsePassive = true;

                FTP_Ver.Method = WebRequestMethods.Ftp.UploadFile;
                FileStream fs_Ver = File.OpenRead(filename);
                byte[] buffer_Ver = new byte[fs_Ver.Length];
                fs_Ver.Read(buffer_Ver, 0, buffer_Ver.Length);
                fs_Ver.Close();
                Stream ftpstream_Ver = FTP_Ver.GetRequestStream();
                ftpstream_Ver.Write(buffer_Ver, 0, buffer_Ver.Length);
                ftpstream_Ver.Close();
            }
        }
        System.IO.File.Delete(filename);
    }
    protected void Button10_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow gdr in gridFileAttach.Rows)
        {

            Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");

            string strftpinsert = "INSERT INTO PageFinishWorkUploadTbl(PageWorkTblId,FileName,Date) values ('" + lblpageworkmasterId.Text + "','" + lblpdfurl.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

            SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
            con.Open();
            cmdinsert.ExecuteNonQuery();
            con.Close();
            ftpfile(lblpdfurl.Text.ToString(), Server.MapPath("~\\Attachment\\") + lblpdfurl.Text.ToString());


            Label2.Visible = true;
            Label2.Text = "Record Inserted Succesfully";

        }

        foreach (GridViewRow gdr in Griddoc.Rows)
        {
            Label lbldocname = (Label)gdr.FindControl("lbldocname");

            string docinsert = "INSERT INTO VersionDocument_Master(PageWorkTblID,DocumentTitle,DocumentName,PageVersionID) values ('" + lblpageworkmasterId.Text + "','" + lbldocname.Text + "','" + lbldocname.Text + "','" + ViewState["PageVersionTblID"] + "')";

            SqlCommand cmdinsert = new SqlCommand(docinsert, con);
            con.Open();
            cmdinsert.ExecuteNonQuery();
            con.Close();
            ftpfile(lbldocname.Text.ToString(), Server.MapPath("~\\Attachment\\") + lbldocname.Text.ToString());


            Label2.Visible = true;
            Label2.Text = "Record Inserted Succesfully";
        }



        //Button4_Click(Button4, e);
        fillgrid();
        Session["GridFileAttach1"] = null;
        gridFileAttach.DataSource = null;
        gridFileAttach.DataBind();
        Session["GridFileDocu"] = null;
        Griddoc.DataSource = null;
        Griddoc.DataBind();

        //foreach (GridViewRow gg1 in GridView2.Rows)
        //{

        Int64 id = Convert.ToInt64(ViewState["PageVersionTblID"]);
        string usertype = Convert.ToString(ViewState["UserType"]).ToUpper();

        string strcomid = "select PageVersionTblId,[EpmloyeeID_AssignedDeveloper],[EpmloyeeID_AssignedTester],[EpmloyeeID_AssignedSupervisor] from PageWorkTbl where Id='" + ViewState["Pageworktblid"] + "'";
        SqlDataAdapter adcheck1 = new SqlDataAdapter(strcomid, con);
        DataTable dtcheck1 = new DataTable();
        adcheck1.Fill(dtcheck1);

        if (dtcheck1.Rows.Count > 0)
        {
            if (Chkcertify.Checked == true)
            {
                if (usertype == "DEVELOPER")
                {
                    string updatedev = "Update PageVersionTbl set DeveloperOK='1' ,DeveloperOkDate='" + DateTime.Now.ToShortDateString() + "',DeveloperNote='' where Id='" + Convert.ToString(dtcheck1.Rows[0]["PageVersionTblId"]) + "'";
                    SqlCommand ccm1 = new SqlCommand(updatedev, con);
                    con.Open();
                    ccm1.ExecuteNonQuery();
                    con.Close();
                    Label1.Text = "Data updated sucessfully";

                    //syncronisedata(Convert.ToString(dtcheck1.Rows[0]["PageVersionTblId"]), "deve");
                }
                if (usertype == "TESTER")
                {

                    string strcln = "Select Distinct MainMenuMaster.MainMenuName as manu,PageWorkTbl.Id,PageWorkTbl.WorkRequirementTitle,SubMenuMaster.SubMenuName,PageVersionTbl.VersionNo as VersionName,PageVersionTbl.PageName,Convert(Nvarchar,PageVersionTbl.Date,101) as Date,PageVersionTbl.Id as pvid from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join pageMaster on pageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId  inner join  MasterPageMaster  on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId where  (PageVersionTbl.Date Between '" + TextBox1.Text + "' and '" + TextBox2.Text + "') and WebsiteSectionId='" + ViewState["WebsiteSectionId"] + "' ";
                    string strtypework = " and PageVersionTbl.EmployeeId_Tester='" + Session["id"] + "' and PageVersionTbl.DeveloperOK='0'";
                    string pagename = " and PageMaster.PageId='" + ViewState["PageId"] + "'";
                    string alldata = strcln + strtypework + pagename;
                    SqlCommand cmdcount1 = new SqlCommand(alldata, con);
                    DataTable dtcount1 = new DataTable();
                    SqlDataAdapter adpcount1 = new SqlDataAdapter(cmdcount1);
                    adpcount1.Fill(dtcount1);
                    if (dtcount1.Rows.Count > 0)
                    {
                        //   txttdate.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Sorry, Before  Certification by Developer you can not update record";

                    }
                    else
                    {

                        string updatetest = "Update PageVersionTbl set TesterOk='1' ,TesterOkDate='" + DateTime.Now.ToShortDateString() + "',TesterNote='' where   Id='" + Convert.ToString(dtcheck1.Rows[0]["PageVersionTblId"]) + "'";
                        SqlCommand ccm1 = new SqlCommand(updatetest, con);
                        //SqlDataAdapter da = new SqlDataAdapter(ccm1);
                        //DataTable dt = new DataTable();
                        //da.Fill(dt);
                        //if (dt.Rows.Count > 0)
                        //{
                        con.Open();
                        ccm1.ExecuteNonQuery();
                        con.Close();
                        Label1.Visible = true;
                        Label1.Text = "Data updated sucessfully";

                        //}


                    }
                }
                if (usertype == "SUPERVISOR")
                {
                    string strclnn = "Select Distinct MainMenuMaster.MainMenuName as manu,PageWorkTbl.Id,PageWorkTbl.WorkRequirementTitle,SubMenuMaster.SubMenuName,PageVersionTbl.VersionNo as VersionName,PageVersionTbl.PageName,Convert(Nvarchar,PageVersionTbl.Date,101) as Date,PageVersionTbl.Id as pvid from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join pageMaster on pageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId  inner join  MasterPageMaster  on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId where  (PageVersionTbl.Date Between '" + TextBox1.Text + "' and '" + TextBox2.Text + "') and WebsiteSectionId='" + ViewState["WebsiteSectionId"] + "' ";
                    string strtypeworkk = " and PageVersionTbl.EmployeeId_Supervisor='" + Session["id"] + "' and PageVersionTbl.TesterOk='0'";
                    string pagenamee = " and PageMaster.PageId='" + ViewState["PageId"] + "'";
                    string alldataa = strclnn + strtypeworkk + pagenamee;
                    SqlCommand cmdcount12 = new SqlCommand(alldataa, con);
                    DataTable dtcount12 = new DataTable();
                    SqlDataAdapter adpcount12 = new SqlDataAdapter(cmdcount12);
                    adpcount12.Fill(dtcount12);
                    if (dtcount12.Rows.Count > 0)
                    {
                        Label1.Text = "Sorry, Before  Certification by Tester you can not update record";
                    }
                    else
                    {
                        string updatesup = "Update PageVersionTbl set SupervisorOk='1' ,SupervisorOkDate='" + DateTime.Now.ToShortDateString() + "',SupervisorNote='' where Id='" + Convert.ToString(dtcheck1.Rows[0]["PageVersionTblId"]) + "'";
                        SqlCommand ccm1 = new SqlCommand(updatesup, con);
                        con.Open();
                        ccm1.ExecuteNonQuery();
                        con.Close();
                        Label1.Visible = true;
                        Label1.Text = "Data updated sucessfully";
                        syncronisedata(Convert.ToString(ViewState["PageVersionTblID"]), "");
                    }
                }


                // FillGrid();

                // Label1.Text = "Data updated sucessfully";

            }

        }


        //}
        chkupld.Visible = false;
        chkupld_CheckedChanged(chkupld, e);
        Chkmultiupld.Visible = false;
        Chkmultiupld_CheckedChanged(Chkmultiupld, e);
        Chkcertify.Visible = false;
        Chkcertify_CheckedChanged1(Chkcertify, e);
        ModalPopupExtender2.Show();
    }
    protected void linkdow_Click(object sender, EventArgs e)
    {
        lblpaged.Visible = true;
        lblpaged.Text = "";
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        int data = Convert.ToInt32(grdsourcefile.DataKeys[rinrow].Value);




        string strcount = " select PDSA.FileName, PDSA.PageWorkTblId as PageWorkMasterId, PDSA.OriginalFileName, " +
                        "WebsiteMaster.*,PageMaster.FolderName from PageDevelopmentSourceCodeAllocateTable AS PDSA " +
                        "inner join  PageWorkTbl  on PageWorkTbl.id=PDSA.PageWorkTblId " +
                        "inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                        "inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId " +
                        "inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  " +
                        "inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  " +
                        "inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  " +
                        "inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PDSA.Id='" + data + "'";
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);



        if (dtcount.Rows.Count > 0)
        {

            string strpageversion = "select  * from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId where PageWorkTbl.Id='" + dtcount.Rows[0]["PageWorkMasterId"].ToString() + "'";
            SqlCommand cmdpageversion = new SqlCommand(strpageversion, con);
            DataTable dtpageversion = new DataTable();
            SqlDataAdapter adppageversion = new SqlDataAdapter(cmdpageversion);
            adppageversion.Fill(dtpageversion);

            if (dtpageversion.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtpageversion.Rows[0]["SupervisorCheckingDone"].ToString()) == true && Convert.ToBoolean(dtpageversion.Rows[0]["SupervisorOk"].ToString()) == true)
                {
                    lblpaged.Visible = true;
                    lblpaged.Text = "Sorry ,After Certification of Supervisior you can not download this page.";
                    ModalPopupExtender1.Show();

                }
                else
                {

                    lblftpurl123.Text = dtcount.Rows[0]["FTP_Url"].ToString();
                    lblftpport123.Text = dtcount.Rows[0]["FTP_Port"].ToString();
                    lblftpuserid.Text = dtcount.Rows[0]["FTP_UserId"].ToString();
                    lblftppassword123.Text = PageMgmt.Decrypted(dtcount.Rows[0]["FTP_Password"].ToString());
                    lbl_versionuser.Text = dtcount.Rows[0]["FileuploadUserId"].ToString(); ;
                    lbl_versionpass.Text =  PageMgmt.Decrypted(dtcount.Rows[0]["FileuploadPW"].ToString());

                    string[] seperator_RootPath = new string[] { "/" };
                    string RootPath = Convert.ToString(dtcount.Rows[0]["RootFolderPath"]);
                    string[] RootPathArray = RootPath.Split(seperator_RootPath, StringSplitOptions.RemoveEmptyEntries);
                    string FolderName = "";
                    for (int k = 2; k < RootPathArray.Length; k++)
                    {
                        FolderName += RootPathArray[k].ToString() + "/";
                    }
                    ViewState["folder"] = FolderName.ToString().Substring(0, FolderName.Length - 1);

                    //ViewState["folder"] = dtcount.Rows[0]["FolderName"].ToString();

                    string[] separator1 = new string[] { "/" };
                    string[] strSplitArr1 = lblftpurl123.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

                    String productno = strSplitArr1[0].ToString();
                    string ftpurl = "";

                    if (productno == "FTP:" || productno == "ftp:")
                    {
                        if (strSplitArr1.Length >= 3)
                        {
                            ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;
                            for (int i = 2; i < strSplitArr1.Length; i++)
                            {
                                ftpurl += "/" + strSplitArr1[i].ToString();
                            }
                        }
                        else
                        {
                            ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;

                        }
                    }
                    else
                    {
                        if (strSplitArr1.Length >= 2)
                        {
                            ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;
                            for (int i = 1; i < strSplitArr1.Length; i++)
                            {
                                ftpurl += "/" + strSplitArr1[i].ToString();
                            }
                        }
                        else
                        {
                            ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;

                        }

                    }


                    if (lblftpurl123.Text.Length > 0)
                    {
                        string VersionNo = Convert.ToString(dtpageversion.Rows[0]["VersionNo"]);
                        string VersionFolderName = "pageversion" + VersionNo.ToString().Trim();
                        //string ftphost = ftpurl + "/" + ViewState["folder"] + "/Attach/";
                        string ftphost = ftpurl + "/" + ViewState["folder"] + "/VersionFolder/" + VersionFolderName + "/";
                       
                        ftphost = ftpurl + "/" + VersionFolderName + "/";
                        string fnname = dtcount.Rows[0]["OriginalFileName"].ToString();
                        string despath = Server.MapPath("~\\Attachment\\") + fnname.ToString();
                        FileInfo filec = new FileInfo(despath);
                        try
                        {
                            if (!filec.Exists)
                            {
                                GetFile(ftphost, fnname, despath, lbl_versionuser.Text, lbl_versionpass.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            lblpaged.Text = ex.ToString();
                        }

                        
                            Response.Redirect("~/CheckDownload.aspx?path=" + despath + "");
                        FileInfo file = new FileInfo(despath);
                        if (file.Exists)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            Response.AddHeader("Content-Length", file.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.WriteFile(file.FullName);
                            Response.End();
                        }


                    }

                }
            }



        }

    }
    protected void linkdow1_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        int data = Convert.ToInt32(GridView1.DataKeys[rinrow].Value);


        //string strcount = " select PageFinishWorkUploadTbl.FileName, WebsiteMaster.*,PageMaster.FolderName from PageFinishWorkUploadTbl inner join  PageWorkTbl  on PageWorkTbl.id=PageFinishWorkUploadTbl.PageWorkTblId inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageFinishWorkUploadTbl.Id='" + data + "'";
        string strcount = " select PageMaster.FolderName, PageWorkGuideUploadTbl.WorkRequirementPdfFilename,PageWorkGuideUploadTbl.WorkRequirementAudioFileName,WebsiteMaster.* from PageWorkGuideUploadTbl inner join PageWorkTbl on PageWorkTbl.Id=PageWorkGuideUploadTbl.PageWorkTblId inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkGuideUploadTbl.Id='" + data + "' ";
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);

        if (dtcount.Rows.Count > 0)
        {
            lblftpurl123.Text = dtcount.Rows[0]["FTPWorkGuideUrl"].ToString();
            lblftpport123.Text = dtcount.Rows[0]["FTPWorkGuidePort"].ToString();
            lblftpuserid.Text = dtcount.Rows[0]["FTPWorkGuideUserId"].ToString();
            lblftppassword123.Text = PageMgmt.Decrypted(dtcount.Rows[0]["FTPWorkGuidePW"].ToString());
            //ViewState["folder"] = dtcount.Rows[0]["FolderName"].ToString();

            string[] separator1 = new string[] { "/" };
            string[] strSplitArr1 = lblftpurl123.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            String productno = strSplitArr1[0].ToString();
            string ftpurl = "";

            if (productno == "FTP:" || productno == "ftp:")
            {
                if (strSplitArr1.Length >= 3)
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;

                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + "/" ;

                }

            }


            if (lblftpurl123.Text.Length > 0)
            {
                string[] seperator_RootPath = new string[] { "/" };
                string RootPath = Convert.ToString(dtcount.Rows[0]["RootFolderPath"]);
                string[] RootPathArray = RootPath.Split(seperator_RootPath, StringSplitOptions.RemoveEmptyEntries);
                string FolderName = "";
                for (int k = 2; k < RootPathArray.Length; k++)
                {
                    FolderName += RootPathArray[k].ToString() + "/";
                }
                ViewState["folder"] = FolderName.ToString().Substring(0, FolderName.Length - 1);

                /*11 Dec2015 141 Folder Path  */
                //string ftphost = ftpurl + "/" + Convert.ToString(ViewState["folder"]) + "/Attach/";
               
             //141   string ftphost = ftpurl + "/Attach/";
                string ftphost = ftpurl;

                ///////string ftphost = ftpurl + "/" ;
                string fnname = "";
                if (dtcount.Rows[0]["WorkRequirementPdfFilename"].ToString() != "")
                {
                    fnname = dtcount.Rows[0]["WorkRequirementPdfFilename"].ToString();
                }
                else
                {
                    fnname = dtcount.Rows[0]["WorkRequirementAudioFileName"].ToString();

                }


                string despath = Server.MapPath("~\\Attachment\\") + fnname.ToString();

                FileInfo filec = new FileInfo(despath);
                try
                {
                    if (!filec.Exists)
                    {
                      //  ftphost = "ftp://72.38.84.226/";
                        
                        GetFile(ftphost, fnname, despath, lblftpuserid.Text, lblftppassword123.Text);
                    }
                }
                catch (Exception ex)
                {
                    lblpaged.Text = ex.ToString();
                }

                Response.Redirect("~/CheckDownload.aspx?path=" + despath + "");
                //despath = despath.Replace("\\Clientadmin", "");
                //FileInfo file = new FileInfo(despath);
                //if (file.Exists)
                //{
                //    Response.Clear();
                //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                //    Response.AddHeader("Content-Length", file.Length.ToString());
                //    Response.ContentType = "application/octet-stream";
                //    Response.WriteFile(file.FullName);
                //    Response.End();
                //}
                //else
                //{
                //    lblMsg1.Text = despath;
                //}


            }


        }

    }
    public bool GetFile(string ftp, string filename, string Destpath, string username, string password)
    {
        FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.
           Create(ftp.ToString() + filename.ToString());
        password = PageMgmt.Decrypted(password); // add this line by ninad at 1/7/2015
        oFTP.Credentials = new NetworkCredential(username.ToString(), password.ToString());
        oFTP.UseBinary = false;
        oFTP.UsePassive = true;
        oFTP.Method = WebRequestMethods.Ftp.DownloadFile;
        FtpWebResponse response =
           (FtpWebResponse)oFTP.GetResponse();
        Stream responseStream = response.GetResponseStream();

        FileStream fs = new FileStream(Destpath, FileMode.CreateNew);
        Byte[] buffer = new Byte[2047];
        int read = 1;
        while (read != 0)
        {
            read = responseStream.Read(buffer, 0, buffer.Length);
            fs.Write(buffer, 0, read);
        }
        responseStream.Close();
        fs.Flush();
        fs.Close();
        responseStream.Close();
        response.Close();
        oFTP = null;
        return true;
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void chkupld_CheckedChanged(object sender, EventArgs e)
    {
        fileuploadadattachment.Visible = chkupld.Checked;
        Button7.Visible = chkupld.Checked;
        lblfnm.Visible = chkupld.Checked;
        gridFileAttach.Visible = chkupld.Checked;
        ModalPopupExtender2.Show();

    }
    protected void Chkmultiupld_CheckedChanged(object sender, EventArgs e)
    {
        Fileupldmulti.Visible = Chkmultiupld.Checked;
        Button8.Visible = Chkmultiupld.Checked;
        lbldocname.Visible = Chkmultiupld.Checked;
        Griddoc.Visible = Chkmultiupld.Checked;
        ModalPopupExtender2.Show();

    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "mp3", "wma", "pdf" };

        string ext = System.IO.Path.GetExtension(Fileupldmulti.PostedFile.FileName);

        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + validFileTypes[i])
            {

                isValidFile = true;

                break;

            }

        }

        if (!isValidFile)
        {

            Label2.Visible = true;

            Label2.Text = "Invalid File. Please upload a File with extension " +

                           string.Join(",", validFileTypes);
            ModalPopupExtender2.Show();
        }

        else
        {


            String filename = "";

            if (Fileupldmulti.HasFile)
            {
                filename = Fileupldmulti.FileName;

                //string strSQL = "SELECT FileName FROM PageDevelopmentSourceCodeAllocateTable WHERE PageWorkTblID = " + lblpageworkmasterId.Text;
                //con.Open();
                //DataTable dtFiles = new DataTable();
                //SqlCommand cmd = new SqlCommand(strSQL, con);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(dtFiles);
                //con.Close();

                //if (filename.EndsWith(".aspx"))
                //{
                //    string NewFileName = Convert.ToString(dtFiles.Rows[0]["FileName"]);
                //    if (filename.ToString() == NewFileName.ToString())
                //    {
                //        Label2.Visible = false;
                //    }
                //    else
                //    {
                //        Label2.Visible = true;
                //        Label2.Text = "Filename should be the same file as you have downloaded.";
                //        ModalPopupExtender2.Show();
                //        return;
                //    }
                //}

                //if (filename.EndsWith(".aspx.cs"))
                //{
                //    string NewFIleName = Convert.ToString(dtFiles.Rows[1]["FileName"]);
                //    if (filename.ToString() == NewFIleName.ToString())
                //    {
                //        Label2.Visible = false;
                //    }
                //    else
                //    {
                //        Label2.Visible = true;
                //        Label2.Text = "Filename should be the same file as you have downloaded.";
                //        ModalPopupExtender2.Show();
                //        return;
                //    }
                //}

                //filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fileuploadadattachment.FileName;
                Fileupldmulti.PostedFile.SaveAs(Server.MapPath("~\\Attachment\\") + filename);




                DataTable dt = new DataTable();
                if (Session["GridFileDocu"] == null)
                {
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "DocumentTitle";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);



                }
                else
                {
                    dt = (DataTable)Session["GridFileDocu"];
                }
                DataRow dtrow = dt.NewRow();
                dtrow["DocumentTitle"] = filename;



                dt.Rows.Add(dtrow);
                Session["GridFileDocu"] = dt;
                Griddoc.DataSource = dt;


                Griddoc.DataBind();
                ModalPopupExtender2.Show();
            }
            else
            {
                Label2.Visible = true;
                Label2.Text = "Please Attach File to Upload.";
                return;
            }
        }
    }


    protected void syncronisedata(string Pageverid, string deve)
    {

        lblftpurl123.Text = "";
        lblftpport123.Text = "";
        lblftpuserid.Text = "";
        lblftppassword123.Text = "";


        //string strcount = "select Distinct WebsiteMaster.*,PageMaster.FolderName,PageFinishWorkUploadTbl.Folder_path,PageMaster.PageName,PageFinishWorkUploadTbl.FileName,PageMaster.PageId,PageVersionTbl.Id as pvid,PageVersionTbl.VersionNo from PageFinishWorkUploadTbl inner join PageWorkTbl on PageFinishWorkUploadTbl.PageWorkTblId=PageWorkTbl.Id inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageVersionTbl.SupervisorOk='1' and PageVersionTbl.TesterOk='1'  and PageVersionTbl.DeveloperOK='1' and PageVersionTbl.Id='" + Pageverid + "'";
        //string strcount = "select Distinct WebsiteMaster.*,PageMaster.FolderName,PageFinishWorkUploadTbl.Folder_path,PageMaster.PageName,PageFinishWorkUploadTbl.FileName,PageMaster.PageId,PageVersionTbl.Id as pvid,PageVersionTbl.VersionNo,PageVersionTbl.PageName as vpname from PageFinishWorkUploadTbl inner join PageWorkTbl on PageFinishWorkUploadTbl.PageWorkTblId=PageWorkTbl.Id inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where   PageVersionTbl.DeveloperOK='1' and PageVersionTbl.Id='" + Pageverid + "'";
        string strcount = "SELECT DISTINCT WebsiteMaster.*, PageMaster.FolderName, PDSA.Folder_path, PageMaster.PageName, PDSA.OriginalFileName, " +
                        "PDSA.FileName, PageMaster.PageId, PageVersionTbl.Id as pvid, PageVersionTbl.VersionNo, PageVersionTbl.PageName as vpname " +
                        "from PageDevelopmentSourceCodeAllocateTable AS PDSA " +
                        "inner join PageWorkTbl on PDSA.PageWorkTblId=PageWorkTbl.Id " +
                        "inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                        "inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId " +
                        "inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  " +
                        "inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  " +
                        "inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  " +
                        "inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId " +
                        "where   PageVersionTbl.DeveloperOK='1' and PageVersionTbl.Id='" + Pageverid + "'";
        //NOTE : Changed the name of the table from PageFinishWorkUploadTbl to PageDevelopmentSourceCodeAllocateTable bcoz while allocating any task to developer
        // we are entering record in that table - 17-Jan-2015
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);

        // string filedate = "UpdatedVersion" +dtcount.Rows[0]["VersionNo"].ToString()+ DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        string filedate = "";
        int RowIndex = 0;
        foreach (DataRow dt in dtcount.Rows)
        {
            try
            {
                lblftpurl123.Text = dt["FTP_Url"].ToString();
                lblftpport123.Text = dt["FTP_Port"].ToString();
                lblftpuserid.Text = dt["FTP_UserId"].ToString();
                //lblftppassword123 = PageMgmt.Decrypted(dt["FTP_Password"].ToString());
                lblftppassword123.Text = PageMgmt.Decrypted(dt["FTP_Password"].ToString());
                if (deve == "deve")
                {
                    filedate = "/VersionFolder";
                }
                //if (Convert.ToString(dt["Folder_path"]) == "")
                //{
                //    ViewState["folder"] = dt["FolderName"].ToString() + filedate;
                //}
                //else
                //{
                //    ViewState["folder"] = Convert.ToString(dt["Folder_path"]);
                //}
                string VersionNo = Convert.ToString(dtcount.Rows[0]["VersionNo"]);
                string VersionFolderName = "pageversion" + VersionNo.ToString().Trim();

                //string[] seperator_RootPath = new string[] { "/" };
                //string RootPath = Convert.ToString(dt["RootFolderPath"]);
                //string[] RootPathArray = RootPath.Split(seperator_RootPath, StringSplitOptions.RemoveEmptyEntries);
                //string FolderName = "";
                //for (int k = 2; k < RootPathArray.Length; k++)
                //{
                //    FolderName += RootPathArray[k].ToString() + "/";
                //}
                //ViewState["folder"] = FolderName.ToString().Substring(0, FolderName.Length - 1);

                ViewState["folder"] = dt["FolderName"].ToString();
                ViewState["RootPath"] = dt["RootFolderPath"];
                //string filename = Server.MapPath("~\\Attachment\\") + Convert.ToString(dt["FileName"]).Replace(" ", "");
                string filename = ViewState["RootPath"] + "/VersionFolder/" + VersionFolderName + "/" + Convert.ToString(dt["OriginalFileName"]).Replace(" ", "");
               

                string NewFileName = "";
                if (RowIndex == 1)
                {
                    NewFileName = dtcount.Rows[1]["PageName"].ToString() + ".cs";
                }
                else
                {
                    NewFileName = dtcount.Rows[0]["PageName"].ToString();
                }

                //ftpfile(Convert.ToString(dt["FileName"]).Replace(" ", ""), filename, Convert.ToString(dt["PageId"]), Convert.ToString(dt["pvid"]), filedate, Convert.ToString(dt["WebsiteUrl"]));
                ftpfile1(NewFileName.ToString(), filename, Convert.ToString(dt["PageId"]), Convert.ToString(dt["pvid"]), filedate, Convert.ToString(dt["WebsiteUrl"]));

                //if (deve == "")
                //{
                //    string fb = Convert.ToString(dt["FileName"]).Replace(" ", "");

                //    string filexten = Path.GetExtension(filename);
                //    if (filexten.ToUpper() == ".cs".ToUpper())
                //    {
                //        fb = fb.Remove(fb.Length - 3, 3);

                //    }
                //    if (Convert.ToString(dt["PageName"]).Replace(" ", "").ToUpper() == Convert.ToString(dt["FileName"]).Replace(" ", "").ToUpper())
                //    {
                //        ftpfile(Convert.ToString(dt["FileName"]).Replace(" ", ""), filename, Convert.ToString(dt["PageId"]), Convert.ToString(dt["pvid"]), filedate, Convert.ToString(dt["WebsiteUrl"]));
                //    }
                //    else if (Convert.ToString(dt["PageName"]).Replace(" ", "").ToUpper() == fb.Replace(" ", "").ToUpper())
                //    {
                //        ftpfile(Convert.ToString(dt["FileName"]).Replace(" ", ""), filename, Convert.ToString(dt["PageId"]), Convert.ToString(dt["pvid"]), filedate, Convert.ToString(dt["WebsiteUrl"]));
                //    }
                //}
                //else
                //{
                //    ftpfile(Convert.ToString(dt["FileName"]).Replace(" ", ""), filename, Convert.ToString(dt["PageId"]), Convert.ToString(dt["pvid"]), filedate, Convert.ToString(dt["WebsiteUrl"]));

                //}
                RowIndex += 1;
            }
            catch (Exception e1)
            {
                Label1.Text = e1.ToString();
            }

        }
    }

    public void ftpfile1(string inputfilepath, string filename, string pageid, string pvid, string filedate, string websitename1)
    {
        // string ftphost = "ftp://FTP.Eparcel.us/OnlineAcc/" + inputfilepath;

        FileInfo filec = new FileInfo(filename);
        if (filec.Exists == true)
        {
            string[] separator1 = new string[] { "/" };
            string[] strSplitArr1 = lblftpurl123.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            String productno = strSplitArr1[0].ToString();
            string ftpurl = "";

            if (productno == "FTP:" || productno == "ftp:")
            {
                if (strSplitArr1.Length >= 3)
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;

                }

            }


            if (lblftpurl123.Text.Length > 0)
            {
                string ftphost = ftpurl + "/" + ViewState["folder"] + "/" + inputfilepath;

                FtpWebRequest FTP = (FtpWebRequest)FtpWebRequest.Create(ftphost);

                try
                {
                    string websitename = websitename1;

                    //string value = stopwebsite(websitename);

                    FTP.Credentials = new NetworkCredential(lblftpuserid.Text, lblftppassword123.Text);
                    FTP.UseBinary = false;
                    FTP.KeepAlive = true;
                    FTP.UsePassive = true;
                    FTP.Method = WebRequestMethods.Ftp.UploadFile;


                    FileStream fs = File.OpenRead(filename);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();

                    Stream ftpstream = FTP.GetRequestStream();
                    ftpstream.Write(buffer, 0, buffer.Length);
                    ftpstream.Close();

                    if (filedate == "")
                    {
                        // System.IO.File.Delete(filename);
                    }
                    //string value1 = startwebsite(websitename);

                    //Label3.Text = ftphost;
                    //Label4.Text = filename;
                    //Label5.Text = lblftpuserid;
                    //Label6.Text = lblftppassword123;
                }
                catch
                {
                    string value1 = startwebsite(websitename1);
                    //Response.Write(ftphost);
                    //Response.Write(filename);

                }
            }

        }

    }

    public string stopwebsite(string websitename)
    {
        try
        {
            string siteid = getsiteid(websitename);
            if (siteid == null) return "error:this web site is not exist.";


            DirectoryEntry devdir = new DirectoryEntry("IIS://localhost/W3SVC/" + siteid);
            devdir.Invoke("stop", null);
            return "successful:stop web site " + websitename + " is succeed!";


        }
        catch (Exception e)
        {
            return "error:stop web site has been failed." + e.Message;
        }
    }

    public string startwebsite(string websitename)
    {
        try
        {
            string siteid = getsiteid(websitename);
            if (siteid == null) return "error:this web site is not exist.";

            DirectoryEntry devdir = new DirectoryEntry("IIS://localhost/W3SVC/" + siteid);
            devdir.Invoke("start", null);
            return "successful:start web site " + websitename + " is succeed!";

        }
        catch (Exception e)
        {
            return "error:start web site has been failed." + e.Message;
        }
    }

    public string getsiteid(string websitename)
    {
        DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
        try
        {
            string siteid = null;
            foreach (DirectoryEntry bb in root.Children)
            {
                if (bb.SchemaClassName == "IIsWebServer")
                {
                    if (websitename == bb.Properties["ServerComment"].Value.ToString()) siteid = bb.Name;
                }
            }
            if (siteid == null) return null;
            return siteid;
        }
        catch
        {
            return null;
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           
            int i = e.Row.RowIndex ;
           
            if (i == -1)
            {
                return;
            }

            //Label lblworktitle12345 = e.Row.FindControl("lblworktitle12345") as Label;                     //(Label)GridView2.Rows[rinrow].FindControl("lblworktitle12345");
            //Label lblbujtedhr = e.Row.FindControl("lblbudgetd132") as Label;                                                                        //(Label)GridView2.Rows[rinrow].FindControl("lblbudgetd132");
            //LinkButton LinkButton1 = e.Row.FindControl("LinkButton1") as LinkButton;
            //Int64 id = Convert.ToInt64(GridView2.DataKeys[i]["Id"]);
            //string url = "Fillreport_mywork.aspx?VID=" + id + "&eid="+ ddlemployee.SelectedValue +"&frmdt="+ TextBox1.Text +"&todt="+ TextBox2.Text +"&lblworktitle="+lblworktitle12345.Text+"&lblbujtedhr="+lblbujtedhr.Text+"";
            //LinkButton1.Attributes.Add("onclick", "window.open('" + url + "', 'popup_window', 'width=910,height=710,left=100,top=10,resizable=yes')");


            LinkButton LinkButton1 = e.Row.FindControl("LinkButton1") as LinkButton;
            Button Button15 = e.Row.FindControl("Button15") as Button;
            int SupervisorOK = Convert.ToInt16(GridView2.DataKeys[i]["SupervisorOK"]);
            int DevelopersOK = Convert.ToInt16(GridView2.DataKeys[i]["DeveloperOK"]);
            int TestingsOK = Convert.ToInt16(GridView2.DataKeys[i]["TesterOk"]);
            int ReturntoDevelopers=0;
            try
            {
                 ReturntoDevelopers = Convert.ToInt16(GridView2.DataKeys[i]["ReturntoDevelopers"]); 
            }
            catch(Exception ex)
            {
            }
            
            
            if (ddltypeofwork.SelectedValue == "3")
            {
                if (SupervisorOK == 1)
                {
                    LinkButton1.Enabled = false;
                    LinkButton1.CssClass = "btnFillGrey";
                }
                else
                {
                    LinkButton1.CssClass = "btnFillGreen";
                }
                //
                try
                {
                    if (ReturntoDevelopers == 1)
                    {
                        LinkButton1.CssClass = "btnFillRed";
                        LinkButton1.Enabled = false;


                    }

                }
                catch (Exception ex)
                {
                }
            }

            if (ddltypeofwork.SelectedValue == "2")
            {
                if (DevelopersOK == 1)
                {
                    LinkButton1.CssClass = "btnFillGreen";
                    LinkButton1.Enabled = true;
                }
                if (TestingsOK == 1)
                {
                    LinkButton1.CssClass = "btnFillBlue";
                    LinkButton1.Enabled = false;
                }
                if (TestingsOK == 0 && DevelopersOK == 1)
                {
                    LinkButton1.CssClass = "btnFillGreen";
                    LinkButton1.Enabled = true;
                }
                if (DevelopersOK == 0 )
                {
                    LinkButton1.Enabled = false;
                    LinkButton1.CssClass = "btnFillGrey";
                    
                }


                try
                {
                    if (ReturntoDevelopers == 1)
                    {
                        LinkButton1.CssClass = "btnFillRed";
                        LinkButton1.Enabled = false;


                    }

                }
                catch (Exception ex)
                {
                }
            }
            if (ddltypeofwork.SelectedValue == "1")
            {
            
            if (DevelopersOK == 1)
            {
              
                LinkButton1.CssClass = "btnFillGreen";
                LinkButton1.Enabled = false;  

            }
            else
            {
                LinkButton1.CssClass = "btnFillGrey";
            }
            }
            try
            {
                if (ReturntoDevelopers == 1)
                {
                    LinkButton1.CssClass = "btnFillRed";
                 


                }

            }
            catch (Exception ex)
            {
            }
            
        }
        catch (Exception)
        {

        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        string usertype = Convert.ToString(ViewState["UserType"]);

        if (usertype == "Tester" || usertype == "Supervisor")
        {
            lblupldcode.Visible = false;
            lbluplddoc.Visible = false;
            lblcerti.Visible = CheckBox1.Checked;
            Chkcertify.Visible = CheckBox1.Checked;
            //fileuploadadattachment.Visible = true;
            // Button7.Visible = true;
            lblfnm.Visible = false;
            chkupld.Visible = false;
            Chkmultiupld.Visible = false;
            //  Fileupldmulti.Visible = true;
            // Button8.Visible = true;
            lbldocname.Visible = false;
            chkupld.Checked = false;
            chkupld_CheckedChanged(chkupld, e);
            Chkmultiupld.Checked = false;
            Chkmultiupld_CheckedChanged(Chkmultiupld, e);
        }
        else
        {
            lblcopycode.Visible = CheckBox1.Checked;
            lblupldcode.Visible = CheckBox1.Checked;
            lbluplddoc.Visible = CheckBox1.Checked;
            lblcerti.Visible = CheckBox1.Checked;
            Chkcertify.Visible = CheckBox1.Checked;
            //fileuploadadattachment.Visible = true;
            // Button7.Visible = true;
            lblfnm.Visible = CheckBox1.Checked;
            chkupld.Visible = CheckBox1.Checked;
            Chkmultiupld.Visible = CheckBox1.Checked;
            //  Fileupldmulti.Visible = true;
            // Button8.Visible = true;
            lbldocname.Visible = CheckBox1.Checked;
            chkupld.Checked = false;
            chkupld_CheckedChanged(chkupld, e);
            Chkmultiupld.Checked = false;
            Chkmultiupld_CheckedChanged(Chkmultiupld, e);
        }

        ModalPopupExtender2.Show();
    }
    protected void Chkcertify_CheckedChanged1(object sender, EventArgs e)
    {
        string usertype = Convert.ToString(ViewState["UserType"]).ToUpper();

        if (usertype == "DEVELOPER")
        {
            lbldeveloper.Visible = true;
            lbldeveloper.Text = "I certify that I have completed the Development as per the Instruction, I also certify that  page design is good and the functionality is working properly.";
        }
        else if (usertype == "TESTER")
        {
            lbltester.Visible = true;
            lbltester.Text = "I certify that I have completed the Testing as per the Instruction, I also certify that  page design is good and the functionality is working properly.";
        }
        else if (usertype == "SUPERVISOR")
        {
            lblsupervior.Visible = true;
            lblsupervior.Text = "I certify that I have checked the page and it is working properly as planned. I also certify that proper documentation is done and is also uploaded in the system.";

        }


        Button4.Visible = true;

        ModalPopupExtender2.Show();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        lblpageworkId.Text = "";
        lblworltitleatreport.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        CheckBox1.Checked = false;
        chkupld.Checked = false;
        Chkmultiupld.Checked = false;
        Chkcertify.Checked = false;
        fileuploadadattachment.Visible = false;
        Button7.Visible = false;
        gridFileAttach.Visible = false;
        Fileupldmulti.Visible = false;
        Button8.Visible = false;
        Griddoc.Visible = false;
        lbldeveloper.Text = "";
        lbltester.Text = "";
        lblsupervior.Text = "";
        lblupldcode.Visible = false;
        lblcerti.Visible = false;
        lbluplddoc.Visible = false;
        ModalPopupExtender2.Hide();


    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            lblactulhr.Visible = true;
            txtactualhourrequired.Visible = true;
            CheckBox1.Checked = false;
            CheckBox1_CheckedChanged(CheckBox1, e);
            Button4.Visible = true;
            trtbl.Visible = false;
        }
        else
        {
            trtbl.Visible = true;
            CheckBox1.Checked = true;
            CheckBox1_CheckedChanged(CheckBox1, e);
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "popup")
        {
          //  int i = Convert.ToInt16(e.CommandArgument);
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

           // Label lblworktitle12345 = GridView2.Rows[i].FindControl("lblworktitle12345") as Label;
            Label lblworktitle12345 = row.FindControl("lblworktitle12345") as Label;
            Label lblbujtedhr = row.FindControl("lblbudgetd132") as Label;            
           // Label lblbujtedhr = GridView2.Rows[i].FindControl("lblbudgetd132") as Label;                                                                        //(Label)GridView2.Rows[rinrow].FindControl("lblbudgetd132");
           // LinkButton LinkButton1 = GridView2.Rows[i].FindControl("LinkButton1") as LinkButton;
            LinkButton LinkButton1 = row.FindControl("LinkButton1") as LinkButton;
          //  Int64 id = Convert.ToInt64(GridView2.DataKeys[i]["Id"]);
           // Int64 Pageid = Convert.ToInt64(GridView2.DataKeys[i]["PageId"]);
            Label id = row.FindControl("lblmasterId") as Label;
        //   Int64 id = Convert.ToInt64(lblmasterId);
            Label Pageid = row.FindControl("Label3PageId") as Label;
         //  Int64 Pageid = Convert.ToInt64(Label3PageId); 
            string url = "Fillreport_mywork.aspx?VID=" + id.Text + "&eid=" + ddlemployee.SelectedValue + "&frmdt=" + TextBox1.Text + "&todt=" + TextBox2.Text + "&lblworktitle=" + lblworktitle12345.Text + "&lblbujtedhr=" + lblbujtedhr.Text + "&PageId=" + Pageid.Text + "";
            //LinkButton1.Attributes.Add("onclick", "window.open('" + url + "', 'popup_window', 'width=910,height=710,left=100,top=10,resizable=yes')");
            LinkButton1.OnClientClick = "window.open('" + url + "', '_blank', 'width=1000,height=710,left=100,top=10,resizable=yes')";
        }

        if (e.CommandName == "popup1")
        {
            int i = Convert.ToInt16(e.CommandArgument);
            Label lblworkdonereport45 = GridView2.Rows[i].FindControl("lblworkdonereport45") as Label;                     //(Label)GridView2.Rows[rinrow].FindControl("lblworktitle12345");
            Label lbl_page = GridView2.Rows[i].FindControl("lbl_page") as Label;                                                                        //(Label)GridView2.Rows[rinrow].FindControl("lblbudgetd132");
            LinkButton LinkButton11 = GridView2.Rows[i].FindControl("LinkButton11") as LinkButton;
            Label lblVirsionurl = GridView2.Rows[i].FindControl("lblVirsionurl") as Label;     
            String header =Convert.ToString(lblworkdonereport45.Text);
            Console.WriteLine(header);
            Console.WriteLine(header.Trim(new Char[] { ' ', '-', '\t' }));
            header = header.Replace(" ", "");
            string url = "" + lblVirsionurl.Text + "/pageversion" + header + "/" + lbl_page.Text + "";
            Response.Redirect("http://" + url);
            //Response.Redirect("https://paymentgateway.safestserver.com/paymentnow.aspx?payid=" + i + "");
            //LinkButton1.Attributes.Add("onclick", "window.open('" + url + "', 'popup_window', 'width=910,height=710,left=100,top=10,resizable=yes')");
        //141    LinkButton11.OnClientClick = "window.open('" + url + "', '_blank', 'width=1000,height=710,left=100,top=10,resizable=yes')";
        }


    }

    protected void fillpagload()
    {
        string str = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId  , PageMaster.PageName,WebsiteMaster.VersionFolderUrl, dbo.PageMaster.FolderName from PageWorkTbl " +
       " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
        "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
        " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
        " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
         " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
       " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId  ";  //where PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddlemployee.SelectedValue + "' and WebsiteMaster.ID='" + ddlwebsite.SelectedValue + "' and PageWorkTbl.DevelopmentDone='" + ddlstatus.SelectedIndex + "' 

        SqlCommand cmdcln = new SqlCommand(str, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            GridView2.DataSource = dtcln;
            GridView2.DataBind();

        }

    }

    //private void filldata()
    //{
    //    string strSQL = "SELECT PWT.EpmloyeeID_AssignedDeveloper, PWT.EpmloyeeID_AssignedSupervisor, PWT.EpmloyeeID_AssignedTester FROM PageWorkTbl AS PWT " +
    //                       "INNER JOIN PageVersionTbl AS PVT ON PVT.ID = PWT.PageVersionTblID " +
    //                       "WHERE PWT.ID = " + Session["vid"];
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand(strSQL, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dtRoles = new DataTable();
    //    da.Fill(dtRoles);
    //    con.Close();
    //    string empid = "";
    //    string Emp_Developer = ""; string Emp_Supervisor = ""; string Emp_Tester = "";
    //    if (dtRoles.Rows.Count > 0)
    //    {
    //        empid = Convert.ToString(Session["id"]);
    //        Emp_Developer = Convert.ToString(dtRoles.Rows[0]["EpmloyeeID_AssignedDeveloper"]);
    //        Emp_Supervisor = Convert.ToString(dtRoles.Rows[0]["EpmloyeeID_AssignedSupervisor"]);
    //        Emp_Tester = Convert.ToString(dtRoles.Rows[0]["EpmloyeeID_AssignedTester"]);
    //    }

    //    string Emp_Role = "";
    //    if (Emp_Developer == empid)
    //    {
    //        Emp_Role = "Developer";
    //    }
    //    else if (Emp_Tester == empid)
    //    {
    //        Emp_Role = "Tester";
    //    }
    //    else if (Emp_Supervisor == empid)
    //    {
    //        Emp_Role = "Supervisor";
    //    }
    //    ViewState["UserType"] = Emp_Role;

    //    ViewState["Pageworktblid"] = Session["vid"];

    //    string strverid = "select PageVersionTblID from PageWorkTbl WHERE ID = '" + Session["vid"] + "'";
    //    SqlCommand cmdver = new SqlCommand(strverid, con);
    //    DataTable dtver = new DataTable();
    //    SqlDataAdapter adpver = new SqlDataAdapter(cmdver);
    //    adpver.Fill(dtver);
    //    if (dtver.Rows.Count > 0)
    //    {
    //        ViewState["PageVersionTblID"] = dtver.Rows[0]["PageVersionTblID"].ToString();
    //    }

    //    lblpageworkId.Text = Convert.ToString(Session["vid"]);
    //    lblworltitleatreport.Text = Convert.ToString(Session["lblworktitle"]);
    //    lblnewbujtedhr.Text = Convert.ToString(Session["lblbujtedhr"]);
    //    //GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

    //    //int rinrow = row.RowIndex;


    //    //Label lblmasterId = (Label)GridView2.Rows[rinrow].FindControl("lblmasterId");


    //    lblpageworkmasterId.Text = Convert.ToString(Session["vid"]);


    //    string strcount = " select WebsiteMaster.*,PageMaster.PageId,WebsiteSection.WebsiteSectionId,PageMaster.FolderName from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.Id='" + Session["vid"] + "'";
    //    SqlCommand cmdcount = new SqlCommand(strcount, con);
    //    DataTable dtcount = new DataTable();
    //    SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
    //    adpcount.Fill(dtcount);
    //    if (dtcount.Rows.Count > 0)
    //    {
    //        lblftpurl123.Text = dtcount.Rows[0]["FTP_Url"].ToString();
    //        lblftpport123.Text = dtcount.Rows[0]["FTP_Port"].ToString();
    //        lblftpuserid.Text = dtcount.Rows[0]["FTP_UserId"].ToString();
    //        lblftppassword123.Text = PageMgmt.Decrypted(dtcount.Rows[0]["FTP_Password"].ToString());
    //        ViewState["folder"] = dtcount.Rows[0]["FolderName"].ToString();
    //        ViewState["WebsiteSectionId"] = dtcount.Rows[0]["WebsiteSectionId"].ToString();
    //        ViewState["PageId"] = dtcount.Rows[0]["PageId"].ToString();
    //    }
    //    else
    //    {
    //        lblftpurl123.Text = "";
    //        lblftpport123.Text = "";
    //        lblftpuserid.Text = "";
    //        lblftppassword123.Text = "";
    //        ViewState["folder"] = "";
    //    }
    //    Session["GridFileAttach1"] = null;
    //    gridFileAttach.DataSource = null;
    //    gridFileAttach.DataBind();

    //    Session["GridFileDocu"] = null;
    //    Griddoc.DataSource = null;
    //    Griddoc.DataBind();

    //    TextBox3.Text = System.DateTime.Now.ToShortDateString();

    //}

    protected void fillgrid()
    {

        string me = "";

        if (ddlwebsite.SelectedIndex > 0)
        {
            me += " and WebsiteMaster.ID='" + ddlwebsite.SelectedValue + "'";
        }
        
        if (TextBox1.Text == "" || TextBox2.Text == "")
        {
            
        }
        else
        {
            me += " and PageWorkTbl.TargetDateTester between '" + TextBox1.Text + "' and '" + TextBox2.Text + "'";
        }
        if (TextBox6.Text != "")
        {
            me += "  and (PageMaster.PageName Like '%" + TextBox6.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox6.Text + "%' ) ";
        }
        //string strtypework = "";

        if (ddltypeofwork.SelectedValue == "1")
        {
            // developer
            string strcln = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId , dbo.PageMaster.PageName , dbo.PageMaster.FolderName,  dbo.WebsiteMaster.VersionFolderUrl  , dbo.PageWorkTbl.DevelopmentDone, dbo.PageWorkTbl.TestingDone, dbo.PageWorkTbl.SupervisorCheckingDone , dbo.PageVersionTbl.DeveloperOK, dbo.PageVersionTbl.TesterOk ,dbo.PageVersionTbl.ReturntoDevelopers from PageWorkTbl " +
                        " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                        "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
                        " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
                        " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
                        " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
                        " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddlemployee.SelectedValue + "' and PageVersionTbl.DeveloperOK='" + ddlstatus.SelectedIndex + "'   " + me + "";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                GridView2.DataSource = dtcln;
                GridView2.DataBind();

            }
            //strtypework = " and PageVersionTbl.EmployeeId_Developer='" + ddlemployee.SelectedValue + "' and PageVersionTbl.DeveloperOK='" + cert + "' ";
        }
        if (ddltypeofwork.SelectedValue == "2")
        {
            //tester
            string strcln12 = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId , dbo.PageMaster.PageName , dbo.PageMaster.FolderName , dbo.WebsiteMaster.VersionFolderUrl , dbo.PageWorkTbl.DevelopmentDone, dbo.PageWorkTbl.TestingDone, dbo.PageWorkTbl.SupervisorCheckingDone , dbo.PageVersionTbl.DeveloperOK, dbo.PageVersionTbl.TesterOk ,  dbo.PageVersionTbl.ReturntoDevelopers from PageWorkTbl " +
            " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
            "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
            " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
            " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
            " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
            " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.EpmloyeeID_AssignedTester='" + ddlemployee.SelectedValue + "' and PageVersionTbl.TesterOk='" + ddlstatus.SelectedIndex + "'  " + me + "  ";

            SqlCommand cmdcln12 = new SqlCommand(strcln12, con);
            DataTable dtcln12 = new DataTable();
            SqlDataAdapter adpcln12 = new SqlDataAdapter(cmdcln12);
            adpcln12.Fill(dtcln12);

            if (dtcln12.Rows.Count > 0)
            {
                GridView2.DataSource = dtcln12;
                GridView2.DataBind();

            }
            //strtypework = " and PageVersionTbl.EmployeeId_Tester='" + ddlemployee.SelectedValue + "' and PageVersionTbl.TesterOk='" + cert + "'";
        }
        if (ddltypeofwork.SelectedValue == "3")
        {
            //supervisor
            string strcln1234 = " select PageWorkTbl.Id,PageWorkTbl.EpmloyeeID_AssignedDeveloper,PageWorkTbl.EpmloyeeID_AssignedTester,PageWorkTbl.EpmloyeeID_AssignedSupervisor,PageWorkTbl.WorkRequirementTitle,PageVersionTbl.VersionNo as VersionName,PageMaster.PageTitle,WebsiteMaster.WebsiteName, PageVersionTbl.SupervisorOK,PageMaster.PageId, dbo.PageMaster.PageName , dbo.PageMaster.FolderName , dbo.WebsiteMaster.VersionFolderUrl  , dbo.PageWorkTbl.DevelopmentDone, dbo.PageWorkTbl.TestingDone, dbo.PageWorkTbl.SupervisorCheckingDone , dbo.PageVersionTbl.DeveloperOK, dbo.PageVersionTbl.TesterOk ,dbo.PageVersionTbl.ReturntoDevelopers from PageWorkTbl " +
          " inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
          "  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   " +
          " inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   " +
          " inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   " +
          " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId   " +
          " inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.EpmloyeeID_AssignedSupervisor='" + ddlemployee.SelectedValue + "' and PageVersionTbl.SupervisorOk='" + ddlstatus.SelectedIndex + "'   " + me + "  ";
            //dbo.PageVersionTbl.TesterOk=1 and 

            SqlCommand cmdcln1234 = new SqlCommand(strcln1234, con);
            DataTable dtcln1234 = new DataTable();
            SqlDataAdapter adpcln1234 = new SqlDataAdapter(cmdcln1234);
            adpcln1234.Fill(dtcln1234);

            if (dtcln1234.Rows.Count > 0)
            {
                GridView2.DataSource = dtcln1234;
                GridView2.DataBind();
               // GridView2.Columns[9].Visible = false;


            }
            //strtypework = " and PageVersionTbl.EmployeeId_Supervisor='" + ddlemployee.SelectedValue + "' and PageVersionTbl.SupervisorOk='" + cert + "'";
        }

       

        foreach (GridViewRow gdr in GridView2.Rows)
        {
            Label lblmasterId = (Label)gdr.FindControl("lblmasterId");
            Label lbldate12345 = (Label)gdr.FindControl("lbldate12345");
            Label lblbudgetd132 = (Label)gdr.FindControl("lblbudgetd132");
            Label lblactualhour = (Label)gdr.FindControl("lblactualhour");

            Label lblfileupload = (Label)gdr.FindControl("lblfileupload");


            string str123 = "select PageWorkTbl.* from PageWorkTbl  inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId   inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId    inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId    inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id    inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId    inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId  where PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddlemployee.SelectedValue + "' AND PageWorkTbl.ID = " + lblmasterId.Text;
            SqlCommand cmd123 = new SqlCommand(str123, con);
            DataTable dt123 = new DataTable();
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
            adp123.Fill(dt123);
            if (dt123.Rows.Count > 0)
            {
                DateTime MainStartDate;
                MainStartDate = Convert.ToDateTime(dt123.Rows[0]["TargetDateDeveloper"].ToString());


                lbldate12345.Text = MainStartDate.ToShortDateString();

                lblbudgetd132.Text = dt123.Rows[0]["BudgetedHourDevelopment"].ToString();

            }

            string str456 = "select PageWorkTbl.* from PageWorkTbl  inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId   inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId    inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId    inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id    inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId    inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId  where PageWorkTbl.EpmloyeeID_AssignedTester='" + ddlemployee.SelectedValue + "' AND PageWorkTbl.ID = " + lblmasterId.Text;
            SqlCommand cmd456 = new SqlCommand(str456, con);
            DataTable dt456 = new DataTable();
            SqlDataAdapter adp456 = new SqlDataAdapter(cmd456);
            adp456.Fill(dt456);
            if (dt456.Rows.Count > 0)
            {
                DateTime MainStartDate1;
                MainStartDate1 = Convert.ToDateTime(dt456.Rows[0]["TargetDateTester"].ToString());


                lbldate12345.Text = MainStartDate1.ToShortDateString();


                //  lbldate12345.Text = dt123.Rows[0]["TargetDateTester"].ToString();

                lblbudgetd132.Text = dt456.Rows[0]["BudgetedHourTesting"].ToString();

            }

            string str789 = "select PageWorkTbl.* from PageWorkTbl  inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId   inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId    inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId    inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id    inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId    inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId  where PageWorkTbl.EpmloyeeID_AssignedSupervisor='" + ddlemployee.SelectedValue + "' AND PageWorkTbl.ID = " + lblmasterId.Text;
            SqlCommand cmd789 = new SqlCommand(str789, con);
            DataTable dt789 = new DataTable();
            SqlDataAdapter adp789 = new SqlDataAdapter(cmd789);
            adp789.Fill(dt789);
            if (dt789.Rows.Count > 0)
            {
                DateTime MainStartDate2;
                MainStartDate2 = Convert.ToDateTime(dt789.Rows[0]["TargetDateSuperviserApproval"].ToString());

                //lbldate12345.Text = dt123.Rows[0]["BudgetedHourSupervisorChecking"].ToString();
                lbldate12345.Text = MainStartDate2.ToShortDateString();

                // lbldate12345.Text = dt123.Rows[0]["TargetDateSuperviserApproval"].ToString();
                lblbudgetd132.Text = dt789.Rows[0]["BudgetedHourSupervisorChecking"].ToString();

            }
            string stractual = " select sum(datepart(hour,convert(datetime,HourSpent)))  AS TotalHours,sum(datepart(minute,convert(datetime,HourSpent))) AS TotalMinutes from MyDailyWorkReport where PageWorkTblId='" + lblmasterId.Text + "' and EmployeeId='" + ddlemployee.SelectedValue + "' ";
            SqlCommand cmdactual = new SqlCommand(stractual, con);
            DataTable dtactual = new DataTable();
            SqlDataAdapter adpactual = new SqlDataAdapter(cmdactual);
            adpactual.Fill(dtactual);
            if (dtactual.Rows.Count > 0)
            {
                string FinalTime = "";
                Int32 in1 = 0;
                Int32 HourtoMinute1 = 0;
                Int32 Minute1 = 0;
                Int32 TotalMinutes132 = 0;
                Int32 FinalHours = 0;
                Int32 FinalMinute = 0;


                string TotalHour = dtactual.Rows[0]["TotalHours"].ToString();
                string TotalMinutes = dtactual.Rows[0]["TotalMinutes"].ToString();

                if (TotalHour == "")
                {

                }
                else
                {
                    in1 = Convert.ToInt32(TotalHour.ToString());
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(TotalMinutes.ToString());

                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                }


                FinalHours = (TotalMinutes132 / 60);
                FinalMinute = (TotalMinutes132 % 60);

                FinalTime = FinalHours + ":" + FinalMinute;
                lblactualhour.Text = FinalTime.ToString();

            }
            string strcount = " select COUNT(Id) As Count from PageFinishWorkUploadTbl where PageWorkTblId='" + lblmasterId.Text + "' ";
            SqlCommand cmdcount = new SqlCommand(strcount, con);
            DataTable dtcount = new DataTable();
            SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
            adpcount.Fill(dtcount);
            if (dtcount.Rows.Count > 0)
            {
                // lblfileupload.Text = dtcount.Rows[0]["Count"].ToString();
            }




        }


    }
}