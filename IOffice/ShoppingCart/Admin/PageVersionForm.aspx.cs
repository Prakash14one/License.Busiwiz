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

public partial class SubMenuMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
       // Session["ClientId"] = "35";

        if (!IsPostBack)
        {

            ViewState["sortOrder"] = "";
            FillProduct();
            FillMainmenu();
            FillSubMenu();
            fillpage();

            filterproduct();
            FilterMainmenu();
            FillFilterSubMenu();
            fillfilterpage();
            Fillgrid();


        }

    }

    protected void FillProduct()
    {


        string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, MasterPageMaster.MasterPageId, ProductMaster.ProductName + '-' +   VersionInfoMaster.VersionInfoName  + ' - ' + WebsiteMaster.WebsiteName  + ' - ' +  WebsiteSection.SectionName   + ' - ' +  MasterPageMaster.MasterPageName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by productversion ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlWebsite.DataSource = dtcln;
        ddlWebsite.DataValueField = "MasterPageId";
        ddlWebsite.DataTextField = "productversion";
        ddlWebsite.DataBind();


    }

    protected void FillMainmenu()
    {
        ddlMainMenu.Items.Clear();

        if (ddlWebsite.SelectedIndex > -1)
        {
            string strcln = " SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + ddlWebsite.SelectedValue + "' ";
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
    protected void fillpage()
    {
        ddlPage.Items.Clear();
        if (ddlWebsite.SelectedIndex > -1)
        {

            string strcln = "";
            string str1 = "";
            string str2 = "";

            //strcln = "SELECT distinct MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName+'-'+SubMenuMaster.SubMenuName as Page_Name from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId   where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MasterPageMaster.MasterPageId='" + ddlWebsite.SelectedValue + "'   and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '')   ";
            strcln = "SELECT DISTINCT MMM.*, PM.PageId, PM.PageName + '-' + PM.PageTitle + '-' + MMM.MainMenuName " +
                    " /*+ '-' + SMM.SubMenuName */AS Page_Name FROM PageMaster AS PM " + 
                    "INNER  JOIN  MainMenuMaster AS MMM ON PM.MainMenuId = MMM.MainMenuId " + 
                    "LEFT OUTER JOIN SubMenuMaster As SMM ON SMM.SubMenuId = PM.SubMenuId " + 
                    "INNER JOIN MasterPageMaster AS MPM ON MPM.MasterPageId = MMM.MasterPage_Id " + 
                    "INNER JOIN WebsiteSection AS WS ON WS.WebsiteSectionId = MPM.WebsiteSectionId 	" + 
                    "INNER JOIN WebsiteMaster AS WM ON WM.ID = WS.WebsiteMasterId " + 
                    "INNER JOIN VersionInfoMaster AS VIM ON VIM.VersionInfoId = WM.VersionInfoId " + 
                    "INNER JOIN ProductMaster AS PrdM ON VIM.ProductId = PrdM.ProductId " + 
                    "WHERE PrdM.ClientMasterId = '" + Session["ClientId"].ToString() + "' AND MPM.MasterPageId = '" + ddlWebsite.SelectedValue + "' " + 
                    "AND (MMM.MainMenuName  <> '' /*AND SMM.SubMenuName  <> '' */AND PM.PageTitle  <> '')";
            if (ddlMainMenu.SelectedIndex > 0)
            {
                str1 = "  AND PM.MainMenuId='" + ddlMainMenu.SelectedValue + "' ";

            }

            if (ddlSubmenu.SelectedIndex > 0)
            {
                str2 = " AND PM.SubMenuId='" + ddlSubmenu.SelectedValue + "'";
            }

            string orderby = "ORDER BY Page_Name";

            string finalstr = strcln + str1 + str2 + orderby;

            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddlPage.DataSource = dtcln;

            ddlPage.DataValueField = "PageId";
            ddlPage.DataTextField = "Page_Name";
            ddlPage.DataBind();
            ddlPage.Items.Insert(0, "-Select-");
            ddlPage.Items[0].Value = "0";
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string str1 = "Select PageVersionTbl.PageMasterId from [PageVersionTbl] where [PageVersionTbl].PageMasterId='" + ddlPage.SelectedValue + "' and [PageVersionTbl].VersionName='" + txtVersionNo.Text + "'";

        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }

        else
        {
            string strlastversion = "select * from PageVersionTbl where PageMasterId='" + ddlPage.SelectedValue + "'  order by Id desc";

            SqlCommand cmdlastversion = new SqlCommand(strlastversion, con);
            SqlDataAdapter dalastversion = new SqlDataAdapter(cmdlastversion);
            DataTable dtlastversion = new DataTable();
            dalastversion.Fill(dtlastversion);

            if (dtlastversion.Rows.Count > 0)
            {
                

                bool a = Convert.ToBoolean(dtlastversion.Rows[0]["DeveloperOK"].ToString());
                bool b = Convert.ToBoolean(dtlastversion.Rows[0]["TesterOk"].ToString());
                bool c = Convert.ToBoolean(dtlastversion.Rows[0]["SupervisorOk"].ToString());

                if (a == true && b == true && c == true)
                {


                    string MasterInsert = "Insert Into PageVersionTbl(PageMasterId,VersionName,Date,VersionNo,PageName,Active,FolderName) values ('" + ddlPage.SelectedValue + "','" + txtVersionName.Text + "','" + System.DateTime.Now.Date + "','" + txtVersionNo.Text + "','" + txtPageName.Text + "','" + CheckBox1.Checked + "','')";
                    SqlCommand cmd = new SqlCommand(MasterInsert, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    clearall();
                    // Fillgrid();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully";

                    string strmax = " Select Max(Id) as Id from PageVersionTbl";
                    SqlCommand cmdmax = new SqlCommand(strmax, con);
                    DataTable dtmax = new DataTable();
                    SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
                    adpmax.Fill(dtmax);
                    string id = "";
                    if (dtmax.Rows.Count > 0)
                    {
                        id = dtmax.Rows[0]["Id"].ToString();
                    }

                    if (chkonsubmit.Checked == true)
                    {
                        string te = "PageWorkMaster.aspx?Id=" + id;
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                    }

                }
                else
                {

                    lblmsg.Visible = true;
                    lblmsg.Text = "Sorry, Please first certify the previous version.";

                }


            }
            else
            {

                string MasterInsert = "Insert Into PageVersionTbl(PageMasterId,VersionName,Date,VersionNo,PageName,Active,FolderName) values ('" + ddlPage.SelectedValue + "','" + txtVersionName.Text + "','" + System.DateTime.Now.Date + "','" + txtVersionNo.Text + "','" + txtPageName.Text + "','" + CheckBox1.Checked + "','')";
                SqlCommand cmd = new SqlCommand(MasterInsert, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                clearall();
                // Fillgrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";

                string strmax = " Select Max(Id) as Id from PageVersionTbl";
                SqlCommand cmdmax = new SqlCommand(strmax, con);
                DataTable dtmax = new DataTable();
                SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
                adpmax.Fill(dtmax);
                string id = "";
                if (dtmax.Rows.Count > 0)
                {
                    id = dtmax.Rows[0]["Id"].ToString();
                }

                if (chkonsubmit.Checked == true)
                {
                    string te = "PageWorkMaster.aspx?Id=" + id;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }


            }
        }


    }
    protected void Fillgrid()
    {
        string str1 = "";
        string str2 = "";
        string str3 = "";
        string str4 = "";
        string str5 = "";
        string strcln = "";
        string finalstr = "";
        string UNALLPAGE = "";

        strcln = "SELECT distinct PageVersionTbl.Id,PageVersionTbl.Active, SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId, PageMaster.PageId,PageMaster.FolderName,MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,PageMaster.Active,PageMaster.PageTitle, ProductMaster.ProductName,VersionInfoMaster.VersionInfoName, PageVersionTbl.PageName,PageVersionTbl.Date,PageVersionTbl.VersionNo,PageMaster.PageTitle,PageVersionTbl.VersionName,  WebsiteSection.SectionName + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join PageVersionTbl on PageMaster.PageId=PageVersionTbl.PageMasterId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId   where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "'  and ProductDetail.Active='1'     ";
        if (FilterProduct.SelectedIndex >= -1)
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
        if (ddlAct.SelectedIndex > 0)
        {
            str4 = "and PageMaster.Active='" + ddlAct.SelectedValue + "'";
        }
        if (ddlfilterpage.SelectedIndex > 0)
        {
            str5 = "and PageMaster.PageId='" + ddlfilterpage.SelectedValue + "'";
        }

        if (CHKUNALLPAGE.Checked == true)
        {
            UNALLPAGE = "and   PageVersionTbl.Active='True' AND ISNULL(PageVersionTbl.SupervisorOK, 0) = 0 AND ISNULL(PageVersionTbl.DeveloperOK, 0) = 0 AND ISNULL(PageVersionTbl.TesterOk, 0) = 0  ";   // PageMaster.PageId='" + ddlfilterpage.SelectedValue + "'  and
        }

        string orderby = " order by MainMenuMaster.MainMenuName,SubMenuMaster.SubMenuName,PageMaster.PageTitle,PageVersionTbl.PageName,PageVersionTbl.VersionNo ";

        finalstr = strcln + str1 + str2 + str3 + str4 + str5 + UNALLPAGE + orderby;

        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        DataView myDataView = new DataView();
        myDataView = ds.Tables[0].DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataSource = myDataView;
        GridView1.DataBind();

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
    protected void clearall()
    {

        Button1.Visible = true;
        btnUpdate.Visible = false;
        txtPageName.Text = "";
        txtVersionNo.Text = "";
        txtVersionName.Text = "";
        if (ddlPage.SelectedIndex >= -1)
        {
            ddlPage.SelectedIndex = -1;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        clearall();
        lbladdlabel.Text = "";
        lblmsg.Visible = false;
        lblmsg.Text = "";



    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from PageVersionTbl where Id='" + ViewState["DID"] + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        con.Open();
        cmd2.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        Fillgrid();
        lblmsg.Visible = true;
        lblmsg.Text = "Record deleted successfully ";

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Fillgrid();
    }

    protected void ddlWebsite_SelectedIndexChanged(object sender, EventArgs e)
    {


        FillMainmenu();
        FillSubMenu();
        fillpage();


    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Page Master.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            ViewState["DID"] = Convert.ToInt32(e.CommandArgument);
        }
        if (e.CommandName == "editview")
        {
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;
            lbladdlabel.Text = "Edit Page Version";
            lblmsg.Text = "";
            RequiredFieldValidator117.Visible = false;
            Label2.Visible = false;

            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            ViewState["sid"] = GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString();
            //string finalcompid23 = Session["Comid"].ToString();

            SqlCommand cmdedit = new SqlCommand("  SELECT    PageMaster.VersionInfoMasterId ,PageVersionTbl.*,MainMenuMaster.MainMenuId,MainMenuMaster.MasterPage_Id,SubMenuMaster.SubMenuId from  PageVersionTbl inner join [PageMaster] on [PageMaster].PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=[PageMaster].MainMenuId left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId  where PageVersionTbl.Id = '" + ViewState["sid"] + "'", con);


            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);
            if (dtedit.Rows.Count > 0)
            {
                // Controlenable(false);
                Button1.Visible = false;
                btnUpdate.Visible = true;
                //imgBtnCancelMainUpdate.Visible = false;
                //imgbtnedit.Visible = true;
                ddlWebsite.Visible = false;
                Label1.Visible = false;

                // and PageMaster.VersionInfoMasterId='" + dtedit.Rows[0]["VersionInfoMasterId"] + "'
                string strcln = "SELECT  distinct  VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, MasterPageMaster.MasterPageId,'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName  + ':' + 'MASTER PAGE' + ' : ' +  MasterPageMaster.MasterPageName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join PageMaster on PageMaster.VersionInfoMasterId=WebsiteMaster.VersionInfoId inner join PageVersionTbl on PageMaster.PageId=PageVersionTbl.PageMasterId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "'  and VersionInfoMaster.Active ='True' order  by productversion";
                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);
                ddlWebsite.DataSource = dtcln;

                ddlWebsite.DataValueField = "MasterPageId";
                ddlWebsite.DataTextField = "productversion";
                ddlWebsite.DataBind();
                ddlWebsite.Items.Insert(0, "-Select-");

                ddlWebsite.SelectedIndex = ddlWebsite.Items.IndexOf(ddlWebsite.Items.FindByValue(dtedit.Rows[0]["MasterPage_Id"].ToString()));

                string strcln22 = " SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MainMenuMaster.MasterPage_Id='" + ddlWebsite.SelectedValue + "' ";
                SqlCommand cmdcln22 = new SqlCommand(strcln22, con);
                DataTable dtcln22 = new DataTable();
                SqlDataAdapter adpcln22 = new SqlDataAdapter(cmdcln22);
                adpcln22.Fill(dtcln22);
                ddlMainMenu.DataSource = dtcln22;

                ddlMainMenu.DataValueField = "MainMenuId";
                ddlMainMenu.DataTextField = "Page_Name";
                ddlMainMenu.DataBind();
                ddlMainMenu.Items.Insert(0, "-Select-");

                ddlMainMenu.SelectedIndex = ddlMainMenu.Items.IndexOf(ddlMainMenu.Items.FindByValue(dtedit.Rows[0]["MainMenuId"].ToString()));


                string strcln33 = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MainMenuMaster.MasterPage_Id='" + ddlWebsite.SelectedValue + "' and SubMenuMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' ";
                SqlCommand cmdcln33 = new SqlCommand(strcln33, con);
                DataTable dtcln33 = new DataTable();
                SqlDataAdapter adpcln33 = new SqlDataAdapter(cmdcln33);
                adpcln33.Fill(dtcln33);
                ddlSubmenu.DataSource = dtcln33;

                ddlSubmenu.DataValueField = "SubMenuId";
                ddlSubmenu.DataTextField = "SubMenuName";
                ddlSubmenu.DataBind();
                ddlSubmenu.Items.Insert(0, "-Select-");

                ddlSubmenu.SelectedIndex = ddlSubmenu.Items.IndexOf(ddlSubmenu.Items.FindByValue(dtedit.Rows[0]["SubMenuId"].ToString()));

                string strcln44 = "";
                if (ddlMainMenu.SelectedIndex > 0 && ddlSubmenu.SelectedIndex > 0)
                {
                    //   "SELECT distinct MainMenuMaster.*,PageMaster.PageId,'MAIN MENU' + ' : '+ MainMenuMaster.MainMenuName + 'SUB MENU' + ' : '+ SubMenuMaster.SubMenuName + 'PAGE NAME' + ' : '+ PageMaster.PageTitle as Page_Name from MainMenuMaster inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join PageMaster on PageMaster.VersionInfoMasterId=WebsiteMaster.VersionInfoId
                    strcln44 = "    SELECT distinct MainMenuMaster.*,PageMaster.PageId,'MAIN MENU' + ' : '+ MainMenuMaster.MainMenuName + 'SUB MENU' + ' : '+ SubMenuMaster.SubMenuName + 'PAGE NAME' + ' : '+ PageMaster.PageTitle as Page_Name from  " +
                      " PageMaster inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id " +
                       " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId " +
                        " inner join WebsiteMaster " +
                       "  on PageMaster.VersionInfoMasterId=WebsiteMaster.VersionInfoId  inner join VersionInfoMaster " +
                       "   on VersionInfoMaster.VersionInfoId = PageMaster.VersionInfoMasterId inner join ProductMaster " +
                        "  on VersionInfoMaster.ProductId=ProductMaster.ProductId where MasterPageMaster.MasterPageId='" + ddlWebsite.SelectedValue + "' " +
                         "and PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' and PageMaster.SubMenuId='" + ddlSubmenu.SelectedValue + "' and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '') and ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' order by Page_Name ";


                }

                else if (ddlMainMenu.SelectedIndex > 0)
                {
                    strcln44 = "    SELECT distinct MainMenuMaster.*,PageMaster.PageId,'MAIN MENU' + ' : '+ MainMenuMaster.MainMenuName  + 'PAGE NAME' + ' : '+ PageMaster.PageTitle as Page_Name from  " +
               " PageMaster inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id " +
                " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId " +
                 " inner join WebsiteMaster " +
                "  on PageMaster.VersionInfoMasterId=WebsiteMaster.VersionInfoId  inner join VersionInfoMaster " +
                "   on VersionInfoMaster.VersionInfoId = PageMaster.VersionInfoMasterId inner join ProductMaster " +
                 "  on VersionInfoMaster.ProductId=ProductMaster.ProductId where MasterPageMaster.MasterPageId='" + ddlWebsite.SelectedValue + "' " +
                  "and PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '') and ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' order by Page_Name ";

                }


                SqlCommand cmdcln44 = new SqlCommand(strcln44, con);
                DataTable dtcln44 = new DataTable();
                SqlDataAdapter adpcln44 = new SqlDataAdapter(cmdcln44);
                adpcln44.Fill(dtcln44);
                ddlPage.DataSource = dtcln44;

                ddlPage.DataValueField = "PageId";
                ddlPage.DataTextField = "Page_Name";
                ddlPage.DataBind();
                ddlPage.Items.Insert(0, "-Select-");


                ddlPage.SelectedIndex = ddlPage.Items.IndexOf(ddlPage.Items.FindByValue(dtedit.Rows[0]["PageMasterId"].ToString()));
                // txtFolderName.Text = dtedit.Rows[0]["FolderName"].ToString();

                txtVersionName.Text = dtedit.Rows[0]["VersionName"].ToString();
                txtVersionNo.Text = dtedit.Rows[0]["VersionNo"].ToString();

                txtPageName.Text = dtedit.Rows[0]["PageName"].ToString();

                CheckBox1.Checked = Convert.ToBoolean(dtedit.Rows[0]["Active"].ToString());
                // PnlforNew.Visible = true;
            }

            GridView1.EditIndex = -1;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string str1 = "Select PageVersionTbl.PageMasterId from [PageVersionTbl] where [PageVersionTbl].PageMasterId='" + ddlPage.SelectedValue + "' and [PageVersionTbl].VersionName='" + txtVersionNo.Text + "' and Id<>'" + ViewState["sid"] + "'";

        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }

        else
        {

            string MasterInsert = "Update PageVersionTbl Set PageMasterId='" + ddlPage.SelectedValue + "',VersionName='" + txtVersionName.Text + "',VersionNo='" + txtVersionNo.Text + "',PageName='" + txtPageName.Text + "',Active='" + CheckBox1.Checked + "',Date='" + System.DateTime.Now.Date + "' where Id='" + ViewState["sid"] + "'";
            SqlCommand cmd = new SqlCommand(MasterInsert, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            clearall();
            Fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";

            ddlWebsite.Visible = true;
            Label1.Visible = true;
        }

        lbladdlabel.Text = "";
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
    }
    protected void ddlMainMenu_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillSubMenu();
        fillpage();
    }

    protected void FillSubMenu()
    {
        ddlSubmenu.Items.Clear();

        if (ddlMainMenu.SelectedIndex > 0)
        {
            string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + ddlWebsite.SelectedValue + "' and SubMenuMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "'";
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
    protected void ddlSubmenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpage();
    }
    protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "Select Top(1) PageVersionTbl.Id,PageVersionTbl.VersionNo,PageMaster.PageName from PageVersionTbl left join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where PageVersionTbl.PageMasterId='" + ddlPage.SelectedValue + "' Order by Id Desc";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {

            txtPageName.Text = dt.Rows[0]["PageName"].ToString().Replace(" ", "");
            string[] separator1 = new string[] { "." };
            string[] strSplitArr1 = txtPageName.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
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
            string[] strSplitAr = txtPageName.Text.Split(separ, StringSplitOptions.RemoveEmptyEntries);
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
                txtPageName.Text = strSplitArr1[0].ToString() + "Ver" + pver + "." + strSplitArr1[1].ToString();
            }
            txtVersionNo.Text = pver.ToString();
            txtVersionName.Text = "Version-" + pver;
            CheckBox1.Checked = true;
        }
    }
    protected void FilterProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterMainmenu();
        FillFilterSubMenu();
        fillfilterpage();
    }

    protected void FilterMenu_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillFilterSubMenu();
        fillfilterpage();
    }
    protected void BtnGo_Click(object sender, EventArgs e)
    {
        Fillgrid();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        Fillgrid();
    }
    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        string strcln = "";
        if (ddlWebsite.SelectedIndex > 0 && ddlMainMenu.SelectedIndex > 0 && ddlSubmenu.SelectedIndex > 0)
        {
            strcln = "    SELECT distinct MainMenuMaster.*,PageMaster.PageId,'MAIN MENU' + ' : '+ MainMenuMaster.MainMenuName + 'SUB MENU' + ' : '+ SubMenuMaster.SubMenuName + 'PAGE NAME' + ' : '+ PageMaster.PageTitle as Page_Name from  " +
             " PageMaster inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id " +
              " inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId " +
               " inner join WebsiteMaster " +
              "  on PageMaster.VersionInfoMasterId=WebsiteMaster.VersionInfoId  inner join VersionInfoMaster " +
              "   on VersionInfoMaster.VersionInfoId = PageMaster.VersionInfoMasterId inner join ProductMaster " +
               "  on VersionInfoMaster.ProductId=ProductMaster.ProductId where WebsiteMaster.VersionInfoId='" + ddlWebsite.SelectedValue + "' " +
                "and PageMaster.MainMenuId='" + ddlMainMenu.SelectedValue + "' and PageMaster.SubMenuId='" + ddlSubmenu.SelectedValue + "' and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '') and ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' order by Page_Name ";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddlPage.DataSource = dtcln;

            ddlPage.DataValueField = "PageId";
            ddlPage.DataTextField = "Page_Name";
            ddlPage.DataBind();
            ddlPage.Items.Insert(0, "-Select-");
        }

    }

    protected void Button4_Click(object sender, EventArgs e)
    {



        if (Button4.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
            if (GridView1.Columns[10].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[10].Visible = false;
            }

        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(100);

            Button4.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[10].Visible = true;
            }



        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        clearall();
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        lblmsg.Text = "";
        lbladdlabel.Text = "Add New Page Version";
        RequiredFieldValidator117.Visible = true;
        Label2.Visible = true;
    }
    protected void filterproduct()
    {
        if (CHKSHOWALL.Checked == true)
        {
            string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, MasterPageMaster.MasterPageId, ProductMaster.ProductName + '-' +   VersionInfoMaster.VersionInfoName  + ' - ' + WebsiteMaster.WebsiteName  + ' - ' +  WebsiteSection.SectionName   + ' - ' +  MasterPageMaster.MasterPageName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True'  order  by productversion ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            FilterProduct.DataSource = dtcln;
            FilterProduct.DataValueField = "MasterPageId";
            FilterProduct.DataTextField = "productversion";
            FilterProduct.DataBind();
        }
        else
        {
            string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, MasterPageMaster.MasterPageId, ProductMaster.ProductName + '-' +   VersionInfoMaster.VersionInfoName  + ' - ' + WebsiteMaster.WebsiteName  + ' - ' +  WebsiteSection.SectionName   + ' - ' +  MasterPageMaster.MasterPageName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by productversion ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            FilterProduct.DataSource = dtcln;
            FilterProduct.DataValueField = "MasterPageId";
            FilterProduct.DataTextField = "productversion";
            FilterProduct.DataBind();
        }

        

    }

    protected void FilterMainmenu()
    {
        if (CHKSHOWALL.Checked == true)
        {
            FilterMenu.Items.Clear();

            if (FilterProduct.SelectedIndex > -1)
            {
                string strcln = " SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "'";
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
            else
            {
                FilterMenu.DataSource = null;
                FilterMenu.DataValueField = "MainMenuId";
                FilterMenu.DataTextField = "Page_Name";
                FilterMenu.DataBind();

                FilterMenu.Items.Insert(0, "All");
                FilterMenu.Items[0].Value = "0";

            }
        }
        else
        {
            FilterMenu.Items.Clear();

            if (FilterProduct.SelectedIndex > -1)
            {
                string strcln = " SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "' AND MainMenuMaster.Active='1'  ";
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
            else
            {
                FilterMenu.DataSource = null;
                FilterMenu.DataValueField = "MainMenuId";
                FilterMenu.DataTextField = "Page_Name";
                FilterMenu.DataBind();

                FilterMenu.Items.Insert(0, "All");
                FilterMenu.Items[0].Value = "0";

            }
        }
       
    }
    protected void FillFilterSubMenu()
    {
        if (CHKSHOWALL.Checked == true)
        {
            FilterSubMenu.Items.Clear();

            if (FilterMenu.SelectedIndex > 0)
            {
                string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "' and SubMenuMaster.MainMenuId='" + FilterMenu.SelectedValue + "' ";
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
            else
            {

                FilterSubMenu.DataSource = null;
                FilterSubMenu.DataValueField = "SubMenuId";
                FilterSubMenu.DataTextField = "SubMenuName";
                FilterSubMenu.DataBind();
                FilterSubMenu.Items.Insert(0, "All");
                FilterSubMenu.Items[0].Value = "0";



            }
        }
        else
        {
            FilterSubMenu.Items.Clear();

            if (FilterMenu.SelectedIndex > 0)
            {
                string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "' and SubMenuMaster.MainMenuId='" + FilterMenu.SelectedValue + "' and SubMenuMaster.Active='1'";
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
            else
            {

                FilterSubMenu.DataSource = null;
                FilterSubMenu.DataValueField = "SubMenuId";
                FilterSubMenu.DataTextField = "SubMenuName";
                FilterSubMenu.DataBind();
                FilterSubMenu.Items.Insert(0, "All");
                FilterSubMenu.Items[0].Value = "0";



            }
        }
        


    }
    protected void fillfilterpage()
    {
        if (CHKSHOWALL.Checked == true)
        {
            ddlfilterpage.Items.Clear();
            if (FilterProduct.SelectedIndex > -1)
            {

                string strcln = "";
                string str1 = "";
                string str2 = "";

                strcln = "SELECT distinct MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName+'-'+SubMenuMaster.SubMenuName as Page_Name from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId   where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "'   and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '')   ";

                if (FilterMenu.SelectedIndex > 0)
                {
                    str1 = "  and PageMaster.MainMenuId='" + FilterMenu.SelectedValue + "' ";

                }

                if (FilterSubMenu.SelectedIndex > 0)
                {
                    str2 = " and PageMaster.SubMenuId='" + FilterSubMenu.SelectedValue + "'";
                }

                string orderby = "order by Page_Name";

                string finalstr = strcln + str1 + str2 + orderby;

                SqlCommand cmdcln = new SqlCommand(finalstr, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);

                ddlfilterpage.DataSource = dtcln;

                ddlfilterpage.DataValueField = "PageId";
                ddlfilterpage.DataTextField = "Page_Name";
                ddlfilterpage.DataBind();
                ddlfilterpage.Items.Insert(0, "All");
                ddlfilterpage.Items[0].Value = "0";
            }
        }
        else
        {
            ddlfilterpage.Items.Clear();
            if (FilterProduct.SelectedIndex > -1)
            {

                string strcln = "";
                string str1 = "";
                string str2 = "";

                strcln = "SELECT distinct MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName+'-'+SubMenuMaster.SubMenuName as Page_Name from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId   where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MasterPageMaster.MasterPageId='" + FilterProduct.SelectedValue + "'   and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '') and PageMaster.Active = '1'   ";

                if (FilterMenu.SelectedIndex > 0)
                {
                    str1 = "  and PageMaster.MainMenuId='" + FilterMenu.SelectedValue + "' ";

                }

                if (FilterSubMenu.SelectedIndex > 0)
                {
                    str2 = " and PageMaster.SubMenuId='" + FilterSubMenu.SelectedValue + "'";
                }

                string orderby = "order by Page_Name";

                string finalstr = strcln + str1 + str2 + orderby;

                SqlCommand cmdcln = new SqlCommand(finalstr, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);

                ddlfilterpage.DataSource = dtcln;

                ddlfilterpage.DataValueField = "PageId";
                ddlfilterpage.DataTextField = "Page_Name";
                ddlfilterpage.DataBind();
                ddlfilterpage.Items.Insert(0, "All");
                ddlfilterpage.Items[0].Value = "0";
            }
        }


        

    }
    protected void FilterSubMenu_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillfilterpage();
    }
    protected void CHKSHOWALL_CheckedChanged(object sender, EventArgs e)
    {
        filterproduct();
        FilterMainmenu();
        FillFilterSubMenu();
        fillfilterpage();
    }
    protected void CHKUNALLPAGE_CheckedChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
}
