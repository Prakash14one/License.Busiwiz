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
//using Microsoft.VisualBasic.FileIO;
public partial class productcode_databaseaddmanage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    bool gg;
    SqlConnection conn;
    //   SqlConnection conmaster = new SqlConnection(ConfigurationManager.ConnectionStrings["masterfile"].ConnectionString);
    public static string encstr = "";
    public static string str = "";
     
    int versionfolder;
    string menuid;
    string submenuid;
    string category;
    string masterpage;
    string websitesection;
   
    protected void Page_Load(object sender, EventArgs e)
    {
       
      //  string[] folders = System.IO.Directory.GetDirectories(@"E:\PRAKASH\AHM LOCAL SERVER\License.busiwiz.com_AhmServer\Clientadmin", "*", System.IO.SearchOption.AllDirectories);
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            ViewState["changedata"] = "0";
            FillProduct();
            fillcodetypecategory();

            //For Code---------------
            FillWebsiteMaster();
            //
            //fillgrid();
           // FillProductsearch();
           // FillMainFOlderdown();
            fillgrid();
            FillProductsearch();
        }
    }
  
    public void clear()
    {
        ddlproductversion.SelectedIndex = 0;
        DDLWebsiteC.SelectedIndex = 0;
       

        txtcodetypename.Text = "";
       
        lblmsg.Text = "";
        ddlproductversion.Enabled = true;
        ddlcodetypecategory.Enabled = true;

        btn_submitCode.Visible = true;
        btn_updateCode.Visible = false;      

       // lblfinishmsg.Visible = false;
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        fillgrid();

        Panel1.Visible = false;
        Panel4.Visible = true;
        ddlproductversion.Enabled = true;
        ddlcodetypecategory.Enabled = true;
        clear();
    }
    protected void btnAddNewDAta_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        clear();
    }
    protected void FillProduct()
    {
        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion,ProductMaster.ProductName FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductMaster.ProductId = dbo.ProductDetail.ProductId AND dbo.VersionInfoMaster.VersionInfoName = dbo.ProductDetail.VersionNo where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and ProductDetail.Active='True' and VersionInfoMaster.Active='True'  order  by productversion");
        ddlproductversion.DataSource = dtcln;
        ddlproductversion.DataValueField = "VersionInfoId";
        ddlproductversion.DataTextField = "ProductName";
        ddlproductversion.DataBind();
        fillproductid();
    }
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
    protected void fillproductid()
    {
        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId,  dbo.ClientMaster.ServerId  ,ProductMaster.Description,dbo.VersionInfoMaster.ServerMasterCodeSourceIISWebsitePath  FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.VersionNo = dbo.VersionInfoMaster.VersionInfoName INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId  where ProductMaster.ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and VersionInfoMaster.VersionInfoId='" + ddlproductversion.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            ViewState["ProductId"] = dtcln.Rows[0]["ProductId"].ToString();
            txt_prod_desc.Text = dtcln.Rows[0]["Description"].ToString();
            lbl_serverid.Text = dtcln.Rows[0]["ServerId"].ToString();
        }
        else
        {
            txt_prod_desc.Text = "";
        }

    }   
    protected void ddlproductversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillproductid();
       // FillMainFOlderdown(); 
        FillWebsiteMaster();
        DDLWebsiteC_SelectedIndexChanged(sender, e);
    }
    protected void fillcodetypecategory()
    {
        DataTable dtcln = selectBZ(" select * from CodeTypeCategory where id='1' ");
        ddlcodetypecategory.DataSource = dtcln;
        ddlcodetypecategory.DataValueField = "CodeMasterNo";
        ddlcodetypecategory.DataTextField = "CodeTypeCategory";
        ddlcodetypecategory.DataBind();

        ddlcodetypecategory0.DataSource = dtcln;
        ddlcodetypecategory0.DataValueField = "CodeMasterNo";
        ddlcodetypecategory0.DataTextField = "CodeTypeCategory";
        ddlcodetypecategory0.DataBind();

    }
    protected void ddlcodetypecategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
   
    
   
   
   
   
    //-------------Add product code part --------------------------------------------------------------------
     protected void FillWebsiteMaster()
    {
        string strcln = " SELECT distinct ID, WebsiteName,WebsiteUrl From WebsiteMaster Where VersionInfoId=" + ddlproductversion.SelectedValue + " ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DDLWebsiteC.DataSource = dtcln;       
        DDLWebsiteC.DataValueField = "ID";
        DDLWebsiteC.DataTextField = "WebsiteName";
        DDLWebsiteC.DataBind();
        DDLWebsiteC.Items.Insert(0, "--Select--");
        DDLWebsiteC.Items[0].Value = "0";
        DDLWebsiteC.SelectedIndex = 0;
    }
    protected void DDLWebsiteC_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strcln = " SELECT DISTINCT dbo.WebsiteMaster.ID,dbo.WebsiteMaster.WebsiteUrl, dbo.WebsiteMaster.WebsiteName, dbo.WebsiteMaster.RootFolderPath, dbo.VersionInfoMaster.MasterCodeLatestVersionFolderFullPath, dbo.VersionInfoMaster.VersionInfoId,dbo.VersionInfoMaster.Active FROM dbo.ProductMaster INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.WebsiteMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId Where dbo.WebsiteMaster.ID='" + DDLWebsiteC.SelectedValue + "'  ";
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
         
        }
    }
    
    protected void btn_submitCode_Click(object sender, EventArgs e)
    {
          
        //if (DDLWebsiteC.SelectedValue >0)
        //{
        //    string strcln1 = "  select * from Websitemaster_Information where websiteid='" + websiteid + "' and foldername='" + foldername + "'";
        //    SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
        //    DataTable dtcln1 = new DataTable();
        //    SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
        //    adpcln1.Fill(dtcln1);
        //    if (dtcln1.Rows.Count == 0)
        //    {
        //        if (con.State.ToString() != "Open")
        //        {
        //            con.Open();
        //        }
        //        SqlCommand cmd = new SqlCommand("Websitemaster_Information_AddDelUpdtSelect", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@StatementType", "Insert");
        //        cmd.Parameters.AddWithValue("@websiteid", websiteid);
        //        cmd.Parameters.AddWithValue("@websitepath", websitepath);
        //        cmd.Parameters.AddWithValue("@filename", filename);
        //        cmd.Parameters.AddWithValue("@foldername", foldername);
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }


        //    if (con.State.ToString() != "Open")
        //    {
        //        con.Open();
        //    }
        //    SqlCommand cmd = new SqlCommand("ProductCodeVersionDetailDeleteFolder_AddDelUpdtSelect", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@StatementType", "Insert");
        //    cmd.Parameters.AddWithValue("@CodeTypeID", lbl_codetypeid.Text);
        //    cmd.Parameters.AddWithValue("@VersionDeleteFolderPath", lblfoldername.Text);
        //    cmd.Parameters.AddWithValue("@Websitemaster_Information_ID", lblfolderid.Text);
        //    cmd.Parameters.AddWithValue("@DeleteAll_Subfolder", true);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}       
        clear();
        fillgrid();
        Panel1.Visible = false;
        lblmsg.Visible = true;
        lblmsg.Text = "Record inserted successfully";
    }
   
   
    public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        Directory.CreateDirectory(target.FullName);

        // Copy each file into the new directory.
        foreach (FileInfo fi in source.GetFiles())
        {
            Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }

        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }
    }

    protected void btn_updateCode_Click(object sender, EventArgs e)
    {
        if (ddlcodetypecategory.SelectedValue == "1" || ddlcodetypecategory.SelectedValue == "3")
        {
                      
        }      
        
        clear();
        btn_updateCode.Visible = false;
        btn_submitCode.Visible = true;
        fillgrid();
        Panel1.Visible = false;
        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully";

    } 
   //Grid
    protected void FillProductsearch()
    {
        string active = "";
        if (chk_Active.Checked == true)
        {
            active = "and ProductDetail.Active='True' and VersionInfoMaster.Active='True' ";
        }
        string strcln = " SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductMaster.ProductId = dbo.ProductDetail.ProductId AND  dbo.VersionInfoMaster.VersionInfoName = dbo.ProductDetail.VersionNo where ClientMasterId=" + Session["ClientId"].ToString() + "  " + active + " order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "ProductName";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");
        ddlProductname.Items[0].Value = "0";
    }
    protected void chk_Active_CheckedChanged(object sender, EventArgs e)
    {
        FillProductsearch();
    }
    protected void fillgrid()
    {
        string str = "";
       
        if (ddlProductname.SelectedIndex > 0)
        {
            str += "and  dbo.VersionInfoMaster.VersionInfoId= '" + ddlProductname.SelectedValue + "'";
        }
        if (ddlstatus.SelectedItem.Text == "Active")
        {
            str += " and ProductCodeDetailTbl.Active='True'";
        }
        if (ddlstatus.SelectedItem.Text == "Inactive")
        {
            str += " and ProductCodeDetailTbl.Active='False'";
        }
         str += " and CodeTypeCategory.CodeMasterNo='" + ddlcodetypecategory0 .SelectedValue+ "'";
        //
         DataTable dtsvr = selectBZ(" SELECT DISTINCT dbo.ProductCodeDetailTbl.Id,dbo.ProductCodeDetailTbl.Active,dbo.CodeTypeTbl.Temppath,dbo.CodeTypeTbl.Outputpath,dbo.CodeTypeTbl.WebsiteID,dbo.CodeTypeTbl.FileLocationPath,dbo.CodeTypeTbl.FileName,dbo.ProductCodeDetailTbl.Id as vv, dbo.ProductCodeDetailTbl.CodeTypeName , dbo.ProductMaster.ProductName, dbo.ProductMaster.ProductName + ':' + dbo.VersionInfoMaster.VersionInfoName AS VersionInfoName, dbo.CodeTypeCategory.CodeTypeCategory, dbo.ProductCodeDetailTbl.AdditionalPageInserted, dbo.ProductCodeDetailTbl.BusiwizSynchronization, dbo.ProductCodeDetailTbl.CompanyDefaultData,dbo.WebsiteMaster.WebsiteName ,  dbo.ProductCodeVersionDetailDeleteFolder.Id AS DelFolID, dbo.ProductCodeVersionDetailDeleteFolder.CodeTypeID, dbo.ProductCodeVersionDetailDeleteFolder.VersionDeleteFolderPath, dbo.ProductCodeVersionDetailDeleteFolder.DeleteAll_Subfolder, dbo.ProductCodeVersionDetailDeleteFolder.Websitemaster_Information_ID FROM dbo.Websitemaster_Information INNER JOIN dbo.ProductCodeVersionDetailDeleteFolder ON dbo.Websitemaster_Information.id = dbo.ProductCodeVersionDetailDeleteFolder.Websitemaster_Information_ID INNER JOIN dbo.WebsiteMaster INNER JOIN dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.CodeTypeTbl.ProductVersionId INNER JOIN dbo.ProductMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ProductCodeDetailTbl.Id = dbo.CodeTypeTbl.ProductCodeDetailId ON dbo.WebsiteMaster.ID = dbo.CodeTypeTbl.WebsiteID ON dbo.Websitemaster_Information.websiteid = dbo.WebsiteMaster.ID where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' " + str + "");
       // SELECT dbo.ProductCodeDetailTbl.CodeTypeName, dbo.ProductMaster.ProductName + ':' + dbo.VersionInfoMaster.VersionInfoName AS VersionInfoName, dbo.CodeTypeCategory.CodeTypeCategory, dbo.ProductCodeDetailTbl.AdditionalPageInserted, dbo.ProductCodeDetailTbl.BusiwizSynchronization, dbo.ProductCodeDetailTbl.CompanyDefaultData, dbo.ProductCodeDetailTbl.Id AS vv FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId CROSS JOIN dbo.CodeTypeCategory CROSS JOIN dbo.ProductCodeDetailTbl
        GridView1.DataSource = dtsvr;
        GridView1.DataBind();
    } 
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlcodetypecategory0_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }   
   
    protected void ImgBtn_EditGrig(object sender, ImageClickEventArgs e)
    {
        lblmsg.Visible = true;
        lblmsg.Text = "";
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label id = (Label)GridView1.Rows[j].FindControl("Label7");
        ViewState["id"] = id.Text;
        string strcln = "select * from ProductCodeDetailTbl where Id="+id.Text+"";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            FillProduct();
            ddlproductversion.SelectedValue = dtcln.Rows[0]["ProductId"].ToString();
            ddlproductversion.Enabled = false;
            txtcodetypename.Text = dtcln.Rows[0]["CodeTypeName"].ToString();
         
            try
            {
                Chk_addactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
            }
            catch
            {
            }           
        }
        string strcln1 = "select * from CodeTypeTbl where ProductCodeDetailId=" + id.Text + "";
        SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
        DataTable dtcln1 = new DataTable();
        SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
        adpcln1.Fill(dtcln1);
        if (dtcln1.Rows.Count > 0)
        {
         
        }
      
        Panel1.Visible = true;
        btn_submitCode.Visible = false;
        btn_updateCode.Visible = true;
    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        lblmsg.Visible = true;
        lblmsg.Text = "";
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label id = (Label)GridView1.Rows[j].FindControl("Label7");
        ViewState["id"] = id.Text;
        string strcln = "select id from ProductMasterCodeTbl where CodeTypeID=" + id.Text + "";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";           
        }
        else
        {            
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("ProductCodeDetailTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Delete");
                cmd.Parameters.AddWithValue("@ID", ViewState["id"]);
                cmd.ExecuteNonQuery();
               
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Delete");
                cmd.Parameters.AddWithValue("@ProductCodeDetailId", ViewState["id"]);
                cmd.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Record Deleted Sucessfully";

        }
        
        fillgrid();
    }   
   
  
   
    

    //--------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------
   
    protected void Insert_Websitemaster_Information(int websiteid, string websitepath, string filename, string foldername)
    {
        string strcln1 = "  select * from Websitemaster_Information where websiteid='" + websiteid + "' and foldername='" + foldername + "'";
        SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
        DataTable dtcln1 = new DataTable();
        SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
        adpcln1.Fill(dtcln1);
        if (dtcln1.Rows.Count == 0)
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Websitemaster_Information_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            cmd.Parameters.AddWithValue("@websiteid", websiteid);
            cmd.Parameters.AddWithValue("@websitepath", websitepath);
            cmd.Parameters.AddWithValue("@filename", filename);
            cmd.Parameters.AddWithValue("@foldername", foldername);
            cmd.ExecuteNonQuery();
            con.Close();
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
    //-***************************




        

   
  
}