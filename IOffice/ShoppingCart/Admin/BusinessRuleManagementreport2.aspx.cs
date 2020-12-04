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
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
public partial class BusinessRuleManagementreport2 : System.Web.UI.Page
{
    SqlConnection con;
    MasterCls clsMaster = new MasterCls();
    DataTable dt = new DataTable();
    InstructionCls clsInstruction = new InstructionCls();
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    protected void FillRuleGrid(string sortExp, string sortDir)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        DataTable DtMain = new DataTable();

        //
        DataColumn dtcom1 = new DataColumn();
        dtcom1.DataType = System.Type.GetType("System.String");
        dtcom1.ColumnName = "DocId";
        dtcom1.ReadOnly = false;
        dtcom1.Unique = false;
        dtcom1.AllowDBNull = true;

        DtMain.Columns.Add(dtcom1);
        DataColumn dtcom2 = new DataColumn();
        dtcom2.DataType = System.Type.GetType("System.String");
        dtcom2.ColumnName = "DocumentTitle";
        dtcom2.ReadOnly = false;
        dtcom2.Unique = false;
        dtcom2.AllowDBNull = true;
        DtMain.Columns.Add(dtcom2);

        DataColumn dtcom3 = new DataColumn();
        dtcom3.DataType = System.Type.GetType("System.String");
        dtcom3.ColumnName = "RuleProcessDate";

        dtcom3.ReadOnly = false;
        dtcom3.Unique = false;
        dtcom3.AllowDBNull = true;
        DtMain.Columns.Add(dtcom3);

        //DataColumn dtcom4 = new DataColumn();
        //dtcom4.DataType = System.Type.GetType("System.String");
        //dtcom4.ColumnName = "RuleApproveTypeName";
        //dtcom4.ReadOnly = false;
        //dtcom4.Unique = false;
        //dtcom4.AllowDBNull = true;
        //DtMain.Columns.Add(dtcom4);

        DataColumn dtcom5 = new DataColumn();
        dtcom5.DataType = System.Type.GetType("System.String");
        dtcom5.ColumnName = "RuleId";
        dtcom5.ReadOnly = false;
        dtcom5.Unique = false;
        dtcom5.AllowDBNull = true;
        DtMain.Columns.Add(dtcom5);

        DataColumn dtcom6 = new DataColumn();
        dtcom6.DataType = System.Type.GetType("System.String");
        dtcom6.ColumnName = "RuleDetailId";
        dtcom6.ReadOnly = false;
        dtcom6.Unique = false;
        dtcom6.AllowDBNull = true;
        DtMain.Columns.Add(dtcom6);
        DataColumn dtcom7 = new DataColumn();
        dtcom7.DataType = System.Type.GetType("System.String");
        dtcom7.ColumnName = "RuleTitle";
        dtcom7.ReadOnly = false;
        dtcom7.Unique = false;
        dtcom7.AllowDBNull = true;
        DtMain.Columns.Add(dtcom7);
        //

        DataColumn dtcom8 = new DataColumn();
        dtcom8.DataType = System.Type.GetType("System.String");
        dtcom8.ColumnName = "RuleTypeName";
        dtcom8.ReadOnly = false;
        dtcom8.Unique = false;
        dtcom8.AllowDBNull = true;
        DtMain.Columns.Add(dtcom8);


        DataColumn dtcom9 = new DataColumn();
        dtcom9.DataType = System.Type.GetType("System.String");
        dtcom9.ColumnName = "ConditionTypeName";
        dtcom9.ReadOnly = false;
        dtcom9.Unique = false;
        dtcom9.AllowDBNull = true;
        DtMain.Columns.Add(dtcom9);

        DataColumn dtcom10 = new DataColumn();
        dtcom10.DataType = System.Type.GetType("System.String");
        dtcom10.ColumnName = "RuleApproveTypeName";
        dtcom10.ReadOnly = false;
        dtcom10.Unique = false;
        dtcom10.AllowDBNull = true;
        DtMain.Columns.Add(dtcom10);


        DataColumn dtcom11 = new DataColumn();
        dtcom11.DataType = System.Type.GetType("System.String");
        dtcom11.ColumnName = "Note";
        dtcom11.ReadOnly = false;
        dtcom11.Unique = false;
        dtcom11.AllowDBNull = true;
        DtMain.Columns.Add(dtcom11);


