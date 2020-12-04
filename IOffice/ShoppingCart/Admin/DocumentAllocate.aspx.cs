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

using System.Data.SqlClient;

public partial class Account_DocumentAllocate : System.Web.UI.Page
{
    SqlConnection con;
    [DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    public static extern int apOpen(string lpFileName, string lpOwnerPw, string lpUserPw);

    [DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    public static extern int apClose(int nHandle);

    [DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    public static extern int apConvert(int nHandle);

    [DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    public static extern int apConvertPage(int nHandle, int nPageNo);

    [DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    public static extern int apConvertPageToStream(int nHandle, int nPageNo, ref int pMemStream, ref int nSize);

    [DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    public static extern int apSetProperty(int nHandle,  //Conversion object's handle.
                                            int nIndex,   //Property tag,refer to the following property definitions.
                                            string lpValue,  //A String value of the property,(or)
                                            int nValue,   //An integer value of the property.
                                            int nOther);  //Only valid for progress call-back.

    [DllImport("pdf2image", CallingConvention = CallingConvention.StdCall)]
    public static extern int apGetProperty(int nHandle, //Conversion object's handle.
                                            int nIndex,  //Property tag,refer to the following property definitions.
                                            ref int pValue,     //A String value of the property,(or)
                                            ref int nValue);    //An integer value of the property.
    //==============================================================================================================

    //1.2 Definitions of converter's properties 
    //==============================================================================================================
    //read/write properties
    const int AP_PROP_OUTDIR = 1;  //Destination directory.
    const int AP_PROP_PREFIX = 2;  //The prefix of the name of result image file.
    const int AP_PROP_PAGEZOOM = 3;  //Zoom scale of the source pdf page.
    const int AP_PROP_BGCOLOR = 4;  //Image background color.
    const int AP_PROP_IMAGETYPE = 5;  //Result image format.
    const int AP_PROP_BITCOUNT = 6;  //Color bits per pixel.
    const int AP_PROP_XDPI = 7;  //Horizontal resolution.
    const int AP_PROP_YDPI = 8;  //Vertical resolution.
    const int AP_PROP_QUALITY = 9;  //JPEG compression quality.
    const int AP_PROP_COMPRESSION = 10; //TIFF compression mode.
    const int AP_PROP_MULTIPAGES = 11; //Multipages TIFF file.
    const int AP_PROP_GRAYSCALE = 12; //Grayscale image.
    const int AP_PROP_XDIMENSIONS = 13; //The width of the result image in pixel
    const int AP_PROP_YDIMENSIONS = 14; //The height of the result image in pixel

    //read only properties
    const int AP_PROP_PAGECOUNT = 20; //The total pages of source pdf file.
    const int AP_PROP_PAGEHEIGHT = 21; //Height of the current page of source pdf file.
    const int AP_PROP_PAGEWIDTH = 22; //Width of the current page of source pdf file.
    //==============================================================================================================

    //1.3 TIFF tag's definitions.
    //==============================================================================================================
    const int AP_TIFF_COMPRESSION_NONE = 0;  //No compression.
    const int AP_TIFF_COMPRESSION_LZW = 1;  //1,4,8,24bits(default 4,8,24bits)
    const int AP_TIFF_COMPRESSION_JPEG = 2;  //Grayscale 8bits,24bits
    const int AP_TIFF_COMPRESSION_PACKBITS = 3;  //4,8,24bits
    const int AP_TIFF_COMPRESSION_CCITTG4 = 4;  //1bit(default 1bit)
    const int AP_TIFF_COMPRESSION_CCITTG3 = 5;  //1bit
    const int AP_TIFF_COMPRESSION_RLE = 6;  //1bit
    //==============================================================================================================

    //1.4 Image type's definitions
    //==============================================================================================================
    const int AP_IMAGE_BMP = 1;   //BMP
    const int AP_IMAGE_EMF = 2;   //EMF
    const int AP_IMAGE_WMF = 3;   //WMF
    const int AP_IMAGE_JPG = 4;   //JPG
    const int AP_IMAGE_PNG = 5;   //PNG
    const int AP_IMAGE_GIF = 6;   //GIF
    const int AP_IMAGE_TIF = 7;   //TIF
    const int AP_IMAGE_PCX = 8;   //PCX

    const int AP_IMAGE_JPEG = 4;   //JPEG
    const int AP_IMAGE_TIFF = 7;   //TIFF
    //==============================================================================================================

    //1.5 Return code's definitions.
    //==============================================================================================================
    const int RTN_OK = 1;           //Successful operation.
    const int ERR_UNKNOWN = -99;    //Unknown system error.

    const int ERR_OVER_MAXTHREADS = -1;  //Over the limit amount of threads.
    const int ERR_FILE_UNEXIST = -2;  //Source PDF file unexist.
    const int ERR_FILE_DAMAGED = -3;  //Source PDF file is damaged.
    const int ERR_FILE_RESTRICTED = -4;  //Source PDF file is restricted.
    //==============================================================================================================

    //1.6 Windows API definitions.
    //==============================================================================================================
    [DllImport("kernel32.dll")]
    public static extern int GetTickCount();
    [DllImport("kernel32.dll")]
    public static extern void CopyMemory(Byte[] dest, int Source, Int32 length);

    const int TRUE = 1;
    const int FALSE = 0;




    DocumentCls1 clsDocument = new DocumentCls1();
    MasterCls clsMaster;
    Companycls ClsCompany = new Companycls();
    protected int DesignationId;
    EmployeeCls clsEmployee = new EmployeeCls();

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

        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);

            ViewState["sortOrder"] = "";
            fillDDl(ddlbusiness, "Name", "WareHouseId", "SELECT WareHouseId,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name");
            string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
            SqlCommand cmdeeed = new SqlCommand(eeed, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);
            if (dteeed.Rows.Count > 0)
            {
                ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

            }



            SelectDocumentforProcessing();
            selectEMployeelist();




            DataTable dt_emailftp_priceplan = new DataTable();


        }


    }
    protected void fillDDl(DropDownList ddl, string Textfild, string valfild, string text)
    {
        SqlDataAdapter adp = new SqlDataAdapter(text, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddl.DataSource = dt;
        ddl.DataTextField = Textfild;
        ddl.DataValueField = valfild;
        ddl.DataBind();
    }
    protected void SelectDocumentforProcessing()
    {
        DataTable Dt = new DataTable();
        Dt = clsDocument.SelectDocumentforProcessing(ddlbusiness.SelectedValue);

        grdDocList.DataSource = Dt;
        DataView myDataView = new DataView();
        myDataView = Dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        grdDocList.DataBind();

    }
    protected void FillGridDesignationwithDept()
    {
        DataTable dt;
        dt = new DataTable();

        clsMaster = new MasterCls();
        //dt = clsMaster.SelectDesignationMasterwithDept(6);
        dt = clsMaster.SelectDesignationMasterwithDataDept(ddlbusiness.SelectedValue);

        grdDesignation.DataSource = dt;
        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        grdDesignation.DataBind();

    }

    protected void imgbShowEmp_Click(object sender, EventArgs e)
    {
        selectEMployeelist();
    }
    public void selectEMployeelist()
    {
        //{
        //    int i;
        //    i = 0;
        //    DataTable dt;
        //    DataTable dtt;
        //    DataTable dttt = new DataTable();
        //    DataSet ds = new DataSet();
        //    grdEmployeeList.DataSource = null;
        //    grdEmployeeList.DataBind();
        //    if (grdDesignation.Rows.Count > 0)
        //    {
        //        do
        //        {
        //            CheckBox chkdegs = (CheckBox)grdDesignation.Rows[i].FindControl("chkDesignation");

        //            if (chkdegs.Checked == true)
        //            {
        //                Int32 degid;
        //                degid = Convert.ToInt32(grdDesignation.DataKeys[i].Value.ToString());
        //                clsMaster = new MasterCls();
        //                dt = new DataTable();
        //                dt = clsMaster.selectEmployeewithDesignation(degid, ddlbusiness.SelectedValue);
        //                dtt = new DataTable();
        //                if (dt.Rows.Count > 0)
        //                {
        //                    dtt = dt.Copy();
        //                    dtt.TableName = degid.ToString();
        //                    ds.Tables.Add(dtt);// =   (DataSet) dtt; 
        //                }
        //            }
        //            i = i + 1;
        //        } while (i <= grdDesignation.Rows.Count - 1);
        //    }
        //    DataSet dsmain = new DataSet();
        //    dsmain = ds.Clone();
        //    int j;
        //    j = 0;
        //    if (ds.Tables.Count > 0)
        //    {
        //        do
        //        {
        //            foreach (DataRow r in ds.Tables[j].Rows)
        //            {
        //                dsmain.Tables[0].ImportRow(r);
        //            }

        //            j = j + 1;
        //        } while (j <= ds.Tables.Count - 1);

        //        DataView myDataView = new DataView();
        //        myDataView = dsmain.Tables[0].DefaultView;

        //        if (hdnsortExp.Value != string.Empty)
        //        {
        //            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //        }
        //        grdEmployeeList.DataSource = dsmain.Tables[0]; // ds;
        //        grdEmployeeList.DataBind();
        //        imgbtnallocate.Visible = true;
        //    }

        //    setGridisze();

        string allbus = "";
        if (rdsetrul.SelectedIndex == 0)
        {
            allbus = "  EmployeeMaster.Whid in (SELECT Distinct WareHouseId  FROM WareHouseMaster  where comid = '" + Session["comid"] + "' and WareHouseMaster.Status='1') and";
        }
        else
        {

            allbus = " EmployeeMaster.Whid='" + ddlbusiness.SelectedValue + "' and ";
        }


        string str = " SELECT     EmployeeMaster.EmployeeMasterID as EmployeeID, EmployeeMaster.DesignationMasterId as DesignationId,  WarehouseMaster.Name +':'+ EmployeeMaster.EmployeeName as EmployeeName, DesignationMaster.DesignationName, EmployeeMaster.Address,    EmployeeMaster.City, EmployeeMaster.StateId as State, EmployeeMaster.CountryId as Country, EmployeeMaster.ContactNo as Phone,  EmployeeMaster.Email,     EmployeeMaster.DateOfJoin as DOJ, EmployeeMaster.SuprviserId,    EmployeeMaster.StatusMasterId as StatusId, DepartmentmasterMNC.DepartmentName FROM          WarehouseMaster inner join EmployeeMaster on EmployeeMaster.Whid=WarehouseMaster.WarehouseId INNER JOIN    DesignationMaster ON EmployeeMaster.DesignationMasterId = DesignationMaster.DesignationMasterId LEFT OUTER JOIN       DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id WHERE   " + allbus + " DesignationMaster.DesignationName in('Manager','Supervisor','Office Clerk')   ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        grdEmployeeList.DataSource = dt;
        grdEmployeeList.DataBind();
    }
    protected void imgbtnallocate_Click(object sender, EventArgs e)
    {
        int Docgrd, Empgrd;
        bool insdata;
        insdata = false;
        Docgrd = 0;
        Empgrd = 0;


        if (grdDocList.Rows.Count > 0)
        {
            do
            {
                CheckBox chkdegs = (CheckBox)grdDocList.Rows[Docgrd].FindControl("chkDoc");
                Int32 docidd = Int32.Parse(grdDocList.DataKeys[Docgrd].Value.ToString());
                if (chkdegs.Checked == true)
                {
                    Empgrd = 0;
                    do
                    {
                        CheckBox chkEmployee = (CheckBox)grdEmployeeList.Rows[Empgrd].FindControl("chkEmployee");

                        // insdata = false;
                        if (chkEmployee.Checked == true)
                        {
                           
                                Int32 empid = 0;
                                empid = Convert.ToInt32(grdEmployeeList.DataKeys[Empgrd].Value.ToString());

                                insdata = false;

                                int accesslevel = 0;

                                string strdesig = " select DesignationMaster.* from EmployeeMaster inner join DesignationMaster ON EmployeeMaster.DesignationMasterID = DesignationMaster.DesignationMasterID where EmployeeMaster.EmployeeMasterID='" + empid + "'  ";
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

                                string eeed = " Select  * from  DocumentProcessing where  EmployeeId='" + Int32.Parse(grdEmployeeList.DataKeys[Empgrd].Value.ToString()) + "' and DocumentId='" + docidd + "'";
                                SqlCommand cmdeeedc = new SqlCommand(eeed, con);
                                SqlDataAdapter adpeeedc = new SqlDataAdapter(cmdeeedc);
                                DataTable dteeedc = new DataTable();
                                adpeeedc.Fill(dteeedc);
                                if (dteeedc.Rows.Count <= 0)
                                {
                                    string str1 = " INSERT INTO DocumentProcessing  (DocumentId ,EmployeeId,DocAllocateDate,CID,StatusId,Levelofaccess) VALUES  ('" + docidd + "' ,'" + empid + "','" + System.DateTime.Now.ToShortDateString() + "','" + Session["Comid"].ToString() + "','0','" + accesslevel + "') ";
                                    SqlCommand cmd1 = new SqlCommand(str1, con);
                                    con.Open();
                                    insdata=Convert.ToBoolean(cmd1.ExecuteNonQuery());
                                    con.Close();
                                }

                                //   insdata = clsDocument.insertDocumentProcessing(empid, rst);

                           
                            //string eeed = " Select  * from  DocumentProcessing where  EmployeeId='" + Int32.Parse(grdEmployeeList.DataKeys[Empgrd].Value.ToString()) + "' and DocumentId='" + docidd + "'";
                            //SqlCommand cmdeeed = new SqlCommand(eeed, con);
                            //SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                            //DataTable dteeed = new DataTable();
                            //adpeeed.Fill(dteeed);
                            //if (dteeed.Rows.Count <= 0)
                            //{
                            //    insdata = clsDocument.insertDocumentProcessing(Int32.Parse(grdEmployeeList.DataKeys[Empgrd].Value.ToString()), docidd);
                            //}
                        }
                        Empgrd = Empgrd + 1;
                    } while (Empgrd <= grdEmployeeList.Rows.Count - 1);
                }

                Docgrd = Docgrd + 1;
            } while (Docgrd <= grdDocList.Rows.Count - 1);
        }
        if (insdata == true)
        {

            lblmsg.Text = "Document Allocated Successfully.";
            SelectDocumentforProcessing();
        }
        else
        {

            lblmsg.Text = "No documents have been selected to allocate. Please select a document, and an employee.";
        }
        setGridisze();
    }
    public void setGridisze()
    {
        // employee grid

        // doc grid
        if (grdEmployeeList.Rows.Count == 0)
        {
            pnlEmpoylee.CssClass = "GridPanel20";
        }
        else if (grdEmployeeList.Rows.Count == 1)
        {
            pnlEmpoylee.CssClass = "GridPanel125";
        }
        else if (grdEmployeeList.Rows.Count == 2)
        {
            pnlEmpoylee.CssClass = "GridPanel150";
        }
        else if (grdEmployeeList.Rows.Count == 3)
        {
            pnlEmpoylee.CssClass = "GridPanel175";
        }
        else if (grdEmployeeList.Rows.Count == 4)
        {
            pnlEmpoylee.CssClass = "GridPanel200";
        }
        else if (grdEmployeeList.Rows.Count == 5)
        {
            pnlEmpoylee.CssClass = "GridPanel225";
        }
        else if (grdEmployeeList.Rows.Count == 6)
        {
            pnlEmpoylee.CssClass = "GridPanel250";
        }
        else if (grdEmployeeList.Rows.Count == 7)
        {
            pnlEmpoylee.CssClass = "GridPanel275";
        }
        else if (grdEmployeeList.Rows.Count == 8)
        {
            pnlEmpoylee.CssClass = "GridPanel";
        }
        else if (grdEmployeeList.Rows.Count == 9)
        {
            pnlEmpoylee.CssClass = "GridPanel325";
        }
        else if (grdEmployeeList.Rows.Count == 10)
        {
            pnlEmpoylee.CssClass = "GridPanel350";
        }

        else
        {
            pnlEmpoylee.CssClass = "GridPanel375";
        }


        if (grdDesignation.Rows.Count == 0)
        {
            pnldesignation.CssClass = "GridPanel20";
        }
        else if (grdDesignation.Rows.Count == 1)
        {
            pnldesignation.CssClass = "GridPanel125";
        }
        else if (grdDesignation.Rows.Count == 2)
        {
            pnldesignation.CssClass = "GridPanel150";
        }
        else if (grdDesignation.Rows.Count == 3)
        {
            pnldesignation.CssClass = "GridPanel175";
        }
        else if (grdDesignation.Rows.Count == 4)
        {
            pnldesignation.CssClass = "GridPanel200";
        }
        else if (grdDesignation.Rows.Count == 5)
        {
            pnldesignation.CssClass = "GridPanel225";
        }
        else if (grdDesignation.Rows.Count == 6)
        {
            pnldesignation.CssClass = "GridPanel250";
        }
        else if (grdDesignation.Rows.Count == 7)
        {
            pnldesignation.CssClass = "GridPanel275";
        }
        else if (grdDesignation.Rows.Count == 8)
        {
            pnldesignation.CssClass = "GridPanel";
        }
        else if (grdDesignation.Rows.Count == 9)
        {
            pnldesignation.CssClass = "GridPanel325";
        }
        else if (grdDesignation.Rows.Count == 10)
        {
            pnldesignation.CssClass = "GridPanel350";
        }

        else
        {
            pnldesignation.CssClass = "GridPanel375";
        }


        if (grdDocList.Rows.Count == 0)
        {
            pnldocument.CssClass = "GridPanel20";
        }
        else if (grdDocList.Rows.Count == 1)
        {
            pnldocument.CssClass = "GridPanel125";
        }
        else if (grdDocList.Rows.Count == 2)
        {
            pnldocument.CssClass = "GridPanel150";
        }
        else if (grdDocList.Rows.Count == 3)
        {
            pnldocument.CssClass = "GridPanel175";
        }
        else if (grdDocList.Rows.Count == 4)
        {
            pnldocument.CssClass = "GridPanel200";
        }
        else if (grdDocList.Rows.Count == 5)
        {
            pnldocument.CssClass = "GridPanel225";
        }
        else if (grdDocList.Rows.Count == 6)
        {
            pnldocument.CssClass = "GridPanel250";
        }
        else if (grdDocList.Rows.Count == 7)
        {
            pnldocument.CssClass = "GridPanel275";
        }
        else if (grdDocList.Rows.Count == 8)
        {
            pnldocument.CssClass = "GridPanel";
        }
        else if (grdDocList.Rows.Count == 9)
        {
            pnldocument.CssClass = "GridPanel325";
        }
        else if (grdDocList.Rows.Count == 10)
        {
            pnldocument.CssClass = "GridPanel350";
        }

        else
        {
            pnldocument.CssClass = "GridPanel375";
        }

        //////if (grdDocList.Rows.Count == 0)
        //////{
        //////    pnldocument.CssClass = "Gridpnldocument0";
        //////}
        //////else if (grdDocList.Rows.Count > 0 && grdDocList.Rows.Count <= 2)
        //////{
        //////    pnldocument.CssClass = "GridPane100";
        //////}
        //////else if (grdDocList.Rows.Count > 2 && grdDocList.Rows.Count <= 4)
        //////{
        //////    pnldocument.CssClass = "GridPanel150";
        //////}
        //////else if (grdDocList.Rows.Count > 4 && grdDocList.Rows.Count <= 6)
        //////{
        //////    pnldocument.CssClass = "GridPanel225";
        //////}
        //////else if (grdDocList.Rows.Count > 6 && grdDocList.Rows.Count <= 8)
        //////{
        //////    pnldocument.CssClass = "GridPanel";
        //////}
        //////else
        //////{
        //////    pnldocument.CssClass = "GridPanel375";
        //////}
        //////if (grdDocList.Rows.Count >= 0 && grdDocList.Rows.Count < 2)
        //////{
        //////    pnldocument.CssClass = "GridPanel100";
        //////}
        //////else if (grdDocList.Rows.Count >= 2 && grdDocList.Rows.Count <= 4)
        //////{
        //////    pnldocument.CssClass = "GridPanel200";
        //////}
        //////else
        //////{
        //////    pnldocument.CssClass = "GridPanel";
        //////}


    }
    protected void grdDocList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocList.PageIndex = e.NewPageIndex;
        SelectDocumentforProcessing();
    }
    protected void grdEmployeeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEmployeeList.PageIndex = e.NewPageIndex;
        selectEMployeelist();

    }
    protected void grdDocList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //    try {
        //        if (grdDocList.Rows.Count > 0)
        //        {
        //            CheckBox cbHeader = (CheckBox)grdDocList.HeaderRow.FindControl("HeaderChkbox");
        //         cbHeader.Attributes["onclick"] = "ChangeAllCheckBoxStates(this.checked);";
        //       List<string> ArrayValues = new List<string>();
        //        ArrayValues.Add(string.Concat("'", cbHeader.ClientID, "'"));
        //        foreach (GridViewRow gvr in grdDocList.Rows)
        //        {
        //            CheckBox cb = (CheckBox)gvr.FindControl("chkDoc");
        //            cb.Attributes["onclick"] = "ChangeHeaderAsNeeded();";
        //            ArrayValues.Add(string.Concat("'", cb.ClientID, "'"));
        //        }
        //        CheckBoxIDsArray.Text = "<script type='text/javascript'>" + "\n" +  "<!--" + "\n" + String.Concat("var CheckBoxIDs =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") + "\n // -->" + "\n" + "</script>" ;

        //    }
        //    else {
        //    }
        //}
        //catch (Exception ex) {
        //    
        //    lblmsg.Text  = "Error in databound : " + ex.Message.ToString();
        //}
    }
    protected void grdDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
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
        //        //CheckBoxIDsArray.Text = "<script type=\"text/javascript\">" +  Constants.vbCrLf + "<!--" + Constants.vbCrLf + string.Concat("var CheckBoxIDs = new Array(", string.Join(",", ArrayValues.ToArray()), ");") + Constants.vbCrLf + "// -->" + Constants.vbCrLf + "</script>";
        //    }
        //    else
        //    {
        //    }
        //}
        //catch (Exception ex)
        //{
        //    
        //    lblmsg.Text = "Error in  DataBound : " + ex.Message.ToString();
        //}

    }
    protected void grdEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    if (grdEmployeeList.Rows.Count > 0)
        //    {
        //        CheckBox cbHeader = (CheckBox)grdEmployeeList.HeaderRow.FindControl("HeaderChkboxEmp");
        //        cbHeader.Attributes["onclick"] = "ChangeAllCheckBoxStatesEmp(this.checked);";
        //        List<string> ArrayValuesEmp =  new List<string>();
        //        ArrayValuesEmp.Add(string.Concat("'", cbHeader.ClientID, "'"));
        //        foreach (GridViewRow gvr in grdEmployeeList.Rows)
        //        {
        //            CheckBox cb = (CheckBox)gvr.FindControl("chkEmployee");
        //            cb.Attributes["onclick"] = "ChangeHeaderAsNeededEmp();";
        //          ArrayValuesEmp.Add(string.Concat("'", cb.ClientID, "'"));
        //        }
        //        CheckBoxIDsArrayEmp.Text = "<script type='text/javascript'>" + "\n" + "<!--" + "\n" + String.Concat("var CheckBoxIDsEmp =  new Array(", String.Join(",", ArrayValuesEmp.ToArray()), ");") + "\n // -->" + "\n" + "</script>";
        //        //CheckBoxIDsArray.Text = "<script type=\"text/javascript\">" +  Constants.vbCrLf + "<!--" + Constants.vbCrLf + string.Concat("var CheckBoxIDs = new Array(", string.Join(",", ArrayValues.ToArray()), ");") + Constants.vbCrLf + "// -->" + Constants.vbCrLf + "</script>";
        //    }
        //    else
        //    {
        //    }
        //}
        //catch (Exception ex)
        //{
        //   
        //    lblmsg.Text = "Error in databound : " + ex.Message.ToString();
        //}
    }


    protected void grdDocList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            int DocumentId = Convert.ToInt32(e.CommandArgument);
            int rst = clsDocument.InsertDocumentLog(DocumentId, Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, false, false, true, false, false, false, false);
            Response.Redirect("DocumentEditBeforeAllocateApprove.aspx?id=" + DocumentId + "&&return=1");
        }
        if (e.CommandName == "delete1")
        {
            int DocumentId = Convert.ToInt32(e.CommandArgument);
            hdncnfm.Value = DocumentId.ToString();

            mdlpopupconfirm.Show();


        }
    }


    protected void grdDocList_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        SelectDocumentforProcessing();
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
    protected void imgconfirmok_Click(object sender, EventArgs e)
    {
        mdlpopupconfirm.Hide();
        int rst = clsDocument.DeleteDocumentMasterByID(Convert.ToInt32(hdncnfm.Value));

        if (rst > 0)
        {

            lblmsg.Text = "Document Deleted Successfully.";
            SelectDocumentforProcessing();
        }
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectDocumentforProcessing();
        selectEMployeelist();
    }

    protected void HeaderChkboxDes_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox HeaderChkboxDes = (CheckBox)grdDesignation.HeaderRow.FindControl("HeaderChkboxDes");

        foreach (GridViewRow grd in grdDesignation.Rows)
        {
            CheckBox chkDesignation = (CheckBox)grd.FindControl("chkDesignation");
            if (HeaderChkboxDes.Checked == true)
            {

                chkDesignation.Checked = true;
            }
            else
            {
                chkDesignation.Checked = false;
            }
        }

    }
    protected void HeaderChkboxEmp_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox HeaderChkboxEmp = (CheckBox)grdEmployeeList.HeaderRow.FindControl("HeaderChkboxEmp");

        foreach (GridViewRow grd in grdEmployeeList.Rows)
        {
            CheckBox chkEmployee = (CheckBox)grd.FindControl("chkEmployee");
            if (HeaderChkboxEmp.Checked == true)
            {

                chkEmployee.Checked = true;
            }
            else
            {
                chkEmployee.Checked = false;
            }
        }

    }

    protected void HeaderChkbox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox HeaderChkbox = (CheckBox)grdDocList.HeaderRow.FindControl("HeaderChkbox");

        foreach (GridViewRow grd in grdDocList.Rows)
        {
            CheckBox chkDoc = (CheckBox)grd.FindControl("chkDoc");
            if (HeaderChkbox.Checked == true)
            {

                chkDoc.Checked = true;
            }
            else
            {
                chkDoc.Checked = false;
            }
        }

    }
    protected void grdEmployeeList_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        selectEMployeelist();
    }
    protected void grdDesignation_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGridDesignationwithDept();
    }
    protected void rdsetrul_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlbusiness_SelectedIndexChanged(sender, e);
    }
}
