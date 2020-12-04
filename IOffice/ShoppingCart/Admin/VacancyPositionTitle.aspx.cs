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
using System.Data;
using System.Data.SqlClient;

public partial class ShoppingCart_Admin_Master_Default : System.Web.UI.Page
{
    SqlConnection con;
    string compid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        pagetitleclass pg = new pagetitleclass();


        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        compid = Session["Comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);

            fillvacancy();
            fillgrid();
        }
    }

    protected void fillvacancy()
    {
        DropDownList1.Items.Clear();

        SqlDataAdapter dav = new SqlDataAdapter("select ID,Name from VacancyTypeMaster", con);
        DataTable dtv = new DataTable();
        dav.Fill(dtv);

        if (dtv.Rows.Count > 0)
        {
            DropDownList1.DataSource = dtv;
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataBind();
        }
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("insert into VacancyPositionTitleMaster values('" + DropDownList1.SelectedValue + "','" + txtvacancytype.Text + "','" + ddlstatus.SelectedValue + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        fillgrid();
        clear();
        statuslable.Text = "Record inserted successfully";
    }

    protected void fillgrid()
    {
        SqlDataAdapter da = new SqlDataAdapter("select VacancyPositionTitleMaster.*,case when (VacancyPositionTitleMaster.Active='1') then 'Active' else 'Inactive' End as Statuslabel,VacancyTypeMaster.Name from VacancyPositionTitleMaster inner join VacancyTypeMaster on VacancyTypeMaster.ID=VacancyPositionTitleMaster.VacancyPositionTypeID", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "Edit Vacancy Title";

            Pnladdnew.Visible = true;
            btnadd.Visible = false;

            statuslable.Text = "";
            btnsubmit.Visible = false;
            btnupdate.Visible = true;

            int mm = Convert.ToInt32(e.CommandArgument);
            ViewState["updateid"] = mm;

            SqlDataAdapter da1 = new SqlDataAdapter("select * from VacancyPositionTitleMaster where ID=" + mm, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                fillvacancy();
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dt1.Rows[0]["VacancyPositionTypeID"].ToString()));

                txtvacancytype.Text = dt1.Rows[0]["VacancyPositionTitle"].ToString();
               // CheckBox1.Checked = Convert.ToBoolean(dt1.Rows[0]["Active"].ToString());
                if (Convert.ToBoolean(dt1.Rows[0]["Active"].ToString()) == true)
                {
                    ddlstatus.SelectedValue = "1";
                }
                else
                {
                    ddlstatus.SelectedValue = "0";
                }
            }
        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            SqlCommand cmd1 = new SqlCommand("delete from VacancyPositionTitleMaster where ID=" + mm1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();

            fillgrid();

            statuslable.Text = "Record deleted successfully";
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        SqlCommand cmd1 = new SqlCommand("update VacancyPositionTitleMaster set VacancyPositionTypeID='" + DropDownList1.SelectedValue + "',VacancyPositionTitle='" + txtvacancytype.Text + "',Active='" + ddlstatus.SelectedValue + "' where ID='" + ViewState["updateid"].ToString() + "'", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd1.ExecuteNonQuery();
        con.Close();

        fillgrid();
        clear();
        statuslable.Text = "Record updated successfully";

        btnupdate.Visible = false;
        btnsubmit.Visible = true;
    }

    protected void clear()
    {
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        txtvacancytype.Text = "";
        //CheckBox1.Checked = false;
        ddlstatus.SelectedIndex = 0;
        lbllegend.Text = "";
        DropDownList1.SelectedIndex = 0;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        statuslable.Text = "";
        lbllegend.Text = "";
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Pnladdnew.Visible = true;
        btnadd.Visible = false;
        statuslable.Text = "";
        lbllegend.Text = "Add Vacancy Title";
    }

    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["Docs"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[3].Visible = false;
            }

        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["Docs"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["edith"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }

        }
    }
}
