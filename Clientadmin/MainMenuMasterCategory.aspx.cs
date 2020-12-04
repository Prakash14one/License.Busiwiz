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

public partial class MainMenuMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ClientId"] = 35;

            ViewState["sortOrder"] = "";
            FillProduct();
            fillMasterPage();
            fillLanguage();
            fillcount();

            FilterProduct();
            Fillgrid();

           

        }

    }

    protected void FillProduct()
    {

        string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId,  ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName + ' : ' + WebsiteMaster.WebsiteName + ':' +   WebsiteSection.SectionName  as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by productversion";


        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlWebsiteSection.DataSource = dtcln;

        ddlWebsiteSection.DataValueField = "WebsiteSectionId";
        ddlWebsiteSection.DataTextField = "productversion";
        ddlWebsiteSection.DataBind();



    }
    protected void fillLanguage()
    {
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


    protected void fillMasterPage()
    {
        if (ddlWebsiteSection.SelectedIndex > -1)
        {
            string str = "select * from MasterPageMaster Where WebsiteSectionId='" + ddlWebsiteSection.SelectedValue + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ddlMaster.DataSource = ds;
            ddlMaster.DataTextField = "MasterPageName";
            ddlMaster.DataValueField = "MasterPageId";
            ddlMaster.DataBind();

        }
        else
        {
            ddlMaster.DataSource = null;
            ddlMaster.DataTextField = "MasterPageName";
            ddlMaster.DataValueField = "MasterPageId";
            ddlMaster.DataBind();

        }
    }


    protected void FilterProduct()
    {
        string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageName,WebsiteSection.WebsiteSectionId,  ProductMaster.ProductName + ':'+   VersionInfoMaster.VersionInfoName + ' : '+  WebsiteMaster.WebsiteName + ':' +   WebsiteSection.SectionName + ':' +   MasterPageMaster.MasterPageName  as MasterPage_Name ,MasterPageMaster.MasterPageId FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by MasterPage_Name";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        FilterProductname.DataSource = dtcln;

        FilterProductname.DataValueField = "MasterPageId";
        FilterProductname.DataTextField = "MasterPage_Name";
        FilterProductname.DataBind();
        //FilterProductname.Items.Insert(0, "All");
        //FilterProductname.Items[0].Value = "0";

    }

    protected void Fillgrid()
    {
        string str = "";
        string stractive = "";
        if (FilterProductname.SelectedIndex > 0)
        {
            str = "select MainMenuMaster.*,MasterPageMaster.MasterPageName,LanguageMaster.Name,LanguageMaster.Id  from MainMenuMaster inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId  left outer join LanguageMaster on LanguageMaster.Id = MainMenuMaster.LanguageId where WebsiteMaster.VersionInfoId='" + FilterProductname.SelectedValue + "' and ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and ProductDetail.Active='1'  ";
            str = @"  SELECT dbo.MasterPageMaster.MasterPageName, dbo.LanguageMaster.Name, dbo.LanguageMaster.Id, dbo.Mainmenucategory.MainMenucatId, dbo.Mainmenucategory.MainMenuCatName, dbo.Mainmenucategory.BackColour, dbo.Mainmenucategory.MainMenuCatTitle, dbo.Mainmenucategory.MasterPage_Id, dbo.Mainmenucategory.MainMenuCatIndex, dbo.Mainmenucategory.Active, dbo.Mainmenucategory.LanguageId, dbo.Mainmenucategory.MainMenuCatDesc  FROM dbo.MasterPageMaster INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.Mainmenucategory ON dbo.MasterPageMaster.MasterPageId = dbo.Mainmenucategory.MasterPage_Id LEFT OUTER JOIN dbo.LanguageMaster ON dbo.Mainmenucategory.LanguageId = dbo.LanguageMaster.Id 
                    WHERE (dbo.ProductMaster.ClientMasterId = '" + Session["ClientId"] + "') AND (dbo.ProductDetail.Active = '1') AND (Mainmenucategory.MasterPage_Id = '" + FilterProductname.SelectedValue + "') ";
        }
        else
        {
            str = "select MainMenuMaster.*,MasterPageMaster.MasterPageName,LanguageMaster.Name,LanguageMaster.Id  from MainMenuMaster inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId  left outer join LanguageMaster on LanguageMaster.Id = MainMenuMaster.LanguageId  where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and ProductDetail.Active='1' ";
            str = @"  SELECT dbo.MasterPageMaster.MasterPageName, dbo.LanguageMaster.Name, dbo.LanguageMaster.Id, dbo.Mainmenucategory.MainMenucatId, dbo.Mainmenucategory.MainMenuCatName, dbo.Mainmenucategory.BackColour, dbo.Mainmenucategory.MainMenuCatTitle, dbo.Mainmenucategory.MasterPage_Id, dbo.Mainmenucategory.MainMenuCatIndex, dbo.Mainmenucategory.Active, dbo.Mainmenucategory.LanguageId, dbo.Mainmenucategory.MainMenuCatDesc  FROM dbo.MasterPageMaster INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.Mainmenucategory ON dbo.MasterPageMaster.MasterPageId = dbo.Mainmenucategory.MasterPage_Id LEFT OUTER JOIN dbo.LanguageMaster ON dbo.Mainmenucategory.LanguageId = dbo.LanguageMaster.Id 
                    where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and ProductDetail.Active='1' ";
        }
        if (ddlactivestatus.SelectedValue == "0")
        {

            stractive = "and Mainmenucategory.Active='0'";
        }
        if (ddlactivestatus.SelectedValue == "1")
        {
            stractive = "and Mainmenucategory.Active='1'";
        }

        lblproductname.Text = FilterProductname.SelectedItem.Text;
        lblstatus.Text = ddlactivestatus.SelectedItem.Text;

        string orderby = "order by Mainmenucategory.MainMenuCatIndex,Mainmenucategory.MainMenuCatTitle,MasterPageMaster.MasterPageName,LanguageMaster.Name";

        string finalstr = str + stractive + orderby;

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

  


    protected void Button1_Click(object sender, EventArgs e)
    {
        string str1 = "select * from [Mainmenucategory] where MainMenuCatName='" + txtMainMenuName.Text + "' and MasterPage_Id='" + ddlMaster.SelectedValue + "' ";

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
            txtMainMenuTitle.Text = txtMainMenuName.Text;

            string SubMenuInsert = "Insert Into Mainmenucategory (MainMenuCatName,BackColour,MainMenuCatTitle,MasterPage_Id,MainMenuCatIndex,Active,LanguageId, MainMenuCatDesc) values ('" + txtMainMenuName.Text + "','" + txtBackgroundColor.Text + "','" + txtMainMenuTitle.Text + "','" + ddlMaster.SelectedValue + "','" + txtMenuIndex.Text + "','" + CheckBox1.Checked + "','" + ddlanguage.SelectedValue + "','" + txt_desc.Text + "')";
            SqlCommand cmd = new SqlCommand(SubMenuInsert, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            clearall();
            Fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
        }
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lbllegend.Text = "";

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
       
        ddlWebsiteSection.SelectedIndex = 0;
        ddlMaster.SelectedIndex = 0;
        txtMainMenuName.Text = "";
        txtMainMenuTitle.Text = "";
        txtBackgroundColor.Text = "";
        lblmsg.Text = "";
        txtMenuIndex.Text = "";
        CheckBox1.Checked = false;
        Button1.Visible = true;
        Buttonupdate.Visible = false;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView1.EditIndex = e.NewEditIndex;
        //int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);

        //Fillgrid();

        //TextBox txtMainMenu = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtMainMenu");

        //DropDownList ddlMasterPageName = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlMasterPageName");
        //DropDownList ddlLanguageedit = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlLanguageedit");
        //Label lblMasterPageId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblMasterPageId");
        //Label lblLanguageId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblLanguageId");

        //string str = "SELECT  distinct MasterPageMaster.*, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName + ':' + 'Master Page' + ' : ' +  MasterPageMaster.MasterPageName  as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True'";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        //ddlMasterPageName.DataSource = ds;
        //ddlMasterPageName.DataTextField = "productversion";
        //ddlMasterPageName.DataValueField = "MasterPageId";
        //ddlMasterPageName.DataBind();

        //string strlan = "SELECT  Id, Name from LanguageMaster";
        //SqlCommand cmdlan = new SqlCommand(strlan, con);
        //SqlDataAdapter adplan = new SqlDataAdapter(cmdlan);
        //DataSet dslan = new DataSet();
        //adplan.Fill(dslan);
        //ddlLanguageedit.DataSource = dslan;
        //ddlLanguageedit.DataTextField = "Name";
        //ddlLanguageedit.DataValueField = "Id";
        //ddlLanguageedit.DataBind();
        //ddlLanguageedit.SelectedIndex = ddlLanguageedit.Items.IndexOf(ddlLanguageedit.Items.FindByValue(lblLanguageId.Text));
        //ddlMasterPageName.SelectedIndex = ddlMasterPageName.Items.IndexOf(ddlMasterPageName.Items.FindByValue(lblMasterPageId.Text));

        //TextBox txtBackColor = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtBackColor");

        //TextBox txtMainMenuTitle = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtMainMenuTitle");

        //TextBox lblMenuIndex = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("lblMenuIndex");

        //CheckBox chkActive = (CheckBox)GridView1.Rows[GridView1.EditIndex].FindControl("chkActive");

        //DropDownList ddlWebsection = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlWebsection");
        //Label lblWebsectionId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblWebsectionId");

        //string str2 = "select * from WebsiteSection ";
        //SqlCommand cmd2 = new SqlCommand(str2, con);
        //SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
        //DataSet ds2 = new DataSet();
        //adp.Fill(ds2);
        //ddlWebsection.DataSource = ds;
        //ddlWebsection.DataTextField = "SectionName";
        //ddlWebsection.DataValueField = "WebsiteSectionId";
        //ddlWebsection.DataBind();

        //ddlWebsection.SelectedIndex = ddlWebsection.Items.IndexOf(ddlWebsection.Items.FindByValue(lblWebsectionId.Text));
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);

        //TextBox txtMainMenu = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtMainMenu");

        ////DropDownList ddlMasterPageName = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlMasterPageName");
        ////Label lblMasterPageId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblMasterPageId");

        //TextBox txtBackColor = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtBackColor");

        //TextBox txtMainMenuTitle = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtMainMenuTitle");

        //TextBox lblMenuIndex = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("lblMenuIndex");

        //CheckBox chkActive = (CheckBox)GridView1.Rows[GridView1.EditIndex].FindControl("chkActive");

        //DropDownList ddlMasterPageName = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlMasterPageName");
        //Label lblMasterPageId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblMasterPageId");

        //DropDownList ddlLanguageedit = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlLanguageedit");
        //Label lblLanguageId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblLanguageId");

        //string str1 = "select * from MainMenuMaster where MainMenuName='" + txtMainMenu.Text + "'   and MainMenuId<>'" + dk + "'  ";

        //SqlCommand cmd1 = new SqlCommand(str1, con);
        //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);
        //if (dt1.Rows.Count > 0)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record Already Exist";
        //}

        //else
        //{


        //string sr51 = ("update MainMenuMaster set MainMenuName='" + txtMainMenu.Text + "',BackColour='" + txtBackColor.Text + "',MainMenuTitle='" + txtMainMenuTitle.Text + "',MasterPage_Id='" + ddlMasterPageName.SelectedValue + "',MainMenuIndex='" + lblMenuIndex.Text + "', LanguageId = '" + ddlLanguageedit.SelectedValue + "' ,Active='" + chkActive.Checked + "' where MainMenuId='" + dk + "' ");
        //SqlCommand cmd801 = new SqlCommand(sr51, con);

        //con.Open();
        //cmd801.ExecuteNonQuery();
        //con.Close();
        //GridView1.EditIndex = -1;
        //Fillgrid();
        //lblmsg.Visible = true;
        //lblmsg.Text = "Record updated successfully";

        // }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        clearall();
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lblmsg.Text = "";
        lbllegend.Text = "";
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GridView1.EditIndex = -1;
        //Fillgrid();
    }
    protected void ddlWebsiteSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMasterPage();
        fillcount();
    }
    protected void FilterProduct_SelectedIndexChanged(object sender, EventArgs e)
    {

        Fillgrid();
    }
    protected void FilterMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
    protected void ddlMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillLanguage();
        fillcount();

    }

    protected void fillcount()
    {
        SqlDataAdapter adp = new SqlDataAdapter("select Count(MainMenucatId) as MainMenucatId from Mainmenucategory where MasterPage_Id='" + ddlMaster.SelectedValue + "'", con);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            int a = Convert.ToInt32(ds.Rows[0]["MainMenucatId"].ToString()) + 1;
            txtMenuIndex.Text = a.ToString();
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int iikt = 0;
        if (e.CommandName == "Delete")
        {
                    int mm = Convert.ToInt32(e.CommandArgument);
                    string stpageall = "select *  from MainMenuMaster  where MainMenuId='" + mm + "'";
                    SqlCommand cmall = new SqlCommand(stpageall, con);
                    DataTable dtall = new DataTable();
                    SqlDataAdapter adpall = new SqlDataAdapter(cmall);
                    adpall.Fill(dtall);
                    if (dtall.Rows.Count > 0)
                    {
                        iikt = 1;
                    }
                    //string stm1 = "select *  from PageMaster  where MainMenuId='" + mm + "'";
                    //SqlCommand cmm1 = new SqlCommand(stm1, con);
                    //DataTable dtm1 = new DataTable();
                    //SqlDataAdapter adm1 = new SqlDataAdapter(cmm1);
                    //adm1.Fill(dtm1);
                    //if (dtm1.Rows.Count > 0)
                    //{
                    //    iikt = 1;
                    //}
                    if (iikt == 0)
                    {
                        string st2 = "Delete from Mainmenucategory where MainMenucatId=" + mm;
                        SqlCommand cmd2 = new SqlCommand(st2, con);
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        //GridView1.EditIndex = -1;
                        Fillgrid();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record deleted successfully ";
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
                    }
        }

        if (e.CommandName == "Edit")
        {
            pnladdnew.Visible = true;
            addnewpanel.Visible = false;
            lblmsg.Text = "";
            lbllegend.Text = "Edit Main Menu Category";

            Buttonupdate.Visible = true;
            Button1.Visible = false;

            int mm1 = Convert.ToInt32(e.CommandArgument);
            ViewState["editid"] = mm1.ToString();
            ViewState["MasterId"] = mm1.ToString();

            SqlDataAdapter adp = new SqlDataAdapter(@"SELECT        dbo.MasterPageMaster.MasterPageId, dbo.WebsiteSection.WebsiteSectionId, dbo.MasterPageMaster.MasterPageName, dbo.LanguageMaster.Name, dbo.LanguageMaster.Id, dbo.Mainmenucategory.MainMenucatId, dbo.Mainmenucategory.MainMenuCatName, dbo.Mainmenucategory.BackColour , dbo.Mainmenucategory.MainMenuCatTitle, dbo.Mainmenucategory.MasterPage_Id , dbo.Mainmenucategory.MainMenuCatIndex, dbo.Mainmenucategory.Active, dbo.Mainmenucategory.LanguageId , dbo.Mainmenucategory.MainMenuCatDesc
                                                        FROM dbo.MasterPageMaster INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.Mainmenucategory ON dbo.MasterPageMaster.MasterPageId = dbo.Mainmenucategory.MasterPage_Id LEFT OUTER JOIN dbo.LanguageMaster ON dbo.Mainmenucategory.LanguageId = dbo.LanguageMaster.Id 
                                                        where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and MainMenucatId='" + mm1 + "'", con);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            FillProduct();
            ddlWebsiteSection.SelectedIndex = ddlWebsiteSection.Items.IndexOf(ddlWebsiteSection.Items.FindByValue(ds.Rows[0]["WebsiteSectionId"].ToString()));
            fillMasterPage();
            ddlMaster.SelectedIndex = ddlMaster.Items.IndexOf(ddlMaster.Items.FindByValue(ds.Rows[0]["MasterPageId"].ToString()));
            fillLanguage();
            ddlanguage.SelectedIndex = ddlanguage.Items.IndexOf(ddlanguage.Items.FindByValue(ds.Rows[0]["Id"].ToString()));

            CheckBox1.Checked = Convert.ToBoolean(ds.Rows[0]["Active"]);
            txtMainMenuName.Text = ds.Rows[0]["MainMenuCatName"].ToString();
            txtBackgroundColor.Text = ds.Rows[0]["BackColour"].ToString();
            txtMainMenuTitle.Text = ds.Rows[0]["MainMenuCatTitle"].ToString();
            txtMenuIndex.Text = ds.Rows[0]["MainMenuCatIndex"].ToString();
        }
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[7].Visible = false;
            }
            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
        }
        else
        {



            Button3.Text = "Printable Version";
            Button4.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[8].Visible = true;
            }
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        Fillgrid();
    }
    protected void Buttonupdate_Click(object sender, EventArgs e)
    {
        string str1 = "select * from [Mainmenucategory] where MainMenuCatName='" + txtMainMenuName.Text + "' and MasterPage_Id='" + ddlMaster.SelectedValue + "' and MainMenucatId<>'" + ViewState["MasterId"].ToString() + "' ";

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
            Button1.Visible = true;
            Buttonupdate.Visible = false;

            txtMainMenuTitle.Text = txtMainMenuName.Text;

            string sr51 = ("update Mainmenucategory set MainMenuCatName='" + txtMainMenuName.Text + "',BackColour='" + txtBackgroundColor.Text + "',MainMenuCatTitle='" + txtMainMenuTitle.Text + "',MasterPage_Id='" + ddlMaster.SelectedValue + "',MainMenuCatIndex='" + txtMenuIndex.Text + "', LanguageId = '" + ddlanguage.SelectedValue + "' ,Active='" + CheckBox1.Checked + "' where MainMenucatId='" + ViewState["MasterId"].ToString() + "' ");
            SqlCommand cmd801 = new SqlCommand(sr51, con);

            con.Open();
            cmd801.ExecuteNonQuery();
            con.Close();
            //GridView1.EditIndex = -1;
            Fillgrid();
            clearall();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";

            addnewpanel.Visible = true;
            pnladdnew.Visible = false;
            lbllegend.Text = "";
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        lbllegend.Text = "Add New Main Menu";
        lblmsg.Text = "";
    }
    protected void ddlactivestatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void btndosyncro_Clickpop(object sender, EventArgs e)
    {
        ModernpopSync.Show();
    }
    protected void btndosyncro_Click(object sender, EventArgs e)
    {
        int transf = 0;


        DataTable dt1 = select("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='Mainmenucategory' ");
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
                    DataTable dtcln = select("SELECT Distinct ServerMasterTbl.Id FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId    where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' ");

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

}
