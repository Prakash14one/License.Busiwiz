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
using System.IO;
using System.Text;
using System.Data.Common;
using System.Drawing;
using System.ServiceProcess;
using System.Diagnostics;
using System.Windows;


public partial class ListOfBusinessess : System.Web.UI.Page
{
    Class1 n = new Class1();

    //SqlConnection con;
    SqlConnection con = new SqlConnection(PageConn.connnn);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //PageConn pgcon = new PageConn();
            //con = pgcon.dynconn;

            if (Convert.ToInt32(Request.QueryString["subid"]) != 0)
            {
                int m = Convert.ToInt32(Request.QueryString["subid"]);

                ViewState["sub"] = " and BusinessSubCat.B_SubCatID='" + m + "'";

                DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
                string thismonthstartdate = thismonthstart.ToShortDateString();
                ViewState["thismonthstartdate"] = thismonthstartdate;

                txtestartdate.Text = ViewState["thismonthstartdate"].ToString();
                txteenddate.Text = System.DateTime.Now.ToShortDateString();

                fillemployee();
                fillddlcountry();
                fillcategory();
                fillsubcategory();
                fillsubsubcategory();
                fillgrid();
            }

            else if (Convert.ToInt32(Request.QueryString["id"]) != 0)
            {
                int m1 = Convert.ToInt32(Request.QueryString["id"]);

                ViewState["id"] = " and BusinessCategory.B_CatID='" + m1 + "'";

                DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
                string thismonthstartdate = thismonthstart.ToShortDateString();
                ViewState["thismonthstartdate"] = thismonthstartdate;

                txtestartdate.Text = ViewState["thismonthstartdate"].ToString();
                txteenddate.Text = System.DateTime.Now.ToShortDateString();

                fillemployee();
                fillddlcountry();
                fillcategory();
                fillsubcategory();
                fillsubsubcategory();
                fillgrid();
            }
            else if (Convert.ToInt32(Request.QueryString["subsubid"]) != 0)
            {
                int m11 = Convert.ToInt32(Request.QueryString["subsubid"]);

                ViewState["subsubid"] = " and BusinessTempMaster.subcatID='" + m11 + "'";

                DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
                string thismonthstartdate = thismonthstart.ToShortDateString();
                ViewState["thismonthstartdate"] = thismonthstartdate;

                txtestartdate.Text = ViewState["thismonthstartdate"].ToString();
                txteenddate.Text = System.DateTime.Now.ToShortDateString();

                fillemployee();
                fillddlcountry();
                fillcategory();
                fillsubcategory();
                fillsubsubcategory();
                fillgrid();
            }

            else if (Convert.ToInt32(Request.QueryString["busid"]) != 0)
            {
                int m11 = Convert.ToInt32(Request.QueryString["busid"]);

                ViewState["busid"] = " and BusinessTempMaster.ID='" + m11 + "'";

                DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
                string thismonthstartdate = thismonthstart.ToShortDateString();
                ViewState["thismonthstartdate"] = thismonthstartdate;

                txtestartdate.Text = ViewState["thismonthstartdate"].ToString();
                txteenddate.Text = System.DateTime.Now.ToShortDateString();

                fillemployee();
                fillddlcountry();
                fillcategory();
                fillsubcategory();
                fillsubsubcategory();
                fillgrid();
            }

