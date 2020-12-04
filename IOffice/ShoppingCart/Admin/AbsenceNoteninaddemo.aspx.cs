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
        Response.Write("<script language='javascript'>alert('Ninad testing.');</script>");

        if (!IsPostBack)
        {
            txtstartdate.Text = DateTime.Now.ToShortDateString();

            fillstore();
            fillemployee();
        }

    }

    protected void fillstore()
    {
        ddlbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlbusiness.DataSource = ds;
        ddlbusiness.DataTextField = "Name";
        ddlbusiness.DataValueField = "WareHouseId";
        ddlbusiness.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string st11 = "select * from AttendenceEntryMaster where Date='" + txtstartdate.Text + "' and EmployeeID='" + ddlemployee.SelectedValue + "'";

        SqlDataAdapter da = new SqlDataAdapter(st11, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You can not add Absence Note for this Employee as Attendance for this Employee is already Done.";
        }
        else
        {
            SqlCommand cmdstr = new SqlCommand("insert into AbsenseNote ([whid],[empid],[date],[reason],[Note]) values('" + ddlbusiness.SelectedValue + "','" + ddlemployee.SelectedValue + "','" + txtstartdate.Text + "','" + ddlreason.SelectedItem.Text + "','" + txtdescription.Text + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdstr.ExecuteNonQuery();
            con.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully.";
            clear();
        }
    }

    protected void clear()
    {
        fillstore();
        fillemployee();
        ddlemployee.SelectedIndex = 0;
        ddlreason.SelectedIndex = 0;
        txtdescription.Text = "";
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        lblmsg.Text = "";
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployee();
    }

    protected void fillemployee()
    {
        ddlemployee.Items.Clear();

        string str = "select employeemasterid,employeename from employeemaster where whid='" + ddlbusiness.SelectedValue + "' and Active='1' order by employeename";
        DataTable table = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        adp.Fill(table);

        if (table.Rows.Count > 0)
        {
            ddlemployee.DataSource = table;
            ddlemployee.DataTextField = "employeename";
            ddlemployee.DataValueField = "employeemasterid";
            ddlemployee.DataBind();
        }
    }
}
