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
using System.Collections.Generic;
using System.Data.SqlClient;
public partial class Account_AssignrightstoEmpforEmail : System.Web.UI.Page
{
    SqlConnection con;
    MessageCls clsMessage = new MessageCls();
    protected Int32 coid1, coid2, coid3, coid4, coid5, coid6, coid7, coid8, coid9, coid10;
    protected string email1, email2, email3, email4, email5, email6, email7, email8, email9, email10;
    protected DataTable dtcom = new DataTable();
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
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
        lblCompany.Text = Session["Comid"].ToString();


        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);
        DataTable dsss = new DataTable();
        dsss = clsMessage.Empid("158");
        if (dsss.Rows.Count > 0)
        {
            Session["EmployeeIdep"] = dsss.Rows[0]["EmployeeMasterId"].ToString();
        }
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            string str = "SELECT WareHouseId,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlstore.DataSource = dt;
            ddlstore.DataTextField = "Name";
            ddlstore.DataValueField = "WareHouseId";
            ddlstore.DataBind();
            ddlstore_SelectedIndexChanged(sender, e);
            //fillemail();
        }

    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void Fillddlemployee()
    {
        ddlemployee.Items.Clear();
        DataTable dt = new DataTable();

        dt = select("select EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName as EmployeeName,dbo.EmployeeMaster.Active from EmployeeMaster where EmployeeMaster.Whid='" + ddlstore.SelectedValue + "' and EmployeeMaster.DesignationMasterID='" + DropDownList1.SelectedValue + "' order by EmployeeMaster.EmployeeName");
        ddlemployee.DataSource = dt;

        ddlemployee.DataTextField = "EmployeeName";
        ddlemployee.DataValueField = "EmployeeMasterID";
        ddlemployee.DataBind();

        ddlemployee.Items.Insert(0, "All");
        ddlemployee.SelectedItem.Value = "0";
    }

    protected void flgrd()
    {
        lblBusiness.Text = ddlstore.SelectedItem.Text;
        //MasterCls clsMaster = new MasterCls();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        //dt = clsMaster.SelectCompanyEmailRightsbyEmp(Convert.ToInt32(ddlemployee.SelectedValue));
        //dt = clsMaster.SelectCompanyEmail();

        //if (ddlemployee.SelectedIndex > 0)
        //{
        //    dt = select("select ID as CompanyEmailId,EmployeeId,InEmailID as EmailId,WarehouseMaster.Name as Wname from InOutCompanyEmail inner join WarehouseMaster on WarehouseMaster.WarehouseId=InOutCompanyEmail.Whid where (Whid='" + ddlstore.SelectedValue + "' and EmployeeID='" + ddlemployee.SelectedValue + "') and WarehouseMaster.status='1' order by EmailId asc");
        //}   
        //if (ddlemployee.SelectedIndex == 0)
        //{
        //    dt = select("select ID as CompanyEmailId,EmployeeId,InEmailID as EmailId,WarehouseMaster.Name as Wname from InOutCompanyEmail inner join WarehouseMaster on WarehouseMaster.WarehouseId=InOutCompanyEmail.Whid where Whid='" + ddlstore.SelectedValue + "' and WarehouseMaster.status='1' order by EmailId asc");
        //}

        if (checkbox12.Checked == true)
        {
            dt = select("select ID as CompanyEmailId,EmployeeId,InEmailID as EmailId,WarehouseMaster.Name as Wname from InOutCompanyEmail inner join WarehouseMaster on WarehouseMaster.WarehouseId=InOutCompanyEmail.Whid where WarehouseMaster.status='1' order by EmailId asc");
        }
        else
        {
            dt = select(" select ID as CompanyEmailId,EmployeeId,InEmailID as EmailId,WarehouseMaster.Name as Wname from InOutCompanyEmail inner join WarehouseMaster on WarehouseMaster.WarehouseId=InOutCompanyEmail.Whid where Whid='" + ddlstore.SelectedValue + "' and WarehouseMaster.status='1' order by EmailId asc");
        }

        if (dt.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            gridocaccessright1.DataSource = dt;
            gridocaccessright1.DataBind();
            imgbtnsubmit.Visible = true;
            ViewState["we"] = dt;
        }
        else
        {
            gridocaccessright1.DataSource = null;
            gridocaccessright1.DataBind();
        }

        foreach (GridViewRow gdrr in gridocaccessright1.Rows)
        {
            Label Label2 = (Label)gdrr.FindControl("Label2");
            Label lblempname = (Label)gdrr.FindControl("lblempname");

            if (Label2.Text == "0")
            {
                lblempname.Text = "Company Email";
            }
            else
            {
                lblempname.Text = "Employee Email";
            }
        }
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlemployee.SelectedIndex >0)
        {
            pnlmsg.Visible = false;
            flgrd();
        }
        else 
        {
            imgbtnsubmit.Visible = false;  
            gridocaccessright1.DataSource = null;
            gridocaccessright1.DataBind();
        }        
    }
    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Int32 i;
            Int32 CompanyEmailId;
            Int32 rst;
            rst = 0;
            i = 0;
            Int32 EmployeeId;
            EmployeeId = Convert.ToInt32(ddlemployee.SelectedValue);



            if (gridocaccessright1.Rows.Count > 0)
            {
                do
                {
                    CompanyEmailId = Int32.Parse(gridocaccessright1.DataKeys[i].Value.ToString());
                    CheckBox chkprint = (CheckBox)gridocaccessright1.Rows[i].FindControl("chkprint");
                    CheckBox chkprint1 = (CheckBox)gridocaccessright1.Rows[i].FindControl("chkprint1");
                    CheckBox chkprint2 = (CheckBox)gridocaccessright1.Rows[i].FindControl("chkprint2");

                    //

                    // rst = clsDocument.InsertCompanyEmailAccessRightMaster(CompanyEmailId, Convert.ToInt32(ddlemployee.SelectedValue), chkprint.Checked, chkprint1.Checked, chkprint2.Checked);

                    SqlDataAdapter daff = new SqlDataAdapter("select CompanyEmailAssignId from CompanyEmailAssignAccessRights where CompanyEmailId='" + CompanyEmailId + "' and EmployeeId='" + Convert.ToInt32(ddlemployee.SelectedValue) + "' and DesignationID='" + DropDownList1.SelectedValue + "' and CID='" + Session["Comid"].ToString() + "'", con);
                    DataTable dtff = new DataTable();
                    daff.Fill(dtff);

                    string str12 = "";

                    if (dtff.Rows.Count > 0)
                    {
                        str12 = "update CompanyEmailAssignAccessRights set viewRights='" + chkprint.Checked + "',DeleteRights='" + chkprint1.Checked + "',SendRights='" + chkprint2.Checked + "' where CompanyEmailAssignId='" + dtff.Rows[0]["CompanyEmailAssignId"].ToString() + "' and CompanyEmailId='" + CompanyEmailId + "' and EmployeeId='" + Convert.ToInt32(ddlemployee.SelectedValue) + "' and DesignationID='" + DropDownList1.SelectedValue + "' and CID='" + Session["Comid"].ToString() + "'";
                    }
                    else
                    {
                        str12 = "insert into CompanyEmailAssignAccessRights([CompanyEmailId],[DesignationID],[EmployeeId],[viewRights],[CID],[DeleteRights],[SendRights]) values('" + CompanyEmailId + "','" + DropDownList1.SelectedValue + "','" + Convert.ToInt32(ddlemployee.SelectedValue) + "','" + chkprint.Checked + "','" + Session["Comid"].ToString() + "','" + chkprint1.Checked + "','" + chkprint2.Checked + "')";
                    }
                    SqlCommand cmd1 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    SqlDataAdapter daf = new SqlDataAdapter("select max(CompanyEmailAssignId) as CompanyEmailAssignId from CompanyEmailAssignAccessRights", con);
                    DataTable dtf = new DataTable();
                    daf.Fill(dtf);

                    if (dtf.Rows.Count > 0)
                    {
                        rst = Convert.ToInt32(dtf.Rows[0]["CompanyEmailAssignId"]);
                    }

                    i = i + 1;
                }
                while (i <= gridocaccessright1.Rows.Count - 1);
            }
            if (rst > 0)
            {
                pnlmsg.Visible = true;
                lblmsg.Visible = true;

                lblmsg.Text = "Record inserted successfully";

                if (ddlemployee.SelectedIndex > 0)
                {
                    DataTable dt = new DataTable();
                    //dt = clsMaster.SelectCompanyEmailRightsbyEmp(Convert.ToInt32(ddlemployee.SelectedValue));
                    //dt=clsMaster.SelectCompanyEmail();

                    dt = select("select ID as CompanyEmailId,EmployeeId,InEmailID as EmailId from InOutCompanyEmail where Whid='" + ddlstore.SelectedValue + "'");

                    gridocaccessright1.DataSource = ViewState["we"];
                    gridocaccessright1.DataBind();

                    foreach (GridViewRow gdrr in gridocaccessright1.Rows)
                    {
                        Label Label2 = (Label)gdrr.FindControl("Label2");
                        Label lblempname = (Label)gdrr.FindControl("lblempname");

                        if (Label2.Text == "0")
                        {
                            lblempname.Text = "Company Email";
                        }
                        else
                        {
                            lblempname.Text = "Employee Email";
                        }
                    }
                }
            }
        }
        catch (Exception es)
        {
            pnlmsg.Visible = true;
            lblmsg.Visible = true;
            lblmsg.Text = es.Message.ToString();
        }
    }
    protected void gridocaccessright1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkprint = (CheckBox)(e.Row.FindControl("chkprint"));
            CheckBox chkprint1 = (CheckBox)(e.Row.FindControl("chkprint1"));
            CheckBox chkprint2 = (CheckBox)(e.Row.FindControl("chkprint2"));

            //chkprint.Enabled = false;
            //chkprint1.Enabled = false;
            //chkprint2.Enabled = false;            

            Int32 companyemailid = Int32.Parse(gridocaccessright1.DataKeys[e.Row.RowIndex].Value.ToString());

            //if (ddlemployee.SelectedIndex > 0)
            //{
            chkprint.Enabled = true;
            chkprint1.Enabled = true;
            chkprint2.Enabled = true;

            DataTable dt;
            dt = new DataTable();
            dt = SelectEmailAccessRightwithCompanyID(companyemailid);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    chkprint.Checked = ((object)row["viewRights"]).ToString() == "True" ? true : false;
                    chkprint1.Checked = ((object)row["DeleteRights"]).ToString() == "True" ? true : false;
                    chkprint2.Checked = ((object)row["SendRights"]).ToString() == "True" ? true : false;
                }
            }
            //}
        }
    }
    protected DataTable SelectEmailAccessRightwithCompanyID(Int32 companyemailid)
    {
        DataTable dt;
        dt = new DataTable();

        string stfr = "select * from CompanyEmailAssignAccessRights where DesignationID='" + DropDownList1.SelectedValue + "' and EmployeeId='" + Int32.Parse(ddlemployee.SelectedValue.ToString()) + "' and CompanyEmailId ='" + companyemailid + "' and CID='" + Session["Comid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(stfr, con);
        da.Fill(dt);

        //dt = clsDocument.SelectEmailAccessRightwithCompanyID(companyemailid, Int32.Parse(ddlemployee.SelectedValue.ToString())); //.SelectAccessRightMasterwithDepartmentDesignation ( DesignationId);
        return dt;

    }
    //protected void gridocaccessright1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gridocaccessright1.PageIndex = e.NewPageIndex;
    //    flgrd();
    //}
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlmsg.Visible = false;
        filldeprdesg();
        DropDownList1_SelectedIndexChanged(sender, e);
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;
            pnlmsg.Visible = false;
            gridocaccessright1.Enabled = false;

        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            gridocaccessright1.Enabled = true;
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
    protected void gridocaccessright1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        flgrd();
    }
    protected void HeaderChkboxPrint_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)gridocaccessright1.HeaderRow.Cells[3].Controls[1];
        for (int i = 0; i < gridocaccessright1.Rows.Count; i++)
        {
            CheckBox ch = (CheckBox)gridocaccessright1.Rows[i].Cells[3].Controls[1];
            ch.Checked = chk.Checked;
        }
    }
    protected void HeaderChkboxPrint1_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)gridocaccessright1.HeaderRow.Cells[4].Controls[1];
        for (int i = 0; i < gridocaccessright1.Rows.Count; i++)
        {
            CheckBox ch = (CheckBox)gridocaccessright1.Rows[i].Cells[4].Controls[1];
            ch.Checked = chk.Checked;
        }
    }
    protected void HeaderChkboxPrint2_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)gridocaccessright1.HeaderRow.Cells[5].Controls[1];
        for (int i = 0; i < gridocaccessright1.Rows.Count; i++)
        {
            CheckBox ch = (CheckBox)gridocaccessright1.Rows[i].Cells[5].Controls[1];
            ch.Checked = chk.Checked;
        }
    }

    protected void checkbox12_CheckedChanged(object sender, EventArgs e)
    {
        flgrd();
    }

    protected void filldeprdesg()
    {
        string str = "select DesignationMaster.DesignationMasterId,DepartmentmasterMNC.Departmentname + ' : ' + DesignationMaster.DesignationName as name FROM DepartmentmasterMNC INNER JOIN DesignationMaster ON DesignationMaster.DeptID = DepartmentmasterMNC.id where Companyid='" + Session["Comid"].ToString() + "' and Whid='" + ddlstore.SelectedValue + "' ORDER BY DepartmentmasterMNC.Departmentname,DesignationMaster.DesignationName";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        DropDownList1.DataSource = dt;
        DropDownList1.DataTextField = "name";
        DropDownList1.DataValueField = "DesignationMasterId";
        DropDownList1.DataBind();

        //DropDownList1.Items.Insert(0, "-Select-");
        //DropDownList1.SelectedItem.Value = "0";
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillddlemployee();
       // flgrd();
    }
}
