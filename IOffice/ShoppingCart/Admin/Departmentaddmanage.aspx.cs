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

public partial class Add_Department : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con;
    string compid;
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
            statuslable.Text = "";
            Pagecontrol.dypcontrol(Page, page);
            //PageConn pgcon = new PageConn();
            //con = pgcon.dynconn; 
            statuslable.Visible = false;

            ViewState["sortOrder"] = "";
            // ddl();
            fillwarehouse();
            // ModalPopupExtender1.Hide();
            ModalPopupExtender1222.Hide();
            FillGridView1();
        }
    }
    protected void fillwarehouse()
    {
        string str1 = "select * from WarehouseMaster where comid='" + Session["comid"] + "' and [WareHouseMaster].status = '1' order by Name";

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds1;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WarehouseId";
            ddlwarehouse.DataBind();

            filterBus.DataSource = ds1;
            filterBus.DataTextField = "Name";
            filterBus.DataValueField = "WarehouseId";
            filterBus.DataBind();
            filterBus.Items.Insert(0, "All");

            ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByText("Eplaza Store"));
        }
    }

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
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

    protected void FillGridView1()
    {
        lblCompany.Text = Session["Cname"].ToString();
        lblBus.Text = "All";
        string strrrr = "";

        string s3;
        string active3="";

        
            if(ddlactivesearch.SelectedIndex >0)
            {
                active3 = " AND DepartmentmasterMNC.Active='" + ddlactivesearch.SelectedValue + "' ";
            }
        if (filterBus.SelectedIndex > 0)
        {
            lblBus.Text = filterBus.SelectedItem.Text;
            s3 = "Departmentname,id,Whid,WarehouseMaster.Name as Whname FROM WarehouseMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.Whid=WarehouseMaster.WarehouseId where WarehouseMaster.WarehouseId='" + filterBus.SelectedValue + "' and DepartmentmasterMNC.Companyid='" + compid + "' and  [WareHouseMaster].status = '1'  " + active3 + " ";
            //order by Whname, Departmentname ";

            strrrr = "select count(DepartmentmasterMNC.Id) as ci from WarehouseMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.Whid=WarehouseMaster.WarehouseId where WarehouseMaster.WarehouseId='" + filterBus.SelectedValue + "' and DepartmentmasterMNC.Companyid='" + compid + "' and  [WareHouseMaster].status = '1' " + active3 + " ";
        }
        else
        {
            s3 = "Departmentname,id,Whid,WarehouseMaster.Name as Whname FROM WarehouseMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.Whid=WarehouseMaster.WarehouseId where Companyid='" + compid + "' and  [WareHouseMaster].status = '1' " + active3 + " ";
            //order by Whname, Departmentname";

            strrrr = "select count(DepartmentmasterMNC.Id) as ci from WarehouseMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.Whid=WarehouseMaster.WarehouseId where Companyid='" + compid + "' and  [WareHouseMaster].status = '1' " + active3 + " ";

        }
        //string strfillgrid = "SELECT Deptname,DeptID FROM Dept_master order by Deptname";

        GridView1.VirtualItemCount = GetRowCount(strrrr);

        string sortExpression = "WarehouseMaster.Name,Departmentname asc";


        //SqlCommand cmdfillgrid = new SqlCommand(strrrr, con);
        //SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
        //DataTable dtfill = new DataTable();
        //adpfillgrid.Fill(dtfill);

        //GridView1.DataSource = dtfill;


        //DataView myDataView = new DataView();
        //myDataView = dtfill.DefaultView;
        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt1 = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, s3);

            GridView1.DataSource = dt1;

            DataView myDataView = new DataView();
            myDataView = dt1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }
    //public void ddl()
    //{

    //    //string strfillgrid = " SELECT Departmentname,id FROM DepartmentmasterMNC where Companyid='" + compid + "' order by Departmentname";
    //    string strfillgrid = " SELECT WarehouseMaster.Name+':'+DepartmentmasterMNC.Departmentname as Departmentname ,DepartmentmasterMNC.id FROM DepartmentmasterMNC inner join WarehouseMaster on WarehouseMaster.WareHouseId=DepartmentmasterMNC.Whid where DepartmentmasterMNC.Companyid='" + compid + "' and WarehouseMaster.Status='1'  order by Departmentname";
    //    SqlCommand cmdfillgrid = new SqlCommand(strfillgrid, con);
    //    SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
    //    DataSet ds = new DataSet();
    //    adpfillgrid.Fill(ds);

    //    DropDownList1.DataSource = ds;
    //    DropDownList1.DataTextField = "Departmentname";
    //    DropDownList1.DataValueField = "id";
    //    DropDownList1.DataBind();

    //    DropDownList1.Items.Insert(0, "All");
    //    DropDownList1.SelectedItem.Value = "0";
    //    FillGridView1();
    //    // DropDownList1.SelectedItem.Value = "0";
    //}
    //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    //{

    //}

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        statuslable.Text = "";
        ViewState["id"] = GridView1.DataKeys[e.NewEditIndex].Value.ToString();
        SqlCommand cmdedit = new SqlCommand("select EditAllowed,DeptId from Fixeddata where DeptId='" + ViewState["id"] + "'", con);
        SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
        DataTable dtedit = new DataTable();
        dtpedit.Fill(dtedit);
        if (dtedit.Rows.Count > 0)
        {
            if (dtedit.Rows[0]["EditAllowed"].ToString() == "0")
            {
                edit();
            }
            else
            {
                statuslable.Text = "You are not allowed to edit this record.";

            }
        }
        else
        {
            edit();
        }


    }
    protected void edit()
    {
        string str = "select * from DepartmentmasterMNC where id='" + ViewState["id"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
            txtdegnation.Text = dt.Rows[0]["Departmentname"].ToString();
           // ddlactivestatus.SelectedValue=Convert.ToBoolean(dt.Rows[0]["Active"]);
            try
            {
                ddlactivestatus.SelectedValue = dt.Rows[0]["Active"].ToString();
            }
            catch (Exception ex)
            {
            }
            pnladd.Visible = true;
            btnadd.Visible = false;
            lbladd.Text = "Edit Department " + dt.Rows[0]["Departmentname"].ToString();
            btnupdate.Visible = true;
            ImageButton1.Visible = false;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGridView1();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        //if (DropDownList1.SelectedIndex > 0)
        //{
        //    DropDownList1_SelectedIndexChanged(sender, e);
        //}
        //else
        //{
        FillGridView1();
        //}
        //FillGridView1();
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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        ViewState["Id"] = GridView1.DataKeys[e.RowIndex].Value.ToString();

        Label lbldpid1 = (Label)GridView1.Rows[e.RowIndex].FindControl("lblCatId");
        if (lbldpid1.Text != "")
        {
            SqlCommand cmddel = new SqlCommand("select DeleteAllowed,DeptId from Fixeddata where DeptId='" + lbldpid1.Text + "'", con);
            SqlDataAdapter dtpdel = new SqlDataAdapter(cmddel);
            DataTable dtdel = new DataTable();
            dtpdel.Fill(dtdel);

            if (dtdel.Rows.Count > 0)
            {
                if (dtdel.Rows[0]["DeleteAllowed"].ToString() == "0")
                {
                    // ModalPopupExtender1222.Show();
                    delete();
                }
                else
                {
                    statuslable.Text = "You are not allow delete this record";
                    // ModalPopupExtender2.Show();

                }
            }
            else
            {
                //ModalPopupExtender1222.Show();
                delete();
            }
        }

    }
    protected void delete()
    {
        SqlCommand cmd1 = new SqlCommand("Select * from DesignationMaster where DeptID = " + ViewState["Id"] + " ", con);
        SqlDataAdapter ado = new SqlDataAdapter(cmd1);
        DataTable dto = new DataTable();
        ado.Fill(dto);
        if (dto.Rows.Count > 0)
        {
            statuslable.Visible = true;
            //statuslable.Text = "Sorry,First delete the record of Designation under this Department before you attempt to delete again";
            statuslable.Text = "There are designations which are listed under this department. You must remove the designation from this department before deleting. Please go to " + "<a href=\"DesignationAddManage.aspx\" style=\"font-size:14px; color:red; \" target=\"_blank\">" + "Designation: Add, Manage " + "</a>to proceed.";
        }
        else
        {
            SqlCommand cmd = new SqlCommand("delete  from DepartmentmasterMNC where [id]=" + ViewState["Id"] + " ", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            statuslable.Visible = true;
            statuslable.Text = "Record deleted successfully";
            FillGridView1();
            //  ddl();
            GridView1.SelectedIndex = -1;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void btncancel_Click(object sender, EventArgs e)
    {

    }

    protected void ImageButton61_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzEmailContentMaster.aspx");
    }
    protected void ImageButton71_Click(object sender, ImageClickEventArgs e)
    {

        Response.Redirect("~/ShoppingCart/Admin/wzDesignationmaster.aspx");
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        try
        {
            string str1 = "";
            str1 = "SELECT * from DepartmentmasterMNC  where Departmentname='" + txtdegnation.Text + "' and DepartmentmasterMNC.Whid='" + ddlwarehouse.SelectedValue + "' " +
             "order by Departmentname";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                statuslable.Visible = true;
                statuslable.Text = "Record already exists";
            }
            else
            {
                DataTable dts = select("Select * from WareHouseMaster where comid='"+Session["Comid"]+"'");
                foreach (DataRow item in dts.Rows)
                {

                    DataTable dtv = select("SELECT * from DepartmentmasterMNC  where Departmentname='" + txtdegnation.Text + "' and DepartmentmasterMNC.Whid='" + item["WareHouseId"] + "' ");

                    if (dtv.Rows.Count == 0)
                    {
                        SqlCommand cmddept = new SqlCommand("DeptRetIdentity", con);
                        cmddept.CommandType = CommandType.StoredProcedure;
                        cmddept.Parameters.AddWithValue("@Departmentname", txtdegnation.Text);
                        cmddept.Parameters.AddWithValue("@Companyid", Session["Comid"]);
                        cmddept.Parameters.AddWithValue("@Whid", item["WareHouseId"]);
                        cmddept.Parameters.AddWithValue("@Active", ddlactivestatus.SelectedValue);
                        cmddept.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                        cmddept.Parameters["@Id"].Direction = ParameterDirection.Output;
                        cmddept.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                        cmddept.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
                 

                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        object dept = (object)cmddept.ExecuteNonQuery();
                        dept = cmddept.Parameters["@Id"].Value;
                    }
                   
                }
                statuslable.Visible = true;
                statuslable.Text = "Record inserted successfully";
                FillGridView1();
                // ddl();
                //GridView1.DataBind();
                txtdegnation.Text = "";
                ddlwarehouse.SelectedIndex = 0;
                pnladd.Visible = false;
                // lbladd.Visible = false;
                lbladd.Text = "";
                btnadd.Visible = true;
            }
        }
        catch (Exception ererer)
        {
            statuslable.Visible = true;
            statuslable.Text = "error ;" + ererer.Message;

        }
    }
    
    protected void ImageButton8_Click(object sender, EventArgs e)
    {
        statuslable.Visible = false;
        txtdegnation.Text = "";
        pnladd.Visible = false;
        ddlwarehouse.SelectedIndex = 0;
        //lbladd.Visible = false;
        lbladd.Text = "";
        btnadd.Visible = true;
        btnupdate.Visible = false;
        ImageButton1.Visible = true;
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        SqlCommand cmd1 = new SqlCommand("Select * from DesignationMaster where DeptID = " + ViewState["Id"] + " ", con);
        SqlDataAdapter ado = new SqlDataAdapter(cmd1);
        DataTable dto = new DataTable();
        ado.Fill(dto);
        if (dto.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "Sorry,First delete the record of Designation under this Department before you attemp to delete again";
        }
        else
        {
            SqlCommand cmd = new SqlCommand("delete  from DepartmentmasterMNC where [id]=" + ViewState["Id"] + " ", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            statuslable.Visible = true;
            statuslable.Text = "Record deleted successfully";
            FillGridView1();
            //  ddl();
            GridView1.SelectedIndex = -1;
        }
    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {
        statuslable.Visible = false;
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void ImageButton10_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Hide();
    }
    protected void filterBus_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridView1();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;


            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;

            FillGridView1();

            if (GridView1.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[2].Visible = false;
            }
            //if (GridView1.Columns[3].Visible == true)
            //{
            //    ViewState["deleHide"] = "tt";
            //    GridView1.Columns[3].Visible = false;
            //}

        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(300);

            Button1.Text = "Printable Version";
            Button7.Visible = false;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;

            FillGridView1();
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[2].Visible = true;
            }
            //if (ViewState["deleHide"] != null)
            //{
            //    GridView1.Columns[3].Visible = true;
            //}

        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        statuslable.Text = "";
        pnladd.Visible = true;
        lbladd.Text = "Add Department";
        btnadd.Visible = false;
        // lbladd.Visible = true;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {




            string str1 = "SELECT * from DepartmentmasterMNC  where Departmentname='" + txtdegnation.Text + "' and DepartmentmasterMNC.Whid='" + ddlwarehouse.SelectedValue + "' and id<>'" + ViewState["id"] + "' " +
           "order by Departmentname";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                statuslable.Visible = true;
                statuslable.Text = "Record already exists";
            }

            else
            {
                DataTable dtcv = select("SELECT Departmentname from DepartmentmasterMNC  where  id='" + ViewState["id"] + "'");
                if (dtcv.Rows.Count > 0)
                {
                    string sr51 = ("update DepartmentmasterMNC set Departmentname='" + txtdegnation.Text + "' ,Active='" + ddlactivestatus .SelectedValue+ "' where Departmentname='" + dtcv.Rows[0]["Departmentname"] + "' and Companyid='" + Session["Comid"] + "'  ");

                    //string sr51 = ("update DepartmentmasterMNC set Departmentname='" + txtdegnation.Text + "',Whid='" + ddlwarehouse.SelectedValue + "'  where id='" + ViewState["id"] + "' ");
                    SqlCommand cmd801 = new SqlCommand(sr51, con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd801.ExecuteNonQuery();
                    con.Close();
                    FillGridView1();
                    statuslable.Visible = true;
                    statuslable.Text = "Record updated successfully";
                    pnladd.Visible = false;
                    btnadd.Visible = true;
                    lbladd.Text = "";
                    btnupdate.Visible = false;
                    ImageButton1.Visible = true;
                    ddlwarehouse.SelectedIndex = 0;
                    txtdegnation.Text = "";
                }
            }


        }
        catch (Exception ert)
        {
            statuslable.Visible = true;
            statuslable.Text = "Error :" + ert.Message;

        }
    }
}
