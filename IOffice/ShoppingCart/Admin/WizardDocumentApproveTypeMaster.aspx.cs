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
public partial class WizardDocumentApproveTypeMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);
    DocumentCls1 clsDocument = new DocumentCls1();
    InstructionCls clsInstruction = new InstructionCls();
    MasterCls clsMaster = new MasterCls();
    EmployeeCls clsEmployee = new EmployeeCls();
    protected void Page_Load(object sender, EventArgs e)
    {

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
            lblcomid.Text = Session["Cname"].ToString();
            ViewState["sortOrder"] = "";
            ViewState["sortOrder"] = "";
            ViewState["Dltitem"] = "";
            fillstore();
            fillfilterstore();
            FillDocumentApproveType();

        }

    }
    protected void FillDocumentApproveType()
    {
        lblcomname.Text = ddlbusifil.SelectedItem.Text;

        string str = "";
        string str2 = "";

        if (ddlbusifil.SelectedIndex > 0)
        {
            str = " RuleApproveTypeMaster.*,WareHouseMaster.Name from RuleApproveTypeMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=RuleApproveTypeMaster.Whid where CID='" + Session["Comid"] + "' and RuleApproveTypeMaster.Whid='" + ddlbusifil.SelectedValue + "'";

            str2 = "select count(RuleApproveTypeMaster.RuleApproveTypeId) as ci from RuleApproveTypeMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=RuleApproveTypeMaster.Whid where CID='" + Session["Comid"] + "' and RuleApproveTypeMaster.Whid='" + ddlbusifil.SelectedValue + "'";
        }
        else
        {
            str = " RuleApproveTypeMaster.*,WareHouseMaster.Name from RuleApproveTypeMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=RuleApproveTypeMaster.Whid where CID='" + Session["Comid"] + "'";

            str2 = "select count(RuleApproveTypeMaster.RuleApproveTypeId) as ci from RuleApproveTypeMaster inner join WareHouseMaster on WareHouseMaster.WarehouseId=RuleApproveTypeMaster.Whid where CID='" + Session["Comid"] + "'";
        }

        griddocapprovaltype.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " RuleApproveTypeMaster.RuleApproveTypeId asc";

        // dt = clsInstruction.SelectRuleApproveTypeMaster(ddlbusifil.SelectedValue);

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(griddocapprovaltype.PageIndex, griddocapprovaltype.PageSize, sortExpression, str);

            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            griddocapprovaltype.DataSource = dt;
            griddocapprovaltype.DataBind();
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
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));

        dt.Columns.Remove("ROW_NUM");

        return dt;

        ViewState["dt"] = dt;
    }

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {
        string str = "SELECT * FROM RuleApproveTypeMaster where RuleApproveTypeName = '" + txtdocapprvltype.Text + "'and Whid='" + ddlbusiness.SelectedValue + "' ";

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count == 0)
        {
            clsDocument = new DocumentCls1();
            Int32 rst = clsDocument.InsertDocumentApproveTypeMaster(txtdocapprvltype.Text, txtdesc.Text, ddlbusiness.SelectedValue);
            if (rst > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted Successfully.";
                FillDocumentApproveType();
                clearall();
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";
        }
        pnladd.Visible = false;
        Label8.Visible = false;
        btnadd.Visible = true;
        Label8.Text = "Add New Document Approval Type";
    }
    protected void griddocapprovaltype_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblmsg.Text = "";
        if (e.CommandName == "Sort")
        {
            return;
        }

        int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "Edit")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["Id"] = id.ToString();

            SqlCommand cmdmaster = new SqlCommand("Select * from RuleApproveTypeMaster where RuleApproveTypeId='" + id + "' ", con);
            SqlDataAdapter adpmaster = new SqlDataAdapter(cmdmaster);
            DataTable dtmaster = new DataTable();
            adpmaster.Fill(dtmaster);

            if (dtmaster.Rows.Count > 0)
            {
                fillstore();
                ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dtmaster.Rows[0]["Whid"].ToString()));

                txtdocapprvltype.Text = dtmaster.Rows[0]["RuleApproveTypeName"].ToString();
                txtdesc.Text = dtmaster.Rows[0]["Description"].ToString();

                imgbtnsubmit.Visible = false;
                Button3.Visible = true;

                pnladd.Visible = true;
                Label8.Visible = true;
                btnadd.Visible = false;
                Label8.Text = "Edit Document Approval Type";


            }
        }
        if (e.CommandName == "Delete1")
        {
            Int32 id = Convert.ToInt32(e.CommandArgument.ToString());

            DataTable dtSubType = new DataTable();
            dtSubType = clsInstruction.SelectRuleDetailByRuleApproveTypeId(Convert.ToInt32(id));
            if (dtSubType.Rows.Count > 0)
            {
                //string str="<a href=BusinessProcessRules.aspx target=_blank style=color:Red> here</a>";
                lblmsg.Visible = true;
                lblmsg.Text = "You cannot delete this approval type as there are approval rule details attached to this record. Please click  <a href=BusinessProcessRules.aspx target=_blank style=color:Red;> here</a> to delete the approval rule details, then try again.";
            }
            else
            {
                bool dlt = clsDocument.DeleteRuleApproveTypeMasterByID(Convert.ToInt32(id));

                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully";

            }
            FillDocumentApproveType();
        }

    }
    protected void griddocapprovaltype_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }


    protected void griddocapprovaltype_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        griddocapprovaltype.EditIndex = -1;

        FillDocumentApproveType();
    }


    protected void griddocapprovaltype_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillDocumentApproveType();

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



    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentApproveType();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;

            griddocapprovaltype.AllowPaging = false;
            griddocapprovaltype.PageSize = 1000;
            FillDocumentApproveType();

            if (griddocapprovaltype.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                griddocapprovaltype.Columns[3].Visible = false;
            }
            if (griddocapprovaltype.Columns[4].Visible == true)
            {
                ViewState["delHide"] = "tt";
                griddocapprovaltype.Columns[4].Visible = false;
            }
        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(250);

            Button2.Text = "Printable Version";
            Button1.Visible = false;

            griddocapprovaltype.AllowPaging = true;
            griddocapprovaltype.PageSize = 10;
            FillDocumentApproveType();

            if (ViewState["editHide"] != null)
            {
                griddocapprovaltype.Columns[3].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                griddocapprovaltype.Columns[4].Visible = true;
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
        ddlbusifil.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlbusifil.DataSource = ds;
        ddlbusifil.DataTextField = "Name";
        ddlbusifil.DataValueField = "WareHouseId";
        ddlbusifil.DataBind();
        ddlbusifil.Items.Insert(0, "All");
        ddlbusifil.Items[0].Value = "0";



    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string str = "SELECT * FROM RuleApproveTypeMaster where RuleApproveTypeName = '" + txtdocapprvltype.Text + "' and Whid='" + ddlbusiness.SelectedValue + "' and RuleApproveTypeId<>'" + ViewState["Id"].ToString() + "' ";

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count == 0)
        {
            bool success = clsDocument.UpdateDocumentApproveTypeMaster(Convert.ToInt32(ViewState["Id"].ToString()), txtdocapprvltype.Text, txtdesc.Text, ddlbusiness.SelectedValue);
            if (Convert.ToString(success) == "True")
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Record not updated successfully";

            }
            clearall();
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";
        }

        griddocapprovaltype.EditIndex = -1;

        FillDocumentApproveType();

        pnladd.Visible = false;
        Label8.Visible = false;
        btnadd.Visible = true;
        Label8.Text = "Add New Document Approval Type";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        Label8.Visible = false;
        btnadd.Visible = true;
        Label8.Text = "Add New Document Approval Type";
        clearall();
    }
    protected void clearall()
    {
        txtdocapprvltype.Text = "";
        txtdesc.Text = "";
        imgbtnsubmit.Visible = true;
        Button3.Visible = false;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            Label8.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            Label8.Visible = false;
        }
        btnadd.Visible = false;

        Label8.Text = "Add New Document Approval Type";
        lblmsg.Text = "";

    }
    protected void griddocapprovaltype_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void griddocapprovaltype_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddocapprovaltype.PageIndex = e.NewPageIndex;
        FillDocumentApproveType();
    }
}
