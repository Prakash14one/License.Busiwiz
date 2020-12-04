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

public partial class Add_Group_Master : System.Web.UI.Page
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

        lblmsg.Visible = false;

        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";

            fillwarehouse();
            FillStateWithCountry();

            fillstore();
            ddlstore_SelectedIndexChanged(sender, e);
            fillgrid();
        }
    }



    protected void fillwarehouse()
    {
        string str1 = "select * from WarehouseMaster where comid='" + Session["comid"] + "'and [WareHouseMaster].status = '1' order by Name";

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds1;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WarehouseId";
            ddlwarehouse.DataBind();

            ddlwarehouse.Items.Insert(0, "-Select-");
            ddlwarehouse.Items[0].Value = "0";
        }
    }



    protected void FillStateWithCountry()
    {

        ddlcity.Items.Clear();
        if (ddlwarehouse.SelectedIndex > 0)
        {
            string str4500 = "SELECT ClassCompanyMaster.id as ccid, ClassCompanyMaster.ClassId,ClassTypeCompanyMaster.ClassTypeId,ClassTypeCompanyMaster.displayname + ' : ' + ClassCompanyMaster.displayname AS country FROM  ClassCompanyMaster inner join ClassTypeCompanyMaster ON ClassCompanyMaster.classtypecompanymasterid = ClassTypeCompanyMaster.id WHERE(ClassTypeCompanyMaster.Status = 1) AND (ClassCompanyMaster.active = 1) and ClassCompanyMaster.cid='" + Session["comid"] + "' and ClassCompanyMaster.Whid='" + ddlwarehouse.SelectedValue + "' order by ClassTypeCompanyMaster.displayname,ClassCompanyMaster.displayname";
            SqlCommand cmd4500 = new SqlCommand(str4500, con);

            SqlDataAdapter da00 = new SqlDataAdapter(cmd4500);

            DataTable ds00 = new DataTable();

            da00.Fill(ds00);
            ddlcity.DataSource = ds00;
            ddlcity.DataTextField = "country";
            ddlcity.DataValueField = "ccid";
            ddlcity.DataBind();
            ddlcity.Items.Insert(0, "-Select-");
            ddlcity.SelectedItem.Value = "0";

        }

    }

    protected void fillgrid()
    {
        string str1 = "";

        string str2 = "";

        if (ddlstore.SelectedIndex > 0)
        {
            if (ddlSearchByCountry.SelectedIndex > 0)
            {
                if (ddlSearchByState.SelectedIndex > 0)
                {

                    str1 = "  [WareHouseMaster].Name,GroupCompanyMaster.groupdisplayname,case when (GroupCompanyMaster.active = 1) then 'Active' else 'Inactive' end as active, GroupCompanyMaster.Id, ClassTypeCompanyMaster.ClassTypeId,ClassTypeCompanyMaster.displayname + ' : ' + ClassCompanyMaster.displayname AS country,ClassCompanyMaster.ClassId, GroupCompanyMaster.classcompanymasterid FROM ClassCompanyMaster inner JOIN ClassTypeCompanyMaster ON ClassCompanyMaster.classtypecompanymasterid = ClassTypeCompanyMaster.id inner JOIN GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid inner join [WareHouseMaster] on [WareHouseMaster].[WareHouseId] = [GroupCompanyMaster].Whid AND (ClassCompanyMaster.active = 1) AND  (ClassTypeCompanyMaster.Status = 1) And (GroupCompanyMaster.cid='" + Session["comid"] + "') and ClassCompanyMaster.id='" + ddlSearchByState.SelectedValue + "' where [WareHouseMaster].status = '1'";
                    //order by [WareHouseMaster].Name,country, GroupCompanyMaster.groupdisplayname";

                    str2 = " select count(GroupCompanyMaster.Id) as ci FROM ClassCompanyMaster inner JOIN ClassTypeCompanyMaster ON ClassCompanyMaster.classtypecompanymasterid = ClassTypeCompanyMaster.id inner JOIN GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid inner join [WareHouseMaster] on [WareHouseMaster].[WareHouseId] = [GroupCompanyMaster].Whid AND (ClassCompanyMaster.active = 1) AND  (ClassTypeCompanyMaster.Status = 1) And (GroupCompanyMaster.cid='" + Session["comid"] + "') and ClassCompanyMaster.id='" + ddlSearchByState.SelectedValue + "' where [WareHouseMaster].status = '1'";

                }
                else
                {
                    str1 = "  [WareHouseMaster].Name,GroupCompanyMaster.groupdisplayname,case when (GroupCompanyMaster.active = 1) then 'Active' else 'Inactive' end as active, GroupCompanyMaster.Id, ClassTypeCompanyMaster.ClassTypeId,ClassTypeCompanyMaster.displayname + ' : ' + ClassCompanyMaster.displayname AS country,ClassCompanyMaster.ClassId, GroupCompanyMaster.classcompanymasterid FROM ClassCompanyMaster inner JOIN ClassTypeCompanyMaster ON ClassCompanyMaster.classtypecompanymasterid = ClassTypeCompanyMaster.id inner JOIN GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid inner join [WareHouseMaster] on [WareHouseMaster].[WareHouseId] = [GroupCompanyMaster].Whid AND (ClassCompanyMaster.active = 1) AND  (ClassTypeCompanyMaster.Status = 1) And (GroupCompanyMaster.cid='" + Session["comid"] + "') and ClassTypeCompanyMaster.id='" + ddlSearchByCountry.SelectedValue + "' where [WareHouseMaster].status = '1'";
                    //order by [WareHouseMaster].Name,country, GroupCompanyMaster.groupdisplayname";

                    str2 = " select count(GroupCompanyMaster.Id) as ci FROM ClassCompanyMaster inner JOIN ClassTypeCompanyMaster ON ClassCompanyMaster.classtypecompanymasterid = ClassTypeCompanyMaster.id inner JOIN GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid inner join [WareHouseMaster] on [WareHouseMaster].[WareHouseId] = [GroupCompanyMaster].Whid AND (ClassCompanyMaster.active = 1) AND  (ClassTypeCompanyMaster.Status = 1) And (GroupCompanyMaster.cid='" + Session["comid"] + "') and ClassTypeCompanyMaster.id='" + ddlSearchByCountry.SelectedValue + "' where [WareHouseMaster].status = '1'";
                }

            }
            else
            {

                str1 = "  [WareHouseMaster].Name,GroupCompanyMaster.groupdisplayname,case when (GroupCompanyMaster.active = 1) then 'Active' else 'Inactive' end as active, GroupCompanyMaster.Id, ClassTypeCompanyMaster.ClassTypeId,ClassTypeCompanyMaster.displayname + ' : ' + ClassCompanyMaster.displayname AS country,ClassCompanyMaster.ClassId, GroupCompanyMaster.classcompanymasterid FROM ClassCompanyMaster inner JOIN ClassTypeCompanyMaster ON ClassCompanyMaster.classtypecompanymasterid = ClassTypeCompanyMaster.id inner JOIN GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid inner join [WareHouseMaster] on [WareHouseMaster].[WareHouseId] = [GroupCompanyMaster].Whid AND (ClassCompanyMaster.active = 1) AND  (ClassTypeCompanyMaster.Status = 1) And (GroupCompanyMaster.cid='" + Session["comid"] + "') and GroupCompanyMaster.Whid='" + ddlstore.SelectedValue + "' where [WareHouseMaster].status = '1'";
                //order by [WareHouseMaster].Name,country, GroupCompanyMaster.groupdisplayname";

                str2 = " select count(GroupCompanyMaster.Id) as ci FROM ClassCompanyMaster inner JOIN ClassTypeCompanyMaster ON ClassCompanyMaster.classtypecompanymasterid = ClassTypeCompanyMaster.id inner JOIN GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid inner join [WareHouseMaster] on [WareHouseMaster].[WareHouseId] = [GroupCompanyMaster].Whid AND (ClassCompanyMaster.active = 1) AND  (ClassTypeCompanyMaster.Status = 1) And (GroupCompanyMaster.cid='" + Session["comid"] + "') and GroupCompanyMaster.Whid='" + ddlstore.SelectedValue + "' where [WareHouseMaster].status = '1'";
            }
        }

        else
        {

            str1 = "  [WareHouseMaster].Name, GroupCompanyMaster.cid,groupdisplayname,case when (GroupCompanyMaster.active = 1) then 'Active' else 'Inactive' end as active, GroupCompanyMaster.Id, ClassTypeCompanyMaster.ClassTypeId,ClassTypeCompanyMaster.displayname + ' : ' + ClassCompanyMaster.displayname AS country,ClassCompanyMaster.ClassId, GroupCompanyMaster.classcompanymasterid FROM ClassCompanyMaster inner JOIN ClassTypeCompanyMaster ON ClassCompanyMaster.classtypecompanymasterid = ClassTypeCompanyMaster.id inner JOIN GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid inner join [WareHouseMaster] on [WareHouseMaster].[WareHouseId] = [GroupCompanyMaster].Whid  AND (ClassCompanyMaster.active = 1) AND  (ClassTypeCompanyMaster.Status = 1) And (GroupCompanyMaster.cid= '" + Session["comid"] + "') where [WareHouseMaster].status = '1'";
            //order by [WareHouseMaster].Name,country, GroupCompanyMaster.groupdisplayname";

            str2 = " select count(GroupCompanyMaster.Id) as ci FROM ClassCompanyMaster inner JOIN ClassTypeCompanyMaster ON ClassCompanyMaster.classtypecompanymasterid = ClassTypeCompanyMaster.id inner JOIN GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid inner join [WareHouseMaster] on [WareHouseMaster].[WareHouseId] = [GroupCompanyMaster].Whid  AND (ClassCompanyMaster.active = 1) AND  (ClassTypeCompanyMaster.Status = 1) And (GroupCompanyMaster.cid= '" + Session["comid"] + "') where [WareHouseMaster].status = '1'";
        }



        lblctype.Text = ddlSearchByCountry.SelectedItem.Text;
        lblcmast.Text = ddlSearchByState.SelectedItem.Text;

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,ClassTypeCompanyMaster.displayname,ClassCompanyMaster.displayname , GroupCompanyMaster.groupdisplayname";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable ds1 = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str1);

            GridView1.DataSource = ds1;


            DataView myDataView = new DataView();
            myDataView = ds1.DefaultView;

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

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        //int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString()); //.SelectedDataKey.Value);
        ViewState["id"] = GridView1.DataKeys[e.NewEditIndex].Value.ToString();

        string qs = "SELECT * FROM GroupCompanyMaster  WHERE Id ='" + ViewState["id"] + "' and cid='" + Session["comid"].ToString() + "'";

        SqlCommand cmdclasstypeid = new SqlCommand(qs, con);

        SqlDataAdapter dtpaccid = new SqlDataAdapter(cmdclasstypeid);
        DataTable dtaccid = new DataTable();
        dtpaccid.Fill(dtaccid);
        if (dtaccid.Rows.Count > 0)
        {
            Panel1.Visible = true;
            lbladd.Text = "Edit Account Group";
            ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(dtaccid.Rows[0]["Whid"].ToString()));
            ddlwarehouse_SelectedIndexChanged(sender, e);
            ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(dtaccid.Rows[0]["classcompanymasterid"].ToString()));
            txtcity.Text = dtaccid.Rows[0]["groupdisplayname"].ToString();
            txtdesc.Text = dtaccid.Rows[0]["Description"].ToString();

            ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(dtaccid.Rows[0]["Active"].ToString()));
            //chbxActiveStatus.Checked = Convert.ToBoolean(dtaccid.Rows[0]["Active"].ToString());

            //img1.ImageUrl = "~/ShoppingCart/images/update.png";
            img1.Visible = true;
            img2.Visible = true;
            ddlwarehouse.Enabled = false;
        }
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {

        }

        if (e.CommandName == "Sort")
        {
            return;
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




    protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlSearchByCity_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillgrid();
    }
    protected void ddlSearchByState_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillgrid();

    }
    protected void ddlSearchByCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchByCountry.SelectedIndex > 0)
        {

            ddlSearchByState.Items.Clear();



            string str45090 = "SELECT distinct id,displayname  ,ClassId ,active" +
                  " FROM  ClassCompanyMaster where classtypecompanymasterid='" + ddlSearchByCountry.SelectedValue + "' AND (active = 1) AND cid='" + Session["comid"] + "'  " +
                  " Order By displayname";



            SqlCommand cmd4590 = new SqlCommand(str45090, con);

            SqlDataAdapter da90 = new SqlDataAdapter(cmd4590);
            //da.SelectCommand = cmd;
            DataTable ds90 = new DataTable();

            da90.Fill(ds90);
            ddlSearchByState.DataSource = ds90;
            ddlSearchByState.DataTextField = "displayname";
            ddlSearchByState.DataValueField = "id";
            ddlSearchByState.DataBind();

            ddlSearchByState.Items.Insert(0, "All");
            ddlSearchByState.SelectedItem.Value = "0";
            ddlSearchByState_SelectedIndexChanged(sender, e);

        }
        else
        {
            ddlSearchByState.Items.Clear();
            ddlSearchByState.Items.Insert(0, "All");
            ddlSearchByState.SelectedItem.Value = "0";



            fillgrid();
        }

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ddlcity.SelectedIndex = 0;
        txtcity.Text = "";

    }

    protected void fillstore()
    {
        string strstore = "select * from WarehouseMaster where comid='" + Session["comid"] + "'and [WareHouseMaster].status = '1' order by Name";

        DataTable dsstore = new DataTable();
        SqlDataAdapter dastore = new SqlDataAdapter(strstore, con);
        dastore.Fill(dsstore);
        if (dsstore.Rows.Count > 0)
        {
            ddlstore.DataSource = dsstore;
            ddlstore.DataTextField = "Name";
            ddlstore.DataValueField = "WarehouseId";
            ddlstore.DataBind();
            //  ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByText("Eplaza Store"));
            ddlstore.Items.Insert(0, "All");
            ddlstore.Items[0].Value = "0";

            lblcomname.Text = Session["Cname"].ToString();
            lblbusinessprint.Text = ddlstore.SelectedItem.Text;
        }
    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstore.SelectedIndex > 0)
        {
            lblcomname.Text = Session["Cname"].ToString();
            lblbusinessprint.Text = ddlstore.SelectedItem.Text;
            string str = "Select distinct ClassTypeCompanyMaster.displayname AS displayname,ClassTypeCompanyMaster.Id from ClassTypeCompanyMaster where cid= '" + Session["comid"] + "' and ClassTypeCompanyMaster.whid='" + ddlstore.SelectedValue + "' order by displayname";

            //string str = "Select distinct ClassTypeCompanyMaster.displayname+ ':' + ClassCompanyMaster.displayname AS displayname ,ClassTypeCompanyMaster.Id from  ClassTypeCompanyMaster inner join ClassCompanyMaster on ClassCompanyMaster.classtypecompanymasterid = ClassTypeCompanyMaster.id WHERE (Status = 1) AND ClassCompanyMaster.cid= '" + Session["comid"] + "' and ClassCompanyMaster.whid='" + ddlstore.SelectedValue + "' order by displayname";


            SqlCommand cmd = new SqlCommand(str);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable ds = new DataTable();

            da.Fill(ds);


            ddlSearchByCountry.DataSource = ds;
            ddlSearchByCountry.DataTextField = "displayname";
            ddlSearchByCountry.DataValueField = "id";
            ddlSearchByCountry.DataBind();
            ddlSearchByCountry.Items.Insert(0, "All");
            ddlSearchByCountry.SelectedItem.Value = "0";


            object sen = new object();
            EventArgs gg = new EventArgs();

            ddlSearchByCountry_SelectedIndexChanged(sen, gg);

        }
        else
        {
            ddlSearchByCountry.Items.Clear();
            ddlSearchByCountry.Items.Insert(0, "All");
            ddlSearchByState.Items.Clear();
            ddlSearchByState.Items.Insert(0, "All");

            lblcomname.Text = Session["Cname"].ToString();
            lblbusinessprint.Text = ddlstore.SelectedItem.Text;

            fillgrid();
        }
    }
    protected void img1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        String selectStr = "select [WareHouseMaster].[WareHouseId],Groupdisplayname from GroupCompanyMaster inner join [WareHouseMaster] on [WareHouseMaster].[WareHouseId] = GroupCompanyMaster.[whid] where classcompanymasterid='" + ddlcity.SelectedValue + "' and  groupdisplayname = '" + txtcity.Text + "' and id <>'" + ViewState["id"].ToString() + "' and Whid='" + ddlwarehouse.SelectedValue + "' ";
        SqlCommand selectCmd = new SqlCommand(selectStr, con);
        SqlDataAdapter dtp = new SqlDataAdapter(selectCmd);
        DataTable dt11 = new DataTable();
        dtp.Fill(dt11);
        if (dt11.Rows.Count > 0)
        {
            lblmsg.Text = "Record already exists";
            lblmsg.Visible = true;
        }
        else
        {
            SqlCommand mycmd = new SqlCommand("Sp_Update_GroupCompanyMaster", con);
            mycmd.CommandType = CommandType.StoredProcedure;
            mycmd.Parameters.AddWithValue("@GroupId", ViewState["id"].ToString());
            mycmd.Parameters.AddWithValue("@classcompanymasterid", ddlcity.SelectedValue);
            mycmd.Parameters.AddWithValue("@groupdisplayname", txtcity.Text);
            mycmd.Parameters.AddWithValue("@description", txtdesc.Text);
            mycmd.Parameters.AddWithValue("@active", ddlstatus.SelectedValue);
            mycmd.Parameters.AddWithValue("@cid", Session["comid"].ToString());
            mycmd.Parameters.AddWithValue("@Whid", ddlwarehouse.SelectedValue);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            mycmd.ExecuteNonQuery();



            con.Close();
            lblmsg.Visible = true;
            img1.Visible = false;
            img2.Visible = false;
            lblmsg.Text = "Record updated successfully";
            txtcity.Text = "";

            txtdesc.Text = "";
            ddlwarehouse.SelectedIndex = 0;
            ddlwarehouse.Enabled = true;
            ddlstatus.SelectedIndex = 0;
            //chbxActiveStatus.Checked = false;
            ddlcity.SelectedIndex = 0;
            GridView1.EditIndex = -1;
            //Fillddlcountry();
            fillgrid();
        }
    }
    protected void img2_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        img2.Visible = false;
        img1.Visible = false;
        txtcity.Text = "";
        txtdesc.Text = "";
        ddlcity.SelectedIndex = 0;
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillStateWithCountry();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 10000;
            fillgrid();

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }

        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = 20;
            fillgrid();

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }

        }
    }
}
