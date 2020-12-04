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

public partial class Add_User_Role_Management : System.Web.UI.Page
{
    //PageMgmt obj = new PageMgmt();
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

        compid = Session["comid"].ToString();
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        lblmsg.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            lblmsg.Text = "";

            fillstore();
            filluser();
            fillRole();
            filterbystore();
            filterbyusertype();
            filterbyrolename();
            filterbyusername();

            fillgrid();
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

    protected void btSubmit_Click(object sender, EventArgs e)
    {


        string str = " Select * from User_Role where User_id='" + dpdUserName.SelectedValue + "' and Role_id='" + dpdRoleName.SelectedValue + "'";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }


        else
        {
            string insertdatabase = "insert into User_Role (User_id,Role_id,ActiveDeactive)values(" + dpdUserName.SelectedValue + "," + dpdRoleName.SelectedValue + "," + ddlstatus.SelectedValue + ")";
            SqlCommand cmd = new SqlCommand(insertdatabase, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";

            pnladd.Visible = false;
            lbllegend.Visible = false;
            btnadd.Visible = true;
            lbllegend.Text = "Assign Roles to Users";

            //ddlstatus.SelectedIndex = -1;
            //dpdRoleName.SelectedIndex = -1;
            //dpdUserName.SelectedIndex = -1;


            btnupdate.Visible = false;
            btSubmit.Visible = true;

            fillgrid();

            fillstore();
            filluser();
            fillRole();




        }






    }

