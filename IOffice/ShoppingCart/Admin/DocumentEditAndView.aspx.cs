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
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Diagnostics;
public partial class Account_DocumentEditAndView : System.Web.UI.Page
{
    SqlConnection con; 
    protected int DesignationId;
    protected string DocumentName;
    protected int DocumentID;
    DocumentCls1 clsDocument = new DocumentCls1();
    protected static int jk = 0;
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
			Session["PageUrl"]=strData;
            Session["PageName"] = page;
            Page.Title = pg.getPageTitle(page);
            if (Session["CompanyName"] != null)
            {
                this.Title = Session["CompanyName"] + " IFileCabinet.com - Edit Document";
            }

            Session["PageName"] = "DocumentEditAndView.aspx";

            if (!IsPostBack)
            {
                ViewState["dtrws"] = "1";
                int Docid = Convert.ToInt32(Request.QueryString["id"]);
                hdnDocId.Value = Request.QueryString["id"];
                // int Docid = 145;
                // int DesignationId = Convert.ToInt32(Request.QueryString["Did"]);
                ViewState["docid"] = Docid.ToString();
                flaganddoc();
                if (Convert.ToString(ViewState["docid"]) != "")
                {
                    DataTable dt = new DataTable();
                    string doc = "Select DocumentMainType.Whid FROM DocumentMainType INNER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId INNER JOIN DocumentType ON DocumentSubType.DocumentSubTypeId = DocumentType.DocumentSubTypeId inner join DocumentMaster on DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId where DocumentMainType.CID='" + Session["Comid"] + "' and DocumentMaster.DocumentId='" + ViewState["docid"] + "'";
                    SqlDataAdapter adp = new SqlDataAdapter(doc, con);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        ViewState["Whid"] = Convert.ToString(dt.Rows[0]["Whid"]);
                    }
                    string strf = "SELECT WareHouseId,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";

                    SqlCommand cmd1 = new SqlCommand(strf, con);
                    cmd1.CommandType = CommandType.Text;
                    SqlDataAdapter daf = new SqlDataAdapter(cmd1);
                    DataTable dtf = new DataTable();
                    daf.Fill(dtf);

                    ddlbusiness.DataSource = dtf;
                    ddlbusiness.DataTextField = "Name";
                    ddlbusiness.DataValueField = "WareHouseId";
                    ddlbusiness.DataBind();
                    if (Convert.ToString(ViewState["Whid"]) != "")
                    {
                        ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(ViewState["Whid"].ToString()));
                    }
                    ddlbusiness_SelectedIndexChanged(sender, e);


