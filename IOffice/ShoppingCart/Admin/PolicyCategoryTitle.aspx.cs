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

public partial class ShoppingCart_Admin_PolicyCategoryTitle : System.Web.UI.Page
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
            lblCompany.Text = Session["Comid"].ToString();
            fillstore();
            fillpolicy();
            filterstore();
            fillfilterpolicy();
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
        string st1 = "select * from Policyprocedureruletiletbl where Whid='" + ddlWarehouse.SelectedValue + "' and PolicyTitle='" + txtpolicycat.Text + "' and Policyprocedureruleid='" + ddlpolicyprocedurerule.SelectedValue + "' ";
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

            String str = "Insert Into Policyprocedureruletiletbl (Policyprocedureruleid,PolicyTitle,Whid,compid)values('" + ddlpolicyprocedurerule.SelectedValue + "','" + txtpolicycat.Text + "','" + ddlWarehouse.SelectedValue + "','" + Session["Comid"] + "')";

            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
            txtpolicycat.Text = "";

            fillgrid();
            Pnladdnew.Visible = false;
            btnadd.Visible = true;
            lbllegend.Text = "";
            txtpolicycat.Text = "";
        }
    }
    protected void fillgrid()
    {
        lblBusiness.Text = ddlfilterbybusiness.SelectedItem.Text;

        string st2 = "";

        if (ddlfilterbybusiness.SelectedIndex > 0)
        {
            st2 += " and Policyprocedureruletiletbl.Whid='" + ddlfilterbybusiness.SelectedValue + "'";
        }

        if (ddlfilterpolicyprocedure.SelectedIndex > 0)
        {
            st2 += " and Policyprocedureruletiletbl.Policyprocedureruleid='" + ddlfilterpolicyprocedure.SelectedValue + "'";
        }

        string str1 = " Policyprocedureruletiletbl.*,Policyprocedureruletbl.Policyprocedurecategory,WareHouseMaster.Name as WName from Policyprocedureruletiletbl inner join Policyprocedureruletbl on Policyprocedureruletiletbl.Policyprocedureruleid=Policyprocedureruletbl.Id  inner join WareHouseMaster on WareHouseMaster.WareHouseId=Policyprocedureruletiletbl.Whid where  Policyprocedureruletiletbl.compid='" + Session["Comid"] + "' and WareHouseMaster.status='1' " + st2 + "  ";

        string str2 = " select Count(Policyprocedureruletiletbl.Id) as ci from Policyprocedureruletiletbl inner join Policyprocedureruletbl on Policyprocedureruletiletbl.Policyprocedureruleid=Policyprocedureruletbl.Id  inner join WareHouseMaster on WareHouseMaster.WareHouseId=Policyprocedureruletiletbl.Whid where  Policyprocedureruletiletbl.compid='" + Session["Comid"] + "' and WareHouseMaster.status='1' " + st2 + "  ";

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,Policyprocedurecategory,Policyprocedureruletiletbl.PolicyTitle";

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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "Edit Policy";
            btnadd.Visible = false;
            lblmsg.Text = "";
            Pnladdnew.Visible = true;

            btnsubmit.Visible = false;
            btnupdate.Visible = true;

            //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //ViewState["Id"] = GridView1.SelectedDataKey.Value;

            int mm = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = mm;

            string st1 = "select * from Policyprocedureruletiletbl where Id='" + ViewState["Id"] + "' ";
            SqlCommand cmd1 = new SqlCommand(st1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                fillstore();
                ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByValue(Convert.ToInt32(dt1.Rows[0]["Whid"]).ToString()));
                fillpolicy();
                ddlpolicyprocedurerule.SelectedIndex = ddlpolicyprocedurerule.Items.IndexOf(ddlpolicyprocedurerule.Items.FindByValue(Convert.ToInt32(dt1.Rows[0]["Policyprocedureruleid"]).ToString()));
                txtpolicycat.Text = dt1.Rows[0]["PolicyTitle"].ToString();
            }
        }
        if (e.CommandName == "Delete")
        {
            //ViewState["MasterId"] = Convert.ToInt32(e.CommandArgument);

            int mm1 = Convert.ToInt32(e.CommandArgument);

            //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //ViewState["MasterId"] = GridView1.SelectedDataKey.Value;

            SqlDataAdapter da1 = new SqlDataAdapter("select Id from Ruleforpolicytbl where PolicyId=" + mm1, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            SqlDataAdapter da2 = new SqlDataAdapter("select Id from PolicyAddManagetbl where PolicyId=" + mm1, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            SqlDataAdapter da3 = new SqlDataAdapter("select Id from Procedureforpolicytbl where PolicyId=" + mm1, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt1.Rows.Count > 0 || dt2.Rows.Count > 0 || dt3.Rows.Count > 0)
            {

                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, cannot delete this record because a Policy, Procedure or Rule exists for this category. Please delete those records and try again.";

            }
            else
            {

                string st2 = "Delete from Policyprocedureruletiletbl where Id='" + mm1 + "' ";
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
    protected void Update_Click(object sender, EventArgs e)
    {
        String str = "Update  Policyprocedureruletiletbl  set Policyprocedureruleid='" + ddlpolicyprocedurerule.SelectedValue + "',Whid='" + ddlWarehouse.SelectedValue + "',PolicyTitle='" + txtpolicycat.Text + "' where Id='" + ViewState["Id"] + "'";

        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully";
        txtpolicycat.Text = "";
        fillgrid();

        btnsubmit.Visible = true;
        btnupdate.Visible = false;

        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
        txtpolicycat.Text = "";

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {


    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

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


            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["docth"] = "tt";
                GridView1.Columns[4].Visible = false;
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
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["edith"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }

        }
    }
    protected void ddlfilterbybusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilterpolicy();
        fillgrid();

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

        if (ddlfilterbybusiness.SelectedIndex > 0)
        {
            string st1 = "select * from Policyprocedureruletbl where compid='" + Session["Comid"] + "' and Policyprocedureruletbl.Whid='" + ddlfilterbybusiness.SelectedValue + "'";

            SqlCommand cmd1 = new SqlCommand(st1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);

            ddlfilterpolicyprocedure.DataSource = dt1;
            ddlfilterpolicyprocedure.DataTextField = "Policyprocedurecategory";
            ddlfilterpolicyprocedure.DataValueField = "Id";
            ddlfilterpolicyprocedure.DataBind();
        }

        ddlfilterpolicyprocedure.Items.Insert(0, "All");
        ddlfilterpolicyprocedure.Items[0].Value = "0";

    }
    protected void ddlfilterpolicyprocedure_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void imgadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PolicyProcedureRuleCategory.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgrefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillpolicy();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        fillstore();
        fillpolicy();
        lbllegend.Text = "Add New Policy";
        Pnladdnew.Visible = true;
        lblmsg.Text = "";
        btnadd.Visible = false;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lblmsg.Text = "";
        lbllegend.Text = "";
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
        txtpolicycat.Text = "";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
}
