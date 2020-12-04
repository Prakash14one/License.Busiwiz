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
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;

public partial class FunctionalityProfile : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    int viewid ;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblVersion.Text = "  ";
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {

                viewid = Convert.ToInt32(Request.QueryString["ID"]);
                ViewState["viewid"] = viewid;
                FillProduct();
                fromfunctionalitymaster();

            }
            else
            {
                FillProduct();
            }
        }
        
    }

    public void fromfunctionalitymaster()
    {
        string str1 = " select  distinct VersionInfoMaster.VersionInfoId from VersionInfoMaster INNER JOIN  FunctionalityMasterTbl ON VersionInfoMaster.VersionInfoId=FunctionalityMasterTbl.VersionID  where  FunctionalityMasterTbl.ID='" + ViewState["viewid"] + "' and VersionInfoMaster.Active='True'";
        SqlCommand cmdcln = new SqlCommand(str1, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlversion.SelectedValue = dtcln.Rows[0]["VersionInfoId"].ToString();
   
        functionality();

        ddlfuncti.SelectedValue = viewid.ToString();
        string functionality1 = "select FunctionalityMasterTbl.ID,FunctionalityTitle,FunctionalityDescription from FunctionalityMasterTbl  where VersionID='" + ddlversion.SelectedValue + "' and FunctionalityMasterTbl.ID='" + ViewState["viewid"] + "'";
        SqlCommand cmdcln1 = new SqlCommand(functionality1, con);
        DataTable dtcln1 = new DataTable();
        SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
        adpcln1.Fill(dtcln1);
        if (dtcln1.Rows.Count > 0)
        {
            txtfuncti.Text = dtcln1.Rows[0][1].ToString();
            txtfundesc.Text = dtcln1.Rows[0][2].ToString();
        }

        string functionali = "select FileTitle,FileName   from FunctinalityAttachmentTbl   inner join FunctionalityMasterTbl on FunctionalityMasterTbl.ID = FunctinalityAttachmentTbl.FunctionalityMasterTblID where VersionID='" + ddlversion.SelectedValue + "' and FunctionalityMasterTbl.ID='" + ViewState["viewid"] + "'";
            SqlCommand cmdc = new SqlCommand(functionali, con);
            DataTable dtc = new DataTable();
            SqlDataAdapter adpc = new SqlDataAdapter(cmdc);
            adpc.Fill(dtc);
            if (dtc.Rows.Count > 0)
            {
                GridView2.DataSource = dtc;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
        

        fillgrid();
    }

    protected void FillProduct()
    {


        string strcln = "SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,  ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName + ' : ' + WebsiteMaster.WebsiteName + ':' +  " +
                        " WebsiteSection.SectionName + ':' +   MasterPageMaster.MasterPageName  as productversion  " +
                        " FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId " +
                        " inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId " +
                       " inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId " +
                         " inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID " +
                     " inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId " +
                    " where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlversion.DataSource = dtcln;

        ddlversion.DataValueField = "VersionInfoId";
        ddlversion.DataTextField = "productversion";
        ddlversion.DataBind();
        ddlversion.Items.Insert(0, "-Select-");
        ddlversion.Items[0].Value = "0";

    }
    public void functionality()
    {
        string functionality = "select ID,FunctionalityTitle from FunctionalityMasterTbl where VersionID='" + ddlversion.SelectedValue + "'";
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
    protected void ddlversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        functionality();


    }
    protected void ddlfuncti_SelectedIndexChanged(object sender, EventArgs e)
    {
        string functionality = " select FunctionalityMasterTbl.ID,FunctionalityTitle,FunctionalityDescription from FunctionalityMasterTbl where VersionID='" + ddlversion.SelectedValue + "' and FunctionalityMasterTbl.ID='" + ddlfuncti.SelectedValue + "'";
        SqlCommand cmdcln = new SqlCommand(functionality, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        txtfuncti.Text = dtcln.Rows[0][1].ToString();
        txtfundesc.Text = dtcln.Rows[0][2].ToString();
        string functionali = "select FileTitle,FileName   from FunctinalityAttachmentTbl   inner join FunctionalityMasterTbl on FunctionalityMasterTbl.ID = FunctinalityAttachmentTbl.FunctionalityMasterTblID where VersionID='" + ddlversion.SelectedValue + "' and FunctionalityMasterTbl.ID='" + ddlfuncti.SelectedValue  +"'";
        SqlCommand cmdc = new SqlCommand(functionali, con);
        DataTable dtc = new DataTable();
        SqlDataAdapter adpc = new SqlDataAdapter(cmdc);
        adpc.Fill(dtc);
        if (dtc.Rows.Count > 0)
        {
            GridView2.DataSource = dtc;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
       
        fillgrid();

    }
    public void fillgrid()
    {

        string dd = " SELECT distinct  PageMaster.PageId,PageMaster.PageTitle , dbo.PageMaster.PageName,OrderNo from   PageMaster  inner join FunctionalityMasterTbl on FunctionalityMasterTbl.VersionID=PageMaster.VersionInfoMasterId " +
                    " inner join FunctionalityPageOrderTbl on FunctionalityPageOrderTbl.PagemasterID=PageMaster.PageId where FunctionalityPageOrderTbl.FunctionalityMasterTblID='" + ddlfuncti.SelectedValue + "' order by OrderNo ";

        dd = " SELECT distinct  PageMaster.PageId,PageMaster.PageTitle , dbo.PageMaster.PageName ,OrderNo from    dbo.PageMaster INNER JOIN dbo.FunctionalityPageOrderTbl ON dbo.FunctionalityPageOrderTbl.PagemasterID = dbo.PageMaster.PageId INNER JOIN dbo.FunctionalityMasterTbl ON dbo.FunctionalityPageOrderTbl.FunctionalityMasterTblID = dbo.FunctionalityMasterTbl.ID " +
                     " where FunctionalityMasterTbl.ID='" + ddlfuncti.SelectedValue + "' order by OrderNo ";         

        SqlDataAdapter da1 = new SqlDataAdapter(dd, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            DataTable dt_s = new DataTable();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string ff = " SELECT  PageMaster.PageName,PageVersionTbl.VersionNo+':'+ CAST(PageVersionTbl.SupervisorOkDate AS VARCHAR(20)) as VersionNo  FROM PageMaster  inner join PageVersionTbl on PageVersionTbl.PageMasterId=PageMaster.PageId where PageVersionTbl.SupervisorOk='1' and PageMaster.PageId ='" + dt1.Rows[i][0].ToString() + "' " +
                               " order by PageVersionTbl.VersionNo desc ";
                SqlDataAdapter da = new SqlDataAdapter(ff, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    if (dt_s.Rows.Count < 1)
                    {

                        dt_s.Columns.Add("PageId");
                        dt_s.Columns.Add("PageTitle");
                        dt_s.Columns.Add("PageName");
                        dt_s.Columns.Add("OrderNo");
                        dt_s.Columns.Add("VersionNo");
                    }
                    DataRow dr = dt_s.NewRow();
                    dr["PageId"] = dt1.Rows[i]["PageId"].ToString();
                    dr["PageTitle"] = dt1.Rows[i]["PageTitle"].ToString();
                    dr["PageName"] = dt.Rows[0]["PageName"].ToString();
                    dr["OrderNo"] = dt1.Rows[i]["OrderNo"].ToString();
                   
                    if (dt.Rows.Count > 0)
                    {
                        dr["VersionNo"] = dt.Rows[0]["VersionNo"].ToString();
                    }
                    else
                    {
                        dr["VersionNo"] = "";
                    }
                    dt_s.Rows.Add(dr);
                }
            }

            GridView1.DataSource = dt1;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }




    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View1")
        {
            int hh = Convert.ToInt32(e.CommandArgument);

            string te = "PageMasterNew.aspx?id=" + hh;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
}
   
