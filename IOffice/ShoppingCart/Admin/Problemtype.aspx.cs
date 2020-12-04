using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class Problemtype : System.Web.UI.Page
{
    SqlConnection con;
    string compid;
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


        Page.Title = pg.getPageTitle(page);
        compid = Session["comid"].ToString();
        if (!IsPostBack)
        {
            fillgrid();
            ViewState["sortOrder"] = "";
            lblcompny.Text = compid;
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
    protected void btnprint_Click(object sender, EventArgs e)
    {

        if (btnprint.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            btnprint.Text = "Hide Printable Version";
            btnin.Visible = true;
            if (GridView2.Columns[1].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView2.Columns[1].Visible = false;
            }
            if (GridView2.Columns[2].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView2.Columns[2].Visible = false;
            }

        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(100);

            btnprint.Text = "Printable Version";
            btnin.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView2.Columns[1].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView2.Columns[2].Visible = true;
            }

        }
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();
    }
    protected void clear()
    {
        txtproblm.Text = "";
        btnadd.Visible = true;
        panel1.Visible = false;
      
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
        lbllegend.Visible = false;
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string strwh = "SELECT * FROM ProblemTypeMaster where  ProblemName='" + txtproblm.Text + "' and compid='" + compid + "'";
        SqlCommand cmdwh = new SqlCommand(strwh, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);
        if (dtwh.Rows.Count > 0)
        {
            lblmasg.Visible = true;
            lblmasg.Text = "Record already exists";
        }
        else
        {
            string str = "insert into ProblemTypeMaster values('" + txtproblm.Text.Trim() + "','" + compid + "')";
            SqlCommand mycmd = new SqlCommand(str, con);



            if (con.State.ToString() != "Open")
            {
                con.Open();
            }

            mycmd.ExecuteNonQuery();
            con.Close();
            lblmasg.Visible = true;
            lblmasg.Text = "Record inserted successfully";
            fillgrid();

            panel1.Visible = false;
            btnadd.Visible = true;
            clear();
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        lblmasg.Visible = false;
        panel1.Visible = true;
        btnadd.Visible = false;
        lbllegend.Text = "Add New Customer Problem Type";
        lbllegend.Visible = true;

    }
    protected void fillgrid()
    {
        SqlCommand cmd = new SqlCommand("select * from ProblemTypeMaster where compid='" + compid + "' order by ProblemName ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);


        GridView2.DataSource = dt;
        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }


        GridView2.DataBind();

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        lblmasg.Visible = false;
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlCommand cmd = new SqlCommand("delete from ProblemTypeMaster where ProblemTypeId='" + ViewState["id"] + "'", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        lblmasg.Visible = true;
        lblmasg.Text = "Record deleted successfully";

        GridView2.EditIndex = -1;
        fillgrid();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            panel1.Visible = true;
            btnadd.Visible = false;
            lblmasg.Visible = false;
            btnsubmit.Visible = false;
            btnupdate.Visible = true;
            lbllegend.Text = "Edit Customer Problem Type";
            lbllegend.Visible = true;
            ViewState["id"] = Convert.ToInt32(e.CommandArgument);

            SqlDataAdapter da = new SqlDataAdapter("select * from ProblemTypeMaster where ProblemTypeId='" + ViewState["id"] + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtproblm.Text = dt.Rows[0]["ProblemName"].ToString();
            }
        }
        if (e.CommandName == "Delete")
        {
            ViewState["id"] = Convert.ToInt32(e.CommandArgument);
        }
    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string strwh = "SELECT * FROM ProblemTypeMaster where  ProblemName='" + txtproblm.Text + "' and ProblemTypeId<> '" + ViewState["id"] + "'";
        SqlCommand cmdwh = new SqlCommand(strwh, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);
        if (dtwh.Rows.Count > 0)
        {
            lblmasg.Visible = true;
            lblmasg.Text = "Record already exists";
        }
        else
        {
            string str = "update ProblemTypeMaster Set ProblemName='" + txtproblm.Text.Trim() + "' where ProblemTypeId='" + ViewState["id"] + "'";
            SqlCommand mycmd = new SqlCommand(str, con);



            if (con.State.ToString() != "Open")
            {
                con.Open();
            }

            mycmd.ExecuteNonQuery();
            con.Close();
            lblmasg.Visible = true;
            lblmasg.Text = "Record updated successfully";
            fillgrid();

            panel1.Visible = false;
            btnadd.Visible = true;
            clear();

        }
    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}