                    LoadPdf(Docid);
                    FillDocumentTypeAll();
                    // FillDocumentMainType();
                    FillParty();
                    FillDocDetail(sender, e);
                    //txtDate.Text = System.DateTime.Now.ToShortDateString();
                }
            }
            
            DocumentName = ViewState["docname"].ToString();
            DocumentID = Convert.ToInt32(ViewState["docId"]);
            DesignationId = Convert.ToInt32(Session["DesignationId"]);


            pnlmsg.Visible = false;
       
      

    }
    protected void flaganddoc()
    {

        DataTable dts1 = select("select Id,name from DocumentTypenm where  active='1' Order by name");
        ddldt.DataSource = dts1;
        ddldt.DataTextField = "name";
        ddldt.DataValueField = "Id";
        ddldt.DataBind();
        //ddldt.Items.Insert(0, "Select");
        //ddldt.Items[0].Value = "0";
    }
    protected DataTable select(string std)
    {
        SqlDataAdapter cidco = new SqlDataAdapter(std, con);
        DataTable dts = new DataTable();
        cidco.Fill(dts);
        return dts;
    }
    protected void FillDocDetail(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        int Docid = Convert.ToInt32(hdnDocId.Value);
        dt = clsDocument.SelectDoucmentMasterByID(Docid);
        lblUploadDate.Text  = Convert.ToDateTime(dt.Rows[0]["DocumentUploadDate"]).ToShortDateString();
        txtdoctitle.Text = dt.Rows[0]["DocumentTitle"].ToString();
        txtdocrefnmbr.Text = dt.Rows[0]["DocumentRefNo"].ToString();
        if (txtdoctitle.Text == "")
        {
            txtdoctitle.Text = "Untitled";
        }
        txtdocdscrptn.Text = dt.Rows[0]["Description"].ToString();
        //=============Fill All Combo=========================
        DataTable dt1 = new DataTable();
        if (Convert.ToString(dt.Rows[0]["DocumentTypeId"]) != "0")
        {
            
           

            ddldoctype.SelectedValue = dt.Rows[0]["DocumentTypeId"].ToString();
        }
        else
        {
            ddldoctype.SelectedIndex = 0;
            //ddlmaindoctype.SelectedIndex = 0;29-4-11
            //ddlsubdoctype.SelectedIndex = 0;
        }
        if (dt.Rows[0]["PartyId"] != System.DBNull.Value)
        {
            ddlpartyname.SelectedValue = dt.Rows[0]["PartyId"].ToString();
        }
        else
        {
            ddlpartyname.SelectedValue = "0";
        }
        ddldt.SelectedIndex = ddldt.Items.IndexOf(ddldt.Items.FindByValue(Convert.ToString(dt.Rows[0]["DocumentTypenmId"])));
        ddldt_SelectedIndexChanged(sender, e);
        txtnetamount.Text = dt.Rows[0]["DocumentAmount"].ToString();
        if (txtnetamount.Text == "")
        {
            txtnetamount.Text = "0.0";
        }
        txtpartdocrefno.Text=Convert.ToString(dt.Rows[0]["PartyDocrefno"]);
        ViewState["docname"] = dt.Rows[0]["DocumentName"].ToString();
        ViewState["docId"] = Convert.ToInt32(dt.Rows[0]["DocumentId"]);
        hdnDocId.Value = dt.Rows[0]["DocumentId"].ToString();
        DocumentName = ViewState["docname"].ToString();
        DocumentID = Convert.ToInt32(ViewState["docId"]);
       

        txtDate.Text = Convert.ToDateTime(dt.Rows[0]["DocumentDate"]).ToShortDateString();
       
        string pidn = "";
        DataTable dtr=select("select * from Party_master where PartyID='"+ddlpartyname.SelectedValue+"'");
        if (dtr.Rows.Count > 0)
        {
            pidn = " of " + Convert.ToString(dtr.Rows[0]["Compname"]);
        }
        else
        {
                
        }
        txtdoctitle.Text = ddldt.SelectedItem.Text + pidn + " dated " + txtDate.Text + " for $ " + txtnetamount.Text;
    }
    protected void LoadPdf(int Docid)
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDoucmentMasterByID(Docid);
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
           
          DataTable dt2 = new DataTable();
                dt2 = clsDocument.SelectDoucmentImageMaster(Docid);
                if (dt2.Rows.Count > 0)
                {
                    for (int kk = 1; kk <= dt2.Rows.Count; kk++)
                    {
                        DataRow drow = dt1.NewRow();
                        drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + dt2.Rows[kk - 1]["DocumentImgName"].ToString();
                        dt1.Rows.Add(drow);
                    }
                }
        //direct doc fill
              //  string Location = Server.MapPath("~//Account//" + Session["comid"] + "//DocumentImage//");

              //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
              
              //foreach (System.IO.FileInfo f in dir.GetFiles(docnameIn.ToString() + "*.*"))
              //{

              //    DataRow drow = dt1.NewRow();
              //    drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + f.Name.ToString();
              //    dt1.Rows.Add(drow);


              //}
              DataList1.DataSource = dt1;
              DataList1.DataBind();
          
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
        catch(Exception ecx)
        {
        }
        return true;
    }
    protected void imgbtnupdate_Click(object sender, EventArgs e)
    {
       
           //  Response.Write( ViewState["docId"] + " :: " + Convert.ToDecimal(txtnetamount.Text).ToString());
       
            Int32 rst = clsDocument.UpdateDocumentMaster(Convert.ToInt32(ViewState["docId"]), Convert.ToInt32(ddldoctype.SelectedValue), 2, Convert.ToDateTime(lblUploadDate.Text), ViewState["decname"].ToString(), txtdoctitle.Text, txtdocdscrptn.Text, Convert.ToInt32(ddlpartyname.SelectedValue), txtdocrefnmbr.Text, Convert.ToDecimal(txtnetamount.Text), Convert.ToDateTime(txtDate.Text), ddldt.SelectedValue, txtpartdocrefno.Text);
            int rst1 = clsDocument.InsertDocumentLog(Convert.ToInt32(ViewState["docId"]), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, false, false, true, false, false, false, false);
            if (rst > 0)
            {
                bool fnws = clsDocument.UpdateDocumentDateDetail(Convert.ToInt32(ViewState["docId"]), Convert.ToDateTime(txtDate.Text));
                int rst111 = clsDocument.InsertDocumentLog(Convert.ToInt32(ViewState["docId"]), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, false, false, true, false, false, false, false);
                pnlmsg.Visible = true;
                lblmsg.Text = "Document Updated Successfully.";
                lblmsg.Visible = true;
                fillen(true);
                btncon.Visible = true;
                imgbtnupdate.Visible = false;
            }
        
    }

 
    protected void FillDocumentTypeAll()
    {
        DocumentCls1 clsDocument = new DocumentCls1();
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocTypeAll(ddlbusiness.SelectedValue);
        ddldoctype.DataSource = dt;
        ddldoctype.DataBind();
        ddldoctype.Items.Insert(0, "--Select--");
        ddldoctype.Items[0].Value = "0";
    }
    protected void FillParty()
    {

        DataTable dt = new DataTable();
        dt = clsDocument.selectparty(ddlbusiness.SelectedValue);
        ddlpartyname.DataSource = dt;
        ddlpartyname.DataTextField = "PartyName";
        ddlpartyname.DataValueField = "PartyId";
        ddlpartyname.DataBind();
        ddlpartyname.Items.Insert(0, "-select-");
        ddlpartyname.Items[0].Value = "0";
        //DataTable dt = new DataTable();
        //dt = clsDocument.SelectPartyidAll();
        //ddlpartyname.DataSource = dt;
        //ddlpartyname.DataTextField = "PartyName";
        //ddlpartyname.DataValueField = "PartyId";
        //ddlpartyname.DataBind();
        //ddlpartyname.Items.Insert(0, "-select-");
        //ddlpartyname.SelectedItem.Value = "0";
    }
  
    protected void ibtnFirst_Click(object sender, ImageClickEventArgs e)
    {
        Int32 DocId ;
        IbtnNext.Enabled = false;
        DocId = 0;
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentforApprovalFirst();
        if (dt.Rows.Count > 0)
        {
          DocId = Convert.ToInt32( dt.Rows[0]["DocumentId"].ToString());
          hdnDocId.Value = DocId.ToString();
        }
        if (DocId != 0)
        {
            LoadPdf(DocId);
            FillDocDetail(sender, e);
        }
        ibtnFirst.Enabled = false ;
        IbtnNext.Enabled = true;
        IbtnPrev.Enabled = false ;
        IbtnLast.Enabled = true;
    }
    protected void IbtnPrev_Click(object sender, ImageClickEventArgs e)
    {
        Int32 DocId;
        DocId = 0;
        DataTable dt = new DataTable();
        //dt = clsDocument.SelectDocumentforApprovalPrev  (Convert.ToInt32(hdnDocId.Value));
        dt = clsDocument.SelectDocumentforApproval(hdnDocId.Value, "True");
        
        if (dt.Rows.Count > 0)
        {
            
            if ((jk > 0) && (jk <= dt.Rows.Count))
            {
                jk = jk - 1;
                DocId = Convert.ToInt32(dt.Rows[jk]["DocumentId"].ToString());
                hdnDocId.Value = DocId.ToString();
            }
        }
        if (DocId != 0)
        {
            LoadPdf(DocId);
            FillDocDetail(sender, e);
        }
        ibtnFirst.Enabled = true;
        IbtnNext.Enabled = true;
        IbtnPrev.Enabled = true;
        IbtnLast.Enabled = true;
    }
    protected void IbtnNext_Click(object sender, ImageClickEventArgs e)
    {
        
        Int32 DocId;
        DocId = 0;
        DataTable dt = new DataTable();
        //dt = clsDocument.SelectDocumentforApprovalNext (Convert.ToInt32(hdnDocId.Value));

        dt = clsDocument.SelectDocumentforApproval(hdnDocId.Value, "True");
        if (dt.Rows.Count > 0)
        {
            if (jk < dt.Rows.Count && jk>=0)
            {
                DocId = Convert.ToInt32(dt.Rows[jk]["DocumentId"].ToString());
                hdnDocId.Value = DocId.ToString();
                jk = jk + 1;
            }
            
        }
        if (DocId != 0)
        {
            LoadPdf(DocId);
            FillDocDetail(sender, e);
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
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentforApprovalLast ();
        if (dt.Rows.Count > 0)
        {
            DocId = Convert.ToInt32(dt.Rows[0]["DocumentId"].ToString());
            hdnDocId.Value = DocId.ToString();
        }
        if (DocId != 0)
        {
            LoadPdf(DocId);
            FillDocDetail(sender, e);
        }
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentTypeAll();
        
        FillParty();
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
    protected void ImageButton50_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PartyMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton51_Click(object sender, ImageClickEventArgs e)
    {
        FillParty();
    }
  
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        flaganddoc();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }

    protected void btncon_Click(object sender, EventArgs e)
    {
        string pidn = "";
        if (txtdoctitle.Text.Length <= 0)
        {
            DataTable dtr = select("select * from Party_master where PartyID='" + ddlpartyname.SelectedValue + "'");
            if (dtr.Rows.Count > 0)
            {
                pidn = " of " + Convert.ToString(dtr.Rows[0]["Compname"]);
            }
            else
            {

            }
            txtdoctitle.Text = ddldt.SelectedItem.Text + pidn + " dated " + txtDate.Text + " for $ " + txtnetamount.Text;
        }
          int flagpd = 0;
        lblmsg.Text = "";
        //if (ddldt.SelectedItem.Text == "Credit Invoice" || ddldt.SelectedItem.Text == "Cash Invoice" || ddldt.SelectedItem.Text == "Cash Voucher" || ddldt.SelectedItem.Text == "Credit Voucher")
        //{
        if (txtpartdocrefno.Text.Length > 0)
        {
            DataTable dtsc = select("select PartyDocrefno from DocumentMaster where  DocumentMaster.DocumentId<>'" + ViewState["docId"] + "' and CID='" + Session["Comid"] + "' and PartyDocrefno='" + txtpartdocrefno.Text + "'");
            if (dtsc.Rows.Count == 0)
            {
            }
            else
            {
                flagpd = 1;
                lblmsg.Text = "Please enter unique party document reference number.";
            }
        }

        if (flagpd == 0)
        {
            imgbtnupdate.Visible = true;
            btncon.Visible = false;
            fillen(false);
        }
    }
    protected void fillen(bool allk)
    {
        ddlbusiness.Enabled = allk;
        ddldt.Enabled = allk;
        ddlpartyname.Enabled = allk;
        ddldoctype.Enabled = allk;
        txtpartdocrefno.Enabled = allk;
        txtnetamount.Enabled = allk;
        txtdoctitle.Enabled = allk;
        txtdocrefnmbr.Enabled = allk;
        txtdocdscrptn.Enabled = allk;
        txtDate.Enabled = allk;
        imgbtncal.Enabled = allk;
    }
    protected void ddldt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldt.SelectedItem.Text == "Credit Invoice" || ddldt.SelectedItem.Text == "Cash Invoice" || ddldt.SelectedItem.Text == "Cash Voucher" || ddldt.SelectedItem.Text == "Credit Voucher")
        {
            RequicmnredFieldValidator2.Visible = true;
        }
        else
        {
            RequicmnredFieldValidator2.Visible = false;
        }
        string pidn = "";
        
            DataTable dtr = select("select * from Party_master where PartyID='" + ddlpartyname.SelectedValue + "'");
            if (dtr.Rows.Count > 0)
            {
                pidn = " of " + Convert.ToString(dtr.Rows[0]["Compname"]);
            }
            else
            {

            }
            txtdoctitle.Text = ddldt.SelectedItem.Text + pidn + " dated " + txtDate.Text + " for $ " + txtnetamount.Text;
       
    }
}
