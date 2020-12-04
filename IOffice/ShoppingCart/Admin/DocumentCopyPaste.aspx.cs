using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
public partial class Account_DocumentCopyPaste : System.Web.UI.Page
{
    SqlConnection con; 
    protected int DesignationId;
    protected string DocumentName;
    protected int DocumentID;
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
           // Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
            pagetitleclass pg = new pagetitleclass();
            string strData = Request.Url.ToString();

            char[] separator = new char[] { '/' };

            string[] strSplitArr = strData.Split(separator);
            int i = Convert.ToInt32(strSplitArr.Length);
            string page = strSplitArr[i - 1].ToString();
			Session["PageUrl"]=strData;	
            Session["PageName"] = page;
            Page.Title = pg.getPageTitle(page);
            if (Session["CompanyName"] != null)
            {
                this.Title = Session["CompanyName"] + " IFileCabinet.com - Edit Document";
            }

            Session["PageName"] = "DocumentCopyPaste.aspx";

            if (!IsPostBack)
            {
                Pagecontrol.dypcontrol(Page, page);
                int Docid = Convert.ToInt32(Request.QueryString["id"]);
                hdnDocId.Value = Request.QueryString["id"];
                // int Docid = 145;
                // int DesignationId = Convert.ToInt32(Request.QueryString["Did"]);
                ViewState["docid"] = Docid.ToString();
               if(Convert.ToString( ViewState["docid"])!="")
               {
                   DataTable dt = new DataTable();
                   string doc = "Select DocumentMainType.Whid FROM DocumentMainType INNER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId INNER JOIN DocumentType ON DocumentSubType.DocumentSubTypeId = DocumentType.DocumentSubTypeId inner join DocumentMaster on DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId where DocumentMainType.CID='"+Session["Comid"]+"' and DocumentMaster.DocumentId='" + ViewState["docid"] + "'";
                    SqlDataAdapter adp = new SqlDataAdapter(doc, con);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        ViewState["Whid"] = Convert.ToString(dt.Rows[0]["Whid"]);
                    }
        
               }
                LoadPdf(Docid);

                FillDocumentMainType();
             
