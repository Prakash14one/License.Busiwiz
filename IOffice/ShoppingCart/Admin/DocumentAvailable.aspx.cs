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

public partial class DocumentAvailable : System.Web.UI.Page
{
    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    DataTable dt = new DataTable();
    protected int DesignationId;
    EmployeeCls clsEmployee = new EmployeeCls();
    protected void Page_Load(object sender, EventArgs e)
    {

       
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
            pagetitleclass pg = new pagetitleclass();
            string strData = Request.Url.ToString();

            char[] separator = new char[] { '/' };

            string[] strSplitArr = strData.Split(separator);
            int i = Convert.ToInt32(strSplitArr.Length);
            string page = strSplitArr[i - 1].ToString();
			Session["PageUrl"]=strData;
            Session["PageName"] = page;
            Page.Title = pg.getPageTitle(page);

            if (Session["CompanyName"] != null)
            {
                this.Title = Session["CompanyName"] + " IFileCabinet.com - Document Available ";
            }

            Session["PageName"] = "DocumentAvailable.aspx";

            if (!IsPostBack)
            {
                Pagecontrol.dypcontrol(Page, page);
             
                lblCompany.Text = Session["Cname"].ToString();
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
                string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
                SqlCommand cmdeeed = new SqlCommand(eeed, con);
                SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                DataTable dteeed = new DataTable();
                adpeeed.Fill(dteeed);
                if (dteeed.Rows.Count > 0)
                {
                    ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

                }
                ViewState["sortOrder"] = "";

                ddlbusiness_SelectedIndexChanged(sender, e);

            }
            
    }
    protected void Fillddl_doc_appvd_by_deo()
    {
        dt = clsDocument.SelectDEO(ddlbusiness.SelectedValue);
        ddl_doc_appvd_by_deo.DataSource = dt;
        ddl_doc_appvd_by_deo.DataBind();
        ddl_doc_appvd_by_deo.Items.Insert(0, "-Select-");
        ddl_doc_appvd_by_deo.SelectedItem.Value = "0";
    }
    protected void Fillddl_doc_not_appvd_by_sup()
    {
        dt = clsDocument.SelectSupervisor(ddlbusiness.SelectedValue);
        ddl_doc_not_appvd_by_sup.DataSource = dt;
        ddl_doc_not_appvd_by_sup.DataBind();
        ddl_doc_not_appvd_by_sup.Items.Insert(0, "-Select-");
        ddl_doc_not_appvd_by_sup.SelectedItem.Value = "0";
    }   
    
  
    protected void SelectDocument()
    {
       try
        {
            if (ddl_doc_appvd_by_deo.SelectedIndex == 0 && ddl_doc_not_appvd_by_sup.SelectedIndex == 0 )
            {
                lblBusiness.Text = ddlbusiness.SelectedItem.Text;
                lblDEO.Text = ddl_doc_appvd_by_deo.SelectedItem.Text;
                lblSuper.Text = ddl_doc_not_appvd_by_sup.SelectedItem.Text;
                lblOS.Text = ddldeostatus.SelectedItem.Text;
                lblSS.Text = ddlsupstatus.SelectedItem.Text;
                DataTable dt1;
                dt1 = new DataTable();
                Int32 deo = Convert.ToInt32(ddl_doc_appvd_by_deo.SelectedValue);
                if (ddldeostatus.SelectedIndex > 0 && ddlsupstatus.SelectedIndex > 0)
                {
                    dt1 = clsDocument.SelectDocumentAvailableApprovebyAllDEO_NotbyAllSup(ddlbusiness.SelectedValue, Convert.ToBoolean(ddldeostatus.SelectedValue), Convert.ToBoolean(ddlsupstatus.SelectedValue));
                }
                else
                {
                    dt1 = clsDocument.SelectDocumentAvailableApprovebyAllDEO_NotbyAllSup(ddlbusiness.SelectedValue,false, false);
                }
                DataView myDataView = new DataView();
                myDataView = dt1.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }
                grid_doc_available.DataSource = myDataView;
                grid_doc_available.DataBind();
            }
            else if (ddl_doc_appvd_by_deo.SelectedIndex > 0 && ddl_doc_not_appvd_by_sup.SelectedIndex <= 0 && ddldeostatus.SelectedIndex > 0)
            {
                lblBusiness.Text = ddlbusiness.SelectedItem.Text;
                lblDEO.Text = ddl_doc_appvd_by_deo.SelectedItem.Text;
                lblSuper.Text = ddl_doc_not_appvd_by_sup.SelectedItem.Text;
                lblOS.Text = ddldeostatus.SelectedItem.Text;
                lblSS.Text = ddlsupstatus.SelectedItem.Text;
                DataTable dt1 ;
                 Int32 deo = Convert.ToInt32(ddl_doc_appvd_by_deo.SelectedValue);
                 dt1 = clsDocument.SelectDocumentAvailableApprovebyDEO_NotbyAllSup(deo,Convert.ToBoolean(ddldeostatus.SelectedValue));
                 DataView myDataView = new DataView();
                 myDataView = dt1.DefaultView;

                 if (hdnsortExp.Value != string.Empty)
                 {
                     myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                 }
                 grid_doc_available.DataSource = myDataView;
                 grid_doc_available.DataBind();
            }
            else if (ddl_doc_appvd_by_deo.SelectedIndex <= 0 && ddl_doc_not_appvd_by_sup.SelectedIndex > 0)
            {
                lblBusiness.Text = ddlbusiness.SelectedItem.Text;
                lblDEO.Text = ddl_doc_appvd_by_deo.SelectedItem.Text;
                lblSuper.Text = ddl_doc_not_appvd_by_sup.SelectedItem.Text;
                lblOS.Text = ddldeostatus.SelectedItem.Text;
                lblSS.Text = ddlsupstatus.SelectedItem.Text;
               
                 DataTable dt1 ;
                 Int32 Sup = Convert.ToInt32(ddl_doc_not_appvd_by_sup.SelectedValue);
                 dt1 = clsDocument.SelectDocumentAvailableApprovebyAllDEO_NotbySup(Sup, ddlbusiness.SelectedValue,Convert.ToBoolean( ddlsupstatus.SelectedValue));
                 DataView myDataView = new DataView();
                 myDataView = dt1.DefaultView;

                 if (hdnsortExp.Value != string.Empty)
                 {
                     myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                 }
                 grid_doc_available.DataSource = myDataView;
                 grid_doc_available.DataBind();
            }
            //else if (ddl_doc_appvd_by_deo.SelectedIndex <= 0 && ddl_doc_not_appvd_by_sup.SelectedIndex > 0 && ddlsupstatus.SelectedIndex > 0)
            //{
            //    DataTable dt1;
            //    Int32 Sup = Convert.ToInt32(ddl_doc_not_appvd_by_sup.SelectedValue);
            //    dt1 = clsDocument.SelectDocumentAvailableApprovebyAllDEO_NotbySup_supstatus(Sup, ddlbusiness.SelectedValue, Convert.ToBoolean(ddlsupstatus.SelectedValue));
            //    grid_doc_available.DataSource = dt1;
            //    grid_doc_available.DataBind();
            //}
            else if (ddl_doc_appvd_by_deo.SelectedIndex > 0 && ddl_doc_not_appvd_by_sup.SelectedIndex > 0 && ddldeostatus.SelectedIndex>0 && ddlsupstatus.SelectedIndex>0)
            {
                lblBusiness.Text = ddlbusiness.SelectedItem.Text;
                lblDEO.Text = ddl_doc_appvd_by_deo.SelectedItem.Text;
                lblSuper.Text = ddl_doc_not_appvd_by_sup.SelectedItem.Text;
                lblOS.Text = ddldeostatus.SelectedItem.Text;
                lblSS.Text = ddlsupstatus.SelectedItem.Text;
                DataTable dt1;
                Int32 Sup = Convert.ToInt32(ddl_doc_not_appvd_by_sup.SelectedValue);
                Int32 deo = Convert.ToInt32(ddl_doc_appvd_by_deo.SelectedValue);
                dt1 = clsDocument.SelectDocumentAvailableApprovebyDEO_NotbySup(deo, Sup, Convert.ToBoolean(ddldeostatus.SelectedValue), Convert.ToBoolean(ddlsupstatus.SelectedValue), ddlbusiness.SelectedValue);
                DataView myDataView = new DataView();
                myDataView = dt1.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }
                grid_doc_available.DataSource = myDataView;
                grid_doc_available.DataBind();
            }
            
            }      
        catch (Exception exx)
        {            
            lblmsg.Text = "errror: " + exx.Message;
        }      
    }    
    public void setGridisize()
    {
        // doc grid
        if (grid_doc_available.Rows.Count == 0)
        {
            pnlgrid.CssClass = "Gridpnlgrid0";
        }
        else if (grid_doc_available.Rows.Count == 1)
        {
            pnlgrid.CssClass = "GridPanel125";
        }
        else if (grid_doc_available.Rows.Count == 2)
        {
            pnlgrid.CssClass = "GridPanel150";
        }
        else if (grid_doc_available.Rows.Count == 3)
        {
            pnlgrid.CssClass = "GridPanel175";
        }
        else if (grid_doc_available.Rows.Count == 4)
        {
            pnlgrid.CssClass = "Gridpnlgrid00";
        }
        else if (grid_doc_available.Rows.Count == 5)
        {
            pnlgrid.CssClass = "Gridpnlgrid25";
        }
        else if (grid_doc_available.Rows.Count == 6)
        {
            pnlgrid.CssClass = "Gridpnlgrid50";
        }
        else if (grid_doc_available.Rows.Count == 7)
        {
            pnlgrid.CssClass = "Gridpnlgrid75";
        }
        else if (grid_doc_available.Rows.Count == 8)
        {
            pnlgrid.CssClass = "GridPanel";
        }
        else if (grid_doc_available.Rows.Count == 9)
        {
            pnlgrid.CssClass = "GridPanel325";
        }
        else if (grid_doc_available.Rows.Count == 10)
        {
            pnlgrid.CssClass = "GridPanel350";
        }

        else
        {
            pnlgrid.CssClass = "GridPanel375";
        }
       


    }
    protected void imgbtngo_Click(object sender, EventArgs e)
    {
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
    protected void ddl_doc_not_appvd_by_sup_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SelectDocument();
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
        Fillddl_doc_appvd_by_deo();
        Fillddl_doc_not_appvd_by_sup();
    }
    protected void ddldeostatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnprintableversion_Click(object sender, EventArgs e)
    {
        if (btnprintableversion.Text == "Printable Version")
        {
            btnprintableversion.Text = "Hide Printable Version";
            Button7.Visible = true;
           
        }
        else
        {
            btnprintableversion.Text = "Printable Version";
            Button7.Visible = false;
           
        }
    }

}

    
  