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

public partial class Add_Batch_Master : System.Web.UI.Page
{

    // SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection conn;
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        compid = Session["comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        if (!Page.IsPostBack)
        {

            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            lblmsg.Text = "";

            fillstore();
            Fillddltimezone();

            //Fillddlstore();

            Fillgridtimezone();


            btnupdate.Visible = false;
            btnsubmit.Visible = true;
            lblmsg.Visible = false;
            txtstartdate.Text = System.DateTime.Now.ToShortDateString();
            txtenddate.Text = System.DateTime.Now.AddYears(5).ToShortDateString();
            ddlstrname_SelectedIndexChanged(sender, e);
        }
    }
    protected void Fillddltimezone()
    {
        lblmsg.Text = "";

        string str = "select WHTimeZone.ID as WId,WHTimeZone.TimeZone,TimeZoneMaster.Name+':'+TimeZoneMaster.ShortName+':'+TimeZoneMaster.gmt AS BatchTimeZone from WHTimeZone inner join TimeZoneMaster on TimeZoneMaster.ID=WHTimeZone.TimeZone where WHTimeZone.compid='" + Session["Comid"].ToString() + "' and WHID='" + ddlstrname.SelectedValue + "'  ";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            ddltimezone.DataSource = ds;
            ddltimezone.DataTextField = "BatchTimeZone";
            ddltimezone.DataValueField = "WId";
            ddltimezone.DataBind();
            //ddltimezone.Items.Insert(0, "--Select--");

        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtstartdate.Text) == System.DateTime.Now.Date)
        {
            if (Convert.ToDateTime(txtenddate.Text) > Convert.ToDateTime(txtstartdate.Text))
            {
                Insertdata();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "The end date must be greater than the start date";
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "The start date cannot be earlier than today and by default is current date";
        }
    }


    protected void Fillgridtimezone()
    {
        lblCompany.Text = Session["Cname"].ToString();
        lblBusiness.Text = "All";

        string str = "  [BatchMaster].ID,case when(BatchMaster.Status='1') then 'Active' else 'Inactive' end as Status,WareHouseMaster.Name as WName,[BatchMaster].Name,TimeZoneMaster.[Name]+':'+TimeZoneMaster.ShortName+':'+TimeZoneMaster.gmt as TimeFormat from WareHouseMaster " +
                   " inner join [BatchMaster] on [BatchMaster].WHID = WareHouseMaster.WareHouseId " +
                   " inner join WHTimeZone on WHTimeZone.Id=[BatchMaster].BatchTimeZone " +
                   " inner join [TimeZoneMaster] on TimeZoneMaster.ID=WHTimeZone.TimeZone  where [WareHouseMaster].Status='1' and CompanyId='" + Session["comid"].ToString() + "'";
                    //order by WName, Name,TimeFormat ";

        string str2 = " select count(BatchMaster.ID) as ci from  WareHouseMaster" +
                     " inner join [BatchMaster] on [BatchMaster].WHID = WareHouseMaster.WareHouseId " +
                   " inner join WHTimeZone on WHTimeZone.Id=[BatchMaster].BatchTimeZone " +
                   " inner join [TimeZoneMaster] on TimeZoneMaster.ID=WHTimeZone.TimeZone  where [WareHouseMaster].Status='1' and CompanyId='" + Session["comid"].ToString() + "'";

        if (DropDownList2.SelectedIndex > 0)
        {
            str = "  [BatchMaster].ID,case when(BatchMaster.Status='1') then 'Active' else 'Inactive' end as Status,WareHouseMaster.Name as WName,[BatchMaster].Name,TimeZoneMaster.[Name]+':'+TimeZoneMaster.ShortName+':'+TimeZoneMaster.gmt as TimeFormat from WareHouseMaster " +
                  " inner join [BatchMaster] on [BatchMaster].WHID = WareHouseMaster.WareHouseId " +
                  " inner join WHTimeZone on  WHTimeZone.Id=[BatchMaster].BatchTimeZone " +
                  " inner join [TimeZoneMaster] on TimeZoneMaster.ID=WHTimeZone.TimeZone where BatchMaster.WHID='" + DropDownList2.SelectedValue + "'";

            str2 = " select count(BatchMaster.ID) as ci from  WareHouseMaster" +
                     " inner join [BatchMaster] on [BatchMaster].WHID = WareHouseMaster.WareHouseId " +
                  " inner join WHTimeZone on  WHTimeZone.Id=[BatchMaster].BatchTimeZone " +
                  " inner join [TimeZoneMaster] on TimeZoneMaster.ID=WHTimeZone.TimeZone where BatchMaster.WHID='" + DropDownList2.SelectedValue + "'";
        }

        lblBusiness.Text = DropDownList2.SelectedItem.Text;


        //SqlCommand cmd = new SqlCommand(str, conn);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,[BatchMaster].Name asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt1 = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str);

            DataView myDataView = new DataView();
            myDataView = dt1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataSource = myDataView;
            GridView1.DataBind();
        }

        foreach (GridViewRow gdr in GridView1.Rows)
        {
            Label lblbatchid = (Label)gdr.FindControl("lblbatchid");
            Label lblschedule = (Label)gdr.FindControl("lblschedule");
            SqlDataAdapter adpt = new SqlDataAdapter("select BatchMaster.Name,TimeSchedulMaster.SchedulName,BatchTiming.StartTime,BatchTiming.EndTime from BatchMaster inner join BatchTiming on BatchMaster.ID = BatchTiming.BatchMasterId inner join TimeSchedulMaster on TimeSchedulMaster.id = BatchTiming.TimeScheduleMasterId where BatchTiming.BatchMasterId = '" + lblbatchid.Text + "' and BatchTiming.Active = '1'", conn);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string schedule = "";
                foreach (DataRow dr in dt.Rows)
                {
                    schedule = dr["SchedulName"] + " : " + dr["StartTime"] + " - " + dr["EndTime"];
                    schedule = schedule + "," + "<br>";

                }
                lblschedule.Text = schedule;
                string st = lblschedule.Text.Substring(0, lblschedule.Text.Length - 5);
                lblschedule.Text = st;
            }
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
        SqlCommand cmd = new SqlCommand(qu, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        AddBatch.Visible = true;
        btnaddbatchmaster.Visible = false;

        lbladd.Text = "Edit Batch";
        int j = 0;
        //ddlstrname.Enabled = false;
        if (GridView1.Rows.Count == 1)
        {
            j = Convert.ToInt32(GridView1.DataKeys[0].Value);
        }
        else
        {
            ImageButton lk = (ImageButton)sender;
            //LinkButton lk = (LinkButton)sender;
            j = Convert.ToInt32(lk.CommandArgument);
        }
        ViewState["Id"] = j;
        Session["TimeId"] = j;

        string str = "select * from BatchMaster where ID='" + ViewState["Id"] + "'";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        adp.Fill(dt);

        string st1 = "select WHTimeZone.ID as WId,WHTimeZone.TimeZone,TimeZoneMaster.Name+':'+TimeZoneMaster.ShortName+':'+TimeZoneMaster.gmt AS BatchTimeZone from WHTimeZone inner join TimeZoneMaster on TimeZoneMaster.ID=WHTimeZone.TimeZone where Whid='" + j + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        ddlstrname.SelectedValue = dt.Rows[0]["WHID"].ToString();
        Session["strid"] = ddlstrname.SelectedValue;
        RadioButtonList1.Items[0].Text = "Set this as a default batch for all employees of the selected business " + ddlstrname.SelectedItem.Text;
        RadioButtonList1.Items[1].Text = "Set this as a default batch for all employees of the department you are going to select";
        RadioButtonList1.Items[2].Text = "Set this as a default batch for all employees of the designation you are going to select";
        lblbusidept.Text = ddlstrname.SelectedItem.Text;
        lbldesibusi.Text = ddlstrname.SelectedItem.Text;
        selectbatch();

        ddltimezone.SelectedIndex = ddltimezone.Items.IndexOf(ddltimezone.Items.FindByValue(Convert.ToString(dt.Rows[0]["BatchTimeZone"])));



        txttimename.Text = Convert.ToString(dt.Rows[0]["Name"]);
        ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(dt.Rows[0]["Status"].ToString()));


        //string ck = Convert.ToString(dt.Rows[0]["Status"]);


        //if (ck == "True")
        //{
        //    chkactive.Checked = true;

        //}
        //else
        //{
        //    chkactive.Checked = false;
        //}

        ViewState["DefaultBatchId"] = Convert.ToString(dt.Rows[0]["DefaultBatchId"]);
        chkyes.Checked = true;
        chkyes_CheckedChanged(sender, e);

        RadioButtonList1.SelectedValue = Convert.ToString(dt.Rows[0]["DefaultBatchId"]);

        if (ViewState["DefaultBatchId"].ToString() != "")
        {
            if (Convert.ToInt32(ViewState["DefaultBatchId"]) == 1)
            {
                //  pnldepartment.Visible = true;
                // pnldesignation.Visible = false;

                ModalPopupExtender1.Hide();
                ModalPopupExtenderAddnew.Show();



                string streditdept = " select DepartmentmasterMNC.* from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where DepartmentmasterMNC.Whid='" + ddlstrname.SelectedValue + "' order by Departmentname ";
                SqlCommand cmdeditdept = new SqlCommand(streditdept, conn);
                SqlDataAdapter adpeditdept = new SqlDataAdapter(cmdeditdept);
                DataTable dteditdept = new DataTable();
                adpeditdept.Fill(dteditdept);

                grddepartment.DataSource = dteditdept;
                grddepartment.DataBind();

                foreach (GridViewRow gdr in grddepartment.Rows)
                {
                    Label lbldepartmasterid = (Label)gdr.FindControl("lbldepartmasterid");
                    CheckBox chkactive123 = (CheckBox)gdr.FindControl("chkactive123");
                    // DropDownList ddlstatusdept = (DropDownList)gdr.FindControl("ddlstatusdept");

                    string streditdept123 = " select * from BatchByDefault where DepartmentId='" + lbldepartmasterid.Text + "' and BatchId='" + ViewState["Id"] + "' ";
                    SqlCommand cmdeditdept123 = new SqlCommand(streditdept123, conn);
                    SqlDataAdapter adpeditdept123 = new SqlDataAdapter(cmdeditdept123);
                    DataTable dteditdept123 = new DataTable();
                    adpeditdept123.Fill(dteditdept123);
                    if (dteditdept123.Rows.Count > 0)
                    {

                        if (Convert.ToBoolean(dteditdept123.Rows[0]["Status"]) == true)
                        {
                            chkactive123.Checked = true;
                            //ddlstatusdept.SelectedIndex = ddlstatusdept.Items.IndexOf(ddlstatusdept.Items.FindByValue("1"));
                        }
                        else
                        {
                            chkactive123.Checked = false;
                            //ddlstatusdept.SelectedIndex = ddlstatusdept.Items.IndexOf(ddlstatusdept.Items.FindByValue("0"));
                        }
                    }
                    else
                    {
                        chkactive123.Checked = false;
                    }

                }


            }
            if (Convert.ToInt32(ViewState["DefaultBatchId"]) == 2)
            {


                //  pnldepartment.Visible = false;
                //  pnldesignation.Visible = true;
                ModalPopupExtender1.Show();
                ModalPopupExtenderAddnew.Hide();

                //btnsubmitpop1.Visible = false;
                //btnsubmitpop2.Visible = false;

                //btnupdatepop1.Visible = true;
                //btnupdatepop2.Visible = true;

                string streditdept = " select DesignationMaster.*,DepartmentmasterMNC.Departmentname,DepartmentmasterMNC.id from DesignationMaster inner join  DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where DepartmentmasterMNC.Whid='" + ddlstrname.SelectedValue + "' order by Departmentname , DesignationName ";
                SqlCommand cmdeditdept = new SqlCommand(streditdept, conn);
                SqlDataAdapter adpeditdept = new SqlDataAdapter(cmdeditdept);
                DataTable dteditdept = new DataTable();
                adpeditdept.Fill(dteditdept);

                grddesignation.DataSource = dteditdept;
                grddesignation.DataBind();

                foreach (GridViewRow gdr in grddesignation.Rows)
                {
                    Label lbldesignationmasterid = (Label)gdr.FindControl("lbldesignationmasterid");
                    CheckBox chkactivedesign123 = (CheckBox)gdr.FindControl("chkactivedesign123");
                    //DropDownList ddlstatusdesi = (DropDownList)gdr.FindControl("ddlstatusdesi");

                    string streditdept123 = " select * from BatchByDefault where DesignationId='" + lbldesignationmasterid.Text + "' and BatchId='" + ViewState["Id"] + "' ";
                    SqlCommand cmdeditdept123 = new SqlCommand(streditdept123, conn);
                    SqlDataAdapter adpeditdept123 = new SqlDataAdapter(cmdeditdept123);
                    DataTable dteditdept123 = new DataTable();
                    adpeditdept123.Fill(dteditdept123);

                    if (dteditdept123.Rows.Count > 0)
                    {
                        if (Convert.ToString(dteditdept123.Rows[0]["Status"]) != "")
                        {

                            if (Convert.ToBoolean(dteditdept123.Rows[0]["Status"]) == true)
                            {
                                chkactivedesign123.Checked = true;
                                // ddlstatusdesi.SelectedIndex = ddlstatusdesi.Items.IndexOf(ddlstatusdesi.Items.FindByValue("1"));
                            }
                            else
                            {
                                chkactivedesign123.Checked = false;
                                // ddlstatusdesi.SelectedIndex = ddlstatusdesi.Items.IndexOf(ddlstatusdesi.Items.FindByValue("0"));
                            }
                        }
                    }
                    else
                    {
                        chkactivedesign123.Checked = false;
                    }

                }

            }

        }
        btnsubmitpop1.Visible = false;
        btnsubmitpop2.Visible = false;

        btnupdatepop1.Visible = true;
        btnupdatepop2.Visible = true;


        btnupdate.Visible = true;
        btnsubmit.Visible = false;
        CheckBox1.Visible = false;
        //btnsubmitpop1.Visible = false;
        //btnsubmitpop2.Visible = false;

        //btnupdatepop1.Visible = true;
        //btnupdatepop2.Visible = true;

    }
    protected void ImageButton48_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtstartdate.Text) == System.DateTime.Now.Date)
        {
            if (Convert.ToDateTime(txtenddate.Text) > Convert.ToDateTime(txtstartdate.Text))
            {
                updatedata();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "The end date must be greater than the start date";
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "The start date cannot be earlier than today and by default is current date";
        }

        btnsubmitpop1.Visible = true;
        btnupdatepop1.Visible = false;
        btnsubmitpop2.Visible = true;
        btnupdatepop2.Visible = false;

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;

        Fillgridtimezone();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Fillgridtimezone();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st1 = "select BatchMasterId from BatchTiming where BatchMasterId= '" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        string st3 = "select BatchMasterId from BatchWorkingday where BatchMasterId='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
        SqlCommand cmd3 = new SqlCommand(st3, conn);
        SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);

        DataTable dt3 = new DataTable();
        adp3.Fill(dt3);
        if (dt1.Rows.Count > 0 || dt3.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You are unable to delete this batch as there is a schedule attached to it. Please delete " + "<a href=\"BatchTimingManage.aspx\" style=\"font-size:14px; color:red; \" target=\"_blank\">" + "the batch timings " + "</a>and " + "<a href=\"BatchworkingDay.aspx\" style=\"font-size:14px; color:red;\" target=\"_blank\">" + "the batch weekly schedule " + "</a>before deleting the master batch record.";

        }
        else
        {
            lblmsg.Text = "";
            string st2 = "Delete from BatchMaster where BatchMaster.ID='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
            SqlCommand cmd2 = new SqlCommand(st2, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd2.ExecuteNonQuery();
            conn.Close();
            GridView1.EditIndex = -1;
            Fillgridtimezone();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully ";
        }

    }

    protected void Fillddlstore()
    {
        string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "' and [WareHouseMaster].Status='1' order by Name";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Name";
        DropDownList2.DataValueField = "WareHouseId";
        DropDownList2.DataBind();
        //ddltimezone.Items.Insert(0, "--Select--");
        DropDownList2.Items.Insert(0, "All");

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

        Fillgridtimezone();


    }

    protected void ddlstrname_SelectedIndexChanged(object sender, EventArgs e)
    {

        RadioButtonList1.Items[0].Text = "Set this as a default batch for all employees of the selected business " + ddlstrname.SelectedItem.Text;
        RadioButtonList1.Items[1].Text = "Set this as a default batch for all employees of the department you are going to select";
        RadioButtonList1.Items[2].Text = "Set this as a default batch for all employees of the designation you are going to select";
        lblbusidept.Text = ddlstrname.SelectedItem.Text;
        lbldesibusi.Text = ddlstrname.SelectedItem.Text;

        if (ddlstrname.SelectedIndex > 0)
        {
            lblmsg.Text = "";
            string str = "select WHTimeZone.ID as WId,WHTimeZone.TimeZone,TimeZoneMaster.Name+':'+TimeZoneMaster.ShortName+':'+TimeZoneMaster.gmt AS BatchTimeZone from WHTimeZone inner join TimeZoneMaster on TimeZoneMaster.ID=WHTimeZone.TimeZone where Whid='" + ddlstrname.SelectedValue + "' ";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ddltimezone.DataSource = ds;
            ddltimezone.DataTextField = "BatchTimeZone";
            ddltimezone.DataValueField = "WId";
            ddltimezone.DataBind();
            //ddltimezone.Items.Insert(0, "-Select-");
            //ddltimezone.Items[0].Value = "0";
        }
        else
        {
            ddltimezone.Items.Clear();
            Fillddltimezone();
            //ddlemployee.Items.Insert(0, "--Select--");
            //ddlemployee.SelectedItem.Value = "0";
            Fillgridtimezone();
        }
    }
    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        btnsubmit.Visible = true;
        AddBatch.Visible = false;
        btnaddbatchmaster.Visible = true;
        clearall();
        CheckBox1.Visible = true;
        lbladd.Text = "";
        btnsubmitpop1.Visible = true;
        btnupdatepop1.Visible = false;
        btnsubmitpop2.Visible = true;
        btnupdatepop2.Visible = false;
    }
    protected void cleartxt()
    {
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
        lblmsg.Text = "";
        Fillddltimezone();
        ddlstrname.SelectedIndex = 0;
        txttimename.Text = "";
        ddltimezone.SelectedIndex = 0;
        //chkactive.Checked = true;
        ddlstatus.SelectedIndex = 0;
    }
    protected void selectbatch()
    {

        lblmsg.Text = "";
        string str = "select WHTimeZone.ID as WId,WHTimeZone.TimeZone,TimeZoneMaster.Name+':'+TimeZoneMaster.ShortName+':'+TimeZoneMaster.gmt AS BatchTimeZone from WHTimeZone inner join TimeZoneMaster on TimeZoneMaster.ID=WHTimeZone.TimeZone where WHTimeZone.WHID='" + Session["strid"] + "' ";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddltimezone.DataSource = ds;
        ddltimezone.DataTextField = "BatchTimeZone";
        ddltimezone.DataValueField = "WId";
        ddltimezone.DataBind();
        ddltimezone.Items.Insert(0, "-Select-");
        ddltimezone.Items[0].Value = "0";

    }
    protected void clearall()
    {
        ddlstatus.SelectedIndex = 0;
        txttimename.Text = "";
        //chkactive.Checked = false;
        ddlstrname.Enabled = true;
        ddlstrname.SelectedIndex = 0;
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
        chkyes.Checked = false;
        RadioButtonList1.SelectedIndex = -1;
        RadioButtonList1.Visible = false;
        RadioButtonList1.Items[0].Text = "Set this as a default batch for all employees of the selected business " + ddlstrname.SelectedItem.Text;
        RadioButtonList1.Items[1].Text = "Set this as a default batch for all employees of the department you are going to select";
        RadioButtonList1.Items[2].Text = "Set this as a default batch for all employees of the designation you are going to select";
        lblbusidept.Text = ddlstrname.SelectedItem.Text;
        lbldesibusi.Text = ddlstrname.SelectedItem.Text;
        //  pnldepartment.Visible = false;
        //pnldesignation.Visible = false;
    }




    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Fillgridtimezone();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        Fillgridtimezone();
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


        DataTable ds = ClsStore.SelectStorename();
        ddlstrname.DataSource = ds;
        ddlstrname.DataTextField = "Name";
        ddlstrname.DataValueField = "WareHouseId";
        ddlstrname.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstrname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Name";
        DropDownList2.DataValueField = "WareHouseId";
        DropDownList2.DataBind();

        DropDownList2.Items.Insert(0, "All");
        DropDownList2.Items[0].Value = "0";

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            Fillgridtimezone();

            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[7].Visible = false;
            }

        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(200);

            GridView1.AllowPaging = true;
            GridView1.PageSize = 20;
            Fillgridtimezone();

            Button2.Text = "Printable Version";
            Button1.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            // pnldepartment.Visible = true;
            // pnldesignation.Visible = false;

            //btnsubmit.Visible = false;
            ModalPopupExtender1.Hide();
            ModalPopupExtenderAddnew.Show();

            string str = " select DepartmentmasterMNC.* from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where DepartmentmasterMNC.Whid='" + ddlstrname.SelectedValue + "'  order by Departmentname ";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            grddepartment.DataSource = dt;
            grddepartment.DataBind();

        }
        else if (RadioButtonList1.SelectedValue == "2")
        {
            // pnldepartment.Visible = false;
            // pnldesignation.Visible = true;
            //btnsubmit.Visible = false;
            ModalPopupExtenderAddnew.Hide();
            ModalPopupExtender1.Show();

            string str = " select DesignationMaster.*,DepartmentmasterMNC.Departmentname,DepartmentmasterMNC.id from DesignationMaster inner join  DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where DepartmentmasterMNC.Whid='" + ddlstrname.SelectedValue + "' order by Departmentname, DesignationName ";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            grddesignation.DataSource = dt;
            grddesignation.DataBind();

        }
        else
        {
            //   pnldepartment.Visible = false;
            //  pnldesignation.Visible = false;
            //btnsubmit.Visible = true;
            ModalPopupExtenderAddnew.Hide();
            ModalPopupExtender1.Hide();
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtenderAddnew.Hide();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void btnsubmitpop1_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtstartdate.Text) == System.DateTime.Now.Date)
        {
            if (Convert.ToDateTime(txtenddate.Text) > Convert.ToDateTime(txtstartdate.Text))
            {
                Insertdata();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "The end date must be greater than the start date";
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "The start date cannot be earlier than today and by default is current date";
        }


    }
    protected void btnsubmitpop2_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtstartdate.Text) == System.DateTime.Now.Date)
        {
            if (Convert.ToDateTime(txtenddate.Text) > Convert.ToDateTime(txtstartdate.Text))
            {
                Insertdata();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "The end date must be greater than the start date";
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "The start date cannot be earlier than today and by default is current date";
        }



    }
    protected void Insertdata()
    {
        string st1 = "select * from BatchMaster where WHID='" + ddlstrname.SelectedValue + "' and Name='" + txttimename.Text + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }
        else
        {
            lblmsg.Text = "";

            String str = "Insert Into BatchMaster (Name,WHID,Status,BatchTimeZone,CompanyId,DefaultBatchId,EffectiveStartDate,EffectiveEndDate)values('" + txttimename.Text + "','" + ddlstrname.SelectedValue + "','" + ddlstatus.SelectedValue + "','" + ddltimezone.SelectedValue + "','" + Session["Comid"] + "','" + RadioButtonList1.SelectedValue + "','" + txtstartdate.Text + "','" + txtenddate.Text + "')";

            SqlCommand cmd = new SqlCommand(str, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmd.ExecuteNonQuery();
            conn.Close();

            string str123 = "select Max(ID) as BatchMasterId from BatchMaster ";
            DataTable ds123 = new DataTable();
            SqlDataAdapter da123 = new SqlDataAdapter(str123, conn);
            da123.Fill(ds123);

            ViewState["BatchMasterId"] = ds123.Rows[0]["BatchMasterId"].ToString();

            if (RadioButtonList1.SelectedValue == "1")
            {
                foreach (GridViewRow gdr in grddepartment.Rows)
                {
                    Label lbldepartmasterid = (Label)gdr.FindControl("lbldepartmasterid");
                    CheckBox chkactive123 = (CheckBox)gdr.FindControl("chkactive123");
                    //DropDownList ddlstatusdept = (DropDownList)gdr.FindControl("ddlstatusdept");
                    if (chkactive123.Checked == true)
                    {

                        String strdept = "Insert Into BatchByDefault (BatchId,DepartmentId,Status)values('" + ViewState["BatchMasterId"] + "','" + lbldepartmasterid.Text + "','" + chkactive123.Checked + "')";

                        SqlCommand cmddept = new SqlCommand(strdept, conn);
                        if (conn.State.ToString() != "Open")
                        {
                            conn.Open();
                        }
                        cmddept.ExecuteNonQuery();
                        conn.Close();
                    }

                }

            }
            if (RadioButtonList1.SelectedValue == "2")
            {
                foreach (GridViewRow gdr in grddesignation.Rows)
                {
                    Label lbldesignationmasterid = (Label)gdr.FindControl("lbldesignationmasterid");
                    CheckBox chkactivedesign123 = (CheckBox)gdr.FindControl("chkactivedesign123");
                    //DropDownList ddlstatusdesi = (DropDownList)gdr.FindControl("ddlstatusdesi");
                    if (chkactivedesign123.Checked == true)
                    {

                        String strdept = "Insert Into BatchByDefault (BatchId,DesignationId,Status)values('" + ViewState["BatchMasterId"] + "','" + lbldesignationmasterid.Text + "','" + chkactivedesign123.Checked + "')";

                        SqlCommand cmddept = new SqlCommand(strdept, conn);
                        if (conn.State.ToString() != "Open")
                        {
                            conn.Open();
                        }
                        cmddept.ExecuteNonQuery();
                        conn.Close();
                    }


                }


            }



            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
            Fillgridtimezone();
            clearall();
            if (CheckBox1.Checked == true)
            {
                string te = "BatchTimingManage.aspx?id=" + ViewState["BatchMasterId"];
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            }
            AddBatch.Visible = false;
            btnaddbatchmaster.Visible = true;


            lbladd.Text = "";

        }

    }

    protected void updatedata()
    {
        string st1 = "select * from BatchMaster where WHID='" + ddlstrname.SelectedValue + "' and Name='" + txttimename.Text + "' and ID !='" + Session["TimeId"] + "'";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }
        else
        {

            string st2 = "select * from BatchTiming where BatchMasterId= '" + Session["TimeId"] + "' ";
            SqlCommand cmd2 = new SqlCommand(st2, conn);
            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
            DataSet ds1 = new DataSet();
            DataTable dt2 = new DataTable();
            adp2.Fill(dt2);

            string st3 = "select * from BatchWorkingday where BatchMasterId='" + Session["TimeId"] + "'";
            SqlCommand cmd3 = new SqlCommand(st3, conn);
            SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);
            DataSet ds3 = new DataSet();
            DataTable dt3 = new DataTable();
            adp3.Fill(dt3);

            string st4 = "select * from PolicyEmployee where BatchMasterId='" + Session["TimeId"] + "'";
            SqlCommand cmd4 = new SqlCommand(st4, conn);
            SqlDataAdapter adp4 = new SqlDataAdapter(cmd4);
            DataSet ds4 = new DataSet();
            DataTable dt4 = new DataTable();
            adp4.Fill(dt4);

            if (dt2.Rows.Count > 0 || dt3.Rows.Count > 0 || dt4.Rows.Count > 0)
            {

                lblmsg.Visible = true;
                //lblmsg.Text = "Sorry,This record is exists in Batch Timing & Batch Workingday";
                lblmsg.Text = "You are unable to edit this batch as there is a schedule attached to it. Please delete " + "<a href=\"BatchTimingManage.aspx\" style=\"font-size:14px; color:red; \" target=\"_blank\">" + "the batch timings " + "</a>and " + "<a href=\"BatchworkingDay.aspx\" style=\"font-size:14px; color:red;\" target=\"_blank\">" + "the batch weekly schedule " + "</a>before editing the master batch record.";
            }
            else
            {

                lblmsg.Text = "";
                string str = "UPDATE BatchMaster set [Name]='" + txttimename.Text + "',WHID='" + ddlstrname.SelectedValue + "',Status='" + ddlstatus.SelectedValue + "',BatchTimeZone='" + ddltimezone.SelectedValue + "' ,DefaultBatchId='" + RadioButtonList1.SelectedValue + "', EffectiveStartDate = '" + txtstartdate.Text + "', EffectiveEndDate = '" + txtenddate.Text + "' WHERE ID='" + ViewState["Id"] + "'";
                SqlCommand cmd = new SqlCommand(str, conn);
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                conn.Close();


                if (ViewState["DefaultBatchId"].ToString() == RadioButtonList1.SelectedValue)
                {
                    if (ViewState["DefaultBatchId"].ToString() == "1")
                    {
                        foreach (GridViewRow gdr in grddepartment.Rows)
                        {
                            Label lbldepartmasterid = (Label)gdr.FindControl("lbldepartmasterid");
                            CheckBox chkactive123 = (CheckBox)gdr.FindControl("chkactive123");
                            //DropDownList ddlstatusdept = (DropDownList)gdr.FindControl("ddlstatusdept");

                            string streditdept123 = " select * from BatchByDefault where DepartmentId='" + lbldepartmasterid.Text + "' and BatchId='" + ViewState["Id"] + "' ";
                            SqlCommand cmdeditdept123 = new SqlCommand(streditdept123, conn);
                            SqlDataAdapter adpeditdept123 = new SqlDataAdapter(cmdeditdept123);
                            DataTable dteditdept123 = new DataTable();
                            adpeditdept123.Fill(dteditdept123);


                            if (dteditdept123.Rows.Count > 0)
                            {
                                string strupdate = "UPDATE BatchByDefault set BatchId='" + ViewState["Id"] + "',DepartmentId='" + lbldepartmasterid.Text + "',Status='" + chkactive123.Checked + "'";
                                SqlCommand cmdupdate = new SqlCommand(strupdate, conn);
                                if (conn.State.ToString() != "Open")
                                {
                                    conn.Open();
                                }
                                cmdupdate.ExecuteNonQuery();
                                conn.Close();



                            }
                            else
                            {
                                String strdept = "Insert Into BatchByDefault (BatchId,DepartmentId,Status)values('" + ViewState["Id"] + "','" + lbldepartmasterid.Text + "','" + chkactive123.Checked + "')";

                                SqlCommand cmddept = new SqlCommand(strdept, conn);
                                if (conn.State.ToString() != "Open")
                                {
                                    conn.Open();
                                }
                                cmddept.ExecuteNonQuery();
                                conn.Close();


                            }

                        }
                    }
                    else if (ViewState["DefaultBatchId"].ToString() == "2")
                    {
                        foreach (GridViewRow gdr in grddesignation.Rows)
                        {
                            Label lbldesignationmasterid = (Label)gdr.FindControl("lbldesignationmasterid");
                            CheckBox chkactivedesign123 = (CheckBox)gdr.FindControl("chkactivedesign123");
                            //DropDownList ddlstatusdesi = (DropDownList)gdr.FindControl("ddlstatusdesi");

                            string streditdept123 = " select * from BatchByDefault where DesignationId='" + lbldesignationmasterid.Text + "' and BatchId='" + ViewState["Id"] + "' ";
                            SqlCommand cmdeditdept123 = new SqlCommand(streditdept123, conn);
                            SqlDataAdapter adpeditdept123 = new SqlDataAdapter(cmdeditdept123);
                            DataTable dteditdept123 = new DataTable();
                            adpeditdept123.Fill(dteditdept123);


                            if (dteditdept123.Rows.Count > 0)
                            {
                                string strupdate = "UPDATE BatchByDefault set BatchId='" + ViewState["Id"] + "',DesignationId='" + lbldesignationmasterid.Text + "',Status='" + chkactivedesign123.Checked + "'";
                                SqlCommand cmdupdate = new SqlCommand(strupdate, conn);
                                if (conn.State.ToString() != "Open")
                                {
                                    conn.Open();
                                }
                                cmdupdate.ExecuteNonQuery();
                                conn.Close();



                            }
                            else
                            {
                                String strdept = "Insert Into BatchByDefault (BatchId,DesignationId,Status)values('" + ViewState["Id"] + "','" + lbldesignationmasterid.Text + "','" + chkactivedesign123.Checked + "')";

                                SqlCommand cmddept = new SqlCommand(strdept, conn);
                                if (conn.State.ToString() != "Open")
                                {
                                    conn.Open();
                                }
                                cmddept.ExecuteNonQuery();
                                conn.Close();


                            }

                        }

                    }
                }
                else
                {
                    if (RadioButtonList1.SelectedValue == "1")
                    {
                        String strdelete = " Delete from BatchByDefault where BatchId='" + ViewState["Id"] + "' ";

                        SqlCommand cmddelete = new SqlCommand(strdelete, conn);
                        if (conn.State.ToString() != "Open")
                        {
                            conn.Open();
                        }
                        cmddelete.ExecuteNonQuery();
                        conn.Close();

                        foreach (GridViewRow gdr in grddepartment.Rows)
                        {
                            Label lbldepartmasterid = (Label)gdr.FindControl("lbldepartmasterid");
                            CheckBox chkactive123 = (CheckBox)gdr.FindControl("chkactive123");
                            //DropDownList ddlstatusdept = (DropDownList)gdr.FindControl("ddlstatusdept");



                            if (RadioButtonList1.SelectedValue == "1")
                            {

                                String strdept = "Insert Into BatchByDefault (BatchId,DepartmentId,Status)values('" + ViewState["Id"] + "','" + lbldepartmasterid.Text + "','" + chkactive123.Checked + "')";

                                SqlCommand cmddept = new SqlCommand(strdept, conn);
                                if (conn.State.ToString() != "Open")
                                {
                                    conn.Open();
                                }
                                cmddept.ExecuteNonQuery();
                                conn.Close();
                            }

                        }
                    }
                    else if (RadioButtonList1.SelectedValue == "2")
                    {
                        String strdelete = " Delete from BatchByDefault where BatchId='" + ViewState["Id"] + "' ";

                        SqlCommand cmddelete = new SqlCommand(strdelete, conn);
                        if (conn.State.ToString() != "Open")
                        {
                            conn.Open();
                        }
                        cmddelete.ExecuteNonQuery();
                        conn.Close();

                        foreach (GridViewRow gdr in grddesignation.Rows)
                        {
                            Label lbldesignationmasterid = (Label)gdr.FindControl("lbldesignationmasterid");
                            CheckBox chkactivedesign123 = (CheckBox)gdr.FindControl("chkactivedesign123");
                            //DropDownList ddlstatusdesi = (DropDownList)gdr.FindControl("ddlstatusdesi");


                            if (RadioButtonList1.SelectedValue == "2")
                            {

                                String strdept = "Insert Into BatchByDefault (BatchId,DesignationId,Status)values('" + ViewState["Id"] + "','" + lbldesignationmasterid.Text + "','" + chkactivedesign123.Checked + "')";

                                SqlCommand cmddept = new SqlCommand(strdept, conn);
                                if (conn.State.ToString() != "Open")
                                {
                                    conn.Open();
                                }
                                cmddept.ExecuteNonQuery();
                                conn.Close();
                            }

                        }

                    }

                }

                GridView1.EditIndex = -1;
                Fillgridtimezone();
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";
                Fillgridtimezone();
                clearall();
                btnsubmit.Visible = true;
                btnupdate.Visible = false;

                AddBatch.Visible = false;
                btnaddbatchmaster.Visible = true;

                lbladd.Text = "";
                CheckBox1.Visible = true;
            }

        }

    }
    protected void btnupdatepop1_Click(object sender, EventArgs e)
    {

        if (Convert.ToDateTime(txtstartdate.Text) == System.DateTime.Now.Date)
        {
            if (Convert.ToDateTime(txtenddate.Text) > Convert.ToDateTime(txtstartdate.Text))
            {
                updatedata();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "The end date must be greater than the start date";
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "The start date cannot be earlier than today and by default is current date";
        }


        btnsubmitpop1.Visible = true;
        btnupdatepop1.Visible = false;
        btnsubmitpop2.Visible = true;
        btnupdatepop2.Visible = false;

    }
    protected void btnupdatepop2_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtstartdate.Text) == System.DateTime.Now.Date)
        {
            if (Convert.ToDateTime(txtenddate.Text) > Convert.ToDateTime(txtstartdate.Text))
            {
                updatedata();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "The end date must be greater than the start date";
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "The start date cannot be earlier than today and by default is current date";
        }

        btnsubmitpop2.Visible = true;
        btnupdatepop2.Visible = false;
        btnsubmitpop1.Visible = true;
        btnupdatepop1.Visible = false;



    }
    protected void btnaddbatchmaster_Click(object sender, EventArgs e)
    {

        if (AddBatch.Visible == true)
        {
            lblmsg.Text = "";
            AddBatch.Visible = false;
            btnaddbatchmaster.Visible = true;
            lbladd.Text = "";
            CheckBox1.Visible = true;
        }
        else
        {
            lblmsg.Text = "";
            AddBatch.Visible = true;
            btnaddbatchmaster.Visible = false;
            lbladd.Text = "Add New Batch";
            if (GridView1.Rows.Count == 1)
            {
                if (ViewState["Id"] == null)
                {
                    LinkButton4_Click(sender, e);
                }
                else
                {
                    btnupdate.Visible = false;
                    btnsubmit.Visible = true;

                }

            }
            else
            {
                btnupdate.Visible = false;
                btnsubmit.Visible = true;
            }
        }

    }
    protected void chkyes_CheckedChanged(object sender, EventArgs e)
    {
        if (chkyes.Checked == true)
        {
            RadioButtonList1.Visible = true;
        }
        else if (chkyes.Checked == false)
        {
            RadioButtonList1.Visible = false;
        }

    }
}
