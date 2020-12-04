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

public partial class UserRoleforePriceplan : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["infinal"].ConnectionString);
    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["busiwizclient"].ConnectionString);
    DataSet dt;
    SqlConnection conn;
    public SqlConnection connweb;
    protected void Page_Load(object sender, EventArgs e)
    {

        lblVersion.Text = "This PageVersion Is V4  Date:28-Oct-2015 Develop By @Pk";
       
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            FillProduct();
       
        }

    }
    protected void FillProduct()
    {

        //string strcln = " SELECT * from  ProductMaster where ClientMasterId= " + Session["ClientId"].ToString();
        string strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;

        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "productversion";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");



    }

    protected void FillPriceplan()
    {

        string strcln = "select distinct Case when(DefaultRolIdforePriceplan.PricePlanId IS NULL) then  cast ('0' as bit) else  cast('1' as bit) end as chk, ProductMaster.ProductName,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanId from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId Left Join DefaultRolIdforePriceplan on DefaultRolIdforePriceplan.PricePlanId=PricePlanMaster.PricePlanId where PricePlanMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' and PricePlanMaster.active='True' order by PricePlanMaster.PricePlanName Asc";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddl_priceplan.DataSource = dtcln;

        ddl_priceplan.DataValueField = "PricePlanId";
        ddl_priceplan.DataTextField = "PricePlanName";
        ddl_priceplan.DataBind();
        ddl_priceplan.Items.Insert(0, "-Select-");
    }

    protected void FillGrid()
    {


        string strcln1 = "SELECT DISTINCT  dbo.DefaultRole.RoleId, dbo.DefaultDept.DeptName + ':' + dbo.DefaultDesignationTbl.DesignationName AS RoleName, dbo.DefaultDept.DeptName,dbo.DefaultDesignationTbl.Id as degid, dbo.DefaultDesignationTbl.DesignationName,dbo.DefaultDept.DeptId, dbo.DefaultAfterloginForDefaultRolesTBL.pagename FROM            dbo.DefaultRole INNER JOIN dbo.DefaultRolIdforePriceplan ON dbo.DefaultRolIdforePriceplan.DefaultRoleId = dbo.DefaultRole.RoleId INNER JOIN dbo.DefaultDesignationTbl ON dbo.DefaultDesignationTbl.RoleId = dbo.DefaultRole.RoleId INNER JOIN dbo.DefaultDept ON dbo.DefaultDept.DeptId = dbo.DefaultDesignationTbl.DeptId LEFT OUTER JOIN dbo.DefaultAfterloginForDefaultRolesTBL ON dbo.DefaultDesignationTbl.Id = dbo.DefaultAfterloginForDefaultRolesTBL.DefaultDesignationTbl  where dbo.DefaultRole.VersionId="+ ddlProductname.SelectedValue  +"";
        //SELECT DISTINCT  dbo.DefaultRole.RoleId, dbo.DefaultDept.DeptName + ':' + dbo.DefaultDesignationTbl.DesignationName AS RoleName, dbo.DefaultDept.DeptName, dbo.DefaultDesignationTbl.DesignationName, dbo.DefaultAfterloginForDefaultRolesTBL.pagename FROM            dbo.DefaultRole INNER JOIN dbo.DefaultRolIdforePriceplan ON dbo.DefaultRolIdforePriceplan.DefaultRoleId = dbo.DefaultRole.RoleId INNER JOIN dbo.DefaultDesignationTbl ON dbo.DefaultDesignationTbl.RoleId = dbo.DefaultRole.RoleId INNER JOIN dbo.DefaultDept ON dbo.DefaultDept.DeptId = dbo.DefaultDesignationTbl.DeptId LEFT OUTER JOIN dbo.DefaultAfterloginForDefaultRolesTBL ON dbo.DefaultDesignationTbl.Id = dbo.DefaultAfterloginForDefaultRolesTBL.DefaultDesignationTbl
        SqlCommand cmdcln = new SqlCommand(strcln1, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView3.DataSource = dtcln;
        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        //if (hdnsortExp.Value != string.Empty)
        //{
        //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //}

        GridView3.DataBind();
       
    }
    protected void ddlProductname_SelectedIndexChanged11(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillGrid(); 
        FillPriceplan();
           
    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
    }


   
  
   
    protected void Button2_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
       
        Panel3.Enabled = true;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "change")
        {
            ViewState["StrategyMasterId"] = Convert.ToInt32(e.CommandArgument);
           
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Image Image1 = (Image)e.Row.FindControl("Image1");

        //    Image1.ImageUrl = "~/ShoppingCart/images/" + Image1.ImageUrl;
        //}
     
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView3.EditIndex = e.NewEditIndex;

        FillGrid();

        DropDownList DropDownList1 = (DropDownList)(GridView3.Rows[GridView3.EditIndex].FindControl("DropDownList1"));

        //string bbc = "select [StatusMaster].[StatusName],[StatusMaster].[StatusId] from [StatusMaster] where [StatusCategoryMasterId]='2207'";
        string strcln = "select Distinct PageMaster.PageId,case when (pageplaneaccesstbl.Id IS NULL) then cast('0' as bit) else pageplaneaccesstbl.pageaccess End as pag,  PageMaster.PageName from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId  inner join PricePlanMaster on PricePlanMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId inner join PageMaster on PageMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId Left join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId   where  PageMaster.Active='1' and  PageMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "'   Order By  PageMaster.PageName";

        SqlDataAdapter da1 = new SqlDataAdapter(strcln, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        DropDownList1.DataSource = dt1;
        DropDownList1.DataValueField = "PageId";
        DropDownList1.DataTextField = "PageName";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "-Select-");
    }


    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList DropDownList1 = (DropDownList)(GridView3.Rows[e.RowIndex].FindControl("DropDownList1"));
        Label txtdesigid = (Label)(GridView3.Rows[e.RowIndex].FindControl("txtdeptid"));
      //  Label lblVancanID = (Label)(GridView3.Rows[e.RowIndex].FindControl("lblVancanID"));



        DataTable dtsell = select("select * from DefaultAfterloginForDefaultRolesTBL where DefaultDesignationTbl='" + txtdesigid.Text + "'");

        if (dtsell.Rows.Count > 0)
        {
            SqlCommand cmdin = new SqlCommand("update DefaultAfterloginForDefaultRolesTBL set PagemasterID='" + DropDownList1.SelectedValue + "',VersionId='" + ddlProductname.SelectedValue + "',pagename='" + DropDownList1.SelectedItem.Text + "' where DefaultDesignationTbl='" + txtdesigid.Text + "'", con);
            con.Open();
            cmdin.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            SqlCommand cmdin = new SqlCommand("insert into DefaultAfterloginForDefaultRolesTBL(DefaultDesignationTbl,PagemasterID, VersionId, pagename)values('" + txtdesigid.Text + "','" + DropDownList1.SelectedValue + "', '" + ddlProductname.SelectedValue + "' , '" + DropDownList1.SelectedItem.Text + "')", con);
            con.Open();
            cmdin.ExecuteNonQuery();
            con.Close();
        }

       
        GridView3.EditIndex = -1;
        FillGrid();
    }


    protected void GridView1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GridView3.EditIndex = -1;
        FillGrid();
    }
}

