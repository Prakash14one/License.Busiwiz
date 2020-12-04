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

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!IsPostBack)
        {
            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
            }

            ViewState["sortOrder"] = "";
            fillstore();
            fillstore1();
            fillgrid();
        }
    }

    protected void fillstore()
    {
        ddlbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlbusiness.DataSource = ds;
        ddlbusiness.DataTextField = "Name";
        ddlbusiness.DataValueField = "WareHouseId";
        ddlbusiness.DataBind();


        //ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }

    protected void fillstore1()
    {
        ddlbusiness1.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlbusiness1.DataSource = ds;
        ddlbusiness1.DataTextField = "Name";
        ddlbusiness1.DataValueField = "WareHouseId";
        ddlbusiness1.DataBind();


        //ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlbusiness1.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
        ddlbusiness1.Items.Insert(0, "All");
        ddlbusiness1.Items[0].Value = "0";
        ddlbusiness1.SelectedIndex = 0;
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //clssecuritygatesaddmanage objsecuritygates = new clssecuritygatesaddmanage();
        //objsecuritygates.BusinessID = Convert.ToInt32(ddlbusiness.SelectedValue);
        //objsecuritygates.Name = txtsecure.Text;
        //objsecuritygates.Location = txtlocation.Text;
        //objsecuritygates.Active = Convert.ToInt32(chkstatus.Checked);
        //objsecuritygates.executeinsert();

        cmd = new SqlCommand("insert into SecurityGateMaster(BusinessID,Name,Location,Active,comid) values('" + ddlbusiness.SelectedValue + "' , '" + txtsecure.Text + "' , '" + txtlocation.Text + "' , '" + chkstatus.Checked + "','" + Session["Comid"] + "')", con);
        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        fillgrid();
        clear();
        lblmessage.Text = "Record inserted successfully.";
        lblmessage.Visible = true;
        Panel1.Visible = false;
        Button4.Visible = true;
        lbllegend.Text = "";
    }

    protected void fillgrid()
    {
        Label1.Text = "All";

        string st1 = "";
        string str = "";

        if (ddlbusiness1.SelectedIndex > 0)
        {
            Label1.Text = ddlbusiness1.SelectedItem.Text;

            st1 += " and BusinessID='" + ddlbusiness1.SelectedValue + "'";
        }

        if (ddlstatus_search.SelectedIndex > 0)
        {
            st1 += " and SecurityGateMaster.Active='" + ddlstatus_search.SelectedValue + "' ";
        }

        str = "select WareHouseMaster.Name as BusinessName,SecurityGateMaster.Id,SecurityGateMaster.Name,SecurityGateMaster.Location,case when(SecurityGateMaster.Active = '1') then 'Active' else 'Inactive' end as Status from SecurityGateMaster INNER JOIN WareHouseMaster ON SecurityGateMaster.BusinessID=WareHouseMaster.WareHouseId where SecurityGateMaster.comid='" + Session["Comid"] + "' " + st1 + "";

        SqlDataAdapter da1 = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da1.Fill(dt);

        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataSource = myDataView;
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        clssecuritygatesaddmanage objsecuritygates = new clssecuritygatesaddmanage();

        if (e.CommandName == "Delete")
        {


            int i = Convert.ToInt32(e.CommandArgument);
            objsecuritygates.Id = i;
            objsecuritygates.executedelete();

            //cmd = new SqlCommand("delete from SecurityGateMaster where Id="+i, con);
            //if (cmd.Connection.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmd.ExecuteNonQuery();
            //con.Close();
            fillgrid();
            lblmessage.Text = "Record deleted successfully.";
            lblmessage.Visible = true;
        }

        if (e.CommandName == "Edit")
        {
            lblmessage.Visible = false;

            lbllegend.Text = "Edit Security Gate";

            int i = Convert.ToInt32(e.CommandArgument);
            objsecuritygates.Id = i;

            Label9.Text = i.ToString();

            Panel1.Visible = true;
            Button4.Visible = false;
            //da = new SqlDataAdapter("select * from SecurityGateMaster where Id="+i, con);
            DataTable dt1 = new DataTable();
            dt1 = objsecuritygates.filgrid_securityedit();

            //   da.Fill(dt1);

            fillstore();
            ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dt1.Rows[0]["BusinessID"].ToString()));

            txtsecure.Text = dt1.Rows[0]["Name"].ToString();
            txtlocation.Text = dt1.Rows[0]["Location"].ToString();
            chkstatus.Checked = Convert.ToBoolean(dt1.Rows[0]["Active"]);

            ddlbusiness.Focus();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        btnsubmit.Visible = false;
        btnupdate.Visible = true;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        //clssecuritygatesaddmanage objsecuritygates = new clssecuritygatesaddmanage();

        //objsecuritygates.Id = Convert.ToInt32(Label9.Text);
        //objsecuritygates.BusinessID = Convert.ToInt32(ddlbusiness.SelectedValue);
        //objsecuritygates.Name = txtsecure.Text;
        //objsecuritygates.Location = txtlocation.Text;
        //objsecuritygates.Active = Convert.ToInt32(chkstatus.Checked);
        //objsecuritygates.executeupdate();

        cmd = new SqlCommand("update SecurityGateMaster set BusinessID='" + ddlbusiness.SelectedValue + "' , Name='" + txtsecure.Text + "' , Location='" + txtlocation.Text + "' , Active='" + chkstatus.Checked + "',comid='" + Session["Comid"] + "' where Id='" + Label9.Text + "'", con);
        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        fillgrid();

        Label9.Text = "";

        clear();
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
        lblmessage.Text = "Record updated successfully.";
        lblmessage.Visible = true;
        Panel1.Visible = false;
        Button4.Visible = true;
        lbllegend.Text = "";
    }
    protected void clear()
    {
        ddlbusiness.SelectedIndex = 0;
        txtsecure.Text = "";
        txtlocation.Text = "";
        chkstatus.Checked = false;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Button4.Visible = false;
        lblmessage.Visible = false;
        lbllegend.Text = "Add New Security Gate";
        fillstore();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Visible == true)
        {
            clear();
        }
        if (btnupdate.Visible == true)
        {
            btnsubmit.Visible = true;
            btnupdate.Visible = false;
            clear();
        }

        Panel1.Visible = false;
        Button4.Visible = true;
        lblmessage.Visible = false;
        lbllegend.Text = "";
    }
    protected void ddlbusiness1_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    protected void btnPrintVersion_Click(object sender, EventArgs e)
    {
        if (btnPrintVersion.Text == "Printable Version")
        {
            btnPrintVersion.Text = "Hide Printable Version";
            btnPrint.Visible = true;
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }


        }
        else
        {


            btnPrintVersion.Text = "Printable Version";
            btnPrint.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }

        }
    }
    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
