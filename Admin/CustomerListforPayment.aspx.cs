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

public partial class CustomerListforPayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillClient();
            fillgrid();
        }
    }
    public DataTable getCompanyMaster()
    {
        //string str = "SELECT     CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation,  "+
        //              " CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email, CompanyMaster.pincode,  "+
        //              " CompanyMaster.phone, CompanyMaster.fax, CompanyMaster.CompanyLoginId, CompanyMaster.AdminId, CompanyMaster.Password, CompanyMaster.redirect + '.ifilecabinet.com '  as url, "+
        //              " case CompanyMaster.active when 1 then 'Active' else 'Deactive' end as active , CompanyMaster.deactiveReason, PricePlanMaster.PlanName, PricePlanMaster.MaxNoOfUser, PricePlanMaster.MaxStorage, "+
        //              " PricePlanMaster.PricePerMonth "+
        //                " FROM         CompanyMaster INNER JOIN "+
        //             " PricePlanMaster ON CompanyMaster.PlanId = PricePlanMaster.PlanId ";
        //string str = "  SELECT     CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, " +
        //              " CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email AS Expr1, " +
        //              "  CompanyMaster.pincode, CompanyMaster.phone, CompanyMaster.fax AS Expr2, CompanyMaster.CompanyLoginId, CompanyMaster.AdminId, " +
        //               " CompanyMaster.Password, CompanyMaster.redirect + '.ifilecabinet.com ' AS url, CASE CompanyMaster.active WHEN 1 THEN 'Active' ELSE 'Deactive' END AS active, " +
        //               " CompanyMaster.deactiveReason, PricePlanView.*  FROM         CompanyMaster LEFT OUTER JOIN " +
        //                " PricePlanView ON CompanyMaster.PlanId = PricePlanView.PricePlanDetailId ";
        //string str = " SELECT     ClientMaster.ClientMasterId, ClientMaster.CompanyName, ClientMaster.Address1, ClientMaster.Address2, ClientMaster.CountryId, ClientMaster.StateId, " +
        //         " ClientMaster.City, ClientMaster.Zipcode, ClientMaster.ContactPersonName, ClientMaster.Fax1, ClientMaster.Fax2, ClientMaster.Email1, ClientMaster.Email2, " +
        //         " ClientMaster.Phone1, ClientMaster.Phone2, ClientMaster.ClientURL, ClientMaster.CustomerSupportURL, ClientMaster.SalesCustomerSupportURL, " +
        //         " ClientMaster.SalesPhoneNo, ClientMaster.SalesFaxNo, ClientMaster.SalesEmail, ClientMaster.AfterSalesSupportPhoneNo, ClientMaster.AfterSalesSupportFaxNo, " +
        //         " ClientMaster.AfterSalesSupportEmail, ClientMaster.TechSupportPhoneNo, ClientMaster.TechSupportFaxNo, ClientMaster.TechSupportEmail, ClientMaster.FTP, " +
        //         " ClientMaster.FTPUserName, ClientMaster.FTPPassword, ClientMaster.LoginName, ClientMaster.LoginPassword, CountryMaster.CountryId AS Expr1, " +
        //         " CountryMaster.CountryName, CountryMaster.Country_Code, StateMasterTbl.StateId AS Expr2, StateMasterTbl.StateName, StateMasterTbl.CountryId AS Expr3, " +
        //         " StateMasterTbl.State_Code FROM         StateMasterTbl INNER JOIN " +
        //          " CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId RIGHT OUTER JOIN " +
        //          " ClientMaster ON StateMasterTbl.StateId = ClientMaster.StateId AND CountryMaster.CountryId = ClientMaster.CountryId " +
        //                 " WHERE (ClientMaster.ClientMasterId = '" + Convert.ToInt32(Request.QueryString["ClientId"].ToString()) + "')";
        //string str = " SELECT     OrderMaster.AdminId, OrderMaster.Password, OrderMaster.Domain, OrderMaster.HostId, CompanyMaster.CompanyId, CompanyMaster.CompanyLoginId, " +
        //           "  PricePlanMaster.PricePlanId, PricePlanMaster.PricePlanName, PricePlanMaster.PricePlanDesc, PricePlanMaster.active AS Expr15, PricePlanMaster.StartDate, " +
        //           "  PricePlanMaster.EndDate, PricePlanMaster.PricePlanAmount, PricePlanMaster.ProductId, CompanyMaster.CompanyId AS Expr1, CompanyMaster.CompanyName, " +
        //           "  CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, " +
        //           "  CompanyMaster.Address, CompanyMaster.Email, CompanyMaster.pincode, CompanyMaster.phone, CompanyMaster.fax, CompanyMaster.CompanyLoginId AS Expr2," +
        //           "  CompanyMaster.AdminId AS Expr3, CompanyMaster.Password AS Expr4, CompanyMaster.redirect, CompanyMaster.active, CompanyMaster.deactiveReason, " +
        //           "  CompanyMaster.PlanActive, CompanyMaster.HostId AS Expr5, CompanyMaster.StateId, CompanyMaster.OrderId, CompanyMaster.ProductId AS Expr6, " +
        //           "  ProductMaster.ClientMasterId, OrderMaster.PlanId AS Expr7, OrderMaster.OrderId AS Expr8 ,ProductMaster.ProductURL" +
        //           " FROM         ProductMaster LEFT OUTER JOIN " +
        //             " PricePlanMaster ON ProductMaster.ProductId = PricePlanMaster.ProductId RIGHT OUTER JOIN " +
        //             " CompanyMaster ON ProductMaster.ProductId = CompanyMaster.ProductId FULL OUTER JOIN " +
        //             " OrderMaster ON CompanyMaster.OrderId = OrderMaster.OrderId " +
        //            " WHERE     (ProductMaster.ClientMasterId = '" + Session["ClientId"].ToString() + "')";
        //string str = "SELECT     OrderMaster.OrderId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, CompanyMaster.Address, CompanyMaster.Email, " + 
        //              " OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, OrderMaster.Password, OrderMaster.Domain, OrderMaster.HostId, " + 
        //            " CompanyMaster.CompanyId ,  "+ 
        //             "  CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.CompanyLoginId, CompanyMaster.redirect, CompanyMaster.active, " + 
        //             " CompanyMaster.deactiveReason, CompanyMaster.PlanActive, CompanyMaster.StateId, OrderPaymentSatus.OrderPaymentId, OrderPaymentSatus.PaymentStatus, " + 
        //             " OrderPaymentSatus.TransactionID, CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz's Server' END AS HostName, " + 
        //             " TempDomainMaster.TempDomainId, TempDomainMaster.TempDomainName, OrderMaster.Status, TempDomainMaster.DatabaseName, " + 
        //             " PricePlanMaster.PricePlanDesc FROM         PricePlanMaster RIGHT OUTER JOIN " + 
        //              " CompanyMaster ON PricePlanMaster.ProductId = CompanyMaster.ProductId LEFT OUTER JOIN " + 
        //              " TempDomainMaster ON CompanyMaster.CompanyId = TempDomainMaster.CompanyId RIGHT OUTER JOIN " + 
        //              " OrderMaster LEFT OUTER JOIN OrderPaymentSatus ON OrderMaster.OrderId = OrderPaymentSatus.OrderId ON CompanyMaster.OrderId = OrderMaster.OrderId" +
        //              " WHERE     (ProductMaster.ClientMasterId = '" + Session["ClientId"].ToString() + "')";
//        string str = "SELECT     OrderMaster.OrderId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, CompanyMaster.Address, " +
//        " CompanyMaster.Email, OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, OrderMaster.Password, OrderMaster.Domain, " +
//         "             OrderMaster.HostId, CompanyMaster.CompanyId, CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.CompanyLoginId, " +
//          "            CompanyMaster.redirect, CompanyMaster.active, CompanyMaster.deactiveReason, CompanyMaster.PlanActive, CompanyMaster.StateId, " +
//           "           OrderPaymentSatus.OrderPaymentId, OrderPaymentSatus.PaymentStatus, OrderPaymentSatus.TransactionID, " +
//            "          CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName, TempDomainMaster.TempDomainId, " +
//             "         TempDomainMaster.TempDomainName, OrderMaster.Status, TempDomainMaster.DatabaseName, PricePlanMaster.PricePlanDesc, PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanAmount, ProductMaster.*" +
//" FROM        PricePlanMaster RIGHT OUTER JOIN ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId RIGHT OUTER JOIN " +
//"                      CompanyMaster ON ProductMaster.ProductId = CompanyMaster.ProductId AND PricePlanMaster.ProductId = CompanyMaster.ProductId LEFT OUTER JOIN " +
// "                     TempDomainMaster ON CompanyMaster.CompanyId = TempDomainMaster.CompanyId RIGHT OUTER JOIN " +
//  "                    OrderMaster LEFT OUTER JOIN OrderPaymentSatus ON OrderMaster.OrderId = OrderPaymentSatus.OrderId ON CompanyMaster.OrderId = OrderMaster.OrderId " +
//         " WHERE     (ProductMaster.ClientMasterId = '" + Session["ClientId"].ToString() + "')";
//string str ="SELECT     OrderMaster.OrderId AS Expr1, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, " + 
//" CompanyMaster.Address, CompanyMaster.Email, OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, OrderMaster.Password, " + 
// "                     OrderMaster.Domain, OrderMaster.HostId, CompanyMaster.CompanyId, CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.CompanyLoginId, " + 
//  "                    CompanyMaster.redirect, CompanyMaster.active, CompanyMaster.deactiveReason, CompanyMaster.PlanActive, CompanyMaster.StateId, " + 
//   "                   CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName, TempDomainMaster.TempDomainId, " + 
//    "                  TempDomainMaster.TempDomainName, OrderMaster.Status, TempDomainMaster.DatabaseName, PricePlanMaster.PricePlanDesc, PricePlanMaster.PricePlanName, " + 
//     "                 PricePlanMaster.PricePlanAmount, ProductMaster.ProductId, ProductMaster.ClientMasterId, ProductMaster.ProductName, ProductMaster.ProductURL, " + 
//      "                ProductMaster.PricePlanURL, OrderPaymentSatus.* " + 
//" FROM         OrderPaymentSatus RIGHT OUTER JOIN " + 
//                      " OrderMaster ON OrderPaymentSatus.OrderId = OrderMaster.OrderId LEFT OUTER JOIN " + 
//                      " PricePlanMaster RIGHT OUTER JOIN " +
//                      " CompanyMaster ON PricePlanMaster.PricePlanId = CompanyMaster.PricePlanId LEFT OUTER JOIN " + 
//                      " TempDomainMaster ON CompanyMaster.CompanyId = TempDomainMaster.CompanyId ON OrderMaster.OrderId = CompanyMaster.OrderId RIGHT OUTER JOIN " +
//"                      ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId AND CompanyMaster.ProductId = ProductMaster.ProductId " +
// " WHERE     (ProductMaster.ClientMasterId = '" + Session["ClientId"].ToString() + "')";
        string str = "SELECT     OrderMaster.OrderId AS Expr1,CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, " + 
                     " CompanyMaster.Address, CompanyMaster.Email, OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, " + 
                     " OrderMaster.Password, OrderMaster.Domain, OrderMaster.HostId, CompanyMaster.CompanyId AS Expr10, CompanyMaster.CompanyWebsite, " +  
                     " CompanyMaster.date, CompanyMaster.CompanyLoginId, CompanyMaster.redirect, CompanyMaster.active, CompanyMaster.deactiveReason, " + 
                     " CompanyMaster.PlanActive, CompanyMaster.StateId, " + 
                     " CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName, TempDomainMaster.TempDomainId,  " + 
                     " TempDomainMaster.TempDomainName, OrderMaster.Status, TempDomainMaster.DatabaseName AS Expr3, PricePlanMaster.PricePlanDesc, " + 
                     " PricePlanMaster.PricePlanName, PricePlanMaster.PricePlanAmount, ProductMaster.ProductId, ProductMaster.ClientMasterId, " + 
                     " ProductMaster.ProductName, ProductMaster.ProductURL, ProductMaster.PricePlanURL, OrderPaymentSatus.OrderPaymentId, " + 
                     " OrderPaymentSatus.OrderId, OrderPaymentSatus.PaymentStatus, OrderPaymentSatus.TransactionID, LicenseMaster.LicenseMasterId, " + 
                     " LicenseMaster.SiteMasterId, LicenseMaster.CompanyId AS Expr2, LicenseMaster.LicenseKey, LicenseMaster.HashKey, LicenseMaster.LicenseDate,  " + 
                     " LicenseMaster.DatabaseName, CASE PricePlanMaster.AllowIPTrack WHEN 1 THEN 'Yes' ELSE 'No' END AS AllowIPTrack, " + 
                     " PricePlanMaster.PricePlanId, PricePlanMaster.PricePlanName AS Expr4, PricePlanMaster.PricePlanDesc AS Expr5, PricePlanMaster.active AS Expr6, " + 
                     " PricePlanMaster.StartDate, PricePlanMaster.EndDate, PricePlanMaster.PricePlanAmount AS Expr7, PricePlanMaster.ProductId AS Expr8, " + 
                     " PricePlanMaster.DurationMonth, PricePlanMaster.AllowIPTrack AS Expr9, PricePlanMaster.GBUsage, PricePlanMaster.MaxUser,  " + 
                     " PricePlanMaster.TrafficinGB, PricePlanMaster.TotalMail, CompanyExpInfo.ExpireId, CompanyExpInfo.ExpDate " + 
                     " FROM         LicenseMaster RIGHT OUTER JOIN " + 
                     " ProductMaster RIGHT OUTER JOIN CompanyMaster LEFT OUTER JOIN " + 
                     " CompanyExpInfo ON CompanyMaster.CompanyId = CompanyExpInfo.CompanyId LEFT OUTER JOIN " + 
                     " PricePlanMaster ON CompanyMaster.PricePlanId = PricePlanMaster.PricePlanId ON ProductMaster.ProductId = CompanyMaster.ProductId ON  " + 
                     " LicenseMaster.CompanyId = CompanyMaster.CompanyId LEFT OUTER JOIN " + 
                     " TempDomainMaster ON CompanyMaster.CompanyId = TempDomainMaster.CompanyId LEFT OUTER JOIN " + 
                     " OrderPaymentSatus RIGHT OUTER JOIN " + 
                     " OrderMaster ON OrderPaymentSatus.OrderId = OrderMaster.OrderId ON CompanyMaster.OrderId = OrderMaster.OrderId    " ;
        
        if (ddlClientList.SelectedIndex > 0)
        { 
        str =str +  " WHERE     (ProductMaster.ClientMasterId = '" + ddlClientList.SelectedItem.Value.ToString() + "')";
        }
        str = str + " order by  CompanyExpInfo.ExpDate  asc ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }
    public void fillgrid()
    {
        DataTable dt = new DataTable();
        dt = getCompanyMaster();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            if (gdr.Cells[4].Text == "Active")
            {
             //   GridView1.Rows[gdr.RowIndex].BackColor = System.Drawing.Color.LightGreen;

            }
            else
            {
             //   GridView1.Rows[gdr.RowIndex].BackColor = System.Drawing.Color.LightPink;
             //   GridView1.Rows[gdr.RowIndex].ForeColor = System.Drawing.Color.White;
            }
        }

    }
    protected void FillClient()
    {
        string str = " SELECT   * from   ClientMaster ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlClientList.DataSource = dt;
        ddlClientList.DataBind();
        ddlClientList.Items.Insert(0, "- All -");
        ddlClientList.Items[0].Value = "0";
        ddlClientList.SelectedIndex = 0;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.SelectedDataKey.Value);
            Response.Redirect("CustomerEditforPayment.aspx?CompanyId=" + i + ""); // order id
        }
    }
    protected void ddlClientList_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  if (ddlClientList.SelectedIndex > 0)
      //  {
            fillgrid();
       // }

    }
}
