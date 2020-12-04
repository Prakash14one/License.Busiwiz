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

        }
    }
    //protected void LinkButton2_Click(object sender, EventArgs e)
    //{
    //    string te = "Attendencerule.aspx";
    //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //}
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    string te = "DoorIPBaseMaster.aspx";
    //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //}
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            string te = "AttendenceRule.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            string te = "dooripbasemaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
}
