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
using System.IO.Compression;

public partial class Account_DocumentFastUpload : System.Web.UI.Page
{
    SqlConnection con;
    //[DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    //public static extern int apOpen(string lpFileName, string lpOwnerPw, string lpUserPw);

    //[DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    //public static extern int apClose(int nHandle);

    //[DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    //public static extern int apConvert(int nHandle);

    //[DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    //public static extern int apConvertPage(int nHandle, int nPageNo);

    //[DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    //public static extern int apConvertPageToStream(int nHandle, int nPageNo, ref int pMemStream, ref int nSize);

    //[DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    //public static extern int apSetProperty(int nHandle,  //Conversion object's handle.
    //                                        int nIndex,   //Property tag,refer to the following property definitions.
    //                                        string lpValue,  //A String value of the property,(or)
    //                                        int nValue,   //An integer value of the property.
    //                                        int nOther);  //Only valid for progress call-back.

    //[DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    //public static extern int apGetProperty(int nHandle, //Conversion object's handle.
    //                                        int nIndex,  //Property tag,refer to the following property definitions.
    //                                        ref int pValue,     //A String value of the property,(or)
    //                                        ref int nValue);    //An integer value of the property.
    ////==============================================================================================================

    ////1.2 Definitions of converter's properties 
    ////==============================================================================================================
    ////read/write properties
    //const int AP_PROP_OUTDIR = 1;  //Destination directory.
    //const int AP_PROP_PREFIX = 2;  //The prefix of the name of result image file.
    //const int AP_PROP_PAGEZOOM = 3;  //Zoom scale of the source pdf page.
    //const int AP_PROP_BGCOLOR = 4;  //Image background color.
    //const int AP_PROP_IMAGETYPE = 5;  //Result image format.
    //const int AP_PROP_BITCOUNT = 6;  //Color bits per pixel.
    //const int AP_PROP_XDPI = 7;  //Horizontal resolution.
    //const int AP_PROP_YDPI = 8;  //Vertical resolution.
    //const int AP_PROP_QUALITY = 9;  //JPEG compression quality.
    //const int AP_PROP_COMPRESSION = 10; //TIFF compression mode.
    //const int AP_PROP_MULTIPAGES = 11; //Multipages TIFF file.
    //const int AP_PROP_GRAYSCALE = 12; //Grayscale image.
    //const int AP_PROP_XDIMENSIONS = 13; //The width of the result image in pixel
    //const int AP_PROP_YDIMENSIONS = 14; //The height of the result image in pixel

    ////read only properties
    //const int AP_PROP_PAGECOUNT = 20; //The total pages of source pdf file.
    //const int AP_PROP_PAGEHEIGHT = 21; //Height of the current page of source pdf file.
    //const int AP_PROP_PAGEWIDTH = 22; //Width of the current page of source pdf file.
    ////==============================================================================================================

    ////1.3 TIFF tag's definitions.
    ////==============================================================================================================
    //const int AP_TIFF_COMPRESSION_NONE = 0;  //No compression.
    //const int AP_TIFF_COMPRESSION_LZW = 1;  //1,4,8,24bits(default 4,8,24bits)
    //const int AP_TIFF_COMPRESSION_JPEG = 2;  //Grayscale 8bits,24bits
    //const int AP_TIFF_COMPRESSION_PACKBITS = 3;  //4,8,24bits
    //const int AP_TIFF_COMPRESSION_CCITTG4 = 4;  //1bit(default 1bit)
    //const int AP_TIFF_COMPRESSION_CCITTG3 = 5;  //1bit
    //const int AP_TIFF_COMPRESSION_RLE = 6;  //1bit
    ////==============================================================================================================

    ////1.4 Image type's definitions
    ////==============================================================================================================
    //const int AP_IMAGE_BMP = 1;   //BMP
    //const int AP_IMAGE_EMF = 2;   //EMF
    //const int AP_IMAGE_WMF = 3;   //WMF
    //const int AP_IMAGE_JPG = 4;   //JPG
    //const int AP_IMAGE_PNG = 5;   //PNG
    //const int AP_IMAGE_GIF = 6;   //GIF
    //const int AP_IMAGE_TIF = 7;   //TIF
    //const int AP_IMAGE_PCX = 8;   //PCX

    //const int AP_IMAGE_JPEG = 4;   //JPEG
    //const int AP_IMAGE_TIFF = 7;   //TIFF
    ////==============================================================================================================

    ////1.5 Return code's definitions.
    ////==============================================================================================================
    //const int RTN_OK = 1;           //Successful operation.
    //const int ERR_UNKNOWN = -99;    //Unknown system error.

    //const int ERR_OVER_MAXTHREADS = -1;  //Over the limit amount of threads.
    //const int ERR_FILE_UNEXIST = -2;  //Source PDF file unexist.
    //const int ERR_FILE_DAMAGED = -3;  //Source PDF file is damaged.
    //const int ERR_FILE_RESTRICTED = -4;  //Source PDF file is restricted.
    ////==============================================================================================================

    ////1.6 Windows API definitions.
    ////==============================================================================================================
    //[DllImport("kernel32.dll")]
    //public static extern int GetTickCount();
    //[DllImport("kernel32.dll")]
    //public static extern void CopyMemory(Byte[] dest, int Source, Int32 length);

    //const int TRUE = 1;
    //const int FALSE = 0;
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

    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    Companycls ClsCompany = new Companycls();
    protected void Page_Load(object sender, EventArgs e)
    {

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i12 = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i12 - 1].ToString();

        Page.Title = pg.getPageTitle(page);


        if (Session["CompanyName"] != null)
        {
            this.Title = Session["CompanyName"] + " IFileCabinet.com - Multiple Upload";
        }

        Session["PageName"] = "DocumentFastUpload.aspx";


