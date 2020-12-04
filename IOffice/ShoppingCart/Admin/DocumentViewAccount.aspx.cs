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
public partial class DocumentViewAccount : System.Web.UI.Page
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
            compid = Session["comid"].ToString();

            flaganddoc();
            ddldt_SelectedIndexChanged(sender, e);
            fillstore();
            fillpartytype();
            FillParty();
            fillmaintaype();
            filldll();
            FillDocumentTypeAll();
            ddldoctype_SelectedIndexChanged(sender, e); 

           
           
        }
        
        DesignationId = Convert.ToInt32(Session["DesignationId"]);

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
                //string strft = "Select FileStorage.* from FileStorage Where B='" + ClsEncDesc.EncDyn(Session["comid"].ToString()) + "' and H='" + ClsEncDesc.EncDyn("True") + "'";

                //SqlCommand cmdft = new SqlCommand(strft, con);
                //SqlDataAdapter adpft = new SqlDataAdapter(cmdft);
                //DataTable dtft = new DataTable();
                //adpft.Fill(dtft);

                //if (dtft.Rows.Count > 0)
                //{
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
                //}
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
                        //if (Convert.ToString(Session["no"]) == Convert.ToString(Session["no1"]))
                        //{
                        //    int no = Convert.ToInt32(Session["no"]);
                        //    sav = no;
                        //    drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + dt2.Rows[no - 1]["DocumentImgName"].ToString();
                        //    Session["no1"] = Convert.ToInt32(Session["no1"]) + 1;
                        //    dt1.Rows.Add(drow);
                        //    DataRow drow1 = dt1.NewRow();
                        //    if (no != 1)
                        //    {
                        //        drow1["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + dt2.Rows[kk - 1]["DocumentImgName"].ToString();
                        //        dt1.Rows.Add(drow1);
                        //    }
                        //}
                        //else
                        //{
                        //    if (sav == kk)
                        //    {
                        //    }
                        //    else
                        //    {
                                drow["image"] = "~/Account/" + Session["comid"] + "/DocumentImage/" + dt2.Rows[kk - 1]["DocumentImgName"].ToString();
                                dt1.Rows.Add(drow);
                        //    }
                        //}


                       
                        lblnototal.Text = dt2.Rows.Count.ToString();
                    }
                }
                else
                {
                   
                   
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
      //  DocumentCls1 clsDocument = new DocumentCls1();
        DataTable dt = new DataTable();
        string str132 = "select Distinct  DocumentType.DocumentTypeId, Left(DocumentMainType.DocumentMainType,25)+' - '+ Left(DocumentSubType.DocumentSubType,25)+' - '+ Left(DocumentType.DocumentType,25) as doctype from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentType.DocumentTypeId In( SELECT  Distinct  DocumentTypeId FROM  DocumentAccessRightMaster WHERE     DesignationId ='" + Session["DesignationId"] + "' and (CID='" + Session["Comid"].ToString() + "' ) and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')  or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))) and  DocumentMainType.CID='" + Session["Comid"].ToString() + "' and DocumentMainType.Whid='" + ddlWarehouse.SelectedValue + "' order by doctype ";
        SqlCommand cmd = new SqlCommand(str132, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        //dt = clsDocument.SelectDocTypeAll(ddlWarehouse.SelectedValue);
        ddldoctype.DataSource = dt;
        ddldoctype.DataBind();

    }

    protected void ddldoctype_SelectedIndexChanged(object sender, EventArgs e)
    {
       // fillen(true);
       // btncon.Visible = true;
       // Button1.Visible = false;
        fillddllistofdocument();
        if (ddllistofdoc.SelectedIndex > -1)
        {
            defaultviewswitch();

            LoadPdf(Convert.ToInt32(ddllistofdoc.SelectedValue));
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
        if (ddllistofdoc.Items.Count == 0)
        {
            ddllistofdoc.Items.Insert(0, "0 Document");
            ddllistofdoc.Items[0].Value = "0";
        }
        showentry();

    }
    protected void ddllistofdoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllistofdoc.SelectedIndex > -1)
        {
            defaultviewswitch();
            LoadPdf(Convert.ToInt32(ddllistofdoc.SelectedValue));
           
        }
    }
    

    protected DataTable listofdoc()
    {
        string toprecord = "";
        if (DropDownList1.SelectedValue != "All")
        {
            toprecord = "Top  " + DropDownList1.SelectedValue;
        }
        string strbydoctype = " and DocumentMaster.DocumentTypeId IN( SELECT  Distinct  DocumentAccessRightMaster.DocumentTypeId FROM  DocumentMainType inner join     DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentAccessRightMaster on DocumentAccessRightMaster.DocumentTypeId=DocumentType.DocumentTypeId  WHERE   DocumentMainType.Whid='" + ddlWarehouse.SelectedValue + "' and  DesignationId ='" + Session["DesignationId"] + "' and (DocumentAccessRightMaster.CID='" + Session["Comid"] + "') and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')   or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true')))";

        string strcat = "SELECT " + toprecord + "  DocumentMaster.DocumentId,Convert(Nvarchar(50),DocumentMaster.DocumentId) + ':' +DocumentMaster.DocumentTitle as DocumentTitle  FROM   DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId LEFT OUTER JOIN DocumentProcessing ON DocumentMaster.DocumentId = DocumentProcessing.DocumentId LEFT OUTER JOIN Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId  WHERE  DocumentMainType.Whid='" + ddlWarehouse.SelectedValue + "' and DocumentType.DocumentTypeId='" + ddldoctype.SelectedValue + "' AND DocumentMaster.DocumentId  in ( SELECT  distinct   DocumentId FROM         DocumentProcessing WHERE     (Approve = 1) ) AND DocumentMaster.DocumentId not in ( SELECT distinct    DocumentId FROM         DocumentProcessing WHERE     (Approve = 0) or (Approve is null) )  and(DocumentMaster.CID='" + Session["Comid"] + "')  ";
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

        string orderby = " order by DocumentUploadDate desc ";

        string finalstr = strcat + strtypeid + strbydate + strbydoctype + orderby;

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

        filldll();

    }

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
                filldll();
                ddldoctypeup.SelectedIndex = ddldoctypeup.Items.IndexOf(ddldoctypeup.Items.FindByValue(dtedit.Rows[0]["DocumentTypeId"].ToString()));

                fillpartytype();
                if (dtedit.Rows[0]["PartyTypeId"].ToString() != null)
                {

                    ddlPartyType.SelectedIndex = ddlPartyType.Items.IndexOf(ddlPartyType.Items.FindByValue(dtedit.Rows[0]["PartyTypeId"].ToString()));
                }

                FillParty();
                ddldt.SelectedIndex = ddldt.Items.IndexOf(ddldt.Items.FindByValue(Convert.ToString(dtedit.Rows[0]["DocumentTypenmId"])));
                EventArgs e = new EventArgs();
                object sender = new object();
                ddldt_SelectedIndexChanged(sender, e);
                txtnetamount.Text = dtedit.Rows[0]["DocumentAmount"].ToString();
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
                string pidn = "";
                DataTable dtr = select("select * from Party_master where PartyID='" + ddlpartyname.SelectedValue + "'");
                if (dtr.Rows.Count > 0)
                {
                    pidn = " of " + Convert.ToString(dtr.Rows[0]["Compname"]);
                }
                else
                {

                }
                txtdoctitle.Text = ddldt.SelectedItem.Text + pidn + " dated " + TxtDocDate.Text + " for $ " + txtnetamount.Text;

            }

        }

    }

    protected void Button1_Click(object sender, EventArgs e)
     { string pidn = "";
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
         txtdoctitle.Text = ddldt.SelectedItem.Text + pidn + " dated " + TxtDocDate.Text + " for $ " + txtnetamount.Text;
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
            string str1010 = " update  DocumentMaster set DocumentTypenmId='" + ddldt.SelectedValue + "',PartyDocrefno='" + txtpartdocrefno.Text + "', DocumentTypeId='" + ddldoctypeup.SelectedValue + "',DocumentTitle='" + txtdoctitle.Text + "',PartyId='" + ddlpartyname.SelectedValue + "' ,DocumentDate='" + TxtDocDate.Text + "',DocumentRefNo='" + txtdocrefnmbr.Text + "' ,DocumentAmount='" + txtnetamount.Text + "' where DocumentId='" + ddllistofdoc.SelectedValue + "' ";

            SqlCommand cmd1010 = new SqlCommand(str1010, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1010.ExecuteNonQuery();
            con.Close();
            lblmsg.Text = "Record updated successfully";
            ddldoctype_SelectedIndexChanged(sender, e);
        }
    }
    protected DataTable select(String str)
    {
        DataTable dt = new DataTable();
        SqlCommand cmdeeed = new SqlCommand(str, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        return dteeed;
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        int tid = Convert.ToInt32(LinkButton6.CommandArgument);

        string st = "Select EntryTypeId, EntryTypeMaster.Entry_Type_Name,TranctionMaster.EntryNumber,AttachmentMaster.Datetime From AttachmentMaster inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=AttachmentMaster.RelatedTableId inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId where TranctionMaster.Tranction_Master_Id='" + tid + "' ";

        SqlCommand cm = new SqlCommand(st, con);
        SqlDataAdapter ad = new SqlDataAdapter(cm);
        DataTable d = new DataTable();
        ad.Fill(d);
        if (d.Rows.Count > 0)
        {
            string te = "";
            if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "33")
            {
                te = "CashRegister.aspx?n12&trid=" + tid + "&eid=33";

            }
            else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "32")
            {
                te = "CashRegister.aspx?n12&trid=" + tid + "&eid=32";

            }
            else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "2")
            {
                te = "CashReciept.aspx?n12&trid=" + tid + "&eid=2";

            }
            else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "1")
            {
                te = "CashPaymentnew.aspx?n12&trid=" + tid + "&eid=1";

            }
            else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "6" || Convert.ToString(d.Rows[0]["EntryTypeId"]) == "7")
            {
                te = "CrDrNoteAddByCompany.aspx?n12&EntryN=" + Convert.ToString(d.Rows[0]["EntryNumber"]) + "&EntryT=" + Convert.ToString(d.Rows[0]["EntryTypeId"]);

            }
            else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "3")
            {
                te = "JournalEntryCrDrCompany.aspx?n12&Tid=" + tid.ToString();

            }
            else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "27")
            {
                DataTable dsrc = select(" SELECT  distinct Purchase_Details_Id,  TranctionMaster.Tranction_Master_Id, TranctionMaster.EntryNumber " +
                      " FROM PurchaseDetails INNER JOIN  TranctionMaster ON PurchaseDetails.TransId = TranctionMaster.Tranction_Master_Id " +
                 " where PurchaseDetails.TransId = '" + tid + "'  and EntryTypeId='27'");
                if (dsrc.Rows.Count > 0)
                {
                    te = "EditPurchaseInvoice.aspx?n12&Purchase_Details_Id=" + dsrc.Rows[0]["Purchase_Details_Id"];
                }
            }
            else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "30")
            {
                te = "RetailInvoice_Edit.aspx?vn=n12&Tid=" + tid.ToString();

            }
            else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "26")
            {
                DataTable drt = select("select TransactionMasterSalesChallanTbl.SalesChallanMasterId,SalesChallanDatetime,TransactionMasterId,SalesOrderMasterId from SalesChallanMaster inner join  TransactionMasterSalesChallanTbl ON TransactionMasterSalesChallanTbl.SalesChallanMasterId=SalesChallanMaster.SalesChallanMasterId where TransactionMasterId='" + tid.ToString() + "'");
                if (drt.Rows.Count > 0)
                {
                    te = "CustomerDCEdit.aspx?vn=n12&id1=" + Convert.ToString(drt.Rows[0]["SalesChallanMasterId"]);
                }
            }
            else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "34")
            {
                te = "ProductInvoiceReport.aspx?vn=n12&Tapid=" + tid.ToString();

            }
            else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "4")
            {
                DataTable dsrc = select(" SELECT  distinct Purchase_Details_Id,  TranctionMaster.Tranction_Master_Id, TranctionMaster.EntryNumber " +
                      " FROM PurchaseDetails INNER JOIN  TranctionMaster ON PurchaseDetails.TransId = TranctionMaster.Tranction_Master_Id " +
                 " where PurchaseDetails.TransId = '" + tid + "'  and EntryTypeId='4'");
                if (dsrc.Rows.Count > 0)
                {
                    te = "ExpenseInvoice.aspx?n12&Purchase_Details_Id=" + dsrc.Rows[0]["Purchase_Details_Id"];
                }
            }
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        if (ddllistofdoc.SelectedIndex > -1)
        {
            string str = "select DocumentMaster.DocumentId,DocumentMaster.DocumentTitle from DocumentMaster where DocumentMaster.DocumentId = '" + ddllistofdoc.SelectedValue + "'";
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
            te = "ExpenseInvoice.aspx?docid=" + ddllistofdoc.SelectedValue + "&ici=" + Session["Comid"];

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
        lblmsg.Text="";
        ddldoctype_SelectedIndexChanged(sender, e);
    }

    protected void ddldt_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pidn = "";
        if (ddldt.SelectedItem.Text == "Credit Invoice" || ddldt.SelectedItem.Text == "Cash Invoice" || ddldt.SelectedItem.Text == "Cash Voucher" || ddldt.SelectedItem.Text == "Credit Voucher")
        {
            RequicmnredFieljgdValidator2.Visible = true;
        }
        else
        {
            RequicmnredFieljgdValidator2.Visible = false;
        }
        DataTable dtr = select("select * from Party_master where PartyID='" + ddlpartyname.SelectedValue + "'");
        if (dtr.Rows.Count > 0)
        {
            pidn = " of " + Convert.ToString(dtr.Rows[0]["Compname"]);
        }
        else
        {

        }
        txtdoctitle.Text = ddldt.SelectedItem.Text + pidn + " dated " + TxtDocDate.Text + " for $ " + txtnetamount.Text;

    }
    //protected void btncon_Click(object sender, EventArgs e)
    //{
    //    string pidn = "";
    //    DataTable dtr = select("select * from Party_master where PartyID='" + ddlpartyname.SelectedValue + "'");
    //    if (dtr.Rows.Count > 0)
    //    {
    //        pidn = " of " + Convert.ToString(dtr.Rows[0]["Compname"]);
    //    }
    //    else
    //    {

    //    }
    //    txtdoctitle.Text = ddldt.SelectedItem.Text + pidn + " dated " + TxtDocDate.Text + " for $ " + txtnetamount.Text;
    //    int flagpd = 0;
    //    lblmsg.Text = "";
    //    //if (ddldt.SelectedItem.Text == "Credit Invoice" || ddldt.SelectedItem.Text == "Cash Invoice" || ddldt.SelectedItem.Text == "Cash Voucher" || ddldt.SelectedItem.Text == "Credit Voucher")
    //    //{
    //    if (txtpartdocrefno.Text.Length > 0)
    //    {
    //        DataTable dtsc = select("select PartyDocrefno from DocumentMaster where  DocumentMaster.DocumentId<>'" + ViewState["docId"] + "' and CID='" + Session["Comid"] + "' and PartyDocrefno='" + txtpartdocrefno.Text + "'");
    //        if (dtsc.Rows.Count == 0)
    //        {
    //        }
    //        else
    //        {
    //            flagpd = 1;
    //            lblmsg.Text = "Please enter unique party document reference number.";
    //        }
    //    }

    //    if (flagpd == 0)
    //    {
    //        Button1.Visible = true;
    //        btncon.Visible = false;
    //        fillen(false);
    //    }
    //}
    protected void fillen(bool allk)
    {
        ddlPartyType.Enabled = allk;
        ddldt.Enabled = allk;
        ddlpartyname.Enabled = allk;
        ddldocmaintype.Enabled = allk;
        ddldoctypeup.Enabled = allk;
        txtpartdocrefno.Enabled = allk;
        txtnetamount.Enabled = allk;
        txtdoctitle.Enabled = allk;
        txtdocrefnmbr.Enabled = allk;
 
        TxtDocDate.Enabled = allk;
        imgbtncal.Enabled = allk;
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        flaganddoc();
    }
}
