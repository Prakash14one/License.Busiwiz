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

public partial class Add_Attendence_Approval : System.Web.UI.Page
{

    SqlConnection con;
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


        Page.Title = pg.getPageTitle(page);


        statuslable.Visible = false;
        if (!IsPostBack)
        {


            if (!IsPostBack)
            {
                Pagecontrol.dypcontrol(Page, page);
                ViewState["sortOrder"] = "";
                txtdate.Text = DateTime.Now.ToShortDateString();
                fillstore();
                ddlwarehouse_SelectedIndexChanged(sender, e);
                fillgrid();
            }

        }
    }
    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "AttendanceId";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "EmployeeName";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "Date";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "InTime";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "InTimeforcalculation";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;


        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "OutTime";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;


        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "OutTimeforcalculation";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "BatchRequiredhours";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "TotalHourWork";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;

        DataColumn Dcom9 = new DataColumn();
        Dcom9.DataType = System.Type.GetType("System.String");
        Dcom9.ColumnName = "sname";
        Dcom9.AllowDBNull = true;
        Dcom9.Unique = false;
        Dcom9.ReadOnly = false;

        DataColumn Dcom10 = new DataColumn();
        Dcom10.DataType = System.Type.GetType("System.Boolean");
        Dcom10.ColumnName = "ConsiderHalfDayLeave";
        Dcom10.AllowDBNull = true;
        Dcom10.Unique = false;
        Dcom10.ReadOnly = false;



        DataColumn Dcom11 = new DataColumn();
        Dcom11.DataType = System.Type.GetType("System.Boolean");
        Dcom11.ColumnName = "ConsiderFullDayLeave";
        Dcom11.AllowDBNull = true;
        Dcom11.Unique = false;
        Dcom11.ReadOnly = false;

        DataColumn Dcom12 = new DataColumn();
        Dcom12.DataType = System.Type.GetType("System.Boolean");
        Dcom12.ColumnName = "Varify";
        Dcom12.AllowDBNull = true;
        Dcom12.Unique = false;
        Dcom12.ReadOnly = false;


        DataColumn Dcom13 = new DataColumn();
        Dcom13.DataType = System.Type.GetType("System.String");
        Dcom13.ColumnName = "Payabledays";
        Dcom13.AllowDBNull = true;
        Dcom13.Unique = false;
        Dcom13.ReadOnly = false;

        DataColumn Dcom14 = new DataColumn();
        Dcom14.DataType = System.Type.GetType("System.String");
        Dcom14.ColumnName = "EmployeeId";
        Dcom14.AllowDBNull = true;
        Dcom14.Unique = false;
        Dcom14.ReadOnly = false;

        DataColumn Dcom15 = new DataColumn();
        Dcom15.DataType = System.Type.GetType("System.String");
        Dcom15.ColumnName = "Overtime";
        Dcom15.AllowDBNull = true;
        Dcom15.Unique = false;
        Dcom15.ReadOnly = false;

        DataColumn Dcom16 = new DataColumn();
        Dcom16.DataType = System.Type.GetType("System.Boolean");
        Dcom16.ColumnName = "Overtimeapprove";
        Dcom16.AllowDBNull = true;
        Dcom16.Unique = false;
        Dcom16.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);
        dt.Columns.Add(Dcom8);

        dt.Columns.Add(Dcom9);
        dt.Columns.Add(Dcom10);
        dt.Columns.Add(Dcom11);
        dt.Columns.Add(Dcom12);
        dt.Columns.Add(Dcom13);
        dt.Columns.Add(Dcom14);
        dt.Columns.Add(Dcom15);
        dt.Columns.Add(Dcom16);
        return dt;
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

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
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();
    }

    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;

            fillgrid();


            if (GridView1.Columns[11].Visible == true)
            {
                ViewState["docth"] = "tt";
                GridView1.Columns[11].Visible = false;
            }
            if (GridView1.Columns[12].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[12].Visible = false;
            }
            if (GridView1.Columns[13].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[13].Visible = false;
            }
            if (GridView1.Columns[14].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[14].Visible = false;
            }


        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 15;
            fillgrid();


            if (ViewState["docth"] != null)
            {
                GridView1.Columns[11].Visible = true;
            }
            if (ViewState["edith"] != null)
            {
                GridView1.Columns[12].Visible = true;
            }
            if (ViewState["docth"] != null)
            {
                GridView1.Columns[13].Visible = true;
            }
            if (ViewState["edith"] != null)
            {
                GridView1.Columns[14].Visible = true;
            }


        }
    }
    protected DataTable WorkingDay()
    {
        DataTable dsday = new DataTable();
        DataTable ds123 = select("select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ddlbatchmaster.SelectedValue + "'  and DateMasterTbl.Date='" + txtdate.Text + "' ");
        DateTime dtdate = Convert.ToDateTime(txtdate.Text);
        string s = dtdate.DayOfWeek.ToString();
        //if (ds123.Rows.Count > 0)
        //{

            if (s.ToString() == "Monday")
            {
                dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ddlwarehouse.SelectedValue + "' and BatchTiming.BatchMasterId='" + ddlbatchmaster.SelectedValue + "' ");

            }
            if (s.ToString() == "Tuesday")
            {
                dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlwarehouse.SelectedValue + "' and BatchTiming.BatchMasterId='" + ddlbatchmaster.SelectedValue + "' ");

            }
            if (s.ToString() == "Wednesday")
            {
                dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlwarehouse.SelectedValue + "' and BatchTiming.BatchMasterId='" + ddlbatchmaster.SelectedValue + "' ");

            }
            if (s.ToString() == "Thursday")
            {
                dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlwarehouse.SelectedValue + "' and BatchTiming.BatchMasterId='" + ddlbatchmaster.SelectedValue + "' ");

            }
            if (s.ToString() == "Friday")
            {
                dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlwarehouse.SelectedValue + "' and BatchTiming.BatchMasterId='" + ddlbatchmaster.SelectedValue + "' ");

            }
            if (s.ToString() == "Saturday")
            {
                dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlwarehouse.SelectedValue + "' and BatchTiming.BatchMasterId='" + ddlbatchmaster.SelectedValue + "' ");

            }
            if (s.ToString() == "Sunday")
            {
                dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlwarehouse.SelectedValue + "' and BatchTiming.BatchMasterId='" + ddlbatchmaster.SelectedValue + "' ");

            }

        //}
        //else
        //{
        //    statuslable.Visible = true;
        //    statuslable.Text = "You are not allowed to make attendence as today is not a working day.";

        //}
        return dsday;
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        statuslable.Text = "";

        fillgrid();

    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        statuslable.Text = "";
        Fillddlbatch();

    }
    protected void fillstore()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();


            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
        }
    }

    protected void Fillddlbatch()
    {
        string str = "select ID,Name from BatchMaster where BatchMaster.Whid='" + ddlwarehouse.SelectedValue + "' order by ID  Desc";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlbatchmaster.DataSource = ds;
        ddlbatchmaster.DataTextField = "Name";
        ddlbatchmaster.DataValueField = "ID";
        ddlbatchmaster.DataBind();

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        ViewState["emp"] = "";
        DataTable ds1 = new DataTable();
        SqlCommand Mycommand = new SqlCommand("Select AttandanceRule.SeniorEmployeeID from AttandanceRule where StoreId ='" + ddlwarehouse.SelectedValue + "'", con);

        SqlDataAdapter MyDataAdapter = new SqlDataAdapter(Mycommand);

        MyDataAdapter.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            if (Convert.ToString(Session["EmployeeId"]) == Convert.ToString(ds1.Rows[0]["SeniorEmployeeID"]))
            {
                ViewState["emp"] = Convert.ToString(ds1.Rows[0]["SeniorEmployeeID"]);
            }
            else
            {
                string str = "select PartytTypeMaster.PartType from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where EmployeeMaster.EmployeeMasterId='" + Session["EmployeeId"] + "' ";
                SqlDataAdapter adp = new SqlDataAdapter(str, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0]["PartType"]) == "Admin")
                    {
                        ViewState["emp"] = Convert.ToString(Session["EmployeeId"]);
                    }
                }
            }
        }
        else if (Convert.ToString(ViewState["emp"]) == "")
        {
            string str = "select PartytTypeMaster.PartType from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where EmployeeMaster.EmployeeMasterId='" + Session["EmployeeId"] + "' ";
            SqlDataAdapter adp = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0]["PartType"]) == "Admin")
                {
                    ViewState["emp"] = Convert.ToString(Session["EmployeeId"]);
                }
            }
        }

        if (Convert.ToString(ViewState["emp"]) != "")
        {
            int insert = 0;
            int update = 0;


            foreach (GridViewRow grd in GridView1.Rows)
            {
                int AttendanceId = Convert.ToInt32(GridView1.DataKeys[grd.RowIndex].Value);
                Label lblempid = (Label)grd.FindControl("lblempid");
                Label lbldate = (Label)grd.FindControl("lbldate");
                Label txtreqintime = (Label)grd.FindControl("txtreqintime");
                TextBox txtactintime = (TextBox)grd.FindControl("txtactintime");
                Label lblactintime = (Label)grd.FindControl("lblactintime");
                Label txtreqouttime = (Label)grd.FindControl("txtreqouttime");
                TextBox txtactouttime = (TextBox)grd.FindControl("txtactouttime");
                Label lblactouttime = (Label)grd.FindControl("lblactouttime");
                Label txtbatchreqhour = (Label)grd.FindControl("txtbatchreqhour");

                Label lblsname = (Label)grd.FindControl("lblsname");
                decimal payday = 0;

                CheckBox ConsiderHalfDayLeave = (CheckBox)grd.FindControl("chkhalfday");

                CheckBox ConsiderFullDayLeave = (CheckBox)grd.FindControl("chkfullday");

                CheckBox ConsiderHalfDayLeaveap = (CheckBox)grd.FindControl("chkhalfdayap");

                CheckBox ConsiderFullDayLeaveap = (CheckBox)grd.FindControl("chkfulldayap");
                CheckBox Varify = (CheckBox)grd.FindControl("chlapproved");
                CheckBox Varifyap = (CheckBox)grd.FindControl("chlapprovedap");

                CheckBox chkoverapprove = (CheckBox)grd.FindControl("chkoverapprove");

                CheckBox chkoverapproveap = (CheckBox)grd.FindControl("chkoverapproveap");

                if (ConsiderFullDayLeave.Checked == true)
                {
                    payday = 0;
                    ConsiderHalfDayLeave.Checked = false;
                }
                else if (ConsiderHalfDayLeave.Checked == true)
                {
                    payday = Convert.ToDecimal(1.0 / 2.0);
                }
                else
                {
                    payday = 1;
                }
                string totalwork = "";

                TimeSpan t1 = TimeSpan.Parse(txtreqintime.Text);
                TimeSpan t3 = TimeSpan.Parse(txtreqouttime.Text);


                TimeSpan t5 = TimeSpan.Parse(txtactintime.Text);
                TimeSpan t6 = TimeSpan.Parse(txtactouttime.Text);
                string comsmt = t6.CompareTo(t5).ToString();
                string outdifftime = t6.Subtract(t3).ToString();
                string indifftime = t1.Subtract(t5).ToString();
                if (comsmt == "1")
                {
                    totalwork = t6.Subtract(t5).ToString();

                }
                else
                {
                    string forfir = (TimeSpan.Parse("23:59:59")).Subtract(t5).ToString();
                    totalwork = TimeSpan.Parse(forfir).Add(t6).ToString(); ;
                }
                TimeSpan inovertime = new TimeSpan();
                TimeSpan outovertime = new TimeSpan();
                totalwork = totalwork.Replace("-", "");
                totalwork = Convert.ToDateTime(totalwork).ToString("HH:mm");
                Int32 earlydevmin = 0;
                Int32 afterdevmin = 0;
                string stover = "1";
                string overtime = "00:00";
                DataTable drt = select("Select * from AttandanceRule where StoreId='" + ddlwarehouse.SelectedValue + "'");
                if (drt.Rows.Count > 0)
                {

                    earlydevmin = Convert.ToInt32(drt.Rows[0]["AcceptableInTimeDeviationMinutes"]);
                    afterdevmin = Convert.ToInt32(drt.Rows[0]["AcceptableOutTimeDeviationMinutes"]);

                    stover = Convert.ToString(drt.Rows[0]["Overtimepara"]);
                    if (Convert.ToString(drt.Rows[0]["overtimeruleno"]) == "2" || Convert.ToString(drt.Rows[0]["overtimeruleno"]) == "3")
                    {
                        if (Convert.ToString(drt.Rows[0]["overtimerulerange"]) == "True")
                        {

                            t5 = t5.Add(new TimeSpan(0, earlydevmin, 0));
                            t6 = t6.Subtract(new TimeSpan(0, afterdevmin, 0));
                        }
                        if (Convert.ToString(drt.Rows[0]["Overtimehours"]) != "")
                        {
                            Int32 timeextra = Convert.ToInt32(drt.Rows[0]["Overtimehours"]);
                            t5 = t5.Add(new TimeSpan(timeextra, 0, 0));
                            t6 = t6.Subtract(new TimeSpan(timeextra, 0, 0));
                        }
                    }


                }
                if (stover != "0")
                {
                    string comoverf = t5.CompareTo(t1).ToString();
                    if (comoverf == "1")
                    {
                    }
                    else
                    {
                        inovertime = t1.Subtract(t5);

                    }
                    string conoveraf = t3.CompareTo(t6).ToString();
                    if (conoveraf == "1")
                    {
                    }
                    else
                    {
                        outovertime = t6.Subtract(t3);

                    }

                    overtime = outovertime.Add(inovertime).ToString();
                    overtime = Convert.ToDateTime(overtime).ToString("HH:mm");
                }
                else
                {
                    overtime = "00:00";
                }
                if ((txtactintime.Text != lblactintime.Text) || (txtactouttime.Text != lblactouttime.Text) || (ConsiderHalfDayLeave.Checked != ConsiderHalfDayLeaveap.Checked) || (ConsiderFullDayLeave.Checked != ConsiderFullDayLeaveap.Checked) || (Varify.Checked != Varifyap.Checked) || (chkoverapprove.Checked != chkoverapproveap.Checked))
                {
                    if (AttendanceId != 0)
                    {
                        string s1 = "";
                        if (txtactouttime.Text != "00:00")
                        {

                            s1 = "UPDATE AttendenceEntryMaster SET [InTimeforcalculation]='" + txtactintime.Text + "',BatchRequiredhours='" + txtbatchreqhour.Text + "', " +
                                            " LateInMinuts='" + indifftime + "',OutInMinuts='" + outdifftime + "',[OutTimeforcalculation] = '" + txtactouttime.Text + "',[Payabledays] = '" + payday + "',TotalHourWork='" + totalwork + "',Payablehours='" + totalwork + "',ConsiderHalfDayLeave='" + ConsiderHalfDayLeave.Checked + "',ConsiderFullDayLeave='" + ConsiderFullDayLeave.Checked + "',Varify='" + Varify.Checked + "',SupervisorId='" + ViewState["emp"] + "',Overtime='" + overtime + "',Overtimeapprove='" + chkoverapprove.Checked + "' WHERE [AttendanceId] ='" + AttendanceId + "'";
                        }
                        else
                        {
                            s1 = "UPDATE AttendenceEntryMaster SET [InTimeforcalculation]='" + txtactintime.Text + "',[Payabledays] = '" + payday + "',BatchRequiredhours='" + txtbatchreqhour.Text + "', " +
                                           " LateInMinuts='" + indifftime + "',[OutTimeforcalculation] = '" + txtactouttime.Text + "',Payablehours='" + totalwork + "',ConsiderHalfDayLeave='" + ConsiderHalfDayLeave.Checked + "',ConsiderFullDayLeave='" + ConsiderFullDayLeave.Checked + "',Varify='" + Varify.Checked + "',SupervisorId='" + ViewState["emp"] + "',TotalHourWork='00:00',Overtime='" + overtime + "',Overtimeapprove='" + chkoverapprove.Checked + "' WHERE [AttendanceId] ='" + AttendanceId + "'";

                        }
                        SqlCommand cmd333 = new SqlCommand(s1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd333.ExecuteNonQuery();
                        con.Close();
                        update += 1;

                    }
                    else if ((AttendanceId == 0) && (txtactintime.Text != "00:00" || txtactouttime.Text != "00:00"))
                    {
                        string str3 = "";
                        if (txtactouttime.Text != "00:00")
                        {
                            str3 = "insert into AttendenceEntryMaster(EmployeeID,Date,InTime,InTimeforcalculation,OutTime,OutTimeforcalculation,LateInMinuts,OutInMinuts,Outtimedate,Varify,ConsiderHalfDayLeave,ConsiderFullDayLeave,SupervisorId,Payabledays,TotalHourWork,Payablehours,BatchRequiredhours,Overtime,Overtimeapprove)values(" + lblempid.Text + ",'" + lbldate.Text + "','" + txtreqintime.Text + "','" + txtactintime.Text + "','" + txtreqouttime.Text + "','" + txtactouttime.Text + "','" + indifftime + "','" + outdifftime + "','" + lbldate.Text + "','" + Varify.Checked + "','" + ConsiderHalfDayLeave.Checked + "','" + ConsiderHalfDayLeave.Checked + "','" + ViewState["emp"] + "','" + payday + "','" + totalwork + "','" + totalwork + "','" + txtbatchreqhour.Text + "','" + overtime + "','" + chkoverapprove.Checked + "')";
                        }
                        else
                        {
                            str3 = "insert into AttendenceEntryMaster(EmployeeID,Date,InTime,InTimeforcalculation,OutTime,OutTimeforcalculation,LateInMinuts,Outtimedate,Varify,ConsiderHalfDayLeave,ConsiderFullDayLeave,SupervisorId,Payabledays,BatchRequiredhours,Overtime,Overtimeapprove)values(" + lblempid.Text + ",'" + lbldate.Text + "','" + txtreqintime.Text + "','" + txtactintime.Text + "','" + txtreqouttime.Text + "','" + txtactouttime.Text + "','" + indifftime + "','" + lbldate.Text + "','" + Varify.Checked + "','" + ConsiderHalfDayLeave.Checked + "','" + ConsiderHalfDayLeave.Checked + "','" + ViewState["emp"] + "','" + payday + "','" + txtbatchreqhour.Text + "','" + overtime + "','" + chkoverapprove.Checked + "')";

                        }
                        SqlCommand cmd1 = new SqlCommand(str3, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        insert += 1;

                    }

                }

            }
            if (insert > 0 || update > 0)
            {
                statuslable.Visible = true;
                statuslable.Text = insert + " Record inserted, " + update + " Record updated.";
                fillgrid();
            }


        }
        else
        {
            statuslable.Visible = true;
            statuslable.Text = "Allow only supervisor/admin approved.";
        }
    }
    public void fillgrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string str1 = "";
        DataTable chkAttAvailable = WorkingDay();
        if (chkAttAvailable.Rows.Count > 0)
        {
            lblCompany.Text = Session["Cname"].ToString();
            lblBusiness.Text = ddlwarehouse.SelectedItem.Text;
            lblbatch.Text = "Batch Name : " + ddlbatchmaster.SelectedItem.Text;
            lblemp.Text = "Date : " + txtdate.Text;
            string empid = "";
            str1 = "Select distinct AttendenceEntryMaster.*,Left(AttendenceEntryMaster.BatchRequiredhours,5) as reqhour,EmployeeMaster.EmployeeName, a.EmployeeName as sname from    AttendenceEntryMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterId=AttendenceEntryMaster.EmployeeID inner join  EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID left join EmployeeMaster as a on a.EmployeeMasterId= AttendenceEntryMaster.SupervisorId where EmployeeBatchMaster.Batchmasterid='" + ddlbatchmaster.SelectedValue + "' and Date='" + Convert.ToDateTime(txtdate.Text).ToShortDateString() + "'";

            DataTable ds1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(str1, con);
            da.Fill(ds1);

            DataTable dtf = new DataTable();
            dtf = CreateDatatable();

            foreach (DataRow dtp in ds1.Rows)
            {
                DataRow Drow = dtf.NewRow();
                empid += "'" + Convert.ToString(dtp["EmployeeID"]) + "',";

                Drow["AttendanceId"] = Convert.ToString(dtp["AttendanceId"]);
                Drow["EmployeeName"] = Convert.ToString(dtp["EmployeeName"]);
                Drow["Date"] = txtdate.Text;

                Drow["InTime"] = Convert.ToString(dtp["InTime"]);
                Drow["InTimeforcalculation"] = Convert.ToString(dtp["InTimeforcalculation"]);

                Drow["EmployeeId"] = Convert.ToString(dtp["EmployeeId"]);

                Drow["OutTime"] = Convert.ToString(dtp["OutTime"]);
                Drow["OutTimeforcalculation"] = Convert.ToString(dtp["OutTimeforcalculation"]);
                Drow["BatchRequiredhours"] = Convert.ToString(dtp["reqhour"]);
                Drow["TotalHourWork"] = Convert.ToString(dtp["TotalHourWork"]);
                Drow["Payabledays"] = Convert.ToString(dtp["Payabledays"]);
                Drow["Overtime"] = Convert.ToString(dtp["Overtime"]);
                Drow["sname"] = Convert.ToString(dtp["sname"]);
                if (Convert.ToString(dtp["ConsiderHalfDayLeave"]) != "")
                {
                    Drow["ConsiderHalfDayLeave"] = Convert.ToBoolean(dtp["ConsiderHalfDayLeave"]);
                }
                else
                {
                    Drow["ConsiderHalfDayLeave"] = Convert.ToBoolean(0);
                }


                if (Convert.ToString(dtp["ConsiderFullDayLeave"]) != "")
                {
                    Drow["ConsiderFullDayLeave"] = Convert.ToBoolean(dtp["ConsiderFullDayLeave"]);
                }
                else
                {
                    Drow["ConsiderFullDayLeave"] = Convert.ToBoolean(0);
                }
                if (Convert.ToString(dtp["Varify"]) != "")
                {
                    Drow["Varify"] = Convert.ToBoolean(dtp["Varify"]);
                }
                else
                {
                    Drow["Varify"] = Convert.ToBoolean(0);
                }
                if (Convert.ToString(dtp["Overtimeapprove"]) != "")
                {
                    Drow["Overtimeapprove"] = Convert.ToBoolean(dtp["Overtimeapprove"]);
                }
                else
                {
                    Drow["Overtimeapprove"] = Convert.ToBoolean(0);
                }

                dtf.Rows.Add(Drow);
            }
            if (empid.Length > 0)
            {
                empid = empid.Remove(empid.Length - 1, 1);
                empid = " and EmployeeMasterID not in(" + empid + ")  ";
            }
            string filtertype = "";
            DataTable ds123 = select("select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ddlbatchmaster.SelectedValue + "'  and DateMasterTbl.Date='" + Convert.ToDateTime(txtdate.Text).ToShortDateString() + "' ");

            if (ds123.Rows.Count > 0)
            {

                if (ds123.Rows[0]["day"].ToString() == "Monday")
                {
                    filtertype = " BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId  ";

                }
                else if (ds123.Rows[0]["day"].ToString() == "Tuesday")
                {
                    filtertype = " BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId  ";

                }
                else if (ds123.Rows[0]["day"].ToString() == "Wednesday")
                {
                    filtertype = " BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId ";

                }
                else if (ds123.Rows[0]["day"].ToString() == "Thursday")
                {
                    filtertype = " BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId ";

                }
                else if (ds123.Rows[0]["day"].ToString() == "Friday")
                {
                    filtertype = " BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId ";

                }
                else if (ds123.Rows[0]["day"].ToString() == "Saturday")
                {
                    filtertype = " BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId  ";

                }
                else  if (ds123.Rows[0]["day"].ToString() == "Sunday")
                {
                    filtertype = " BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId ";

                }
                else
                {
                    filtertype = "BatchTiming ";

                }


            }
            else
            {
                filtertype = " BatchTiming ";

            }
            string strnotl = "SELECT distinct EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterID,Left(BatchTiming.Totalhours,5) as Totalhours, EmployeeMaster.EmployeeName,Left(BatchTiming.StartTime,5) as StartTime,Left(BatchTiming.EndTime,5) as EndTime " +
                                  " FROM " + filtertype + " inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchTiming.BatchMasterId inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID where  BatchTiming.BatchMasterId='" + ddlbatchmaster.SelectedValue + "' and EmployeeBatchMaster.Batchmasterid='" + ddlbatchmaster.SelectedValue + "'" + empid;


            DataTable dsno = new DataTable();
            SqlDataAdapter dano = new SqlDataAdapter(strnotl, con);
            dano.Fill(dsno);


            foreach (DataRow dtp in dsno.Rows)
            {


                DataRow Drow = dtf.NewRow();
                Drow["AttendanceId"] = "0";
                Drow["EmployeeName"] = Convert.ToString(dtp["EmployeeName"]);
                Drow["Date"] = txtdate.Text;

                Drow["InTime"] = Convert.ToString(dtp["StartTime"]);
                Drow["InTimeforcalculation"] = "00:00";

                Drow["EmployeeId"] = Convert.ToString(dtp["EmployeeMasterID"]);

                Drow["OutTime"] = Convert.ToString(dtp["EndTime"]);
                Drow["OutTimeforcalculation"] = "00:00";
                Drow["BatchRequiredhours"] = Convert.ToString(dtp["Totalhours"]);
                Drow["TotalHourWork"] = "00:00";
                Drow["Payabledays"] = "0";
                Drow["Overtime"] = "00:00";
                Drow["sname"] = "";
                Drow["ConsiderHalfDayLeave"] = Convert.ToBoolean(0);
                Drow["ConsiderFullDayLeave"] = Convert.ToBoolean(0);
                Drow["Varify"] = Convert.ToBoolean(0);
                Drow["Overtimeapprove"] = Convert.ToBoolean(0);
                dtf.Rows.Add(Drow);
            }
            DataView myDataView = new DataView();
            myDataView = dtf.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataSource = myDataView;
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                btnsubmit.Visible = true;
            }
            else
            {
                btnsubmit.Visible = false;
            }

        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}
