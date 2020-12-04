using System;
using System.Windows;
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

public partial class Add_Employee_Attendance_Report_Detail : System.Web.UI.Page
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
            fillwarehouse();

            txteffectend.Text = System.DateTime.Now.ToShortDateString();
            txteffectstart.Text = System.DateTime.Now.ToShortDateString();
        }

    }
    protected void fillwarehouse()
    {
        //string str1 = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["Comid"].ToString() + "' and WareHouseMaster.Status='1' and EmployeeWarehouseRights.AccessAllowed='True' order by name asc";        

        //DataTable ds1 = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(str1, con);
        //da.Fill(ds1);
        //if (ds1.Rows.Count > 0)
        //{
        //    ddlwarehouse.DataSource = ds1;
        //    ddlwarehouse.DataTextField = "Name";
        //    ddlwarehouse.DataValueField = "WarehouseId";
        //    ddlwarehouse.DataBind();           
        //}
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();


            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
        }

        fillEmployee();

    }
    protected void fillEmployee()
    {
        ddlEmployeeName.Items.Clear();
        string str = "SELECT * from EmployeeMaster where Whid = '" + ddlwarehouse.SelectedValue + "' and SuprviserId = '" + Session["EmployeeId"] + "' order by EmployeeName asc";
        str = "select EmployeeMaster.EmployeeName , EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join EmployeeBatchMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where  dbo.EmployeeMaster.Whid='" + ddlwarehouse.SelectedValue + "'";
        SqlCommand cmdfilteremp = new SqlCommand(str, con);
        SqlDataAdapter adpfilteremp = new SqlDataAdapter(cmdfilteremp);
        DataTable dtfilteremp = new DataTable();
        adpfilteremp.Fill(dtfilteremp);

        if (dtfilteremp.Rows.Count > 0)
        {
            ddlEmployeeName.DataSource = dtfilteremp;
            ddlEmployeeName.DataTextField = "EmployeeName";
            ddlEmployeeName.DataValueField = "EmployeeMasterId";
            ddlEmployeeName.DataBind();       
        }
        ddlEmployeeName.Items.Insert(0, "-Select-");
        ddlEmployeeName.Items[0].Value = "0";
        fillparty();
    }
    protected void ddEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillparty();
    }
    protected void fillgrid()
    {
        string str = "select distinct GatepassTBL.Id,GatepassTBL.GatepassREQNo,GatepassTBL.Date,GatepassTBL.ExpectedOutTime,GatepassTBL.ExpectedInTime,EmployeeMaster.EmployeeName,GatepassTBL.EmployeeID  from GatepassTBL  inner join GatepassDetails  on GatepassDetails.GatePassID=GatepassTBL.Id inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=GatepassTBL.EmployeeID where GatepassTBL.Approved = " + 2 + " and GatepassTBL.StoreID='" + ddlwarehouse.SelectedValue + "' and gatepassTBL.Date between '" + txteffectstart.Text + "' and '" + txteffectend.Text + "'";

        if (ddlEmployeeName.SelectedIndex > 0)
        {
            str += " and GatepassTBL.EmployeeID = '" + ddlEmployeeName.SelectedValue + "'";
        }
        if (ddlEmployeeName.SelectedIndex > 0)
        {
            str += " and EmployeeMaster.EmployeeMasterID = '" + ddlEmployeeName.SelectedValue + "' ";
        }
        if (ddParty.SelectedIndex > 0)
        {
            str += " and GatepassDetails.PartyID = '" + ddParty.SelectedValue + "'";
        }
        lblCompany.Text = Session["Cname"].ToString();
        lblBusiness.Text = ddlwarehouse.SelectedItem.Text.ToString();
        lblEmp.Text = ddlEmployeeName.SelectedItem.Text.ToString();
        lblParty.Text = ddParty.SelectedItem.Text.ToString();
        SqlCommand cmd = new SqlCommand(str, con);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            ViewState["GatePassId"] = Convert.ToInt32(ds.Rows[0]["Id"].ToString());
        }

        if (ds.Rows.Count > 0)
        {

            grdgatepassreport.DataSource = ds;
            grdgatepassreport.DataBind();
            
        }
        else
        {
            grdgatepassreport.DataSource = null;
            grdgatepassreport.DataBind();
       
        }
        foreach (GridViewRow gdr in grdgatepassreport.Rows)
        {
            Label lblmasterid123 = (Label)gdr.FindControl("lblmasterid123");

            Label lblEmployeeName = (Label)gdr.FindControl("lblEmployeeName");
            Label lblDate = (Label)gdr.FindControl("lblDate");
            Label txtOuttime = (Label)gdr.FindControl("txtOuttime");
            Label txtInTime = (Label)gdr.FindControl("txtInTime");
          


            // string acco = "select GatepassDetails.*,Party_master.Compname as 'partyname' from dbo.Party_master inner join GatepassDetails on Party_master.PartyID = GatepassDetails.PartyID  where GatePassID='" + lblmasterid123.Text + "' ";
            string acco = "select GatepassDetails.*,Party_master.Compname as partyname from Party_master inner join GatepassDetails on Party_master.PartyID = GatepassDetails.PartyID   where GatePassID='" + lblmasterid123.Text + "' ";
            SqlCommand cmd1 = new SqlCommand(acco, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            adp1.Fill(ds1);
            string partyname1 = "";
            if (ds1.Rows.Count > 0)
            {
                if (ds1.Rows.Count == 1)
                {

                    partyname1 = ds1.Rows[0]["partyname"].ToString();                       
                   
                }
                else
                {
                    partyname1 = ds1.Rows[0]["partyname"].ToString();
                    partyname1 = partyname1 +  " <Font color=##0000> & MORE... ";
                }
                
                Label lblPartyName1 = (Label)gdr.FindControl("lblPartyName");

                lblPartyName1.Text = partyname1;
            }
        }
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillEmployee();
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void grdgatepassreport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {

            int dk = Convert.ToInt32(e.CommandArgument);
            string abcfinal =  "select EmployeeId from GatepassTBL where Id = '"+ dk +"'";
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand findemp = new SqlCommand(abcfinal,con);
            string eid = findemp.ExecuteScalar().ToString();
            string te = ("frmGatepassProfile.aspx?empid="+eid+"&req="+dk);
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button5.Visible = true;
            if (grdgatepassreport.Columns[7].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdgatepassreport.Columns[7].Visible = false;
            }


        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(250);

            Button1.Text = "Printable Version";
            Button5.Visible = false;
            if (ViewState["editHide"] != null)
            {
                grdgatepassreport.Columns[7].Visible = true;
            }


        }
    }
    protected void fillparty()
    {
        ddParty.Items.Clear();

        string str1 = "select party_master.partyId,Compname from party_master where Compname!='' AND Whid = '" + ddlwarehouse.SelectedValue + "' order by Compname asc";

        SqlCommand cmdparty = new SqlCommand(str1, con);
        SqlDataAdapter adpparty = new SqlDataAdapter(cmdparty);
        DataTable dtparty = new DataTable();
        adpparty.Fill(dtparty);
        if (dtparty.Rows.Count > 0)
        {

            ddParty.DataSource = dtparty;
            ddParty.DataTextField = "Compname";
            ddParty.DataValueField = "partyId";
            ddParty.DataBind();
        }
    }

}