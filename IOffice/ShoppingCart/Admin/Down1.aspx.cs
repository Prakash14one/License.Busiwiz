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
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using ForAspNet.POP3;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO.Compression;
//using UtilitiesFTP.FTP;


public partial class Down : System.Web.UI.Page
{
    public delegate Int32 PDF2ImageCallback(int mode, string msg, IntPtr user_data);

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
    const string lic_key = "AGPVCWBRYBCDEPFD";

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
    public static string refnumber = "";
    public static string foldername = "";
    public static string doctype = "";
    public static string partyname = "";
    public static string doctitle = "";
    public static string docdate = "";
    public static string docAMT = "";

    SqlConnection con;

    DocumentCls1 clsDocument = new DocumentCls1();
    MasterCls clsMaster;
    Companycls ClsCompany = new Companycls();
    protected int DesignationId;
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
            this.Title = Session["CompanyName"] + " IFileCabinet.com - Auto Document Download  ";
        }

        Session["PageName"] = "Down.aspx";

        if (!Page.IsPostBack)
        {
            string str = "SELECT WareHouseId,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dte = new DataTable();
            da.Fill(dte);

            ddlbusiness.DataSource = dte;
            ddlbusiness.DataTextField = "Name";
            ddlbusiness.DataValueField = "WareHouseId";
            ddlbusiness.DataBind();

            DataTable dt = new DataTable();

            if (Request.QueryString["Id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);

                if (id == 1)
                {
                    RadioButtonList1.SelectedIndex = 0;
                }
                if (id == 2)
                {
                    RadioButtonList1.SelectedIndex = 1;
                }
                if (id == 3)
                {
                    RadioButtonList1.SelectedIndex = 2;

                }

            }


            RadioButtonList1_SelectedIndexChanged(sender, e);
        }

    }
    public void FillFolderGrid()
    {
        lblhea.Text = "Select Server Folder Path to Download";
        DataTable dt = new DataTable();
        dt = ClsCompany.SelectDownloadFolder(ddlbusiness.SelectedValue);
        grdDesignation.DataSource = dt;
        grdDesignation.DataKeyNames = new string[] { "FolderId" };
        grdDesignation.Columns[2].Visible = false;
        grdDesignation.Columns[1].HeaderText = "Server Folder";
        grdDesignation.DataBind();
        fillret();

    }
    public void FillFtpGrid()
    {
        lblhea.Text = "Select FTP Account Path to Download";
        DataTable dt = new DataTable();
        dt = clsDocument.SelectFtpMaster(ddlbusiness.SelectedValue);
        grdDesignation.DataSource = dt;
        grdDesignation.DataKeyNames = new string[] { "FtpId" };
        grdDesignation.Columns[1].HeaderText = "Ftp Account";
        grdDesignation.Columns[2].HeaderText = "User Name";
        grdDesignation.Columns[2].Visible = true;
        grdDesignation.DataBind();
        fillret();

    }
    public void FillEmailGrid()
    {
        lblhea.Text = "Select Email Account Path to Download";
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentEmailDownloadMaster(ddlbusiness.SelectedValue);
        grdDesignation.DataSource = dt;
        grdDesignation.DataKeyNames = new string[] { "DocumentEmailDownloadID" };
        grdDesignation.Columns[1].HeaderText = "Email Account";
        grdDesignation.Columns[2].HeaderText = "Server Name";
        grdDesignation.Columns[2].Visible = true;
        grdDesignation.DataBind();
        fillret();

    }

    protected void imgbShowEmp_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            MoveFromFTP();
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            MoveFromMail();
        }
        else if (RadioButtonList1.SelectedIndex == 2)
        {
            MoveFromFolder();
        }


    }
    protected void accbalentry()
    {

        DataTable ds153 = new DataTable();
        ds153 = select("select Report_Period_Id  from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + ddlbusiness.SelectedValue + "' and Active='1'");
        Session["reportid"] = ds153.Rows[0]["Report_Period_Id"].ToString();

        DataTable ds1531 = new DataTable();
        ds1531 = select("select Report_Period_Id from [ReportPeriod] where ReportPeriod.Report_Period_Id<'" + Session["reportid"] + "' and  Whid='" + ddlbusiness.SelectedValue + "'  order by Report_Period_Id Desc");
        Session["reportid1"] = ds1531.Rows[0]["Report_Period_Id"].ToString();

        string str4562 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid1"] + "')";
        SqlCommand cmd4562 = new SqlCommand(str4562, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd4562.ExecuteNonQuery();
        con.Close();


        string str456 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid"].ToString() + "')";
        SqlCommand cmd456 = new SqlCommand(str456, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd456.ExecuteNonQuery();
        con.Close();

    }
    protected int UserEntry()
    {
        DataTable dtt = new DataTable();
        string accid = "";
        dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and  Whid='" + ddlbusiness.SelectedValue + "'");
        if (dtt.Rows.Count > 0)
        {
            if (dtt.Rows[0]["aid"].ToString() != "")
            {
                //if(Convert.ToInt32(dtt.Rows[0]["aid"].ToString() >  )
                //{

                //}
                int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                accid = Convert.ToString(gid);
            }
            else
            {
                accid = Convert.ToString(30000);
            }
        }
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
        SqlCommand cmddept = new SqlCommand("Select id from DepartmentmasterMNC where Whid='" + ddlbusiness.SelectedValue + "'", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        object dept = cmddept.ExecuteScalar();
        con.Close();

        SqlCommand cmddesig = new SqlCommand("Select DesignationMasterId from DesignationMaster where DeptID='" + dept.ToString() + "'", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        object desig = cmddesig.ExecuteScalar();
        con.Close();
        EmployeeCls clsEmployee = new EmployeeCls();
        Int32 AccountId = clsMaster.InsertAccountMasterParty(accid, 5, 15, "test", System.DateTime.Today.ToShortDateString(), ddlbusiness.SelectedValue);
        Session["maxaid"] = AccountId.ToString();
        accbalentry();
        Int32 partyid = clsMaster.InsertPartyMasterMess(prttypeid, AccountId, "Capman", "Na", "Capman", "0", "0", "0", "epaza.us", "", ddlbusiness.SelectedValue);
        string ins6 = "insert into User_master(Name,Address ,City,State,Country,Phoneno,EmailID ,Username,Department,Accesslevel,PartyID,DesigantionMasterId,Active,Extention,zipcode)" +
                                       "values ('WellCapman','WellCapman','0','0','0','0','','Capman','" + dept + "','1','" + partyid + "','" + desig + "','True','0','0')";
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
            string ins7 = "insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + Convert.ToInt32(datUser.Rows[0]["UserID"]) + "','Capman','Capman','0','1','0','1')";
            SqlCommand cmd9 = new SqlCommand(ins7, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd9.ExecuteNonQuery();
            con.Close();

            string stremp = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
            " Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description) values('" + partyid + "','" + dept + "', " +
            " '" + desig + "','0','0','Capman', " +
                " '0','0','0','5675','','" + AccountId + "','" + AccountId + "','Capman','" + ddlbusiness.SelectedValue + "','Capman')";
            SqlCommand cmdemp = new SqlCommand(stremp, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdemp.ExecuteNonQuery();
            con.Close();



            string userid = datUser.Rows[0]["UserID"].ToString();

            SqlCommand cmd45 = new SqlCommand("Insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + userid + "','0','000','" + dept.ToString() + "','1','3','1')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd45.ExecuteNonQuery();
            con.Close();
            string str3 = "select Role_id from RoleMaster where Role_name='Admin' and compid='" + Session["comid"] + "' ";
            SqlCommand cmd34 = new SqlCommand(str3, con);
            SqlDataAdapter adp12 = new SqlDataAdapter(cmd34);
            DataTable dtlogin12 = new DataTable();
            adp12.Fill(dtlogin12);

            if (dtlogin12.Rows.Count > 0)
            {
                string roleid = dtlogin12.Rows[0]["Role_id"].ToString();


                SqlCommand cmd8 = new SqlCommand("Insert into User_Role (User_id,Role_id,ActiveDeactive) values ('" + userid + "','" + roleid + "','true ')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd8.ExecuteNonQuery();
                con.Close();

            }



        }

        return partyid;

    }
    public void MoveFromFolder()
    {
        int GenErr = 0;
        int i;
        i = 0;
        bool insdata;
        insdata = false;

        string location = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//");
        string location1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocumentsTemp//");
        DataTable dt = new DataTable();
        dt = ClsCompany.selectCompanyMaster();

        bool chked = false;
        if (grdDesignation.Rows.Count > 0)
        {
            for (i = 0; i < grdDesignation.Rows.Count; i++)
            {
                int fdidms = 0;
                try
                {

                    CheckBox chkdegs = (CheckBox)grdDesignation.Rows[i].FindControl("chkDesignation");

                    if (chkdegs.Checked == true)
                    {
                        chked = true;

                        Int32 FolderId;
                        FolderId = Convert.ToInt32(grdDesignation.DataKeys[i].Value.ToString());
                        fdidms = FolderId;
                        clsMaster = new MasterCls();
                        dt = new DataTable();
                        dt = clsDocument.SelectDownloadFolderIdwise(FolderId);
                        DataTable dtt = new DataTable();
                        if (dt.Rows.Count > 0)
                        {

                            string FolderPath = ""; //barodaoffice/port huron document"
                            // W:/websites/Capman.ifilecabinet 16-June-09/Capman.ifilecabinet_new/Account
                            string strpath = dt.Rows[0]["FolderName"].ToString();
                            string[] starr2 = strpath.Split('\\');

                            string drv = starr2[0].ToString();
                            string fldmn = starr2[1].ToString();
                            FileInfo[] file = null;
                            FolderPath = FolderPath + dt.Rows[0]["FolderName"].ToString();
                            FolderPath = FolderPath + @"\";
                            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(FolderPath);
                            file = dir.GetFiles();
                            int totdatasize = 25;
                            //if (file.Length < 25)
                            //{
                            totdatasize = file.Length;
                            //}
                            int totfile = 0;
                            for (int kl = 0; kl < totdatasize; kl++)
                            {
                                if (totfile < 25)
                                {
                                    refnumber = "";
                                    foldername = "";
                                    doctype = "";
                                    partyname = "";
                                    doctitle = "";
                                    docdate = "";
                                    docAMT = "";
                                    string filename1 = file[kl].ToString();
                                    if (System.IO.File.GetAttributes(FolderPath + filename1).ToString() != System.IO.FileAttributes.Hidden.ToString())
                                    {
                                        totfile += 1;
                                        string docdiscription = "";
                                        string flexnt = Path.GetExtension(location + filename1);
                                        string extstr = flexnt;

                                        if (Convert.ToString(dt.Rows[0]["DocumentFolderDownloadDefaultPropId"]) == "")
                                        {
                                            DynamicruleFielddata(filename1, flexnt);
                                            findfolparty();
                                        }
                                        else
                                        {
                                            doctitle = Convert.ToString(dt.Rows[0]["DocumentTittle"]);
                                            if (doctitle != "")
                                            {
                                            }
                                            doctitle = Convert.ToString(dt.Rows[0]["DocumentTittle"]);
                                            foldername = Convert.ToString(dt.Rows[0]["DocumentTypeId"]);
                                            partyname = Convert.ToString(dt.Rows[0]["PartyId"]);
                                            doctype = Convert.ToString(dt.Rows[0]["DocTypenm"]);
                                            docdiscription = Convert.ToString(dt.Rows[0]["DocumentDescription"]);

                                        }

                                        location = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//");
                                        location1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocumentsTemp//");
                                        if (foldername != "")
                                        {
                                            Int32 DocId = InsertDocEntry(Convert.ToBoolean(dt.Rows[0]["DocumentAutoApprove"]), filename1, docdiscription, flexnt);
                                            if (DocId > 0)
                                            {
                                                insdata = true;
                                                if (System.IO.File.Exists(location + filename1))
                                                {
                                                }
                                                else
                                                {
                                                    System.IO.File.Copy(FolderPath + filename1, location + filename1);
                                                }

                                                if (flexnt == ".pdf")
                                                {
                                                    string filepath = Server.MapPath("~//Account//pdftoimage.exe");
                                                    System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);


                                                    pti.Arguments = filepath + " -i UploadedDocuments//" + filename1 + " " + "-o" + " " + "DocumentImage//";
                                                    filepath += " " + "-r" + " " + "AGPVCWBRYBCDEPFD";

                                                    pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");

                                                    pti.UseShellExecute = false;
                                                    pti.RedirectStandardOutput = true;
                                                    pti.RedirectStandardInput = true;
                                                    pti.RedirectStandardError = true;

                                                    System.Diagnostics.Process ps = Process.Start(pti);

                                                    if (DocId > 0)
                                                    {
                                                        string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + filename1);
                                                        int ii = 0;
                                                        using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                                                        {
                                                            Regex regex = new Regex(@"/Type\s*/Page[^s]");
                                                            MatchCollection match = regex.Matches(st.ReadToEnd());
                                                            ii = match.Count;
                                                        }

                                                        int length = Convert.ToInt32(filename1.Length);
                                                        string docnameIn = filename1.ToString().Substring(0, length - 4);


                                                        for (int kk = 1; kk <= ii; kk++)
                                                        {
                                                            string scpf = docnameIn;
                                                            scpf = scpf + "0000" + kk + ".jpg";
                                                            clsEmployee.InserDocumentImageMaster(DocId, scpf);

                                                        }

                                                    }
                                                }
                                                System.IO.File.SetAttributes(FolderPath + filename1, System.IO.FileAttributes.Hidden);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (insdata == true)
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = " Retrieve Successfully.";

                    }
                    else if (chked == false)
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Please select atleast one path to download folder.";
                    }
                    else
                    {
                        if (GenErr == 0)
                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = "Number of documents not in specific path.";
                        }
                        chked = false;
                    }
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand ftpcmd11 = new SqlCommand("FolderAutoRetrivalMsgTbl", con);
                    ftpcmd11.CommandType = CommandType.StoredProcedure;
                    ftpcmd11.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.NVarChar));
                    ftpcmd11.Parameters["@FolderId"].Value = fdidms;
                    ftpcmd11.Parameters.Add(new SqlParameter("@LasttimeChecked", SqlDbType.DateTime));
                    ftpcmd11.Parameters["@LasttimeChecked"].Value = DateTime.Now.ToString();

                    ftpcmd11.Parameters.Add(new SqlParameter("@Message ", SqlDbType.NVarChar));
                    ftpcmd11.Parameters["@Message "].Value = lblmsg.Text;
                    ftpcmd11.ExecuteNonQuery();

                    con.Close();
                }
                catch (Exception ex)
                {   GenErr = 1;
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand ftpcmd11 = new SqlCommand("FolderAutoRetrivalMsgTbl", con);
                    ftpcmd11.CommandType = CommandType.StoredProcedure;
                    ftpcmd11.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.NVarChar));
                    ftpcmd11.Parameters["@FolderId"].Value = fdidms;
                    ftpcmd11.Parameters.Add(new SqlParameter("@LasttimeChecked", SqlDbType.DateTime));
                    ftpcmd11.Parameters["@LasttimeChecked"].Value = DateTime.Now.ToString();

                    ftpcmd11.Parameters.Add(new SqlParameter("@Message ", SqlDbType.NVarChar));
                    ftpcmd11.Parameters["@Message "].Value = ex.ToString();
                    ftpcmd11.ExecuteNonQuery();

                    con.Close();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Error Downloading Folder:" + ex.Message.ToString();
                }

            }
            
            if (chked == true)
            {
                fillret();
            }
        }
    }
    protected void findfolparty()
    {

        int folt = 0;
        int Partyde = 0;
        int doctdef = 0;
        if (foldername != "")
        {
            DataTable dtrc = select("Select  DocumentType.DocumentTypeId from DocumentType inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId where  DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and DocumentType='" + foldername + "'");
            if (dtrc.Rows.Count > 0)
            {
                folt = 1;
                foldername = Convert.ToString(dtrc.Rows[0]["DocumentTypeId"]);
            }
        }
        if (folt == 0)
        {
            foldername = "";
            DataTable dtgen = clsDocument.SelectGeneralFolderId(ddlbusiness.SelectedValue);
            if (dtgen.Rows.Count > 0)
            {
                if (dtgen.Rows[0]["DocumentTypeId"] != System.DBNull.Value)
                {
                    foldername = Convert.ToString(dtgen.Rows[0]["DocumentTypeId"]);
                }

            }



        }
        if (partyname != "")
        {
            DataTable dtminprt1 = clsDocument.SelectMinPartyIdfromPartyMaster(partyname, ddlbusiness.SelectedValue);
            if (dtminprt1.Rows.Count > 0)
            {
                Partyde = 1;
                partyname = Convert.ToString(dtminprt1.Rows[0]["PartyId"]);
            }
        }
        if (Partyde == 0)
        {
            partyname = "";
            DataTable dtminprt1 = clsDocument.SelectMinPartyIdfromPartyMasterName(ddlbusiness.SelectedValue);
            if (dtminprt1.Rows.Count > 0)
            {
                if (dtminprt1.Rows[0]["PartyId"] != System.DBNull.Value)
                {
                    partyname = Convert.ToString(dtminprt1.Rows[0][0]);
                }

            }

        }
        if (doctype != "")
        {
            DataTable dtrc = select("Select  id from DocumentTypenm where name='" + doctype + "'");
            if (dtrc.Rows.Count > 0)
            {
                doctdef = 1;
                doctype = Convert.ToString(dtrc.Rows[0]["id"]);
            }
        }
        if (doctdef == 0)
        {
            doctype = "1";
        }

    }

    protected Int32 InsertDocEntry(bool fillingdeskreq, string filename1, string docdiscription, string extstr)
    {

        if (foldername == "")
        {
            foldername = "0";
        }
        if (doctype == "")
        {
            doctype = "0";
        }
        if (partyname == "")
        {
            partyname = "0";
        }

        if (docdate == "")
        {
            docdate = DateTime.Now.ToShortDateString();

        }
        if (docAMT == "")
        {
            docAMT = "0";

        }
        int DocId = 0;
        DocId = clsDocument.InsertDocumentMaster(Convert.ToInt32(foldername), 2, Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), filename1.ToString(), doctitle, docdiscription, Convert.ToInt32(partyname), refnumber, Convert.ToDecimal(docAMT), 1, Convert.ToDateTime(docdate), extstr, doctype, "");
        if (DocId > 0)
        {
            if (fillingdeskreq == false)
            {
                bool dcaprv = true;
                bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), DocId, dcaprv);
            }
            else
            {
                bool insdata;
                DataTable dtt = new DataTable();
                DataTable DtMain = new DataTable();

                dtt = new DataTable();
                dtt = ClsCompany.selectAutoAllocationMaster(ddlbusiness.SelectedValue);
                if (dtt.Rows.Count > 0)
                {
                    foreach (DataRow drr in dtt.Rows)
                    {
                        Int32 empid = 0;
                        empid = Convert.ToInt32(drr["EmployeeId"].ToString());

                        insdata = false;

                        int accesslevel = 0;

                        DataTable dteeed = select(" select DesignationMaster.* from EmployeeMaster inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID where EmployeeMaster.EmployeeMasterID='" + empid + "'  ");

                        if (dteeed.Rows.Count > 0)
                        {
                            ViewState["DesignationName"] = dteeed.Rows[0]["DesignationName"].ToString();

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
                        }
                        else
                        {
                            accesslevel = 0;
                        }
                        string str1 = " INSERT INTO DocumentProcessing  (DocumentId ,EmployeeId,DocAllocateDate,CID,StatusId,Levelofaccess) VALUES  ('" + DocId + "' ,'" + empid + "','" + System.DateTime.Now.ToShortDateString() + "','" + Session["Comid"].ToString() + "','0','" + accesslevel + "') ";
                        SqlCommand cmd1 = new SqlCommand(str1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd1.ExecuteNonQuery();
                        con.Close();

                        //   insdata = clsDocument.insertDocumentProcessing(empid, rst);

                    }

                }
            }
            int rsts = clsDocument.InsertDocumentLog(DocId, Convert.ToInt32(Session["EmployeeId"]),
                               Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);
        }
        return DocId;
    }
    protected void DynamicruleFielddata(string filename, string extention)
    {

        filename = filename.Replace(extention, "");
        char[] separator1 = new char[] { '_' };
        string[] strSplitArr1 = filename.Split(separator1);
        int i111 = Convert.ToInt32(strSplitArr1.Length);
        for (int bh = 0; bh < i111; bh++)
        {
            char[] strcl = new char[] { '=' };
            string[] strSplitch = strSplitArr1[bh].Split(strcl);
            int maxsp = Convert.ToInt32(strSplitch.Length);
            if (maxsp == 2)
            {
                if (refnumber == "")
                {
                    if (strSplitch[0].ToUpper() == "REF")
                    {
                        refnumber = strSplitch[1];


                    }
                }
                if (foldername == "")
                {
                    if (strSplitch[0].ToUpper() == "FOLDER")
                    {
                        foldername = strSplitch[1];

                    }
                }
                if (doctype == "")
                {
                    if (strSplitch[0].ToUpper() == "TYPE")
                    {
                        doctype = strSplitch[1];

                    }
                }
                if (partyname == "")
                {
                    if (strSplitch[0].ToUpper() == "PARTY")
                    {
                        partyname = strSplitch[1];

                    }
                }
                if (doctitle == "")
                {
                    if (strSplitch[0].ToUpper() == "TITLE")
                    {
                        doctitle = strSplitch[1];

                    }
                }
                if (docdate == "")
                {
                    if (strSplitch[0].ToUpper() == "DT")
                    {
                        docdate = strSplitch[1];
                        if (docdate.Length != 8)
                        {
                            docdate = DateTime.Now.ToShortDateString();
                        }
                        else if (docdate.Length == 8)
                        {
                            string first1 = docdate.Substring(0, 2);
                            string first2 = docdate.Substring(2, 2);
                            string first3 = docdate.Substring(4, 4);
                            docdate = first1 + "/" + first2 + "/" + first3;
                        }



                    }
                }
                if (docAMT == "")
                {
                    if (strSplitch[0].ToUpper() == "AMT")
                    {
                        docAMT = strSplitch[1];

                    }
                }
            }
        }


    }
    protected void grdDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // try
        //{
        //    if (grdDesignation.Rows.Count > 0)
        //    {
        //        CheckBox cbHeader = (CheckBox)grdDesignation.HeaderRow.FindControl("HeaderChkboxDes");
        //        cbHeader.Attributes["onclick"] = "ChangeAllCheckBoxStatesDes(this.checked);";
        //        List<string> ArrayValuesdes = new List<string>();
        //        ArrayValuesdes.Add(string.Concat("'", cbHeader.ClientID, "'"));
        //        foreach (GridViewRow gvr in grdDesignation.Rows)
        //        {
        //            CheckBox cb = (CheckBox)gvr.FindControl("chkDesignation");
        //            cb.Attributes["onclick"] = "ChangeHeaderAsNeededDes();";
        //            ArrayValuesdes.Add(string.Concat("'", cb.ClientID, "'"));
        //        }
        //        CheckBoxIDsArrayDes.Text = "<script type='text/javascript'>" + "\n" + "<!--" + "\n" + String.Concat("var CheckBoxIDsDes =  new Array(", String.Join(",", ArrayValuesdes.ToArray()), ");") + "\n // -->" + "\n" + "</script>";
        //        //CheckBoxIDsArray.Text = "<script type=\"text/javascript\">" + Constants.vbCrLf + "<!--" + Constants.vbCrLf + string.Concat("var CheckBoxIDs = new Array(", string.Join(",", ArrayValues.ToArray()), ");") + Constants.vbCrLf + "// -->" + Constants.vbCrLf + "</script>";
        //    }
        //    else
        //    {
        //    }
        //}
        //catch (Exception ex)
        //{
        //    pnlmsg.Visible = true;
        //    lblmsg.Text = "Error in  DataBound : " + ex.Message.ToString();
        //}

    }

    protected void MoveDocument()
    {
        ///======Transfer Document and Add in to document master===============
        string filextn = "";
        string Location = Server.MapPath("~//Account//UploadedDocuments//");
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
        foreach (System.IO.FileInfo f in dir.GetFiles("*.pdf"))
        {
            string Location1 = Server.MapPath("~//Account//UploadedDocumentsTemp//");

            bool success = false;

            if (File.GetAttributes(Location + f.Name).ToString() != FileAttributes.Hidden.ToString())
            {
                string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + f.Name;
                //string filename = f.Name;
                File.Copy(Location + f.Name, Location1 + filename.ToString(), true);

                DataTable doc_email_down = new DataTable();
                //doc_email_down = clsDocument.SelectDocumentEmailDownloadMasterWithID();

                Int32 rst = clsDocument.InsertDocumentMaster(0, 2, Convert.ToDateTime(System.DateTime.Now.ToString()), filename.ToString(), "", "", 0, "", 0, 1, Convert.ToDateTime(System.DateTime.Now.ToString()), filextn, "1", "");


                File.SetAttributes(Location + f.Name, FileAttributes.Hidden);
            }
        }

    }


    protected void grdDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDesignation.PageIndex = e.NewPageIndex;

        if (RadioButtonList1.SelectedIndex == 0)
        {

            FillFtpGrid();
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {

            FillEmailGrid();
        }
        else if (RadioButtonList1.SelectedIndex == 2)
        {

            FillFolderGrid();
        }

    }
    protected void imgbtnreplay_Click(object sender, EventArgs e)
    {


    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            grdDesignation.DataSource = null;
            grdDesignation.DataBind();
            FillFtpGrid();
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            grdDesignation.DataSource = null;
            grdDesignation.DataBind();
            FillEmailGrid();
        }
        else if (RadioButtonList1.SelectedIndex == 2)
        {
            grdDesignation.DataSource = null;
            grdDesignation.DataBind();
            FillFolderGrid();
        }

    }

    protected void fillret()
    {

        foreach (GridViewRow grd in grdDesignation.Rows)
        {
            int Id = Convert.ToInt32(grdDesignation.DataKeys[grd.DataItemIndex].Value);
            Label lbllastrettime = (Label)grd.FindControl("lbllastrettime");
            Label lbllastretmsg = (Label)grd.FindControl("lbllastretmsg");
            DataTable dtt = new DataTable();
            if (RadioButtonList1.SelectedIndex == 0)
            {
                dtt = (DataTable)select("Select Top(1) AutoRetrivalMsgTbl.* from FtpMaster inner join AutoRetrivalMsgTbl on AutoRetrivalMsgTbl.Ftpid=FtpMaster.Ftpid  where FtpMaster.Ftpid='" + Id + "' order by AutoRetrivalMsgTbl.Id Desc");
                if (dtt.Rows.Count > 0)
                {
                    lbllastrettime.Text = Convert.ToString(dtt.Rows[0]["LasttimeChecked"]);
                    lbllastretmsg.Text = Convert.ToString(dtt.Rows[0]["Message"]);
                }
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                dtt = (DataTable)select("Select Top(1) AutoRetrivalMsgTbl.* from DocumentEmailDownloadMaster inner join AutoRetrivalMsgTbl on AutoRetrivalMsgTbl.EmailId=DocumentEmailDownloadMaster.DocumentEmailDownloadID  where DocumentEmailDownloadMaster.DocumentEmailDownloadID='" + Id + "' order by AutoRetrivalMsgTbl.Id Desc");
                if (dtt.Rows.Count > 0)
                {
                    lbllastrettime.Text = Convert.ToString(dtt.Rows[0]["LasttimeChecked"]);
                    lbllastretmsg.Text = Convert.ToString(dtt.Rows[0]["Message"]);
                }
            }
            else if (RadioButtonList1.SelectedIndex == 2)
            {
                dtt = (DataTable)select("Select Top(1) AutoRetrivalMsgTbl.* from DownloadFolder inner join AutoRetrivalMsgTbl on AutoRetrivalMsgTbl.FolderId=DownloadFolder.FolderId  where DownloadFolder.FolderId='" + Id + "' order by AutoRetrivalMsgTbl.Id Desc");
                if (dtt.Rows.Count > 0)
                {
                    lbllastrettime.Text = Convert.ToString(dtt.Rows[0]["LasttimeChecked"]);
                    lbllastretmsg.Text = Convert.ToString(dtt.Rows[0]["Message"]);
                }
            }
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
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList1_SelectedIndexChanged(sender, e);
    }
    protected void MoveFromFTP()
    {
        int GenErr = 0;
        int is1;
        is1 = 0;
        bool insdata;
        insdata = false;
        string name = "";
        try
        {
            DataTable dt = new DataTable();
            dt = ClsCompany.selectCompanyMaster();
            if (Convert.ToString(dt.Rows[0]["FtpAccount"]) == "True" || Convert.ToString(dt.Rows[0]["FtpAccount"]) == "False")
            {
                bool chked = false;
                if (grdDesignation.Rows.Count > 0)
                {
                    do
                    {
                        CheckBox chkdegs = (CheckBox)grdDesignation.Rows[is1].FindControl("chkDesignation");

                        if (chkdegs.Checked == true)
                        {
                            Int32 ftpid1 = 0; ;
                            try
                            {
                                chked = true;
                               
                              
                                ftpid1 = Convert.ToInt32(grdDesignation.DataKeys[is1].Value.ToString());
                                DataTable dt1 = new DataTable();
                                dt1 = clsDocument.SelectFTPMasterWithID(ftpid1);
                                if (dt1.Rows.Count > 0)
                                {
                                    lblmsg.Text = "";
                                    for (int h = 0; h < dt1.Rows.Count; h++)
                                    {
                                        string[] separatorft1 = new string[] { "/" };
                                        string[] strSplitArrft1 = dt1.Rows[0]["FTP"].ToString().Split(separatorft1, StringSplitOptions.RemoveEmptyEntries);

                                        String productno = strSplitArrft1[0].ToString();
                                        string ftpurl = "";

                                        if (productno == "FTP:" || productno == "ftp:")
                                        {
                                            if (strSplitArrft1.Length >= 3)
                                            {
                                                ftpurl = strSplitArrft1[0].ToString() + "//" + strSplitArrft1[1].ToString() + ":" + dt1.Rows[0]["Ftppath"];
                                                for (int i = 2; i < strSplitArrft1.Length; i++)
                                                {
                                                    ftpurl += "/" + strSplitArrft1[i].ToString();
                                                }
                                            }
                                            else
                                            {
                                                ftpurl = strSplitArrft1[0].ToString() + "//" + strSplitArrft1[1].ToString() + ":" + dt1.Rows[0]["Ftppath"];

                                            }
                                        }
                                        else
                                        {
                                            if (strSplitArrft1.Length >= 2)
                                            {
                                                ftpurl = "ftp://" + strSplitArrft1[0].ToString() + ":" + dt1.Rows[0]["Ftppath"];
                                                for (int i = 1; i < strSplitArrft1.Length; i++)
                                                {
                                                    ftpurl += "/" + strSplitArrft1[i].ToString();
                                                }
                                            }
                                            else
                                            {
                                                ftpurl = "ftp://" + strSplitArrft1[0].ToString() + ":" + dt1.Rows[0]["Ftppath"];

                                            }

                                        }
                                        int ftpid = Convert.ToInt32(dt1.Rows[h]["FtpId"]);


                                        string ftp1 = "";
                                        ftp1 = ftpurl + "/";

                                        string username = dt1.Rows[h]["Username"].ToString();
                                        string password = dt1.Rows[h]["Password"].ToString();

                                        string locationt = Server.MapPath("~//Account//" + Session["Comid"] + "//Ftpdowndoc//");
                                        string locationt1 = Server.MapPath("~//Account//" + Session["Comid"] + "//FtpdowndocTemp//");
                                        // DirectoryInfo dir = new DirectoryInfo(ftp);

                                       // FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.Create("ftp://72.38.84.230:21/i:/capmanversion/license.busiwiz.com/Account/UploadedDocuments");
                                        FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.Create(ftp1);
                                        //oFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                                        oFTP.Credentials = new NetworkCredential(username, password);
                                        oFTP.UseBinary = false;
                                        oFTP.UsePassive = true;
                                        oFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                                        FtpWebResponse response = (FtpWebResponse)oFTP.GetResponse();
                                        StreamReader sr = new StreamReader(response.GetResponseStream());
                                        string str = sr.ReadLine();
                                        List<string> oList = new List<string>();
                                        while (str != null)
                                        {
                                            if (str.Length > 3)
                                            {
                                                name = str.Trim();
                                                string extension = name.Substring(name.Length - 3);
                                                if (Convert.ToString(extension) == "pdf")
                                                {
                                                    oList.Add(str);
                                                }

                                            }
                                            str = sr.ReadLine();
                                        }
                                        sr.Close();
                                        response.Close();
                                        oFTP = null;
                                        int j = oList.Count;
                                        if (j > 25)
                                        {
                                            j = 25;
                                        }
                                        for (int i = 0; i < j; i++)
                                        {
                                           // string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + oList[i].ToString();
                                            string filename = oList[i].ToString();

                                            string destpath = locationt.ToString() + filename.ToString();
                                            string destpathImage = locationt1.ToString() + filename.ToString();
                                            GetFile(ftp1.ToString(), oList[i].ToString(), destpath.ToString(), username.ToString(), password.ToString());
                                            GetFile(ftp1.ToString(), oList[i].ToString(), destpathImage.ToString(), username.ToString(), password.ToString());
                                            DeleteFile(ftp1.ToString(), oList[i].ToString(), username.ToString(), password.ToString());

                                        }


                                        try
                                        {
                                            //string Location = Server.MapPath("~//Account//UploadedDocuments//");
                                            string Location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");
                                            string Location2 = Server.MapPath("~//Account//" + Session["Comid"] + "//Ftpdowndoc//");
                                            string Location3 = Server.MapPath("~//Account//" + Session["Comid"] + "//FtpdowndocTemp//");
                                            System.Threading.Thread.Sleep(5000);
                                            System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(Location2);
                                            foreach (System.IO.FileInfo f2 in dir2.GetFiles("*.*"))
                                            {
                                                refnumber = "";
                                                foldername = "";
                                                doctype = "";
                                                partyname = "";
                                                doctitle = "";
                                                docdate = "";
                                                docAMT = "";
                                              //  string Location1 = Server.MapPath("~//Account//" + Session["Comid"] + "//DocumentImage//");

                                                bool success = false;

                                                if (System.IO.File.GetAttributes(Location2 + f2.Name).ToString() != System.IO.FileAttributes.Hidden.ToString())
                                                {
                                                    string docdiscription = "";

                                                   

                                                    string filename1 = f2.Name.ToString();
                                                    string filename3 = f2.Name.ToString();
                                                    string fnam = f2.Name.Trim();
                                                    string extension1 = Path.GetExtension(Location2 + filename1);
                                                    string extstr = extension1;

                                                    if (Convert.ToString(dt1.Rows[0]["FTPMasterDefaultPropId"]) == "")
                                                    {
                                                        DynamicruleFielddata(filename1, extension1);
                                                        findfolparty();
                                                    }
                                                    else
                                                    {
                                                        doctitle = Convert.ToString(dt1.Rows[0]["DocumentTitle"]);
                                                        foldername = Convert.ToString(dt1.Rows[0]["DocumentTypeId"]);
                                                        partyname = Convert.ToString(dt1.Rows[0]["PartyId"]);
                                                        doctype = Convert.ToString(dt1.Rows[0]["DocTypenm"]);
                                                        docdiscription = Convert.ToString(dt1.Rows[0]["DocumentDescription"]);

                                                    }
                                                    if (foldername != "")
                                                    {

                                                        filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + doctitle.ToString().Replace(" ", "_");

                                                        string path1 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\Ftpdowndoc\\" + filename3.ToString());
                                                        string path2 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + filename1.ToString());
                                                        ViewState["filp"] = path2;


                                                        if (System.IO.File.Exists(path2))
                                                        {
                                                        }
                                                        else
                                                        {
                                                            File.Copy(path1, path2);
                                                        }
                                                        Int32 DocId = InsertDocEntry(Convert.ToBoolean(dt1.Rows[0]["DocumentAutoApprove"]), filename1, docdiscription, extstr);
                                                        if (DocId > 0)
                                                        {
                                                            insdata = true;
                                                            // string extension1 = fnam.Substring(fnam.Length - 3);
                                                            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
                                                            if (System.IO.File.Exists(Location + f2.Name))
                                                            {
                                                            }
                                                            else
                                                            {
                                                                System.IO.File.Copy(Location2 + f2.Name, Location + f2.Name);
                                                            }
                                                            System.IO.File.SetAttributes(Location + f2.Name, System.IO.FileAttributes.Hidden);
                                                            if (Convert.ToString(extension1) == ".pdf")
                                                            {


                                                                foreach (System.IO.FileInfo f in dir.GetFiles(filename1))
                                                                {

                                                                    string Location1 = Server.MapPath(@"~/Account/" + Session["comid"] + "/DocumentImage/");

                                                                    string filepath = Server.MapPath("~//Account//pdftoimage.exe");



                                                                    System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);


                                                                    //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
                                                                    //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");

                                                                    //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                                                                    pti.Arguments = filepath + " -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                                                                    filepath += " " + "-r" + " " + "AGPVCWBRYBCDEPFD";
                                                                    //  filepath += " " + "-r" + " " + "XIWMOMMTAGFCFDMD";

                                                                    pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");

                                                                    pti.UseShellExecute = false;
                                                                    pti.RedirectStandardOutput = true;
                                                                    pti.RedirectStandardInput = true;
                                                                    pti.RedirectStandardError = true;
                                                                    //pti.WorkingDirectory = "D:\\Capman.ifilecabinet.com1\\Account\\";

                                                                    System.Diagnostics.Process ps = Process.Start(pti);

                                                                    if (System.IO.File.Exists(Location2 + f.Name))
                                                                    {
                                                                    }
                                                                    else
                                                                    {
                                                                        System.IO.File.Copy(Location2 + f.Name, Location + f.Name);
                                                                    }
                                                                    System.IO.File.SetAttributes(Location2 + f.Name, System.IO.FileAttributes.Hidden);

                                                                    //}
                                                                }
                                                            }

                                                            //string argument = "-p " + physicalpath + " -v " + virtualfilename + " -u -f " + outputpath + " -fixednames";
                                                            // Process.Start(fullcompilerpath, argument).WaitForExit();
                                                            /////addede
                                                            if (extension1 == ".pdf")
                                                            {
                                                                int ii = 0;
                                                                string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + filename1);
                                                                using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                                                                {
                                                                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                                                                    MatchCollection match = regex.Matches(st.ReadToEnd());
                                                                    ii = match.Count;
                                                                }

                                                                int length = filename1.Length;
                                                                string docnameIn = filename1.Substring(0, length - 4);


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
                                                                    clsEmployee.InserDocumentImageMaster(DocId, scpf);

                                                                }
                                                            }
                                                            //////////////////////    foreach (System.IO.FileInfo f in dir.GetFiles(filename1))
                                                            //////////////////////    {

                                                            //////////////////////        string Location1 = Server.MapPath(@"~/Account/" + Session["comid"] + "/DocumentImage/");

                                                            //////////////////////        string filepath = Server.MapPath("~//Account//pdftoimage.exe");



                                                            //////////////////////        System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);


                                                            //////////////////////        //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
                                                            //////////////////////        //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");

                                                            //////////////////////        //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                                                            //////////////////////        pti.Arguments = filepath + " -i UploadedDocuments//" + filename1 + " " + "-o" + " " + "DocumentImage//";
                                                            //////////////////////        filepath += " " + "-r" + " " + "AGPVCWBRYBCDEPFD";
                                                            //////////////////////        //  filepath += " " + "-r" + " " + "XIWMOMMTAGFCFDMD";

                                                            //////////////////////        pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");

                                                            //////////////////////        pti.UseShellExecute = false;
                                                            //////////////////////        pti.RedirectStandardOutput = true;
                                                            //////////////////////        pti.RedirectStandardInput = true;
                                                            //////////////////////        pti.RedirectStandardError = true;

                                                            //////////////////////        //   string filepath = Server.MapPath("~//Account//pdftoimage.exe");
                                                            //////////////////////        //  System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);


                                                            //////////////////////        // pti.Arguments = filepath + " -i UploadedDocuments//" + filename1 + " " + "-o" + " " + "DocumentImage//";
                                                            //////////////////////        // filepath += " " + "-r" + " " + "AGPVCWBRYBCDEPFD";

                                                            //////////////////////        // pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");

                                                            //////////////////////        ////  pti.UseShellExecute = false;
                                                            //////////////////////        // pti.RedirectStandardOutput = true;
                                                            //////////////////////        //  pti.RedirectStandardInput = true;
                                                            //////////////////////        // pti.RedirectStandardError = true;

                                                            //////////////////////        System.Diagnostics.Process ps = Process.Start(pti);
                                                            //////////////////////        if (System.IO.File.Exists(Location + f2.Name))
                                                            //////////////////////        {

                                                            //////////////////////        }
                                                            //////////////////////        else
                                                            //////////////////////        {

                                                            //////////////////////            System.IO.File.Copy(Location2 + f2.Name, Location + f2.Name);
                                                            //////////////////////        }
                                                            //////////////////////        System.IO.File.SetAttributes(Location2 + f2.Name, System.IO.FileAttributes.Hidden);

                                                            //////////////////////    }


                                                            //////////////////////}
                                                            //////////////////////int ii = 0;
                                                            //////////////////////string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + filename1);

                                                            //////////////////////using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                                                            //////////////////////{
                                                            //////////////////////    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                                                            //////////////////////    MatchCollection match = regex.Matches(st.ReadToEnd());
                                                            //////////////////////    ii = match.Count;
                                                            //////////////////////}

                                                            ////////////////////////int length = Convert.ToInt32(filename1.Length);
                                                            ////////////////////////string docnameIn = filename1.ToString().Substring(0, length - 4);
                                                            //////////////////////int length = filename1.Length;
                                                            //////////////////////string docnameIn = "";

                                                            //////////////////////    docnameIn = filename1.Substring(0, length - 4);


                                                            //////////////////////for (int kk = 1; kk <= ii; kk++)
                                                            //////////////////////{
                                                            //////////////////////    string scpf = docnameIn;
                                                            //////////////////////    if (kk >= 1 && kk < 10)
                                                            //////////////////////    {
                                                            //////////////////////        scpf = scpf + "0000" + kk + ".jpg";
                                                            //////////////////////    }
                                                            //////////////////////    else if (kk >= 10 && kk < 100)
                                                            //////////////////////    {
                                                            //////////////////////        scpf = scpf + "000" + kk + ".jpg";
                                                            //////////////////////    }
                                                            //////////////////////    else if (kk >= 100)
                                                            //////////////////////    {
                                                            //////////////////////        scpf = scpf + "00" + kk + ".jpg";
                                                            //////////////////////    }
                                                            //////////////////////    clsEmployee.InserDocumentImageMaster(DocId, scpf);

                                                            //////////////////////}
                                                        }
                                                    }
                                                }

                                            }

                                        }
                                        catch (Exception es)
                                        {
                                            GenErr = 1;
                                            //  pnlmsg.Visible = true;
                                            lblmsg.Visible = true;

                                            lblmsg.Text = "Message : " + es.Message;

                                        }
                                        //pnlmsg.Visible = false;
                                    }////loops
                                    if (insdata == true)
                                    {
                                        lblmsg.Visible = true;
                                        lblmsg.Text = " Retrieve Successfully.";

                                    }
                                    else if (chked == false)
                                    {
                                        lblmsg.Visible = true;
                                        lblmsg.Text = "Please select atleast one path to download folder.";
                                    }
                                    else
                                    {
                                        if (GenErr == 0)
                                        {
                                            lblmsg.Visible = true;
                                            lblmsg.Text = "Number of documents not in specific path.";
                                            chked = false;
                                        }
                                    }
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    SqlCommand ftpcmd1 = new SqlCommand("FtpidAutoRetrivalMsgTbl", con);
                                    ftpcmd1.CommandType = CommandType.StoredProcedure;
                                    ftpcmd1.Parameters.Add(new SqlParameter("@Ftpid", SqlDbType.NVarChar));
                                    ftpcmd1.Parameters["@Ftpid"].Value = ftpid1;
                                    ftpcmd1.Parameters.Add(new SqlParameter("@LasttimeChecked", SqlDbType.DateTime));
                                    ftpcmd1.Parameters["@LasttimeChecked"].Value = DateTime.Now.ToString();

                                    ftpcmd1.Parameters.Add(new SqlParameter("@Message ", SqlDbType.NVarChar));
                                    ftpcmd1.Parameters["@Message "].Value = lblmsg.Text.ToString();
                                    ftpcmd1.ExecuteNonQuery();
                                    con.Close();

                                }
                            }
                            catch(Exception ex)
                            {
                                GenErr = 1;
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                SqlCommand ftpcmd1 = new SqlCommand("FtpidAutoRetrivalMsgTbl", con);
                                ftpcmd1.CommandType = CommandType.StoredProcedure;
                                ftpcmd1.Parameters.Add(new SqlParameter("@Ftpid", SqlDbType.NVarChar));
                                ftpcmd1.Parameters["@Ftpid"].Value = ftpid1;
                                ftpcmd1.Parameters.Add(new SqlParameter("@LasttimeChecked", SqlDbType.DateTime));
                                ftpcmd1.Parameters["@LasttimeChecked"].Value = DateTime.Now.ToString();

                                ftpcmd1.Parameters.Add(new SqlParameter("@Message ", SqlDbType.NVarChar));
                                ftpcmd1.Parameters["@Message "].Value = ex.ToString();
                                ftpcmd1.ExecuteNonQuery();
                                con.Close();
                                lblmsg.Visible = true;
                                lblmsg.Text = ex.ToString();
                            }
                        }
                        is1 = is1 + 1;
                    } while (is1 <= grdDesignation.Rows.Count - 1);
                   

                  
                    if (chked == true)
                    {
                        fillret();
                    }
                 
                }
            }
        }
        catch (Exception ex)
        {
            // pnlmsg.Visible = true;
            lblmsg.Visible = true;
            lblmsg.Text = "FTP Download Error:" + ex.Message.ToString();

        }
    }
    protected void imagefo(String filename1, Int32 DocId)
    {

        string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + filename1);
        int ii = 0;
        using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
        {
            Regex regex = new Regex(@"/Type\s*/Page[^s]");
            MatchCollection match = regex.Matches(st.ReadToEnd());
            ii = match.Count;
        }

        int length = filename1.Length;
        string docnameIn = filename1.Substring(0, length - 4);


        for (int kk = 1; kk <= ii; kk++)
        {
            string scpf = docnameIn;
            scpf = scpf + "0000" + kk + ".jpg";
            clsEmployee.InserDocumentImageMaster(DocId, scpf);

        }
        bool dcaprv = true;
        bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), DocId, dcaprv);
        //   pnlmsg.Visible = true;
        lblmsg.Visible = true;
        lblmsg.Text = " Retrieve Successfully.";

    }
    public bool GetFile(string ftp, string filename, string Destpath, string username, string password)
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
        return true;
    }
    public bool DeleteFile(string ftp, string filename, string username, string password)
    {
        FtpWebRequest oFTPDel = (FtpWebRequest)FtpWebRequest.
         Create(ftp.ToString() + filename.ToString());

        oFTPDel.Credentials = new NetworkCredential(username.ToString(), password.ToString());

        oFTPDel.Method = WebRequestMethods.Ftp.DeleteFile;

        FtpWebResponse responseDel = (FtpWebResponse)oFTPDel.GetResponse();
        FtpStatusCode oStatDel = responseDel.StatusCode;
        responseDel.Close();
        oFTPDel = null;
        return true;
    }

    public void MoveFromMail()
    {
        int i1;
        i1 = 0;
        bool insdata;
        insdata = false;
        try
        {

            string location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");
            string location1 = Server.MapPath("~//Account//" + Session["Comid"] + "//DocumentImageTemp//");
            DataTable dt = new DataTable();
            dt = ClsCompany.selectCompanyMaster();
            if (Convert.ToString(dt.Rows[0]["MailAccounts"]) == "True" || Convert.ToString(dt.Rows[0]["MailAccounts"]) == "False" || Convert.ToString(dt.Rows[0]["MailAccounts"]) == "")
            {
                bool chked = false;
                if (grdDesignation.Rows.Count > 0)
                {
                    do
                    {
                        CheckBox chkdegs = (CheckBox)grdDesignation.Rows[i1].FindControl("chkDesignation");
                        Int32 DocumentEmailDownloadID = 0;
                        if (chkdegs.Checked == true)
                        {
                            try
                            {

                                chked = true;
                                insdata = true;

                                DocumentEmailDownloadID = Convert.ToInt32(grdDesignation.DataKeys[i1].Value.ToString());
                                DataTable dt1 = new DataTable();
                                dt1 = clsDocument.SelectDocumentEmailDownloadMasterWithID(DocumentEmailDownloadID);
                                foreach (DataRow dr in dt1.Rows)
                                {


                                    bool i = DownloadMail(dr["ServerName"].ToString(), dr["EmailId"].ToString(), dr["Password"].ToString(), location.ToString(), Convert.ToInt32(dr["LastDownloadIndex"]), Convert.ToInt32(dr["DocumentEmailDownloadID"]), Convert.ToBoolean(dr["DocumentAutoApprove"]));
                                    //bool j = DownloadMail(dr["ServerName"].ToString(), dr["EmailId"].ToString(), dr["Password"].ToString(), Convert.ToDateTime(dr["LastDownloadedTime"].ToString()), location1.ToString(), Convert.ToInt32(dr["LastDownloadIndex"]));



                                }
                            }
                            catch (Exception exc)
                            {
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }

                                SqlCommand ftpcmd1 = new SqlCommand("EmailAutoRetrivalMsgTbl", con);
                                ftpcmd1.CommandType = CommandType.StoredProcedure;
                                ftpcmd1.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar));
                                ftpcmd1.Parameters["@EmailId"].Value = DocumentEmailDownloadID.ToString();
                                ftpcmd1.Parameters.Add(new SqlParameter("@LasttimeChecked", SqlDbType.DateTime));
                                ftpcmd1.Parameters["@LasttimeChecked"].Value = DateTime.Now.ToString();

                                ftpcmd1.Parameters.Add(new SqlParameter("@Message ", SqlDbType.NVarChar));
                                ftpcmd1.Parameters["@Message "].Value = exc.ToString();
                                ftpcmd1.ExecuteNonQuery();
                                con.Close();
                                lblmsg.Text = "Error Downloading Mail:" + exc.Message.ToString();
                                lblmsg.Visible = true;
                            }
                        }
                        i1 = i1 + 1;
                    } while (i1 <= grdDesignation.Rows.Count - 1);
                    if (insdata == false)
                    {
                        lblmsg.Visible = true;
                        //  pnlmsg.Visible = true;
                        lblmsg.Text = "ErrorPlease select atleast one path to download folder.";
                    }
                    else
                    {
                        fillret();
                    }



                }

            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error Downloading Mail:" + ex.Message.ToString();
            lblmsg.Visible = true;

        }
    }
    public bool DownloadMail(string server, string Email, string password, string DestPath, int LastDownloadIndex, int DocumentEmailDownloadID, bool docautoapprove)
    {
        bool insdata = false;
        string location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");
        string location12 = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");

        ForAspNet.POP3.License.LicenseKey = "D0NhcG1hbiBMdGQgUjI5OQEAAAABAAAA/z839HUoyiulFja7UZFbY3sZ6Q9mwIxPBhmGr7oX4+0PgLDF4APv+woUNOa+DYcN9XkD9r+SmFQ=";
        //docautoapprove = true;

        //ForAspNet.POP3.License.LicenseKey = "FWhhaXl1Z2FqamFyQGdtYWlsLmNvbQEAAAAAAAAAcIc1pXCsywgEUz0mjTOAafZyvZE+0LPIACuAJyphd1jmsRYlJEuuoBlkNXRgor8nstpwsL4z0l4=";
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
        // download 10 mail at a time for singe email id using count===========================
        int count = 0;

        for (int index = LastDownloadIndex + 1; index <= objPOP3.TotalMailCount; index++)
        {

            if (count <= 25)
            {
                count = count + 1;

                EmailMessage objEmail = objPOP3.GetMessage(index, false);

                //if (Convert.ToDateTime(objEmail.Date) > Convert.ToDateTime(lastDownloadTime))
                //{
                bool i = clsDocument.UpdateDocumentEmailLastDownloadIndex(DocumentEmailDownloadID, Convert.ToInt32(index));
                string msgsubject = objEmail.Subject.ToString();
                string fromparty = objEmail.From.ToString();
                string msgbody = objEmail.Body.ToString();
                if (objEmail.IsAnyAttachments)
                {

                    for (int attCount = 0; attCount < objEmail.Attachments.Count; attCount++)
                    {
                        ForAspNet.POP3.Attachment att = (Attachment)objEmail.Attachments[attCount];

                        if (att.IsFileAttachment)
                        {
                            //name = att.FileName.Trim();
                            //string extension = name.Substring(name.Length - 3);
                            //if (Convert.ToString(extension) == "pdf")
                            //{
                            //string filename1 = att.FileName + "_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + Email.ToString();
                            string filename1 = att.FileName;

                            att.Save(DestPath.ToString() + filename1.ToString());
                            if (System.IO.File.Exists(location12 + filename1))
                            {
                            }
                            else
                            {
                                File.Copy(DestPath.ToString() + filename1.ToString(), location12.ToString() + filename1.ToString(), true);
                            }

                          
                            string Location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");
                            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
                            foreach (System.IO.FileInfo f in dir.GetFiles(filename1))
                            {
                                refnumber = "";
                                foldername = "";
                                doctype = "";
                                partyname = "";
                                doctitle = "";
                                docdate = "";
                                docAMT = "";
                                string Location1 = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");
                                if (File.GetAttributes(location + f.Name).ToString() != FileAttributes.Hidden.ToString())
                                {
                                    
                                    if (System.IO.File.Exists(Location + f.Name))
                                    {
                                    }
                                    else
                                    {
                                        File.Copy(Location1 + f.Name, Location + filename1.ToString(), true);
                                    }

                                    string docdiscription = "";
                                    string flexnt = Path.GetExtension(location + f.Name);
                                    string extstr = flexnt;
                                    DataTable dt = new DataTable();
                                    dt = clsDocument.SelectDocumentEmailDownloadMasterWithID(DocumentEmailDownloadID);
                                    if (Convert.ToString(dt.Rows[0]["DocumentEmailDownloadDefaultPropId"]) == "")
                                    {
                                        DynamicruleFielddata(filename1, flexnt);
                                        findfolparty();
                                    }
                                    else
                                    {

                                        doctitle = Convert.ToString(dt.Rows[0]["DocumentTittle"]);
                                        foldername = Convert.ToString(dt.Rows[0]["DocumentTypeId"]);
                                        partyname = Convert.ToString(dt.Rows[0]["PartyId"]);
                                        doctype = Convert.ToString(dt.Rows[0]["DocTypenm"]);
                                        docdiscription = Convert.ToString(dt.Rows[0]["DocumentDescription"]);

                                    }


                                    if (foldername != "")
                                    {
                                        Int32 DocId = InsertDocEntry(Convert.ToBoolean(dt.Rows[0]["DocumentAutoApprove"]), filename1, docdiscription, flexnt);
                                        if (DocId > 0)
                                        {
                                            insdata = true;


                                            if (flexnt == ".pdf")
                                            {
                                                string filepath = Server.MapPath("~//Account//pdftoimage.exe");
                                                System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);


                                                pti.Arguments = filepath + " -i UploadedDocuments//" + filename1 + " " + "-o" + " " + "DocumentImage//";
                                                filepath += " " + "-r" + " " + "AGPVCWBRYBCDEPFD";

                                                pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");

                                                pti.UseShellExecute = false;
                                                pti.RedirectStandardOutput = true;
                                                pti.RedirectStandardInput = true;
                                                pti.RedirectStandardError = true;

                                                System.Diagnostics.Process ps = Process.Start(pti);

                                                if (DocId > 0)
                                                {
                                                    string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + filename1);
                                                    int ii = 0;
                                                    using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                                                    {
                                                        Regex regex = new Regex(@"/Type\s*/Page[^s]");
                                                        MatchCollection match = regex.Matches(st.ReadToEnd());
                                                        ii = match.Count;
                                                    }

                                                    int length = Convert.ToInt32(filename1.Length);
                                                    string docnameIn = filename1.ToString().Substring(0, length - 4);


                                                    for (int kk = 1; kk <= ii; kk++)
                                                    {
                                                        string scpf = docnameIn;
                                                        scpf = scpf + "0000" + kk + ".jpg";
                                                        clsEmployee.InserDocumentImageMaster(DocId, scpf);

                                                    }

                                                }
                                            }
                                            System.IO.File.SetAttributes(Location1 + filename1, System.IO.FileAttributes.Hidden);
                                        }
                                    }
                                }
                            }
                        }


                    }
                }

            }


        }
        string msdis = "";
        if (insdata == true)
        {
            msdis = "Data Retrieved Successfully";
        }
        else
        {
            msdis = "Number of documents not in specific path.";
        }
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlCommand ftpcmd11 = new SqlCommand("EmailAutoRetrivalMsgTbl", con);
        ftpcmd11.CommandType = CommandType.StoredProcedure;
        ftpcmd11.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar));
        ftpcmd11.Parameters["@EmailId"].Value = DocumentEmailDownloadID;
        ftpcmd11.Parameters.Add(new SqlParameter("@LasttimeChecked", SqlDbType.DateTime));
        ftpcmd11.Parameters["@LasttimeChecked"].Value = DateTime.Now.ToString();

        ftpcmd11.Parameters.Add(new SqlParameter("@Message ", SqlDbType.NVarChar));
        ftpcmd11.Parameters["@Message "].Value = msdis;
        ftpcmd11.ExecuteNonQuery();

        con.Close();
        lblmsg.Visible = true;
        // pnlmsg.Visible = true;
        lblmsg.Text = msdis;


        objPOP3.Close();

        return true;
    }

}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  