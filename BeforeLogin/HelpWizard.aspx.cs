using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class HelpWizard : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Menucategory"] != null && Request.QueryString["MainMenu"] != null)
            {
                if (Request.QueryString["Menucategory"] == "Help Wizard")
                {
                    Panel2.Visible = true;
                    helpmenus();
                }
                else
                {
                    Panel2.Visible = false;
                    Label18.Text = "This page will only for display help  written under pages";
                }
            }
        }
    }
    public void helpmenus()
    {
        DataTable dty = select("SELECT  MainMenuName FROM   MainMenuMaster where MainMenuId='" + Request.QueryString["MainMenu"].ToString() + "'");
        if (dty.Rows.Count > 0)
        {
            mainmenu.Text = dty.Rows[0]["MainMenuName"].ToString();
            DataTable dt = select("SELECT SubMenuName, SubMenuId FROM SubMenuMaster where  MainMenuId='" + Request.QueryString["MainMenu"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                DataList3.DataSource = dt;
                DataList3.DataBind();
                foreach (DataListItem gvbn in DataList3.Items)
                {
                    Label id = (Label)gvbn.FindControl("Label17");
                    DataList dl = (DataList)gvbn.FindControl("DataList4");
                    DataTable page = select("SELECT PageTitle, PageId FROM PageMaster where MainMenuId='" + Request.QueryString["MainMenu"].ToString() + "' and SubMenuId='" + id.Text+ "'");
                    if (page.Rows.Count > 0)
                    {
                        dl.DataSource = page;
                        dl.DataBind();
                    }
                }
            }
        }
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;
        DataListItem item = (DataListItem)lnkbtn.NamingContainer;
        int i = Convert.ToInt32(item.ItemIndex);
        Label id = ((Label)DataList3.Items[i].FindControl("Label17"));
        Panel pnl = ((Panel)DataList3.Items[i].FindControl("Panel1"));
        ImageButton img = ((ImageButton)DataList3.Items[i].FindControl("ImageButton1"));
        if (pnl.Visible == true)
        {
            pnl.Visible = false;
            img.ImageUrl = "~/images/plus.png";
        }
        else
        {
            pnl.Visible = true;
            img.ImageUrl = "~/Images/minus.png";
        }
    }
}