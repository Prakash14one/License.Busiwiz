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
public partial class ClientProductList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillClient();
            FillGrid();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void  FillGrid()
    {

        string strcln = " SELECT ClientMaster.* , ProductMaster.*, ProductDetail.* " +
            "FROM   ClientMaster lefT OUTER JOIN ProductMaster on ClientMaster.ClientMasterId = ProductMaster.ClientMasterId Left Outer join " +                        
                       " ProductDetail ON  ProductMaster.ProductId =  ProductDetail.ProductId ";  
        if (ddlClientList.SelectedIndex > 0)
       {
           strcln = strcln + " where ClientMaster.ClientMasterId=" + ddlClientList.SelectedItem.Value.ToString(); 
        
       }
       SqlCommand cmd = new SqlCommand(strcln, con);
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
          //  GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
          //  int i = Convert.ToInt32(GridView1.SelectedDataKey.Value);

            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.SelectedDataKey.Value);
          //  Response.Redirect("CompanySetupEdit.aspx?id=" + i + "");

          //  GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
          //  int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString()); //.SelectedDataKey.Value);
            Response.Redirect("EditProduct.aspx?productDetailId=" + i.ToString());
        }
    }
    protected void ddlClientList_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
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
}
