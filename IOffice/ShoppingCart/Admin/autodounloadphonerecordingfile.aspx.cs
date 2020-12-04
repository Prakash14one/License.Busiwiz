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

public partial class ShoppingCart_Admin_AddNewServiceCall : System.Web.UI.Page
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


        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            lblcomid.Text = Session["Cname"].ToString();
            flaganddoc();
            //lblcomname.Text = "All";
            string str = "SELECT WareHouseId,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlbusiness.DataSource = dt;
                ddlbusiness.DataTextField = "Name";
                ddlbusiness.DataValueField = "WareHouseId";
                ddlbusiness.DataBind();
                ddlstore.DataSource = dt;


                ddlstore.DataTextField = "Name";
                ddlstore.DataValueField = "WareHouseId";
                ddlstore.DataBind();
                ddlstore.Items.Insert(0, "All");
                ddlstore.Items[0].Value = "0";
                string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
                SqlCommand cmdeeed = new SqlCommand(eeed, con);
                SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                DataTable dteeed = new DataTable();
                adpeeed.Fill(dteeed);
                if (dteeed.Rows.Count > 0)
                {
                    ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                }

            }
            RadioButtonList1_SelectedIndexChanged(sender, e);
            FillDocumentTypeAll();
            FillParty();
            FillGrid();

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

    }
    protected void FillGrid()
    {
        lblcomname.Text = ddlstore.SelectedItem.Text;
        DataTable dt = new DataTable();


        string st3 = "";
        if (ddlstore.SelectedValue == "0")
        {
            st3 = " SELECT  WarehouseMaster.Name as Wname,FTPMaster.FtpId as FtpId,AutoRetrival, FTPMaster.FTP , FTPMaster.Username, FTPMaster.Password,FTPMaster.Ftppath,case when (FTPMaster.DocumentAutoApprove ='1') then 'Yes' else 'No' End as DocumentAutoApprove, FTPMasterDefaultProp.FTPMasterDefaultPropId, FTPMasterDefaultProp.DocumentTitle as DocumentTittle,  " +
             " FTPMasterDefaultProp.DocumentTypeId, FTPMasterDefaultProp.PartyId, FTPMasterDefaultProp.DocumentDescription, DocumentMainType.DocumentMainType    + ' - ' + DocumentSubType.DocumentSubType + ' -  ' + DocumentType.DocumentType  AS DocTypeAll,FTPMaster.Ftppath as FolderName,FTPMaster.DocumentAutoApprove,FTPMaster.RuleType " +
             ", location FROM     DocumentMainType RIGHT OUTER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId RIGHT OUTER JOIN DocumentType ON DocumentSubType.DocumentSubTypeId = DocumentType.DocumentSubTypeId  RIGHT OUTER JOIN  FTPMasterDefaultProp on DocumentType.DocumentTypeId = FTPMasterDefaultProp.DocumentTypeId  RIGHT OUTER JOIN FTPMaster ON FTPMasterDefaultProp.FTPMasterId = FTPMaster.FtpId inner join WarehouseMaster on WarehouseMaster.WarehouseId=FTPMaster.Whid " +
             " WHERE  (FTPMaster.location IS NOT NULL) and   (FTPMaster.CID ='" + Session["Comid"] + "')";
        }
        else
        {
            st3 = " SELECT  WarehouseMaster.Name as Wname,FTPMaster.FtpId as FtpId,AutoRetrival, FTPMaster.FTP , FTPMaster.Username, FTPMaster.Password,FTPMaster.Ftppath,case when (FTPMaster.DocumentAutoApprove ='1') then 'Yes' else 'No' End as DocumentAutoApprove, FTPMasterDefaultProp.FTPMasterDefaultPropId, FTPMasterDefaultProp.DocumentTitle as DocumentTittle,  " +
             " FTPMasterDefaultProp.DocumentTypeId, FTPMasterDefaultProp.PartyId, FTPMasterDefaultProp.DocumentDescription, DocumentMainType.DocumentMainType    + ' - ' + DocumentSubType.DocumentSubType + ' -  ' + DocumentType.DocumentType  AS DocTypeAll,FTPMaster.Ftppath as FolderName,FTPMaster.DocumentAutoApprove,FTPMaster.RuleType " +
             ", location FROM     DocumentMainType RIGHT OUTER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId RIGHT OUTER JOIN DocumentType ON DocumentSubType.DocumentSubTypeId = DocumentType.DocumentSubTypeId  RIGHT OUTER JOIN  FTPMasterDefaultProp on DocumentType.DocumentTypeId = FTPMasterDefaultProp.DocumentTypeId  RIGHT OUTER JOIN FTPMaster ON FTPMasterDefaultProp.FTPMasterId = FTPMaster.FtpId inner join WarehouseMaster on WarehouseMaster.WarehouseId=FTPMaster.Whid " +
             " WHERE   (FTPMaster.location IS NOT NULL) and   (FTPMaster.CID ='" + Session["Comid"] + "' and FTPMaster.Whid='" + ddlstore.SelectedValue + "') ";

        }
        SqlCommand cmd3 = new SqlCommand(st3, con);
        SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);
        adp3.Fill(dt);

        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridFTP.DataSource = dt;

        GridFTP.DataBind();


    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (RadioButtonList1.SelectedIndex == 1)
        {
            txtFtpName.Visible = true;
            txtPwd.Visible = true;
            txtUsername.Visible = true;
            imgbtnsubmit.Visible = true;
            txtftpserver.Visible = true;
            Chkautoprcss.Visible = true;
            rbtnlistsetrules.Visible = true;
            txtautoretrival.Enabled = true;
            txtFtpName.Enabled = true;
            txtPwd.Enabled = true;
            txtUsername.Enabled = true;
            imgbtnsubmit.Enabled = true;
            txtftpserver.Enabled = true;
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
        }
        else
        {
            txtftpserver.Enabled = false;
            txtFtpName.Enabled = false;
            txtPwd.Enabled = false;
            txtUsername.Enabled = false;
            imgbtnsubmit.Enabled = false;
            Chkautoprcss.Enabled = false;
            rbtnlistsetrules.Enabled = false;
            txtautoretrival.Enabled = false;
            txtftpserver.Visible = true;
            txtFtpName.Visible = true;
            txtPwd.Visible = true;

            txtUsername.Visible = true;
            imgbtnsubmit.Visible = false;
            Chkautoprcss.Visible = true;
            rbtnlistsetrules.Visible = true;
            pnl_FtpAccount_priceplan.Visible = false;
            int j = clscompany.InsertCompanyMasterFTP(Convert.ToBoolean(RadioButtonList1.SelectedIndex));
        }
    }

    protected void RadioButtonList1_SelectedIndexChanged1(object sender, EventArgs e)
    {

            txtFtpName.Visible = true;
            txtPwd.Visible = true;
            txtUsername.Visible = true;
            imgbtnsubmit.Visible = true;
            txtftpserver.Visible = true;
            Chkautoprcss.Visible = true;
            rbtnlistsetrules.Visible = true;
            txtautoretrival.Enabled = true;
            txtFtpName.Enabled = true;
            txtPwd.Enabled = true;
            txtUsername.Enabled = true;
            imgbtnsubmit.Enabled = true;
            txtftpserver.Enabled = true;
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
       
    }
    protected void imgbtnsubmit_Click1(object sender, EventArgs e)
    {
        bool docautoapprove = Chkautoprcss.Checked;
        int j = clscompany.InsertCompanyMasterFTP(Convert.ToBoolean(RadioButtonList1.SelectedIndex));
        //if (rbtnlistsetrules.SelectedItem.Text == "Set Dynamic Document Process Rule for This FTP Account")
        if (rbtnlistsetrules.SelectedItem.Value == "Set Dynamic Document Process Rule for This FTP Account")
        {

                bool access = UserAccess.Usercon("FTPMaster", "", "FtpId", "", "", "FTPMaster.CID", "FTPMaster");
                if (access == true)
                {
                  // int i = clsDocument.InsertFTPMaster(txtftpserver.Text, txtUsername.Text, txtPwd.Text, txtFtpName.Text, docautoapprove, "Dynamic", ddlbusiness.SelectedValue, txtautoretrival.Text);
                    con.Close();
                    con.Open();
                    if (ddl_location.SelectedValue == "FTP")
                    {
                        SqlCommand cmd = new SqlCommand("Insert Into FTPMaster ( FTP, Username, Password, Ftppath, DocumentAutoApprove, CID, RuleType,  Whid, AutoRetrival, location) Values " +
                                                        "  ('" + txtftpserver.Text + "','" + txtUsername.Text + "','" + txtPwd.Text + "' ,'" + txtFtpName.Text + "','" + docautoapprove + "', '" + Session["Comid"].ToString() + "','Dynamic','" + ddlbusiness.SelectedValue + "','" + txtautoretrival.Text + "', 'FTP')", con);
                        cmd.ExecuteNonQuery();
                    
                    }
                    if (ddl_location.SelectedValue == "Folder")
                    {
                        SqlCommand cmd = new SqlCommand("Insert Into FTPMaster (DocumentAutoApprove, CID, RuleType,  Whid, AutoRetrival, location, folderpath) Values " +
                                                        "  ('" + docautoapprove + "', '" + Session["Comid"].ToString() + "','Dynamic','" + ddlbusiness.SelectedValue + "','" + txtautoretrival.Text + "', 'Folder', '"+ txt_folder.Text   +"')", con);
                        cmd.ExecuteNonQuery();
                    
                    }
                    //Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
                   
                    
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully.";
                    FillGrid();
                //}
                //else if (i == 2)
                //{
                //    pnlmsg.Visible = true;
                //    lblmsg.Text = "Record Updated Successfully.";
                //}
            }
        }
        else
        {
            pnlsetrule.Visible = true;
            if (RadioButtonList1.SelectedIndex == 1)
            {
                bool access = UserAccess.Usercon("FTPMaster", "", "FtpId", "", "", "FTPMaster.CID", "FTPMaster");
                if (access == true)
                {
                    int i = clsDocument.InsertFTPMaster(txtftpserver.Text, txtUsername.Text, txtPwd.Text, txtFtpName.Text, docautoapprove, "Fixed", ddlbusiness.SelectedValue, txtautoretrival.Text);

                    string doctitle = txtdoctitle.Text;
                    int doctypeid = Convert.ToInt32(ddldoctype.SelectedValue);
                    int partyid = Convert.ToInt32(ddlparty.SelectedValue);
                    string desc = txtdocdesc.Text;
                    int jj = clsDocument.InsertFTPMasterDefaultProp(i, doctitle, doctypeid, partyid, desc, ddldt.SelectedValue);

                    if (i > 0)
                    {

                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully.";
                        FillGrid();
                    }
                }
                else
                {
                    lblmsg.Visible = true;

                    lblmsg.Text = "Sorry, You don't permited greter record to priceplan";
                }
            }
        }
        imgbtnsubmit.Visible = true;
        imgbtnUpdate.Visible = false;

        Session["FTPMasterDefaultPropId"] = null;
        Session["FTPId"] = null;
        FillGrid();
        txtUsername.Visible = true;
        txtPwd.Visible = true;
        clear();
        RadioButtonList1.SelectedIndex = 0;
        RadioButtonList1_SelectedIndexChanged(sender, e);

        pnlsetrule.Visible = false;
        rbtnlistsetrules.SelectedValue = "Set Dynamic Document Process Rule for This FTP Account";


    }
    protected void rbtnlistsetrules_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rbtnlistsetrules.SelectedItem.Text == "Set Fixed Document Process Rule for This FTP Account")
        if (rbtnlistsetrules.SelectedItem.Value == "Set Fixed Document Process Rule for This FTP Account")
        {
            pnlsetrule.Visible = true;
        }
        else
        {
            pnlsetrule.Visible = false;
        }
        txtPwd.Attributes.Add("Value", txtPwd.Text);
    }

    protected void GridFTP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridFTP.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void GridFTP_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridFTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblmsg.Text = "";
        if (e.CommandName == "Sort")
        {
            return;
        }
        if (e.CommandName == "Delete1")
        {
            int indx = Convert.ToInt32(e.CommandArgument.ToString());
            Int32 datakey = indx;
            hdncnfm.Value = datakey.ToString();


            string st2 = "Delete from FTPMaster where FtpId='" + Convert.ToInt32(hdncnfm.Value) + "' ";
            SqlCommand cmd2 = new SqlCommand(st2, con);
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();



            string st3 = "select * from FTPMasterDefaultProp where FTPMasterId='" + Convert.ToInt32(hdncnfm.Value) + "'";
            SqlCommand cmd3 = new SqlCommand(st3, con);
            SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            adp3.Fill(dt3);
            if (dt3.Rows.Count > 0)
            {

            }
            else
            {
                string st21 = "Delete from FTPMasterDefaultProp where FTPMasterId='" + Convert.ToInt32(hdncnfm.Value) + "' ";
                SqlCommand cmd21 = new SqlCommand(st21, con);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd21.ExecuteNonQuery();
                con.Close();
            }


            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";

            FillGrid();


        }
        if (e.CommandName == "Edit")
        {
            int indx = Convert.ToInt32(e.CommandArgument.ToString());

            Int32 datakey = indx;
            Session["FTPId"] = datakey;
            DataTable dt = new DataTable();
            dt = clsDocument.SelectFTPMasterWithID(datakey);

            if (dt.Rows.Count > 0)
            {
                ddl_location.SelectedValue  = dt.Rows[0]["location"].ToString();
                if (ddl_location.SelectedValue == "FTP")
                {
                    pnl_main.Visible = true;
                    pnl_ftp.Visible = true;
                    pnl_folder.Visible = false;
                }
                if (ddl_location.SelectedValue == "Folder")
                {
                    txt_folder.Text = dt.Rows[0]["folderpath"].ToString();
                    pnl_main.Visible = true;
                    pnl_ftp.Visible = false;
                    pnl_folder.Visible = true;

                }
                if (ddl_location.SelectedValue == "0")
                {
                    pnl_main.Visible = false;

                }
                ddlbusiness.SelectedValue = dt.Rows[0]["Whid"].ToString();
                txtftpserver.Text = Convert.ToString(dt.Rows[0]["FTP"].ToString());
                txtFtpName.Text = Convert.ToString(dt.Rows[0]["Ftppath"].ToString());
                txtUsername.Visible = true;
                txtautoretrival.Text = Convert.ToString(dt.Rows[0]["AutoRetrival"].ToString());
                txtUsername.Text = Convert.ToString(dt.Rows[0]["Username"].ToString());
                lblUserName.Visible = false;
                txtPwd.Visible = true;
                txtPwd.Attributes.Add("Value", Convert.ToString(dt.Rows[0]["Password"].ToString()));
                imgbtnsubmit.Visible = false;
                pnlsetrule.Visible = false;
                rbtnlistsetrules.SelectedValue = "Set Dynamic Document Process Rule for This FTP Account";

                if (Convert.ToBoolean(dt.Rows[0]["DocumentAutoApprove"].ToString()) == false)
                {
                    Chkautoprcss.Checked = false;
                }
                else
                {
                    Chkautoprcss.Checked = true;
                }
                if (dt.Rows[0]["FTPMasterDefaultPropId"].ToString() != "")
                {
                    ddlbusiness_SelectedIndexChanged(sender, e);
                    Session["FTPMasterDefaultPropId"] = dt.Rows[0]["FTPMasterDefaultPropId"].ToString();
                    rbtnlistsetrules.SelectedValue = "Set Fixed Document Process Rule for This FTP Account";
                    pnlsetrule.Visible = true;
                    ddldoctype.SelectedValue = dt.Rows[0]["DocumentTypeId"].ToString();
                    ddlparty.SelectedIndex = ddlparty.Items.IndexOf(ddlparty.Items.FindByValue(Convert.ToString(dt.Rows[0]["PartyId"])));
                    txtdoctitle.Text = Convert.ToString(dt.Rows[0]["DocumentTitle"].ToString());
                    txtdocdesc.Text = Convert.ToString(dt.Rows[0]["DocumentDescription"].ToString());
                    ddldt.SelectedIndex = ddldt.Items.IndexOf(ddldt.Items.FindByValue(Convert.ToString(dt.Rows[0]["DocTypenm"])));

                    if (Convert.ToBoolean(dt.Rows[0]["DocumentAutoApprove"].ToString()) == false)
                    {
                        Chkautoprcss.Checked = false;
                    }
                    else
                    {
                        Chkautoprcss.Checked = true;
                    }
                }
                imgbtnUpdate.Visible = true;


                RadioButtonList1.SelectedIndex = 1;
                txtFtpName.Visible = true;
                txtPwd.Visible = true;
                txtUsername.Visible = true;
                pnl_FtpAccount_priceplan.Visible = true;

                txtautoretrival.Enabled = true;
                txtFtpName.Enabled = true;
                txtPwd.Enabled = true;
                txtUsername.Enabled = true;
                imgbtnsubmit.Enabled = true;
                txtftpserver.Enabled = true;
                Chkautoprcss.Enabled = true;
                rbtnlistsetrules.Enabled = true;
            }
        }

    }
    protected void GridFTP_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridFTP_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridFTP_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


    }

    protected void imgbtnUpdate_Click(object sender, EventArgs e)
    {

        UpdateFTPmaster();

        if (rbtnlistsetrules.SelectedItem.Value == "Set Fixed Document Process Rule for This FTP Account")
        {

            if (Session["FTPMasterDefaultPropId"] != null)
            {
                UpdateFTPde_prop();
            }
            else if (Session["FTPMasterDefaultPropId"] == null)
            {
                int i = Convert.ToInt32(Session["FTPId"].ToString());
                string doctitle = txtdoctitle.Text;
                int doctypeid = Convert.ToInt32(ddldoctype.SelectedValue);
                int partyid = Convert.ToInt32(ddlparty.SelectedValue);
                string desc = txtdocdesc.Text;
                int jj = clsDocument.InsertFTPMasterDefaultProp(i, doctitle, doctypeid, partyid, desc, ddldt.SelectedValue);

                if (i > 0)
                {

                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully.";
                }
            }
        }
        else if (rbtnlistsetrules.SelectedItem.Value == "Set Dynamic Document Process Rule for This FTP Account")
        {
            int result = clscompany.DeleteFTPMasterDefaultProp(Convert.ToInt32(Session["FTPId"].ToString()));
            txtdocdesc.Text = "";
            txtdoctitle.Text = "";
            ddldoctype.SelectedIndex = 0;
            ddlparty.SelectedIndex = 0;

        }



        imgbtnsubmit.Visible = true;
        imgbtnUpdate.Visible = false;

        Session["FTPMasterDefaultPropId"] = null;
        Session["FTPId"] = null;
        FillGrid();
        txtUsername.Visible = true;
        txtPwd.Visible = true;
        clear();
        RadioButtonList1.SelectedIndex = 0;
        RadioButtonList1_SelectedIndexChanged(sender, e);

        pnlsetrule.Visible = false;
        rbtnlistsetrules.SelectedValue = "Set Dynamic Document Process Rule for This FTP Account";
    }
    protected void UpdateFTPmaster()
    {
        Int32 ftpid;
        String ftp;
        String ftppath;
        string ruletype = "";
        ftpid = Convert.ToInt32(Session["FTPId"].ToString());
        ftp = txtftpserver.Text;
        ftppath = txtFtpName.Text;
        if (rbtnlistsetrules.SelectedItem.Value == "Set Fixed Document Process Rule for This FTP Account")
        {
            ruletype = "Fixed";
        }
        else if (rbtnlistsetrules.SelectedItem.Value == "Set Dynamic Document Process Rule for This FTP Account")
        {
            ruletype = "Dynamic";
        }
        bool access = UserAccess.Usercon("FTPMaster", "", "FtpId", "", "", "FTPMaster.CID", "FTPMaster");
        if (access == true)
        {
          //  bool scs_ftp = clsDocument.UpdateFTPMaster(ftpid, ftp, ftppath, txtUsername.Text, txtPwd.Text, Convert.ToBoolean(Chkautoprcss.Checked), ruletype, ddlbusiness.SelectedValue, txtautoretrival.Text);

            if (ddl_location.SelectedValue == "FTP")
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE FTPMaster SET FTP ='" + txtFtpName.Text + "', Username ='" + txtUsername.Text + "', Password ='" + txtPwd.Text + "', Ftppath ='" + txtftpserver.Text + "', DocumentAutoApprove ='" + Convert.ToBoolean(Chkautoprcss.Checked) + "', AutoRetrival ='" + txtautoretrival.Text + "', location ='" + ddl_location.SelectedValue + "'  Where FTPid='" + ftpid + "' ", con);
                cmd.ExecuteNonQuery();

            }
            if (ddl_location.SelectedValue == "Folder")
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE       FTPMaster SET DocumentAutoApprove ='" + Convert.ToBoolean(Chkautoprcss.Checked) + "', AutoRetrival ='" + txtautoretrival.Text + "', location ='" + ddl_location.SelectedValue + "', folderpath ='" + txt_folder.Text + "'  Where FTPid='" + ftpid + "'", con);
                cmd.ExecuteNonQuery();

            }
                //  pnlmsg.Visible = true;
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";

            
        }
        else
        {
            //   pnlmsg.Visible = true;
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, You don't permited greter record to priceplan";
        }

    }
    protected void UpdateFTPde_prop()
    {

        Int32 de_prop_id, doctypeid, partyid;
        String doctitle, docdesc;

        doctypeid = Convert.ToInt32(ddldoctype.SelectedValue);
        de_prop_id = Convert.ToInt32(Session["FTPMasterDefaultPropId"].ToString());
        partyid = Convert.ToInt32(ddlparty.SelectedValue);
        doctitle = txtdoctitle.Text;
        docdesc = txtdocdesc.Text;
        bool scs_def_prop = clsDocument.UpdateFTPMasterDefaultProp(de_prop_id, doctitle, doctypeid, partyid, docdesc, ddldt.SelectedValue);
    }
    protected void clear()
    {
        txtautoretrival.Text = "";
        txtftpserver.Text = "";
        txtdocdesc.Text = "";
        txtdoctitle.Text = "";
        txtFtpName.Text = "";
        txtPwd.Attributes.Clear();
        txtUsername.Text = "";
        txtftpserver.Text = "";
        lblUserName.Text = "";
        Chkautoprcss.Checked = false;
    }

    protected void imgbtnReset_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";

        imgbtnsubmit.Visible = true;
        imgbtnUpdate.Visible = false;

        txtUsername.Visible = true;
        txtPwd.Visible = true;
        clear();
        RadioButtonList1.SelectedIndex = 0;
        RadioButtonList1_SelectedIndexChanged(sender, e);

        pnlsetrule.Visible = false;
        rbtnlistsetrules.SelectedValue = "Set Dynamic Document Process Rule for This FTP Account";
    }


    protected void GridFTP_Sorting(object sender, GridViewSortEventArgs e)
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

    protected void ibtnCancelCabinetAdd_Click(object sender, EventArgs e)
    {
        ModalPopupExtender9.Hide();
    }
    protected void ddlbusiness_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddl_location.SelectedValue == "FTP")
        {
            pnl_main.Visible = true; 
            pnl_ftp.Visible = true;
            pnl_folder.Visible = false;
        }
        if (ddl_location.SelectedValue == "Folder")
        {
            pnl_main.Visible = true;
            pnl_ftp.Visible = false;
            pnl_folder.Visible = true;

        }
       if (ddl_location.SelectedValue == "0")
        {
            pnl_main.Visible = false; 
          
        }
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentTypeAll();
        FillParty();
        FillGrid();
    }
    protected void LinkButton152_Click(object sender, EventArgs e)
    {
        DocumentCls1 clsDocument = new DocumentCls1();
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocTypeAll(ddlbusiness.SelectedValue);
        ddldoctype.DataSource = dt;
        ddldoctype.DataBind();
    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  lblcomname.Text =  ddlstore.SelectedItem.Text;
        FillGrid();
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
    protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillDocumentTypeAll();
    }
    protected void imgRefresh2_Click(object sender, ImageClickEventArgs e)
    {
        FillParty();
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {


            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;
            if (GridFTP.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridFTP.Columns[6].Visible = false;
            }
            if (GridFTP.Columns[7].Visible == true)
            {
                ViewState["delHide"] = "tt";
                GridFTP.Columns[7].Visible = false;
            }
        }
        else
        {



            Button2.Text = "Printable Version";
            Button1.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridFTP.Columns[6].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                GridFTP.Columns[7].Visible = true;
            }
        }
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
}
