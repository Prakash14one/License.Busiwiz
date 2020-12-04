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
using System.DirectoryServices;
using System.IO.Compression;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Security.Cryptography;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;


public partial class CodeType : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    SqlConnection conn;
    SqlConnection conmaster = new SqlConnection(ConfigurationManager.ConnectionStrings["masterfile"].ConnectionString);
    public static string encstr = "";
    
    
    protected void Page_Load(object sender, EventArgs e)
    {        
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            FillProduct();
            fillcodetypecategory();
            fillgrid();
           
        }

    }

    protected void FillProduct()
    {

        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion");
        ddlproductversion.DataSource = dtcln;
        ddlproductversion.DataValueField = "VersionInfoId";
        ddlproductversion.DataTextField = "productversion";
        ddlproductversion.DataBind();

        fillproductid();

    }

    protected void fillproductid()
    {
        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and VersionInfoMaster.VersionInfoId='" + ddlproductversion.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            ViewState["ProductId"] = dtcln.Rows[0]["ProductId"].ToString();
        }

    }

    protected void fillcodedefaultcategory()
    {
        DataTable dtcln = selectBZ("SELECT * from ProductCodeDetailTbl where ProductId='" + ddlproductversion.SelectedValue + "'"); // ViewState["ProductId"].ToString() replace 7/23 ninad
        DropDownList1.DataSource = dtcln;
        DropDownList1.DataValueField = "Id";
        DropDownList1.DataTextField = "CodeTypeName";
        DropDownList1.DataBind();


    }

    protected void fillcodetypecategory()
    {
        DataTable dtcln = selectBZ(" select * from CodeTypeCategory ");
        ddlcodetypecategory.DataSource = dtcln;
        ddlcodetypecategory.DataValueField = "CodeMasterNo";
        ddlcodetypecategory.DataTextField = "CodeTypeCategory";
        ddlcodetypecategory.DataBind();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;

        }
        else
        {
            Button3.Text = "Printable Version";
            Button4.Visible = false;

        }
    }
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlCommand cmdsq = new SqlCommand("Insert into CodeTypeTbl(Name,CodeTypeCategoryId,ProductVersionId,ProductCodeDetailId)Values('" + txtcodetypecategory.Text + "','" + ddlcodetypecategory.SelectedValue + "','" + ddlproductversion.SelectedValue + "','" + DropDownList1.SelectedValue + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdsq.ExecuteNonQuery();
        con.Close();

        txtcodetypecategory.Text = "";
        lblmsg.Text = "Record Inserted successfully";

        fillgrid();
    }

    protected void fillgrid()
    {
        DataTable dtsvr = selectBZ(" select CodeTypeTbl.*,ProductCodeDetailTbl.CodeTypeName,ProductMaster.ProductName +':'+VersionInfoMaster.VersionInfoName as VersionInfoName,CodeTypeCategory.CodeTypeCategory from CodeTypeTbl inner join CodeTypeCategory on CodeTypeCategory.CodeMasterNo=CodeTypeTbl.CodeTypeCategoryId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=CodeTypeTbl.ProductVersionId inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=CodeTypeTbl.ProductCodeDetailId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' ");

        GridView1.DataSource = dtsvr;

        GridView1.DataBind();
    }
    protected void ddlproductversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillproductid();
        fillcodedefaultcategory();
    }
}
