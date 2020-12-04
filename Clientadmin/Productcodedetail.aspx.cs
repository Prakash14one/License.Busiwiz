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
using System.DirectoryServices;
using System.IO.Compression;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Security.Cryptography;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;


public partial class CodeType : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    SqlConnection conn;
 //   SqlConnection conmaster = new SqlConnection(ConfigurationManager.ConnectionStrings["masterfile"].ConnectionString);
    public static string encstr = "";
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ClientId"] = "35";
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {

            FillProduct();
            //fillcodetypecategory();
            //fillgrid();
            FillProductsearch();
            FillMainFOlderdown();
            fillgrid();
            
        }

    }
    protected void FillProductsearch()
    {
        //string strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1' and ProductDetail.Active ='True' order  by productversion";
        string activestr = "";
        if (CheckBox1.Checked == true)
        {
            activestr = " and VersionInfoMaster.Active=1 and ProductDetail.Active='1' ";
            //activestr = " where ProductDetail.Active='1' ";
        }

        string strcln = " SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + "  " + activestr + " order  by productversion";

      //  strcln = " Select  distinct ProductId,ProductName from ProductMaster  order by ProductName ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "productversion";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");
        ddlProductname.Items[0].Value = "0";
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
      
        FillProductsearch();
        fillgrid();
    }
    protected void fillgrid()
    {
        string st1 = "";
        if (ddlProductname.SelectedIndex > 0)
        {
            // st1 += " and PortalMasterTbl.ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlProductname.SelectedValue + "' ) ";
            st1 += " where ProductCodeDetailTbl.ProductId = '" + ddlProductname.SelectedValue + "' " + st1 + "  ";
        }

        DataTable dtsvr = selectBZ(" Select * From ProductCodeDetailTbl " + st1 + " ");

        GridView1.DataSource = dtsvr;

        GridView1.DataBind();
    }
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        //   dllportal();
        //  ddlsrechportal_SelectedIndexChanged(sender, e);
        fillgrid();
    }
    protected void FillProduct()
    {
        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion");
        ddlproductversion.DataSource = dtcln;
        ddlproductversion.DataValueField = "VersionInfoId";
        ddlproductversion.DataTextField = "productversion";
        ddlproductversion.DataBind();
        fillproductid();
    }

    protected void fillproductid()
    {
        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and VersionInfoMaster.VersionInfoId='" + ddlproductversion.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            ViewState["ProductId"] = dtcln.Rows[0]["ProductId"].ToString();
        }

    }

    //protected void fillcodedefaultcategory()
    //{
    //    DataTable dtcln = selectBZ("SELECT * from ProductCodeDetailTbl where ProductId='" + ViewState["ProductId"].ToString() + "'");
    //    DropDownList1.DataSource = dtcln;
    //    DropDownList1.DataValueField = "Id";
    //    DropDownList1.DataTextField = "CodeTypeName";
    //    DropDownList1.DataBind();


    //}

    //protected void fillcodetypecategory()
    //{
    //    DataTable dtcln = selectBZ(" select * from CodeTypeCategory ");
    //    ddlcodetypecategory.DataSource = dtcln;
    //    ddlcodetypecategory.DataValueField = "CodeMasterNo";
    //    ddlcodetypecategory.DataTextField = "CodeTypeCategory";
    //    ddlcodetypecategory.DataBind();
    //}
    //protected void Button1_Click1(object sender, EventArgs e)
    //{
    //    if (Button3.Text == "Printable Version")
    //    {
    //        Button3.Text = "Hide Printable Version";
    //        Button4.Visible = true;

    //    }
    //    else
    //    {
    //        Button3.Text = "Printable Version";
    //        Button4.Visible = false;

    //    }
    //}
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Update")
        {
            string otherup = " update ProductCodeDetailTbl set ProductId='" + ddlProductname.SelectedValue + "' ,CodeTypeName='" + txtcodetypename.Text + "' Where  id='" + ViewState["ID"] + "' ";
            SqlCommand cmdotherup = new SqlCommand(otherup, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdotherup.ExecuteNonQuery();
            con.Close();
            string st2 = " Delete from CompanyBackupFolderMaster where ProductCodeDetailid=" + ViewState["ID"];
            SqlCommand cmd2 = new SqlCommand(st2, con);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
            foreach (GridViewRow gdr in gridFileAttach.Rows)
            {
                Label lblFolderID = (Label)gdr.FindControl("lblFolderID");
                Label lblSubfolderid = (Label)gdr.FindControl("lblSubfolderid");
                Label lblSubsubfolderid = (Label)gdr.FindControl("lblSubsubfolderid");

                Label lblFolderCatName = (Label)gdr.FindControl("lblFolderCatName");
                Label lblFolderSubName = (Label)gdr.FindControl("lblFolderSubName");
                Label lblFolderSubSubName = (Label)gdr.FindControl("lblFolderSubSubName");
                Label lbllastfolder = (Label)gdr.FindControl("lbllastfolder");
                string path = "";
                path = lblFolderCatName.Text;
                if (lblFolderSubName.Text == "0")
                {
                    path += "\\" + lblFolderSubName.Text;
                    if (lblFolderSubSubName.Text == "0")
                    {
                        path += "\\" + lblFolderSubSubName.Text;
                    }
                }
                con.Open();
                SqlCommand cmd = new SqlCommand("Inser_CompanyBackupFolderMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductCodeDetailid", ViewState["ID"]);
                cmd.Parameters.AddWithValue("@FolderID", lblFolderID.Text);
                cmd.Parameters.AddWithValue("@Subfolderid", lblSubfolderid.Text);
                cmd.Parameters.AddWithValue("@Subsubfolderid", lblSubsubfolderid.Text);
                cmd.Parameters.AddWithValue("@Folderath", path);
                cmd.Parameters.AddWithValue("@FolderName", lbllastfolder.Text);
                cmd.ExecuteNonQuery();
                con.Close();
            }      

            lblmsg.Text = "Record Update successfully";
            lblmsg.Visible = true; 
            Button1.Text = "Submit";
            txtcodetypename.Text = "";
            ddlProductname.SelectedIndex = 0;
            FillProductsearch();
            fillgrid();
            gridFileAttach.DataSource = null;
            gridFileAttach.DataBind();
        }
        else 
        {
            SqlCommand cmdsq = new SqlCommand(" Insert into ProductCodeDetailTbl(ProductId,CodeTypeName,AdditionalPageInserted,BusiwizSynchronization,CompanyDefaultData)Values('" + ddlproductversion.SelectedValue + "','" + txtcodetypename.Text + "',0,0,0)", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdsq.ExecuteNonQuery();
            con.Close();

            	 if (con.State.ToString() != "Open")
        {
            con.Open();
        }
         string strcln1 = "  select Max(id) as id  from ProductCodeDetailTbl ";
        SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
        DataTable dtcln1 = new DataTable();
        SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
        adpcln1.Fill(dtcln1);
        if (dtcln1.Rows.Count > 0)
        {
            foreach (GridViewRow gdr in gridFileAttach.Rows)
            {
                Label lblFolderID = (Label)gdr.FindControl("lblFolderID");
                Label lblSubfolderid = (Label)gdr.FindControl("lblSubfolderid");
                Label lblSubsubfolderid = (Label)gdr.FindControl("lblSubsubfolderid");

                Label lblFolderCatName = (Label)gdr.FindControl("lblFolderCatName");
                Label lblFolderSubName = (Label)gdr.FindControl("lblFolderSubName");
                Label lblFolderSubSubName = (Label)gdr.FindControl("lblFolderSubSubName");
                Label lbllastfolder = (Label)gdr.FindControl("lbllastfolder");
                string path = "";
                path = lblFolderCatName.Text;
                if (lblFolderSubName.Text == "0")
                {
                    path +="\\"+lblFolderSubName.Text;
                    if (lblFolderSubSubName.Text == "0")
                    {
                        path += "\\" + lblFolderSubSubName.Text;
                    }
                }

                SqlCommand cmd = new SqlCommand("Inser_CompanyBackupFolderMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductCodeDetailid", dtcln1.Rows[0]["id"].ToString());
                cmd.Parameters.AddWithValue("@FolderID", lblFolderID.Text);
                cmd.Parameters.AddWithValue("@Subfolderid", lblSubfolderid.Text);
                cmd.Parameters.AddWithValue("@Subsubfolderid", lblSubsubfolderid.Text);
                cmd.Parameters.AddWithValue("@Folderath", path);
                cmd.Parameters.AddWithValue("@FolderName",lbllastfolder.Text) ;
                cmd.ExecuteNonQuery();
                con.Close();
            }            
        }

       

            
            txtcodetypename.Text = "";
            lblmsg.Text = "Record Inserted successfully";

            

            fillgrid(); 
        }
      

        //fillgrid();
    }

    //protected void fillgrid()
    //{
    //    DataTable dtsvr = selectBZ(" select ProductCodeDetailTbl.*,ProductMaster.ProductName +':'+VersionInfoMaster.VersionInfoName as VersionInfoName,ProductCodeDetailTbl.CodeTypeName from ProductCodeDetailTbl  where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' "); //inner join CodeTypeCategory on CodeTypeCategory.CodeMasterNo=CodeTypeTbl.CodeTypeCategoryId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=CodeTypeTbl.ProductVersionId inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=CodeTypeTbl.ProductCodeDetailId

    //    GridView1.DataSource = dtsvr;

    //    GridView1.DataBind();
    //}
    protected void ddlproductversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillproductid();
        FillMainFOlderdown(); 
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_RowEditing1(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        lblmsg.Text = "";
        if (e.CommandName == "edit")
        {
            ViewState["ID"] = e.CommandArgument.ToString();  

            //SqlCommand cmd = new SqlCommand("SELECT * from PortalMasterTbl where Id='" + e.CommandArgument.ToString() + "'", con);
            SqlCommand cmd = new SqlCommand("Select * From ProductCodeDetailTbl where id=" + e.CommandArgument.ToString() + " ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtbn = new DataTable();
            da.Fill(dtbn);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
             if (dtbn.Rows.Count > 0)
            {
                FillProduct();
                ddlproductversion.SelectedIndex = ddlproductversion.Items.IndexOf(ddlproductversion.Items.FindByValue(dtbn.Rows[0]["ProductId"].ToString()));
                txtcodetypename.Text = dtbn.Rows[0]["CodeTypeName"].ToString();
                Button1.Text = "Update";
            }
             FillMainFOlderdown();
             FillSubfolder();
             FillSubSubFolder();
            //----------------------------------
             /*
               dtrow["CodeTypeName"] dtrow["FolderID"] dtrow["FolderCatName"] 
                 dtrow["Subfolderid"] 
                     dtrow["FolderSubName"] = ddl_subfolder.SelectedItem.Text;                 
                 dtrow["Subsubfolderid"] = ddl_SubSubfolder.SelectedValue;
                     dtrow["FolderSubSubName"] = ddl_SubSubfolder.SelectedItem.Text;    
                 
              */
             string stpageall = " SELECT dbo.CompanyBackupFolderMaster.ProductCodeDetailid, CompanyBackupFolderMaster.FolderName, dbo.CompanyBackupFolderMaster.Folderath, dbo.FolderCategoryMaster1.FolderCatName as FolderCatName, dbo.FolderSubCatName.FolderSubName as FolderSubName, dbo.FolderSubSubCategory.FolderSubSubName, dbo.FolderCategoryMaster1.FolderMasterId as FolderID,  dbo.FolderSubSubCategory.FolderSubSubId as Subsubfolderid, dbo.FolderSubCatName.FolderSubId as Subfolderid FROM dbo.FolderSubSubCategory RIGHT OUTER JOIN dbo.FolderSubCatName RIGHT OUTER JOIN dbo.CompanyBackupFolderMaster INNER JOIN dbo.FolderCategoryMaster1 ON dbo.CompanyBackupFolderMaster.FolderID = dbo.FolderCategoryMaster1.FolderMasterId ON  dbo.FolderSubCatName.FolderSubId = dbo.CompanyBackupFolderMaster.Subfolderid ON  dbo.FolderSubSubCategory.FolderSubSubId = dbo.CompanyBackupFolderMaster.Subsubfolderid  Where CompanyBackupFolderMaster.ProductCodeDetailid='" + e.CommandArgument.ToString() + "'";
             SqlCommand cmall = new SqlCommand(stpageall, con);
             DataTable dtall = new DataTable();
             SqlDataAdapter adpall = new SqlDataAdapter(cmall);
             adpall.Fill(dtall);
             Session["GridFileAttach1"] = null;
             if (dtall.Rows.Count > 0)
             {
                 Session["GridFileAttach1"] = dtall;
                 gridFileAttach.DataSource = dtall;
                 gridFileAttach.DataBind();
             }
        }

        if (e.CommandName == "Delete")
        {

            //SqlCommand cmd1 = new SqlCommand("Delete  From PortalMasterTbl  where Id='" + e.CommandArgument.ToString() + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("Delete From ProductCodeDetailTbl where id="+e.CommandArgument.ToString()+"", con);
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Record deleted successfully.";
            fillgrid();
            con.Close();                     
            


        }

    }


    //---------------------------


    protected void FillMainFOlderdown()
    {
        if (ddlproductversion.SelectedIndex > 0)
        {
            string cmdstr = " SELECT FolderMasterId , FolderCatName from FolderCategoryMaster1 where  ProductId=" + ddlproductversion.SelectedValue + " and Activestatus='Active' ";
            SqlCommand cmdcln = new SqlCommand(cmdstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddl_MainFolder.DataSource = dtcln;
            ddl_MainFolder.DataValueField = "FolderMasterId";
            ddl_MainFolder.DataTextField = "FolderCatName";
            ddl_MainFolder.DataBind();
            ddl_MainFolder.Items.Insert(0, "-Select Main Folder-");
            ddl_MainFolder.Items[0].Value = "0";
        }
      
    }
    protected void FillSubfolder()
    {
        if (ddl_MainFolder.SelectedIndex > 0)
         {

             string cmdstr = "select  FolderSubId,FolderSubName from FolderSubCatName where  FolderMasterId='" + ddl_MainFolder.SelectedValue + "' and Activestatus='1'";
            SqlCommand cmdcln = new SqlCommand(cmdstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddl_subfolder.DataSource = dtcln;

            ddl_subfolder.DataValueField = "FolderSubId";
            ddl_subfolder.DataTextField = "FolderSubName";
            ddl_subfolder.DataBind();
            ddl_subfolder.Items.Insert(0, "-Select Sub Folder-");
            ddl_subfolder.Items[0].Value = "0";
        }
       


    }
    protected void FillSubSubFolder()
    {
        if (ddl_subfolder.SelectedIndex > 0)
        {
            ddl_SubSubfolder.Items.Clear();
            string strcln = " SELECT  * From FolderSubSubCategory Where Activestatus='1' and FolderSubId='" + ddl_subfolder.SelectedValue + "'  Order By FolderSubSubName ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddl_SubSubfolder.DataSource = dtcln;
            ddl_SubSubfolder.DataValueField = "FolderSubSubId";
            ddl_SubSubfolder.DataTextField = "FolderSubSubName";
            ddl_SubSubfolder.DataBind();
            ddl_SubSubfolder.Items.Insert(0, "---Select Subsub Folder---");
            ddl_SubSubfolder.Items[0].Value = "0";
        }
       


    }
   
    protected void ddlMainMenu_SelectedIndexChangedMainFolder(object sender, EventArgs e)
    {
      
           // txtFolderName.Text = ddl_MainFolder.SelectedItem.Text;
            FillSubfolder();
        
    }
    protected void ddlMainMenu_SelectedIndexChangedsubFolder(object sender, EventArgs e)
    {       
           // txtFolderName.Text = ddl_MainFolder.SelectedItem.Text + "/" + ddl_subfolder.SelectedItem.Text;
            FillSubSubFolder();       
    }

    protected void ddlMainMenu_SelectedIndexChangedSubsubFolder(object sender, EventArgs e)
    {
        if (ddl_SubSubfolder.SelectedIndex > 0)
        {
           // txtFolderName.Text = ddl_MainFolder.SelectedItem.Text + "/" + ddl_subfolder.SelectedItem.Text + "/" + ddl_SubSubfolder.SelectedItem.Text;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        lblempmsg.Visible = false;
        if (ddl_MainFolder.SelectedIndex > 0)
        {

            String filename = "";
            string audiofile = "";
            DataTable dt = new DataTable();

            int add = 1;
            foreach (GridViewRow gdr in gridFileAttach.Rows)
            {
                Label FolderID = (Label)gdr.FindControl("lblFolderID");
                Label Subfolderid = (Label)gdr.FindControl("lblSubfolderid");
                Label Subsubfolderid = (Label)gdr.FindControl("lblSubsubfolderid");

                if (ddl_MainFolder.SelectedValue == FolderID.Text)
                {
                    if (ddl_subfolder.SelectedValue == Subfolderid.Text)
                        {                           
                                if (ddl_SubSubfolder.SelectedValue == Subsubfolderid.Text)
                                {
                                    add = 0;
                                }                                
                        }                   
                }
            }
            if (add == 1)
            {
                if (Session["GridFileAttach1"] == null)
                {
                    lblempmsg.Visible = false;
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "ProductCodeDetailid";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);

                    DataColumn dtcom3 = new DataColumn();
                    dtcom3.DataType = System.Type.GetType("System.String");
                    dtcom3.ColumnName = "CodeTypeName";
                    dtcom3.ReadOnly = false;
                    dtcom3.Unique = false;
                    dtcom3.AllowDBNull = true;
                    dt.Columns.Add(dtcom3);

                    DataColumn dtcom4 = new DataColumn();
                    dtcom4.DataType = System.Type.GetType("System.String");
                    dtcom4.ColumnName = "FolderID";
                    dtcom4.ReadOnly = false;
                    dtcom4.Unique = false;
                    dtcom4.AllowDBNull = true;
                    dt.Columns.Add(dtcom4);

                    DataColumn dtcom5 = new DataColumn();
                    dtcom5.DataType = System.Type.GetType("System.String");
                    dtcom5.ColumnName = "FolderCatName";
                    dtcom5.ReadOnly = false;
                    dtcom5.Unique = false;
                    dtcom5.AllowDBNull = true;
                    dt.Columns.Add(dtcom5);

                    DataColumn dtcom6 = new DataColumn();
                    dtcom6.DataType = System.Type.GetType("System.String");
                    dtcom6.ColumnName = "Subfolderid";
                    dtcom6.ReadOnly = false;
                    dtcom6.Unique = false;
                    dtcom6.AllowDBNull = true;
                    dt.Columns.Add(dtcom6);

                    DataColumn dtcom7 = new DataColumn();
                    dtcom7.DataType = System.Type.GetType("System.String");
                    dtcom7.ColumnName = "FolderSubName";
                    dtcom7.ReadOnly = false;
                    dtcom7.Unique = false;
                    dtcom7.AllowDBNull = true;
                    dt.Columns.Add(dtcom7);

                    DataColumn dtcom8 = new DataColumn();
                    dtcom8.DataType = System.Type.GetType("System.String");
                    dtcom8.ColumnName = "Subsubfolderid";
                    dtcom8.ReadOnly = false;
                    dtcom8.Unique = false;
                    dtcom8.AllowDBNull = true;
                    dt.Columns.Add(dtcom8);

                    DataColumn dtcom9 = new DataColumn();
                    dtcom9.DataType = System.Type.GetType("System.String");
                    dtcom9.ColumnName = "FolderSubSubName";
                    dtcom9.ReadOnly = false;
                    dtcom9.Unique = false;
                    dtcom9.AllowDBNull = true;
                    dt.Columns.Add(dtcom9);

                    DataColumn dtcom10 = new DataColumn();
                    dtcom10.DataType = System.Type.GetType("System.String");
                    dtcom10.ColumnName = "FolderName";
                    dtcom10.ReadOnly = false;
                    dtcom10.Unique = false;
                    dtcom10.AllowDBNull = true;
                    dt.Columns.Add(dtcom10);  
                    
                }
                else
                {
                    dt = (DataTable)Session["GridFileAttach1"];
                }
                DataRow dtrow = dt.NewRow();
                //dtrow["CodeTypeName"] = txtcodetypename.Text;
                dtrow["FolderID"] = ddl_MainFolder.SelectedValue;
                dtrow["FolderCatName"] = ddl_MainFolder.SelectedItem.Text;

                dtrow["FolderName"] = ddl_MainFolder.SelectedItem.Text;
                if (ddl_subfolder.SelectedIndex > 0)
                {
                    dtrow["FolderSubName"] = ddl_subfolder.SelectedItem.Text;
                    dtrow["Subfolderid"] = ddl_subfolder.SelectedValue;

                    dtrow["FolderName"] = ddl_subfolder.SelectedItem.Text;
                }
                else
                {
                    dtrow["FolderSubName"] = "";
                    dtrow["Subfolderid"] = "0";
                }
                
                if (ddl_SubSubfolder.SelectedIndex > 0)
                {
                    dtrow["FolderSubSubName"] = ddl_SubSubfolder.SelectedItem.Text;
                    dtrow["Subsubfolderid"] = ddl_SubSubfolder.SelectedValue;

                    dtrow["FolderName"] = ddl_SubSubfolder.SelectedItem.Text;
                }
                else
                {
                    dtrow["FolderSubSubName"] = "";
                    dtrow["Subsubfolderid"] = "0";
                }
                
                dt.Rows.Add(dtrow);
              
               
                    Session["GridFileAttach1"] = dt;
                    if (Session["GridFileAttach1"] != null)
                    {
                        gridFileAttach.DataSource = dt;
                        gridFileAttach.DataBind();
                    }
                
                ddl_MainFolder.SelectedIndex = 0;
                ddl_subfolder.SelectedIndex = 0;
                ddl_SubSubfolder.SelectedIndex = 0;
            }
            else
            {
                lblempmsg.Visible = true;
            }

         
           

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
}
