using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class Add_Designation : System.Web.UI.Page
{
    SqlConnection con;
    string compid;
    SqlConnection conlicense = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
      

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        compid = Session["comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);

            ViewState["sortOrder"] = "";
            DataTable ds = ClsStore.SelectStorename();

            ddlstore.DataSource = ds;
            ddlstore.DataValueField = "WareHouseId";
            ddlstore.DataTextField = "Name";
            ddlstore.DataBind();

            DropDownList3.DataSource = ds;
            DropDownList3.DataValueField = "WareHouseId";
            DropDownList3.DataTextField = "Name";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, "All");
            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                DropDownList3.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }

            filllicense_role();
            DDLlicenseroleFilter();

            fillddldepartment();
            fillparentdesg();
            fillrole();
            fillddl1();
            fillgrid();


        }
    }


    protected void filllicense_role()
    {
        string emprole = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + compid + "' and ActiveDeactive='1' and Role_name<>'Customer' and Role_name<>'Vendor'  order by Role_name";
        SqlCommand cmdrole = new SqlCommand(emprole, PageConn.licenseconn());
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();

        darole.Fill(dtrole);
        DDLLicense_role.DataSource = dtrole;
        DDLLicense_role.DataTextField = "Role_name";
        DDLLicense_role.DataValueField = "Role_id";
        DDLLicense_role.DataBind();
        DDLLicense_role.Items.Insert(0, "--Select--");
        DDLLicense_role.SelectedItem.Value = "0";
    }
    public void fillddldepartment()
    {



        string strfillgrid1 = "SELECT Departmentname as Departmentname,ID from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where Companyid='" + compid + "' and DepartmentmasterMNC.Active='1'  and  DepartmentmasterMNC.Whid='" + ddlstore.SelectedValue + "' and  [WareHouseMaster].status = '1' order by Departmentname";
        SqlCommand cmdfillgrid1 = new SqlCommand(strfillgrid1, con);
        SqlDataAdapter adpfillgrid1 = new SqlDataAdapter(cmdfillgrid1);
        DataTable dtfill1 = new DataTable();
        adpfillgrid1.Fill(dtfill1);
        ddldesignation.DataSource = dtfill1;
        ddldesignation.DataValueField = "ID";
        ddldesignation.DataTextField = "Departmentname";
        ddldesignation.DataBind();
        ddldesignation.Items.Insert(0, "--Select--");
        ddldesignation.SelectedItem.Value = "0";
    }

    //-----Filter DDL
    public void fillddl1()
    {
        DropDownList1.Items.Clear();

        if (DropDownList3.SelectedIndex > 0)
        {
            DropDownList2.Items.Clear();
            string strfillgrid = "SELECT  Departmentname as Departmentname,id FROM DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where Companyid='" + compid + "' and DepartmentmasterMNC.Active='1' and DepartmentmasterMNC.Whid='" + DropDownList3.SelectedValue + "' and  [WareHouseMaster].status = '1' order by Departmentname";
            SqlCommand cmdfillgrid = new SqlCommand(strfillgrid, con);
            SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
            DataTable dtfill = new DataTable();
            adpfillgrid.Fill(dtfill);
            DropDownList1.DataSource = dtfill;
            DropDownList1.DataValueField = "id";
            DropDownList1.DataTextField = "Departmentname";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "--All--");
            DropDownList1.SelectedItem.Value = "0";
        }
        else
        {
            DropDownList1.DataSource = null;
            DropDownList1.DataValueField = "id";
            DropDownList1.DataTextField = "Departmentname";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "--All--");
            DropDownList1.SelectedIndex = 0;
        }
    }
    public void DDLlicenseroleFilter()
    {
        ddlLicenseRoleFilter.Items.Clear();
        string emprole = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + compid + "' and ActiveDeactive='1' and Role_name<>'Customer' and Role_name<>'Vendor'  order by Role_name";
        SqlCommand cmdrole = new SqlCommand(emprole, PageConn.licenseconn());
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();
        darole.Fill(dtrole);
        ddlLicenseRoleFilter.DataSource = dtrole;
        ddlLicenseRoleFilter.DataTextField = "Role_name";
        ddlLicenseRoleFilter.DataValueField = "Role_id";
        ddlLicenseRoleFilter.DataBind();
        ddlLicenseRoleFilter.Items.Insert(0, "--Select--");
        ddlLicenseRoleFilter.SelectedItem.Value = "0";
    }
    protected void ddlLicenseRoleFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
       // fillgrid();
    }


    protected void fillparentdesg()
    {
        ddlparentdesg.Items.Clear();

        string strfillgrid = "select [DesignationName],[DesignationMasterId] from [DesignationMaster] where Active='1' and [DeptID]='" + ddldesignation.SelectedValue + "'";
        SqlCommand cmdfillgrid = new SqlCommand(strfillgrid, con);
        SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
        DataTable dtfill = new DataTable();
        adpfillgrid.Fill(dtfill);

        if (dtfill.Rows.Count > 0)
        {
            ddlparentdesg.DataSource = dtfill;
            ddlparentdesg.DataValueField = "DesignationMasterId";
            ddlparentdesg.DataTextField = "DesignationName";
            ddlparentdesg.DataBind();
        }
        else
        {
            string str = "select DesignationMaster.*,DepartmentmasterMNC.Whid from DesignationMaster inner join DepartmentmasterMNC on DesignationMaster.DeptID =  DepartmentmasterMNC.id where DesignationName='Admin' and DepartmentmasterMNC.Whid = '" + ddlstore.SelectedValue + "'";
            SqlDataAdapter adpt = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlparentdesg.DataSource = dt;
                ddlparentdesg.DataValueField = "DesignationMasterId";
                ddlparentdesg.DataTextField = "DesignationName";
                ddlparentdesg.DataBind();
            }
        }
        ddlparentdesg.Items.Insert(0, "--Select--");
        ddlparentdesg.SelectedItem.Value = "0";
    }

    public void fillgrid()
    {
        lblCompany.Text = Session["Cname"].ToString();

        string str1 = "";

        string st2 = "";
        if (ddlactivesearch.SelectedIndex > 0)
        {
            st2 += " and DesignationMaster.Active='" + ddlactivesearch.SelectedValue + "'";
        }
        if (DropDownList3.SelectedIndex > 0)
        {
            st2 += "and DepartmentmasterMNC.Whid='" + DropDownList3.SelectedValue + "'";
        }
        if (DropDownList1.SelectedIndex > 0)
        {
            st2 += "and DepartmentmasterMNC.id='" + DropDownList1.SelectedValue + "'";
        }
        if (ddlLicenseRoleFilter.SelectedIndex > 0)
        {
            st2 += "and DesignationMaster.RoleId='" + ddlLicenseRoleFilter.SelectedValue + "'";
        }

        str1 = " DesignationMaster.DeptID,WareHouseMaster.Name as Wname,WareHouseMaster.WareHouseId,DepartmentmasterMNC.Departmentname as Departmentname ,  DesignationMaster.Active,DepartmentmasterMNC.id,  DesignationMaster.DesignationMasterId, DesignationMaster.DesignationName,RoleMaster.Role_name,DesignationMaster.RoleId From DesignationMaster " +
                "inner join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid left outer join RoleMaster on RoleMaster.Role_id=DesignationMaster.RoleId where DepartmentmasterMNC.Companyid='" + compid + "' and  [WareHouseMaster].status = '1' " + st2 + "";

        string str2 = "select count(DesignationMaster.DesignationMasterId) as ci from DesignationMaster inner join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid left outer join RoleMaster on RoleMaster.Role_id=DesignationMaster.RoleId where DepartmentmasterMNC.Companyid='" + compid + "' and  [WareHouseMaster].status = '1' " + st2 + "";

        //str1 += "Order by WareHouseMaster.Name, Departmentname, DesignationName ";


        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name, Departmentname, DesignationName ";

        lblDepartment.Text = DropDownList1.SelectedItem.Text;
        lblBusines.Text = DropDownList3.SelectedItem.Text;

        //DataTable dt = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(str1, con);
        //da.Fill(dt);

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str1);

            GridView1.DataSource = dt;
            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataBind();
        }
        else
        {
            GridView1.EmptyDataText = "No Record Found.";
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbllicenseroleid = (Label)e.Row.FindControl("lbllicenseroleid");
                Label lbllicenserole = (Label)e.Row.FindControl("lbllicenserole");             
                DataTable dtDeletecom = selectbackupdb(" select Role_id, Role_name,ActiveDeactive from RoleMaster where Role_id='" + lbllicenseroleid.Text + "'");
                if (dtDeletecom.Rows.Count > 0)
                {
                    lbllicenserole.Text = dtDeletecom.Rows[0]["Role_name"].ToString();  
                }
              
            }       

    }
    protected DataTable selectbackupdb(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, conlicense);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
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
        DataTable dt = select(string.Format("SELECT TOP(200)* FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));

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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }
        else if (e.CommandName == "Manage")
        {
            string te = "http://license.busiwiz.com/admin/Page_role_AccessShow.aspx?roleid=" + e.CommandArgument.ToString() + "&clientid=" + Session["ClientId"].ToString(); 
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }



    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        statuslable.Text = "";
        int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        ViewState["editid"] = GridView1.DataKeys[e.NewEditIndex].Value.ToString();

        if (dk1.ToString() != "")
        {
            CheckBox1.Checked = false;
            SqlCommand cmdedit = new SqlCommand("select EditAllowed,DesignationId from Fixeddata where DesignationId='" + dk1 + "'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);
            if (dtedit.Rows.Count > 0)
            {
                if (dtedit.Rows[0]["EditAllowed"].ToString() == "0")
                {
                    string str = "select DesignationMaster.*,DepartmentmasterMNC.Whid from DesignationMaster inner join DepartmentmasterMNC on DesignationMaster.DeptID =  DepartmentmasterMNC.id where DesignationMaster.DesignationMasterId = '" + ViewState["editid"] + "'";
                    SqlDataAdapter adpt = new SqlDataAdapter(str, con);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
                        ddlstore_SelectedIndexChanged(sender, e);
                        ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(dt.Rows[0]["DeptID"].ToString()));
                        ddldesignation_SelectedIndexChanged(sender, e);
                        txtdegnation.Text = dt.Rows[0]["DesignationName"].ToString();
                        try
                        {
                            ddlactive.SelectedValue = dt.Rows[0]["Active"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        filllicense_role();
                        if (Convert.ToString(dt.Rows[0]["RoleId"]) != "")
                        {
                            DDLLicense_role.SelectedIndex = DDLLicense_role.Items.IndexOf(DDLLicense_role.Items.FindByValue(dt.Rows[0]["RoleId"].ToString()));
                        }

                        ImageButton1.Visible = false;
                        btnupdate.Visible = true;
                        pnladd.Visible = true;
                        btnadd.Visible = false;
                        lbladd.Text = "Edit Designation";

                        string str11 = "select ParentDesignationID from DesignationMasterTemp where DesignationMasterTemp.DesignationID = '" + ViewState["editid"] + "'";
                        SqlDataAdapter adpt11 = new SqlDataAdapter(str11, con);
                        DataTable dt11 = new DataTable();
                        adpt11.Fill(dt11);

                        //if (dt11.Rows.Count > 0)
                        //{
                        fillparentdesg();
                        if (dt11.Rows.Count > 0)
                        {
                            ddlparentdesg.SelectedIndex = ddlparentdesg.Items.IndexOf(ddlparentdesg.Items.FindByValue(dt11.Rows[0]["ParentDesignationID"].ToString()));
                        }
                    }
                }
                else
                {
                    statuslable.Text = "Sorry, you cannot edit this record.";
                }
            }
            else
            {
                string str = "select DesignationMaster.*,DepartmentmasterMNC.Whid from DesignationMaster inner join DepartmentmasterMNC on DesignationMaster.DeptID =  DepartmentmasterMNC.id where DesignationMaster.DesignationMasterId = '" + ViewState["editid"] + "'";
                SqlDataAdapter adpt = new SqlDataAdapter(str, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
                    ddlstore_SelectedIndexChanged(sender, e);
                    ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(dt.Rows[0]["DeptID"].ToString()));
                    ddldesignation_SelectedIndexChanged(sender, e);
                    txtdegnation.Text = dt.Rows[0]["DesignationName"].ToString();
                    filllicense_role();
                    if (Convert.ToString(dt.Rows[0]["RoleId"]) != "")
                    {
                        string ss=dt.Rows[0]["RoleId"].ToString(); 
                        DDLLicense_role.SelectedIndex = DDLLicense_role.Items.IndexOf(DDLLicense_role.Items.FindByValue(dt.Rows[0]["RoleId"].ToString()));
                    }

                    ImageButton1.Visible = false;
                    btnupdate.Visible = true;
                    pnladd.Visible = true;
                    btnadd.Visible = false;
                    lbladd.Text = "Edit Designation";


                    string str11 = "select ParentDesignationID from DesignationMasterTemp where DesignationMasterTemp.DesignationID = '" + ViewState["editid"] + "'";
                    SqlDataAdapter adpt11 = new SqlDataAdapter(str11, con);
                    DataTable dt11 = new DataTable();
                    adpt11.Fill(dt11);

                    //if (dt11.Rows.Count > 0)
                    //{
                    fillparentdesg();
                    if (dt11.Rows.Count > 0)
                    {
                        ddlparentdesg.SelectedIndex = ddlparentdesg.Items.IndexOf(ddlparentdesg.Items.FindByValue(dt11.Rows[0]["ParentDesignationID"].ToString()));
                    }
                }
            }
        }
    }
    protected void ddlBusiness_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlBusiness = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlBusiness");
        Label lblBusId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblBusId");

        DropDownList ddlgrdct = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlgrdcat");
        Label catid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblgrdCatid");

        string str11 = "SELECT  Departmentname as Departmentname,id FROM DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where DepartmentmasterMNC.Companyid='" + compid + "' and DepartmentmasterMNC.Whid = '" + ddlBusiness.SelectedValue + "' and  [WareHouseMaster].status = '1' order by Departmentname";
        SqlCommand cmd11 = new SqlCommand(str11, con);
        SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
        DataTable dt11 = new DataTable();
        adp11.Fill(dt11);
        if (dt11.Rows.Count > 0)
        {
            ddlgrdct.DataSource = dt11;
            ddlgrdct.DataTextField = "Departmentname";
            ddlgrdct.DataValueField = "id";
            ddlgrdct.DataBind();

            ddlgrdct.Items.Insert(0, "-Select-");
            ddlgrdct.Items[0].Value = "0";
            ddlgrdct.SelectedIndex = ddlgrdct.Items.IndexOf(ddlgrdct.Items.FindByValue(catid.Text));

        }

    }


    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        //Label lbldpid1 = (Label)GridView1.Rows[e.RowIndex].FindControl("lblCatId");
        ViewState["Id"] = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        if (ViewState["Id"].ToString() != "")
        {
            SqlCommand cmddel = new SqlCommand("select DeleteAllowed,DesignationId from Fixeddata where DesignationId='" + ViewState["Id"].ToString() + "'", con);
            SqlDataAdapter dtpdel = new SqlDataAdapter(cmddel);
            DataTable dtdel = new DataTable();
            dtpdel.Fill(dtdel);
            if (dtdel.Rows.Count > 0)
            {
                if (dtdel.Rows[0]["DeleteAllowed"].ToString() == "0")
                {

                    ImageButton2_Click(sender, e);
                }
                else
                {
                    statuslable.Text = "Sorry, you cannot delete this record.";

                }
            }
            else
            {

                ImageButton2_Click(sender, e);
            }
        }


    }


    protected void ddldesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        statuslable.Text = "";

        string strfillgrid = "SELECT WareHouseMaster.WareHouseId,DepartmentmasterMNC.ID from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where Companyid='" + compid + "' and  [WareHouseMaster].status = '1' and DepartmentmasterMNC.id='" + ddldesignation.SelectedValue + "' order by Departmentname";
        SqlCommand cmdfillgrid = new SqlCommand(strfillgrid, con);
        SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
        DataTable dtfill = new DataTable();
        adpfillgrid.Fill(dtfill);
        if (dtfill.Rows.Count > 0)
        {
            Session["WId11"] = dtfill.Rows[0]["WareHouseId"];


            
        }
        fillparentdesg();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList2.Items.Clear();
        if (DropDownList1.SelectedIndex > 0)
        {
            DropDownList2.Items.Clear();

            string str = "select DesignationName,DesignationMasterId FROM DesignationMaster left outer join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where DeptID='" + DropDownList1.SelectedValue + "'and DepartmentmasterMNC.Companyid='" + compid + "' order by DesignationName";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "DesignationName";
            DropDownList2.DataValueField = "DesignationMasterId";
            DropDownList2.DataBind();

            DropDownList2.Items.Insert(0, "All");
            DropDownList2.SelectedItem.Value = "0";

        }
        else
        {

            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, "All");

        }
        DropDownList2_SelectedIndexChanged(sender, e);
      //  fillgrid();
    }
    protected void ddlactivesearch_SelectedIndexChanged(object sender, EventArgs e)
    {       
        fillgrid();
    }


    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
       // fillgrid();
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (ddldesignation.SelectedIndex > 0)
        {
            string str1 = "SELECT DesignationName,DesignationMasterId FROM DesignationMaster left outer join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where DesignationName='" + txtdegnation.Text + "' and  DeptID='" + ddldesignation.SelectedValue + "' and DepartmentmasterMNC.Companyid='" + Session["Comid"] + "' and DepartmentmasterMNC.Whid='" + Session["WId11"] + "'    order by DesignationName";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                statuslable.Text = "Record already exists";
            }
            else
            {
                DataTable dtv = select("SELECT * from DepartmentmasterMNC  where Departmentname='" + ddldesignation.SelectedItem.Text + "' and Companyid='" + Session["Comid"] + "' and id='" + ddldesignation.SelectedValue + "' ");
                int k1 = 0;
                string rolid = "";
                foreach (DataRow item in dtv.Rows)
                {
                    DataTable dtr = select("SELECT * from DesignationMaster  where DesignationName='"+txtdegnation.Text+"' and DeptID='" + item["Id"] + "' ");

                    if (dtr.Rows.Count == 0)
                     {
                         
                         string str = "insert into  DesignationMaster  values ('" + txtdegnation.Text + "','" + item["Id"] + "','" + DDLLicense_role.SelectedValue + "','"+ ddlactive.SelectedValue  +"')";
                         SqlCommand cmd = new SqlCommand(str, con);
                         if (con.State.ToString() != "Open")
                         {
                             con.Open();
                         }
                         cmd.ExecuteNonQuery();
                         con.Close();

                         SqlDataAdapter dade = new SqlDataAdapter("select max(DesignationMasterId) as DesignationMasterId from DesignationMaster", con);
                         DataTable dtde = new DataTable();
                         dade.Fill(dtde);
                         SqlDataAdapter dadeDn = new SqlDataAdapter("select * from DesignationMaster where DesignationName='" + ddlparentdesg.SelectedItem.Text + "' and DeptID='" + item["Id"] + "'", con);
                         DataTable dtdeDn = new DataTable();
                         dadeDn.Fill(dtdeDn);
                         if (dtdeDn.Rows.Count > 0)
                         {
                             string str2 = "insert into  DesignationMasterTemp  values ('" + dtde.Rows[0]["DesignationMasterId"] + "','" + item["Id"] + "','" + dtdeDn.Rows[0]["DesignationMasterId"] + "')";
                             SqlCommand cmd2 = new SqlCommand(str2, con);
                             if (con.State.ToString() != "Open")
                             {
                                 con.Open();
                             }
                             cmd2.ExecuteNonQuery();
                             con.Close();
                         }
                     }

                }

                //if (k1 > 0)
                //{
                    fillgrid();
                    statuslable.Text = "Record inserted successfully";
                    txtdegnation.Text = "";
                    ddldesignation.SelectedIndex = 0;
                    DropDownList1_SelectedIndexChanged(sender, e);
                    if (CheckBox1.Checked == true)
                    {
                     //   string te = "Page_role_Access.aspx?id=" + rolid;
                      //  ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                    }
                    pnladd.Visible = false;
                    lbladd.Text = "";
                    btnadd.Visible = true;
                //}
                //else
                //{
                //    statuslable.Text = "Record not inserted successfully";
                //}
            }
        }
        else
        {

            statuslable.Text = "Please select Department";
        }
    }

    /*
      protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (ddldesignation.SelectedIndex > 0)
        {
            string str1 = "SELECT DesignationName,DesignationMasterId FROM DesignationMaster left outer join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where DesignationName='" + txtdegnation.Text + "' and  DeptID='" + ddldesignation.SelectedValue + "' and DepartmentmasterMNC.Companyid='" + Session["Comid"] + "' and DepartmentmasterMNC.Whid='" + Session["WId11"] + "'    order by DesignationName";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                statuslable.Text = "Record already exists";
            }
            else
            {
                DataTable dtv = select("SELECT * from DepartmentmasterMNC  where Departmentname='" + ddldesignation.SelectedItem.Text + "' and Companyid='" + Session["Comid"]+ "' ");
                int k1 = 0;
                string rolid = "";
                foreach (DataRow item in dtv.Rows)
                {
                    DataTable dtr = select("SELECT * from DesignationMaster  where DesignationName='"+txtdegnation.Text+"' and DeptID='" + item["Id"] + "' ");

                    if (dtr.Rows.Count == 0)
                     {
                         if (k1 == 0)
                         {
                             SqlDataAdapter dadeD = new SqlDataAdapter("select * from RoleMaster where Role_name='" + txtdegnation.Text + "' and compid='" + Session["Comid"] + "'", con);
                             DataTable dtdeD = new DataTable();
                             dadeD.Fill(dtdeD);
                             SqlCommand cmd3r = new SqlCommand("Insert into RoleMaster (Role_name,ActiveDeactive,compid) values ('" + txtdegnation.Text + "','True','" + Session["Comid"] + "')", con);
                             if (con.State.ToString() != "Open")
                             {
                                 con.Open();
                             }
                             cmd3r.ExecuteNonQuery();
                             con.Close();
                             k1 = k1 + 1;
                             SqlDataAdapter datrio = new SqlDataAdapter("select max(Role_id) as RoleId  from RoleMaster where Role_name='" + txtdegnation.Text + "' and compid='" + Session["Comid"] + "'", con);
                             DataTable dtdeDq = new DataTable();
                             datrio.Fill(dtdeDq);
                             rolid = Convert.ToString(dtdeDq.Rows[0]["RoleId"]);
                         }
                         string str = "insert into  DesignationMaster  values ('" + txtdegnation.Text + "','" + item["Id"] + "','" + DDLLicense_role.SelectedValue + "')";
                         SqlCommand cmd = new SqlCommand(str, con);
                         if (con.State.ToString() != "Open")
                         {
                             con.Open();
                         }
                         cmd.ExecuteNonQuery();
                         con.Close();

                         SqlDataAdapter dade = new SqlDataAdapter("select max(DesignationMasterId) as DesignationMasterId from DesignationMaster", con);
                         DataTable dtde = new DataTable();
                         dade.Fill(dtde);
                         SqlDataAdapter dadeDn = new SqlDataAdapter("select * from DesignationMaster where DesignationName='" + ddlparentdesg.SelectedItem.Text + "' and DeptID='" + item["Id"] + "'", con);
                         DataTable dtdeDn = new DataTable();
                         dadeDn.Fill(dtdeDn);
                         if (dtdeDn.Rows.Count > 0)
                         {
                             string str2 = "insert into  DesignationMasterTemp  values ('" + dtde.Rows[0]["DesignationMasterId"] + "','" + item["Id"] + "','" + dtdeDn.Rows[0]["DesignationMasterId"] + "')";
                             SqlCommand cmd2 = new SqlCommand(str2, con);
                             if (con.State.ToString() != "Open")
                             {
                                 con.Open();
                             }
                             cmd2.ExecuteNonQuery();
                             con.Close();
                         }
                     }

                }

                if (k1 > 0)
                {
                    fillgrid();
                    statuslable.Text = "Record inserted successfully";
                    txtdegnation.Text = "";
                    ddldesignation.SelectedIndex = 0;
                    DropDownList1_SelectedIndexChanged(sender, e);
                    if (CheckBox1.Checked == true)
                    {
                        string te = "Page_role_Access.aspx?id=" + rolid;
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                    }
                    pnladd.Visible = false;
                    lbladd.Text = "";
                    btnadd.Visible = true;
                }
                else
                {
                    statuslable.Text = "Record not inserted successfully";
                }
            }
        }
        else
        {

            statuslable.Text = "Please select Department";
        }
    }
     * 
     * 
     * 
     */
    protected void Button2_Click(object sender, EventArgs e)
    {

        statuslable.Text = "";
        ddldesignation.SelectedIndex = 0;
        ddlstore.SelectedIndex = 0;
        ddluserrole.SelectedIndex = 0;
        txtdegnation.Text = "";

        pnladd.Visible = false;
        lbladd.Text = "";
        btnadd.Visible = true;
        btnupdate.Visible = false;
        ImageButton1.Visible = true;

    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        SqlCommand cmd1 = new SqlCommand("Select * from User_master where DesigantionMasterId=" + ViewState["Id"] + " ", con);
        SqlDataAdapter ado = new SqlDataAdapter(cmd1);
        DataTable dto = new DataTable();
        ado.Fill(dto);

        SqlCommand cmd11 = new SqlCommand("Select * from EmployeeMaster where DesignationMasterId=" + ViewState["Id"] + " ", con);
        SqlDataAdapter ado1 = new SqlDataAdapter(cmd11);
        DataTable dto1 = new DataTable();
        ado1.Fill(dto1);

        if (dto.Rows.Count > 0 || dto1.Rows.Count > 0)
        {

            statuslable.Text = "There are employees who are listed under this designation. You must remove the employees from this designation before deleting. Please go to " + "<a href=\"EmployeeMaster.aspx\" style=\"font-size:14px; color:red; \" target=\"_blank\">" + "Employee: Add, Manage " + "</a>to proceed.";
        }
        else
        {
            SqlCommand cmd = new SqlCommand("delete  from DesignationMaster where [DesignationMasterId]=" + ViewState["Id"] + " ", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            statuslable.Text = "Record deleted successfully";
            GridView1.SelectedIndex = -1;
            fillgrid();
            DropDownList1_SelectedIndexChanged(sender, e);
        }
    }


    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddldepartment();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddl1();

      //  fillgrid();
    }
    protected void fillrole()
    {
        string emprole = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + compid + "' and ActiveDeactive='1' and Role_name<>'Customer' and Role_name<>'Vendor'  order by Role_name";
        SqlCommand cmdrole = new SqlCommand(emprole, con);
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();

        darole.Fill(dtrole);
        ddluserrole.DataSource = dtrole;
        ddluserrole.DataTextField = "Role_name";
        ddluserrole.DataValueField = "Role_id";
        ddluserrole.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillgrid();

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            GridView1.Columns[3].Visible = false;
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            //if (GridView1.Columns[6].Visible == true)
            //{
            //    ViewState["deleHide"] = "tt";
            //    GridView1.Columns[6].Visible = false;
            //}
        }
        else
        {
            Button1.Text = "Printable Version";
            Button7.Visible = false;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            fillgrid();
            GridView1.Columns[3].Visible = true;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            //if (ViewState["deleHide"] != null)
            //{
            //    GridView1.Columns[6].Visible = true;
            //}
        }
    }
    protected void imgadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "Departmentaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgref_Click(object sender, ImageClickEventArgs e)
    {
        fillddldepartment();
    }
    protected void imgaddrole_Click(object sender, ImageClickEventArgs e)
    {
        string te = "RoleMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgrefrole_Click(object sender, ImageClickEventArgs e)
    {
        fillrole();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        statuslable.Text = "";
        pnladd.Visible = true;
        lbladd.Text = "Add New Designation";
        btnadd.Visible = false;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        SqlCommand cmdup = new SqlCommand("select DesignationName,DeptID from DesignationMaster left outer join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where DesignationName='" + txtdegnation.Text + "' and DeptID='" + ddldesignation.SelectedValue + "' and Whid='" + ddlstore.SelectedValue + "' and  DesignationMasterId<>'" + ViewState["editid"] + "' ", con);
        SqlDataAdapter dtpup = new SqlDataAdapter(cmdup);
        DataTable dtup = new DataTable();
        dtpup.Fill(dtup);
        if (dtup.Rows.Count > 0)
        {
            statuslable.Text = "Record already exists";

            GridView1.EditIndex = -1;
            fillgrid();
        }
        else
        {
            string roled = "";
            DataTable dtsr = select("select DesignationName,DeptID,RoleId from DesignationMaster  where DesignationMasterId='" + ViewState["editid"] + "' ");
            if (dtsr.Rows.Count > 0)
            {
                string update = "update DesignationMaster set DesignationName='" + txtdegnation.Text + "', " +
                      " DeptID='" + ddldesignation.SelectedValue + "' , Roleid='" + DDLLicense_role.SelectedValue + "' ,Active='"+ddlactive.SelectedValue+"' where DesignationMasterId='" + ViewState["editid"] + "' ";

                SqlCommand cmdupate = new SqlCommand(update, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }

                cmdupate.ExecuteNonQuery();
                con.Close();

                statuslable.Text = "Record updated successfully";

                txtdegnation.Text = "";
                ddldesignation.SelectedIndex = 0;
                ddlstore.SelectedIndex = 0;
                ddluserrole.SelectedIndex = 0;
                pnladd.Visible = false;
                lbladd.Text = "";
                ImageButton1.Visible = true;
                btnupdate.Visible = false;
                btnadd.Visible = true;
                fillgrid();


                                                //DataTable dtalldes = select("select DesignationName,DeptID,DesignationMasterId,Whid from DesignationMaster inner join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where DesignationName='" + dtsr.Rows[0]["DesignationName"] + "' and Companyid='" + Session["Comid"] + "' ");
                                                //foreach (DataRow item in dtalldes.Rows)
                                                //{
                                                //    DataTable datadeptd = select("select Id from DepartmentmasterMNC  where Departmentname='" + ddldesignation.SelectedItem.Text + "' and Whid='" + item["Whid"] + "' ");
                                                //    if (datadeptd.Rows.Count > 0)
                                                //    {
                                                //                string update = "update DesignationMaster set DesignationName='" + txtdegnation.Text + "', " +
                                                //                " DeptID='" + datadeptd.Rows[0]["Id"] + "' AND Roleid='"+ DDLLicense_role.SelectedValue +"'  where DesignationMasterId='" + item["DesignationMasterId"] + "' ";

                                                //                SqlCommand cmdupate = new SqlCommand(update, con);
                                                //                if (con.State.ToString() != "Open")
                                                //                {
                                                //                    con.Open();
                                                //                }

                                                //                    cmdupate.ExecuteNonQuery();
                                                //                    con.Close();
                                                //                    //  DataTable datatemp = select("select DesignationMasterId from DesignationMasterTemp inner join DesignationMaster on DesignationMaster.DesignationMasterId=DesignationMasterTemp.DesignationID inner join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where  DesignationName='" + ddlparentdesg.SelectedItem.Text + "' and Whid='" + item["Whid"] + "'  ");
                                                //                    DataTable datatemp = select("select DesignationMasterId from  DesignationMaster inner join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where  DesignationName='" + ddlparentdesg.SelectedItem.Text + "' and Whid='" + item["Whid"] + "'  ");

                                                //                    if (datatemp.Rows.Count > 0)
                                                //                    {

                                                //                        DataTable dty = select("select * from DesignationMasterTemp  where DesignationID='" + item["DesignationMasterId"] + "' ");
                                                //                        if (dty.Rows.Count > 0)
                                                //                        {
                                                //                            string update11 = "update DesignationMasterTemp set DepartmentID='" + datadeptd.Rows[0]["Id"] + "',ParentDesignationID='" + datatemp.Rows[0]["DesignationMasterId"] + "' where DesignationID='" + item["DesignationMasterId"] + "' ";
                                                //                            SqlCommand cmdupate11 = new SqlCommand(update11, con);
                                                //                            if (con.State.ToString() != "Open")
                                                //                            {
                                                //                                con.Open();
                                                //                            }
                                                //                            cmdupate11.ExecuteNonQuery();
                                                //                            con.Close();
                                                //                        }
                                                //                        else
                                                //                        {
                                                //                            string ins123 = "insert into DesignationMasterTemp values('" + item["DesignationMasterId"] + "','" + datadeptd.Rows[0]["Id"] + "','" + datatemp.Rows[0]["DesignationMasterId"] + "' )";
                                                //                            SqlCommand vd = new SqlCommand(ins123,con);
                                                //                            con.Open();
                                                //                            vd.ExecuteNonQuery();
                                                //                            con.Close();
                                                //                        }

                                                //                    }
                                                //    }
                                                //}
            }

            if (roled.Length > 0)
            {
                statuslable.Text = "Record updated successfully";

                txtdegnation.Text = "";
                ddldesignation.SelectedIndex = 0;
                ddlstore.SelectedIndex = 0;
                ddluserrole.SelectedIndex = 0;
                pnladd.Visible = false;
                lbladd.Text = "";
                ImageButton1.Visible = true;
                btnupdate.Visible = false;
                btnadd.Visible = true;
                fillgrid();

                if (CheckBox1.Checked == true)
                {
                    string te = "Page_role_Access.aspx?id=" + roled;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }

            }
            else
            {
                //statuslable.Text = "Record not successfully";
            }
        }
    }


    //protected void btnupdate_Click(object sender, EventArgs e)
    //{
    //    SqlCommand cmdup = new SqlCommand("select DesignationName,DeptID from DesignationMaster left outer join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where DesignationName='" + txtdegnation.Text + "' and DeptID='" + ddldesignation.SelectedValue + "' and Whid='" + ddlstore.SelectedValue + "' and  DesignationMasterId<>'" + ViewState["editid"] + "' ", con);
    //    SqlDataAdapter dtpup = new SqlDataAdapter(cmdup);
    //    DataTable dtup = new DataTable();
    //    dtpup.Fill(dtup);
    //    if (dtup.Rows.Count > 0)
    //    {
    //        statuslable.Text = "Record already exists";

    //        GridView1.EditIndex = -1;
    //        fillgrid();
    //    }
    //    else
    //    {
    //        string roled = "";
    //        DataTable dtsr = select("select DesignationName,DeptID,RoleId from DesignationMaster  where DesignationMasterId='" + ViewState["editid"] + "' ");
    //        if (dtsr.Rows.Count > 0)
    //        {
    //            roled = dtsr.Rows[0]["RoleId"].ToString();
    //            string uprol = "update RoleMaster set Role_name='" + txtdegnation.Text + "' where Role_id='" + dtsr.Rows[0]["RoleId"] + "' ";
    //            SqlCommand cmupr = new SqlCommand(uprol, con);
    //            if (con.State.ToString() != "Open")
    //            {
    //                con.Open();
    //            }
    //            cmupr.ExecuteNonQuery();
    //            con.Close();
    //            DataTable dtalldes = select("select DesignationName,DeptID,DesignationMasterId,Whid from DesignationMaster inner join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where DesignationName='" + dtsr.Rows[0]["DesignationName"] + "' and Companyid='" + Session["Comid"] + "' ");
    //            foreach (DataRow item in dtalldes.Rows)
    //            {
    //                DataTable datadeptd = select("select Id from DepartmentmasterMNC  where Departmentname='" + ddldesignation.SelectedItem.Text + "' and Whid='" + item["Whid"] + "' ");
    //                if (datadeptd.Rows.Count > 0)
    //                {
    //                    string update = "update DesignationMaster set DesignationName='" + txtdegnation.Text + "', " +
    //                  " DeptID='" + datadeptd.Rows[0]["Id"] + "' AND Roleid='" + DDLLicense_role.SelectedValue + "'  where DesignationMasterId='" + item["DesignationMasterId"] + "' ";

    //                    SqlCommand cmdupate = new SqlCommand(update, con);
    //                    if (con.State.ToString() != "Open")
    //                    {
    //                        con.Open();
    //                    }

    //                    cmdupate.ExecuteNonQuery();
    //                    con.Close();
    //                    //  DataTable datatemp = select("select DesignationMasterId from DesignationMasterTemp inner join DesignationMaster on DesignationMaster.DesignationMasterId=DesignationMasterTemp.DesignationID inner join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where  DesignationName='" + ddlparentdesg.SelectedItem.Text + "' and Whid='" + item["Whid"] + "'  ");
    //                    DataTable datatemp = select("select DesignationMasterId from  DesignationMaster inner join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where  DesignationName='" + ddlparentdesg.SelectedItem.Text + "' and Whid='" + item["Whid"] + "'  ");

    //                    if (datatemp.Rows.Count > 0)
    //                    {

    //                        DataTable dty = select("select * from DesignationMasterTemp  where DesignationID='" + item["DesignationMasterId"] + "' ");
    //                        if (dty.Rows.Count > 0)
    //                        {
    //                            string update11 = "update DesignationMasterTemp set DepartmentID='" + datadeptd.Rows[0]["Id"] + "',ParentDesignationID='" + datatemp.Rows[0]["DesignationMasterId"] + "' where DesignationID='" + item["DesignationMasterId"] + "' ";
    //                            SqlCommand cmdupate11 = new SqlCommand(update11, con);
    //                            if (con.State.ToString() != "Open")
    //                            {
    //                                con.Open();
    //                            }
    //                            cmdupate11.ExecuteNonQuery();
    //                            con.Close();
    //                        }
    //                        else
    //                        {
    //                            string ins123 = "insert into DesignationMasterTemp values('" + item["DesignationMasterId"] + "','" + datadeptd.Rows[0]["Id"] + "','" + datatemp.Rows[0]["DesignationMasterId"] + "' )";
    //                            SqlCommand vd = new SqlCommand(ins123, con);
    //                            con.Open();
    //                            vd.ExecuteNonQuery();
    //                            con.Close();
    //                        }

    //                    }
    //                }
    //            }
    //        }

    //        if (roled.Length > 0)
    //        {
    //            statuslable.Text = "Record updated successfully";

    //            txtdegnation.Text = "";
    //            ddldesignation.SelectedIndex = 0;
    //            ddlstore.SelectedIndex = 0;
    //            ddluserrole.SelectedIndex = 0;
    //            pnladd.Visible = false;
    //            lbladd.Text = "";
    //            ImageButton1.Visible = true;
    //            btnupdate.Visible = false;
    //            btnadd.Visible = true;
    //            fillgrid();

    //            if (CheckBox1.Checked == true)
    //            {
    //                string te = "Page_role_Access.aspx?id=" + roled;
    //                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //            }

    //        }
    //        else
    //        {
    //            statuslable.Text = "Record not successfully";
    //        }
    //    }
    //}
}
