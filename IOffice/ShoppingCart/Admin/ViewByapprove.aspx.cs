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
public partial class ViewbyApprove : System.Web.UI.Page
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
			Session["PageUrl"]=strData;
            Session["PageName"] = page;
            Page.Title = pg.getPageTitle(page);
           
          //  Session["PageName"] = "DocumentEditAndView.aspx";

            if (!IsPostBack)
            {
                                            
                    //DataTable dt = new DataTable();
                    //string doc = "Select DocumentMainType.Whid FROM DocumentMainType INNER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId INNER JOIN DocumentType ON DocumentSubType.DocumentSubTypeId = DocumentType.DocumentSubTypeId inner join DocumentMaster on DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId where DocumentMainType.CID='" + Session["Comid"] + "' and DocumentMaster.DocumentId='" + ViewState["docid"] + "'";
                    //SqlDataAdapter adp = new SqlDataAdapter(doc, con);
                    //adp.Fill(dt);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    ViewState["Whid"] = Convert.ToString(dt.Rows[0]["Whid"]);
                    //}
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
                    //ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(ViewState["Whid"].ToString()));
                    string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
                    SqlCommand cmdeeed = new SqlCommand(eeed, con);
                    SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                    DataTable dteeed = new DataTable();
                    adpeeed.Fill(dteeed);
                    if (dteeed.Rows.Count > 0)
                    {
                        ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                    }

                
                  ddlbusiness_SelectedIndexChanged(sender, e);

                
                //LoadPdf(Docid);
              
                // FillDocumentMainType();
              
              
               
            }
            
           
      

    }
   
    protected void LoadPdf(int Docid)
    {
        DataList1.DataSource = null;
        DataList1.DataBind();
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDoucmentMasterByID(Docid);
        string docname = dt.Rows[0]["DocumentName"].ToString();
        ViewState["decname"] = docname.ToString();
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



    protected void fillapro()
    {
        ddlapprule.Items.Clear();
       // string str = "Select Distinct RuleApproveTypeMaster.RuleApproveTypeId,RuleApproveTypeMaster.RuleApproveTypeName from RuleApproveTypeMaster inner join RuleDetail on  RuleDetail.RuleApproveTypeId=RuleApproveTypeMaster.RuleApproveTypeId where RuleApproveTypeMaster.Whid='" + ddlbusiness.SelectedValue + "' order by RuleApproveTypeName";
        string str = "Select Distinct RuleApproveTypeMaster.RuleApproveTypeId,RuleApproveTypeMaster.RuleApproveTypeName from RuleApproveTypeMaster inner join RuleDetail on  RuleDetail.RuleApproveTypeId=RuleApproveTypeMaster.RuleApproveTypeId where  RuleDetail.Employeeid='" + Session["EmployeeId"] + "' order by RuleApproveTypeName";

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlapprule.DataSource = dt;
            ddlapprule.DataTextField = "RuleApproveTypeName";
            ddlapprule.DataValueField = "RuleApproveTypeId";
            ddlapprule.DataBind();

        }
        //ddlapprule.Items.Insert(0, "All");
        //ddlapprule.Items[0].Value = "0";
    }
  
      protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {

            DataTable dt4 = new DataTable();
            dt4 = select("Select RuleMaster.Whid,RuleApproveTypeMaster.RuleApproveTypeId from RuleMaster inner join  RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId   where RuleDetail.EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' and RuleMaster.Active='1' and RuleDetail.RuleDetailId='" + Request.QueryString["Rd"] + "'");
            if (dt4.Rows.Count > 0)
            {
                ddlbusiness.SelectedValue = dt4.Rows[0]["Whid"].ToString();
            }
            fillapro();
            ddlapprule.SelectedValue = dt4.Rows[0]["RuleApproveTypeId"].ToString();
            ddlapprule_SelectedIndexChanged(sender, e);
           
        }
        else
        {
            fillapro();

            ddlapprule_SelectedIndexChanged(sender, e);
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
    protected void ddlapprule_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddldoc.Items.Clear();
        DataTable dtr = new DataTable();
        DataTable dtr1 = new DataTable();
        string app = "";
        string vv = "0";
        if(ddlapprule.SelectedIndex!=-1)
        {
            DataTable dtsa = select("Select RuleMaster.Whid,RuleApproveTypeMaster.RuleApproveTypeId,RuleTitle from RuleMaster inner join  RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId   where RuleDetail.EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' and RuleDetail.RuleApproveTypeId='" + ddlapprule.SelectedValue + "'");
            lblaatyperule.Text = Convert.ToString(dtsa.Rows[0]["RuleTitle"]);
        }
        
        if (ddlapprule.SelectedIndex > -1)
        {
            app = " and (RuleApproveTypeMaster.RuleApproveTypeId='" + ddlapprule.SelectedValue + "')";
            vv = ddlapprule.SelectedValue;
        }
        int g=0;
        if (vv != "0")
        {
            dtr1 = clsInstruction.SelectRuleDetailforEmployee(Convert.ToInt32(Session["EmployeeId"]), vv, ddlbusiness.SelectedValue);
            foreach (DataRow drw in dtr1.Rows)
            {
                if (Convert.ToString(drw["DocumentTypeId"]) != "0")
                {
                    dtr = (DataTable)select("SELECT Distinct RuleMaster.DocumentTypeId, DocumentMaster.DocumentId, CAST( DocumentMaster.DocumentId as nvarchar)+'-'+DocumentMaster.DocumentTitle as Doc from  RuleApproveTypeMaster RIGHT OUTER JOIN RuleTypeMaster INNER JOIN RuleDetail INNER JOIN RuleMaster inner join DocumentMaster  on RuleMaster.DocumentTypeId=DocumentMaster.DocumentTypeId ON RuleDetail.RuleId = RuleMaster.RuleId ON RuleTypeMaster.RuleTypeId = RuleMaster.RuleTypeId ON RuleApproveTypeMaster.RuleApproveTypeId = RuleDetail.RuleApproveTypeId WHERE (RuleMaster.Active='1') and   (RuleMaster.Whid= '" + ddlbusiness.SelectedValue + "')" + app + " and (RuleDetail.EmployeeId = '" + Session["EmployeeId"] + "') and (DocumentMaster.DocumentId NOT IN(SELECT     DocumentId FROM  RuleProcessMaster inner join RuleDetail on RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId WHERE    (RuleDetail.RuleApproveTypeId='" + ddlapprule.SelectedValue + "') and  (RuleDetail.EmployeeId = '" + Session["EmployeeId"] + "') AND (RuleProcessMaster.RuleDetailId = RuleDetail.RuleDetailId))) ORDER BY DocumentMaster.DocumentId");

                }
                else if (Convert.ToString(drw["DocumentSubId"]) != "0")
                {
                    dtr = (DataTable)select("SELECT Distinct RuleMaster.DocumentMainId, RuleMaster.DocumentTypeId,DocumentMaster.DocumentId, CAST( DocumentMaster.DocumentId as nvarchar)+'-'+DocumentMaster.DocumentTitle as Doc from  RuleApproveTypeMaster RIGHT OUTER JOIN RuleTypeMaster INNER JOIN RuleDetail INNER JOIN RuleMaster inner join  DocumentSubType inner join DocumentType inner join DocumentMaster on DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId    on RuleMaster.DocumentSubId=DocumentSubType.DocumentSubTypeId ON RuleDetail.RuleId = RuleMaster.RuleId ON RuleTypeMaster.RuleTypeId = RuleMaster.RuleTypeId ON RuleApproveTypeMaster.RuleApproveTypeId = RuleDetail.RuleApproveTypeId WHERE (RuleMaster.Active='1') and  (RuleMaster.Whid= '" + ddlbusiness.SelectedValue + "')" + app + " and (RuleDetail.EmployeeId = '" + Session["EmployeeId"] + "') and (DocumentMaster.DocumentId NOT IN(SELECT     DocumentId FROM  RuleProcessMaster inner join RuleDetail on RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId WHERE    (RuleDetail.RuleApproveTypeId='" + ddlapprule.SelectedValue + "') and  (RuleDetail.EmployeeId = '" + Session["EmployeeId"] + "') AND (RuleProcessMaster.RuleDetailId = RuleDetail.RuleDetailId))) ORDER BY DocumentMaster.DocumentId");

                }
                else if (Convert.ToString(drw["DocumentMainId"]) != "0")
                {
                    dtr = (DataTable)select(" SELECT Distinct RuleMaster.DocumentTypeId,DocumentMaster.DocumentId, CAST( DocumentMaster.DocumentId as nvarchar)+'-'+DocumentMaster.DocumentTitle as Doc from  RuleApproveTypeMaster RIGHT OUTER JOIN RuleTypeMaster INNER JOIN RuleDetail INNER JOIN RuleMaster inner join DocumentMainType inner join DocumentSubType inner join DocumentType inner join DocumentMaster on DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId    on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId  on RuleMaster.DocumentMainId=DocumentMainType.DocumentMainTypeId ON RuleDetail.RuleId = RuleMaster.RuleId ON RuleTypeMaster.RuleTypeId = RuleMaster.RuleTypeId ON RuleApproveTypeMaster.RuleApproveTypeId = RuleDetail.RuleApproveTypeId  WHERE (RuleMaster.Active='1') and  (RuleMaster.Whid= '" + ddlbusiness.SelectedValue + "')" + app + " and (RuleDetail.EmployeeId = '" + Session["EmployeeId"] + "') and (DocumentMaster.DocumentId NOT IN(SELECT     DocumentId FROM  RuleProcessMaster inner join RuleDetail on RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId WHERE    (RuleDetail.RuleApproveTypeId='" + ddlapprule.SelectedValue + "') and  (RuleDetail.EmployeeId = '" + Session["EmployeeId"] + "') AND (RuleProcessMaster.RuleDetailId = RuleDetail.RuleDetailId))) ORDER BY DocumentMaster.DocumentId");


                }

                foreach (DataRow dr in dtr.Rows)
                {
                    ListItem match = ddldoc.Items.FindByValue(dr["DocumentId"].ToString());
                    if (match == null)
                    {
                        ddldoc.Items.Insert(g, dr["Doc"].ToString());
                        ddldoc.Items[g].Value = dr["DocumentId"].ToString();
                    }
                }
                //if (dtr.Rows.Count > 0)
                //{
                //    ddldoc.DataSource = dtr;
                //    ddldoc.DataTextField = "Doc";
                //    ddldoc.DataValueField = "DocumentId";
                //    ddldoc.DataBind();
                //}
            }
        }
        if (Convert.ToString(ViewState["in"]) != "in")
        {
            if (ddlapprule.SelectedIndex != -1)
            {
                DataTable dtt = (DataTable)select("Select * from RuleApproveTypeMaster  where RuleApproveTypeMaster.RuleApproveTypeId='" + ddlapprule.SelectedValue + "'");
                if (dtt.Rows.Count > 0)
                {
                    //lblaatyperule.Text = Convert.ToString(dtt.Rows[0]["RuleApproveTypeName"]);
                    txtappdisc.Text = Convert.ToString(dtt.Rows[0]["Description"]);
                }

            }
            if (Request.QueryString["id"] != null)
            {
                ddldoc.SelectedValue = Request.QueryString["id"];
            }
            ddldoc_SelectedIndexChanged(sender, e);
        }
        else
        {
            ViewState["in"] = "";
        }

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
    protected void ddldoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtt = (DataTable)select("Select * from DocumentMaster  where DocumentId='" + ddldoc.SelectedValue + "'");
        if (dtt.Rows.Count > 0)
        {
            lblUploadDate.Text = Convert.ToDateTime(dtt.Rows[0]["DocumentUploadDate"]).ToShortDateString();
        
            lblDoctitle.Text = Convert.ToString(dtt.Rows[0]["DocumentTitle"]);
            lbldocid.Text = Convert.ToString(dtt.Rows[0]["DocumentId"]);


            txtdocdiscription.Text = Convert.ToString(dtt.Rows[0]["Description"]);
            DataTable dttc = (DataTable)select("SELECT DISTINCT EmployeeMaster.EmployeeName, RuleProcessMaster.RuleProcessId, DocumentMaster.DocumentId as DocId,RuleDetail.StepId, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentName, DocumentMaster.DocumentTitle, RuleApproveTypeMaster.Description, DocumentMaster.PartyId,RuleMaster.RuleId,RuleMaster.RuleTitle, RuleApproveTypeMaster.RuleApproveTypeName,RuleDetail.RuleDetailId,Convert(nvarchar, RuleProcessMaster.RuleProcessDate,101) as ProcessDate,RuleProcessMaster.Note,RuleDetail.Days FROM  DocumentMaster RIGHT OUTER JOIN RuleProcessMaster ON RuleProcessMaster.DocumentId=DocumentMaster.DocumentId inner join EmployeeMaster on EmployeeMaster.EmployeeMasterId=RuleProcessMaster.EmployeeId RIGHT OUTER JOIN RuleDetail ON RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId RIGHT OUTER JOIN RuleMaster on RuleMaster.RuleId=RuleDetail.RuleId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId WHERE RuleMaster.Whid= '" + ddlbusiness.SelectedValue + "' and RuleProcessMaster.DocumentId='" + ddldoc.SelectedValue + "' AND  (RuleProcessMaster.Approve='1') order by DocId desc");
            if (dttc.Rows.Count > 0)
            {
                GridView1.DataSource = dttc;
                GridView1.DataBind();
                pvlApphis.Visible = true;
                lblc.Visible = true;
            }
            else
            {
                pvlApphis.Visible = false;
                lblc.Visible = false;
            }

            LoadPdf(Convert.ToInt32(ddldoc.SelectedValue));
         }
        
    }
    protected void lblsavenext_Click(object sender, EventArgs e)
    {
        if (rdlist.SelectedItem.Text != "Pending")
        {
            int RuleDetailId = 0;
            if (Request.QueryString["Rd"] == "")
            {
                DataTable dtr = (DataTable)select(" SELECT Distinct RuleDetail.RuleDetailId  from  RuleApproveTypeMaster INNER JOIN  RuleDetail  ON RuleApproveTypeMaster.RuleApproveTypeId = RuleDetail.RuleApproveTypeId  inner join  RuleMaster on RuleMaster.RuleId= RuleDetail.RuleId   WHERE RuleMaster.Active='1' and RuleDetail.RuleApproveTypeId='" + ddlapprule.SelectedValue + "'   and (RuleDetail.EmployeeId = '" + Session["EmployeeId"] + "')");
                if (dtr.Rows.Count > 0)
                {
                    RuleDetailId = Convert.ToInt32(dtr.Rows[0]["RuleDetailId"]);
                }
            }
            else
            {
                RuleDetailId = Convert.ToInt32(Request.QueryString["Rd"]);
            }


            bool success = clsDocument.InsertRuleProcess(Convert.ToInt32(ddldoc.SelectedValue), RuleDetailId, txtupnote.Text, Convert.ToBoolean(rdlist.SelectedValue)); //(.InsertDocumentApprove(DocProcId, Convert.ToBoolean(Rbtn.SelectedValue), Note.Text);
            if (success == true)
            {
                //sendmail(rdlist.SelectedItem.Text);
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
               // pnlmsg.Visible = true;
                int cc = Convert.ToInt32(ddldoc.SelectedIndex);
                ViewState["in"] = "in";
                ddlapprule_SelectedIndexChanged(sender, e);
               
                ddldoc_SelectedIndexChanged(sender, e);

                if (ddldoc.Items.Count > cc)
                {
                    ddldoc.SelectedIndex = (Convert.ToInt32(ddldoc.SelectedIndex));
                }
                else
                {
                    ddldoc.SelectedIndex = 0;

                }
            }
        }
    }



    protected void lblnext_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (ddldoc.Items.Count > (Convert.ToInt32(ddldoc.SelectedIndex) + 1))
        {
            ddldoc.SelectedIndex = (Convert.ToInt32(ddldoc.SelectedIndex) + 1);
        }
        else
        {
            ddldoc.SelectedIndex = 0;

        }
        ddldoc_SelectedIndexChanged(sender, e);
       
    }

    public StringBuilder getSiteAddressWithWh()
    {
        //DataSet ds544 = (DataSet)getProdcutDetail();
        int whid = Convert.ToInt32(ddlbusiness.SelectedValue);

        SqlCommand cmd = new SqlCommand("Sp_select_SiteaddressWithWH", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("Whid", whid);
        cmd.Parameters.AddWithValue("compid", Session["Comid"]);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        //return ds;
        StringBuilder strAddress = new StringBuilder();
        if (ds.Rows.Count > 0)
        {


            strAddress.Append("<table width=\"100%\"> ");



            strAddress.Append("<tr><td> <img src=\"../images/" + ds.Rows[0]["CompanyLogo"].ToString() + "\" \"border=\"0\" Width=\"176px\" Height=\"106px\" / > </td><td align=\"center\"><b><span style=\"color: #996600\">" + ds.Rows[0]["Sitename"].ToString() + "</span></b><Br><b>" + ds.Rows[0]["CompanyName"].ToString() + "</b><Br>" + ds.Rows[0]["Address1"].ToString() + "<Br><b>TollFree:</b>" + ds.Rows[0]["TollFree1"].ToString() + "," + ds.Rows[0]["TollFree2"].ToString() + "<Br><b>Phone:</b>" + ds.Rows[0]["Phone1"].ToString() + "," + ds.Rows[0]["Phone2"].ToString() + "<Br><b>Fax:</b>" + ds.Rows[0]["Fax"].ToString() + "<Br><b>Email:</b>" + ds.Rows[0]["Email"].ToString() + "<Br><b>Website:</b>" + ds.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");

            strAddress.Append("</table> ");
            ViewState["sitename"] = ds.Rows[0]["Sitename"].ToString();

        }
        return strAddress;
    }
    public void sendmail(String VV)
    {
        int i = 0;
        try
        {


            string hhhg = "SELECT    User_master.EmailID,  Login_master.username, Login_master.password, " +
                " Login_master.department, Login_master.accesslevel, Login_master.deptid, Login_master.accessid, " +
                      " User_master.Name, Login_master.UserID " +
            " FROM         Login_master LEFT OUTER JOIN " +
                      " User_master ON Login_master.UserID = User_master.UserID left outer join DepartmentmasterMNC on Login_master.department=DepartmentmasterMNC.Id " +
                      " Where User_master.UserId='"+Session["UserId"]+"'";
            SqlCommand cm = new SqlCommand(hhhg, con);
            cm.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable ds = new DataTable();
            da.Fill(ds);

            //StringBuilder HeadingTable = new StringBuilder();
            //HeadingTable = (StringBuilder)getSiteAddress();
            StringBuilder HeadingTable = new StringBuilder();
            HeadingTable = (StringBuilder)getSiteAddressWithWh();
            //lblHeading.Text = HeadingTable.ToString();
            //lblHeading.Visible = true;
            // string body = txtBody.Text;

            string AccountInfo = "";
            if (ds.Rows.Count > 0)
            {
                AccountInfo = " <b><span style=\"color: #996600\">ACCOUNT INFORMATION:</span></b><br><b>Username:</b><br>" + ds.Rows[0]["Name"].ToString() + "<br><b>Password:</b><br>" + ds.Rows[0]["password"].ToString() + " ";
            }


            string finalmessage = " Your Document "+VV+ " Succcessfully";
             String body = "" + HeadingTable + "<br><br> Dear " + ds.Rows[0]["Name"].ToString() + ",<br><br>" + finalmessage.ToString() + " <br>" + AccountInfo.ToString() + "<br><br><br> " +
                   "<br><b>Customer Support:</b> " +
                    " <br><br> " +
                    " <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"> " +
                    "Sincerely,</span><br><strong><span style=\"color: #996600\"> " + ViewState["sitename"] + " " +
                    " Team</span></strong>";
           
            string strmal = "  SELECT     OutGoingMailServer,WebMasterEmail, EmailMasterLoginPassword, AdminEmail, WHId " +
                " FROM         CompanyWebsitMaster left outer join CompanyMaster on CompanyWebsitMaster.CompanyId=CompanyMaster.CompanyId WHERE     (WHId = " + Convert.ToInt32(ddlbusiness.SelectedValue) + ") and CompanyMaster.Compid='" + Session["Comid"] + "' ";
            SqlCommand cmdma = new SqlCommand(strmal, con);
            SqlDataAdapter adpma = new SqlDataAdapter(cmdma);
            DataTable dtma = new DataTable();
            adpma.Fill(dtma);
            if (dtma.Rows.Count > 0)
            {
                string AdminEmail = dtma.Rows[0]["WebMasterEmail"].ToString();// TextAdminEmail.Text;
                //string AdminEmail = txtusmail.Text;
                String Password = dtma.Rows[0]["EmailMasterLoginPassword"].ToString();// TextEmailMasterLoginPassword.Text;

                //string body = "Test Mail Server - TestIwebshop";
                MailAddress to = new MailAddress(ds.Rows[0]["EmailID"].ToString());
                MailAddress from = new MailAddress(AdminEmail);

                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = ddldoc.SelectedItem.Text;

                // if (RadioButtonList1.SelectedValue == "0")
                {
                    objEmail.Body = body.ToString();
                    objEmail.IsBodyHtml = true;

                }


                objEmail.Priority = MailPriority.High;
               
                SmtpClient client = new SmtpClient();

                client.Credentials = new NetworkCredential(AdminEmail, Password);
                client.Host = dtma.Rows[0]["OutGoingMailServer"].ToString();

                           client.Send(objEmail);
               

               
            }
            else
            {
             
            }

        }
        catch (Exception tr)
        {
            //lblmsg.Visible = true;
            //lblmsg.Text = i + " Mail Sent - Error " + tr.Message;
        }

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {  string rdlist="";
          DataTable dtr = (DataTable)select(" SELECT Distinct RuleDetail.RuleDetailId  from  RuleApproveTypeMaster INNER JOIN  RuleDetail  ON RuleApproveTypeMaster.RuleApproveTypeId = RuleDetail.RuleApproveTypeId  WHERE RuleDetail.RuleApproveTypeId='" + ddlapprule.SelectedValue + "'   and (RuleDetail.EmployeeId = '" + Session["EmployeeId"] + "')");
            if (dtr.Rows.Count > 0)
            {
                rdlist = Convert.ToString(dtr.Rows[0]["RuleDetailId"]);
            }

          
         string te = "MessageCompose.aspx?apd="+ddldoc.SelectedValue+"&Rd="+rdlist;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
   
}
