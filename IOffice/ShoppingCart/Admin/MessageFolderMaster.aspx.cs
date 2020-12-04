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
public partial class ShoppingCart_Admin_MessageFolderMaster : System.Web.UI.Page
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
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            Pagecontrol.dypcontrol(Page, page);
            lblCompany.Text = Session["Comid"].ToString();
            lblBusiness.Text = "ALL";

            fillbusiness();
            string str1 = "SELECT WareHouseId,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";

            SqlCommand cmd11 = new SqlCommand(str1, con);
            cmd11.CommandType = CommandType.Text;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd11);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            ddlfilterstore.DataSource = dt1;
            ddlfilterstore.DataTextField = "Name";
            ddlfilterstore.DataValueField = "WareHouseId";
            ddlfilterstore.DataBind();
            ddlfilterstore.Items.Insert(0, "All");
            ddlfilterstore.Items[0].Value = "0";
            Fillgrid();
        }

    }
    protected void fillbusiness()
    {
        string str = "SELECT Id,Name +':' +InOutCompanyEmail.EmailId as Name FROM WareHouseMaster inner join InOutCompanyEmail on InOutCompanyEmail.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "' and WareHouseMaster.Status=1 order by name";

        // 

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlstore.DataSource = dt;
        ddlstore.DataTextField = "Name";
        ddlstore.DataValueField = "Id";
        ddlstore.DataBind();
    }
    protected void btninsert_Click(object sender, EventArgs e)
    {
        if (btninsert.Text == "Submit")
        {
            DataTable ds = new DataTable();
            ds = select("select * from MessageFolderMaster where InoutcommingId='" + ddlstore.SelectedValue + "' and Foldername='" + txtmessagefolder.Text + "'");
            if (ds.Rows.Count == 0)
            {
                string indd = "insert into MessageFolderMaster(InoutcommingId,Foldername)values('" + ddlstore.SelectedValue + "','" + txtmessagefolder.Text + "')";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(indd, con);
                cmd.ExecuteNonQuery();
                con.Close();
                //     pnlmsg.Visible = true;
                
                labelmsg.Text = "Record inserted successfully";
                btnadddd.Visible = true;
                pnladdd.Visible = false;
                lbllegend.Visible = false;

                Fillgrid();
                Clear();
            }
            else
            {
                //     pnlmsg.Visible = true;
               
                labelmsg.Text = "Record already exists";

            }
        }
        if (btninsert.Text == "Update")
        {
            DataTable ds = new DataTable();
            ds = select("select * from MessageFolderMaster where InoutcommingId='" + ddlstore.SelectedValue + "' and Foldername='" + txtmessagefolder.Text + "' and Id<>'" + ViewState["folid"] + "'");
            if (ds.Rows.Count == 0)
            {
                string indd = "Update  MessageFolderMaster Set InoutcommingId='" + ddlstore.SelectedValue + "',Foldername='" + txtmessagefolder.Text + "' where Id='" + ViewState["folid"] + "'";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(indd, con);
                cmd.ExecuteNonQuery();
                con.Close();
                //     pnlmsg.Visible = true;

                labelmsg.Text = "Record updated successfully";
                btnadddd.Visible = true;
                pnladdd.Visible = false;
                lbllegend.Visible = false;

                btninsert.Text = "Submit";
                Fillgrid();
                Clear();
            }
            else
            {
                //     pnlmsg.Visible = true;
   
                labelmsg.Text = "Record already exists";

            }
        }
        
    }
    protected void Clear()
    {
        txtmessagefolder.Text = "";
        ddlstore.SelectedIndex = 0;
        btninsert.Text = "Submit";
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnadddd.Visible = true;
        lbllegend.Visible = false;
        pnladdd.Visible = false;
        labelmsg.Text = "";
        Clear();
       

    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
    protected void Fillgrid()
    {
        DataTable dt = new DataTable();
        if (ddlfilterstore.SelectedIndex > 0)
        {
            lblBusiness.Text = ddlfilterstore.SelectedItem.Text;
            dt = select("SELECT WareHouseMaster.Name, MessageFolderMaster.*,InOutCompanyEmail.EmailId FROM MessageFolderMaster inner join InOutCompanyEmail on InOutCompanyEmail.Id=MessageFolderMaster.InoutcommingId inner join  WareHouseMaster   on InOutCompanyEmail.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "' and  WareHouseMaster.Status='" + 1 + "' and Whid='" + ddlfilterstore.SelectedValue + "' order by Name,EmailId,Foldername asc");
            //
        }
        else
        {
            dt = select("SELECT WareHouseMaster.Name, MessageFolderMaster.*,InOutCompanyEmail.EmailId FROM MessageFolderMaster inner join InOutCompanyEmail on InOutCompanyEmail.Id=MessageFolderMaster.InoutcommingId inner join  WareHouseMaster   on InOutCompanyEmail.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "' and WareHouseMaster.Status='" + 1 + "' order by Name,EmailId,Foldername asc");
            //comid = '" + Session["comid"] + "'and
        }


        if (dt.Rows.Count > 0)
        {

            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridEmail.DataSource = dt;
            GridEmail.DataBind();
        }
        else
        {
            GridEmail.DataSource = null;
            GridEmail.DataBind();

        }
    }
    protected void GridEmail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            pnladdd.Visible = true;
            lbllegend.Visible = true;
            lbllegend.Text = "Edit Message Folder";
            btnadddd.Visible = false;
            labelmsg.Text = "";

            int mm = Convert.ToInt32(e.CommandArgument);
            ViewState["folid"] = mm;

            //int indx = Convert.ToInt32(e.CommandArgument.ToString());
            //Int32 datakey = Int32.Parse(GridEmail.DataKeys[indx].Value.ToString());
            //ViewState["folid"] = datakey;

            DataTable dt = new DataTable();
            dt = select("SELECT * FROM MessageFolderMaster where  MessageFolderMaster.ID='" + ViewState["folid"] + "'");

            if (dt.Rows.Count > 0)
            {
                ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dt.Rows[0]["InoutcommingId"].ToString()));
                txtmessagefolder.Text = Convert.ToString(dt.Rows[0]["Foldername"]);
                btninsert.Text = "Update";
            }
        }
        if (e.CommandName == "Delete")
        {
            int m = Convert.ToInt32(e.CommandArgument);


            //int indx = Convert.ToInt32(e.CommandArgument.ToString());
            //Int32 datakey = Int32.Parse(GridEmail.DataKeys[indx].Value.ToString());

            //ViewState["folid"] = datakey.ToString();



            if (m != null)
            {

                DataTable dsa = new DataTable();
                dsa = select("Select * from MessageProcessRulesDetail where MovetoFolderID='" + m + "'");
                if (dsa.Rows.Count == 0)
                {
                    string result1 = "delete from MessageFolderMaster where ID='" + m + "'";
                    SqlCommand cmddel = new SqlCommand(result1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmddel.ExecuteNonQuery();
                    con.Close();
                    //      pnlmsg.Visible = true;

                    labelmsg.Text = "Record deleted successfully";

                    Fillgrid();
                }
                else
                {
                    //      pnlmsg.Visible = true;

                    labelmsg.Text = "This record used in child table,first delete child records";

                }
            }


        }

    }
    protected void GridEmail_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["folid"] != null)
        {
            ModalPopupExtender9.Hide();
            DataTable dsa = new DataTable();
            dsa = select("Select * from MessageProcessRulesDetail where MovetoFolderID='" + ViewState["folid"] + "'");
            if (dsa.Rows.Count == 0)
            {
                string result1 = "delete from MessageFolderMaster where ID='" + ViewState["folid"] + "'";
                SqlCommand cmddel = new SqlCommand(result1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddel.ExecuteNonQuery();
                con.Close();

                labelmsg.Text = "Record deleted successfully";
                //   pnlmsg.Visible = true;
                Fillgrid();
            }
            else
            {

                labelmsg.Text = "This record used in chield table,first deleted chield records";
                //    pnlmsg.Visible = true;
            }
        }


    }


    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender9.Show();
    }
    protected void GridEmail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (GridEmail.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridEmail.Columns[3].Visible = false;
            }
            if (GridEmail.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridEmail.Columns[4].Visible = false;
            }
        }
        else
        {



            Button1.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridEmail.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridEmail.Columns[4].Visible = true;
            }
        }
    }

    protected void btnadddd_Click(object sender, EventArgs e)
    {
        labelmsg.Text = "";
        pnladdd.Visible = true;
        btnadddd.Visible = false;
        lbllegend.Visible = true;
        lbllegend.Text = "Add New Message Folder";

    }
    protected void GridEmail_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        Fillgrid();
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
    protected void imgadddept_Click(object sender, ImageClickEventArgs e)
    {
        string te = "AddCompanyEmail.aspx";        
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgdeptrefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillbusiness();
       
    }
}
