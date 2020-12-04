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

public partial class Add_Department : System.Web.UI.Page
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
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        compid = Session["comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        lblmsg.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            lblcomname.Text = Convert.ToString(Session["Cname"]);
            //  checkaccess();
            fillddl();
            fill();

        }
    }
    //protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void fillddl()
    {
        string str = "select BalanceLimitTypeId,BalanceLimitType from BalanceLimitType where compid='" + Session["comid"] + "' order by BalanceLimitType ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        DropDownList1.DataSource = dt;
        DropDownList1.DataTextField = "BalanceLimitType";
        DropDownList1.DataValueField = "BalanceLimitTypeId";
        DropDownList1.DataBind();

        DropDownList1.Items.Insert(0, "All");
        DropDownList1.SelectedItem.Value = "0";
        fill();

    }
    protected void fill()
    {
        Label3.Text = DropDownList1.SelectedItem.Text;
        string str = "";
        if (DropDownList1.SelectedIndex > 0)
        {
            str = "select BalanceLimitTypeId,BalanceLimitType from BalanceLimitType where BalanceLimitTypeId='" + DropDownList1.SelectedValue + "' and  compid='" + Session["comid"] + "' ";
        }
        else
        {
            str = "select BalanceLimitTypeId,BalanceLimitType from BalanceLimitType  where compid='" + Session["comid"] + "' order by BalanceLimitType ";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
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

        }

        GridView1.DataBind();


    }
    //protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }
        if (e.CommandName == "Edit1")
        {
             ViewState["editid"] = Convert.ToInt32(e.CommandArgument);



             string str = "select * from BalanceLimitType where BalanceLimitTypeId='" + ViewState["editid"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                TextBox1.Text = dt.Rows[0]["BalanceLimitType"].ToString();

                ImageButton3.Visible = false;
                Button4.Visible = true;
            }

        }
        if (e.CommandName == "Delete1")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["id"] = GridView1.SelectedIndex;


            if (ViewState["id"] != "")
            {
                SqlCommand cmd = new SqlCommand("select DeleteAllowed,BalanceLimitId from Fixeddata where BalanceLimitId = '" + ViewState["id"].ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["DeleteAllowed"].ToString() == "0")
                    {
                        fill();
                        delete();

                    }
                    else if (dt.Rows[0]["DeleteAllowed"].ToString() == "")
                    {
                        fill();
                        delete();

                    }
                    
                }
                else
                {

                    fill();
                    delete();

                }
            }

            delete();
        }
    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fill();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //Label l11 = (Label)GridView1.Rows[e.RowIndex].FindControl("l1");
        
        //ViewState["id"] = Convert.ToInt32(l11.Text);

        //if (l11.Text != "")
        //{
        //    SqlCommand cmd = new SqlCommand("select DeleteAllowed,BalanceLimitId from Fixeddata where BalanceLimitId = '" + l11.Text + "'", con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    if (dt.Rows.Count > 0)
        //    {
        //        if (dt.Rows[0]["DeleteAllowed"].ToString() == "0")
        //        {
        //            fill();
        //            delete();
                    
        //        }
        //        else if (dt.Rows[0]["DeleteAllowed"].ToString() == "")
        //        {
        //            fill();
        //            delete();
                    
        //        }
        //        else
        //        {
        //            ModalPopupExtender2.Show();

        //        }
        //    }
        //    else
        //    {
                
        //        fill();
        //        delete();
                
        //    }
        //}
        
        //delete();
    }
    protected void delete()
    {
        SqlCommand cmd = new SqlCommand("delete from BalanceLimitType where BalanceLimitTypeId=" + ViewState["id"] + "", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record deleted successfully";
        GridView1.EditIndex = -1;
        
        fill();
        fillddl();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        //Label l11 = (Label)GridView1.Rows[e.NewEditIndex].FindControl("l1");
        //if (l11.Text != "")
        //{
        //    SqlCommand cmd = new SqlCommand("select EditAllowed,BalanceLimitId from Fixeddata where BalanceLimitId = '" + l11.Text + "'", con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    if (dt.Rows.Count > 0)
        //    {
        //        if (dt.Rows[0]["EditAllowed"].ToString() == "0")
        //        {
        //            GridView1.EditIndex = e.NewEditIndex;
        //            fill();
        //        }
        //        else if (dt.Rows[0]["EditAllowed"].ToString() == "")
        //        {
        //            GridView1.EditIndex = e.NewEditIndex;
        //            fill();
        //        }
        //        else
        //        {
        //            ModalPopupExtender3.Show();

        //        }
        //    }
        //    else
        //    {
        //        GridView1.EditIndex = e.NewEditIndex;
        //        fill();
        //    }
        //}
        
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //TextBox cityname1 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox1");
        //int dk = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        //string str = "select BalanceLimitType from BalanceLimitType where BalanceLimitType='" + cityname1.Text + "' and compid='" + Session["comid"] + "' and  [BalanceLimitTypeId] <>'" + dk.ToString() + "' ";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record already exist";
        //    TextBox1.Text = "";


        //}
        //else
        //{




        //    TextBox cityname = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox1");
        //    string s1 = "UPDATE BalanceLimitType SET [BalanceLimitType] ='" + cityname.Text + "' " +
        //        "WHERE [BalanceLimitTypeId] ='" + dk.ToString() + "'";


        //    SqlCommand cmd333 = new SqlCommand(s1, con);
        //    if (con.State.ToString() != "Open")
        //    {
        //        con.Open();
        //    }
        //    cmd333.ExecuteNonQuery();
        //    con.Close();

        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record updated successfully";
        //    GridView1.EditIndex = -1;


        //    fill();

        //}
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fill();
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
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        TextBox1.Text = "";
        lblmsg.Visible = false;
        fill();

    }
    
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        fill();
    }
    
    protected void checkaccess()
    {
        //string stacess = " select PageControlMaster.ControlName,Role_Page_Contreol_Access.ActiveDeactive  from  Role_Page_Contreol_Access inner join PageControlMaster on Role_Page_Contreol_Access.Page_Control_id=PageControlMaster.PageControl_id  ";

        string stacess = " select PageControlMaster.ControlName,Role_Page_Contreol_Access.ActiveDeactive,Role_Page_Contreol_Access.Role_id,Role_Page_Contreol_Access.Page_id from  Role_Page_Contreol_Access " +
        " inner join PageControlMaster on Role_Page_Contreol_Access.Page_Control_id=PageControlMaster.PageControl_id " +
        " inner join User_Role on User_Role.Role_id=Role_Page_Contreol_Access.Role_id where User_Role.User_id='" + Session["userid"] + "' and Role_Page_Contreol_Access.Page_id='208' ";

        SqlCommand cmdaccess = new SqlCommand(stacess, con);
        SqlDataAdapter daaccess = new SqlDataAdapter(cmdaccess);
        DataTable dsaccess = new DataTable();
        daaccess.Fill(dsaccess);
        int i;
        for (i = 0; i <= dsaccess.Rows.Count - 1; i++)
        {
            if (dsaccess.Rows[i]["ControlName"].ToString() == "Edit")
            {
                if (dsaccess.Rows[i]["ActiveDeactive"].ToString() == "False")
                {
                    for (int hr = 0; hr < GridView1.Columns.Count; hr++)
                    {
                        if (GridView1.Columns[hr].HeaderText == "Edit")
                        {
                            GridView1.Columns[hr].Visible = false;
                        }
                    }
                }
            }
        }
    }

    protected void yes_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("delete from BalanceLimitType where BalanceLimitTypeId = " + ViewState["id"] + "", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }

        cmd.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record deleted successfully";
        GridView1.EditIndex = -1;
        
        fill();
        fillddl();
        
      
    }
    protected void ImageButton6_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1222.Hide();
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        string str1 = "SELECT  BalanceLimitType FROM BalanceLimitType where BalanceLimitType='" + TextBox1.Text + "' and compid='" + Session["comid"] + "' order by BalanceLimitType";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";
            TextBox1.Text = "";

        }
        else
        {
            string str = "Insert Into BalanceLimitType(BalanceLimitType,compid) Values ('" + TextBox1.Text + "','" + Session["comid"] + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
            TextBox1.Text = "";
            fill();

        }

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        lblmsg.Visible = false;
        TextBox1.Text = "";
        ImageButton3.Visible = true;
        Button4.Visible = false;
    }
    protected void ImageButton10_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Hide();
    }
    protected void ImageButton11_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[1].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[1].Visible = false;
            }
            if (GridView1.Columns[2].Visible == true)
            {
                ViewState["delHide"] = "tt";
                GridView1.Columns[2].Visible = false;
            }
        }
        else
        {

            pnlgrid.ScrollBars = ScrollBars.Vertical;
            pnlgrid.Height = new Unit(200);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[1].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                GridView1.Columns[2].Visible = true;
            }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand(" update BalanceLimitType set BalanceLimitType='" + TextBox1.Text + "' where BalanceLimitTypeId='" + ViewState["editid"].ToString() + "' and compid='" + Session["comid"].ToString() + "'  ", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }

        cmd.ExecuteNonQuery();
        con.Close();
      
        GridView1.EditIndex = -1;
        fill();
        fillddl();

        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully";

        TextBox1.Text = "";
        ImageButton3.Visible = true;
        Button4.Visible = false;
        
    }
}
