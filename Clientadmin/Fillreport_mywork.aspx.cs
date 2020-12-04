using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

using System.Net;
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;

using System.Net.Security;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.DirectoryServices;
using System.Data;
using System.Configuration;

public partial class Fillreport_mywork : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        lblVersion.Text = "This is V1 Updated on 14Dec2015 by @Pk";
        if (!IsPostBack)
        {
            if (Request.QueryString["vid"] != null || Request.QueryString["vid"] != "0")
            {
                Session["vid"] = Request.QueryString["vid"];
                Session["empname"] = Request.QueryString["eid"];
                Session["txt1"] = Request.QueryString["frmdt"];
                Session["txt2"] = Request.QueryString["todt"];
                Session["lblworktitle"] = Request.QueryString["lblworktitle"];
                Session["lblbujtedhr"] = Request.QueryString["lblbujtedhr"];
                Session["Pageid"] = Request.QueryString["PageId"];
                filldata();
            }
            Button10.Visible = false;
        }

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            lblactulhr.Visible = true;
            txtactualhourrequired.Visible = true;
            CheckBox1.Checked = false;
            CheckBox1_CheckedChanged(CheckBox1, e);
            Button4.Visible = true;
            trtbl.Visible = false;
            trbdoc.Visible = false;
            tbllist.Visible = false;
            Tr1.Visible = false;
        }
        else
        {
            Tr1.Visible = true;
            tbllist.Visible = true;
            trtbl.Visible = true;
            trbdoc.Visible = true;
            CheckBox1.Checked = true;
            CheckBox1_CheckedChanged(CheckBox1, e);
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        string usertype = Convert.ToString(ViewState["UserType"]);

        if (usertype == "Tester" || usertype == "Supervisor")
        {
            lblupldcode.Visible = false;
            lbluplddoc.Visible = false;
            lblcerti.Visible = CheckBox1.Checked;
            Chkcertify.Visible = CheckBox1.Checked;
            Chkcertify.Checked = true;
            //fileuploadadattachment.Visible = true;
            // Button7.Visible = true;
            lblfnm.Visible = false;
            chkupld.Visible = false;
            Chkmultiupld.Visible = false;
            //  Fileupldmulti.Visible = true;
            // Button8.Visible = true;
            lbldocname.Visible = false;
            chkupld.Checked = false;
            chkupld_CheckedChanged(chkupld, e);
            Chkmultiupld.Checked = false;
            Chkmultiupld_CheckedChanged(Chkmultiupld, e);

            trtestscren.Visible = true;
            trtestscren2.Visible = true;
            Fillscreen();

            btn_Return.Visible = true; 
            
            Chkcertify_CheckedChanged1(sender, e);
            lblcopycode.Visible = true;
            lblcopydoc.Visible = true;
        }
        else
        {
            //tbllist. = true;
            lbltblname.Visible = CheckBox1.Checked;
            lblcopydoc.Visible = CheckBox1.Checked;
            lblcopycode.Visible = CheckBox1.Checked;
            lblupldcode.Visible = CheckBox1.Checked;
            lbluplddoc.Visible = CheckBox1.Checked;
            lblcerti.Visible = CheckBox1.Checked;
            Chkcertify.Visible = CheckBox1.Checked;
            //fileuploadadattachment.Visible = true;
            // Button7.Visible = true;
            lblfnm.Visible = CheckBox1.Checked;
            chkupld.Visible = CheckBox1.Checked;
            Chkmultiupld.Visible = CheckBox1.Checked;
            //  Fileupldmulti.Visible = true;
            // Button8.Visible = true;
            lbldocname.Visible = CheckBox1.Checked;
            chkupld.Checked = true;
            chkupld.Visible = false; 
            chkupld_CheckedChanged(chkupld, e);
            Chkmultiupld.Checked = true;
            Chkmultiupld.Visible = false;
            Chkmultiupld_CheckedChanged(Chkmultiupld, e);
            filltablename();
            Chkcertify.Checked = true;

            Chkcertify_CheckedChanged1(sender, e);
        }
        if (usertype == "Supervisor")
        {
            trtestscren.Visible = false;
        }

        //ModalPopupExtender2.Show();
    }
    protected void Fillscreen()
    {
        GridView1.DataSource = null;
       // Session["GridFileDocu1"] = null;
        string strSQL = "SELECT ScreensortTitle as ScreensortTitle ,ScreensortTitle as DocumentTitle FROM VersionScreensort_Master WHERE PageWorkTblID = " + lblpageworkmasterId.Text;
        con.Open();
        DataTable dtFiles = new DataTable();
        SqlCommand cmd = new SqlCommand(strSQL, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtFiles);
        GridView1.DataSource = dtFiles;
        GridView1.DataBind();
        GridView1.Visible = true;


        string strSQL1 = "SELECT DocumentTitle as ScreensortTitle ,DocumentTitle as DocumentTitle FROM VersionDocument_Master WHERE PageWorkTblID = " + lblpageworkmasterId.Text;
        con.Close(); 
        con.Open();
        DataTable dtFiles1 = new DataTable();
        SqlCommand cmd1 = new SqlCommand(strSQL1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.Fill(dtFiles1);
        Griddoc.DataSource = dtFiles1;
        Griddoc.DataBind();
        Griddoc.Visible = true;
        
    }
   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "LinkpageVersion")
        {
            String mm = "Attach/" + Convert.ToString(e.CommandArgument);
           
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + mm + "');", true);
        }
        if (e.CommandName == "LinkpageVersion1")
        {
            String mm = "Attach/" + Convert.ToString(e.CommandArgument);

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + mm + "');", true);
        }
    }
    protected void chkupld_CheckedChanged(object sender, EventArgs e)
    {
        fileuploadadattachment.Visible = chkupld.Checked;
        Button7.Visible = chkupld.Checked;
        lblfnm.Visible = chkupld.Checked;
        gridFileAttach.Visible = chkupld.Checked;
        //ModalPopupExtender2.Show();
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "mp3", "wma" };

        string ext = System.IO.Path.GetExtension(fileuploadadattachment.PostedFile.FileName);

        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + validFileTypes[i])
            {

                isValidFile = true;

                break;

            }

        }

        if (!isValidFile)
        {

            Label2.Visible = true;

            Label2.Text = "Invalid File. Please upload a File with extension " +

                           string.Join(",", validFileTypes);
            // ModalPopupExtender2.Show();
        }

        else
        {

            
            String filename = "";

            if (fileuploadadattachment.HasFile)
            {
                filename = fileuploadadattachment.FileName;

                string strSQL = "SELECT FileName FROM PageDevelopmentSourceCodeAllocateTable WHERE PageWorkTblID = " + lblpageworkmasterId.Text;
                con.Open();
                DataTable dtFiles = new DataTable();
                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtFiles);
                con.Close();

                //if (filename.EndsWith(".aspx"))
                //{
                //    string NewFileName = Convert.ToString(dtFiles.Rows[0]["FileName"]);
                //    if (filename.ToString() == NewFileName.ToString())
                //    {
                //        Label2.Visible = false;
                //    }
                //    else
                //    {
                //        Label2.Visible = true;
                //        Label2.Text = "Filename should be the same file as you have downloaded.";
                //        ModalPopupExtender2.Show();
                //        return;
                //    }
                //}

                //if (filename.EndsWith(".aspx.cs"))
                //{
                //    string NewFIleName = Convert.ToString(dtFiles.Rows[1]["FileName"]);
                //    if (filename.ToString() == NewFIleName.ToString())
                //    {
                //        Label2.Visible = false;
                //    }
                //    else
                //    {
                //        Label2.Visible = true;
                //        Label2.Text = "Filename should be the same file as you have downloaded.";
                //        ModalPopupExtender2.Show();
                //        return;
                //    }
                //}

                //filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fileuploadadattachment.FileName;
                fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Attachment\\") + filename);
                DataTable dt = new DataTable();
                if (Session["GridFileAttach1"] == null)
                {
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "PDFURL";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);
                }
                else
                {
                    dt = (DataTable)Session["GridFileAttach1"];
                }
                DataRow dtrow = dt.NewRow();
                dtrow["PDFURL"] = filename;
                dt.Rows.Add(dtrow);
                Session["GridFileAttach1"] = dt;
                gridFileAttach.DataSource = dt;
                gridFileAttach.DataBind();
                // ModalPopupExtender2.Show();
            }
            else
            {
                Label2.Visible = true;
                Label2.Text = "Please Attach File to Upload.";
                return;
            }
        }
    }
    protected void Chkmultiupld_CheckedChanged(object sender, EventArgs e)
    {
        Fileupldmulti.Visible = Chkmultiupld.Checked;
        Button8.Visible = Chkmultiupld.Checked;
        lbldocname.Visible = Chkmultiupld.Checked;
        Griddoc.Visible = Chkmultiupld.Checked;
        //ModalPopupExtender2.Show();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "mp3", "wma", "pdf" };

        string ext = System.IO.Path.GetExtension(Fileupldmulti.PostedFile.FileName);

        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + validFileTypes[i])
            {

                isValidFile = true;

                break;

            }

        }

        if (!isValidFile)
        {

            Label2.Visible = true;

            Label2.Text = "Invalid File. Please upload a File with extension " +

                           string.Join(",", validFileTypes);
            // ModalPopupExtender2.Show();
        }

        else
        {


            String filename = "";

            if (Fileupldmulti.HasFile)
            {
                filename = Fileupldmulti.FileName;

                //string strSQL = "SELECT FileName FROM PageDevelopmentSourceCodeAllocateTable WHERE PageWorkTblID = " + lblpageworkmasterId.Text;
                //con.Open();
                //DataTable dtFiles = new DataTable();
                //SqlCommand cmd = new SqlCommand(strSQL, con);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(dtFiles);
                //con.Close();

                //if (filename.EndsWith(".aspx"))
                //{
                //    string NewFileName = Convert.ToString(dtFiles.Rows[0]["FileName"]);
                //    if (filename.ToString() == NewFileName.ToString())
                //    {
                //        Label2.Visible = false;
                //    }
                //    else
                //    {
                //        Label2.Visible = true;
                //        Label2.Text = "Filename should be the same file as you have downloaded.";
                //        ModalPopupExtender2.Show();
                //        return;
                //    }
                //}

                //if (filename.EndsWith(".aspx.cs"))
                //{
                //    string NewFIleName = Convert.ToString(dtFiles.Rows[1]["FileName"]);
                //    if (filename.ToString() == NewFIleName.ToString())
                //    {
                //        Label2.Visible = false;
                //    }
                //    else
                //    {
                //        Label2.Visible = true;
                //        Label2.Text = "Filename should be the same file as you have downloaded.";
                //        ModalPopupExtender2.Show();
                //        return;
                //    }
                //}

                //filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fileuploadadattachment.FileName;
                Fileupldmulti.PostedFile.SaveAs(Server.MapPath("~\\Attachment\\") + filename);




                DataTable dt = new DataTable();
                if (Session["GridFileDocu"] == null)
                {
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "DocumentTitle";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);



                }
                else
                {
                    dt = (DataTable)Session["GridFileDocu"];
                }
                DataRow dtrow = dt.NewRow();
                dtrow["DocumentTitle"] = filename;



                dt.Rows.Add(dtrow);
                Session["GridFileDocu"] = dt;
                Griddoc.DataSource = dt;


                Griddoc.DataBind();
                // ModalPopupExtender2.Show();
            }
            else
            {
                Label2.Visible = true;
                Label2.Text = "Please Attach File to Upload.";
                return;
            }
        }
    }

    protected void Button8_Clicksre(object sender, EventArgs e)
    {
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg" };

        string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + validFileTypes[i])
            {

                isValidFile = true;

                break;

            }
            else
            {
            }

        }

        if (!isValidFile)
        {

            Label1.Visible = true;

            Label1.Text = "Invalid File. Please upload a File with extension " +

                           string.Join(",", validFileTypes);
            // ModalPopupExtender2.Show();
        }

        else
        {

            Label1.Text = "";
            String filename = "";

            if (FileUpload1.HasFile)
            {
                filename = lblworltitleatreport.Text + "_"+ FileUpload1.FileName;

                string path = Server.MapPath("~\\Clientadmin\\Attach\\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Clientadmin\\Attach\\") + filename);



                DataTable dt = new DataTable();
                if (Session["GridFileDocu1"] == null)
                {
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "DocumentTitle";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);



                }
                else
                {
                    dt = (DataTable)Session["GridFileDocu1"];
                }
                DataRow dtrow = dt.NewRow();
                dtrow["DocumentTitle"] = filename;



                dt.Rows.Add(dtrow);
                Session["GridFileDocu1"] = dt;
                GridView1.DataSource = dt;


                GridView1.DataBind();
                GridView1.Visible = true;
                // ModalPopupExtender2.Show();
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "Please Attach File to Upload.";
                return;
            }
        }
    }
    protected void Chkcertify_CheckedChanged1(object sender, EventArgs e)
    {
        string usertype = Convert.ToString(ViewState["UserType"]).ToUpper();
        if (usertype == "DEVELOPER")
        {
            lbldeveloper.Visible = true;
            lbldeveloper.Text = "I certify that I have completed the Development as per the Instruction, I also certify that  page design is good and the functionality is working properly.";
        }
        else if (usertype == "TESTER")
        {
            lbltester.Visible = true;
            lbltester.Text = "I certify that I have completed the Testing as per the Instruction, I also certify that  page design is good and the functionality is working properly.";
        }
        else if (usertype == "SUPERVISOR")
        {
            lblsupervior.Visible = true;
            lblsupervior.Text = "I certify that I have checked the page and it is working properly as planned. I also certify that proper documentation is done and is also uploaded in the system.";

        }


        Button4.Visible = true;

        //ModalPopupExtender2.Show();
    }
    protected void Button4_ClickReturn(object sender, EventArgs e)
    {

        string Insert = "Insert into PageWorkTbl_Return(PageWorkTblID,UserID,TypeofworkID ,Certify,ReturntoDev,DateandTime) values ('" + lblpageworkId.Text + "','" + Session["empname"] + "','" + ViewState["UserType"] + "','0','1','" + DateTime.Now.ToShortDateString() + "')";
        SqlCommand cmdinsert = new SqlCommand(Insert, con);
        con.Open();
        cmdinsert.ExecuteNonQuery();
        con.Close();



        Int64 id = Convert.ToInt64(ViewState["PageVersionTblID"]);
        string usertype = Convert.ToString(ViewState["UserType"]).ToUpper();

        string strcomid = "select PageVersionTblId,[EpmloyeeID_AssignedDeveloper],[EpmloyeeID_AssignedTester],[EpmloyeeID_AssignedSupervisor] from PageWorkTbl where Id='" + ViewState["Pageworktblid"] + "'";
        SqlDataAdapter adcheck1 = new SqlDataAdapter(strcomid, con);
        DataTable dtcheck1 = new DataTable();
        adcheck1.Fill(dtcheck1);
        if (dtcheck1.Rows.Count > 0)
        {
        
        string updatedev = "Update PageVersionTbl set DeveloperOK='0',TesterOk='0',SupervisorOk='0',ReturntoDevelopers='1' where Id='" + Convert.ToString(dtcheck1.Rows[0]["PageVersionTblId"]) + "'";
        SqlCommand ccm1 = new SqlCommand(updatedev, con);
        con.Open();
        ccm1.ExecuteNonQuery();
        con.Close();
        }

        Label1.Visible = true;
        Label1.Text = "This page Return to Developers";


    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Session["check"] = "Check";
        Button10_Click(Button10, e);
        if (Session["check"] != null)
        {
            string Insert = "Insert into MyDailyWorkReport(PageWorkTblId,Date,HourSpent,WorkDoneReport,EmployeeId,WorkDone,EmpRequestHour,PageId) values ('" + lblpageworkId.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + Session["empname"] + "','" + CheckBox1.Checked + "','" + txtactualhourrequired.Text + "','" + Session["Pageid"] + "')";
            SqlCommand cmdinsert = new SqlCommand(Insert, con);
            con.Open();
            cmdinsert.ExecuteNonQuery();
            con.Close();
            //fillgrid();
            Label1.Visible = true;
            Label1.Text = "Record Inserted Succesfully";
            //ModalPopupExtender2.Hide();
            //lblMsg1.Text = "Record Inserted Succesfully";
        }
       
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        lblpageworkId.Text = "";
        lblworltitleatreport.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        CheckBox1.Checked = false;
        chkupld.Checked = false;
        Chkmultiupld.Checked = false;
        Chkcertify.Checked = false;
        fileuploadadattachment.Visible = false;
        Button7.Visible = false;
        gridFileAttach.Visible = false;
        Fileupldmulti.Visible = false;
        Button8.Visible = false;
        Griddoc.Visible = false;
        lbldeveloper.Text = "";
        lbltester.Text = "";
        lblsupervior.Text = "";
        lblupldcode.Visible = false;
        lblcerti.Visible = false;
        lbluplddoc.Visible = false;
        //ModalPopupExtender2.Hide();

    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        Session["Dev"] = ""; 
        foreach (GridViewRow gdr in gridFileAttach.Rows)
        {

            Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");
            string strftpinsert = "INSERT INTO PageFinishWorkUploadTbl(PageWorkTblId,FileName,Date) values ('" + lblpageworkmasterId.Text + "','" + lblpdfurl.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

            SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
            con.Open();
            cmdinsert.ExecuteNonQuery();
            con.Close();

            ftpfile(lblpdfurl.Text.ToString(), Server.MapPath("~\\Attachment\\") + lblpdfurl.Text.ToString());


            Label2.Visible = true;
            Label2.Text = "Record Inserted Succesfully";

        }

        string VersionCopyCode = txtcopycode.Text.Trim().Replace("'", "''");
        string codeinsert123 = "INSERT INTO PagecodeMoreInfo(PageWorkTblID,PageVersionID,PageVersionCode) values ('" + lblpageworkmasterId.Text + "','" + ViewState["PageVersionTblID"] + "','" + VersionCopyCode.ToString() + "')";

        SqlCommand cmddinsert123 = new SqlCommand(codeinsert123, con);
        con.Open();
        cmddinsert123.ExecuteNonQuery();
        con.Close();

        foreach (GridViewRow gdr in Griddoc.Rows)
        {
            Label lbldocname = (Label)gdr.FindControl("lbldocname");

            string docinsert = "INSERT INTO VersionDocument_Master(PageWorkTblID,DocumentTitle,DocumentName,PageVersionID) values ('" + lblpageworkmasterId.Text + "','" + lbldocname.Text + "','" + lbldocname.Text + "','" + ViewState["PageVersionTblID"] + "')";

            SqlCommand cmdinsert = new SqlCommand(docinsert, con);
            con.Open();
            cmdinsert.ExecuteNonQuery();
            con.Close();

            ftpfile(lbldocname.Text.ToString(), Server.MapPath("~\\Attachment\\") + lbldocname.Text.ToString());


            Label2.Visible = true;
            Label2.Text = "Record Inserted Succesfully";
        }
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            Label lbldocname = (Label)gdr.FindControl("lbldocname");

            string docinsert = "INSERT INTO VersionScreensort_Master(PageWorkTblID,ScreensortTitle,ScreensortName,PageVersionID) values ('" + lblpageworkmasterId.Text + "','" + lbldocname.Text + "','" + lbldocname.Text + "','" + ViewState["PageVersionTblID"] + "')";

            SqlCommand cmdinsert = new SqlCommand(docinsert, con);
            con.Open();
            cmdinsert.ExecuteNonQuery();
            con.Close();

         //   ftpfile(lbldocname.Text.ToString(), Server.MapPath("~\\Attachment\\") + lbldocname.Text.ToString());


            Label2.Visible = true;
            Label2.Text = "Record Inserted Succesfully";
        }

        string VersionCode = txtdoccopy.Text.Trim().Replace("'", "''");
        string docmntinsert123 = "INSERT INTO PageDocMoreInfo(PageWorkTblID,PageVersionID,PageVersionCode) values ('" + lblpageworkmasterId.Text + "','" + ViewState["PageVersionTblID"] + "','" + VersionCode.ToString() + "')";

        SqlCommand cmdddinsert123 = new SqlCommand(docmntinsert123, con);
        con.Open();
        cmdddinsert123.ExecuteNonQuery();
        con.Close();

        //foreach (DataRow dr in DataList1.Items)
        int i = 0;
        for (i = 0; i <= DataList1.Items.Count - 1; i++)
        {
            CheckBox chktbl = DataList1.Items[i].FindControl("chktbl") as CheckBox;
            HiddenField hftblid = DataList1.Items[i].FindControl("hftblid") as HiddenField;
            Label lbltblname = DataList1.Items[i].FindControl("lbltblname") as Label;

            if (chktbl.Checked == true)
            {
                string tblusedinsert = "INSERT INTO Pagetableused(pageversionid,tablemasterid,tblname) values ('" + ViewState["PageVersionTblID"] + "','" + hftblid.Value + "','" + lbltblname.Text + "')";

                SqlCommand tblused = new SqlCommand(tblusedinsert, con);
                con.Open();
                tblused.ExecuteNonQuery();
                con.Close();
            }
            else
            {
 
            }
        }

        //Button4_Click(Button4, e);

        // fillgrid();

        Session["GridFileAttach1"] = null;
        gridFileAttach.DataSource = null;
        gridFileAttach.DataBind();
        Session["GridFileDocu"] = null;
        Griddoc.DataSource = null;
        Griddoc.DataBind();

        //foreach (GridViewRow gg1 in GridView2.Rows)
        //{

        Int64 id = Convert.ToInt64(ViewState["PageVersionTblID"]);
        string usertype = Convert.ToString(ViewState["UserType"]).ToUpper();

        string strcomid = "select PageVersionTblId,[EpmloyeeID_AssignedDeveloper],[EpmloyeeID_AssignedTester],[EpmloyeeID_AssignedSupervisor] from PageWorkTbl where Id='" + ViewState["Pageworktblid"] + "'";
        SqlDataAdapter adcheck1 = new SqlDataAdapter(strcomid, con);
        DataTable dtcheck1 = new DataTable();
        adcheck1.Fill(dtcheck1);

        if (dtcheck1.Rows.Count > 0)
        {
            if (Chkcertify.Checked == true)
            {
                if (usertype == "DEVELOPER")
                {
                    string updatedev = "Update PageVersionTbl set DeveloperOK='1', ReturntoDevelopers='0' ,DeveloperOkDate='" + DateTime.Now.ToShortDateString() + "',DeveloperNote='' where Id='" + Convert.ToString(dtcheck1.Rows[0]["PageVersionTblId"]) + "'";
                    SqlCommand ccm1 = new SqlCommand(updatedev, con);
                    con.Open();
                    ccm1.ExecuteNonQuery();
                    con.Close();
                    Label1.Text = "Data updated sucessfully";

                    //syncronisedata(Convert.ToString(dtcheck1.Rows[0]["PageVersionTblId"]), "deve");
                }
                if (usertype == "TESTER")
                {

                    string strcln = "Select Distinct MainMenuMaster.MainMenuName as manu,PageWorkTbl.Id,PageWorkTbl.WorkRequirementTitle,SubMenuMaster.SubMenuName,PageVersionTbl.VersionNo as VersionName,PageVersionTbl.PageName,Convert(Nvarchar,PageVersionTbl.Date,101) as Date,PageVersionTbl.Id as pvid from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join pageMaster on pageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId  inner join  MasterPageMaster  on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId where  (PageVersionTbl.Date Between '" + Session["txt1"] + "' and '" + Session["txt2"] + "') and WebsiteSectionId='" + ViewState["WebsiteSectionId"] + "' ";
                    string strtypework = " and PageVersionTbl.EmployeeId_Tester='" + Session["id"] + "' and PageVersionTbl.DeveloperOK='0'";
                    string pagename = " and PageMaster.PageId='" + ViewState["PageId"] + "'";
                    string alldata = strcln + strtypework + pagename;
                    SqlCommand cmdcount1 = new SqlCommand(alldata, con);
                    DataTable dtcount1 = new DataTable();
                    SqlDataAdapter adpcount1 = new SqlDataAdapter(cmdcount1);
                    adpcount1.Fill(dtcount1);
                    if (dtcount1.Rows.Count > 0)
                    {
                        //   txttdate.ForeColor = System.Drawing.Color.Red;
                        Label1.Visible = true;
                        Label1.Text = "Sorry, Before  Certification by Developer you can not update record Or Check previous Version Is Pending For Certified";
                        Session["check"] = null;

                        return;

                    }
                    else
                    {

                        string updatetest = "Update PageVersionTbl set TesterOk='1' ,TesterOkDate='" + DateTime.Now.ToShortDateString() + "',TesterNote='' where   Id='" + Convert.ToString(dtcheck1.Rows[0]["PageVersionTblId"]) + "'";
                        SqlCommand ccm1 = new SqlCommand(updatetest, con);
                        //SqlDataAdapter da = new SqlDataAdapter(ccm1);
                        //DataTable dt = new DataTable();
                        //da.Fill(dt);
                        //if (dt.Rows.Count > 0)
                        //{
                        con.Open();
                        ccm1.ExecuteNonQuery();
                        con.Close();
                        Label1.Visible = true;
                        Label1.Text = "Data updated sucessfully";

                        //}


                    }
                }
                if (usertype == "SUPERVISOR")
                {
                    string strclnn = "Select Distinct MainMenuMaster.MainMenuName as manu,PageWorkTbl.Id,PageWorkTbl.WorkRequirementTitle,SubMenuMaster.SubMenuName,PageVersionTbl.VersionNo as VersionName,PageVersionTbl.PageName,Convert(Nvarchar,PageVersionTbl.Date,101) as Date,PageVersionTbl.Id as pvid from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join pageMaster on pageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId  inner join  MasterPageMaster  on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId where  (PageVersionTbl.Date Between '" + Session["txt1"] + "' and '" + Session["txt2"] + "') and WebsiteSectionId='" + ViewState["WebsiteSectionId"] + "' ";
                    string strtypeworkk = " and PageVersionTbl.EmployeeId_Supervisor='" + Session["id"] + "' and PageVersionTbl.TesterOk='0'";
                    string pagenamee = " and PageMaster.PageId='" + ViewState["PageId"] + "'";
                    string alldataa = strclnn + strtypeworkk + pagenamee;
                    SqlCommand cmdcount12 = new SqlCommand(alldataa, con);
                    DataTable dtcount12 = new DataTable();
                    SqlDataAdapter adpcount12 = new SqlDataAdapter(cmdcount12);
                    adpcount12.Fill(dtcount12);
                    if (dtcount12.Rows.Count > 0)
                    {
                        Label1.Text = "Sorry, Before  Certification by Tester you can not update record Or Check previous Version Is Pending For Certified";
                        Label1.Visible = true;
                        return;
                        //string updatesup = "Update PageVersionTbl set SupervisorOk='1' ,SupervisorOkDate='" + DateTime.Now.ToShortDateString() + "',SupervisorNote='' where Id='" + Convert.ToString(dtcheck1.Rows[0]["PageVersionTblId"]) + "'";
                        //SqlCommand ccm1 = new SqlCommand(updatesup, con);
                        //con.Open();
                        //ccm1.ExecuteNonQuery();
                        //con.Close();
                        //Label1.Visible = true;
                        //Label1.Text = "Data updated sucessfully";
                        //syncronisedata(Convert.ToString(ViewState["PageVersionTblID"]), "");
                    }
                    else
                    {
                        string updatesup = "Update PageVersionTbl set SupervisorOk='1' ,SupervisorOkDate='" + DateTime.Now.ToShortDateString() + "',SupervisorNote='' where Id='" + Convert.ToString(dtcheck1.Rows[0]["PageVersionTblId"]) + "'";
                        SqlCommand ccm1 = new SqlCommand(updatesup, con);
                        con.Open();
                        ccm1.ExecuteNonQuery();
                        con.Close();
                        Label1.Visible = true;
                        Label1.Text = "Data updated sucessfully";
                        Session["Dev"] = "Dev"; 
                        syncronisedata(Convert.ToString(ViewState["PageVersionTblID"]), "");
                    }
                }


                // FillGrid();

                // Label1.Text = "Data updated sucessfully";

            }

        }


        //}
        chkupld.Visible = false;
        chkupld_CheckedChanged(chkupld, e);
        Chkmultiupld.Visible = false;
        Chkmultiupld_CheckedChanged(Chkmultiupld, e);
        Chkcertify.Visible = false;
        Chkcertify_CheckedChanged1(Chkcertify, e);
        //ModalPopupExtender2.Show();
    }

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    this.MasterPageFile = "Master/Blank.master";
    //}

    private void filldata()
    {
        string strSQL = "SELECT PWT.EpmloyeeID_AssignedDeveloper, PWT.EpmloyeeID_AssignedSupervisor, PWT.EpmloyeeID_AssignedTester FROM PageWorkTbl AS PWT " +
                           "INNER JOIN PageVersionTbl AS PVT ON PVT.ID = PWT.PageVersionTblID " +
                           "WHERE PWT.ID = " + Session["vid"];
        con.Open();
        SqlCommand cmd = new SqlCommand(strSQL, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtRoles = new DataTable();
        da.Fill(dtRoles);
        con.Close();
        string empid = "";
        string Emp_Developer = ""; string Emp_Supervisor = ""; string Emp_Tester = "";
        if (dtRoles.Rows.Count > 0)
        {
            empid = Convert.ToString(Session["id"]);
            Emp_Developer = Convert.ToString(dtRoles.Rows[0]["EpmloyeeID_AssignedDeveloper"]);
            Emp_Supervisor = Convert.ToString(dtRoles.Rows[0]["EpmloyeeID_AssignedSupervisor"]);
            Emp_Tester = Convert.ToString(dtRoles.Rows[0]["EpmloyeeID_AssignedTester"]);
        }

        string Emp_Role = "";
        if (Emp_Developer == empid)
        {
            Emp_Role = "Developer";
        }
        else if (Emp_Tester == empid)
        {
            Emp_Role = "Tester";
        }
        else if (Emp_Supervisor == empid)
        {
            Emp_Role = "Supervisor";
        }
        ViewState["UserType"] = Emp_Role;

        ViewState["Pageworktblid"] = Session["vid"];

        string strverid = "select PageVersionTblID from PageWorkTbl WHERE ID = '" + Session["vid"] + "'";
        SqlCommand cmdver = new SqlCommand(strverid, con);
        DataTable dtver = new DataTable();
        SqlDataAdapter adpver = new SqlDataAdapter(cmdver);
        adpver.Fill(dtver);
        if (dtver.Rows.Count > 0)
        {
            ViewState["PageVersionTblID"] = dtver.Rows[0]["PageVersionTblID"].ToString();
        }

        lblpageworkId.Text = Convert.ToString(Session["vid"]);
        lblworltitleatreport.Text = Convert.ToString(Session["lblworktitle"]);
        lblnewbujtedhr.Text = Convert.ToString(Session["lblbujtedhr"]);
        //GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        //int rinrow = row.RowIndex;


        //Label lblmasterId = (Label)GridView2.Rows[rinrow].FindControl("lblmasterId");


        lblpageworkmasterId.Text = Convert.ToString(Session["vid"]);


        string strcount = " select WebsiteMaster.*,PageMaster.PageId,WebsiteSection.WebsiteSectionId,PageMaster.FolderName from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.Id='" + Session["vid"] + "'";
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);
        if (dtcount.Rows.Count > 0)
        {
            lblftpurl123.Text = dtcount.Rows[0]["FTP_Url"].ToString();
            lblftpport123.Text = dtcount.Rows[0]["FTP_Port"].ToString();
            lblftpuserid.Text = dtcount.Rows[0]["FTP_UserId"].ToString();
            lblftppassword123.Text = PageMgmt.Decrypted(dtcount.Rows[0]["FTP_Password"].ToString());
            ViewState["folder"] = dtcount.Rows[0]["FolderName"].ToString();
            ViewState["WebsiteSectionId"] = dtcount.Rows[0]["WebsiteSectionId"].ToString();
            ViewState["PageId"] = dtcount.Rows[0]["PageId"].ToString();
        }
        else
        {
            lblftpurl123.Text = "";
            lblftpport123.Text = "";
            lblftpuserid.Text = "";
            lblftppassword123.Text = "";
            ViewState["folder"] = "";
        }
        Session["GridFileAttach1"] = null;
        gridFileAttach.DataSource = null;
        gridFileAttach.DataBind();

        Session["GridFileDocu"] = null;
        Griddoc.DataSource = null;
        Griddoc.DataBind();

        TextBox3.Text = System.DateTime.Now.ToShortDateString();

    }

    public void ftpfile(string inputfilepath, string filename)
    {
        // string ftphost = "ftp://FTP.Eparcel.us/OnlineAcc/" + inputfilepath;


        string[] separator1 = new string[] { "/" };
        string[] strSplitArr1 = lblftpurl123.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

        String productno = strSplitArr1[0].ToString();
        string ftpurl = "";

        if (productno == "FTP:" || productno == "ftp:")
        {
            if (strSplitArr1.Length >= 3)
            {
                ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;
                for (int i = 2; i < strSplitArr1.Length; i++)
                {
                    ftpurl += "/" + strSplitArr1[i].ToString();
                }
            }
            else
            {
                ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;

            }
        }
        else
        {
            if (strSplitArr1.Length >= 2)
            {
                ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;
                for (int i = 1; i < strSplitArr1.Length; i++)
                {
                    ftpurl += "/" + strSplitArr1[i].ToString();
                }
            }
            else
            {
                ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;

            }

        }

        string strSQL = "SELECT * FROM PageDevelopmentSourceCodeAllocateTable WHERE PageWorkTblID = " + lblpageworkId.Text.Trim();
        con.Open();
        DataTable dtTemp = new DataTable();
        SqlCommand cmd = new SqlCommand(strSQL, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtTemp);
        con.Close();

        string data = "";
        if (inputfilepath.EndsWith(".aspx"))
        {
            data = Convert.ToString(dtTemp.Rows[0]["ID"]);
        }
        else
        {
            data = Convert.ToString(dtTemp.Rows[1]["ID"]);
        }

        string strcount = " select PDSA.FileName, PDSA.PageWorkTblId as PageWorkMasterId, PDSA.OriginalFileName, PageVersionTbl.VersionNo, " +
                        "WebsiteMaster.*,PageMaster.FolderName from PageDevelopmentSourceCodeAllocateTable AS PDSA " +
                        "inner join  PageWorkTbl  on PageWorkTbl.id=PDSA.PageWorkTblId " +
                        "inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                        "inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId " +
                        "inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  " +
                        "inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  " +
                        "inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  " +
                        "inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PDSA.Id='" + data + "'";
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);

        if (lblftpurl123.Text.Length > 0)
        {
            string[] seperator_RootPath = new string[] { "/" };
            string RootPath = Convert.ToString(dtcount.Rows[0]["RootFolderPath"]);
            string[] RootPathArray = RootPath.Split(seperator_RootPath, StringSplitOptions.RemoveEmptyEntries);
            string FolderName = "";
            for (int k = 2; k < RootPathArray.Length; k++)
            {
                FolderName += RootPathArray[k].ToString() + "/";
            }
            try
            {
                ViewState["folder"] = FolderName.ToString().Substring(0, FolderName.Length - 1);
                // string ftphost = lblftpurl123.Text + "/" + inputfilepath;
                //string ftphost =ftpurl + "/" + inputfilepath;

                /*Replace on 2nov Remove Folder path  141*/
                //string ftphost = ftpurl + "/" + ViewState["folder"] + "/Attachment/" + inputfilepath;
                string ftphost = ftpurl +  "/Attachment/" + inputfilepath;
                FtpWebRequest FTP = (FtpWebRequest)FtpWebRequest.Create(ftphost);
                FTP.Credentials = new NetworkCredential(lblftpuserid.Text, lblftppassword123.Text);
                FTP.UseBinary = true;
                FTP.KeepAlive = true;
                FTP.UsePassive = true;

                FTP.Method = WebRequestMethods.Ftp.UploadFile;
                FileStream fs = File.OpenRead(filename);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
                Stream ftpstream = FTP.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
            }
            catch (Exception ex)
            {
            }
          

            if (inputfilepath.EndsWith(".aspx") || inputfilepath.EndsWith(".aspx.cs"))
            {
                string VersionNo = Convert.ToString(dtcount.Rows[0]["VersionNo"]);
                string VersionFolderName = "pageversion" + VersionNo.ToString().Trim();
                string NewFileName = "";
                if (inputfilepath.EndsWith(".aspx"))
                {
                   
                    NewFileName += Convert.ToString(dtTemp.Rows[0]["OriginalFileName"]);
                    
                }
                else
                {
                    NewFileName = Convert.ToString(dtTemp.Rows[1]["OriginalFileName"]);
                    
                }

                /*Replace on 2nov Remove Folder path  141*/
               //2Nov141 string ftphost_Ver = ftpurl + "/" + ViewState["folder"] + "/VersionFolder/" + VersionFolderName + "/" + NewFileName;
                string ftphost_Ver = ftpurl +  "/" + VersionFolderName + "/" + NewFileName;
                
                
                FtpWebRequest FTP_Ver = (FtpWebRequest)FtpWebRequest.Create(ftphost_Ver);
                
                string fileUserid=Convert.ToString(dtcount.Rows[0]["FileUploadUserId"].ToString());
               //2Nov141 FTP_Ver.Credentials = new NetworkCredential(lblftpuserid.Text, lblftppassword123.Text);
                FTP_Ver.Credentials = new NetworkCredential(fileUserid, PageMgmt.Decrypted(dtcount.Rows[0]["FileuploadPW"].ToString()));
                FTP_Ver.UseBinary = true;
                FTP_Ver.KeepAlive = true;
                FTP_Ver.UsePassive = true;
                try
                {
                   
                    FTP_Ver.Method = WebRequestMethods.Ftp.UploadFile;
                    FileStream fs_Ver = File.OpenRead(filename);
                    byte[] buffer_Ver = new byte[fs_Ver.Length];
                    fs_Ver.Read(buffer_Ver, 0, buffer_Ver.Length);
                    fs_Ver.Close();
                    Stream ftpstream_Ver = FTP_Ver.GetRequestStream();
                    ftpstream_Ver.Write(buffer_Ver, 0, buffer_Ver.Length);
                    ftpstream_Ver.Close();
                }
                catch (Exception ex)
                {
                }
                
            }
        }
        System.IO.File.Delete(filename);
    }

    protected void syncronisedata(string Pageverid, string deve)
    {

        lblftpurl123.Text = "";
        lblftpport123.Text = "";
        lblftpuserid.Text = "";
        lblftppassword123.Text = "";


        //string strcount = "select Distinct WebsiteMaster.*,PageMaster.FolderName,PageFinishWorkUploadTbl.Folder_path,PageMaster.PageName,PageFinishWorkUploadTbl.FileName,PageMaster.PageId,PageVersionTbl.Id as pvid,PageVersionTbl.VersionNo from PageFinishWorkUploadTbl inner join PageWorkTbl on PageFinishWorkUploadTbl.PageWorkTblId=PageWorkTbl.Id inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageVersionTbl.SupervisorOk='1' and PageVersionTbl.TesterOk='1'  and PageVersionTbl.DeveloperOK='1' and PageVersionTbl.Id='" + Pageverid + "'";
        //string strcount = "select Distinct WebsiteMaster.*,PageMaster.FolderName,PageFinishWorkUploadTbl.Folder_path,PageMaster.PageName,PageFinishWorkUploadTbl.FileName,PageMaster.PageId,PageVersionTbl.Id as pvid,PageVersionTbl.VersionNo,PageVersionTbl.PageName as vpname from PageFinishWorkUploadTbl inner join PageWorkTbl on PageFinishWorkUploadTbl.PageWorkTblId=PageWorkTbl.Id inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where   PageVersionTbl.DeveloperOK='1' and PageVersionTbl.Id='" + Pageverid + "'";
        string strcount = "SELECT DISTINCT WebsiteMaster.*, PageMaster.FolderName, PDSA.Folder_path, PageMaster.PageName, PDSA.OriginalFileName, " +
                        "PDSA.FileName, PageMaster.PageId, PageVersionTbl.Id as pvid, PageVersionTbl.VersionNo, PageVersionTbl.PageName as vpname " +
                        "from PageDevelopmentSourceCodeAllocateTable AS PDSA " +
                        "inner join PageWorkTbl on PDSA.PageWorkTblId=PageWorkTbl.Id " +
                        "inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId " +
                        "inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId " +
                        "inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  " +
                        "inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  " +
                        "inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  " +
                        "inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId " +
                        "where   PageVersionTbl.DeveloperOK='1' and PageVersionTbl.Id='" + Pageverid + "'";
        //NOTE : Changed the name of the table from PageFinishWorkUploadTbl to PageDevelopmentSourceCodeAllocateTable bcoz while allocating any task to developer
        // we are entering record in that table - 17-Jan-2015
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);

        // string filedate = "UpdatedVersion" +dtcount.Rows[0]["VersionNo"].ToString()+ DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        string filedate = "";
        int RowIndex = 0;
        foreach (DataRow dt in dtcount.Rows)
        {
            try
            {
                lblftpurl123.Text = dt["FTP_Url"].ToString();
                lblftpport123.Text = dt["FTP_Port"].ToString();
                lblftpuserid.Text = dt["FTP_UserId"].ToString();
                //lblftppassword123 = PageMgmt.Decrypted(dt["FTP_Password"].ToString());

                lbl_versionFTPID.Text = dtcount.Rows[0]["FileuploadUserId"].ToString();
                lbl_versionFTPPass.Text = PageMgmt.Decrypted(dtcount.Rows[0]["FileuploadPW"].ToString());

                lblftppassword123.Text = PageMgmt.Decrypted(dt["FTP_Password"].ToString());
                if(Session["Dev"]=="Dev")
                {
                    lbl_versionFTPID.Text = dtcount.Rows[0]["FTP_UserId"].ToString();
                    lbl_versionFTPPass.Text = PageMgmt.Decrypted(dtcount.Rows[0]["FTP_Password"].ToString());
                }
                if (deve == "deve")
                {
                    filedate = "/VersionFolder";
                }
                //if (Convert.ToString(dt["Folder_path"]) == "")
                //{
                //    ViewState["folder"] = dt["FolderName"].ToString() + filedate;
                //}
                //else
                //{
                //    ViewState["folder"] = Convert.ToString(dt["Folder_path"]);
                //}
                string VersionNo = Convert.ToString(dtcount.Rows[0]["VersionNo"]);
                string VersionFolderName = "pageversion" + VersionNo.ToString().Trim();
                string foldername = Convert.ToString(dtcount.Rows[0]["FolderName"]);
                //string[] seperator_RootPath = new string[] { "/" };
                //string RootPath = Convert.ToString(dt["RootFolderPath"]);
                //string[] RootPathArray = RootPath.Split(seperator_RootPath, StringSplitOptions.RemoveEmptyEntries);
                //string FolderName = "";
                //for (int k = 2; k < RootPathArray.Length; k++)
                //{
                //    FolderName += RootPathArray[k].ToString() + "/";
                //}
                //ViewState["folder"] = FolderName.ToString().Substring(0, FolderName.Length - 1);

                ViewState["folder"] = dt["FolderName"].ToString();
                ViewState["RootPath"] = dt["RootFolderPath"];
                ViewState["VersionRootPath"] = dt["VersionFolderRootPath"];
                //string filename = Server.MapPath("~\\Attachment\\") + Convert.ToString(dt["FileName"]).Replace(" ", "");
                string filename = ViewState["RootPath"] + foldername  + "/VersionFolder/" + VersionFolderName + "/" + Convert.ToString(dt["OriginalFileName"]).Replace(" ", "");
                filename = ViewState["VersionRootPath"] + VersionFolderName + "/" + Convert.ToString(dt["OriginalFileName"]).Replace(" ", "");
                     //      E:\141\New\beta.busiwiz.com\VersionFolder/         
                //I:/capmanversion/license.busiwiz.com/VersionFolder/
                string NewFileName = "";
                if (RowIndex == 1)
                {
                    NewFileName = dtcount.Rows[1]["PageName"].ToString() + ".cs";
                }
                else
                {
                    NewFileName = dtcount.Rows[0]["PageName"].ToString();
                }

                //ftpfile(Convert.ToString(dt["FileName"]).Replace(" ", ""), filename, Convert.ToString(dt["PageId"]), Convert.ToString(dt["pvid"]), filedate, Convert.ToString(dt["WebsiteUrl"]));
                ftpfile1(NewFileName.ToString(), filename, Convert.ToString(dt["PageId"]), Convert.ToString(dt["pvid"]), filedate, Convert.ToString(dt["WebsiteUrl"]));

                //if (deve == "")
                //{
                //    string fb = Convert.ToString(dt["FileName"]).Replace(" ", "");

                //    string filexten = Path.GetExtension(filename);
                //    if (filexten.ToUpper() == ".cs".ToUpper())
                //    {
                //        fb = fb.Remove(fb.Length - 3, 3);

                //    }
                //    if (Convert.ToString(dt["PageName"]).Replace(" ", "").ToUpper() == Convert.ToString(dt["FileName"]).Replace(" ", "").ToUpper())
                //    {
                //        ftpfile(Convert.ToString(dt["FileName"]).Replace(" ", ""), filename, Convert.ToString(dt["PageId"]), Convert.ToString(dt["pvid"]), filedate, Convert.ToString(dt["WebsiteUrl"]));
                //    }
                //    else if (Convert.ToString(dt["PageName"]).Replace(" ", "").ToUpper() == fb.Replace(" ", "").ToUpper())
                //    {
                //        ftpfile(Convert.ToString(dt["FileName"]).Replace(" ", ""), filename, Convert.ToString(dt["PageId"]), Convert.ToString(dt["pvid"]), filedate, Convert.ToString(dt["WebsiteUrl"]));
                //    }
                //}
                //else
                //{
                //    ftpfile(Convert.ToString(dt["FileName"]).Replace(" ", ""), filename, Convert.ToString(dt["PageId"]), Convert.ToString(dt["pvid"]), filedate, Convert.ToString(dt["WebsiteUrl"]));

                //}
                RowIndex += 1;
            }
            catch (Exception e1)
            {
                Label1.Text = e1.ToString();
            }

        }
    }

    public void ftpfile1(string inputfilepath, string filename, string pageid, string pvid, string filedate, string websitename1)
    {
        // string ftphost = "ftp://FTP.Eparcel.us/OnlineAcc/" + inputfilepath;

        FileInfo filec = new FileInfo(filename);
        if (filec.Exists == true || filec.Exists == false)
        {
            string[] separator1 = new string[] { "/" };
            string[] strSplitArr1 = lblftpurl123.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            String productno = strSplitArr1[0].ToString();
            string ftpurl = "";

            if (productno == "FTP:" || productno == "ftp:")
            {
                if (strSplitArr1.Length >= 3)
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;

                }

            }


            if (lblftpurl123.Text.Length > 0)
            {
                string ftphost = ftpurl + "/" + ViewState["folder"] + "/" + inputfilepath;

                FtpWebRequest FTP = (FtpWebRequest)FtpWebRequest.Create(ftphost);

                try
                {
                    string websitename = websitename1;

                    //string value = stopwebsite(websitename);

                   //1412Dec FTP.Credentials = new NetworkCredential(lblftpuserid.Text, lblftppassword123.Text);
                    FTP.Credentials = new NetworkCredential(lbl_versionFTPID.Text, lbl_versionFTPPass.Text);
                    
                    FTP.UseBinary = false;
                    FTP.KeepAlive = true;
                    FTP.UsePassive = true;
                    FTP.Method = WebRequestMethods.Ftp.UploadFile;


                    FileStream fs = File.OpenRead(filename);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();

                    Stream ftpstream = FTP.GetRequestStream();
                    ftpstream.Write(buffer, 0, buffer.Length);
                    ftpstream.Close();

                    if (filedate == "")
                    {
                        // System.IO.File.Delete(filename);
                    }
                    //string value1 = startwebsite(websitename);

                    //Label3.Text = ftphost;
                    //Label4.Text = filename;
                    //Label5.Text = lblftpuserid;
                    //Label6.Text = lblftppassword123;
                }
                catch
                {
                    string value1 = startwebsite(websitename1);
                    //Response.Write(ftphost);
                    //Response.Write(filename);

                }
            }

        }

    }

    public string startwebsite(string websitename)
    {
        try
        {
            string siteid = getsiteid(websitename);
            if (siteid == null) return "error:this web site is not exist.";

            DirectoryEntry devdir = new DirectoryEntry("IIS://localhost/W3SVC/" + siteid);
            devdir.Invoke("start", null);
            return "successful:start web site " + websitename + " is succeed!";

        }
        catch (Exception e)
        {
            return "error:start web site has been failed." + e.Message;
        }
    }

    public string getsiteid(string websitename)
    {
        DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
        try
        {
            string siteid = null;
            foreach (DirectoryEntry bb in root.Children)
            {
                if (bb.SchemaClassName == "IIsWebServer")
                {
                    if (websitename == bb.Properties["ServerComment"].Value.ToString()) siteid = bb.Name;
                }
            }
            if (siteid == null) return null;
            return siteid;
        }
        catch
        {
            return null;
        }
    }

    private void filltablename()
    {
        string filltble = "SELECT name, id FROM Sysobjects where xtype = 'u' order by name";

        SqlCommand cmdcln1234 = new SqlCommand(filltble, con);
        DataTable dtcln1234 = new DataTable();
        SqlDataAdapter adpcln1234 = new SqlDataAdapter(cmdcln1234);
        adpcln1234.Fill(dtcln1234);

        if (dtcln1234.Rows.Count > 0)
        {
           
            //Gridtallist.DataSource = dtcln1234;
            //Gridtallist.DataBind();
            DataList1.DataSource = dtcln1234;
            DataList1.DataBind();
        }

    }

   
}