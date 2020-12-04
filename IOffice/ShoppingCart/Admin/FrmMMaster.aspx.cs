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

public partial class Admin_FrmMMaster : System.Web.UI.Page
{
    static int temp;
    static DataTable dt;
    string mnt;
    DocumentCls1 clsDocument = new DocumentCls1();
    DataByCompany obj = new DataByCompany();
    SqlConnection con;
    string compid;

    string currentmonth = System.DateTime.Now.Month.ToString();
    //  string adasdas = System.DateTime.Now.ToShortDateString();
    string currentyear = System.DateTime.Now.Year.ToString();
    DateTime lastday;
    //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((String)Session["RoleName"] == "")
        //{
        //    Response.Redirect(ResolveUrl("~/") + "\\Admin\\default.aspx");
        //}

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        pagetitleclass pg = new pagetitleclass();


        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        compid = Session["Comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
            if (Session["Comid"] == null)
            {
               Response.Redirect("~/Login.aspx");
            }
            ViewState["sortOrder"] = "";
            fillstore();
            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();
            if (dteeed.Rows.Count > 0)
            {
                ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
            filyear();
            RadioButtonList1_SelectedIndexChanged(sender, e);
            fillteryear();
            RadioButtonList2_SelectedIndexChanged(sender, e);
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlEmployeeBusiness.Visible = false;

        pnlmonthly1.Visible = false;
        pnlmonthly2.Visible = false;
        pnlmonthly3.Visible = false;

        pnlradio.Visible = false;
        pnlradio1.Visible = false;
        pnlradio2.Visible = false;

        Panel15.Visible = false;
        Panel16.Visible = false;

        lblwname0.Text = "Business-Department Name ";
        imgadddept.Visible = false;
        imgdeptrefresh.Visible = false;

        imgadddivision.Visible = false;
        imgdivisionrefresh.Visible = false;

        if (RadioButtonList1.SelectedValue == "0")
        {
            Panel5.Visible = true;

            pnldivision.Visible = false;
            pnlemployee.Visible = false;
            lblwname0.Text = "Business Name ";

            ddlemployee.Items.Clear();
            ddlbusiness.Items.Clear();
            //if (Convert.ToString(ViewState["cd"]) != "1")
            //{
            //  fillstore();
            //  }
            fillbusinessyear();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            pnlradio.Visible = true;

            pnldivision.Visible = false;
            imgadddept.Visible = true;
            imgdeptrefresh.Visible = true;
            pnlemployee.Visible = false;

            RadioButtonList3.SelectedValue = "0";
            RadioButtonList3_SelectedIndexChanged(sender, e);

            ddlemployee.Items.Clear();
            ddlbusiness.Items.Clear();
            //if (Convert.ToString(ViewState["cd"]) != "2")
            //{
            fillDepartment();
            //   }
            filldepartmentyear();
        }
        if (RadioButtonList1.SelectedValue == "2")//Division
        {
           


            pnlradio1.Visible = true;
            Panel15.Visible = true;
            //  pnldivision.Visible = true


            RadioButtonList4.SelectedValue = "0";
            RadioButtonList4_SelectedIndexChanged(sender, e);

            lblwname0.Text = "Business-Department-Division Name ";

            imgadddivision.Visible = true;
            imgdivisionrefresh.Visible = true;

            pnlemployee.Visible = false;
            ddlemployee.Items.Clear();

            RadioButtonList4.SelectedValue = "0";
            RadioButtonList4_SelectedIndexChanged(sender, e);

            //if (Convert.ToString(ViewState["cd"]) != "2")
            //{
          
            filldevesion();
             //  fillDepartment();
            //    }
            filldivisionyear();
             fillDivision();
        }
        if (RadioButtonList1.SelectedValue == "3")//Employee
        {
            pnlEmployeeBusiness.Visible = true;  

            pnlradio2.Visible = true;

            pnlemployee.Visible = true;
            pnldivision.Visible = false;
            ddlbusiness.Items.Clear();
            //if (Convert.ToString(ViewState["cd"]) != "2")

            RadioButtonList5.SelectedValue = "0";
            RadioButtonList5_SelectedIndexChanged(sender, e);
            //{
            //fillDepartment();
            //  }
            DDLwarehous();
            // ddldept();
            fillemployee();
            fillemployeeyear();
            // }            
        }
        if (RadioButtonList1.SelectedValue == "")
        {
            pnldivision.Visible = false;
            pnlemployee.Visible = false;
        }

        //filltgmain();
        ddlyear_SelectedIndexChanged1(sender, e);
        //ddly_SelectedIndexChanged(sender, e);

    }
    public void DDLwarehous()
    {
        string finalstr = " SELECT DISTINCT  dbo.WareHouseMaster.Name, dbo.WareHouseMaster.WareHouseId, dbo.WareHouseMaster.comid FROM dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid WHERE (dbo.WareHouseMaster.Status = 1) AND (dbo.DepartmentmasterMNC.Active = 1) AND dbo.WareHouseMaster.comid='" + Session["comid"] + "' ORDER BY dbo.WareHouseMaster.Name ";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        DDLonlyBusiness.DataSource = dtcln;
        DDLonlyBusiness.DataValueField = "WareHouseId";
        DDLonlyBusiness.DataTextField = "Name";
        DDLonlyBusiness.DataBind();
        DDLonlyBusiness.Items.Insert(0, "--Select--");
        DDLonlyBusiness.Items[0].Value = "0";
        ddlStore.Items.Clear();
    }
    protected void DDLonlyBusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStore.Items.Clear();
        if (DDLonlyBusiness.SelectedIndex > 0)
        {
            string employeeisstr = "";
            if (RadioButtonList1.SelectedValue == "3")
            {
                employeeisstr = "   and  dbo.DepartmentmasterMNC.id IN (SELECT DISTINCT dbo.EmployeeMaster.DeptID as id FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 and dbo.EmployeeMaster.Whid='" + DDLonlyBusiness.SelectedValue + "')";
            }
            //SELECT DISTINCT dbo.EmployeeMaster.DeptID FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 and dbo.EmployeeMaster.Whid='" + DDLonlyBusiness.SelectedValue + "'

            string finalstr = " SELECT dbo.DepartmentmasterMNC.id, dbo.DepartmentmasterMNC.Departmentname ,dbo.WareHouseMaster.comid FROM dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid WHERE (dbo.WareHouseMaster.Status = 1) AND (dbo.DepartmentmasterMNC.Active = 1) AND dbo.WareHouseMaster.comid='" + Session["comid"] + "' AND  dbo.WareHouseMaster.WareHouseId='" + DDLonlyBusiness.SelectedValue + "'  ORDER BY dbo.DepartmentmasterMNC.Departmentname ";
            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            DataTable dtcln = new DataTable();
            adpcln.Fill(dtcln);
            ddlStore.DataSource = dtcln;
            ddlStore.DataValueField = "id";
            ddlStore.DataTextField = "Departmentname";
            ddlStore.DataBind();
            ddlStore.Items.Insert(0, "--Select--");
            ddlStore.Items[0].Value = "0";

        }
        else
        {
            ddlStore.Items.Insert(0, "--Select--");
            ddlStore.Items[0].Value = "0";

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

   
    protected void fillstore()
    {
        ddlStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlStore.DataSource = ds;
        ddlStore.DataTextField = "Name";
        ddlStore.DataValueField = "WareHouseId";
        ddlStore.DataBind();

        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }

    protected void filyear()
    {
        ddlyear.Items.Clear();

        ddlyear.DataSource = obj.Tablemaster("Select * from Year where Name>='" + System.DateTime.Now.Year.ToString() + "'");
        ddlyear.DataMember = "Name";
        ddlyear.DataTextField = "Name";
        ddlyear.DataValueField = "Id";
        ddlyear.DataBind();
        ddlyear.Items.Insert(0, "-Select-");
        ddlyear.Items[0].Value = "0";
        ddlyear.SelectedIndex = ddlyear.Items.IndexOf(ddlyear.Items.FindByText(System.DateTime.Now.Year.ToString()));
    }
    public void fillDepartment()
    {


        ddlStore.Items.Clear();

        ViewState["cd"] = "2";
        DataTable dsemp = MainAcocount.SelectDepartmentmasterMNCwithCID();
        if (dsemp.Rows.Count > 0)
        {
            ddlStore.DataSource = dsemp;
            ddlStore.DataTextField = "Departmentname";
            ddlStore.DataValueField = "id";
            ddlStore.DataBind();
        }
        //ddlstore.Items.Insert(0, "-Select-");
        //ddlstore.Items[0].Value = "0";
        //ddlDepartment_SelectedIndexChanged(sender, e);

    }

   
    protected void fillemployee()
    {
        ddlemployee.Items.Clear();
        if (ddlStore.SelectedIndex > -1)
        {
            DataTable ds12 = clsDocument.SelectEmployeeMasterwithDivId("0", 1, ddlStore.SelectedValue);

            ddlemployee.DataSource = ds12;
            ddlemployee.DataTextField = "EmployeeName";
            ddlemployee.DataValueField = "EmployeeMasterID";
            ddlemployee.DataBind();
        }
        //ddlEmp.Items.Insert(0, "-Select-");
        //ddlEmp.Items[0].Value = "0";


    }


    protected void Filteremployee()
    {

        DropDownList3.Items.Clear();
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            DataTable ds12 = clsDocument.SelectEmployeeMasterwithDivId("0", 1, ddlsearchByStore.SelectedValue);

            DropDownList3.DataSource = ds12;
            DropDownList3.DataTextField = "EmployeeName";
            DropDownList3.DataValueField = "EmployeeMasterID";
            DropDownList3.DataBind();
        }
        DropDownList3.Items.Insert(0, "-Select-");
        DropDownList3.Items[0].Value = "0";

    }
    public void fillDivision()
    {
        ddlbusiness.Items.Clear();
        if (ddlStore.SelectedIndex > 0)
        {

            DataTable dt2 = clsDocument.SelectDivisionwithStoreIdanddeptId(ddlStore.SelectedValue, "0", 1);

            ddlbusiness.DataSource = dt2;
            ddlbusiness.DataMember = "businessname";
            ddlbusiness.DataTextField = "businessname";
            ddlbusiness.DataValueField = "BusinessID";
            ddlbusiness.DataBind();
        }
        //ddlbusiness.Items.Insert(0, "-Select-");
        //ddlbusiness.Items[0].Value = "0";


    }

    public void filterDivision()
    {

        DropDownList2.Items.Clear();
        if (ddlsearchByStore.SelectedIndex > 0)
        {

            DataTable dt2 = clsDocument.SelectDivisionwithStoreIdanddeptId(ddlsearchByStore.SelectedValue, "0", 1);

            DropDownList2.DataSource = dt2;
            DropDownList2.DataMember = "businessname";
            DropDownList2.DataTextField = "businessname";
            DropDownList2.DataValueField = "BusinessID";
            DropDownList2.DataBind();
        }
        DropDownList2.Items.Insert(0, "-Select-");
        DropDownList2.Items[0].Value = "0";
    }

    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            fillbusinessyear();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            if (RadioButtonList3.SelectedValue == "0")
            {
                filldepartmentyear();
            }
            if (RadioButtonList3.SelectedValue == "1")
            {
                filbusinessmonths();
            }
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            if (RadioButtonList4.SelectedValue == "0")
            {
                filldivisionyear();
            }
            if (RadioButtonList4.SelectedValue == "1")
            {
                if (DropDownList6.SelectedValue == "0")
                {
                    filbusinessmonths11();
                }
                if (DropDownList6.SelectedValue == "1")
                {
                    fildepartmentmonths();
                }
            }
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            fillemployee();
            fillemployeeyear();
        }
        //   filltgmain();
        ddly_SelectedIndexChanged(sender, e);
    }


    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {

        filltgmain();
        ddly_SelectedIndexChanged(sender, e);
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployeeyear();
        ddly_SelectedIndexChanged(sender, e);
    }

    //---------------Filters **-----------/////////////////////
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlEmpbusinessfilter.Visible = false;


        Panel12.Visible = false;
        paneldepartment.Visible = false;
        paneldivision.Visible = false;
        panelemployee.Visible = false;

        lblwnamefilter.Text = "Business-Department Name ";
        if (RadioButtonList2.SelectedValue == "0")
        {
            paneldepartment.Visible = true;

            Panel3.Visible = false;
            Panel4.Visible = false;

            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{
            FilterDepartment();
            //  }
            filterfilldepartmentyear();
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            paneldivision.Visible = true;

            lblwnamefilter.Text = "Business-Department-Division Name ";
            // Panel3.Visible = true;
            Panel4.Visible = false;
            DropDownList3.Items.Clear();

            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{
            //   FilterDepartment();
            filterfilldevesion();
            //  }
            filterfilldivisionyear();
            //   filterDivision();
        }
        if (RadioButtonList2.SelectedValue == "2")//Employee
        {
            pnlEmpbusinessfilter.Visible = true;

            panelemployee.Visible = true;

            Panel3.Visible = false;
            Panel4.Visible = true;
            DropDownList2.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{
            DDLwarehousFilter();
            DDLonlyBusinessFilter_SelectedIndexChanged(sender, e);
            //FilterDepartment();
            // }
            Filteremployee();
            filterfillemployeeyear();
            BindGrid();
        }
        if (RadioButtonList2.SelectedValue == "4")
        {
            Panel12.Visible = true;

            Panel3.Visible = false;
            Panel4.Visible = false;
            lblwnamefilter.Text = "Business Name ";

            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "1")
            //{
            fillstorewithfilter();
            // }
            filterfillbusinessyear();
        }
        DropDownList1_SelectedIndexChanged1(sender, e);
        //  filltg();
        BindGrid();
    }
    public void DDLwarehousFilter()
    {
        string finalstr = " SELECT DISTINCT  dbo.WareHouseMaster.Name, dbo.WareHouseMaster.WareHouseId, dbo.WareHouseMaster.comid FROM dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid WHERE (dbo.WareHouseMaster.Status = 1) AND (dbo.DepartmentmasterMNC.Active = 1) AND dbo.WareHouseMaster.comid='" + Session["comid"] + "' ORDER BY dbo.WareHouseMaster.Name ";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        DDLonlyBusinessFilter.DataSource = dtcln;
        DDLonlyBusinessFilter.DataValueField = "WareHouseId";
        DDLonlyBusinessFilter.DataTextField = "Name";
        DDLonlyBusinessFilter.DataBind();
        ddlsearchByStore.Items.Clear();
    }
    protected void DDLonlyBusinessFilter_SelectedIndexChanged(object sender, EventArgs e)
    {

        string employeeisstr = "";
        if (RadioButtonList2.SelectedValue == "3")
        {
            employeeisstr = "   and  dbo.DepartmentmasterMNC.id IN (SELECT DISTINCT dbo.EmployeeMaster.DeptID as id FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 and dbo.EmployeeMaster.Whid='" + DDLonlyBusinessFilter.SelectedValue + "')";
        }
        //SELECT DISTINCT dbo.EmployeeMaster.DeptID FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 and dbo.EmployeeMaster.Whid='" + DDLonlyBusiness.SelectedValue + "'

        string finalstr = " SELECT dbo.DepartmentmasterMNC.id, dbo.DepartmentmasterMNC.Departmentname ,dbo.WareHouseMaster.comid FROM dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid WHERE (dbo.WareHouseMaster.Status = 1) AND (dbo.DepartmentmasterMNC.Active = 1) AND dbo.WareHouseMaster.comid='" + Session["comid"] + "' AND  dbo.WareHouseMaster.WareHouseId='" + DDLonlyBusinessFilter.SelectedValue + "'  ORDER BY dbo.DepartmentmasterMNC.Departmentname ";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlsearchByStore.DataSource = dtcln;
        ddlsearchByStore.DataValueField = "id";
        ddlsearchByStore.DataTextField = "Departmentname";
        ddlsearchByStore.DataBind();
      
        

    }
    protected void fillstorewithfilter()
    {
        ddlsearchByStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();

        ViewState["cdf"] = "1";
        ddlsearchByStore.DataSource = ds;
        ddlsearchByStore.DataTextField = "Name";
        ddlsearchByStore.DataValueField = "WareHouseId";
        ddlsearchByStore.DataBind();


        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            // ddlsearchByStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        //ddlsearchByStore.Items.Insert(0, "All");
        //ddlsearchByStore.Items[0].Value = "0";
    }
    public void FilterDepartment()
    {
        ddlsearchByStore.Items.Clear();

        ViewState["cdf"] = "2";
        DataTable dsemp = MainAcocount.SelectDepartmentmasterMNCwithCID();
        if (dsemp.Rows.Count > 0)
        {
            ddlsearchByStore.DataSource = dsemp;
            ddlsearchByStore.DataTextField = "Departmentname";
            ddlsearchByStore.DataValueField = "id";
            ddlsearchByStore.DataBind();
        }

        ddlsearchByStore.Items.Insert(0, "-Select-");
        ddlsearchByStore.Items[0].Value = "0";
    }
    //-------------------------------------------------/////////////////--------/*-*/-----

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDoucmentMasterByIDwithobj(Convert.ToInt32(e.CommandArgument));

            string docname = dt.Rows[0]["DocumentName"].ToString();
            string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);
            string name = docname.Trim();
            string extension = name.Substring(name.Length - 3);
            if (Convert.ToString(extension) == "pdf")
            {
                Session["ABCDE"] = "ABCDE";

                //                    string popupScript = "<script language='javascript'>" +
                //"newWindow=window.open('ViewDocument.aspx?id='" + e.CommandArgument + ", 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" + "</script>";
                int docid = 0;
                docid = Convert.ToInt32(e.CommandArgument);

                String temp = "ViewDocument.aspx?id=" + docid + "&Siddd=VHDS";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);
                  Response.Redirect("ViewDocument.aspx?id=" + docid + "&Siddd=VHDS");
            }
            else
            {
                FileInfo file = new FileInfo(filepath);

                if (file.Exists)
                {
                    //Response.ClearContent();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = ReturnExtension(file.Extension.ToLower());
                    //Response.TransmitFile(file.FullName);

                    //Response.End();
                    String temp = "http://license.busiwiz.com/ioffice/Account/" + Session["comid"] + "/UploadedDocuments//" + docname + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('http://license.busiwiz.com/Account/" + Session["comid"] + "/UploadedDocuments/" + docname + "');", true);

                }
            }
        }
    }



    protected void ddlsearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (RadioButtonList2.SelectedValue == "4")
        {
            filterfillbusinessyear();
        }
        if (RadioButtonList2.SelectedValue == "0")
        {
            if (dropdowndepartment.SelectedValue == "1")
            {
                filterfilldepartmentyear();
            }
            if (dropdowndepartment.SelectedValue == "2")
            {
                filterfilbusinessmonths();
            }
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            if (dropdowndivision.SelectedValue == "1")
            {
                filterfilldivisionyear();

            }
            if (dropdowndivision.SelectedValue == "2")
            {
                filterfilbusinessmonths111();

            }
            if (dropdowndivision.SelectedValue == "3")
            {
                filterfildepartmentmonths();

            }
            if (dropdowndivision.SelectedValue == "0")
            {

            }
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
            Filteremployee();

            if (dropdownemployee.SelectedValue == "1")
            {
                filterfillemployeeyear();

            }
            if (dropdownemployee.SelectedValue == "2")
            {
                filterfilbusinessmonths();

            }
            if (dropdownemployee.SelectedValue == "3")
            {
                filterfildepartmentmonths11();

            }
            if (dropdownemployee.SelectedValue == "4")
            {
                filterfilemployeemonths();

            }
            if (dropdownemployee.SelectedValue == "0")
            {

            }
        }

        BindGrid();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        filltg();
        BindGrid();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        filltg();
        BindGrid();
    }
    protected void filltgmain()
    {
        ddly.Items.Clear();
        string bus = "0";
        string em = "0";
        if (ddlemployee.SelectedIndex != -1)
        {
            em = ddlemployee.SelectedValue;
        }
        else
        {
            em = "0";
        }
        if (ddlbusiness.SelectedIndex != -1)
        {
            bus = ddlbusiness.SelectedValue;
        }
        else
        {
            bus = "0";
        }

        int flag = 0;
        if (RadioButtonList1.SelectedIndex == 0)
        {
            flag = 0;
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            flag = 1;
        }
        else if (RadioButtonList1.SelectedIndex == 2)
        {
            flag = 2;
        }
        else if (RadioButtonList1.SelectedIndex == 3)
        {
            flag = 3;
        }
        DataTable ds12 = new DataTable();
        if (RadioButtonList1.SelectedIndex == 0)
        {
            ds12 = ClsMMaster.Selectyddfilter(ddlStore.SelectedValue, "0", bus, em, flag);
        }
        else
        {
            ds12 = ClsMMaster.Selectyddfilter("0", ddlStore.SelectedValue, bus, em, flag);

        }
        if (ds12.Rows.Count > 0)
        {
            ddly.DataSource = ds12;

            ddly.DataMember = "Title1";
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "masterid";


            ddly.DataBind();


        }



    }
    protected void filltg()
    {

        ddlyfilter.Items.Clear();
        int flag = 0;
        if (RadioButtonList2.SelectedIndex == 0)
        {
            flag = 0;
        }
        else if (RadioButtonList2.SelectedIndex == 1)
        {
            flag = 1;
        }
        else if (RadioButtonList2.SelectedIndex == 2)
        {
            flag = 2;
        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            flag = 3;
        }
        DataTable ds12 = new DataTable();
        if (RadioButtonList2.SelectedIndex == 0)
        {
            ds12 = ClsMMaster.Selectyddfilter(ddlsearchByStore.SelectedValue, "0", DropDownList2.SelectedValue, DropDownList3.SelectedValue, flag);
        }
        else
        {
            ds12 = ClsMMaster.Selectyddfilter("0", ddlsearchByStore.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, flag);

        }
        if (ds12.Rows.Count > 0)
        {
            ddlyfilter.DataSource = ds12;

            ddlyfilter.DataMember = "Title1";
            ddlyfilter.DataTextField = "Title1";
            ddlyfilter.DataValueField = "masterid";


            ddlyfilter.DataBind();


        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";


    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropdownemployee.SelectedValue == "1")
        {
            filterfillemployeeyear();

        }
        if (dropdownemployee.SelectedValue == "2")
        {
            filterfilbusinessmonths();

        }
        if (dropdownemployee.SelectedValue == "3")
        {
            filterfildepartmentmonths11();

        }
        if (dropdownemployee.SelectedValue == "4")
        {
            filterfilemployeemonths();

        }
        if (dropdownemployee.SelectedValue == "0")
        {

        }
        BindGrid();
    }


    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {



       // string whid = "";
       // Int32 success = 0;
        string bus = "0";
        string em = "0";
        statuslable.Text = "";
        if (ddlemployee.SelectedIndex != -1)
        {
            em = ddlemployee.SelectedValue;
        }
        else
        {
            em = "0";
        }
        if (ddlbusiness.SelectedIndex != -1)
        {
            bus = ddlbusiness.SelectedValue;
        }
        else
        {
            bus = "0";
        }
        string part = "0";
        string wg = "0";
        string mg = "0";
        //if (ddlparty.SelectedIndex != -1)
        //{
        //    part = ddlparty.SelectedValue;
        //}
        //else
        //{
        //    part = "0";
        //}
        //if (ddlw.SelectedIndex != -1)
        //{
        //    wg = ddlw.SelectedValue;
        //}
        //else
        //{
        //    wg = "0";
        //}
        //if (ddlm.SelectedIndex != -1)
        //{
        //    mg = ddlm.SelectedValue;
        //}
        //else
        //{
        //    mg = "0";
        //}
        //string forkey;
        //if (ddlstatus.SelectedValue == "Pending")
        //{
        //    forkey = "0";
        //}
        //else
        //{
        //    forkey = "1";
        //}









        int currentyear = Convert.ToInt32(System.DateTime.Now.Year);
        int currentmonth = Convert.ToInt32(System.DateTime.Now.Month);

        int noofdays = DateTime.DaysInMonth(currentyear, currentmonth);

        lastday = new DateTime(currentyear, currentmonth, noofdays);

        ViewState["lastday"] = lastday;
        string startdate = System.DateTime.Now.ToShortDateString();
        string enddate = lastday.ToShortDateString();



        //DataTable df = MainAcocount.SelectWhidwithdeptid(ddlStore.SelectedValue);
        // string whid = Convert.ToString(df.Rows[0]["Whid"]);



        bool access = UserAccess.Usercon("MMaster", "", "MasterId", "", "", "WareHouseMaster.comid", "MMaster inner join WareHouseMaster on WareHouseMaster.warehouseid=MMaster.BusinessID");
        if (access == true)
        {

            string insert1 = "";
            int success = 0;
            string insertproject = "";
            //  int success = ClsMMaster.SpMMasterAddData(Convert.ToString(ddly.SelectedValue), txttitle.Text, txtdescription.Text, Convert.ToString(ddlmonth.SelectedValue), txtbudgetedamount.Text);
            // Response.Write(success);


            if (RadioButtonList1.SelectedValue == "0")
            {
                insert1 = "insert into MMaster (BusinessID,title,ymasterid,description,month,budgetedcost,StatusId) values('" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(ddlmonth.SelectedValue) + "','" + txtbudgetedamount.Text + "',192)";

                if (CheckBox1.Checked == true)
                {
                    insertproject = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txttitle.Text + "', 'Pending', '" + startdate + " ','" + enddate + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + Convert.ToString(em) + "','0','" + ddlStore.SelectedValue + "','false', '" + part + "',1)";
                }
                }

            if (RadioButtonList1.SelectedValue == "1")
            {
                int int1 = Convert.ToInt32(ddlStore.SelectedValue);
                SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlStore.SelectedValue + "'", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                ViewState["att1"] = dt1.Rows[0]["WareHouseId"].ToString();

                if (RadioButtonList3.SelectedValue == "0")
                {
                    insert1 = "insert into MMaster (BusinessID,DepartmentID,title,ymasterid,description,month,budgetedcost,StatusId) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(ddlmonth.SelectedValue) + "','" + txtbudgetedamount.Text + "',192)";
                }
                if (RadioButtonList3.SelectedValue == "1")
                {
                    insert1 = "insert into MMaster (BusinessID,DepartmentID,title,parentmonthlygoalid,description,month,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddlbusimonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(ddlmonth.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Busi')";
                }

                if (CheckBox1.Checked == true)
                {
                    insertproject = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txttitle.Text + "', 'Pending', '" + startdate + " ','" + enddate + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + Convert.ToString(em) + "','" + ddlStore.SelectedValue + "','" + dt1.Rows[0]["WareHouseId"].ToString() + "','false', '" + part + "',1)";
                }

            }
            if (RadioButtonList1.SelectedValue == "2")
            {
                int int1 = Convert.ToInt32(ddlStore.SelectedValue);
                SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId,DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlStore.SelectedValue + "'", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                ViewState["att2"] = dt1.Rows[0]["WareHouseId"].ToString();

                if (RadioButtonList4.SelectedValue == "0")
                {
                    insert1 = "insert into MMaster (BusinessID,DepartmentID,Divisionid,title,ymasterid,description,month,budgetedcost,StatusId) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + dt1.Rows[0]["id"].ToString() + "','" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(ddlmonth.SelectedValue) + "','" + txtbudgetedamount.Text + "',192)";
                }
                if (RadioButtonList4.SelectedValue == "1")
                {
                    if (DropDownList6.SelectedValue == "0")
                    {
                        insert1 = "insert into MMaster (BusinessID,DepartmentID,Divisionid,title,parentmonthlygoalid,description,month,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + dt1.Rows[0]["id"].ToString() + "','" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddlbusimonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(ddlmonth.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Busi')";
                    }
                    if (DropDownList6.SelectedValue == "1")
                    {
                        insert1 = "insert into MMaster (BusinessID,DepartmentID,Divisionid,title,parentmonthlygoalid,description,month,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + dt1.Rows[0]["id"].ToString() + "','" + ddlStore.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddldeptmonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(ddlmonth.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Dept')";
                    }
                }
                if (CheckBox1.Checked == true)
                {
                    insertproject = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txttitle.Text + "', 'Pending', '" + startdate + " ','" + enddate + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + Convert.ToString(em) + "','" + ddlStore.SelectedValue + "','" + dt1.Rows[0]["WareHouseId"].ToString() + "','false', '" + part + "',1)";
                }

            }

            if (RadioButtonList1.SelectedValue == "3")
            {
                int int1 = Convert.ToInt32(ddlStore.SelectedValue);
                SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlStore.SelectedValue + "'", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                ViewState["att3"] = dt1.Rows[0]["WareHouseId"].ToString();

                if (RadioButtonList5.SelectedValue == "0")
                {
                    insert1 = "insert into MMaster (BusinessID,DepartmentID,Divisionid,Employeeid,title,ymasterid,description,month,budgetedcost,StatusId) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "',0,'" + ddlemployee.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(ddlmonth.SelectedValue) + "','" + txtbudgetedamount.Text + "',192)";
                }
                if (RadioButtonList5.SelectedValue == "1")
                {
                    if (DropDownList7.SelectedValue == "0")
                    {
                        insert1 = "insert into MMaster (BusinessID,DepartmentID,Divisionid,Employeeid,title,parentmonthlygoalid,description,month,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "',0,'" + ddlemployee.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddlbusimonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(ddlmonth.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Busi')";
                    }
                    if (DropDownList7.SelectedValue == "1")
                    {
                        insert1 = "insert into MMaster (BusinessID,DepartmentID,Divisionid,Employeeid,title,parentmonthlygoalid,description,month,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "',0,'" + ddlemployee.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddldeptmonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(ddlmonth.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Dept')";
                    }
                    if (DropDownList7.SelectedValue == "2")
                    {
                        insert1 = "insert into MMaster (BusinessID,DepartmentID,Divisionid,Employeeid,title,parentmonthlygoalid,description,month,budgetedcost,StatusId,TypeofMonthlyGoal) values('" + dt1.Rows[0]["WareHouseId"].ToString() + "','" + ddlStore.SelectedValue + "',0,'" + ddlemployee.SelectedValue + "','" + txttitle.Text + "','" + Convert.ToString(ddldivimonthly.SelectedValue) + "','" + txtdescription.Text + "','" + Convert.ToString(ddlmonth.SelectedValue) + "','" + txtbudgetedamount.Text + "',192,'Divi')";
                    }
                }

                if (CheckBox1.Checked == true)
                {
                    insertproject = "insert into projectmaster (businessid,projectname,status,estartdate,eenddate,percentage,ltgmasterid,stgmasterid,ygmasterid,mgmasterid,wtmasterid,strategyid,tacticid,description,budgetedamount,EmployeeID,DeptId,Whid,Addjob,PartyId,RelatedProjectID) values ('" + Convert.ToString(bus) + "', '" + txttitle.Text + "', 'Pending', '" + startdate + " ','" + enddate + "',0,0,0,0,'" + Convert.ToString(mg) + "','" + Convert.ToString(wg) + "',0,0,'" + txtdescription.Text + "','" + txtbudgetedamount.Text + "','" + ddlemployee.SelectedItem.Value + "','" + ddlStore.SelectedValue + "','" + dt1.Rows[0]["WareHouseId"].ToString() + "','false', '" + part + "',1)";
                    SqlCommand cmd1 = new SqlCommand(insertproject, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();
                
                }



            }

            SqlCommand cmd = new SqlCommand(insert1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            
           

            SqlDataAdapter daf = new SqlDataAdapter("select max(MasterId) as MasterId from Mmaster", con);
            DataTable dtf = new DataTable();
            daf.Fill(dtf);

            if (dtf.Rows.Count > 0)
            {
                success = Convert.ToInt32(dtf.Rows[0]["MasterId"]);
            }

            if (success > 0)
            {
                statuslable.Text = "Record inserted successfully";
                if (chk.Checked == true)
                {
                    string te = "";
                    if (RadioButtonList1.SelectedValue == "0")
                    {
                        te = "AddDocMaster.aspx?mom=" + success + "&storeid=" + ddlStore.SelectedValue + "";
                    }
                    if (RadioButtonList1.SelectedValue == "1")
                    {
                        te = "AddDocMaster.aspx?mom=" + success + "&storeid=" + ViewState["att1"] + "";
                    }
                    if (RadioButtonList1.SelectedValue == "2")
                    {
                        te = "AddDocMaster.aspx?mom=" + success + "&storeid=" + ViewState["att2"] + "";
                    }
                    if (RadioButtonList1.SelectedValue == "3")
                    {
                        te = "AddDocMaster.aspx?mom=" + success + "&storeid=" + ViewState["att3"] + "";
                    }
                    //string te = "AddDocMaster.aspx?mom=" + success + "&storeid=" + whid + "";


                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


                }
                BindGrid();
            }
            else if (success == 0)
            {
                statuslable.Text = "Record already existed";
            }
            else
            {
                statuslable.Text = "Record not inserted successfully";
            }

        }
        else
        {
            statuslable.Text = "";
            statuslable.Text = "Sorry, you don't permitted greater record to priceplan";
        }


        EmptyControls();
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {


        string bus = "0";
        string em = "0";
        statuslable.Text = "";
        if (ddlemployee.SelectedIndex != -1)
        {
            em = ddlemployee.SelectedValue;
        }
        else
        {
            em = "0";
        }
        if (ddlbusiness.SelectedIndex != -1)
        {
            bus = ddlbusiness.SelectedValue;
        }
        else
        {
            bus = "0";
        }
        string part = "0";
        string wg = "0";
        string mg = "0";
        //if (ddlparty.SelectedIndex != -1)
        //{
        //    part = ddlparty.SelectedValue;
        //}
        //else
        //{
        //    part = "0";
        //}
        //if (ddlw.SelectedIndex != -1)
        //{
        //    wg = ddlw.SelectedValue;
        //}
        //else
        //{
        //    wg = "0";
        //}
        //if (ddlm.SelectedIndex != -1)
        //{
        //    mg = ddlm.SelectedValue;
        //}
        //else
        //{
        //    mg = "0";
        //}
        //string forkey;
        //if (ddlstatus.SelectedValue == "Pending")
        //{
        //    forkey = "0";
        //}
        //else
        //{
        //    forkey = "1";
        //}









        int currentyear = Convert.ToInt32(System.DateTime.Now.Year);
        int currentmonth = Convert.ToInt32(System.DateTime.Now.Month);

        int noofdays = DateTime.DaysInMonth(currentyear, currentmonth);

        lastday = new DateTime(currentyear, currentmonth, noofdays);

        ViewState["lastday"] = lastday;
        string startdate = System.DateTime.Now.ToShortDateString();
        string enddate = lastday.ToShortDateString();



        bool access = UserAccess.Usercon("MMaster", "", "MasterId", "", "", "WareHouseMaster.comid", "MMaster inner join WareHouseMaster on WareHouseMaster.warehouseid=MMaster.BusinessID");
        if (access == true)
        {

            int success = 0;
            //if (RadioButtonList1.SelectedValue == "0")
            //{
            //    success = ClsMMaster.SpMMasterUpdateData(hdnid.Value, Convert.ToString(ddly.SelectedValue), txttitle.Text, txtdescription.Text, Convert.ToString(ddlmonth.SelectedValue), txtbudgetedamount.Text);
            //}

            if (RadioButtonList1.SelectedValue == "0")
            {
                string update1 = " update MMaster set BusinessID='" + ddlStore.SelectedValue + "',title='" + txttitle.Text + "',YMasterId='" + Convert.ToString(ddly.SelectedValue) + "',description='" + txtdescription.Text + "',month='" + Convert.ToString(ddlmonth.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "' where masterid='" + ViewState["updateid"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(update1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                success = Convert.ToInt32(ViewState["updateid"].ToString());


            }

            if (RadioButtonList1.SelectedValue == "1")
            {
                string update1 = "";

                int int1 = Convert.ToInt32(ddlStore.SelectedValue);
                SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlStore.SelectedValue + "'", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                ViewState["attach1"] = dt1.Rows[0]["WareHouseId"].ToString();

                if (RadioButtonList3.SelectedValue == "0")
                {
                    update1 = " update MMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "', title='" + txttitle.Text + "',YMasterId='" + Convert.ToString(ddly.SelectedValue) + "',description='" + txtdescription.Text + "',month='" + Convert.ToString(ddlmonth.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "' where masterid='" + ViewState["updateid"].ToString() + "'";
                }
                if (RadioButtonList3.SelectedValue == "1")
                {
                    update1 = " update MMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "', title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddlbusimonthly.SelectedValue) + "',description='" + txtdescription.Text + "',month='" + Convert.ToString(ddlmonth.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "' where masterid='" + ViewState["updateid"].ToString() + "'";
                }
                SqlCommand cmd = new SqlCommand(update1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                success = Convert.ToInt32(ViewState["updateid"].ToString());


              





            }

            if (RadioButtonList1.SelectedValue == "2")
            {
                string update1 = "";

                int int1 = Convert.ToInt32(ddlStore.SelectedValue);
                SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId,DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlStore.SelectedValue + "'", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                ViewState["attach2"] = dt1.Rows[0]["WareHouseId"].ToString();

                if (RadioButtonList4.SelectedValue == "0")
                {
                    update1 = " update MMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + dt1.Rows[0]["id"].ToString() + "',Divisionid='" + ddlStore.SelectedValue + "',title='" + txttitle.Text + "',YMasterId='" + Convert.ToString(ddly.SelectedValue) + "',description='" + txtdescription.Text + "',month='" + Convert.ToString(ddlmonth.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "' where masterid='" + ViewState["updateid"].ToString() + "'";
                }
                if (RadioButtonList4.SelectedValue == "1")
                {
                    if (DropDownList6.SelectedValue == "0")
                    {
                        update1 = " update MMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + dt1.Rows[0]["id"].ToString() + "',Divisionid='" + ddlStore.SelectedValue + "',title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddlbusimonthly.SelectedValue) + "',description='" + txtdescription.Text + "',month='" + Convert.ToString(ddlmonth.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "',TypeofMonthlyGoal='Busi' where masterid='" + ViewState["updateid"].ToString() + "'";
                    }
                    if (DropDownList6.SelectedValue == "1")
                    {
                        update1 = " update MMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + dt1.Rows[0]["id"].ToString() + "',Divisionid='" + ddlStore.SelectedValue + "',title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddldeptmonthly.SelectedValue) + "',description='" + txtdescription.Text + "',month='" + Convert.ToString(ddlmonth.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "',TypeofMonthlyGoal='Dept' where masterid='" + ViewState["updateid"].ToString() + "'";
                    }
                }
                SqlCommand cmd = new SqlCommand(update1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                success = Convert.ToInt32(ViewState["updateid"].ToString());
            }

            if (RadioButtonList1.SelectedValue == "3")
            {
                string update1 = "";

                int int1 = Convert.ToInt32(ddlStore.SelectedValue);
                SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlStore.SelectedValue + "'", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                ViewState["attach3"] = dt1.Rows[0]["WareHouseId"].ToString();

                if (RadioButtonList5.SelectedValue == "0")
                {
                    update1 = " update MMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "',Employeeid='" + ddlemployee.SelectedValue + "',title='" + txttitle.Text + "',YMasterId='" + Convert.ToString(ddly.SelectedValue) + "',description='" + txtdescription.Text + "',month='" + Convert.ToString(ddlmonth.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "' where masterid='" + ViewState["updateid"].ToString() + "'";
                }
                if (RadioButtonList5.SelectedValue == "1")
                {
                    if (DropDownList7.SelectedValue == "0")
                    {
                        update1 = " update MMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "',Employeeid='" + ddlemployee.SelectedValue + "',title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddlbusimonthly.SelectedValue) + "',description='" + txtdescription.Text + "',month='" + Convert.ToString(ddlmonth.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "',TypeofMonthlyGoal='Busi' where masterid='" + ViewState["updateid"].ToString() + "'";
                    }
                    if (DropDownList7.SelectedValue == "1")
                    {
                        update1 = " update MMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "',Employeeid='" + ddlemployee.SelectedValue + "',title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddldeptmonthly.SelectedValue) + "',description='" + txtdescription.Text + "',month='" + Convert.ToString(ddlmonth.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "',TypeofMonthlyGoal='Dept' where masterid='" + ViewState["updateid"].ToString() + "'";
                    }
                    if (DropDownList7.SelectedValue == "2")
                    {
                        update1 = " update MMaster set BusinessID='" + dt1.Rows[0]["WareHouseId"].ToString() + "',DepartmentID='" + ddlStore.SelectedValue + "',Employeeid='" + ddlemployee.SelectedValue + "',title='" + txttitle.Text + "',parentmonthlygoalid='" + Convert.ToString(ddldivimonthly.SelectedValue) + "',description='" + txtdescription.Text + "',month='" + Convert.ToString(ddlmonth.SelectedValue) + "',budgetedcost='" + txtbudgetedamount.Text + "',TypeofMonthlyGoal='Divi' where masterid='" + ViewState["updateid"].ToString() + "'";
                    }
                }
                SqlCommand cmd = new SqlCommand(update1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                success = Convert.ToInt32(ViewState["updateid"].ToString());
            }

            if (success > 0)
            {
                if (chk.Checked == true)
                {

                    string te = "";
                    if (RadioButtonList1.SelectedValue == "0")
                    {
                        te = "AddDocMaster.aspx?mom=" + success + "&storeid=" + ddlStore.SelectedValue + "";
                    }
                    if (RadioButtonList1.SelectedValue == "1")
                    {
                        te = "AddDocMaster.aspx?mom=" + success + "&storeid=" + ViewState["attach1"] + "";
                    }
                    if (RadioButtonList1.SelectedValue == "2")
                    {
                        te = "AddDocMaster.aspx?mom=" + success + "&storeid=" + ViewState["attach2"] + "";
                    }
                    if (RadioButtonList1.SelectedValue == "3")
                    {
                        te = "AddDocMaster.aspx?mom=" + success + "&storeid=" + ViewState["attach3"] + "";
                    }


                    //string te = "AddDocMaster.aspx?mom=" + hdnid.Value + "&storeid=" + whid + "";

                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }
                statuslable.Text = "Record updated successfully";
                BindGrid();
            }
            else
            {
                statuslable.Text = "Record already existed";
            }

        }
        else
        {
            statuslable.Text = "";
            statuslable.Text = "Sorry, you don't permitted greater record to priceplan";
        }

        EmptyControls();

        btncancel.Visible = true;
        btnupdate.Visible = false;

        btnsubmit.Visible = true;
        btnreset.Visible = false;
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        lbllegend.Text = "";
        statuslable.Text = "";
        EmptyControls();
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
    }


    protected void btncancel_Click(object sender, EventArgs e)
    {
        EmptyControls();
        BindGrid();
        lbllegend.Text = "";
        statuslable.Text = "";
        btncancel.Visible = true;
        btnupdate.Visible = false;

        btnsubmit.Visible = true;
        btnreset.Visible = false;
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
    }


    private void EmptyControls()
    {
        txttitle.Text = "";
        chk.Checked = false;
        Button2.Checked = false;
        Pnl1.Visible = false;
        txtdescription.Text = "";
        txtbudgetedamount.Text = "0";
    }

    private void BindGrid()
    {

        string st1 = "";
        string strw = "";

        lblBusiness.Text = "";
        lblBusiness1.Text = "";
        lblDepartmemnt.Text = "";
        lblDivision.Text = "";
        lblEmp.Text = "";

        if (RadioButtonList2.SelectedIndex == 0)
        {
            lblcompany.Text = Session["Comid"].ToString();
            lblBusiness.Text = ddlsearchByStore.SelectedItem.Text;
            lblBusiness1.Text = "Business Name : " + ddlsearchByStore.SelectedItem.Text;
            if (ddlsearchByStore.SelectedIndex > -1)
            {
                st1 = " and ObjectiveMaster.StoreId='" + ddlsearchByStore.SelectedValue + "' and ObjectiveMaster.DepartmentId='0' and objectivemaster.businessid='0' and objectiveMaster.EmployeeId='0'";
            }
            else
            {
                st1 = " and  ObjectiveMaster.DepartmentId='0' and objectivemaster.businessid='0' and objectiveMaster.EmployeeId='0'";

            }
        }
        else
        {
            if (ddlsearchByStore.SelectedIndex > 0)
            {
                string[] separator1 = new string[] { ":" };
                string[] strSplitArr1 = ddlsearchByStore.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                lblcompany.Text = Session["Comid"].ToString();
                lblBusiness.Text = strSplitArr1[0].ToString();
                //   lblBusiness1.Text = "Business Name : " + strSplitArr1[0].ToString();
                //  lblDepartmemnt.Text = "Department : " + strSplitArr1[1].ToString();
            }
            else
            {
                lblcompany.Text = Session["Comid"].ToString();
                lblBusiness.Text = ddlsearchByStore.SelectedItem.Text;
                //  lblBusiness1.Text = "Business Name : " + ddlsearchByStore.SelectedItem.Text;

            }
        }
        if (RadioButtonList2.SelectedIndex == 1)
        {


            lblDepartmemnt.Text = "Business:Department - " + Convert.ToString(ddlsearchByStore.SelectedItem.Text);
            if (ddlsearchByStore.SelectedValue == "0")
            {
                st1 = " and MMaster.DepartmentId>0  and MMaster.divisionid IS NULL and MMaster.EmployeeId IS NULL";

            }
            else
            {
                st1 = " and MMaster.DepartmentId='" + ddlsearchByStore.SelectedValue + "'  and MMaster.divisionid IS NULL and MMaster.EmployeeId IS NULL";

            }
        }
        else if (RadioButtonList2.SelectedIndex == 2)
        {


            lblDivision.Text = "Business:Department:Division  - " + ddlsearchByStore.SelectedItem.Text;
            if (ddlsearchByStore.SelectedValue == "0")
            {
                st1 = " and MMaster.divisionid>0 and MMaster.EmployeeId IS NULL";
            }
            else
            {
                st1 = " and MMaster.divisionid='" + ddlsearchByStore.SelectedValue + "' and MMaster.EmployeeId IS NULL";
            }

            //else
            //{
            //    st1 = " and objectivemaster.businessid='" + DropDownList2.SelectedValue + "' and objectiveMaster.EmployeeId='0'";

            //}
        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {

            lblEmp.Text = "Employee " + "   :  " + DropDownList3.SelectedItem.Text;
            if (DropDownList3.SelectedValue == "0")
            {

                if (Convert.ToInt32(ddlsearchByStore.SelectedValue) > 0)
                {
                    st1 = " and MMaster.DepartmentId='" + ddlsearchByStore.SelectedValue + "' and MMaster.EmployeeId>'0'";

                }
                else
                {
                    st1 = " and MMaster.EmployeeId>0";
                }
            }
            else
            {
                st1 = " and MMaster.EmployeeId='" + DropDownList3.SelectedValue + "'";

            }
        }
        // }
        lblyeartext.Text = "Yearly Goal : All";
        //if (ddlfilterobj.SelectedIndex > 0)
        //{
        //    st1 += " and objectiveMaster.masterId='" + ddlfilterobj.SelectedValue + "'";
        //}

        if (DropDownList1.SelectedIndex > 0)
        {
            st1 += " and year.id='" + DropDownList1.SelectedValue + "'";
        }

        if (DropDownList4.SelectedIndex > 0)
        {
            st1 += " and Month.Monthid='" + DropDownList4.SelectedValue + "'";
        }

        if (ddlyfilter.SelectedIndex > 0)
        {
            lblyeartext.Text = "Yearly Goal : " + ddlyfilter.SelectedItem.Text;

            st1 += " and MMaster.YMasterId='" + ddlyfilter.SelectedValue + "'";
        }

        if (DropDownList5.SelectedIndex > 0)
        {
            st1 += " and MMaster.parentmonthlygoalid='" + DropDownList5.SelectedValue + "'";
        }
        if (DropDownList8.SelectedIndex > 0)
        {
            st1 += " and MMaster.parentmonthlygoalid='" + DropDownList8.SelectedValue + "'";
        }
        if (DropDownList9.SelectedIndex > 0)
        {
            st1 += " and MMaster.parentmonthlygoalid='" + DropDownList9.SelectedValue + "'";
        }

        string filc = "";

        string str2 = "";

        if (RadioButtonList2.SelectedIndex == 0)
        {
            filc = "WareHouseMaster.Name as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname,";
            grid.Columns[0].HeaderText = "Business";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = false;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = false;

            strw = "  " + filc + " MMaster.MasterId,MMaster.BudgetedCost as bdcost,MMaster.ActualCost, StatusMaster.statusname,YMaster.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId,objectivemaster.DepartmentId as Dept_Id from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=YMaster.year inner join STGMaster on STGMaster.MasterId=YMaster.StgMasterId  inner join LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId left outer join BusinessMaster on objectivemaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectivemaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ObjectiveMaster.StoreId   left outer join    StatusMaster   on StatusMaster.StatusId=MMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

            str2 = " select Count(MMaster.MasterId) as ci from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=YMaster.year inner join STGMaster on STGMaster.MasterId=YMaster.StgMasterId  inner join LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId left outer join BusinessMaster on objectivemaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectivemaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ObjectiveMaster.StoreId   left outer join    StatusMaster   on StatusMaster.StatusId=MMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

            //order by Warehousemaster.Name,Yearname,title asc";
        }
        else if (RadioButtonList2.SelectedIndex == 1)
        {
            //   filc = "  LEFT(WareHouseMaster.Name,4) as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname,";
            grid.Columns[0].HeaderText = "Busi";
            grid.Columns[1].HeaderText = "Department";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = true;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = false;

            if (dropdowndepartment.SelectedValue == "1")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,Year.Name  + ':' +  YMaster.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on Year.Id=YMaster.Year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(MMaster.MasterId) as ci from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on Year.Id=YMaster.Year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
                //order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,Yearname,title asc";
            }
            if (dropdowndepartment.SelectedValue == "2")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,Month.Name + ':' + M.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster  on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(MMaster.MasterId) as ci from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster  on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
                //order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,Yearname,title asc";
            }
            if (dropdowndepartment.SelectedValue == "0")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,YMaster.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(MMaster.MasterId) as ci from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
                //order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,Yearname,title asc";
            }
        }
        else if (RadioButtonList2.SelectedIndex == 2)
        {
            //  filc = "  LEFT(WareHouseMaster.Name,4) as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,LEFT(DepartmentmasterMNC.Departmentname,4)as Departmentname,";
            grid.Columns[0].HeaderText = "Busi";
            grid.Columns[1].HeaderText = "Dept";
            grid.Columns[2].HeaderText = "Division";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = false;
            grid.Columns[2].Visible = true;
            grid.Columns[3].Visible = false;

            if (dropdowndivision.SelectedValue == "0")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,YMaster.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(MMaster.MasterId) as ci from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
                // order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName,Yearname,title asc";
            }
            if (dropdowndivision.SelectedValue == "1")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,Year.Name  + ':' +  YMaster.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(MMaster.MasterId) as ci from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
                //order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName,Yearname,title asc";
            }
            if (dropdowndivision.SelectedValue == "2")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,Month.Name + ':' + M.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster  on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and mmaster.TypeofMonthlyGoal='Busi'";

                str2 = " select Count(MMaster.MasterId) as ci from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster  on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and mmaster.TypeofMonthlyGoal='Busi'";
                //order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,Yearname,title asc";

            }
            if (dropdowndivision.SelectedValue == "3")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,Month.Name + ':' + M.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster  on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and mmaster.TypeofMonthlyGoal='Dept'";

                str2 = " select Count(MMaster.MasterId) as ci from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster  on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and mmaster.TypeofMonthlyGoal='Dept'";
                //order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,Yearname,title asc";
            }
        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            //    filc = "  LEFT(WareHouseMaster.Name,4) as Wname, LEFT(BusinessMaster.BusinessName,4) as businessname,EmployeeMaster.EmployeeName,LEFT(DepartmentmasterMNC.Departmentname,4)as Departmentname,";
            grid.Columns[0].HeaderText = "Busi";
            grid.Columns[1].HeaderText = "Dept";
            grid.Columns[2].HeaderText = "Divi";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = false;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = true;

            if (dropdownemployee.SelectedValue == "0")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,YMaster.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(MMaster.MasterId) as ci from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
                //order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName,EmployeeMaster.EmployeeName,Yearname,title asc";
            }
            if (dropdownemployee.SelectedValue == "1")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,Year.Name  + ':' +  YMaster.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";

                str2 = " select Count(MMaster.MasterId) as ci from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year left outer join BusinessMaster  on ymaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + "";
                //order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName,EmployeeMaster.EmployeeName,Yearname,title asc";
            }
            if (dropdownemployee.SelectedValue == "2")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,Month.Name + ':' + M.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and mmaster.TypeofMonthlyGoal='Busi'";

                str2 = " select Count(MMaster.MasterId) as ci from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and mmaster.TypeofMonthlyGoal='Busi'";
                //order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,Yearname,title asc";
            }
            if (dropdownemployee.SelectedValue == "3")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,Month.Name + ':' + M.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and mmaster.TypeofMonthlyGoal='Dept'";

                str2 = " select Count(MMaster.MasterId) as ci from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and mmaster.TypeofMonthlyGoal='Dept'";
                //order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,Yearname,title asc";
            }
            if (dropdownemployee.SelectedValue == "4")
            {
                strw = "  WareHouseMaster.Name as Wname,MMaster.BudgetedCost as bdcost,MMaster.ActualCost,EmployeeMaster.EmployeeName,BusinessMaster.BusinessName,DepartmentmasterMNC.Departmentname,MMaster.MasterId, StatusMaster.statusname,Month.Name + ':' + M.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster  on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and mmaster.TypeofMonthlyGoal='Divi'";

                str2 = " select Count(MMaster.MasterId) as ci from MMaster as M inner join MMaster on M.MasterId=MMaster.parentmonthlygoalid inner join  Month on  MMaster.Month=Month.Id left outer join ymaster on ymaster.masterid=mmaster.ymasterid inner join year on Year.Id=month.Yid left outer join BusinessMaster  on MMaster.divisionId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on  DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid  left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " and mmaster.TypeofMonthlyGoal='Divi'";
                //order by Warehousemaster.Name,DepartmentmasterMNC.Departmentname,Yearname,title asc";
            }
        }

        //  DataTable dt2 = ClsMMaster.SpMMasterGridfill(st1, filc);

        decimal d111 = 0;
        decimal d222 = 0;
        decimal d33 = 0;

        grid.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,YMaster.Title,MMaster.title asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt2 = GetDataPage(grid.PageIndex, grid.PageSize, sortExpression, strw);

            DataView myDataView = new DataView();
            myDataView = dt2.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            grid.DataSource = myDataView;

            for (int rowindex = 0; rowindex < dt2.Rows.Count; rowindex++)
            {
                dt2.Rows[rowindex]["Departmentname"] = dt2.Rows[rowindex]["Departmentname"].ToString();

                DataTable dtcrNew11 = ClsMMaster.SelectOfficeManagerDocumentsforMID(Convert.ToString(dt2.Rows[rowindex]["MasterId"]));

                dt2.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];


                if (RadioButtonList2.SelectedValue == "2")
                {
                    SqlDataAdapter dar = new SqlDataAdapter("select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId inner join MMaster on MMaster.MasterId=WMaster.MMasterId  where MMaster.MasterId='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr = new DataTable();
                    dar.Fill(dtr);

                    dt2.Rows[rowindex]["ActualCost"] = dtr.Rows[0]["cost"];

                    if (dtr.Rows[0]["cost"].ToString() != "")
                    {
                        d111 += Convert.ToDecimal(dtr.Rows[0]["cost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "4")
                {
                    SqlDataAdapter dar = new SqlDataAdapter("select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId inner join MMaster on MMaster.MasterId=WMaster.MMasterId  where MMaster.TypeofMonthlyGoal='Busi' and MMaster.parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr = new DataTable();
                    dar.Fill(dtr);

                    dt2.Rows[rowindex]["ActualCost"] = dtr.Rows[0]["cost"];

                    if (dtr.Rows[0]["cost"].ToString() != "")
                    {
                        d111 += Convert.ToDecimal(dtr.Rows[0]["cost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "0")
                {
                    SqlDataAdapter dar = new SqlDataAdapter("select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId inner join MMaster on MMaster.MasterId=WMaster.MMasterId  where MMaster.TypeofMonthlyGoal='Dept' and MMaster.parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr = new DataTable();
                    dar.Fill(dtr);

                    dt2.Rows[rowindex]["ActualCost"] = dtr.Rows[0]["cost"];

                    if (dtr.Rows[0]["cost"].ToString() != "")
                    {
                        d111 += Convert.ToDecimal(dtr.Rows[0]["cost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "1")
                {
                    SqlDataAdapter dar = new SqlDataAdapter("select sum(cast(TaskAllocationMaster.ActualRate as float)) as cost from TaskAllocationMaster inner join TaskMaster on TaskAllocationMaster.taskid=TaskMaster.taskid inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId inner join MMaster on MMaster.MasterId=WMaster.MMasterId  where MMaster.TypeofMonthlyGoal='Divi' and MMaster.parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr = new DataTable();
                    dar.Fill(dtr);

                    dt2.Rows[rowindex]["ActualCost"] = dtr.Rows[0]["cost"];

                    if (dtr.Rows[0]["cost"].ToString() != "")
                    {
                        d111 += Convert.ToDecimal(dtr.Rows[0]["cost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "2")
                {
                    SqlDataAdapter dar1 = new SqlDataAdapter("select sum(cast(TaskMaster.Rate as float)) as rcost from TaskMaster inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId inner join MMaster on MMaster.MasterId=WMaster.MMasterId  where MMaster.MasterId='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr1 = new DataTable();
                    dar1.Fill(dtr1);

                    dt2.Rows[rowindex]["bdcost"] = dtr1.Rows[0]["rcost"];

                    if (dtr1.Rows[0]["rcost"].ToString() != "")
                    {
                        d222 += Convert.ToDecimal(dtr1.Rows[0]["rcost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "4")
                {
                    SqlDataAdapter dar1 = new SqlDataAdapter("select sum(cast(TaskMaster.Rate as float)) as rcost from TaskMaster inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId inner join MMaster on MMaster.MasterId=WMaster.MMasterId  where MMaster.TypeofMonthlyGoal='Busi' and MMaster.parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr1 = new DataTable();
                    dar1.Fill(dtr1);

                    dt2.Rows[rowindex]["bdcost"] = dtr1.Rows[0]["rcost"];

                    if (dtr1.Rows[0]["rcost"].ToString() != "")
                    {
                        d222 += Convert.ToDecimal(dtr1.Rows[0]["rcost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "0")
                {
                    SqlDataAdapter dar1 = new SqlDataAdapter("select sum(cast(TaskMaster.Rate as float)) as rcost from TaskMaster inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId inner join MMaster on MMaster.MasterId=WMaster.MMasterId  where MMaster.TypeofMonthlyGoal='Dept' and MMaster.parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr1 = new DataTable();
                    dar1.Fill(dtr1);

                    dt2.Rows[rowindex]["bdcost"] = dtr1.Rows[0]["rcost"];

                    if (dtr1.Rows[0]["rcost"].ToString() != "")
                    {
                        d222 += Convert.ToDecimal(dtr1.Rows[0]["rcost"]);
                    }
                }

                if (RadioButtonList2.SelectedValue == "1")
                {
                    SqlDataAdapter dar1 = new SqlDataAdapter("select sum(cast(TaskMaster.Rate as float)) as rcost from TaskMaster inner join WMaster on WMaster.MasterId=TaskMaster.WeeklygoalId inner join MMaster on MMaster.MasterId=WMaster.MMasterId  where MMaster.TypeofMonthlyGoal='Divi' and MMaster.parentmonthlygoalid='" + Convert.ToString(dt2.Rows[rowindex]["MasterId"]) + "'", con);
                    DataTable dtr1 = new DataTable();
                    dar1.Fill(dtr1);

                    dt2.Rows[rowindex]["bdcost"] = dtr1.Rows[0]["rcost"];

                    if (dtr1.Rows[0]["rcost"].ToString() != "")
                    {
                        d222 += Convert.ToDecimal(dtr1.Rows[0]["rcost"]);
                    }
                }

                string d3 = dt2.Rows[rowindex]["BudgetedCost"].ToString();
                d33 += Convert.ToDecimal(d3);

            }
            grid.DataBind();
        }
        else
        {
            grid.DataSource = null;
            grid.DataBind();
        }

        GridViewRow dr = (GridViewRow)grid.FooterRow;

        if (grid.Rows.Count > 0)
        {

            Label lblfooter = (Label)dr.FindControl("lblfooter");
            Label lblfooter1 = (Label)dr.FindControl("lblfooter1");
            Label lblfooter2 = (Label)dr.FindControl("lblfooter2");
            lblfooter.Text = d33.ToString("###,###.##");
            lblfooter2.Text = d111.ToString("###,###.##");
            lblfooter1.Text = d222.ToString("###,###.##");

        }
        //   totalbudgetedcost();

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

    //on click on edit button i.e. select index chage of grid
    protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //store selected row id into id
        //string id = grid.DataKeys[e.NewSelectedIndex].Value.ToString();

        ////create object of structure


        //StMMaster st;
        ////give data source to object of structure
        //st = ClsMMaster.SpMMasterGetDataStructById(id);

        ////load data of productmaster table into object
        ////ddlbrand.SelectedItem = st.bname;
        //hdnid.Value = st.masterid.ToString();

        //ddlbusiness.Items.Clear();
        //ddlemployee.Items.Clear();
        //pnlemployee.Visible = false;
        //pnldivision.Visible = false;

        //if (Convert.ToInt32(st.departmentId) > 0)
        //{
        //    lblwname0.Text = "Business-Department Name : ";
        //    RadioButtonList1.SelectedIndex = 1;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        fillDepartment();
        //    }

        //    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue((st.departmentId).ToString()));

        //}
        //else
        //{
        //    RadioButtonList1.SelectedIndex = 0;
        //    lblwname0.Text = "Business Name : ";
        //    if (Convert.ToString(ViewState["cd"]) != "1")
        //    {
        //        fillstore();
        //    }
        //    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(st.Whid.ToString()));

        //}


        //if ((st.employeeid.ToString() != "0"))
        //{
        //    fillemployee();
        //    pnlemployee.Visible = true;
        //    RadioButtonList1.SelectedIndex = 3;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        fillDepartment();
        //    }
        //    fillemployee();

        //    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(st.employeeid.ToString()));
        //    // ddlEmp_SelectedIndexChanged(sender, e);
        //}
        //else if (Convert.ToString(st.businessid) != "0")
        //{
        //    pnldivision.Visible = true;
        //    // PnlDp.Visible = false;
        //    RadioButtonList1.SelectedIndex = 2;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        fillDepartment();
        //    }

        //    fillDivision();
        //    ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(st.businessid.ToString()));
        //    ddlbusiness_SelectedIndexChanged(sender, e);
        //}
        //if (Convert.ToString(st.description) != "")
        //{
        //    Button2.Checked = true;
        //    Pnl1.Visible = true;
        //    txtdescription.Text = st.description.ToString();
        //}
        //else
        //{
        //    Button2.Checked = false;
        //    Pnl1.Visible = false;
        //    txtdescription.Text = "";
        //}


        //filltgmain();
        //ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(st.ymasterid.ToString()));

        //txttitle.Text = st.title.ToString();

        //ddly_SelectedIndexChanged(sender, e);

        //ddlmonth.SelectedValue = st.month.ToString();
        //txtbudgetedamount.Text = st.budgetedcost.ToString();

        //btncancel.Visible = true;
        //btnupdate.Visible = true;

        //btnsubmit.Visible = false;
        //btnreset.Visible = false;
        //Pnladdnew.Visible = true;
        //btnadd.Visible = false;
    }

    // DELETE DATA FROM BRANDMASTER BY BID[PRIMARY KEY]
    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Get the ID of the record to be deleted
        string id = grid.DataKeys[e.RowIndex].Value.ToString();

        // Execute the delete command
        bool success = ClsMMaster.SpMMasterDeleteData(id);

        if (Convert.ToString(success) == "True")
        {
            statuslable.Text = "Record deleted successfully";
        }
        else
        {
            statuslable.Text = "Record not deleted successfully";
        }
        // Cancel edit mode
        grid.EditIndex = -1;

        // Reload the grid
        BindGrid();
    }

    //GRID PAGINATION FUNCTION
    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        BindGrid();
    }
    // This code in main page class... for freetextbox//

    public new void RegisterOnSubmitStatement(string key, string script)
    {
        ScriptManager.RegisterOnSubmitStatement(this, typeof(Page), key, script);
    }
    [Obsolete]
    public override void RegisterStartupScript(string key, string script)
    {
        string newScript = script.Replace("FTB_AddEvent(window,'load',function () {", "").Replace("});", "");
        ScriptManager.RegisterStartupScript(this, typeof(Page), key, newScript, false);
    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
   
  
    protected void ddlfilterobj_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "Departmentaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        fillDepartment();
        filltgmain();
    }


    protected void imgadddivision_Click(object sender, ImageClickEventArgs e)
    {
        string te = "FrmBusinessMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgempadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "EmployeeMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgobjadd_Click(object sender, ImageClickEventArgs e)
    {

        string te = "frmYMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgdivisionrefresh_Click(object sender, ImageClickEventArgs e)
    {
        filldevesion();
        filldivisionyear();
        //fillDivision();
        //filltgmain();
    }
    protected void imgemprefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillemployee();
        filltgmain();
    }
    protected void imgobjreshresh_Click(object sender, ImageClickEventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            fillbusinessyear();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            filldepartmentyear();
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            filldivisionyear();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            fillemployeeyear();
        }

        //filltgmain();
    }
    protected void Button2_CheckedChanged(object sender, EventArgs e)
    {
        if (!Pnl1.Visible)
            Pnl1.Visible = true;
        else
            Pnl1.Visible = false;
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;

            grid.AllowPaging = false;
            grid.PageSize = 1000;
            BindGrid();

            if (grid.Columns[12].Visible == true)
            {
                ViewState["docs"] = "tt";
                grid.Columns[12].Visible = false;
            }
            if (grid.Columns[13].Visible == true)
            {
                ViewState["edith"] = "tt";
                grid.Columns[13].Visible = false;
            }
            if (grid.Columns[14].Visible == true)
            {
                ViewState["deleteh"] = "tt";
                grid.Columns[14].Visible = false;
            }
            if (grid.Columns[11].Visible == true)
            {
                ViewState["viewm"] = "tt";
                grid.Columns[11].Visible = false;
            }
        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;

            grid.AllowPaging = true;
            grid.PageSize = 10;
            BindGrid();

            if (ViewState["docs"] != null)
            {
                grid.Columns[12].Visible = true;
            }
            if (ViewState["edith"] != null)
            {
                grid.Columns[13].Visible = true;
            }
            if (ViewState["deleteh"] != null)
            {
                grid.Columns[14].Visible = true;
            }
            if (ViewState["viewm"] != null)
            {
                grid.Columns[11].Visible = true;
            }
        }
    }



    protected void ddlyfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Send")
        {
            //grid.SelectedIndex = 
            ////ViewState["MissionId"] = grid.SelectedDataKey.Value;

            //int index = grid.SelectedIndex;

            //Label MId = (Label)grid.Rows[index].FindControl("lblMasterId");
            ViewState["MissionId"] = Convert.ToInt32(e.CommandArgument);

            // DataTable dtcrNew11 = ClsSTGMaster.SelectOfficedocwithstgid(Convert.ToString(ViewState["MissionId"]));
            DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["MissionId"]), 5);

            GridView1.DataSource = dtcrNew11;
            GridView1.DataBind();



            ModalPopupExtenderAddnew.Show();



        }
        else if (e.CommandName == "view")
        {

            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "frmMMasterReport.aspx?Mid=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }

        if (e.CommandName == "Edit")
        {
            //string id = grid.DataKeys[e.NewSelectedIndex].Value.ToString();

            //create object of structure

            lbllegend.Text = "Edit Monthly Goal";
            statuslable.Text = "";

            Panel19.Visible = false;


            string id = Convert.ToString(e.CommandArgument);

            ViewState["updateid"] = id;

            if (RadioButtonList2.SelectedValue == "4")
            {
                pnlemployee.Visible = false;
                pnldivision.Visible = false;

                string dfd = "Select * from MMaster where MMaster.MasterId=" + id;
                SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                DataTable dtfg = new DataTable();
                dafg.Fill(dtfg);

                RadioButtonList1.SelectedValue = "0";
                RadioButtonList1_SelectedIndexChanged(sender, e);

                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["Businessid"].ToString()));

                fillbusinessyear();

                ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["YMasterid"].ToString()));

                SqlDataAdapter dls = new SqlDataAdapter("select year.name as Names from year inner join month on month.yid=year.id where month.id='" + dtfg.Rows[0]["Month"].ToString() + "'", con);
                DataTable dts = new DataTable();
                dls.Fill(dts);

                ddlyear.Items.Clear();
                ddlyear.DataSource = obj.Tablemaster("Select * from Year where Name>='" + System.DateTime.Now.Year.ToString() + "'");
                ddlyear.DataMember = "Name";
                ddlyear.DataTextField = "Name";
                ddlyear.DataValueField = "Id";
                ddlyear.DataBind();
                ddlyear.Items.Insert(0, "-Select-");
                ddlyear.Items[0].Value = "0";

                ddlyear.SelectedIndex = ddlyear.Items.IndexOf(ddlyear.Items.FindByText(dts.Rows[0]["Names"].ToString()));

                filmonthly();

                ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(dtfg.Rows[0]["Month"].ToString()));

                txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                {
                    Button2.Checked = true;
                    Pnl1.Visible = true;
                    txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                }

            }

            if (RadioButtonList2.SelectedValue == "0")
            {
                pnlradio.Visible = true;

                SqlDataAdapter da0 = new SqlDataAdapter("select * from MMaster where Masterid=" + id, con);
                DataTable dt0 = new DataTable();
                da0.Fill(dt0);

                string dfd = "";

                if (dt0.Rows[0]["YMasterId"].ToString() == "")
                {
                    RadioButtonList3.SelectedValue = "1";

                    dfd = "Select Distinct MMaster.Month as ip,MMaster.description,MMaster.parentmonthlygoalid,MMaster.DepartmentId,MMaster.MasterId, StatusMaster.statusname,Month.Name as Month, MMaster.title,MMaster.YMasterId,MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id left outer join BusinessMaster  on MMaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and MMaster.MasterId= " + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "1";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["DepartmentId"].ToString()));

                    pnlmonthly1.Visible = true;
                    Panel5.Visible = false;

                    ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(dtfg.Rows[0]["ip"].ToString()));

                    filbusinessmonths();
                    ddlbusimonthly.SelectedIndex = ddlbusimonthly.Items.IndexOf(ddlbusimonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));



                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }
                }
                else
                {
                    RadioButtonList3.SelectedValue = "0";

                    pnlmonthly1.Visible = false;
                    Panel5.Visible = true;

                    dfd = " Select Distinct WareHouseMaster.Name as Wname,MMaster.Month as ip,DepartmentmasterMNC.Departmentname,MMaster.description,YMaster.DepartmentId, MMaster.MasterId, StatusMaster.statusname,YMaster.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on ymaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and MMaster.MasterId= " + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "1";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["DepartmentId"].ToString()));

                    filldepartmentyear();
                    ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["YMasterid"].ToString()));

                    ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(dtfg.Rows[0]["ip"].ToString()));

                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }
                }
            }

            if (RadioButtonList2.SelectedValue == "1")
            {
                pnlradio1.Visible = true;

                SqlDataAdapter da0 = new SqlDataAdapter("select * from MMaster where Masterid=" + id, con);
                DataTable dt0 = new DataTable();
                da0.Fill(dt0);

                string dfd = "";


                if (dt0.Rows[0]["YMasterId"].ToString() == "")
                {

                    dfd = "Select Distinct MMaster.Month as ip,MMaster.description,MMaster.TypeofMonthlyGoal,MMaster.parentmonthlygoalid,MMaster.DepartmentId,MMaster.divisionid,MMaster.MasterId, StatusMaster.statusname,Month.Name as Month, MMaster.title,MMaster.YMasterId,MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id left outer join BusinessMaster  on MMaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and MMaster.MasterId= " + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "2";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    RadioButtonList4.SelectedValue = "1";

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["divisionid"].ToString()));

                    Panel15.Visible = true;
                    Panel5.Visible = false;

                    ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(dtfg.Rows[0]["ip"].ToString()));

                    if (dtfg.Rows[0]["TypeofMonthlyGoal"].ToString() == "Busi")
                    {
                        DropDownList6.SelectedValue = "0";
                        pnlmonthly1.Visible = true;
                        pnlmonthly2.Visible = false;

                        filbusinessmonths11();
                        ddlbusimonthly.SelectedIndex = ddlbusimonthly.Items.IndexOf(ddlbusimonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));
                    }

                    if (dtfg.Rows[0]["TypeofMonthlyGoal"].ToString() == "Dept")
                    {
                        DropDownList6.SelectedValue = "1";
                        pnlmonthly1.Visible = false;
                        pnlmonthly2.Visible = true;

                        fildepartmentmonths();
                        ddldeptmonthly.SelectedIndex = ddldeptmonthly.Items.IndexOf(ddldeptmonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));
                    }

                    //ddlbusimonthly.SelectedIndex = ddlbusimonthly.Items.IndexOf(ddlbusimonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));


                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }
                }
                else
                {
                    RadioButtonList4.SelectedValue = "0";

                    Panel15.Visible = false;
                    Panel5.Visible = true;

                    dfd = "Select Distinct WareHouseMaster.Name as Wname,MMaster.Month as ip,DepartmentmasterMNC.Departmentname,MMaster.description,YMaster.divisionid, MMaster.MasterId, StatusMaster.statusname,YMaster.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on ymaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and MMaster.MasterId= " + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "2";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["divisionid"].ToString()));

                    filldivisionyear();
                    ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["YMasterid"].ToString()));

                    ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(dtfg.Rows[0]["ip"].ToString()));

                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }
                }
            }

            if (RadioButtonList2.SelectedValue == "2")
            {
                pnlradio2.Visible = true;

                SqlDataAdapter da0 = new SqlDataAdapter("select * from MMaster where Masterid=" + id, con);
                DataTable dt0 = new DataTable();
                da0.Fill(dt0);

                string dfd = "";

                if (dt0.Rows[0]["YMasterId"].ToString() == "")
                {

                    dfd = "Select Distinct MMaster.Month as ip,MMaster.description,MMaster.employeeid,MMaster.TypeofMonthlyGoal,MMaster.parentmonthlygoalid,MMaster.DepartmentId,MMaster.MasterId, StatusMaster.statusname,Month.Name as Month, MMaster.title,MMaster.YMasterId,MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id left outer join BusinessMaster  on MMaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=MMaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=MMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MMaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and MMaster.MasterId= " + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "3";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    RadioButtonList5.SelectedValue = "1";

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["DepartmentId"].ToString()));

                    fillemployee();
                    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dtfg.Rows[0]["employeeid"].ToString()));

                    Panel16.Visible = true;
                    Panel5.Visible = false;

                    ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(dtfg.Rows[0]["ip"].ToString()));

                    if (dtfg.Rows[0]["TypeofMonthlyGoal"].ToString() == "Busi")
                    {
                        DropDownList7.SelectedValue = "0";
                        pnlmonthly1.Visible = true;
                        pnlmonthly2.Visible = false;
                        pnlmonthly3.Visible = false;

                        filbusinessmonths();
                        ddlbusimonthly.SelectedIndex = ddlbusimonthly.Items.IndexOf(ddlbusimonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));
                    }

                    if (dtfg.Rows[0]["TypeofMonthlyGoal"].ToString() == "Dept")
                    {
                        DropDownList7.SelectedValue = "1";
                        pnlmonthly1.Visible = false;
                        pnlmonthly2.Visible = true;
                        pnlmonthly3.Visible = false;

                        fildepartmentmonths111();
                        ddldeptmonthly.SelectedIndex = ddldeptmonthly.Items.IndexOf(ddldeptmonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));
                    }

                    if (dtfg.Rows[0]["TypeofMonthlyGoal"].ToString() == "Divi")
                    {
                        DropDownList7.SelectedValue = "2";
                        pnlmonthly1.Visible = false;
                        pnlmonthly2.Visible = false;
                        pnlmonthly3.Visible = true;

                        filemployeemonths();
                        ddldivimonthly.SelectedIndex = ddldivimonthly.Items.IndexOf(ddldivimonthly.Items.FindByValue(dtfg.Rows[0]["parentmonthlygoalid"].ToString()));
                    }

                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }
                }
                else
                {
                    RadioButtonList5.SelectedValue = "0";

                    Panel16.Visible = false;
                    Panel5.Visible = true;

                    dfd = "Select Distinct WareHouseMaster.Name as Wname,MMaster.Month as ip,DepartmentmasterMNC.Departmentname,MMaster.description,YMaster.DepartmentId, MMaster.MasterId, StatusMaster.statusname,YMaster.Title as Yearname,Month.Name as Month, MMaster.title,MMaster.YMasterId,MMaster.employeeid, MMaster.budgetedcost,EmployeeMaster.EmployeeMasterID as DocumentId from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on ymaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and MMaster.MasterId= " + id;
                    SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                    DataTable dtfg = new DataTable();
                    dafg.Fill(dtfg);

                    RadioButtonList1.SelectedValue = "3";
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["DepartmentId"].ToString()));

                    fillemployee();
                    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dtfg.Rows[0]["employeeid"].ToString()));

                    fillemployeeyear();
                    ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["YMasterid"].ToString()));

                    ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(dtfg.Rows[0]["ip"].ToString()));

                    txttitle.Text = dtfg.Rows[0]["Title"].ToString();
                    txtbudgetedamount.Text = dtfg.Rows[0]["BudgetedCost"].ToString();

                    if (Convert.ToString(dtfg.Rows[0]["description"]) != "")
                    {
                        Button2.Checked = true;
                        Pnl1.Visible = true;
                        txtdescription.Text = dtfg.Rows[0]["description"].ToString();
                    }
                }
            }

            btncancel.Visible = true;
            btnupdate.Visible = true;

            btnsubmit.Visible = false;
            btnreset.Visible = false;
            Pnladdnew.Visible = true;
            btnadd.Visible = false;
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtenderAddnew.Hide();
    }
    protected void ddly_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlmonth.Items.Clear();
        //if (ddly.SelectedIndex != -1)
        //{
        //    string[] separator1 = new string[] { ":" };
        //    string[] strSplitArr1 = ddly.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
        //    string Year = strSplitArr1[0].ToString();

        //    SqlDataAdapter dafa1 = new SqlDataAdapter("Select Year.Name from Ymaster INNER JOIN Year ON Ymaster.Year=Year.ID where Ymaster.MasterId='" + ddly.SelectedValue + "'", con);
        //    DataTable dtfa1 = new DataTable();
        //    dafa1.Fill(dtfa1);

        //    string debut = "";

        //    if (dtfa1.Rows.Count > 0)
        //    {

        //        debut = Convert.ToString(dtfa1.Rows[0]["Name"]);
        //    }


        //    if (debut == currentyear)
        //    {
        //        ddlmonth.DataSource = ClsMMaster.SelctMonthonYear(Year, currentmonth);
        //        ddlmonth.DataMember = "yeayrmonth";
        //        ddlmonth.DataTextField = "yeayrmonth";
        //        ddlmonth.DataValueField = "Id";
        //        ddlmonth.DataBind();               
        //    }
        //    if (debut != currentyear)
        //    {
        //        SqlDataAdapter dafa = new SqlDataAdapter("Select Month.Id, Year.Name+ ' -> ' + Month.Name AS yeayrmonth from dbo.Month INNER JOIN dbo.Year ON dbo.Month.Yid = dbo.Year.Id where Year.Name='" + debut + "' Order by Year.Name, Month.Id", con);
        //        DataTable dtfa = new DataTable();
        //        dafa.Fill(dtfa);

        //        ddlmonth.DataSource = dtfa;
        //        ddlmonth.DataMember = "yeayrmonth";
        //        ddlmonth.DataTextField = "yeayrmonth";
        //        ddlmonth.DataValueField = "Id";
        //        ddlmonth.DataBind();
        //    }
        //    ddlmonth.Items.Insert(0, "-Select-");
        //    ddlmonth.Items[0].Value = "0";

        //    int yr = System.DateTime.Today.Date.Year;
        //    string yer = Convert.ToString(yr);
        //    mnt = yer + " -> " + Convert.ToString(System.DateTime.Now.Month);
        //    ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByText(mnt));
        //}

    }


    protected void btnadd_Click(object sender, EventArgs e)
    {
        Pnladdnew.Visible = true;
        lbllegend.Text = "Add Monthly Goal";
        if (Pnladdnew.Visible == true)
        {
            btnadd.Visible = false;
        }
        statuslable.Text = "";
        RadioButtonList1.SelectedValue = "0";
        RadioButtonList1_SelectedIndexChanged(sender, e);
    }
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void totalbudgetedcost()
    {
        string temp = "";
        string totalsum = "";
        if (RadioButtonList2.SelectedValue == "4")
        {
            if (ddlsearchByStore.SelectedIndex > -1)
            {
                totalsum = "Select sum(MMaster.budgetedcost) as TotalBudgtedCost from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join STGMaster on STGMaster.MasterId=YMaster.StgMasterId  inner join          LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join            objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId            left outer join   BusinessMaster  on              objectivemaster.BusinessId=BusinessMaster.BusinessID left outer join               [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectivemaster.EmployeeId               left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId               inner join WareHouseMaster on WareHouseMaster.WareHouseId=ObjectiveMaster.StoreId   left outer join    StatusMaster   on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and ObjectiveMaster.StoreId='" + ddlsearchByStore.SelectedValue + "' and ObjectiveMaster.DepartmentId='0' and objectivemaster.businessid='0' and objectiveMaster.EmployeeId='0'";

            }
            else
            {
                totalsum = "Select sum(MMaster.budgetedcost) as TotalBudgtedCost from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join STGMaster on STGMaster.MasterId=YMaster.StgMasterId  inner join          LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join            objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId            left outer join   BusinessMaster  on              objectivemaster.BusinessId=BusinessMaster.BusinessID left outer join               [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectivemaster.EmployeeId               left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId               inner join WareHouseMaster on WareHouseMaster.WareHouseId=ObjectiveMaster.StoreId   left outer join    StatusMaster   on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and ObjectiveMaster.DepartmentId='0' and objectivemaster.businessid='0' and objectiveMaster.EmployeeId='0'";
            }
            if (ddlyfilter.SelectedIndex > 0)
            {
                totalsum += " and MMaster.YMasterId='" + ddlyfilter.SelectedValue + "'";
            }
        }

        if (RadioButtonList2.SelectedValue == "0")
        {
            if (ddlsearchByStore.SelectedIndex > 0)
            {
                totalsum = "Select sum(MMaster.budgetedcost) as TotalBudgtedCost  from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on ymaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and ymaster.DepartmentId='" + ddlsearchByStore.SelectedValue + "' and ymaster.divisionid IS NULL and ymaster.EmployeeId IS NULL";

            }
            else
            {
                totalsum = "Select sum(MMaster.budgetedcost) as TotalBudgtedCost  from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on ymaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and ymaster.DepartmentId>0 and ymaster.divisionid IS NULL and ymaster.EmployeeId IS NULL";
            }
            if (ddlyfilter.SelectedIndex > 0)
            {
                totalsum += " and MMaster.YMasterId='" + ddlyfilter.SelectedValue + "'";
            }
        }

        if (RadioButtonList2.SelectedValue == "1")
        {
            if (ddlsearchByStore.SelectedIndex > 0)
            {
                totalsum = "Select sum(MMaster.budgetedcost) as TotalBudgtedCost  from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on ymaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and ymaster.divisionid='" + ddlsearchByStore.SelectedValue + "' and ymaster.EmployeeId IS NULL";

            }
            else
            {
                totalsum = "Select sum(MMaster.budgetedcost) as TotalBudgtedCost  from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on ymaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and ymaster.divisionid>0 and ymaster.EmployeeId IS NULL";
            }
            if (ddlyfilter.SelectedIndex > 0)
            {
                totalsum += " and MMaster.YMasterId='" + ddlyfilter.SelectedValue + "'";
            }
        }

        if (RadioButtonList2.SelectedValue == "2")
        {
            if (ddlsearchByStore.SelectedIndex > 0)
            {
                totalsum = "Select sum(MMaster.budgetedcost) as TotalBudgtedCost  from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on ymaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and ymaster.departmentid='" + ddlsearchByStore.SelectedValue + "'";

            }
            else
            {
                totalsum = "Select sum(MMaster.budgetedcost) as TotalBudgtedCost  from Month inner join  MMaster on  MMaster.Month=Month.Id inner join YMaster on  YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on ymaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on  [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid left outer join StatusMaster on StatusMaster.StatusId=MMaster.StatusId  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and ymaster.employeeid>0";
            }
            if (DropDownList3.SelectedIndex > 0)
            {
                totalsum += "and YMaster.EmployeeID='" + DropDownList3.SelectedValue + "'";
            }
            if (ddlyfilter.SelectedIndex > 0)
            {
                totalsum += " and MMaster.YMasterId='" + ddlyfilter.SelectedValue + "'";
            }
        }

        SqlDataAdapter damil = new SqlDataAdapter(totalsum, con);
        DataTable dtmil = new DataTable();
        damil.Fill(dtmil);

        decimal t1 = 0;
        if (dtmil.Rows.Count > 0)
        {
            if (Convert.ToString(dtmil.Rows[0]["TotalBudgtedCost"]) != "")
            {
                //  t1 = Convert.ToDecimal(Convert.ToString(dtmil.Rows[0]["TotalBudgtedCost"]));
                t1 = Math.Round(Convert.ToDecimal(dtmil.Rows[0]["TotalBudgtedCost"]), 2);
                temp = t1.ToString("###,###.##");
            }
        }

        if (grid.Rows.Count > 0)
        {
            GridViewRow dr = (GridViewRow)grid.FooterRow;
            Label lblfooter = (Label)dr.FindControl("lblfooter");
            lblfooter.Text = temp;
        }

    }

    protected void fillbusinessyear()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select Year.Name  + ':' +  YMaster.title as BusiYear,YMaster.MasterId from objectivemaster Left outer join BusinessMaster on objectivemaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectiveMaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WarehouseId=objectivemaster.StoreId inner join LTGMaster on LTGMaster.ObjectiveMasterId=ObjectiveMaster.MasterId inner join STGMaster on STGMaster.LTGMasterID=LTGMaster.MasterId inner join YMaster on YMaster.StgMasterId=STGMaster.MasterId inner join Year on Year.Id=YMaster.Year left outer join StatusMaster on StatusMaster.StatusId=YMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and Year.Name>='" + currentyear + "' and YMaster.BusinessID='" + ddlStore.SelectedValue + "'";

            if (ddlyear.SelectedIndex > 0)
            {
                y11 += " and YMaster.year='" + ddlyear.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "BusiYear";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfillbusinessyear()
    {
        ddlyfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > -1)
        {

            y11 = "select Year.Name  + ':' +  YMaster.title as BusiYear,YMaster.MasterId from objectivemaster Left outer join BusinessMaster on objectivemaster.businessid=BusinessMaster.BusinessID Left outer join  [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectiveMaster.EmployeeId Left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WarehouseId=objectivemaster.StoreId inner join LTGMaster on LTGMaster.ObjectiveMasterId=ObjectiveMaster.MasterId inner join STGMaster on STGMaster.LTGMasterID=LTGMaster.MasterId inner join YMaster on YMaster.StgMasterId=STGMaster.MasterId inner join Year on Year.Id=YMaster.Year left outer join StatusMaster on StatusMaster.StatusId=YMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and Year.Name>='" + currentyear + "' and YMaster.BusinessID='" + ddlsearchByStore.SelectedValue + "'";

            if (DropDownList1.SelectedIndex > 0)
            {
                y11 += " and YMaster.year='" + DropDownList1.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "BusiYear";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();

            ddlyfilter.Items.Insert(0, "-Select-");
            ddlyfilter.Items[0].Value = "0";
        }
    }

    protected void filldepartmentyear()
    {
        ddly.Items.Clear();
        string y12 = "";
        if (ddlStore.SelectedIndex > -1)
        {

            y12 = "select Year.Name  + ':' +  YMaster.title as DesiYear,YMaster.MasterId from Ymaster left outer join businessmaster on businessmaster.businessid=YMaster.divisionid inner join Warehousemaster on WareHouseMaster.WarehouseId=YMaster.Businessid inner join Year on Year.Id=YMaster.Year left outer join StatusMaster on StatusMaster.StatusId=YMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and Year.Name>='" + currentyear + "' and YMaster.departmentid='" + ddlStore.SelectedValue + "' and ymaster.divisionid IS NULL ";

            if (ddlyear.SelectedIndex > 0)
            {
                y12 += " and YMaster.year='" + ddlyear.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y12, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;

            ddly.DataTextField = "DesiYear";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";

        }
    }
    protected void filterfilldepartmentyear()
    {
        ddlyfilter.Items.Clear();

        string y12 = "";
        if (ddlsearchByStore.SelectedIndex > 0)
        {


            y12 = "select Year.Name  + ':' +  YMaster.title as DesiYear,YMaster.MasterId from Ymaster left outer join businessmaster on businessmaster.businessid=YMaster.divisionid inner join Warehousemaster on WareHouseMaster.WarehouseId=YMaster.Businessid inner join Year on Year.Id=YMaster.Year left outer join StatusMaster on StatusMaster.StatusId=YMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and Year.Name>='" + currentyear + "' and YMaster.departmentid='" + ddlsearchByStore.SelectedValue + "' and ymaster.divisionid IS NULL ";

            if (DropDownList1.SelectedIndex > 0)
            {
                y12 += " and YMaster.year='" + DropDownList1.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y12, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "DesiYear";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();
        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";
    }

    protected void filldevesion()
    {
        string dev = "select  distinct BusinessMaster.BusinessID,BusinessName,WareHouseMaster.Name,DepartmentmasterMNC.Departmentname, WareHouseMaster.Name +' : '+ DepartmentmasterMNC.Departmentname +' : '+ BusinessMaster.BusinessName  as Divisionname  from  BusinessMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid    where  BusinessMaster.company_id='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName";
        SqlDataAdapter ddev = new SqlDataAdapter(dev, con);
        DataTable dtdev = new DataTable();
        ddev.Fill(dtdev);

        if (dtdev.Rows.Count > 0)
        {
            ddlStore.DataSource = dtdev;
            ddlStore.DataTextField = "Divisionname";
            ddlStore.DataValueField = "BusinessID";
            ddlStore.DataBind();
        }
    }

    protected void filterfilldevesion()
    {
        string dev = "select  distinct BusinessMaster.BusinessID,BusinessName,WareHouseMaster.Name,DepartmentmasterMNC.Departmentname, WareHouseMaster.Name +' : '+ DepartmentmasterMNC.Departmentname +' : '+ BusinessMaster.BusinessName  as Divisionname  from  BusinessMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid    where  BusinessMaster.company_id='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname,BusinessMaster.BusinessName";
        SqlDataAdapter ddev = new SqlDataAdapter(dev, con);
        DataTable dtdev = new DataTable();
        ddev.Fill(dtdev);

        if (dtdev.Rows.Count > 0)
        {
            ddlsearchByStore.DataSource = dtdev;
            ddlsearchByStore.DataTextField = "Divisionname";
            ddlsearchByStore.DataValueField = "BusinessID";
            ddlsearchByStore.DataBind();
        }
        ddlsearchByStore.Items.Insert(0, "-Select-");
        ddlsearchByStore.Items[0].Value = "0";
    }

    protected void filldivisionyear()
    {
        ddly.Items.Clear();
        if (ddlStore.SelectedIndex > -1)
        {

            string y13 = "select Year.Name  + ':' +  YMaster.title as DivYear,YMaster.MasterId from Ymaster left outer join businessmaster on businessmaster.businessid=YMaster.divisionid inner join Warehousemaster on WareHouseMaster.WarehouseId=YMaster.Businessid inner join Year on Year.Id=YMaster.Year left outer join StatusMaster on StatusMaster.StatusId=YMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and Year.Name>='" + currentyear + "' and YMaster.divisionid='" + ddlStore.SelectedValue + "' and YMaster.employeeid IS NULL";

            if (ddlyear.SelectedIndex > 0)
            {
                y13 += " and YMaster.year='" + ddlyear.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y13, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "DivYear";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfilldivisionyear()
    {
        ddlyfilter.Items.Clear();
        if (ddlsearchByStore.SelectedIndex > 0)
        {

            string y13 = "select Year.Name  + ':' +  YMaster.title as DivYear,YMaster.MasterId from Ymaster left outer join businessmaster on businessmaster.businessid=YMaster.divisionid inner join Warehousemaster on WareHouseMaster.WarehouseId=YMaster.Businessid inner join Year on Year.Id=YMaster.Year left outer join StatusMaster on StatusMaster.StatusId=YMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and Year.Name>='" + currentyear + "' and YMaster.divisionid='" + ddlsearchByStore.SelectedValue + "' and YMaster.employeeid IS NULL";

            if (DropDownList1.SelectedIndex > 0)
            {
                y13 += " and YMaster.year='" + DropDownList1.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y13, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "DivYear";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();

        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";
    }

    protected void fillemployeeyear()
    {
        ddly.Items.Clear();
        if (ddlStore.SelectedIndex > -1)
        {

            string y13 = "select Year.Name  + ':' +  YMaster.title as DivYear,YMaster.MasterId from Ymaster left outer join businessmaster on businessmaster.businessid=YMaster.divisionid inner join Warehousemaster on WareHouseMaster.WarehouseId=YMaster.Businessid inner join Year on Year.Id=YMaster.Year left outer join StatusMaster on StatusMaster.StatusId=YMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and Year.Name>='" + currentyear + "' and YMaster.employeeid='" + ddlemployee.SelectedValue + "'";

            if (ddlyear.SelectedIndex > 0)
            {
                y13 += " and YMaster.year='" + ddlyear.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y13, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "DivYear";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfillemployeeyear()
    {
        ddlyfilter.Items.Clear();
        if (ddlsearchByStore.SelectedIndex > 0)
        {

            string y13 = "select Year.Name  + ':' +  YMaster.title as DivYear,YMaster.MasterId from Ymaster left outer join businessmaster on businessmaster.businessid=YMaster.divisionid inner join Warehousemaster on WareHouseMaster.WarehouseId=YMaster.Businessid inner join Year on Year.Id=YMaster.Year left outer join StatusMaster on StatusMaster.StatusId=YMaster.StatusId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "' and Year.Name>='" + currentyear + "' and YMaster.employeeid='" + DropDownList3.SelectedValue + "'";

            if (DropDownList1.SelectedIndex > 0)
            {
                y13 += " and YMaster.year='" + DropDownList1.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y13, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlyfilter.DataSource = dt;
            ddlyfilter.DataTextField = "DivYear";
            ddlyfilter.DataValueField = "MasterId";
            ddlyfilter.DataBind();
        }
        ddlyfilter.Items.Insert(0, "-Select-");
        ddlyfilter.Items[0].Value = "0";
    }
    protected void filmonthly()
    {
        ddlmonth.Items.Clear();
        if (ddlyear.SelectedIndex != -1)
        {

            if (ddlyear.SelectedItem.Text == currentyear)
            {
                ddlmonth.DataSource = ClsMMaster.SelctMonthonYear(currentyear, currentmonth);
                ddlmonth.DataMember = "yeayrmonth";
                ddlmonth.DataTextField = "yeayrmonth";
                ddlmonth.DataValueField = "Id";
                ddlmonth.DataBind();
            }
            if (ddlyear.SelectedItem.Text != currentyear)
            {
                SqlDataAdapter dafa = new SqlDataAdapter("Select Month.Id, Year.Name+ ' -> ' + Month.Name AS yeayrmonth from dbo.Month INNER JOIN dbo.Year ON dbo.Month.Yid = dbo.Year.Id where Year.Name='" + ddlyear.SelectedItem.Text + "' Order by Year.Name, Month.Id", con);
                DataTable dtfa = new DataTable();
                dafa.Fill(dtfa);

                ddlmonth.DataSource = dtfa;
                ddlmonth.DataMember = "yeayrmonth";
                ddlmonth.DataTextField = "yeayrmonth";
                ddlmonth.DataValueField = "Id";
                ddlmonth.DataBind();
            }
            ddlmonth.Items.Insert(0, "-Select-");
            ddlmonth.Items[0].Value = "0";

        }
    }
    protected void ddlyear_SelectedIndexChanged1(object sender, EventArgs e)
    {
        filmonthly();

        if (RadioButtonList1.SelectedValue == "0")
        {
            fillbusinessyear();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            if (RadioButtonList3.SelectedValue == "0")
            {
                filldepartmentyear();
            }
            if (RadioButtonList3.SelectedValue == "1")
            {
                filbusinessmonths();
            }
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            filldivisionyear();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            fillemployeeyear();
        }
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            filbusinessmonths();
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            if (DropDownList6.SelectedValue == "0")
            {
                filbusinessmonths11();
            }
            if (DropDownList6.SelectedValue == "1")
            {
                fildepartmentmonths();
            }
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            if (DropDownList7.SelectedValue == "0")
            {
                filbusinessmonths();
            }
            if (DropDownList7.SelectedValue == "1")
            {
                fildepartmentmonths111();
            }
            if (DropDownList7.SelectedValue == "2")
            {
                filemployeemonths();
            }
        }
    }
    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList3.SelectedValue == "0")
        {
            Panel5.Visible = true;
            pnlmonthly1.Visible = false;
            filldepartmentyear();
        }
        if (RadioButtonList3.SelectedValue == "1")
        {
            pnlmonthly1.Visible = true;
            Panel5.Visible = false;
            filbusinessmonths();
        }
    }
    protected void filbusinessmonths()
    {
        ddlbusimonthly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            string deped = "select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {

                if (ddlyear.SelectedIndex > 0)
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "' and MMaster.Departmentid IS NULL and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                else
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and MMaster.Departmentid IS NULL and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                if (ddlmonth.SelectedIndex > 0)
                {
                    y11 += " and Month.Id='" + ddlmonth.SelectedValue + "'";
                }

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlbusimonthly.DataSource = dt;
                ddlbusimonthly.DataTextField = "Title1";
                ddlbusimonthly.DataValueField = "MasterId";
                ddlbusimonthly.DataBind();
            }

        }
        ddlbusimonthly.Items.Insert(0, "-Select-");
        ddlbusimonthly.Items[0].Value = "0";
    }

    protected void filbusinessmonths11()
    {
        ddlbusimonthly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            string deped = "select WareHouseMaster.WareHouseId,DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {

                if (ddlyear.SelectedIndex > 0)
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "' and MMaster.Departmentid IS NULL and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                else
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and MMaster.Departmentid IS NULL and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                if (ddlmonth.SelectedIndex > 0)
                {
                    y11 += " and Month.Id='" + ddlmonth.SelectedValue + "'";
                }

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlbusimonthly.DataSource = dt;
                ddlbusimonthly.DataTextField = "Title1";
                ddlbusimonthly.DataValueField = "MasterId";
                ddlbusimonthly.DataBind();
            }

        }
        ddlbusimonthly.Items.Insert(0, "-Select-");
        ddlbusimonthly.Items[0].Value = "0";
    }

    protected void RadioButtonList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList4.SelectedValue == "0")
        {
            Panel5.Visible = true;
            Panel15.Visible = false;
            pnlmonthly1.Visible = false;
            pnlmonthly2.Visible = false;
            filldivisionyear();
        }
        if (RadioButtonList4.SelectedValue == "1")
        {
            Panel15.Visible = true;
            Panel5.Visible = false;
            pnlmonthly1.Visible = true;
            DropDownList6_SelectedIndexChanged(sender, e);
        }
    }
    protected void RadioButtonList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList5.SelectedValue == "0")
        {
            Panel5.Visible = true;
            Panel16.Visible = false;
            pnlmonthly1.Visible = false;
            pnlmonthly2.Visible = false;
            pnlmonthly3.Visible = false;
            fillemployeeyear();
        }
        if (RadioButtonList5.SelectedValue == "1")
        {
            Panel5.Visible = false;
            Panel16.Visible = true;
            pnlmonthly1.Visible = true;
            DropDownList7_SelectedIndexChanged(sender, e);
        }
    }
    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        filtermonthly();
        if (RadioButtonList2.SelectedValue == "4")
        {
            filterfillbusinessyear();
        }
        if (RadioButtonList2.SelectedValue == "0")
        {
            filterfilldepartmentyear();
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            filterfilldivisionyear();
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
            filterfillemployeeyear();
        }
        BindGrid();
    }
    protected void filtermonthly()
    {
        DropDownList4.Items.Clear();

        if (DropDownList1.SelectedIndex != -1)
        {
            SqlDataAdapter dafa = new SqlDataAdapter("Select Month.Id,Month.MonthId, Year.Name+ ' -> ' + Month.Name AS yeayrmonth from dbo.Month INNER JOIN dbo.Year ON dbo.Month.Yid = dbo.Year.Id where Year.Name='" + DropDownList1.SelectedItem.Text + "' Order by Year.Name, Month.Id", con);
            DataTable dtfa = new DataTable();
            dafa.Fill(dtfa);

            DropDownList4.DataSource = dtfa;
            DropDownList4.DataMember = "yeayrmonth";
            DropDownList4.DataTextField = "yeayrmonth";
            DropDownList4.DataValueField = "MonthId";
            DropDownList4.DataBind();

            DropDownList4.Items.Insert(0, "-Select-");
            DropDownList4.Items[0].Value = "0";

            //  ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(System.DateTime.Now.Month.ToString()));
        }
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "0")
        {
            if (dropdowndepartment.SelectedValue == "2")
            {
                filterfilbusinessmonths();
            }
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            if (dropdowndivision.SelectedValue == "2")
            {
                filterfilbusinessmonths111();
            }
            if (dropdowndivision.SelectedValue == "3")
            {
                filterfildepartmentmonths();
            }
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
            if (dropdownemployee.SelectedValue == "2")
            {
                filterfilbusinessmonths();
            }
            if (dropdownemployee.SelectedValue == "3")
            {
                filterfildepartmentmonths11();
            }
            if (dropdownemployee.SelectedValue == "4")
            {
                filterfilemployeemonths();
            }

        }
        BindGrid();
    }

    protected void fillteryear()
    {
        DropDownList1.Items.Clear();

        // ddlyear.DataSource = obj.Tablemaster("Select * from Year where Name>='" + currentyear + "'");
        DropDownList1.DataSource = obj.Tablemaster("Select * from Year");
        DropDownList1.DataMember = "Name";
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "Id";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";
        DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(System.DateTime.Now.Year.ToString()));
    }
    protected void dropdowndepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropdowndepartment.SelectedValue == "1")
        {
            Panel12.Visible = true;
            Panel14.Visible = false;
            filterfilldepartmentyear();
            DropDownList5.Items.Clear();
            BindGrid();
        }
        if (dropdowndepartment.SelectedValue == "2")
        {
            Panel12.Visible = false;
            Panel14.Visible = true;
            filterfilbusinessmonths();
            ddlyfilter.Items.Clear();
            BindGrid();
        }
        if (dropdowndepartment.SelectedValue == "0")
        {
            Panel12.Visible = false;
            Panel14.Visible = false;
            DropDownList5.Items.Clear();
            ddlyfilter.Items.Clear();
            BindGrid();
        }
    }


    protected void filterfilbusinessmonths()
    {
        DropDownList5.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > -1)
        {
            string deped = "select WareHouseMaster.WareHouseId from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  DepartmentmasterMNC.id='" + ddlsearchByStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {

                if (DropDownList1.SelectedIndex > 0)
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and  Year.Name='" + DropDownList1.SelectedItem.Text + "' and MMaster.Departmentid IS NULL and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                else
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and MMaster.Departmentid IS NULL and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                if (DropDownList4.SelectedIndex > 0)
                {
                    y11 += " and Month.Monthid='" + DropDownList4.SelectedValue + "'";
                }

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DropDownList5.DataSource = dt;
                DropDownList5.DataTextField = "Title1";
                DropDownList5.DataValueField = "MasterId";
                DropDownList5.DataBind();
            }

        }
        DropDownList5.Items.Insert(0, "-Select-");
        DropDownList5.Items[0].Value = "0";
    }
    protected void filterfilbusinessmonths111()
    {
        DropDownList5.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > -1)
        {
            string deped = "select WareHouseMaster.WareHouseId,DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlsearchByStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {
                if (DropDownList1.SelectedIndex > 0)
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and  Year.Name='" + DropDownList1.SelectedItem.Text + "' and MMaster.Departmentid IS NULL and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                else
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.businessid='" + dt3.Rows[0]["WareHouseId"].ToString() + "' and MMaster.Departmentid IS NULL and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                if (DropDownList4.SelectedIndex > 0)
                {
                    y11 += " and Month.Monthid='" + DropDownList4.SelectedValue + "'";
                }

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DropDownList5.DataSource = dt;
                DropDownList5.DataTextField = "Title1";
                DropDownList5.DataValueField = "MasterId";
                DropDownList5.DataBind();
            }

        }
        DropDownList5.Items.Insert(0, "-Select-");
        DropDownList5.Items[0].Value = "0";
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList6.SelectedValue == "0")
        {
            pnlmonthly1.Visible = true;
            pnlmonthly2.Visible = false;
            filbusinessmonths11();
        }
        if (DropDownList6.SelectedValue == "1")
        {
            pnlmonthly2.Visible = true;
            pnlmonthly1.Visible = false;
            fildepartmentmonths();
        }
    }


    protected void fildepartmentmonths()
    {
        ddldeptmonthly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            string deped = "select DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {

                if (ddlyear.SelectedIndex > 0)
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + dt3.Rows[0]["id"].ToString() + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                else
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + dt3.Rows[0]["id"].ToString() + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                if (ddlmonth.SelectedIndex > 0)
                {
                    y11 += " and Month.Id='" + ddlmonth.SelectedValue + "'";
                }

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddldeptmonthly.DataSource = dt;
                ddldeptmonthly.DataTextField = "Title1";
                ddldeptmonthly.DataValueField = "MasterId";
                ddldeptmonthly.DataBind();
            }

        }
        ddldeptmonthly.Items.Insert(0, "-Select-");
        ddldeptmonthly.Items[0].Value = "0";
    }

    protected void fildepartmentmonths111()
    {
        ddldeptmonthly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            if (ddlyear.SelectedIndex > 0)
            {
                y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + ddlStore.SelectedValue + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + ddlStore.SelectedValue + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and Month.Id='" + ddlmonth.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddldeptmonthly.DataSource = dt;
            ddldeptmonthly.DataTextField = "Title1";
            ddldeptmonthly.DataValueField = "MasterId";
            ddldeptmonthly.DataBind();
        }
        ddldeptmonthly.Items.Insert(0, "-Select-");
        ddldeptmonthly.Items[0].Value = "0";
    }

    protected void filterfildepartmentmonths()
    {
        DropDownList8.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > -1)
        {
            string deped = "select DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlsearchByStore.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {

                if (DropDownList1.SelectedIndex > 0)
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + dt3.Rows[0]["id"].ToString() + "' and  Year.Name='" + DropDownList1.SelectedItem.Text + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                else
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + dt3.Rows[0]["id"].ToString() + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                if (DropDownList4.SelectedIndex > 0)
                {
                    y11 += " and Month.Id='" + DropDownList4.SelectedValue + "'";
                }

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DropDownList8.DataSource = dt;
                DropDownList8.DataTextField = "Title1";
                DropDownList8.DataValueField = "MasterId";
                DropDownList8.DataBind();
            }

        }
        DropDownList8.Items.Insert(0, "-Select-");
        DropDownList8.Items[0].Value = "0";
    }
    protected void filterfildepartmentmonths11()
    {
        DropDownList8.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > -1)
        {


            if (DropDownList1.SelectedIndex > 0)
            {
                y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + ddlsearchByStore.SelectedValue + "' and  Year.Name='" + DropDownList1.SelectedItem.Text + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + ddlsearchByStore.SelectedValue + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and Month.Id='" + DropDownList4.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DropDownList8.DataSource = dt;
            DropDownList8.DataTextField = "Title1";
            DropDownList8.DataValueField = "MasterId";
            DropDownList8.DataBind();


        }
        DropDownList8.Items.Insert(0, "-Select-");
        DropDownList8.Items[0].Value = "0";
    }
    protected void dropdowndivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropdowndivision.SelectedValue == "1")
        {
            Panel12.Visible = true;
            Panel14.Visible = false;
            Panel17.Visible = false;
            filterfilldivisionyear();
            DropDownList5.Items.Clear();
            DropDownList8.Items.Clear();
            BindGrid();
        }
        if (dropdowndivision.SelectedValue == "2")
        {
            Panel12.Visible = false;
            Panel14.Visible = true;
            Panel17.Visible = false;
            filterfilbusinessmonths111();
            ddlyfilter.Items.Clear();
            DropDownList8.Items.Clear();
            BindGrid();
        }
        if (dropdowndivision.SelectedValue == "3")
        {
            Panel12.Visible = false;
            Panel14.Visible = false;
            Panel17.Visible = true;
            filterfildepartmentmonths();
            ddlyfilter.Items.Clear();
            DropDownList5.Items.Clear();
            BindGrid();
        }
        if (dropdowndivision.SelectedValue == "0")
        {
            Panel12.Visible = false;
            Panel14.Visible = false;
            Panel17.Visible = false;
            DropDownList5.Items.Clear();
            ddlyfilter.Items.Clear();
            DropDownList8.Items.Clear();
            BindGrid();
        }
    }
    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList7.SelectedValue == "0")
        {
            pnlmonthly1.Visible = true;
            pnlmonthly2.Visible = false;
            pnlmonthly3.Visible = false;
            filbusinessmonths();
        }
        if (DropDownList7.SelectedValue == "1")
        {
            pnlmonthly2.Visible = true;
            pnlmonthly1.Visible = false;
            pnlmonthly3.Visible = false;
            fildepartmentmonths111();
        }
        if (DropDownList7.SelectedValue == "2")
        {
            pnlmonthly2.Visible = false;
            pnlmonthly1.Visible = false;
            pnlmonthly3.Visible = true;
            filemployeemonths();
        }
    }

    protected void filemployeemonths()
    {
        ddldivimonthly.Items.Clear();

        string y11 = "";
        if (ddlemployee.SelectedIndex > -1)
        {
            string deped = "select employeemaster.DeptID as departmentid from employeemaster where employeemasterid='" + ddlemployee.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {

                if (ddlyear.SelectedIndex > 0)
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + dt3.Rows[0]["departmentid"].ToString() + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                else
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + dt3.Rows[0]["departmentid"].ToString() + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                if (ddlmonth.SelectedIndex > 0)
                {
                    y11 += " and Month.Id='" + ddlmonth.SelectedValue + "'";
                }

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddldivimonthly.DataSource = dt;
                ddldivimonthly.DataTextField = "Title1";
                ddldivimonthly.DataValueField = "MasterId";
                ddldivimonthly.DataBind();
            }

        }
        ddldivimonthly.Items.Insert(0, "-Select-");
        ddldivimonthly.Items[0].Value = "0";
    }

    protected void filterfilemployeemonths()
    {
        DropDownList9.Items.Clear();

        string y11 = "";
        if (DropDownList3.SelectedIndex > -1)
        {
            string deped = "select employeemaster.DeptID as departmentid from employeemaster where employeemasterid='" + DropDownList3.SelectedValue + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(deped, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {

                if (DropDownList1.SelectedIndex > 0)
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + dt3.Rows[0]["departmentid"].ToString() + "' and  Year.Name='" + DropDownList1.SelectedItem.Text + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                else
                {
                    y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join YMaster on YMaster.MasterId=MMaster.YMasterId  inner join Year on Month.Yid = Year.Id where MMaster.Departmentid='" + dt3.Rows[0]["departmentid"].ToString() + "' and MMaster.Divisionid IS NULL and MMaster.Employeeid IS NULL";
                }
                if (DropDownList4.SelectedIndex > 0)
                {
                    y11 += " and Month.Id='" + DropDownList4.SelectedValue + "'";
                }

                SqlDataAdapter da = new SqlDataAdapter(y11, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DropDownList9.DataSource = dt;
                DropDownList9.DataTextField = "Title1";
                DropDownList9.DataValueField = "MasterId";
                DropDownList9.DataBind();
            }

        }
        DropDownList9.Items.Insert(0, "-Select-");
        DropDownList9.Items[0].Value = "0";
    }

    protected void dropdownemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropdownemployee.SelectedValue == "1")
        {
            Panel12.Visible = true;
            Panel14.Visible = false;
            Panel17.Visible = false;
            Panel18.Visible = false;
            filterfillemployeeyear();
            DropDownList5.Items.Clear();
            DropDownList8.Items.Clear();
            DropDownList9.Items.Clear();
            BindGrid();
        }
        if (dropdownemployee.SelectedValue == "2")
        {
            Panel12.Visible = false;
            Panel14.Visible = true;
            Panel17.Visible = false;
            Panel18.Visible = false;
            filterfilbusinessmonths();
            ddlyfilter.Items.Clear();
            DropDownList8.Items.Clear();
            DropDownList9.Items.Clear();
            BindGrid();
        }
        if (dropdownemployee.SelectedValue == "3")
        {
            Panel12.Visible = false;
            Panel14.Visible = false;
            Panel17.Visible = true;
            Panel18.Visible = false;
            filterfildepartmentmonths11();
            ddlyfilter.Items.Clear();
            DropDownList5.Items.Clear();
            DropDownList9.Items.Clear();
            BindGrid();
        }
        if (dropdownemployee.SelectedValue == "4")
        {
            Panel12.Visible = false;
            Panel14.Visible = false;
            Panel17.Visible = false;
            Panel18.Visible = true;
            filterfilemployeemonths();
            ddlyfilter.Items.Clear();
            DropDownList5.Items.Clear();
            DropDownList8.Items.Clear();
            BindGrid();
        }
        if (dropdownemployee.SelectedValue == "0")
        {
            Panel12.Visible = false;
            Panel14.Visible = false;
            Panel17.Visible = false;
            Panel18.Visible = false;
            DropDownList5.Items.Clear();
            ddlyfilter.Items.Clear();
            DropDownList8.Items.Clear();
            DropDownList9.Items.Clear();
            BindGrid();
        }
    }
    protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void DropDownList9_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

}
