
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
public partial class WizardAccount_DocumentEmailDownload : System.Web.UI.Page
{
    SqlConnection con;
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

            string pass = txtPwd.Text;
            txtPwd.Attributes.Add("Value", pass);


            if (!IsPostBack)
            {

                ViewState["sortOrder"] = "";
                lblcom.Text = Session["Cname"].ToString();
                lblCompany.Text = "All";

                fillstore();
                fillfilterstore();
                flaganddoc();
                DataTable dt1 = new DataTable();
                dt1 = clscompany.selectCompanyMaster();
                if (dt1.Rows.Count > 0)
                {
                    if (Convert.ToString(dt1.Rows[0]["MailAccounts"]) == "True")
                    {
                        RadioButtonList1.SelectedIndex = 1;
                    }
                    else
                    {
                        RadioButtonList1.SelectedIndex = 0;
                    }
                }
                RadioButtonList1_SelectedIndexChanged(sender, e);

                FillGrid();
                FillDocumentTypeAll();
                FillParty();
                rbtnlistsetrules_SelectedIndexChanged(sender, e);

            }
            
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
        ddlparty.Items.Insert(0, "--Select--");
        ddlparty.Items[0].Value = "0";

    }
    protected void GridEmail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridEmail.PageIndex = e.NewPageIndex;
        FillGrid();

    }
    public Int32 DeleteDocumentEmailDownloadDefaultProp(Int32 DocumentEmailDownloadID)
    {
     SqlCommand   cmd = new SqlCommand();
        cmd.CommandText = "DeleteDocumentEmailDownloadDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Value = DocumentEmailDownloadID;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    protected void GridEmail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridEmail.EditIndex = -1;
        FillGrid();
    }
    protected void GridEmail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }
        if (e.CommandName == "Delete1")
        {
           // int indx = Convert.ToInt32(e.CommandArgument.ToString());
            Int32 datakey = Int32.Parse(e.CommandArgument.ToString());
            //GridEmail.DataKeys[GridEmail.]
            hdncnfm.Value = datakey.ToString();
            //mdlpopupconfirm.Show();
            imgconfirmok_Click(sender, e);
        }

        if (e.CommandName == "Edit1")
        {
            int indx = Convert.ToInt32(e.CommandArgument.ToString());
           // Int32 datakey = Int32.Parse(GridEmail.DataKeys[indx].Value.ToString());
            Int32 datakey = Convert.ToInt32(e.CommandArgument.ToString());
            Session["DocumentEmailDownloadID"] = datakey;
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDocumentEmailDownloadMasterWithID(datakey);

            if (dt.Rows.Count > 0)
            {
                Panel1.Visible = true;
                ddlbusiness.SelectedValue = dt.Rows[0]["Whid"].ToString();
                txtServer.Text = Convert.ToString(dt.Rows[0]["ServerName"].ToString());
                txtEmail.Visible = true;

                txtEmail.Text = Convert.ToString(dt.Rows[0]["EmailId"].ToString());
                //lblEmailId.Visible = false;
                txtautoretrival.Text = dt.Rows[0]["AutoRetrival"].ToString(); 
                txtPwd.Visible = true;
                //txtPwd.Text = Convert.ToString(dt.Rows[0]["Password"].ToString());

                string strqa = Convert.ToString(dt.Rows[0]["Password"].ToString());
                txtPwd.Attributes.Add("Value", strqa);

                imgbtnsubmit.Visible = false;
                pnlsetrule.Visible = false;
                rbtnlistsetrules.SelectedValue = "Set Dynamic Document Process Rule for This Email Account";
                if (Convert.ToBoolean(dt.Rows[0]["DocumentAutoApprove"].ToString()) == false)
                {
                    Chkautoprcss.Checked = false;
                }
                else
                {
                    Chkautoprcss.Checked = true;
                }
                if (dt.Rows[0]["DocumentEmailDownloadDefaultPropId"].ToString() != "")
                {
                    ddlbusiness_SelectedIndexChanged(sender, e);
                    Session["DocumentEmailDownloadDefaultPropId"] = dt.Rows[0]["DocumentEmailDownloadDefaultPropId"].ToString();
                    rbtnlistsetrules.SelectedValue = "Set Fixed Document Process Rule for This Email Account";
                    pnlsetrule.Visible = true;
                    ddldoctype.SelectedValue = dt.Rows[0]["DocumentTypeId"].ToString();
                    ddlparty.SelectedValue = dt.Rows[0]["PartyId"].ToString();
                    txtdoctitle.Text = Convert.ToString(dt.Rows[0]["DocumentTittle"].ToString());
                    txtdocdesc.Text = Convert.ToString(dt.Rows[0]["DocumentDescription"].ToString());
                    ddldt.SelectedIndex = ddldt.Items.IndexOf(ddldt.Items.FindByValue(Convert.ToString(dt.Rows[0]["DocTypenm"])));
                    if (Convert.ToBoolean(dt.Rows[0]["DocTittleOrEmailSub"].ToString()) == false)
                    {
                        chkboxoremail.Checked = false;
                    }
                    else
                    {
                        chkboxoremail.Checked = true;
                    }
                    
                }
                imgbtnUpdate.Visible = true;
               // imgbtnReset.Visible = true;
               
            }

        }
       
    }
    protected void GridEmail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridEmail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridEmail.EditIndex = e.NewEditIndex;
        //FillGrid();
    }

    public Int32 InsertDocumentEmailDownloadMaster(string ServerName, string EmailId, string Password, bool DocumentAutoApprove, String RuleType, String Whid,String AutoInterval)
    {
      SqlCommand  cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentEmailDownloadMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@ServerName", SqlDbType.NVarChar));
        cmd.Parameters["@ServerName"].Value = ServerName;

        cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar));
        cmd.Parameters["@EmailId"].Value = EmailId;

        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
        cmd.Parameters["@Password"].Value = Password;

        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Direction = ParameterDirection.Output;

        cmd.Parameters.Add(new SqlParameter("@DocumentAutoApprove", SqlDbType.Bit));
        cmd.Parameters["@DocumentAutoApprove"].Value = DocumentAutoApprove;

        cmd.Parameters.Add(new SqlParameter("@RuleType", SqlDbType.VarChar));
        cmd.Parameters["@RuleType"].Value = RuleType;

        cmd.Parameters.Add(new SqlParameter("@AutoRetrival", SqlDbType.NVarChar));
        cmd.Parameters["@AutoRetrival"].Value = AutoInterval;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;


        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@DocumentEmailDownloadID"].Value);
        return (result);
    }

    public Int32 InsertDocumentEmailDownloadDefaultProp(Int32 DocumentEmailDownloadID, string DocumentTittle, Int32 DocumentTypeId, Int32 PartyId, string DocumentDescription, bool DocTittleOrEmailSub, string AutoInterval, String Docyt)
    {
      SqlCommand  cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentEmailDownloadDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Value = DocumentEmailDownloadID;

        cmd.Parameters.Add(new SqlParameter("@DocumentTittle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTittle"].Value = DocumentTittle;

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;

        cmd.Parameters.Add(new SqlParameter("@DocumentDescription", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentDescription"].Value = DocumentDescription;

        cmd.Parameters.Add(new SqlParameter("@DocTittleOrEmailSub", SqlDbType.Bit));
        cmd.Parameters["@DocTittleOrEmailSub"].Value = DocTittleOrEmailSub;


        cmd.Parameters.Add(new SqlParameter("@DocTypenm", SqlDbType.NVarChar));
        cmd.Parameters["@DocTypenm"].Value = Docyt;

        cmd.Parameters.Add(new SqlParameter("@AutoInterval", SqlDbType.NVarChar));
        cmd.Parameters["@AutoInterval"].Value = AutoInterval;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }

    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {
        bool docautoapprove = Chkautoprcss.Checked;

        
       
        if (rbtnlistsetrules.SelectedValue.ToString() == "Set Dynamic Document Process Rule for This Email Account")
        {
            bool access = UserAccess.Usercon("DocumentEmailDownloadMaster", "", "DocumentEmailDownloadID", "", "", "DocumentEmailDownloadMaster.CID", "DocumentEmailDownloadMaster");
            if (access == true)
            {
                int i = InsertDocumentEmailDownloadMaster(txtServer.Text, txtEmail.Text, txtPwd.Text, docautoapprove, "Dynamic", ddlbusiness.SelectedValue, txtautoretrival.Text);
                if (i > 0)
                {
                  //  pnlmsg.Visible = true;
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully";
                    FillGrid();
                    txtEmail.Text = "";
                    txtPwd.Text = "";
                    txtServer.Text = "";
                    txtautoretrival.Text = "";
                    txtPwd.Attributes.Clear();
                }
            }
            else
            {
              
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You don't have permission to insert more record according to your price plan";
            }
        }
        else
        {
            bool access = UserAccess.Usercon("DocumentEmailDownloadMaster", "", "DocumentEmailDownloadID", "", "", "DocumentEmailDownloadMaster.CID", "DocumentEmailDownloadMaster");
            if (access == true)
            {
                pnlsetrule.Visible = true;
                int i = InsertDocumentEmailDownloadMaster(txtServer.Text, txtEmail.Text, txtPwd.Text, docautoapprove, "Fixed", ddlbusiness.SelectedValue, txtautoretrival.Text);

                
                if (i > 0)
                {

                    string doctitle = txtdoctitle.Text;
                    int doctypeid = Convert.ToInt32(ddldoctype.SelectedValue);
                    int partyid = Convert.ToInt32(ddlparty.SelectedValue);
                    string desc = txtdocdesc.Text;
                    bool doctitleoremailsub = chkboxoremail.Checked;// DocTittleOrEmailSub

                    int j = InsertDocumentEmailDownloadDefaultProp(i, doctitle, doctypeid, partyid, desc, doctitleoremailsub, txtautoretrival.Text,ddldt.SelectedValue);
                    if (j > 0)
                    {
                       // pnlmsg.Visible = true;
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully";
                        FillGrid();
                        txtEmail.Text = "";
                        txtPwd.Text = "";
                        txtServer.Text = "";
                        txtdocdesc.Text = "";
                        txtdoctitle.Text = "";
                        txtPwd.Attributes.Clear();
                        txtautoretrival.Text = "";
                        
                       
                        pnlsetrule.Visible = false;
                    }
                }
            }
            else
            {
              //  pnlmsg.Visible = true;
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You don't have permission to insert more record according to your price plan";
            }
        }

        FillGrid();
      
        //rbtnlistsetrules.SelectedValue = "No";
        rbtnlistsetrules.SelectedValue = "Set Dynamic Document Process Rule for This Email Account";
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            Panel1.Visible = false;
            //txtEmail.Enabled = false;
            //txtPwd.Enabled = false;
            txtServer.Enabled = false;
            imgbtnsubmit.Enabled = false;
            //Chkautoprcss.Enabled = false;
           
            //rbtnlistsetrules.Enabled = false;
            txtPwd.Attributes.Add("Value", txtPwd.Text);
            ViewState["ps"] = txtPwd.Text;
            txtEmail.Visible = true;
            txtPwd.Visible = true;
            txtServer.Visible = true;
            imgbtnsubmit.Visible = false;
            //Chkautoprcss.Visible = false;
            //rbtnlistsetrules.Visible = false;
            //pnlsetrule.Visible = false;
        }
        else
        {
            if (Convert.ToString(ViewState["ps"]) != "")
            {
                txtPwd.Attributes.Add("Value", Convert.ToString(ViewState["ps"]));
            }
            Panel1.Visible = true;
            //txtEmail.Visible = true;
            //txtPwd.Visible = true;
            txtServer.Visible = true;
            if (imgbtnUpdate.Visible == true)
            {
                imgbtnsubmit.Visible = false;
            }
            else
            {
                imgbtnsubmit.Visible = true;
            }
            //Chkautoprcss.Visible = true;
            //rbtnlistsetrules.Visible = true;

            txtEmail.Enabled = true;
            txtPwd.Enabled = true;
            txtServer.Enabled = true;
            imgbtnsubmit.Enabled = true;
            //Chkautoprcss.Enabled = true;
            //rbtnlistsetrules.Enabled = true;
        }
        int j = clscompany.InsertCompanyMasterEmail(Convert.ToBoolean(RadioButtonList1.SelectedIndex));

    }
    public void FillGrid()
    {
        lblCompany.Text = DropDownList1.SelectedItem.Text;

        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentEmailDownloadMaster(DropDownList1.SelectedValue);


        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }


        GridEmail.DataSource = dt;
        GridEmail.DataBind();
        //setGridisze();

    }
    protected void GridEmail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        
    }





    protected void UpdateDocEmailmaster()
    {
        Int32 emaildownid;
        String servername;
        string ruletype="";
        emaildownid = Convert.ToInt32(Session["DocumentEmailDownloadID"].ToString());
        servername = txtServer.Text;
        if (rbtnlistsetrules.SelectedItem.Value == "Set Fixed Document Process Rule for This Email Account")
        {
            ruletype="Fixed";
        }
        else if(rbtnlistsetrules.SelectedItem.Value == "Set Dynamic Document Process Rule for This Email Account")
        {
            ruletype="Dynamic";
        }
        bool access = UserAccess.Usercon("DocumentEmailDownloadMaster", "", "DocumentEmailDownloadID", "", "", "DocumentEmailDownloadMaster.CID", "DocumentEmailDownloadMaster");
        if (access == true)
        {
            bool scs_ftp = clsDocument.UpdateDocumentEmailDownloadMaster(emaildownid, servername, Convert.ToBoolean(Chkautoprcss.Checked), ruletype, ddlbusiness.SelectedValue, txtPwd.Text, txtEmail.Text, txtautoretrival.Text);
            if (Convert.ToInt32(scs_ftp) > 0)
            {
                
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";

            }
        }
        else
        {
           // pnlmsg.Visible = true; 
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, You don't permited greter record to priceplan";
        }

    }
    protected void UpdateDocEmailde_prop()
    {

        Int32 de_prop_id, doctypeid, partyid;
        String doctitle, docdesc;

        doctypeid = Convert.ToInt32(ddldoctype.SelectedValue);
        de_prop_id = Convert.ToInt32(Session["DocumentEmailDownloadDefaultPropId"].ToString());
        partyid = Convert.ToInt32(ddlparty.SelectedValue);
        doctitle = txtdoctitle.Text;
        docdesc = txtdocdesc.Text;

        bool doctitleoremailsub = chkboxoremail.Checked;// DocTittleOrEmailSub

        if (de_prop_id.ToString() != null)
        {
            bool scs_def_prop = clsDocument.UpdateDocumentEmailDownloadDefaultProp(de_prop_id, doctitle, doctypeid, partyid, docdesc, doctitleoremailsub, ddldt.SelectedValue);
        }
       
    }
    protected void imgbtnUpdate_Click(object sender, EventArgs e)
    {
        UpdateDocEmailmaster();
        
        if (rbtnlistsetrules.SelectedItem.Value == "Set Fixed Document Process Rule for This Email Account")
        {
            if (Session["DocumentEmailDownloadDefaultPropId"] != null)
            {
                UpdateDocEmailde_prop();
            }
            else if (Session["DocumentEmailDownloadDefaultPropId"] == null)
            {
                Int32 doctypeid, partyid;
                String doctitle, docdesc;

                doctypeid = Convert.ToInt32(ddldoctype.SelectedValue);
                //de_prop_id = Convert.ToInt32(Session["DocumentEmailDownloadDefaultPropId"].ToString());
                partyid = Convert.ToInt32(ddlparty.SelectedValue);
                doctitle = txtdoctitle.Text;
                docdesc = txtdocdesc.Text;
                bool doctitleoremailsub = chkboxoremail.Checked;// DocTittleOrEmailSub


                int i = Convert.ToInt32(Session["DocumentEmailDownloadID"].ToString());

                int j = InsertDocumentEmailDownloadDefaultProp(i, doctitle, doctypeid, partyid, docdesc, doctitleoremailsub, txtautoretrival.Text, ddldt.SelectedValue);
                if (j > 0)
                {
                   // pnlmsg.Visible = true;
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record updated successfully";
                    FillGrid();
                    txtEmail.Text = "";
                    txtPwd.Text = "";
                    txtServer.Text = "";
                    txtdocdesc.Text = "";
                    txtdoctitle.Text = "";
                    txtPwd.Attributes.Clear();
                    txtautoretrival.Text = "";
                    
                    pnlsetrule.Visible = false;
                }
            }
        
        }
        else if (rbtnlistsetrules.SelectedItem.Value == "Set Dynamic Document Process Rule for This Email Account")
        {
            int result = DeleteDocumentEmailDownloadDefaultProp(Convert.ToInt32(Session["DocumentEmailDownloadID"].ToString()));
            txtdocdesc.Text = "";
            txtdoctitle.Text = "";
            ddldoctype.SelectedIndex = 0;
            ddlparty.SelectedIndex = 0;
        }

        imgbtnsubmit.Visible = true;
        imgbtnUpdate.Visible = false;
        
        Session["DocumentEmailDownloadDefaultPropId"] = null;
        Session["DocumentEmailDownloadID"] = null;
        FillGrid();
        txtEmail.Visible = true;
        txtPwd.Visible = true;
        clear();
        pnlsetrule.Visible = false;
        rbtnlistsetrules.SelectedValue = "Set Dynamic Document Process Rule for This Email Account";
    }
    protected void imgbtnReset_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        clear();
        pnlsetrule.Visible = false;
        rbtnlistsetrules.SelectedValue = "Set Dynamic Document Process Rule for This Email Account";
        txtPwd.Visible = true;
        txtEmail.Visible = true;
        imgbtnsubmit.Visible = true;
        imgbtnUpdate.Visible = false;
       // imgbtnReset.Visible = false;
        //lblEmailId.Visible = false;
        //lblEmailId.Visible = false;
    }
    protected void clear()
    {   
        txtPwd.Attributes.Clear();
        txtautoretrival.Text = "";
        txtdocdesc.Text = "";
        txtdoctitle.Text = "";
        txtServer.Text = "";
        txtPwd.Text = "";
        txtEmail.Text = "";
        imgbtnsubmit.Enabled = true;
        imgbtnUpdate.Visible = false;
        //lblEmailId.Text = "";
        Chkautoprcss.Checked = false;
        RadioButtonList1.SelectedIndex = 0;
        object sender=new object();
        EventArgs e=new EventArgs();
        RadioButtonList1_SelectedIndexChanged(sender, e);
    }
   
    protected void rbtnlistsetrules_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnlistsetrules.SelectedValue == "Set Fixed Document Process Rule for This Email Account")
        {
            pnlsetrule.Visible = true;
        }
        else
        {
            txtdoctitle.Text = "";
            ddldoctype.SelectedIndex = 0;
            ddlparty.SelectedIndex = 0;
            txtdocdesc.Text = "";
            pnlsetrule.Visible = false;
        }
    }
    protected void GridEmail_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
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
    //protected void ibtnCancelCabinetAdd_Click(object sender, ImageClickEventArgs e)
    //{
    //    ModalPopupExtender9.Hide();
    //}
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    ModalPopupExtender9.Show();
    //}
    protected void imgconfirmok_Click(object sender, EventArgs e)
    {
        mdlpopupconfirm.Hide();

        int result = clscompany.DeleteDocumentEmailDownloadMaster(Convert.ToInt32(hdncnfm.Value));
        if (result > 0)
        {
          
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";
        }
        FillGrid();
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
        FillDocumentTypeAll();
        FillParty();
    }

    protected void imgconfirmcalcel_Click(object sender, EventArgs e)
    {
        mdlpopupconfirm.Hide();
    }
    protected void imgAdd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentSubSubType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

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
    protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillDocumentTypeAll();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCompany.Text =  DropDownList1.SelectedItem.Text;
        FillGrid();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            Panel11.ScrollBars = ScrollBars.None;
            Panel11.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;
            if (GridEmail.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridEmail.Columns[6].Visible = false;
            }
            if (GridEmail.Columns[7].Visible == true)
            {
                ViewState["delHide"] = "tt";
                GridEmail.Columns[7].Visible = false;
            }
        }
        else
        {
            //Panel11.ScrollBars = ScrollBars.Vertical;
            //Panel11.Height = new Unit(200);

            Button2.Text = "Printable Version";
            Button1.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridEmail.Columns[6].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                GridEmail.Columns[7].Visible = true;
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
    protected DataTable select(string std)
    {
        SqlDataAdapter cidco = new SqlDataAdapter(std, con);
        DataTable dts = new DataTable();
        cidco.Fill(dts);
        return dts;
    }

    protected void txtPwd_TextChanged(object sender, EventArgs e)
    {

    }
}





















