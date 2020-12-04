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

public partial class ShoppingCart_Admin_frmsmallmessagetype1 : System.Web.UI.Page
{
    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection conn;
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
        conn = pgcon.dynconn;

        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            lblCompany.Text = Session["Cname"].ToString();
            fillgrid();

        }

    }
    protected void Button26_Click(object sender, EventArgs e)
    {
        string st1 = "select * from SmallMessageType where Smallmesstype='" + txtmessagetype.Text + "' and Company_id='" + Session["Comid"].ToString() + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }
        else
        {
            String str = "Insert Into SmallMessageType (Smallmesstype,Company_id)values('" + txtmessagetype.Text + "','" + Session["Comid"].ToString() + "')";
            SqlCommand cmd = new SqlCommand(str, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmd.ExecuteNonQuery();
            conn.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
            clearall();
            fillgrid();
            btnmsgtype.Visible = true;
            panelforadd.Visible = false;
            lbllegend.Visible = false;
        }



    }
    protected void clearall()
    {
        txtmessagetype.Text = "";

    }
    private int GetRowCount(string str)
    {
        int count = 0;
        DataTable dte = new DataTable();
        dte = select(str);
        if (dte.Rows.Count > 0)
        {
            count += Convert.ToInt32(dte.Rows[0]["ci"]);
        }
        ViewState["count"] = count;
        return count;

    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression, string query)
    {
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));

        dt.Columns.Remove("ROW_NUM");

        return dt;

        ViewState["dt"] = dt;
    }
    protected void fillgrid()
    {
        string st1 = " SmallMessageType.* from SmallMessageType  where Company_id='" + Session["comid"] + "'";

        string str3 = "select count(SmallMessageType.SmallmesstypeId) as ci from SmallMessageType  where Company_id='" + Session["comid"] + "'";

        GridView1.VirtualItemCount = GetRowCount(str3);

        string sortExpression = " Smallmesstype asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt1 = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, st1);

            DataView myDataView = new DataView();
            myDataView = dt1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataSource = myDataView;
            GridView1.DataBind();
        }
        else         
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }

    protected DataTable select(string qu)
    {

        SqlCommand cmd = new SqlCommand(qu, conn);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();

        adp.Fill(dt);

        return dt;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {


            ViewState["Id"] = Convert.ToInt32(e.CommandArgument);

            string streditallow = "select * from SmallMessageType where Smallmesstype In ('In Phone','Out Phone','In Email','Out Email','In Visit','Out Visit','In Fax','Out Fax') and  SmallmesstypeId='" + ViewState["Id"] + "'";
            SqlCommand cmdeditallow = new SqlCommand(streditallow, conn);
            SqlDataAdapter adpeditallow = new SqlDataAdapter(cmdeditallow);
            DataTable dseditallow = new DataTable();
            adpeditallow.Fill(dseditallow);

            if (dseditallow.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry you can not edit permanent record.";

            }
            else
            {
                lblmsg.Text = "";
                btnmsgtype.Visible = false;
                lbllegend.Visible = true;
                panelforadd.Visible = true;

                lbllegend.Text = "Edit Message Type";

                Button27.Visible = true;
                Button26.Visible = false;

                string str12 = "select * from SmallMessageType where SmallmesstypeId='" + ViewState["Id"] + "'";
                SqlCommand cmd1 = new SqlCommand(str12, conn);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                DataTable ds1 = new DataTable();
                adp1.Fill(ds1);
                txtmessagetype.Text = ds1.Rows[0]["Smallmesstype"].ToString();
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string streditallow = "select * from SmallMessageType where Smallmesstype In ('In Phone','Out Phone','In Email','Out Email','In Visit','Out Visit','In Fax','Out Fax') and SmallmesstypeId='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
        SqlCommand cmdeditallow = new SqlCommand(streditallow, conn);
        SqlDataAdapter adpeditallow = new SqlDataAdapter(cmdeditallow);
        DataTable dseditallow = new DataTable();
        adpeditallow.Fill(dseditallow);

        if (dseditallow.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry you can not delete permanent record.";

        }
        else
        {
            string st2 = "Delete from SmallMessageType where SmallmesstypeId='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
            SqlCommand cmd2 = new SqlCommand(st2, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmd2.ExecuteNonQuery();
            conn.Close();
            GridView1.EditIndex = -1;
            fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void Button27_Click(object sender, EventArgs e)
    {
        string st1 = "select * from SmallMessageType where Smallmesstype='" + txtmessagetype.Text + "' and Company_id='" + Session["Comid"].ToString() + "' and SmallmesstypeId<>'" + ViewState["Id"] + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, Record already exists";
        }
        else
        {
            String str = "Update SmallMessageType set Smallmesstype='" + txtmessagetype.Text + "',Company_id='" + Session["Comid"].ToString() + "' where SmallmesstypeId='" + ViewState["Id"] + "'";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";
            clearall();
            fillgrid();
            Button27.Visible = false;
            Button26.Visible = true;

            btnmsgtype.Visible = true;
            panelforadd.Visible = false;
            lbllegend.Visible = false;
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
    }
    protected void Button28_Click(object sender, EventArgs e)
    {
        btnmsgtype.Visible = true;
        panelforadd.Visible = false;
        lbllegend.Visible = false;

        txtmessagetype.Text = "";
        lblmsg.Text = "";
        Button27.Visible = false;
        Button26.Visible = true;

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
    protected void btnprintableversion_Click(object sender, EventArgs e)
    {
        if (btnprintableversion.Text == "Printable Version")
        {


            btnprintableversion.Text = "Hide Printable Version";
            Button7.Visible = true;

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;

            fillgrid();
            if (GridView1.Columns[1].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[1].Visible = false;
            }
            if (GridView1.Columns[2].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[2].Visible = false;
            }
        }
        else
        {
            btnprintableversion.Text = "Printable Version";
            Button7.Visible = false;


            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;

            fillgrid();
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[1].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[2].Visible = true;
            }
        }
    }
    protected void btnmsgtype_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        btnmsgtype.Visible = false;
        panelforadd.Visible = true;
        lbllegend.Visible = true;
        lbllegend.Text = "Add New Message Type";

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}
