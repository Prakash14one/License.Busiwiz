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

    protected void Page_Load(object sender, EventArgs e)
    {
       // chkboxActiveDeactive.Checked=true;
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
            FillPriceplanName();

            FillGrid();
        }
    }
    protected void Clr()
    {
        txtid.Text = "";
        txtdesc.Text = "";
        txtcategorytype.Text = "";
        ddlrelatedto.SelectedIndex = 0; 
        chkboxActiveDeactive.Checked=false;
        pnladdnew.Visible = false;
        txtid.Enabled = true;
        txtcategorytype.Enabled = true;
        lblcatid.Visible = false;
        lblcatetype.Visible = false; 
    }
    protected void FillPriceplanName()
    {
      
            ddlrelatedto.DataSource = null;

            string strcln1 = " Select * From PriceplanCategoryTypeRelatedTo Where  active='1'";

            SqlCommand cmdcln = new SqlCommand(strcln1, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlrelatedto.DataSource = dtcln;
            ddlrelatedto.DataValueField = "ID";
            ddlrelatedto.DataTextField = "Name";
            ddlrelatedto.DataBind();
            ddlrelatedto.Items.Insert(0, "--Select--");
            ddlrelatedto.Items[0].Value = "0";            

    }
    protected void FillGrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string active;
        string deactive;

        string strcln = " select *,PriceplanCategoryTypeRelatedTo.Name FROM dbo.PriceplanCategoryType LEFT OUTER JOIN dbo.PriceplanCategoryTypeRelatedTo ON dbo.PriceplanCategoryType.PriceplanCategoryTypeRelatedToid = dbo.PriceplanCategoryTypeRelatedTo.ID   ";
        
        if (ddlstatus.SelectedItem.Text == "Active")
        {
            active = " Where PriceplanCategoryType.Active='True'";
            strcln += active;
        }
        else if (ddlstatus.SelectedItem.Text == "Inactive")
        {
            deactive = " Where PriceplanCategoryType.Active='False'";
            strcln += deactive;
        }      
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
  

    protected void btnSubmit_Click(object sender, EventArgs e)
    {      
               
        try
        {
            if (btnSubmit.Text == "Update")
            { 
                            #region
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("PriceplanCategoryType_AddDelUpdtSelect", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatementType", "Update");
                            cmd.Parameters.AddWithValue("@IDAuto", ViewState["IDAuto"]);
                            cmd.Parameters.AddWithValue("@ID", txtid.Text);
                            cmd.Parameters.AddWithValue("@CategoryType", txtcategorytype.Text);
                            cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                            cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
                            cmd.Parameters.AddWithValue("@PriceplanCategoryTypeRelatedToid", ddlrelatedto.SelectedValue);
                
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lblmsg.Visible = true;
                            lblmsg.Text = "Record update successfully";
                            Clr();
                            FillGrid();
                            #endregion
               
            }
            else
            {
                #region
                string finalstr = "Select * from  PriceplanCategoryType where ID='" + txtid.Text + "'";
                SqlCommand cmdcln = new SqlCommand(finalstr, con);
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                DataTable dtcln = new DataTable();
                adpcln.Fill(dtcln);
                if (dtcln.Rows.Count == 0)
                {
                    #region

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("PriceplanCategoryType_AddDelUpdtSelect", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StatementType", "Insert");
                    cmd.Parameters.AddWithValue("@ID", txtid.Text);
                    cmd.Parameters.AddWithValue("@CategoryType", txtcategorytype.Text);
                    cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                    cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
                    cmd.Parameters.AddWithValue("@PriceplanCategoryTypeRelatedToid", ddlrelatedto.SelectedValue);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully.";
                    Clr();
                    FillGrid();
                    #endregion
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Sorry, this ID is alredy Added";
                }
                #endregion
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
            Label19.Text = " Edit Price Plan Category  Type ";
            lblmsg.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());
                string strcln = "Select * From PriceplanCategoryType Where ID='" + i.ToString() + "'";
                hdnProductDetailId.Value = i.ToString();

                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);               
                if (dtcln.Rows.Count > 0)
                {
                    ViewState["IDAuto"] = dtcln.Rows[0]["IDAuto"].ToString();
                    hdnProductId.Value = dtcln.Rows[0]["IDAuto"].ToString();
                    txtid.Text = dtcln.Rows[0]["ID"].ToString();
                    txtcategorytype.Text = dtcln.Rows[0]["CategoryType"].ToString();
                    txtdesc.Text = dtcln.Rows[0]["Description"].ToString();
                    try
                    {
                        if (Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString()) == true)
                        {
                            chkboxActiveDeactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        ddlrelatedto.SelectedValue = dtcln.Rows[0]["PriceplanCategoryTypeRelatedToid"].ToString();                       
                    }
                    catch (Exception ex)
                    {
                    }
                    
                    //-----------------
                    try
                    {
                        //------------
                        string finalstr = "Select * from  Priceplancategory where CategoryTypeID='" + i.ToString() + "'";
                         cmdcln = new SqlCommand(finalstr, con);
                         adpcln = new SqlDataAdapter(cmdcln);
                         dtcln = new DataTable();
                        adpcln.Fill(dtcln);
                        if (dtcln.Rows.Count == 0)
                        {
                            finalstr = "Select * from  Product_MasterIndividual where PriceplanCategoryTypeID='" + i.ToString() + "'";
                            cmdcln = new SqlCommand(finalstr, con);
                            adpcln = new SqlDataAdapter(cmdcln);
                            dtcln = new DataTable();
                            adpcln.Fill(dtcln);
                            if (dtcln.Rows.Count == 0)
                            {
                                
                                finalstr = "Select * from  PriceplanCategoryType where ID='" + i.ToString() + "' and IDAuto !='" + ViewState["IDAuto"] + "'";
                                cmdcln = new SqlCommand(finalstr, con);
                                adpcln = new SqlDataAdapter(cmdcln);
                                dtcln = new DataTable();
                                adpcln.Fill(dtcln);
                                if (dtcln.Rows.Count == 0)
                                {
                                    txtid.Enabled = true;
                                    txtcategorytype.Enabled = true;
                                    lblcatid.Visible = false;
                                    lblcatetype.Visible = false; 
                                }
                                else
                                {
                                    txtid.Enabled = false;
                                    txtcategorytype.Enabled = false;
                                    lblcatid.Visible = true;
                                    lblcatetype.Visible = true; 
                                }
                            }
                            else
                            {
                                txtid.Enabled = false;
                                txtcategorytype.Enabled = false;
                                lblcatid.Visible = true;
                                lblcatetype.Visible = true; 
                            }
                        }
                        else
                        {
                            txtid.Enabled = false;
                            txtcategorytype.Enabled = false;
                            lblcatid.Visible = true;
                            lblcatetype.Visible = true; 
                        }
                        //------------
                    }
                    catch (Exception ex)
                    {
                    }
                    //------------------
                    addnewpanel.Visible = true;
                    pnladdnew.Visible = true;
                    //------------------------
                    btnSubmit.Text = "Update";                   
            }
        }
        if (e.CommandName == "delete")
        {
            //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());
            int mm = Convert.ToInt32(e.CommandArgument);
            string finalstr = "Select * from  Priceplancategory where CategoryTypeID='" + mm + "'";
            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            DataTable dtcln = new DataTable();
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count == 0)
            {
                finalstr = "Select * from  Product_MasterIndividual where PriceplanCategoryTypeID='" + mm + "'";
                cmdcln = new SqlCommand(finalstr, con);
                adpcln = new SqlDataAdapter(cmdcln);
                dtcln = new DataTable();
                adpcln.Fill(dtcln);
                if (dtcln.Rows.Count == 0)
                {
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("PriceplanCategoryType_AddDelUpdtSelect", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StatementType", "Delete");
                    cmd.Parameters.AddWithValue("@ID", mm);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record deleted successfully";
                    FillGrid();
                    Clr();
                }
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You are not allow delete this record,first delete child record.";
            }
       
           
          
           
        }
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
        pnladdnew.Visible = true;
        Label19.Text = "";
        Clr();
    }
   
  

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;
            if (GridView1.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[2].Visible = false;
            }
       
        }
        else
        {
            Button3.Text = "Printable Version";
            Button4.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[2].Visible = true;
            }
         
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
      
        addnewpanel.Visible = false;       
        pnladdnew.Visible = true;
        lblmsg.Text = "";
        Label19.Text = " Price Plan Category  Type  Add / Manage ";
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
}