        if (!IsPostBack)
        {
            ViewState["data"] = null;

            if (Directory.Exists(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc")))
            {

                FileInfo[] file = null;
                int i = 0;

                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc"));
                file = dir.GetFiles();
                if (file.Length > 0)
                {
                    for (i = 0; i <= file.Length - 1; i++)
                    {
                        file[i].Delete();
                        //File.Delete(Server.MapPath("..\\Account\\TempDoc\\") + file[i].);
                    }
                }


                // Directory.Delete(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc"));
            }
            if (Request.QueryString["Tid"] != null)
            {
                pnlentry.Visible = true;
                Button1.Visible = true;
                ViewState["tid"] = Request.QueryString["Tid"];
                string strt = "select Entry_Type_Name,EntryNumber,Entry_Type_Id FROM EntryTypeMaster INNER JOIN TranctionMaster ON dbo.EntryTypeMaster.Entry_Type_Id = dbo.TranctionMaster.EntryTypeId WHERE  TranctionMaster.Tranction_Master_Id='" + Request.QueryString["Tid"] + "'";
                SqlCommand cmd1t = new SqlCommand(strt, con);
                cmd1t.CommandType = CommandType.Text;
                SqlDataAdapter dat = new SqlDataAdapter(cmd1t);
                DataTable dtt = new DataTable();
                dat.Fill(dtt);
                if (dtt.Rows.Count > 0)
                {
                    lbletype.Text = dtt.Rows[0]["Entry_Type_Name"].ToString();
                    lbleno.Text = dtt.Rows[0]["EntryNumber"].ToString();
                    lbltid.Text = Request.QueryString["Tid"].ToString();
                }
            }
            else
            {
                Button1.Visible = false;
                pnlentry.Visible = false;
            }
            flaganddoc();
            fillstore();
            ddlbusiness_SelectedIndexChanged(sender, e);
            ddldt_SelectedIndexChanged(sender, e);
            // TxtDocDate.Text = DateTime.Now.ToShortDateString();
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
    protected void fillstore()
    {
        ddlbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
            ddlbusiness.DataSource = ds;
            ddlbusiness.DataTextField = "Name";
            ddlbusiness.DataValueField = "WareHouseId";
            ddlbusiness.DataBind();


            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
        }

    }
    protected void imgbtnAdd_Click(object sender, EventArgs e)
    {
        // lblmsg.Text = "";

        //  FillGrid();
        //  imgbtnUpload.Visible = true;
        //   txtdoctitle.Text="";
        //   txtdocrefnmbr.Text = "";
        //   txtnetamount.Text = "";
        //   TxtDocDate.Text = "";
        // FillGrid();
        int flagpd = 0;
        lblmsg.Text = "";
        //if (ddldt.SelectedItem.Text == "Credit Invoice" || ddldt.SelectedItem.Text == "Cash Invoice" || ddldt.SelectedItem.Text == "Cash Voucher" || ddldt.SelectedItem.Text == "Credit Voucher")
        //{
        if (txtpartdocrefno.Text.Length > 0)
        {
            DataTable dtsc = select("select PartyDocrefno from DocumentMaster where   CID='" + Session["Comid"] + "' and PartyDocrefno='" + txtpartdocrefno.Text + "'");
            if (dtsc.Rows.Count == 0)
            {
                if (Convert.ToString(ViewState["data"]) != "")
                {
                    DataTable dtc = (DataTable)ViewState["data"];
                    foreach (DataRow item in dtc.Rows)
                    {
                        if (Convert.ToString(item["PRN"]) == Convert.ToString(txtpartdocrefno.Text))
                        {
                            flagpd = 1;
                            lblmsg.Text = "Please enter unique party document reference number.";
                            break;
                        }
                        else
                        {

                        }
                    }
                }
            }
            else
            {
                flagpd = 1;
                lblmsg.Text = "Please enter unique party document reference number.";
            }
        }

        if (flagpd == 0)
        {
            //if (FileUpload1.HasFile == true)
            //{

            //    Directory.CreateDirectory(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc"));
            //    FileUpload1.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\") + FileUpload1.FileName);
            //    string filename = FileUpload1.FileName;
            //    FillGridwithstring(filename);

            //}
            if (FileUpload1.HasFiles == true)
            {
                foreach (HttpPostedFile file in FileUpload1.PostedFiles)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc"));

                    string filename = System.IO.Path.GetFileName(file.FileName.ToString());

                    file.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\") + filename);
                    
                    FillGridwithstring(filename);
                }
            }


            lblmsg.Text = "";
            imgbtnUpload.Visible = true;
            txtdoctitle.Text = "";
            txtdocrefnmbr.Text = "";
            txtnetamount.Text = "";
            TxtDocDate.Text = "";

        }

    }

    protected void imgbtnUpload_Click(object sender, EventArgs e)
    {
        fillgddefalt();
        UploadDocumets();
        ViewState["data"] = null;
        Gridreqinfo.DataSource = null;
        Gridreqinfo.DataBind();
        imgbtnUpload.Visible = false;


    }


    protected void Gridreqinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            Gridreqinfo.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DeleteFromGrid(Convert.ToInt32(Gridreqinfo.SelectedIndex.ToString()));

        }
    }
    //======================= Functions===================================
    //=====================================================================



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
        Dcom4.ColumnName = "Businessname";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;


        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "PartyId";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;


        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "DocType";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "Whid";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "docdate";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;


        DataColumn Dcom9 = new DataColumn();
        Dcom9.DataType = System.Type.GetType("System.String");
        Dcom9.ColumnName = "docrefno";
        Dcom9.AllowDBNull = true;
        Dcom9.Unique = false;
        Dcom9.ReadOnly = false;

        DataColumn Dcom10 = new DataColumn();
        Dcom10.DataType = System.Type.GetType("System.String");
        Dcom10.ColumnName = "docamt";
        Dcom10.AllowDBNull = true;
        Dcom10.Unique = false;
        Dcom10.ReadOnly = false;
        DataColumn Dcom4a = new DataColumn();
        Dcom4a.DataType = System.Type.GetType("System.String");
        Dcom4a.ColumnName = "Docty";
        Dcom4a.AllowDBNull = true;
        Dcom4a.Unique = false;
        Dcom4a.ReadOnly = false;

        DataColumn Dcom5a = new DataColumn();
        Dcom5a.DataType = System.Type.GetType("System.String");
        Dcom5a.ColumnName = "DoctyId";
        Dcom5a.AllowDBNull = true;
        Dcom5a.Unique = false;
        Dcom5a.ReadOnly = false;
        DataColumn Dcom6a = new DataColumn();
        Dcom6a.DataType = System.Type.GetType("System.String");
        Dcom6a.ColumnName = "PRN";
        Dcom6a.AllowDBNull = true;
        Dcom6a.Unique = false;
        Dcom6a.ReadOnly = false;


        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);
        dt.Columns.Add(Dcom8);
        dt.Columns.Add(Dcom9);
        dt.Columns.Add(Dcom10);
        dt.Columns.Add(Dcom4a);
        dt.Columns.Add(Dcom5a);
        dt.Columns.Add(Dcom6a);
        return dt;
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;

        //DropDownList ddlclassgroup = (DropDownList)grdaddacc.Rows[rinrow].FindControl("ddlclassgroup");
        //DropDownList ddlwareacc = (DropDownList)grdaddacc.Rows[rinrow].FindControl("ddlwareacc");

        //DataTable dt = new DataTable();

        //ddlclassgroup.Items.Clear();

        //string str = "SELECT Left(ClassCompanyMaster.displayname,20)+':'+(GroupCompanyMaster.groupdisplayname) as Classgroup,GroupCompanyMaster.GroupId  FROM ClassCompanyMaster  inner join GroupCompanyMaster on GroupCompanyMaster.classcompanymasterid=ClassCompanyMaster.Id  where ClassCompanyMaster.Whid='" + ddlwareacc.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlwareacc.SelectedValue + "' order by Classgroup";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //da.Fill(dt);
        //ddlclassgroup.DataSource = dt;
        //ddlclassgroup.DataTextField = "Classgroup";
        //ddlclassgroup.DataValueField = "GroupId";
        //ddlclassgroup.DataBind();

    }
    protected void ddluser_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;

        
    }
    protected void ddldocty_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;

        DropDownList ddldocty = (DropDownList)Gridreqinfo.Rows[rinrow].FindControl("ddldocty");
        RequiredFieldValidator ReqValPArtyDocRefno = (RequiredFieldValidator)Gridreqinfo.Rows[rinrow].FindControl("ReqValPArtyDocRefno");
        if (ddldocty.SelectedItem.Text == "Credit Invoice" || ddldocty.SelectedItem.Text == "Cash Invoice" || ddldocty.SelectedItem.Text == "Cash Voucher" || ddldocty.SelectedItem.Text == "Credit Voucher")
        {
            ReqValPArtyDocRefno.Visible = true;
        }
        else
        {
            ReqValPArtyDocRefno.Visible = false;
        }
    }
    protected void FillGrid()
    {
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["data"]) == "")
        {
            dt = CreateDatatable();

            if (FileUpload1.HasFile == true)
            {

                DataRow Drow = dt.NewRow();
                Drow["documentname"] = FileUpload1.FileName;
                Drow["documenttype"] = ddlDocType.SelectedValue;
                Drow["status"] = "Not Uploaded";

                Drow["Businessname"] = ddlbusiness.SelectedItem.Text;
                Drow["DocType"] = ddlDocType.SelectedItem.Text;
                Drow["DocumentTitle"] = txtdoctitle.Text;

                Drow["PartyId"] = ddlpartyname.SelectedValue;
                Drow["Whid"] = ddlbusiness.SelectedValue;
                if (TxtDocDate.Text == "")
                {
                    Drow["docdate"] = DateTime.Now.ToShortDateString();
                }
                else
                {
                    Drow["docdate"] = Convert.ToDateTime(TxtDocDate.Text).ToShortDateString();
                }
                Drow["docrefno"] = txtdocrefnmbr.Text;
                Drow["Docty"] = ddldt.SelectedItem.Text;
                Drow["DoctyId"] = ddldt.SelectedValue;
                Drow["PRN"] = txtpartdocrefno.Text;
                dt.Rows.Add(Drow);
            }
            ViewState["data"] = dt;
            Gridreqinfo.DataSource = dt;
            Gridreqinfo.DataBind();
            foreach (GridViewRow gdr in Gridreqinfo.Rows)
            {
                if (gdr.Cells[7].Text == "Uploaded")
                {
                    gdr.Cells[7].ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    gdr.Cells[7].ForeColor = System.Drawing.Color.Red;
                }
            }
            if (FileUpload1.HasFile == true)
            {
                Directory.CreateDirectory(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc"));
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\") + FileUpload1.FileName);
            }

        }
        else
        {

            dt = (DataTable)ViewState["data"];


            int flag = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string doctypeid = dr["documenttype"].ToString();
                string docname = dr["DocumentTitle"].ToString();
                if (doctypeid == ddlDocType.SelectedValue && docname == txtdoctitle.Text)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record already exist";
                    flag = 1;
                    break;
                }
            }


            if (flag == 0)
            {




                if (FileUpload1.HasFile == true)
                {

                    DataRow Drow = dt.NewRow();
                    Drow["documentname"] = FileUpload1.FileName;
                    Drow["documenttype"] = ddlDocType.SelectedValue;
                    Drow["status"] = "Not Uploaded";

                    Drow["Businessname"] = ddlbusiness.SelectedItem.Text;
                    Drow["DocType"] = ddlDocType.SelectedItem.Text;
                    Drow["DocumentTitle"] = txtdoctitle.Text;

                    Drow["PartyId"] = ddlpartyname.SelectedValue;
                    Drow["Whid"] = ddlbusiness.SelectedValue;
                    if (TxtDocDate.Text == "")
                    {
                        Drow["docdate"] = DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        Drow["docdate"] = Convert.ToDateTime(TxtDocDate.Text).ToShortDateString();
                    }

                    Drow["docrefno"] = txtdocrefnmbr.Text;
                    Drow["docamt"] = txtnetamount.Text; ;

                    dt.Rows.Add(Drow);
                }
                ViewState["data"] = dt;
                Gridreqinfo.DataSource = dt;
                Gridreqinfo.DataBind();
                foreach (GridViewRow gdr in Gridreqinfo.Rows)
                {
                    if (gdr.Cells[7].Text == "Uploaded")
                    {
                        gdr.Cells[7].ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        gdr.Cells[7].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (FileUpload1.HasFile == true)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc"));
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\") + FileUpload1.FileName);
                }
            }
        }

    }
    protected void fillgridoc()
    {
        DataTable dtx = new DataTable();
        dtx = clsDocument.SelectDocTypeAll(ddlbusiness.SelectedValue);
        DataTable dt = new DataTable();
        dt = clsDocument.selectparty(ddlbusiness.SelectedValue);
        DataTable dts1 = select("select Id,name from DocumentTypenm where  active='1' Order by name");
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            DropDownList ddldoctypemas = (DropDownList)gdr.FindControl("ddldoctypemas");
            DropDownList ddluser = (DropDownList)gdr.FindControl("ddluser");
            DropDownList ddldocty = (DropDownList)gdr.FindControl("ddldocty");
            Label lbldocmasId = (Label)gdr.FindControl("lbldocmasId");
            Label lblpid = (Label)gdr.FindControl("lblpid");
            Label lbldoctid = (Label)gdr.FindControl("lbldoctid");


            if (gdr.Cells[7].Text == "Uploaded")
            {
                gdr.Cells[7].ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                gdr.Cells[7].ForeColor = System.Drawing.Color.Red;
            }

            ddldoctypemas.DataSource = dtx;
            ddldoctypemas.DataTextField = "doctype";
            ddldoctypemas.DataValueField = "DocumentTypeId";
            ddldoctypemas.DataBind();
            ddldoctypemas.SelectedIndex = ddldoctypemas.Items.IndexOf(ddldoctypemas.Items.FindByValue(lbldocmasId.Text));


            ddluser.DataSource = dt;
            ddluser.DataTextField = "PartyName";
            ddluser.DataValueField = "PartyId";
            ddluser.DataBind();
            ddluser.Items.Insert(0, "-Select-");
            ddluser.Items[0].Value = "0";
            ddluser.SelectedIndex = ddluser.Items.IndexOf(ddluser.Items.FindByValue(lblpid.Text));

            ddldocty.DataSource = dts1;
            ddldocty.DataTextField = "name";
            ddldocty.DataValueField = "Id";
            ddldocty.DataBind();
            ddldocty.SelectedIndex = ddldocty.Items.IndexOf(ddldocty.Items.FindByValue(lbldoctid.Text));


        }

    }
    protected void fillgddefalt()
    {
        DataTable dt =new DataTable();
        if (ViewState["data"] != null)
        {
            dt = (DataTable)ViewState["data"];

        }
       
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            DropDownList ddldoctypemas = (DropDownList)Gridreqinfo.Rows[i].FindControl("ddldoctypemas");
            DropDownList ddluser = (DropDownList)Gridreqinfo.Rows[i].FindControl("ddluser");
            DropDownList ddldocty = (DropDownList)Gridreqinfo.Rows[i].FindControl("ddldocty");
            Label lbldocmasId = (Label)Gridreqinfo.Rows[i].FindControl("lbldocmasId");
            Label lblpid = (Label)Gridreqinfo.Rows[i].FindControl("lblpid");
            Label lbldoctid = (Label)Gridreqinfo.Rows[i].FindControl("lbldoctid");

            TextBox txtdoctitlegrd = (TextBox)Gridreqinfo.Rows[i].FindControl("txtdoctitlegrd");
            TextBox lblprn = (TextBox)Gridreqinfo.Rows[i].FindControl("lblprn");
            TextBox lbldocrefno = (TextBox)Gridreqinfo.Rows[i].FindControl("lbldocrefno");
            RequiredFieldValidator ReqValPArtyDocRefno = (RequiredFieldValidator)Gridreqinfo.Rows[i].FindControl("ReqValPArtyDocRefno");
            lbldocmasId.Text = ddldoctypemas.SelectedValue;
            lblpid.Text = ddluser.SelectedValue;
            lbldoctid.Text = ddldocty.SelectedValue;
            dt.Rows[i]["documenttype"] = ddldoctypemas.SelectedValue;
            dt.Rows[i]["PartyId"] = ddluser.SelectedValue;
            dt.Rows[i]["DoctyId"] = ddldocty.SelectedValue;

            dt.Rows[i]["DocumentTitle"] = txtdoctitlegrd.Text;
            dt.Rows[i]["PRN"] = lblprn.Text;
            dt.Rows[i]["docrefno"] = lbldocrefno.Text;


            if (ddldocty.SelectedItem.Text == "Credit Invoice" || ddldocty.SelectedItem.Text == "Cash Invoice" || ddldocty.SelectedItem.Text == "Cash Voucher" || ddldocty.SelectedItem.Text == "Credit Voucher")
            {
                ReqValPArtyDocRefno.Visible = true;
            }
            else
            {
                ReqValPArtyDocRefno.Visible = false;
            }
        }
        ViewState["data"] = dt;
    }
    protected void FillGridwithstring(string filaname)
    {
        DataTable dt = new DataTable();
        //if (Convert.ToString(ViewState["data"]) == null)
        if (ViewState["data"] == null)
        {
            dt = CreateDatatable();

            if (filaname.ToString() != null)
            {

                DataRow Drow = dt.NewRow();
                Drow["documentname"] = filaname.ToString();
                Drow["documenttype"] = ddlDocType.SelectedValue;
                Drow["status"] = "Not Uploaded";

                Drow["Businessname"] = ddlbusiness.SelectedItem.Text;
                Drow["DocType"] = ddlDocType.SelectedItem.Text;
                Drow["DocumentTitle"] = txtdoctitle.Text;

                Drow["PartyId"] = ddlpartyname.SelectedValue;
                Drow["Whid"] = ddlbusiness.SelectedValue;
                if (TxtDocDate.Text == "")
                {
                    Drow["docdate"] = DateTime.Now.ToShortDateString();
                }
                else
                {
                    Drow["docdate"] = Convert.ToDateTime(TxtDocDate.Text).ToShortDateString();
                }
                Drow["docrefno"] = txtdocrefnmbr.Text;
                Drow["docamt"] = txtnetamount.Text; ;
                Drow["Docty"] = ddldt.SelectedItem.Text;
                Drow["DoctyId"] = ddldt.SelectedValue;
                Drow["PRN"] = txtpartdocrefno.Text;
                dt.Rows.Add(Drow);
            }
            ViewState["data"] = dt;

            Gridreqinfo.DataSource = dt;
            Gridreqinfo.DataBind();
            fillgridoc();

        }
        else
        {
            fillgddefalt();
            dt = (DataTable)ViewState["data"];

            if (filaname.ToString() != null)
            {

                DataRow Drow = dt.NewRow();
                Drow["documentname"] = filaname.ToString();
                Drow["documenttype"] = ddlDocType.SelectedValue;
                Drow["status"] = "Not Uploaded";

                Drow["Businessname"] = ddlbusiness.SelectedItem.Text;
                Drow["DocType"] = ddlDocType.SelectedItem.Text;
                Drow["DocumentTitle"] = txtdoctitle.Text;

                Drow["PartyId"] = ddlpartyname.SelectedValue;
                Drow["Whid"] = ddlbusiness.SelectedValue;
                if (TxtDocDate.Text == "")
                {
                    Drow["docdate"] = DateTime.Now.ToShortDateString();
                }
                else
                {
                    Drow["docdate"] = Convert.ToDateTime(TxtDocDate.Text).ToShortDateString();
                }
                Drow["docrefno"] = txtdocrefnmbr.Text;
                Drow["docamt"] = txtnetamount.Text; ;
                Drow["Docty"] = ddldt.SelectedItem.Text;
                Drow["DoctyId"] = ddldt.SelectedValue;
                Drow["PRN"] = txtpartdocrefno.Text;

                dt.Rows.Add(Drow);
            }
            ViewState["data"] = dt;
            Gridreqinfo.DataSource = dt;
            Gridreqinfo.DataBind();
            fillgridoc();

            // }
        }



    }
    protected void UploadDocumets()
    {
     
        int i1 = 0;
        int count = 0;

        try
        {

            if (Gridreqinfo.Rows.Count > 0)
            {
                if (Gridreqinfo.Rows.Count == 1)
                {
                    count = 1;
                }
                else if (Gridreqinfo.Rows.Count > 0)
                {
                    count = Gridreqinfo.Rows.Count;
                }
                else
                {
                    count = 0;
                }

                bool access = UserAccess.Usercon("DocumentMaster", "", "DocumentId", "", "", "CID", "DocumentMaster");
                if (access == true)
                {
                    do
                    {
                        string status = Gridreqinfo.Rows[i1].Cells[7].Text;

                        if (status == "Not Uploaded" || status=="")
                        {
                            //====================upload files from grid

                            Label lblfilenamegrd = (Label)Gridreqinfo.Rows[i1].Cells[4].FindControl("lblfilenamegrd");
                            Label lbldoctid = (Label)Gridreqinfo.Rows[i1].FindControl("lbldoctid");
                            TextBox lblprn = (TextBox)Gridreqinfo.Rows[i1].FindControl("lblprn");
                            string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + lblfilenamegrd.Text.Replace(" ", "_");

                            string path1 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\" + lblfilenamegrd.Text);
                            string path2 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + filename.ToString());
                            ViewState["filp"] = path2;


                            if (System.IO.File.Exists(path2))
                            {
                            }
                            else
                            {
                                File.Copy(path1, path2);
                            }
                            string filexten = Path.GetExtension(path2);

                            Label lbldocdate = (Label)Gridreqinfo.Rows[i1].Cells[7].FindControl("lbldocdate");
                            Label lblwhid = (Label)Gridreqinfo.Rows[i1].Cells[6].FindControl("lblwhid");
                            TextBox lbldocrefno = (TextBox)Gridreqinfo.Rows[i1].Cells[8].FindControl("lbldocrefno");
                            Label lbldocamt = (Label)Gridreqinfo.Rows[i1].Cells[9].FindControl("lbldocamt");
                            Label lblpid = (Label)Gridreqinfo.Rows[i1].Cells[1].FindControl("lblpid");



                            TextBox txtdoctitlegrd = (TextBox)Gridreqinfo.Rows[i1].Cells[3].FindControl("txtdoctitlegrd");



                            if (lbldocamt.Text == "")
                            {
                                lbldocamt.Text = "0";
                            }
                            Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(Gridreqinfo.DataKeys[i1].Value.ToString()), 2, Convert.ToDateTime(System.DateTime.Now.ToString()), filename.ToString(), txtdoctitlegrd.Text, "", Convert.ToInt32(lblpid.Text), lbldocrefno.Text, Convert.ToDecimal(lbldocamt.Text), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(lbldocdate.Text), filexten, lbldoctid.Text, lblprn.Text);

                            if (Chkautoprcss.Checked == true)//Filing desk approval not required
                            {
                                bool st = Chkautoprcss.Checked;
                                ViewState["rst"] = rst.ToString();
                                if (st.ToString() == "True")
                                {
                                    bool dcaprv = true;
                                  // bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);
                                   string str1 = " INSERT INTO DocumentProcessing  (EmployeeId ,DocumentId,Approve,CID,StatusId,Levelofaccess) VALUES  ('" + Session["EmployeeId"] + "' ,'" + rst + "','" + dcaprv + "','" + Session["Comid"].ToString() + "','1','3') ";
                                   SqlCommand cmd1 = new SqlCommand(str1, con);
                                   con.Open();
                                   cmd1.ExecuteNonQuery();
                                   con.Close();
                                }
                                Gridreqinfo.Rows[i1].Cells[7].Text = "Uploaded";
                                Gridreqinfo.Rows[i1].Cells[7].ForeColor = System.Drawing.Color.Green;
                                int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                               Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);
                            }
                            if (Chkautoprcss.Checked == false)
                            {
                                bool insdata;
                                DataTable dtt = new DataTable();
                                DataTable DtMain = new DataTable();

                                dtt = new DataTable();
                                dtt = ClsCompany.selectAutoAllocationMaster(lblwhid.Text);
                                if (dtt.Rows.Count > 0)
                                {
                                    #region                                   
                                    foreach (DataRow drr in dtt.Rows)
                                    {
                                        #region                                        
                                        Int32 empid = 0;
                                        empid = Convert.ToInt32(drr["EmployeeId"].ToString());

                                        insdata = false;

                                        int accesslevel = 0;

                                        string strdesig = " select DesignationMaster.* from EmployeeMaster inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID where EmployeeMaster.EmployeeMasterID='" + empid + "'  ";
                                        SqlCommand cmdeeed = new SqlCommand(strdesig, con);
                                        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                                        DataTable dteeed = new DataTable();
                                        adpeeed.Fill(dteeed);
                                        #region
                                      
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
                                        #endregion
                                           string str1 = " INSERT INTO DocumentProcessing  (DocumentId ,EmployeeId,DocAllocateDate,CID,StatusId,Levelofaccess) VALUES  ('" + rst + "' ,'" + empid + "','" + System.DateTime.Now.ToShortDateString() + "','" + Session["Comid"].ToString() + "','0','" + accesslevel + "') ";
                                           SqlCommand cmd1 = new SqlCommand(str1, con);
                                            con.Open();
                                            cmd1.ExecuteNonQuery();
                                            con.Close();

                                        //   insdata = clsDocument.insertDocumentProcessing(empid, rst);
                                        #endregion
                                    }
                                    #endregion
                                }

                            }

                            string Location = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocuments/");
                            string Location2 = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocumentsTemp/");
                            TextBox txtdoctitl = (TextBox)Gridreqinfo.Rows[i1].Cells[8].FindControl("txtdoctitlegrd");
                            DropDownList ddldoctypemas = (DropDownList)Gridreqinfo.Rows[i1].Cells[9].FindControl("ddldoctypemas");
                            sendaccessmail(Convert.ToInt32(Gridreqinfo.DataKeys[i1].Value.ToString()), Convert.ToInt32(lblwhid.Text), ddldoctypemas.SelectedItem.Text, txtdoctitl.Text);


                            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
                            if (filexten == ".pdf")
                            {
                                foreach (System.IO.FileInfo f in dir.GetFiles(filename))
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
                                        System.IO.File.Copy(Location + f.Name, Location2 + f.Name);
                                    }
                                    System.IO.File.SetAttributes(Location2 + f.Name, System.IO.FileAttributes.Hidden);

                                    //}
                                }
                            }

                            //string argument = "-p " + physicalpath + " -v " + virtualfilename + " -u -f " + outputpath + " -fixednames";
                            // Process.Start(fullcompilerpath, argument).WaitForExit();
                            /////addede
                            if (filexten == ".pdf")
                            {
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
                            }
                            if (Request.QueryString["Tid"] != null)
                            {
                                SqlCommand cmdi = new SqlCommand("InsertAttachmentMaster", con);

                                cmdi.CommandType = CommandType.StoredProcedure;
                                cmdi.Parameters.Add(new SqlParameter("@Titlename", SqlDbType.NVarChar));
                                cmdi.Parameters["@Titlename"].Value = "Document from MultiUpload";
                                cmdi.Parameters.Add(new SqlParameter("@Filename", SqlDbType.NVarChar));
                                cmdi.Parameters["@Filename"].Value = filename;

                                cmdi.Parameters.Add(new SqlParameter("@Datetime", SqlDbType.DateTime));
                                cmdi.Parameters["@Datetime"].Value = (Convert.ToDateTime(System.DateTime.Now.ToString())).ToString(); ;
                                cmdi.Parameters.Add(new SqlParameter("@RelatedtablemasterId", SqlDbType.NVarChar));
                                cmdi.Parameters["@RelatedtablemasterId"].Value = "5";
                                cmdi.Parameters.Add(new SqlParameter("@RelatedTableId", SqlDbType.NVarChar));
                                cmdi.Parameters["@RelatedTableId"].Value = ViewState["tid"];
                                cmdi.Parameters.Add(new SqlParameter("@IfilecabinetDocId", SqlDbType.NVarChar));
                                cmdi.Parameters["@IfilecabinetDocId"].Value = rst.ToString();
                                cmdi.Parameters.Add(new SqlParameter("@Attachment", SqlDbType.Int));
                                cmdi.Parameters["@Attachment"].Direction = ParameterDirection.Output;

                                cmdi.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                                cmdi.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                Int32 result = cmdi.ExecuteNonQuery();
                                result = Convert.ToInt32(cmdi.Parameters["@Attachment"].Value);
                                con.Close();

                            }
                            lblmsg.Visible = true;
                            if (count == 1)
                            {

                                lblmsg.Text = "Document uploaded successfully.";
                            }
                            else
                            {
                                lblmsg.Text = "Documents uploaded successfully.";
                            }
                        }

                        i1 = i1 + 1;
                    } while (i1 <= Gridreqinfo.Rows.Count - 1);
                }
                else
                {
                    lblmsg.Visible = true;
                   // lblmsg.Text = "Sorry, You are not permitted for greater record to Priceplan";
                    lblmsg.Text = Convert.ToString(Session["msgdata"]);
                }
            }


            //=========================== update status in view state

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["data"];
            foreach (DataRow dr in dt.Rows)
            {
                dr["status"] = "Uploaded";
            }
            ViewState["data"] = dt;



        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();

        }
    }

    protected void DeleteFromGrid(int rowindex)
    {
        fillgddefalt();
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["data"];
        dt.Rows[rowindex].Delete();
        dt.AcceptChanges();
        Gridreqinfo.DataSource = dt;
        Gridreqinfo.DataBind();
        ViewState["data"] = dt;
        fillgridoc();
        fillgddefalt();
        lblmsg.Text = "Record Deleted successfully.";

    }

    protected void clear()
    {


        if (ddlDocType.Items.Count > 0)
        {
            ddlDocType.SelectedIndex = 0;
            Gridreqinfo.DataSource = null;
            Gridreqinfo.DataBind();
        }


    }

    protected void imgbtnreset_Click(object sender, EventArgs e)
    {
        ViewState["data"] = null;
        Gridreqinfo.DataSource = null;
        Gridreqinfo.DataBind();
        txtdoctitle.Text = "";

        lblmsg.Text = "";
        imgbtnUpload.Visible = false;
        txtdoctitle.Text = "";
        txtdocrefnmbr.Text = "0";
        txtnetamount.Text = "0";



    }

    public void filldll()
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocTypeAll(ddlbusiness.SelectedValue);
        ddlDocType.DataSource = dt;
        ddlDocType.DataTextField = "doctype";
        ddlDocType.DataValueField = "DocumentTypeId";
        ddlDocType.DataBind();
        ddlDocType.SelectedIndex = ddlDocType.Items.IndexOf(ddlDocType.Items.FindByText("GENERAL - GENERAL - GENERAL"));

    }


    protected void Gridreqinfo_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        Gridreqinfo.PageIndex = e.NewPageIndex;
        FillGrid();

    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldll();
        FillParty();
    }


    protected void Gridreqinfo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/AccEntryDocUp.aspx?tid=" + ViewState["tid"] + "");
    }
    protected void ImageButton49_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentSubSubType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton48_Click(object sender, ImageClickEventArgs e)
    {
        filldll();
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
    protected void FillParty()
    {
        DataTable dt = new DataTable();
        dt = clsDocument.selectparty(ddlbusiness.SelectedValue);
        ddlpartyname.DataSource = dt;
        ddlpartyname.DataTextField = "PartyName";
        ddlpartyname.DataValueField = "PartyId";
        ddlpartyname.DataBind();
        ddlpartyname.Items.Insert(0, "-Select-");
        ddlpartyname.Items[0].Value = "0";


    }
    protected void sendaccessmail(int docid, int whid, string cabinetdrawer, string title)
    {
        string acess = "";
        string repumd = "";
        DataTable dt = select("Select distinct RuleDetail.RuleApproveTypeId, RuleMaster.RuleId, RuleDetail.EmployeeId,ConditionTypeId, EmployeeMaster.Email, RuleApproveTypeMaster.RuleApproveTypeName, RuleMaster.RuleDate,StepId,EmployeeMaster.EmployeeName, RuleMaster.RuleTitle,RuleDetail.RuleDetailId from RuleMaster inner join " +
                  "RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId= " +
                   " RuleDetail.EmployeeId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId where DocumentTypeId='" + docid + "' and RuleMaster.Approvemail='1' and (RuleMaster.Active='1') order by RuleDetailId ASC");
        if (dt.Rows.Count > 0)
        {
            repumd += Convert.ToString(dt.Rows[0]["RuleId"]);
            if (repumd.Length > 0)
            {
                acess = " and  RuleMaster.RuleId Not in(" + repumd + ")";
            }
            sendmail(dt, docid, whid, cabinetdrawer, title);
        }
        DataTable dt1 = select("Select distinct RuleDetail.RuleApproveTypeId, RuleMaster.RuleId, RuleDetail.EmployeeId,ConditionTypeId, EmployeeMaster.Email, RuleApproveTypeMaster.RuleApproveTypeName, RuleMaster.RuleDate,StepId,EmployeeMaster.EmployeeName, RuleMaster.RuleTitle,RuleDetail.RuleDetailId from RuleMaster inner join " +
                      "RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId= " +
                       " RuleDetail.EmployeeId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId where DocumentSubId in(Select DocumentType.DocumentSubTypeId from DocumentType inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId  where DocumentTypeId='" + docid + "') " + acess + " and RuleMaster.Approvemail='1' and (RuleMaster.Active='1') order by RuleDetailId ASC");
        if (dt1.Rows.Count > 0)
        {
            if (repumd.Length <= 0)
            {
                repumd += Convert.ToString(dt1.Rows[0]["RuleId"]);

            }
            else
            {
                repumd += "," + Convert.ToString(dt1.Rows[0]["RuleId"]);
            }
            acess = " and  RuleMaster.RuleId Not in(" + repumd + ")";
            sendmail(dt1, docid, whid, cabinetdrawer, title);
        }
        DataTable dt2 = select("Select distinct RuleDetail.RuleApproveTypeId, RuleMaster.RuleId, RuleDetail.EmployeeId,ConditionTypeId, EmployeeMaster.Email, RuleApproveTypeMaster.RuleApproveTypeName, RuleMaster.RuleDate,StepId,EmployeeMaster.EmployeeName, RuleMaster.RuleTitle,RuleDetail.RuleDetailId from RuleMaster inner join " +
                          "RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId= " +
                           " RuleDetail.EmployeeId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId where DocumentMainId in(Select DocumentMainType.DocumentMainTypeId from DocumentType inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId where DocumentTypeId='" + docid + "') " + acess + " and RuleMaster.Approvemail='1' and (RuleMaster.Active='1') order by RuleDetailId ASC");
        if (dt2.Rows.Count > 0)
        {
            sendmail(dt2, docid, whid, cabinetdrawer, title);
        }


    }
    public void sendmail(DataTable dtsc, int documentid, int whid, string cabinetdrawer, string title)
    {
        foreach (DataRow dts in dtsc.Rows)
        {
            if (Convert.ToString(dts["Email"]) != "")
            {
                if ((Convert.ToInt32(dts["ConditionTypeId"]) == 2) || (Convert.ToInt32(dts["ConditionTypeId"]) == 1 && Convert.ToInt32(dts["StepId"]) == 1))
                {
                    string str = " Select Distinct CompanyMaster.CompanyName,EmployeeMaster.EmployeeName, OutGoingMailServer,WebMasterEmail, EmailMasterLoginPassword, AdminEmail,MasterEmailId, CompanyMaster.CompanyLogo,WarehouseMaster.Name as Wname,CityMasterTbl.CityName,StateMasterTbl.Statename,CountryMaster.CountryName from EmployeeMaster inner join WarehouseMaster on WarehouseMaster.WarehouseId=EmployeeMaster.Whid" +
                                    " inner join CompanyWebsitMaster on  CompanyWebsitMaster.Whid= WarehouseMaster.WarehouseId inner join " +
                                  " CompanyMaster on CompanyMaster.CompanyId=CompanyWebsitMaster.CompanyId inner join CompanyAddressMaster" +
                                 " on CompanyAddressMaster.CompanyMasterId=CompanyMaster.CompanyId inner join CountryMaster on " +
                                    "CountryMaster.CountryId=CompanyAddressMaster.Country inner join StateMasterTbl on " +
                                 "StateMasterTbl.StateId=CompanyAddressMaster.State inner join CityMasterTbl on " +
                                "CityMasterTbl.CityId=CompanyAddressMaster.City where  EmployeeMaster.EmployeeMasterId='" + dts["EmployeeId"] + "'";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        SqlDataAdapter da11 = new SqlDataAdapter("select LogoUrl from CompanyWebsitMaster inner join employeemaster on EmployeeMaster.Whid=CompanyWebsitMaster.WHId where EmployeeMaster.EmployeeMasterID='" + dts["EmployeeId"] + "'", con);
                        DataTable dt11 = new DataTable();
                        da11.Fill(dt11);

                        if (dt11.Rows.Count > 0)
                        {
                            SqlCommand cmdxx = new SqlCommand();
                            cmdxx.CommandText = "InsertEmailApproval";
                            cmdxx.CommandType = CommandType.StoredProcedure;

                            cmdxx.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
                            cmdxx.Parameters["@Whid"].Value = whid;
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
                            strhead.Append("<table width=\"50%\"> ");
                            strhead.Append("<tr><td Width=\"50%\" align=\"left\"> <img src=\"http://" + Request.Url.Host.ToString() + "/Shoppingcart/images/" + Convert.ToString(dt11.Rows[0]["LogoUrl"]) + "\" \"border=\"0\" width=\"100\" height=\"60\" / > </td>" +
                            "<td align=\"left\"><b><span style=\"color: #996600\">" + Convert.ToString(dt.Rows[0]["Wname"]) + "</span></b><BR>" + Convert.ToString(dt.Rows[0]["CityName"]) + "<Br>" + Convert.ToString(dt.Rows[0]["Statename"]) + "<Br>" + Convert.ToString(dt.Rows[0]["CountryName"]) + "</td></tr>  ");
                            strhead.Append("<tr><td  colspan=\"2\"><br></td></tr>");
                            strhead.Append("<tr><td  colspan=\"2\"><b>Dear " + Convert.ToString(dt.Rows[0]["EmployeeName"]) + ",</b></td></tr>");
                            strhead.Append("<tr><td  colspan=\"2\"><br></td></tr>");
                            strhead.Append("<tr><td  colspan=\"2\" align=\"left\"><b> The following document entry requires your approval:</b></td></tr>");
                            strhead.Append("<tr><td  colspan=\"2\"><table width=\"100%\">");
                            strhead.Append("<tr><td> Business Name :</td><td  align=\"left\">" + Convert.ToString(dt.Rows[0]["Wname"]) + "</td></tr>");
                            if (title != "")
                            {
                                strhead.Append("<tr><td> Document Title :</td><td  align=\"left\">" + title + "</td></tr>");
                            }
                            else
                            {
                                strhead.Append("<tr><td> Document Title :</td><td  align=\"left\">'document title'</td></tr>");
                            }
                            strhead.Append("<tr><td> Cabinet-Drower-Folder :</td><td  align=\"left\">" + cabinetdrawer + "</td></tr>");
                            strhead.Append("<tr><td> Document Approval Rule Type :</td><td  align=\"left\">" + dts["RuleApproveTypeName"] + "</td></tr>");
                            strhead.Append("<tr><td>  Document Approval Rule Name :</td><td  align=\"left\">" + dts["RuleTitle"] + "</td></tr>");
                            strhead.Append("</table></td></tr> ");
                            DataTable dt2 = select(" Select EmployeeMaster.EmployeeName, RuleDetail.RuleDetailId,DesignationMaster.DesignationName,DepartmentmasterMNC.Departmentname,RuleProcessDate from RuleMaster inner join RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId " +
                            " inner join RuleProcessMaster on RuleProcessMaster.RuleDetailId=RuleDetail.RuleDetailId" +
                            " inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId=  RuleProcessMaster.EmployeeId inner join DesignationMaster" +
                            " on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId inner join DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptID where RuleProcessMaster.DocumentId='" + ViewState["rst"] + "' and RuleMaster.RuleId='" + dts["RuleId"] + "'");
                            int i = 0;
                            foreach (DataRow dx in dt2.Rows)
                            {
                                if (i == 0)
                                {
                                    strhead.Append("<tr><td colspan=\"2\"><table width=\"100% BorderColor=Black BorderStyle=Solid\">");
                                    strhead.Append("<tr><td colsplan=\"4\"><b>This Document as already approved by </b> </td>");
                                    strhead.Append("<tr><td  align=\"left\">Employee Name </td><td  align=\"left\">Designation Name</td> <td   align=\"left\">Department Name</td>  <td  align=\"left\">Aproval DateTime</td></tr>");
                                }

                                strhead.Append("<tr><td  align=\"left\">" + dx["EmployeeName"] + " </td><td   align=\"left\">" + dx["DesignationName"] + "</td> <td   align=\"left\">" + dx["Departmentname"] + "</td>  <td  align=\"left\">" + dx["RuleProcessDate"] + "</td></tr>");
                                i = i + 1;

                            }
                            if (i > 0)
                            {
                                strhead.Append("</table></td></tr> ");

                            }
                            strhead.Append("<tr><td colspan=\"2\"><br></td></tr>");
                            strhead.Append("<tr><td colspan=\"2\" align=\"left\"><b>You can approve or reject the document from the link given below: </b></td></tr>");
                            strhead.Append("<tr><td colspan=\"2\" align=\"left\"><b><a href=http://" + Request.Url.Host.ToString() + "/EmailDocApprove.aspx?cn=" + result + "&rdt=" + dts["RuleDetailId"] + "&cid=" + Session["Comid"] + ">Document Approved</a></b></td></tr>");
                            strhead.Append("<tr><td colspan=\"2\" align=\"left\"><b><a href=http://" + Request.Url.Host.ToString() + "/EmailDocApprove.aspx?ap=ync&cn=" + result + "&rdt=" + dts["RuleDetailId"] + "&cid=" + Session["Comid"] + ">Document Rejected</a></b></td></tr>");
                            strhead.Append("<tr><td colspan=\"2\"><br></td></tr>");
                            strhead.Append("<tr><td colspan=\"2\"><b>Best regards</b></td></tr>");
                            //strhead.Append("<tr><td><b>Sincerely</b></td></tr>");
                            strhead.Append("<tr><td colspan=\"2\"><br></td></tr>");
                            strhead.Append("<tr><td colspan=\"2\"><b>Filling Desk Team</b></td></tr>");
                            strhead.Append("<tr><td colspan=\"2\"><b> " + Convert.ToString(dt.Rows[0]["Wname"]) + "</b></td></tr>");
                            strhead.Append("</table> ");

                            string AdminEmail = dt.Rows[0]["MasterEmailId"].ToString();// TextAdminEmail.Text;
                            //string AdminEmail = txtusmail.Text;
                            String Password = dt.Rows[0]["EmailMasterLoginPassword"].ToString();// TextEmailMasterLoginPassword.Text;

                            //string body = "Test Mail Server - TestIwebshop";
                            MailAddress to = new MailAddress(dts["Email"].ToString());
                            // MailAddress to = new MailAddress("maheshsorathiya500@gmail.com");
                            MailAddress from = new MailAddress(AdminEmail);

                            MailMessage objEmail = new MailMessage(from, to);
                            objEmail.Subject = "Document for Approval of " + Convert.ToString(dt.Rows[0]["EmployeeName"]);

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
        }
    }

    protected DataTable select(string std)
    {
        SqlDataAdapter cidco = new SqlDataAdapter(std, con);
        DataTable dts = new DataTable();
        cidco.Fill(dts);
        return dts;
    }

    protected void Gridreqinfo_DataBound(object sender, EventArgs e)
    {

    }
    protected void Gridreqinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblfilenamegrd = (Label)e.Row.FindControl("lblfilenamegrd");

            HyperLink FileOpenLink = (HyperLink)e.Row.FindControl("FileOpen");

            FileOpenLink.NavigateUrl = "~/Account/" + Session["comid"] + "/TempDoc/" + lblfilenamegrd.Text.ToString();


        }
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
