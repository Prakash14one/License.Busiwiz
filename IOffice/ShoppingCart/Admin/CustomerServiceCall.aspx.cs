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


public partial class ShoppingCart_Admin_CustomerSericeCall : System.Web.UI.Page
{
    SqlConnection conn;
    //= new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ToString());

    string s;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
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
        if (!IsPostBack)
        {
            txtFromDate.Text = System.DateTime.Now.Month.ToString() + "/01/" + System.DateTime.Now.Year.ToString();
            txtTodate.Text = System.DateTime.Now.ToShortDateString();
            fillstore();
            fillupdatestore();
            FillStatus();
            FillMainStatus();
            fillupdateproblemtype();
            ddlProbType.DataSource = (DataSet)fillddl();
            ddlProbType.DataTextField = "ProblemName";
            ddlProbType.DataValueField = "ProblemTypeId";
            ddlProbType.DataBind();

            fillproblemtype();
            selectservicecall();

            Panel2123.Visible = false;

            lblCompany.Text = Session["Cname"].ToString();

        }

    }

    public void selectservicecall()
    {
        lblBusiness.Text = ddlWarehouse.SelectedItem.Text;
        lblfromdateprint.Text = txtFromDate.Text;
        lbltodateprint.Text = txtTodate.Text;
        lblstatusprint.Text = ddlMainStatus.SelectedItem.Text;
        lblnamephoneprint.Text = txtname.Text;
        lblproblemtypeprint.Text = ddlseacrchbyproblemtype.SelectedItem.Text;

        string attch = "";
        if (ddlWarehouse.SelectedIndex > 0)
        {
            attch = " Party_master.id='" + Session["comid"] + "' and Party_master.Whid='" + ddlWarehouse.SelectedValue + "' ";
        }
        else
        {
            attch = " Party_master.id='" + Session["comid"] + "'  ";
        }
        string probtype = "";
        if (ddlseacrchbyproblemtype.SelectedIndex > 0)
        {
            probtype = "and CustomerServiceCallMaster.ProblemTypeId='" + ddlseacrchbyproblemtype.SelectedValue + "'";
        }

        string str2 = "";

        if (ddlMainStatus.SelectedIndex > 0 && txtname.Text != "")
        {
            s = " WareHouseMaster.Name as Wname,  ServiceStatusMaster.StatusName AS Servicestatus, CustomerServiceCallMaster.CustomerServiceCallMasterId,  " +
                         " CustomerServiceCallMaster.Entrydate, CustomerServiceCallMaster.ProblemTitle, CustomerServiceCallMaster.ProblemDescription,  " +
                         " CustomerServiceCallMaster.ProblemType, CustomerServiceCallMaster.CustomerId, CustomerServiceCallMaster.ServiceStatusId,  " +
                         " Party_master.Compname, Party_master.Contactperson, Party_master.Contactperson as comp,ProblemTypeMaster.ProblemName " +
                       " FROM         CustomerServiceCallMaster INNER JOIN " +
                        " ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId INNER JOIN " +
                        " User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID INNER JOIN " +
                        " Party_master ON User_master.PartyID = Party_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join ProblemTypeMaster on ProblemTypeMaster.ProblemTypeId=CustomerServiceCallMaster.ProblemTypeId " +
                       " WHERE    (ServiceStatusMaster.StatusId = '" + ddlMainStatus.SelectedValue + "') and " + attch + " and (CustomerServiceCallMaster.Entrydate between '" + Convert.ToDateTime(txtFromDate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') " +
                           " " + probtype + "  and Party_master.Contactperson like'" + txtname.Text + "%' or Party_master.Phoneno ='" + txtname.Text + "' or Party_master.Email ='" + txtname.Text + "'";

            str2 = " select count(CustomerServiceCallMaster.CustomerServiceCallMasterId) as ci " +
                     " FROM         CustomerServiceCallMaster INNER JOIN " +
                        " ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId INNER JOIN " +
                        " User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID INNER JOIN " +
                        " Party_master ON User_master.PartyID = Party_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join ProblemTypeMaster on ProblemTypeMaster.ProblemTypeId=CustomerServiceCallMaster.ProblemTypeId " +
                       " WHERE    (ServiceStatusMaster.StatusId = '" + ddlMainStatus.SelectedValue + "') and " + attch + " and (CustomerServiceCallMaster.Entrydate between '" + Convert.ToDateTime(txtFromDate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') " +
                           " " + probtype + "  and Party_master.Contactperson like'" + txtname.Text + "%' or Party_master.Phoneno ='" + txtname.Text + "' or Party_master.Email ='" + txtname.Text + "'";

        }
        else if (ddlMainStatus.SelectedIndex > 0)
        {
            s = " WareHouseMaster.Name as Wname,  ServiceStatusMaster.StatusName AS Servicestatus, CustomerServiceCallMaster.CustomerServiceCallMasterId,  " +
                         " CustomerServiceCallMaster.Entrydate, CustomerServiceCallMaster.ProblemTitle, CustomerServiceCallMaster.ProblemDescription,  " +
                         " CustomerServiceCallMaster.ProblemType, CustomerServiceCallMaster.CustomerId, CustomerServiceCallMaster.ServiceStatusId,  " +
                         " Party_master.Compname, Party_master.Contactperson, Party_master.Contactperson as comp,ProblemTypeMaster.ProblemName " +
                       " FROM         CustomerServiceCallMaster INNER JOIN " +
                        " ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId INNER JOIN " +
                        " User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID INNER JOIN " +
                        " Party_master ON User_master.PartyID = Party_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join ProblemTypeMaster on ProblemTypeMaster.ProblemTypeId=CustomerServiceCallMaster.ProblemTypeId" +
                       " WHERE      (ServiceStatusMaster.StatusId = '" + ddlMainStatus.SelectedValue + "') and " + attch + " and (CustomerServiceCallMaster.Entrydate between '" + Convert.ToDateTime(txtFromDate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') " +
                           " " + probtype + "";

            str2 = " select count(CustomerServiceCallMaster.CustomerServiceCallMasterId) as ci " +
                     " FROM         CustomerServiceCallMaster INNER JOIN " +
                        " ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId INNER JOIN " +
                        " User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID INNER JOIN " +
                        " Party_master ON User_master.PartyID = Party_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join ProblemTypeMaster on ProblemTypeMaster.ProblemTypeId=CustomerServiceCallMaster.ProblemTypeId" +
                       " WHERE      (ServiceStatusMaster.StatusId = '" + ddlMainStatus.SelectedValue + "') and " + attch + " and (CustomerServiceCallMaster.Entrydate between '" + Convert.ToDateTime(txtFromDate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') " +
                           " " + probtype + "";

        }
        else if (txtname.Text != "")
        {
            s = " WareHouseMaster.Name as Wname, ServiceStatusMaster.StatusName AS Servicestatus, CustomerServiceCallMaster.CustomerServiceCallMasterId,  " +
                         " CustomerServiceCallMaster.Entrydate, CustomerServiceCallMaster.ProblemTitle, CustomerServiceCallMaster.ProblemDescription,  " +
                         " CustomerServiceCallMaster.ProblemType, CustomerServiceCallMaster.CustomerId, CustomerServiceCallMaster.ServiceStatusId,  " +
                         " Party_master.Compname, Party_master.Contactperson, Party_master.Contactperson as comp,ProblemTypeMaster.ProblemName " +
                       " FROM         CustomerServiceCallMaster INNER JOIN " +
                        " ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId INNER JOIN " +
                        " User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID INNER JOIN " +
                        " Party_master ON User_master.PartyID = Party_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join ProblemTypeMaster on ProblemTypeMaster.ProblemTypeId=CustomerServiceCallMaster.ProblemTypeId" +
                       " WHERE  (CustomerServiceCallMaster.Entrydate between '" + Convert.ToDateTime(txtFromDate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') " + probtype + " " +
                           " and " + attch + " and Party_master.Contactperson like'" + txtname.Text + "%' or Party_master.Phoneno ='" + txtname.Text + "' or Party_master.Email ='" + txtname.Text + "' ";

            str2 = " select count(CustomerServiceCallMaster.CustomerServiceCallMasterId) as ci " +
                     " FROM         CustomerServiceCallMaster INNER JOIN " +
                        " ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId INNER JOIN " +
                        " User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID INNER JOIN " +
                        " Party_master ON User_master.PartyID = Party_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join ProblemTypeMaster on ProblemTypeMaster.ProblemTypeId=CustomerServiceCallMaster.ProblemTypeId" +
                       " WHERE  (CustomerServiceCallMaster.Entrydate between '" + Convert.ToDateTime(txtFromDate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') " + probtype + " " +
                           " and " + attch + " and Party_master.Contactperson like'" + txtname.Text + "%' or Party_master.Phoneno ='" + txtname.Text + "' or Party_master.Email ='" + txtname.Text + "'";
        }
        else
        {
            s = " WareHouseMaster.Name as Wname, ServiceStatusMaster.StatusName AS Servicestatus, CustomerServiceCallMaster.CustomerServiceCallMasterId,  " +
                      " CustomerServiceCallMaster.Entrydate, CustomerServiceCallMaster.ProblemTitle, CustomerServiceCallMaster.ProblemDescription,  " +
                      " CustomerServiceCallMaster.ProblemType, CustomerServiceCallMaster.CustomerId, CustomerServiceCallMaster.ServiceStatusId,  " +
                      " Party_master.Compname, Party_master.Contactperson, Party_master.Contactperson as comp,ProblemTypeMaster.ProblemName " +
                    " FROM         CustomerServiceCallMaster INNER JOIN " +
                     " ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId INNER JOIN " +
                     " User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID INNER JOIN " +
                     " Party_master ON User_master.PartyID = Party_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join ProblemTypeMaster on ProblemTypeMaster.ProblemTypeId=CustomerServiceCallMaster.ProblemTypeId" +
                    " WHERE     (CustomerServiceCallMaster.Entrydate between '" + Convert.ToDateTime(txtFromDate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') " + probtype + " " +
                        "  and " + attch + " ";

            str2 = " select count(CustomerServiceCallMaster.CustomerServiceCallMasterId) as ci " +
                     " FROM         CustomerServiceCallMaster INNER JOIN " +
                     " ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId INNER JOIN " +
                     " User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID INNER JOIN " +
                     " Party_master ON User_master.PartyID = Party_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join ProblemTypeMaster on ProblemTypeMaster.ProblemTypeId=CustomerServiceCallMaster.ProblemTypeId" +
                    " WHERE     (CustomerServiceCallMaster.Entrydate between '" + Convert.ToDateTime(txtFromDate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') " + probtype + " " +
                        "  and " + attch + " ";
        }


        GridServiceCall.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " CustomerServiceCallMaster.CustomerServiceCallMasterId ,WareHouseMaster.Name,CustomerServiceCallMaster.Entrydate,Party_master.Contactperson,CustomerServiceCallMaster.ProblemType,CustomerServiceCallMaster.ProblemTitle,ServiceStatusMaster.StatusName";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt1 = GetDataPage(GridServiceCall.PageIndex, GridServiceCall.PageSize, sortExpression, s);

            GridServiceCall.DataSource = dt1;
            GridServiceCall.DataBind();

            Session["data"] = dt1;
        }
        else
        {

            GridServiceCall.DataSource = null;
            GridServiceCall.DataBind();
        }        
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
        SqlCommand cmd = new SqlCommand(qu, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void GridServiceCall_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "view")
        {
            GridServiceCall.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            selectDetail(Convert.ToInt32(GridServiceCall.SelectedIndex));

            Panel2123.Visible = true;
            pnlmastershow123.Visible = true;
            pnlmaster1.Visible = false;
            lblmessage.Text = "";


        }
        else if (e.CommandName == "viewprofile")
        {

            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "Servicecalllog.aspx?id=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }

        if (e.CommandName == "Delete")
        {
            int m = Convert.ToInt32(e.CommandArgument);

            SqlCommand cmm = new SqlCommand("Delete from CustomerServiceCallMaster where CustomerServiceCallMasterId=" + m, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmm.ExecuteNonQuery();
            conn.Close();
            lblmessage.Text = "Record deleted successfully";
            selectservicecall();
        }
    }
    protected void GridServiceCall_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        selectservicecall();

    }



    public void FillStatus()
    {
        string str = "SELECT     StatusId, StatusName " +
                    " FROM         ServiceStatusMaster";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlStatus.DataSource = dt;
        ddlStatus.DataTextField = "StatusName";
        ddlStatus.DataValueField = "StatusId";
        ddlStatus.DataBind();

    }
    public void FillMainStatus()
    {
        string str = "SELECT     StatusId, StatusName " +
                    " FROM         ServiceStatusMaster";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlMainStatus.DataSource = dt;
        ddlMainStatus.DataTextField = "StatusName";
        ddlMainStatus.DataValueField = "StatusId";
        ddlMainStatus.DataBind();
        ddlMainStatus.Items.Insert(0, "All");

    }
    public DataSet fillddl()
    {
        //SqlCommand cmd = new SqlCommand("Sp_Select_Problemtype", conn);
        //cmd.CommandType = CommandType.StoredProcedure;
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        //return ds;
        SqlCommand cmd = new SqlCommand("Sp_Select_Problemtype", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@compid", Session["comid"]);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    public void customerfill()
    {
        SqlCommand cmd = new SqlCommand("SELECT UserID, PartytTypeMaster.PartType+':'+Party_master.Contactperson AS Uname FROM dbo.User_master inner join Party_master ON User_master.PartyID = Party_master.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where Party_master.id='" + Session["comid"] + "' and Party_master.Whid='" + ddlupdatebusiness.SelectedValue + "' and PartytTypeMaster.PartType In ('Customer') order by User_master.Name,User_master.Phoneno ", conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlUserMaster.DataSource = ds;
        ddlUserMaster.DataTextField = "Uname";
        ddlUserMaster.DataValueField = "UserID";
        ddlUserMaster.DataBind();

    }

    public void selectDetail(int id)
    {
        string s = " SELECT WareHouseMaster.WareHouseId as Wid,ServiceStatusMaster.StatusName AS Servicestatus, CustomerServiceCallMaster.CustomerServiceCallMasterId, CustomerServiceCallMaster.Entrydate,  " +
                     " CustomerServiceCallMaster.ProblemTitle,User_master.UserID ,CustomerServiceCallMaster.ProblemDescription,   " +
                     " CustomerServiceCallMaster.CustomerId,CustomerServiceCallMaster.ServiceNotes, CustomerServiceCallMaster.ServiceStatusId, Party_master.Contactperson, Party_master.Contactperson,  " +
                     " ProblemTypeMaster.ProblemName, ProblemTypeMaster.ProblemTypeId " +
                       " FROM   CustomerServiceCallMaster INNER JOIN " +
                     " ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId INNER JOIN " +
                     " User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID INNER JOIN " +
                     " Party_master ON User_master.PartyID = Party_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid INNER JOIN " +
                     " ProblemTypeMaster ON CustomerServiceCallMaster.ProblemTypeId = ProblemTypeMaster.ProblemTypeId " +
                    " WHERE     (CustomerServiceCallMaster.CustomerServiceCallMasterId = '" + id + "')" +
                       "  ORDER BY CustomerServiceCallMaster.Entrydate DESC";

        SqlCommand cmd = new SqlCommand(s, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        lblId.Text = id.ToString();
        fillupdatestore();
        ddlupdatebusiness.SelectedIndex = ddlupdatebusiness.Items.IndexOf(ddlupdatebusiness.Items.FindByValue(dt.Rows[0]["Wid"].ToString()));

        ddlStatus.SelectedValue = dt.Rows[0]["ServiceStatusId"].ToString();
        fillupdateproblemtype();
        ddlProbType.SelectedIndex = ddlProbType.Items.IndexOf(ddlProbType.Items.FindByValue(dt.Rows[0]["ProblemTypeId"].ToString()));

        //ddlProbType.SelectedValue = dt.Rows[0]["ProblemTypeId"].ToString();
        customerfill();
        ddlUserMaster.SelectedIndex = ddlUserMaster.Items.IndexOf(ddlUserMaster.Items.FindByValue(dt.Rows[0]["UserID"].ToString()));

        txtdate1.Text = Convert.ToDateTime(dt.Rows[0]["Entrydate"].ToString()).ToShortDateString();

        txtdescription.Text = dt.Rows[0]["ProblemDescription"].ToString();

        txtprobtitle.Text = dt.Rows[0]["ProblemTitle"].ToString();

        txtServiceNotes.Text = dt.Rows[0]["ServiceNotes"].ToString();

    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {

        lblmessage.Text = "";
        Panel2123.Visible = false;
        pnlmastershow123.Visible = false;
        pnlmaster1.Visible = true;


        selectservicecall();
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        string str = "UPDATE    CustomerServiceCallMaster " +
                    " SET    Entrydate='" + txtdate1.Text + "', ProblemTitle='" + txtprobtitle.Text + "', CustomerId='" + ddlUserMaster.SelectedValue + "',ProblemTypeId ='" + ddlProbType.SelectedValue + "',ProblemDescription='" + txtdescription.Text + "' ,ServiceStatusId ='" + ddlStatus.SelectedValue + "',CustomerServiceCallMaster.ServiceNotes='" + txtServiceNotes.Text + "' " +
        " where  (CustomerServiceCallMaster.CustomerServiceCallMasterId = '" + Convert.ToInt32(lblId.Text) + "') ";

        SqlCommand cmd = new SqlCommand(str, conn);
        if (conn.State.ToString() != "Open")
        {
            conn.Open();
        }
        cmd.ExecuteNonQuery();
        conn.Close();

        lblmessage.Text = "Record updated successfully";
        Panel2123.Visible = false;
        pnlmastershow123.Visible = false;
        pnlmaster1.Visible = true;
        selectservicecall();

    }
    protected void GridServiceCall_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridServiceCall.PageIndex = e.NewPageIndex;
        selectservicecall();

    }
    protected void GridServiceCall_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortexpression"] = e.SortExpression;
        if (ViewState["sortdirection"] == null)
        {
            ViewState["sortdirection"] = "asc";
        }
        else
        {
            if (ViewState["sortdirection"].ToString() == "asc")
            {
                ViewState["sortdirection"] = "desc";
            }
            else
            {
                ViewState["sortdirection"] = "asc";
            }
        }
        DataTable dtable = new DataTable();
        dtable = (DataTable)Session["data"];
        DataView dview = dtable.DefaultView;

        if (ViewState["sortexpression"] != null)
        {
            dview.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
        }
        GridServiceCall.DataSource = dview;
        GridServiceCall.DataBind();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;

            GridServiceCall.AllowPaging = false;
            GridServiceCall.PageSize = 1000;
            selectservicecall();

            if (GridServiceCall.Columns[7].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridServiceCall.Columns[7].Visible = false;
            }
            if (GridServiceCall.Columns[8].Visible == true)
            {
                ViewState["viewHide"] = "tt";
                GridServiceCall.Columns[8].Visible = false;
            }
            if (GridServiceCall.Columns[9].Visible == true)
            {
                ViewState["viewHideprofile"] = "tt";
                GridServiceCall.Columns[9].Visible = false;
            }

        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(250);

            GridServiceCall.AllowPaging = true;
            GridServiceCall.PageSize = 10;
            selectservicecall();

            Button1.Text = "Printable Version";
            Button7.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GridServiceCall.Columns[7].Visible = true;
            }
            if (ViewState["viewHide"] != null)
            {
                GridServiceCall.Columns[8].Visible = true;
            }
            if (ViewState["viewHideprofile"] != null)
            {
                GridServiceCall.Columns[9].Visible = true;
            }

        }
    }
    protected void fillstore()
    {
        ddlWarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlWarehouse.DataSource = ds;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "WareHouseId";
        ddlWarehouse.DataBind();
        ddlWarehouse.Items.Insert(0, "All");
        ddlWarehouse.Items[0].Value = "0";

    }
    protected void fillproblemtype()
    {
        ddlseacrchbyproblemtype.DataSource = (DataSet)fillddltype();
        ddlseacrchbyproblemtype.DataTextField = "ProblemName";
        ddlseacrchbyproblemtype.DataValueField = "ProblemTypeId";
        ddlseacrchbyproblemtype.DataBind();
        ddlseacrchbyproblemtype.Items.Insert(0, "All");
        ddlseacrchbyproblemtype.Items[0].Value = "0";
    }
    public DataSet fillddltype()
    {
        //SqlCommand cmd = new SqlCommand("Sp_Select_Problemtype", conn);
        //cmd.CommandType = CommandType.StoredProcedure;
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        //return ds;

        SqlCommand cmd = new SqlCommand("Sp_Select_Problemtype", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@compid", Session["comid"]);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    protected void fillupdatestore()
    {
        ddlupdatebusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlupdatebusiness.DataSource = ds;
        ddlupdatebusiness.DataTextField = "Name";
        ddlupdatebusiness.DataValueField = "WareHouseId";
        ddlupdatebusiness.DataBind();


    }
    protected void fillupdateproblemtype()
    {
        ddlProbType.DataSource = (DataSet)fillddltype();
        ddlProbType.DataTextField = "ProblemName";
        ddlProbType.DataValueField = "ProblemTypeId";
        ddlProbType.DataBind();

    }

    protected void GridServiceCall_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string te = "CustomerNewServiceCall.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
}
