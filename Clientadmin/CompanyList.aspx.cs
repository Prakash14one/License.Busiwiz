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

public partial class Admin_CompanyList : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ifilecabinateConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
        string str = "  SELECT     CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, " +
                      " CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email AS Expr1, " +
                      "  CompanyMaster.pincode, CompanyMaster.phone, CompanyMaster.fax AS Expr2, CompanyMaster.CompanyLoginId, CompanyMaster.AdminId, " +
                       " CompanyMaster.Password, CompanyMaster.redirect + '.ifilecabinet.com ' AS url, CASE CompanyMaster.active WHEN 1 THEN 'Active' ELSE 'Deactive' END AS active, " +
                       " CompanyMaster.deactiveReason, PricePlanView.*  FROM         CompanyMaster LEFT OUTER JOIN " +
                        " PricePlanView ON CompanyMaster.PlanId = PricePlanView.PricePlanDetailId "; 
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
                GridView1.Rows[gdr.RowIndex].BackColor = System.Drawing.Color.LightGreen;

            }
            else
            {
                GridView1.Rows[gdr.RowIndex].BackColor = System.Drawing.Color.LightPink ;
                GridView1.Rows[gdr.RowIndex].ForeColor = System.Drawing.Color.White;
            }
        }

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
            Response.Redirect("CompanySetupEdit.aspx?id="+ i +"");
        }
    }
}
