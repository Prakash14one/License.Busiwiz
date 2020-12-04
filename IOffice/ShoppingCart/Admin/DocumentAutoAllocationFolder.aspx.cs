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
public partial class DocumentAutoAllocationFolder : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(PageConn.connnn);
    DocumentCls1 clsDocument = new DocumentCls1();
    Companycls clscompany = new Companycls();
    MasterCls clsMaster = new MasterCls();
    EmployeeCls clsEmployee = new EmployeeCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }


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
            Pagecontrol.dypcontrol(Page, page);
            lblCompany.Text = Session["Cname"].ToString();
            ViewState["sortOrder"] = "";
            lblmsg.Text = "";

            flaganddoc();
            fillstore();
            fillfilterstore();





            //DataTable dt1 = new DataTable();
            //dt1 = clscompany.selectCompanyMaster();
            //if (dt1.Rows.Count > 0)
            //{
            //    if (Convert.ToString(dt1.Rows[0]["DocFolder"]) == "True")
            //    {
            //        RadioButtonList1.SelectedIndex = 1;
                   
            //    }
            //    else
            //    {
            //        RadioButtonList1.SelectedIndex = 0;
            //    }
               
            //}
            FillGrid();
            RadioButtonList1_SelectedIndexChanged(sender, e);

            FillDocumentTypeAll();
            FillParty();
        }

    }
   


    protected void GridEmail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridEmail.PageIndex = e.NewPageIndex;
        FillGrid();

    }
    protected void GridEmail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridEmail.EditIndex = -1;
        FillGrid();
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

    protected void GridEmail_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        lblmsg.Text = "";

        if (e.CommandName == "Delete1")
        {

            int indx1 = Convert.ToInt32(e.CommandArgument.ToString());
            Int32 datakey = indx1;
            hdncnfm.Value = datakey.ToString();

            int result = clscompany.DeleteDownloadFolder(Convert.ToInt32(datakey));

            if (result > 0)
            {
                FillGrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record Deleted successfully";

            }



        }
        if (e.CommandName == "Edit")
        {
            int indx1 = Convert.ToInt32(e.CommandArgument.ToString());

            Int32 datakey = indx1;
            Session["FolderId"] = datakey;
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDownloadFolderIdwise(datakey);

            if (dt.Rows.Count > 0)
            {
                RadioButtonList1.SelectedIndex = 1;
                RadioButtonList1_SelectedIndexChanged(sender, e);
                ddlbusiness.SelectedValue = dt.Rows[0]["Whid"].ToString();
                txtServer.Text = Convert.ToString(dt.Rows[0]["FolderName"].ToString());
                txtautoretrival.Text = dt.Rows[0]["AutoInterval"].ToString();

                imgbtnsubmit.Visible = false;
                pnlsetrule.Visible = false;

                if (Convert.ToBoolean(dt.Rows[0]["DocumentAutoApprove"].ToString()) == false)
                {
                    Chkautoprcss.Checked = false;
                }
                else
                {
                    Chkautoprcss.Checked = true;
                }
                Session["DocumentFolderDownloadDefaultPropId"] = null;
                if (dt.Rows[0]["DocumentFolderDownloadDefaultPropId"].ToString() != "")
                {
                    Session["DocumentFolderDownloadDefaultPropId"] = dt.Rows[0]["DocumentFolderDownloadDefaultPropId"].ToString();
                    rbtnlistsetrules.SelectedValue = "Set Fixed Document Process Rule for This Folder";
                    pnlsetrule.Visible = true;

                    ddlbusiness_SelectedIndexChanged(sender, e);

                    ddldoctype.SelectedValue = dt.Rows[0]["DocumentTypeId"].ToString();
                    ddlparty.SelectedValue = dt.Rows[0]["PartyId"].ToString();
                    txtdoctitle.Text = Convert.ToString(dt.Rows[0]["DocumentTittle"].ToString());
                    txtdocdesc.Text = Convert.ToString(dt.Rows[0]["DocumentDescription"].ToString());
                    ddldt.SelectedIndex = ddldt.Items.IndexOf(ddldt.Items.FindByValue(Convert.ToString(dt.Rows[0]["DocTypenm"])));
                }
                rbtnlistsetrules_SelectedIndexChanged(sender, e);
                imgbtnUpdate.Visible = true;
                


            }
        }


    }




    protected void GridEmail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridEmail_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    public Int32 InsertDownloadFolder(String FolderName, bool FolderRule, bool DocumentAutoApprove, String RuleType, String Whid, String AutoInterval)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "InsertDownloadFolder";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FolderName", SqlDbType.NVarChar));
        cmd.Parameters["@FolderName"].Value = FolderName;
        cmd.Parameters.Add(new SqlParameter("@FolderRule", SqlDbType.Bit));
        cmd.Parameters["@FolderRule"].Value = FolderRule;
        cmd.Parameters.Add(new SqlParameter("@DocumentAutoApprove", SqlDbType.Bit));
        cmd.Parameters["@DocumentAutoApprove"].Value = DocumentAutoApprove;
        cmd.Parameters.Add(new SqlParameter("@RuleType", SqlDbType.VarChar));
        cmd.Parameters["@RuleType"].Value = RuleType;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@AutoInterval", SqlDbType.NVarChar));
        cmd.Parameters["@AutoInterval"].Value = AutoInterval;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Direction = ParameterDirection.Output;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@FolderId"].Value.ToString());
        return result;
    }

    protected void FillDocumentTypeAll()
    {
        DocumentCls1 clsDocument = new DocumentCls1();
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocTypeAll(ddlbusiness.SelectedValue);
        ddldoctype.DataSource = dt;
        ddldoctype.DataBind();

    }
    protected void FillParty()
    {
        DataTable dt = new DataTable();
        dt = clsDocument.selectparty(ddlbusiness.SelectedValue);
        ddlparty.DataSource = dt;
        ddlparty.DataBind();

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
           // Panel2.Visible = false;
           
            txtServer.Enabled = false;
            imgbtnsubmit.Enabled = false;
            Chkautoprcss.Enabled = false;
            rbtnlistsetrules.Enabled = false;

           
            txtServer.Visible = false;
            imgbtnsubmit.Visible = false;
            Chkautoprcss.Visible = false;
            rbtnlistsetrules.Visible = false;
            pnl_FtpAccount_priceplan.Visible = false;
        }
        else
        {
           // Panel2.Visible = true;
            //txtEmail.Visible = true;
            //txtPwd.Visible = true;
            txtServer.Visible = true;
            imgbtnsubmit.Visible = true;
            Chkautoprcss.Visible = true;
            rbtnlistsetrules.Visible = true;

            //txtEmail.Enabled = true;
            //txtPwd.Enabled = true;
            txtServer.Enabled = true;
            imgbtnsubmit.Enabled = true;
            Chkautoprcss.Enabled = true;
            rbtnlistsetrules.Enabled = true;
            pnl_FtpAccount_priceplan.Visible = true;
            if (imgbtnUpdate.Visible == true)
            {
                imgbtnsubmit.Visible = false;
            }
            else
            {
                imgbtnsubmit.Visible = true;
            }
            //FillGrid();
        }
        int j = clscompany.UpdateCompanyMasterforDocFolder(Convert.ToBoolean(RadioButtonList1.SelectedIndex));

    }
    public void FillGrid()
    {
        lblBusiness.Text = DropDownList1.SelectedItem.Text;

        DataTable dt = new DataTable();
        dt = clscompany.SelectDownloadFolder(DropDownList1.SelectedValue);


        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridEmail.DataSource = dt;
        GridEmail.DataBind();


    }

    protected void UpdateDocEmailmaster()
    {
        Int32 emaildownid;
        String servername;
        string ruletype = "";
        emaildownid = Convert.ToInt32(Session["FolderId"].ToString());
        servername = txtServer.Text;

        if (rbtnlistsetrules.SelectedItem.Value == "Set Fixed Document Process Rule for This Folder")
        {
            ruletype = "Fixed";
        }
        else if (rbtnlistsetrules.SelectedItem.Value == "Set Dynamic Document Process Rule for This Folder")
        {
            ruletype = "Dynamic";
        }

        bool scs_ftp = clsDocument.UpdateDownloadFolder(emaildownid, servername, Convert.ToBoolean(Chkautoprcss.Checked), ruletype, ddlbusiness.SelectedValue, txtautoretrival.Text);
        if (Convert.ToInt32(scs_ftp) > 0)
        {
            //  pnlmsg.Visible = true;
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";

        }


    }
    protected void UpdateDocEmailde_prop()
    {

        Int32 de_prop_id, doctypeid, partyid;
        String doctitle, docdesc;

        doctypeid = Convert.ToInt32(ddldoctype.SelectedValue);
        de_prop_id = Convert.ToInt32(Session["DocumentFolderDownloadDefaultPropId"].ToString());
        partyid = Convert.ToInt32(ddlparty.SelectedValue);
        doctitle = txtdoctitle.Text;
        docdesc = txtdocdesc.Text;
        bool scs_def_prop = UpdateDocumentFolderDownloadDefaultProp(de_prop_id, doctitle, doctypeid, partyid, docdesc, txtautoretrival.Text);
    }

    public bool UpdateDocumentFolderDownloadDefaultProp(Int32 DocumentFolderDownloadDefaultPropId, String DocumentTittle, Int32 DocumentTypeId, Int32 PartyId, String DocumentDescription, String AutoInterval)
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UpdateDocumentFolderDownloadDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentFolderDownloadDefaultPropId", SqlDbType.Int));
        cmd.Parameters["@DocumentFolderDownloadDefaultPropId"].Value = DocumentFolderDownloadDefaultPropId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTittle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTittle"].Value = DocumentTittle;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@DocumentDescription", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentDescription"].Value = DocumentDescription;
        cmd.Parameters.Add(new SqlParameter("@AutoInterval", SqlDbType.NVarChar));
        cmd.Parameters["@AutoInterval"].Value = AutoInterval;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public Int32 InsertDocumentFolderDownloadDefaultProp(Int32 FolderId, string DocumentTittle, Int32 DocumentTypeId, Int32 PartyId, string DocumentDescription, string AutoInterval, string Docyt)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentFolderDownloadDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = FolderId;

        cmd.Parameters.Add(new SqlParameter("@DocumentTittle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTittle"].Value = DocumentTittle;

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;

        cmd.Parameters.Add(new SqlParameter("@DocumentDescription", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentDescription"].Value = DocumentDescription;
        cmd.Parameters.Add(new SqlParameter("@DocTypenm", SqlDbType.NVarChar));
        cmd.Parameters["@DocTypenm"].Value = Docyt;
        cmd.Parameters.Add(new SqlParameter("@AutoInterval", SqlDbType.NVarChar));
        cmd.Parameters["@AutoInterval"].Value = AutoInterval;

        //cmd.Parameters.Add(new SqlParameter("@DocTittleOrEmailSub", SqlDbType.Bit));
        //cmd.Parameters["@DocTittleOrEmailSub"].Value = DocTittleOrEmailSub;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }


    protected void clear()
    {
        txtautoretrival.Text = "";
        txtServer.Text = "";
      
        txtdocdesc.Text = "";
        txtdoctitle.Text = "";
        Chkautoprcss.Checked = false;
    }

    protected void lnkadd2_Click(object sender, EventArgs e)
    {
        if (ddlbusiness.SelectedIndex > 0)
        {
            FillDocumentTypeAll();
        }


    }



    protected void rbtnlistsetrules_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnlistsetrules.SelectedValue == "Set Fixed Document Process Rule for This Folder")
        {
            pnlsetrule.Visible = true;
        }
        else
        {
            pnlsetrule.Visible = false;

        }
    }
    protected void ibtnCancelCabinetAdd_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender9.Hide();
    }

    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
        FillDocumentTypeAll();
        FillParty();
    }
    protected void GridEmail_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillGrid();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillGrid();
    }
    protected void imgbtnUpdate_Click(object sender, EventArgs e)
    {
        DataTable dt111 = (DataTable)select("Select * from  DownloadFolder where FolderName='" + txtServer.Text + "' and Whid='" + ddlbusiness.SelectedValue + "' and FolderId<>'" + Session["FolderId"].ToString() + "'");
        if (dt111.Rows.Count <= 0)
        {
            bool access = UserAccess.Usercon("DownloadFolder", "", "FolderId", "", "", "CID", "DownloadFolder");
            if (access == true)
            {
                UpdateDocEmailmaster();


                if (rbtnlistsetrules.SelectedItem.Value == "Set Fixed Document Process Rule for This Folder")
                {

                    if (Session["DocumentFolderDownloadDefaultPropId"] != null)
                    {
                        UpdateDocEmailde_prop();
                    }
                    else if (Session["DocumentFolderDownloadDefaultPropId"] == null)
                    {
                        int i = Convert.ToInt32(Session["FolderId"].ToString());
                        string doctitle = txtdoctitle.Text;
                        int doctypeid = Convert.ToInt32(ddldoctype.SelectedValue);
                        int partyid = Convert.ToInt32(ddlparty.SelectedValue);
                        string desc = txtdocdesc.Text;
                        int jj = InsertDocumentFolderDownloadDefaultProp(i, doctitle, doctypeid, partyid, desc, txtautoretrival.Text,ddldt.SelectedValue);

                        if (i > 0)
                        {
                            RadioButtonList1.SelectedIndex = 0;
                            RadioButtonList1_SelectedIndexChanged(sender, e);
                            lblmsg.Visible = true;
                            lblmsg.Text = "Record inserted successfully.";

                        }
                    }
                }
                else if (rbtnlistsetrules.SelectedItem.Value == "Set Dynamic Document Process Rule for This Folder")
                {
                    int result = clscompany.DeleteDownloadFolderDefaultProp(Convert.ToInt32(Session["FolderId"].ToString()));
                    txtdocdesc.Text = "";
                    txtdoctitle.Text = "";
                   
                }


              
                
                Session["DocumentEmailDownloadDefaultPropId"] = null;
                Session["DocumentEmailDownloadID"] = null;
                FillGrid();
                RadioButtonList1.SelectedIndex = 0;
                RadioButtonList1_SelectedIndexChanged(sender, e);
                clear();
                imgbtnsubmit.Visible = true;
                imgbtnUpdate.Visible = false;
                rbtnlistsetrules.SelectedIndex = 0;
                pnlsetrule.Visible = false;
                pnlsetrule.Visible = false;
            }
            else
            {
                // pnlmsg.Visible = true;
                lblmsg.Visible = true;
                lblmsg.Text = "Record already exist.";
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, You are not permitted for greater record to Priceplan";
        }
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {
        DataTable dt111 = (DataTable)select("Select * from  DownloadFolder where FolderName='" + txtServer.Text + "' and Whid='" + ddlbusiness.SelectedValue + "'");
        if (dt111.Rows.Count <= 0)
        {
            bool access = UserAccess.Usercon("DownloadFolder", "", "FolderId", "", "", "CID", "DownloadFolder");
            if (access == true)
            {
                bool docautoapprove = Chkautoprcss.Checked;
                Int32 insrt;
                insrt = 0;

                bool statusRule = true;
                bool status = Convert.ToBoolean(RadioButtonList1.SelectedItem.Value);
                Int32 result = clscompany.UpdateCompanyMasterforDocFolder(status);
                if (status == true)
                {

                }
                if (rbtnlistsetrules.SelectedValue.ToString() == "Set Dynamic Document Process Rule for This Folder")
                {

                    insrt = InsertDownloadFolder(txtServer.Text, statusRule, docautoapprove, "Dynamic", ddlbusiness.SelectedValue, txtautoretrival.Text);
                    if (insrt > 0)
                    {
                        pnlsetrule.Visible = false;
                        // pnlmsg.Visible = true;
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully.";
                        FillGrid();
                      
                        txtServer.Text = "";
                        txtautoretrival.Text = "";
                    }

                }
                else if (rbtnlistsetrules.SelectedValue.ToString() == "Set Fixed Document Process Rule for This Folder")
                {

                    pnlsetrule.Visible = true;

                    insrt = InsertDownloadFolder(txtServer.Text, statusRule, docautoapprove, "Fixed", ddlbusiness.SelectedValue, txtautoretrival.Text);
                    if (insrt > 0)
                    {

                        string doctitle = txtdoctitle.Text;
                        int doctypeid = Convert.ToInt32(ddldoctype.SelectedValue);
                        int partyid = Convert.ToInt32(ddlparty.SelectedValue);
                        string desc = txtdocdesc.Text;

                        int j = InsertDocumentFolderDownloadDefaultProp(insrt, doctitle, doctypeid, partyid, desc, txtautoretrival.Text, ddldt.SelectedValue);
                        if (j > 0)
                        {

                            lblmsg.Text = "Record inserted Successfully.";
                            lblmsg.Visible = true;
                            lblmsg.Visible = true;
                            FillGrid();
                         
                            //txtEmail.Text = "";
                            //txtPwd.Text = "";
                            txtServer.Text = "";
                            txtdocdesc.Text = "";
                            txtdoctitle.Text = "";
                            txtautoretrival.Text = "";

                        }
                    }

                }

                //


                //lblmsg.Text = "Data is inserted Successfully.";
                lblmsg.Visible = true;

                rbtnlistsetrules.SelectedIndex = 0;
                pnlsetrule.Visible = false;
                FillGrid();
                RadioButtonList1.SelectedIndex = 0;
                RadioButtonList1_SelectedIndexChanged(sender, e);
            }
            else
            {

                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You are not permitted for greater record to Priceplan";
            }
        }

        else
        {

            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist.";
        }
    }
    protected void imgbtnReset_Click(object sender, EventArgs e)
    {
        RadioButtonList1.SelectedIndex = 0;
        RadioButtonList1_SelectedIndexChanged(sender, e);
        lblmsg.Text = "";
        txtautoretrival.Text = "";
        txtServer.Text = "";
        txtdoctitle.Text = "";
        txtdocdesc.Text = "";
        Chkautoprcss.Checked = false;
        
        imgbtnsubmit.Visible = true;
        imgbtnUpdate.Visible = false;
        rbtnlistsetrules.SelectedIndex = 0;
        pnlsetrule.Visible = false;
        pnlsetrule.Visible = false;

    }
   
    protected void imgAdd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentSubSubType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        
        FillDocumentTypeAll();
    }
    protected void imgAdd2_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PartyMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgRefresh2_Click(object sender, ImageClickEventArgs e)
    {
       
        FillParty();
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;
            if (GridEmail.Columns[5].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridEmail.Columns[5].Visible = false;
            }
            if (GridEmail.Columns[6].Visible == true)
            {
                ViewState["delHide"] = "tt";
                GridEmail.Columns[6].Visible = false;
            }
        }
        else
        {

            pnlgrid.ScrollBars = ScrollBars.Vertical;
            pnlgrid.Height = new Unit(200);

            Button2.Text = "Printable Version";
            Button1.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridEmail.Columns[5].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                GridEmail.Columns[6].Visible = true;
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

    protected void fillfilterstore()
    {
        DropDownList1.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";



    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
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
        //ddldt.Items.Insert(0, "Select");
        //ddldt.Items[0].Value = "0";
    }
  
}
