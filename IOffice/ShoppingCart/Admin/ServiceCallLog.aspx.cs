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

public partial class ShoppingCart_Admin_Default3 : System.Web.UI.Page
{
    SqlConnection con;
    string strconn;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        strconn = pgcon.dynconn.ConnectionString;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";

            if (Request.QueryString["id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);

                SqlDataAdapter da1 = new SqlDataAdapter("select User_Master.UserID, CustomerServiceCallMaster.CustomerServiceCallMasterId,CustomerServiceCallMaster.ServiceStatusId,Party_master.PartyID,CustomerServiceCallMaster.Entrydate,cast(CustomerServiceCallMaster.CustomerServiceCallMasterId as nvarchar)+' : '+User_Master.Name as Pname,party_master.Whid from CustomerServiceCallMaster inner join user_master on CustomerServiceCallMaster.[CustomerId]=user_master.[UserID] inner join party_master on [User_master].[PartyID]=party_master.[PartyID] where CustomerServiceCallMaster.CustomerServiceCallMasterId='" + Request.QueryString["id"] + "' ", con);
                DataTable dt = new DataTable();
                da1.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                   
                    fillstore();
                    ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
                    fillfilterparty();
                    ddlpartynamefilter.SelectedIndex = ddlpartynamefilter.Items.IndexOf(ddlpartynamefilter.Items.FindByValue(dt.Rows[0]["UserID"].ToString()));

                    txtFromDate.Text = Convert.ToDateTime(dt.Rows[0]["Entrydate"].ToString()).ToShortDateString();
                    txtTodate.Text = Convert.ToDateTime(dt.Rows[0]["Entrydate"].ToString()).ToShortDateString();

                    FillMainStatus();
                  
                    ddlMainStatus.SelectedIndex = ddlMainStatus.Items.IndexOf(ddlMainStatus.Items.FindByValue(dt.Rows[0]["ServiceStatusId"].ToString()));

                    fillparty();
                    ddlpartytype.SelectedIndex = ddlpartytype.Items.IndexOf(ddlpartytype.Items.FindByValue(dt.Rows[0]["CustomerServiceCallMasterId"].ToString()));
                    ddlpartytype_SelectedIndexChanged(sender, e);
                    fillgrid();
                    fillfilterstore();
                    filluser();
                }



            }
            else
            {
                txtFromDate.Text = System.DateTime.Now.ToShortDateString();
                txtTodate.Text = System.DateTime.Now.ToShortDateString();

                fillstore();
                fillfilterparty();
                FillMainStatus();
                fillparty();
               
                ddlpartytype_SelectedIndexChanged(sender, e);
                fillgrid();
                fillfilterstore();
                filluser();
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



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

    }
    protected void fillfilterparty()
    {
        SqlDataAdapter da1 = new SqlDataAdapter("SELECT UserID,Party_master.PartyID, PartytTypeMaster.PartType+':'+ Party_master.Compname +':'+ Party_master.Contactperson AS Uname FROM dbo.User_master inner join Party_master ON User_master.PartyID = Party_master.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where Party_master.id='" + Session["comid"] + "' and Party_master.Whid='" + ddlWarehouse.SelectedValue + "' and PartytTypeMaster.PartType In ('Customer') order by User_master.Name,User_master.Phoneno", con);
       
            DataTable dt = new DataTable();
        da1.Fill(dt);

        ddlpartynamefilter.DataSource = dt;
        ddlpartynamefilter.DataTextField = "Uname";
        ddlpartynamefilter.DataValueField = "UserID";
        ddlpartynamefilter.DataBind();
    }

