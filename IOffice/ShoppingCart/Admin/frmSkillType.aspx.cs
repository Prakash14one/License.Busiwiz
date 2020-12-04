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
            fillwarehouse();
            fillwarehouse1();
            fillgrid();
        }
    }
    protected void fillwarehouse()
    {

        ddBusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddBusiness.DataSource = ds;
        ddBusiness.DataTextField = "Name";
        ddBusiness.DataValueField = "WareHouseId";
        ddBusiness.DataBind();
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddBusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
     
    }

    protected void fillwarehouse1()
    {
        ddBusinessfilter.Items.Clear();

        DataTable ds = ClsStore.SelectStorename();

        ddBusinessfilter.DataSource = ds;
        ddBusinessfilter.DataTextField = "Name";
        ddBusinessfilter.DataValueField = "WarehouseId";
        ddBusinessfilter.DataBind();
        ddBusinessfilter.Items.Insert(0, "All");
        ddBusinessfilter.Items[0].Value = "0";
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("insert into SkillType values( '" + txtName.Text + "' ,'" + ddBusiness.SelectedValue + "' , '" + Session["Comid"].ToString() + "')", con);
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

    protected void fillgrid()
    {
        lblBusiness.Text = "All";

        string a = "select SkillType.*,WareHouseMaster.Name as BusinessName from SkillType inner join WareHouseMaster on WareHouseMaster.WareHouseId = SkillType.BusinessID where SkillType.compid='" + Session["Comid"] + "'";

        if (ddBusinessfilter.SelectedIndex > 0)
        {
            lblBusiness.Text = ddBusinessfilter.SelectedItem.Text;

            a += " and SkillType.BusinessId = '" + ddBusinessfilter.SelectedValue + "'";
        }

        a += " order by WareHouseMaster.Name,SkillType.Name asc";

        da = new SqlDataAdapter(a, con);
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
        lblmessage.Text = "";
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "Edit Skill Type";
            Panel1.Visible = true;
            Button1.Visible = false;
            lblmessage.Text = "";

            int i = Convert.ToInt32(e.CommandArgument);
            Label9.Text = i.ToString();


            da = new SqlDataAdapter("select * from SkillType where ID=" + i, con);
            dt = new DataTable();
            da.Fill(dt);

            txtName.Text = dt.Rows[0]["Name"].ToString();
            fillwarehouse();
            ddBusiness.SelectedIndex = ddBusiness.Items.IndexOf(ddBusiness.Items.FindByValue(dt.Rows[0]["BusinessId"].ToString()));
            txtName.Focus();
        }
        if (e.CommandName == "Delete")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            Label9.Text = i.ToString();

            SqlDataAdapter da = new SqlDataAdapter("select * from SkillName where SkillTypeID='" + i + "'", con);
            DataTable dtf = new DataTable();
            da.Fill(dtf);

            if (dtf.Rows.Count > 0)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Sorry, you are unable to delete this skill type as there are skill names exist for this skill type.";
            }
            else
            {

                string del = "delete from SkillType where ID =" + i;
                SqlCommand cmddel = new SqlCommand(del, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddel.ExecuteNonQuery();
                con.Close();
                fillgrid();
                lblmessage.Visible = true;
                lblmessage.Text = "Record Deleted Successfully.";
            }
        }


    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        btnsubmit.Visible = false;
        btnupdate.Visible = true;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("update SkillType set Name='" + txtName.Text + "' , BusinessId='" + ddBusiness.SelectedValue + "' where  ID='" + Label9.Text + "' ", con);
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
        btnupdate.Visible = false;
        btnsubmit.Visible = true;

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
            if (GridView1.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[2].Visible = false;
            }
            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[3].Visible = false;
            }

        }
        else
        {

            btnPrintVersion.Text = "Printable Version";
            btnPrint.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[2].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
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
    protected void ddBusinessfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Button1.Visible = false;
        lbllegend.Text = "Add New Skill Type";
        lblmessage.Text = "";
        fillwarehouse();
    }
}
