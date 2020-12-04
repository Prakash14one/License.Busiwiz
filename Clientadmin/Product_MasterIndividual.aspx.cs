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

public partial class AddProduct : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection iofficecon = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
       // chkboxActiveDeactive.Checked=true;
        PageConn pgcon = new PageConn();
        iofficecon = pgcon.dynconn;
        lblmsg.Text = "";
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
           
            if (Session["Login"] != null)
            {
                if (Session["Login"].ToString() == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
          
            fillddlcategoryType();


            DDLBatchname();
            DDLsuppliername();
            DDLEmployeeName();
            DDLBrandname();

            DDLBatchnamefilter();
            DDLsuppliernamefilter();         
            DDLBrandnamefilter();
            DDLEmployeeNamefilter();
            DDLddlpriceplancateFilterfilter();
            FillGrid();
        }
    }
    protected void Clr()
    {
        ddlbrand.SelectedIndex = 0;
        ddlbatch.SelectedIndex = 0;
        ddlsupplier.SelectedIndex = 0;
         ddlbuildempl.SelectedIndex= 0;
         ddlQltyemp.SelectedIndex =0;
        txtindividual.Text="";
         txtdesc.Text="";
         txtsrno.Text="";
         txtmodelno.Text="";
        txtStartdate.Text="";
       txtEndDate.Text="";
        txtDocumentaionURL.Text="";
         txtspecification.Text="";
        txtsize.Text="";
        txtvender.Text="";       
        chkboxActiveDeactive.Checked=false;
        pnladdnew.Visible = false;
        btnSubmit.Text = "Submit";
        addnewpanel.Visible = true;  
    }
    public void DDLBatchname()
    {
        string finalstr = "Select * from Product_BatchMaster Where BatchName !='' and Active='1' Order By BatchName ";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlbatch.DataSource = dtcln;
        ddlbatch.DataValueField = "ProductBAtchMasterID";
        ddlbatch.DataTextField = "BatchName";
        ddlbatch.DataBind();
        ddlbatch.Items.Insert(0, "--Select--");
        ddlbatch.Items[0].Value = "0";

    }
    protected void fillddlcategoryType()
    {
        ddlcategoryType.Items.Clear();
        string strcln = " SELECT distinct * FROM PriceplanCategoryType where Active='1' and id IN('13','14','12') order  by CategoryType";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlcategoryType.DataSource = dtcln;
        ddlcategoryType.DataTextField = "CategoryType";
        ddlcategoryType.DataValueField = "ID";
        ddlcategoryType.DataBind();
        ddlcategoryType.Items.Insert(0, "-Select-");
        ddlcategoryType.Items[0].Value = "0";
    }

    public void DDLsuppliername()
    {
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();      

        string finalstr = "Select * from SupplierMasterTbl where CompanyName!='' Order By CompanyName";
        finalstr = " SELECT DISTINCT dbo.Party_master.Compname ,dbo.Party_master.Compname + ':'+ dbo.Party_master.Contactperson as CompanyName, dbo.PartyTypeCategoryMasterTbl.PartyCategoryName, dbo.PartytTypeMaster.PartType, dbo.Party_master.PartyID FROM dbo.Party_master INNER JOIN dbo.PartytTypeMaster ON dbo.Party_master.PartyTypeId = dbo.PartytTypeMaster.PartyTypeId INNER JOIN dbo.PartyTypeCategoryMasterTbl ON dbo.PartytTypeMaster.PartyCategoryId = dbo.PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId WHERE  dbo.Party_master.Whid='"+  dteeed.Rows[0]["Whid"] +"' and (dbo.PartytTypeMaster.PartType = 'Vendor')  and dbo.PartyTypeCategoryMasterTbl.Active='1'  Order By dbo.Party_master.Compname ";

        SqlCommand cmdcln = new SqlCommand(finalstr, iofficecon);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlsupplier.DataSource = dtcln;
        ddlsupplier.DataValueField = "PartyID";
        ddlsupplier.DataTextField = "CompanyName";
        ddlsupplier.DataBind();
        ddlsupplier.Items.Insert(0, "--Select--");
        ddlsupplier.Items[0].Value = "0";

    } 
    public void DDLBrandname()
    {
        string finalstr = "Select * from BrandMasterTbl Where Name !='' and Active='1'  Order By Name";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlbrand.DataSource = dtcln;
        ddlbrand.DataValueField = "ID";
        ddlbrand.DataTextField = "Name";
        ddlbrand.DataBind();
        ddlbrand.Items.Insert(0, "--Select--");
        ddlbrand.Items[0].Value = "0";
    }
    protected void DDLEmployeeName()
    {
        string strcln = " SELECT DISTINCT dbo.EmployeeMaster.EmployeeMasterID, dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid ,  dbo.EmployeeMaster.DeptID FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1  ";
        strcln = " SELECT DISTINCT dbo.Party_master.Compname as EmployeeName ,  dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id FROM dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id INNER JOIN dbo.Party_master ON dbo.EmployeeMaster.PartyID = dbo.Party_master.PartyID where  dbo.DepartmentmasterMNC.Active=1  and EmployeeMaster.Active=1 ";
        SqlCommand cmdcln = new SqlCommand(strcln, iofficecon);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlQltyemp.DataSource = dtcln;
        ddlQltyemp.DataValueField = "License_Emp_id";
        ddlQltyemp.DataTextField = "EmployeeName";
        ddlQltyemp.DataBind();
        ddlQltyemp.Items.Insert(0, "---Select All---");

        ddlbuildempl.DataSource = dtcln;
        ddlbuildempl.DataValueField = "License_Emp_id";
        ddlbuildempl.DataTextField = "EmployeeName";
        ddlbuildempl.DataBind();
        ddlbuildempl.Items.Insert(0, "---Select All---");
    }
    //-Filter//---------------------------------------------------------------------------------------------------------
    public void DDLBatchnamefilter()
    {
        string finalstr = "Select * from Product_BatchMaster Where BatchName !='' and Active='1'  Order By BatchName";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlbatchfilter.DataSource = dtcln;
        ddlbatchfilter.DataValueField = "ProductBAtchMasterID";
        ddlbatchfilter.DataTextField = "BatchName";
        ddlbatchfilter.DataBind();
        ddlbatchfilter.Items.Insert(0, "--Select--");
        ddlbatchfilter.Items[0].Value = "0";

    }
    public void DDLsuppliernamefilter()
    {
        string finalstr = "Select * from SupplierMasterTbl where CompanyName!='' Order By CompanyName";
        finalstr = " SELECT DISTINCT dbo.Party_master.Compname , dbo.Party_master.Compname + ':'+ dbo.Party_master.Contactperson as CompanyName, dbo.PartyTypeCategoryMasterTbl.PartyCategoryName, dbo.PartytTypeMaster.PartType, dbo.Party_master.PartyID FROM dbo.Party_master INNER JOIN dbo.PartytTypeMaster ON dbo.Party_master.PartyTypeId = dbo.PartytTypeMaster.PartyTypeId INNER JOIN dbo.PartyTypeCategoryMasterTbl ON dbo.PartytTypeMaster.PartyCategoryId = dbo.PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId WHERE        (dbo.PartytTypeMaster.PartType = 'Vendor') and dbo.PartyTypeCategoryMasterTbl.Active='1' Order By dbo.Party_master.Compname ";
        SqlCommand cmdcln = new SqlCommand(finalstr, iofficecon);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlsupplierfilter.DataSource = dtcln;
        ddlsupplierfilter.DataValueField = "PartyID";
        ddlsupplierfilter.DataTextField = "CompanyName";
        ddlsupplierfilter.DataBind();
        ddlsupplierfilter.Items.Insert(0, "--Select--");
        ddlsupplierfilter.Items[0].Value = "0";
    }  
    public void DDLBrandnamefilter()
    {
        string finalstr = "Select * from BrandMasterTbl Where Name !='' and Active='1' Order By Name";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlBrandNamefilter.DataSource = dtcln;
        ddlBrandNamefilter.DataValueField = "ID";
        ddlBrandNamefilter.DataTextField = "Name";
        ddlBrandNamefilter.DataBind();
        ddlBrandNamefilter.Items.Insert(0, "--Select--");
        ddlBrandNamefilter.Items[0].Value = "0";
    }
    protected void DDLEmployeeNamefilter()
    {

        string data = "select * from EmployeeMaster Where Name !='' and Active='1' order by Name ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
       
            ddlbuilderempfilter.DataSource = dt;
            ddlbuilderempfilter.DataTextField = "Name";
            ddlbuilderempfilter.DataValueField = "Id";
            ddlbuilderempfilter.DataBind();

            ddlqlycheckfilter.DataSource = dt;
            ddlqlycheckfilter.DataTextField = "Name";
            ddlqlycheckfilter.DataValueField = "Id";
            ddlqlycheckfilter.DataBind();
       
        ddlbuilderempfilter.Items.Insert(0, "--Select--");
        ddlbuilderempfilter.Items[0].Value = "0";

        ddlqlycheckfilter.Items.Insert(0, "--Select--");
        ddlqlycheckfilter.Items[0].Value = "0";

    }
    public void DDLddlpriceplancateFilterfilter()
    {
        string finalstr = " SELECT distinct * FROM PriceplanCategoryType where Active='1' and id IN('13','14','15') order  by CategoryType";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlpriceplancateFilter.DataSource = dtcln;
        ddlpriceplancateFilter.DataValueField = "ID";
        ddlpriceplancateFilter.DataTextField = "CategoryType";
        ddlpriceplancateFilter.DataBind();
        ddlpriceplancateFilter.Items.Insert(0, "--Select--");
        ddlpriceplancateFilter.Items[0].Value = "0";
    }
    
    protected void FillGrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string active;
        string deactive;

        string strcln = " SELECT  LEFT(dbo.Product_MasterIndividual.Specification, 100) AS SpecificationSort ,    dbo.PriceplanCategoryType.CategoryType, LEFT(dbo.Product_MasterIndividual.Product_desc, 100) AS Product_descSort  , " +
                        "  dbo.Product_MasterIndividual.ID, dbo.Product_MasterIndividual.ProductBAtchMasterID, dbo.Product_MasterIndividual.SupplierID, dbo.Product_MasterIndividual.BuilderEmpId, dbo.Product_MasterIndividual.QltyCheckerEmpId, dbo.Product_MasterIndividual.BrandID, dbo.Product_MasterIndividual.ProductName, dbo.Product_MasterIndividual.Product_desc, dbo.Product_MasterIndividual.SrNumber, dbo.Product_MasterIndividual.ModelNumber, dbo.Product_MasterIndividual.StartDate, dbo.Product_MasterIndividual.Retiredate, dbo.Product_MasterIndividual.DocumentationURL, dbo.Product_MasterIndividual.Specification, dbo.Product_MasterIndividual.Size, dbo.Product_MasterIndividual.VendorProductPageURL, dbo.Product_MasterIndividual.Active , dbo.Product_BatchMaster.BatchName " +
                        "  FROM dbo.Product_MasterIndividual INNER JOIN dbo.Product_BatchMaster ON dbo.Product_MasterIndividual.ProductBAtchMasterID = dbo.Product_BatchMaster.ProductBAtchMasterID  LEFT OUTER JOIN dbo.BrandMasterTbl ON dbo.Product_MasterIndividual.BrandID = dbo.BrandMasterTbl.ID LEFT OUTER JOIN dbo.PriceplanCategoryType ON dbo.Product_MasterIndividual.PriceplanCategoryTypeID = dbo.PriceplanCategoryType.ID Where dbo.Product_MasterIndividual.ProductName !='' ";
        
        if (ddlstatus.SelectedItem.Text == "Active")
        {
            active = " and Product_MasterIndividual.Active='True'";
            strcln += active;
        }
        else if (ddlstatus.SelectedItem.Text == "Deactive")
        {
            deactive = " and Product_MasterIndividual.Active='False'";
            strcln += deactive;
        }
        if (ddlbatch.SelectedIndex > 0)
        {
            strcln += " and Product_MasterIndividual.ProductBAtchMasterID=" + ddlbatch.SelectedValue + "";
        }
        if (ddlpriceplancateFilter.SelectedIndex > 0)
        {
            strcln += " and Product_MasterIndividual.PriceplanCategoryTypeID=" + ddlpriceplancateFilter.SelectedValue + "";
        }        
        if (ddlsupplierfilter.SelectedIndex > 0)
        {
            strcln += " and Product_MasterIndividual.SupplierID=" + ddlsupplierfilter.SelectedValue + "";
        }
        if (ddlbatchfilter.SelectedIndex > 0)
        {
            strcln += " and Product_MasterIndividual.BrandID=" + ddlbatchfilter.SelectedValue + "";
        }
        if (ddlqlycheckfilter.SelectedIndex > 0)
        {
            strcln += " and  dbo.Product_MasterIndividual.QltyCheckerEmpId=" + ddlqlycheckfilter.SelectedValue + "";
        }
        if (ddlbuilderempfilter.SelectedIndex > 0)
        {
            strcln += " and Product_MasterIndividual.BuilderEmpId=" + ddlbuilderempfilter.SelectedValue + "";
        }
     
        strcln += "Order By ID Desc";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView1.DataSource = dtcln;
        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();
    }
    //-----------------------------------------------------------------/*/*/*/*******************************************//////////////
    protected void BtnGo_Click(object sender, EventArgs e)
    {
       
            FillGrid();
       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {      
               
        try
        {
            if (btnSubmit.Text == "Update")
            {

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Product_MasterIndividual_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "Update");
                cmd.Parameters.AddWithValue("@ID", ViewState["ID"]);
                cmd.Parameters.AddWithValue("@ProductBAtchMasterID",ddlbatch.SelectedValue);
                 cmd.Parameters.AddWithValue("@SupplierID", ddlsupplier.SelectedValue);
                cmd.Parameters.AddWithValue("@BuilderEmpId", ddlbuildempl.SelectedValue);
                cmd.Parameters.AddWithValue("@QltyCheckerEmpId", ddlQltyemp.SelectedValue);
                cmd.Parameters.AddWithValue("@BrandID", ddlbrand.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductName", txtindividual.Text);
                cmd.Parameters.AddWithValue("@Product_desc", txtdesc.Text);
                cmd.Parameters.AddWithValue("@SrNumber", txtsrno.Text);
                cmd.Parameters.AddWithValue("@ModelNumber", txtmodelno.Text);
                cmd.Parameters.AddWithValue("@StartDate", txtStartdate.Text);
                cmd.Parameters.AddWithValue("@Retiredate", txtEndDate.Text);
                cmd.Parameters.AddWithValue("@DocumentationURL", txtDocumentaionURL.Text);
                cmd.Parameters.AddWithValue("@Specification", txtspecification.Text);
                cmd.Parameters.AddWithValue("@Size", txtsize.Text);
                cmd.Parameters.AddWithValue("@VendorProductPageURL", txtvender.Text);                                
                cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
                cmd.Parameters.AddWithValue("@PriceplanCategoryTypeID", ddlcategoryType.SelectedValue);
                
                cmd.ExecuteNonQuery();
                con.Close();
              
                //-------------------------------------------------
               
                //---------------------------------------------------
                lblmsg.Visible = true;
                lblmsg.Text = "Record Updated successfully";
                Clr();
                FillGrid();
                        
            }
            else
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Product_MasterIndividual_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                cmd.Parameters.AddWithValue("@ProductBAtchMasterID", ddlbatch.SelectedValue);
                cmd.Parameters.AddWithValue("@SupplierID", ddlsupplier.SelectedValue);
                cmd.Parameters.AddWithValue("@BuilderEmpId", ddlbuildempl.SelectedValue);
                cmd.Parameters.AddWithValue("@QltyCheckerEmpId", ddlQltyemp.SelectedValue);
                cmd.Parameters.AddWithValue("@BrandID", ddlbrand.SelectedValue);
               
                cmd.Parameters.AddWithValue("@ProductName", txtindividual.Text);
                cmd.Parameters.AddWithValue("@Product_desc", txtdesc.Text);
                cmd.Parameters.AddWithValue("@SrNumber", txtsrno.Text);
                cmd.Parameters.AddWithValue("@ModelNumber", txtmodelno.Text);
                cmd.Parameters.AddWithValue("@StartDate", txtStartdate.Text);
                cmd.Parameters.AddWithValue("@Retiredate", txtEndDate.Text);
                cmd.Parameters.AddWithValue("@DocumentationURL", txtDocumentaionURL.Text);
                cmd.Parameters.AddWithValue("@Specification", txtspecification.Text);
                cmd.Parameters.AddWithValue("@Size", txtsize.Text);
                cmd.Parameters.AddWithValue("@VendorProductPageURL", txtvender.Text);
                cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
                cmd.Parameters.AddWithValue("@PriceplanCategoryTypeID", ddlcategoryType.SelectedValue);
                cmd.ExecuteNonQuery();
                con.Close();
                //-------------------------------------------------------             
                //------------------------------------------------------------------------------------

                lblmsg.Visible = true;
                lblmsg.Text = "Record Inserted Successfully";
                Clr();
                FillGrid();
                 
            }
        }
        catch (Exception eerr)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error : " + eerr.Message;

        }



    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            addnewpanel.Visible = true;
            pnladdnew.Visible = true;
            Label19.Text = "Edit Product Master Individual";
            lblmsg.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());

            string strcln = "Select * From Product_MasterIndividual Where ID='" + i.ToString() + "'";
                hdnProductDetailId.Value = i.ToString();

                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);               
                if (dtcln.Rows.Count > 0)
                {
                    ViewState["ID"] = dtcln.Rows[0]["ID"].ToString();
                    hdnProductId.Value = dtcln.Rows[0]["ID"].ToString();
                   ddlbatch.SelectedValue = dtcln.Rows[0]["ProductBAtchMasterID"].ToString();
                   try
                   {
                       DDLsuppliername();
                       ddlsupplier.SelectedValue = dtcln.Rows[0]["SupplierID"].ToString();                  
                   }
                   catch (Exception ex)
                   {
                   }
                   try
                   {
                       DDLEmployeeName();
                       ddlbuildempl.SelectedValue = dtcln.Rows[0]["BuilderEmpId"].ToString();
                       ddlQltyemp.SelectedValue = dtcln.Rows[0]["QltyCheckerEmpId"].ToString();
                   }
                   catch (Exception ex)
                   {
                   }
                   try
                   {
                       DDLBrandname();
                       ddlbrand.SelectedValue = dtcln.Rows[0]["BrandID"].ToString();
                   }
                   catch (Exception ex)
                   {
                   }                  

                    txtindividual.Text   = dtcln.Rows[0]["ProductName"].ToString();
                    txtdesc.Text = dtcln.Rows[0]["Product_desc"].ToString();
                    txtsrno.Text = dtcln.Rows[0]["SrNumber"].ToString();
                    txtmodelno.Text = dtcln.Rows[0]["ModelNumber"].ToString();
                    txtStartdate.Text = dtcln.Rows[0]["StartDate"].ToString();
                    txtEndDate.Text = dtcln.Rows[0]["Retiredate"].ToString();
                    txtDocumentaionURL.Text = dtcln.Rows[0]["DocumentationURL"].ToString();
                    txtspecification.Text = dtcln.Rows[0]["Specification"].ToString();
                    txtsize.Text = dtcln.Rows[0]["Size"].ToString();
                    txtvender.Text = dtcln.Rows[0]["VendorProductPageURL"].ToString();
                    try
                    {
                        chkboxActiveDeactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        ddlcategoryType.SelectedValue = dtcln.Rows[0]["PriceplanCategoryTypeID"].ToString(); 
                    }
                    catch (Exception ex)
                    {
                    }
                    //-----------Discount-------------
                   
                    //------------------------
                    btnSubmit.Text = "Update";                   
            }
        }
        if (e.CommandName == "Delete")
        {            
            int mm = Convert.ToInt32(e.CommandArgument);
            string finalstr = "Select * from  ServerMasterTbl where ProductMasterindividualID='" + mm + "'";
            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            DataTable dtcln = new DataTable();
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count == 0)
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Product_MasterIndividual_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Delete");
                cmd.Parameters.AddWithValue("@ID", mm);
                cmd.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;
                FillGrid();
                Clr();
                lblmsg.Text = "Record deleted successfully";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
            }
            
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
   
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink");    
        ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>OpenNewWin('../"+"http://"+"" + ctrl.Text + "')</script>");
         
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {        
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;      
        int rinrow = row.RowIndex;
        Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");       
        ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>OpenNewWin('../"+"http://"+ "" + ctrl.Text + "')</script>");
        //"http://safestmall.indiaauthentic.com/ShoppingCart/Default.aspx?Cid=111&Wid=4" target="_blank" >
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

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        btnSubmit.Text = "Submit";
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
      
        Label19.Text = " ";
    }
   
  

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[7].Visible = false;
            }
       
        }
        else
        {
            Button3.Text = "Printable Version";
            Button4.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }
         
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        pnladdnew.Visible = true;
        addnewpanel.Visible = false;
        lblmsg.Text = "";
        Label19.Text = "Add New Product Master Individual";
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {


    }

    protected void Button6_Click(object sender, EventArgs e)
    {
      
    }

    //-------

    protected void btnUpload_Click1(object sender, EventArgs e)
    {
        if (txtDocumentaionURL.Text == "")
        {
            lblmsg.Text = "  ";
        }
        if (FileUpload1.HasFile)
        {           
            lblmsg.Text = "";    
                lblmsg.Text = "";
                string filename = Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~\\images\\") + filename);             
                txtDocumentaionURL.Text = "http://license.busiwiz.com/images/" + filename + "";           
        }
    }
    protected void btnadd_Click1(object sender, EventArgs e)
    {
        if (btnadd.Text == "Add")
        {
            FileUpload1.Visible = true;
            btnUpload.Visible = true;
            btnadd.Text = "Close";
        }
        else if (btnadd.Text == "Close")
        {
            btnadd.Text = "Add";
            FileUpload1.Visible = false;
            btnUpload.Visible = false; 
        }
    }


    protected void btnUpload_Click2(object sender, EventArgs e)
    {
        if (txtDocumentaionURL.Text == "")
        {
           
        }
        if (FileUpload2.HasFile)
        {

            string filename = Path.GetFileName(FileUpload2.FileName);
            FileUpload2.SaveAs(Server.MapPath("~\\images\\") + filename);
            txtvender.Text = "http://license.busiwiz.com/images/" + filename + "";
        }
    }
    protected void btnadd_Click2(object sender, EventArgs e)
    {
        if (btnadd2.Text == "Add")
        {
            FileUpload2.Visible = true;
            btnUpload2.Visible = true;
            btnadd2.Text = "Close";
        }
        else if (btnadd2.Text == "Close")
        {
            btnadd2.Text = "Add";
            FileUpload2.Visible = false;
            btnUpload2.Visible = false;
        }
    }

    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtEndDate.Text) < Convert.ToDateTime(txtStartdate.Text))
        {
            lblenddateerror.Text = "The end Date must be greater than the start date.";
            txtEndDate.Text = "";
        }
        else
        {
            lblenddateerror.Text = "";
        }
    }
} 