    protected void filluser()
    {

        string str = "SELECT User_master.UserID,PartytTypeMaster.PartType+':'+Party_master.Compname+':'+Party_master.Contactperson as Name FROM [User_master]  inner join Party_master  on Party_master.PartyID=User_master.PartyID  inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId  where Party_master.id='" + compid + "'  and Party_master.Whid='" + ddlbusinessname.SelectedValue + "' order by PartytTypeMaster.PartType,Party_master.Compname  ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        dpdUserName.DataSource = dt;
        dpdUserName.DataTextField = "Name";
        dpdUserName.DataValueField = "UserID";
        dpdUserName.DataBind();




    }
    protected void fillRole()
    {
        DataTable dt = new DataTable();

        //dt = obj.selectGriddata_role(compid);

        SqlDataAdapter da = new SqlDataAdapter("SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + compid + "'  order by Role_name", con);

        da.Fill(dt);

        dpdRoleName.DataSource = dt;
        dpdRoleName.DataTextField = "Role_name";
        dpdRoleName.DataValueField = "Role_id";
        dpdRoleName.DataBind();


    }

    protected void fillgrid()
    {
        lblCompany.Text = Session["Cname"].ToString();

        lblbusinessprint.Text = ddlfilterbybusiness.SelectedItem.Text;
        lblusertypeprint.Text = ddlfilterbyusertype.SelectedItem.Text;
        lblusernameprint.Text = ddlfilterbyusername.SelectedItem.Text;
        lblrolenameprint.Text = ddlfilterbyrole.SelectedItem.Text;
        lblstatusprint.Text = ddlfilterbystatus.SelectedItem.Text;

        string st1 = "";
        string st2 = "";
        string st3 = "";
        string st4 = "";
        string st5 = "";
        string st6 = "";

        if (ddlfilterbybusiness.SelectedIndex > 0)
        {
            st1 = " and Party_master.Whid='" + ddlfilterbybusiness.SelectedValue + "' ";
        }
        if (ddlfilterbyusertype.SelectedIndex > 0)
        {
            st2 = " and  Party_master.PartyTypeId='" + ddlfilterbyusertype.SelectedValue + "'";

        }
        if (ddlfilterbyusername.SelectedIndex > 0)
        {
            st3 = " and  User_master.UserID ='" + ddlfilterbyusername.SelectedValue + "'";
        }
        if (ddlfilterbyrole.SelectedIndex > 0)
        {
            st4 = " and  User_Role.Role_id ='" + ddlfilterbyrole.SelectedValue + "'";
        }
        if (ddlfilterbystatus.SelectedIndex > 0)
        {
            st5 = " and  User_Role.ActiveDeactive ='" + ddlfilterbystatus.SelectedValue + "'";
        }
        if (txtsearchbyusername.Text.Length > 0)
        {
            st6 = " and (Party_master.Compname like '%" + txtsearchbyusername.Text + "%' or Party_master.Contactperson like '%" + txtsearchbyusername.Text + "%')  ";
        }


        string strfillgrid = " WareHouseMaster.Name as Wname ,Party_master.Compname+':'+Party_master.Contactperson as Compname ,PartytTypeMaster.PartType,User_master.Name ,Username, RoleMaster.Role_name,  case when (User_Role.ActiveDeactive='1') then 'Active' else 'Inactive' End as ActiveDeactive,User_Role.User_Role_id FROM User_Role INNER JOIN RoleMaster ON User_Role.Role_id = RoleMaster.Role_id INNER JOIN User_master ON User_Role.User_id = User_master.UserID inner join Party_master on Party_master.PartyID=User_master.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid where Party_master.id='" + Session["comid"] + "' " + st1 + " " + st2 + " " + st3 + " " + st4 + " " + st5 + " " + st6 + "  ";

        string str2 = " select count(User_Role.User_Role_id) as ci from User_Role INNER JOIN RoleMaster ON User_Role.Role_id = RoleMaster.Role_id INNER JOIN User_master ON User_Role.User_id = User_master.UserID inner join Party_master on Party_master.PartyID=User_master.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid where Party_master.id='" + Session["comid"] + "' " + st1 + " " + st2 + " " + st3 + " " + st4 + " " + st5 + " " + st6 + "  ";

        //string sorting = "order by WareHouseMaster.Name,PartytTypeMaster.PartType,Party_master.Compname,RoleMaster.Role_name,User_Role.ActiveDeactive";


        //strfillgrid = strfillgrid + st1 + st2 + st3 + st4 + st5 + st6 + sorting;

        //SqlCommand cmdfillgrid = new SqlCommand(strfillgrid, con);
        //SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
        //DataTable dtfill = new DataTable();
        //adpfillgrid.Fill(dtfill);

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,PartytTypeMaster.PartType,Party_master.Compname,RoleMaster.Role_name,User_Role.ActiveDeactive";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dtfill = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, strfillgrid);

            GridView1.DataSource = dtfill;

            DataView myDataView = new DataView();
            myDataView = dtfill.DefaultView;

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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lblid.Text = e.CommandArgument.ToString();
            ViewState["editid"] = e.CommandArgument.ToString();

            string str = "select User_Role.*,Party_master.Whid from User_Role INNER JOIN RoleMaster ON User_Role.Role_id = RoleMaster.Role_id INNER JOIN User_master ON User_Role.User_id = User_master.UserID inner join Party_master on Party_master.PartyID=User_master.PartyID  where User_Role.User_Role_id='" + ViewState["editid"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                fillstore();
                ddlbusinessname.SelectedIndex = ddlbusinessname.Items.IndexOf(ddlbusinessname.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
                filluser();
                dpdUserName.SelectedIndex = dpdUserName.Items.IndexOf(dpdUserName.Items.FindByValue(dt.Rows[0]["User_id"].ToString()));
                fillRole();
                dpdRoleName.SelectedIndex = dpdRoleName.Items.IndexOf(dpdRoleName.Items.FindByValue(dt.Rows[0]["Role_id"].ToString()));

                string chk = dt.Rows[0]["ActiveDeactive"].ToString();

                if (chk == "True")
                {

                    ddlstatus.SelectedValue = "1";
                }
                else
                {

                    ddlstatus.SelectedValue = "0";
                }
                btnupdate.Visible = true;
                btSubmit.Visible = false;


                pnladd.Visible = true;
                lbllegend.Visible = true;
                btnadd.Visible = false;
                lbllegend.Text = "Edit Roles to Users";



            }



        }

    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btCancel_Click(object sender, EventArgs e)
    {

        lblmsg.Text = "";
        pnladd.Visible = false;
        lbllegend.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "Assign Roles to Users";
        ddlstatus.SelectedIndex = -1;
        dpdRoleName.SelectedIndex = -1;
        dpdUserName.SelectedIndex = -1;
        btnupdate.Visible = false;
        btSubmit.Visible = true;
        fillstore();
        filluser();
        fillRole();

    }
    protected void dpdUserName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ViewState["Id"] = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string str = "delete from User_Role  where User_Role_id=' " + ViewState["Id"] + "' ";
        DataSet ds = new DataSet();
        SqlCommand cmdd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdd.ExecuteNonQuery();
        con.Close();


        GridView1.EditIndex = -1;
        fillgrid();
        lblmsg.Visible = true;
        lblmsg.Text = "Record deleted successfully";
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (Button8.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillgrid();

            Button8.Text = "Hide Printable Version";
            Button2.Visible = true;

            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["edithide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["delhide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }

        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = 20;
            fillgrid();

            Button8.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["edithide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
            if (ViewState["delhide"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }

        }
    }
    protected void ddlbusinessname_SelectedIndexChanged(object sender, EventArgs e)
    {
        filluser();
    }
    protected void fillstore()
    {
        ddlbusinessname.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlbusinessname.DataSource = ds;
        ddlbusinessname.DataTextField = "Name";
        ddlbusinessname.DataValueField = "WareHouseId";
        ddlbusinessname.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlbusinessname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

    }

    protected void filterbystore()
    {
        ddlfilterbybusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();

        ddlfilterbybusiness.DataSource = ds;
        ddlfilterbybusiness.DataTextField = "Name";
        ddlfilterbybusiness.DataValueField = "WareHouseId";
        ddlfilterbybusiness.DataBind();

        ddlfilterbybusiness.Items.Insert(0, "All");
        ddlfilterbybusiness.SelectedItem.Value = "0";
    }
    protected void filterbyusertype()
    {
        ddlfilterbyusertype.Items.Clear();

        string str = "SELECT PartytTypeMaster.PartType,PartytTypeMaster.PartyTypeId FROM PartytTypeMaster  where compid='" + compid + "' order by PartytTypeMaster.PartType  ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlfilterbyusertype.DataSource = dt;
            ddlfilterbyusertype.DataTextField = "PartType";
            ddlfilterbyusertype.DataValueField = "PartyTypeId";
            ddlfilterbyusertype.DataBind();
            ddlfilterbyusertype.Items.Insert(0, "All");
            ddlfilterbyusertype.SelectedItem.Value = "0";
        }
        else
        {
            ddlfilterbyusertype.Items.Insert(0, "All");
            ddlfilterbyusertype.SelectedItem.Value = "0";

        }
    }
    protected void filterbyrolename()
    {
        DataTable dt = new DataTable();

        // dt = obj.selectGriddata_role(compid);

        SqlDataAdapter da = new SqlDataAdapter("SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + compid + "'  order by Role_name", con);

        da.Fill(dt);

        ddlfilterbyrole.DataSource = dt;
        ddlfilterbyrole.DataTextField = "Role_name";
        ddlfilterbyrole.DataValueField = "Role_id";
        ddlfilterbyrole.DataBind();

        ddlfilterbyrole.Items.Insert(0, "All");
        ddlfilterbyrole.SelectedItem.Value = "0";
    }
    protected void filterbyusername()
    {
        string str1 = "";
        string str = "SELECT User_master.UserID,PartytTypeMaster.PartType+':'+Party_master.Compname +':'+Party_master.Contactperson as Name FROM [User_master]  inner join Party_master  on Party_master.PartyID=User_master.PartyID  inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId  where Party_master.id='" + compid + "'  and Party_master.Whid='" + ddlfilterbybusiness.SelectedValue + "'   ";

        if (ddlfilterbyusertype.SelectedIndex > 0)
        {
            str1 = " and  PartytTypeMaster.PartyTypeId='" + ddlfilterbyusertype.SelectedValue + "'";
        }

        string strsorting = "order by PartytTypeMaster.PartType,Party_master.Compname";

        string finalstr = str + str1 + strsorting;

        SqlDataAdapter da = new SqlDataAdapter(finalstr, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlfilterbyusername.DataSource = dt;
        ddlfilterbyusername.DataTextField = "Name";
        ddlfilterbyusername.DataValueField = "UserID";
        ddlfilterbyusername.DataBind();

        ddlfilterbyusername.Items.Insert(0, "All");
        ddlfilterbyusername.SelectedItem.Value = "0";
    }
    protected void ddlfilterbybusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterbyusertype();
        filterbyusername();
        fillgrid();
    }
    protected void ddlfilterbyusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterbyusername();
        fillgrid();
    }
    protected void ddlfilterbyusername_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlfilterbyrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlfilterbystatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void txtsearchbyusername_TextChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            lbllegend.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            lbllegend.Visible = false;
        }
        btnadd.Visible = false;
        lblmsg.Text = "";
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {


        string str = " Select * from User_Role where User_id='" + dpdUserName.SelectedValue + "' and Role_id='" + dpdRoleName.SelectedValue + "' and User_Role_id<>'" + ViewState["editid"] + "'";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }


        else
        {
            string insertdatabase = " Update User_Role set User_id='" + dpdUserName.SelectedValue + "',Role_id='" + dpdRoleName.SelectedValue + "',ActiveDeactive='" + ddlstatus.SelectedValue + "' where User_Role_id='" + ViewState["editid"] + "'";
            SqlCommand cmd = new SqlCommand(insertdatabase, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";

            pnladd.Visible = false;
            lbllegend.Visible = false;
            btnadd.Visible = true;
            lbllegend.Text = "Assign Roles to Users";
            //ddlstatus.SelectedIndex = -1;
            //dpdRoleName.SelectedIndex = -1;
            //dpdUserName.SelectedIndex = -1;
            btnupdate.Visible = false;
            btSubmit.Visible = true;

            fillgrid();

            fillstore();
            filluser();
            fillRole();




        }



    }
    protected void imgadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PartyMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgaddrem_Click(object sender, ImageClickEventArgs e)
    {
        string te = "RoleMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgref_Click(object sender, ImageClickEventArgs e)
    {
        filluser();
    }
    protected void imgrefrem_Click(object sender, ImageClickEventArgs e)
    {
        fillRole();
    }
}
