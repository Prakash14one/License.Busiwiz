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

public partial class ShoppingCart_Admin_Incidence_Profile : System.Web.UI.Page
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
            txtfromdt.Text = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
            txttodt.Text = System.DateTime.Now.ToShortDateString();

            if (Request.QueryString["id"] != null)
            {
                fillgrid1();
                panelhide.Visible = true;
            }
            else
            {
                btngo.Visible = true;
                fillgrid();
            }

            fillmonthpoint();
            fillyearpoint();
            fillemployee();
            fillincidence();
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
        lblBusiness.Text = ddlstore.SelectedItem.Text;
    }
    protected void fillemployee()
    {
        ddlemployee.Items.Clear();
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
        //ddlemployee.Items.Insert(0, "-Select-");
        //ddlemployee.Items[0].Value = "0";
    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblBusiness.Text = ddlstore.SelectedItem.Text;

        fillemployee();
        fillincidence();
    }
    protected void fillincidence()
    {
        string str = "select distinct IncidenceAddManagetbl.Id,cast(IncidenceAddManagetbl.Id as nvarchar) +' : '+EmployeeMaster.EmployeeName +' : '+ LEFT(cast(IncidenceAddManagetbl.Date as nvarchar),11) as incidencename from IncidenceAddManagetbl inner join EmployeeMaster on IncidenceAddManagetbl.EmpId = EmployeeMaster.EmployeeMasterID where IncidenceAddManagetbl.Compid='" + ViewState["Compid"] + "' and IncidenceAddManagetbl.Date >='" + txtfromdt.Text + "' and IncidenceAddManagetbl.Date <='" + txttodt.Text + "' ";
        if (ddlstore.SelectedIndex > 0)
        {
            str += "and IncidenceAddManagetbl.whid='" + ddlstore.SelectedValue + "'";
        }
        if (ddlemployee.SelectedIndex > -1)
        {
            str += "and IncidenceAddManagetbl.whid='" + ddlstore.SelectedValue + "' and IncidenceAddManagetbl.EmpId='" + ddlemployee.SelectedValue + "'";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ddlincidence.DataSource = ds;
        ddlincidence.DataValueField = "Id";
        ddlincidence.DataTextField = "incidencename";
        ddlincidence.DataBind();
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillincidence();
        fillmonthpoint();
        fillyearpoint();
    }

    protected void fillgrid1()
    {
        string str = "";

        ddlstore.Enabled = false;
        ddlemployee.Enabled = false;

        int id = Convert.ToInt32(Request.QueryString["id"]);
        str = "select distinct IncidenceAddManagetbl.*,PolicyAddManagetbl.PolicyName,ProcedureAddManagetbl.ProcedureName,RuleAddManagetbl.RuleName,IncidenceDetailTbl.IncidenceEmpAnsNote,Policyprocedureruletiletbl.PolicyTitle,Procedureforpolicytbl.ProcedureTitle,Ruleforpolicytbl.RuleTitle,EmployeeMaster.EmployeeName " +
                " from IncidenceAddManagetbl inner join IncidenceDetailTbl on IncidenceDetailTbl.IncidenceMasterId=IncidenceAddManagetbl.Id left join PolicyAddManagetbl on PolicyAddManagetbl.Id = IncidenceAddManagetbl.PolicyNameId left join Policyprocedureruletiletbl on PolicyAddManagetbl.PolicyId=Policyprocedureruletiletbl.Id " +
                " left join ProcedureAddManagetbl on ProcedureAddManagetbl.Id = IncidenceAddManagetbl.ProcedureNameId left join Procedureforpolicytbl on ProcedureAddManagetbl.ProcedureId=Procedureforpolicytbl.Id left join  RuleAddManagetbl on RuleAddManagetbl.Id = IncidenceAddManagetbl.RuleNameId " +
                " left join Ruleforpolicytbl on RuleAddManagetbl.RuleId=Ruleforpolicytbl.Id inner join EmployeeMaster on IncidenceAddManagetbl.EmpId=EmployeeMaster.EmployeeMasterID where IncidenceAddManagetbl.Id='" + id + "'";


        if (txtfromdt.Text != "" && txttodt.Text != "")
        {
            str += "and IncidenceAddManagetbl.Date between '" + Convert.ToDateTime(txtfromdt.Text) + "' and '" + Convert.ToDateTime(txttodt.Text) + "'";
        }

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);

        fillstore();
        if (dt.Rows.Count > 0)
        {
            ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(Convert.ToString(dt.Rows[0]["whid"])));
            fillemployee();
            ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dt.Rows[0]["EmpId"].ToString()));

            fillincidence();
            ddlincidence.SelectedIndex = ddlincidence.Items.IndexOf(ddlincidence.Items.FindByValue(dt.Rows[0]["Id"].ToString()));

            lblincidenceno.Text = dt.Rows[0]["Id"].ToString();
            lblemp.Text = dt.Rows[0]["EmployeeName"].ToString();
            lbldate.Text = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToShortDateString();
            lbltime.Text = dt.Rows[0]["Time"].ToString() + " " + dt.Rows[0]["Timezone"].ToString();
            lblpoint.Text = dt.Rows[0]["Penaltypoint"].ToString();
            if (dt.Rows[0]["PolicyTitle"].ToString() != "" && dt.Rows[0]["PolicyTitle"].ToString() != null)
            {
                lblname.Visible = true;
                lbltitle.Visible = true;

                lblname.Text = "Policy Name";
                lbltitle.Text = dt.Rows[0]["PolicyTitle"].ToString() + " : " + dt.Rows[0]["PolicyName"].ToString();
            }
            if (dt.Rows[0]["ProcedureTitle"].ToString() != "" && dt.Rows[0]["ProcedureTitle"].ToString() != null)
            {
                lblname.Visible = true;
                lbltitle.Visible = true;

                lblname.Text = "Procedure Name";
                lbltitle.Text = dt.Rows[0]["ProcedureTitle"].ToString() + " : " + dt.Rows[0]["ProcedureName"].ToString();
            }
            if (dt.Rows[0]["RuleTitle"].ToString() != "" && dt.Rows[0]["RuleTitle"].ToString() != null)
            {
                lblname.Visible = true;
                lbltitle.Visible = true;

                lblname.Text = "Rule Name";
                lbltitle.Text = dt.Rows[0]["RuleTitle"].ToString() + " : " + dt.Rows[0]["RuleName"].ToString();
            }
            lblnote.Text = dt.Rows[0]["IncidenceNote"].ToString();
            lblempans.Text = dt.Rows[0]["IncidenceEmpAnsNote"].ToString();
            // grid.DataSource = dt;
            //grid.DataBind();
        }
    }

    protected void fillgrid()
    {
        string str = "";

        str = "select distinct IncidenceAddManagetbl.*,PolicyAddManagetbl.PolicyName,ProcedureAddManagetbl.ProcedureName,RuleAddManagetbl.RuleName,IncidenceDetailTbl.IncidenceEmpAnsNote,Policyprocedureruletiletbl.PolicyTitle,Procedureforpolicytbl.ProcedureTitle,Ruleforpolicytbl.RuleTitle,EmployeeMaster.EmployeeName " +
                " from IncidenceAddManagetbl inner join IncidenceDetailTbl on IncidenceDetailTbl.IncidenceMasterId=IncidenceAddManagetbl.Id left join PolicyAddManagetbl on PolicyAddManagetbl.Id = IncidenceAddManagetbl.PolicyNameId left join Policyprocedureruletiletbl on PolicyAddManagetbl.PolicyId=Policyprocedureruletiletbl.Id " +
                " left join ProcedureAddManagetbl on ProcedureAddManagetbl.Id = IncidenceAddManagetbl.ProcedureNameId left join Procedureforpolicytbl on ProcedureAddManagetbl.ProcedureId=Procedureforpolicytbl.Id left join  RuleAddManagetbl on RuleAddManagetbl.Id = IncidenceAddManagetbl.RuleNameId " +
                " left join Ruleforpolicytbl on RuleAddManagetbl.RuleId=Ruleforpolicytbl.Id inner join EmployeeMaster on IncidenceAddManagetbl.EmpId=EmployeeMaster.EmployeeMasterID where IncidenceAddManagetbl.Compid='" + ViewState["Compid"] + "'";
        //if(ddlstore.SelectedIndex > 0)
        //{
        //    str += "and IncidenceAddManagetbl.whid='"+ ddlstore.SelectedValue +"'";
        //}
        //if(ddlemployee.SelectedIndex > 0)
        //{
        //    str += "and IncidenceAddManagetbl.EmpId='"+ ddlemployee.SelectedValue +"'";
        //}
        {
            str += "and IncidenceAddManagetbl.Id='" + ddlincidence.SelectedValue + "'";
        }
        if (txtfromdt.Text != "" && txttodt.Text != "")
        {
            str += "and IncidenceAddManagetbl.Date between '" + Convert.ToDateTime(txtfromdt.Text) + "' and '" + Convert.ToDateTime(txttodt.Text) + "'";
        }

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);

        fillstore();
        if (dt.Rows.Count > 0)
        {
            ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(Convert.ToString(dt.Rows[0]["whid"])));
            fillemployee();
            ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dt.Rows[0]["EmpId"].ToString()));

            lblincidenceno.Text = dt.Rows[0]["Id"].ToString();
            lblemp.Text = dt.Rows[0]["EmployeeName"].ToString();
            lbldate.Text = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToShortDateString();
            lbltime.Text = dt.Rows[0]["Time"].ToString() + " " + dt.Rows[0]["Timezone"].ToString();
            lblpoint.Text = dt.Rows[0]["Penaltypoint"].ToString();
            if (dt.Rows[0]["PolicyTitle"].ToString() != "" && dt.Rows[0]["PolicyTitle"].ToString() != null)
            {
                lblname.Visible = true;
                lbltitle.Visible = true;

                lblname.Text = "Policy Name";
                lbltitle.Text = dt.Rows[0]["PolicyTitle"].ToString() + " : " + dt.Rows[0]["PolicyName"].ToString();
            }
            if (dt.Rows[0]["ProcedureTitle"].ToString() != "" && dt.Rows[0]["ProcedureTitle"].ToString() != null)
            {
                lblname.Visible = true;
                lbltitle.Visible = true;

                lblname.Text = "Procedure Name";
                lbltitle.Text = dt.Rows[0]["ProcedureTitle"].ToString() + " : " + dt.Rows[0]["ProcedureName"].ToString();
            }
            if (dt.Rows[0]["RuleTitle"].ToString() != "" && dt.Rows[0]["RuleTitle"].ToString() != null)
            {
                lblname.Visible = true;
                lbltitle.Visible = true;

                lblname.Text = "Rule Name";
                lbltitle.Text = dt.Rows[0]["RuleTitle"].ToString() + " : " + dt.Rows[0]["RuleName"].ToString();
            }
            lblnote.Text = dt.Rows[0]["IncidenceNote"].ToString();
            lblempans.Text = dt.Rows[0]["IncidenceEmpAnsNote"].ToString();
            // grid.DataSource = dt;
            //grid.DataBind();
        }
    }
    protected void fillmonthpoint()
    {
        Int32 pointm = 0;
        string str = "select Penaltypoint from IncidenceAddManagetbl where EmpId='" + ddlemployee.SelectedValue + "' and Month(Date) = '" + System.DateTime.Now.Month.ToString() + "'";
        if (ddlincidence.SelectedIndex > 0)
        {
            str += "and Id = '" + ddlincidence.SelectedValue + "'";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            pointm = pointm + Convert.ToInt32(ds.Tables[0].Rows[i]["Penaltypoint"].ToString());
        }
        lblptm.Text = pointm.ToString();
    }
    protected void fillyearpoint()
    {
        Int32 pointy = 0;
        string str = "select Penaltypoint from IncidenceAddManagetbl where EmpId='" + ddlemployee.SelectedValue + "' and Year(Date) = '" + System.DateTime.Now.Year.ToString() + "'";
        if (ddlincidence.SelectedIndex > 0)
        {
            str += "and Id = '" + ddlincidence.SelectedValue + "'";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            pointy = pointy + Convert.ToInt32(ds.Tables[0].Rows[i]["Penaltypoint"].ToString());
        }
        lblpty.Text = pointy.ToString();
    }
    protected void txttodt_TextChanged(object sender, EventArgs e)
    {
        fillincidence();
        fillgrid();
    }
    protected void ddlincidence_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillmonthpoint();
        fillyearpoint();
        fillgrid();
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");
            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;


        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;


        }
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        panelhide.Visible = true;
        fillgrid();
    }
    protected void txtfromdt_TextChanged(object sender, EventArgs e)
    {
        fillincidence();
        fillgrid();
    }
}
