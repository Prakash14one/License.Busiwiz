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
public partial class ListofExeAll : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillClient();
            FillGrid();            
        }
        lblmsg.Text = "";
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
    protected void FillGrid()
    {
        //string strcln = " SELECT ProductMaster.*, ProductDetail.* " +
        //                " FROM   ProductDetail RIGHT OUTER JOIN " +
        //                " ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId " +
        //                " where ClientMasterId= " + Session["ClientId"].ToString();

        string strcln = " SELECT  CONVERT(NCHAR(10), (SELECT case ProductDetailExe.Status when   NULL      then 'Not Done' When '0' then 'Not Done' else 'Done' end     ))as Status ,        ProductDetailExe.ProductDetailExeId, ProductDetailExe.ProductDetailId, ProductDetailExe.Location, ProductDetailExe.UploadDate, " +
                     " ProductDetailExe.Status, ProductDetailExe.PricePlanId AS Expr4, ProductDetail.ProductDetailId AS Expr1, ProductDetail.ProductId AS Expr5, " +
                     " ProductDetail.VersionNo, ProductDetail.Active AS Expr6, ProductDetail.Startdate AS Expr7, ProductDetail.EndDate AS Expr8," +
                     " ProductDetail.ProductSetup, ProductDetail.ProductDb, ProductDetail.ProductExtra, ProductMaster.ProductId  , ProductMaster.ClientMasterId,  " +
                     " ProductMaster.ProductName, ProductMaster.ProductURL, ProductMaster.PricePlanURL, ClientMaster.ClientMasterId  ,  " +
                     " ClientMaster.CompanyName, ClientMaster.Address1, ClientMaster.Address2, ClientMaster.CountryId, ClientMaster.StateId, ClientMaster.City,  " +
                     " ClientMaster.Zipcode, ClientMaster.ContactPersonName, ClientMaster.Fax1, ClientMaster.Fax2, ClientMaster.Email1, ClientMaster.Email2, " +
                     " ClientMaster.Phone1, ClientMaster.Phone2, ClientMaster.ClientURL, ClientMaster.CustomerSupportURL, ClientMaster.SalesCustomerSupportURL, " +
                     " ClientMaster.SalesPhoneNo, ClientMaster.SalesFaxNo, ClientMaster.SalesEmail, ClientMaster.AfterSalesSupportPhoneNo,  " +
                     " ClientMaster.AfterSalesSupportFaxNo, ClientMaster.AfterSalesSupportEmail, ClientMaster.TechSupportPhoneNo, ClientMaster.TechSupportFaxNo,  " +
                     " ClientMaster.TechSupportEmail, ClientMaster.FTP, ClientMaster.FTPUserName, ClientMaster.FTPPassword, ClientMaster.LoginName, " +
                     " ClientMaster.LoginPassword, ClientMaster.ClientPricePlanID, PricePlanMaster.PricePlanName AS Expr9, PricePlanMaster.PricePlanAmount AS Expr10,  " +
                     " PricePlanMaster.* FROM         PricePlanMaster RIGHT OUTER JOIN " +
                     " ProductDetailExe ON PricePlanMaster.PricePlanId = ProductDetailExe.PricePlanId LEFT OUTER JOIN " +
                     " ProductMaster LEFT OUTER JOIN " +
                     " ClientMaster ON ProductMaster.ClientMasterId = ClientMaster.ClientMasterId RIGHT OUTER JOIN " +
                     " ProductDetail ON ProductMaster.ProductId = ProductDetail.ProductId ON ProductDetailExe.ProductDetailId = ProductDetail.ProductDetailId ";



        if (ddlStatus.SelectedItem.Value == "2")
        {
          //  strcln = strcln + " where ProductDetailExe.status is    null or  ProductDetailExe.status = 0 ";
            //   strcln = strcln + "   and ClientMaster.ClientMasterId = '" + ddlClientList.SelectedItem.Value.ToString() + "'";
        }
        else if (ddlStatus.SelectedItem.Value == "1")
        {
               strcln = strcln + " where   ProductDetailExe.status = 1 ";
            //   strcln = strcln + "   and ClientMaster.ClientMasterId = '" + ddlClientList.SelectedItem.Value.ToString() + "'";
        }
        else if (ddlStatus.SelectedItem.Value == "0")
        {
              strcln = strcln + " where ProductDetailExe.status is    null or  ProductDetailExe.status = 0 ";
            //   strcln = strcln + "   and ClientMaster.ClientMasterId = '" + ddlClientList.SelectedItem.Value.ToString() + "'";
        }
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView1.DataSource = dtcln;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblLocation");
         //   lbl.Text = Server.MapPath(".") + "\\ProductMainCode\\" + DataBinder.Eval(e.Row.DataItem, "Location").ToString();

            lbl.Text = "W:\\websites\\members.busiwiz.com\\Clients\\" + DataBinder.Eval(e.Row.DataItem, "ClientMasterId").ToString() + "\\SetupExe\\" + DataBinder.Eval(e.Row.DataItem, "ProductId").ToString() +"\\" + DataBinder.Eval(e.Row.DataItem, "Location").ToString();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.SelectedDataKey.Value);
            string strcln   = "update ProductDetailExe set Status = 1 where productDetailExeId=" + i.ToString() ; // +

            SqlCommand cmd = new SqlCommand(strcln, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            FillGrid();
            lblmsg.Text = "Data is updated.";
            lblmsg.Visible = true;
        }
    }
    protected void ddlClientList_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}
