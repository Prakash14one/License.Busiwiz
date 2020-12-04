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
using System.Data.SqlClient;
public partial class Account_DocumentCabinet : System.Web.UI.Page
{
    SqlConnection con;
    InstructionCls clsInstruction = new InstructionCls();
    DocumentCls1 clsDocument = new DocumentCls1();
    protected int DesignationId;
    protected static int DocMainTypeId121 = 0;
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

        DesignationId = Convert.ToInt32(Session["DesignationId"]);

        if (!IsPostBack)
        {
            txtFromDate.Text = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
            txtToDate.Text = System.DateTime.Now.ToShortDateString();

            Pagecontrol.dypcontrol(Page, page);
            lblcom.Text = Session["Cname"].ToString();
            imgbtnSubmit.Visible = false;
            ViewState["sortOrder"] = "";

            fillstore();
            ddlbusiness_SelectedIndexChanged(sender, e);

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
    protected void FillGrid()
    {


        DataTable dt = new DataTable();

        lblcomname.Text = ddlbusiness.SelectedItem.Text;
        lblfolder.Text = ddltypeofdoc.SelectedItem.Text;
        lbldrower.Text = ddldrower.SelectedItem.Text;
        lblcabi.Text = ddlcabinet.SelectedItem.Text;
        lblstatusprint.Text = ddlstatus.SelectedItem.Text;


        DataTable dt2 = new DataTable();
        string valid = "";
        string Status = "";
        string datesearch = "";

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            datesearch = " and  Cast(DocumentMaster.DocumentUploadDate as Date) between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ";
        }

        if (ddlstatus.SelectedItem.Text == "All")
        {
            Status = "";
        }
        else if (ddlstatus.SelectedItem.Text == "Pending")
        {
            Status = "  AND DocumentMaster.DocumentId  Not in(Select DocumentEmpApproveLog.DocumentId from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join DocumentEmpApproveLog on DocumentEmpApproveLog.DocumentId=DocumentMaster.DocumentId where DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and (DocumentEmpApproveLog.Approve='1' or DocumentEmpApproveLog.Approve='0') and DocumentEmpApproveLog.EmployeeID='" + Session["EmployeeId"] + "')";

        }
        else if (ddlstatus.SelectedItem.Text == "Reject")
        {
            Status = "  AND DocumentMaster.DocumentId  in(Select DocumentEmpApproveLog.DocumentId from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join DocumentEmpApproveLog on DocumentEmpApproveLog.DocumentId=DocumentMaster.DocumentId where DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and (DocumentEmpApproveLog.Approve='0') and DocumentEmpApproveLog.EmployeeID='" + Session["EmployeeId"] + "')";

        }
        else if (ddlstatus.SelectedItem.Text == "Accept")
        {
            Status = " AND DocumentMaster.DocumentId  in(Select DocumentEmpApproveLog.DocumentId from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join DocumentEmpApproveLog on DocumentEmpApproveLog.DocumentId=DocumentMaster.DocumentId where DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and (DocumentEmpApproveLog.Approve='1') and DocumentEmpApproveLog.EmployeeID='" + Session["EmployeeId"] + "')";

        }

        if (ddltypeofdoc.SelectedIndex > 0)
        {
            valid = " and (DocumentMaster.DocumentTypeId ='" + ddltypeofdoc.SelectedValue + "') ";
        }
        else if (ddldrower.SelectedIndex > 0)
        {
            valid = " and (DocumentSubType.DocumentSubTypeId ='" + ddldrower.SelectedValue + "') ";
        }
        else if (ddlcabinet.SelectedIndex > -1)
        {
            valid = " and (DocumentMainType.DocumentMainTypeId ='" + ddlcabinet.SelectedValue + "') ";
        }
        valid = valid + " and (DocumentMaster.DocumentTypeId In(SELECT  Distinct  DocumentAccessRightMaster.DocumentTypeId FROM  DocumentMainType inner join     DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join    DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentAccessRightMaster on DocumentAccessRightMaster.DocumentTypeId=DocumentType.DocumentTypeId  WHERE   DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and     (DocumentAccessRightMaster.CID='" + Session["Comid"] + "') and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')   or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))))";
        dt2 = select("SELECT DISTINCT DocumentMaster.DocumentId,DocumentMainType.Whid, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentName,  DocumentMaster.DocumentUploadDate,DocumentMaster.DocumentDate, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId, DocumentMaster.DocumentRefNo, DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId,DocumentMaster.FileExtensionType, DocumentType.DocumentType, Party_Master.Compname as PartyName FROM   DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId LEFT OUTER JOIN DocumentProcessing ON DocumentMaster.DocumentId = DocumentProcessing.DocumentId LEFT OUTER JOIN Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId WHERE  (DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "') " + valid + " " + Status + " " + datesearch + " AND DocumentMaster.DocumentId  in ( SELECT     DocumentId FROM         DocumentProcessing WHERE     (Approve = 1) ) AND DocumentMaster.DocumentId not in ( SELECT     DocumentId FROM         DocumentProcessing WHERE     (Approve = 0) or (Approve is null) )  and(DocumentMaster.CID='" + Session["Comid"] + "')");
        DataView dv = dt2.DefaultView;
        if (dv.Table.Rows.Count > 0)
        {
            dv.Sort = "DocumentId desc";
        }
        Gridreqinfo.DataSource = dt2;
        DataView myDataView = new DataView();
        myDataView = dt2.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        Gridreqinfo.DataBind();

        int gridrow = 0;
        if (Gridreqinfo.Rows.Count > 0)
        {

            dt = clsDocument.SelectDocumentAccessRigthsByDesignationID();
            do
            {
                foreach (DataRow ddr in dt.Rows)
                {
                    if (Convert.ToInt32(Gridreqinfo.DataKeys[gridrow].Value) == Convert.ToInt32(ddr["DocumentTypeId"]))
                    {

                        Gridreqinfo.Rows[gridrow].Cells[9].Enabled = Convert.ToBoolean(ddr["EditAccess"]);
                        Gridreqinfo.Rows[gridrow].Cells[10].Enabled = Convert.ToBoolean(ddr["DeleteAccess"]);
                        Gridreqinfo.Rows[gridrow].Cells[5].Enabled = Convert.ToBoolean(ddr["ViewAccess"]);

                    }
                }

                //   ===fill drop down inside gridview

                DropDownList ddlApproveType = (DropDownList)Gridreqinfo.Rows[gridrow].FindControl("ddlApprovetype");

                DataTable dt3 = new DataTable();

                dt3 = clsInstruction.SelectRuleApproveTypeMaster(ddlbusiness.SelectedValue);

                ddlApproveType.DataSource = dt3;
                ddlApproveType.DataTextField = "RuleApproveTypeName";
                ddlApproveType.DataValueField = "RuleApproveTypeId";
                ddlApproveType.DataBind();
                ddlApproveType.Items.Insert(0, "None");
                ddlApproveType.Items[0].Value = "0";



                gridrow = gridrow + 1;

            }
            while (gridrow <= Gridreqinfo.Rows.Count - 1);

            foreach (GridViewRow gdr in Gridreqinfo.Rows)
            {
                DropDownList ddlApprovetype1 = (DropDownList)gdr.FindControl("ddlApprovetype");

                if (ddlstatus.SelectedItem.Text != "Pending")
                {
                    DataTable dt4 = new DataTable();

                    Label lnbvvd = (Label)gdr.FindControl("lnbvvd");
                    dt4 = clsDocument.SelectDocumentEmpApproveLogByDocId(Convert.ToInt32(lnbvvd.Text), Convert.ToInt32(Session["EmployeeId"]));
                    if (dt4.Rows.Count > 0)
                    {
                        RadioButtonList rbt = (RadioButtonList)gdr.FindControl("rbtnAcceptReject");

                        TextBox txtNote = (TextBox)gdr.FindControl("TextBox3");
                        Label lbltxtnote = (Label)gdr.FindControl("lbltxtnote");

                        rbt.SelectedValue = dt4.Rows[0]["Approve"].ToString();
                        ddlApprovetype1.SelectedValue = dt4.Rows[0]["DocumentApproveTypeId"].ToString();
                        txtNote.Text = dt4.Rows[0]["Note"].ToString();
                        lbltxtnote.Text = dt4.Rows[0]["Note"].ToString();

                        txtNote.Enabled = false;
                        rbt.Enabled = false;
                        ddlApprovetype1.Enabled = false;

                    }

                }

                if (ddlApprovetype1.SelectedValue == "0")
                {
                    LinkButton lnkapp = (LinkButton)gdr.FindControl("lnkapp");
                    lnkapp.Visible = false;
                }
            }

            if (ddlstatus.SelectedItem.Text == "Accept" || ddlstatus.SelectedItem.Text == "Reject")
            {
                imgbtnSubmit.Visible = false;
            }
            else
            {
                imgbtnSubmit.Visible = true;
            }


        }


    }
    protected void rlheader_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList head = (RadioButtonList)Gridreqinfo.HeaderRow.FindControl("rlheader");
        RadioButtonList rbtnAcceptReject;
        foreach (GridViewRow rowitem in Gridreqinfo.Rows)
        {
            rbtnAcceptReject = (RadioButtonList)(rowitem.FindControl("rbtnAcceptReject"));
            if (head.SelectedValue == "0")
            {
                if (rbtnAcceptReject.Enabled == true)
                {
                    rbtnAcceptReject.SelectedIndex = 0;
                }
            }
            else if (head.SelectedValue == "1")
            {
                if (rbtnAcceptReject.Enabled == true)
                {
                    rbtnAcceptReject.SelectedIndex = 1;
                }
            }
            else if (head.SelectedValue == "2")
            {
                if (rbtnAcceptReject.Enabled == true)
                {
                    rbtnAcceptReject.SelectedIndex = 2;
                }
            }

        }

    }
    protected void Gridreqinfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gridreqinfo.PageIndex = e.NewPageIndex;
        FillGrid();

    }
    protected void Gridreqinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {



        if (e.CommandName == "edit1")
        {
            int DocumentId = Convert.ToInt32(e.CommandArgument);
            int rst = clsDocument.InsertDocumentLog(DocumentId, Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, false, false, true, false, false, false, false);

            string te = "DocumentEditAndView.aspx?id=" + DocumentId + "&&return=3";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);



        }
        if (e.CommandName == "delete1")
        {
            int DocumentId = Convert.ToInt32(e.CommandArgument);
            hdncnfm.Value = DocumentId.ToString();
            imgconfirmok_Click(sender, e);
            // mdlpopupconfirm.Show();

        }

    }

    protected void imgbtnSubmit_Click(object sender, EventArgs e)
    {
        bool success = false;
        foreach (GridViewRow gdr in Gridreqinfo.Rows)
        {
            RadioButtonList rbt = (RadioButtonList)gdr.FindControl("rbtnAcceptReject");
            DropDownList ddlApprovetype = (DropDownList)gdr.FindControl("ddlApprovetype");
            TextBox txtNote = (TextBox)gdr.FindControl("TextBox3");
            Label lnbvvd = (Label)gdr.FindControl("lnbvvd");
            if (rbt.SelectedValue.ToString() != "None" && rbt.Enabled == true)
            {
                success = clsDocument.InsertDocumentEmpApproveLog(Convert.ToInt32(ddlApprovetype.SelectedValue), Convert.ToInt32(lnbvvd.Text), Convert.ToInt32(Session["EmployeeId"]), Convert.ToBoolean(rbt.SelectedValue), txtNote.Text);
            }


        }
        if (success == true)
        {

            lblmsg.Text = "Document Approved Successfully.";
            FillGrid();
        }
    }
    protected void imgconfirmok_Click(object sender, EventArgs e)
    {
        // mdlpopupconfirm.Hide();
        int rst = clsDocument.DeleteDocumentMasterByID(Convert.ToInt32(hdncnfm.Value));
        int rst1 = clsDocument.InsertDocumentLog(Convert.ToInt32(hdncnfm.Value), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, true, false, false, false, false, false, false);
        if (rst > 0)
        {

            lblmsg.Text = "Document Deleted Successfully.";
            FillGrid();
        }
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillddldocmaintype();
    }
    protected void Fillddldocmaintype()
    {
        string str132 = "select Distinct DocumentMainType.DocumentMainTypeId,Left(DocumentMainType.DocumentMainType,25) as DocumentMainType from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentType.DocumentTypeId In( SELECT  Distinct  DocumentTypeId FROM  DocumentAccessRightMaster WHERE     (CID='" + Session["Comid"].ToString() + "' ) and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')  or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true'))) and  DocumentMainType.CID='" + Session["Comid"].ToString() + "' and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' order by DocumentMainType ";

     
        //string str132 = " SELECT [DocumentMainTypeId],DocumentMainType  FROM  [dbo].[DocumentMainType] where Whid='" + ddlbusiness.SelectedValue + "'";

        //string str132 = " SELECT [DocumentMainTypeId],upper(DocumentMainType) as DocumentMainType FROM  [dbo].[DocumentMainType] where CID='" + Session["Comid"] + "'";
        SqlCommand cgw = new SqlCommand(str132, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);

        DataTable dt = new DataTable();

        adgw.Fill(dt);

        ddlcabinet.DataSource = dt;
        ddlcabinet.DataBind();
        EventArgs e = new EventArgs();
        object sender = new object();
        ddlcabinet_SelectedIndexChanged(sender, e);
        //ddldrower.Items.Insert(0, "All");

        //ddldrower.SelectedItem.Value = "0";
        //ddltypeofdoc.Items.Insert(0, "All");

        //ddltypeofdoc.SelectedItem.Value = "0";
    }
    protected void ddlcabinet_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddldrower.Items.Clear();


        FillDocumentSubTypeWithMainType(Int32.Parse(ddlcabinet.SelectedValue.ToString()));

    }
    protected void FillDocumentSubTypeWithMainType(Int32 DocumentMainTypeId)
    {
        //DataTable dt = new DataTable();
        //dt = clsDocument.SelectDocumentSubTypeWithMainType(DocumentMainTypeId);
        ddldrower.Items.Clear();
        string str178 = " SELECT     DocumentSubType.DocumentSubTypeId, DocumentSubType.DocumentSubType, DocumentMainType.DocumentMainTypeId as DocumentMainTypeId,  DocumentMainType.DocumentMainType FROM         DocumentMainType RIGHT OUTER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId WHERE     (DocumentMainType.DocumentMainTypeId = '" + DocumentMainTypeId + "') Order by DocumentSubType ";
        SqlCommand cgw = new SqlCommand(str178, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);

        DataTable dt = new DataTable();

        adgw.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddldrower.DataSource = dt;
            ddldrower.DataBind();
        }
        ddldrower.Items.Insert(0, "All");
        ddldrower.SelectedItem.Value = "0";
        FillDocumentTypeWithSubType(Int32.Parse(ddldrower.SelectedValue.ToString()));
    }
    protected void ddldrower_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldrower.SelectedIndex > 0)
        {
            FillDocumentTypeWithSubType(Int32.Parse(ddldrower.SelectedValue.ToString()));
        }
    }
    protected void FillDocumentTypeWithSubType(Int32 DocumentSubTypeId)
    {
        //DataTable dt = new DataTable();
        //dt = clsDocument.SelectDocumentTypeWithSubType(DocumentSubTypeId);
        ddltypeofdoc.Items.Clear();
        string str132 = "SELECT     DocumentType.DocumentTypeId, DocumentType.DocumentType   FROM         DocumentType WHERE     (DocumentType.DocumentSubTypeId = '" + ddldrower.SelectedValue + "') order by DocumentType";

        SqlCommand cgw = new SqlCommand(str132, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);

        DataTable dt = new DataTable();

        adgw.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddltypeofdoc.DataSource = dt;
            ddltypeofdoc.DataBind();
        }
        ddltypeofdoc.Items.Insert(0, "All");
        ddltypeofdoc.SelectedItem.Value = "0";
        //FillGrid();
    }
    protected void ddltypeofdoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        FillGrid();

        if (Gridreqinfo.Rows.Count > 0)
        {
            imgbtnSubmit.Visible = true;
        }
        else
        {
            imgbtnSubmit.Visible = false;
        }
    }


    protected void LinkButton152_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;


        LinkButton lnkapp = (LinkButton)Gridreqinfo.Rows[rinrow].FindControl("lnkapp");
        DropDownList ddlApprovetype1 = (DropDownList)Gridreqinfo.Rows[rinrow].FindControl("ddlApprovetype");
        if (ddlApprovetype1.SelectedValue != "0")
        {
            DataTable dt3 = clsInstruction.SelectRuleApproveTypeMaster(ddlbusiness.SelectedValue);
            if (dt3.Rows.Count > 0)
            {
                ddlappd.DataSource = dt3;
                ddlappd.DataTextField = "RuleApproveTypeName";
                ddlappd.DataValueField = "RuleApproveTypeId";
                ddlappd.DataBind();

                DataTable dts = select("Select * from RuleApproveTypeMaster where RuleApproveTypeId='" + ddlApprovetype1.SelectedValue + "' ");
                if (dts.Rows.Count > 0)
                {
                    ddlappd.SelectedIndex = ddlappd.Items.IndexOf(ddlappd.Items.FindByValue(dts.Rows[0]["RuleApproveTypeId"].ToString()));
                    txtappdes.Text = Convert.ToString(dts.Rows[0]["Description"]);
                    ModalPopupExtender1.Show();
                }
            }
        }
    }
    protected void ddlApprovetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;


        LinkButton lnkapp = (LinkButton)Gridreqinfo.Rows[rinrow].FindControl("lnkapp");
        DropDownList ddlApprovetype1 = (DropDownList)Gridreqinfo.Rows[rinrow].FindControl("ddlApprovetype");
        if (ddlApprovetype1.SelectedValue != "0")
        {
            lnkapp.Visible = true;
        }
        else
        {
            lnkapp.Visible = false;
        }
    }
    protected void ddlappd_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtappdes.Text = "";
        DataTable dts = select("Select * from RuleApproveTypeMaster where RuleApproveTypeId='" + ddlappd.SelectedValue + "' ");
        if (dts.Rows.Count > 0)
        {
            // ddlappd.SelectedIndex = ddlappd.Items.IndexOf(ddlappd.Items.FindByValue(dts.Rows[0]["RuleApproveTypeId"].ToString()));
            txtappdes.Text = Convert.ToString(dts.Rows[0]["Description"]);
            ModalPopupExtender1.Show();
        }
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
        FillGrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (Gridreqinfo.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                Gridreqinfo.Columns[9].Visible = false;
            }

        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(200);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                Gridreqinfo.Columns[9].Visible = true;
            }

        }
    }
    protected void fillstore()
    {
        ddlbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
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
    protected void Gridreqinfo_RowEditing(object sender, GridViewEditEventArgs e)
    {

        Gridreqinfo.EditIndex = e.NewEditIndex;
        int dk1 = Convert.ToInt32(Gridreqinfo.DataKeys[e.NewEditIndex].Value);


       filleditgrid();
       // FillGrid();


        DropDownList ddlApprovetypeedit = (DropDownList)Gridreqinfo.Rows[Gridreqinfo.EditIndex].FindControl("ddlApprovetypeedit");
        Label lbldocmasterid = (Label)Gridreqinfo.Rows[Gridreqinfo.EditIndex].FindControl("lbldocmasterid");
        Label lnbvvd = (Label)Gridreqinfo.Rows[Gridreqinfo.EditIndex].FindControl("lnbvvd");


        RadioButtonList rbtnAcceptRejectedit = (RadioButtonList)Gridreqinfo.Rows[Gridreqinfo.EditIndex].FindControl("rbtnAcceptRejectedit");
        TextBox TextBox2 = (TextBox)Gridreqinfo.Rows[Gridreqinfo.EditIndex].FindControl("TextBox2");



        DataTable dt4 = clsDocument.SelectDocumentEmpApproveLogByDocId(Convert.ToInt32(lbldocmasterid.Text), Convert.ToInt32(Session["EmployeeId"]));
        if (dt4.Rows.Count > 0)
        {
            DataTable dt3 = clsInstruction.SelectRuleApproveTypeMaster(ddlbusiness.SelectedValue);

            ddlApprovetypeedit.DataSource = dt3;
            ddlApprovetypeedit.DataTextField = "RuleApproveTypeName";
            ddlApprovetypeedit.DataValueField = "RuleApproveTypeId";
            ddlApprovetypeedit.DataBind();
            ddlApprovetypeedit.Items.Insert(0, "None");
            ddlApprovetypeedit.Items[0].Value = "0";

            ddlApprovetypeedit.SelectedIndex = ddlApprovetypeedit.Items.IndexOf(ddlApprovetypeedit.Items.FindByValue(dt4.Rows[0]["DocumentApproveTypeId"].ToString()));

            string a = dt4.Rows[0]["Approve"].ToString();

            if (a == "True")
            {
                rbtnAcceptRejectedit.SelectedValue = "True";
            }
            else
            {
                rbtnAcceptRejectedit.SelectedValue = "False";
            }

            TextBox2.Text = dt4.Rows[0]["Note"].ToString();



        }
        else
        {
            Gridreqinfo.EditIndex = -1;
            FillGrid();


            lblmsg.Visible = true;
            lblmsg.Text = " This document is not yet approved. No changes can be made until the document is approved.";
        }


    }
    protected void ddlApprovetypeedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;


        LinkButton lnkappedit = (LinkButton)Gridreqinfo.Rows[rinrow].FindControl("lnkappedit");
        DropDownList ddlApprovetypeedit = (DropDownList)Gridreqinfo.Rows[rinrow].FindControl("ddlApprovetypeedit");
        if (ddlApprovetypeedit.SelectedValue != "0")
        {
            lnkappedit.Visible = true;
        }
        else
        {
            lnkappedit.Visible = false;
        }
    }
    protected void LinkButton152edit_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;


        LinkButton lnkappedit = (LinkButton)Gridreqinfo.Rows[rinrow].FindControl("lnkappedit");
        DropDownList ddlApprovetypeedit = (DropDownList)Gridreqinfo.Rows[rinrow].FindControl("ddlApprovetypeedit");
        if (ddlApprovetypeedit.SelectedValue != "0")
        {
            DataTable dt3 = clsInstruction.SelectRuleApproveTypeMaster(ddlbusiness.SelectedValue);
            if (dt3.Rows.Count > 0)
            {
                ddlappd.DataSource = dt3;
                ddlappd.DataTextField = "RuleApproveTypeName";
                ddlappd.DataValueField = "RuleApproveTypeId";
                ddlappd.DataBind();

                DataTable dts = select("Select * from RuleApproveTypeMaster where RuleApproveTypeId='" + ddlApprovetypeedit.SelectedValue + "' ");
                if (dts.Rows.Count > 0)
                {
                    ddlappd.SelectedIndex = ddlappd.Items.IndexOf(ddlappd.Items.FindByValue(dts.Rows[0]["RuleApproveTypeId"].ToString()));
                    txtappdes.Text = Convert.ToString(dts.Rows[0]["Description"]);
                    ModalPopupExtender1.Show();
                }
            }
        }
    }
    protected void filleditgrid()
    {

        DataTable dt = new DataTable();

        lblcomname.Text = ddlbusiness.SelectedItem.Text;
        lblfolder.Text = ddltypeofdoc.SelectedItem.Text;
        lbldrower.Text = ddldrower.SelectedItem.Text;
        lblcabi.Text = ddlcabinet.SelectedItem.Text;
        lblstatusprint.Text = ddlstatus.SelectedItem.Text;


        DataTable dt2 = new DataTable();
        string valid = "";
        string Status = "";
        string datesearch = "";

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            datesearch = " and DocumentMaster.DocumentUploadDate between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ";
        }

        if (ddlstatus.SelectedItem.Text == "All")
        {
            Status = "";
        }
        else if (ddlstatus.SelectedItem.Text == "Pending")
        {
            Status = "  AND DocumentMaster.DocumentId  Not in(Select DocumentEmpApproveLog.DocumentId from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join DocumentEmpApproveLog on DocumentEmpApproveLog.DocumentId=DocumentMaster.DocumentId where DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and (DocumentEmpApproveLog.Approve='1' or DocumentEmpApproveLog.Approve='0') and DocumentEmpApproveLog.EmployeeID='" + Session["EmployeeId"] + "')";

        }
        else if (ddlstatus.SelectedItem.Text == "Reject")
        {
            Status = "  AND DocumentMaster.DocumentId  in(Select DocumentEmpApproveLog.DocumentId from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join DocumentEmpApproveLog on DocumentEmpApproveLog.DocumentId=DocumentMaster.DocumentId where DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and (DocumentEmpApproveLog.Approve='0') and DocumentEmpApproveLog.EmployeeID='" + Session["EmployeeId"] + "')";

        }
        else if (ddlstatus.SelectedItem.Text == "Accept")
        {
            Status = " AND DocumentMaster.DocumentId  in(Select DocumentEmpApproveLog.DocumentId from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId inner join DocumentEmpApproveLog on DocumentEmpApproveLog.DocumentId=DocumentMaster.DocumentId where DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and (DocumentEmpApproveLog.Approve='1') and DocumentEmpApproveLog.EmployeeID='" + Session["EmployeeId"] + "')";

        }

        if (ddltypeofdoc.SelectedIndex > 0)
        {
            valid = " and (DocumentMaster.DocumentTypeId ='" + ddltypeofdoc.SelectedValue + "') ";
        }
        else if (ddldrower.SelectedIndex > 0)
        {
            valid = " and (DocumentSubType.DocumentSubTypeId ='" + ddldrower.SelectedValue + "') ";
        }
        else if (ddlcabinet.SelectedIndex > -1)
        {
            valid = " and (DocumentMainType.DocumentMainTypeId ='" + ddlcabinet.SelectedValue + "') ";
        }

        dt2 = select("SELECT DISTINCT DocumentMaster.DocumentId,DocumentMainType.Whid, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentName,  DocumentMaster.DocumentUploadDate,DocumentMaster.DocumentDate, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId, DocumentMaster.DocumentRefNo, DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId,DocumentMaster.FileExtensionType, DocumentType.DocumentType, Party_Master.Compname as PartyName FROM   DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId LEFT OUTER JOIN DocumentProcessing ON DocumentMaster.DocumentId = DocumentProcessing.DocumentId LEFT OUTER JOIN Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId WHERE  (DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "') " + valid + " " + Status + " " + datesearch + " AND DocumentMaster.DocumentId  in ( SELECT     DocumentId FROM         DocumentProcessing WHERE     (Approve = 1) ) AND DocumentMaster.DocumentId not in ( SELECT     DocumentId FROM         DocumentProcessing WHERE     (Approve = 0) or (Approve is null) )  and(DocumentMaster.CID='" + Session["Comid"] + "')");
        DataView dv = dt2.DefaultView;
        if (dv.Table.Rows.Count > 0)
        {
            dv.Sort = "DocumentId desc";
        }
        Gridreqinfo.DataSource = dt2;
        DataView myDataView = new DataView();
        myDataView = dt2.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        Gridreqinfo.DataBind();
    }
    protected void Gridreqinfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Gridreqinfo.EditIndex = -1;

        FillGrid();
    }

    protected void Gridreqinfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList ddlApprovetypeedit = (DropDownList)Gridreqinfo.Rows[Gridreqinfo.EditIndex].FindControl("ddlApprovetypeedit");
        Label lbldocmasterid = (Label)Gridreqinfo.Rows[Gridreqinfo.EditIndex].FindControl("lbldocmasterid");

        RadioButtonList rbtnAcceptRejectedit = (RadioButtonList)Gridreqinfo.Rows[Gridreqinfo.EditIndex].FindControl("rbtnAcceptRejectedit");
        TextBox TextBox2 = (TextBox)Gridreqinfo.Rows[Gridreqinfo.EditIndex].FindControl("TextBox2");


        string str = "update DocumentEmpApproveLog set Approve='" + rbtnAcceptRejectedit.SelectedValue + "',Note='" + TextBox2.Text + "',DocumentApproveTypeId='" + ddlApprovetypeedit.SelectedValue + "' where DocumentId='" + lbldocmasterid.Text + "' and EmployeeID='" + Convert.ToInt32(Session["EmployeeId"]) + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }

        cmd.ExecuteNonQuery();
        con.Close();

        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully.";

        Gridreqinfo.EditIndex = -1;
        FillGrid();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {

        LinkButton LinkButton3 = (LinkButton)Gridreqinfo.HeaderRow.FindControl("LinkButton3");




        foreach (GridViewRow grd in Gridreqinfo.Rows)
        {
            TextBox TextBox3 = (TextBox)grd.FindControl("TextBox3");
            Label lbltxtnote = (Label)grd.FindControl("lbltxtnote");

            if (LinkButton3.Text == "(More Info)")
            {

                TextBox3.Visible = true;
                lbltxtnote.Visible = false;

            }
            if (LinkButton3.Text == "(Hide)")
            {

                TextBox3.Visible = false;
                lbltxtnote.Visible = true;
            }



        }
        if (LinkButton3.Text == "(More Info)")
        {
            LinkButton3.Text = "(Hide)";
        }
        else
        {
            LinkButton3.Text = "(More Info)";
        }




    }
    protected void Gridreqinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DropDownList ddlApprovetype = (DropDownList)e.Row.FindControl("ddlApprovetype");
            //DataTable dt3 = new DataTable();
            //dt3 = clsInstruction.SelectRuleApproveTypeMaster(ddlbusiness.SelectedValue);
            //ddlApprovetype.DataSource = dt3;
            //ddlApprovetype.DataTextField = "RuleApproveTypeName";
            //ddlApprovetype.DataValueField = "RuleApproveTypeId";
            //ddlApprovetype.DataBind();
            //ddlApprovetype.Items.Insert(0, "None");
            //ddlApprovetype.Items[0].Value = "0";


            //if (ddlstatus.SelectedItem.Text != "Pending")
            //{
            //    DataTable dt4 = new DataTable();

            //    Label lnbvvd = (Label)e.Row.FindControl("lnbvvd");

            //    dt4 = clsDocument.SelectDocumentEmpApproveLogByDocId(Convert.ToInt32(lnbvvd.Text), Convert.ToInt32(Session["EmployeeId"]));

            //    if (dt4.Rows.Count > 0)
            //    {
            //        RadioButtonList rbt = (RadioButtonList)e.Row.FindControl("rbtnAcceptReject");

            //        TextBox txtNote = (TextBox)e.Row.FindControl("TextBox3");
            //        Label lbltxtnote = (Label)e.Row.FindControl("lbltxtnote");

            //        rbt.SelectedValue = dt4.Rows[0]["Approve"].ToString();

            //        //ddlApprovetype1.SelectedValue = dt4.Rows[0]["DocumentApproveTypeId"].ToString();
            //        ddlApprovetype.SelectedIndex = ddlApprovetype.Items.IndexOf(ddlApprovetype.Items.FindByValue(dt4.Rows[0]["DocumentApproveTypeId"].ToString()));
            //        txtNote.Text = dt4.Rows[0]["Note"].ToString();
            //        lbltxtnote.Text = dt4.Rows[0]["Note"].ToString();
            //        txtNote.Enabled = false;
            //        rbt.Enabled = false;
            //        ddlApprovetype.Enabled = false;



            //    }

            //}

            //if (ddlApprovetype.SelectedValue == "0")
            ////if (ddlApproveType.SelectedIndex>0)
            //{
            //    LinkButton lnkapp = (LinkButton)e.Row.FindControl("lnkapp");
            //    lnkapp.Visible = false;
            //}



        }
    }
}
