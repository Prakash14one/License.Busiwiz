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

public partial class ShoppingCart_Admin_FrmBusinessMaster : System.Web.UI.Page
{
    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);

    SqlConnection conn = new SqlConnection(PageConn.connnn);

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            Fillwarehouse();

            fillgrid();
            ddlstore_SelectedIndexChanged(sender, e);

        }
    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillempddl();
    }

    protected void fillstore()
    {
        ddlstore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlstore.DataSource = ds;
        ddlstore.DataTextField = "Name";
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

    }

    protected void Fillwarehouse()
    {
        ddlstore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlstore.DataSource = ds;
        ddlstore.DataTextField = "Name";
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");

        DropDownList2.DataSource = null;
        DropDownList2.DataTextField = "Departmentname";
        DropDownList2.DataValueField = "id";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "All");
    }

    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {
        string te = "Departmentaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        fillempddl();
    }

    protected void fillempddl()
    {
        string stremp = "select *  from  DepartmentmasterMNC   where DepartmentmasterMNC.Whid='" + ddlstore.SelectedValue + "' Order by Departmentname";

        SqlCommand cmdemp = new SqlCommand(stremp, conn);
        SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
        DataSet dsemp = new DataSet();
        adpemp.Fill(dsemp);
        ddldepartment.DataSource = dsemp;
        ddldepartment.DataTextField = "Departmentname";
        ddldepartment.DataValueField = "id";
        ddldepartment.DataBind();

        ddldepartment.Items.Insert(0, "-Select-");
        ddldepartment.Items[0].Value = "0";

    }
    protected void Button26_Click(object sender, EventArgs e)
    {
        string st1 = "select * from BusinessMaster where Whid='" + ddlstore.SelectedValue + "' and BusinessName='" + TextBox1.Text + "' and DepartmentId='" + ddldepartment.SelectedValue + "'  ";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, Record already exist ";
        }
        else
        {
            String str = "Insert Into BusinessMaster (BusinessName,company_id,Whid,DepartmentId)values('" + TextBox1.Text + "','" + Session["Comid"].ToString() + "','" + ddlstore.SelectedValue + "','" + ddldepartment.SelectedValue + "')";
            SqlCommand cmd = new SqlCommand(str, conn);

            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmd.ExecuteNonQuery();
            conn.Close();

            String strmax = " select max(BusinessID) as Id  from BusinessMaster ";
            SqlCommand cmdmax = new SqlCommand(strmax, conn);
            SqlDataAdapter damax = new SqlDataAdapter(cmdmax);
            DataTable dsmax = new DataTable();
            damax.Fill(dsmax);

            String strcmp = "Insert Into Company_Business (company_id,BusinessID) values('" + Session["Comid"].ToString() + "','" + dsmax.Rows[0]["Id"].ToString() + "')";
            SqlCommand cmdcmp = new SqlCommand(strcmp, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmdcmp.ExecuteNonQuery();
            conn.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
            clearall();

            fillstore();
            ddlstore_SelectedIndexChanged(sender, e);
            fillgrid();

            btnadddd.Visible = true;
            pnladdd.Visible = false;
            lbllegend.Text = "";
        }
    }
    protected void clearall()
    {
        ddlstore.SelectedIndex = 0;
        ddldepartment.SelectedIndex = 0;
        TextBox1.Text = "";



    }
    protected void fillgrid()
    {
        string st1 = "";
        lblCompany.Text = Session["Cname"].ToString();
        lblBusiness.Text = "All";
        lblDepartment.Text = "All";

        string st2 = "";
        if (DropDownList1.SelectedIndex > 0)
        {
            lblBusiness.Text = DropDownList1.SelectedItem.Text;
            st2 = " and BusinessMaster.Whid='" + DropDownList1.SelectedValue + "'";
        }

        string st3 = "";
        if (DropDownList2.SelectedIndex > 0)
        {
            lblDepartment.Text = DropDownList2.SelectedItem.Text;
            st3 = " and BusinessMaster.DepartmentId='" + DropDownList2.SelectedValue + "'";
        }

        st1 = " BusinessMaster.*,WareHouseMaster.Name as Wname,DepartmentmasterMNC.Departmentname as Dname from BusinessMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=BusinessMaster.Whid left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId   where BusinessMaster.company_id='" + Session["comid"] + "' " + st2 + "" + st3 + "";

        string str2 = "select count(BusinessMaster.BusinessID) as ci from BusinessMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=BusinessMaster.Whid left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId   where BusinessMaster.company_id='" + Session["comid"] + "' " + st2 + "" + st3 + "";

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt1 = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, st1);

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

        //st1 = st1 + st2 + st3;

        //SqlCommand cmd1 = new SqlCommand(st1, conn);
        //SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        //DataTable dt1 = new DataTable();
        //adp1.Fill(dt1);
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
        SqlCommand cmd = new SqlCommand(qu, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "vi")
        {
            lbllegend.Text = "Edit Division";
            pnladdd.Visible = true;
            btnadddd.Visible = false;
            lblmsg.Text = "";

            //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["Id"] = mm;
            //GridView1.SelectedDataKey.Value;
            Button28.Visible = true;
            Button26.Visible = false;

            string str12 = "select * from BusinessMaster where BusinessID='" + ViewState["Id"] + "'";
            SqlCommand cmd1 = new SqlCommand(str12, conn);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            adp1.Fill(ds1);

            Fillwarehouse();
            ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(ds1.Rows[0]["Whid"].ToString()));

            fillempddl();
            ddldepartment.SelectedIndex = ddldepartment.Items.IndexOf(ddldepartment.Items.FindByValue(ds1.Rows[0]["DepartmentId"].ToString()));

            TextBox1.Text = ds1.Rows[0]["BusinessName"].ToString();
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from BusinessMaster where BusinessID='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, conn);
        conn.Open();
        cmd2.ExecuteNonQuery();
        conn.Close();
        GridView1.EditIndex = -1;
        fillgrid();
        lblmsg.Visible = true;
        lblmsg.Text = "Record deleted successfully";
    }
    protected void Button28_Click(object sender, EventArgs e)
    {
        string st1 = "select * from BusinessMaster where Whid='" + ddlstore.SelectedValue + "' and BusinessName='" + TextBox1.Text + "' and DepartmentId='" + ddldepartment.SelectedValue + "' and BusinessID<>'" + ViewState["Id"] + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, Record already exist";
        }
        else
        {
            String str = "Update BusinessMaster set BusinessName='" + TextBox1.Text + "',company_id='" + Session["Comid"].ToString() + "',Whid='" + ddlstore.SelectedValue + "',DepartmentId='" + ddldepartment.SelectedValue + "' where BusinessID='" + ViewState["Id"] + "' ";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";
            clearall();
            fillstore();
            ddlstore_SelectedIndexChanged(sender, e);
            fillgrid();
            Button28.Visible = false;
            Button26.Visible = true;

            btnadddd.Visible = true;
            pnladdd.Visible = false;
            lbllegend.Text = "";
        }


    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void Button27_Click(object sender, EventArgs e)
    {
        clearall();
        fillstore();
        ddlstore_SelectedIndexChanged(sender, e);
        lblmsg.Text = "";
        Button26.Visible = true;
        Button28.Visible = false;

        btnadddd.Visible = true;
        pnladdd.Visible = false;
        lbllegend.Text = "";
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList2.Items.Clear();
        if (DropDownList1.SelectedIndex > 0)
        {
            string stremp = "select *  from  DepartmentmasterMNC   where DepartmentmasterMNC.Whid='" + DropDownList1.SelectedValue + "' Order by Departmentname ";

            SqlCommand cmdemp = new SqlCommand(stremp, conn);
            SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
            DataSet dsemp = new DataSet();
            adpemp.Fill(dsemp);
            DropDownList2.DataSource = dsemp;
            DropDownList2.DataTextField = "Departmentname";
            DropDownList2.DataValueField = "id";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "All");
        }
        else
        {
            DropDownList2.Items.Clear();
            DropDownList2.DataSource = null;
            DropDownList2.DataTextField = "Departmentname";
            DropDownList2.DataValueField = "id";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "All");
        }
        fillgrid();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillgrid();

            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[3].Visible = false;
            }
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button7.Visible = false;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            fillgrid();

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        lbllegend.Text = "Add New Division";
        pnladdd.Visible = true;
        btnadddd.Visible = false;
        lblmsg.Text = "";
    }
}
