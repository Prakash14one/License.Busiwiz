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
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class Admin_AddDocMaster : System.Web.UI.Page
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

    EmployeeCls clsEmployee = new EmployeeCls();
    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    Companycls ClsCompany = new Companycls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int ik = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[ik - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);

        if (!IsPostBack)
        {
            if (Request.QueryString["emp"] == "yes")   
            {
                ImageButton49.Visible = false;
                ImageButton48.Visible = false;
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton50.Visible = false;
                ImageButton51.Visible = false;  
            }

            Pagecontrol.dypcontrol(Page, page);
            if (Directory.Exists(Server.MapPath("~\\Account\\" + Session["Comid"] + "\\TempDoc")))
            {

                FileInfo[] file = null;
                int i = 0;

                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~\\Account\\" + Session["Comid"] + "\\TempDoc"));
                file = dir.GetFiles();
                if (file.Length > 0)
                {

                    for (i = 0; i <= file.Length - 1; i++)
                    {
                        file[i].Delete();

                    }
                }


            }
            ViewState["sortOrder"] = "";

            ViewState["storeid"] = Request.QueryString["storeid"];
            fillpagetype();

            FillParty();
            rdpop_SelectedIndexChanged(sender, e);
            DataTable dw = select("SELECT Name from WareHouseMaster where WareHouseId='" + ViewState["storeid"] + "'");

            if (dw.Rows.Count > 0)
            {
                ViewState["Wname"] = dw.Rows[0]["Name"].ToString();
            }
            fillpop();
            flaganddoc();
            ddldt_SelectedIndexChanged(sender, e);

        }
    }
    protected void FillParty()
    {
        DataTable dt = new DataTable();
        dt = clsDocument.selectparty(Convert.ToString(ViewState["storeid"]));
        ddlpartyname.DataSource = dt;
        ddlpartyname.DataTextField = "PartyName";
        ddlpartyname.DataValueField = "PartyId";
        ddlpartyname.DataBind();
        //ddlpartyname.Items.Insert(0, "-select-");
        //ddlpartyname.SelectedItem.Value = "0";

        //if (Request.QueryString["objm"] != null)
        //{

        //    string fmdfs = "select employeename from employeemaster where employeemasterid='" + Session["EmployeeID"].ToString() + "'";
        //    SqlDataAdapter da11 = new SqlDataAdapter(fmdfs, con);
        //    DataTable dt11 = new DataTable();
        //    da11.Fill(dt11);

        //    lablcname.Text = dt11.Rows[0]["employeename"].ToString();


        //    ddlpartyname.SelectedIndex = ddlpartyname.Items.IndexOf(ddlpartyname.Items.FindByText("Admin:" + lablcname.Text));
        //}



    }
    protected void fillpagetype()
    {
        ViewState["MissionId"] = 0;
        ViewState["Mdetail"] = 0;
        ViewState["Mevalution"] = 0;
        ViewState["employeemmid"] = 0;

        ViewState["ltgm"] = 0;
        ViewState["ltgd"] = 0;
        ViewState["ltge"] = 0;

        ViewState["stgm"] = 0;
        ViewState["stgd"] = 0;
        ViewState["stge"] = 0;

        ViewState["mom"] = 0;
        ViewState["mod"] = 0;
        ViewState["moe"] = 0;


        ViewState["yem"] = 0;
        ViewState["yed"] = 0;
        ViewState["yee"] = 0;

        ViewState["annam"] = 0;

        ViewState["wem"] = 0;
        ViewState["wed"] = 0;
        ViewState["wee"] = 0;

        ViewState["stram"] = 0;
        ViewState["strad"] = 0;
        ViewState["strae"] = 0;

        ViewState["tectm"] = 0;
        ViewState["tectd"] = 0;
        ViewState["tecte"] = 0;

        ViewState["takid"] = 0;
        ViewState["Proid"] = 0;
        ViewState["Proevaid"] = 0;

        ViewState["commid"] = 0;

        ViewState["InvCountId"] = 0;
        ViewState["CandidateId"] = 0;

        ViewState["storeid"] = Request.QueryString["storeid"];
        DataTable dsc = new DataTable();

        if (Request.QueryString["objm"] != null)
        {

            ViewState["MissionId"] = Request.QueryString["objm"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["MissionId"]), 1);
            lblhead.Text = "Mission Master - ";
        }
        else if (Request.QueryString["employeemm"] != null)
        {

            ViewState["employeemmid"] = Request.QueryString["employeemm"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["employeemmid"]), 500);
            lblhead.Text = "Employee Master - ";
        }
        else if (Request.QueryString["objd"] != null)
        {
            ViewState["Mdetail"] = Request.QueryString["objd"];

            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["Mdetail"]), 11);
            lblhead.Text = "Mission Instruction - ";
        }
        else if (Request.QueryString["obje"] != null)
        {
            ViewState["Mevalution"] = Request.QueryString["obje"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["Mevalution"]), 12);
            lblhead.Text = "Mission Evalution - ";
        }
        else if (Request.QueryString["ltgm"] != null)
        {
            ViewState["ltgm"] = Request.QueryString["ltgm"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["ltgm"]), 2);
            lblhead.Text = "LTG Master - ";

        }
        else if (Request.QueryString["ltgd"] != null)
        {
            ViewState["ltgd"] = Request.QueryString["ltgd"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["ltgd"]), 21);
            lblhead.Text = "LTG Instruction - ";
        }
        else if (Request.QueryString["ltge"] != null)
        {
            ViewState["ltge"] = Request.QueryString["ltge"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["ltge"]), 22);
            lblhead.Text = "LTG Evalution - ";
        }
        else if (Request.QueryString["stgm"] != null)
        {
            ViewState["stgm"] = Request.QueryString["stgm"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["stgm"]), 3);
            lblhead.Text = "STG Master - ";
        }
        else if (Request.QueryString["stgd"] != null)
        {
            ViewState["stgd"] = Request.QueryString["stgd"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["stgd"]), 31);
            lblhead.Text = "STG Instruction - ";
        }
        else if (Request.QueryString["stge"] != null)
        {
            ViewState["stge"] = Request.QueryString["stge"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["stge"]), 32);
            lblhead.Text = "STG Evalution - ";
        }

        else if (Request.QueryString["yem"] != null)
        {
            ViewState["yem"] = Request.QueryString["yem"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["yem"]), 4);
            lblhead.Text = "Year Master - ";
        }

        else if (Request.QueryString["annam"] != null)
        {
            ViewState["annam"] = Request.QueryString["annam"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["annam"]), 44);
            lblhead.Text = "Announcement Master - ";
        }

        else if (Request.QueryString["yed"] != null)
        {
            ViewState["yed"] = Request.QueryString["yed"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["yed"]), 41);
            lblhead.Text = "Year Instruction - ";
        }
        else if (Request.QueryString["yee"] != null)
        {
            ViewState["yee"] = Request.QueryString["yee"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["yee"]), 42);
            lblhead.Text = "Year Evalution - ";
        }
        else if (Request.QueryString["mom"] != null)
        {
            ViewState["mom"] = Request.QueryString["mom"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["mom"]), 5);
            lblhead.Text = "Month Master - ";
        }
        else if (Request.QueryString["mod"] != null)
        {
            ViewState["mod"] = Request.QueryString["mod"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["mod"]), 51);
            lblhead.Text = "Month Instruction-";
        }
        else if (Request.QueryString["moe"] != null)
        {

            ViewState["moe"] = Request.QueryString["moe"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["moe"]), 52);
            lblhead.Text = "Month Evalution - ";
        }

        else if (Request.QueryString["wem"] != null)
        {
            ViewState["wem"] = Request.QueryString["wem"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["wem"]), 6);
            lblhead.Text = "Week Master - ";
        }
        else if (Request.QueryString["wed"] != null)
        {
            ViewState["wed"] = Request.QueryString["wed"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["wed"]), 61);
            lblhead.Text = "Week Instruction - ";
        }
        else if (Request.QueryString["wee"] != null)
        {
            ViewState["wee"] = Request.QueryString["wee"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["wee"]), 62);
            lblhead.Text = "Week Evalution - ";
        }


        else if (Request.QueryString["stram"] != null)
        {
            ViewState["stram"] = Request.QueryString["stram"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["stram"]), 7);
            lblhead.Text = "Stratagy Master - ";
        }
        else if (Request.QueryString["strad"] != null)
        {
            ViewState["strad"] = Request.QueryString["strad"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["strad"]), 71);
            lblhead.Text = "Stratagy Instruction - ";
        }
        else if (Request.QueryString["strae"] != null)
        {
            ViewState["strae"] = Request.QueryString["strae"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["strae"]), 72);
            lblhead.Text = "Stratagy Evalution - ";
        }


        else if (Request.QueryString["tectm"] != null)
        {
            ViewState["tectm"] = Request.QueryString["tectm"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["tectm"]), 8);
            lblhead.Text = "tactic Master - ";
        }
        else if (Request.QueryString["tectd"] != null)
        {
            ViewState["tectd"] = Request.QueryString["tectd"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["tectd"]), 81);
            lblhead.Text = "tactic Instruction - ";
        }
        else if (Request.QueryString["tecte"] != null)
        {
            ViewState["tecte"] = Request.QueryString["tecte"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["tecte"]), 82);
            lblhead.Text = "tactic Evalution - ";
        }

        else if (Request.QueryString["takid"] != null)
        {
            ViewState["takid"] = Request.QueryString["takid"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["takid"]), 9);

            lblhead.Text = "Task Master - ";

            //tempupload.Visible = true;
            //btnuplo.Visible = false;
        }
        else if (Request.QueryString["proid"] != null)
        {
            ViewState["proid"] = Request.QueryString["proid"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["proid"]), 10);
            lblhead.Text = "Project Master - ";
        }
        else if (Request.QueryString["Proevaid"] != null)
        {
            ViewState["Proevaid"] = Request.QueryString["Proevaid"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["Proevaid"]), 116);
            lblhead.Text = "Project Evaluation - ";
        }
        else if (Request.QueryString["commid"] != null)
        {
            ViewState["commid"] = Request.QueryString["commid"];
            dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["commid"]), 111);
            lblhead.Text = "Communication Master - ";
        }
        else if (Request.QueryString["InvCountId"] != null && Request.QueryString["InvCountNo"] != null)
        {
            ViewState["InvCountId"] = Request.QueryString["InvCountId"];
            //  dsc = MainAcocount.SelctDocHeadType(Convert.ToString(ViewState["InvCountId"]), 112);
            lblhead.Text = "Inventory Count No. " + Request.QueryString["InvCountNo"].ToString();
        }
        else if (Request.QueryString["CandidateId"] != null)
        {
            ViewState["CandidateId"] = Request.QueryString["CandidateId"];

            lblhead.Text = "Candidate Resume";
        }

        if (dsc.Rows.Count > 0)
        {
            lblhead.Text += Convert.ToString(dsc.Rows[0]["titledate"]);
            txtdoctitle.Text = Convert.ToString(dsc.Rows[0]["tname"]) + "_" + Convert.ToString(dsc.Rows[0]["titledate"]);

        }
    }
    protected void fillpop()
    {

        grd.DataSource = null;
        grd.DataBind();
        DataTable ds58 = new DataTable();
        if (Convert.ToInt32(ViewState["MissionId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["MissionId"]), 1);
        }
        else if (Convert.ToInt32(ViewState["employeemmid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["employeemmid"]), 500);
        }
        else if (Convert.ToInt32(ViewState["Mdetail"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["Mdetail"]), 11);
        }
        else if (Convert.ToInt32(ViewState["Mevalution"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["Mevalution"]), 12);
        }
        else if (Convert.ToInt32(ViewState["ltgm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["ltgm"]), 2);
        }
        else if (Convert.ToInt32(ViewState["ltgd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["ltgd"]), 21);
        }
        else if (Convert.ToInt32(ViewState["ltge"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["ltge"]), 22);
        }


        else if (Convert.ToInt32(ViewState["stgm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["stgm"]), 3);
        }
        else if (Convert.ToInt32(ViewState["stgd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["stgd"]), 31);
        }
        else if (Convert.ToInt32(ViewState["stge"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["stge"]), 32);
        }


        else if (Convert.ToInt32(ViewState["yem"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["yem"]), 4);
        }

        else if (Convert.ToInt32(ViewState["annam"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["annam"]), 44);
        }

        else if (Convert.ToInt32(ViewState["yed"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["yed"]), 41);
        }
        else if (Convert.ToInt32(ViewState["yee"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["yee"]), 42);
        }

        else if (Convert.ToInt32(ViewState["mom"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["mom"]), 5);
        }
        else if (Convert.ToInt32(ViewState["mod"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["mod"]), 51);
        }
        else if (Convert.ToInt32(ViewState["moe"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["moe"]), 52);
        }

        else if (Convert.ToInt32(ViewState["wem"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["wem"]), 6);
        }
        else if (Convert.ToInt32(ViewState["wed"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["wed"]), 61);
        }
        else if (Convert.ToInt32(ViewState["wee"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["wee"]), 62);
        }

        else if (Convert.ToInt32(ViewState["stram"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["stram"]), 7);
        }
        else if (Convert.ToInt32(ViewState["strad"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["strad"]), 71);
        }
        else if (Convert.ToInt32(ViewState["strae"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["strae"]), 72);
        }

        else if (Convert.ToInt32(ViewState["tectm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["tectm"]), 8);
        }
        else if (Convert.ToInt32(ViewState["tectd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["tectd"]), 81);
        }
        else if (Convert.ToInt32(ViewState["tecte"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["tecte"]), 82);
        }
        else if (Convert.ToInt32(ViewState["takid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["takid"]), 9);
        }
        else if (Convert.ToInt32(ViewState["proid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["proid"]), 10);
        }
        else if (Convert.ToInt32(ViewState["Proevaid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["Proevaid"]), 116);
        }
        else if (Convert.ToInt32(ViewState["commid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["commid"]), 111);
        }
        else if (Convert.ToInt32(ViewState["InvCountId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["InvCountId"]), 112);
        }
        else if (Convert.ToInt32(ViewState["CandidateId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["CandidateId"]), 113);
        }



        if (ds58.Rows.Count > 0)
        {

            grd.DataSource = ds58;
            grd.DataBind();
            // Label13.Text = "The list of document to be attach to the above mention entry.";
        }


        //ModalPopupExtender1.Show();
    }
    protected void btn1pop_Click(object sender, EventArgs e)
    {

        //if (rdpop.SelectedValue == "1")
        //{
        //    Response.Redirect("DocumentSearch.aspx?tid=" + ViewState["tid"].ToString() + "");

        //}
        //else if (rdpop.SelectedValue == "2")
        //{
        //    Response.Redirect("DocumentFastUpload.aspx?tid=" + ViewState["tid"].ToString() + "");
        //}

        if (rdpop.SelectedValue == "1")
        {
            Response.Redirect("DocumentSearch.aspx?tid=" + ViewState["tid"].ToString() + "");

        }
        else if (rdpop.SelectedValue == "2")
        {
            Response.Redirect("DocumentFastUpload.aspx?tid=" + ViewState["tid"].ToString() + "");
        }
    }
    protected void FillDocumentType()
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocTypeAll(Convert.ToString(ViewState["storeid"]));
        if (rdpop.SelectedValue == "1")
        {

            ddltypeofdoc.DataTextField = "doctype";
            ddltypeofdoc.DataValueField = "DocumentTypeId";
            ddltypeofdoc.DataSource = dt;
            ddltypeofdoc.DataBind();
            ddltypeofdoc.SelectedIndex = ddltypeofdoc.Items.IndexOf(ddltypeofdoc.Items.FindByText("GENERAL - General - GENERAL"));
        }
        else if (rdpop.SelectedValue == "2")
        {
            ddlindocfil.DataTextField = "doctype";
            ddlindocfil.DataValueField = "DocumentTypeId";
            ddlindocfil.DataSource = dt;
            ddlindocfil.DataBind();
            ddlindocfil.SelectedIndex = ddlindocfil.Items.IndexOf(ddlindocfil.Items.FindByText("MANAGMENT - MASTER DOCUMENTS - MISSION DOCUMENTS"));
        }
        //ddltypeofdoc.Items.Insert(0, "- All - ");
        //ddltypeofdoc.SelectedItem.Value = "0";
    }
    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delete1")
        {
            int Id = Convert.ToInt32(e.CommandArgument);

            string scpt = "Delete  from OfficeManagerDocuments where Id='" + Id + "'";
            SqlCommand cmd1 = new SqlCommand(scpt, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            int k = cmd1.ExecuteNonQuery();
            con.Close();
            if (k > 0)
            {
                lblmsg.Text = "Record deleted sucessfully  ";
            }
            else
            {
                lblmsg.Text = "Record not deleted sucessfully ";
            }
            fillpop();


        }
        else if (e.CommandName == "Send")
        {
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDoucmentMasterByID(Convert.ToInt32(e.CommandArgument));
            string docname = dt.Rows[0]["DocumentName"].ToString();
            string filepath = Server.MapPath("Account//" + Session["comid"] + "//UploadedDocuments//" + docname);


            string name = docname.Trim();
            string extension = name.Substring(name.Length - 3);
            if (Convert.ToString(extension) == "pdf")
            {
                Session["ABCDE"] = "ABCDE";

                string te = "ViewDocument.aspx?id=" + e.CommandArgument + "&Siddd=VHDS";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

            }
            else
            {
                FileInfo file = new FileInfo(filepath);

                if (file.Exists)
                {
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = file.Extension.ToLower();
                    Response.TransmitFile(file.FullName);

                    Response.End();

                }
            }
        }
    }
    protected void rdpop_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (rdpop.SelectedValue == "1")
        {
            DateTime fristdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            txtfrom.Text = fristdayofmonth.ToShortDateString();
            DateTime lastdaymonth = fristdayofmonth.AddMonths(1).AddDays(-1);
            txtto.Text = lastdaymonth.ToShortDateString();
            FillDocumentType();
            pnlfileup.Visible = true;
            pnlinserdoc.Visible = false;

        }
        else if (rdpop.SelectedValue == "2")
        {
            FillDocumentType();
            pnlinserdoc.Visible = true;
            pnlfileup.Visible = false;
            TxtDocDate.Text = DateTime.Now.ToShortDateString();
            //Response.Redirect("DocumentFastUpload.aspx?tid=" + ViewState["tid"].ToString() + "");
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
  
   

    protected void imgbtnAdd_Click(object sender, EventArgs e)
    {
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
            FillGrid();
            if (GridView1.Rows.Count > 0)
            {
                btnuplo.Visible = true;
                imgbtnreset.Visible = true;
            }
            else
            {
                btnuplo.Visible = false;
                imgbtnreset.Visible = false;
            }
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

        dt.Columns.Add(Dcom8);
        dt.Columns.Add(Dcom9);
        dt.Columns.Add(Dcom10);
        dt.Columns.Add(Dcom4a);
        dt.Columns.Add(Dcom5a);
        dt.Columns.Add(Dcom6a);
        return dt;
    }
    protected void FillGrid()
    {
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["data"]) == "")
        {
            dt = CreateDatatable();
        }
        else
        {
            dt = (DataTable)ViewState["data"];
        }
        if (FileUpload1.HasFile == true)
        {

            DataRow Drow = dt.NewRow();
            Drow["documentname"] = FileUpload1.FileName;
            Drow["documenttype"] = ddlindocfil.SelectedValue;
            Drow["status"] = "Not Uploaded";

            Drow["Businessname"] = ViewState["Wname"].ToString();
            Drow["DocType"] = ddlindocfil.SelectedItem.Text;
            Drow["DocumentTitle"] = txtdoctitle.Text;

            Drow["PartyId"] = ddlpartyname.SelectedValue;

            Drow["docdate"] = Convert.ToDateTime(TxtDocDate.Text).ToShortDateString();

            Drow["docrefno"] = txtdocrefnmbr.Text;
            Drow["docamt"] = txtnetamount.Text;
            Drow["Docty"] = ddldt.SelectedItem.Text;
            Drow["DoctyId"] = ddldt.SelectedValue;
            Drow["PRN"] = txtpartdocrefno.Text;
            dt.Rows.Add(Drow);
        }
        ViewState["data"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();

        if (FileUpload1.HasFile == true)
        {
            if (Directory.Exists(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc")) == false)
            {
                Directory.CreateDirectory(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc"));
            }
            string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\") + filename);
        }
    }

    protected void btnuplo_Click(object sender, EventArgs e)
    {
        bool access = UserAccess.Usercon("DocumentMaster", "", "DocumentId", "", "", "CID", "DocumentMaster");
        if (access == true)
        {
            UploadDocumets();
            ViewState["data"] = null;
            GridView1.DataSource = null;
            GridView1.DataBind();
            btnuplo.Visible = false;
            imgbtnreset.Visible = false;
            fillpop();
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, You are not permitted for greater record to Priceplan";
        }
    }
    protected void UploadDocumets()
    {
        int i1 = 0;
        try
        {
            //foreach (GridViewRow gdr in Gridreqinfo.Rows)
            if (GridView1.Rows.Count > 0)
            {

                do
                {


                    string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + GridView1.Rows[i1].Cells[4].Text.Replace(" ", "_");
                    //string path1 = Server.MapPath("..\\Account\\TempDoc\\" + gdr.Cells[1].Text);
                    string path1 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\" + GridView1.Rows[i1].Cells[4].Text);
                    string path2 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + filename.ToString());


                    if (System.IO.File.Exists(path2))
                    {
                    }
                    else
                    {
                        File.Copy(path1, path2);
                    }
                    string filexten = Path.GetExtension(path2);

                    Label lbldocdate = (Label)GridView1.Rows[i1].Cells[6].FindControl("lbldocdate");

                    Label lbldocrefno = (Label)GridView1.Rows[i1].Cells[7].FindControl("lbldocrefno");
                    Label lbldocamt = (Label)GridView1.Rows[i1].Cells[8].FindControl("lbldocamt");
                    Label lblpid = (Label)GridView1.Rows[i1].Cells[1].FindControl("lblpid");
                    Label lbldoctid = (Label)GridView1.Rows[i1].FindControl("lbldoctid");
                    Label lblprn = (Label)GridView1.Rows[i1].FindControl("lblprn");
                
                    if (lbldocamt.Text == "")
                    {
                        lbldocamt.Text = "0";
                    }

                    Int32 rst = clsDocument.InsertDocumentMaster(Convert.ToInt32(GridView1.DataKeys[i1].Value), 2, Convert.ToDateTime(System.DateTime.Now.ToString()), filename.ToString(), GridView1.Rows[i1].Cells[3].Text, "", Convert.ToInt32(lblpid.Text), lbldocrefno.Text, Convert.ToDecimal(lbldocamt.Text), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(lbldocdate.Text), filexten, lbldoctid.Text,lblprn.Text);

                    if (rst > 0)
                    {

                        bool dcaprv = true;
                        bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);


                        int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                       Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                        string str12 = "Insert into OfficeManagerDocuments(MissionId,MissioninstructionId,MissionevId,LtgId,Ltgdetail,Ltgevalution,StgId,Stgdetail,Stgevalution,YgId,annid,Ydetail,Yeevalution,MgId,Mdetail,Mevalution,WgId,Wdetail,Wevalution,StrategyId,Strategydetail,Strategevaution,tectm,tectd,texte,taskid,projectid,projectevaid,DocumentId,StoreId,CommunicationId,InvCountId,EmployeeID,CandidateId)" +

                                     "values('" + ViewState["MissionId"] + "','" + ViewState["Mdetail"] + "','" + ViewState["Mevalution"] + "'," +
                                    "'" + ViewState["ltgm"] + "','" + ViewState["ltgd"] + "','" + ViewState["ltge"] + "'," +
                                     "'" + ViewState["stgm"] + "','" + ViewState["stgd"] + "','" + ViewState["stge"] + "'," +
                                     "'" + ViewState["yem"] + "','" + ViewState["annam"] + "','" + ViewState["yed"] + "','" + ViewState["yee"] + "'," +
                                     "'" + ViewState["mom"] + "','" + ViewState["mod"] + "','" + ViewState["moe"] + "'," +
                                     "'" + ViewState["wem"] + "','" + ViewState["wed"] + "','" + ViewState["wee"] + "'," +

                                      "'" + ViewState["stram"] + "','" + ViewState["strad"] + "','" + ViewState["strae"] + "'," +
                                       "'" + ViewState["tectm"] + "','" + ViewState["tectd"] + "','" + ViewState["tecte"] + "'," +
                                        "'" + ViewState["takid"] + "','" + ViewState["proid"] + "','" + ViewState["Proevaid"] + "','" + rst + "','" + ViewState["storeid"] + "','" + ViewState["commid"] + "','" + ViewState["InvCountId"] + "','" + ViewState["employeemmid"] + "','" + ViewState["CandidateId"] + "')";

                        SqlCommand cmd12 = new SqlCommand(str12, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd12.ExecuteNonQuery();
                        con.Close();

                    }
                    //string Location = Server.MapPath(@"~/Account/TempDoc/");
                    string Location = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocuments/");
                    string Location2 = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocumentsTemp/");
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
                    if (filexten == ".pdf")
                    {
                        foreach (System.IO.FileInfo f in dir.GetFiles(filename))
                        {

                            string Location1 = Server.MapPath(@"~/Account/" + Session["comid"] + "/DocumentImage/");
                            ;

                            //if (System.IO.File.GetAttributes(Location + f.Name).ToString() != System.IO.FileAttributes.Hidden.ToString())
                            //{

                            string filepath = Server.MapPath("~//Account//pdftoimage.exe");
                            System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);
                            //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
                            //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");
                            
                            //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                            pti.Arguments = filepath + " -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                            filepath += " " + "-r" + " " + "AGPVCWBRYBCDEPFD";

                            pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");
                            
                            pti.UseShellExecute = false;
                            pti.RedirectStandardOutput = true;
                            pti.RedirectStandardInput = true;
                            pti.RedirectStandardError = true;
                            //pti.WorkingDirectory = "D:\\Capman.ifilecabinet.com1\\Account\\";
                           // pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");
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

                    lblmsg.Visible = true;
                    lblmsg.Text = "Message : All PDFs Are Converted Successfully!!!";



                    i1 = i1 + 1;

                } while (i1 <= GridView1.Rows.Count - 1);


            }

            //=========================== update status in view state


            ViewState["data"] = null;


            lblmsg.Text = "Documents uploaded successfully.";
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();

        }
    }



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["data"];
            dt.Rows[Convert.ToInt32(GridView1.SelectedIndex.ToString())].Delete();
            dt.AcceptChanges();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            ViewState["data"] = dt;
            if (GridView1.Rows.Count == 0)
            {
                btnuplo.Visible = false;
                imgbtnreset.Visible = false;
            }
            lblmsg.Text = "Record deleted successfully.";
        }
    }
    protected void ImageButton49_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentSubSubType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton48_Click(object sender, ImageClickEventArgs e)
    {
        FillDocumentType();
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
    protected void imgbtnreset_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void clear()
    {



        GridView1.DataSource = null;
        GridView1.DataBind();

        ViewState["data"] = null;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        UploadDocumetsTempforTask();
        ViewState["data"] = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
        btnuplo.Visible = false;
        imgbtnreset.Visible = false;
        //fillpop();

    }
    protected void UploadDocumetsTempforTask()
    {
        int i1 = 0;
        try
        {
            //foreach (GridViewRow gdr in Gridreqinfo.Rows)
            if (GridView1.Rows.Count > 0)
            {
                do
                {


                    string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + GridView1.Rows[i1].Cells[4].Text.Replace(" ", "_");
                    //string path1 = Server.MapPath("..\\Account\\TempDoc\\" + gdr.Cells[1].Text);
                    string path1 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\" + GridView1.Rows[i1].Cells[4].Text);
                    string path2 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + filename.ToString());


                    if (System.IO.File.Exists(path2))
                    {
                    }
                    else
                    {
                        File.Copy(path1, path2);
                    }
                    string filexten = Path.GetExtension(path2);

                    Label lbldocdate = (Label)GridView1.Rows[i1].Cells[6].FindControl("lbldocdate");

                    Label lbldocrefno = (Label)GridView1.Rows[i1].Cells[7].FindControl("lbldocrefno");
                    Label lbldocamt = (Label)GridView1.Rows[i1].Cells[8].FindControl("lbldocamt");
                    Label lblpid = (Label)GridView1.Rows[i1].Cells[1].FindControl("lblpid");

                    Int32 rst = clsDocument.InsertDocumentMasterForTempTask(Convert.ToInt32(GridView1.DataKeys[i1].Value), 2, Convert.ToDateTime(System.DateTime.Now.ToString()), filename.ToString(), GridView1.Rows[i1].Cells[3].Text, "", Convert.ToInt32(lblpid.Text), lbldocrefno.Text, Convert.ToDecimal(lbldocamt.Text), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(lbldocdate.Text), filexten, Convert.ToInt32(ViewState["takid"].ToString()));

                    if (rst > 0)
                    {

                        // bool dcaprv = true;
                        // bool indc = clsDocument.insertDocumentProcessingnew(Convert.ToInt32(Session["EmployeeId"]), rst, dcaprv);


                        //  int rsts = clsDocument.InsertDocumentLog(rst, Convert.ToInt32(Session["EmployeeId"]),
                        // Convert.ToDateTime(System.DateTime.Now), false, false, true, false, false, false, false, false);


                        //string str12 = "Insert into OfficeManagerDocuments(MissionId,MissioninstructionId,MissionevId,LtgId,Ltgdetail,Ltgevalution,StgId,Stgdetail,Stgevalution,YgId,Ydetail,Yeevalution,MgId,Mdetail,Mevalution,WgId,Wdetail,Wevalution,StrategyId,Strategydetail,Strategevaution,tectm,tectd,texte,taskid,projectid,DocumentId,StoreId,CommunicationId)" +

                        //             "values('" + ViewState["MissionId"] + "','" + ViewState["Mdetail"] + "','" + ViewState["Mevalution"] + "'," +
                        //            "'" + ViewState["ltgm"] + "','" + ViewState["ltgd"] + "','" + ViewState["ltge"] + "'," +
                        //             "'" + ViewState["stgm"] + "','" + ViewState["stgd"] + "','" + ViewState["stge"] + "'," +
                        //             "'" + ViewState["yem"] + "','" + ViewState["yed"] + "','" + ViewState["yee"] + "'," +
                        //             "'" + ViewState["mom"] + "','" + ViewState["mod"] + "','" + ViewState["moe"] + "'," +
                        //             "'" + ViewState["wem"] + "','" + ViewState["wed"] + "','" + ViewState["wee"] + "'," +

                        //              "'" + ViewState["stram"] + "','" + ViewState["strad"] + "','" + ViewState["strae"] + "'," +
                        //               "'" + ViewState["tectm"] + "','" + ViewState["tectd"] + "','" + ViewState["tecte"] + "'," +
                        //                "'" + ViewState["takid"] + "','" + ViewState["Proid"] + "','" + rst + "','" + ViewState["storeid"] + "','" + ViewState["commid"] + "')";

                        //SqlCommand cmd12 = new SqlCommand(str12, con);
                        //if (con.State.ToString() != "Open")
                        //{
                        //    con.Open();
                        //}
                        //cmd12.ExecuteNonQuery();
                        //con.Close();

                    }
                    //string Location = Server.MapPath(@"~/Account/TempDoc/");
                    string Location = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocuments/");
                    string Location2 = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocumentsTemp/");
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
                    if (filexten == ".pdf")
                    {
                        foreach (System.IO.FileInfo f in dir.GetFiles(filename))
                        {

                            string Location1 = Server.MapPath(@"~/Account/" + Session["comid"] + "/DocumentImage/");
                            ;

                            //if (System.IO.File.GetAttributes(Location + f.Name).ToString() != System.IO.FileAttributes.Hidden.ToString())
                            //{

                            string filepath = Server.MapPath("~//Account//pdftoimage.exe");
                            System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);
                            //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
                            //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");
                            pti.UseShellExecute = false;
                            //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                            pti.Arguments = filepath + " -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";

                            pti.RedirectStandardOutput = true;
                            pti.RedirectStandardInput = true;
                            pti.RedirectStandardError = true;
                            //pti.WorkingDirectory = "D:\\Capman.ifilecabinet.com1\\Account\\";
                            pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//");
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


                        //int ii = 0;
                        //string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + filename);
                        //using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                        //{
                        //    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                        //    MatchCollection match = regex.Matches(st.ReadToEnd());
                        //    ii = match.Count;
                        //}

                        //int length = filename.Length;
                        //string docnameIn = filename.Substring(0, length - 4);


                        //for (int kk = 1; kk <= ii; kk++)
                        //{
                        //    string scpf = docnameIn;
                        //    if (kk >= 1 && kk < 10)
                        //    {
                        //        scpf = scpf + "0000" + kk + ".jpg";
                        //    }
                        //    else if (kk >= 10 && kk < 100)
                        //    {
                        //        scpf = scpf + "000" + kk + ".jpg";
                        //    }
                        //    else if (kk >= 100)
                        //    {
                        //        scpf = scpf + "00" + kk + ".jpg";
                        //    }

                        //    clsEmployee.InserDocumentImageMaster(rst, scpf);

                        //}
                    }


                    lblmsg.Text = "Message : All PDFs Are Converted Successfully!!!";
                    lblmsg.Visible = true;


                    i1 = i1 + 1;

                } while (i1 <= GridView1.Rows.Count - 1);

            }

            //=========================== update status in view state


            ViewState["data"] = null;


            lblmsg.Text = "Documents uploaded successfully.";
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();

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
    protected void flaganddoc()
    {

        DataTable dts1 = select("select Id,name from DocumentTypenm where  active='1' Order by name");
        ddldt.DataSource = dts1;
        ddldt.DataTextField = "name";
        ddldt.DataValueField = "Id";
        ddldt.DataBind();
        ddldt.SelectedIndex = ddldt.Items.IndexOf(ddldt.Items.FindByText("Document"));
        //ddldt.Items.Insert(0, "Select");
        //ddldt.Items[0].Value = "0";
    }


    //----------------------------------

    protected void btnsearchgo_Click(object sender, EventArgs e)
    {

        string monter = "";


        if (RadioButtonList1.SelectedIndex == 0)
        {
            if ((txtfrom.Text.Length > 0) && (txtto.Text.Length > 0))
            {
                monter = " and (DocumentMaster.DocumentDate between '" + Convert.ToDateTime(txtfrom.Text).AddDays(-1) + "' and '" + Convert.ToDateTime(txtto.Text).AddDays(1) + "')";

            }
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            if ((txtfrom.Text.Length > 0) && (txtto.Text.Length > 0))
            {
                monter = " and (DocumentMaster.DocumentUploadDate between '" + Convert.ToDateTime(txtfrom.Text).AddDays(-1) + "' and '" + Convert.ToDateTime(txtto.Text).AddDays(1) + "')";
            }

        }

        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentAccessRigthsByDesignationIDGene(Convert.ToString(ViewState["storeid"]));
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        int flag = 1;
        foreach (DataRow dr in dt.Rows)
        {

            dt1 = select("SELECT  distinct   DocumentMaster.DocumentId, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentName, DocumentMaster.DocumentUploadDate,DocumentMaster.DocumentDate, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId, DocumentMaster.DocumentRefNo, DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId,DocumentMaster.FileExtensionType, DocumentType.DocumentType,Party_master.Compname as PartyName FROM  DocumentMainType inner join  DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId INNER JOIN DocumentMaster  ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId LEFT OUTER JOIN DocumentProcessing ON DocumentMaster.DocumentId = DocumentProcessing.DocumentId LEFT OUTER JOIN Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId WHERE   (DocumentMainType.Whid='" + Convert.ToString(ViewState["storeid"]) + "') and (DocumentMaster.DocumentTypeId = '" + Convert.ToInt32(ddltypeofdoc.SelectedValue) + "') and DocumentMaster.DocumentId in ( SELECT     DocumentId FROM         DocumentProcessing WHERE     (Approve = 1) )  AND DocumentMaster.DocumentId not in ( SELECT DocumentId FROM         DocumentProcessing WHERE     (Approve = 0)OR (Approve  is null)) and DocumentMaster.CID='" + Session["Comid"] + "'  and (DocumentMaster.DocumentTypeId = '" + Convert.ToInt32(ddltypeofdoc.SelectedValue) + "')" + monter);

        }
        if (flag == 1)
        {
            dt2 = dt1.Clone();
            flag = 0;
        }
        foreach (DataRow r in dt1.Rows)
        {
            dt2.ImportRow(r);
        }


        DataView myDataView = new DataView();
        myDataView = dt2.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        Gridreqinfo.DataSource = dt2;
        Gridreqinfo.DataBind();
        if (Gridreqinfo.Rows.Count > 0)
        {
            btnatt.Visible = true;
        }
        else
        {
            btnatt.Visible = true;
        }
    }
    protected void Gridreqinfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gridreqinfo.PageIndex = e.NewPageIndex;
        btnsearchgo_Click(sender, e);
    }
    protected void Gridreqinfo_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        btnsearchgo_Click(sender, e);
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
    protected void Gridreqinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnatt_Click(object sender, EventArgs e)
    {
        inserdocatt();
    }
    protected DataTable condc(int docid)
    {
        DataTable ds58 = new DataTable();
        if (Convert.ToInt32(ViewState["MissionId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["MissionId"]), 1, docid);
        }
        else if (Convert.ToInt32(ViewState["employeemmid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["employeemmid"]), 1, docid);
        }
        else if (Convert.ToInt32(ViewState["Mdetail"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["Mdetail"]), 11, docid);
        }
        else if (Convert.ToInt32(ViewState["Mevalution"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["Mevalution"]), 12, docid);
        }
        else if (Convert.ToInt32(ViewState["ltgm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["ltgm"]), 2, docid);
        }
        else if (Convert.ToInt32(ViewState["ltgd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["ltgd"]), 21, docid);
        }
        else if (Convert.ToInt32(ViewState["ltge"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["ltge"]), 22, docid);
        }


        else if (Convert.ToInt32(ViewState["stgm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["stgm"]), 3, docid);
        }
        else if (Convert.ToInt32(ViewState["stgd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["stgd"]), 31, docid);
        }
        else if (Convert.ToInt32(ViewState["stge"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["stge"]), 32, docid);
        }


        else if (Convert.ToInt32(ViewState["yem"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["yem"]), 4, docid);
        }
        else if (Convert.ToInt32(ViewState["annam"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["annam"]), 44, docid);
        }
        else if (Convert.ToInt32(ViewState["yed"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["yed"]), 41, docid);
        }
        else if (Convert.ToInt32(ViewState["yee"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["yee"]), 42, docid);
        }

        else if (Convert.ToInt32(ViewState["mom"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["mom"]), 5, docid);
        }
        else if (Convert.ToInt32(ViewState["mod"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["mod"]), 51, docid);
        }
        else if (Convert.ToInt32(ViewState["moe"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["moe"]), 52, docid);
        }

        else if (Convert.ToInt32(ViewState["wem"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["wem"]), 6, docid);
        }
        else if (Convert.ToInt32(ViewState["wed"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["wed"]), 61, docid);
        }
        else if (Convert.ToInt32(ViewState["wee"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["wee"]), 62, docid);
        }

        else if (Convert.ToInt32(ViewState["stram"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["stram"]), 7, docid);
        }
        else if (Convert.ToInt32(ViewState["strad"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["strad"]), 71, docid);
        }
        else if (Convert.ToInt32(ViewState["strae"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["strae"]), 72, docid);
        }

        else if (Convert.ToInt32(ViewState["tectm"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["tectm"]), 8, docid);
        }
        else if (Convert.ToInt32(ViewState["tectd"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["tectd"]), 81, docid);
        }
        else if (Convert.ToInt32(ViewState["tecte"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["tecte"]), 82, docid);
        }
        else if (Convert.ToInt32(ViewState["takid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["takid"]), 9, docid);
        }
        else if (Convert.ToInt32(ViewState["Proid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["Proid"]), 10, docid);
        }
        else if (Convert.ToInt32(ViewState["Proevaid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["Proevaid"]), 116, docid);
        }
        else if (Convert.ToInt32(ViewState["Proid"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["commid"]), 111, docid);
        }
        else if (Convert.ToInt32(ViewState["InvCountId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["InvCountId"]), 112, docid);
        }
        else if (Convert.ToInt32(ViewState["CandidateId"]) > 0)
        {
            ds58 = MainAcocount.SelectOfficedocidwithdocid(Convert.ToString(ViewState["CandidateId"]), 113, docid);
        }

        return ds58;
    }
    public void inserdocatt()
    {


        int k = 0;

        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            CheckBox chk1 = (CheckBox)gdr.FindControl("chkaccentry");

            LinkButton LinkButton1 = (LinkButton)gdr.FindControl("LinkButton1");
            int docid = Convert.ToInt32(LinkButton1.Text);


            if (chk1.Checked == true)
            {
                DataTable dcr = condc(docid);
                if (dcr.Rows.Count <= 0)
                {
                    k = k + 1;
                    string str12 = "Insert into OfficeManagerDocuments(MissionId,MissioninstructionId,MissionevId,LtgId,Ltgdetail,Ltgevalution,StgId,Stgdetail,Stgevalution,YgId,annid,Ydetail,Yeevalution,MgId,Mdetail,Mevalution,WgId,Wdetail,Wevalution,StrategyId,Strategydetail,Strategevaution,tectm,tectd,texte,taskid,projectid,projectevaid,DocumentId,StoreId,CommunicationId,InvCountId,EmployeeID,CandidateId)" +

                                     "values('" + ViewState["MissionId"] + "','" + ViewState["Mdetail"] + "','" + ViewState["Mevalution"] + "'," +
                                    "'" + ViewState["ltgm"] + "','" + ViewState["ltgd"] + "','" + ViewState["ltge"] + "'," +
                                     "'" + ViewState["stgm"] + "','" + ViewState["stgd"] + "','" + ViewState["stge"] + "'," +
                                     "'" + ViewState["yem"] + "','" + ViewState["annam"] + "','" + ViewState["yed"] + "','" + ViewState["yee"] + "'," +
                                     "'" + ViewState["mom"] + "','" + ViewState["mod"] + "','" + ViewState["moe"] + "'," +
                                     "'" + ViewState["wem"] + "','" + ViewState["wed"] + "','" + ViewState["wee"] + "'," +

                                     "'" + ViewState["stram"] + "','" + ViewState["strad"] + "','" + ViewState["strae"] + "'," +
                                     "'" + ViewState["tectm"] + "','" + ViewState["tectd"] + "','" + ViewState["tecte"] + "'," +
                                     "'" + ViewState["takid"] + "','" + ViewState["Proid"] + "','" + ViewState["Proevaid"] + "','" + LinkButton1.Text + "','" + ViewState["storeid"] + "','" + ViewState["commid"] + "','" + ViewState["InvCountId"] + "','" + ViewState["employeemmid"] + "','" + ViewState["CandidateId"] + "')";

                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();
                }

            }
        }
        if (k > 0)
        {
            lblmsg.Text = "Record inserted successfully";
            lblmsg.Visible = true;
            fillpop();
        }
        else
        {
            lblmsg.Text = "Record already exist";
            lblmsg.Visible = true;
        }


    }
    
}
