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
public partial class ShoppingCart_Admin_EmailContentMaster : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
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
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            TextEntryDate.Text = System.DateTime.Now.ToShortDateString();
            lblCompany.Text = Session["Cname"].ToString();

            ddlWebsiteName.Enabled = true;
            ddlEmailType.Enabled = true;
            txtEmailContent.Enabled = true;
            TextEntryDate.Enabled = true;
            ImageButton1.Enabled = true;
            imgaddcatt.Visible = true;
            imgcattrefresh.Visible = true;

            if (Request.QueryString["Id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);

                SqlDataAdapter da = new SqlDataAdapter("SELECT * from EmailTypeMaster where EmailTypeId='" + id + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {


                    FillddlSitename();

                    ddlWebsiteName.SelectedIndex = ddlWebsiteName.Items.IndexOf(ddlWebsiteName.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));

                    FillddlEmailType();
                    ddlEmailType.SelectedIndex = ddlEmailType.Items.IndexOf(ddlEmailType.Items.FindByValue(dt.Rows[0]["EmailTypeId"].ToString()));



                    filterstore();
                    filterbyemailtype();

                    FillGrid();

                    addemail.Visible = true;
                    btnadd.Visible = false;
                    Label1.Visible = false;
                    contntlabel.Visible = true;
                    contntlabel.Text = "Draft Pre-Formatted Email";

                }
            }
            else if (Request.QueryString["ViewManageId"] != null)
            {

                int viewmanageid = Convert.ToInt32(Request.QueryString["ViewManageId"]);
               

                SqlDataAdapter da = new SqlDataAdapter("SELECT * from EmailContentMaster where EmailTypeId='" + viewmanageid + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    FillddlSitename();

                    ddlWebsiteName.SelectedIndex = ddlWebsiteName.Items.IndexOf(ddlWebsiteName.Items.FindByValue(dt.Rows[0]["CompanyWebsiteMasterId"].ToString()));

                    ViewState["Id"] = dt.Rows[0]["EmailContentMasterId"].ToString();

                    FillddlEmailType();

                    ddlEmailType.SelectedIndex = ddlEmailType.Items.IndexOf(ddlEmailType.Items.FindByValue(dt.Rows[0]["EmailTypeId"].ToString()));

                    txtEmailContent.Text = dt.Rows[0]["EmailContent"].ToString();

                    TextEntryDate.Text = Convert.ToDateTime(dt.Rows[0]["EntryDate"].ToString()).ToShortDateString();

                    btnadd.Visible = false;
                    addemail.Visible = true;
                    btnupdate.Visible = true;
                    contntlabel.Visible = true;
                    contntlabel.Text = "Edit Pre-Formatted Email";
                    btnsubmit.Visible = false;
                    txtEmailContent.Enabled = true;
                    TextEntryDate.Enabled = true;
                    ddlEmailType.Enabled = true;
                    ddlWebsiteName.Enabled = true;

                    filterstore();
                    filterbyemailtype();

                    FillGrid();

                }
                else
                {
                    SqlDataAdapter da123 = new SqlDataAdapter("SELECT * from EmailTypeMaster where EmailTypeId='" + viewmanageid + "'", con);
                    DataTable dt123 = new DataTable();
                    da123.Fill(dt123);
                    if (dt123.Rows.Count > 0)
                    {


                        FillddlSitename();

                        ddlWebsiteName.SelectedIndex = ddlWebsiteName.Items.IndexOf(ddlWebsiteName.Items.FindByValue(dt123.Rows[0]["Whid"].ToString()));

                        FillddlEmailType();
                        ddlEmailType.SelectedIndex = ddlEmailType.Items.IndexOf(ddlEmailType.Items.FindByValue(dt123.Rows[0]["EmailTypeId"].ToString()));



                        filterstore();
                        filterbyemailtype();

                        FillGrid();

                        addemail.Visible = true;
                        btnadd.Visible = false;
                        Label1.Visible = false;
                        contntlabel.Visible = true;
                        contntlabel.Text = "Draft Pre-Formatted Email";

                    }

                }

               
            }

            else
            {

                FillddlSitename();
                FillddlEmailType();
                filterstore();
                filterbyemailtype();

                FillGrid();
            }

        }
    }

    protected void FillddlSitename()
    {

        ddlWebsiteName.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlWebsiteName.DataSource = ds;
        ddlWebsiteName.DataTextField = "Name";
        ddlWebsiteName.DataValueField = "WareHouseId";
        ddlWebsiteName.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlWebsiteName.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }



    }

    protected void filterstore()
    {
        ddlbusinessfilter.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();

        ddlbusinessfilter.DataSource = ds;
        ddlbusinessfilter.DataTextField = "Name";
        ddlbusinessfilter.DataValueField = "WareHouseId";
        ddlbusinessfilter.DataBind();

        ddlbusinessfilter.Items.Insert(0, "All");
        ddlbusinessfilter.Items[0].Value = "0";


    }

    protected void FillddlEmailType()
    {

        ddlEmailType.Items.Clear();

        string sitestr1 = "SELECT * from EmailTypeMaster where Whid='" + ddlWebsiteName.SelectedValue + "' order by Name";

        SqlCommand cmd21 = new SqlCommand(sitestr1, con);
        SqlDataAdapter adt21 = new SqlDataAdapter(cmd21);
        DataTable dt21 = new DataTable();
        adt21.Fill(dt21);

        if (dt21.Rows.Count > 0)
        {
            ddlEmailType.DataSource = dt21;
            ddlEmailType.DataTextField = "Name";
            ddlEmailType.DataValueField = "EmailTypeId";
            ddlEmailType.DataBind();

        }


    }

    protected void filterbyemailtype()
    {
        ddshorting.Items.Clear();

        string sitestr1 = "SELECT * from EmailTypeMaster where Whid='" + ddlbusinessfilter.SelectedValue + "' order by Name";

        SqlCommand cmd21 = new SqlCommand(sitestr1, con);
        SqlDataAdapter adt21 = new SqlDataAdapter(cmd21);
        DataTable dt21 = new DataTable();
        adt21.Fill(dt21);

        if (dt21.Rows.Count > 0)
        {
            ddshorting.DataSource = dt21;
            ddshorting.DataTextField = "Name";
            ddshorting.DataValueField = "EmailTypeId";
            ddshorting.DataBind();

            ddshorting.Items.Insert(0, "All");
            ddshorting.Items[0].Value = "0";



        }
        else
        {
            ddshorting.Items.Insert(0, "All");
            ddshorting.Items[0].Value = "0";

        }

    }


    protected void FillGrid()
    {


        lblemailtypeprint.Text = ddshorting.SelectedItem.Text;
        lblBusiness.Text = ddlbusinessfilter.SelectedItem.Text;

        string strbusiness = "";
        string emailtype = "";
        string strfinal = "";

        string sitestr13 = "Select EmailContentMaster.*,Left(EmailContentMaster.EmailContent,30) as EmailContentDetail ,EmailTypeMaster.Name,WareHouseMaster.Name as Wname from EmailContentMaster inner join EmailTypeMaster on EmailTypeMaster.EmailTypeId=EmailContentMaster.EmailTypeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=EmailContentMaster.CompanyWebsiteMasterId where EmailContentMaster.compid='" + Session["Comid"].ToString() + "'";

        if (ddlbusinessfilter.SelectedIndex > 0)
        {
            strbusiness = " and EmailContentMaster.CompanyWebsiteMasterId='" + ddlbusinessfilter.SelectedValue + "'";
        }
        if (ddshorting.SelectedIndex > 0)
        {
            emailtype = " and EmailContentMaster.EmailTypeId='" + ddshorting.SelectedValue + "' ";

        }
        string sortingorder = " order by WareHouseMaster.Name,EmailTypeMaster.Name,EmailContentMaster.EmailContent ";

        strfinal = sitestr13 + strbusiness + emailtype + sortingorder;


      

        SqlCommand cmd213 = new SqlCommand(strfinal, con);
        SqlDataAdapter adt213 = new SqlDataAdapter(cmd213);
        DataTable dt213 = new DataTable();
        adt213.Fill(dt213);
        if (dt213.Rows.Count > 0)
        {
            GridView1.DataSource = dt213;


            DataView myDataView = new DataView();
            myDataView = dt213.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }


            GridView1.DataBind();

        }
        else
        {
            GridView1.DataSource = dt213;
            GridView1.DataBind();
           
        }

    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        string up = "Delete from  EmailContentMaster  where EmailContentMasterId=" + ViewState["Id"] + " ";
        SqlCommand cm = new SqlCommand(up, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cm.ExecuteNonQuery();
        con.Close();
        Label1.Text = "Record deleted successfully";
        Label1.Visible = true;
        FillGrid();

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (ddlEmailType.SelectedIndex != -1 && ddlWebsiteName.SelectedIndex != -1)
        {
           
            string sitestr13 = "Select * from EmailContentMaster where CompanyWebsiteMasterId='" + ddlWebsiteName.SelectedValue + "' and EmailTypeId='" + ddlEmailType.SelectedValue + "' ";
            SqlCommand cmd213 = new SqlCommand(sitestr13, con);
            SqlDataAdapter adt213 = new SqlDataAdapter(cmd213);
            DataTable dt213 = new DataTable();
            adt213.Fill(dt213);
            if (dt213.Rows.Count > 0)
            {
                Label1.Visible = true;
                Label1.Text = "Record already exists";
            }
            else
            {
                SqlCommand mycmd = new SqlCommand("Sp_Insert_EmailContentMaster", con);
                mycmd.CommandType = CommandType.StoredProcedure;
                mycmd.Parameters.AddWithValue("@EmailTypeId", ddlEmailType.SelectedValue);
                mycmd.Parameters.AddWithValue("@CompanyWebsiteMasterId", ddlWebsiteName.SelectedValue);
                mycmd.Parameters.AddWithValue("@EmailContent", txtEmailContent.Text);
                mycmd.Parameters.AddWithValue("@EntryDate", Convert.ToDateTime(TextEntryDate.Text));
                mycmd.Parameters.AddWithValue("@compid", Session["Comid"].ToString());


                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }
                mycmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record inserted successfully";
                txtEmailContent.Text = "";
               
                ddlWebsiteName.SelectedIndex = 0;
                addemail.Visible = false;
                btnadd.Visible = true;
                contntlabel.Visible = false;
                FillGrid();
               
            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Please select Pre-Formatted Email Name";
        }


    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        



    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGrid();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {



    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {


        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillGrid();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

      

        if (e.CommandName == "Edit")
        {
            txtEmailContent.Enabled = true;
            TextEntryDate.Enabled = true;
            ddlEmailType.Enabled = true;
            ddlWebsiteName.Enabled = true;

            ViewState["Id"] = Convert.ToInt32(e.CommandArgument);

            SqlDataAdapter da = new SqlDataAdapter("SELECT * from EmailContentMaster where EmailContentMasterId='" + ViewState["Id"] + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            

            FillddlSitename();

            ddlWebsiteName.SelectedIndex = ddlWebsiteName.Items.IndexOf(ddlWebsiteName.Items.FindByValue(dt.Rows[0]["CompanyWebsiteMasterId"].ToString()));

            FillddlEmailType();

            ddlEmailType.SelectedIndex = ddlEmailType.Items.IndexOf(ddlEmailType.Items.FindByValue(dt.Rows[0]["EmailTypeId"].ToString()));

            txtEmailContent.Text = dt.Rows[0]["EmailContent"].ToString();

            TextEntryDate.Text = Convert.ToDateTime(dt.Rows[0]["EntryDate"].ToString()).ToShortDateString();

            btnadd.Visible = false;
            addemail.Visible = true;
            btnupdate.Visible = true;
            contntlabel.Visible = true;
            contntlabel.Text = "Edit Pre-Formatted Email";
            btnsubmit.Visible = false;




        }
        if (e.CommandName == "Sort")
        {
            return;
        }
        if (e.CommandName == "Delete")
        {
           
            ViewState["Id"] = Convert.ToInt32(e.CommandArgument);
            ImageButton2_Click(sender, e);
            
        }

        if (e.CommandName == "View")
        {
           

            txtEmailContent.Enabled = true;
            TextEntryDate.Enabled = true;
            ddlEmailType.Enabled = true;
            ddlWebsiteName.Enabled = true;
            pnlprinthide.Visible = false;
            imgaddcatt.Visible = false;
            imgcattrefresh.Visible = false;
            ViewState["Id"] = Convert.ToInt32(e.CommandArgument);

            SqlDataAdapter da = new SqlDataAdapter("SELECT * from EmailContentMaster where EmailContentMasterId='" + ViewState["Id"] + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);



            FillddlSitename();

            ddlWebsiteName.SelectedIndex = ddlWebsiteName.Items.IndexOf(ddlWebsiteName.Items.FindByValue(dt.Rows[0]["CompanyWebsiteMasterId"].ToString()));

            FillddlEmailType();

            ddlEmailType.SelectedIndex = ddlEmailType.Items.IndexOf(ddlEmailType.Items.FindByValue(dt.Rows[0]["EmailTypeId"].ToString()));

            txtEmailContent.Text = dt.Rows[0]["EmailContent"].ToString();

            TextEntryDate.Text = Convert.ToDateTime(dt.Rows[0]["EntryDate"].ToString()).ToShortDateString();

            btnadd.Visible = false;
            addemail.Visible = true;
            btnupdate.Visible = false;
            contntlabel.Visible = true;
            contntlabel.Text = "View Pre-Formatted Email";
            btnsubmit.Visible = false;

            ddlWebsiteName.Enabled = false;
            ddlEmailType.Enabled = false;
            txtEmailContent.Enabled = false;
            TextEntryDate.Enabled = false;
            ImageButton1.Enabled = false;




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


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGrid();

    }
   
    protected void Buttony1_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void HiddenButton_Click(object sender, EventArgs e)
    {

    }
   

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void ddshorting_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }



    protected void Button2_Click1(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            Button7.Visible = true;
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
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["vHide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }

        }
        else
        {
            

            Button2.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
            if (ViewState["vHide"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        pnlprinthide.Visible = true;
        txtEmailContent.Enabled = true;
        TextEntryDate.Enabled = true;
        ddlEmailType.Enabled = true;
        ddlWebsiteName.Enabled = true;
        imgaddcatt.Visible = true;
        imgcattrefresh.Visible = true;

        txtEmailContent.Text = "";
        TextEntryDate.Text = "";
        ddlEmailType.SelectedIndex =-1;
        
        addemail.Visible = false;
        btnadd.Visible = true;
        Label1.Visible = false;
        contntlabel.Visible = false;
        btnupdate.Visible = false;
        btnsubmit.Visible = true;


        ddlWebsiteName.Enabled = true;
        ddlEmailType.Enabled = true;
        txtEmailContent.Enabled = true;
        TextEntryDate.Enabled = true;
        ImageButton1.Enabled = true;

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        pnlprinthide.Visible = true;
        addemail.Visible = true;
        btnadd.Visible = false;
        Label1.Visible = false;
        contntlabel.Visible = true;
        contntlabel.Text = "Draft Pre-Formatted Email";

        ddlWebsiteName.Enabled = true;
        ddlEmailType.Enabled = true;
        txtEmailContent.Enabled = true;
        TextEntryDate.Enabled = true;
        ImageButton1.Enabled = true;
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {

        if (ddlEmailType.SelectedIndex != -1 && ddlWebsiteName.SelectedIndex != -1)
        {

            string sitestr13 = "Select * from EmailContentMaster where CompanyWebsiteMasterId='" + ddlWebsiteName.SelectedValue + "' and EmailTypeId='" + ddlEmailType.SelectedValue + "' and EmailContentMasterId<>'" + ViewState["Id"] + "' ";
            SqlCommand cmd213 = new SqlCommand(sitestr13, con);
            SqlDataAdapter adt213 = new SqlDataAdapter(cmd213);
            DataTable dt213 = new DataTable();
            adt213.Fill(dt213);

            if (dt213.Rows.Count > 0)
            {
                Label1.Visible = true;
                Label1.Text = "Record already exists";
            }

            else
            {
                string up = "update EmailContentMaster set EmailTypeId='" + ddlEmailType.SelectedValue + "',CompanyWebsiteMasterId='" + ddlWebsiteName.SelectedValue + "',EmailContent='" + txtEmailContent.Text + "',EntryDate='" + TextEntryDate.Text + "' where EmailContentMasterId='" + ViewState["Id"] + "' ";
                SqlCommand cm = new SqlCommand(up, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }
                cm.ExecuteNonQuery();
                con.Close();
               
                Label1.Visible = true;
                Label1.Text = "Record updated successfully";

                addemail.Visible = false;
                btnadd.Visible = true;
                contntlabel.Visible = false;
                btnupdate.Visible = false;
                btnsubmit.Visible = true;

                TextEntryDate.Text = "";
                txtEmailContent.Text = "";
                ddlWebsiteName.SelectedIndex = 0;
                txtEmailContent.Text = "";
                GridView1.EditIndex = -1;
                FillGrid();

            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Please select email type ";
        }






        //try
        //{
        //    string sitestr13 = " SELECT    EmailContentMaster.EmailContent " +
        //         " FROM         EmailContentMaster where EmailContent='" + txtEmailContent.Text + "' and EmailTypeId='" + ddlEmailType.SelectedValue + "' and CompanyWebsiteMasterId='" + ddlWebsiteName.SelectedValue + "'  and  EmailContentMasterId<>'" + ViewState["Id"] + "' ";


        //    SqlCommand cmd213 = new SqlCommand(sitestr13, con);
        //    SqlDataAdapter adt213 = new SqlDataAdapter(cmd213);
        //    DataTable dt213 = new DataTable();
        //    adt213.Fill(dt213);
        //    if (dt213.Rows.Count > 0)
        //    {
        //        Label1.Visible = true;
        //        Label1.Text = "Record already exist";
        //    }
        //    else
        //    {
        //        string up = "update EmailContentMaster set EmailContent='" + txtEmailContent.Text + "', EmailTypeId='" + ddlEmailType.SelectedValue + "', " +
        //            " CompanyWebsiteMasterId='" + ddlWebsiteName.SelectedValue + "',EntryDate='" + TextEntryDate.Text + "'      where EmailContentMasterId='" + ViewState["Id"] + "' ";
        //        SqlCommand cm = new SqlCommand(up, con);
        //        if (con.State.ToString() != "Open")
        //        {
        //            con.Open();

        //        }
        //        cm.ExecuteNonQuery();
        //        con.Close();
        //        Label1.Text = "Record updated successfully";
        //        Label1.Visible = true;
        //        addemail.Visible = false;
        //        btnadd.Visible = true;
        //        contntlabel.Visible = false;
        //        btnupdate.Visible = false;
        //        btnsubmit.Visible = true;
        //        TextEntryDate.Text = "";
        //        ddlEmailType.SelectedIndex = 0;
        //        ddlWebsiteName.SelectedIndex = 0;
        //        txtEmailContent.Text = "";

        //    }
        //}
        //catch (Exception ter)
        //{
        //    Label1.Text = "Error :" + ter.Message;
        //    Label1.Visible = true;

        //}
        //GridView1.EditIndex = -1;
        //FillGrid();


    }
    protected void ddlbusinessfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterbyemailtype();
        FillGrid();
    }

    protected void ddlWebsiteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillddlEmailType();
    }
    protected void imgaddcatt_Click(object sender, ImageClickEventArgs e)
    {
        string te = "EmailTypeMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgcattrefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillddlEmailType();
    }
}
