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

public partial class ShoppingCart_Admin_partyautoallocationnew : System.Web.UI.Page
{

    string compid;
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(PageConn.connnn);

    protected void Page_Load(object sender, EventArgs e)
    {
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Page.Title = pg.getPageTitle(page);
        compid = Session["Comid"].ToString();



        if (!Page.IsPostBack)
        {
            lblCompany.Text = Session["Cname"].ToString();

            ViewState["sortOrder"] = "";
            fillwarehouse();

            RadioButtonList1_SelectedIndexChanged(sender, e);

            fillaccountmanager();
            fillreceivedept();
            fillpurchasedept();
            fillshippingdept();
            fillsalesdept();

            fillterstore();
            fillcon();
            fillstatebyfilter();
            fillcity1();
            fillgrid();


        }
    }


    protected void fillwarehouse()
    {

        ddlSearchByStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlSearchByStore.DataSource = ds;
        ddlSearchByStore.DataTextField = "Name";
        ddlSearchByStore.DataValueField = "WareHouseId";
        ddlSearchByStore.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlSearchByStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
    protected void fillterstore()
    {


        DropDownList1.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";


    }


    public DataTable fillemployee()
    {

        DataTable dtemp = ClsStore.SelectEmployeewithBusinessId(ddlSearchByStore.SelectedValue);
        return dtemp;


    }
    protected void fillstatebyfilter()
    {


        dllstate1.Items.Clear();

        string str45 = "SELECT     StateName  ,StateId FROM  StateMasterTbl where CountryId='" + ddlcountry1.SelectedValue + "' Order By StateName ";
        SqlCommand cmd45 = new SqlCommand(str45, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd45);
        DataTable ds = new DataTable();
        da.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            dllstate1.DataSource = ds;
            dllstate1.DataTextField = "StateName";
            dllstate1.DataValueField = "StateId";
            dllstate1.DataBind();
            dllstate1.Items.Insert(0, "All");
            dllstate1.Items[0].Value = "0";
        }
        else
        {
            dllstate1.DataSource = null;
            dllstate1.DataTextField = "StateName";
            dllstate1.DataValueField = "StateId";
            dllstate1.DataBind();
            dllstate1.Items.Insert(0, "All");
            dllstate1.Items[0].Value = "0";

        }





    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedradio();

    }
    protected void ddlselectcountry_SelectedIndexChanged(object sender, EventArgs e)
    {


        fillstate();
        fillcity();

    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillcity();

    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        string str45 = "SELECT  * from PartyAutoAllocationManager where Whid='" + ddlSearchByStore.SelectedValue + "' and AllocationMethod='" + RadioButtonList1.SelectedValue + "' ";
        SqlCommand cmd45 = new SqlCommand(str45, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd45);
        DataTable ds = new DataTable();
        da.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "Record already exist";

        }
        else
        {
            if (ddlAssignedAccountManager.SelectedIndex == -1 || ddlAssignedPurchseDept.SelectedIndex == -1 || ddlAssignedSalesDept.SelectedIndex == -1 || ddlAssignedRecievingDept.SelectedIndex == -1 || ddlAssignedShippingDept.SelectedIndex == -1)
            {
                statuslable.Visible = true;
                statuslable.Text = "Please fill all mandatory fields";
            }
            else
            {
                string strinsert = "Insert into PartyAutoAllocationManager values('0','" + ddlselectcountry.SelectedValue + "','" + ddlstate.SelectedValue + "','" + ddlcity.SelectedValue + "','" + ddlAssignedAccountManager.SelectedValue + "','" + ddlAssignedRecievingDept.SelectedValue + "','" + ddlAssignedPurchseDept.SelectedValue + "','" + ddlAssignedShippingDept.SelectedValue + "','" + ddlAssignedSalesDept.SelectedValue + "','" + compid + "','" + ddlSearchByStore.SelectedValue + "','" + RadioButtonList1.SelectedValue + "')";
                SqlCommand cmd1 = new SqlCommand(strinsert, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }
                cmd1.ExecuteNonQuery();
                con.Close();

                pnladd.Visible = false;
                btnadd.Visible = true;
                Button3.Visible = false;
                ImageButton1.Visible = true;
                lbladdlabel.Visible = false;
                lbladdlabel.Text = "Add Support Team";

                statuslable.Visible = true;
                statuslable.Text = "Record inserted successfully";

                fillgrid();
            }


        }




    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillgrid();

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
            if (GridView1.Columns[10].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[10].Visible = false;
            }

        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            fillgrid();

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[10].Visible = true;
            }
        }

    }

    public void fillgrid()
    {
        lblbusinessprint.Text = DropDownList1.SelectedItem.Text;
        lblcon.Text = ddlcountry1.SelectedItem.Text;
        lblst.Text = dllstate1.SelectedItem.Text;
        lblcityyyy.Text = dllcity1.SelectedItem.Text;

        string str1 = "";
        string str2 = "";
        string str3 = "";
        string str4 = "";

        if (DropDownList1.SelectedIndex > 0)
        {
            str1 = " and PartyAutoAllocationManager.Whid=" + DropDownList1.SelectedValue + ""; ;
        }
        if (ddlcountry1.SelectedIndex > 0)
        {

            str2 = " and CountryMaster.CountryId=" + ddlcountry1.SelectedValue + " ";
        }
        if (dllstate1.SelectedIndex > 0)
        {

            str3 = " and StateMasterTbl.StateId=" + dllstate1.SelectedValue + " ";
        }
        if (dllcity1.SelectedIndex > 0)
        {

            str4 = " and CityMasterTbl.CityId=" + dllcity1.SelectedValue + "";
        }

        string str = "  WareHouseMaster.Name as Wname,   PartyAutoAllocationManager.PartyAutoAllocationID,   " +
                     " CityMasterTbl.CityId,   case when CityMasterTbl.CityName is null or CityMasterTbl.CityName='0'  then 'ALL' else CityMasterTbl.CityName end as CityName , " +
                     " StateMasterTbl.StateId, case when   StateMasterTbl.StateName IS null or  StateMasterTbl.StateName='0' then 'ALL' ELSE StateMasterTbl.StateName END AS StateName, " +
                     " CountryMaster.CountryId,case when   CountryMaster.CountryName is null or CountryMaster.CountryName='0' then 'ALL' else  CountryMaster.CountryName end as   CountryName, " +
                     " PartyAutoAllocationManager.AssignedAccountManager, PartyAutoAllocationManager.AssignedRecievingDept, PartyAutoAllocationManager.AssignedPurchseDept, PartyAutoAllocationManager.AssignedShippingDept,PartyAutoAllocationManager.AssignedSalesDept,  " +
                    " EmployeeMaster_1.EmployeeName AS AccountMgr, EmployeeMaster_2.EmployeeName AS RcvDpt, EmployeeMaster_3.EmployeeName AS SalesDpt, EmployeeMaster_4.EmployeeName AS PrcDpt, EmployeeMaster.EmployeeName AS ShipDpt  " +

                  " FROM  EmployeeMaster RIGHT OUTER JOIN " +
                  "  EmployeeMaster AS EmployeeMaster_4 RIGHT OUTER JOIN " +
                  "  EmployeeMaster AS EmployeeMaster_3 RIGHT OUTER JOIN " +
                  "  PartyAutoAllocationManager ON EmployeeMaster_3.EmployeeMasterID = PartyAutoAllocationManager.AssignedSalesDept ON  " +
                  "  EmployeeMaster_4.EmployeeMasterID = PartyAutoAllocationManager.AssignedPurchseDept ON  " +
                  "  EmployeeMaster.EmployeeMasterID = PartyAutoAllocationManager.AssignedShippingDept LEFT OUTER JOIN" +
                  "  EmployeeMaster AS EmployeeMaster_2 ON PartyAutoAllocationManager.AssignedRecievingDept = EmployeeMaster_2.EmployeeMasterID LEFT OUTER JOIN " +
                  "  EmployeeMaster AS EmployeeMaster_1 ON PartyAutoAllocationManager.AssignedAccountManager = EmployeeMaster_1.EmployeeMasterID LEFT OUTER JOIN " +
                  "  CountryMaster ON PartyAutoAllocationManager.Country = CountryMaster.CountryId LEFT OUTER JOIN " +
                  "  StateMasterTbl ON PartyAutoAllocationManager.State = StateMasterTbl.StateId LEFT OUTER JOIN " +
                  "  CityMasterTbl ON PartyAutoAllocationManager.City = CityMasterTbl.CityId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=PartyAutoAllocationManager.Whid where PartyAutoAllocationManager.compid='" + compid + "' " + str1 + " " + str2 + " " + str3 + " " + str4 + "";

        string strrrrr = " select count(PartyAutoAllocationManager.PartyAutoAllocationID) as ci " +
                          " FROM  EmployeeMaster RIGHT OUTER JOIN " +
                  "  EmployeeMaster AS EmployeeMaster_4 RIGHT OUTER JOIN " +
                  "  EmployeeMaster AS EmployeeMaster_3 RIGHT OUTER JOIN " +
                  "  PartyAutoAllocationManager ON EmployeeMaster_3.EmployeeMasterID = PartyAutoAllocationManager.AssignedSalesDept ON  " +
                  "  EmployeeMaster_4.EmployeeMasterID = PartyAutoAllocationManager.AssignedPurchseDept ON  " +
                  "  EmployeeMaster.EmployeeMasterID = PartyAutoAllocationManager.AssignedShippingDept LEFT OUTER JOIN" +
                  "  EmployeeMaster AS EmployeeMaster_2 ON PartyAutoAllocationManager.AssignedRecievingDept = EmployeeMaster_2.EmployeeMasterID LEFT OUTER JOIN " +
                  "  EmployeeMaster AS EmployeeMaster_1 ON PartyAutoAllocationManager.AssignedAccountManager = EmployeeMaster_1.EmployeeMasterID LEFT OUTER JOIN " +
                  "  CountryMaster ON PartyAutoAllocationManager.Country = CountryMaster.CountryId LEFT OUTER JOIN " +
                  "  StateMasterTbl ON PartyAutoAllocationManager.State = StateMasterTbl.StateId LEFT OUTER JOIN " +
                  "  CityMasterTbl ON PartyAutoAllocationManager.City = CityMasterTbl.CityId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=PartyAutoAllocationManager.Whid where PartyAutoAllocationManager.compid='" + compid + "' " + str1 + " " + str2 + " " + str3 + " " + str4 + "";

        GridView1.VirtualItemCount = GetRowCount(strrrrr);

        string sortExpression = " WareHouseMaster.Name asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str);

            GridView1.DataSource = dt;
            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

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

        //string orderby = "order by WareHouseMaster.Name,CountryMaster.CountryName,StateMasterTbl.StateName,CityMasterTbl.CityName ";
        //string finalstr = str + str1 + str2 + str3 + str4 + orderby;


        //SqlDataAdapter da = new SqlDataAdapter(finalstr, con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);       
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
        if (e.CommandName == "Delete1")
        {
            ViewState["Id"] = Convert.ToInt32(e.CommandArgument);

            SqlCommand cmd = new SqlCommand("delete  from PartyAutoAllocationManager  where [PartyAutoAllocationID]=" + ViewState["Id"] + " ", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();

            }

            cmd.ExecuteNonQuery();
            con.Close();
            statuslable.Visible = true;
            statuslable.Text = "Record deleted successfully";

            GridView1.SelectedIndex = -1;
            fillgrid();




        }
        if (e.CommandName == "Editgrd")
        {
            int dk1 = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = dk1.ToString();

            string eeed = " select * from PartyAutoAllocationManager  where PartyAutoAllocationID='" + dk1 + "'  ";
            SqlCommand cmdeeed = new SqlCommand(eeed, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);

            fillwarehouse();
            ddlSearchByStore.SelectedIndex = ddlSearchByStore.Items.IndexOf(ddlSearchByStore.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["Whid"]).ToString()));

            fillaccountmanager();
            ddlAssignedAccountManager.SelectedIndex = ddlAssignedAccountManager.Items.IndexOf(ddlAssignedAccountManager.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["AssignedAccountManager"]).ToString()));

            fillreceivedept();
            ddlAssignedRecievingDept.SelectedIndex = ddlAssignedRecievingDept.Items.IndexOf(ddlAssignedRecievingDept.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["AssignedRecievingDept"]).ToString()));

            fillpurchasedept();
            ddlAssignedPurchseDept.SelectedIndex = ddlAssignedPurchseDept.Items.IndexOf(ddlAssignedPurchseDept.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["AssignedPurchseDept"]).ToString()));

            fillshippingdept();
            ddlAssignedShippingDept.SelectedIndex = ddlAssignedShippingDept.Items.IndexOf(ddlAssignedShippingDept.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["AssignedShippingDept"]).ToString()));

            fillsalesdept();
            ddlAssignedSalesDept.SelectedIndex = ddlAssignedSalesDept.Items.IndexOf(ddlAssignedSalesDept.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["AssignedSalesDept"]).ToString()));

            RadioButtonList1.SelectedValue = Convert.ToInt32(dteeed.Rows[0]["AllocationMethod"]).ToString();

            selectedradio();
            ddlselectcountry.SelectedIndex = ddlselectcountry.Items.IndexOf(ddlselectcountry.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["Country"]).ToString()));
            ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["State"]).ToString()));
            ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["City"]).ToString()));



            ImageButton1.Visible = false;
            Button3.Visible = true;

            pnladd.Visible = true;
            lbladdlabel.Visible = true;
            lbladdlabel.Text = "Edit Support Team";
            btnadd.Visible = false;
            statuslable.Visible = true;

        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();

    }


    protected void fillcon()
    {
        ddlcountry1.Items.Clear();

        string str = "  SELECT     CountryId, CountryName FROM     CountryMaster order by CountryName";
        DataTable ds = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        da.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            ddlcountry1.DataSource = ds;
            ddlcountry1.DataTextField = "CountryName";
            ddlcountry1.DataValueField = "CountryId";
            ddlcountry1.DataBind();
            ddlcountry1.Items.Insert(0, "All");
            ddlcountry1.Items[0].Value = "0";
        }
        else
        {
            ddlcountry1.DataSource = null;
            ddlcountry1.DataTextField = "CountryName";
            ddlcountry1.DataValueField = "CountryId";
            ddlcountry1.DataBind();
            ddlcountry1.Items.Insert(0, "All");
            ddlcountry1.Items[0].Value = "0";

        }







    }
    protected void fillcity1()
    {
        dllcity1.Items.Clear();

        string str455 = "SELECT     CityName  ,CityId FROM  CityMasterTbl where StateId='" + dllstate1.SelectedValue + "' Order By CityName ";
        SqlCommand cmd45555 = new SqlCommand(str455, con);
        SqlDataAdapter da5 = new SqlDataAdapter(cmd45555);
        DataTable ds5 = new DataTable();
        da5.Fill(ds5);
        if (ds5.Rows.Count > 0)
        {
            dllcity1.DataSource = ds5;
            dllcity1.DataTextField = "CityName";
            dllcity1.DataValueField = "CityId";
            dllcity1.DataBind();
            dllcity1.Items.Insert(0, "All");
            dllcity1.Items[0].Value = "0";
        }
        else
        {
            dllcity1.DataSource = null;
            dllcity1.DataTextField = "CityName";
            dllcity1.DataValueField = "CityId";
            dllcity1.DataBind();
            dllcity1.Items.Insert(0, "All");
            dllcity1.Items[0].Value = "0";

        }









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

    protected void ddlSearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillaccountmanager();
        fillreceivedept();
        fillpurchasedept();
        fillshippingdept();
        fillsalesdept();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {


        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            lbladdlabel.Visible = true;


        }
        else
        {
            pnladd.Visible = false;
            lbladdlabel.Visible = false;
        }
        btnadd.Visible = false;
        statuslable.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

        btnadd.Visible = true;

        lbladdlabel.Visible = false;
        lbladdlabel.Text = "Add Support Team";
        statuslable.Text = "";
        ImageButton1.Visible = true;
        Button3.Visible = false;
        clear();

    }
    protected void clear()
    {
        ddlAssignedAccountManager.SelectedIndex = -1;
        ddlAssignedRecievingDept.SelectedIndex = -1;
        ddlAssignedPurchseDept.SelectedIndex = -1;
        ddlAssignedShippingDept.SelectedIndex = -1;
        ddlAssignedSalesDept.SelectedIndex = -1;
        RadioButtonList1.SelectedIndex = 0;
        selectedradio();


    }
    protected void ddlcountry1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillstatebyfilter();
        fillcity1();
        fillgrid();



    }
    protected void dllstate1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcity1();
        fillgrid();
    }

    protected void dllcity1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Selected Person In Charge Of";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;


            HeaderGridRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }


    protected void fillaccountmanager()
    {
        ddlAssignedAccountManager.Items.Clear();
        ddlAssignedAccountManager.DataSource = (DataTable)fillemployee();
        ddlAssignedAccountManager.DataValueField = "EmployeeMasterID";
        ddlAssignedAccountManager.DataTextField = "EmployeeName";
        ddlAssignedAccountManager.DataBind();

    }
    protected void fillreceivedept()
    {
        ddlAssignedRecievingDept.Items.Clear();
        ddlAssignedRecievingDept.DataSource = (DataTable)fillemployee();
        ddlAssignedRecievingDept.DataValueField = "EmployeeMasterID";
        ddlAssignedRecievingDept.DataTextField = "EmployeeName";
        ddlAssignedRecievingDept.DataBind();

    }
    protected void fillpurchasedept()
    {
        ddlAssignedPurchseDept.Items.Clear();
        ddlAssignedPurchseDept.DataSource = (DataTable)fillemployee();
        ddlAssignedPurchseDept.DataValueField = "EmployeeMasterID";
        ddlAssignedPurchseDept.DataTextField = "EmployeeName";
        ddlAssignedPurchseDept.DataBind();


    }
    protected void fillshippingdept()
    {
        ddlAssignedShippingDept.Items.Clear();
        ddlAssignedShippingDept.DataSource = (DataTable)fillemployee();
        ddlAssignedShippingDept.DataValueField = "EmployeeMasterID";
        ddlAssignedShippingDept.DataTextField = "EmployeeName";
        ddlAssignedShippingDept.DataBind();

    }
    protected void fillsalesdept()
    {
        ddlAssignedSalesDept.Items.Clear();
        ddlAssignedSalesDept.DataSource = (DataTable)fillemployee();
        ddlAssignedSalesDept.DataValueField = "EmployeeMasterID";
        ddlAssignedSalesDept.DataTextField = "EmployeeName";
        ddlAssignedSalesDept.DataBind();

    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        string str45 = "SELECT  * from PartyAutoAllocationManager where Whid='" + ddlSearchByStore.SelectedValue + "' and AllocationMethod='" + RadioButtonList1.SelectedValue + "' and PartyAutoAllocationID<>'" + ViewState["Id"] + "' ";
        SqlCommand cmd45 = new SqlCommand(str45, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd45);
        DataTable ds = new DataTable();
        da.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "Please fill all mandatory fields";

        }
        else
        {
            if (ddlAssignedAccountManager.SelectedIndex == -1 || ddlAssignedPurchseDept.SelectedIndex == -1 || ddlAssignedSalesDept.SelectedIndex == -1 || ddlAssignedRecievingDept.SelectedIndex == -1 || ddlAssignedShippingDept.SelectedIndex == -1)
            {
                statuslable.Visible = true;
                statuslable.Text = "Please select any employee";
            }
            else
            {

                string strup = "Update PartyAutoAllocationManager set [All]='" + 0 + "',Country='" + ddlselectcountry.SelectedValue + "',State='" + ddlstate.SelectedValue + "',City='" + ddlcity.SelectedValue + "',AssignedAccountManager='" + ddlAssignedAccountManager.SelectedValue + "',AssignedRecievingDept='" + ddlAssignedRecievingDept.SelectedValue + "',AssignedPurchseDept='" + ddlAssignedPurchseDept.SelectedValue + "',AssignedShippingDept='" + ddlAssignedShippingDept.SelectedValue + "', AssignedSalesDept='" + ddlAssignedSalesDept.SelectedValue + "',Whid='" + ddlSearchByStore.SelectedValue + "',AllocationMethod='" + RadioButtonList1.SelectedValue + "' where PartyAutoAllocationID='" + ViewState["Id"] + "' ";
                SqlCommand cmd = new SqlCommand(strup, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }
                cmd.ExecuteNonQuery();
                con.Close();

                statuslable.Visible = true;
                statuslable.Text = "Record updated successfully";


                pnladd.Visible = false;
                btnadd.Visible = true;
                lbladdlabel.Visible = false;
                lbladdlabel.Text = "Add Support Team";
                ImageButton1.Visible = true;
                Button3.Visible = false;
                fillgrid();

                clear();
            }
        }

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillgrid();
    }
    protected void fillcountry()
    {
        ddlselectcountry.Items.Clear();

        string strfillgrid = "SELECT CountryName,CountryId FROM CountryMaster order by CountryName";
        SqlCommand cmdfillgrid = new SqlCommand(strfillgrid, con);
        SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
        DataTable dtfill = new DataTable();
        adpfillgrid.Fill(dtfill);

        if (dtfill.Rows.Count > 0)
        {
            ddlselectcountry.DataSource = dtfill;
            ddlselectcountry.DataValueField = "CountryId";
            ddlselectcountry.DataTextField = "CountryName";
            ddlselectcountry.DataBind();
            ddlselectcountry.Items.Insert(0, "All");
            ddlselectcountry.SelectedItem.Value = "0";
        }
        else
        {
            ddlselectcountry.DataSource = null;
            ddlselectcountry.DataValueField = "CountryId";
            ddlselectcountry.DataTextField = "CountryName";
            ddlselectcountry.DataBind();
            ddlselectcountry.Items.Insert(0, "All");
            ddlselectcountry.SelectedItem.Value = "0";
        }



    }
    protected void fillstate()
    {
        ddlstate.Items.Clear();


        string str45 = "SELECT     StateName  ,StateId  FROM  StateMasterTbl where CountryId='" + ddlselectcountry.SelectedValue + "' Order By StateName";
        SqlCommand cmd45 = new SqlCommand(str45, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd45);
        DataTable ds = new DataTable();
        da.Fill(ds);
        if (ds.Rows.Count > 0)
        {

            ddlstate.DataSource = ds;
            ddlstate.DataTextField = "StateName";
            ddlstate.DataValueField = "StateId";
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, "All");
            ddlstate.SelectedItem.Value = "0";
        }
        else
        {
            ddlstate.DataSource = null;
            ddlstate.DataTextField = "StateName";
            ddlstate.DataValueField = "StateId";
            ddlstate.DataBind();

            ddlstate.Items.Insert(0, "All");
            ddlstate.SelectedItem.Value = "0";

        }

    }

    protected void fillcity()
    {
        ddlcity.Items.Clear();

        string str455 = "SELECT     CityName  ,CityId FROM  CityMasterTbl where StateId='" + ddlstate.SelectedValue + "'   Order By CityName ";
        SqlCommand cmd45555 = new SqlCommand(str455, con);
        SqlDataAdapter da5 = new SqlDataAdapter(cmd45555);

        DataTable ds5 = new DataTable();
        da5.Fill(ds5);

        if (ds5.Rows.Count > 0)
        {

            ddlcity.DataSource = ds5;
            ddlcity.DataTextField = "CityName";
            ddlcity.DataValueField = "CityId";
            ddlcity.DataBind();
            ddlcity.Items.Insert(0, "All");
            ddlcity.SelectedItem.Value = "0";

        }
        else
        {
            ddlcity.DataSource = null;
            ddlcity.DataTextField = "CityName";
            ddlcity.DataValueField = "CityId";
            ddlcity.DataBind();
            ddlcity.Items.Insert(0, "All");
            ddlcity.SelectedItem.Value = "0";
        }
    }
    protected void selectedradio()
    {
        fillcountry();
        fillstate();
        fillcity();


        if (RadioButtonList1.SelectedValue == "1")
        {

            lblCountry.Visible = true;
            ddlselectcountry.Visible = true;
            ddlstate.Visible = true;
            lblstate.Visible = true;
            lblcity.Visible = true;
            ddlcity.Visible = true;
        }

        if (RadioButtonList1.SelectedValue == "2")
        {
            lblCountry.Visible = true;
            ddlselectcountry.Visible = true;
            ddlstate.Visible = true;
            lblstate.Visible = true;
            lblcity.Visible = false;
            ddlcity.Visible = false;
        }
        if (RadioButtonList1.SelectedValue == "3")
        {

            lblCountry.Visible = true;
            ddlselectcountry.Visible = true;
            ddlstate.Visible = false;
            lblstate.Visible = false;
            lblcity.Visible = false;
            ddlcity.Visible = false;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
}
