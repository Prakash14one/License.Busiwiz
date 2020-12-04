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

public partial class ShoppingCart_Admin_MyIncidence_Profile : System.Web.UI.Page
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
            Pagecontrol.dypcontrol(Page, page);

            ViewState["sortOrder"] = "";                       
            fillgrid();
            fillmonthpoint();
            fillyearpoint();
            fillemmmm();
        }
    }
    
    
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();               
    }
    protected void fillemmmm()
    {
        string stttt = "select warehousemaster.name,employeemaster.employeename from employeemaster inner join warehousemaster on warehousemaster.warehouseid=employeemaster.whid where employeemaster.employeemasterid='" + Session["EmployeeId"] + "'";
        SqlDataAdapter da = new SqlDataAdapter(stttt, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        lblBusiness.Text = Convert.ToString(dt.Rows[0]["name"]);
    }
    
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
       
      
    }
    protected void fillgrid()
    {
        string str = "";
        if (Request.QueryString["id"] != null)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            str = "select distinct IncidenceAddManagetbl.*,IncidenceDetailTbl.IncidenceEmpAnsNote,Policyprocedureruletiletbl.PolicyTitle,Procedureforpolicytbl.ProcedureTitle,Ruleforpolicytbl.RuleTitle,EmployeeMaster.EmployeeName " +
                    " from IncidenceAddManagetbl inner join IncidenceDetailTbl on IncidenceDetailTbl.IncidenceMasterId=IncidenceAddManagetbl.Id left join PolicyAddManagetbl on PolicyAddManagetbl.Id = IncidenceAddManagetbl.PolicyNameId left join Policyprocedureruletiletbl on PolicyAddManagetbl.PolicyId=Policyprocedureruletiletbl.Id " +
                    " left join ProcedureAddManagetbl on ProcedureAddManagetbl.Id = IncidenceAddManagetbl.ProcedureNameId left join Procedureforpolicytbl on ProcedureAddManagetbl.ProcedureId=Procedureforpolicytbl.Id left join  RuleAddManagetbl on RuleAddManagetbl.Id = IncidenceAddManagetbl.RuleNameId " +
                    " left join Ruleforpolicytbl on RuleAddManagetbl.RuleId=Ruleforpolicytbl.Id inner join EmployeeMaster on IncidenceAddManagetbl.EmpId=EmployeeMaster.EmployeeMasterID where IncidenceAddManagetbl.Id='" + id + "'";
       
        
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        lblincidenceno.Text = dt.Rows[0]["Id"].ToString();
        lblemp.Text = dt.Rows[0]["EmployeeName"].ToString();
        lbldate.Text = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToShortDateString();
        lbltime.Text = dt.Rows[0]["Time"].ToString() + " " + dt.Rows[0]["Timezone"].ToString();
        lblpoint.Text = dt.Rows[0]["Penaltypoint"].ToString();
        if (dt.Rows[0]["PolicyTitle"].ToString() != "" && dt.Rows[0]["PolicyTitle"].ToString() != null)
        {
            lblname.Visible = true;
            lbltitle.Visible = true;

            lblname.Text = "Policy Title";
            lbltitle.Text = dt.Rows[0]["PolicyTitle"].ToString();
        }
        if (dt.Rows[0]["ProcedureTitle"].ToString() != "" && dt.Rows[0]["ProcedureTitle"].ToString() != null)
        {
            lblname.Visible = true;
            lbltitle.Visible = true;

            lblname.Text = "Procedure Title";
            lbltitle.Text = dt.Rows[0]["ProcedureTitle"].ToString();
        }
        if (dt.Rows[0]["RuleTitle"].ToString() != "" && dt.Rows[0]["RuleTitle"].ToString() != null)
        {
            lblname.Visible = true;
            lbltitle.Visible = true;

            lblname.Text = "Rule Title";
            lbltitle.Text = dt.Rows[0]["RuleTitle"].ToString();
        }
        lblnote.Text = dt.Rows[0]["IncidenceNote"].ToString();
        lblempans.Text = dt.Rows[0]["IncidenceEmpAnsNote"].ToString();
        }
    }
    //protected void fillmonthpoint()
    //{
    //    Int32 pointm = 0;
    //    string str = "select Penaltypoint from IncidenceAddManagetbl where EmpId='" + ddlemployee.SelectedValue + "' and Month(Date) = '" + System.DateTime.Now.Month.ToString() + "'";
    //    if (ddlincidence.SelectedIndex > 0)
    //    {
    //        str += "and Id = '" + ddlincidence.SelectedValue + "'";
    //    }
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    adpt.Fill(ds);
    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //    {
    //        pointm = pointm + Convert.ToInt32(ds.Tables[0].Rows[i]["Penaltypoint"].ToString());
    //    }
    //    lblptm.Text = pointm.ToString();
    //}
    //protected void fillyearpoint()
    //{
    //    Int32 pointy = 0;
    //    string str = "select Penaltypoint from IncidenceAddManagetbl where EmpId='" + ddlemployee.SelectedValue + "' and Year(Date) = '" + System.DateTime.Now.Year.ToString() + "'";
    //    if (ddlincidence.SelectedIndex > 0)
    //    {
    //        str += "and Id = '" + ddlincidence.SelectedValue + "'";
    //    }
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    adpt.Fill(ds);
    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //    {
    //        pointy = pointy + Convert.ToInt32(ds.Tables[0].Rows[i]["Penaltypoint"].ToString());
    //    }
    //    lblpty.Text = pointy.ToString();
    //}
    protected void txttodt_TextChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlincidence_SelectedIndexChanged(object sender, EventArgs e)
    {
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

    protected void fillmonthpoint()
    {
        if (Request.QueryString["id"] != null)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);

            Int32 pointm = 0;
            string str = "select Penaltypoint from IncidenceAddManagetbl where Id=" + id;          
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
    }
    protected void fillyearpoint()
    {
        if (Request.QueryString["id"] != null)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);

            Int32 pointy = 0;
            string str = "select Penaltypoint from IncidenceAddManagetbl where Id=" + id;
           
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
    }
}
