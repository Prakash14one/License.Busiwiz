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

public partial class ShoppingCart_Admin_EmployeeType : System.Web.UI.Page
{
    SqlConnection con;

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


        Page.Title = pg.getPageTitle(page);
        Label1.Visible = false;

        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            lblcomname.Text = Convert.ToString(Session["Cname"]);
            ViewState["sortOrder"] = "";
            Fillddl();
            FillGrid();
        }
    }
    protected void Fillddl()
    {

        string str = " Select EmployeeTypeId, EmployeeTypeName From EmployeeType where CID='" + Session["Comid"] + "' order by EmployeeTypeName";
        SqlCommand cmd = new SqlCommand(str);
        cmd.Connection = con;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataSet ds = new DataSet();

        da.Fill(ds, "EmployeeType");
        //if (ds.Tables[0].Rows.Count < 0)
        //{
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "EmployeeTypeName";
        DropDownList1.DataValueField = "EmployeeTypeId";
        DropDownList1.DataBind();

        DropDownList1.Items.Insert(0, "All");
        DropDownList1.SelectedItem.Value = "0";
        FillGrid();
    }
    protected void FillGrid()
    {
        string str = "";
        //if (DropDownList1.SelectedIndex > 0)
        //{
        //    str = " Select EmployeeTypeId, EmployeeTypeName From EmployeeType where EmployeeTypeId='" + DropDownList1.SelectedValue + "' order by EmployeeTypeName";
        //}
        //else
        //{
        //   str = " Select EmployeeTypeId, EmployeeTypeName From EmployeeType where CID='" + Session["Comid"] + "' order by EmployeeTypeName ";
        //}

        str = "  EmployeeTypeId, EmployeeTypeName From EmployeeType where CID='" + Session["Comid"] + "'";
        //order by EmployeeTypeName ";

        string str2 = " select count(EmployeeTypeId) as ci from EmployeeType where CID='" + Session["Comid"] + "'";

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " EmployeeTypeName asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str);

            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;
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

        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);
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

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {



    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGrid();

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

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ViewState["Id"] = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        if (ViewState["Id"].ToString() != "")
        {
            SqlCommand cmddel1 = new SqlCommand("select [EmployeeTypeId] from [EmployeeMaster] where [EmployeeTypeId]='" + ViewState["Id"].ToString() + "'", con);
            SqlDataAdapter dtpdel1 = new SqlDataAdapter(cmddel1);
            DataTable dtde1l = new DataTable();
            dtpdel1.Fill(dtde1l);
            if (dtde1l.Rows.Count == 0)
            {
                SqlCommand cmddel = new SqlCommand("select DeleteAllowed,EmployeeTypeId from Fixeddata where EmployeeTypeId='" + ViewState["Id"].ToString() + "'", con);
                SqlDataAdapter dtpdel = new SqlDataAdapter(cmddel);
                DataTable dtdel = new DataTable();
                dtpdel.Fill(dtdel);
                if (dtdel.Rows.Count > 0)
                {
                    if (dtdel.Rows[0]["DeleteAllowed"].ToString() == "0")
                    {
                        FillGrid();

                        ViewState["id"] = GridView1.DataKeys[e.RowIndex].Value;

                        //ModalPopupExtender1222.Show();
                        delete();

                        GridView1.EditIndex = -1;
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Sorry,you are not allowed to delete this record.";
                        // ModalPopupExtender2.Show();

                    }
                }
                else
                {
                    FillGrid();

                    ViewState["id"] = GridView1.DataKeys[e.RowIndex].Value;

                    //ModalPopupExtender1222.Show();
                    delete();

                    GridView1.EditIndex = -1;
                }
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "Sorry,you are not allowed to delete this record.";
                // ModalPopupExtender2.Show();

            }

        }

    }

    protected void delete()
    {
        SqlCommand cmd = new SqlCommand("Delete from EmployeeType where EmployeeTypeId = " + ViewState["id"] + "", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        Label1.Visible = true;
        Label1.Text = "Record deleted successfully";
        tbEmployeeTypeName.Text = "";
        GridView1.EditIndex = -1;

        FillGrid();
        Fillddl();
    }
    protected void yes_Click(object sender, ImageClickEventArgs e)
    {

        //SqlCommand cmd = new SqlCommand("Delete from EmployeeType where EmployeeTypeId = " + ViewState["id"] + "", con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmd.ExecuteNonQuery();
        //con.Close();

        //Label1.Visible = true;
        //Label1.Text = "Record deleted successfully";
        //tbEmployeeTypeName.Text = "";
        //GridView1.EditIndex = -1;

        //FillGrid();
        //Fillddl();
        //ModalPopupExtender1222.Hide();

    }

    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1222.Hide();
        Label1.Visible = false;
    }



    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
        Label1.Visible = false;
    }
    protected void ImageB10_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Hide();
    }
    protected void Ima3_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = " Select EmployeeTypeId, EmployeeTypeName From EmployeeType where EmployeeTypeName = '" + tbEmployeeTypeName.Text + "' and CID='" + Session["Comid"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = " Record already exists";
            tbEmployeeTypeName.Text = "";
            tbEmployeeTypeName.Text = "";
        }
        else
        {
            string str1 = " Insert into EmployeeType(EmployeeTypeName,CID)values ('" + tbEmployeeTypeName.Text + "','" + Session["Comid"] + "') ";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();

            Label1.Visible = true;
            Label1.Text = "Record inserted successfully";
            tbEmployeeTypeName.Text = "";
            //Fillddl();
            FillGrid();
            pnladd.Visible = false;
            lbladd.Text = "";
            btnadd.Visible = true;
            tbEmployeeTypeName.Text = "";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        tbEmployeeTypeName.Text = "";
        Label1.Visible = false;
        pnladd.Visible = false;
        lbladd.Text = "";
        btnadd.Visible = true;
        btnupdate.Visible = false;
        ImageButton1.Visible = true;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label1.Text = "";
        ViewState["id"] = GridView1.DataKeys[e.NewEditIndex].Value.ToString();
        string str = "Select * from EmployeeType where EmployeeTypeId='" + ViewState["id"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            tbEmployeeTypeName.Text = dt.Rows[0]["EmployeeTypeName"].ToString();
            lbladd.Text = "Edit Employee Type";
            pnladd.Visible = true;
            btnadd.Visible = false;
            btnupdate.Visible = true;
            ImageButton1.Visible = false;
        }



    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            FillGrid();

            Button4.Text = "Hide Printable Version";
            Button1.Visible = true;
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

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(300);

            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            FillGrid();

            Button4.Text = "Printable Version";
            Button1.Visible = false;
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
    protected void btnadd_Click(object sender, EventArgs e)
    {
        lbladd.Text = "Add New Employee Type";
        btnadd.Visible = false;
        pnladd.Visible = true;
        Label1.Text = "";
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {

        SqlCommand cmdup = new SqlCommand("select EmployeeTypeName from EmployeeType where EmployeeTypeName='" + tbEmployeeTypeName.Text + "' and EmployeeTypeId <>'" + ViewState["id"] + "' and CID ='" + Session["Comid"] + "'", con);
        SqlDataAdapter dtpup = new SqlDataAdapter(cmdup);
        DataTable dtup = new DataTable();
        dtpup.Fill(dtup);
        if (dtup.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
            // tbEmployeeTypeName.Text = "";
            // GridView1.EditIndex = -1;

            //  FillGrid();
        }
        else
        {
            string str = " Update EmployeeType set EmployeeTypeName = '" + tbEmployeeTypeName.Text + "' where EmployeeTypeId = '" + ViewState["id"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            Label1.Visible = true;
            Label1.Text = "Record updated successfully";
            tbEmployeeTypeName.Text = "";
            FillGrid();
            pnladd.Visible = false;
            btnupdate.Visible = false;
            ImageButton1.Visible = true;
            btnadd.Visible = true;
            lbladd.Text = "";
        }
    }
}
