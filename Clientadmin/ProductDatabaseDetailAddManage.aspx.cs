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

public partial class productcode_databaseaddmanage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection connNewDB = new SqlConnection();
    bool gg;
    SqlConnection conn;
    //   SqlConnection conmaster = new SqlConnection(ConfigurationManager.ConnectionStrings["masterfile"].ConnectionString);
    public static string encstr = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {       
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {          

            ViewState["changedata"] = "0";
            FillProduct();
            fillcodetypecategory();

            FillProductMasterWeb();
            FillWebsiteGrid();          
                
            fillgrid();
            FillProductsearch();

            FilFTP();
        }
    }
    public void clear()
    {        
        ddlproductversion.SelectedIndex = 0;
        txtcodetypename.Text = "";
        FillProductMasterWeb();
        FillWebsiteGrid();     

        CheckBox11.Checked = false;
        CheckBox12.Checked = false;
        lblmsg.Text = "";
        ddlproductversion.Enabled = true;
        ddlcodetypecategory.Enabled = true;

        btn_submitCode.Visible = true;
        btn_update.Visible = false;

        lbl_uploadldfsucc.Text = "";
        lbl_uploadmdfsucc.Text = "";
        lbl_serverattach.Text = "";

        txt_ldffilename.Text = "";
        txt_ldffilepath.Text = "";
        txt_mdffilename.Text = "";
        txt_mdffilepath.Text = "";
        txtcodetypename.Text = "";
        ViewState["CodeDetailID"] = null;
        ViewState["id"] = null;
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
    protected void BtnCancelgo_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void btnAddNewDAta_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        clear();
    }
    protected void FillProduct()
    {
        DataTable dtcln =MyCommonfile.selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion,ProductMaster.ProductName FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductMaster.ProductId = dbo.ProductDetail.ProductId AND dbo.VersionInfoMaster.VersionInfoName = dbo.ProductDetail.VersionNo where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and ProductDetail.Active='True' and VersionInfoMaster.Active='True'  order  by productversion");
        ddlproductversion.DataSource = dtcln;
        ddlproductversion.DataValueField = "VersionInfoId";
        ddlproductversion.DataTextField = "ProductName";
        ddlproductversion.DataBind();
        fillproductid();
        FillProductMasterWeb();
        DDLProdFolterWeb.SelectedValue = ddlproductversion.SelectedValue;
        FillWebsiteGrid();
    }
    protected void fillproductid()
    {
        DataTable dtcln =MyCommonfile.selectBZ("SELECT distinct ProductMaster.ProductId,  dbo.ClientMaster.ServerId  ,ProductMaster.Description,dbo.VersionInfoMaster.ServerMasterCodeSourceIISWebsitePath  FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.VersionNo = dbo.VersionInfoMaster.VersionInfoName INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId  where ProductMaster.ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and VersionInfoMaster.VersionInfoId='" + ddlproductversion.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            ViewState["ProductId"] = dtcln.Rows[0]["ProductId"].ToString();
            txt_prod_desc.Text = dtcln.Rows[0]["Description"].ToString();
            lbl_serverid.Text = dtcln.Rows[0]["ServerId"].ToString();

            txtmastercodepathC.Text = dtcln.Rows[0]["ServerMasterCodeSourceIISWebsitePath"].ToString() + "" + ddlproductversion.SelectedItem.Text;
            txttemppathC.Text = dtcln.Rows[0]["ServerMasterCodeSourceIISWebsitePath"].ToString() + "" + ddlproductversion.SelectedItem.Text + "" + "\\Temppath";
            txtoutputsourcepathC.Text = dtcln.Rows[0]["ServerMasterCodeSourceIISWebsitePath"].ToString() + "" + ddlproductversion.SelectedItem.Text + "" + "\\Mastercode";

            MasterServerMDFLDFPathGet();
        }
        else
        {
            txt_prod_desc.Text = "";
        }
       CheckMDFLDFFileLocation();
    }    
    protected void ddlproductversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillproductid();
        DDLProdFolterWeb.SelectedValue = ddlproductversion.SelectedValue;
        FillWebsiteGrid();
    }
    protected void fillcodetypecategory()
    {
        DataTable dtcln =MyCommonfile.selectBZ(" select * from CodeTypeCategory where id='2' ");
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
        pnl_uploaddatabasefile.Visible = false;
    }
    protected void FillProductMasterWeb()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName,ProductMaster.ProductName +':'+ VersionInfoMaster.VersionInfoName as aa,VersionInfoMaster.VersionInfoId  FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  and VersionInfoMaster.Active='1'  order  by ProductName ");
        DDLProdFolterWeb.DataSource = dtcln;
        DDLProdFolterWeb.DataValueField = "VersionInfoId";
        DDLProdFolterWeb.DataTextField = "ProductName";
        DDLProdFolterWeb.DataBind();
        DDLProdFolterWeb.Items.Insert(0, "--Select--");
        DDLProdFolterWeb.Items[0].Value = "0";
        DDLProdFolterWeb.SelectedIndex = 0;
    }
    protected void DDLProdFolterWeb_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillWebsiteGrid();
    }
   
    //-***********Website Grid web
    protected void FillWebsiteGrid()
    {
        string strsearch = "";
        if (ddlproductversion.SelectedIndex > 0)
        {
            strsearch += " and dbo.WebsiteMaster.VersionInfoId=" + ddlproductversion.SelectedValue + "";
        }
        string strcln = " SELECT distinct ID, WebsiteName,Case when(ID IS NULL) then  cast ('1' as bit) else  cast('0' as bit) end as chk From dbo.WebsiteMaster INNER JOIN dbo.VersionInfoMaster ON dbo.WebsiteMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId  Where dbo.VersionInfoMaster.Active=1 " + strsearch + "  ";        
        DataTable dtclnweb = MyCommonfile.selectBZ("SELECT distinct ID, WebsiteName,Case when(ID IS NULL) then  cast ('1' as bit) else  cast('0' as bit) end as chk From dbo.WebsiteMaster INNER JOIN dbo.VersionInfoMaster ON dbo.WebsiteMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId  Where dbo.VersionInfoMaster.Active=1 " + strsearch + "");
        GridView2.DataSource = dtclnweb;
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //VersionDate           
            Label lblwebid = (Label)e.Row.FindControl("lblwebid");
            CheckBox cbItem = (CheckBox)(e.Row.FindControl("cbItem"));
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            string strwebsite = " SELECT   TOP (1) * From ProductCodeDatabasDetailWithWebsiteID Where ProductCodeDatabasDetailID='" + ViewState["id"] + "' and WebsiteID='" + lblwebid.Text + "' ";
            SqlCommand cmd12web = new SqlCommand(strwebsite, con);
            SqlDataAdapter adp12web = new SqlDataAdapter(cmd12web);
            DataTable ds12web = new DataTable();
            adp12web.Fill(ds12web);
            if (ds12web.Rows.Count > 0)
            {
                cbItem.Checked = true;
            }
            else
            {
                cbItem.Checked = false;
            }
        }
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
    protected void btn_test_Click(object sender, EventArgs e)
    {
        CheckMDFLDFFileLocation();
    }
    private void CheckMDFLDFFileLocation()
    {
        try
        {
            if (Directory.Exists(txttemppathC.Text))
            {
                lbltemppathtest.ForeColor = System.Drawing.Color.Green;
                lbltemppathtest.Text = "Connection Possible";
            }
            else
            {
                lbltemppathtest.ForeColor = System.Drawing.Color.Red;
                lbltemppathtest.Text = "Connection Not Possible";
            }           
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
                lbloupputpathtest.ForeColor = System.Drawing.Color.Green;
                lbloupputpathtest.Text = "Connection Possible";
            }
            else
            {
                lbloupputpathtest.ForeColor = System.Drawing.Color.Red;
                lbloupputpathtest.Text = "Connection Not Possible";
            }
           
        }
        catch (Exception ex)
        {
            lbloupputpathtest.ForeColor = System.Drawing.Color.Red;
            lbloupputpathtest.Text = "Connection Not Possible";
        }

        try
        {
            if (Directory.Exists(txt_mdffilepath.Text))
            {
                lbl_mdffilenamepath.ForeColor = System.Drawing.Color.Green;
                lbl_mdffilenamepath.Text = "Connection Possible";
            }
            else
            {
                lbl_mdffilenamepath.ForeColor = System.Drawing.Color.Red;
                lbl_mdffilenamepath.Text = "Connection Not Possible";
            }
           
        }
        catch (Exception ex)
        {
            lbl_mdffilenamepath.ForeColor = System.Drawing.Color.Red;
            lbl_mdffilenamepath.Text = "Connection Not Possible";
        }

        try
        {
            if (Directory.Exists(txt_ldffilepath.Text))
            {
                lbl_ldffilepath.ForeColor = System.Drawing.Color.Green;
                lbl_ldffilepath.Text = "Connection Possible";
            }
            else
            {
                lbl_ldffilepath.ForeColor = System.Drawing.Color.Red;
                lbl_ldffilepath.Text = "Connection Not Possible";
            }
          
        }
        catch (Exception ex)
        {
            lbl_ldffilepath.ForeColor = System.Drawing.Color.Red;
            lbl_ldffilepath.Text = "Connection Not Possible";
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

    protected void chk_attechdb_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_attechdb.Checked == true)
        {
            pnl_attechdb.Visible = true;
        }
        else
        {
            pnl_attechdb.Visible = false;
        }
    }
    protected void MasterServerMDFLDFPathGet()
    {
        string finalstr = "SELECT * FROM dbo.ServerMasterTbl Where Id='"+lbl_serverid.Text +"'";
        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lbl_severname.Text = ds.Rows[0]["ServerName"].ToString();
            string ftp = ds.Rows[0]["FTPforMastercode"].ToString();
            string user = ds.Rows[0]["FTPuseridforDefaultIISpath"].ToString();
            lbl_settelliteserver.Text = ds.Rows[0]["SqlServerName"].ToString();

            txt_mdffilepath.Text = ds.Rows[0]["serverdefaultpathformdf"].ToString()+"\\"+Session["ClientId"]+"\\"+ddlproductversion.SelectedItem.Text;
            txt_ldffilepath.Text = ds.Rows[0]["serverdefaultpathforfdf"].ToString()+"\\"+Session["ClientId"]+"\\"+ddlproductversion.SelectedItem.Text;
          
            ddlinstance.Items.Clear();
            ddlinstance.Items.Insert(0, "" + ds.Rows[0]["Sqlinstancename"].ToString() + "");
            ddlinstance.Items[0].Value = "0";
            ddlinstance.Items.Insert(1, "" + ds.Rows[0]["DefaultsqlInstance"].ToString() + "");
            ddlinstance.Items[0].Value = "1";          
            
            checkdatabaseconn();         
        }
        else
        {
        }
    }
    protected void ddlinstance_SelectedIndexChanged(object sender, EventArgs e)
    {
        checkdatabaseconn();
    }
    protected void checkdatabaseconn()
    {
    //****************************************************************
        lbl_dbatttacherror.Text = "";
        lbl_serverconnectioncheck.Text = "";
         string finalstr = "SELECT * FROM dbo.ServerMasterTbl Where Id='"+lbl_serverid.Text +"'";
        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            string serversqlserverip = ds.Rows[0]["sqlurl"].ToString();
            string serversqlinstancename = ds.Rows[0]["DefaultDatabasename"].ToString();
            string serversqldbname = txtcodetypename.Text;
            string serversqlpwd = ds.Rows[0]["Sapassword"].ToString();
            string serversqlport = ds.Rows[0]["port"].ToString();

            string Sqlinstancename = ds.Rows[0]["Sqlinstancename"].ToString();
            string DefaultsqlInstance = ds.Rows[0]["DefaultsqlInstance"].ToString();


            SqlConnection connNewDB = new SqlConnection();
            connNewDB = ServerWizard.ServerDatabaseFromInstanceTCP(lbl_serverid.Text, ddlinstance.SelectedItem.Text, txtcodetypename.Text);
            SqlConnection connNewInstance = new SqlConnection();
            connNewInstance = ServerWizard.ServerTwoInstanceTCPConncection(lbl_serverid.Text, ddlinstance.SelectedItem.Text);
            Boolean CheckConne = false;
                try
                {
                    if (connNewInstance.State.ToString() != "Open")
                    {
                        connNewInstance.Open();
                    }
                    lbl_serverconnectioncheck.Text = "Connection Possible ";
                    lbl_serverconnectioncheck.ForeColor = System.Drawing.Color.Green;
                    CheckConne = true;
                }
                catch (Exception ex)
                {
                    lbl_serverconnectioncheck.Text = "Connection Not Possible";
                    lbl_serverconnectioncheck.ForeColor = System.Drawing.Color.Red;
                }
                if (CheckConne == true)
                {
                    if (Chk_CodeOp2.Checked == true && txtcodetypename.Text !=null)
                    {
                        Boolean DatabaseConn = false;
                        try
                        {
                            if (connNewDB.State.ToString() != "Open")
                            {
                                connNewDB.Open();
                            }
                            lbl_dbatttacherror.Text = " Database by the name  " + txtcodetypename.Text + " already exist pelase change the name (Suggested Name " + txtcodetypename.Text + "New1)";
                            DatabaseConn = true;
                        }
                        catch
                        {
                        }
                        if (DatabaseConn == false)
                        {
                            if (connNewInstance.State.ToString() != "Open")
                            {
                                connNewInstance.Open();
                            }
                            lbl_dbatttacherror.Text = " New database";
                        }
                    }

                    if (Chk_CodeOp1.Checked == true)
                    {
                        try
                        {
                            if (connNewDB.State.ToString() != "Open")
                            {
                                connNewDB.Open();
                            }
                            if (Chk_CodeOp1.Checked == true)
                            {
                                string strcln1 = " select name,physical_name from sys.database_files ";
                                SqlCommand cmdcln1 = new SqlCommand(strcln1, connNewDB);
                                DataTable dtcln1 = new DataTable();
                                SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
                                adpcln1.Fill(dtcln1);
                                if (dtcln1.Rows.Count > 0)
                                {
                                    txt_mdffilepath.Text = dtcln1.Rows[0]["physical_name"].ToString();
                                    txt_mdffilename.Text = Path.GetFileName(txt_mdffilepath.Text);
                                    txt_mdffilepath.Text = Path.GetDirectoryName("" + txt_mdffilepath.Text + "");
                                    //txt_ldffilename.Text = dtcln1.Rows[1]["name"].ToString();                      
                                    txt_ldffilepath.Text = dtcln1.Rows[1]["physical_name"].ToString();
                                    txt_ldffilename.Text = Path.GetFileName(txt_ldffilepath.Text);
                                    txt_ldffilepath.Text = Path.GetDirectoryName("" + txt_ldffilepath.Text + "");

                                    lbl_dbatttacherror.Text = "Connection Possible with " + txtcodetypename.Text + " database";
                                }
                                else
                                {
                                    lbl_dbatttacherror.Text = "No database available";
                                }
                            }
                        }
                        catch
                        {
                            lbl_dbatttacherror.Text = "No exist database ";
                        }
                    }
                }
                else
                {
                    lbl_dbatttacherror.Text = "No connetion available with instance";
                }
           
        }
        //*********************************************************
    }
    protected void btn_serverconncheck_Click(object sender, EventArgs e)
    {
        MasterServerMDFLDFPathGet();
    }
    protected void btn_view_detail_Click(object sender, EventArgs e)
    {
        if (btn_view_detail.Text == "View file details")
        {
            pnl_uploaddatabasefile.Visible = true;
            btn_view_detail.Text = "Hide file details";
        }
        else if (btn_view_detail.Text == "Hide file details")
        {
            pnl_uploaddatabasefile.Visible = false;
            btn_view_detail.Text = "View file details";
        }
    }   
  
   
    //--------------------------------------
    //-------------Add product database part --------------------------------------------------------------------
    protected void Chk_CodeOp1_CheckedChanged(object sender, EventArgs e)
    {
        pnl_uploaddatabasefile.Visible = false;
        pnl_uploadMdfLdf.Visible = false;
        if (Chk_CodeOp1.Checked == true)
        {           
            Chk_CodeOp2.Checked = false;
            Chk_CodeOp3.Checked = false;
            pnl_selectradio.Visible = true;

            chk_attechdb.Visible =false;
            lblradio3.Text = "Please select the sql server instance where the database is attached"; 
            chk_attechdb.Enabled = false;
            pnl_attechdb.Visible = true;

            btn_view_detail.Visible = true;
            lbl_checkAlredyAttechDB.Visible = true;   
           // pnl_uploaddatabasefile.Enabled = false;
            CheckMDFLDFFileLocation();           
        }
    }
    protected void Chk_CodeOp2_CheckedChanged(object sender, EventArgs e)
    {
        pnl_uploadMdfLdf.Visible = false;
        pnl_uploaddatabasefile.Visible = false;
        if (Chk_CodeOp2.Checked == true)
        {
            Chk_CodeOp1.Checked = false;
            Chk_CodeOp3.Checked = false;
            pnl_selectradio.Visible = true;

            
            chk_attechdb.Visible = false;
            lblradio3.Text = "Please select the sql server instance where the database is attached"; 
            
            chk_attechdb.Enabled = false;
            pnl_attechdb.Visible = true;

            pnl_uploadMdfLdf.Visible = true;
            pnl_uploaddatabasefile.Visible = true;

            btn_view_detail.Visible = false;
            lbl_checkAlredyAttechDB.Visible = false;

            CheckMDFLDFFileLocation();
   
        }
    }
    protected void Chk_CodeOp3_CheckedChanged(object sender, EventArgs e)
    {
        pnl_uploaddatabasefile.Visible = false;
        pnl_uploadMdfLdf.Visible = false;
        if (Chk_CodeOp3.Checked == true)
        {
            Chk_CodeOp2.Checked = false;
            Chk_CodeOp1.Checked = false;
            pnl_selectradio.Visible = true;

            chk_attechdb.Text = "Would you like to test whether the database is in working order  by attaching the database to sql server (Recommended) ?"; //
            lblradio3.Text = "(After testing the database will be made offline again)"; 
            chk_attechdb.Enabled = true;
            chk_attechdb.Visible = true; 
            
            pnl_uploadMdfLdf.Visible = true;
            pnl_uploaddatabasefile.Visible = true;

            btn_view_detail.Visible = false;
            lbl_checkAlredyAttechDB.Visible = false;

            pnl_attechdb.Visible = false;

            CheckMDFLDFFileLocation();
        }
    }   
    //-----------------------
    protected void btn_submitCode_Click(object sender, EventArgs e)
    {
       if (ddlcodetypecategory.SelectedValue =="2")
        {
            Boolean result=AttechDatabase();
            if (result == true)
            {
                if (CheckBox11.Checked == true)
                {
                    SqlCommand sb = new SqlCommand(" update ProductCodeDetailTbl set BusiwizSynchronization=0 where ProductId='" + ddlproductversion.SelectedValue + "' ", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    sb.ExecuteNonQuery();
                    con.Close();
                }
                if (CheckBox12.Checked == true)
                {
                    SqlCommand sb = new SqlCommand(" update ProductCodeDetailTbl set CompanyDefaultData=0 where ProductId='" + ddlproductversion.SelectedValue + "' ", con);
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
                cmd.Parameters.AddWithValue("@AdditionalPageInserted", false);
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
                ViewState["databaseid"] = dtcln1.Rows[0][0].ToString();
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text + "MDF");
                cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductCodeDetailId", dtcln1.Rows[0][0].ToString());
                cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
                cmd.Parameters.AddWithValue("@Temppath", txttemppathC.Text);
                cmd.Parameters.AddWithValue("@Outputpath", txtoutputsourcepathC.Text);
                cmd.Parameters.AddWithValue("@FileLocationPath", txt_mdffilepath.Text);
                cmd.Parameters.AddWithValue("@FileName", txt_mdffilename.Text);
                cmd.Parameters.AddWithValue("@IsforDownloadonly", Chk_CodeOp3.Checked);
                cmd.Parameters.AddWithValue("@Instancename", ddlinstance.SelectedItem.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                // cmd.Parameters.AddWithValue("@WebsiteID", DDLWebsiteC.SelectedValue);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text + "LDF");
                cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductCodeDetailId", dtcln1.Rows[0][0].ToString());
                cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
                cmd.Parameters.AddWithValue("@Temppath", txttemppathC.Text);
                cmd.Parameters.AddWithValue("@Outputpath", txtoutputsourcepathC.Text);
                cmd.Parameters.AddWithValue("@FileLocationPath", txt_ldffilepath.Text);
                cmd.Parameters.AddWithValue("@FileName", txt_ldffilename.Text);
                cmd.Parameters.AddWithValue("@IsforDownloadonly", Chk_CodeOp3.Checked);
                cmd.Parameters.AddWithValue("@Instancename", ddlinstance.SelectedItem.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                foreach (GridViewRow item in GridView2.Rows)
                {
                    Label lblwebid = (Label)(item.FindControl("lblwebid"));
                    CheckBox cbItem = (CheckBox)(item.FindControl("cbItem"));
                    if (cbItem.Checked == true)
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        SqlCommand cmd1 = new SqlCommand("ProductCodeDatabasDetailWithWebsiteID_AddDelUpdtSelect", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@StatementType", "Insert");
                        cmd1.Parameters.AddWithValue("@ProductCodeDatabasDetailID", ViewState["databaseid"]);
                        cmd1.Parameters.AddWithValue("@WebsiteID", lblwebid.Text);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }

                if (Chk_CodeOp2.Checked == true)
                {
                    pnlbackupstatus.Visible = true;
                    SyncroTableAll(lbl_serverid.Text, ViewState["databaseid"].ToString(), ddlproductversion.SelectedValue);
                    FillTableGrid(ViewState["databaseid"].ToString());
                }
                
                if (Chk_CodeOp3.Checked == true)
                {
                    try
                    {
                        SqlCommand cmd1 = new SqlCommand(" DROP DATABASE [" + txtcodetypename.Text + "] ", connNewDB);
                        if (connNewDB.State.ToString() != "Open")
                        {
                            connNewDB.Open();
                        }
                        cmd1.ExecuteNonQuery();
                        connNewDB.Close();
                    }
                    catch
                    {
                    }
                }               
                clear();
                fillgrid();
                Panel1.Visible = false;
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = " Problem to attach database";
            }
        } 
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        if (ddlcodetypecategory.SelectedValue == "2")
        {
            string ccc = ViewState["changedata"].ToString();
            if (ccc == "1")
            {
                if (CheckBox11.Checked == true)
                {
                    SqlCommand sb = new SqlCommand("update ProductCodeDetailTbl set BusiwizSynchronization=0 where ProductId='" + ddlproductversion.SelectedValue + "' ", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    sb.ExecuteNonQuery();
                    con.Close();
                }
                if (CheckBox12.Checked == true)
                {
                    SqlCommand sb = new SqlCommand("update ProductCodeDetailTbl set CompanyDefaultData=0 where ProductId='" + ddlproductversion.SelectedValue + "' ", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    sb.ExecuteNonQuery();
                    con.Close();
                }
                ViewState["changedata"] = "1";
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
            cmd.Parameters.AddWithValue("@AdditionalPageInserted", false);
            cmd.Parameters.AddWithValue("@BusiwizSynchronization", CheckBox11.Checked);
            cmd.Parameters.AddWithValue("@CompanyDefaultData", CheckBox12.Checked);
            cmd.Parameters.AddWithValue("@ProductId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.ExecuteNonQuery();
            con.Close();
            //-
            string strcln1 = "  select *  from CodeTypeTbl Where ProductCodeDetailId=" + ViewState["id"].ToString();
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "UpdateData");
            cmd.Parameters.AddWithValue("@Id", dtcln1.Rows[0][0].ToString());
            cmd.Parameters.AddWithValue("@ProductCodeDetailId", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text + "MDF");
            cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.Parameters.AddWithValue("@Temppath", txttemppathC.Text);
            cmd.Parameters.AddWithValue("@Outputpath", txtoutputsourcepathC.Text);           
            cmd.Parameters.AddWithValue("@FileLocationPath", txt_mdffilepath.Text);
            cmd.Parameters.AddWithValue("@FileName", txt_mdffilename.Text);
            cmd.Parameters.AddWithValue("@IsforDownloadonly", Chk_CodeOp3.Checked);
            cmd.Parameters.AddWithValue("@Instancename", ddlinstance.SelectedItem.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "UpdateData");
            cmd.Parameters.AddWithValue("@ID", dtcln1.Rows[1][0].ToString());
            cmd.Parameters.AddWithValue("@ProductCodeDetailId", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text + "LDF");
            cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.Parameters.AddWithValue("@Temppath", txttemppathC.Text);
            cmd.Parameters.AddWithValue("@Outputpath", txtoutputsourcepathC.Text);           
            cmd.Parameters.AddWithValue("@FileLocationPath", txt_ldffilepath.Text);
            cmd.Parameters.AddWithValue("@FileName", txt_ldffilename.Text);
            cmd.Parameters.AddWithValue("@IsforDownloadonly", Chk_CodeOp3.Checked);
            cmd.Parameters.AddWithValue("@Instancename", ddlinstance.SelectedItem.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            //

            //Website
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd = new SqlCommand("ProductCodeDatabasDetailWithWebsiteID_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@ProductCodeDatabasDetailID", ViewState["id"].ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            //
            foreach (GridViewRow item in GridView2.Rows)
            {
                Label lblwebid = (Label)(item.FindControl("lblwebid"));
                CheckBox cbItem = (CheckBox)(item.FindControl("cbItem"));
                if (cbItem.Checked == true)
                {
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmd1 = new SqlCommand("ProductCodeDatabasDetailWithWebsiteID_AddDelUpdtSelect", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@StatementType", "Insert");
                    cmd1.Parameters.AddWithValue("@ProductCodeDatabasDetailID", ViewState["id"]);
                    cmd1.Parameters.AddWithValue("@WebsiteID", lblwebid.Text);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
            }

           
        }
        pnlbackupstatus.Visible = true;        
        SyncroTableAll(lbl_serverid.Text, ViewState["id"].ToString(), ddlproductversion.SelectedValue);
        FillTableGrid(ViewState["id"].ToString());


        clear();
        btn_update.Visible = false;
        btn_submitCode.Visible = true;
        fillgrid();
        Panel1.Visible = false;
        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully";
    }    
    //--------------------------------------------------------------------   
    //-Grid Search---------------------------------------------------------
    protected void chk_Active_CheckedChanged(object sender, EventArgs e)
    {
        FillProductsearch();
    }
    protected void chkconnection_CheckedChanged(object sender, EventArgs e)
    {
        fillgrid();
    }   
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
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlcodetypecategory0_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
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
         DataTable dtsvr =MyCommonfile.selectBZ(" SELECT DISTINCT dbo.ProductCodeDetailTbl.Id,dbo.ProductCodeDetailTbl.Active,dbo.VersionInfoMaster.VersionInfoId, dbo.ClientMaster.ServerId,dbo.ProductCodeDetailTbl.Id as vv, dbo.ProductCodeDetailTbl.CodeTypeName , dbo.ProductMaster.ProductName, dbo.ProductMaster.ProductName + ':' + dbo.VersionInfoMaster.VersionInfoName AS VersionInfoName, dbo.CodeTypeCategory.CodeTypeCategory, dbo.ProductCodeDetailTbl.AdditionalPageInserted, dbo.ProductCodeDetailTbl.BusiwizSynchronization, dbo.ProductCodeDetailTbl.CompanyDefaultData FROM   dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.CodeTypeTbl.ProductVersionId INNER JOIN dbo.ProductMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ProductCodeDetailTbl.Id = dbo.CodeTypeTbl.ProductCodeDetailId INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' " + str + "");
       // SELECT dbo.ProductCodeDetailTbl.CodeTypeName, dbo.ProductMaster.ProductName + ':' + dbo.VersionInfoMaster.VersionInfoName AS VersionInfoName, dbo.CodeTypeCategory.CodeTypeCategory, dbo.ProductCodeDetailTbl.AdditionalPageInserted, dbo.ProductCodeDetailTbl.BusiwizSynchronization, dbo.ProductCodeDetailTbl.CompanyDefaultData, dbo.ProductCodeDetailTbl.Id AS vv FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId CROSS JOIN dbo.CodeTypeCategory CROSS JOIN dbo.ProductCodeDetailTbl
        GridView1.DataSource = dtsvr;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcodedetailID = (Label)e.Row.FindControl("Label7");
            LinkButton linl_totalnoofcompany = (LinkButton)e.Row.FindControl("linl_totalnoofcompany");


            Label lbldefaultcodename = (Label)e.Row.FindControl("lbldefaultcodename");
            Label lblmdffile = (Label)e.Row.FindControl("lblmdffile");
            Label lblldffile = (Label)e.Row.FindControl("lblldffile");
            ImageButton imgdbconn = (ImageButton)e.Row.FindControl("imgdbconn");  

            string strcln1 = "select * from CodeTypeTbl where ProductCodeDetailId=" + lblcodedetailID.Text + "";
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);
            if (dtcln1.Rows.Count > 0)
            {
                lblmdffile.Text = dtcln1.Rows[0]["FileLocationPath"].ToString() + "\\ " + dtcln1.Rows[0]["FileName"].ToString();                
               // txt_mdffilename.Text = dtcln1.Rows[0]["FileName"].ToString();
                lblldffile.Text = dtcln1.Rows[1]["FileLocationPath"].ToString() + "\\ " + dtcln1.Rows[1]["FileName"].ToString();
               // txt_ldffilename.Text = dtcln1.Rows[1]["FileName"].ToString();                
            }
            if (chkconnection.Checked == true)
            {
                Boolean status = false;
                DataTable dtdatabaseins = MyCommonfile.selectBZ(@"SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + lblcodedetailID.Text + "'");
                if (dtdatabaseins.Rows.Count > 0)
                {
                    string serverid = dtdatabaseins.Rows[0]["ServerId"].ToString();
                    string serversqlinstancename = dtdatabaseins.Rows[0]["Instancename"].ToString();
                    SqlConnection conn1 = new SqlConnection();
                    conn1 = ServerWizard.ServerDatabaseFromInstanceTCP(serverid, serversqlinstancename, lbldefaultcodename.Text);
                    try
                    {
                        if (conn1.State.ToString() != "Open")
                        {
                            conn1.Open();
                            status = true;
                            imgdbconn.ImageUrl = "~/images/DatabaseConnection/DatabaseConnTrue.png";
                            imgdbconn.Visible = true;
                            
                        }
                    }
                    catch
                    {
                        imgdbconn.ImageUrl = "~/images/DatabaseConnection/DatabaseConnFalse.png";
                        imgdbconn.Visible = true;
                    }
                }
            }

        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
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
        string strcln = "select * from ProductCodeDetailTbl where Id=" + id.Text + "";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
           // ViewState["CodeDetailID"] = id.Text;

            FillProduct();
            ddlproductversion.SelectedValue = dtcln.Rows[0]["ProductId"].ToString();
            ddlproductversion.Enabled = false;
            fillproductid();

            FillWebsiteGrid();

            txtcodetypename.Text = dtcln.Rows[0]["CodeTypeName"].ToString();

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
            ddlcodetypecategory_SelectedIndexChanged(sender, e);
            try
            {
                Boolean IsforDownloadonly = Convert.ToBoolean(dtcln1.Rows[0]["IsforDownloadonly"].ToString());
                if (IsforDownloadonly == false)
                {
                    Chk_CodeOp2.Checked = true;
                    Chk_CodeOp3.Checked = false;
                    Chk_CodeOp2_CheckedChanged(sender, e);            
                }
                else
                {
                    Chk_CodeOp2.Checked = false;
                    Chk_CodeOp3.Checked = true;
                    Chk_CodeOp3_CheckedChanged(sender, e);            
                }
            }
            catch
            {
                Chk_CodeOp2.Checked = true;
                Chk_CodeOp3.Checked = false;
                Chk_CodeOp2_CheckedChanged(sender, e);            
            }
            txttemppathC.Text = dtcln1.Rows[0]["Temppath"].ToString();
            txtoutputsourcepathC.Text = dtcln1.Rows[0]["Outputpath"].ToString();
            ddlinstance.SelectedItem.Text = dtcln1.Rows[0]["Instancename"].ToString();
           

            txt_mdffilepath.Text = dtcln1.Rows[0]["FileLocationPath"].ToString();
            txt_mdffilename.Text = dtcln1.Rows[0]["FileName"].ToString();
            txt_ldffilepath.Text = dtcln1.Rows[1]["FileLocationPath"].ToString();
            txt_ldffilename.Text = dtcln1.Rows[1]["FileName"].ToString();
          
        }
        //---------------------------
        FillWebsiteGrid();      

        foreach (GridViewRow item in GridView2.Rows)
        {
            Label lblwebid = (Label)(item.FindControl("lblwebid"));
            //Label lblprodid = (Label)(item.FindControl("lblprodid"));
            CheckBox cbItem = (CheckBox)(item.FindControl("cbItem"));

            string strwebsite = " SELECT * From ProductCodeDatabasDetailWithWebsiteID Where  WebsiteID=" + lblwebid.Text + " and ProductCodeDatabasDetailID=" + ViewState["id"] + "  ";
            SqlCommand cmd12web = new SqlCommand(strwebsite, con);
            SqlDataAdapter adp12web = new SqlDataAdapter(cmd12web);
            DataTable ds12web = new DataTable();
            adp12web.Fill(ds12web);
            if (ds12web.Rows.Count == 0)
            {
                cbItem.Checked = false;
            }
        }
      
        btn_update.Visible = true;
        Panel1.Visible = true;
        btn_submitCode.Visible = false;
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
        string strcln = "select id from ClientProductTableMaster where Databaseid=" + id.Text + "";
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
            strcln = "select id from ProductMasterCodeTbl where CodeTypeID=" + id.Text + "";
            cmdcln = new SqlCommand(strcln, con);
            dtcln = new DataTable();
            adpcln = new SqlDataAdapter(cmdcln);
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
                con.Close();

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
                fillgrid();
            }
        }        
    }
    protected void imgattach_Click(object sender, ImageClickEventArgs e)
    {
        lblmsg.Visible = true;
        lblmsg.Text = "";
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label lbldatabaseid = (Label)GridView1.Rows[j].FindControl("Label7");

        Label lbldefaultcodename = (Label)GridView1.Rows[j].FindControl("lbldefaultcodename");
        Label lblmdffile = (Label)GridView1.Rows[j].FindControl("lblmdffile");
        Label lblldffile = (Label)GridView1.Rows[j].FindControl("lblldffile");
        Label lbl_versionId = (Label)GridView1.Rows[j].FindControl("lbl_versionId");
        Label lbl_ServerId = (Label)GridView1.Rows[j].FindControl("lbl_ServerId");
        ViewState["id"] = lbldatabaseid.Text;
        pnlbackupstatus.Visible = true;        
        SyncroTableAll(lbl_ServerId.Text, lbldatabaseid.Text, lbl_versionId.Text);
        FillTableGrid(lbldatabaseid.Text);
    }    
   //-************************************************************************************

   
  
    protected void chk_uploadcode_CheckedChanged(object sender, EventArgs e)
    {      
    }

    //Attach Database Syncro Table Colum
    public Boolean AttechDatabase()
    {
        Boolean result = true; 
        try
        {
            string serversqlinstancename = ddlinstance.SelectedItem.Text;
            //****************************************************************
                connNewDB = ServerWizard.ServerTwoInstanceTCPConncection(lbl_serverid.Text, serversqlinstancename);
                try
                {
                    if (connNewDB.State.ToString() != "Open")
                    {
                        connNewDB.Open();
                    }
                    try
                    {
                        if (Chk_CodeOp2.Checked == true || Chk_CodeOp3.Checked == true)
                        {
                            string mdffilepath = "" + txt_mdffilepath.Text + "\\" + txt_mdffilename.Text;
                            string ldffilepath = "" + txt_ldffilepath.Text + "\\" + txt_ldffilename.Text;
                            SqlCommand cmd1 = new SqlCommand("CREATE DATABASE [" + txtcodetypename.Text + "] ON ( FILENAME = N'" + mdffilepath + "' ),( FILENAME = N'" + ldffilepath + "' )FOR ATTACH", connNewDB);
                            cmd1.ExecuteNonQuery();
                            connNewDB.Close();                           
                            lbl_serverattach.Text = "Database attach successfully";
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        lbl_serverattach.Text = "Some problem in attaching database";
                        result = false;
                    }
                }
                catch
                {
                    lbl_serverattach.Text = "Some problem in Connectionstring " + connNewDB.ConnectionString;
                    result = false;
                }
            //*********************************************************
        }
        catch (Exception ex)
        {
            lbl_serverattach.Text = "We can't able to connect server for atteching database";
            result = false;
        }
        return result;
    }  
   
 
    //--------------
    //--Grid Popup--
    //-***********Table Grid web
    protected void SyncroTableAll(string serverid, string databaseid, string versionId)
    {
        try
        {
            lbl_databaseid.Text = databaseid;
            string serversqldbname = txtcodetypename.Text;
            SqlConnection connNewDB = new SqlConnection();
            connNewDB = ServerWizard.ServerDatabaseFromInstanceTCP(serverid, ddlinstance.SelectedItem.Text, serversqldbname);
            try
            {
                if (connNewDB.State.ToString() != "Open")
                {
                    connNewDB.Open();
                }
                encstr = "";
                int inv = 0;
                string strnew = "SELECT name FROM sys.Tables";
                SqlCommand cmdnew = new SqlCommand(strnew, connNewDB);
                SqlDataAdapter danew = new SqlDataAdapter(cmdnew);
                DataTable dtnew = new DataTable();
                danew.Fill(dtnew);
                if (dtnew.Rows.Count > 0)
                {
                    int countTable = 0;

                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        string srtr = " ";
                        DataTable dtr = MyCommonfile.selectBZ(" select * from ClientProductTableMaster where Databaseid='" + databaseid + "' and TableName='" + dtnew.Rows[i]["name"].ToString() + "' ");
                        if (dtr.Rows.Count == 0)
                        {
                            string st = "insert into ClientProductTableMaster(VersionInfoId,TableName ,TableTitle ,Databaseid ,Active) values('" + versionId + "','" + dtnew.Rows[i]["name"].ToString() + "','" + dtnew.Rows[i]["name"].ToString() + "','" + databaseid + "','1')  ";//select @@identity
                            SqlCommand cmdst = new SqlCommand(st, con);
                            object ob = new object();
                            ob = cmdst.ExecuteScalar();
                            countTable++;
                        }


                        //TAble Field
                        try
                        {
                            dtr = MyCommonfile.selectBZ(" select * from ClientProductTableMaster where Databaseid='" + databaseid + "' and TableName='" + dtnew.Rows[i]["name"].ToString() + "' ");

                            string stdel = "Delete From tablefielddetail where tableId='" + dtr.Rows[0]["Id"].ToString()  + "' ";
                            SqlCommand cmdstdel = new SqlCommand(stdel, con);
                            object obdel = new object();
                            obdel = cmdstdel.ExecuteScalar();

                            string strdata = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '" + dtnew.Rows[i]["name"].ToString() + "'";
                            SqlCommand cmddata = new SqlCommand(strdata, connNewDB);
                            SqlDataAdapter dadata = new SqlDataAdapter(cmddata);
                            DataTable dtdata = new DataTable();
                            dadata.Fill(dtdata);
                            for (int j = 0; j < dtdata.Rows.Count; j++)
                            {
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                string str12 = "insert into tablefielddetail (tableId,feildname,fieldtype,size,ORDINAL_POSITION, COLUMN_DEFAULT, IS_NULLABLE,CHARACTER_OCTET_LENGTH, NUMERIC_PRECISION,NUMERIC_PRECISION_RADIX,NUMERIC_SCALE,DATETIME_PRECISION,CHARACTER_SET_CATALOG,CHARACTER_SET_SCHEMA,CHARACTER_SET_NAME,COLLATION_CATALOG,COLLATION_SCHEMA,COLLATION_NAME,DOMAIN_CATALOG,DOMAIN_SCHEMA,DOMAIN_NAME) values ('" + dtr.Rows[0]["Id"].ToString() + "'," +
                                        "'" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "','" + dtdata.Rows[j]["DATA_TYPE"].ToString() + "', " +
                                        "'" + dtdata.Rows[j]["CHARACTER_MAXIMUM_LENGTH"] + "','" + dtdata.Rows[j]["ORDINAL_POSITION"].ToString() + "'," +
                                         "'" + dtdata.Rows[j]["COLUMN_DEFAULT"].ToString() + "','" + dtdata.Rows[j]["IS_NULLABLE"].ToString() + "'," +
                                         "'" + dtdata.Rows[j]["CHARACTER_OCTET_LENGTH"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_PRECISION"].ToString() + "'," +
                                         "'" + dtdata.Rows[j]["NUMERIC_PRECISION_RADIX"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_SCALE"].ToString() + "'," +
                                         "'" + dtdata.Rows[j]["DATETIME_PRECISION"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_CATALOG"].ToString() + "'," +
                                         "'" + dtdata.Rows[j]["CHARACTER_SET_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_NAME"].ToString() + "'," +
                                          "'" + dtdata.Rows[j]["COLLATION_CATALOG"].ToString() + "','" + dtdata.Rows[j]["COLLATION_SCHEMA"].ToString() + "'," +
                                          "'" + dtdata.Rows[j]["COLLATION_NAME"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_CATALOG"].ToString() + "'," +
                                           "'" + dtdata.Rows[j]["DOMAIN_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_NAME"].ToString() + "')";

                                SqlCommand cmd12 = new SqlCommand(str12, con);
                                cmd12.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                        }

                    }
                    lbl_serverattach.Text = Convert.ToString(countTable);
                    lbl_serverattach.Text += " table inserted in client database";
                }
            }
            catch (Exception ex)
            {
            }
        }
        catch (Exception e1)
        {
        }
    }



    protected void FillTableGrid(string databaseid)
    {
        lbl_databaseid.Text = databaseid;
        string strcln = " SELECT id ,TableName,Case when(ID IS NULL) then  cast ('1' as bit) else  cast('0' as bit) end as chk From ClientProductTableMaster where Databaseid='" + databaseid + "' ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        gv_table.DataSource = dtcln;
        gv_table.DataBind();
    }
    protected void ch1table_chachedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow item in gv_table.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void gv_table_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbltblid = (Label)e.Row.FindControl("lbltblid");          
            CheckBox cbItem = (CheckBox)(e.Row.FindControl("cbItem"));
            DataTable ds12web = MyCommonfile.selectBZ(" SELECT * From ProductDatabaseRequiredDataInTable Where  ProductCodeDatabasDetailID=" + lbl_databaseid.Text + " and TAbleID=" + lbltblid.Text + "");
            if (ds12web.Rows.Count > 0)
            {
                cbItem.Checked = true;
            }          
        }
    }
    protected void gv_table_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void gv_table_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_table.PageIndex = e.NewPageIndex;
        FillWebsiteGrid();
    }
    protected void gv_table_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
    }
    protected void gv_table_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btn_finish_Click(object sender, EventArgs e)
    {
        UpdateTAbleDelete();
        pnlbackupstatus.Visible = false;
        lblmsg.Text = "Successfully syncronice tables and field in client database";
        lbl_serverattach.Text = "";
    }
    protected void UpdateTAbleDelete()
    {
        foreach (GridViewRow item in gv_table.Rows)
        {
            Label lbltblname = (Label)(item.FindControl("lbltblname"));
            Label lbltblid = (Label)(item.FindControl("lbltblid"));
            CheckBox cbItem = (CheckBox)(item.FindControl("cbItem"));

            DataTable ds12web = MyCommonfile.selectBZ(" SELECT * From ProductDatabaseRequiredDataInTable Where  ProductCodeDatabasDetailID=" + lbl_databaseid.Text + " and TAbleID=" + lbltblid.Text + "  ");
            if (ds12web.Rows.Count == 0 && cbItem.Checked == true)
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd1 = new SqlCommand("ProductDatabaseRequiredDataInTable_AddDelUpdtSelect", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@StatementType", "Insert");
                cmd1.Parameters.AddWithValue("@ProductCodeDatabasDetailID", lbl_databaseid.Text);
                cmd1.Parameters.AddWithValue("@TAbleID", lbltblid.Text);
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            if (ds12web.Rows.Count > 0 && cbItem.Checked == false)
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("ProductDatabaseRequiredDataInTable_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Deleteusing2id");
                cmd.Parameters.AddWithValue("@TAbleID", lbltblid.Text);
                cmd.Parameters.AddWithValue("@ProductCodeDatabasDetailID", lbl_databaseid.Text);
                cmd.ExecuteNonQuery();
                con.Close();               
            }

        }
    }



   

    //
  
    //-***************************
    //Close Popup    
   
       
    protected void btn_uploadmdf_Click(object sender, EventArgs e)
    {
        try
        {
            bool valid = extmdf(fu_mdffile.FileName);
            txt_mdffilename.Text = fu_mdffile.FileName;
            if (valid == true)
            {
                if (fu_mdffile.HasFile == true)
                {
                    if (Directory.Exists(txtmastercodepathC.Text) == false)
                    {
                        Directory.CreateDirectory(txtmastercodepathC.Text);
                    }
                    fu_mdffile.PostedFile.SaveAs(txt_mdffilepath.Text +  "\\" + fu_mdffile.FileName);
                    lbl_uploadmdfsucc.Text = "file uploaded sucessfully";
                }
            }
        }
        catch (Exception ex)
        {
            lbl_uploadmdfsucc.Text = "file not uploaded";
        }
        
    }
    protected void btn_uploadldf_Click(object sender, EventArgs e)
    {
        try
        {
            bool valid = extldf(fu_ldffile.FileName);
            txt_ldffilename.Text = fu_ldffile.FileName;
            if (valid == true)
            {
                if (fu_ldffile.HasFile == true)
                {
                    if (Directory.Exists(txtmastercodepathC.Text) == false)
                    {
                        Directory.CreateDirectory(txtmastercodepathC.Text);
                    }
                    fu_ldffile.PostedFile.SaveAs(txt_ldffilepath.Text + "\\" + fu_ldffile.FileName);
                    lbl_uploadldfsucc.Text = "file uploaded sucessfully";
                }
            }
        }
        catch
        {
            lbl_uploadldfsucc.Text = "file not uploaded";
        }        
    }
  
  
    public bool extmdf(string filename)
    {
        string[] validFileTypes = { ".mdf", "MDF", "mdf" };
        string ext = System.IO.Path.GetExtension(filename);
        bool isValidFile = false;
        for (int i = 0; i < validFileTypes.Length; i++)
        {
            if (ext == "." + validFileTypes[i])
            {
                isValidFile = true;
                break;
            }
        }
        return isValidFile;
    }
    public bool extldf(string filename)
    {
        string[] validFileTypes = { ".ldf", "LDF", "ldf" };
        string ext = System.IO.Path.GetExtension(filename);
        bool isValidFile = false;
        for (int i = 0; i < validFileTypes.Length; i++)
        {
            if (ext == "." + validFileTypes[i])
            {
                isValidFile = true;
                break;
            }
        }
        return isValidFile;
    }
   
    //Table Sync Geid Part
    protected void Btn_pnlbackupstatus_Click(object sender, EventArgs e)
    {
        pnlbackupstatus.Visible = false;
    }
    //Hide
    protected void CheckBox11_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox11.Checked == true)
        {
            CheckBox12.Checked = false;
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
    protected void Button4_Click(object sender, EventArgs e)
    {
        CheckBox11.Checked = false;
        CheckBox12.Checked = false;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ViewState["changedata"] = "1";
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
}