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
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using ForAspNet.POP3;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class Account_MessageInboxExt : System.Web.UI.Page
{
    SqlConnection con;
    public delegate Int32 PDF2ImageCallback(int mode, string msg, IntPtr user_data);
    int groupid = 0;
    string accid = "";
    int classid = 0;
    // The following lines import the PDF2Image functions from pdf2image.dll (via PInvoke).
    [DllImport("pdf2image.dll")]
    public static extern int PDF2ImageInit(string user_name, string company, string license_key);

    [DllImport("pdf2image.dll")]
    public static extern int PDF2ImageRun(string command_str, PDF2ImageCallback funct, IntPtr user_data);

    // Return codes for PDF2ImageRun function.
    const int PDF2IMAGE_OK = 0;  // No errors 
    const int PDF2IMAGE_ERR = 1;  // Unspecified error 
    const int PDF2IMAGE_ERR_BADKEY = 2;  // Bad license key 
    const int PDF2IMAGE_ERR_DIRCREATE = 3;  // Failed to create the output file/directory 
    const int PDF2IMAGE_ERR_READINGPDF = 4;  // Failed to read input document 
    const int PDF2IMAGE_ERR_PASSWORD = 5;  // The password required to open PDF is incorrect 
    const int PDF2IMAGE_ERR_CONVERT = 6;  // A conversion error 

    // You can modify the following lines with your registration information.
    const string username = "John Doe";
    const string company = "My Company";
    const string lic_key = "my license key";

    // 'mode' identifier passed in PDF2ImageCallback.
    const int PDF2IMAGE_ERROR = 1;    // Show the error message
    const int PDF2IMAGE_MSG = 2;      // Report the message
    const int PDF2IMAGE_GETPASS = 3;  // Get the password
    const int PDF2IMAGE_OUT_FILENAME = 4; //Get the output filenames
    //1.6 Windows API definitions.
    //==============================================================================================================
    [DllImport("kernel32.dll")]
    public static extern int GetTickCount();
    [DllImport("kernel32.dll")]
    public static extern void CopyMemory(Byte[] dest, int Source, Int32 length);

    const int TRUE = 1;
    const int FALSE = 0;

    MessageCls clsMessage = new MessageCls();
    MasterCls clsMaster = new MasterCls();
    DataTable dt = new DataTable();
    DocumentCls1 clsDocument = new DocumentCls1();
    protected static string flnam = "";
    protected static Int32 flg = 0;
    protected static string empeml = "";
    protected static Int32 spm;
    protected static bool allw;
    EmployeeCls clsEmployee = new EmployeeCls();
    public static int count = 0;
    //int count;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

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
        DataTable dsss = new DataTable();
        //dsss = clsMessage.Empid();
        //if (dsss.Rows.Count > 0)
        //{
        //    Session["EmployeeIdep"] = dsss.Rows[0]["EmployeeMasterId"].ToString();
        //}
        Session["EmployeeIdep"] = Session["EmployeeId"];

        DataTable dter = new DataTable();


        dter = select("Select Whid From EmployeeMaster where EmployeeMasterId='" + Session["EmployeeIdep"] + "'");

        if (dter.Rows.Count > 0)
        {
            ViewState["Whid"] = dter.Rows[0]["Whid"].ToString();
        }

        if (Session["CompanyName"] != null)
        {
            this.Title = Session["CompanyName"] + " IFileCabinet.com Inbox Message";
        }

        Session["PageName"] = "MessageInboxExt.aspx";

        pnlmsg.Visible = false;

        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";

            fillddl();

            fillunreadmails();

            dwnemail1();

            filldatebyperiod();

            fillusertype();
            fillusertype111();
            fillusername();

            filltask();
            fillproject();

            //fillextraemail();

            SelectMsgforInbox();
        }
    }
    protected void fillddl()
    {
        DataTable dtcomemail = new DataTable();
        DataTable dtempemail = new DataTable();
        MasterCls clsMaster = new MasterCls();

        //dtempemail = clsMaster.SelectEmpEmail(Convert.ToInt32(Session["EmployeeIdep"]));
        //if (dtempemail.Rows.Count > 0)
        //{
        //    if (dtempemail.Rows[0]["Email"] != System.DBNull.Value)
        //    {
        //        empeml = dtempemail.Rows[0]["Email"].ToString();
        //    }
        //}
        //dtcomemail = select("select Distinct InOutCompanyEmail.ID as CompanyEmailId,InOutCompanyEmail.InEmailID as EmailId  from InOutCompanyEmail inner join " +
        //                    " CompanyEmailAssignAccessRights  on InOutCompanyEmail.ID=CompanyEmailAssignAccessRights.CompanyEmailId  where CompanyEmailAssignAccessRights.CompanyEmailId in(select CompanyEmailAssignAccessRights.CompanyEmailId from CompanyEmailAssignAccessRights where EmployeeId='" + Convert.ToInt32(Session["EmployeeIdep"]) + "' and DeleteRights='true' and viewRights='true' and sendrights='true')");
        //ddlempemail.DataSource = dtcomemail;
        //ddlempemail.DataBind();
        //ddlempemail.Items.Insert(0, "-Select-");
        //ddlempemail.SelectedItem.Value = "0";
        //if (empeml != "")
        //{

        //    ddlempemail.Items.Insert(1, empeml);
        //    ddlempemail.SelectedIndex = 1;
        //}

        DataTable dtenn = select("select Distinct InOutCompanyEmail.ID as CompanyEmailId,InOutCompanyEmail.InEmailID as EmailId  from InOutCompanyEmail inner join CompanyEmailAssignAccessRights on InOutCompanyEmail.ID=CompanyEmailAssignAccessRights.CompanyEmailId where InOutCompanyEmail.EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "'");

        SqlDataAdapter da1 = new SqlDataAdapter("select designationmasterid from employeemaster where employeemasterid='" + Convert.ToInt32(Session["EmployeeId"]) + "'", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        dtempemail = select("select Distinct InOutCompanyEmail.ID as CompanyEmailId,InOutCompanyEmail.InEmailID as EmailId  from InOutCompanyEmail inner join " +
                         "CompanyEmailAssignAccessRights  on InOutCompanyEmail.ID=CompanyEmailAssignAccessRights.CompanyEmailId  where " +
                         "CompanyEmailAssignAccessRights.CompanyEmailId in(select CompanyEmailAssignAccessRights.CompanyEmailId from CompanyEmailAssignAccessRights " +
                        "where (EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' OR DesignationID='" + dt1.Rows[0]["designationmasterid"].ToString() + "') and viewRights='true') order by InOutCompanyEmail.InEmailID asc");

        ddlempemail.DataSource = dtempemail;
        ddlempemail.DataTextField = "EmailId";
        ddlempemail.DataValueField = "CompanyEmailId";
        ddlempemail.DataBind();

        ddlempemail.Items.Insert(0, "-Select-");
        ddlempemail.SelectedItem.Value = "0";

        if (dtenn.Rows.Count > 0)
        {
            ddlempemail.SelectedIndex = ddlempemail.Items.IndexOf(ddlempemail.Items.FindByValue(dtenn.Rows[0]["CompanyEmailId"].ToString()));
        }

        for (int m = 0; m < ddlempemail.Items.Count; m++)
        {
            string count = "select count(" + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.MsgStatusId) as count2 from " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext where " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.ToEmailID='" + ddlempemail.Items[m].Text + "' and " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.MsgStatusId=1";
            SqlDataAdapter da12 = new SqlDataAdapter(count, con);
            DataTable dt12 = new DataTable();
            da12.Fill(dt12);

            if (m != 0)
            {
                ddlempemail.Items[m].Text = "New " + dt12.Rows[0]["count2"].ToString() + " - " + ddlempemail.Items[m].Text;
            }
        }
    }

    public void fillunreadmails()
    {
        string count = "select count(" + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.MsgStatusId) as count2 from " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext where " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.ToEmailID='" + ddlempemail.SelectedItem.Text + "' and " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.MsgStatusId=1";
        SqlDataAdapter da12 = new SqlDataAdapter(count, con);
        DataTable dt12 = new DataTable();
        da12.Fill(dt12);

        if (dt12.Rows.Count > 0)
        {
            Label16.Text = dt12.Rows[0]["count2"].ToString();
        }
    }

    public Int32 MyCallback(int mode, string msg, IntPtr user_data)
    {
        if (mode == PDF2IMAGE_ERROR)
        {
            Console.WriteLine("Error: {0}", msg);
        }
        else if (mode == PDF2IMAGE_MSG)
        {
            Console.Write("{0}", msg);
        }
        else if (mode == PDF2IMAGE_GETPASS)
        {
            //gl_pass = Console.ReadLine();
            // return gl_pass.c_str();
        }
        else if (mode == PDF2IMAGE_OUT_FILENAME)
        {
            //check whether the IntPtr is valid
            if (user_data.ToInt32() != 0)
            {
                //cast it to a GCHandle to
                //obtain the ArrayList
                GCHandle gch = (GCHandle)user_data;
                ArrayList list = (ArrayList)gch.Target;
                //add the newly created file's path
                list.Add(msg);
            }
        }
        return 0;
    }
    public void Main(string[] args)
    {
        PDF2ImageInit(username, company, lic_key);
        PDF2ImageCallback mycallback = new PDF2ImageCallback(MyCallback);

        String s = "";
        if (args.Length > 1)
        {
            foreach (string arg in args)
            {
                s += arg + " ";
            }

            PDF2ImageRun(s, mycallback, new IntPtr(0));
        }

        string output_folder = Server.MapPath("~/Account/DocumentImage/");

        string output_format = "jpg";


        string filename_prefix = "";
        int filename_digits = 4;

        bool multipage = false;
        bool grayscale = false;


        int dpi = 96;

        int hres = 500, vres = 500;

        int compression_quality = 60;

        int rotate = 0;
        bool antialias = true;
        bool printmode = true;

        s = "";

        if (output_folder != "") s += "-o " + output_folder + " ";

        if (output_format != "") s += "-f " + output_format + " ";

        if (filename_prefix != "") s += "--prefix " + filename_prefix + " ";
        if (filename_digits > 0) s += "--digits " + filename_digits + " ";

        if (multipage) s += "--multipage ";
        if (grayscale) s += "--gray ";

        if (dpi > 0)
        {
            s += "--dpi " + dpi + " ";
        }
        else
        {
            s += "--hres " + hres + " --vres " + vres + " ";
        }

        if (compression_quality > 0)
        {
            s += "-q " + compression_quality + " ";
        }

        if (antialias == false) s += "--nosmooth ";
        if (printmode) s += "--printmode ";

        if (rotate == 90) s += "--rotate 90 ";
        if (rotate == 180) s += "--rotate 180 ";
        if (rotate == 270) s += "--rotate 270 ";

        string Location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");
        string Location2 = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
        foreach (System.IO.FileInfo f in dir.GetFiles("*.pdf"))
        {
            if (System.IO.File.GetAttributes(Location + f.Name).ToString() != System.IO.FileAttributes.Hidden.ToString())
            {
                if (flnam != "")
                {
                    if (f.Name.ToString() == flnam)
                    {

                        s += Location + f.Name + " ";


                        System.IO.File.SetAttributes(Location + f.Name, System.IO.FileAttributes.Hidden);

                    }
                }
            }


        }
        ArrayList saved_files = new ArrayList();

        GCHandle gch = GCHandle.Alloc(saved_files);

        PDF2ImageRun(s, mycallback, (IntPtr)gch);
        gch.Free();

        for (int i = 0; i < saved_files.Count; i++)
        {
            Console.WriteLine(saved_files[i]);
        }
    }
    protected DataTable SelectEmpEmaildetail()
    {
        DataTable dtempemail1 = new DataTable();


        dtempemail1 = select("  Select Signature,LastDownloadedTime,LastDownloadIndex,EmailSignatureMaster.Id,EmailId ,IncomingEmailServer as ServerName,InEmailID as Email,InPassword as Password from InOutCompanyEmail inner join EmailSignatureMaster on EmailSignatureMaster.InoutgoingMasterId=InOutCompanyEmail.ID where EmployeeID='" + Session["EmployeeIdep"] + "'");
        if (dtempemail1.Rows.Count == 0)
        {
            dtempemail1 = select("Select Signature,LastDownloadedTime,LastDownloadIndex,InOutCompanyEmail.Id,EmailId ,IncomingEmailServer as ServerName,InEmailID as Email,InPassword  as Password from InOutCompanyEmail inner join EmailSignatureMaster on EmailSignatureMaster.InoutgoingMasterId=InOutCompanyEmail.ID where Whid='" + ViewState["Whid"] + "'");

        }
        return dtempemail1;
    }

    protected void dwnemail()
    {
        string location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");

        DataTable dbs = new DataTable();
        dbs = select("Select Whid From EmployeeMaster where EmployeeMasterId='" + Session["EmployeeIdep"] + "'");
        if (dbs.Rows.Count > 0)
        {
            ViewState["Whid"] = dbs.Rows[0]["Whid"].ToString();
        }
        DataTable dtemail = new DataTable();

        dtemail = SelectEmpEmaildetail();
        if (dtemail.Rows.Count > 0)
        {
            foreach (DataRow dr in dtemail.Rows)
            {

                flg = 1;

                bool i = DownloadMail(dr["ServerName"].ToString(), dr["Email"].ToString(), dr["Password"].ToString(), location.ToString(), Convert.ToInt32(dr["LastDownloadIndex"]), Convert.ToInt32(Session["EmployeeIdep"]), Convert.ToDateTime(dr["LastDownloadedTime"]), Convert.ToBoolean("True"));

            }
        }
    }

    protected void dwnemail1()
    {
        string location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");

        DataTable dtemail = new DataTable();

        if (ddlempemail.SelectedIndex > 0)
        {
            dtemail = clsMaster.SelectCompanyEmaildetail(Convert.ToInt32(ddlempemail.SelectedValue));

            if (dtemail.Rows.Count > 0)
            {
                foreach (DataRow dr in dtemail.Rows)
                {
                    ViewState["Whid"] = dr["Whid"].ToString();

                    flg = 2;

                    bool i = DownloadMail(dr["ServerName"].ToString(), dr["EmailId"].ToString(), dr["Password"].ToString(), location.ToString(), Convert.ToInt32(dr["LastDownloadIndex"]), Convert.ToInt32(Session["EmployeeIdep"]), Convert.ToDateTime(dr["LastDownloadedTime"]), Convert.ToBoolean("True"));

                }
            }
        }
    }

    public bool DownloadMail(string server, string Email, string password, string DestPath, int LastDownloadIndex, Int32 DocumentEmailDownloadID, DateTime LastTime, bool docautoapprove)
    {

        string location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");
        string location12 = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");
        try
        {

            ForAspNet.POP3.License.LicenseKey = "D0NhcG1hbiBMdGQgUjI5OQEAAAABAAAA/z839HUoyiulFja7UZFbY3sZ6Q9mwIxPBhmGr7oX4+0PgLDF4APv+woUNOa+DYcN9XkD9r+SmFQ=";

            POP3 objPOP3 = new POP3(server.ToString(), Email.ToString(), password.ToString());

            objPOP3.Connect();

            if (objPOP3.IsAPOPSupported)
            {
                objPOP3.SecureLogin();
            }
            else
            {
                objPOP3.Login();
            }

            objPOP3.QueryServer();

            int count = 0;

            for (int index = LastDownloadIndex + 1; index <= objPOP3.TotalMailCount; index++)
            {
                count = count + 1;
                string filename, name;

                EmailMessage objEmail = objPOP3.GetMessage(index, false);

                DateTime msgtime1 = Convert.ToDateTime(objEmail.Date);

                if (flg == 1)
                {
                    index = index + 1;
                    bool i = clsDocument.UpdateEmpEmailLastDownloadIndex(Convert.ToInt32(Convert.ToInt32(ddlempemail.SelectedValue)), Convert.ToInt32(index), msgtime1);

                }
                else if (flg == 2)
                {
                    index = index + 1;
                    bool i = clsDocument.UpdateCompanyEmailLastDownloadIndex(Convert.ToInt32(Convert.ToInt32(ddlempemail.SelectedValue)), Convert.ToInt32(index), msgtime1);
                }

                string msgsubject = objEmail.Subject.ToString();

                string msgbody = objEmail.Body.ToString();

                string fromparty = objEmail.From.ToString();
                int attach = 0;
                int prtid = 0;
                string fst0 = "";
                string sncd0 = "";
                string steml = "";

                char[] separator1 = new char[] { '<' };
                string[] starr1 = fromparty.Split(separator1);
                int i11 = Convert.ToInt32(starr1.Length);
                string fst = "";
                string sncd = "";
                if (i11 == 2)
                {
                    fst = starr1[0].ToString();
                    sncd = starr1[1].ToString();
                }
                char[] separator10 = new char[] { '>' };
                string[] starr10 = sncd.Split(separator10);
                int i110 = Convert.ToInt32(starr10.Length);

                if (i110 == 2)
                {
                    steml = starr10[0].ToString();
                }
                if (steml == "")
                {
                    steml = fromparty;
                }

                DataTable dtspmeml = new DataTable();
                dtspmeml = clsDocument.SelectSpamEmail(steml, Convert.ToInt32(Session["PartyId"]));
                if (dtspmeml.Rows.Count > 0)
                {
                    spm = 1;
                    allw = Convert.ToBoolean(dtspmeml.Rows[0]["AllowEmail"].ToString());

                }
                else
                {
                    allw = false;
                    spm = 0;
                }

                DataTable dtminprt1 = new DataTable();
                dtminprt1 = clsDocument.SelectPartyIdFromPartyEmail(steml);

                Int32 folderid = 0;
                Int32 ign = 0;
                string doc_email_desc = "";
                string refst = "";
                Double amt = 0.0;

                if (dtminprt1.Rows.Count > 0)
                {
                    if (dtminprt1.Rows[0]["PartyId"] != System.DBNull.Value)
                    {
                        prtid = Convert.ToInt32(dtminprt1.Rows[0][0].ToString());
                    }
                }
                if (prtid > 0)
                {
                    DataTable dtfldfmprt = new DataTable();
                    dtfldfmprt = clsDocument.SelectFolderIdFromPartyID(prtid);
                    if (dtfldfmprt.Rows.Count > 0)
                    {
                        if (dtfldfmprt.Rows[0]["FolderId"] != System.DBNull.Value)
                        {
                            folderid = Convert.ToInt32(dtfldfmprt.Rows[0]["FolderId"].ToString());
                        }
                    }
                }
                if (prtid == 0)
                {
                    dtminprt1 = clsDocument.SelectMinPartyIdfromPartyMaster("General", ViewState["Whid"].ToString());
                    if (dtminprt1.Rows.Count > 0)
                    {
                        if (dtminprt1.Rows[0]["PartyId"] != System.DBNull.Value)
                        {
                            prtid = Convert.ToInt32(dtminprt1.Rows[0][0].ToString());
                        }
                    }
                }
                if (prtid == 0)
                {
                    int prttypeid = 0;
                    DataTable dtminprt100 = new DataTable();
                    dtminprt100 = clsDocument.SelectOtherPartyTypeId("Admin");

                    if (dtminprt100.Rows.Count > 0)
                    {
                        if (dtminprt100.Rows[0]["PartyTypeId"] != System.DBNull.Value)
                        {
                            prttypeid = Convert.ToInt32(dtminprt100.Rows[0]["PartyTypeId"].ToString());

                        }
                    }

                }
                if (folderid == 0)
                {
                    //Int32 i = 0;
                    DataTable dtgen = new DataTable();
                    dtgen = clsDocument.SelectGeneralFolderId(ViewState["Whid"].ToString());
                    if (dtgen.Rows.Count > 0)
                    {
                        if (dtgen.Rows[0]["DocumentTypeId"] != System.DBNull.Value)
                        {
                            ign = Convert.ToInt32(dtgen.Rows[0]["DocumentTypeId"].ToString());
                        }
                    }
                }
                Int32 topartyid = 0;
                DataTable dtorgeml = new DataTable();
                dtorgeml = clsMaster.SelectPartyIdfromCompanyEmail(Convert.ToInt32(ViewState["emailids"]));

                if (dtorgeml.Rows.Count > 0)
                {
                    if (dtorgeml.Rows[0][1] != System.DBNull.Value)
                    {
                        topartyid = Convert.ToInt32(dtorgeml.Rows[0][1].ToString());
                    }
                }
                folderid = ign;
                string extstr = "";
                string doc_title = "";
                doc_title = "Receive From:" + steml + "," + msgsubject + "," + objEmail.Date;
                if (fromparty != "")
                {

                }

                if (objEmail.IsAnyAttachments)
                {

                    for (int attCount = 0; attCount < objEmail.Attachments.Count; attCount++)
                    {
                        ForAspNet.POP3.Attachment att = (Attachment)objEmail.Attachments[attCount];

                        if (att.IsFileAttachment)
                        {

                            attach = 1;

                            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + Email.ToString() + "_" + att.FileName;
                            att.Save(DestPath.ToString() + filename.ToString());

                            Int32 MsgId = 0;

                            string ins1 = "insert into " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt (FromPartyId,MsgDate,MsgSubject,FromEmail,CID) values('" + prtid + "','" + System.DateTime.Now.ToString() + "','" + msgsubject + "','" + steml + "','" + Session["Comid"].ToString() + "')";
                            SqlCommand cmd = new SqlCommand(ins1, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();

                            SqlDataAdapter da = new SqlDataAdapter("select max(MsgId) as MsgId from " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt", con);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                MsgId = Convert.ToInt32(dt.Rows[0]["MsgId"].ToString());
                            }

                            msgbody = msgbody.Replace("'", " ");

                            string ins2 = "insert into " + "[" + PageConn.extmsg11 + "]" + ".dbo.Msgbodyext(MsgId,MsgDetail) values('" + MsgId + "','" + msgbody + "')";
                            SqlCommand cmd1 = new SqlCommand(ins2, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd1.ExecuteNonQuery();
                            con.Close();


                            if (MsgId > 0)
                            {

                                string insert2 = "insert into " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt(MsgId,topartyid,ToEmailID,msgstatusid) values('" + MsgId + "','" + Convert.ToInt32(Session["PartyId"].ToString()) + "','" + Email + "',1)";
                                SqlCommand cmdlel2 = new SqlCommand(insert2, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdlel2.ExecuteNonQuery();
                                con.Close();

                                bool ins = clsMessage.InsertMsgFileAttachDetailExt(MsgId, filename);

                                flnam = filename;


                            }

                            if (System.IO.File.Exists(location12 + filename))
                            {
                            }
                            else
                            {
                                File.Copy(DestPath.ToString() + filename.ToString(), location12.ToString() + filename.ToString(), true);
                            }
                        }
                        //}
                    }
                    if (attach == 0)
                    {
                        Int32 MsgId = 0;
                        Int32 MessageDetailId = 0;

                        string insert = "insert into " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgmasterext(FromPartyId,MsgSubject,FromEmail,Msgdate,cid) values('" + prtid + "','" + msgsubject + "','" + steml + "','" + System.DateTime.Now.ToShortDateString() + "','" + Session["Comid"].ToString() + "')";
                        SqlCommand cmdlel = new SqlCommand(insert, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdlel.ExecuteNonQuery();
                        con.Close();

                        SqlDataAdapter dalal = new SqlDataAdapter("select max(MsgId) as MsgId from " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgmasterext", con);
                        DataTable dtlal = new DataTable();
                        dalal.Fill(dtlal);

                        if (dtlal.Rows.Count > 0)
                        {
                            MsgId = Convert.ToInt32(dtlal.Rows[0]["MsgId"]);
                        }

                        msgbody = msgbody.Replace("'", " ");

                        if (MsgId > 0)
                        {
                            string insert1 = "insert into " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgbodyext(MsgId,MsgDetail) values('" + MsgId + "','" + msgbody + "')";
                            SqlCommand cmdlel1 = new SqlCommand(insert1, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdlel1.ExecuteNonQuery();
                            con.Close();

                            string insert2 = "insert into " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt(MsgId,topartyid,ToEmailID,msgstatusid) values('" + MsgId + "','" + Convert.ToInt32(Session["PartyId"].ToString()) + "','" + Email + "',1)";
                            SqlCommand cmdlel2 = new SqlCommand(insert2, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdlel2.ExecuteNonQuery();
                            con.Close();

                            SqlDataAdapter dalal1 = new SqlDataAdapter("select max(msgdetailid) as msgdetailid from " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt", con);
                            DataTable dtlal1 = new DataTable();
                            dalal1.Fill(dtlal1);

                            if (dtlal1.Rows.Count > 0)
                            {
                                MessageDetailId = Convert.ToInt32(dtlal1.Rows[0]["msgdetailid"]);
                            }

                        }

                    }
                }
                else
                {

                }

            }


            objPOP3.Close();


        }
        catch (Exception es)
        {
            pnlmsg.Visible = true;

            lblmsg.Text = "Message : Check your Incoming and Outgoing Mail Server" + es.Message;

        }
        return true;
    }


    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void accbalentry()
    {

        DataTable ds153 = new DataTable();
        ds153 = select("select Report_Period_Id  from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + ViewState["Whid"] + "' and Active='1'");
        Session["reportid"] = ds153.Rows[0]["Report_Period_Id"].ToString();

        DataTable ds1531 = new DataTable();
        ds1531 = select("select Report_Period_Id from [ReportPeriod] where ReportPeriod.Report_Period_Id<'" + Session["reportid"] + "' and  Whid='" + ViewState["Whid"] + "'  order by Report_Period_Id Desc");
        Session["reportid1"] = ds1531.Rows[0]["Report_Period_Id"].ToString();

        string str4562 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid1"] + "')";
        SqlCommand cmd4562 = new SqlCommand(str4562, con);
        con.Open();
        cmd4562.ExecuteNonQuery();
        con.Close();


        string str456 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid"].ToString() + "')";
        SqlCommand cmd456 = new SqlCommand(str456, con);
        con.Open();
        cmd456.ExecuteNonQuery();
        con.Close();

    }
    protected void SelectMsgforInbox()
    {
        DataTable dt1 = new DataTable();

        //foreach (GridViewRow GR in GridView2.Rows)
        //{
        //    CheckBox chk = (CheckBox)GR.FindControl("chkParty");

        //    if (chk.Checked == true)
        //    {
        //        Label lalaparty = (Label)GR.FindControl("lalaparty");

        //        ViewState["emailids"] = lalaparty.Text;

        //        if (GR.RowIndex > 0)
        //        {
        //            dwnemail1();
        //        }

        //        Label lbl1 = (Label)GR.FindControl("Label13");
        //        ViewState["lb"] = lbl1.Text;

        string mes1 = "";
        string mes2 = "";
        string mes3 = "";
        string mes4 = "";
        string mes5 = "";
        string mes6 = "";
        string mes7 = "";
        string mes8 = "";
        string mes9 = "";
        string mes10 = "";
        string mes11 = "";
        string mes12 = "";
        string mes13 = "";
        string mes14 = "";
        string mes15 = "";
        string mes16 = "";
        string mes17 = "";
        string mes18 = "";
        string mes19 = "";
        string mes20 = "";
        string mes = "";

        if (TextBox1.Text != "")
        {
            mes1 += " and ((" + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate like '%" + TextBox1.Text.Replace("'", "''") + "%') or (" + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgSubject like '%" + TextBox1.Text.Replace("'", "''") + "%') or (Party_Master.Compname like '%" + TextBox1.Text.Replace("'", "''") + "%') or (" + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.FromEmail like '%" + TextBox1.Text.Replace("'", "''") + "%'))";
        }

        if (RadioButtonList1.SelectedValue == "1")
        {
            if (ddlperiod.SelectedItem.Text == "All")
            {

                mes2 += "";
            }
            if (ddlperiod.SelectedItem.Text == "Today")
            {

                mes3 += " and cast(" + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate as Date)='" + System.DateTime.Now.ToShortDateString() + "'";
            }
            if (ddlperiod.SelectedItem.Text == "Yesterday")
            {

                mes4 += " and cast(" + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate as Date)='" + System.DateTime.Now.AddDays(-1).ToShortDateString() + "'";
            }
            if (ddlperiod.SelectedItem.Text == "This Week")
            {


                mes5 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate between '" + ViewState["thisweekstart"] + "' and '" + ViewState["thisweekend"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last Week")
            {


                mes6 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate between '" + ViewState["lastweekstart"] + "' and '" + ViewState["lastweekend"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last 2 Weeks")
            {


                mes7 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate between '" + ViewState["last2weekstart"] + "' and '" + ViewState["lastweekend"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "This Month")
            {


                mes8 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate between '" + ViewState["thismonthstartdate"] + "' and '" + ViewState["thismonthenddate"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last Month")
            {


                mes9 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate between '" + ViewState["lastmonthstartdate"] + "' and '" + ViewState["lastmonthenddate"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last 2 Months")
            {


                mes10 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate between '" + ViewState["last2monthstart"] + "' and '" + ViewState["lastmonthenddate"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Current Year")
            {


                mes11 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate between '" + ViewState["thisyearstartdate"] + "' and '" + ViewState["thisyearenddate"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last Year")
            {


                mes12 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate between '" + ViewState["lastyearstartdate"] + "' and '" + ViewState["lastyearenddate"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last 2 Years")
            {


                mes13 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate between '" + ViewState["last2yearstartdate"] + "' and '" + ViewState["lastyearenddate"] + "'";
            }
        }

        if (RadioButtonList1.SelectedValue == "0")
        {
            if (txtestartdate.Text != "" && txteenddate.Text != "")
            {
                mes17 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate between '" + txtestartdate.Text + "' and '" + txteenddate.Text + "'";
            }

        }

        if (RadioButtonList2.SelectedValue == "0")
        {
            if (ddlusertype.SelectedIndex > 0)
            {


                mes14 += " and Party_master.PartyTypeId='" + ddlusertype.SelectedValue + "'";
            }

        }

        if (RadioButtonList2.SelectedValue == "1")
        {

            if (ddlusertype1.SelectedIndex > 0)
            {


                mes15 += " and Party_master.PartyTypeId='" + ddlusertype1.SelectedValue + "'";
            }

            if (ddlusername.SelectedIndex > 0)
            {

                mes16 += " and Party_master.PartyId='" + ddlusername.SelectedValue + "'";
            }
        }

        if (chktaskproject.Checked == true)
        {
            if (ddlproject.SelectedIndex > 0)
            {
                mes18 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgmasterext.projectid='" + ddlproject.SelectedValue + "'";
            }
            if (ddltask.SelectedIndex > 0)
            {
                mes19 += " and " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgmasterext.taskid='" + ddltask.SelectedValue + "'";
            }
        }

        string mast = "";


        string meld = ddlempemail.SelectedItem.Text;

        int len = ddlempemail.SelectedItem.Text.Length;

        int mou = meld.IndexOf('-');

        int len2 = len - (mou + 2);

        //int mou2 = Convert.ToInt32(len);

        meld = meld.Substring(mou + 2, len2);

        if (ddlempemail.SelectedIndex > 0)
        {
            mast += " and (" + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.ToEmailID = '" + meld + "')";
        }

        mes = "select distinct " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate, " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgSubject,CASE WHEN (" + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.FromEmail IS NULL)THEN '' ELSE " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.FromEmail END as Email,CASE WHEN (Party_Master.Compname IS NULL)THEN '' ELSE  Party_Master.Compname END as Compname, " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgStatusMasterExt.MsgStatusName, " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgDetailId, " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.ToPartyId," + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgStatusId FROM " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt INNER JOIN " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt ON " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgId = " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgId INNER JOIN  " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgStatusMasterExt ON " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgStatusId = " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgStatusMasterExt.MsgStatusId Left Join Party_Master ON " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.FromPartyId = Party_Master.PartyId WHERE " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgStatusId IN (1, 2) " + mast + " " + mes1 + " " + mes2 + " " + mes3 + " " + mes3 + " " + mes4 + " " + mes5 + " " + mes6 + " " + mes7 + " " + mes8 + " " + mes9 + " " + mes10 + " " + mes11 + " " + mes12 + " " + mes13 + " " + mes14 + " " + mes15 + " " + mes16 + " " + mes17 + " " + mes18 + " " + mes19 + " order by " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgDate desc";


        SqlDataAdapter daw = new SqlDataAdapter(mes, con);
        daw.Fill(dt1);
        dt.Merge(dt1);

        //    }
        //}
        gridInbox.DataSource = dt;
        gridInbox.DataBind();


        if (gridInbox.Rows.Count > 0)
        {
            imgbtndiscard.Visible = true;
            ImageButton2.Visible = true;
            btndelete.Visible = true;
            btnreply.Visible = true;
            btnforward.Visible = true;
            // btnemailrule.Visible = true;
        }
        else
        {
            imgbtndiscard.Visible = false;
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            btndelete.Visible = false;
            btnreply.Visible = false;
            btnforward.Visible = false;
            btnemailrule.Visible = false;
        }

        foreach (GridViewRow gdr5 in gridInbox.Rows)
        {
            Label lblemail = (Label)gdr5.FindControl("lblemail");
            Button btnAddparty = (Button)gdr5.FindControl("btnAddparty");
            string fromparty = lblemail.Text;

            if (fromparty == "")
            {
                btnAddparty.Enabled = true;
            }
            else
            {
                btnAddparty.Enabled = false;
            }
        }
    }

    private int GetRowCount(string str)
    {
        int count = 0;
        DataTable dte = new DataTable();
        dte = select(str);
        if (dte.Rows.Count > 0)
        {
            count += Convert.ToInt32(dte.Rows[0]["ci"]);
        }
        ViewState["count"] = count;
        return count;

    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression, string query)
    {
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));

        dt.Columns.Remove("ROW_NUM");

        return dt;

        ViewState["dt"] = dt;
    }

    //protected DataTable select(string qu)
    //{
    //    SqlCommand cmd = new SqlCommand(qu, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    return dt;
    //}

    protected void gridInbox_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ////
            if (DataBinder.Eval(e.Row.DataItem, "MsgStatusName").ToString() == "UNREAD")
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[5].Font.Bold = true;

            }
            Int32 MsgId = 0;
            DataTable dtMain = new DataTable();
            Int32 MsgDetailId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MsgDetailId").ToString());
            dtMain = clsMessage.SelectMsgIdUsingMsgDetailIdExt(MsgDetailId);
            if (dtMain.Rows.Count > 0)
            {
                MsgId = Convert.ToInt32(dtMain.Rows[0]["MsgId"].ToString());
                dtMain = new DataTable();
                dtMain = clsMessage.SelectMsgforFileAttachExt(MsgId);
                Image img = (Image)e.Row.FindControl("ImgFile");
                if (dtMain.Rows.Count > 0)
                {
                    img.ImageUrl = "~/Account/images/attach.png";
                    img.Visible = true;
                }
                else
                {
                    img.ImageUrl = "";
                    img.Visible = false;
                }
            }
        }
        try
        {
            if (gridInbox.Rows.Count > 0)
            {
                CheckBox cbHeader = (CheckBox)gridInbox.HeaderRow.FindControl("HeaderChkbox");
                cbHeader.Attributes["onclick"] = "ChangeAllCheckBoxStates(this.checked);";
                List<string> ArrayValues = new List<string>();
                ArrayValues.Add(string.Concat("'", cbHeader.ClientID, "'"));
                foreach (GridViewRow gvr in gridInbox.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("chkMsg");
                    cb.Attributes["onclick"] = "ChangeHeaderAsNeeded();";
                    ArrayValues.Add(string.Concat("'", cb.ClientID, "'"));
                }
                CheckBoxIDsArray.Text = "<script type='text/javascript'>" + "\n" + "<!--" + "\n" + String.Concat("var CheckBoxIDs =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") + "\n // -->" + "\n" + "</script>";

            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            pnlmsg.Visible = true;
            lblmsg.Text = "Error in databound : " + ex.Message.ToString();
        }
    }
    protected void gridInbox_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridInbox.PageIndex = e.NewPageIndex;
        SelectMsgforInbox();
    }
    protected void imgbtndiscard_Click(object sender, EventArgs e)
    {
        if (gridInbox.Rows.Count > 0)
        {
            foreach (GridViewRow GR in gridInbox.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkMsg");
                if (chk.Checked == true)
                {
                    Int32 MsgDetailId = Convert.ToInt32(gridInbox.DataKeys[GR.RowIndex].Value);
                    clsMessage.UpdateMsgDetailExt(MsgDetailId, 4);
                    lblmsg.Text = "Message Discard Successfully";
                    lblmsg.Visible = true;
                    pnlmsg.Visible = true;
                }
            }
            SelectMsgforInbox();
        }
    }
    public void setGridisze()
    {
        // doc grid
        if (gridInbox.Rows.Count == 0)
        {
            pnlgrid.CssClass = "GridPanel20";
        }
        else if (gridInbox.Rows.Count == 1)
        {
            pnlgrid.CssClass = "GridPanel125";
        }
        else if (gridInbox.Rows.Count == 2)
        {
            pnlgrid.CssClass = "GridPanel150";
        }
        else if (gridInbox.Rows.Count == 3)
        {
            pnlgrid.CssClass = "GridPanel175";
        }
        else if (gridInbox.Rows.Count == 4)
        {
            pnlgrid.CssClass = "GridPanel200";
        }
        else if (gridInbox.Rows.Count == 5)
        {
            pnlgrid.CssClass = "GridPanel225";
        }
        else if (gridInbox.Rows.Count == 6)
        {
            pnlgrid.CssClass = "GridPanel250";
        }
        else if (gridInbox.Rows.Count == 7)
        {
            pnlgrid.CssClass = "GridPanel275";
        }
        else if (gridInbox.Rows.Count == 8)
        {
            pnlgrid.CssClass = "GridPanel";
        }
        else if (gridInbox.Rows.Count == 9)
        {
            pnlgrid.CssClass = "GridPanel325";
        }
        else if (gridInbox.Rows.Count == 10)
        {
            pnlgrid.CssClass = "GridPanel350";
        }

        else
        {
            pnlgrid.CssClass = "GridPanel375";
        }


    }
    protected void gridInbox_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        SelectMsgforInbox();
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
    protected void ddlempemail_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillunreadmails();
        dwnemail1();
        SelectMsgforInbox();
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        Int32 MsgDetailId = 0;
        Int32 MsgId = 0;
        string sbjct = "";
        Int32 frmprt = 0;
        Int32 folderid = 0;
        Int32 ign = 0;

        string location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");
        string location1 = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");

        //string location = Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\" + filename.ToString());
        //string location1 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + filename.ToString());


        DataTable dt012 = new DataTable();
        DataTable dtMain = new DataTable();
        DataTable dt = new DataTable();
        if (gridInbox.Rows.Count > 0)
        {
            foreach (GridViewRow GR in gridInbox.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkMsg");
                if (chk.Checked == true)
                {
                    MsgDetailId = Convert.ToInt32(gridInbox.DataKeys[GR.RowIndex].Value);
                }
            }
        }
        dtMain = clsMessage.SelectMsgIdUsingMsgDetailIdExt(MsgDetailId);
        if (dtMain.Rows.Count > 0)
        {
            MsgId = Convert.ToInt32(dtMain.Rows[0]["MsgId"].ToString());
        }
        dt012 = clsMessage.SelectMsgforDetailExt(MsgDetailId);
        if (dt012.Rows.Count > 0)
        {
            sbjct = dt012.Rows[0]["MsgSubject"].ToString();
            //TxtMsgDetail.Text = dt.Rows[0]["MsgDetail"].ToString();
            frmprt = Convert.ToInt32(dt012.Rows[0]["PartyId"].ToString());
        }

        dt = clsMessage.SelectMsgforFileAttachExt(MsgId);
        DataTable dtfldfmprt = new DataTable();
        dtfldfmprt = clsDocument.SelectFolderIdFromPartyID(frmprt);
        if (dtfldfmprt.Rows.Count > 0)
        {
            if (dtfldfmprt.Rows[0]["FolderId"] != System.DBNull.Value)
            {
                folderid = Convert.ToInt32(dtfldfmprt.Rows[0]["FolderId"].ToString());
            }
        }
        if (folderid == 0)
        {
            //Int32 i = 0;
            DataTable dtgen = new DataTable();
            dtgen = clsDocument.SelectGeneralFolderId(ViewState["Whid"].ToString());
            if (dtgen.Rows.Count > 0)
            {
                if (dtgen.Rows[0]["DocumentTypeId"] != System.DBNull.Value)
                {
                    ign = Convert.ToInt32(dtgen.Rows[0]["DocumentTypeId"].ToString());
                }
            }
        }
        folderid = ign;
        foreach (DataRow DR in dt.Rows)
        {
            string flext = Path.GetExtension(location + DR["FileName"].ToString());

            string dctr = "1";


            SqlDataAdapter dass = new SqlDataAdapter("select id from DocumentTypenm where name='Email' and Active='1'", con);
            DataTable dtss = new DataTable();
            dass.Fill(dtss);

            if (dtss.Rows.Count > 0)
            {
                dctr = Convert.ToString(dtss.Rows[0]["id"]);
            }

            Int32 rst45 = clsDocument.InsertDocumentMaster(folderid, 2, Convert.ToDateTime(System.DateTime.Now.ToString()), DR["FileName"].ToString(), sbjct, "", frmprt, "0", 0, Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now.ToString()), flext, dctr, "");


            string path1 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\" + DR["FileName"].ToString());
            string path2 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + DR["FileName"].ToString());


            if (System.IO.File.Exists(path2))
            {
            }
            else
            {
                File.Copy(path1, path2);
            }
            string filexten = Path.GetExtension(path2);


            if (rst45 > 0)
            {
                bool st = true;
                ViewState["rst"] = rst45.ToString();
                if (st.ToString() == "True")
                {
                    bool dcaprv = true;
                    bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst45, dcaprv);
                }

                int rsts = clsDocument.InsertDocumentLog(rst45, Convert.ToInt32(Session["EmployeeId"]),
               Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);
            }


            string Location = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocuments/");
            string Location2 = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocumentsTemp/");

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);

            if (filexten == ".pdf")
            {
                foreach (System.IO.FileInfo f in dir.GetFiles(DR["FileName"].ToString()))
                {

                    string Location1 = Server.MapPath(@"~/Account/" + Session["comid"] + "/DocumentImage/");

                    string filepath = Server.MapPath("~//Account//pdftoimage.exe");
                    System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);
                    pti.UseShellExecute = false;
                    pti.Arguments = filepath + " -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";

                    pti.RedirectStandardOutput = true;
                    pti.RedirectStandardInput = true;
                    pti.RedirectStandardError = true;

                    pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//");
                    System.Diagnostics.Process ps = Process.Start(pti);

                    if (System.IO.File.Exists(Location2 + f.Name))
                    {
                    }
                    else
                    {
                        System.IO.File.Copy(Location + f.Name, Location2 + f.Name);
                    }
                    System.IO.File.SetAttributes(Location2 + f.Name, System.IO.FileAttributes.Hidden);
                }
            }

            if (filexten == ".pdf")
            {
                int ii = 0;
                string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + DR["FileName"].ToString());
                using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                {
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection match = regex.Matches(st.ReadToEnd());
                    ii = match.Count;
                }

                int length = DR["FileName"].ToString().Length;
                string docnameIn = DR["FileName"].ToString().Substring(0, length - 4);


                for (int kk = 1; kk <= ii; kk++)
                {
                    string scpf = docnameIn;
                    if (kk >= 1 && kk < 10)
                    {
                        scpf = scpf + "0000" + kk + ".jpg";
                    }
                    else if (kk >= 10 && kk < 100)
                    {
                        scpf = scpf + "000" + kk + ".jpg";
                    }
                    else if (kk >= 100)
                    {
                        scpf = scpf + "00" + kk + ".jpg";
                    }
                    clsEmployee.InserDocumentImageMaster(rst45, scpf);

                }
            }
            pnlmsg.Visible = true;
            lblmsg.Visible = true;
            lblmsg.Text = "Documents Uploaded Successfully.";

            //if (flext == ".pdf")
            //{
            //    string filepath = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");
            //    System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);
            //    //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
            //    //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");
            //    pti.UseShellExecute = false;
            //    //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
            //    pti.Arguments = filepath + " -i UploadedDocuments//" + DR["FileName"].ToString() + " " + "-o" + " " + "DocumentImage//";

            //    pti.RedirectStandardOutput = true;
            //    pti.RedirectStandardInput = true;
            //    pti.RedirectStandardError = true;
            //    //pti.WorkingDirectory = "D:\\Capman.ifilecabinet.com1\\Account\\";
            //    pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");
            //    System.Diagnostics.Process ps = Process.Start(pti);
            //}
            ////if (System.IO.File.Exists(location1 + DR["FileName"].ToString()))
            ////{

            ////}
            //else
            //{
            //    File.Copy(location.ToString() + DR["FileName"].ToString(), location1.ToString() + DR["FileName"].ToString(), true);
            //}

        }
    }

    protected void btnAddparty_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;

        int indx = row.RowIndex;

        Int32 datakey = Int32.Parse(gridInbox.DataKeys[indx].Value.ToString());
        Label lblemail = (Label)gridInbox.Rows[indx].FindControl("lblemail");
        string fromparty = lblemail.Text;
        char[] separator1 = new char[] { ':' };
        string[] starr1 = fromparty.Split(separator1);
        int i11 = Convert.ToInt32(starr1.Length);

        tbEmail.Text = starr1[0].ToString();
        tbEmail.Enabled = false;
        DataTable dt = new DataTable();
        dt = select("Select Whid from Party_Master where  PartyId='" + Session["PartyId"] + "'");

        if (dt.Rows.Count > 0)
        {
            ViewState["Whid"] = Convert.ToString(dt.Rows[0]["Whid"]);
            string qryStr = "select * from PartytTypeMaster where compid='" + Session["comid"] + "'  order by PartType";

            ddlPartyType.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlPartyType, "PartType", "PartyTypeId");
            ddlPartyType.Items.Insert(0, "--Select--");
            ddlPartyType.Items[0].Value = "0";
            string qryStr1 = "select CountryId,CountryName from CountryMaster order by CountryName";
            ddlCountry.DataSource = (DataTable)select(qryStr1);
            fillddlOther(ddlCountry, "CountryName", "CountryId");
            ddlCountry.Items.Insert(0, "--Select--");
            ddlCountry.Items[0].Value = "0";

            qryStr = "Select id,Departmentname from DepartmentmasterMNC  where Companyid='" + Session["comid"] + "' and Whid='" + ViewState["Whid"] + "'  order by Departmentname";
            ddldept.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddldept, "Departmentname", "id");
            ddldept.Items.Insert(0, "-Select-");
            ddldept.Items[0].Value = "0";

            ddldesignation.Items.Insert(0, "-Select-");
            ddldesignation.Items[0].Value = "0";
        }
        ModalPopupExtender142422.X = 90;
        ModalPopupExtender142422.Y = 140;
        // ModalPopupExtender142422.Show();


        string te = "partymaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.Items.Clear();
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();

    }


    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
        if (ddlCountry.SelectedIndex > 0)
        {

            string qryStr = "select StateId,StateName from StateMasterTbl where CountryId=" + ddlCountry.SelectedValue + " order by StateName";
            ddlState.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlState, "StateName", "StateId");
            ddlState.Items.Insert(0, "-Select-");
            ddlState.Items[0].Value = "0";
            ddlCity.Items.Insert(0, "-Select-");
            ddlCity.Items[0].Value = "0";

        }
        else
        {

            ddlState.Items.Insert(0, "-Select-");
            ddlState.Items[0].Value = "0";
            ddlCity.Items.Insert(0, "-Select-");
            ddlCity.Items[0].Value = "0";

        }

        ModalPopupExtender142422.Show();

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCity.Items.Clear();
        if (ddlState.SelectedIndex > 0)
        {

            string qryStr = "select CityId,CityName from CityMasterTbl where StateId=" + ddlState.SelectedValue + " order by CityName";
            ddlCity.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlCity, "CityName", "CityId");
            ddlCity.Items.Insert(0, "--Select--");
            ddlCity.Items[0].Value = "0";
        }
        else
        {
            ddlCity.Items.Insert(0, "--Select--");
            ddlCity.Items[0].Value = "0";
        }
        ModalPopupExtender142422.Show();
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddldesignation.Items.Clear();
        if (ddldept.SelectedIndex > 0)
        {
            string str = "SELECT DesignationMasterId,DesignationName,DeptID " +
                         " FROM DesignationMaster " +
                         "where DeptID=" + ddldept.SelectedValue + "";

            DataTable dt = new DataTable();
            dt = select(str);

            if (dt.Rows.Count > 0)
            {
                ddldesignation.DataSource = dt;
                fillddlOther(ddldesignation, "DesignationName", "DesignationMasterId");

                ddldesignation.Items.Insert(0, "-Select-");
                ddldesignation.Items[0].Value = "0";
            }


        }
        else
        {
            ddldesignation.Items.Insert(0, "-Select-");
            ddldesignation.Items[0].Value = "0";
        }
        ModalPopupExtender142422.Show();
    }
    protected void groupclass()
    {
        if (ddlPartyType.SelectedItem.Text == "Vendor")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ViewState["Whid"] + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {

                    int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                    accid = Convert.ToString(gid);
                }
                else
                {
                    accid = Convert.ToString(30000);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Customer")
        {
            groupid = 2;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='2' and Whid='" + ViewState["Whid"] + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    if (Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) >= 29999)
                    {
                        accid = Convert.ToString(100000);
                    }
                    else
                    {
                        int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                        accid = Convert.ToString(gid);
                    }
                }
                else
                {
                    accid = Convert.ToString(10000);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ViewState["Whid"] + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {

                    int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                    accid = Convert.ToString(gid);
                }
                else
                {
                    accid = Convert.ToString(30000);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Other")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ViewState["Whid"] + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {

                    int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                    accid = Convert.ToString(gid);
                }
                else
                {
                    accid = Convert.ToString(30000);
                }
            }
        }

        else if (ddlPartyType.SelectedItem.Text == "Admin")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ViewState["Whid"] + "' ");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                    accid = Convert.ToString(gid);
                }
                else
                {
                    accid = Convert.ToString(30000);
                }
            }
        }





        if (groupid == 15)
        {
            classid = 5;
        }
        else if (groupid == 2)
        {
            classid = 1;
        }
        else if (groupid == 5)
        {
            classid = 1;
        }
        else if (groupid == 20)
        {
            classid = 5;
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        groupclass();
        Int32 AccountId = clsMaster.InsertAccountMasterParty(accid, classid, groupid, tbCompanyName.Text, System.DateTime.Today.ToShortDateString(), ViewState["Whid"].ToString());
        Session["maxaid"] = AccountId.ToString();
        accbalentry();
        Int32 partyid = clsMaster.InsertPartyMasterMess(Convert.ToInt32(ddlPartyType.SelectedValue), AccountId, tbCompanyName.Text, tbName.Text, tbAddress.Text, ddlCity.SelectedValue, ddlState.SelectedValue, ddlCountry.SelectedValue, tbWebsite.Text, tbEmail.Text, ViewState["Whid"].ToString());
        string ins6 = "insert into User_master(Name,Address ,City,State,Country,Phoneno,EmailID ,Username,Department,Accesslevel,PartyID,DesigantionMasterId,Active,Extention,zipcode)" +
                                       "values ('" + tbName.Text + "','" + tbAddress.Text + "','" + ddlCity.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCountry.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + tbUserName.Text + "','" + ddldept.SelectedValue + "','1','" + partyid + "','" + ddldesignation.SelectedValue + "','" + ddlActive.SelectedValue + "','0','0')";
        SqlCommand cmd6 = new SqlCommand(ins6, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd6.ExecuteNonQuery();

        con.Close();
        DataTable datUser = new DataTable();
        datUser = select("select max(UserID) as UserID from User_master where PartyID='" + partyid + "' ");
        if (datUser.Rows.Count > 0)
        {
            string ins7 = "insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + Convert.ToInt32(datUser.Rows[0]["UserID"]) + "','" + tbUserName.Text + "','" + tbPassword.Text + "','" + ddldept.SelectedValue + "','1','" + ddldept.SelectedValue + "','1')";
            SqlCommand cmd9 = new SqlCommand(ins7, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd9.ExecuteNonQuery();
            con.Close();

            string stremp = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
            " Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description) values('" + partyid + "','" + ddldept.SelectedValue + "', " +
            " '" + ddldesignation.SelectedValue + "','0','0','" + tbAddress.Text + "', " +
                " '" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + AccountId + "','" + AccountId + "','" + tbName.Text + "','" + ViewState["Whid"].ToString() + "','Capman')";
            SqlCommand cmdemp = new SqlCommand(stremp, con);
            con.Open();
            cmdemp.ExecuteNonQuery();
            con.Close();
        }
        string upme = "Update MsgMasterExt Set FromPartyId='" + partyid + "' where FromEmail='" + tbEmail.Text + "' and (FromPartyId IS NULL or FromPartyId='0' or FromPartyId='')";
        SqlCommand upmec = new SqlCommand(upme, con);
        con.Open();
        upmec.ExecuteNonQuery();
        con.Close();
        Label9.Text = "Record Added Successfully";
        Label9.Visible = true;
        ddlPartyType.SelectedIndex = 0;
        ddldept.SelectedIndex = 0;
        ddldesignation.SelectedIndex = 0;
        ddlActive.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
        tbName.Text = "";
        tbUserName.Text = "";
        tbPassword.Text = "";
        tbConPassword.Text = "";
        tbCompanyName.Text = "";
        tbName.Text = "";
        tbPhone.Text = "";
        tbEmail.Text = "";
        tbWebsite.Text = "";
        tbZipCode.Text = "";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ModalPopupExtender142422.Hide();
    }
    protected void chkfilter_CheckedChanged(object sender, EventArgs e)
    {
        if (chkfilter.Checked == true)
        {
            ModalPopupExtender4.Show();
            pnltype.Visible = true;
        }
        if (chkfilter.Checked == false)
        {
            ModalPopupExtender4.Hide();
            pnltype.Visible = false;
        }
    }
    protected void chkdate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkdate.Checked == true)
        {
            pnldate.Visible = true;
            RadioButtonList1_SelectedIndexChanged(sender, e);
            ModalPopupExtender4.Show();
            plllll.Visible = true;
        }
        if (chkdate.Checked == false)
        {
            pnldate.Visible = false;
            pnlfromto.Visible = false;
            pnlperiod.Visible = false;
            ModalPopupExtender4.Show();
        }
    }
    protected void chkuser_CheckedChanged(object sender, EventArgs e)
    {
        if (chkuser.Checked == true)
        {
            pnluser.Visible = true;
            RadioButtonList2_SelectedIndexChanged(sender, e);
            ModalPopupExtender4.Show();
            plllll.Visible = true;
        }
        if (chkuser.Checked == false)
        {
            pnluser.Visible = false;
            pnlusername.Visible = false;
            pnlusertype.Visible = false;
            ModalPopupExtender4.Show();
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            pnlfromto.Visible = true;
            pnlperiod.Visible = false;
            txteenddate.Text = System.DateTime.Now.ToShortDateString();
            txtestartdate.Text = System.DateTime.Now.ToShortDateString();
            ModalPopupExtender4.Show();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            ddlperiod_SelectedIndexChanged(sender, e);
            pnlfromto.Visible = false;
            pnlperiod.Visible = true;
            ModalPopupExtender4.Show();
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "0")
        {
            pnlusertype.Visible = true;
            pnlusername.Visible = false;
            ModalPopupExtender4.Show();
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            pnlusertype.Visible = false;
            pnlusername.Visible = true;
            ModalPopupExtender4.Show();
        }
    }
    protected void buttongo_Click(object sender, EventArgs e)
    {
        SelectMsgforInbox();
        ModalPopupExtender4.Show();
    }
    protected void ddlperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforInbox();
        ModalPopupExtender4.Show();
    }
    protected void ddlusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforInbox();
        ModalPopupExtender4.Show();
    }
    protected void ddlusername_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforInbox();
        ModalPopupExtender4.Show();
    }
    protected void ddlusertype1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillusername();
        SelectMsgforInbox();
        ModalPopupExtender4.Show();
    }

    protected void fillusertype()
    {
        ddlusertype.Items.Clear();

        string emprole = "select PartyTypeId,PartType from [PartytTypeMaster] where compid='" + Session["Comid"] + "' order by PartType";
        SqlCommand cmdrole = new SqlCommand(emprole, con);
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();
        darole.Fill(dtrole);

        ddlusertype.DataSource = dtrole;
        ddlusertype.DataTextField = "PartType";
        ddlusertype.DataValueField = "PartyTypeId";
        ddlusertype.DataBind();

        ddlusertype.Items.Insert(0, "All");
        ddlusertype.Items[0].Value = "0";

        //ddlusertype.SelectedIndex = ddlusertype.Items.IndexOf(ddlusertype.Items.FindByText("Employee"));
    }

    protected void fillusertype111()
    {
        ddlusertype1.Items.Clear();

        string emprole = "select PartyTypeId,PartType from [PartytTypeMaster] where compid='" + Session["Comid"] + "' order by PartType";
        SqlCommand cmdrole = new SqlCommand(emprole, con);
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();
        darole.Fill(dtrole);

        ddlusertype1.DataSource = dtrole;
        ddlusertype1.DataTextField = "PartType";
        ddlusertype1.DataValueField = "PartyTypeId";
        ddlusertype1.DataBind();

        ddlusertype1.Items.Insert(0, "All");
        ddlusertype1.Items[0].Value = "0";

        //ddlusertype.SelectedIndex = ddlusertype.Items.IndexOf(ddlusertype.Items.FindByText("Employee"));
    }

    protected void fillusername()
    {
        ddlusername.Items.Clear();

        if (ddlusertype1.SelectedItem.Text == "Candidate")
        {

            string emprole = "select CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename +' : '+ VacancyPositionTitleMaster.VacancyPositionTitle as CName,Party_master.PartyID from CandidateMaster inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.id=CandidateMaster.Jobpositionid inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartytTypeMaster.PartyTypeId='" + ddlusertype1.SelectedValue + "' and CandidateMaster.Active='1' order by CName";
            SqlCommand cmdrole = new SqlCommand(emprole, con);
            SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
            DataTable dtrole = new DataTable();
            darole.Fill(dtrole);

            ddlusername.DataSource = dtrole;
            ddlusername.DataTextField = "CName";
            ddlusername.DataValueField = "PartyID";
            ddlusername.DataBind();

            ddlusername.Items.Insert(0, "All");
            ddlusername.Items[0].Value = "0";

        }

        else if (ddlusertype1.SelectedItem.Text == "Employee")
        {

            string emprole = "SELECT distinct Employeemaster.EmployeeName as Name,Party_master.PartyID FROM Party_master inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID  where PartytTypeMaster.PartyTypeId='" + ddlusertype1.SelectedValue + "' and EmployeeMaster.Active='1' order by Name";
            SqlCommand cmdrole = new SqlCommand(emprole, con);
            SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
            DataTable dtrole = new DataTable();
            darole.Fill(dtrole);

            ddlusername.DataSource = dtrole;
            ddlusername.DataTextField = "Name";
            ddlusername.DataValueField = "PartyID";
            ddlusername.DataBind();

            ddlusername.Items.Insert(0, "All");
            ddlusername.Items[0].Value = "0";
        }
        else if (ddlusertype1.SelectedItem.Text == "Customer" || ddlusertype1.SelectedItem.Text == "Other" || ddlusertype1.SelectedItem.Text == "Vendor" || ddlusertype1.SelectedItem.Text == "Admin")
        {
            string emprole = "SELECT distinct Party_master.Compname +' : '+ Party_master.Contactperson as Name,Party_master.PartyID FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartytTypeMaster.PartyTypeId='" + ddlusertype1.SelectedValue + "' and AccountMaster.Status='1' order by Name";
            SqlCommand cmdrole = new SqlCommand(emprole, con);
            SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
            DataTable dtrole = new DataTable();
            darole.Fill(dtrole);

            ddlusername.DataSource = dtrole;
            ddlusername.DataTextField = "Name";
            ddlusername.DataValueField = "PartyID";
            ddlusername.DataBind();

            ddlusername.Items.Insert(0, "All");
            ddlusername.Items[0].Value = "0";
        }
        else
        {
            ddlusername.Items.Insert(0, "All");
            ddlusername.Items[0].Value = "0";
        }

    }

    protected void filldatebyperiod()
    {
        string Today, Yesterday, ThisYear;
        Today = Convert.ToString(System.DateTime.Today.ToShortDateString());
        Yesterday = Convert.ToString(System.DateTime.Today.AddDays(-1).ToShortDateString());
        ThisYear = Convert.ToString(System.DateTime.Today.Year.ToString());


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
        ViewState["thisweekstart"] = thisweekstart;
        string thisweekend = weekend.ToShortDateString();
        ViewState["thisweekend"] = thisweekend;

        //.................this week .....................


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
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            lastweekstart = d8;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            lastweekstart = d9;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            lastweekstart = d10;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            lastweekstart = d11;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            lastweekstart = d12;
            lastweekend = lastweekstart.Date.AddDays(+6);

        }
        else
        {
            lastweekstart = d13;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        string lastweekstartdate = lastweekstart.ToShortDateString();
        ViewState["lastweekstart"] = lastweekstartdate;
        string lastweekenddate = lastweekend.ToShortDateString();
        ViewState["lastweekend"] = lastweekenddate;

        //.................last week .....................

        DateTime d14, d15, d16, d171, d18, d19, d20;
        DateTime last2weekstart, last2weekend;

        d14 = Convert.ToDateTime(System.DateTime.Today.AddDays(-14).ToShortDateString());
        d15 = Convert.ToDateTime(System.DateTime.Today.AddDays(-15).ToShortDateString());
        d16 = Convert.ToDateTime(System.DateTime.Today.AddDays(-16).ToShortDateString());
        d171 = Convert.ToDateTime(System.DateTime.Today.AddDays(-17).ToShortDateString());
        d18 = Convert.ToDateTime(System.DateTime.Today.AddDays(-18).ToShortDateString());
        d19 = Convert.ToDateTime(System.DateTime.Today.AddDays(-19).ToShortDateString());
        d20 = Convert.ToDateTime(System.DateTime.Today.AddDays(-20).ToShortDateString());

        //string thisday = (System.DateTime.Today.DayOfWeek.ToString());
        if (thisday.ToString() == "Monday")
        {
            last2weekstart = d14;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            last2weekstart = d15;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            last2weekstart = d16;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            last2weekstart = d171;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            last2weekstart = d18;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            last2weekstart = d19;
            last2weekend = last2weekstart.Date.AddDays(+6);

        }
        else
        {
            last2weekstart = d20;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }

        string last2weekstartdate = last2weekstart.ToShortDateString();
        ViewState["last2weekstart"] = last2weekstartdate;
        //string last2weekenddate = last2weekend.ToShortDateString();
        //ViewState["last2week"] = last2weekenddate;



        //------------------this month period-----------------

        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        string thismonthstartdate = thismonthstart.ToShortDateString();
        ViewState["thismonthstartdate"] = thismonthstartdate;
        string thismonthenddate = Today.ToString();
        ViewState["thismonthenddate"] = thismonthenddate;

        //------------------this month period end................



        //-----------------last month period start ---------------

        // int last2monthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 2;



        int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
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
        ViewState["lastmonthstartdate"] = lastmonthstartdate;
        string lastmonthenddate = lastmonthend.ToString();
        ViewState["lastmonthenddate"] = lastmonthenddate;

        //-----------------last month period end -----------------------

        int last2monthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 2;
        string last2monthNumber = Convert.ToString(last2monthno.ToString());
        DateTime last2month = Convert.ToDateTime(last2monthNumber.ToString() + "/1/" + ThisYear.ToString());
        string last2monthstart = last2month.ToShortDateString();
        ViewState["last2monthstart"] = last2monthstart;

        //-----------------last 2 month period end -----------------------


        //--------------this year period start----------------------


        DateTime thisyearstart = Convert.ToDateTime("1/1/" + ThisYear.ToString());
        DateTime thisyearend = Convert.ToDateTime("12/31/" + ThisYear.ToString());

        string thisyearstartdate = thisyearstart.ToShortDateString();
        ViewState["thisyearstartdate"] = thisyearstartdate;
        string thisyearenddate = thisyearend.ToShortDateString();
        ViewState["thisyearenddate"] = thisyearenddate;

        //---------------this year period end-------------------



        //--------------last year period start----------------------


        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());

        string lastyearstartdate = lastyearstart.ToShortDateString();
        ViewState["lastyearstartdate"] = lastyearstartdate;
        string lastyearenddate = lastyearend.ToShortDateString();
        ViewState["lastyearenddate"] = lastyearenddate;



        //---------------last year period end-------------------

        DateTime last2yearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-2).Year.ToString());
        string last2yearstartdate = last2yearstart.ToShortDateString();
        ViewState["last2yearstartdate"] = last2yearstartdate;

        //---------------last 2 year period -------------------
    }


    //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (CheckBox1.Checked == true)
    //    {
    //        Panel1.Visible = true;
    //    }
    //    if (CheckBox1.Checked == false)
    //    {
    //        Panel1.Visible = false;
    //    }
    //}
    protected void btngo_Click(object sender, EventArgs e)
    {
        SelectMsgforInbox();
    }
    protected void chktaskproject_CheckedChanged(object sender, EventArgs e)
    {
        if (chktaskproject.Checked == true)
        {
            pnltaskproject.Visible = true;
            ModalPopupExtender4.Show();
            plllll.Visible = true;
        }
        if (chktaskproject.Checked == false)
        {
            pnltaskproject.Visible = false;
            ModalPopupExtender4.Show();
        }
    }

    protected void filltask()
    {
        string que = "select left(cast(TaskAllocationDate as nvarchar),12) + ' : ' + TaskName as Task,TaskId from TaskAllocationMaster where EmployeeId='" + Session["EmployeeId"].ToString() + "' and TaskAllocationDate between '" + ViewState["lastweekstart"] + "' and '" + System.DateTime.Now.ToShortDateString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(que, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddltask.DataSource = dt;
        ddltask.DataTextField = "Task";
        ddltask.DataValueField = "TaskId";
        ddltask.DataBind();

        ddltask.Items.Insert(0, "-Select-");
        ddltask.Items[0].Value = "0";
    }

    protected void fillproject()
    {
        string que = "select Projectid,ProjectName from ProjectMaster where EmployeeID='" + Session["EmployeeId"].ToString() + "' and Estartdate between '" + ViewState["lastmonthstartdate"] + "' and '" + System.DateTime.Now.ToShortDateString() + "' and Eenddate between '" + ViewState["lastmonthstartdate"] + "' and '" + System.DateTime.Now.ToShortDateString() + "' order by ProjectName asc";
        SqlDataAdapter da = new SqlDataAdapter(que, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlproject.DataSource = dt;
        ddlproject.DataTextField = "ProjectName";
        ddlproject.DataValueField = "Projectid";
        ddlproject.DataBind();

        ddlproject.Items.Insert(0, "-Select-");
        ddlproject.Items[0].Value = "0";
    }

    protected void ddlproject_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforInbox();
        ModalPopupExtender4.Show();
    }
    protected void ddltask_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforInbox();
        ModalPopupExtender4.Show();
    }
    protected void btnokkk_Click(object sender, EventArgs e)
    {
        Int32 MsgId = 0;
        if (gridInbox.Rows.Count > 0)
        {
            foreach (GridViewRow GR in gridInbox.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkMsg");
                if (chk.Checked == true)
                {
                    Int32 MsgDetailId = Convert.ToInt32(gridInbox.DataKeys[GR.RowIndex].Value);
                    DataTable dtMain = new DataTable();

                    dtMain = clsMessage.SelectMsgIdUsingMsgDetailIdExt(MsgDetailId);
                    if (dtMain.Rows.Count > 0)
                    {
                        MsgId = Convert.ToInt32(dtMain.Rows[0]["MsgId"].ToString());
                        //clsMessage.UpdateSpamEmail(MsgDetailId);
                        //clsMessage.UpdateMsgDetailExt(MsgDetailId, 4);
                    }
                    DataTable dtMain01 = new DataTable();
                    //  dtMain01 = clsMessage.SelectPartyEmailFromMsgId(MsgId);

                    SqlDataAdapter dla = new SqlDataAdapter("Select " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.FromPartyId from " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt where " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgId='" + MsgId + "'", con);
                    dla.Fill(dtMain01);

                    DataTable dtem = new DataTable();

                    if (dtMain01.Rows.Count > 0)
                    {
                        // SqlDataAdapter daem = new SqlDataAdapter("select MsgMasterExt.fromemail,MsgDetailExt.MsgId from MsgMasterExt inner join MsgDetailExt on MsgDetailExt.MsgId=MsgMasterExt.MsgId where MsgMasterExt.FromPartyId='" + dtMain01.Rows[0]["FromPartyId"].ToString() + "'", con);
                        SqlDataAdapter daem = new SqlDataAdapter("SELECT " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgId," + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.fromemail," + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgDetailId FROM " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt INNER JOIN " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt ON " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgId = " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgId INNER JOIN  " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgStatusMasterExt  ON " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgStatusId = " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgStatusMasterExt.MsgStatusId Left Join Party_Master ON " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.FromPartyId = Party_Master.PartyId WHERE (" + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.ToPartyId = '" + Convert.ToInt32(Session["PartyId"]) + "') AND (" + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgStatusId IN (1, 2))", con);
                        daem.Fill(dtem);
                    }

                    if (dtem.Rows.Count > 0)
                    {
                        for (int rowindex = 0; rowindex < dtem.Rows.Count; rowindex++)
                        {
                            if (dtem.Rows[rowindex]["MsgId"].ToString() != "")
                            {
                                //if (dtMain01.Rows[0]["FromPartyId"].ToString() != "")
                                //{
                                bool adem = clsMessage.InsertSpamEmail(dtem.Rows[rowindex]["fromemail"].ToString(), Convert.ToBoolean("False"), Convert.ToInt32(dtem.Rows[rowindex]["MsgId"]), Convert.ToInt32(Session["PartyId"]));
                                if (adem == true)
                                {

                                    //clsMessage.UpdateMsgDetailExt(MsgDetailId, 5);
                                    SqlCommand cmdup = new SqlCommand("UPDATE  " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt SET MsgStatusId = 5 where MsgDetailId='" + dtem.Rows[rowindex]["MsgDetailId"].ToString() + "'", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdup.ExecuteNonQuery();
                                    con.Close();

                                    lblmsg.Text = "Message inserted to Spamlist Successfully";
                                    lblmsg.Visible = true;
                                    pnlmsg.Visible = true;
                                }
                            }
                        }
                    }
                }

                SelectMsgforInbox();
            }
        }
    }
    protected void btnno_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Int32 MsgId = 0;

        if (gridInbox.Rows.Count > 0)
        {
            foreach (GridViewRow GR in gridInbox.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkMsg");
                if (chk.Checked == true)
                {
                    Int32 MsgDetailId = Convert.ToInt32(gridInbox.DataKeys[GR.RowIndex].Value);

                    DataTable dtMain = new DataTable();

                    dtMain = clsMessage.SelectMsgIdUsingMsgDetailIdExt(MsgDetailId);
                    if (dtMain.Rows.Count > 0)
                    {
                        MsgId = Convert.ToInt32(dtMain.Rows[0]["MsgId"].ToString());

                    }
                    DataTable dtMain01 = new DataTable();

                    SqlDataAdapter dla = new SqlDataAdapter("Select " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.FromPartyId from " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt where " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgId='" + MsgId + "'", con);
                    dla.Fill(dtMain01);

                    DataTable dtem = new DataTable();

                    if (dtMain01.Rows.Count > 0)
                    {
                        SqlDataAdapter daem = new SqlDataAdapter("SELECT " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgDetailId FROM " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt INNER JOIN " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt ON " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgId = " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.MsgId INNER JOIN  " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgStatusMasterExt  ON " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgStatusId = " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgStatusMasterExt.MsgStatusId Left Join Party_Master ON " + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgMasterExt.FromPartyId = Party_Master.PartyId WHERE (" + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.ToPartyId = '" + Convert.ToInt32(Session["PartyId"]) + "') AND (" + "[" + PageConn.extmsg11 + "]" + ".dbo.MsgDetailExt.MsgStatusId IN (1, 2))", con);
                        daem.Fill(dtem);
                    }
                    if (dtem.Rows.Count > 0)
                    {
                        for (int rowindex = 0; rowindex < dtem.Rows.Count; rowindex++)
                        {
                            clsMessage.UpdateMsgDetailExt(Convert.ToInt32(dtem.Rows[rowindex]["MsgDetailId"]), 4);
                            lblmsg.Text = "Message Deleted Successfully";
                            lblmsg.Visible = true;
                            pnlmsg.Visible = true;
                        }
                    }
                }
            }
            SelectMsgforInbox();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void chkMsg_CheckedChanged(object sender, EventArgs e)
    {
        int flag = 0;

        CheckBox chkhead = (CheckBox)gridInbox.HeaderRow.Cells[0].Controls[1];

        foreach (GridViewRow GR in gridInbox.Rows)
        {
            CheckBox chk = (CheckBox)GR.FindControl("chkMsg");

            if (flag != 1)
            {
                if (chk.Checked == true)
                {
                    ImageButton1.Visible = true;
                    count++;
                    flag = 1;
                }
                else
                {
                    ImageButton1.Visible = false;
                    count = 0;
                }
            }
            if (count > 1)
            {
                btnreply.Enabled = false;
            }
            if (count == 1)
            {
                if (chkhead.Checked == false)
                {
                    btnreply.Enabled = true;
                }
            }
        }
    }
    protected void btnreply_Click(object sender, EventArgs e)
    {
        if (gridInbox.Rows.Count > 0)
        {
            foreach (GridViewRow GR in gridInbox.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkMsg");
                if (chk.Checked == true)
                {
                    Int32 MsgDetailId = Convert.ToInt32(gridInbox.DataKeys[GR.RowIndex].Value);

                    Response.Redirect("MessageComposeExt.aspx?MsgDetailIdR=" + MsgDetailId);
                }
            }
        }
    }
    protected void btnforward_Click(object sender, EventArgs e)
    {
        if (gridInbox.Rows.Count > 0)
        {
            foreach (GridViewRow GR in gridInbox.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkMsg");
                if (chk.Checked == true)
                {
                    Int32 MsgDetailId = Convert.ToInt32(gridInbox.DataKeys[GR.RowIndex].Value);
                    Response.Redirect("MessageComposeExt.aspx?MsgDetailIdF=" + MsgDetailId);
                }
            }
        }
    }
    protected void HeaderChkbox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkhead = (CheckBox)gridInbox.HeaderRow.Cells[0].Controls[1];

        if (chkhead.Checked == true)
        {
            btnreply.Enabled = false;
            //btnforward.Enabled = false;
        }
        else
        {
            btnreply.Enabled = true;
            //btnforward.Enabled = true;
        }
    }
    protected void btnemailrule_Click(object sender, EventArgs e)
    {
        Response.Redirect("MessageProcessRules.aspx");
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            ModalPopupExtender3.Show();
        }
        if (CheckBox2.Checked == false)
        {
            ModalPopupExtender3.Hide();
        }
    }
    protected void fillextraemail()
    {
        SqlDataAdapter da1 = new SqlDataAdapter("select designationmasterid from employeemaster where employeemasterid='" + Convert.ToInt32(Session["EmployeeId"]) + "'", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        string str = "select Distinct InOutCompanyEmail.ID as CompanyEmailId,InOutCompanyEmail.InEmailID as EmailId  from InOutCompanyEmail inner join " +
                         "CompanyEmailAssignAccessRights  on InOutCompanyEmail.ID=CompanyEmailAssignAccessRights.CompanyEmailId  where " +
                         "CompanyEmailAssignAccessRights.CompanyEmailId in(select CompanyEmailAssignAccessRights.CompanyEmailId from CompanyEmailAssignAccessRights " +
                        "where (EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' OR DesignationID='" + dt1.Rows[0]["designationmasterid"].ToString() + "') and viewRights='true')";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        //dtadd.Merge(dt);

        GridView2.DataSource = dt;
        GridView2.DataBind();

        //foreach (GridViewRow gg in GridView2.Rows)
        //{
        //    CheckBox chk = (CheckBox)gg.FindControl("chkParty");
        //    {
        //        if (chk.Checked == true)
        //        {
        //            Label lalaparty = (Label)gg.FindControl("lalaparty");

        //            if (gg.RowIndex == 0)
        //            {
        //                dwnemail();
        //            }
        //            else
        //            {
        //                dwnemail1();
        //            }
        //        }
        //    }
        //}
    }
    protected void buttonGo11_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow GR in GridView1.Rows)
        {
            CheckBox chk = (CheckBox)GR.FindControl("chkParty");
            if (chk.Checked == true)
            {
                Label Label13 = (Label)GR.FindControl("Label13");
                ViewState["mailid"] = Label13.Text;
                SelectMsgforInbox();
            }
        }
        ModalPopupExtender3.Show();
    }
    //protected void HeaderChkbox_CheckedChanged1(object sender, EventArgs e)
    //{
    //    CheckBox chk = (CheckBox)GridView1.HeaderRow.Cells[0].Controls[1];
    //    for (int i = 0; i < GridView1.Rows.Count; i++)
    //    {
    //        CheckBox ch = (CheckBox)GridView1.Rows[i].Cells[0].Controls[1];
    //        ch.Checked = chk.Checked;
    //    }
    //    ModalPopupExtender3.Show();
    //}
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            panelsearch.Visible = true;
        }
        else
        {
            panelsearch.Visible = false;
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label Label13 = (Label)(e.Row.FindControl("Label13"));
            Label Label1we3 = (Label)(e.Row.FindControl("Label1we3"));

            CheckBox chk = (CheckBox)(e.Row.FindControl("chkParty"));

            if (e.Row.RowIndex == 0)
            {
                string count = "select count(" + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.MsgStatusId) as count2 from " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext where " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.ToEmailID='" + Label13.Text + "' and " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.MsgStatusId=1";
                SqlDataAdapter da1 = new SqlDataAdapter(count, con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                Label1we3.Text = Convert.ToString(dt1.Rows[0]["count2"]);

                chk.Checked = true;
                chk.Enabled = false;

                if (chk.Checked == true)
                {
                    Label lalaparty = (Label)(e.Row.FindControl("lalaparty"));

                    ViewState["emailids"] = lalaparty.Text;

                    dwnemail1();

                    Label lbl1 = (Label)(e.Row.FindControl("Label13"));

                    ViewState["lb"] = lbl1.Text;

                    SelectMsgforInbox();

                }
            }

            if (e.Row.RowIndex > 0)
            {

                string count = "select count(" + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.MsgStatusId) as count2 from " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext where " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.ToEmailID='" + Label13.Text + "' and " + "[" + PageConn.extmsg11 + "]" + ".dbo.msgdetailext.MsgStatusId=1";
                SqlDataAdapter da1 = new SqlDataAdapter(count, con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                Label1we3.Text = Convert.ToString(dt1.Rows[0]["count2"]);

                chk.Checked = false;
                chk.Enabled = false;

                dwnemail1();
            }
        }
    }
    protected void buttondual_Click(object sender, EventArgs e)
    {
        if (buttondual.Text == "Select")
        {
            foreach (GridViewRow GR in GridView2.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkParty");

                if (GR.RowIndex == 0)
                {
                    chk.Enabled = false;
                    buttondual.Text = "Go";
                }
                else
                {
                    chk.Enabled = true;
                    buttondual.Text = "Go";
                }
            }
        }

        if (buttondual.Text == "Go")
        {
            //foreach (GridViewRow GR in GridView2.Rows)
            //{
            //    CheckBox chk = (CheckBox)GR.FindControl("chkParty");

            //    if (chk.Checked == true)
            //    {
            //        Label lalaparty = (Label)GR.FindControl("lalaparty");

            //        ViewState["emailids"] = lalaparty.Text;

            //        if (GR.RowIndex > 0)
            //        {
            //            dwnemail1();
            //        }

            //        Label lbl1 = (Label)GR.FindControl("Label13");
            //        ViewState["lb"] = lbl1.Text;
            SelectMsgforInbox();
            //    }
            //}
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            gridInbox.AllowPaging = false;
            gridInbox.PageSize = 1000;
            SelectMsgforInbox();

            Button4.Text = "Hide Printable Version";
            Button7.Visible = true;

            if (gridInbox.Columns[0].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gridInbox.Columns[0].Visible = false;
            }

            if (gridInbox.Columns[7].Visible == true)
            {
                ViewState["editHides"] = "tt";
                gridInbox.Columns[7].Visible = false;
            }


        }
        else
        {
            gridInbox.AllowPaging = true;
            gridInbox.PageSize = 10;
            SelectMsgforInbox();

            Button4.Text = "Printable Version";
            Button7.Visible = false;

            if (ViewState["editHide"] != null)
            {
                gridInbox.Columns[0].Visible = true;
            }

            if (ViewState["editHides"] != null)
            {
                gridInbox.Columns[7].Visible = true;
            }
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        buttondual.Text = "Select";
        fillextraemail();
    }
}
