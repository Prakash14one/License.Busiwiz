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

public partial class ShoppingCart_Admin_Default : System.Web.UI.Page
{

    SqlConnection con;
    SqlCommand cmd;
    DataTable dt;
    DataSet ds;
    SqlDataAdapter da;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Page.Title = pg.getPageTitle(page);

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        ViewState["Compid"] = Session["Comid"].ToString();
        ViewState["UserName"] = Session["userid"].ToString();
        ViewState["sortOrder"] = "";
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            //  fillwarehouse();
            fillwarehouse();
            fillwarehousefilter();

            fillskillfilter();
            fillgrid();
        }
    }
    protected void fillwarehouse()
    {
        ddlWarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlWarehouse.DataSource = ds;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "WareHouseId";
        ddlWarehouse.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
        fillskill();
    }
    protected void fillwarehousefilter()
    {
        ddBusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();


        ddBusiness.DataSource = ds;
        ddBusiness.DataTextField = "Name";
        ddBusiness.DataValueField = "WareHouseId";
        ddBusiness.DataBind();
        ddBusiness.Items.Insert(0, "All");
        ddBusiness.Items[0].Value = "0";
    }
    protected void fillskill()
    {
        ddSkillType.Items.Clear();
        string strskill = "select Name,ID from SkillType where BusinessId = '" + ddlWarehouse.SelectedValue + "'";
        SqlCommand cmdskill = new SqlCommand(strskill, con);
        SqlDataAdapter daskill = new SqlDataAdapter(cmdskill);
        DataTable dsskill = new DataTable();
        daskill.Fill(dsskill);
        if (dsskill.Rows.Count > 0)
        {
            ddSkillType.DataSource = dsskill;
            ddSkillType.DataTextField = "Name";
            ddSkillType.DataValueField = "ID";
            ddSkillType.DataBind();
        }
        ddSkillType.Items.Insert(0, "-Select-");
        ddSkillType.Items[0].Value = "0";
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string insertquery = "insert into SkillName values( '" + ddSkillType.SelectedValue + "' , '" + txtName.Text + "')";
        cmd = new SqlCommand(insertquery, con);
        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        fillgrid();
        clear();
        lblmessage.Visible = true;
        lblmessage.Text = "Record inserted successfully.";

        Panel1.Visible = false;
        Button1.Visible = true;
        lbllegend.Text = "";
    }
    protected void fillskillfilter()
    {
        ddSkillTypeFilter.Items.Clear();
        string strskilltype = "select Name,ID from SkillType where BusinessId = '" + ddBusiness.SelectedValue + "'";

        DataTable dtskilltype = new DataTable();
        SqlDataAdapter daskilltype = new SqlDataAdapter(strskilltype, con);
        daskilltype.Fill(dtskilltype);
        if (dtskilltype.Rows.Count > 0)
        {
            ddSkillTypeFilter.DataSource = dtskilltype;
            ddSkillTypeFilter.DataTextField = "Name";
            ddSkillTypeFilter.DataValueField = "ID";
            ddSkillTypeFilter.DataBind();
        }
        ddSkillTypeFilter.Items.Insert(0, "All");
        ddSkillTypeFilter.Items[0].Value = "0";
    }
    protected void fillgrid()
    {
        lblBusiness.Text = "All";

        string fill = "select SkillName.*,SkillType.Name as SkillType,WareHouseMaster.Name as BusinessName from SkillName inner join SkillType on SkillType.ID=SkillName.SkillTypeId inner join WareHouseMaster on WareHouseMaster.WareHouseId = SkillType.BusinessID";

        if (ddBusiness.SelectedIndex > 0)
        {
            lblBusiness.Text = ddBusiness.SelectedItem.Text;

            fill += " where SkillType.BusinessID = '" + ddBusiness.SelectedValue + "'";
        }
        if (ddSkillTypeFilter.SelectedIndex > 0)
        {
            fill += " and SkillType.ID = '" + ddSkillTypeFilter.SelectedValue + "'";
        }
        da = new SqlDataAdapter(fill, con);
        dt = new DataTable();
        da.Fill(dt);


        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataSource = myDataView;
        GridView1.DataBind();
    }
    protected void clear()
    {
        txtName.Text = "";
        lblmessage.Visible = false;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Visible == true)
        {
            clear();
        }
        if (btnupdate.Visible == true)
        {

            btnupdate.Visible = false;
            btnsubmit.Visible = true;
            clear();

        }
        lblmessage.Visible = false;
        Panel1.Visible = false;
        Button1.Visible = true;
        lbllegend.Text = "";
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {
            Panel1.Visible = true;
            Button1.Visible = false;
            lbllegend.Text = "Edit Skill Name";
            lblmessage.Text = "";

            int i = Convert.ToInt32(e.CommandArgument);
            Label9.Text = i.ToString();

            da = new SqlDataAdapter("select SkillType.BusinessID,SkillName.* from SkillName inner join SkillType on SkillType.ID=SkillName.SkillTypeID where SkillName.ID=" + i, con);
            dt = new DataTable();
            da.Fill(dt);

            txtName.Text = dt.Rows[0]["SkillName"].ToString();
            fillskill();
            ddSkillType.SelectedIndex = ddSkillType.Items.IndexOf(ddSkillType.Items.FindByValue(dt.Rows[0]["SkillTypeId"].ToString()));
            ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByValue(dt.Rows[0]["BusinessId"].ToString()));
            txtName.Focus();
        }
        if (e.CommandName == "Delete")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            Label9.Text = i.ToString();

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            string del = "delete from SkillName where ID =" + i;
            SqlCommand cmddel = new SqlCommand(del, con);

            cmddel.ExecuteNonQuery();
            con.Close();
            fillgrid();
            lblmessage.Visible = true;
            lblmessage.Text = "Record deleted successfully.";
        }


    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        btnsubmit.Visible = false;
        btnupdate.Visible = true;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("update SkillName set SkillName='" + txtName.Text + "' , SkillTypeId='" + ddSkillType.SelectedValue + "' where  ID='" + Label9.Text + "' ", con);
        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        fillgrid();
        clear();
        lblmessage.Visible = true;
        lblmessage.Text = "Record updated successfully.";
        btnsubmit.Visible = true;
        btnupdate.Visible = false;

        Panel1.Visible = false;
        Button1.Visible = true;
        lbllegend.Text = "";
    }


    protected void btnPrintVersion_Click(object sender, EventArgs e)
    {
        if (btnPrintVersion.Text == "Printable Version")
        {
            btnPrintVersion.Text = "Hide Printable Version";
            btnPrint.Visible = true;
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
            btnPrintVersion.Text = "Printable Version";
            btnPrint.Visible = false;
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
        fillgrid();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddSkillTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddBusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillskillfilter();
        fillgrid();
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillskill();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Button1.Visible = false;
        lbllegend.Text = "Add New Skill Name";
    }
}
