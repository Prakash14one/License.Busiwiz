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

public partial class AddProduct : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection iofficecon = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        iofficecon = pgcon.dynconn;
       // chkboxActiveDeactive.Checked=true;
        lblmsg.Text = "";
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
           
            if (Session["Login"] != null)
            {
                if (Session["Login"].ToString() == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            DDLProductModelName();
            DDLsuppliername();
            DDLddlcomponent();

            DDLProductModelNamefilter();
            DDLsuppliernamefilter();
            DDLddlcomponentfilter();
            FillGrid();
        }
    }
    protected void Clr()
    {
        ddlModel.SelectedValue = "0";
        ddlsupplier.SelectedValue = "0";
        ddlcomponent.SelectedValue = "0";        
        txtbanchname.Text = "";
        txtbanchno.Text ="";
        txtinvoiceno.Text = "";
        txtqtypurchased.Text ="";
        txtmarkupper.Text = "";
        txttotalcost.Text = "";
        txttotalcostperunit.Text ="";
        txtspecification.Text = "";
        txtdescription.Text = "";
        chkboxActiveDeactive.Checked=false;

        pnladdnew.Visible = false;


        txtEndDate.Text = "";
        txtCurrency.Text = "";
        txtStartdate.Text = "";
        txtSalePrice.Text = "";        
        txtCurrency.Text = "";        
        txtSalePrice.Text = "";
    }
    public void DDLsuppliername()
    {
        string finalstr = "Select * from SupplierMasterTbl where CompanyName!='' Order By CompanyName";
        finalstr = " SELECT DISTINCT dbo.Party_master.Compname , dbo.Party_master.Compname + ':'+ dbo.Party_master.Contactperson as CompanyName, dbo.PartyTypeCategoryMasterTbl.PartyCategoryName, dbo.PartytTypeMaster.PartType, dbo.Party_master.PartyID FROM dbo.Party_master INNER JOIN dbo.PartytTypeMaster ON dbo.Party_master.PartyTypeId = dbo.PartytTypeMaster.PartyTypeId INNER JOIN dbo.PartyTypeCategoryMasterTbl ON dbo.PartytTypeMaster.PartyCategoryId = dbo.PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId WHERE        (dbo.PartytTypeMaster.PartType = 'Vendor') and dbo.PartyTypeCategoryMasterTbl.Active='1'  Order By dbo.Party_master.Compname ";
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
    public void DDLddlcomponent()
    {
        string finalstr = "Select * from ProductComponentMasterTbl Where ComponentName !=''  Order By ComponentName";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlcomponent.DataSource = dtcln;
        ddlcomponent.DataValueField = "ID";
        ddlcomponent.DataTextField = "ComponentName";
        ddlcomponent.DataBind();
        ddlcomponent.Items.Insert(0, "--Select--");
        ddlcomponent.Items[0].Value = "0";
       
    }
    protected void DDLProductModelName()
    {

        string data = "select * from Product_Model Where ProductModelName !='' and Active='1' order by ProductModelName ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlModel.DataSource = dt;
            ddlModel.DataTextField = "ProductModelName";
            ddlModel.DataValueField = "ID";
            ddlModel.DataBind();
        }
        ddlModel.Items.Insert(0, "--Select--");
        ddlModel.Items[0].Value = "0";
    }


    public void DDLsuppliernamefilter()
    {
        string finalstr = "Select * from SupplierMasterTbl Where CompanyName !='' Order By CompanyName ";
        finalstr = " SELECT DISTINCT  dbo.Party_master.Compname , dbo.Party_master.Compname + ':'+ dbo.Party_master.Contactperson as CompanyName, dbo.PartyTypeCategoryMasterTbl.PartyCategoryName, dbo.PartytTypeMaster.PartType, dbo.Party_master.PartyID FROM dbo.Party_master INNER JOIN dbo.PartytTypeMaster ON dbo.Party_master.PartyTypeId = dbo.PartytTypeMaster.PartyTypeId INNER JOIN dbo.PartyTypeCategoryMasterTbl ON dbo.PartytTypeMaster.PartyCategoryId = dbo.PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId WHERE        (dbo.PartytTypeMaster.PartType = 'Vendor') and dbo.PartyTypeCategoryMasterTbl.Active='1' Order By dbo.Party_master.Compname ";
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
    public void DDLddlcomponentfilter()
    {
        string finalstr = "Select * from ProductComponentMasterTbl Where ComponentName !='' and Active='1' Order By ComponentName";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlcomponentfilter.DataSource = dtcln;
        ddlcomponentfilter.DataValueField = "ID";
        ddlcomponentfilter.DataTextField = "ComponentName";
        ddlcomponentfilter.DataBind();
        ddlcomponentfilter.Items.Insert(0, "--Select--");
        ddlcomponentfilter.Items[0].Value = "0";
       
    }
    protected void DDLProductModelNamefilter()
    {

        string data = "select * from Product_Model Where ProductModelName !='' and Active='1' order by ProductModelName ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlproductmodelfilter.DataSource = dt;
            ddlproductmodelfilter.DataTextField = "ProductModelName";
            ddlproductmodelfilter.DataValueField = "ID";
            ddlproductmodelfilter.DataBind();
        }
        ddlproductmodelfilter.Items.Insert(0, "--Select--");
        ddlproductmodelfilter.Items[0].Value = "0";
    }
    
    


    protected void FillGrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string active;
        string deactive;

        string strcln = " select *,LEFT(dbo.Product_BatchMaster.BatchDesc, 100) AS BatchDescsort from Product_BatchMaster where  BatchName!=''  ";
        
        if (ddlstatus.SelectedItem.Text == "Active")
        {
            active = " and Product_BatchMaster.Active='True'";
            strcln += active;
        }
        else if (ddlstatus.SelectedItem.Text == "Deactive")
        {
            deactive = " and Product_BatchMaster.Active='False'";
            strcln += deactive;
        }
        if (ddlsupplierfilter.SelectedIndex > 0)
        {
            strcln += " and Product_BatchMaster.ProductComponentMasterTblID=" + ddlsupplier.SelectedValue + "";
        }
        if (ddlcomponentfilter.SelectedIndex > 0)
        {
            strcln += " and Product_BatchMaster.SupplierID=" + ddlcomponentfilter.SelectedValue + "";
        }
        if (ddlproductmodelfilter.SelectedIndex > 0)
        {
            strcln += " and Product_BatchMaster.ProductModelID=" + ddlproductmodelfilter.SelectedValue + "";
        }
        strcln += "Order By ProductBAtchMasterID Desc";
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
                SqlCommand cmd = new SqlCommand("Product_BatchMaster_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "Update");
                cmd.Parameters.AddWithValue("@ProductBAtchMasterID", ViewState["ID"]);
                cmd.Parameters.AddWithValue("@ProductModelID", ddlModel.SelectedValue);
                 cmd.Parameters.AddWithValue("@SupplierID", ddlsupplier.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductComponentMasterTblID", ddlcomponent.SelectedValue );
                cmd.Parameters.AddWithValue("@BatchName", txtbanchname.Text);
                cmd.Parameters.AddWithValue("@BatchNo", txtbanchno.Text);
                cmd.Parameters.AddWithValue("@PurchaseInvoiceNo", txtinvoiceno.Text);
                cmd.Parameters.AddWithValue("@QtyPurchased", txtqtypurchased.Text);
                cmd.Parameters.AddWithValue("@MarkUpPercentageOverEffectiveCost", txtmarkupper.Text);
                cmd.Parameters.AddWithValue("@TotalCost", txttotalcost.Text);
                cmd.Parameters.AddWithValue("@CostPerUnit", txttotalcostperunit.Text);
                cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
                cmd.Parameters.AddWithValue("@BAtchSpecification", txtspecification.Text);
                cmd.Parameters.AddWithValue("@BatchDesc", txtdescription.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                //----------------------------------------------------
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd = new SqlCommand("Product_BatchVolumeDicount_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Update");
                cmd.Parameters.AddWithValue("@ProductBAtchMasterID", ViewState["ID"]);
                cmd.Parameters.AddWithValue("@VolumeDiscountEligibleMinQty", txtSalePrice.Text);
                cmd.Parameters.AddWithValue("@VolumeDiscountAmtPerUnit", txtCurrency.Text);
                cmd.Parameters.AddWithValue("@StartDate", txtStartdate.Text);
                cmd.Parameters.AddWithValue("@EndDate", txtEndDate.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                //----------------------------------------------------
                //-------------------------------------------------
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd = new SqlCommand("Product_BtchPriceSalePriceDetail_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Update");              
                cmd.Parameters.AddWithValue("@ProductBAtchMasterID", ViewState["ID"]);
                cmd.Parameters.AddWithValue("@SalePrice", txtSalePrice.Text);
                cmd.Parameters.AddWithValue("@Currency", txtCurrency.Text);
                cmd.Parameters.AddWithValue("@StartDate", txtStartdate.Text);
                cmd.Parameters.AddWithValue("@EndDate", txtEndDate.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                //---------------------------------------------------
                lblmsg.Visible = true;
                lblmsg.Text = "Record insert successfully";
                Clr();
                FillGrid();
                        
            }
            else
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Product_BatchMaster_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "Insert");                
                cmd.Parameters.AddWithValue("@ProductModelID", ddlModel.SelectedValue);
                cmd.Parameters.AddWithValue("@SupplierID", ddlsupplier.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductComponentMasterTblID", ddlcomponent.SelectedValue);
                cmd.Parameters.AddWithValue("@BatchName", txtbanchname.Text);
                cmd.Parameters.AddWithValue("@BatchNo", txtbanchno.Text);
                cmd.Parameters.AddWithValue("@PurchaseInvoiceNo", txtinvoiceno.Text);
                cmd.Parameters.AddWithValue("@QtyPurchased", txtqtypurchased.Text);
                cmd.Parameters.AddWithValue("@MarkUpPercentageOverEffectiveCost", txtmarkupper.Text);
                cmd.Parameters.AddWithValue("@TotalCost", txttotalcost.Text);
                cmd.Parameters.AddWithValue("@CostPerUnit", txttotalcostperunit.Text);
                cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
                cmd.Parameters.AddWithValue("@BAtchSpecification", txtspecification.Text);
                cmd.Parameters.AddWithValue("@BatchDesc", txtdescription.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                //-------------------------------------------------------
                string strmax = " Select Max(ProductBAtchMasterID) as ID from Product_BatchMaster";
                SqlCommand cmdmax = new SqlCommand(strmax, con);
                DataTable dtmax = new DataTable();
                SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
                adpmax.Fill(dtmax);
                string id = "";
                if (dtmax.Rows.Count > 0)
                {
                    id = dtmax.Rows[0]["Id"].ToString();
                }
                //------------------------------------------------------------------------------------
                //------------------------------------------------------------------------------------
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd = new SqlCommand("Product_BatchVolumeDicount_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                cmd.Parameters.AddWithValue("@ProductBAtchMasterID", id);
                cmd.Parameters.AddWithValue("@VolumeDiscountEligibleMinQty", txtSalePrice.Text);
                cmd.Parameters.AddWithValue("@VolumeDiscountAmtPerUnit", txtCurrency.Text);
                cmd.Parameters.AddWithValue("@StartDate", txtStartdate.Text);
                cmd.Parameters.AddWithValue("@EndDate", txtEndDate.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                //--------------------------------------------
                //-----------------------------------------------
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                 cmd = new SqlCommand("Product_BtchPriceSalePriceDetail_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                cmd.Parameters.AddWithValue("@ProductBAtchMasterID", id);
                cmd.Parameters.AddWithValue("@SalePrice", txtSalePrice.Text);
                cmd.Parameters.AddWithValue("@Currency", txtCurrency.Text);
                cmd.Parameters.AddWithValue("@StartDate", txtStartdate.Text);
                cmd.Parameters.AddWithValue("@EndDate", txtEndDate.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                //-------------------------------------------------

                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
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
            Label19.Text = "Edit Product Batch Master";
            lblmsg.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());

            string strcln = "Select * From Product_BatchMaster Where ProductBAtchMasterID='" + i.ToString() + "'";
                hdnProductDetailId.Value = i.ToString();

                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);               
                if (dtcln.Rows.Count > 0)
                {
                    ViewState["ID"] = dtcln.Rows[0]["ProductBAtchMasterID"].ToString();
                    hdnProductId.Value = dtcln.Rows[0]["ProductBAtchMasterID"].ToString();
                    ddlModel.SelectedValue = dtcln.Rows[0]["ProductModelID"].ToString();
                    ddlsupplier.SelectedValue = dtcln.Rows[0]["SupplierID"].ToString();
                    ddlcomponent.SelectedValue = dtcln.Rows[0]["ProductComponentMasterTblID"].ToString();
                    txtbanchname.Text = dtcln.Rows[0]["BatchName"].ToString();
                    txtbanchno.Text = dtcln.Rows[0]["BatchNo"].ToString();
                    txtinvoiceno.Text = dtcln.Rows[0]["PurchaseInvoiceNo"].ToString();
                    txtqtypurchased.Text = dtcln.Rows[0]["QtyPurchased"].ToString();
                    txtmarkupper.Text = dtcln.Rows[0]["MarkUpPercentageOverEffectiveCost"].ToString();
                    txttotalcost.Text = dtcln.Rows[0]["TotalCost"].ToString();
                    txttotalcostperunit.Text = dtcln.Rows[0]["CostPerUnit"].ToString();
                    txtspecification.Text = dtcln.Rows[0]["BAtchSpecification"].ToString();
                    txtdescription.Text = dtcln.Rows[0]["BatchDesc"].ToString();                    
                    try
                    {
                        chkboxActiveDeactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
                    }
                    catch (Exception ex)
                    {
                    }
                    //-----------Discount-------------
                    strcln = "Select * From Product_BatchVolumeDicount Where ProductBAtchMasterID='" + i.ToString() + "'";
                     cmdcln = new SqlCommand(strcln, con);
                     dtcln = new DataTable();
                     adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);
                    if (dtcln.Rows.Count > 0)
                    {
                        txtSalePrice.Text = dtcln.Rows[0]["VolumeDiscountEligibleMinQty"].ToString();
                        txtCurrency.Text = dtcln.Rows[0]["VolumeDiscountAmtPerUnit"].ToString();
                        txtStartdate.Text = dtcln.Rows[0]["StartDate"].ToString();
                        txtEndDate.Text = dtcln.Rows[0]["EndDate"].ToString();
                    }
                    //------------------------
                    //------------------------
                    strcln = "Select * From Product_BtchPriceSalePriceDetail Where ProductBAtchMasterID='" + i.ToString() + "'";
                     cmdcln = new SqlCommand(strcln, con);
                     dtcln = new DataTable();
                     adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);
                    if (dtcln.Rows.Count > 0)
                    {
                        txtSalePrice.Text = dtcln.Rows[0]["SalePrice"].ToString();
                        txtCurrency.Text = dtcln.Rows[0]["Currency"].ToString();
                        txtStartdate.Text = dtcln.Rows[0]["StartDate"].ToString();
                        txtEndDate.Text = dtcln.Rows[0]["EndDate"].ToString();
                    }
                    //------------------------
                    btnSubmit.Text = "Update";                   
            }
        }
        if (e.CommandName == "Delete")
        {            
            int mm = Convert.ToInt32(e.CommandArgument);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Product_BatchMaster_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@ProductBAtchMasterID", mm);
            cmd.ExecuteNonQuery();
            con.Close();
            //-----------------------------------------------------------
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
             cmd = new SqlCommand("Product_BatchVolumeDicount_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@ProductBAtchMasterID", mm);
            cmd.ExecuteNonQuery();
            con.Close();
            //-------------------------------------------------------
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd = new SqlCommand("Product_BtchPriceSalePriceDetail_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@ProductBAtchMasterID", mm);
            cmd.ExecuteNonQuery();
            con.Close();            
            lblmsg.Visible = true;
            FillGrid();
            Clr();
            lblmsg.Text = "Record deleted successfully";
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
        addnewpanel.Visible = true;
        lblmsg.Text = "";
        Label19.Text = "Add New Product Batch";
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



    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtEndDate.Text) < Convert.ToDateTime(txtStartdate.Text))
        {
            lblenddateerror.Text = "The end Date must be greater than today's date.";
            txtEndDate.Text = "";
        }
        else
        {
            lblenddateerror.Text = "";
        }
    }
}
