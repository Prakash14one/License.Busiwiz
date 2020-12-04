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

    int filterprodctid=0;
    protected void Page_Load(object sender, EventArgs e)
    {
       
       Session["ClientId"] = "35";

        lblmsg.Text = "";
        if (!IsPostBack)
        {
            lblVersion.Text = "This PageVersion Is V4  Date:3March2017";
            lblpagename.Text = "";
           // TextBox5.Attributes.Add("onKeyPress", "doClick('" + BtnGo.ClientID + "',event)");
            if (Request.QueryString["PageId"] != null)
            {

                int id = Convert.ToInt32(Request.QueryString["PageId"]);
                FillEdit();
            }
            

            ViewState["sortOrder"] = "";
            FillProduct();
            versionid();
            FillCategory();
            FillMainmenu();
            FillSubMenu();
            fillLanguage();


            
            filterproduct();
            FillterCategorysearch();
            filtermainmenu();
            filtersubmenu();
           
            FillGrid();

            FilFolder();

            functionality();

            chkupload_CheckedChanged(sender, e);
            if (filterprodctid == 1)
            {
                BtnGo_Click(sender, e);
            }
        }

        if (pnladdnew.Visible == true)
        {
            btnSubmit.Focus();
        }
        else
        {
            BtnGo.Focus();
            
             
        }

     //   pnladdnew.DefaultButton = "BtnGo";
      

    }

    protected void Fillpages()
    {

        ddlpagename.Items.Clear();
        if (ddlProductname.SelectedIndex > 1)
        {
            string strcln = "";
            strcln = " SELECT distinct MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName as Page_Name from      dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id INNER JOIN dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteSectionId = dbo.MasterPageMaster.WebsiteSectionId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.ReportMasterFunctionality ON dbo.PageMaster.PageId = dbo.ReportMasterFunctionality.Pageid  where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MainMenuMaster.MasterPage_Id='" + ddlProductname.SelectedValue + "' and  PageMaster.PageName !='' and MainMenuMaster.MainMenuName !='' ";
            strcln = " SELECT DISTINCT  dbo.MainMenuMaster.MainMenuId, dbo.MainMenuMaster.MainMenuName, dbo.MainMenuMaster.BackColour, dbo.MainMenuMaster.MainMenuTitle,  dbo.MainMenuMaster.MasterPage_Id, dbo.MainMenuMaster.MainMenuIndex, dbo.MainMenuMaster.Active, dbo.MainMenuMaster.LanguageId,  dbo.MainMenuMaster.MainMenucatId, dbo.PageMaster.PageId,  dbo.PageMaster.PageName + '-' + dbo.PageMaster.PageTitle + '-' + dbo.MainMenuMaster.MainMenuName AS Page_Name FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id INNER JOIN dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteSectionId = dbo.MasterPageMaster.WebsiteSectionId Where MainMenuMaster.MasterPage_Id='" + ddlProductname.SelectedValue + "' ";
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
    protected void FillEdit()
    {
        Label1.Text = "Edit Product Page";
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;



        ViewState["pageid"] = Request.QueryString["PageId"];

        string strcln = "select pagemaster.*,MainMenuMaster.MasterPage_Id  from pagemaster inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId where pageid='" + ViewState["pageid"] + "'";

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            FillProduct();

            ddlProductname.SelectedIndex = ddlProductname.Items.IndexOf(ddlProductname.Items.FindByValue(dtcln.Rows[0]["MasterPage_Id"].ToString()));
            Session["pageid"] = dtcln.Rows[0]["MasterPage_Id"].ToString();

            versionid();


            string strcln1 = "SELECT TOP (1) PERCENT dbo.WebsiteMaster.ID, dbo.MasterPageMaster.MasterPageId, dbo.WebsiteMaster.RootFolderPath FROM            dbo.WebsiteMaster INNER JOIN   dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteMasterId = dbo.WebsiteMaster.ID INNER JOIN  dbo.MasterPageMaster ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId where  dbo.MasterPageMaster.MasterPageId= " + ddlProductname.SelectedValue + "";

            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);
            if (dtcln1.Rows.Count > 0)
            {
                lbl_rootpath.Text = dtcln1.Rows[0]["RootFolderPath"].ToString();
            }

            fillLanguage();
            ddlanguage.SelectedIndex = ddlanguage.Items.IndexOf(ddlanguage.Items.FindByValue(dtcln.Rows[0]["LanguageId"].ToString()));

            FillMainmenu();
            ddlMainMenu.SelectedIndex = ddlMainMenu.Items.IndexOf(ddlMainMenu.Items.FindByValue(dtcln.Rows[0]["MainMenuId"].ToString()));

            FillSubMenu();
            ddlSubmenu.SelectedIndex = ddlSubmenu.Items.IndexOf(ddlSubmenu.Items.FindByValue(dtcln.Rows[0]["SubMenuId"].ToString()));


            txtpagetitle.Text = dtcln.Rows[0]["PageTitle"].ToString();
            txtpagename.Text = dtcln.Rows[0]["PageName"].ToString();
            txtpagedescriptin.Text = dtcln.Rows[0]["PageDescription"].ToString();
            txtpageindex.Text = dtcln.Rows[0]["PageIndex"].ToString();
            txtFolderName.Text = dtcln.Rows[0]["FolderName"].ToString();
            txtFolderName.Visible = true;
            pnl_pathddl.Visible = false;
            chkpathselect.Checked = true;
            chkactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"]);
            if (Convert.ToString(dtcln.Rows[0]["ManuAccess"]) != "")
            {
                chkmanuaccess.Checked = Convert.ToBoolean(dtcln.Rows[0]["ManuAccess"]);
            }

            //****File
            string str1231 = " select PagemasterID, PageMaster_PdfFilename as  Title, PageMaster_AudioFileName as AudioURL, FileTitle as PDFURL , Date from PageMaster_FileUploadTbl where PagemasterID='" + dtcln.Rows[0]["MasterPage_Id"].ToString() + "'";

            SqlCommand cmd1231 = new SqlCommand(str1231, con);
            DataTable dt1231 = new DataTable();
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd1231);
            adp123.Fill(dt1231);

            if (dt1231.Rows.Count > 0)
            {

                gridFileAttach.DataSource = dt1231;
                gridFileAttach.DataBind();
            }
            //
            //Functinality edite
            if (FilterProduct.SelectedIndex >= 0)
            {
                DataTable dtff = select("select * from FunctionalityMasterTbl where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");
                pnl_funct.Visible = true;

                datalist2.DataSource = dtff;
                datalist2.DataBind();

                foreach (DataListItem gr in datalist2.Items)
                {
                    Label FunctionaID = (Label)gr.FindControl("Label51");

                    Label Label51 = (Label)gr.FindControl("Label51");
                    TextBox txtrenk = (TextBox)gr.FindControl("txtrenk");
                    CheckBox chkMsg11 = (CheckBox)gr.FindControl("chkMsg11");
                    int Rent;
                    DataTable dtffnew = select("select * from FunctionalityPageOrderTbl where FunctionalityMasterTblID='" + FunctionaID.Text + "' and PagemasterID='" + dtcln.Rows[0]["PageId"].ToString() + "'");
                    if (dtffnew.Rows.Count > 0)
                    {

                        try
                        {
                            Rent = Convert.ToInt32(dtffnew.Rows[0]["OrderNo"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Rent = 1;
                        }
                        chkMsg11.Checked = true;
                        txtrenk.Text = Convert.ToString(Rent);
                    }
                    else
                    {

                        DataTable dtffrenk = select("select max(OrderNo) as OrderNo from FunctionalityPageOrderTbl where FunctionalityMasterTblID='" + FunctionaID.Text + "'");// where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");
                        pnl_funct.Visible = true;

                        try
                        {
                            Rent = 1 + Convert.ToInt32(dtffrenk.Rows[0]["OrderNo"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Rent = 1;
                        }

                        txtrenk.Text = Convert.ToString(Rent);
                    }
                }
            }


            btnSubmit.Text = "Update";


        }
    }


    protected void ddlMainMenu_SelectedIndexChangedMainFolder(object sender, EventArgs e)
    {
        if(ddl_MainFolder.SelectedIndex > 0)
        {
        txtFolderName.Text = ddl_MainFolder.SelectedItem.Text;
        FillSubfolder();
        }
    }

    protected void ddlMainMenu_SelectedIndexChangedsubFolder(object sender, EventArgs e)
    {
        if (ddl_subfolder.SelectedIndex > 0)
        {
            txtFolderName.Text = ddl_MainFolder.SelectedItem.Text + "/" + ddl_subfolder.SelectedItem.Text;
            FillSubSubFolder();
        }
        else
        {
         
        }
    }

    protected void ddlMainMenu_SelectedIndexChangedSubsubFolder(object sender, EventArgs e)
    {
        if (ddl_SubSubfolder.SelectedIndex > 0)
        {
            txtFolderName.Text = ddl_MainFolder.SelectedItem.Text + "/" + ddl_subfolder.SelectedItem.Text + "/" + ddl_SubSubfolder.SelectedItem.Text;
        }
  
       
    }

    protected void FilFolder()
    {

        ddl_MainFolder.Items.Clear();
        string strcln = " SELECT  * From FolderCategoryMaster1 where  Activestatus='Active'   order By FolderCatName ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddl_MainFolder.DataSource = dtcln;
        ddl_MainFolder.DataValueField = "FolderMasterId";
        ddl_MainFolder.DataTextField = "FolderCatName";

        ddl_MainFolder.DataBind();
        ddl_MainFolder.Items.Insert(0, "---Select Main Folder---");



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
    protected void ddlfuncti_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterGrid();
    }
    protected void FillSubfolder()
    {

        ddl_subfolder.Items.Clear();
        string strcln = " Select * From FolderSubCatName Where Activestatus='1' and FolderMasterId='" + ddl_MainFolder.SelectedValue + "' Order By FolderSubName ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddl_subfolder.DataSource = dtcln;
        ddl_subfolder.DataValueField = "FolderSubId";
        ddl_subfolder.DataTextField = "FolderSubName";
        ddl_subfolder.DataBind();
        ddl_subfolder.Items.Insert(0, "---Select Sub Folder---");



    }

    protected void FillSubSubFolder()
    {

        ddl_SubSubfolder.Items.Clear();
        string strcln = " SELECT  * From FolderSubSubCategory Where Activestatus='1' and FolderSubId='" + ddl_subfolder.SelectedValue + "' Order By FolderSubSubName ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddl_SubSubfolder.DataSource = dtcln;
        ddl_SubSubfolder.DataValueField = "FolderSubSubId";
        ddl_SubSubfolder.DataTextField = "FolderSubSubName";
        ddl_SubSubfolder.DataBind();
        ddl_SubSubfolder.Items.Insert(0, "---Select SubSub Folder---");


    }
    //**
    public void FillFunctionality()
    {
        if (FilterProduct.SelectedIndex >= 0)
        {
            DataTable dtff = select("select * from FunctionalityMasterTbl where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");
            pnl_funct.Visible = true;
         
            datalist2.DataSource = dtff;
            datalist2.DataBind();

            foreach (DataListItem gr in datalist2.Items)
            {
                Label FunctionaID = (Label)gr.FindControl("Label51");

                Label Label51 = (Label)gr.FindControl("Label51");
                TextBox txtrenk = (TextBox)gr.FindControl("txtrenk");
                DataTable dtffrenk = select("select max(OrderNo) as OrderNo from FunctionalityPageOrderTbl where FunctionalityMasterTblID='" + FunctionaID.Text + "'");// where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");
                pnl_funct.Visible = true;
                int Rent;
                try
                {
                     Rent = 1 + Convert.ToInt32(dtffrenk.Rows[0]["OrderNo"].ToString());
                }
                catch (Exception ex)
                {
                    Rent = 1;
                }
                
                txtrenk.Text =Convert.ToString(Rent);   
                
            }
        }
        else
        {
            pnl_funct.Visible = false;
     
            datalist2.DataSource = null;
            datalist2.DataBind();
        }
    }
    //**
    //***********File Upload******
    protected void chkupload_CheckedChanged(object sender, EventArgs e)
    {
        if (chkupload.Checked == true)
        {
            pnlup.Visible = true;
        }
        else
        {
            pnlup.Visible = false;
        }

        if (chkuploadFunction.Checked == true)
        {
            pnl_funct.Visible = true;
        }
        else
        {
            pnl_funct.Visible = false;
        }


        if (chkpathselect.Checked == true)
        {
            pnl_pathddl.Visible = false;
            txtFolderName.Visible = true;  
        }
        else
        {
            pnl_pathddl.Visible = true;
            txtFolderName.Visible = false;
        }
    }

    protected void check_autoreport_CheckedChanged(object sender, EventArgs e)
    {
        if (check_autoreport.Checked == true)
        {
            pnlpage.Visible = true;
        }
        else
        {
            pnlpage.Visible = false;
        }
    }

    protected void Upradio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Upradio.SelectedValue == "1")
        {
            pnladio.Visible = true;
            pnlpdfup.Visible = false;
        }
        else
        {
            pnladio.Visible = false;
            pnlpdfup.Visible = true;
        }
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
    protected void btnup_Click(object sender, EventArgs e)
    {
        if (pnlup.Visible == true)
        {
            pnlup.Visible = false;
        }
        else
        {
            pnlup.Visible = true;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        string ext = "";
        string[] validFileTypes = { "bmp", "mp3", "gif", "mp4", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "pdf", "PDF", "MP3", "MP4", "wma", "html", "css", "rar", "zip", "rpt" };
        string[] validFileTypes1 = { "MP3", "MP4", "mp3", "mp4", "pdf", "PDF", "wma", "html", "css", "rar", "zip", "rpt", "xls", "xlsx" };
        bool isValidFile = false;
        if (Upradio.SelectedValue == "1")
        {
            if (fileuploadaudio.HasFile == true)
            {
                ext = System.IO.Path.GetExtension(fileuploadaudio.PostedFile.FileName);
                for (int i = 0; i < validFileTypes1.Length; i++)
                {

                    if (ext == "." + validFileTypes1[i])
                    {

                        isValidFile = true;

                        break;

                    }

                }
            }
        }
        else
        {
            if (fileuploadadattachment.HasFile == true)
            {

                ext = System.IO.Path.GetExtension(fileuploadadattachment.PostedFile.FileName);
                for (int i = 0; i < validFileTypes.Length; i++)
                {

                    if (ext == "." + validFileTypes[i])
                    {

                        isValidFile = true;

                        break;

                    }

                }
            }
        }


        if (!isValidFile)
        {

            lblmsg.Visible = true;
            if (Upradio.SelectedValue == "1")
            {
                lblmsg.Text = "Invalid File. Please upload a File with extension " +

                               string.Join(",", validFileTypes1);
            }
            else
            {
                lblmsg.Text = "Invalid File. Please upload a File with extension " +

                              string.Join(",", validFileTypes);
            }

        }

        else
        {

            String filename = "";
            string audiofile = "";
            //PnlFileAttachLbl.Visible = true;
            if (fileuploadadattachment.HasFile || fileuploadaudio.HasFile)
            {
                if (fileuploadadattachment.HasFile)
                {
                    filename = fileuploadadattachment.FileName;

                    //filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fileuploadadattachment.FileName;
                    fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Attach\\") + filename);
                }
                if (fileuploadaudio.HasFile)
                {
                    audiofile = fileuploadaudio.FileName;
                    fileuploadaudio.PostedFile.SaveAs(Server.MapPath("~\\Attach\\") + audiofile);
                }
                //hdnFileName.Value = filename;
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

                    DataColumn dtcom3 = new DataColumn();
                    dtcom3.DataType = System.Type.GetType("System.String");
                    dtcom3.ColumnName = "Title";
                    dtcom3.ReadOnly = false;
                    dtcom3.Unique = false;
                    dtcom3.AllowDBNull = true;
                    dt.Columns.Add(dtcom3);

                    DataColumn dtcom4 = new DataColumn();
                    dtcom4.DataType = System.Type.GetType("System.String");
                    dtcom4.ColumnName = "AudioURL";
                    dtcom4.ReadOnly = false;
                    dtcom4.Unique = false;
                    dtcom4.AllowDBNull = true;
                    dt.Columns.Add(dtcom4);

                }
                else
                {
                    dt = (DataTable)Session["GridFileAttach1"];
                }
                DataRow dtrow = dt.NewRow();
                dtrow["PDFURL"] = filename;
                dtrow["Title"] = txttitlename.Text;
                dtrow["AudioURL"] = audiofile;

                // dtrow["FileNameChanged"] = hdnFileName.Value;
                dt.Rows.Add(dtrow);
                Session["GridFileAttach1"] = dt;
                gridFileAttach.DataSource = dt;


                gridFileAttach.DataBind();
                txttitlename.Text = "";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please Attach File to Upload.";
                return;
            }
        }
    }

    //********File Upload Close**********
    protected void FillProduct()
    {

        ddlProductname.Items.Clear();
        string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,  ProductMaster.ProductName + ':' +   VersionInfoMaster.VersionInfoName + ' : ' + WebsiteMaster.WebsiteName + ':' +   WebsiteSection.SectionName + ':' +   MasterPageMaster.MasterPageName  as MasterPage_Name  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by MasterPage_Name";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "MasterPageId";
        ddlProductname.DataTextField = "MasterPage_Name";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");
        ddlProductname.Items[0].Value = "0";

        }
    protected void versionid()
    {
        string strcln2 = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and MasterPageMaster.MasterPageId='" + ddlProductname.SelectedValue + "' order  by VersionInfoMaster.VersionInfoId Asc";
        SqlCommand cmdcln2 = new SqlCommand(strcln2, con);
        DataTable dtcln2 = new DataTable();
        SqlDataAdapter adpcln2 = new SqlDataAdapter(cmdcln2);
        adpcln2.Fill(dtcln2);
        if (dtcln2.Rows.Count > 0)
        {
            ViewState["VersionInfoId"] = dtcln2.Rows[0]["VersionInfoId"].ToString();
        }
        
    }
    protected void fillLanguage()
    {
        ddlanguage.Items.Clear();
            string strlan = "select Id,Name from LanguageMaster";
            SqlCommand cmdlan = new SqlCommand(strlan, con);
            SqlDataAdapter adplan = new SqlDataAdapter(cmdlan);
            DataSet dslan = new DataSet();
            adplan.Fill(dslan);

            ddlanguage.DataSource = dslan;
            ddlanguage.DataTextField = "Name";
            ddlanguage.DataValueField = "Id";
            ddlanguage.DataBind();

       
    }
    protected void FillCategory()
    {

        string strlan = "select * from Mainmenucategory where MasterPage_Id='" + ddlProductname.SelectedValue + "'";
        SqlCommand cmdlan = new SqlCommand(strlan, con);
        SqlDataAdapter adplan = new SqlDataAdapter(cmdlan);
        DataSet dslan = new DataSet();
        adplan.Fill(dslan);
        ddlcategory.DataSource = dslan;
        ddlcategory.DataTextField = "MainMenuCatName";
        ddlcategory.DataValueField = "MainMenucatId";
        ddlcategory.DataBind();
        ddlcategory.Items.Insert(0, "-Select-");
        ddlcategory.Items[0].Value = "0";
    }
   
    protected void FillMainmenu()
    {
        ddlMainMenu.Items.Clear();
        string filter = "";
        if (ddlcategory.SelectedIndex > 0)
        {
            filter = " and MainMenuMaster.MainMenucatId='" + ddlcategory.SelectedValue + "' ";
        }
        if (ddlProductname.SelectedIndex > -1)
        {
            string strcln = " SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + ddlProductname.SelectedValue + "' " + filter + " Order By MainMenuMaster.MainMenuTitle ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                ddlMainMenu.DataSource = dtcln;
                ddlMainMenu.DataValueField = "MainMenuId";
                ddlMainMenu.DataTextField = "Page_Name";
                ddlMainMenu.DataBind();

              //  ddlMainMenu.SelectedIndex = 1;  

            }
            
        }
        else
        {
            ddlMainMenu.DataSource = null;
            ddlMainMenu.DataValueField = "MainMenuId";
            ddlMainMenu.DataTextField = "MainMenuTitle";
            ddlMainMenu.DataBind();

           
           
        }
    }

    protected void FillSubMenu()
    {
        

        if (ddlMainMenu.SelectedIndex > -1)
        {
            string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + ddlProductname.SelectedValue + "' and SubMenuMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "'  Order By SubMenuName ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlSubmenu.DataSource = dtcln;
            ddlSubmenu.DataValueField = "SubMenuId";
            ddlSubmenu.DataTextField = "SubMenuName";
            ddlSubmenu.DataBind();

            ddlSubmenu.Items.Insert(0, "-Select-");
            ddlSubmenu.Items[0].Value = "0";
           
           
        }
        else
        {
            string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + ddlProductname.SelectedValue + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlSubmenu.DataSource = dtcln;
            ddlSubmenu.DataValueField = "SubMenuId";
            ddlSubmenu.DataTextField = "SubMenuName";
            ddlSubmenu.DataBind();

            ddlSubmenu.Items.Insert(0, "-Select-");
            ddlSubmenu.Items[0].Value = "0";
           
        }

     
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Session["SelectedIndex"] = ddlProductname.SelectedIndex;   
        int iti = 0;
        if (ddlSubmenu.SelectedIndex > 0)
        {
            string strpage = "  select MainMenuId  from  SubMenuMaster  where SubMenuId='" + ddlSubmenu.SelectedValue + "'";

            SqlCommand cmallo = new SqlCommand(strpage, con);
            DataTable dtallo = new DataTable();
            SqlDataAdapter adallo = new SqlDataAdapter(cmallo);
            adallo.Fill(dtallo);
            if (dtallo.Rows.Count > 0)
            {
                if (Convert.ToString(dtallo.Rows[0]["MainMenuId"]) != Convert.ToString(ddlMainMenu.SelectedValue))
                {
                    iti = 1;
                }
            }
            else
            {
                iti = 1;
            }

        }
        if (iti == 0)
        {
            try
            {
                string filtersub = "";
                if (ddlSubmenu.SelectedIndex > 0)
                {
                    filtersub = " and SubMenuId='" + ddlSubmenu.SelectedValue + "'"; 
                }
                if (btnSubmit.Text == "Update")
                {

                    string stpageall = "select *  from PageMaster  where PageName='" + txtpagename.Text + "' and MainMenuId='" + ddlMainMenu.SelectedValue + "' "+filtersub +" and pageid<>'" + ViewState["pageid"] + "'";

                    SqlCommand cmall = new SqlCommand(stpageall, con);
                    DataTable dtall = new DataTable();
                    SqlDataAdapter adpall = new SqlDataAdapter(cmall);
                    adpall.Fill(dtall);
                    if (dtall.Rows.Count == 0)
                    {
                        if (ddlSubmenu.SelectedIndex > 0)
                        {
                            string str = "update   PageMaster set   LanguageId = '" + ddlanguage.SelectedValue + "',PageName= '" + txtpagename.Text + "' ,PageTitle='" + txtpagetitle.Text + "', PageDescription='" + txtpagedescriptin.Text + "',pageIndex='" + txtpageindex.Text + "',VersionInfoMasterId='" + ViewState["VersionInfoId"].ToString() + "',MainMenuId='" + ddlMainMenu.SelectedValue + "',FolderName='" + txtFolderName.Text + "',Active='" + chkactive.Checked + "',SubMenuId='" + ddlSubmenu.SelectedValue + "',ManuAccess='" + chkmanuaccess.Checked + "',labelimage='" + Label37.Text + "',linktoReport='" + check_autoreport.Checked + "',ReportPageId='" + ddlpagename.SelectedValue + "' where pageid='" + ViewState["pageid"] + "'";
                            SqlCommand cmd = new SqlCommand(str, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            lblmsg.Visible = true;
                            btnSubmit.Text = "Submit";
                            lblmsg.Text = "Record updated successfully";
                        }
                        else
                        {
                            string str = "update   PageMaster set  LanguageId = '" + ddlanguage.SelectedValue + "',PageName= '" + txtpagename.Text + "' ,PageTitle='" + txtpagetitle.Text + "', PageDescription='" + txtpagedescriptin.Text + "',pageIndex='" + txtpageindex.Text + "',VersionInfoMasterId='" + ViewState["VersionInfoId"].ToString() + "',MainMenuId='" + ddlMainMenu.SelectedValue + "',FolderName='" + txtFolderName.Text + "',Active='" + chkactive.Checked + "',SubMenuId='0',ManuAccess='" + chkmanuaccess.Checked + "',labelimage='" + Label37.Text + "' ,linktoReport='" + check_autoreport.Checked + "',ReportPageId='" + ddlpagename.SelectedValue + "' where pageid='" + ViewState["pageid"] + "'";
                            SqlCommand cmd = new SqlCommand(str, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            lblmsg.Visible = true;
                            btnSubmit.Text = "Submit";
                            lblmsg.Text = "Record updated successfully";
                        }
                        //

                        string strcln1 = "  select Max(PageMaster.PageId) as PageId  from PageMaster inner join VersionInfoMaster on PageMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and PageMaster.VersionInfoMasterId='" + ViewState["VersionInfoId"].ToString() + "'";

                        SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
                        DataTable dtcln1 = new DataTable();
                        SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
                        adpcln1.Fill(dtcln1);


                        if (dtcln1.Rows.Count > 0)
                        {

                           

                            foreach (GridViewRow gdr in gridFileAttach.Rows)
                            {
                                Label lbltitle = (Label)gdr.FindControl("lbltitle");
                                Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");
                                Label lblaudiourl = (Label)gdr.FindControl("lblaudiourl");

                                string strftpinsert1 = "(Delete From PageMaster_FileUploadTbl where PagemasterID=" + dtcln1.Rows[0]["PageId"] + ")";
                                con.Close();
                                con.Open();
                                SqlCommand cmdinsert1 = new SqlCommand(strftpinsert1, con);
                                cmdinsert1.ExecuteNonQuery();

                                con.Close(); 
                                string strftpinsert = "INSERT INTO PageMaster_FileUploadTbl (PagemasterID, PageMaster_PdfFilename, PageMaster_AudioFileName, FileTitle, Date) values('" + dtcln1.Rows[0]["PageId"] + "','" + lblpdfurl.Text + "','" + lblaudiourl.Text + "','" + lbltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                                SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                                con.Open();
                                cmdinsert.ExecuteNonQuery();
                                con.Close();

                            }

                        //Functionality Update
                        foreach (DataListItem gr in datalist2.Items)
                        {
                            CheckBox chkMsg11 = (CheckBox)gr.FindControl("chkMsg11");

                            Label FunctionaID = (Label)gr.FindControl("Label51");

                            Label Label51 = (Label)gr.FindControl("Label51");
                            TextBox txtrenk = (TextBox)gr.FindControl("txtrenk");
                            int pageid =Convert.ToInt32(ViewState["pageid"]);// Convert.ToInt32(dtcln1.Rows[0]["PageId"]);

                            if (chkMsg11.Checked == true)
                            {

                                DataTable dtff = select("select * from FunctionalityPageOrderTbl where FunctionalityMasterTblID='" + FunctionaID.Text + "' and OrderNo >='" + txtrenk.Text + "' ");//where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");
                                pnl_funct.Visible = true;

                                foreach (DataRow row in dtff.Rows)
                                {
                                    int orderno = Convert.ToInt32(row["OrderNo"].ToString());
                                    orderno = orderno + 1;
                                    int mid = Convert.ToInt32(row["id"].ToString());
                                    string str = "update   FunctionalityPageOrderTbl set  OrderNo = '" + orderno + "' where ID='" + mid + "'";
                                    SqlCommand cmd = new SqlCommand(str, con);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }


                                string strftpinsert1 = "Delete From FunctionalityPageOrderTbl where PagemasterID=" + pageid + " and FunctionalityMasterTblID='" + FunctionaID.Text + "'";
                                con.Close();
                                con.Open();
                                SqlCommand cmdinsert1 = new SqlCommand(strftpinsert1, con);
                                cmdinsert1.ExecuteNonQuery();

                                DataTable dtvac = new DataTable();
                                string sds = Convert.ToString(ViewState["datavac"]);
                                string strftpinsert = "INSERT INTO FunctionalityPageOrderTbl (FunctionalityMasterTblID, PagemasterID, OrderNo) values('" + FunctionaID.Text + "'," + pageid + ",'" + txtrenk.Text + "')";
                                SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                                con.Close();
                                con.Open();
                                cmdinsert.ExecuteNonQuery();
                                con.Close();



                                //Reset All Order to 1 To N
                                int ordernonew = 0;
                                DataTable dtffnewupdate = select("select * from FunctionalityPageOrderTbl where FunctionalityMasterTblID='" + FunctionaID.Text + "' order by OrderNo Asc");//where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");
                                foreach (DataRow row in dtffnewupdate.Rows)
                                {
                                    ordernonew = ordernonew + 1;
                                    int mid = Convert.ToInt32(row["id"].ToString());
                                    string str = "update   FunctionalityPageOrderTbl set  OrderNo = '" + ordernonew + "' where ID='" + mid + "'";
                                    SqlCommand cmd = new SqlCommand(str, con);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }

                        resetall();
                        pnladdnew.Visible = false;
                        addnewpanel.Visible = true;
                        Label1.Text = "";
                        ModernpopSync.Show();
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Sorry,Record already existed.";
                    }



                  


                }
                else
                {

                    string stpageall = "select *  from PageMaster  where PageName='" + txtpagename.Text + "' and MainMenuId='" + ddlMainMenu.SelectedValue + "' "+filtersub;

                    SqlCommand cmall = new SqlCommand(stpageall, con);
                    DataTable dtall = new DataTable();
                    SqlDataAdapter adpall = new SqlDataAdapter(cmall);
                    adpall.Fill(dtall);
                    if (dtall.Rows.Count == 0)
                    {
                        if (ddlSubmenu.SelectedIndex > 0)
                        {
                            string str = "INSERT INTO PageMaster(PageName,PageTitle,PageDescription,PageIndex,VersionInfoMasterId,MainMenuId,FolderName,Active,SubMenuId,LanguageId,ManuAccess,labelimage) " +
                                       "VALUES('" + txtpagename.Text + "','" + txtpagetitle.Text + "','" + txtpagedescriptin.Text + "','" + txtpageindex.Text + "','" + ViewState["VersionInfoId"].ToString() + "','" + ddlMainMenu.SelectedValue + "','" + txtFolderName.Text + "','" + chkactive.Checked + "','" + ddlSubmenu.SelectedValue + "','" + ddlanguage.SelectedValue + "','" + chkmanuaccess.Checked + "','" + Label37.Text + "')";
                            SqlCommand cmd = new SqlCommand(str, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lblmsg.Visible = true;
                            lblmsg.Text = "Record inserted successfully";
                        }
                        else
                        {
                            string str = "INSERT INTO PageMaster(PageName,PageTitle,PageDescription,PageIndex,VersionInfoMasterId,MainMenuId,FolderName,Active,LanguageId,ManuAccess,labelimage,linktoReport, ReportPageId) " +
                                      " VALUES('" + txtpagename.Text + "','" + txtpagetitle.Text + "','" + txtpagedescriptin.Text + "','" + txtpageindex.Text + "','" + ViewState["VersionInfoId"].ToString() + "','" + ddlMainMenu.SelectedValue + "','" + txtFolderName.Text + "','" + chkactive.Checked + "','" + ddlanguage.SelectedValue + "','" + chkmanuaccess.Checked + "','" + Label37.Text + "','" + check_autoreport.Checked + "','"+ddlpagename.SelectedValue +"')";
                            SqlCommand cmd = new SqlCommand(str, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lblmsg.Visible = true;
                            lblmsg.Text = "Record inserted successfully";

                        }


                        string strcln1 = "  select Max(PageMaster.PageId) as PageId  from PageMaster inner join VersionInfoMaster on PageMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and PageMaster.VersionInfoMasterId='" + ViewState["VersionInfoId"].ToString() + "'";

                        SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
                        DataTable dtcln1 = new DataTable();
                        SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
                        adpcln1.Fill(dtcln1);


                        if (dtcln1.Rows.Count > 0)
                        {

                            con.Close();

                            foreach (GridViewRow gdr in gridFileAttach.Rows)
                            {
                                Label lbltitle = (Label)gdr.FindControl("lbltitle");
                                Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");
                                Label lblaudiourl = (Label)gdr.FindControl("lblaudiourl");

                                string strftpinsert = "INSERT INTO PageMaster_FileUploadTbl (PagemasterID, PageMaster_PdfFilename, PageMaster_AudioFileName, FileTitle, Date) values('" + dtcln1.Rows[0]["PageId"] + "','" + lblpdfurl.Text + "','" + lblaudiourl.Text + "','" + lbltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                                SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                                con.Open();
                                cmdinsert.ExecuteNonQuery();
                                con.Close();
                               
                            }

                            //Functionality order No
                          

                            foreach (DataListItem gr in datalist2.Items)
                            {
                                CheckBox chkMsg11 = (CheckBox)gr.FindControl("chkMsg11");

                                Label FunctionaID = (Label)gr.FindControl("Label51");

                                Label Label51 = (Label)gr.FindControl("Label51");
                                TextBox txtrenk= (TextBox)gr.FindControl("txtrenk");
                                int pageid = Convert.ToInt32(dtcln1.Rows[0]["PageId"]);

                                if (chkMsg11.Checked == true)
                                {

                                    DataTable dtff = select("select * from FunctionalityPageOrderTbl where FunctionalityMasterTblID='" + FunctionaID.Text + "' and OrderNo >='"+ txtrenk.Text + "' ");//where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");
                                    pnl_funct.Visible = true;

                                    foreach (DataRow row in dtff.Rows)
                                    {
                                        int orderno =Convert.ToInt32(row["OrderNo"].ToString());
                                        orderno = orderno + 1;
                                        int mid = Convert.ToInt32(row["id"].ToString());
                                        string str = "update   FunctionalityPageOrderTbl set  OrderNo = '" + orderno + "' where ID='" + mid + "'";
                                        SqlCommand cmd = new SqlCommand(str, con);
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }


                                    DataTable dtvac = new DataTable();
                                    string sds = Convert.ToString(ViewState["datavac"]);
                                    string strftpinsert = "INSERT INTO FunctionalityPageOrderTbl (FunctionalityMasterTblID, PagemasterID, OrderNo) values('" + FunctionaID.Text + "'," + pageid + ",'" + txtrenk.Text + "')";
                                    SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                                    con.Open();
                                    cmdinsert.ExecuteNonQuery();
                                    con.Close();


                                    //Reset All Order to 1 To N
                                    int ordernonew =0;
                                    DataTable dtffnewupdate = select("select * from FunctionalityPageOrderTbl where FunctionalityMasterTblID='" + FunctionaID.Text + "' order by OrderNo Asc");//where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");
                                    foreach (DataRow row in dtffnewupdate.Rows)
                                    {
                                        ordernonew = ordernonew + 1;
                                        int mid = Convert.ToInt32(row["id"].ToString());
                                        string str = "update   FunctionalityPageOrderTbl set  OrderNo = '" + ordernonew + "' where ID='" + mid + "'";
                                        SqlCommand cmd = new SqlCommand(str, con);
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }

                                }
                            }



                            lblpagename.Text = txtpagename.Text.ToString().Replace(" ", "");

                            string[] separator1 = new string[] { "." };
                            string[] strSplitArr1 = lblpagename.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                            string len = Convert.ToString(strSplitArr1.Length);
                            int pver = 0;
                            decimal pverdec = 0;

                            pverdec = 1;

                            string[] separ = new string[] { "." };
                            string[] strSplitAr = lblpagename.Text.Split(separ, StringSplitOptions.RemoveEmptyEntries);
                            string arr = Convert.ToString(strSplitAr.Length);
                            if (arr.Length > 1)
                            {
                                pver = Convert.ToInt32(pverdec.ToString().Remove(pverdec.ToString().Length - 2, 2));
                            }
                            else
                            {
                                pver = Convert.ToInt32(pverdec);
                            }
                            if (len.Length >= 1)
                            {
                                lblpagename.Text = strSplitArr1[0].ToString() + "Ver" + pver + "." + strSplitArr1[1].ToString();
                            }
                            ViewState["VerNo"] = pver.ToString();
                            // lblpagename.Text = "Version-" + pver;




                            string str1 = " insert into PageVersionTbl(PageMasterId,VersionName,Date,VersionNo,PageName,Active)values('" + dtcln1.Rows[0]["PageId"] + "','Version-1','" + System.DateTime.Now.ToShortDateString() + "','1','" + lblpagename.Text + "','1')";
                            SqlCommand cmd1 = new SqlCommand(str1, con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();

                        }
                        resetall();
                        Label1.Text = "";
                        addnewpanel.Visible = true;
                        pnladdnew.Visible = false;
                        ModernpopSync.Show();
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Sorry,Record already existed.";
                    }

                  
                }



            }
            catch (Exception ex)
            {

            }

        }
        else

        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, mainmanu  and subManu Relation is not match";
        }



        FillGrid();


    }



    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
     
        versionid();
        FillCategory(); 
        FillFunctionality();
        FillMainmenu();
        FillSubMenu();
        Fillpages();
        pnl_autorepo.Visible = true;  
        string strcln = "SELECT TOP (1) PERCENT dbo.WebsiteMaster.ID, dbo.MasterPageMaster.MasterPageId, dbo.WebsiteMaster.RootFolderPath, dbo.WebsiteSection.MainFolder,dbo.WebsiteSection.SubFolder, dbo.WebsiteSection.SubSubFolder , dbo.WebsiteSection.PageLocationPath FROM            dbo.WebsiteMaster INNER JOIN   dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteMasterId = dbo.WebsiteMaster.ID INNER JOIN  dbo.MasterPageMaster ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId where  dbo.MasterPageMaster.MasterPageId= " + ddlProductname.SelectedValue + "";

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            lbl_rootpath.Text = dtcln.Rows[0]["RootFolderPath"].ToString() +"";

            txtFolderName.Text = dtcln.Rows[0]["PageLocationPath"].ToString();
            FilFolder();
            try
            {
                ddl_MainFolder.SelectedValue = dtcln.Rows[0]["MainFolder"].ToString();

            }
            catch (Exception ex)
            {
            }

            try
            {
                FillSubfolder();
                ddl_subfolder.SelectedValue = dtcln.Rows[0]["SubFolder"].ToString();

            }
            catch (Exception ex)
            {
            }

            try
            {
                FillSubSubFolder();
                ddl_SubSubfolder.SelectedValue = dtcln.Rows[0]["SubSubFolder"].ToString();

            }
            catch (Exception ex)
            {
            }
           
        }

     

        
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            Label1.Text = "Edit Product Page";
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;

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
                FillProduct();

                ddlProductname.SelectedIndex = ddlProductname.Items.IndexOf(ddlProductname.Items.FindByValue(dtcln.Rows[0]["MasterPage_Id"].ToString()));
               Session["pageid"]=  dtcln.Rows[0]["MasterPage_Id"].ToString();

                versionid();


                string strcln1 = "SELECT TOP (1) PERCENT dbo.WebsiteMaster.ID, dbo.MasterPageMaster.MasterPageId, dbo.WebsiteMaster.RootFolderPath FROM            dbo.WebsiteMaster INNER JOIN   dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteMasterId = dbo.WebsiteMaster.ID INNER JOIN  dbo.MasterPageMaster ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId where  dbo.MasterPageMaster.MasterPageId= " + ddlProductname.SelectedValue + "";

                SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
                DataTable dtcln1 = new DataTable();
                SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
                adpcln1.Fill(dtcln1);
                if (dtcln1.Rows.Count > 0)
                {
                    lbl_rootpath.Text = dtcln1.Rows[0]["RootFolderPath"].ToString();
                }
                
                fillLanguage();
                
                try
                {
                    ddlanguage.SelectedIndex = ddlanguage.Items.IndexOf(ddlanguage.Items.FindByValue(dtcln.Rows[0]["LanguageId"].ToString()));
                }
                catch (Exception ex)
                {
                }
                FillMainmenu();
                
                try
                {
                    ddlMainMenu.SelectedIndex = ddlMainMenu.Items.IndexOf(ddlMainMenu.Items.FindByValue(dtcln.Rows[0]["MainMenuId"].ToString()));
                }
                catch (Exception ex)
                {
                }
                FillSubMenu();
                try
                {
                    ddlSubmenu.SelectedIndex = ddlSubmenu.Items.IndexOf(ddlSubmenu.Items.FindByValue(dtcln.Rows[0]["SubMenuId"].ToString()));
                }
                catch (Exception ex)
                {
                }
                FillCategory();                 
                  try
                  {
                      pnl_autorepo.Visible = true;
                      pnlpage.Visible = false;  
                      Fillpages();
                      check_autoreport.Checked = Convert.ToBoolean(dtcln.Rows[0]["linktoReport"]);
                     
                      if (check_autoreport.Checked == true)
                      {
                          pnlpage.Visible = true;  
                          ddlpagename.SelectedIndex = ddlpagename.Items.IndexOf(ddlpagename.Items.FindByValue(dtcln.Rows[0]["ReportPageId"].ToString()));
                      }                      
                  }
                  catch (Exception ex)
                  {
                  }
                

                txtpagetitle.Text = dtcln.Rows[0]["PageTitle"].ToString();
                txtpagename.Text = dtcln.Rows[0]["PageName"].ToString();
                txtpagedescriptin.Text = dtcln.Rows[0]["PageDescription"].ToString();
                txtpageindex.Text = dtcln.Rows[0]["PageIndex"].ToString();
                txtFolderName.Text = dtcln.Rows[0]["FolderName"].ToString();
                Label37.Text = dtcln.Rows[0]["labelimage"].ToString();
                imglogo.ImageUrl = "~/images/" + Label37.Text + "";
                txtFolderName.Visible = true;
                pnl_pathddl.Visible = false;
                chkpathselect.Checked = true;  
                chkactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"]);
                if (Convert.ToString(dtcln.Rows[0]["ManuAccess"]) != "")
                {
                    chkmanuaccess.Checked = Convert.ToBoolean(dtcln.Rows[0]["ManuAccess"]);
                }

                //****File
                string str1231 = " select PagemasterID, PageMaster_PdfFilename as  Title, PageMaster_AudioFileName as AudioURL, FileTitle as PDFURL , Date from PageMaster_FileUploadTbl where PagemasterID='" + dtcln.Rows[0]["MasterPage_Id"].ToString() + "'";

                SqlCommand cmd1231 = new SqlCommand(str1231, con);
                DataTable dt1231 = new DataTable();
                SqlDataAdapter adp123 = new SqlDataAdapter(cmd1231);
                adp123.Fill(dt1231);

                if (dt1231.Rows.Count > 0)
                {
                     
                    gridFileAttach.DataSource = dt1231;
                    gridFileAttach.DataBind();
                }
                //
                //Functinality edite
                if (FilterProduct.SelectedIndex >= 0)
                {
                    DataTable dtff = select("select * from FunctionalityMasterTbl where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");
                    pnl_funct.Visible = true;

                    datalist2.DataSource = dtff;
                    datalist2.DataBind();

                    foreach (DataListItem gr in datalist2.Items)
                    {
                        Label FunctionaID = (Label)gr.FindControl("Label51");

                        Label Label51 = (Label)gr.FindControl("Label51");
                        TextBox txtrenk = (TextBox)gr.FindControl("txtrenk");
                        CheckBox chkMsg11 = (CheckBox)gr.FindControl("chkMsg11");
                        int Rent;
                        DataTable dtffnew = select("select * from FunctionalityPageOrderTbl where FunctionalityMasterTblID='" + FunctionaID.Text + "' and PagemasterID='" + dtcln.Rows[0]["PageId"].ToString() + "'");
                        if (dtffnew.Rows.Count > 0)
                        {
                            
                            try
                            {
                                Rent =  Convert.ToInt32(dtffnew.Rows[0]["OrderNo"].ToString());
                            }
                            catch (Exception ex)
                            {
                                Rent = 1;
                            }
                            chkMsg11.Checked = true;
                            txtrenk.Text = Convert.ToString(Rent);
                        }
                        else
                        {

                            DataTable dtffrenk = select("select max(OrderNo) as OrderNo from FunctionalityPageOrderTbl where FunctionalityMasterTblID='" + FunctionaID.Text + "'");// where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");
                            pnl_funct.Visible = true;

                            try
                            {
                                Rent = 1 + Convert.ToInt32(dtffrenk.Rows[0]["OrderNo"].ToString());
                            }
                            catch (Exception ex)
                            {
                                Rent = 1;
                            }

                            txtrenk.Text = Convert.ToString(Rent);
                        }
                    }
                }


                btnSubmit.Text = "Update";

                
            }
        }

        if (e.CommandName == "Delete")
        {
            int iikt = 0;
            int mm = Convert.ToInt32(e.CommandArgument);
            string  comd = "Select * from PageMaster where PageId=" + mm;
            SqlCommand cmall = new SqlCommand(comd, con);
            DataTable dtall = new DataTable();
            SqlDataAdapter adpall = new SqlDataAdapter(cmall);
            adpall.Fill(dtall);
            if (dtall.Rows.Count > 0)
            {
                iikt = 1;
            }
            string stm1 = "select *  from pageplaneaccesstbl  where Pageid='" + mm + "'";
            SqlCommand cmm1 = new SqlCommand(stm1, con);
            DataTable dtm1 = new DataTable();
            SqlDataAdapter adm1 = new SqlDataAdapter(cmm1);
            adm1.Fill(dtm1);
            if (dtm1.Rows.Count > 0)
            {
                iikt = 1;
            }

            string stm1d = "select *  from DefaultRolewisePageAccess  where PageId='" + mm + "'";
            SqlCommand cmm1d = new SqlCommand(stm1d, con);
            DataTable dtm1d = new DataTable();
            SqlDataAdapter adm1d = new SqlDataAdapter(cmm1d);
            adm1d.Fill(dtm1d);
            if (dtm1d.Rows.Count > 0)
            {
                iikt = 1;
            }
            if (iikt == 0)
            {
                string st2 = "Delete from PageMaster where PageId=" + mm;
                SqlCommand cmd2 = new SqlCommand(st2, con);
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();

                string st2a = "Delete from pageplaneaccesstbl where Pageid=" + mm;
                SqlCommand cmd2a = new SqlCommand(st2a, con);
                con.Open();
                cmd2a.ExecuteNonQuery();
                con.Close();


                string st2ader = "Delete from DefaultRolewisePageAccess where PageId=" + mm;
                SqlCommand cmd2ader = new SqlCommand(st2ader, con);
                con.Open();
                cmd2ader.ExecuteNonQuery();
                con.Close();
                GridView1.EditIndex = -1;
                FilterGrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted succesfully";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
            }

           
        }
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
        pnladdnew.Visible = false; 

    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FilterGrid();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        resetall();
        Label1.Text = "";
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;

        //string strcln = "select PageMaster.PageId,PageMaster.FolderName,PageMaster.Active, ProductMaster.ProductName,VersionInfoMaster.VersionInfoName, PageMaster.PageName,PageMaster.PageTitle   from  ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join PageMaster on PageMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId where  ClientMasterId='" + Session["ClientId"].ToString() + "'";

        //SqlCommand cmdcln = new SqlCommand(strcln, con);
        //DataTable dtcln = new DataTable();
        //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //adpcln.Fill(dtcln);
        //GridView1.DataSource = dtcln;
        //DataView myDataView = new DataView();
        //myDataView = dtcln.DefaultView;

        //if (hdnsortExp.Value != string.Empty)
        //{
        //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //}

        //GridView1.DataBind();
        //GridView1.DataBind();
    }
    protected void resetall()
    {
        //ddlProductname.SelectedIndex = 0;
        ////ddlpagetype.SelectedIndex = 0;
        //ddlMainMenu.SelectedIndex = 0;
        //ddlSubmenu.SelectedIndex = 0;
        txtpagename.Text = "";
        txtpagetitle.Text = "";
        txtpagedescriptin.Text = "";
        txtpageindex.Text = "";


        txtFolderName.Text = "";
        chkactive.Checked = true;
        chkmanuaccess.Checked = true;
        btnSubmit.Text = "Submit";

    }
    protected void ddlMainMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        FillSubMenu();
        

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

    //public void FilterMenu1()
    //{
    //    if (FilterProduct.SelectedIndex > 0 && FilterMenu.SelectedIndex>0)
    //    {

    //        string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where WebsiteMaster.VersionInfoId='" + FilterProduct.SelectedValue + "' and SubMenuMaster.MainMenuId='" + FilterMenu.SelectedValue + "'";
    //        SqlCommand cmdcln = new SqlCommand(strcln, con);
    //        DataTable dtcln = new DataTable();
    //        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //        adpcln.Fill(dtcln);


    //        FilterSubMenu.DataSource = dtcln;

    //        FilterSubMenu.DataValueField = "SubMenuId";
    //        FilterSubMenu.DataTextField = "SubMenuName";
    //        FilterSubMenu.DataBind();
    //        FilterSubMenu.Items.Insert(0, "-ALL-");
    //        FilterGrid();
    //    }
    //    else
    //    {
    //        string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where WebsiteMaster.VersionInfoId='" + FilterProduct.SelectedValue + "'";
    //        SqlCommand cmdcln = new SqlCommand(strcln, con);
    //        DataTable dtcln = new DataTable();
    //        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //        adpcln.Fill(dtcln);
    //        FilterSubMenu.DataSource = dtcln;

    //        FilterSubMenu.DataValueField = "SubMenuId";
    //        FilterSubMenu.DataTextField = "SubMenuName";
    //        FilterSubMenu.DataBind();
    //        FilterSubMenu.Items.Insert(0, "-ALL-");
    //        //FilterGrid();
    //    }
    //}
    protected void FilterMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        filtersubmenu();
        //  FillGrid();
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


        strcln = "select distinct SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId, PageMaster.PageId,PageMaster.FolderName,LanguageMaster.Name,LanguageMaster.Id,MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,PageMaster.Active, ProductMaster.ProductName,VersionInfoMaster.VersionInfoName, PageMaster.PageName,PageMaster.PageTitle,  WebsiteSection.SectionName + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId left outer join SubMenuMaster on PageMaster.SubMenuId=SubMenuMaster.SubMenuId left outer join LanguageMaster on LanguageMaster.Id = PageMaster.LanguageId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' ";

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
            str4 += "  and (PageMaster.PageName Like '%" + TextBox5.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox5.Text + "%' ) "; 
        }
        string orderby = "order by  PageMaster.PageTitle";

        lblfilterbyproduct.Text=FilterProduct.SelectedItem.Text;
            lblmainmenu.Text=FilterMenu.SelectedItem.Text;
            lblsubmenu.Text = FilterSubMenu.SelectedItem.Text;
            lblstatus.Text = ddlAct.SelectedItem.Text;

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




    public void FilterSubMenu1()
    {
        FillGrid();


    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strcln = " SELECT distinct MainMenuMaster.* from MainMenuMaster inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + ddlSection.SelectedValue + "' Order By MainMenuMaster.MainMenuTitle ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);


        FilterMenu.DataSource = dtcln;

        FilterMenu.DataValueField = "MainMenuId";
        FilterMenu.DataTextField = "MainMenuTitle";
        FilterMenu.DataBind();
        FilterMenu.Items.Insert(0, "-ALL-");
    }

   
    protected void BtnGo_Click(object sender, EventArgs e)
    {
        
        try
        {
            FilterGrid();
            string strftpinsert1 = "Delete From TheLastSearchedPageMasterTBL where UserID=" + Session["EmpId"] + " and Pagename='PageMasterNew'";
            con.Close();
            con.Open();
            SqlCommand cmdinsert1 = new SqlCommand(strftpinsert1, con);
            cmdinsert1.ExecuteNonQuery();

            con.Close();


            string str22 = "Insert Into TheLastSearchedPageMasterTBL(UserID,ProductID,Datetime, Pagename)Values('" + Session["EmpId"] + "'," + FilterProduct.SelectedValue + ",'" + DateTime.Now + "','PageMasterNew')";
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmn = new SqlCommand(str22, con);
            cmn.ExecuteNonQuery();
            con.Close();

            pnladdnew.Visible = false;
        }
        catch (Exception ex)
        {
        }
        
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


    }
    //protected void ddlSubmenu_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillLanguage();
    //}

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
    
    protected void filtersectionmasterpage()
    {
        string strcln11 = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageName,MasterPageMaster.MasterPageId,WebsiteSection.WebsiteSectionId, 'SECTION' + ' : ' +  WebsiteSection.SectionName + ':' + 'MASTER PAGE' + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and VersionInfoMaster.VersionInfoId='" + FilterProduct.SelectedValue + "' order  by MasterPage_Name Asc";
        SqlCommand cmdcln11 = new SqlCommand(strcln11, con);
        DataTable dtcln11 = new DataTable();
        SqlDataAdapter adpcln11 = new SqlDataAdapter(cmdcln11);
        adpcln11.Fill(dtcln11);
        //  ddlProductname.DataSource = dtcln11;

        //   :' + 'SECTION' + ' : ' +  WebsiteSection.SectionName + ':' + 'MASTER PAGE' + ' : ' +  MasterPageMaster.MasterPageName

        ddlSection.DataSource = dtcln11;

        ddlSection.DataValueField = "MasterPageId";
        ddlSection.DataTextField = "MasterPage_Name";
        ddlSection.DataBind();
        ddlSection.Items.Insert(0, "-ALL-");


    }
    protected void FilterCategorysearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        filtermainmenu();
        filtersubmenu();
    }
    protected void FilterCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMainmenu();
        FillSubMenu(); 
    }
    protected void filtermainmenu()
    {
        string filter="";
        if(DDLCategoryS.SelectedIndex >0)
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


        //}
        //else
        //{
        //    string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "'  Order By SubMenuMaster.SubMenuName ";
        //    SqlCommand cmdcln = new SqlCommand(strcln, con);
        //    DataTable dtcln = new DataTable();
        //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //    adpcln.Fill(dtcln);
        //    FilterSubMenu.DataSource = dtcln;
        //    FilterSubMenu.DataValueField = "SubMenuId";
        //    FilterSubMenu.DataTextField = "SubMenuName";
        //    FilterSubMenu.DataBind();
           

        //    FilterSubMenu.Items.Insert(0, "All");
        //    FilterSubMenu.Items[0].Value = "0";


        //}

    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            Button4.Visible = true;
            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
        }
        else
        {



            Button2.Text = "Printable Version";
            Button4.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[8].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {

        Label1.Text = "Add New Product Page";
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        lblmsg.Text = "";
      
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
    protected void btndosyncro_Clickpop(object sender, EventArgs e)
    {
        ModernpopSync.Show();
    }
    protected void btndosyncro_Click(object sender, EventArgs e)
    {
        int transf = 0;
        DataTable dt1 = selectBZ("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id,ClientProductTableMaster.VersionInfoId FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=SatelliteSyncronisationrequiringTablesMaster.ProductVersionID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='pagemaster' ");


        //DataTable dt1 = select("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='setupwizardquestion' and ClientProductTableMaster.VersionInfoId='" + ddlProductname.SelectedValue + "' and SatelliteSyncronisationrequiringTablesMaster.ProductVersionID='" + ddlProductname.SelectedValue + "'");
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

                DataTable dt121 = select("SELECT Max(ID) as ID from SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='" + arqid + "'");

                if (Convert.ToString(dt121.Rows[0]["ID"]) != "")
                {
                    DataTable dtcln = select("SELECT Distinct ServerMasterTbl.Id FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId    where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' and  PricePlanMaster.VersionInfoMasterId='" + dt1.Rows[i]["VersionInfoId"] + "'");

                    for (int j = 0; j < dtcln.Rows.Count; j++)
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        transf = Convert.ToInt32(dt1.Rows[i]["VersionInfoId"]);
                        string str223 = "Insert Into SateliteServerRequiringSynchronisationMasterTbl(SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + dt121.Rows[0]["ID"] + "','" + dtcln.Rows[j]["Id"] + "','0','" + DateTime.Now.ToString() + "')";
                        SqlCommand cmn3 = new SqlCommand(str223, con);
                        cmn3.ExecuteNonQuery();
                        con.Close();
                        transf = Convert.ToInt32(rdsync.SelectedValue);
                    }
                }


            }

        }


        if (transf > 0)
        {
            string te = "SyncData.aspx?verId=" + transf;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        Panel3.Visible = true;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        if (fileuploadaudio0.HasFile)
        {

            string str = Path.GetExtension(fileuploadaudio0.PostedFile.FileName);
            switch (str.ToLower())
            {
                case ".bmp":
                case ".gif":
                case ".jpg":
                case ".jpeg":
                case ".png":


                    break;

            }
            if (str == ".bmp" || str == ".gif" || str == ".jpg" || str == ".jpeg" || str == ".png")
            {
                lblmsg.Text = "";
                string filename = Path.GetFileName(fileuploadaudio0.FileName);

                fileuploadaudio0.SaveAs(Server.MapPath("~\\images\\") + filename);
                Label37.Text = fileuploadaudio0.FileName;
              
                imglogo.ImageUrl = "~/images/" + Label37.Text;
            }
            else
            {
             
                lblmsg.Text = "Invalid file extension.";
            }
        }
    }
}