    protected void fillparty()
    {
        SqlDataAdapter da1 = new SqlDataAdapter("select CustomerServiceCallMaster.CustomerServiceCallMasterId,cast(CustomerServiceCallMaster.CustomerServiceCallMasterId as nvarchar)+' : '+Left(CustomerServiceCallMaster.ProblemTitle,30) as Pname from CustomerServiceCallMaster inner join user_master on CustomerServiceCallMaster.[CustomerId]=user_master.[UserID] inner join party_master on [User_master].[PartyID]=party_master.[PartyID] where party_master.Whid='" + ddlWarehouse.SelectedValue + "' and CustomerServiceCallMaster.ServiceStatusId='" + ddlMainStatus.SelectedValue + "' and CustomerServiceCallMaster.Entrydate between '" + txtFromDate.Text + "' and '" + txtTodate.Text + "' and user_master.[UserID]='" + ddlpartynamefilter.SelectedValue + "' ", con);
        DataTable dt = new DataTable();
        da1.Fill(dt);

        ddlpartytype.DataSource = dt;
        ddlpartytype.DataTextField = "Pname";
        ddlpartytype.DataValueField = "CustomerServiceCallMasterId";
        ddlpartytype.DataBind();
    }
    protected void ddlpartytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpartytype.SelectedIndex > -1)
        {

            SqlCommand cmd = new SqlCommand("select CustomerServiceCallMaster.CustomerServiceCallMasterId,Party_master.Contactperson as Name ,CustomerServiceCallMaster.[Entrydate],CustomerServiceCallMaster.[ProblemTitle],CustomerServiceCallMaster.[ProblemDescription],case when (CustomerServiceCallMaster.ServiceStatusId = 1) then 'In Progress' else 'Complete' end as status from CustomerServiceCallMaster inner join user_master on CustomerServiceCallMaster.[CustomerId]=user_master.[UserID] inner join party_master on [User_master].[PartyID]=party_master.[PartyID] where CustomerServiceCallMaster.CustomerServiceCallMasterId='" + ddlpartytype.SelectedValue + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                Panel1.Visible = true;

                lblID.Text = dt1.Rows[0]["CustomerServiceCallMasterId"].ToString();
                lblDate.Text = Convert.ToDateTime(dt1.Rows[0]["Entrydate"].ToString()).ToShortDateString();
                lblpname.Text = dt1.Rows[0]["Name"].ToString();
                lblprobtitle.Text = dt1.Rows[0]["ProblemTitle"].ToString();
                lblprobdesc.Text = dt1.Rows[0]["ProblemDescription"].ToString();
                lblstatus.Text = dt1.Rows[0]["status"].ToString();
            }
            else
            {
                Panel1.Visible = false;
            }
        }
        else
        {
            Panel1.Visible = false;

        }
        fillgrid();
    }
    public void FillMainStatus()
    {
        string str = "SELECT     StatusId, StatusName " +
                    " FROM         ServiceStatusMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlMainStatus.DataSource = dt;
        ddlMainStatus.DataTextField = "StatusName";
        ddlMainStatus.DataValueField = "StatusId";
        ddlMainStatus.DataBind();


    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilterparty();
        fillparty();
        ddlpartytype_SelectedIndexChanged(sender, e);
        
    }
    protected void ddlMainStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillparty();
        ddlpartytype_SelectedIndexChanged(sender, e);
       
    }
    protected void fillgrid()
    {
        lblCompany.Text = Session["Cname"].ToString();

        lblBusiness.Text = ddlWarehouse.SelectedItem.Text;

        Label20.Text = ddlMainStatus.SelectedItem.Text;

        lblprintfromdate.Text = txtFromDate.Text;
        lblprinttodate.Text = txtTodate.Text;
        if (ddlpartytype.SelectedIndex > -1)
        {
            lblprblemtitalidprint.Text = ddlpartytype.SelectedItem.Text;
        }

        SqlCommand cmd = new SqlCommand("select ServiceCallDetailTbl.*,EmployeeMaster.EmployeeName as Uname from ServiceCallDetailTbl inner join CustomerServiceCallMaster on CustomerServiceCallMaster.CustomerServiceCallMasterId=ServiceCallDetailTbl.ServiceCallId inner join User_master on User_master.UserID=ServiceCallDetailTbl.AssignedUserId inner join Party_master on Party_master.PartyID=User_master.PartyID inner join EmployeeMaster on EmployeeMaster.PartyID=Party_master.PartyID where CustomerServiceCallMaster.CustomerServiceCallMasterId='" + ddlpartytype.SelectedValue + "' order by ServiceCallDetailTbl.ServiceCallId,ServiceCallDetailTbl.ServiceProvidedDate,EmployeeMaster.EmployeeName,ServiceCallDetailTbl.ServiceDoneNote  ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            //GridView1.DataSource = dt1;

           
            //GridView1.DataBind();

            GridView1.DataSource = dt1;


            DataView myDataView = new DataView();
            myDataView = dt1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }
    protected void filluser()
    {

        SqlCommand cmd = new SqlCommand("SELECT UserID, EmployeeMaster.EmployeeName as Uname FROM dbo.User_master inner join Party_master ON User_master.PartyID = Party_master.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId inner join EmployeeMaster on EmployeeMaster.PartyID=Party_master.PartyID where EmployeeMaster.Whid='" + ddlfilterwarehouse.SelectedValue + "' and Party_master.id='" + Session["comid"] + "' and Party_master.Whid='" + ddlfilterwarehouse.SelectedValue + "' order by EmployeeMaster.EmployeeName ", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlUserMaster.DataSource = ds;
        ddlUserMaster.DataTextField = "Uname";
        ddlUserMaster.DataValueField = "UserID";
        ddlUserMaster.DataBind();



    }
    protected void fillfilterstore()
    {
        ddlfilterwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();

        ddlfilterwarehouse.DataSource = ds;
        ddlfilterwarehouse.DataTextField = "Name";
        ddlfilterwarehouse.DataValueField = "WareHouseId";
        ddlfilterwarehouse.DataBind();



       

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            Panel2.Visible = false;
            Panel3.Visible = true;
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(GridView1.SelectedIndex);
            ViewState["Id"] = Convert.ToInt32(GridView1.SelectedIndex);

            string s = " select ServiceCallDetailTbl.*,Party_master.Whid,CustomerServiceCallMaster.ServiceStatusId from ServiceCallDetailTbl inner join CustomerServiceCallMaster on CustomerServiceCallMaster.CustomerServiceCallMasterId=ServiceCallDetailTbl.ServiceCallId inner join User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID inner join Party_master ON User_master.PartyID = Party_master.PartyID where ServiceCallDetailTbl.Id='" + id + "' ";
            SqlCommand cmd = new SqlCommand(s, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ViewState["customercallid"] = dt.Rows[0]["ServiceCallId"].ToString();


            fillfilterstore();
            ddlfilterwarehouse.SelectedIndex = ddlfilterwarehouse.Items.IndexOf(ddlfilterwarehouse.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));

            filluser();
            ddlUserMaster.SelectedIndex = ddlUserMaster.Items.IndexOf(ddlUserMaster.Items.FindByValue(dt.Rows[0]["AssignedUserId"].ToString()));



            txtservicedate.Text = Convert.ToDateTime(dt.Rows[0]["ServiceProvidedDate"].ToString()).ToShortDateString();

            TextBox1.Text = dt.Rows[0]["ServiceDoneNote"].ToString();

            if (dt.Rows[0]["ServiceStatusId"].ToString() == "2")
            {
                CheckBox1.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
            }

        }
        if (e.CommandName == "Delete")
        {
            int m = Convert.ToInt32(e.CommandArgument);

            SqlCommand cmm = new SqlCommand("Delete from ServiceCallDetailTbl where Id=" + m, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmm.ExecuteNonQuery();
            con.Close();
            lblmsg.Text = "Record deleted successfully";
            fillgrid();
        }

    }
    protected void ddlfilterwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        filluser();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = " Update ServiceCallDetailTbl set AssignedUserId='" + ddlUserMaster.SelectedValue + "',ServiceProvidedDate='" + txtservicedate.Text + "',ServiceDoneNote='" + TextBox1.Text + "' where Id='" + ViewState["Id"] + "' ";

        SqlCommand cm = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cm.ExecuteNonQuery();
        con.Close();

        if (CheckBox1.Checked == true)
        {

            string strupdate = " Update CustomerServiceCallMaster set ServiceStatusId='" + 2 + "' where CustomerServiceCallMasterId='" + ViewState["customercallid"] + "' ";

            SqlCommand cmdupdate = new SqlCommand(strupdate, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdupdate.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            string strupdate = " Update CustomerServiceCallMaster set ServiceStatusId='" + 1 + "' where CustomerServiceCallMasterId='" + ViewState["customercallid"] + "' ";

            SqlCommand cmdupdate = new SqlCommand(strupdate, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdupdate.ExecuteNonQuery();
            con.Close();
        }

        Panel2.Visible = true;
        Panel3.Visible = false;
        fillgrid();

        lblmsg.Text = "Record updated successfully";

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        Panel3.Visible = false;
    }
    protected void fillstoreprofile()
    {
        ddlWarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlWarehouse.DataSource = ds;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "WareHouseId";
        ddlWarehouse.DataBind();
    }
    public void FillMainStatusProfile()
    {
        string str = "SELECT     StatusId, StatusName " +
                    " FROM         ServiceStatusMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlMainStatus.DataSource = dt;
        ddlMainStatus.DataTextField = "StatusName";
        ddlMainStatus.DataValueField = "StatusId";
        ddlMainStatus.DataBind();


    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        string te = "AddNewServiceCallNote.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
   


    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
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

        }
        else
        {

            pnlgrid.ScrollBars = ScrollBars.Both;
            //pnlgrid.Height = new Unit(250);

            Button4.Text = "Printable Version";
            Button7.Visible = false;
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
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        fillparty();
        ddlpartytype_SelectedIndexChanged(sender, e);
    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        fillparty();
        ddlpartytype_SelectedIndexChanged(sender, e);
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

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
    protected void ddlpartynamefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillparty();

        ddlpartytype_SelectedIndexChanged(sender, e);
    }
}
