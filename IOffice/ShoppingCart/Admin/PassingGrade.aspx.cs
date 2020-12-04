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

public partial class ShoppingCart_Admin_Default : System.Web.UI.Page
{

    SqlConnection con;
    SqlCommand cmd;
    DataTable dt;
    DataSet ds;
    SqlDataAdapter da;


    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
            }


            ViewState["sortOrder"] = "";

            txtname.Focus();
            fillgrid();
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        clspassinggrade objpass = new clspassinggrade();
        objpass.Name = txtname.Text;
        objpass.EquivallentGPA = txtequiGpa.Text;
        objpass.Active = Convert.ToInt32(chkstatus.Checked);
        objpass.executenoninsert_pass();


        //cmd = new SqlCommand("insert into PassingGrade(Name,EquivallentGPA,Active) values('"+txtname.Text+"' , '"+txtequiGpa.Text+"' , '"+chkstatus.Checked+"')",con);
        //if (cmd.Connection.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmd.ExecuteNonQuery();
        //con.Close();
        fillgrid();
        clear();

        lblmessage.Text = "Record inserted successfully.";
        lblmessage.Visible = true;
        Panel1.Visible = false;
        Button1.Visible = true;
        lbllegend.Text = "";
    }

    protected void fillgrid()
    {
        string str = "select PassingGrade.ID,PassingGrade.Name,PassingGrade.EquivallentGPA,case when(PassingGrade.Active = '1') then 'Active' else 'Inactive' end as Status from PassingGrade ";

        if (ddlstatus_search.SelectedIndex > 0)
        {
            str += " where PassingGrade.Active='" + ddlstatus_search.SelectedValue + "' ";
        }

        da = new SqlDataAdapter(str, con);
        dt = new DataTable();        
        da.Fill(dt);

        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataSource = myDataView;
        GridView1.DataBind();
    }
    protected void clear()
    {
        txtname.Text = "";
        txtequiGpa.Text = "";
        chkstatus.Checked = false;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Visible == true)
        {
            clear();
        }
        if (btnupdate.Visible == true)
        {

            btnupdate.Visible = false;
            btnsubmit.Visible = true;
            clear();

        }
        lblmessage.Visible = false;
        Panel1.Visible = false;
        Button1.Visible = true;
        lbllegend.Text = "";
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        clspassinggrade objpass = new clspassinggrade();

        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "Edit Passing Grade";

            Panel1.Visible = true;
            Button1.Visible = false;
            int i = Convert.ToInt32(e.CommandArgument);
            Label9.Text = i.ToString();

            objpass.ID = i;

            //    da = new SqlDataAdapter("select * from PassingGrade where ID="+i,con);
            dt = new DataTable();
            dt = objpass.filgrid_pas2();
            //    da.Fill(dt);

            txtname.Text = dt.Rows[0]["Name"].ToString();
            txtequiGpa.Text = dt.Rows[0]["EquivallentGPA"].ToString();
            chkstatus.Checked = Convert.ToBoolean(dt.Rows[0]["Active"]);

            txtname.Focus();
        }
        if (e.CommandName == "Delete")
        {
            int i = Convert.ToInt32(e.CommandArgument);

            cmd = new SqlCommand("delete from PassingGrade where ID=" + i, con);
            if (cmd.Connection.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            fillgrid();
            lblmessage.Visible = true;
            lblmessage.Text = "Record deleted successfully.";
        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        btnsubmit.Visible = false;
        btnupdate.Visible = true;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        clspassinggrade objpass = new clspassinggrade();
        objpass.ID = Convert.ToInt32(Label9.Text);
        objpass.Name = txtname.Text;
        objpass.EquivallentGPA = txtequiGpa.Text;
        objpass.Active = Convert.ToInt32(chkstatus.Checked);
        objpass.executenonupdate_pass();

        //cmd = new SqlCommand("update PassingGrade set Name='"+txtname.Text+"' , EquivallentGPA='"+txtequiGpa.Text+"' , Active='"+chkstatus.Checked+"' where ID='"+Label9.Text+"' " ,con);
        //if (cmd.Connection.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmd.ExecuteNonQuery();
        //con.Close();
        fillgrid();
        clear();

        btnupdate.Visible = false;
        btnsubmit.Visible = true;

        lblmessage.Text = "Record updated successfully.";
        lblmessage.Visible = true;
        lbllegend.Text = "";
        Panel1.Visible = false;
        Button1.Visible = true;
    }


    protected void btnPrintVersion_Click(object sender, EventArgs e)
    {
        if (btnPrintVersion.Text == "Printable Version")
        {
            btnPrintVersion.Text = "Hide Printable Version";
            btnPrint.Visible = true;
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
            btnPrintVersion.Text = "Printable Version";
            btnPrint.Visible = false;
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

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Button1.Visible = false;
        lblmessage.Visible = false;
        lbllegend.Text = "Add New Passing Grade";
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
