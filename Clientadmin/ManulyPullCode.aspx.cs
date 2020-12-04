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
public partial class ManulyPullCode : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection conn;
    public static string encstr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ClientId"] = "35";
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {

            ViewState["sortOrder"] = "";
            fillserver();

            FillProduct();
            fillcodetype();

            fillgrid();
        }

    }
    protected void fillserver()
    {
        DataTable dtcln = selectBZ("SELECT Id,ServerName from ServerMasterTbl where Status='1' order  by ServerName");
        ddlserver.DataSource = dtcln;
        ddlserver.DataValueField = "Id";
        ddlserver.DataTextField = "ServerName";
        ddlserver.DataBind();

        ddlserver.Items.Insert(0, "All");
        ddlserver.Items[0].Value = "0";

    }
    protected void FillProduct()
    {
        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion");
        FilterProductname.DataSource = dtcln;
        FilterProductname.DataValueField = "VersionInfoId";
        FilterProductname.DataTextField = "productversion";
        FilterProductname.DataBind();
        FilterProductname.Items.Insert(0, "All");
        FilterProductname.Items[0].Value = "0";



    }

    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }


    protected void fillcodetype()
    {
        string strcln = " select * from CodeTypeTbl where ProductVersionId='" + FilterProductname.SelectedValue + "'  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlcodetype.DataSource = dtcln;
        ddlcodetype.DataValueField = "ID";
        ddlcodetype.DataTextField = "Name";
        ddlcodetype.DataBind();

        ddlcodetype.Items.Insert(0, "All");
        ddlcodetype.Items[0].Value = "0";
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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

    }

    private static string Encrypt(string strtxt, string strtoencrypt)
    {
        byte[] bykey = new byte[20];
        byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bykey = System.Text.Encoding.UTF8.GetBytes(strtoencrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strtxt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, dv), CryptoStreamMode.Write);
            cs.Write(inputArray, 0, inputArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            return strtxt;
            //  throw ex;
        }

    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        fillgrid();
    }
    protected void fillgrid()
    {
        string stcper = "";


        lblproductname.Text = FilterProductname.SelectedItem.Text;
        lblcodetype.Text = ddlcodetype.SelectedItem.Text;



        if (ddlserver.SelectedIndex > 0)
        {
            lblserv.Text = ddlserver.SelectedItem.Text;
            stcper = stcper + " and ProductMasterCodeonsatelliteserverTbl.ServerID='" + ddlserver.SelectedValue + "'";
        }
        if (FilterProductname.SelectedIndex > 0)
        {
            stcper = stcper + " and  ProductMasterCodeTbl.ProductVerID='" + FilterProductname.SelectedValue + "'";
        }
        if (ddlcodetype.SelectedIndex > 0)
        {
            stcper = stcper + " and ProductMasterCodeTbl.CodeTypeID ='" + ddlcodetype.SelectedValue + "'";
        }

        string orderby = " order by ID Desc ";

        string str = " select Top(100) ProductMasterCodeonsatelliteserverTbl.ID,ProductMasterCodeonsatelliteserverTbl.ServerID,ServerMasterTbl.Busiwizsatellitesiteurl,ServerMasterTbl.ServerName,CodeTypeTbl.Name ,ProductMaster.ProductName+':'+VersionInfoMaster.VersionInfoName as VersionInfoName,ProductMasterCodeTbl.codeversionnumber,ProductMasterCodeTbl.filename,ProductMasterCodeonsatelliteserverTbl.Successfullyuploadedtoserver  from ProductMasterCodeTbl inner join ProductMasterCodeonsatelliteserverTbl on ProductMasterCodeonsatelliteserverTbl.ProductMastercodeID=ProductMasterCodeTbl.ID inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=ProductMasterCodeTbl.ProductVerID inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ServerMasterTbl on ServerMasterTbl.Id=ProductMasterCodeonsatelliteserverTbl.ServerID inner join CodeTypeTbl on CodeTypeTbl.ID=ProductMasterCodeTbl.CodeTypeID where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and ServerMasterTbl.status='1' " + stcper + orderby;
        DataTable dtsvr = selectBZ(str);
        GridView1.DataSource = dtsvr;
        GridView1.DataBind();


    }
    protected void ch1_chachedChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow item in GridView1.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void FilterProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcodetype();
    }
   
    protected void btntransfercode_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        string pv = GridView1.DataKeys[rinrow].Value.ToString();

        Label lblproductlatestcodeid = (Label)GridView1.Rows[rinrow].FindControl("lblproductlatestcodeid");
        Label lblserverid = (Label)GridView1.Rows[rinrow].FindControl("lblserverid");
        Label lblbusicontrolsiteurl = (Label)GridView1.Rows[rinrow].FindControl("lblbusicontrolsiteurl");

        string serverurl = "http://" + lblbusicontrolsiteurl.Text;
        string te1 = serverurl + "/" + "ServerFolderCreationPage.aspx?ID=" + lblproductlatestcodeid.Text + "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te1 + "');", true);


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblupsuc = (Label)e.Row.FindControl("lblupsuc");
            Button btntransfercode = (Button)e.Row.FindControl("btntransfercode");

            if (lblupsuc.Text == "True")
            {
                // btntransfercode.Visible = false;
                btntransfercode.Enabled = false;
            }

        }
    }

    
}
