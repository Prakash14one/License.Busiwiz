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

public partial class ShoppingCart_Admin_Default4 : System.Web.UI.Page
{

    SqlConnection con;
    SqlCommand cmd;
    DataTable dt;
    DataSet ds;
    SqlDataAdapter da;
    string compid;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        compid = Session["comid"].ToString();
        Page.Title = pg.getPageTitle(page);

        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
            }


            ViewState["sortOrder"] = "";
            fillgrid();


        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        clsareaofstudies objarea = new clsareaofstudies();
        objarea.Name = txtname.Text;
        objarea.Active = Convert.ToInt32(chkstatus.Checked);
        objarea.executenoninsert();

        //cmd = new SqlCommand("insert into AreaofStudiesTbl(Name,Active) values('" + txtname.Text + "' , '" + chkstatus.Checked + "')", con);
        //if (cmd.Connection.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmd.ExecuteNonQuery();
        //con.Close();


        fillgrid();
        clear();

        Panel1.Visible = false;
        Button2.Visible = true;
        lblmessage.Text = "Record inserted successfully.";
        lblmessage.Visible = true;
        lbllegend.Text = "";
    }

    protected void fillgrid()
    {
        string st1 = "";

        lblstat.Text = "All";

        if (ddlstatus_search.SelectedIndex > 0)
        {
            lblstat.Text = ddlstatus_search.SelectedItem.Text;
            st1 += " where AreaofStudiesTbl.Active='" + ddlstatus_search.SelectedValue + "'";
        }
        
        string str = " select ID,Name,case when(Active = '1') then 'Active' else 'Inactive' end as Status from AreaofStudiesTbl " + st1 + "";
        str += " order by AreaofStudiesTbl.Name asc";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
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
        txtname.Text = "";
        chkstatus.Checked = false;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        clsareaofstudies objarea = new clsareaofstudies();

        if (e.CommandName == "Edit")
        {
            Panel1.Visible = true;
            Button2.Visible = false;
            lbllegend.Text = "Edit Study";
            lblmessage.Text = "";

            int i = Convert.ToInt32(e.CommandArgument);
            Label8.Text = i.ToString();

            objarea.ID = i;

            //   da = new SqlDataAdapter("select * from AreaofStudiesTbl where ID=" +i,con);
            dt = new DataTable();
            dt = objarea.rowcmod_selc();
            //    da.Fill(dt);


            txtname.Text = dt.Rows[0]["Name"].ToString();
            chkstatus.Checked = Convert.ToBoolean(dt.Rows[0]["Active"]);

        }

        if (e.CommandName == "Delete")
        {
            int i = Convert.ToInt32(e.CommandArgument);

            string strwh = "select * from LevelofEducationTBL where AreaofStudyID='" + i + "' ";
            SqlCommand cmdwh = new SqlCommand(strwh, con);
            SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            DataTable dts = new DataTable();
            adpwh.Fill(dts);

            if (dts.Rows.Count == 0)
            {
                string strwh3 = "select * from SpecialisedSubjectTBL where AreaofStudiesId='" + i + "' ";
                SqlCommand cmdwh3 = new SqlCommand(strwh3, con);
                SqlDataAdapter adpwh3 = new SqlDataAdapter(cmdwh3);
                DataTable dts3 = new DataTable();
                adpwh3.Fill(dts3);

                if (dts3.Rows.Count == 0)
                {
                    SqlCommand cmd = new SqlCommand("delete from AreaofStudiesTbl where ID=" + i, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    lblmessage.Visible = true;
                    lblmessage.Text = "Record deleted successfully.";

                }
                else
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "sorry,you are not able to delete this record as child record exist using this record.";
                }
            }
            else
            {
                lblmessage.Visible = true;
                lblmessage.Text = "sorry,you are not able to delete this record as child record exist using this record.";
            }
        }

        fillgrid();

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        btnsubmit.Visible = false;
        btnupdate.Visible = true;

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        clsareaofstudies objarea = new clsareaofstudies();
        objarea.Name = txtname.Text;
        objarea.Active = Convert.ToInt32(chkstatus.Checked);
        objarea.ID = Convert.ToInt32(Label8.Text);
        objarea.executenonupdte();

        //cmd = new SqlCommand("update AreaofStudiesTbl set Name='" + txtname.Text + "' , Active='" + chkstatus.Checked + "' where ID='" + Label8.Text + "'", con);
        //if (cmd.Connection.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmd.ExecuteNonQuery();
        //con.Close();

        fillgrid();
        clear();
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
        Panel1.Visible = false;
        Button2.Visible = true;
        lblmessage.Text = "Record updated successfully.";
        lblmessage.Visible = true;
        lbllegend.Text = "";
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
        Button2.Visible = true;
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Button2.Visible = false;
        lblmessage.Visible = false;
        lbllegend.Text = "Add New Study";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