                FillDocDetail(sender, e);

            }
           
            DocumentName = ViewState["docname"].ToString();
            DocumentID = Convert.ToInt32(ViewState["docId"]);
            DesignationId = Convert.ToInt32(Session["DesignationId"]);


          
       
    }
    protected void FillDocDetail(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            int Docid = Convert.ToInt32(hdnDocId.Value);
            dt = clsDocument.SelectDoucmentMasterByID(Docid);
        }
        catch (Exception ex)
        {
        } 
        
        if (dt.Rows.Count > 0)
        {

        
            //lblUploadDate.Text = Convert.ToDateTime(dt.Rows[0]["DocumentUploadDate"]).ToShortDateString();
            //txtdoctitle.Text = dt.Rows[0]["DocumentTitle"].ToString();
            //txtdocrefnmbr.Text = dt.Rows[0]["DocumentRefNo"].ToString();
            //txtdocdscrptn.Text = dt.Rows[0]["Description"].ToString();
            //=============Fill All Combo=========================
            DataTable dt1 = new DataTable();
            if (Convert.ToString(dt.Rows[0]["DocumentTypeId"]) != "0")
            {
                dt1 = clsDocument.SelectDocumentMainTypeByType(Convert.ToInt32(dt.Rows[0]["DocumentTypeId"]));
                ddlmaindoctype.SelectedValue = dt1.Rows[0]["DocumentMainTypeId"].ToString();
                ddlmaindoclass_SelectedIndexChanged(sender, e);
                ddlsubdoctype.SelectedValue = dt1.Rows[0]["DocumentSubTypeId"].ToString();
                ddlsubdoctype_SelectedIndexChanged(sender, e);

                ddldoctype.SelectedValue = dt.Rows[0]["DocumentTypeId"].ToString();
            }
            else
            {
                ddldoctype.SelectedIndex = 0;
                ddlmaindoctype.SelectedIndex = 0;
                ddlsubdoctype.SelectedIndex = 0;
            }
            //ddlpartyname.SelectedValue = dt.Rows[0]["PartyId"].ToString();
            //txtnetamount.Text = dt.Rows[0]["DocumentAmount"].ToString();
            ViewState["docname"] = dt.Rows[0]["DocumentName"].ToString();
            ViewState["docId"] = Convert.ToInt32(dt.Rows[0]["DocumentId"]);
            hdnDocId.Value = dt.Rows[0]["DocumentId"].ToString();
            DocumentName = ViewState["docname"].ToString();
            DocumentID = Convert.ToInt32(ViewState["docId"]);
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
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDoucmentMasterByID(Docid);
       
        if (dt.Rows.Count > 0)
        {
            string docname = dt.Rows[0]["DocumentName"].ToString();
            ViewState["decname"] = docname.ToString();
            string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);
            string strft = "Select FileStorage.* from FileStorage Where B='" + ClsEncDesc.EncDyn(Session["comid"].ToString()) + "' and H='" + ClsEncDesc.EncDyn("True") + "'";

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
            string Location = Server.MapPath("~//Account//" + Session["comid"] + "//DocumentImage//");
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);

            foreach (System.IO.FileInfo f in dir.GetFiles(docnameIn.ToString() + "*.*"))
            {

                DataRow drow = dt1.NewRow();
                drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + f.Name.ToString();
                dt1.Rows.Add(drow);


            }
            DataList1.DataSource = dt1;
            DataList1.DataBind();
            DataTable dtfill = (DataTable)Session["grdcopy"];
            GridView2.DataSource = dtfill;
            GridView2.DataBind();
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
  
    //protected void LoadPdf(int Docid)
    //{
    //    DataTable dt = new DataTable();
    //    dt = clsDocument.SelectDoucmentMasterByID(Docid);
    //    string docname = dt.Rows[0]["DocumentName"].ToString();
    //    ViewState["decname"] = docname.ToString();
    //    string flext="";
    //    flext=dt.Rows[0]["FileExtensionType"].ToString();
    //    ViewState["fileext"] = flext.ToString();
    //    //lblTitle.Text = dt.Rows[0]["DocumentTitle"].ToString();
    //    string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);
    //    int length = docname.Length;
    //    string docnameIn = docname.Substring(0, length - 4);

    //    //grid.DataSource = dt;
    //    //grid.DataBind();
    //    ViewState["path"] = filepath.ToString();
    //    DataTable dt1 = new DataTable();
    //    DataColumn dcom = new DataColumn();
    //    dcom.ColumnName = "image";
    //    dcom.DataType = System.Type.GetType("System.String");
    //    dt1.Columns.Add(dcom);

    //    string Location = Server.MapPath("~//Account//" + Session["comid"] + "//DocumentImage//");
    //    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
    //    foreach (System.IO.FileInfo f in dir.GetFiles(docnameIn.ToString() + "*.*"))
    //    {
    //        //string Location1 = Server.MapPath("~//Account//DocumentImage//");

    //        bool success = false;


    //        DataRow drow = dt1.NewRow();
    //        drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + f.Name.ToString();
    //        dt1.Rows.Add(drow);


    //    }
    //    DataList1.DataSource = dt1;
    //    DataList1.DataBind();

    //}
    protected void ddlmaindoclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentSubTypeMainMethod();
    }

    protected void ddlsubdoctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentTypeMainMethod();

       
    }
    protected void imgbtnupdate_Click(object sender, EventArgs e)
    {

        if (ddlcopyormove.SelectedValue == "0")
        {

            foreach (GridViewRow grd in GridView2.Rows)
            {
                string doc = "SELECT * FROM  DocumentMaster   where DocumentId='" + grd.Cells[0].Text + "'";
                SqlDataAdapter adp = new SqlDataAdapter(doc, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    bool insdata;
                    insdata = true;
                    Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(ddldoctype.SelectedValue), 2, Convert.ToDateTime(dt.Rows[0]["DocumentUploadDate"]), Convert.ToString(dt.Rows[0]["DocumentName"]), Convert.ToString(dt.Rows[0]["DocumentTitle"]), Convert.ToString(dt.Rows[0]["Description"]), Convert.ToInt32(dt.Rows[0]["PartyId"]), Convert.ToString(dt.Rows[0]["DocumentRefNo"]), Convert.ToDecimal(dt.Rows[0]["DocumentAmount"]), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(dt.Rows[0]["DocumentDate"]), Convert.ToString(ViewState["fileext"]), Convert.ToString(dt.Rows[0]["DocumentTypenmId"]), Convert.ToString(dt.Rows[0]["PartyDocrefno"]));
                    if (rst > 0)
                    {
                        //bool dcaprv = true;
                        //bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);
                        int accesslevel = 0;

                        string strdesig = " select DesignationMaster.* from EmployeeMaster inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID where EmployeeMaster.EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "'  ";
                        SqlCommand cmdeeed = new SqlCommand(strdesig, con);
                        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                        DataTable dteeed = new DataTable();
                        adpeeed.Fill(dteeed);

                        if (dteeed.Rows.Count > 0)
                        {
                            ViewState["DesignationName"] = dteeed.Rows[0]["DesignationName"].ToString();
                        }
                        if (ViewState["DesignationName"].ToString() == "Manager")
                        {
                            accesslevel = 3;
                        }
                        else if (ViewState["DesignationName"].ToString() == "Supervisor")
                        {
                            accesslevel = 2;
                        }
                        else if (ViewState["DesignationName"].ToString() == "Office Clerk")
                        {
                            accesslevel = 1;
                        }
                        else
                        {
                            accesslevel = 0;
                        }

                        string eeed = " Select  * from  DocumentProcessing where  EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' and DocumentId='" + rst + "'";
                        SqlCommand cmdeeedc = new SqlCommand(eeed, con);
                        SqlDataAdapter adpeeedc = new SqlDataAdapter(cmdeeedc);
                        DataTable dteeedc = new DataTable();
                        adpeeedc.Fill(dteeedc);
                        if (dteeedc.Rows.Count <= 0)
                        {
                            string str1 = " INSERT INTO DocumentProcessing  (DocumentId ,EmployeeId,DocAllocateDate,CID,StatusId,Levelofaccess) VALUES  ('" + rst + "' ,'" + Convert.ToInt32(Session["EmployeeId"]) + "','" + System.DateTime.Now.ToShortDateString() + "','" + Session["Comid"].ToString() + "','0','" + accesslevel + "') ";
                            SqlCommand cmd1 = new SqlCommand(str1, con);
                            con.Open();
                            insdata = Convert.ToBoolean(cmd1.ExecuteNonQuery());
                            con.Close();
                        }
                        bool fnws = clsDocument.UpdateDocumentDateDetail(rst, Convert.ToDateTime(dt.Rows[0]["DocumentDate"]));
                        int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                        lblmsg.Text = "Document copied successfully";
                        lblmsg.Visible = true;

                    }
                }
            }
        }
        else if (ddlcopyormove.SelectedValue == "1")
        {
            foreach (GridViewRow grd in GridView2.Rows)
            {
                string doc = "SELECT * FROM  DocumentMaster   where DocumentId='" + grd.Cells[0].Text + "'";
                SqlDataAdapter adp = new SqlDataAdapter(doc, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    string sr51 = "update DocumentMaster  set DocumentTypeId='" + ddldoctype.SelectedValue + "' where DocumentId='" + dt.Rows[0]["DocumentId"].ToString() + "'  ";
                    SqlCommand cmd801 = new SqlCommand(sr51, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd801.ExecuteNonQuery();
                    con.Close();


                    lblmsg.Text = "Document moved successfully";
                    lblmsg.Visible = true;


                }
            }

        }
        else
        {
        }
    }

    protected void FillDocumentMainType()
    {
        DataTable dt = new DataTable();
       // dt = clsDocument.SelectDocumentMainType();

        string doc = "SELECT DocumentMainTypeId,DocumentMainType as DocumentMainType FROM  [dbo].[DocumentMainType]   where CID='" + Session["comid"] + "' and Whid='" + ViewState["Whid"] + "'";
        SqlDataAdapter adp = new SqlDataAdapter(doc, con);
        adp.Fill(dt);
        
         ddlmaindoctype.DataSource = dt;
        ddlmaindoctype.DataTextField = "DocumentMainType";
        ddlmaindoctype.DataValueField = "DocumentMainTypeId";
        ddlmaindoctype.DataBind();
        //ddlmaindoctype.Items.Insert(0, "-select-");
        //ddlmaindoctype.SelectedItem.Value = "0";
    }
  
    protected void FillDocumentSubTypeMainMethod()
    {
        
            ddlsubdoctype.Items.Clear();
            ddldoctype.Items.Clear();
      
            FillDocumentSubTypeWithMainType(Int32.Parse(ddlmaindoctype.SelectedValue.ToString()));
            FillDocumentTypeWithSubType(Convert.ToInt32(ddlsubdoctype.SelectedValue));

    }

    protected void FillDocumentSubTypeWithMainType(Int32 DocumentMainTypeId)
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentSubTypeWithMainType(DocumentMainTypeId);
        ddlsubdoctype.DataSource = dt;
        ddlsubdoctype.DataTextField = "DocumentSubType";
        ddlsubdoctype.DataValueField = "DocumentSubTypeId";
        ddlsubdoctype.DataBind();
        //ddlsubdoctype.Items.Insert(0, "-select-");
        //ddlsubdoctype.SelectedItem.Value = "0";
        ddldoctype.Items.Clear();

    }

    protected void FillDocumentTypeMainMethod()
    {
        
            ddldoctype.Items.Clear();
       
            FillDocumentTypeWithSubType(Int32.Parse(ddlsubdoctype.SelectedValue.ToString()));
       

    }

    protected void FillDocumentTypeWithSubType(Int32 DocumentSubTypeId)
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentTypeWithSubType(DocumentSubTypeId);
        ddldoctype.DataSource = dt;
        ddldoctype.DataTextField = "DocumentType";
        ddldoctype.DataValueField = "DocumentTypeId";
        ddldoctype.DataBind();
        //ddldoctype.Items.Insert(0, "-select-");
        //ddldoctype.SelectedItem.Value = "0";
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
}
