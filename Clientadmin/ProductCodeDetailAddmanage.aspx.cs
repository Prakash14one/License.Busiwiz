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

            FilFTP();
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
    protected void FilFTP()
    {
        string finalstr = " Select * From ClientMaster Where ClientMasterId='" + Session["ClientId"] + "'";
        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            string ftp = ds.Rows[0]["FTP"].ToString();
            string user = ds.Rows[0]["FTPUserName"].ToString();
            string pass = ds.Rows[0]["FTPPassword"].ToString();
            string port = ds.Rows[0]["FTPPort"].ToString();
            string ftpuseurl = ds.Rows[0]["FTP"].ToString();
            string username = ds.Rows[0]["FTPUserName"].ToString();
            ftpservename.Text = ftpuseurl;
            ftpuser.Text = username;
        }
        else
        {
        }

    }
    public void clear()
    {
        ddlproductversion.SelectedIndex = 0;
        DDLWebsiteC.SelectedIndex = 0;
        txt_existingwebsite.Text = "";
        txttemppathC.Text = "";
        txtmastercodepathC.Text = "";
        txtoutputsourcepathC.Text = "";

        Chk_CodeOp1.Checked = false;
        Chk_CodeOp2.Checked = false;
        Chk_CodeOp3.Checked = false;

        Chk_CodeOp1.Enabled = true;
        pnl_chkoption1.Visible = false;
        pnl_chkoption2.Visible = false;

        lBox_removablefolderC.Items.Clear();
        lst_box_deletefolderC.Items.Clear();

        lBox_removablefolderC.Visible = false;
        btn_addremovefolderC.Visible = false;
        btnEditremovefolderC.Visible = false;
        lst_box_deletefolderC.Visible = false;
        btn_RemovedeletefolderC.Visible = false;
        btn_editdeletefolderC.Visible = false;

        txtcodetypename.Text = "";
        CheckBox1.Checked = false;
        CheckBox11.Checked = false;
        CheckBox12.Checked = false;
        lblmsg.Text = "";
        ddlproductversion.Enabled = true;
        ddlcodetypecategory.Enabled = true;

        btn_submitCode.Visible = true;
        btn_updateCode.Visible = false;

        pnl_selectradio.Visible = false;

        lblfinishmsg.Visible = false;
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
            FillgridFTP();
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
        Panel2.Visible = false;
        Panel3.Visible = false;        

        if (ddlcodetypecategory.SelectedItem.Text == "Code")
        {
            Panel2.Visible = true;
            Panel3.Visible = false;
        }
        else if (ddlcodetypecategory.SelectedItem.Text == "Database")
        {
            Panel2.Visible = false;
            Panel3.Visible = true;
        }
    }
   
    
    protected void FillgridFTP()
    {
        string finalstr = "SELECT * FROM dbo.ServerMasterTbl Where Id='"+lbl_serverid.Text +"'";
        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lbl_severname.Text = ds.Rows[0]["ServerName"].ToString();
            lbl_pubIP.Text = ds.Rows[0]["PublicIp"].ToString();
            lbl_priIP.Text = ds.Rows[0]["Ipaddress"].ToString();


            string ftp = ds.Rows[0]["FTPforMastercode"].ToString();
            string user = ds.Rows[0]["FTPuseridforDefaultIISpath"].ToString();
           
            txtmastercodepathC.Text = ds.Rows[0]["folderpathformastercode"].ToString();  
              //****************************************************************
            //*********************************************************
            string serversqlserverip = ds.Rows[0]["sqlurl"].ToString();
            string serversqlinstancename = ds.Rows[0]["DefaultsqlInstance"].ToString();
            string serversqldbname = ds.Rows[0]["DefaultDatabaseName"].ToString();
            string serversqlpwd = ds.Rows[0]["Sapassword"].ToString();
            string serversqlport = ds.Rows[0]["port"].ToString();
            SqlConnection connserver = new SqlConnection();
            connserver.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";
            try
            {                       
                if (connserver.State.ToString() != "Open")
                {
                    connserver.Open();
                }                     
                connserver.Close();
                lbl_serverconnectioncheck.Text = "Connection Possible";
                lbl_serverconnectioncheck.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lbl_serverconnectioncheck.Text = "Connection Not Possible";
                lbl_serverconnectioncheck.ForeColor = System.Drawing.Color.Red;
            }
            //****************************************************************
            //*********************************************************
        }
        else
        {
        }
    }
   
   
   
    //-------------Add product code part --------------------------------------------------------------------
    protected void Chk_CodeOp1_CheckedChanged(object sender, EventArgs e)
    {
        pnl_chkoption1.Visible = false;
        pnl_chkoption2.Visible = false;
        if (Chk_CodeOp1.Checked == true)
        {
            Chk_CodeOp2.Checked = false;
            Chk_CodeOp3.Checked = false;
            pnl_selectradio.Visible = true;
            lbl_sourrcepathh.Text = " Recommended source path folder name";

            pnl_chkoption1.Visible = true;
            pnl_chkoption2.Visible = false;
           // txt_existingwebsite.Text = Server.MapPath(txtcodetypename.Text);
        }
    }
    protected void Chk_CodeOp2_CheckedChanged(object sender, EventArgs e)
    {
        pnl_chkoption1.Visible = false;
        pnl_chkoption2.Visible = false;
        if (Chk_CodeOp2.Checked == true)
        {
            lbl_sourrcepathh.Text = "Source path folder name";
            Chk_CodeOp1.Checked = false;
            Chk_CodeOp3.Checked = false;
            pnl_selectradio.Visible = true;
            pnl_chkoption1.Visible = false;
            pnl_chkoption2.Visible = true;

           
        }
    }
    protected void Chk_CodeOp3_CheckedChanged(object sender, EventArgs e)
    {
        pnl_chkoption1.Visible = false;
        pnl_chkoption2.Visible = false;
        if (Chk_CodeOp3.Checked == true)
        {
            Chk_CodeOp2.Checked = false;
            Chk_CodeOp1.Checked = false;
            pnl_selectradio.Visible = true;

            pnl_chkoption2.Visible = true;
        }
    }
    //******************************************************************************************************
    protected void FillWebsiteMaster()
    {
        string strcln = " SELECT distinct ID, WebsiteName,WebsiteUrl From WebsiteMaster Where VersionInfoId=" + ddlproductversion.SelectedValue + " and id NOT IN (Select WebsiteID as id From CodeTypeTbl Where WebsiteID !='') ";//
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
        {            //RootFolderPath
            txtmastercodepathC.Text = dtcln.Rows[0]["RootFolderPath"].ToString();
            txttemppathC.Text = dtcln.Rows[0]["MasterCodeLatestVersionFolderFullPath"].ToString() + "\\" + DDLWebsiteC.SelectedItem.Text + "\\Temp\\";
            txtoutputsourcepathC.Text = dtcln.Rows[0]["MasterCodeLatestVersionFolderFullPath"].ToString() + "\\" + DDLWebsiteC.SelectedItem.Text + "\\PublishCode\\";

            lbl_websiteurl.Text = dtcln.Rows[0]["WebsiteUrl"].ToString();
            GetServerMasterDetail(lbl_websiteurl.Text);

            ViewState["verid"] = dtcln.Rows[0]["VersionInfoId"].ToString();
        }
        else             
        {
            Chk_CodeOp1.Enabled = true;
            lbl_websiteurl.Text = "";
            txtmastercodepathC.Text = "";
            txttemppathC.Text = "";
            txtoutputsourcepathC.Text = "";
        }
    }
    private void GetServerMasterDetail(string websiteurl)
    {
        try
        {
            var ping = new System.Net.NetworkInformation.Ping();
            var result = ping.Send(websiteurl);
            lbl_weburlconnection.Text = "Connect the website";
            lbl_weburlconnection.ForeColor = System.Drawing.Color.Green;
            Chk_CodeOp1.Enabled = true;
            Chk_CodeOp1.ToolTip = "";  
        }
        catch
        {
            lbl_weburlconnection.ForeColor = System.Drawing.Color.Red;
            lbl_weburlconnection.Text = "Not connect the website";
            Chk_CodeOp1.Enabled = false;
            Chk_CodeOp1.ToolTip = "there is no site by this name in iis kindly check wherther the website name written matches with the website name in iis";  
        }
        try
        {
            if (Directory.Exists(txtmastercodepathC.Text))
            {

            }
            else
            {
                Directory.CreateDirectory(txtmastercodepathC.Text);
            }
            lbl_mastercodepath.ForeColor = System.Drawing.Color.Green;
            lbl_mastercodepath.Text = "Connection Possible";
        }
        catch (Exception ex)
        {
            lbl_mastercodepath.ForeColor = System.Drawing.Color.Red;
            lbl_mastercodepath.Text = "Connection Not Possible";
        }

        try
        {
            if (Directory.Exists(txttemppathC.Text))
            {

            }
            else
            {
                Directory.CreateDirectory(txttemppathC.Text);
            }
            lbltemppathtest.ForeColor = System.Drawing.Color.Green;
            lbltemppathtest.Text = "Connection Possible";
        }
        catch (Exception ex)
        {
            lbltemppathtest.ForeColor = System.Drawing.Color.Red;
            lbltemppathtest.Text = "Connection Not Possible";
        }
        try
        {
            if (Directory.Exists(txtoutputsourcepathC.Text))
            {

            }
            else
            {
                Directory.CreateDirectory(txtoutputsourcepathC.Text);
            }
            lbloupputpathtest.ForeColor = System.Drawing.Color.Green;
            lbloupputpathtest.Text = "Connection Possible";
        }
        catch (Exception ex)
        {
            lbloupputpathtest.ForeColor = System.Drawing.Color.Red;
            lbloupputpathtest.Text = "Connection Not Possible";
        }
    }
    //-----------------------
    protected void btn_addremovablefolderC_Click(object sender, EventArgs e)
    {
        lBox_removablefolderC.Visible = true;
        btn_addremovefolderC.Visible = true;

        lBox_removablefolderC.Items.Add(txt_removablefolderC.Text);

        txt_removablefolderC.Text = "";
    }
    protected void btn_addremovefolderC_Click(object sender, EventArgs e)
    {
        lBox_removablefolderC.Items.Remove(lBox_removablefolderC.SelectedItem.Text);
        lBox_removablefolderC.SelectedIndex = lBox_removablefolderC.Items.Count - 1;
    }
    protected void btnEditremovefolderC_Click(object sender, EventArgs e)
    {
        if (btnEditremovefolderC.Text == "Edit")
        {
            txt_removablefolderC.Text = lBox_removablefolderC.SelectedItem.Text;
        }
        if (btnEditremovefolderC.Text == "Update")
        {
            lBox_removablefolderC.SelectedItem.Text = txt_removablefolderC.Text;
            //btnEdit.Text = "Edit";
        }

        if (btnEditremovefolderC.Text == "Update")
        {
            btnEditremovefolderC.Text = "Edit";
        }
        else
        {
            btnEditremovefolderC.Text = "Update";
        }
    }
    protected void btn_test_Click(object sender, EventArgs e)
    {
        GetServerMasterDetail(lbl_websiteurl.Text);
    }
    //--------------------------
    //-----------------------
    protected void btn_ADDdeletefolderC_Click(object sender, EventArgs e)
    {
        lst_box_deletefolderC.Visible = true;
        btn_RemovedeletefolderC.Visible = true;

        lst_box_deletefolderC.Items.Add(txtADDdeletefolderC.Text);
        txtADDdeletefolderC.Text = "";
    }
    protected void btn_RemovedeletefolderC_Click(object sender, EventArgs e)
    {
        lst_box_deletefolderC.Items.Remove(lst_box_deletefolderC.SelectedItem.Text);
        lst_box_deletefolderC.SelectedIndex = lst_box_deletefolderC.Items.Count - 1;
    }
    protected void btn_editdeletefolderC_Click(object sender, EventArgs e)
    {
        if (btn_editdeletefolderC.Text == "Edit")
        {
            txtADDdeletefolderC.Text = lst_box_deletefolderC.SelectedItem.Text;
        }
        if (btn_editdeletefolderC.Text == "Update")
        {
            lst_box_deletefolderC.SelectedItem.Text = txtADDdeletefolderC.Text;
            //btnEdit.Text = "Edit";
        }
        if (btn_editdeletefolderC.Text == "Update")
        {
            btn_editdeletefolderC.Text = "Edit";
        }
        else
        {
            btn_editdeletefolderC.Text = "Update";
        }
    }
    //-----------------------------------------------
    protected void btn_submitCode_Click(object sender, EventArgs e)
    {
          
        if (ddlcodetypecategory.SelectedValue == "1" )
        {

            if (CheckBox1.Checked == true)
            {
                SqlCommand sb = new SqlCommand("update ProductCodeDetailTbl set AdditionalPageInserted=0 where ProductId='" + ddlproductversion.SelectedValue + "' ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                sb.ExecuteNonQuery();
                con.Close();
            }
         

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("ProductCodeDetailTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            cmd.Parameters.AddWithValue("@ProductId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@CodeTypeName", txtcodetypename.Text);
            cmd.Parameters.AddWithValue("@AdditionalPageInserted", CheckBox1.Checked);
            cmd.Parameters.AddWithValue("@BusiwizSynchronization", CheckBox11.Checked);
            cmd.Parameters.AddWithValue("@CompanyDefaultData", CheckBox12.Checked);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);           
            cmd.ExecuteNonQuery();
            con.Close();

            string strcln1 = "  select Max(id) as id  from ProductCodeDetailTbl ";
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);
            //
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
             cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text);
            cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductCodeDetailId", dtcln1.Rows[0][0].ToString());     
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);           
            cmd.Parameters.AddWithValue("@Temppath", txttemppathC.Text);           
            cmd.Parameters.AddWithValue("@Outputpath", txtoutputsourcepathC.Text);           
            cmd.Parameters.AddWithValue("@WebsiteID", DDLWebsiteC.SelectedValue );
            cmd.Parameters.AddWithValue("@FileLocationPath", txtmastercodepathC.Text);
            cmd.Parameters.AddWithValue("@FileName", lblfilename.Text);
            cmd.Parameters.AddWithValue("@IsforDownloadonly", Chk_CodeOp3.Checked);               
            cmd.ExecuteNonQuery();
            con.Close();
            string strcln_CodeTypeTbl = "  select Max(id) as id  from ProductCodeDetailTbl ";
            SqlCommand cmdcln_CodeTypeTbl = new SqlCommand(strcln_CodeTypeTbl, con);
            DataTable dtcln_CodeTypeTbl = new DataTable();
            SqlDataAdapter adpcln_CodeTypeTbl = new SqlDataAdapter(cmdcln_CodeTypeTbl);
            adpcln_CodeTypeTbl.Fill(dtcln_CodeTypeTbl);

            if (chk_insertpagemaster.Checked == true)
            {
                if (Chk_CodeOp1.Checked == true)
                {
                    //  Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(txt_existingwebsite.Text, txtmastercodepathC.Text); 
                    try
                    {
                        DirectoryInfo diSource = new DirectoryInfo(txt_existingwebsite.Text);
                        DirectoryInfo diTarget = new DirectoryInfo(txtmastercodepathC.Text);
                        CopyAll(diSource, diTarget);
                    }
                    catch
                    {
                    }
                }
                if (Chk_CodeOp2.Checked == true)
                {

                }
                if (chk_insertpagemaster.Checked == true)
                {
                    //Syncronice Folder
                   // Insert_WebsiteSection(Convert.ToString(DDLWebsiteC.SelectedValue), "ComonSection", "0", lbl_websiteurl.Text, lbl_websiteurl.Text, txtmastercodepathC.Text, "", "", "");
                    MainFolder(txtmastercodepathC.Text);
                }
                if (txtmastercodepathC.Text != txt_existingwebsite.Text)
                {
                    //pnlPopup1.Visible = true;
                }

                lbl_codetypeid.Text = dtcln1.Rows[0]["Id"].ToString();
                FillTableGrid(DDLWebsiteC.SelectedValue);
                pnlfolder.Visible = true;
            }
        }       
        clear();
        fillgrid();
        Panel1.Visible = false;
        lblmsg.Visible = true;
        lblmsg.Text = "Record inserted successfully";
    }
   
    //public static void Copy(string sourceDirectory, string targetDirectory)
    //{
    //    DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
    //    DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

    //    CopyAll(diSource, diTarget);
    //}
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
            if (CheckBox1.Checked == true)
            {
                SqlCommand sb = new SqlCommand("update ProductCodeDetailTbl set AdditionalPageInserted=0 where ProductId='" + ddlproductversion.SelectedValue + "' ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                sb.ExecuteNonQuery();
                con.Close();
            }
         


            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("ProductCodeDetailTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Update");
            cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@CodeTypeName", txtcodetypename.Text);
            cmd.Parameters.AddWithValue("@AdditionalPageInserted", CheckBox1.Checked);
            cmd.Parameters.AddWithValue("@BusiwizSynchronization", CheckBox11.Checked);
            cmd.Parameters.AddWithValue("@CompanyDefaultData", CheckBox12.Checked);
            cmd.Parameters.AddWithValue("@ProductId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.ExecuteNonQuery();
            con.Close();         

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Update");
            cmd.Parameters.AddWithValue("@ProductCodeDetailId", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text);
            cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.Parameters.AddWithValue("@Temppath", txttemppathC.Text);
            cmd.Parameters.AddWithValue("@Outputpath", txtoutputsourcepathC.Text);
            cmd.Parameters.AddWithValue("@WebsiteID", DDLWebsiteC.SelectedValue);
            cmd.Parameters.AddWithValue("@FileLocationPath", txtmastercodepathC.Text);
            cmd.Parameters.AddWithValue("@FileName", lblfilename.Text);
            cmd.Parameters.AddWithValue("@IsforDownloadonly", Chk_CodeOp3.Checked);     
            cmd.ExecuteNonQuery();
            con.Close();

            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmd = new SqlCommand("ProductCodeVersionDetailDeleteFolder_AddDelUpdtSelect", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@StatementType", "Delete");
            //cmd.Parameters.AddWithValue("@ProductCodeVersionDetailID", ViewState["id"].ToString());
            //cmd.ExecuteNonQuery();
            //con.Close();

            lbl_codetypeid.Text = ViewState["id"].ToString();
            FillTableGrid(DDLWebsiteC.SelectedValue);
            pnlfolder.Visible = true;
           
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
         DataTable dtsvr = selectBZ(" SELECT DISTINCT dbo.ProductCodeDetailTbl.Id,dbo.ProductCodeDetailTbl.Active,dbo.CodeTypeTbl.Temppath,dbo.CodeTypeTbl.Outputpath,dbo.CodeTypeTbl.WebsiteID,dbo.CodeTypeTbl.FileLocationPath,dbo.CodeTypeTbl.FileName,dbo.ProductCodeDetailTbl.Id as vv, dbo.ProductCodeDetailTbl.CodeTypeName , dbo.ProductMaster.ProductName, dbo.ProductMaster.ProductName + ':' + dbo.VersionInfoMaster.VersionInfoName AS VersionInfoName, dbo.CodeTypeCategory.CodeTypeCategory, dbo.ProductCodeDetailTbl.AdditionalPageInserted, dbo.ProductCodeDetailTbl.BusiwizSynchronization, dbo.ProductCodeDetailTbl.CompanyDefaultData,dbo.WebsiteMaster.WebsiteName FROM dbo.WebsiteMaster INNER JOIN dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.CodeTypeTbl.ProductVersionId INNER JOIN dbo.ProductMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ProductCodeDetailTbl.Id = dbo.CodeTypeTbl.ProductCodeDetailId ON dbo.WebsiteMaster.ID = dbo.CodeTypeTbl.WebsiteID where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' " + str + "");
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
            CheckBox1.Checked = Convert.ToBoolean(dtcln.Rows[0]["AdditionalPageInserted"].ToString());
            CheckBox11.Checked = Convert.ToBoolean(dtcln.Rows[0]["BusiwizSynchronization"].ToString());
            CheckBox12.Checked = Convert.ToBoolean(dtcln.Rows[0]["CompanyDefaultData"].ToString());
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
            fillcodetypecategory();
            ddlcodetypecategory.SelectedValue = dtcln1.Rows[0]["CodeTypeCategoryId"].ToString();
            ddlcodetypecategory.Enabled = false;
            
            txttemppathC.Text =dtcln1.Rows[0]["Temppath"].ToString();
            txtoutputsourcepathC.Text =dtcln1.Rows[0]["Outputpath"].ToString();
             strcln = " SELECT distinct ID, WebsiteName,WebsiteUrl From WebsiteMaster Where VersionInfoId=" + ddlproductversion.SelectedValue + " and (id="+dtcln1.Rows[0]["WebsiteID"].ToString()+" OR id NOT IN( Select WebsiteID as id From CodeTypeTbl Where WebsiteID !='' ))";//and id NOT IN( Select WebsiteID as id From ProductCodeVersionDetail)
             cmdcln = new SqlCommand(strcln, con);
             dtcln = new DataTable();
             adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            DDLWebsiteC.DataSource = dtcln;
            DDLWebsiteC.DataValueField = "ID";
            DDLWebsiteC.DataTextField = "WebsiteName";
            DDLWebsiteC.DataBind();
            DDLWebsiteC.Items.Insert(0, "--Select--");
            DDLWebsiteC.Items[0].Value = "0";
            DDLWebsiteC.SelectedIndex = 0;
            DDLWebsiteC.SelectedValue  =dtcln1.Rows[0]["WebsiteID"].ToString();
            DDLWebsiteC_SelectedIndexChanged(sender ,e);
            txtmastercodepathC.Text =dtcln1.Rows[0]["FileLocationPath"].ToString();
            lblfilename.Text = dtcln1.Rows[0]["FileName"].ToString();
            try
            {
                Chk_CodeOp1.Enabled = false;
                Chk_CodeOp1.Checked = false;
                Boolean download = Convert.ToBoolean(dtcln1.Rows[0]["IsforDownloadonly"].ToString());
                if (download == true)
                {
                    Chk_CodeOp3.Checked = true;
                }
                else
                {
                    Chk_CodeOp2.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Chk_CodeOp2.Checked = true;
            }
            try
            {
                SyncroniceFolder(txtmastercodepathC.Text);
            }
            catch
            {
            }
            //-----------------------------------------------------------          
            //------------------------


            string str122 = "select * from Websitemaster_Information where websiteid='" + dtcln1.Rows[0]["Id"].ToString() + "' ";
            SqlCommand cmd12 = new SqlCommand(str122, con);
            SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
            DataTable ds12 = new DataTable();
            adp12.Fill(ds12);
            {
                GridView2.DataSource = ds12;
                GridView2.DataBind();

            }

           
            //---------------------------
            //---------------------------------------------------------
            //-----------------------------------------------------------          
            //------------------------
           
            //---------------------------
            //---------------------------------------------------------
        }
        pnl_selectradio.Visible = true;
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
    private bool isValidConnection(string url, string user, string password, string port)
    {
        try
        {
            string[] separator1 = new string[] { "/" };
            string[] strSplitArr1 = url.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            String productno = strSplitArr1[0].ToString();
            string ftpurl = "";

            if (productno == "FTP:" || productno == "ftp:")
            {
                if (strSplitArr1.Length >= 3)
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(port);
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(port);

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(port);
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(port);

                }

            }
            string ftphost = ftpurl;



            // FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftphost);

            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(user, password);
            request.GetResponse();
        }
        catch (WebException ex)
        {
            return false;
        }
        return true;
    }
    //-------Upload Code /////*******************************************************************************************
    protected DataTable select(string std)
    {
        SqlDataAdapter cidco = new SqlDataAdapter(std, con);
        DataTable dts = new DataTable();
        cidco.Fill(dts);
        return dts;
    }
    protected void btn_upload_mastercode_Click(object sender, EventArgs e)
    {
        lblfilename.Text = fu_mastercode.FileName;
        bool valid = extOnlyzip(fu_mastercode.FileName);
        if (valid == true)
        {
            if (fu_mastercode.HasFile == true)
            {
                if (Directory.Exists(txtmastercodepathC.Text) == false)
                {
                    //Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                    Directory.CreateDirectory(txtmastercodepathC.Text);
                }
                // fu_mastercode.PostedFile.SaveAs(Server.MapPath("~\\images\\") + fu_mastercode.FileName);
                try
                {
                    if (File.Exists(txtmastercodepathC.Text + "\\" + fu_mastercode.FileName))
                    {
                        File.Delete(txtmastercodepathC.Text + "\\" + fu_mastercode.FileName);
                    }
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.ToString();
                }
                fu_mastercode.PostedFile.SaveAs(txtmastercodepathC.Text + "\\" + fu_mastercode.FileName);


                using (ZipFile zip = ZipFile.Read(txtmastercodepathC.Text + "\\" + fu_mastercode.FileName))
                {
                    zip.ExtractAll(txtmastercodepathC.Text, ExtractExistingFileAction.OverwriteSilently);
                }
                try
                {
                    File.Delete(txtmastercodepathC.Text + "\\" + fu_mastercode.FileName);
                }
                catch
                {
                }
                lbl_fileupload.Text = "code uploaded sucessfully";
            }
        }
        else
        {
            lbl_fileupload.Text = "only allow .zip file";
        }
    }
    //Folder Selection --------------------------------------
    //--Grid Popup--
    //-***********Table Grid web
    protected void FillTableGrid(string websiteid)
    {
       // lbl_databaseid.Text = databaseid;
        string strcln = " SELECT id ,foldername,Case when(ID IS NULL) then  cast ('1' as bit) else  cast('0' as bit) end as chk From Websitemaster_Information where websiteid='" + websiteid + "' ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        gv_webfolder.DataSource = dtcln;
        gv_webfolder.DataBind();
    }
    protected void ch1table_chachedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow item in gv_webfolder.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void gv_webfolder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblfolderid = (Label)e.Row.FindControl("lblfolderid");
            CheckBox cbItem = (CheckBox)(e.Row.FindControl("cbItem"));
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            string strwebsite = " SELECT * From ProductCodeVersionDetailDeleteFolder Where  Websitemaster_Information_ID=" + lblfolderid.Text + " and CodeTypeID="+ lbl_codetypeid.Text+"  ";
            SqlCommand cmd12web = new SqlCommand(strwebsite, con);
            SqlDataAdapter adp12web = new SqlDataAdapter(cmd12web);
            DataTable ds12web = new DataTable();
            adp12web.Fill(ds12web);
            if (ds12web.Rows.Count == 0)
            {
                cbItem.Checked = false;
            }
            else
            {
                cbItem.Checked = true;
            }
        }
    }
   
    protected void gv_webfolder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void gv_webfolder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_webfolder.PageIndex = e.NewPageIndex;
        FillWebsiteGrid();
    }
    protected void gv_webfolder_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
    }
    protected void gv_webfolder_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btnnext_Click(object sender, EventArgs e)
    {
      //  ProductDatabaseRequiredDataInTable();
        UpdateFolder();
        int i = gv_webfolder.PageIndex + 1;
        if (i == gv_webfolder.PageCount)
        {
            btnprevious.Visible = false;
            btnnext.Visible = false;
            btn_finish.Visible = true;
        }
        else
        {
            gv_webfolder.PageIndex = i;
           // FillTableGrid(lbl_databaseid.Text);
        }
      
    }
    protected void btnprevious_Click(object sender, EventArgs e)
    {
      
    }
    protected void UpdateFolder()
    {
        foreach (GridViewRow item in gv_webfolder.Rows)
        {
            Label lblfoldername = (Label)(item.FindControl("lblfoldername"));
            Label lblfolderid = (Label)(item.FindControl("lblfolderid"));
            CheckBox cbItem = (CheckBox)(item.FindControl("cbItem"));          
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                string strwebsite = " SELECT ID as ID From ProductCodeVersionDetailDeleteFolder Where  Websitemaster_Information_ID=" + lblfolderid.Text + " and CodeTypeID=" + lbl_codetypeid.Text + " and VersionDeleteFolderPath='"+ lblfoldername.Text+"' ";
                SqlCommand cmd12web = new SqlCommand(strwebsite, con);
                SqlDataAdapter adp12web = new SqlDataAdapter(cmd12web);
                DataTable ds12web = new DataTable();
                adp12web.Fill(ds12web);
                if (ds12web.Rows.Count == 0 && cbItem.Checked == true)
                {
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("ProductCodeVersionDetailDeleteFolder_AddDelUpdtSelect", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StatementType", "Insert");
                    cmd.Parameters.AddWithValue("@CodeTypeID", lbl_codetypeid.Text);
                    cmd.Parameters.AddWithValue("@VersionDeleteFolderPath", lblfoldername.Text);
                    cmd.Parameters.AddWithValue("@Websitemaster_Information_ID", lblfolderid.Text);
                    cmd.Parameters.AddWithValue("@DeleteAll_Subfolder", true);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else if (ds12web.Rows.Count >0 && cbItem.Checked == false)
                {
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("ProductCodeVersionDetailDeleteFolder_AddDelUpdtSelect", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StatementType", "Deleteusing2id");
                    cmd.Parameters.AddWithValue("@ID", ds12web.Rows[0]["ID"].ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();            
                }
            // SyncroTablessFieldAll(lbl_databaseid.Text, lbltblname.Text, lbltblid.Text);
        }
        int i = gv_webfolder.PageCount;
        if (gv_webfolder.PageIndex > 0)
        {
            gv_webfolder.PageIndex = gv_webfolder.PageIndex - 1;
            // FillTableGrid(DDLWebsiteC.SelectedValue);
        }
    }
    protected void btn_finish_Click(object sender, EventArgs e)
    {
        UpdateFolder();
        pnlfolder.Visible = false;
        lblmsg.Text = "Successfully Synchronise tables and field in client database";
        lblfinishmsg.Visible = true;
       // lbl_serverattach.Text = "";
        try
        {
            if (Chk_CodeOp2.Checked == true)
            {
                CreateWebSite(lbl_websiteurl.Text, lbl_websiteurl.Text, lbl_websiteurl.Text, txtmastercodepathC.Text, lbl_websiteurl.Text, lbl_websiteurl.Text);
            }
        }
        catch
        {
        }
        pnlfolder.Visible = false;
    }
    protected void insertinFolder()
    {
        int ii = 0;
        while (ii < lBox_removablefolderC.Items.Count)
        {
           
        }
    }
    protected void Btn_pnlbackupstatus_Click(object sender, EventArgs e)
    {
        pnlfolder.Visible = false;
    }
    //***************************
    //Close Popup   


    //*********************************************
    
    
    public bool extOnlyzip(string filename)
    {
        string[] validFileTypes = { ".zip", "zip", "Zip" };

        string ext = System.IO.Path.GetExtension(filename);

        bool isValidFile = true;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + filename)
            {

                isValidFile = true;

                break;

            }

        }
        return isValidFile;
    }
    public bool ext111(string filename)
    {
        string[] validFileTypes = { ".zip", "zip", "Zip" };

        string ext = System.IO.Path.GetExtension(filename);

        bool isValidFile = true;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + filename)
            {

                isValidFile = true;

                break;

            }

        }
        return isValidFile;
    }

    //***************************************************************
    //****************************************************************
    public string CreateWebSite(string hostname, string websitehostname, string webSiteName, string PhysicalPath, string username, string password)
    {
        
       

        string HostHeader = hostname;
        string hostheader2 = websitehostname;
        string DefaultDoc = "Default.aspx";
        string appPoolName = "";
        if (hostname.Length > 0)
        {
            appPoolName = hostname;

        }
        else
        {
            appPoolName = "Default";
        }
        string errorMessage = "";
        DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
      

        // Find unused ID value for new web site
        int siteID = 1;
        int siteID1 = 1;


        foreach (DirectoryEntry e in root.Children)
        {
            if (e.SchemaClassName == "IIsWebServer")
            {
                int ID = Convert.ToInt32(e.Name);

                if (ID == siteID)
                {
                    siteID = siteID + 1;
                    //   break;
                }
                else
                {
                    try
                    {
                        DirectoryEntry sites = (DirectoryEntry)root.Invoke("Create", "IIsWebServer", siteID);
                        break;
                    }
                    catch (Exception ex)
                    {
                        siteID = siteID + 1;
                    }
                    //   break;
                }
            }
            else if (e.SchemaClassName == "IIsApplicationPools")
            {
                //appPoolName = e.Name;
            }
        }
        //------------------

        //--------------------
        ViewState["siteID"] = siteID;
        DirectoryEntry site = (DirectoryEntry)root.Invoke("Create", "IIsWebServer", siteID);
        site.Invoke("Put", "ServerComment", webSiteName);
        site.Invoke("Put", "KeyType", "IIsWebServer");
        string PortNumber = "80";

        site.Invoke("Put", "ServerBindings", ":" + PortNumber + ":" + HostHeader);

        //site.Invoke("Add", "ServerBindings", ":" + PortNumber + ":" + HostHeader);

        // site.Invoke("Add", "ServerBindings", ":" + PortNumber + ":" + hostheader2); // add on 842015

        site.Invoke("Put", "ServerState", 2);
        site.Invoke("Put", "FrontPageWeb", 1);
        site.Invoke("Put", "DefaultDoc", DefaultDoc);
        site.Invoke("Put", "ServerAutoStart", 1);
        site.Invoke("Put", "ServerSize", 1);
        site.Invoke("SetInfo");

        DirectoryEntry siteVDir = site.Children.Add("Root", "IISWebVirtualDir");
        if (appPoolName != "")
        {
            object[] param = { 0, appPoolName, true };
            siteVDir.Invoke("AppCreate3", param);
        }
        siteVDir.Properties["AppIsolated"][0] = 2;
        siteVDir.Properties["Path"][0] = PhysicalPath;
        siteVDir.Properties["AccessFlags"][0] = 513;
        siteVDir.Properties["AspEnableParentPaths"][0] = true;
        siteVDir.Properties["AppFriendlyName"][0] = webSiteName;
        siteVDir.Properties["FrontPageWeb"][0] = 1;
        siteVDir.Properties["AppRoot"][0] = "LM/W3SVC/" + siteID + "/Root";
        siteVDir.Properties["AppFriendlyName"][0] = "Root";
        siteVDir.Properties["AspSessionTimeout"][0] = "60";
        siteVDir.Properties["AuthFlags"].Value = 4;//integrity windows Authentication checked
        siteVDir.Properties["AuthAnonymous"][0] = true;//Anonymouse uncheck
        siteVDir.Properties["HttpErrors"].Add("401,1,FILE," + PhysicalPath + "/Lib/CustomError/SSOLoginError.htm");
        siteVDir.Properties["HttpErrors"].Add("401,2,FILE," + PhysicalPath + "/Lib/CustomError/SSOLoginError.htm");
        siteVDir.Properties["HttpErrors"].Add("401,3,FILE," + PhysicalPath + "/Lib/CustomError/SSOLoginError.htm");
        siteVDir.Properties["HttpErrors"].Add("401,4,FILE," + PhysicalPath + "/Lib/CustomError/SSOLoginError.htm");
        siteVDir.Properties["HttpErrors"].Add("401,5,FILE," + PhysicalPath + "/Lib/CustomError/SSOLoginError.htm");
        siteVDir.Properties["HttpErrors"].Add("401,7,FILE," + PhysicalPath + "/Lib/CustomError/SSOLoginError.htm");

        //For SSO, Set special settings for  WinLogin.aspx page -- This has beed added after version 8.1.1001
        DirectoryEntry deLoginDir;
        deLoginDir = siteVDir.Children.Add("WinLogin.aspx", siteVDir.SchemaClassName);
        deLoginDir.Properties["AuthAnonymous"][0] = false;//Anonymouse uncheck
        deLoginDir.Properties["AuthFlags"].Value = 4;//integrity windows Authentication checked
        deLoginDir.CommitChanges();
        ////////////////////////////////////////////

        siteVDir.CommitChanges();
        siteVDir.Invoke("AppDelete");
        siteVDir.Invoke("AppCreate", true);
        siteVDir.Invoke("AppEnable");
        site.CommitChanges();

        #region AssignApplicationPool

        DirectoryEntry vDir = new DirectoryEntry("IIS://localhost/W3SVC/" + siteID.ToString() + "/Root");
        string className = vDir.SchemaClassName.ToString();
        if (className.EndsWith("VirtualDir"))
        {
            object[] param = { 0, appPoolName, true };
            vDir.Invoke("AppCreate3", param);
            vDir.Properties["AppIsolated"][0] = "2";
            vDir.CommitChanges();
        }
        else
        {
            return "-1";
        }

        if (Environment.OSVersion.Version.Major < 6)
        {
            try
            {
                const string aspNetV1 = "1.0.3705";
                const string aspNetV11 = "1.1.4322";
                const string aspNetV2 = "2.0.50727";
                const string aspNetV4 = "4.0.30319";
                const string targetAspNetVersion = aspNetV4;

                //loop through the script maps
                for (var i = 0; i < siteVDir.Properties["ScriptMaps"].Count; i++)
                {
                    //replace the versions if they exists
                    siteVDir.Properties["ScriptMaps"][i] = siteVDir.Properties["ScriptMaps"][i].ToString().Replace(aspNetV1, targetAspNetVersion);
                    siteVDir.Properties["ScriptMaps"][i] = siteVDir.Properties["ScriptMaps"][i].ToString().Replace(aspNetV11, targetAspNetVersion);
                    siteVDir.Properties["ScriptMaps"][i] = siteVDir.Properties["ScriptMaps"][i].ToString().Replace(aspNetV2, targetAspNetVersion);
                    siteVDir.Properties["ScriptMaps"][i] = siteVDir.Properties["ScriptMaps"][i].ToString().Replace(aspNetV4, targetAspNetVersion);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errorMessage = ex.Message + Environment.NewLine + ex.StackTrace;
            }
        }
        else
        {
            string appPoolPath = @"IIS://localhost/W3SVC/AppPools/" + appPoolName;
            try
            {
                var appPoolEntry = new DirectoryEntry(appPoolPath);
                appPoolEntry.Properties["managedRuntimeVersion"].Value = "v4.0";


                appPoolEntry.InvokeSet("AppPoolIdentityType", new Object[] { 3 });
                appPoolEntry.InvokeSet("WAMUserName", new Object[] { username });

                // Configure password for the AppPool with above specified password                       
                appPoolEntry.InvokeSet("WAMUserPass", new Object[] { password });
                appPoolEntry.Invoke("SetInfo", null);

                //appPoolEntry.Username = username;
                //appPoolEntry.Password = password;
                appPoolEntry.CommitChanges();
                appPoolEntry.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errorMessage = ex.Message + Environment.NewLine + ex.StackTrace;
            }
            siteVDir.CommitChanges();
            siteVDir.Close();
        }

        #endregion
        return siteID.ToString();
    }

   
   
    //*******
   

    //--------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------
    protected void MainFolder(string targetDirectory)
    {
        
        int websiteid=Convert.ToInt32(DDLWebsiteC.SelectedValue);        
        str = "";
        int index=1;
        string[] fileEntriesMain = Directory.GetDirectories(targetDirectory);
        DirectoryInfo DirectoryMain = new DirectoryInfo(targetDirectory);
        //Insert_Websitemaster_Information(websiteid, targetDirectory, "");
        FileInfo[] FilesMainGet = DirectoryMain.GetFiles("*.aspx");
        foreach (FileInfo filenamemain in FilesMainGet)
        {
            string pagepathafteerweb = targetDirectory.Replace(txtmastercodepathC.Text.Replace(@"\\", @"\"), "");
            //str = str + "," + filenamemain.Name;
          
           // Insert_PageMaster("0", filenamemain.Name, filenamemain.Name, filenamemain.Name, Convert.ToString(index), Convert.ToString(ViewState["verid"]), menuid, pagepathafteerweb, submenuid, "1", "0", "0", true, true, false, false);            
            index++;
        }
        foreach (string fileNameDirectMain in fileEntriesMain)
        {
            string ffmain = fileNameDirectMain;
            string pagepathafteerweb = ffmain.Replace(txtmastercodepathC.Text.Replace(@"\\", @"\"), "");
           // Insert_Websitemaster_Information(websiteid, "", "", pagepathafteerweb);
            MainFolder(ffmain);
        }
    }
    protected void Insert_Websitemaster_Information(int websiteid, string websitepath, string filename, string foldername)
    {
       
        DataTable dtcln1 = selectBZ(" select * from Websitemaster_Information where websiteid='" + websiteid + "' and foldername='" + foldername + "'  ");
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
   
   

    //
 





    //-***********Website Grid web

    protected void SyncroniceFolder(string targetDirectory)
    {

        int websiteid = Convert.ToInt32(DDLWebsiteC.SelectedValue);
        str = "";
        int index = 1;
        string[] fileEntriesMain = Directory.GetDirectories(targetDirectory);
        DirectoryInfo DirectoryMain = new DirectoryInfo(targetDirectory);
        //Insert_Websitemaster_Information(websiteid, targetDirectory, "");
        FileInfo[] FilesMainGet = DirectoryMain.GetFiles("*.aspx");
        foreach (FileInfo filenamemain in FilesMainGet)
        {
            string pagepathafteerweb = targetDirectory.Replace(txtmastercodepathC.Text.Replace(@"\\", @"\"), "");
            //str = str + "," + filenamemain.Name;
            //Insert_Websitemaster_Information(websiteid, pagepathafteerweb, filenamemain.Name);
            
            index++;
        }
        foreach (string fileNameDirectMain in fileEntriesMain)
        {
            string ffmain = fileNameDirectMain;
            string pagepathafteerweb = ffmain.Replace(txtmastercodepathC.Text.Replace(@"\\", @"\"), "");
            
            Insert_Websitemaster_Information(websiteid, "", "", pagepathafteerweb);
            SyncroniceFolder(ffmain);
        }
    }
    protected void FillWebsiteGrid()
    {
        string strsearch = "";       
        string strcln = " SELECT * From Websitemaster_Information  Where websiteid= " + DDLWebsiteC.SelectedValue + "  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView2.DataSource = dtcln;
        GridView2.DataBind();
    }
    protected void ch1_chachedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow item in GridView2.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        FillWebsiteGrid();
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillWebsiteGrid();
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

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




    protected void Button4_Click(object sender, EventArgs e)
    {
        CheckBox11.Checked = false;
        CheckBox12.Checked = false;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ViewState["changedata"] = "1";
    }    

    protected void CheckBox11_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox11.Checked == true)
        {
            CheckBox12.Checked = false;
            CheckBox1.Checked = false;
            string strcln1 = "  select *   from ProductCodeDetailTbl where  BusiwizSynchronization=1 and ProductId='" + ddlproductversion.SelectedValue + "'";
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);
            if (dtcln1.Rows.Count > 0)
            {
                ModernpopSync.Show();
                Label6.Text = "For one product only one database can be set as busicontroller database, there is already another database code set as Busicontroller database " + dtcln1.Rows[0]["CodeTypeName"].ToString() + " would you wish to change the Busicontroller data base to " + txtcodetypename.Text + "";
            }
        }
    }
    protected void CheckBox12_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox12.Checked == true)
        {
            CheckBox11.Checked = false;
            CheckBox1.Checked = false;
            string strcln1 = "  select *   from ProductCodeDetailTbl where  CompanyDefaultData=1 and ProductId='" + ddlproductversion.SelectedValue + "'";
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);
            if (dtcln1.Rows.Count > 0)
            {
                ModernpopSync.Show();
                Label6.Text = "There is another database code type name " + dtcln1.Rows[0]["CodeTypeName"].ToString() + "  is selected as main applciation database, Would you like to change that to this new selction ?";
            }
        }
    }
}