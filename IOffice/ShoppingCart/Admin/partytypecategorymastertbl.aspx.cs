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

public partial class PartyTypeCategoryMasterTbl : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);

    SqlConnection con;
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
        Label1.Visible = false;

        PageConn pgcon = new PageConn();

        con = pgcon.dynconn;

        if (!IsPostBack)
        {
            txtFromDate.Text = System.DateTime.Now.ToShortDateString();
            txtToDate.Text = System.DateTime.Now.ToShortDateString();

            TextBox3.Text = System.DateTime.Now.ToShortDateString();
            TextBox4.Text = System.DateTime.Now.ToShortDateString();

            ViewState["sortOrder"] = "";
           
            fillstore();

            fillfilterstore();

            RadioButtonList1_SelectedIndexChanged(sender, e);
            fillgriddata();
            
        }
    }
    protected void fillgriddata()
    {

        lblstatusprint.Text = ddlstatus.SelectedItem.Text;

        GridView1.DataSource = null;
        GridView1.DataBind();

        lblCompany.Text = Session["Cname"].ToString();
        lblBusiness.Text = DropDownList1.SelectedItem.Text;

       
        string str1 = " SELECT PartyTypeCategoryMasterTbl.*,case when (PartyTypeCategoryMasterTbl.Active='1') then 'Active' else 'Inactive' End as Statuslabel,WarehouseMaster.Name from PartyTypeCategoryMasterTbl inner join  WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid where PartyTypeCategoryMasterTbl.compid='" + Session["comid"].ToString() + "'";
        string str2 = "";
        string str3 = "";
        string str4 = "";
        


        if (DropDownList1.SelectedIndex> 0)
        {

            str2 = " and PartyTypeCategoryMasterTbl.Whid='" + DropDownList1.SelectedValue + "'";
        }
        if (ddlfilterstatus.SelectedIndex > 0)
        {
            str3 = "and PartyTypeCategoryMasterTbl.Active='" + ddlfilterstatus.SelectedValue + "' ";
        }
        if (RadioButtonList1.SelectedValue == "0")
        {
            str4 = " and PartyTypeCategoryMasterTbl.StartDate between '"+System.DateTime.Now.ToShortDateString()+"' and '"+System.DateTime.Now.ToShortDateString()+"'";
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            str4 = " and PartyTypeCategoryMasterTbl.StartDate between '" + TextBox3.Text + "' and '" + TextBox4.Text + "'";
        }


        string finalstr = str1 + str2 + str3 + str4;
        SqlCommand cmd = new SqlCommand(finalstr,con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            GridView1.DataSource = ds.DefaultView;
            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;
            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataSource = myDataView;
            GridView1.DataBind();
        }

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
    public void clean()
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        RadioButton1.Checked = true;
        
        ddlwarehouse.SelectedIndex = 0;
    }
    
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label1.Visible = true;
        Label1.Text = "Record updated successfully";
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label1.Visible = true;
        Label1.Text = "Record deleted successfully";
    }

    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editview")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["partyid"] = Convert.ToInt32(e.CommandArgument);

            SqlCommand cmd1 = new SqlCommand("SELECT * from PartyTypeCategoryMasterTbl where PartyTypeCategoryMasterId='" + ViewState["partyid"] + "'", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd1);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            if (ds.Rows.Count > 0)
            {
               
                ImageButton1.Text = "Update";
                fillstore();
                ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(ds.Rows[0]["Whid"].ToString()));
                
                TextBox1.Text = ds.Rows[0]["PartyCategoryName"].ToString();
                TextBox2.Text = ds.Rows[0]["PartyCategoryDiscount"].ToString();

                txtFromDate.Text = Convert.ToDateTime(ds.Rows[0]["StartDate"].ToString()).ToShortDateString();
                txtToDate.Text = Convert.ToDateTime(ds.Rows[0]["EndDate"].ToString()).ToShortDateString();

                if (ds.Rows[0]["IsPercentage"].ToString() == "True")
                {
                    RadioButton1.Checked = true;
                }
                else
                {
                    RadioButton2.Checked = true;
                }
                if (ds.Rows[0]["Active"].ToString() == "True")
                {
                   
                    ddlstatus.SelectedValue = "1";
                }
                else
                {
                  
                    ddlstatus.SelectedValue = "0";
                }

            }
            lbllegend.Text = "Edit Category for Customer Discount";
            pnladd.Visible = true;
            lbllegend.Visible = true;
            btnadd.Visible = false;


        }
        else
            if (e.CommandName == "del")
            {
                GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                //ViewState["partyid"] = GridView1.SelectedDataKey.Value;
                ViewState["partyid"] = Convert.ToInt32(e.CommandArgument);
                // ModalPopupExtender1222.Show();
                string str = " Select * FROM PartyTypeDetailTbl inner join PartyTypeCategoryMasterTbl on PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId=PartyTypeDetailTbl.PartyTypeCategoryMasterId inner join WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid  " +

                    " where (PartyTypeCategoryMasterTbl.Active = 1) and PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId='" + ViewState["partyid"] + "' ";

                SqlCommand cmd12 = new SqlCommand(str, con);
                SqlDataAdapter adp22 = new SqlDataAdapter(cmd12);
                DataTable dt = new DataTable();
                adp22.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    SqlCommand cmd1 = new SqlCommand("Delete from PartyTypeCategoryMasterTbl where PartyTypeCategoryMasterId='" + ViewState["partyid"] + "'", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();

                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    Label1.Visible = true;
                    Label1.Text = "Record deleted successfully";
                    fillgriddata();
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Sorry ,This Discount Category is applied to customer so you can not delete this record ";
                }
            }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click1(object sender, EventArgs e)
    {
      
        ImageButton1.Text = "Submit";
        clean();

        lbllegend.Text = "Add New Category for Customer Discount";
        pnladd.Visible = false;
        lbllegend.Visible = false;
        btnadd.Visible = true;
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        SqlCommand cmd;
        try
        {
            if (ImageButton1.Text == "Update")
            {
                SqlCommand cmd1 = new SqlCommand("SELECT * from PartyTypeCategoryMasterTbl where Whid='" + ddlwarehouse.SelectedValue + "' and PartyCategoryName='" + TextBox1.Text + "' and PartyTypeCategoryMasterId<>'" + ViewState["partyid"] + "' ", con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                DataTable ds = new DataTable();
                adp.Fill(ds);

                if (ds.Rows.Count > 0)
                {
                    Label1.Text = "Record already used";
                    Label1.Visible = true;
                }
                else
                {
                    cmd = new SqlCommand("SP_Update_PartyTypeCategoryMasterTbl", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PartyTypeCategoryMasterId", ViewState["partyid"]);
                    Label1.Text = "Record updated successfully";
                    cmd.Parameters.AddWithValue("@PartyCategoryName", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@PartyCategoryDiscount", TextBox2.Text);

                    cmd.Parameters.AddWithValue("@Whid", ddlwarehouse.SelectedValue);
                    if (RadioButton1.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@IsPercentage", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@IsPercentage", 0);
                    }
                    if (ddlstatus.SelectedValue == "1")
                    {
                        cmd.Parameters.AddWithValue("@Active", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Active", 0);
                    }
                    DateTime DT = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                    cmd.Parameters.AddWithValue("@EntryDate", Convert.ToDateTime(DT));
                    cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtFromDate.Text).ToShortDateString());
                    cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(txtToDate.Text).ToShortDateString());
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Label1.Visible = true;
                    //ImageButton1.ImageUrl = "~/ShoppingCart/images/submit.png";
                    ImageButton1.Text = "Submit";
                    clean();
                    fillgriddata();
                    pnladd.Visible = false;
                    Label1.Text = "Record updated succesfully";
                    lbllegend.Text = "Add New Category for Customer Discount";
                    pnladd.Visible = false;
                    lbllegend.Visible = false;
                    btnadd.Visible = true;

                   


                }
            }
            else
            {
                SqlCommand cmd1 = new SqlCommand("SELECT * from PartyTypeCategoryMasterTbl where Whid='" + ddlwarehouse.SelectedValue + "' and PartyCategoryName='" + TextBox1.Text + "'", con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                DataTable ds = new DataTable();
                adp.Fill(ds);

                if (ds.Rows.Count > 0)
                {
                    Label1.Text = "Record already used";
                    Label1.Visible = true;
                }
                else
                {
                    cmd = new SqlCommand("sP_iNSERT_PartyTypeCategoryMasterTbl", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@PartyCategoryName", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@PartyCategoryDiscount", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@Whid", ddlwarehouse.SelectedValue);

                    if (RadioButton1.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@IsPercentage", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@IsPercentage", 0);
                    }
                    if (ddlstatus.SelectedValue == "1")
                    {
                        cmd.Parameters.AddWithValue("@Active", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Active", 0);
                    }
                    DateTime DT = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                    cmd.Parameters.AddWithValue("@EntryDate", Convert.ToDateTime(DT));
                    cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtFromDate.Text).ToShortDateString());
                    cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(txtToDate.Text).ToShortDateString());

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Label1.Visible = true;
                    Label1.Text = "Record inserted succesfully";
                    ImageButton1.Text = "Submit";
                    lbllegend.Text = "Add New Category for Customer Discount";
                    pnladd.Visible = false;
                    lbllegend.Visible = false;
                    btnadd.Visible = true;
                    clean();
                    fillgriddata();

                    SqlCommand cmdmax = new SqlCommand("SELECT Max(PartyTypeCategoryMasterId) as PartyTypeCategoryMasterId from PartyTypeCategoryMasterTbl ", con);
                    SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
                    DataTable dsmax = new DataTable();
                    adpmax.Fill(dsmax);

                    if (dsmax.Rows.Count > 0)
                    {


                        if (CheckBox1.Checked == true)
                        {
                            string te1 = "ManageCustomerDiscountCategory.aspx?Id=" + dsmax.Rows[0]["PartyTypeCategoryMasterId"].ToString();
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te1 + "');", true);
                        }
                    }



                }
            }


        }
        catch (Exception ex)
        {
            Label1.Visible = true;
            Label1.Text = "Error:" + ex.Message.ToString();
        }
        finally { }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgriddata();

    }
    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgriddata();
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {




    }
    protected void Button3_Click2(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button3.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[10].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[10].Visible = false;
            }
            if (GridView1.Columns[11].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                GridView1.Columns[11].Visible = false;
            }

        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(150);

            Button3.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[10].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                GridView1.Columns[11].Visible = true;
            }
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            lbllegend.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            lbllegend.Visible = false;
        }
        btnadd.Visible = false;
        Label1.Text = "";
    }
    protected void fillstore()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlwarehouse.DataSource = ds;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void fillfilterstore()
    {
        DropDownList1.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";

    

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue =="0")
        {
            Panel2.Visible = false;
           
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            Panel2.Visible = true;
        }
    }
    protected void Button4_Click1(object sender, EventArgs e)
    {
        fillgriddata();

    }
}
