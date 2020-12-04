using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using ForAspNet.POP3;
using System.Text;
using System.Data.SqlClient;
public partial class WizardAutoAllocation : System.Web.UI.Page
{
    SqlConnection con;
    MasterCls clsMaster = new MasterCls();
    Companycls clsCompany = new Companycls();
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

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);



        lblmsg.Text = "";
        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            string str = "SELECT WareHouseId,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlbusiness.DataSource = dt;
            ddlbusiness.DataTextField = "Name";
            ddlbusiness.DataValueField = "WareHouseId";
            ddlbusiness.DataBind();

            ddlbusfilter.DataSource = dt;
            ddlbusfilter.DataTextField = "Name";
            ddlbusfilter.DataValueField = "WareHouseId";
            ddlbusfilter.DataBind();
            ddlbusfilter.Items.Insert(0, "All");
            ddlbusfilter.Items[0].Value = "0";
            lblcompany.Text = Session["Cname"].ToString();
            lblbusiness.Text = ddlbusfilter.SelectedItem.Text;
            string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
            SqlCommand cmdeeed = new SqlCommand(eeed, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);
            if (dteeed.Rows.Count > 0)
            {
                ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

            }
            RadioButtonList1_SelectedIndexChanged(sender, e);

            selectEMployeelist();
            FillGrid();
        }

    }
    protected void FillCompanyInfo()
    {
        DataTable DtMain = new DataTable();
        DtMain = clsCompany.selectCompanyMaster();
        if (DtMain.Rows.Count > 0)
        {

            RadioButtonList1.SelectedIndex = 0;
            FillGrid();
            if (RadioButtonList1.SelectedItem.Value == "True")
            {
                pnlIP.Visible = true;
                //txt1.Focus();
            }
            else
            {
                pnlIP.Visible = false;


            }

        }
    }

    protected void FillGridDesignationwithDept()
    {
        DataTable dt;
        dt = new DataTable();

        clsMaster = new MasterCls();
        dt = clsMaster.SelectDesignationMasterwithDataDept(ddlbusiness.SelectedValue);
        //dt = clsMaster.SelectDesignationMasterwithDept(6);
        grdDesignation.DataSource = dt;
        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        grdDesignation.DataBind();

    }

    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {

        Int32 insrt;
        insrt = 0;
        String strr = "";
        bool status = Convert.ToBoolean(RadioButtonList1.SelectedItem.Value);
        // Int32 result = clsCompany.UpdateCompanyMasterforAutoAllocation(status);
        if (status == true)
        {

            int Docgrd, Empgrd;
            int insdata;
            insdata = 0;
            Docgrd = 0;
            Empgrd = 0;

            {

                Empgrd = 0;
                do
                {
                    CheckBox chkEmployee = (CheckBox)grdEmployeeList.Rows[Empgrd].FindControl("chkEmployee");
                    insdata = 0;
                    if (chkEmployee.Checked == true)
                    {
                        string eeed = " Select distinct * from  AutoAllocationMaster where EmployeeId='" + Int32.Parse(grdEmployeeList.DataKeys[Empgrd].Value.ToString()) + "' and Whid='" + ddlbusiness.SelectedValue + "'";
                        SqlCommand cmdeeed = new SqlCommand(eeed, con);
                        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                        DataTable dteeed = new DataTable();
                        adpeeed.Fill(dteeed);
                        if (dteeed.Rows.Count <= 0)
                        {
                            insrt = 1;
                            //  insdata = clsDocument.insertDocumentProcessing(Int32.Parse(grdEmployeeList.DataKeys[Empgrd].Value.ToString()), docidd);
                            insdata = clsCompany.InsertAutoAllocation(Int32.Parse(grdEmployeeList.DataKeys[Empgrd].Value.ToString()), ddlbusiness.SelectedValue);
                        }
                    }
                    Empgrd = Empgrd + 1;
                } while (Empgrd <= grdEmployeeList.Rows.Count - 1);

            }
            if (insrt == 1)
            {

                lblmsg.Text = "Record inserted successfully.";

                FillGrid();
            }
            else
            {

                lblmsg.Text = "No one Employee is selected.";
            }

        }
        selectEMployeelist();
        btnadd.Visible = true;
        pnladd.Visible = false;

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedItem.Value == "True")
        {
            pnlIP.Visible = true;
            //txt1.Focus();
        }
        else
        {
            pnlIP.Visible = false;
            gridIPAddress.Visible = false;


        }
    }

    protected void FillGrid()
    {
        //DataTable dt = new DataTable();

        string str1 = "";

        string str2 = "";

        if (ddlbusfilter.SelectedIndex > 0)
        {
            str1 = " WareHouseMaster.Name as Wname, AutoAllocationMaster.EmployeeId AS EmployeeId,DepartmentmasterMNC.Companyid,DepartmentmasterMNC.Departmentid,DepartmentmasterMNC.Departmentname,DepartmentmasterMNC.id,DesignationMaster.DesignationName,DesignationMaster.RoleId, EmployeeMaster.*,WarehouseMaster_1.Name+':'+EmployeeMaster.EmployeeName as Ename FROM DesignationMaster inner join DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id inner join EmployeeMaster ON DesignationMaster.DesignationMasterId = EmployeeMaster.DesignationMasterId inner join WarehouseMaster as WarehouseMaster_1  on WarehouseMaster_1.WarehouseId= EmployeeMaster.Whid inner join AutoAllocationMaster ON EmployeeMaster.EmployeeMasterID = AutoAllocationMaster.EmployeeId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=AutoAllocationMaster.Whid where AutoAllocationMaster.CID='" + Session["Comid"] + "' and AutoAllocationMaster.Whid='" + ddlbusfilter.SelectedValue + "'";

            str2 = "select count(AutoAllocationMaster.EmployeeId) as ci FROM DesignationMaster inner join DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id RIGHT OUTER JOIN EmployeeMaster ON DesignationMaster.DesignationMasterId = EmployeeMaster.DesignationMasterId inner join WarehouseMaster as WarehouseMaster_1  on WarehouseMaster_1.WarehouseId= EmployeeMaster.Whid inner join AutoAllocationMaster ON EmployeeMaster.EmployeeMasterID = AutoAllocationMaster.EmployeeId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=AutoAllocationMaster.Whid where AutoAllocationMaster.CID='" + Session["Comid"] + "' and AutoAllocationMaster.Whid='" + ddlbusfilter.SelectedValue + "'";
        }
        else
        {
            str1 = " WareHouseMaster.Name as Wname, AutoAllocationMaster.EmployeeId AS Expr1,DepartmentmasterMNC.Companyid,DepartmentmasterMNC.Departmentid,DepartmentmasterMNC.Departmentname,DepartmentmasterMNC.id,DesignationMaster.DesignationName,DesignationMaster.RoleId, EmployeeMaster.*,WarehouseMaster_1.Name+':'+EmployeeMaster.EmployeeName as Ename FROM DesignationMaster inner join DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id inner join EmployeeMaster ON DesignationMaster.DesignationMasterId = EmployeeMaster.DesignationMasterId inner join WarehouseMaster as WarehouseMaster_1  on WarehouseMaster_1.WarehouseId= EmployeeMaster.Whid inner join AutoAllocationMaster ON EmployeeMaster.EmployeeMasterID = AutoAllocationMaster.EmployeeId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=AutoAllocationMaster.Whid where AutoAllocationMaster.CID='" + Session["Comid"] + "' ";

            str2 = "select count(AutoAllocationMaster.EmployeeId) as ci FROM DesignationMaster inner join DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id inner join EmployeeMaster ON DesignationMaster.DesignationMasterId = EmployeeMaster.DesignationMasterId inner join  WarehouseMaster as WarehouseMaster_1  on WarehouseMaster_1.WarehouseId= EmployeeMaster.Whid inner join AutoAllocationMaster ON EmployeeMaster.EmployeeMasterID = AutoAllocationMaster.EmployeeId inner join  WareHouseMaster on WareHouseMaster.WareHouseId=AutoAllocationMaster.Whid where AutoAllocationMaster.CID='" + Session["Comid"] + "' ";
        }

        gridIPAddress.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,DesignationMaster.DesignationName,WareHouseMaster_1.Name, EmployeeMaster.EmployeeName asc";

        // dt = clsCompany.selectAutoAllocationMaster(ddlbusfilter.SelectedValue);


        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(gridIPAddress.PageIndex, gridIPAddress.PageSize, sortExpression, str1);

            gridIPAddress.DataSource = dt;
            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            gridIPAddress.DataBind();
        }

        else
        {
            gridIPAddress.DataSource = null;
            gridIPAddress.DataBind();
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

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void gridIPAddress_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridIPAddress.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void gridIPAddress_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        //  Int32 indx = Convert.ToInt32(gridIPAddress.SelectedIndex.ToString());

        if (e.CommandName == "Delete1")
        {
            //Int32 id = Convert.ToInt32(gridIPAddress.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value);
            Int32 id = Convert.ToInt32(e.CommandArgument);

            hdncnfm.Value = id.ToString();

            imgconfirmok_Click(sender, e);


        }
    }
    protected void gridIPAddress_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdEmployeeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEmployeeList.PageIndex = e.NewPageIndex;
        selectEMployeelist();
    }
    protected void grdDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {

            lblmsg.Text = "Error in  DataBound : " + ex.Message.ToString();
        }

    }
    protected void grdEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {

            lblmsg.Text = "Error in databound : " + ex.Message.ToString();
        }
    }
    public void selectEMployeelist()
    {
        string stralready = " select EmployeeId from AutoAllocationMaster where Whid='" + ddlbusiness.SelectedValue + "'   ";
        SqlCommand cmdalready = new SqlCommand(stralready, con);
        SqlDataAdapter adpalready = new SqlDataAdapter(cmdalready);
        DataTable dtalready = new DataTable();
        adpalready.Fill(dtalready);

        string empid = "";
        if (dtalready.Rows.Count > 0)
        {
            empid = "and EmployeeMaster.EmployeeMasterID Not In (select EmployeeId from AutoAllocationMaster where Whid='" + ddlbusiness.SelectedValue + "' )";
        }
        else
        {
            empid = "";
        }
        string allbus = "";
        if (rdsetrul.SelectedIndex == 0)
        {
            allbus = "  EmployeeMaster.Whid in (SELECT Distinct WareHouseId  FROM WareHouseMaster  where comid = '" + Session["comid"] + "' and WareHouseMaster.Status='1') ";
        }
        else
        {

            allbus = " EmployeeMaster.Whid='" + ddlbusiness.SelectedValue + "'  ";
        }

        string str = " SELECT EmployeeMaster.EmployeeMasterID as EmployeeID, EmployeeMaster.DesignationMasterId as DesignationId, WarehouseMaster.Name +':'+ EmployeeMaster.EmployeeName as EmployeeName , DesignationMaster.DesignationName, EmployeeMaster.Address,    EmployeeMaster.City, EmployeeMaster.StateId as State, EmployeeMaster.CountryId as Country, EmployeeMaster.ContactNo as Phone,  EmployeeMaster.Email,     EmployeeMaster.DateOfJoin as DOJ, EmployeeMaster.SuprviserId,    EmployeeMaster.StatusMasterId as StatusId, DepartmentmasterMNC.DepartmentName FROM  WarehouseMaster inner join EmployeeMaster on EmployeeMaster.Whid=WarehouseMaster.WarehouseId INNER JOIN    DesignationMaster ON EmployeeMaster.DesignationMasterId = DesignationMaster.DesignationMasterId  inner join  DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id WHERE " + allbus + "  " + empid + "  "; //DesignationMaster.DesignationName in('Manager','Supervisor','Office Clerk')
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        grdEmployeeList.DataSource = dt;
        grdEmployeeList.DataBind();

        imgbtnsubmit.Visible = true;

    }
    protected void imgbShowEmp_Click(object sender, EventArgs e)
    {
        selectEMployeelist();
    }
    protected void imgconfirmok_Click(object sender, EventArgs e)
    {
        //  mdlpopupconfirm.Hide();
        int result = clsCompany.DeleteAutoAllocationMaster(Convert.ToInt32(hdncnfm.Value));
        lblmsg.Text = "Record Deleted Successfully.";
        FillGrid();
        selectEMployeelist();
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectEMployeelist();
    }
    protected void HeaderChkboxDes_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox HeaderChkboxDes = (CheckBox)grdDesignation.HeaderRow.FindControl("HeaderChkboxDes");

        foreach (GridViewRow grd in grdDesignation.Rows)
        {
            CheckBox chkDesignation = (CheckBox)grd.FindControl("chkDesignation");
            if (HeaderChkboxDes.Checked == true)
            {

                chkDesignation.Checked = true;
            }
            else
            {
                chkDesignation.Checked = false;
            }
        }

    }
    protected void HeaderChkboxEmp_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox HeaderChkboxEmp = (CheckBox)grdEmployeeList.HeaderRow.FindControl("HeaderChkboxEmp");

        foreach (GridViewRow grd in grdEmployeeList.Rows)
        {
            CheckBox chkEmployee = (CheckBox)grd.FindControl("chkEmployee");
            if (HeaderChkboxEmp.Checked == true)
            {

                chkEmployee.Checked = true;
            }
            else
            {
                chkEmployee.Checked = false;
            }
        }

    }
    protected void grdDesignation_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGridDesignationwithDept();
    }
    protected void grdEmployeeList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdEmployeeList_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        selectEMployeelist();
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
    protected void gridIPAddress_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGrid();
    }
    protected void ddlbusfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblcompany.Text = Session["Cname"].ToString();
        lblbusiness.Text = ddlbusfilter.SelectedItem.Text;
        FillGrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;

            gridIPAddress.AllowPaging = false;
            gridIPAddress.PageSize = 10000;
            FillGrid();

            if (gridIPAddress.Columns[3].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                gridIPAddress.Columns[3].Visible = false;
            }

        }
        else
        {
            gridIPAddress.AllowPaging = true;
            gridIPAddress.PageSize = 20;
            FillGrid();

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["deleHide"] != null)
            {
                gridIPAddress.Columns[3].Visible = true;
            }

        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            //Label6.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            //Label6.Visible = false;
        }
        btnadd.Visible = false;

        // Label6.Text = "Add New Document Cabinet";
        lblmsg.Text = "";

    }
    protected void rdsetrul_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlbusiness_SelectedIndexChanged(sender, e);
    }
}
