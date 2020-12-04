using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;

using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Web.UI.WebControls.Adapters;
using System.Web.UI;
using System.Security.Cryptography;
using System.Diagnostics;
public partial class DocumentViewandAccount : System.Web.UI.Page
{

    string compid;

    DBCommands1 dbss1 = new DBCommands1();


    SqlConnection con;
    SqlConnection conn;
    SqlConnection connn;
    SqlConnection conn1;
    protected int DesignationId;
    protected string DocumentName;
    protected int DocumentID;
    DocumentCls1 clsDocument = new DocumentCls1();

    EmployeeCls clsEmployee = new EmployeeCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        conn = pgcon.dynconn;
        connn = pgcon.dynconn;
        conn1 = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();
        char[] separator = new char[] { '/' };
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);




        if (!IsPostBack)
        {
            txtfrom.Text = System.DateTime.Now.ToShortDateString();

            txtto.Text = System.DateTime.Now.ToShortDateString();

            CheckBox1_CheckedChanged(sender, e);

            ViewState["docid"] = "0";
            compid = Session["comid"].ToString();
            Session["no"] = "1";
            Session["no1"] = "1";
            ViewState["dtrws"] = "1";

            fillstore();
            fillpartytype();
            FillParty();
            fillmaintaype();
           // fillsubtype();
            filldll();
            FillDocumentTypeAll();

            ddldoctype_SelectedIndexChanged(sender, e);
            defaultviewswitch();
        }
        DocumentID = Convert.ToInt32(ViewState["docId"]);
        DesignationId = Convert.ToInt32(Session["DesignationId"]);

    }
    protected void FillDocDetail(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        int Docid = Convert.ToInt32(ViewState["docid"]);
        dt = clsDocument.SelectDoucmentMasterByID(Docid);
        if (dt.Rows.Count > 0)
        {
            //lblUploadDate.Text = Convert.ToDateTime(dt.Rows[0]["DocumentUploadDate"]).ToShortDateString();
            //txtdoctitle.Text = dt.Rows[0]["DocumentTitle"].ToString();
            //txtdocrefnmbr.Text = dt.Rows[0]["DocumentRefNo"].ToString();
            //if (txtdoctitle.Text == "")
            //{
            //    txtdoctitle.Text = "Untitled";
            //}
            //txtdocdscrptn.Text = dt.Rows[0]["Description"].ToString();
            ////=============Fill All Combo=========================
            //DataTable dt1 = new DataTable();
            //if (Convert.ToString(dt.Rows[0]["DocumentTypeId"]) != "0")
            //{

            //    ddldoctype.SelectedIndex = ddldoctype.Items.IndexOf(ddldoctype.Items.FindByValue(dt.Rows[0]["DocumentTypeId"].ToString()));
            //}
            //else
            //{
            //    ddldoctype.SelectedIndex = 0;

            //}
            //if (dt.Rows[0]["PartyId"] != System.DBNull.Value)
            //{
            //    ddlpartyname1.SelectedIndex = ddlpartyname1.Items.IndexOf(ddlpartyname1.Items.FindByValue(dt.Rows[0]["PartyId"].ToString()));
            //}
            //else
            //{
            //    ddlpartyname1.SelectedValue = "0";
            //}
            //txtnetamount1.Text = dt.Rows[0]["DocumentAmount"].ToString();
            //if (txtnetamount1.Text == "")
            //{
            //    txtnetamount1.Text = "0.0";
            //}
            //ViewState["docname"] = dt.Rows[0]["DocumentName"].ToString();
            //ViewState["docId"] = Convert.ToInt32(dt.Rows[0]["DocumentId"]);
            //hdnDocId.Value = dt.Rows[0]["DocumentId"].ToString();
            //DocumentName = ViewState["docname"].ToString();
            //DocumentID = Convert.ToInt32(ViewState["docId"]);
            //dt = new DataTable();
            //dt = clsDocument.SelectDocumentDateDetail(DocumentID);
            //if (dt.Rows.Count > 0)
            //{
            //    txtDate.Text = Convert.ToDateTime(dt.Rows[0]["DocumentDate"]).ToShortDateString();
            //}
        }
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


                string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);
                string strft = "Select FileStorage.* from FileStorage Where B='" + encryptstrring(Session["comid"].ToString()) + "' and H='" + encryptstrring("True") + "'";

                SqlCommand cmdft = new SqlCommand(strft, con);
                SqlDataAdapter adpft = new SqlDataAdapter(cmdft);
                DataTable dtft = new DataTable();
                adpft.Fill(dtft);

                if (dtft.Rows.Count > 0)
                {
                    FileInfo filec = new FileInfo(filepath);
                    if (!filec.Exists)
                    {
                        datatransftp(docname, filepath);
                        System.Threading.Thread.Sleep(1000);
                        FileInfo filecup = new FileInfo(filepath);
                        if (filecup.Exists)
                        {

                            string filepathu = Server.MapPath("~//Account//pdftoimage.exe");
                            System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepathu);

                            pti.UseShellExecute = false;
                            pti.Arguments = filepathu + " -i UploadedDocuments//" + docname + " " + "-o" + " " + "DocumentImage//";//+ " " + "-r" + "VNKSURDLWQOVHPGH";


                            pti.RedirectStandardOutput = true;
                            pti.RedirectStandardInput = true;
                            pti.RedirectStandardError = true;

                            pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");
                            System.Diagnostics.Process ps = Process.Start(pti);
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                }
                int length = docname.Length;
                string docnameIn = docname.Substring(0, length - 4);
                ViewState["path"] = filepath.ToString();
                DataTable dt1 = new DataTable();
                DataColumn dcom = new DataColumn();
                dcom.ColumnName = "image";
                dcom.DataType = System.Type.GetType("System.String");
                dt1.Columns.Add(dcom);
                DataTable dt2 = new DataTable();
                dt2 = clsDocument.SelectDoucmentImageMaster(Docid);
                if (dt2.Rows.Count > 0)
                {
                    int sav = 0;
                    for (int kk = 1; kk <= dt2.Rows.Count; kk++)
                    {
                        DataRow drow = dt1.NewRow();
                        if (Convert.ToString(Session["no"]) == Convert.ToString(Session["no1"]))
                        {
                            int no = Convert.ToInt32(Session["no"]);
                            sav = no;
                            drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + dt2.Rows[no - 1]["DocumentImgName"].ToString();
                            Session["no1"] = Convert.ToInt32(Session["no1"]) + 1;
                            dt1.Rows.Add(drow);
                            DataRow drow1 = dt1.NewRow();
                            if (no != 1)
                            {
                                drow1["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + dt2.Rows[kk - 1]["DocumentImgName"].ToString();
                                dt1.Rows.Add(drow1);
                            }
                        }
                        else
                        {
                            if (sav == kk)
                            {
                            }
                            else
                            {
                                drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + dt2.Rows[kk - 1]["DocumentImgName"].ToString();
                                dt1.Rows.Add(drow);
                            }
                        }


                        lblnooff.Text = Session["no"].ToString();
                        lblnototal.Text = dt2.Rows.Count.ToString();
                    }
                }
                else
                {
                    Session["no"] = "1";
                    Session["no1"] = "1";
                    lblnototal.Text = "1";
                    lblnooff.Text = "1";
                    docnameIn += "00001.jpg";
                    DataRow drow = dt1.NewRow();
                    drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + docnameIn;
                    dt1.Rows.Add(drow);

                }

                DataList1.DataSource = dt1;
                DataList1.DataBind();
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
    protected void FillDocumentTypeAll()
    {
        DocumentCls1 clsDocument = new DocumentCls1();
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocTypeAll(ddlWarehouse.SelectedValue);
        ddldoctype.DataSource = dt;
        ddldoctype.DataBind();

    }
    protected void ibtnFirst_Click(object sender, ImageClickEventArgs e)
    {
        Int32 DocId;
        IbtnNext.Enabled = false;
        DocId = 0;
        string str111 = "  Select Min(DocumentId) as DocumentId from  [DocumentMaster]  where DocumentTypeId='" + ddldoctype.SelectedValue + "'";
        SqlCommand cmd11 = new SqlCommand(str111);
        cmd11.Connection = connn;
        SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
        DataTable dts1 = new DataTable();
        da11.Fill(dts1);
        if (dts1.Rows.Count > 0)
        {
            ViewState["docid"] = dts1.Rows[0]["DocumentId"];

            DocId = Convert.ToInt32(ViewState["docid"]);
            hdnDocId.Value = DocId.ToString();
            LoadPdf(DocId);
            FillDocDetail(sender, e);
            txtdocno.Text = "1";
            ddllistofdoc.SelectedIndex = ddllistofdoc.Items.IndexOf(ddllistofdoc.Items.FindByValue(DocId.ToString()));

            
            defaultviewswitch();
        }

        ibtnFirst.Enabled = false;
        IbtnNext.Enabled = true;
        IbtnPrev.Enabled = false;
        IbtnLast.Enabled = true;
    }
    protected void IbtnPrev_Click(object sender, ImageClickEventArgs e)
    {
        btnaccprivi_Click(sender, e);
    }
    protected void IbtnNext_Click(object sender, ImageClickEventArgs e)
    {
        btnaccnext_Click(sender, e);

    }
    protected void btnaccnext_Click(object sender, EventArgs e)
    {
        Int32 DocId;
        DocId = 0;
        DataTable dt = new DataTable();
        string toprecord = "Top 0";
        if (TextBox1.Text != "")
        {
            toprecord = "Top  " + TextBox1.Text;
        }


        //dt = clsDocument.SelectDocumentforApprovalNext (Convert.ToInt32(hdnDocId.Value));
        string str111 = "  Select  DocumentId as DocumentId from  [DocumentMaster] where DocumentId>('" + ViewState["docid"] + "')  And DocumentTypeId='" + ddldoctype.SelectedValue + "' order by DocumentId ";
        SqlCommand cmd11 = new SqlCommand(str111);
        cmd11.Connection = connn;
        SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
        DataTable dts1 = new DataTable();
        da11.Fill(dts1);
        if (dts1.Rows.Count > 0)
        {
            ViewState["docid"] = dts1.Rows[0]["DocumentId"];

            DocId = Convert.ToInt32(ViewState["docid"]);
            hdnDocId.Value = DocId.ToString();
            LoadPdf(DocId);
            txtdocno.Text = (Convert.ToInt32(txtdocno.Text) + 1).ToString();
            ddllistofdoc.SelectedIndex = ddllistofdoc.Items.IndexOf(ddllistofdoc.Items.FindByValue(DocId.ToString()));

            ddldocpreviousnext();
            defaultviewswitch();
            //FillDocDetail(sender, e);

        }

        ibtnFirst.Enabled = true;
        IbtnNext.Enabled = true;
        IbtnPrev.Enabled = true;
        IbtnLast.Enabled = true;
    }
    protected void btnaccprivi_Click(object sender, EventArgs e)
    {
        Int32 DocId;
        DocId = 0;
        DataTable dt = new DataTable();
        string toprecord = "Top 0";
        if (TextBox1.Text != "")
        {
            toprecord = "Top  " + TextBox1.Text;
        }

        string str111 = "  Select  DocumentId as DocumentId from  [DocumentMaster] where DocumentId<('" + ViewState["docid"] + "')  And DocumentTypeId='" + ddldoctype.SelectedValue + "' order by DocumentId Desc";
        SqlCommand cmd11 = new SqlCommand(str111);
        cmd11.Connection = connn;
        SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
        DataTable dts1 = new DataTable();
        da11.Fill(dts1);
        if (dts1.Rows.Count > 0)
        {
            ViewState["docid"] = dts1.Rows[0]["DocumentId"];

            DocId = Convert.ToInt32(ViewState["docid"]);
            hdnDocId.Value = DocId.ToString();


            LoadPdf(DocId);
            txtdocno.Text = (Convert.ToInt32(txtdocno.Text) - 1).ToString();
            ddllistofdoc.SelectedIndex = ddllistofdoc.Items.IndexOf(ddllistofdoc.Items.FindByValue(DocId.ToString()));

            ddldocpreviousnext();
            defaultviewswitch();
            //FillDocDetail(sender, e);
        }

        ibtnFirst.Enabled = true;
        IbtnNext.Enabled = true;
        IbtnPrev.Enabled = true;
        IbtnLast.Enabled = true;
    }
    protected void IbtnLast_Click(object sender, ImageClickEventArgs e)
    {
        Int32 DocId;
        DocId = 0;
        ibtnFirst.Enabled = true;
        IbtnNext.Enabled = false;
        IbtnPrev.Enabled = true;
        IbtnLast.Enabled = false;
        string str111 = "  Select Max(DocumentId) as DocumentId from  [DocumentMaster]  where DocumentTypeId='" + ddldoctype.SelectedValue + "' ";
        SqlCommand cmd11 = new SqlCommand(str111);
        cmd11.Connection = connn;
        SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
        DataTable dts1 = new DataTable();
        da11.Fill(dts1);
        if (dts1.Rows.Count > 0)
        {
            ViewState["docid"] = dts1.Rows[0]["DocumentId"];

            DocId = Convert.ToInt32(ViewState["docid"]);
            hdnDocId.Value = DocId.ToString();
            txtdocno.Text = lblofno.Text;
            ddllistofdoc.SelectedIndex = ddllistofdoc.Items.IndexOf(ddllistofdoc.Items.FindByValue(DocId.ToString()));

            
            defaultviewswitch();
        }
        if (DocId != 0)
        {
            LoadPdf(DocId);
            FillDocDetail(sender, e);
        }
    }

    protected void ddldoctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddllistofdocument();

        ddldocpreviousnext();
        defaultviewswitch();

        Session["no"] = "1";
        Session["no1"] = "1";


        DataTable dts1 = listofdoc();

        if (dts1.Rows.Count > 0)
        {
            ViewState["docid"] = dts1.Rows[0]["DocumentId"];
            txtdocno.Text = "1";
            lblofno.Text = dts1.Rows.Count.ToString();
        }
        else
        {
            txtdocno.Text = "0";
            lblofno.Text = "0";
        }


        ibtnFirst.Enabled = false;
        IbtnNext.Enabled = true;
        IbtnPrev.Enabled = false;
        IbtnLast.Enabled = true;
        LoadPdf(Convert.ToInt32(ViewState["docid"]));


    }
    protected void imgfirstimg_Click(object sender, ImageClickEventArgs e)
    {
        Session["no"] = 1;
        Session["no1"] = 1;
        LoadPdf(Convert.ToInt32(ViewState["docid"]));
        imgfirstimg.Enabled = false;
        imgnextimg.Enabled = true;
        imgpriimg.Enabled = false;
        imglastimg.Enabled = true;
    }
    protected void imglastimg_Click(object sender, ImageClickEventArgs e)
    {
        Session["no"] = Convert.ToInt32(lblnototal.Text);
        Session["no1"] = Convert.ToInt32(lblnototal.Text);
        LoadPdf(Convert.ToInt32(ViewState["docid"]));
        imgfirstimg.Enabled = true;
        imgnextimg.Enabled = false;
        imgpriimg.Enabled = true;
        imglastimg.Enabled = false;
    }
    protected void imgpriimg_Click(object sender, ImageClickEventArgs e)
    {
        Session["no"] = Convert.ToInt32(Session["no"]) - 1;
        Session["no1"] = Convert.ToInt32(Session["no1"]) - 2;
        LoadPdf(Convert.ToInt32(ViewState["docid"]));
        if (lblnototal.Text == Session["no"].ToString())
        {
            imgpriimg.Enabled = false;
            imgfirstimg.Enabled = false;
        }
        else
        {
            imgpriimg.Enabled = true;
            imgfirstimg.Enabled = true;

        }
        imgfirstimg.Enabled = true;
        imgnextimg.Enabled = true;


    }
    protected void imgnextimg_Click(object sender, ImageClickEventArgs e)
    {
        Session["no"] = Convert.ToInt32(Session["no"]) + 1;
        Session["no1"] = Convert.ToInt32(Session["no1"]);
        LoadPdf(Convert.ToInt32(ViewState["docid"]));
        if (lblnototal.Text.ToString() == Session["no"].ToString())
        {
            imgnextimg.Enabled = false;
            imglastimg.Enabled = false;
        }
        else
        {
            imgnextimg.Enabled = true;
            imglastimg.Enabled = true;

        }
        imgfirstimg.Enabled = true;

        imgpriimg.Enabled = true;
        imglastimg.Enabled = true;
    }
    protected void imgimgo_Click(object sender, EventArgs e)
    {
        Session["no"] = lblnooff.Text;
        Session["no1"] = lblnooff.Text;
        LoadPdf(Convert.ToInt32(ViewState["docid"]));
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
    protected void lbldocbtn_Click(object sender, EventArgs e)
    {
        Int32 DocId;
        DocId = 0;
        int cc = 0;
        DataTable dt = new DataTable();
        if (txtdocno.Text.Length > 0)
        {
            string str111 = "";
            string str111e = "";


            str111e = "  Select Row_NUMBER()OVER(Order by (Select 0)) as rt, DocumentId as DocumentId from  [DocumentMaster] where DocumentTypeId='" + ddldoctype.SelectedValue + "' order by DocumentId ";
            SqlCommand cmd11e = new SqlCommand(str111e);
            cmd11e.Connection = connn;
            SqlDataAdapter da11e = new SqlDataAdapter(cmd11e);
            DataTable dts1e = new DataTable();
            da11e.Fill(dts1e);


            if (dts1e.Rows.Count > 0)
            {

                cc = Convert.ToInt32(txtdocno.Text);
                if (Convert.ToInt32(lblofno.Text) >= (cc))
                {

                    str111 = "  Select Row_NUMBER()OVER(Order by (Select 0)) as rt, DocumentId as DocumentId from  [DocumentMaster] where DocumentId='" + dts1e.Rows[cc - 1]["DocumentId"] + "'  And DocumentTypeId='" + ddldoctype.SelectedValue + "' order by DocumentId ";
                    txtdocno.Text = (Convert.ToInt32(txtdocno.Text)).ToString();
                }
                else
                {
                    cc = 1;
                    str111 = "  Select Row_NUMBER()OVER(Order by (Select 0)) as rt, DocumentId as DocumentId from  [DocumentMaster] where DocumentId='" + dts1e.Rows[0]["DocumentId"] + "'  And DocumentTypeId='" + ddldoctype.SelectedValue + "' order by DocumentId ";
                    txtdocno.Text = (1).ToString();
                }
                SqlCommand cmd11 = new SqlCommand(str111);
                cmd11.Connection = connn;
                SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                DataTable dts1 = new DataTable();
                da11.Fill(dts1);
                if (dts1.Rows.Count > 0)
                {
                    ViewState["docid"] = dts1.Rows[0]["DocumentId"];

                    DocId = Convert.ToInt32(ViewState["docid"]);
                    hdnDocId.Value = DocId.ToString();
                    LoadPdf(DocId);
                    ddllistofdoc.SelectedIndex = ddllistofdoc.Items.IndexOf(ddllistofdoc.Items.FindByValue(DocId.ToString()));
                    FillDocDetail(sender, e);
                }
            }
        }
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentTypeAll();

        ddldoctype_SelectedIndexChanged(sender, e);
        DocumentID = Convert.ToInt32(ViewState["docId"]);
        DesignationId = Convert.ToInt32(Session["DesignationId"]);
    }
    protected void fillstore()
    {
        ddlWarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlWarehouse.DataSource = ds;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "WareHouseId";
        ddlWarehouse.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

        }


    }
    protected void fillddllistofdocument()
    {

        DataTable dtcat = listofdoc();

        ddllistofdoc.DataSource = dtcat;
        ddllistofdoc.DataTextField = "DocumentTitle";
        ddllistofdoc.DataValueField = "DocumentId";
        ddllistofdoc.DataBind();

        showentry();

    }
    protected void ddllistofdoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllistofdoc.SelectedIndex > -1)
        {
            ddldocpreviousnext();
            defaultviewswitch();
            Session["no"] = "1";
            Session["no1"] = "1";

            ViewState["docid"] = Convert.ToInt32(ddllistofdoc.SelectedValue);
            LoadPdf(Convert.ToInt32(ddllistofdoc.SelectedValue));
        }
    }
    protected void ddldocpreviousnext()
    {

        Session["no"] = "1";
        Session["no1"] = "1";
        DataTable dtcat = rowofdoc();

        if (dtcat.Rows.Count > 0)
        {
            string rowno = "1";
            string docid = ddllistofdoc.SelectedValue;

            foreach (DataRow dr in dtcat.Rows)
            {
                rowno = dr["RowNo"].ToString();
                if (dr["DocumentId"].ToString() == docid)
                {
                    rowno = dr["RowNo"].ToString();
                    break;
                }
            }

            txtdocno.Text = rowno;
            lblofno.Text = dtcat.Rows.Count.ToString();
        }
        else
        {
            txtdocno.Text = "0";
            lblofno.Text = "0";
        }
    }

    protected DataTable listofdoc()
    {
        string toprecord = "Top 0";
        if (TextBox1.Text != "")
        {
             toprecord = "Top  " + TextBox1.Text;
        }
        string strcat = "SELECT   DocumentMaster.DocumentId,Convert(Nvarchar(50),DocumentMaster.DocumentId) + ':' +DocumentMaster.DocumentTitle as DocumentTitle  FROM   DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId LEFT OUTER JOIN DocumentProcessing ON DocumentMaster.DocumentId = DocumentProcessing.DocumentId LEFT OUTER JOIN Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId  WHERE  DocumentMainType.Whid='" + ddlWarehouse.SelectedValue + "' and DocumentType.DocumentTypeId='" + ddldoctype.SelectedValue + "' AND DocumentMaster.DocumentId  in ( SELECT  distinct   DocumentId FROM         DocumentProcessing WHERE     (Approve = 1) ) AND DocumentMaster.DocumentId not in ( SELECT distinct    DocumentId FROM         DocumentProcessing WHERE     (Approve = 0) or (Approve is null) )  and(DocumentMaster.CID='" + Session["Comid"] + "')  ";
        string strtypeid = "";
      
        string strbydate = "";

        if (ddldoctype.SelectedIndex > 0)
        {
            strtypeid = " And DocumentMaster.DocumentTypeId='" + ddldoctype.SelectedValue + "'";
        }

        if (CheckBox1.Checked == true)
        {
            if (RadioButtonList1.SelectedIndex == 0)
            {
                strbydate = " and (DocumentMaster.DocumentDate between '" + Convert.ToDateTime(txtfrom.Text).ToShortDateString() + "' and '" + Convert.ToDateTime(txtto.Text).ToShortDateString() + "')";

            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                strbydate = " and (DocumentMaster.DocumentUploadDate between '" + Convert.ToDateTime(txtfrom.Text).ToString() + "' and '" + Convert.ToDateTime(txtto.Text).ToString() + "')";


            }
        }

        string orderby = " order by DocumentUploadDate ";

        string finalstr = strcat + strtypeid +  strbydate + orderby;

        SqlCommand cmdcat = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);

        return dtcat;

    }
    protected DataTable rowofdoc()
    {
        string strcat = "SELECT Row_NUMBER()OVER(Order by DocumentMaster.DocumentId) as RowNo, DocumentMaster.DocumentId,Convert(Nvarchar(50),DocumentMaster.DocumentId) + ':' +DocumentMaster.DocumentTitle as DocumentTitle  FROM   DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId LEFT OUTER JOIN DocumentProcessing ON DocumentMaster.DocumentId = DocumentProcessing.DocumentId LEFT OUTER JOIN Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId  WHERE  DocumentMainType.Whid='" + ddlWarehouse.SelectedValue + "' and DocumentType.DocumentTypeId='" + ddldoctype.SelectedValue + "' AND DocumentMaster.DocumentId  in ( SELECT  distinct   DocumentId FROM         DocumentProcessing WHERE     (Approve = 1) ) AND DocumentMaster.DocumentId not in ( SELECT distinct    DocumentId FROM         DocumentProcessing WHERE     (Approve = 0) or (Approve is null) )  and(DocumentMaster.CID='" + Session["Comid"] + "') ";
        string strtypeid = "";
        string strbyperiod = "";
        string strbydate = "";

        if (ddldoctype.SelectedIndex > 0)
        {
            strtypeid = " And DocumentMaster.DocumentTypeId='" + ddldoctype.SelectedValue + "'";
        }


        string finalstr = strcat + strtypeid + strbyperiod + strbydate;

        SqlCommand cmdcat = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);

        return dtcat;

    }
    protected void fillpartytype()
    {
        string qryStr = "select * from PartytTypeMaster  where  compid='" + Session["Comid"] + "'  order by PartType";
        SqlCommand cmdeeed = new SqlCommand(qryStr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);


        ddlPartyType.DataSource = dteeed;
        ddlPartyType.DataTextField = "PartType";
        ddlPartyType.DataValueField = "PartyTypeId";
        ddlPartyType.DataBind();

    }
    protected void FillParty()
    {
        ddlpartyname.Items.Clear();
        string qryStr = "";

        if (ddlPartyType.SelectedItem.Text == "Employee")
        {
            qryStr = "select Party_master.PartyID,DepartmentmasterMNC.Departmentname +':'+ DesignationMaster.DesignationName +':'+ EmployeeMaster.EmployeeName as PartyName  from EmployeeMaster inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId inner join DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId WHERE   Party_master.Whid='" + ddlWarehouse.SelectedValue + "' and Party_master.PartyTypeId='" + ddlPartyType.SelectedValue + "' order by PartyName ";
        }
        else if (ddlPartyType.SelectedItem.Text == "Candidate")
        {
            qryStr = " select Party_master.PartyID, CandidateMaster.LastName +' '+ CandidateMaster.FirstName +' '+ CandidateMaster.MiddleName as PartyName  from CandidateMaster inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId WHERE   Party_master.Whid='" + ddlWarehouse.SelectedValue + "' and Party_master.PartyTypeId='" + ddlPartyType.SelectedValue + "' order by PartyName  ";
        }
        else
        {
            qryStr = "SELECT  PartyId, Party_Master.PartyTypeId, Party_Master.Compname + ':'+ Party_Master.ContactPerson as PartyName, ContactPerson FROM   Party_master inner JOIN    PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId WHERE   Party_master.Whid='" + ddlWarehouse.SelectedValue + "' and Party_master.PartyTypeId='" + ddlPartyType.SelectedValue + "' order by PartyName  ";
        }



        SqlCommand cmdeeed = new SqlCommand(qryStr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        ddlpartyname.DataSource = dteeed;
        ddlpartyname.DataTextField = "PartyName";
        ddlpartyname.DataValueField = "PartyId";
        ddlpartyname.DataBind();
        ddlpartyname.Items.Insert(0, "-Select-");
        ddlpartyname.Items[0].Value = "0";


    }
    protected void ddlPartyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillParty();
    }
    protected void fillmaintaype()
    {
        ddldocmaintype.Items.Clear();

        string doc = "SELECT DocumentMainTypeId,DocumentMainType as DocumentMainType FROM  [dbo].[DocumentMainType]   where CID='" + Session["comid"] + "' and Whid='" + ddlWarehouse.SelectedValue + "'";
        SqlDataAdapter adp = new SqlDataAdapter(doc, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        ddldocmaintype.DataSource = dt;
        ddldocmaintype.DataTextField = "DocumentMainType";
        ddldocmaintype.DataValueField = "DocumentMainTypeId";
        ddldocmaintype.DataBind();



    }
    //protected void fillsubtype()
    //{
    //    ddldocsubtypename.Items.Clear();
    //    //string str178 = " SELECT     DocumentSubType.DocumentSubTypeId, DocumentSubType.DocumentSubType, DocumentMainType.DocumentMainTypeId as DocumentMainTypeId,  DocumentMainType.DocumentMainType FROM         DocumentMainType INNER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId WHERE     (DocumentMainType.DocumentMainTypeId = '" + ddldocmaintype.SelectedValue + "') and DocumentMainType.CID='" + Session["Comid"] + "' ";

    //    string str178 = "SELECT    DocumentType.DocumentType, DocumentType.DocumentTypeId FROM         DocumentMainType INNER JOIN   DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId INNER JOIN   DocumentType ON DocumentSubType.DocumentSubTypeId = DocumentType.DocumentSubTypeId where DocumentMainType.CID='" + Session["Comid"] + "' and DocumentMainType.Whid='" + ddlWarehouse.SelectedValue + "' and DocumentMainType.DocumentMainTypeId = '" + ddldocmaintype.SelectedValue + "' and DocumentMainType.CID='" + Session["Comid"] + "'  order by DocumentType.DocumentType ";

    //    SqlCommand cgw = new SqlCommand(str178, con);
    //    SqlDataAdapter adgw = new SqlDataAdapter(cgw);
    //    DataTable dt = new DataTable();
    //    adgw.Fill(dt);
    //    //ddldocsubtypename.DataSource = dt;
    //    //ddldocsubtypename.DataTextField = "DocumentSubType";
    //    //ddldocsubtypename.DataValueField = "DocumentSubTypeId";
    //    //ddldocsubtypename.DataBind();
    //    ddldocsubtypename.DataSource = dt;
    //    ddldocsubtypename.DataTextField = "DocumentType";
    //    ddldocsubtypename.DataValueField = "DocumentTypeId";
    //    ddldocsubtypename.DataBind();

    //}
    public void filldll()
    {
        ddldoctypeup.Items.Clear();
        string str178 = "SELECT  DocumentSubType.DocumentSubType + ' - '+ DocumentType.DocumentType as DocumentType , DocumentType.DocumentTypeId FROM         DocumentMainType INNER JOIN   DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId INNER JOIN   DocumentType ON DocumentSubType.DocumentSubTypeId = DocumentType.DocumentSubTypeId where DocumentMainType.CID='" + Session["Comid"] + "' and DocumentMainType.Whid='" + ddlWarehouse.SelectedValue + "' and DocumentMainType.DocumentMainTypeId = '" + ddldocmaintype.SelectedValue + "' and DocumentMainType.CID='" + Session["Comid"] + "' order by DocumentType.DocumentType ";
        SqlCommand cgw = new SqlCommand(str178, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);

        ddldoctypeup.DataSource = dt;
        ddldoctypeup.DataTextField = "DocumentType";
        ddldoctypeup.DataValueField = "DocumentTypeId";
        ddldoctypeup.DataBind();


    }
    protected void ddldocmaintype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillsubtype();
        filldll();

    }
    //protected void ddldocsubtypename_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    filldll();
    //}
    protected void defaultviewswitch()
    {
        if (ddllistofdoc.SelectedIndex > -1)
        {


            int dk1 = Convert.ToInt32(ddllistofdoc.SelectedValue);

            SqlCommand cmdedit = new SqlCommand("Select DocumentMaster.*,DocumentMainType.DocumentMainTypeId,DocumentSubType.DocumentSubTypeId,Party_master.PartyTypeId from DocumentMaster inner join DocumentType on DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId left outer join Party_master on Party_master.PartyID=DocumentMaster.PartyId left outer join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where DocumentMaster.DocumentId='" + dk1 + "'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);

            if (dtedit.Rows.Count > 0)
            {


                lbldocidmaster.Text = dtedit.Rows[0]["DocumentId"].ToString();


                fillmaintaype();
                ddldocmaintype.SelectedIndex = ddldocmaintype.Items.IndexOf(ddldocmaintype.Items.FindByValue(dtedit.Rows[0]["DocumentMainTypeId"].ToString()));
                //fillsubtype();
                //ddldocsubtypename.SelectedIndex = ddldocsubtypename.Items.IndexOf(ddldocsubtypename.Items.FindByValue(dtedit.Rows[0]["DocumentSubTypeId"].ToString()));
                filldll();
                ddldoctypeup.SelectedIndex = ddldoctypeup.Items.IndexOf(ddldoctypeup.Items.FindByValue(dtedit.Rows[0]["DocumentTypeId"].ToString()));

                fillpartytype();
                if (dtedit.Rows[0]["PartyTypeId"].ToString() != null)
                {

                    ddlPartyType.SelectedIndex = ddlPartyType.Items.IndexOf(ddlPartyType.Items.FindByValue(dtedit.Rows[0]["PartyTypeId"].ToString()));
                }

                FillParty();
                if (dtedit.Rows[0]["PartyId"].ToString() != null)
                {

                    ddlpartyname.SelectedIndex = ddlpartyname.Items.IndexOf(ddlpartyname.Items.FindByValue(dtedit.Rows[0]["PartyId"].ToString()));
                }
                txtdoctitle.Text = dtedit.Rows[0]["DocumentTitle"].ToString();

                txtdocrefnmbr.Text = dtedit.Rows[0]["DocumentRefNo"].ToString();
                txtnetamount.Text = dtedit.Rows[0]["DocumentAmount"].ToString();
                lbldocuploaddate.Text = dtedit.Rows[0]["DocumentUploadDate"].ToString();

                if (dtedit.Rows[0]["DocumentDate"].ToString() != "")
                {
                    TxtDocDate.Text = Convert.ToDateTime(dtedit.Rows[0]["DocumentDate"].ToString()).ToShortDateString();
                }
                else
                {
                    TxtDocDate.Text = "";
                }
                pnlupdatedoc.Visible = true;

            }

        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string str1010 = " update  DocumentMaster set DocumentTypeId='" + ddldoctypeup.SelectedValue + "',DocumentTitle='" + txtdoctitle.Text + "',PartyId='" + ddlpartyname.SelectedValue + "' ,DocumentDate='" + TxtDocDate.Text + "',DocumentRefNo='" + txtdocrefnmbr.Text + "' ,DocumentAmount='" + txtnetamount.Text + "' where DocumentId='" + ddllistofdoc.SelectedValue + "' ";

        SqlCommand cmd1010 = new SqlCommand(str1010, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd1010.ExecuteNonQuery();
        con.Close();

        ddldoctype_SelectedIndexChanged(sender, e);
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




        mdloa.Show();
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
    protected void ImageButton5_Click(object sender, EventArgs e)
    {


        string te = "";
        if (ddloa.SelectedIndex == 0)
        {

            te = "cashpaymentnew.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];


        }
        else if (ddloa.SelectedIndex == 1)
        {
            te = "CashReciept.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 2)
        {
            te = "JournalEntryCrDrCompany.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 3)
        {
            te = "CrDrNoteAddByCompany.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 4)
        {
            te = "CrDrNoteAddByCompany.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 5)
        {
            te = "CustomerDeliveryChallan3.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 6)
        {
            te = "PurchaseInvoice.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 7)
        {
            te = "RetailCustomerDeliveryChallan_new.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 8)
        {
            te = "Register_Expense.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void Img2_Click(object sender, EventArgs e)
    {


        string te = "";
        if (ddldo.SelectedIndex == 0)
        {

            te = "CashRegister.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];


        }
        
        else if (ddldo.SelectedIndex == 1)
        {
            te = "LedgerJour_New.aspx?docid=" + ddllistofdoc.SelectedValue;

            hypost.NavigateUrl = "LedgerJour_New.aspx?docid=" + ddllistofdoc.SelectedValue;
        }
        else if (ddldo.SelectedIndex == 2)
        {
            te = "LedgerCRDR.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];


        }
        else if (ddldo.SelectedIndex == 3)
        {
            te = "Register_DeliveryChallan.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedIndex == 4)
        {
            te = "Register_purchase.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedIndex == 5)
        {
            te = "Register_Sales.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedIndex == 6)
        {
            te = "Register_SalesOrder.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedIndex == 7)
        {
            te = "Register_Expense.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

        }
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void gridpopup_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void showentry()
    {
        string scpt = "select Entry_Type_Name,EntryNumber,TranctionMaster.Tranction_Master_Id from AttachmentMaster  inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=AttachmentMaster.RelatedTableId inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId where IfilecabinetDocId='" + ddllistofdoc.SelectedValue + "'";
        SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
        DataTable ds58 = new DataTable();
        adp58.Fill(ds58);



        if (ds58.Rows.Count == 0)
        {

            LinkButton4.Text = "Make Entry";
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
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        ddldoctype_SelectedIndexChanged(sender, e);
    }
    protected void imgAdd2_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PartyMasternouid.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgRefresh2_Click(object sender, ImageClickEventArgs e)
    {
        FillParty();
    }
    protected void imgAdd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentMainType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillmaintaype();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentSubSubType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        filldll();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            Panel1.Visible = true;
        }
        else
        {
            Panel1.Visible = false;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ddldoctype_SelectedIndexChanged(sender, e);
    }
}
