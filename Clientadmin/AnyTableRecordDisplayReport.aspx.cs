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
using System.Data.Sql;
using System.ServiceModel;
using System.Data.SqlTypes;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class SyncData : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    DataSet dt;
    SqlConnection conn;
    public SqlConnection connweb;
    public static string encstr = "";
    double counter = 0;

    SqlConnection conn1 = new SqlConnection();
    Boolean Portconn = false;
    //Stopwatch sw = new Stopwatch(); 
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            //------------------------------------------------------------------------------
            FillProduct();
            fillfiltercodetype();
            FillLBTable();
            //------------------------------------------------------------------------------                    
        }
    }


    protected void FillProduct()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName,ProductMaster.ProductName +':'+ VersionInfoMaster.VersionInfoName as aa,VersionInfoMaster.VersionInfoId  FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  and VersionInfoMaster.Active='1'  order  by ProductName");
        FilterProductname.DataSource = dtcln;
        FilterProductname.DataValueField = "VersionInfoId";
        FilterProductname.DataTextField = "ProductName";
        FilterProductname.DataBind();
        FilterProductname.Items.Insert(0, "All");
        FilterProductname.Items[0].Value = "0";
    }
    protected void FilterProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        fillfiltercodetype();
    }
    protected void fillfiltercodetype()
    {
        DataTable dtcln = MyCommonfile.selectBZ("SELECT DISTINCT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where  ProductCodeDetailTbl.Active='1' and CodeTypeTbl.ProductVersionId='" + FilterProductname.SelectedValue + "' and dbo.CodeTypeTbl.CodeTypeCategoryId='2'  order  by dbo.ProductCodeDetailTbl.CodeTypeName ");
        ddlctype.DataSource = dtcln;
        ddlctype.DataValueField = "Id";
        ddlctype.DataTextField = "CodeTypeName";
        ddlctype.DataBind();
        ddlctype.Items.Insert(0, "All");
        ddlctype.Items[0].Value = "0";
    }
    protected void ddlctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        Boolean Connstatus = CheckDbConn();
        
        FillLBTable();
    }
    protected void FillLBTable()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT dbo.ClientProductTableMaster.TableName , dbo.ClientProductTableMaster.Id, dbo.SatelliteSyncronisationrequiringTablesMaster.Id AS syncId FROM dbo.SatelliteSyncronisationrequiringTablesMaster INNER JOIN dbo.ClientProductTableMaster ON dbo.SatelliteSyncronisationrequiringTablesMaster.TableID = dbo.ClientProductTableMaster.Id where ClientProductTableMaster.active=1  order by ClientProductTableMaster.TableName ");
        DDLLiceTableName.DataSource = dtcln;
        DDLLiceTableName.DataValueField = "Id";
        DDLLiceTableName.DataTextField = "TableName";
        DDLLiceTableName.DataBind();
        DDLLiceTableName.Items.Insert(0, "--Select--");
        DDLLiceTableName.Items[0].Value = "0";
    }
    protected void DDLLiceTableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txttblsearch.Text = DDLLiceTableName.SelectedItem.Text;
        btnSubmit_Click(sender, e);
    }
    public Boolean CreateColumn(string tablename)
    {
        Boolean status = false;
        try
        {
            lbl_PKid.Text = "";
            DataTable dts1 = selectdynamic("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
            for (int k = 0; k < dts1.Rows.Count; k++)
            {
                if (k == 0)
                {
                    lbl_PKid.Text = dts1.Rows[k]["column_name"].ToString();
                    status = true;
                }
                AddColuminGrid(dts1.Rows[k]["column_name"].ToString());
            }
        }
        catch
        {
            lblmsg.Text = " Connection not working in selected Database/Table";
        }
        return status;
    }
    protected void AddColuminGrid(string ColumnName)
    {
        BoundField bfield = new BoundField();
        bfield.HeaderText = "" + ColumnName + "";
        bfield.DataField = "" + ColumnName + "";       
        GridView1.Columns.Add(bfield);
        
    }

    public Boolean FillDesign(string TableName)
    {
        Boolean status = false;
        try
        {
            DataTable dts1 = selectdynamic(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + TableName + "'");
            GridView2.DataSource = dts1;
            GridView2.DataBind();
            GridView2.Visible = true;            
            status = true;
        }
        catch
        {
        }
        return status;
    }
    public Boolean FillFrid(string TableName)
    {
        Boolean status = false;
        try
        {
            pnlsync.Visible = true;
            GridView1.Visible = true;
            DataTable dtfindtab = selectdynamic(@" SELECT TOP(" + txt_noofrecord.Text + ") *  From " + TableName + "  " + txt_where.Text + " Order by " + lbl_PKid.Text + " " + ddlorder.SelectedValue + "");
            GridView1.DataSource = dtfindtab;
            GridView1.DataBind();
            GridView1.Visible = true;
            DataTable dtfindtabbb = selectdynamic(@" SELECT count(" + lbl_PKid.Text + ") as TotalRec  From " + TableName + " " + txt_where.Text + "");
            lbl_totalrecord.Text ="Total record In table "+ dtfindtabbb.Rows[0]["TotalRec"].ToString();
            status = true;
        }
        catch
        {
            GridView1.Visible = false;
        }
        return status;
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {      
    }
    protected void btnsynTables(object sender, EventArgs e)
    {       
    }   
    //
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        lbl_totalrecord.Text = "";
        lblmsg.Text = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.Columns.Clear();

       
        Boolean Connstatus= CheckDbConn();
        if (Connstatus == true)
        {
            if (txttblsearch.Text == "" || txttblsearch.Text != null)
            {
                Boolean anyfielddeding = FillDesign(txttblsearch.Text);
                GridView1.Visible = true;
                Boolean anyfieldstatus = CreateColumn(txttblsearch.Text);
                if (anyfieldstatus == true)
                {
                    FillFrid(txttblsearch.Text);
                }
                else
                {
                    lblmsg.Text = " table not match";
                }
            }
            else
            {
                GridView1.Visible = false;
            }
        }
        else
        {
            lblmsg.Text = " Connection not working in selected Database"; 
        }
    }
   







    public Boolean CheckDbConn()
    {
        Boolean status = false;
        DataTable dtdatabaseins = MyCommonfile.selectBZ(@"SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ddlctype.SelectedValue + "'");
        if (dtdatabaseins.Rows.Count > 0)
        {
            string serverid = dtdatabaseins.Rows[0]["ServerId"].ToString();
            string serversqlinstancename = dtdatabaseins.Rows[0]["Instancename"].ToString();           
                conn1 = new SqlConnection();
                conn1 = ServerWizard.ServerDatabaseFromInstanceTCP(serverid, serversqlinstancename, ddlctype.SelectedItem.Text);
                try
                {
                    if (conn1.State.ToString() != "Open")
                    {
                        conn1.Open();
                        status = true;
                        img_dbconn.ImageUrl = "~/images/DatabaseConnection/DatabaseConnTrue.png";
                        img_dbconn.Visible = true;
                        conn1.Close();
                    }
                }
                catch
                {
                    img_dbconn.ImageUrl = "~/images/DatabaseConnection/DatabaseConnFalse.png";
                    img_dbconn.Visible = true; 
                }         
        }
        return status;
    }
    protected DataTable selectdynamic(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, conn1);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }


   
    public static string Encrypted(string strText)
    {
        return Encrypt(strText, encstr);
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

 
}
