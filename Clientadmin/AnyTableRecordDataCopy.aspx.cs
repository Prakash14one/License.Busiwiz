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
    protected void clr()
    {
        FillProduct();
        fillfiltercodetype();
        FillLBTable();

        txt1.Text = "";
        txt2.Text = "";
        txt3.Text = "";
        txt4.Text = "";

        txtto1.Text = "";
        txtto2.Text = "";
        txtto3.Text = "";
        txtto4.Text = "";

        txtcontrol.Text = "";
        txttblsearch.Text = "";

         
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

        DDLCopyToTable.DataSource = dtcln;
        DDLCopyToTable.DataValueField = "Id";
        DDLCopyToTable.DataTextField = "TableName";
        DDLCopyToTable.DataBind();
        DDLCopyToTable.Items.Insert(0, "--Select--");
        DDLCopyToTable.Items[0].Value = "0";
    }
    protected void DDLLiceTableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txttblsearch.Text = DDLLiceTableName.SelectedItem.Text;
        Boolean Connstatus = CheckDbConn();
        FillLBField();
       
    }
    protected void DDLCopyToTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        Boolean Connstatus = CheckDbConn();
        FillLBFieldTO();
    }
    
    protected void FillLBField()
    {
        DataTable dts1 = selectdynamic(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + DDLLiceTableName.SelectedItem.Text  + "'");
        DDL1.DataSource = dts1;
        DDL1.DataValueField = "column_name";
        DDL1.DataTextField = "column_name";
        DDL1.DataBind();
        DDL1.Items.Insert(0, "--Select--");
        DDL1.Items[0].Value = "0";


        DDL2.DataSource = dts1;
        DDL2.DataValueField = "column_name";
        DDL2.DataTextField = "column_name";
        DDL2.DataBind();
        DDL2.Items.Insert(0, "--Select--");
        DDL2.Items[0].Value = "0";


        DDL3.DataSource = dts1;
        DDL3.DataValueField = "column_name";
        DDL3.DataTextField = "column_name";
        DDL3.DataBind();
        DDL3.Items.Insert(0, "--Select--");
        DDL3.Items[0].Value = "0";

        DDL4.DataSource = dts1;
        DDL4.DataValueField = "column_name";
        DDL4.DataTextField = "column_name";
        DDL4.DataBind();
        DDL4.Items.Insert(0, "--Select--");
        DDL4.Items[0].Value = "0";

        DDLNotCopied.DataSource = dts1;
        DDLNotCopied.DataValueField = "column_name";
        DDLNotCopied.DataTextField = "column_name";
        DDLNotCopied.DataBind();
        DDLNotCopied.Items.Insert(0, "--Select--");
        DDLNotCopied.Items[0].Value = "0";    

       
    }
    protected void DDL1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DDL2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DDL3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DDL4_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void FillLBFieldTO()
    {
        DataTable dts1 = selectdynamic(" select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + DDLCopyToTable.SelectedItem.Text + "'");
        DDLTo1.DataSource = dts1;
        DDLTo1.DataValueField = "column_name";
        DDLTo1.DataTextField = "column_name";
        DDLTo1.DataBind();
        DDLTo1.Items.Insert(0, "--Select--");
        DDLTo1.Items[0].Value = "0";


        DDLTo2.DataSource = dts1;
        DDLTo2.DataValueField = "column_name";
        DDLTo2.DataTextField = "column_name";
        DDLTo2.DataBind();
        DDLTo2.Items.Insert(0, "--Select--");
        DDLTo2.Items[0].Value = "0";


        DDLTo3.DataSource = dts1;
        DDLTo3.DataValueField = "column_name";
        DDLTo3.DataTextField = "column_name";
        DDLTo3.DataBind();
        DDLTo3.Items.Insert(0, "--Select--");
        DDLTo3.Items[0].Value = "0";

        DDLTo4.DataSource = dts1;
        DDLTo4.DataValueField = "column_name";
        DDLTo4.DataTextField = "column_name";
        DDLTo4.DataBind();
        DDLTo4.Items.Insert(0, "--Select--");
        DDLTo4.Items[0].Value = "0";

        DDLcontrol.DataSource = dts1;
        DDLcontrol.DataValueField = "column_name";
        DDLcontrol.DataTextField = "column_name";
        DDLcontrol.DataBind();
        DDLcontrol.Items.Insert(0, "--Select--");
        DDLcontrol.Items[0].Value = "0";

        
    }
    protected void DDLTo1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DDLTo2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DDLTo3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DDLTo4_SelectedIndexChanged(object sender, EventArgs e)
    {

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
   
    protected void Start()
    {
        Boolean Connstatus = CheckDbConn();
        string where = "";

        TextBox1.Text = " Insert Into  " + DDLCopyToTable.SelectedItem.Text + " (";

        lbl_PKid.Text = "";
        DataTable dts1 = selectdynamic("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + DDLLiceTableName.SelectedItem.Text + "'");
        for (int k = 0; k < dts1.Rows.Count; k++)
        {
            //if (k == 0)
            //{
            //    lbl_PKid.Text = dts1.Rows[k]["column_name"].ToString();
            //}
            if (dts1.Rows[k]["column_name"].ToString() != DDLNotCopied.SelectedItem.Text)
            {
                TextBox1.Text += dts1.Rows[k]["column_name"].ToString() + " ,";
            }
            //AddColuminGrid(dts1.Rows[k]["column_name"].ToString());
        }
        TextBox1.Text = TextBox1.Text.Remove(TextBox1.Text.Length - 1);
        TextBox1.Text += ") Select ";
        DataTable dts1to = selectdynamic("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + DDLCopyToTable.SelectedItem.Text + "'");
        for (int k = 0; k < dts1to.Rows.Count; k++)
        {
            if (dts1to.Rows[k]["column_name"].ToString() != DDLNotCopied.SelectedItem.Text)
            {
                if (dts1to.Rows[k]["column_name"].ToString() == DDL1.SelectedItem.Text && DDL1.SelectedIndex > 0)
                {
                    TextBox1.Text += " '" + txtto1.Text + "' ,";
                    if (where == "")
                    {
                        where += " " + dts1to.Rows[k]["column_name"].ToString() + "=" + txt1.Text; 
                    }
                    else
                    {
                        where += " and " + dts1to.Rows[k]["column_name"].ToString() + "=" + txt1.Text; 
                    }                    
                }
                else if (dts1to.Rows[k]["column_name"].ToString() == DDL2.SelectedItem.Text && DDL2.SelectedIndex > 0)
                {
                    TextBox1.Text += " '" + txtto2.Text + "' ,";
                    if (where == "")
                    {
                        where += " " + dts1to.Rows[k]["column_name"].ToString() + "=" + txt2.Text;
                    }
                    else
                    {
                        where += " and " + dts1to.Rows[k]["column_name"].ToString() + "=" + txt2.Text;
                    }                    
                }
                else if (dts1to.Rows[k]["column_name"].ToString() == DDL3.SelectedItem.Text && DDL3.SelectedIndex > 0)
                {
                    TextBox1.Text += " '" + txtto3.Text + "' ,";
                    if (where == "")
                    {
                        where += "  " + dts1to.Rows[k]["column_name"].ToString() + "=" + txt3.Text;
                    }
                    else
                    {
                        where += " and " + dts1to.Rows[k]["column_name"].ToString() + "=" + txt3.Text;
                    }                    
                }
                else if (dts1to.Rows[k]["column_name"].ToString() == DDL4.SelectedItem.Text && DDL4.SelectedIndex > 0)
                {
                    TextBox1.Text += " '" + txtto4.Text + "' ,";
                    if (where == "")
                    {
                        where += "  " + dts1to.Rows[k]["column_name"].ToString() + "=" + txt4.Text;
                    }
                    else
                    {
                        where += " and " + dts1to.Rows[k]["column_name"].ToString() + "=" + txt4.Text;
                    }
                   
                }
                else if (dts1to.Rows[k]["column_name"].ToString() == DDLcontrol.SelectedItem.Text && DDLcontrol.SelectedIndex > 0)
                {
                    TextBox1.Text += " '" + txtcontrol.Text + "' ,";                    
                }
                else
                {
                    TextBox1.Text += dts1to.Rows[k]["column_name"].ToString() + " ,";
                }
            }
        }
        if (where == "")
        {
            where = "  " + where;
        }
        else
        {
            where = " Where " + where;
        }  
        TextBox1.Text = TextBox1.Text.Remove(TextBox1.Text.Length - 1);
        TextBox1.Text += " From  " + DDLLiceTableName.SelectedItem.Text + " " + where;
    }
    protected void btnStart_Click(object sender, EventArgs e)
    {
        Start();
    }
    protected void btncopynow_Click(object sender, EventArgs e)
    {
        lblmsg.Text = ""; 
      //  Start();
        Boolean check=  CheckDbConn();
        if (check == true)
        {
            string str = @" " + TextBox1.Text + " ";
            SqlCommand cmd = new SqlCommand(str, conn1);
            if (conn1.State.ToString() != "Open")
            {
                conn1.Open();
            }
            //cmd.ExecuteNonQuery();
            int numberOfRecords = cmd.ExecuteNonQuery();
            conn1.Close();

            lblmsg.Text = "Successfully inserted " + numberOfRecords + " record";
            clr(); 

        }
        else
        {
            lblmsg.Text = "Connection problem"; 
        }
    }


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
                    conn1.Close();
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
