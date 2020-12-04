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

public partial class SupervisorMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillemployee();
            Fillgrid();
            ViewState["sortOrder"] = "";

        }
    }
    protected void fillemployee()
    {
        string str = "select * from EmployeeMaster where  EmployeeMaster.ClientId='" + Session["ClientId"] + "' and EmployeeMaster.Active='1' ORDER BY EmployeeMaster.Name ASC ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlemployee.DataSource = ds;
        ddlemployee.DataTextField = "Name";
        ddlemployee.DataValueField = "Id";
        ddlemployee.DataBind();
    }
    protected void Fillgrid()
    {



        string str = "select SupervisorMaster.*,EmployeeMaster.Id as EmpId,EmployeeMaster.Name as EmpName from SupervisorMaster inner join EmployeeMaster on EmployeeMaster.Id=SupervisorMaster.EmployeeId where  EmployeeMaster.ClientId='" + Session["ClientId"] + "'";

        if (ddlactive.SelectedValue.ToString() == "All")
        { }
        else
        {
            str += " AND SupervisorMaster.Active=" + ddlactive.SelectedValue;
        }



        str += " ORDER BY SupervisorMaster.Id DESC";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {

            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

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
    protected void clearall()
    {
        ddlemployee.SelectedIndex = 0;
        txtsupervisorname.Text = "";
        CheckBox1.Checked = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str1 = "select * from SupervisorMaster where Name='" + txtsupervisorname.Text + "'  and Active='" + CheckBox1.Checked + "'   ";

        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }
        else
        {

            string SubMenuInsert = "Insert Into SupervisorMaster (EmployeeId,Name,Active) values ('" + ddlemployee.SelectedValue + "','" + txtsupervisorname.Text + "','" + CheckBox1.Checked + "')";
            SqlCommand cmd = new SqlCommand(SubMenuInsert, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            clearall();
            Fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
        }

        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        Label4.Text = "";
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {



        string st2 = "Delete from SupervisorMaster where Id='" + ViewState["Did"] + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        con.Open();
        cmd2.ExecuteNonQuery();
        con.Close();
        //GridView1.EditIndex = -1;
        Fillgrid();
        lblmsg.Visible = true;
        lblmsg.Text = "Record deleted succesfully ";
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GridView1.EditIndex = -1;
        //Fillgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView1.EditIndex = e.NewEditIndex;
        //int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);

        //Fillgrid();

        //DropDownList ddlemployeemaster = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlemployeemaster");
        //Label lblemployeeIdMaster = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblemployeeIdMaster");

        //string str = "select * from EmployeeMaster ";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);

        //ddlemployeemaster.DataSource = ds;
        //ddlemployeemaster.DataTextField = "Name";
        //ddlemployeemaster.DataValueField = "Id";
        //ddlemployeemaster.DataBind();

        //ddlemployeemaster.SelectedIndex = ddlemployeemaster.Items.IndexOf(ddlemployeemaster.Items.FindByValue(lblemployeeIdMaster.Text));




    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        //int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);

        //DropDownList ddlemployeemaster = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlemployeemaster");
        //TextBox txtsupervisorname123 = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtsupervisorname123");
        //CheckBox CheckBox12edit = (CheckBox)GridView1.Rows[GridView1.EditIndex].FindControl("CheckBox12edit");


        //string str1 = "select * from SupervisorMaster where Name='" + txtsupervisorname.Text + "'  and Active='" + CheckBox1.Checked + "' and Id<>'" + dk + "'  ";

        //SqlCommand cmd1 = new SqlCommand(str1, con);
        //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);
        //if (dt1.Rows.Count > 0)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record Already Exist";
        //}

        //else
        //{


        //    string sr51 = ("update SupervisorMaster set EmployeeId='" + ddlemployeemaster.SelectedValue + "', Name='" + txtsupervisorname123.Text + "'  , Active='" + CheckBox12edit.Checked + "'  where Id='" + dk + "' ");
        //    SqlCommand cmd801 = new SqlCommand(sr51, con);

        //    con.Open();
        //    cmd801.ExecuteNonQuery();
        //    con.Close();
        //    GridView1.EditIndex = -1;
        //    Fillgrid();
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record Update Successfully";

    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        Button1.Visible = true;
        Button3.Visible = false;
        clearall();
        lblmsg.Visible = false;
        lblmsg.Text = "";
        Label4.Text = "";
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            ViewState["Did"] = Convert.ToInt32(e.CommandArgument);
        }

        if (e.CommandName == "Edit")
        {
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;
            Button3.Visible = true;
            Button1.Visible = false;
            lblmsg.Text = "";
            Label4.Text = "Edit Supervisor";

            int mm = Convert.ToInt32(e.CommandArgument);
            ViewState["id"] = mm;

            string str = "select SupervisorMaster.*,EmployeeMaster.Id as EmpId,EmployeeMaster.Name as EmpName from SupervisorMaster inner join EmployeeMaster on EmployeeMaster.Id=SupervisorMaster.EmployeeId where  EmployeeMaster.ClientId='" + Session["ClientId"] + "' and SupervisorMaster.Id='" + mm + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            txtsupervisorname.Text = ds.Rows[0]["Name"].ToString();

            fillemployee();
            ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(ds.Rows[0]["EmpId"].ToString()));
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
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(100);

            btnprint.Text = "Printable Version";
            btnin.Visible = false;
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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        Fillgrid();
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        Label4.Text = "Add New Supervisor";
        lblmsg.Text = "";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string str1 = "select * from SupervisorMaster where Name='" + txtsupervisorname.Text + "'  and Active='" + CheckBox1.Checked + "' and Id<>'" + ViewState["id"] + "'  ";

        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }

        else
        {


            string sr51 = ("update SupervisorMaster set EmployeeId='" + ddlemployee.SelectedValue + "', Name='" + txtsupervisorname.Text + "'  , Active='" + CheckBox1.Checked + "'  where Id='" + ViewState["id"] + "' ");
            SqlCommand cmd801 = new SqlCommand(sr51, con);

            con.Open();
            cmd801.ExecuteNonQuery();
            con.Close();
         //   GridView1.EditIndex = -1;
            Fillgrid();
            clearall();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";
            Button1.Visible = true;
            Button3.Visible = false;
            addnewpanel.Visible = true;
            pnladdnew.Visible = false;
            Label4.Text = "";
        }

       

    }
    protected void ddlactive_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();

    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        string str = "select EmployeeMaster.*,SupervisorMaster.*,SupervisorMaster.Name as SupervisorName,DesignationMaster.Name as DesignationName,RoleMaster.Role_name from EmployeeMaster  INNER JOIN SupervisorMaster ON SupervisorMaster.Id = EmployeeMaster.SupervisorId INNER JOIN RoleMaster ON EmployeeMaster.RoleId = RoleMaster.Role_id LEFT OUTER JOIN DesignationMaster ON DesignationMaster.Id = EmployeeMaster.DesignationId where EmployeeMaster.ClientId='" + Session["ClientId"].ToString() + "' and SupervisorMaster.Name like '%" + txtsearch.Text + "%'";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {

            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
}
        
    

