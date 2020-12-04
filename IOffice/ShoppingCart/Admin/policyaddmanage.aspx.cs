﻿using System;
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
using System.Windows;
using Winthusiasm.HtmlEditor;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

public partial class ShoppingCart_Admin_PolicyAddManage : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        ViewState["sortOrder"] = "";


        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            fillstore();
            fillpolicycategory();
            ddlpolicytitle.Items.Insert(0, "-Select-");
            ddlpolicytitle.Items[0].Value = "0";

            filterstore();
            ddlfilterstore_SelectedIndexChanged(sender, e);
            fillDepartment();
            fillDesignation();
            fillemployee();
            lblBusiness.Text = "All";

            //ddlfilterpolicyprocedure.Items.Insert(0, "All");
            //ddlfilterpolicyprocedure.Items[0].Value = "0";
            //ddlfilterpolicy.Items.Insert(0, "All");
            //ddlfilterpolicy.Items[0].Value = "0";

            //fillPolicyTitle();
            fillgrid();


        }
    }
    protected void getversion()
    {
        string str = "select MAX(Version) as Version from PolicyAddManagetbl where PolicyAddManagetbl.PolicyId='" + ddlpolicytitle.SelectedValue + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ViewState["version"] = ds.Tables[0].Rows[0]["Version"].ToString();
        if (ViewState["version"].ToString() != null && ViewState["version"].ToString() != "")
        {

            Int32 version = 0;
            version = Convert.ToInt32(ViewState["version"].ToString());
            version = version + 1;
            lblversion.Text = version.ToString();
        }
        else
        {
            lblversion.Text = "1";
        }
    }
    protected void fillstore()
    {
        ddlstore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlstore.DataSource = ds;
        ddlstore.DataTextField = "Name";
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void filterstore()
    {
        ddlfilterstore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlfilterstore.DataSource = ds;
        ddlfilterstore.DataTextField = "Name";
        ddlfilterstore.DataValueField = "WareHouseId";
        ddlfilterstore.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlfilterstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        //ddlfilterstore.Items.Insert(0, "All");
        //ddlfilterstore.Items[0].Value = "0";
    }


    protected void Button2_CheckedChanged(object sender, EventArgs e)
    {
        if (!Pnl1.Visible)
        {
            Pnl1.Visible = true;
        }
        else
        {
            Pnl1.Visible = false;
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtenderAddnew.Hide();
        chkdepartment.Checked = false;
    }
    protected void fillDepartment()
    {
        string str = " select DepartmentmasterMNC.* from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where DepartmentmasterMNC.Whid='" + ddlstore.SelectedValue + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        grddepartment.DataSource = dt;
        grddepartment.DataBind();
    }
    protected void fillDesignation()
    {
        string str = " select DesignationMaster.*,DesignationMaster.DesignationName + ':' +DepartmentmasterMNC.Departmentname as DesignationName,DepartmentmasterMNC.id from DesignationMaster inner join  DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where DepartmentmasterMNC.Whid='" + ddlstore.SelectedValue + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddldesignation.DataSource = dt;
        ddldesignation.DataTextField = "DesignationName";
        ddldesignation.DataValueField = "DesignationMasterId";
        ddldesignation.DataBind();
        ddldesignation.Items.Insert(0, "-Select-");
        ddldesignation.Items[0].Value = "0";
    }
    protected void fillemployee()
    {
        string str = "Select EmployeeMaster.EmployeeMasterID,EmployeeMaster.DeptID,EmployeeMaster.DesignationMasterId,EmployeeMaster.Whid,EmployeeMaster.EmployeeName,DepartmentmasterMNC.id,DesignationMaster.DesignationMasterId from EmployeeMaster inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join DepartmentmasterMNC on EmployeeMaster.DeptID=DepartmentmasterMNC.id where EmployeeMaster.Whid='" + ddlstore.SelectedValue + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ddlemployee.DataSource = ds;
        ddlemployee.DataTextField = "EmployeeName";
        ddlemployee.DataValueField = "EmployeeMasterID";
        ddlemployee.DataBind();
        ddlemployee.Items.Insert(0, "-Select-");
        ddlemployee.Items[0].Value = "0";
    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDepartment();
        fillDesignation();
        fillemployee();
        fillpolicycategory();
    }
    protected void chkdepartment_CheckedChanged(object sender, EventArgs e)
    {
        if (chkdepartment.Checked)
        {
            ModalPopupExtenderAddnew.Show();

        }
        else
        {
            ModalPopupExtenderAddnew.Hide();
        }
    }
    protected void fillpolicycategory()
    {
        ddlpolicycategory.Items.Clear();

        string st1 = "select * from Policyprocedureruletbl where compid='" + Session["Comid"] + "' and Whid='" + ddlstore.SelectedValue + "' ";

        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

        ddlpolicycategory.DataSource = dt1;
        ddlpolicycategory.DataTextField = "Policyprocedurecategory";
        ddlpolicycategory.DataValueField = "Id";
        ddlpolicycategory.DataBind();
        ddlpolicycategory.Items.Insert(0, "-Select-");
        ddlpolicycategory.Items[0].Value = "0";

    }
    protected void fillfilterpolicy()
    {
        ddlfilterpolicyprocedure.Items.Clear();


        if (ddlfilterstore.SelectedIndex > 0)
        {
            string st1 = "select * from Policyprocedureruletbl where compid='" + Session["Comid"] + "' and Policyprocedureruletbl.Whid='" + ddlfilterstore.SelectedValue + "'";


            SqlCommand cmd1 = new SqlCommand(st1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);

            ddlfilterpolicyprocedure.DataSource = dt1;
            ddlfilterpolicyprocedure.DataTextField = "Policyprocedurecategory";
            ddlfilterpolicyprocedure.DataValueField = "Id";
            ddlfilterpolicyprocedure.DataBind();
        }
        ddlfilterpolicyprocedure.Items.Insert(0, "All");
        ddlfilterpolicyprocedure.Items[0].Value = "0";
        fillfilterpolicytitle();



    }
    protected void fillpolicytitle()
    {
        ddlpolicytitle.Items.Clear();
        string st1 = "select * from Policyprocedureruletiletbl where compid='" + Session["Comid"] + "' and Whid='" + ddlstore.SelectedValue + "' and Policyprocedureruleid='" + ddlpolicycategory.SelectedValue + "'";

        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        ddlpolicytitle.DataSource = dt1;
        ddlpolicytitle.DataTextField = "PolicyTitle";
        ddlpolicytitle.DataValueField = "Id";
        ddlpolicytitle.DataBind();
        ddlpolicytitle.Items.Insert(0, "-Select-");
        ddlpolicytitle.Items[0].Value = "0";
    }
    protected void fillfilterpolicytitle()
    {
        ddlfilterpolicy.Items.Clear();


        if (ddlfilterpolicyprocedure.SelectedIndex > 0)
        {
            string st1 = "select * from Policyprocedureruletiletbl where compid='" + Session["Comid"] + "' and Policyprocedureruletiletbl.Policyprocedureruleid='" + ddlfilterpolicyprocedure.SelectedValue + "'";


            SqlCommand cmd1 = new SqlCommand(st1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);
            ddlfilterpolicy.DataSource = dt1;
            ddlfilterpolicy.DataTextField = "PolicyTitle";
            ddlfilterpolicy.DataValueField = "Id";
            ddlfilterpolicy.DataBind();
        }
        ddlfilterpolicy.Items.Insert(0, "All");
        ddlfilterpolicy.Items[0].Value = "0";

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        insertdata();
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
        fillgrid();
    }
    protected void insertdata()
    {
        try
        {
            string strinsert;

            if (chkstatus.Checked)
            {
                strinsert = "Insert into PolicyAddManagetbl(PolicyCategoryId,PolicyId,Version,PolicyName,PolicyDescription,Startdate,Enddate,Status,Whid,Compid,DesignationId,EmployeeId,Penaltypoint)" +
                                "values('" + ddlpolicycategory.SelectedValue + "','" + ddlpolicytitle.SelectedValue + "','" + lblversion.Text + "','" + txtpolicyname.Text + "','" + txtdescription.Text + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','1','" + ddlstore.SelectedValue + "','" + Session["Comid"] + "','" + ddldesignation.SelectedValue + "','" + ddlemployee.SelectedValue + "','" + txtpenaltypoint.Text + "')";
            }
            else
            {
                strinsert = "Insert into PolicyAddManagetbl(PolicyCategoryId,PolicyId,Version,PolicyName,PolicyDescription,Startdate,Enddate,Status,Whid,Compid,DesignationId,EmployeeId,Penaltypoint)" +
                                "values('" + ddlpolicycategory.SelectedValue + "','" + ddlpolicytitle.SelectedValue + "','" + lblversion.Text + "','" + txtpolicyname.Text + "','" + txtdescription.Text + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','0','" + ddlstore.SelectedValue + "','" + Session["Comid"] + "','" + ddldesignation.SelectedValue + "','" + ddlemployee.SelectedValue + "','" + txtpenaltypoint.Text + "')";
            }

            SqlCommand cmd = new SqlCommand(strinsert, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


            string getprojectId = "select MAX(Id) As ID From PolicyAddManagetbl";
            SqlCommand cmdmonth = new SqlCommand(getprojectId, con);
            SqlDataAdapter adpmonth = new SqlDataAdapter(cmdmonth);
            DataTable dtmonth = new DataTable();
            adpmonth.Fill(dtmonth);
            if (dtmonth.Rows.Count > 0)
            {
                ViewState["Id"] = dtmonth.Rows[0]["ID"].ToString();
            }


            if (gridFileAttach.Rows.Count > 0)
            {

                foreach (GridViewRow g1 in gridFileAttach.Rows)
                {
                    string filename = (g1.FindControl("lblpdfurl") as Label).Text;
                    string name = (g1.FindControl("lbltitle") as Label).Text;
                    string str22 = "Insert Into Add_policy_docs (policyid,title,filename) Values ('" + dtmonth.Rows[0]["ID"].ToString() + "','" + name + "','" + filename + "')";
                    //    string query = "insert into product values(" + id + ",'" + name + "'," + price + ",'" + description + "')";
                    SqlCommand cmd12 = new SqlCommand(str22, con);
                    con.Open();
                    cmd12.ExecuteNonQuery();
                    con.Close();
                }

            }


            statuslable.Text = "Record inserted successfully";
            clearall();
        }
        catch (Exception es)
        {
            statuslable.Text = es.Message.ToString();
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
        statuslable.Text = "";
        clearall();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string strupdate;

        if (chkstatus.Checked)
        {
            strupdate = "Update PolicyAddManagetbl set PolicyCategoryId='" + ddlpolicycategory.SelectedValue + "',PolicyId='" + ddlpolicytitle.SelectedValue + "',Version='" + lblversion.Text + "',PolicyName='" + txtpolicyname.Text + "',PolicyDescription='" + txtdescription.Text + "',Startdate='" + txtestartdate.Text + "',Enddate='" + txteenddate.Text + "',Status='1',Whid='" + ddlstore.SelectedValue + "',Compid='" + Session["Comid"] + "',DesignationId='" + ddldesignation.SelectedValue + "',EmployeeId='" + ddlemployee.SelectedValue + "',Penaltypoint='" + txtpenaltypoint.Text + "' where Id='" + ViewState["id"] + "'";

        }
        else
        {
            strupdate = "Update PolicyAddManagetbl set PolicyCategoryId='" + ddlpolicycategory.SelectedValue + "',PolicyId='" + ddlpolicytitle.SelectedValue + "',Version='" + lblversion.Text + "',PolicyName='" + txtpolicyname.Text + "',PolicyDescription='" + txtdescription.Text + "',Startdate='" + txtestartdate.Text + "',Enddate='" + txteenddate.Text + "',Status='0',Whid='" + ddlstore.SelectedValue + "',Compid='" + Session["Comid"] + "',DesignationId='" + ddldesignation.SelectedValue + "',EmployeeId='" + ddlemployee.SelectedValue + "',Penaltypoint='" + txtpenaltypoint.Text + "' where Id='" + ViewState["id"] + "'";
        }

        SqlCommand cmd = new SqlCommand(strupdate, con);

        string strdel = "delete from  Add_policy_docs where procedureid = '" + ViewState["id"] + "'";
        SqlDataAdapter da = new SqlDataAdapter(strdel, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        foreach (GridViewRow g1 in gridFileAttach.Rows)
        {
            string filename = (g1.FindControl("lblpdfurl") as Label).Text;
            string name = (g1.FindControl("lbltitle") as Label).Text;
            string str22 = "Insert Into Add_policy_docs (policyid,title,filename) Values ('" + ViewState["id"] + "','" + name + "','" + filename + "')";
            //    string query = "insert into product values(" + id + ",'" + name + "'," + price + ",'" + description + "')";
            SqlCommand cmd12 = new SqlCommand(str22, con);
            con.Open();
            cmd12.ExecuteNonQuery();
            con.Close();
        }

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        statuslable.Text = "Record updated successfully";
        btnsubmit.Visible = true;
        btnreset.Visible = true;
        btnupdate.Visible = false;
        btncancel.Visible = false;
        fillgrid();
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
        clearall();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
        statuslable.Text = "";
        clearall();
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {

            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;

            grid.AllowPaging = false;
            grid.PageSize = 1000;
            fillgrid();

            if (grid.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grid.Columns[6].Visible = false;
            }
            if (grid.Columns[7].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                grid.Columns[7].Visible = false;
            }
            if (grid.Columns[8].Visible == true)
            {
                ViewState["viewHide"] = "tt";
                grid.Columns[8].Visible = false;
            }
        }
        else
        {

            btncancel0.Text = "Printable Version";
            Button7.Visible = false;

            grid.AllowPaging = true;
            grid.PageSize = 10;
            fillgrid();

            if (ViewState["editHide"] != null)
            {
                grid.Columns[6].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                grid.Columns[7].Visible = true;
            }
            if (ViewState["viewHide"] != null)
            {
                grid.Columns[8].Visible = true;
            }
        }
    }
    protected void btnsubmitpop1_Click(object sender, EventArgs e)
    {

        if (chkdepartment.Checked)
        {

            foreach (GridViewRow gdr in grddepartment.Rows)
            {
                Label lbldepartmasterid = (Label)gdr.FindControl("lbldepartmasterid");
                CheckBox chkactive123 = (CheckBox)gdr.FindControl("chkactive123");
                if (chkactive123.Checked)
                {
                    String strdept = "Insert Into Policybydefaulttbl (PolicyMasterId,version,DepartmentId,status)values('" + ddlpolicytitle.SelectedValue + "','" + lblversion.Text + "','" + lbldepartmasterid.Text + "','" + chkactive123.Checked + "')";

                    SqlCommand cmddept = new SqlCommand(strdept, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmddept.ExecuteNonQuery();
                    con.Close();
                }
                ModalPopupExtenderAddnew.Hide();
                chkdepartment.Checked = false;
            }
        }
    }
    protected void clearall()
    {
        ddlpolicytitle.SelectedIndex = 0;
        txtpolicyname.Text = "";
        txtdescription.Text = "";
        chkdepartment.Checked = false;
        txtpenaltypoint.Text = "";
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
        txtestartdate.Text = "";
        txteenddate.Text = "";
        rblemforcedby.SelectedIndex = 0;
        pnldesignation.Visible = true;
        pnlemployee.Visible = false;

    }
    protected void rblemforcedby_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblemforcedby.SelectedValue == "0")
        {
            pnldesignation.Visible = true;
            pnlemployee.Visible = false;
        }
        if (rblemforcedby.SelectedValue == "1")
        {
            pnldesignation.Visible = false;
            pnlemployee.Visible = true;

        }

    }
    protected void fillgrid()
    {
        lblcmpny.Text = Session["comid"].ToString();

        string st2 = "";

        if (ddlfilterstore.SelectedIndex > 0)
        {
            lblBusiness.Text = ddlfilterstore.SelectedItem.Text;
            st2 += " and PolicyAddManagetbl.Whid='" + ddlfilterstore.SelectedValue + "'";
        }
        if (ddlfilterpolicy.SelectedIndex > 0)
        {

            st2 += " and PolicyAddManagetbl.PolicyId='" + ddlfilterpolicy.SelectedValue + "'";
        }
        if (ddlfilterpolicyprocedure.SelectedIndex > 0)
        {
            st2 += "and PolicyAddManagetbl.PolicyCategoryId='" + ddlfilterpolicyprocedure.SelectedValue + "'";
        }

        if (showlatest.Checked)
        {
            string str12 = "select MAX(Version) as Version from PolicyAddManagetbl where PolicyAddManagetbl.Compid = '" + Session["Comid"] + "' ";
            SqlCommand cmd12 = new SqlCommand(str12, con);
            SqlDataAdapter adpt12 = new SqlDataAdapter(cmd12);
            DataSet ds12 = new DataSet();
            adpt12.Fill(ds12);
            ViewState["version"] = ds12.Tables[0].Rows[0]["Version"].ToString();
            st2 += "and PolicyAddManagetbl.Version='" + ViewState["version"] + "'";
        }

        string str = " PolicyAddManagetbl.Id,case when(PolicyAddManagetbl.Status = '1') then 'Active' else 'Inactive' end as Status11,PolicyAddManagetbl.PolicyCategoryId,PolicyAddManagetbl.PolicyId,PolicyAddManagetbl.Version,PolicyAddManagetbl.PolicyName,PolicyAddManagetbl.Status,PolicyAddManagetbl.Whid,PolicyAddManagetbl.Compid,PolicyAddManagetbl.Penaltypoint,Policyprocedureruletbl.Policyprocedurecategory,Policyprocedureruletiletbl.PolicyTitle from PolicyAddManagetbl inner join Policyprocedureruletiletbl on PolicyAddManagetbl.PolicyId=Policyprocedureruletiletbl.Id inner join Policyprocedureruletbl on Policyprocedureruletbl.Id=PolicyAddManagetbl.PolicyCategoryId where  PolicyAddManagetbl.Compid = '" + Session["Comid"] + "' " + st2 + "";

        string str2 = " select Count(PolicyAddManagetbl.Id) as ci  from PolicyAddManagetbl inner join Policyprocedureruletiletbl on PolicyAddManagetbl.PolicyId=Policyprocedureruletiletbl.Id inner join Policyprocedureruletbl on Policyprocedureruletbl.Id=PolicyAddManagetbl.PolicyCategoryId where  PolicyAddManagetbl.Compid = '" + Session["Comid"] + "' " + st2 + "";

        grid.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " Policyprocedureruletbl.Policyprocedurecategory,Policyprocedureruletiletbl.PolicyTitle,PolicyAddManagetbl.PolicyName";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(grid.PageIndex, grid.PageSize, sortExpression, str);

            grid.DataSource = dt;

            DataView dv = new DataView();
            dv = dt.DefaultView;
            if (hdnsortExp.Value != string.Empty)
            {
                dv.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            grid.DataBind();

            foreach (GridViewRow gvbn in grid.Rows)
            {
                Label dd = (Label)gvbn.FindControl("lbl33");
                LinkButton LinkButton1 = (LinkButton)gvbn.FindControl("LinkButton1");
                string str22 = "select count(id) from Add_policy_docs where policyid='" + dd.Text + "' ";

                SqlCommand cmd12 = new SqlCommand(str22, con);
                SqlDataAdapter adpme1 = new SqlDataAdapter(cmd12);
                DataTable dtme1 = new DataTable();
                adpme1.Fill(dtme1);

                if (dtme1.Rows.Count > 0)
                {

                    LinkButton1.Text = dtme1.Rows[0][0].ToString();
                    // grid.DataBind();
                }
            }

        }
        else
        {
            grid.DataSource = null;
            grid.DataBind();
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

    protected void ddlruletitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        getversion();
    }
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //lbllegend.Text = "Edit Policy";
        //btnadd.Visible = false;
        //statuslable.Text = "";
        //Pnladdnew.Visible = true;

        //ViewState["id"] = grid.DataKeys[e.NewEditIndex].Value.ToString();
        //string str = "select * from PolicyAddManagetbl where Id='" + ViewState["id"] + "'";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adpt.Fill(dt);
        //btnupdate.Visible = true;
        //btncancel.Visible = true;
        //btnsubmit.Visible = false;
        //btnreset.Visible = false;
        //fillstore();
        //ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
        //fillpolicycategory();
        //ddlpolicycategory.SelectedIndex = ddlpolicycategory.Items.IndexOf(ddlpolicycategory.Items.FindByValue(dt.Rows[0]["PolicyCategoryId"].ToString()));
        //fillpolicytitle();
        //ddlpolicytitle.SelectedIndex = ddlpolicytitle.Items.IndexOf(ddlpolicytitle.Items.FindByValue(dt.Rows[0]["PolicyId"].ToString()));
        //ViewState["policymasterid"] = dt.Rows[0]["PolicyId"].ToString();
        //lblversion.Text = dt.Rows[0]["Version"].ToString();
        //ViewState["getversion"] = dt.Rows[0]["Version"].ToString();
        //txtpolicyname.Text = dt.Rows[0]["PolicyName"].ToString();
        //if (Convert.ToString(dt.Rows[0]["PolicyDescription"]) != "")
        //{
        //    Button2.Checked = true;
        //    Pnl1.Visible = true;
        //    txtdescription.Text = Convert.ToString(dt.Rows[0]["PolicyDescription"].ToString()); ;
        //}
        //else
        //{
        //    Button2.Checked = false;
        //    Pnl1.Visible = false;
        //    txtdescription.Text = "";

        //}
        //txtpenaltypoint.Text = dt.Rows[0]["Penaltypoint"].ToString();

        //if (dt.Rows[0]["DesignationId"].ToString() != "0")
        //{
        //    pnldesignation.Visible = true;
        //    fillDesignation();
        //    ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(dt.Rows[0]["DesignationId"].ToString()));
        //}
        //else
        //{
        //    pnldesignation.Visible = false;

        //}
        //if (dt.Rows[0]["EmployeeId"].ToString() != "0")
        //{
        //    pnlemployee.Visible = true;
        //    fillemployee();
        //    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dt.Rows[0]["EmployeeId"].ToString()));
        //}
        //else
        //{
        //    pnlemployee.Visible = false;
        //}
        //txtestartdate.Text = dt.Rows[0]["Startdate"].ToString();
        //txteenddate.Text = dt.Rows[0]["Enddate"].ToString();
        //if (dt.Rows[0]["Status"].ToString() == "True")
        //{
        //    chkstatus.Checked = true;
        //}
        //else
        //{
        //    chkstatus.Checked = false;
        //}
        //ModalPopupExtenderAddnew.Show();

        //btnsubmitpop1.Visible = false;
        //btnupdatepop1.Visible = true;
    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "Edit Policy";
            btnadd.Visible = false;
            statuslable.Text = "";
            Pnladdnew.Visible = true;

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["id"] = mm;

            string str = "select * from PolicyAddManagetbl where Id='" + ViewState["id"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            btnupdate.Visible = true;
            btncancel.Visible = true;
            btnsubmit.Visible = false;
            btnreset.Visible = false;
            fillstore();
            ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
            fillpolicycategory();
            ddlpolicycategory.SelectedIndex = ddlpolicycategory.Items.IndexOf(ddlpolicycategory.Items.FindByValue(dt.Rows[0]["PolicyCategoryId"].ToString()));
            fillpolicytitle();
            ddlpolicytitle.SelectedIndex = ddlpolicytitle.Items.IndexOf(ddlpolicytitle.Items.FindByValue(dt.Rows[0]["PolicyId"].ToString()));
            ViewState["policymasterid"] = dt.Rows[0]["PolicyId"].ToString();
            lblversion.Text = dt.Rows[0]["Version"].ToString();
            ViewState["getversion"] = dt.Rows[0]["Version"].ToString();
            txtpolicyname.Text = dt.Rows[0]["PolicyName"].ToString();
            if (Convert.ToString(dt.Rows[0]["PolicyDescription"]) != "")
            {
                Button2.Checked = true;
                Pnl1.Visible = true;
                txtdescription.Text = Convert.ToString(dt.Rows[0]["PolicyDescription"].ToString()); ;
            }
            else
            {
                Button2.Checked = false;
                Pnl1.Visible = false;
                txtdescription.Text = "";

            }
            txtpenaltypoint.Text = dt.Rows[0]["Penaltypoint"].ToString();

            if (dt.Rows[0]["DesignationId"].ToString() != "0")
            {
                pnldesignation.Visible = true;
                fillDesignation();
                ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(dt.Rows[0]["DesignationId"].ToString()));
            }
            else
            {
                pnldesignation.Visible = false;

            }
            if (dt.Rows[0]["EmployeeId"].ToString() != "0")
            {
                pnlemployee.Visible = true;
                fillemployee();
                ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dt.Rows[0]["EmployeeId"].ToString()));
            }
            else
            {
                pnlemployee.Visible = false;
            }
            txtestartdate.Text = dt.Rows[0]["Startdate"].ToString();
            txteenddate.Text = dt.Rows[0]["Enddate"].ToString();
            if (dt.Rows[0]["Status"].ToString() == "True")
            {
                chkstatus.Checked = true;
            }
            else
            {
                chkstatus.Checked = false;
            }
            ModalPopupExtenderAddnew.Show();

            btnsubmitpop1.Visible = false;
            btnupdatepop1.Visible = true;
        }
    }
    protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void btnupdatepop1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gdr in grddepartment.Rows)
        {
            Label lbldepartmasterid = (Label)gdr.FindControl("lbldepartmasterid");
            CheckBox chkactive123 = (CheckBox)gdr.FindControl("chkactive123");
            string strdept = "Select * from Policybydefaulttbl where DepartmentId='" + lbldepartmasterid.Text + "' and  PolicyMasterId='" + ViewState["policymasterid"] + "' and version='" + ViewState["getversion"] + "'";
            SqlCommand cmddept = new SqlCommand(strdept, con);
            SqlDataAdapter addept = new SqlDataAdapter(cmddept);
            DataSet ds = new DataSet();
            addept.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                String strupdate = "Update Policybydefaulttbl set DepartmentId='" + lbldepartmasterid.Text + "',status='" + chkactive123.Checked + "' where PolicyMasterId='" + ViewState["policymasterid"] + "' and version = '" + ViewState["getversion"] + "'";

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
                String strupdate = "insert into Policybydefaulttbl (PolicyMasterId,version,DepartmentId,status) values ('" + ViewState["policymasterid"] + "','" + ViewState["getversion"] + "','" + lbldepartmasterid.Text + "','" + chkactive123.Checked + "')";

                SqlCommand cmdupdate = new SqlCommand(strupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupdate.ExecuteNonQuery();
                con.Close();
            }


        }
        ModalPopupExtenderAddnew.Hide();
        chkdepartment.Checked = false;
        btnsubmitpop1.Visible = true;
        btnupdatepop1.Visible = false;
        fillgrid();
    }
    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ViewState["deleteid"] = grid.DataKeys[e.RowIndex].Value.ToString();
        string str = "delete from PolicyAddManagetbl where Id='" + ViewState["deleteid"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        statuslable.Text = "Record deleted successfully";
        fillgrid();
    }
    protected void ddlfilterstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilterpolicy();
        fillgrid();
    }
    protected void ddlfilterpolicy_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    public void ddlfilterpolicyprocedure_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilterpolicytitle();
        fillgrid();
    }
    protected void ddlfilterruletitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void showlatest_CheckedChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlpolicycategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpolicytitle();
    }
    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
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
    protected void imgadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "Policyprocedurerulecategory.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgaddtitle_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PolicyCategoryTitle.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgrefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillpolicycategory();
    }
    protected void imgrefreshtitle_Click(object sender, ImageClickEventArgs e)
    {
        fillpolicytitle();
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        fillstore();
        fillpolicycategory();

        Pnl1.Visible = false;
        Button2.Checked = false;

        ddlpolicycategory.SelectedIndex = 0;
        ddlpolicytitle.SelectedIndex = 0;

        lblversion.Text = "1";

        Session["GridFileAttach1"] = null;

        lbllegend.Text = "Add New Policy";
        Pnladdnew.Visible = true;
        statuslable.Text = "";
        btnadd.Visible = false;

        txtestartdate.Text = System.DateTime.Now.ToShortDateString();
        txteenddate.Text = System.DateTime.Now.ToShortDateString();
    }
    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "Title";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "FileName";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom2);

        return dt;
    }
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        string ext = "";
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "pdf", "PDF", "MP3", "MP4", "wma", "html", "css", "rar", "zip", "rpt" };
        string[] validFileTypes1 = { "MP3", "MP4", "pdf", "PDF", "wma", "html", "css", "rar", "zip", "rpt", "xls", "xlsx" };
        bool isValidFile = false;
        if (Upradio.SelectedValue == "1")
        {
            ext = System.IO.Path.GetExtension(fileuploadaudio.PostedFile.FileName);
            for (int i = 0; i < validFileTypes1.Length; i++)
            {
                if (ext == "." + validFileTypes1[i])
                {
                    isValidFile = true;
                    break;

                }
            }
        }
        else
        {
            if (fileuploadadattachment.HasFile == true)
            {

                ext = System.IO.Path.GetExtension(fileuploadadattachment.PostedFile.FileName);
                for (int i = 0; i < validFileTypes.Length; i++)
                {
                    if (ext == "." + validFileTypes[i])
                    {
                        isValidFile = true;
                        break;
                    }

                }
            }
        }


        if (!isValidFile)
        {

            Label65.Visible = true;
            if (Upradio.SelectedValue == "1")
            {
                //  Label65.Text = "Invalid File. Please upload a File with extension " +
                ///
                string.Join(",", validFileTypes1);
            }
            else
            {
                // Label65.Text = "Invalid File. Please upload a File with extension " +

                string.Join(",", validFileTypes);
            }

        }

        else
        {

            String filename = "";
            string audiofile = "";
            //PnlFileAttachLbl.Visible = true;
            if (fileuploadadattachment.HasFile || fileuploadaudio.HasFile)
            {
                if (fileuploadadattachment.HasFile)
                {
                    filename = fileuploadadattachment.FileName;
                    fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Attach\\") + filename);
                    fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + filename);
                    string file = Path.GetFileName(fileuploadadattachment.PostedFile.FileName);
                    Stream str = fileuploadadattachment.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(str);
                    Byte[] size = br.ReadBytes((int)str.Length);

                }
                if (fileuploadaudio.HasFile)
                {
                    // audiofile = fileuploadaudio.FileName;
                    //fileuploadaudio.PostedFile.SaveAs(Server.MapPath("~\\Attach\\") + audiofile);
                }
                //hdnFileName.Value = filename;
                DataTable dt = new DataTable();
                if (Session["GridFileAttach1"] == null)
                {
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "PDFURL";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);

                    DataColumn dtcom3 = new DataColumn();
                    dtcom3.DataType = System.Type.GetType("System.String");
                    dtcom3.ColumnName = "Title";
                    dtcom3.ReadOnly = false;
                    dtcom3.Unique = false;
                    dtcom3.AllowDBNull = true;
                    dt.Columns.Add(dtcom3);

                    DataColumn dtcom4 = new DataColumn();

                    dtcom4.DataType = System.Type.GetType("System.String");
                    dtcom4.ColumnName = "AudioURL";
                    dtcom4.ReadOnly = false;
                    dtcom4.Unique = false;
                    dtcom4.AllowDBNull = true;
                    dt.Columns.Add(dtcom4);

                }
                else
                {
                    dt = (DataTable)Session["GridFileAttach1"];
                }
                DataRow dtrow = dt.NewRow();
                dtrow["PDFURL"] = filename;
                dtrow["Title"] = txttitlename.Text;
                dtrow["AudioURL"] = audiofile;

                // dtrow["FileNameChanged"] = hdnFileName.Value;
                dt.Rows.Add(dtrow);
                Session["GridFileAttach1"] = dt;
                gridFileAttach.DataSource = dt;


                gridFileAttach.DataBind();
                txttitlename.Text = "";
            }
            else
            {
                Label65.Visible = true;
                Label65.Text = "Please Attach File to Upload.";
                return;
            }
        }
    }
    protected void Upradio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Upradio.SelectedValue == "1")
        {
            pnladio.Visible = true;
            pnlpdfup.Visible = false;
        }
        else
        {
            pnladio.Visible = false;
            pnlpdfup.Visible = true;
        }
    }
    protected void Label4731235531_Click1(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
       
        LinkButton name = (LinkButton)GridView3.Rows[j].FindControl("lbl473123553");
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('http://license.busiwiz.com/Attach/" + name.Text + "');", true);
    }
    protected void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            pnladio.Visible = false;
            pnlpdfup.Visible = true;
            pnlup.Visible = true;
        }
    }
    protected void LinkButton1_Click(object sender, System.EventArgs e)
    {

        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label res1 = (Label)grid.Rows[j].FindControl("lbl33");

        ViewState["Id"] = res1.Text;

        string str22 = "select * from Add_policy_docs where policyid='" + ViewState["Id"].ToString() + "' ";

        SqlCommand cmdcln = new SqlCommand(str22, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        GridView3.DataSource = dtcln;
        GridView3.DataBind();

        //  Label63.Text = dt1.Rows[0]["Name"].ToString();
        // Label64.Text = DateTime.Now.ToString();




        ModalPopupExtender1.Show();
    }
    protected void ImageButton3_Click1(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void gridFileAttach_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete1")
        {
            gridFileAttach.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            if (Session["GridFileAttach1"] != null)
            {
                if (gridFileAttach.Rows.Count > 0)
                {
                    dt = (DataTable)Session["GridFileAttach1"];

                    dt.Rows.Remove(dt.Rows[gridFileAttach.SelectedIndex]);


                    gridFileAttach.DataSource = dt;
                    gridFileAttach.DataBind();
                    Session["GridFileAttach1"] = dt;
                }
            }

        }
    }
    protected void GridView3_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {

    }

}
