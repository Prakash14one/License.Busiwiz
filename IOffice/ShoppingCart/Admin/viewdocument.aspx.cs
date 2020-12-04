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
using System.IO;
using System.Text;
using System.Drawing.Printing;
using System.Drawing.Design;
using System.Drawing;
using System.ServiceProcess;
using System.Diagnostics;
using System.Windows;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net;

public partial class Account_ViewDocument : System.Web.UI.Page
{
    DocumentCls1 clsDocument = new DocumentCls1();
    MasterCls clsMaster = new MasterCls();
    SqlConnection con;
    protected static int fst = 0;
    EmployeeCls clsEmployee = new EmployeeCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();
        char[] separator = new char[] { '/' };
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);

        if (Session["CompanyName"] != null)
        {
            this.Title = Session["CompanyName"].ToString();
        }

        Session["PageName"] = "ViewDocument.aspx";

        if (!IsPostBack)
        {


            if (Request.QueryString["Siddd"] != null)
            {
                if (Request.QueryString["Siddd"].ToString() == "VHDS")
                {
                    ImageButton2.Visible = false;
                }
            }
            else
            {
                ImageButton2.Visible = false;
            }
            int Docid = Convert.ToInt32(PageMgmt.Decrypted(Request.QueryString["id"].ToString().Replace(" ", "+")));
            fst = Convert.ToInt32(Request.QueryString["zmr"]);

            Int32 DesignationId = Convert.ToInt32(Session["DesignationId"]);
            ViewState["docid"] = Docid.ToString();

            if (Convert.ToString(ViewState["docid"]) != "")
            {
                DataTable dt1 = new DataTable();
                string doc = "Select DocumentMainType.Whid,DocumentMaster.DocumentTitle,DocumentMainType.DocumentMainTypeId,DocumentSubType.DocumentSubTypeId,DocumentType.DocumentTypeId,DocumentMainType.DocumentMainType,DocumentSubType.DocumentSubType,DocumentType.DocumentType FROM DocumentMainType INNER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId INNER JOIN DocumentType ON DocumentSubType.DocumentSubTypeId = DocumentType.DocumentSubTypeId inner join DocumentMaster on DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId where DocumentMainType.CID='" + Session["Comid"] + "' and DocumentMaster.DocumentId='" + ViewState["docid"] + "'";
                SqlDataAdapter adp = new SqlDataAdapter(doc, con);
                adp.Fill(dt1);

                if (dt1.Rows.Count > 0)
                {
                    ViewState["Whid"] = Convert.ToString(dt1.Rows[0]["Whid"]);

                    ViewState["DocumentMainTypeId"] = Convert.ToString(dt1.Rows[0]["DocumentMainTypeId"]);
                    ViewState["DocumentSubTypeId"] = Convert.ToString(dt1.Rows[0]["DocumentSubTypeId"]);
                    ViewState["DocumentTypeId"] = Convert.ToString(dt1.Rows[0]["DocumentTypeId"]);

                    lblTitle.Text = Convert.ToString(dt1.Rows[0]["DocumentTitle"]);
                    LinkButton2.Text = Convert.ToString(dt1.Rows[0]["DocumentMainType"]);
                    LinkButton3.Text = Convert.ToString(dt1.Rows[0]["DocumentSubType"]);
                    LinkButton5.Text = Convert.ToString(dt1.Rows[0]["DocumentType"]);
                }

            }

            LoadPdf(Docid);
            SetAccessRights(Docid, DesignationId);
            fillDatalist(Docid);
            loadTree();
            pnltree.Visible = false;
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDoucmentMasterByID(Docid);
            string docname = dt.Rows[0]["DocumentName"].ToString();
            string name = docname.Trim();
            string extension = name.Substring(name.Length - 3);

            //if (Convert.ToString(extension) == "pdf")
            //{
            //    lblTitle.Text = docname;
            //}
            //else
            //{
            //    lblTitle.Visible = true;
            //    lblTitle.Text = "The File " + docname + " cannot be Displayed Here but canbe Downloaded to Your Desktop Using 'Save'";
            //}

            if (Session["ABCDE"] != null)
            {
                if (Session["ABCDE"].ToString() == "ABCDE")
                {
                    int rst = clsDocument.InsertDocumentLog(Convert.ToInt32(ViewState["docid"]), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), true, false, false, false, false, false, false, false);
                    //fst = fst + 1;
                    Session["ABCDE"] = "KFKDF";
                }
            }

            string scpt = "select Entry_Type_Name,EntryNumber,TranctionMaster.Tranction_Master_Id from AttachmentMaster  inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=AttachmentMaster.RelatedTableId inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId where IfilecabinetDocId='" + Docid + "'";
            SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
            DataTable ds58 = new DataTable();
            adp58.Fill(ds58);
            if (ds58.Rows.Count == 0)
            {
                LinkButton4.Text = "Accounting Entry";
                LinkButton6.Visible = false;
            }
            else
            {
                LinkButton6.Text = ds58.Rows[0]["Entry_Type_Name"].ToString() + ":" + ds58.Rows[0]["EntryNumber"].ToString();
                LinkButton6.CommandArgument = ds58.Rows[0]["Tranction_Master_Id"].ToString();
                LinkButton4.Text = " More";
                LinkButton6.Visible = true;
            }


        }


    }
    protected void imgbtnSave_Click(object sender, EventArgs e)
    {
        int rst = clsDocument.InsertDocumentLog(Convert.ToInt32(ViewState["docid"]), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);
        string filepath = ViewState["path"].ToString();
        FileInfo file = new FileInfo(filepath);

        if (file.Exists)
        {
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = ReturnExtension(file.Extension.ToLower());
            Response.TransmitFile(file.FullName);
            Response.End();

        }

    }
    protected void imgbtnPrint_Click(object sender, EventArgs e)
    {
        string filepath = ViewState["path"].ToString();
        //try
        //{
        //    string pathToExecutable = "C:\\Program Files\\Adobe\\Reader 8.0\\Reader\\AcroRd32.exe";
        //    RunExecutable(pathToExecutable, filepath);
        //}
        //catch
        //{
        int rst = clsDocument.InsertDocumentLog(Convert.ToInt32(ViewState["docid"]), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, false, false, false, false, false, true, false);
        Response.Redirect("ViewDocForPrint.aspx?id=" + PageMgmt.Encrypted(ViewState["docid"].ToString()) + "");
        //}
        //finally
        //{


        //}


    }

    protected void imgbtnSendMessage_Click(object sender, EventArgs e)
    {
        string DocumentId = ViewState["docid"].ToString();
        // int DocumentId = 39;
        Response.Redirect("MessageCompose.aspx?id=" + DocumentId + "");

    }
    protected void ImgBtnEmail_Click(object sender, EventArgs e)
    {
        string DocumentId = ViewState["docid"].ToString();
        Response.Redirect("MessageComposeExt.aspx?id=" + DocumentId + "");
    }
    protected void ImgBtnFax_Click(object sender, EventArgs e)
    {

    }


    public static string DefaultPrinterName()
    {
        string functionReturnValue = null;
        System.Drawing.Printing.PrinterSettings oPS = new System.Drawing.Printing.PrinterSettings();

        try
        {
            functionReturnValue = oPS.PrinterName;
        }
        catch (System.Exception ex)
        {
            functionReturnValue = "";
        }
        finally
        {
            oPS = null;
        }
        return functionReturnValue;
    }

    protected void LoadPdf(int Docid)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDoucmentMasterByID(Docid);
            if (dt.Rows.Count > 0)
            {
                string docname = dt.Rows[0]["DocumentName"].ToString();
                ViewState["decname"] = docname.ToString();
                lblddcname.Text = dt.Rows[0]["DocumentTitle"].ToString();

                // string strft = "select Id from (select ROW_NUMBER() OVER (ORDER BY ID) as ROW_NUM,Id from  DocumentImageMaster where DocumentMasterId='" + Docid + "') q where ROW_NUM='" + lblnooff.Text + "'";
                string strft = "select * from DocumentImageMaster where DocumentMasterId='" + Docid + "'   ";
                SqlCommand cmdft = new SqlCommand(strft, con);
                SqlDataAdapter adpft = new SqlDataAdapter(cmdft);
                DataTable dtft = new DataTable();
                adpft.Fill(dtft);

                if (dtft.Rows.Count > 0)
                {
                    lblnototal.Text = dtft.Rows.Count.ToString();
                    Image2.ImageUrl = "~/Account/" + Session["comid"] + "/DocumentImage/" + dtft.Rows[0]["DocumentImgName"].ToString();
                }


                string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);

                //string strft = "Select FileStorage.* from FileStorage Where B='" + encryptstrring(Session["comid"].ToString()) + "' and H='" + encryptstrring("True") + "'";
                //SqlCommand cmdft = new SqlCommand(strft, con);
                //SqlDataAdapter adpft = new SqlDataAdapter(cmdft);
                //DataTable dtft = new DataTable();
                //adpft.Fill(dtft);

                //if (dtft.Rows.Count > 0)
                //{
                //    FileInfo filec = new FileInfo(filepath);
                //    if (!filec.Exists)
                //    {
                //        datatransftp(docname, filepath);
                //        System.Threading.Thread.Sleep(1000);
                //        FileInfo filecup = new FileInfo(filepath);
                //        if (filecup.Exists)
                //        {

                //            string filepathu = Server.MapPath("~//Account//pdftoimage.exe");
                //            System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepathu);

                //            pti.UseShellExecute = false;
                //            pti.Arguments = filepathu + " -i UploadedDocuments//" + docname + " " + "-o" + " " + "DocumentImage//";//+ " " + "-r" + "VNKSURDLWQOVHPGH";


                //            pti.RedirectStandardOutput = true;
                //            pti.RedirectStandardInput = true;
                //            pti.RedirectStandardError = true;

                //            pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");
                //            System.Diagnostics.Process ps = Process.Start(pti);
                //            System.Threading.Thread.Sleep(1000);
                //        }
                //    }
                //}
                int length = docname.Length;
                string docnameIn = docname.Substring(0, length - 4);
                ViewState["path"] = filepath.ToString();
                //DataTable dt1 = new DataTable();
                //DataColumn dcom = new DataColumn();
                //dcom.ColumnName = "image";
                //dcom.DataType = System.Type.GetType("System.String");
                //dt1.Columns.Add(dcom);
                //DataTable dt2 = new DataTable();
                //dt2 = clsDocument.SelectDoucmentImageMaster(Docid);



                //if (dt2.Rows.Count > 0)
                //{
                //    int sav = 0;
                //    //for (int kk = 1; kk <= dt2.Rows.Count; kk++)
                //    //{
                //        DataRow drow = dt1.NewRow();
                //        if (Convert.ToString(Session["no"]) == Convert.ToString(Session["no1"]))
                //        {
                //            int no = Convert.ToInt32(Session["no"]);
                //            sav = no;
                //            drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + dt2.Rows[no - 1]["DocumentImgName"].ToString();
                //            Session["no1"] = Convert.ToInt32(Session["no1"]) + 1;
                //            dt1.Rows.Add(drow);
                //            DataRow drow1 = dt1.NewRow();
                //            //if (no != 1)
                //            //{
                //            //    drow1["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + dt2.Rows[kk - 1]["DocumentImgName"].ToString();
                //            //    dt1.Rows.Add(drow1);
                //            //}
                //        }
                //        else
                //        {
                //            if (sav == kk)
                //            {
                //            }
                //            else
                //            {
                //                drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + dt2.Rows[kk - 1]["DocumentImgName"].ToString();
                //                dt1.Rows.Add(drow);
                //            }
                //        }


                //        lblnooff.Text = Session["no"].ToString();
                //        lblnototal.Text = dt2.Rows.Count.ToString();
                //  //  }
                //}
                //else
                //{
                //    Session["no"] = "1";
                //    Session["no1"] = "1";
                //    lblnototal.Text = "1";
                //    lblnooff.Text = "1";
                //    docnameIn += "00001.jpg";
                //    DataRow drow = dt1.NewRow();
                //    drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + docnameIn;
                //    dt1.Rows.Add(drow);

                //}

                //DataList1.DataSource = dt1;
                //DataList1.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void LoadPdfwithno(int Docid, string no)
    {
        try
        {
            string strft = "select * from (select ROW_NUMBER() OVER (ORDER BY ID) as ROW_NUM,* from  DocumentImageMaster where DocumentMasterId='" + Docid + "') q where ROW_NUM='" + no + "'";
            SqlCommand cmdft = new SqlCommand(strft, con);
            SqlDataAdapter adpft = new SqlDataAdapter(cmdft);
            DataTable dtft = new DataTable();
            adpft.Fill(dtft);

            if (dtft.Rows.Count > 0)
            {
                Image2.ImageUrl = "~/Account/" + Session["comid"] + "/DocumentImage/" + dtft.Rows[0]["DocumentImgName"].ToString();

            }
        }
        catch (Exception)
        {
        }
    }
    protected void datatransftp(string doc, string filepath)
    {


        string str1 = "Select FileStorage.* from FileStorage Where B='" + encryptstrring(Session["comid"].ToString()) + "' and H='" + encryptstrring("True") + "'";
        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {

            string ftpur = decryptstring(dt.Rows[0]["C"].ToString());
            string ftpport = decryptstring(dt.Rows[0]["D"].ToString());

            string ftpuser = decryptstring(dt.Rows[0]["E"].ToString());
            string ftppass = decryptstring(dt.Rows[0]["F"].ToString());
            string ftpcond = decryptstring(dt.Rows[0]["G"].ToString());
            string ftpallowed = decryptstring(dt.Rows[0]["H"].ToString());
            string[] separator1 = new string[] { "/" };
            string[] strSplitArr1 = ftpur.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            String productno = strSplitArr1[0].ToString();
            string ftpurl = "";

            if (productno == "FTP:" || productno == "ftp:")
            {
                if (strSplitArr1.Length >= 3)
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + ftpport;
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + ftpport;

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + ftpport;
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + ftpport;

                }

            }
            if (ftpur.Length > 0)
            {

                string ftphost = ftpurl + "/";
                string fnname = doc;

                GetFile(ftphost, fnname, filepath, ftpuser, ftppass);

            }
        }
    }
    public bool GetFile(string ftp, string filename, string Destpath, string username, string password)
    {
        try
        {
            FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.
               Create(ftp.ToString() + filename.ToString());

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

        }
        catch (Exception ecx)
        {
        }
        return true;
    }

    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }
    protected void SetAccessRights(int Docid, int DesignationId)
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDoucmentMasterByID(Docid);
        int DoctypeId = Convert.ToInt32(dt.Rows[0]["DocumentTypeId"]);
        DataTable dt1 = new DataTable();
        dt1 = clsDocument.SelectDocumentAccessRightwithDesignation(DesignationId, DoctypeId);// .SelectDocumentAccessRightByTypeDesi(DoctypeId, DesignationId);
        if (dt1.Rows.Count > 0)
        {

            imgbtnSave.Visible = Convert.ToBoolean(dt1.Rows[0]["SaveAccess"]);
            imgbtnPrint.Visible = Convert.ToBoolean(dt1.Rows[0]["SaveAccess"]);
            imgbtnSendMessage.Visible = Convert.ToBoolean(dt1.Rows[0]["MessageAccess"]);
            ImgBtnEmail.Visible = Convert.ToBoolean(dt1.Rows[0]["EmailAccess"]);
            // ImgBtnFax.Visible = Convert.ToBoolean(dt1.Rows[0]["FaxAccess"]);
        }
    }
    protected void RunExecutable(string executable, string arguments)
    {
        //'Declarations 
        ProcessStartInfo starter = default(ProcessStartInfo);
        Process Prc = default(Process);

        //'Pass File Path And Arguments 
        starter = new ProcessStartInfo(executable, arguments);
        starter.CreateNoWindow = true;
        starter.RedirectStandardOutput = true;
        starter.UseShellExecute = false;

        //'Start Adobe Process 
        Prc = new Process();
        Prc.StartInfo = starter;
        Prc.Start();
    }
    protected void Gridreqinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            GridView Gridreqinfo = (GridView)DataListFolder.Items[DataListFolder.SelectedIndex].FindControl("Gridreqinfo");
            Gridreqinfo.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int docid = Convert.ToInt32(Gridreqinfo.SelectedDataKey.Value);
            LoadPdf(docid);
        }
    }

    protected void fillDatalist(int DocumentId)
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentFolderByDocumentId(DocumentId);

        DataListFolder.DataSource = dt;
        DataListFolder.DataBind();

        foreach (DataListItem ditem in DataListFolder.Items)
        {
            //DataList1.SelectedIndex = ditem.ItemIndex;
            int folderid = Convert.ToInt32(DataListFolder.DataKeys[ditem.ItemIndex]);
            DataTable dt1 = new DataTable();
            dt1 = clsDocument.SelectDoucmentTotalInFolder(folderid);

            LinkButton lnk = (LinkButton)ditem.FindControl("LinkButton1");
            lnk.Text = lnk.Text + "(" + dt1.Rows[0]["total"].ToString() + ")";

        }
    }

    protected void DataListFolder_ItemCommand(object source, DataListCommandEventArgs e)
    {
        DataListFolder.SelectedIndex = e.Item.ItemIndex;
        int FolderId = Convert.ToInt32(DataListFolder.DataKeys[DataListFolder.SelectedIndex]);
        ViewState["FolderId"] = FolderId.ToString();
        foreach (DataListItem ditem in DataListFolder.Items)
        {
            GridView Gridreqinfo = (GridView)ditem.FindControl("Gridreqinfo");

            Gridreqinfo.DataSource = null;
            Gridreqinfo.DataBind();


        }
        FillGrid(FolderId);
    }
    protected void FillGrid(int FolderID)
    {

        DataTable dt = new DataTable();

        dt = clsDocument.SelectDocumentAccessRigthsByDesignationID();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        int flag = 1;
        foreach (DataRow dr in dt.Rows)
        {

            dt1 = clsDocument.SelectDocumentMasterByDocumentTypeIDFolder(Convert.ToInt32(dr["DocumentTypeId"]), FolderID);
            if (flag == 1)
            {
                dt2 = dt1.Clone();
                flag = 0;
            }
            foreach (DataRow r in dt1.Rows)
            {
                dt2.ImportRow(r);
            }
        }
        DataView dv = dt2.DefaultView;
        if (dv.Table.Rows.Count > 0)
        {
            dv.Sort = "DocumentId desc";
        }

        GridView Gridreqinfo = (GridView)DataListFolder.Items[DataListFolder.SelectedIndex].FindControl("Gridreqinfo");

        Gridreqinfo.DataSource = dv;
        Gridreqinfo.DataBind();


    }

    protected void lnkbtnShowTree_Click(object sender, EventArgs e)
    {
        lnkbtnShowTree.Visible = false;
        lnkbtnHideTree.Visible = true;
        pnltree.Visible = true;

        loadTree();

        pnltree.Width = 200;
        pnlDoc.Width = 800;

    }
    protected void lnkbtnHideTree_Click(object sender, EventArgs e)
    {
        lnkbtnShowTree.Visible = true;
        lnkbtnHideTree.Visible = false;
        pnlDoc.CssClass = "divpnlright";

        pnltree.Visible = false;
        pnlDoc.Visible = true;
        pnlDoc.Width = 1000;
    }
    protected void lnkbtnShowRelated_Click(object sender, EventArgs e)
    {
        lnkbtnShowRelated.Visible = false;
        lnkbtnHideRelated.Visible = true;
        pnlRelated.Visible = true;
        if (pnltree.Visible == true)
        {
            pnlDoc.CssClass = "divpnl";
        }
        else
        {
            pnlDoc.CssClass = "divpnlright";

        }
    }
    protected void lnkbtnHideRelated_Click(object sender, EventArgs e)
    {
        lnkbtnShowRelated.Visible = true;
        lnkbtnHideRelated.Visible = false;
        pnlRelated.Visible = false;
        if (pnltree.Visible == true)
        {
            pnlDoc.CssClass = "divpnlleft";
        }
        else
        {
            pnlDoc.CssClass = "divpnlBoth";
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Siddd"] != null)
        {
            if (Request.QueryString["Siddd"].ToString() == "VHDS")
            {
                Response.Redirect("DocumentSearch.aspx");
            }
        }
    }
    public string decryptstring(string str)
    {
        return Decrypt(str, "&%#@?,:*");
    }
    private string Decrypt(string strText, string strEncrypt)
    {
        byte[] bKey = new byte[20];
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string Encrypt(string strtxt, string strtoencrypt)
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
            throw ex;
        }

    }
    public string encryptstrring(string strText)
    {
        return Encrypt(strText, "&%#@?,:*");
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        int tid = Convert.ToInt32(LinkButton6.CommandArgument);

        string st = "Select EntryTypeMaster.Entry_Type_Id, EntryTypeMaster.Entry_Type_Name,TranctionMaster.EntryNumber,AttachmentMaster.Datetime From AttachmentMaster inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=AttachmentMaster.RelatedTableId inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId where TranctionMaster.Tranction_Master_Id='" + tid + "' ";

        SqlCommand cm = new SqlCommand(st, con);
        SqlDataAdapter ad = new SqlDataAdapter(cm);
        DataTable d = new DataTable();
        ad.Fill(d);
        if (d.Rows.Count > 0)
        {
            string te = "";
            if (Convert.ToString(d.Rows[0]["Entry_Type_Id"]) == "1")
            {
                te = "cashpaymentnew.aspx?trid=" + tid + "&&return=view";

            }
            else if (Convert.ToString(d.Rows[0]["Entry_Type_Id"]) == "2")
            {
                te = "CashReciept.aspx?trid=" + tid + "&&return=view";
            }
            else if (Convert.ToString(d.Rows[0]["Entry_Type_Id"]) == "30")
            {
                te = "RetailCustomerDeliveryChallan_new.aspx?trid=" + tid + "&&return=view";
            }
            else if (Convert.ToString(d.Rows[0]["Entry_Type_Id"]) == "27")
            {
                te = "PurchaseInvoice.aspx?trid=" + tid + "&&return=view";
            }
            else if (Convert.ToString(d.Rows[0]["Entry_Type_Id"]) == "7" || Convert.ToString(d.Rows[0]["Entry_Type_Id"]) == "6")
            {
                te = "CrDrNoteAddByCompany.aspx?trid=" + tid + "&&return=view";
            }
            else if (Convert.ToString(d.Rows[0]["Entry_Type_Id"]) == "3")
            {
                te = "JournalEntryCrDrCompany.aspx?trid=" + tid + "&&return=view";
            }
            else if (Convert.ToString(d.Rows[0]["Entry_Type_Id"]) == "26")
            {

                te = "CustomerDeliveryChallan3.aspx?trid=" + tid + "&&return=view"; ;
            }
            else if (Convert.ToString(d.Rows[0]["Entry_Type_Id"]) == "32" || Convert.ToString(d.Rows[0]["Entry_Type_Id"]) == "33")
            {

                te = "CashRegister.aspx?trid=" + tid + "&&return=view"; ;
            }
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string str = "select DocumentMaster.DocumentId,DocumentMaster.DocumentTitle from DocumentMaster where DocumentMaster.DocumentId = '" + ViewState["docid"] + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        lbldid.Text = dt.Rows[0]["DocumentId"].ToString();
        lbldtitle.Text = dt.Rows[0]["DocumentTitle"].ToString();

        string str2 = "select  Entry_Type_Id,Entry_Type_Name as Name,SortName from EntryTypeMaster Where SortName IS NOT NULL ";
        SqlCommand cmd1 = new SqlCommand(str2, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);

        ddloa.DataSource = ds1;
        ddloa.DataTextField = "SortName";
        ddloa.DataValueField = "Entry_Type_Id";
        ddloa.DataBind();
        string st = "";

        st = "Select EntryTypeMaster.Entry_Type_Name,TranctionMaster.EntryNumber,AttachmentMaster.Datetime From AttachmentMaster inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=AttachmentMaster.RelatedTableId inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId where IfilecabinetDocId='" + dt.Rows[0]["DocumentId"].ToString() + "' ";

        SqlCommand cm = new SqlCommand(st, con);
        SqlDataAdapter ad = new SqlDataAdapter(cm);
        DataTable d = new DataTable();
        ad.Fill(d);
        gridpopup.DataSource = d;
        gridpopup.DataBind();
        Panel5.Visible = true;

        // }


        mdloa.Show();
    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {


        string te = "";
        if (ddloa.SelectedIndex == 0)
        {

            te = "cashpaymentnew.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];


        }
        else if (ddloa.SelectedIndex == 1)
        {
            te = "CashReciept.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 2)
        {
            te = "JournalEntryCrDrCompany.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 3)
        {
            te = "CrDrNoteAddByCompany.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 4)
        {
            te = "CrDrNoteAddByCompany.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 5)
        {
            te = "CustomerDeliveryChallan3.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 6)
        {
            te = "PurchaseInvoice.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 7)
        {
            te = "RetailCustomerDeliveryChallan_new.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 8)
        {
            te = "ExpenseInvoice.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void Img2_Click(object sender, EventArgs e)
    {


        string te = "";
        if (ddldo.SelectedIndex == 0)
        {

            te = "CashRegister.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];


        }
        else if (ddldo.SelectedIndex == 1)
        {
            te = "Registerformpayment.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedIndex == 2)
        {
            te = "LedgerJour_New.aspx?docid=" + ViewState["ID"];

            hypost.NavigateUrl = "LedgerJour_New.aspx?docid=" + ViewState["ID"];
        }
        else if (ddldo.SelectedIndex == 3)
        {
            te = "LedgerCRDR.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];


        }
        else if (ddldo.SelectedIndex == 4)
        {
            te = "Register_DeliveryChallan.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedIndex == 5)
        {
            te = "Register_purchase.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedIndex == 6)
        {
            te = "Register_Sales.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedIndex == 7)
        {
            te = "Register_SalesOrder.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedIndex == 8)
        {
            te = "Expense_SalesOrder.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }

    protected void gridpopup_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void rdradio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdradio.SelectedIndex == 0)
        {
            pvlnewentry.Visible = true;
            pnlexist.Visible = false;
        }
        else
        {
            pvlnewentry.Visible = false;
            pnlexist.Visible = true;
        }
        mdloa.Show();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        lnkbtnShowTree.Visible = false;
        lnkbtnHideTree.Visible = true;
        pnltree.Visible = true;

        pnltree.Width = 200;
        pnlDoc.Width = 800;
        loadTreecabinetwise();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        lnkbtnShowTree.Visible = false;
        lnkbtnHideTree.Visible = true;
        pnltree.Visible = true;
        pnltree.Width = 200;
        pnlDoc.Width = 800;
        loadTreedrawerwise();
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        lnkbtnShowTree.Visible = false;
        lnkbtnHideTree.Visible = true;
        pnltree.Visible = true;
        pnltree.Width = 200;
        pnlDoc.Width = 800;
        loadTreefolderrwise();
    }





    public void loadTree()
    {
        tree1.Nodes.Clear();
        DataSet ds = new DataSet();

        bool flg = false;
        bool flg1 = false;
        bool flg2 = false;
        ds = getAllType();

        foreach (DataRow pnode in ds.Tables[0].Rows)
        {
            // =========== document main type===============
            TreeNode parentnode = new TreeNode(pnode["DocumentMainType"].ToString());

            int i = Convert.ToInt32(pnode.GetChildRows("Children").Length);
            if (i == 0)
            {
                goto A;
            }

            else
            {

                tree1.Nodes.Add(parentnode);


                parentnode.ImageUrl = "~/Account/images/cabine.png";
                parentnode.CollapseAll();
                parentnode.Value = "expand" + parentnode.Text;
                if (flg == false)
                {
                    flg = true;
                    parentnode.CollapseAll();
                }
            }

            foreach (DataRow cnode in pnode.GetChildRows("Children"))
            {
                //==============   document sub type ===================================
                TreeNode childnode = new TreeNode(cnode["DocumentSubType"].ToString());


                int j = Convert.ToInt32(cnode.GetChildRows("Children2").Length);
                if (j == 0)
                {
                    goto B;
                }
                else
                {
                    parentnode.ChildNodes.Add(childnode);
                    childnode.ImageUrl = "~/Account/images/CloseDrawer1.png";
                    childnode.CollapseAll();
                    childnode.Value = "expand" + childnode.Text;
                    if (flg1 == false)
                    {
                        flg1 = true;
                        childnode.CollapseAll();
                    }


                }

                foreach (DataRow lnode in cnode.GetChildRows("Children2"))
                {
                    //============= document type===================
                    TreeNode typenode = new TreeNode(lnode["DocumentType"].ToString());


                    int n = Convert.ToInt32(lnode.GetChildRows("Children3").Length);
                    if (n == 0)
                    {
                        goto C;
                    }
                    else
                    {
                        childnode.ChildNodes.Add(typenode);
                        typenode.ImageUrl = "~/Account/images/closefolder.png";
                        typenode.CollapseAll();
                        typenode.Value = "expand" + typenode.Text;
                        if (flg2 == false)
                        {
                            flg2 = true;
                            typenode.CollapseAll();
                        }

                    }

                    foreach (DataRow docnode in lnode.GetChildRows("Children3"))
                    {
                        // ==========documents==========================

                        TreeNode dnode = new TreeNode(docnode["DocumentTitle"].ToString());
                        typenode.ChildNodes.Add(dnode);
                        dnode.ImageUrl = "~/Account/images/acrobat1.jpg";
                        dnode.Value = docnode["DocumentId"].ToString();
                        
                    }
                C: ;
                    int m = Convert.ToInt32(typenode.ChildNodes.Count);
                    if (m == 0)
                    {
                        childnode.ChildNodes.Remove(typenode);
                    }
                }
            B: ;
                int d = Convert.ToInt32(childnode.ChildNodes.Count);
                if (d == 0)
                {
                    parentnode.ChildNodes.Remove(childnode);
                }
            }
        A: ;
            int k = Convert.ToInt32(parentnode.ChildNodes.Count);
            if (k == 0)
            {
                tree1.Nodes.Remove(parentnode);
            }
        }
    }
    public void loadTreecabinetwise()
    {
        tree1.Nodes.Clear();
        DataSet ds = new DataSet();

        bool flg = false;
        bool flg1 = false;
        bool flg2 = false;
        ds = getAllTypecabinetwise();

        foreach (DataRow pnode in ds.Tables[0].Rows)
        {
            // =========== document main type===============
            TreeNode parentnode = new TreeNode(pnode["DocumentMainType"].ToString());

            int i = Convert.ToInt32(pnode.GetChildRows("Children").Length);
            if (i == 0)
            {
                goto A;
            }

            else
            {

                tree1.Nodes.Add(parentnode);


                parentnode.ImageUrl = "~/Account/images/cabine.png";
                parentnode.CollapseAll();
                parentnode.Value = "expand" + parentnode.Text;

                if (pnode["DocumentMainType"].ToString() == LinkButton2.Text)
                {
                    parentnode.ExpandAll();
                }
            }

            foreach (DataRow cnode in pnode.GetChildRows("Children"))
            {
                //==============   document sub type ===================================
                TreeNode childnode = new TreeNode(cnode["DocumentSubType"].ToString());


                int j = Convert.ToInt32(cnode.GetChildRows("Children2").Length);
                if (j == 0)
                {
                    goto B;
                }
                else
                {
                    parentnode.ChildNodes.Add(childnode);
                    childnode.ImageUrl = "~/Account/images/CloseDrawer1.png";
                    childnode.CollapseAll();
                    childnode.Value = "expand" + childnode.Text;
                   


                }

                foreach (DataRow lnode in cnode.GetChildRows("Children2"))
                {
                    //============= document type===================
                    TreeNode typenode = new TreeNode(lnode["DocumentType"].ToString());


                    int n = Convert.ToInt32(lnode.GetChildRows("Children3").Length);
                    if (n == 0)
                    {
                        goto C;
                    }
                    else
                    {
                        childnode.ChildNodes.Add(typenode);
                        typenode.ImageUrl = "~/Account/images/closefolder.png";
                        typenode.CollapseAll();
                        typenode.Value = "expand" + typenode.Text;
                       

                    }

                    foreach (DataRow docnode in lnode.GetChildRows("Children3"))
                    {
                        // ==========documents==========================

                        TreeNode dnode = new TreeNode(docnode["DocumentTitle"].ToString());
                        typenode.ChildNodes.Add(dnode);
                        dnode.ImageUrl = "~/Account/images/acrobat1.jpg";
                        dnode.Value = docnode["DocumentId"].ToString();
                    }
                C: ;
                    int m = Convert.ToInt32(typenode.ChildNodes.Count);
                    if (m == 0)
                    {
                        childnode.ChildNodes.Remove(typenode);
                    }
                }
            B: ;
                int d = Convert.ToInt32(childnode.ChildNodes.Count);
                if (d == 0)
                {
                    parentnode.ChildNodes.Remove(childnode);
                }
            }
        A: ;
            int k = Convert.ToInt32(parentnode.ChildNodes.Count);
            if (k == 0)
            {
                tree1.Nodes.Remove(parentnode);
            }
        }
    }
    public void loadTreedrawerwise()
    {
        tree1.Nodes.Clear();
        DataSet ds = new DataSet();

        bool flg = false;
        bool flg1 = false;
        bool flg2 = false;
        ds = getAllTypedrawerwise();

        foreach (DataRow pnode in ds.Tables[0].Rows)
        {
            // =========== document main type===============
            TreeNode parentnode = new TreeNode(pnode["DocumentMainType"].ToString());

            int i = Convert.ToInt32(pnode.GetChildRows("Children").Length);
            if (i == 0)
            {
                goto A;
            }

            else
            {

                tree1.Nodes.Add(parentnode);
                parentnode.ImageUrl = "~/Account/images/cabine.png";
                parentnode.CollapseAll();
                parentnode.Value = "expand" + parentnode.Text;
                if (pnode["DocumentMainType"].ToString() == LinkButton2.Text)
                {
                    parentnode.ExpandAll();
                }
            }

            foreach (DataRow cnode in pnode.GetChildRows("Children"))
            {
                //==============   document sub type ===================================
                TreeNode childnode = new TreeNode(cnode["DocumentSubType"].ToString());
                int j = Convert.ToInt32(cnode.GetChildRows("Children2").Length);
                if (j == 0)
                {
                    goto B;
                }
                else
                {
                    parentnode.ChildNodes.Add(childnode);
                    childnode.ImageUrl = "~/Account/images/CloseDrawer1.png";
                    childnode.CollapseAll();
                    childnode.Value = "expand" + childnode.Text;

                    if (cnode["DocumentSubType"].ToString() == LinkButton3.Text)
                    {
                        childnode.ExpandAll();                       
                    }


                }

                foreach (DataRow lnode in cnode.GetChildRows("Children2"))
                {
                    //============= document type===================
                    TreeNode typenode = new TreeNode(lnode["DocumentType"].ToString());


                    int n = Convert.ToInt32(lnode.GetChildRows("Children3").Length);
                    if (n == 0)
                    {
                        goto C;
                    }
                    else
                    {
                        childnode.ChildNodes.Add(typenode);
                        typenode.ImageUrl = "~/Account/images/closefolder.png";
                        typenode.CollapseAll();
                        typenode.Value = "expand" + typenode.Text;
                       

                    }

                    foreach (DataRow docnode in lnode.GetChildRows("Children3"))
                    {
                        // ==========documents==========================

                        TreeNode dnode = new TreeNode(docnode["DocumentTitle"].ToString());
                        typenode.ChildNodes.Add(dnode);
                        dnode.ImageUrl = "~/Account/images/acrobat1.jpg";
                        dnode.Value = docnode["DocumentId"].ToString();
                    }
                C: ;
                    int m = Convert.ToInt32(typenode.ChildNodes.Count);
                    if (m == 0)
                    {
                        childnode.ChildNodes.Remove(typenode);
                    }
                }
            B: ;
                int d = Convert.ToInt32(childnode.ChildNodes.Count);
                if (d == 0)
                {
                    parentnode.ChildNodes.Remove(childnode);
                }
            }
        A: ;
            int k = Convert.ToInt32(parentnode.ChildNodes.Count);
            if (k == 0)
            {
                tree1.Nodes.Remove(parentnode);
            }
        }
    }
    public void loadTreefolderrwise()
    {
        tree1.Nodes.Clear();
        DataSet ds = new DataSet();

        bool flg = false;
        bool flg1 = false;
        bool flg2 = false;
        ds = getAllTypefolderwise();

        foreach (DataRow pnode in ds.Tables[0].Rows)
        {
            // =========== document main type===============
            TreeNode parentnode = new TreeNode(pnode["DocumentMainType"].ToString());

            int i = Convert.ToInt32(pnode.GetChildRows("Children").Length);
            if (i == 0)
            {
                goto A;
            }

            else
            {

                tree1.Nodes.Add(parentnode);
                parentnode.ImageUrl = "~/Account/images/cabine.png";
                parentnode.CollapseAll();
                parentnode.Value = "expand" + parentnode.Text;

                if (pnode["DocumentMainType"].ToString() == LinkButton2.Text)
                {
                    parentnode.ExpandAll();
                }
            }

            foreach (DataRow cnode in pnode.GetChildRows("Children"))
            {
                //==============   document sub type ===================================
                TreeNode childnode = new TreeNode(cnode["DocumentSubType"].ToString());


                int j = Convert.ToInt32(cnode.GetChildRows("Children2").Length);
                if (j == 0)
                {
                    goto B;
                }
                else
                {
                    parentnode.ChildNodes.Add(childnode);
                    childnode.ImageUrl = "~/Account/images/CloseDrawer1.png";
                    childnode.CollapseAll();
                    childnode.Value = "expand" + childnode.Text;
                    if (cnode["DocumentSubType"].ToString() == LinkButton3.Text)
                    {
                        childnode.ExpandAll();
                    }


                }

                foreach (DataRow lnode in cnode.GetChildRows("Children2"))
                {
                    //============= document type===================
                    TreeNode typenode = new TreeNode(lnode["DocumentType"].ToString());


                    int n = Convert.ToInt32(lnode.GetChildRows("Children3").Length);
                    if (n == 0)
                    {
                        goto C;
                    }
                    else
                    {
                        childnode.ChildNodes.Add(typenode);
                        typenode.ImageUrl = "~/Account/images/closefolder.png";
                        typenode.CollapseAll();
                        typenode.Value = "expand" + typenode.Text;

                        if (lnode["DocumentType"].ToString() == LinkButton5.Text)
                        {
                            typenode.ExpandAll();
                        }

                    }

                    foreach (DataRow docnode in lnode.GetChildRows("Children3"))
                    {
                        // ==========documents==========================

                        TreeNode dnode = new TreeNode(docnode["DocumentTitle"].ToString());
                        typenode.ChildNodes.Add(dnode);
                        dnode.ImageUrl = "~/Account/images/acrobat1.jpg";
                        dnode.Value = docnode["DocumentId"].ToString();
                    }
                C: ;
                    int m = Convert.ToInt32(typenode.ChildNodes.Count);
                    if (m == 0)
                    {
                        childnode.ChildNodes.Remove(typenode);
                    }
                }
            B: ;
                int d = Convert.ToInt32(childnode.ChildNodes.Count);
                if (d == 0)
                {
                    parentnode.ChildNodes.Remove(childnode);
                }
            }
        A: ;
            int k = Convert.ToInt32(parentnode.ChildNodes.Count);
            if (k == 0)
            {
                tree1.Nodes.Remove(parentnode);
            }
        }
    }
    public DataSet getAllType()
    {

        string str = " SELECT Distinct DocumentMainType.DocumentMainTypeId, DocumentMainType.DocumentMainType from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentMainType.CID='" + Session["Comid"].ToString() + "' and DocumentMainType.Whid='" + ViewState["Whid"] + "' and DocumentType.DocumentTypeId In( SELECT  Distinct  DocumentTypeId FROM  DocumentAccessRightMaster WHERE     DesignationId ='" + Session["DesignationId"] + "' and (CID='" + Session["Comid"].ToString() + "' ) and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')  or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))) ORDER BY DocumentMainType";

        SqlDataAdapter adpcat = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        adpcat.Fill(ds, "parent");

        ViewState["row"] = ds.Tables[0].Rows.Count;
        int a = Convert.ToInt32(ViewState["row"]);

        for (int i = 0; i < a; i++)
        {

            string str11 = "SELECT  DocumentSubTypeId, DocumentMainTypeId, DocumentSubType FROM  DocumentSubType " +
                           " WHERE     (DocumentMainTypeId = '" + ds.Tables[0].Rows[i]["DocumentMainTypeId"] + "') " +
                           " ORDER BY DocumentSubType ";

            SqlDataAdapter adpProduct = new SqlDataAdapter(str11, con);
            adpProduct.Fill(ds, "child");
            ds = (DataSet)ds;

        }

        if (ds.Tables[0].Rows.Count > 0)
        {

            ViewState["rowSubnode"] = ds.Tables["child"].Rows.Count;
            int b = Convert.ToInt32(ViewState["rowSubnode"]);

            for (int i = 0; i < b; i++)
            {


                string str1 = "SELECT DocumentTypeId, DocumentSubTypeId, DocumentType FROM DocumentType " +
                                " WHERE     (DocumentSubTypeId = '" + ds.Tables["child"].Rows[i]["DocumentSubTypeId"] + "') " +
                                    " ORDER BY DocumentType ";


                SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                adp11.Fill(ds, "leafchild");
                ds = (DataSet)ds;

            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["rowSubSubnode"] = ds.Tables["leafchild"].Rows.Count;
                int c = Convert.ToInt32(ViewState["rowSubSubnode"]);

                for (int i = 0; i < c; i++)
                {

                    string str12 = " SELECT  DocumentId, DocumentTitle, DocumentUploadDate, DocumentTypeId FROM DocumentMaster " +
                                           " WHERE     (DocumentTypeId = '" + ds.Tables["leafchild"].Rows[i]["DocumentTypeId"] + "')  " +
                           " AND DocumentMaster.DocumentId  in ( SELECT DocumentId FROM   DocumentProcessing " +
                           " WHERE     (Approve = 1) ) AND DocumentMaster.DocumentId not in ( SELECT DocumentId FROM   DocumentProcessing " +
                           " WHERE     (Approve = 0)  or (Approve is null) )" +
                                       " ORDER BY DocumentTitle ";


                    SqlDataAdapter adpDoc = new SqlDataAdapter(str12, con);
                    adpDoc.Fill(ds, "docchild");
                    ds = (DataSet)ds;
                }
            }

            ds.Relations.Add("Children", ds.Tables["parent"].Columns["DocumentMainTypeId"], ds.Tables["child"].Columns["DocumentMainTypeId"]);
            ds.Relations.Add("Children2", ds.Tables["child"].Columns["DocumentSubTypeId"], ds.Tables["leafchild"].Columns["DocumentSubTypeId"]);

            ds.Relations.Add("Children3", ds.Tables["leafchild"].Columns["DocumentTypeId"], ds.Tables["docchild"].Columns["DocumentTypeId"]);

        }

        return ds;
    }
    public DataSet getAllTypecabinetwise()
    {

        string str = " SELECT Distinct DocumentMainType.DocumentMainTypeId, DocumentMainType.DocumentMainType from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentMainType.CID='" + Session["Comid"].ToString() + "' and DocumentMainType.Whid='" + ViewState["Whid"] + "' and DocumentMainType.DocumentMainTypeId='" + ViewState["DocumentMainTypeId"] + "' and DocumentType.DocumentTypeId In( SELECT  Distinct  DocumentTypeId FROM  DocumentAccessRightMaster WHERE     DesignationId ='" + Session["DesignationId"] + "' and (CID='" + Session["Comid"].ToString() + "' ) and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')  or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))) ORDER BY DocumentMainType";

        SqlDataAdapter adpcat = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        adpcat.Fill(ds, "parent");

        ViewState["row"] = ds.Tables[0].Rows.Count;
        int a = Convert.ToInt32(ViewState["row"]);

        for (int i = 0; i < a; i++)
        {

            string str11 = "SELECT  DocumentSubTypeId, DocumentMainTypeId, DocumentSubType FROM  DocumentSubType " +
                           " WHERE     (DocumentMainTypeId = '" + ds.Tables[0].Rows[i]["DocumentMainTypeId"] + "') " +
                           " ORDER BY DocumentSubType ";

            SqlDataAdapter adpProduct = new SqlDataAdapter(str11, con);
            adpProduct.Fill(ds, "child");
            ds = (DataSet)ds;

        }

        if (ds.Tables[0].Rows.Count > 0)
        {

            ViewState["rowSubnode"] = ds.Tables["child"].Rows.Count;
            int b = Convert.ToInt32(ViewState["rowSubnode"]);

            for (int i = 0; i < b; i++)
            {


                string str1 = "SELECT DocumentTypeId, DocumentSubTypeId, DocumentType FROM DocumentType " +
                                " WHERE     (DocumentSubTypeId = '" + ds.Tables["child"].Rows[i]["DocumentSubTypeId"] + "') " +
                                    " ORDER BY DocumentType ";


                SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                adp11.Fill(ds, "leafchild");
                ds = (DataSet)ds;

            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["rowSubSubnode"] = ds.Tables["leafchild"].Rows.Count;
                int c = Convert.ToInt32(ViewState["rowSubSubnode"]);

                for (int i = 0; i < c; i++)
                {

                    string str12 = " SELECT  DocumentId, DocumentTitle, DocumentUploadDate, DocumentTypeId FROM DocumentMaster " +
                                           " WHERE     (DocumentTypeId = '" + ds.Tables["leafchild"].Rows[i]["DocumentTypeId"] + "')  " +
                           " AND DocumentMaster.DocumentId  in ( SELECT DocumentId FROM   DocumentProcessing " +
                           " WHERE     (Approve = 1) ) AND DocumentMaster.DocumentId not in ( SELECT DocumentId FROM   DocumentProcessing " +
                           " WHERE     (Approve = 0)  or (Approve is null) )" +
                                       " ORDER BY DocumentTitle ";


                    SqlDataAdapter adpDoc = new SqlDataAdapter(str12, con);
                    adpDoc.Fill(ds, "docchild");
                    ds = (DataSet)ds;
                }
            }

            ds.Relations.Add("Children", ds.Tables["parent"].Columns["DocumentMainTypeId"], ds.Tables["child"].Columns["DocumentMainTypeId"]);
            ds.Relations.Add("Children2", ds.Tables["child"].Columns["DocumentSubTypeId"], ds.Tables["leafchild"].Columns["DocumentSubTypeId"]);

            ds.Relations.Add("Children3", ds.Tables["leafchild"].Columns["DocumentTypeId"], ds.Tables["docchild"].Columns["DocumentTypeId"]);

        }

        return ds;
    }
    public DataSet getAllTypedrawerwise()
    {

        string str = " SELECT Distinct DocumentMainType.DocumentMainTypeId, DocumentMainType.DocumentMainType from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentMainType.CID='" + Session["Comid"].ToString() + "' and DocumentMainType.Whid='" + ViewState["Whid"] + "' and DocumentSubType.DocumentSubTypeId='" + ViewState["DocumentSubTypeId"] + "'  and DocumentType.DocumentTypeId In( SELECT  Distinct  DocumentTypeId FROM  DocumentAccessRightMaster WHERE     DesignationId ='" + Session["DesignationId"] + "' and (CID='" + Session["Comid"].ToString() + "' ) and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')  or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))) ORDER BY DocumentMainType";

        SqlDataAdapter adpcat = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        adpcat.Fill(ds, "parent");

        ViewState["row"] = ds.Tables[0].Rows.Count;
        int a = Convert.ToInt32(ViewState["row"]);

        for (int i = 0; i < a; i++)
        {

            string str11 = "SELECT  DocumentSubTypeId, DocumentMainTypeId, DocumentSubType FROM  DocumentSubType " +
                           " WHERE     (DocumentMainTypeId = '" + ds.Tables[0].Rows[i]["DocumentMainTypeId"] + "') " +
                           " ORDER BY DocumentSubType ";

            SqlDataAdapter adpProduct = new SqlDataAdapter(str11, con);
            adpProduct.Fill(ds, "child");
            ds = (DataSet)ds;

        }

        if (ds.Tables[0].Rows.Count > 0)
        {

            ViewState["rowSubnode"] = ds.Tables["child"].Rows.Count;
            int b = Convert.ToInt32(ViewState["rowSubnode"]);

            for (int i = 0; i < b; i++)
            {


                string str1 = "SELECT DocumentTypeId, DocumentSubTypeId, DocumentType FROM DocumentType " +
                                " WHERE     (DocumentSubTypeId = '" + ds.Tables["child"].Rows[i]["DocumentSubTypeId"] + "') " +
                                    " ORDER BY DocumentType ";


                SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                adp11.Fill(ds, "leafchild");
                ds = (DataSet)ds;

            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["rowSubSubnode"] = ds.Tables["leafchild"].Rows.Count;
                int c = Convert.ToInt32(ViewState["rowSubSubnode"]);

                for (int i = 0; i < c; i++)
                {

                    string str12 = " SELECT  DocumentId, DocumentTitle, DocumentUploadDate, DocumentTypeId FROM DocumentMaster " +
                                           " WHERE     (DocumentTypeId = '" + ds.Tables["leafchild"].Rows[i]["DocumentTypeId"] + "')  " +
                           " AND DocumentMaster.DocumentId  in ( SELECT DocumentId FROM   DocumentProcessing " +
                           " WHERE     (Approve = 1) ) AND DocumentMaster.DocumentId not in ( SELECT DocumentId FROM   DocumentProcessing " +
                           " WHERE     (Approve = 0)  or (Approve is null) )" +
                                       " ORDER BY DocumentTitle ";


                    SqlDataAdapter adpDoc = new SqlDataAdapter(str12, con);
                    adpDoc.Fill(ds, "docchild");
                    ds = (DataSet)ds;
                }
            }

            ds.Relations.Add("Children", ds.Tables["parent"].Columns["DocumentMainTypeId"], ds.Tables["child"].Columns["DocumentMainTypeId"]);
            ds.Relations.Add("Children2", ds.Tables["child"].Columns["DocumentSubTypeId"], ds.Tables["leafchild"].Columns["DocumentSubTypeId"]);

            ds.Relations.Add("Children3", ds.Tables["leafchild"].Columns["DocumentTypeId"], ds.Tables["docchild"].Columns["DocumentTypeId"]);

        }

        return ds;
    }
    public DataSet getAllTypefolderwise()
    {

        string str = " SELECT Distinct DocumentMainType.DocumentMainTypeId, DocumentMainType.DocumentMainType from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentMainType.CID='" + Session["Comid"].ToString() + "' and DocumentMainType.Whid='" + ViewState["Whid"] + "' and DocumentSubType.DocumentSubTypeId='" + ViewState["DocumentSubTypeId"] + "' and DocumentType.DocumentTypeId='" + ViewState["DocumentTypeId"] + "'  and DocumentType.DocumentTypeId In( SELECT  Distinct  DocumentTypeId FROM  DocumentAccessRightMaster WHERE     DesignationId ='" + Session["DesignationId"] + "' and (CID='" + Session["Comid"].ToString() + "' ) and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')  or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))) ORDER BY DocumentMainType";

        SqlDataAdapter adpcat = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        adpcat.Fill(ds, "parent");

        ViewState["row"] = ds.Tables[0].Rows.Count;
        int a = Convert.ToInt32(ViewState["row"]);

        for (int i = 0; i < a; i++)
        {

            string str11 = "SELECT  DocumentSubTypeId, DocumentMainTypeId, DocumentSubType FROM  DocumentSubType " +
                           " WHERE     (DocumentMainTypeId = '" + ds.Tables[0].Rows[i]["DocumentMainTypeId"] + "') " +
                           " ORDER BY DocumentSubType ";

            SqlDataAdapter adpProduct = new SqlDataAdapter(str11, con);
            adpProduct.Fill(ds, "child");
            ds = (DataSet)ds;

        }

        if (ds.Tables[0].Rows.Count > 0)
        {

            ViewState["rowSubnode"] = ds.Tables["child"].Rows.Count;
            int b = Convert.ToInt32(ViewState["rowSubnode"]);

            for (int i = 0; i < b; i++)
            {


                string str1 = "SELECT DocumentTypeId, DocumentSubTypeId, DocumentType FROM DocumentType " +
                                " WHERE     (DocumentSubTypeId = '" + ds.Tables["child"].Rows[i]["DocumentSubTypeId"] + "') " +
                                    " ORDER BY DocumentType ";


                SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                adp11.Fill(ds, "leafchild");
                ds = (DataSet)ds;

            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["rowSubSubnode"] = ds.Tables["leafchild"].Rows.Count;
                int c = Convert.ToInt32(ViewState["rowSubSubnode"]);

                for (int i = 0; i < c; i++)
                {

                    string str12 = " SELECT  DocumentId, DocumentTitle, DocumentUploadDate, DocumentTypeId FROM DocumentMaster " +
                                           " WHERE     (DocumentTypeId = '" + ds.Tables["leafchild"].Rows[i]["DocumentTypeId"] + "')  " +
                           " AND DocumentMaster.DocumentId  in ( SELECT DocumentId FROM   DocumentProcessing " +
                           " WHERE     (Approve = 1) ) AND DocumentMaster.DocumentId not in ( SELECT DocumentId FROM   DocumentProcessing " +
                           " WHERE     (Approve = 0)  or (Approve is null) )" +
                                       " ORDER BY DocumentTitle ";


                    SqlDataAdapter adpDoc = new SqlDataAdapter(str12, con);
                    adpDoc.Fill(ds, "docchild");
                    ds = (DataSet)ds;
                }
            }

            ds.Relations.Add("Children", ds.Tables["parent"].Columns["DocumentMainTypeId"], ds.Tables["child"].Columns["DocumentMainTypeId"]);
            ds.Relations.Add("Children2", ds.Tables["child"].Columns["DocumentSubTypeId"], ds.Tables["leafchild"].Columns["DocumentSubTypeId"]);

            ds.Relations.Add("Children3", ds.Tables["leafchild"].Columns["DocumentTypeId"], ds.Tables["docchild"].Columns["DocumentTypeId"]);

        }

        return ds;
    }

    protected void imgimgo_Click(object sender, EventArgs e)
    {
        LoadPdfwithno(Convert.ToInt32(ViewState["docid"]), lblnooff.Text);

    }
    protected void imgfirstimg_Click(object sender, ImageClickEventArgs e)
    {
        lblnooff.Text = "1";
        LoadPdfwithno(Convert.ToInt32(ViewState["docid"]), lblnooff.Text);
    }
    protected void imgpriimg_Click(object sender, ImageClickEventArgs e)
    {
        int no = Convert.ToInt32(lblnooff.Text) - 1;
        if (no <= 1)
        {
            no = 1;
        }
        lblnooff.Text = no.ToString();
        LoadPdfwithno(Convert.ToInt32(ViewState["docid"]), no.ToString());

    }
    protected void imgnextimg_Click(object sender, ImageClickEventArgs e)
    {
        int no = Convert.ToInt32(lblnooff.Text) + 1;
        if (no >= Convert.ToInt32(lblnototal.Text))
        {
            no = Convert.ToInt32(lblnototal.Text);
        }
        lblnooff.Text = no.ToString();
        LoadPdfwithno(Convert.ToInt32(ViewState["docid"]), no.ToString());

    }
    protected void imglastimg_Click(object sender, ImageClickEventArgs e)
    {
        lblnooff.Text = lblnototal.Text;
        LoadPdfwithno(Convert.ToInt32(ViewState["docid"]), lblnototal.Text);
    }
    protected void tree1_SelectedNodeChanged(object sender, EventArgs e)
    {
        string sub = "";
        string name = tree1.SelectedValue;
        if (name.Length > 6)
        {
            sub = name.Substring(0, 6);
            if (sub == "expand")
            {
                tree1.CollapseAll();
                tree1.SelectedNode.ToggleExpandState();
                foreach (TreeNode pnode in tree1.Nodes)
                {
                    foreach (TreeNode cnode in pnode.ChildNodes)
                    {
                        foreach (TreeNode lnode in cnode.ChildNodes)
                        {
                            if (tree1.SelectedNode == cnode)
                            {
                                cnode.Parent.Expand();
                            }
                            if (tree1.SelectedNode == lnode)
                            {
                                cnode.Parent.Expand();
                                lnode.Parent.Expand();
                            }
                        }

                    }

                }

            }
            else
            {

                int docid = Convert.ToInt32(tree1.SelectedNode.Value);

                string te = "ViewDocument.aspx?id=" + docid + "&Did= " + Session["DesignationId"] + "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

                //Response.Redirect("ViewDocument.aspx?id=" + docid + "&Did= " + Session["DesignationId"] + "");

            }
        }
        else
        {
            int docid = Convert.ToInt32(tree1.SelectedNode.Value);

            string te = "ViewDocument.aspx?id=" + docid + "&Did= " + Session["DesignationId"] + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


        }

    }
}
