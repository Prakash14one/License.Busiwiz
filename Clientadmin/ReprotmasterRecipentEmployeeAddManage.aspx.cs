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
public partial class Page_Master : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    DataSet dt;
    SqlConnection conOADB; 
        int filterprodctid=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        conOADB = pgcon.dynconn;

        if (Session["Comid"] == null || Session["ClientId"]==null)
       {
           Response.Redirect("~/Login.aspx");
       }
        lblmsg.Text = "";
        if (!IsPostBack)
        {
          
            lblpagename.Text = "";
            Session["GridFileAttach1"] = null;
            ViewState["sortOrder"] = "";          
            
            filterproduct();//M
            Fillpages();//M
            FillGV_pagelist();//
            Fillwarehouse();//
            filltimezone();
            fillfilteremp();//            
            Fillgrid();       
            
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        pnladdnew.Visible = true;
        lbllegend.Text = "Add New Rules for sending Reprots";
        lblmsg.Text = "";
        lbl_pagename.Text = "";
        lbl_pagename.Text = "";
        Session["GridFileAttach1"] = null;
        gridFileAttach.DataSource = null;
        gridFileAttach.DataBind();
        TextBox5.Text = "";
        lblempmsg.Visible = false;
        addnewpanel.Visible = false;
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

        FilterProduct.SelectedValue = "12";

    }
    protected void ddlpagename_SelectedIndexChanged(object sender, EventArgs e)
    {
        string comd = " SELECT dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageId, dbo.ReportMasterFunctionality.Id FROM  dbo.PageMaster INNER JOIN dbo.ReportMasterFunctionality ON dbo.PageMaster.PageId = dbo.ReportMasterFunctionality.Pageid where ReportMasterFunctionality.PageId=" + ddlpagename.SelectedValue;


        SqlCommand cmall = new SqlCommand(comd, con);
        DataTable dtall = new DataTable();
        SqlDataAdapter adpall = new SqlDataAdapter(cmall);
        adpall.Fill(dtall);
        if (dtall.Rows.Count > 0)
        {
            lbl_pagename.Text = dtall.Rows[0]["PageName"].ToString();
            lbl_titel.Text = dtall.Rows[0]["PageTitle"].ToString();
            ViewState["Pagerepoid"] = dtall.Rows[0]["Id"].ToString();
        }
    }
    protected void Fillpages()
    {

        ddlpagename.Items.Clear();
        if (FilterProduct.SelectedIndex > 1)
        {
            string strcln = "";
            strcln = " SELECT distinct MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName as Page_Name from      dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id INNER JOIN dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteSectionId = dbo.MasterPageMaster.WebsiteSectionId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ReportMasterFunctionality ON dbo.PageMaster.PageId = dbo.ReportMasterFunctionality.Pageid  where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MainMenuMaster.MasterPage_Id='" + FilterProduct.SelectedValue + "' and  PageMaster.PageName !='' and MainMenuMaster.MainMenuName !='' ";
            string orderby = "order by Page_Name";
            string finalstr = strcln + orderby;
            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlpagename.DataSource = dtcln;
            ddlpagename.DataValueField = "PageId";
            ddlpagename.DataTextField = "Page_Name";
            ddlpagename.DataBind();
            ddlpagename.Items.Insert(0, "All");
            ddlpagename.Items[0].Value = "0";

        }
        else
        {
            ddlpagename.DataSource = null;
            ddlpagename.DataValueField = "PageId";
            ddlpagename.DataTextField = "Page_Name";
            ddlpagename.DataBind();
            ddlpagename.Items.Insert(0, "All");
            ddlpagename.Items[0].Value = "0";
        }
    }
    protected void FillGV_pagelist()
    {
        Session["SelectedIndexS"] = FilterProduct.SelectedIndex;
        string str1 = "";
        string str2 = "";
        string str3 = "";
        string str4 = "";
        string strcln = "";

        //strcln = "select distinct SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId, PageMaster.PageId,PageMaster.FolderName,LanguageMaster.Name,LanguageMaster.Id,MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,PageMaster.Active, ProductMaster.ProductName,VersionInfoMaster.VersionInfoName, PageMaster.PageName,PageMaster.PageTitle,  WebsiteSection.SectionName + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name   from  ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId  inner join PageMaster on PageMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId  left outer join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId left outer join SubMenuMaster  on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join ProductDetail  on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster  on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection  on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster  on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId  left outer join LanguageMaster on LanguageMaster.Id = PageMaster.LanguageId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and PageMaster.VersionInfoMasterId='" + ViewState["versioninfo"] + "' ";


        strcln = " select distinct SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId, PageMaster.PageId,PageMaster.FolderName,LanguageMaster.Name,LanguageMaster.Id,MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,PageMaster.Active, ProductMaster.ProductName,VersionInfoMaster.VersionInfoName, PageMaster.PageName,PageMaster.PageTitle,  WebsiteSection.SectionName + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId left outer join SubMenuMaster on PageMaster.SubMenuId=SubMenuMaster.SubMenuId left outer join LanguageMaster on LanguageMaster.Id = PageMaster.LanguageId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' ";
        strcln = " SELECT DISTINCT dbo.SubMenuMaster.SubMenuName, dbo.SubMenuMaster.SubMenuId, dbo.PageMaster.PageId, dbo.PageMaster.FolderName, dbo.MainMenuMaster.MainMenuName, dbo.MainMenuMaster.MainMenuId, dbo.PageMaster.Active, dbo.VersionInfoMaster.VersionInfoName, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.ReportMasterFunctionality.MasterpageID FROM dbo.VersionInfoMaster INNER JOIN dbo.PageMaster ON dbo.PageMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId LEFT OUTER JOIN dbo.MainMenuMaster ON dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId INNER JOIN dbo.ReportMasterFunctionality ON dbo.PageMaster.PageId = dbo.ReportMasterFunctionality.Pageid WHERE (dbo.PageMaster.Active = '1') and dbo.ReportMasterFunctionality.MasterpageID=" + FilterProduct.SelectedValue + "";
        if (FilterProduct.SelectedIndex > 0)
        {
            str1 = "and ReportMasterFunctionality.MasterpageID='" + FilterProduct.SelectedValue + "' ";
        }


        if (TextBox5.Text != "")
        {
            str4 += "  and (PageMaster.PageName Like '%" + TextBox5.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox5.Text + "%' ) ";
        }
        string orderby = "order by  PageMaster.PageTitle";
        strcln = strcln + str1 + str2 + str3 + str4 + orderby;
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GV_pagelist.DataSource = dtcln;
        //DataView myDataView = new DataView();
        //myDataView = dtcln.DefaultView;

        //if (hdnsortExp.Value != string.Empty)
        //{
        //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //}

        GV_pagelist.DataBind();
    }
  
    protected void GV_pagelist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            // lbllegend.Text = "Edit Rules for sending Reprots";

            GV_pagelist.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GV_pagelist.DataKeys[GV_pagelist.SelectedIndex].Value.ToString());
            ViewState["pageid"] = i.ToString();

            string comd = "Select * from PageMaster where PageId=" + ViewState["pageid"].ToString();
            SqlCommand cmall = new SqlCommand(comd, con);
            DataTable dtall = new DataTable();
            SqlDataAdapter adpall = new SqlDataAdapter(cmall);
            adpall.Fill(dtall);
            if (dtall.Rows.Count > 0)
            {
                lbl_pagename.Text = dtall.Rows[0]["PageName"].ToString();
                lbl_titel.Text = dtall.Rows[0]["PageTitle"].ToString();
            }
            ddlpagename.SelectedValue = ViewState["pageid"].ToString();
        }
      
    }  

   

  
    protected void GV_search_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
    protected void GV_search_Sorting(object sender, GridViewSortEventArgs e)
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
   
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }

    //*-----------------------------***************

    protected void Fillwarehouse()
    {

        ddlfilterstore.Items.Clear();
        string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "' and [WareHouseMaster].Status='1' order by Name ";
        SqlCommand cmd = new SqlCommand(str, conOADB);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
       

        ddlfilterstore.DataSource = ds;
        ddlfilterstore.DataTextField = "Name";
        ddlfilterstore.DataValueField = "WareHouseId";
        ddlfilterstore.DataBind();
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlfilterstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

    }
    protected void filltimezone()
    {
        string str1 = "SELECT ID,[Name]+':'+ShortName+':'+gmt AS TimeZone FROM [TimeZoneMaster] where  TimeZoneMaster.Status ='1'";
        SqlCommand cmd1 = new SqlCommand(str1, conOADB);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        da1.Fill(ds1);

        ddlwztimezone.DataSource = ds1;
        ddlwztimezone.DataTextField = "TimeZone";
        ddlwztimezone.DataValueField = "ID";
        ddlwztimezone.DataBind();

        string bus = "select top(1) * from WHTimeZone where WHID = '" + ddlfilterstore.SelectedValue + "'  order by Id desc ";
        SqlCommand cmdbus = new SqlCommand(bus, conOADB);
        SqlDataAdapter adpbus = new SqlDataAdapter(cmdbus);
        DataTable dtbus = new DataTable();
        adpbus.Fill(dtbus);
        if (dtbus.Rows.Count > 0)
        {
            ddlwztimezone.SelectedIndex = ddlwztimezone.Items.IndexOf(ddlwztimezone.Items.FindByValue(dtbus.Rows[0]["TimeZone"].ToString()));
        }
    }
    protected void fillfilteremp()
    {
        ddlfilteremployee.Items.Clear();
        SqlDataAdapter adpt = new SqlDataAdapter("select EmployeeMaster.EmployeeMasterID, EmployeeMaster.EmployeeName   from EmployeeMaster inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join WareHouseMaster on EmployeeMaster.Whid = WareHouseMaster.WareHouseId where EmployeeMaster.Active='1' and  EmployeeMaster.WorkEmail !='' and EmployeeMaster.Whid='" + ddlfilterstore.SelectedValue + "' order by EmployeeName", conOADB);
        DataTable dsemp = new DataTable();
        adpt.Fill(dsemp);
        ddlfilteremployee.DataSource = dsemp;
        ddlfilteremployee.DataTextField = "EmployeeName";
        ddlfilteremployee.DataValueField = "EmployeeMasterID";
        ddlfilteremployee.DataBind();
        ddlfilteremployee.Items.Insert(0, "All");
        ddlfilteremployee.Items[0].Value = "0";
    }
    protected void ddlfilterstore_SelectedIndexChanged(object sender, EventArgs e)
    {        
        fillfilteremp();
        string bus = "select top(1) * from WHTimeZone where WHID = '" + ddlfilterstore.SelectedValue + "'  order by Id desc ";
        SqlCommand cmdbus = new SqlCommand(bus, conOADB);
        SqlDataAdapter adpbus = new SqlDataAdapter(cmdbus);
        DataTable dtbus = new DataTable();
        adpbus.Fill(dtbus);
        if (dtbus.Rows.Count > 0)
        {
            ddlwztimezone.SelectedIndex = ddlwztimezone.Items.IndexOf(ddlwztimezone.Items.FindByValue(dtbus.Rows[0]["TimeZone"].ToString()));
        }
    }
    protected void ddlfilteremployee_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void BtnGo_Click(object sender, EventArgs e)
    {
        try
        {
            FillGV_pagelist();
        }
        catch (Exception ex)
        {
        }
    }
  
  

    
    //Add EMp
    protected void imgadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PagesAutoGeneratedReportsAddManage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgrefrole_Click(object sender, ImageClickEventArgs e)
    {
        Fillpages();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (pnl_search.Visible == true)
        {
            pnl_search.Visible = false;
            btnsearch.Text = "Search"; 
        }
        else
        {
            pnl_search.Visible = true;
            btnsearch.Text = "Hide Search";
            FillGV_pagelist();

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        lblempmsg.Visible = false; 
        if (ddlfilteremployee.SelectedIndex > 0)
        {

            String filename = "";
            string audiofile = "";
            DataTable dt = new DataTable();
           
                int add =1;
                foreach (GridViewRow gdr in gridFileAttach.Rows)
                {
                    Label lblempid = (Label)gdr.FindControl("lblempid");

                    if (ddlfilteremployee.SelectedValue == lblempid.Text)
                    {
                        add = 0;
                    }
                }
                if (add == 1)
                {
                    if (Session["GridFileAttach1"] == null)
                    {
                    lblempmsg.Visible = false; 
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "employeename";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);

                    DataColumn dtcom3 = new DataColumn();
                    dtcom3.DataType = System.Type.GetType("System.String");
                    dtcom3.ColumnName = "employee";
                    dtcom3.ReadOnly = false;
                    dtcom3.Unique = false;
                    dtcom3.AllowDBNull = true;
                    dt.Columns.Add(dtcom3);

                    DataColumn dtcom4 = new DataColumn();
                    dtcom4.DataType = System.Type.GetType("System.String");
                    dtcom4.ColumnName = "time";
                    dtcom4.ReadOnly = false;
                    dtcom4.Unique = false;
                    dtcom4.AllowDBNull = true;
                    dt.Columns.Add(dtcom4);

                    DataColumn dtcom5 = new DataColumn();
                    dtcom5.DataType = System.Type.GetType("System.String");
                    dtcom5.ColumnName = "timezoneid";
                    dtcom5.ReadOnly = false;
                    dtcom5.Unique = false;
                    dtcom5.AllowDBNull = true;
                    dt.Columns.Add(dtcom5);

                    DataColumn dtcom6 = new DataColumn();
                    dtcom6.DataType = System.Type.GetType("System.String");
                    dtcom6.ColumnName = "timezone";
                    dtcom6.ReadOnly = false;
                    dtcom6.Unique = false;
                    dtcom6.AllowDBNull = true;
                    dt.Columns.Add(dtcom6);
                    }
                    else
                    {
                        dt = (DataTable)Session["GridFileAttach1"];
                    }
                    DataRow dtrow = dt.NewRow();
                    dtrow["employee"] = ddlfilteremployee.SelectedValue;
                    dtrow["employeename"] = ddlfilteremployee.SelectedItem.Text;

                    dtrow["time"] = txtstarttime.Text;
                    dtrow["timezoneid"] = ddlwztimezone.SelectedValue;
                    dtrow["timezone"] = ddlwztimezone.SelectedItem.Text;
                    // dtrow["FileNameChanged"] = hdnFileName.Value;
                    dt.Rows.Add(dtrow);
                    Session["GridFileAttach1"] = dt;
                    if (Session["GridFileAttach1"] != null)
                    {
                        gridFileAttach.DataSource = dt;
                        gridFileAttach.DataBind();
                    }
                }
                else
                {
                    lblempmsg.Visible = true;  
                }

                ddlfilteremployee.SelectedIndex = 0;
                txtstarttime.Text = "";  
          
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gdr in gridFileAttach.Rows)
        {
            Label lblempid = (Label)gdr.FindControl("lblempid");
            Label lbltime = (Label)gdr.FindControl("lbltime");
            Label lbltimezoneid = (Label)gdr.FindControl("lbltimezoneid");
            string SubMenuInsert = "Insert Into ReprotmasterRrecipentEmployeeTbl (PageID ,EmployeeID, time, tiezoneid,Pagerepoid) values ('" + ddlpagename.SelectedValue + "','" + lblempid.Text + "' ,'" + lbltime.Text + "' ," + lbltimezoneid.Text + "," + ViewState["Pagerepoid"].ToString() + ")";
            SqlCommand cmd = new SqlCommand(SubMenuInsert, conOADB);
            conOADB.Open();
            cmd.ExecuteNonQuery();
            conOADB.Close();
        }
        Fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
            lblempmsg.Visible = false; 
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lbllegend.Text = "";

    }
    
    protected void gridFileAttach_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete1")
        {
            gridFileAttach.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            if (Session["GridFileAttach1"] != null)
            {
                if (gridFileAttach.Rows.Count > 0)
                {
                    dt = (DataTable)Session["GridFileAttach1"];

                    dt.Rows.Remove(dt.Rows[gridFileAttach.SelectedIndex]);


                    gridFileAttach.DataSource = dt;
                    gridFileAttach.DataBind();
                    Session["GridFileAttach1"] = dt;
                }
            }

        }
    }



    //-----------------------------------------------------------------
    //---------------------------------------------------------------

  
   
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
    }
   
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {      
        
    }
        //----------------------

    protected void Fillgrid()
    {
        string comd = " SELECT DISTINCT  Pagerepoid as Pagerepoid FROM dbo.ReprotmasterRrecipentEmployeeTbl ";

        string idofoadb = "1,";
        SqlCommand cmall = new SqlCommand(comd, conOADB);
        DataTable dtall = new DataTable();
        SqlDataAdapter adpall = new SqlDataAdapter(cmall);
        adpall.Fill(dtall);
        if (dtall.Rows.Count > 0)
        {
            //idofoadb = dtall.Rows[0]["Pagerepoid"].ToString();
            foreach (DataRow dr in dtall.Rows)
            {
                idofoadb += dr["Pagerepoid"].ToString() + ",";
            }
        }
        if (idofoadb.Length > 0)
        {
            idofoadb = idofoadb.Remove(idofoadb.Length - 1);

        }
        string str = "";
        string stractive = "";
        string categs = "";
        str = " SELECT dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.ReprotmasterRrecipentEmployeeTbl.Id, dbo.ReprotmasterRrecipentEmployeeTbl.EmployeeID, dbo.PageMaster.PageId FROM  dbo.ReprotmasterRrecipentEmployeeTbl INNER JOIN dbo.PageMaster ON dbo.ReprotmasterRrecipentEmployeeTbl.PageID = dbo.PageMaster.PageId ";

        str = " SELECT  dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageId, dbo.ReportMasterFunctionality.Pageid , dbo.ReportMasterFunctionality.Id FROM            dbo.ReportMasterFunctionality INNER JOIN  dbo.PageMaster ON dbo.ReportMasterFunctionality.Pageid = dbo.PageMaster.PageId WHERE (dbo.ReportMasterFunctionality.Id IN (" + idofoadb + "))  ";
        string orderby = " ";
        string finalstr = str + categs + stractive + orderby;
        

        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);


        if (ds.Rows.Count > 0)
        {

            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

        }      
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {        
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {       
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {          
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int iikt = 0;
        if (e.CommandName == "Delete")
        {
            int mm = Convert.ToInt32(e.CommandArgument);
            string st2 = " Delete from ReprotmasterRrecipentEmployeeTbl where pagerepoid=" + mm;
            SqlCommand cmd2 = new SqlCommand(st2, conOADB);
            conOADB.Open();
            cmd2.ExecuteNonQuery();
            conOADB.Close();
           
             Fillgrid(); 
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully ";    
            
          
        }

        if (e.CommandName == "Edit")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);
            ViewState["editid"] = mm1.ToString();
            ViewState["MasterId"] = mm1.ToString();
            ViewState["Pagerepoid"] = mm1.ToString();
            SqlDataAdapter adp = new SqlDataAdapter("select * From ReportMasterFunctionality Where Id='" + mm1 + "'", con);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            filterproduct();//M
            Fillpages();//M
            ddlpagename.SelectedValue = ds.Rows[0]["Pageid"].ToString();
            FillGV_pagelist();//
            Fillwarehouse();//
            fillfilteremp();//

            // string stpageall = " select EmployeeMasterID as employee, EmployeeName as employeename,time,tiezoneid,Pagerepoid  from EmployeeMaster where EmployeeMasterID='" + ds.Rows[0]["EmployeeID"].ToString() + "'";
            string stpageall = " SELECT  dbo.EmployeeMaster.EmployeeMasterID AS employee, dbo.EmployeeMaster.EmployeeName AS employeename, dbo.ReprotmasterRrecipentEmployeeTbl.time,  dbo.ReprotmasterRrecipentEmployeeTbl.tiezoneid as timezoneid, dbo.ReprotmasterRrecipentEmployeeTbl.Pagerepoid, dbo.ReprotmasterRrecipentEmployeeTbl.EmployeeID,  dbo.ReprotmasterRrecipentEmployeeTbl.PageID, dbo.ReprotmasterRrecipentEmployeeTbl.Id, TimeZoneMaster.Name+':'+ TimeZoneMaster.ShortName +':'+ TimeZoneMaster.gmt AS timezone FROM dbo.EmployeeMaster INNER JOIN dbo.ReprotmasterRrecipentEmployeeTbl ON dbo.EmployeeMaster.EmployeeMasterID = dbo.ReprotmasterRrecipentEmployeeTbl.EmployeeID INNER JOIN dbo.TimeZoneMaster ON dbo.ReprotmasterRrecipentEmployeeTbl.tiezoneid = dbo.TimeZoneMaster.ID Where dbo.ReprotmasterRrecipentEmployeeTbl.Pagerepoid=" + mm1 + "";
            SqlCommand cmall = new SqlCommand(stpageall, conOADB);
            DataTable dtall = new DataTable();
            SqlDataAdapter adpall = new SqlDataAdapter(cmall);
            adpall.Fill(dtall);
            if (dtall.Rows.Count > 0)
            {
                Session["GridFileAttach1"] = dtall;
                gridFileAttach.DataSource = dtall;
                gridFileAttach.DataBind();
            }
            pnladdnew.Visible = true;
            lblmsg.Text = "";
            lbllegend.Text = "Edit Rules for sending Reprots";
            btnupdate.Visible = true;
            Button1.Visible = false;
        }
    }
    protected void Btnupdate_Click(object sender, EventArgs e)
    {        
        string st2 = " Delete from ReprotmasterRrecipentEmployeeTbl where pagerepoid=" + ViewState["Pagerepoid"].ToString();
        SqlCommand cmd2 = new SqlCommand(st2, conOADB);
        conOADB.Open();
        cmd2.ExecuteNonQuery();
        conOADB.Close();
        foreach (GridViewRow gdr in gridFileAttach.Rows)
        {
            Label lblempid = (Label)gdr.FindControl("lblempid");
            Label lbltime = (Label)gdr.FindControl("lbltime");
            Label lbltimezoneid = (Label)gdr.FindControl("lbltimezoneid");
            string SubMenuInsert = "Insert Into ReprotmasterRrecipentEmployeeTbl (PageID ,EmployeeID, time, tiezoneid,Pagerepoid) values ('" + ddlpagename.SelectedValue + "','" + lblempid.Text + "' ,'" + lbltime.Text + "' ," + lbltimezoneid.Text + "," + ViewState["Pagerepoid"].ToString() + ")";
            SqlCommand cmd = new SqlCommand(SubMenuInsert, conOADB);
            conOADB.Open();
            cmd.ExecuteNonQuery();
            conOADB.Close();
        }
        Fillgrid();
        lblmsg.Visible = true;
        lblmsg.Text = "Record Update successfully";
        lblempmsg.Visible = false;
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lbllegend.Text = "";

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder1;
        Fillgrid();
    }
    public string sortOrder1
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

   
}
