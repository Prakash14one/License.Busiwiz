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

public partial class Add_PartyType_Master : System.Web.UI.Page
{
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
        GridView1.PagerSettings.Visible = true;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
        Label1.Visible = false;

        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            lblCompany.Text = Session["Cname"].ToString();
            ViewState["sortOrder"] = "";
            fillpartymastercategory();
            DDL();
            FillGrid();

        }

    }
    protected void fillpartymastercategory()
    {
        string str = "Select Id, Name From PartyMasterCategory order by Name ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable datat = new DataTable();
        adp.Fill(datat);
        if (datat.Rows.Count > 0)
        {
            ddlptclist.DataSource = datat;
            ddlptclist.DataTextField = "Name";
            ddlptclist.DataValueField = "Id";
            ddlptclist.DataBind();

            DropDownList1.DataSource = datat;
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "Id";
            DropDownList1.DataBind();

            DropDownList1.Items.Insert(0, "All");
            DropDownList1.SelectedItem.Value = "0";
        }
    }
    public void DDL()
    {
        //string str = " Select PartyTypeId, PartType From PartytTypeMaster where compid='"+compid+"' order by PartType ";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();

        //adp.Fill(ds, "PartytTypeMaster ");
        ////if (ds.Tables[0].Rows.Count < 0)
        ////{
        //DropDownList1.DataSource = ds;
        //DropDownList1.DataTextField = "PartType";
        //DropDownList1.DataValueField = "PartyTypeId";
        //DropDownList1.DataBind();

        //DropDownList1.Items.Insert(0, "All");
        //DropDownList1.SelectedItem.Value = "0";
        // FillGrid();
    }
    protected void FillGrid()
    {
        string str = "";
        if (DropDownList1.SelectedIndex > 0)
        {
            str = "Select PartyTypeId, PartType,PartyMasterCategory.Name From PartytTypeMaster left join PartyMasterCategory on PartyMasterCategory.id=PartytTypeMaster.PartyCategoryId where PartyCategoryId='" + DropDownList1.SelectedValue + "' and compid='" + compid + "' order by Name, PartType";
        }
        else
        {
            str = " Select PartyTypeId, PartType,PartyMasterCategory.Name From PartytTypeMaster left join PartyMasterCategory on PartyMasterCategory.id=PartytTypeMaster.PartyCategoryId where compid='" + compid + "'  order by Name, PartType ";
        }
        if (DropDownList1.SelectedIndex > -1)
        {
            lblcategory.Text = DropDownList1.SelectedItem.Text;
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

       
        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataSource = myDataView;
        GridView1.DataBind();
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PagerSettings.Visible = true;
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGrid();


    }



    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbl = (Label)(GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblpartytypeid"));
        Label lblpt = (Label)(GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblPartType"));
        int id;
        id = Convert.ToInt16(lbl.Text);
        ViewState["id"] = Convert.ToInt16(lbl.Text);
        if (lbl.Text != "")
        {
            string ptyid = "  select top 4 PartyTypeId from PartytTypeMaster where compid='" + compid + "'";
            SqlCommand cmdptyid = new SqlCommand(ptyid, con);
            SqlDataAdapter dtptyid = new SqlDataAdapter(cmdptyid);
            DataTable dtaptyid = new DataTable();
            dtptyid.Fill(dtaptyid);
            int max = Convert.ToInt32(dtaptyid.Rows[3]["PartyTypeId"].ToString());
            if (lblpt.Text == "Admin" || lblpt.Text == "Vendor" || lblpt.Text == "Employee" || lblpt.Text == "Customer")
            {
                //if (Convert.ToInt32(ViewState["id"]) <= max)
                //{
                Label1.Visible = true;
                Label1.Text = "Sorry, you are not allowed to delete this user sub-category.";
            }
            else
            {
                id = Convert.ToInt32(lbl.Text);
                String qs = "SELECT f.PartytypeId, f.DeleteAllowed FROM PartytTypeMaster p ,Fixeddata f  WHERE p.PartyTypeId = f.PartytypeId and p.PartyTypeId ='" + id + "' and compid='" + compid + "'";
                //SqlCommand cmdclassid = new SqlCommand("select EditAllowed,DeleteAllowed from Fixeddata where ClassType='" + id + "'", con);
                SqlCommand cmdclassid = new SqlCommand(qs, con);
                SqlDataAdapter dtpaccid = new SqlDataAdapter(cmdclassid);
                DataTable dtaccid = new DataTable();
                dtpaccid.Fill(dtaccid);
                if (dtaccid.Rows.Count > 0)
                {
                    if (dtaccid.Rows[0]["DeleteAllowed"].ToString() == "0")
                    {
                        FillGrid();
                        SqlCommand cmd = new SqlCommand("Delete from PartytTypeMaster where PartyTypeId = " + ViewState["id"] + "", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Label1.Visible = true;
                        Label1.Text = "Record deleted successfully";
                        GridView1.EditIndex = -1;

                        FillGrid();
                        DDL();

                        //Response.Redirect("AccountMaster_Edit.aspx?Id=" + Session["Id"].ToString() + "");
                    }
                    else if (dtaccid.Rows[0]["DeleteAllowed"].ToString() == "")
                    {
                        FillGrid();
                        SqlCommand cmd = new SqlCommand("Delete from PartytTypeMaster where PartyTypeId = " + ViewState["id"] + "", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Label1.Visible = true;
                        Label1.Text = "Record deleted successfully";
                        GridView1.EditIndex = -1;

                        FillGrid();
                        DDL();

                        //Response.Redirect("AccountMaster_Edit.aspx?Id=" + Session["Id"].ToString() + "");
                    }

                    else
                    {
                        ModalPopupExtender2.Show();
                    }
                }
                else
                {
                    FillGrid();
                    SqlCommand cmd = new SqlCommand("Delete from PartytTypeMaster where PartyTypeId = " + ViewState["id"] + "", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Label1.Visible = true;
                    Label1.Text = "Record deleted successfully";
                    GridView1.EditIndex = -1;

                    FillGrid();
                    DDL();
                    //GridView1.EditIndex = e.NewEditIndex;
                    //FillGrid();
                    //ModalPopupExtender3.Show();
                    //Response.Redirect("AccountMaster_Edit.aspx?Id=" + Session["Id"].ToString() + "");
                }
            }
        }
        //GridView1.EditIndex = e.NewEditIndex;
        //FillGrid();
        //ModalPopupExtender2.Show();
        //FillGrid();

        //ViewState["id"] = GridView1.DataKeys[e.RowIndex].Value;

        //ModalPopupExtender1222.Show();
        //GridView1.EditIndex = -1;
    }

    protected void yes_Click(object sender, EventArgs e)
    {
        //SqlCommand cmd = new SqlCommand("Delete from PartytTypeMaster where PartyTypeId = " + ViewState["id"] + "", con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmd.ExecuteNonQuery();
        //con.Close();

        //Label1.Visible = true;
        //Label1.Text = "Record deleted successfully";
        //GridView1.EditIndex = -1;

        //FillGrid();
        //DDL();
        //ModalPopupExtender1222.Hide();
    }

    protected void ImageButton6_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender1222.Hide();
        Label1.Visible = false;
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

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
        Label1.Visible = false;
    }
    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void ImageButton8_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void ImageButton10_Click(object sender, EventArgs e)
    {
        GridView1.PagerSettings.Visible = true;
        ModalPopupExtender3.Hide();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = " Select PartyTypeId, PartType From PartytTypeMaster where PartType='" + tbPartyName.Text + "' and compid='" + compid + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = " Record already exists";
            tbPartyName.Text = "";
        }
        else
        {
            string str1 = " Insert into PartytTypeMaster(PartType,compid,PartyCategoryId) values('" + tbPartyName.Text + "','" + compid + "','" + ddlptclist.SelectedValue + "')";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();

            Label1.Visible = true;
            Label1.Text = "Record inserted successfully";
            DDL();
            FillGrid();
            pnladd.Visible = false;
            lbladd.Text = "";
            btnadd.Visible = true;
            tbPartyName.Text = "";
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        tbPartyName.Text = "";
        ddlptclist.SelectedIndex = 0;
        Label1.Visible = false;
        pnladd.Visible = false;
        lbladd.Text = "";
        btnadd.Visible = true;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button7.Visible = true;
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

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(250);

            Button4.Text = "Printable Version";
            Button7.Visible = false;
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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {
            //ViewState["id"] = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
            ViewState["id"] = Convert.ToInt32(e.CommandArgument);
            string str = "Select * from PartytTypeMaster where PartyTypeId='" + ViewState["id"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string partytype = dt.Rows[0]["PartType"].ToString();
                if (partytype == "Admin" || partytype == "Vendor" || partytype == "Employee" || partytype == "Customer")
                {
                    Label1.Visible = true;
                    Label1.Text = "Sorry, you are not allowed to edit this user sub-category.";
                }
                else
                {
                    pnladd.Visible = true;
                    lbladd.Text = "Edit User Sub-Category";
                    btnadd.Visible = false;
                    fillpartymastercategory();
                    tbPartyName.Text = dt.Rows[0]["PartType"].ToString();
                    ddlptclist.SelectedIndex = ddlptclist.Items.IndexOf(ddlptclist.Items.FindByValue(dt.Rows[0]["PartyCategoryId"].ToString()));
                    btnupdate.Visible = true;
                    ImageButton1.Visible = false;
                    Label1.Text = "";
                }
            }
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        
        string str11 = " Select PartyTypeId, PartType From PartytTypeMaster where PartType='" + tbPartyName.Text + "' and compid='" + compid + "' and PartyTypeId <>'" + ViewState["id"] + "' ";
        SqlCommand cmd11 = new SqlCommand(str11, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd11);
        DataTable dt11 = new DataTable();
        adp.Fill(dt11);

        if (dt11.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = " Record already exists";
            tbPartyName.Text = "";
            ddlptclist.SelectedIndex = 0;
            FillGrid();
            btnupdate.Visible = false;
            ImageButton1.Visible = true;
        }
        else
        {
            string str = " Update PartytTypeMaster set PartType = '" + tbPartyName.Text + "',PartyCategoryId='" + ddlptclist.SelectedValue + "' where PartyTypeId = '" + ViewState["id"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            Label1.Visible = true;
            Label1.Text = "Record updated successfully";
            tbPartyName.Text = "";
            ddlptclist.SelectedIndex = 0;
            FillGrid();
            btnupdate.Visible = false;
            ImageButton1.Visible = true;
            pnladd.Visible = false;
            lbladd.Text = "";
            btnadd.Visible = true;
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        pnladd.Visible = true;
        lbladd.Text = "Add New User Sub-Category";
        btnadd.Visible = false;
    }

    
}
