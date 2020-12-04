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
using System.Text;
using System.Net.Mail;
public partial class FilingDeskViewApprove : System.Web.UI.Page
{
    SqlConnection con;
    InstructionCls clsInstruction = new InstructionCls();
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
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);


        if (!IsPostBack)
        {
            flaganddoc();
            ddldt_SelectedIndexChanged(sender, e);
            if (Request.QueryString["id"] != null && Request.QueryString["Eid"] != null && Request.QueryString["Pid"] != null)
            {
                Session["no"] = "1";
                Session["no1"] = "1";
                ViewState["dtrws"] = "1";

               
                
                int docid = Convert.ToInt32(Request.QueryString["id"]);
                int Eid = Convert.ToInt32(Request.QueryString["Eid"]);
                int Pid = Convert.ToInt32(Request.QueryString["Pid"]);



                string strmasteridver = "select   DocumentMaster.*,DocumentProcessing.StatusId ,DocumentType.DocumentType, EmployeeMaster.Whid,Party_Master.Compname as PartyName,EmployeeMaster.EmployeeName, DocumentProcessing.ProcessingId as DocumentProcessingId,DocumentProcessing.ProcessingId ,DocumentProcessing.DocAllocateDate,DocumentProcessing.ApproveDate, DocumentProcessing.Approve, DocumentProcessing.Note, DocumentProcessing.StatusId,DocumentProcessing.Levelofaccess,DesignationMaster.DesignationName,DesignationMaster.DesignationMasterID as DesignationID , EmployeeMaster.EmployeeMasterID as EmployeeID ,CAST(DocumentMaster.DocumentId as Nvarchar(50))+' : '+ DocumentMaster.DocumentTitle +' : '+case when (DocumentProcessing.StatusId='0') then 'Pending-New' else  (case when (DocumentProcessing.StatusId='1') then 'Pending-Returned' else  (case when (DocumentProcessing.StatusId='2') then 'Rejected'  else (case when (DocumentProcessing.StatusId='3') then 'Approved'  else  '' End ) End   ) End )  End  as DocumentNameMaster  from DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join  EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where DocumentProcessing.EmployeeId = '" + Eid + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "' and DocumentProcessing.ProcessingId='" + Pid + "'   ";
                SqlCommand cmdmasteridver = new SqlCommand(strmasteridver, con);
                DataTable dtmasteridver = new DataTable();
                SqlDataAdapter adpmasteridver = new SqlDataAdapter(cmdmasteridver);
                adpmasteridver.Fill(dtmasteridver);
                if (dtmasteridver.Rows.Count > 0)
                {
                    fillstore();
                    ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dtmasteridver.Rows[0]["Whid"].ToString()));
                     fillemployee();
                    ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(Eid.ToString()));
                    if (dtmasteridver.Rows[0]["StatusId"].ToString() != null)
                    {
                        ddlapproval.SelectedValue = dtmasteridver.Rows[0]["StatusId"].ToString();
                        ddlapprovalstatusforupdate.SelectedValue = dtmasteridver.Rows[0]["StatusId"].ToString();
                    }
                    else
                    {
                        ddlapproval.SelectedValue = "0";
                        ddlapprovalstatusforupdate.SelectedValue = "0";
                    }

                    filldesignationquerystring();
                    fillpartytype();
                    fillemployeeofficeclerk();
                    fillemployeesupervisor();
                    fillmaintaype();
                    fillsubtype();
                    filldll();
                    RadioButtonList1.SelectedValue = "1";
                    
                    
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    txtfrom.Text = Convert.ToDateTime(dtmasteridver.Rows[0]["DocumentUploadDate"].ToString()).ToShortDateString();
                    txtto.Text = Convert.ToDateTime(dtmasteridver.Rows[0]["DocumentUploadDate"].ToString()).AddDays(1).ToShortDateString();

                    string doc = "SELECT  * from EmployeeDefaultDocumentDetail where Whid='" + ddlbusiness.SelectedValue + "' and EmployeeId='" + ddlemp.SelectedValue + "' ";
                    SqlDataAdapter adp = new SqlDataAdapter(doc, con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        CheckBox4.Visible = true;
                        Label57.Visible = true;
                    }
                    else
                    {
                        CheckBox4.Visible = false;
                        Label57.Visible = false;
                    }

                    fillgrid();

                    ddllistofdoc.SelectedIndex = ddllistofdoc.Items.IndexOf(ddllistofdoc.Items.FindByValue(docid.ToString()));

                    ddldocpreviousnext();

                    ViewState["sortOrder"] = "";

                    fillpopuppartytype();
                    FillpopupParty();
                    fillpopupmaintaype();
                    fillpopupsubtype();
                    fillpopupdll();

                   

                    

                }

                
            }
            else
            {
                Session["no"] = "1";
                Session["no1"] = "1";
                ViewState["dtrws"] = "1";

                fillstore();

                filldesignation();
                fillemployee();

                fillpartytype();
                fillemployeeofficeclerk();
                fillemployeesupervisor();

                fillmaintaype();
                fillsubtype();
                filldll();

                filldatebyperiod();


                string doc = "SELECT  * from EmployeeDefaultDocumentDetail where Whid='" + ddlbusiness.SelectedValue + "' and EmployeeId='" + ddlemp.SelectedValue + "' ";
                SqlDataAdapter adp = new SqlDataAdapter(doc, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    CheckBox4.Visible = true;
                    Label57.Visible = true;
                }
                else
                {
                    CheckBox4.Visible = false;
                    Label57.Visible = false;
                }



                fillgrid();
                ddldocpreviousnext();

                ViewState["sortOrder"] = "";

                fillpopuppartytype();
                FillpopupParty();
                fillpopupmaintaype();
                fillpopupsubtype();
                fillpopupdll();


            }



           

        }




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
                //lblddcname.Text = ViewState["decname"].ToString();
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

    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlbusnessmaster.SelectedValue = ddlbusiness.SelectedValue;
        fillmaintaype();
        fillsubtype();
        filldll();

       // filldesignation();
        fillemployeeofficeclerk();
        fillemployeesupervisor();
        fillgrid();
        ddldocpreviousnext();
       // fillemployee();


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

    protected DataTable select(string str)
    {

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    protected void fillstore()
    {
        ddlbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlbusiness.DataSource = ds;
        ddlbusiness.DataTextField = "Name";
        ddlbusiness.DataValueField = "WareHouseId";
        ddlbusiness.DataBind();
        ddlbusnessmaster.DataSource = ds;
        ddlbusnessmaster.DataTextField = "Name";
        ddlbusnessmaster.DataValueField = "WareHouseId";
        ddlbusnessmaster.DataBind();
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            ddlbusnessmaster.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
          //  ddlbusiness.Enabled = false;
        }


    }
    protected void fillemployee()
    {



        string eeed = "SELECT EmployeeMaster.EmployeeMasterID ,DesignationMaster.DesignationName+' : '+EmployeeMaster.EmployeeName as EmployeeName FROM         DesignationMaster inner join DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id inner join EmployeeMaster ON EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId WHERE      DepartmentmasterMNC.Companyid='" + Session["Comid"] + "' and DepartmentmasterMNC.Whid='" + ddlbusiness.SelectedValue + "' and (DepartmentmasterMNC.DepartmentName='Filling Desk') and  EmployeeMaster.Whid ='" + ddlbusiness.SelectedValue + "' and DesignationMaster.Designationname in('Manager','Office Clerk','Supervisor') order by EmployeeName ";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ddlemp.DataSource = dteeed;
            ddlemp.DataTextField = "EmployeeName";
            ddlemp.DataValueField = "EmployeeMasterID";
            ddlemp.DataBind();
            ddlemp.Items.Insert(0, "-Select-");
            ddlemp.Items[0].Value = "0";
            ddlemp.SelectedValue = Session["EmployeeID"].ToString();
            ddlemp.Enabled = false;



        }
        else
        {
            ddlemp.DataSource = null;
            ddlemp.DataTextField = "EmployeeName";
            ddlemp.DataValueField = "EmployeeMasterID";
            ddlemp.DataBind();
            ddlemp.Items.Insert(0, "-Select-");
            ddlemp.Items[0].Value = "0";
        }

    }
    protected void filldesignation()
    {
        string strdesig = " select DesignationMaster.* from EmployeeMaster inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID where EmployeeMaster.EmployeeMasterID='" + Session["EmployeeID"].ToString() + "'  ";
        SqlCommand cmdeeed = new SqlCommand(strdesig, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        if (dteeed.Rows.Count > 0)
        {
            ViewState["DesignationName"] = dteeed.Rows[0]["DesignationName"].ToString();
            lbldesignation.Text = dteeed.Rows[0]["DesignationName"].ToString();
        }


    }
    protected void filldesignationquerystring()
    {
        string strdesig = " select DesignationMaster.* from EmployeeMaster inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID where EmployeeMaster.EmployeeMasterID='" + Convert.ToInt32(Request.QueryString["Eid"]) + "'  ";
        SqlCommand cmdeeed = new SqlCommand(strdesig, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        if (dteeed.Rows.Count > 0)
        {
            ViewState["DesignationName"] = dteeed.Rows[0]["DesignationName"].ToString();
            lbldesignation.Text = dteeed.Rows[0]["DesignationName"].ToString();
        }


    }


    protected void fillgrid()
    {

        ddllistofdoc.Items.Clear();


        string strmaster = "select   DocumentMaster.*, DocumentType.DocumentType, Party_Master.Compname as PartyName,EmployeeMaster.EmployeeName, DocumentProcessing.ProcessingId as DocumentProcessingId,DocumentProcessing.ProcessingId ,DocumentProcessing.DocAllocateDate,DocumentProcessing.ApproveDate, DocumentProcessing.Approve, DocumentProcessing.Note, DocumentProcessing.StatusId,DocumentProcessing.Levelofaccess,DesignationMaster.DesignationName,DesignationMaster.DesignationMasterID as DesignationID , EmployeeMaster.EmployeeMasterID as EmployeeID ,CAST(DocumentMaster.DocumentId as Nvarchar(50))+' : '+ DocumentMaster.DocumentTitle +' : '+case when (DocumentProcessing.StatusId='0') then 'Pending-New' else  (case when (DocumentProcessing.StatusId='1') then 'Pending-Returned' else  (case when (DocumentProcessing.StatusId='2') then 'Rejected'  else (case when (DocumentProcessing.StatusId='3') then 'Approved'  else  '' End ) End   ) End )  End  as DocumentNameMaster  from DocumentMainType inner join     DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster on  DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId inner join DocumentProcessing on  DocumentProcessing.DocumentId= DocumentMaster.DocumentId inner join   EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and DocumentProcessing.EmployeeId = '" + ddlemp.SelectedValue + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "'  ";
        string status = "";
        string strbyperiod = "";
        string strbydate = "";
        string strsearch = "";
        string strfilterbyofficeclerk = "";
        string strfilterbysupervisor = "";

        if (ddlapproval.SelectedValue == "5")
        {
            status = " AND (DocumentProcessing.StatusId='0' or DocumentProcessing.StatusId='1') ";

        }
        else
        {
            status = " AND DocumentProcessing.StatusId='" + ddlapproval.SelectedValue + "' ";
        }




        if (RadioButtonList1.SelectedValue == "0")
        {

            if (ViewState["periodstartdate"] != null && ViewState["periodenddate"] != null)
            {
                strbyperiod = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + ViewState["periodstartdate"].ToString() + "' and '" + ViewState["periodenddate"].ToString() + "'";
            }
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtfrom.Text != "" && txtto.Text != "")
            {
                strbydate = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
            }
        }

        if (chkfilteronapprovalstatus.Checked == true)
        {

            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue == "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex == 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }


            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue == "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex == 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }

        }
        string finalstr = strmaster + status + strsearch + strbyperiod + strbydate + strfilterbyofficeclerk + strfilterbysupervisor;
        SqlCommand cmdeeed = new SqlCommand(finalstr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);


        if (dteeed.Rows.Count > 0)
        {

            ddllistofdoc.DataSource = dteeed;
            ddllistofdoc.DataTextField = "DocumentNameMaster";
            ddllistofdoc.DataValueField = "DocumentId";
            ddllistofdoc.DataBind();
        }

        else
        {
            ddllistofdoc.DataSource = null;
            ddllistofdoc.DataTextField = "DocumentNameMaster";
            ddllistofdoc.DataValueField = "DocumentId";
            ddllistofdoc.DataBind();
            ddllistofdoc.Items.Insert(0, "-Select-");
            ddllistofdoc.Items[0].Value = "0";
        }


        ViewState["docid"] = Convert.ToInt32(ddllistofdoc.SelectedValue);

        LoadPdf(Convert.ToInt32(ddllistofdoc.SelectedValue));
        if (ddllistofdoc.SelectedValue == "0")
        {
            pnlupdatedoc.Visible = false;
        }
        else
        {
            pnlupdatedoc.Visible = true;
            //fillupdatepanel();
            defaultviewswitch();
        }


        //  fillgriddate();


        Approvalstatusselection();
        showentry();


    }

    protected void ddllistofdoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddldocpreviousnext();
        Approvalstatusselection();
      
        defaultviewswitch();

        Session["no"] = "1";
        Session["no1"] = "1";

        ViewState["docid"] = Convert.ToInt32(ddllistofdoc.SelectedValue);
        LoadPdf(Convert.ToInt32(ddllistofdoc.SelectedValue));


    }
    protected void filesave()
    {

    }
    protected void imgimgo_Click(object sender, EventArgs e)
    {
        Session["no"] = lblnooff.Text;
        Session["no1"] = lblnooff.Text;
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
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (RadioButtonList1.SelectedValue == "0")
        {
            pnlperiod.Visible = true;
            pnldate.Visible = false;
        }
        else
        {
            pnlperiod.Visible = false;
            pnldate.Visible = true;
        }
        filldatebyperiod();

    }
    protected void filldatebyperiod()
    {
        //date between you should use  date first and earlier date lateafter
        string Today, Yesterday, ThisYear;
        Today = Convert.ToString(System.DateTime.Today.ToShortDateString());
        Yesterday = Convert.ToString(System.DateTime.Today.AddDays(-1).ToShortDateString());
        ThisYear = Convert.ToString(System.DateTime.Today.Year.ToString());


        //-------------------this week start...............
        DateTime d1, d2, d3, d4, d5, d6, d7;
        DateTime weekstart, weekend;
        d1 = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
        d2 = Convert.ToDateTime(System.DateTime.Today.AddDays(-1).ToShortDateString());
        d3 = Convert.ToDateTime(System.DateTime.Today.AddDays(-2).ToShortDateString());
        d4 = Convert.ToDateTime(System.DateTime.Today.AddDays(-3).ToShortDateString());
        d5 = Convert.ToDateTime(System.DateTime.Today.AddDays(-4).ToShortDateString());
        d6 = Convert.ToDateTime(System.DateTime.Today.AddDays(-5).ToShortDateString());
        d7 = Convert.ToDateTime(System.DateTime.Today.AddDays(-6).ToShortDateString());
        string ThisWeek = (System.DateTime.Today.DayOfWeek.ToString());
        if (ThisWeek.ToString() == "Monday")
        {
            weekstart = d1;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(ThisWeek) == "Tuesday")
        {
            weekstart = d2;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Wednesday")
        {
            weekstart = d3;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Thursday")
        {
            weekstart = d4;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Friday")
        {
            weekstart = d5;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Saturday")
        {
            weekstart = d6;
            weekend = weekstart.Date.AddDays(+6);

        }
        else
        {
            weekstart = d7;
            weekend = weekstart.Date.AddDays(+6);
        }
        string thisweekstart = weekstart.ToShortDateString();
        string thisweekend = weekend.ToShortDateString();

        //.................this week duration end.....................

        ///--------------------last week duration ....

        DateTime d17, d8, d9, d10, d11, d12, d13;
        DateTime lastweekstart, lastweekend;
        d17 = Convert.ToDateTime(System.DateTime.Today.AddDays(-7).ToShortDateString());
        d8 = Convert.ToDateTime(System.DateTime.Today.AddDays(-8).ToShortDateString());
        d9 = Convert.ToDateTime(System.DateTime.Today.AddDays(-9).ToShortDateString());
        d10 = Convert.ToDateTime(System.DateTime.Today.AddDays(-10).ToShortDateString());
        d11 = Convert.ToDateTime(System.DateTime.Today.AddDays(-11).ToShortDateString());
        d12 = Convert.ToDateTime(System.DateTime.Today.AddDays(-12).ToShortDateString());
        d13 = Convert.ToDateTime(System.DateTime.Today.AddDays(-13).ToShortDateString());
        string thisday = (System.DateTime.Today.DayOfWeek.ToString());

        if (thisday.ToString() == "Monday")
        {
            lastweekstart = d17;
            lastweekend = lastweekstart.Date.AddDays(+5);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            lastweekstart = d8;
            lastweekend = lastweekstart.Date.AddDays(+5);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            lastweekstart = d9;
            lastweekend = lastweekstart.Date.AddDays(+5);
        }
        else if (thisday.ToString() == "Thursday")
        {
            lastweekstart = d10;
            lastweekend = lastweekstart.Date.AddDays(+5);
        }
        else if (thisday.ToString() == "Friday")
        {
            lastweekstart = d11;
            lastweekend = lastweekstart.Date.AddDays(+5);
        }
        else if (thisday.ToString() == "Saturday")
        {
            lastweekstart = d12;
            lastweekend = lastweekstart.Date.AddDays(+5);

        }
        else
        {
            lastweekstart = d13;
            lastweekend = lastweekstart.Date.AddDays(+5);
        }
        string lastweekstartdate = lastweekstart.ToShortDateString();
        string lastweekenddate = lastweekend.ToShortDateString();
        //---------------lastweek duration end.................

        //        Today
        //2	Yesterday
        //3	ThisWeek
        //4	LastWeek
        //5	ThisMonth
        //6	LastMonth
        //7	ThisQuarter
        //8	LastQuarter
        //9	ThisYear
        //10Last Year
        //------------------this month period-----------------

        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        string thismonthstartdate = thismonthstart.ToShortDateString();
        string thismonthenddate = Today.ToString();
        //------------------this month period end................



        //-----------------last month period start ---------------


        int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        if (lastmonthno ==0)
        {
            lastmonthno = 12;
          Int32  ThisYearstr =Convert.ToInt32(ThisYear) - 1;
          ThisYear =Convert.ToString(ThisYearstr);
        }
        string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        DateTime lastmonth = Convert.ToDateTime(lastmonthNumber.ToString() + "/1/" + ThisYear.ToString());
        string lastmonthstart = lastmonth.ToShortDateString();
        string lastmonthend = "";

        if (lastmonthNumber == "1" || lastmonthNumber == "3" || lastmonthNumber == "5" || lastmonthNumber == "7" || lastmonthNumber == "8" || lastmonthNumber == "10" || lastmonthNumber == "12")
        {
            lastmonthend = lastmonthNumber + "/31/" + ThisYear.ToString();
        }
        else if (lastmonthNumber == "4" || lastmonthNumber == "6" || lastmonthNumber == "9" || lastmonthNumber == "11")
        {
            lastmonthend = lastmonthNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastmonthend = lastmonthNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                lastmonthend = lastmonthNumber + "/28/" + ThisYear.ToString();
            }
        }

        string lastmonthstartdate = lastmonthstart.ToString();
        string lastmonthenddate = lastmonthend.ToString();


        //-----------------last month period end -----------------------

        //--------------------this quater period start ----------------


        int thisqtr = 0;
        string thisqtrNumber = "";
        int mon = Convert.ToInt32(DateTime.Now.Month.ToString());
        if (mon >= 1 && mon <= 3)
        {
            thisqtr = 1;
            thisqtrNumber = "3";

        }
        else if (mon >= 4 && mon <= 6)
        {
            thisqtr = 4;
            thisqtrNumber = "6";
        }
        else if (mon >= 7 && mon <= 9)
        {
            thisqtr = 7;
            thisqtrNumber = "9";
        }
        else if (mon >= 10 && mon <= 12)
        {
            thisqtr = 10;
            thisqtrNumber = "12";
        }


        DateTime thisquater = Convert.ToDateTime(thisqtr.ToString() + "/1/" + ThisYear.ToString());
        string thisquaterstart = thisquater.ToShortDateString();

        string thisquaterend = "";

        if (thisqtrNumber == "1" || thisqtrNumber == "3" || thisqtrNumber == "5" || thisqtrNumber == "7" || thisqtrNumber == "8" || thisqtrNumber == "10" || thisqtrNumber == "12")
        {
            thisquaterend = thisqtrNumber + "/31/" + ThisYear.ToString();
        }
        else if (thisqtrNumber == "4" || thisqtrNumber == "6" || thisqtrNumber == "9" || thisqtrNumber == "11")
        {
            thisquaterend = thisqtrNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                thisquaterend = thisqtrNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                thisquaterend = thisqtrNumber + "/28/" + ThisYear.ToString();
            }
        }

        string thisquaterstartdate = thisquaterstart.ToString();
        string thisquaterenddate = thisquaterend.ToString();


        // --------------this quater period end ------------------------

        // --------------last quater period start----------------------

        int lastqtr = Convert.ToInt32(thismonthstart.AddMonths(-5).Month.ToString());// -5;
        string lastqtrNumber = Convert.ToString(lastqtr.ToString());
        int lastqater3 = Convert.ToInt32(thismonthstart.AddMonths(-3).Month.ToString());
        //DateTime lastqater3 = Convert.ToDateTime(System.DateTime.Now.AddMonths(-3).Month.ToString());
        string lasterquater3 = lastqater3.ToString();
        DateTime lastquater = Convert.ToDateTime(lastqtrNumber.ToString() + "/1/" + ThisYear.ToString());
        string lastquaterstart = lastquater.ToShortDateString();
        string lastquaterend = "";

        if (lasterquater3 == "1" || lasterquater3 == "3" || lasterquater3 == "5" || lasterquater3 == "7" || lasterquater3 == "8" || lasterquater3 == "10" || lasterquater3 == "12")
        {
            lastquaterend = lasterquater3 + "/31/" + ThisYear.ToString();
        }
        else if (lasterquater3 == "4" || lasterquater3 == "6" || lasterquater3 == "9" || lasterquater3 == "11")
        {
            lastquaterend = lasterquater3 + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastquaterend = lasterquater3 + "/29/" + ThisYear.ToString();
            }
            else
            {
                lastquaterend = lasterquater3 + "/28/" + ThisYear.ToString();
            }
        }

        string lastquaterstartdate = lastquaterstart.ToString();
        string lastquaterenddate = lastquaterend.ToString();

        //--------------last quater period end-------------------------

        //--------------this year period start----------------------
        DateTime thisyearstart = Convert.ToDateTime("1/1/" + ThisYear.ToString());
        DateTime thisyearend = Convert.ToDateTime("12/31/" + ThisYear.ToString());

        string thisyearstartdate = thisyearstart.ToShortDateString();
        string thisyearenddate = thisyearend.ToShortDateString();

        //---------------this year period end-------------------
        //--------------this year period start----------------------
        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());

        string lastyearstartdate = lastyearstart.ToShortDateString();
        string lastyearenddate = lastyearend.ToShortDateString();



        //---------------this year period end-------------------


        string periodstartdate = "";
        string periodenddate = "";

        if (ddlDuration.SelectedItem.Text == "Today")
        {
            periodstartdate = Today.ToString();
            periodenddate = Today.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "Yesterday")
        {
            periodstartdate = Yesterday.ToString();
            periodenddate = Yesterday.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "This Week")
        {
            periodstartdate = thisweekstart.ToString();
            periodenddate = thisweekend.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "Last Week")
        {

            periodstartdate = lastweekstartdate.ToString();
            periodenddate = lastweekenddate.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "This Month")
        {

            periodstartdate = thismonthstart.ToShortDateString();
            periodenddate = Today.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "Last Month")
        {

            periodstartdate = lastmonthstartdate.ToString();
            periodenddate = lastmonthenddate.ToString();


        }
        else if (ddlDuration.SelectedItem.Text == "This Quarter")
        {

            periodstartdate = thisquaterstartdate.ToString();
            periodenddate = thisquaterenddate.ToString();


        }
        else if (ddlDuration.SelectedItem.Text == "Last Quarter")
        {

            periodstartdate = lastquaterstartdate.ToString();
            periodenddate = lastquaterenddate.ToString();


        }

        else if (ddlDuration.SelectedItem.Text == "This Year")
        {

            periodstartdate = thisyearstartdate.ToString();
            periodenddate = thisyearenddate.ToString();


        }
        else if (ddlDuration.SelectedItem.Text == "Last Year")
        {

            periodstartdate = lastyearstartdate.ToString();
            periodenddate = lastyearenddate.ToString();
        }
        else
        {
            periodstartdate = Today.ToString();
            periodenddate = Today.ToString();
        }
        if (periodstartdate.Length > 0)
        {
            txtfrom.Text = periodstartdate;
            txtto.Text = periodenddate;

        }
        ViewState["periodstartdate"] = periodstartdate.ToString();
        ViewState["periodenddate"] = periodenddate.ToString();
    }
    protected void ddlDuration_SelectedIndexChanged(object sender, EventArgs e)
    {

        filldatebyperiod();


    }
    protected void fillemployeeofficeclerk()
    {

        string eeed = "SELECT EmployeeMaster.EmployeeMasterID ,DesignationMaster.DesignationName+' : '+EmployeeMaster.EmployeeName as EmployeeName FROM         DesignationMaster inner join DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id inner join EmployeeMaster ON EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId WHERE      DepartmentmasterMNC.Companyid='" + Session["Comid"] + "' and DepartmentmasterMNC.Whid='" + ddlbusiness.SelectedValue + "' and (DepartmentmasterMNC.DepartmentName='Filling Desk') and  EmployeeMaster.Whid ='" + ddlbusiness.SelectedValue + "' and (DesignationMaster.DesignationName ='Office Clerk' ) order by EmployeeName ";

        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ddlfilterofficeclerk.DataSource = dteeed;
            ddlfilterofficeclerk.DataTextField = "EmployeeName";
            ddlfilterofficeclerk.DataValueField = "EmployeeMasterID";
            ddlfilterofficeclerk.DataBind();
            ddlfilterofficeclerk.Items.Insert(0, "All");
            ddlfilterofficeclerk.Items[0].Value = "0";



        }
        else
        {
            ddlfilterofficeclerk.DataSource = null;
            ddlfilterofficeclerk.DataTextField = "EmployeeName";
            ddlfilterofficeclerk.DataValueField = "EmployeeMasterID";
            ddlfilterofficeclerk.DataBind();
            ddlfilterofficeclerk.Items.Insert(0, "All");
            ddlfilterofficeclerk.Items[0].Value = "0";
        }

    }
    protected void fillemployeesupervisor()
    {
        //string eeed = "Select distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName from EmployeeMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID inner join  DesignationMaster on DesignationMaster.DeptId=DepartmentmasterMNC.Id where DepartmentmasterMNC.Departmentname='Filling Desk' and  DesignationMaster.Designationname ='Supervisor' and  EmployeeMaster.Whid ='" + ddlbusiness.SelectedValue + "' order by EmployeeName";
        string eeed = "SELECT EmployeeMaster.EmployeeMasterID ,DesignationMaster.DesignationName+' : '+EmployeeMaster.EmployeeName as EmployeeName FROM         DesignationMaster inner join DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id inner join EmployeeMaster ON EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId WHERE      DepartmentmasterMNC.Companyid='" + Session["Comid"] + "' and DepartmentmasterMNC.Whid='" + ddlbusiness.SelectedValue + "' and (DepartmentmasterMNC.DepartmentName='Filling Desk') and  EmployeeMaster.Whid ='" + ddlbusiness.SelectedValue + "' and (DesignationMaster.DesignationName ='Supervisor' ) order by EmployeeName ";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ddlsupervisorfilter.DataSource = dteeed;
            ddlsupervisorfilter.DataTextField = "EmployeeName";
            ddlsupervisorfilter.DataValueField = "EmployeeMasterID";
            ddlsupervisorfilter.DataBind();
            ddlsupervisorfilter.Items.Insert(0, "All");
            ddlsupervisorfilter.Items[0].Value = "0";



        }
        else
        {
            ddlsupervisorfilter.DataSource = null;
            ddlsupervisorfilter.DataTextField = "EmployeeName";
            ddlsupervisorfilter.DataValueField = "EmployeeMasterID";
            ddlsupervisorfilter.DataBind();
            ddlsupervisorfilter.Items.Insert(0, "All");
            ddlsupervisorfilter.Items[0].Value = "0";
        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Select Display Columns")
        {
            Button4.Text = "Hide Display Columns";
            Panel6.Visible = true;
        }
        else
        {
            Button4.Text = "Select Display Columns";
            Panel6.Visible = false;

        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void columndisplay()
    {
        if (chkidcolumn.Checked == true)
        {
            gridocapproval.Columns[0].Visible = true;
        }
        else
        {
            gridocapproval.Columns[0].Visible = false;
        }
        if (chktitlecolumn.Checked == true)
        {
            gridocapproval.Columns[1].Visible = true;
        }
        else
        {
            gridocapproval.Columns[1].Visible = false;

        }
        if (chkfileextsion.Checked == true)
        {
            gridocapproval.Columns[2].Visible = true;
        }
        else
        {
            gridocapproval.Columns[2].Visible = false;
        }
        if (chkfoldername.Checked == true)
        {
            gridocapproval.Columns[3].Visible = true;
        }
        else
        {
            gridocapproval.Columns[3].Visible = false;
        }

        if (chkpartycolumn.Checked == true)
        {
            gridocapproval.Columns[4].Visible = true;
        }
        else
        {
            gridocapproval.Columns[4].Visible = false;
        }


        if (chkdocumentdate.Checked == true)
        {

            gridocapproval.Columns[5].Visible = true;
        }
        else
        {
            gridocapproval.Columns[5].Visible = false;
        }

        if (chkrefno.Checked == true)
        {

            gridocapproval.Columns[6].Visible = true;
        }
        else
        {
            gridocapproval.Columns[6].Visible = false;
        }


        if (chkdocamount.Checked == true)
        {

            gridocapproval.Columns[7].Visible = true;
        }
        else
        {
            gridocapproval.Columns[7].Visible = false;
        }




        if (chkuploaddate.Checked == true)
        {

            gridocapproval.Columns[8].Visible = true;
        }
        else
        {
            gridocapproval.Columns[8].Visible = false;
        }

        if (chkmyfoldercolumn.Checked == true)
        {
            gridocapproval.Columns[9].Visible = true;
        }
        else
        {
            gridocapproval.Columns[9].Visible = false;
        }

        if (chkaddtomyfoldercolumn.Checked == true)
        {
            gridocapproval.Columns[10].Visible = true;

        }
        else
        {
            gridocapproval.Columns[10].Visible = false;
        }

        if (chkaccountentrycolumn.Checked == true)
        {
            gridocapproval.Columns[11].Visible = true;
        }
        else
        {
            gridocapproval.Columns[11].Visible = false;
        }

        if (chksendmessagecolumn.Checked == true)
        {
            gridocapproval.Columns[12].Visible = true;
        }
        else
        {
            gridocapproval.Columns[12].Visible = false;
        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        if (ddllistofdoc.SelectedValue != "0")
        {
            string str1010 = " update  DocumentMaster set DocumentTypeId='" + ddlDocType.SelectedValue + "',DocumentTitle='" + txtdoctitle.Text + "',PartyId='" + ddlpartyname.SelectedValue + "' ,DocumentDate='" + TxtDocDate.Text + "',DocumentRefNo='" + txtdocrefnmbr.Text + "' ,DocumentAmount='" + txtnetamount.Text + "',DocumentTypenmId='" + ddldt.SelectedValue + "',PartyDocrefno='"+txtpartdocrefno.Text +"' where DocumentId='" + ddllistofdoc.SelectedValue + "' ";

            SqlCommand cmd1010 = new SqlCommand(str1010, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1010.ExecuteNonQuery();
            con.Close();



        }





    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        pnlupdatedoc.Visible = false;
        txtdoctitle.Text = "";
        ddlpartyname.SelectedIndex = -1;
        ddlDocType.SelectedIndex = -1;
    }
    protected void gridocapproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridocapproval.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void gridocapproval_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();
    }
    protected void gridocapproval_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit1")
        {
            lblmsg.Text = "";
            int dk1 = Convert.ToInt32(e.CommandArgument);
            ViewState["MasterId"] = dk1.ToString();

            SqlCommand cmdedit = new SqlCommand("Select * from DocumentMaster where DocumentId='" + dk1 + "'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);

            if (dtedit.Rows.Count > 0)
            {


                lbldocidmaster.Text = dtedit.Rows[0]["DocumentId"].ToString();

                filldll();
                ddlDocType.SelectedIndex = ddlDocType.Items.IndexOf(ddlDocType.Items.FindByValue(dtedit.Rows[0]["DocumentTypeId"].ToString()));
                FillParty();
                if (dtedit.Rows[0]["PartyId"].ToString() != null)
                {

                    ddlpartyname.SelectedIndex = ddlpartyname.Items.IndexOf(ddlpartyname.Items.FindByValue(dtedit.Rows[0]["PartyId"].ToString()));
                }
                txtdoctitle.Text = dtedit.Rows[0]["DocumentTitle"].ToString();

                txtdocrefnmbr.Text = dtedit.Rows[0]["DocumentRefNo"].ToString();
                txtnetamount.Text = dtedit.Rows[0]["DocumentAmount"].ToString();


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
    protected void gridocapproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbllevelofaccess = (Label)e.Row.FindControl("lbllevelofaccess");
            Label lblofficeclarkapproval = (Label)e.Row.FindControl("lblofficeclarkapproval");
            Label lblsupervisorapproval = (Label)e.Row.FindControl("lblsupervisorapproval");



            Label lblstatusid = (Label)e.Row.FindControl("lblstatusid");
            Label lblapprovalstatus = (Label)e.Row.FindControl("lblapprovalstatus");
            Label lbldocumentid = (Label)e.Row.FindControl("lbldocumentid");
            TextBox txtNote = (TextBox)e.Row.FindControl("txtNote");
            DropDownList rbtnAcceptReject = (DropDownList)e.Row.FindControl("rbtnAcceptReject");
            Label lblmasterid = (Label)e.Row.FindControl("lblmasterid");
            Image Image2 = (Image)e.Row.FindControl("Image2");
            ImageButton llinedit = (ImageButton)e.Row.FindControl("llinedit");
            ImageButton ImageButton2 = (ImageButton)e.Row.FindControl("ImageButton2");


            Image Image1 = (Image)e.Row.FindControl("Image1");
            ImageButton ImageButton3 = (ImageButton)e.Row.FindControl("ImageButton3");





            if (lbllevelofaccess.Text == "1")
            {
                if (lblstatusid.Text != "")
                {
                    rbtnAcceptReject.SelectedValue = lblstatusid.Text;
                }

                if (lblstatusid.Text == "3" || lblstatusid.Text == "2")
                {
                    rbtnAcceptReject.Enabled = false;

                    llinedit.Visible = false;
                    ImageButton2.Visible = true;

                    Image1.Visible = false;
                    ImageButton3.Visible = true;
                }
                else
                {
                    rbtnAcceptReject.Enabled = true;
                    llinedit.Enabled = true;
                    ImageButton2.Visible = false;

                    Image1.Visible = true;
                    ImageButton3.Visible = false;
                }
            }

            if (lbllevelofaccess.Text == "2")
            {
                if (lblstatusid.Text != "")
                {
                    rbtnAcceptReject.SelectedValue = lblstatusid.Text;
                }

                string strt = " select * from  DocumentProcessing where DocumentId='" + lbldocumentid.Text + "' and  Levelofaccess='3' ";
                SqlCommand cmd1t = new SqlCommand(strt, con);
                cmd1t.CommandType = CommandType.Text;
                SqlDataAdapter dat = new SqlDataAdapter(cmd1t);
                DataTable dtt = new DataTable();
                dat.Fill(dtt);

                if (dtt.Rows.Count > 0)
                {

                    int flag = 0;
                    foreach (DataRow dr in dtt.Rows)
                    {
                        string status = dr["StatusId"].ToString();

                        if (status == "3")
                        {
                            flag = 1;

                        }


                    }
                    if (flag == 1)
                    {
                        rbtnAcceptReject.Enabled = false;

                        llinedit.Visible = false;
                        ImageButton2.Visible = true;

                        Image1.Visible = false;
                        ImageButton3.Visible = true;
                    }
                    else
                    {
                        rbtnAcceptReject.Enabled = true;
                        llinedit.Enabled = true;
                        ImageButton2.Visible = false;

                        Image1.Visible = true;
                        ImageButton3.Visible = false;
                    }

                }
                else
                {
                    rbtnAcceptReject.Enabled = true;
                    llinedit.Enabled = true;
                    ImageButton2.Visible = false;

                    Image1.Visible = true;
                    ImageButton3.Visible = false;
                }

            }
            if (lbllevelofaccess.Text == "3")
            {
                if (lblstatusid.Text != "")
                {
                    rbtnAcceptReject.SelectedValue = lblstatusid.Text;
                }
            }



            string strofficeclerk = " select DocumentProcessing.*,EmployeeMaster.EmployeeName,case when (DocumentProcessing.StatusId='0') then 'Pending-New' else  (case when (DocumentProcessing.StatusId='1') then 'Pending-Returned' else  (case when (DocumentProcessing.StatusId='2') then 'Rejected'  else (case when (DocumentProcessing.StatusId='3') then 'Approved'  else  '' End ) End   ) End )  End  as Statuslabel from  DocumentProcessing inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=DocumentProcessing.EmployeeId where DocumentId='" + lbldocumentid.Text + "' and  Levelofaccess='1'  ";
            SqlCommand cmdofficeclerk = new SqlCommand(strofficeclerk, con);
            cmdofficeclerk.CommandType = CommandType.Text;
            SqlDataAdapter adpofficeclerk = new SqlDataAdapter(cmdofficeclerk);
            DataTable dtofficeclerk = new DataTable();
            adpofficeclerk.Fill(dtofficeclerk);

            if (dtofficeclerk.Rows.Count > 0)
            {
                string strId = "";
                string strInvAllIds = "";
                string strtemp = "";

                foreach (DataRow dtrrr in dtofficeclerk.Rows)
                {
                    strId = dtrrr["EmployeeName"].ToString() + "-" + dtrrr["Statuslabel"].ToString();
                    strInvAllIds = strId + " <br/>" + strInvAllIds;
                    strtemp = strInvAllIds.Substring(0, (strInvAllIds.Length - 1));
                }
                lblofficeclarkapproval.Text = strtemp.ToString();
            }
            else
            {
                lblofficeclarkapproval.Text = "";
            }

            string strsupervisorapprove = " select DocumentProcessing.*,EmployeeMaster.EmployeeName,case when (DocumentProcessing.StatusId='0') then 'Pending-New' else  (case when (DocumentProcessing.StatusId='1') then 'Pending-Returned' else  (case when (DocumentProcessing.StatusId='2') then 'Rejected'  else (case when (DocumentProcessing.StatusId='3') then 'Approved'  else  '' End ) End   ) End )  End  as Statuslabel from  DocumentProcessing inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=DocumentProcessing.EmployeeId where DocumentId='" + lbldocumentid.Text + "' and  Levelofaccess='2'  ";
            SqlCommand cmdsupervisorapprove = new SqlCommand(strsupervisorapprove, con);
            cmdsupervisorapprove.CommandType = CommandType.Text;
            SqlDataAdapter adpsupervisorapprove = new SqlDataAdapter(cmdsupervisorapprove);
            DataTable dtsupervisorapprove = new DataTable();
            adpsupervisorapprove.Fill(dtsupervisorapprove);

            if (dtsupervisorapprove.Rows.Count > 0)
            {
                string strId1 = "";
                string strInvAllIds1 = "";
                string strtemp1 = "";

                foreach (DataRow dtrrr in dtsupervisorapprove.Rows)
                {
                    strId1 = dtrrr["EmployeeName"].ToString() + "-" + dtrrr["Statuslabel"].ToString();

                    strInvAllIds1 = strId1 + " <br/>" + strInvAllIds1;
                    strtemp1 = strInvAllIds1.Substring(0, (strInvAllIds1.Length - 1));
                }
                lblsupervisorapproval.Text = strtemp1.ToString();
            }
            else
            {
                lblsupervisorapproval.Text = "";
            }


        }
    }

    protected void FillParty()
    {
        ddlpartyname.Items.Clear();
        string qryStr = "";

        if (ddlPartyType.SelectedItem.Text == "Employee")
        {
            qryStr = "select Party_master.PartyID, EmployeeMaster.EmployeeName  +':'+ DesignationMaster.DesignationName +':'+DepartmentmasterMNC.Departmentname as PartyName  from EmployeeMaster inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId inner join DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId WHERE   Party_master.Whid='" + ddlbusnessmaster.SelectedValue + "' and Party_master.PartyTypeId='" + ddlPartyType.SelectedValue + "' order by PartyName ";
        }
        else if (ddlPartyType.SelectedItem.Text == "Candidate")
        {
            qryStr = " select Party_master.PartyID, CandidateMaster.LastName +' '+ CandidateMaster.FirstName +' '+ CandidateMaster.MiddleName as PartyName  from CandidateMaster inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId WHERE   Party_master.Whid='" + ddlbusnessmaster.SelectedValue + "' and Party_master.PartyTypeId='" + ddlPartyType.SelectedValue + "' order by PartyName  ";
        }
        else
        {
            qryStr = "SELECT  PartyId, Party_Master.PartyTypeId, Party_Master.Compname + ':'+ Party_Master.ContactPerson as PartyName, ContactPerson FROM   Party_master inner JOIN    PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId WHERE   Party_master.Whid='" + ddlbusnessmaster.SelectedValue + "' and Party_master.PartyTypeId='" + ddlPartyType.SelectedValue + "' order by PartyName  ";
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
    protected void fillgriddate()
    {
        columndisplay();


        string strmaster = "select   DocumentMaster.*, DocumentType.DocumentType, Party_Master.Compname as PartyName,EmployeeMaster.EmployeeName, DocumentProcessing.ProcessingId as DocumentProcessingId,DocumentProcessing.ProcessingId ,DocumentProcessing.DocAllocateDate,DocumentProcessing.ApproveDate, DocumentProcessing.Approve, DocumentProcessing.Note, DocumentProcessing.StatusId,DocumentProcessing.Levelofaccess,DesignationMaster.DesignationName,DesignationMaster.DesignationMasterID as DesignationID , EmployeeMaster.EmployeeMasterID as EmployeeID   from DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join  EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where DocumentProcessing.EmployeeId = '" + ddlemp.SelectedValue + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "' and DocumentProcessing.DocumentId='" + ddllistofdoc.SelectedValue + "'  ";

        //string status = "";
        //string strbyperiod = "";
        //string strbydate = "";
        //string strsearch = "";
        //string strfilterbyofficeclerk = "";
        //string strfilterbysupervisor = "";

        //if (ddlapproval.SelectedValue == "5")
        //{
        //    status = " AND (DocumentProcessing.StatusId='0' or DocumentProcessing.StatusId='1') ";

        //}
        //else
        //{
        //    status = " AND DocumentProcessing.StatusId='" + ddlapproval.SelectedValue + "' ";
        //}

        //if (RadioButtonList1.SelectedValue == "0")
        //{

        //    if (ViewState["periodstartdate"] != null && ViewState["periodenddate"] != null)
        //    {
        //        strbyperiod = " and DocumentMaster.DocumentUploadDate between '" + ViewState["periodstartdate"].ToString() + "' and '" + ViewState["periodenddate"].ToString() + "'";
        //    }
        //}
        //if (RadioButtonList1.SelectedValue == "1")
        //{
        //    if (txtfrom.Text != "" && txtto.Text != "")
        //    {
        //        strbydate = " and DocumentMaster.DocumentUploadDate between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
        //    }
        //}
        //if (txtsearch.Text != "")
        //{
        //    strsearch = " and DocumentMaster.DocumentTitle Like '%" + txtsearch.Text.Replace("'", "''") + "%' ";
        //}


        //if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue == "5")
        //{
        //    strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
        //}
        //if (ddlfilterofficeclerk.SelectedIndex == 0 && ddlofficestatus.SelectedValue != "5")
        //{
        //    strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
        //}
        //if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue != "5")
        //{
        //    strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
        //}


        //if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue == "5")
        //{
        //    strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
        //}
        //if (ddlsupervisorfilter.SelectedIndex == 0 && ddlsupervisorstatus.SelectedValue != "5")
        //{
        //    strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
        //}
        //if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue != "5")
        //{
        //    strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
        //}


        //string finalstr = strmaster + status + strsearch + strbyperiod + strbydate + strfilterbyofficeclerk + strfilterbysupervisor;

        SqlCommand cmdeeed = new SqlCommand(strmaster, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);


        DataView myDataView = new DataView();
        myDataView = dteeed.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        gridocapproval.DataSource = dteeed;
        gridocapproval.DataBind();




        if (gridocapproval.Rows.Count > 0)
        {
            imgbtnSubmit.Visible = true;
        }
        else
        {
            imgbtnSubmit.Visible = false;
        }

    }
    protected void imgbtnSubmit_Click(object sender, EventArgs e)
    { int flagpd = 0;
        lblmsg.Text = "";
        //if (ddldt.SelectedItem.Text == "Credit Invoice" || ddldt.SelectedItem.Text == "Cash Invoice" || ddldt.SelectedItem.Text == "Cash Voucher" || ddldt.SelectedItem.Text == "Credit Voucher")
        //{
        if (txtpartdocrefno.Text.Length > 0)
        {
            DataTable dtsc = select("select PartyDocrefno from DocumentMaster where  DocumentMaster.DocumentId<>'" + ddllistofdoc.SelectedValue + "' and CID='" + Session["Comid"] + "' and PartyDocrefno='" + txtpartdocrefno.Text + "'");
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
            string oldstatusid = "";
            string strmaster = "select   DocumentMaster.*,  DocumentProcessing.StatusId,DocumentProcessing.Levelofaccess,DocumentProcessing.ProcessingId   from DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join  EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where DocumentProcessing.EmployeeId = '" + ddlemp.SelectedValue + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "' and DocumentProcessing.DocumentId='" + ddllistofdoc.SelectedValue + "'  ";
            SqlCommand cgw = new SqlCommand(strmaster, con);
            SqlDataAdapter adgw = new SqlDataAdapter(cgw);
            DataTable dt = new DataTable();
            adgw.Fill(dt);


            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["StatusId"].ToString() != null)
                {
                    oldstatusid = dt.Rows[0]["StatusId"].ToString();
                }
                string levelofaccess = "";

                if (dt.Rows[0]["Levelofaccess"].ToString() != null)
                {
                    levelofaccess = dt.Rows[0]["Levelofaccess"].ToString();

                }




                if (ddlapprovalstatusforupdate.SelectedValue != "0" && ddlapprovalstatusforupdate.Enabled == true)
                {

                    if (levelofaccess == "1")
                    {

                        string strofficeclerk = " select * from  DocumentProcessing where DocumentId='" + ddllistofdoc.SelectedValue + "' and Levelofaccess='1'  and ProcessingId<>'" + dt.Rows[0]["ProcessingId"].ToString() + "'   ";
                        SqlCommand cmdofficeclerk = new SqlCommand(strofficeclerk, con);
                        cmdofficeclerk.CommandType = CommandType.Text;
                        SqlDataAdapter daofficeclerk = new SqlDataAdapter(cmdofficeclerk);
                        DataTable dtofficeclerk = new DataTable();
                        daofficeclerk.Fill(dtofficeclerk);

                        if (dtofficeclerk.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtofficeclerk.Rows)
                            {
                                string status = dr["StatusId"].ToString();
                                string masterid = dr["ProcessingId"].ToString();

                                if (ddlapprovalstatusforupdate.SelectedValue == "3")
                                {
                                    if (status != "3")
                                    {
                                        string str123office = " delete from  DocumentProcessing  where ProcessingId='" + masterid.ToString() + "'  ";
                                        SqlCommand cmd1123office = new SqlCommand(str123office, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmd1123office.ExecuteNonQuery();
                                        con.Close();
                                    }

                                }


                            }


                        }



                        int approve = 0;
                        if (ddlapprovalstatusforupdate.SelectedValue == "3")
                        {
                            approve = 1;
                        }
                        else
                        {
                            approve = 0;
                        }

                        string str1 = " update  DocumentProcessing set ApproveDate='" + System.DateTime.Now.ToShortDateString() + "',Note ='',StatusId='" + ddlapprovalstatusforupdate.SelectedValue + "' , Approve='" + approve + "' where ProcessingId='" + dt.Rows[0]["ProcessingId"].ToString() + "'  ";
                        SqlCommand cmd1 = new SqlCommand(str1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd1.ExecuteNonQuery();
                        con.Close();

                    }
                    if (levelofaccess == "2")
                    {

                        string strt = " select * from  DocumentProcessing where DocumentId='" + ddllistofdoc.SelectedValue + "' and (Levelofaccess='1' or Levelofaccess='2') and ProcessingId<>'" + dt.Rows[0]["ProcessingId"].ToString() + "'   ";
                        SqlCommand cmd1t = new SqlCommand(strt, con);
                        cmd1t.CommandType = CommandType.Text;
                        SqlDataAdapter dat = new SqlDataAdapter(cmd1t);
                        DataTable dtt = new DataTable();
                        dat.Fill(dtt);

                        if (dtt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtt.Rows)
                            {
                                string status = dr["StatusId"].ToString();
                                string masterid = dr["ProcessingId"].ToString();

                                if (ddlapprovalstatusforupdate.SelectedValue == "3")
                                {
                                    if (status != "3")
                                    {
                                        string str123 = " delete from  DocumentProcessing  where ProcessingId='" + masterid.ToString() + "'  ";
                                        SqlCommand cmd1123 = new SqlCommand(str123, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmd1123.ExecuteNonQuery();
                                        con.Close();
                                    }

                                }
                                else if (ddlapprovalstatusforupdate.SelectedValue == "2")
                                {


                                    string str12345 = " Update  DocumentProcessing set  StatusId='1' ,Approve='0' where ProcessingId='" + masterid.ToString() + "' and Levelofaccess<>'2' ";
                                    SqlCommand cmd12345 = new SqlCommand(str12345, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd12345.ExecuteNonQuery();
                                    con.Close();

                                }

                            }


                        }
                        int approve = 0;
                        if (ddlapprovalstatusforupdate.SelectedValue == "3")
                        {
                            approve = 1;
                        }
                        else
                        {
                            approve = 0;
                        }
                        string str167 = " update  DocumentProcessing set ApproveDate='" + System.DateTime.Now.ToShortDateString() + "',Note ='',StatusId='" + ddlapprovalstatusforupdate.SelectedValue + "' , Approve='" + approve + "' where ProcessingId='" + dt.Rows[0]["ProcessingId"].ToString() + "'  ";
                        SqlCommand cmd167 = new SqlCommand(str167, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd167.ExecuteNonQuery();
                        con.Close();

                    }

                    if (levelofaccess == "3")
                    {

                        string strt4565 = " select * from  DocumentProcessing where DocumentId='" + ddllistofdoc.SelectedValue + "' and ( Levelofaccess='1' or Levelofaccess='2' or Levelofaccess='3')   and ProcessingId<>'" + dt.Rows[0]["ProcessingId"].ToString() + "'   ";
                        SqlCommand cmd1t4565 = new SqlCommand(strt4565, con);
                        cmd1t4565.CommandType = CommandType.Text;
                        SqlDataAdapter dat = new SqlDataAdapter(cmd1t4565);
                        DataTable dtt = new DataTable();
                        dat.Fill(dtt);

                        if (dtt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtt.Rows)
                            {
                                string status = dr["StatusId"].ToString();
                                string masterid = dr["ProcessingId"].ToString();

                                if (ddlapprovalstatusforupdate.SelectedValue == "3")
                                {
                                    if (status != "3")
                                    {
                                        string str12586 = " delete from  DocumentProcessing  where ProcessingId='" + masterid.ToString() + "'  ";
                                        SqlCommand cmd12586 = new SqlCommand(str12586, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmd12586.ExecuteNonQuery();
                                        con.Close();
                                    }

                                }
                                else if (ddlapprovalstatusforupdate.SelectedValue == "2")
                                {


                                    string str1789 = " Update  DocumentProcessing set  StatusId='1' ,Approve='0' where ProcessingId='" + masterid.ToString() + "' and Levelofaccess<>'3' ";
                                    SqlCommand cmd1789 = new SqlCommand(str1789, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd1789.ExecuteNonQuery();
                                    con.Close();

                                }

                            }


                        }
                        int approve = 0;
                        if (ddlapprovalstatusforupdate.SelectedValue == "3")
                        {
                            approve = 1;
                        }
                        else
                        {
                            approve = 0;
                        }
                        string str1010 = " update  DocumentProcessing set ApproveDate='" + System.DateTime.Now.ToShortDateString() + "',Note ='',StatusId='" + ddlapprovalstatusforupdate.SelectedValue + "' , Approve='" + approve + "' where ProcessingId='" + dt.Rows[0]["ProcessingId"].ToString() + "'  ";
                        SqlCommand cmd1010 = new SqlCommand(str1010, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd1010.ExecuteNonQuery();
                        con.Close();

                    }

                    lblmsg.Visible = true;
                    lblmsg.Text = "Document approved successfully.";


                }


            }
            Button2_Click(sender, e);

            if (oldstatusid == ddlapprovalstatusforupdate.SelectedValue)
            {
                next();

            }
            else
            {
                fillgrid();
                ddldocpreviousnext();
            }





        }

    }

    protected void fillupdatepanel()
    {
        lblmsg.Text = "";
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
            fillsubtype();
            ddldocsubtypename.SelectedIndex = ddldocsubtypename.Items.IndexOf(ddldocsubtypename.Items.FindByValue(dtedit.Rows[0]["DocumentSubTypeId"].ToString()));
            filldll();
            ddlDocType.SelectedIndex = ddlDocType.Items.IndexOf(ddlDocType.Items.FindByValue(dtedit.Rows[0]["DocumentTypeId"].ToString()));

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
            txtpartdocrefno.Text = Convert.ToString(dtedit.Rows[0]["PartyDocrefno"]);
     
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
    protected void ddlPartyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillParty();

    }
    protected void fillmaintaype()
    {
        ddldocmaintype.Items.Clear();

        string doc = "SELECT DocumentMainTypeId,DocumentMainType as DocumentMainType FROM  [dbo].[DocumentMainType]   where CID='" + Session["comid"] + "' and Whid='" + ddlbusnessmaster.SelectedValue + "'";
        SqlDataAdapter adp = new SqlDataAdapter(doc, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        ddldocmaintype.DataSource = dt;
        ddldocmaintype.DataTextField = "DocumentMainType";
        ddldocmaintype.DataValueField = "DocumentMainTypeId";
        ddldocmaintype.DataBind();



    }
    protected void fillsubtype()
    {
        ddldocsubtypename.Items.Clear();
        string str178 = " SELECT     DocumentSubType.DocumentSubTypeId, DocumentSubType.DocumentSubType, DocumentMainType.DocumentMainTypeId as DocumentMainTypeId,  DocumentMainType.DocumentMainType FROM         DocumentMainType RIGHT OUTER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId WHERE     (DocumentMainType.DocumentMainTypeId = '" + ddldocmaintype.SelectedValue + "') and DocumentMainType.CID='" + Session["Comid"] + "' ";
        SqlCommand cgw = new SqlCommand(str178, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);
        ddldocsubtypename.DataSource = dt;
        ddldocsubtypename.DataTextField = "DocumentSubType";
        ddldocsubtypename.DataValueField = "DocumentSubTypeId";
        ddldocsubtypename.DataBind();

    }
    public void filldll()
    {
        ddlDocType.Items.Clear();
        string str178 = "SELECT    DocumentType.DocumentType, DocumentType.DocumentTypeId FROM         DocumentMainType INNER JOIN   DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId INNER JOIN   DocumentType ON DocumentSubType.DocumentSubTypeId = DocumentType.DocumentSubTypeId where DocumentMainType.CID='" + Session["Comid"] + "' and DocumentMainType.Whid='" + ddlbusnessmaster.SelectedValue + "' and DocumentType.DocumentSubTypeId='" + ddldocsubtypename.SelectedValue + "'  order by DocumentType.DocumentType ";
        SqlCommand cgw = new SqlCommand(str178, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);

        ddlDocType.DataSource = dt;
        ddlDocType.DataTextField = "DocumentType";
        ddlDocType.DataValueField = "DocumentTypeId";
        ddlDocType.DataBind();


    }
    protected void ddldocmaintype_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillsubtype();
        filldll();
    }
    protected void ddldocsubtypename_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldll();
    }
    protected void Approvalstatusselection()
    {
        string strmaster = "select   DocumentMaster.*,  DocumentProcessing.StatusId,DocumentProcessing.Levelofaccess   from DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join  EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where DocumentProcessing.EmployeeId = '" + ddlemp.SelectedValue + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "' and DocumentProcessing.DocumentId='" + ddllistofdoc.SelectedValue + "'  ";
        SqlCommand cgw = new SqlCommand(strmaster, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);


        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["StatusId"].ToString() != null)
            {
                ddlapprovalstatusforupdate.SelectedIndex = ddlapprovalstatusforupdate.Items.IndexOf(ddlapprovalstatusforupdate.Items.FindByValue(dt.Rows[0]["StatusId"].ToString()));
            }
            string levelofaccess = "";
            if (dt.Rows[0]["Levelofaccess"].ToString() != null)
            {
                levelofaccess = dt.Rows[0]["Levelofaccess"].ToString();

            }

            if (levelofaccess == "1")
            {
                if (ddlapprovalstatusforupdate.SelectedValue == "3" || ddlapprovalstatusforupdate.SelectedValue == "2")
                {
                    ddlapprovalstatusforupdate.Enabled = false;

                }
                else
                {
                    ddlapprovalstatusforupdate.Enabled = true;

                }
            }

            if (levelofaccess == "2")
            {

                string strt = " select * from  DocumentProcessing where DocumentId='" + ddllistofdoc.SelectedValue + "' and  Levelofaccess='3' ";
                SqlCommand cmd1t = new SqlCommand(strt, con);
                cmd1t.CommandType = CommandType.Text;
                SqlDataAdapter dat = new SqlDataAdapter(cmd1t);
                DataTable dtt = new DataTable();
                dat.Fill(dtt);

                if (dtt.Rows.Count > 0)
                {

                    int flag = 0;
                    foreach (DataRow dr in dtt.Rows)
                    {
                        string status = dr["StatusId"].ToString();

                        if (status == "3")
                        {
                            flag = 1;

                        }


                    }
                    if (flag == 1)
                    {
                        ddlapprovalstatusforupdate.Enabled = false;


                    }
                    else
                    {
                        ddlapprovalstatusforupdate.Enabled = true;

                    }

                }
                else
                {
                    ddlapprovalstatusforupdate.Enabled = true;

                }

            }






        }

    }

    protected void ddldocpreviousnext()
    {
        Session["no"] = "1";
        Session["no1"] = "1";

        string strmaster = "select   DocumentMaster.*, DocumentType.DocumentType, Party_Master.Compname as PartyName,EmployeeMaster.EmployeeName, DocumentProcessing.ProcessingId as DocumentProcessingId,DocumentProcessing.ProcessingId ,DocumentProcessing.DocAllocateDate,DocumentProcessing.ApproveDate, DocumentProcessing.Approve, DocumentProcessing.Note, DocumentProcessing.StatusId,DocumentProcessing.Levelofaccess,DesignationMaster.DesignationName,DesignationMaster.DesignationMasterID as DesignationID , EmployeeMaster.EmployeeMasterID as EmployeeID ,CAST(DocumentMaster.DocumentId as Nvarchar(50))+' : '+ DocumentMaster.DocumentTitle +' : '+case when (DocumentProcessing.StatusId='0') then 'Pending-New' else  (case when (DocumentProcessing.StatusId='1') then 'Pending-Returned' else  (case when (DocumentProcessing.StatusId='2') then 'Rejected'  else (case when (DocumentProcessing.StatusId='3') then 'Approved'  else  '' End ) End   ) End )  End  as DocumentNameMaster  from DocumentMainType inner join     DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster on  DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId inner join DocumentProcessing on  DocumentProcessing.DocumentId= DocumentMaster.DocumentId inner join  EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where  DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and DocumentProcessing.EmployeeId = '" + ddlemp.SelectedValue + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "'  ";
        string status = "";
        string strbyperiod = "";
        string strbydate = "";
        string strsearch = "";
        string strfilterbyofficeclerk = "";
        string strfilterbysupervisor = "";

        if (ddlapproval.SelectedValue == "5")
        {
            status = " AND (DocumentProcessing.StatusId='0' or DocumentProcessing.StatusId='1') ";

        }
        else
        {
            status = " AND DocumentProcessing.StatusId='" + ddlapproval.SelectedValue + "' ";
        }




        if (RadioButtonList1.SelectedValue == "0")
        {

            if (ViewState["periodstartdate"] != null && ViewState["periodenddate"] != null)
            {
                strbyperiod = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + ViewState["periodstartdate"].ToString() + "' and '" + ViewState["periodenddate"].ToString() + "'";
            }
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtfrom.Text != "" && txtto.Text != "")
            {
                strbydate = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
            }
        }

        if (chkfilteronapprovalstatus.Checked == true)
        {

            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue == "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex == 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }


            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue == "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex == 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }

        }
        string finalstr = strmaster + status + strsearch + strbyperiod + strbydate + strfilterbyofficeclerk + strfilterbysupervisor;

        SqlCommand cmdeeed = new SqlCommand(finalstr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);


        if (dteeed.Rows.Count > 0)
        {
            ViewState["docid"] = dteeed.Rows[0]["DocumentId"];
            txtdocno.Text = "1";
            lblofno.Text = dteeed.Rows.Count.ToString();
        }
        else
        {
            txtdocno.Text = "0";
            lblofno.Text = "0";
        }

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


            //str111e = "  Select Row_NUMBER()OVER(Order by (Select 0)) as rt, DocumentId as DocumentId from  [DocumentMaster] where DocumentTypeId='" + ddldoctype.SelectedValue + "' order by DocumentId ";

            string strmaster = "select  Row_NUMBER()OVER(Order by (Select 0)) as rt, DocumentMaster.*, DocumentType.DocumentType, Party_Master.Compname as PartyName,EmployeeMaster.EmployeeName, DocumentProcessing.ProcessingId as DocumentProcessingId,DocumentProcessing.ProcessingId ,DocumentProcessing.DocAllocateDate,DocumentProcessing.ApproveDate, DocumentProcessing.Approve, DocumentProcessing.Note, DocumentProcessing.StatusId,DocumentProcessing.Levelofaccess,DesignationMaster.DesignationName,DesignationMaster.DesignationMasterID as DesignationID , EmployeeMaster.EmployeeMasterID as EmployeeID ,CAST(DocumentMaster.DocumentId as Nvarchar(50))+' : '+ DocumentMaster.DocumentTitle +' : '+case when (DocumentProcessing.StatusId='0') then 'Pending-New' else  (case when (DocumentProcessing.StatusId='1') then 'Pending-Returned' else  (case when (DocumentProcessing.StatusId='2') then 'Rejected'  else (case when (DocumentProcessing.StatusId='3') then 'Approved'  else  '' End ) End   ) End )  End  as DocumentNameMaster  from DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join  EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where DocumentProcessing.EmployeeId = '" + ddlemp.SelectedValue + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "'  ";
            string status = "";
            string strbyperiod = "";
            string strbydate = "";
            string strsearch = "";
            string strfilterbyofficeclerk = "";
            string strfilterbysupervisor = "";

            if (ddlapproval.SelectedValue == "5")
            {
                status = " AND (DocumentProcessing.StatusId='0' or DocumentProcessing.StatusId='1') ";

            }
            else
            {
                status = " AND DocumentProcessing.StatusId='" + ddlapproval.SelectedValue + "' ";
            }




            if (RadioButtonList1.SelectedValue == "0")
            {

                if (ViewState["periodstartdate"] != null && ViewState["periodenddate"] != null)
                {
                    strbyperiod = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + ViewState["periodstartdate"].ToString() + "' and '" + ViewState["periodenddate"].ToString() + "'";
                }
            }
            if (RadioButtonList1.SelectedValue == "1")
            {
                if (txtfrom.Text != "" && txtto.Text != "")
                {
                    strbydate = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
                }
            }

            if (chkfilteronapprovalstatus.Checked == true)
            {


                if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue == "5")
                {
                    strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
                }
                if (ddlfilterofficeclerk.SelectedIndex == 0 && ddlofficestatus.SelectedValue != "5")
                {
                    strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
                }
                if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue != "5")
                {
                    strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
                }


                if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue == "5")
                {
                    strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
                }
                if (ddlsupervisorfilter.SelectedIndex == 0 && ddlsupervisorstatus.SelectedValue != "5")
                {
                    strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
                }
                if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue != "5")
                {
                    strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
                }
            }

            string finalstr = strmaster + status + strsearch + strbyperiod + strbydate + strfilterbyofficeclerk + strfilterbysupervisor;


            SqlCommand cmd11e = new SqlCommand(finalstr);
            cmd11e.Connection = con;
            SqlDataAdapter da11e = new SqlDataAdapter(cmd11e);
            DataTable dts1e = new DataTable();
            da11e.Fill(dts1e);


            if (dts1e.Rows.Count > 0)
            {

                cc = Convert.ToInt32(txtdocno.Text);
                if (Convert.ToInt32(lblofno.Text) >= (cc))
                {

                    str111 = "  Select Row_NUMBER()OVER(Order by (Select 0)) as rt, DocumentId as DocumentId from  [DocumentMaster] where DocumentId='" + dts1e.Rows[cc - 1]["DocumentId"] + "'   order by DocumentId ";


                    txtdocno.Text = (Convert.ToInt32(txtdocno.Text)).ToString();
                }
                else
                {
                    cc = 1;
                    str111 = "  Select Row_NUMBER()OVER(Order by (Select 0)) as rt, DocumentId as DocumentId from  [DocumentMaster] where DocumentId='" + dts1e.Rows[0]["DocumentId"] + "'   order by DocumentId ";
                    txtdocno.Text = (1).ToString();
                }
                SqlCommand cmd11 = new SqlCommand(str111);
                cmd11.Connection = con;
                SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                DataTable dts1 = new DataTable();
                da11.Fill(dts1);
                if (dts1.Rows.Count > 0)
                {
                    ViewState["docid"] = dts1.Rows[0]["DocumentId"];

                    DocId = Convert.ToInt32(ViewState["docid"]);



                    LoadPdf(DocId);
                            ddllistofdoc.SelectedIndex = ddllistofdoc.Items.IndexOf(ddllistofdoc.Items.FindByValue(DocId.ToString()));
                    //fillupdatepanel();
                    defaultviewswitch();
                    Approvalstatusselection();

                }
            }
        }
    }
    protected void ibtnFirst_Click(object sender, ImageClickEventArgs e)
    {



        Int32 DocId;
        IbtnNext.Enabled = false;
        DocId = 0;
        string strmaster = "select  Min(DocumentProcessing.DocumentId) as DocumentId  from DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join  EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where DocumentProcessing.EmployeeId = '" + ddlemp.SelectedValue + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "'  ";
        string status = "";
        string strbyperiod = "";
        string strbydate = "";
        string strsearch = "";
        string strfilterbyofficeclerk = "";
        string strfilterbysupervisor = "";

        if (ddlapproval.SelectedValue == "5")
        {
            status = " AND (DocumentProcessing.StatusId='0' or DocumentProcessing.StatusId='1') ";

        }
        else
        {
            status = " AND DocumentProcessing.StatusId='" + ddlapproval.SelectedValue + "' ";
        }




        if (RadioButtonList1.SelectedValue == "0")
        {

            if (ViewState["periodstartdate"] != null && ViewState["periodenddate"] != null)
            {
                strbyperiod = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + ViewState["periodstartdate"].ToString() + "' and '" + ViewState["periodenddate"].ToString() + "'";
            }
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtfrom.Text != "" && txtto.Text != "")
            {
                strbydate = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
            }
        }

        if (chkfilteronapprovalstatus.Checked == true)
        {

            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue == "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex == 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }


            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue == "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex == 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
        }

        string finalstr = strmaster + status + strsearch + strbyperiod + strbydate + strfilterbyofficeclerk + strfilterbysupervisor;


        SqlCommand cmd11e = new SqlCommand(finalstr);
        cmd11e.Connection = con;
        SqlDataAdapter da11e = new SqlDataAdapter(cmd11e);
        DataTable dts1e = new DataTable();
        da11e.Fill(dts1e);



        if (dts1e.Rows.Count > 0)
        {
            ViewState["docid"] = dts1e.Rows[0]["DocumentId"];

            DocId = Convert.ToInt32(ViewState["docid"]);

            LoadPdf(DocId);

            txtdocno.Text = "1";

            ddllistofdoc.SelectedIndex = ddllistofdoc.Items.IndexOf(ddllistofdoc.Items.FindByValue(DocId.ToString()));
            //fillupdatepanel();
            defaultviewswitch();
            Approvalstatusselection();
        }

        ibtnFirst.Enabled = false;
        IbtnNext.Enabled = true;
        IbtnPrev.Enabled = false;
        IbtnLast.Enabled = true;
    }
    protected void IbtnLast_Click(object sender, ImageClickEventArgs e)
    {
        ibtnFirst.Enabled = true;
        IbtnNext.Enabled = false;
        IbtnPrev.Enabled = true;
        IbtnLast.Enabled = false;


        Int32 DocId;
        IbtnNext.Enabled = false;
        DocId = 0;
        string strmaster = "select  Max(DocumentProcessing.DocumentId) as DocumentId  from DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join  EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where DocumentProcessing.EmployeeId = '" + ddlemp.SelectedValue + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "'  ";
        string status = "";
        string strbyperiod = "";
        string strbydate = "";
        string strsearch = "";
        string strfilterbyofficeclerk = "";
        string strfilterbysupervisor = "";

        if (ddlapproval.SelectedValue == "5")
        {
            status = " AND (DocumentProcessing.StatusId='0' or DocumentProcessing.StatusId='1') ";

        }
        else
        {
            status = " AND DocumentProcessing.StatusId='" + ddlapproval.SelectedValue + "' ";
        }



        if (RadioButtonList1.SelectedValue == "0")
        {

            if (ViewState["periodstartdate"] != null && ViewState["periodenddate"] != null)
            {
                strbyperiod = " andCast(DocumentMaster.DocumentUploadDate as Date) between '" + ViewState["periodstartdate"].ToString() + "' and '" + ViewState["periodenddate"].ToString() + "'";
            }
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtfrom.Text != "" && txtto.Text != "")
            {
                strbydate = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
            }
        }

        if (chkfilteronapprovalstatus.Checked == true)
        {

            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue == "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex == 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }


            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue == "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex == 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
        }

        string finalstr = strmaster + status + strsearch + strbyperiod + strbydate + strfilterbyofficeclerk + strfilterbysupervisor;


        SqlCommand cmd11e = new SqlCommand(finalstr);
        cmd11e.Connection = con;
        SqlDataAdapter da11e = new SqlDataAdapter(cmd11e);
        DataTable dts1e = new DataTable();
        da11e.Fill(dts1e);



        if (dts1e.Rows.Count > 0)
        {
            ViewState["docid"] = dts1e.Rows[0]["DocumentId"];

            DocId = Convert.ToInt32(ViewState["docid"]);

            LoadPdf(DocId);

            txtdocno.Text = lblofno.Text;

            ddllistofdoc.SelectedIndex = ddllistofdoc.Items.IndexOf(ddllistofdoc.Items.FindByValue(DocId.ToString()));
            //fillupdatepanel();
            defaultviewswitch();
            Approvalstatusselection();
        }

    }
    protected void IbtnPrev_Click(object sender, ImageClickEventArgs e)
    {
        previous();
    }
    protected void previous()
    {



        Int32 DocId;
        DocId = 0;
        DataTable dt = new DataTable();


        string strmaster = "select  DocumentProcessing.DocumentId   from DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join  EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where DocumentProcessing.EmployeeId = '" + ddlemp.SelectedValue + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "' and  DocumentProcessing.DocumentId <('" + ViewState["docid"] + "')  ";
        string status = "";
        string strbyperiod = "";
        string strbydate = "";
        string strsearch = "";
        string strfilterbyofficeclerk = "";
        string strfilterbysupervisor = "";

        if (ddlapproval.SelectedValue == "5")
        {
            status = " AND (DocumentProcessing.StatusId='0' or DocumentProcessing.StatusId='1') ";

        }
        else
        {
            status = " AND DocumentProcessing.StatusId='" + ddlapproval.SelectedValue + "' ";
        }




        if (RadioButtonList1.SelectedValue == "0")
        {

            if (ViewState["periodstartdate"] != null && ViewState["periodenddate"] != null)
            {
                strbyperiod = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + ViewState["periodstartdate"].ToString() + "' and '" + ViewState["periodenddate"].ToString() + "'";
            }
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtfrom.Text != "" && txtto.Text != "")
            {
                strbydate = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
            }
        }

        if (chkfilteronapprovalstatus.Checked == true)
        {

            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue == "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex == 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }


            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue == "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex == 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
        }

        string orderby = "order by DocumentId Desc";
        string finalstr = strmaster + status + strsearch + strbyperiod + strbydate + strfilterbyofficeclerk + strfilterbysupervisor + orderby;


        SqlCommand cmd11e = new SqlCommand(finalstr);
        cmd11e.Connection = con;
        SqlDataAdapter da11e = new SqlDataAdapter(cmd11e);
        DataTable dts1e = new DataTable();
        da11e.Fill(dts1e);


        if (dts1e.Rows.Count > 0)
        {
            ViewState["docid"] = dts1e.Rows[0]["DocumentId"];

            DocId = Convert.ToInt32(ViewState["docid"]);

            LoadPdf(DocId);
            txtdocno.Text = (Convert.ToInt32(txtdocno.Text) - 1).ToString();

            ddllistofdoc.SelectedIndex = ddllistofdoc.Items.IndexOf(ddllistofdoc.Items.FindByValue(DocId.ToString()));
            //fillupdatepanel();
            defaultviewswitch();
            Approvalstatusselection();
        }

        ibtnFirst.Enabled = true;
        IbtnNext.Enabled = true;
        IbtnPrev.Enabled = true;
        IbtnLast.Enabled = true;
    }
    protected void IbtnNext_Click(object sender, ImageClickEventArgs e)
    {
        next();

    }

    protected void next()
    {
        Int32 DocId;
        DocId = 0;
        DataTable dt = new DataTable();

        string strmaster = "select  DocumentProcessing.DocumentId  from DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join  EmployeeMaster ON DocumentProcessing.EmployeeId = EmployeeMaster.EmployeeMasterID inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID left outer join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId where DocumentProcessing.EmployeeId = '" + ddlemp.SelectedValue + "' and DocumentProcessing.CID='" + Session["Comid"].ToString() + "'  and DocumentProcessing.DocumentId>('" + ViewState["docid"] + "') ";
        string status = "";
        string strbyperiod = "";
        string strbydate = "";
        string strsearch = "";
        string strfilterbyofficeclerk = "";
        string strfilterbysupervisor = "";

        if (ddlapproval.SelectedValue == "5")
        {
            status = " AND (DocumentProcessing.StatusId='0' or DocumentProcessing.StatusId='1') ";

        }
        else
        {
            status = " AND DocumentProcessing.StatusId='" + ddlapproval.SelectedValue + "' ";
        }




        if (RadioButtonList1.SelectedValue == "0")
        {

            if (ViewState["periodstartdate"] != null && ViewState["periodenddate"] != null)
            {
                strbyperiod = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + ViewState["periodstartdate"].ToString() + "' and '" + ViewState["periodenddate"].ToString() + "'";
            }
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtfrom.Text != "" && txtto.Text != "")
            {
                strbydate = " and Cast(DocumentMaster.DocumentUploadDate as Date) between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
            }
        }

        if (chkfilteronapprovalstatus.Checked == true)
        {

            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue == "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex == 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }
            if (ddlfilterofficeclerk.SelectedIndex > 0 && ddlofficestatus.SelectedValue != "5")
            {
                strfilterbyofficeclerk = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlofficestatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlfilterofficeclerk.SelectedValue + "' and DocumentProcessing.Levelofaccess='1' )";
            }


            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue == "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex == 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from  DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
            if (ddlsupervisorfilter.SelectedIndex > 0 && ddlsupervisorstatus.SelectedValue != "5")
            {
                strfilterbysupervisor = " and DocumentProcessing.DocumentId in (select DocumentProcessing.DocumentId from DocumentProcessing where DocumentProcessing.StatusId='" + ddlsupervisorstatus.SelectedValue + "' and DocumentProcessing.EmployeeId='" + ddlsupervisorfilter.SelectedValue + "' and DocumentProcessing.Levelofaccess='2' )";
            }
        }

        string finalstr = strmaster + status + strsearch + strbyperiod + strbydate + strfilterbyofficeclerk + strfilterbysupervisor;


        SqlCommand cmd11e = new SqlCommand(finalstr);
        cmd11e.Connection = con;
        SqlDataAdapter da11e = new SqlDataAdapter(cmd11e);
        DataTable dts1e = new DataTable();
        da11e.Fill(dts1e);

        if (dts1e.Rows.Count > 0)
        {
            ViewState["docid"] = dts1e.Rows[0]["DocumentId"];
            DocId = Convert.ToInt32(ViewState["docid"]);
            LoadPdf(DocId);
            txtdocno.Text = (Convert.ToInt32(txtdocno.Text) + 1).ToString();

            ddllistofdoc.SelectedIndex = ddllistofdoc.Items.IndexOf(ddllistofdoc.Items.FindByValue(DocId.ToString()));
            //fillupdatepanel();
            defaultviewswitch();

            Approvalstatusselection();


        }

        ibtnFirst.Enabled = true;
        IbtnNext.Enabled = true;
        IbtnPrev.Enabled = true;
        IbtnLast.Enabled = true;
    }
    protected void chkfilteronapprovalstatus_CheckedChanged(object sender, EventArgs e)
    {
        if (chkfilteronapprovalstatus.Checked == true)
        {
            Panel3.Visible = true;

        }
        else
        {
            Panel3.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();
        ddldocpreviousnext();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        next();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        string doc = "SELECT  * from EmployeeDefaultDocumentDetail where Whid='" + ddlbusiness.SelectedValue + "' and EmployeeId='" + ddlemp.SelectedValue + "' ";
        SqlDataAdapter adp = new SqlDataAdapter(doc, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {

            TextBox1.Text = dt.Rows[0]["Title"].ToString();

            CheckBox2.Checked = Convert.ToBoolean(dt.Rows[0]["DaynamicDate"].ToString());
            CheckBox3.Checked = Convert.ToBoolean(dt.Rows[0]["DynamicParty"].ToString());


            fillpopuppartytype();

            ddlpopupusertype.SelectedIndex = ddlpopupusertype.Items.IndexOf(ddlpopupusertype.Items.FindByValue(dt.Rows[0]["PartyTypeId"].ToString()));

            FillpopupParty();
            ddlpopuppartyname.SelectedIndex = ddlpopuppartyname.Items.IndexOf(ddlpopuppartyname.Items.FindByValue(dt.Rows[0]["PartyId"].ToString()));

            fillpopupmaintaype();
            ddlpopupcabinet.SelectedIndex = ddlpopupcabinet.Items.IndexOf(ddlpopupcabinet.Items.FindByValue(dt.Rows[0]["CabinetId"].ToString()));

            fillpopupsubtype();
            ddlpopupdrawer.SelectedIndex = ddlpopupdrawer.Items.IndexOf(ddlpopupdrawer.Items.FindByValue(dt.Rows[0]["DrawerId"].ToString()));

            fillpopupdll();
            ddlpopupfolder.SelectedIndex = ddlpopupfolder.Items.IndexOf(ddlpopupfolder.Items.FindByValue(dt.Rows[0]["FolderId"].ToString()));

            chkpopupstatus.Checked = Convert.ToBoolean(dt.Rows[0]["Status"].ToString());
        }
        Label56.Text = "";
        ModalPopupExtender3.Show();

    }

    protected void fillpopuppartytype()
    {
        string qryStr = "select * from PartytTypeMaster  where  compid='" + Session["Comid"] + "'  order by PartType";
        SqlCommand cmdeeed = new SqlCommand(qryStr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);


        ddlpopupusertype.DataSource = dteeed;
        ddlpopupusertype.DataTextField = "PartType";
        ddlpopupusertype.DataValueField = "PartyTypeId";
        ddlpopupusertype.DataBind();

        ddlpopupusertype.Items.Insert(0, "-Select-");
        ddlpopupusertype.Items[0].Value = "0";

    }

    protected void FillpopupParty()
    {
        ddlpopuppartyname.Items.Clear();
        string qryStr = "";

        if (ddlpopupusertype.SelectedItem.Text == "Employee")
        {
            qryStr = "select Party_master.PartyID,DepartmentmasterMNC.Departmentname +':'+ DesignationMaster.DesignationName +':'+ EmployeeMaster.EmployeeName as PartyName  from EmployeeMaster inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId inner join DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId WHERE   Party_master.Whid='" + ddlbusiness.SelectedValue + "' and Party_master.PartyTypeId='" + ddlpopupusertype.SelectedValue + "' order by PartyName ";
        }
        else if (ddlpopupusertype.SelectedItem.Text == "Candidate")
        {
            qryStr = " select Party_master.PartyID, CandidateMaster.LastName +' '+ CandidateMaster.FirstName +' '+ CandidateMaster.MiddleName as PartyName  from CandidateMaster inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId WHERE   Party_master.Whid='" + ddlbusiness.SelectedValue + "' and Party_master.PartyTypeId='" + ddlpopupusertype.SelectedValue + "' order by PartyName  ";
        }
        else
        {
            qryStr = "SELECT  PartyId, Party_Master.PartyTypeId, Party_Master.Compname + ':'+ Party_Master.ContactPerson as PartyName, ContactPerson FROM   Party_master inner JOIN    PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId WHERE   Party_master.Whid='" + ddlbusiness.SelectedValue + "' and Party_master.PartyTypeId='" + ddlpopupusertype.SelectedValue + "' order by PartyName  ";
        }



        SqlCommand cmdeeed = new SqlCommand(qryStr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        ddlpopuppartyname.DataSource = dteeed;
        ddlpopuppartyname.DataTextField = "PartyName";
        ddlpopuppartyname.DataValueField = "PartyId";
        ddlpopuppartyname.DataBind();
        ddlpopuppartyname.Items.Insert(0, "-Select-");
        ddlpopuppartyname.Items[0].Value = "0";


    }

    protected void fillpopupmaintaype()
    {
        ddlpopupcabinet.Items.Clear();

        string doc = "SELECT DocumentMainTypeId,DocumentMainType as DocumentMainType FROM  [dbo].[DocumentMainType]   where CID='" + Session["comid"] + "' and Whid='" + ddlbusiness.SelectedValue + "'";
        SqlDataAdapter adp = new SqlDataAdapter(doc, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        ddlpopupcabinet.DataSource = dt;
        ddlpopupcabinet.DataTextField = "DocumentMainType";
        ddlpopupcabinet.DataValueField = "DocumentMainTypeId";
        ddlpopupcabinet.DataBind();

        ddlpopupcabinet.Items.Insert(0, "-Select-");
        ddlpopupcabinet.Items[0].Value = "0";



    }

    protected void fillpopupsubtype()
    {
        ddlpopupdrawer.Items.Clear();
        string str178 = " SELECT     DocumentSubType.DocumentSubTypeId, DocumentSubType.DocumentSubType, DocumentMainType.DocumentMainTypeId as DocumentMainTypeId,  DocumentMainType.DocumentMainType FROM         DocumentMainType RIGHT OUTER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId WHERE     (DocumentMainType.DocumentMainTypeId = '" + ddlpopupcabinet.SelectedValue + "') and DocumentMainType.CID='" + Session["Comid"] + "' ";
        SqlCommand cgw = new SqlCommand(str178, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);
        ddlpopupdrawer.DataSource = dt;
        ddlpopupdrawer.DataTextField = "DocumentSubType";
        ddlpopupdrawer.DataValueField = "DocumentSubTypeId";
        ddlpopupdrawer.DataBind();

        ddlpopupdrawer.Items.Insert(0, "-Select-");
        ddlpopupdrawer.Items[0].Value = "0";

    }

    protected void fillpopupdll()
    {
        ddlpopupfolder.Items.Clear();
        string str178 = "SELECT    DocumentType.DocumentType, DocumentType.DocumentTypeId FROM         DocumentMainType INNER JOIN   DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId INNER JOIN   DocumentType ON DocumentSubType.DocumentSubTypeId = DocumentType.DocumentSubTypeId where DocumentMainType.CID='" + Session["Comid"] + "' and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and DocumentType.DocumentSubTypeId='" + ddlpopupdrawer.SelectedValue + "'  order by DocumentType.DocumentType ";
        SqlCommand cgw = new SqlCommand(str178, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);

        ddlpopupfolder.DataSource = dt;
        ddlpopupfolder.DataTextField = "DocumentType";
        ddlpopupfolder.DataValueField = "DocumentTypeId";
        ddlpopupfolder.DataBind();

        ddlpopupfolder.Items.Insert(0, "-Select-");
        ddlpopupfolder.Items[0].Value = "0";



    }
    protected void ddlpopupusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillpopupParty();
        ModalPopupExtender3.Show();
    }
    protected void ddlpopupcabinet_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpopupsubtype();
        fillpopupdll();
        ModalPopupExtender3.Show();
    }
    protected void ddlpopupdrawer_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpopupdll();
        ModalPopupExtender3.Show();
    }
    protected void Button7_Click(object sender, EventArgs e)
    {

        string doc = "SELECT  * from EmployeeDefaultDocumentDetail where Whid='" + ddlbusiness.SelectedValue + "' and EmployeeId='" + ddlemp.SelectedValue + "' ";
        SqlDataAdapter adp = new SqlDataAdapter(doc, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {


            string str12345 = " update EmployeeDefaultDocumentDetail set Title='" + TextBox1.Text + "',DaynamicDate='" + CheckBox2.Checked + "',DynamicParty='" + CheckBox3.Checked + "',PartyTypeId='" + ddlpopupusertype.SelectedValue + "',PartyId='" + ddlpopuppartyname.SelectedValue + "',CabinetId='" + ddlpopupcabinet.SelectedValue + "',DrawerId='" + ddlpopupdrawer.SelectedValue + "',FolderId='" + ddlpopupfolder.SelectedValue + "',Status='" + chkpopupstatus.Checked + "' where Whid='" + ddlbusiness.SelectedValue + "' and EmployeeId='" + ddlemp.SelectedValue + "' ";
            SqlCommand cmd12345 = new SqlCommand(str12345, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd12345.ExecuteNonQuery();
            con.Close();


        }
        else
        {
            string str12345 = " Insert Into EmployeeDefaultDocumentDetail values('" + ddlbusiness.SelectedValue + "','" + ddlemp.SelectedValue + "','" + TextBox1.Text + "','" + CheckBox2.Checked + "','" + CheckBox3.Checked + "','" + ddlpopupusertype.SelectedValue + "','" + ddlpopuppartyname.SelectedValue + "','" + ddlpopupcabinet.SelectedValue + "','" + ddlpopupdrawer.SelectedValue + "','" + ddlpopupfolder.SelectedValue + "','" + chkpopupstatus.Checked + "')  ";
            SqlCommand cmd12345 = new SqlCommand(str12345, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd12345.ExecuteNonQuery();
            con.Close();


        }
        Label56.Visible = true;
        Label56.Text = "Record inserted successfully";

        ModalPopupExtender3.Show();
    }

    protected void defaultviewswitch()
    {
        ddlbusnessmaster.SelectedValue = ddlbusiness.SelectedValue;
           
        if (CheckBox4.Checked == true)
        {


            string doc = "SELECT  * from EmployeeDefaultDocumentDetail where Whid='" + ddlbusiness.SelectedValue + "' and EmployeeId='" + ddlemp.SelectedValue + "' ";
            SqlDataAdapter adp = new SqlDataAdapter(doc, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                string date = "";
                string partyname = "";



                string title = dt.Rows[0]["Title"].ToString();

                SqlCommand cmdedit = new SqlCommand("Select DocumentMaster.*,DocumentMainType.DocumentMainTypeId,DocumentSubType.DocumentSubTypeId,Party_master.PartyTypeId,Party_master.Compname +':'+ Party_master.Contactperson as Contactperson from DocumentMaster inner join DocumentType on DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId left outer join Party_master on Party_master.PartyID=DocumentMaster.PartyId left outer join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where DocumentMaster.DocumentId='" + Convert.ToInt32(ddllistofdoc.SelectedValue) + "'", con);
                SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
                DataTable dtedit = new DataTable();
                dtpedit.Fill(dtedit);

                if (dtedit.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["DaynamicDate"].ToString()) == true)
                    {
                        if (dtedit.Rows[0]["DocumentDate"].ToString() != null)
                        {
                            date = Convert.ToDateTime(dtedit.Rows[0]["DocumentDate"].ToString()).ToShortDateString();
                        }
                    }
                    if (Convert.ToBoolean(dt.Rows[0]["DynamicParty"].ToString()) == true)
                    {
                        partyname = dtedit.Rows[0]["Contactperson"].ToString();
                    }


                }

                string finaltitle = title + " "+ date + " "+partyname;
                txtdoctitle.Text = finaltitle.ToString();




                fillpartytype();
                ddlPartyType.SelectedIndex = ddlPartyType.Items.IndexOf(ddlPartyType.Items.FindByValue(dt.Rows[0]["PartyTypeId"].ToString()));

               

                FillParty();
                ddldt.SelectedIndex = ddldt.Items.IndexOf(ddldt.Items.FindByValue(Convert.ToString(dtedit.Rows[0]["DocumentTypenmId"])));
                EventArgs e = new EventArgs();
                object sender = new object();
                ddldt_SelectedIndexChanged(sender, e);
                txtpartdocrefno.Text = Convert.ToString(dtedit.Rows[0]["PartyDocrefno"]);
                ddlpopuppartyname.SelectedIndex = ddlpopuppartyname.Items.IndexOf(ddlpopuppartyname.Items.FindByValue(dt.Rows[0]["PartyId"].ToString()));

                fillmaintaype();
                ddldocmaintype.SelectedIndex = ddldocmaintype.Items.IndexOf(ddldocmaintype.Items.FindByValue(dt.Rows[0]["CabinetId"].ToString()));
                fillsubtype();
                ddldocsubtypename.SelectedIndex = ddldocsubtypename.Items.IndexOf(ddldocsubtypename.Items.FindByValue(dt.Rows[0]["DrawerId"].ToString()));
                filldll();
                ddlDocType.SelectedIndex = ddlDocType.Items.IndexOf(ddlDocType.Items.FindByValue(dt.Rows[0]["FolderId"].ToString()));


                pnlupdatedoc.Visible = true;
            }
            else
            {
                pnlupdatedoc.Visible =false ;
            }

        }
        else
        {
            lblmsg.Text = "";
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
                fillsubtype();
                ddldocsubtypename.SelectedIndex = ddldocsubtypename.Items.IndexOf(ddldocsubtypename.Items.FindByValue(dtedit.Rows[0]["DocumentSubTypeId"].ToString()));
                filldll();
                ddlDocType.SelectedIndex = ddlDocType.Items.IndexOf(ddlDocType.Items.FindByValue(dtedit.Rows[0]["DocumentTypeId"].ToString()));

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
                txtpartdocrefno.Text = Convert.ToString(dtedit.Rows[0]["PartyDocrefno"]);
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

    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        defaultviewswitch();
    }
    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox5.Checked == true)
        {
            Panel4.Visible = true;
        }
        else
        {
            Panel4.Visible = false;
        }
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
    protected void ddlbusnessmaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillmaintaype();
        fillsubtype();
        filldll();

    }
    protected void ddldt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldt.SelectedItem.Text == "Credit Invoice" || ddldt.SelectedItem.Text == "Cash Invoice" || ddldt.SelectedItem.Text == "Cash Voucher" || ddldt.SelectedItem.Text == "Credit Voucher")
        {
            RiredFiealidator2.Visible = true;
        }
        else
        {
            RiredFiealidator2.Visible = false;
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        flaganddoc();
    }
}
