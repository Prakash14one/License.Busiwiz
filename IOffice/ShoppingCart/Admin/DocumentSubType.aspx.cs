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

public partial class ShoppingCart_Admin_DocumentSubType : System.Web.UI.Page
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ifilecabinateConnectionString"].ConnectionString);
    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    int key = 0;
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
   //     compid = Session["comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
   
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            
           
            ViewState["sortOrder"] = "";
            lblcompany.Text = Session["Cname"].ToString();
            lblmsg.Visible = false;

            if (Request.QueryString["Id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);

                string strmasteridver = " select * from DocumentMainType where DocumentMainTypeId='" + id + "'";
                SqlCommand cmdmasteridver = new SqlCommand(strmasteridver, con);
                DataTable dtmasteridver = new DataTable();
                SqlDataAdapter adpmasteridver = new SqlDataAdapter(cmdmasteridver);
                adpmasteridver.Fill(dtmasteridver);

                if (dtmasteridver.Rows.Count > 0)
                {
                    ViewState["DocumentMainTypeId"] = dtmasteridver.Rows[0]["DocumentMainTypeId"].ToString();
                    ViewState["Whid"] = dtmasteridver.Rows[0]["Whid"].ToString();

                    fillbusiness();
                    ddlbusinessname.SelectedIndex = ddlbusinessname.Items.IndexOf(ddlbusinessname.Items.FindByValue(ViewState["Whid"].ToString()));
                    
                    Fillddldocmaintype();

                    ddldocmaintype.SelectedIndex = ddldocmaintype.Items.IndexOf(ddldocmaintype.Items.FindByValue(ViewState["DocumentMainTypeId"].ToString()));
                    filldesright();
                    fillright();
                    btnadd_Click(sender, e);
                }

            }
            else
            {
                fillbusiness();
                Fillddldocmaintype();
                filldesright();
                fillright();
            }
           
            filterbybusiness();
            filterbycabinet();
            FillDocumentSubType();
           
            
        }

    }

    protected void Fillddldocmaintype()
    {
        string str132 = " SELECT [DocumentMainTypeId], DocumentMainType as DocumentMainType FROM  [dbo].[DocumentMainType] inner join WarehouseMaster on WarehouseMaster.WarehouseId=DocumentMainType.Whid where CID='" + Session["Comid"] + "' and DocumentMainType.Whid='" + ddlbusinessname.SelectedValue + "'";
        SqlCommand cgw = new SqlCommand(str132, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        DataTable dt = new DataTable();
        adgw.Fill(dt);
        ddldocmaintype.DataSource = dt;
        ddldocmaintype.DataBind();
       
    }
    protected void fillbusiness()
    {
       

        ddlbusinessname.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlbusinessname.DataSource = ds;
        ddlbusinessname.DataTextField = "Name";
        ddlbusinessname.DataValueField = "WareHouseId";
        ddlbusinessname.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlbusinessname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
    protected void FillDocumentSubType()
    {
        Label5.Text = ddlfilterbusiness.SelectedItem.Text;
        Label4.Text = ddlfiltercabinet.SelectedItem.Text;

        string str1 = "";
        string str2 = "";

        if (ddlfilterbusiness.SelectedIndex > 0)
        {
            str1 = " and DocumentMainType.Whid='" + ddlfilterbusiness.SelectedValue + "'";
        }
        if (ddlfiltercabinet.SelectedIndex > 0)
        {
            str2 = " and DocumentSubType.DocumentMainTypeId='" + ddlfiltercabinet.SelectedValue + "'";
        }

        string str1321 = " DocumentMainType.DocumentMainTypeId, DocumentSubType.DocumentSubTypeId, DocumentSubType.DocumentSubType,WareHouseMaster.WareHouseId as Whid, WarehouseMaster.Name as WName  ,DocumentMainType.DocumentMainType as DocumentMainType FROM  WarehouseMaster inner join DocumentMainType on WarehouseMaster.WarehouseId=DocumentMainType.Whid Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId where DocumentSubType.CID='" + Session["Comid"] + "' " + str1 + " " + str2 + " ";

        string strmak = " select Count(DocumentSubType.DocumentSubTypeId) as ci FROM  WarehouseMaster inner join DocumentMainType on WarehouseMaster.WarehouseId=DocumentMainType.Whid Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId where DocumentSubType.CID='" + Session["Comid"] + "' " + str1 + " " + str2 + " ";


        gridocsubtype1.VirtualItemCount = GetRowCount(strmak);

        string sortExpression = " WarehouseMaster.Name ,DocumentMainType.DocumentMainType,DocumentSubType.DocumentSubType";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt1 = GetDataPage(gridocsubtype1.PageIndex, gridocsubtype1.PageSize, sortExpression, str1321);

            gridocsubtype1.DataSource = dt1;

            DataView myDataView = new DataView();

            myDataView = dt1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            gridocsubtype1.DataBind();
        }

        else
        {
            gridocsubtype1.DataSource = null;
            gridocsubtype1.DataBind();
        }

        //string orderby = " order by WarehouseMaster.Name ,DocumentMainType.DocumentMainType,DocumentSubType.DocumentSubType";
        //string strfinal = str1321 + str1 + str2 + orderby;

        //SqlCommand cgw1 = new SqlCommand(strfinal, con);
        //SqlDataAdapter adgw1 = new SqlDataAdapter(cgw1);
        //DataTable dt1 = new DataTable();
        //adgw1.Fill(dt1);            
    
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


        try
        {
            string st1 = "select * from DocumentSubType inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId  where DocumentSubType.DocumentMainTypeId='" + ddldocmaintype.SelectedValue + "' and DocumentSubType.DocumentSubType='" + txtdocsubtypename.Text + "' and DocumentMainType.Whid='" + ddlbusinessname.SelectedValue + "' ";
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
                bool access = UserAccess.Usercon("DocumentSubType", "", "DocumentSubTypeId", "", "", "DocumentMainType.CID", "DocumentSubType inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId");
                if (access == true)
                {
                    bool access1 = UserAccess.Usercon("DocumentSubType", ddldocmaintype.SelectedValue, "DocumentSubTypeId", "", "", "DocumentMainType.CID", "DocumentSubType inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId");
                    {
                        if (access1 == true)
                        {

                            lblmsg.Text = "";
                            String str = "Insert Into DocumentSubType (DocumentMainTypeId,DocumentSubType,CID)values('" + ddldocmaintype.SelectedValue + "','" + txtdocsubtypename.Text.ToUpper() + "','" + Session["Comid"].ToString() + "')";
                            SqlCommand cmd = new SqlCommand(str, con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);



                          


                            string strmax = " Select Max(DocumentSubTypeId) as Id from DocumentSubType";
                            SqlCommand cmdmax = new SqlCommand(strmax, con);
                            DataTable dtmax = new DataTable();
                            SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
                            adpmax.Fill(dtmax);
                            string id = "0";
                            if (dtmax.Rows.Count > 0)
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
                                                string strallbusdet = "Update DocumentAccessRightforbusallCabinet set ViewAccess='" + chkview.Checked + "',DeleteAccess='" + chkdelete.Checked + "',SaveAccess='" + chksave.Checked + "',EditAccess='" + chkedit.Checked + "',EmailAccess='" + chkemail.Checked + "',MessageAccess='" + chkMessage.Checked + "' where DesignationId='" + desid + "' and Whid='" + ddlbusinessname.SelectedValue + "'";
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
                                        else if (lblcode.Text.ToString() == "4" || lblcode.Text.ToString() == "0")
                                        {
                                            if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                            {
                                                int rst11 = InsertDrawerAccessRightMaster(Convert.ToInt32(dtmax.Rows[0]["DocumentSubTypeId"]), Convert.ToInt32(desid), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked, "1");
                                            }
                                        }
                                    }
                                }
                                id = dtmax.Rows[0]["Id"].ToString();

                                
                            }
                            lblmsg.Visible = true;
                            lblmsg.Text = "Record inserted successfully";
                            ViewState["MasterId"] = "";
                            if (CheckBox1.Checked == true)
                            {
                                string te1 = "DocumentSubSubType.aspx?Id=" + id;
                                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te1 + "');", true);
                            }
                            FillDocumentSubType();
                            ddldocmaintype.SelectedIndex = -1;
                            txtdocsubtypename.Text = "";

                        }
                        else
                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = "Sorry, You don't permited greter record in this cabinet into priceplan";
                        }
                    }
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Sorry, You don't permited greter record to priceplan";
                }

                pnladd.Visible = false;
                Label12.Visible = false;
                btnadd.Visible = true;
                Label12.Text = "Add New Document Drawer";
            }
        }
        catch (Exception es)
        {
            Response.Write(es.Message.ToString());
        }

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
 
    protected void gridocsubtype_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //if (key == 0)
        //{
        //    gridocsubtype1.EditIndex = e.NewEditIndex;

        //    FillDocumentSubType();

        //    DropDownList ddlwarehouse = (DropDownList)gridocsubtype1.Rows[gridocsubtype1.EditIndex].FindControl("ddlwarehouse");
        //    Label whid = (Label)gridocsubtype1.Rows[gridocsubtype1.EditIndex].FindControl("Label4");

        //    DropDownList cmbDocumentMainType = (DropDownList)gridocsubtype1.Rows[gridocsubtype1.EditIndex].FindControl("cmbDocumentMainType");
        //    Label lbldocumentmaintype123 = (Label)gridocsubtype1.Rows[gridocsubtype1.EditIndex].FindControl("lbldocumentmaintype123");

        //    string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "'and [WareHouseMaster].Status='1' order by Name";
        //    SqlCommand cmd = new SqlCommand(str, con);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);
        //    ddlwarehouse.DataSource = ds;
        //    ddlwarehouse.DataTextField = "Name";
        //    ddlwarehouse.DataValueField = "WareHouseId";
        //    ddlwarehouse.DataBind();

        //    ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(whid.Text));


        //    string str1 = " SELECT [DocumentMainTypeId], DocumentMainType as DocumentMainType FROM  [dbo].[DocumentMainType] inner join WarehouseMaster on WarehouseMaster.WarehouseId=DocumentMainType.Whid where CID='" + Session["Comid"] + "' and DocumentMainType.Whid='" + ddlwarehouse.SelectedValue + "'";
        //    SqlCommand cmd1 = new SqlCommand(str1, con);
        //    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        //    DataSet ds1 = new DataSet();
        //    adp1.Fill(ds1);
        //    cmbDocumentMainType.DataSource = ds1;
        //    cmbDocumentMainType.DataTextField = "DocumentMainType";
        //    cmbDocumentMainType.DataValueField = "DocumentMainTypeId";
        //    cmbDocumentMainType.DataBind();

        //    cmbDocumentMainType.SelectedIndex = cmbDocumentMainType.Items.IndexOf(cmbDocumentMainType.Items.FindByValue(lbldocumentmaintype123.Text));

        //}
    }
    protected void gridocsubtype_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ViewState["MasterId"] = "";
        if (e.CommandName == "Edit")
        {
            lblmsg.Text = "";
            int dk1 = Convert.ToInt32(e.CommandArgument);
            ViewState["MasterId"] = dk1.ToString();

            SqlCommand cmdedit = new SqlCommand("Select * from DocumentSubType  where DocumentSubType.DocumentSubTypeId='" + dk1 + "' and DocumentSubType.DocumentSubType='GENERAL'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);
            if (dtedit.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You are unable to edit this drawer as it is a system generated drawer and cannot be edited";
              
            }
            else
            {
                SqlCommand cmdmaster = new SqlCommand("select DocumentMainType.Whid,DocumentSubType.* from DocumentSubType inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId where DocumentSubType.DocumentSubTypeId='" + dk1 + "' ", con);
                SqlDataAdapter adpmaster = new SqlDataAdapter(cmdmaster);
                DataTable dtmaster = new DataTable();
                adpmaster.Fill(dtmaster);
                if (dtmaster.Rows.Count > 0)
                {
                    fillbusiness();
                    ddlbusinessname.SelectedIndex = ddlbusinessname.Items.IndexOf(ddlbusinessname.Items.FindByValue(dtmaster.Rows[0]["Whid"].ToString()));

                    Fillddldocmaintype();
                    ddldocmaintype.SelectedIndex = ddldocmaintype.Items.IndexOf(ddldocmaintype.Items.FindByValue(dtmaster.Rows[0]["DocumentMainTypeId"].ToString()));

                    txtdocsubtypename.Text = dtmaster.Rows[0]["DocumentSubType"].ToString();
                    fillright();
                    imgbtnsubmit.Visible = false;
                    btnupdate.Visible = true;

                    pnladd.Visible = true;
                    Label12.Visible = true;
                    btnadd.Visible = false;
                    Label12.Text = "Edit Document Drawer";
                }

            }
        }
    }
    protected void gridocsubtype_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int dk1 = Convert.ToInt32(gridocsubtype1.DataKeys[e.RowIndex].Value);

        SqlCommand cmdedit = new SqlCommand("Select * from DocumentSubType  where DocumentSubType.DocumentSubTypeId='" + dk1 + "' and DocumentSubType.DocumentSubType='GENERAL'", con);
        SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
        DataTable dtedit = new DataTable();
        dtpedit.Fill(dtedit);
        if (dtedit.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You are unable to delete this drawer as it is a system generated drawer and cannot be deleted";
        }
        else
        {

            string st3 = "select * from DocumentType where DocumentSubTypeId='" + gridocsubtype1.DataKeys[e.RowIndex].Value.ToString() + "'";
            SqlCommand cmd3 = new SqlCommand(st3, con);
            SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            adp3.Fill(dt3);
            if (dt3.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You cannot delete this drawer as there are folders within it. Please delete the folders, then try again.";

            }
            else
            {
                string st2 = "Delete from DocumentSubType where DocumentSubTypeId='" + gridocsubtype1.DataKeys[e.RowIndex].Value.ToString() + "' ";
                SqlCommand cmd2 = new SqlCommand(st2, con);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
                gridocsubtype1.EditIndex = -1;
                FillDocumentSubType();
                

                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully";
            }

        }
    }
    protected void gridocsubtype_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridocsubtype1.EditIndex = -1;
        
        FillDocumentSubType();
    }
   
    protected void ddldocmaintype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillright();
    }


    protected void ddlbusinessname_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillddldocmaintype();
        filldesright();
        fillright();
    }
    protected void filterbycabinet()
    {
        ddlfiltercabinet.Items.Clear();

        if (ddlfilterbusiness.SelectedIndex >= 0)
        {
            string str132 = " SELECT [DocumentMainTypeId], DocumentMainType as DocumentMainType FROM  [dbo].[DocumentMainType] inner join WarehouseMaster on WarehouseMaster.WarehouseId=DocumentMainType.Whid where CID='" + Session["Comid"] + "' and  DocumentMainType.Whid='" + ddlfilterbusiness.SelectedValue + "' ";
            SqlCommand cgw = new SqlCommand(str132, con);
            SqlDataAdapter adgw = new SqlDataAdapter(cgw);
            DataTable dt = new DataTable();
            adgw.Fill(dt);

            ddlfiltercabinet.DataSource = dt;
            ddlfiltercabinet.DataTextField = "DocumentMainType";
            ddlfiltercabinet.DataValueField = "DocumentMainTypeId";
            ddlfiltercabinet.DataBind();
            ddlfiltercabinet.Items.Insert(0, "All");
            ddlfiltercabinet.SelectedItem.Value = "0";


        }
        else
        {

            ddlfiltercabinet.DataSource = null;
            ddlfiltercabinet.DataTextField = "DocumentMainType";
            ddlfiltercabinet.DataValueField = "DocumentMainTypeId";
            ddlfiltercabinet.DataBind();
            ddlfiltercabinet.Items.Insert(0, "All");
            ddlfiltercabinet.SelectedItem.Value = "0";
           

        }

    }
    protected void filldesright()
    {
        DataTable dt = select("select distinct DepartmentmasterMNC.Departmentname as Departmentname,DepartmentmasterMNC.Id  from DepartmentmasterMNC inner join DesignationMaster on DesignationMaster.DeptID=DepartmentmasterMNC.Id where DepartmentmasterMNC.Whid='" + ddlbusinessname.SelectedValue + "' Order by Departmentname");
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
        DataTable dt = select("select Distinct Departmentname+':'+DesignationName as DesignationName,DesignationMasterId  from DepartmentmasterMNC inner join DesignationMaster on DesignationMaster.DeptID=DepartmentmasterMNC.Id where DepartmentmasterMNC.Whid='" + ddlbusinessname.SelectedValue + "' " + deptid + " Order by DesignationName");

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
                dtc = select("select * from DocumentAccessRightforbusallCabinet where CabinetAccess='1' and DesignationId='" + desid + "' and Whid='" + ddlbusinessname.SelectedValue + "'");
                if (dtc.Rows.Count == 0)
                {
                        dtc = select("select * from CabinetAccessRightsMaster where CabinetId='" + ddldocmaintype.SelectedValue + "' and  DesignationId='" + desid + "' ");
                        if (dtc.Rows.Count == 0)
                        {
                            if (Convert.ToString(ViewState["MasterId"]) != "")
                            {
                                dtc = select("select * from DrawerAccessRightsMaster where DrawerId='" + ViewState["MasterId"] + "' and  DesignationId='" + desid + "' ");
                                if (dtc.Rows.Count == 0)
                                {

                                }
                                else
                                {
                                    flagt = 4;
                                }
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

    protected void filterbybusiness()
    {
        

        ddlfilterbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlfilterbusiness.DataSource = ds;
        ddlfilterbusiness.DataTextField = "Name";
        ddlfilterbusiness.DataValueField = "WareHouseId";
        ddlfilterbusiness.DataBind();
        ddlfilterbusiness.Items.Insert(0, "All");
        ddlfilterbusiness.Items[0].Value = "0";

    }
    protected void ddlfilterbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterbycabinet(); 
        FillDocumentSubType();
       
       

    }
    protected void ddlfiltercabinet_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentSubType();


    }
    
    protected void gridocsubtype_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        //SelectDocumentforApproval();
        FillDocumentSubType();
        //filterbycabinet();
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

    protected void LinkButton153_Click(object sender, EventArgs e)
    {
        Fillddldocmaintype();
    }
   
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ViewState["MasterId"] = "";
        ddldocmaintype.SelectedIndex = -1;
        txtdocsubtypename.Text = "";
        lblmsg.Text = "";
        btnupdate.Visible = false;
        imgbtnsubmit.Visible = true;
        pnladd.Visible = false;
        Label12.Visible = false;
        btnadd.Visible = true;
        Label12.Text = "Add New Document Drawer";

    }
    protected void imgAdd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DocumentMainType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlbusinessname.SelectedIndex >= 0)
        {
            Fillddldocmaintype();
        }
    }
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;

            gridocsubtype1.AllowPaging = false;
            gridocsubtype1.PageSize = 1000;
            FillDocumentSubType();

            if (gridocsubtype1.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gridocsubtype1.Columns[3].Visible = false;
            }
            if (gridocsubtype1.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                gridocsubtype1.Columns[4].Visible = false;
            }            
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(200);

            Button2.Text = "Printable Version";
            Button1.Visible = false;

            gridocsubtype1.AllowPaging = true;
            gridocsubtype1.PageSize = 20;
            FillDocumentSubType();

            if (ViewState["editHide"] != null)
            {
                gridocsubtype1.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                gridocsubtype1.Columns[4].Visible = true;
            }
           
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            Label12.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            Label12.Visible = false;
        }
        btnadd.Visible = false;

        Label12.Text = "Add New Document Drawer";
        lblmsg.Text = "";
        imgbtnsubmit.Visible = true;
        btnupdate.Visible = false;
        ViewState["MasterId"] = "";
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string str = "select * from DocumentSubType inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId  where DocumentSubType.DocumentMainTypeId='" + ddldocmaintype.SelectedValue + "' and  DocumentSubType='" + txtdocsubtypename.Text + "' and Whid='" + ddlbusinessname.SelectedValue + "' and DocumentSubTypeId <> '" + ViewState["MasterId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";
        }
        else
        {
            bool access = UserAccess.Usercon("DocumentSubType", "", "DocumentSubTypeId", "", "", "DocumentMainType.CID", "DocumentSubType inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId");
            if (access == true)
            {
                bool access1 = UserAccess.Usercon("DocumentSubType", ddldocmaintype.SelectedValue, "DocumentSubTypeId", "", "", "DocumentMainType.CID", "DocumentSubType inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId");
                if (access1 == true)
                {
                    bool success = clsDocument.UpdateDocumentSubType(Convert.ToInt32(ViewState["MasterId"].ToString()), Convert.ToInt32(ddldocmaintype.SelectedValue),Convert.ToString(txtdocsubtypename.Text.ToUpper()));
                  

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
                                        dtmain = select("SELECT Distinct DocumentType.DocumentTypeId from  DocumentMainType Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where  DocumentSubType.DocumentSubTypeId='" + ViewState["MasterId"] + "'");

                                    }
                                }
                                else if (lblcode.Text.ToString() == "2")
                                {
                                    if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                    {
                                        string strallbusdet = "Update DocumentAccessRightforbusallCabinet set ViewAccess='" + chkview.Checked + "',DeleteAccess='" + chkdelete.Checked + "',SaveAccess='" + chksave.Checked + "',EditAccess='" + chkedit.Checked + "',EmailAccess='" + chkemail.Checked + "',MessageAccess='" + chkMessage.Checked + "' where DesignationId='" + desid + "' and Whid='" + ddlbusinessname.SelectedValue + "'";
                                        SqlCommand cmdallbusdel = new SqlCommand(strallbusdet, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdallbusdel.ExecuteNonQuery();
                                        con.Close();
                                        dtmain = select("SELECT Distinct DocumentType.DocumentTypeId from  DocumentMainType Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where  DocumentSubType.DocumentSubTypeId='" + ViewState["MasterId"] + "'");

                                    }

                                }
                                else if (lblcode.Text.ToString() == "3" )
                                {
                                    if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                    {
                                        int rst11 = InsertCabinetAccessRightMaster(Convert.ToInt32(ddldocmaintype.SelectedValue), Convert.ToInt32(desid), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked, "1");
                                        dtmain = select("SELECT Distinct DocumentType.DocumentTypeId from  DocumentMainType Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where  DocumentSubType.DocumentSubTypeId='" + ViewState["MasterId"] + "'");

                                    }

                                }
                                else if (lblcode.Text.ToString() == "4" || lblcode.Text.ToString() == "0")
                                {
                                    if (chkMessage.Checked == true || chkview.Checked == true || chkdelete.Checked == true || chksave.Checked == true || chkedit.Checked == true || chkemail.Checked == true)
                                    {
                                        int rst11 = InsertDrawerAccessRightMaster(Convert.ToInt32(ViewState["MasterId"]), Convert.ToInt32(desid), Convert.ToBoolean(1), chkview.Checked, chkdelete.Checked, chksave.Checked, chkedit.Checked, chkemail.Checked, Convert.ToBoolean(true), chkMessage.Checked, "1");
                                        dtmain = select("SELECT Distinct DocumentType.DocumentTypeId from  DocumentMainType Inner join  DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId where DocumentSubType.DocumentSubTypeId='" + ViewState["MasterId"] + "'");

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
                    
                    gridocsubtype1.EditIndex = -1;
                    FillDocumentSubType();

                   
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Sorry, You don't permited greter record to priceplan";
                }
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You don't permited greter record to priceplan";
            }

            btnupdate.Visible = false;
            imgbtnsubmit.Visible = true;
            pnladd.Visible = false;
            Label12.Visible = false;
            btnadd.Visible = true;
            Label12.Text = "Add New Document Drawer";
            ddldocmaintype.SelectedIndex = -1;
            txtdocsubtypename.Text = "";
        }
      

    }
    protected void gridocsubtype1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridocsubtype1.PageIndex = e.NewPageIndex;
        FillDocumentSubType();
    }
}
