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
public partial class Admin_FrmWDetail : System.Web.UI.Page
{
    static int temp;
    static DataTable dt;
    DataByCompany obj = new DataByCompany();
    DocumentCls1 clsDocument = new DocumentCls1();

    SqlConnection con;

    string currentmonth = System.DateTime.Now.Month.ToString();
    string currentdate = System.DateTime.Now.ToShortDateString();
    string currentyear = System.DateTime.Now.Year.ToString();
    string compid;

    protected void Page_Load(object sender, EventArgs e)
    {
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

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        if (!IsPostBack)
        {
            //for freetextbox in page load...//

            ViewState["sortOrder"] = "";


            txtincdate.Text = DateTime.Now.ToShortDateString();
            fillstore();


            filyear();
            RadioButtonList1_SelectedIndexChanged(sender, e);
            fillteryear();
            RadioButtonList2_SelectedIndexChanged(sender, e);


            SqlDataAdapter dafa = new SqlDataAdapter("Select Month.Id,Month.MonthId, Year.Name+ ' -> ' + Month.Name AS yeayrmonth from dbo.Month INNER JOIN dbo.Year ON dbo.Month.Yid = dbo.Year.Id where Year.Name='" + ddlyear.SelectedItem.Text + "' Order by Year.Name, Month.Id", con);
            DataTable dtfa = new DataTable();
            dafa.Fill(dtfa);

            ddlmonth.DataSource = dtfa;
            ddlmonth.DataMember = "yeayrmonth";
            ddlmonth.DataTextField = "yeayrmonth";
            ddlmonth.DataValueField = "MonthId";
            ddlmonth.DataBind();

            ddlmonth.Items.Insert(0, "-Select-");
            ddlmonth.Items[0].Value = "0";

            ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(System.DateTime.Now.Month.ToString()));


            string myweek = "Select Week.Id,Week.Name AS yeayrmonth from dbo.Week INNER JOIN dbo.Month ON dbo.Week.Mid = dbo.Month.Id inner join year on year.id=month.yid where year.Name='" + ddlyear.SelectedItem.Text + "' and month.monthid='" + currentmonth + "'";

            SqlDataAdapter dafa1 = new SqlDataAdapter(myweek, con);
            DataTable dtfa1 = new DataTable();
            dafa1.Fill(dtfa1);

            ddlweek.DataSource = dtfa1;
            ddlweek.DataMember = "yeayrmonth";
            ddlweek.DataTextField = "yeayrmonth";
            ddlweek.DataValueField = "Id";
            ddlweek.DataBind();

            ddlweek.Items.Insert(0, "-Select-");
            ddlweek.Items[0].Value = "0";

            SqlDataAdapter daw = new SqlDataAdapter("select Id from week where FirstDate1<='" + currentdate + "' and lastdate1>='" + currentdate + "'", con);
            DataTable dtw = new DataTable();
            daw.Fill(dtw);

            ddlweek.SelectedIndex = ddlweek.Items.IndexOf(ddlweek.Items.FindByValue(dtw.Rows[0]["Id"].ToString()));

            filterfillbusinessweek();

            BindGrid();


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
            ddlsearchByStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        //ddlsearchByStore.Items.Insert(0, "All");
        //ddlsearchByStore.Items[0].Value = "0";
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
        DropDownList2.Items.Insert(0, "All");
        DropDownList2.Items[0].Value = "0";
    }

    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            fillbusinessmonth();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            filldepartmentmonth();
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            filldivisionmonth();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            fillemployee();
            fillemployeemonth();
        }
        filllweek();
        //filltgmain();
        //ddly_SelectedIndexChanged(sender, e);
    }


    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {

        filltgmain();
        ddly_SelectedIndexChanged(sender, e);
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployeemonth();
        filllweek();
        //filltgmain();
        //ddly_SelectedIndexChanged(sender, e);
    }
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

                //                    Page.RegisterClientScriptBlock("newWindow", popupScript);
                //LinkButton lnkbtn = (LinkButton)Gridreqinfo.FindControl("LinkButton1");
                //lnkbtn.Attributes.Add("onclick", "window.open('ViewDocument.aspx?id='" + e.CommandArgument + ",, 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')");


                String temp = "ViewDocument.aspx?id=" + docid + "&Siddd=VHDS";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);


                //    Response.Redirect("ViewDocument.aspx?id=" + docid + "&Siddd=VHDS");
            }
            else
            {
                FileInfo file = new FileInfo(filepath);

                if (file.Exists)
                {
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = ReturnExtension(file.Extension.ToLower());
                    Response.TransmitFile(file.FullName);

                    Response.End();

                }
            }
        }
    }



    protected void ddlsearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "4")
        {
            filterfillbusinessweek();
        }
        if (RadioButtonList2.SelectedValue == "0")
        {
            filterfilldepartmentweek();
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            filterfilldivisionweek();
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
            Filteremployee();
            filterfillemployeeweek();
        }
        //  filllweekfilter();
        BindGrid();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        filltg();
        ddlyfilter_SelectedIndexChanged(sender, e);
        BindGrid();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        filltg();
        ddlyfilter_SelectedIndexChanged(sender, e);

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
            ds12 = ClsWDetail.SelectMddfilter(ddlStore.SelectedValue, "0", bus, em, flag);
        }
        else
        {
            ds12 = ClsWDetail.SelectMddfilter("0", ddlStore.SelectedValue, bus, em, flag);

        }
        if (ds12.Rows.Count > 0)
        {
            ddly.DataSource = ds12;

            ddly.DataMember = "Title";
            ddly.DataTextField = "Title";
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
            ds12 = ClsWDetail.SelectMddfilter(ddlsearchByStore.SelectedValue, "0", DropDownList2.SelectedValue, DropDownList3.SelectedValue, flag);
        }
        else
        {
            ds12 = ClsWDetail.SelectMddfilter("0", ddlsearchByStore.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, flag);

        }
        if (ds12.Rows.Count > 0)
        {
            ddlyfilter.DataSource = ds12;

            ddlyfilter.DataMember = "Title";
            ddlyfilter.DataTextField = "Title";
            ddlyfilter.DataValueField = "masterid";


            ddlyfilter.DataBind();


        }
        ddlyfilter.Items.Insert(0, "All");
        ddlyfilter.Items[0].Value = "0";


    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterfillemployeeweek();
        //  filllweekfilter();
        BindGrid();
        //filltg();
        //ddlyfilter_SelectedIndexChanged(sender, e);

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
        int success = ClsWDetail.SpWDetailAddData(Convert.ToString(ddlm.SelectedValue), Convert.ToDateTime(txtincdate.Text).ToShortDateString(), txtdetail.Text, Convert.ToString(Session["UserId"]));

        if (success > 0)
        {
            statuslable.Text = "Record inserted successfully";
            if (chk.Checked == true)
            {

                string whid = "";
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    whid = ddlStore.SelectedValue;
                }
                else if (RadioButtonList1.SelectedIndex == 1 || RadioButtonList1.SelectedIndex == 3)
                {
                    DataTable df = MainAcocount.SelectWhidwithdeptid(ddlStore.SelectedValue);
                    whid = Convert.ToString(df.Rows[0]["Whid"]);
                }
                else if (RadioButtonList1.SelectedIndex == 2)
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId,DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlStore.SelectedValue + "'", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    whid = dt1.Rows[0]["WareHouseId"].ToString();
                }

                string te = "AddDocMaster.aspx?wed=" + success + "&storeid=" + whid + "";



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
            statuslable.Text = "Record  not inserted successfully";
        }


        EmptyControls();
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {

        int success = ClsWDetail.SpWDetailUpdateData(ViewState["updateid"].ToString(), Convert.ToDateTime(txtincdate.Text).ToShortDateString(), ddlm.SelectedValue, txtdetail.Text.ToString());


        if (success > 0)
        {
            if (chk.Checked == true)
            {

                string whid = "";
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    whid = ddlStore.SelectedValue;
                }
                else if (RadioButtonList1.SelectedIndex == 1 || RadioButtonList1.SelectedIndex == 3)
                {
                    DataTable df = MainAcocount.SelectWhidwithdeptid(ddlStore.SelectedValue);
                    whid = Convert.ToString(df.Rows[0]["Whid"]);
                }
                else if (RadioButtonList1.SelectedIndex == 2)
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("select WareHouseMaster.WareHouseId,DepartmentmasterMNC.id from businessmaster inner join  DepartmentmasterMNC on DepartmentmasterMNC.id=businessmaster.departmentid inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid   where  businessmaster.businessid='" + ddlStore.SelectedValue + "'", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    whid = dt1.Rows[0]["WareHouseId"].ToString();
                }

                string te = "AddDocMaster.aspx?wed=" + ViewState["updateid"].ToString() + "&storeid=" + whid + "";



                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


            }
            BindGrid();
            statuslable.Text = "Record updated successfully";
        }
        else
        {
            statuslable.Text = "Record  already existed";
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
        txtincdate.Text = DateTime.Now.ToShortDateString();
        txtdetail.Text = "";
    }
    private void BindGrid()
    {

        string st1 = "";
        lblBusiness.Text = "";
        lblBusiness1.Text = "";
        lblDepartmemnt.Text = "";
        lblDivision.Text = "";
        lblEmp.Text = "";
        string strw = "";

        if (RadioButtonList2.SelectedIndex == 0)
        {
            lblcompany.Text = Session["Comid"].ToString();
            lblBusiness.Text = ddlsearchByStore.SelectedItem.Text;
            lblBusiness1.Text = "Business Name : " + ddlsearchByStore.SelectedItem.Text;
            if (ddlsearchByStore.SelectedIndex > 0)
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
            }
            else
            {
                lblcompany.Text = Session["Comid"].ToString();
                lblBusiness.Text = ddlsearchByStore.SelectedItem.Text;

            }
        }
        if (RadioButtonList2.SelectedIndex == 1)
        {


            lblDepartmemnt.Text = "Business : Department - " + Convert.ToString(ddlsearchByStore.SelectedItem.Text);
            if (ddlsearchByStore.SelectedValue == "0")
            {
                st1 = " and YMaster.DepartmentId>0   and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL";

            }
            else
            {
                st1 = " and YMaster.DepartmentId='" + ddlsearchByStore.SelectedValue + "'  and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL";

            }
        }
        else if (RadioButtonList2.SelectedIndex == 2)
        {
            lblDivision.Text = "Business : Department : Division - " + ddlsearchByStore.SelectedItem.Text;

            if (ddlsearchByStore.SelectedValue == "0")
            {
                st1 = " and YMaster.divisionid>0 and YMaster.EmployeeId IS NULL";
            }
            else
            {
                st1 = " and YMaster.divisionid='" + ddlsearchByStore.SelectedValue + "' and YMaster.EmployeeId IS NULL";
            }

        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            lblDepartmemnt.Text = "Business : Department - " + Convert.ToString(ddlsearchByStore.SelectedItem.Text);
            lblDivision.Text = ",";
            lblEmp.Text = "Employee " + "   :  " + DropDownList3.SelectedItem.Text;
            if (DropDownList3.SelectedValue == "0")
            {

                if (Convert.ToInt32(ddlsearchByStore.SelectedValue) > 0)
                {
                    st1 = " and YMaster.DepartmentId='" + ddlsearchByStore.SelectedValue + "' and YMaster.EmployeeId>'0'";


                }
                else
                {
                    st1 = " and YMaster.EmployeeId>0";
                }
            }
            else
            {
                st1 = " and YMaster.EmployeeId='" + DropDownList3.SelectedValue + "'";

            }
        }

        if (ddlyear.SelectedIndex > 0)
        {
            st1 += " and YMaster.year='" + ddlyear.SelectedValue + "'";
        }

        if (ddlmonth.SelectedIndex > 0)
        {
            st1 += " and Month.Monthid='" + ddlmonth.SelectedValue + "'";
        }

        if (ddlweek.SelectedIndex > 0)
        {
            st1 += " and week.id='" + ddlweek.SelectedValue + "'";
        }

        lblmonthtext.Text = "Weekly Goal : -Select- ";

        if (ddlmfilter.SelectedIndex > 0)
        {
            lblmonthtext.Text = "Weekly Goal : " + ddlmfilter.SelectedItem.Text;

            st1 += " and WMaster.MasterId='" + ddlmfilter.SelectedValue + "'";
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

            strw = "  " + filc + " WMaster.MasterId,WDetail.DetailId,WDetail.Detail,Left(MMaster.Title,20) as mtitle, Left(WMaster.Title,20) as Title , objectivemaster.ObjectiveName,Convert(nvarchar, WDetail.Date,101) as Date,EmployeeMaster.EmployeeMasterID as DocumentId,objectivemaster.DepartmentId as Dept_Id from WDetail inner join WMaster on WMaster.MasterId=Wdetail.MasterId inner join week on week.id=wmaster.week inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join STGMaster on STGMaster.MasterId=Ymaster.StgMasterId inner join  LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId left outer join BusinessMaster  on objectivemaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectivemaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ObjectiveMaster.StoreId where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " ";

            str2 = " select Count(WMaster.MasterId) as ci from WDetail inner join WMaster on WMaster.MasterId=Wdetail.MasterId inner join week on week.id=wmaster.week inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join month on month.id=mmaster.month inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join STGMaster on STGMaster.MasterId=Ymaster.StgMasterId inner join  LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId left outer join BusinessMaster  on objectivemaster.BusinessId=BusinessMaster.BusinessID left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=objectivemaster.EmployeeId left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ObjectiveMaster.DepartmentId inner join WareHouseMaster on WareHouseMaster.WareHouseId=ObjectiveMaster.StoreId where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " ";
        }
        else if (RadioButtonList2.SelectedIndex == 1)
        {
            // filc = "  LEFT(WareHouseMaster.Name,4) as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname,";
            grid.Columns[0].HeaderText = "Busi";
            grid.Columns[1].HeaderText = "Department";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = true;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = false;

            strw = "  WareHouseMaster.Name as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname, WMaster.MasterId,WDetail.DetailId,WDetail.Detail,Left(MMaster.Title,20) as mtitle, Left(WMaster.Title,20) as Title , Convert(nvarchar, WDetail.Date,101) as Date,EmployeeMaster.EmployeeMasterID as DocumentId from WDetail  inner join WMaster on WMaster.MasterId=Wdetail.MasterId inner join week on week.id=wmaster.week inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join month on month.id=mmaster.month inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on YMaster.Divisionid=BusinessMaster.BusinessID  left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId  inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " ";

            str2 = " select Count(WMaster.MasterId) as ci from WDetail  inner join WMaster on WMaster.MasterId=Wdetail.MasterId inner join week on week.id=wmaster.week inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join month on month.id=mmaster.month inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on YMaster.Divisionid=BusinessMaster.BusinessID  left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId  inner join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " ";
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

            strw = "  WareHouseMaster.Name as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname, WMaster.MasterId,WDetail.DetailId,WDetail.Detail,Left(MMaster.Title,20) as mtitle, Left(WMaster.Title,20) as Title , Convert(nvarchar, WDetail.Date,101) as Date,EmployeeMaster.EmployeeMasterID as DocumentId from WDetail  inner join WMaster on WMaster.MasterId=Wdetail.MasterId inner join week on week.id=wmaster.week inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join month on month.id=mmaster.month inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  inner join BusinessMaster  on YMaster.Divisionid=BusinessMaster.BusinessID  left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId  left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " ";

            str2 = " select Count(WMaster.MasterId) as ci from WDetail  inner join WMaster on WMaster.MasterId=Wdetail.MasterId inner join week on week.id=wmaster.week inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join month on month.id=mmaster.month inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  inner join BusinessMaster  on YMaster.Divisionid=BusinessMaster.BusinessID  left outer join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId  left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " ";
        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            //   filc = "  LEFT(WareHouseMaster.Name,4) as Wname, LEFT(BusinessMaster.BusinessName,4) as businessname,EmployeeMaster.EmployeeName,LEFT(DepartmentmasterMNC.Departmentname,4)as Departmentname,";
            grid.Columns[0].HeaderText = "Busi";
            grid.Columns[1].HeaderText = "Dept";
            grid.Columns[2].HeaderText = "Divi";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Visible = false;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = true;

            strw = "  WareHouseMaster.Name as Wname,BusinessMaster.BusinessName as businessname,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname, WMaster.MasterId,WDetail.DetailId,WDetail.Detail,Left(MMaster.Title,20) as mtitle, Left(WMaster.Title,20) as Title , Convert(nvarchar, WDetail.Date,101) as Date,EmployeeMaster.EmployeeMasterID as DocumentId from WDetail  inner join WMaster on WMaster.MasterId=Wdetail.MasterId inner join week on week.id=wmaster.week inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join month on month.id=mmaster.month inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on YMaster.Divisionid=BusinessMaster.BusinessID  inner join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId  left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " ";

            str2 = " select Count(WMaster.MasterId) as ci from WDetail  inner join WMaster on WMaster.MasterId=Wdetail.MasterId inner join week on week.id=wmaster.week inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join month on month.id=mmaster.month inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  left outer join BusinessMaster  on YMaster.Divisionid=BusinessMaster.BusinessID  inner join [EmployeeMaster] on [EmployeeMaster].EmployeeMasterID=ymaster.EmployeeId  left outer join DepartmentmasterMNC on DepartmentmasterMNC.id=ymaster.DepartmentId  inner join WareHouseMaster on WareHouseMaster.WareHouseId=ymaster.businessid  where  WareHouseMaster.comid='" + Session["Comid"].ToString() + "' " + st1 + " ";
        }
        //   DataTable dt2 = ClsWDetail.SpWDetailGridfill(st1, filc);

        grid.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,MMaster.title,WMaster.Title asc";

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
                DataTable dtcrNew11 = ClsWDetail.SelectOfficeManagerDocumentsforWDetailID(Convert.ToString(dt2.Rows[rowindex]["DetailId"]));

                dt2.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];
            }
            grid.DataBind();
        }
        else
        {
            grid.DataSource = null;
            grid.DataBind();
        }



        // ClsMDetail.SpMDetailGetDataByMasterId(Convert.ToString(ddlm.SelectedValue));
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


        //StWDetail st;
        ////give data source to object of structure
        //st = ClsWDetail.SpWDetailGetDataStructById(id);


        //hdnid.Value = st.detailid.ToString();


        //ddlbusiness.Items.Clear();
        //ddlemployee.Items.Clear();
        //PnlEmployee.Visible = false;
        //PnlDivision.Visible = false;

        //if (Convert.ToInt32(st.Deptid) > 0)
        //{
        //    lblwname0.Text = "Business-Department Name : ";
        //    RadioButtonList1.SelectedIndex = 1;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        fillDepartment();
        //    }

        //    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue((st.Deptid).ToString()));

        //}
        //else
        //{
        //    RadioButtonList1.SelectedIndex = 0;
        //    lblwname0.Text = "Business Name : ";
        //    if (Convert.ToString(ViewState["cd"]) != "1")
        //    {
        //        fillstore();
        //    }
        //    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(st.StoreId.ToString()));

        //}


        //if ((st.employeeid.ToString() != "0"))
        //{
        //    fillemployee();
        //    PnlEmployee.Visible = true;
        //    RadioButtonList1.SelectedIndex = 3;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        fillDepartment();
        //    }
        //    fillemployee();

        //    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(st.employeeid.ToString()));
        //    // ddlEmp_SelectedIndexChanged(sender, e);
        //}
        //else if (Convert.ToString(st.BusId) != "0")
        //{
        //    PnlDivision.Visible = true;
        //    // PnlDp.Visible = false;
        //    RadioButtonList1.SelectedIndex = 2;
        //    if (Convert.ToString(ViewState["cd"]) != "2")
        //    {
        //        fillDepartment();
        //    }

        //    fillDivision();
        //    ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(st.BusId.ToString()));
        //    ddlbusiness_SelectedIndexChanged(sender, e);
        //}


        //filltgmain();
        //ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(st.Mid.ToString()));



        //ddly_SelectedIndexChanged(sender, e);



        //ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByValue(st.masterid.ToString()));
        ////ddlemployee.SelectedValue = st.employeeid.ToString();
        //txtdetail.Text = st.detail.ToString();
        //txtincdate.Text = Convert.ToDateTime(st.date).ToShortDateString();
        //btncancel.Visible = true;
        //btnupdate.Visible = true;

        //btnsubmit.Visible = false;
        //btnreset.Visible = false;
        //Pnladdnew.Visible = true;

        //btnadd.Visible = false;

    }


    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Get the ID of the record to be deleted
        string id = grid.DataKeys[e.RowIndex].Value.ToString();

        // Execute the delete command
        bool success = ClsWDetail.SpWDetailDeleteData(id);

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
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblwname0.Text = "Business-Department Name ";
        imgadddept.Visible = false;
        imgdeptrefresh.Visible = false;

        imgadddivision.Visible = false;
        imgdivisionrefresh.Visible = false;

        if (RadioButtonList1.SelectedValue == "0")
        {
            PnlDivision.Visible = false;
            PnlEmployee.Visible = false;
            lblwname0.Text = "Business Name ";

            ddlemployee.Items.Clear();
            ddlbusiness.Items.Clear();
            //if (Convert.ToString(ViewState["cd"]) != "1")
            //{
            fillstore();
            //  fillbusinessmonth();

            // }
        }
        if (RadioButtonList1.SelectedValue == "1")
        {


            PnlDivision.Visible = false;
            imgadddept.Visible = true;
            imgdeptrefresh.Visible = true;
            PnlEmployee.Visible = false;
            ddlemployee.Items.Clear();
            ddlbusiness.Items.Clear();
            //if (Convert.ToString(ViewState["cd"]) != "2")
            //{
            fillDepartment();
            //   filldepartmentmonth();
            // }
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            imgadddivision.Visible = true;
            imgdivisionrefresh.Visible = true;

            lblwname0.Text = "Business-Department-Division Name ";

            PnlDivision.Visible = false;

            PnlEmployee.Visible = false;
            ddlemployee.Items.Clear();

            //if (Convert.ToString(ViewState["cd"]) != "2")
            //{
            //  fillDepartment();
            //  }
            // fillDivision();
            filldevesion();
            //  filldivisionmonth();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            PnlEmployee.Visible = true;
            PnlDivision.Visible = false;
            ddlbusiness.Items.Clear();
            //if (Convert.ToString(ViewState["cd"]) != "2")
            //{
            fillDepartment();
            //  }
            fillemployee();
            //  fillemployeemonth();

        }

        if (RadioButtonList1.SelectedValue == "")
        {
            PnlDivision.Visible = false;

            PnlEmployee.Visible = false;

        }
        DropDownList1_SelectedIndexChanged1(sender, e);
        DropDownList4_SelectedIndexChanged(sender, e);
        filllweek();
        //filltgmain();
        //ddly_SelectedIndexChanged(sender, e);
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblwnamefilter.Text = "Business-Department Name ";
        if (RadioButtonList2.SelectedValue == "0")
        {

            Panel6.Visible = false;
            Panel7.Visible = false;


            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{
            FilterDepartment();
            //filterfilldepartmentmonth();
            // }
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            lblwnamefilter.Text = "Business-Department-Division Name ";

            Panel6.Visible = false;
            Panel7.Visible = false;
            DropDownList3.Items.Clear();

            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{
            //      FilterDepartment();
            // }
            //     filterDivision();
            filterfilldevesion();
            //filterfilldivisionmonth();
        }
        if (RadioButtonList2.SelectedValue == "2")
        {

            Panel6.Visible = false;
            Panel7.Visible = true;
            DropDownList2.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "2")
            //{
            FilterDepartment();
            //}
            Filteremployee();
            //filterfillemployeemonth();
        }
        if (RadioButtonList2.SelectedValue == "4")
        {

            Panel6.Visible = false;
            Panel7.Visible = false;
            lblwnamefilter.Text = "Business Name ";

            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            //if (Convert.ToString(ViewState["cdf"]) != "1")
            //{
            fillstorewithfilter();
            //filterfillbusinessmonth();
            //}
        }
        //filllweekfilter();

        ddlyear_SelectedIndexChanged(sender, e);
        ddlmonth_SelectedIndexChanged(sender, e);
        BindGrid();
        //filltg();
        //ddlyfilter_SelectedIndexChanged(sender, e);
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

        string te = "frmMMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgdivisionrefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillDivision();
        filltgmain();
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
            fillbusinessmonth();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            filldepartmentmonth();
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            filldivisionmonth();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            fillemployeemonth();
        }
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

            if (grid.Columns[11].Visible == true)
            {
                ViewState["docs"] = "tt";
                grid.Columns[11].Visible = false;
            }
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

            grid.AllowPaging = true;
            grid.PageSize = 10;
            BindGrid();

            if (ViewState["docs"] != null)
            {
                grid.Columns[11].Visible = true;
            }
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



    protected void ddlyfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlmfilter.Items.Clear();
        if (ddlyfilter.SelectedIndex > 0)
        {



            ddlmfilter.DataSource = ClsWDetail.SpWeekMasterGetDataBymmasterid(Convert.ToString(ddlyfilter.SelectedValue));
            ddlmfilter.DataMember = "title";
            ddlmfilter.DataTextField = "title";
            ddlmfilter.DataValueField = "masterid";
            ddlmfilter.DataBind();


        }

        ddlmfilter.Items.Insert(0, "All");
        ddlmfilter.Items[0].Value = "0";

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
            ViewState["DetailId"] = Convert.ToInt32(e.CommandArgument);

            // DataTable dtcrNew11 = ClsSTGMaster.SelectOfficedocwithstgid(Convert.ToString(ViewState["MissionId"]));
            DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["DetailId"]), 61);

            GridView1.DataSource = dtcrNew11;
            GridView1.DataBind();



            ModalPopupExtenderAddnew.Show();



        }
        else if (e.CommandName == "view")
        {

            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "frmWMasterReport.aspx?Wid=" + dk;

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }

        if (e.CommandName == "Edit")
        {
            //string id = grid.DataKeys[e.NewSelectedIndex].Value.ToString();

            lbllegend.Text = "Edit Instruction";
            statuslable.Text = "";

            string id = Convert.ToString(e.CommandArgument);

            ViewState["updateid"] = id;

            //StWDetail st;

            //st = ClsWDetail.SpWDetailGetDataStructById(id);


            //hdnid.Value = st.detailid.ToString();


            if (RadioButtonList2.SelectedValue == "4")
            {
                PnlEmployee.Visible = false;
                PnlDivision.Visible = false;

                string dfd = "Select  WDetail.*,WMaster.MMasterId, ObjectiveMaster.BusinessId,ObjectiveMaster.DepartmentId,ObjectiveMaster.EmployeeId,ObjectiveMaster.StoreId from WDetail inner join WMaster on WMaster.MasterId=WDetail.MasterId inner join MMaster on MMaster.masterid=WMaster.MMasterId inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join STGMaster on STGMaster.MasterId=YMaster.StgMasterId  inner join LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId where detailid=" + id;
                SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                DataTable dtfg = new DataTable();
                dafg.Fill(dtfg);

                RadioButtonList1.SelectedValue = "0";
                RadioButtonList1_SelectedIndexChanged(sender, e);

                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["storeid"].ToString()));

                fillbusinessmonth();
                ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["MMasterId"].ToString()));

                SqlDataAdapter da111 = new SqlDataAdapter("select month.id,month.monthid from month inner join mmaster on mmaster.month=month.id where mmaster.masterid='" + dtfg.Rows[0]["MMasterId"].ToString() + "'", con);
                DataTable dt111 = new DataTable();
                da111.Fill(dt111);

                SqlDataAdapter da222 = new SqlDataAdapter("select year.name from year inner join month on month.yid=year.id where month.id='" + dt111.Rows[0]["id"].ToString() + "'", con);
                DataTable dt222 = new DataTable();
                da222.Fill(dt222);

                DropDownList1.Items.Clear();

                // ddlyear.DataSource = obj.Tablemaster("Select * from Year where Name>='" + currentyear + "'");
                DropDownList1.DataSource = obj.Tablemaster("Select * from Year");
                DropDownList1.DataMember = "Name";
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "Id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "-Select-");
                DropDownList1.Items[0].Value = "0";

                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dt222.Rows[0]["name"].ToString()));

                filmonthlyuper();
                DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dt111.Rows[0]["monthid"].ToString()));

                fillweeklyuper();

                filllweek();
                ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByValue(dtfg.Rows[0]["MasterId"].ToString()));

                SqlDataAdapter da333 = new SqlDataAdapter("select week.id from week inner join wmaster on week.id=wmaster.week where wmaster.masterid='" + dtfg.Rows[0]["MasterId"].ToString() + "'", con);
                DataTable dt333 = new DataTable();
                da333.Fill(dt333);

                DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dt333.Rows[0]["id"].ToString()));

                txtdetail.Text = dtfg.Rows[0]["Detail"].ToString();

                txtincdate.Text = Convert.ToDateTime(dtfg.Rows[0]["Date"].ToString()).ToShortDateString();
            }

            if (RadioButtonList2.SelectedValue == "0")
            {
                PnlEmployee.Visible = false;
                PnlDivision.Visible = false;

                string dfd = "Select  WDetail.*,WMaster.MMasterId, YMaster.BusinessId,YMaster.DepartmentId,YMaster.EmployeeId,YMaster.Divisionid from WDetail inner join WMaster on WMaster.MasterId=WDetail.MasterId inner join MMaster on MMaster.masterid=WMaster.MMasterId inner join YMaster on YMaster.MasterId=MMaster.YMasterId where detailid=" + id;
                SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                DataTable dtfg = new DataTable();
                dafg.Fill(dtfg);

                RadioButtonList1.SelectedValue = "1";
                RadioButtonList1_SelectedIndexChanged(sender, e);

                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["DepartmentId"].ToString()));

                filldepartmentmonth();
                ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["MMasterId"].ToString()));

                SqlDataAdapter da111 = new SqlDataAdapter("select month.id,month.monthid from month inner join mmaster on mmaster.month=month.id where mmaster.masterid='" + dtfg.Rows[0]["MMasterId"].ToString() + "'", con);
                DataTable dt111 = new DataTable();
                da111.Fill(dt111);

                SqlDataAdapter da222 = new SqlDataAdapter("select year.name from year inner join month on month.yid=year.id where month.id='" + dt111.Rows[0]["id"].ToString() + "'", con);
                DataTable dt222 = new DataTable();
                da222.Fill(dt222);

                DropDownList1.Items.Clear();

                // ddlyear.DataSource = obj.Tablemaster("Select * from Year where Name>='" + currentyear + "'");
                DropDownList1.DataSource = obj.Tablemaster("Select * from Year");
                DropDownList1.DataMember = "Name";
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "Id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "-Select-");
                DropDownList1.Items[0].Value = "0";

                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dt222.Rows[0]["name"].ToString()));

                filmonthlyuper();
                DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dt111.Rows[0]["monthid"].ToString()));

                fillweeklyuper();

                filllweek();
                ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByValue(dtfg.Rows[0]["MasterId"].ToString()));

                SqlDataAdapter da333 = new SqlDataAdapter("select week.id from week inner join wmaster on week.id=wmaster.week where wmaster.masterid='" + dtfg.Rows[0]["MasterId"].ToString() + "'", con);
                DataTable dt333 = new DataTable();
                da333.Fill(dt333);

                DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dt333.Rows[0]["id"].ToString()));


                txtdetail.Text = dtfg.Rows[0]["Detail"].ToString();

                txtincdate.Text = Convert.ToDateTime(dtfg.Rows[0]["Date"].ToString()).ToShortDateString();
            }

            if (RadioButtonList2.SelectedValue == "1")
            {
                PnlEmployee.Visible = false;
                PnlDivision.Visible = false;

                string dfd = "Select  WDetail.*,WMaster.MMasterId, YMaster.BusinessId,YMaster.DepartmentId,YMaster.EmployeeId,YMaster.Divisionid from WDetail inner join WMaster on WMaster.MasterId=WDetail.MasterId inner join MMaster on MMaster.masterid=WMaster.MMasterId inner join YMaster on YMaster.MasterId=MMaster.YMasterId where detailid=" + id;
                SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                DataTable dtfg = new DataTable();
                dafg.Fill(dtfg);

                RadioButtonList1.SelectedValue = "2";
                RadioButtonList1_SelectedIndexChanged(sender, e);

                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["Divisionid"].ToString()));

                filldivisionmonth();
                ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["MMasterId"].ToString()));

                SqlDataAdapter da111 = new SqlDataAdapter("select month.id,month.monthid from month inner join mmaster on mmaster.month=month.id where mmaster.masterid='" + dtfg.Rows[0]["MMasterId"].ToString() + "'", con);
                DataTable dt111 = new DataTable();
                da111.Fill(dt111);

                SqlDataAdapter da222 = new SqlDataAdapter("select year.name from year inner join month on month.yid=year.id where month.id='" + dt111.Rows[0]["id"].ToString() + "'", con);
                DataTable dt222 = new DataTable();
                da222.Fill(dt222);

                DropDownList1.Items.Clear();

                // ddlyear.DataSource = obj.Tablemaster("Select * from Year where Name>='" + currentyear + "'");
                DropDownList1.DataSource = obj.Tablemaster("Select * from Year");
                DropDownList1.DataMember = "Name";
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "Id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "-Select-");
                DropDownList1.Items[0].Value = "0";

                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dt222.Rows[0]["name"].ToString()));

                filmonthlyuper();
                DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dt111.Rows[0]["monthid"].ToString()));

                fillweeklyuper();

                filllweek();
                ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByValue(dtfg.Rows[0]["MasterId"].ToString()));

                SqlDataAdapter da333 = new SqlDataAdapter("select week.id from week inner join wmaster on week.id=wmaster.week where wmaster.masterid='" + dtfg.Rows[0]["MasterId"].ToString() + "'", con);
                DataTable dt333 = new DataTable();
                da333.Fill(dt333);

                DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dt333.Rows[0]["id"].ToString()));


                txtdetail.Text = dtfg.Rows[0]["Detail"].ToString();

                txtincdate.Text = Convert.ToDateTime(dtfg.Rows[0]["Date"].ToString()).ToShortDateString();
            }

            if (RadioButtonList2.SelectedValue == "2")
            {
                PnlEmployee.Visible = true;
                PnlDivision.Visible = false;

                string dfd = "Select  WDetail.*,WMaster.MMasterId, YMaster.BusinessId,YMaster.DepartmentId,YMaster.EmployeeId,YMaster.Divisionid from WDetail inner join WMaster on WMaster.MasterId=WDetail.MasterId inner join MMaster on MMaster.masterid=WMaster.MMasterId inner join YMaster on YMaster.MasterId=MMaster.YMasterId where detailid=" + id;
                SqlDataAdapter dafg = new SqlDataAdapter(dfd, con);
                DataTable dtfg = new DataTable();
                dafg.Fill(dtfg);

                RadioButtonList1.SelectedValue = "3";
                RadioButtonList1_SelectedIndexChanged(sender, e);

                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtfg.Rows[0]["DepartmentId"].ToString()));

                fillemployee();
                ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dtfg.Rows[0]["EmployeeId"].ToString()));

                fillemployeemonth();
                ddly.SelectedIndex = ddly.Items.IndexOf(ddly.Items.FindByValue(dtfg.Rows[0]["MMasterId"].ToString()));

                SqlDataAdapter da111 = new SqlDataAdapter("select month.id,month.monthid from month inner join mmaster on mmaster.month=month.id where mmaster.masterid='" + dtfg.Rows[0]["MMasterId"].ToString() + "'", con);
                DataTable dt111 = new DataTable();
                da111.Fill(dt111);

                SqlDataAdapter da222 = new SqlDataAdapter("select year.name from year inner join month on month.yid=year.id where month.id='" + dt111.Rows[0]["id"].ToString() + "'", con);
                DataTable dt222 = new DataTable();
                da222.Fill(dt222);

                DropDownList1.Items.Clear();

                // ddlyear.DataSource = obj.Tablemaster("Select * from Year where Name>='" + currentyear + "'");
                DropDownList1.DataSource = obj.Tablemaster("Select * from Year");
                DropDownList1.DataMember = "Name";
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "Id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "-Select-");
                DropDownList1.Items[0].Value = "0";

                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dt222.Rows[0]["name"].ToString()));

                filmonthlyuper();
                DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dt111.Rows[0]["monthid"].ToString()));

                fillweeklyuper();

                filllweek();
                ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByValue(dtfg.Rows[0]["MasterId"].ToString()));

                SqlDataAdapter da333 = new SqlDataAdapter("select week.id from week inner join wmaster on week.id=wmaster.week where wmaster.masterid='" + dtfg.Rows[0]["MasterId"].ToString() + "'", con);
                DataTable dt333 = new DataTable();
                da333.Fill(dt333);

                DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dt333.Rows[0]["id"].ToString()));


                txtdetail.Text = dtfg.Rows[0]["Detail"].ToString();

                txtincdate.Text = Convert.ToDateTime(dtfg.Rows[0]["Date"].ToString()).ToShortDateString();
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
        filllweek();

        //ddlm.Items.Clear();
        //if (ddly.SelectedValue != "")
        //{



        //    ddlm.DataSource = ClsWDetail.SpWeekMasterGetDataBymmasterid(Convert.ToString(ddly.SelectedValue));
        //    ddlm.DataMember = "title";
        //    ddlm.DataTextField = "title";
        //    ddlm.DataValueField = "masterid";
        //    ddlm.DataBind();


        //}
    }
    protected void ddlmfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }


    protected void imgmonthadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "frmWMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgmonthrefresh_Click(object sender, ImageClickEventArgs e)
    {
        //ddly_SelectedIndexChanged(sender, e);
        filllweek();
    }


    protected void btnadd_Click(object sender, EventArgs e)
    {
        Pnladdnew.Visible = true;
        if (Pnladdnew.Visible == true)
        {
            btnadd.Visible = false;
        }
        statuslable.Text = "";
        lbllegend.Text = "Add Instruction";
        statuslable.Text = "";
        RadioButtonList1.SelectedValue = "0";
        RadioButtonList1_SelectedIndexChanged(sender, e);
    }

    protected void fillbusinessmonth()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.businessid='" + ddlStore.SelectedValue + "' and YMaster.DepartmentId IS NULL and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL";
            //order by Month.Name,MMaster.Title asc";
            if (DropDownList1.SelectedIndex > 0)
            {
                y11 += " and year.name='" + DropDownList1.SelectedItem.Text + "'";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and month.monthid='" + DropDownList4.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfillbusinessmonth()
    {
        ddlmonthfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.businessid='" + ddlsearchByStore.SelectedValue + "' and YMaster.DepartmentId IS NULL and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlmonthfilter.DataSource = dt;
            ddlmonthfilter.DataTextField = "Title1";
            ddlmonthfilter.DataValueField = "MasterId";
            ddlmonthfilter.DataBind();
        }
        ddlmonthfilter.Items.Insert(0, "-Select-");
        ddlmonthfilter.Items[0].Value = "0";

    }

    protected void filldepartmentmonth()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.DepartmentId='" + ddlStore.SelectedValue + "' and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL";
            //order by Month.Name,MMaster.Title asc";

            if (DropDownList1.SelectedIndex > 0)
            {
                y11 += " and year.name='" + DropDownList1.SelectedItem.Text + "'";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and month.monthid='" + DropDownList4.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfilldepartmentmonth()
    {
        ddlmonthfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.DepartmentId='" + ddlsearchByStore.SelectedValue + "' and YMaster.divisionid IS NULL and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlmonthfilter.DataSource = dt;
            ddlmonthfilter.DataTextField = "Title1";
            ddlmonthfilter.DataValueField = "MasterId";
            ddlmonthfilter.DataBind();

        }
        ddlmonthfilter.Items.Insert(0, "-Select-");
        ddlmonthfilter.Items[0].Value = "0";
    }

    protected void filldivisionmonth()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.DivisionID='" + ddlStore.SelectedValue + "' and YMaster.EmployeeId IS NULL";
            //order by Month.Name,MMaster.Title asc";
            if (DropDownList1.SelectedIndex > 0)
            {
                y11 += " and year.name='" + DropDownList1.SelectedItem.Text + "'";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and month.monthid='" + DropDownList4.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfilldivisionmonth()
    {
        ddlmonthfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.DivisionID='" + ddlsearchByStore.SelectedValue + "' and YMaster.EmployeeId IS NULL order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlmonthfilter.DataSource = dt;
            ddlmonthfilter.DataTextField = "Title1";
            ddlmonthfilter.DataValueField = "MasterId";
            ddlmonthfilter.DataBind();

        }
        ddlmonthfilter.Items.Insert(0, "-Select-");
        ddlmonthfilter.Items[0].Value = "0";
    }

    protected void fillemployeemonth()
    {
        ddly.Items.Clear();

        string y11 = "";
        if (ddlStore.SelectedIndex > -1)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join year on year.id=ymaster.year  where YMaster.EmployeeId='" + ddlemployee.SelectedValue + "'";
            //order by Month.Name,MMaster.Title asc";
            if (DropDownList1.SelectedIndex > 0)
            {
                y11 += " and year.name='" + DropDownList1.SelectedItem.Text + "'";
            }
            if (DropDownList4.SelectedIndex > 0)
            {
                y11 += " and month.monthid='" + DropDownList4.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddly.DataSource = dt;
            ddly.DataTextField = "Title1";
            ddly.DataValueField = "MasterId";
            ddly.DataBind();
            ddly.Items.Insert(0, "-Select-");
            ddly.Items[0].Value = "0";
        }
    }

    protected void filterfillemployeemonth()
    {
        ddlmonthfilter.Items.Clear();

        string y11 = "";
        if (ddlsearchByStore.SelectedIndex > 0)
        {
            y11 = "select distinct MMaster.MasterId,Month.Name,MMaster.Title ,Month.Name +':'+ MMaster.Title as Title1 from Month inner join MMaster on MMaster.Month=Month.Id inner join  YMaster on YMaster.MasterId=MMaster.YMasterId  where YMaster.EmployeeId='" + DropDownList3.SelectedValue + "' order by Month.Name,MMaster.Title asc";
            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlmonthfilter.DataSource = dt;
            ddlmonthfilter.DataTextField = "Title1";
            ddlmonthfilter.DataValueField = "MasterId";
            ddlmonthfilter.DataBind();

        }
        ddlmonthfilter.Items.Insert(0, "-Select-");
        ddlmonthfilter.Items[0].Value = "0";
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

    protected void filllweek()
    {
        ddlm.Items.Clear();

        string y11 = "";

        if (ddly.SelectedIndex > 0)
        {
            string given = "select month.monthid from month inner join mmaster on MMaster.Month=Month.Id where mmaster.masterid='" + ddly.SelectedValue + "'";
            SqlDataAdapter da = new SqlDataAdapter(given, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string debut = "";

            if (dt.Rows.Count > 0)
            {
                debut = Convert.ToString(dt.Rows[0]["monthid"]);
            }

            if (debut != currentmonth)
            {
                string st1 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId where WMaster.MMasterId='" + ddly.SelectedValue + "'";
                // inner join  YMaster on YMaster.MasterId=MMaster.YMasterId

                if (DropDownList5.SelectedIndex > 0)
                {
                    st1 += " and week.id='" + DropDownList5.SelectedValue + "'";
                }

                SqlDataAdapter dafa = new SqlDataAdapter(st1, con);
                DataTable dtfa = new DataTable();
                dafa.Fill(dtfa);

                ddlm.DataSource = dtfa;
                ddlm.DataTextField = "Title1";
                ddlm.DataValueField = "MasterId";
                ddlm.DataBind();

            }

            if (debut == currentmonth)
            {
                string st2 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId where WMaster.MMasterId='" + ddly.SelectedValue + "' and week.lastdate1>='" + currentdate + "'";
                //inner join  YMaster on YMaster.MasterId=MMaster.YMasterId

                if (DropDownList5.SelectedIndex > 0)
                {
                    st2 += " and week.id='" + DropDownList5.SelectedValue + "'";
                }

                SqlDataAdapter dafa = new SqlDataAdapter(st2, con);
                DataTable dtfa = new DataTable();
                dafa.Fill(dtfa);

                ddlm.DataSource = dtfa;
                ddlm.DataTextField = "Title1";
                ddlm.DataValueField = "MasterId";
                ddlm.DataBind();
            }
        }
        ddlm.Items.Insert(0, "-Select-");
        ddlm.Items[0].Value = "0";
    }

    protected void filterfillbusinessweek()
    {
        ddlmfilter.Items.Clear();

        string y11 = "";

        if (ddlsearchByStore.SelectedIndex > -1)
        {
            if (ddlyear.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Businessid='" + ddlsearchByStore.SelectedValue + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "' and YMaster.Departmentid IS NULL and YMaster.Divisionid IS NULL and YMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Businessid='" + ddlsearchByStore.SelectedValue + "' and YMaster.Departmentid IS NULL and YMaster.Divisionid IS NULL and YMaster.Employeeid IS NULL";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and Month.MonthId='" + ddlmonth.SelectedValue + "'";
            }
            if (ddlweek.SelectedIndex > 0)
            {
                y11 += " and week.id='" + ddlweek.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlmfilter.DataSource = dt;
            ddlmfilter.DataTextField = "Title1";
            ddlmfilter.DataValueField = "MasterId";
            ddlmfilter.DataBind();
        }
        ddlmfilter.Items.Insert(0, "-Select-");
        ddlmfilter.Items[0].Value = "0";
    }


    protected void filterfilldepartmentweek()
    {
        ddlmfilter.Items.Clear();

        string y11 = "";

        if (ddlsearchByStore.SelectedIndex > -1)
        {
            if (ddlyear.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Departmentid='" + ddlsearchByStore.SelectedValue + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "' and YMaster.Divisionid IS NULL and YMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Departmentid='" + ddlsearchByStore.SelectedValue + "' and YMaster.Divisionid IS NULL and YMaster.Employeeid IS NULL";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and Month.MonthId='" + ddlmonth.SelectedValue + "'";
            }
            if (ddlweek.SelectedIndex > 0)
            {
                y11 += " and week.id='" + ddlweek.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlmfilter.DataSource = dt;
            ddlmfilter.DataTextField = "Title1";
            ddlmfilter.DataValueField = "MasterId";
            ddlmfilter.DataBind();
        }
        ddlmfilter.Items.Insert(0, "-Select-");
        ddlmfilter.Items[0].Value = "0";
    }


    protected void filterfilldivisionweek()
    {
        ddlmfilter.Items.Clear();

        string y11 = "";

        if (ddlsearchByStore.SelectedIndex > -1)
        {
            if (ddlyear.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Divisionid='" + ddlsearchByStore.SelectedValue + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "'  and YMaster.Employeeid IS NULL";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Divisionid='" + ddlsearchByStore.SelectedValue + "' and YMaster.Employeeid IS NULL";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and Month.MonthId='" + ddlmonth.SelectedValue + "'";
            }
            if (ddlweek.SelectedIndex > 0)
            {
                y11 += " and week.id='" + ddlweek.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlmfilter.DataSource = dt;
            ddlmfilter.DataTextField = "Title1";
            ddlmfilter.DataValueField = "MasterId";
            ddlmfilter.DataBind();
        }
        ddlmfilter.Items.Insert(0, "-Select-");
        ddlmfilter.Items[0].Value = "0";
    }


    protected void filterfillemployeeweek()
    {
        ddlmfilter.Items.Clear();

        string y11 = "";

        if (ddlsearchByStore.SelectedIndex > -1)
        {
            if (ddlyear.SelectedIndex > 0)
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Employeeid='" + DropDownList3.SelectedValue + "' and  Year.Name='" + ddlyear.SelectedItem.Text + "'";
            }
            else
            {
                y11 = "select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId inner join Year on Month.Yid = Year.Id where YMaster.Employeeid='" + DropDownList3.SelectedValue + "'";
            }
            if (ddlmonth.SelectedIndex > 0)
            {
                y11 += " and Month.MonthId='" + ddlmonth.SelectedValue + "'";
            }
            if (ddlweek.SelectedIndex > 0)
            {
                y11 += " and week.id='" + ddlweek.SelectedValue + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(y11, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlmfilter.DataSource = dt;
            ddlmfilter.DataTextField = "Title1";
            ddlmfilter.DataValueField = "MasterId";
            ddlmfilter.DataBind();
        }
        ddlmfilter.Items.Insert(0, "-Select-");
        ddlmfilter.Items[0].Value = "0";
    }

    //protected void filllweekfilter()
    //{
    //    ddlmfilter.Items.Clear();

    //    string y11 = "";

    //    if (ddlmonthfilter.SelectedIndex > 0)
    //    {
    //        string given = "select month.monthid from month inner join mmaster on MMaster.Month=Month.Id where mmaster.masterid='" + ddlmonthfilter.SelectedValue + "'";
    //        SqlDataAdapter da = new SqlDataAdapter(given, con);
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);

    //        string debut = "";

    //        if (dt.Rows.Count > 0)
    //        {
    //            debut = Convert.ToString(dt.Rows[0]["monthid"]);
    //        }

    //        if (debut != currentmonth)
    //        {
    //            SqlDataAdapter dafa = new SqlDataAdapter("select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId where WMaster.MMasterId='" + ddlmonthfilter.SelectedValue + "'", con);
    //            DataTable dtfa = new DataTable();
    //            dafa.Fill(dtfa);

    //            ddlmfilter.DataSource = dtfa;
    //            ddlmfilter.DataTextField = "Title1";
    //            ddlmfilter.DataValueField = "MasterId";
    //            ddlmfilter.DataBind();

    //        }

    //        if (debut == currentmonth)
    //        {
    //            SqlDataAdapter dafa = new SqlDataAdapter("select distinct WMaster.MasterId,Week.Name +':'+ WMaster.Title as Title1 from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id  inner join MMaster on MMaster.MasterId=WMaster.MMasterId inner join  YMaster on YMaster.MasterId=MMaster.YMasterId where WMaster.MMasterId='" + ddlmonthfilter.SelectedValue + "' and week.lastdate1>='" + currentdate + "'", con);
    //            DataTable dtfa = new DataTable();
    //            dafa.Fill(dtfa);

    //            ddlmfilter.DataSource = dtfa;
    //            ddlmfilter.DataTextField = "Title1";
    //            ddlmfilter.DataValueField = "MasterId";
    //            ddlmfilter.DataBind();
    //        }
    //    }
    //    ddlmfilter.Items.Insert(0, "-Select-");
    //    ddlmfilter.Items[0].Value = "0";
    //}
    protected void ddlmonthfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        //filllweekfilter();
        BindGrid();
    }
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        filmonthly();

        BindGrid();
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillweekly();
        if (RadioButtonList2.SelectedValue == "4")
        {
            filterfillbusinessweek();
        }
        if (RadioButtonList2.SelectedValue == "0")
        {
            filterfilldepartmentweek();
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            filterfilldivisionweek();
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
            filterfillemployeeweek();
        }
        BindGrid();
    }
    protected void ddlweek_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "4")
        {
            filterfillbusinessweek();
        }
        if (RadioButtonList2.SelectedValue == "0")
        {
            filterfilldepartmentweek();
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            filterfilldivisionweek();
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
            filterfillemployeeweek();
        }
        BindGrid();
    }

    protected void filyear()
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


    protected void fillteryear()
    {
        ddlyear.Items.Clear();

        // ddlyear.DataSource = obj.Tablemaster("Select * from Year where Name>='" + currentyear + "'");
        ddlyear.DataSource = obj.Tablemaster("Select * from Year");
        ddlyear.DataMember = "Name";
        ddlyear.DataTextField = "Name";
        ddlyear.DataValueField = "Id";
        ddlyear.DataBind();
        ddlyear.Items.Insert(0, "-Select-");
        ddlyear.Items[0].Value = "0";
        ddlyear.SelectedIndex = ddlyear.Items.IndexOf(ddlyear.Items.FindByText(System.DateTime.Now.Year.ToString()));
    }

    protected void filmonthly()
    {
        ddlmonth.Items.Clear();
        if (ddlyear.SelectedIndex != -1)
        {
            SqlDataAdapter dafa = new SqlDataAdapter("Select Month.Id,Month.MonthId, Year.Name+ ' -> ' + Month.Name AS yeayrmonth from dbo.Month INNER JOIN dbo.Year ON dbo.Month.Yid = dbo.Year.Id where Year.Name='" + ddlyear.SelectedItem.Text + "' Order by Year.Name, Month.Id", con);
            DataTable dtfa = new DataTable();
            dafa.Fill(dtfa);

            ddlmonth.DataSource = dtfa;
            ddlmonth.DataMember = "yeayrmonth";
            ddlmonth.DataTextField = "yeayrmonth";
            ddlmonth.DataValueField = "MonthId";
            ddlmonth.DataBind();

            ddlmonth.Items.Insert(0, "-Select-");
            ddlmonth.Items[0].Value = "0";

            //  ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(System.DateTime.Now.Month.ToString()));
        }
    }

    protected void filmonthlyuper()
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

    protected void fillweekly()
    {
        ddlweek.Items.Clear();

        if (ddlmonth.SelectedIndex != -1)
        {
            string myweek = "Select Week.Id,Week.Name AS yeayrmonth from dbo.Week INNER JOIN dbo.Month ON dbo.Week.Mid = dbo.Month.Id inner join year on year.id=month.yid where year.Name='" + ddlyear.SelectedItem.Text + "' and month.monthid='" + ddlmonth.SelectedValue + "'";

            SqlDataAdapter dafa = new SqlDataAdapter(myweek, con);
            DataTable dtfa = new DataTable();
            dafa.Fill(dtfa);

            ddlweek.DataSource = dtfa;
            ddlweek.DataMember = "yeayrmonth";
            ddlweek.DataTextField = "yeayrmonth";
            ddlweek.DataValueField = "Id";
            ddlweek.DataBind();

            ddlweek.Items.Insert(0, "-Select-");
            ddlweek.Items[0].Value = "0";
        }
    }

    protected void fillweeklyuper()
    {
        DropDownList5.Items.Clear();

        if (DropDownList4.SelectedIndex != -1)
        {
            string myweek = "Select Week.Id,Week.Name AS yeayrmonth from dbo.Week INNER JOIN dbo.Month ON dbo.Week.Mid = dbo.Month.Id inner join year on year.id=month.yid where year.Name='" + DropDownList1.SelectedItem.Text + "' and month.monthid='" + DropDownList4.SelectedValue + "'";

            SqlDataAdapter dafa = new SqlDataAdapter(myweek, con);
            DataTable dtfa = new DataTable();
            dafa.Fill(dtfa);

            DropDownList5.DataSource = dtfa;
            DropDownList5.DataMember = "yeayrmonth";
            DropDownList5.DataTextField = "yeayrmonth";
            DropDownList5.DataValueField = "Id";
            DropDownList5.DataBind();

            DropDownList5.Items.Insert(0, "-Select-");
            DropDownList5.Items[0].Value = "0";
        }
    }

    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        filmonthlyuper();
        if (RadioButtonList1.SelectedValue == "0")
        {
            fillbusinessmonth();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            filldepartmentmonth();
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            filldivisionmonth();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            fillemployeemonth();
        }
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillweeklyuper();
        if (RadioButtonList1.SelectedValue == "0")
        {
            fillbusinessmonth();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            filldepartmentmonth();
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            filldivisionmonth();
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            fillemployeemonth();

        }
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        filllweek();
    }
}
