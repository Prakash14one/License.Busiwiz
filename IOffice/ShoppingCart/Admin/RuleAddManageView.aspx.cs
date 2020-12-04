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

public partial class ShoppingCart_Admin_RuleAddManage : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            fillstore();
            
            fillruletitle();
            lblcmpny.Text = Session["comid"].ToString();
            lblBusiness.Text = "All";
            filterstore();
            ddlfilterstore_SelectedIndexChanged(sender, e);

            fillDepartment();
            fillDesignation();
            fillemployee();
            //ddlfilterpolicyprocedure.Items.Insert(0, "All");
            //ddlfilterpolicyprocedure.Items[0].Value = "0";
            ddlfilterpolicy.Items.Insert(0, "All");
            ddlfilterpolicy.Items[0].Value = "0";
            ddlfilterruletitle.Items.Insert(0, "All");
            ddlfilterruletitle.Items[0].Value = "0";

            fillgrid();

        }
    }
    protected void getversion()
    {
        string str = "select MAX(Version) as Version from RuleAddManagetbl where RuleAddManagetbl.RuleId='" + ddlruletitle.SelectedValue + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ViewState["version"] = ds.Tables[0].Rows[0]["Version"].ToString();
        if (ViewState["version"].ToString() != null && ViewState["version"].ToString() != "")
        {
            Int32 version = 0;
            version = Convert.ToInt32(ds.Tables[0].Rows[0]["Version"].ToString());
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
     //   DataTable ds = ClsStore.SelectStorename();
        string str = " SELECT dbo.DepartmentmasterMNC.id, dbo.WareHouseMaster.WareHouseId, dbo.WareHouseMaster.Name FROM  dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid INNER JOIN dbo.EmployeeMaster ON dbo.DepartmentmasterMNC.id = dbo.EmployeeMaster.DeptID where EmployeeMaster.EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "' ";
        SqlCommand cmdwh = new SqlCommand(str, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable ds = new DataTable();
        adpwh.Fill(ds);
        ddlstore.DataSource = ds;
        ddlstore.DataTextField = "Name";
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataBind();
        ddlstore.Enabled = false;  
        //DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        //if (dteeed.Rows.Count > 0)
        //{
        //    ddlstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        //}

    }
    protected void filterstore()
    {
        ddlfilterstore.Items.Clear();
       // DataTable ds = ClsStore.SelectStorename();
        string str = " SELECT dbo.DepartmentmasterMNC.id, dbo.WareHouseMaster.WareHouseId, dbo.WareHouseMaster.Name FROM  dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid INNER JOIN dbo.EmployeeMaster ON dbo.DepartmentmasterMNC.id = dbo.EmployeeMaster.DeptID where EmployeeMaster.EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "' ";
        SqlCommand cmdwh = new SqlCommand(str, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable ds = new DataTable();
        adpwh.Fill(ds);
        ddlfilterstore.DataSource = ds;
        ddlfilterstore.DataTextField = "Name";
        ddlfilterstore.DataValueField = "WareHouseId";
        ddlfilterstore.DataBind();
        ddlfilterstore.Enabled = false;  
        //DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        //if (dteeed.Rows.Count > 0)
        //{
        //    ddlfilterstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        //}

        //ddlfilterstore.Items.Insert(0, "All");
        //ddlfilterstore.Items[0].Value = "0";
    }
    protected void fillruletitle()
    {
        ddlruletitle.Items.Clear();
        string str = "Select * from Ruleforpolicytbl where Whid='" + ddlstore.SelectedValue + "' and Compid='" + Session["Comid"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ddlruletitle.DataSource = ds;
        ddlruletitle.DataTextField = "RuleTitle";
        ddlruletitle.DataValueField = "Id";
        ddlruletitle.DataBind();
        ddlruletitle.Items.Insert(0, "-Select-");
        ddlruletitle.Items[0].Value = "0";
    }
    protected void filterruletitle()
    {
        ddlfilterruletitle.Items.Clear();
        string str = "Select * from Ruleforpolicytbl where Compid='" + Session["Comid"] + "'";
        if (ddlfilterpolicy.SelectedIndex > 0)
        {
            str += "and Ruleforpolicytbl.PolicyId='" + ddlfilterpolicy.SelectedValue + "'";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ddlfilterruletitle.DataSource = ds;
        ddlfilterruletitle.DataTextField = "RuleTitle";
        ddlfilterruletitle.DataValueField = "Id";
        ddlfilterruletitle.DataBind();
        ddlfilterruletitle.Items.Insert(0, "All");
        ddlfilterruletitle.Items[0].Value = "0";
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
        fillruletitle();
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
    protected void fillfilterpolicy()
    {
        ddlfilterpolicyprocedure.Items.Clear();
        string st1 = "select * from Policyprocedureruletbl where compid='" + Session["Comid"] + "'  ";

        if (ddlfilterstore.SelectedIndex > 0)
        {
            st1 += " and Policyprocedureruletbl.Whid='" + ddlfilterstore.SelectedValue + "'";
        }

        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

        ddlfilterpolicyprocedure.DataSource = dt1;
        ddlfilterpolicyprocedure.DataTextField = "Policyprocedurecategory";
        ddlfilterpolicyprocedure.DataValueField = "Id";
        ddlfilterpolicyprocedure.DataBind();

        ddlfilterpolicyprocedure.Items.Insert(0, "All");
        ddlfilterpolicyprocedure.Items[0].Value = "0";

    }
    protected void fillfilterpolicytitle()
    {
        ddlfilterpolicy.Items.Clear();
        string st1 = "select * from Policyprocedureruletiletbl where compid='" + Session["Comid"] + "'  ";

        if (ddlfilterstore.SelectedIndex > 0)
        {
            st1 += " and Policyprocedureruletiletbl.Policyprocedureruleid='" + ddlfilterpolicyprocedure.SelectedValue + "'";
        }

        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        ddlfilterpolicy.DataSource = dt1;
        ddlfilterpolicy.DataTextField = "PolicyTitle";
        ddlfilterpolicy.DataValueField = "Id";
        ddlfilterpolicy.DataBind();

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
        string strinsert;
        //if (chkstatus.Checked)
        //{
        //    strinsert = "Insert into RuleAddManagetbl(RuleId,Version,RuleName,RuleDescription,Startdate,Enddate,Status,Whid,Compid,DesignationId,EmployeeId,Penaltypoint)" +
        //                    "values('" + ddlruletitle.SelectedValue + "','" + lblversion.Text + "','" + txtrulename.Text + "','" + txtdescription.Text + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','1','" + ddlstore.SelectedValue + "','" + Session["Comid"] + "','" + ddldesignation.SelectedValue + "','" + ddlemployee.SelectedValue + "','" + txtpenaltypoint.Text + "')";
        //}
        //else
        //{
        //    strinsert = "Insert into RuleAddManagetbl(RuleId,Version,RuleName,RuleDescription,Startdate,Enddate,Status,Whid,Compid,DesignationId,EmployeeId,Penaltypoint)" +
        //                    "values('" + ddlruletitle.SelectedValue + "','" + lblversion.Text + "','" + txtrulename.Text + "','" + txtdescription.Text + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','0','" + ddlstore.SelectedValue + "','" + Session["Comid"] + "','" + ddldesignation.SelectedValue + "','" + ddlemployee.SelectedValue + "','" + txtpenaltypoint.Text + "')";
        //}
        // SqlCommand cmd = new SqlCommand(strinsert, con);

        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("insertruleaddmanage", con);

        cmd.Parameters.AddWithValue("@RuleId", ddlruletitle.SelectedValue);
        cmd.Parameters.AddWithValue("@RuleName", txtrulename.Text);
        cmd.Parameters.AddWithValue("@RuleDescription", txtdescription.Text);
        cmd.Parameters.AddWithValue("@Version", lblversion.Text);
        cmd.Parameters.AddWithValue("@Startdate", txtestartdate.Text);
        cmd.Parameters.AddWithValue("@Enddate", txteenddate.Text);
        cmd.Parameters.AddWithValue("@Status", 0);
        cmd.Parameters.AddWithValue("@Whid", ddlstore.SelectedValue);
        cmd.Parameters.AddWithValue("@Compid", Session["Comid"].ToString());
        cmd.Parameters.AddWithValue("@DesignationId", ddldesignation.SelectedValue);
        cmd.Parameters.AddWithValue("@EmployeeId", ddlemployee.SelectedValue);
        cmd.Parameters.AddWithValue("@Penaltypoint", txtpenaltypoint.Text);

        if (chkstatus.Checked)
        {
            cmd.Parameters.AddWithValue("@flag", "a");
        }
        else
        {
            cmd.Parameters.AddWithValue("@flag", "b");
        }

        cmd.CommandType = CommandType.StoredProcedure;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();


        statuslable.Text = "Record inserted successfully";
        clearall();
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
        //if (chkstatus.Checked)
        //{
        //    strupdate = "Update RuleAddManagetbl set RuleId='" + ddlruletitle.SelectedValue + "',Version='" + lblversion.Text + "',RuleName='" + txtrulename.Text + "',RuleDescription='" + txtdescription.Text + "',Startdate='" + txtestartdate.Text + "',Enddate='" + txteenddate.Text + "',Status='1',Whid='" + ddlstore.SelectedValue + "',Compid='" + Session["Comid"] + "',DesignationId='" + ddldesignation.SelectedValue + "',EmployeeId='" + ddlemployee.SelectedValue + "',Penaltypoint='" + txtpenaltypoint.Text + "' where Id='" + ViewState["id"] + "'";

        //}
        //else
        //{
        //    strupdate = "Update RuleAddManagetbl set RuleId='" + ddlruletitle.SelectedValue + "',Version='" + lblversion.Text + "',RuleName='" + txtrulename.Text + "',RuleDescription='" + txtdescription.Text + "',Startdate='" + txtestartdate.Text + "',Enddate='" + txteenddate.Text + "',Status='0',Whid='" + ddlstore.SelectedValue + "',Compid='" + Session["Comid"] + "',DesignationId='" + ddldesignation.SelectedValue + "',EmployeeId='" + ddlemployee.SelectedValue + "',Penaltypoint='" + txtpenaltypoint.Text + "' where Id='" + ViewState["id"] + "'";
        //}
        //SqlCommand cmd = new SqlCommand(strupdate, con);

        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("updateruleaddmanage", con);

        cmd.Parameters.AddWithValue("@RuleId", ddlruletitle.SelectedValue);
        cmd.Parameters.AddWithValue("@RuleName", txtrulename.Text);
        cmd.Parameters.AddWithValue("@RuleDescription", txtdescription.Text);
        cmd.Parameters.AddWithValue("@Version", lblversion.Text);
        cmd.Parameters.AddWithValue("@Startdate", txtestartdate.Text);
        cmd.Parameters.AddWithValue("@Enddate", txteenddate.Text);
        cmd.Parameters.AddWithValue("@Status", 0);
        cmd.Parameters.AddWithValue("@Whid", ddlstore.SelectedValue);
        cmd.Parameters.AddWithValue("@Compid", Session["Comid"].ToString());
        cmd.Parameters.AddWithValue("@DesignationId", ddldesignation.SelectedValue);
        cmd.Parameters.AddWithValue("@EmployeeId", ddlemployee.SelectedValue);
        cmd.Parameters.AddWithValue("@Penaltypoint", txtpenaltypoint.Text);
        cmd.Parameters.AddWithValue("@Id", ViewState["id"]);

        if (chkstatus.Checked)
        {
            cmd.Parameters.AddWithValue("@flag", "a");
        }
        else
        {
            cmd.Parameters.AddWithValue("@flag", "b");
        }

        cmd.CommandType = CommandType.StoredProcedure;

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
                    String strdept = "Insert Into Rulebydefaulttbl (RuleMasterId,version,DepartmentId,status)values('" + ddlruletitle.SelectedValue + "','" + lblversion.Text + "','" + lbldepartmasterid.Text + "','" + chkactive123.Checked + "')";

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
        ddlruletitle.SelectedIndex = 0;
        txtrulename.Text = "";
        txtdescription.Text = "";
        lblversion.Text = "";
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
        string st2 = "";

        if (ddlfilterstore.SelectedIndex > 0)
        {
            lblBusiness.Text = ddlfilterstore.SelectedItem.Text;
            st2 += " and RuleAddManagetbl.Whid='" + ddlfilterstore.SelectedValue + "'";
        }
        if (ddlfilterpolicy.SelectedIndex > 0)
        {
            st2 += " and Ruleforpolicytbl.PolicyId='" + ddlfilterpolicy.SelectedValue + "'";
        }
        if (ddlfilterpolicyprocedure.SelectedIndex > 0)
        {
            st2 += "and Ruleforpolicytbl.PolicyprocedurecategoryId='" + ddlfilterpolicyprocedure.SelectedValue + "'";
        }
        if (ddlfilterruletitle.SelectedIndex > 0)
        {
            st2 += "and RuleAddManagetbl.RuleId='" + ddlfilterruletitle.SelectedValue + "'";
        }
        if (showlatest.Checked)
        {
            string str12 = "select MAX(Version) as Version from RuleAddManagetbl where RuleAddManagetbl.Compid = '" + Session["Comid"] + "'";
            SqlCommand cmd12 = new SqlCommand(str12, con);
            SqlDataAdapter adpt12 = new SqlDataAdapter(cmd12);
            DataSet ds12 = new DataSet();
            adpt12.Fill(ds12);
            ViewState["version"] = ds12.Tables[0].Rows[0]["Version"].ToString();
            st2 += "and RuleAddManagetbl.Version='" + ViewState["version"] + "'";
        }


        string str = " RuleAddManagetbl.Id,RuleAddManagetbl.RuleId,RuleAddManagetbl.Version,RuleAddManagetbl.RuleName,RuleAddManagetbl.Status,RuleAddManagetbl.Whid,RuleAddManagetbl.Compid,RuleAddManagetbl.Penaltypoint,Ruleforpolicytbl.RuleTitle,Ruleforpolicytbl.PolicyprocedurecategoryId,Ruleforpolicytbl.PolicyId,Policyprocedureruletbl.Policyprocedurecategory from RuleAddManagetbl inner join Ruleforpolicytbl on RuleAddManagetbl.RuleId=Ruleforpolicytbl.Id inner join Policyprocedureruletbl on Policyprocedureruletbl.Id=Ruleforpolicytbl.PolicyprocedurecategoryId where  RuleAddManagetbl.Compid = '" + Session["Comid"] + "' " + st2 + " ";

        string str2 = " select Count(RuleAddManagetbl.Id) as ci  from RuleAddManagetbl inner join Ruleforpolicytbl on RuleAddManagetbl.RuleId=Ruleforpolicytbl.Id inner join Policyprocedureruletbl on Policyprocedureruletbl.Id=Ruleforpolicytbl.PolicyprocedurecategoryId where  RuleAddManagetbl.Compid = '" + Session["Comid"] + "' " + st2 + " ";

        grid.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " Policyprocedureruletbl.Policyprocedurecategory,Ruleforpolicytbl.RuleTitle,RuleAddManagetbl.RuleName";

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
        //ViewState["id"] = grid.DataKeys[e.NewEditIndex].Value.ToString();
        //string str = "select * from RuleAddManagetbl where Id='" + ViewState["id"] + "'";
        //SqlCommand cmd = new SqlCommand(str,con);
        //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adpt.Fill(dt);
        //btnupdate.Visible = true;
        //btncancel.Visible = true;
        //btnsubmit.Visible = false;
        //btnreset.Visible = false;
        //fillstore();
        //ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dt.Rows[0]["whid"].ToString()));
        //fillruletitle();
        //ddlruletitle.SelectedIndex = ddlruletitle.Items.IndexOf(ddlruletitle.Items.FindByValue(dt.Rows[0]["RuleId"].ToString()));
        //ViewState["rulemasterid"] = dt.Rows[0]["RuleId"].ToString();
        //lblversion.Text = dt.Rows[0]["Version"].ToString();
        //ViewState["getversion"] = dt.Rows[0]["Version"].ToString();
        //txtrulename.Text = dt.Rows[0]["RuleName"].ToString();
        //if (Convert.ToString(dt.Rows[0]["RuleDescription"]) != "")
        //{
        //    Button2.Checked = true;
        //    Pnl1.Visible = true;
        //    txtdescription.Text = Convert.ToString(dt.Rows[0]["RuleDescription"].ToString());;
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
        btnupdatepop1.Visible = true;
    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "View Policy";
            btnadd.Visible = false;
            statuslable.Text = "";
            Pnladdnew.Visible = true;
            Pnladdnew.Enabled = false;  

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["id"] = mm;

            //ViewState["id"] = grid.DataKeys[e.NewEditIndex].Value.ToString();


            string str = "select * from RuleAddManagetbl where Id='" + ViewState["id"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            btnupdate.Visible = false;
            btncancel.Visible = false;
            btnsubmit.Visible = false;
            btnreset.Visible = false;
            fillstore();
            ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dt.Rows[0]["whid"].ToString()));
            fillruletitle();
            ddlruletitle.SelectedIndex = ddlruletitle.Items.IndexOf(ddlruletitle.Items.FindByValue(dt.Rows[0]["RuleId"].ToString()));
            ViewState["rulemasterid"] = dt.Rows[0]["RuleId"].ToString();
            lblversion.Text = dt.Rows[0]["Version"].ToString();
            ViewState["getversion"] = dt.Rows[0]["Version"].ToString();
            txtrulename.Text = dt.Rows[0]["RuleName"].ToString();
            if (Convert.ToString(dt.Rows[0]["RuleDescription"]) != "")
            {
                Button2.Checked = true;
                Pnl1.Visible = true;
                txtdescription.Text = Convert.ToString(dt.Rows[0]["RuleDescription"].ToString()); ;
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
          //  ModalPopupExtenderAddnew.Show();

            btnsubmitpop1.Visible = false;
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
            string strdept = "Select * from Rulebydefaulttbl where DepartmentId='" + lbldepartmasterid.Text + "' and RuleMasterId='" + ViewState["rulemasterid"] + "' and version='" + ViewState["getversion"] + "'";
            SqlCommand cmddept = new SqlCommand(strdept, con);
            SqlDataAdapter addept = new SqlDataAdapter(cmddept);
            DataSet ds = new DataSet();
            addept.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                String strupdate = "Update Rulebydefaulttbl set DepartmentId='" + lbldepartmasterid.Text + "',status='" + chkactive123.Checked + "' where RuleMasterId='" + ViewState["rulemasterid"] + "' and version = '" + ViewState["getversion"] + "'";

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
                String strupdate = "insert into Rulebydefaulttbl (RuleMasterId,version,DepartmentId,status) values ('" + ViewState["rulemasterid"] + "','" + ViewState["getversion"] + "','" + lbldepartmasterid.Text + "','" + chkactive123.Checked + "')";

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
    }
    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ViewState["deleid"] = grid.DataKeys[e.RowIndex].Value.ToString();
        string str1 = "select Max(Id) as deleteid from RuleAddManagetbl";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adpt1.Fill(ds1);
        if (ViewState["deleid"].ToString() == ds1.Tables[0].Rows[0]["deleteid"].ToString())
        {
            string str = "delete from RuleAddManagetbl where Id='" + ViewState["deleid"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            statuslable.Text = "Record deleted successfully";
        }
        else
        {
            statuslable.Text = "You can not delete previous rule";
        }
        fillgrid();
    }
    protected void ddlfilterstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilterpolicy();
        fillgrid();
    }
    protected void ddlfilterpolicy_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterruletitle();
        fillgrid();
    }
    protected void ddlfilterpolicyprocedure_SelectedIndexChanged(object sender, EventArgs e)
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
        string te = "RuleTitleMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgrefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillruletitle();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        lbllegend.Text = "Add New Procedure";
        Pnladdnew.Visible = true;
        statuslable.Text = "";
        btnadd.Visible = false;
        Button2.Checked = false;
        Pnl1.Visible = false;

        txtestartdate.Text = System.DateTime.Now.ToShortDateString();
        txteenddate.Text = System.DateTime.Now.ToShortDateString();
    }
    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid.PageIndex = e.NewPageIndex;
        fillgrid();
    }
}
