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

public partial class ShoppingCart_Admin_CompanyWebsitMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    bool gg;
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!IsPostBack)
        {
            Label1.Text = "";      
            ViewState["sortOrder"] = "";
            FillProduct();
            ddlproductname_SelectedIndexChanged(sender, e);
            filldata();
           

            FillProductSearch();
            fillgriddata();
            fillDNS();
        }
        
    }
    public void clearall()
    {
        txtSiteName.Text = "";
        txtWebUrl.Text = "";
        txtWebsitePort.Text = "";

        txtIISServerIP.Text = "";
        txtServerUserId.Text = "";
        txtServerPw.Text = "";
        txtServerPw.Attributes.Clear();
        txt_codepath.Text = "";
        txt_IISWebsiteFolderName.Text = "";

        txtDatabaseName.Text = "";
        txtDatabaseServerurl.Text = "";
        txtDBUserId.Text = "";
        txtDBPassword.Text = "";
        txtDBPassword.Attributes.Clear();

        txtBusicontrollerName.Text = "";
        txtBusiDatabaseName.Text = "";
        txtBusiServerUrl.Text = "";
        txtBusiUserId.Text = "";
        txtBusipassword.Text = "";
        txtBusipassword.Attributes.Clear();

        txtBusiconnectionString.Text = "";
        txtFtpUrl.Text = "";
        txtFtpPort.Text = "";
        txtFtpUserId.Text = "";
        txtFtpPassword.Attributes.Clear();
        txtFtpPassword.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        Label1.Text = "";
        txtBusiController.Text = "";
        txtDatabaseAccessPort.Text = "";
        txtISSPort.Text = "";
        ddlProductname.SelectedIndex = 0;

        txtFtpWorkGuidePassword.Attributes.Clear();
        txtFtpWorkGuidePort.Text = "";
        txtFTPWorkGuideUrl.Text = "";
        txtFtpWorkGuideUserId.Text = "";
        BtnSubmit.Visible = true;
        BtnUpdate.Visible = false;
    }

    protected void FillProduct()
    {
        string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId, ProductMaster.ProductName,ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='1' and ProductDetail.Active='1'  order  by  ProductMaster.ProductName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "ProductName";
        ddlProductname.DataBind();
        //ddlProductname.Items.Insert(0, "-Select-");
        //ddlProductname.Items[0].Value = "0";
        filldata();
      
        
    }
    protected void ddlproductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpath();
        Fillserverddl() ;



    }
    protected void filldata()
    {

       
    DataTable dtcln = selectBZ("select ClientMaster.serverid from ClientMaster inner join ProductMaster on ClientMaster.ClientMasterId =ProductMaster.ClientMasterId inner join  dbo.VersionInfoMaster  ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId where VersionInfoId='" + ddlProductname.SelectedValue + "' ");
     if (dtcln.Rows.Count > 0)
     {

         int value = Convert.ToInt32(dtcln.Rows[0]["serverid"]);
         ViewState["serverid"] = value;
         DataTable dts = selectBZ("select ServerMasterTbl.ServerName,ServerMasterTbl.PublicIp,ServerMasterTbl.Ipaddress,ServerMasterTbl.serverdefaultpathforiis from  ServerMasterTbl where ServerMasterTbl.Id='" + value + "' ");
          if (dts.Rows.Count > 0)
        {


            Label131.Text = dts.Rows[0]["ServerName"].ToString();

            Label132.Text = dts.Rows[0]["PublicIp"].ToString();

            Label147.Text = dts.Rows[0]["Ipaddress"].ToString();
            Label128.Text = dts.Rows[0]["serverdefaultpathforiis"].ToString();
             
              //Label128.Text=dtcln.Rows[0]["serverid"].ToString();
          }
          if (ViewState["serverid"] != "")
          {
             // ddlserverMas.SelectedItem.Value = ViewState["serverid"].ToString();
              Fillserverddl();
          }
         

     }
    
    }



    protected void fillpath()
    {
        DataTable dtcln = selectBZ("SELECT  dbo.ProductMaster.Description,dbo.ProductMaster.ProductName,dbo.VersionInfoMaster.ClientFTPRootPath, dbo.VersionInfoMaster.VersionInfoId, dbo.VersionInfoMaster.VersionInfoName, dbo.VersionInfoMaster.ProductId, dbo.VersionInfoMaster.Active, dbo.VersionInfoMaster.ProductURL, dbo.VersionInfoMaster.PricePlanURL, dbo.VersionInfoMaster.MasterCodeSourcePath, dbo.VersionInfoMaster.TemporaryPublishPath, dbo.VersionInfoMaster.DestinationPath FROM dbo.VersionInfoMaster INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId where VersionInfoId='" + ddlProductname.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            // txtsourcepath.Text = dtcln.Rows[0]["MasterCodeSourcePath"].ToString();
            // txttemppath.Text = dtcln.Rows[0]["TemporaryPublishPath"].ToString();
            // txtoutputsourcepath.Text = dtcln.Rows[0]["DestinationPath"].ToString();
            //  txtsourcepath.Text = dtcln.Rows[0]["ClientFTPRootPath"].ToString() + "\\" + dtcln.Rows[0]["ProductName"].ToString();

            txt_codepath.Text = Label128.Text + "\\" + Session["ClientId"] + "\\" + dtcln.Rows[0]["ProductName"].ToString();
        }
    }



    protected void fillDNS()
    {
        DataTable dtcln = selectBZ(" select * from ServerMasterTbl where DNS='1' ");
        if (dtcln.Rows.Count > 0)
        {
            DropDownList2.DataSource = dtcln;
            DropDownList2.DataTextField = "ServerName";
            DropDownList2.DataValueField = "Id";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "-Select-");
            DropDownList2.Items[0].Value = "0";
        }
    }



    protected void Fillserverddl()
    {
        string strcln = " SELECT distinct ServerName,Id FROM ServerMasterTbl where Status='1' order  by ServerName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlserverMas.DataSource = dtcln;
        ddlserverMas.DataTextField = "ServerName";
        ddlserverMas.DataValueField = "Id";
        ddlserverMas.DataBind();
        ddlserverMas.Items.Insert(0, "-Select-");
        ddlserverMas.Items[0].Value = "0";

        DropDownList1.DataSource = dtcln;
        DropDownList1.DataTextField = "ServerName";
        DropDownList1.DataValueField = "Id";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";

       
    }    
    protected void ddlserverMas_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strftpdetail = " SELECT * from ServerMasterTbl where Id='" + ddlserverMas.SelectedValue + "'";
        SqlCommand cmdftpdetail = new SqlCommand(strftpdetail, con);
        DataTable dtftpdetail = new DataTable();
        SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
        adpftpdetail.Fill(dtftpdetail);
        if (dtftpdetail.Rows.Count > 0)
        {
            string serversqlserverip = dtftpdetail.Rows[0]["sqlurl"].ToString();
            string serversqlinstancename = dtftpdetail.Rows[0]["DefaultsqlInstance"].ToString();
            string serversqldbname = dtftpdetail.Rows[0]["DefaultDatabaseName"].ToString();
            string serversqlpwd = dtftpdetail.Rows[0]["Sapassword"].ToString();
            string serversqlport = dtftpdetail.Rows[0]["port"].ToString();
            SqlConnection connserver = new SqlConnection();           
            try
            {
                connserver.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";
                connserver.Open(); 
                lbl_weburlconnection.Text = "Connection successful";
                lbl_weburlconnection.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lbl_weburlconnection.Text = "Connection unsuccessfull";
                lbl_weburlconnection.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void FillProductSearch()
    {       
        string activestr = "";
        
            
        activestr = " and  ";
        

        //string strcln = " SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  order  by productversion";
        
       // strcln = " SELECT dbo.VersionInfoMaster.ProductId, dbo.ProductMaster.ProductName FROM dbo.VersionInfoMaster INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId WHERE   VersionInfoMaster.Active ='True'  and (dbo.VersionInfoMaster.VersionInfoId IN (SELECT DISTINCT VersionInfoId FROM dbo.WebsiteMaster))";
        string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId,dbo.VersionInfoMaster.ProductId, ProductMaster.ProductName,ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='1' and ProductDetail.Active='1'  order  by  ProductMaster.ProductName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        Ddlproduct_search.DataSource = dtcln;
        Ddlproduct_search.DataValueField = "ProductId";
        Ddlproduct_search.DataTextField = "ProductName";
        Ddlproduct_search.DataBind();
        Ddlproduct_search.Items.Insert(0, "-Select-");
        Ddlproduct_search.Items[0].Value = "0";

    }
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        fillgriddata();

    }
    protected void txtWebUrl_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var ping = new System.Net.NetworkInformation.Ping();
            var result = ping.Send(""+txtWebUrl.Text+"");
            if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
            {
                lbl_weburlconnection.Text = "Connection successful";
                lbl_weburlconnection.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lbl_weburlconnection.Text = "Connection unsuccessfull";
                lbl_weburlconnection.ForeColor = System.Drawing.Color.Red;
            }                
        }
        catch
        {
            lbl_weburlconnection.Text = "Connection unsuccessfull";
            lbl_weburlconnection.ForeColor = System.Drawing.Color.Red;
        }
        txtVersionfolderpath.Text = txtWebUrl.Text + '/' + txt_IISVersionFolderPath.Text;
      
    }
    protected void txt_IISWebsiteFolderName_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (!Directory.Exists(txt_codepath.Text + '\\' + txt_IISWebsiteFolderName.Text))
            {
                Directory.CreateDirectory(txt_codepath.Text + '\\' + txt_IISWebsiteFolderName.Text);
                lbl_webfoldertest.Text = "Path is available";
                lbl_webfoldertest.ForeColor = System.Drawing.Color.Green;
                //+ txt_IISWebsiteFolderName.Text
               
            }
        }
        catch
        {
            lbl_webfoldertest.Text = "Path not exist";
            lbl_webfoldertest.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void txt_IISVersionFolderPath_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (!Directory.Exists(txt_codepath.Text + '\\' + txt_IISWebsiteFolderName.Text))
            {
                Directory.CreateDirectory(txt_codepath.Text + '\\' + txt_IISWebsiteFolderName.Text);
                lbl_versionfoldertest.Text = "Path is available";
                lbl_versionfoldertest.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch
        {
            lbl_versionfoldertest.Text = "Path not exist";
            lbl_versionfoldertest.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

        string str22 = "  Select WebsiteMaster.* from WebsiteMaster where WebsiteMaster.WebsiteName='" + txtSiteName.Text + "' and WebsiteMaster.VersionInfoId='" + ddlProductname.SelectedValue + "'";

        SqlCommand cmd2 = new SqlCommand(str22,con);
        con.Open();
        SqlDataReader rdr= cmd2.ExecuteReader();
       if (rdr.Read())
       {
           Label1.Text = "You can't add same Website name in the same Version of the Product";
           con.Close();
       }
           
       else
       {
           try
           {


               SqlCommand mycmd = new SqlCommand("WebsiteMaster_AddDelUpdtSelect", con);
               mycmd.CommandType = CommandType.StoredProcedure;
               mycmd.Parameters.AddWithValue("@StatementType", "Insert");
               mycmd.Parameters.AddWithValue("@WebsiteName", txtSiteName.Text);
               mycmd.Parameters.AddWithValue("@WebsiteUrl", txtWebUrl.Text);
               mycmd.Parameters.AddWithValue("@WebsitePort", txtWebsitePort.Text);

               mycmd.Parameters.AddWithValue("@IISServerIpUrl", txtIISServerIP.Text);
               mycmd.Parameters.AddWithValue("@IISServerUserId", txtServerUserId.Text);               
               mycmd.Parameters.AddWithValue("@IISServerPassWord", PageMgmt.Encrypted(txtServerPw.Text));
               mycmd.Parameters.AddWithValue("@WebsitepublicIP", txt_publicip.Text);
               mycmd.Parameters.AddWithValue("@WebsiteprivateIP", txt_private.Text);
               mycmd.Parameters.AddWithValue("@IISAccessPort", txtISSPort.Text);

               mycmd.Parameters.AddWithValue("@DatabaseName", txtDatabaseName.Text);
               mycmd.Parameters.AddWithValue("@DatabaseServerIpUrl", txtDatabaseServerurl.Text);
               mycmd.Parameters.AddWithValue("@DatabaseUserId", txtDBUserId.Text);
               mycmd.Parameters.AddWithValue("@DatabasePassword", PageMgmt.Encrypted(txtDBPassword.Text));
              

               mycmd.Parameters.AddWithValue("@BusiControllerName", txtBusicontrollerName.Text);
               mycmd.Parameters.AddWithValue("@BusiControllerDatabaseName", txtBusiDatabaseName.Text);
               mycmd.Parameters.AddWithValue("@BusiControllerSqlServerIpUrl", txtBusiServerUrl.Text);
               mycmd.Parameters.AddWithValue("@BusiControllerUserId", txtBusiUserId.Text);
               mycmd.Parameters.AddWithValue("@BusiControllerPassword", PageMgmt.Encrypted(txtBusipassword.Text));
               mycmd.Parameters.AddWithValue("@BusiControllerConnectionString", txtBusiconnectionString.Text);

               mycmd.Parameters.AddWithValue("@FTP_Url", txtFtpUrl.Text);
               mycmd.Parameters.AddWithValue("@FTP_Port", txtFtpPort.Text);
               mycmd.Parameters.AddWithValue("@FTP_UserId", txtFtpUserId.Text);
               mycmd.Parameters.AddWithValue("@FTP_Password", PageMgmt.Encrypted(txtFtpPassword.Text));
              

               mycmd.Parameters.AddWithValue("@VersionInfoId", ddlProductname.SelectedValue);
               

               mycmd.Parameters.AddWithValue("@DatabaseAccessPort", txtDatabaseAccessPort.Text);
               mycmd.Parameters.AddWithValue("@BusiControllerPort", txtBusiController.Text);
               mycmd.Parameters.AddWithValue("@FTPWorkGuideUrl", txtFTPWorkGuideUrl.Text);
               mycmd.Parameters.AddWithValue("@FTPWorkGuidePort", txtFtpWorkGuidePort.Text);
               mycmd.Parameters.AddWithValue("@FTPWorkGuideUserId", txtFtpWorkGuideUserId.Text);
               mycmd.Parameters.AddWithValue("@FTPWorkGuidePW", PageMgmt.Encrypted(txtFtpWorkGuidePassword.Text));
               mycmd.Parameters.AddWithValue("@FileUploadUrl", TextBox1.Text);
               mycmd.Parameters.AddWithValue("@FileUploadPort", TextBox2.Text);
               mycmd.Parameters.AddWithValue("@FileUploadUserId", TextBox3.Text);
               mycmd.Parameters.AddWithValue("@FileUploadPW", PageMgmt.Encrypted(TextBox4.Text));
               mycmd.Parameters.AddWithValue("@RootFolderPath", txt_codepath.Text + "\\" + txt_IISWebsiteFolderName.Text);
               mycmd.Parameters.AddWithValue("@VersionFolderUrl", txtVersionfolderpath.Text);

               mycmd.Parameters.AddWithValue("@VersionFolderRootPath", TextBox5.Text + "\\" + txt_IISVersionFolderPath.Text);
               mycmd.Parameters.AddWithValue("@IISWebsiteFolderName", txt_IISWebsiteFolderName.Text);
               mycmd.Parameters.AddWithValue("@IISVersionFolderPath", txt_IISVersionFolderPath.Text);
               mycmd.Parameters.AddWithValue("@ServerID", ViewState["serverid"]);
               mycmd.Parameters.AddWithValue("@Password", TextBox7.Text);
               mycmd.Parameters.AddWithValue("@Status", CheckBox1.Checked);
               mycmd.Parameters.AddWithValue("@productioncodeurl", TextBox9.Text);
               mycmd.Parameters.AddWithValue("@DNSserver", DropDownList2.SelectedValue);
               mycmd.Parameters.AddWithValue("@DNSname", TextBox10.Text);

              // mycmd.Parameters.AddWithValue("@FTPWorkGuidePW", txtFtpWorkGuidePassword.Text);

               try
               {
                   CreateDirectories();

                   string ftpURL = Convert.ToString(ViewState["ftpURL"]);
                   string msg = "Please note that following folders will be created at this path " + txt_codepath.Text.Trim() + txt_IISWebsiteFolderName.Text.Trim() + "" + Environment.NewLine;
                   msg += "Attach " + Environment.NewLine + "Attachment " + Environment.NewLine + "OriginalVersions " + Environment.NewLine + "VersionFolder" + Environment.NewLine;
                   //System.Windows.Forms.MessageBox.Show(msg, "Folder Creation", System.Windows.Forms.MessageBoxButtons.OK);
               }
               catch (Exception)
               {
                   string ftpURL = Convert.ToString(ViewState["ftpURL"]);
                   string msg = "We could not create the following folders at this path " + txt_codepath.Text.Trim() + txt_IISWebsiteFolderName.Text.Trim() + "" + Environment.NewLine;
                   msg += "Attach " + Environment.NewLine + "Attachment " + Environment.NewLine + "OriginalVersions " + Environment.NewLine + "VersionFolder" + Environment.NewLine;
                   msg += "Please create them manually at the server";
                   //System.Windows.Forms.MessageBox.Show(msg, "Folder Creation", System.Windows.Forms.MessageBoxButtons.OK);

               }
               //  mycmd.Parameters.Add("@Answer", SqlDbType.NVarChar).Value = DataList1.FindControl("txtans").ToString();
               con.Close();
               con.Open();
               mycmd.ExecuteNonQuery();
               con.Close();

               //----------------------CODE FOR CREATING NEW FOLDER LIKE ATTACH, ATTACHMENT etc----------------------------//
               try
               {
                   CreateDirectories();
               }
               catch (Exception)
               {

               }
               //----------------------END CODE FOR CREATING NEW FOLDER LIKE ATTACH, ATTACHMENT etc----------------------------//

               clearall();               
               Label1.Text = "Record inserted successfully";
               fillgriddata();
               ModernpopSync.Show(); 

           }
           catch (Exception ex)
           {
               Label1.Text = ex.ToString();
           }
       }

       addnewpanel.Visible = true;
       pnladdnew.Visible = false;

    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        //int id = Convert.ToInt32(GridView1.DataKeys(.ToString());

        //  GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);

        int id = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());


        string str22 = "  Select WebsiteMaster.* from WebsiteMaster where WebsiteMaster.WebsiteName='" + txtSiteName.Text + "' and WebsiteMaster.VersionInfoId='" + ddlProductname.SelectedValue + "' and WebsiteMaster.ID<>'" + ViewState["sid"] + "'";

        SqlCommand cmd2 = new SqlCommand(str22, con);
        con.Open();
        SqlDataReader rdr = cmd2.ExecuteReader();
        if (rdr.Read())
        {
            Label1.Text = "You can't add same Website name in the same Version of the Product";
            con.Close();
        }
        else
        {
            try
            {                

                
                SqlCommand mycmd = new SqlCommand("WebsiteMaster_AddDelUpdtSelect", con);
                mycmd.CommandType = CommandType.StoredProcedure;
                mycmd.Parameters.AddWithValue("@StatementType", "Update");                
                mycmd.Parameters.AddWithValue("@ID", id);
                mycmd.Parameters.AddWithValue("@VersionInfoId", ddlProductname.SelectedValue);
                mycmd.Parameters.AddWithValue("@WebsiteName", txtSiteName.Text);
                mycmd.Parameters.AddWithValue("@WebsiteUrl", txtWebUrl.Text);
                mycmd.Parameters.AddWithValue("@WebsitePort", txtWebsitePort.Text);                        
               

                mycmd.Parameters.AddWithValue("@DatabaseName", txtDatabaseName.Text);
                mycmd.Parameters.AddWithValue("@DatabaseServerIpUrl", txtDatabaseServerurl.Text);
                mycmd.Parameters.AddWithValue("@DatabaseUserId", txtDBUserId.Text);
                mycmd.Parameters.AddWithValue("@DatabasePassword", PageMgmt.Encrypted(txtDBPassword.Text));
                // mycmd.Parameters.AddWithValue("@DatabasePassword", txtDBPassword.Text);

                mycmd.Parameters.AddWithValue("@BusiControllerName", txtBusicontrollerName.Text);
                mycmd.Parameters.AddWithValue("@BusiControllerDatabaseName", txtBusiDatabaseName.Text);
                mycmd.Parameters.AddWithValue("@BusiControllerSqlServerIpUrl", txtBusiServerUrl.Text);
                mycmd.Parameters.AddWithValue("@BusiControllerUserId", txtBusiUserId.Text);
                mycmd.Parameters.AddWithValue("@BusiControllerPassword", PageMgmt.Encrypted(txtBusipassword.Text));
                // mycmd.Parameters.AddWithValue("@BusiControllerPassword", txtBusipassword.Text);

                mycmd.Parameters.AddWithValue("@BusiControllerConnectionString", txtBusiconnectionString.Text);
                mycmd.Parameters.AddWithValue("@FTP_Url", txtFtpUrl.Text);
                mycmd.Parameters.AddWithValue("@FTP_Port", txtFtpPort.Text);
                mycmd.Parameters.AddWithValue("@FTP_UserId", txtFtpUserId.Text);
                mycmd.Parameters.AddWithValue("@FTP_Password", PageMgmt.Encrypted(txtFtpPassword.Text));
                

               
               

                mycmd.Parameters.AddWithValue("@DatabaseAccessPort", txtDatabaseAccessPort.Text);
                mycmd.Parameters.AddWithValue("@BusiControllerPort", txtBusiController.Text);


                mycmd.Parameters.AddWithValue("@FTPWorkGuideUrl", txtFTPWorkGuideUrl.Text);
                mycmd.Parameters.AddWithValue("@FTPWorkGuidePort", txtFtpWorkGuidePort.Text);

                mycmd.Parameters.AddWithValue("@FTPWorkGuideUserId", txtFtpWorkGuideUserId.Text);
                mycmd.Parameters.AddWithValue("@FTPWorkGuidePW", PageMgmt.Encrypted(txtFtpWorkGuidePassword.Text));

                mycmd.Parameters.AddWithValue("@FileUploadUrl", TextBox1.Text);
                mycmd.Parameters.AddWithValue("@FileUploadPort", TextBox2.Text);

                mycmd.Parameters.AddWithValue("@FileUploadUserId", TextBox3.Text);
                mycmd.Parameters.AddWithValue("@FileUploadPW", PageMgmt.Encrypted(TextBox4.Text));



                mycmd.Parameters.AddWithValue("@RootFolderPath", txt_codepath.Text + "\\" + txt_IISWebsiteFolderName.Text);
                mycmd.Parameters.AddWithValue("@VersionFolderUrl", txtVersionfolderpath.Text);
                mycmd.Parameters.AddWithValue("@VersionFolderRootPath", TextBox5.Text +"\\"+ txt_IISVersionFolderPath.Text);

                mycmd.Parameters.AddWithValue("@IISWebsiteFolderName", txt_IISWebsiteFolderName.Text);
                mycmd.Parameters.AddWithValue("@IISVersionFolderPath", txt_IISVersionFolderPath.Text);
                mycmd.Parameters.AddWithValue("@ServerID", ViewState["serverid"]);
                mycmd.Parameters.AddWithValue("@Password", TextBox7.Text);
                mycmd.Parameters.AddWithValue("@Status", CheckBox1.Checked);
                mycmd.Parameters.AddWithValue("@productioncodeurl", TextBox9.Text);
                mycmd.Parameters.AddWithValue("@DNSserver", DropDownList2.SelectedValue);
              //  mycmd.Parameters.AddWithValue("@DNSserver", DropDownList2.SelectedValue);
                mycmd.Parameters.AddWithValue("@DNSname", TextBox10.Text);
              

                try
                {
                    CreateDirectories();

                    string ftpURL = Convert.ToString(ViewState["ftpURL"]);
                    string msg = "Please note that following folders will be created at this path " + txt_codepath.Text.Trim() + txt_IISWebsiteFolderName.Text.Trim() + "" + Environment.NewLine;
                    msg += "Attach " + Environment.NewLine + "Attachment " + Environment.NewLine + "OriginalVersions " + Environment.NewLine + "VersionFolder" + Environment.NewLine;
                    //System.Windows.Forms.MessageBox.Show(msg, "Folder Creation", System.Windows.Forms.MessageBoxButtons.OK);
                }
                catch (Exception)
                {
                    string ftpURL = Convert.ToString(ViewState["ftpURL"]);
                    string msg = "We could not create the following folders at this path " + txt_codepath.Text.Trim() + txt_IISWebsiteFolderName.Text.Trim() + "" + Environment.NewLine;
                    msg += "Attach " + Environment.NewLine + "Attachment " + Environment.NewLine + "OriginalVersions " + Environment.NewLine + "VersionFolder" + Environment.NewLine;
                    msg += "Please create them manually at the server";
                    //System.Windows.Forms.MessageBox.Show(msg, "Folder Creation", System.Windows.Forms.MessageBoxButtons.OK);

                }

                con.Close();
                con.Open();
                mycmd.ExecuteNonQuery();
                con.Close();
                clearall();
                Label1.Text = "Record updated successfully";

                fillgriddata();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }

        }

        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
    }
    public bool directoryExists(string directory, string host, string user, string pass)
    {
        /* Create an FTP Request */
        //string host = "";
        //string user = "";
        //string pass = "";
        //FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
        FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(directory);
        /* Log in to the FTP Server with the User Name and Password Provided */
        ftpRequest.Credentials = new NetworkCredential(user, pass);
        /* Specify the Type of FTP Request */
        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
        try
        {
            using (ftpRequest.GetResponse())
            {
                return true;
            }
            //var response = ftpRequest.GetResponse();
            //if (response != null)
            //    return true;
            //else return false;
        }
        catch (Exception ex)
        {
            WebRequest request = WebRequest.Create(directory);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(user, pass);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
            Console.WriteLine(ex.ToString());
            return false;
        }

        /* Resource Cleanup */
        finally
        {
            ftpRequest = null;
        }
    }

    private void CreateDirectories()
    {
            //----------------------CODE FOR CREATING NEW FOLDER LIKE ATTACH, ATTACHMENT etc----------------------------//
            string[] seperator_RootPath = new string[] { "/" };
            string RootPath = txt_codepath.Text.Trim() + txt_IISWebsiteFolderName.Text.Trim();
            string[] RootPathArray = RootPath.Split(seperator_RootPath, StringSplitOptions.RemoveEmptyEntries);
            string FolderName = "";
            for (int k = 2; k < RootPathArray.Length; k++)
            {
                FolderName += RootPathArray[k].ToString() + "/";
            }
            FolderName = FolderName.ToString().Substring(0, FolderName.Length - 1);
            string ftpURL = "ftp://" + txtFTPWorkGuideUrl.Text.Trim() + ":" + txtFtpWorkGuidePort.Text.Trim() + "/" + FolderName;
            ViewState["ftpURL"] = ftpURL;
            string ftpVersionFolder = ftpURL + "/" + "VersionFolder";
            string ftpAttachFolder = ftpURL + "/" + "Attach";
            string ftpAttachmentFolder = ftpURL + "/" + "Attachment";
            string ftpOriginalVersionFolder = ftpURL + "/" + "OriginalVersions";
            
                directoryExists(ftpVersionFolder, "", txtFtpUserId.Text.Trim(), txtFtpPassword.Text.Trim());
                directoryExists(ftpAttachFolder, "", txtFtpUserId.Text.Trim(), txtFtpPassword.Text.Trim());
                directoryExists(ftpAttachmentFolder, "", txtFtpUserId.Text.Trim(), txtFtpPassword.Text.Trim());
                directoryExists(ftpOriginalVersionFolder, "", txtFtpUserId.Text.Trim(), txtFtpPassword.Text.Trim());
            
                
            
            //----------------------END CODE FOR CREATING NEW FOLDER LIKE ATTACH, ATTACHMENT etc----------------------------//
        
    }
  
    protected void fillgriddata()
    {
        string activestr = "";
        if (Ddlproduct_search.SelectedIndex > 0)
        {
            activestr += " and  dbo.VersionInfoMaster.ProductId=" + Ddlproduct_search.SelectedValue + " ";

        }

        if (DropDownList1.SelectedIndex > 0)
        {
            activestr += " and  WebsiteMaster.ServerID=" + DropDownList1.SelectedValue + " ";

        }
        if (TextBox8.Text != "")
        {
            activestr += " and ( (WebsiteMaster.WebsiteName like '%" + TextBox8.Text.Replace("'", "''") + "%') )";
        
        }


        string sgggg = "SELECT  WebsiteMaster.*,ServerMasterTbl.ServerName,ServerMasterTbl.PublicIp,ServerMasterTbl.Ipaddress,ProductMaster.ProductName,VersionInfoMaster.VersionInfoName  from WebsiteMaster inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId  inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId left join ServerMasterTbl on ServerMasterTbl.Id=WebsiteMaster.ServerID where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active='1' and ProductDetail.Active='1' " + activestr + " ";
        SqlCommand cmdgrid = new SqlCommand(sgggg, con);
        SqlDataAdapter dtpgrid = new SqlDataAdapter(cmdgrid);
        DataTable dtgrid = new DataTable();
        dtpgrid.Fill(dtgrid);


        if (dtgrid.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = dtgrid.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataSource = dtgrid;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editview")
        {
            TextBox9.Text = "";
            TextBox10.Text = "";
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;

            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            ViewState["sid"] = GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString();
            

            SqlCommand cmdedit = new SqlCommand(" SELECT     WebsiteMaster.* from  WebsiteMaster  where WebsiteMaster.ID = '" + ViewState["sid"] + "'", con);


            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);
            if (dtedit.Rows.Count > 0)
            {
                
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = true;
             
                txtSiteName.Text = dtedit.Rows[0]["WebsiteName"].ToString();

                txtWebUrl.Text = dtedit.Rows[0]["WebsiteUrl"].ToString();
                txtWebsitePort.Text = dtedit.Rows[0]["WebsitePort"].ToString();

                txtWebUrl_TextChanged(sender ,e);
              //  
                //txtIISServerIP.Text = dtedit.Rows[0]["IISServerIpUrl"].ToString();
                //txtServerUserId.Text = dtedit.Rows[0]["IISServerUserId"].ToString();
                // txt_publicip.Text = dtedit.Rows[0]["WebsitepublicIP"].ToString();
                //txt_private.Text = dtedit.Rows[0]["WebsiteprivateIP"].ToString();
               // txtServerPw.Text = PageMgmt.Decrypted(dtedit.Rows[0]["IISServerPassWord"].ToString());
                //txtISSPort.Text = dtedit.Rows[0]["IISAccessPort"].ToString();

                string strqa = txtServerPw.Text;
                txtServerPw.Attributes.Add("Value", strqa);
                txtDatabaseName.Text = dtedit.Rows[0]["DatabaseName"].ToString();
                txtDatabaseServerurl.Text = dtedit.Rows[0]["DatabaseServerIpUrl"].ToString();
                txtDBUserId.Text = dtedit.Rows[0]["DatabaseUserId"].ToString();
                txtDBPassword.Text = PageMgmt.Decrypted(dtedit.Rows[0]["DatabasePassword"].ToString());

                //

                //

                string strqa1 = txtDBPassword.Text;
                txtDBPassword.Attributes.Add("Value", strqa1);


                txtBusicontrollerName.Text = dtedit.Rows[0]["BusiControllerName"].ToString();
                txtBusiDatabaseName.Text = dtedit.Rows[0]["BusiControllerDatabaseName"].ToString();
                txtBusiServerUrl.Text = dtedit.Rows[0]["BusiControllerSqlServerIpUrl"].ToString();
                txtBusiUserId.Text = dtedit.Rows[0]["BusiControllerUserId"].ToString();
                txtBusipassword.Text = PageMgmt.Decrypted(dtedit.Rows[0]["BusiControllerPassword"].ToString());

                string strqa2 = txtBusipassword.Text;
                txtBusipassword.Attributes.Add("Value", strqa2);

                txtBusiconnectionString.Text = dtedit.Rows[0]["BusiControllerConnectionString"].ToString();
                txtFtpUrl.Text = dtedit.Rows[0]["FTP_Url"].ToString();
                txtFtpPort.Text = dtedit.Rows[0]["FTP_Port"].ToString();
                txtFtpUserId.Text = dtedit.Rows[0]["FTP_UserId"].ToString();
                txtFtpPassword.Text = PageMgmt.Decrypted(dtedit.Rows[0]["FTP_Password"].ToString());

                string strqa3 = txtFtpPassword.Text;
                txtFtpPassword.Attributes.Add("Value", strqa3);
                FillProduct();
                ddlProductname.SelectedValue = dtedit.Rows[0]["VersionInfoId"].ToString();

                fillpath();
                txtDatabaseAccessPort.Text = dtedit.Rows[0]["DatabaseAccessPort"].ToString();
                txtBusiController.Text = dtedit.Rows[0]["BusiControllerPort"].ToString();

                txtFTPWorkGuideUrl.Text = dtedit.Rows[0]["FTPWorkGuideUrl"].ToString();
                txtFtpWorkGuidePort.Text = dtedit.Rows[0]["FTPWorkGuidePort"].ToString();
                txtFtpWorkGuideUserId.Text = dtedit.Rows[0]["FTPWorkGuideUserId"].ToString();
                txtFtpWorkGuidePassword.Text = PageMgmt.Decrypted(dtedit.Rows[0]["FTPWorkGuidePW"].ToString());

                string strqa4 = txtFtpWorkGuidePassword.Text;
                txtFtpWorkGuidePassword.Attributes.Add("Value", strqa4);

                if (Convert.ToString(dtedit.Rows[0]["FileUploadUrl"]) != "")
                {
                    TextBox1.Text = dtedit.Rows[0]["FileUploadUrl"].ToString();
                }
                if (Convert.ToString(dtedit.Rows[0]["FileUploadPort"]) != "")
                {
                    TextBox2.Text = dtedit.Rows[0]["FileUploadPort"].ToString();
                }
                if (Convert.ToString(dtedit.Rows[0]["FileUploadUserId"]) != "")
                {
                    TextBox3.Text = dtedit.Rows[0]["FileUploadUserId"].ToString();
                }
                if (Convert.ToString(dtedit.Rows[0]["FileUploadPW"]) != "")
                {
                    TextBox4.Text = PageMgmt.Decrypted(dtedit.Rows[0]["FileUploadPW"].ToString());

                    string strqa5 = TextBox4.Text;
                    TextBox4.Attributes.Add("Value", strqa5);
                }


               // txtRootFolderPath.Text = dtedit.Rows[0]["RootFolderPath"].ToString();
                txtVersionfolderpath.Text = dtedit.Rows[0]["VersionFolderUrl"].ToString();

               // txt_versionfolderrootpath.Text = dtedit.Rows[0]["VersionFolderRootPath"].ToString();
               txt_IISWebsiteFolderName.Text = dtedit.Rows[0]["IISWebsiteFolderName"].ToString();
               txt_IISVersionFolderPath.Text= dtedit.Rows[0]["IISVersionFolderPath"].ToString();
              
               TextBox7.Text = dtedit.Rows[0]["Password"].ToString();
               try
               {
                   Fillserverddl(); 
                   ddlserverMas.SelectedValue = dtedit.Rows[0]["ServerID"].ToString();
                   TextBox9.Text = dtedit.Rows[0]["productioncodeurl"].ToString();
                   DropDownList2.SelectedValue = dtedit.Rows[0]["DNSserver"].ToString();
                   fillDNS();
                   TextBox10.Text = dtedit.Rows[0]["DNSname"].ToString();

                   TextBox10_TextChanged(sender, e);
                 



               }
               catch
               {
               }

               txt_IISWebsiteFolderName_TextChanged(sender, e);
               txt_IISVersionFolderPath_TextChanged(sender, e);
               txtSiteName_TextChanged(sender, e);
              // TextBox5.Text = dtedit.Rows[0]["VersionFolderRootPath"].ToString();

               Label1.Text = "";


               if (dtedit.Rows[0]["Status"].ToString() == "0")
               {
                   CheckBox1.Checked = false;
               }
               else
               {

                   CheckBox1.Checked = true;
               
               }

            }
        }
        else if (e.CommandName == "Delete")
        {
        

            int id = Convert.ToInt32(e.CommandArgument);

            try
            {
                string str = "select PageId from PageMaster left outer join SubMenuMaster on SubMenuMaster.SubMenuId = PageMaster.SubMenuId left outer join MainMenuMaster on MainMenuMaster.MainMenuId = PageMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId = MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId = MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID = WebsiteSection.WebsiteMasterId where WebsiteMaster.ID ='" + id + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "You can not delete this record";
                }
                else
                {
                    SqlCommand mycmd = new SqlCommand("DeleteWebsiteMaster", con);
                    mycmd.CommandType = CommandType.StoredProcedure;
                    mycmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    mycmd.ExecuteNonQuery();
                    con.Close();

                    Label1.Text = "Record deleted successfully";

                    fillgriddata();
                }
            }
            catch (Exception ex1)
            {
                Label1.Text = ex1.ToString();
            }
        }


    }
    
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgriddata();
    }
  

   

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        fillgriddata();
        clearall();
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
       
    }
    
    private bool isValidConnection(string url, string user, string password)
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
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(txtFtpPort.Text);
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(txtFtpPort.Text);

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(txtFtpPort.Text);
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(txtFtpPort.Text);

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
    protected void Button1_Click(object sender, EventArgs e)
    {
        gg = isValidConnection(txtFtpUrl.Text, txtFtpUserId.Text, txtFtpPassword.Text);
        if (gg)
        {
            Label1.Visible = true;
            Label1.Text = "Read and Write successful";
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Read and Write not successful";
        }
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        gg = isValidConnection(txtFTPWorkGuideUrl.Text, txtFtpWorkGuideUserId.Text, txtFtpWorkGuidePassword.Text);
        if (gg)
        {
            Label1.Visible = true;
            Label1.Text = "Read and Write successful";
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Read and Write not successful";
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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgriddata();
    }


    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
        }
        else
        {



            Button3.Text = "Printable Version";
            Button4.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        Label1.Text = "";
       
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        gg = isValidConnection(TextBox1.Text, TextBox3.Text, TextBox4.Text);
        if (gg)
        {
            Label1.Visible = true;
            Label1.Text = "Read and Write successful";
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Read and Write not successful";
        }
    }
    protected void txtRootFolderPath_TextChanged(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message.ToString();
        }
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


        DataTable dt1 = select("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='WebsiteMaster' ");
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
                        
                       transf=Convert.ToInt32(rdsync.SelectedValue);
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
    //********
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }

    protected void txtSiteName_TextChanged(object sender, EventArgs e)
    {
        txt_IISWebsiteFolderName.Text = txtSiteName.Text;
        txt_IISVersionFolderPath.Text = "VersionFolder";
        TextBox5.Text=txt_codepath.Text+'\\'+txt_IISWebsiteFolderName.Text;
        TextBox6.Text = txt_codepath.Text + '\\' + txt_IISWebsiteFolderName.Text;
        Label152.Text = "35" + txtSiteName.Text;

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void TextBox10_TextChanged(object sender, EventArgs e)
    {
        Label156.Text = TextBox10.Text;
    }
}
