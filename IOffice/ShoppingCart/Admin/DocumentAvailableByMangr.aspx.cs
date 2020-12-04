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
using System.Data.SqlClient;

public partial class DocumentAvailableByMangr : System.Web.UI.Page
{
    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    DataTable dt = new DataTable();
    protected int DesignationId;
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
        if (Session["CompanyName"] != null)
        {
            this.Title = Session["CompanyName"] + " IFileCabinet.com - Available Documents : Manager";
        }

        Session["PageName"] = "DocumentAvailableByMangr.aspx";
        if (Session["EmployeeId"] != null)
        {
            if (!IsPostBack)
            {
                Pagecontrol.dypcontrol(Page, page);

                ViewState["EmployeeId"] = Session["EmployeeId"];
                string str = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";

                SqlCommand cmd1 = new SqlCommand(str, con);
                cmd1.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlbusiness.DataSource = dt;
                ddlbusiness.DataTextField = "Name";
                ddlbusiness.DataValueField = "WareHouseId";
                ddlbusiness.DataBind();
                ViewState["sortOrder"] = "";
                string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
                SqlCommand cmdeeed = new SqlCommand(eeed, con);
                SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                DataTable dteeed = new DataTable();
                adpeeed.Fill(dteeed);
                if (dteeed.Rows.Count > 0)
                {
                    ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

                }
                Fillddl_doc_appvd_by_deo();
                Fillddl_doc_appvd_by_sup();
                //  fillemp();
                SelectDocument();


            }
        }
        else
        {
            Response.Redirect("Shoppingcartlogin.aspx");
        }

    }
    protected void fillemp()
    {
        //string eeed = " Select distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName,Whid from EmployeeMaster where EmployeeMaster.EmployeeMasterID ='" + Session["EmployeeId"] + "'";
        //SqlCommand cmdeeed = new SqlCommand(eeed, con);
        //SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        //DataTable dteeed = new DataTable();
        //adpeeed.Fill(dteeed);
        //if (dteeed.Rows.Count > 0)
        //{
        //    if (ddlbusiness.SelectedValue != Convert.ToString(dteeed.Rows[0]["Whid"]))
        //    {
        //        ddlemp.DataSource = dteeed;
        //        ddlemp.DataTextField = "EmployeeName";
        //        ddlemp.DataValueField = "EmployeeMasterID";
        //        ddlemp.DataBind();

        //        pnlemp.Visible = true;
        //        ViewState["EmployeeId"] = ddlemp.SelectedValue;
        //    }
        //    else
        //    {
        //        pnlemp.Visible = false;
        //        ViewState["EmployeeId"] = Session["EmployeeId"];
        //    }
        //}
    }
    protected void Fillddl_doc_appvd_by_deo()
    {
        // dt = clsDocument.SelectDEO(ddlbusiness.SelectedValue);
        string eeed = "SELECT EmployeeMaster.EmployeeMasterID as EmployeeID,DesignationMaster.DesignationName+'-'+EmployeeMaster.EmployeeName as EmployeeName FROM         DesignationMaster inner join DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id inner join EmployeeMaster ON EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId WHERE      DepartmentmasterMNC.Companyid='" + Session["Comid"] + "' and DepartmentmasterMNC.Whid='" + ddlbusiness.SelectedValue + "' and (DepartmentmasterMNC.DepartmentName='Filling Desk') and (DesignationMaster.DesignationName in('Office Clerk') ) order by EmployeeName desc";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        ddl_doc_appvd_by_deo.DataSource = dteeed;
        ddl_doc_appvd_by_deo.DataBind();
        ddl_doc_appvd_by_deo.Items.Insert(0, "-All-");
        ddl_doc_appvd_by_deo.SelectedItem.Value = "0";
    }
    protected void Fillddl_doc_appvd_by_sup()
    {
        // dt = clsDocument.SelectSupervisor(ddlbusiness.SelectedValue);
        string eeed = "SELECT EmployeeMaster.EmployeeMasterID as EmployeeID,DesignationMaster.DesignationName+'-'+EmployeeMaster.EmployeeName as EmployeeName FROM         DesignationMaster inner join DepartmentmasterMNC ON DesignationMaster.DeptID = DepartmentmasterMNC.id inner join EmployeeMaster ON EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId WHERE      DepartmentmasterMNC.Companyid='" + Session["Comid"] + "' and DepartmentmasterMNC.Whid='" + ddlbusiness.SelectedValue + "' and (DepartmentmasterMNC.DepartmentName='Filling Desk') and (DesignationMaster.DesignationName in('Supervisor') ) order by EmployeeName desc";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        ddl_doc_appvd_by_sup.DataSource = dteeed;
        ddl_doc_appvd_by_sup.DataBind();
        ddl_doc_appvd_by_sup.Items.Insert(0, "-All-");
        ddl_doc_appvd_by_sup.SelectedItem.Value = "0";

    }
    protected void SelectDocumentByDEO()
    {
        Int32 deo;
        deo = Convert.ToInt32(ddl_doc_appvd_by_deo.SelectedValue);
        dt = clsDocument.SelectDocumentAvailableApprovebyDEO(deo);
        grid_doc_available.DataSource = dt;
        grid_doc_available.DataBind();
        setGridisize();
    }
    protected void SelectDocumentAvailableApproveBySupervisor()
    {
        Int32 supr;
        supr = Convert.ToInt32(ddl_doc_appvd_by_sup.SelectedValue);
        dt = clsDocument.SelectDocumentAvailableApproveBySupervisor(supr);
        grid_doc_available.DataSource = dt;
        grid_doc_available.DataBind();
        setGridisize();
    }
    protected void SelectDocumentAvailableNOTapproveByMng()
    {
        Int32 mng;
        if (Session["EmployeeId"].ToString() != null)
        {
            mng = Convert.ToInt32(Session["EmployeeId"]);
            dt = clsDocument.SelectDocumentAvailableNOTapproveByMng(mng);
            grid_doc_available.DataSource = dt;
            grid_doc_available.DataBind();
            setGridisize();
        }
    }


    protected void SelectDocument()
    {
        DataTable dt1 = new DataTable();
        try
        {
            if (ddl_doc_appvd_by_deo.SelectedIndex == 0 && ddl_doc_appvd_by_sup.SelectedIndex == 0)
            {
                //DataTable dt1;
                //dt1 = new DataTable();
                Int32 deo = Convert.ToInt32(ddl_doc_appvd_by_deo.SelectedValue);
                dt1 = clsDocument.SelectDocumentAvailableApprovebyAllDEO_AllSup_NotByMe(ViewState["EmployeeId"].ToString());
                DataView myDataView = new DataView();
                myDataView = dt1.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }

                //grid_doc_available.DataSource = dt1;
                //grid_doc_available.DataBind();
            }
            else if (ddl_doc_appvd_by_deo.SelectedIndex > 0 && ddl_doc_appvd_by_sup.SelectedIndex <= 0)
            {
                //DataTable dt1 ;
                Int32 deo = Convert.ToInt32(ddl_doc_appvd_by_deo.SelectedValue);
                dt1 = clsDocument.SelectDocumentAvailableApprovebyDEO_AllSup_NotByMe(deo, ViewState["EmployeeId"].ToString());
                DataView myDataView = new DataView();
                myDataView = dt1.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }

                //grid_doc_available.DataSource = dt1;
                //grid_doc_available.DataBind();
            }
            else if (ddl_doc_appvd_by_deo.SelectedIndex <= 0 && ddl_doc_appvd_by_sup.SelectedIndex > 0)
            {
                //DataTable dt1 ;1
                Int32 Sup = Convert.ToInt32(ddl_doc_appvd_by_sup.SelectedValue);
                dt1 = clsDocument.SelectDocumentAvailableApprovebyAllDEO_Sup_NotByMe(Sup, ViewState["EmployeeId"].ToString());
                DataView myDataView = new DataView();
                myDataView = dt1.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }

                //grid_doc_available.DataSource = dt1;
                //grid_doc_available.DataBind(); 
            }
            else if (ddl_doc_appvd_by_deo.SelectedIndex > 0 && ddl_doc_appvd_by_sup.SelectedIndex > 0)
            {
                //DataTable dt1;
                Int32 Sup = Convert.ToInt32(ddl_doc_appvd_by_sup.SelectedValue);
                Int32 deo = Convert.ToInt32(ddl_doc_appvd_by_deo.SelectedValue);
                dt1 = clsDocument.SelectDocumentAvailableApprovebyDEO_Sup_NotByMe(deo, Sup, ViewState["EmployeeId"].ToString());
                DataView myDataView = new DataView();
                myDataView = dt1.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }


                //grid_doc_available.DataSource = dt1;
                // grid_doc_available.DataBind();
            }
            grid_doc_available.DataSource = dt1;
            grid_doc_available.DataBind();
        }
        catch (Exception exx)
        {
            lblmsg.Text = "errror: " + exx.Message;
        }
    }


    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {

        DocumentApprove();
    }

    protected void DocumentApprove()
    {
        int i;
        i = 0;
        Int32 DocProcId;
        CheckBox chk;
        DropDownList Rbtn;
        bool success;
        success = false;

        if (grid_doc_available.Rows.Count > 0)
        {
            do
            {
                //  chk = (CheckBox)gridocapproval.Rows[i].FindControl("chkAccept");
                Rbtn = (DropDownList)grid_doc_available.Rows[i].FindControl("rbtnAcceptReject");
                if (Rbtn.SelectedValue.ToString() != "None")
                {
                    DocProcId = Int32.Parse(grid_doc_available.DataKeys[i].Value.ToString());
                    TextBox Note = (TextBox)grid_doc_available.Rows[i].FindControl("txtnote");
                    success = clsDocument.InsertDocumentApprove(DocProcId, Convert.ToBoolean(Rbtn.SelectedValue), Note.Text);
                    success = true;
                }
                i = i + 1;
            }

            while (i <= grid_doc_available.Rows.Count - 1);
        }
        if (success == true)
        {
            lblmsg.Text = "Document Approved Successfully.";
            lblmsg.Visible = true;
            SelectDocument();
        }
    }



    protected void imgbtngo_Click(object sender, EventArgs e)
    {
        lblcompany.Text = Session["Cname"].ToString();
        lblcomname.Text = ddlbusiness.SelectedItem.Text;
        SelectDocument();
    }

    protected void grid_doc_available_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        grid_doc_available.PageIndex = e.NewPageIndex;
        SelectDocument();
    }

    protected void ddl_doc_appvd_by_deo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SelectDocument();

    }
    protected void ddl_doc_appvd_by_sup_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  SelectDocument();
    }
    public void setGridisize()
    {
        // doc grid
        if (grid_doc_available.Rows.Count == 0)
        {
            Panel2.CssClass = "GridPanel20";
        }
        else if (grid_doc_available.Rows.Count == 1)
        {
            Panel2.CssClass = "GridPanel125";
        }
        else if (grid_doc_available.Rows.Count == 2)
        {
            Panel2.CssClass = "GridPanel150";
        }
        else if (grid_doc_available.Rows.Count == 3)
        {
            Panel2.CssClass = "GridPanel175";
        }
        else if (grid_doc_available.Rows.Count == 4)
        {
            Panel2.CssClass = "GridPanel200";
        }
        else if (grid_doc_available.Rows.Count == 5)
        {
            Panel2.CssClass = "GridPanel225";
        }
        else if (grid_doc_available.Rows.Count == 6)
        {
            Panel2.CssClass = "GridPanel250";
        }
        else if (grid_doc_available.Rows.Count == 7)
        {
            Panel2.CssClass = "GridPanel275";
        }
        else if (grid_doc_available.Rows.Count == 8)
        {
            Panel2.CssClass = "GridPanel";
        }
        else if (grid_doc_available.Rows.Count == 9)
        {
            Panel2.CssClass = "GridPanel325";
        }
        else if (grid_doc_available.Rows.Count == 10)
        {
            Panel2.CssClass = "GridPanel350";
        }

        else
        {
            Panel2.CssClass = "GridPanel375";
        }




    }
    protected void grid_doc_available_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        SelectDocument();
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
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillemp();
        Fillddl_doc_appvd_by_deo();
        Fillddl_doc_appvd_by_sup();
        SelectDocument();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
        }
        else
        {

            pnlgrid.ScrollBars = ScrollBars.Vertical;
            pnlgrid.Height = new Unit(400);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
        }
    }
}


