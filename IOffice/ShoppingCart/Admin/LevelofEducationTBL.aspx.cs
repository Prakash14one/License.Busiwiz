using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ShoppingCart_Admin_Default : System.Web.UI.Page
{

    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    int key = 0;
    DataSet ds;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        if (!IsPostBack)
        {
            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
            }
            ViewState["sortOrder"] = "";

            filleducation();
            filteredu();
            fillgrid();
        }

    }

    protected DataSet test(string name)
    {
        String s = "select * from LevelofEducationTBL where Name = '" + name + "'";
        da = new SqlDataAdapter(s, con);

        ds = new DataSet();
        da.Fill(ds);
        //CLS_LevelofEducationTBL let = new CLS_LevelofEducationTBL();
        //let.Name = name;
        //ds = let.cls_levelof_edu1();
        return (ds);

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        String s1 = "insert into LevelofEducationTBL(Name,Active,AreaofStudyID) values('" + txtName.Text + "','" + CheckBox1.Checked + "','" + ddlareaofstudy.SelectedValue + "')";
        cmd = new SqlCommand(s1, con);

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();


        lblmsg.Text = "Record inserted successfully";
        lblmsg.Visible = true;

        fillgrid();

        clear();
        Panel4.Visible = false;
        btnaddnew.Visible = true;
        lbllegend.Text = "";
    }

    protected void clear()
    {
        txtName.Text = "";
        CheckBox1.Checked = false;
        ddlareaofstudy.SelectedIndex = 0;
    }

    protected void filleducation()
    {
        cmd = new SqlCommand("select ID,Name from AreaofStudiesTbl where Active='1' order by Name", con);
        da = new SqlDataAdapter(cmd);
        DataTable dt2 = new DataTable();
        da.Fill(dt2);

        ddlareaofstudy.DataSource = dt2;
        ddlareaofstudy.DataTextField = "Name";
        ddlareaofstudy.DataValueField = "ID";
        ddlareaofstudy.DataBind();

        ddlareaofstudy.Items.Insert(0, "-Select-");
        ddlareaofstudy.SelectedItem.Value = "0";
    }

    protected void filteredu()
    {
        cmd = new SqlCommand("select ID,Name from AreaofStudiesTbl where Active='1' order by Name", con);
        da = new SqlDataAdapter(cmd);
        DataTable dt2 = new DataTable();
        da.Fill(dt2);

        ddlareaaaa.DataSource = dt2;
        ddlareaaaa.DataTextField = "Name";
        ddlareaaaa.DataValueField = "ID";
        ddlareaaaa.DataBind();

        ddlareaaaa.Items.Insert(0, "All");
        ddlareaaaa.SelectedItem.Value = "0";
    }

    protected void fillgrid()
    {
        Label3.Text = "All";

        lblstat.Text = "All";

        String s = "select LevelofEducationTBL.Id,LevelofEducationTBL.Name as LName,case when(LevelofEducationTBL.Active = '1') then 'Active' else 'Inactive' end as Status,AreaofStudiesTbl.Name as AName from LevelofEducationTBL inner join AreaofStudiesTbl on AreaofStudiesTbl.Id=LevelofEducationTBL.AreaofStudyID where AreaofStudiesTbl.Active='1'";

        if (ddlareaaaa.SelectedIndex > 0)
        {
            Label3.Text = ddlareaaaa.SelectedItem.Text;

            s += " and LevelofEducationTBL.AreaofStudyID='" + ddlareaaaa.SelectedValue + "'";
        }
        if (ddlstatus_search.SelectedIndex > 0)
        {
            lblstat.Text = ddlstatus_search.SelectedItem.Text;

            s += " and LevelofEducationTBL.Active='" + ddlstatus_search.SelectedValue + "'";
        }
        s += " order by AreaofStudiesTbl.Name,LevelofEducationTBL.Name  asc";
        da = new SqlDataAdapter(s, con);
        ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        Panel4.Visible = false;
        btnaddnew.Visible = true;
        lbllegend.Text = "";
        btnSubmit.Visible = true;
        btnupdate.Visible = false;
        lblmsg.Text = "";
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void btnprintv_Click(object sender, EventArgs e)
    {
        if (btnprintv.Text == "Printable Version")
        {

            btnprintv.Text = "Hide Printable Version";
            btnPrint.Visible = true;

            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[3].Visible = false;
            }
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide1"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
        }
        else
        {

            btnprintv.Text = "Printable Version";
            btnPrint.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }
            if (ViewState["editHide1"] != null)
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
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        Panel4.Visible = true;
        lblmsg.Visible = false;
        btnaddnew.Visible = false;
        lbllegend.Text = "Add New Level of Education";
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "Edit Level of Education";

            Panel4.Visible = true;
            lblmsg.Visible = false;
            lblmsg.Text = "";
            btnaddnew.Visible = false;
            btnupdate.Visible = true;
            btnSubmit.Visible = false;

            int mm = Convert.ToInt32(e.CommandArgument);
            ViewState["id"] = mm;

            SqlDataAdapter da = new SqlDataAdapter("select * from LevelofEducationTBL where Id=" + mm, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            filleducation();
            ddlareaofstudy.SelectedIndex = ddlareaofstudy.Items.IndexOf(ddlareaofstudy.Items.FindByValue(dt.Rows[0]["AreaofStudyID"].ToString()));

            txtName.Text = dt.Rows[0]["Name"].ToString();

            CheckBox1.Checked = Convert.ToBoolean(dt.Rows[0]["Active"].ToString());

        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            string strwh3 = "select * from EducationDegrees where LevelofEducationTblID='" + mm1 + "' ";
            SqlCommand cmdwh3 = new SqlCommand(strwh3, con);
            SqlDataAdapter adpwh3 = new SqlDataAdapter(cmdwh3);
            DataTable dts3 = new DataTable();
            adpwh3.Fill(dts3);

            if (dts3.Rows.Count == 0)
            {
                SqlCommand cmddel = new SqlCommand("delete from LevelofEducationTBL where Id=" + mm1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddel.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully";
                fillgrid();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "sorry,you are not able to delete this record as child record exist using this record.";
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        SqlCommand cmddel1 = new SqlCommand("update LevelofEducationTBL set Name='" + txtName.Text + "', Active='" + CheckBox1.Checked + "',AreaofStudyID='" + ddlareaofstudy.SelectedValue + "' where Id='" + ViewState["id"].ToString() + "'", con);

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmddel1.ExecuteNonQuery();
        con.Close();

        fillgrid();
        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully";

        btnSubmit.Visible = true;
        btnupdate.Visible = false;
        clear();

        btnaddnew.Visible = true;
        Panel4.Visible = false;
        lbllegend.Text = "";
    }
    protected void ddlareaaaa_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
