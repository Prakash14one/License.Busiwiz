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
    protected void Page_Load(object sender, EventArgs e)
    {
        lblVersion.Text = " 12/12/15 Version 2 priya";
        if (!IsPostBack)
        {
            FillProduct();
        }
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

        string str = "";
        if(ddlfunctionalitycategory.SelectedIndex>0)
        {
            str += " and Functionalitycategory='" + ddlfunctionalitycategory.SelectedValue + "'";
        }

        string functionality = "select ID,FunctionalityTitle from FunctionalityMasterTbl where VersionID='" + ddlversion.SelectedValue + "' "+str+" ";
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
    public void functionalitycategory()
    {
        string strcln = @"select functionalitycategorymaster.Id,functionalitycategorymaster.functionalitycategory from functionalitycategorymaster
                           inner join WebsiteMaster on WebsiteMaster.ID = functionalitycategorymaster.productid
                           INNER JOIN VersionInfoMaster on WebsiteMaster.VersionInfoId = VersionInfoMaster.VersionInfoId
                           inner join WebsiteSection on WebsiteSection.WebsiteMasterId = WebsiteMaster.ID
                            INNER JOIN ProductMaster ON ProductMaster.ProductId = VersionInfoMaster.ProductId
                            where VersionInfoMaster.VersionInfoId='" + ddlversion.SelectedValue + "'";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            ddlfunctionalitycategory.DataSource = dtcln;
            ddlfunctionalitycategory.DataValueField = "Id";
            ddlfunctionalitycategory.DataTextField = "functionalitycategory";
            ddlfunctionalitycategory.DataBind();
        }

        ddlfunctionalitycategory.Items.Insert(0, "-Select-");
        ddlfunctionalitycategory.Items[0].Value = "0";
    }
    protected void ddlversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        functionality();
        functionalitycategory();
    }
    protected void ddlfuncti_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button3.Visible = true;
        string functionality = " select FunctionalityMasterTbl.ID,FunctionalityTitle,FunctionalityDescription from FunctionalityMasterTbl where VersionID='" + ddlversion.SelectedValue + "' and FunctionalityMasterTbl.ID='" + ddlfuncti.SelectedValue + "'";
        SqlCommand cmdcln = new SqlCommand(functionality, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            txtfuncti.Text = dtcln.Rows[0][1].ToString();
            txtfundesc.Text = dtcln.Rows[0][2].ToString();
            Button2.Visible = true;
            string functionali = "select FileTitle,FileName   from FunctinalityAttachmentTbl   inner join FunctionalityMasterTbl on FunctionalityMasterTbl.ID = FunctinalityAttachmentTbl.FunctionalityMasterTblID where VersionID='" + ddlversion.SelectedValue + "' and FunctionalityMasterTbl.ID='" + ddlfuncti.SelectedValue + "'";
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
        }

        fillgrid();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Change Rank")
        {
            foreach (GridViewRow ggg in GridView1.Rows)
            {
                TextBox res = (TextBox)ggg.FindControl("TextBox3");
                res.Enabled = true;
            }
            Button2.Text="Update";
        }
        else if (Button2.Text == "Update")
        {

            if (ViewState["rankduplicate"] == "")
            {
                foreach (GridViewRow ggg in GridView1.Rows)
                {
                    TextBox res = (TextBox)ggg.FindControl("TextBox3");
                    Label lbl = (Label)ggg.FindControl("Label22");

                    string update = "update FunctionalityPageOrderTbl set OrderNo='" + res.Text + "' where PagemasterID='" + lbl.Text + "' and FunctionalityMasterTblID='" + ddlfuncti.SelectedValue + "'";
                    SqlCommand cmd = new SqlCommand(update, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Enabled = false;

                }
                Button2.Text = "Change Rank";
                fillgrid();
            }
            else
            {
                lblmsg.Text = "Duplicate values are not permitted!! ";
            }

            

            }
        }
    
    public void fillgrid()
    {

        string dd = " SELECT  distinct  PageMaster.PageId,PageMaster.PageTitle,OrderNo,PageMaster.PageName from   PageMaster  inner join FunctionalityMasterTbl on FunctionalityMasterTbl.VersionID=PageMaster.VersionInfoMasterId inner join FunctionalityPageOrderTbl on FunctionalityPageOrderTbl.PagemasterID=PageMaster.PageId "
                    +" where FunctionalityPageOrderTbl.FunctionalityMasterTblID='" + ddlfuncti.SelectedValue + "' order by OrderNo ";
        dd = " SELECT  distinct  PageMaster.PageId,PageMaster.PageTitle,OrderNo,PageMaster.PageName from   dbo.PageMaster INNER JOIN dbo.FunctionalityPageOrderTbl ON dbo.FunctionalityPageOrderTbl.PagemasterID = dbo.PageMaster.PageId INNER JOIN dbo.FunctionalityMasterTbl ON dbo.FunctionalityPageOrderTbl.FunctionalityMasterTblID = dbo.FunctionalityMasterTbl.ID "
                    +" where FunctionalityPageOrderTbl.FunctionalityMasterTblID='" + ddlfuncti.SelectedValue + "' order by OrderNo ";
         
        SqlDataAdapter da1 = new SqlDataAdapter(dd, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            DataTable dt_s = new DataTable();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string ff = " SELECT  PageMaster.PageName,PageVersionTbl.VersionNo+':'+ CAST(PageVersionTbl.SupervisorOkDate AS VARCHAR(20)) as VersionNo FROM PageMaster  inner join PageVersionTbl on PageVersionTbl.PageMasterId=PageMaster.PageId where PageVersionTbl.SupervisorOk='1' and  PageMaster.PageId ='" + dt1.Rows[i][0].ToString() + "' " +
                               " order by PageVersionTbl.VersionNo desc ";
                SqlDataAdapter da = new SqlDataAdapter(ff, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
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
                dr["PageName"] = dt1.Rows[i]["PageName"].ToString();
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

            GridView1.DataSource = dt_s;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        lblmsg.Text = " ";
        ViewState["rankduplicate"] = "";
        TextBox lnkbtn = (TextBox)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        TextBox rank = (TextBox)GridView1.Rows[j].FindControl("TextBox3");
        int count3 = Convert.ToInt16(rank.Text);
        int count1 = GridView1.Rows.Count + 1;
        if (count3 <= count1)
        {
            foreach (GridViewRow ggg in GridView1.Rows)
            {
                int count = ggg.RowIndex;

                TextBox res = (TextBox)ggg.FindControl("TextBox3");

                if (j != count)
                {

                    if (res.Text == rank.Text)
                    {
                        lblmsg.Text = "Duplicate values are not permitted!! ";
                        ViewState["rankduplicate"] = 1;
                    }
                    else
                    {
                        
                    }
                }
            }
        }
        else
        {
            ViewState["rankduplicate"] = 1;
            lblmsg.Text = "Please Enter the correct order from the list";
        }
            
        
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label pageid = (Label)GridView1.Rows[j].FindControl("Label22");

        string update = "delete  from FunctionalityPageOrderTbl where PagemasterID='" + pageid.Text + "' ";
        SqlCommand cmd = new SqlCommand(update, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        fillgrid();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) && (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header))
        {
            e.Row.Cells[0].Visible = false;
           
        }
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
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label title = (Label)GridView2.Rows[j].FindControl("Label23");
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('http://license.busiwiz.com//Uploads/" + title.Text + "');", true);


    }
    protected void ddlfunctionalitycategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        functionality();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        FillterCategorysearch();
        fillpagegrid();
        filtermainmenu();
        ModalPopupExtender4.Show();

    }
    protected void FillterCategorysearch()
    {

        string strlan = @"select *  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId 
                            inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId 
                            inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId 
                            inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID 
                            inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId 
                            inner join Mainmenucategory on Mainmenucategory.MasterPage_Id=MasterPageMaster.MasterPageId where VersionInfoMaster.VersionInfoId='" + ddlversion.SelectedValue + "'";
        
          

        SqlCommand cmdlan = new SqlCommand(strlan, con);
        SqlDataAdapter adplan = new SqlDataAdapter(cmdlan);
        DataSet dslan = new DataSet();
        adplan.Fill(dslan);
        DropDownList1.DataSource = dslan;
        DropDownList1.DataTextField = "MainMenuCatName";
        DropDownList1.DataValueField = "MainMenucatId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";
    }
    protected void filtermainmenu()
    {
        string filter = "";
        if (DropDownList1.SelectedIndex > 0)
        {
            filter = " where MainMenuMaster.MainMenucatId='" + DropDownList1.SelectedValue + "' ";
        }
        string strcln = " SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  " + filter + " order By MainMenuMaster.MainMenuTitle ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        DropDownList2.DataSource = dtcln;

        DropDownList2.DataValueField = "MainMenuId";
        DropDownList2.DataTextField = "Page_Name";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "All");
        DropDownList2.Items[0].Value = "0";

    }

    protected void filtersubmenu()
    {



        string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster where SubMenuMaster.MainMenuId='" + DropDownList2.SelectedValue + "'  Order By SubMenuMaster.SubMenuName ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DropDownList3.DataSource = dtcln;

        DropDownList3.DataValueField = "SubMenuId";
        DropDownList3.DataTextField = "SubMenuName";
        DropDownList3.DataBind();

        DropDownList3.Items.Insert(0, "All");
        DropDownList3.Items[0].Value = "0";


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
    public void fillpagegrid()
    {
        string str = "";
        if (TextBox6.Text != null)
        {
            str = " and (PageMaster.PageName Like '%" + TextBox6.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox6.Text + "%' )";
        }

        string te = "select distinct SubMenuMaster.SubMenuName,SubMenuMaster.SubMenuId, PageMaster.PageId,PageMaster.FolderName,LanguageMaster.Name,LanguageMaster.Id,MainMenuMaster.MainMenuName,MainMenuMaster.MainMenuId,PageMaster.Active, ProductMaster.ProductName,VersionInfoMaster.VersionInfoName, PageMaster.PageName,PageMaster.PageTitle,  WebsiteSection.SectionName + ' : ' +  MasterPageMaster.MasterPageName  as MasterPage_Name from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId left outer join SubMenuMaster on PageMaster.SubMenuId=SubMenuMaster.SubMenuId left outer join LanguageMaster on LanguageMaster.Id = PageMaster.LanguageId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and  VersionInfoMaster.VersionInfoId='"+ddlversion.SelectedValue+"' "+str+"";
        SqlDataAdapter da = new SqlDataAdapter(te, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView3.DataSource = dt;
            GridView3.DataBind();
        }


    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        filtermainmenu();
        ModalPopupExtender4.Show();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        filtersubmenu();
        ModalPopupExtender4.Show();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string filtersub = "";
        if (DropDownList3.SelectedIndex > 0)
        {
            filtersub = " and SubMenuId='" + DropDownList3.SelectedValue + "'";
        }
        string stpageall = "select *  from PageMaster  where PageName='" + TextBox5.Text + "' and MainMenuId='" + DropDownList2.SelectedValue + "' " + filtersub;

        SqlCommand cmall = new SqlCommand(stpageall, con);
        DataTable dtall = new DataTable();
        SqlDataAdapter adpall = new SqlDataAdapter(cmall);
        adpall.Fill(dtall);
        if (dtall.Rows.Count == 0)
        {
            if (DropDownList3.SelectedIndex > 0)
            {
                string str = "INSERT INTO PageMaster(PageName,PageTitle,PageDescription,PageIndex,VersionInfoMasterId,MainMenuId,FolderName,Active,SubMenuId,LanguageId,ManuAccess,labelimage) " +
                           "VALUES('" + TextBox5.Text + "','" + TextBox4.Text + "','','','" + ddlversion.SelectedValue + "','" + DropDownList2.SelectedValue + "','','true','" + DropDownList3.SelectedValue + "','','','')";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                lblmsg0.Visible = true;
                lblmsg0.Text = "Record inserted successfully";
            }
            else
            {
                string str = "INSERT INTO PageMaster(PageName,PageTitle,PageDescription,PageIndex,VersionInfoMasterId,MainMenuId,FolderName,Active,LanguageId,ManuAccess,labelimage) " +
                          "VALUES('" + TextBox5.Text + "','" + TextBox4.Text + "','','','" + ddlversion.SelectedValue + "','" + DropDownList2.SelectedValue + "','','true','','','')";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                lblmsg0.Visible = true;
                lblmsg0.Text = "Record inserted successfully";

            }


            string strcln1 = "  select Max(PageMaster.PageId) as PageId  from PageMaster inner join VersionInfoMaster on PageMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and PageMaster.VersionInfoMasterId='" + ddlversion.SelectedValue + "'";

            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);


            if (dtcln1.Rows.Count > 0)
            {

                con.Close();

                DataTable dtff = select("select Max(OrderNo) from FunctionalityPageOrderTbl where FunctionalityMasterTblID='" + ddlfuncti.SelectedValue + "'");//where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");


                int orderno = Convert.ToInt32(dtff.Rows[0][0].ToString());
                orderno = orderno + 1;

                string sds = Convert.ToString(ViewState["datavac"]);
                string strftpinsert = "INSERT INTO FunctionalityPageOrderTbl (FunctionalityMasterTblID, PagemasterID, OrderNo) values('" + ddlfuncti.SelectedValue + "'," + dtcln1.Rows[0][0].ToString()+ ",'" +orderno + "')";
                SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                con.Open();
                cmdinsert.ExecuteNonQuery();
                con.Close();


                string str1 = " insert into PageVersionTbl(PageMasterId,VersionName,Date,VersionNo,PageName,Active)values('" + dtcln1.Rows[0]["PageId"] + "','Version-1','" + System.DateTime.Now.ToShortDateString() + "','1','" + TextBox5.Text + "','1')";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

            }
            TextBox4.Text = "";
            TextBox5.Text = "";
            DropDownList1.SelectedValue = "0";
            DropDownList2.SelectedValue = "0";
            DropDownList3.SelectedValue = "0";
        }
        else
        {
            lblmsg0.Visible = true;
            lblmsg0.Text = "Sorry,Record already existed.";
        }
        fillgrid();

    }
  protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
  protected void Button5_Click(object sender, EventArgs e)
  {
      Button lnkbtn = (Button)sender;
      GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
      int j = Convert.ToInt32(row.RowIndex);
      Label pageid = (Label)GridView3.Rows[j].FindControl("Label31");

      int orderno = 0;
      DataTable dtff = select("select Max(OrderNo) from FunctionalityPageOrderTbl where FunctionalityMasterTblID='" + ddlfuncti.SelectedValue + "'");//where VersionID='" + ViewState["VersionInfoId"] + "' and Active=1");

      try
      {
          orderno = Convert.ToInt32(dtff.Rows[0][0].ToString());
      }
      catch
      {
      }
      orderno = orderno + 1;

      string sds = Convert.ToString(ViewState["datavac"]);
      string strftpinsert = "INSERT INTO FunctionalityPageOrderTbl (FunctionalityMasterTblID, PagemasterID, OrderNo) values('" + ddlfuncti.SelectedValue + "'," + pageid.Text + ",'" + orderno + "')";
      SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
      con.Open();
      cmdinsert.ExecuteNonQuery();
      con.Close();
      fillgrid();
  }
  protected void TextBox6_TextChanged(object sender, EventArgs e)
  {
      fillpagegrid();
      ModalPopupExtender4.Show();
  }
  protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
  {
      lblmsg.Text = "";
      GridView1.PageIndex = e.NewPageIndex;

      fillgrid();
  }
}