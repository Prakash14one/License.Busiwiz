
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
using System.IO;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data.Common;
//using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Smo.SqlEnum;
//using Microsoft.SqlServer.Management.Sdk.Sfc;
using System.Configuration;
using System.Collections.Specialized;


public partial class Productportalmaster2 : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection conn1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Login"] != null)
            {
                if (Session["Login"].ToString() == null)
                {
                    Response.Redirect("../Login.aspx");
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
            
            FillProduct();
            fillproductid();
            fillcodetype();
            FillTableCategory();

            SearchVersion();
            SearchCodetype();
            SearchCategory();
            FillGrid();
        }
    }
  
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        pnladdnew.Visible = true;
        Label1.Visible = true;
        addnewpanel.Visible = false;
        lbladd.Text = "Add New Client Product Table Details";

        ddlProductVersion.Enabled = true;
        DDLTableCategory.Enabled = true;
        txttable.Enabled = true;

        btnSubmit.Visible = true;
    }
    public void FillProduct()
    {
        String strcln = " SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName  as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);        
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductVersion.DataSource = dtcln;
        ddlProductVersion.DataValueField = "VersionInfoId";
        ddlProductVersion.DataTextField = "productversion";
        ddlProductVersion.DataBind();      
    }
    protected void ddlProductVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillproductid();
        fillcodetype();
        FillTableCategory();
    }
    protected void fillproductid()
    {
        DataTable dtcln = MyCommonfile.selectBZ("SELECT distinct ProductMaster.ProductId,ProductMaster.Description  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and VersionInfoMaster.VersionInfoId='" + ddlProductVersion.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            ViewState["ProductId"] = dtcln.Rows[0]["ProductId"].ToString();
            Label11.Text = dtcln.Rows[0]["Description"].ToString();
        }
        else
        {
            Label11.Text = "";
        }
    }  
    protected void fillcodetype()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT TOP (100) PERCENT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName,dbo.CodeTypeTbl.CodeTypeCategoryId FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id  where  ProductCodeDetailTbl.Active='1' and CodeTypeTbl.ProductVersionId='" + ddlProductVersion.SelectedValue + "' and CodeTypeCategory.Id='2'  order  by dbo.ProductCodeDetailTbl.CodeTypeName ");
        DDLCodetype.DataSource = dtcln;
        DDLCodetype.DataValueField = "Id";
        DDLCodetype.DataTextField = "CodeTypeName";
        DDLCodetype.DataBind();
        DDLCodetype.Items.Insert(0, "--Select--");
        DDLCodetype.Items[0].Value = "0";
    }
    protected void DDLCodetype_SelectedIndexChanged(object sender, EventArgs e)
    {       
        CheckDbConn();
        DataTable dt = MyCommonfile.selectBZ("select BusiwizSynchronization,CompanyDefaultData from ProductCodeDetailTbl where Id='" + DDLCodetype.SelectedValue + "' ");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["BusiwizSynchronization"].ToString() == "1")
            {
                Label23.Text = "This is BusiController database ";
            }
            else if (dt.Rows[0]["CompanyDefaultData"].ToString() == "1")
            {
                Label23.Text = " This is Company Database";
            }
        }
    }
    protected void FillTableCategory()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" select id,CategoryName+'-'+CategoryDescription as CategoryName from TableCategory  where TableCategory.Active='1' ");
        DDLTableCategory.DataSource = dtcln;
        DDLTableCategory.DataValueField = "id";
        DDLTableCategory.DataTextField = "CategoryName";
        DDLTableCategory.DataBind();
        DDLTableCategory.Items.Insert(0, "--Select--");
        DDLTableCategory.Items[0].Value = "0";
    }
    protected void txttable_TextChanged(object sender, EventArgs e)
    {
        CheckDbConn();
        if (conn1.State.ToString() != "Open")
        {
            conn1.Open();
        }
        SqlCommand cmd = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME =@tname ", conn1);
        cmd.Parameters.AddWithValue("@tname", txttable.Text);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Label36.Text = "Table Already Exist in Database";
            ViewState["tableadd"] = "0";
        }
        else
        {
            Label36.Text = "New Table";
            ViewState["tableadd"] = "1";
        }
    }
    private void CheckDbConn()
    {
        DataTable dtdatabaseins = MyCommonfile.selectBZ(@"SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + DDLCodetype.SelectedValue + "'");
        if (dtdatabaseins.Rows.Count > 0)
        {
            string serverid = dtdatabaseins.Rows[0]["ServerId"].ToString();
            string serversqlinstancename = dtdatabaseins.Rows[0]["Instancename"].ToString();
            try
            {
                conn1 = new SqlConnection();
                conn1 = ServerWizard.ServerDatabaseFromInstanceTCP(serverid, serversqlinstancename, DDLCodetype.SelectedItem.Text);
                try
                {
                    if (conn1.State.ToString() != "Open")
                    {
                        conn1.Open();
                        Image1.Visible = true;
                        Image1.ImageUrl = "~/Account/images/Right.jpg";
                        Label35.Text = "Connection Possible";
                        btnSubmit.Enabled = true;
                    }
                }
                catch
                {
                    Image1.Visible = true;
                    Image1.ImageUrl = "~/Account/images/closeicon.png";
                    Label35.Text = "Connection not possible please check the following sql server";
                    // LinkButton2.Visible = true;
                    btnSubmit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Image1.Visible = true;
                Image1.ImageUrl = "~/Account/images/closeicon.png";
                Label35.Text = "Connection not possible Please check the following sql server";
                // LinkButton2.Visible = true;
                btnSubmit.Enabled = false;
            }
        }
    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3.Checked == true)
        {
            Panel9.Visible = true;
        }
        else
        {
            Panel9.Visible = false;
        }
    }
    protected void btnaddnewfield_Click(object sender, EventArgs e)
    {
        Panel10.Visible = true;
        CreateDatatable();
    }
    protected void btn_storefield_Click(object sender, EventArgs e)
    {
        filladdtable();
    }

    protected void ChkPK_CheckedChanged(object sender, EventArgs e)//primary
    {
        CheckBox lnkbtn = (CheckBox)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        CheckBox chk = (CheckBox)gvaddnew.Rows[j].FindControl("ChkPK");//ChkAllowNull
        CheckBox null1 = (CheckBox)gvaddnew.Rows[j].FindControl("ChkAllowNull");
        if (chk.Checked == true)
        {
            null1.Checked = false;
        }
    }
    protected void ChkFK_CheckedChanged(object sender, EventArgs e)//foreign k
    {
        CheckBox lnkbtn = (CheckBox)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);        
        CheckBox lblcandiID = (CheckBox)gvaddnew.Rows[j].FindControl("ChkFK");
        gvaddnew.HeaderRow.Cells[5].Visible = true;
        gvaddnew.Columns[5].Visible = true;
        gvaddnew.HeaderRow.Cells[6].Visible = true;
        gvaddnew.Columns[6].Visible = true;
    }
   
    public void CreateDatatable()
    {
        DataTable dt = new DataTable();

        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "feildname";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "type";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "size";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;

        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "primarykey";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "foreignkey";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;

        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "keytable";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;

        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "keyfeild";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "nullvalue";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "autunumber";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);
        dt.Columns.Add(Dcom8);

        DataRow Drow = dt.NewRow();
        Drow["feildname"] = "";
        Drow["type"] = "";
        Drow["size"] = "";
        Drow["primarykey"] = "";
        Drow["keytable"] = "";
        Drow["keyfeild"] = "";
        Drow["nullvalue"] = "";
        Drow["foreignkey"] = "";
        Drow["autunumber"] = "";
        dt.Rows.Add(Drow);

        gvaddnew.DataSource = dt;
        gvaddnew.DataBind();
        gvaddnew.HeaderRow.Cells[5].Visible = false;
        gvaddnew.Columns[5].Visible = false;
        gvaddnew.HeaderRow.Cells[6].Visible = false;
        gvaddnew.Columns[6].Visible = false;
        foreach (GridViewRow gr5 in gvaddnew.Rows)
        {
            DropDownList DDLgvFKTableName = ((DropDownList)gr5.FindControl("DDLgvFKTableName"));
            CheckBox nul = ((CheckBox)gr5.FindControl("ChkAllowNull"));
            if (ViewState["sp"] == "1")
            {
                nul.Checked = true;
            }
            string strcln = " Select ClientProductTableMaster.TableName,ClientProductTableMaster.Id as tableid From ClientProductTableMaster Where  ClientProductTableMaster.Databaseid='" + DDLCodetype.SelectedValue + "'  Order by ClientProductTableMaster.TableName";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                DDLgvFKTableName.DataSource = dtcln;
                DDLgvFKTableName.DataValueField = "tableid";
                DDLgvFKTableName.DataTextField = "TableName";
                DDLgvFKTableName.DataBind();
                DDLgvFKTableName.Items.Insert(0, "--Select--");
                DDLgvFKTableName.Items[0].Value = "0";
            }
        }
    }
    protected void DDLgvFKTableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList lnkbtn = (DropDownList)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        //Label lblcandiID = (Label)GridView1.Rows[j].FindControl("lblcandiID");
        DropDownList DDLgvFKTableName = (DropDownList)gvaddnew.Rows[j].FindControl("DDLgvFKTableName");//CheckBox6
        DropDownList DDLgvFKFieldName = (DropDownList)gvaddnew.Rows[j].FindControl("DDLgvFKFieldName");
        DataTable dty = MyCommonfile.selectBZ("select Id,feildname from tablefielddetail where tableId='" + DDLgvFKTableName.SelectedValue + "' Order by feildname ");
        if (dty.Rows.Count > 0)
        {
            DDLgvFKFieldName.DataSource = dty;
            DDLgvFKFieldName.DataValueField = "Id";
            DDLgvFKFieldName.DataTextField = "feildname";
            DDLgvFKFieldName.DataBind();
            DDLgvFKFieldName.Items.Insert(0, "--Select--");
            DDLgvFKFieldName.Items[0].Value = "0";
        }
    }  
    public void filladdtable()
    {
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["table"]) == "")
        {
            dt = CreateDatatable1();
        }
        else
        {
            dt = (DataTable)ViewState["table"];
        }
        foreach (GridViewRow gr5 in gvaddnew.Rows)
        {
            TextBox txtfeild = ((TextBox)gr5.FindControl("txt_FieldName"));
            DropDownList ddltype = ((DropDownList)gr5.FindControl("ddlfiledtype"));
            TextBox txtsize = ((TextBox)gr5.FindControl("txtsize"));
            CheckBox chkprimary = ((CheckBox)gr5.FindControl("ChkPK"));
            CheckBox chkforign = ((CheckBox)gr5.FindControl("ChkFK"));
            DropDownList ddlforigntable = ((DropDownList)gr5.FindControl("DDLgvFKTableName"));
            DropDownList ddlforignfeild = ((DropDownList)gr5.FindControl("DDLgvFKFieldName"));
            CheckBox chknull = ((CheckBox)gr5.FindControl("ChkAllowNull"));
            CheckBox chk_identity = ((CheckBox)gr5.FindControl("chk_identity"));            
            //autunumber

            DataRow Drow = dt.NewRow();
            Drow["feildname"] = txtfeild.Text;
            Drow["type"] = ddltype.SelectedValue;
            Drow["size"] = txtsize.Text;
            Drow["primarykey"] = chkprimary.Checked;
            Drow["foreignkey"] = chkforign.Checked;
            if (chkforign.Checked == true)
            {
                Drow["keytable"] = ddlforigntable.SelectedItem.Text;
                Drow["keytableid"] = ddlforigntable.SelectedValue;

                try
                {
                    Drow["keyfeild"] = ddlforignfeild.SelectedItem.Text;
                    Drow["keyfeildid"] = ddlforignfeild.SelectedValue;
                }
                catch
                {
                }
            }
            else
            {
                Drow["keytable"] = "";
                Drow["keyfeild"] = "";
                Drow["keytableid"] = "";
                Drow["keyfeildid"] = "";
            }
            Drow["nullvalue"] = chknull.Checked;
            Drow["autunumber"] = chk_identity.Checked;
            dt.Rows.Add(Drow);
        }
        ViewState["table"] = dt;
        Gv_TableField.DataSource = dt;
        Gv_TableField.DataBind();
        gvaddnew.DataSource = "";
        gvaddnew.DataBind();
        Panel10.Visible = false;
    }
    protected void Gv_TableField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            Gv_TableField.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["table"];
            dt.Rows[Convert.ToInt32(Gv_TableField.SelectedIndex.ToString())].Delete();
            dt.AcceptChanges();
            Gv_TableField.DataSource = dt;
            Gv_TableField.DataBind();
            ViewState["table"] = dt;
        }
    }
    public DataTable CreateDatatable1()
    {
        DataTable dt = new DataTable();

        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "feildname";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "type";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "size";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;

        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "primarykey";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "foreignkey";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;

        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "keytable";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;

        DataColumn Dcom51 = new DataColumn();
        Dcom51.DataType = System.Type.GetType("System.String");
        Dcom51.ColumnName = "keytableid";
        Dcom51.AllowDBNull = true;
        Dcom51.Unique = false;
        Dcom51.ReadOnly = false;

        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "keyfeild";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;


        DataColumn Dcom61 = new DataColumn();
        Dcom61.DataType = System.Type.GetType("System.String");
        Dcom61.ColumnName = "keyfeildid";
        Dcom61.AllowDBNull = true;
        Dcom61.Unique = false;
        Dcom61.ReadOnly = false;

        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "nullvalue";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "autunumber";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);
        dt.Columns.Add(Dcom51);
        dt.Columns.Add(Dcom61);
        dt.Columns.Add(Dcom8);
        return dt;

    }




















    //Search Grid
    protected void SearchVersion()
    {
        DataTable dtcln = MyCommonfile.selectBZ("SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName  as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  order  by productversion");
        DDL_ProductversionSearch.DataSource = dtcln;
        DDL_ProductversionSearch.DataValueField = "VersionInfoId";
        DDL_ProductversionSearch.DataTextField = "productversion";
        DDL_ProductversionSearch.DataBind();
        DDL_ProductversionSearch.Items.Insert(0, "--Select--");
        DDL_ProductversionSearch.Items[0].Value = "0";
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchCodetype();
        FillGrid();
    }
    protected void SearchCodetype()
    {
        DataTable dtcln = MyCommonfile.selectBZ("SELECT DISTINCT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where  ProductCodeDetailTbl.Active='1' and CodeTypeTbl.ProductVersionId='" + DDL_ProductversionSearch.SelectedValue + "' and CodeTypeCategory.Id='2'  order  by dbo.ProductCodeDetailTbl.CodeTypeName ");     
        DropDownList6.DataSource = dtcln;
        DropDownList6.DataValueField = "Id";
        DropDownList6.DataTextField = "CodeTypeName";
        DropDownList6.DataBind();
        DropDownList6.Items.Insert(0, "--Select--");
        DropDownList6.Items[0].Value = "0";
    }
    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void SearchCategory()
    {        
        DataTable dtcln = MyCommonfile.selectBZ(" Select id,CategoryName+'-'+CategoryDescription as CategoryName from TableCategory  where TableCategory.Active='1' "); 
        DropDownList5.DataSource = dtcln;
        DropDownList5.DataValueField = "id";
        DropDownList5.DataTextField = "CategoryName";
        DropDownList5.DataBind();
        DropDownList5.Items.Insert(0, "--Select--");
        DropDownList5.Items[0].Value = "0";
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    
    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void FillGrid()
    {
        string strcln = "";

        if (DDL_ProductversionSearch.SelectedIndex > 0)
        {
            strcln += "  and ( ClientProductTableMaster.VersionInfoId='" + DDL_ProductversionSearch.SelectedItem.Value + "' )";
        }

        if (DropDownList6.SelectedIndex > 0)
        {
            strcln += " and ClientProductTableMaster.Databaseid='" + DropDownList6.SelectedValue + "'";
        }
        if (DropDownList7.SelectedIndex > 0)
        {
            strcln += " and ClientProductTableMaster.Active='" + DropDownList7.SelectedValue + "'";
        }
        if (DropDownList5.SelectedIndex > 0)
        {
            strcln += " and ClientProductTableMaster.category='" + DropDownList5.SelectedValue + "'";
        }
        if (TextBox1.Text != "")
        {
            strcln += " and ((ClientProductTableMaster.TableName like '%" + TextBox1.Text.Replace("'", "''") + "%' OR ClientProductTableMaster.TableTitle like '%" + TextBox1.Text.Replace("'", "''") + "%' ))";
        }
        strcln = "SELECT distinct  ClientProductTableMaster.*,ClientProductTableMaster.TableName as parenttable ,ProductCodeDetailTbl.CodeTypeName as databasename,TableCategory.CategoryName, VersionInfoMaster.VersionInfoName, ProductMaster.ProductName, VersionInfoMaster.ProductId, ProductMaster.ProductId , ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion, ProductMaster.ProductId as sp  FROM  ProductMaster INNER JOIN  VersionInfoMaster ON ProductMaster.ProductId = VersionInfoMaster.ProductId INNER JOIN ClientProductTableMaster ON VersionInfoMaster.VersionInfoId = ClientProductTableMaster.VersionInfoId left join ParentTableAdd on ParentTableAdd.TableId =  ClientProductTableMaster.Id left join TableCategory on TableCategory.Id=ParentTableAdd.Category left join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=ClientProductTableMaster.Databaseid  inner join CodeTypeTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where ClientProductTableMaster.Id>0   " + strcln + "";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        //SqlCommand cmdcln = new SqlCommand("selectproductdetaitablefillgrid", con);
        //cmdcln.CommandType = CommandType.StoredProcedure;
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            GridView1.DataSource = dtcln;
            GridView1.DataBind();
            for (int rowindex = 0; rowindex < dtcln.Rows.Count; rowindex++)
            {
                DataTable dateyu = MyCommonfile.selectBZ("select * from ParentTableAdd inner join ClientProductTableMaster on ClientProductTableMaster.Id=ParentTableAdd.ParentTableid where ParentTableAdd.TableID='" + dtcln.Rows[rowindex]["Id"] + "'");
                if (dateyu.Rows.Count > 0)
                {

                    string aa1 = "";
                    for (int i = 0; i < dateyu.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            string date = dateyu.Rows[i]["TableName"].ToString();
                            aa1 = dateyu.Rows[i]["TableName"].ToString();
                        }
                        else
                        {
                            aa1 = aa1 + ", " + dateyu.Rows[i]["TableName"].ToString();
                        }
                    }
                    dtcln.Rows[rowindex]["parenttable"] = aa1.ToString();
                }

                DataTable sp = MyCommonfile.selectBZ("select Proce_id,SP_name from storeproc_usingtable   inner join StoreProcedure_Master on StoreProcedure_Master.storeproce_id =storeproc_usingtable. Proce_id   where storeproc_usingtable.table_id='" + dtcln.Rows[rowindex]["Id"] + "'");
                if (sp.Rows.Count > 0)
                {

                    string aa1 = "";
                    for (int i = 0; i < sp.Rows.Count; i++)
                    {
                        if (i == 0)
                        {

                            string date = sp.Rows[i]["SP_name"].ToString();
                            aa1 = sp.Rows[i]["SP_name"].ToString();
                        }
                        else
                        {

                            aa1 = aa1 + ", " + sp.Rows[i]["SP_name"].ToString();

                        }
                    }
                    dtcln.Rows[rowindex]["sp"] = aa1.ToString();
                }
                else
                {
                    dtcln.Rows[rowindex]["sp"]=Convert.ToInt32(null) ;
                }
            }
            GridView1.DataSource = dtcln;
            GridView1.DataBind();

        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
   
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblmsg.Text = "";
        if (e.CommandName == "Edit")
        {
            lbladd.Text = "Edit Client Product Table Details";
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;
            BtnUpdate.Visible = true;
            btnSubmit.Visible = false;
            int mm = Convert.ToInt32(e.CommandArgument);
            SqlCommand cmd = new SqlCommand(" SELECT distinct  ClientProductTableMaster.*,ClientProductTableMaster.TableName as parenttable ,ProductCodeDetailTbl.CodeTypeName as databasename,TableCategory.id as catid, VersionInfoMaster.VersionInfoName, ProductMaster.ProductName, VersionInfoMaster.ProductId, ProductMaster.ProductId , ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion  FROM  ProductMaster INNER JOIN  VersionInfoMaster ON ProductMaster.ProductId = VersionInfoMaster.ProductId INNER JOIN ClientProductTableMaster ON VersionInfoMaster.VersionInfoId = ClientProductTableMaster.VersionInfoId left join ParentTableAdd on ParentTableAdd.TableId =  ClientProductTableMaster.Id left join TableCategory on TableCategory.Id=ParentTableAdd.Category left join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=ClientProductTableMaster.Databaseid  inner join CodeTypeTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id  where ClientProductTableMaster.Id='" + mm + "'", con);
            DataTable dtbn = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtbn);
            ViewState["Productid"] = dtbn.Rows[0]["Id"].ToString();
            FillProduct();
            ddlProductVersion.SelectedValue = dtbn.Rows[0]["VersionInfoId"].ToString();
            ddlProductVersion.Enabled = false;
            try
            {
                FillTableCategory();
                DDLTableCategory.SelectedValue = dtbn.Rows[0]["category"].ToString();
            }
            catch
            {
            }
            
           
            fillcodetype();
            DDLCodetype.SelectedValue = dtbn.Rows[0]["Databaseid"].ToString();            
            txttable.Text = dtbn.Rows[0]["TableName"].ToString();
            txttable.Enabled = false;
            txttitle.Text = dtbn.Rows[0]["TableTitle"].ToString();
            try
            {
                CheckBox1.Checked = Convert.ToBoolean(dtbn.Rows[0]["Active"]);
            }
            catch
            {
            }
            if (DDLCodetype.SelectedIndex > 0)
            {
                DataTable gg = MyCommonfile.selectBZ("select feildname as feildname,fieldtype as type,size as size,isprimarykey as primarykey ,notallownull as nullvalue,Isforeignkey as foreignkey,foreignkeytblid as keytableid,foreignkeyfieldId as keyfeildid,foreignkeytblid as keytable,foreignkeyfieldId as keyfeild ,AUTONUMBER_IDENTITY as autunumber from tablefielddetail where tableId='" + ViewState["Productid"].ToString() + "' ");
                if (gg.Rows.Count > 0)
                {
                    CheckBox3.Checked = true;
                    Panel9.Visible = true;
                    DataTable dtrf = new DataTable();
                    ViewState["table"] = "";
                    if (Convert.ToString(ViewState["table"]) == "")
                    {
                        dtrf = CreateDatatable1();
                    }
                    else
                    {
                        dtrf = (DataTable)ViewState["table"];
                    }
                    for (int k = 0; k < gg.Rows.Count; k++)
                    {

                        DataRow Drow = dtrf.NewRow();
                        Drow["feildname"] = gg.Rows[k]["feildname"].ToString();
                        Drow["type"] = gg.Rows[k]["type"].ToString();
                        Drow["size"] = gg.Rows[k]["size"].ToString();
                        Drow["primarykey"] = gg.Rows[k]["primarykey"].ToString();
                        Drow["foreignkey"] = gg.Rows[k]["foreignkey"].ToString();

                        DataTable dt23 = MyCommonfile.selectBZ("select * from ClientProductTableMaster where id='" + gg.Rows[k]["keytableid"].ToString() + "' ");
                        if (dt23.Rows.Count > 0)
                        {
                            Drow["keytable"] = dt23.Rows[0]["TableName"].ToString();
                            Drow["keytableid"] = gg.Rows[k]["keytableid"].ToString();
                        }
                        else
                        {
                        }
                        DataTable dt231 = MyCommonfile.selectBZ("select * from tablefielddetail where Id='" + gg.Rows[k]["keyfeildid"].ToString() + "' ");
                        if (dt231.Rows.Count > 0)
                        {
                            Drow["keyfeild"] = dt231.Rows[0]["feildname"].ToString();
                            Drow["keyfeildid"] = gg.Rows[k]["keyfeildid"].ToString();
                        }

                        Drow["nullvalue"] = gg.Rows[k]["nullvalue"].ToString();
                        Drow["autunumber"] = gg.Rows[k]["autunumber"].ToString();
                        dtrf.Rows.Add(Drow);
                    }
                    ViewState["table"] = dtrf;
                    Gv_TableField.DataSource = dtrf;
                    Gv_TableField.DataBind();
                    Gv_TableField.Columns[9].Visible = false;

                }
            }
            DataTable sp = MyCommonfile.selectBZ("select Proce_id,SP_name from storeproc_usingtable  inner join StoreProcedure_Master on StoreProcedure_Master.storeproce_id =storeproc_usingtable. Proce_id   where storeproc_usingtable.table_id='" + ViewState["Productid"].ToString() + "'");
            if (sp.Rows.Count > 0)
            {
                ViewState["sp"] = "1";
                string b = "";
                for (int i = 0; i < sp.Rows.Count; i++)
                {
                    string a = "<a href=http://license.busiwiz.com/Clientadmin/Viewstoredprocedureprofile.aspx?id=" + sp.Rows[i]["Proce_id"].ToString() + " target=_blank>" + sp.Rows[i]["SP_name"].ToString() + "</a>";
                    b = "" + b + "," + a + "";
                }
                Label371.Text = b;
            }

            else
            {
                ViewState["sp"] = "0";
            }
            DataTable sp1 = MyCommonfile.selectBZ("select Pagemaster.PageName from storeproc_usingpage   inner join StoreProcedure_Master on StoreProcedure_Master.storeproce_id =storeproc_usingpage. Proce_id  inner join storeproc_usingtable on storeproc_usingtable.Proce_id=StoreProcedure_Master.storeproce_id   inner join PageMaster on PageMaster.PageId=storeproc_usingpage.page_id   where storeproc_usingtable.table_id='" + ViewState["Productid"].ToString() + "'");
            if (sp1.Rows.Count > 0)
            {
                ViewState["sp"] = "1";
                string b1 = "";
                for (int i = 0; i < sp1.Rows.Count; i++)
                {
                    string a1 = sp.Rows[i]["PageName"].ToString();
                    b1 = "" + b1 + "," + a1 + "";
                }
                Label372.Text = b1;
            }
            else
            {
                ViewState["sp"] = "0";
            }
        }
        if (e.CommandName == "ViewRecords")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);
            string te = "Addrecordstable.aspx?id=" + mm1;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
        if (e.CommandName == "Delete")
        {
            lblmsg.Visible = true;
            int mm1 = Convert.ToInt32(e.CommandArgument);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlDataAdapter daf = new SqlDataAdapter(" select * from ClientProductRecordsAllowed where ClientProductTblId='" + mm1 + "' ", con);
            DataTable dtf = new DataTable();
            daf.Fill(dtf);
            if (dtf.Rows.Count > 0)
            {
                lblmsg.Text = "Sorry, you are not able to delete this record as child record exist using this record.";
            }
            else
            {
                String str11 = " Delete From ClientProductTableMaster   where Id='" + e.CommandArgument.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(str11, con);
                cmd1.ExecuteNonQuery();
                con.Close();
                con.Open();
                String str12 = "Delete From ParentTableAdd   where TableID='" + e.CommandArgument.ToString() + "'";
                SqlCommand cmd12 = new SqlCommand(str12, con);
                cmd12.ExecuteNonQuery();
                con.Close();
                lblmsg.Text = "Record deleted successfully.";
                FillGrid();
                ddlProductVersion.SelectedIndex = 0;
                btnSubmit.Visible = true;
                Panel1.Visible = true;
                BtnUpdate.Visible = false;
            }
        }
        if (e.CommandName == "view")
        {
            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "Viewtableprofile.aspx?id=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
    }
    protected void GridView1_RowEditing1(object sender, GridViewEditEventArgs e)
    {
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }









    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }


  
   

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string str11 = "Select * From ClientProductTableMaster Where (( VersionInfoId='" + ddlProductVersion.SelectedValue + "' and TableName='" + txttable.Text + "') or ( VersionInfoId='" + ddlProductVersion.SelectedValue + "' and TableTitle='" + txttitle.Text + "'))  and Id<>'" + ViewState["Productid"] + "'";
         str11 = "Select * From ClientProductTableMaster Where (( VersionInfoId='" + ddlProductVersion.SelectedValue + "' and TableName='" + txttable.Text + "')) and ClientProductTableMaster.Databaseid='"+DDLCodetype.SelectedValue +"'  and Id<>'" + ViewState["Productid"] + "'";
        
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand(str11, con);
        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist.";
            con.Close();
            //    ddlProductVersion.SelectedIndex = 0;
            BtnUpdate.Visible = false;
            btnSubmit.Visible = true;
            Panel1.Visible = false;
        }
        else
        {
            Boolean TableInsert = false;
            if (ViewState["tableadd"].ToString() == "1")
            {
                try
                {
                    string aa = "";
                    string pk = "";
                    DataTable st1 = MyCommonfile.selectBZ("select feildname,fieldtype,size,Isforeignkey,foreignkeytblid,foreignkeyfieldId,isprimarykey,notallownull from  tablefielddetail where TableId='" + Convert.ToInt32(Session["value"]) + "'  and AUTONUMBER_IDENTITY=1");
                    if (st1.Rows.Count > 0)
                    {
                        pk = "" + st1.Rows[0]["feildname"].ToString() + " [" + st1.Rows[0]["fieldtype"].ToString() + "] IDENTITY(1,1) NOT NULL ,";
                    }
                    DataTable st = MyCommonfile.selectBZ("select feildname,fieldtype,size,Isforeignkey,foreignkeytblid,foreignkeyfieldId,isprimarykey,notallownull from  tablefielddetail where TableId='" + Convert.ToInt32(Session["value"]) + "' and AUTONUMBER_IDENTITY=0");
                    if (st.Rows.Count > 0)
                    {
                        for (int i = 0; i < st.Rows.Count; i++)
                        {
                            string null1 = st.Rows[i]["notallownull"].ToString();
                            string nn = "NOT NULL";
                            if (null1.ToString() == "False")
                            {
                                nn = "NOT NULL";
                            }
                            else
                            {
                                nn = "NULL";
                            }
                            string bb = "";
                            if (st.Rows[i]["fieldtype"].ToString() == "int" || st.Rows[i]["fieldtype"].ToString() == "datetime" || st.Rows[i]["fieldtype"].ToString() == "bit")
                            {
                                bb = "" + st.Rows[i]["feildname"].ToString() + " [" + st.Rows[i]["fieldtype"].ToString() + "] " + nn + "";
                            }
                            else
                            {
                                bb = "" + st.Rows[i]["feildname"].ToString() + " [" + st.Rows[i]["fieldtype"].ToString() + "] (" + st.Rows[i]["size"].ToString() + ") " + nn + "";
                            }
                            aa += "" + bb + ",";
                        }
                    }
                    CheckDbConn();
                    aa = pk + aa;
                    if (aa.Length > 0)
                    {
                        aa = aa.Remove(aa.Length - 1);
                    }
                    if (aa.Length > 0)
                    {
                        string createtable = " Create Table [dbo]." + txttable.Text + "(" + aa + ")";
                        SqlCommand cmdf = new SqlCommand(createtable, conn1);
                        if (conn1.State.ToString() != "Open")
                        {
                            conn1.Open();
                        }
                        cmdf.ExecuteNonQuery();
                        conn1.Close();
                        TableInsert = true;
                    }
                    //string createtableD = " DROP Table [dbo]." + txttable.Text + "";
                  
                }
                catch (Exception ex)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Some problem when adding table. <br>"+ex.ToString();
                }               
            }
          
            if (TableInsert == true)
            {
                string str = "Insert Into ClientProductTableMaster(VersionInfoId,TableName,TableTitle,Databaseid,Active,category) values('" + ddlProductVersion.SelectedValue + "','" + txttable.Text + "','" + txttitle.Text + "','" + DDLCodetype.SelectedValue + "','" + CheckBox1.Checked + "','" + DDLTableCategory.SelectedValue + "')";
                SqlCommand cmd1 = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                DataTable dt2 = new DataTable();
                cmd1.ExecuteNonQuery();

                string strnw = "select Max(Id) from ClientProductTableMaster";
                SqlCommand cmds = new SqlCommand(strnw, con);
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dts = new DataTable();
                das.Fill(dts);
                if (dts.Rows.Count > 0)
                {
                    int Active = Convert.ToInt32(dts.Rows[0][0]);
                    Session["value"] = Active;
                }

                foreach (GridViewRow gr51 in Gv_TableField.Rows)
                {
                    Label txtfeild = ((Label)gr51.FindControl("Label25"));
                    Label ddltype = ((Label)gr51.FindControl("Label27"));
                    Label txtsize = ((Label)gr51.FindControl("Label28"));
                    Label chkprimary = ((Label)gr51.FindControl("Label29"));
                    Label chkforign = ((Label)gr51.FindControl("Label31"));
                    Label ddlforigntableid = ((Label)gr51.FindControl("Label351"));
                    Label ddlforignfeildid = ((Label)gr51.FindControl("Label354"));
                    Label chknull = ((Label)gr51.FindControl("Label34"));
                    Label lblgvAutoNumber = ((Label)gr51.FindControl("lblgvAutoNumber"));

                    SqlCommand cmd33 = new SqlCommand(" Insert into tablefielddetail(TableId,feildname,fieldtype,size,Isforeignkey,foreignkeytblid,foreignkeyfieldId,isprimarykey,notallownull,AUTONUMBER_IDENTITY)Values('" + Convert.ToInt32(Session["value"]) + "','" + txtfeild.Text + "','" + ddltype.Text + "','" + txtsize.Text + "','" + chkforign.Text + "','" + ddlforigntableid.Text + "','" + ddlforignfeildid.Text + "','" + chkprimary.Text + "','" + chknull.Text + "','" + lblgvAutoNumber.Text + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd33.ExecuteNonQuery();
                }


                lblmsg.Visible = true;
                lblmsg.Text = "Record Inserted sucessfully.";
                FillGrid();

                ddlProductVersion.SelectedIndex = 0;
                lbladd.Text = "";
                txttable.Text = "";
                txttitle.Text = "";

                btndosyncro_Click(sender, e);
                addnewpanel.Visible = true;
                pnladdnew.Visible = false;
            }

            try
            {
                string server = "" + Label361.Text + "\\" + Label365.Text + "";
                DataTable dtui = MyCommonfile.selectBZ("select category from ClientProductTableMaster where Id='" + Session["value"].ToString() + "'");
                if (dtui.Rows[0][0].ToString() == "2")
                {
                    string te = "scriptadd.aspx?server=" + PageMgmt.Encrypted(server.ToString()) + "&database=" + DDLCodetype.SelectedItem.Text + "&tablename=" + txttable.Text + "&productid=" + ddlProductVersion.SelectedValue + "&data=1";
                    // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }
                else
                {
                    string te = "scriptadd.aspx?server=" + PageMgmt.Encrypted(server.ToString()) + "&database=" + DDLCodetype.SelectedItem.Text + "&tablename=" + txttable.Text + "&productid=" + ddlProductVersion.SelectedValue + "&data=2";
                    //  ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }
            }
            catch
            {
            }            
        }
    }
    protected void BtnUpdate_Click1(object sender, EventArgs e)
    {
        string qry = "update   ClientProductTableMaster set VersionInfoId='" + ddlProductVersion.SelectedValue + "',TableName='" + txttable.Text + "',TableTitle='" + txttitle.Text + "' , Databaseid='" + DDLCodetype.SelectedValue + "',Active='" + CheckBox1.Checked + "',category='" + DDLTableCategory.SelectedValue + "' where Id='" + ViewState["Productid"] + "'";
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();

        string str13 = "delete from  ParentTableAdd where  TableID='" + ViewState["Productid"] + "'";
        SqlCommand cmd13 = new SqlCommand(str13, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlDataAdapter da31 = new SqlDataAdapter(cmd13);
        DataTable dt31 = new DataTable();
        cmd13.ExecuteNonQuery();
        con.Close();

      

        string str131 = "delete from  tablefielddetail where  tableId='" + ViewState["Productid"] + "'";
        SqlCommand cmd131 = new SqlCommand(str131, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd131.ExecuteNonQuery();
        con.Close();
        foreach (GridViewRow gr51 in Gv_TableField.Rows)
        {
            Label txtfeild = ((Label)gr51.FindControl("Label25"));
            Label ddltype = ((Label)gr51.FindControl("Label27"));
            Label txtsize = ((Label)gr51.FindControl("Label28"));
            Label chkprimary = ((Label)gr51.FindControl("Label29"));
            Label chkforign = ((Label)gr51.FindControl("Label31"));
            Label ddlforigntableid = ((Label)gr51.FindControl("Label351"));
            Label ddlforignfeildid = ((Label)gr51.FindControl("Label354"));
            Label chknull = ((Label)gr51.FindControl("Label34"));
            Label lblgvAutoNumber = ((Label)gr51.FindControl("lblgvAutoNumber"));
            SqlCommand cmd33 = new SqlCommand("Insert into tablefielddetail(TableId,feildname,fieldtype,size,Isforeignkey,foreignkeytblid,foreignkeyfieldId,isprimarykey,notallownull,AUTONUMBER_IDENTITY)Values('" + ViewState["Productid"] + "','" + txtfeild.Text + "','" + ddltype.Text + "','" + txtsize.Text + "','" + chkforign.Text + "','" + ddlforigntableid.Text + "','" + ddlforignfeildid.Text + "','" + chkprimary.Text + "','" + chknull.Text + "','" + lblgvAutoNumber.Text + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd33.ExecuteNonQuery();
        }



            CheckDbConn();
            string aa = "";
            string pk = "";

            DataTable st1 = MyCommonfile.selectBZ("select feildname,fieldtype,size,Isforeignkey,foreignkeytblid,foreignkeyfieldId,isprimarykey,notallownull from  tablefielddetail where tableId='" + ViewState["Productid"].ToString() + "' and  isprimarykey=1 and AUTONUMBER_IDENTITY=1 ");
            if (st1.Rows.Count > 0)
            {
                pk = "" + st1.Rows[0]["feildname"].ToString() + " [" + st1.Rows[0]["fieldtype"].ToString() + "] IDENTITY(1,1) NOT NULL";

                try
                {
                    string createtable = " ALTER TABLE [dbo]." + txttable.Text + " ADD " + pk + ";";
                    SqlCommand cmdf = new SqlCommand(createtable, conn1);
                    if (conn1.State.ToString() != "Open")
                    {
                        conn1.Open();
                    }
                    cmdf.ExecuteNonQuery();
                    conn1.Close();
                }
                catch
                {
                }                
            }
            DataTable st = MyCommonfile.selectBZ("select feildname,fieldtype,size,Isforeignkey,foreignkeytblid,foreignkeyfieldId,isprimarykey,notallownull from  tablefielddetail where tableId='" + ViewState["Productid"].ToString()  + "' and AUTONUMBER_IDENTITY=0 ");
            if (st.Rows.Count > 0)
            {
                for (int i = 0; i < st.Rows.Count; i++)
                {
                    string null1 = st.Rows[i]["notallownull"].ToString();
                    string nn = "NOT NULL";
                    if (null1.ToString() == "False")
                    {
                        nn = "NOT NULL";
                    }
                    else
                    {
                        nn = "NULL";
                    }
                    string bb = "";
                    if (st.Rows[i]["fieldtype"].ToString() == "int" || st.Rows[i]["fieldtype"].ToString() == "datetime" || st.Rows[i]["fieldtype"].ToString() == "bit")
                    {
                        bb = "" + st.Rows[i]["feildname"].ToString() + " [" + st.Rows[i]["fieldtype"].ToString() + "] " + nn + "";
                    }
                    else
                    {
                        bb = "" + st.Rows[i]["feildname"].ToString() + " [" + st.Rows[i]["fieldtype"].ToString() + "] (" + st.Rows[i]["size"].ToString() + ") " + nn + "";
                    }
                    aa += "" + bb + ",";

                    try
                    {
                        //ALTER TABLE aaaasasa
                        //ADD name [nvarchar](50) NULL;
                       
                        string createtable = " ALTER TABLE [dbo]." + txttable.Text + " ADD " + bb + ";";
                        SqlCommand cmdf = new SqlCommand(createtable, conn1);
                        if (conn1.State.ToString() != "Open")
                        {
                            conn1.Open();
                        }
                        cmdf.ExecuteNonQuery();
                        conn1.Close();
                    }
                    catch
                    {
                    }
                }
            }
           // CheckDbConn();
            aa = pk + aa;
            if (aa.Length > 0)
            {
                aa = aa.Remove(aa.Length - 1);
            }
            try
            {
                //string createtableD = " DROP Table [dbo]." + txttable.Text + "";
                //SqlCommand cmdf = new SqlCommand(createtableD, conn1);
                //if (conn1.State.ToString() != "Open")
                //{
                //    conn1.Open();
                //}
                //cmdf.ExecuteNonQuery();
                //conn1.Close();
            }
            catch
            {
            }
            try
            {
                //string createtable = " Create Table [dbo]." + txttable.Text + "(" + aa + ")";
                //SqlCommand cmdfd = new SqlCommand(createtable, conn1);
                //if (conn1.State.ToString() != "Open")
                //{
                //    conn1.Open();
                //}
                //cmdfd.ExecuteNonQuery();
                //conn1.Close();
            }
            catch
            {
            }



        lblmsg.Visible = true;
        FillGrid();
        lblmsg.Text = "Record updated successfully.";
        con.Close();

        ddlProductVersion.SelectedIndex = 0;
        txttable.Text = "";
        txttitle.Text = "";
        BtnUpdate.Visible = false;
        btnSubmit.Visible = true;
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        Panel1.Visible = true;
        btndosyncro_Click(sender, e);
      
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        lblmsg.Text = "";
        ddlProductVersion.SelectedIndex = 0;
        txttable.Text = "";
        txttitle.Text = "";
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        BtnUpdate.Visible = false;
        Response.Redirect("ClientProductTableDetails.aspx");
    }

  

   
   
    
  
   
  
    
    

   
     protected void LinkButton3_Click(object sender, EventArgs e)//view recors
     {

     } 

    

     protected void btndosyncro_Clickpop(object sender, EventArgs e)
     {
         ModernpopSync.Show();
     }
  

     protected void btndosyncro_Click(object sender, EventArgs e)
     {
         int transf = 0;
         DataTable dt1 = MyCommonfile.selectBZ("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='ClientProductTableMaster'");
         if (dt1.Rows.Count > 0)
         {
             for (int i = 0; i < dt1.Rows.Count; i++)
             {
                 string datetim = DateTime.Now.ToString();
                 string arqid = dt1.Rows[i]["Id"].ToString();

                 string str22 = "Insert Into SyncronisationrequiredTbl(SatelliteSyncronisationrequiringTablesMasterID,DateandTime)Values('" + arqid + "','" + Convert.ToDateTime(datetim) + "')";
                 if (con.State.ToString() != "Open")
                 {
                     con.Open();
                 }
                 SqlCommand cmn = new SqlCommand(str22, con);
                 cmn.ExecuteNonQuery();
                 con.Close();

                 DataTable dt121 = MyCommonfile.selectBZ("SELECT Max(ID) as ID from SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='" + arqid + "'");

                 if (Convert.ToString(dt121.Rows[0]["ID"]) != "")
                 {
                     DataTable dtcln = MyCommonfile.selectBZ("SELECT Distinct ServerMasterTbl.Id FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId    where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' ");

                     for (int j = 0; j < dtcln.Rows.Count; j++)
                     {
                         if (con.State.ToString() != "Open")
                         {
                             con.Open();
                         }
                         transf = Convert.ToInt32(rdsync.SelectedValue);
                         string str223 = "Insert Into SateliteServerRequiringSynchronisationMasterTbl(SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + dt121.Rows[0]["ID"] + "','" + dtcln.Rows[j]["Id"] + "','0','" + DateTime.Now.ToString() + "')";
                         SqlCommand cmn3 = new SqlCommand(str223, con);
                         cmn3.ExecuteNonQuery();
                         con.Close();
                     }
                 }
             }
         }

         if (transf > 0)
         {
             string te = "SyncData.aspx?verId=" + transf;
             ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

         }
     }

     protected void Button1_Click1(object sender, EventArgs e)
     {
         if (Button1.Text == "Printable Version")
         {

             Button1.Text = "Hide Printable Version";
             Button4.Visible = true;
             if (GridView1.Columns[3].Visible == true)
             {
                 ViewState["editHide"] = "tt";
                 GridView1.Columns[3].Visible = false;
             }
             if (GridView1.Columns[4].Visible == true)
             {
                 ViewState["deleHide"] = "tt";
                 GridView1.Columns[4].Visible = false;
             }
         }
         else
         {

             Button1.Text = "Printable Version";
             Button4.Visible = false;
             if (ViewState["editHide"] != null)
             {
                 GridView1.Columns[3].Visible = true;
             }
             if (ViewState["deleHide"] != null)
             {
                 GridView1.Columns[4].Visible = true;
             }
         }
     }
}






