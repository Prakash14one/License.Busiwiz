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
        if (!IsPostBack)
        {
            Session["ClientId"] = 35;
              
            FillProduct();
            mainmenu();
            fillLanguage();
            submenucount();
            FilterProduct();
            FillMainmenu();
            
            Fillgrid();
          


            ViewState["sortOrder"] = "";
        }


    }

    protected void FilterProduct()
    {


        string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageName,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName + ':' + 'MASTER PAGE' + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by MasterPage_Name";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        FilterProductname.DataSource = dtcln;
        FilterProductname.DataValueField = "VersionInfoId";
        FilterProductname.DataTextField = "MasterPage_Name";
        FilterProductname.DataBind();

        FilterProductname.Items.Insert(0, "All");
        FilterProductname.Items[0].Value = "0";
        FilterProductname.SelectedIndex = 1;


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
    protected void FillMainmenu()
    {

        if (FilterProductname.SelectedIndex > -1)
        {
            string strcln = " SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where WebsiteMaster.VersionInfoId='" + FilterProductname.SelectedValue + "' order by MainMenuMaster.MainMenuTitle ";
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
            FilterMenu.DataTextField = "MainMenuTitle";
            FilterMenu.DataBind();
            FilterMenu.Items.Insert(0, "All");
            FilterMenu.Items[0].Value = "0";



        }
    }
    protected void mainmenu()
    {
        string str = "select * from MainMenuMaster where MasterPage_Id='" + ddlWebsiteSection.SelectedValue + "' and Active='1' order by MainMenuTitle  ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlmainmenu.DataSource = ds;
        ddlmainmenu.DataTextField = "MainMenuTitle";
        ddlmainmenu.DataValueField = "MainMenuId";
        ddlmainmenu.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        string str1 = "select * from SubMenuMaster where MainMenuId='" + ddlmainmenu.SelectedValue + "'  and SubMenuName='" + txtsubmenu.Text + "'   ";

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

            string SubMenuInsert = "Insert Into SubMenuMaster (MainMenuId,SubMenuName,SubMenuIndex,Active,LanguageId) values ('" + ddlmainmenu.SelectedValue + "','" + txtsubmenu.Text + "','" + txtmenuindex.Text + "','" + CheckBox1.Checked + "','" + ddlanguage.SelectedValue + "')";
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
        clearall();
    }
    protected void Fillgrid()
    {
        string str = "";
        string strproduct = "";
        string strmenu = "";
        string status = "";



        str = "select SubMenuMaster.SubMenuId,SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuIndex ,SubMenuMaster.Active as SubmenuActive,LanguageMaster.Id,LanguageMaster.Name, MainMenuMaster.* from SubMenuMaster inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId left outer join LanguageMaster on LanguageMaster.Id = SubMenuMaster.LanguageId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True'";

        if (FilterProductname.SelectedIndex > 0)
        {
            strproduct = "and WebsiteMaster.VersionInfoId='" + FilterProductname.SelectedValue + "' ";
        }
        if (FilterMenu.SelectedIndex > 0)
        {
            strmenu = "  and MainMenuMaster.MainMenuId='" + FilterMenu.SelectedValue + "' ";
        }
        if (ddlstatus.SelectedValue == "0")
        {
            status = " and SubMenuMaster.Active='0'";
        }
        if (ddlstatus.SelectedValue == "")
        {
            status = " and SubMenuMaster.Active='1'";
        }

        string orderby = "order by MainMenuMaster.MainMenuIndex,SubMenuMaster.SubMenuIndex,MainMenuMaster.MainMenuName,SubMenuMaster.SubMenuName  ";

        string finalstr = str + strproduct + strmenu + status + orderby;

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
        ddlmainmenu.SelectedIndex = 0;
        ddlanguage.SelectedIndex = 0;
        txtsubmenu.Text = "";
        txtmenuindex.Text = "";
        CheckBox1.Checked = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        clearall();
        lblmsg.Visible = false;
        lblmsg.Text = "";
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lbllegend.Text = "";
        Button1.Visible = true;
        Button3.Visible = false;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
                  string stpageall = "select *  from PageMaster  where SubMenuId='" + ViewState["Did"] + "'";

                    SqlCommand cmall = new SqlCommand(stpageall, con);
                    DataTable dtall = new DataTable();
                    SqlDataAdapter adpall = new SqlDataAdapter(cmall);
                    adpall.Fill(dtall);
                    if (dtall.Rows.Count == 0)
                    {
                        string st2 = "Delete from SubMenuMaster where SubMenuId='" + ViewState["Did"] + "' ";
                        SqlCommand cmd2 = new SqlCommand(st2, con);
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        GridView1.EditIndex = -1;
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
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GridView1.EditIndex = -1;
        //Fillgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView1.EditIndex = e.NewEditIndex;
        //int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);

        //Fillgrid();

        //DropDownList ddlmainmenu123 = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlmainmenu123");
        //DropDownList ddlLanguageedit = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlLanguageedit");
        //Label lblddlmainmenuId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblddlmainmenuId");
        //Label lblLanguageId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblLanguageId");

        //string str = " select SubMenuMaster.*,  'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName + ' : ' +'MASTER PAGE' + ' : ' + MasterPageMaster.MasterPageName + ':' + 'MENU' + ' : ' +  MainMenuMaster.MainMenuTitle as productversion,MainMenuMaster.MainMenuId  from SubMenuMaster inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId  inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and MainMenuMaster.Active='1'";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        //ddlmainmenu123.DataSource = ds;
        //ddlmainmenu123.DataTextField = "productversion";
        //ddlmainmenu123.DataValueField = "MainMenuId";
        //ddlmainmenu123.DataBind();
        //ddlmainmenu123.SelectedIndex = ddlmainmenu123.Items.IndexOf(ddlmainmenu123.Items.FindByValue(lblddlmainmenuId.Text));

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



    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);

        //DropDownList ddlmainmenu123 = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlmainmenu123");
        //DropDownList ddlLanguageedit = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlLanguageedit");
        //TextBox txtsubmenu123 = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtsubmenu123");
        //CheckBox chkgrd123456 = (CheckBox)GridView1.Rows[GridView1.EditIndex].FindControl("chkgrd123456");
        //TextBox txtsubmenuindex123 = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtsubmenuindex123");


        //string str1 = "select * from SubMenuMaster where MainMenuId='" + ddlmainmenu123.SelectedValue + "'  and SubMenuName='" + txtsubmenu123.Text + "' and SubMenuId<>'" + dk + "'  ";

        //SqlCommand cmd1 = new SqlCommand(str1, con);
        //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);
        //if (dt1.Rows.Count > 0)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record already exists";
        //}

        //else
        //{


        //    string sr51 = ("update SubMenuMaster set MainMenuId='" + ddlmainmenu123.SelectedValue + "', LanguageId='" + ddlLanguageedit.SelectedValue + "' ,SubMenuName='" + txtsubmenu123.Text + "',SubMenuIndex='" + txtsubmenuindex123.Text + "',Active='" + chkgrd123456.Checked + "'  where SubMenuId='" + dk + "' ");
        //    SqlCommand cmd801 = new SqlCommand(sr51, con);

        //    con.Open();
        //    cmd801.ExecuteNonQuery();
        //    con.Close();
        //    GridView1.EditIndex = -1;
        //    Fillgrid();
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record updated successfully";

        //}


    }
    protected void ddlWebsiteSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        mainmenu();
        submenucount();
    }
    protected void FillProduct()
    {



        string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName + ':' + 'MASTER PAGE' + ' : ' + MasterPageMaster.MasterPageName as productversion ,MasterPageMaster.MasterPageId as MasterPageId FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID  inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True'  and ProductDetail.Active='1' order  by productversion ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlWebsiteSection.DataSource = dtcln;
        ddlWebsiteSection.DataValueField = "MasterPageId";
        ddlWebsiteSection.DataTextField = "productversion";
        ddlWebsiteSection.DataBind();





    }
    protected void FilterProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMainmenu();
        Fillgrid();

    }
    protected void FilterMenu_SelectedIndexChanged(object sender, EventArgs e)
    {

        Fillgrid();
    }
    protected void ddlmainmenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        submenucount();
    }
    protected void submenucount()
    {
        SqlDataAdapter adp = new SqlDataAdapter("select Count(SubMenuId) as SubMenuId from SubMenuMaster where MainMenuId='" + ddlmainmenu.SelectedValue + "'", con);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            int a = Convert.ToInt32(ds.Rows[0]["SubMenuId"].ToString()) + 1;
            txtmenuindex.Text = a.ToString();
        }

    }
    protected void btnprint_Click(object sender, EventArgs e)
    {



        if (btnprint.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            btnprint.Text = "Hide Printable Version";
            btnin.Visible = true;
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }

        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(100);

            btnprint.Text = "Printable Version";
            btnin.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }




        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            ViewState["Did"] = Convert.ToInt32(e.CommandArgument);
        }

        if (e.CommandName == "Edit")
        {
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;
            lbllegend.Text = "Edit SubMenu";
            lblmsg.Text = "";

            Button3.Visible = true;
            Button1.Visible = false;

            int mm = Convert.ToInt32(e.CommandArgument);
            ViewState["editid"] = mm;

            SqlDataAdapter dat = new SqlDataAdapter("select SubMenuMaster.SubMenuId,MasterPageMaster.MasterPageId,SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuIndex ,SubMenuMaster.Active as SubmenuActive,LanguageMaster.Id,LanguageMaster.Name, MainMenuMaster.* from SubMenuMaster inner join MainMenuMaster on MainMenuMaster.MainMenuId=SubMenuMaster.MainMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId left outer join LanguageMaster on LanguageMaster.Id = SubMenuMaster.LanguageId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and SubMenuId='" + ViewState["editid"] + "'", con);
            DataTable dtt = new DataTable();
            dat.Fill(dtt);

            txtsubmenu.Text = dtt.Rows[0]["SubMenuName"].ToString();
            txtmenuindex.Text = dtt.Rows[0]["SubMenuIndex"].ToString();
            FillProduct();
            ddlWebsiteSection.SelectedIndex = ddlWebsiteSection.Items.IndexOf(ddlWebsiteSection.Items.FindByValue(dtt.Rows[0]["MasterPageId"].ToString()));
            fillLanguage();
            ddlanguage.SelectedIndex = ddlanguage.Items.IndexOf(ddlanguage.Items.FindByValue(dtt.Rows[0]["SubMenuIndex"].ToString()));

            CheckBox1.Checked = Convert.ToBoolean(dtt.Rows[0]["Active"]);

            mainmenu();
            ddlmainmenu.SelectedIndex = ddlmainmenu.Items.IndexOf(ddlmainmenu.Items.FindByValue(dtt.Rows[0]["MainMenuId"].ToString()));

           
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Button1.Visible = true;
        Button3.Visible = false;

        string str1 = "select * from SubMenuMaster where MainMenuId='" + ddlmainmenu.SelectedValue + "'  and SubMenuName='" + txtsubmenu.Text + "' and SubMenuId<>'" + ViewState["editid"] + "'  ";

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


            string sr51 = ("update SubMenuMaster set MainMenuId='" + ddlmainmenu.SelectedValue + "', LanguageId='" + ddlanguage.SelectedValue + "' ,SubMenuName='" + txtsubmenu.Text + "',SubMenuIndex='" + txtmenuindex.Text + "',Active='" + CheckBox1.Checked + "'  where SubMenuId='" + ViewState["editid"] + "' ");
            SqlCommand cmd801 = new SqlCommand(sr51, con);

            con.Open();
            cmd801.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            Fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";

        }
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        clearall();
        lbllegend.Text = "";
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        lblmsg.Text = "";
        lbllegend.Text = "Add New SubMenu";
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        Fillgrid();
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
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


        DataTable dt1 = select("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='SubMenuMaster' ");
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

                        string str223 = " Insert Into SateliteServerRequiringSynchronisationMasterTbl(SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + dt121.Rows[0]["ID"] + "','" + dtcln.Rows[j]["Id"] + "','0','" + DateTime.Now.ToString() + "')";
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
