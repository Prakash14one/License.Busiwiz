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
using System.Globalization;

public partial class ShoppingCart_Admin_AddressTypeMaster : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        Label1.Visible = false;

        if (!IsPostBack)
        {
            filldata();
        }
    }

    public void filldata()
    {
        string sg90 = "select * from AddressType order by AddressType";
        SqlCommand cmd23490 = new SqlCommand(sg90, con);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();

        adp23490.Fill(dt23490);

        GridView1.DataSource = dt23490;
        GridView1.DataBind();

    }

    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
     
    }
    

    protected void ImageButton8_Click(object sender, EventArgs e)
    {
        string sg90 = "select AddressType from AddressType where AddressType='" + Textname.Text + "'";
        SqlCommand cmd23490 = new SqlCommand(sg90, con);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();
        adp23490.Fill(dt23490);

        if (dt23490.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exist.";
            Textname.Text = "";
        }
        else
        {
            SqlCommand mycmd = new SqlCommand("insert into AddressType values('" + Textname.Text + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            mycmd.ExecuteNonQuery();
            con.Close();

            Label1.Visible = true;
            Label1.Text = "Record inserted successfully.";

            filldata();

            Textname.Text = "";
            pnladd.Visible = false;
            btnadd.Visible = true;
            lbladd.Text = "";
        }
    }

    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        Textname.Text = "";
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        filldata();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button1.Visible = true;
            if (GridView1.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[2].Visible = false;
            }
            if (GridView1.Columns[1].Visible == true)
            {
                ViewState["delHide"] = "tt";
                GridView1.Columns[1].Visible = false;
            }
        }
        else
        {
            Button4.Text = "Printable Version";
            Button1.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[2].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                GridView1.Columns[1].Visible = true;
            }
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            btnadd.Visible = false;
            lbladd.Text = "Add New Address Type";
        }
        else
        {
            pnladd.Visible = false;
            btnadd.Visible = true;
            lbladd.Text = "";
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string sg90 = "select AddressType from AddressType where AddressType='" + Textname.Text + "'  and AddressTypeID <> '" + ViewState["IDs"] + "' ";
        SqlCommand cmd23490 = new SqlCommand(sg90, con);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();
        adp23490.Fill(dt23490);

        if (dt23490.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exist.";
            Textname.Text = "";
        }
        else
        {
            string str = "update  AddressType set AddressType='" + Textname.Text + "' where AddressTypeID='" + ViewState["IDs"] + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            Label1.Visible = true;
            Label1.Text = "Record updated successfully.";

            filldata();

            Textname.Text = "";
            btnupdate.Visible = false;
            btnsubmit.Visible = true;
            pnladd.Visible = false;
            btnadd.Visible = true;
            lbladd.Text = "";
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            pnladd.Visible = true;
            btnadd.Visible = false;
            lbladd.Text = "Edit Address Type";
            btnsubmit.Visible = false;
            btnupdate.Visible = true;

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["IDs"] = mm;

            SqlDataAdapter da = new SqlDataAdapter("select * from AddressType where AddressTypeID=" + mm, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Textname.Text = dt.Rows[0]["AddressType"].ToString();
        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            string str = "delete from AddressType where AddressTypeID='" + mm1 + "' ";
            DataSet ds = new DataSet();
            SqlCommand cmdd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdd.ExecuteNonQuery();
            con.Close();
            
            filldata();
            
            Label1.Visible = true;
            Label1.Text = "Record deleted successfully.";
        }
    }
}
