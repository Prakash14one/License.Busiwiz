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


public partial class BusinessSubSubCategory : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!IsPostBack)
        {
            fillddlCat();
            fillddlsubcat();
            filtercategory();
            filtersubcategory();
            fillGVSSC();
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        SqlCommand cmdstr = new SqlCommand("select B_SubSubCategory from BusinessSubSubCat where B_SubSubCategory='" + txtSSC.Text + "'", con);
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
        fillGVSSC();
    }

    protected void clear()
    {
        txtSSC.Text = "";
        ddlCat.SelectedValue = "0";
        ddlSCat.SelectedValue = "0";
        cbActive.Checked = false;
        lbllegend.Text = "";
        btnsave.Visible = true;
        Button3.Visible = false;
        btnadddd.Visible = true;
        pnladdd.Visible = false;
    }

    protected void insert()
    {
        SqlCommand cmd = new SqlCommand("Insert Into BusinessSubSubCat(B_SubSubCategory,B_SubCatID,Active) values('" + txtSSC.Text + "','" + ddlSCat.SelectedValue + "','" + cbActive.Checked + "')", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    protected void fillGVSSC()
    {
        string st1 = "";

        lblstat.Text = "All";
        lblcategory.Text = "All";
        lblsubcategory.Text = "All";

        if (ddlcategory.SelectedIndex > 0)
        {
            lblcategory.Text = ddlcategory.SelectedItem.Text;
            st1 += " and BusinessSubCat.B_CatID='" + ddlcategory.SelectedValue + "'";
        }

        if (ddlsubcategory.SelectedIndex > 0)
        {
            lblsubcategory.Text = ddlsubcategory.SelectedItem.Text;
            st1 += " and BusinessSubSubCat.B_SubCatID='" + ddlsubcategory.SelectedValue + "'";
        }

        if (ddlstatus_search.SelectedIndex > 0)
        {
            lblstat.Text = ddlstatus_search.SelectedItem.Text;
            st1 += " and BusinessSubSubCat.Active='" + ddlstatus_search.SelectedValue + "'";
        }

        SqlCommand cmdssc = new SqlCommand("SELECT BusinessCategory.B_Category,BusinessSubSubCat.B_SubSubCatID,case when(BusinessSubSubCat.Active = '1') then 'Active' else 'Inactive' end as Active,BusinessSubSubCat.B_SubSubCategory,BusinessSubCat.B_SubCategory FROM BusinessCategory INNER JOIN BusinessSubCat ON BusinessCategory.B_CatID = BusinessSubCat.B_CatID INNER JOIN BusinessSubSubCat ON BusinessSubCat.B_SubCatID = BusinessSubSubCat.B_SubCatID where BusinessSubCat.Active='1' " + st1 + "", con);
        SqlDataAdapter dassc = new SqlDataAdapter(cmdssc);
        DataTable dtssc = new DataTable();
        dassc.Fill(dtssc);

        if (dtssc.Rows.Count > 0)
        {
            GVSSC.DataSource = dtssc;
            GVSSC.DataBind();
        }
        else
        {
            GVSSC.DataSource = null;
            GVSSC.DataBind();
        }
    }

    protected void fillddlCat()
    {
        ddlCat.Items.Clear();

        string strc = "SELECT B_Category, B_CatID From BusinessCategory where Active='1'";
        SqlCommand cmdddlc = new SqlCommand(strc, con);
        SqlDataAdapter dac = new SqlDataAdapter(cmdddlc);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);

        if (dtc.Rows.Count > 0)
        {
            ddlCat.DataSource = dtc;
            ddlCat.DataTextField = "B_Category";
            ddlCat.DataValueField = "B_CatID";
            ddlCat.DataBind();
        }
        ddlCat.Items.Insert(0, "-Select-");
        ddlCat.Items[0].Value = "0";
    }

    protected void fillddlsubcat()
    {
        ddlSCat.Items.Clear();

        if (ddlCat.SelectedIndex > 0)
        {
            string strsc = "SELECT B_SubCategory, B_SubCatID From BusinessSubCat Where B_CatID=" + ddlCat.SelectedValue + " and Active='1'";
            SqlCommand cmdddlsc = new SqlCommand(strsc, con);
            SqlDataAdapter dasc = new SqlDataAdapter(cmdddlsc);
            DataTable dtsc = new DataTable();
            dasc.Fill(dtsc);

            if (dtsc.Rows.Count > 0)
            {
                ddlSCat.DataSource = dtsc;
                ddlSCat.DataTextField = "B_SubCategory";
                ddlSCat.DataValueField = "B_SubCatID";
                ddlSCat.DataBind();
            }         
        }
        ddlSCat.Items.Insert(0, "-Select-");
        ddlSCat.Items[0].Value = "0";
    }

    protected void ddlCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlsubcat();
    }
    protected void GVSSC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVSSC.PageIndex = e.NewPageIndex;
        fillGVSSC();
    }
    protected void GVSSC_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
    }
    protected void GVSSC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      
    }

    protected void GVSSC_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        lblmsg.Text = "";
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        filtersubcategory();
        fillGVSSC();
    }
    protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGVSSC();
    }
    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGVSSC();
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {
        string te = "BusinessSubCategory.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        fillddlsubcat();
    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        lbllegend.Text = "Add New Business Sub Sub Category";
        lblmsg.Text = "";
        pnladdd.Visible = true;
        btnadddd.Visible = false;
    }
    protected void GVSSC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            btnsave.Visible = false;
            Button3.Visible = true;
            pnladdd.Visible = true;
            btnadddd.Visible = false;
            lbllegend.Text = "Edit Business Sub Sub Category";
            lblmsg.Text = "";

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["ID"] = mm;

            SqlDataAdapter da = new SqlDataAdapter("select BusinessSubSubCat.*,BusinessSubCat.B_CatID from BusinessSubSubCat inner join BusinessSubCat on BusinessSubCat.B_SubCatID=BusinessSubSubCat.B_SubCatID where B_SubSubCatID='" + mm + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            fillddlCat();
            ddlCat.SelectedIndex = ddlCat.Items.IndexOf(ddlCat.Items.FindByValue(dt.Rows[0]["B_CatID"].ToString()));

            fillddlsubcat();
            ddlSCat.SelectedIndex = ddlSCat.Items.IndexOf(ddlSCat.Items.FindByValue(dt.Rows[0]["B_SubCatID"].ToString()));

            txtSSC.Text = dt.Rows[0]["B_SubSubCategory"].ToString();

            cbActive.Checked = Convert.ToBoolean(dt.Rows[0]["Active"].ToString());
        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            string str1 = "Delete From BusinessSubSubCat  where B_SubSubCatID=" + mm1 + " ";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully.";

            fillGVSSC();
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string str = "update BusinessSubSubCat set B_SubSubCategory='" + txtSSC.Text + "',Active='" + cbActive.Checked + "',B_SubCatID='" + ddlSCat.SelectedValue + "' where B_SubSubCatID='" + ViewState["ID"] + "'";

        SqlCommand cmd1 = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd1.ExecuteNonQuery();
        con.Close();

        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully.";

        fillGVSSC();

        clear();
    }

    protected void filtercategory()
    {
        ddlcategory.Items.Clear();

        string strc = "SELECT B_Category, B_CatID From BusinessCategory where Active='1'";
        SqlCommand cmdddlc = new SqlCommand(strc, con);
        SqlDataAdapter dac = new SqlDataAdapter(cmdddlc);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);

        if (dtc.Rows.Count > 0)
        {
            ddlcategory.DataSource = dtc;
            ddlcategory.DataTextField = "B_Category";
            ddlcategory.DataValueField = "B_CatID";
            ddlcategory.DataBind();
        }
        ddlcategory.Items.Insert(0, "All");
        ddlcategory.Items[0].Value = "0";
    }

    protected void filtersubcategory()
    {
        ddlsubcategory.Items.Clear();

        if (ddlcategory.SelectedIndex > 0)
        {
            string strsc = "SELECT B_SubCategory, B_SubCatID From BusinessSubCat Where B_CatID=" + ddlcategory.SelectedValue + " and Active='1'";
            SqlCommand cmdddlsc = new SqlCommand(strsc, con);
            SqlDataAdapter dasc = new SqlDataAdapter(cmdddlsc);
            DataTable dtsc = new DataTable();
            dasc.Fill(dtsc);

            if (dtsc.Rows.Count > 0)
            {
                ddlsubcategory.DataSource = dtsc;
                ddlsubcategory.DataTextField = "B_SubCategory";
                ddlsubcategory.DataValueField = "B_SubCatID";
                ddlsubcategory.DataBind();
            }
        }
        ddlsubcategory.Items.Insert(0, "All");
        ddlsubcategory.Items[0].Value = "0";
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;

            if (GVSSC.Columns[5].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GVSSC.Columns[5].Visible = false;
            }
            if (GVSSC.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GVSSC.Columns[4].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GVSSC.Columns[5].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GVSSC.Columns[4].Visible = true;
            }
        }
    }
}
