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

public partial class BusinessCategory : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!IsPostBack)
        {
            fillGVC();
        }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        SqlCommand cmdstr = new SqlCommand("select B_Category from BusinessCategory where B_Category='" + txtCname.Text + "'", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmdstr);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lblmsg.Text = "Record already exist.";
        }
        else
        {
            insert();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully.";
            clear();
        }
        fillGVC();
    }

    protected void clear()
    {
        txtCname.Text = "";
        cbActive.Checked = false;
        btnadddd.Visible = true;
        pnladdd.Visible = false;
        lbllegend.Text = "";
        btnsave.Visible = true;
        Button3.Visible = false;
    }

    protected void insert()
    {
        SqlCommand cmd = new SqlCommand("Insert Into BusinessCategory(B_Category,Active) values('" + txtCname.Text + "','" + cbActive.Checked + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void fillGVC()
    {
        string st1 = "";

        lblstat.Text = "All";

        if (ddlstatus_search.SelectedIndex > 0)
        {
            lblstat.Text = ddlstatus_search.SelectedItem.Text;

            st1 += " where BusinessCategory.Active='" + ddlstatus_search.SelectedValue + "'";
        }

        SqlCommand cmdc = new SqlCommand("SELECT B_CatID,B_Category,case when(Active = '1') then 'Active' else 'Inactive' end as Active From BusinessCategory " + st1 + "", con);
        SqlDataAdapter dac = new SqlDataAdapter(cmdc);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);

        if (dtc.Rows.Count > 0)
        {
            GVC.DataSource = dtc;
            GVC.DataBind();
        }
        else
        {
            GVC.DataSource = null;
            GVC.DataBind();
        }

    }
    //protected void GVC_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    int id = Convert.ToInt32( GVC.SelectedDataKey.Value);
    //    TextBox txt = new TextBox();
    //    txt =(TextBox)GVC.Rows[e.RowIndex].Cells[1].Controls[0];
    //    string str = "UPDATE    BusinessCategory SET B_Category ='" + txt.Text + "' where B_CatID=" + id + "";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    con.Open();
    //    cmd.ExecuteNonQuery();
    //    con.Close();


    //}
    //protected void GVC_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    GVC.EditIndex = e.NewEditIndex;
    //}


    protected void GVC_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GVC.EditIndex = e.NewEditIndex;
        //int id = Convert.ToInt32(GVC.DataKeys[e.NewEditIndex].Value);
        //fillGVC();
        //TextBox txt = new TextBox();
        //txt.Text = txtCname.Text;
    }

    protected void GVC_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int id = Convert.ToInt32(GVC.DataKeys[e.RowIndex].Value);
        //TextBox txt = new TextBox();
        ////txt = (TextBox)GVC.Rows[e.RowIndex].Cells[1].Controls[0];
        //txt = (TextBox)GVC.Rows[e.RowIndex].Cells[0].Controls[0];
        //string str = "UPDATE BusinessCategory SET B_Category='" + txt.Text + "' where B_CatID=" + id + "";
        //SqlCommand cmd = new SqlCommand(str, con);
        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();
        //GVC.EditIndex = -1;
        //fillGVC();
    }
    protected void GVC_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GVC.EditIndex = -1;
        //fillGVC();
    }
    protected void GVC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GVC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVC.PageIndex = e.NewPageIndex;
        fillGVC();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        lblmsg.Text = "";

    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = true;
        btnadddd.Visible = false;
        lbllegend.Text = "Add New Business Category";
        lblmsg.Text = "";
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;

            if (GVC.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GVC.Columns[3].Visible = false;
            }
            if (GVC.Columns[2].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GVC.Columns[2].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GVC.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GVC.Columns[2].Visible = true;
            }
        }
    }

    protected void GVC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            btnsave.Visible = false;
            Button3.Visible = true;
            pnladdd.Visible = true;
            btnadddd.Visible = false;
            lbllegend.Text = "Edit Business Category";
            lblmsg.Text = "";

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["ID"] = mm;

            SqlDataAdapter da = new SqlDataAdapter("select * from BusinessCategory where B_CatID='" + mm + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            txtCname.Text = dt.Rows[0]["B_Category"].ToString();
            cbActive.Checked = Convert.ToBoolean(dt.Rows[0]["Active"].ToString());
        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            SqlDataAdapter daf = new SqlDataAdapter("select * from BusinessSubCat where B_CatID='" + mm1 + "'",con);
            DataTable dtf = new DataTable();
            daf.Fill(dtf);

            if (dtf.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, you are not able to delete this record as child record exist using this record.";
            }
            else
            {
                string str1 = "Delete  From BusinessCategory Where B_CatID= " + mm1 + " ";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();

                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully.";
            }
            fillGVC();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string str = "update BusinessCategory set B_Category='" + txtCname.Text + "',Active='" + cbActive.Checked + "' where B_CatID='" + ViewState["ID"] + "'";

        SqlCommand cmd1 = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd1.ExecuteNonQuery();
        con.Close();

        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully.";
        fillGVC();
        clear();
    }

    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGVC();
    }
}
