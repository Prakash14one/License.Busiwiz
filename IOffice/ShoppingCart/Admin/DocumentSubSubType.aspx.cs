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

public partial class ShoppingCart_Admin_DocumentSubSubType : System.Web.UI.Page
{

    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    int key = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        if (!IsPostBack)
        {

            lblBusiness0.Text = Session["Cname"].ToString();
            ViewState["sortOrder"] = "";
            lblmsg.Text = "";

            if (Request.QueryString["Id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);

                string strmasteridver = " select DocumentMainType.*,DocumentSubType.DocumentSubTypeId from DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId where DocumentSubType.DocumentSubTypeId='" + id + "'";
                SqlCommand cmdmasteridver = new SqlCommand(strmasteridver, con);
                DataTable dtmasteridver = new DataTable();
                SqlDataAdapter adpmasteridver = new SqlDataAdapter(cmdmasteridver);
                adpmasteridver.Fill(dtmasteridver);

                if (dtmasteridver.Rows.Count > 0)
                {

                    fillbusiness();
                    ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dtmasteridver.Rows[0]["Whid"].ToString()));

                    Fillddldocmaintype();
                    ddldocmaintype.SelectedIndex = ddldocmaintype.Items.IndexOf(ddldocmaintype.Items.FindByValue(dtmasteridver.Rows[0]["DocumentMainTypeId"].ToString()));

                    ddldocumentsubtype();
                    ddldocsubtypename.SelectedIndex = ddldocsubtypename.Items.IndexOf(ddldocsubtypename.Items.FindByValue(dtmasteridver.Rows[0]["DocumentSubTypeId"].ToString()));
                    filldesright();
                    fillright();
                    btnadd_Click(sender, e);
                }




            }
            else
            {
                fillbusiness();
                Fillddldocmaintype();
                ddldocumentsubtype();
                filldesright();
                fillright();

            }
            fillfilterbusiness();
            filterbycabinet();
            fillfilterddlsubtype();



            FillDocumentType();

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





    protected void FillDocumentType()
    {

        lblBusiness.Text = DropDownList1.SelectedItem.Text;
        lblCabinet.Text = DropDownList2.SelectedItem.Text;
        lblDrawer.Text = DropDownList3.SelectedItem.Text;

        string str145 = "";
        string st1 = "";
        string st2 = "";
        string st3 = "";

        if (DropDownList1.SelectedIndex > 0)
        {
            st1 = " and DocumentMainType.Whid='" + DropDownList1.SelectedValue + "'";
        }
        if (DropDownList2.SelectedIndex > 0)
        {
            st2 = " and DocumentMainType.DocumentMainTypeId='" + DropDownList2.SelectedValue + "'";
        }
        if (DropDownList3.SelectedIndex > 0)
        {
            st3 = " and DocumentSubType.DocumentSubTypeId ='" + DropDownList3.SelectedValue + "'";
        }

        str145 = "  WareHouseMaster.Name,WareHouseMaster.WareHouseId ,DocumentMainType.DocumentMainTypeId, DocumentMainType.DocumentMainType, DocumentType.DocumentTypeId, DocumentType.DocumentType, DocumentSubType.DocumentSubTypeId, DocumentSubType.DocumentSubType FROM DocumentType inner join DocumentSubType ON DocumentType.DocumentSubTypeId = DocumentSubType.DocumentSubTypeId   inner join DocumentMainType  on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=DocumentMainType.Whid where DocumentType.CID='" + Session["Comid"] + "' " + st1 + " " + st2 + " " + st3 + " ";

        string strmak = " select Count(DocumentType.DocumentTypeId) as ci FROM DocumentType inner join DocumentSubType ON DocumentType.DocumentSubTypeId = DocumentSubType.DocumentSubTypeId   inner join DocumentMainType  on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=DocumentMainType.Whid where DocumentType.CID='" + Session["Comid"] + "' " + st1 + " " + st2 + " " + st3 + " ";


        gridocsubsubtype.VirtualItemCount = GetRowCount(strmak);

        string sortExpression = " WareHouseMaster.Name,DocumentMainType.DocumentMainType,DocumentSubType.DocumentSubType,DocumentType.DocumentType ";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(gridocsubsubtype.PageIndex, gridocsubsubtype.PageSize, sortExpression, str145);

            gridocsubsubtype.DataSource = dt;

            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            gridocsubsubtype.DataBind();
        }

        else
        {
            gridocsubsubtype.DataSource = null;
            gridocsubsubtype.DataBind();
        }

        //string orderby = "Order by WareHouseMaster.Name,DocumentMainType.DocumentMainType,DocumentSubType.DocumentSubType,DocumentType.DocumentType ";

