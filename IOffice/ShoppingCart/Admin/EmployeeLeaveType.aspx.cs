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

public partial class Add_Employee_Leave_Type : System.Web.UI.Page
{
    SqlConnection conn;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";

            lblCompany.Text = Session["Cname"].ToString();
            lblmsg.Text = "";


            fillstore();

            Fillgridemployeeleave();



            ImageButton48.Visible = false;
            ImageButton2.Visible = true;
            // lblmsg.Text = "";
            ddlstorename_SelectedIndexChanged(sender, e);

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


    protected void Fillgridemployeeleave()
    {

        string str = "";

        string s3 = "";

        if (DropDownList1.SelectedIndex > 0)
        {
            s3 = "Case EmployeeLeaveType.Ispaidleave When '0' then 'No' When '1' then 'Yes' end as Ispaidleave, EmployeeLeaveType.EmployeeLeaveTypeName, EmployeeLeaveType.ID,WareHouseMaster.WareHouseId as WId,WareHouseMaster.Name as WName from EmployeeLeaveType INNER JOIN WareHouseMaster on WareHouseMaster.WareHouseId=EmployeeLeaveType.Whid  where compid='" + Session["Comid"].ToString() + "' and EmployeeLeaveType.Whid='" + DropDownList1.SelectedValue + "'";
            //order by WName, EmployeeLeaveTypeName";
            str = "select count(EmployeeLeaveType.Id) as ci from EmployeeLeaveType INNER JOIN WareHouseMaster on WareHouseMaster.WareHouseId=EmployeeLeaveType.Whid  where compid='" + Session["Comid"].ToString() + "' and EmployeeLeaveType.Whid='" + DropDownList1.SelectedValue + "'";
        }
        else
        {
            s3 = "Case EmployeeLeaveType.Ispaidleave When '0' then 'No' When '1' then 'Yes' end as Ispaidleave, EmployeeLeaveType.EmployeeLeaveTypeName, EmployeeLeaveType.ID,WareHouseMaster.WareHouseId as WId,WareHouseMaster.Name as WName from EmployeeLeaveType INNER JOIN WareHouseMaster on WareHouseMaster.WareHouseId=EmployeeLeaveType.Whid  where compid='" + Session["Comid"].ToString() + "'";
            //order by WName, EmployeeLeaveTypeName ";
            str = "select count(EmployeeLeaveType.Id) as ci from EmployeeLeaveType INNER JOIN WareHouseMaster on WareHouseMaster.WareHouseId=EmployeeLeaveType.Whid  where compid='" + Session["Comid"].ToString() + "'";
        }
        lblBusiness.Text = DropDownList1.SelectedItem.Text;

        //SqlCommand cmd = new SqlCommand(str, conn);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        GridView1.VirtualItemCount = GetRowCount(str);

        string sortExpression = "WarehouseMaster.Name,EmployeeLeaveTypeName asc";


        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt1 = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, s3);

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

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblmsg.Text = "";
        GridView1.EditIndex = e.NewEditIndex;
        pnladd.Visible = true;
        btnadd.Visible = false;
        lbladd.Text = "Edit Leave Type";
        Fillgridemployeeleave();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        lblmsg.Text = "";
        GridView1.EditIndex = -1;
        Fillgridemployeeleave();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st1 = "select * from PolicyEmployeeLeave where LeaveTypeId='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists in policy employee leave";
        }
        else
        {
            SqlDataAdapter adpt = new SqlDataAdapter("select * from EmployeeHoliday where leavetypeid = '" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'", conn);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You can not delete this leave type, because this leave type is already used by some or more employees for their leave request.";
            }
            else
            {
                lblmsg.Text = "";
                string st2 = "Delete from EmployeeLeaveType where EmployeeLeaveType.ID='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
                SqlCommand cmd2 = new SqlCommand(st2, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmd2.ExecuteNonQuery();
                conn.Close();
                GridView1.EditIndex = -1;
                Fillgridemployeeleave();
                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully ";
            }
            //cleartxt();
        }
    }
    //protected void LinkButton4_Click(object sender, EventArgs e)
    //{
    //    lblmsg.Text = "";
    //    LinkButton lk = (LinkButton)sender;
    //    int j = Convert.ToInt32(lk.CommandArgument);
    //    ViewState["Id"] = j;
    //    Session["TimeId"] = j;
    //    string str = "SELECT * from EmployeeLeaveType where ID='" + j + "'  ";
    //    SqlCommand cmd = new SqlCommand(str, conn);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    ddlstorename.SelectedValue = dt.Rows[0]["Whid"].ToString();
    //    txtleavetypename.Text = dt.Rows[0]["EmployeeLeaveTypeName"].ToString();
    //    ImageButton48.Visible = true;
    //    ImageButton2.Visible = false;



    //}
    //protected void ImageButton48_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    //protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void cleartxt()
    {
        lblmsg.Text = "";
        ddlstorename.SelectedIndex = 0;
        txtleavetypename.Text = "";
        ImageButton48.Visible = false;
        ImageButton2.Visible = true;
        txtmoreemp.Text = "0";
        txtleaveno.Text = "0";
        rdpais.SelectedValue = "0";
        pnlpaid.Visible = false;
        rdencashable.Visible = false;
        lblisenca.Visible = false;
        lblisencaemp.Visible = false;
    }
    protected void ImageButton2_Click1(object sender, EventArgs e)
    {
        string st1 = "";
        if (rdpais.SelectedValue == "1")
        {
            st1 = "select * from EmployeeLeaveType where Whid='" + ddlstorename.SelectedValue + "'and EmployeeLeaveTypeName='" + txtleavetypename.Text + "' and DesignationId='" + ddldesi.SelectedValue + "'";

        }
        else
        {
            st1 = "select * from EmployeeLeaveType where Whid='" + ddlstorename.SelectedValue + "'and EmployeeLeaveTypeName='" + txtleavetypename.Text + "'";

        }
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        adp1.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";

        }
        else
        {
            string str = "";
            if (rdpais.SelectedIndex == 0)
            {
                if (pnlpaid.Visible != true)
                {
                    pnlpaid.Visible = true;
                }
                else
                {
                    str = "Insert  Into EmployeeLeaveType (Whid,EmployeeLeaveTypeName,compid,Ispaidleave,Leaveno,pertype,DesignationId,Moreday,Leaveencashable)Values('" + ddlstorename.SelectedValue + "','" + txtleavetypename.Text + "','" + Session["Comid"].ToString() + "','" + rdpais.SelectedValue + "','" + txtleaveno.Text + "','" + ddlpertype.SelectedValue + "','" + ddldesi.SelectedValue + "','" + txtmoreemp.Text + "','" + rdencashable.SelectedValue + "')";

                    SqlCommand cmd = new SqlCommand(str, conn);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully";
                    ddlstorename.SelectedIndex = 0;
                    txtleavetypename.Text = "";
                    txtmoreemp.Text = "0";
                    txtleaveno.Text = "0";
                    rdpais.SelectedValue = "0";
                    pnlpaid.Visible = false;
                    rdencashable.Visible = false;
                    lblisenca.Visible = false;
                    lblisencaemp.Visible = false;
                    pnladd.Visible = false;
                    btnadd.Visible = true;
                    lbladd.Text = "";
                }
            }
            else
            {
                str = "Insert  Into EmployeeLeaveType (Whid,EmployeeLeaveTypeName,compid,Ispaidleave,Leaveno,pertype,DesignationId,Moreday,Leaveencashable)Values('" + ddlstorename.SelectedValue + "','" + txtleavetypename.Text + "','" + Session["Comid"].ToString() + "','" + rdpais.SelectedValue + "','" + txtleaveno.Text + "','0','" + ddldesi.SelectedValue + "','" + txtmoreemp.Text + "','0')";
                SqlCommand cmd = new SqlCommand(str, conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                ddlstorename.SelectedIndex = 0;
                txtleavetypename.Text = "";
                txtmoreemp.Text = "0";
                txtleaveno.Text = "0";
                rdpais.SelectedValue = "0";
                pnlpaid.Visible = false;
                rdencashable.Visible = false;
                lblisenca.Visible = false;
                lblisencaemp.Visible = false;
                pnladd.Visible = false;
                btnadd.Visible = true;
                lbladd.Text = "";
            }

        }
        Fillgridemployeeleave();


    }
    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        cleartxt();
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";
        lblmsg.Text = "";
    }
    protected void ImageButton48_Click(object sender, EventArgs e)
    {
        string st1 = "";
        if (rdpais.SelectedValue == "1")
        {
            st1 = "select * from EmployeeLeaveType where Whid='" + ddlstorename.SelectedValue + "'and EmployeeLeaveTypeName='" + txtleavetypename.Text + "' and DesignationId='" + ddldesi.SelectedValue + "' and ID<>'" + ViewState["Id"] + "'";

        }
        else
        {
            st1 = "select * from EmployeeLeaveType where Whid='" + ddlstorename.SelectedValue + "'and EmployeeLeaveTypeName='" + txtleavetypename.Text + "' and ID<>'" + ViewState["Id"] + "'";

        }
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        adp1.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";

        }
        else
        {
            string str = "";
            if (rdpais.SelectedIndex == 0)
            {
                if (pnlpaid.Visible != true)
                {
                    pnlpaid.Visible = true;
                }
                else
                {
                    str = "UPDATE EmployeeLeaveType set Whid='" + ddlstorename.SelectedValue + "',EmployeeLeaveTypeName='" + txtleavetypename.Text + "',Ispaidleave='" + rdpais.SelectedValue + "',Leaveno='" + txtleaveno.Text + "',pertype='" + ddlpertype.SelectedValue + "',DesignationId='" + ddldesi.SelectedValue + "',Moreday='" + txtmoreemp.Text + "',Leaveencashable='" + rdencashable.SelectedValue + "' WHERE ID='" + ViewState["Id"] + "'";
                }
            }
            else
            {
                str = "UPDATE EmployeeLeaveType set Whid='" + ddlstorename.SelectedValue + "',EmployeeLeaveTypeName='" + txtleavetypename.Text + "',Ispaidleave='" + rdpais.SelectedValue + "',Leaveno='" + txtleaveno.Text + "',pertype='0',DesignationId='" + ddldesi.SelectedValue + "',Moreday='" + txtmoreemp.Text + "',Leaveencashable='0' WHERE ID='" + ViewState["Id"] + "'";

            }
            SqlCommand cmd = new SqlCommand(str, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmd.ExecuteNonQuery();
            conn.Close();
            GridView1.EditIndex = -1;
            Fillgridemployeeleave();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully ";
            ddlstorename.SelectedIndex = 0;
            txtleavetypename.Text = "";

            ImageButton48.Visible = false;
            ImageButton2.Visible = true;
            txtmoreemp.Text = "0";
            txtleaveno.Text = "0";
            rdpais.SelectedValue = "0";
            pnlpaid.Visible = false;
            rdencashable.Visible = false;
            lblisenca.Visible = false;
            lblisencaemp.Visible = false;
            pnladd.Visible = false;
            btnadd.Visible = true;
            lbladd.Text = "";

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
        hdnsortDir.Value = sortOrder; // sortOrder;
        Fillgridemployeeleave();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgridemployeeleave();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            Fillgridemployeeleave();

            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }

        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(150);

            Button1.Text = "Printable Version";
            Button7.Visible = false;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            Fillgridemployeeleave();

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lblmsg.Text = "";
            //LinkButton lk = (LinkButton)sender;
            int j = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = j;
            Session["TimeId"] = j;
            string str = "SELECT * from EmployeeLeaveType where ID='" + j + "'  ";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlstorename.SelectedValue = dt.Rows[0]["Whid"].ToString();
                txtleavetypename.Text = dt.Rows[0]["EmployeeLeaveTypeName"].ToString();
                ImageButton48.Visible = true;
                ImageButton2.Visible = false;
                if (Convert.ToString(dt.Rows[0]["Ispaidleave"]) == "True")
                {
                    rdpais.SelectedValue = "1";
                    txtleaveno.Text = Convert.ToString(dt.Rows[0]["Leaveno"]);
                    txtmoreemp.Text = Convert.ToString(dt.Rows[0]["Moreday"]);
                    FillDept();
                    ddlpertype.SelectedIndex = ddlpertype.Items.IndexOf(ddlpertype.Items.FindByValue(Convert.ToString(dt.Rows[0]["pertype"])));
                    ddldesi.SelectedIndex = ddldesi.Items.IndexOf(ddldesi.Items.FindByValue(Convert.ToString(dt.Rows[0]["DesignationId"])));
                    rdencashable.SelectedValue = "1";
                    pnlpaid.Visible = true;
                    lblisenca.Visible = true;

                    rdencashable.Visible = true;
                    lblisencaemp.Visible = true;
                }
                else
                {
                    rdpais.SelectedValue = "0";
                    rdencashable.SelectedValue = "0";
                    pnlpaid.Visible = false;
                    lblisenca.Visible = false;
                    rdencashable.Visible = false;
                    lblisencaemp.Visible = false;
                }
            }
        }
    }
    protected void fillstore()
    {
        ddlstorename.Items.Clear();

        DataTable ds = ClsStore.SelectStorename();
        ddlstorename.DataSource = ds;
        ddlstorename.DataTextField = "Name";
        ddlstorename.DataValueField = "WareHouseId";
        ddlstorename.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstorename.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";

    }
    protected void rdpais_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdpais.SelectedIndex == 0)
        {
            pnlpaid.Visible = true;
            lblisenca.Visible = true;
            rdencashable.Visible = true;
            lblisencaemp.Visible = true;
            FillDept();

        }
        else
        {
            pnlpaid.Visible = false;
            lblisenca.Visible = false;
            rdencashable.Visible = false;
            lblisencaemp.Visible = false;
        }
    }
    protected void ddlstorename_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pnlpaid.Visible == true)
        {
            FillDept();
        }
    }
    protected void FillDept()
    {

        ddldesi.Items.Clear();
        DataTable ds = select("Select DesignationMaster.DesignationMasterId,DesignationMaster.DesignationName as desname from DesignationMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptId where DepartmentmasterMNC.Whid='" + ddlstorename.SelectedValue + "' order by desname");
        if (ds.Rows.Count > 0)
        {
            ddldesi.DataSource = ds;
            ddldesi.DataTextField = "desname";
            ddldesi.DataValueField = "DesignationMasterId";
            ddldesi.DataBind();
        }
        ddldesi.Items.Insert(0, "All");
        ddldesi.Items[0].Value = "0";


    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Fillgridemployeeleave();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        btnadd.Visible = false;
        lbladd.Text = "Add Leave Type";
        lblmsg.Text = "";
    }
}