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
            timezone();
        }
    }

    protected void business()
    {
        string business = "select WareHouseMaster.Name,WareHouseMaster.WareHouseID from WareHouseMaster inner join employeemaster on employeemaster.whid=WareHouseMaster.WarehouseId where employeemaster.employeemasterid='" + Session["EmployeeId"] + "'";
        SqlDataAdapter da = new SqlDataAdapter(business, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            lblbusiness.Text = dt.Rows[0]["Name"].ToString();
            lblbusiness1.Text = dt.Rows[0]["Name"].ToString();
            ViewState["wwe"] = dt.Rows[0]["WareHouseID"].ToString();
        }
    }

    protected void timezone()
    {
        string tam = "select WHTimeZone.ID,WHTimeZone.WHID,WareHouseMaster.Name,TimeZoneMaster.Name +':'+ TimeZoneMaster.ShortName+':'+ TimeZoneMaster.gmt as tname from WHTimeZone inner join WareHouseMaster on WareHouseMaster.WareHouseId=WHTimeZone.WHID inner join TimeZoneMaster on TimeZoneMaster.ID=WHTimeZone.TimeZone where [WareHouseMaster].Status='1' and compid='" + Session["Comid"].ToString() + "' and WHTimeZone.WHID='" + ViewState["wwe"].ToString() + "' order by Name, tname";

        SqlDataAdapter daf = new SqlDataAdapter(tam, con);
        DataTable dtf = new DataTable();
        daf.Fill(dtf);

        if (dtf.Rows.Count > 0)
        {
            Label17.Text = dtf.Rows[0]["tname"].ToString();
        }
    }

    //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (CheckBox1.Checked == true)
    //    {
    //        string te = "Wizardcompanywebsitmaster.aspx";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //    }
    //}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string te = "Departmentaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string te = "DesignationAddManage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    //protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (CheckBox2.Checked == true)
    //    {
    //        string te = "wzWareHouseTimeZoneAddMaster.aspx";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //    }
    //}
    //protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (CheckBox3.Checked == true)
    //    {
    //        string te = "Departmentaddmanage.aspx";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //    }
    //}
    //protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (CheckBox4.Checked == true)
    //    {
    //        string te = "Designationaddmanage.aspx";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //    }
    //}
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        string te = "WizardCompanyWebsitMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string te = "Departmentaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        string te = "DesignationAddManage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        string te = "wzWareHouseTimeZoneAddMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        string te = "BatchMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        string te = "BatchMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        string te = "BatchworkingDay.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        string te = "BatchworkingDay.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        string te = "Rolemaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        string te = "EmployeeMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        string te = "companyholiday.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        string te = "EmployeeLeaveType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            string te = "EmployeeLeaveType.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            string te = "companyholiday.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3.Checked == true)
        {
            string te = "PartyMaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox7_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox7.Checked == true)
        {
            string te = "wzWareHouseTimeZoneAddMaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox6_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox6.Checked == true)
        {
            string te = "BatchMaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox5.Checked == true)
        {
            string te = "BatchworkingDay.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox4.Checked == true)
        {
            string te = "Rolemaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox33_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox33.Checked == true)
        {
            string te = "EmployeeMaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox123_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox123.Checked == true)
        {
            string te = "WizardCompanyWebsitMaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox3a_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3a.Checked == true)
        {
            string te = "Departmentaddmanage.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox8_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox8.Checked == true)
        {
            string te = "DesignationAddManage.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
}
