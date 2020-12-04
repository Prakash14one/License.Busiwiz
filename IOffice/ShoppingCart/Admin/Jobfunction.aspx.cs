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

public partial class ShoppingCart_Admin_Default2 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    DataTable dt;
    DataSet ds;
    SqlDataAdapter da;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
            }

            ViewState["sortOrder"] = "";

            //fillwarehouse();
            fillddl_business();
            test();
            filldesignation();
            filljobfuncat();
            filljobfunsubcat();
            fillwarehouse1();
            test1();
            ddlbusiness1_SelectedIndexChanged(sender, e);
            //fillgrid();
            fillgriddata();
            //ddldeprdesg1.Items.Insert(0, "All");
            //ddljobfuncat1.Items.Insert(0, "All");
            ddljobfunsubcat1.Items.Insert(0, "All");

            // ddlbusiness.SelectedIndex = 0;
        }
    }

    protected void fillddl_business()
    {
        String s = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["Comid"].ToString() + "' and WareHouseMaster.Status='1' and EmployeeWarehouseRights.AccessAllowed='True' order by name";
        da = new SqlDataAdapter(s, con);

        ds = new DataSet();
        da.Fill(ds);

        ddlbusiness.DataSource = ds;
        ddlbusiness.DataTextField = "Name";
        ddlbusiness.DataValueField = "WareHouseId";
        ddlbusiness.DataBind();

    }
    protected void test()
    {
        string ee = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
        SqlCommand cmdeeed = new SqlCommand(ee, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }

    protected void test1()
    {
        string ee = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
        SqlCommand cmdeeed = new SqlCommand(ee, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ddlbusiness1.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }

    protected void fillwarehouse()
    {

        clsjobfunction objjobfunction = new clsjobfunction();
        objjobfunction.comid = Session["comid"].ToString();
        DataTable ds1 = new DataTable();
        ds1 = objjobfunction.selec_filliwh();
        //      if (ds1.Rows.Count > 0)
        //      {
        ddlbusiness.DataSource = ds1;
        ddlbusiness.DataTextField = "Name";
        ddlbusiness.DataValueField = "WarehouseId";
        ddlbusiness.DataBind();
        //     }
        // ddlbusiness.Items.Insert(0, "All");
        //  ddlbusiness.Items[0].Value = "0";
    }

    protected void filljobfuncat()
    {
        ddljobfuncat.Items.Clear();

        clsjobfunction objjobfunction = new clsjobfunction();
        ddljobfuncat.Items.Clear();
        objjobfunction.Whid = Convert.ToInt32(ddlbusiness.SelectedValue);

        DataTable dt = new DataTable();
        dt = objjobfunction.selec_jobfunct();

        if (dt.Rows.Count > 0)
        {
            ddljobfuncat.DataSource = dt;
            ddljobfuncat.DataTextField = "CategoryName";
            ddljobfuncat.DataValueField = "Id";
            ddljobfuncat.DataBind();
        }

        ddljobfuncat.Items.Insert(0, "-Select-");
        ddljobfuncat.Items[0].Value = "0";

    }

    protected void filljobfunsubcat()
    {
        ddljobfunsubcat.Items.Clear();

        clsjobfunction objjobfunction = new clsjobfunction();
        ddljobfunsubcat.Items.Clear();

        if (ddljobfuncat.SelectedIndex >= 0)
        {
            objjobfunction.JobCategoryId = Convert.ToInt32(ddljobfuncat.SelectedValue);
        }
        DataTable dt = new DataTable();
        dt = objjobfunction.selec_jobfunsubct();


        if (dt.Rows.Count > 0)
        {
            ddljobfunsubcat.DataSource = dt;
            ddljobfunsubcat.DataTextField = "SubCategoryName";
            ddljobfunsubcat.DataValueField = "Id";
            ddljobfunsubcat.DataBind();
        }
        ddljobfunsubcat.Items.Insert(0, "-Select-");
        ddljobfunsubcat.Items[0].Value = "0";

    }



    protected void ddljobfuncat_SelectedIndexChanged(object sender, EventArgs e)
    {

        filljobfunsubcat();
        //clsjobfunction objjobfunction = new clsjobfunction();
        //objjobfunction.JobCategoryId = Convert.ToInt32(ddljobfuncat.SelectedValue);
        //DataTable dt = new DataTable();
        //dt = objjobfunction.selec_jobfunsubct();

        //if (dt.Rows.Count > 0)
        //{
        //    ddljobfunsubcat.DataSource = dt;
        //    ddljobfunsubcat.DataTextField = "SubCategoryName";
        //    ddljobfunsubcat.DataValueField = "Id";
        //    ddljobfunsubcat.DataBind();
        //}
    }


    // protected void test()
    // {

    // }

    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsjobfunction objjobfunction = new clsjobfunction();
        objjobfunction.comid = Session["Comid"].ToString();

        objjobfunction.Whid = Convert.ToInt32(ddlbusiness.SelectedValue);
        DataTable dt = new DataTable();
        dt = objjobfunction.selec_deprdes();

        if (dt.Rows.Count > 0)
        {
            ddldepartdesg.DataSource = dt;
            ddldepartdesg.DataTextField = "name";
            ddldepartdesg.DataValueField = "DesignationMasterId";
            ddldepartdesg.DataBind();
        }
        else
        {
            ddldepartdesg.Items.Clear();
        }
        //     ddlbusiness.Items.Insert(0, "All");
        //     ddlbusiness.Items[0].Value = "0";

        filljobfuncat();
        filljobfunsubcat();
    }
    protected void filldesignation()
    {
        clsjobfunction objjobfunction = new clsjobfunction();
        objjobfunction.comid = Session["Comid"].ToString();
        objjobfunction.Whid = Convert.ToInt32(ddlbusiness.SelectedValue);
        //  objjobfunction.DeptID = Convert.ToInt32(ddlbusiness.SelectedValue);
        DataTable dt = new DataTable();
        dt = objjobfunction.selec_deprdes();

        if (dt.Rows.Count > 0)
        {
            ddldepartdesg.DataSource = dt;
            ddldepartdesg.DataTextField = "name";
            ddldepartdesg.DataValueField = "DesignationMasterId";
            ddldepartdesg.DataBind();
        }
        //    ddldepartdesg.Items.Insert(0, "-Select-");
        //    ddldepartdesg.SelectedItem.Value = "0";

    }
    protected void fillgrid()
    {
        Label1.Text = ddlbusiness1.SelectedItem.Text;

        clsjobfunction objjobfunction = new clsjobfunction();
        dt = new DataTable();
        dt = objjobfunction.selec_fillinggrdvw();

        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataSource = myDataView;
        GridView1.DataBind();
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //string man = FileUpload1.FileName;
        clsjobfunction objjobfunction = new clsjobfunction();
        objjobfunction.Whid = Convert.ToInt32(ddlbusiness.SelectedValue);
        objjobfunction.DesignationId = Convert.ToInt32(ddldepartdesg.SelectedValue);
        objjobfunction.JobfunctioncategoryId = Convert.ToInt32(ddljobfuncat.SelectedValue);
        objjobfunction.JobfunctionsubcategoryId = Convert.ToInt32(ddljobfunsubcat.SelectedValue);
        objjobfunction.Jobfunctiontitle = txtjobfuntitle.Text;
        objjobfunction.Jobfunctiondescription = txtjobdesc.Text;
        objjobfunction.Status = Convert.ToInt32(chkstatus.Checked);
        objjobfunction.executeinsertquery();

        //if (FileUpload1.HasFile)
        //{
        //    FileUpload1.SaveAs(Server.MapPath("~/NewFolder1/") + man);
        //}
        clear();
        //fillgrid();
        fillgriddata();
        lblmessage.Visible = true;
        lblmessage.Text = "Record inserted successfully.";
        lblmessage.Visible = true;

        Panel1.Visible = false;
        Button1.Visible = true;
        lbllegend.Text = "";
    }

    protected void clear()
    {
        ddlbusiness.SelectedIndex = 0;
        // ddldepartdesg.Items.Insert(0,"ALL");
        ddldepartdesg.SelectedIndex = 0;
        if (ddljobfuncat.SelectedIndex >= 0)
        {
            ddljobfuncat.SelectedIndex = 0;
        }
        //  ddljobfunsubcat.Items.Insert(0, "ALL");
        if (ddljobfunsubcat.SelectedIndex >= 0)
        {
            ddljobfunsubcat.SelectedIndex = 0;
        }
        txtjobfuntitle.Text = "";
        txtjobdesc.Text = "";
        chkstatus.Checked = false;
    }

    protected void ImageButton48_Click(object sender, ImageClickEventArgs e)
    {
        string te = "DesignationAddManage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton49_Click(object sender, ImageClickEventArgs e)
    {
        filldesignation();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        clsjobfunction objjobfunction = new clsjobfunction();



        if (e.CommandName == "Delete")
        {

            int i = Convert.ToInt32(e.CommandArgument);
            //  Session["ID11"] = i;


            objjobfunction.Id = i;
            objjobfunction.executeinsertquerydelet();

            //cmd = new SqlCommand("delete from JobFunction where Id=" + i ,con);
            //if (cmd.Connection.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmd.ExecuteNonQuery();
            //con.Close();
            lblmessage.Visible = true;
            lblmessage.Text = "Record deleted successfully.";
        }

        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "Edit Job Function";

            Button1.Visible = false;
            Panel1.Visible = true;
            int i = Convert.ToInt32(e.CommandArgument);
            objjobfunction.Id = i;

            lblId.Text = i.ToString();
            dt = new DataTable();
            dt = objjobfunction.selec_grdviewrowcomd();
            //     string str = "select * from Jobfunction where Id = '" + i + "'";
            //     SqlCommand cmd = new SqlCommand(str, con);
            //     SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //     DataTable dt = new DataTable();
            //     adpt.Fill(dt);


            fillwarehouse();
            ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));


            filldesignation();
            ddldepartdesg.SelectedIndex = ddldepartdesg.Items.IndexOf(ddldepartdesg.Items.FindByValue(dt.Rows[0]["DesignationId"].ToString()));

            filljobfuncat();
            ddljobfuncat.SelectedIndex = ddljobfuncat.Items.IndexOf(ddljobfuncat.Items.FindByValue(dt.Rows[0]["JobfunctioncategoryId"].ToString()));

            filljobfunsubcat();
            ddljobfunsubcat.SelectedIndex = ddljobfunsubcat.Items.IndexOf(ddljobfunsubcat.Items.FindByValue(dt.Rows[0]["JobfunctionsubcategoryId"].ToString()));


            txtjobfuntitle.Text = dt.Rows[0]["Jobfunctiontitle"].ToString();
            txtjobdesc.Text = dt.Rows[0]["Jobfunctiondescription"].ToString();
            chkstatus.Checked = Convert.ToBoolean(dt.Rows[0]["Status"]);

            ddlbusiness.Focus();
        }
        if (e.CommandName == "View")
        {
            int i = Convert.ToInt32(e.CommandArgument);

            lblId.Text = i.ToString();
            // int dk = Convert.ToInt32(e.CommandArgument);
            string te = "Jobfunctionprofile.aspx?Id=" + i;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }

        Session["ID11"] = lblId.Text;
        //fillgrid();
        fillgriddata();
    }

    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        btnsubmit.Visible = false;
        btnupdate.Visible = true;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        clsjobfunction objjobfunction = new clsjobfunction();

        objjobfunction.Id = Convert.ToInt32(lblId.Text);
        objjobfunction.Whid = Convert.ToInt32(ddlbusiness.SelectedValue);
        objjobfunction.DesignationId = Convert.ToInt32(ddldepartdesg.SelectedValue);
        objjobfunction.JobfunctioncategoryId = Convert.ToInt32(ddljobfuncat.SelectedValue);
        objjobfunction.JobfunctionsubcategoryId = Convert.ToInt32(ddljobfunsubcat.SelectedValue);
        objjobfunction.Jobfunctiontitle = txtjobfuntitle.Text;
        objjobfunction.Jobfunctiondescription = txtjobdesc.Text;
        objjobfunction.Status = Convert.ToInt32(chkstatus.Checked);
        objjobfunction.executeinsertqueryupdate();



        lblId.Text = "";

        //fillgrid();
        fillgriddata();
        clear();
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
        lblmessage.Visible = true;
        lblmessage.Text = "Record updated successfully.";
        lblmessage.Visible = true;

        ddljobfuncat.Items.Clear();
        filljobfuncat();

        ddljobfunsubcat.Items.Clear();
        filljobfunsubcat();
        lbllegend.Text = "";

        Panel1.Visible = false;
        Button1.Visible = true;

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Visible == true)
        {
            clear();
        }
        if (btnupdate.Visible == true)
        {
            btnsubmit.Visible = true;
            btnupdate.Visible = false;
            clear();
        }
        lblmessage.Visible = false;
        fillddl_business();
        test();
        filldesignation();
        filljobfuncat();
        filljobfunsubcat();

        Panel1.Visible = false;
        Button1.Visible = true;
        lbllegend.Text = "";
    }
    protected void ImageButton49_Click1(object sender, ImageClickEventArgs e)
    {
        filljobfuncat();
    }
    protected void ImageButton51_Click(object sender, ImageClickEventArgs e)
    {
        filljobfunsubcat();
    }
    protected void ImageButton48_Click1(object sender, ImageClickEventArgs e)
    {
        string te = "JobFunctionCategory.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton50_Click(object sender, ImageClickEventArgs e)
    {
        string te = "JobFunctionSubCategory.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ddldepartdesg_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnPrintVersion_Click(object sender, EventArgs e)
    {
        if (btnPrintVersion.Text == "Printable Version")
        {

            btnPrintVersion.Text = "Hide Printable Version";
            btnPrint.Visible = true;
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[7].Visible = false;
            }
            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["viewHide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }

        }
        else
        {

            btnPrintVersion.Text = "Printable Version";
            btnPrint.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[8].Visible = true;
            }
            if (ViewState["viewHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
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


    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        //fillgrid();
        fillgriddata();
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //fillgrid();
        fillgriddata();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Button1.Visible = false;
        lblmessage.Visible = false;
        lbllegend.Text = "Add New Job Function";

        fillddl_business();
        test();
        filldesignation();
        filljobfuncat();
        filljobfunsubcat();
    }


    protected void fillwarehouse1()
    {
        string str1 = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["Comid"].ToString() + "' and WareHouseMaster.Status='1' and EmployeeWarehouseRights.AccessAllowed='True' order by name";

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            ddlbusiness1.DataSource = ds1;
            ddlbusiness1.DataTextField = "Name";
            ddlbusiness1.DataValueField = "WarehouseId";
            ddlbusiness1.DataBind();
        }
        ddlbusiness1.Items.Insert(0, "All");
        ddlbusiness1.SelectedItem.Value = "0";
    }

    protected void filldeprdesg1()
    {
        ddldeprdesg1.Items.Clear();

        string str1 = "select DesignationMaster.DesignationMasterId,DepartmentmasterMNC.Departmentname + ':'+DesignationMaster.DesignationName as name FROM DepartmentmasterMNC INNER JOIN DesignationMaster ON DesignationMaster.DeptID = DepartmentmasterMNC.id where Companyid='" + Session["Comid"].ToString() + "' and Whid='" + ddlbusiness1.SelectedValue + "' ORDER BY DepartmentmasterMNC.Departmentname,DesignationMaster.DesignationName ";

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            ddldeprdesg1.DataSource = ds1;
            ddldeprdesg1.DataTextField = "name";
            ddldeprdesg1.DataValueField = "DesignationMasterId";
            ddldeprdesg1.DataBind();
        }
        ddldeprdesg1.Items.Insert(0, "All");
        ddldeprdesg1.SelectedItem.Value = "0";
    }
    protected void filljobfun1()
    {
        ddljobfuncat1.Items.Clear();

        string str1 = "SELECT * from JobFunctionCategory where Whid='" + ddlbusiness1.SelectedValue + "' and Status='1' ";

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            ddljobfuncat1.DataSource = ds1;
            ddljobfuncat1.DataTextField = "CategoryName";
            ddljobfuncat1.DataValueField = "Id";
            ddljobfuncat1.DataBind();
        }
        ddljobfuncat1.Items.Insert(0, "All");
        ddljobfuncat1.SelectedItem.Value = "0";
    }

    protected void filljobfunsub1()
    {
        ddljobfunsubcat1.Items.Clear();

        string str1 = "SELECT * from JobFunctionSubCategory where JobCategoryId='" + ddljobfuncat1.SelectedValue + "' and Status='1' ";

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            ddljobfunsubcat1.DataSource = ds1;
            ddljobfunsubcat1.DataTextField = "SubCategoryName";
            ddljobfunsubcat1.DataValueField = "Id";
            ddljobfunsubcat1.DataBind();
        }
        ddljobfunsubcat1.Items.Insert(0, "All");
        ddljobfunsubcat1.SelectedItem.Value = "0";
    }





    protected void fillgriddata()
    {
        Label1.Text = "All";

        string str = "select dbo.Jobfunction.Id,dbo.WareHouseMaster.Name, (dbo.DepartmentmasterMNC.Departmentname +' : '+ dbo.DesignationMaster.DesignationName) as DeprDesg,dbo.JobFunctionCategory.CategoryName,dbo.JobFunctionSubCategory.SubCategoryName,dbo.Jobfunction.Jobfunctiontitle,dbo.Jobfunction.Jobfunctiondescription,case when(JobFunction.Status = '1') then 'Active' else 'Inactive' end as Status  from dbo.Jobfunction inner join dbo.WareHouseMaster on	dbo.Jobfunction.Whid=dbo.WareHouseMaster.WareHouseId inner join	dbo.DesignationMaster on dbo.Jobfunction.DesignationId=dbo.DesignationMaster.DesignationMasterId inner join dbo.DepartmentmasterMNC on dbo.DesignationMaster.DeptID=dbo.DepartmentmasterMNC.id inner join dbo.JobFunctionCategory on dbo.Jobfunction.JobfunctioncategoryId=dbo.JobFunctionCategory.Id inner join dbo.JobFunctionSubCategory on dbo.Jobfunction.JobfunctionsubcategoryId=dbo.JobFunctionSubCategory.Id where WareHouseMaster.comid='" + Session["Comid"] + "'";

        if (ddlbusiness1.SelectedIndex > 0)
        {
            Label1.Text = ddlbusiness1.SelectedItem.Text;

            str += " and Jobfunction.Whid='" + ddlbusiness1.SelectedValue + "' ";
        }
        if (ddldeprdesg1.SelectedIndex > 0)
        {
            str += " and DesignationMaster.DesignationMasterId='" + ddldeprdesg1.SelectedValue + "' ";

        }
        if (ddljobfuncat1.SelectedIndex > 0)
        {
            str += " and JobFunctionCategory.Id='" + ddljobfuncat1.SelectedValue + "' and JobFunctionCategory.Status='1' ";

        }
        if (ddljobfunsubcat1.SelectedIndex > 0)
        {
            str += " and JobFunctionSubCategory.Id='" + ddljobfunsubcat1.SelectedValue + "' and JobFunctionSubCategory.Status='1' ";
        }
        if (ddlstatus_search.SelectedIndex > 0)
        {
            str += " and Jobfunction.Status='" + ddlstatus_search.SelectedValue + "' ";
        }

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);


        if (ds.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataSource = myDataView;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }

    protected void ddlbusiness1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbusiness1.SelectedIndex == 0)
        {
            ddldeprdesg1.Items.Clear();
            ddljobfuncat1.Items.Clear();
            ddljobfunsubcat1.Items.Clear();
            ddljobfunsubcat1.Items.Insert(0, "All");
        }

        filldeprdesg1();
        filljobfun1();
        fillgriddata();

    }
    protected void ddldeprdesg1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddldeprdesg1.SelectedIndex == 0 && ddlbusiness1.SelectedIndex > 0)
        //{
        //    ddljobfuncat1.Items.Clear();
        //    ddljobfuncat1.Items.Insert(0, "ALL");
        //    ddljobfuncat1.SelectedIndex = 0;
        //    ddljobfunsubcat1.Items.Clear();
        //    ddljobfunsubcat1.Items.Insert(0, "ALL");
        //    ddljobfunsubcat1.SelectedIndex = 0;
        //    da = new SqlDataAdapter("select  dbo.Jobfunction.Id,dbo.WareHouseMaster.Name, (dbo.DepartmentmasterMNC.Departmentname +' : '+ dbo.DesignationMaster.DesignationName) as DeprDesg,dbo.JobFunctionCategory.CategoryName,dbo.JobFunctionSubCategory.SubCategoryName,dbo.Jobfunction.Jobfunctiontitle,dbo.Jobfunction.Jobfunctiondescription,dbo.Jobfunction.Status from dbo.Jobfunction inner join dbo.WareHouseMaster on	dbo.Jobfunction.Whid=dbo.WareHouseMaster.WareHouseId inner join	dbo.DesignationMaster on dbo.Jobfunction.DesignationId=dbo.DesignationMaster.DesignationMasterId inner join dbo.DepartmentmasterMNC on dbo.DesignationMaster.DeptID=dbo.DepartmentmasterMNC.id inner join dbo.JobFunctionCategory on dbo.Jobfunction.JobfunctioncategoryId=dbo.JobFunctionCategory.Id inner join dbo.JobFunctionSubCategory on dbo.Jobfunction.JobfunctionsubcategoryId=dbo.JobFunctionSubCategory.Id where Jobfunction.Whid='" + ddlbusiness1.SelectedValue + "' ", con);
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //}

        fillgriddata();
    }
    protected void ddljobfuncat1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddljobfuncat1.SelectedIndex == 0)
        {
            ddljobfunsubcat1.Items.Clear();
            //   ddljobfunsubcat1.Items.Insert(0,"ALL");
            //  ddljobfunsubcat1.SelectedIndex = 0;
        }

        filljobfunsub1();
        //if (ddljobfuncat1.SelectedIndex == 0 && ddlbusiness1.SelectedIndex > 0 && ddldeprdesg1.SelectedIndex > 0)
        //{
        //    ddljobfunsubcat1.Items.Clear();
        //    ddljobfunsubcat1.Items.Insert(0, "ALL");
        //    ddljobfunsubcat1.SelectedIndex = 0;
        //    da = new SqlDataAdapter("select  dbo.Jobfunction.Id,dbo.WareHouseMaster.Name, (dbo.DepartmentmasterMNC.Departmentname +' : '+ dbo.DesignationMaster.DesignationName) as DeprDesg,dbo.JobFunctionCategory.CategoryName,dbo.JobFunctionSubCategory.SubCategoryName,dbo.Jobfunction.Jobfunctiontitle,dbo.Jobfunction.Jobfunctiondescription,dbo.Jobfunction.Status from dbo.Jobfunction inner join dbo.WareHouseMaster on	dbo.Jobfunction.Whid=dbo.WareHouseMaster.WareHouseId inner join	dbo.DesignationMaster on dbo.Jobfunction.DesignationId=dbo.DesignationMaster.DesignationMasterId inner join dbo.DepartmentmasterMNC on dbo.DesignationMaster.DeptID=dbo.DepartmentmasterMNC.id inner join dbo.JobFunctionCategory on dbo.Jobfunction.JobfunctioncategoryId=dbo.JobFunctionCategory.Id inner join dbo.JobFunctionSubCategory on dbo.Jobfunction.JobfunctionsubcategoryId=dbo.JobFunctionSubCategory.Id where Jobfunction.Whid='" + ddlbusiness1.SelectedValue + "' and DesignationMaster.DesignationMasterId='" + ddldeprdesg1.SelectedValue + "'", con);
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //}
        fillgriddata();

        //else if(ddljobfuncat1.SelectedIndex > 0 && ddlbusiness1.SelectedIndex > 0 && ddldeprdesg1.SelectedIndex > 0)
        //{
        //fillgriddata();
        //}
    }
    protected void ddljobfunsubcat1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
}
