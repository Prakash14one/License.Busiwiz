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
using System.Net.NetworkInformation;
using System.IO;
using System.Text;
using System.Data.Common;
using System.Diagnostics;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Net;
using System.Net.Mail;


public partial class ShoppingCart_Admin_ProcedureTitleMaster : System.Web.UI.Page
{
    SqlConnection con;
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

        ViewState["sortOrder"] = "";


        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            fillstore();
            fillpolicy();
            filterstore();
            ddlpolicy.Items.Insert(0, "-Select-");
            ddlpolicy.Items[0].Value = "0";

            ddlfilterpolicyprocedure.Items.Insert(0, "All");
            ddlfilterpolicyprocedure.Items[0].Value = "0";

            ddlfilterpolicy.Items.Insert(0, "All");
            ddlfilterpolicy.Items[0].Value = "0";

            fillgrid();



        }
    }
    protected void fillstore()
    {
        ddlWarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlWarehouse.DataSource = ds;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "WareHouseId";
        ddlWarehouse.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string st1 = "select * from Procedureforpolicytbl where Whid='" + ddlWarehouse.SelectedValue + "' and ProcedureTitle='" + txtprocedure.Text + "' and PolicyId='" + ddlpolicy.SelectedValue + "' ";
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
            bool access = UserAccess.Usercon("Procedureforpolicytbl", "", "Id", "", "", "Compid", "Procedureforpolicytbl");
            if (access == true)
            {
                String str = "Insert Into Procedureforpolicytbl (ProcedureTitle,PolicyprocedurecategoryId,PolicyId,Whid,compid)values('" + txtprocedure.Text + "','" + ddlpolicyprocedurerule.SelectedValue + "','" + ddlpolicy.SelectedValue + "','" + ddlWarehouse.SelectedValue + "','" + Session["Comid"] + "')";

                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                txtprocedure.Text = "";
            }
            else
            {
                lblmsg.Text = "";
                lblmsg.Text = "Sorry, you don't permitted greater record to priceplan";
            }

            fillgrid();
            Pnladdnew.Visible = false;
            btnadd.Visible = true;
            lbllegend.Text = "";
        }
    }
    protected void fillgrid()
    {
        lblcmpny.Text = Session["Cname"].ToString();
        lblBusiness.Text = ddlfilterbybusiness.SelectedItem.Text;

        string st2 = "";

        if (ddlfilterbybusiness.SelectedIndex > 0)
        {
            st2 += " and Procedureforpolicytbl.Whid='" + ddlfilterbybusiness.SelectedValue + "'";
        }
        if (ddlfilterpolicyprocedure.SelectedIndex > 0)
        {
            st2 += " and Procedureforpolicytbl.PolicyprocedurecategoryId='" + ddlfilterpolicyprocedure.SelectedValue + "'";
        }
        if (ddlfilterpolicy.SelectedIndex > 0)
        {
            st2 += "and Procedureforpolicytbl.PolicyId='" + ddlfilterpolicy.SelectedValue + "'";
        }

        string str1 = " Procedureforpolicytbl.*,Policyprocedureruletiletbl.PolicyTitle,Policyprocedureruletbl.Policyprocedurecategory,WareHouseMaster.Name as WName from Procedureforpolicytbl inner join Policyprocedureruletiletbl on Procedureforpolicytbl.PolicyId = Policyprocedureruletiletbl.Id inner join Policyprocedureruletbl on Policyprocedureruletiletbl.Policyprocedureruleid=Policyprocedureruletbl.Id  inner join WareHouseMaster on WareHouseMaster.WareHouseId=Policyprocedureruletiletbl.Whid where  Policyprocedureruletiletbl.compid='" + Session["Comid"] + "' and WareHouseMaster.status='1' " + st2 + "  ";

        string str2 = " select Count(Procedureforpolicytbl.Id) as ci from Procedureforpolicytbl inner join Policyprocedureruletiletbl on Procedureforpolicytbl.PolicyId = Policyprocedureruletiletbl.Id inner join Policyprocedureruletbl on Policyprocedureruletiletbl.Policyprocedureruleid=Policyprocedureruletbl.Id  inner join WareHouseMaster on WareHouseMaster.WareHouseId=Policyprocedureruletiletbl.Whid where  Policyprocedureruletiletbl.compid='" + Session["Comid"] + "' and WareHouseMaster.status='1' " + st2 + "  ";

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,Policyprocedurecategory,PolicyTitle,ProcedureTitle";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt1 = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str1);

            DataView myDataView = new DataView();
            myDataView = dt1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataSource = myDataView;

            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;

            GridView1.DataBind();
        } 
    }
    protected void btnprintableversion_Click(object sender, EventArgs e)
    {
        if (btnprintableversion.Text == "Printable Version")
        {
            btnprintableversion.Text = "Hide Printable Version";
            Button7.Visible = true;

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillgrid();

            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["docth"] = "tt";
                GridView1.Columns[6].Visible = false;
            }
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[5].Visible = false;
            }


        }
        else
        {
            btnprintableversion.Text = "Printable Version";
            Button7.Visible = false;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            fillgrid();

            if (ViewState["docth"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }
            if (ViewState["edith"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }


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
    public static PhysicalAddress GetMacAddress()
    {
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            // Only consider Ethernet network interfaces
            if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                nic.OperationalStatus == OperationalStatus.Up)
            {
                return nic.GetPhysicalAddress();
            }
        }
        return null;
    }
    protected void ddlfilterbybusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilterpolicy();
        fillgrid();


    }
    protected void filterstore()
    {
        ddlfilterbybusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlfilterbybusiness.DataSource = ds;
        ddlfilterbybusiness.DataTextField = "Name";
        ddlfilterbybusiness.DataValueField = "WareHouseId";
        ddlfilterbybusiness.DataBind();
        ddlfilterbybusiness.Items.Insert(0, "All");
        ddlfilterbybusiness.Items[0].Value = "0";



    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();
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
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpolicy();

    }
    protected void fillpolicy()
    {
        ddlpolicyprocedurerule.Items.Clear();
        string st1 = "select * from Policyprocedureruletbl where Whid='" + ddlWarehouse.SelectedValue + "' and compid='" + Session["Comid"] + "'  ";
        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

        ddlpolicyprocedurerule.DataSource = dt1;
        ddlpolicyprocedurerule.DataTextField = "Policyprocedurecategory";
        ddlpolicyprocedurerule.DataValueField = "Id";
        ddlpolicyprocedurerule.DataBind();

        ddlpolicyprocedurerule.Items.Insert(0, "-Select-");
        ddlpolicyprocedurerule.Items[0].Value = "0";
    }

    protected void fillfilterpolicy()
    {
        ddlfilterpolicyprocedure.Items.Clear();
        string st1 = "select * from Policyprocedureruletbl where compid='" + Session["Comid"] + "'  ";

        if (ddlfilterbybusiness.SelectedIndex > 0)
        {
            st1 += " and Policyprocedureruletbl.Whid='" + ddlfilterbybusiness.SelectedValue + "'";
        }

        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

        ddlfilterpolicyprocedure.DataSource = dt1;
        ddlfilterpolicyprocedure.DataTextField = "Policyprocedurecategory";
        ddlfilterpolicyprocedure.DataValueField = "Id";
        ddlfilterpolicyprocedure.DataBind();

        ddlfilterpolicyprocedure.Items.Insert(0, "All");
        ddlfilterpolicyprocedure.Items[0].Value = "0";

    }
    protected void ddlfilterpolicyprocedure_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilterpolicytitle();
        fillgrid();
    }
    protected void fillPolicyTitle()
    {
        ddlpolicy.Items.Clear();
        string st1 = "select * from Policyprocedureruletiletbl where compid='" + Session["Comid"] + "'  ";

        if (ddlpolicyprocedurerule.SelectedIndex > 0)
        {
            st1 += " and Policyprocedureruletiletbl.Policyprocedureruleid='" + ddlpolicyprocedurerule.SelectedValue + "'";
        }

        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

        ddlpolicy.DataSource = dt1;
        ddlpolicy.DataTextField = "PolicyTitle";
        ddlpolicy.DataValueField = "Id";
        ddlpolicy.DataBind();

        ddlpolicy.Items.Insert(0, "-Select-");
        ddlpolicy.Items[0].Value = "0";


    }
    protected void fillfilterpolicytitle()
    {
        ddlfilterpolicy.Items.Clear();
        string st1 = "select * from Policyprocedureruletiletbl where compid='" + Session["Comid"] + "'  ";

        if (ddlfilterpolicyprocedure.SelectedIndex > 0)
        {
            st1 += " and Policyprocedureruletiletbl.Policyprocedureruleid='" + ddlfilterpolicyprocedure.SelectedValue + "'";
        }

        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        ddlfilterpolicy.DataSource = dt1;
        ddlfilterpolicy.DataTextField = "PolicyTitle";
        ddlfilterpolicy.DataValueField = "Id";
        ddlfilterpolicy.DataBind();

        ddlfilterpolicy.Items.Insert(0, "All");
        ddlfilterpolicy.Items[0].Value = "0";

    }
    protected void ddlfilterpolicy_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlpolicyprocedurerule_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillPolicyTitle();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        fillstore();
        fillpolicy();
        ddlpolicyprocedurerule.SelectedIndex = 0;
        fillPolicyTitle();
        ddlpolicy.SelectedIndex = 0;

        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lblmsg.Text = "";
        lbllegend.Text = "";
        btnsubmit.Visible = true;
        btnupdate.Visible = false;

        txtprocedure.Text = "";

    }
    protected void imgadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PolicyProcedureRuleCategory.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgaddtitle_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PolicyCategoryTitle.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgrefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillpolicy();
    }
    protected void imgrefreshtitle_Click(object sender, ImageClickEventArgs e)
    {
        fillPolicyTitle();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        lbllegend.Text = "Add New Procedure";
        Pnladdnew.Visible = true;
        lblmsg.Text = "";
        btnadd.Visible = false;

        fillstore();
        fillpolicy();
        ddlpolicyprocedurerule.SelectedIndex = 0;
        fillPolicyTitle();
        ddlpolicy.SelectedIndex = 0;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        bool access = UserAccess.Usercon("Procedureforpolicytbl", "", "Id", "", "", "Compid", "Procedureforpolicytbl");
        if (access == true)
        {
            String str = "Update  Procedureforpolicytbl  set ProcedureTitle='" + txtprocedure.Text + "',PolicyprocedurecategoryId='" + ddlpolicyprocedurerule.SelectedValue + "',PolicyId='" + ddlpolicy.SelectedValue + "',Whid='" + ddlWarehouse.SelectedValue + "',compid='" + Session["Comid"] + "' where Id='" + ViewState["Id"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";
            txtprocedure.Text = "";
        }
        else
        {
            lblmsg.Text = "";
            lblmsg.Text = "Sorry, you don't permitted greater record to priceplan";
        }
        fillgrid();

        btnsubmit.Visible = true;
        btnupdate.Visible = false;

        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "Edit Procedure";
            btnadd.Visible = false;
            lblmsg.Text = "";
            Pnladdnew.Visible = true;

            btnsubmit.Visible = false;
            btnupdate.Visible = true;

            int mm = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = mm;

            string st1 = "select * from Procedureforpolicytbl where Id='" + ViewState["Id"] + "' ";
            SqlCommand cmd1 = new SqlCommand(st1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                fillstore();
                ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByValue(Convert.ToInt32(dt1.Rows[0]["Whid"]).ToString()));

                fillpolicy();
                ddlpolicyprocedurerule.SelectedIndex = ddlpolicyprocedurerule.Items.IndexOf(ddlpolicyprocedurerule.Items.FindByValue(Convert.ToInt32(dt1.Rows[0]["PolicyprocedurecategoryId"]).ToString()));

                fillPolicyTitle();
                ddlpolicy.SelectedIndex = ddlpolicy.Items.IndexOf(ddlpolicy.Items.FindByValue(Convert.ToInt32(dt1.Rows[0]["Policyid"]).ToString()));

                txtprocedure.Text = dt1.Rows[0]["ProcedureTitle"].ToString();
            }
        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            SqlDataAdapter da3 = new SqlDataAdapter("select Id from ProcedureAddManagetbl where ProcedureId=" + mm1, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {

                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, cannot delete this record because a Policy, Procedure or Rule exists for this category. Please delete those records and try again.";

            }
            else
            {

                string st2 = "Delete from Procedureforpolicytbl where Id='" + mm1 + "' ";
                SqlCommand cmd = new SqlCommand(st2, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully";

            }
            fillgrid();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
}
