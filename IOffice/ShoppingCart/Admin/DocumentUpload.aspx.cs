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
using System.Runtime.InteropServices;
using System.Net;
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net.Mail;
public partial class Account_DocumentUpload : System.Web.UI.Page
{
    //SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString1"].ToString());
    SqlConnection con1;
    SqlConnection con;
    //==============================================================================================================
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
    DocumentCls1 clsDocument = new DocumentCls1();
    Companycls ClsCompany = new Companycls();
      protected int PartyId;
      protected static string flnam = "";
      EmployeeCls clsEmployee = new EmployeeCls();
    protected void Page_Load(object sender, EventArgs e)
    {

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        con1 = PageConn.licenseconn();
            pagetitleclass pg = new pagetitleclass();
            string strData = Request.Url.ToString();

            char[] separator = new char[] { '/' };

            string[] strSplitArr = strData.Split(separator);
            int i = Convert.ToInt32(strSplitArr.Length);
            string page = strSplitArr[i - 1].ToString();
			Session["PageUrl"]=strData;
            Session["PageName"] = page;
            Page.Title = pg.getPageTitle(page);

            double mul = 1073741824;

            //double plansize = Convert.ToDouble(Session["Size"].ToString());
            double plansize = 1;
            double sizeinbytes = mul * plansize;
            //string eeed = " Select distinct EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join User_master on User_master.PartyID=Party_master.PartyID where User_master.UserID='" + Session["userid"] + "'";
            //SqlCommand cmdeeed = new SqlCommand(eeed, con);
            //SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            //DataTable dteeed = new DataTable();
            //adpeeed.Fill(dteeed);
            //if (dteeed.Rows.Count > 0)
            //{
            //    Session["EmployeeId"] = Convert.ToString(dteeed.Rows[0]["EmployeeMasterID"]);
            //}
          
            if (Session["CompanyName"] != null)
            {
                this.Title = Session["CompanyName"] + " IFileCabinet.com - Document Upload";
            }
            Session["PageName"] = "DocumentUpload.aspx";
          //  pnlmsg.Visible = false;
            lblmsg.Visible = false;
            if (!IsPostBack)
            {
                
                TxtDocDate.Text = System.DateTime.Now.ToShortDateString();
                //FillDocumentMainType();
                string str = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";

                SqlCommand cmd1 = new SqlCommand(str, con);
                cmd1.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlbusiness.DataSource = dt;
                ddlbusiness.DataTextField = "Name";
                ddlbusiness.DataValueField = "WareHouseId";
                ddlbusiness.DataBind();
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
                hdbDId.Value = "0";
                hdbDMId.Value = "0";
                hdbDSId.Value = "0";
                hdbPartyId.Value = "0";
                //hdnSIZEofFOLDER.Value = "0";
                // imgbtnupload.Attributes.Add("OnClick", "Validate();");


                str = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments");
                GetFolderSize(str, false);


                //string[] ag = new string[0];
                //Main(ag);
               

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
   
    double plansize = 0;
   
   
    
  
    protected void FillDocumentTypeAll()
    {
        DocumentCls1 clsDocument = new DocumentCls1();
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocTypeAll(ddlbusiness.SelectedValue);
        ddldoctype.DataSource = dt;
        ddldoctype.DataTextField = "doctype";
        ddldoctype.DataValueField = "DocumentTypeId";
        ddldoctype.DataBind();
        //ddldoctype.Items.Insert(0, "--Select--");
        EventArgs e = new EventArgs();
        object sender = new object();
        ddldoctype_SelectedIndexChanged(sender, e);
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
        ddlpartyname.SelectedItem.Value = "0";
    }
    

    protected void FillDocumentTypeWithSubType(Int32 DocumentSubTypeId)
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentTypeWithSubType(DocumentSubTypeId);
        ddldoctype.DataSource = dt;
        ddldoctype.DataTextField = "DocumentType";
        ddldoctype.DataValueField = "DocumentTypeId";
        ddldoctype.DataBind();
        ddldoctype.Items.Insert(0, "-select-");
        ddldoctype.SelectedItem.Value = "0";
    }

    protected void clear()
    {
        txtdocdscrptn.Text = "";
        txtdocrefnmbr.Text = "";
        txtdoctitle.Text = "";
        txtnetamount.Text = "";
        ddldoctype.SelectedIndex = 0;
        //if (ddlmaindotype.Items.Count > 0) { ddlmaindotype.SelectedIndex = 0; }
        if (ddlpartyname.Items.Count > 0) { ddlpartyname.SelectedIndex = 0; }
        //if (ddlsubdoctype.Items.Count > 0) { ddlsubdoctype.SelectedIndex = 0; }
      
    }

    protected void ddlpartyname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpartyname.SelectedIndex > 0)
        {
            hdbPartyId.Value = (ddlpartyname.SelectedValue.ToString());
        }
        else
        {
            hdbPartyId.Value = "0";
        }
    }
 
    protected void ddldoctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldoctype.SelectedIndex > -1)
        {
            hdbDId.Value = ddldoctype.SelectedValue.ToString();
        }
        else
        {
            hdbDId.Value = "0";
        }
    }


    public long GetFolderSize(string dPath, bool includeSubFolders)
    {
        try
        {
            long size = 0;
            DirectoryInfo diBase = new DirectoryInfo(dPath);
            FileInfo[] files = null;
            if (includeSubFolders)
            {
                files = diBase.GetFiles("*", SearchOption.AllDirectories);
            }
            else
            {
                files = diBase.GetFiles("*", SearchOption.TopDirectoryOnly);
            }
            System.Collections.IEnumerator ie = files.GetEnumerator();

            //while   (  ie.MoveNext & files.e )
            //{

            //    size += ((FileInfo)ie.Current).Length;
            //}
            for (int i = 0; i < files.Length - 1; i++)
            {

                size += files[i].Length; //().ToString();
            }
            //size = 1073741824;
            string cid = "select PricePlanMaster.FoldersizeMB from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join ProductMaster on ProductMaster.ProductId =PricePlanMaster.ProductId where CompanyLoginId='" + Session["Comid"].ToString() + "'";


            SqlDataAdapter cidco = new SqlDataAdapter(cid, con1);
            DataTable dts = new DataTable();
            cidco.Fill(dts);
            double mb = size / 1048576;
            if (dts.Rows.Count > 0)
            {
                lblmsg.Visible = true; 
               // pnlmsg.Visible = true;
                if (Convert.ToString(dts.Rows[0]["FoldersizeMB"]) != "")
                {
                    lblmsg.Text = "Folder Size : Used " + mb + " MB out of Allowed " + Convert.ToString(dts.Rows[0]["FoldersizeMB"]) + " MB";
                }
                else
                {
                    lblmsg.Text = "Folder Size : Used " + mb + " MB out of Allowed " + 0 + " MB";

                }
            }
            return size;
            
             //hdnSIZEofFOLDER.Value = size.ToString();
        }
        catch (Exception ex)
        {
           // pnlmsg.Visible = true;
            lblmsg.Visible = true;
            lblmsg.Text = "Error :" +  ex.Message;
           
            return -1;
        }
    }
    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "documentname";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "documenttype";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "DocumentTitle";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "status";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "status";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;


        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "status";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;


        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "status";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "status";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "status";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);
        dt.Columns.Add(Dcom8);
        return dt;
    }

    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentTypeAll();
        FillParty();
    }
   
    protected void lnkadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Shoppingcart/Admin/DocumentSubSubType.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Shoppingcart/Admin/PartyMaster.aspx");
    }
   
    protected void imgbtnupload_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlpartyname.SelectedIndex > 0)
            {
                hdbPartyId.Value = (ddlpartyname.SelectedValue.ToString());
            }
            else
            {
                hdbPartyId.Value = "0";
            }
            if (ddldoctype.SelectedIndex > -1)
            {
                hdbDId.Value = ddldoctype.SelectedValue.ToString();
            }
            else
            {
                hdbDId.Value = "0";
            }
            double mul = 1073741824;

            plansize = 1;

            double sizeinbytes = mul * plansize;
            string str = "";
            str = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments");

            string Location22 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocumentsTemp//");
            long size = GetFolderSize(str, false);
            hdnSIZEofFOLDER.Value = size.ToString();


            if (fileuploadocurl.HasFile)
            {
                double foldsize = Convert.ToDouble(hdnSIZEofFOLDER.Value);
                double hasfilesize = Convert.ToDouble(fileuploadocurl.PostedFile.ContentLength.ToString());

                double totalsize = foldsize + hasfilesize;
                bool access = UserAccess.Usercon("DocumentMaster", "", "DocumentId", "", "", "CID", "DocumentMaster");
                if (access == true)
                {
                    //if (totalsize <= sizeinbytes)
                    //{
                        string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()+ "_" + fileuploadocurl.FileName.Replace(" ","_");
                        ViewState["filename"] = filename;
                        
                        fileuploadocurl.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\") + filename);
                        flnam = filename;
                        
                        string path2 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + filename.ToString());

                        ViewState["filp"] = path2;
                        string filexten = Path.GetExtension(path2);

                        Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(ddldoctype.SelectedValue), 2, Convert.ToDateTime(System.DateTime.Now.ToString()), filename.ToString(), txtdoctitle.Text, txtdocdscrptn.Text, Convert.ToInt32(ddlpartyname.SelectedValue), txtdocrefnmbr.Text, Convert.ToDecimal(txtnetamount.Text), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(TxtDocDate.Text), filexten);
                        if (rst > 0)
                        {
                            string Location12 = Server.MapPath("~//Account//" + Session["comid"] + "//DocumentImage//");
                            Array file1;
                            int nooffile1 = 0;
                            file1 = Directory.GetFiles(Location12);
                            nooffile1 = file1.Length;
                            //  Response.Write(nooffile1);
                            ViewState["rst"] = rst.ToString();
                            bool st = Chkautoprcss.Checked;
                            if (st.ToString() == "True")
                            {
                                bool dcaprv = true;
                                //bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["AdminEmpId"]), rst, dcaprv);
                                bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);

                            }
                            bool fnws = clsDocument.UpdateDocumentDateDetail(rst, Convert.ToDateTime(TxtDocDate.Text));
                            lblmsg.Visible = true;
                         //   pnlmsg.Visible = true;
                            int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);

                            //  System.IO.File.Move(str + filename, Location22 + filename);
                            lblmsg.Text = "Document Uploaded Successfully.";

                            //string[] ag=new string[0];
                            //Main(ag);
                            string Location = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//");
                            string Location2 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocumentsTemp//");
                            string ss = "", ss1 = "";
                            //string Location2 = "TestOutput/test2";
                            sendaccessmail();

                            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
                            bool cof = false;
                            foreach (System.IO.FileInfo f in dir.GetFiles("*.pdf"))
                            {
                                if (System.IO.File.GetAttributes(Location + f.Name).ToString() != System.IO.FileAttributes.Hidden.ToString())
                                {
                                    if (flnam != "")
                                    {
                                        if (f.Name.ToString() == flnam)
                                        {
                                            cof = true;
                                            string filepath = Server.MapPath("~//Account//pdftoimage.exe");
                                            System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);
                                            //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
                                            //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");
                                            pti.UseShellExecute = false;
                                            //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                                            //pti.Arguments = filepath;
                                            //pti.Arguments += " "+ "-r" + " " + "VNKSURDLWQOVHPGH";
                                            // pti.Arguments += " " + " -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";//+ " " + "-r" + "VNKSURDLWQOVHPGH";
                                            pti.Arguments = filepath + " -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";//+ " " + "-r" + "VNKSURDLWQOVHPGH";

                                            //pdftoimage.exe= -r XIWMOMMTAGFCFDMD;
                                            filepath += " " + "-r" + " " + "XIWMOMMTAGFCFDMD";
                                            pti.RedirectStandardOutput = true;
                                            pti.RedirectStandardInput = true;
                                            pti.RedirectStandardError = true;
                                            //pti.WorkingDirectory = "D:\\Capman.ifilecabinet.com1\\Account\\";
                                            pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");
                                            System.Diagnostics.Process ps = Process.Start(pti);

                                            if (System.IO.File.Exists(Location + f.Name))
                                            {

                                            }
                                            else
                                            {

                                                System.IO.File.Copy(Location2 + f.Name, Location + f.Name);
                                            }
                                            // System.IO.File.SetAttributes(Location2 + f.Name, System.IO.FileAttributes.Hidden);


                                        }
                                    }
                                }

                            }

                            if (cof == false)
                            {
                                if (System.IO.File.Exists(Location2 + flnam))
                                {
                                }
                                else
                                {
                                    System.IO.File.Copy(Location + flnam, Location2 + flnam);
                                }
                                System.IO.File.SetAttributes(Location + flnam, System.IO.FileAttributes.Hidden);
                            }



                            if (Chkautoprcss.Checked == false)
                            {
                                bool insdata;
                                DataTable dtt = new DataTable();
                                DataTable DtMain = new DataTable();
                                //DtMain = ClsCompany.selectCompanyMaster();
                                //if (DtMain.Rows.Count > 0)
                                //{
                                //    bool st1 = Convert.ToBoolean(DtMain.Rows[0]["AutoAllocation"].ToString());
                                //    if (st1.ToString() == "True")
                                //    {
                                dtt = new DataTable();
                                dtt = ClsCompany.selectAutoAllocationMaster(ddlbusiness.SelectedValue);
                                if (dtt.Rows.Count > 0)
                                {
                                    foreach (DataRow drr in dtt.Rows)
                                    {
                                        Int32 empid = 0;
                                        empid = Convert.ToInt32(drr["EmployeeId"].ToString());

                                        insdata = false;

                                        insdata = clsDocument.insertDocumentProcessing(empid, rst);

                                    }

                                }
                                //    }
                                //}
                            }
                            if (Request.QueryString["rrspd"] != null)
                            {
                                string strc = "Update F1_F2_TaxDedctionTbl Set RRSPDoc='" + rst + "' where Id='" + Request.QueryString["rrspd"] + "'";
                                SqlCommand cmaq = new SqlCommand(strc, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmaq.ExecuteNonQuery();
                                con.Close();
                            }
                            else if (Request.QueryString["f1d"] != null)
                            {
                                string strc = "Update F1_F2_TaxDedctionTbl Set F1Doc='" + rst + "' where Id='" + Request.QueryString["f1d"] + "'";
                                SqlCommand cmaq = new SqlCommand(strc, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmaq.ExecuteNonQuery();
                                con.Close();
                            }
                            else if (Request.QueryString["f2d"] != null)
                            {
                                string strc = "Update F1_F2_TaxDedctionTbl Set F2Doc='" + rst + "' where Id='" + Request.QueryString["f2d"] + "'";
                                SqlCommand cmaq = new SqlCommand(strc, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmaq.ExecuteNonQuery();
                                con.Close();
                            }
                           
                            clear();
                        }
                        /////addede
                        int ii = 0;
                        string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + filename);
                        using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                        {
                            Regex regex = new Regex(@"/Type\s*/Page[^s]");
                            MatchCollection match = regex.Matches(st.ReadToEnd());
                            ii = match.Count;
                        }

                        int length = filename.Length;
                        string docnameIn = filename.Substring(0, length - 4);


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
                            clsEmployee.InserDocumentImageMaster(rst, scpf);

                        }


                    //}
                    //else
                    //{
                    //    lblmsg.Visible = true;
                    //  //  pnlmsg.Visible = true;
                    //    lblmsg.Text = "Your Storage accede more than limit ";
                    //}
                }
                else
                {
                    lblmsg.Visible = true;
                 //   pnlmsg.Visible = true;
                    lblmsg.Text = "Sorry, You are not permitted for greater record to Priceplan";
                }
                

            }

        }
        catch (Exception es)
        {
            lblmsg.Visible = true;
           // pnlmsg.Visible = true;
            lblmsg.Text = "error : " + es.Message;
        }
        
    }
    protected void sendaccessmail()
    { string acess ="";
        string repumd ="";
        DataTable dt = select("Select distinct RuleDetail.RuleApproveTypeId, RuleMaster.RuleId, RuleDetail.EmployeeId,ConditionTypeId, EmployeeMaster.Email, RuleApproveTypeMaster.RuleApproveTypeName, RuleMaster.RuleDate,StepId,EmployeeMaster.EmployeeName, RuleMaster.RuleTitle,RuleDetail.RuleDetailId from RuleMaster inner join " +
                  "RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId= " +
                   " RuleDetail.EmployeeId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId where DocumentTypeId='" + ddldoctype.SelectedValue + "' and RuleMaster.Approvemail='1' order by RuleDetailId ASC");
        if (dt.Rows.Count > 0)
        {
            repumd += Convert.ToString(dt.Rows[0]["RuleId"]);
            if(repumd.Length>0)
            {
                acess = " and  RuleMaster.RuleId Not in(" + repumd + ")";
            }
            sendmail(dt);
        }
        DataTable dt1 = select("Select distinct RuleDetail.RuleApproveTypeId, RuleMaster.RuleId, RuleDetail.EmployeeId,ConditionTypeId, EmployeeMaster.Email, RuleApproveTypeMaster.RuleApproveTypeName, RuleMaster.RuleDate,StepId,EmployeeMaster.EmployeeName, RuleMaster.RuleTitle,RuleDetail.RuleDetailId from RuleMaster inner join " +
                      "RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId= " +
                       " RuleDetail.EmployeeId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId where DocumentSubId in(Select DocumentType.DocumentSubTypeId from DocumentType inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId  where DocumentTypeId='" + ddldoctype.SelectedValue + "') "+acess+" and RuleMaster.Approvemail='1' order by RuleDetailId ASC");
        if (dt1.Rows.Count > 0)
        {
            if (repumd.Length <= 0)
            {
                repumd += Convert.ToString(dt1.Rows[0]["RuleId"]);
              
            }
            else
            {
                repumd +=","+ Convert.ToString(dt1.Rows[0]["RuleId"]);
            }
            acess = " and  RuleMaster.RuleId Not in(" + repumd + ")";
            sendmail(dt1);
        }
        DataTable dt2 = select("Select distinct RuleDetail.RuleApproveTypeId, RuleMaster.RuleId, RuleDetail.EmployeeId,ConditionTypeId, EmployeeMaster.Email, RuleApproveTypeMaster.RuleApproveTypeName, RuleMaster.RuleDate,StepId,EmployeeMaster.EmployeeName, RuleMaster.RuleTitle,RuleDetail.RuleDetailId from RuleMaster inner join " +
                          "RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId= " +
                           " RuleDetail.EmployeeId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId where DocumentMainId in(Select DocumentMainType.DocumentMainTypeId from DocumentType inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId where DocumentTypeId='" + ddldoctype.SelectedValue + "') " + acess + " and RuleMaster.Approvemail='1' order by RuleDetailId ASC");
        if (dt2.Rows.Count > 0)
        {
            sendmail(dt2);
        }


    }
    public void sendmail(DataTable dtsc)
    {
        foreach (DataRow dts in dtsc.Rows)
        {
            if ((Convert.ToInt32(dts["ConditionTypeId"]) == 2) || (Convert.ToInt32(dts["ConditionTypeId"]) == 1 && Convert.ToInt32(dts["StepId"])==1))
            {
                string str = " Select Distinct CompanyMaster.CompanyName, EmployeeMaster.EmployeeName, OutGoingMailServer,WebMasterEmail, EmailMasterLoginPassword, AdminEmail, CompanyMaster.CompanyLogo,WarehouseMaster.Name as Wname,CityMasterTbl.CityName,StateMasterTbl.Statename,CountryMaster.CountryName from EmployeeMaster inner join WarehouseMaster on WarehouseMaster.WarehouseId=EmployeeMaster.Whid" +
         " inner join CompanyWebsitMaster on  CompanyWebsitMaster.Whid= WarehouseMaster.WarehouseId inner join " +
         " CompanyMaster on CompanyMaster.CompanyId=CompanyWebsitMaster.CompanyId inner join CompanyAddressMaster" +
         " on CompanyAddressMaster.CompanyMasterId=CompanyMaster.CompanyId inner join CountryMaster on " +
          "CountryMaster.CountryId=CompanyAddressMaster.Country inner join StateMasterTbl on " +
          "StateMasterTbl.StateId=CompanyAddressMaster.State inner join CityMasterTbl on " +
          "CityMasterTbl.CityId=CompanyAddressMaster.City where CompanyWebsitMaster.Whid='" + ddlbusiness.SelectedValue + "' and EmployeeMaster.EmployeeMasterId='" + dts["EmployeeId"] + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    SqlCommand cmdxx = new SqlCommand();
                    cmdxx.CommandText = "InsertEmailApproval";
                    cmdxx.CommandType = CommandType.StoredProcedure;

                    cmdxx.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
                    cmdxx.Parameters["@Whid"].Value = ddlbusiness.SelectedValue;
                    cmdxx.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
                    cmdxx.Parameters["@DocumentId"].Value = ViewState["rst"];
                    cmdxx.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
                    cmdxx.Parameters["@RuleId"].Value = dts["RuleId"];
                    cmdxx.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar));
                    cmdxx.Parameters["@UserId"].Value = dts["EmployeeId"];
                    cmdxx.Parameters.Add(new SqlParameter("@EmailSend", SqlDbType.NVarChar));
                    cmdxx.Parameters["@EmailSend"].Value = true;
                    cmdxx.Parameters.Add(new SqlParameter("@AnswerReceived", SqlDbType.NVarChar));
                    cmdxx.Parameters["@AnswerReceived"].Value = false;
                    cmdxx.Parameters.Add(new SqlParameter("@ApprovalReject", SqlDbType.NVarChar));
                    cmdxx.Parameters["@ApprovalReject"].Value = false;

                    cmdxx.Parameters.Add(new SqlParameter("@DocApprovalType", SqlDbType.NVarChar));
                    cmdxx.Parameters["@DocApprovalType"].Value = dts["RuleApproveTypeId"];
                    cmdxx.Parameters.Add(new SqlParameter("@DatetimeEmailSend", SqlDbType.NVarChar));
                    cmdxx.Parameters["@DatetimeEmailSend"].Value = DateTime.Now.ToString();
                    cmdxx.Parameters.Add(new SqlParameter("@DatetimeInventorySend", SqlDbType.NVarChar));
                    cmdxx.Parameters["@DatetimeInventorySend"].Value = DateTime.Now.ToString();
                    cmdxx.Parameters.Add(new SqlParameter("@ControlNo", SqlDbType.Int));
                    cmdxx.Parameters["@ControlNo"].Direction = ParameterDirection.Output;
                    cmdxx.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                    cmdxx.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
                    Int32 result = DatabaseCls1.ExecuteNonQueryep(cmdxx);
                    result = Convert.ToInt32(cmdxx.Parameters["@ControlNo"].Value);
                    
                    StringBuilder strhead = new StringBuilder();
                    strhead.Append("<table width=\"100%\"> ");
                    strhead.Append("<tr><td Width=\"80%\" align=\"left\"> <img src=" + Request.Url.Host.ToString() + "/ShoppingCart/images/" + Convert.ToString(dt.Rows[0]["CompanyLogo"]) + "\" \"border=\"0\" Width=\"176px\" Height=\"106px\" / > </td><td align=\"left\"><b><span style=\"color: #996600\">" + Convert.ToString(dt.Rows[0]["Wname"]) + "</span></b><BR>" + Convert.ToString(dt.Rows[0]["CityName"]) + "<Br>" + Convert.ToString(dt.Rows[0]["Statename"]) + "<Br>" + Convert.ToString(dt.Rows[0]["CountryName"]) + "</td></tr>  ");
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td><b>Dear " + Convert.ToString(dt.Rows[0]["EmployeeName"]) + ",</b></td></tr>");
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b> The following company has send you the following document for your approval kindly do need full in the matter.</b></td></tr>");
                    strhead.Append("<tr><td><table width=\"100%\">");
                    strhead.Append("<tr><td> Document Title :</td><td>" + txtdoctitle.Text + "</td></tr>");
                    strhead.Append("<tr><td> Cabinet-Drower-Folder :</td><td>" + ddldoctype.SelectedItem.Text + "</td></tr>");
                    strhead.Append("<tr><td> Document Approval Rule Type :</td><td>" + dts["RuleApproveTypeName"] + "</td></tr>");
                    strhead.Append("<tr><td>  Document Approval Rule Name :</td><td>" + dts["RuleTitle"] + "</td></tr>");
                    strhead.Append("</table></td></tr> ");
                      DataTable dt2 = select(" Select EmployeeMaster.EmployeeName, RuleDetail.RuleDetailId,DesignationMaster.DesignationName,DepartmentmasterMNC.Departmentname,RuleProcessDate from RuleMaster inner join RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId "+
  " inner join RuleProcessMaster on RuleProcessMaster.RuleDetailId=RuleDetail.RuleDetailId"+
  " inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId=  RuleProcessMaster.EmployeeId inner join DesignationMaster"+
  " on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId inner join DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptID where RuleProcessMaster.DocumentId='" + ViewState["rst"] + "' and RuleMaster.RuleId='" + dts["RuleId"] + "'");
                      int i = 0; 
                    foreach(DataRow dx in dt2.Rows)
                    {
                        if (i == 0)
                        {
                            strhead.Append("<tr><td><table width=\"100% BorderColor=Black BorderStyle=Solid\">");
                            strhead.Append("<tr><td colsplan=\"4\"><b>This Document as already approved by </b> </td>");
                            strhead.Append("<tr><td align=left>Employee Name </td><td  align=left>Designation Name</td> <td  align=left>Department Name</td>  <td  align=left>Aproval DateTime</td></tr>");
                        }
                       
                          strhead.Append("<tr><td align=left>" + dx["EmployeeName"] + " </td><td  align=left>" + dx["DesignationName"] + "</td> <td  align=left>" + dx["Departmentname"] + "</td>  <td  align=left>" + dx["RuleProcessDate"] + "</td></tr>");
                          i = i + 1;
                        
                      }
                    if (i > 0)
                    {
                        strhead.Append("</table></td></tr> ");
                       
                    }
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b>if you want to Approve/Reject above document,you can do it from here bellow. </b></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b><a href=http://" + Request.Url.Host.ToString() + "/EmailDocApprove.aspx?cn=" + result + "&rdt=" + dts["RuleDetailId"] + "&cid=" + Session["Comid"] + ">Approve</a></b></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b><a href=http://" + Request.Url.Host.ToString() + "/EmailDocApprove.aspx?ap=ync&cn=" + result + "&rdt=" + dts["RuleDetailId"] + "&cid=" + Session["Comid"] + ">Reject</a></b></td></tr>");
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td><b>Thanking You</b></td></tr>");
                    strhead.Append("<tr><td><b>Sincerely</b></td></tr>");
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td><b>For,</b></td></tr>");
                    strhead.Append("<tr><td><b> " + Convert.ToString(dt.Rows[0]["CompanyName"]) + "</b></td></tr>");
                    strhead.Append("</table> ");

                    string AdminEmail = dt.Rows[0]["WebMasterEmail"].ToString();// TextAdminEmail.Text;
                    //string AdminEmail = txtusmail.Text;
                    String Password = dt.Rows[0]["EmailMasterLoginPassword"].ToString();// TextEmailMasterLoginPassword.Text;

                    //string body = "Test Mail Server - TestIwebshop";
                    MailAddress to = new MailAddress(dts["Email"].ToString());
                   // MailAddress to = new MailAddress("maheshsorathiya500@gmail.com");
                    MailAddress from = new MailAddress(AdminEmail);
                   
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = "Document Approved by " + Convert.ToString(dt.Rows[0]["EmployeeName"]);

                    // if (RadioButtonList1.SelectedValue == "0")
                    {
                        objEmail.Body = strhead.ToString();
                        objEmail.IsBodyHtml = true;

                    }


                    objEmail.Priority = MailPriority.High;

                    Attachment attachFile = new Attachment(ViewState["filp"].ToString());
                    objEmail.Attachments.Add(attachFile);

                    SmtpClient client = new SmtpClient();

                    client.Credentials = new NetworkCredential(AdminEmail, Password);
                    client.Host = dt.Rows[0]["OutGoingMailServer"].ToString();


                    client.Send(objEmail);

                    

                }

            }
        }
    }
   
    protected DataTable select(string std)
    {
        SqlDataAdapter cidco = new SqlDataAdapter(std, con);
        DataTable dts = new DataTable();
        cidco.Fill(dts);
        return dts;
    }
    protected void imgbtnreset0_Click(object sender, EventArgs e)
    {
        clear();
        lblmsg.Visible = false;
       // pnlmsg.Visible = false;
    }

    protected void ImageButton48_Click(object sender, ImageClickEventArgs e)
    {
        FillDocumentTypeAll();
    }
    protected void ImageButton49_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentSubSubType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton51_Click(object sender, ImageClickEventArgs e)
    {
        FillParty();
    }
    protected void ImageButton50_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PartyMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
}
