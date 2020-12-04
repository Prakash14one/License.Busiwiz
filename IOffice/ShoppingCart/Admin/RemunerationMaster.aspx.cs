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

public partial class Add_Remuneration_Master : System.Web.UI.Page
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
            Pagecontrol.dypcontrol(Page, page);
            lblCompany.Text = Session["Cname"].ToString();
            ViewState["sortOrder"] = "";
            Fillwarehouse();
            sh();
            Fillaccount();
            fillgrid();
            lblmsg.Visible = false;
        }

    }
    protected void Fillwarehouse()
    {
        //string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "'and [WareHouseMaster].Status='1' order by Name";
        //SqlCommand cmd = new SqlCommand(str, conn);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        //ddlstrname.DataSource = ds;
        //ddlstrname.DataTextField = "Name";
        //ddlstrname.DataValueField = "WareHouseId";
        //ddlstrname.DataBind();

        //DropDownList1.DataSource = ds;
        //DropDownList1.DataTextField = "Name";
        //DropDownList1.DataValueField = "WareHouseId";
        //DropDownList1.DataBind();
        //DropDownList1.Items.Insert(0, "ALL");

        ddlstrname.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlstrname.DataSource = ds;
        ddlstrname.DataTextField = "Name";
        ddlstrname.DataValueField = "WareHouseId";
        ddlstrname.DataBind();

        //ddlStore.Items.Insert(0, "Select");

        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstrname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");

    }
    protected void Fillaccount()
    {
        //string str1 = " select Id,AccountName,AccountId from AccountMaster where  AccountMaster.Status=1 and  ClassId=13 and AccountMaster.compid = '" + Session["Comid"].ToString() + "' and AccountMaster.Whid='" + ddlstrname.SelectedValue + "'  order by AccountName asc";

        string str1 = "SELECT  AccountMaster.AccountId as Id,GroupCompanyMaster.groupdisplayname,AccountMaster.AccountName as accname, cast(AccountMaster.AccountId as nvarchar) as accid, GroupCompanyMaster.groupdisplayname+':'+cast(AccountMaster.AccountId as nvarchar) +':'+AccountMaster.AccountName as accountname    FROM         AccountMaster LEFT OUTER JOIN  " +
                 "    ClassTypeCompanyMaster RIGHT OUTER JOIN  " +
                "   ClassCompanyMaster ON ClassTypeCompanyMaster.id = ClassCompanyMaster.classtypecompanymasterid RIGHT OUTER JOIN  " +
                       "  GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid ON AccountMaster.GroupId = GroupCompanyMaster.GroupId where AccountMaster.Status=1 and AccountMaster.compid = '" + Session["comid"] + "' " +
                "  and AccountMaster.Whid='" + ddlstrname.SelectedValue + "' and GroupCompanyMaster.whid='" + ddlstrname.SelectedValue + "' and AccountMaster.ClassId=13  order by AccountMaster.Id desc ";

        SqlCommand cmd1 = new SqlCommand(str1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        ddlaccount.DataSource = ds1;
        ddlaccount.DataTextField = "accountname";
        ddlaccount.DataValueField = "Id";
        ddlaccount.DataBind();
        if (ds1.Tables[0].Rows.Count > 0)
        {
            for (Int32 i = 1; i < ds1.Tables[0].Rows.Count; i++)
            {

                String s1 = Convert.ToString(ds1.Tables[0].Rows[i]["groupdisplayname"]);
                String s2 = Convert.ToString(ds1.Tables[0].Rows[i]["accname"]);

                if (s1 == "General and Administrative Expenses")
                {
                    if (s2 == "Office Salary")
                    {
                        ddlaccount.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[i]["Id"]);
                        break;
                    }
                }
            }
        }
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {

        string st1 = "select * from RemunerationMaster where Whid='" + ddlstrname.Text + "' and RemunerationName='" + txtremuneration.Text + "'  ";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, Record already exist ";
        }
        else
        {
            String str = "Insert Into RemunerationMaster (RemunerationName,AccountMasterId,Whid,Compid,Commitiontyperem,IsPensionIncome,IsInsurableIncome,active)values('" + txtremuneration.Text + "','" + ddlaccount.SelectedValue + "','" + ddlstrname.SelectedValue + "','" + Session["comid"] + "','" + chkIscommi.Checked + "','" + chlcanadapension.Checked + "','" + chkcanadainsurable.Checked + "', '" + cb_active.Checked + "')";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
            clearall();
            fillgrid();
        }


    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        clearall();
        if (Button5.Visible == true)
        {
            Button5.Visible = false;
            Btn_Submit.Visible = true;
        }

    }
    protected void clearall()
    {
        // ddlstrname.SelectedIndex = 0;
        // ddlaccount.SelectedIndex = 0;
        txtremuneration.Text = "";
        chkcanadainsurable.Checked = false;
        chlcanadapension.Checked = false;
        chkIscommi.Checked = false;
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbledins.Text = "";
    }
    protected void fillgrid()
    {
        string st1 = "";
        string act = "";
        if (ddlactive.SelectedIndex  > 0)
        {
            act = " And Active='" + ddlactive.SelectedValue + "' ";
        }
        
        if (DropDownList1.SelectedIndex > 0)
        {
            st1 = "select distinct RemunerationMaster.*,AccountMaster.AccountName,AccountMaster.Id as AccountMasterId,WareHouseMaster.Name as WName from  RemunerationMaster inner join AccountMaster on AccountMaster.AccountId=RemunerationMaster.AccountMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=RemunerationMaster.Whid where AccountMaster.Whid='" + DropDownList1.SelectedValue + "' and RemunerationMaster.Compid='" + Session["comid"] + "' and RemunerationMaster.Whid='" + DropDownList1.SelectedValue + "' " + act + " ";
        }
        else
        {
            st1 = "select distinct RemunerationMaster.*,AccountMaster.AccountName,WareHouseMaster.Name as WName from  RemunerationMaster inner join AccountMaster on AccountMaster.AccountId=RemunerationMaster.AccountMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=RemunerationMaster.Whid where RemunerationMaster.Compid='" + Session["comid"] + "' " + act + "  and AccountMaster.Whid in(Select Distinct WareHouseId from WareHouseMaster where comid='" + Session["comid"] + "' and Status='1')";
        }
        lblBusiness.Text ="Business :"+ DropDownList1.SelectedItem.Text;
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

        DataView myDataView = new DataView();
        myDataView = dt1.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataSource = myDataView;
        GridView1.DataBind();

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblmsg.Text = "";
        int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        ViewState["id"] = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        string str12 = "select * from RemunerationMaster where Id='" + dk1 + "'";
        SqlCommand cmd = new SqlCommand(str12, conn);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        adpt.Fill(dt1);
        pnladd.Visible = true;
        btnadd.Visible = false;
        lbledins.Text = "Edit Remuneration";
       
        ddlstrname.SelectedIndex = ddlstrname.Items.IndexOf(ddlstrname.Items.FindByValue(dt1.Rows[0]["Whid"].ToString()));

        txtremuneration.Text = dt1.Rows[0]["RemunerationName"].ToString();
        sh();
        Fillaccount();
        ddlaccount.SelectedIndex = ddlaccount.Items.IndexOf(ddlaccount.Items.FindByValue(dt1.Rows[0]["AccountMasterId"].ToString()));

        if (Convert.ToString(dt1.Rows[0]["IsInsurableIncome"]) != "")
        {
            chkcanadainsurable.Checked = Convert.ToBoolean(dt1.Rows[0]["IsInsurableIncome"]);
        }
        else
        {
            chkcanadainsurable.Checked = false;
        }
        if (Convert.ToString(dt1.Rows[0]["IsPensionIncome"]) != "")
        {
            chlcanadapension.Checked = Convert.ToBoolean(dt1.Rows[0]["IsPensionIncome"]);
        }
        else
        {
            chlcanadapension.Checked = false;
        }
        if (Convert.ToString(dt1.Rows[0]["Commitiontyperem"]) != "")
        {
            chkIscommi.Checked = Convert.ToBoolean(dt1.Rows[0]["Commitiontyperem"]);
        }
        else
        {
            chkIscommi.Checked = false;
        }
        if (Convert.ToString(dt1.Rows[0]["Active"]) != "")
        {
            cb_active.Checked = Convert.ToBoolean(dt1.Rows[0]["Active"]);
        }
        else
        {
            cb_active.Checked = false;
        }

        Button5.Visible = true;
        Btn_Submit.Visible = false;



    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st1 = " select * from EmployeeSalaryMaster where Remuneration_Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' or IsPercentRemunerationId='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {


            lblmsg.Visible = true;
            lblmsg.Text = "You cannot delete this record as it is linked to a payroll deduction for your salary.";
        }
        else
        {
            string stdesign = " select * from RemunerationByDesignation where Remuneration_Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' or IsPercentRemunerationId='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
            SqlCommand cmddesign = new SqlCommand(stdesign, conn);
            SqlDataAdapter adpdesign = new SqlDataAdapter(cmddesign);
            DataTable dtdesign = new DataTable();
            adpdesign.Fill(dtdesign);
            if (dtdesign.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You cannot delete this record as it is linked to a payroll deduction for your salary.";
            }
            else
            {
                string st2 = "Delete from RemunerationMaster where Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
                SqlCommand cmd2 = new SqlCommand(st2, conn);
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }

                cmd2.ExecuteNonQuery();
                conn.Close();
                GridView1.EditIndex = -1;
                fillgrid();

                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully";
            }


        }


    }


    protected void ddlstrname_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillaccount();
        sh();
    }
    protected void sh()
    {
        pnlcan.Visible = false;
        string str = "Select CountryName  from CountryMaster inner join  CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId where CompanyWebsitMaster.Whid='" + ddlstrname.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        da.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            if (Convert.ToString(ds.Rows[0]["CountryName"]).ToUpper() == "CANADA")
            {
                pnlcan.Visible = true;
            }
        }
        else
        {
            string str1 = "Select CountryName from CompanyMaster inner join CompanyAddressMaster on CompanyAddressMaster.CompanyMasterId=CompanyMaster.CompanyId inner join CountryMaster on CountryMaster.CountryId=CompanyAddressMaster.Country   where CompanyMaster.Compid='" + Session["Comid"] + "'";
            SqlCommand cmd1 = new SqlCommand(str1, conn);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            if (ds1.Rows.Count > 0)
            {
                if (Convert.ToString(ds1.Rows[0]["CountryName"]).ToUpper() == "CANADA")
                {
                    pnlcan.Visible = true;
                }
            }
        }
    }


    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        fillgrid();
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "AccountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlstrname.SelectedIndex > 0)
        {
            Fillaccount();
        }
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {

        if (btncancel0.Text == "Printable Version")
        {
            btncancel0.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["deleteh"] = "tt";
                GridView1.Columns[5].Visible = false;
            }

        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["edith"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["deleteh"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }

        }
    }
    protected void fillstore()
    {
        ddlstrname.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlstrname.DataSource = ds;
        ddlstrname.DataTextField = "Name";
        ddlstrname.DataValueField = "WareHouseId";
        ddlstrname.DataBind();
        //ddlStore.Items.Insert(0, "Select");

        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstrname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {


    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            string str1 = "select * from RemunerationMaster where Whid='" + ddlstrname.SelectedValue + "' and RemunerationName='" + txtremuneration.Text + "' and Id<>'" + ViewState["id"] + "'  ";

            SqlCommand cmd1 = new SqlCommand(str1, conn);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Record already exist";
            }

            else
            {


                string sr51 = ("update RemunerationMaster set RemunerationName='" + txtremuneration.Text + "',Whid='" + ddlstrname.SelectedValue + "',AccountMasterId='" + ddlaccount.SelectedValue + "',Commitiontyperem='" + chkIscommi.Checked + "',IsPensionIncome='" + chlcanadapension.Checked + "',IsInsurableIncome='" + chkcanadainsurable.Checked + "',Active='"+ cb_active.Checked  +"' where Id='" + ViewState["id"] + "' ");
                SqlCommand cmd801 = new SqlCommand(sr51, conn);

                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                cmd801.ExecuteNonQuery();
                conn.Close();
                GridView1.EditIndex = -1;
                fillgrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record update successfully";
                Btn_Submit.Visible = true;
                Button5.Visible = false;
                clearall();
            }
        }
        catch
        {
            lblmsg.Text = "invalid";
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {


    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        btnadd.Visible = false;
        lbledins.Text = "Add New Remuneration";
        lblmsg.Text = "";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
}