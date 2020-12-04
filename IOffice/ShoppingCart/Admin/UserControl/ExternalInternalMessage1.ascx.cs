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

public partial class Account_UserControl_ExternalInternalMessage1 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    //{
    //    ddlempemail.Visible = true;
    //    fillddl();
    //    Label1.Text = "External Message Center";
    //    //Response.Redirect("~/Account/MessageInboxExt.aspx");
    //    Response.Redirect("~/ShoppingCart/Admin/MessageComposeExt.aspx");
    //}
    protected void fillddl()
    {
        DataTable dtcomemail = new DataTable();
        DataTable dtempemail = new DataTable();
        MasterCls1 clsMaster = new MasterCls1();
        string empeml = "";
        dtempemail = clsMaster.SelectEmpEmail(Convert.ToInt32(Session["EmployeeId"]));
        if (dtempemail.Rows.Count > 0)
        {
            if (dtempemail.Rows[0]["Email"] != System.DBNull.Value)
            {
                empeml = dtempemail.Rows[0]["Email"].ToString();
            }
        }
        dtcomemail = clsMaster.SelectCompanyEmailForEmp(Convert.ToInt32(Session["EmployeeId"]));
        ddlempemail.DataSource = dtcomemail;
        ddlempemail.DataBind();
        ddlempemail.Items.Insert(0, "-Select-");
        ddlempemail.SelectedItem.Value = "0";
        if (empeml != "")
        {
            ddlempemail.Items.Insert(1, empeml);
            ddlempemail.SelectedIndex = 1;
        }

    }
    protected void btnexternal_Click(object sender, EventArgs e)
    {
        ddlempemail.Visible = true;
        fillddl();
        //Response.Redirect("~/Account/MessageInboxExt.aspx");
        Response.Redirect("~/ShoppingCart/Admin/MessageComposeExt.aspx");
    }
}