        //string strfinal = str145 + st1 + st2 + st3 + orderby;
        //SqlCommand cgw = new SqlCommand(strfinal, con);
        //SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        //DataTable dt = new DataTable();
        //adgw.Fill(dt);
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
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));
        dt.Columns.Remove("ROW_NUM");
        return dt;

        ViewState["dt"] = dt;
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
        if (ddldocsubtypename.SelectedIndex < 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please select Document Sub Type";

        }

        else
        {
            try
            {

                string st1 = "  select DocumentType.* from  DocumentType inner join DocumentSubType on  DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId  inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId  where DocumentSubType.DocumentMainTypeId='" + ddldocmaintype.SelectedValue + "' and DocumentSubType.DocumentSubTypeId='" + ddldocsubtypename.SelectedValue + "' and DocumentType.DocumentType='" + txtdocsubsubtypename.Text + "' and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' ";
                SqlCommand cmd1 = new SqlCommand(st1, con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record already exist";
                }
                else
                {
                    bool access = UserAccess.Usercon("DocumentType", "", "DocumentTypeId", "", "", "DocumentType.CID", "DocumentType inner join DocumentSubType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId");

                    if (access == true)
                    {
                        bool access1 = UserAccess.Usercon("DocumentType", ddldocsubtypename.SelectedValue, "DocumentTypeId", "", "", "DocumentType.CID", "DocumentType inner join DocumentSubType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId");
                        if (access1 == true)
                        {


                            Int32 rst = clsDocument.InsertDocumentType1(Convert.ToInt32(ddldocsubtypename.SelectedValue), txtdocsubsubtypename.Text.ToUpper());
                            if (rst > 0)
                            {
                                if (chkdesright.Checked == true)
                                {

                                    foreach (GridViewRow item in grdacc.Rows)
                                    {
                                        string desid = grdacc.DataKeys[item.RowIndex].Value.ToString();
                                        CheckBox chkview = (CheckBox)(item.FindControl("chkview"));
                                        CheckBox chkdelete = (CheckBox)(item.FindControl("chkdelete"));
                                        CheckBox chksave = (CheckBox)(item.FindControl("chksave"));
                                        CheckBox chkedit = (CheckBox)(item.FindControl("chkedit"));
                                        CheckBox chkemail = (CheckBox)(item.FindControl("chkemail"));
                                        CheckBox chkMessage = (CheckBox)(item.FindControl("chkMessage"));
                                        Label lblcode = (Label)(item.FindControl("lblcode"));
                                        if (lblcode.Text.ToString() == "1")
                                        {
                                            if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                            {
                                                string strallbusdet = "Update DocumentAccessRighallBus set ViewAccess='" + chkview.Checked + "',DeleteAccess='" + chkdelete.Checked + "',SaveAccess='" + chksave.Checked + "',EditAccess='" + chkedit.Checked + "',EmailAccess='" + chkemail.Checked + "',MessageAccess='" + chkMessage.Checked + "' where DesignationId='" + desid + "' and CID='" + Session["Comid"] + "'";
                                                SqlCommand cmdallbusdel = new SqlCommand(strallbusdet, con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmdallbusdel.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }
                                        else if (lblcode.Text.ToString() == "2")
                                        {
                                            if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                            {
                                                string strallbusdet = "Update DocumentAccessRightforbusallCabinet set ViewAccess='" + chkview.Checked + "',DeleteAccess='" + chkdelete.Checked + "',SaveAccess='" + chksave.Checked + "',EditAccess='" + chkedit.Checked + "',EmailAccess='" + chkemail.Checked + "',MessageAccess='" + chkMessage.Checked + "' where DesignationId='" + desid + "' and Whid='" + ddlbusiness.SelectedValue + "'";
                                                SqlCommand cmdallbusdel = new SqlCommand(strallbusdet, con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmdallbusdel.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }
                                        else if (lblcode.Text.ToString() == "3")
                                        {
                                            if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                            {
                                                int rst11 = InsertCabinetAccessRightMaster(Convert.ToInt32(ddldocmaintype.SelectedValue), Convert.ToInt32(desid), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked, "1");
                                            }
                                        }
                                        else if (lblcode.Text.ToString() == "4")
                                        {
                                            if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                            {
                                                int rst11 = InsertDrawerAccessRightMaster(Convert.ToInt32(ddldocsubtypename.SelectedValue), Convert.ToInt32(desid), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked, "1");
                                            }
                                        }
                                        else if (lblcode.Text.ToString() == "5" || lblcode.Text.ToString() == "0")
                                        {
                                            if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                            {
                                                int rst1 = clsDocument.InsertDocumentAccessRightMaster(rst, Convert.ToInt32(desid), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked);

                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DataTable dtc = new DataTable();
                                    dtc = select("select * from DocumentAccessRighallBus where AllbusAccess='1' and DesignationId='" + Session["DesignationId"] + "' and CID='" + Session["Comid"] + "'");
                                    if (dtc.Rows.Count == 0)
                                    {
                                        dtc = select("select * from DocumentAccessRightforbusallCabinet where CabinetAccess='1' and DesignationId='" + Session["DesignationId"] + "' and Whid='" + ddlbusiness.SelectedValue + "'");
                                        if (dtc.Rows.Count == 0)
                                        {
                                            dtc = select("select * from CabinetAccessRightsMaster where CabinetId='" + ddldocmaintype.SelectedValue + "' and  DesignationId='" + Session["DesignationId"] + "' ");
                                            if (dtc.Rows.Count == 0)
                                            {
                                                dtc = select("select * from DrawerAccessRightsMaster where DrawerId='" + ddldocsubtypename.SelectedValue + "' and  DesignationId='" + Session["DesignationId"] + "' ");

                                            }
                                        }
                                    }
                                    if (dtc.Rows.Count > 0)
                                    {
                                        int rst1 = clsDocument.InsertDocumentAccessRightMaster(rst, Convert.ToInt32(Session["DesignationId"]), Convert.ToBoolean(1), Convert.ToBoolean(dtc.Rows[0]["ViewAccess"]), Convert.ToBoolean(dtc.Rows[0]["DeleteAccess"]), Convert.ToBoolean(dtc.Rows[0]["SaveAccess"]), Convert.ToBoolean(dtc.Rows[0]["EditAccess"]), Convert.ToBoolean(dtc.Rows[0]["EmailAccess"]), Convert.ToBoolean(true), Convert.ToBoolean(dtc.Rows[0]["MessageAccess"]));

                                    }
                                }
                                lblmsg.Visible = true;
                                lblmsg.Text = "Record inserted successfully";
                                ViewState["MasterId"] = "";
                                FillDocumentType();

                                ddldocsubtypename.SelectedIndex = -1;
                                txtdocsubsubtypename.Text = "";

                                pnladd.Visible = false;
                                Label12.Visible = false;
                                btnadd.Visible = true;
                                Label17.Text = "Add New Document Folder";
                                Label17.Visible = false;

                                if (CheckBox1.Checked == true)
                                {
                                    string te1 = "DocumentUploadGuide.aspx";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te1 + "');", true);
                                }
                            }
                        }
                        else
                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = "Sorry, You don't permitted greater record in this Drawer into priceplan";
                        }
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Sorry, You don't permitted greater record to priceplan";
                    }
                }
            }
            catch (Exception es)
            {
                Response.Write(es.Message.ToString());
            }
        }
        FillDocumentType();
    }
    public Int32 InsertCabinetAccessRightMaster(Int32 CabinetId, Int32 DesignationId, bool PrintAccess, bool ViewAccess,
bool DeleteAccess, bool SaveAccess, bool EditAccess, bool EmailAccess, bool FaxAccess, bool MessageAccess, string Cabty)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "InsertCabinetAccessRightMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@CabinetId", SqlDbType.Int));
        cmd.Parameters["@CabinetId"].Value = CabinetId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@PrintAccess", SqlDbType.Bit));
        cmd.Parameters["@PrintAccess"].Value = PrintAccess;
        cmd.Parameters.Add(new SqlParameter("@ViewAccess", SqlDbType.Bit));
        cmd.Parameters["@ViewAccess"].Value = ViewAccess;
        cmd.Parameters.Add(new SqlParameter("@DeleteAccess", SqlDbType.Bit));
        cmd.Parameters["@DeleteAccess"].Value = DeleteAccess;
        cmd.Parameters.Add(new SqlParameter("@SaveAccess", SqlDbType.Bit));
        cmd.Parameters["@SaveAccess"].Value = SaveAccess;
        cmd.Parameters.Add(new SqlParameter("@EditAccess", SqlDbType.Bit));
        cmd.Parameters["@EditAccess"].Value = EditAccess;
        cmd.Parameters.Add(new SqlParameter("@EmailAccess", SqlDbType.Bit));
        cmd.Parameters["@EmailAccess"].Value = EmailAccess;
        cmd.Parameters.Add(new SqlParameter("@FaxAccess", SqlDbType.Bit));
        cmd.Parameters["@FaxAccess"].Value = FaxAccess;
        cmd.Parameters.Add(new SqlParameter("@MessageAccess", SqlDbType.Bit));
        cmd.Parameters["@MessageAccess"].Value = MessageAccess;

        cmd.Parameters.Add(new SqlParameter("@Cabty", SqlDbType.NVarChar));
        cmd.Parameters["@Cabty"].Value = Cabty;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        //cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        // result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 InsertDrawerAccessRightMaster(Int32 DrawerId, Int32 DesignationId, bool PrintAccess, bool ViewAccess,
        bool DeleteAccess, bool SaveAccess, bool EditAccess, bool EmailAccess, bool FaxAccess, bool MessageAccess, string draty)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "InsertDrawerAccessRightMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@DrawerId", SqlDbType.Int));
        cmd.Parameters["@DrawerId"].Value = DrawerId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@PrintAccess", SqlDbType.Bit));
        cmd.Parameters["@PrintAccess"].Value = PrintAccess;
        cmd.Parameters.Add(new SqlParameter("@ViewAccess", SqlDbType.Bit));
        cmd.Parameters["@ViewAccess"].Value = ViewAccess;
        cmd.Parameters.Add(new SqlParameter("@DeleteAccess", SqlDbType.Bit));
        cmd.Parameters["@DeleteAccess"].Value = DeleteAccess;
        cmd.Parameters.Add(new SqlParameter("@SaveAccess", SqlDbType.Bit));
        cmd.Parameters["@SaveAccess"].Value = SaveAccess;
        cmd.Parameters.Add(new SqlParameter("@EditAccess", SqlDbType.Bit));
        cmd.Parameters["@EditAccess"].Value = EditAccess;
        cmd.Parameters.Add(new SqlParameter("@EmailAccess", SqlDbType.Bit));
        cmd.Parameters["@EmailAccess"].Value = EmailAccess;
        cmd.Parameters.Add(new SqlParameter("@FaxAccess", SqlDbType.Bit));
        cmd.Parameters["@FaxAccess"].Value = FaxAccess;
        cmd.Parameters.Add(new SqlParameter("@MessageAccess", SqlDbType.Bit));
        cmd.Parameters["@MessageAccess"].Value = MessageAccess;

        cmd.Parameters.Add(new SqlParameter("@draty", SqlDbType.NVarChar));
        cmd.Parameters["@draty"].Value = draty;


        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        //result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }

    protected void gridocsubsubtype_RowEditing(object sender, GridViewEditEventArgs e)
    {


    }
    protected void gridocsubsubtype_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lblmsg.Text = "";
            int dk1 = Convert.ToInt32(e.CommandArgument);
            ViewState["MasterId"] = dk1.ToString();

            SqlCommand cmdedit = new SqlCommand("Select * from DocumentType  where DocumentType.DocumentTypeId='" + dk1 + "' and DocumentType.DocumentType='GENERAL'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);
            if (dtedit.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You are unable to edit this folder as it is a system generated folder and cannot be edited";
                key = 1;
            }
            else
            {
                SqlCommand cmdmaster = new SqlCommand("select DocumentMainType.Whid,DocumentMainType.DocumentMainTypeId,DocumentType.* from DocumentType inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId where DocumentType.DocumentTypeId='" + dk1 + "' ", con);
                SqlDataAdapter adpmaster = new SqlDataAdapter(cmdmaster);
                DataTable dtmaster = new DataTable();
                adpmaster.Fill(dtmaster);
                if (dtmaster.Rows.Count > 0)
                {
                    fillbusiness();
                    ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dtmaster.Rows[0]["Whid"].ToString()));

                    Fillddldocmaintype();
                    ddldocmaintype.SelectedIndex = ddldocmaintype.Items.IndexOf(ddldocmaintype.Items.FindByValue(dtmaster.Rows[0]["DocumentMainTypeId"].ToString()));


                    ddldocumentsubtype();
                    ddldocsubtypename.SelectedIndex = ddldocsubtypename.Items.IndexOf(ddldocsubtypename.Items.FindByValue(dtmaster.Rows[0]["DocumentTypeId"].ToString()));

                    txtdocsubsubtypename.Text = dtmaster.Rows[0]["DocumentType"].ToString();
                    fillright();
                    imgbtnsubmit.Visible = false;
                    btnupdate.Visible = true;

                    pnladd.Visible = true;
                    Label12.Visible = true;
                    btnadd.Visible = false;
                    Label17.Text = "Edit Document Folder";



                }
            }

        }
    }
    protected void gridocsubsubtype_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string id = gridocsubsubtype.DataKeys[e.RowIndex].Value.ToString();


        SqlCommand cmdedit = new SqlCommand("Select * from DocumentType  where DocumentType.DocumentTypeId='" + id + "' and DocumentType.DocumentType='GENERAL'", con);
        SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
        DataTable dtedit = new DataTable();
        dtpedit.Fill(dtedit);
        if (dtedit.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You are unable to delete this folder as it is a system generated folder and cannot be deleted";
        }
        else
        {
            string str = "Select * from DocumentMaster where DocumentTypeId='" + id + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You cannot delete this folder as there are documents within it. Please move the documents to another folder then try again.";
                gridocsubsubtype.EditIndex = -1;
                FillDocumentType();


            }
            else
            {
                string str1 = "delete  from DocumentType where [DocumentType].DocumentTypeId='" + id + "'";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();
                gridocsubsubtype.EditIndex = -1;
                FillDocumentType();
                lblmsg.Text = "Record deleted successfully";



            }
        }
        pnladd.Visible = false;
        Label12.Visible = false;
        btnadd.Visible = true;
        Label17.Visible = false;
        Label17.Text = "Add New Document Folder";
    }
    protected void gridocsubsubtype_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridocsubsubtype.EditIndex = -1;
        FillDocumentType();
    }
    //protected void gridocsubsubtype_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    Int32 docid = Int32.Parse(gridocsubsubtype.DataKeys[e.RowIndex].Value.ToString());



    //    DropDownList ddlw = (DropDownList)gridocsubsubtype.Rows[gridocsubsubtype.EditIndex].FindControl("ddlBisi");
    //    Label whid = (Label)gridocsubsubtype.Rows[gridocsubsubtype.EditIndex].FindControl("lblw");

    //    DropDownList ddlDocMType = (DropDownList)gridocsubsubtype.Rows[gridocsubsubtype.EditIndex].FindControl("ddlDocMType");
    //    Label lblDocumentMainType = (Label)gridocsubsubtype.Rows[gridocsubsubtype.EditIndex].FindControl("lblDMId");


    //    DropDownList dlst = (DropDownList)gridocsubsubtype.Rows[e.RowIndex].FindControl("cmbDocumentSubType");
    //    Int32 docsubid = Int32.Parse(dlst.SelectedValue);

    //    string doctype = ((TextBox)gridocsubsubtype.Rows[e.RowIndex].FindControl("txtDocumentType")).Text;

    //    string st1 = "  select DocumentType.* from  DocumentType inner join DocumentSubType on  DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId  inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId  where DocumentSubType.DocumentMainTypeId='" + ddlDocMType.SelectedValue + "' and DocumentType.DocumentType='" + doctype.ToString() + "' and DocumentMainType.Whid='" + ddlw.SelectedValue + "' ";
    //    SqlCommand cmd1 = new SqlCommand(st1, con);
    //    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
    //    DataTable dt1 = new DataTable();
    //    adp1.Fill(dt1);
    //    if (dt1.Rows.Count > 0)
    //    {
    //        lblmsg.Visible = true;
    //        lblmsg.Text = "Sorry, Record already exist";
    //    }
    //    else
    //    {
    //        bool access = UserAccess.Usercon("DocumentType", "", "DocumentTypeId", "", "", "DocumentType.CID", "DocumentType inner join DocumentSubType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId");
    //        if (access == true)
    //        {
    //            bool access1 = UserAccess.Usercon("DocumentType", dlst.SelectedValue, "DocumentTypeId", "", "", "DocumentType.CID", "DocumentType inner join DocumentSubType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId");
    //            if (access1 == true)
    //            {

    //                bool success = clsDocument.UpdateDocumentType(docid, docsubid, doctype);
    //                if (Convert.ToString(success) == "True")
    //                {
    //                    lblmsg.Visible = true;
    //                    lblmsg.Text = "Record updated successfully";
    //                }
    //                else
    //                {
    //                    lblmsg.Visible = true;
    //                    lblmsg.Text = "Record is not updated successfully";
    //                }

    //                gridocsubsubtype.EditIndex = -1;
    //                FillDocumentTypeMainMethod();
    //                FillDocumentType();
    //            }
    //            else
    //            {
    //                lblmsg.Visible = true;
    //                lblmsg.Text = "Sorry, You don't permitted greater record in this Drawer into priceplan";
    //            }
    //        }
    //        else
    //        {
    //            lblmsg.Visible = true;
    //            lblmsg.Text = "Sorry, You don't permitted greater record to priceplan";
    //        }
    //    }
    //}
    protected void ddldocsubtypename_SelectedIndexChanged1(object sender, EventArgs e)
    {
        gridocsubsubtype.EditIndex = -1;
        FillDocumentType();


    }
    protected void ddldocmaintype_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddldocumentsubtype();
        fillright();
    }


    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillddldocmaintype();
        ddldocumentsubtype();
        filldesright();
        fillright();
    }




    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterbycabinet();
        fillfilterddlsubtype();
        FillDocumentType();

    }

    protected void gridocsubsubtype_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillDocumentType();
    }
    protected void gridocsubsubtype_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridocsubsubtype.PageIndex = e.NewPageIndex;
        FillDocumentType();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilterddlsubtype();
        FillDocumentType();
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentType();
    }


    protected void imgAdd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentMainType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
    {

        Fillddldocmaintype();

    }


    protected void imgAdd2_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentSubType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgRefresh2_Click(object sender, ImageClickEventArgs e)
    {
        ddldocumentsubtype();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;

            gridocsubsubtype.AllowPaging = false;
            gridocsubsubtype.PageSize = 1000;
            FillDocumentType();

            if (gridocsubsubtype.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gridocsubsubtype.Columns[4].Visible = false;
            }
            if (gridocsubsubtype.Columns[5].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                gridocsubsubtype.Columns[5].Visible = false;
            }

        }
        else
        {
            Button2.Text = "Printable Version";
            Button1.Visible = false;

            gridocsubsubtype.AllowPaging = true;
            gridocsubsubtype.PageSize = 10;
            FillDocumentType();

            if (ViewState["editHide"] != null)
            {
                gridocsubsubtype.Columns[4].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                gridocsubsubtype.Columns[5].Visible = true;
            }

        }
    }

    protected void fillbusiness()
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
    protected void Fillddldocmaintype()
    {
        string str132 = " SELECT [DocumentMainTypeId], DocumentMainType as DocumentMainType FROM  [dbo].[DocumentMainType] inner join WarehouseMaster on WarehouseMaster.WarehouseId=DocumentMainType.Whid where CID='" + Session["Comid"] + "' and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' order by DocumentMainType ";
        SqlCommand cgw = new SqlCommand(str132, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);
        ddldocmaintype.DataSource = dt;
        ddldocmaintype.DataBind();

    }
    protected void ddldocumentsubtype()
    {
        string str178 = " SELECT     DocumentSubType.DocumentSubTypeId, DocumentSubType.DocumentSubType, DocumentMainType.DocumentMainTypeId as DocumentMainTypeId,  DocumentMainType.DocumentMainType FROM         DocumentMainType RIGHT OUTER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId WHERE     (DocumentMainType.DocumentMainTypeId = '" + ddldocmaintype.SelectedValue + "') and DocumentMainType.CID='" + Session["Comid"] + "' ";
        SqlCommand cgw = new SqlCommand(str178, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);
        ddldocsubtypename.DataSource = dt;
        ddldocsubtypename.DataTextField = "DocumentSubType";
        ddldocsubtypename.DataValueField = "DocumentSubTypeId";
        ddldocsubtypename.DataBind();
    }
    protected void fillfilterbusiness()
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
    protected void filterbycabinet()
    {
        DropDownList2.Items.Clear();

        if (DropDownList1.SelectedIndex >= 0)
        {
            string str132 = " SELECT [DocumentMainTypeId], DocumentMainType as DocumentMainType FROM  [dbo].[DocumentMainType] inner join WarehouseMaster on WarehouseMaster.WarehouseId=DocumentMainType.Whid where CID='" + Session["Comid"] + "' and  DocumentMainType.Whid='" + DropDownList1.SelectedValue + "' ";
            SqlCommand cgw = new SqlCommand(str132, con);
            SqlDataAdapter adgw = new SqlDataAdapter(cgw);
            DataTable dt = new DataTable();
            adgw.Fill(dt);

            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "DocumentMainType";
            DropDownList2.DataValueField = "DocumentMainTypeId";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "All");
            DropDownList2.Items[0].Value = "0";


        }
        else
        {

            DropDownList2.DataSource = null;
            DropDownList2.DataTextField = "DocumentMainType";
            DropDownList2.DataValueField = "DocumentMainTypeId";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "All");
            DropDownList2.Items[0].Value = "0";


        }

    }
    protected void fillfilterddlsubtype()
    {


        DropDownList3.Items.Clear();

        if (DropDownList2.SelectedIndex >= 0)
        {
            string str178 = " SELECT     DocumentSubType.DocumentSubTypeId, DocumentSubType.DocumentSubType, DocumentMainType.DocumentMainTypeId as DocumentMainTypeId,  DocumentMainType.DocumentMainType FROM         DocumentMainType RIGHT OUTER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId WHERE     (DocumentMainType.DocumentMainTypeId = '" + DropDownList2.SelectedValue + "') and DocumentMainType.CID='" + Session["Comid"] + "' ";
            SqlCommand cgw = new SqlCommand(str178, con);
            SqlDataAdapter adgw = new SqlDataAdapter(cgw);
            DataTable dt = new DataTable();
            adgw.Fill(dt);

            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "DocumentSubType";
            DropDownList3.DataValueField = "DocumentSubTypeId";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, "All");
            DropDownList3.Items[0].Value = "0";
        }
        else
        {
            DropDownList3.DataSource = null;
            DropDownList3.DataTextField = "DocumentSubType";
            DropDownList3.DataValueField = "DocumentSubTypeId";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, "All");
            DropDownList3.Items[0].Value = "0";

        }


    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {


        string st1 = "  select DocumentType.* from  DocumentType inner join DocumentSubType on  DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId  inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId  where DocumentSubType.DocumentMainTypeId='" + ddldocmaintype.SelectedValue + "' and DocumentType.DocumentSubTypeId='" + ddldocsubtypename.SelectedValue + "' and DocumentType.DocumentType='" + txtdocsubsubtypename.Text + "' and DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and DocumentTypeId<>'" + ViewState["MasterId"].ToString() + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";
        }
        else
        {
            bool access = UserAccess.Usercon("DocumentType", "", "DocumentTypeId", "", "", "DocumentType.CID", "DocumentType inner join DocumentSubType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId");
            if (access == true)
            {
                bool access1 = UserAccess.Usercon("DocumentType", ddldocsubtypename.SelectedValue, "DocumentTypeId", "", "", "DocumentType.CID", "DocumentType inner join DocumentSubType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId");
                if (access1 == true)
                {

                    bool success = clsDocument.UpdateDocumentType(Convert.ToInt32(ViewState["MasterId"].ToString()), Convert.ToInt32(ddldocsubtypename.SelectedValue), txtdocsubsubtypename.Text.ToUpper());
                    if (Convert.ToString(success) == "True")
                    {
                        if (chkdesright.Checked == true)
                        {
                            foreach (GridViewRow item in grdacc.Rows)
                            {
                                string desid = grdacc.DataKeys[item.RowIndex].Value.ToString();
                                CheckBox chkview = (CheckBox)(item.FindControl("chkview"));
                                CheckBox chkdelete = (CheckBox)(item.FindControl("chkdelete"));
                                CheckBox chksave = (CheckBox)(item.FindControl("chksave"));
                                CheckBox chkedit = (CheckBox)(item.FindControl("chkedit"));
                                CheckBox chkemail = (CheckBox)(item.FindControl("chkemail"));
                                CheckBox chkMessage = (CheckBox)(item.FindControl("chkMessage"));
                                Label lblcode = (Label)(item.FindControl("lblcode"));
                                DataTable dtmain = new DataTable();
                                if (lblcode.Text.ToString() == "1")
                                {
                                    if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                    {
                                        string strallbusdet = "Update DocumentAccessRighallBus set ViewAccess='" + chkview.Checked + "',DeleteAccess='" + chkdelete.Checked + "',SaveAccess='" + chksave.Checked + "',EditAccess='" + chkedit.Checked + "',EmailAccess='" + chkemail.Checked + "',MessageAccess='" + chkMessage.Checked + "' where DesignationId='" + desid + "' and CID='" + Session["Comid"] + "'";
                                        SqlCommand cmdallbusdel = new SqlCommand(strallbusdet, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdallbusdel.ExecuteNonQuery();
                                        con.Close();
                                        dtmain = select("SELECT Distinct DocumentType.DocumentTypeId from  DocumentMainType Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where  DocumentType.DocumentTypeId='" + ViewState["MasterId"] + "'");

                                    }
                                }
                                else if (lblcode.Text.ToString() == "2")
                                {
                                    if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                    {
                                        string strallbusdet = "Update DocumentAccessRightforbusallCabinet set ViewAccess='" + chkview.Checked + "',DeleteAccess='" + chkdelete.Checked + "',SaveAccess='" + chksave.Checked + "',EditAccess='" + chkedit.Checked + "',EmailAccess='" + chkemail.Checked + "',MessageAccess='" + chkMessage.Checked + "' where DesignationId='" + desid + "' and Whid='" + ddlbusiness.SelectedValue + "'";
                                        SqlCommand cmdallbusdel = new SqlCommand(strallbusdet, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdallbusdel.ExecuteNonQuery();
                                        con.Close();
                                        dtmain = select("SELECT Distinct DocumentType.DocumentTypeId from  DocumentMainType Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where  DocumentType.DocumentTypeId='" + ViewState["MasterId"] + "'");

                                    }

                                }
                                else if (lblcode.Text.ToString() == "3")
                                {
                                    if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                    {
                                        int rst11 = InsertCabinetAccessRightMaster(Convert.ToInt32(ddldocmaintype.SelectedValue), Convert.ToInt32(desid), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked, "1");
                                        dtmain = select("SELECT Distinct DocumentType.DocumentTypeId from  DocumentMainType Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where  DocumentType.DocumentTypeId='" + ViewState["MasterId"] + "'");

                                    }

                                }
                                else if (lblcode.Text.ToString() == "4" )
                                {
                                    if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                    {
                                        int rst11 = InsertDrawerAccessRightMaster(Convert.ToInt32(ViewState["MasterId"]), Convert.ToInt32(desid), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked, "1");
                                        dtmain = select("SELECT Distinct DocumentType.DocumentTypeId from  DocumentMainType Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentType.DocumentTypeId='" + ViewState["MasterId"] + "'");

                                    }

                                }
                                else if (lblcode.Text.ToString() == "5" || lblcode.Text.ToString() == "0")
                                {
                                    if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                    {
                                     int rst1 = clsDocument.InsertDocumentAccessRightMaster(Convert.ToInt32(ViewState["MasterId"]), Convert.ToInt32(desid), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked);
                                    }

                                }
                                if (dtmain.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtmain.Rows.Count; i++)
                                    {

                                        int rst1 = clsDocument.InsertDocumentAccessRightMaster(Convert.ToInt32(dtmain.Rows[i]["DocumentTypeId"]), Convert.ToInt32(desid), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked);
                                    }
                                }
                            }
                        }
                        ViewState["MasterId"] = "";
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record updated successfully";
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record is not updated successfully";
                    }

                    gridocsubsubtype.EditIndex = -1;
                    FillDocumentType();

                    pnladd.Visible = false;
                    Label12.Visible = false;
                    btnadd.Visible = true;
                    Label17.Text = "Add New Document Folder";
                    Label17.Visible = false;

                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Sorry, You don't permitted greater record as per your priceplan";
                }
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You don't permitted greater record as per your priceplan";
            }


            imgbtnsubmit.Visible = true;
            btnupdate.Visible = false;
        }
    }
    protected void imgbtnsubmit0_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        Label12.Visible = false;
        btnadd.Visible = true;
        Label17.Visible = false;
        Label17.Text = "Add New Document Folder";
        txtdocsubsubtypename.Text = "";
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            Label17.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            Label17.Visible = false;
        }
        btnadd.Visible = false;

        Label17.Text = "Add New Document Folder";
        lblmsg.Text = "";

    }
    protected void filldesright()
    {
        DataTable dt = select("select distinct DepartmentmasterMNC.Departmentname as Departmentname,DepartmentmasterMNC.Id  from DepartmentmasterMNC inner join DesignationMaster on DesignationMaster.DeptID=DepartmentmasterMNC.Id where DepartmentmasterMNC.Whid='" + ddlbusiness.SelectedValue + "' Order by Departmentname");
        //dt = clsMaster.SelectDepartmentMaster(ddlbusiness.SelectedValue);
        ddldept.DataSource = dt;
        ddldept.DataTextField = "Departmentname";
        ddldept.DataValueField = "Id";

        ddldept.DataBind();

        ddldept.Items.Insert(0, "All");
        ddldept.SelectedItem.Value = "0";
    }
    protected void fillright()
    {
        string deptid = "";
        if (ddldept.SelectedIndex > 0)
        {
            deptid = " and DesignationMaster.DeptID='" + ddldept.SelectedValue + "'";
        }
        DataTable dt = select("select Distinct Departmentname+':'+DesignationName as DesignationName,DesignationMasterId  from DepartmentmasterMNC inner join DesignationMaster on DesignationMaster.DeptID=DepartmentmasterMNC.Id where DepartmentmasterMNC.Whid='" + ddlbusiness.SelectedValue + "' " + deptid + " Order by DesignationName");

        grdacc.DataSource = dt;

        grdacc.DataBind();
        foreach (GridViewRow item in grdacc.Rows)
        {
            int flagt = 0;
            string desid = grdacc.DataKeys[item.RowIndex].Value.ToString();
            CheckBox chkview = (CheckBox)(item.FindControl("chkview"));
            CheckBox chkdelete = (CheckBox)(item.FindControl("chkdelete"));
            CheckBox chksave = (CheckBox)(item.FindControl("chksave"));
            CheckBox chkedit = (CheckBox)(item.FindControl("chkedit"));
            CheckBox chkemail = (CheckBox)(item.FindControl("chkemail"));
            CheckBox chkMessage = (CheckBox)(item.FindControl("chkMessage"));
            Label lblcode = (Label)(item.FindControl("lblcode"));
            DataTable dtc = select("select * from DocumentAccessRighallBus where AllbusAccess='1' and DesignationId='" + desid + "' and CID='" + Session["Comid"] + "'");
            if (dtc.Rows.Count == 0)
            {
                dtc = select("select * from DocumentAccessRightforbusallCabinet where CabinetAccess='1' and DesignationId='" + desid + "' and Whid='" + ddlbusiness.SelectedValue + "'");
                if (dtc.Rows.Count == 0)
                {
                    dtc = select("select * from CabinetAccessRightsMaster where CabinetId='" + ddldocmaintype.SelectedValue + "' and  DesignationId='" + desid + "' ");
                    if (dtc.Rows.Count == 0)
                    {
                       
                            dtc = select("select * from DrawerAccessRightsMaster where DrawerId='" + ddldocsubtypename.SelectedValue + "' and  DesignationId='" + desid + "' ");
                            if (dtc.Rows.Count == 0)
                            {
                                if (Convert.ToString(ViewState["MasterId"]) != "")
                                {
                                    dtc = select("select * from DocumentAccessRightMaster where DocumentTypeId='" + ViewState["MasterId"] + "' and  DesignationId='" + desid + "' ");
                                    if (dtc.Rows.Count == 0)
                                    {

                                    }
                                    else
                                    {
                                        flagt = 5;
                                    }
                                }
                            }
                            else
                            {
                                flagt = 4;
                            }
                       

                    }
                    else
                    {
                        flagt = 3;
                    }


                }
                else
                {
                    flagt = 2;
                }


            }
            else
            {
                flagt = 1;
            }
            if (dtc.Rows.Count > 0)
            {
                lblcode.Text = flagt.ToString();
                chkview.Checked = Convert.ToBoolean(dtc.Rows[0]["ViewAccess"]);
                chkdelete.Checked = Convert.ToBoolean(dtc.Rows[0]["DeleteAccess"]);
                chkedit.Checked = Convert.ToBoolean(dtc.Rows[0]["EditAccess"]);
                chksave.Checked = Convert.ToBoolean(dtc.Rows[0]["SaveAccess"]);
                chkemail.Checked = Convert.ToBoolean(dtc.Rows[0]["EmailAccess"]);
                chkMessage.Checked = Convert.ToBoolean(dtc.Rows[0]["MessageAccess"]);
                if (chkview.Checked == true)
                {
                    chkview.Enabled = false;
                }
                else
                {
                    chkview.Enabled = true;
                }

                if (chkdelete.Checked == true)
                {
                    chkdelete.Enabled = false;
                }
                else
                {
                    chkdelete.Enabled = true;
                }
                if (chksave.Checked == true)
                {
                    chksave.Enabled = false;
                }
                else
                {
                    chksave.Enabled = true;
                }
                if (chkedit.Checked == true)
                {
                    chkedit.Enabled = false;
                }
                else
                {
                    chkedit.Enabled = true;
                }
                if (chkemail.Checked == true)
                {
                    chkemail.Enabled = false;
                }
                else
                {
                    chkemail.Enabled = true;
                }
                if (chkMessage.Checked == true)
                {
                    chkMessage.Enabled = false;
                }
                else
                {
                    chkMessage.Enabled = true;
                }

            }

        }
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillright();
    }
    protected void chkdesright_CheckedChanged(object sender, EventArgs e)
    {
        if (chkdesright.Checked == true)
        {
            pnldataaccess.Visible = true;
        }
        else
        {
            pnldataaccess.Visible = false;
        }
    }
    protected void chkViewhead_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdacc.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkview"));
            if (chk.Enabled == true)
            {
                chk.Checked = ((CheckBox)sender).Checked;
            }
        }

    }
    protected void chkdeletehead_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdacc.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkdelete"));
            if (chk.Enabled == true)
            {
                chk.Checked = ((CheckBox)sender).Checked;
            }
        }

    }
    protected void chksavehead_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdacc.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chksave"));
            if (chk.Enabled == true)
            {
                chk.Checked = ((CheckBox)sender).Checked;
            }
        }

    }
    protected void chkedithead_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdacc.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkedit"));
            if (chk.Enabled == true)
            {
                chk.Checked = ((CheckBox)sender).Checked;
            }
        }

    }

    protected void chkemailhead_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdacc.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkemail"));
            if (chk.Enabled == true)
            {
                chk.Checked = ((CheckBox)sender).Checked;
            }
        }

    }
    protected void chkMessagehead_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdacc.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkMessage"));
            if (chk.Enabled == true)
            {
                chk.Checked = ((CheckBox)sender).Checked;
            }
        }

    }

   
    protected void ddldocsubtypename_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillright();
    }
}
