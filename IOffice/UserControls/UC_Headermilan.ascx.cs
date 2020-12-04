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

public partial class UserControls_UC_Header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //protected void filllogo()
    //{
    //    string str = "SELECT CompanyMaster.CompanyLogo from CompanyMaster where CompanyMaster.Websiteurl='" + Request.Url.Host + "' ";

    //    SqlDataAdapter da = new SqlDataAdapter(str, PageConn.connnn);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);

    //    if (dt.Rows.Count > 0)
    //    {
    //        Image1.ImageUrl = "~/Shoppingcart/images/" + dt.Rows[0]["CompanyLogo"].ToString();
    //    }
    //}
}
