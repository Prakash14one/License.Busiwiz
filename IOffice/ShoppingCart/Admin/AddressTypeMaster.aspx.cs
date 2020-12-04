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
using System.Globalization;

public partial class ShoppingCart_Admin_AddressTypeMaster : System.Web.UI.Page
{
    
    SqlConnection con;
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/ShoppingCart/Admin/ShoppingCartLogin.aspx");
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
        Label1.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            lblCompany.Text = Session["Cname"].ToString();
            filldata();
        }
    }

    public void filldata()
    {
        
        string sg90 = "select AddressTypeMasterId,  Name from AddressTypeMaster  where compid='" + compid + "' order by  Name ";
        SqlCommand cmd23490 = new SqlCommand(sg90, con);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();

        adp23490.Fill(dt23490);

        DataView myDataView = new DataView();
        myDataView = dt23490.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataSource = myDataView;
        GridView1.DataBind();

    }


    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //if (System.Windows.Forms.MessageBox.Show("are you ","conform delete!",System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Warning)==System.Windows.Forms.DialogResult.Yes)
        //{

        //    //GridView1.DeleteRow();
        //}
        //else
        //{
        //}
    }


    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        int i = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        ViewState["dkdd"] = i;

        if (ViewState["dkdd"].ToString() != "")
        {
            SqlCommand cmddel = new SqlCommand("select DeleteAllowed,AddressTypeId from Fixeddata where AddressTypeId='" + ViewState["dkdd"].ToString() + "'", con);
            SqlDataAdapter dtpdel = new SqlDataAdapter(cmddel);
            DataTable dtdel = new DataTable();
            dtpdel.Fill(dtdel);
            if (dtdel.Rows.Count > 0)
            {
                if (dtdel.Rows[0]["DeleteAllowed"].ToString() == "0")
                {
                    //ModalPopupExtender1222.Show();
                    delete();
                }
                else
                {
                    Label1.Text = "Sorry, you cannot delete this record.";
                    //ModalPopupExtender2.Show();
                }
            }
            else
            {
                //ModalPopupExtender1222.Show();
                delete();
            }
        }
    }
    protected void delete()
    {
        SqlCommand cmdedit = new SqlCommand("select AddressTypeMasterId from CompanyWebsiteAddressMaster where AddressTypeMasterId='" + Convert.ToString(ViewState["dkdd"]) + "'", con);
        SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
        DataTable dtedit = new DataTable();
        dtpedit.Fill(dtedit);
        if (dtedit.Rows.Count == 0)
        {
            string str = "delete from AddressTypeMaster where AddressTypeMasterId='" + Convert.ToString(ViewState["dkdd"]) + "' ";
            DataSet ds = new DataSet();
            SqlCommand cmdd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdd.ExecuteNonQuery();
            con.Close();
            //GridView1.DataSource = ds;
            filldata();
            //GridView1.EditIndex = -1;
            Label1.Visible = true;
            Label1.Text = "Record deleted successfully";
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Sorry, first delete record from Business Address Master";
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



    protected void ImageButton8_Click(object sender, EventArgs e)
    {
        string sg90 = "select Name from AddressTypeMaster where Name='" + Textname.Text + "' and compid='" + Session["comid"] + "'";
        SqlCommand cmd23490 = new SqlCommand(sg90, con);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();
        adp23490.Fill(dt23490);
        if (dt23490.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
            Textname.Text = "";
        }
        else
        {
            SqlCommand mycmd = new SqlCommand("Sp_Insert_AddressTypeMaster", con);
            mycmd.CommandType = CommandType.StoredProcedure;
            mycmd.Parameters.AddWithValue("@Name", Textname.Text);
            mycmd.Parameters.AddWithValue("@compid", compid);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }

            mycmd.ExecuteNonQuery();
            con.Close();
            Label1.Visible = true;
            Label1.Text = "Record inserted successfully";
            filldata();
            Textname.Text = "";
            pnladd.Visible = false;
            btnadd.Visible = true;
            lbladd.Text = "";
        }
    }
    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        Textname.Text = "";
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
       
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label1.Text = "";
        int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        ViewState["editid"] = GridView1.DataKeys[e.NewEditIndex].Value.ToString();
        if (dk1.ToString() != "")
        {
            SqlCommand cmdedit = new SqlCommand("select EditAllowed,AddressTypeId from Fixeddata where AddressTypeId='" + dk1 + "'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);
            if (dtedit.Rows.Count > 0)
            {
                if (dtedit.Rows[0]["EditAllowed"].ToString() == "0")
                {

                }
                else
                {
                    Label1.Text = "Sorry, you cannot edit this record.";
                    //ModalPopupExtender3.Show();
                    //GridView1.EditIndex = -1;
                    //GridView1.DataBind();
                    filldata();
                }
            }
            else
            {
                string str = "Select * from AddressTypeMaster where AddressTypeMasterId='" + dk1 + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Textname.Text = dt.Rows[0]["Name"].ToString();
                    pnladd.Visible = true;
                    btnadd.Visible = false;
                    lbladd.Text = "Edit Address Type";
                    btnsubmit.Visible = false;
                    btnupdate.Visible = true;
                }
                //GridView1.EditIndex = e.NewEditIndex;
                //filldata();
            }
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        filldata();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        filldata();

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button1.Visible = true;
            if (GridView1.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[2].Visible = false;
            }
            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["delHide"] = "tt";
                GridView1.Columns[3].Visible = false;
            }
        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(200);

            Button4.Text = "Printable Version";
            Button1.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[2].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            btnadd.Visible = false;
            lbladd.Text = "Add New Address Type";
        }
        else
        {
            pnladd.Visible = false;
            btnadd.Visible = true;
            lbladd.Text = "";
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {

        string sg90 = "select Name from AddressTypeMaster where Name='" + Textname.Text + "' and compid='" + Session["comid"] + "' and AddressTypeMasterId<>'" + ViewState["editid"] + "' ";
        SqlCommand cmd23490 = new SqlCommand(sg90, con);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();
        adp23490.Fill(dt23490);
        if (dt23490.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
            Textname.Text = "";
        }
        else
        {
            string str = "update  AddressTypeMaster set Name='" + Textname.Text + "'  where AddressTypeMasterId='" + ViewState["editid"] + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            Label1.Visible = true;
            Label1.Text = "Record updated successfully";
            filldata();
            Textname.Text = "";
            btnupdate.Visible = false;
            btnsubmit.Visible = true;
            pnladd.Visible = false;
            btnadd.Visible = true;
            lbladd.Text = "";
        }
    }
}
