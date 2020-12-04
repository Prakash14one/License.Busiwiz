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

public partial class PageWorkMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    DataSet dt;
    string txtVersionName = "";
    string txtVersionNo = "";
    string txtPageName = "";
    int checkHoursavailability;
    protected void FillProductID()
    {
        if (ddlWebsiteSection.SelectedIndex > 0)
        {
        
        string strcln = " SELECT  distinct   ProductMaster.ProductId   FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where  MasterPageMaster.MasterPageId='" + ddlWebsiteSection.SelectedValue + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {
            Session["ProductId"] = dtcln.Rows[0]["ProductId"].ToString();
        }
        else
        {
            Session["ProductId"] = "0";
        }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      //  Session["ClientId"] = "35";
        //con.Open(); 
        lblmsg.Text = "";
        lblVersion.Visible = true;
        lblVersion.Text = "This is Version 5 Updated on 18-3-16 by Pk";
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            DateTime d1 =  DateTime.Now;
            string s_today = d1.ToString("MM/dd/yyyy"); // As String
            //TextBox1.Text = s_today;
            //TextBox2.Text = s_today;
            //TextBox3.Text = s_today;
            //TextBox4.Text = s_today;
            //TextBox5.Text = s_today;
            //TextBox6.Text = s_today;
             d1 = d1.AddDays(1);
             string enddate = d1.ToString("MM/dd/yyyy"); // As String
             txttargetdatedeve.Text = Convert.ToString(enddate); //System.DateTime.Now.ToShortDateString();
             txttargetdatedeve.Text = txttargetdatedeve.Text.Trim();
             txttargatedatetester.Text = Convert.ToString(enddate);
             txttarsupapprove.Text = Convert.ToString(enddate);


             TextBox11S.Text = txttargetdatedeve.Text;

             TextBox13.Text = txttargatedatetester.Text;
             TextBox15.Text = txttarsupapprove.Text;

             DateTime d1pre = DateTime.Now;
             d1pre = d1pre.AddDays(-1);
             string pre_day = d1pre.ToString("MM/dd/yyyy"); // As String


             TextBox1.Text = Convert.ToString(pre_day);
             TextBox3.Text = Convert.ToString(pre_day);
             TextBox5.Text = Convert.ToString(pre_day);
             d1pre = d1pre.AddDays(2);
             pre_day = d1pre.ToString("MM/dd/yyyy"); // As String
             TextBox2.Text = Convert.ToString(pre_day);
             TextBox4.Text = Convert.ToString(pre_day);
             TextBox6.Text = Convert.ToString(pre_day);

             txt_dateTestITwork.Text = txttargetdatedeve.Text;
             txt_DateSupITwork.Text = txttargatedatetester.Text;
             txt_dateDevITwork.Text = txttargatedatetester.Text;

             

            if (Request.QueryString["Id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);

                string strmasteridver = " SELECT PageVersionTbl.*,ProductMaster.ProductId,MasterPageMaster.MasterPageId from PageVersionTbl inner join   PageMaster on PageVersionTbl.PageMasterId=PageMaster.PageId     inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId   where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and PageVersionTbl.Id='" + id + "'   ";
                SqlCommand cmdmasteridver = new SqlCommand(strmasteridver, con);
                DataTable dtmasteridver = new DataTable();
                SqlDataAdapter adpmasteridver = new SqlDataAdapter(cmdmasteridver);
                adpmasteridver.Fill(dtmasteridver);

                if (dtmasteridver.Rows.Count > 0)
                {
                    ViewState["MasterPageId"] = dtmasteridver.Rows[0]["MasterPageId"].ToString();
                    ViewState["PageMasterId"] = dtmasteridver.Rows[0]["PageMasterId"].ToString();

                    FillProductVersion();
                    ddlWebsiteSection.SelectedIndex = ddlWebsiteSection.Items.IndexOf(ddlWebsiteSection.Items.FindByValue(ViewState["MasterPageId"].ToString()));
                    FillMainmenu();
                    FillSubMenu();
                    filltype();
                    ddlpage.SelectedIndex = ddlpage.Items.IndexOf(ddlpage.Items.FindByValue(ViewState["PageMasterId"].ToString()));
                    ddlpage_SelectedIndexChanged(sender, e);
                    ddlpageversionid.SelectedIndex = ddlpageversionid.Items.IndexOf(ddlpageversionid.Items.FindByValue(id.ToString()));

                    ddlProductname_SelectedIndexChanged(sender, e);

                    addnewpanel.Visible = false;
                    pnladdnew.Visible = true;
                    lblmsg.Text = "";
                    lbllegend.Text = "Add New Software Page Work Allocation ";
                }


                //
                //if (Session["EmpSelect"] != "")
                //{
                //    ddlemployee.SelectedIndex = Convert.ToInt32(Session["EmpSelect"]);
                //}
                //if (Session["EmpType"] != "")
                //{
                //    DropDownList1.SelectedIndex = Convert.ToInt32(Session["EmpType"]);
                //}
                if (ViewState["select_filterProduct"] != null)
                {
                    ddlfilterwebsite.SelectedValue = Convert.ToString(ViewState["select_filterProduct"]);
                    ddlfilterwebsite_SelectedIndexChanged(sender, e);

                }
             
            }
            else
            {
                FillProductVersion();
                FillMainmenu();
                FillSubMenu();
                filltype();
                ddlpage_SelectedIndexChanged(sender, e);

            }

            string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageName,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName + ':' + 'MASTER PAGE' + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where MasterPageMaster.MasterPageId='" + ddlWebsiteSection.SelectedValue + "' and ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' order  by VersionInfoMaster.VersionInfoId Asc";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                ViewState["verid"] = dtcln.Rows[0]["VersionInfoId"].ToString();
            }
            else
            {
                ViewState["verid"] = "0";
            }

            ViewState["sortOrder"] = "";
            
            filterwebsite();
            fillpage();
            filteremployee();
            FillProduct();
          FillGrid();
            costofdeveloper();
            costofdtester();
            costofdsupervisor();
          
            chkallowedfiledown.Checked =true;
            chkallowedfiledown_CheckedChanged(sender, e);
            
        }
      
    }

    protected void chkupload_CheckedChangedITwork(object sender, EventArgs e)
    {
        if (CheckBox4.Checked == true)
        {
            TextBox9.Visible = false;
            ImageButton13.Visible = false;

            pbl_ITworkallocation.Visible = true;
            filltodayhourDEV();
        }
        else
        {
            btnSubmit.Enabled = true;
            Panel6.Visible = false; 
            TextBox9.Visible = false;
            ImageButton13.Visible = false;
            pbl_ITworkallocation.Visible = false;

        }
    }
    protected void chkupload_CheckedChanged1(object sender, EventArgs e)
    {
        filterwebsite();
    }

    protected void chkupload_CheckedChanged12(object sender, EventArgs e)
    {
      
            CheckBox2.Checked = true;
            CheckBox3.Checked = false;

            CheckBox2.Enabled = false;
            CheckBox3.Enabled = true; 
            fillpage();
    }
    protected void chkupload_CheckedChanged123(object sender, EventArgs e)
    {
        CheckBox2.Checked = false;
        CheckBox3.Checked = true;

        CheckBox3.Enabled = false;
        CheckBox2.Enabled = true; 
        fillpage();
    }

    public void functionality()
    {
        string functionality = "select ID,FunctionalityTitle from FunctionalityMasterTbl where VersionID='" + ViewState["verid"] + "' Order by FunctionalityTitle ";
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
        filltype();
    }
    protected void FillProductVersion()
    {
        string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, MasterPageMaster.MasterPageId, ProductMaster.ProductName + '-' +   VersionInfoMaster.VersionInfoName  + ' - ' + WebsiteMaster.WebsiteName  + ' - ' +  WebsiteSection.SectionName   + ' - ' +  MasterPageMaster.MasterPageName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by productversion ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlWebsiteSection.DataSource = dtcln;
        ddlWebsiteSection.DataValueField = "MasterPageId";
        ddlWebsiteSection.DataTextField = "productversion";
        ddlWebsiteSection.DataBind();

        if (Session["ProductSel"] != "")
        {
            ddlWebsiteSection.SelectedIndex = Convert.ToInt32(Session["ProductSel"]);
            
        }
    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (btnSubmit.Text == "Update")
        {
            //string str1113 = "select * from PageWorkTbl  where PageVersionTblId='" + ddlpageversionid.SelectedValue + "' and WorkRequirementTitle='" + txtworkrequtitle.Text + "' and ID = '" + ViewState["pwid"] + "'";
            //SqlCommand cmd1113 = new SqlCommand(str1113, con);
            //SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
            //DataTable ds1113 = new DataTable();
            //adp1113.Fill(ds1113);
            //if (ds1113.Rows.Count == 0)
            //{
            Session["ProductSel"] = ddlWebsiteSection.SelectedValue;
            string str = "update   PageWorkTbl set  PageVersionTblId ='" + ddlpageversionid.SelectedValue + "', WorkRequirementTitle= '" + txtworkrequtitle.Text + "' ,WorkRequirementDescription='" + txtworkreqdesc.Text + "',TargetDateDeveloper='" + txttargetdatedeve.Text + "',TargetDateTester='" + txttargatedatetester.Text + "',TargetDateSuperviserApproval='" + txttarsupapprove.Text + "',EpmloyeeID_AssignedDeveloper='" + ddlempassdeve.SelectedValue + "',EpmloyeeID_AssignedTester='" + ddlempasstester.SelectedValue + "',EpmloyeeID_AssignedSupervisor='" + ddlempasssuper.SelectedValue + "',BudgetedHourDevelopment='" + txtbudgethourdev.Text + "',BudgetedHourTesting='" + txtbudhourtest.Text + "',BudgetedHourSupervisorChecking='" + txtbudhoursupcheck.Text + "', Incentive='" + txt_incentive.Text + "',Incentive_Tester='" + txt_incentivetester.Text + "',Incentive_Supervisor='" + txt_incentivetester.Text + "' where ID= '" + ViewState["pwid"] + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                string MasterInsert = "Update PageVersionTbl Set EmployeeId_Developer='" + ddlempassdeve.SelectedValue + "', DeveloperOK='0' , EmployeeId_Tester='" + ddlempasstester.SelectedValue + "' , TesterOk='0', EmployeeId_Supervisor='" + ddlempasssuper.SelectedValue + "' , SupervisorOk='0' where Id='" + ddlpageversionid.SelectedValue + "'";
                SqlCommand cmd1 = new SqlCommand(MasterInsert, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                string strcount = " select WebsiteMaster.*,PageMaster.FolderName,PageVersionTbl.PageName from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageVersionTbl.Id='" + ddlpageversionid.SelectedValue + "'";
                SqlCommand cmdcount = new SqlCommand(strcount, con);
                DataTable dtcount = new DataTable();
                SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
                adpcount.Fill(dtcount);


                if (dtcount.Rows.Count > 0)
                {
                    if (chkallowedfiledown.Checked == true)
                    {

                        foreach (GridViewRow gdr in grdpversion.Rows)
                        {
                            string ids = grdpversion.DataKeys[gdr.RowIndex].Value.ToString();
                            Label llblpvnamebltitle = (Label)gdr.FindControl("lblpvname");
                            Label lblpname = (Label)gdr.FindControl("lblpname");
                            Label lblpid = (Label)gdr.FindControl("lblpid");
                            string fol = "";
                            if (ids != "0")
                            {
                                fol = "/VersionFolder";
                            }

                            CheckBox chk = (CheckBox)gdr.FindControl("chk");
                            if (chk.Checked == true)
                            {
                                string strv = lblpname.Text.Substring(lblpname.Text.Length - 5, 5);

                                for (int i = 0; i < 2; i++)
                                {
                                    if ((strv == ".aspx") || (strv == "aspx") || (strv == " aspx ") || (strv == " .aspx "))
                                    {
                                        if (i == 1)
                                        {
                                            lblpname.Text = lblpname.Text.Replace(".aspx ", ".aspx");
                                            lblpname.Text = lblpname.Text + ".cs";
                                        }
                                    }

                                    string strftpinsert = "INSERT INTO PageWorkGuideUploadTbl (PageWorkTblId,WorkRequirementPdfFilename,FileTitle,Date) values('" + ViewState["pwid"].ToString() + "','" + lblpname.Text + "','" + llblpvnamebltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                                    SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                                    con.Open();
                                    cmdinsert.ExecuteNonQuery();
                                    con.Close();

                                    try
                                    {
                                        pdg(dtcount, lblpname, fol, "");
                                        if (lblpname.Text != "")
                                        {
                                            ftpfile(lblpname.Text.ToString(), Server.MapPath("~\\Attach\\") + lblpname.Text.ToString());

                                           // ftpfile(lblpname.Text.ToString(), Server.MapPath("~\\Attachment\\") + lblpname.Text.ToString());

                                        }
                                    }
                                    catch
                                    {
                                    }

                                    
                                }
                            }

                        }
                    }
                    if (pnlgenefile.Visible == true)
                    {
                        foreach (GridViewRow gdr in grdgene.Rows)
                        {


                            Label lblfo = (Label)gdr.FindControl("lblfo");
                            Label lblf = (Label)gdr.FindControl("lblf");
                            string strftpinsert = "INSERT INTO PageWorkGuideUploadTbl (PageWorkTblId,WorkRequirementPdfFilename,FileTitle,Date) values('" + ViewState["pwid"].ToString() + "','" + lblf.Text + "','','" + System.DateTime.Now.ToShortDateString() + "')";

                            SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                            con.Open();
                            cmdinsert.ExecuteNonQuery();
                            con.Close();
                            try
                            {
                                pdg(dtcount, lblf, "", lblfo.Text);
                                if (lblf.Text != "")
                                {
                                    ftpfile(lblf.Text.ToString(), Server.MapPath("~\\Attach\\") + lblf.Text.ToString());
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }


                foreach (GridViewRow gdr in gridFileAttach.Rows)
                {
                    Label lbltitle = (Label)gdr.FindControl("lbltitle");
                    Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");
                    Label lblaudiourl = (Label)gdr.FindControl("lblaudiourl");

                    string strftpinsert = "INSERT INTO PageWorkGuideUploadTbl (PageWorkTblId,WorkRequirementPdfFilename,WorkRequirementAudioFileName,FileTitle,Date) values('" + ViewState["pwid"].ToString() + "','" + lblpdfurl.Text + "','" + lblaudiourl.Text + "','" + lbltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                    SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                    con.Open();
                    cmdinsert.ExecuteNonQuery();
                    con.Close();
                    if (lblpdfurl.Text != "")
                    {
                        ftpfile(lblpdfurl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblpdfurl.Text.ToString());

                    }
                    if (lblaudiourl.Text != "")
                    {
                        ftpfile(lblaudiourl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblaudiourl.Text.ToString());

                    }

                }

                //ViewState["data"] = null;
                grdgene.DataSource = null;
                grdgene.DataBind();
               // Session["GridFileAttach1"] = null;
                gridFileAttach.DataSource = null;
                gridFileAttach.DataBind();

                lblmsg.Visible = true;
                btnSubmit.Text = "Submit";
                lblmsg.Text = "Record updated successfully";
          //  }
            //else
            //{
            //    lblmsg.Visible = true;

            //    lblmsg.Text = "Record for this Page Version already exists";
            //}

            addnewpanel.Visible = true;
            pnladdnew.Visible = false;
            lbllegend.Text = "";

        }
        else

         {




            string str11132 = "select * from PageWorkTbl  where PageVersionTblId='" + ddlpageversionid.SelectedValue + "'  ";
            SqlCommand cmd11132 = new SqlCommand(str11132, con);
            SqlDataAdapter adp11132 = new SqlDataAdapter(cmd11132);
            DataTable ds11132 = new DataTable();
            adp11132.Fill(ds11132);
            if (ds11132.Rows.Count == 0)
            {


                string str = "INSERT INTO [PageWorkTbl]([PageVersionTblId],[WorkRequirementTitle],[WorkRequirementDescription],[TargetDateDeveloper],[TargetDateTester],[TargetDateSuperviserApproval],[EpmloyeeID_AssignedDeveloper],[EpmloyeeID_AssignedTester],[EpmloyeeID_AssignedSupervisor],[BudgetedHourDevelopment],[BudgetedHourTesting],[BudgetedHourSupervisorChecking],[DevelopmentDone],[TestingDone],[SupervisorCheckingDone],[ClientId],[VersionId], [Incentive], [Incentive_Tester],[Incentive_Supervisor]) " +
                           "VALUES('" + ddlpageversionid.SelectedValue + "','" + txtworkrequtitle.Text + "','" + txtworkreqdesc.Text + "','" + txttargetdatedeve.Text + "','" + txttargatedatetester.Text + "','" + txttarsupapprove.Text + "','" + ddlempassdeve.SelectedValue + "','" + ddlempasstester.SelectedValue + "','" + ddlempasssuper.SelectedValue + "','" + txtbudgethourdev.Text + "','" + txtbudhourtest.Text + "','" + txtbudhoursupcheck.Text + "','" + 0 + "','" + 0 + "','" + 0 + "','" + Session["ClientId"].ToString() + "','" + ViewState["verid"].ToString() + "' ,'" + txt_incentive.Text + "' ,'" + txt_incentivetester.Text + "' ,'" + txt_incentiveSuper.Text + "')";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                string str1113 = "select max(Id) as PageWorkTblId from PageWorkTbl";
                SqlCommand cmd1113 = new SqlCommand(str1113, con);
                SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
                DataTable ds1113 = new DataTable();
                adp1113.Fill(ds1113);
                Session["PageWorkTblId"] = ds1113.Rows[0]["PageWorkTblId"].ToString();
                ViewState["PageWorkTblId"] = ds1113.Rows[0]["PageWorkTblId"].ToString();
                string WorkToBeDone_Deveolper = "To Develop Page " + txtworkrequtitle.Text.Trim();
                string WorkToBeDone_Tester = "To Test Page " + txtworkrequtitle.Text.Trim();
                string WorkToBeDone_Supervisor = "To Approve Page " + txtworkrequtitle.Text.Trim();


                Int64 PageID = Convert.ToInt64(ddlpage.SelectedValue);
                ViewState["PageID_PM"] = PageID;

                string strSQL = "";
                SqlCommand cmdSQL = new SqlCommand();

                //strSQL = "EXEC Insert_MyWorkDailyReport 0, " + ViewState["PageWorkTblId"] + ", '', '', '', " + ddlempassdeve.SelectedValue + ", 0, '" + txtbudgethourdev.Text.Trim() + "', " +
                //    "'" + DateTime.Now.ToString() + "', '" + WorkToBeDone_Deveolper + "', 1, '', " + PageID + ", " + ddlWebsiteSection.SelectedValue + "";
                //con.Open();
                //cmdSQL.CommandText = strSQL;
                //cmdSQL.Connection = con;
                //cmdSQL.ExecuteNonQuery();
                //con.Close();

                //strSQL = "EXEC Insert_MyWorkDailyReport 0, " + ViewState["PageWorkTblId"] + ", '', '', '', " + ddlempasstester.SelectedValue + ", 0, '" + txtbudhourtest.Text.Trim() + "', " +
                //   "'" + DateTime.Now.ToString() + "', '" + WorkToBeDone_Tester + "', 2, '', " + PageID + ", " + ddlWebsiteSection.SelectedValue + "";
                //con.Open();
                //cmdSQL.CommandText = strSQL;
                //cmdSQL.Connection = con;
                //cmdSQL.ExecuteNonQuery();
                //con.Close();

                //strSQL = "EXEC Insert_MyWorkDailyReport 0, " + ViewState["PageWorkTblId"] + ", '', '', '', " + ddlempasssuper.SelectedValue + ", 0, '" + txtbudhoursupcheck.Text.Trim() + "', " +
                //   "'" + DateTime.Now.ToString() + "', '" + WorkToBeDone_Supervisor + "', 3, '', " + PageID + ", " + ddlWebsiteSection.SelectedValue + "";
                //con.Open();
                //cmdSQL.CommandText = strSQL;
                //cmdSQL.Connection = con;
                //cmdSQL.ExecuteNonQuery();
                //con.Close();
               

                string MasterInsert = "Update PageVersionTbl Set EmployeeId_Developer='" + ddlempassdeve.SelectedValue + "', DeveloperOK='0' , EmployeeId_Tester='" + ddlempasstester.SelectedValue + "' , TesterOk='0', EmployeeId_Supervisor='" + ddlempasssuper.SelectedValue + "' , SupervisorOk='0' where Id='" + ddlpageversionid.SelectedValue + "'";
                SqlCommand cmd1 = new SqlCommand(MasterInsert, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                

                string strcount = " select WebsiteMaster.*,PageMaster.FolderName,PageVersionTbl.PageName from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageVersionTbl.Id='" + ddlpageversionid.SelectedValue + "'";
                SqlCommand cmdcount = new SqlCommand(strcount, con);
                DataTable dtcount = new DataTable();
                SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
                adpcount.Fill(dtcount);

                if (dtcount.Rows.Count > 0)
                {

                    if (chkallowedfiledown.Checked == true)
                    {
                        foreach (GridViewRow gdr in grdpversion.Rows)
                        {
                            string ids = grdpversion.DataKeys[gdr.RowIndex].Value.ToString();
                            Label llblpvnamebltitle = (Label)gdr.FindControl("lblpvname");
                            Label lblpname = (Label)gdr.FindControl("lblpname");
                            Label lblpid = (Label)gdr.FindControl("lblpid");
                            string fol = "";
                            if (ids != "0")
                            {
                                fol = "/VersionFolder";
                            }

                            CheckBox chk = (CheckBox)gdr.FindControl("chk");
                            if (chk.Checked == true)
                            {
                                string strv = lblpname.Text.Substring(lblpname.Text.Length - 5, 5);

                                for (int i = 0; i < 2; i++)
                                {
                                    if ((strv == ".aspx") || (strv == "aspx") || (strv == " aspx ") || (strv == " .aspx "))
                                    {
                                        if (i == 1)
                                        {
                                            lblpname.Text = lblpname.Text.Replace(".aspx ", ".aspx");
                                            lblpname.Text = lblpname.Text + ".cs";
                                        }
                                    }

                                    string NewFileName = "";
                                    DataTable dt = (DataTable)Session["dtVersionTbl"];
                                    if (lblpname.Text.EndsWith(".aspx"))
                                    { 
                                        NewFileName = Convert.ToString(dt.Rows[0]["PageName"]);
                                    }
                                    else if (lblpname.Text.EndsWith(".aspx.cs"))
                                    {
                                        NewFileName = Convert.ToString(dt.Rows[0]["PageName"]) + ".cs";
                                    }
                                    ViewState["NewFileName"] = NewFileName;
                                    //string strftpinsert = "INSERT INTO PageWorkGuideUploadTbl (PageWorkTblId,WorkRequirementPdfFilename,FileTitle,Date) values('" + ViewState["PageWorkTblId"].ToString() + "','" + lblpname.Text + "','" + llblpvnamebltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";
                                    string strftpinsert = " INSERT INTO PageDevelopmentSourceCodeAllocateTable (PageWorkTblId, FileName, Date, Folder_path, OriginalFileName) " +
                                       "values('" + ViewState["PageWorkTblId"].ToString() + "','" + NewFileName.ToString() + "', " +
                                       "'" + System.DateTime.Now.ToShortDateString() + "', '" + Server.MapPath("~\\Attach\\") + "', '"+ lblpname.Text.Trim() +"')";

                                    SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                                    con.Open();
                                    cmdinsert.ExecuteNonQuery();
                                    con.Close();
                                    try
                                    {
                                       
                                            pdg(dtcount, lblpname, fol, "");
                                        
                                       
                                        if (lblpname.Text != "")
                                        {
                                            ftpfile(lblpname.Text.ToString(), Server.MapPath("~\\Attach\\") + lblpname.Text.ToString(), "CodeFiles");

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                            }

                        }
                    }
                    if (pnlgenefile.Visible == true)
                    {
                        foreach (GridViewRow gdr in grdgene.Rows)
                        {


                            Label lblfo = (Label)gdr.FindControl("lblfo");
                            Label lblf = (Label)gdr.FindControl("lblf");
                            string strftpinsert = "INSERT INTO PageWorkGuideUploadTbl (PageWorkTblId,WorkRequirementPdfFilename,FileTitle,Date) values('" + ViewState["PageWorkTblId"].ToString() + "','" + lblf.Text + "','','" + System.DateTime.Now.ToShortDateString() + "')";

                            SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                            con.Open();
                            cmdinsert.ExecuteNonQuery();
                            con.Close();
                            try
                            {
                                pdg(dtcount, lblf, "", lblfo.Text);
                                if (lblf.Text != "")
                                {
                                    ftpfile(lblf.Text.ToString(), Server.MapPath("~\\Attach\\") + lblf.Text.ToString(), "OtherFiles");
                                }
                            }
                            catch(Exception ex)
                            {
                            }
                        }
                    }
                }
                foreach (GridViewRow gdr in gridFileAttach.Rows)
                {
                    Label lbltitle = (Label)gdr.FindControl("lbltitle");
                    Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");
                    Label lblaudiourl = (Label)gdr.FindControl("lblaudiourl");

                    string strftpinsert = "INSERT INTO PageWorkGuideUploadTbl (PageWorkTblId,WorkRequirementPdfFilename,WorkRequirementAudioFileName,FileTitle,Date) values('" + ViewState["PageWorkTblId"].ToString() + "','" + lblpdfurl.Text + "','" + lblaudiourl.Text + "','" + lbltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                    SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                    con.Open();
                    cmdinsert.ExecuteNonQuery();
                    con.Close();
                    if (lblpdfurl.Text != "")
                    {
                        ftpfile(lblpdfurl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblpdfurl.Text.ToString(), "InstructionFiles");

                    }
                    if (lblaudiourl.Text != "")
                    {
                        ftpfile(lblaudiourl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblaudiourl.Text.ToString(), "InstructionFiles");

                    }

                }



                if (CheckBox4.Checked == true && RadioButtonList1.SelectedValue !="0")
                {
                    try
                    {
                        string Insert = "Insert into MyDailyWorkReport(EmployeeId,budgetedhour,workallocationdate,worktobedone,Typeofwork,PageId, Offer_Amount, Priority, PageWorkTblid, Productid ,WorkDone ) values " +//Productid
                            " ('" + DDL_devForITwork.SelectedValue + "','" + txt_devhourITwork.Text + "','" + txt_dateDevITwork.Text + "','" + txtworkreqdesc.Text + "','1','" + ddlpage.SelectedValue + "', '" + txt_incentive.Text + "', 'Medium' , '" + Session["PageWorkTblId"] + "' ,'" + Session["ProductId"].ToString()  + "', '0')";//, '" + Session["ProductId"] + "'

                        SqlCommand cmdinsert = new SqlCommand(Insert, con);
                        con.Open();
                        cmdinsert.ExecuteNonQuery();
                        con.Close();



                        string str111314 = "select Max(Id) as Id from MyDailyWorkReport  ";
                        SqlCommand cmd111314 = new SqlCommand(str111314, con);
                        SqlDataAdapter adp111314 = new SqlDataAdapter(cmd111314);
                        DataTable ds111314 = new DataTable();
                        adp111314.Fill(ds111314);

                        if (ds111314.Rows.Count > 0)
                        {
                            ViewState["MaxId"] = ds111314.Rows[0]["Id"].ToString();

                            foreach (GridViewRow gdr in gridFileAttach.Rows)
                            {
                                Label lbltitle = (Label)gdr.FindControl("lbltitle");
                                Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");
                                Label lblaudiourl = (Label)gdr.FindControl("lblaudiourl");

                                string strftpinsert = "INSERT INTO DailyWorkGuideUploadTbl (MyworktblId,WorkRequirementPdfFilename,WorkRequirementAudioFileName,FileTitle,Date) values('" + ViewState["MaxId"].ToString() + "','" + lblpdfurl.Text + "','" + lblaudiourl.Text + "','" + lbltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                                SqlCommand cmdftpinsert = new SqlCommand(strftpinsert, con);
                                con.Open();
                                cmdftpinsert.ExecuteNonQuery();
                                con.Close();
                                if (lblpdfurl.Text != "")
                                {
                                    //  ftpfile(lblpdfurl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblpdfurl.Text.ToString());

                                }
                                if (lblaudiourl.Text != "")
                                {
                                    //    ftpfile(lblaudiourl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblaudiourl.Text.ToString());

                                }

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
                        string Insert = "Insert into MyDailyWorkReport(EmployeeId,budgetedhour,workallocationdate,worktobedone,Typeofwork,PageId, Offer_Amount, Priority ,PageWorkTblid ,Productid , WorkDone ) values " +
                            " ('" + ddl_testForITwork.SelectedValue + "','" + txt_BudhourITwork.Text + "','" + txt_dateTestITwork.Text + "','" + txtworkreqdesc.Text + "','2','" + ddlpage.SelectedValue + "' , '" + txt_incentivetester.Text + "', 'Medium', '" + Session["PageWorkTblId"] + "' ,'" + Session["ProductId"].ToString()  + "', '0')";

                        SqlCommand cmdinsert = new SqlCommand(Insert, con);
                        con.Open();
                        cmdinsert.ExecuteNonQuery();
                        con.Close();


                        string str111314 = "select Max(Id) as Id from MyDailyWorkReport  ";
                        SqlCommand cmd111314 = new SqlCommand(str111314, con);
                        SqlDataAdapter adp111314 = new SqlDataAdapter(cmd111314);
                        DataTable ds111314 = new DataTable();
                        adp111314.Fill(ds111314);

                        if (ds111314.Rows.Count > 0)
                        {
                            ViewState["MaxId"] = ds111314.Rows[0]["Id"].ToString();

                            foreach (GridViewRow gdr in gridFileAttach.Rows)
                            {
                                Label lbltitle = (Label)gdr.FindControl("lbltitle");
                                Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");
                                Label lblaudiourl = (Label)gdr.FindControl("lblaudiourl");

                                string strftpinsert = "INSERT INTO DailyWorkGuideUploadTbl (MyworktblId,WorkRequirementPdfFilename,WorkRequirementAudioFileName,FileTitle,Date) values('" + ViewState["MaxId"].ToString() + "','" + lblpdfurl.Text + "','" + lblaudiourl.Text + "','" + lbltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                                SqlCommand cmdftpinsert = new SqlCommand(strftpinsert, con);
                                con.Open();
                                cmdftpinsert.ExecuteNonQuery();
                                con.Close();
                                if (lblpdfurl.Text != "")
                                {
                                    //  ftpfile(lblpdfurl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblpdfurl.Text.ToString());

                                }
                                if (lblaudiourl.Text != "")
                                {
                                    //    ftpfile(lblaudiourl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblaudiourl.Text.ToString());

                                }

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
                        string Insert = "Insert into MyDailyWorkReport(EmployeeId,budgetedhour,workallocationdate,worktobedone,Typeofwork,PageId, Offer_Amount, Priority ,PageWorkTblid , Productid, WorkDone) values " +
                            " ('" + ddl_suppITWork.SelectedValue + "','" + txt_SuphourITwork.Text + "','" + txt_DateSupITwork.Text + "','" + txtworkreqdesc.Text + "','3','" + ddlpage.SelectedValue + "', '" + txt_incentiveSuper.Text + "', 'Medium' , '" + Session["PageWorkTblId"] + "','" + Session["ProductId"].ToString()  + "','0')";

                        SqlCommand cmdinsert = new SqlCommand(Insert, con);
                        con.Open();
                        cmdinsert.ExecuteNonQuery();
                        con.Close();




                        string str111314 = "select Max(Id) as Id from MyDailyWorkReport  ";
                        SqlCommand cmd111314 = new SqlCommand(str111314, con);
                        SqlDataAdapter adp111314 = new SqlDataAdapter(cmd111314);
                        DataTable ds111314 = new DataTable();
                        adp111314.Fill(ds111314);

                        if (ds111314.Rows.Count > 0)
                        {
                            ViewState["MaxId"] = ds111314.Rows[0]["Id"].ToString();

                            foreach (GridViewRow gdr in gridFileAttach.Rows)
                            {
                                Label lbltitle = (Label)gdr.FindControl("lbltitle");
                                Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");
                                Label lblaudiourl = (Label)gdr.FindControl("lblaudiourl");

                                string strftpinsert = "INSERT INTO DailyWorkGuideUploadTbl (MyworktblId,WorkRequirementPdfFilename,WorkRequirementAudioFileName,FileTitle,Date) values('" + ViewState["MaxId"].ToString() + "','" + lblpdfurl.Text + "','" + lblaudiourl.Text + "','" + lbltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                                SqlCommand cmdftpinsert = new SqlCommand(strftpinsert, con);
                                con.Open();
                                cmdftpinsert.ExecuteNonQuery();
                                con.Close();
                                if (lblpdfurl.Text != "")
                                {
                                    //  ftpfile(lblpdfurl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblpdfurl.Text.ToString());

                                }
                                if (lblaudiourl.Text != "")
                                {
                                    //    ftpfile(lblaudiourl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblaudiourl.Text.ToString());

                                }

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                   
                    
                   
                    Session["GridFileAttach1"] = null;
                    gridFileAttach.DataSource = null;
                    gridFileAttach.DataBind();


                    lblmsg.Visible = true;
                    lblmsg.Text = "Record Inserted Successfully";
                }

                         //-----------------------------------------------------

                ViewState["data"] = null;
                grdgene.DataSource = null;
                grdgene.DataBind();
                Session["GridFileAttach1"] = null;
                gridFileAttach.DataSource = null;
                gridFileAttach.DataBind();

                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";

                string strmax = " Select Max(Id) as Id from PageWorkTbl";
                SqlCommand cmdmax = new SqlCommand(strmax, con);
                DataTable dtmax = new DataTable();
                SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
                adpmax.Fill(dtmax);

                string id = "";
                int empdeveloper = Convert.ToInt32(ddlempassdeve.SelectedValue.ToString());
                int emptester = Convert.ToInt32(ddlempasstester.SelectedValue.ToString());
                int empsupervisor = Convert.ToInt32(ddlempasssuper.SelectedValue.ToString());

                if (dtmax.Rows.Count > 0)
                {
                    id = dtmax.Rows[0]["Id"].ToString();
                }

                //if (chkdeveloper.Checked == true)
                //{
                //    string te1 = "WorkAllocationForEmployee.aspx?Id=" + id + "&EmpDevid=" + empdeveloper;
                //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te1 + "');", true);
                //}
                //if (chktester.Checked == true)
                //{
                    
                //    string te2 = "WorkAllocationForEmployee.aspx?Id=" + id + "&EmpTestid=" + emptester;
                //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te2 + "');", true);
                //}
                //if (chksupervisor.Checked == true)
                //{
                //    string te3 = "WorkAllocationForEmployee.aspx?Id=" + id + "&EmpSupid=" + empsupervisor;
                //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te3 + "');", true);
                //}
            }
            else
            {
                lblmsg.Visible = true;
                //lblyes.Visible = true;
                //chkyes.Visible = true;
                lblmsg.Text = "Work is already allocated for this version , if you wish to edit the work allocation go to grid and click edit for this version ";
            }
        }
        //addnewpanel.Visible = true;
        //pnladdnew.Visible = false;
        //lbllegend.Text = "";
       // FillGrid();
        resetall();
       
    }

     protected void pdg(DataTable dtcount, Label lblpname, string fol, string gen)
     {




         if (dtcount.Rows.Count > 0)
        {


            string lblftpurl123 = Convert.ToString(dtcount.Rows[0]["FTP_Url"]);
            string lblftpport123 = Convert.ToString(dtcount.Rows[0]["FTP_Port"]);
            string lblftpuserid = Convert.ToString(dtcount.Rows[0]["FTP_UserId"]);
            string lblftppassword123 = PageMgmt.Decrypted(Convert.ToString(dtcount.Rows[0]["FTP_Password"]));
            if (gen == "")
            {
                ViewState["folder"] = dtcount.Rows[0]["FolderName"].ToString() + fol;
            }
            else
            {
                ViewState["folder"] = gen;

            }
            string[] separator1 = new string[] { "/" };
            string[] strSplitArr1 = lblftpurl123.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            String productno = strSplitArr1[0].ToString();
            string ftpurl = "";

            if (productno == "FTP:" || productno == "ftp:")
            {
                if (strSplitArr1.Length >= 3)
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123;
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123;

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123;
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123;

                }

            }


            if (lblftpurl123.Length > 0)
            {

                string ftphost = ftpurl + "/" + ViewState["folder"] + "/";
                string fnname = lblpname.Text.ToString();
                string despath = Server.MapPath("~\\Attach\\") + fnname.ToString();
                FileInfo filec = new FileInfo(despath);
                //try
                //{
                //if (!filec.Exists)
                //{
                GetFile(ftphost, fnname, despath, lblftpuserid, lblftppassword123);
                //}
                //}
                //catch (Exception ex)
                //{
                //    lblmsg.Text = ex.ToString();
                //    lblmsg.Visible = true;
                //}



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


            }

        }

    }


    public void ftpfile(string inputfilepath, string filename, string from = "")
    {

        string strcount = " select PageMaster.FolderName, WebsiteMaster.* from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.Id='" + ViewState["PageWorkTblId"].ToString() + "'"; //ViewState["pwid"]
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);
        if (dtcount.Rows.Count > 0)
        {
            string[] separator1 = new string[] { "/" };
            string[] strSplitArr1 = dtcount.Rows[0]["FTPWorkGuideUrl"].ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            String productno = strSplitArr1[0].ToString();
            string ftpurl = "";

            if (productno == "FTP:" || productno == "ftp:")
            {
                if (strSplitArr1.Length >= 3)
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);

                }

            }
            string[] seperator_RootPath = new string[] { "/" };
            string RootPath = Convert.ToString(dtcount.Rows[0]["RootFolderPath"]);
            string[] RootPathArray = RootPath.Split(seperator_RootPath, StringSplitOptions.RemoveEmptyEntries);
            string FolderName = "";
            for (int k = 2; k < RootPathArray.Length; k++)
            {
                FolderName += RootPathArray[k].ToString() + "/";
            }
            FolderName = FolderName.ToString().Substring(0, FolderName.Length - 1);
            //FolderName = Convert.ToString(dtcount.Rows[0]["FolderName"]);

            if (inputfilepath.EndsWith(".aspx") || inputfilepath.EndsWith(".aspx.cs"))
            {
                //string strSQL = "SELECT TOP 1 PageMasterId FROM PageVersionTbl WHERE PageMasterId = '" + ViewState["PageID_PM"] + "' ORDER BY ID DESC";
                //con.Open();
                //SqlCommand cmd = new SqlCommand(strSQL, con);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //con.Close();
                //if (dt.Rows.Count > 0)
                //{
                //    // do nothing
                //}
                //else
                //{
                    if (System.IO.File.Exists(ftpurl + "/" + FolderName + "/OriginalVersions/" + inputfilepath))
                    {
                        //do nothing
                    }
                    else
                    {
                        try
                        {
                            string ftphost_Orig = ftpurl + "/" + FolderName + "/OriginalVersions/" + inputfilepath;
                            ftphost_Orig = ftpurl + "/OriginalVersions/" + inputfilepath; //5Nov141
                            //////string ftphost = ftpurl + "/" + inputfilepath;
                            //////string ftphost = Convert.ToString(dtcount.Rows[0]["FTPWorkGuideUrl"]) + "/" + inputfilepath;
                            FtpWebRequest FTP_Orig = (FtpWebRequest)FtpWebRequest.Create(ftphost_Orig);
                            FTP_Orig.Credentials = new NetworkCredential(Convert.ToString(dtcount.Rows[0]["FTP_UserId"]), PageMgmt.Decrypted(Convert.ToString(dtcount.Rows[0]["FTP_Password"])));
                            FTP_Orig.UseBinary = false;
                            FTP_Orig.KeepAlive = true;
                            FTP_Orig.UsePassive = true;

                            FTP_Orig.Method = WebRequestMethods.Ftp.UploadFile;
                            FileStream fs_Orig = File.OpenRead(filename);
                            byte[] buffer_Orig = new byte[fs_Orig.Length];
                            fs_Orig.Read(buffer_Orig, 0, buffer_Orig.Length);
                            fs_Orig.Close();
                            Stream ftpstream_Orig = FTP_Orig.GetRequestStream();
                            ftpstream_Orig.Write(buffer_Orig, 0, buffer_Orig.Length);
                            ftpstream_Orig.Close();
                        }
                        catch (Exception Ex)
                        {
                            
                        }
                        
                    }
                //}
            }
            else
            {
                try
                {
                    string ftphost = ftpurl + "/" + FolderName + "/Attach/" + inputfilepath;
                    ftphost = ftpurl + "/Attach/" + inputfilepath;  //5Nov2015141
                    ftphost = ftpurl + "/" + inputfilepath;  //16Nov2015141
                    //////string ftphost = ftpurl + "/" + inputfilepath;
                    //////string ftphost = Convert.ToString(dtcount.Rows[0]["FTPWorkGuideUrl"]) + "/" + inputfilepath;
                    FtpWebRequest FTP = (FtpWebRequest)FtpWebRequest.Create(ftphost);
                    FTP.Credentials = new NetworkCredential(Convert.ToString(dtcount.Rows[0]["FTPWorkGuideUserId"]), PageMgmt.Decrypted(Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePW"])));
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
                }
                catch (Exception ex)
                {
                }
                
            }
            //------------------------------ATTACH FOLDER INSERT WITH VER SUFFIX----------------------------------------------//

            if (inputfilepath.EndsWith(".aspx") || inputfilepath.EndsWith(".aspx.cs"))
            {
                DataTable dt = (DataTable)Session["dtVersionTbl"];
                string ftphost_Upload = "";
                if (inputfilepath.EndsWith(".aspx"))
                {
                    ftphost_Upload = ftpurl + "/" + FolderName + "/Attach/" + Convert.ToString(dt.Rows[0]["PageName"]);
                    ftphost_Upload = ftpurl + "/Attach/" + Convert.ToString(dt.Rows[0]["PageName"]);
                    ftphost_Upload = ftpurl + "/" + Convert.ToString(dt.Rows[0]["PageName"]);
                }
                else if (inputfilepath.EndsWith(".aspx.cs"))
                {
                    ftphost_Upload = ftpurl + "/" + FolderName + "/Attach/" + Convert.ToString(dt.Rows[0]["PageName"]) + ".cs";
                    ftphost_Upload = ftpurl + "/Attach/" + Convert.ToString(dt.Rows[0]["PageName"]) + ".cs";
                    ftphost_Upload = ftpurl + "/" + Convert.ToString(dt.Rows[0]["PageName"]) + ".cs";
                }
                // string ftphost = Convert.ToString( dtcount.Rows[0]["FTPWorkGuideUrl"]) + "/" + inputfilepath;
                FtpWebRequest FTP_Upload = (FtpWebRequest)FtpWebRequest.Create(ftphost_Upload);
                FTP_Upload.Credentials = new NetworkCredential(Convert.ToString(dtcount.Rows[0]["FTPWorkGuideUserId"]), PageMgmt.Decrypted(Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePW"])));
               // FTP_Upload.Credentials = new NetworkCredential(Convert.ToString(dtcount.Rows[0]["FTP_UserId"]), PageMgmt.Decrypted(Convert.ToString(dtcount.Rows[0]["FTP_Password"])));
                FTP_Upload.UseBinary = false;
                FTP_Upload.KeepAlive = true;
                FTP_Upload.UsePassive = true;
                try
                {
                    FTP_Upload.Method = WebRequestMethods.Ftp.UploadFile;
                    FileStream fs_Upload = File.OpenRead(filename);
                    byte[] buffer_Upload = new byte[fs_Upload.Length];
                    fs_Upload.Read(buffer_Upload, 0, buffer_Upload.Length);
                    fs_Upload.Close();
                    Stream ftpstream_Upload = FTP_Upload.GetRequestStream();
                    ftpstream_Upload.Write(buffer_Upload, 0, buffer_Upload.Length);
                    ftpstream_Upload.Close();

                }
                catch (Exception ex)
                {
                }
                
                //------------------------------END ATTACH FOLDER INSERT WITH VER SUFFIX----------------------------------------------//

                //------------------------------VERSION FOLDER INSERT WITH VER SUFFIX----------------------------------------------//


                string Ver_ftphost_Upload = "";
                string VersionNo = Convert.ToString(dt.Rows[0]["VersionNo"]);
                string VersionFolderName = "pageversion" + VersionNo.ToString().Trim();
               //5Nov2015141 directoryExists(ftpurl + "/" + FolderName + "/VersionFolder/" + VersionFolderName, "", Convert.ToString(dtcount.Rows[0]["FTP_UserId"]), PageMgmt.Decrypted(Convert.ToString(dtcount.Rows[0]["FTP_Password"])));
                try
                {
                    directoryExists(ftpurl + "/" + VersionFolderName, "", Convert.ToString(dtcount.Rows[0]["FileuploadUserId"]), PageMgmt.Decrypted(Convert.ToString(dtcount.Rows[0]["FileuploadPW"])));
                }
                catch (Exception ex)
                {
                }
                
                //if (Directory.Exists(ftpurl + "/" + FolderName + "/VersionFolder/" + VersionFolderName))
                //{
                //    //Do nothing
                //}
                //else
                //{
                //    Directory.CreateDirectory(ftpurl + "/" + FolderName + "/VersionFolder/" + VersionFolderName);
                //}

                if (inputfilepath.EndsWith(".aspx"))
                {
                    //Ver_ftphost_Upload = ftpurl + "/" + FolderName + "/VersionFolder/" + VersionFolderName + "/" + Convert.ToString(dt.Rows[0]["PageName"]);
                    Ver_ftphost_Upload = ftpurl + "/" + FolderName + "/VersionFolder/" + VersionFolderName + "/" + inputfilepath;
                    Ver_ftphost_Upload = ftpurl +  "/" + VersionFolderName + "/" + inputfilepath;
                }
                else if (inputfilepath.EndsWith(".aspx.cs"))
                {
                    //Ver_ftphost_Upload = ftpurl + "/" + FolderName + "/VersionFolder/" + Convert.ToString(dt.Rows[0]["PageName"]) + ".cs";
                    Ver_ftphost_Upload = ftpurl + "/" + FolderName + "/VersionFolder/" + VersionFolderName + "/" + inputfilepath;
                    Ver_ftphost_Upload = ftpurl + "/" + VersionFolderName + "/" + inputfilepath;
                }
                // string ftphost = Convert.ToString( dtcount.Rows[0]["FTPWorkGuideUrl"]) + "/" + inputfilepath;
                FtpWebRequest FTP_Upload_Ver = (FtpWebRequest)FtpWebRequest.Create(Ver_ftphost_Upload);
                FTP_Upload_Ver.Credentials = new NetworkCredential(Convert.ToString(dtcount.Rows[0]["FileuploadUserId"]), PageMgmt.Decrypted(Convert.ToString(dtcount.Rows[0]["FileuploadPW"])));
                FTP_Upload_Ver.UseBinary = false;
                FTP_Upload_Ver.KeepAlive = true;
                FTP_Upload_Ver.UsePassive = true;
                try
                {
                    FTP_Upload_Ver.Method = WebRequestMethods.Ftp.UploadFile;
                    FileStream fs_Upload_Ver = File.OpenRead(filename);
                    byte[] buffer_Upload_Ver = new byte[fs_Upload_Ver.Length];
                    fs_Upload_Ver.Read(buffer_Upload_Ver, 0, buffer_Upload_Ver.Length);
                    fs_Upload_Ver.Close();
                    Stream ftpstream_Upload_Ver = FTP_Upload_Ver.GetRequestStream();
                    ftpstream_Upload_Ver.Write(buffer_Upload_Ver, 0, buffer_Upload_Ver.Length);
                    ftpstream_Upload_Ver.Close();
                }
                catch (Exception ex)
                {
                }
                
            }
            //------------------------------END VERSION FOLDER INSERT WITH VER SUFFIX----------------------------------------------//

            //if (inputfilepath.EndsWith(".aspx"))
            //{
            //    if (File.Exists(ftpurl + "/" + FolderName + "/Attach/" + Convert.ToString(dt.Rows[0]["PageName"])))
            //    {
            //        System.IO.File.Delete(ftpurl + "/" + FolderName + "/Attach/" + Convert.ToString(dt.Rows[0]["PageName"]));
            //    }
            //}
            //else if (inputfilepath.EndsWith(".aspx.cs"))
            //{
            //    if (File.Exists(ftpurl + "/" + FolderName + "/Attach/" + Convert.ToString(dt.Rows[0]["PageName"]) + ".cs"))
            //    {
            //        System.IO.File.Delete(ftpurl + "/" + FolderName + "/Attach/" + Convert.ToString(dt.Rows[0]["PageName"]) + ".cs");
            //    }
            //}
                                   
        }
        System.IO.File.Delete(filename);
    }
    public bool directoryExists(string directory, string host, string user, string pass)
    {
        /* Create an FTP Request */
        //string host = "";
        //string user = "";
        //string pass = "";
        //FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
        FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(directory);
        /* Log in to the FTP Server with the User Name and Password Provided */
        ftpRequest.Credentials = new NetworkCredential(user, pass);
        /* Specify the Type of FTP Request */
        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
        try
        {
            using (ftpRequest.GetResponse())
            {
                return true;
            }
            //var response = ftpRequest.GetResponse();
            //if (response != null)
            //    return true;
            //else return false;
        }
        catch (Exception ex)
        {
            WebRequest request = WebRequest.Create(directory);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(user, pass);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
            Console.WriteLine(ex.ToString());
            return false;
        }

        /* Resource Cleanup */
        finally
        {
            ftpRequest = null;
        }
    }
    protected void filltype()
    {

        ddlpage.Items.Clear();
        if (ddlWebsiteSection.SelectedIndex > -1)
        {

            string strcln = "";
            string str1 = "";
            string str2 = "";

            //strcln = "SELECT distinct MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName+'-'+SubMenuMaster.SubMenuName as Page_Name from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId   where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MasterPageMaster.MasterPageId='" + ddlWebsiteSection.SelectedValue + "'   and ( MainMenuMaster.MainMenuName  <> ''  and  PageMaster.PageTitle  <> '')   ";
            strcln = "SELECT distinct MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName as Page_Name from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId   where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MasterPageMaster.MasterPageId='" + ddlWebsiteSection.SelectedValue + "'  and PageMaster.Active='1' and ( MainMenuMaster.MainMenuName  <> ''  and  PageMaster.PageTitle  <> '')   ";



            if (ddlMainMenu.SelectedIndex > 0)
            {
                str1 = "  and PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' ";

            }

            if (ddlSubmenu.SelectedIndex > 0)
            {
                str2 = " and PageMaster.SubMenuId='" + ddlSubmenu.SelectedValue + "'";
            }
            if (ddlfuncti.SelectedIndex > 0)
            {
                str2 += " and PageMaster.PageId in (Select PagemasterID From FunctionalityPageOrderTbl where FunctionalityMasterTblID ='" + ddlfuncti.SelectedValue + "') ";
            }

            string orderby = "order by Page_Name";

            string finalstr = strcln + str1 + str2 + orderby;

            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlpage.DataSource = dtcln;
            ddlpage.DataValueField = "PageId";
            ddlpage.DataTextField = "Page_Name";
            ddlpage.DataBind();
            ddlpage.Items.Insert(0, "-Select-");
            ddlpage.Items[0].Value = "0";

           

            if(ViewState["pop_pageid"]  !=null)
            {
                ddlpage.SelectedValue = Convert.ToString(ViewState["pop_pageid"]); 
               
            }

        }
    }


    protected void linkdow_Click1(object sender, EventArgs e)
    {
        
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
            string fnname = dtcount.Rows[0]["OriginalFileName"].ToString();
            string despath = Server.MapPath("~\\Attachment\\") + fnname.ToString();

           


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
    protected void linkdow_Click(object sender, EventArgs e)
    {

        //lblpaged.Visible = true;
        //lblpaged.Text = "";
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
                    //lblpaged.Visible = true;
                    //lblpaged.Text = "Sorry ,After Certification of Supervisior you can not download this page.";
                    ModalPopupExtender1.Show();

                }
                else
                {

                    lblftpurl123.Text = dtcount.Rows[0]["FTP_Url"].ToString();
                    lblftpport123.Text = dtcount.Rows[0]["FTP_Port"].ToString();
                    lblftpuserid.Text = dtcount.Rows[0]["FTP_UserId"].ToString();
                    lblftppassword123.Text = PageMgmt.Decrypted(dtcount.Rows[0]["FTP_Password"].ToString());
                    lbl_versionuser.Text = dtcount.Rows[0]["FileuploadUserId"].ToString(); ;
                    lbl_versionpass.Text = PageMgmt.Decrypted(dtcount.Rows[0]["FileuploadPW"].ToString());

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

                       

                        FileInfo file = new FileInfo(despath);

                        Response.Redirect("~/CheckDownload.aspx?path=" + despath + "");
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

    public bool GetFile(string ftp, string filename, string Destpath, string username, string password)
    {
        FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.
           Create(ftp.ToString() + filename.ToString());

        password = PageMgmt.Decrypted(password);
        oFTP.Credentials = new NetworkCredential(username.ToString(), password.ToString());
        oFTP.UseBinary = false;
        oFTP.UsePassive = true;
        oFTP.Method = WebRequestMethods.Ftp.DownloadFile;


        FtpWebResponse response = (FtpWebResponse)oFTP.GetResponse();
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

    protected void FillProduct()
    {
        ddlempassdeve.Items.Clear();
        ddlempasstester.Items.Clear();
        ddlempasssuper.Items.Clear();

        string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' order By Name ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            ddlempassdeve.DataSource = dtcln;
            ddlempassdeve.DataValueField = "Id";
            ddlempassdeve.DataTextField = "Name";
            ddlempassdeve.DataBind();

            ddlempasstester.DataSource = dtcln;
            ddlempasstester.DataValueField = "Id";
            ddlempasstester.DataTextField = "Name";
            ddlempasstester.DataBind();

            ddlempasssuper.DataSource = dtcln;
            ddlempasssuper.DataValueField = "Id";
            ddlempasssuper.DataTextField = "Name";
            ddlempasssuper.DataBind();


            DDL_devForITwork.DataSource = dtcln;
            DDL_devForITwork.DataValueField = "Id";
            DDL_devForITwork.DataTextField = "Name";
            DDL_devForITwork.DataBind();

            ddl_suppITWork.DataSource = dtcln;
            ddl_suppITWork.DataValueField = "Id";
            ddl_suppITWork.DataTextField = "Name";
            ddl_suppITWork.DataBind();

            ddl_testForITwork.DataSource = dtcln;
            ddl_testForITwork.DataValueField = "Id";
            ddl_testForITwork.DataTextField = "Name";
            ddl_testForITwork.DataBind();


            DropDownList1.DataSource = dtcln;

            DropDownList1.DataValueField = "Id";
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "All");
            DropDownList1.Items[0].Value = "0";


            DropDownList2.DataSource = dtcln;

            DropDownList2.DataValueField = "Id";
            DropDownList2.DataTextField = "Name";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "All");
            DropDownList2.Items[0].Value = "0";


            DropDownList3.DataSource = dtcln;

            DropDownList3.DataValueField = "Id";
            DropDownList3.DataTextField = "Name";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, "All");
            DropDownList3.Items[0].Value = "0";
        }

    }
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {


        string strpageversionname = "select * from PageVersionTbl where Id='" + ddlpageversionid.SelectedValue + "'";

        SqlCommand cmdcontrol = new SqlCommand(strpageversionname, con);
        SqlDataAdapter adp123 = new SqlDataAdapter(cmdcontrol);
        DataTable dt = new DataTable();
        adp123.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            txtworkrequtitle.Text = "Work for " + dt.Rows[0]["PageName"].ToString();

        }
        else
        {
            txtworkrequtitle.Text = "";
        }


        string strcontrol = "select Top(1) PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion,  dbo.PageMaster.FolderName AS Pagepath   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where   PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True' AND ISNULL(SupervisorOK, 0) = 0 and PageVersionTbl.Id < ( SELECT MAX( Id ) FROM PageVersionTbl where  PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True' AND ISNULL(SupervisorOK, 0) = 0 ) order by PageVersionTbl.Id DESC ";
        //strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where  PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' and PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True'  order by PageVersionTbl.Id";
        SqlCommand cmdcontrolmax = new SqlCommand(strcontrol, con);
        SqlDataAdapter adp123max = new SqlDataAdapter(cmdcontrolmax);
        DataTable dtmax = new DataTable();
        adp123max.Fill(dtmax);
        try
        {
            ViewState["EmployeeId_Developer"] = dtmax.Rows[0]["EmployeeId_Developer"].ToString();
            ViewState["EmployeeId_Tester"] = dtmax.Rows[0]["EmployeeId_Tester"].ToString();
            ViewState["EmployeeId_Supervisor"] = dtmax.Rows[0]["EmployeeId_Supervisor"].ToString();

            try
            {
                if (ViewState["EmployeeId_Developer"] != null)
                {
                    ddlempassdeve.SelectedValue = Convert.ToString(ViewState["EmployeeId_Developer"]);
                    DDL_devForITwork.SelectedValue = Convert.ToString(ViewState["EmployeeId_Developer"]);

                }

            }
            catch (Exception ex)
            {
            }
            try
            {
                if (ViewState["EmployeeId_Tester"] != null)
                {
                    ddlempasstester.SelectedValue = Convert.ToString(ViewState["EmployeeId_Tester"]);
                    ddl_testForITwork.SelectedValue = Convert.ToString(ViewState["EmployeeId_Tester"]);
                }

            }
            catch (Exception ex)
            {
            }
            try
            {
                if (ViewState["EmployeeId_Supervisor"] != null)
                {
                    ddlempasssuper.SelectedValue = Convert.ToString(ViewState["EmployeeId_Supervisor"]);
                    ddl_suppITWork.SelectedValue = Convert.ToString(ViewState["EmployeeId_Supervisor"]);
                }

            }
            catch (Exception ex)
            {
            }
        }
        catch (Exception ex)
        {
        }
    }
      protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblDevStatus = e.Row.FindControl("lblDevStatus") as Label;
            Label lblTesterStatus = e.Row.FindControl("lblTesterStatus") as Label;
            Label lblSupStatus = e.Row.FindControl("lblSupStatus") as Label;

            if (lblDevStatus.Text == "true" || lblDevStatus.Text== "True")
            {
                lblDevStatus.Text = "Complete";
            }
            else
            {
                lblDevStatus.Text = "Pending";
            }
            if (lblTesterStatus.Text == "true" || lblTesterStatus.Text == "True")
            {
                lblTesterStatus.Text = "Complete";
            }
            else
            {
                lblTesterStatus.Text = "Pending";
            }
            if (lblSupStatus.Text == "true" || lblSupStatus.Text == "True")
            {
                lblSupStatus.Text = "Complete";
            }
            else
            {
                lblSupStatus.Text = "Pending";
            }
        }
        catch (Exception ex)
        {
        }
      }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;
            lbllegend.Text = "Edit Task";
            lblmsg.Text = "";

            //int mmc = Convert.ToInt32(e.CommandArgument);

            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString()); //.SelectedDataKey.Value)

            ViewState["pwid"] = i.ToString();
            string strcln = " select distinct PageWorkTbl.*,PageMaster.MainMenuId,PageMaster.SubMenuId,PageVersionTbl.Id as pvid,PageVersionTbl.PageMasterId,MainMenuMaster.MasterPage_Id from PageWorkTbl  inner join PageVersionTbl on PageVersionTbl.Id= PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId where  PageWorkTbl.Id='" + i + "'";
            //string strcln = "select * from PageWorkTbl where Id='" + i + "'";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {

                FillProductVersion();

                //  filltype();

                //  ddlpageversionid.SelectedIndex = ddlpageversionid.Items.IndexOf(ddlpageversionid.Items.FindByValue(dtcln.Rows[0]["PageVersionTblId"].ToString()));

                string strcln1 = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageName,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName + ':' + 'MASTER PAGE' + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' order  by VersionInfoMaster.VersionInfoId Asc";
                SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
                DataTable dtcln1 = new DataTable();
                SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
                adpcln1.Fill(dtcln1);
                ddlWebsiteSection.DataSource = dtcln1;

                ddlWebsiteSection.DataValueField = "MasterPageId";
                ddlWebsiteSection.DataTextField = "MasterPage_Name";
                ddlWebsiteSection.DataBind();
                ddlWebsiteSection.Items.Insert(0, "-Select-");
                ddlWebsiteSection.Items[0].Value = "0";
                ddlWebsiteSection.SelectedIndex = ddlWebsiteSection.Items.IndexOf(ddlWebsiteSection.Items.FindByValue(dtcln.Rows[0]["MasterPage_Id"].ToString()));
                FillMainmenu();


                ddlMainMenu.SelectedIndex = ddlMainMenu.Items.IndexOf(ddlMainMenu.Items.FindByValue(dtcln.Rows[0]["MainMenuId"].ToString()));
                FillSubMenu();
                ddlSubmenu.SelectedIndex = ddlSubmenu.Items.IndexOf(ddlSubmenu.Items.FindByValue(dtcln.Rows[0]["SubMenuId"].ToString()));
                filltype();
                ddlpage.SelectedIndex = ddlpage.Items.IndexOf(ddlpage.Items.FindByValue(dtcln.Rows[0]["PageMasterId"].ToString()));
                ddlpage_SelectedIndexChanged(sender, e);
                ddlpageversionid.SelectedIndex = ddlpageversionid.Items.IndexOf(ddlpageversionid.Items.FindByValue(dtcln.Rows[0]["pvid"].ToString()));
                ddlempassdeve.SelectedIndex = ddlempassdeve.Items.IndexOf(ddlempassdeve.Items.FindByValue(dtcln.Rows[0]["EpmloyeeID_AssignedDeveloper"].ToString()));
                ddlempasstester.SelectedIndex = ddlempasstester.Items.IndexOf(ddlempasstester.Items.FindByValue(dtcln.Rows[0]["EpmloyeeID_AssignedTester"].ToString()));
                ddlempasssuper.SelectedIndex = ddlempasssuper.Items.IndexOf(ddlempasssuper.Items.FindByValue(dtcln.Rows[0]["EpmloyeeID_AssignedSupervisor"].ToString()));


                txtworkrequtitle.Text = dtcln.Rows[0]["WorkRequirementTitle"].ToString();
                txtworkreqdesc.Text = dtcln.Rows[0]["WorkRequirementDescription"].ToString();

                txttargetdatedeve.Text = Convert.ToDateTime(dtcln.Rows[0]["TargetDateDeveloper"]).ToShortDateString();
                txttargatedatetester.Text = Convert.ToDateTime(dtcln.Rows[0]["TargetDateTester"]).ToShortDateString();
                txttarsupapprove.Text = Convert.ToDateTime(dtcln.Rows[0]["TargetDateSuperviserApproval"]).ToShortDateString();

                ViewState["PageWorkTblId"] = dtcln.Rows[0]["id"].ToString();

                txtbudgethourdev.Text = dtcln.Rows[0]["BudgetedHourDevelopment"].ToString();
                txtbudhourtest.Text = dtcln.Rows[0]["BudgetedHourTesting"].ToString();
                txtbudhoursupcheck.Text = dtcln.Rows[0]["BudgetedHourSupervisorChecking"].ToString();


                txt_incentive.Text = dtcln.Rows[0]["Incentive"].ToString();
                txt_incentivetester.Text = dtcln.Rows[0]["Incentive_Tester"].ToString();
                txt_incentiveSuper.Text = dtcln.Rows[0]["Incentive_Supervisor"].ToString();
                //chkdevdone.Checked = Convert.ToBoolean(dtcln.Rows[0]["DevelopmentDone"].ToString());
                //chktestdone.Checked = Convert.ToBoolean(dtcln.Rows[0]["TestingDone"].ToString());

                // chksuptestingdone.Checked = Convert.ToBoolean(dtcln.Rows[0]["SupervisorCheckingDone"].ToString());

                // txtactualhodeve.Text = dtcln.Rows[0]["ActualHourDevelopment"].ToString();
                //  txtacthotest.Text = dtcln.Rows[0]["ActualHourTesting"].ToString();
                // txtactsperchek.Text = dtcln.Rows[0]["ActualHourSupervisorChecking"].ToString();
                // txtdevenote.Text = dtcln.Rows[0]["DeveloperNote"].ToString();
                // txttesternote.Text = dtcln.Rows[0]["TesterNote"].ToString();
                // txtsupernote.Text = dtcln.Rows[0]["SupervisorNote"].ToString();

                //txtdevedonedate.Text = dtcln.Rows[0]["DevelopeMentDoneDATE"].ToString();
                // txttestingdone.Text = dtcln.Rows[0]["TestingDoneDate"].ToString();
                // txtsuperdonedate.Text = dtcln.Rows[0]["SupervisorDoneDate"].ToString();

                btnSubmit.Text = "Update";
            }
        }
       
        if (e.CommandName == "Delete")
        {
            int mm = Convert.ToInt32(e.CommandArgument);

            string st2 = "Delete from PageWorkTbl where Id='" + mm + "' ";
            SqlCommand cmd2 = new SqlCommand(st2, con);

            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
            //GridView1.EditIndex = -1;
            FillGrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";
        }
        if (e.CommandName == "Linkpage")
        {
            int mm = Convert.ToInt32(e.CommandArgument);
            string te = "PageMasterNew.aspx?PageId=";
            te += Convert.ToString(mm);  
           ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);           


         }

       
        if (e.CommandName == "LinkpageVersion1")
        {
            int mm = Convert.ToInt32(e.CommandArgument);

            string te = "PageVersionForm.aspx?VersionId=";
            te += Convert.ToString(mm);
            //GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            //int rinrow = row.RowIndex;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            //Label lblmasterId = (Label)GridView2.Rows[rinrow].FindControl("lblmasterId");
            //Label lbldate12345 = (Label)GridView2.Rows[rinrow].FindControl("lbldate12345");
            //Label lblbudgetd132 = (Label)GridView2.Rows[rinrow].FindControl("lblbudgetd132");
            //Label lblactualhour = (Label)GridView2.Rows[rinrow].FindControl("lblactualhour");


            string strcount = "select PageWorkTbl.*,WebsiteMaster.WebsiteName,PageMaster.PageTitle, PageMaster.PageName,PageVersionTbl.PageName as NewVersionName,PageVersionTbl.VersionNo from PageWorkTbl  inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId   inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId    inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId    inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id    inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId    inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.Id='" + mm + "'";

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

                //lblbudgetedhourdetail.Text = lblbudgetd132.Text;

                //lbltargatedatedetail.Text = lbldate12345.Text;
                //lblactualhourdetail.Text = lblactualhour.Text;

                string str1231 = " select * from PageWorkGuideUploadTbl where PageWorkTblId='" + mm + "'";

                SqlCommand cmd1231 = new SqlCommand(str1231, con);
                DataTable dt1231 = new DataTable();
                SqlDataAdapter adp123 = new SqlDataAdapter(cmd1231);
                adp123.Fill(dt1231);

                if (dt1231.Rows.Count > 0)
                {
                    GridView4.DataSource = dt1231;
                    GridView4.DataBind();
                }

                string strcount2 = " select PDSA.Id,Convert(nvarchar,PDSA.Date,101) as Date, 'pageversion' + RTRIM(PageVersionTbl.VersionNo) + '/' + PDSA.OriginalFileName AS PName, PDSA.FileName as upfile,PageMaster.FolderName,PageMaster.PageTitle,PageVersionTbl.VersionNo,PageVersionTbl.PageName from PageDevelopmentSourceCodeAllocateTable AS PDSA inner join PageWorkTbl on PageWorkTbl.Id=PDSA.PageWorkTblId inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.Id='" + mm + "' order by PDSA.Id Desc";
                SqlCommand cmdcount2 = new SqlCommand(strcount2, con);
                DataTable dtcount2 = new DataTable();
                SqlDataAdapter adpcount2 = new SqlDataAdapter(cmdcount2);
                adpcount2.Fill(dtcount2);
                if (dtcount2.Rows.Count > 0)
                {

                    grdsourcefile.DataSource = dtcount2;
                    grdsourcefile.DataBind();
                    Panel7.Visible = false;
                    Panel4.Visible = true;
                }


                string str12312 = " select * from PageWorkGuideUploadTbl where PageWorkTblId='" + mm + "'";

                SqlCommand cmd12312 = new SqlCommand(str12312, con);
                DataTable dt12312 = new DataTable();
                SqlDataAdapter adp1232 = new SqlDataAdapter(cmd12312);
                adp1232.Fill(dt12312);

                if (dt1231.Rows.Count > 0)
                {
                    GridView4.DataSource = dt12312;
                    GridView4.DataBind();
                }
            }

            ModalPopupExtender1.Show();
         }
        if (e.CommandName == "ITWork")
        {
            int i = Convert.ToInt32(e.CommandArgument);

            ModalPopupExtender3.Show();



            Session["workid"] = i;


            //   GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString()); //.SelectedDataKey.Value)

            ViewState["pwid"] = i.ToString();
            string strcln = " select distinct PageWorkTbl.*, PageMaster.Pageid,  PageMaster.MainMenuId,PageMaster.SubMenuId,PageVersionTbl.Id as pvid,PageVersionTbl.PageMasterId,MainMenuMaster.MasterPage_Id from PageWorkTbl  inner join PageVersionTbl on PageVersionTbl.Id= PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId where  PageWorkTbl.Id='" + i + "'";
            //string strcln = "select * from PageWorkTbl where Id='" + i + "'";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                Session["id"] = dtcln.Rows[0]["id"].ToString();
                Session["PageidDe"] = dtcln.Rows[0]["Pageid"].ToString();
                Session["incentive"] = dtcln.Rows[0]["incentive"].ToString();
                Session["incentive_Tester"] = dtcln.Rows[0]["incentive_Tester"].ToString();
                Session["incentive_SuperVisor"] = dtcln.Rows[0]["incentive_SuperVisor"].ToString();
                Session["WorkRequirementDescription"] = dtcln.Rows[0]["WorkRequirementDescription"].ToString();

                ViewState["EmployeeId_Developer"] = dtcln.Rows[0]["EpmloyeeID_AssignedDeveloper"].ToString();
                ViewState["EmployeeId_Tester"] = dtcln.Rows[0]["EpmloyeeID_AssignedTester"].ToString();
                ViewState["EmployeeId_Supervisor"] = dtcln.Rows[0]["EpmloyeeID_AssignedSupervisor"].ToString();

                try
                {
                    if (ViewState["EmployeeId_Developer"] != null)
                    {
                        DropDownList1.SelectedValue = Convert.ToString(ViewState["EmployeeId_Developer"]);

                    }

                }
                catch (Exception ex)
                {
                }

                try
                {
                    if (ViewState["EmployeeId_Tester"] != null)
                    {
                        DropDownList2.SelectedValue = Convert.ToString(ViewState["EmployeeId_Tester"]);

                    }

                }
                catch (Exception ex)
                {
                }

                try
                {
                    if (ViewState["EmployeeId_Supervisor"] != null)
                    {
                        DropDownList3.SelectedValue = Convert.ToString(ViewState["EmployeeId_Supervisor"]);

                    }

                }
                catch (Exception ex)
                {
                }

           
                
              //  FillProductVersion();

                //  filltype();

                //  ddlpageversionid.SelectedIndex = ddlpageversionid.Items.IndexOf(ddlpageversionid.Items.FindByValue(dtcln.Rows[0]["PageVersionTblId"].ToString()));

                //string strcln1 = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageName,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName + ':' + 'MASTER PAGE' + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' order  by VersionInfoMaster.VersionInfoId Asc";
                //SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
                //DataTable dtcln1 = new DataTable();
                //SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
                //adpcln1.Fill(dtcln1);
                //ddlWebsiteSection.DataSource = dtcln1;

                //ddlWebsiteSection.DataValueField = "MasterPageId";
                //ddlWebsiteSection.DataTextField = "MasterPage_Name";
                //ddlWebsiteSection.DataBind();
                //ddlWebsiteSection.Items.Insert(0, "-Select-");
                //ddlWebsiteSection.Items[0].Value = "0";
                //ddlWebsiteSection.SelectedIndex = ddlWebsiteSection.Items.IndexOf(ddlWebsiteSection.Items.FindByValue(dtcln.Rows[0]["MasterPage_Id"].ToString()));


                txt_incentive.Text = dtcln.Rows[0]["Incentive"].ToString();
                txt_incentivetester.Text = dtcln.Rows[0]["Incentive_Tester"].ToString();
                txt_incentiveSuper.Text = dtcln.Rows[0]["Incentive_Supervisor"].ToString();
            }

        }
    }


    protected void btnSubmit_ClickIT(object sender, EventArgs e)
    {
        string ssssssDesc = Session["WorkRequirementDescription"].ToString();
        string s = Session["PageidDe"].ToString();
        filltodayhourDEVPopp();
        if (checkHoursavailability == 2)
        {

            try
            {
                //Developers
                string Insert = "Insert into MyDailyWorkReport(EmployeeId,budgetedhour,workallocationdate,worktobedone,Typeofwork,PageId, Offer_Amount, Priority,  PageWorkTblid ,  WorkDone) values " +
                     " ('" + DropDownList1.SelectedValue + "','" + TextBox12.Text + "','" + TextBox11S.Text + "', " +
                     " '" + ssssssDesc + "' ,'1','" + s + "' , '" + txt_incentive.Text + "', 'Medium', '" + Session["id"].ToString()  + "', '0')";
                //" + Session["ProductId"].ToString() + "

                SqlCommand cmdinsert = new SqlCommand(Insert, con);
                con.Open();
                cmdinsert.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                string Insert = "Insert into MyDailyWorkReport(EmployeeId,budgetedhour,workallocationdate,worktobedone,Typeofwork,PageId, Offer_Amount, Priority, PageWorkTblid,WorkDone) values " +
                    " ('" + DropDownList2.SelectedValue + "','" + TextBox14.Text + "','" + TextBox13.Text + "', " +
                    "'" + ssssssDesc + "','2','" + s + "' , '" + txt_incentivetester.Text + "', 'Medium', '" + Session["id"] + "','0')";

                SqlCommand cmdinsert = new SqlCommand(Insert, con);
                con.Open();
                cmdinsert.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
            }
            try
            {
                string Insert = "Insert into MyDailyWorkReport(EmployeeId,budgetedhour,workallocationdate,worktobedone,Typeofwork,PageId, Offer_Amount, Priority, PageWorkTblid ,WorkDone) values " +
                    " ('" + DropDownList3.SelectedValue + "','" + TextBox16.Text + "','" + TextBox15.Text + "', " +
                    "'" + ssssssDesc + "','3','" + s + "' , '" + txt_incentiveSuper.Text + "', 'Medium', '" + Session["id"] + "', '0')";

                SqlCommand cmdinsert = new SqlCommand(Insert, con);
                con.Open();
                cmdinsert.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
            }
            FillGrid();
            ModalPopupExtender3.Hide();
            lblmsg.Text = "Record Add successfully";
            pnladdnew.Visible = false;

        }
        else
        {
            ModalPopupExtender3.Show();  
        }

    }

  
    protected void FillGrid()
    {

        string strcln = "";
        string str2 = "";
        string str1 = "";
        string str3 = "";
        string str4 = "";

        string str5 = "";
        string str6 = "";
        string str7 = "";
        string str8 = "";
        string str9 = "";
        string pageid = "";
        string verid = "";


        strcln = " select Distinct top(200)  PageWorkTbl.*,ProductMaster.ProductName,WebsiteMaster.WebsiteName,WebsiteMaster.ID,PageMaster.PageId,PageMaster.PageTitle,PageMaster.PageName,PageVersionTbl.VersionNo,PageVersionTbl.VersionName,Emp1.Name as Ename1,Emp2.Name as Ename2,Emp3.Name as Ename3 ,  dbo.EmployeeMaster.Name AS Dev_name, EmployeeMaster_1.Name AS Test_name, EmployeeMaster_2.Name AS Sup_name , dbo.PageVersionTbl.DeveloperOK, dbo.PageVersionTbl.TesterOk, dbo.PageVersionTbl.SupervisorOk " +  
            " from   dbo.PageWorkTbl INNER JOIN dbo.PageVersionTbl ON dbo.PageVersionTbl.Id = dbo.PageWorkTbl.PageVersionTblId INNER JOIN dbo.PageMaster ON dbo.PageMaster.PageId = dbo.PageVersionTbl.PageMasterId INNER JOIN  dbo.MainMenuMaster ON dbo.MainMenuMaster.MainMenuId = dbo.PageMaster.MainMenuId INNER JOIN  dbo.MasterPageMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id INNER JOIN  dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteSectionId = dbo.MasterPageMaster.WebsiteSectionId INNER JOIN  dbo.WebsiteMaster ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId INNER JOIN  dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId AND dbo.WebsiteMaster.WebsitePort = dbo.VersionInfoMaster.Active INNER JOIN  dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN  dbo.EmployeeMaster AS Emp1 ON dbo.PageWorkTbl.EpmloyeeID_AssignedDeveloper = Emp1.Id INNER JOIN   dbo.EmployeeMaster AS Emp2 ON dbo.PageWorkTbl.EpmloyeeID_AssignedTester = Emp2.Id INNER JOIN  dbo.EmployeeMaster AS Emp3 ON dbo.PageWorkTbl.EpmloyeeID_AssignedSupervisor = Emp3.Id INNER JOIN    dbo.EmployeeMaster ON dbo.PageWorkTbl.EpmloyeeID_AssignedDeveloper = dbo.EmployeeMaster.Id INNER JOIN  dbo.EmployeeMaster AS EmployeeMaster_1 ON dbo.PageWorkTbl.EpmloyeeID_AssignedTester = EmployeeMaster_1.Id INNER JOIN  dbo.EmployeeMaster AS EmployeeMaster_2 ON dbo.PageWorkTbl.EpmloyeeID_AssignedSupervisor = EmployeeMaster_2.Id " +
         " where PageWorkTbl.ClientId='" + Session["ClientId"].ToString() + "' ";
        

        if (ddlfilterdeveloper.SelectedIndex > 0)
        {
            str1  = " and PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddlfilterdeveloper.SelectedValue + "'";
        }
        if (TextBox1.Text != "" && TextBox2.Text != "")
        {
            str2 = " and PageWorkTbl.TargetDateDeveloper between '" + Convert.ToDateTime(TextBox1.Text).ToString() + "' and '" + Convert.ToDateTime(TextBox2.Text).ToString() + "'";
        }
        if (ddlfiltertester.SelectedIndex > 0)
        {
            str3 = " and PageWorkTbl.EpmloyeeID_AssignedTester='" + ddlfiltertester.SelectedValue + "'";
        }
        if (TextBox3.Text != "" && TextBox4.Text != "")
        {
            str4 = " and PageWorkTbl.TargetDateTester between '" + Convert.ToDateTime(TextBox3.Text).ToString() + "' and '" + Convert.ToDateTime(TextBox4.Text).ToString() + "'";
        }
        if (ddlfiltersupervisor.SelectedIndex > 0)
        {
            str5 = " and PageWorkTbl.EpmloyeeID_AssignedSupervisor='" + ddlfiltersupervisor.SelectedValue + "'";
        }
        if (TextBox5.Text != "" && TextBox6.Text != "")
        {
            str6 = " and PageWorkTbl.TargetDateSuperviserApproval between '" + Convert.ToDateTime(TextBox5.Text).ToString() + "' and '" + Convert.ToDateTime(TextBox6.Text).ToString() + "'";
        }
        if (ddlAct1.SelectedIndex > 0)
        {
            str7 = " and PageWorkTbl.DevelopmentDone='" + ddlAct1.SelectedValue + "'";
        }
        if (ddlAct2.SelectedIndex > 0)
        {
            str8 = " and PageWorkTbl.TestingDone='" + ddlAct2.SelectedValue + "'";
        }
        if (ddlAct3.SelectedIndex > 0)
        {
            str9 = " and PageWorkTbl.SupervisorCheckingDone='" + ddlAct3.SelectedValue + "'";
        }
        if (ddlpagenamefilter.SelectedIndex > 0)
        {
            pageid = "and PageMaster.PageId='" + ddlpagenamefilter.SelectedValue + "' ";
        }

        if (Drpver.SelectedIndex > 0)
        {
            verid = "and PageVersionTbl.Id= '" + Drpver.SelectedValue + "' ";
            
        }
        if (ddlfilterwebsite.SelectedIndex > 0)
        {
            str1 += " and  MasterPageMaster.MasterPageId='" + ddlfilterwebsite.SelectedValue + "' ";
        }
    
        string orderby = "order by ProductMaster.ProductName,PageMaster.PageName,PageVersionTbl.VersionNo ";
        strcln = strcln + str1 + str2 + str3 + str4 + str5 + str6 + str7 + str8 + str9 + pageid + verid + orderby;
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataSource = dtcln;
        GridView1.DataBind();

       
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            try
            {
                Label lbl_PageWorkTblId = (Label)gdr.FindControl("lbl_PageWorkTblId");
                //   string lbl_totaloffer = ((Label)e.Row.FindControl("lbl_totaloffer")).Text;

                Label String_product = (Label)gdr.FindControl("lblgrdproductname");
                try
                {
                    if (String_product.Text.ToString().Length > 4)
                    {
                        String_product.Text = String_product.Text.ToString().Substring(0, 12) + "...";
                    }
                }
                catch (Exception ex) { }




                Label lbl_Date_NextWork = (Label)gdr.FindControl("lbl_Date_NextWork");
                ImageButton imageBTN_IWork = (ImageButton)gdr.FindControl("imageBTN_IWork");


                //For Developers

                Label ActHourDev = (Label)gdr.FindControl("ActHourDev");
                Label lbl_devEmpID = (Label)gdr.FindControl("lbl_devEmpID");
                Label lbl_TestEmpID = (Label)gdr.FindControl("lbl_TestEmpID");
                Label lbl_SupEmpID = (Label)gdr.FindControl("lbl_SupEmpID");
                Label lbl_devName = (Label)gdr.FindControl("lbl_devName");
                Label lbl_TestName = (Label)gdr.FindControl("lbl_TestName");
                Label lbl_supName = (Label)gdr.FindControl("lbl_supName");
                try
                {
                    if (lbl_supName.Text.Length > 0 && lbl_TestName.Text.Length > 0 && lbl_devName.Text.Length > 0)
                    {
                        lbl_supName.Text = lbl_supName.Text.ToString().Substring(0, 5);
                        lbl_TestName.Text = lbl_TestName.Text.ToString().Substring(0, 5);
                        lbl_devName.Text = lbl_devName.Text.ToString().Substring(0, 5);
                    }
                }
                catch (Exception ex)
                {
                }

                string strSup = "select * from EmployeeMaster where Id='" + lbl_SupEmpID.Text + "'";
                //SqlCommand cmdSup = new SqlCommand(strSup, con);
                //SqlDataAdapter adpSup = new SqlDataAdapter(cmdSup);
                //DataTable daSup = new DataTable();
                //adpSup.Fill(daSup);
                //if (daSup.Rows.Count > 0)
                //{
                //    lbl_SupEmpID.Text = daSup.Rows[0]["Name"].ToString();
                //    lbl_SupEmpID.Text = lbl_SupEmpID.Text.ToString().Substring(0, 5);
                //}

                //string strTest = "select * from EmployeeMaster where Id='" + lbl_devEmpID.Text + "'";
                //SqlCommand cmdTest = new SqlCommand(strTest, con);
                //SqlDataAdapter adpTest = new SqlDataAdapter(cmdTest);
                //DataTable daTest = new DataTable();
                //adpTest.Fill(daTest);
                //if (daTest.Rows.Count > 0)
                //{
                //    lbl_TestName.Text = daTest.Rows[0]["Name"].ToString();
                //    lbl_TestName.Text = lbl_TestName.Text.ToString().Substring(0, 5);
                //}

                //string str = "select * from EmployeeMaster where Id='" + lbl_devEmpID.Text + "'";
                //SqlCommand cmd = new SqlCommand(str, con);
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //DataTable da = new DataTable();
                //adp.Fill(da);
                //if (da.Rows.Count > 0)
                //{
                //    lbl_supName.Text = da.Rows[0]["Name"].ToString();
                //    lbl_supName.Text = lbl_supName.Text.ToString().Substring(0, 5);
                //}


                string EmpDenRate = "0";
                string strhrspent = " select * from MyDailyWorkReport  where PageWorkTblId='" + lbl_PageWorkTblId.Text + "'  and MyDailyWorkReport.Typeofwork='1' ";
                SqlCommand cmdhrspent = new SqlCommand(strhrspent, con);
                DataTable dthrspent = new DataTable();
                SqlDataAdapter adphrspent = new SqlDataAdapter(cmdhrspent);
                adphrspent.Fill(dthrspent);

                if (dthrspent.Rows.Count > 0)
                {
                    int totalhr = 0;
                    int totalminute = 0;
                    string FinalTime = "";

                    lbl_Date_NextWork.Visible = true;
                    lbl_Date_NextWork.Text = dthrspent.Rows[0]["WorkAllocationdate"].ToString();
                    //workallocationdate  lbl_Date_NextWork

                    foreach (DataRow dr in dthrspent.Rows)
                    {
                        string time = "";
                        string temp12 = "";
                        string temp123 = "";
                        string outdifftime = "";

                        time = dr["HourSpent"].ToString();
                        if (time != "")
                        {

                            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");

                            temp12 = Convert.ToDateTime(time).ToString("HH");
                            temp123 = Convert.ToDateTime(time).ToString("mm");

                            int main1 = Convert.ToInt32(temp12);
                            int main2 = Convert.ToInt32(temp123);

                            totalhr += main1;
                            totalminute += main2;

                            FinalTime = totalhr + ":" + totalminute;

                            ActHourDev.Text = FinalTime.ToString();
                        }


                    }
                }



                //For Tester
                Label ActHourTest = (Label)gdr.FindControl("ActHourTsting");
                //Label lbl_TestEmpID = (Label)gdr.FindControl("lbl_TestEmpID");
                string EmptestRate = "0";
                string strhrspentTest = " select * from MyDailyWorkReport  where PageWorkTblId='" + lbl_PageWorkTblId.Text + "'  and MyDailyWorkReport.Typeofwork='2' ";
                SqlCommand cmdhrspentTest = new SqlCommand(strhrspentTest, con);
                DataTable dthrspentTest = new DataTable();
                SqlDataAdapter adphrspentTest = new SqlDataAdapter(cmdhrspentTest);
                adphrspentTest.Fill(dthrspentTest);

                if (dthrspentTest.Rows.Count > 0)
                {
                    lbl_Date_NextWork.Visible = true;
                    lbl_Date_NextWork.Text = dthrspentTest.Rows[0]["WorkAllocationdate"].ToString();
                    imageBTN_IWork.Visible = false;
                    //lbl_Date_NextWork.Text = dtcln1.Rows[0]["CD"]; 


                    int totalhr = 0;
                    int totalminute = 0;
                    string FinalTime = "";
                    foreach (DataRow dr in dthrspentTest.Rows)
                    {
                        string time = "";
                        string temp12 = "";
                        string temp123 = "";
                        string outdifftime = "";

                        time = dr["HourSpent"].ToString();
                        if (time != "")
                        {

                            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");

                            temp12 = Convert.ToDateTime(time).ToString("HH");
                            temp123 = Convert.ToDateTime(time).ToString("mm");

                            int main1 = Convert.ToInt32(temp12);
                            int main2 = Convert.ToInt32(temp123);

                            totalhr += main1;
                            totalminute += main2;

                            FinalTime = totalhr + ":" + totalminute;

                            ActHourTest.Text = FinalTime.ToString();
                        }


                    }
                }


                //For Sup
                Label ActHourSup = (Label)gdr.FindControl("ActHourSup");
                //Label lbl_TestEmpID = (Label)gdr.FindControl("lbl_TestEmpID");

                string strhrspentSup = " select * from MyDailyWorkReport  where PageWorkTblId='" + lbl_PageWorkTblId.Text + "'  and MyDailyWorkReport.Typeofwork='3' ";
                SqlCommand cmdhrspentSup = new SqlCommand(strhrspentSup, con);
                DataTable dthrspentSup = new DataTable();
                SqlDataAdapter adphrspentSup = new SqlDataAdapter(cmdhrspentSup);
                adphrspentSup.Fill(dthrspentSup);

                if (dthrspentSup.Rows.Count > 0)
                {
                    lbl_Date_NextWork.Visible = true;
                    lbl_Date_NextWork.Text = dthrspentSup.Rows[0]["WorkAllocationdate"].ToString();
                    int totalhr = 0;
                    int totalminute = 0;
                    string FinalTime = "";
                    foreach (DataRow dr in dthrspentSup.Rows)
                    {
                        string time = "";
                        string temp12 = "";
                        string temp123 = "";
                        string outdifftime = "";

                        time = dr["HourSpent"].ToString();
                        if (time != "")
                        {

                            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");

                            temp12 = Convert.ToDateTime(time).ToString("HH");
                            temp123 = Convert.ToDateTime(time).ToString("mm");

                            int main1 = Convert.ToInt32(temp12);
                            int main2 = Convert.ToInt32(temp123);

                            totalhr += main1;
                            totalminute += main2;

                            FinalTime = totalhr + ":" + totalminute;

                            ActHourSup.Text = FinalTime.ToString();
                        }


                    }
                }
            }
            catch (Exception ex)
            {
            }
            
        }
        
    
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
   
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        resetall();
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lblmsg.Text = "";
        lbllegend.Text = "";
    }
    protected void resetall()
    {
      
        txtworkrequtitle.Text = "";
      
        txtworkreqdesc.Text = "";
       
        btnSubmit.Text = "Submit";
    
        
    }
    protected void attachment()
    {

    }


    //protected void Button2_Click(object sender, EventArgs e)
    //{

    //    string ext = "";
    //    string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "pdf", "PDF", "MP3", "MP4", "wma" };
    //    string[] validFileTypes1 = { "MP3", "MP4", "pdf", "PDF", "wma" };
    //    bool isValidFile = false;
    //    if (Upradio.SelectedValue == "1")
    //    {

    //        ext = System.IO.Path.GetExtension(fileuploadaudio.PostedFile.FileName);
    //        for (int i = 0; i < validFileTypes1.Length; i++)
    //        {

    //            if (ext == "." + validFileTypes1[i])
    //            {

    //                isValidFile = true;

    //                break;

    //            }

    //        }
    //    }
    //    else
    //    {


    //        ext = System.IO.Path.GetExtension(fileuploadadattachment.PostedFile.FileName);
    //        for (int i = 0; i < validFileTypes.Length; i++)
    //        {

    //            if (ext == "." + validFileTypes[i])
    //            {

    //                isValidFile = true;

    //                break;

    //            }

    //        }
    //    }


    //    if (!isValidFile)
    //    {

    //        lblmsg.Visible = true;
    //        if (Upradio.SelectedValue == "1")
    //        {
    //            lblmsg.Text = "Invalid File. Please upload a File with extension " +

    //                           string.Join(",", validFileTypes1);
    //        }
    //        else
    //        {
    //            lblmsg.Text = "Invalid File. Please upload a File with extension " +

    //                          string.Join(",", validFileTypes);
    //        }

    //    }

    //    else
    //    {

    //        String filename = "";
    //        string audiofile = "";
    //        //PnlFileAttachLbl.Visible = true;
    //        if (fileuploadadattachment.HasFile || fileuploadaudio.HasFile)
    //        {
    //            if (fileuploadadattachment.HasFile)
    //            {
    //                filename = fileuploadadattachment.FileName;

    //                //filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fileuploadadattachment.FileName;
    //                fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Attachment\\") + filename);
    //            }
    //            if (fileuploadaudio.HasFile)
    //            {
    //                audiofile = fileuploadaudio.FileName;
    //                fileuploadaudio.PostedFile.SaveAs(Server.MapPath("~\\Attachment\\") + audiofile);
    //            }
    //            //hdnFileName.Value = filename;
    //            DataTable dt = new DataTable();
    //            if (Session["GridFileAttach1"] == null)
    //            {
    //                DataColumn dtcom2 = new DataColumn();
    //                dtcom2.DataType = System.Type.GetType("System.String");
    //                dtcom2.ColumnName = "PDFURL";
    //                dtcom2.ReadOnly = false;
    //                dtcom2.Unique = false;
    //                dtcom2.AllowDBNull = true;
    //                dt.Columns.Add(dtcom2);

    //                DataColumn dtcom3 = new DataColumn();
    //                dtcom3.DataType = System.Type.GetType("System.String");
    //                dtcom3.ColumnName = "Title";
    //                dtcom3.ReadOnly = false;
    //                dtcom3.Unique = false;
    //                dtcom3.AllowDBNull = true;
    //                dt.Columns.Add(dtcom3);

    //                DataColumn dtcom4 = new DataColumn();
    //                dtcom4.DataType = System.Type.GetType("System.String");
    //                dtcom4.ColumnName = "AudioURL";
    //                dtcom4.ReadOnly = false;
    //                dtcom4.Unique = false;
    //                dtcom4.AllowDBNull = true;
    //                dt.Columns.Add(dtcom4);

    //            }
    //            else
    //            {
    //                dt = (DataTable)Session["GridFileAttach1"];
    //            }
    //            DataRow dtrow = dt.NewRow();
    //            dtrow["PDFURL"] = filename;
    //            dtrow["Title"] = txttitlename.Text;
    //            dtrow["AudioURL"] = audiofile;

    //            // dtrow["FileNameChanged"] = hdnFileName.Value;
    //            dt.Rows.Add(dtrow);
    //            Session["GridFileAttach1"] = dt;
    //            gridFileAttach.DataSource = dt;


    //            gridFileAttach.DataBind();
    //            txttitlename.Text = "";
    //        }
    //        else
    //        {
    //            lblmsg.Visible = true;
    //            lblmsg.Text = "Please Attach File to Upload.";
    //            return;
    //        }
    //    }
    //}
    protected void Button2_ClickSearch(object sender, EventArgs e)
    {
        string strpageworkall = "SELECT TOP (100) dbo.PageMaster.FolderName AS Pagepath, dbo.FunctionalityPageOrderTbl.OrderNo, dbo.FunctionalityMasterTbl.FunctionalityTitle,dbo.PageMaster.pageid, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageDescription, dbo.FunctionalityMasterTbl.FunctionalityDescription, dbo.FunctionalityMasterTbl.Active, dbo.PageMaster.Active AS Expr1 FROM   dbo.FunctionalityPageOrderTbl INNER JOIN dbo.FunctionalityMasterTbl ON dbo.FunctionalityPageOrderTbl.FunctionalityMasterTblID = dbo.FunctionalityMasterTbl.ID INNER JOIN dbo.PageMaster ON dbo.FunctionalityPageOrderTbl.PagemasterID = dbo.PageMaster.PageId " +
                                    " where dbo.FunctionalityMasterTbl.Active='1' and dbo.PageMaster.Active='1' and  MasterPageMaster.MasterPageId='" + ddlfilterwebsite.SelectedValue  + "'  and (PageMaster.PageName Like '%" + TextBox5.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox5.Text + "%' )  ";

        strpageworkall = " SELECT distinct Top(50)  dbo.PageMaster.FolderName AS Pagepath, dbo.FunctionalityPageOrderTbl.OrderNo, dbo.FunctionalityMasterTbl.FunctionalityTitle,dbo.PageMaster.pageid, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageDescription, dbo.FunctionalityMasterTbl.FunctionalityDescription, dbo.FunctionalityMasterTbl.Active, dbo.PageMaster.Active AS Expr1 from     dbo.FunctionalityPageOrderTbl LEFT OUTER JOIN dbo.FunctionalityMasterTbl ON dbo.FunctionalityPageOrderTbl.FunctionalityMasterTblID = dbo.FunctionalityMasterTbl.ID LEFT OUTER JOIN dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId LEFT OUTER JOIN  dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId LEFT OUTER JOIN  dbo.MasterPageMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id LEFT OUTER JOIN  dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteSectionId = dbo.MasterPageMaster.WebsiteSectionId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId ON dbo.FunctionalityPageOrderTbl.PagemasterID = dbo.PageMaster.PageId " +
                    "  where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MasterPageMaster.MasterPageId='" + ddlfilterwebsite.SelectedValue + "'   and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '')      and dbo.PageMaster.Active='1' and  MasterPageMaster.MasterPageId='" + ddlfilterwebsite.SelectedValue + "'  and (PageMaster.PageName Like '%" + TextBox7.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox7.Text + "%' )  "; 
        strpageworkall ="SELECT DISTINCT dbo.PageMaster.FolderName AS Pagepath, dbo.PageMaster.PageId, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageDescription, dbo.PageMaster.Active AS Expr1 FROM            dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId LEFT OUTER JOIN dbo.MasterPageMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id LEFT OUTER JOIN dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteSectionId = dbo.MasterPageMaster.WebsiteSectionId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId RIGHT OUTER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId " +
                        "  where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MasterPageMaster.MasterPageId='" + ddlfilterwebsite.SelectedValue + "'   and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '')      and dbo.PageMaster.Active='1' and  MasterPageMaster.MasterPageId='" + ddlfilterwebsite.SelectedValue + "'  and (PageMaster.PageName Like '%" + TextBox7.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox7.Text + "%' )  "; 
        SqlCommand cmdpageworkall = new SqlCommand(strpageworkall, con);
        DataTable dtpageworkall = new DataTable();
        SqlDataAdapter adppageworkall = new SqlDataAdapter(cmdpageworkall);
        adppageworkall.Fill(dtpageworkall);

        if (dtpageworkall.Rows.Count > 0)
        {
            GridView3.DataSource = dtpageworkall;
            GridView3.DataBind();
            //GridView2.Visible = false;
            GridView3.Visible = true;
        }
        ModalPopupExtender2.Show(); 
    }
    protected void Button2_ClickSearchAdd(object sender, EventArgs e)
    {
        string strpageworkall = "SELECT DISTINCT TOP (100) dbo.PageMaster.FolderName AS Pagepath, dbo.PageMaster.PageId, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageDescription, dbo.PageMaster.Active AS Expr1 " + 
                                " FROM   dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId LEFT OUTER JOIN  dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId INNER JOIN  dbo.MasterPageMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id INNER JOIN dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteSectionId = dbo.MasterPageMaster.WebsiteSectionId INNER JOIN  dbo.WebsiteMaster ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId INNER JOIN  dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId INNER JOIN   dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId" +
                                " where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "'   and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '')      and dbo.PageMaster.Active='1' and  MasterPageMaster.MasterPageId='" + ddlWebsiteSection.SelectedValue + "'  and (PageMaster.PageName Like '%" + TextBox8.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox8.Text + "%' )  ";

       // strpageworkall = " SELECT distinct Top(50)  dbo.PageMaster.FolderName AS Pagepath, dbo.FunctionalityPageOrderTbl.OrderNo, dbo.FunctionalityMasterTbl.FunctionalityTitle,dbo.PageMaster.pageid, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle, dbo.PageMaster.PageDescription, dbo.FunctionalityMasterTbl.FunctionalityDescription, dbo.FunctionalityMasterTbl.Active, dbo.PageMaster.Active AS Expr1 from     dbo.FunctionalityPageOrderTbl INNER JOIN dbo.FunctionalityMasterTbl ON dbo.FunctionalityPageOrderTbl.FunctionalityMasterTblID = dbo.FunctionalityMasterTbl.ID INNER JOIN dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId LEFT OUTER JOIN  dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId INNER JOIN  dbo.MasterPageMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id INNER JOIN  dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteSectionId = dbo.MasterPageMaster.WebsiteSectionId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId ON dbo.FunctionalityPageOrderTbl.PagemasterID = dbo.PageMaster.PageId " +
         //           "  where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "'   and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '')     and dbo.FunctionalityMasterTbl.Active='1' and dbo.PageMaster.Active='1' and  MasterPageMaster.MasterPageId='" + ddlWebsiteSection.SelectedValue + "'  and (PageMaster.PageName Like '%" + TextBox8.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox8.Text + "%' )  ";
        SqlCommand cmdpageworkall = new SqlCommand(strpageworkall, con);
        DataTable dtpageworkall = new DataTable();
        SqlDataAdapter adppageworkall = new SqlDataAdapter(cmdpageworkall);
        adppageworkall.Fill(dtpageworkall);

        if (dtpageworkall.Rows.Count > 0)
        {
            GridView2.DataSource = dtpageworkall;
            GridView2.DataBind();
            GridView2.Visible = true;
          //  GridView3.Visible = false; 
        }
       // ModalPopupExtender1.Show();
    }
    protected void linkdow1dailywork_Clickadd(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        int data = Convert.ToInt32(GridView2.DataKeys[rinrow].Value);
        ViewState["pop_pageid"] = data;

        filltype();
        ddlpage_SelectedIndexChanged(sender, e);
        ViewState["pop_pageid"] = null;

        ModalPopupExtender1.Hide();
    }
    protected void linkdow1dailywork_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        int data = Convert.ToInt32(GridView3.DataKeys[rinrow].Value);
        ViewState["pop_pageid"] = data;
        
        fillpage();
         ddlpagenamefilter_SelectedIndexChanged(sender ,e);
        ViewState["pop_pageid"] = null;

        ModalPopupExtender1.Hide(); 
    }



    protected void Button2_Click(object sender, EventArgs e)
    {

        string ext = "";
        string[] validFileTypes = { "bmp", "mp3", "gif", "mp4", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "pdf", "PDF", "MP3", "MP4", "wma", "html", "css", "rar", "zip", "rpt" };
        string[] validFileTypes1 = { "MP3", "MP4", "mp3", "mp4", "pdf", "PDF", "wma", "html", "css", "rar", "zip", "rpt", "xls", "xlsx" };
        bool isValidFile = false;
        if (Upradio.SelectedValue == "1")
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

                    fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Attachment\\") + filename);
                }
                if (fileuploadaudio.HasFile)
                {
                    audiofile = fileuploadaudio.FileName;
                    fileuploadaudio.PostedFile.SaveAs(Server.MapPath("~\\Attach\\") + audiofile);

                    fileuploadaudio.PostedFile.SaveAs(Server.MapPath("~\\Attachment\\") + audiofile);
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
    protected void FillMainmenu()
    {



        ddlMainMenu.Items.Clear();

        if (ddlWebsiteSection.SelectedIndex > -1)
        {
            string strcln = " SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + ddlWebsiteSection.SelectedValue + "' and MainMenuMaster.Active='1' ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddlMainMenu.DataSource = dtcln;
            ddlMainMenu.DataValueField = "MainMenuId";
            ddlMainMenu.DataTextField = "Page_Name";
            ddlMainMenu.DataBind();
            ddlMainMenu.Items.Insert(0, "-Select-");
            ddlMainMenu.Items[0].Value = "0";


        }
        else
        {
            ddlMainMenu.DataSource = null;
            ddlMainMenu.DataValueField = "MainMenuId";
            ddlMainMenu.DataTextField = "MainMenuTitle";
            ddlMainMenu.DataBind();
            ddlMainMenu.Items.Insert(0, "-Select-");
            ddlMainMenu.Items[0].Value = "0";

        }
    }
    
    protected void FillSubMenu()
    {

        ddlSubmenu.Items.Clear();

        if (ddlMainMenu.SelectedIndex > 0)
        {
            string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + ddlWebsiteSection.SelectedValue + "' and SubMenuMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' and SubMenuMaster.Active = '1'";
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

            ddlSubmenu.DataSource = null;
            ddlSubmenu.DataValueField = "SubMenuId";
            ddlSubmenu.DataTextField = "SubMenuName";
            ddlSubmenu.DataBind();
            ddlSubmenu.Items.Insert(0, "-Select-");
            ddlSubmenu.Items[0].Value = "0";


        }
    }
    protected void ddlWebsiteSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageName,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName + ':' + 'MASTER PAGE' + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name,  dbo.WebsiteMaster.RootFolderPath  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where MasterPageMaster.MasterPageId='" + ddlWebsiteSection.SelectedValue + "' and ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' order  by VersionInfoMaster.VersionInfoId Asc";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            ViewState["verid"] = dtcln.Rows[0]["VersionInfoId"].ToString();
            ViewState["Rootpath"] = dtcln.Rows[0]["RootFolderPath"].ToString();
        }
        else
        {
            ViewState["verid"] = "0";
            ViewState["Rootpath"] = "";
        }
        FillProductID();
        FillMainmenu();
        FillSubMenu();
        filltype();
        functionality();
        ddlpage_SelectedIndexChanged(sender, e);
    }
    protected void ddlMainMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSubMenu();
        filltype();
        ddlpage_SelectedIndexChanged(sender, e);
    }
    protected void ddlSubmenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        filltype();
        ddlpage_SelectedIndexChanged(sender, e);
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void filterwebsite()
    {
           string activestr = "";
           if (CheckBox1.Checked == true)
        {
            activestr = " and ProductDetail.Active='1' and VersionInfoMaster.Active ='True' ";
        }


           string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId, ProductMaster.ProductName ,WebsiteSection.WebsiteSectionId, MasterPageMaster.MasterPageId, ProductMaster.ProductName + '-' +   VersionInfoMaster.VersionInfoName  + ' - ' + WebsiteMaster.WebsiteName  + ' - ' +  WebsiteSection.SectionName   + ' - ' +  MasterPageMaster.MasterPageName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "'  " + activestr + "  order  by ProductMaster.ProductName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlfilterwebsite.DataSource = dtcln;
        ddlfilterwebsite.DataValueField = "MasterPageId";
        ddlfilterwebsite.DataTextField = "productversion";
        ddlfilterwebsite.DataBind();
        ddlfilterwebsite.Items.Insert(0, "All");
        ddlfilterwebsite.Items[0].Value = "0";

       
    }

    protected void filteremployee()
    {

        string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' and EmployeeMaster.Active='1' ORDER BY EmployeeMaster.Name ASC";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            ddlfilterdeveloper.DataSource = dtcln;

            ddlfilterdeveloper.DataValueField = "Id";
            ddlfilterdeveloper.DataTextField = "Name";
            ddlfilterdeveloper.DataBind();

            ddlfilterdeveloper.Items.Insert(0, "All");
            ddlfilterdeveloper.Items[0].Value = "0";

            ddlfiltertester.DataSource = dtcln;

            ddlfiltertester.DataValueField = "Id";
            ddlfiltertester.DataTextField = "Name";
            ddlfiltertester.DataBind();
            ddlfiltertester.Items.Insert(0, "All");
            ddlfiltertester.Items[0].Value = "0";

            ddlfiltersupervisor.DataSource = dtcln;

            ddlfiltersupervisor.DataValueField = "Id";
            ddlfiltersupervisor.DataTextField = "Name";
            ddlfiltersupervisor.DataBind();
            ddlfiltersupervisor.Items.Insert(0, "All");
            ddlfiltersupervisor.Items[0].Value = "0";


            DropDownList1.DataSource = dtcln;

            DropDownList1.DataValueField = "Id";
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "All");
            DropDownList1.Items[0].Value = "0";


            DropDownList2.DataSource = dtcln;

            DropDownList2.DataValueField = "Id";
            DropDownList2.DataTextField = "Name";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "All");
            DropDownList2.Items[0].Value = "0";


            DropDownList3.DataSource = dtcln;

            DropDownList3.DataValueField = "Id";
            DropDownList3.DataTextField = "Name";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, "All");
            DropDownList3.Items[0].Value = "0";


            DDL_devForITwork.DataSource = dtcln;

            DDL_devForITwork.DataValueField = "Id";
            DDL_devForITwork.DataTextField = "Name";
            DDL_devForITwork.DataBind();
            DDL_devForITwork.Items.Insert(0, "All");
            DDL_devForITwork.Items[0].Value = "0";

            ddl_suppITWork.DataSource = dtcln;

            ddl_suppITWork.DataValueField = "Id";
            ddl_suppITWork.DataTextField = "Name";
            ddl_suppITWork.DataBind();
            ddl_suppITWork.Items.Insert(0, "All");
            ddl_suppITWork.Items[0].Value = "0";


            ddl_testForITwork.DataSource = dtcln;

            ddl_testForITwork.DataValueField = "Id";
            ddl_testForITwork.DataTextField = "Name";
            ddl_testForITwork.DataBind();
            ddl_testForITwork.Items.Insert(0, "All");
            ddl_testForITwork.Items[0].Value = "0";
           
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ViewState["select_filterProduct"] = ddlfilterwebsite.SelectedValue;  
        FillGrid();
       


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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strcontrol = "";
        ddlpageversionid.Items.Clear();

        if (ddlpage.SelectedIndex > 0)
        {

            //strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where   PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True'  order by PageVersionTbl.Id DESC";
            lbl_pageid.Text = ddlpage.SelectedValue;
            strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion,  dbo.PageMaster.FolderName AS Pagepath   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where   PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True' AND ISNULL(SupervisorOK, 0) = 0  order by PageVersionTbl.Id DESC";
            //strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where  PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' and PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True'  order by PageVersionTbl.Id";
            SqlCommand cmdcontrol = new SqlCommand(strcontrol, con);
            SqlDataAdapter adp123 = new SqlDataAdapter(cmdcontrol);
            DataTable dt = new DataTable();
            adp123.Fill(dt);
            try
            {
                ViewState["EmployeeId_Developer"] = dt.Rows[0]["EmployeeId_Developer"].ToString();
                ViewState["EmployeeId_Tester"] = dt.Rows[0]["EmployeeId_Tester"].ToString();
                ViewState["EmployeeId_Supervisor"] = dt.Rows[0]["EmployeeId_Supervisor"].ToString();

                try
                {
                    if (ViewState["EmployeeId_Developer"] != null)
                    {
                        ddlempassdeve.SelectedValue = Convert.ToString(ViewState["EmployeeId_Developer"]);
                        DDL_devForITwork.SelectedValue = Convert.ToString(ViewState["EmployeeId_Developer"]);
                    }

                }
                catch (Exception ex)
                {
                }
                try
                {
                    if (ViewState["EmployeeId_Tester"] != null)
                    {
                        ddlempasstester.SelectedValue = Convert.ToString(ViewState["EmployeeId_Tester"]);
                        ddl_testForITwork.SelectedValue = Convert.ToString(ViewState["EmployeeId_Tester"]);
                    }

                }
                catch (Exception ex)
                {
                }
                try
                {
                    if (ViewState["EmployeeId_Supervisor"] != null)
                    {
                        ddlempasssuper.SelectedValue = Convert.ToString(ViewState["EmployeeId_Supervisor"]);
                        ddl_suppITWork.SelectedValue = Convert.ToString(ViewState["EmployeeId_Supervisor"]);

                    }

                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
            }
            ddlpageversionid.DataSource = dt;
            ddlpageversionid.DataTextField = "PageNameVersion";
            ddlpageversionid.DataValueField = "Id";
            ddlpageversionid.DataBind();
            Session["dtVersionTbl"] = dt;
            if (dt.Rows.Count > 1)
            {
                lbl_moreversionError.Text = "Pending Page Conflict: More than one pending version of this page exists. To resolve this issue certify all but the latest pending version and reallocate it if required. Otherwise, if old versions cannot be certified, create a new " +
                                                "page should be created and this current page should be abandoned.";
                lbl_moreversionError.Visible = true;  
            }
            else if (dt.Rows.Count > 0)
            {
                ImageButton10.Visible = false;
                ImageButton11.Visible = false;
                lbl_moreversionError.Text = "";
                lbl_moreversionError.Visible = false;  
            }
             
            else
            {
                ImageButton10.Visible = true;
                ImageButton11.Visible = true;
                lbl_moreversionError.Text = "";
                lbl_moreversionError.Visible = false;  
            }


            string str = "select  dbo.PageMaster.FolderName AS Pagepath , pagename from pagemaster where PageId='" + ddlpage.SelectedValue + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable da = new DataTable();
            adp.Fill(da);

            if (da.Rows.Count > 0)
            {
                ViewState["Pagepath"] = da.Rows[0]["Pagepath"].ToString();
                string pagepath = Convert.ToString(ViewState["Pagepath"]);
                lbl_pagepath.Text = ViewState["Rootpath"] + "" + pagepath + "/" + da.Rows[0]["pagename"].ToString(); 
            }
            else
            {
                lbl_pagepath.Text = "";
            }
        }

        ddlpageversionid.Items.Insert(0, "-Select-");
        ddlpageversionid.Items[0].Value = "0";
        ImageButton10.Visible = true;
        ImageButton11.Visible = true;
        try
        {
            ddlpageversionid.SelectedIndex = 1;
            ddlProductname_SelectedIndexChanged(sender, e);
            ImageButton10.Visible = false;
            ImageButton11.Visible = false;
        }
        catch (Exception ex)
        {
        }

        Pagevfill();
    }
    protected void linkdow1_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        int data = Convert.ToInt32(GridView4.DataKeys[rinrow].Value);


        string strcount ;
        strcount = " Select * From PageWorkGuideUploadTbl where id='" + data + "' ";
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);

        if (dtcount.Rows.Count > 0)
        {

            string fnname = "";
            if (dtcount.Rows[0]["WorkRequirementPdfFilename"].ToString() != "")
            {
                fnname = dtcount.Rows[0]["WorkRequirementPdfFilename"].ToString();
            }
            else
            {
                fnname = dtcount.Rows[0]["WorkRequirementAudioFileName"].ToString();

            }


            string despath = Server.MapPath("~\\Attach\\") + fnname.ToString();
            //FileInfo file = new FileInfo(despath);
            //despath = "~\\Uploads\\" + fnname.ToString();
            despath = "http://license.busiwiz.com/Attachment/" + fnname.ToString();


            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + despath + "');", true);//'" + "Attachment/" + fnname.ToString() + "'
        }

     
        //string strcount = " select PageMaster.FolderName, PageWorkGuideUploadTbl.WorkRequirementPdfFilename,PageWorkGuideUploadTbl.WorkRequirementAudioFileName,WebsiteMaster.* from PageWorkGuideUploadTbl inner join PageWorkTbl on PageWorkTbl.Id=PageWorkGuideUploadTbl.PageWorkTblId inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkGuideUploadTbl.Id='" + data + "' ";
        //SqlCommand cmdcount = new SqlCommand(strcount, con);
        //DataTable dtcount = new DataTable();
        //SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        //adpcount.Fill(dtcount);

        //if (dtcount.Rows.Count > 0)
        //{
        //    lblftpurl123.Text = dtcount.Rows[0]["FTPWorkGuideUrl"].ToString();
        //    lblftpport123.Text = dtcount.Rows[0]["FTPWorkGuidePort"].ToString();
        //    lblftpuserid.Text = dtcount.Rows[0]["FTPWorkGuideUserId"].ToString();
        //    lblftppassword123.Text = PageMgmt.Decrypted(dtcount.Rows[0]["FTPWorkGuidePW"].ToString());
        //    //ViewState["folder"] = dtcount.Rows[0]["FolderName"].ToString();

        //    string[] separator1 = new string[] { "/" };
        //    string[] strSplitArr1 = lblftpurl123.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

        //    String productno = strSplitArr1[0].ToString();
        //    string ftpurl = "";

        //    if (productno == "FTP:" || productno == "ftp:")
        //    {
        //        if (strSplitArr1.Length >= 3)
        //        {
        //            ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;
        //            for (int i = 2; i < strSplitArr1.Length; i++)
        //            {
        //                ftpurl += "/" + strSplitArr1[i].ToString();
        //            }
        //        }
        //        else
        //        {
        //            ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;

        //        }
        //    }
        //    else
        //    {
        //        if (strSplitArr1.Length >= 2)
        //        {
        //            ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;
        //            for (int i = 1; i < strSplitArr1.Length; i++)
        //            {
        //                ftpurl += "/" + strSplitArr1[i].ToString();
        //            }
        //        }
        //        else
        //        {
        //            ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;

        //            ftpurl = "ftp://" + strSplitArr1[0].ToString() + "/";

        //        }

        //    }


        //    if (lblftpurl123.Text.Length > 0)
        //    {
        //        string[] seperator_RootPath = new string[] { "/" };
        //        string RootPath = Convert.ToString(dtcount.Rows[0]["RootFolderPath"]);
        //        string[] RootPathArray = RootPath.Split(seperator_RootPath, StringSplitOptions.RemoveEmptyEntries);
        //        string FolderName = "";
        //        for (int k = 2; k < RootPathArray.Length; k++)
        //        {
        //            FolderName += RootPathArray[k].ToString() + "/";
        //        }
        //        ViewState["folder"] = FolderName.ToString().Substring(0, FolderName.Length - 1);

        //        /*11 Dec2015 141 Folder Path  */
        //        //string ftphost = ftpurl + "/" + Convert.ToString(ViewState["folder"]) + "/Attach/";

        //        //141   string ftphost = ftpurl + "/Attach/";
        //        string ftphost = ftpurl;

        //        ///////string ftphost = ftpurl + "/" ;
        //        string fnname = "";
        //        if (dtcount.Rows[0]["WorkRequirementPdfFilename"].ToString() != "")
        //        {
        //            fnname = dtcount.Rows[0]["WorkRequirementPdfFilename"].ToString();
        //        }
        //        else
        //        {
        //            fnname = dtcount.Rows[0]["WorkRequirementAudioFileName"].ToString();

        //        }


        //        string despath = Server.MapPath("~\\Attachment\\") + fnname.ToString();

        //        FileInfo filec = new FileInfo(despath);
        //        try
        //        {
        //            if (!filec.Exists)
        //            {
        //                //  ftphost = "ftp://72.38.84.226/";

        //                GetFile(ftphost, fnname, despath, lblftpuserid.Text, lblftppassword123.Text);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            lblpaged.Text = ex.ToString();
        //        }


        //        despath = despath.Replace("\\Clientadmin", "");
        //        FileInfo file = new FileInfo(despath);
        //        if (file.Exists)
        //        {
        //            Response.Clear();
        //            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
        //            Response.AddHeader("Content-Length", file.Length.ToString());
        //            Response.ContentType = "application/octet-stream";
        //            Response.WriteFile(file.FullName);
        //            Response.End();
        //        }
        //        else
        //        {
        //          //  lblMsg1.Text = despath;
        //        }


        //    }


        //}

    }
    protected void Pagevfill()
    {
        grdpversion.DataSource = null;
        grdpversion.DataBind();
        string pv = "";
        if (ddlpage.SelectedIndex > 0)
        {
            pv = "  PageMaster.PageId='" + ddlpage.SelectedValue + "'";


            DataTable dtf = new DataTable();
            dtf = CreateDatatable();
            string pageve = "SELECT DISTINCT Case When(PageVersionTbl.Id IS NULL) then '0' else PageVersionTbl.Id End as pvid, VersionName,VersionNo,PageVersionTbl.PageName,Convert(nvarchar,PageVersionTbl.Date,101) as Date,PageMaster.PageId,PageMaster.PageName as Pname FROM PageMaster inner join PageVersionTbl on PageVersionTbl.PageMasterId= PageMaster.PageId where DeveloperOK = '0' and  " + pv + " and PageMaster.Active = '1' Order by pvid Desc";
            //string pageve = "SELECT DISTINCT Case When(PageVersionTbl.Id IS NULL) then '0' else PageVersionTbl.Id End as pvid, VersionName,VersionNo,PageVersionTbl.PageName,Convert(nvarchar,PageVersionTbl.Date,101) as Date,PageMaster.PageId,PageMaster.PageName as Pname FROM PageMaster inner join PageVersionTbl on PageVersionTbl.PageMasterId= PageMaster.PageId where ISNULL(DeveloperOK, 0)='0' and  " + pv + " Order by pvid Desc"; // old at 07-01-2015
            //  string pageve = "SELECT DISTINCT  PageVersionTbl.Id as pvid, VersionName,VersionNo,PageVersionTbl.PageName,Convert(nvarchar,PageVersionTbl.Date,101) as Date FROM PageVersionTbl INNER JOIN PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId   WHERE  PageVersionTbl.DeveloperOK='1' and PageVersionTbl.SupervisorOk='1' and PageVersionTbl.TesterOk='1'  and PageMaster.VersionInfoMasterId='" + ddlpageversionid.SelectedValue + "' " + pv + " Order by PageVersionTbl.Id";

            SqlCommand cmspv = new SqlCommand(pageve, con);
            SqlDataAdapter asp = new SqlDataAdapter(cmspv);
            DataTable dtpa = new DataTable();
            asp.Fill(dtpa);
            if (dtpa.Rows.Count > 0)
            {
                DataRow Drow1 = dtf.NewRow();

                Drow1["pvid"] = "0";
                Drow1["VersionName"] = "-";
                Drow1["VersionNo"] = "-";

                Drow1["PageName"] = Convert.ToString(dtpa.Rows[0]["Pname"]).Replace(" ", "");
                Drow1["Date"] = "-";
                Drow1["PageId"] = Convert.ToString(dtpa.Rows[0]["PageId"]);
                dtf.Rows.Add(Drow1);

                foreach (DataRow dtp in dtpa.Rows)
                {
                    DataRow Drow = dtf.NewRow();

                    Drow["pvid"] = Convert.ToString(dtp["pvid"]);
                    Drow["VersionName"] = Convert.ToString(dtp["VersionName"]);
                    Drow["VersionNo"] = Convert.ToString(dtp["VersionNo"]);
                    Drow["PageName"] = Convert.ToString(dtp["PageName"]).Replace(" ", "");
                    Drow["Date"] = Convert.ToString(dtp["Date"]);
                    Drow["PageId"] = Convert.ToString(dtp["PageId"]);
                    dtf.Rows.Add(Drow);

                }



            }
            else
            {
                string vfg = "SELECT DISTINCT PageMaster.PageId,PageMaster.PageName as Pname FROM PageMaster Where  " + pv;
                SqlCommand cmsd = new SqlCommand(vfg, con);
                SqlDataAdapter asdc = new SqlDataAdapter(cmsd);
                DataTable asd = new DataTable();
                asdc.Fill(asd);
              
                if (asd.Rows.Count > 0)
                {
                    DataRow Drow1 = dtf.NewRow();

                    Drow1["pvid"] = "0";
                    Drow1["VersionName"] = "-";
                    Drow1["VersionNo"] = "-";

                    Drow1["PageName"] = Convert.ToString(asd.Rows[0]["Pname"]).Replace(" ", "");
                    Drow1["Date"] = "-";
                    Drow1["PageId"] = Convert.ToString(asd.Rows[0]["PageId"]);
                    dtf.Rows.Add(Drow1);
                }
            }
            grdpversion.DataSource = dtf;
            grdpversion.DataBind();
            foreach (GridViewRow dts in grdpversion.Rows)
            {
                string griid = grdpversion.DataKeys[dts.RowIndex].Value.ToString();
                CheckBox chk = (CheckBox)dts.FindControl("chk");
                if (griid == "0")
                {
                    chk.Checked = true;
                }
            }
        }
    }
    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "pvid";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "VersionName";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "PageName";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "Date";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;


        DataColumn Dcom16 = new DataColumn();
        Dcom16.DataType = System.Type.GetType("System.String");
        Dcom16.ColumnName = "PageId";
        Dcom16.AllowDBNull = true;
        Dcom16.Unique = false;
        Dcom16.ReadOnly = false;

        DataColumn Dcom17 = new DataColumn();
        Dcom17.DataType = System.Type.GetType("System.String");
        Dcom17.ColumnName = "VersionNo";
        Dcom17.AllowDBNull = true;
        Dcom17.Unique = false;
        Dcom17.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);

        dt.Columns.Add(Dcom16);
        dt.Columns.Add(Dcom17);
        return dt;
    }

    protected void chkallowedfiledown_CheckedChanged(object sender, EventArgs e)
    {
        if (chkallowedfiledown.Checked == true)
        {
            pnlupfile.Visible = true;
        }
        else
        {
            pnlupfile.Visible = false;
            ViewState["data"] = null;
            grdgene.DataSource = null;
            grdgene.DataBind();
        }

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["data"]) == "")
        {
            dt = filldata();
        }
        else
        {
            dt = (DataTable)ViewState["data"];
        }
        DataRow Drow = dt.NewRow();
        Drow["Folderpath"] = txtfoldername.Text;
        Drow["Filename"] = txtfname.Text;

        dt.Rows.Add(Drow);
        ViewState["data"] = dt;
        grdgene.DataSource = dt;
        grdgene.DataBind();
    }
    protected DataTable filldata()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "Folderpath";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "Filename";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;



        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);

        return dt;
    }
    protected void grdgene_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            grdgene.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DeleteFromGrid(Convert.ToInt32(grdgene.SelectedIndex.ToString()));

        }
    }
    protected void DeleteFromGrid(int rowindex)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["data"];
        dt.Rows[rowindex].Delete();
        dt.AcceptChanges();
        grdgene.DataSource = dt;
        grdgene.DataBind();
        ViewState["data"] = dt;

        lblmsg.Text = "Record deleted successfully.";

    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            //Panel1.ScrollBars = ScrollBars.None;
            //Panel1.ScrollBars = ScrollBars.Horizontal;
            //Panel1.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button6.Visible = true;
            if (GridView1.Columns[17].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[17].Visible = false;
            }
            if (GridView1.Columns[18].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[18].Visible = false;
            }
        }
        else
        {

            //Panel1.ScrollBars = ScrollBars.Horizontal;

            Button4.Text = "Printable Version";
            Button6.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[17].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[18].Visible = true;
            }
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        lblmsg.Text = "";
        lbllegend.Text = "Add New Software Page Work Allocation ";
    }

    protected void chkothersourcefile_CheckedChanged(object sender, EventArgs e)
    {
        if (chkothersourcefile.Checked == true)
        {
            pnlothersourcefile.Visible = true;
        }
        else
        {
            pnlothersourcefile.Visible = false;
        }
    }
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

    }
    //protected void chktaskallocatetoemployee_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chktaskallocatetoemployee.Checked == true)
    //    {
    //        Panel2.Visible = true;
    //    }
    //    else
    //    {
    //        Panel2.Visible = false;
    //    }
    //}
    protected void ddlempassdeve_SelectedIndexChanged(object sender, EventArgs e)
    {       
            try
            {
                DDL_devForITwork.SelectedValue = ddlempassdeve.SelectedValue;
            }
            catch (Exception ex)
            {
            }
            try
            {
                ddl_testForITwork.SelectedValue = ddlempasstester.SelectedValue;
            }
            catch (Exception ex)
            {
            }
            try
            {
                ddl_suppITWork.SelectedValue = ddlempasssuper.SelectedValue;
            }
            catch (Exception ex)
            {
            }

        //costofdeveloper();

    }
   

    
    protected void txtbudgethourdev_TextChanged(object sender, EventArgs e)
    {
        costofdeveloper();        
    }

    protected void txtbudgethourdev_TextChangedTextBox14(object sender, EventArgs e)
    {
        //costofdeveloper();

        try
        {
            string checkhours;
            checkhours = TextBox14.Text.Replace(":", "");
            int Hourint;
            Hourint = Convert.ToInt32(checkhours);
            if (Hourint > 900)
            {
                TextBox14.Text = "00:00";
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            TextBox14.Text = "00:00";
        }

    }

    protected void txtbudgethourdev_TextChangedTextBox12(object sender, EventArgs e)
    {
        //costofdeveloper();
        try
        {
            string checkhours;
            checkhours = TextBox12.Text.Replace(":", "");
            int Hourint;
            Hourint = Convert.ToInt32(checkhours);
            if (Hourint > 900)
            {
                TextBox12.Text = "00:00";
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            TextBox12.Text = "00:00";
        }

    }

    protected void txtbudgethourdev_TextChangedTextBox16(object sender, EventArgs e)
    {
         try
         {
             string checkhours;
             checkhours = TextBox16.Text.Replace(":", "");
             int Hourint;
             Hourint = Convert.ToInt32(checkhours);
             if (Hourint > 900)
             {
                 TextBox16.Text = "00:00";
             }
             else
             { 
             
             }
         }
         catch (Exception ex)
         {
             TextBox16.Text = "00:00";
         }
       // costofdeveloper();


    }
    protected void costofdeveloper()
    {
        string str = "select * from EmployeeMaster where Id='" + ddlempassdeve.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable da = new DataTable();
        adp.Fill(da);

        if (da.Rows.Count > 0)
        {
            ViewState["EffectiveRate1"] = da.Rows[0]["EffectiveRate"].ToString();

            lbldeveloperrate.Text = ViewState["EffectiveRate1"].ToString();
        }

        if (txtbudgethourdev.Text.Length > 0)
        {
            string time = "";
            string outdifftime = "";
            string temp12 = "";
            string temp123 = "";
            TimeSpan t4 = TimeSpan.Parse(txtbudgethourdev.Text);
            time = t4.ToString();


            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
            temp12 = Convert.ToDateTime(time).ToString("HH");
            temp123 = Convert.ToDateTime(time).ToString("mm");

            double main1 = Convert.ToDouble(temp12);
            double main2 = Convert.ToDouble(temp123);

            double cost1 = main1 * Convert.ToDouble(ViewState["EffectiveRate1"].ToString());

            double cost2 = ((main2 / 60) * (Convert.ToDouble(ViewState["EffectiveRate1"].ToString())));
            double FinalCost = cost1 + cost2;
            FinalCost = Math.Round(FinalCost, 2);
            ViewState["FinalcostDeveloper"] = FinalCost.ToString();
            lbldevelopercost.Text = ViewState["FinalcostDeveloper"].ToString();
        }

    }
    protected void costofdtester()
    {
        string str = "select * from EmployeeMaster where Id='" + ddlempasstester.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable da = new DataTable();
        adp.Fill(da);

        if (da.Rows.Count > 0)
        {
            ViewState["EffectiveRate2"] = da.Rows[0]["EffectiveRate"].ToString();

            lbltesterrate.Text = ViewState["EffectiveRate2"].ToString();
        }

        if (txtbudhourtest.Text.Length > 0)
        {
            string time = "";
            string outdifftime = "";
            string temp12 = "";
            string temp123 = "";
            TimeSpan t4 = TimeSpan.Parse(txtbudhourtest.Text);
            time = t4.ToString();


            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
            temp12 = Convert.ToDateTime(time).ToString("HH");
            temp123 = Convert.ToDateTime(time).ToString("mm");

            double main1 = Convert.ToDouble(temp12);
            double main2 = Convert.ToDouble(temp123);

            double cost1 = main1 * Convert.ToDouble(ViewState["EffectiveRate2"].ToString());

            double cost2 = ((main2 / 60) * (Convert.ToDouble(ViewState["EffectiveRate2"].ToString())));
            double FinalCost = cost1 + cost2;
            FinalCost = Math.Round(FinalCost, 2);
            ViewState["FinalcostTester"] = FinalCost.ToString();
            lblcosttester.Text = ViewState["FinalcostTester"].ToString();
        }

    }
    protected void costofdsupervisor()
    {
        string str = "select * from EmployeeMaster where Id='" + ddlempasstester.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable da = new DataTable();
        adp.Fill(da);

        if (da.Rows.Count > 0)
        {
            ViewState["EffectiveRate3"] = da.Rows[0]["EffectiveRate"].ToString();

            lblratesupervisor.Text = ViewState["EffectiveRate3"].ToString();
        }

        if (txtbudhoursupcheck.Text.Length > 0)
        {
            string time = "";
            string outdifftime = "";
            string temp12 = "";
            string temp123 = "";
            TimeSpan t4 = TimeSpan.Parse(txtbudhoursupcheck.Text);
            time = t4.ToString();


            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
            temp12 = Convert.ToDateTime(time).ToString("HH");
            temp123 = Convert.ToDateTime(time).ToString("mm");

            double main1 = Convert.ToDouble(temp12);
            double main2 = Convert.ToDouble(temp123);

            double cost1 = main1 * Convert.ToDouble(ViewState["EffectiveRate3"].ToString());

            double cost2 = ((main2 / 60) * (Convert.ToDouble(ViewState["EffectiveRate3"].ToString())));
            double FinalCost = cost1 + cost2;
            FinalCost = Math.Round(FinalCost, 2);
            ViewState["FinalcostSupervisor"] = FinalCost.ToString();
            lblsupervisorcost.Text = ViewState["FinalcostSupervisor"].ToString();
        }

    }
    protected void ddlempasstester_SelectedIndexChanged(object sender, EventArgs e)
    {
        costofdtester();
    }
    protected void ddlempasssuper_SelectedIndexChanged(object sender, EventArgs e)
    {
        costofdsupervisor();
                        
    }
   
    protected void txtbudhoursupcheck_TextChanged(object sender, EventArgs e)
    {
        costofdsupervisor();
    }

    protected void txtbudhourtest_TextChanged(object sender, EventArgs e)
    {
        costofdtester();
    }
    protected void fillpage()
    {
        
        ddlpagenamefilter.Items.Clear();
        if (ddlfilterwebsite.SelectedIndex > -1)
        {

            string strcln = "";
            string stract = "";
            if (CheckBox3.Checked == true)
            {
                stract = " and PageMaster.Active='1' ";
            }
            if (CheckBox2.Checked == true)
            {
                stract = " and PageMaster.PageId IN ( Select PageMasterid From PageVersionTbl Where DeveloperOK=0 Group By PageMasterid ) ";
            }

            

           // strcln = "SELECT  MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName+'-'+SubMenuMaster.SubMenuName as Page_Name  from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId INNER JOIN dbo.PageVersionTbl ON dbo.PageMaster.PageId = dbo.PageVersionTbl.PageMasterId " +
            strcln = " SELECT        dbo.MainMenuMaster.MainMenuId, dbo.MainMenuMaster.MainMenuName, dbo.MainMenuMaster.BackColour, dbo.MainMenuMaster.MainMenuTitle, dbo.MainMenuMaster.MasterPage_Id, dbo.MainMenuMaster.MainMenuIndex, dbo.MainMenuMaster.Active, dbo.MainMenuMaster.LanguageId, dbo.PageMaster.PageId, dbo.PageMaster.PageName + '-' + dbo.PageMaster.PageTitle + '-' + dbo.MainMenuMaster.MainMenuName   AS Page_Name FROM dbo.PageMaster INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId LEFT OUTER JOIN dbo.SubMenuMaster ON dbo.SubMenuMaster.SubMenuId = dbo.PageMaster.SubMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MasterPageMaster.MasterPageId = dbo.MainMenuMaster.MasterPage_Id INNER JOIN dbo.WebsiteSection ON dbo.WebsiteSection.WebsiteSectionId = dbo.MasterPageMaster.WebsiteSectionId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId " +
                "  where    MasterPageMaster.MasterPageId='" + ddlfilterwebsite.SelectedValue + "'   and ( MainMenuMaster.MainMenuName  <> '' and  PageMaster.PageTitle  <> '') " + stract + "  ";// ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "'
            if (ViewState["pop_pageid"] != null)
            {
                strcln = "SELECT  distinct  MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName as Page_Name  from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId INNER JOIN dbo.PageVersionTbl ON dbo.PageMaster.PageId = dbo.PageVersionTbl.PageMasterId  where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MasterPageMaster.MasterPageId='" + ddlfilterwebsite.SelectedValue + "'   and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '')   ";

            }
            //if (ddlMainMenu.SelectedIndex > 0)
            //{
            //    str1 = "  and PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' ";

            //}

            //if (ddlSubmenu.SelectedIndex > 0)
            //{
            //    str2 = " and PageMaster.SubMenuId='" + ddlSubmenu.SelectedValue + "'";
            //}

            string orderby = "order by Page_Name";

            string finalstr = strcln  + orderby;

            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddlpagenamefilter.DataSource = dtcln;

            ddlpagenamefilter.DataValueField = "PageId";
            ddlpagenamefilter.DataTextField = "Page_Name";
            ddlpagenamefilter.DataBind();

            ddlpagenamefilter.Items.Insert(0, "All");
            ddlpagenamefilter.Items[0].Value = "0";

            if(ViewState["pop_pageid"]  !=null)
            {
                ddlpagenamefilter.SelectedValue =Convert.ToString(ViewState["pop_pageid"]); 
               
            }
        }

    }
    protected void ddlfilterwebsite_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpage();
    }

    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "EmployeeMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        FillProduct();

    }
    protected void chkver_CheckedChanged(object sender, EventArgs e)
    {
        if (chkver.Checked == true)
        {
            
            filterwebsitever();
        }
        else
        {
            string strcontrol = "";
            Drpver.Items.Clear();

            if (ddlpagenamefilter.SelectedIndex > 0)
            {

                //strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where   PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True'  order by PageVersionTbl.Id DESC";

                strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where   PageMaster.PageId='" + ddlpagenamefilter.SelectedValue + "'  and PageVersionTbl.Active='True' AND ISNULL(SupervisorOK, 0) = 0  order by PageVersionTbl.Id DESC";
                //strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where  PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' and PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True'  order by PageVersionTbl.Id";
                SqlCommand cmdcontrol = new SqlCommand(strcontrol, con);
                SqlDataAdapter adp123 = new SqlDataAdapter(cmdcontrol);
                DataTable dt = new DataTable();
                adp123.Fill(dt);
                Drpver.DataSource = dt;
                Drpver.DataTextField = "PageNameVersion";
                Drpver.DataValueField = "Id";
                Drpver.DataBind();
               // Session["dtVersionTbl"] = dt;

            }

            Drpver.Items.Insert(0, "-Select-");
            Drpver.Items[0].Value = "0";    
        }
    }

    protected void filterwebsitever()
    {
        string strcontrol = "";
        Drpver.Items.Clear();

        if (ddlpagenamefilter.SelectedIndex > 0)
        {

            //strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where   PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True'  order by PageVersionTbl.Id DESC";

            strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where   PageMaster.PageId='" + ddlpagenamefilter.SelectedValue + "'  and PageVersionTbl.Active='True' AND ISNULL(SupervisorOK, 0) = 0 AND ISNULL(DeveloperOK, 0) = 0 AND ISNULL(TesterOk, 0) = 0  order by PageVersionTbl.Id DESC";
            //strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where  PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' and PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True'  order by PageVersionTbl.Id";
            SqlCommand cmdcontrol = new SqlCommand(strcontrol, con);
            SqlDataAdapter adp123 = new SqlDataAdapter(cmdcontrol);
            DataTable dt = new DataTable();
            adp123.Fill(dt);
            Drpver.DataSource = dt;
            Drpver.DataTextField = "PageNameVersion";
            Drpver.DataValueField = "Id";
            Drpver.DataBind();
            //Session["dtVersionTbl"] = dt;

        }

        Drpver.Items.Insert(0, "-Select-");
        Drpver.Items[0].Value = "0";    
        
    }

    protected void filterallver()
    {

          string strcontrol = "";
          Drpver.Items.Clear();

          if (ddlpagenamefilter.SelectedIndex > 0)
            {

                //strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where   PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True'  order by PageVersionTbl.Id DESC";

                strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where   PageMaster.PageId='" + ddlpagenamefilter.SelectedValue + "'  and PageVersionTbl.Active='True' AND ISNULL(SupervisorOK, 0) = 0  order by PageVersionTbl.Id DESC";
                //strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where  PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' and PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True'  order by PageVersionTbl.Id";
                SqlCommand cmdcontrol = new SqlCommand(strcontrol, con);
                SqlDataAdapter adp123 = new SqlDataAdapter(cmdcontrol);
                DataTable dt = new DataTable();
                adp123.Fill(dt);
                Drpver.DataSource = dt;
                Drpver.DataTextField = "PageNameVersion";
                Drpver.DataValueField = "Id";
                Drpver.DataBind();
                //Session["dtVersionTbl"] = dt;

            }

            Drpver.Items.Insert(0, "-Select-");
            Drpver.Items[0].Value = "0";    
        


    }
    protected void ddlpagenamefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterallver();
    }

    protected void chkyes_CheckedChanged(object sender, EventArgs e)
    {
        if (chkyes.Checked == true)
        {
            string strpageversionname = "select * from PageVersionTbl where Id='" + ddlpageversionid.SelectedValue + "'";

            SqlCommand cmdcontrol = new SqlCommand(strpageversionname, con);
            SqlDataAdapter adp123 = new SqlDataAdapter(cmdcontrol);
            DataTable dt = new DataTable();
            adp123.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtworkrequtitle.Text = "Work for" + dt.Rows[0]["PageName"].ToString();
            }
            else
            {
                txtworkrequtitle.Text = "";
            }
            btnSubmit.Text = "Update";
           
        }
        else
        {
            
        }
    }
    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        
         
        //-----------
        string str = "Select Top(1) PageVersionTbl.Id,PageVersionTbl.VersionNo,PageMaster.PageName from PageVersionTbl left join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where PageVersionTbl.PageMasterId='" + ddlpage.SelectedValue + "' Order by Id Desc";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        
        string df;
        if (dt.Rows.Count > 0)
        {

            txtPageName = dt.Rows[0]["PageName"].ToString().Replace(" ", "");
            string[] separator1 = new string[] { "." };
            string[] strSplitArr1 = txtPageName.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
            string len = Convert.ToString(strSplitArr1.Length);
            int pver = 0;
            decimal pverdec = 0;
            if (Convert.ToString(dt.Rows[0]["VersionNo"]) != "")
            {
                pverdec = Convert.ToDecimal(dt.Rows[0]["VersionNo"]) + 1;
            }
            else
            {
                pverdec = 1;
            }
            string[] separ = new string[] { "." };
            string[] strSplitAr = txtPageName.Split(separ, StringSplitOptions.RemoveEmptyEntries);
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
                txtPageName = strSplitArr1[0].ToString() + "Ver" + pver + "." + strSplitArr1[1].ToString();
            }
           txtVersionNo = pver.ToString();
          txtVersionName = "Version-" + pver;
            //CheckBox1.Checked = true;
        }
        //--
        string MasterInsert = "Insert Into PageVersionTbl(PageMasterId,VersionName,Date,VersionNo,PageName,Active,FolderName,SupervisorOk) values ('" + ddlpage.SelectedValue + "','" + txtVersionName + "','" + System.DateTime.Now.Date + "','" + txtVersionNo + "','" + txtPageName + "','1','','0')";
        SqlCommand cmd1 = new SqlCommand(MasterInsert, con);
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close();

       string  strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion,  dbo.PageMaster.FolderName AS Pagepath   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where   PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True' AND ISNULL(SupervisorOK, 0) = 0  order by PageVersionTbl.Id DESC";
        //strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where  PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' and PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True'  order by PageVersionTbl.Id";
        SqlCommand cmdcontrol = new SqlCommand(strcontrol, con);
        SqlDataAdapter adp123 = new SqlDataAdapter(cmdcontrol);
        DataTable dt2 = new DataTable();
        adp123.Fill(dt2);
        Session["dtVersionTbl"] = dt2;


        ImageButton11_Click(sender, e);
       // string te = "PageVersionForm.aspx";
       // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ImageButton11_Click(object sender, ImageClickEventArgs e)
    {
        string strcontrol;
        ddlpageversionid.Items.Clear();
        strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where   PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True' AND ISNULL(SupervisorOK, 0) = 0  order by PageVersionTbl.Id DESC";
        //strcontrol = "select PageVersionTbl.*, PageMaster.PageTitle +':'+PageVersionTbl.VersionNo +':'+PageVersionTbl.PageName as PageNameVersion   from  PageVersionTbl inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where  PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' and PageMaster.PageId='" + ddlpage.SelectedValue + "'  and PageVersionTbl.Active='True'  order by PageVersionTbl.Id";
        SqlCommand cmdcontrol = new SqlCommand(strcontrol, con);
        SqlDataAdapter adp123 = new SqlDataAdapter(cmdcontrol);
        DataTable dt = new DataTable();
        adp123.Fill(dt);
        ddlpageversionid.DataSource = dt;
        ddlpageversionid.DataTextField = "PageNameVersion";
        ddlpageversionid.DataValueField = "Id";
        ddlpageversionid.DataBind();
        ddlpageversionid.Items.Insert(0, "-Select-");
        ddlpageversionid.Items[0].Value = "0";
        ImageButton10.Visible = true;
        ImageButton11.Visible = true;
        try
        {
            ddlpageversionid.SelectedIndex = 1;
            ddlProductname_SelectedIndexChanged(sender, e);
            ImageButton10.Visible = false;
            ImageButton11.Visible = false;
        }
        catch (Exception ex)
        {
        }
        //Session["dtVersionTbl"] = dt;
    }

    //-----IT Work Allocation----------------------------

    protected void txttargetdatedeve_TextChanged(object sender, EventArgs e)
    {
        if (txttargetdatedeve.Text.ToString() == "")
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please Select date";

        }
        else
        {
            //fillpagename();
            //fillbudgetdhour();
            //maintaskbudhourcost();
            //todaytaskbudhourcost();

        }
        TextBox10.Text = TextBox9.Text;
        filltodayhourDEV();
        

    }

    protected void txttargetdatedeve_TextChanged2(object sender, EventArgs e)
    {
        if (txttargetdatedeve.Text.ToString() == "")
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please Select date";

        }
        else
        {
            //fillpagename();
            //fillbudgetdhour();
            //maintaskbudhourcost();
            //todaytaskbudhourcost();

        }
        TextBox9.Text = TextBox10.Text;
        filltodayhourDEV();


    }

     protected void filltodayhourDEV()
    {
        // string strcln = " SELECT * from MyDailyWorkReport where EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "'  ";
        string strcln = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + ddlempassdeve.SelectedValue + "' and workallocationdate='" + txt_dateDevITwork.Text + "'";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {
            string TotalHour = dtcln.Rows[0]["TotalHours"].ToString();
            string TotalMinutes = dtcln.Rows[0]["TotalMinutes"].ToString();

            if (TotalHour == "" && TotalMinutes == "")
            {
                TotalHour = "00";
                TotalMinutes = "00";

            }
            double contohour;
            try
            {
                string time = "";
                string outdifftime = "";
                string temp12 = "";
                string temp123 = "";
                TimeSpan t4 = TimeSpan.Parse(txt_devhourITwork.Text);
                time = t4.ToString();


                outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
                temp12 = Convert.ToDateTime(time).ToString("HH");
                temp123 = Convert.ToDateTime(time).ToString("mm");

                double main1 = Convert.ToDouble(temp12);
                main1 = main1 * 60;
                double main2 = Convert.ToDouble(temp123);
                main1 = main1 + main2;


                contohour = Convert.ToInt32(TotalHour);
                contohour = contohour * 60;
                contohour = contohour + main1;
                TotalHour =Convert.ToString(contohour);
            }
            catch (Exception ex)
            {
                
            }

            if (Convert.ToInt32(TotalHour) > 5400)
            {
                Lbl_notHour.Visible =true;
                btnSubmit.Enabled=false;
                Panel6.Visible = true;
                pbl_ITworkallocation.Visible = true;
                Lbl_notHour.Text = " Insufficient Time Available on this Day for Developer. Select Another Date ";
            }
            else 
            {
                Lbl_notHour.Visible =false;
                btnSubmit.Enabled=true;
                Panel6.Visible = false;
                pbl_ITworkallocation.Visible = true ;
                Lbl_notHour.Text = "";
                filltodayhourTEST();
                
            }
            lbltotalworkallocatedtoday.Text = TotalHour + ":" + TotalMinutes;

        }
        else
        {
            lbltotalworkallocatedtoday.Text = "";

        }



    }

     protected void filltodayhourTEST()
    {
        // string strcln = " SELECT * from MyDailyWorkReport where EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "'  ";
        string strcln = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + ddlempasstester.SelectedValue + "' and workallocationdate='" + txt_dateTestITwork.Text + "'";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {
            string TotalHour = dtcln.Rows[0]["TotalHours"].ToString();
            string TotalMinutes = dtcln.Rows[0]["TotalMinutes"].ToString();

            if (TotalHour == "" && TotalMinutes == "")
            {
                TotalHour = "0";
                TotalMinutes = "0";

            }
            double contohour;
            try
            {
                string time = "";
                string outdifftime = "";
                string temp12 = "";
                string temp123 = "";
                TimeSpan t4 = TimeSpan.Parse(txt_BudhourITwork.Text);
                time = t4.ToString(); 

                outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
                temp12 = Convert.ToDateTime(time).ToString("HH");
                temp123 = Convert.ToDateTime(time).ToString("mm");

                double main1 = Convert.ToDouble(temp12);
                main1 = main1 * 60;
                double main2 = Convert.ToDouble(temp123);
                main1 = main1 + main2;


                contohour = Convert.ToInt32(TotalHour);
                contohour = contohour * 60;
                contohour = contohour + main1;
                TotalHour = Convert.ToString(contohour);
            }
            catch (Exception ex)
            {

            }

             if(Convert.ToInt32(TotalHour) > 540)
            {
                Lbl_notHour.Visible =true;
                btnSubmit.Enabled=false;
                pbl_ITworkallocation.Visible = true;
                Panel6.Visible = true;

                Lbl_notHour.Text = " Insufficient Time Available on this Day for Tester. Select Another Date ";
            }
            else 
            {
                Lbl_notHour.Visible =false;
                btnSubmit.Enabled=true;
                Panel6.Visible = false;
                pbl_ITworkallocation.Visible = true ;
                 Lbl_notHour.Text = "";
                 filltodayhourSUP();
            }
            lbltotalworkallocatedtoday.Text = TotalHour + ":" + TotalMinutes;
        }
        else
        {
            lbltotalworkallocatedtoday.Text = "";

        }



    }

     protected void filltodayhourSUP()
    {
        // string strcln = " SELECT * from MyDailyWorkReport where EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "'  ";
        string strcln = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + ddlempasssuper.SelectedValue + "' and workallocationdate='" + txt_DateSupITwork.Text + "'";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {
            string TotalHour = dtcln.Rows[0]["TotalHours"].ToString();
            string TotalMinutes = dtcln.Rows[0]["TotalMinutes"].ToString();

            if (TotalHour == "" && TotalMinutes == "")
            {
                TotalHour = "0";
                TotalMinutes = "0";

            }
            double contohour;
            try
            {
                string time = "";
                string outdifftime = "";
                string temp12 = "";
                string temp123 = "";
                TimeSpan t4 = TimeSpan.Parse(txt_SuphourITwork.Text);
                time = t4.ToString();

                outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
                temp12 = Convert.ToDateTime(time).ToString("HH");
                temp123 = Convert.ToDateTime(time).ToString("mm");

                double main1 = Convert.ToDouble(temp12);
                main1 = main1 * 60;
                double main2 = Convert.ToDouble(temp123);
                main1 = main1 + main2;


                contohour = Convert.ToInt32(TotalHour);
                contohour = contohour * 60;
                contohour = contohour + main1;
                TotalHour = Convert.ToString(contohour);
            }
            catch (Exception ex)
            {

            }
            //
             if(Convert.ToInt32(TotalHour) > 540)
            {
                Lbl_notHour.Visible =true;
                btnSubmit.Enabled=false;
                Panel6.Visible = true;
                pbl_ITworkallocation.Visible = true;
                Lbl_notHour.Text = " Insufficient Time Available on this Day for Supervisor. Select Another Date ";
            }
            else 
            {
                Lbl_notHour.Visible =false;
                btnSubmit.Enabled=true;
                Panel6.Visible = false;
                pbl_ITworkallocation.Visible = true;
                Lbl_notHour.Text = "";
            }
            lbltotalworkallocatedtoday.Text = TotalHour + ":" + TotalMinutes;
        }
        else
        {
            lbltotalworkallocatedtoday.Text = "";

        }



    }


     protected void txttargetdatedeve_TextChanged2Popup(object sender, EventArgs e)
     {

         filltodayhourDEVPopp();

     }
     protected void filltodayhourDEVPopp()
     {
         // string strcln = " SELECT * from MyDailyWorkReport where EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "'  ";
         string strcln = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + DropDownList1.SelectedValue + "' and workallocationdate='" + TextBox11S.Text + "'";
         SqlCommand cmdcln = new SqlCommand(strcln, con);
         DataTable dtcln = new DataTable();
         SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
         adpcln.Fill(dtcln);

         if (dtcln.Rows.Count > 0)
         {
             string TotalHour = dtcln.Rows[0]["TotalHours"].ToString();
             string TotalMinutes = dtcln.Rows[0]["TotalMinutes"].ToString();

             if (TotalHour == "" && TotalMinutes == "")
             {
                 TotalHour = "0";
                 TotalMinutes = "0";

             }

             double contohour;
             try
             {
                 string time = "";
                 string outdifftime = "";
                 string temp12 = "";
                 string temp123 = "";
                 TimeSpan t4 = TimeSpan.Parse(TextBox12.Text);
                 time = t4.ToString();

                 outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
                 temp12 = Convert.ToDateTime(time).ToString("HH");
                 temp123 = Convert.ToDateTime(time).ToString("mm");

                 double main1 = Convert.ToDouble(temp12);
                 main1 = main1 * 60;
                 double main2 = Convert.ToDouble(temp123);
                 main1 = main1 + main2;


                 contohour = Convert.ToInt32(TotalHour);
                 contohour = contohour * 60;
                 contohour = contohour + main1;
                 TotalHour = Convert.ToString(contohour);
             }
             catch (Exception ex)
             {

             }

             if (Convert.ToInt32(TotalHour) > 540)
             {
                 lbl_message.Visible = true;
               //  Button12S.Enabled = false;
                 lbl_message.Text = " Insufficient Time Available on this Day for Developer. Select Another Date ";
                 //ModalPopupExtender3.Show(); 
                 checkHoursavailability = 1;
                 return;
             }
             else
             {
                 checkHoursavailability = 2;
                 lbl_message.Visible = false;
                 Button12S.Enabled = true;
                 
                 lbl_message.Text = "";
                 filltodayhourTESTpopup();

             }
             lbltotalworkallocatedtoday.Text = TotalHour + ":" + TotalMinutes;

         }
         else
         {
             lbltotalworkallocatedtoday.Text = "";

         }



     }

     protected void filltodayhourTESTpopup()
     {
         // string strcln = " SELECT * from MyDailyWorkReport where EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "'  ";
         string strcln = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + DropDownList2.SelectedValue + "' and workallocationdate='" + TextBox13.Text + "'";
         SqlCommand cmdcln = new SqlCommand(strcln, con);
         DataTable dtcln = new DataTable();
         SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
         adpcln.Fill(dtcln);

         if (dtcln.Rows.Count > 0)
         {
             string TotalHour = dtcln.Rows[0]["TotalHours"].ToString();
             string TotalMinutes = dtcln.Rows[0]["TotalMinutes"].ToString();

             if (TotalHour == "" && TotalMinutes == "")
             {
                 TotalHour = "0";
                 TotalMinutes = "0";

             }
             double contohour;
             try
             {
                 string time = "";
                 string outdifftime = "";
                 string temp12 = "";
                 string temp123 = "";
                 TimeSpan t4 = TimeSpan.Parse(TextBox14.Text);
                 time = t4.ToString();

                 outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
                 temp12 = Convert.ToDateTime(time).ToString("HH");
                 temp123 = Convert.ToDateTime(time).ToString("mm");

                 double main1 = Convert.ToDouble(temp12);
                 main1 = main1 * 60;
                 double main2 = Convert.ToDouble(temp123);
                 main1 = main1 + main2;


                 contohour = Convert.ToInt32(TotalHour);
                 contohour = contohour * 60;
                 contohour = contohour + main1;
                 TotalHour = Convert.ToString(contohour);
             }
             catch (Exception ex)
             {

             }
             if (Convert.ToInt32(TotalHour) > 540)
             {
                 lbl_message.Visible = true;
               //  Button12S.Enabled = false;
                 lbl_message.Text = " Insufficient Time Available on this Day for Tester. Select Another Date ";
                 checkHoursavailability = 1;
                 return;
             }
             else
             {
                 checkHoursavailability = 2;
                 lbl_message.Visible = false;
                 Button12S.Enabled = true;
                 lbl_message.Text = "";
                 filltodayhourSUPpopup();
             }
             lbltotalworkallocatedtoday.Text = TotalHour + ":" + TotalMinutes;
         }
         else
         {
             lbltotalworkallocatedtoday.Text = "";

         }



     }

     protected void filltodayhourSUPpopup()
     {
         // string strcln = " SELECT * from MyDailyWorkReport where EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "'  ";
         string strcln = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + DropDownList3.SelectedValue + "' and workallocationdate='" + TextBox15.Text + "'";
         SqlCommand cmdcln = new SqlCommand(strcln, con);
         DataTable dtcln = new DataTable();
         SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
         adpcln.Fill(dtcln);

         if (dtcln.Rows.Count > 0)
         {
             string TotalHour = dtcln.Rows[0]["TotalHours"].ToString();
             string TotalMinutes = dtcln.Rows[0]["TotalMinutes"].ToString();

             if (TotalHour == "" && TotalMinutes == "")
             {
                 TotalHour = "0";
                 TotalMinutes = "0";

             }

             double contohour;
             try
             {
                 string time = "";
                 string outdifftime = "";
                 string temp12 = "";
                 string temp123 = "";
                 TimeSpan t4 = TimeSpan.Parse(TextBox16.Text);
                 time = t4.ToString();

                 outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
                 temp12 = Convert.ToDateTime(time).ToString("HH");
                 temp123 = Convert.ToDateTime(time).ToString("mm");

                 double main1 = Convert.ToDouble(temp12);
                 main1 = main1 * 60;
                 double main2 = Convert.ToDouble(temp123);
                 main1 = main1 + main2;


                 contohour = Convert.ToInt32(TotalHour);
                 contohour = contohour * 60;
                 contohour = contohour + main1;
                 TotalHour = Convert.ToString(contohour);
             }
             catch (Exception ex)
             {

             }
             if (Convert.ToInt32(TotalHour) > 540)
             {
                 lbl_message.Visible = true;
                 //Button12S.Enabled = false;
                 
                 lbl_message.Text = " Insufficient Time Available on this Day for Supervisor. Select Another Date ";
                 checkHoursavailability = 1;
                 return;
             }
             else
             {
                 lbl_message.Visible = false;
                 Button12S.Enabled = true;
                 lbl_message.Text = "";
                 checkHoursavailability = 2;
             }
             lbltotalworkallocatedtoday.Text = TotalHour + ":" + TotalMinutes;
         }
         else
         {
             lbltotalworkallocatedtoday.Text = "";

         }



     }
     protected void txttargetdatedeve_TextChangedRBL(object sender, EventArgs e)
     {
         if (RadioButtonList1.SelectedValue == "1")
         {
             TextBox10.Visible = true;
             ImageButton14.Visible = true;
         }
         else
         {
             TextBox10.Visible = false;
             ImageButton14.Visible = false;
             btnSubmit.Enabled = true; 
         }
     }
}

