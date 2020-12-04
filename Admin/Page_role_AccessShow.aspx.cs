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
using AjaxControlToolkit;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
//using Microsoft.SqlServer.Management.Common;
using System.Xml;
using System.IO;
//using Microsoft.SqlServer.Management.Smo;
using System.Security.Cryptography;
public partial class Shoppingcart_Admin_Page_role_Access : System.Web.UI.Page
{
    string compid;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
      
       
        if (Convert.ToInt32(Request.QueryString["roleid"]) > 0)
        {
            Session["ClientId"] = Request.QueryString["clientid"].ToString();
            compid = Session["ClientId"].ToString();
            if (!IsPostBack)
            {
                FillRole();
                Rlist_SelectedIndexChanged(sender, e);
                ViewState["sortOrder"] = "";

            }
        }
        

    }



    protected void FillRole()
    {

        string k1 = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where Role_id='" + Request.QueryString["roleid"] + "'  order by Role_name";
       
          SqlCommand  cmd = new SqlCommand(k1, con);
          SqlDataAdapter  adp = new SqlDataAdapter(cmd);
          DataTable dt = new DataTable();
          adp.Fill(dt);
        dpdrolename.DataSource = dt;
        dpdrolename.DataTextField = "Role_name";
        dpdrolename.DataValueField = "Role_id";
        dpdrolename.DataBind();
        //if (Convert.ToInt32(Request.QueryString["roleid"]) > 0)
        //{
        //    dpdrolename.SelectedValue = Request.QueryString["roleid"];
        //    dpdrolename.Enabled = false;
        //    grdmain.Enabled = false;
        //    Grdsub.Enabled = false;
        //    grdpage.Enabled = false;
        //}
        //else
        //{
        //    dpdrolename.SelectedIndex = 1;
        //}  

    }
    protected void fillgridmenu()
    {
        Label1.Text = "";
        grdmain.DataSource = null;
        grdmain.DataBind();
        if (Rlist.SelectedIndex == 0)
        {
            grdmain.DataSource = null;
            grdmain.DataBind();
            pnlpage.Visible = false;
            pnlsubmanu.Visible = false;
            pnlmain.Visible = true;
                    // string k1 = "select MainMenuMaster.* from  MainMenuMaster   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ProductMaster.ClientMasterId='" + compid + "' and VersionInfoMaster.Active ='True' and MainMenuMaster.Active='1' and WebsiteMaster.compid='"+Session["ClientId"]+"'  order by MainMenuName";
            string k1 = "SELECT Distinct MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,MainMenuMaster.MainMenuId,Case when( RoleMenuAccessRightTbl.AccessRight IS NULL) then '0' else AccessRight end as AccessRight, " +
       " Case when( RoleMenuAccessRightTbl.Edit_Right IS NULL) then '0' else Edit_Right end as Edit_Right,Case when( RoleMenuAccessRightTbl.Delete_Right IS NULL) then '0' else Delete_Right end as Delete_Right," +
       "Case when( RoleMenuAccessRightTbl.Download_Right IS NULL) then '0' else Download_Right end as Download_Right,Case when( RoleMenuAccessRightTbl.Insert_Right IS NULL) then '0' else Insert_Right end as Insert_Right," +
                      "Case when( RoleMenuAccessRightTbl.Update_Right IS NULL) then '0' else  Update_Right end as Update_Right,Case when( RoleMenuAccessRightTbl.View_Right IS NULL) then '0' else View_Right end as View_Right," +
                          "Case when( RoleMenuAccessRightTbl.Go_Right IS NULL) then '0' else Go_Right end as Go_Right,Case when( RoleMenuAccessRightTbl.SendMail_Right IS NULL) then '0' else SendMail_Right end as SendMail_Right " +
       "FROM  RoleMenuAccessRightTbl Right join MainMenuMaster on MainMenuMaster.MainMenuId=RoleMenuAccessRightTbl.MenuId and    RoleMenuAccessRightTbl.RoleId='" + dpdrolename.SelectedValue + "' inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId INNER JOIN MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ProductMaster.ClientMasterId='" + compid + "' and VersionInfoMaster.Active ='True' and MainMenuMaster.Active='1' and WebsiteMaster.compid='" + Session["ClientId"] + "'  order by MainMenuName";

            SqlCommand cmd = new SqlCommand(k1, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                DataView myDataView = new DataView();
                myDataView = dt.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }

                grdmain.DataSource = dt;
                grdmain.DataBind();
            }

            else
            {
                grdmain.DataSource = null;
                grdmain.DataBind();

            }

        }
        else if (Rlist.SelectedIndex == 1)
        {
            Fillmainfilter();
            string fil = "";
            if (ddlmainfilter.SelectedIndex > 0)
            {
                fil = " and MainMenuMaster.MainMenuId='" + ddlmainfilter.SelectedValue + "'";
            }
                   //  string k1 = "select MainMenuMaster.*,SubMenuMaster.* from  MainMenuMaster  inner join SubMenuMaster on SubMenuMaster.MainMenuId= MainMenuMaster.MainMenuId  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ProductMaster.ClientMasterId='" + compid + "' and VersionInfoMaster.Active ='True' and WebsiteMaster.compid='"+Session["ClientId"]+"' and MainMenuMaster.Active='1' " + fil + " order by MainMenuName,SubMenuName";
              string k1 = "SELECT Distinct  MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId,Case when( RoleSubMenuAccessRightTbl.AccessRight IS NULL) then '0' else AccessRight end as AccessRight, " +
                " Case when( RoleSubMenuAccessRightTbl.Edit_Right IS NULL) then '0' else Edit_Right end as Edit_Right,Case when( RoleSubMenuAccessRightTbl.Delete_Right IS NULL) then '0' else Delete_Right end as Delete_Right," +
                 "Case when( RoleSubMenuAccessRightTbl.Download_Right IS NULL) then '0' else Download_Right end as Download_Right,Case when(RoleSubMenuAccessRightTbl.Insert_Right IS NULL) then '0' else Insert_Right end as Insert_Right," +
                 "Case when( RoleSubMenuAccessRightTbl.Update_Right IS NULL) then '0' else  Update_Right end as Update_Right,Case when( RoleSubMenuAccessRightTbl.View_Right IS NULL) then '0' else View_Right end as View_Right," +
                 "Case when( RoleSubMenuAccessRightTbl.Go_Right IS NULL) then '0' else Go_Right end as Go_Right,Case when(RoleSubMenuAccessRightTbl.SendMail_Right IS NULL) then '0' else SendMail_Right end as SendMail_Right " +
                " FROM MainMenuMaster inner join SubMenuMaster on SubMenuMaster.MainMenuId= MainMenuMaster.MainMenuId Left join RoleSubMenuAccessRightTbl on RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId and RoleSubMenuAccessRightTbl.RoleId='"+dpdrolename.SelectedValue+"' inner join PageMaster on PageMaster.SubMenuId=SubMenuMaster.SubMenuId  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ProductMaster.ClientMasterId='" + compid + "' and VersionInfoMaster.Active ='True' and WebsiteMaster.compid='"+Session["ClientId"]+"' and MainMenuMaster.Active='1' " + fil + " order by MainMenuName,SubMenuName";
          
            pnlpage.Visible = false;
            pnlsubmanu.Visible = true;
            pnlmain.Visible = false;
            SqlCommand cmd = new SqlCommand(k1, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                DataView myDataView = new DataView();
                myDataView = dt.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }

                Grdsub.DataSource = dt;
                Grdsub.DataBind();
            }
            else
            {
                Grdsub.DataSource = null;
                Grdsub.DataBind();

            }
        }
        else if (Rlist.SelectedIndex == 2)
           
        {
            string fil = "";
            if (ddlmailpagefilter.SelectedIndex > 0)
            {
                fil = " and PageMaster.MainMenuId='" + ddlmailpagefilter.SelectedValue + "'";
            }
            if (ddlsubpagefilter.SelectedIndex > 0)
            {
                fil = " And PageMaster.Submenuid='" + ddlsubpagefilter.SelectedValue + "'";
            }
           // Fillmainfilter();//where RoleId='" + dpdrolename.SelectedValue + "'";
           // fillsubfilter();
                      //string k1 = "select distinct SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId, PageMaster.PageId,PageMaster.FolderName,MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,PageMaster.Active, ProductMaster.ProductName,VersionInfoMaster.VersionInfoName, PageMaster.PageName,PageMaster.PageTitle   from  ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId  inner join PageMaster on PageMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId  inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId where   ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and MainMenuMaster.Active=1 and MainMenuMaster.MainMenuId in(Select  MainMenuMaster.MainMenuId from MainMenuMaster inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId  where WebsiteMaster.compid='" + Session["ClientId"] + "') " + fil + " order by MainMenuName,SubMenuName,PageName";
            string k1 = "Select Distinct PageMaster.PageName,PageMaster.PageTitle,PageMaster.PageId, MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId,Case when( RolePageAccessRightTbl.AccessRight IS NULL) then '0' else AccessRight end as AccessRight, " +
      " Case when( RolePageAccessRightTbl.Edit_Right IS NULL) then '0' else Edit_Right end as Edit_Right,Case when( RolePageAccessRightTbl.Delete_Right IS NULL) then '0' else Delete_Right end as Delete_Right," +
      "Case when( RolePageAccessRightTbl.Download_Right IS NULL) then '0' else Download_Right end as Download_Right,Case when( RolePageAccessRightTbl.Insert_Right IS NULL) then '0' else Insert_Right end as Insert_Right," +
                     "Case when( RolePageAccessRightTbl.Update_Right IS NULL) then '0' else  Update_Right end as Update_Right,Case when( RolePageAccessRightTbl.View_Right IS NULL) then '0' else View_Right end as View_Right," +
                         "Case when( RolePageAccessRightTbl.Go_Right IS NULL) then '0' else Go_Right end as Go_Right,Case when( RolePageAccessRightTbl.SendMail_Right IS NULL) then '0' else SendMail_Right end as SendMail_Right " +
      "FROM  RolePageAccessRightTbl Right join PageMaster on PageMaster.PageId=RolePageAccessRightTbl.PageId and    RolePageAccessRightTbl.RoleId='" + dpdrolename.SelectedValue + "'  inner join MainMenuMaster on MainMenuMaster.MainMenuId= PageMaster.MainMenuId left join SubMenuMaster on SubMenuMaster.SubMenuId= PageMaster.SubMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId  where WebsiteMaster.compid='" + Session["ClientId"] + "' " + fil + " order by MainMenuName,SubMenuName,PageName";
      
            pnlpage.Visible = false; ;
            pnlpage.Visible = true;
            pnlsubmanu.Visible = false;
            pnlmain.Visible = false;
            SqlCommand cmd = new SqlCommand(k1, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                DataView myDataView = new DataView();
                myDataView = dt.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }

                grdpage.DataSource = dt;
                grdpage.DataBind();
                fillg(grdpage);
            }
            else
            {
                grdpage.DataSource = null;
                grdpage.DataBind();
            }
        }
        


 
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

    protected void RadioButtonListgrdmain_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void fillsubfilter()
    {
        ddlsubpagefilter.Items.Clear();
        if (ddlsubpagefilter.Items.Count <= 1)
        {
            string k1 = "SELECT [SubMenuName],[SubMenuId] FROM [SubMenuMaster] inner join MainMenuMaster on SubMenuMaster.MainMenuId= MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId  where MainMenuMaster.MainMenuId='" + ddlmailpagefilter.SelectedValue + "' and WebsiteMaster.compid='"+Session["ClientId"]+"' order by SubMenuName";

            SqlCommand cmd = new SqlCommand(k1, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            ddlsubpagefilter.DataSource = dt;
            ddlsubpagefilter.DataTextField = "SubMenuName";
            ddlsubpagefilter.DataValueField = "SubMenuId";
            ddlsubpagefilter.DataBind();
            ddlsubpagefilter.Items.Insert(0, "All");
            ddlsubpagefilter.Items[0].Value = "0";
 
        }

    }
    protected void Fillmainfilter()
    {
        if (ddlmainfilter.Items.Count <= 0)
        {
            string k1 = "SELECT [MainMenuName],[MainMenuId] FROM [MainMenuMaster] inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where  WebsiteMaster.compid='"+Session["ClientId"]+"' order by MainMenuName";

            SqlCommand cmd = new SqlCommand(k1, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            ddlmainfilter.DataSource = dt;
            ddlmainfilter.DataTextField = "MainMenuName";
            ddlmainfilter.DataValueField = "MainMenuId";
            ddlmainfilter.DataBind();
            ddlmainfilter.Items.Insert(0, "All");
            ddlmainfilter.Items[0].Value = "0";

            ddlmailpagefilter.DataSource = dt;
            ddlmailpagefilter.DataTextField = "MainMenuName";
            ddlmailpagefilter.DataValueField = "MainMenuId";
            ddlmailpagefilter.DataBind();
            ddlmailpagefilter.Items.Insert(0, "All");
            ddlmailpagefilter.Items[0].Value = "0";
            ddlsubpagefilter.Items.Insert(0, "All");
            ddlsubpagefilter.Items[0].Value = "0";
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
       
    }
    protected void Deletesubpage(Label lblmanu)
    {
       

    }
    protected void Deletepage(Label lblsubmanu)
    {
        
       

    }
    protected void Rlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Rlist.SelectedIndex == 1)
        {
            Fillmainfilter();
        }
        else if (Rlist.SelectedIndex == 2)
        {
            Fillmainfilter();
            fillsubfilter();
        }
        fillgridmenu();
    }
    protected void dpdrolename_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgridmenu();
    }
   
    protected void ddlmailpagefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillsubfilter();
        fillgridmenu();
    }
    protected void ddlsubpagefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgridmenu();
    }
    protected void ddlmainfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgridmenu();
    }
    protected void chkedit_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxEdit1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxEdit1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
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
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDelete1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
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
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDownload1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
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
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxInsert1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
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
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxUpdate1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
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
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxView1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
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
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxGo1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxGo1"));
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
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxSendMail1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxSendMail1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }

    }
    protected void rlheader_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (RadioButtonList)(rowitem.FindControl("RadioButtonList1"));
            chk.SelectedValue = ((RadioButtonList)sender).SelectedValue;
            fillaccessgrid();

        }
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (RadioButtonList)(rowitem.FindControl("RadioButtonList1"));
            chk.SelectedValue = ((RadioButtonList)sender).SelectedValue;
            fillaccessgrid1();
        }
        foreach (GridViewRow rowitem in grdpage.Rows)
        {
            chk = (RadioButtonList)(rowitem.FindControl("RadioButtonList1"));
            chk.SelectedValue = ((RadioButtonList)sender).SelectedValue;
            fillaccessgrid2();
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

    protected void grdmain_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((RadioButtonList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");

        RadioButtonList rdlist;

        rdlist = (RadioButtonList)(Grdsub.Rows[rinrow].FindControl("RadioButtonList1"));
        CheckBox Checksendmail_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxSendMail1");
        CheckBox CheckEdit_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxEdit1");
        CheckBox CheckDelete_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxDelete1");
        CheckBox CheckDownload_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxDownload1");
        CheckBox CheckInsert_Allows = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxInsert1");
        CheckBox CheckUpdate_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxUpdate1");
        CheckBox CheckView_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxView1");
        CheckBox CheckGo_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxGo1");

        CheckBox CheckSendMail_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxSendMail1");
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
    protected void RadioButtonList4_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((RadioButtonList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");

        RadioButtonList rdlist;

        rdlist = (RadioButtonList)(grdpage.Rows[rinrow].FindControl("RadioButtonList1"));
        CheckBox Checksendmail_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxSendMail1");
        CheckBox CheckEdit_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxEdit1");
        CheckBox CheckDelete_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxDelete1");
        CheckBox CheckDownload_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxDownload1");
        CheckBox CheckInsert_Allows = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxInsert1");
        CheckBox CheckUpdate_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxUpdate1");
        CheckBox CheckView_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxView1");
        CheckBox CheckGo_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxGo1");

        CheckBox CheckSendMail_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxSendMail1");
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
    protected void fillaccessgrid1()
    {



        foreach (GridViewRow dsc in Grdsub.Rows)
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
    protected void fillaccessgrid2()
    {



        foreach (GridViewRow dsc in grdpage.Rows)
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
    protected void grdmain_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgridmenu();
    }
    protected void Grdsub_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgridmenu();
    }
    protected void grdpage_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgridmenu();
    }
}
