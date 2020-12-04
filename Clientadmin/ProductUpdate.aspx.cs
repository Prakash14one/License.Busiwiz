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


public partial class ProductUpdateLastt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection conn;
    SqlConnection connserver;


    SqlConnection conmaster = new SqlConnection(ConfigurationManager.ConnectionStrings["masterfile"].ConnectionString);
    public static string encstr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ClientId"] = "35";
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {

            ViewState["sortOrder"] = "";
            if (Request.QueryString["Clid"] != null && Request.QueryString["fln"]!=null)
            {
                Session["ClientId"] = Request.QueryString["Clid"];
                DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName  inner join CodeTypeTbl on CodeTypeTbl.ProductVersionId=VersionInfoMaster.VersionInfoId   where ClientMasterId=" + Session["ClientId"].ToString() + "  and ProductDetail.Active='1'  and CodeTypeTbl.CodeTypeCategoryId='3' order  by productversion");
                ddlproductversion.DataSource = dtcln;
                ddlproductversion.DataValueField = "VersionInfoId";
                ddlproductversion.DataTextField = "productversion";
                ddlproductversion.DataBind();
                DataTable dtcln1 = selectBZ(" select * from CodeTypeCategory where CodeMasterNo In ('3') ");
                ddlcodetypecatefory.DataSource = dtcln1;
                ddlcodetypecatefory.DataValueField = "CodeMasterNo";
                ddlcodetypecatefory.DataTextField = "CodeTypeCategory";
                ddlcodetypecatefory.DataBind();
                int retal = 0;
                for (int i = 0; i < ddlproductversion.Items.Count; i++)
                {
                    ddlproductversion.SelectedIndex = i;
                    DataTable dtcln2 = selectBZ("select CodeTypeTbl.* from CodeTypeTbl inner join CodeTypeCategory on CodeTypeCategory.CodeMasterNo=CodeTypeTbl.CodeTypeCategoryId where CodeTypeTbl.ProductVersionId='" + ddlproductversion.SelectedValue + "' and CodeTypeTbl.CodeTypeCategoryId='" + ddlcodetypecatefory.SelectedValue + "' and CodeTypeTbl.Name='OADBSCRIPT' order  by CodeTypeTbl.Name");

                    ddlcodtypeforup.DataSource = dtcln2;
                    ddlcodtypeforup.DataValueField = "Id";
                    ddlcodtypeforup.DataTextField = "Name";
                    ddlcodtypeforup.DataBind();
                    fillcodeversion();
                    DataTable dtclnn = selectBZ(" select * from ClientMaster where ClientMasterId='" + Session["ClientId"].ToString() + "'");
                    if (dtclnn.Rows.Count > 0)
                    {

                        string fileall = "";
                        string mastersourcefilepath = "";
                        string defaltdrivepath = dtclnn.Rows[0]["DefaultSourceDrivePath"].ToString();
                        fileall = "Drop_" + Request.QueryString["fln"];
                        mastersourcefilepath = defaltdrivepath + "\\" + fileall;
                        if (System.IO.File.Exists(mastersourcefilepath))
                        {
                            try
                            {

                                ViewState["filename"] = fileall;
                                Button1_Click(sender, e);
                                retal = 1;
                            }
                            catch
                            {
                                retal = 0;
                            }
                        }
                        if (retal == 1)
                        {
                            fileall = "Create_" + Request.QueryString["fln"];
                            mastersourcefilepath = defaltdrivepath + "\\" + fileall;
                            if (System.IO.File.Exists(mastersourcefilepath))
                            {
                                try
                                {
                                    ViewState["filename"] = fileall;
                                    Button1_Click(sender, e);
                                }
                                catch
                                {
                                    retal = 0;
                                }
                            }
                        }
                       
                    }
                    int sync = 0;
                    if (retal == 1)
                    {
                        sync = 9;
                    }
                    else
                    {
                        sync = 0;
                    }
                    string scp = Request.QueryString["RTU"].ToString();
                    string scp1 = "";
                    if (scp.Contains("?") == true)
                    {
                         scp1 = scp.Substring(scp.LastIndexOf('?'));
                         scp = scp.Replace(scp1, "");
                    }
                 
                    string url = scp + "?Sy=" + sync;
                    Response.Redirect(url);
                }
            }
            else
            {
              

                FillProduct();
                fillcodetypecategory();
                fillCodetype();

                fillfilterproduct();
                fillfiltercodetype();

                ddlcodtypeforup_SelectedIndexChanged(sender, e);
            }

        }

    }

    protected void FillProduct()
    {       
        DataTable dtcln=new DataTable();
      
         dtcln = selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + "  and ProductDetail.Active='1' order  by productversion");
      
        ddlproductversion.DataSource = dtcln;
        ddlproductversion.DataValueField = "VersionInfoId";
        ddlproductversion.DataTextField = "productversion";
        ddlproductversion.DataBind();




    }
    protected void fillCodetype()
    {
        DataTable dtcln = selectBZ("select CodeTypeTbl.* from CodeTypeTbl inner join CodeTypeCategory on CodeTypeCategory.CodeMasterNo=CodeTypeTbl.CodeTypeCategoryId where CodeTypeTbl.ProductVersionId='" + ddlproductversion.SelectedValue + "' and CodeTypeTbl.CodeTypeCategoryId='" + ddlcodetypecatefory.SelectedValue + "' order  by CodeTypeTbl.Name");

        ddlcodtypeforup.DataSource = dtcln;
        ddlcodtypeforup.DataValueField = "Id";
        ddlcodtypeforup.DataTextField = "Name";
        ddlcodtypeforup.DataBind();
        fillcodeversion();

    }
    protected void fillfilterproduct()
    {
        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion");

        FilterProductname.DataSource = dtcln;
        FilterProductname.DataValueField = "VersionInfoId";
        FilterProductname.DataTextField = "productversion";
        FilterProductname.DataBind();
        FilterProductname.Items.Insert(0, "All");
        FilterProductname.Items[0].Value = "0";
    }
   protected void fillfiltercodetype()
    {
        DataTable dtcln = selectBZ("select CodeTypeTbl.* from CodeTypeTbl inner join CodeTypeCategory on CodeTypeCategory.CodeMasterNo=CodeTypeTbl.CodeTypeCategoryId where ProductVersionId='" + FilterProductname.SelectedValue+ "' order  by CodeTypeTbl.Name");

        ddlctype.DataSource = dtcln;
        ddlctype.DataValueField = "Id";
        ddlctype.DataTextField = "Name";
        ddlctype.DataBind();
        ddlctype.Items.Insert(0, "All");
        ddlctype.Items[0].Value = "0";

    }
    protected void fillcodetypecategory()
    {
        DataTable dtcln = selectBZ(" select * from CodeTypeCategory where CodeMasterNo In ('2','3') ");
        ddlcodetypecatefory.DataSource = dtcln;
        ddlcodetypecatefory.DataValueField = "CodeMasterNo";
        ddlcodetypecatefory.DataTextField = "CodeTypeCategory";
        ddlcodetypecatefory.DataBind();
    }
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
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
    protected void Button2_Click(object sender, EventArgs e)
    {

        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lblmsg.Text = "";
        lbllegend.Text = "";
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;

        }
        else
        {



            Button3.Text = "Printable Version";
            Button4.Visible = false;

        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        lbllegend.Text = "Add New Product Update";
        lblmsg.Text = "";
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string ext = "";
        string[] validFileTypes = { "zip", "ZIP", "SQL", "sql", "mdf" , "MDF" ,"LDF" , "ldf" };
        bool isValidFile = false;

        if (fileup.HasFile == true)
        {

            ext = System.IO.Path.GetExtension(fileup.PostedFile.FileName);
            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }

            }
        }
        if (Request.QueryString["Clid"] != null)
        {
            isValidFile = true;
        }
        if (!isValidFile)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Invalid File. Please upload a File with extension " +
            string.Join(",", validFileTypes);

        }
        else
        {
            string filepath = "";
            string filnames = "";
            if (fileup.HasFile || Request.QueryString["Clid"] != null)
            {


                string productversionid = ddlproductversion.SelectedValue;
                string codecategoryno = ddlcodetypecatefory.SelectedValue;
                string codetypeid = ddlcodtypeforup.SelectedValue;
                string codeversionno = lblnewcodetypeNo.Text;
                string dateformat = "";
                string uploadfilename = "";
                if (Request.QueryString["Clid"] == null)
                {
                    dateformat = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                    uploadfilename = fileup.FileName;

                    filnames = productversionid + "_" + codecategoryno + "_" + codetypeid + "_" + codeversionno + "_" + dateformat + uploadfilename;
                    fileup.PostedFile.SaveAs(Server.MapPath("~\\Attach\\") + filnames);
                    filepath = Server.MapPath("~\\Attach\\") + filnames;
                }
                else
                {
                 
                    filnames = ViewState["filename"].ToString();

                }
              


                string strcln = " select * from ClientMaster where ClientMasterId='" + Session["ClientId"].ToString() + "'";
                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);


                if (dtcln.Rows.Count > 0)
                {
                    string defaltdrivepath = dtcln.Rows[0]["DefaultSourceDrivePath"].ToString();
                    string companyname = dtcln.Rows[0]["CompanyName"].ToString();
                    string mastersourcefilepath = defaltdrivepath + "\\" + filnames;  
                    
                    // Copy files
                    try
                    {
                        if (Request.QueryString["Clid"] == null)
                        {
                            File.Copy(filepath, mastersourcefilepath, true);
                        }
                        else
                        {
                            filepath = Server.MapPath("~\\Attach\\") + filnames;
                            File.Copy(mastersourcefilepath, filepath, true);
                        }
                    }
                    catch
                    {

                    }
                    // end copy files      
                                
                    insert(Convert.ToInt32(ddlproductversion.SelectedValue), Convert.ToInt32(ddlcodtypeforup.SelectedValue), Convert.ToInt32(lblnewcodetypeNo.Text), filnames, mastersourcefilepath, filepath);

                    SqlCommand cmdsq = new SqlCommand("Insert into ProductMasterLatestcodeversioninfoTBl(Productveriontbl,CodeVersion,CodeTypeID)Values('" + ddlproductversion.SelectedValue + "','" + lblnewcodetypeNo.Text + "','" + ddlcodtypeforup.SelectedValue + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdsq.ExecuteNonQuery();
                    con.Close();


                    DataTable dtrsc = selectBZ("Select Max(Id) as Id,Productveriontbl,CodeTypeID,CodeVersion  from ProductMasterLatestcodeversioninfoTBl where Productveriontbl='" + ddlproductversion.SelectedValue + "' and  CodeVersion='" + lblnewcodetypeNo.Text + "' and CodeTypeID='" + ddlcodtypeforup.SelectedValue + "' Group by Productveriontbl,CodeTypeID,CodeVersion");//ddlcodetypecatefory



                    string strgetserverid = "select distinct ServerId from ServerAssignmentMasterTbl where VersionId='" + ddlproductversion.SelectedValue + "'";
                    SqlCommand cmdgetserverid = new SqlCommand(strgetserverid, con);
                    DataTable dtgetserverid = new DataTable();
                    SqlDataAdapter adpgetserverid = new SqlDataAdapter(cmdgetserverid);
                    adpgetserverid.Fill(dtgetserverid);

                    if (dtgetserverid.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtgetserverid.Rows)
                        {
                            string serverid = dr["ServerId"].ToString();

                            string strftpdetail = " SELECT * from ServerMasterTbl where Id='" + serverid + "'";
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

                                connserver = new SqlConnection();
                                connserver.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";

                                try
                                {
                                    SqlCommand cmdsx = new SqlCommand("Insert into ProductMasterLatestcodeversioninfoTBl(Id,Productveriontbl,CodeVersion,CodeTypeID)Values('" + Convert.ToString(dtrsc.Rows[0]["Id"]) + "','" + Convert.ToString(dtrsc.Rows[0]["Productveriontbl"]) + "','" + Convert.ToString(dtrsc.Rows[0]["CodeVersion"]) + "','" + Convert.ToString(dtrsc.Rows[0]["CodeTypeID"]) + "')", connserver);
                                    if (connserver.State.ToString() != "Open")
                                    {
                                        connserver.Open();
                                    }
                                    cmdsx.ExecuteNonQuery();
                                    connserver.Close();
                                }
                                catch (Exception ex)
                                {
                                   
                                }
                            }
                        }


                    }

                    if (Convert.ToInt32(ddlcodetypecatefory.SelectedValue) == 3)
                    {
                        try
                        {

                            databaseattachdetach(Convert.ToInt32(ddlcodtypeforup.SelectedValue), Convert.ToInt32(ddlproductversion.SelectedValue), mastersourcefilepath, lblnewcodetypeNo.Text);
                        }
                        catch
                        {

                        }
                    }

                   
                    Button2_Click1(sender, e);
                    fillcodeversion();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Product setup uploaded successfully";
                    
                }
            }
        }
    }
    private static string Encrypt(string strtxt, string strtoencrypt)
    {
        byte[] bykey = new byte[20];
        byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bykey = System.Text.Encoding.UTF8.GetBytes(strtoencrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strtxt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, dv), CryptoStreamMode.Write);
            cs.Write(inputArray, 0, inputArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            return strtxt;
            //  throw ex;
        }

    }
    public static string Encrypted(string strText)
    {

        return Encrypt(strText, encstr);

    }

    protected void insert(int ProductVerID, int CodeTypeID, int codeversionnumber, string filename, string physicalpath, string TemporaryPath)
    {
        string strinsert = "Insert into ProductMasterCodeTbl(ProductVerID,CodeTypeID,codeversionnumber,filename,physicalpath,TemporaryPath) values ('" + ProductVerID + "','" + CodeTypeID + "','" + codeversionnumber + "','" + filename + "','" + physicalpath + "','" + TemporaryPath + "')";
        SqlCommand cmdinsert = new SqlCommand(strinsert, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdinsert.ExecuteNonQuery();
        con.Close();


        string strmaxid = "select Max(ID) as ID from ProductMasterCodeTbl where CodeTypeID='" + CodeTypeID + "' ";
        SqlCommand cmdmaxid = new SqlCommand(strmaxid, con);
        DataTable dtmaxid = new DataTable();
        SqlDataAdapter adpmaxid = new SqlDataAdapter(cmdmaxid);
        adpmaxid.Fill(dtmaxid);

        if (dtmaxid.Rows.Count > 0)
        {

            string strgetserverid = "select distinct ServerId from ServerAssignmentMasterTbl where VersionId='" + ProductVerID + "'";
            SqlCommand cmdgetserverid = new SqlCommand(strgetserverid, con);
            DataTable dtgetserverid = new DataTable();
            SqlDataAdapter adpgetserverid = new SqlDataAdapter(cmdgetserverid);
            adpgetserverid.Fill(dtgetserverid);

            if (dtgetserverid.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgetserverid.Rows)
                {
                    string serverid = dr["ServerId"].ToString();

                    string strftpdetail = " SELECT * from ServerMasterTbl where Id='" + serverid + "'";
                    SqlCommand cmdftpdetail = new SqlCommand(strftpdetail, con);
                    DataTable dtftpdetail = new DataTable();
                    SqlDataAdapter adpftpdetail = new SqlDataAdapter(cmdftpdetail);
                    adpftpdetail.Fill(dtftpdetail);

                    if (dtftpdetail.Rows.Count > 0)
                    {
                        string ftpphysicalpath = dtftpdetail.Rows[0]["folderpathformastercode"].ToString() + "\\" + filename;

                        string serversqlserverip = dtftpdetail.Rows[0]["sqlurl"].ToString();
                        string serversqlinstancename = dtftpdetail.Rows[0]["DefaultsqlInstance"].ToString();
                        string serversqldbname = dtftpdetail.Rows[0]["DefaultDatabaseName"].ToString();
                        string serversqlpwd = dtftpdetail.Rows[0]["Sapassword"].ToString();
                        string serversqlport = dtftpdetail.Rows[0]["port"].ToString();

                        connserver = new SqlConnection();
                        connserver.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";



                        try
                        {
                            string strsatelliteserverinsert = "Insert into ProductMasterCodeonsatelliteserverTbl(ProductMastercodeID,ServerID,Successfullyuploadedtoserver,Physicalpath,filename) values ('" + dtmaxid.Rows[0]["ID"].ToString() + "','" + serverid + "','0','" + ftpphysicalpath + "','" + filename + "')";
                            SqlCommand cmdsatelliteserverinsert = new SqlCommand(strsatelliteserverinsert, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdsatelliteserverinsert.ExecuteNonQuery();
                            con.Close();

                            DataTable dtrsc = selectBZ("Select Max(ID) as ID  from ProductMasterCodeonsatelliteserverTbl where ServerID='" + serverid + "' and ProductMastercodeID='" + dtmaxid.Rows[0]["ID"].ToString() + "' ");


                            string strserverinsert = "Insert into ProductMasterCodeonsatelliteserverTbl(ID,ProductMastercodeID,ServerID,Successfullyuploadedtoserver,Physicalpath,filename,DownloadStart,DownloadFinish) values ('" + dtrsc.Rows[0]["ID"].ToString() + "','" + dtmaxid.Rows[0]["ID"].ToString() + "','" + serverid + "','0','" + ftpphysicalpath + "','" + filename + "','0','0')";
                            SqlCommand cmdserverinsert = new SqlCommand(strserverinsert, connserver);
                            if (connserver.State.ToString() != "Open")
                            {
                                connserver.Open();
                            }
                            cmdserverinsert.ExecuteNonQuery();
                            connserver.Close();


                        }
                        catch
                        {
                            

                        }

                    }
                }
            }



        }


    }
    protected void ddlcodtypeforup_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcodeversion();
    }
    protected void fillcodeversion()
    {
        DataTable dtv = selectBZ("select Max(codeversionnumber) as codeversionnumber from ProductMasterCodeTbl where CodeTypeID='" + ddlcodtypeforup.SelectedValue + "' and ProductVerID='" + ddlproductversion.SelectedValue + "'");
        if (dtv.Rows.Count > 0)
        {
            if (Convert.ToString(dtv.Rows[0]["codeversionnumber"]) != "")
            {
                lblnewcodetypeNo.Text = (Convert.ToInt32(dtv.Rows[0]["codeversionnumber"]) + 1).ToString();
            }
            else
            {
                lblnewcodetypeNo.Text = "1";
            }
        }
        else
        {
            lblnewcodetypeNo.Text = "1";
        }
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        string stcper = "";


        if (FilterProductname.SelectedIndex > 0)
        {
            stcper = stcper + " and ProductMasterCodeTbl.ProductVerID ='" + FilterProductname.SelectedValue + "'";
        }
        if (ddlctype.SelectedIndex > 0)
        {
            stcper = stcper + " and CodeTypeID ='" + ddlctype.SelectedValue + "'";
        }


        DataTable dtsvr = selectBZ("select codeversionnumber as CodeVersion,CodeTypeTbl.Name as CodeType, ProductMasterCodeTbl.Id,codeversionnumber,ProductMasterCodeTbl.filename,physicalpath as FileLocation,TemporaryPath,ProductMaster.ProductName +':'+VersionInfoMaster.VersionInfoName as VersionInfoName  from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId " +
      " inner join ProductMasterCodeTbl on ProductMasterCodeTbl.ProductVerID=VersionInfoMaster.VersionInfoId inner join CodeTypeTbl on CodeTypeTbl.Id=ProductMasterCodeTbl.CodeTypeID where ProductMaster.ClientMasterId='" + Session["ClientId"] + "'" + stcper);


        GridView1.DataSource = dtsvr;

        GridView1.DataBind();
    }
    protected void databaseattachdetach(int codetypeid, int productversionid, string scriptpath,string codeversion)
    {

        DataTable dtscripttype =selectBZ(" select ProductCodeDetailTbl.* from ProductMasterCodeTbl inner join CodeTypeTbl on CodeTypeTbl.ID=ProductMasterCodeTbl.CodeTypeID inner join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=CodeTypeTbl.ProductCodeDetailId where ProductMasterCodeTbl.ProductVerID='" + productversionid + "' and ProductMasterCodeTbl.CodeTypeID='" + codetypeid + "' and ProductMasterCodeTbl.codeversionnumber='" + codeversion + "'");

        if (dtscripttype.Rows.Count > 0)
        {
            string codetypename = dtscripttype.Rows[0]["CodeTypeName"].ToString();

            string strfindldf = "select ProductMasterCodeTbl.ProductVerID,ProductMasterCodeTbl.CodeTypeID,Max(ProductMasterCodeTbl.codeversionnumber) as codeversionnumber,ProductCodeDetailTbl.CodeTypeName  from ProductMasterCodeTbl  inner join CodeTypeTbl on CodeTypeTbl.ID=ProductMasterCodeTbl.CodeTypeID inner join CodeTypeCategory on CodeTypeCategory.CodeMasterNo=CodeTypeTbl.CodeTypeCategoryId inner join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=CodeTypeTbl.ProductCodeDetailId  where ProductMasterCodeTbl.ProductVerID='" + productversionid + "'  and CodeTypeCategory.CodeMasterNo In ('2') and ProductCodeDetailTbl.CodeTypeName='" + codetypename + "' group by ProductMasterCodeTbl.ProductVerID,ProductMasterCodeTbl.CodeTypeID,ProductCodeDetailTbl.CodeTypeName";
            SqlCommand cmdfindldf = new SqlCommand(strfindldf, con);
            SqlDataAdapter adpfindldf = new SqlDataAdapter(cmdfindldf);
            DataTable dsfindldf = new DataTable();
            adpfindldf.Fill(dsfindldf);

            if (dsfindldf.Rows.Count > 0)
            {
                string mdffilename = "";
                string mdffilepath = "";
                string temporarymdfpath = "";
                int mdfnewversion = 0;
                int mdfcodetype = 0;

                string ldffilename = "";
                string ldffilepath = "";
                string temporaryldfpath = "";
                int ldfnewversion = 0;
                int ldfcodetype = 0;
               



                foreach (DataRow drldffile in dsfindldf.Rows)
                {

                    string strldffilecomp = "select * from ProductMasterCodeTbl  where ProductMasterCodeTbl.ProductVerID='" + drldffile["ProductVerID"].ToString() + "' and ProductMasterCodeTbl.CodeTypeID='" + drldffile["CodeTypeID"].ToString() + "' and ProductMasterCodeTbl.codeversionnumber='" + drldffile["codeversionnumber"].ToString() + "' ";
                    SqlCommand cmdldffilecomp = new SqlCommand(strldffilecomp, con);
                    SqlDataAdapter adpldffilecomp = new SqlDataAdapter(cmdldffilecomp);
                    DataTable dsldffilecomp = new DataTable();
                    adpldffilecomp.Fill(dsldffilecomp);

                    if (dsldffilecomp.Rows.Count > 0)
                    {
                        string getldffilename = dsldffilecomp.Rows[0]["filename"].ToString();
                        string ldffilexten = Path.GetExtension(getldffilename);

                        if (ldffilexten == ".LDF" || ldffilexten == ".ldf")
                        {
                            ldffilename = dsldffilecomp.Rows[0]["filename"].ToString();
                            ldffilepath = dsldffilecomp.Rows[0]["physicalpath"].ToString();
                            temporaryldfpath = dsldffilecomp.Rows[0]["TemporaryPath"].ToString();
                            ldfnewversion = Convert.ToInt32(dsldffilecomp.Rows[0]["codeversionnumber"].ToString()) + 1;
                            mdfcodetype = Convert.ToInt32(dsldffilecomp.Rows[0]["CodeTypeID"].ToString());
                        }

                        if (ldffilexten == ".MDF" || ldffilexten == ".mdf")
                        {
                            mdffilename = dsldffilecomp.Rows[0]["filename"].ToString();
                            mdffilepath = dsldffilecomp.Rows[0]["physicalpath"].ToString();
                            temporarymdfpath = dsldffilecomp.Rows[0]["TemporaryPath"].ToString();
                            mdfnewversion = Convert.ToInt32(dsldffilecomp.Rows[0]["codeversionnumber"].ToString()) + 1;
                            ldfcodetype = Convert.ToInt32(dsldffilecomp.Rows[0]["CodeTypeID"].ToString());
                            
                        }

                    }

                }

                if (mdffilepath != "" && ldffilepath != "")
                {


                    string databasename = "OAmasterblank";


                    SqlConnection condatupd = new SqlConnection(@"Data Source =tcp:192.168.1.219,2810; Initial Catalog = " + databasename + "; Integrated Security=True");

                    try
                    {

                        SqlCommand cmd = new SqlCommand("CREATE DATABASE " + databasename + " ON ( FILENAME = N'" + mdffilepath + "' ),( FILENAME = N'" + ldffilepath + "' )FOR ATTACH", conmaster);
                        if (conmaster.State.ToString() != "Open")
                        {
                            conmaster.Open();
                        }
                        cmd.ExecuteNonQuery();
                        conmaster.Close();

                        try
                        {
                            if (condatupd.State.ToString() != "Open")
                            {
                                condatupd.Open();
                            }

                            FileInfo fl = new FileInfo(scriptpath);
                            string csript = fl.OpenText().ReadToEnd();
                            Server SERV = new Server(new ServerConnection(condatupd));
                            SERV.ConnectionContext.ExecuteNonQuery(csript);
                        }
                        catch
                        {
                            // throw ex;
                            condatupd.Close();
                        }
                        try
                        {
                            // ALTER DATABASE [2056.EXTMSGDB] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE

                            SqlCommand cmdsinglerole = new SqlCommand(" ALTER DATABASE " + databasename + " SET  SINGLE_USER WITH ROLLBACK IMMEDIATE ", conmaster);
                            if (conmaster.State.ToString() != "Open")
                            {
                                conmaster.Open();
                            }
                            cmdsinglerole.ExecuteNonQuery();
                            conmaster.Close();

                            SqlCommand cmddetach = new SqlCommand("EXEC master.dbo.sp_detach_db @dbname = " + databasename + ", @skipchecks = 'false'", conmaster);
                            if (conmaster.State.ToString() != "Open")
                            {
                                conmaster.Open();
                            }
                            cmddetach.ExecuteNonQuery();
                            conmaster.Close();
                        }
                        catch
                        {
                            SqlCommand cmddetach = new SqlCommand("EXEC master.dbo.sp_detach_db @dbname = " + databasename + ", @skipchecks = 'false'", conmaster);
                            if (conmaster.State.ToString() != "Open")
                            {
                                conmaster.Open();
                            }
                            cmddetach.ExecuteNonQuery();
                            conmaster.Close();
                        }

                        //mdffilepath
                        //ldffilepath

                        string tempfolder = "i:\\TestData\\TempDatabase";

                        File.Copy(mdffilepath, tempfolder + "\\" + mdffilename, true);
                        File.Copy(ldffilepath, tempfolder + "\\" + ldffilename, true);

                        File.Delete(mdffilepath);
                        File.Delete(ldffilepath);

                        File.Copy(tempfolder + "\\" + mdffilename, mdffilepath, true);
                        File.Copy(tempfolder + "\\" + ldffilename, ldffilepath, true);



                        insert(productversionid, mdfcodetype, mdfnewversion, mdffilename, mdffilepath, temporarymdfpath);


                        SqlCommand cmdsq = new SqlCommand("Insert into ProductMasterLatestcodeversioninfoTBl(Productveriontbl,CodeVersion,CodeTypeID)Values('" + productversionid + "','" + mdfnewversion + "','" + mdfcodetype + "')", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdsq.ExecuteNonQuery();
                        con.Close();



                        insert(productversionid, ldfcodetype, ldfnewversion, ldffilename, ldffilepath, temporaryldfpath);

                        SqlCommand cmdldfversion = new SqlCommand("Insert into ProductMasterLatestcodeversioninfoTBl(Productveriontbl,CodeVersion,CodeTypeID)Values('" + productversionid + "','" + ldfnewversion + "','" + ldfcodetype + "')", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdldfversion.ExecuteNonQuery();
                        con.Close();
                    }
                    catch
                    {
                    }

                }

            }
        }

      


      


    }
    protected void ddlproductversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCodetype();
        //  fillcodeversion();
    }
    protected void ddlcodetypecatefory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCodetype();
        // fillcodeversion();
    }
    protected void FilterProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfiltercodetype();
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


        DataTable dt1 = select("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ( ClientProductTableMaster.TableName='CompanyProductUpdateStatusTbl' OR ClientProductTableMaster.TableName='ProductMasterLatestcodeversioninfoTBl'  )");
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

        }
        if (transf > 0)
        {
            string te = "SyncData.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
}
