using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class ShoppingCart_Admin_UserControl_UcontrolHelpPanel : System.Web.UI.UserControl
{
    MasterCls clsMaster = new MasterCls();
    DataTable dt;
     SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {

        dt = new DataTable();
        if (Session["PageName"] != null)
        {
            string pagename = Session["PageName"].ToString();
            dt = clsMaster.SelectPageMasterbyPageNameForShoppingcart(pagename);
            if (dt.Rows.Count > 0)
            {
                lblDetail.Text = dt.Rows[0]["PageDescription"].ToString();
                lbltitle.Text = dt.Rows[0]["PageTitle"].ToString();
                pnlhelp.Visible = true;
                PNLTITLE.Visible = true;

            }
            else
            {
                lbltitle.Text = "";
                lblDetail.Text = "";
                PNLTITLE.Visible = false;
                pnlhelp.Visible = false;
            }
        }
        else
        {
            lblDetail.Text = "";
           pnlhelp.Visible = false;
        }
        if (!IsPostBack)
        {
            //fillwarehouse();
            ddlbus_SelectedIndexChanged(sender, e);
        }
    }
    protected DataTable select(string str)
    { PageConn pgcon = new PageConn();
                  con = pgcon.dynconn; 
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void ddlbus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtda = (DataTable)select("select Distinct  ReportPeriod.Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Compid = '" + Session["comid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "' and Active='1' ");
        if (dtda.Rows.Count > 0)
        {
            lblopenaccy.Text = Convert.ToString(dtda.Rows[0]["StartDate"]) + "-" + Convert.ToString(dtda.Rows[0]["EndDate"]);
            lblop.NavigateUrl = "~/ShoppingCart/Admin/AccountYearChange.aspx?Whid=" + ddlwarehouse.SelectedValue + "";
        }
    }
    //protected void ImageButton5_Click(object sender, EventArgs e)
    //{


    //    string te = "AccountYearChange.aspx?Whid=" + ddlwarehouse.SelectedValue;


       
    //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    //}
    protected void fillwarehouse()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();


            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
        }

    }
}
