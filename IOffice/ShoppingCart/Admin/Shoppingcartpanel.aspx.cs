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

public partial class ShoppingCart_Admin_Default : System.Web.UI.Page
{
    SqlConnection con;
    CompanyWizard clsCompany = new CompanyWizard();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            LoadData();
        }
    }
    protected void LoadData()
    {
        DataTable dt = new DataTable();
        dt = clsCompany.SelectCompanyInfo(Session["Comid"].ToString());
        if (dt.Rows.Count > 0)
        {
           
            ViewState["comid"] = dt.Rows[0]["CompanyId"].ToString();                                  
            txtPaypalId.Text = dt.Rows[0]["PaypalEmailId"].ToString();
            txtwebsite1.Text = dt.Rows[0]["WebSite"].ToString();
            txtwebsite12.Text = dt.Rows[0]["WebSite"].ToString();
            txtwebsite13.Text = dt.Rows[0]["WebSite"].ToString();
            if (dt.Rows[0]["WebSite"].ToString() != "" && dt.Rows[0]["WebSite"].ToString() != null)
            {
                pnlwebsitelabel.Visible = true;
                pnlwebsiteadd.Visible = false;
            }
            else
            {
                pnlwebsitelabel.Visible = true;
                pnlwebsiteadd.Visible = false;
            }
        }
        lblwebsite.Text = "http://www.eplaza.us/ShoppingCart" + " /default.aspx?Cid=" + Session["comid"];
        //lbldomain.Text = txtwebsite1.Text;
        lblcid.Text = Session["comid"].ToString();
        lblcid1.Text = Session["comid"].ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            pnladd.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
        }
    }
}
