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
            FillProduct();             
            Fillpages();
            FilterProduct();
            Fillgrid();
            Fillterpages();
            ViewState["sortOrder"] = "";
        }


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

    protected void Fillpages()
    {

        ddlpagename.Items.Clear();
        if (ddlWebsiteSection.SelectedIndex > 1)
        {
            string strcln = "";
            strcln = "SELECT distinct MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName as Page_Name from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId   where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MainMenuMaster.MasterPage_Id='" + ddlWebsiteSection.SelectedValue + "' and  PageMaster.PageName !='' and MainMenuMaster.MainMenuName !='' ";
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

    protected void FilterProduct()
    {
        string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageName,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName + ':' + 'MASTER PAGE' + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name,MasterPageMaster.MasterPageId as MasterPageId   FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by MasterPage_Name";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        FilterProductname.DataSource = dtcln;
        FilterProductname.DataValueField = "MasterPageId";
        FilterProductname.DataTextField = "MasterPage_Name";
        FilterProductname.DataBind();

        FilterProductname.Items.Insert(0, "All");
        FilterProductname.Items[0].Value = "0";
       
    }
    protected void Fillterpages()
    {

        DdlFilterPage.Items.Clear();
        if (FilterProductname.SelectedIndex > 1)
        {
            string strcln = "";
            strcln = "SELECT distinct MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName as Page_Name from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId   where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MainMenuMaster.MasterPage_Id='" + FilterProductname.SelectedValue + "' and  PageMaster.PageName !='' and MainMenuMaster.MainMenuName !='' ";
            string orderby = "order by Page_Name";
            string finalstr = strcln + orderby;
            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            DdlFilterPage.DataSource = dtcln;
            DdlFilterPage.DataValueField = "PageId";
            DdlFilterPage.DataTextField = "Page_Name";
            DdlFilterPage.DataBind();
            DdlFilterPage.Items.Insert(0, "All");
            DdlFilterPage.Items[0].Value = "0";

        }
        else
        {
            DdlFilterPage.DataSource = null;
            DdlFilterPage.DataValueField = "PageId";
            DdlFilterPage.DataTextField = "Page_Name";
            DdlFilterPage.DataBind();
            DdlFilterPage.Items.Insert(0, "All");
            DdlFilterPage.Items[0].Value = "0";
        }
    }
    protected void ddlpagename_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string str1 = "select * from ReportMasterFunctionality where Pageid='" + ddlpagename.SelectedValue + "'  ";

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

            string SubMenuInsert = "Insert Into ReportMasterFunctionality (Pageid,MasterpageID) values ('" + ddlpagename.SelectedValue + "', '" + ddlWebsiteSection.SelectedValue + "')";
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

        if (FilterProductname.SelectedIndex > 0)
        {
            strproduct = " and dbo.ReportMasterFunctionality.MasterpageID='" + FilterProductname.SelectedValue + "' ";
        }
        if (DdlFilterPage.SelectedIndex > 0)
        {
            strmenu = " and  dbo.PageMaster.PageId='" + DdlFilterPage.SelectedValue + "' ";
        }

        str = " SELECT dbo.ReportMasterFunctionality.Id, dbo.ReportMasterFunctionality.Pageid, dbo.ReportMasterFunctionality.MasterpageID, dbo.PageMaster.PageName, dbo.PageMaster.PageTitle FROM dbo.ReportMasterFunctionality INNER JOIN dbo.PageMaster ON dbo.ReportMasterFunctionality.Pageid = dbo.PageMaster.PageId  where PageMaster.active='1' ";        

        string finalstr = str + strproduct + strmenu + status ;

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
        ddlpagename.SelectedIndex = 0;       
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
        string st2 = "Delete from ReportMasterFunctionality where Id='" + ViewState["Did"] + "' ";
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
        //GridView1.EditIndex = -1;
        //Fillgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {     

    }
    protected void ddlWebsiteSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillpages();
    }
   
    protected void FilterProduct_SelectedIndexChanged(object sender, EventArgs e)
    {       
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
            lbllegend.Text = "Edit Page For autogenerated reports ";
            lblmsg.Text = "";

            Button3.Visible = true;
            Button1.Visible = false;

            int mm = Convert.ToInt32(e.CommandArgument);
            ViewState["editid"] = mm;

            SqlDataAdapter dat = new SqlDataAdapter("select * from ReportMasterFunctionality Where id='" + ViewState["editid"] + "'", con);
            DataTable dtt = new DataTable();
            dat.Fill(dtt);
            
            FillProduct();
            ddlWebsiteSection.SelectedIndex = ddlWebsiteSection.Items.IndexOf(ddlWebsiteSection.Items.FindByValue(dtt.Rows[0]["MasterpageID"].ToString()));
            ddlWebsiteSection.SelectedValue = dtt.Rows[0]["MasterpageID"].ToString();
            Fillpages();
            ddlpagename.SelectedIndex = ddlpagename.Items.IndexOf(ddlpagename.Items.FindByValue(dtt.Rows[0]["Pageid"].ToString()));
            ddlpagename.SelectedValue = dtt.Rows[0]["Pageid"].ToString();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Button1.Visible = true;
        Button3.Visible = false;

        string str1 = "select * from ReportMasterFunctionality where Pageid='" + ddlpagename.SelectedValue + "'   ";

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


            string sr51 = (" update ReportMasterFunctionality set Pageid='" + ddlpagename.SelectedValue + "', MasterpageID='" + ddlWebsiteSection.SelectedValue + "' where id='" + ViewState["editid"].ToString()  + "' ");
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
        lbllegend.Text = "Add New pages For autogenerated reports";
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
