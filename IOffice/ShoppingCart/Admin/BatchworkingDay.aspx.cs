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
using System.Globalization;

public partial class Add_Batch_working_Day : System.Web.UI.Page
{
    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection conn;

    DayOfWeek dayweek;
    dynamic d;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        if (!Page.IsPostBack)
        {
            //GetWeekNumber(Convert.ToDateTime(System.DateTime.Now.ToString()));



            ViewState["sortOrder"] = "";
            lblCompany0.Text = "All";
            lblCompany.Text = Session["Cname"].ToString();


            fillstore();
            ddlstorename_SelectedIndexChanged(sender, e);



            Fillddlbatchname();

            Fillddlmonday();
            Fillddltuesday();
            Fillddlwednesday();
            Fillddlthursday();
            Fillddlfriday();
            Fillddlsaturday();

            Fillddlsunday();

            Fillgridday();

            ImageButton49.Visible = false;
            ImageButton2.Visible = true;
            lblmsg.Text = "";
            edit();
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
            {
                SqlDataAdapter adpt = new SqlDataAdapter("select BatchMaster.ID,BatchMaster.WHID,WareHouseMaster.WareHouseId from BatchMaster inner join WareHouseMaster on BatchMaster.WHID = WareHouseMaster.WareHouseId where BatchMaster.ID ='" + Request.QueryString["id"] + "'", conn);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddlstorename.SelectedIndex = ddlstorename.Items.IndexOf(ddlstorename.Items.FindByValue(dt.Rows[0]["WareHouseId"].ToString()));
                    Fillddlbatchname();
                    ddlbatchname.SelectedIndex = ddlbatchname.Items.IndexOf(ddlbatchname.Items.FindByValue(dt.Rows[0]["ID"].ToString()));
                    ddlbatchname_SelectedIndexChanged(sender, e);
                    Fillddlmonday();
                    Fillddltuesday();
                    Fillddlwednesday();
                    Fillddlthursday();
                    Fillddlfriday();
                    Fillddlsaturday();
                    Fillddlsunday();
                    chkmoday.Checked = true;
                    pnlMonday.Visible = true;
                    //chkmoday_CheckedChanged(sender, e);

                    chktuesday.Checked = true;
                    pnlTuesday.Visible = true;
                    //chktuesday_CheckedChanged(sender, e);

                    chkwednseday.Checked = true;
                    pnlWednesday.Visible = true;
                    //chkwednseday_CheckedChanged(sender, e);

                    chkthursday.Checked = true;
                    pnlThursday.Visible = true;
                    //chkthursday_CheckedChanged(sender, e);
                    chkfriday.Checked = true;
                    pnlFriday.Visible = true;
                    //chkfriday_CheckedChanged(sender, e);

                    chksaturday.Checked = false;
                    chksaturday_CheckedChanged(sender, e);

                    chksunday.Checked = false;
                    chksunday_CheckedChanged(sender, e);
                    lblmsg.Text = "";
                    if (pnladd.Visible == false)
                    {
                        pnladd.Visible = true;
                    }
                    lbladd.Text = "Add New Weekly Schedule";
                    btnadd.Visible = false;
                }
            }
        }
    }

    //public static int GetWeekNumber(DateTime dtPassed)
    //{
    //    CultureInfo ciCurr = CultureInfo.CurrentCulture;
    //    int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

    //    var lastDay = new DateTime(DateTime.Now.Year, 1, 1).AddDays((weekNum) * 7);
    //    var firstDay = lastDay.AddDays(-6);

    //    return weekNum;
    //}

    //protected void getweekdates()
    //{

    //}

    protected void Fillddlmonday()
    {
        ddlmonday.Items.Clear();
        string str = "select distinct BatchTiming.Id, TimeSchedulMaster.SchedulName +' : '+Left(cast(BatchTiming.StartTime as nvarchar(50)),5) +' : '+Left(cast(BatchTiming.EndTime as nvarchar(50)),5)  as SchedulName from TimeSchedulMaster  inner join [BatchTiming] ON [BatchTiming].TimeScheduleMasterId=TimeSchedulMaster.id where [BatchTiming].Active=1 and BatchTiming.compid='" + Session["Comid"].ToString() + "' and BatchTiming.BatchMasterId='" + ddlbatchname.SelectedValue + "'  and BatchTiming.Whid='" + ddlstorename.SelectedValue + "' and BatchTiming.Active='1'";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlmonday.DataSource = ds;
            ddlmonday.DataTextField = "SchedulName";
            ddlmonday.DataValueField = "ID";
            ddlmonday.DataBind();
        }

        ddlmonday.Items.Insert(0, "-Select-");
        ddlmonday.Items[0].Value = "0";

    }
    protected void Fillddltuesday()
    {
        ddltuesday.Items.Clear();
        string str = "select distinct BatchTiming.Id, TimeSchedulMaster.SchedulName +' : '+Left(cast(BatchTiming.StartTime as nvarchar(50)),5) +' : '+Left(cast(BatchTiming.EndTime as nvarchar(50)),5)  as SchedulName from TimeSchedulMaster  inner join [BatchTiming] ON [BatchTiming].TimeScheduleMasterId=TimeSchedulMaster.id where [BatchTiming].Active=1 and BatchTiming.compid='" + Session["Comid"].ToString() + "' and BatchTiming.BatchMasterId='" + ddlbatchname.SelectedValue + "' and BatchTiming.Whid='" + ddlstorename.SelectedValue + "' and BatchTiming.Active='1'";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddltuesday.DataSource = ds;
            ddltuesday.DataTextField = "SchedulName";
            ddltuesday.DataValueField = "ID";
            ddltuesday.DataBind();
        }

        ddltuesday.Items.Insert(0, "-Select-");
        ddltuesday.Items[0].Value = "0";

    }
    protected void Fillddlwednesday()
    {
        ddlwednesday.Items.Clear();
        string str = "select distinct BatchTiming.Id, TimeSchedulMaster.SchedulName +' : '+Left(cast(BatchTiming.StartTime as nvarchar(50)),5) +' : '+Left(cast(BatchTiming.EndTime as nvarchar(50)),5)  as SchedulName from TimeSchedulMaster  inner join [BatchTiming] ON [BatchTiming].TimeScheduleMasterId=TimeSchedulMaster.id where [BatchTiming].Active=1 and BatchTiming.compid='" + Session["Comid"].ToString() + "' and BatchTiming.BatchMasterId='" + ddlbatchname.SelectedValue + "' and BatchTiming.Whid='" + ddlstorename.SelectedValue + "' and BatchTiming.Active='1'";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlwednesday.DataSource = ds;
            ddlwednesday.DataTextField = "SchedulName";
            ddlwednesday.DataValueField = "ID";
            ddlwednesday.DataBind();
        }

        ddlwednesday.Items.Insert(0, "-Select-");
        ddlwednesday.Items[0].Value = "0";

    }
    protected void Fillddlthursday()
    {
        ddlthursday.Items.Clear();
        string str = "select distinct BatchTiming.Id, TimeSchedulMaster.SchedulName +' : '+Left(cast(BatchTiming.StartTime as nvarchar(50)),5) +' : '+Left(cast(BatchTiming.EndTime as nvarchar(50)),5)  as SchedulName from TimeSchedulMaster  inner join [BatchTiming] ON [BatchTiming].TimeScheduleMasterId=TimeSchedulMaster.id where [BatchTiming].Active=1 and BatchTiming.compid='" + Session["Comid"].ToString() + "' and BatchTiming.BatchMasterId='" + ddlbatchname.SelectedValue + "' and BatchTiming.Whid='" + ddlstorename.SelectedValue + "' and BatchTiming.Active='1'";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlthursday.DataSource = ds;
            ddlthursday.DataTextField = "SchedulName";
            ddlthursday.DataValueField = "ID";
            ddlthursday.DataBind();
        }

        ddlthursday.Items.Insert(0, "-Select-");
        ddlthursday.Items[0].Value = "0";

    }
    protected void Fillddlfriday()
    {
        ddlfriday.Items.Clear();
        string str = "select distinct BatchTiming.Id, TimeSchedulMaster.SchedulName +' : '+Left(cast(BatchTiming.StartTime as nvarchar(50)),5) +' : '+Left(cast(BatchTiming.EndTime as nvarchar(50)),5)  as SchedulName from TimeSchedulMaster  inner join [BatchTiming] ON [BatchTiming].TimeScheduleMasterId=TimeSchedulMaster.id where [BatchTiming].Active=1 and BatchTiming.compid='" + Session["Comid"].ToString() + "' and BatchTiming.BatchMasterId='" + ddlbatchname.SelectedValue + "' and BatchTiming.Whid='" + ddlstorename.SelectedValue + "' and BatchTiming.Active='1'";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlfriday.DataSource = ds;
            ddlfriday.DataTextField = "SchedulName";
            ddlfriday.DataValueField = "ID";
            ddlfriday.DataBind();
        }

        ddlfriday.Items.Insert(0, "-Select-");
        ddlfriday.Items[0].Value = "0";

    }
    protected void Fillddlsaturday()
    {
        ddlsaturday.Items.Clear();
        string str = "select distinct BatchTiming.Id, TimeSchedulMaster.SchedulName +' : '+Left(cast(BatchTiming.StartTime as nvarchar(50)),5) +' : '+Left(cast(BatchTiming.EndTime as nvarchar(50)),5)  as SchedulName from TimeSchedulMaster  inner join [BatchTiming] ON [BatchTiming].TimeScheduleMasterId=TimeSchedulMaster.id where [BatchTiming].Active=1 and BatchTiming.compid='" + Session["Comid"].ToString() + "' and BatchTiming.BatchMasterId='" + ddlbatchname.SelectedValue + "' and BatchTiming.Whid='" + ddlstorename.SelectedValue + "' and BatchTiming.Active='1'";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlsaturday.DataSource = ds;
            ddlsaturday.DataTextField = "SchedulName";
            ddlsaturday.DataValueField = "ID";
            ddlsaturday.DataBind();
            ddlsaturday.Items.Insert(0, "-Select-");
            ddlsaturday.Items[0].Value = "0";
        }
        else
        {
            ddlsaturday.Items.Insert(0, "-Select-");
            ddlsaturday.Items[0].Value = "0";
        }
    }
    protected void Fillddlsunday()
    {
        ddlsunday.Items.Clear();
        string str = "select distinct BatchTiming.Id, TimeSchedulMaster.SchedulName +' : '+Left(cast(BatchTiming.StartTime as nvarchar(50)),5) +' : '+Left(cast(BatchTiming.EndTime as nvarchar(50)),5)  as SchedulName from TimeSchedulMaster  inner join [BatchTiming] ON [BatchTiming].TimeScheduleMasterId=TimeSchedulMaster.id where [BatchTiming].Active=1 and BatchTiming.compid='" + Session["Comid"].ToString() + "' and BatchTiming.BatchMasterId='" + ddlbatchname.SelectedValue + "' and BatchTiming.Whid='" + ddlstorename.SelectedValue + "' and BatchTiming.Active='1'";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlsunday.DataSource = ds;
            ddlsunday.DataTextField = "SchedulName";
            ddlsunday.DataValueField = "ID";
            ddlsunday.DataBind();
            ddlsunday.Items.Insert(0, "-Select-");
            ddlsunday.Items[0].Value = "0";
        }
        else
        {
            ddlsunday.Items.Insert(0, "-Select-");
            ddlsunday.Items[0].Value = "0";
        }
    }

    protected void Fillddlbatchname()
    {

        string str = "select ID,Name,EffectiveStartDate from BatchMaster where WHID='" + ddlstorename.SelectedValue + "' and Status='" + 1 + "' order by Name ";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlbatchname.DataSource = ds;
        ddlbatchname.DataTextField = "Name";
        ddlbatchname.DataValueField = "ID";
        ddlbatchname.DataBind();
        ddlbatchname.Items.Insert(0, "-Select-");
        ddlbatchname.Items[0].Value = "0";
    }
    protected void Fillgridday()
    {
        string str = "";
        string str1 = "";

        lblCompany0.Text = DropDownList1.SelectedItem.Text;
        lblfilstatus.Text = ddlfilterstatus.SelectedItem.Text;

        if (DropDownList1.SelectedIndex > 0)
        {
            str1 += " and BatchWorkingDay.Whid='" + DropDownList1.SelectedValue + "'";
        }
        if (ddlfilterstatus.SelectedIndex > 0)
        {
            str1 += " and BatchMaster.Status = '" + ddlfilterstatus.SelectedValue + "'";
        }

        str = "  BatchWorkingDay.*,BatchMaster.ID as BId,BatchMaster.Name as BName,WareHouseMaster.WareHouseId as WId,WareHouseMaster.Name +' : '+ TimeZoneMaster.Name +'('+ TimeZoneMaster.gmt +')' as WName,(t.SchedulName +'<br/>'+ left(b.starttime,5) +'-'+ left(b.endtime,5)) as d,(t1.SchedulName +'<br/>'+ left(b1.starttime,5) +'-'+ left(b1.endtime,5)) as d1, (t2.SchedulName +'<br/>'+ left(b2.starttime,5) +'-'+ left(b2.endtime,5)) as d2, (t3.SchedulName +'<br/>'+ left(b3.starttime,5) +'-'+ left(b3.endtime,5)) as d3, (t4.SchedulName +'<br/>'+ left(b4.starttime,5) +'-'+ left(b4.endtime,5)) as d4,(t5.SchedulName +'<br/>'+ left(b5.starttime,5) +'-'+ left(b5.endtime,5)) as d5,(t6.SchedulName +'<br/>'+ left(b6.starttime,5) +'-'+ left(b6.endtime,5)) as d6 from BatchWorkingDay  INNER JOIN BatchMaster ON BatchMaster.ID=BatchWorkingDay.BatchMasterId  	INNER JOIN WareHouseMaster ON WareHouseMaster.WareHouseId=BatchWorkingDay.Whid inner join WHTimeZone on  WHTimeZone.Id=[BatchMaster].BatchTimeZone inner join TimeZoneMaster on TimeZoneMaster.ID = WHTimeZone.TimeZone	Left join [BatchTiming] as b on b.ID=[BatchWorkingDay].[MondayScheduleId] 	Left join [BatchTiming] as b1 on b1.ID=[BatchWorkingDay].TuesdayScheduleId	Left join [BatchTiming] as b2 on b2.ID=[BatchWorkingDay].[WednesdayScheduleId] 	Left join [BatchTiming] as b3 on b3.ID=[BatchWorkingDay].ThursdayScheduleId  	Left join [BatchTiming] as b4 on b4.ID=[BatchWorkingDay].FridayScheduleId  	Left join [BatchTiming] as b5 on b5.ID=[BatchWorkingDay].SaturdayScheduleId  	Left join [BatchTiming] as b6 on b6.ID=[BatchWorkingDay].SundayScheduleId 	Left join [TimeSchedulMaster] as t on t.id=b.TimeScheduleMasterId		  Left join [TimeSchedulMaster] as t1 on t1.id=b1.TimeScheduleMasterId  	   Left join [TimeSchedulMaster] as t2 on t2.id=b2.TimeScheduleMasterId       Left join [TimeSchedulMaster] as t3 on t3.id=b3.TimeScheduleMasterId	  	Left join [TimeSchedulMaster] as t4 on t4.id=b4.TimeScheduleMasterId		Left join [TimeSchedulMaster] as t5 on t5.id=b5.TimeScheduleMasterId	Left join [TimeSchedulMaster] as t6 on t6.id=b6.TimeScheduleMasterId where [WareHouseMaster].Status='1' and BatchWorkingDay.compid='" + Session["Comid"].ToString() + "' " + str1 + "  ";

        string str2 = " select count(BatchWorkingDay.ID) as ci from BatchWorkingDay  INNER JOIN BatchMaster ON BatchMaster.ID=BatchWorkingDay.BatchMasterId  	INNER JOIN WareHouseMaster ON WareHouseMaster.WareHouseId=BatchWorkingDay.Whid inner join WHTimeZone on  WHTimeZone.Id=[BatchMaster].BatchTimeZone inner join TimeZoneMaster on TimeZoneMaster.ID = WHTimeZone.TimeZone	Left join [BatchTiming] as b on b.ID=[BatchWorkingDay].[MondayScheduleId] 	Left join [BatchTiming] as b1 on b1.ID=[BatchWorkingDay].TuesdayScheduleId	Left join [BatchTiming] as b2 on b2.ID=[BatchWorkingDay].[WednesdayScheduleId] 	Left join [BatchTiming] as b3 on b3.ID=[BatchWorkingDay].ThursdayScheduleId  	Left join [BatchTiming] as b4 on b4.ID=[BatchWorkingDay].FridayScheduleId  	Left join [BatchTiming] as b5 on b5.ID=[BatchWorkingDay].SaturdayScheduleId  	Left join [BatchTiming] as b6 on b6.ID=[BatchWorkingDay].SundayScheduleId 	Left join [TimeSchedulMaster] as t on t.id=b.TimeScheduleMasterId		  Left join [TimeSchedulMaster] as t1 on t1.id=b1.TimeScheduleMasterId  	   Left join [TimeSchedulMaster] as t2 on t2.id=b2.TimeScheduleMasterId       Left join [TimeSchedulMaster] as t3 on t3.id=b3.TimeScheduleMasterId	  	Left join [TimeSchedulMaster] as t4 on t4.id=b4.TimeScheduleMasterId		Left join [TimeSchedulMaster] as t5 on t5.id=b5.TimeScheduleMasterId	Left join [TimeSchedulMaster] as t6 on t6.id=b6.TimeScheduleMasterId where [WareHouseMaster].Status='1' and BatchWorkingDay.compid='" + Session["Comid"].ToString() + "' " + str1 + "  ";

        gridworkingday.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,BatchMaster.Name asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt1 = GetDataPage(gridworkingday.PageIndex, gridworkingday.PageSize, sortExpression, str);

            DataView myDataView = new DataView();
            myDataView = dt1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            gridworkingday.DataSource = myDataView;
            gridworkingday.DataBind();
        }
        else
        {
            gridworkingday.DataSource = null;
            gridworkingday.DataBind();
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

    protected void gridworkingday_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridworkingday.EditIndex = -1;
        Fillgridday();
    }

    //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void gridworkingday_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label batchid = (Label)gridworkingday.Rows[e.RowIndex].FindControl("lblBatchId");
        ViewState["BatchId"] = batchid.Text;
        string str = "Select EmployeeBatchMaster.[Batchmasterid],EmployeeBatchMaster.Employeeid from EmployeeBatchMaster inner join BatchWorkingDay on BatchWorkingDay.BatchMasterId=EmployeeBatchMaster.Batchmasterid where  EmployeeBatchMaster.[Batchmasterid]='" + ViewState["BatchId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);


        if (ds.Tables[0].Rows.Count == 0)
        {
            string strdatecomnyholiday = "Select Company_Holiday.Date,DateMasterTbl.DateId from Company_Holiday inner join DateMasterTbl on DateMasterTbl.Date= Company_Holiday.Date where Company_Holiday.Company_Id='" + Session["comid"] + "'";
            SqlCommand cmddatecomnyholiday = new SqlCommand(strdatecomnyholiday, conn);
            SqlDataAdapter adpcmddatecomnyholiday = new SqlDataAdapter(cmddatecomnyholiday);
            DataSet dscmddatecomnyholiday = new DataSet();
            adpcmddatecomnyholiday.Fill(dscmddatecomnyholiday);
            //  int k = 0;
            int count = dscmddatecomnyholiday.Tables[0].Rows.Count;
            for (; count > 0; count--)
            {
                if (count != 0)
                {
                    string strCompnyHoliday = "Delete from BatchWorkingDays where DateMasterID='" + dscmddatecomnyholiday.Tables[0].Rows[count - 1]["DateId"] + "'";
                    SqlCommand cmdCompnyHoliday = new SqlCommand(strCompnyHoliday, conn);
                    if (conn.State.ToString() != "Open")
                    {
                        conn.Open();
                    }
                    cmdCompnyHoliday.ExecuteNonQuery();
                    conn.Close();
                }
            }


            string st2 = "Delete from BatchWorkingDay where BatchWorkingDay.ID='" + gridworkingday.DataKeys[e.RowIndex].Value.ToString() + "' ";
            SqlCommand cmd2 = new SqlCommand(st2, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd2.ExecuteNonQuery();
            conn.Close();

            string st22 = "Delete from Batch_WorkingDays where Batch_WorkingDays.BatchId='" + gridworkingday.DataKeys[e.RowIndex].Value.ToString() + "' ";
            SqlCommand cmd22 = new SqlCommand(st22, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd22.ExecuteNonQuery();
            conn.Close();
            gridworkingday.EditIndex = -1;
            Fillgridday();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";

        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "sorry, you can not allow to delete this Batch Working , Because this batch has employees";
        }



        //cleartxt();



    }

    public void edit()
    {

        ViewState["BatchId"] = ddlbatchname.SelectedValue;
        string str = "select BatchWorkingDay.* from BatchWorkingDay  where  BatchWorkingDay.BatchMasterId='" + ddlbatchname.SelectedValue + "'";

        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)

        //string str = "Select EmployeeBatchMaster.[Batchmasterid],EmployeeBatchMaster.Employeeid from EmployeeBatchMaster inner join BatchWorkingDay on BatchWorkingDay.BatchMasterId=EmployeeBatchMaster.Batchmasterid where  EmployeeBatchMaster.[Batchmasterid]='" + ViewState["BatchId"].ToString() + "'";
        //SqlCommand cmd = new SqlCommand(str, conn);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);


        //if (ds.Tables[0].Rows.Count == 0)
        {
            ViewState["Id"] = Convert.ToString(ds.Rows[0]["Id"]);
            lblmsg.Text = "";
            ImageButton2.Visible = true;
            ImageButton49.Visible = false;
            ImageButton48.Visible = true;
            string str11 = " select BatchWorkingDay.*,BatchMaster.ID as BId,BatchMaster.Name as BName,WareHouseMaster.WareHouseId as WId,WareHouseMaster.Name as WName  from BatchWorkingDay " +
                    " INNER JOIN BatchMaster ON BatchMaster.ID=BatchWorkingDay.BatchMasterId " +
                    " INNER JOIN WareHouseMaster ON WareHouseMaster.WareHouseId=BatchWorkingDay.Whid " +
          " where [WareHouseMaster].Status='1' and BatchWorkingDay.compid='" + Session["Comid"].ToString() + "' and BatchWorkingDay.Id='" + ViewState["Id"] + "' ";

            //String st = "select * from AssociateAdminLoginInfoTbl where ID='" + j + "' ";
            //string str11 = "SELECT * from BatchWorkingDay where ID='" + ViewState["Id"] + "'  ";

            SqlCommand cmd11 = new SqlCommand(str11, conn);
            SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);

            DataTable dt = new DataTable();

            adp11.Fill(dt);



            // ddlstorename.SelectedIndex = ddlstorename.Items.IndexOf(ddlstorename.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));

            // Fillddlbatchname();

            // ddlbatchname.SelectedIndex = ddlbatchname.Items.IndexOf(ddlbatchname.Items.FindByValue(dt.Rows[0]["BatchMasterId"].ToString()));

            //Fillddlfriday();
            //Fillddlmonday();
            //Fillddltuesday();
            //Fillddlwednesday();
            //Fillddlthursday();
            //Fillddlfriday();
            //Fillddlsaturday();
            //Fillddlsunday();
            EventArgs e = new EventArgs();
            object sender = new object();
            string ck1 = dt.Rows[0]["Monday"].ToString();
            if (ck1 == "True")
            {
                chkmoday.Checked = true;

            }
            else { chkmoday.Checked = false; }
            chkmoday_CheckedChanged(sender, e);
            string ck2 = dt.Rows[0]["Tuesday"].ToString();
            if (ck2 == "True") { chktuesday.Checked = true; } else { chktuesday.Checked = false; }
            chktuesday_CheckedChanged(sender, e);
            string ck3 = dt.Rows[0]["Wednesday"].ToString();
            if (ck3 == "True") { chkwednseday.Checked = true; } else { chkwednseday.Checked = false; }
            chkwednseday_CheckedChanged(sender, e);
            string ck4 = dt.Rows[0]["Thursday"].ToString();
            if (ck4 == "True") { chkthursday.Checked = true; } else { chkthursday.Checked = false; }
            chkthursday_CheckedChanged(sender, e);
            string ck5 = dt.Rows[0]["Friday"].ToString();
            if (ck5 == "True") { chkfriday.Checked = true; } else { chkfriday.Checked = false; }
            chkfriday_CheckedChanged(sender, e);
            string ck6 = dt.Rows[0]["Saturday"].ToString();
            if (ck6 == "True") { chksaturday.Checked = true; } else { chksaturday.Checked = false; }
            chksaturday_CheckedChanged(sender, e);
            string ck7 = dt.Rows[0]["Sunday"].ToString();
            if (ck7 == "True") { chksunday.Checked = true; } else { chksunday.Checked = false; }
            chksunday_CheckedChanged(sender, e);
            ddlmonday.SelectedIndex = ddlmonday.Items.IndexOf(ddlmonday.Items.FindByValue(dt.Rows[0]["MondayScheduleId"].ToString()));

            ddltuesday.SelectedIndex = ddltuesday.Items.IndexOf(ddltuesday.Items.FindByValue(dt.Rows[0]["TuesdayScheduleId"].ToString()));
            ddlwednesday.SelectedIndex = ddlwednesday.Items.IndexOf(ddlwednesday.Items.FindByValue(dt.Rows[0]["WednesdayScheduleId"].ToString()));
            ddlthursday.SelectedIndex = ddlthursday.Items.IndexOf(ddlthursday.Items.FindByValue(dt.Rows[0]["ThursdayScheduleId"].ToString()));
            ddlfriday.SelectedIndex = ddlfriday.Items.IndexOf(ddlfriday.Items.FindByValue(dt.Rows[0]["FridayScheduleId"].ToString()));
            ddlsaturday.SelectedIndex = ddlsaturday.Items.IndexOf(ddlsaturday.Items.FindByValue(dt.Rows[0]["SaturdayScheduleId"].ToString()));
            ddlsunday.SelectedIndex = ddlsunday.Items.IndexOf(ddlsunday.Items.FindByValue(dt.Rows[0]["SundayScheduleId"].ToString()));
            // ddllastdayofweek.SelectedValue = dt.Rows[0]["LastDayOftheWeek"].ToString();


            string str12 = "Select * from Day";
            SqlCommand cmd12 = new SqlCommand(str12, conn);
            SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
            DataTable ds12 = new DataTable();
            adp12.Fill(ds12);
            ddllastdayofweek.DataSource = ds12;
            ddllastdayofweek.DataTextField = "day";
            ddllastdayofweek.DataValueField = "ID";


            ddllastdayofweek.DataBind();
            ddllastdayofweek.SelectedIndex = ddllastdayofweek.Items.IndexOf(ddllastdayofweek.Items.FindByText(dt.Rows[0]["LastDayOftheWeek"].ToString()));

            ImageButton49.Visible = false;
            ImageButton2.Visible = true;
            ImageButton48.Visible = true;
        }
        else
        {
            lblmsg.Text = "";
            ImageButton2.Visible = true;
            ImageButton49.Visible = false;
            ImageButton48.Visible = true;
            //lblmsg.Visible = true;
            // lblmsg.Text = "Sorry, you can not allow to edit this Batch Working , Because this batch has employees";
        }
    }

    //protected void ImageButton49_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    //protected void ImageButton48_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void cleartxt()
    {

        ddlbatchname.SelectedIndex = 0;
        ddlstorename.SelectedIndex = 0;
        ddlmonday.SelectedIndex = 0;
        ddltuesday.SelectedIndex = 0;
        ddlwednesday.SelectedIndex = 0;
        ddlthursday.SelectedIndex = 0;
        ddlfriday.SelectedIndex = 0;
        ddlsaturday.SelectedIndex = 0;
        ddlsunday.SelectedIndex = 0;
        chkmoday.Checked = false;
        chktuesday.Checked = false;
        chkwednseday.Checked = false;
        chkthursday.Checked = false;
        chkfriday.Checked = false;
        chksaturday.Checked = false;
        chksunday.Checked = false;
        ImageButton49.Visible = false;
        ImageButton2.Visible = true;
        pnlworking.Visible = false;
        ddllastdayofweek.SelectedIndex = 4;
    }
    protected void ddlstorename_SelectedIndexChanged(object sender, EventArgs e)
    {


        Fillddlbatchname();
        //ddlbatchname_SelectedIndexChanged(sender, e);        
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {

        ModalPopupExtender1222.Show();
        //}
    }
    protected void ImageButton49_Click(object sender, EventArgs e)
    {
        int days = 0;
        String day = "";
        bool chk = false;

        string st1 = "select * from BatchWorkingDay where Whid='" + ddlstorename.SelectedValue + "' and BatchMasterId='" + ddlbatchname.SelectedValue + "'  and ID!='" + ViewState["editid"] + "'";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        adp1.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }
        else
        {

            string str11 = "Select ReportPeriod.StartDate from ReportPeriod where  ReportPeriod.Whid='" + ddlstorename.SelectedValue + "' and ReportPeriod.Active='1'";
            SqlCommand cmd11 = new SqlCommand(str11, conn);
            SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
            DataSet ds11 = new DataSet();
            adp11.Fill(ds11);
            DateTime date = Convert.ToDateTime(ds11.Tables[0].Rows[0]["StartDate"]);
            int year = Convert.ToDateTime(ds11.Tables[0].Rows[0]["StartDate"]).Year;
            day = date.DayOfWeek.ToString();
            if (ds11.Tables[0].Rows.Count > 0)
            {
                if (year % 4 == 0)
                {
                    days = 366;
                }
                else
                {
                    days = 365;
                }



                for (int i = 0; i < days; i++)
                {

                    day = date.DayOfWeek.ToString();


                    if (chkmoday.Checked)
                    {
                        if (day == "Monday")
                            chk = true;

                    }
                    else
                    {
                        ddlmonday.SelectedIndex = 0;
                    }
                    if (chktuesday.Checked)
                    {
                        if (day == "Tuesday")
                            chk = true;

                    }
                    else
                    {
                        ddltuesday.SelectedIndex = 0;
                    }
                    if (chkwednseday.Checked)
                    {
                        if (day == "Wednesday")
                            chk = true;

                    }
                    else
                    {
                        ddlwednesday.SelectedIndex = 0;
                    }
                    if (chkthursday.Checked)
                    {
                        if (day == "Thursday")
                            chk = true;
                    }
                    else
                    {
                        ddlthursday.SelectedIndex = 0;
                    }
                    if (chkfriday.Checked)
                    {
                        if (day == "Friday")
                            chk = true;

                    }
                    else
                    {
                        ddlfriday.SelectedIndex = 0;
                    }
                    if (chksaturday.Checked)
                    {
                        if (day == "Saturday")
                            chk = true;

                    }
                    else
                    {
                        ddlsaturday.SelectedIndex = 0;
                    }
                    if (chksunday.Checked)
                    {
                        if (day == "Sunday")
                            chk = true;

                    }
                    else
                    {
                        ddlsunday.SelectedIndex = 0;
                    }

                    if (chk)
                    {

                        DataTable ds1222 = select("Select DateMasterTbl.Date , DateMasterTbl.DateId,DateMasterTbl.day from DateMasterTbl where DateMasterTbl.Date='" + date + "' and DateMasterTbl.day='" + day + "'");

                        if (ds1222.Rows.Count == 1)
                        {
                            DataTable dtach = select("Select * from BatchWorkingDays where DateMasterID ='" + ds1222.Rows[0]["DateId"] + "' and BatchID='" + ddlbatchname.SelectedValue + "'");
                            //inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID 
                            if (dtach.Rows.Count == 0)
                            {
                                string strbatch = "Insert  Into BatchWorkingDays(DateMasterID,BatchID,Monday,MondayScheduleId,Tuesday,TuesdayscheduleId,Wednesday,WednesdayscheduleId,Thursday,ThursdayscheduleId,Friday,FridayscheduleId,Saturday,SaturdayscheduleId,Sunday,SundayscheduleId)values('" + ds1222.Rows[0]["DateId"] + "','" + ddlbatchname.SelectedValue + "','" + chkmoday.Checked + "','" + ddlmonday.SelectedValue + "','" + chktuesday.Checked + "','" + ddltuesday.SelectedValue + "','" + chkwednseday.Checked + "','" + ddlwednesday.SelectedValue + "','" + chkthursday.Checked + "','" + ddlthursday.SelectedValue + "','" + chkfriday.Checked + "','" + ddlfriday.SelectedValue + "','" + chksaturday.Checked + "','" + ddlsaturday.SelectedValue + "','" + chksunday.Checked + "','" + ddlsunday.SelectedValue + "')";
                                SqlCommand cmd12 = new SqlCommand(strbatch, conn);
                                if (conn.State.ToString() != "Open")
                                {
                                    conn.Open();

                                }

                                cmd12.ExecuteNonQuery();
                                conn.Close();
                            }
                            else
                            {
                                string strbatch = "Update  BatchWorkingDays set Monday='" + chkmoday.Checked + "',MondayScheduleId='" + ddlmonday.SelectedValue + "',Tuesday='" + chktuesday.Checked + "',TuesdayscheduleId='" + ddltuesday.SelectedValue + "',Wednesday='" + chkwednseday.Checked + "'," +
                                    " WednesdayscheduleId='" + ddlwednesday.SelectedValue + "',Thursday='" + chkthursday.Checked + "',ThursdayscheduleId='" + ddlthursday.SelectedValue + "', " +
                                    " Friday='" + chkfriday.Checked + "',FridayscheduleId='" + ddlfriday.SelectedValue + "',Saturday='" + chksaturday.Checked + "', " +
                                    " SaturdayscheduleId='" + ddlsaturday.SelectedValue + "',Sunday='" + chksunday.Checked + "'," +
                                    " SundayscheduleId='" + ddlsunday.SelectedValue + "' where BatchID='" + ViewState["BD"] + "'";
                                SqlCommand cmd12 = new SqlCommand(strbatch, conn);
                                if (conn.State.ToString() != "Open")
                                {
                                    conn.Open();

                                }

                                cmd12.ExecuteNonQuery();
                                conn.Close();
                            }

                        }
                    }
                    date = date.AddDays(1);
                    chk = false;
                }

            }


            string str = "UPDATE BatchWorkingDay set Whid='" + ddlstorename.SelectedValue + "',BatchMasterId='" + ddlbatchname.SelectedValue + "',Monday='" + chkmoday.Checked + "',MondayScheduleId='" + ddlmonday.SelectedValue + "',Tuesday='" + chktuesday.Checked + "',TuesdayscheduleId='" + ddltuesday.SelectedValue + "',Wednesday='" + chkwednseday.Checked + "',WednesdayscheduleId='" + ddlwednesday.SelectedValue + "',Thursday='" + chkthursday.Checked + "',ThursdayscheduleId='" + ddlthursday.SelectedValue + "',Friday='" + chkfriday.Checked + "',FridayscheduleId='" + ddlfriday.SelectedValue + "',Saturday='" + chksaturday.Checked + "',SaturdayscheduleId='" + ddlsaturday.SelectedValue + "',Sunday='" + chksunday.Checked + "',SundayscheduleId='" + ddlsunday.SelectedValue + "',LastDayOftheWeek='" + ddllastdayofweek.SelectedItem.Text + "' where  ID='" + ViewState["editid"] + "'";
            SqlCommand cmd = new SqlCommand(str, conn);

            if (conn.State.ToString() != "Open")
            {
                conn.Open();

            }
            cmd.ExecuteNonQuery();
            conn.Close();

            Fillgridday();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully ";
            ImageButton49.Visible = false;
            ImageButton2.Visible = true;
            cleartxt();
            pnladd.Visible = false;
            btnadd.Visible = true;

            lbladd.Text = "";




        }



    }
    //protected DataTable select(string str)
    //{
    //    SqlCommand cmd = new SqlCommand(str, conn);
    //    SqlDataAdapter dtp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    dtp.Fill(dt);

    //    return dt;

    //}
    protected void ImageButton48_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";
        cleartxt();
        //ddlbatchname_SelectedIndexChanged(sender, e);
    }

    protected void gridworkingday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "vi")
        {
            lblmsg.Text = "";

            gridworkingday.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["editid"] = gridworkingday.SelectedIndex;

            string str = "select BatchWorkingDay.* from BatchWorkingDay  where  BatchWorkingDay.ID='" + gridworkingday.SelectedIndex + "'";

            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                string str1 = "Select EmployeeBatchMaster.[Batchmasterid],EmployeeBatchMaster.Employeeid from EmployeeBatchMaster inner join BatchWorkingDay on BatchWorkingDay.BatchMasterId=EmployeeBatchMaster.Batchmasterid where  EmployeeBatchMaster.[Batchmasterid]='" + ds.Rows[0]["BatchMasterId"].ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(str1, conn);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adp1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    lblmsg.Text = "";
                    lbladd.Text = "Edit Weekly Schedule";
                    pnladd.Visible = true;
                    pnlworking.Visible = true;
                    btnadd.Visible = false;
                    ImageButton2.Visible = false;
                    ImageButton49.Visible = true;
                    ImageButton48.Visible = true;

                    ddlstorename.SelectedIndex = ddlstorename.Items.IndexOf(ddlstorename.Items.FindByValue(ds.Rows[0]["Whid"].ToString()));
                    Fillddlbatchname();
                    ddlbatchname.SelectedIndex = ddlbatchname.Items.IndexOf(ddlbatchname.Items.FindByValue(ds.Rows[0]["BatchMasterId"].ToString()));
                    ViewState["BD"] = Convert.ToString(ds.Rows[0]["BatchMasterId"]);
                    Fillddlfriday();
                    Fillddlmonday();
                    Fillddltuesday();
                    Fillddlwednesday();
                    Fillddlthursday();
                    Fillddlfriday();
                    Fillddlsaturday();
                    Fillddlsunday();


                    string ck1 = ds.Rows[0]["Monday"].ToString();
                    if (ck1 == "True")
                    {
                        chkmoday.Checked = true;

                    }
                    else { chkmoday.Checked = false; }
                    chkmoday_CheckedChanged(sender, e);
                    string ck2 = ds.Rows[0]["Tuesday"].ToString();
                    if (ck2 == "True") { chktuesday.Checked = true; } else { chktuesday.Checked = false; }
                    chktuesday_CheckedChanged(sender, e);
                    string ck3 = ds.Rows[0]["Wednesday"].ToString();
                    if (ck3 == "True") { chkwednseday.Checked = true; } else { chkwednseday.Checked = false; }
                    chkwednseday_CheckedChanged(sender, e);
                    string ck4 = ds.Rows[0]["Thursday"].ToString();
                    if (ck4 == "True") { chkthursday.Checked = true; } else { chkthursday.Checked = false; }
                    chkthursday_CheckedChanged(sender, e);
                    string ck5 = ds.Rows[0]["Friday"].ToString();
                    if (ck5 == "True") { chkfriday.Checked = true; } else { chkfriday.Checked = false; }
                    chkfriday_CheckedChanged(sender, e);
                    string ck6 = ds.Rows[0]["Saturday"].ToString();
                    if (ck6 == "True") { chksaturday.Checked = true; } else { chksaturday.Checked = false; }
                    chksaturday_CheckedChanged(sender, e);
                    string ck7 = ds.Rows[0]["Sunday"].ToString();
                    if (ck7 == "True") { chksunday.Checked = true; } else { chksunday.Checked = false; }
                    chksunday_CheckedChanged(sender, e);


                    ddlmonday.SelectedIndex = ddlmonday.Items.IndexOf(ddlmonday.Items.FindByValue(ds.Rows[0]["MondayScheduleId"].ToString()));

                    ddltuesday.SelectedIndex = ddltuesday.Items.IndexOf(ddltuesday.Items.FindByValue(ds.Rows[0]["TuesdayScheduleId"].ToString()));
                    ddlwednesday.SelectedIndex = ddlwednesday.Items.IndexOf(ddlwednesday.Items.FindByValue(ds.Rows[0]["WednesdayScheduleId"].ToString()));
                    ddlthursday.SelectedIndex = ddlthursday.Items.IndexOf(ddlthursday.Items.FindByValue(ds.Rows[0]["ThursdayScheduleId"].ToString()));
                    ddlfriday.SelectedIndex = ddlfriday.Items.IndexOf(ddlfriday.Items.FindByValue(ds.Rows[0]["FridayScheduleId"].ToString()));
                    ddlsaturday.SelectedIndex = ddlsaturday.Items.IndexOf(ddlsaturday.Items.FindByValue(ds.Rows[0]["SaturdayScheduleId"].ToString()));
                    ddlsunday.SelectedIndex = ddlsunday.Items.IndexOf(ddlsunday.Items.FindByValue(ds.Rows[0]["SundayScheduleId"].ToString()));

                    string str12 = "Select * from Day";
                    SqlCommand cmd12 = new SqlCommand(str12, conn);
                    SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
                    DataTable ds12 = new DataTable();
                    adp12.Fill(ds12);
                    ddllastdayofweek.DataSource = ds12;
                    ddllastdayofweek.DataTextField = "day";
                    ddllastdayofweek.DataValueField = "ID";
                    ddllastdayofweek.DataBind();
                    ddllastdayofweek.SelectedIndex = ddllastdayofweek.Items.IndexOf(ddllastdayofweek.Items.FindByText(ds.Rows[0]["LastDayOftheWeek"].ToString()));

                }
                else
                {
                    lblmsg.Text = "";
                    //lblmsg.Text = "Sorry, you can not allow to edit this Batch Working , Because this batch has employees";
                    ModalPopupExtender145.Show();
                }
            }

        }


        //if (e.CommandName == "Delete")
        // {

        //     //gridworkingday.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        //     //ViewState["DId"] = gridworkingday.SelectedDataKey.Value;

        //     int index=gridworkingday.SelectedIndex;

        //     Label batchid = (Label)gridworkingday.Rows[index].FindControl("lblBatchId");
        //     ViewState["BatchId"] = batchid.Text;


        // }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        int days = 0;
        String day = "";
        bool chk = false;
        string st1 = "select * from BatchWorkingDay where Whid='" + ddlstorename.SelectedValue + "' and BatchMasterId='" + ddlbatchname.SelectedValue + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        adp1.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }
        else
        {
            string str11 = "Select ReportPeriod.StartDate from ReportPeriod where  ReportPeriod.Whid='" + ddlstorename.SelectedValue + "' and ReportPeriod.Active='1'";
            SqlCommand cmd11 = new SqlCommand(str11, conn);
            SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
            DataSet ds11 = new DataSet();
            adp11.Fill(ds11);
            DateTime date = Convert.ToDateTime(ds11.Tables[0].Rows[0]["StartDate"]);
            int year = Convert.ToDateTime(ds11.Tables[0].Rows[0]["StartDate"]).Year;
            day = date.DayOfWeek.ToString();
            if (ds11.Tables[0].Rows.Count > 0)
            {
                if (year % 4 == 0)
                {
                    days = 366;
                    // days = 20;
                }
                else
                {
                    days = 365;
                    // days = 15;
                }
                for (int i = 0; i < days; i++)
                {
                    day = date.DayOfWeek.ToString();
                    if (chkmoday.Checked)
                    {
                        if (day == "Monday")
                            chk = true;
                    }
                    else
                    {
                        ddlmonday.SelectedIndex = 0;
                    }
                    if (chktuesday.Checked)
                    {
                        if (day == "Tuesday")
                            chk = true;

                    }
                    else
                    {
                        ddltuesday.SelectedIndex = 0;
                    }
                    if (chkwednseday.Checked)
                    {
                        if (day == "Wednesday")
                            chk = true;

                    }
                    else
                    {
                        ddlwednesday.SelectedIndex = 0;
                    }
                    if (chkthursday.Checked)
                    {
                        if (day == "Thursday")
                            chk = true;
                    }
                    else
                    {
                        ddlthursday.SelectedIndex = 0;
                    }
                    if (chkfriday.Checked)
                    {
                        if (day == "Friday")
                            chk = true;

                    }
                    else
                    {
                        ddlfriday.SelectedIndex = 0;
                    }
                    if (chksaturday.Checked)
                    {
                        if (day == "Saturday")
                            chk = true;

                    }
                    else
                    {
                        ddlsaturday.SelectedIndex = 0;
                    }
                    if (chksunday.Checked)
                    {
                        if (day == "Sunday")
                            chk = true;

                    }
                    else
                    {
                        ddlsunday.SelectedIndex = 0;
                    }
                    if (chk)
                    {

                        string strfinaldates = "Select DateMasterTbl.Date , DateMasterTbl.DateId,DateMasterTbl.day from DateMasterTbl where DateMasterTbl.Date='" + date + "' and DateMasterTbl.day='" + day + "'";
                        SqlCommand cmdfinaldates = new SqlCommand(strfinaldates, conn);
                        SqlDataAdapter adpfinaldates = new SqlDataAdapter(cmdfinaldates);
                        DataSet ds1222 = new DataSet();
                        adpfinaldates.Fill(ds1222);
                        // DateTime date = Convert.ToDateTime(ds11.Tables[0].Rows[0]["EffectiveStartDate"]);
                        if (ds1222.Tables[0].Rows.Count == 1)
                        {
                            string strbatch = "Insert  Into BatchWorkingDays(DateMasterID,BatchID,Monday,MondayScheduleId,Tuesday,TuesdayscheduleId,Wednesday,WednesdayscheduleId,Thursday,ThursdayscheduleId,Friday,FridayscheduleId,Saturday,SaturdayscheduleId,Sunday,SundayscheduleId)values('" + ds1222.Tables[0].Rows[0]["DateId"] + "','" + ddlbatchname.SelectedValue + "','" + chkmoday.Checked + "','" + ddlmonday.SelectedValue + "','" + chktuesday.Checked + "','" + ddltuesday.SelectedValue + "','" + chkwednseday.Checked + "','" + ddlwednesday.SelectedValue + "','" + chkthursday.Checked + "','" + ddlthursday.SelectedValue + "','" + chkfriday.Checked + "','" + ddlfriday.SelectedValue + "','" + chksaturday.Checked + "','" + ddlsaturday.SelectedValue + "','" + chksunday.Checked + "','" + ddlsunday.SelectedValue + "')";
                            SqlCommand cmd12 = new SqlCommand(strbatch, conn);
                            if (conn.State.ToString() != "Open")
                            {
                                conn.Open();
                            }

                            cmd12.ExecuteNonQuery();
                            conn.Close();

                            SqlDataAdapter dabb = new SqlDataAdapter("select * from batchmaster where ID='" + ddlbatchname.SelectedValue + "'", conn);
                            DataTable dtbb = new DataTable();
                            dabb.Fill(dtbb);

                            DateTime date1;
                            DateTime date2;

                            DateTime dt111 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                            DateTime lastd = new DateTime(dt111.Year + 1, 1, 1);
                            lastd = lastd.AddDays(-1);

                            if (dtbb.Rows.Count > 0)
                            {
                                date1 = Convert.ToDateTime(dtbb.Rows[0]["EffectiveStartDate"]);
                                date2 = lastd;

                                DateTime date0;

                                for (date0 = date1; date0 <= date2; date0 = date0.AddDays(1))
                                {
                                    CultureInfo ciCurr = CultureInfo.CurrentCulture;
                                    int weekNum = ciCurr.Calendar.GetWeekOfYear(date0, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

                                    var lastDay = new DateTime(DateTime.Now.Year, 1, 1).AddDays((weekNum) * 7);
                                    var firstDay = lastDay.AddDays(-6);
                                }
                            }
                        }
                    }
                    date = date.AddDays(1);
                    chk = false;


                }


            }
            string strdatecomnyholiday = "Select Company_Holiday.Date,DateMasterTbl.DateId from Company_Holiday inner join DateMasterTbl on DateMasterTbl.Date= Company_Holiday.Date where Company_Holiday.Whid='" + ddlstorename.SelectedValue + "' and  Company_Holiday.Company_Id='" + Session["comid"] + "'";
            SqlCommand cmddatecomnyholiday = new SqlCommand(strdatecomnyholiday, conn);
            SqlDataAdapter adpcmddatecomnyholiday = new SqlDataAdapter(cmddatecomnyholiday);
            DataSet dscmddatecomnyholiday = new DataSet();
            adpcmddatecomnyholiday.Fill(dscmddatecomnyholiday);
            //  int k = 0;

            for (int j = 0; j < dscmddatecomnyholiday.Tables[0].Rows.Count; j++)
            {

                string strCompnyHoliday = "Delete from BatchWorkingDays where BatchID in(select Id from BatchMaster where Whid='" + ddlstorename.SelectedValue + "') and DateMasterID='" + dscmddatecomnyholiday.Tables[0].Rows[j]["DateId"] + "'";
                SqlCommand cmdCompnyHoliday = new SqlCommand(strCompnyHoliday, conn);
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();

                }
                cmdCompnyHoliday.ExecuteNonQuery();
                conn.Close();

            }
            string str = "Insert  Into BatchWorkingDay(Whid,BatchMasterId,Monday,MondayScheduleId,Tuesday,TuesdayscheduleId,Wednesday,WednesdayscheduleId,Thursday,ThursdayscheduleId,Friday,FridayscheduleId,Saturday,SaturdayscheduleId,Sunday,SundayscheduleId,compid,LastDayOftheWeek,firstdayweek,lastdayfirstweek)  Values('" + ddlstorename.SelectedValue + "','" + ddlbatchname.SelectedValue + "','" + chkmoday.Checked + "','" + ddlmonday.SelectedValue + "','" + chktuesday.Checked + "','" + ddltuesday.SelectedValue + "','" + chkwednseday.Checked + "','" + ddlwednesday.SelectedValue + "','" + chkthursday.Checked + "','" + ddlthursday.SelectedValue + "','" + chkfriday.Checked + "','" + ddlfriday.SelectedValue + "','" + chksaturday.Checked + "','" + ddlsaturday.SelectedValue + "','" + chksunday.Checked + "','" + ddlsunday.SelectedValue + "','" + Session["Comid"].ToString() + "','" + ddllastdayofweek.SelectedItem.Text + "','" + txtstartdate.Text + "','" + Label18.Text + "')";
            SqlCommand cmd = new SqlCommand(str, conn);

            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmd.ExecuteNonQuery();
            conn.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";


            SqlDataAdapter dabb1 = new SqlDataAdapter("select * from batchmaster where ID='" + ddlbatchname.SelectedValue + "'", conn);
            DataTable dtbb1 = new DataTable();
            dabb1.Fill(dtbb1);

            ViewState["btch"] = Convert.ToString(dtbb1.Rows[0]["Name"]);

            DateTime date11;
            DateTime date22;

            DateTime dt11123 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            DateTime lastd1 = new DateTime(dt11123.Year + 1, 1, 1);
            lastd1 = lastd1.AddDays(-1);

            //var weekDay1 = DayOfWeek.Sunday;

            if (dtbb1.Rows.Count > 0)
            {
                date11 = Convert.ToDateTime(txtstartdate.Text);
                date22 = lastd1;
                //date22 = Convert.ToDateTime(dtbb1.Rows[0]["lastdayfirstweek"]);
                DateTime date0;

                int weekendid = 0;

                if (ddllastdayofweek.SelectedItem.Text == "Sunday")
                {
                    weekendid = 1;
                }
                if (ddllastdayofweek.SelectedItem.Text == "Monday")
                {
                    weekendid = 2;
                }
                if (ddllastdayofweek.SelectedItem.Text == "Tuesday")
                {
                    weekendid = 3;
                }
                if (ddllastdayofweek.SelectedItem.Text == "Wednesday")
                {
                    weekendid = 4;
                }
                if (ddllastdayofweek.SelectedItem.Text == "Thursday")
                {
                    weekendid = 5;
                }
                if (ddllastdayofweek.SelectedItem.Text == "Friday")
                {
                    weekendid = 6;
                }
                if (ddllastdayofweek.SelectedItem.Text == "Saturday")
                {
                    weekendid = 7;
                }

                for (date11 = date11; date11 <= date22; date11 = date11.AddDays(7))
                {
                    CultureInfo ciCurr = CultureInfo.CurrentCulture;
                    int weekNum = ciCurr.Calendar.GetWeekOfYear(date11, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

                    //var lastDay = new DateTime(DateTime.Now.Year, 1, 1).AddDays((weekNum) * 7);
                    //var firstDay = lastDay.AddDays(-6);

                    var firstDay = date11;
                    var lastDay = firstDay.AddDays(+6);

                    //firstDay = firstDay.AddDays(-1);
                    //lastDay = lastDay.AddDays(-1);                    

                    string mmsd = "Week";

                    for (int i = 1; i <= 9999; i++)
                    {
                        string strusercheck = " select PayperiodName from payperiodMaster where PayperiodTypeID='2' and PayperiodName='" + ViewState["btch"] + ' ' + mmsd + ' ' + i + "'";
                        SqlCommand cmdusercheck = new SqlCommand(strusercheck, conn);
                        SqlDataAdapter adpusercheck = new SqlDataAdapter(cmdusercheck);
                        DataTable dsusercheck = new DataTable();
                        adpusercheck.Fill(dsusercheck);

                        if (dsusercheck.Rows.Count > 0)
                        {
                            weekNum = i + 1;
                        }
                        else
                        {
                            weekNum = i;
                            break;
                        }
                    }
                    string insertfd = "insert into payperiodMaster([PayperiodName],[PayperiodTypeID],[PayperiodStartDate],[PayperiodEndDate],[WeekEndID]) values('" + ViewState["btch"] + ' ' + mmsd + ' ' + weekNum + "' , 2 , '" + firstDay + "','" + lastDay + "','" + weekendid + "')";

                    SqlCommand cmdfff = new SqlCommand(insertfd, conn);
                    if (conn.State.ToString() != "Open")
                    {
                        conn.Open();
                    }
                    cmdfff.ExecuteNonQuery();
                    conn.Close();
                }
            }

            Fillgridday();
            pnladd.Visible = false;
            btnadd.Visible = true;

            lbladd.Text = "";

            cleartxt();

            ModalPopupExtender1222.Hide();
        }
    }
    private DataTable strfinaldates(DateTime date, String day)
    {
        string strfinaldates = "Select DateMasterTbl.Date , DateMasterTbl.DateId,DateMasterTbl.day from DateMasterTbl where DateMasterTbl.Date='" + date + "' and DateMasterTbl.day='" + day + "'";
        SqlCommand cmdfinaldates = new SqlCommand(strfinaldates, conn);
        SqlDataAdapter adpfinaldates = new SqlDataAdapter(cmdfinaldates);
        DataTable ds1222 = new DataTable();
        adpfinaldates.Fill(ds1222);

        return ds1222;
    }
    private void insertworkingdays(string dateid, string batchid, string batchworkingscheduleid)
    {
        string strbatch = "Insert  Into BatchWorkingDays(DateMasterID,BatchID,BatchWorkingDayMasterId)values('" + dateid + "','" + batchid + "','" + batchworkingscheduleid + "')";
        SqlCommand cmd12 = new SqlCommand(strbatch, conn);
        if (conn.State.ToString() != "Open")
        {
            conn.Open();

        }

        cmd12.ExecuteNonQuery();
        conn.Close();

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1222.Hide();
        lblmsg.Text = "";
        cleartxt();


    }
    protected void gridworkingday_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgridday();
    }
    protected void ddlbatchname_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataAdapter adpt = new SqlDataAdapter("select distinct BatchTiming.Id,BatchTiming.BatchMasterId ,TimeSchedulMaster.SchedulName +' : '+Left(cast(BatchTiming.StartTime as nvarchar(50)),5) +' : '+Left(cast(BatchTiming.EndTime as nvarchar(50)),5)  as SchedulName from TimeSchedulMaster  inner join [BatchTiming] ON [BatchTiming].TimeScheduleMasterId=TimeSchedulMaster.id where [BatchTiming].Active=1 and BatchTiming.compid='" + Session["Comid"].ToString() + "' and BatchTiming.BatchMasterId='" + ddlbatchname.SelectedValue + "'  and BatchTiming.Whid='" + ddlstorename.SelectedValue + "' and BatchTiming.Active='1'", conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            string strfingme = "select EffectiveStartDate from BatchMaster where ID='" + dt.Rows[0]["BatchMasterId"].ToString() + "'";

            SqlDataAdapter dafindme = new SqlDataAdapter(strfingme, conn);
            DataTable dtfindme = new DataTable();
            dafindme.Fill(dtfindme);

            txtstartdate.Text = Convert.ToDateTime(dtfindme.Rows[0]["EffectiveStartDate"].ToString()).ToShortDateString();

            DateTime dsf11 = Convert.ToDateTime(txtstartdate.Text);
            dsf11 = dsf11.AddDays(+6);
            Label18.Text = dsf11.ToShortDateString();

            Fillddlmonday();
            Fillddltuesday();
            Fillddlwednesday();
            Fillddlthursday();
            Fillddlfriday();
            Fillddlsaturday();
            Fillddlsunday();
            lblmsg.Text = "";

            chkmoday.Checked = true;
            chkmoday_CheckedChanged(sender, e);

            chktuesday.Checked = true;
            chktuesday_CheckedChanged(sender, e);

            chkwednseday.Checked = true;
            chkwednseday_CheckedChanged(sender, e);

            chkthursday.Checked = true;
            chkthursday_CheckedChanged(sender, e);
            chkfriday.Checked = true;
            chkfriday_CheckedChanged(sender, e);

            chksaturday.Checked = true;
            chksaturday_CheckedChanged(sender, e);

            chksunday.Checked = true;
            chksunday_CheckedChanged(sender, e);


            edit();
            pnlworking.Visible = true;

        }
        else
        {
            pnlworking.Visible = false;
            lblmsg.Visible = true;
            lblmsg.Text = "There is no schedule set for this batch. Please return to " + "<a href=\"BatchTimingManage.aspx\" style=\"font-size:14px; color:red; \" target=\"_blank\">" + "Batch Timing: Add, Manage " + "</a>  to set a schedule.";
        }
    }
    protected void LNK_Click(object sender, EventArgs e)
    {
        if (ddlstorename.SelectedIndex >= 0)
        {
            Fillddlbatchname();
        }
    }
    protected void fillstore()
    {


        DataTable ds = ClsStore.SelectStorename();
        ddlstorename.DataSource = ds;
        ddlstorename.DataTextField = "Name";
        ddlstorename.DataValueField = "WareHouseId";
        ddlstorename.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstorename.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();

        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            gridworkingday.AllowPaging = false;
            gridworkingday.PageSize = 1000;
            Fillgridday();

            Button4.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (gridworkingday.Columns[11].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gridworkingday.Columns[11].Visible = false;
            }
            if (gridworkingday.Columns[12].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                gridworkingday.Columns[12].Visible = false;
            }

        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(100);

            gridworkingday.AllowPaging = true;
            gridworkingday.PageSize = 20;
            Fillgridday();

            Button4.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["editHide"] != null)
            {
                gridworkingday.Columns[11].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                gridworkingday.Columns[12].Visible = true;
            }

        }
    }
    protected void chkmoday_CheckedChanged(object sender, EventArgs e)
    {
        if (chkmoday.Checked == true)
        {
            pnlMonday.Visible = true;
        }
        else
        {
            pnlMonday.Visible = false;
        }
    }
    protected void chktuesday_CheckedChanged(object sender, EventArgs e)
    {
        if (chktuesday.Checked == true)
        {
            pnlTuesday.Visible = true;
        }
        else
        {
            pnlTuesday.Visible = false;
        }
    }
    protected void chkwednseday_CheckedChanged(object sender, EventArgs e)
    {
        if (chkwednseday.Checked == true)
        {
            pnlWednesday.Visible = true;
        }
        else
        {
            pnlWednesday.Visible = false;
        }
    }
    protected void chkthursday_CheckedChanged(object sender, EventArgs e)
    {
        if (chkthursday.Checked == true)
        {
            pnlThursday.Visible = true;
        }
        else
        {
            pnlThursday.Visible = false;
        }
    }
    protected void chkfriday_CheckedChanged(object sender, EventArgs e)
    {
        if (chkfriday.Checked == true)
        {
            pnlFriday.Visible = true;
        }
        else
        {
            pnlFriday.Visible = false;
        }
    }
    protected void chksaturday_CheckedChanged(object sender, EventArgs e)
    {
        if (chksaturday.Checked == true)
        {
            pnlSaturday.Visible = true;
            ddlsaturday.SelectedIndex = 1;
        }
        else
        {
            pnlSaturday.Visible = false;
            ddlsaturday.SelectedIndex = 0;
        }

    }
    protected void chksunday_CheckedChanged(object sender, EventArgs e)
    {
        if (chksunday.Checked == true)
        {
            pnlSunday.Visible = true;
            ddlsaturday.SelectedIndex = 1;
        }
        else
        {
            pnlSunday.Visible = false;
            ddlsaturday.SelectedIndex = 0;
        }
    }
    protected void ImageButton2_Click1(object sender, EventArgs e)
    {
        if (chkmoday.Checked == true || chktuesday.Checked == true || chkwednseday.Checked == true || chkthursday.Checked == true || chkfriday.Checked == true || chksaturday.Checked == true || chksunday.Checked == true)
        {
            ModalPopupExtender1222.Show();
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please set working days and schedule for this batch.";
        }
    }
    protected void gridworkingday_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        Fillgridday();
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


    protected void gridworkingday_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridworkingday.PageIndex = e.NewPageIndex;
        Fillgridday();

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        btnadd.Visible = false;
        lblmsg.Text = "";
        lbladd.Text = "Add New Weekly Schedule";

    }
    protected void chkallbatch_CheckedChanged(object sender, EventArgs e)
    {
        Fillgridday();
    }
    protected void ddlfilterstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgridday();
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

        string te = "BatchMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        ModalPopupExtender145.Hide();
    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {
        ModalPopupExtender145.Hide();
    }
    protected void txtstartdate_TextChanged(object sender, EventArgs e)
    {
        DateTime dsf = Convert.ToDateTime(txtstartdate.Text);

        dsf = dsf.AddDays(+6);

        Label18.Text = dsf.ToShortDateString();
    }
}
