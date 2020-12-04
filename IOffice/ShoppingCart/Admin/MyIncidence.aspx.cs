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

public partial class ShoppingCart_Admin_MyIncidence : System.Web.UI.Page
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
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";   
            txtfromdt.Text = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
            txttodt.Text = System.DateTime.Now.ToShortDateString();
            fillgrid();
            fillemmmm();
        }
    }
    protected void fillgrid()
    {
        Int32 pointm = 0;
        Int32 pointy = 0;
        string str = "select Login_master.UserID,Login_master.username,User_master.UserID,User_master.PartyID," +
                    " EmployeeMaster.EmployeeMasterID,Party_master.PartyID,Party_master.id,EmployeeMaster.SuprviserId " +
                    " from Login_master inner join User_master on Login_master.UserID=User_master.UserID  " +
                    " inner join Party_master on User_master.PartyID=Party_master.PartyID inner join " +
                    " EmployeeMaster on User_master.PartyID=EmployeeMaster.PartyID where Login_master.UserID='" + ViewState["UserName"] + "' and Party_master.id='" + ViewState["Compid"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);       
        ViewState["empid"] = ds.Tables[0].Rows[0]["EmployeeMasterID"].ToString();

        string str1 = "";
        str1 = "select distinct IncidenceAddManagetbl.*,IncidenceDetailTbl.IncidenceEmpAnsNote,Policyprocedureruletiletbl.PolicyTitle,Procedureforpolicytbl.ProcedureTitle,Ruleforpolicytbl.RuleTitle,EmployeeMaster.EmployeeName " +
                " from IncidenceAddManagetbl inner join IncidenceDetailTbl on IncidenceDetailTbl.IncidenceMasterId=IncidenceAddManagetbl.Id left join PolicyAddManagetbl on PolicyAddManagetbl.Id = IncidenceAddManagetbl.PolicyNameId left join Policyprocedureruletiletbl on PolicyAddManagetbl.PolicyId=Policyprocedureruletiletbl.Id " +
                " left join ProcedureAddManagetbl on ProcedureAddManagetbl.Id = IncidenceAddManagetbl.ProcedureNameId left join Procedureforpolicytbl on ProcedureAddManagetbl.ProcedureId=Procedureforpolicytbl.Id left join  RuleAddManagetbl on RuleAddManagetbl.Id = IncidenceAddManagetbl.RuleNameId " +
                " left join Ruleforpolicytbl on RuleAddManagetbl.RuleId=Ruleforpolicytbl.Id inner join EmployeeMaster on IncidenceAddManagetbl.EmpId=EmployeeMaster.EmployeeMasterID where IncidenceAddManagetbl.EmpId='" + ViewState["empid"] + "'";
        if (txtfromdt.Text != "" && txttodt.Text != "")
        {
            str1 += "and IncidenceAddManagetbl.Date between '" + Convert.ToDateTime(txtfromdt.Text) + "' and '" + Convert.ToDateTime(txttodt.Text) + "'";
        }
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        adpt1.Fill(dt);
        grid.DataSource = dt;
        grid.DataBind();

        string str2 = " select Penaltypoint from IncidenceAddManagetbl where EmpId='" + ViewState["empid"] + "' and Month(Date) = '" + System.DateTime.Now.Month.ToString() + "'";
        SqlCommand cmd2 = new SqlCommand(str2, con);
        SqlDataAdapter adpt2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        adpt2.Fill(ds2);
        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
        {
            pointm = pointm + Convert.ToInt32(ds2.Tables[0].Rows[i]["Penaltypoint"].ToString());
        }
        lblptm.Text = pointm.ToString();

        string str3 = "select Penaltypoint from IncidenceAddManagetbl where EmpId='" + ViewState["empid"] + "' and Year(Date) = '" + System.DateTime.Now.Year.ToString() + "'";

        SqlCommand cmd3 = new SqlCommand(str3, con);
        SqlDataAdapter adpt3 = new SqlDataAdapter(cmd3);
        DataSet ds3 = new DataSet();
        adpt3.Fill(ds3);
        for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
        {
            pointy = pointy + Convert.ToInt32(ds3.Tables[0].Rows[i]["Penaltypoint"].ToString());
        }
        lblpty.Text = pointy.ToString();
       
    }
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Panel9.Visible = true;
        //Pnl1.Visible = true;
        //ViewState["ansid"] = grid.DataKeys[e.NewEditIndex].Value.ToString();
        int dk = Convert.ToInt32(grid.DataKeys[e.NewEditIndex].Value);
        
        string str1 = "select * from IncidenceDetailTbl where IncidenceMasterId='" + dk + "'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd1);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        if (ds.Tables[0].Rows[0]["IncidenceEmpAnsNote"].ToString() != "")
        {
            statuslable.Text = "You can not change this record";
        }
        else
        {
            grid.EditIndex = e.NewEditIndex;
            fillgrid();
            TextBox txtbox = (TextBox)grid.Rows[grid.EditIndex].FindControl("txtempans");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //if (txtdescription.Text != "")
        //{
        //    string str = "Update IncidenceDetailTbl set IncidenceEmpAnsNote='" + txtdescription.Text + "' , EmpAnsDateTime='" + System.DateTime.Now.ToString() + "' where IncidenceMasterId='" + ViewState["ansid"] + "'";
        //    SqlCommand cmd = new SqlCommand(str, con);
        //    if (con.State.ToString() != "Open")
        //    {
        //        con.Open();
        //    }
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    statuslable.Text = "Record inserted successfully";
           
        //    Panel9.Visible = false;
        //    Pnl1.Visible = false;
        //    fillgrid();
        //}
        //else
        //{
        //    statuslable.Text = "Please fill note";
        //}
    }
    protected void txttodt_TextChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    
    protected void btnreset_Click(object sender, EventArgs e)
    {
       
        Panel9.Visible = false;
        Pnl1.Visible = false;
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
                ViewState["viewm"] = "tt";
                grid.Columns[9].Visible = false;
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
            
            if (ViewState["viewm"] != null)
            {
                grid.Columns[9].Visible = true;
            }
        }
    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "MyIncidence_Profile.aspx?id=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
    protected void grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int dk = Convert.ToInt32(grid.DataKeys[grid.EditIndex].Value);
        TextBox txtbox = (TextBox)grid.Rows[grid.EditIndex].FindControl("txtempans");
        
            string str = "Update IncidenceDetailTbl set IncidenceEmpAnsNote='" + txtbox.Text + "' , EmpAnsDateTime='" + System.DateTime.Now.ToString() + "' where IncidenceMasterId='" + dk + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            statuslable.Text = "Record updated successfully";

            grid.EditIndex = -1;
            fillgrid();
        
    }
    protected void grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grid.EditIndex = -1;
        fillgrid();
    }

    protected void fillemmmm()
    {
        string stttt = "select warehousemaster.name,employeemaster.employeename from employeemaster inner join warehousemaster on warehousemaster.warehouseid=employeemaster.whid where employeemaster.employeemasterid='" + Session["EmployeeId"] + "'";
        SqlDataAdapter da = new SqlDataAdapter(stttt,con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        lblBusiness.Text = Convert.ToString(dt.Rows[0]["name"]);
        lblempname.Text = "Employee Name : " + Convert.ToString(dt.Rows[0]["employeename"]);
    }
}