            else
            {
                DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
                string thismonthstartdate = thismonthstart.ToShortDateString();
                ViewState["thismonthstartdate"] = thismonthstartdate;

                txtestartdate.Text = ViewState["thismonthstartdate"].ToString();
                txteenddate.Text = System.DateTime.Now.ToShortDateString();

                fillemployee();
                fillddlcountry();
                fillcategory();
                fillsubcategory();
                fillsubsubcategory();
                fillgrid();
            }
        }
    }

    protected void fillemployee()
    {
        ddlemployee.Items.Clear();

        SqlDataAdapter da = new SqlDataAdapter("select warehouseid from warehousemaster inner join employeemaster on warehousemaster.warehouseid=employeemaster.whid where employeemaster.employeemasterid='" + Session["EmployeeID"] + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string strsc1 = "SELECT Employeename,Employeemasterid from Employeemaster where whid='" + dt.Rows[0]["warehouseid"].ToString() + "' order by Employeename";
        SqlCommand cmdddlsc1 = new SqlCommand(strsc1, con);
        SqlDataAdapter dasc1 = new SqlDataAdapter(cmdddlsc1);
        DataTable dtsc1 = new DataTable();
        dasc1.Fill(dtsc1);

        if (dtsc1.Rows.Count > 0)
        {
            ddlemployee.DataSource = dtsc1;
            ddlemployee.DataTextField = "Employeename";
            ddlemployee.DataValueField = "Employeemasterid";
            ddlemployee.DataBind();
        }
        ddlemployee.Items.Insert(0, "-Select-");
        ddlemployee.Items[0].Value = "0";
    }

    protected void fillsubsubcategory()
    {
        filtersubsub.Items.Clear();

        SqlCommand cmd = new SqlCommand("selectsubsubcategory", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@B_SubCatID", filterSub.SelectedValue);

        DataTable dtsc1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtsc1);
        //string strsc1 = "SELECT B_SubSubCategory, B_SubSubCatID From BusinessSubSubCat where Active='1' and B_SubCatID='" + filterSub.SelectedValue + "'";
        //SqlCommand cmdddlsc1 = new SqlCommand(strsc1, con);
        //SqlDataAdapter dasc1 = new SqlDataAdapter(cmdddlsc1);
        //DataTable dtsc1 = new DataTable();
        //dasc1.Fill(dtsc1);

        if (dtsc1.Rows.Count > 0)
        {
            filtersubsub.DataSource = dtsc1;
            filtersubsub.DataTextField = "B_SubSubCategory";
            filtersubsub.DataValueField = "B_SubSubCatID";
            filtersubsub.DataBind();
        }
        filtersubsub.Items.Insert(0, "-Select-");
        filtersubsub.Items[0].Value = "0";
    }

    protected void fillsubcategory()
    {
        filterSub.Items.Clear();

        SqlCommand cmd = new SqlCommand("selectsubcategory", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@B_CatID", filterCat.SelectedValue);

        DataTable dtsc = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtsc);
        //string strsc = "SELECT B_SubCategory, B_SubCatID From BusinessSubCat where Active='1' and B_CatID='" + filterCat.SelectedValue + "'";
        //SqlCommand cmdddlsc = new SqlCommand(strsc, con);
        //SqlDataAdapter dasc = new SqlDataAdapter(cmdddlsc);
        //DataTable dtsc = new DataTable();
        //dasc.Fill(dtsc);
        if (dtsc.Rows.Count > 0)
        {
            filterSub.DataSource = dtsc;
            filterSub.DataTextField = "B_SubCategory";
            filterSub.DataValueField = "B_SubCatID";
            filterSub.DataBind();
        }
        filterSub.Items.Insert(0, "-Select-");
        filterSub.Items[0].Value = "0";
    }

    protected void fillcategory()
    {
        filterCat.Items.Clear();

        SqlCommand cmd = new SqlCommand("selectcategory", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //SqlCommand cmd = new SqlCommand("Select * from StateMasterTbl where CountryId='" + filtercountry.SelectedValue + "' order by StateName", con);
        //SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dtc = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtc);

        //string strc = "SELECT B_Category, B_CatID From BusinessCategory where Active='1'";
        //SqlCommand cmdddlc = new SqlCommand(strc, con);
        //SqlDataAdapter dac = new SqlDataAdapter(cmdddlc);
        //DataTable dtc = new DataTable();
        //dac.Fill(dtc);

        if (dtc.Rows.Count > 0)
        {
            filterCat.DataSource = dtc;
            filterCat.DataTextField = "B_Category";
            filterCat.DataValueField = "B_CatID";
            filterCat.DataBind();
        }
        filterCat.Items.Insert(0, "-Select-");
        filterCat.Items[0].Value = "0";
    }

    protected void fillddlstate()
    {
        filterState.Items.Clear();
        filterCity.Items.Clear();

        SqlCommand cmd = new SqlCommand("selectStateMasterTbl", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CountryId", filtercountry.SelectedValue);

        //SqlCommand cmd = new SqlCommand("Select * from StateMasterTbl where CountryId='" + filtercountry.SelectedValue + "' order by StateName", con);
        //SqlDataAdapter dtp = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            filterState.DataSource = dt;
            filterState.DataTextField = "StateName";
            filterState.DataValueField = "StateId";
            filterState.DataBind();
            // ddlstate.Items.Insert(0, "--Select--");
        }
        filterState.Items.Insert(0, "-Select-");
        filterState.Items[0].Value = "0";
        filterCity.Items.Insert(0, "-Select-");
        filterCity.Items[0].Value = "0";
    }

    protected void fillddlcity()
    {
        filterCity.Items.Clear();

        SqlCommand cmd = new SqlCommand("selectCityMasterTbl", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StateId", filterState.SelectedValue);

        //SqlCommand cmd = new SqlCommand("Select * from CityMasterTbl where StateId='" + filterState.SelectedValue + "' order by CityName", con);
        //SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //dtp.Fill(dt);

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            filterCity.DataSource = dt;
            filterCity.DataTextField = "CityName";
            filterCity.DataValueField = "CityId";
            filterCity.DataBind();
            // ddlcity.Items.Insert(0, "--Select--");
        }

        filterCity.Items.Insert(0, "-Select-");
        filterCity.Items[0].Value = "0";
    }

    protected void fillddlcountry()
    {
        SqlCommand cmd = new SqlCommand("selectCountryMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;

        //SqlCommand cmd = new SqlCommand("Select distinct CountryId,CountryName from CountryMaster order by CountryName", con);
        //SqlDataAdapter dtp = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            filtercountry.DataSource = dt;
            filtercountry.DataTextField = "CountryName";
            filtercountry.DataValueField = "CountryId";
            filtercountry.DataBind();
        }

        filtercountry.Items.Insert(0, "-Select-");
        filtercountry.Items[0].Value = "0";
        filterState.Items.Insert(0, "-Select-");
        filterState.Items[0].Value = "0";
        filterCity.Items.Insert(0, "-Select-");
        filterCity.Items[0].Value = "0";
    }

    protected void filterCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillsubcategory();
        lblmsg.Text = "";
    }

    protected void filtercountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlstate();
        lblmsg.Text = "";
    }
    protected void filterState_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlcity();
        lblmsg.Text = "";
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        fillgrid();
        lblmsg.Text = "";
    }

    protected void fillgrid()
    {
        string st1 = "";

        string st2 = "";

        if (Convert.ToInt32(Request.QueryString["subid"]) != 0)
        {
            st2 = ViewState["sub"].ToString();
        }

        if (Convert.ToInt32(Request.QueryString["id"]) != 0)
        {
            st2 = ViewState["id"].ToString();
        }

        if (Convert.ToInt32(Request.QueryString["subsubid"]) != 0)
        {
            st2 = ViewState["subsubid"].ToString();
        }
        if (Convert.ToInt32(Request.QueryString["busid"]) != 0)
        {
            st2 = ViewState["busid"].ToString();
        }
        if (txtestartdate.Text != "" && txteenddate.Text != "")
        {
            st1 += " and cast(UserLoginLog.DateTime as date) >= '" + txtestartdate.Text + "' and cast(UserLoginLog.DateTime as date) <= '" + txteenddate.Text + "'";
        }
        if (ddlemployee.SelectedIndex > 0)
        {
            st1 += " and UserLoginLog.employeeid='" + ddlemployee.SelectedValue + "'";
        }

        if (filtercountry.SelectedIndex > 0)
        {
            st1 += " and BusinessTempMaster.country='" + filtercountry.SelectedValue + "'";
        }

        if (filterState.SelectedIndex > 0)
        {
            st1 += " and BusinessTempMaster.state='" + filterState.SelectedValue + "'";
        }

        if (filterCity.SelectedIndex > 0)
        {
            st1 += " and BusinessTempMaster.city='" + filterCity.SelectedValue + "'";
        }

        if (filterCat.SelectedIndex > 0)
        {
            st1 += " and BusinessCategory.B_CatID='" + filterCat.SelectedValue + "'";
        }

        if (filterSub.SelectedIndex > 0)
        {
            st1 += " and BusinessSubCat.B_SubCatID='" + filterSub.SelectedValue + "'";
        }

        if (filtersubsub.SelectedIndex > 0)
        {
            st1 += " and BusinessTempMaster.subcatID='" + filtersubsub.SelectedValue + "'";
        }

        //if (ddlstatus_search.SelectedValue != "2")
        //{
        //    st1 += " and BusinessTempMaster.status='" + ddlstatus_search.SelectedValue + "'";
        //}

        if (txtSearch.Text != "")
        {
            st1 += " and ((BusinessTempMaster.BusinessName like '%" + txtSearch.Text.Replace("'", "''") + "%') or (BusinessCategory.B_Category like '%" + txtSearch.Text.Replace("'", "''") + "%') or (BusinessSubCat.B_SubCategory like '%" + txtSearch.Text.Replace("'", "''") + "%') or (BusinessSubSubCat.B_SubSubCategory like '%" + txtSearch.Text.Replace("'", "''") + "%') or (CityMasterTbl.CityName like '%" + txtSearch.Text.Replace("'", "''") + "%') or (StateMasterTbl.StateName like '%" + txtSearch.Text.Replace("'", "''") + "%') or (CountryMaster.CountryName like '%" + txtSearch.Text.Replace("'", "''") + "%') or (BusinessTempMaster.phone like '%" + txtSearch.Text.Replace("'", "''") + "%') or (BusinessTempMaster.email like '%" + txtSearch.Text.Replace("'", "''") + "%'))";
        }
        string str = " BusinessTempMaster.ID,BusinessTempMaster.BusinessName,case when(BusinessTempMaster.status ='1') then 'Active' else 'Inactive' end as Status,BusinessCategory.B_Category,BusinessSubCat.B_SubCategory,BusinessSubSubCat.B_SubSubCategory,BusinessTempMaster.phone,BusinessTempMaster.email,CityMasterTbl.CityName,StateMasterTbl.StateName,CountryMaster.CountryName from BusinessTempMaster inner join BusinessSubSubCat on BusinessSubSubCat.B_SubSubCatID=BusinessTempMaster.subcatID inner join BusinessSubCat on BusinessSubCat.B_SubCatID=BusinessSubSubCat.B_SubCatID inner join BusinessCategory on BusinessCategory.B_CatID=BusinessSubCat.B_CatID inner join CityMasterTbl on CityMasterTbl.CityId=BusinessTempMaster.city inner join StateMasterTbl on StateMasterTbl.StateId=BusinessTempMaster.state inner join CountryMaster on CountryMaster.CountryId=BusinessTempMaster.country inner join UserLoginLog on UserLoginLog.businessid=BusinessTempMaster.ID where BusinessTempMaster.status='1' " + st1 + " " + st2 + "";

        string str2 = "select Count(BusinessTempMaster.ID) as ci from BusinessTempMaster inner join BusinessSubSubCat on BusinessSubSubCat.B_SubSubCatID=BusinessTempMaster.subcatID inner join BusinessSubCat on BusinessSubCat.B_SubCatID=BusinessSubSubCat.B_SubCatID inner join BusinessCategory on BusinessCategory.B_CatID=BusinessSubCat.B_CatID inner join CityMasterTbl on CityMasterTbl.CityId=BusinessTempMaster.city inner join StateMasterTbl on StateMasterTbl.StateId=BusinessTempMaster.state inner join CountryMaster on CountryMaster.CountryId=BusinessTempMaster.country inner join UserLoginLog on UserLoginLog.businessid=BusinessTempMaster.ID where BusinessTempMaster.status='1' " + st1 + " " + st2 + "";

        GvBlisting.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " BusinessTempMaster.BusinessName asc";

        Label1.Text = ViewState["count"].ToString();

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dtt = GetDataPage(GvBlisting.PageIndex, GvBlisting.PageSize, sortExpression, str);

            //SqlDataAdapter da = new SqlDataAdapter(str2, con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            // Label1.Text = dtt.Rows.Count.ToString();

            GvBlisting.DataSource = dtt;
            GvBlisting.DataBind();
        }
        else
        {
            GvBlisting.DataSource = null;
            GvBlisting.DataBind();
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

    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
    }

    protected void filterSub_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillsubsubcategory();
        lblmsg.Text = "";
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        object obj = new object();
        EventArgs ee = new EventArgs();
        btnGo_Click(obj, ee);
    }

    protected void GvBlisting_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvBlisting.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void btnPrintVersion_Click(object sender, EventArgs e)
    {
        if (btnPrintVersion.Text == "Printable Version")
        {
            btnPrintVersion.Text = "Hide Printable Version";
            btnPrint.Visible = true;

            GvBlisting.AllowPaging = false;
            GvBlisting.PageSize = 100000;
            fillgrid();
            //if (GvBlisting.Columns[10].Visible == true)
            //{
            //    ViewState["editHide"] = "tt";
            //    GvBlisting.Columns[10].Visible = false;
            //}

            lblmsg.Text = "";
        }
        else
        {
            btnPrintVersion.Text = "Printable Version";
            btnPrint.Visible = false;

            GvBlisting.AllowPaging = true;
            GvBlisting.PageSize = 50;
            fillgrid();
            //if (ViewState["editHide"] != null)
            //{
            //    GvBlisting.Columns[10].Visible = true;
            //}
        }
    }
    protected void GvBlisting_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GvBlisting.EditIndex = e.NewEditIndex;
        fillgrid();

        DropDownList DropDownList1 = (DropDownList)(GvBlisting.Rows[GvBlisting.EditIndex].FindControl("DropDownList1"));
    }
    protected void GvBlisting_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GvBlisting.EditIndex = -1;
        fillgrid();
    }
    protected void GvBlisting_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList DropDownList1 = (DropDownList)(GvBlisting.Rows[e.RowIndex].FindControl("DropDownList1"));
        Label lblcandiID = (Label)(GvBlisting.Rows[e.RowIndex].FindControl("lblcandiID"));

        SqlCommand cmdup = new SqlCommand("update businessadd set status='" + DropDownList1.SelectedValue + "' where ID='" + lblcandiID.Text + "'", con);
        con.Open();
        cmdup.ExecuteNonQuery();
        con.Close();

        GvBlisting.EditIndex = -1;
        fillgrid();

        lblmsg.Text = "Record updated successfully.";
    }
}






