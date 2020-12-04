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
public partial class ManagePricePlanofCustomer : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGrid();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void  FillGrid()
    {
       string str = " SELECT     ClientMaster.ClientMasterId, ClientMaster.CompanyName, ClientMaster.Address1, ClientMaster.Address2, ClientMaster.CountryId, ClientMaster.StateId, " +
                     " ClientMaster.City, ClientMaster.Zipcode, ClientMaster.ContactPersonName, ClientMaster.Fax1, ClientMaster.Fax2, ClientMaster.Email1, ClientMaster.Email2, " +
                     " ClientMaster.Phone1, ClientMaster.Phone2, ClientMaster.ClientURL, ClientMaster.CustomerSupportURL, ClientMaster.SalesCustomerSupportURL, " +
                     " ClientMaster.SalesPhoneNo, ClientMaster.SalesFaxNo, ClientMaster.SalesEmail, ClientMaster.AfterSalesSupportPhoneNo, ClientMaster.AfterSalesSupportFaxNo, " +
                     " ClientMaster.AfterSalesSupportEmail, ClientMaster.TechSupportPhoneNo, ClientMaster.TechSupportFaxNo, ClientMaster.TechSupportEmail, ClientMaster.FTP, " +
                     " ClientMaster.FTPUserName, ClientMaster.FTPPassword, ClientMaster.LoginName, ClientMaster.LoginPassword, CountryMaster.CountryId AS Expr1, " +
                     " CountryMaster.CountryName, CountryMaster.Country_Code, StateMasterTbl.StateId AS Expr2, StateMasterTbl.StateName, StateMasterTbl.CountryId AS Expr3, " +
                     " StateMasterTbl.State_Code FROM         StateMasterTbl INNER JOIN " +
                      " CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId RIGHT OUTER JOIN " +
                      " ClientMaster ON StateMasterTbl.StateId = ClientMaster.StateId AND CountryMaster.CountryId = ClientMaster.CountryId ";
                           
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();      
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString()); //.SelectedDataKey.Value);
            Response.Redirect("EditClientInfo.aspx?ClientId=" + i.ToString());
        }
    }
}
