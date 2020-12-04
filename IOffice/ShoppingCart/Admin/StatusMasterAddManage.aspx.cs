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
using System.Data.SqlClient;
using System.Xml.Linq;

public partial class Add_Status_Master : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con;
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
        statuslable.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            if (Request.QueryString["Id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                fillddldepartment();
                ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(id.ToString()));

                fillgrid();
                fillddl();
                pnladd.Visible = true;
                lbllegend.Text = "Add New Status";
                btnadd.Visible = false;

            }
            else
            {

                fillddldepartment();
                fillgrid();
                fillddl();
            }
        }
    }

    public void fillddldepartment()
    {
        string strfillgrid = "SELECT     StatusCategoryMasterId, StatusCategory FROM   StatusCategory where compid='" + compid + "' order by StatusCategory";
        SqlCommand cmdfillgrid = new SqlCommand(strfillgrid, con);
        SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
        DataTable dtfill = new DataTable();
        adpfillgrid.Fill(dtfill);
        ddldesignation.DataSource = dtfill;
        ddldesignation.DataValueField = "StatusCategoryMasterId";
        ddldesignation.DataTextField = "StatusCategory";
        ddldesignation.DataBind();

        ddldesignation.Items.Insert(0, "-Select-");
        ddldesignation.SelectedItem.Value = "0";
    }
    public void fillddl()
    {
        string strfillgrid = "SELECT     StatusCategoryMasterId, StatusCategory FROM   StatusCategory where compid='" + compid + "'  order by StatusCategory";
        SqlCommand cmdfillgrid = new SqlCommand(strfillgrid, con);
        SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
        DataTable dtfill = new DataTable();
        adpfillgrid.Fill(dtfill);
        DropDownList1.DataSource = dtfill;
        DropDownList1.DataValueField = "StatusCategoryMasterId";
        DropDownList1.DataTextField = "StatusCategory";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
        DropDownList1.SelectedItem.Value = "0";

        //object sender = new object();
        //EventArgs e= new EventArgs();
        //DropDownList1_SelectedIndexChanged1(sender,e);



    }
    public void fillgrid()
    {
        lblCompany.Text = Session["Cname"].ToString();
        string str1 = "";
        if (DropDownList1.SelectedIndex > 0)
        {
            lblCompany.Text = Session["Cname"].ToString();
            lblStatusCat.Text = DropDownList1.SelectedItem.Text;
            if (DropDownList2.SelectedIndex > 0)
            {
                lblStatus.Text = DropDownList2.SelectedItem.Text;

                //str1 = "SELECT     InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId,  " +
                //    " InventoryCategoryMaster.InventoryCatName " +
                //    " FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                //    "  InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId " +
                //    " where InventorySubCategoryMaster.InventorySubCatId = '" + ddlsubcat.SelectedValue + "'";

                str1 = "SELECT     StatusMaster.StatusId, StatusMaster.StatusName, StatusMaster.StatusCategoryMasterId, StatusCategory.StatusCategory," +
                 " StatusCategory.StatusCategoryMasterId FROM         StatusCategory INNER JOIN " +
                " StatusMaster ON StatusCategory.StatusCategoryMasterId = StatusMaster.StatusCategoryMasterId " +
                "where StatusMaster.StatusId='" + DropDownList2.SelectedValue + "' and compid='" + compid + "' order by  StatusCategory, StatusName";
            }
            else
            {
                lblStatus.Text = "ALL";
                str1 = "SELECT     StatusMaster.StatusId, StatusMaster.StatusName, StatusMaster.StatusCategoryMasterId, StatusCategory.StatusCategory," +
                " StatusCategory.StatusCategoryMasterId FROM         StatusCategory INNER JOIN " +
               " StatusMaster ON StatusCategory.StatusCategoryMasterId = StatusMaster.StatusCategoryMasterId " +
               "where StatusCategory.StatusCategoryMasterId='" + DropDownList1.SelectedValue + "' and compid='" + compid + "' order by  StatusCategory, StatusName";
            }
        }
        else
        {
            lblStatusCat.Text = "All";
            lblStatus.Text = "All";
            lblCompany.Text = Session["Cname"].ToString();
            str1 = "SELECT     StatusMaster.StatusId, StatusMaster.StatusName, StatusMaster.StatusCategoryMasterId, StatusCategory.StatusCategory," +
                " StatusCategory.StatusCategoryMasterId FROM         StatusCategory INNER JOIN " +
               " StatusMaster ON StatusCategory.StatusCategoryMasterId = StatusMaster.StatusCategoryMasterId  where compid='" + compid + "' order by  StatusCategory, StatusName";
        }
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            GridView1.DataSource = dt;
            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataBind();
        }
        else
        {
            GridView1.EmptyDataText = "No Record Found.";
            GridView1.DataBind();
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        statuslable.Text = "";
        ViewState["id"] = GridView1.DataKeys[e.NewEditIndex].Value.ToString();

        SqlCommand cmd = new SqlCommand("select EditAllowed,StatusMasterId from Fixeddata where StatusMasterId = '" + ViewState["id"] + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["EditAllowed"].ToString() == "0")
                {

                    edit();
                    pnladd.Visible = true;
                    lbllegend.Text = "Edit Status";
                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                    ImageButton1.Visible = false;
                }
                else if (dt.Rows[0]["EditAllowed"].ToString() == "")
                {
                    edit();
                    pnladd.Visible = true;
                    lbllegend.Text = "Edit Status";
                    btnupdate.Visible = false;
                    ImageButton1.Visible = false;
                    btnadd.Visible = true;
                }
                else
                {
                    statuslable.Text = "Sorry, You are not allowed to edit this record.";
                    // ModalPopupExtender3.Show();

                }
            }
            else
            {
                edit();
                pnladd.Visible = true;
                lbllegend.Text = "Edit Status";
                btnadd.Visible = false;
                btnupdate.Visible = true;
                ImageButton1.Visible = false;
            }
        
       
    }
    protected void edit()
    {
        string str = "select * from StatusMaster where StatusId = '" + ViewState["id"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(dt.Rows[0]["StatusCategoryMasterId"].ToString()));
            txtdegnation.Text = dt.Rows[0]["StatusName"].ToString();
        }
    }
    
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label l11 = (Label)GridView1.Rows[e.RowIndex].FindControl("lblgrdCatid");
        //int id;
        //id = Convert.ToInt32(l11.Text);
        //ViewState["id"] = Convert.ToInt32(l11.Text);

        if (l11.Text != "")
        {
            SqlCommand cmd = new SqlCommand("select DeleteAllowed,StatusMasterId from Fixeddata where StatusMasterId = '" + l11.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["DeleteAllowed"].ToString() == "0")
                {
                    //GridView1.EditIndex = e.RowIndex;
                    ViewState["Id"] = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                    // ModalPopupExtender1222.Show();
                    delete(sender, e);
                }
                else if (dt.Rows[0]["DeleteAllowed"].ToString() == "")
                {
                    //GridView1.EditIndex = e.RowIndex;
                    ViewState["Id"] = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                    //ModalPopupExtender1222.Show();
                    delete(sender, e);
                }
                else
                {
                    ModalPopupExtender2.Show();

                }
            }
            else
            {
                //GridView1.EditIndex = e.RowIndex;
                ViewState["Id"] = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                // ModalPopupExtender1222.Show();
                delete(sender, e);
            }
        }
        //ViewState["Id"] = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        // ModalPopupExtender1222.Show();
    }
    protected void delete(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("delete  from StatusMaster where [StatusId]=" + ViewState["Id"] + " ", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        statuslable.Visible = true;
        statuslable.Text = "Record deleted successfully";

        
        fillgrid();
        DropDownList1_SelectedIndexChanged1(sender, e);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        //SqlCommand cmd = new SqlCommand("delete  from StatusMaster where [StatusId]=" + ViewState["Id"] + " ", con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmd.ExecuteNonQuery();
        //con.Close();
        //statuslable.Visible = true;
        //statuslable.Text = "Recored Deleted Successfully";

        //GridView1.SelectedIndex = -1;
        //fillgrid();
        //DropDownList1_SelectedIndexChanged1(sender, e);
        // fillddl();
        // DropDownList2_SelectedIndexChanged(sender,  e);

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }
        //else if (e.CommandName == "Delete")
        //{
        //    GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        //    ViewState["Id"] = GridView1.SelectedDataKey.Value;
        //}
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
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
    protected void ImageButton1_Click1(object sender, EventArgs e)
    {
        if (ddldesignation.SelectedIndex > 0)
        {
            string str1 = "SELECT     StatusMaster.StatusName, StatusMaster.StatusCategoryMasterId  FROM StatusMaster left outer join StatusCategory on  StatusCategory.StatusCategoryMasterId=StatusMaster.StatusCategoryMasterId" +
            " where StatusName='" + txtdegnation.Text + "' and compid='" + compid + "' order by StatusName";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                statuslable.Visible = true;
                statuslable.Text = "Record already exists";
            }
            else
            {
                string str = "insert into  StatusMaster  values ('" + txtdegnation.Text + "','" + ddldesignation.SelectedValue + "')";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                statuslable.Visible = true;
                statuslable.Text = "Record inserted successfully";


                fillgrid();

                txtdegnation.Text = "";
                ddldesignation.SelectedIndex = 0;


                DropDownList1_SelectedIndexChanged1(sender, e);

                pnladd.Visible = false;
                lbllegend.Text = "";
                btnadd.Visible = true;

            }
        }
        else
        {
            statuslable.Visible = true;
            statuslable.Text = "Please Select Category";
        }
    }
    

    protected void ImageButton6_Click(object sender, EventArgs e)
    {
        ddldesignation.SelectedIndex = 0;
        txtdegnation.Text = "";
        statuslable.Visible = false;
        pnladd.Visible = false;
        lbllegend.Text = "";
        btnadd.Visible = true;
        btnupdate.Visible = false;
        ImageButton1.Visible = true;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (DropDownList1.SelectedIndex > 0)
        //{
        //    DropDownList2.Items.Clear();
        //     //string str45 = "SELECT     StateName  ,StateId " +
        //     //       " FROM  StateMasterTbl where CountryId='" + ddlSearchByCountry.SelectedValue + "' "+
        //     //       " Order By StateName";
        //    string str45="SELECT    StatusId,StatusName"+
        //              " FROM         StatusMaster  where StatusCategoryMasterId='" +DropDownList1.SelectedValue + "'";
        //            //" StatusMaster ON StatusCategory.StatusCategoryMasterId = StatusMaster.StatusCategoryMasterId";


        //    //"Select StateName,StateId from StateMasterTbl";
        //    SqlCommand cmd45 = new SqlCommand(str45, con);

        //    SqlDataAdapter da = new SqlDataAdapter(cmd45);
        //    //da.SelectCommand = cmd;
        //    DataTable ds = new DataTable();

        //    da.Fill(ds);
        //    DropDownList2.DataSource = ds;
        //    DropDownList2.DataTextField = "StatusName";
        //    DropDownList2.DataValueField = "StatusId";
        //    DropDownList2.DataBind();

        //    DropDownList2.Items.Insert(0, "All");
        //    DropDownList2.SelectedItem.Value = "0";


        //}
        //else
        //{
        //    DropDownList2.Items.Clear();
        //    DropDownList2.Items.Insert(0, "All");
        //    DropDownList2.SelectedItem.Value = "0";
        //}

        //ddlSearchByState_SelectedIndexChanged(sender, e);
    }
    //protected void ddlSearchByState_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //}
    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex > 0)
        {
            DropDownList2.Items.Clear();
            //string str45 = "SELECT     StateName  ,StateId " +
            //       " FROM  StateMasterTbl where CountryId='" + ddlSearchByCountry.SelectedValue + "' "+
            //       " Order By StateName";
            //string str45 = "SELECT    StatusId,StatusName" +
            //          " FROM         StatusMaster left outer join StatusCategory on  StatusCategory.StatusCategoryMasterId=StatusMaster.StatusCategoryMasterId" +
            //          "where StatusCategoryMasterId='" + DropDownList1.SelectedValue + "' and compid='" + compid + "'";
            //" StatusMaster ON StatusCategory.StatusCategoryMasterId = StatusMaster.StatusCategoryMasterId";
            string str45 = "select StatusId,StatusName from StatusMaster where StatusCategoryMasterId='" + DropDownList1.SelectedValue + "'";

            //"Select StateName,StateId from StateMasterTbl";
            SqlCommand cmd45 = new SqlCommand(str45, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd45);
            //da.SelectCommand = cmd;
            DataTable ds = new DataTable();

            da.Fill(ds);
            DropDownList2.DataSource = ds;
            DropDownList2.DataTextField = "StatusName";
            DropDownList2.DataValueField = "StatusId";
            DropDownList2.DataBind();
            //statuslable.Visible = false;
            DropDownList2.Items.Insert(0, "--Select--");
            DropDownList2.SelectedItem.Value = "0";
            //statuslable.Visible = false;
        }
        else
        {
            //DropDownList2.DataSource = null;
            //DropDownList2.DataBind();
            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, "--Select--");
            DropDownList2.SelectedItem.Value = "-1";
        }
        //    DropDownList2.Items.Insert(0, "All");
        //    DropDownList2.SelectedItem.Value = "0";
        //    statuslable.Visible = false;

        //}
        //else
        //{
        //    DropDownList2.Items.Clear();
        //    DropDownList2.Items.Insert(0, "All");
        //    DropDownList2.SelectedItem.Value = "0";
        //}
        DropDownList2_SelectedIndexChanged(sender, e);
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();

    }
    protected void ImageButton10_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Hide();
    }
    protected void ImageButton11_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (Button5.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button5.Text = "Hide Printable Version";
            Button3.Visible = true;
            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[3].Visible = false;
            }
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }

        }
        else
        {
            // pnlgrid.ScrollBars = ScrollBars.Vertical;
            //  pnlgrid.Height = new Unit(150);

            Button5.Text = "Printable Version";
            Button3.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            lbllegend.Text = "Add New Status";
        }
        else
        {
            pnladd.Visible = false;
            lbllegend.Text = "";
        }
        btnadd.Visible = false;
        statuslable.Text = "";
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string str1 = "SELECT     StatusMaster.StatusName, StatusMaster.StatusCategoryMasterId  FROM StatusMaster left outer join StatusCategory on  StatusCategory.StatusCategoryMasterId=StatusMaster.StatusCategoryMasterId" +
            " where StatusName='" + txtdegnation.Text + "' and compid='" + compid + "' and StatusId<>'" + ViewState["id"] + "' ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "Record already exists";
        }
        else
        {

            string update = "update  StatusMaster set StatusName='" + txtdegnation.Text + "', " +
                   " StatusCategoryMasterId='" + ddldesignation.SelectedValue + "' where StatusId='" + ViewState["id"] + "'  ";


            SqlCommand cmdupate = new SqlCommand(update, con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }

            cmdupate.ExecuteNonQuery();
            con.Close();
            statuslable.Text = "Record updated successfully";
            statuslable.Visible = true;

            fillgrid();
            pnladd.Visible = false;
            lbllegend.Text = "";
            ddldesignation.SelectedIndex = 0;
            txtdegnation.Text = "";
            btnadd.Visible = true;
            btnupdate.Visible = false;
            ImageButton1.Visible = true;

        }
    }
}