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
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            FillGrid();
        }
    }
    protected void Clr()
    {
        txtSalesType.Text = "";       
        chkboxActiveDeactive.Checked=false;
        pnladdnew.Visible = false;   
    }   
      
    protected void FillGrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string active;
        string deactive;

        string strcln = " select * from SalesTypeTbl   ";
        
        if (ddlstatus.SelectedItem.Text == "Active")
        {
            active = " Where SalesTypeTbl.Active='True'";
            strcln += active;
        }
        else if (ddlstatus.SelectedItem.Text == "Inactive")
        {
            deactive = " Where SalesTypeTbl.Active='False'";
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

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SalesTypeTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Update");
                cmd.Parameters.AddWithValue("@id", ViewState["ID"]);
                cmd.Parameters.AddWithValue("@Name", txtSalesType.Text);
                cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
                cmd.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Record update successfully";
                Clr();
                FillGrid();
                        
            }
            else
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SalesTypeTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                cmd.Parameters.AddWithValue("@Name", txtSalesType.Text);
                cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
                cmd.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully.";
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
            
            Label19.Text = " Edit Sales Type Add / Manage ";
            lblmsg.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());

            string strcln = "Select * From SalesTypeTbl Where id='" + i.ToString() + "'";
                hdnProductDetailId.Value = i.ToString();

                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);               
                if (dtcln.Rows.Count > 0)
                {
                    ViewState["ID"] = dtcln.Rows[0]["id"].ToString();
                    hdnProductId.Value = dtcln.Rows[0]["id"].ToString();
                    txtSalesType.Text = dtcln.Rows[0]["Name"].ToString();
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
            string finalstr = "Select * from  Product_TypeTbl where SaleTypeTblID='" + mm + "'";
            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            DataTable dtcln = new DataTable();
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count == 0)
            {
                //string otherup = " Delete SalesTypeTbl_AddDelUpdtSelect Where  id='" + mm + "' ";
                //SqlCommand cmdotherup = new SqlCommand(otherup, con);
                //if (con.State.ToString() != "Open")
                //{
                //    con.Open();
                //}
                //cmdotherup.ExecuteNonQuery();
                //con.Close();
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SalesTypeTbl_AddDelUpdtSelect", con);
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
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
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
        pnladdnew.Visible = false;
        Label19.Text = "";
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
        pnladdnew.Visible = true;
        addnewpanel.Visible = true;
        lblmsg.Text = "";
        Label19.Text = " Sales Type Add / Manage ";
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