        DataColumn dtcom12 = new DataColumn();
        dtcom12.DataType = System.Type.GetType("System.String");
        dtcom12.ColumnName = "EmployeeName";
        dtcom12.ReadOnly = false;
        dtcom12.Unique = false;
        dtcom12.AllowDBNull = true;
        DtMain.Columns.Add(dtcom12);

        DataColumn dtcom13 = new DataColumn();
        dtcom13.DataType = System.Type.GetType("System.String");
        dtcom13.ColumnName = "DesignationName";
        dtcom13.ReadOnly = false;
        dtcom13.Unique = false;
        dtcom13.AllowDBNull = true;
        DtMain.Columns.Add(dtcom13);


        DataColumn dtcom14 = new DataColumn();
        dtcom14.DataType = System.Type.GetType("System.String");
        dtcom14.ColumnName = "Departmentname";
        dtcom14.ReadOnly = false;
        dtcom14.Unique = false;
        dtcom14.AllowDBNull = true;
        DtMain.Columns.Add(dtcom14);
        if (Session["EmployeeId"] != null)
        {
            Int32 EmpId = Convert.ToInt32(Session["EmployeeId"]);
            dt = clsInstruction.SelectRuleDetailAllDocIdWise(Convert.ToInt32(ViewState["DocId"].ToString()), ddlbusiness.SelectedValue); 
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow DtMainRow = DtMain.NewRow();
                    DtMainRow["DocId"] = dr["DocumentId"].ToString();

                    DtMainRow["Departmentname"] = dr["Departmentname"].ToString();

                    DtMainRow["DesignationName"] = dr["DesignationName"].ToString();
                    DtMainRow["EmployeeName"] = dr["EmployeeName"].ToString();

                    DtMainRow["DocumentTitle"] = dr["DocumentTitle"].ToString();
                    DtMainRow["RuleProcessDate"] = dr["RuleProcessDate"].ToString();
                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                    DtMainRow["RuleDetailId"] = "34"; 
                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                    DtMainRow["RuleTypeName"] = dr["RuleTypeName"].ToString();
                    DtMainRow["ConditionTypeName"] = dr["ConditionTypeName"].ToString();
                    DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                    DtMainRow["Note"] = dr["Note"].ToString();
                    DtMain.Rows.Add(DtMainRow);

                }

            }



            DataView myDataView = new DataView();
            myDataView = DtMain.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView2.DataSource = DtMain;
            GridView2.DataBind();
        }
        else
        {
            Response.Redirect("~/Shoppingcart/Admin/Shoppingcartlogin.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        lblmsg.Text = "";


        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);

        if (!Page.IsPostBack)
        {
            string str = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";
            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dtx = new DataTable();
            da.Fill(dtx);

            ddlbusiness.DataSource = dtx;
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
            ViewState["sortOrder"] = "";

            if (Request.QueryString["DocId"] != null)
            {
                ViewState["DocId"] = Request.QueryString["DocId"].ToString();
                DataTable dt = new DataTable();

                string cvv = "Select DocumentMainType.Whid from   DocumentMaster inner join  DocumentType on DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId  inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId where DocumentMaster.DocumentId='" + ViewState["DocId"] + "'";
                SqlCommand cmd1q = new SqlCommand(cvv, con);
                cmd1q.CommandType = CommandType.Text;
                SqlDataAdapter daq = new SqlDataAdapter(cmd1q);
                DataTable dtxq = new DataTable();
                daq.Fill(dtxq);
                if (dtxq.Rows.Count > 0)
                {
                    //ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dtxq.Rows[0]["Whid"].ToString()));
                    ddlbusiness.SelectedValue = dtxq.Rows[0]["Whid"].ToString();

                }
                dt = clsDocument.SelectDocumentMasterByDocId(Convert.ToInt32(ViewState["DocId"].ToString()), ddlbusiness.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    lblcomname.Text = Session["Cname"] + " - " + ddlbusiness.SelectedItem.Text;
                    lblDocID.Text = ViewState["DocId"].ToString();
                    lblDocName.Text = dt.Rows[0]["DocumentTitle"].ToString();
                    lblDocDate.Text = dt.Rows[0]["DocumentUploadDate"].ToString();
                    lblPartyName.Text = dt.Rows[0]["PartyName"].ToString();
                    FillGrid();
                    FillRuleGrid("", "");
                }

                if (GridView2.Rows.Count > 0 || grd_general.Rows.Count > 0 || GridView1.Rows.Count > 0)
                {
                    Panel5.Visible = false;
                    Panel4.Visible = true;
                }
                else
                {
                    Panel5.Visible = true;
                    Panel4.Visible = false;
                }
            }
        }

    }
    protected void FillGrid()
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentProcesswithAllEmployee(Convert.ToInt32(ViewState["DocId"].ToString()));

        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();
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
        catch (Exception ecx)
        {
        }
        return true;
    }


    protected void ibtnSearchShow_Click(object sender, EventArgs e)
    {
        hdnsortExp.Value = null;
        hdnsortDir.Value = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView2.DataSource = null;
        GridView2.DataBind();
        grd_general.DataSource = null;
        grd_general.DataBind();
        if (RadioButton1.Checked == true)
        {
            
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDocumentMasterByDocId(Convert.ToInt32(txtDocTitle.Text), ddlbusiness.SelectedValue);
            if (dt.Rows.Count > 0)
            {

                lblcomname.Text = Session["Cname"] + " - " + ddlbusiness.SelectedItem.Text;
                lblDocID.Text = dt.Rows[0]["DocumentId"].ToString();  // txtDocTitle.Text.ToString();
                ViewState["DocId"] = lblDocID.Text;
                lblDocName.Text = dt.Rows[0]["DocumentTitle"].ToString();
                lblDocDate.Text = dt.Rows[0]["DocumentUploadDate"].ToString();
                lblPartyName.Text = dt.Rows[0]["PartyName"].ToString();
                pnldd.Visible = true;
                Panel3.Visible = false;
                FillGrid();
                FillRuleGrid("", "");
                FillGenerel();
                if (Convert.ToString(dt.Rows[0]["DocumentId"]) != "")
                {
                    LoadPdf(Convert.ToInt32(dt.Rows[0]["DocumentId"]));
                }
            }
            else
            {
                pnldd.Visible = true;
                Panel3.Visible = false;
                lblmsg.Text = "No Record Found.";
            }

            if (GridView2.Rows.Count > 0 || grd_general.Rows.Count > 0 || GridView1.Rows.Count > 0)
            {
                Panel5.Visible = false;
                Panel4.Visible = true;
            }
            else
            {
                Panel5.Visible = true;
                Panel4.Visible = false;
            }

        }
        else if (RadioButton4.Checked == true)
        {
            // Response.Write("RadioButton444");
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDocumentMasterByDocumentName(txtDocTitle.Text, ddlbusiness.SelectedValue);
            if (dt.Rows.Count > 0)
            {

                pnldd.Visible = false;
                Panel3.Visible = true;
                GridView3.DataSource = dt;
                DataView myDataView = new DataView();
                myDataView = dt.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }

                GridView3.DataBind();

            }
            else
            {
                pnldd.Visible = false;
                Panel3.Visible = true;
                GridView3.DataSource = null;

                GridView3.DataBind();
                lblmsg.Text = "No Record Found.";

                if (GridView2.Rows.Count > 0 || grd_general.Rows.Count > 0 || GridView1.Rows.Count > 0)
                {
                    Panel5.Visible = false;
                    Panel4.Visible = true;
                }
                else
                {
                    Panel5.Visible = true;
                    Panel4.Visible = false;
                }
            }

        }
    }
    protected void DRuleDetail_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item)

        //{

        Label lblempname = (Label)e.Item.FindControl("lblRuleDetail1");
        lblempname.Text = "<b>" + (DataBinder.Eval(e.Item.DataItem, "EmployeeName").ToString()) + "</b>";
        Label lblstep = (Label)e.Item.FindControl("lblStep");
        lblstep.Text = (DataBinder.Eval(e.Item.DataItem, "StepNo").ToString()) + "    <b>" + (DataBinder.Eval(e.Item.DataItem, "AnyAll").ToString()) + "</b>";
        Label lblReqDate = (Label)e.Item.FindControl("lblReqDate");
        lblReqDate.Text = (DataBinder.Eval(e.Item.DataItem, "ApprovalReqDate").ToString());
        Label lblApprovedDate = (Label)e.Item.FindControl("lblApprovedDate");
        lblApprovedDate.Text = (DataBinder.Eval(e.Item.DataItem, "ApprovedDate").ToString());
        Label lblStatus = (Label)e.Item.FindControl("lblStatus");
        lblStatus.Text = (DataBinder.Eval(e.Item.DataItem, "ApprovalStatus").ToString());
        Label lblConType = (Label)e.Item.FindControl("lblApprovalType");
        lblConType.Text = (DataBinder.Eval(e.Item.DataItem, "ApprovalType").ToString());
        Panel pnlshow = (Panel)e.Item.FindControl("pnlDetail"); //pnlhd");        
        //if (DateTime.Now > Convert.ToDateTime(lblReqDate.Text)) // time over
        //{
        //    pnlshow.BackColor = System.Drawing.Color.Red;
        //}
        Panel pnlHead = (Panel)e.Item.FindControl("pnlhd"); //pnlhd");
        int stepno = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "StepNo").ToString());
        if (stepno % 2 == 0)
        {
            // It's even
            pnlHead.BackColor = System.Drawing.Color.DarkKhaki;
        }
        else
        {
            // It's odd
            pnlHead.BackColor = System.Drawing.Color.PaleGoldenrod;
        }
        if (DateTime.Now > Convert.ToDateTime(lblReqDate.Text)) // time over
        {
            pnlHead.BackColor = System.Drawing.Color.Red;
            lblstep.ForeColor = System.Drawing.Color.White;
        }



    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ViewState["FinalStatus"] = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataTable DtDetail = new DataTable();
            DataRow dr = DtDetail.NewRow();
            DataRow DtMainRow1 = DtDetail.NewRow();

            DateTime ProcessDt;
            double Days = 0;
            DateTime RecentTime;
            RecentTime = DateTime.Now;
            //
            DataColumn dtcom1 = new DataColumn();
            dtcom1.DataType = System.Type.GetType("System.String");
            dtcom1.ColumnName = "StepNo";
            dtcom1.ReadOnly = false;
            dtcom1.Unique = false;
            dtcom1.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom1);
            DataColumn dtcom2 = new DataColumn();
            dtcom2.DataType = System.Type.GetType("System.String");
            dtcom2.ColumnName = "ApprovalReqDate";
            dtcom2.ReadOnly = false;
            dtcom2.Unique = false;
            dtcom2.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom2);
            DataColumn dtcom3 = new DataColumn();
            dtcom3.DataType = System.Type.GetType("System.String");
            dtcom3.ColumnName = "ApprovedDate";
            dtcom3.ReadOnly = false;
            dtcom3.Unique = false;
            dtcom3.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom3);
            DataColumn dtcom4 = new DataColumn();
            dtcom4.DataType = System.Type.GetType("System.String");
            dtcom4.ColumnName = "ApprovalStatus";
            dtcom4.ReadOnly = false;
            dtcom4.Unique = false;
            dtcom4.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom4);
            DataColumn dtcom5 = new DataColumn();
            dtcom5.DataType = System.Type.GetType("System.String");
            dtcom5.ColumnName = "EmployeeName";
            dtcom5.ReadOnly = false;
            dtcom5.Unique = false;
            dtcom5.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom5);
            DataColumn dtcom6 = new DataColumn();
            dtcom6.DataType = System.Type.GetType("System.String");
            dtcom6.ColumnName = "ApprovalType";
            dtcom6.ReadOnly = false;
            dtcom6.Unique = false;
            dtcom6.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom6);

            DataColumn dtcom7 = new DataColumn();
            dtcom7.DataType = System.Type.GetType("System.String");
            dtcom7.ColumnName = "AnyAll";
            dtcom7.ReadOnly = false;
            dtcom7.Unique = false;
            dtcom7.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom7);

            int RuleId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RuleId").ToString());
            DataTable dtr = new DataTable();
            DataTable dtDocProcess = new DataTable();
            DataTable dtstep = new DataTable();
            dtstep = clsInstruction.SelectRuleDetailStep(Convert.ToInt32(RuleId));
            if (dtstep.Rows.Count > 0)
            {
                foreach (DataRow Drdtstep in dtstep.Rows) // Get Steps in RUle
                {
                    Int32 StepinProcess = Convert.ToInt32(Drdtstep["StepId"].ToString());

                    DataTable DtEmpSelforLess = new DataTable();
                    DtEmpSelforLess = clsInstruction.SelectRuleEmpSelectionMaster(Convert.ToInt32(RuleId), StepinProcess);
                    if (DtEmpSelforLess.Rows.Count > 0)
                    {

                        if (DtEmpSelforLess.Rows[0]["EmpSelectionId"].ToString() == "1") // Any
                        {
                            dtDocProcess = clsInstruction.SelectRuleDetailRuleIdwiseStepWise(Convert.ToInt32(RuleId), Convert.ToInt32(Drdtstep["StepId"]), ddlbusiness.SelectedValue);
                            if (dtDocProcess.Rows.Count > 0)
                            {
                                //
                                DataTable dtDocProcess1 = new DataTable();
                                dtDocProcess1 = clsDocument.SelectProcessingDocumentbyDocIdwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId").ToString()));


                                if (dtDocProcess1.Rows.Count > 0)
                                {
                                    ProcessDt = Convert.ToDateTime(dtDocProcess1.Rows[0]["ApproveDate"].ToString());
                                    Days = Convert.ToDouble(dtDocProcess.Rows[0]["Days"].ToString());
                                    RecentTime = ProcessDt.AddDays(Days);
                                }

                                bool donebyany1 = false;

                                foreach (DataRow DrDocProcess in dtDocProcess.Rows)
                                {
                                    DataTable LastEmpProcess = new DataTable();
                                    LastEmpProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWiseRuleDetailIdwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId").ToString()), Convert.ToInt32(DrDocProcess["RuleDetailId"]));
                                    if (LastEmpProcess.Rows.Count > 0)
                                    {
                                        DtMainRow1 = DtDetail.NewRow();
                                        DtMainRow1["EmployeeName"] = DrDocProcess["EmployeeName"].ToString();
                                        DtMainRow1["AnyAll"] = "Any"; // dtDocProcess.Rows[0]["EmployeeName"].ToString();

                                        DtMainRow1["StepNo"] = DrDocProcess["StepId"].ToString();
                                        DtMainRow1["ApprovalType"] = DrDocProcess["RuleApproveTypeName"].ToString();
                                        DtMainRow1["ApprovalReqDate"] = RecentTime.ToShortDateString(); // RecentTime.ToShortDateString();
                                        //  DtMainRow1["ApprovalReqDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovedDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        //DtMainRow1["ApprovalStatus"] = Convert.ToString("Approved");
                                        if (Convert.ToBoolean(LastEmpProcess.Rows[0]["Approve"]) == true)
                                        {
                                            DtMainRow1["ApprovalStatus"] = Convert.ToString("Approved");
                                            ViewState["FinalStatus"] = "Approved";
                                        }
                                        else if (Convert.ToBoolean(LastEmpProcess.Rows[0]["Approve"]) == false)
                                        {
                                            DtMainRow1["ApprovalStatus"] = Convert.ToString("Rejected");
                                            ViewState["FinalStatus"] = "Rejected";
                                        }
                                        else
                                        {
                                            DtMainRow1["ApprovalStatus"] = Convert.ToString("Pending");
                                            ViewState["FinalStatus"] = "Pending";
                                        }
                                        DtDetail.Rows.Add(DtMainRow1);
                                        donebyany1 = true;
                                        // if there then green 
                                        // Add data with requied date approve date etc  color info etc
                                    }
                                    else
                                    {
                                    }
                                }
                                if (donebyany1 != true)
                                {
                                    foreach (DataRow DrDocProcess in dtDocProcess.Rows)
                                    {
                                        DtMainRow1 = DtDetail.NewRow();
                                        DtMainRow1["EmployeeName"] = DrDocProcess["EmployeeName"].ToString();
                                        DtMainRow1["StepNo"] = DrDocProcess["StepId"].ToString();
                                        DtMainRow1["ApprovalType"] = DrDocProcess["RuleApproveTypeName"].ToString();
                                        DtMainRow1["AnyAll"] = "Any";
                                        //   DtMainRow1["ApprovalReqDate"] = RecentTime.ToShortDateString();
                                        DtMainRow1["ApprovalReqDate"] = RecentTime.ToShortDateString(); // date LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovedDate"] = "";// LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovalStatus"] = "Pending"; // Convert.ToString("Approved");
                                        ViewState["FinalStatus"] = "Pending";
                                        DtDetail.Rows.Add(DtMainRow1);
                                    }
                                }

                            }

                        }
                        else // All Emp
                        {
                            dtDocProcess = clsInstruction.SelectRuleDetailRuleIdwiseStepWise(Convert.ToInt32(RuleId), Convert.ToInt32(Drdtstep["StepId"]), ddlbusiness.SelectedValue);
                            if (dtDocProcess.Rows.Count > 0)
                            {
                                //
                                DataTable dtDocProcess1 = new DataTable();
                                dtDocProcess1 = clsDocument.SelectProcessingDocumentbyDocIdwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId").ToString()));


                                if (dtDocProcess1.Rows.Count > 0)
                                {
                                    ProcessDt = Convert.ToDateTime(dtDocProcess1.Rows[0]["ApproveDate"].ToString());
                                    Days = Convert.ToDouble(dtDocProcess.Rows[0]["Days"].ToString());
                                    RecentTime = ProcessDt.AddDays(Days);
                                }


                                bool donebyany1 = false;

                                foreach (DataRow DrDocProcess in dtDocProcess.Rows)
                                {
                                    DataTable LastEmpProcess = new DataTable();
                                    LastEmpProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWiseRuleDetailIdwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId").ToString()), Convert.ToInt32(DrDocProcess["RuleDetailId"]));
                                    if (LastEmpProcess.Rows.Count > 0)
                                    {
                                        DtMainRow1 = DtDetail.NewRow();
                                        DtMainRow1["EmployeeName"] = DrDocProcess["EmployeeName"].ToString();
                                        DtMainRow1["StepNo"] = DrDocProcess["StepId"].ToString();
                                        DtMainRow1["ApprovalReqDate"] = RecentTime.ToShortDateString();
                                        DtMainRow1["ApprovalType"] = DrDocProcess["RuleApproveTypeName"].ToString();
                                        DtMainRow1["AnyAll"] = "All";
                                        // DtMainRow1["ApprovalReqDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovedDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        //DtMainRow1["ApprovalStatus"] = Convert.ToString("Approved");
                                        if (Convert.ToBoolean(LastEmpProcess.Rows[0]["Approve"]) == true)
                                        {
                                            DtMainRow1["ApprovalStatus"] = Convert.ToString("Approved");
                                            ViewState["FinalStatus"] = "Approved";
                                        }
                                        else if (Convert.ToBoolean(LastEmpProcess.Rows[0]["Approve"]) == false)
                                        {
                                            DtMainRow1["ApprovalStatus"] = Convert.ToString("Rejected");
                                            ViewState["FinalStatus"] = "Rejected";
                                        }
                                        else
                                        {
                                            DtMainRow1["ApprovalStatus"] = Convert.ToString("Pending");
                                            ViewState["FinalStatus"] = "Pending";
                                        }
                                        DtDetail.Rows.Add(DtMainRow1);
                                        donebyany1 = true;
                                        // if there then green 
                                        // Add data with requied date approve date etc  color info etc
                                    }
                                    else
                                    {
                                        DtMainRow1 = DtDetail.NewRow();
                                        DtMainRow1["EmployeeName"] = DrDocProcess["EmployeeName"].ToString();
                                        DtMainRow1["StepNo"] = DrDocProcess["StepId"].ToString();
                                        DtMainRow1["ApprovalReqDate"] = RecentTime.ToShortDateString();
                                        DtMainRow1["ApprovalType"] = DrDocProcess["RuleApproveTypeName"].ToString();
                                        DtMainRow1["AnyAll"] = "All";
                                        //   DtMainRow1["ApprovalReqDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovedDate"] = "";// LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovalStatus"] = Convert.ToString("Pending");
                                        ViewState["FinalStatus"] = "Pending";
                                        DtDetail.Rows.Add(DtMainRow1);
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        dtDocProcess = clsInstruction.SelectRuleDetailRuleIdwiseStepWise(Convert.ToInt32(RuleId), Convert.ToInt32(Drdtstep["StepId"]), ddlbusiness.SelectedValue);
                        if (dtDocProcess.Rows.Count > 0)
                        {
                            //
                            DataTable dtDocProcess1 = new DataTable();
                            dtDocProcess1 = clsDocument.SelectProcessingDocumentbyDocIdwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId").ToString()));


                            if (dtDocProcess1.Rows.Count > 0)
                            {
                                try
                                {
                                    ProcessDt = Convert.ToDateTime(dtDocProcess1.Rows[0]["ApproveDate"].ToString());
                                    Days = Convert.ToDouble(dtDocProcess.Rows[0]["Days"].ToString());
                                    RecentTime = ProcessDt.AddDays(Days);
                                }
                                catch (Exception ex)
                                {
                                }    
                            }

                            foreach (DataRow DrDocProcess in dtDocProcess.Rows)
                            {
                                // DataRow dr = new DataRow(); // DtDetail.NewRow();
                                dr = DtDetail.NewRow();
                                //  DataRow DtMainRow1 = DtDetail.NewRow();
                                dr["EmployeeName"] = DrDocProcess["EmployeeName"].ToString();
                                dr["StepNo"] = DrDocProcess["StepId"].ToString();
                                dr["ApprovalType"] = DrDocProcess["RuleApproveTypeName"].ToString();
                                dr["AnyAll"] = "";
                                dr["ApprovalReqDate"] = RecentTime.ToShortDateString();
                                DataTable LastEmpProcess = new DataTable();
                                LastEmpProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWiseRuleDetailIdwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId")), Convert.ToInt32(DrDocProcess["RuleDetailId"]));
                                if (LastEmpProcess.Rows.Count > 0)
                                {
                                    // dr["ApprovalReqDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                    dr["ApprovedDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                    if (Convert.ToBoolean(LastEmpProcess.Rows[0]["Approve"]) == true)
                                    {
                                        dr["ApprovalStatus"] = Convert.ToString("Approved");
                                        ViewState["FinalStatus"] = "Approved";
                                    }
                                    else
                                    {
                                        dr["ApprovalStatus"] = Convert.ToString("Rejected");
                                        ViewState["FinalStatus"] = "Rejected";
                                    }

                                    // if there then green 
                                    // Add data with requied date approve date etc  color info etc
                                }
                                else
                                {
                                    // other wise Red 
                                    //  DtMainRow1["ApprovalReqDate"] = ""; // LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                    dr["ApprovedDate"] = ""; // LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                    dr["ApprovalStatus"] = Convert.ToString("Pending");

                                    ViewState["FinalStatus"] = "Pending";
                                    //  DtDetail.Rows.Add(DtMainRow1);
                                }
                                DtDetail.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            DataList gridDetail1 = (DataList)e.Row.FindControl("DRuleDetail");
            ViewState["StepNo"] = "";
            gridDetail1.DataSource = DtDetail;
            gridDetail1.DataBind();
            Image imgfinalstatus = (Image)e.Row.FindControl("imgfinalstatus");

            Label lblFinalStatus = (Label)e.Row.FindControl("lblFinalStatus");

            Label lblfinalstatusword = (Label)e.Row.FindControl("lblfinalstatusword");
            
            if (ViewState["FinalStatus"].ToString().Length > 0)
            {
                if (Convert.ToString(ViewState["FinalStatus"]) == "Rejected")
                {
                    imgfinalstatus.ImageUrl = "~/Account/images/closeicon.png";
                    lblfinalstatusword.Text = "Rejected";

                }
                else if (Convert.ToString(ViewState["FinalStatus"]) == "Approved")
                {
                    imgfinalstatus.ImageUrl = "~/Account/images/Right.jpg";
                    lblfinalstatusword.Text = "Approved";
                }
                else
                {
                    imgfinalstatus.ImageUrl = "~/Account/images/Datapending.jpg";
                    //  lblFinalStatus.Text = "Approved";
                    lblfinalstatusword.Text = "Pending";
                }
            }
            //  lblempname.Text ="<b>" + (DataBinder.Eval(e.Item.DataItem, "EmployeeName").ToString()) + "</b>";

        }
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView2.DataSource = null;
        GridView2.DataBind();

        grd_general.DataSource = null;
        grd_general.DataBind();
        lblDocDate.Text = "";
        lblDocID.Text = "";
        lblDocName.Text = "";
        lblPartyName.Text = "";
    }

    protected void GridView3_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        hdnsortExp.Value = null;
        hdnsortDir.Value = null;
        GridView3.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        ViewState["DocId"] = GridView3.DataKeys[GridView3.SelectedIndex].Value.ToString();

        dt = clsDocument.SelectDocumentMasterByDocId(Convert.ToInt32(ViewState["DocId"]), ddlbusiness.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            lblcomname.Text = Session["Cname"] + " - " + ddlbusiness.SelectedItem.Text;
            lblDocID.Text = dt.Rows[0]["DocumentId"].ToString();  // txtDocTitle.Text.ToString();
            ViewState["DocId"] = lblDocID.Text;
            lblDocName.Text = dt.Rows[0]["DocumentTitle"].ToString();
            lblDocDate.Text = dt.Rows[0]["DocumentUploadDate"].ToString();
            lblPartyName.Text = dt.Rows[0]["PartyName"].ToString();
            pnldd.Visible = true;
        }
        FillGrid();
        FillRuleGrid("", "");
        FillGenerel();

        if (GridView2.Rows.Count > 0 || grd_general.Rows.Count > 0 || GridView1.Rows.Count > 0)
        {
            Panel5.Visible = false;
            Panel4.Visible = true;
        }
        else
        {
            Panel5.Visible = true;
            Panel4.Visible = false;
        }

    }

    protected void FillGenerel()
    {
        DataTable dts = new DataTable();
        string str = "Select Distinct case when(RuleApproveTypeMaster.RuleApproveTypeName IS NULL)then 'None' else RuleApproveTypeMaster.RuleApproveTypeName End as RuleApproveTypeName , DesignationMaster.DesignationName,DepartmentmasterMNC.Departmentname,EmployeeMaster.EmployeeName, DocumentEmpApproveLog.Note,DocumentEmpApproveLog.ApproveDate,case when(DocumentEmpApproveLog.Approve=1) then 'Accept' else 'Reject' End as Approve from DocumentEmpApproveLog inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=DocumentEmpApproveLog.EmployeeID inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId inner join DepartmentmasterMNC on DepartmentmasterMNC.id= DesignationMaster.DeptID Left join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=DocumentEmpApproveLog.DocumentApproveTypeId where  DocumentEmpApproveLog.DocumentId='" + ViewState["DocId"] + "'";
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        adp.Fill(dts);

        grd_general.DataSource = dts;

        DataView myDataView = new DataView();
        myDataView = dts.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        grd_general.DataBind();

    }
    protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        ibtnSearchShow_Click(sender, e);
    }
    protected void grd_general_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGenerel();
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
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        ibtnSearchShow_Click(sender, e);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            //pnlgr.ScrollBars = ScrollBars.None;
            //pnlgr.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            Button3.Visible = true;
            //if (griddocapprovaltype.Columns[3].Visible == true)
            //{
            //    ViewState["editHide"] = "tt";
            //    griddocapprovaltype.Columns[3].Visible = false;
            //}
            //if (griddocapprovaltype.Columns[4].Visible == true)
            //{
            //    ViewState["delHide"] = "tt";
            //    griddocapprovaltype.Columns[4].Visible = false;
            //}
        }
        else
        {

            //pnlgr.ScrollBars = ScrollBars.Vertical;
            //pnlgr.Height = new Unit(250);

            Button2.Text = "Printable Version";
            Button3.Visible = false;
            //if (ViewState["editHide"] != null)
            //{
            //    griddocapprovaltype.Columns[3].Visible = true;
            //}
            //if (ViewState["delHide"] != null)
            //{
            //    griddocapprovaltype.Columns[4].Visible = true;
            //}
        }
    }
    protected void chkapp_CheckedChanged(object sender, EventArgs e)
    {
        if (chkapp.Checked == true)
        {
            GridView2.Columns[9].Visible = true;
            grd_general.Columns[6].Visible = true;
        }
        else
        {
            GridView2.Columns[9].Visible = false;
            grd_general.Columns[6].Visible = false;
        }
    }
}

