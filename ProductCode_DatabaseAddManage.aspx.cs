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

public partial class productcode_databaseaddmanage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    SqlConnection conn;
    //   SqlConnection conmaster = new SqlConnection(ConfigurationManager.ConnectionStrings["masterfile"].ConnectionString);
    public static string encstr = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            ViewState["changedata"] = "0";
            FillProduct();
            fillcodetypecategory();
            //fillgrid();
           // FillProductsearch();
           // FillMainFOlderdown();
            fillgrid();
            FillProductsearch();
        }
    }
    protected void btnAddNewDAta_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        clear();
    }
    protected void FillProduct()
    {
        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion,ProductMaster.ProductName FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductMaster.ProductId = dbo.ProductDetail.ProductId AND dbo.VersionInfoMaster.VersionInfoName = dbo.ProductDetail.VersionNo where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and ProductDetail.Active='True' and VersionInfoMaster.Active='True'  order  by productversion");
        ddlproductversion.DataSource = dtcln;
        ddlproductversion.DataValueField = "VersionInfoId";
        ddlproductversion.DataTextField = "ProductName";
        ddlproductversion.DataBind();
        fillproductid();
    }
    protected void fillproductid()
    {
        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId,ProductMaster.Description  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and VersionInfoMaster.VersionInfoId='" + ddlproductversion.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            ViewState["ProductId"] = dtcln.Rows[0]["ProductId"].ToString();
            txt_prod_desc.Text = dtcln.Rows[0]["Description"].ToString(); 
        }
        else
        {
            txt_prod_desc.Text = "";
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
    protected void ddlproductversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillproductid();
       // FillMainFOlderdown(); 
    }
    protected void fillcodetypecategory()
    {
        DataTable dtcln = selectBZ(" select * from CodeTypeCategory ");
        ddlcodetypecategory.DataSource = dtcln;
        ddlcodetypecategory.DataValueField = "CodeMasterNo";
        ddlcodetypecategory.DataTextField = "CodeTypeCategory";
        ddlcodetypecategory.DataBind();

        ddlcodetypecategory0.DataSource = dtcln;
        ddlcodetypecategory0.DataValueField = "CodeMasterNo";
        ddlcodetypecategory0.DataTextField = "CodeTypeCategory";
        ddlcodetypecategory0.DataBind();

    }
    protected void ddlcodetypecategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel2.Visible = false;
        Panel3.Visible = false;
        //CheckBox1.Checked = false;
        //CheckBox11.Checked = false;
        //CheckBox12.Checked = false;

        if (ddlcodetypecategory.SelectedItem.Text == "Code")
        {
            Panel2.Visible = true;
            Panel3.Visible = false;
        }
        else if (ddlcodetypecategory.SelectedItem.Text == "Database")
        {
            Panel2.Visible = false;
            Panel3.Visible = true;
        }
    }
    public void clear()
    {
        ddlproductversion.SelectedIndex = 0;
        txtcodetypename.Text = "";
        CheckBox1.Checked = false;
        CheckBox11.Checked = false;
        CheckBox12.Checked = false;
        lblmsg.Text = "";
        ddlproductversion.Enabled = true;
        ddlcodetypecategory.Enabled = true;

        btn_submit.Visible = true;
        btn_update.Visible = false;
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        fillgrid();
      
        Panel1.Visible = false;
        Panel4.Visible = true;
        ddlproductversion.Enabled = true;
        ddlcodetypecategory.Enabled = true;
        clear();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlcodetypecategory.SelectedValue == "1" || ddlcodetypecategory.SelectedValue == "3")
        {
            //ProductCodeDetailTbl_AddDelUpdtSelect
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("ProductCodeDetailTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            cmd.Parameters.AddWithValue("@ProductId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@CodeTypeName", txtcodetypename.Text);
            cmd.Parameters.AddWithValue("@AdditionalPageInserted", CheckBox1.Checked);
            cmd.Parameters.AddWithValue("@BusiwizSynchronization", CheckBox11.Checked);
            cmd.Parameters.AddWithValue("@CompanyDefaultData", CheckBox12.Checked);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);           
            cmd.ExecuteNonQuery();
            con.Close();
            //SqlCommand cmdsq = new SqlCommand(" Insert into ProductCodeDetailTbl(ProductId,CodeTypeName,AdditionalPageInserted,BusiwizSynchronization,CompanyDefaultData)Values('" + ddlproductversion.SelectedValue + "','" + txtcodetypename.Text + "','" + CheckBox1.Checked+ "',0,0)", con);
            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmdsq.ExecuteNonQuery();
            //con.Close();

            string strcln1 = "  select Max(id) as id  from ProductCodeDetailTbl ";
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);
            //
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
             cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text);
            cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductCodeDetailId", dtcln1.Rows[0][0].ToString());     
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);           
            cmd.ExecuteNonQuery();
            con.Close();
            //SqlCommand cmdsq1 = new SqlCommand("Insert into CodeTypeTbl(Name,CodeTypeCategoryId,ProductVersionId,ProductCodeDetailId)Values('" + txtcodetypename.Text + "','" + ddlcodetypecategory.SelectedValue + "','" + ddlproductversion.SelectedValue + "','" + dtcln1.Rows[0][0].ToString() + "')", con);
            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmdsq1.ExecuteNonQuery();
            //con.Close();
        }
        else if (ddlcodetypecategory.SelectedValue =="2")
        {
            string ccc = ViewState["changedata"].ToString();
            if (ccc == "1")
            {
                if (CheckBox11.Checked == true)
                {
                    SqlCommand sb = new SqlCommand("update ProductCodeDetailTbl set BusiwizSynchronization=0 where ProductId='" + ddlproductversion.SelectedValue + "' ", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    sb.ExecuteNonQuery();
                    con.Close();
                    
                }
                if (CheckBox12.Checked == true)
                {
                    SqlCommand sb = new SqlCommand("update ProductCodeDetailTbl set CompanyDefaultData=0 where ProductId='" + ddlproductversion.SelectedValue + "' ", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    sb.ExecuteNonQuery();
                    con.Close();
                }
                ViewState["changedata"] = "1";
            }           
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("ProductCodeDetailTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            cmd.Parameters.AddWithValue("@ProductId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@CodeTypeName", txtcodetypename.Text);
            cmd.Parameters.AddWithValue("@AdditionalPageInserted", CheckBox1.Checked);
            cmd.Parameters.AddWithValue("@BusiwizSynchronization", CheckBox11.Checked);
            cmd.Parameters.AddWithValue("@CompanyDefaultData", CheckBox12.Checked);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.ExecuteNonQuery();
            con.Close();
            string strcln1 = "  select Max(id) as id  from ProductCodeDetailTbl ";
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text + "MDF");
            cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductCodeDetailId", dtcln1.Rows[0][0].ToString());
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.ExecuteNonQuery();
            con.Close();
          
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text + "LDF");
            cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductCodeDetailId", dtcln1.Rows[0][0].ToString());
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.ExecuteNonQuery();
            con.Close();
            //SqlCommand cmdsq11 = new SqlCommand("Insert into CodeTypeTbl(Name,CodeTypeCategoryId,ProductVersionId,ProductCodeDetailId)Values('" + txtcodetypename.Text + "LDF','" + ddlcodetypecategory.SelectedValue + "','" + ddlproductversion.SelectedValue + "','" + dtcln1.Rows[0][0].ToString() + "')", con);
            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmdsq11.ExecuteNonQuery();
            //con.Close();
        }
        clear();
        fillgrid();
        Panel1.Visible = false;
        lblmsg.Visible = true;
        lblmsg.Text = "Record inserted successfully";
    }
    protected void CheckBox11_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox11.Checked == true)
        {
            CheckBox12.Checked = false;
            CheckBox1.Checked = false; 
            string strcln1 = "  select *   from ProductCodeDetailTbl where  BusiwizSynchronization=1 and ProductId='" + ddlproductversion.SelectedValue + "'";
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);
            if (dtcln1.Rows.Count > 0)
            {
                ModernpopSync.Show();
                Label6.Text = "For one product only one database can be set as busicontroller database, there is already another database code set as Busicontroller database " + dtcln1.Rows[0]["CodeTypeName"].ToString() + " would you wish to change the Busicontroller data base to " + txtcodetypename.Text + "";
            }
        }

    }

    protected void chk_Active_CheckedChanged(object sender, EventArgs e)
    {
        FillProductsearch();
    }
    protected void CheckBox12_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox12.Checked == true)
        {
            CheckBox11.Checked = false;
            CheckBox1.Checked = false;
            string strcln1 = "  select *   from ProductCodeDetailTbl where  CompanyDefaultData=1 and ProductId='" + ddlproductversion.SelectedValue + "'";
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);
            if (dtcln1.Rows.Count > 0)
            {
                ModernpopSync.Show();
                Label6.Text = "There is another database code type name " + dtcln1.Rows[0]["CodeTypeName"].ToString() + "  is selected as main applciation database, Would you like to change that to this new selction ?";
            }
        }
    }
    protected void FillProductsearch()
    {
        string active = "";
        if (chk_Active.Checked == true)
        {
            active = "and ProductDetail.Active='True' and VersionInfoMaster.Active='True' ";
        }
        string strcln = " SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductMaster.ProductId = dbo.ProductDetail.ProductId AND  dbo.VersionInfoMaster.VersionInfoName = dbo.ProductDetail.VersionNo where ClientMasterId=" + Session["ClientId"].ToString() + "  " + active + " order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "ProductName";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");
        ddlProductname.Items[0].Value = "0";
    }
    protected void fillgrid()
    {
        string str = "";
       
        if (ddlProductname.SelectedIndex > 0)
        {
            str += "and  dbo.VersionInfoMaster.VersionInfoId= '" + ddlProductname.SelectedValue + "'";
        }
        if (ddlstatus.SelectedItem.Text == "Active")
        {
            str += " and ProductCodeDetailTbl.Active='True'";
        }
        if (ddlstatus.SelectedItem.Text == "Inactive")
        {
            str += " and ProductCodeDetailTbl.Active='False'";
        }
         str += " and CodeTypeCategory.CodeMasterNo='" + ddlcodetypecategory0 .SelectedValue+ "'";
        //
         DataTable dtsvr = selectBZ(" SELECT DISTINCT dbo.ProductCodeDetailTbl.Id,dbo.ProductCodeDetailTbl.Active,dbo.ProductCodeDetailTbl.Id as vv, dbo.ProductCodeDetailTbl.CodeTypeName , dbo.ProductMaster.ProductName, dbo.ProductMaster.ProductName + ':' + dbo.VersionInfoMaster.VersionInfoName AS VersionInfoName, dbo.CodeTypeCategory.CodeTypeCategory, dbo.ProductCodeDetailTbl.AdditionalPageInserted, dbo.ProductCodeDetailTbl.BusiwizSynchronization, dbo.ProductCodeDetailTbl.CompanyDefaultData FROM            dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.CodeTypeTbl.ProductVersionId INNER JOIN dbo.ProductMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ProductCodeDetailTbl.Id = dbo.CodeTypeTbl.ProductCodeDetailId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' " + str + "");
       // SELECT dbo.ProductCodeDetailTbl.CodeTypeName, dbo.ProductMaster.ProductName + ':' + dbo.VersionInfoMaster.VersionInfoName AS VersionInfoName, dbo.CodeTypeCategory.CodeTypeCategory, dbo.ProductCodeDetailTbl.AdditionalPageInserted, dbo.ProductCodeDetailTbl.BusiwizSynchronization, dbo.ProductCodeDetailTbl.CompanyDefaultData, dbo.ProductCodeDetailTbl.Id AS vv FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId CROSS JOIN dbo.CodeTypeCategory CROSS JOIN dbo.ProductCodeDetailTbl
        GridView1.DataSource = dtsvr;
        GridView1.DataBind();
    }
 
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlcodetypecategory0_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
   
    protected void Button4_Click(object sender, EventArgs e)
    {
        CheckBox11.Checked = false;
        CheckBox12.Checked = false;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ViewState["changedata"] = "1";
    }

    
    protected void ImgBtn_EditGrig(object sender, ImageClickEventArgs e)
    {
        lblmsg.Visible = true;
        lblmsg.Text = "";
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label id = (Label)GridView1.Rows[j].FindControl("Label7");
        ViewState["id"] = id.Text;
        string strcln = "select * from ProductCodeDetailTbl where Id="+id.Text+"";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            FillProduct();
            ddlproductversion.SelectedValue = dtcln.Rows[0]["ProductId"].ToString();
            ddlproductversion.Enabled = false;
            txtcodetypename.Text = dtcln.Rows[0]["CodeTypeName"].ToString();
            CheckBox1.Checked = Convert.ToBoolean(dtcln.Rows[0]["AdditionalPageInserted"].ToString());
            CheckBox11.Checked = Convert.ToBoolean(dtcln.Rows[0]["BusiwizSynchronization"].ToString());
            CheckBox12.Checked = Convert.ToBoolean(dtcln.Rows[0]["CompanyDefaultData"].ToString());
            try
            {
                Chk_addactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
            }
            catch
            {
            }
        }

        string strcln1 = "select * from CodeTypeTbl where ProductCodeDetailId=" + id.Text + "";
        SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
        DataTable dtcln1 = new DataTable();
        SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
        adpcln1.Fill(dtcln1);
        if (dtcln1.Rows.Count > 0)
        {
            fillcodetypecategory();
            ddlcodetypecategory.SelectedValue = dtcln1.Rows[0]["CodeTypeCategoryId"].ToString();
            ddlcodetypecategory.Enabled = false;
            ddlcodetypecategory_SelectedIndexChanged(sender,e);
           
        }
        btn_update.Visible = true;
        Panel1.Visible = true;
        btn_submit.Visible = false;        
    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        lblmsg.Visible = true;
        lblmsg.Text = "";
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label id = (Label)GridView1.Rows[j].FindControl("Label7");
        ViewState["id"] = id.Text;
        string strcln = "select id from ProductCodeVersionDetail where CodeTypeID=" + id.Text + "";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";           
        }
        else
        {
            strcln = "select id from ProductCodeDatabasDetail where CodeTypeID=" + id.Text + "";
             cmdcln = new SqlCommand(strcln, con);
             dtcln = new DataTable();
             adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record."; 
            }
            else
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("ProductCodeDetailTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Delete");
                cmd.Parameters.AddWithValue("@ID", ViewState["id"]);
                cmd.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Record Deleted Sucessfully";
            }
        }
        
        fillgrid();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (ddlcodetypecategory.SelectedValue == "1" || ddlcodetypecategory.SelectedValue == "3")
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("ProductCodeDetailTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Update");
            cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@CodeTypeName", txtcodetypename.Text);
            cmd.Parameters.AddWithValue("@AdditionalPageInserted", CheckBox1.Checked);
            cmd.Parameters.AddWithValue("@BusiwizSynchronization", CheckBox11.Checked);
            cmd.Parameters.AddWithValue("@CompanyDefaultData", CheckBox12.Checked);
            cmd.Parameters.AddWithValue("@ProductId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.ExecuteNonQuery();
            con.Close();
            //SqlCommand cmdsq = new SqlCommand(" update ProductCodeDetailTbl set CodeTypeName='" + txtcodetypename.Text + "',AdditionalPageInserted='" + CheckBox1.Checked + "' where Id='" + ViewState["id"].ToString() + "'", con);
            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmdsq.ExecuteNonQuery();
            //con.Close();

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Update");
            cmd.Parameters.AddWithValue("@ProductCodeDetailId", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text );
            cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);          
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.ExecuteNonQuery();
            con.Close();
            //SqlCommand cmdsq1 = new SqlCommand("update CodeTypeTbl set Name='" + txtcodetypename.Text + "' where ProductCodeDetailId='" + ViewState["id"].ToString() + "' ", con);
            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmdsq1.ExecuteNonQuery();
            //con.Close();
        }
        else if  (ddlcodetypecategory.SelectedValue == "2")
        {
            string ccc = ViewState["changedata"].ToString();
            if (ccc == "1")
            {
                if (CheckBox11.Checked == true)
                {
                    SqlCommand sb = new SqlCommand("update ProductCodeDetailTbl set BusiwizSynchronization=0 where ProductId='" + ddlproductversion.SelectedValue + "' ", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    sb.ExecuteNonQuery();
                    con.Close();
                }
                if (CheckBox12.Checked == true)
                {
                    SqlCommand sb = new SqlCommand("update ProductCodeDetailTbl set CompanyDefaultData=0 where ProductId='" + ddlproductversion.SelectedValue + "' ", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    sb.ExecuteNonQuery();
                    con.Close();
                }
                ViewState["changedata"] = "1";
            }

            //SqlCommand cmdsq = new SqlCommand(" update  ProductCodeDetailTbl set CodeTypeName='" + txtcodetypename.Text + "' ,BusiwizSynchronization='" + CheckBox11.Checked + "',CompanyDefaultData='" + CheckBox12.Checked + "'  where Id='"+ViewState["id"].ToString()+"'", con);
            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmdsq.ExecuteNonQuery();
            //con.Close();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("ProductCodeDetailTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Update");
            cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@CodeTypeName", txtcodetypename.Text);
            cmd.Parameters.AddWithValue("@AdditionalPageInserted", CheckBox1.Checked);
            cmd.Parameters.AddWithValue("@BusiwizSynchronization", CheckBox11.Checked);
            cmd.Parameters.AddWithValue("@CompanyDefaultData", CheckBox12.Checked);
            cmd.Parameters.AddWithValue("@ProductId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.ExecuteNonQuery();
            con.Close();

            //-
            string strcln1 = "  select *  from CodeTypeTbl Where ProductCodeDetailId="+ ViewState["id"].ToString();
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "UpdateData");
            cmd.Parameters.AddWithValue("@Id", dtcln1.Rows[0][0].ToString());
            cmd.Parameters.AddWithValue("@ProductCodeDetailId", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text+"MDF");
            cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.ExecuteNonQuery();
            con.Close();

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd = new SqlCommand("CodeTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "UpdateData");
            cmd.Parameters.AddWithValue("@ID", dtcln1.Rows[1][0].ToString());
            cmd.Parameters.AddWithValue("@ProductCodeDetailId", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@Name", txtcodetypename.Text+"LDF");
            cmd.Parameters.AddWithValue("@CodeTypeCategoryId", ddlcodetypecategory.SelectedValue);
            cmd.Parameters.AddWithValue("@ProductVersionId", ddlproductversion.SelectedValue);
            cmd.Parameters.AddWithValue("@Active", Chk_addactive.Checked);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        clear();
        btn_update.Visible = false;
        btn_submit.Visible = true;
        fillgrid();
        Panel1.Visible = false;
        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully";

    }
}