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

public partial class ShoppingCart_Admin_Master_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        if (!IsPostBack)
        {
            business();
            datefill();
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string te = "AccountYearMasterInfo.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string te = "AccountYearChange.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        string te = "AccountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string te = "Opening_Balance.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void business()
    {
        string business = "select WareHouseMaster.Name,WareHouseMaster.WareHouseID from WareHouseMaster inner join employeemaster on employeemaster.whid=WareHouseMaster.WarehouseId where employeemaster.employeemasterid='" + Session["EmployeeId"] + "'";
        SqlDataAdapter da = new SqlDataAdapter(business, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {           
            ViewState["wwe"] = dt.Rows[0]["WareHouseID"].ToString();
        }
    }
    protected void datefill()
    {       
        string openingdate = "select StartDate,EndDate from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + ViewState["wwe"].ToString() + "' and Active='1'";
        SqlCommand cmd22221 = new SqlCommand(openingdate, con);
        SqlDataAdapter adp22221 = new SqlDataAdapter(cmd22221);
        DataTable ds112221 = new DataTable();
        adp22221.Fill(ds112221);

        if (ds112221.Rows.Count > 0)
        {
            DateTime t1;
            DateTime t2;

            t1 = Convert.ToDateTime(ds112221.Rows[0]["StartDate"].ToString());
            t2 = Convert.ToDateTime(ds112221.Rows[0]["EndDate"].ToString());

            ViewState["CurrentYearStartdate"] = t1.ToShortDateString();
            ViewState["CurrentYearEnddate"] = t2.ToShortDateString();

            Label1.Text = ViewState["CurrentYearStartdate"].ToString() + "  -  " + ViewState["CurrentYearEnddate"].ToString();
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            string te = "Accountingyearmasterinfo.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            string te = "AccountYearChange.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3.Checked == true)
        {
            string te = "Opening_Balance.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox4.Checked == true)
        {
            string te = "GroupMaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
}
