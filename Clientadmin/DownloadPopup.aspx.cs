using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net;

public partial class DownloadPopup : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Int64 VID = Convert.ToInt64(Request.QueryString["VID"]);
                string From = Convert.ToString(Request.QueryString["From"]);
                if (VID != null || VID != 0)
                {
                    Fill_grvDocuments(VID,From);    
                }
                
            }
        }
        catch (Exception)
        { }
    }

    protected void Fill_grvDocuments(Int64 PageVersionID, string From = "")
    {
        try
        {
            if (From == "Documentation")
            {
                string strSQL = "SELECT * FROM VersionDocument_Master WHERE PageVersionID = " + PageVersionID;
                con.Open();
                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtDocuments = new DataTable();
                da.Fill(dtDocuments);
                con.Close();
                grvDocuments.DataSource = dtDocuments;
                grvDocuments.DataBind();
                grvDocuments.Visible = true;
                grvInstructions.Visible = false;
                lblTitle.Text = "List of documentation";
            }
            else if (From == "Instructions")
            {
                string strSQL = "SELECT PWGU.ID, PVT.ID AS VersionID, PWGU.WorkRequirementPdfFileName, PWGU.FileTitle FROM PageWorkGuideUploadTbl AS PWGU " +
                    "INNER JOIN PageWorkTbl AS PWT ON PWT.ID = PWGU.PageWorkTblID " +
                    "INNER JOIN PageVersionTbl AS PVT ON PVT.ID = PWT.PageVersionTblID " +
                    "WHERE PVT.ID = " + PageVersionID;
                con.Open();
                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtInstructions = new DataTable();
                da.Fill(dtInstructions);
                con.Close();
                grvInstructions.DataSource = dtInstructions;
                grvInstructions.DataBind();
                grvDocuments.Visible = false;
                grvInstructions.Visible = true;
                lblTitle.Text = "List of instruction files";
            }
        }
        catch (Exception)
        {
            
        }
    }

    protected void grvInstructions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //try
        //{
        if (e.CommandName == "Download")
        {
            int j = Convert.ToInt16(e.CommandArgument);
            Int64 FileID = Convert.ToInt64(grvInstructions.DataKeys[j]["ID"]);
            Int64 PageVersionID = Convert.ToInt64(grvInstructions.DataKeys[j]["VersionID"]);
            string DocumentTitle = Convert.ToString(grvInstructions.DataKeys[j]["WorkRequirementPdfFileName"]);

            string strcount = " select PDSA.FileName, PDSA.PageWorkTblId as PageWorkMasterId, " +
                   "WebsiteMaster.*,PageMaster.FolderName from PageDevelopmentSourceCodeAllocateTable AS PDSA " +
                   "inner join  PageWorkTbl  on PageWorkTbl.id=PDSA.PageWorkTblId " +
                   "inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                   "inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId " +
                   "inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  " +
                   "inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  " +
                   "inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  " +
                   "inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageVersionTbl.Id='" + PageVersionID + "'";
            SqlCommand cmdcount = new SqlCommand(strcount, con);
            DataTable dtcount = new DataTable();
            SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
            adpcount.Fill(dtcount);


            string lblftpurl123 = "";
            string lblftpport123 = "";
            string lblftpuserid = "";
            string lblftppassword123 = "";

            if (dtcount.Rows.Count > 0)
            {
                lblftpurl123 = dtcount.Rows[0]["FTP_Url"].ToString();
                lblftpport123 = dtcount.Rows[0]["FTP_Port"].ToString();
                lblftpuserid = dtcount.Rows[0]["FTP_UserId"].ToString();
                lblftppassword123 = PageMgmt.Decrypted(dtcount.Rows[0]["FTP_Password"].ToString());
                ViewState["folder"] = dtcount.Rows[0]["FolderName"].ToString();

                string[] separator1 = new string[] { "/" };
                string[] strSplitArr1 = lblftpurl123.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

                String productno = strSplitArr1[0].ToString();
                string ftpurl = "";

                if (productno == "FTP:" || productno == "ftp:")
                {
                    if (strSplitArr1.Length >= 3)
                    {
                        ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123;
                        for (int i = 2; i < strSplitArr1.Length; i++)
                        {
                            ftpurl += "/" + strSplitArr1[i].ToString();
                        }
                    }
                    else
                    {
                        ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123;

                    }
                }
                else
                {
                    if (strSplitArr1.Length >= 2)
                    {
                        ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123;
                        for (int i = 1; i < strSplitArr1.Length; i++)
                        {
                            ftpurl += "/" + strSplitArr1[i].ToString();
                        }
                    }
                    else
                    {
                        ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123;

                    }

                }


                if (lblftpurl123.Length > 0)
                {

                    string ftphost = ftpurl + "/" + ViewState["folder"] + "/Attach/";
                    string fnname = DocumentTitle.ToString();
                    string despath = Server.MapPath("~\\Attachment\\") + fnname.ToString();
                    FileInfo filec = new FileInfo(despath);
                    try
                    {
                        if (!filec.Exists)
                        {
                            GetFile(ftphost, fnname, despath, lblftpuserid, lblftppassword123);
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }



                    FileInfo file = new FileInfo(despath);
                    if (file.Exists)
                    {
                        //Response.Clear();
                        //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        //Response.AddHeader("Content-Length", file.Length.ToString());
                        //Response.ContentType = "application/octet-stream";
                        //Response.WriteFile(file.FullName);
                        //Response.End();

                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                        //Response.Flush();

                        //string filePath = "";
                        //filePath = Server.MapPath("~/Attachment/" + fnname);
                        //Response.ContentType = ContentType;
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                        //Response.WriteFile(filePath);
                        //Response.End();

                        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                        response.ClearContent();
                        response.Clear();
                        response.ContentType = "application/octet-stream";
                        response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name + ";");
                        response.TransmitFile(Server.MapPath("~/Attachment/" + file.Name));
                        response.Flush();
                        response.End();
                    }


                }

            }
            
        }
    }

    public bool GetFile(string ftp, string filename, string Destpath, string username, string password)
    {
        FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.
           Create(ftp.ToString() + filename.ToString());
        password = PageMgmt.Decrypted(password); // add this line by ninad at 1/7/2015
        oFTP.Credentials = new NetworkCredential(username.ToString(), password.ToString());
        oFTP.UseBinary = false;
        oFTP.UsePassive = true;
        oFTP.Method = WebRequestMethods.Ftp.DownloadFile;


        FtpWebResponse response =
           (FtpWebResponse)oFTP.GetResponse();
        Stream responseStream = response.GetResponseStream();

        FileStream fs = new FileStream(Destpath, FileMode.CreateNew);
        Byte[] buffer = new Byte[2047];
        int read = 1;
        while (read != 0)
        {
            read = responseStream.Read(buffer, 0, buffer.Length);
            fs.Write(buffer, 0, read);
        }

        responseStream.Close();
        fs.Flush();
        fs.Close();
        responseStream.Close();
        response.Close();

        oFTP = null;
        return true;
    }

    protected void grvDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Download")
            {
                int j = Convert.ToInt16(e.CommandArgument);
                Int64 DocumentID = Convert.ToInt64(grvDocuments.DataKeys[j]["DocumentID"]);
                Int64 PageVersionID = Convert.ToInt64(grvDocuments.DataKeys[j]["PageVersionID"]);
                string DocumentTitle = Convert.ToString(grvDocuments.DataKeys[j]["DocumentTitle"]);

                string strcount = " select PDSA.FileName, PDSA.PageWorkTblId as PageWorkMasterId, " +
                       "WebsiteMaster.*,PageMaster.FolderName from PageDevelopmentSourceCodeAllocateTable AS PDSA " +
                       "inner join  PageWorkTbl  on PageWorkTbl.id=PDSA.PageWorkTblId " +
                       "inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                       "inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId " +
                       "inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  " +
                       "inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  " +
                       "inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  " +
                       "inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageVersionTbl.Id='" + PageVersionID + "'";
                SqlCommand cmdcount = new SqlCommand(strcount, con);
                DataTable dtcount = new DataTable();
                SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
                adpcount.Fill(dtcount);


                string lblftpurl123 = "";
                string lblftpport123 = "";
                string lblftpuserid = "";
                string lblftppassword123 = "";

                if (dtcount.Rows.Count > 0)
                {
                    lblftpurl123 = dtcount.Rows[0]["FTP_Url"].ToString();
                    lblftpport123 = dtcount.Rows[0]["FTP_Port"].ToString();
                    lblftpuserid = dtcount.Rows[0]["FTP_UserId"].ToString();
                    lblftppassword123 = PageMgmt.Decrypted(dtcount.Rows[0]["FTP_Password"].ToString());
                    ViewState["folder"] = dtcount.Rows[0]["FolderName"].ToString();

                    string[] separator1 = new string[] { "/" };
                    string[] strSplitArr1 = lblftpurl123.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

                    String productno = strSplitArr1[0].ToString();
                    string ftpurl = "";

                    if (productno == "FTP:" || productno == "ftp:")
                    {
                        if (strSplitArr1.Length >= 3)
                        {
                            ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123;
                            for (int i = 2; i < strSplitArr1.Length; i++)
                            {
                                ftpurl += "/" + strSplitArr1[i].ToString();
                            }
                        }
                        else
                        {
                            ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123;

                        }
                    }
                    else
                    {
                        if (strSplitArr1.Length >= 2)
                        {
                            ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123;
                            for (int i = 1; i < strSplitArr1.Length; i++)
                            {
                                ftpurl += "/" + strSplitArr1[i].ToString();
                            }
                        }
                        else
                        {
                            ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123;

                        }

                    }


                    if (lblftpurl123.Length > 0)
                    {

                        string ftphost = ftpurl + "/" + ViewState["folder"] + "/Attachment/";
                        string fnname = DocumentTitle.ToString();
                        string despath = Server.MapPath("~\\Attachment\\") + fnname.ToString();
                        FileInfo filec = new FileInfo(despath);
                        try
                        {
                            if (!filec.Exists)
                            {
                                GetFile(ftphost, fnname, despath, lblftpuserid, lblftppassword123);
                            }
                        }
                        catch (Exception ex)
                        {
                            
                        }



                        FileInfo file = new FileInfo(despath);
                        if (file.Exists)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            Response.AddHeader("Content-Length", file.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.WriteFile(file.FullName);
                            //Response.End();
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                            Response.Flush();
                        }


                    }

                }
               
            }

        }
        catch (Exception ex)
        {
            
        }

    }

    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    try
    //    { 
            
    //    }
    //    catch (Exception)
    //    { }
    //}
}