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

public partial class Add_Employee_Batch : System.Web.UI.Page
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


        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {

            Pagecontrol.dypcontrol(Page, page);
            Fillwarehouse();
            //DropDownList3.Items.Insert(0, "-Select-");
            //DropDownList3.Items[0].Value = "0";
            ViewState["sortOrder"] = "";
            //fillemployee();

            fillbatch();
            fillstorebatch();
            gridfun();
            lblCompany.Text = Session["Cname"].ToString();
            ddlfilterstore_SelectedIndexChanged(sender, e);
        }

    }
    protected void Fillwarehouse()
    {

        ddlstrname.Items.Clear();
        string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "' and [WareHouseMaster].Status='1' order by Name ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlstrname.DataSource = ds;
        ddlstrname.DataTextField = "Name";
        ddlstrname.DataValueField = "WareHouseId";
        ddlstrname.DataBind();
        ViewState["cd"] = "1";
        ddlstrname.Items.Insert(0, "-Select-");
        ddlstrname.Items[0].Value = "0";

        ddlfilterstore.DataSource = ds;
        ddlfilterstore.DataTextField = "Name";
        ddlfilterstore.DataValueField = "WareHouseId";
        ddlfilterstore.DataBind();
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstrname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            ddlfilterstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

        }

    }
    protected void fillfilteremp()
    {
        ddlfilteremployee.Items.Clear();
        SqlDataAdapter adpt = new SqlDataAdapter("select EmployeeMaster.EmployeeMasterID,WareHouseMaster.Name +' : '+ EmployeeMaster.EmployeeName +' : '+ DesignationMaster.DesignationName as EmployeeName from EmployeeMaster inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join WareHouseMaster on EmployeeMaster.Whid = WareHouseMaster.WareHouseId where EmployeeMaster.Active='1' and EmployeeMaster.Whid='" + ddlfilterstore.SelectedValue + "' order by EmployeeName", con);
        DataTable dsemp = new DataTable();
        adpt.Fill(dsemp);
        ddlfilteremployee.DataSource = dsemp;
        ddlfilteremployee.DataTextField = "EmployeeName";
        ddlfilteremployee.DataValueField = "EmployeeMasterID";
        ddlfilteremployee.DataBind();
        ddlfilteremployee.Items.Insert(0, "All");
        ddlfilteremployee.Items[0].Value = "0";
    }
    protected void fillbatch()
    {
        ddlBatchName.Items.Clear();
        string str = "select BatchMaster.ID,WareHouseMaster.Name +' : '+ BatchMaster.Name +' : '+ TimeSchedulMaster.SchedulName +' : '+ BatchTiming.StartTime +' : '+ BatchTiming.EndTime as BatchName from BatchMaster inner join BatchTiming on BatchMaster.ID = BatchTiming.BatchMasterId inner join TimeSchedulMaster  on BatchTiming.TimeScheduleMasterId = TimeSchedulMaster.id inner join WareHouseMaster on WareHouseMaster.WareHouseId = BatchMaster.WHID where BatchMaster.WHID='" + ddlstrname.SelectedValue + "' and  BatchMaster.Status='" + 1 + "' order by BatchName";

        //string str = "select BatchMaster.Name,BatchMaster.ID,BatchMaster.Status, TimeSchedulMaster.schedulName + ' : ' +left(BatchTiming.StartTime,5) +' : '+ left(BatchTiming.EndTime,5) as sche from BatchMaster inner join BatchTiming on BatchMaster.ID = BatchTiming.BatchMasterId inner join TimeSchedulMaster on BatchTiming.TimeScheduleMasterId = TimeSchedulMaster.id where BatchMaster.ID = '" + lblbatchid.Text + "'";
       
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBatchName.DataSource = ds;
            ddlBatchName.DataTextField = "BatchName";
            ddlBatchName.DataValueField = "ID";
            ddlBatchName.DataBind();
            ddlBatchName.Items.Insert(0, "-Select-");
            ddlBatchName.Items[0].Value = "0";
        }
        else
        {
            ddlBatchName.Items.Insert(0, "-Select-");
            ddlBatchName.Items[0].Value = "0";
        }

        //string str = "select BatchMaster.ID,WareHouseMaster.Name +' : '+ BatchMaster.Name +' : '+ TimeSchedulMaster.SchedulName +' : '+ BatchTiming.StartTime +' : '+ BatchTiming.EndTime as BatchName from BatchMaster inner join BatchTiming on BatchMaster.ID = BatchTiming.BatchMasterId inner join TimeSchedulMaster  on BatchTiming.TimeScheduleMasterId = TimeSchedulMaster.id inner join WareHouseMaster on WareHouseMaster.WareHouseId = BatchMaster.WHID where BatchMaster.WHID='" + ddlstrname.SelectedValue + "' and  BatchMaster.Status='" + 1 + "' order by BatchName";



    }
    protected void fillstorebatch()
    {

        ddlstrbatchname.Items.Clear();
        string str = "select BatchMaster.ID,WareHouseMaster.Name +' : '+ BatchMaster.Name as BatchName from BatchMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId = BatchMaster.WHID where BatchMaster.WHID='" + ddlstrname.SelectedValue + "' and  BatchMaster.Status='" + 1 + "' order by BatchName";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        ListItem[] item = new ListItem[ds.Rows.Count];
        for (int i = 0; i < ds.Rows.Count; i++)
        {
           // string strtime = "select TimeSchedulMaster.SchedulName +' : '+ BatchTiming.StartTime +' : '+ BatchTiming.EndTime as normaltime from BatchTiming inner join TimeSchedulMaster  on BatchTiming.TimeScheduleMasterId = TimeSchedulMaster.id where BatchTiming.BatchMasterId = '" + ds.Rows[i]["ID"].ToString() + "' ";
      
            string strtime = "select TimeSchedulMaster.SchedulName +' : '+ BatchTiming.StartTime +' : '+ BatchTiming.EndTime as normaltime from BatchTiming inner join TimeSchedulMaster  on BatchTiming.TimeScheduleMasterId = TimeSchedulMaster.id where BatchTiming.BatchMasterId = '" + ds.Rows[i]["ID"].ToString() + "'  and  TimeSchedulMaster.id in (1) ";
            SqlCommand cmdtime = new SqlCommand(strtime, con);
            SqlDataAdapter adptime = new SqlDataAdapter(cmdtime);
            DataTable dstime = new DataTable();
            adptime.Fill(dstime);
            if (dstime.Rows.Count > 0)
            {

                item[i] = new ListItem((ds.Rows[i]["BatchName"].ToString() + " : " + dstime.Rows[0]["normaltime"].ToString()), ds.Rows[i]["ID"].ToString());
            }
            else
            {

                item[i] = new ListItem((ds.Rows[i]["BatchName"].ToString() + " : - :" + " - : - "), ds.Rows[i]["ID"].ToString());
            }

        }
        ddlstrbatchname.Items.AddRange(item);
        ddlstrbatchname.DataBind();

    }
    protected void fillfilterbatch()
    {
        ddlfilterbatch.Items.Clear();
        string str = "select BatchMaster.ID,WareHouseMaster.Name +' : '+ BatchMaster.Name as BatchName from BatchMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId = BatchMaster.WHID where BatchMaster.WHID='" + ddlfilterstore.SelectedValue + "' and  BatchMaster.Status='" + 1 + "' order by BatchName";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        ListItem[] item = new ListItem[ds.Rows.Count];
        for (int i = 0; i < ds.Rows.Count; i++)
        {
            string strtime = "select TimeSchedulMaster.SchedulName +' : '+ BatchTiming.StartTime +' : '+ BatchTiming.EndTime as normaltime from BatchTiming inner join TimeSchedulMaster  on BatchTiming.TimeScheduleMasterId = TimeSchedulMaster.id where BatchTiming.BatchMasterId = '" + ds.Rows[i]["ID"].ToString() + "'  and  TimeSchedulMaster.id in (1) ";
            SqlCommand cmdtime = new SqlCommand(strtime, con);
            SqlDataAdapter adptime = new SqlDataAdapter(cmdtime);
            DataTable dstime = new DataTable();
            adptime.Fill(dstime);
            if (dstime.Rows.Count > 0)
            {

                item[i] = new ListItem((ds.Rows[i]["BatchName"].ToString() + " : " + dstime.Rows[0]["normaltime"].ToString()), ds.Rows[i]["ID"].ToString());
            }
            else
            {

                item[i] = new ListItem((ds.Rows[i]["BatchName"].ToString() + " : - :" + " - : - "), ds.Rows[i]["ID"].ToString());
            }

        }
        ddlfilterbatch.Items.AddRange(item);
        ddlfilterbatch.DataBind();
        ddlfilterbatch.Items.Insert(0, "All");
        ddlfilterbatch.Items[0].Value = "0";
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlfilterbatch.DataSource = ds;
        //    ddlfilterbatch.DataTextField = "BatchName";
        //    ddlfilterbatch.DataValueField = "ID";
        //    ddlfilterbatch.DataBind();
        //    ddlfilterbatch.Items.Insert(0, "All");
        //    ddlfilterbatch.Items[0].Value = "0";
        //}
        //else
        //{
        //    ddlfilterbatch.Items.Insert(0, "All");
        //    ddlfilterbatch.Items[0].Value = "0";
        //}
    }

 
    //protected void fillemployee()
    //{

    //    SqlDataAdapter adpt = new SqlDataAdapter("select EmployeeMaster.EmployeeMasterID,WareHouseMaster.Name +' : '+ EmployeeMaster.EmployeeName +' : '+ DesignationMaster.DesignationName as EmployeeName from EmployeeMaster inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join WareHouseMaster on EmployeeMaster.Whid = WareHouseMaster.WareHouseId where EmployeeMaster.Active='1' and EmployeeMaster.Whid='" + ddlstrname.SelectedValue + "' order by EmployeeName", con);
    //    DataTable dsemp = new DataTable();
    //    adpt.Fill(dsemp);

    //    DropDownList3.DataSource = dsemp;
    //    DropDownList3.DataTextField = "EmployeeName";
    //    DropDownList3.DataValueField = "EmployeeMasterID";
    //    DropDownList3.DataBind();
    //    DropDownList3.Items.Insert(0, "-Select-");
    //    DropDownList3.Items[0].Value = "0";


    //    gridfun();
    //}
    public void gridfun()
    {

        string em1 = "";

        if (ddlfilteremployee.SelectedIndex > 0)
        {
            em1 += " and EmployeeBatchMaster.Employeeid = '" + ddlfilteremployee.SelectedValue + "'";
        }
        if (ddlfilterbatch.SelectedIndex > 0)
        {
            em1 += "and EmployeeBatchMaster.Batchmasterid = '" + ddlfilterbatch.SelectedValue + "'";
        }
        if (ddlstatus.SelectedIndex > 0)
        {
            em1 += "and EmployeeMaster.Active = '" + ddlstatus.SelectedValue + "'";
        }

        string em = " EmployeeBatchMaster.*,BatchMaster.Name,BatchMaster.Status,case when(EmployeeMaster.Active = '1') then 'Active' else 'Inactive' end as Active,EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname,DesignationMaster.DesignationName,EmployeePayrollMaster.EmployeeNo from EmployeeBatchMaster inner join BatchMaster on BatchMaster.ID=EmployeeBatchMaster.Batchmasterid inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   left join EmployeePayrollMaster on EmployeeMaster.EmployeeMasterID=EmployeePayrollMaster.EmpId inner join DepartmentmasterMNC on EmployeeMaster.DeptID=DepartmentmasterMNC.id inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId where EmployeeBatchMaster.Whid='" + ddlfilterstore.SelectedValue + "' " + em1 + "";

        string str2 = " select count(EmployeeBatchMaster.id) as ci from EmployeeBatchMaster inner join BatchMaster on BatchMaster.ID=EmployeeBatchMaster.Batchmasterid inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   left join EmployeePayrollMaster on EmployeeMaster.EmployeeMasterID=EmployeePayrollMaster.EmpId inner join DepartmentmasterMNC on EmployeeMaster.DeptID=DepartmentmasterMNC.id inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId where EmployeeBatchMaster.Whid='" + ddlfilterstore.SelectedValue + "' " + em1 + "";

        //em += "order by EmployeeName";
        //SqlCommand cmd3 = new SqlCommand(em, con);
        //SqlDataAdapter adap3 = new SqlDataAdapter(cmd3);
        //DataTable ds3 = new DataTable();
        //adap3.Fill(ds3);

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " EmployeeName asc";

        lblBusines.Text = ddlfilterstore.SelectedItem.Text;

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable ds3 = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, em);

            //if (ds3.Rows.Count > 0)
            //{
            DataView myDataView = new DataView();
            myDataView = ds3.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataSource = myDataView;
            GridView1.DataBind();

            hfidwhid.Value = ds3.Rows[0][2].ToString();
            hfempid.Value = ds3.Rows[0][3].ToString();
            hfbmid.Value = ds3.Rows[0][4].ToString();

            foreach (GridViewRow gdr in GridView1.Rows)
            {
                Label lblbatchid = (Label)gdr.FindControl("lblbatchid");
                Label Label12 = (Label)gdr.FindControl("Label12");
                string str = "select BatchMaster.Name,BatchMaster.ID,BatchMaster.Status, TimeSchedulMaster.schedulName + ' : ' +left(BatchTiming.StartTime,5) +' : '+ left(BatchTiming.EndTime,5) as sche from BatchMaster inner join BatchTiming on BatchMaster.ID = BatchTiming.BatchMasterId inner join TimeSchedulMaster on BatchTiming.TimeScheduleMasterId = TimeSchedulMaster.id where BatchMaster.ID = '" + lblbatchid.Text + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Label12.Text = " : " + dt.Rows[0]["sche"].ToString();
                }
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        //ddl();
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

    public void ddl()
    {
        if (GridView1.Rows.Count > 0)
        {
            foreach (GridViewRow gdr in GridView1.Rows)
            {
                DropDownList drp = (DropDownList)gdr.Cells[0].FindControl("DropDownList2");

                drp.DataSource = (DataSet)fillddl();

                drp.DataTextField = "name";
                drp.DataValueField = "BatchMasterId";
                drp.DataBind();
            }
            con.Close();
        }
    }


    protected void ddlstrname_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //fillemployee();
        fillbatch();

    }


    public DataSet fillddl()
    {
        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
        Mycommand = new SqlCommand(" SELECT distinct BatchTiming.BatchMasterId,CONVERT(nvarchar(6),BatchTiming.StartTime) +' - '+ CONVERT(nvarchar(6),BatchTiming.EndTime)+' :'+ BatchMaster.Name as name, BatchMaster.ID FROM BatchTiming INNER JOIN BatchMaster ON BatchTiming.BatchMasterId = BatchMaster.ID where BatchTiming.Whid='" + ddlstrname.SelectedValue + "' and BatchMaster.Status='1' order by name", con);
        Mycommand.CommandType = CommandType.Text;
        MyDataAdapter = new SqlDataAdapter(Mycommand);
        MyDataAdapter.Fill(ds);
        con.Close();
        return ds;
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        //Button lk = (Button)sender;
        // int index = Convert.ToInt32(lk.CommandArgument);
        // ViewState["Id"] = index;
        if (!(CheckBox1.Checked))
        {


            //Label empname = (Label)GridView1.Rows[index].FindControl("Label1");

            int k = 0;
            string str2 = "Select EmployeeBatchMaster.id  from  EmployeeMaster  inner join  EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID where EmployeeBatchMaster.Employeeid='" + DropDownList3.SelectedValue + "'";
            SqlCommand cmd2 = new SqlCommand(str2, con);

            SqlDataAdapter adap2 = new SqlDataAdapter(cmd2);
            DataTable ds2 = new DataTable();
            adap2.Fill(ds2);
            int count2;
            count2 = ds2.Rows.Count;
            if (count2 > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "This Employee is already assigned to one batch";


                foreach (GridViewRow gridemp in this.GridView1.Rows)
                {
                    ViewState["empid"] = (int)GridView1.DataKeys[k].Value;
                    DropDownList batchid = (DropDownList)gridemp.FindControl("DropDownList2");
                    string str21 = "Select distinct EmployeeBatchMaster.[Employeeid]  from  EmployeeMaster  inner join  EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID where EmployeeBatchMaster.Employeeid='" + ViewState["empid"] + "'";
                    SqlCommand cmd21 = new SqlCommand(str2, con);

                    SqlDataAdapter adap21 = new SqlDataAdapter(cmd21);
                    DataTable ds21 = new DataTable();
                    adap2.Fill(ds21);
                    int count21;
                    count21 = ds2.Rows.Count;
                    if (count21 > 0)
                    {
                        string strupdate = "Update EmployeeBatchMaster set Batchmasterid='" + batchid.SelectedValue + "' where [Employeeid]='" + ViewState["empid"] + "'";

                        SqlCommand cmd5 = new SqlCommand(strupdate, con);

                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd5.ExecuteNonQuery();
                        con.Close();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Employee's Batch is updated successfully";


                    }
                }
            }

            else
            {
                int i, count;
                string em = "SELECT StatusName, EmployeeMaster.EmployeeName,EmployeeMaster.Whid,EmployeeMasterID, StatusMaster.StatusId, EmployeeMaster.StatusMasterId FROM EmployeeMaster INNER JOIN StatusMaster ON EmployeeMaster.StatusMasterId = StatusMaster.StatusId where EmployeeMaster.EmployeeMasterId = " + DropDownList3.SelectedValue + " order by EmployeeMaster.EmployeeName";

                SqlCommand cmd3 = new SqlCommand(em, con);
                SqlDataAdapter adap3 = new SqlDataAdapter(cmd3);
                DataTable ds3 = new DataTable();

                adap3.Fill(ds3);
                count = ds3.Rows.Count;

                for (i = 0; i < count; i++)
                {
                    hfidwhid.Value = ds3.Rows[i][2].ToString();
                    hfempid.Value = ds3.Rows[i][3].ToString();
                    hfbmid.Value = ds3.Rows[i][4].ToString();


                    string str1 = " Insert INTO EmployeeBatchMaster(Whid,Employeeid,Batchmasterid)values('" + hfidwhid.Value + "','" + hfempid.Value + "','" + hfbmid.Value + "') ";
                    SqlCommand cmd1 = new SqlCommand(str1, con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully";
                }
                ddlstrname.SelectedIndex = 0;

                CheckBox1.Checked = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }

        else
        {
            int i, count;

            string em = "SELECT distinct StatusName,EmployeeMaster.EmployeeName,EmployeeMaster.Whid,EmployeeMaster.EmployeeMasterID,BatchTiming.BatchMasterId,CONVERT(nvarchar(6),BatchTiming.StartTime) +':'+ CONVERT(nvarchar(6),BatchTiming.EndTime)+':'+ BatchMaster.Name as Name, BatchMaster.ID FROM BatchTiming inner JOIN BatchMaster ON BatchTiming.BatchMasterId = BatchMaster.ID right outer join EmployeeMaster on EmployeeMaster.Whid =BatchMaster.WHID inner join StatusMaster on StatusMaster.StatusId = EmployeeMaster.StatusMasterId where  EmployeeMaster.Whid='" + ddlstrname.SelectedValue + "'  order by EmployeeMaster.EmployeeName";

            SqlCommand cmd3 = new SqlCommand(em, con);
            SqlDataAdapter adap3 = new SqlDataAdapter(cmd3);
            DataTable ds3 = new DataTable();

            //  adap3.Fill(ds3);

            adap3.Fill(ds3);
            count = ds3.Rows.Count;

            string str2 = "Select EmployeeBatchMaster.id  from  EmployeeMaster  inner join  EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID where EmployeeBatchMaster.Employeeid='" + DropDownList3.SelectedValue + "'";

            SqlCommand cmd2 = new SqlCommand(str2, con);

            SqlDataAdapter adap2 = new SqlDataAdapter(cmd2);
            DataTable ds2 = new DataTable();
            adap2.Fill(ds2);
            int count2;
            count2 = ds2.Rows.Count;
            if (count2 > 0)
            {
                int k = 0;
                lblmsg.Visible = true;
                lblmsg.Text = "This Employee is already assigned to one batch";


                foreach (GridViewRow gridemp in this.GridView1.Rows)
                {
                    ViewState["empid"] = (int)GridView1.DataKeys[k].Value;
                    DropDownList batchid = (DropDownList)gridemp.FindControl("DropDownList2");
                    string str21 = "Select distinct EmployeeBatchMaster.[Employeeid]  from  EmployeeMaster  inner join  EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID where EmployeeBatchMaster.Employeeid='" + ViewState["empid"] + "'";
                    SqlCommand cmd21 = new SqlCommand(str2, con);

                    SqlDataAdapter adap21 = new SqlDataAdapter(cmd21);
                    DataTable ds21 = new DataTable();
                    adap2.Fill(ds21);
                    int count21;
                    count21 = ds2.Rows.Count;
                    if (count21 > 0)
                    {
                        string strupdate = "Update EmployeeBatchMaster set Batchmasterid='" + batchid.SelectedValue + "' where [Employeeid]='" + ViewState["empid"] + "'";

                        SqlCommand cmd5 = new SqlCommand(strupdate, con);


                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd5.ExecuteNonQuery();
                        con.Close();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Employee's Batch is updated successfully";


                    }
                }
            }
            else
            {
                for (i = 0; i < count; i++)
                {
                    hfidwhid.Value = ds3.Rows[i]["Whid"].ToString();
                    hfempid.Value = ds3.Rows[i]["EmployeeMasterID"].ToString();
                    hfbmid.Value = ds3.Rows[i]["BatchMasterId"].ToString();


                    string str1 = " Insert INTO EmployeeBatchMaster(Whid,Employeeid,Batchmasterid)values('" + hfidwhid.Value + "','" + hfempid.Value + "','" + hfbmid.Value + "') ";
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully";
                }
                ddlstrname.SelectedIndex = 0;

                CheckBox1.Checked = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
    }
    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            con.Open();
            string em = "SELECT  distinct StatusName,EmployeeMaster.EmployeeName,EmployeeMaster.Whid,EmployeeMaster.EmployeeMasterID,BatchTiming.BatchMasterId,CONVERT(nvarchar(6),BatchTiming.StartTime) +':'+ CONVERT(nvarchar(6),BatchTiming.EndTime)+':'+ BatchMaster.Name as Name, BatchMaster.ID FROM BatchTiming inner join  BatchMaster ON BatchTiming.BatchMasterId = BatchMaster.ID inner  join EmployeeMaster on EmployeeMaster.Whid =BatchMaster.WHID inner join StatusMaster on StatusMaster.StatusId = EmployeeMaster.StatusMasterId where EmployeeMaster.Whid='" + ddlstrname.SelectedValue + "' order by EmployeeMaster.EmployeeName ";

            SqlCommand cmd3 = new SqlCommand(em, con);
            SqlDataAdapter adap3 = new SqlDataAdapter(cmd3);
            DataTable ds3 = new DataTable();

            adap3.Fill(ds3);


            if (ds3.Rows.Count > 0)
            {
                GridView1.DataSource = ds3;
                GridView1.DataBind();

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            con.Close();
            //ddl();
        }
        if (!(CheckBox1.Checked))
        {
            con.Open();
            string em = "SELECT distinct StatusName, EmployeeMaster.EmployeeName, StatusMaster.StatusId, EmployeeMaster.StatusMasterId FROM EmployeeMaster INNER JOIN StatusMaster ON EmployeeMaster.StatusMasterId = StatusMaster.StatusId where EmployeeMaster.EmployeeMasterId = " + DropDownList3.SelectedValue + " order by EmployeeMaster.EmployeeName";

            SqlCommand cmd4 = new SqlCommand(em, con);
            SqlDataAdapter adap4 = new SqlDataAdapter(cmd4);
            DataSet ds4 = new DataSet();
            adap4.Fill(ds4);
            GridView1.DataSource = ds4;
            GridView1.DataBind();
            con.Close();

            // ddl();
        }


    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string em = "SELECT distinct StatusName,EmployeeMaster.EmployeeName,EmployeeBatchMaster.Employeeid,EmployeeBatchMaster.Batchmasterid,EmployeeMaster.Whid,EmployeeMaster.EmployeeMasterID,EmployeeMaster.DesignationMasterId,DepartmentmasterMNC.id,DepartmentmasterMNC.Departmentname,DesignationMaster.DeptID,DesignationMaster.DesignationName,EmployeeMaster.DeptID,BatchTiming.BatchMasterId,CONVERT(nvarchar(6),BatchTiming.StartTime) +':'+ CONVERT(nvarchar(6),BatchTiming.EndTime)+':'+ BatchMaster.Name as Name,BatchMaster.ID,EmployeePayrollMaster.EmpId,EmployeePayrollMaster.EmployeeNo FROM BatchTiming  inner join BatchMaster on BatchTiming.BatchMasterId = BatchMaster.ID  inner join EmployeeBatchMaster on BatchMaster.ID=EmployeeBatchMaster.Batchmasterid inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID  inner join StatusMaster on StatusMaster.StatusId = EmployeeMaster.StatusMasterId  inner join EmployeePayrollMaster on EmployeeMaster.EmployeeMasterID=EmployeePayrollMaster.EmpId inner join DepartmentmasterMNC on EmployeeMaster.DeptID=DepartmentmasterMNC.id inner join DesignationMaster on EmployeeMaster.DeptID=DesignationMaster.DeptID where EmployeeMaster.Whid='" + DropDownList1.SelectedValue + "' and EmployeeMaster.EmployeeMasterID='"+ DropDownList3.SelectedValue +"' order by EmployeeMaster.EmployeeName";
        //SqlCommand cmd3 = new SqlCommand(em, con);
        //SqlDataAdapter adap3 = new SqlDataAdapter(cmd3);
        //DataTable ds3 = new DataTable();

        //adap3.Fill(ds3);


        //if (ds3.Rows.Count > 0)
        //{
        //    GridView1.DataSource = ds3;
        //    GridView1.DataBind();
        //    hfidwhid.Value = ds3.Rows[0][2].ToString();
        //    hfempid.Value = ds3.Rows[0][3].ToString();
        //    hfbmid.Value = ds3.Rows[0][4].ToString();
        //}
        //else
        //{
        //    GridView1.DataSource = null;
        //    GridView1.DataBind();
        //}
    }
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        ddlstrname.SelectedIndex = 0;

        CheckBox1.Checked = false;
        lblmsg.Text = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        btnupdate.Visible = false;
        btncancel.Visible = false;
        if (ddlstrname.SelectedIndex > 0)
        {
            if (DropDownList3.SelectedIndex > 0)
            {
                string str2 = "Select EmployeeBatchMaster.id  from  EmployeeMaster  inner join  EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID where EmployeeBatchMaster.Employeeid='" + DropDownList3.SelectedValue + "'";
                SqlCommand cmd2 = new SqlCommand(str2, con);

                SqlDataAdapter adap2 = new SqlDataAdapter(cmd2);
                DataTable ds2 = new DataTable();
                adap2.Fill(ds2);
                int count2;
                count2 = ds2.Rows.Count;
                if (count2 > 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "This Employee is already assigned to one batch";
                }
                else
                {
                    string str = "insert into EmployeeBatchMaster(Employeeid,Batchmasterid,Whid) values ('" + DropDownList3.SelectedValue + "','" + ddlBatchName.SelectedValue + "','" + ddlstrname.SelectedValue + "') ";
                    SqlCommand cmd1 = new SqlCommand(str, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully";
                    gridfun();
                }
            }
        }

    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        ddlstrname.SelectedIndex = 0;

        CheckBox1.Checked = false;
        lblmsg.Text = "";
        ddlBatchName.SelectedIndex = 0;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // ViewState["id"] = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
        GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
        lblmsg.Text = "";
        string str12 = "select EmployeeBatchMaster.*,EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname,DesignationMaster.DesignationName,EmployeePayrollMaster.EmployeeNo from EmployeeBatchMaster inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID left join EmployeePayrollMaster on EmployeeMaster.EmployeeMasterID=EmployeePayrollMaster.EmpId  inner join DepartmentmasterMNC on EmployeeMaster.DeptID=DepartmentmasterMNC.id inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId  where EmployeeBatchMaster.id='" + GridView1.SelectedIndex + "'";
        SqlCommand cmd1 = new SqlCommand(str12, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            pnladd.Visible = true;
            Button1.Visible = false;
            ViewState["empid"] = ds1.Rows[0]["Employeeid"].ToString();
            if (ds1.Rows[0]["EmployeeNo"] != "" && ds1.Rows[0]["EmployeeNo"] != null)
            {
                lblempno.Text = ds1.Rows[0]["EmployeeNo"].ToString();
            }
            lblemp.Text = ds1.Rows[0]["EmployeeName"].ToString();
            lbldept.Text = ds1.Rows[0]["Departmentname"].ToString();
            lbldesig.Text = ds1.Rows[0]["DesignationName"].ToString();
            //Fillwarehouse();
            ddlstrname.SelectedIndex = ddlstrname.Items.IndexOf(ddlstrname.Items.FindByValue(ds1.Rows[0]["Whid"].ToString()));
            fillstorebatch();
            ddlstrbatchname.SelectedIndex = ddlstrbatchname.Items.IndexOf(ddlstrbatchname.Items.FindByValue(ds1.Rows[0]["Batchmasterid"].ToString()));
            // fillbatch();
            //ddlBatchName.SelectedIndex = ddlBatchName.Items.IndexOf(ddlBatchName.Items.FindByValue(ds1.Rows[0]["Batchmasterid"].ToString()));
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        //if (rblchange.SelectedValue == "1")
        //{
        //    if (ddlstrname.SelectedIndex > 0)
        //    {
        //        string str = "Update EmployeeBatchMaster set Whid='" + ddlstrname.SelectedValue + "' where Employeeid='" + ViewState["empid"] + "'";
        //        SqlCommand cmd1 = new SqlCommand(str, con);
        //        if (con.State.ToString() != "Open")
        //        {
        //            con.Open();
        //        }
        //        cmd1.ExecuteNonQuery();
        //        con.Close();
        //        lblmsg.Visible = true;
        //        lblmsg.Text = "Record updated successfully";
        //        ddlstrname.SelectedIndex = 0;
        //        pnladd.Visible = false;
        //    }
        //}
        //if (rblchange.SelectedValue == "2")
        //{
        //    if (ddlBatchName.SelectedIndex > 0)
        //    {
        //        string str = "Update EmployeeBatchMaster set Batchmasterid='" + ddlstrbatchname.SelectedValue + "' where Employeeid='" + ViewState["empid"] + "'";
        //        SqlCommand cmd1 = new SqlCommand(str, con);
        //        if (con.State.ToString() != "Open")
        //        {
        //            con.Open();
        //        }
        //        cmd1.ExecuteNonQuery();
        //        con.Close();
        //        pnlbatch.Visible = false;
        //        pnlbusiness.Visible = false;
        //        lblmsg.Visible = true;
        //        rblchange.SelectedIndex = -1;
        //        lblmsg.Text = "Record updated successfully";                
        //        pnladd.Visible = false;
        //    }
        //}
        //if (rblchange.SelectedValue == "0")
        //{
        //    if (ddlstrname.SelectedIndex > 0 && ddlBatchName.SelectedIndex > 0)
        //    {
        //        string str = "Update EmployeeBatchMaster set Whid='" + ddlstrname.SelectedValue + "', Batchmasterid='" + ddlBatchName.SelectedValue + "' where Employeeid='" + ViewState["empid"] + "'";
        //        SqlCommand cmd1 = new SqlCommand(str, con);
        //        if (con.State.ToString() != "Open")
        //        {
        //            con.Open();
        //        }
        //        cmd1.ExecuteNonQuery();
        //        con.Close();

        //        string strupdate = "Update EmployeeMaster set Whid='" + ddlstrname.SelectedValue + "' where EmployeeMasterID='" + ViewState["empid"] + "'";
        //        SqlCommand cmdupdate = new SqlCommand(strupdate, con);
        //        if (con.State.ToString() != "Open")
        //        {
        //            con.Open();
        //        }
        //        cmdupdate.ExecuteNonQuery();
        //        con.Close();

        //        SqlDataAdapter adpselectparty = new SqlDataAdapter("Select PartyID from EmployeeMaster where EmployeeMasterID='" + ViewState["empid"] + "'",con);
        //        DataTable dtparty = new DataTable();
        //        adpselectparty.Fill(dtparty);

        //        string strupparty = "Update Party_master set Whid='" + ddlstrname.SelectedValue + "' where PartyID = '" + dtparty.Rows[0]["PartyID"].ToString() + "'";
        //        SqlCommand cmdupparty = new SqlCommand(strupparty, con);
        //        if (con.State.ToString() != "Open")
        //        {
        //            con.Open();
        //        }
        //        cmdupparty.ExecuteNonQuery();
        //        con.Close();
        //        pnlbatch.Visible = false;
        //        pnlbusiness.Visible = false;
        //       // rblchange.SelectedIndex = -1;
        //        lblmsg.Visible = true;
        //        lblmsg.Text = "Record updated successfully";                
        //        pnladd.Visible = false;
        //    }
        //}

        if (ChkYes.Checked == true)
        {
            if (ddlstrbatchname.SelectedIndex > 0)
            {
                string str = "Update EmployeeBatchMaster set Batchmasterid='" + ddlstrbatchname.SelectedValue + "' where Employeeid='" + ViewState["empid"] + "'";
                SqlCommand cmd1 = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();
                pnlbatch.Visible = false;
                ChkYes.Checked = false;
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";
                pnladd.Visible = false;
            }
        }
        gridfun();
        fillfilteremp();
        Button1.Visible = true;

    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        //rblchange.SelectedIndex = -1;
        pnlbatch.Visible = false;
        ChkYes.Checked = false;
        lblmsg.Text = "";

        pnladd.Visible = false;
        Button1.Visible = true;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            gridfun();

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[7].Visible = false;
            }

        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            gridfun();

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }

        }
    }
    protected void ddlfilterstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilterbatch();
        fillfilteremp();
        gridfun();
    }
    protected void ddlfilteremployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridfun();
    }
    protected void ddlfilterbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridfun();
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridfun();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        gridfun();
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
    protected void rblchange_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rblchange.SelectedValue == "1")
        //{
        //    if (pnlbusiness.Visible == false)
        //    {
        //        ddlstrname.SelectedIndex = 0;
        //        ddlBatchName.SelectedIndex = 0;
        //        pnlbusiness.Visible = true;
        //        pnlbatch.Visible = false;
        //    }
        //    if (pnlbusiness.Visible == true)
        //    {
        //        ddlstrname.SelectedIndex = 0;
        //        ddlBatchName.SelectedIndex = 0;
        //        pnlbusiness.Visible = true;
        //        pnlbatch.Visible = false;
        //    }
        //    else
        //    {
        //        pnlbusiness.Visible = false;
        //        pnlbatch.Visible = false;
        //    }
        //}

        //if (rblchange.SelectedValue == "2")
        //{
        //    if (pnlbatch.Visible == false)
        //    {
        //        ddlstrbatchname.Visible = true;
        //        ddlBatchName.Visible = false;
        //        pnlbatch.Visible = true;
        //        pnlbusiness.Visible = false;
        //    }
        //    if (pnlbatch.Visible == true)
        //    {
        //        ddlstrbatchname.Visible = true;
        //        ddlBatchName.Visible = false;
        //        pnlbatch.Visible = true;
        //        pnlbusiness.Visible = false;
        //    }
        //    else
        //    {
        //        pnlbatch.Visible = false;
        //        pnlbusiness.Visible = false;
        //    }
        //}
        //if (rblchange.SelectedValue == "0")
        //{

        //    ddlstrbatchname.Visible = false;
        //    ddlBatchName.Visible = true;
        //    pnlbusiness.Visible = true;
        //    pnlbatch.Visible = true;

        //}


    }

    protected void ChkYes_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkYes.Checked == true)
        {
            pnlbatch.Visible = true;
        }
        else
        {
            pnlbatch.Visible = false;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        gridfun();
    }
}