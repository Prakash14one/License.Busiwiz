using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.IO.Compression;
using System.IO;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using System.Configuration;
using System.Data;
public partial class Publish_Uncompiled_ : System.Web.UI.Page
{
   
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
        SqlConnection conn;
        SqlConnection connserver;

        public static string encstr = "";

        public static double size = 0;
        public static double sizeout = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (!Page.IsPostBack)
            {
              
               // Move("E:\\PRAKASH\\2820 SERVER PROJECT\\License.busiwiz.com\\Bib\\Web1.Config", "E:\\PRAKASH\\2820 SERVER PROJECT\\License.busiwiz.com\\Web1.Config");
                FillProduct();
                fillcodetypecategory();
                fillcodetype();
                fillgrid();
                FillgridFTP();

            }


        }

        protected void FillgridFTP()
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
                string pass= ds.Rows[0]["FTPPassword"].ToString();
                string port = ds.Rows[0]["FTPPort"].ToString();
                if (ftp == null || user == null || pass == null || port == null)
                {
                    lblFTPCheck1.Text = " Yo have not set the MasterFTP account of the client for syncronisation with server Please set from the ";
                    lblFTPCheck2.Visible = false;  
                    lblservername.Visible=false;
                    lblusername.Visible = false;
                    Label15.Visible = false;
                    Label16.Visible = false;
                    alinkup.Visible = false;  
                }
                else
                {
                    lblFTPCheck1.Text = "Ensure that the following FTP account which is client's main FTP account mentioned inPage  ";
                   // ClientmasterFTPUpdate.aspx
                      lblFTPCheck2.Text=" is linked to the above Output folder mentioned namely  ";
                      lblservername.Text = ftp +"   ";
                      lblusername.Text =  user;                     
                }                
            }
            else
            {
            }

        }
        protected void FillProduct()
        {

            string strcln = " SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName +':'+ VersionInfoMaster.VersionInfoName as ProductName,VersionInfoMaster.VersionInfoId  FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  order  by ProductName";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlproductname.DataSource = dtcln;
            ddlproductname.DataValueField = "VersionInfoId";
            ddlproductname.DataTextField = "ProductName";
            ddlproductname.DataBind();

            fillpath();


        }
        protected void fillcodetype()
        {
            string strcln = "select CodeTypeTbl.* from CodeTypeTbl inner join CodeTypeCategory on CodeTypeCategory.CodeMasterNo=CodeTypeTbl.CodeTypeCategoryId where CodeTypeTbl.ProductVersionId='" + ddlproductname.SelectedValue + "' and CodeTypeTbl.CodeTypeCategoryId='" + ddlcodetypecatefory.SelectedValue + "' order  by CodeTypeTbl.Name";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlcodetype.DataSource = dtcln;
            ddlcodetype.DataValueField = "ID";
            ddlcodetype.DataTextField = "Name";
            ddlcodetype.DataBind();

            findnewcodeversion();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            string productversionid = ddlproductname.SelectedValue;
            string codecategoryno = ddlcodetypecatefory.SelectedValue;
            string codetypeid = ddlcodetype.SelectedValue;
            string codeversionno = lblnewcodetypeNo.Text;

            string newfoldername = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string dateformat = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

            string insidefoldername = "";


            string strbasefilename = " select * from ProductMasterCodeTbl where ProductVerID='" + ddlproductname.SelectedValue + "' and CodeTypeID='" + ddlcodetype.SelectedValue + "' and codeversionnumber=(select Min(codeversionnumber) from ProductMasterCodeTbl where ProductVerID='" + ddlproductname.SelectedValue + "' and CodeTypeID='" + ddlcodetype.SelectedValue + "') ";
            SqlCommand cmdbasefilename = new SqlCommand(strbasefilename, con);
            DataTable dtbasefilename = new DataTable();
            SqlDataAdapter adpbasefilename = new SqlDataAdapter(cmdbasefilename);
            adpbasefilename.Fill(dtbasefilename);

            if (dtbasefilename.Rows.Count > 0)
            {
                string tempfilename = dtbasefilename.Rows[0]["filename"].ToString();
                string unzipfilename = tempfilename.Replace(".zip", "");//Hide 30MarchBecause If Remove.Zip Extension then Not copy File to Server Master Folder
                //string unzipfilename = tempfilename;
                insidefoldername = unzipfilename;
            }
            else
            {
                insidefoldername = productversionid + "_" + codecategoryno + "_" + codetypeid + "_" + codeversionno + "_" + dateformat;
            }


            string filename = insidefoldername +".zip";

            try
            {

                if (Directory.Exists(txtoutputsourcepath.Text + "\\" + newfoldername))
                {

                }
                else
                {
                    Directory.CreateDirectory(txtoutputsourcepath.Text + "\\" + newfoldername);
                }

                if (Directory.Exists(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername))
                {

                }
                else
                {
                    Directory.CreateDirectory(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername);
                }
                if (Directory.Exists(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + "\\" + insidefoldername))
                {

                }
                else
                {
                    Directory.CreateDirectory(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + "\\" + insidefoldername);
                }

            }
            catch
            {

            }



            string physicalpath = txtsourcepath.Text;

            try
            {
                if (File.Exists(physicalpath + ".zip"))
                {
                    File.Delete(physicalpath + ".zip");
                }
            }
            catch
            {
            }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(physicalpath);
                zip.Save(physicalpath + ".zip");
            }

            if (Directory.Exists(txttemppath.Text + "\\" + newfoldername))
            {

            }
            else
            {
                Directory.CreateDirectory(txttemppath.Text + "\\" + newfoldername);
            }


            string temppath = txttemppath.Text + "\\" + newfoldername;
            string outputpath = txtoutputsourcepath.Text + "\\" + newfoldername+ "\\" + insidefoldername + "\\" + insidefoldername;

            using (ZipFile zip = ZipFile.Read(physicalpath + ".zip"))
            {
                zip.ExtractAll(temppath, ExtractExistingFileAction.OverwriteSilently);
                zip.ExtractAll(outputpath, ExtractExistingFileAction.OverwriteSilently);


            }

            File.Delete(physicalpath + ".zip");

            try
            {               
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\ShoppingCart\\Admin\\VersionFolder", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\ShoppingCart\\Developer", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\Party\\VersionFolder", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\_vti_cnf", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\Account\\1133", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\VersionFolder", true);
                //Directory.Delete(txttemppath.Text + "\\" + newfoldername + "\\Attach", true);

                //File.Delete(txttemppath.Text + "\\" + newfoldername + "\\bin\\businesssetup.dll");
                //File.Delete(txttemppath.Text + "\\" + newfoldername + "\\bin\\documentrule.dll");
                //File.Delete(txttemppath.Text + "\\" + newfoldername + "\\bin\\presensenote.dll");
                try
                {
                    Directory.Delete(outputpath + "\\ShoppingCart\\Admin\\VersionFolder", true);
                    Directory.Delete(outputpath + "\\ShoppingCart\\Developer", true);
                    Directory.Delete(outputpath + "\\Party\\VersionFolder", true);
                    Directory.Delete(outputpath + "\\_vti_cnf", true);
                    Directory.Delete(outputpath + "\\Account", true);
                    Directory.Delete(outputpath + "\\VersionFolder", true);
                    Directory.Delete(outputpath + "\\Attach", true);
                    Directory.Delete(outputpath + "\\_vti_cnf", true);

                }
                catch (Exception ex)
                {
                }
                File.Delete(outputpath + "\\Web.Config");
                File.Move(outputpath + "\\OriginalWebConfigCopyPublishtime\\Web.Config", outputpath + "\\Web.Config");            
            
            
            }
            catch
            {

            }



            DirectoryInfo sourceDir = new DirectoryInfo(txttemppath.Text + "\\" + newfoldername);
            double size = GetSizeDirectory(sourceDir);

            double twpercsize = 0.20 * size;


            try
            {
               // string mspath = "C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\";
               // string mscompiler = "aspnet_compiler.exe";
               // string fullcompilerpath = Path.Combine(mspath, mscompiler);
               // ProcessStartInfo startinfo = new ProcessStartInfo();


               // string virtualfilename = "/" + dateformat;
               //// string outputpath = txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + "\\" + insidefoldername;

               // string argument = "-p " + temppath + " -v " + virtualfilename + " -u -f " + outputpath + " -fixednames";
               // Process.Start(fullcompilerpath, argument).WaitForExit();
               // //-p I:\capmanversion\Testdata\34201771258 -v /201743713146 -u -f I:\capmanversion\PublishData\34201771258\247201394956MC_onlineaccounts.net\247201394956MC_onlineaccounts.net -fixednames
               // try
               // {
               //     File.Delete(outputpath + "\\bin\\businesssetup.dll");
               //     File.Delete(outputpath + "\\bin\\documentrule.dll");
               //     File.Delete(outputpath + "\\bin\\presensenote.dll");
               // }
               // catch
               // {
               // }
                
                Directory.Delete(temppath, true);
            }
            catch (Exception ex)
            {
            }

          

            //if (!File.Exists(outputpath + "\\bin\\businesssetup.dll") && !File.Exists(outputpath + "\\bin\\documentrule.dll") && !File.Exists(outputpath + "\\bin\\presensenote.dll"))
            //{

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername);
                    zip.Save(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + ".zip");
                }
               
                DirectoryInfo outdir = new DirectoryInfo(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername);

                double outfoldersize = GetSizeOutDirectory(outdir);

                //if (outfoldersize < twpercsize)
                //{

                    if (System.IO.File.Exists(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + ".zip"))
                    {

                        // File.Move(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + insidefoldername + ".zip", txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + filename);

                        string strcln = " select * from ClientMaster where ClientMasterId='" + Session["ClientId"].ToString() + "'";
                        SqlCommand cmdcln = new SqlCommand(strcln, con);
                        DataTable dtcln = new DataTable();
                        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                        adpcln.Fill(dtcln);

                        if (dtcln.Rows.Count > 0)
                        {
                            string defaltdrivepath = dtcln.Rows[0]["DefaultSourceDrivePath"].ToString();
                            string companyname = dtcln.Rows[0]["CompanyName"].ToString();
                            string mastersourcefilepath = defaltdrivepath + "\\" + filename;  // code type 1

                            // Copy website folder
                            //oa
                            File.Copy(txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + filename, mastersourcefilepath, true);

                            // end oa

                            insert(Convert.ToInt32(ddlproductname.SelectedValue), Convert.ToInt32(ddlcodetype.SelectedValue), Convert.ToInt32(lblnewcodetypeNo.Text), filename, mastersourcefilepath, txtoutputsourcepath.Text + "\\" + newfoldername + "\\" + filename);

                            SqlCommand cmdsq = new SqlCommand("Insert into ProductMasterLatestcodeversioninfoTBl(Productveriontbl,CodeVersion,CodeTypeID)Values('" + ddlproductname.SelectedValue + "','" + lblnewcodetypeNo.Text + "','" + ddlcodetype.SelectedValue + "')", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdsq.ExecuteNonQuery();
                            con.Close();

                            DataTable dtrsc = selectBZ("Select Max(Id) as Id,Productveriontbl,CodeTypeID,CodeVersion  from ProductMasterLatestcodeversioninfoTBl where Productveriontbl='" + ddlproductname.SelectedValue + "' and  CodeVersion='" + lblnewcodetypeNo.Text + "' and CodeTypeID='" + ddlcodetype.SelectedValue + "' Group by Productveriontbl,CodeTypeID,CodeVersion");

                            SqlCommand cmdsxwebpub = new SqlCommand("Insert into WebsitePublish(CodeTypeId,ProductVersionId,DateTime,SourcePath,OutputPath,OutPutfileZipName,codeversionnumber)Values('" + ddlcodetype.SelectedValue + "','" + ddlproductname.SelectedValue + "','" + DateTime.Now.ToString() + "','" + physicalpath + "','" + outputpath + "','" + filename + "','" + lblnewcodetypeNo.Text + "')", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdsxwebpub.ExecuteNonQuery();
                            con.Close();



                            string strgetserverid = "select distinct ServerId from ServerAssignmentMasterTbl where VersionId='" + ddlproductname.SelectedValue + "'";
                            SqlCommand cmdgetserverid = new SqlCommand(strgetserverid, con);
                            DataTable dtgetserverid = new DataTable();
                            SqlDataAdapter adpgetserverid = new SqlDataAdapter(cmdgetserverid);
                            adpgetserverid.Fill(dtgetserverid);

                            if (dtgetserverid.Rows.Count > 0 && dtrsc.Rows.Count > 0 && Convert.ToString(dtrsc.Rows[0]["Id"]) != "")
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
                                        ddlproductname.SelectedIndex = ddlcodetype.SelectedIndex=ddlcodetypecatefory.SelectedIndex= -1;
                                        txtsourcepath.Text = txttemppath.Text = txtoutputsourcepath.Text =lblnewcodetypeNo.Text= "";
                                        lblmsg.Text = "Record Inserted Successfully";

                                        try
                                        {
                                            //SqlCommand cmdsx = new SqlCommand("Insert into ProductMasterLatestcodeversioninfoTBl(Id,Productveriontbl,CodeVersion,CodeTypeID)Values('" + Convert.ToString(dtrsc.Rows[0]["Id"]) + "','" + Convert.ToString(dtrsc.Rows[0]["Productveriontbl"]) + "','" + Convert.ToString(dtrsc.Rows[0]["CodeVersion"]) + "','" + Convert.ToString(dtrsc.Rows[0]["CodeTypeID"]) + "')", connserver);
                                            //if (connserver.State.ToString() != "Open")
                                            //{
                                            //    connserver.Open();
                                            //}
                                            //cmdsx.ExecuteNonQuery();
                                            //connserver.Close();

                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }
                                }
                            }

                       // }

                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "File does not exist";
                    }
                //}
                //else
                //{
                //    lblmsg.Visible = true;
                //    lblmsg.Text = "Error in Compilation";
                //}
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Error in Compilation";
            }

            fillgrid();

            findnewcodeversion();
        }
        protected void fillgrid()
        {
            string strcln = " select WebsitePublish.*,ProductMaster.ProductName +':'+ VersionInfoMaster.VersionInfoName as ProductName,CodeTypeTbl.Name from WebsitePublish inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=WebsitePublish.ProductVersionId inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join CodeTypeTbl on CodeTypeTbl.ID=WebsitePublish.CodeTypeId where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            grdcompiledproduct.DataSource = dtcln;
            grdcompiledproduct.DataBind();

        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            if (btnprint.Text == "Printable Version")
            {
                pnlgrid.ScrollBars = ScrollBars.None;
                pnlgrid.Height = new Unit("100%");

                btnprint.Text = "Hide Printable Version";
                btnin.Visible = true;

            }
            else
            {
                btnprint.Text = "Printable Version";
                btnin.Visible = false;
            }
        }

        protected void findnewcodeversion()
        {
            DataTable dtv = selectBZ("select Max(codeversionnumber) as codeversionnumber from ProductMasterCodeTbl where CodeTypeID='" + ddlcodetype.SelectedValue + "' and ProductVerID='" + ddlproductname.SelectedValue + "'");
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

        protected DataTable selectBZ(string str)
        {
            SqlCommand cmdclnccdweb = new SqlCommand(str, con);
            DataTable dtclnccdweb = new DataTable();
            SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
            adpclnccdweb.Fill(dtclnccdweb);
            return dtclnccdweb;
        }

        protected void fillpath()
        {
            DataTable dtcln = selectBZ("select * from VersionInfoMaster where VersionInfoId='" + ddlproductname.SelectedValue + "' ");

            if (dtcln.Rows.Count > 0)
            {
                txtsourcepath.Text = dtcln.Rows[0]["MasterCodeSourcePath"].ToString();
                txttemppath.Text = dtcln.Rows[0]["TemporaryPublishPath"].ToString();
                txtoutputsourcepath.Text = dtcln.Rows[0]["DestinationPath"].ToString();
            }


        }
        protected void ddlproductname_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillpath();
            fillcodetype();

        }
        protected void ddlcodetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillpath();
            findnewcodeversion();
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
        protected void fillcodetypecategory()
        {
            DataTable dtcln = selectBZ(" select * from CodeTypeCategory where CodeMasterNo='1' ");
            ddlcodetypecatefory.DataSource = dtcln;
            ddlcodetypecatefory.DataValueField = "CodeMasterNo";
            ddlcodetypecatefory.DataTextField = "CodeTypeCategory";
            ddlcodetypecatefory.DataBind();
        }
        protected void ddlcodetypecatefory_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillpath();
            fillcodetype();
        }

        static double GetSizeDirectory(DirectoryInfo source)
        {

            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }
            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();

            foreach (DirectoryInfo dir in dirs)
            {

                GetSizeDirectory(dir);
            }
            return size;
        }

        static double GetSizeOutDirectory(DirectoryInfo source)
        {

            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                sizeout += file.Length;
            }
            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();

            foreach (DirectoryInfo dir in dirs)
            {

                GetSizeOutDirectory(dir);
            }
            return sizeout;
        }
}