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

public partial class Add_Role_Master : System.Web.UI.Page
{
    PageMgmt obj = new PageMgmt();
    string compid;
    SqlConnection con = new SqlConnection(PageConn.connnn);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        compid = Session["Comid"].ToString();
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        Label2.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            lblCompany.Text = Session["Cname"].ToString();
            fillgrid();

        }
        Label2.Text = "";

    }
    protected void butSubmit_Click(object sender, EventArgs e)
    {
        int Active;

        if (butSubmit.Text == "Submit")
        {
            if (ddlstatus.SelectedItem.Text == "Active")
            {
                Active = 1;
            }
            else
            {
                Active = 0;
            }
            string sr123 = "Select * from [RoleMaster] where Role_name='" + txtRole.Text + "' and compid='" + Session["comid"] + "' ";
            SqlCommand cm123 = new SqlCommand(sr123, con);
            cm123.CommandType = CommandType.Text;
            SqlDataAdapter da123 = new SqlDataAdapter(cm123);
            DataTable ds123 = new DataTable();
            da123.Fill(ds123);
            if (ds123.Rows.Count > 0)
            {
                Label2.Visible = true;
                Label2.Text = "Record already exists";
            }
            else
            {
                obj.insertrolemaster(txtRole.Text, Active, compid);
                Label2.Visible = true;
                Label2.Text = "Record inserted successfully";
                pnladd.Visible = false;
                btnadd.Visible = true;
                lbladd.Text = "";
                SqlDataAdapter adpt = new SqlDataAdapter("Select Max(Role_id) as roleid from RoleMaster", con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                if (CheckBox1.Checked == true)
                {
                    string te = "Page_role_Access.aspx?id=" + dt.Rows[0]["roleid"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }
            }
        }
        else
        {
            if (ddlstatus.SelectedItem.Text == "Active")
            {
                Active = 1;
            }
            else
            {
                Active = 0;
            }
            string sr123 = "Select * from [RoleMaster] where Role_name='" + txtRole.Text + "' and compid='" + Session["comid"] + "' and Role_id<>'" + Int16.Parse(lblrole_id.Text) + "'";
            SqlCommand cm123 = new SqlCommand(sr123, con);
            cm123.CommandType = CommandType.Text;
            SqlDataAdapter da123 = new SqlDataAdapter(cm123);
            DataTable ds123 = new DataTable();
            da123.Fill(ds123);
            if (ds123.Rows.Count > 0)
            {
                Label2.Visible = true;
                Label2.Text = "Record already exists";
            }
            else
            {

                obj.updateRolemaster(Int16.Parse(lblrole_id.Text), txtRole.Text, Active);
                Label2.Visible = true;
                Label2.Text = "Record updated successfully";
                butSubmit.Text = "Submit";
                pnladd.Visible = false;
                btnadd.Visible = true;
                lbladd.Text = "";
               
                CheckBox1.Visible = true;
            }
        }




        txtRole.Text = "";
        //raActive.Checked = true;
        //raDeactive.Checked = false;
        ddlstatus.SelectedIndex = 0;
        fillgrid();


    }
    protected void butCancel_Click(object sender, EventArgs e)
    {

        txtRole.Text = "";
        ddlstatus.SelectedIndex = 0;
        //raActive.Checked = true;
        //raDeactive.Checked = false;
        butSubmit.Text = "Submit";
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";
        CheckBox1.Visible = true;
    }
    protected void fillgrid()
    {
        DataTable dt = new DataTable();
        dt = obj.selectGriddata_role(compid);

        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataSource = myDataView;
        GridView1.DataBind();
        
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label2.Text = "";
        if (e.CommandName == "Edit")
        {
            lblrole_id.Text = e.CommandArgument.ToString();
            if ((lblrole_id.Text != "1") && (lblrole_id.Text != "2"))
            {
                SqlDataReader dr = obj.Selectrole_id(Int16.Parse(lblrole_id.Text), compid);
                while (dr.Read())
                {
                    txtRole.Text = dr["Role_name"].ToString();
                    if (dr["ActiveDeactive"].ToString() == "True")
                    {
                       // raActive.Checked = true;
                       // raDeactive.Checked = false;
                        ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue("1"));
                    }
                    else
                    {
                        //raDeactive.Checked = true;
                        //raActive.Checked = false;
                        ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue("0"));
                    }

                }
                butSubmit.Text = "Update";
                pnladd.Visible = true;
                btnadd.Visible = false;
                lbladd.Text = "Edit Website Access Rights Role Name";
                CheckBox1.Visible = false;
            }
            else
            {
                Label2.Text = "Sorry, you cannot edit this record.";
            }
        }
        else if (e.CommandName == "Delete")
        {
            lblrole_id.Text = e.CommandArgument.ToString();
            //ModalPopupExtender1222.Show();
            lblrole_id.Visible = false;
            SqlCommand cmdedit = new SqlCommand("select User_id from User_Role where Role_id='" + Convert.ToString(lblrole_id.Text) + "'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);
            if (dtedit.Rows.Count == 0)
            {
                string str = "delete from RoleMaster where Role_id='" + Convert.ToString(lblrole_id.Text) + "' ";
                DataSet ds = new DataSet();
                SqlCommand cmdd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdd.ExecuteNonQuery();
                con.Close();

                fillgrid();
                GridView1.EditIndex = -1;
                Label2.Visible = true;
                Label2.Text = "Record deleted successfully";
            }
            else
            {
                Label2.Visible = true;
                Label2.Text = "You are unable to delete this record. This role is attached to other users. You must transfer all other users to other roles in order to delete this role. Please go to " + "<a href=\"User_Role_Management.aspx\" style=\"font-size:14px; color:red; \" target=\"_blank\">" + "User Role Management " + "</a>page in order to proceed.";
            }
        }
        else if (e.CommandName == "Manage")
        {
            string te = "Page_role_Access.aspx?id=" + e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
   

    protected void yes_Click(object sender, EventArgs e)
    {
        lblrole_id.Visible = false;
        SqlCommand cmdedit = new SqlCommand("select User_id from User_Role where Role_id='" + Convert.ToString(lblrole_id.Text) + "'", con);
        SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
        DataTable dtedit = new DataTable();
        dtpedit.Fill(dtedit);
        if (dtedit.Rows.Count == 0)
        {
            string str = "delete from RoleMaster where Role_id='" + Convert.ToString(lblrole_id.Text) + "' ";
            DataSet ds = new DataSet();
            SqlCommand cmdd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdd.ExecuteNonQuery();
            con.Close();

            fillgrid();
            GridView1.EditIndex = -1;
            Label2.Visible = true;
            Label2.Text = "Record deleted successfully";
        }
        else
        {
            Label2.Visible = true;
            Label2.Text = "You are not allowed to delete this record, first delete child record.";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();
        ModalPopupExtender1222.Hide();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

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
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Panel6.ScrollBars = ScrollBars.None;
            Panel6.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (GridView1.Columns[2].Visible == true)
            {
                ViewState["manageHide"] = "tt";
                GridView1.Columns[2].Visible = false;
            }
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

            //Panel6.ScrollBars = ScrollBars.Vertical;
            //Panel6.Height = new Unit(250);

            Button1.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["manageHide"] != null)
            {
                GridView1.Columns[2].Visible = true;
            }
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
    protected void btnadd_Click(object sender, EventArgs e)
    {
        lbladd.Text = "Add New Website Access Rights Role Name";
        pnladd.Visible = true;
        btnadd.Visible = false;
        Label2.Text = "";
        CheckBox1.Visible = true;
    }
}