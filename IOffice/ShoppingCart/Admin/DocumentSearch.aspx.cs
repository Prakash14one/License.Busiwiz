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
using System.Text;
using System.Drawing.Printing;
using System.Drawing.Design;
using System.Drawing;
//using System.ServiceProcess;
using System.Diagnostics;
using System.Windows;
using System.Data.SqlClient;

using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;

using System.Net;
using System.Net.Mail;
public partial class Account_DocumentMyUploaded : System.Web.UI.Page
{
    object paramMissing = Type.Missing;
    public string errormessage;
    private bool wordavailable = false;
    private bool checkedword = false;
    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    protected int DesignationId;
    EmployeeCls clsEmployee = new EmployeeCls();
    string DesignationMasterId = "";
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
            ViewState["sortOrder"] = "desc";
            pageMailAccess();
            lblcompny.Text = Session["comid"].ToString();
            lblcomname.Text = "All";
            flaganddoc();
            CheckBox1_CheckedChanged(sender, e);
            if (Request.QueryString["Tid"] != null)
            {
                pnlentry.Visible = true;
                Button1.Visible = true;
                lblmsg.Visible = false;
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
            string str = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster  where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "'  order by name";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlbusiness.DataSource = dt;
            ddlbusiness.DataTextField = "Name";
            ddlbusiness.DataValueField = "WareHouseId";
            ddlbusiness.DataBind();
            ddlbusiness.Items.Insert(0, "All");
            ddlbusiness.Items[0].Value = "0";


            string eeed = " Select distinct EmployeeMaster.Whid, DesignationMasterId from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
            SqlCommand cmdeeed = new SqlCommand(eeed, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);

            if (dteeed.Rows.Count > 0)
            {
                ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                DesignationMasterId = Convert.ToString(dteeed.Rows[0]["DesignationMasterId"]);
            }
            ViewState["state"] = "1";

            if (Request.QueryString["id1"] != null && Request.QueryString["id2"] != null && Request.QueryString["id3"] != null && Request.QueryString["id4"] != null)
            {
                ddlbusiness.SelectedValue = Request.QueryString["id4"].ToString();
                // RadioButton2_CheckedChanged(sender, e);
                ddlSearchby_SelectedIndexChanged(sender, e);
                txtfrom.Text = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
                txtto.Text = System.DateTime.Now.ToShortDateString();

                ddlmaindotype.SelectedValue = Request.QueryString["id1"].ToString();
                ddlmaindotype_SelectedIndexChanged(sender, e);

                ddlsubdoctype.SelectedValue = Request.QueryString["id2"].ToString();
                ddlsubdoctype_SelectedIndexChanged(sender, e);

                ddldoctype.SelectedValue = Request.QueryString["id3"].ToString();

                ddlbusiness.Enabled = false;
                //RadioButton1.Visible = false;
                //RadioButton2.Visible = false;
                //RadioButton3.Visible = false;
                ddlSearchby.Visible = false;
                Label7.Visible = false;
                //Label35.Visible = true;
            }
            else
            {
                // RadioButton2_CheckedChanged(sender, e);
                ddlSearchby_SelectedIndexChanged(sender, e);
                txtfrom.Text = System.DateTime.Now.Month.ToString() + "/01/" + System.DateTime.Now.Year.ToString();
                //txtfrom.Text = System.DateTime.Now.ToShortDateString();
                txtto.Text = System.DateTime.Now.ToShortDateString();
            }
            imgbtnsubmit_Click(sender, e);


        }

    }

    protected void flaganddoc()
    {
        DataTable dts = select("select Id,documentflagname from Mydocumentflagstbl where cid='" + Session["Comid"] + "' Order by documentflagname");
        ddlflag.DataSource = dts;
        ddlflag.DataTextField = "documentflagname";
        ddlflag.DataValueField = "Id";
        ddlflag.DataBind();
        ddlflag.Items.Insert(0, "All");
        ddlflag.Items[0].Value = "0";

        DataTable dts1 = select("select Id,name from DocumentTypenm where  active='1' Order by name");
        ddldoctypem.DataSource = dts1;
        ddldoctypem.DataTextField = "name";
        ddldoctypem.DataValueField = "Id";
        ddldoctypem.DataBind();
        ddldoctypem.Items.Insert(0, "All");
        ddldoctypem.Items[0].Value = "0";
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



    protected void imgBtnGoID_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid(1);
        ViewState["state"] = "1";
        if (Gridreqinfo.Rows.Count == 0)
        {
            //  pnlmsg.Visible = true;
            lblmsg.Visible = true;
            lblmsg.Text = "No Record found";
        }
        //txtDocId.Text = "";
        //txtDocTitle.Text = "";
    }
    protected void ImgBtnGoTitle_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid(2);
        ViewState["state"] = "2";
        if (Gridreqinfo.Rows.Count == 0)
        {
            lblmsg.Visible = true;
            // pnlmsg.Visible = true;
            lblmsg.Text = "No Record found";
        }
        //txtDocTitle.Text = "";

    }
    protected void ImgBtnGoType_Click(object sender, ImageClickEventArgs e)
    {

        FillGrid(3);
        ViewState["state"] = "3";
        if (Gridreqinfo.Rows.Count == 0)
        {
            lblmsg.Visible = true;
            // pnlmsg.Visible = true;
            lblmsg.Text = "No Record found";
        }
    }
    protected void ImgBtnGoParty_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid(4);
        ViewState["state"] = "4";
        if (Gridreqinfo.Rows.Count == 0)
        {
            lblmsg.Visible = true;
            //  pnlmsg.Visible = true;
            lblmsg.Text = "No Record found";
        }
    }




    protected void FillGrid(int i)
    {
        string monter = " ";
        int k = 0;
        int j = 0;
        string accessdesi = "";
        string orderby = " order by DocumentMaster.DocumentId Desc ";

        string whwhe="";
        string whidstr = "";
            if (ddlbusiness.SelectedIndex > 0)
            {
                whwhe += " where DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "'  ";
            }
            if (ddlbusiness.SelectedIndex > 0)
            {
                whidstr += " and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "'  ";
               whidstr += " and DocumentAccessRightMaster.DesignationId='" + DesignationMasterId + "'  ";
            }
           
        if (CheckBox1.Checked == true)
        {
            if (RadioButtonList1.SelectedIndex == 0)
            {
                monter = " and (Cast(DocumentMaster.DocumentDate as date)) between '" + Convert.ToDateTime(txtfrom.Text).ToShortDateString() + "' and '" + Convert.ToDateTime(txtto.Text).ToShortDateString() + "')";

                j = 1;
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                monter = " and (Cast(DocumentMaster.DocumentUploadDate as date)) between '" + Convert.ToDateTime(txtfrom.Text).ToString() + "' and '" + Convert.ToDateTime(txtto.Text).ToString() + "')";

                j = 2;
            }
        }
        else if (CheckBox1.Checked == false)
        {
            j = 0;
        }
        if (rdlistaccentry.SelectedValue == "0")
        {
            k = 0;
        }
        else if (rdlistaccentry.SelectedValue == "1")
        {
            monter = monter + " and DocumentMaster.DocumentId Not In( Select Distinct AttachmentMaster.IfilecabinetDocId from  DocumentMainType inner join     DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId INNER JOIN DocumentMaster  ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId inner join AttachmentMaster  on AttachmentMaster.IfilecabinetDocId=DocumentMaster.DocumentId " + whwhe + " )";
            k = 1;
        }
        else if (rdlistaccentry.SelectedValue == "2")
        {
            monter = monter + " and DocumentMaster.DocumentId  IN( Select Distinct AttachmentMaster.IfilecabinetDocId from  DocumentMainType inner join     DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId INNER JOIN DocumentMaster  ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId inner join AttachmentMaster  on AttachmentMaster.IfilecabinetDocId=DocumentMaster.DocumentId " + whwhe + " )";

            k = 2;
        }
        DataTable dt = new DataTable();
        //dt = clsDocument.SelectDocumentAccessRigthsByDesignationIDGene(ddlbusiness.SelectedValue);
        String dotypid = "";
        DataTable dtdes = select(" SELECT  Distinct  DocumentAccessRightMaster.DocumentTypeId FROM DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentAccessRightMaster on DocumentAccessRightMaster.DocumentTypeId=DocumentType.DocumentTypeId  WHERE   (DocumentAccessRightMaster.CID='" + Session["Comid"] + "')" + accessdesi + " and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')   or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))");
        foreach (DataRow item in dtdes.Rows)
        {
            if (dotypid == "")
            {
                dotypid += "'" + item["DocumentTypeId"] + "'";
            }
            else
            {
                dotypid += ",'" + item["DocumentTypeId"] + "'";
            }

        }

        if (dotypid != "")
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            string sortex = "";
            if (hdnsortExp.Value.Length == 0)
            {
                sortex = " DocumentMaster.DocumentId Desc";
            }
            else
            {
                sortex = hdnsortExp.Value + " " + hdnsortDir.Value;
            }
            string fillda = "";
            string coutdata = "";
            string Doctynm = "";
            string strflag = "";
            if (ddldoctypem.SelectedIndex > 0)
            {
                Doctynm = " and DocumentTypenmId='" + ddldoctypem.SelectedValue + "'";
            }
            if (ddlbusiness.SelectedIndex > 0)
            {
                strflag += " and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "'  ";
            }
             
            
            if (ddlflag.SelectedIndex > 0)
            {
                if (ddlflag.SelectedItem.Text == "Unread")
                {
                    strflag += " and DocumentMaster.DocumentId NOT IN(Select Distinct DocumentID from  DocumentFlag inner join Mydocumentflagstbl on Mydocumentflagstbl.Id=DocumentFlag.MyDocumentFlagID where  UserID='"+Session["UserId"]+"' and documentflagname='Read')";
                }
                else
                {
                    strflag += " and DocumentMaster.DocumentId IN(Select Distinct DocumentID from  DocumentFlag inner join Mydocumentflagstbl on Mydocumentflagstbl.Id=DocumentFlag.MyDocumentFlagID where UserID='" + Session["UserId"] + "' and documentflagname='Read')";

                }
                
             }

            //foreach (DataRow dr in dt.Rows)
            //{
            if (i == 1)
            {

                fillda = "DocumentMaster.DocumentId,DocumentTypenm.Name as Doccname, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentName, DocumentMaster.DocumentUploadDate,DocumentMaster.DocumentDate, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId, DocumentMaster.DocumentRefNo, DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId,DocumentMaster.FileExtensionType, Left(DocumentMainType.DocumentMainType,4)+':'+Left(DocumentSubType.DocumentSubType,4)+':'+ DocumentType.DocumentType as DocumentType,Party_master.Compname as PartyName FROM  DocumentMainType inner join     DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId INNER JOIN DocumentMaster  ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId  Left join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId Left Join DocumentTypenm on DocumentTypenm.Id=DocumentMaster.DocumentTypenmId WHERE   (DocumentMaster.DocumentTypeId in(" + dotypid + ")) " + whidstr + " and (Party_Master.Compname like '%" + txtDocId.Text + "%' or Party_Master.Contactperson like '%" + txtDocId.Text + "%' or  Cast ( DocumentMaster.DocumentId as nvarchar)='" + txtDocId.Text + "'  or DocumentMaster.DocumentTitle like '%" + txtDocId.Text + "%' ) and DocumentMaster.DocumentId in ( SELECT  DocumentMaster.DocumentId FROM         DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId WHERE    (DocumentMaster.DocumentTypeId in(" + dotypid + ") " + strflag + " and  (Approve != 0 OR Approve IS NOT NULL) and  (Levelofaccess IN('1','2','3'))    AND DocumentMaster.CID='" + Session["Comid"] + "'" + Doctynm + monter;
                coutdata = "Select Count(DocumentMaster.DocumentId) ci FROM  DocumentMainType inner join     DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId INNER JOIN DocumentMaster  ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId  Left join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId Left Join DocumentTypenm on DocumentTypenm.Id=DocumentMaster.DocumentTypenmId WHERE    (DocumentMaster.DocumentTypeId in(" + dotypid + ")) " + whidstr + "  and (Party_Master.Compname like '%" + txtDocId.Text + "%' or Party_Master.Contactperson like '%" + txtDocId.Text + "%' or  Cast ( DocumentMaster.DocumentId as nvarchar)='" + txtDocId.Text + "'  or DocumentMaster.DocumentTitle like '%" + txtDocId.Text + "%' ) and DocumentMaster.DocumentId in ( SELECT  DocumentMaster.DocumentId FROM         DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId WHERE    (DocumentMaster.DocumentTypeId in(" + dotypid + "))" + strflag + " and (Approve != 0 OR Approve IS NOT NULL) and  (Levelofaccess IN('1','2','3'))  AND DocumentMaster.CID='" + Session["Comid"] + "'" + Doctynm + monter;

            }
            else if (i == 3)
            {

                if (ddldoctype.SelectedIndex > 0)
                {
                    monter = monter + " and (DocumentMaster.DocumentTypeId = '" + Convert.ToInt32(ddldoctype.SelectedValue) + "')";

                }
                else if (ddlsubdoctype.SelectedIndex > 0)
                {
                    monter = monter + " and (DocumentSubType.DocumentSubTypeId  = '" + Convert.ToInt32(ddlsubdoctype.SelectedValue) + "')";

                }
                else if (ddlmaindotype.SelectedIndex > 0)
                {
                    monter = monter + " and (DocumentMainType.DocumentMainTypeId = '" + Convert.ToInt32(ddlmaindotype.SelectedValue) + "')";

                }

                fillda = " DocumentMaster.DocumentId,DocumentTypenm.Name as Doccname, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentName, DocumentMaster.DocumentUploadDate,DocumentMaster.DocumentDate, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId,  DocumentMaster.DocumentRefNo, DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId,DocumentMaster.FileExtensionType,  Left(DocumentMainType.DocumentMainType,4)+':'+Left(DocumentSubType.DocumentSubType,4)+':'+ DocumentType.DocumentType as DocumentType,  Party_master.Compname as PartyName FROM DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId INNER JOIN DocumentMaster  ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId  Left join Party_Master ON DocumentMaster.PartyId = Party_master.PartyId Left Join DocumentTypenm on DocumentTypenm.Id=DocumentMaster.DocumentTypenmId WHERE   (DocumentMaster.DocumentTypeId in(" + dotypid + ")) " + whidstr + " and DocumentMaster.DocumentId in ( SELECT  DocumentMaster.DocumentId FROM         DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId WHERE    (DocumentMaster.DocumentTypeId in(" + dotypid + "))" + strflag + " and (Approve != 0 OR Approve IS NOT NULL) and  (Levelofaccess IN('1','2','3'))  AND DocumentMaster.CID='" + Session["Comid"] + "'" + Doctynm + monter + "";
                coutdata = "Select Count(DocumentMaster.DocumentId) ci FROM  DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId INNER JOIN DocumentMaster  ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId  LEFT OUTER JOIN Party_master ON DocumentMaster.PartyId = Party_master.PartyId Left Join DocumentTypenm on DocumentTypenm.Id=DocumentMaster.DocumentTypenmId WHERE  (DocumentMaster.DocumentTypeId in(" + dotypid + ")) " + whidstr + " and DocumentMaster.DocumentId in ( SELECT  DocumentMaster.DocumentId FROM         DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId WHERE    (DocumentMaster.DocumentTypeId in(" + dotypid + ")) " + strflag + "and (Approve != 0 OR Approve IS NOT NULL) AND DocumentMaster.CID='" + Session["Comid"] + "'" + Doctynm + monter + "";

            }
            else
            {
                fillda = "   DocumentMaster.DocumentId,DocumentTypenm.Name as Doccname, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentName, DocumentMaster.DocumentUploadDate,DocumentMaster.DocumentDate, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId, DocumentMaster.DocumentRefNo, DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId,DocumentMaster.FileExtensionType, Left(DocumentMainType.DocumentMainType,4)+':'+Left(DocumentSubType.DocumentSubType,4)+':'+ DocumentType.DocumentType as DocumentType,Party_master.Compname as PartyName FROM  DocumentMainType inner join  DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId INNER JOIN DocumentMaster  ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId Left join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId Left Join DocumentTypenm on DocumentTypenm.Id=DocumentMaster.DocumentTypenmId WHERE (DocumentMaster.PartyId ='" + ddlParty.SelectedValue + "') " + whidstr + " and (DocumentMaster.DocumentTypeId in(" + dotypid + ")) and DocumentMaster.DocumentId in ( SELECT  DocumentMaster.DocumentId FROM         DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId WHERE    (DocumentMaster.DocumentTypeId in(" + dotypid + ")) " + strflag + " and (Approve != 0 OR Approve IS NOT NULL) and  (Levelofaccess IN('1','2','3')) and (DocumentMaster.PartyId ='" + ddlParty.SelectedValue + "')   AND DocumentMaster.CID='" + Session["Comid"] + "'" + Doctynm + monter;
                coutdata = "Select Count(DocumentMaster.DocumentId) ci FROM DocumentMainType inner join  DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId INNER JOIN DocumentMaster  ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId  Left join Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId Left Join DocumentTypenm on DocumentTypenm.Id=DocumentMaster.DocumentTypenmId WHERE (DocumentMaster.PartyId ='" + ddlParty.SelectedValue + "') " + whidstr + "  and (DocumentMaster.DocumentTypeId in(" + dotypid + ")) and (Party_Master.PartyId='" + ddlParty.SelectedValue + "') and DocumentMaster.DocumentId in ( SELECT  DocumentMaster.DocumentId FROM         DocumentProcessing inner join DocumentMaster on DocumentMaster.DocumentId=DocumentProcessing.DocumentId inner join DocumentType ON  DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId WHERE    (DocumentMaster.DocumentTypeId in(" + dotypid + "))" + strflag + " and (Approve != 0 OR Approve IS NOT NULL) and  (Levelofaccess IN('1','2','3'))  " + whidstr + " and DocumentMaster.CID='" + Session["Comid"] + "'" + Doctynm + monter;

            }
            //if (flag == 1)
            //{
            //    dt2 = dt1.Clone();
            //    flag = 0;
            //}
            //foreach (DataRow r in dt1.Rows)
            //{
            //    dt2.ImportRow(r);
            //}
            //}
            Gridreqinfo.VirtualItemCount = GetRowCount(coutdata);
            dt1 = GetDataPage(Gridreqinfo.PageIndex, Gridreqinfo.PageSize, sortex, fillda);
            Gridreqinfo.DataSource = dt1;
            Gridreqinfo.DataBind();
        }

        if (Gridreqinfo.Rows.Count > 0)
        {
            for (int gridrow = 0; gridrow < Gridreqinfo.Rows.Count; gridrow++)
            {
                LinkButton docis = (LinkButton)Gridreqinfo.Rows[gridrow].Cells[3].FindControl("LinkButton1");
                Label lblflag = (Label)Gridreqinfo.Rows[gridrow].FindControl("lblflag");
                fillread(docis, lblflag);
                foreach (DataRow ddr in dtdes.Rows)
                {
                    if (Convert.ToInt32(Gridreqinfo.DataKeys[gridrow].Value) == Convert.ToInt32(ddr["DocumentTypeId"]))
                    {
                        string str = "SELECT   Top(1)  DocumentAccessRightId, DocumentTypeId, DesignationId, ViewAccess, DeleteAccess, SaveAccess, EditAccess, EmailAccess, FaxAccess, PrintAccess, MessageAccess FROM         DocumentAccessRightMaster WHERE   (DocumentTypeId='" + Convert.ToInt32(ddr["DocumentTypeId"]) + "')  and (CID='" + Session["Comid"] + "') and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))Order by DocumentAccessRightId Desc";
                        SqlCommand cmd1 = new SqlCommand(str, con);
                        cmd1.CommandType = CommandType.Text;
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataTable dtr = new DataTable();
                        da.Fill(dtr);

                        if (dtr.Rows.Count > 0)
                        {
                            Gridreqinfo.Rows[gridrow].Cells[13].Enabled = Convert.ToBoolean(dtr.Rows[0]["EditAccess"]);

                            if (Convert.ToString(dtr.Rows[0]["EditAccess"]) == "False")
                            {
                                //Gridreqinfo.Rows[gridrow].Cells[0].BackColor = System.Drawing.Color.Red;
                                ImageButton imgedit = (ImageButton)Gridreqinfo.Rows[gridrow].FindControl("ImageButton1");
                                imgedit.ImageUrl = "~/Account/images/AD.png";
                                imgedit.Enabled = false;
                                //CheckBox chkcopy = (CheckBox)Gridreqinfo.Rows[gridrow].FindControl("chkcopy");
                                ////imgcopy.ImageUrl = "~/Account/images/AD.png";
                            }
                            
                            Gridreqinfo.Rows[gridrow].Cells[14].Enabled = Convert.ToBoolean(dtr.Rows[0]["DeleteAccess"]);
                            if (Convert.ToString(dtr.Rows[0]["DeleteAccess"]) == "False")
                            {
                                ImageButton imgDel = (ImageButton)Gridreqinfo.Rows[gridrow].FindControl("ImageButton2");
                                imgDel.ImageUrl = "~/Account/images/AD.png";
                                imgDel.Enabled = false;
                            }
                            //Gridreqinfo.Rows[gridrow].Cells[5].Enabled = Convert.ToBoolean(dtr.Rows[0]["ViewAccess"]);
                            CheckBox chkcopy = (CheckBox)Gridreqinfo.Rows[gridrow].FindControl("chkcopy");
                            chkcopy.Enabled = Convert.ToBoolean(dtr.Rows[0]["SaveAccess"]);
                            CheckBox chksubbox = (CheckBox)Gridreqinfo.Rows[gridrow].FindControl("chksubbox");

                            chksubbox.Enabled = Convert.ToBoolean(dtr.Rows[0]["SaveAccess"]);
                            CheckBox chkmail = (CheckBox)Gridreqinfo.Rows[gridrow].FindControl("chkmail");
                            CheckBox chkmsg = (CheckBox)Gridreqinfo.Rows[gridrow].FindControl("chkmsg");
                            chkmail.Enabled = Convert.ToBoolean(dtr.Rows[0]["EmailAccess"]);
                            chkmsg.Enabled = Convert.ToBoolean(dtr.Rows[0]["MessageAccess"]);
                        }
                    }
                }
              
                     
            }

            foreach (GridViewRow gdr in Gridreqinfo.Rows)
            {
                DataTable dt6 = new DataTable();
                LinkButton lnk1 = (LinkButton)gdr.Cells[3].FindControl("LinkButton1");
                dt6 = clsDocument.SelectDocumentFolderTotalByDocument(Convert.ToInt32(lnk1.Text));

                if (dt6.Rows.Count > 0)
                {
                    LinkButton lnkbtn = (LinkButton)gdr.Cells[11].FindControl("LinkButton2");
                    // lnkbtn.Text = lnkbtn.Text + "(" + dt6.Rows[0]["total"].ToString() + ")";
                    lnkbtn.Text = "(" + dt6.Rows[0]["total"].ToString() + ")";

                }


                string scpt = "select Entry_Type_Name,EntryNumber,TranctionMaster.Tranction_Master_Id from AttachmentMaster  inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=AttachmentMaster.RelatedTableId inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId where IfilecabinetDocId='" + lnk1.Text + "'";

                SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
                DataTable ds58 = new DataTable();
                adp58.Fill(ds58);

                //ImageButton link1 = (ImageButton)gdr.FindControl("LinkButton4");
                //if (ds58.Rows.Count == 0)
                //{

                //    link1.ImageUrl = "~/Account/images/Red.jpg";

                //}
                //else
                //{
                //    link1.AlternateText = ds58.Rows.Count.ToString();

                //}
                LinkButton link1 = (LinkButton)gdr.FindControl("LinkButton4");
                LinkButton LinkButton6 = (LinkButton)gdr.FindControl("LinkButton6");
                //Label lbltid = (Label)gdr.FindControl("lbltid");

                if (ds58.Rows.Count == 0)
                {

                    link1.Text = "Make Entry";
                    LinkButton6.Visible = false;
                }
                else
                {
                    LinkButton6.Text = ds58.Rows[0]["Entry_Type_Name"].ToString() + ":" + ds58.Rows[0]["EntryNumber"].ToString();
                    LinkButton6.CommandArgument = ds58.Rows[0]["Tranction_Master_Id"].ToString();
                    link1.Text = " More";
                    LinkButton6.Visible = true;
                }

            }
            if (Request.QueryString["Tid"] != null)
            {
                Accdocadd.Visible = true;
                Gridreqinfo.Columns[15].Visible = true;
            }
            else
            {
                Gridreqinfo.Columns[15].Visible = false;
                Accdocadd.Visible = false;
            }
            if (chkflag.Checked == true)
            {
                fillflagdata();
                btnflagstatus.Visible = true;
                Gridreqinfo.Columns[15].Visible = true;
            }
            else
            {
                Gridreqinfo.Columns[15].Visible = false;
                btnflagstatus.Visible = false;
            }
        }

        //setGridisze();
    }
    protected void fillread(LinkButton docis, Label lblflag)
    {
        string strunread = "Unread";
        string strflg = "";
        lblflag.Text = "";
        DataTable dtrs = select("Select Distinct DocumentID,documentflagname from  DocumentFlag inner join Mydocumentflagstbl on Mydocumentflagstbl.Id=DocumentFlag.MyDocumentFlagID where DocumentID='" + docis.Text + "' and UserID='" + Session["UserId"] + "' and documentflagname<>'Unread' and Whid='" + ddlbusiness.SelectedValue + "'");
        foreach (DataRow item in dtrs.Rows)
        {
            if (Convert.ToString(item["documentflagname"]) == "Read")
            {
                strunread = "";
            }

            if (strflg.Length > 0)
            {
                strflg = strflg + ",";
            }
            strflg = strflg = strflg + Convert.ToString(item["documentflagname"]);


        }
        if (strunread.Length > 0)
        {
            if (strflg.Length > 0)
            {
                lblflag.Text = strunread + "," + strflg;
            }
            else
            {
                lblflag.Text = strunread;
            }
        }
        else
        {
            lblflag.Text = strflg;
        }
    }
    protected void fillflagdata()
    {
        DataTable dts = select("select Id,documentflagname from Mydocumentflagstbl where cid='" + Session["Comid"] + "' order by documentflagname");
        ddlflag.DataSource = dts;
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {

            DropDownList ddlflaga = (DropDownList)gdr.FindControl("ddlflag");
            ddlflaga.DataSource = dts;
            ddlflaga.DataTextField = "documentflagname";
            ddlflaga.DataValueField = "Id";
            ddlflaga.DataBind();
            ddlflaga.Items.Insert(0, "Select");
            ddlflaga.Items[0].Value = "0";
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
        DataTable dt = select(string.Format("SELECT Distinct * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));
        dt.Columns.Remove("ROW_NUM");
        return dt;

        // ViewState["dt"] = dt;
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {

        Session["GridRule"] = null;
        DataTable dtfill = new DataTable();
        DataTable dt = new DataTable();
        foreach (GridViewRow grd in Gridreqinfo.Rows)
        {
            CheckBox chksubbox = (CheckBox)grd.FindControl("chksubbox");
            if (chksubbox.Checked == true)
            {
                LinkButton LinkButton1 = (LinkButton)grd.FindControl("LinkButton1");
                int DocumentId = Convert.ToInt32(LinkButton1.Text);
                dt = clsDocument.SelectDoucmentMasterByIdAll(DocumentId);
                if (dt.Rows.Count > 0)
                {

                    if (Session["GridRule"] == null)
                    {
                        DataColumn dtcom1 = new DataColumn();
                        dtcom1.DataType = System.Type.GetType("System.String");
                        dtcom1.ColumnName = "DocumentId";
                        dtcom1.ReadOnly = false;
                        dtcom1.Unique = false;
                        dtcom1.AllowDBNull = true;

                        dtfill.Columns.Add(dtcom1);
                        DataColumn dtcom2 = new DataColumn();
                        dtcom2.DataType = System.Type.GetType("System.String");
                        dtcom2.ColumnName = "DocumentTitle";
                        dtcom2.ReadOnly = false;
                        dtcom2.Unique = false;
                        dtcom2.AllowDBNull = true;

                        dtfill.Columns.Add(dtcom2);

                        DataColumn dtcom3 = new DataColumn();
                        dtcom3.DataType = System.Type.GetType("System.String");
                        dtcom3.ColumnName = "PartyName";
                        dtcom3.ReadOnly = false;
                        dtcom3.Unique = false;
                        dtcom3.AllowDBNull = true;
                        dtfill.Columns.Add(dtcom3);

                        DataColumn dtcom4 = new DataColumn();
                        dtcom4.DataType = System.Type.GetType("System.String");
                        dtcom4.ColumnName = "DocumentType";
                        dtcom4.ReadOnly = false;
                        dtcom4.Unique = false;
                        dtcom4.AllowDBNull = true;

                        dtfill.Columns.Add(dtcom4);

                        DataColumn dtcom5 = new DataColumn();
                        dtcom5.DataType = System.Type.GetType("System.String");
                        dtcom5.ColumnName = "DocumentUploadDate";
                        dtcom5.ReadOnly = false;
                        dtcom5.Unique = false;
                        dtcom5.AllowDBNull = true;

                        dtfill.Columns.Add(dtcom5);
                    }
                    else
                    {
                        dtfill = (DataTable)Session["GridRule"];
                    }
                    DataRow dtrow = dtfill.NewRow();
                    dtrow["DocumentId"] = dt.Rows[0]["DocumentId"].ToString();
                    dtrow["DocumentTitle"] = dt.Rows[0]["DocumentTitle"].ToString();
                    dtrow["PartyName"] = dt.Rows[0]["PartyName"].ToString();
                    dtrow["DocumentType"] = dt.Rows[0]["DocumentType"].ToString();
                    dtrow["DocumentUploadDate"] = dt.Rows[0]["DocumentUploadDate"].ToString();
                    dtfill.Rows.Add(dtrow);
                    Session["GridRule"] = dtfill; ;
                }
            }
        }
        if (dtfill.Rows.Count > 0)
        {
            GridView2.DataSource = dtfill;
            GridView2.DataBind();
        }
        fillgrid();
        ModalPopupExtender3.Show();


    }
    protected void Gridreqinfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gridreqinfo.PageIndex = e.NewPageIndex;
        FillGrid(Convert.ToInt32(ViewState["state"]));
    }
    protected void Grdread(int DocumentId)
    {
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {

            LinkButton lnk1 = (LinkButton)gdr.Cells[3].FindControl("LinkButton1");
            DropDownList ddlflag = (DropDownList)gdr.FindControl("ddlflag");
            if (lnk1.Text.ToString() == DocumentId.ToString())
            {
                DataTable dtsta = select("Select * from Mydocumentflagstbl where documentflagname='Read' and  cid='" + Session["Comid"] + "'");
                if (dtsta.Rows.Count > 0)
                {
                    DataTable dtst = select("Select * from DocumentFlag where DocumentID='" + lnk1.Text + "' and Whid='" + ddlbusiness.SelectedValue + "' and  UserID='" + Session["UserId"] + "' and MyDocumentFlagID='" + dtsta.Rows[0]["Id"] + "'");
                    if (dtst.Rows.Count == 0)
                    {
                        string strc = "Insert into DocumentFlag(DocumentID,UserID,MyDocumentFlagID,Whid)Values('" + lnk1.Text + "','" + Session["UserId"] + "','" + dtsta.Rows[0]["Id"] + "','" + ddlbusiness.SelectedValue + "')";
                        SqlCommand cmd = new SqlCommand(strc, con);
                        if (con.State.ToString() != "")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Label lblflag = (Label)gdr.FindControl("lblflag");
                        fillread(lnk1, lblflag);
                    }
                }
            }
           
        }
    }
    protected void Gridreqinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "copy1")
        {
            int DocumentId = Convert.ToInt32(e.CommandArgument);
            Grdread(DocumentId);
            string te = "DocumentCopyPaste.aspx?id=" + DocumentId + "&&return=2";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            // Response.Redirect("DocumentCopyPaste.aspx?id=" + DocumentId + "&&return=2");
        }
        if (e.CommandName == "delete1")
        {
            int DocumentId = Convert.ToInt32(e.CommandArgument);
            hdncnfm.Value = DocumentId.ToString();
            // mdlpopupconfirm.Show();
            imgconfirmok_Click(sender, e);

        }


        if (e.CommandName == "associate")
        {
            int tid = Convert.ToInt32(e.CommandArgument);

            string st = "Select EntryTypeId, EntryTypeMaster.Entry_Type_Name,TranctionMaster.EntryNumber,AttachmentMaster.Datetime From AttachmentMaster inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=AttachmentMaster.RelatedTableId inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId where TranctionMaster.Tranction_Master_Id='" + tid + "' ";

            SqlCommand cm = new SqlCommand(st, con);
            SqlDataAdapter ad = new SqlDataAdapter(cm);
            DataTable d = new DataTable();
            ad.Fill(d);
            if (d.Rows.Count > 0)
            {
                string te = "";

                if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "33")
                {
                    te = "CashRegister.aspx?n12&trid=" + tid + "&eid=33";

                }
                else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "32")
                {
                    te = "CashRegister.aspx?n12&trid=" + tid + "&eid=32";

                }
                else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "2")
                {
                    te = "Cashreciept.aspx?n12&trid=" + tid + "&eid=2";

                }
                else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "1")
                {
                    te = "Cashpaymentnew.aspx?n12&trid=" + tid + "&eid=1";

                }
                else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "6" || Convert.ToString(d.Rows[0]["EntryTypeId"]) == "7")
                {
                    te = "crdrnoteaddbycompany.aspx?n12&EntryN=" + Convert.ToString(d.Rows[0]["EntryNumber"]) + "&EntryT=" + Convert.ToString(d.Rows[0]["EntryTypeId"]);

                }
                else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "3")
                {
                    te = "journalentrycrdrcompany.aspx?n12&Tid=" + tid.ToString();

                }
                else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "27")
                {
                    DataTable dsrc = select(" SELECT  distinct Purchase_Details_Id,  TranctionMaster.Tranction_Master_Id, TranctionMaster.EntryNumber " +
                          " FROM PurchaseDetails INNER JOIN  TranctionMaster ON PurchaseDetails.TransId = TranctionMaster.Tranction_Master_Id " +
                     " where PurchaseDetails.TransId = '" + tid + "'  and EntryTypeId='27'");
                    if (dsrc.Rows.Count > 0)
                    {
                        te = "EditPurchaseInvoice.aspx?n12&Purchase_Details_Id=" + dsrc.Rows[0]["Purchase_Details_Id"];
                    }
                }
                else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "30")
                {
                    te = "RetailInvoice_Edit.aspx?vn=n12&Tid=" + tid.ToString();

                }
                else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "26")
                {
                    DataTable drt = select("select TransactionMasterSalesChallanTbl.SalesChallanMasterId,SalesChallanDatetime,TransactionMasterId,SalesOrderMasterId from SalesChallanMaster inner join  TransactionMasterSalesChallanTbl ON TransactionMasterSalesChallanTbl.SalesChallanMasterId=SalesChallanMaster.SalesChallanMasterId where TransactionMasterId='" + tid.ToString() + "'");
                    if (drt.Rows.Count > 0)
                    {
                        te = "CustomerDCEdit.aspx?vn=n12&id1=" + Convert.ToString(drt.Rows[0]["SalesChallanMasterId"]);
                    }
                }
                else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "34")
                {
                    te = "ProductInvoiceReport.aspx?vn=n12&Tapid=" + tid.ToString();

                }
                else if (Convert.ToString(d.Rows[0]["EntryTypeId"]) == "4")
                {
                    DataTable dsrc = select(" SELECT  distinct Purchase_Details_Id,  TranctionMaster.Tranction_Master_Id, TranctionMaster.EntryNumber " +
                          " FROM PurchaseDetails INNER JOIN  TranctionMaster ON PurchaseDetails.TransId = TranctionMaster.Tranction_Master_Id " +
                     " where PurchaseDetails.TransId = '" + tid + "'  and EntryTypeId='4'");
                    if (dsrc.Rows.Count > 0)
                    {
                        te = "ExpenseInvoice.aspx?n12&Purchase_Details_Id=" + dsrc.Rows[0]["Purchase_Details_Id"];
                    }
                }
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

            }
        }
        if (e.CommandName == "more")
        {

            int DocumentId = Convert.ToInt32(e.CommandArgument);

            ViewState["docid"] = DocumentId.ToString();
            ViewState["ID"] = DocumentId.ToString();
            fillpop(ViewState["ID"].ToString());
            ModalPopupExtender4.Show();
        }
        if (e.CommandName == "openfolder")
        {
            int DocumentId = Convert.ToInt32(e.CommandArgument);

            string te = "DocumentRelatedFolders.aspx?id=" + DocumentId;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

            // Response.Redirect("DocumentRelatedFolders.aspx?id=" + DocumentId + "");
        }
        if (e.CommandName == "Editview")
        {
            int docis = Convert.ToInt32(e.CommandArgument);
            Grdread(Convert.ToInt32(e.CommandArgument));
            string te = "DocumentEditAndView.aspx?id=" + e.CommandArgument + "&Did" + Session["DesignationId"];
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        
        }
        if (e.CommandName == "Send")
        {
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDoucmentMasterByID(Convert.ToInt32(e.CommandArgument));
            string docname = dt.Rows[0]["DocumentName"].ToString();
            string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);
            string name = docname.Trim();
            string extension = name.Substring(name.Length - 3);
            //if (Convert.ToString(extension) == "pdf")
            //{
            Session["ABCDE"] = "ABCDE";

            //                    string popupScript = "<script language='javascript'>" +
            //"newWindow=window.open('ViewDocument.aspx?id='" + e.CommandArgument + ", 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" + "</script>";


            //                    Page.RegisterClientScriptBlock("newWindow", popupScript);
            //LinkButton lnkbtn = (LinkButton)Gridreqinfo.FindControl("LinkButton1");
            //lnkbtn.Attributes.Add("onclick", "window.open('ViewDocument.aspx?id='" + e.CommandArgument + ",, 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')");

            Grdread(Convert.ToInt32(e.CommandArgument));
            string te = "ViewDocument.aspx?id=" + e.CommandArgument + "&Siddd=VHDS";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
           // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('http://license.busiwiz.com/Account/Jobcenter/UploadedDocuments/"+docname+"');", true);

            // Response.Redirect("ViewDocument.aspx?id=" + e.CommandArgument + "&Siddd=VHDS");
            //}
            //else
            //{
            //    FileInfo file = new FileInfo(filepath);

            //    if (file.Exists)
            //    {
            //        Response.ClearContent();
            //        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            //        Response.AddHeader("Content-Length", file.Length.ToString());
            //        Response.ContentType = ReturnExtension(file.Extension.ToLower());
            //        Response.TransmitFile(file.FullName);

            //        Response.End();

            //    }
            //}
        }
    }

    public void fillgrid()
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentFolderByEmpId(Convert.ToInt32(Session["EmployeeId"]));
        Gridreqinfos.DataSource = dt;
        Gridreqinfos.DataBind();

        foreach (GridViewRow gdr in Gridreqinfos.Rows)
        {

            int folderid = Convert.ToInt32(Gridreqinfos.DataKeys[gdr.RowIndex].Value);
            DataTable dt1 = new DataTable();
            dt1 = clsDocument.SelectDoucmentTotalInFolder(folderid);

            string name = gdr.Cells[1].Text;
            gdr.Cells[1].Text = name.ToString() + "(" + dt1.Rows[0]["total"].ToString() + ")";
        }
        check();
        setGridisze();
    }
    public void check()
    {
        foreach (GridViewRow gdr in Gridreqinfos.Rows)
        {
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDoucmentFolderRelation(Convert.ToInt32(Gridreqinfos.DataKeys[gdr.RowIndex].Value), Convert.ToInt32(ViewState["docid"]));
            if (dt.Rows.Count > 0)
            {
                CheckBox ch = (CheckBox)gdr.Cells[0].FindControl("CheckBox1");
                ch.Checked = true;

            }

        }
    }
    protected void Gridreqinfos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "detail")
        {
            foreach (GridViewRow gdr in Gridreqinfos.Rows)
            {
                GridView gr = (GridView)gdr.Cells[3].FindControl("GridView1");
                gr.DataSource = null;
                gr.DataBind();
            }

            Gridreqinfos.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int folderid = Convert.ToInt32(Gridreqinfos.SelectedDataKey.Value);
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDoucmentRelationByFolderId(folderid);
            GridView gridrelatedDoc = (GridView)Gridreqinfos.Rows[Gridreqinfos.SelectedIndex].Cells[1].FindControl("GridView1");
            gridrelatedDoc.DataSource = dt;
            gridrelatedDoc.DataBind();
            ModalPopupExtender3.Show();
        }
    }
    protected void Gridreqinfos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gridreqinfos.PageIndex = e.NewPageIndex;
        fillgrid();
    }



    protected void FillParty()
    {



        string str = "SELECT Party_master.PartyId,Left(PartytTypeMaster.PartType,15)+':'+Left(Party_master.Compname,25)+':'+Left(Party_master.Contactperson,25) as PartyName FROM [User_master]  inner join Party_master  on Party_master.PartyID=User_master.PartyID  inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId  where Party_master.id='" + Session["comid"].ToString() + "'  and Party_master.Whid='" + ddlbusiness.SelectedValue + "' order by PartytTypeMaster.PartType,Party_master.Compname,Party_master.Contactperson  ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlParty.DataSource = dt;
        ddlParty.DataTextField = "PartyName";
        ddlParty.DataValueField = "PartyId";
        ddlParty.DataBind();
        ddlParty.Items.Insert(0, "-select-");
        ddlParty.Items[0].Value = "0";
    }
    public void setGridisze()
    {
        // doc grid
        if (Gridreqinfo.Rows.Count == 0)
        {
            Panel2.CssClass = "GridPanel20";
        }
        else if (Gridreqinfo.Rows.Count == 1)
        {
            Panel2.CssClass = "GridPanel125";
        }
        else if (Gridreqinfo.Rows.Count == 2)
        {
            Panel2.CssClass = "GridPanel150";
        }
        else if (Gridreqinfo.Rows.Count == 3)
        {
            Panel2.CssClass = "GridPanel175";
        }
        else if (Gridreqinfo.Rows.Count == 4)
        {
            Panel2.CssClass = "GridPanel200";
        }
        else if (Gridreqinfo.Rows.Count == 5)
        {
            Panel2.CssClass = "GridPanel225";
        }
        else if (Gridreqinfo.Rows.Count == 6)
        {
            Panel2.CssClass = "GridPanel250";
        }
        else if (Gridreqinfo.Rows.Count == 7)
        {
            Panel2.CssClass = "GridPanel275";
        }
        else if (Gridreqinfo.Rows.Count == 8)
        {
            Panel2.CssClass = "GridPanel";
        }
        else if (Gridreqinfo.Rows.Count == 9)
        {
            Panel2.CssClass = "GridPanel325";
        }
        else if (Gridreqinfo.Rows.Count == 10)
        {
            Panel2.CssClass = "GridPanel350";
        }

        else
        {
            Panel2.CssClass = "GridPanel375";
        }


    }
    protected void chkbox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkbox = (CheckBox)Gridreqinfo.HeaderRow.FindControl("chkbox");
        if (chkbox.Checked == true)
        {
            foreach (GridViewRow grd in Gridreqinfo.Rows)
            {
                CheckBox chksubbox = (CheckBox)grd.FindControl("chksubbox");
                if (chksubbox.Enabled == true)
                {

                    chksubbox.Checked = true;
                }
            }
        }
        else
        {
            foreach (GridViewRow grd in Gridreqinfo.Rows)
            {
                CheckBox chksubbox = (CheckBox)grd.FindControl("chksubbox");
                chksubbox.Checked = false;
            }
        }

        chksubbox_checkedChanged(sender, e);
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            pnldate.Visible = true;
        }
        else
        {
            pnldate.Visible = false;
        }
    }
    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {
        btncopydocument.Visible = false;
        btnsendemail.Visible = false;
        btnsendmsg.Visible = false;
        btnmyfolder.Visible = false;

        lblmsg.Text = "";
        columndisplay();


        lblcomname.Text = ddlbusiness.SelectedItem.Text;
        if (CheckBox1.Checked == true)
        {
            lbldate.Visible = true;
            lbldate.Text = RadioButtonList1.SelectedItem.Text + " : " + txtfrom.Text + " To " + txtto.Text;
        }
        else
        {
            lbldate.Visible = false;
        }
        int condition = Convert.ToInt32(ViewState["state"].ToString());
        if (condition == 1)
        {
            lblserchcabi.Text = "Search by :" + txtDocId.Text;
            lblserchdrowe.Visible = false;
            lblserfolder.Visible = false;

            if (txtDocId.Text.Trim().Length <= 0)
            {
                lblmsg.Visible = true;

                lblmsg.Text = "Please enter document ID";
                txtDocId.Focus();
                return;
            }
        }
        else if (condition == 2)
        {

        }
        else if (condition == 3)
        {
            lblserchcabi.Text = "Cabinet :" + ddlmaindotype.SelectedItem.Text;
            lblserchdrowe.Visible = true;
            lblserfolder.Visible = true;
            lblserchdrowe.Text = "Drawer :" + ddlsubdoctype.SelectedItem.Text;
            lblserfolder.Text = "Folder :" + ddldoctype.SelectedItem.Text;


        }

        else if (condition == 4)
        {
            lblserchcabi.Text = "Search by :" + ddlParty.SelectedItem.Text;
            lblserchdrowe.Visible = false;
            lblserfolder.Visible = false;
            if (ddlParty.SelectedIndex <= 0)
            {
                lblmsg.Visible = true;
                // pnlmsg.Visible = true;
                lblmsg.Text = "Please select party name";
                ddlParty.Focus();
                return;
            }
        }
        FillGrid(condition);
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (RadioButtonList2.SelectedItem.Text == "Document ID")
        //{
        //    txtDocId.Visible = true;
        //   // txtDocTitle.Visible = false;
        //   // txtDocTitle.Text = "";
        //    ViewState["state"] = "1";
        //}
        //else if (RadioButtonList2.SelectedItem.Text == "Document Title")
        //{
        //    txtDocId.Visible = false;
        //    txtDocTitle.Visible = true;
        //    txtDocId.Text = "";
        //    ViewState["state"] = "2";
        //}
    }
    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        Int32 rst = clsDocument.InsertDocumentFolder(txtFoldername.Text, Convert.ToInt32(Session["EmployeeId"]));
        if (rst > 0)
        {
            //  pnlmsg.Visible = true;
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";

            txtFoldername.Text = "";
            Panel1.Visible = false;
            fillgrid();
        }
    }
    protected void imagebtnSubmit_Click(object sender, EventArgs e)
    {


        foreach (GridViewRow mains in GridView2.Rows)
        {
            ViewState["docid"] = Convert.ToString(GridView2.DataKeys[mains.RowIndex].Value);

            foreach (GridViewRow gdr in Gridreqinfos.Rows)
            {
                CheckBox ch = (CheckBox)gdr.Cells[0].FindControl("CheckBox1");
                if (ch.Checked == true)
                {
                    DataTable dt2 = new DataTable();
                    dt2 = clsDocument.SelectDoucmentFolderRelation(Convert.ToInt32(Gridreqinfos.DataKeys[gdr.RowIndex].Value), Convert.ToInt32(ViewState["docid"]));
                    if (dt2.Rows.Count == 0)
                    {
                        int folderid = Convert.ToInt32(Gridreqinfos.DataKeys[gdr.RowIndex].Value);
                        Int32 rst = clsDocument.InsertDoucmentRelationMaster(Convert.ToInt32(ViewState["docid"]), folderid);
                        if (rst > 0)
                        {
                            //  pnlmsg.Visible = true;
                            lblmsg.Visible = true;
                            lblmsg.Text = "Data is inserted Successfully.";

                        }
                    }
                }
                else
                {
                    DataTable dt1 = new DataTable();
                    dt1 = clsDocument.SelectDoucmentFolderRelation(Convert.ToInt32(Gridreqinfos.DataKeys[gdr.RowIndex].Value), Convert.ToInt32(ViewState["docid"]));
                    if (dt1.Rows.Count > 0)
                    {
                        int realtionid = Convert.ToInt32(dt1.Rows[0]["RelationID"]);
                        bool k = clsDocument.DeleteDocumentFolderRelation(realtionid);
                    }


                }


            }
        }
        fillgrid();
        Panel1.Visible = false;
        imgbtnsubmit_Click(sender, e);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        ModalPopupExtender3.Show();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void imgconfirmok_Click(object sender, EventArgs e)
    {

        int rst = clsDocument.DeleteDocumentMasterByID(Convert.ToInt32(hdncnfm.Value));

        int rst1 = clsDocument.InsertDocumentLog(Convert.ToInt32(hdncnfm.Value), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, true, false, false, false, false, false, false);
        if (rst > 0)
        {

            lblmsg.Visible = true;
            lblmsg.Text = "Document deleted successfully";
            // FillGrid(Convert.ToInt32(ViewState["state"]));
            imgbtnsubmit_Click(sender, e);
        }
        mdlpopupconfirm.Hide();
    }


    protected void LinkButton4_Click(object sender, EventArgs e)
    {

    }

    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string eeed = " Select distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.DesignationMasterId,User_master.DesigantionMasterId as did from EmployeeMaster inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join User_master on User_master.PartyID=Party_master.PartyID  inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where PartytTypeMaster.PartType='Admin' and EmployeeMaster.Whid='" + ddlbusiness.SelectedValue + "'";
        //SqlCommand cmdeeed = new SqlCommand(eeed, con);
        //SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        //DataTable dteeed = new DataTable();
        //adpeeed.Fill(dteeed);
        //if (dteeed.Rows.Count > 0)
        //{
        //    Session["EmployeeId"] = Convert.ToString(dteeed.Rows[0]["EmployeeMasterID"]);
        //    Session["DesignationId"] = Convert.ToString(dteeed.Rows[0]["did"]);
        //    DesignationId = Convert.ToInt32(Session["DesignationId"]);
        //}
        if (ddlSearchby.SelectedValue == "1")
        {
            Fillddldocmaintype();
        }
        else if (ddlSearchby.SelectedValue == "2")
        {
            FillParty();
        }
    }
    protected void Fillddldocmaintype()
    {
        //string str132 = " SELECT [DocumentMainTypeId],Left(DocumentMainType,25) as DocumentMainType  FROM  [dbo].[DocumentMainType] where Whid='" + ddlbusiness.SelectedValue + "'";

        string str132 = "select Distinct DocumentMainType.DocumentMainTypeId,Left(DocumentMainType.DocumentMainType,25) as DocumentMainType from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentType.DocumentTypeId In( SELECT  Distinct  DocumentTypeId FROM  DocumentAccessRightMaster WHERE     (CID='" + Session["Comid"].ToString() + "' ) and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')  or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))) and  DocumentMainType.CID='" + Session["Comid"].ToString() + "' and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' order by DocumentMainType ";

        //string str132 = "select Distinct DocumentMainType.DocumentMainTypeId,Left(DocumentMainType.DocumentMainType,25) as DocumentMainType from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentType.DocumentTypeId In( SELECT  Distinct  DocumentTypeId FROM  DocumentAccessRightMaster WHERE     (DesignationId In(Select DesignationMaster.DesignationMasterId from DesignationMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptID where Whid='" + ddlbusiness.SelectedValue + "' )) and (CID='" + Session["Comid"].ToString() + "' ) and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')  or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))) and  DocumentMainType.CID='" + Session["Comid"].ToString() + "' and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' order by DocumentMainType ";

        SqlCommand cgw = new SqlCommand(str132, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);

        DataTable dt = new DataTable();

        adgw.Fill(dt);

        ddlmaindotype.DataSource = dt;
        //ddlmaindotype.DataTextField = "DocumentMainType";
        //ddlmaindotype.DataValueField = "DocumentMainTypeId";
        ddlmaindotype.DataBind();

        ddlmaindotype.Items.Insert(0, "All");
        ddlmaindotype.Items[0].Value = "0";


        EventArgs e = new EventArgs();
        object sender = new object();
        ddlmaindotype_SelectedIndexChanged(sender, e);



    }
    protected void ddlmaindotype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlsubdoctype.Items.Clear();


        FillDocumentSubTypeWithMainType(Int32.Parse(ddlmaindotype.SelectedValue.ToString()));
    }
    protected void FillDocumentSubTypeWithMainType(Int32 DocumentMainTypeId)
    {

        ddlsubdoctype.Items.Clear();
        string str178 = " select Distinct DocumentSubType.DocumentSubTypeId, Left(DocumentSubType.DocumentSubType,25) as DocumentSubType, DocumentMainType.DocumentMainTypeId as DocumentMainTypeId,  DocumentMainType.DocumentMainType from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentType.DocumentTypeId In( SELECT  Distinct  DocumentTypeId FROM  DocumentAccessRightMaster WHERE      (CID='" + Session["Comid"].ToString() + "' ) and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')  or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))) and  DocumentMainType.CID='" + Session["Comid"].ToString() + "' and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and  DocumentMainType.DocumentMainTypeId = '" + DocumentMainTypeId + "' order by DocumentSubType  ";

        //string str178 = " SELECT     DocumentSubType.DocumentSubTypeId, Left(DocumentSubType.DocumentSubType,25) as DocumentSubType, DocumentMainType.DocumentMainTypeId as DocumentMainTypeId,  DocumentMainType.DocumentMainType FROM         DocumentMainType RIGHT OUTER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId WHERE     (DocumentMainType.DocumentMainTypeId = '" + DocumentMainTypeId + "') order by DocumentSubType.DocumentSubType ";
        //string str132 = "select Distinct DocumentMainType.DocumentMainTypeId,Left(DocumentMainType.DocumentMainType,25) as DocumentMainType from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentType.DocumentTypeId In( SELECT  Distinct  DocumentTypeId FROM  DocumentAccessRightMaster WHERE     (DesignationId In(Select DesignationMaster.DesignationMasterId from DesignationMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptID where Whid='" + ddlbusiness.SelectedValue + "' )) and (CID='" + Session["Comid"].ToString() + "' ) and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')  or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))) and  DocumentMainType.CID='" + Session["Comid"].ToString() + "' and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' order by DocumentMainType ";
        SqlCommand cgw = new SqlCommand(str178, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);

        DataTable dt = new DataTable();

        adgw.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlsubdoctype.DataSource = dt;
            ddlsubdoctype.DataBind();
        }
        ddlsubdoctype.Items.Insert(0, "All");
        ddlsubdoctype.SelectedItem.Value = "0";
        FillDocumentTypeWithSubType(Int32.Parse(ddlmaindotype.SelectedValue.ToString()));
    }
    protected void ddlsubdoctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsubdoctype.SelectedIndex > 0)
        {
            FillDocumentTypeWithSubType(Int32.Parse(ddlsubdoctype.SelectedValue.ToString()));
        }
    }
    protected void FillDocumentTypeWithSubType(Int32 DocumentSubTypeId)
    {

        ddldoctype.Items.Clear();
        //string str132 = "SELECT     DocumentType.DocumentTypeId, Left(DocumentType.DocumentType,25) as  DocumentType  FROM         DocumentType WHERE     (DocumentType.DocumentSubTypeId = '" + ddlsubdoctype.SelectedValue + "') order by DocumentType.DocumentType";
        string str132 = "select Distinct DocumentType.DocumentTypeId, Left(DocumentType.DocumentType,25) as  DocumentType from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentType.DocumentTypeId In( SELECT  Distinct  DocumentTypeId FROM  DocumentAccessRightMaster WHERE    (CID='" + Session["Comid"].ToString() + "' ) and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')  or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))) and  DocumentMainType.CID='" + Session["Comid"].ToString() + "' and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and  DocumentType.DocumentSubTypeId = '" + ddlsubdoctype.SelectedValue + "' order by DocumentType ";
        SqlCommand cgw = new SqlCommand(str132, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);

        DataTable dt = new DataTable();

        adgw.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddldoctype.DataSource = dt;
            ddldoctype.DataBind();
        }
        ddldoctype.Items.Insert(0, "All");
        ddldoctype.SelectedItem.Value = "0";

    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {


        string te = "";
        if (ddloa.SelectedIndex == 0)
        {

            te = "Cashpaymentnew.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];


        }
        else if (ddloa.SelectedIndex == 1)
        {
            te = "Cashreciept.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 2)
        {
            te = "journalentrycrdrcompany.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 3)
        {
            te = "crdrnoteaddbycompany.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 4)
        {
            te = "crdrnoteaddbycompany.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 5)
        {
            te = "CustomerDeliveryChallan3.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 6)
        {
            te = "purchaseinvoice.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 7)
        {
            te = "RetailCustomerDeliveryChallan_new.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddloa.SelectedIndex == 8)
        {
            te = "ExpenseInvoice.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void Img2_Click(object sender, EventArgs e)
    {


        string te = "";
        if (ddldo.SelectedValue == "1" || ddldo.SelectedValue == "2")
        {

            te = "cashregister.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];


        }
        //else if (ddldo.SelectedIndex == 1)
        //{
        //    te = "Registerformpayment.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        //}
        else if (ddldo.SelectedValue == "2")
        {
            te = "LedgerJour_New.aspx?docid=" + ViewState["ID"];

            hypost.NavigateUrl = "LedgerJour_New.aspx?docid=" + ViewState["ID"];
        }
        else if (ddldo.SelectedValue == "3")
        {
            te = "Ledgercrdr.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];


        }
        else if (ddldo.SelectedValue == "4")
        {
            te = "Register_Deliverychallan.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedValue == "5")
        {
            te = "register_purchase.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedValue == "6")
        {
            te = "Register_sales.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedValue == "7")
        {
            te = "register_salesorder.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        else if (ddldo.SelectedValue == "8")
        {
            te = "Register_Expense.aspx?docid=" + ViewState["ID"] + "&ici=" + Session["Comid"];

        }
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    public void fillpop(string docid)
    {



        string str = "select DocumentMaster.DocumentId,DocumentMaster.DocumentTitle from DocumentMaster where DocumentMaster.DocumentId = '" + docid + "'";
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






    }
    protected void gridpopup_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        ModalPopupExtender4.Show();
        //  mdloa.Show();
    }
    protected void chkboxcopy_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkboxcopy = (CheckBox)Gridreqinfo.HeaderRow.FindControl("chkboxcopy");
        if (chkboxcopy.Checked == true)
        {

            foreach (GridViewRow grd in Gridreqinfo.Rows)
            {

                CheckBox chkcopy = (CheckBox)grd.FindControl("chkcopy");
                if (chkcopy.Enabled == true)
                {
                    chkcopy.Checked = true;
                }
            }
        }
        else
        {
            foreach (GridViewRow grd in Gridreqinfo.Rows)
            {
                CheckBox chkcopy = (CheckBox)grd.FindControl("chkcopy");
                chkcopy.Checked = false;
            }
        }
        chkcopy_CheckedChanged(sender, e);
    }
    protected void linkcopy_Click(object sender, EventArgs e)
    {
        Session["grdcopy"] = null;
        int DocumentId = 0;
        Session["grdcopy"] = null;
        DataTable dtfill = new DataTable();
        DataTable dt = new DataTable();
        foreach (GridViewRow grd in Gridreqinfo.Rows)
        {
            CheckBox chkcopy = (CheckBox)grd.FindControl("chkcopy");
            if (chkcopy.Checked == true)
            {
                LinkButton LinkButton1 = (LinkButton)grd.FindControl("LinkButton1");
                DocumentId = Convert.ToInt32(LinkButton1.Text);
                dt = clsDocument.SelectDoucmentMasterByIdAll(DocumentId);
                if (dt.Rows.Count > 0)
                {

                    if (Session["grdcopy"] == null)
                    {
                        DataColumn dtcom1 = new DataColumn();
                        dtcom1.DataType = System.Type.GetType("System.String");
                        dtcom1.ColumnName = "DocumentId";
                        dtcom1.ReadOnly = false;
                        dtcom1.Unique = false;
                        dtcom1.AllowDBNull = true;

                        dtfill.Columns.Add(dtcom1);
                        DataColumn dtcom2 = new DataColumn();
                        dtcom2.DataType = System.Type.GetType("System.String");
                        dtcom2.ColumnName = "DocumentTitle";
                        dtcom2.ReadOnly = false;
                        dtcom2.Unique = false;
                        dtcom2.AllowDBNull = true;

                        dtfill.Columns.Add(dtcom2);

                        DataColumn dtcom3 = new DataColumn();
                        dtcom3.DataType = System.Type.GetType("System.String");
                        dtcom3.ColumnName = "PartyName";
                        dtcom3.ReadOnly = false;
                        dtcom3.Unique = false;
                        dtcom3.AllowDBNull = true;
                        dtfill.Columns.Add(dtcom3);

                        DataColumn dtcom4 = new DataColumn();
                        dtcom4.DataType = System.Type.GetType("System.String");
                        dtcom4.ColumnName = "DocumentType";
                        dtcom4.ReadOnly = false;
                        dtcom4.Unique = false;
                        dtcom4.AllowDBNull = true;

                        dtfill.Columns.Add(dtcom4);

                        DataColumn dtcom5 = new DataColumn();
                        dtcom5.DataType = System.Type.GetType("System.String");
                        dtcom5.ColumnName = "DocumentUploadDate";
                        dtcom5.ReadOnly = false;
                        dtcom5.Unique = false;
                        dtcom5.AllowDBNull = true;

                        dtfill.Columns.Add(dtcom5);


                        DataColumn dtcom6 = new DataColumn();
                        dtcom6.DataType = System.Type.GetType("System.String");
                        dtcom6.ColumnName = "DocumentMainType";
                        dtcom6.ReadOnly = false;
                        dtcom6.Unique = false;
                        dtcom6.AllowDBNull = true;

                        dtfill.Columns.Add(dtcom6);

                        DataColumn dtcom7 = new DataColumn();
                        dtcom7.DataType = System.Type.GetType("System.String");
                        dtcom7.ColumnName = "DocumentSubType";
                        dtcom7.ReadOnly = false;
                        dtcom7.Unique = false;
                        dtcom7.AllowDBNull = true;

                        dtfill.Columns.Add(dtcom7);
                    }
                    else
                    {
                        dtfill = (DataTable)Session["grdcopy"];
                    }
                    DataRow dtrow = dtfill.NewRow();
                    dtrow["DocumentId"] = dt.Rows[0]["DocumentId"].ToString();
                    dtrow["DocumentTitle"] = dt.Rows[0]["DocumentTitle"].ToString();
                    dtrow["PartyName"] = dt.Rows[0]["PartyName"].ToString();
                    dtrow["DocumentMainType"] = dt.Rows[0]["DocumentMainType"].ToString();
                    dtrow["DocumentSubType"] = dt.Rows[0]["DocumentSubType"].ToString();
                    dtrow["DocumentType"] = dt.Rows[0]["DocumentType"].ToString();
                    dtrow["DocumentUploadDate"] = dt.Rows[0]["DocumentUploadDate"].ToString();
                    dtfill.Rows.Add(dtrow);
                    Session["grdcopy"] = dtfill; ;
                }
            }
        }
        if (DocumentId > 0)
        {
            string te = "DocumentCopyPaste.aspx?id=" + DocumentId + "&&return=2";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }




    }


    protected void chkboxmsg_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkboxmsg = (CheckBox)Gridreqinfo.HeaderRow.FindControl("chkboxmsg");

        if (chkboxmsg.Checked == true)
        {

            foreach (GridViewRow grd in Gridreqinfo.Rows)
            {

                CheckBox chkmsg = (CheckBox)grd.FindControl("chkmsg");
                if (chkmsg.Enabled == true)
                {
                    chkmsg.Checked = true;
                }
            }
        }
        else
        {
            foreach (GridViewRow grd in Gridreqinfo.Rows)
            {
                CheckBox chkmsg = (CheckBox)grd.FindControl("chkmsg");
                chkmsg.Checked = false;
            }
        }
        chkmsg_CheckedChanged(sender, e);
    }
    protected void linkmsg_Click(object sender, EventArgs e)
    {
        Session["did"] = null;
        string DocumentId = "";

        DataTable dtfill = new DataTable();
        DataTable dt = new DataTable();
        foreach (GridViewRow grd in Gridreqinfo.Rows)
        {
            CheckBox chkmsg = (CheckBox)grd.FindControl("chkmsg");
            if (chkmsg.Checked == true)
            {
                LinkButton LinkButton1 = (LinkButton)grd.FindControl("LinkButton1");

                DocumentId += Convert.ToString(LinkButton1.Text) + ",";

            }
        }
        if (DocumentId.Length > 0)
        {
            DocumentId = DocumentId.Remove(DocumentId.Length - 1, 1);
            Session["did"] = DocumentId.ToString();
            string te = "MessageCompose.aspx?wid=" + ddlbusiness.SelectedValue + "&&ret=2";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }




    }

    protected void chkboxmail_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkboxmail = (CheckBox)Gridreqinfo.HeaderRow.FindControl("chkboxmail");
        if (chkboxmail.Checked == true)
        {

            foreach (GridViewRow grd in Gridreqinfo.Rows)
            {

                CheckBox chkmail = (CheckBox)grd.FindControl("chkmail");
                if (chkmail.Enabled == true)
                {
                    chkmail.Checked = true;
                }
            }
        }
        else
        {
            foreach (GridViewRow grd in Gridreqinfo.Rows)
            {
                CheckBox chkmail = (CheckBox)grd.FindControl("chkmail");
                chkmail.Checked = false;
            }
        }
        chkmail_CheckedChanged(sender, e);
    }
    protected void linkmail_Click(object sender, EventArgs e)
    {
        Session["did"] = null;
        string DocumentId = "";

        DataTable dtfill = new DataTable();
        DataTable dt = new DataTable();
        foreach (GridViewRow grd in Gridreqinfo.Rows)
        {
            CheckBox chkmail = (CheckBox)grd.FindControl("chkmail");
            if (chkmail.Checked == true)
            {
                LinkButton LinkButton1 = (LinkButton)grd.FindControl("LinkButton1");

                DocumentId += Convert.ToString(LinkButton1.Text) + ",";

            }
        }
        if (DocumentId.Length > 0)
        {
            DocumentId = DocumentId.Remove(DocumentId.Length - 1, 1);
            Session["did"] = DocumentId.ToString();
            string te = "MessageComposeExt.aspx?wid=" + ddlbusiness.SelectedValue + "&&ret=2";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }




    }
    protected void Accdocadd_Click(object sender, EventArgs e)
    {
        inserdocatt();

    }
    public void inserdocatt()
    {


        int k = 0;

        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            CheckBox chk1 = (CheckBox)gdr.FindControl("chkaccentry");
            //int tid = Convert.ToInt32(gridDCregister.DataKeys[k].Value);
            LinkButton LinkButton1 = (LinkButton)gdr.FindControl("LinkButton1");
            int docid = Convert.ToInt32(LinkButton1.Text);

            k = k + 1;
            if (chk1.Checked == true)
            {
                string sqlselectr = "select * from AttachmentMaster where RelatedTableId='" + ViewState["tid"] + "' and IfilecabinetDocId='" + docid + "'";
                SqlDataAdapter adptr = new SqlDataAdapter(sqlselectr, con);
                DataTable dtptr = new DataTable();
                adptr.Fill(dtptr);
                if (dtptr.Rows.Count <= 0)
                {
                    string sqlselect = "select * from DocumentMaster where DocumentId='" + docid + "'";
                    SqlDataAdapter adpt = new SqlDataAdapter(sqlselect, con);
                    DataTable dtpt = new DataTable();
                    adpt.Fill(dtpt);
                    if (dtpt.Rows.Count > 0)
                    {

                        SqlCommand cmdi = new SqlCommand("InsertAttachmentMaster", con);

                        cmdi.CommandType = CommandType.StoredProcedure;
                        cmdi.Parameters.Add(new SqlParameter("@Titlename", SqlDbType.NVarChar));
                        cmdi.Parameters["@Titlename"].Value = dtpt.Rows[0]["DocumentTitle"].ToString();
                        cmdi.Parameters.Add(new SqlParameter("@Filename", SqlDbType.NVarChar));
                        cmdi.Parameters["@Filename"].Value = dtpt.Rows[0]["DocumentName"].ToString();

                        cmdi.Parameters.Add(new SqlParameter("@Datetime", SqlDbType.DateTime));
                        cmdi.Parameters["@Datetime"].Value = dtpt.Rows[0]["DocumentUploadDate"].ToString(); ;
                        cmdi.Parameters.Add(new SqlParameter("@RelatedtablemasterId", SqlDbType.NVarChar));
                        cmdi.Parameters["@RelatedtablemasterId"].Value = "5";
                        cmdi.Parameters.Add(new SqlParameter("@RelatedTableId", SqlDbType.NVarChar));
                        cmdi.Parameters["@RelatedTableId"].Value = ViewState["tid"];
                        cmdi.Parameters.Add(new SqlParameter("@IfilecabinetDocId", SqlDbType.NVarChar));
                        cmdi.Parameters["@IfilecabinetDocId"].Value = dtpt.Rows[0]["DocumentId"].ToString();
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
                }
            }
        }
        if (k > 0)
        {
            lblmsg.Text = "Record Inserted Successfully";
            lblmsg.Visible = true;
            //  pnlmsg.Visible = true;
            EventArgs e = new EventArgs();
            object sender = new object();
            imgbtnsubmit_Click(sender, e);
        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/AccEntryDocUp.aspx?tid=" + ViewState["tid"] + "");

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
    protected void Gridreqinfo_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        imgbtnsubmit_Click(sender, e);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Print and Export")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button2.Text = "Hide Print and Export";
            Button7.Visible = true;

            if (Gridreqinfo.Columns[16].Visible == true)
            {
                ViewState["editHide"] = "tt";
                Gridreqinfo.Columns[16].Visible = false;
            }
            if (Gridreqinfo.Columns[17].Visible == true)
            {
                ViewState["delHide"] = "tt";
                Gridreqinfo.Columns[17].Visible = false;
            }
            ddlExport.Visible = true;

            Gridreqinfo.AllowPaging = false;
            Gridreqinfo.PageSize = 100000;
            imgbtnsubmit_Click(sender, e);
            Gridreqinfo.Columns[9].Visible = false;
            Gridreqinfo.Columns[11].Visible = false;
            Gridreqinfo.Columns[12].Visible = false;
            Gridreqinfo.Columns[13].Visible = false;
            //CheckBox chkbox = (Gridreqinfo.HeaderRow.FindControl("chkbox") as CheckBox);
            //CheckBox chkboxmsg = (Gridreqinfo.HeaderRow.FindControl("chkboxmsg") as CheckBox);
            //CheckBox chkboxmail = (Gridreqinfo.HeaderRow.FindControl("chkboxmail") as CheckBox);
            //CheckBox chkboxcopy = (Gridreqinfo.HeaderRow.FindControl("chkboxcopy") as CheckBox);

            //chkbox.Enabled = false;
            //chkboxmsg.Enabled = false;
            //chkboxmail.Enabled = false;
            //chkboxcopy.Enabled = false;

            //foreach (GridViewRow gdr in Gridreqinfo.Rows)
            //{
            //    CheckBox chksubbox = (CheckBox)gdr.FindControl("chksubbox");
            //    CheckBox chkmsg = (CheckBox)gdr.FindControl("chkmsg");
            //    CheckBox chkmail = (CheckBox)gdr.FindControl("chkmail");
            //    CheckBox chkcopy = (CheckBox)gdr.FindControl("chkcopy");


            //    chksubbox.Enabled = false;
            //    chkmsg.Enabled = false;
            //    chkmail.Enabled = false;
            //    chkcopy.Enabled = false;
            //}

        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(400);

            Button2.Text = "Print and Export";
            Button7.Visible = false;

            if (ViewState["editHide"] != null)
            {
                Gridreqinfo.Columns[16].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                Gridreqinfo.Columns[17].Visible = true;
            }

            ddlExport.Visible = false;

            Gridreqinfo.AllowPaging = true;
            Gridreqinfo.PageSize = 25;

            Gridreqinfo.Columns[9].Visible = true;
            Gridreqinfo.Columns[11].Visible = true;
            Gridreqinfo.Columns[12].Visible = true;
            Gridreqinfo.Columns[13].Visible = true;
            imgbtnsubmit_Click(sender, e);
            //CheckBox chkbox = (Gridreqinfo.HeaderRow.FindControl("chkbox") as CheckBox);
            //CheckBox chkboxmsg = (Gridreqinfo.HeaderRow.FindControl("chkboxmsg") as CheckBox);
            //CheckBox chkboxmail = (Gridreqinfo.HeaderRow.FindControl("chkboxmail") as CheckBox);
            //CheckBox chkboxcopy = (Gridreqinfo.HeaderRow.FindControl("chkboxcopy") as CheckBox);

            //chkbox.Enabled = true;
            //chkboxmsg.Enabled = true;
            //chkboxmail.Enabled = true;
            //chkboxcopy.Enabled = true;

            //foreach (GridViewRow gdr in Gridreqinfo.Rows)
            //{
            //    CheckBox chksubbox = (CheckBox)gdr.FindControl("chksubbox");
            //    CheckBox chkmsg = (CheckBox)gdr.FindControl("chkmsg");
            //    CheckBox chkmail = (CheckBox)gdr.FindControl("chkmail");
            //    CheckBox chkcopy = (CheckBox)gdr.FindControl("chkcopy");


            //    chksubbox.Enabled = true;
            //    chkmsg.Enabled = true;
            //    chkmail.Enabled = true;
            //    chkcopy.Enabled = true;
            //}

        }
    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Button3.Text == "Select Display Columns")
        {
            Button3.Text = "Hide Display Columns";
            Panel6.Visible = true;
        }
        else
        {
            Button3.Text = "Select Display Columns";
            Panel6.Visible = false;

        }
    }
    protected void columndisplay()
    {
        if (chkidcolumn.Checked == true)
        {
            Gridreqinfo.Columns[0].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[0].Visible = false;
        }
        if (chktitlecolumn.Checked == true)
        {
            Gridreqinfo.Columns[1].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[1].Visible = false;

        }
        if (chkfileextsion.Checked == true)
        {
            Gridreqinfo.Columns[2].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[2].Visible = false;
        }
        if (chkdoctm.Checked == true)
        {
            Gridreqinfo.Columns[3].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[3].Visible = false;
        }
        if (chkfoldername.Checked == true)
        {
            Gridreqinfo.Columns[4].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[4].Visible = false;
        }

        if (chkpartycolumn.Checked == true)
        {
            Gridreqinfo.Columns[5].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[5].Visible = false;
        }


        if (chkdocumentdate.Checked == true)
        {

            Gridreqinfo.Columns[6].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[6].Visible = false;
        }

        if (chkuploaddate.Checked == true)
        {

            Gridreqinfo.Columns[7].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[7].Visible = false;
        }

        if (chkmyfoldercolumn.Checked == true)
        {
            Gridreqinfo.Columns[8].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[8].Visible = false;
        }

        if (chkaddtomyfoldercolumn.Checked == true)
        {
            Gridreqinfo.Columns[9].Visible = true;

        }
        else
        {
            Gridreqinfo.Columns[9].Visible = false;
        }

        if (chkaccountentrycolumn.Checked == true)
        {
            Gridreqinfo.Columns[10].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[10].Visible = false;
        }

        if (chksendmessagecolumn.Checked == true)
        {
            Gridreqinfo.Columns[11].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[11].Visible = false;
        }
        if (chksendmailcolumn.Checked == true)
        {
            Gridreqinfo.Columns[12].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[12].Visible = false;
        }
        if (chkcopycolumn.Checked == true)
        {
            Gridreqinfo.Columns[13].Visible = true;

        }
        else
        {
            Gridreqinfo.Columns[13].Visible = false;
        }
        





    }
    protected void chksubbox_checkedChanged(object sender, EventArgs e)
    {
        int flagchkaddfolder = 0;
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            CheckBox chksubbox = (CheckBox)gdr.FindControl("chksubbox");


            if (chksubbox.Checked == true)
            {
                flagchkaddfolder = 1;
            }

        }
        CheckBox chkboxmsg = (Gridreqinfo.HeaderRow.FindControl("chkboxmsg") as CheckBox);
        CheckBox chkboxmail = (Gridreqinfo.HeaderRow.FindControl("chkboxmail") as CheckBox);
        CheckBox chkboxcopy = (Gridreqinfo.HeaderRow.FindControl("chkboxcopy") as CheckBox);
        CheckBox chkbox = (Gridreqinfo.HeaderRow.FindControl("chkbox") as CheckBox);

        if (flagchkaddfolder == 1)
        {
            btncopydocument.Visible = false;
            btnmyfolder.Visible = true;
            btnsendemail.Visible = false;
            btnsendmsg.Visible = false;


            chkboxmsg.Enabled = false;
            chkboxmail.Enabled = false;
            chkboxcopy.Enabled = false;

            foreach (GridViewRow gdr in Gridreqinfo.Rows)
            {
                CheckBox chkmsg = (CheckBox)gdr.FindControl("chkmsg");
                CheckBox chkmail = (CheckBox)gdr.FindControl("chkmail");
                CheckBox chkcopy = (CheckBox)gdr.FindControl("chkcopy");



                chkmsg.Enabled = false;
                chkmail.Enabled = false;
                chkcopy.Enabled = false;
            }

        }
        else
        {
            btncopydocument.Visible = false;
            btnmyfolder.Visible = false;
            btnsendemail.Visible = false;
            btnsendmsg.Visible = false;

            chkboxmsg.Enabled = true;
            chkboxmail.Enabled = true;
            chkboxcopy.Enabled = true;

            foreach (GridViewRow gdr in Gridreqinfo.Rows)
            {
                CheckBox chkmsg = (CheckBox)gdr.FindControl("chkmsg");
                CheckBox chkmail = (CheckBox)gdr.FindControl("chkmail");
                CheckBox chkcopy = (CheckBox)gdr.FindControl("chkcopy");


                chkmsg.Enabled = true;
                chkmail.Enabled = true;
                chkcopy.Enabled = true;
            }

        }

        int mastercheckaddtofolder = 0;
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            CheckBox chksubbox = (CheckBox)gdr.FindControl("chksubbox");


            if (chksubbox.Checked == false)
            {
                mastercheckaddtofolder = 1;


            }

        }
        if (mastercheckaddtofolder == 1)
        {
            chkbox.Checked = false;

        }
        else
        {

            chkbox.Checked = true;
        }

    }

    protected void chkmsg_CheckedChanged(object sender, EventArgs e)
    {
        int flagchksendmessage = 0;
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            CheckBox chkmsg = (CheckBox)gdr.FindControl("chkmsg");


            if (chkmsg.Checked == true)
            {
                flagchksendmessage = 1;
            }

        }
        CheckBox chkbox = (Gridreqinfo.HeaderRow.FindControl("chkbox") as CheckBox);
        CheckBox chkboxmail = (Gridreqinfo.HeaderRow.FindControl("chkboxmail") as CheckBox);
        CheckBox chkboxcopy = (Gridreqinfo.HeaderRow.FindControl("chkboxcopy") as CheckBox);
        CheckBox chkboxmsg = (Gridreqinfo.HeaderRow.FindControl("chkboxmsg") as CheckBox);

        if (flagchksendmessage == 1)
        {
            btncopydocument.Visible = false;
            btnmyfolder.Visible = false;
            btnsendemail.Visible = false;
            btnsendmsg.Visible = true;


            chkbox.Enabled = false;
            chkboxmail.Enabled = false;
            chkboxcopy.Enabled = false;

            foreach (GridViewRow gdr in Gridreqinfo.Rows)
            {
                CheckBox chksubbox = (CheckBox)gdr.FindControl("chksubbox");
                CheckBox chkmail = (CheckBox)gdr.FindControl("chkmail");
                CheckBox chkcopy = (CheckBox)gdr.FindControl("chkcopy");



                chksubbox.Enabled = false;
                chkmail.Enabled = false;
                chkcopy.Enabled = false;
            }

        }
        else
        {
            btncopydocument.Visible = false;
            btnmyfolder.Visible = false;
            btnsendemail.Visible = false;
            btnsendmsg.Visible = false;

            chkbox.Enabled = true;
            chkboxmail.Enabled = true;
            chkboxcopy.Enabled = true;

            foreach (GridViewRow gdr in Gridreqinfo.Rows)
            {
                CheckBox chksubbox = (CheckBox)gdr.FindControl("chksubbox");
                CheckBox chkmail = (CheckBox)gdr.FindControl("chkmail");
                CheckBox chkcopy = (CheckBox)gdr.FindControl("chkcopy");


                chksubbox.Enabled = true;
                chkmail.Enabled = true;
                chkcopy.Enabled = true;
            }

        }

        int masterchecksendmessage = 0;
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            CheckBox chkmsg = (CheckBox)gdr.FindControl("chkmsg");


            if (chkmsg.Checked == false)
            {
                masterchecksendmessage = 1;


            }

        }
        if (masterchecksendmessage == 1)
        {
            chkboxmsg.Checked = false;

        }
        else
        {

            chkboxmsg.Checked = true;
        }

    }


    protected void chkmail_CheckedChanged(object sender, EventArgs e)
    {
        int flagchksendmail = 0;
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            CheckBox chkmail = (CheckBox)gdr.FindControl("chkmail");


            if (chkmail.Checked == true)
            {
                flagchksendmail = 1;


            }

        }
        CheckBox chkbox = (Gridreqinfo.HeaderRow.FindControl("chkbox") as CheckBox);
        CheckBox chkboxmsg = (Gridreqinfo.HeaderRow.FindControl("chkboxmsg") as CheckBox);
        CheckBox chkboxcopy = (Gridreqinfo.HeaderRow.FindControl("chkboxcopy") as CheckBox);



        if (flagchksendmail == 1)
        {
            btncopydocument.Visible = false;
            btnmyfolder.Visible = false;
            btnsendemail.Visible = true;
            btnsendmsg.Visible = false;


            chkbox.Enabled = false;
            chkboxmsg.Enabled = false;
            chkboxcopy.Enabled = false;

            foreach (GridViewRow gdr in Gridreqinfo.Rows)
            {
                CheckBox chksubbox = (CheckBox)gdr.FindControl("chksubbox");
                CheckBox chkmsg = (CheckBox)gdr.FindControl("chkmsg");
                CheckBox chkcopy = (CheckBox)gdr.FindControl("chkcopy");



                chksubbox.Enabled = false;
                chkmsg.Enabled = false;
                chkcopy.Enabled = false;
            }

        }
        else
        {
            btncopydocument.Visible = false;
            btnmyfolder.Visible = false;
            btnsendemail.Visible = false;
            btnsendmsg.Visible = false;

            chkbox.Enabled = true;
            chkboxmsg.Enabled = true;
            chkboxcopy.Enabled = true;


            foreach (GridViewRow gdr in Gridreqinfo.Rows)
            {
                CheckBox chksubbox = (CheckBox)gdr.FindControl("chksubbox");
                CheckBox chkmsg = (CheckBox)gdr.FindControl("chkmsg");
                CheckBox chkcopy = (CheckBox)gdr.FindControl("chkcopy");


                chksubbox.Enabled = true;
                chkmsg.Enabled = true;
                chkcopy.Enabled = true;
            }

        }
        CheckBox chkboxmail = (Gridreqinfo.HeaderRow.FindControl("chkboxmail") as CheckBox);

        int masterchecksendmail = 0;
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            CheckBox chkmail = (CheckBox)gdr.FindControl("chkmail");


            if (chkmail.Checked == false)
            {
                masterchecksendmail = 1;


            }

        }
        if (masterchecksendmail == 1)
        {
            chkboxmail.Checked = false;

        }
        else
        {

            chkboxmail.Checked = true;
        }


    }
    protected void chkcopy_CheckedChanged(object sender, EventArgs e)
    {
        int flagchkcopy = 0;
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            CheckBox chkcopy = (CheckBox)gdr.FindControl("chkcopy");


            if (chkcopy.Checked == true)
            {
                flagchkcopy = 1;


            }

        }

        CheckBox chkbox = (Gridreqinfo.HeaderRow.FindControl("chkbox") as CheckBox);
        CheckBox chkboxmsg = (Gridreqinfo.HeaderRow.FindControl("chkboxmsg") as CheckBox);
        CheckBox chkboxmail = (Gridreqinfo.HeaderRow.FindControl("chkboxmail") as CheckBox);
        CheckBox chkboxcopy = (Gridreqinfo.HeaderRow.FindControl("chkboxcopy") as CheckBox);


        if (flagchkcopy == 1)
        {
            btncopydocument.Visible = true;
            btnmyfolder.Visible = false;
            btnsendemail.Visible = false;
            btnsendmsg.Visible = false;


            chkbox.Enabled = false;
            chkboxmsg.Enabled = false;
            chkboxmail.Enabled = false;

            foreach (GridViewRow gdr in Gridreqinfo.Rows)
            {
                CheckBox chksubbox = (CheckBox)gdr.FindControl("chksubbox");
                CheckBox chkmsg = (CheckBox)gdr.FindControl("chkmsg");
                CheckBox chkmail = (CheckBox)gdr.FindControl("chkmail");



                chksubbox.Enabled = false;
                chkmsg.Enabled = false;
                chkmail.Enabled = false;
            }

        }
        else
        {
            btncopydocument.Visible = false;
            btnmyfolder.Visible = false;
            btnsendemail.Visible = false;
            btnsendmsg.Visible = false;

            chkbox.Enabled = true;
            chkboxmsg.Enabled = true;
            chkboxmail.Enabled = true;

            foreach (GridViewRow gdr in Gridreqinfo.Rows)
            {
                CheckBox chksubbox = (CheckBox)gdr.FindControl("chksubbox");
                CheckBox chkmsg = (CheckBox)gdr.FindControl("chkmsg");
                CheckBox chkmail = (CheckBox)gdr.FindControl("chkmail");


                chksubbox.Enabled = true;
                chkmsg.Enabled = true;
                chkmail.Enabled = true;
            }

        }



        int mastercheckcopy = 0;
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            CheckBox chkcopy = (CheckBox)gdr.FindControl("chkcopy");


            if (chkcopy.Checked == false)
            {
                mastercheckcopy = 1;


            }

        }
        if (mastercheckcopy == 1)
        {
            chkboxcopy.Checked = false;

        }
        else
        {

            chkboxcopy.Checked = true;
        }

    }
    protected void btncopydocument_Click(object sender, EventArgs e)
    {
        linkcopy_Click(sender, e);
    }
    protected void btnsendemail_Click(object sender, EventArgs e)
    {
        linkmail_Click(sender, e);

    }
    protected void btnsendmsg_Click(object sender, EventArgs e)
    {
        linkmsg_Click(sender, e);

    }
    protected void btnmyfolder_Click(object sender, EventArgs e)
    {
        LinkButton3_Click(sender, e);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string te = "DocumentFastUpload.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        imgbtnsubmit_Click(sender, e);
    }
    protected void ddlExport_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Gridreqinfo.Rows.Count > 0)
        {

            // Button1_Click1(sender, e);

            if (ddlExport.SelectedValue == "1" || ddlExport.SelectedValue == "4")
            {
                Response.Buffer = true;
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                string filename = "GrdM_" + System.DateTime.Today.Day + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second;
                pnlgrid.RenderControl(hw);
                string style = "";
                string path = HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".Doc");
                System.IO.File.WriteAllText(path, style + sw.ToString());

                //set exportformat to pdf
                Microsoft.Office.Interop.Word.WdExportFormat paramExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF;
                bool paramOpenAfterExport = false;
                Microsoft.Office.Interop.Word.WdExportOptimizeFor paramExportOptimizeFor = Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Microsoft.Office.Interop.Word.WdExportRange paramExportRange = Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;

                Microsoft.Office.Interop.Word.WdExportItem paramExportItem = Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Microsoft.Office.Interop.Word.WdExportCreateBookmarks paramCreateBookmarks = Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;

                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = true;
                object paramSourceDocPath = HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".Doc");
                string paramExportFilePath = HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".pdf");
                Session["Emfile"] = filename + ".pdf";
                Session["GrdmailA"] = null;

                Microsoft.Office.Interop.Word.Application wordApp = null;
                wordApp = new Microsoft.Office.Interop.Word.Application();

                wordApp.Documents.Open(ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing);

                wordApp.ActiveDocument.ExportAsFixedFormat(paramExportFilePath,
                                                                        paramExportFormat, paramOpenAfterExport,
                                                                        paramExportOptimizeFor, paramExportRange, paramStartPage,
                                                                        paramEndPage, paramExportItem, paramIncludeDocProps,
                                                                        paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                                                                        paramBitmapMissingFonts, paramUseISO19005_1,
                                                                        ref paramMissing);


                if (wordApp != null)
                {
                    wordApp.Quit(ref paramMissing, ref paramMissing, ref paramMissing);

                    wordApp = null;
                }
                if (ddlExport.SelectedValue == "4")
                {

                    string te = "MessageComposeExt.aspx?ema=Azxcvyute";
                    try
                    {
                        System.Threading.Thread.Sleep(100);
                        File.Delete(path);
                    }
                    catch
                    {
                    }
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

                }
                else
                {
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                    Response.ContentType = "application/pdf";
                    Response.TransmitFile(paramExportFilePath);
                    Response.End();
                }


            }
            else if (ddlExport.SelectedValue == "2")
            {
                Response.Clear();

                Response.Buffer = true;

                Response.AddHeader("content-disposition",

                "attachment;filename=GridViewExport.xls");

                Response.Charset = "";

                Response.ContentType = "application/vnd.ms-excel";

                StringWriter sw = new StringWriter();

                HtmlTextWriter hw = new HtmlTextWriter(sw);


                //Change the Header Row back to white color

                Gridreqinfo.HeaderRow.Style.Add("background-color", "#FFFFFF");


                for (int i = 0; i < Gridreqinfo.Rows.Count; i++)
                {

                    GridViewRow row = Gridreqinfo.Rows[i];
                    row.BackColor = System.Drawing.Color.White;
                    row.Attributes.Add("class", "textmode");

                }

                pnlgrid.RenderControl(hw);

                //style to format numbers to string

                string style = @"<style> .textmode { mso-number-format:\@; } </style>";

                Response.Write(style);

                Response.Output.Write(sw.ToString());

                Response.Flush();

                Response.End();
            }
            else if (ddlExport.SelectedValue == "3")
            {
                Response.Clear();

                Response.Buffer = true;

                Response.AddHeader("content-disposition",

                "attachment;filename=GridViewExport.doc");

                Response.Charset = "";

                Response.ContentType = "application/vnd.ms-word ";

                StringWriter sw = new StringWriter();

                HtmlTextWriter hw = new HtmlTextWriter(sw);


                pnlgrid.RenderControl(hw);

                Response.Output.Write(sw.ToString());

                Response.Flush();

                Response.End();

            }


        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }


    protected DataTable selectbcon(string str)
    {
        SqlCommand cmd = new SqlCommand(str, PageConn.licenseconn());
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;

    }
    protected void pageMailAccess()
    {
        ddlExport.Items.Insert(0, "Export Type");
        ddlExport.Items[0].Value = "0";
        ddlExport.Items.Insert(1, "Export to PDF");
        ddlExport.Items[1].Value = "1";
        ddlExport.Items.Insert(2, "Export to Excel");
        ddlExport.Items[2].Value = "2";
        ddlExport.Items.Insert(3, "Export to Word");
        ddlExport.Items[3].Value = "3";


        string avfr = "  and PageMaster.PageName='" + ClsEncDesc.EncDyn("MessageCompose.aspx") + "'";
        DataTable drt = selectbcon("SELECT distinct " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId,PageMaster.PageName FROM MainMenuMaster inner join " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=" + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'" + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");
        if (drt.Rows.Count <= 0)
        {

            drt = selectbcon("SELECT PageMaster.PageName FROM PageMaster inner join " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl on " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PageId=PageMaster.PageId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' " + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");
            if (drt.Rows.Count <= 0)
            {
                drt = selectbcon("SELECT distinct PageMaster.PageName FROM MainMenuMaster inner join  SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId inner join PageMaster on PageMaster.SubMenuId=" + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'" + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");


                if (drt.Rows.Count <= 0)
                {


                }
                else
                {
                    ddlExport.Items.Insert(4, "Email with PDF");
                    ddlExport.Items[4].Value = "4";
                }

            }
            else
            {
                ddlExport.Items.Insert(4, "Email with PDF");
                ddlExport.Items[4].Value = "4";

            }


        }
        else
        {

            ddlExport.Items.Insert(4, "Email with PDF");
            ddlExport.Items[4].Value = "4";

        }
    }
    public void createPDFDoc(String strhtml)
    {
        string strfilename = HttpContext.Current.Server.MapPath("TempDoc/GridViewExport.pdf");

        Document doc = new Document(PageSize.A2, 30f, 30f, 30f, 30f);
        PdfWriter.GetInstance(doc, new FileStream(strfilename, FileMode.Create));
        System.IO.StringReader se = new StringReader(strhtml.ToString());
        HTMLWorker obj = new HTMLWorker(doc);

        doc.Open();
        obj.Parse(se);
        doc.Close();
        Showpdf(strfilename);
    }
    public void Showpdf(string strFileName)
    {
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
        Response.ContentType = "application/pdf";
        Response.WriteFile(strFileName);
        Response.Flush();
        Response.Clear();
    }

    protected void ddlSearchby_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchby.SelectedValue == "0")
        {
            pnlType.Visible = false;
            pnlID.Visible = true;
            pnlParty.Visible = false;
            txtDocId.Visible = true;
            ViewState["state"] = "1";
            Gridreqinfo.DataSource = null;
            Gridreqinfo.DataBind();
        }
        else if (ddlSearchby.SelectedValue == "1")
        {
            Fillddldocmaintype();

            pnlType.Visible = true;
            pnlID.Visible = false;
            pnlParty.Visible = false;
            //txtDocTitle.Text = "";
            txtDocId.Text = "";
            //RadioButtonList2.SelectedIndex = -1;
            ViewState["state"] = "3";
            Gridreqinfo.DataSource = null;
            Gridreqinfo.DataBind();
        }
        else if (ddlSearchby.SelectedValue == "2")
        {
            FillParty();

            pnlType.Visible = false;
            pnlID.Visible = false;
            pnlParty.Visible = true;
            //txtDocTitle.Text = "";
            txtDocId.Text = "";

            ViewState["state"] = "4";
            Gridreqinfo.DataSource = null;
            Gridreqinfo.DataBind();
        }
    }
    protected void chkflag_CheckedChanged(object sender, EventArgs e)
    {
        if (chkflag.Checked == true)
        {
            fillflagdata();
            btnflagstatus.Visible = true;
            Gridreqinfo.Columns[15].Visible = true;
        }
        else
        {
            Gridreqinfo.Columns[15].Visible = false;
            btnflagstatus.Visible = false;
        }
    }
    protected void btnflagstatus_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        int Ax = 0;
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            
            LinkButton lnk1 = (LinkButton)gdr.Cells[3].FindControl("LinkButton1");
            DropDownList ddlflag = (DropDownList)gdr.FindControl("ddlflag");
            if(ddlflag.SelectedIndex>0)
            {
                DataTable dtst = select("Select * from DocumentFlag where DocumentID='" + lnk1.Text + "' and Whid='" + ddlbusiness.SelectedValue + "' and  UserID='" + Session["UserId"] + "' and MyDocumentFlagID='" + ddlflag.SelectedValue + "'");
                if (dtst.Rows.Count == 0)
                {
                    string strc = "Insert into DocumentFlag(DocumentID,UserID,MyDocumentFlagID,Whid)Values('" + lnk1.Text + "','" + Session["UserId"] + "','" + ddlflag.SelectedValue + "','" + ddlbusiness.SelectedValue + "')";
                    SqlCommand cmd = new SqlCommand(strc, con);
                    if (con.State.ToString() != "")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Ax = 1;
                    Label lblflag = (Label)gdr.FindControl("lblflag");
                    fillread(lnk1, lblflag);
                }
            }
            if (Ax == 1)
            {
                lblmsg.Text = "Record updated sucessfully";
                chkflag_CheckedChanged(sender, e);
            }
            else
            {
                lblmsg.Text = "Please select flag to change status.";
            }
        }
    }
}
