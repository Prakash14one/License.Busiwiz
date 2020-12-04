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

public partial class Account_UserControl_UcontrolHelpPanel : System.Web.UI.UserControl
{
    MasterCls clsMaster = new MasterCls();
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {

        dt = new DataTable();
        if (Session["PageName"] != null)
        {
            string pagename = Session["PageName"].ToString();
            dt = clsMaster.SelectPageMasterbyPageName(pagename);
            if (dt.Rows.Count > 0)
            {
                lblDetail.Text = dt.Rows[0]["PageDescription"].ToString();
               // pnlhelp.Visible = true;
            }
            else
            {
                if (Session["PageDescri"] != null)
                {
                    lblDetail.Text = Session["PageDescri"].ToString();
                }
                else
                {
                    lblDetail.Text = "";
                }
                //pnlhelp.Visible = false;
            }
        }
        else
        {
            lblDetail.Text = "";
            //pnlhelp.Visible = false;
        }
    }
}
