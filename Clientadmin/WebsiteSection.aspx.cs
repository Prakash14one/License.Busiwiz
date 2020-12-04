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
       // Session["ClientId"] = 35;
        if (!IsPostBack)
        {          
            FillProduct();
            Fillgrid();
            FilFolder();
            ViewState["sortOrder"] = "";
        }
    }
    //Folder Path DDL
    protected void FilFolder()
    {

        ddl_MainFolder.Items.Clear();
        string strcln = " SELECT  * From FolderCategoryMaster1 where Activestatus='Active'  order By FolderCatName ";
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

    protected void FillSubfolder()
    {

        ddl_subfolder.Items.Clear();
        if (ddl_MainFolder.SelectedIndex > 0)
        {
        
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


    }
    protected void FillSubSubFolder()
    {

        ddl_SubSubfolder.Items.Clear();
        if (ddl_subfolder.SelectedIndex > 0)
        {
        
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

    }
    protected void ddlMainMenu_SelectedIndexChangedMainFolder(object sender, EventArgs e)
    {
        if (ddl_MainFolder.SelectedIndex > 0)
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


     /////////--------------------------------------
    

   

    protected void FillProduct()
    {


        string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,  ProductMaster.ProductName + ':' +   VersionInfoMaster.VersionInfoName + ' : '+  WebsiteMaster.WebsiteName   as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlWebsite.DataSource = dtcln;

        ddlWebsite.DataValueField = "WebsiteMaster_ID";
        ddlWebsite.DataTextField = "productversion";
        ddlWebsite.DataBind();
        ddlWebsite.Items.Insert(0, "-Select-");
        ddlWebsite.Items[0].Value = "0";

    }


    protected void fillPage()
    {

        if (ddlWebsite.SelectedIndex == 0)
        {
            string strcln = "Select distinct PageMaster.PageId,PageMaster.PageName from PageMaster inner join WebsiteMaster on WebsiteMaster.VersionInfoId=PageMaster.VersionInfoMasterId   order  by PageName";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddlLoginPage.DataSource = dtcln;

            ddlLoginPage.DataValueField = "PageId";
            ddlLoginPage.DataTextField = "PageName";
            ddlLoginPage.DataBind();
            ddlLoginPage.Items.Insert(0, "-Select-");
            ddlLoginPage.Items[0].Value = "0";
        }
        else
        {
            string strcln = "Select distinct PageMaster.PageId,PageMaster.PageName from PageMaster inner join WebsiteMaster on WebsiteMaster.VersionInfoId=PageMaster.VersionInfoMasterId where  WebsiteMaster.ID='" + ddlWebsite.SelectedValue + "' order  by PageName";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddlLoginPage.DataSource = dtcln;

            ddlLoginPage.DataValueField = "PageId";
            ddlLoginPage.DataTextField = "PageName";
            ddlLoginPage.DataBind();
            ddlLoginPage.Items.Insert(0, "-Select-");
            ddlLoginPage.Items[0].Value = "0";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

       
      
         string str22 = "  Select WebsiteSection.* from WebsiteSection where WebsiteSection.SectionName='" + txtSectionName.Text + "' and WebsiteSection.AfterLoginDefaultPageId='" + ddlLoginPage.SelectedValue + "' and WebsiteSection.WebsiteMasterId='"+ddlWebsite.SelectedValue+"'";

        SqlCommand cmd2 = new SqlCommand(str22,con);
        con.Open();
       SqlDataReader rdr= cmd2.ExecuteReader();
       if (rdr.Read())
       {
           lblmsg.Text = "You can't add Same Website Section  in the Same Version of the Website";
           con.Close();
       }

       else
       {
           con.Close();
           string MasterInsert = "";
           string Mainfol="";
           string SubFol="";
           string MainsubSubFol="";
           string path = "";
           if (ddl_MainFolder.SelectedIndex > 0)
           {
               Mainfol = ddl_MainFolder.SelectedValue; 
           }
           if (ddl_subfolder.SelectedIndex > 0)
           {
               SubFol = ddl_subfolder.SelectedValue;
           }
           if (ddl_SubSubfolder.SelectedIndex > 0)
           {
               MainsubSubFol = ddl_SubSubfolder.SelectedValue;
           }

           if (txtFolderName.Text !="") 
           {
               path = txtFolderName.Text;
           }


           if (ddlLoginPage.SelectedIndex > 0)
           {
               MasterInsert = "Insert Into WebsiteSection (WebsiteMasterId,SectionName,AfterLoginDefaultPageId,NormalUrl,LoginUrl , PageLocationPath, MainFolder, SubFolder, SubSubFolder) values ('" + ddlWebsite.SelectedValue + "','" + txtSectionName.Text + "','" + ddlLoginPage.SelectedValue + "','" + txtNormalUrl.Text + "','" + txtLoginUrl.Text + "', '" + txtFolderName.Text + "' ,'" + Mainfol + "' ,'" + SubFol + "' ,'" + MainsubSubFol + "')";
           }
           else
           {
               MasterInsert = "Insert Into WebsiteSection (WebsiteMasterId,SectionName,NormalUrl,LoginUrl, PageLocationPath, MainFolder, SubFolder, SubSubFolder) values " 
                   +" ('" + ddlWebsite.SelectedValue + "','" + txtSectionName.Text + "','" + txtNormalUrl.Text + "','" + txtLoginUrl.Text + "' , '" + txtFolderName.Text + "' ,'" + Mainfol + "' ,'" + SubFol + "' ,'" + MainsubSubFol + "')";
           }
           SqlCommand cmd = new SqlCommand(MasterInsert, con);
           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();          
           Fillgrid();
           lblmsg.Visible = true;
           lblmsg.Text = "Record inserted successfully";
           ModernpopSync.Show(); 
           clearall();
           lbllegend.Text = "";
           addnewpanel.Visible = true;
           pnladdnew.Visible = false;
           //}
           //}
       }
    }
    protected void Fillgrid()
    {
        string str = "select distinct WebsiteSection.* ,WebsiteMaster.WebsiteName,PageMaster.PageName  from WebsiteSection   inner join WebsiteMaster on WebsiteMaster.ID = WebsiteSection.WebsiteMasterId Left Outer join PageMaster on PageMaster.PageId = WebsiteSection.AfterLoginDefaultPageId  inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId  where ProductMaster.ClientMasterId='" + Session["ClientId"] + "'  ";
        SqlCommand cmd = new SqlCommand(str, con);
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


            GridView1.DataSource = myDataView;
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
        txtLoginUrl.Text = "";
        txtNormalUrl.Text = "";
        txtSectionName.Text="";
        ddlLoginPage.SelectedIndex=0;
        ddlWebsite.SelectedIndex=0;
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        clearall();
        BtnSubmit.Visible = true;
        BtnUpdate.Visible = false;
        lblmsg.Visible = false;
        lblmsg.Text = "";
        lbllegend.Text = "";
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        

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

        

        ////DropDownList ddlmainmenu123 = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlmainmenu123");
        ////Label lblddlmainmenuId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblddlmainmenuId");
        //TextBox txtMasterPageName = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtMasterPageName");

        //TextBox txtdesEdit = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtdesEdit");


        ////GridView1.EditIndex = e.NewEditIndex;
        //Fillgrid();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);

        //TextBox txtMasterPageName = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtMasterPageName");

        //TextBox txtdesEdit = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtdesEdit");


        //string str1 = "select * from WebsiteSection where SectionName='" + txtSectionName.Text + "' and WebsiteMasterId='"+ddlWebsite.SelectedValue+"' and MasterPageId<>'" + dk + "'  ";

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
        //    if (ddlLoginPage.SelectedIndex == 0)
        //    {

        //        string sr51 = ("update WebsiteSection set WebsiteMasterId='" + ddlWebsite.SelectedValue + "',SectionName='" + txtSectionName.Text + "',LoginUrl='" + txtLoginUrl.Text + "',NormalUrl='" + txtNormalUrl.Text + "'  where MasterPageId='" + dk + "' ");
        //        SqlCommand cmd801 = new SqlCommand(sr51, con);

        //        con.Open();
        //        cmd801.ExecuteNonQuery();
        //        con.Close();
        //        GridView1.EditIndex = -1;
        //        Fillgrid();
        //        clearall();
        //        lblmsg.Visible = true;
        //        lblmsg.Text = "Record updated successfully";
        //    }
        //    else
        //    {
        //        string sr51 = ("update WebsiteSection set WebsiteMasterId='" + ddlWebsite.SelectedValue + "',SectionName='" + txtSectionName.Text + "',LoginUrl='" + txtLoginUrl.Text + "',NormalUrl='" + txtNormalUrl.Text + "',AfterLoginDefaultPageId='"+ddlLoginPage.SelectedValue+"'  where MasterPageId='" + dk + "' ");
        //        SqlCommand cmd801 = new SqlCommand(sr51, con);

        //        con.Open();
        //        cmd801.ExecuteNonQuery();
        //        con.Close();
        //        GridView1.EditIndex = -1;
        //        Fillgrid();
        //        clearall();
        //        lblmsg.Visible = true;
        //        lblmsg.Text = "Record updated successfully";
        //    }
        //}

        
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "Edit Website Section";
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;

            lblmsg.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = GridView1.SelectedDataKey.Value;

            edit();


        }
        else if (e.CommandName == "Delete")
        {
            lblmsg.Text = "";
            //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //ViewState["DId"] = GridView1.SelectedDataKey.Value;
            ViewState["DId"] = Convert.ToInt32(e.CommandArgument);

            string st2 = "Delete from WebsiteSection where WebsiteSectionId='" + ViewState["DId"] + "' ";
            SqlCommand cmd2 = new SqlCommand(st2, con);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
            //GridView1.EditIndex = -1;
            Fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";


        }
    }

    protected void edit()
    {

        //string str1 = "select * from EmployeeMaster inner join DesignationMaster on DesignationMaster.Id=EmployeeMaster.DesignationId where DesignationMaster.Name='Admin' and  EmployeeMaster.UserId='" + txtAdminName.Text + "' and  EmployeeMaster.Password = '" + txtPW.Text + "' ";
        //SqlCommand cmd111 = new SqlCommand(str1,con);
        //SqlDataReader rdr=  cmd111.ExecuteReader();
        //if (rdr.Read() == false)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Only Admin is Authorised to Update this Information..";
        //}
        //else
        //{
        string str11 = " select distinct WebsiteSection.* ,WebsiteMaster.WebsiteName,PageMaster.PageName,PageMaster.PageId  from WebsiteSection  inner join WebsiteMaster on WebsiteMaster.ID = WebsiteSection.WebsiteMasterId left outer join PageMaster on PageMaster.PageId = WebsiteSection.AfterLoginDefaultPageId   where WebsiteSection.WebsiteSectionId='" + ViewState["Id"] + "'";

            SqlCommand cmd = new SqlCommand(str11, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            adp.Fill(dt);
            //fillMasterpage();
            FillProduct();


            ddlWebsite.SelectedIndex = ddlWebsite.Items.IndexOf(ddlWebsite.Items.FindByValue(dt.Rows[0]["WebsiteMasterId"].ToString()));
            ddlWebsite.SelectedValue = dt.Rows[0]["WebsiteMasterId"].ToString();
            fillPage();

            ddlLoginPage.SelectedIndex = ddlLoginPage.Items.IndexOf(ddlLoginPage.Items.FindByValue(dt.Rows[0]["PageId"].ToString()));
            
         //txtNormalUrl.Text = dt.Rows[0]["AdminName"].ToString();
         //   txtPW.Text = dt.Rows[0]["AdminPassword"].ToString();

         //   string strr = txtPW.Text;
         //   txtPW.Attributes.Add("Value", strr);
       
            txtSectionName.Text = dt.Rows[0]["SectionName"].ToString();
            txtNormalUrl.Text = dt.Rows[0]["NormalUrl"].ToString();
            txtLoginUrl.Text = dt.Rows[0]["LoginUrl"].ToString();

            txtFolderName.Text = dt.Rows[0]["PageLocationPath"].ToString();
            FilFolder();
            try
            {
                ddl_MainFolder.SelectedValue = dt.Rows[0]["MainFolder"].ToString();
        
            }
            catch (Exception ex)
            {
            }

            try
            {
                FillSubfolder();
                ddl_subfolder.SelectedValue = dt.Rows[0]["SubFolder"].ToString();
        
            }
            catch (Exception ex)
            {
            }

            try
            {
                FillSubSubFolder();
                ddl_SubSubfolder.SelectedValue = dt.Rows[0]["SubSubFolder"].ToString();
        
            }
            catch (Exception ex)
            {
            }
        


            BtnSubmit.Visible = false;
            BtnUpdate.Visible = true;
            //GridView1.EditIndex = -1;
       // }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        string str22 = "  Select WebsiteSection.* from WebsiteSection where WebsiteSectionId !='" + ViewState["Id"] + "' And WebsiteSection.SectionName='" + txtSectionName.Text + "' and WebsiteSection.AfterLoginDefaultPageId='" + ddlLoginPage.SelectedValue + "' and WebsiteSection.WebsiteMasterId='" + ddlWebsite.SelectedValue + "'";

        SqlCommand cmd2 = new SqlCommand(str22,con);
        con.Open();
       SqlDataReader rdr= cmd2.ExecuteReader();
       if (rdr.Read())
       {
           lblmsg.Text = "You can't add Same Website Section  in the Same Version of the Website ";
           con.Close();
       }
             
       else
       {

           string Mainfol = "";
           string SubFol = "";
           string MainsubSubFol = "";
           string path = "";
           if (ddl_MainFolder.SelectedIndex > 0)
           {
               Mainfol = ddl_MainFolder.SelectedValue;
           }
           if (ddl_subfolder.SelectedIndex > 0)
           {
               SubFol = ddl_subfolder.SelectedValue;
           }
           if (ddl_SubSubfolder.SelectedIndex > 0)
           {
               MainsubSubFol = ddl_SubSubfolder.SelectedValue;
           }

           if (txtFolderName.Text != "")
           {
               path = txtFolderName.Text;
           }
                     
           con.Close();
           string MasterInsert = "Update  WebsiteSection Set WebsiteMasterId ='" + ddlWebsite.SelectedValue + "',SectionName='" + txtSectionName.Text + "',AfterLoginDefaultPageId='" + ddlLoginPage.SelectedValue + "',NormalUrl='" + txtNormalUrl.Text + "',LoginUrl='" + txtLoginUrl.Text + "', PageLocationPath='" + txtFolderName.Text + "', MainFolder='" + Mainfol + "', SubFolder='" + SubFol + "', SubSubFolder='" + MainsubSubFol + "' where WebsiteSectionId='" + ViewState["Id"] + "'";
           SqlCommand cmd = new SqlCommand(MasterInsert, con);
           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();
           clearall();
           Fillgrid();

           lblmsg.Visible = true;
           lblmsg.Text = "Record updated successfully";

           BtnSubmit.Visible = true;
           BtnUpdate.Visible = false;
       }
       pnladdnew.Visible = false;
       addnewpanel.Visible = true;
       lbllegend.Text = "";

    }
    protected void ddlWebsite_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillPage();
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        


        if (btnprint.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            asdadasd.ScrollBars = ScrollBars.None;
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
            asdadasd.ScrollBars = ScrollBars.Horizontal;

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
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        lbllegend.Text = "Add New Website Section";
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        lblmsg.Text = "";
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
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
       

                DataTable dt1 = select("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='WebsiteSection' ");
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
            Label1.Text = "Select Product Name";
        }
        if (transf > 0)
        {
            string te = "SyncData.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
}
