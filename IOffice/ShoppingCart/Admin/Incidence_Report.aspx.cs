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
using Winthusiasm.HtmlEditor;

public partial class ShoppingCart_Admin_Incidence_Report : System.Web.UI.Page
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
        ViewState["Compid"] = Session["Comid"].ToString();
        ViewState["UserName"] = Session["userid"].ToString();



        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            lblcmpny.Text = Session["Cname"].ToString();
            ViewState["sortOrder"] = "";
            fillstore();
            filterstore();
            fillemployee();
            filteremp();
            fillpolicy();
            fillprocedures();
            fillDepartment();
            fillrules();
            fillfilterpolicy();
            fillfilterprocedures();
            fillfilterrules();
            txtfromdt.Text = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
            txttodt.Text = System.DateTime.Now.ToShortDateString();
            txteenddate.Text = Convert.ToString(System.DateTime.Now.ToShortDateString());
            txttime.Text = System.DateTime.Now.ToString("HH:mm");
            string time = System.DateTime.Now.ToString("tt");
            if (time == "PM")
            {
                ddltime.SelectedIndex = ddltime.Items.IndexOf(ddltime.Items.FindByValue("1"));
            }
            else
            {
                ddltime.SelectedIndex = ddltime.Items.IndexOf(ddltime.Items.FindByValue("0"));
            }
            fillgrid();
            getversion();
        }
    }
    protected void getversion()
    {
        string str = "select MAX(Id) as ID from IncidenceAddManagetbl ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ViewState["insidenceid"] = ds.Tables[0].Rows[0]["ID"].ToString();
        if (ViewState["insidenceid"].ToString() != null && ViewState["insidenceid"].ToString() != "")
        {
            Int32 version = 0;
            version = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
            version = version + 1;
            lblincidence.Text = version.ToString();
        }
        else
        {
            lblincidence.Text = "1";
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
        ddlsearchByStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlsearchByStore.DataSource = ds;
        ddlsearchByStore.DataTextField = "Name";
        ddlsearchByStore.DataValueField = "WareHouseId";
        ddlsearchByStore.DataBind();
        ddlsearchByStore.Items.Insert(0, "All");
        ddlsearchByStore.Items[0].Value = "0";
    }
    protected void fillemployee()
    {
        string str = "select Login_master.UserID,Login_master.username,User_master.UserID,User_master.PartyID," +
                    " EmployeeMaster.EmployeeMasterID,Party_master.PartyID,Party_master.id,EmployeeMaster.SuprviserId " +
                    " from Login_master inner join User_master on Login_master.UserID=User_master.UserID  " +
                    " inner join Party_master on User_master.PartyID=Party_master.PartyID inner join " +
                    " EmployeeMaster on User_master.PartyID=EmployeeMaster.PartyID where Login_master.UserID='" + ViewState["UserName"] + "' and Party_master.id='" + ViewState["Compid"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ViewState["supervisor"] = ds.Tables[0].Rows[0]["SuprviserId"].ToString();
        ViewState["empid"] = ds.Tables[0].Rows[0]["EmployeeMasterID"].ToString();

        string str12 = "Select EmployeeMaster.EmployeeMasterID,EmployeeMaster.DeptID,EmployeeMaster.DesignationMasterId,EmployeeMaster.Whid,EmployeeMaster.EmployeeName,DepartmentmasterMNC.id,DesignationMaster.DesignationMasterId from EmployeeMaster inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join DepartmentmasterMNC on EmployeeMaster.DeptID=DepartmentmasterMNC.id where EmployeeMaster.SuprviserId='" + ViewState["empid"] + "' and EmployeeMaster.Whid='" + ddlstore.SelectedValue + "' and Active=1";
        SqlCommand cmd12 = new SqlCommand(str12, con);
        SqlDataAdapter adpt12 = new SqlDataAdapter(cmd12);
        DataSet ds12 = new DataSet();
        adpt12.Fill(ds12);
        ddlemployee.DataSource = ds12;
        ddlemployee.DataTextField = "EmployeeName";
        ddlemployee.DataValueField = "EmployeeMasterID";
        ddlemployee.DataBind();
        ddlemployee.Items.Insert(0, "-Select-");
        ddlemployee.Items[0].Value = "0";
    }
    protected void filteremp()
    {
        string str = "Select EmployeeMaster.EmployeeMasterID,EmployeeMaster.DeptID,EmployeeMaster.DesignationMasterId,EmployeeMaster.Whid,EmployeeMaster.EmployeeName,DepartmentmasterMNC.id,DesignationMaster.DesignationMasterId from EmployeeMaster inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join DepartmentmasterMNC on EmployeeMaster.DeptID=DepartmentmasterMNC.id where EmployeeMaster.SuprviserId='" + ViewState["empid"] + "' and Active=1";
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            str += "and EmployeeMaster.Whid='" + ddlsearchByStore.SelectedValue + "' ";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt12 = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt12.Fill(ds);
        ddlfilteremployee.DataSource = ds;
        ddlfilteremployee.DataTextField = "EmployeeName";
        ddlfilteremployee.DataValueField = "EmployeeMasterID";
        ddlfilteremployee.DataBind();
        ddlfilteremployee.Items.Insert(0, "All");
        ddlfilteremployee.Items[0].Value = "0";
    }
    protected void fillpolicy()
    {
        ddlpolicytitle.Items.Clear();
        //ddlfilterpolicy.Items.Clear();
        string str = "Select PolicyAddManagetbl.Id,LEFT(Policyprocedureruletiletbl.PolicyTitle,20) + ' : ' + LEFT(PolicyAddManagetbl.PolicyName,40) as PolicyName from PolicyAddManagetbl inner join Policyprocedureruletiletbl on PolicyAddManagetbl.PolicyId=Policyprocedureruletiletbl.Id where PolicyAddManagetbl.Whid='" + ddlstore.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ddlpolicytitle.DataSource = ds;
        ddlpolicytitle.DataTextField = "PolicyName";
        ddlpolicytitle.DataValueField = "Id";
        ddlpolicytitle.DataBind();
        ddlpolicytitle.Items.Insert(0, "-Select-");
        ddlpolicytitle.Items[0].Value = "0";

    }
    protected void fillfilterpolicy()
    {
        ddlfilterpolicy.Items.Clear();
        string str = "Select PolicyAddManagetbl.Id,LEFT(Policyprocedureruletiletbl.PolicyTitle,20) + ' : ' + LEFT(PolicyAddManagetbl.PolicyName,40) as PolicyName from PolicyAddManagetbl inner join Policyprocedureruletiletbl on PolicyAddManagetbl.PolicyId=Policyprocedureruletiletbl.Id where PolicyAddManagetbl.Compid='" + Session["Comid"] + "'";
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            str += "and PolicyAddManagetbl.Whid = '" + ddlsearchByStore.SelectedValue + "'";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ddlfilterpolicy.DataSource = ds;
        ddlfilterpolicy.DataTextField = "PolicyName";
        ddlfilterpolicy.DataValueField = "Id";
        ddlfilterpolicy.DataBind();
        ddlfilterpolicy.Items.Insert(0, "All");
        ddlfilterpolicy.Items[0].Value = "0";
    }
    protected void fillprocedures()
    {
        ddlproceduretitle.Items.Clear();
        //ddlfilterprocedure.Items.Clear();
        string str = "Select ProcedureAddManagetbl.Id,LEFT(Procedureforpolicytbl.ProcedureTitle,20) + ' : ' + LEFT(ProcedureAddManagetbl.ProcedureName,40) as ProcedureName from ProcedureAddManagetbl inner join Procedureforpolicytbl on ProcedureAddManagetbl.ProcedureId=Procedureforpolicytbl.Id where ProcedureAddManagetbl.Whid='" + ddlstore.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ddlproceduretitle.DataSource = ds;
        ddlproceduretitle.DataTextField = "ProcedureName";
        ddlproceduretitle.DataValueField = "Id";
        ddlproceduretitle.DataBind();
        ddlproceduretitle.Items.Insert(0, "-Select-");
        ddlproceduretitle.Items[0].Value = "0";
    }
    protected void fillfilterprocedures()
    {
        ddlfilterprocedure.Items.Clear();
        string str = "Select ProcedureAddManagetbl.Id,LEFT(Procedureforpolicytbl.ProcedureTitle,20) + ' : ' + LEFT(ProcedureAddManagetbl.ProcedureName,40) as ProcedureName from ProcedureAddManagetbl inner join Procedureforpolicytbl on ProcedureAddManagetbl.ProcedureId=Procedureforpolicytbl.Id where ProcedureAddManagetbl.Compid='" + Session["Comid"] + "'";
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            str += "and ProcedureAddManagetbl.Whid='" + ddlstore.SelectedValue + "'";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ddlfilterprocedure.DataSource = ds;
        ddlfilterprocedure.DataTextField = "ProcedureName";
        ddlfilterprocedure.DataValueField = "Id";
        ddlfilterprocedure.DataBind();
        ddlfilterprocedure.Items.Insert(0, "All");
        ddlfilterprocedure.Items[0].Value = "0";
    }
    protected void fillrules()
    {
        ddlruletitle.Items.Clear();
        //ddlfilterrule.Items.Clear();
        string str = "Select RuleAddManagetbl.Id,LEFT(Ruleforpolicytbl.RuleTitle,20) + ' : ' + LEFT(RuleAddManagetbl.RuleName,40) as RuleName from RuleAddManagetbl inner join Ruleforpolicytbl on RuleAddManagetbl.RuleId=Ruleforpolicytbl.Id where RuleAddManagetbl.Whid='" + ddlstore.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ddlruletitle.DataSource = ds;
        ddlruletitle.DataTextField = "RuleName";
        ddlruletitle.DataValueField = "Id";
        ddlruletitle.DataBind();
        ddlruletitle.Items.Insert(0, "-Select-");
        ddlruletitle.Items[0].Value = "0";


    }
    protected void fillfilterrules()
    {

        ddlfilterrule.Items.Clear();
        string str = "Select RuleAddManagetbl.Id,LEFT(Ruleforpolicytbl.RuleTitle,20) + ' : ' + LEFT(RuleAddManagetbl.RuleName,40) as RuleName from RuleAddManagetbl inner join Ruleforpolicytbl on RuleAddManagetbl.RuleId=Ruleforpolicytbl.Id where RuleAddManagetbl.Compid='" + Session["Comid"] + "'";
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            str += "and RuleAddManagetbl.Whid='" + ddlstore.SelectedValue + "'";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ddlfilterrule.DataSource = ds;
        ddlfilterrule.DataTextField = "RuleName";
        ddlfilterrule.DataValueField = "Id";
        ddlfilterrule.DataBind();
        ddlfilterrule.Items.Insert(0, "All");
        ddlfilterrule.Items[0].Value = "0";
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            pnlpolicy.Visible = false;
            Pnlprocedure.Visible = false;
            Pnlrule.Visible = false;
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            pnlpolicy.Visible = true;
            Pnlprocedure.Visible = false;
            Pnlrule.Visible = false;
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            pnlpolicy.Visible = false;
            Pnlprocedure.Visible = true;
            Pnlrule.Visible = false;
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            pnlpolicy.Visible = false;
            Pnlprocedure.Visible = false;
            Pnlrule.Visible = true;
        }
    }
    //protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (RadioButtonList2.SelectedValue == "1")
    //    {
    //        fillgrid();
    //        Pnlfilterpolicy.Visible = true;
    //        Pnlfilterprocedure.Visible = false;
    //        Pnlfilterrule.Visible = false;
    //    }
    //    if (RadioButtonList2.SelectedValue == "2")
    //    {
    //        fillgrid();
    //        Pnlfilterpolicy.Visible = false;
    //        Pnlfilterprocedure.Visible = true;
    //        Pnlfilterrule.Visible = false;
    //    }
    //    if (RadioButtonList2.SelectedValue == "3")
    //    {
    //        fillgrid();
    //        Pnlfilterpolicy.Visible = false;
    //        Pnlfilterprocedure.Visible = false;
    //        Pnlfilterrule.Visible = true;
    //    }
    //    if (RadioButtonList2.SelectedValue == "0")
    //    {
    //        fillgrid();
    //        Pnlfilterpolicy.Visible = false;
    //        Pnlfilterprocedure.Visible = false;
    //        Pnlfilterrule.Visible = false;

    //    }
    //}
    protected void fillDepartment()
    {
        string str = " select distinct DepartmentmasterMNC.* from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where DepartmentmasterMNC.Companyid='" + Session["Comid"] + "' ";
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            str += "and DepartmentmasterMNC.Whid='" + ddlsearchByStore.SelectedValue + "'";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlfilterdepartment.DataSource = dt;
        ddlfilterdepartment.DataTextField = "Departmentname";
        ddlfilterdepartment.DataValueField = "id";
        ddlfilterdepartment.DataBind();
        ddlfilterdepartment.Items.Insert(0, "All");
        ddlfilterdepartment.Items[0].Value = "0";
    }
    protected void ddlsearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDepartment();
        filteremp();
        fillfilterpolicy();
        fillfilterprocedures();
        fillfilterrules();
        fillgrid();
    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployee();
        fillpolicy();
        fillprocedures();
        fillrules();
    }
    //protected void Button2_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (!Pnl1.Visible)
    //    {
    //        Pnl1.Visible = true;
    //    }
    //    else
    //    {
    //        Pnl1.Visible = false;
    //    }
    //}
    protected void clearall()
    {
        ddlemployee.SelectedIndex = 0;
        txtdescription.Text = "";
        RadioButtonList1.SelectedValue = "0";
        pnlpolicy.Visible = false;
        Pnlprocedure.Visible = false;
        Pnlrule.Visible = false;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        btnupdate.Visible = false;
        btncancel.Visible = false;
        string str1 = "";
        string str = "";
        if (ddlemployee.SelectedIndex > 0)
        {

            if (RadioButtonList1.SelectedValue == "1")
            {
                str1 = "select Penaltypoint from PolicyAddManagetbl where EmployeeId='" + ddlemployee.SelectedValue + "' and Id='" + ddlpolicytitle.SelectedValue + "'";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adpt1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds1.Tables[0].Rows[0]["Penaltypoint"]) != "")
                    {
                        ViewState["penaltypoint"] = ds1.Tables[0].Rows[0]["Penaltypoint"].ToString();
                    }
                    else
                    {
                        ViewState["penaltypoint"] = "0";
                    }
                }
                str = "Insert into IncidenceAddManagetbl(EmpId,Date,Time,Timezone,PolicyNameId,ProcedureNameId,RuleNameId,IncidenceNote,whid,Compid,Penaltypoint)" +
                        " Values('" + ddlemployee.SelectedValue + "','" + txteenddate.Text + "','" + txttime.Text + "','" + ddltime.SelectedItem.Text + "','" + ddlpolicytitle.SelectedValue + "','0','0','" + txtdescription.Text + "','" + ddlstore.SelectedValue + "','" + ViewState["Compid"] + "','" + ViewState["penaltypoint"] + "')";

            }
            if (RadioButtonList1.SelectedValue == "2")
            {
                str1 = "select Penaltypoint from ProcedureAddManagetbl where EmployeeId='" + ddlemployee.SelectedValue + "' and Id='" + ddlproceduretitle.SelectedValue + "'";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adpt1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds1.Tables[0].Rows[0]["Penaltypoint"]) != "")
                    {
                        ViewState["penaltypoint"] = ds1.Tables[0].Rows[0]["Penaltypoint"].ToString();
                    }
                    else
                    {
                        ViewState["penaltypoint"] = "0";
                    }
                }
                str = "Insert into IncidenceAddManagetbl(EmpId,Date,Time,Timezone,PolicyNameId,ProcedureNameId,RuleNameId,IncidenceNote,whid,Compid,Penaltypoint)" +
                        " Values('" + ddlemployee.SelectedValue + "','" + txteenddate.Text + "','" + txttime.Text + "','" + ddltime.SelectedItem.Text + "','0','" + ddlproceduretitle.SelectedValue + "','0','" + txtdescription.Text + "','" + ddlstore.SelectedValue + "','" + ViewState["Compid"] + "','" + ViewState["penaltypoint"] + "')";

            }
            if (RadioButtonList1.SelectedValue == "3")
            {
                str1 = "select Penaltypoint from RuleAddManagetbl where EmployeeId='" + ddlemployee.SelectedValue + "' and Id='" + ddlruletitle.SelectedValue + "'";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adpt1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds1.Tables[0].Rows[0]["Penaltypoint"]) != "")
                    {
                        ViewState["penaltypoint"] = ds1.Tables[0].Rows[0]["Penaltypoint"].ToString();
                    }
                    else
                    {
                        ViewState["penaltypoint"] = "0";
                    }
                }
                str = "Insert into IncidenceAddManagetbl(EmpId,Date,Time,Timezone,PolicyNameId,ProcedureNameId,RuleNameId,IncidenceNote,whid,Compid,Penaltypoint)" +
                        " Values('" + ddlemployee.SelectedValue + "','" + txteenddate.Text + "','" + txttime.Text + "','" + ddltime.SelectedItem.Text + "','0','0','" + ddlruletitle.SelectedValue + "','" + txtdescription.Text + "','" + ddlstore.SelectedValue + "','" + ViewState["Compid"] + "','" + ViewState["penaltypoint"] + "')";

            }
            if (RadioButtonList1.SelectedValue == "0")
            {
                ViewState["penaltypoint"] = "0";

                str = "Insert into IncidenceAddManagetbl(EmpId,Date,Time,Timezone,PolicyNameId,ProcedureNameId,RuleNameId,IncidenceNote,whid,Compid,Penaltypoint)" +
                        " Values('" + ddlemployee.SelectedValue + "','" + txteenddate.Text + "','" + txttime.Text + "','" + ddltime.SelectedItem.Text + "','0','0','0','" + txtdescription.Text + "','" + ddlstore.SelectedValue + "','" + ViewState["Compid"] + "','" + ViewState["penaltypoint"] + "')";

            }

            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            string strid = "select MAX(Id) as ID from IncidenceAddManagetbl ";
            SqlCommand cmdid = new SqlCommand(strid, con);
            SqlDataAdapter adptid = new SqlDataAdapter(cmdid);
            DataSet dsid = new DataSet();
            adptid.Fill(dsid);
            ViewState["id"] = dsid.Tables[0].Rows[0]["ID"].ToString();
            string insert = "Insert into IncidenceDetailTbl(IncidenceMasterId,IncidenceNote) values ('" + ViewState["id"] + "','" + txtdescription.Text + "')";
            SqlCommand cmdinsert = new SqlCommand(insert, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdinsert.ExecuteNonQuery();
            con.Close();
            fillgrid();
            getversion();
            statuslable.Text = "Record inserted successfully";
            clearall();

            Pnladdnew.Visible = false;
            btnadd.Visible = true;
            lbllegend.Text = "";
        }
        else
        {
            statuslable.Text = "Please Select Employee";
        }
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        clearall();

        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
    }
    protected void fillgrid()
    {
        string str1 = "";
        string str = "";
        string str2 = "";

        lblBusiness.Text = ddlsearchByStore.SelectedItem.Text;

        if (DropDownList1.SelectedValue == "0")
        {
            str = "select distinct IncidenceAddManagetbl.*,IncidenceAddManagetbl.incidencenote as policyname,IncidenceDetailTbl.IncidenceEmpAnsNote,EmployeeMaster.EmployeeName from IncidenceAddManagetbl " +
                    "inner join IncidenceDetailTbl on IncidenceDetailTbl.IncidenceMasterId=IncidenceAddManagetbl.Id inner join EmployeeMaster on IncidenceAddManagetbl.EmpId=EmployeeMaster.EmployeeMasterID " +
                    " and IncidenceAddManagetbl.PolicyNameId='0' and IncidenceAddManagetbl.ProcedureNameId='0' and IncidenceAddManagetbl.RuleNameId='0' where IncidenceAddManagetbl.Compid='" + ViewState["Compid"] + "'";

            grid.Columns[5].Visible = false;
            //if (ddlfilterpolicy.SelectedIndex > 0)
            //{
            //    str1 += "and IncidenceAddManagetbl.PolicyNameId='" + ddlfilterpolicy.SelectedValue + "'";
            //}
            //if (ddlfilterdepartment.SelectedIndex > 0)
            //{
            //    str1 += "and Policybydefaulttbl.DepartmentId = '" + ddlfilterdepartment.SelectedValue + "' and Policybydefaulttbl.status='1'";
            //}

        }
        if (DropDownList1.SelectedValue == "1")
        {
            str = "select distinct IncidenceAddManagetbl.*,IncidenceDetailTbl.IncidenceEmpAnsNote,PolicyAddManagetbl.PolicyName as policyname,EmployeeMaster.EmployeeName,PolicyAddManagetbl.PolicyId,Policybydefaulttbl.PolicyMasterId " +
                            " from IncidenceAddManagetbl inner join IncidenceDetailTbl on IncidenceDetailTbl.IncidenceMasterId=IncidenceAddManagetbl.Id inner join PolicyAddManagetbl on PolicyAddManagetbl.Id = IncidenceAddManagetbl.PolicyNameId inner join Policybydefaulttbl on Policybydefaulttbl.PolicyMasterId=PolicyAddManagetbl.PolicyId inner join EmployeeMaster on IncidenceAddManagetbl.EmpId=EmployeeMaster.EmployeeMasterID where IncidenceAddManagetbl.Compid='" + ViewState["Compid"] + "'";

            if (ddlfilterpolicy.SelectedIndex > 0)
            {
                str1 += "and IncidenceAddManagetbl.PolicyNameId='" + ddlfilterpolicy.SelectedValue + "'";
            }
            if (ddlfilterdepartment.SelectedIndex > 0)
            {
                str1 += "and Policybydefaulttbl.DepartmentId = '" + ddlfilterdepartment.SelectedValue + "' and Policybydefaulttbl.status='1'";
            }
            grid.Columns[5].Visible = true;
        }
        if (DropDownList1.SelectedValue == "2")
        {
            str = "select distinct IncidenceAddManagetbl.*,IncidenceDetailTbl.IncidenceEmpAnsNote,ProcedureAddManagetbl.ProcedureName as policyname,EmployeeMaster.EmployeeName,ProcedureAddManagetbl.ProcedureId,Procedurebydefaulttbl.ProcedureMasterId " +
                       " from IncidenceAddManagetbl inner join IncidenceDetailTbl on IncidenceDetailTbl.IncidenceMasterId=IncidenceAddManagetbl.Id inner join ProcedureAddManagetbl on ProcedureAddManagetbl.Id = IncidenceAddManagetbl.ProcedureNameId inner join Procedurebydefaulttbl on Procedurebydefaulttbl.ProcedureMasterId=ProcedureAddManagetbl.ProcedureId inner join EmployeeMaster on IncidenceAddManagetbl.EmpId=EmployeeMaster.EmployeeMasterID where IncidenceAddManagetbl.Compid='" + ViewState["Compid"] + "'";

            if (ddlfilterprocedure.SelectedIndex > 0)
            {
                str1 += "and IncidenceAddManagetbl.ProcedureNameId = '" + ddlfilterprocedure.SelectedValue + "'";
            }
            if (ddlfilterdepartment.SelectedIndex > 0)
            {
                str1 += "and Procedurebydefaulttbl.DepartmentId = '" + ddlfilterdepartment.SelectedValue + "' and Policybydefaulttbl.status='1'";
            }
            grid.Columns[5].Visible = true;
        }
        if (DropDownList1.SelectedValue == "3")
        {
            str = " select distinct IncidenceAddManagetbl.*,IncidenceDetailTbl.IncidenceEmpAnsNote,RuleAddManagetbl.RuleName as policyname,EmployeeMaster.EmployeeName,Rulebydefaulttbl.RuleMasterId,RuleAddManagetbl.RuleId" +
                      " from IncidenceAddManagetbl inner join IncidenceDetailTbl on IncidenceDetailTbl.IncidenceMasterId=IncidenceAddManagetbl.Id inner join  RuleAddManagetbl on RuleAddManagetbl.Id = IncidenceAddManagetbl.RuleNameId inner join Rulebydefaulttbl on Rulebydefaulttbl.RuleMasterId=RuleAddManagetbl.RuleId  inner join EmployeeMaster on IncidenceAddManagetbl.EmpId=EmployeeMaster.EmployeeMasterID where IncidenceAddManagetbl.Compid='" + ViewState["Compid"] + "'";

            if (ddlfilterrule.SelectedIndex > 0)
            {
                str1 += "and IncidenceAddManagetbl.RuleNameId = '" + ddlfilterrule.SelectedValue + "'";
            }
            if (ddlfilterdepartment.SelectedIndex > 0)
            {
                str1 += "and Rulebydefaulttbl.DepartmentId = '" + ddlfilterdepartment.SelectedValue + "' and Rulebydefaulttbl.status='1'";
            }
            grid.Columns[5].Visible = true;
        }
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            str1 += "and IncidenceAddManagetbl.whid = '" + ddlsearchByStore.SelectedValue + "'";
        }
        if (ddlfilteremployee.SelectedIndex > 0)
        {
            str1 += "and IncidenceAddManagetbl.EmpId='" + ddlfilteremployee.SelectedValue + "'";
            Int32 point = 0;
            if (ddlperiod.SelectedValue == "1")
            {
                Panel4.Visible = true;
                Panel5.Visible = false;
                str2 = " select Penaltypoint from IncidenceAddManagetbl where EmpId='" + ddlfilteremployee.SelectedValue + "' and Month(Date) = '" + System.DateTime.Now.Month.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(str2, con);
                SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adpt1.Fill(ds1);
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    point = point + Convert.ToInt32(ds1.Tables[0].Rows[i]["Penaltypoint"].ToString());
                }
                lblptm.Text = point.ToString();
            }
            else if (ddlperiod.SelectedValue == "2")
            {
                Panel4.Visible = false;
                Panel5.Visible = true;
                str2 = "select Penaltypoint from IncidenceAddManagetbl where EmpId='" + ddlfilteremployee.SelectedValue + "' and Year(Date) = '" + System.DateTime.Now.Year.ToString() + "'";

                SqlCommand cmd1 = new SqlCommand(str2, con);
                SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adpt1.Fill(ds1);
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    point = point + Convert.ToInt32(ds1.Tables[0].Rows[i]["Penaltypoint"].ToString());
                }
                lblpty.Text = point.ToString();
            }
        }
        if (txtfromdt.Text != "" && txttodt.Text != "")
        {
            str1 += "and IncidenceAddManagetbl.Date between '" + Convert.ToDateTime(txtfromdt.Text) + "' and '" + Convert.ToDateTime(txttodt.Text) + "'";
        }
        if (ddlperiod.Visible == true)
        {
            if (ddlperiod.SelectedValue == "1")
            {
                if (txtfilterpoint.Text != "")
                {
                    str1 += "and IncidenceAddManagetbl.Penaltypoint > '" + txtfilterpoint.Text + "' and Month(IncidenceAddManagetbl.Date) = '" + System.DateTime.Now.Month.ToString() + "'";
                }
            }
            else if (ddlperiod.SelectedValue == "2")
            {
                if (txtfilterpoint.Text != "")
                {
                    str1 += "and IncidenceAddManagetbl.Penaltypoint > '" + txtfilterpoint.Text + "' and Year(IncidenceAddManagetbl.Date) = '" + System.DateTime.Now.Year.ToString() + "'";
                }
            }
        }

        str = str + str1;
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        DataView dv = new DataView();
        dv = dt.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            dv.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        grid.DataSource = dv;
        grid.DataBind();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        btnupdate.Visible = false;
        btncancel.Visible = false;
        string str1 = "";
        string str = "";
        if (ddlemployee.SelectedIndex > 0)
        {

            if (RadioButtonList1.SelectedValue == "1")
            {
                str1 = "select Penaltypoint from PolicyAddManagetbl where EmployeeId='" + ddlemployee.SelectedValue + "' and Id='" + ddlpolicytitle.SelectedValue + "'";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adpt1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds1.Tables[0].Rows[0]["Penaltypoint"]) != "")
                    {
                        ViewState["penaltypoint"] = ds1.Tables[0].Rows[0]["Penaltypoint"].ToString();
                    }
                    else
                    {
                        ViewState["penaltypoint"] = "0";
                    }
                }
                str = "Update IncidenceAddManagetbl set EmpId = '" + ddlemployee.SelectedValue + "',Date = '" + txteenddate.Text + "',Time = '" + txttime.Text + "',Timezone='" + ddltime.SelectedItem.Text + "',PolicyNameId='" + ddlpolicytitle.SelectedValue + "',ProcedureNameId='0',RuleNameId='0',IncidenceNote='" + txtdescription.Text + "',whid='" + ddlstore.SelectedValue + "',Compid='" + ViewState["Compid"] + "' where Id='" + ViewState["editid"] + "'";

            }
            if (RadioButtonList1.SelectedValue == "2")
            {
                str1 = "select Penaltypoint from ProcedureAddManagetbl where EmployeeId='" + ddlemployee.SelectedValue + "' and Id='" + ddlproceduretitle.SelectedValue + "'";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adpt1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds1.Tables[0].Rows[0]["Penaltypoint"]) != "")
                    {
                        ViewState["penaltypoint"] = ds1.Tables[0].Rows[0]["Penaltypoint"].ToString();
                    }
                    else
                    {
                        ViewState["penaltypoint"] = "0";
                    }
                }
                str = "Update IncidenceAddManagetbl set EmpId = '" + ddlemployee.SelectedValue + "',Date = '" + txteenddate.Text + "',Time = '" + txttime.Text + "',Timezone='" + ddltime.SelectedItem.Text + "',PolicyNameId='0',ProcedureNameId='" + ddlproceduretitle.SelectedValue + "',RuleNameId='0',IncidenceNote='" + txtdescription.Text + "',whid='" + ddlstore.SelectedValue + "',Compid='" + ViewState["Compid"] + "' where Id='" + ViewState["editid"] + "'";

            }
            if (RadioButtonList1.SelectedValue == "3")
            {
                str1 = "select Penaltypoint from RuleAddManagetbl where EmployeeId='" + ddlemployee.SelectedValue + "' and Id='" + ddlruletitle.SelectedValue + "'";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adpt1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds1.Tables[0].Rows[0]["Penaltypoint"]) != "")
                    {
                        ViewState["penaltypoint"] = ds1.Tables[0].Rows[0]["Penaltypoint"].ToString();
                    }
                    else
                    {
                        ViewState["penaltypoint"] = "0";
                    }
                }
                str = "Update IncidenceAddManagetbl set EmpId = '" + ddlemployee.SelectedValue + "',Date = '" + txteenddate.Text + "',Time = '" + txttime.Text + "',Timezone='" + ddltime.SelectedItem.Text + "',PolicyNameId='0',ProcedureNameId='0',RuleNameId='" + ddlruletitle.SelectedValue + "',IncidenceNote='" + txtdescription.Text + "',whid='" + ddlstore.SelectedValue + "',Compid='" + ViewState["Compid"] + "' where Id='" + ViewState["editid"] + "'";

            }
            if (RadioButtonList1.SelectedValue == "0")
            {
                ViewState["penaltypoint"] = "0";
                str = "Update IncidenceAddManagetbl set EmpId = '" + ddlemployee.SelectedValue + "',Date = '" + txteenddate.Text + "',Time = '" + txttime.Text + "',Timezone='" + ddltime.SelectedItem.Text + "',PolicyNameId='0',ProcedureNameId='0',RuleNameId='0',IncidenceNote='" + txtdescription.Text + "',whid='" + ddlstore.SelectedValue + "',Compid='" + ViewState["Compid"] + "' where Id='" + ViewState["editid"] + "'";

            }
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            string update = "Update IncidenceDetailTbl set IncidenceMasterId = '" + ViewState["editid"] + "' ,IncidenceNote = '" + txtdescription.Text + "' where IncidenceMasterId = '" + ViewState["editid"] + "'";
            SqlCommand cmdupdate = new SqlCommand(update, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdupdate.ExecuteNonQuery();
            con.Close();
            fillgrid();
            statuslable.Text = "Record updated successfully";
            clearall();
        }
        btnsubmit.Visible = true;
        btnreset.Visible = true;

        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearall();
        btnsubmit.Visible = true;
        btnreset.Visible = true;

        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");
            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;

            if (grid.Columns[8].Visible == true)
            {
                ViewState["edith"] = "tt";
                grid.Columns[8].Visible = false;
            }
            if (grid.Columns[9].Visible == true)
            {
                ViewState["deleteh"] = "tt";
                grid.Columns[9].Visible = false;
            }
            if (grid.Columns[10].Visible == true)
            {
                ViewState["viewm"] = "tt";
                grid.Columns[10].Visible = false;
            }
        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;

            if (ViewState["edith"] != null)
            {
                grid.Columns[8].Visible = true;
            }
            if (ViewState["deleteh"] != null)
            {
                grid.Columns[9].Visible = true;
            }
            if (ViewState["viewm"] != null)
            {
                grid.Columns[10].Visible = true;
            }
        }
    }
    protected void ddlfilteremployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel7.Visible = true;
        Panel4.Visible = true;
        Panel5.Visible = false;

        fillgrid();
    }
    protected void ddlfilterpolicy_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlfilterprocedure_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlfilterrule_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlfilterdepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
      
    }
    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ViewState["deleteid"] = grid.DataKeys[e.RowIndex].Value.ToString();
        string str = "delete from IncidenceAddManagetbl where Id='" + ViewState["deleteid"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        string strdelete = "delete from IncidenceDetailTbl where IncidenceMasterId='" + ViewState["deleteid"] + "'";
        SqlCommand cmddelete = new SqlCommand(strdelete, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmddelete.ExecuteNonQuery();
        con.Close();
        fillgrid();
        statuslable.Text = "Record deleted successfully";
    }
    protected void txttodt_TextChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlperiod_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlfilteremployee.SelectedIndex > 0)
        {
            if (ddlperiod.SelectedValue == "1")
            {
                Panel7.Visible = true;
                Panel4.Visible = true;
                Panel5.Visible = false;
            }
            if (ddlperiod.SelectedValue == "2")
            {
                Panel7.Visible = true;
                Panel4.Visible = false;
                Panel5.Visible = true;
            }
        }

        else if (ddlperiod.SelectedIndex == 0)
        {
            Panel7.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
        }
        fillgrid();
    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "Incidence_Profile.aspx?id=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }

        if (e.CommandName == "Edit")
        {
            Pnladdnew.Visible = true;
            lbllegend.Text = "Edit Incident";
            statuslable.Text = "";

            //ViewState["editid"] = grid.DataKeys[e.NewEditIndex].Value.ToString();

            ViewState["editid"] = Convert.ToInt32(e.CommandArgument);

            string str = "Select * from IncidenceAddManagetbl where Id='" + ViewState["editid"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            fillstore();
            ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dt.Rows[0]["whid"].ToString()));

            lblincidence.Text = dt.Rows[0]["Id"].ToString();

            fillemployee();
            ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dt.Rows[0]["EmpId"].ToString()));

            txteenddate.Text = dt.Rows[0]["Date"].ToString();
            txttime.Text = dt.Rows[0]["Time"].ToString();
            ddltime.SelectedItem.Text = dt.Rows[0]["Timezone"].ToString();

            if (dt.Rows[0]["PolicyNameId"].ToString() != "0")
            {
                RadioButtonList1.SelectedValue = "1";
                pnlpolicy.Visible = true;
                fillpolicy();
                ddlpolicytitle.SelectedIndex = ddlpolicytitle.Items.IndexOf(ddlpolicytitle.Items.FindByValue(dt.Rows[0]["PolicyNameId"].ToString()));
            }
            if (dt.Rows[0]["ProcedureNameId"].ToString() != "0")
            {
                RadioButtonList1.SelectedValue = "2";
                Pnlprocedure.Visible = true;
                fillprocedures();
                ddlproceduretitle.SelectedIndex = ddlproceduretitle.Items.IndexOf(ddlproceduretitle.Items.FindByValue(dt.Rows[0]["ProcedureNameId"].ToString()));
            }
            if (dt.Rows[0]["RuleNameId"].ToString() != "0")
            {
                RadioButtonList1.SelectedValue = "3";
                Pnlrule.Visible = true;
                fillrules();
                ddlruletitle.SelectedIndex = ddlruletitle.Items.IndexOf(ddlruletitle.Items.FindByValue(dt.Rows[0]["RuleNameId"].ToString()));
            }
            if (dt.Rows[0]["IncidenceNote"].ToString() != " ")
            {
                //Pnl1.Visible = true;
                txtdescription.Text = dt.Rows[0]["IncidenceNote"].ToString();
            }
            btnsubmit.Visible = false;
            btnreset.Visible = false;
            btnupdate.Visible = true;
            btncancel.Visible = true;
        }
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
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Pnladdnew.Visible = true;
        btnadd.Visible = false;
        lbllegend.Text = "Add New Incident";
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "1")
        {
            fillgrid();
            Pnlfilterpolicy.Visible = true;
            Pnlfilterprocedure.Visible = false;
            Pnlfilterrule.Visible = false;
        }
        if (DropDownList1.SelectedValue == "2")
        {
            fillgrid();
            Pnlfilterpolicy.Visible = false;
            Pnlfilterprocedure.Visible = true;
            Pnlfilterrule.Visible = false;
        }
        if (DropDownList1.SelectedValue == "3")
        {
            fillgrid();
            Pnlfilterpolicy.Visible = false;
            Pnlfilterprocedure.Visible = false;
            Pnlfilterrule.Visible = true;
        }
        if (DropDownList1.SelectedValue == "0")
        {
            fillgrid();
            Pnlfilterpolicy.Visible = false;
            Pnlfilterprocedure.Visible = false;
            Pnlfilterrule.Visible = false;

        }
    }
    protected void txtfilterpoint_TextChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
