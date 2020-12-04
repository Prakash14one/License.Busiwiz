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
public partial class Account_AddBusinessRuleType : System.Web.UI.Page
{
    SqlConnection con;
    EmployeeCls clsEmployee = new EmployeeCls();
    InstructionCls clsInstruction = new InstructionCls();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/ShoppingCart/Admin/ShoppingCartLogin.aspx");
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

        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            lblcom.Text = Session["Cname"].ToString();

            fillstore();
            fillfilterstore();
            FillRuleType();



        }

    }
    protected void FillRuleType()
    {
        lblcomname.Text = ddlbusifil.SelectedItem.Text;
        string str = "";
        string str2 = "";

        // dt = clsInstruction.SelectRuleTypeMaster(ddlbusifil.SelectedValue);

        if (ddlbusifil.SelectedIndex > 0)
        {
            str = " RuleTypeMaster.*,WareHouseMaster.Name from RuleTypeMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=RuleTypeMaster.Whid where CID='" + Session["Comid"] + "' and Whid='" + ddlbusifil.SelectedValue + "'";

            str2 = "select count(RuleTypeMaster.RuleTypeId) as ci from RuleTypeMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=RuleTypeMaster.Whid where CID='" + Session["Comid"] + "' and Whid='" + ddlbusifil.SelectedValue + "'";
        }
        else
        {
            str = " RuleTypeMaster.*,WareHouseMaster.Name from RuleTypeMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=RuleTypeMaster.Whid where CID='" + Session["Comid"] + "'";

            str2 = "select count(RuleTypeMaster.RuleTypeId) as ci from RuleTypeMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=RuleTypeMaster.Whid where CID='" + Session["Comid"] + "'";
        }

        griddocapprovaltype.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " RuleTypeMaster.RuleTypeId asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(griddocapprovaltype.PageIndex, griddocapprovaltype.PageSize, sortExpression, str);

            griddocapprovaltype.DataSource = dt;
            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
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
        lblmsg.Text = "";
        if (txtftp.Text != "")
        {
            string str = "SELECT * FROM RuleTypeMaster where RuleType = '" + txtftp.Text + "'and Whid='" + ddlbusiness.SelectedValue + "' ";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                Int32 EmployeeId = clsEmployee.InsertRuleTypeMaster(txtftp.Text, ddlbusiness.SelectedValue);
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                txtftp.Text = "";
                FillRuleType();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Record is already exist";
            }
        }

        pnladd.Visible = false;
        Label6.Visible = false;
        btnadd.Visible = true;
        Label6.Text = "Add New Document Flow Rule Type";

    }
    protected void griddocapprovaltype_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        griddocapprovaltype.EditIndex = -1;

        FillRuleType();
    }
    protected void griddocapprovaltype_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblmsg.Text = "";
        if (e.CommandName == "Edit")
        {
            lblmsg.Text = "";
            int dk1 = Convert.ToInt32(e.CommandArgument);
            ViewState["MasterId"] = dk1.ToString();

            SqlCommand cmdmaster = new SqlCommand("SELECT * FROM RuleTypeMaster where RuleTypeId='" + dk1 + "' ", con);
            SqlDataAdapter adpmaster = new SqlDataAdapter(cmdmaster);
            DataTable dtmaster = new DataTable();
            adpmaster.Fill(dtmaster);

            if (dtmaster.Rows.Count > 0)
            {
                fillstore();
                ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dtmaster.Rows[0]["Whid"].ToString()));

                txtftp.Text = dtmaster.Rows[0]["RuleType"].ToString();

                imgbtnsubmit.Visible = false;
                Button3.Visible = true;

                pnladd.Visible = true;
                Label6.Visible = true;
                btnadd.Visible = false;
                Label6.Text = "Edit Document Flow Rule Type";


            }
        }


    }
    protected void griddocapprovaltype_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblmsg.Text = "";
        int currentRowIndex = Int32.Parse(e.RowIndex.ToString());

        Label lblWhid = (Label)(griddocapprovaltype.Rows[currentRowIndex].Cells[1].FindControl("lblWhid"));

        ViewState["Whid"] = lblWhid.Text;

        string RuleTypeId = griddocapprovaltype.DataKeys[currentRowIndex].Value.ToString();

        hdnRuleTypeId.Value = RuleTypeId.ToString();

        DataTable dtType = new DataTable();

        dtType = clsInstruction.SelectRuleMasterRuleTypeWise(Convert.ToInt32(hdnRuleTypeId.Value), ViewState["Whid"].ToString());

        if (dtType.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You cannot delete this rule type as these rule type exist in documnet approval rule.Please delete the approval rule, then try again. ";
        }
        else
        {
            bool dlt = clsInstruction.DeleteRuleTypeMaster(Convert.ToInt32(hdnRuleTypeId.Value));
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";
            FillRuleType();



        }
    }
    protected void griddocapprovaltype_RowEditing(object sender, GridViewEditEventArgs e)
    {


    }

    protected void griddocapprovaltype_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillRuleType();
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
        FillRuleType();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {

            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;

            griddocapprovaltype.AllowPaging = false;
            griddocapprovaltype.PageSize = 1000;
            FillRuleType();

            if (griddocapprovaltype.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                griddocapprovaltype.Columns[2].Visible = false;
            }
            if (griddocapprovaltype.Columns[3].Visible == true)
            {
                ViewState["delHide"] = "tt";
                griddocapprovaltype.Columns[3].Visible = false;
            }
        }
        else
        {
            Button2.Text = "Printable Version";
            Button1.Visible = false;

            griddocapprovaltype.AllowPaging = true;
            griddocapprovaltype.PageSize = 10;
            FillRuleType();

            if (ViewState["editHide"] != null)
            {
                griddocapprovaltype.Columns[2].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                griddocapprovaltype.Columns[3].Visible = true;
            }
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        Label6.Visible = false;
        btnadd.Visible = true;
        Label6.Text = "Add New Document Flow Rule Type";

        clearall();
    }
    protected void clearall()
    {
        lblmsg.Text = "";
        txtftp.Text = "";
        Button3.Visible = false;
        imgbtnsubmit.Visible = true;
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
        lblmsg.Text = "";
        string str = "SELECT * FROM RuleTypeMaster where RuleType = '" + txtftp.Text + "' and Whid='" + ddlbusiness.SelectedValue + "' and RuleTypeId<>'" + ViewState["MasterId"].ToString() + "' ";

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count == 0)
        {
            bool success = clsEmployee.UpdateRuleTypeMaster(Convert.ToInt32(ViewState["MasterId"]), txtftp.Text, ddlbusiness.SelectedValue);
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
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record is already exist";
        }

        griddocapprovaltype.EditIndex = -1;
        FillRuleType();

        Button3.Visible = false;
        imgbtnsubmit.Visible = true;
        pnladd.Visible = false;
        Label6.Visible = false;
        btnadd.Visible = true;
        Label6.Text = "Add New Document Flow Rule Type";
        clearall();

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            Label6.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            Label6.Visible = false;
        }
        btnadd.Visible = false;
        Label6.Text = "Add New Document Flow Rule Type";
        clearall();



    }
    protected void griddocapprovaltype_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddocapprovaltype.PageIndex = e.NewPageIndex;
        FillRuleType();
    }
}
