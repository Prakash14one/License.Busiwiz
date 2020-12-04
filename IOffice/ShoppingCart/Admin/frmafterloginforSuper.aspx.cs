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
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using ForAspNet.POP3;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net.Mail;


public partial class Add_Employee_Payroll_Master : System.Web.UI.Page
{
    string timezone = "";
    string plusminus = "";

    SqlConnection con = new SqlConnection(PageConn.connnn);
    DocumentCls1 clsDocument = new DocumentCls1();

    //  SqlConnection con111 = new SqlConnection(@"Data Source=192.168.5.221,2810;Initial Catalog=UserLog;Integrated Security=true");
    //SqlConnection con111 = new SqlConnection(ConfigurationManager.ConnectionStrings["infinal123"].ToString());

    EmployeeCls clsEmployee = new EmployeeCls();
    MessageCls clsMessage = new MessageCls();
    MasterCls1 clsMaster = new MasterCls1();
    public static DataTable chkAttAvailable = new DataTable();

    protected static string empeml = "";
    protected string prtemail = "";
    protected static Int32 flg = 0;
    protected static Int32 spm;
    protected static bool allw;
    protected static string flnam = "";

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

            ViewState["Compid"] = Session["Comid"].ToString();
            ViewState["UserName"] = Session["userid"].ToString();

            SqlConnection con111 = new SqlConnection();
            con111 = new SqlConnection(@"Data Source=192.168.5.221,2810;Initial Catalog=UserLog;Integrated Security=true");

            string str1check = "select * from UserLoginLog where userid='" + ViewState["UserName"] + "'";
            SqlDataAdapter da1check = new SqlDataAdapter(str1check, con);
            DataTable dt1check = new DataTable();
            da1check.Fill(dt1check);

            if (dt1check.Rows.Count == 1)
            {
                if (Request.Url.Host.ToString() == "itimekeeper.com")
                {

                    if (Session["PriceId"].ToString() == "136" || Session["PriceId"].ToString() == "1139")
                    {
                        ModalPopupExtender1222121.Show();
                    }
                    if (Session["PriceId"].ToString() == "1180" || Session["PriceId"].ToString() == "1182")
                    {
                        ModalPopupExtender12221212.Show();
                    }

                    if (Convert.ToString(Session["EmployeeId"]) == "")
                    {
                        string eeed = " Select distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.DesignationMasterId from DesignationMaster inner join EmployeeMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join User_master on User_master.PartyID=Party_master.PartyID where User_master.UserID='" + Session["userid"] + "'";
                        SqlCommand cmdeeed = new SqlCommand(eeed, con);
                        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                        DataTable dteeed = new DataTable();
                        adpeeed.Fill(dteeed);
                        if (dteeed.Rows.Count > 0)
                        {
                            Session["EmployeeId"] = Convert.ToString(dteeed.Rows[0]["EmployeeMasterID"]);
                            Session["DesignationId"] = Convert.ToString(dteeed.Rows[0]["DesignationMasterId"]);
                        }
                    }
                    fillcurrentdate();
                    ViewState["supervisor"] = Session["EmployeeId"];
                    ViewState["empid"] = Session["EmployeeId"];

                    lblcdate.Text = System.DateTime.Now.ToShortDateString();
                    txtfrdate.Text = System.DateTime.Now.ToShortDateString();
                    txtfedate.Text = System.DateTime.Now.ToShortDateString();
                    findware();
                    fillstatusforpopup();
                    fillreminderstatus();
                    fillreminder();

                    timecalculate();

                    chkAttAvailable = WorkingDay();
                    if (chkAttAvailable.Rows.Count > 0)
                    {
                        filldate();
                        pnlsetupwizardlabel.Visible = false;
                        if (Pnlcnfout.Visible == false && Pnlout.Visible == false)
                        {
                            pnlintime.Visible = true;
                        }

                    }
                    else
                    {
                        pnlsetupwizardlabel.Visible = true;
                        pnlintime.Visible = false;
                    }

                    fillbatch();
                    filllapproval();
                    fillleavee();
                    fillgrid();

                    selectInbox();
                    SelectMsgforSendBox();
                    SelectMsgforDeleteBox();
                    SelectMsgforDraft();

                    fillstore11();
                    fillstore1123();

                    fillddl1();
                    fillddl123();
                    fillddlsent();
                    fillddldelete();
                    fillddldrafts();


                    fillusertype();
                    fillpartytypr();

                    fillcompanyname();
                    fillcompanyname11();

                    filljobapplied();
                    filljobapplied11();

                    FillPartyGrid();
                    FillPartyGridext();

                    dwnemail();
                    SelectMsgforInbox();
                    SelectMsgforSendBoxext();
                    SelectMsgforDeleteBoxext();
                    SelectMsgforDraftext();

                }
            }

            else
            {
                if (Convert.ToString(Session["EmployeeId"]) == "")
                {
                    string eeed = " Select distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.DesignationMasterId from DesignationMaster inner join EmployeeMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join User_master on User_master.PartyID=Party_master.PartyID where User_master.UserID='" + Session["userid"] + "'";
                    SqlCommand cmdeeed = new SqlCommand(eeed, con);
                    SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                    DataTable dteeed = new DataTable();
                    adpeeed.Fill(dteeed);
                    if (dteeed.Rows.Count > 0)
                    {
                        Session["EmployeeId"] = Convert.ToString(dteeed.Rows[0]["EmployeeMasterID"]);
                        Session["DesignationId"] = Convert.ToString(dteeed.Rows[0]["DesignationMasterId"]);
                    }
                }
                fillcurrentdate();
                ViewState["supervisor"] = Session["EmployeeId"];
                ViewState["empid"] = Session["EmployeeId"];

                lblcdate.Text = System.DateTime.Now.ToShortDateString();
                txtfrdate.Text = System.DateTime.Now.ToShortDateString();
                txtfedate.Text = System.DateTime.Now.ToShortDateString();
                findware();
                fillstatusforpopup();
                fillreminderstatus();
                fillreminder();

                timecalculate();

                chkAttAvailable = WorkingDay();
                if (chkAttAvailable.Rows.Count > 0)
                {
                    filldate();
                    pnlsetupwizardlabel.Visible = false;
                    if (Pnlcnfout.Visible == false && Pnlout.Visible == false)
                    {
                        pnlintime.Visible = true;
                    }

                }
                else
                {
                    pnlsetupwizardlabel.Visible = true;
                    pnlintime.Visible = false;
                }

                fillbatch();
                filllapproval();
                fillleavee();
                fillgrid();

                selectInbox();
                SelectMsgforSendBox();
                SelectMsgforDeleteBox();
                SelectMsgforDraft();

                fillddl1();
                fillddl123();
                fillddlsent();
                fillddldelete();
                fillddldrafts();

                fillstore11();
                fillstore1123();

                fillusertype();
                fillpartytypr();

                fillcompanyname();
                fillcompanyname11();

                filljobapplied();
                filljobapplied11();

                FillPartyGrid();
                FillPartyGridext();

                dwnemail();
                SelectMsgforInbox();
                SelectMsgforSendBoxext();
                SelectMsgforDeleteBoxext();
                SelectMsgforDraftext();
            }
        }
    }
    protected DataTable WorkingDay()
    {
        DataTable dsday = new DataTable();
        string sxd = "select Distinct EmployeeMaster.Whid, CompanyMaster.CompanyName,CompanyMaster.Compid,WareHouseMaster.Name, EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterID,BatchMaster.Name as Bid,BatchMaster.Id from BatchMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join WareHouseMaster on  WareHouseMaster.WarehouseId=EmployeeMaster.whid inner join CompanyMaster on CompanyMaster.compid=WareHouseMaster.comid  where  EmployeeMaster.EmployeeMasterID ='" + ViewState["empid"] + "'";
        SqlDataAdapter adp = new SqlDataAdapter(sxd, con);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            ViewState["Whid"] = ds.Rows[0]["Whid"].ToString();
            ViewState["Compid"] = ds.Rows[0]["Compid"].ToString();
            ViewState["Bid"] = ds.Rows[0]["Id"].ToString();

            DataTable ds123 = select("select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["Bid"] + "'  and DateMasterTbl.Date='" + Label1date.Text + "' ");
            DateTime dtdate = Convert.ToDateTime(Label1date.Text);
            string s = dtdate.DayOfWeek.ToString();
           

                if (s.ToString() == "Monday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (s.ToString() == "Tuesday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (s.ToString() == "Wednesday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (s.ToString() == "Thursday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (s.ToString() == "Friday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (s.ToString() == "Saturday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (s.ToString() == "Sunday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (s.ToString() == "Sunday")
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "You are not allowed to add your attendance, as today is not a working day in your scheduled batch.";

                }

            //}
            //else
            //{
            //    lblmsg.Visible = true;
            //    lblmsg.Text = "You are not allowed to add your attendance, as today is not a working day in your scheduled batch.";

            //}
        }
        return dsday;
    }
    protected void fillgrid()
    {


        string strdate = "";
        string strpera = "";

        if (ddlbatch.SelectedIndex > 0)
        {
            strpera += " and EmployeeBatchMaster.Batchmasterid='" + ddlbatch.SelectedValue + "' ";
        }

        if (txtfrdate.Text != "" && txtfedate.Text != "")
        {
            strdate = strpera + "  and DateMasterTbl.Date between '" + Convert.ToDateTime(txtfrdate.Text).ToShortDateString() + "' and '" + Convert.ToDateTime(txtfedate.Text).ToShortDateString() + "' ";
        }
        if (ddlpresentstatus.SelectedValue == "3")
        {
            //str += " and (AttendenceEntryMaster.LateInMinuts >= '00:00' or AttendenceEntryMaster.OutInMinuts >='00:00') ";
            strpera += " and  (AttendenceEntryMaster.InTimeforcalculation > AttendenceEntryMaster.InTime)  ";

        }
        if (ddlpresentstatus.SelectedValue == "4")
        {
            strpera += "  and  (AttendenceEntryMaster.InTimeforcalculation < AttendenceEntryMaster.InTime) ";
        }
        if (ddlpresentstatus.SelectedValue == "5")
        {
            //str += " and (AttendenceEntryMaster.LateInMinuts >= '00:00' or AttendenceEntryMaster.OutInMinuts >='00:00') ";
            strpera += " and (AttendenceEntryMaster.OutTimeforcalculation > AttendenceEntryMaster.OutTime) ";

        }

        if (ddlpresentstatus.SelectedValue == "6")
        {
            strpera += "  and  (AttendenceEntryMaster.OutTimeforcalculation < AttendenceEntryMaster.OutTime) ";
        }


        DataTable dtf = new DataTable();
        dtf = CreateDatatable();
        DataTable dth = select("SELECT * from dbo.ExplodeDates('" + txtfrdate.Text + "', '" + txtfedate.Text + "') ");
        //DataTable ds1 = select(" SELECT distinct BatchMaster.Id as BID FROM EmployeeMaster  INNER JOIN  dbo.EmployeeBatchMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.EmployeeBatchMaster.Employeeid   inner join BatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID  where  BatchMaster.Whid='" + ddlStore.SelectedValue + "'");

        //for (int k = 0; k < dth.Rows.Count; k++)
        //{
            foreach (DataRow dtr in dth.Rows)
            {



                DateTime dtdate = Convert.ToDateTime(dtr["thedate"].ToString());
                string s = dtdate.DayOfWeek.ToString();
               
            DataTable dt2 = select("SELECT DISTINCT Left(BatchTiming.StartTime,5) as StartTime,Left(BatchTiming.EndTime,5) as EndTime, dbo.AttandanceEntryNotes.OutTimeNote, dbo.AttandanceEntryNotes.IntimeNote,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Varify," +
                        " dbo.AttendenceEntryMaster.EmployeeID, CONVERT(Nvarchar, dbo.AttendenceEntryMaster.Date, 101) AS Date, dbo.AttendenceEntryMaster.InTime," +
                       " dbo.AttendenceEntryMaster.InTimeforcalculation, dbo.AttendenceEntryMaster.OutTime, dbo.AttendenceEntryMaster.OutTimeforcalculation," +
                       " Left(AttendenceEntryMaster.LateInMinuts,5) as LateInMinuts, Left(AttendenceEntryMaster.OutInMinuts,5) as OutInMinuts , dbo.AttendenceEntryMaster.TotalHourWork,dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid, dbo.EmployeeBatchMaster.Batchmasterid,BatchMaster.Name as BatchName  FROM" +
                        " AttandanceEntryNotes Right join AttendenceEntryMaster ON dbo.AttendenceEntryMaster.AttendanceId = dbo.AttandanceEntryNotes.AttendanceId " +
                       " Right  join EmployeeMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  and  AttendenceEntryMaster.Date='" + Convert.ToDateTime(dtr["thedate"]).ToShortDateString() + "' and  EmployeeMaster.Whid='" + ViewState["Whid"] + "' and  EmployeeMaster.Employeemasterid='" + ViewState["empid"] + "' " +
                      " INNER JOIN  dbo.EmployeeBatchMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.EmployeeBatchMaster.Employeeid  inner join BatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join BatchTiming on BatchTiming.BatchMasterId=BatchMaster.Id and BatchMaster.Whid='" + ViewState["Whid"] + "' " + strpera + " order by BatchName,EmployeeMaster.EmployeeName");




            foreach (DataRow dtp in dt2.Rows)
            {
                string staadd = "";
                DataRow Drow = dtf.NewRow();
                if (ddlpresentstatus.SelectedValue == "0" || ddlpresentstatus.SelectedValue == "3" || ddlpresentstatus.SelectedValue == "4" || ddlpresentstatus.SelectedValue == "5" || ddlpresentstatus.SelectedValue == "6")
                {
                    staadd = "ok";
                }
                else if (ddlpresentstatus.SelectedValue == "1")
                {
                    if (Convert.ToString(dtp["AttendanceId"]) == "")
                    {
                        staadd = "ok";
                    }
                }
                else if (ddlpresentstatus.SelectedValue == "2")
                {
                    if (Convert.ToString(dtp["AttendanceId"]) != "")
                    {
                        staadd = "ok";
                    }
                }

                if (staadd == "ok")
                {

                    Drow["BatchName"] = Convert.ToString(dtp["BatchName"]);
                    Drow["AttendanceId"] = Convert.ToString(dtp["AttendanceId"]);
                    Drow["EmployeeName"] = Convert.ToString(dtp["EmployeeName"]);
                    Drow["Date"] = Convert.ToDateTime(dtr["thedate"]).ToShortDateString();

                    if (Convert.ToString(dtp["InTimeforcalculation"]) != "")
                    {
                        Drow["InTimeforcalculation"] = Convert.ToString(dtp["InTimeforcalculation"]);
                    }
                    else
                    {
                        Drow["InTimeforcalculation"] = "00:00";
                    }
                    if (Convert.ToString(dtp["LateInMinuts"]) != "")
                    {
                        string latein = Convert.ToString(dtp["LateInMinuts"]);
                        char last = latein[latein.Length - 1];
                        if (last == ':')
                        {
                            string final = latein.Remove(latein.Length - 1);
                            Drow["LateInMinuts"] = final.ToString();
                        }
                        else
                        {
                            Drow["LateInMinuts"] = Convert.ToString(dtp["LateInMinuts"]);
                        }
                    }
                    else
                    {
                        Drow["LateInMinuts"] = "00:00";
                    }
                    if (Convert.ToString(dtp["OutInMinuts"]) != "")
                    {
                        string lateout = Convert.ToString(dtp["OutInMinuts"]);
                        char last = lateout[lateout.Length - 1];
                        if (last == ':')
                        {
                            string final = lateout.Remove(lateout.Length - 1);
                            Drow["OutInMinuts"] = final.ToString();
                        }
                        else
                        {
                            Drow["OutInMinuts"] = Convert.ToString(dtp["OutInMinuts"]);
                        }
                    }
                    else
                    {
                        Drow["OutInMinuts"] = "00:00";
                    }
                    if (Convert.ToString(dtp["OutTimeforcalculation"]) != "")
                    {
                        Drow["OutTimeforcalculation"] = Convert.ToString(dtp["OutTimeforcalculation"]);
                    }
                    else
                    {
                        Drow["OutTimeforcalculation"] = "00:00";
                    }
                    if (Convert.ToString(dtp["TotalHourWork"]) != "")
                    {
                        Drow["TotalHourWork"] = Convert.ToString(dtp["TotalHourWork"]);
                    }
                    else
                    {
                        Drow["TotalHourWork"] = "00:00";
                    }

                    //Drow["InTimeforcalculation"] = Convert.ToString(dtp["InTimeforcalculation"]);

                    //Drow["LateInMinuts"] = Convert.ToString(dtp["LateInMinuts"]);
                    //Drow["OutInMinuts"] = Convert.ToString(dtp["OutInMinuts"]);



                    //Drow["OutTimeforcalculation"] = Convert.ToString(dtp["OutTimeforcalculation"]);
                    ////Drow["BatchRequiredhours"] = Convert.ToString(dtp["reqhour"]);
                    //Drow["TotalHourWork"] = Convert.ToString(dtp["TotalHourWork"]);






                    Drow["DateId"] = Convert.ToString(dtr["thedate"]);
                    if (Convert.ToString(dtp["Varify"]) == "True" && Convert.ToString(dtp["Varify"]) != "")
                    {
                        Drow["approve"] = "Yes";
                    }
                    else
                    {
                        Drow["approve"] = "No";
                    }
                    if (Convert.ToString(dtp["AttendanceId"]) != "")
                    {
                        Drow["OutTime"] = Convert.ToString(dtp["OutTime"]);
                        Drow["InTime"] = Convert.ToString(dtp["InTime"]);
                        Drow["Status"] = "Present";
                    }
                    else
                    {
                        Drow["OutTime"] = Convert.ToString(dtp["EndTime"]);
                        Drow["InTime"] = Convert.ToString(dtp["StartTime"]);
                        Drow["Status"] = "Absent";
                    }

                    dtf.Rows.Add(Drow);
                }
            }
        }

        //DataView myDataView = new DataView();
        //myDataView = dtf.DefaultView;

        //if (hdnsortExp.Value != string.Empty)
        //{
        //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //}

        gridattendance.DataSource = dtf;
        gridattendance.DataBind();


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
        Dcom1.ColumnName = "BatchName";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "EmployeeName";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "Date";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "InTime";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;


        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "InTimeforcalculation";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;


        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "LateInMinuts";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "OutTime";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "OutInMinuts";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;

        DataColumn Dcom9 = new DataColumn();
        Dcom9.DataType = System.Type.GetType("System.String");
        Dcom9.ColumnName = "TotalHourWork";
        Dcom9.AllowDBNull = true;
        Dcom9.Unique = false;
        Dcom9.ReadOnly = false;

        DataColumn Dcom10 = new DataColumn();
        Dcom10.DataType = System.Type.GetType("System.String");
        Dcom10.ColumnName = "Status";
        Dcom10.AllowDBNull = true;
        Dcom10.Unique = false;
        Dcom10.ReadOnly = false;



        DataColumn Dcom11 = new DataColumn();
        Dcom11.DataType = System.Type.GetType("System.String");
        Dcom11.ColumnName = "approve";
        Dcom11.AllowDBNull = true;
        Dcom11.Unique = false;
        Dcom11.ReadOnly = false;

        DataColumn Dcom12 = new DataColumn();
        Dcom12.DataType = System.Type.GetType("System.String");
        Dcom12.ColumnName = "DateId";
        Dcom12.AllowDBNull = true;
        Dcom12.Unique = false;
        Dcom12.ReadOnly = false;

        DataColumn Dcom13 = new DataColumn();
        Dcom13.DataType = System.Type.GetType("System.String");
        Dcom13.ColumnName = "OutTimeforcalculation";
        Dcom13.AllowDBNull = true;
        Dcom13.Unique = false;
        Dcom13.ReadOnly = false;


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
        return dt;
    }
    protected void fillbatch()
    {
        ddlbatch.Items.Clear();
        string str = "SELECT * from BatchMaster where WHID='" + ViewState["Whid"] + "'";

        SqlCommand cmdwh = new SqlCommand(str, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);
        if (dtwh.Rows.Count > 0)
        {

            ddlbatch.DataSource = dtwh;
            ddlbatch.DataTextField = "Name";
            ddlbatch.DataValueField = "ID";
            ddlbatch.DataBind();
        }
        ddlbatch.Items.Insert(0, "All");
        ddlbatch.Items[0].Value = "0";
    }
    protected void findware()
    {
        DataTable scx = select("Select Whid,EmployeeName from EmployeeMaster where EmployeeMasterId='" + ViewState["empid"] + "'");
        if (scx.Rows.Count > 0)
        {
            ViewState["Whid"] = scx.Rows[0]["Whid"].ToString();
            ViewState["Employeename"] = scx.Rows[0]["EmployeeName"].ToString();
        }
    }
    protected DataTable select(string str)
    {

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter ad6 = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        ad6.Fill(ds);
        return ds;
    }
    protected void fillcurrentdate()
    {
        string Today;
        Today = Convert.ToString(System.DateTime.Today.ToShortDateString());
        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        string thismonthstartdate = thismonthstart.ToShortDateString();
        string thismonthenddate = Today.ToString();

        ViewState["ThisMonthStart"] = thismonthstartdate.ToString();
        ViewState["ThisMonthEnd"] = thismonthenddate.ToString();

    }
    protected void fillreminder()
    {
        SqlDataAdapter daff = new SqlDataAdapter("select warehouseid from warehousemaster inner join employeemaster on employeemaster.whid=warehousemaster.warehouseid where employeemaster.employeemasterid='" + Session["EmployeeId"] + "'", con);
        DataTable dtff = new DataTable();
        daff.Fill(dtff);

        string str1 = "select distinct RemindernoteTbl.* from RemindernoteTbl inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=RemindernoteTbl.EmpId inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join StatusMaster on StatusMaster.StatusId=RemindernoteTbl.StatusId where Party_master.id='" + Session["Comid"].ToString() + "' and  Party_master.Whid='" + Convert.ToString(dtff.Rows[0]["warehouseid"]) + "'  and StatusMaster.StatusId='" + ddlfilterstatus.SelectedValue + "' and RemindernoteTbl.Date between '" + ViewState["ThisMonthStart"] + "' and '" + ViewState["ThisMonthEnd"] + "' and RemindernoteTbl.EmpId='" + Session["EmployeeId"] + "' ";
        //string str1 = "select RemindernoteTbl.* from RemindernoteTbl inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=RemindernoteTbl.EmpId inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join StatusMaster on StatusMaster.StatusId=RemindernoteTbl.StatusId where Party_master.id='" + Session["Comid"].ToString() + "' and  Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and RemindernoteTbl.EmpId='" + Session["EmployeeId"].ToString() + "' and StatusMaster.StatusId='" + ddlfilterstatus.SelectedValue + "' ";
        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {

            grdreminder.DataSource = ds;
            grdreminder.DataBind();
            if (ddlfilterstatus.SelectedItem.Text.Equals("Pending"))
            {
                grdreminder.Columns[2].Visible = true;
            }
            else
            {
                grdreminder.Columns[2].Visible = false;
            }
        }
        else
        {
            grdreminder.DataSource = null;
            grdreminder.DataBind();
        }



    }

    protected void fillstatusforpopup()
    {
        string str = "SELECT [StatusId],[StatusName]  FROM [StatusMaster] inner join StatusCategory on  StatusCategory.StatusCategoryMasterId=StatusMaster.StatusCategoryMasterId where StatusCategory.StatusCategoryMasterId ='188'";
        SqlCommand cmdwh = new SqlCommand(str, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);

        ddlstatus.DataSource = dtwh;
        ddlstatus.DataTextField = "statusname";
        ddlstatus.DataValueField = "statusid";
        ddlstatus.DataBind();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string st1 = "select * from RemindernoteTbl where EmpId='" + Session["EmployeeId"] + "' and Date='" + DateTime.Now.ToShortDateString() + "' and ReminderNote='" + txtremindernote.Text + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmes.Visible = true;
            lblmes.Text = "Sorry, Record Already Exist ";
        }
        else
        {
            String str = "Insert Into RemindernoteTbl (Date,EmpId,ReminderNote,StatusId)values('" + TextBox1.Text + "','" + Session["EmployeeId"] + "','" + txtremindernote.Text + "','" + ddlstatus.SelectedValue + "')";
            SqlCommand cmd = new SqlCommand(str, con);

            if (con.State.ToString() != "Open")
            {
                con.Open();

            }
            cmd.ExecuteNonQuery();
            con.Close();
            //cleartext();
            lblmes.Visible = true;
            lblmes.Text = "Record Inserted Succesfully ";
            fillreminder();
        }
    }


    protected void fillreminderstatus()
    {
        string str = "SELECT [StatusId],[StatusName]  FROM [StatusMaster] inner join StatusCategory on  StatusCategory.StatusCategoryMasterId=StatusMaster.StatusCategoryMasterId where StatusCategory.StatusCategoryMasterId ='188'";
        SqlCommand cmdwh = new SqlCommand(str, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);

        ddlfilterstatus.DataSource = dtwh;
        ddlfilterstatus.DataTextField = "statusname";
        ddlfilterstatus.DataValueField = "statusid";
        ddlfilterstatus.DataBind();
    }






    protected void btnin_Click(object sender, EventArgs e)
    {
        ModalPopupExtender122212.Show();
    }
    protected int rest()
    {
        int cip = 0;
        int uip = 0;
        int flag = 0;
        string ipaddress = "";
        ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string str131 = "select Distinct EmployeeMaster.Whid,EmployeeBarcodeMaster.Barcode, login_Master.username,login_Master.password,EmployeeBarcodeMaster.Employee_Id,EmployeeMaster.EmployeeName,BatchMaster.Name as Bid,BatchMaster.Id from BatchMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join EmployeeBarcodeMaster on EmployeeBarcodeMaster.Employee_Id= EmployeeMaster.EmployeeMasterID inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_master on User_master.PartyId=Party_Master.PartyId inner join login_Master on login_Master.UserId= User_master.UserId Where EmployeeMaster.EmployeeMasterID='" + ViewState["empid"] + "'";
        SqlDataAdapter adp2 = new SqlDataAdapter(str131, con);
        DataTable ds2 = new DataTable();
        adp2.Fill(ds2);
        if (ds2.Rows.Count > 0)
        {
            ViewState["barcode"] = ds2.Rows[0]["Barcode"].ToString();
            DataTable dic = ClsIp.selectIPrestrictionallow(ViewState["Compid"].ToString());
            if (dic.Rows.Count > 0)
            {
                DataTable dcf = ClsIp.selectIPrest(0, ds2.Rows[0]["username"].ToString(), ClsEncDesc.Encrypted(ds2.Rows[0]["password"].ToString()), ViewState["Compid"].ToString());
                if (dcf.Rows.Count > 0)
                {
                    cip = 1;
                    DataTable iprest = ClsIp.selectIPrestriction(ipaddress, 0, ds2.Rows[0]["username"].ToString(), ClsEncDesc.Encrypted(ds2.Rows[0]["password"].ToString()), ViewState["Compid"].ToString());
                    if (iprest.Rows.Count > 0)
                    {
                        flag = 1;
                    }
                    else
                    {
                        DataTable iprestuser = ClsIp.selectIPrestriction(ipaddress, 1, ds2.Rows[0]["username"].ToString(), ClsEncDesc.Encrypted(ds2.Rows[0]["password"].ToString()), ViewState["Compid"].ToString());
                        if (iprestuser.Rows.Count > 0)
                        {
                            flag = 1;
                        }
                    }

                }
                else
                {
                    DataTable dcfe = ClsIp.selectIPrest(1, ds2.Rows[0]["username"].ToString(), ClsEncDesc.Encrypted(ds2.Rows[0]["password"].ToString()), ViewState["Compid"].ToString());
                    if (dcfe.Rows.Count > 0)
                    {
                        uip = 1;
                        DataTable iprestuser = ClsIp.selectIPrestriction(ipaddress, 1, ds2.Rows[0]["username"].ToString(), ClsEncDesc.Encrypted(ds2.Rows[0]["password"].ToString()), ViewState["Compid"].ToString());
                        if (iprestuser.Rows.Count > 0)
                        {
                            flag = 1;
                        }
                    }
                    else
                    {
                        flag = 1;
                    }
                }
                if ((flag != 1 && cip == 1) || (flag != 1 && uip == 1))
                {
                    string ip1 = "";
                    string ipz1 = "";
                    string ip2 = "";
                    string ipz2 = "";
                    string[] separator1 = new string[] { "." };
                    string[] strSplitArr1 = ipaddress.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

                    if (strSplitArr1.Length > 0)
                    {
                        ip1 = strSplitArr1[0].ToString() + "." + strSplitArr1[1].ToString() + ".*.*";
                    }
                    if (strSplitArr1.Length > 1)
                    {
                        ipz1 = strSplitArr1[0].ToString() + "." + strSplitArr1[1].ToString() + ".0.0";
                    }
                    if (strSplitArr1.Length > 2)
                    {
                        ip2 = strSplitArr1[0].ToString() + "." + strSplitArr1[1].ToString() + "." + strSplitArr1[2].ToString() + ".*";
                    }
                    if (strSplitArr1.Length > 3)
                    {
                        ipz2 = strSplitArr1[0].ToString() + "." + strSplitArr1[1].ToString() + "." + strSplitArr1[2].ToString() + ".0";
                    }
                    DataTable iprestuseri = ClsIp.selectmultiIP(ip1, ipz1, ip2, ipz2, cip, uip, ds2.Rows[0]["username"].ToString(), ClsEncDesc.Encrypted(ds2.Rows[0]["password"].ToString()), ViewState["Compid"].ToString());
                    if (iprestuseri.Rows.Count > 0)
                    {
                        flag = 1;
                    }
                }
            }
            else
            {
                flag = 1;
            }
        }
        else
        {
            //lblbarmsg.Text = "Sorry we can not find your barcode in our system,</br> please try again with correct id card. If you are new employee,</br>check with your supervisor whether your barcode is updated in the system.";

            //lblbarmsg.Visible = true;
        }
        return flag;
    }
    protected int CheckEntryaccess()
    {
        int flag = 0;
        string serdate = Convert.ToDateTime(Label1date.Text).ToShortDateString();
        string strapp = "";
        string strde = " Select * from AttandanceRule where StoreId='" + ViewState["Whid"] + "'";
        SqlDataAdapter adpde = new SqlDataAdapter(strde, con);
        DataTable dtde = new DataTable();
        adpde.Fill(dtde);
        if (dtde.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dtde.Rows[0]["rulegreatertime"]) == true)
            {
                int rulegreatertime = Convert.ToInt32(dtde.Rows[0]["rulegreatertimeinstance"]);



                DataTable dt = chkAttAvailable;
                if (dt.Rows.Count > 0)
                {
                    TimeSpan tt;
                    TimeSpan tto = TimeSpan.Parse(time22.Text);
                    TimeSpan tt1 = TimeSpan.Parse(dt.Rows[0]["EndTime"].ToString());

                    tt = tto.Subtract(new TimeSpan(rulegreatertime, 0, 0));
                    string ori = tt1.CompareTo(tt).ToString();
                    string comparesame = tto.CompareTo(tt).ToString();

                    if (comparesame == "1")
                    {
                        serdate = Convert.ToDateTime(Label1date.Text).ToShortDateString();
                    }
                    else
                    {
                        serdate = Convert.ToDateTime(Label1date.Text).AddDays(-1).ToShortDateString();

                    }

                }
            }

            strapp = "SELECT Distinct EmployeeMaster.EmployeeMasterID,AttendenceEntryMaster.InTimeforcalculation,AttendenceEntryMaster.InTime,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Date,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.InTime " +
                "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + Session["EmployeeID1"] + "') and (AttendenceEntryMaster.OutTimeforcalculation<>'00:00') and (AttendenceEntryMaster.Date='" + serdate + "') Order by AttendanceId";
            SqlDataAdapter adpl = new SqlDataAdapter(strapp, con);
            DataTable dsl = new DataTable();
            adpl.Fill(dsl);
            if (dsl.Rows.Count > 0)
            {
                if (Convert.ToString(dtde.Rows[0]["allowedmultipleentry"]) == "True")
                {

                    flag = 0;
                }
                else
                {
                    flag = 5;
                }

            }
            else
            {
                flag = 0;
            }
            if (Convert.ToString(dtde.Rows[0]["blockattendance"]) == "True")
            {
                string strappaw = "SELECT Distinct AttendanceId, AttendenceEntryMaster.OutTimeforcalculation, Case When(AttendenceEntryMaster.OutTimeforcalculation<>'00:00') then OutTimeforcalculation  else InTimeforcalculation  end as lastminit " +
                    "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + Session["EmployeeID1"] + "')  and (AttendenceEntryMaster.Date='" + serdate + "') Order by AttendanceId Desc";
                SqlDataAdapter aadres = new SqlDataAdapter(strappaw, con);
                DataTable dsrest = new DataTable();
                aadres.Fill(dsrest);
                if (dsrest.Rows.Count > 0)
                {
                    TimeSpan tcc = new TimeSpan();
                    tcc = TimeSpan.Parse("00:" + Convert.ToString(dtde.Rows[0]["blackattendanceminit"]) + ":00");
                    TimeSpan tcca = new TimeSpan();

                    tcca = TimeSpan.Parse(Convert.ToString(time22.Text));


                    TimeSpan mindif = new TimeSpan();
                    mindif = TimeSpan.Parse(Convert.ToString(dsrest.Rows[0]["lastminit"]));
                    mindif = tcca.Subtract(mindif);

                    string cmn = tcc.CompareTo(mindif).ToString();

                    ViewState["restmini"] = Convert.ToString(dtde.Rows[0]["blackattendanceminit"]);
                    ViewState["lastminit"] = Convert.ToString(dsrest.Rows[0]["lastminit"]);
                    if (cmn == "1")
                    {
                        flag = 4;

                    }
                    else
                    {
                    }

                }
            }
        }

        return flag;

    }
    protected void makeattendance()
    {

        string ipaddress = "";
        DateTime datet = DateTime.Now.AddMinutes(-10);
        ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();

        //int flag = rest();

        //if (flag == 1)
        //{

        ViewState["ipdel111"] = "IpAddress='" + ipaddress + "' and  Compid='" + Convert.ToString(Session["Comid"]) + "' and   CAST(Datetime as  Datetime) Between   CAST('" + datet + "'as Datetime) and  CAST('" + DateTime.Now.ToString() + "' as Datetime)";
        string stripa = "select * from  [AttandanceTempBanIpTable] where " + ViewState["ipdel111"];
        SqlCommand cmdipa = new SqlCommand(stripa, con);
        SqlDataAdapter adpipa = new SqlDataAdapter(cmdipa);
        DataTable dsipa = new DataTable();
        adpipa.Fill(dsipa);
        if (dsipa.Rows.Count < 3)
        {
            string sxd = "select Distinct EmployeeMaster.Whid,EmployeeMaster.EmployeeMasterID as Employee_Id, CompanyMaster.CompanyName,CompanyMaster.Compid,WareHouseMaster.Name, EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterID,BatchMaster.Name as Bid,BatchMaster.Id from BatchMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join WareHouseMaster on  WareHouseMaster.WarehouseId=EmployeeMaster.whid inner join CompanyMaster on CompanyMaster.compid=WareHouseMaster.comid  where  EmployeeMaster.EmployeeMasterID ='" + ViewState["empid"] + "'";
            SqlDataAdapter adp = new SqlDataAdapter(sxd, con);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                ViewState["Whid"] = ds.Rows[0]["Whid"].ToString();
                ViewState["Compid"] = ds.Rows[0]["Compid"].ToString();
                ViewState["Bid"] = ds.Rows[0]["Id"].ToString();
                Session["EmployeeID1"] = ds.Rows[0]["Employee_Id"];
                ViewState["EmployeeID"] = ds.Rows[0]["Employee_Id"];
                timecalculate();
                chkAttAvailable = WorkingDay();
                if (chkAttAvailable.Rows.Count > 0)
                {

                    int flattt = CheckEntryaccess();
                    TimeSpan ttimestart = new TimeSpan();
                    TimeSpan Endtime = new TimeSpan();
                    TimeSpan tt = new TimeSpan();
                    TimeSpan tto = new TimeSpan();
                    TimeSpan tt1 = new TimeSpan();
                    tto = TimeSpan.Parse(time22.Text);
                    string comparesame = "";
                    string ori = "";
                    if (flattt == 0)
                    {
                        string strapp = "";
                        string strde = " Select * from AttandanceRule where StoreId='" + ViewState["Whid"] + "'";
                        SqlDataAdapter adpde = new SqlDataAdapter(strde, con);
                        DataTable dtde = new DataTable();
                        adpde.Fill(dtde);
                        if (dtde.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(dtde.Rows[0]["rulegreatertime"]) == true)
                            {
                                int rulegreatertime = Convert.ToInt32(dtde.Rows[0]["rulegreatertimeinstance"]);
                                //DataTable dt = WorkingDay();
                                DataTable dt = chkAttAvailable;
                                if (dt.Rows.Count > 0)
                                {
                                    ViewState["Stimeall"] = Convert.ToInt32(dtde.Rows[0]["Maxuserhours"]);

                                    ViewState["Etimeall"] = Convert.ToInt32(dtde.Rows[0]["Maxuserhours"]);

                                    ViewState["Stime"] = TimeSpan.Parse(dt.Rows[0]["StartTime"].ToString());
                                    ViewState["Etime"] = TimeSpan.Parse(dt.Rows[0]["EndTime"].ToString());

                                    tt1 = TimeSpan.Parse(dt.Rows[0]["EndTime"].ToString());
                                    tt = tto.Subtract(new TimeSpan(rulegreatertime, 0, 0));
                                    ori = tt1.CompareTo(tt).ToString();
                                    comparesame = tto.CompareTo(tt).ToString();
                                    string serdate = "";
                                    if (comparesame == "1")
                                    {
                                        serdate = Convert.ToDateTime(Label1date.Text).ToShortDateString();
                                    }
                                    else
                                    {
                                        serdate = Convert.ToDateTime(Label1date.Text).AddDays(-1).ToShortDateString();

                                    }
                                    if (ori == "1")
                                    {
                                        strapp = "SELECT Distinct EmployeeMaster.EmployeeMasterID,AttendenceEntryMaster.InTimeforcalculation,AttendenceEntryMaster.InTime,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Date,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.InTime " +
                                            "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + ds.Rows[0]["Employee_Id"] + "') and (AttendenceEntryMaster.OutTimeforcalculation='00:00') and (AttendenceEntryMaster.Date='" + serdate + "') and Batchmasterid='" + ViewState["Bid"] + "'";


                                    }
                                    else
                                    {
                                        strapp = "SELECT Distinct EmployeeMaster.EmployeeMasterID,AttendenceEntryMaster.InTimeforcalculation,AttendenceEntryMaster.InTime,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Date,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.InTime " +
                                           "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + ds.Rows[0]["Employee_Id"] + "') and (AttendenceEntryMaster.OutTimeforcalculation='00:00')and (AttendenceEntryMaster.Outtime<='" + tt.ToString().Remove(5, 3) + "') and (AttendenceEntryMaster.Date='" + serdate + "') and Batchmasterid='" + ViewState["Bid"] + "'";


                                    }

                                }
                            }
                        }
                        else
                        {

                            strapp = "SELECT Distinct EmployeeMaster.EmployeeMasterID,AttendenceEntryMaster.InTimeforcalculation,AttendenceEntryMaster.InTime,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Date,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.InTime " +
                             "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + ds.Rows[0]["Employee_Id"] + "') and (AttendenceEntryMaster.OutTimeforcalculation='00:00') and (AttendenceEntryMaster.Date='" + Label1date.Text + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(1).ToShortDateString() + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(-1).ToShortDateString() + "') and Batchmasterid='" + ViewState["Bid"] + "'";
                        }
                        SqlDataAdapter adpl = new SqlDataAdapter(strapp, con);
                        DataTable dsl = new DataTable();
                        adpl.Fill(dsl);
                        if (dsl.Rows.Count > 0)
                        {
                            Session["AttendanceId"] = dsl.Rows[0]["AttendanceId"];

                            if (Convert.ToString(ViewState["Etime"]) != "")
                            {
                                TimeSpan tc = new TimeSpan();
                                tc = TimeSpan.Parse(ViewState["Etime"].ToString());


                                DateTime dtsc = Convert.ToDateTime(Label1date.Text).Add(tc);
                                Endtime = TimeSpan.Parse(ViewState["Etime"].ToString()).Add(new TimeSpan(Convert.ToInt32(ViewState["Etimeall"]), 0, 0));

                                TimeSpan tcc = new TimeSpan();
                                tcc = TimeSpan.Parse("0" + ViewState["Etimeall"].ToString() + ":00:00");


                                dtsc = dtsc.Add(tcc);
                                string cmn = tto.CompareTo(Endtime).ToString();
                                string cd = dtsc.ToShortDateString();
                                if (Convert.ToDateTime(Label1date.Text) < Convert.ToDateTime(cd))
                                {
                                    if (cmn != "1")
                                    {
                                        TimeSpan ax = TimeSpan.Parse(ViewState["Etime"].ToString());
                                        TimeSpan axa = TimeSpan.Parse(time22.Text);
                                        string az = axa.CompareTo(ax).ToString();
                                        if (az == "1")
                                        {
                                            cmn = "1";
                                            //if (Convert.ToDateTime(Label1date.Text) == Convert.ToDateTime(cd))
                                            //{
                                            //    cmn = "1";
                                            //}
                                        }
                                    }
                                }
                                if (cmn == "1")
                                {
                                    if (Convert.ToDateTime(Label1date.Text) == Convert.ToDateTime(cd))
                                    {
                                        Endtime = TimeSpan.Parse(time22.Text).Subtract(new TimeSpan(Convert.ToInt32(ViewState["Etimeall"]), 0, 0));
                                        string sc = tc.CompareTo(Endtime).ToString();
                                        if (cmn != "1")
                                        {
                                            lblentry.Text = "";
                                            lblmsg.Text = "";

                                            ModalPopupExtender1222.Show();
                                        }
                                        else
                                        {
                                            lblmsg.Text = "Sorry,you are not allowed late entry for rules";
                                        }
                                    }

                                }
                                else if (Convert.ToDateTime(Label1date.Text) == Convert.ToDateTime(cd))
                                {
                                    lblmsg.Text = "";

                                    lblentry.Text = "";
                                    matchbarlogout();
                                    // ModalPopupExtender1222.Show();
                                }
                                else
                                {

                                    lblmsg.Text = "Sorry,you are not allowed late entry for rules";
                                    // TimerTime.Enabled = true;
                                }
                            }

                            // matchbarlogout();
                        }
                        else
                        {


                            //string strr = "SELECT Top(1)  OutTimeforcalculation FROM AttendenceEntryMaster where EmployeeID='" + ds.Rows[0]["Employee_Id"] + "' order by AttendanceId Desc ";
                            //SqlDataAdapter adpll = new SqlDataAdapter(strr, con);
                            //DataTable dsll = new DataTable();
                            //adpll.Fill(dsll);
                            //if (dsll.Rows.Count > 0)
                            //{
                            //    lbllastexittime.Text = dsll.Rows[0]["OutTimeforcalculation"].ToString();
                            //}
                            //lblempname.Text = ds.Rows[0]["EmployeeName"].ToString() + " : " + ds.Rows[0]["Employee_Id"]; ;
                            //Label7.Text = time22.Text;

                            if (Convert.ToString(ViewState["Stime"]) != "")
                            {
                                ttimestart = TimeSpan.Parse(ViewState["Stime"].ToString()).Subtract(new TimeSpan(Convert.ToInt32(ViewState["Stimeall"]), 0, 0));
                                string cmn = tto.CompareTo(ttimestart).ToString();
                                if (cmn == "1")
                                {
                                    lblmsg.Text = "";

                                    lblentry.Text = "";
                                    matchbarlogin();
                                }
                                else
                                {
                                    lblmsg.Text = "Sorry,you are not allowed early entry for rules";
                                    // TimerTime.Enabled = true;
                                }
                            }





                        }



                    }
                    else
                    {

                        if (flattt == 4)
                        {

                            lblmsg.Text = "You are not able to record your entry/exit time at this time. Your last entry was at  " + ViewState["lastminit"] + ".  You are blocked from making another entry for  " + ViewState["restmini"] + " minutes.";
                        }
                        else
                        {
                            lblmsg.Text = "Sorry,Only single attendence In & Out for the day";
                        }
                        //TimerTime.Enabled = true;
                    }
                }
                else
                {

                    // TimerTime.Enabled = true;
                }

            }
            else
            {
                //ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                //if (con.State.ToString() == "Open")
                //{
                //    con.Close();
                //}
                //string str3 = "insert into AttandanceLog(IpAddress,Barcode,Comid,Successfull,Datetime)values('" + ipaddress + "','" + ViewState["barcode"] + "','" + ViewState["Compid"] + "','0','" + DateTime.Now.ToString() + "')";
                //con.Open();
                //SqlCommand cmd1 = new SqlCommand(str3, con);

                //cmd1.ExecuteNonQuery();
                //string str31 = "insert into AttandanceTempBanIpTable(IpAddress,Compid,Datetime)values('" + ipaddress + "','" + ViewState["Compid"] + "','" + DateTime.Now.ToString() + "')";

                //SqlCommand cmd11 = new SqlCommand(str31, con);

                //cmd11.ExecuteNonQuery();
                //con.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry we can not enter attendance,Please contact your supervisor";

            }
        }
        else
        {

        }
        //}
        ////if (dsipa.Rows.Count < 3) end
        //else
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Sorry,you have not allowed IP :" + ipaddress;
        //    //lblbarmsg.Visible = true;
        //}
        //txtbartext.Text = "";
        // txtbartext.Focus();        
    }
    protected void matchbarlogin()
    {
        string indifftime = "";
        int alldevmail = 0;

        string str = "SELECT EmployeeMaster.EmployeeMasterID, BatchTiming.Totalhours, EmployeeMaster.EmployeeName,BatchTiming.StartTime,BatchTiming.EndTime, User_master.UserId, Login_master.username,Login_master.password" +
                      " FROM BatchTiming inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchTiming.BatchMasterId inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID  inner join User_Master on  EmployeeMaster.PartyID = User_Master.PartyID inner join Login_master on Login_master.UserID=User_Master.UserID and  EmployeeMaster.EmployeeMasterID=" + Session["EmployeeID1"] + "";

        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        con.Close();
        adp.Fill(dt);
        con.Open();
        if (dt.Rows.Count != 0)
        {
            string str2 = dt.Rows[0]["UserName"].ToString();
            string str4 = Label1date.Text;

            TimeSpan t1 = TimeSpan.Parse(chkAttAvailable.Rows[0]["StartTime"].ToString());
            TimeSpan t2 = TimeSpan.Parse(time22.Text);


            int msgshow = 0;
            int devnoentryforregistercount = 0;
            bool devnoentryforregister = false;
            bool seniorrule = false;
            int seniornoofrules = 0;
            int devintime = 0;
            int devmessagecount = 0;
            int indevcount = 0;
            int noofintimeinstance = 0;
            // int devinouttime = 0;
            string superviser = "";
            bool devinouttrue = false;
            bool devmessage = false;
            // int devtr = 0;
            bool devapproved = true;
            bool countingdevapprov = false;
            string strde = " Select * from AttandanceRule where StoreId='" + ViewState["Whid"] + "'";
            SqlDataAdapter adpde = new SqlDataAdapter(strde, con);
            DataTable dtde = new DataTable();
            adpde.Fill(dtde);
            if (dtde.Rows.Count > 0)
            {
                superviser = Convert.ToString(dtde.Rows[0]["SeniorEmployeeID"]);
                if (Convert.ToBoolean(dtde.Rows[0]["considerwithinrangedeviationasstandardtime"]) == true)
                {
                    devinouttrue = Convert.ToBoolean(dtde.Rows[0]["considerwithinrangedeviationasstandardtime"]);
                    devintime = Convert.ToInt32(dtde.Rows[0]["AcceptableInTimeDeviationMinutes"]);
                    noofintimeinstance = Convert.ToInt32(dtde.Rows[0]["Considerinoutrangeintance"]);
                }
                if (Convert.ToBoolean(dtde.Rows[0]["ShowtheFieldtorecordthereasonfordeviation"]) == true)
                {
                    devmessage = Convert.ToBoolean(dtde.Rows[0]["ShowtheFieldtorecordthereasonfordeviation"]);
                    devmessagecount = Convert.ToInt32(dtde.Rows[0]["Showreasonafterinstance"]);
                }
                if (Convert.ToBoolean(dtde.Rows[0]["TakeapprovaloftheseniorEmployee"]) == true)
                {
                    seniorrule = Convert.ToBoolean(dtde.Rows[0]["TakeapprovaloftheseniorEmployee"]);
                    seniornoofrules = Convert.ToInt32(dtde.Rows[0]["Takeapprovalafterinstance"]);
                }


                if (Convert.ToBoolean(dtde.Rows[0]["Donotallowemployeetomakeentryinregister"]) == true)
                {
                    devnoentryforregistercount = Convert.ToInt32(dtde.Rows[0]["Donotallowemployeeinstance"]);
                    devnoentryforregister = Convert.ToBoolean(dtde.Rows[0]["Donotallowemployeetomakeentryinregister"]);
                }
                if (Convert.ToBoolean(dtde.Rows[0]["Generalapprovalrule"]) == true)
                {
                    devapproved = false;
                }
                else
                {
                    devapproved = true;
                }

                countingdevapprov = devapproved;
                // devinouttime = Convert.ToInt32(dtde.Rows[0]["AcceptableOutTimeDeviationMinutes"].ToString());
            }

            int latecout = 0;
            string intime = time22.Text;
            string comm = t2.CompareTo(t1).ToString();
            int countdevmessage = 0;
            int countdevEntry = 0;
            int siniourcount = 0;
            if (comm == "1")
            {

                indifftime = t2.Subtract(t1).ToString();
                indifftime = Convert.ToDateTime(indifftime).ToString("HH:mm");
                if (devinouttrue == true)
                {
                    //t1 = t1.Add(new TimeSpan(0, devintime, 0));
                    string comm1 = t2.CompareTo(t1).ToString();
                    if (comm1 == "1")
                    {
                        indevcount = filldeventry(noofintimeinstance);
                        indifftime = "-" + indifftime;

                        latecout = 2;
                        countdevEntry = 2;


                    }
                    else
                    {

                        indifftime = "00:00";
                        TimeSpan SPT = TimeSpan.Parse(chkAttAvailable.Rows[0]["StartTime"].ToString());
                        intime = Convert.ToDateTime(SPT).ToString("HH:mm");

                    }
                }
                else
                {
                    indifftime = "-" + indifftime;

                    latecout = 2;


                }
            }
            else
            {
                indifftime = t1.Subtract(t2).ToString();
                indifftime = Convert.ToDateTime(indifftime).ToString("HH:mm");
                indifftime = "+" + indifftime;

            }
            if (countdevEntry == 2)
            {
                if (seniorrule == true)
                {

                    siniourcount = filldeventry(seniornoofrules);
                    if (siniourcount != 2)
                    {
                        devapproved = false;
                        countingdevapprov = false;
                    }
                }
                if (devmessage == true)
                {

                    ViewState["payid"] = "0";
                    countdevmessage = filldeventry(devmessagecount);
                    if (countdevmessage != 2)
                    {
                        countingdevapprov = false;
                    }
                }
            }
            int countdevnoentryforregister = 0;
            if (devnoentryforregister == true)
            {
                if (latecout == 2)
                {
                    ViewState["payid"] = "0";
                    countdevnoentryforregister = filldeventry(devnoentryforregistercount);
                }
            }

            if (countdevnoentryforregister != 2)
            {

                TimeSpan SPT1 = TimeSpan.Parse(chkAttAvailable.Rows[0]["StartTime"].ToString());
                string oritime = SPT1.ToString().Remove(5, SPT1.ToString().Length - 5);
                TimeSpan SPT11 = TimeSpan.Parse(chkAttAvailable.Rows[0]["EndTime"].ToString());
                string oriouttime = SPT11.ToString().Remove(5, SPT11.ToString().Length - 5);
                bool halfloeave = false;
                bool fullleave = false;
                if (countdevEntry == 2 || latecout == 2)
                {
                    TimeSpan t7 = TimeSpan.Parse(chkAttAvailable.Rows[0]["StartTime"].ToString());
                    TimeSpan t8 = TimeSpan.Parse(chkAttAvailable.Rows[0]["EndTime"].ToString());
                    string comsm = t8.CompareTo(t7).ToString();
                    if (indevcount == 2)
                    {
                        halfloeave = true;
                    }
                    else if (countdevEntry == 0 && latecout == 2)
                    {
                        halfloeave = true;
                    }
                    string[] separbm = new string[] { ":" };
                    string[] strSplitArrbm = chkAttAvailable.Rows[0]["Totalhours"].ToString().Split(separbm, StringSplitOptions.RemoveEmptyEntries);
                    decimal bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)) / 2);


                    string batchtothour = "";
                    TimeSpan tt = TimeSpan.Parse(time22.Text);
                    TimeSpan tt1 = TimeSpan.Parse(chkAttAvailable.Rows[0]["EndTime"].ToString());
                    string ori = tt1.CompareTo(tt).ToString();
                    if (ori == "1")
                    {
                        batchtothour = tt1.Subtract(tt).ToString();

                    }
                    else
                    {
                        string forfir = (TimeSpan.Parse("23:59:59")).Subtract(tt).ToString();
                        batchtothour = TimeSpan.Parse(forfir).Add(tt1).ToString(); ;
                    }
                    string[] separbm1 = new string[] { ":" };
                    string[] strSplitArrbm1 = batchtothour.Split(separbm, StringSplitOptions.RemoveEmptyEntries);

                    decimal tota = ((Convert.ToDecimal(strSplitArrbm1[0]) + (Convert.ToDecimal(strSplitArrbm1[1]) / 60)));
                    if (tota < bhour)
                    {
                        fullleave = true;
                        halfloeave = false;
                    }

                }
                Int32 earlydevmin = 0;

                string stover = "1";
                string overtime = "00:00";
                TimeSpan inovertime = new TimeSpan();
                if (dtde.Rows.Count > 0)
                {
                    earlydevmin = Convert.ToInt32(dtde.Rows[0]["AcceptableInTimeDeviationMinutes"]);
                    stover = Convert.ToString(dtde.Rows[0]["Overtimepara"]);
                    if (Convert.ToString(dtde.Rows[0]["overtimeruleno"]) == "2" || Convert.ToString(dtde.Rows[0]["overtimeruleno"]) == "3")
                    {
                        if (Convert.ToString(dtde.Rows[0]["overtimerulerange"]) == "True")
                        {

                            t2 = t2.Add(new TimeSpan(0, earlydevmin, 0));

                        }
                        if (Convert.ToString(dtde.Rows[0]["Overtimehours"]) != "")
                        {
                            Int32 timeextra = Convert.ToInt32(dtde.Rows[0]["Overtimehours"]);
                            t2 = t2.Add(new TimeSpan(timeextra, 0, 0));

                        }
                    }

                }
                if (stover != "0")
                {
                    string comoverf = t2.CompareTo(t1).ToString();
                    if (comoverf == "1")
                    {
                    }
                    else
                    {
                        inovertime = t1.Subtract(t2);

                    }

                    overtime = inovertime.ToString();
                    overtime = Convert.ToDateTime(overtime).ToString("HH:mm");
                }
                else
                {
                    overtime = "00:00";
                }



                string str3 = "insert into AttendenceEntryMaster(EmployeeID,Date,InTime,InTimeforcalculation,OutTime,OutTimeforcalculation,LateInMinuts,Outtimedate,Varify,ConsiderHalfDayLeave,ConsiderFullDayLeave,BatchRequiredhours,Overtime)values(" + ViewState["EmployeeID"] + ",'" + Label1date.Text + "','" + oritime + "','" + intime + "','" + oriouttime + "','00:00','" + indifftime + "','" + Label1date.Text + "','" + countingdevapprov + "','" + halfloeave + "','" + fullleave + "','" + chkAttAvailable.Rows[0]["Totalhours"] + "','" + overtime + "')";

                //dr.Close();
                SqlCommand cmd1 = new SqlCommand(str3, con);
                //dr.Close();
                cmd1.ExecuteNonQuery();
                AttandanceEntryNotes();

                if (countdevEntry == 2)
                {

                    AttendenceDeviations(indifftime, "00:00");
                    alldevmail = 1;
                    if (countdevmessage == 2)
                    {

                        msgshow = 1;
                        //timer1.Enabled = false;
                        ViewState["devap"] = devapproved;
                        if (alldevmail == 1)
                        {
                            sendmail();
                        }
                        ModalPopupExtender4.Show();
                    }
                    if (siniourcount == 2)
                    {
                        lblentry.Visible = true;
                        lblentry.Text = "Your check in /check out time has been recorded, however, please contact your supervisor regarding your attendance.";

                    }

                }
                con.Close();


                if (msgshow == 0)
                {
                    // ModalPopupExtender1.Show();

                    //timer1.Enabled = true;

                    // lblmsg.Text = "Welcome " + lblempname.Text + ", you have successfully entered.<br/> Your entry time:" + time22.Text;
                    if (alldevmail == 1)
                    {
                        sendmail();
                    }

                }

            }
            else
            {
                lblentry.Text = "Your attendance was not recorded as you are currently restricted because you have exceeded the allowed number of instances.  Please check with supervisor.";

                lblentry.Visible = true;

            }




        }

    }

    protected int filldeventry(int devrulesno)
    {
        int i = 0;

        DataTable drs = select("Select * from AttandanceRule where StoreId='" + ViewState["Whid"] + "'");
        if (drs.Rows.Count > 0)
        {
            DataTable dt = new DataTable();
            if (Convert.ToString(drs.Rows[0]["op2graceperiod"]) == "1")
            {

                dt = select("Select  payperiodMaster.ID, payperiodMaster.PayperiodStartDate,payperiodMaster.PayperiodEndDate from payperiodtype inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id where EmpId='" + Session["EmployeeID1"] + "' and PayperiodStartDate<='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "' and PayperiodEndDate>='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "'");
            }
            else
            {
                dt = select("Select  payperiodMaster.ID, payperiodMaster.PayperiodStartDate,payperiodMaster.PayperiodEndDate from AttendancePayperiodtype inner join  payperiodtype on payperiodtype.Id=AttendancePayperiodtype.PayperiodtypeIdforrule inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id where AttendancePayperiodtype.Whid='" + ViewState["Whid"] + "' and EmpId='" + Session["EmployeeID1"] + "' and PayperiodStartDate<='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "' and PayperiodEndDate>='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "' order by payperiodMaster.Id Desc");
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["payid"] = dt.Rows[0]["ID"];

                string str2 = "  select Count(Id) as Id from AttendenceDeviations where PayPeriodID='" + dt.Rows[0]["ID"] + "' and EmployeeID='" + Session["EmployeeID1"] + "'";

                SqlDataAdapter adp2 = new SqlDataAdapter(str2, con);
                DataTable dt1 = new DataTable();

                adp2.Fill(dt1);
                if (Convert.ToInt32(dt1.Rows[0]["Id"]) >= devrulesno)
                {
                    i = 2;
                }
            }
        }

        return i;
    }
    public void sendmail()
    {
        string empmanag = "";
        string emn = "";
        string detail = "";
        string headdet = "";
        string body = "";
        string empname = "";
        string empemail = "";
        string managername = "";
        DataTable dta = new DataTable();
        string grda = "select distinct AttandanceRule.*  from AttandanceRule where StoreId='" + ViewState["Whid"] + "' and op2graceperiod='2' and generalwarningmail='1' and(attendencemail='1' or supermail='1' or attadminmail='1' or parentmail='1') ";

        SqlCommand cmda = new SqlCommand(grda, con);
        SqlDataAdapter daa = new SqlDataAdapter(cmda);
        daa.Fill(dta);
        if (dta.Rows.Count > 0)
        {

            StringBuilder HeadingTable = new StringBuilder();
            StringBuilder strProduct = new StringBuilder();
            string str1 = "Select Distinct AttendenceDeviations.Id, EmployeeMaster.EmployeeName,AttendanceId,AttendenceEntryMaster.EmployeeID,intimedeviationminutes,outtimedeviationminutes," +
                "Convert(nvarchar,AttendenceEntryMaster.Date,101) as Date,Left(BatchRequiredhours,5) as BatchRequiredhours,Payabledays,Payablehours," +
                "InTime,OutTime,InTimeforcalculation,OutTimeforcalculation,note  from  EmployeeMaster inner join AttendenceEntryMaster on " +
                "AttendenceEntryMaster.EmployeeID=EmployeeMaster.EmployeeMasterID inner join AttendenceDeviations on " +
                "AttendenceDeviations.attandanceId=AttendenceEntryMaster.AttendanceId where " +
            "AttendenceDeviations.PayPeriodID='" + ViewState["payid"] + "' and AttendenceEntryMaster.EmployeeID='" + ViewState["EmployeeID"] + "' order by Date Desc";
            DataTable ds1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(str1, con);
            da.Fill(ds1);

            if (ds1.Rows.Count > 0)
            {

                try
                {


                    string payt = "Month";
                    DataTable dt = new DataTable();
                    if (Convert.ToString(dta.Rows[0]["op2graceperiod"]) == "1")
                    {

                    }
                    else
                    {
                        dt = select("Select payperiodtype.Name from AttendancePayperiodtype inner join  payperiodtype on payperiodtype.Id=AttendancePayperiodtype.PayperiodtypeIdforrule inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id where AttendancePayperiodtype.Whid='" + ViewState["Whid"] + "' and EmpId='" + Session["EmployeeID1"] + "' and PayperiodStartDate<='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "' and PayperiodEndDate>='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "' order by payperiodMaster.Id Desc");
                    }
                    if (dt.Rows.Count > 0)
                    {
                        payt = Convert.ToString(dt.Rows[0]["Name"]);
                    }





                    int noof = ds1.Rows.Count;
                    detail += "<span style=\"color: #996600\">Promptness is essential at work. We make allowances and grace periods for the unexpected circumstances that cause tardiness. However, consistently being tardy is not acceptable. </span><br>";
                    detail += "<span style=\"color: #996600\">According to our records, you have been late " + noof + " times in the past (" + payt + "). Please see the below chart for specific examples.  </span><br><br>";
                    strProduct.Append("<table width=\"100%\" border=\"1\">   <tr><td style=\"background-color: silver\"><strong>Date</strong> </td> <td style=\"background-color: silver\"><strong>Scheduled<br>In Time</strong> </td> <td style=\"background-color: silver\"><strong>Recorded<br>In Time</strong> </td><td style=\"background-color: silver\"><strong>Scheduled<br> Out Time</strong></td><td style=\"background-color: silver\"><strong>Recorded<br>Out Time</strong></td><td style=\"background-color: silver\"><strong>Deviation in<br>In Time</strong></td><td style=\"background-color: silver\"><strong>Deviation in<br> Out Time</strong></td></tr>");
                    int ii = 1;
                    foreach (DataRow item in ds1.Rows)
                    {
                        strProduct.Append("<tr><td>" + Convert.ToString(item["Date"]) + "</td><td>" + Convert.ToString(item["InTime"]) + "</td><td>" + Convert.ToString(item["InTimeforcalculation"]) + "</td><td>" + Convert.ToString(item["OutTime"]) + "</td><td>" + Convert.ToString(item["OutTimeforcalculation"]) + "</td><td>" + Convert.ToString(item["intimedeviationminutes"]).Replace("-", "") + "</td><td>" + Convert.ToString(item["outtimedeviationminutes"]).Replace("-", "") + "</td></tr>  ");
                        ii += 1;
                    }
                    strProduct.Append("</table> ");
                    headdet += "<br><span style=\"color: #996600\">If there is an unexpected circumstance that prevents you from arriving to work on time, please contact your supervisor to address the problem accordingly. Any further attendance issues in the future may result in disciplinary action.</span><br><br>";
                    headdet += "<span style=\"color: #996600\">I hope that you will improve your punctuality when arriving at work.</span><br><br>";
                    headdet += "<span style=\"color: #996600\">Sincerely,</span><br>";

                    string strmal = "  SELECT     OutGoingMailServer,WebMasterEmail, EmailMasterLoginPassword, AdminEmail, WHId " +
                                   " FROM         CompanyWebsitMaster WHERE     (WHId = " + Convert.ToInt32(ViewState["Whid"]) + ") ";
                    SqlCommand cmdma = new SqlCommand(strmal, con);
                    SqlDataAdapter adpma = new SqlDataAdapter(cmdma);
                    DataTable dtma = new DataTable();
                    adpma.Fill(dtma);
                    if (dtma.Rows.Count > 0)
                    {
                        //////////

                        string AdminEmail = dtma.Rows[0]["WebMasterEmail"].ToString();// TextAdminEmail.Text;

                        String Password = dtma.Rows[0]["EmailMasterLoginPassword"].ToString();// TextEmailMasterLoginPassword.Text;
                        MailAddress from = new MailAddress(AdminEmail);

                        if (Convert.ToString(dta.Rows[0]["attendencemail"]) == "True")
                        {
                            DataTable dtadty = select("select distinct EmployeeMaster.Email, EmployeeMaster.EmployeeName,a.EmployeeName AS SUPER  from AttendenceEntryMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterId=AttendenceEntryMaster.EmployeeID  left join EmployeeMaster as a on a.SuprviserId=  EmployeeMaster.EmployeeMasterId where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'");

                            if (dtadty.Rows.Count > 0)
                            {
                                empname = Convert.ToString(dtadty.Rows[0]["EmployeeName"]);
                                empemail = Convert.ToString(dtadty.Rows[0]["Email"]);
                                managername = Convert.ToString(dtadty.Rows[0]["SUPER"]);
                                MailAddress to = new MailAddress(empemail);
                                MailMessage objEmail = new MailMessage(from, to);
                                //emn = "<span style=\"color: #996600\">You are receiving this email as you are on the send list: Regarding lateness at work </span><b>" + empname + "</b><br>";
                                emn += "<b>" + Label1date.Text + "</b><br><br>";
                                emn += "<span style=\"color: #996600\">Dear </span><b>" + empname + "</b>,<br><br>";

                                empmanag += "<span style=\"color: #996600\"><b>" + Session["Cname"] + "</b></span><br>";
                                body = emn + detail + strProduct + headdet + empmanag;
                                objEmail.Subject = "You are receiving this email as you are on the send list: Regarding lateness at work for " + empname;
                                objEmail.Body = body.ToString();
                                objEmail.IsBodyHtml = true;


                                objEmail.Priority = MailPriority.High;
                                SmtpClient client = new SmtpClient();

                                client.Credentials = new NetworkCredential(AdminEmail, Password);
                                client.Host = dtma.Rows[0]["OutGoingMailServer"].ToString(); //TextOutGoingMailServer.Text;




                                client.Send(objEmail);
                            }
                        }

                        if (Convert.ToString(dta.Rows[0]["supermail"]) == "True")
                        {
                            DataTable dtadty = select("select distinct SuprviserId   from EmployeeMaster  where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'");

                            if (dtadty.Rows.Count > 0)
                            {
                                DataTable dtsuper = select("select distinct EmployeeMaster.Email, EmployeeMaster.EmployeeName,a.EmployeeName AS SUPER  from EmployeeMaster left join EmployeeMaster as a on a.EmployeeMasterId=EmployeeMaster.SuprviserId where EmployeeMaster.EmployeeMasterID='" + dtadty.Rows[0]["SuprviserId"] + "'");

                                if (dtsuper.Rows.Count > 0)
                                {
                                    emn = "";
                                    empmanag = "";
                                    empname = Convert.ToString(dtsuper.Rows[0]["EmployeeName"]);
                                    empemail = Convert.ToString(dtsuper.Rows[0]["Email"]);
                                    managername = Convert.ToString(dtsuper.Rows[0]["SUPER"]);
                                    //emn = "<span style=\"color: #996600\">You are receiving this email as you are on the send list: Regarding lateness at work </span><b>" + empname + "</b><br>";
                                    emn += "<b>" + Label1date.Text + "</b><br><br>";
                                    emn += "<span style=\"color: #996600\">Dear </span><b>" + empname + "</b>,<br><br>";

                                    empmanag += "<span style=\"color: #996600\"><b>" + Session["Cname"] + "</b></span><br>";
                                    body = emn + detail + strProduct + headdet + empmanag;
                                    MailAddress to = new MailAddress(empemail);
                                    MailMessage objEmail = new MailMessage(from, to);

                                    objEmail.Subject = "You are receiving this email as you are on the send list: Regarding lateness at work for " + empname;
                                    objEmail.Body = body.ToString();
                                    objEmail.IsBodyHtml = true;


                                    objEmail.Priority = MailPriority.High;
                                    SmtpClient client = new SmtpClient();

                                    client.Credentials = new NetworkCredential(AdminEmail, Password);
                                    client.Host = dtma.Rows[0]["OutGoingMailServer"].ToString(); //TextOutGoingMailServer.Text;




                                    client.Send(objEmail);
                                }
                            }
                        }
                        if (Convert.ToString(dta.Rows[0]["attadminmail"]) == "True")
                        {

                            DataTable dtadty = select("select distinct EmployeeMaster.Email, EmployeeMaster.EmployeeName,a.EmployeeName AS SUPER  from AttendenceEntryMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterId=AttendenceEntryMaster.EmployeeID  left join EmployeeMaster as a on a.SuprviserId=  EmployeeMaster.EmployeeMasterId where EmployeeMaster.EmployeeMasterID='" + dta.Rows[0]["SeniorEmployeeID"] + "'");

                            if (dtadty.Rows.Count > 0)
                            {
                                emn = "";
                                empmanag = "";
                                empname = Convert.ToString(dtadty.Rows[0]["EmployeeName"]);
                                empemail = Convert.ToString(dtadty.Rows[0]["Email"]);
                                managername = Convert.ToString(dtadty.Rows[0]["SUPER"]);
                                //emn = "<span style=\"color: #996600\">You are receiving this email as you are on the send list: Regarding lateness at work </span><b>" + empname + "</b><br>";
                                emn += "<b>" + Label1date.Text + "</b><br><br>";
                                emn += "<span style=\"color: #996600\">Dear </span><b>" + empname + "</b>,<br><br>";

                                empmanag += "<span style=\"color: #996600\"><b>" + Session["Cname"] + "</b></span><br>";
                                body = emn + detail + strProduct + headdet + empmanag;
                                MailAddress to = new MailAddress(empemail);
                                MailMessage objEmail = new MailMessage(from, to);

                                objEmail.Subject = "You are receiving this email as you are on the send list: Regarding lateness at work for " + empname;
                                objEmail.Body = body.ToString();
                                objEmail.IsBodyHtml = true;


                                objEmail.Priority = MailPriority.High;
                                SmtpClient client = new SmtpClient();

                                client.Credentials = new NetworkCredential(AdminEmail, Password);
                                client.Host = dtma.Rows[0]["OutGoingMailServer"].ToString(); //TextOutGoingMailServer.Text;




                                client.Send(objEmail);
                            }

                        }

                    }


                    else
                    {

                    }

                }

                catch (Exception e)
                {
                    lblmsg.Text = e.ToString();
                }

            }
        }
    }
    protected void AttandanceEntryNotes()
    {
        string str = "SELECT Max(AttendanceId) as AttendanceId FROM AttendenceEntryMaster where EmployeeID='" + ViewState["empid"] + "'";
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        con.Close();
        adp.Fill(dt);
        con.Open();
        if (Convert.ToString(dt.Rows[0]["AttendanceId"]) != "")
        {
            ViewState["atid"] = dt.Rows[0]["AttendanceId"];
            string str3 = "insert into AttandanceEntryNotes(IntimeNote,AttendanceId)values('','" + dt.Rows[0]["AttendanceId"] + "')";
            //dr.Close();
            SqlCommand cmd1 = new SqlCommand(str3, con);
            //dr.Close();
            cmd1.ExecuteNonQuery();
        }
    }
    protected void AttendenceDeviations(string intimedif, string outdiff)
    {

        DataTable drs = select("Select * from AttandanceRule where StoreId='" + ViewState["Whid"] + "'");
        if (drs.Rows.Count > 0)
        {
            DataTable dta = new DataTable();
            if (Convert.ToString(drs.Rows[0]["op2graceperiod"]) == "1")
            {

                dta = select("Select  payperiodMaster.ID, payperiodMaster.PayperiodStartDate,payperiodMaster.PayperiodEndDate from payperiodtype inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id where EmpId='" + Session["EmployeeID1"] + "' and PayperiodStartDate<='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "' and PayperiodEndDate>='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "'");
            }
            else
            {
                dta = select("Select  payperiodMaster.ID, payperiodMaster.PayperiodStartDate,payperiodMaster.PayperiodEndDate from AttendancePayperiodtype inner join  payperiodtype on payperiodtype.Id=AttendancePayperiodtype.PayperiodtypeIdforrule inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id where AttendancePayperiodtype.Whid='" + ViewState["Whid"] + "' and EmpId='" + Session["EmployeeID1"] + "' and PayperiodStartDate<='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "' and PayperiodEndDate>='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "' order by payperiodMaster.Id Desc");
            }

            if (dta.Rows.Count > 0)
            {
                string str3 = "insert into AttendenceDeviations(PayPeriodID,DeviationDate,EmployeeID,intimedeviationminutes,outtimedeviationminutes,attandanceId)values('" + dta.Rows[0]["Id"] + "','" + DateTime.Now.ToShortDateString() + "','" + Session["EmployeeID1"] + "','" + intimedif + "','" + outdiff + "','" + ViewState["atid"] + "')";
                SqlCommand cmd1 = new SqlCommand(str3, con);
                ////dr.Close();
                cmd1.ExecuteNonQuery();
                string str = "SELECT Max(ID)  FROM AttendenceDeviations where attandanceId='" + ViewState["atid"] + "'";
                SqlDataAdapter adp = new SqlDataAdapter(str, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ViewState["dev"] = Convert.ToString(dt.Rows[0][0]);
                }
            }

        }

    }
    protected void matchbarlogout()
    {
        int alldevmail = 0;
        string totalwork = "";
        string outdifftime = "";
        string str = "SELECT Distinct Overtime, AttendenceEntryMaster.LateInMinuts, EmployeeMaster.EmployeeMasterID, EmployeeMaster.EmployeeName,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.ConsiderHalfDayLeave,AttendenceEntryMaster.ConsiderFullDayLeave,AttendenceEntryMaster.InTime,Totalhours, User_master.UserId, Login_master.username,Login_master.password,AttendenceEntryMaster.InTime,AttendenceEntryMaster.InTimeforcalculation, AttendenceEntryMaster.AttendanceId" +
                     " FROM BatchTiming inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchTiming.BatchMasterId inner join AttendenceEntryMaster on AttendenceEntryMaster.EmployeeID=EmployeeBatchMaster.Employeeid   inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID  inner join User_Master on  EmployeeMaster.PartyID = User_Master.PartyID inner join Login_master on Login_master.UserID=User_Master.UserID and  AttendenceEntryMaster.AttendanceId=" + Session["AttendanceId"] + "";

        SqlDataAdapter adp = new SqlDataAdapter(str, con);

        DataTable dt = new DataTable();
        con.Close();
        con.Open();
        adp.Fill(dt);

        if (dt.Rows.Count != 0)
        {
            chkAttAvailable = WorkingDay();
            int msgshow = 0;
            ViewState["atid"] = Session["AttendanceId"];

            int latecout = 0;
            bool devinouttrue = false;
            int devinouttime = 0;
            bool seniorrule = false;
            int seniornoofrules = 0;
            int indevcount = 0;
            int noofintimeinstance = 0;
            bool devmessage = false;
            int devmessagecount = 0;
            bool devnoentryforregister = false;
            int devnoentryforregistercount = 0;
            bool devapproved = true;
            bool countingdevapprov = false;
            string strde = " Select * from AttandanceRule where StoreId='" + ViewState["Whid"] + "'";
            SqlDataAdapter adpde = new SqlDataAdapter(strde, con);
            DataTable dtde = new DataTable();
            adpde.Fill(dtde);
            if (dtde.Rows.Count > 0)
            {

                if (Convert.ToBoolean(dtde.Rows[0]["considerwithinrangedeviationasstandardtime"]) == true)
                {
                    devinouttrue = Convert.ToBoolean(dtde.Rows[0]["considerwithinrangedeviationasstandardtime"]);
                    devinouttime = Convert.ToInt32(dtde.Rows[0]["AcceptableOutTimeDeviationMinutes"].ToString());
                    noofintimeinstance = Convert.ToInt32(dtde.Rows[0]["Considerinoutrangeintance"]);

                }
                if (Convert.ToBoolean(dtde.Rows[0]["ShowtheFieldtorecordthereasonfordeviation"]) == true)
                {
                    devmessage = Convert.ToBoolean(dtde.Rows[0]["ShowtheFieldtorecordthereasonfordeviation"]);
                    devmessagecount = Convert.ToInt32(dtde.Rows[0]["Showreasonafterinstance"]);
                }
                if (Convert.ToBoolean(dtde.Rows[0]["TakeapprovaloftheseniorEmployee"]) == true)
                {
                    seniorrule = Convert.ToBoolean(dtde.Rows[0]["TakeapprovaloftheseniorEmployee"]);
                    seniornoofrules = Convert.ToInt32(dtde.Rows[0]["Takeapprovalafterinstance"]);
                }


                if (Convert.ToBoolean(dtde.Rows[0]["Donotallowemployeetomakeentryinregister"]) == true)
                {
                    devnoentryforregistercount = Convert.ToInt32(dtde.Rows[0]["Donotallowemployeeinstance"]);
                    devnoentryforregister = Convert.ToBoolean(dtde.Rows[0]["Donotallowemployeetomakeentryinregister"]);
                }
                if (Convert.ToBoolean(dtde.Rows[0]["Generalapprovalrule"]) == true)
                {
                    devapproved = false;
                }
                else
                {
                    devapproved = true;
                }

                countingdevapprov = devapproved;

            }


            int countdevmessage = 0;
            int countdevEntry = 0;
            int siniourcount = 0;
            string str2 = dt.Rows[0]["UserName"].ToString();
            TimeSpan t3 = TimeSpan.Parse(dt.Rows[0]["OutTime"].ToString());
            TimeSpan t4 = TimeSpan.Parse(time22.Text);
            string comm = t3.CompareTo(t4).ToString();
            if (comm == "1")
            {
                outdifftime = t3.Subtract(t4).ToString();
                if (devinouttrue == true)
                {
                    //  t4 = t4.Add(new TimeSpan(0, devinouttime, 0));
                    string comm1 = t3.CompareTo(t4).ToString();
                    if (comm1 == "1")
                    {
                        indevcount = filldeventry(noofintimeinstance);
                        outdifftime = Convert.ToDateTime(outdifftime).ToString("HH:mm");
                        outdifftime = "-" + outdifftime;
                        countdevEntry = 2;
                        latecout = 2;


                    }
                    else
                    {
                        outdifftime = "00:00";

                    }
                }
                else
                {
                    outdifftime = Convert.ToDateTime(outdifftime).ToString("HH:mm");
                    outdifftime = "-" + outdifftime;

                    latecout = 2;
                }
            }
            else
            {
                outdifftime = t4.Subtract(t3).ToString();

                outdifftime = Convert.ToDateTime(outdifftime).ToString("HH:mm");
                outdifftime = "+" + outdifftime;

                latecout = 2;
            }
            TimeSpan t5 = TimeSpan.Parse(dt.Rows[0]["InTimeforcalculation"].ToString());
            TimeSpan t6 = TimeSpan.Parse(time22.Text);
            string comsmt = t6.CompareTo(t5).ToString();
            if (comsmt == "1")
            {
                totalwork = t6.Subtract(t5).ToString();

            }
            else
            {
                string forfir = (TimeSpan.Parse("23:59:59")).Subtract(t5).ToString();
                totalwork = TimeSpan.Parse(forfir).Add(t6).ToString(); ;
            }




            //totalwork = t6.Subtract(t5).ToString();
            totalwork = totalwork.Replace("-", "");
            totalwork = Convert.ToDateTime(totalwork).ToString("HH:mm");

            string latemin = dt.Rows[0]["LateInMinuts"].ToString();
            latemin = latemin.Remove(1, latemin.Length - 1);
            string earlymin = outdifftime.Remove(1, outdifftime.Length - 1);
            string batchtothour = "";
            TimeSpan t7 = TimeSpan.Parse(dt.Rows[0]["InTime"].ToString());
            TimeSpan t8 = TimeSpan.Parse(dt.Rows[0]["OutTime"].ToString());
            string comsm = t8.CompareTo(t7).ToString();
            if (comsm == "1")
            {
                batchtothour = t8.Subtract(t7).ToString();

            }
            else
            {
                string forfir = (TimeSpan.Parse("23:59:59")).Subtract(t7).ToString();
                batchtothour = TimeSpan.Parse(forfir).Add(t8).ToString(); ;
            }
            string payhours = "";
            Decimal payday = 1;

            if (latemin == "-" || earlymin == "-")
            {
                TimeSpan baaa = TimeSpan.Parse(batchtothour);
                baaa = TimeSpan.FromTicks(baaa.Ticks / 2);
                payday = 1 / 2;
                TimeSpan t9 = TimeSpan.Parse(totalwork);
                string comm5 = baaa.CompareTo(t9).ToString();
                if (comm5 == "0")
                {
                    payhours = baaa.ToString().Remove(5, baaa.ToString().Length - 5);
                }
                else
                {
                    payhours = totalwork;
                    payday = 0;
                }
            }
            else
            {
                TimeSpan baaa = TimeSpan.Parse(batchtothour);
                string acd = baaa.ToString();
                payhours = Convert.ToDateTime(acd).ToString("HH:mm");
            }
            if (countdevEntry == 2)
            {
                if (seniorrule == true)
                {

                    siniourcount = filldeventry(seniornoofrules);
                    if (siniourcount != 2)
                    {
                        devapproved = false;
                        countingdevapprov = false;
                    }
                }
                if (devmessage == true)
                {

                    ViewState["payid"] = "0";
                    countdevmessage = filldeventry(devmessagecount);
                    if (countdevmessage != 2)
                    {
                        countingdevapprov = false;
                    }
                }
            }
            int countdevnoentryforregister = 0;
            if (devnoentryforregister == true)
            {
                if (latecout == 0)
                {
                    ViewState["payid"] = "0";
                    countdevnoentryforregister = filldeventry(devnoentryforregistercount);
                }
            }
            bool halfloeave = Convert.ToBoolean(dt.Rows[0]["ConsiderHalfDayLeave"]);
            bool fullleave = Convert.ToBoolean(dt.Rows[0]["ConsiderFullDayLeave"]); ;
            if (countdevnoentryforregister != 2)
            {
                if (countdevEntry == 2 || latecout == 2)
                {
                    if (indevcount == 2)
                    {
                        halfloeave = true;
                    }
                    else if (countdevEntry == 0 && latecout == 2)
                    {
                        halfloeave = true;
                    }
                    string tothou = "00:00";
                    if (Convert.ToString(chkAttAvailable.Rows[0]["Totalhours"]) != "")
                    {
                        tothou = chkAttAvailable.Rows[0]["Totalhours"].ToString();
                    }
                    string[] separbm = new string[] { ":" };
                    string[] strSplitArrbm = tothou.Split(separbm, StringSplitOptions.RemoveEmptyEntries);
                    decimal bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)) / 2);




                    string[] separbm1 = new string[] { ":" };
                    string[] strSplitArrbm1 = payhours.Split(separbm, StringSplitOptions.RemoveEmptyEntries);

                    decimal tota = ((Convert.ToDecimal(strSplitArrbm1[0]) + (Convert.ToDecimal(strSplitArrbm1[1]) / 60)));
                    if (tota < bhour)
                    {
                        fullleave = true;
                        halfloeave = false;
                    }
                    if (halfloeave == true)
                    {
                        payday = Convert.ToDecimal(1.0 / 2.0);
                    }
                    if (fullleave == true)
                    {
                        payday = 0;
                    }

                }
                TimeSpan outovertime = new TimeSpan();
                TimeSpan inovertime = new TimeSpan();

                Int32 afterdevmin = 0;
                string stover = "1";
                string overtime = "00:00";
                if (dtde.Rows.Count > 0)
                {

                    afterdevmin = Convert.ToInt32(dtde.Rows[0]["AcceptableOutTimeDeviationMinutes"]);

                    stover = Convert.ToString(dtde.Rows[0]["Overtimepara"]);
                    if (Convert.ToString(dtde.Rows[0]["overtimeruleno"]) == "2" || Convert.ToString(dtde.Rows[0]["overtimeruleno"]) == "3")
                    {
                        if (Convert.ToString(dtde.Rows[0]["overtimerulerange"]) == "True")
                        {


                            t6 = t6.Subtract(new TimeSpan(0, afterdevmin, 0));
                        }
                        if (Convert.ToString(dtde.Rows[0]["Overtimehours"]) != "")
                        {
                            Int32 timeextra = Convert.ToInt32(dtde.Rows[0]["Overtimehours"]);

                            t6 = t6.Subtract(new TimeSpan(timeextra, 0, 0));
                        }
                    }

                }
                if (stover != "0")
                {

                    string conoveraf = t3.CompareTo(t4).ToString();
                    if (conoveraf == "1")
                    {
                    }
                    else
                    {
                        outovertime = t4.Subtract(t3);

                    }
                    if (Convert.ToString(dt.Rows[0]["Overtime"]) != "")
                    {

                        inovertime = TimeSpan.Parse(Convert.ToString(dt.Rows[0]["Overtime"]));
                    }
                    overtime = outovertime.Add(inovertime).ToString();
                    overtime = Convert.ToDateTime(overtime).ToString("HH:mm");
                }
                else
                {
                    overtime = "00:00";
                }

                string str3 = "update AttendenceEntryMaster set OutTimeforcalculation='" + time22.Text + "',OutInMinuts='" + outdifftime + "',TotalHourWork='" + totalwork + "',Outtimedate='" + Label1date.Text + "',Payablehours='" + payhours + "',Payabledays='" + payday + "',Varify='" + countingdevapprov + "',ConsiderHalfDayLeave='" + halfloeave + "',ConsiderFullDayLeave='" + fullleave + "',Overtime='" + overtime + "'  where AttendanceId=" + Session["AttendanceId"] + "";

                SqlCommand cmd1 = new SqlCommand(str3, con);

                cmd1.ExecuteNonQuery();
                AttandanceEntryNotesupdate();
                if (countdevEntry == 2)
                {

                    AttendenceDeviations("00:00", outdifftime);
                    alldevmail = 1;
                    if (countdevmessage == 2)
                    {

                        msgshow = 1;
                        // timer1.Enabled = false;

                        ViewState["devap"] = devapproved;
                        if (alldevmail == 1)
                        {
                            sendmail();
                        }
                        ModalPopupExtender4.Show();
                    }
                    if (siniourcount == 2)
                    {

                        lblentry.Visible = true;
                        lblentry.Text = "Your check in /check out time has been recorded, however, please contact your supervisor regarding your attendance.";

                    }

                }
                con.Close();


                lblentry.Visible = true;

                if (msgshow == 0)
                {

                    if (alldevmail == 1)
                    {
                        sendmail();
                    }
                }

            }
            else
            {
                lblentry.Visible = true;
                // lblentry.Text = "Do not allow approval of the senior Employee larger then rules for payperiod";
                lblentry.Text = "  Your attendance was not recorded as you are currently restricted because you have exceeded the allowed number of instances.  Please check with supervisor.";


            }

        }



    }
    protected void AttandanceEntryNotesupdate()
    {

        string str3 = "update AttandanceEntryNotes set OutTimeNote='' where AttendanceId=" + Session["AttendanceId"] + "";

        SqlCommand cmd1 = new SqlCommand(str3, con);

        cmd1.ExecuteNonQuery();

    }

    protected void btnyes_Click(object sender, EventArgs e)
    {
        makeattendance();
        filldate();
        fillgrid();
        //pnlintime.Visible = false;
        //Pnlcnfout.Visible = false;
        //Pnlout.Visible = true;
        //  string strde = " Select * from AttandanceRule where StoreId='" + ViewState["Whid"] + "'";
        //            SqlDataAdapter adpde = new SqlDataAdapter(strde, con);
        //            DataTable dtde = new DataTable();
        //            adpde.Fill(dtde);
        //            if (dtde.Rows.Count > 0)
        //            {
        //                if (Convert.ToString(dtde.Rows[0]["allowedmultipleentry"]) == "True")
        //                {
        //                    Pnlcnfout.Visible = false;
        //                    pnlintime.Visible = true;
        //                    Pnlout.Visible = true;
        //                    lblyou.Text = "You are multiple in/out entry in a single day. Please click here to mark your attendance";
        //                }
        //            }

    }
    protected void timecalculate()
    {
        Label1date.Text = Convert.ToString(DateTime.Now.ToUniversalTime().ToShortDateString());
        string sxd = "select Distinct EmployeeMaster.Whid, CompanyMaster.CompanyName,CompanyMaster.Compid,WareHouseMaster.Name, EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterID,BatchMaster.Name as Bid,BatchMaster.Id from BatchMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join WareHouseMaster on  WareHouseMaster.WarehouseId=EmployeeMaster.whid inner join CompanyMaster on CompanyMaster.compid=WareHouseMaster.comid  where  EmployeeMaster.EmployeeMasterID ='" + ViewState["empid"] + "'";
        SqlDataAdapter adpq = new SqlDataAdapter(sxd, con);
        DataTable dsx = new DataTable();
        adpq.Fill(dsx);
        if (dsx.Rows.Count > 0)
        {
            ViewState["Whid"] = dsx.Rows[0]["Whid"].ToString();
            ViewState["Compid"] = dsx.Rows[0]["Compid"].ToString();
            ViewState["Bid"] = dsx.Rows[0]["Id"].ToString();
            string str = "select TimeZoneMaster.gmt from  [BatchMaster] inner join WHTimeZone on WHTimeZone.ID=BatchMaster.BatchTimeZone inner join [TimeZoneMaster] on TimeZoneMaster.Id=WHTimeZone.TimeZone Where BatchMaster.Whid='" + ViewState["Whid"] + "' and  BatchMaster.ID='" + ViewState["Bid"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {

                timezone = ds.Rows[0]["gmt"].ToString();
                plusminus = timezone.ToString().Substring(0, 1);
                // hour = timezone.ToString().Substring(timezone.Length - 3, 3);
                if (plusminus == "+")
                {
                    timezone = timezone.Remove(0, 1);
                    TimeSpan t1 = TimeSpan.Parse(DateTime.Now.ToUniversalTime().ToString("HH:mm"));
                    TimeSpan t2 = TimeSpan.Parse(timezone);

                    time22.Text = t1.Add(t2).ToString();
                    string[] sap = new string[] { "." };
                    string[] ssr = time22.Text.Split(sap, StringSplitOptions.RemoveEmptyEntries);
                    if (ssr.Length > 1)
                    {
                        time22.Text = ssr[1].ToString();
                    }
                    time22.Text = Convert.ToDateTime(time22.Text).ToString("HH:mm");
                    if (Convert.ToInt32(Convert.ToDateTime(time22.Text).ToString("HH")) == Convert.ToInt32(DateTime.Now.ToUniversalTime().ToString("HH")))
                    {
                        if (Convert.ToInt32(Convert.ToDateTime(time22.Text).ToString("mm")) >= 59)
                        {
                            Label1date.Text = Convert.ToString(DateTime.Now.ToUniversalTime().AddDays(1).ToShortDateString());
                        }
                    }
                    else if (Convert.ToInt32(Convert.ToDateTime(time22.Text).ToString("HH")) <= Convert.ToInt32(DateTime.Now.ToUniversalTime().ToString("HH")))
                    {

                        Label1date.Text = Convert.ToString(DateTime.Now.ToUniversalTime().AddDays(1).ToShortDateString());

                    }
                }
                else if (plusminus == "-")
                {
                    timezone = timezone.Remove(0, 1);
                    TimeSpan t1 = TimeSpan.Parse(DateTime.Now.ToUniversalTime().ToString("HH:mm"));
                    TimeSpan t2 = TimeSpan.Parse(timezone);
                    time22.Text = t1.Subtract(t2).ToString();
                    string[] sap = new string[] { "." };
                    string[] ssr = time22.Text.Split(sap, StringSplitOptions.RemoveEmptyEntries);
                    if (ssr.Length > 1)
                    {
                        time22.Text = ssr[1].ToString();
                    }
                    time22.Text = Convert.ToDateTime(time22.Text).ToString("HH:mm");
                    if (Convert.ToInt32(Convert.ToDateTime(time22.Text).ToString("HH")) == Convert.ToInt32(DateTime.Now.ToUniversalTime().ToString("HH")))
                    {
                        if (Convert.ToInt32(Convert.ToDateTime(time22.Text).ToString("mm")) >= 59)
                        {
                            Label1date.Text = Convert.ToString(DateTime.Now.ToUniversalTime().AddDays(-1).ToShortDateString());
                        }
                    }
                    else if (Convert.ToInt32(Convert.ToDateTime(time22.Text).ToString("HH")) >= Convert.ToInt32(DateTime.Now.ToUniversalTime().ToString("HH")))
                    {

                        Label1date.Text = Convert.ToString(DateTime.Now.ToUniversalTime().AddDays(-1).ToShortDateString());

                    }
                }
                else
                {
                    time22.Text = DateTime.Now.ToUniversalTime().ToString("HH:mm");
                }
            }
        }
    }
    protected void btnout_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1222.Show();
    }
    protected void filldate()
    {
        lblyou.Text = "You are logging in for the first time today. Please record your attendance.";
        string str = "select Login_master.UserID,Login_master.username,User_master.UserID,User_master.PartyID," +
                   " EmployeeMaster.EmployeeMasterID,Party_master.PartyID,Party_master.id,EmployeeMaster.SuprviserId " +
                   " from Login_master inner join User_master on Login_master.UserID=User_master.UserID  " +
                   " inner join Party_master on User_master.PartyID=Party_master.PartyID inner join " +
                   " EmployeeMaster on User_master.PartyID=EmployeeMaster.PartyID where Login_master.UserID='" + ViewState["UserName"] + "' and Party_master.id='" + ViewState["Compid"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["empid"] = ds.Tables[0].Rows[0]["EmployeeMasterID"].ToString();

            string stratt = "select Top(1) * from AttendenceEntryMaster where EmployeeID='" + ViewState["empid"] + "' and Date='" + System.DateTime.Now.ToShortDateString() + "' order by AttendanceId desc";
            SqlCommand cmdatt = new SqlCommand(stratt, con);
            SqlDataAdapter adpatt = new SqlDataAdapter(cmdatt);
            DataTable dt = new DataTable();
            adpatt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Pnlcnfout.Visible = true;
                pnlintime.Visible = false;
                lblintime.Text = dt.Rows[0]["InTimeforcalculation"].ToString();
                lbltime.Text = dt.Rows[0]["InTimeforcalculation"].ToString();
                lblout.Text = dt.Rows[0]["OutTimeforcalculation"].ToString();
                if (lblout.Text != "00:00")
                {
                    string strde = " Select * from AttandanceRule where StoreId='" + ViewState["Whid"] + "'";
                    SqlDataAdapter adpde = new SqlDataAdapter(strde, con);
                    DataTable dtde = new DataTable();
                    adpde.Fill(dtde);
                    if (dtde.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtde.Rows[0]["allowedmultipleentry"]) == "True")
                        {
                            Pnlcnfout.Visible = false;


                            string mil = "select InTime from AttendenceEntryMaster where EmployeeID='" + ViewState["empid"] + "' and Date='" + Label1date.Text + "'";
                            SqlDataAdapter damil = new SqlDataAdapter(mil, con);
                            DataTable dtmil = new DataTable();
                            damil.Fill(dtmil);

                            if (dtmil.Rows.Count == 0)
                            {
                                pnlintime.Visible = true;
                            }
                            Pnlout.Visible = true;
                            lblyou.Text = "You have multiple in/out entry in a single day. Please click here to mark your attendance";
                        }
                    }
                    else
                    {

                        Pnlcnfout.Visible = false;
                        pnlintime.Visible = false;
                        Pnlout.Visible = true;
                    }
                }
                else
                {
                    Pnlcnfout.Visible = true;
                    pnlintime.Visible = false;
                    Pnlout.Visible = false;
                }
                lbldate.Text = System.DateTime.Now.ToShortDateString();
            }
            else
            {
                //   pnlintime.Visible = true;
                Pnlcnfout.Visible = false;
                Pnlout.Visible = false;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gdr in grdreminder.Rows)
        {
            Label lblremindermasterid = (Label)gdr.FindControl("lblremindermasterid");
            CheckBox CheckBox1 = (CheckBox)gdr.FindControl("CheckBox1");

            if (CheckBox1.Checked == true)
            {
                if (ddlfilterstatus.SelectedIndex == 0)
                {
                    string str = "SELECT [StatusId],[StatusName]  FROM [StatusMaster] inner join StatusCategory on  StatusCategory.StatusCategoryMasterId=StatusMaster.StatusCategoryMasterId where StatusCategory.StatusCategoryMasterId ='188' and StatusMaster.StatusName='Completed'";
                    SqlCommand cmdwh = new SqlCommand(str, con);
                    SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
                    DataTable dtwh = new DataTable();
                    adpwh.Fill(dtwh);


                    if (dtwh.Rows.Count > 0)
                    {
                        String update = "Update  RemindernoteTbl set StatusId='" + dtwh.Rows[0]["StatusId"].ToString() + "' where Id='" + lblremindermasterid.Text + "'";
                        SqlCommand cmd = new SqlCommand(update, con);

                        if (con.State.ToString() != "Open")
                        {
                            con.Open();

                        }
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                else
                {
                    string str = "SELECT [StatusId],[StatusName]  FROM [StatusMaster] inner join StatusCategory on  StatusCategory.StatusCategoryMasterId=StatusMaster.StatusCategoryMasterId where StatusCategory.StatusCategoryMasterId ='188' and StatusMaster.StatusName='Pending'";
                    SqlCommand cmdwh = new SqlCommand(str, con);
                    SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
                    DataTable dtwh = new DataTable();
                    adpwh.Fill(dtwh);


                    if (dtwh.Rows.Count > 0)
                    {
                        String update = "Update  RemindernoteTbl set StatusId='" + dtwh.Rows[0]["StatusId"].ToString() + "' where Id='" + lblremindermasterid.Text + "'";
                        SqlCommand cmd = new SqlCommand(update, con);

                        if (con.State.ToString() != "Open")
                        {
                            con.Open();

                        }
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }



                }

            }
        }
        fillreminder();

    }
    protected void ddlfilterstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmes.Text = "";
        fillreminder();
    }

    public void filllapproval()
    {
        grdgatepass.DataSource = null;
        grdgatepass.DataBind();

        DataTable dt3 = select("Select Distinct Id, EmployeeMaster.EmployeeName,ExpectedOutTime,ExpectedInTime,Approved from EmployeeMaster inner join GatepassTBL on GatepassTBL.EmployeeID=EmployeeMaster.EmployeeMasterId where GatepassTBL.StoreID='" + ViewState["Whid"] + "' and GatepassTBL.ApprovedEmployeeID='" + ViewState["empid"] + "' order by EmployeeMaster.EmployeeName");
        if (dt3.Rows.Count > 0)
        {
            grdgatepass.DataSource = dt3;
            grdgatepass.DataBind();
        }
        foreach (GridViewRow grd in grdgatepass.Rows)
        {
            Label lblsapp = (Label)grd.FindControl("lblsapp");
            if (lblsapp.Text == "1")
            {
                lblsapp.Text = "Pending";
            }
            else if (lblsapp.Text == "1")
            {
                lblsapp.Text = "Approve";
            }
            else if (lblsapp.Text == "1")
            {
                lblsapp.Text = "Reject";
            }
        }
    }
    public void fillleavee()
    {

        string str = "Select EmployeeHoliday.id,EmployeeHoliday.whid,EmployeeHoliday.employeeid,EmployeeHoliday.fromdate,EmployeeHoliday.Todate,EmployeeHoliday.leavetypeid,EmployeeHoliday.leaveRequestNote,EmployeeHoliday.ApprovalNote,EmployeeHoliday.compid,'Status'=CASE EmployeeHoliday.status WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Rejected'  ELSE  EmployeeHoliday.status END, " +
                    " EmployeeLeaveType.EmployeeLeaveTypeName,EmployeeLeaveType.ID,WareHouseMaster.WareHouseId,WareHouseMaster.Name,EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName+':'+ DepartmentmasterMNC.Departmentname +':'+DesignationMaster.DesignationName as empname ,EmployeeMaster.SuprviserId, " +
                    "DepartmentmasterMNC.id,DesignationMaster.DesignationMasterId from EmployeeMaster left join DepartmentmasterMNC on EmployeeMaster.DeptID= DepartmentmasterMNC.id left join DesignationMaster on EmployeeMaster.DesignationMasterId = DesignationMaster.DesignationMasterId inner join EmployeeHoliday on EmployeeMaster.EmployeeMasterID=EmployeeHoliday.employeeid " +
                    "  inner join EmployeeLeaveType on EmployeeLeaveType.ID = EmployeeHoliday.leavetypeid inner join WareHouseMaster on WareHouseMaster.WareHouseId=EmployeeHoliday.whid where EmployeeHoliday.compid='" + ViewState["Compid"] + "' and EmployeeMaster.SuprviserId='" + ViewState["empid"] + "' ";
        string str1 = "";
        //where EmployeeHoliday.whid='" + ddlstorename.SelectedValue + "'

        str1 = "and EmployeeHoliday.whid = '" + ViewState["Whid"] + "'";


        str1 = str1 + " and EmployeeHoliday.fromdate<='" + DateTime.Now.ToShortDateString() + "' and EmployeeHoliday.Todate>='" + DateTime.Now.ToShortDateString() + "'";



        str = str + str1;
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter ad6 = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        ad6.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();



    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string te = "LeaveApplicationbyAdmin_Supervisor.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        string te = "frmGatepassApproval.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void taskmore_Click(object sender, EventArgs e)
    {
        string te = "EmployeeAttendanceReportDetail.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void remindermore_Click(object sender, EventArgs e)
    {
        string businessvalue = ViewState["empid"].ToString();
        string employeename = ViewState["Employeename"].ToString();
        string te = ("ReminderMaster.aspx?bvalue=" + businessvalue + "&evalue=" + employeename);
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void gridattendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridattendance.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdreminder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdreminder.PageIndex = e.NewPageIndex;
        fillreminder();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillleavee();
    }
    protected void grdgatepass_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdgatepass.PageIndex = e.NewPageIndex;
        filllapproval();
    }
    protected void btnaddlater_Click(object sender, EventArgs e)
    {
        string str3 = "update AttendenceEntryMaster set Varify='" + Convert.ToBoolean(ViewState["devap"]) + "' where AttendanceId=" + ViewState["atid"] + "";

        SqlCommand cmd1 = new SqlCommand(str3, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd1.ExecuteNonQuery();

        str3 = "update AttendenceDeviations set note='" + latereaso.Text + "' where Id=" + ViewState["dev"] + "";
        SqlCommand cmd11 = new SqlCommand(str3, con);

        cmd11.ExecuteNonQuery();
        if (lbllatemessagereason.Text == "In")
        {

            string str32 = "update AttandanceEntryNotes set IntimeNote='" + latereaso.Text + "' where AttendanceId=" + ViewState["atid"] + "";

            SqlCommand cmd12 = new SqlCommand(str32, con);

            cmd12.ExecuteNonQuery();

        }
        else
        {
            string str32 = "update AttandanceEntryNotes set OuttimeNote='" + latereaso.Text + "' where AttendanceId=" + ViewState["atid"] + "";

            SqlCommand cmd12 = new SqlCommand(str32, con);

            cmd12.ExecuteNonQuery();

        }
    }
    protected void btnyes12_Click(object sender, EventArgs e)
    {

        makeattendance();
        filldate();
        fillgrid();
    }
    protected void btnyes121_Click(object sender, EventArgs e)
    {
        string te = "setupcontrolpanelbasic.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void btnyes1212_Click(object sender, EventArgs e)
    {
        string te = "setupcontrolpanelpremium.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }

    protected void selectInbox()
    {
        string mes = "SELECT TOP(10) MsgMaster.MsgDate, MsgMaster.MsgSubject,MsgMaster.MsgDetail,Party_master.Compname,MsgStatusMaster.MsgStatusName,MsgDetail.MsgDetailId,MsgDetail.ToPartyId,MsgDetail.MsgStatusId FROM MsgDetail INNER JOIN MsgMaster ON MsgDetail.MsgId = MsgMaster.MsgId INNER JOIN  MsgStatusMaster ON MsgDetail.MsgStatusId = MsgStatusMaster.MsgStatusId INNER JOIN Party_master ON MsgMaster.FromPartyId = Party_master.PartyID  WHERE MsgDetail.ToPartyId = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "' AND (MsgDetail.MsgStatusId IN (1, 2)) order by Msgdate desc";
        SqlDataAdapter da = new SqlDataAdapter(mes, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        GridView3.DataSource = dt;
        GridView3.DataBind();

        foreach (GridViewRow gdr1 in GridView3.Rows)
        {
            Label Label2 = (Label)gdr1.FindControl("Label2");
            Label Label5 = (Label)gdr1.FindControl("Label5");

            string str = System.DateTime.Now.ToShortDateString();

            if (Convert.ToDateTime(Label2.Text) == Convert.ToDateTime(str))
            {
                Label2.Visible = false;
                Label5.Visible = true;
            }
        }
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        selectInbox();
    }

    protected void GridView3_Sorting1(object sender, GridViewSortEventArgs e)
    {
        //hdnsortExp.Value = e.SortExpression.ToString();
        //hdnsortDir.Value = sortOrder; // sortOrder;
        //selectInbox();
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "MsgStatusName").ToString() == "UNREAD")
            {
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[1].Font.Bold = true;

                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[2].Font.Bold = true;

            }
            Int32 MsgId = 0;
            DataTable dtMain = new DataTable();
            Int32 MsgDetailId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MsgDetailId").ToString());
            dtMain = clsMessage.SelectMsgIdUsingMsgDetailId(MsgDetailId);
            if (dtMain.Rows.Count > 0)
            {
                MsgId = Convert.ToInt32(dtMain.Rows[0]["MsgId"].ToString());
                dtMain = new DataTable();
                dtMain = clsMessage.SelectMsgforFileAttach(MsgId);
                Image img = (Image)e.Row.FindControl("ImgFile");
                if (dtMain.Rows.Count > 0)
                {
                    img.ImageUrl = "~/Account/images/attach.png";
                    img.Visible = true;
                }
                else
                {
                    img.ImageUrl = "";
                    img.Visible = false;
                }
            }
        }
    }
    protected void ddlinternal_SelectedIndexChanged(object sender, EventArgs e)
    {
        panelinter.Visible = false;
        panelsent1.Visible = false;
        panelinbox11.Visible = false;
        paneldelete1.Visible = false;
        paneldraft1.Visible = false;

        if (ddlinternal.SelectedValue == "0")
        {
            selectInbox();
            panelinbox.Visible = true;
            panelcompose.Visible = false;
            panelsent.Visible = false;
            paneldelete.Visible = false;
            paneldrafts.Visible = false;
            panelinbox11.Visible = true;

        }
        if (ddlinternal.SelectedValue == "1")
        {
            panelinbox.Visible = false;
            panelcompose.Visible = true;
            panelinter.Visible = true;
            panelsent.Visible = false;
            paneldelete.Visible = false;
            paneldrafts.Visible = false;
        }
        if (ddlinternal.SelectedValue == "2")
        {
            SelectMsgforSendBox();
            panelcompose.Visible = false;
            panelinbox.Visible = false;
            panelsent.Visible = true;
            paneldelete.Visible = false;
            paneldrafts.Visible = false;
            panelsent1.Visible = true;

        }
        if (ddlinternal.SelectedValue == "3")
        {
            SelectMsgforDeleteBox();
            paneldelete.Visible = true;
            panelcompose.Visible = false;
            panelinbox.Visible = false;
            panelsent.Visible = false;
            paneldrafts.Visible = false;
            paneldelete1.Visible = true;

        }
        if (ddlinternal.SelectedValue == "4")
        {
            SelectMsgforDraft();
            paneldelete.Visible = false;
            panelcompose.Visible = false;
            panelinbox.Visible = false;
            panelsent.Visible = false;
            paneldrafts.Visible = true;
            paneldraft1.Visible = true;

        }
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string te = "messageinbox.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }

    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPartyGrid();
    }
    //protected void chkattachment_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkattachment.Checked == true)
    //    {
    //        string te = "Messagecompose.aspx";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //    }
    //}
    protected void ClearAll1()
    {
        foreach (GridViewRow GR in grdPartyList.Rows)
        {
            CheckBox chk = (CheckBox)GR.FindControl("chkParty");
            chk.Checked = false;
        }
        CheckBox chkhead = (CheckBox)grdPartyList.HeaderRow.Cells[1].FindControl("HeaderChkbox");
        chkhead.Checked = false;
        Session["GridFileAttach1"] = null;
        gridFileAttach.DataSource = Session["GridFileAttach1"];
        gridFileAttach.DataBind();
        TextBox6.Text = "";
        TextBox7.Text = "";
        Session["to"] = null;
        TextBox8.Text = "";
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        bool Gcheck = false;
        if (grdPartyList.Rows.Count > 0)
        {
            foreach (GridViewRow GR in grdPartyList.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                if (chk.Checked == true)
                {
                    Gcheck = true;
                    break;
                }
            }
            if (Gcheck == false)
            {
                lblmsg2.Text = "Please select atleast ONE Party to send Message . ";
                return;
            }
            else
            {
                try
                {
                    Int32 FromPartyId;
                    FromPartyId = Convert.ToInt32(Session["PartyId"].ToString());


                    Int32 MsgId = 0;

                    string ins1 = "";

                    if (CheckBox4.Checked == true)
                    {
                        ins1 = "insert into MsgMaster (FromPartyId,MsgDate,MsgSubject,MsgDetail,Picture,Signature) values('" + FromPartyId + "','" + System.DateTime.Now.ToString() + "','" + TextBox7.Text + "','" + TextBox8.Text + ' ' + ViewState["sign"].ToString() + "','" + CheckBox5.Checked + "','" + ViewState["sign"].ToString() + "')";
                    }

                    if (CheckBox4.Checked == false)
                    {
                        ins1 = "insert into MsgMaster (FromPartyId,MsgDate,MsgSubject,MsgDetail,Picture,Signature) values('" + FromPartyId + "','" + System.DateTime.Now.ToString() + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + CheckBox5.Checked + "','')";
                    }


                    SqlCommand cmd = new SqlCommand(ins1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SqlDataAdapter da = new SqlDataAdapter("select max(MsgId) as MsgId from MsgMaster", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MsgId = Convert.ToInt32(dt.Rows[0]["MsgId"].ToString());
                    }

                    if (MsgId > 0)
                    {
                        foreach (GridViewRow GR in grdPartyList.Rows)
                        {
                            CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                            if (chk.Checked == true)
                            {
                                Int32 ToPartyId = Convert.ToInt32(grdPartyList.DataKeys[GR.RowIndex].Value);
                                Int32 MessageDetailId = clsMessage.InsertMsgDetail(MsgId, ToPartyId);
                            }
                        }
                        if (gridFileAttach.Rows.Count > 0)
                        {
                            foreach (GridViewRow GR in gridFileAttach.Rows)
                            {
                                string FileName = (gridFileAttach.DataKeys[GR.RowIndex].Value.ToString());
                                bool ins = clsMessage.InsertMsgFileAttachDetail(MsgId, FileName);
                            }
                        }
                        lblmsg2.Text = "Message Sent Successfully";
                        ClearAll1();
                    }
                }

                catch (Exception es)
                {
                    Response.Write(es.Message.ToString());
                }

            }
        }
        else
        {
            lblmsg2.Text = "No single party is available to send Message.";
            return;
        }
        PnlFileAttachLbl.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        DataColumn dtcom = new DataColumn();
        dtcom.DataType = System.Type.GetType("System.Int32");
        dtcom.ColumnName = "PartyId";
        dtcom.ReadOnly = false;
        dtcom.Unique = false;
        dtcom.AllowDBNull = true;
        dt.Columns.Add(dtcom);

        DataColumn dtcom1 = new DataColumn();
        dtcom1.DataType = System.Type.GetType("System.String");
        dtcom1.ColumnName = "Name";
        dtcom1.ReadOnly = false;
        dtcom1.Unique = false;
        dtcom1.AllowDBNull = true;
        dt.Columns.Add(dtcom1);

        DataColumn dtcom2 = new DataColumn();
        dtcom2.DataType = System.Type.GetType("System.String");
        dtcom2.ColumnName = "CName";
        dtcom2.ReadOnly = false;
        dtcom2.Unique = false;
        dtcom2.AllowDBNull = true;
        dt.Columns.Add(dtcom2);

        DataColumn dtcom3 = new DataColumn();
        dtcom3.DataType = System.Type.GetType("System.String");
        dtcom3.ColumnName = "Contactperson";
        dtcom3.ReadOnly = false;
        dtcom3.Unique = false;
        dtcom3.AllowDBNull = true;
        dt.Columns.Add(dtcom3);


        string address = "";

        foreach (GridViewRow GR in grdPartyList.Rows)
        {
            CheckBox chk = (CheckBox)GR.FindControl("chkParty");

            if (chk.Checked == true)
            {
                Int32 ToPartyId = Convert.ToInt32(grdPartyList.DataKeys[GR.RowIndex].Value);
                DataRow drow = dt.NewRow();
                drow["PartyId"] = ToPartyId.ToString();

                // drow["Contactperson"] = GR.Cells[1].Text;

                if (ddlusertype.SelectedItem.Text == "Candidate")
                {
                    drow["CName"] = GR.Cells[5].Text;
                    dt.Rows.Add(drow);
                    if (Convert.ToString(address) == "")
                    {
                        address = GR.Cells[5].Text;
                    }
                    else
                    {
                        address = address + "; " + GR.Cells[5].Text;
                    }

                }
                if (ddlusertype.SelectedItem.Text == "Employee")
                {
                    drow["Name"] = GR.Cells[3].Text;
                    dt.Rows.Add(drow);
                    if (Convert.ToString(address) == "")
                    {
                        address = GR.Cells[3].Text;
                    }
                    else
                    {
                        address = address + "; " + GR.Cells[3].Text;
                    }
                }
                if (ddlusertype.SelectedItem.Text == "Customer" || ddlusertype.SelectedItem.Text == "Other" || ddlusertype.SelectedItem.Text == "Vendor" || ddlusertype.SelectedItem.Text == "Admin")
                {
                    drow["Contactperson"] = GR.Cells[1].Text;
                    dt.Rows.Add(drow);
                    if (Convert.ToString(address) == "")
                    {
                        address = GR.Cells[1].Text;
                    }
                    else
                    {
                        address = address + "; " + GR.Cells[1].Text;
                    }
                }
            }
        }

        Session["to"] = dt;

        if (Session["to"] != null)
        {
            TextBox6.Text = address.ToString();
        }
        ModalPopupExtender1.Hide();
        Button15.Enabled = true;

    }
    protected void filljobapplied11()
    {
        ddlcandi11.Items.Clear();

        string str11 = "select VacancyPositionTitle,ID from VacancyPositionTitleMaster where Active='1' order by VacancyPositionTitle asc";
        SqlCommand cmd11 = new SqlCommand(str11, con);
        SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
        DataTable dt11 = new DataTable();
        adp11.Fill(dt11);

        if (dt11.Rows.Count > 0)
        {
            ddlcandi11.DataSource = dt11;
            ddlcandi11.DataTextField = "VacancyPositionTitle";
            ddlcandi11.DataValueField = "ID";
            ddlcandi11.DataBind();
        }
        ddlcandi11.Items.Insert(0, "All");
        ddlcandi11.Items[0].Value = "0";
    }
    protected void fillcompanyname11()
    {
        ddlcompany1.Items.Clear();

        string str = "SELECT distinct Party_master.Compname,Party_master.PartyID FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' order by Party_master.Compname";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlcompany1.DataSource = dt;
        ddlcompany1.DataTextField = "Compname";
        ddlcompany1.DataValueField = "PartyID";
        ddlcompany1.DataBind();

        ddlcompany1.Items.Insert(0, "ALL");
        ddlcompany1.Items[0].Value = "0";
    }
    protected void FillPartyGrid()
    {
        pnlusertypeother.Visible = false;
        pnlusertypecandidate.Visible = false;

        DataTable dt = new DataTable();
        if (Request.QueryString["pid"] != null)
        {
            grdPartyList.Columns[0].Visible = true;
            grdPartyList.Columns[1].Visible = false;
            grdPartyList.Columns[2].Visible = true;
            grdPartyList.Columns[3].Visible = false;
            grdPartyList.Columns[4].Visible = true;
            grdPartyList.Columns[5].Visible = false;
            grdPartyList.Columns[6].Visible = false;

            dt = select("SELECT   distinct Party_master.Whid, Party_master.Compname,PartytTypeMaster.PartType,AccountMaster.Status " +
            " FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId " +
            " where  (Party_master.PartyId='" + Request.QueryString["pid"] + "')and(AccountMaster.Status='1')");
            if (dt.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = dt.Rows[0]["Whid"].ToString();
            }
        }
        else
        {

        }

        if (ddlusertype.SelectedItem.Text == "Employee")
        {
            string str1 = "";
            string mes1 = "";
            if (txtsearch.Text != "")
            {
                mes1 = " and (Employeemaster.EmployeeName like '%" + txtsearch.Text.Replace("'", "''") + "%')";
            }

            str1 = "SELECT distinct Employeemaster.EmployeeName as Name,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID FROM Party_master  inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId  inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID inner join [MessageCenterRightsTbl] on [MessageCenterRightsTbl].[DesignationID]=EmployeeMaster.DesignationMasterId where Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and [MessageCenterRightsTbl].[Employee]=1 and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and EmployeeMaster.Active=1 " + mes1 + " order by Name";

            SqlDataAdapter dal = new SqlDataAdapter(str1, con);
            dt = new DataTable();
            dal.Fill(dt);

            grdPartyList.Columns[0].Visible = true;
            grdPartyList.Columns[1].Visible = false;
            grdPartyList.Columns[2].Visible = false;
            grdPartyList.Columns[3].Visible = true;
            grdPartyList.Columns[4].Visible = false;
            grdPartyList.Columns[5].Visible = false;
            grdPartyList.Columns[6].Visible = false;
        }
        if (ddlusertype.SelectedItem.Text == "Customer" || ddlusertype.SelectedItem.Text == "Other" || ddlusertype.SelectedItem.Text == "Vendor" || ddlusertype.SelectedItem.Text == "Admin")
        {
            pnlusertypeother.Visible = true;

            grdPartyList.Columns[0].Visible = true;
            grdPartyList.Columns[1].Visible = true;
            grdPartyList.Columns[2].Visible = true;
            grdPartyList.Columns[3].Visible = false;
            grdPartyList.Columns[4].Visible = false;
            grdPartyList.Columns[5].Visible = false;
            grdPartyList.Columns[6].Visible = false;


            string mes2 = "";
            if (txtsearch.Text != "")
            {
                mes2 = " and ((Party_master.Compname like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
            }
            if (ddlcompany1.SelectedIndex > 0)
            {
                mes2 += " and Party_master.PartyID='" + ddlcompany1.SelectedValue + "'";
            }

            string str2 = "	SELECT   distinct  Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status,employeemaster.DesignationMasterId FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join employeemaster on employeemaster.PartyID=Party_master.PartyID inner join MessageCenterRightsTbl on MessageCenterRightsTbl.DesignationID=employeemaster.DesignationMasterId where Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and (MessageCenterRightsTbl.Customer=1 OR MessageCenterRightsTbl.Others=1 OR MessageCenterRightsTbl.Vendor=1) and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
            SqlDataAdapter dal = new SqlDataAdapter(str2, con);
            dt = new DataTable();
            dal.Fill(dt);

        }

        if (ddlusertype.SelectedItem.Text == "Candidate")
        {
            pnlusertypecandidate.Visible = true;

            grdPartyList.Columns[0].Visible = true;
            grdPartyList.Columns[1].Visible = false;
            grdPartyList.Columns[2].Visible = false;
            grdPartyList.Columns[3].Visible = false;
            grdPartyList.Columns[4].Visible = false;
            grdPartyList.Columns[5].Visible = true;
            grdPartyList.Columns[6].Visible = true;

            string mes2 = "";
            if (txtsearch.Text != "")
            {
                mes2 = " and ((CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename like '%" + txtsearch.Text.Replace("'", "''") + "%') or (VacancyPositionTitleMaster.VacancyPositionTitle like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
            }
            if (ddlcandi11.SelectedIndex > 0)
            {
                mes2 += " and VacancyPositionTitleMaster.id='" + ddlcandi11.SelectedValue + "'";
            }
            string str2 = "select CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename as CName,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID,CandidateMaster.Jobpositionid,VacancyPositionTitleMaster.VacancyPositionTitle from CandidateMaster inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.id=CandidateMaster.Jobpositionid inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join MessageCenterRightsTbl on CandidateMaster.DesignationMasterId=MessageCenterRightsTbl.DesignationID where Party_master.whid='" + ddlwarehouse.SelectedValue + "' and [MessageCenterRightsTbl].Candidate=1 " + mes2 + " order by CName";

            SqlDataAdapter dal = new SqlDataAdapter(str2, con);
            dt = new DataTable();
            dal.Fill(dt);
        }

        grdPartyList.DataSource = dt;
        grdPartyList.DataBind();

    }
    protected void fillusertype()
    {
        ddlusertype.Items.Clear();

        string emprole = "select PartyTypeId,PartType from [PartytTypeMaster] where compid='" + Session["Comid"] + "' order by PartType";
        SqlCommand cmdrole = new SqlCommand(emprole, con);
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();
        darole.Fill(dtrole);

        ddlusertype.DataSource = dtrole;
        ddlusertype.DataTextField = "PartType";
        ddlusertype.DataValueField = "PartyTypeId";
        ddlusertype.DataBind();

        ddlusertype.SelectedIndex = ddlusertype.Items.IndexOf(ddlusertype.Items.FindByText("Employee"));
    }
    protected void ddlusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcompanyname11();
        filljobapplied11();
        FillPartyGrid();
        ModalPopupExtender1.Show();
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        FillPartyGrid();
        ModalPopupExtender1.Show();
    }
    protected void ddlcompname_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPartyGrid();
        ModalPopupExtender1.Show();
    }
    protected void ddlcandi_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPartyGrid();
        ModalPopupExtender1.Show();
    }
    protected void HeaderChkbox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)grdPartyList.HeaderRow.Cells[0].Controls[1];
        for (int i = 0; i < grdPartyList.Rows.Count; i++)
        {
            CheckBox ch = (CheckBox)grdPartyList.Rows[i].Cells[0].Controls[1];
            ch.Checked = chk.Checked;
        }
    }
    protected void grdPartyList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPartyList.PageIndex = e.NewPageIndex;

        FillPartyGrid();
        ModalPopupExtender1.Show();
    }
    protected void grdPartyList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (grdPartyList.Rows.Count > 0)
            {
                CheckBox cbHeader = (CheckBox)grdPartyList.HeaderRow.FindControl("HeaderChkbox");
                cbHeader.Attributes["onclick"] = "ChangeAllCheckBoxStates(this.checked);";
                List<string> ArrayValues = new List<string>();
                ArrayValues.Add(string.Concat("'", cbHeader.ClientID, "'"));
                foreach (GridViewRow gvr in grdPartyList.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("chkParty");
                    cb.Attributes["onclick"] = "ChangeHeaderAsNeeded();";
                    ArrayValues.Add(string.Concat("'", cb.ClientID, "'"));
                }
                CheckBoxIDsArray.Text = "<script type='text/javascript'>" + "\n" + "<!--" + "\n" + String.Concat("var CheckBoxIDs =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") + "\n // -->" + "\n" + "</script>";

            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            // pnlmsg.Visible = true;
            lblmsg2.Text = "Error in databound : " + ex.Message.ToString();
        }
    }
    protected void grdSentMailList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Int32 MsgId = 0;
            DataTable dtMain = new DataTable();
            MsgId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MsgId").ToString());

            dtMain = new DataTable();
            dtMain = clsMessage.SelectMsgDetailforSentPartyList(MsgId);
            Label sentTo = (Label)e.Row.FindControl("lblSentTo");

            if (dtMain.Rows.Count > 0)
            {
                String ToList = "";
                int i = 0;
                foreach (DataRow DR in dtMain.Rows)
                {

                    if (i >= 1)
                    {
                        ToList = ToList + " , " + DR["CompName"].ToString();
                    }
                    if (i == 0)
                    {
                        ToList = DR["CompName"].ToString();
                        i = 1;
                    }
                    if (ToList.Length > 25)
                    {
                        ToList = ToList + " ....";
                        break;
                    }
                }
                sentTo.Text = ToList.ToString();
            }
            dtMain = new DataTable();
            dtMain = clsMessage.SelectMsgforFileAttach(MsgId);
            Image img = (Image)e.Row.FindControl("ImgFile");
            if (dtMain.Rows.Count > 0)
            {
                img.ImageUrl = "~/Account/images/attach.png";
                img.Visible = true;
            }
            else
            {
                img.ImageUrl = "";
                img.Visible = false;
            }
        }
    }
    protected void grdSentMailList_Sorting(object sender, GridViewSortEventArgs e)
    {
        //hdnsortExp.Value = e.SortExpression.ToString();
        //hdnsortDir.Value = sortOrder; // sortOrder;
        //SelectMsgforSendBox();
    }
    protected void SelectMsgforSendBox()
    {
        string mes = "SELECT DISTINCT TOP(10) MsgMaster.MsgDate,Party_master.Compname as From1, MsgMaster.MsgSubject, MsgMaster.FromPartyId, MsgMaster.MsgId, MsgMaster.MsgStatus FROM MsgDetail INNER JOIN MsgMaster ON MsgDetail.MsgId = MsgMaster.MsgId INNER JOIN MsgStatusMaster ON MsgDetail.MsgStatusId = MsgStatusMaster.MsgStatusId LEFT OUTER JOIN Party_master ON MsgDetail.ToPartyId = Party_master.PartyId WHERE (MsgMaster.FromPartyId = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "') AND ( MsgMaster.MsgStatus != 'Deleted' or MsgMaster.MsgStatus is null )and MsgDetail.MsgStatusId not in(3) order by Msgdate desc";

        SqlDataAdapter da = new SqlDataAdapter(mes, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        grdSentMailList.DataSource = dt;
        grdSentMailList.DataBind();

        foreach (GridViewRow gdr1 in grdSentMailList.Rows)
        {
            Label Label21z = (Label)gdr1.FindControl("Label21z");
            Label Label51z = (Label)gdr1.FindControl("Label51z");

            string str = System.DateTime.Now.ToShortDateString();

            if (Convert.ToDateTime(Label21z.Text) == Convert.ToDateTime(str))
            {
                Label21z.Visible = false;
                Label51z.Visible = true;
            }
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        string te = "Messagesent.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void grdSentMailList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSentMailList.PageIndex = e.NewPageIndex;
        SelectMsgforSendBox();
    }
    protected void gridDelete_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Int32 MsgId = 0;
            DataTable dtMain = new DataTable();
            Int32 MsgDetailId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MsgDetailId").ToString());
            dtMain = clsMessage.SelectMsgIdUsingMsgDetailId(MsgDetailId);
            if (dtMain.Rows.Count > 0)
            {
                MsgId = Convert.ToInt32(dtMain.Rows[0]["MsgId"].ToString());
                dtMain = new DataTable();
                dtMain = clsMessage.SelectMsgforFileAttach(MsgId);
                Image img = (Image)e.Row.FindControl("ImgFile");


                if (dtMain.Rows.Count > 0)
                {
                    img.ImageUrl = "~/Account/images/attach.png";
                    img.Visible = true;
                }
                else
                {
                    img.ImageUrl = "";
                    img.Visible = false;
                }
            }
        }
    }
    protected void gridDelete_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridDelete.PageIndex = e.NewPageIndex;
        SelectMsgforDeleteBox();
    }
    protected void gridDelete_Sorting(object sender, GridViewSortEventArgs e)
    {
        //hdnsortExp.Value = e.SortExpression.ToString();
        //hdnsortDir.Value = sortOrder;
        //SelectMsgforDeleteBox();
    }
    protected void SelectMsgforDeleteBox()
    {
        string mes = "SELECT TOP(10) MsgMaster.MsgDate, MsgMaster.MsgSubject, Party_master.Compname, MsgStatusMaster.MsgStatusName, MsgDetail.MsgDetailId, MsgDetail.ToPartyId,MsgDetail.MsgStatusId FROM MsgDetail INNER JOIN MsgMaster ON MsgDetail.MsgId = MsgMaster.MsgId INNER JOIN  MsgStatusMaster ON MsgDetail.MsgStatusId = MsgStatusMaster.MsgStatusId INNER JOIN Party_master ON MsgMaster.FromPartyId = Party_master.PartyId  WHERE (MsgDetail.ToPartyId = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "') AND (MsgDetail.MsgStatusId IN (4)) order by Msgdate desc";

        SqlDataAdapter da = new SqlDataAdapter(mes, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gridDelete.DataSource = dt;
        gridDelete.DataBind();

        foreach (GridViewRow gdr1 in gridDelete.Rows)
        {
            Label Label21x = (Label)gdr1.FindControl("Label21x");
            Label Label51x = (Label)gdr1.FindControl("Label51x");

            string str = System.DateTime.Now.ToShortDateString();

            if (Convert.ToDateTime(Label21x.Text) == Convert.ToDateTime(str))
            {
                Label21x.Visible = false;
                Label51x.Visible = true;
            }
        }

    }
    protected void gridDraft_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridDraft.PageIndex = e.NewPageIndex;
        SelectMsgforDraft();
    }
    protected void gridDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gridDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Int32 MsgId = 0;
            DataTable dtMain = new DataTable();
            MsgId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MsgId").ToString());
            dtMain = new DataTable();
            dtMain = clsMessage.SelectMsgDetailforDraftPartyList(MsgId);
            Label sentTo = (Label)e.Row.FindControl("lblSentTo");

            if (dtMain.Rows.Count > 0)
            {
                String ToList = "";
                int i = 0;
                foreach (DataRow DR in dtMain.Rows)
                {

                    if (i >= 1)
                    {
                        ToList = ToList + " , " + DR["Compname"].ToString();
                    }
                    if (i == 0)
                    {
                        ToList = DR["Compname"].ToString();
                        i = 1;
                    }
                    if (ToList.Length > 25)
                    {
                        ToList = ToList + " ....";
                        break;
                    }
                }
                sentTo.Text = ToList.ToString();
            }
            dtMain = new DataTable();
            dtMain = clsMessage.SelectMsgforFileAttach(MsgId);
            Image img = (Image)e.Row.FindControl("ImgFile");


            if (dtMain.Rows.Count > 0)
            {
                img.ImageUrl = "~/Account/images/attach.png";
                img.Visible = true;
            }
            else
            {
                img.ImageUrl = "";
                img.Visible = false;
            }
        }
    }
    protected void SelectMsgforDraft()
    {
        string mes = "SELECT distinct TOP(10) MsgMaster.MsgDate, MsgMaster.MsgSubject,Party_master.Compname, MsgStatusMaster.MsgStatusName,MsgDetail.MsgStatusId,MsgMaster.FromPartyId, MsgMaster.MsgId FROM MsgDetail INNER JOIN MsgMaster ON MsgDetail.MsgId = MsgMaster.MsgId INNER JOIN MsgStatusMaster ON MsgDetail.MsgStatusId = MsgStatusMaster.MsgStatusId inner join Party_master on Party_master.PartyID=MsgMaster.FromPartyId inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where Party_master.PartyID = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "' and MsgDetail.MsgStatusId in(3) order by Msgdate desc";

        SqlDataAdapter da = new SqlDataAdapter(mes, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gridDraft.DataSource = dt;
        gridDraft.DataBind();

        foreach (GridViewRow gdr1 in gridDraft.Rows)
        {
            Label Label21y = (Label)gdr1.FindControl("Label21y");
            Label Label51y = (Label)gdr1.FindControl("Label51y");

            string str = System.DateTime.Now.ToShortDateString();

            if (Convert.ToDateTime(Label21y.Text) == Convert.ToDateTime(str))
            {
                Label21y.Visible = false;
                Label51y.Visible = true;
            }
        }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        string te = "Messagedeleteditems.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        string te = "Messagedrafts.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void fillstore11()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
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
    protected void ddlexternal_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg3.Text = "";
        lblmsg1.Text = "";

        panelexter.Visible = false;
        panelinboxext1.Visible = false;
        panelsentext1.Visible = false;
        paneldeleteext1.Visible = false;
        paneldraftsext1.Visible = false;

        if (ddlexternal.SelectedValue == "0")
        {
            SelectMsgforInbox();
            panelinboxext.Visible = true;
            panelcomposeext.Visible = false;
            panelsentext.Visible = false;
            paneldeleteext.Visible = false;
            paneldraftsext.Visible = false;
            panelinboxext1.Visible = true;
        }
        if (ddlexternal.SelectedValue == "1")
        {
            panelexter.Visible = true;
            panelcomposeext.Visible = true;
            panelinboxext.Visible = false;
            panelsentext.Visible = false;
            paneldeleteext.Visible = false;
            paneldraftsext.Visible = false;
        }
        if (ddlexternal.SelectedValue == "2")
        {
            SelectMsgforSendBoxext();
            panelinboxext.Visible = false;
            panelcomposeext.Visible = false;
            panelsentext.Visible = true;
            paneldeleteext.Visible = false;
            paneldraftsext.Visible = false;
            panelsentext1.Visible = true;
        }
        if (ddlexternal.SelectedValue == "3")
        {
            SelectMsgforDeleteBoxext();
            panelinboxext.Visible = false;
            panelcomposeext.Visible = false;
            panelsentext.Visible = false;
            paneldeleteext.Visible = true;
            paneldraftsext.Visible = false;
            paneldeleteext1.Visible = true;
        }
        if (ddlexternal.SelectedValue == "4")
        {
            SelectMsgforDraftext();
            paneldraftsext.Visible = true;
            panelinboxext.Visible = false;
            panelcomposeext.Visible = false;
            panelsentext.Visible = false;
            paneldeleteext.Visible = false;
            paneldraftsext1.Visible = true;
        }
    }
    protected void fillddl1()
    {
        DataTable dtcomemail = new DataTable();
        DataTable dtempemail = new DataTable();
        MasterCls clsMaster = new MasterCls();

        dtempemail = clsMaster.SelectEmpEmail(Convert.ToInt32(Session["EmployeeId"]));
        if (dtempemail.Rows.Count > 0)
        {
            if (dtempemail.Rows[0]["Email"] != System.DBNull.Value)
            {
                empeml = dtempemail.Rows[0]["Email"].ToString();
            }
        }
        dtcomemail = select("select Distinct InOutCompanyEmail.ID as CompanyEmailId,InOutCompanyEmail.InEmailID as EmailId  from InOutCompanyEmail inner join " +
                            " CompanyEmailAssignAccessRights  on InOutCompanyEmail.ID=CompanyEmailAssignAccessRights.CompanyEmailId  where CompanyEmailAssignAccessRights.CompanyEmailId in(select CompanyEmailAssignAccessRights.CompanyEmailId from CompanyEmailAssignAccessRights where EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' and DeleteRights='true' and viewRights='true' and sendrights='true')");
        ddlempemail.DataSource = dtcomemail;
        ddlempemail.DataBind();

        if (empeml != "")
        {

            ddlempemail.Items.Insert(0, empeml);
            ddlempemail.SelectedIndex = 0;
        }
    }
    protected void ddlempemail_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlempemail.SelectedIndex == 0)
        {
            dwnemail();
        }
        if (ddlempemail.SelectedIndex > 0)
        {
            dwnemail1();
        }
        SelectMsgforInbox();
    }
    protected void taskmore_Click1(object sender, EventArgs e)
    {
        string te = "Messageinboxext.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void SelectMsgforInbox()
    {
        string mes = "";

        if (ddlempemail.SelectedIndex == 0)
        {
            mes = "SELECT TOP(10) MsgMasterExt.MsgDate, MsgMasterExt.MsgSubject,MsgBodyExt.MsgDetail,CASE WHEN (Party_Master.Compname IS NULL)THEN 'MsgMasterExt.FromEmail' ELSE  Party_Master.Compname END as Compname, MsgStatusMasterExt.MsgStatusName, MsgDetailExt.MsgDetailId, MsgDetailExt.ToPartyId,MsgDetailExt.MsgStatusId FROM MsgDetailExt INNER JOIN MsgMasterExt ON MsgDetailExt.MsgId = MsgMasterExt.MsgId inner join MsgBodyExt on MsgBodyExt.MsgId=MsgMasterExt.MsgId INNER JOIN  MsgStatusMasterExt ON MsgDetailExt.MsgStatusId = MsgStatusMasterExt.MsgStatusId Left Join Party_Master ON MsgMasterExt.FromPartyId = Party_Master.PartyId WHERE (MsgDetailExt.ToPartyId = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "' OR MsgDetailExt.ToEmailID = '" + ddlempemail.SelectedItem.Text + "')  AND (MsgDetailExt.MsgStatusId IN (1, 2)) order by Msgdate desc";

        }
        else if (ddlempemail.SelectedIndex > 0)
        {
            mes = "SELECT TOP(10) MsgMasterExt.MsgDate, MsgMasterExt.MsgSubject,MsgBodyExt.MsgDetail,CASE WHEN (Party_Master.Compname IS NULL)THEN 'MsgMasterExt.FromEmail' ELSE  Party_Master.Compname END as Compname, MsgStatusMasterExt.MsgStatusName, MsgDetailExt.MsgDetailId, MsgDetailExt.ToPartyId,MsgDetailExt.MsgStatusId FROM MsgDetailExt INNER JOIN MsgMasterExt ON MsgDetailExt.MsgId = MsgMasterExt.MsgId inner join MsgBodyExt on MsgBodyExt.MsgId=MsgMasterExt.MsgId  INNER JOIN  MsgStatusMasterExt ON MsgDetailExt.MsgStatusId = MsgStatusMasterExt.MsgStatusId Left Join Party_Master ON MsgMasterExt.FromPartyId = Party_Master.PartyId WHERE (MsgDetailExt.ToEmailID = '" + ddlempemail.SelectedItem.Text + "')  AND (MsgDetailExt.MsgStatusId IN (1, 2)) order by Msgdate desc";

        }

        SqlDataAdapter da = new SqlDataAdapter(mes, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gridInbox.DataSource = dt;
        gridInbox.DataBind();

        foreach (GridViewRow gdr1 in gridInbox.Rows)
        {
            Label Label21q = (Label)gdr1.FindControl("Label21q");
            Label Label51q = (Label)gdr1.FindControl("Label51q");

            string str = System.DateTime.Now.ToShortDateString();

            if (Convert.ToDateTime(Label21q.Text) == Convert.ToDateTime(str))
            {
                Label21q.Visible = false;
                Label51q.Visible = true;
            }
        }
    }
    protected void gridInbox_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ////
            if (DataBinder.Eval(e.Row.DataItem, "MsgStatusName").ToString() == "UNREAD")
            {
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[1].Font.Bold = true;

                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[2].Font.Bold = true;
            }
            Int32 MsgId = 0;
            DataTable dtMain = new DataTable();
            Int32 MsgDetailId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MsgDetailId").ToString());
            dtMain = clsMessage.SelectMsgIdUsingMsgDetailIdExt(MsgDetailId);
            if (dtMain.Rows.Count > 0)
            {
                MsgId = Convert.ToInt32(dtMain.Rows[0]["MsgId"].ToString());
                dtMain = new DataTable();
                dtMain = clsMessage.SelectMsgforFileAttachExt(MsgId);
                Image img = (Image)e.Row.FindControl("ImgFile");
                if (dtMain.Rows.Count > 0)
                {
                    img.ImageUrl = "~/Account/images/attach.png";
                    img.Visible = true;
                }
                else
                {
                    img.ImageUrl = "";
                    img.Visible = false;
                }
            }
        }

    }
    protected void gridInbox_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridInbox.PageIndex = e.NewPageIndex;
        SelectMsgforInbox();
    }
    protected void gridInbox_Sorting(object sender, GridViewSortEventArgs e)
    {
        //hdnsortExp.Value = e.SortExpression.ToString();
        //hdnsortDir.Value = sortOrder; // sortOrder;
        //SelectMsgforInbox();
    }
    protected void fillstore1123()
    {
        ddlwarehouseext.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlwarehouseext.DataSource = ds;
        ddlwarehouseext.DataTextField = "Name";
        ddlwarehouseext.DataValueField = "WareHouseId";
        ddlwarehouseext.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlwarehouseext.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
    protected void ddlwarehouseext_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPartyGridext();
    }
    protected void FillPartyGridext()
    {
        Panel22.Visible = false;
        Panel23.Visible = false;

        DataTable dt = new DataTable();

        if (ddlpartytype.SelectedItem.Text == "Employee")
        {
            string str1 = "";
            string mes1 = "";

            if (TextBox5.Text != "")
            {
                mes1 = " and (Employeemaster.EmployeeName like '%" + TextBox5.Text.Replace("'", "''") + "%')";
            }

            str1 = "SELECT distinct Employeemaster.EmployeeName as Name,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID FROM Party_master  inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId  inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID  where Party_master.Whid='" + ddlwarehouseext.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlpartytype.SelectedValue + "' and EmployeeMaster.Active=1 " + mes1 + "  order by Name";


            SqlDataAdapter dal = new SqlDataAdapter(str1, con);
            dt = new DataTable();
            dal.Fill(dt);

            GridView4.Columns[0].Visible = true;
            GridView4.Columns[1].Visible = false;
            GridView4.Columns[2].Visible = false;
            GridView4.Columns[3].Visible = true;
            GridView4.Columns[4].Visible = false;
            GridView4.Columns[5].Visible = false;
            GridView4.Columns[6].Visible = false;
        }

        if (ddlpartytype.SelectedItem.Text == "Customer" || ddlpartytype.SelectedItem.Text == "Other" || ddlpartytype.SelectedItem.Text == "Vendor" || ddlpartytype.SelectedItem.Text == "Admin")
        {
            Panel22.Visible = true;

            GridView4.Columns[0].Visible = true;
            GridView4.Columns[1].Visible = true;
            GridView4.Columns[2].Visible = true;
            GridView4.Columns[3].Visible = false;
            GridView4.Columns[4].Visible = false;
            GridView4.Columns[5].Visible = false;
            GridView4.Columns[6].Visible = false;


            string mes2 = "";
            if (TextBox5.Text != "")
            {
                mes2 = " and ((Party_master.Compname like '%" + TextBox5.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + TextBox5.Text.Replace("'", "''") + "%'))";
            }
            if (ddlcompname.SelectedIndex > 0)
            {
                mes2 += " and Party_master.PartyID='" + ddlcompname.SelectedValue + "'";
            }

            string str2 = "SELECT   distinct  Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status " +
                        " FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId " +
                        " where Party_master.Whid='" + ddlwarehouseext.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlpartytype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
            SqlDataAdapter dal = new SqlDataAdapter(str2, con);
            dt = new DataTable();
            dal.Fill(dt);
        }
        if (ddlpartytype.SelectedItem.Text == "Candidate")
        {
            Panel23.Visible = true;

            GridView4.Columns[0].Visible = true;
            GridView4.Columns[1].Visible = false;
            GridView4.Columns[2].Visible = false;
            GridView4.Columns[3].Visible = false;
            GridView4.Columns[4].Visible = false;
            GridView4.Columns[5].Visible = true;
            GridView4.Columns[6].Visible = true;

            string mes2 = "";
            if (TextBox5.Text != "")
            {
                mes2 = " and ((CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename like '%" + TextBox5.Text.Replace("'", "''") + "%') or (VacancyPositionTitleMaster.VacancyPositionTitle like '%" + TextBox5.Text.Replace("'", "''") + "%'))";
            }
            if (ddlcandi.SelectedIndex > 0)
            {
                mes2 += " and VacancyPositionTitleMaster.id='" + ddlcandi.SelectedValue + "'";
            }

            string str2 = "select CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename as CName,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID,CandidateMaster.Jobpositionid,VacancyPositionTitleMaster.VacancyPositionTitle from CandidateMaster inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.id=CandidateMaster.Jobpositionid inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID where Party_master.whid='" + ddlwarehouseext.SelectedValue + "' " + mes2 + " order by CName";
            SqlDataAdapter dal = new SqlDataAdapter(str2, con);
            dt = new DataTable();
            dal.Fill(dt);

        }

        GridView4.DataSource = dt;
        GridView4.DataBind();

    }
    //protected void chkattachmentext_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkattachmentext.Checked == true)
    //    {
    //        string te = "Messagecomposeext.aspx";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //    }
    //}
    protected void imgbtnsend_Click(object sender, EventArgs e)
    {
        if (DropDownList15.SelectedIndex == 0)
        {
            Int32 prtid = 0;
            Int32 folderid = 0;
            bool Gcheck = false;

            if (!lblAddresses.Text.Contains("@"))
            {
                if (GridView4.Rows.Count > 0)
                {
                    foreach (GridViewRow GR in GridView4.Rows)
                    {
                        CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                        if (chk.Checked == true)
                        {
                            Gcheck = true;
                            break;
                        }
                    }
                    if (Gcheck == false)
                    {
                        lblmsg1.Text = "Please select atleast ONE Party to send Message . ";
                        return;
                    }
                    else
                    {
                        try
                        {
                            Int32 FromPartyId;
                            FromPartyId = Convert.ToInt32(Session["PartyId"].ToString());

                            Int32 MsgId = 0;

                            string ins1 = "insert into MsgMasterExt (FromPartyId,MsgDate,MsgSubject,FromEmail,CID,BusinessID) values('" + FromPartyId + "','" + System.DateTime.Now.ToString() + "','" + txtsub.Text + "','" + DropDownList15.SelectedItem.Text + "','" + Session["Comid"].ToString() + "','" + ddlwarehouseext.SelectedValue + "')";
                            SqlCommand cmd = new SqlCommand(ins1, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();

                            SqlDataAdapter da = new SqlDataAdapter("select max(MsgId) as MsgId from MsgMasterExt", con);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                MsgId = Convert.ToInt32(dt.Rows[0]["MsgId"].ToString());
                            }

                            string ins2 = "";

                            if (chksignature.Checked == true)
                            {
                                ins2 = "insert into Msgbodyext values('" + MsgId + "','" + TxtMsgDetail.Text + ' ' + ViewState["sign"].ToString() + "','" + chkpicture.Checked + "','" + ViewState["sign"].ToString() + "')";
                            }

                            if (chksignature.Checked == false)
                            {
                                ins2 = "insert into Msgbodyext values('" + MsgId + "','" + TxtMsgDetail.Text + "','" + chkpicture.Checked + "','')";
                            }

                            SqlCommand cmd1 = new SqlCommand(ins2, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd1.ExecuteNonQuery();
                            con.Close();


                            if (MsgId > 0)
                            {
                                foreach (GridViewRow GR in GridView4.Rows)
                                {
                                    CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                                    if (chk.Checked == true)
                                    {
                                        Int32 ToPartyId = Convert.ToInt32(GridView4.DataKeys[GR.RowIndex].Value);
                                        Int32 MessageDetailId = clsMessage.InsertMsgDetailExt(MsgId, ToPartyId, 1);
                                        prtemail = getpartyemail(ToPartyId);
                                        prtid = ToPartyId;
                                    }
                                }
                                if (gridattachext.Rows.Count > 0)
                                {
                                    foreach (GridViewRow GR in gridattachext.Rows)
                                    {
                                        string FileName = (gridattachext.DataKeys[GR.RowIndex].Value.ToString());
                                        ViewState["attachment"] = FileName;
                                        flnam = FileName;
                                        bool ins = clsMessage.InsertMsgFileAttachDetailExt(MsgId, FileName);
                                    }
                                }
                                if (folderid == 0)
                                {
                                    DataTable dtgen = new DataTable();
                                    dtgen = clsDocument.SelectGeneralFolderId(ddlwarehouseext.SelectedValue);
                                    if (dtgen.Rows.Count > 0)
                                    {
                                        if (dtgen.Rows[0]["DocumentTypeId"] != System.DBNull.Value)
                                        {
                                            folderid = Convert.ToInt32(dtgen.Rows[0]["DocumentTypeId"].ToString());
                                        }
                                    }
                                }
                                string doc_email_desc = "";
                                string refst = "";
                                Double amt = 0.0;
                                string extstr = "";
                                string doc_title = "";
                                doc_title = "Sent To:" + prtemail + "," + txtsub.Text + "," + System.DateTime.Now.ToString();

                                if (Convert.ToString(ViewState["sentt"]) != "dc")
                                {
                                    if (ViewState["attachment"] != null)
                                    {
                                        bool autoprcss = false;
                                        string Location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");
                                        string Location2 = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");

                                        if (gridattachext.Rows.Count > 0)
                                        {
                                            foreach (GridViewRow GR in gridattachext.Rows)
                                            {
                                                string FileName = (gridattachext.DataKeys[GR.RowIndex].Value.ToString());

                                                Int32 rst45 = clsDocument.InsertDocumentMaster(folderid, 2, Convert.ToDateTime(System.DateTime.Now.ToString()), FileName, doc_title, doc_email_desc, prtid, refst, Convert.ToDecimal(amt), 1, Convert.ToDateTime(System.DateTime.Now.ToString()), extstr,"","");
                                                if (rst45 > 0)
                                                {
                                                    DataTable dtemail = new DataTable();
                                                    dtemail = SelectEmpEmaildetail();

                                                    string flext = Path.GetExtension(Location + FileName);

                                                    if (flext == ".pdf")
                                                    {
                                                        string filepath = Server.MapPath("~//ShoppingCart//Admin//pdftoimage.exe");
                                                        System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);
                                                        //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
                                                        //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");
                                                        pti.UseShellExecute = false;
                                                        //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                                                        pti.Arguments = filepath + " -i UploadedDocuments//" + FileName + " " + "-o" + " " + "DocumentImage//";

                                                        pti.RedirectStandardOutput = true;
                                                        pti.RedirectStandardInput = true;
                                                        pti.RedirectStandardError = true;
                                                        //pti.WorkingDirectory = "D:\\Capman.ifilecabinet.com1\\Account\\";
                                                        pti.WorkingDirectory = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//");
                                                        System.Diagnostics.Process ps = System.Diagnostics.Process.Start(pti);
                                                    }
                                                    if (System.IO.File.Exists(Location2 + FileName))
                                                    {
                                                    }
                                                    else
                                                    {
                                                        System.IO.File.Copy(Location + FileName, Location2 + FileName);
                                                    }
                                                    System.IO.File.SetAttributes(Location + FileName, System.IO.FileAttributes.Hidden);
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        Int32 rst45 = clsDocument.InsertDocumentMaster(folderid, 2, Convert.ToDateTime(System.DateTime.Now.ToString()), txtsub.Text, doc_title, doc_email_desc, prtid, refst, Convert.ToDecimal(amt), 1, Convert.ToDateTime(System.DateTime.Now.ToString()), extstr,"","");
                                    }
                                }
                                if (prtemail != "")
                                {
                                    ViewState["sentt"] = "";
                                    sendmail11();
                                }
                                lblmsg1.Text = "Message Sent Successfully";
                                ClearAll();
                            }
                        }
                        catch (Exception es)
                        {
                            Response.Write(es.Message.ToString());
                        }

                    }
                }
            }
            else if (lblAddresses.Text.Contains("@"))
            {
                try
                {
                    Int32 FromPartyId;
                    FromPartyId = Convert.ToInt32(Session["PartyId"].ToString());

                    Int32 MsgId = 0;

                    string ins1 = "insert into MsgMasterExt (FromPartyId,MsgDate,MsgSubject,FromEmail,CID,BusinessID) values('" + FromPartyId + "','" + System.DateTime.Now.ToString() + "','" + txtsub.Text + "','" + DropDownList15.SelectedItem.Text + "','" + Session["Comid"].ToString() + "','" + ddlwarehouseext.SelectedValue + "')";
                    SqlCommand cmd = new SqlCommand(ins1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SqlDataAdapter da = new SqlDataAdapter("select max(MsgId) as MsgId from MsgMasterExt", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MsgId = Convert.ToInt32(dt.Rows[0]["MsgId"].ToString());
                    }

                    string ins2 = "";

                    if (chksignature.Checked == true)
                    {
                        ins2 = "insert into Msgbodyext values('" + MsgId + "','" + TxtMsgDetail.Text + ' ' + ViewState["sign"].ToString() + "','" + chkpicture.Checked + "','" + ViewState["sign"].ToString() + "')";
                    }

                    if (chksignature.Checked == false)
                    {
                        ins2 = "insert into Msgbodyext values('" + MsgId + "','" + TxtMsgDetail.Text + "','" + chkpicture.Checked + "','')";
                    }

                    SqlCommand cmd1 = new SqlCommand(ins2, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    if (MsgId > 0)
                    {
                        string emaill = lblAddresses.Text;
                        SqlCommand cmdll = new SqlCommand("insert into msgdetailext(msgid,toemailid,msgstatusid) values('" + MsgId + "','" + emaill + "',1)", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdll.ExecuteNonQuery();
                        con.Close();

                        if (gridattachext.Rows.Count > 0)
                        {
                            foreach (GridViewRow GR in gridattachext.Rows)
                            {
                                string FileName = (gridattachext.DataKeys[GR.RowIndex].Value.ToString());
                                ViewState["attachment"] = FileName;
                                flnam = FileName;
                                bool ins = clsMessage.InsertMsgFileAttachDetailExt(MsgId, FileName);
                            }
                        }


                        if (folderid == 0)
                        {
                            //Int32 i = 0;
                            DataTable dtgen = new DataTable();
                            dtgen = clsDocument.SelectGeneralFolderId(ddlwarehouseext.SelectedValue);
                            if (dtgen.Rows.Count > 0)
                            {
                                if (dtgen.Rows[0]["DocumentTypeId"] != System.DBNull.Value)
                                {
                                    folderid = Convert.ToInt32(dtgen.Rows[0]["DocumentTypeId"].ToString());
                                }
                            }
                        }
                    }
                    string doc_email_desc = "";
                    string refst = "";
                    Double amt = 0.0;
                    string extstr = "";
                    string doc_title = "";
                    doc_title = "Sent To:" + lblAddresses.Text + "," + txtsub.Text + "," + System.DateTime.Now.ToString();

                    if (Convert.ToString(ViewState["sentt"]) != "dc")
                    {
                        if (ViewState["attachment"] != null)
                        {
                            bool autoprcss = false;
                            string Location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");
                            string Location2 = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");

                            if (gridattachext.Rows.Count > 0)
                            {
                                foreach (GridViewRow GR in gridattachext.Rows)
                                {
                                    string FileName = (gridattachext.DataKeys[GR.RowIndex].Value.ToString());

                                    Int32 rst45 = clsDocument.InsertDocumentMaster(folderid, 2, Convert.ToDateTime(System.DateTime.Now.ToString()), FileName, doc_title, doc_email_desc, prtid, refst, Convert.ToDecimal(amt), 1, Convert.ToDateTime(System.DateTime.Now.ToString()), extstr,"","");
                                    if (rst45 > 0)
                                    {
                                        DataTable dtemail = new DataTable();
                                        dtemail = SelectEmpEmaildetail();

                                        string flext = Path.GetExtension(Location + FileName);
                                        if (flext == ".pdf")
                                        {
                                            string filepath = Server.MapPath("~//ShoppingCart//Admin//pdftoimage.exe");
                                            System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);
                                            //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
                                            //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");
                                            pti.UseShellExecute = false;
                                            //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                                            pti.Arguments = filepath + " -i UploadedDocuments//" + FileName + " " + "-o" + " " + "DocumentImage//";

                                            pti.RedirectStandardOutput = true;
                                            pti.RedirectStandardInput = true;
                                            pti.RedirectStandardError = true;
                                            //pti.WorkingDirectory = "D:\\Capman.ifilecabinet.com1\\Account\\";
                                            pti.WorkingDirectory = Server.MapPath("~//Account//");
                                            System.Diagnostics.Process ps = System.Diagnostics.Process.Start(pti);
                                        }
                                        if (System.IO.File.Exists(Location2 + FileName))
                                        {
                                        }
                                        else
                                        {
                                            System.IO.File.Copy(Location + FileName, Location2 + FileName);
                                        }
                                        System.IO.File.SetAttributes(Location + FileName, System.IO.FileAttributes.Hidden);
                                    }
                                }
                            }

                        }
                        else
                        {
                            Int32 rst45 = clsDocument.InsertDocumentMaster(folderid, 2, Convert.ToDateTime(System.DateTime.Now.ToString()), txtsub.Text, doc_title, doc_email_desc, prtid, refst, Convert.ToDecimal(amt), 1, Convert.ToDateTime(System.DateTime.Now.ToString()), extstr,"","");
                        }
                    }
                    if (lblAddresses.Text != "")
                    {
                        ViewState["sentt"] = "";
                        sendmail11();
                    }
                    lblmsg1.Text = "Message Sent Successfully";
                    ClearAll();
                }
                catch (Exception es)
                {
                    Response.Write(es.Message.ToString());
                }
            }
            else
            {
                lblmsg1.Text = "No single party is available to send Message.";
                return;
            }
        }
        else if (DropDownList15.SelectedIndex > 1)
        {
            Int32 prtid = 0;
            Int32 FromPartyId = 0;
            DataTable dtorgeml = new DataTable();
            dtorgeml = clsMaster.SelectPartyIdfromCompanyEmail(Convert.ToInt32(DropDownList15.SelectedValue));

            if (dtorgeml.Rows.Count > 0)
            {
                if (dtorgeml.Rows[0][1] != System.DBNull.Value)
                {
                    FromPartyId = Convert.ToInt32(dtorgeml.Rows[0][1].ToString());
                }
            }

            Int32 folderid = 0;
            bool Gcheck = false;
            if (!lblAddresses.Text.Contains("@"))
            {
                int flag = 0;

                if (GridView4.Rows.Count > 0)
                {
                    foreach (GridViewRow GR in GridView4.Rows)
                    {
                        CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                        if (chk.Checked == true)
                        {
                            Gcheck = true;
                            break;
                        }
                    }
                    if (Gcheck == false)
                    {
                        lblmsg1.Text = "Please select atleast ONE Party to send Message . ";
                        return;
                    }
                    else
                    {

                        try
                        {

                            Int32 MsgId = 0;

                            string ins1 = "insert into MsgMasterExt (FromPartyId,MsgDate,MsgSubject,FromEmail,CID,BusinessID) values('" + FromPartyId + "','" + System.DateTime.Now.ToString() + "','" + txtsub.Text + "','" + DropDownList15.SelectedItem.Text + "','" + Session["Comid"].ToString() + "','" + ddlwarehouseext.SelectedValue + "')";
                            SqlCommand cmd = new SqlCommand(ins1, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();

                            SqlDataAdapter da = new SqlDataAdapter("select max(MsgId) as MsgId from MsgMasterExt", con);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                MsgId = Convert.ToInt32(dt.Rows[0]["MsgId"].ToString());
                            }

                            string ins2 = "";

                            if (chksignature.Checked == true)
                            {
                                ins2 = "insert into Msgbodyext values('" + MsgId + "','" + TxtMsgDetail.Text + ' ' + ViewState["sign"].ToString() + "','" + chkpicture.Checked + "','" + ViewState["sign"].ToString() + "')";
                            }

                            if (chksignature.Checked == false)
                            {
                                ins2 = "insert into Msgbodyext values('" + MsgId + "','" + TxtMsgDetail.Text + "','" + chkpicture.Checked + "','')";
                            }

                            SqlCommand cmd1 = new SqlCommand(ins2, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd1.ExecuteNonQuery();
                            con.Close();


                            if (MsgId > 0)
                            {
                                foreach (GridViewRow GR in GridView4.Rows)
                                {
                                    CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                                    if (chk.Checked == true)
                                    {
                                        Int32 ToPartyId = Convert.ToInt32(GridView4.DataKeys[GR.RowIndex].Value);
                                        Int32 MessageDetailId = clsMessage.InsertMsgDetailExt(MsgId, ToPartyId, 1);
                                        prtemail = getpartyemail(ToPartyId);
                                        prtid = ToPartyId;
                                    }
                                }
                                if (gridattachext.Rows.Count > 0)
                                {
                                    foreach (GridViewRow GR in gridattachext.Rows)
                                    {
                                        string FileName = (gridattachext.DataKeys[GR.RowIndex].Value.ToString());
                                        ViewState["attachment"] = FileName;
                                        flnam = FileName;
                                        bool ins = clsMessage.InsertMsgFileAttachDetailExt(MsgId, FileName);
                                    }
                                }
                                if (folderid == 0)
                                {
                                    //Int32 i = 0;
                                    DataTable dtgen = new DataTable();
                                    dtgen = clsDocument.SelectGeneralFolderId(ddlwarehouseext.SelectedValue);
                                    if (dtgen.Rows.Count > 0)
                                    {
                                        if (dtgen.Rows[0]["DocumentTypeId"] != System.DBNull.Value)
                                        {
                                            folderid = Convert.ToInt32(dtgen.Rows[0]["DocumentTypeId"].ToString());
                                        }
                                    }
                                }
                                string doc_email_desc = "";
                                string refst = "";
                                Double amt = 0.0;
                                string extstr = "";
                                string doc_title = "";
                                doc_title = "Sent To:" + prtemail + "," + txtsub.Text + "," + System.DateTime.Now.ToString();

                                if (ViewState["attachment"] != null)
                                {
                                    bool autoprcss = false;
                                    string Location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");
                                    string Location2 = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");

                                    if (gridattachext.Rows.Count > 0)
                                    {
                                        foreach (GridViewRow GR in gridattachext.Rows)
                                        {
                                            string FileName = (gridattachext.DataKeys[GR.RowIndex].Value.ToString());

                                            Int32 rst45 = clsDocument.InsertDocumentMaster(folderid, 2, Convert.ToDateTime(System.DateTime.Now.ToString()), FileName, doc_title, doc_email_desc, prtid, refst, Convert.ToDecimal(amt), 1, Convert.ToDateTime(System.DateTime.Now.ToString()), extstr,"","");
                                            if (rst45 > 0)
                                            {
                                                string flext = Path.GetExtension(Location + FileName);
                                                if (flext == ".pdf")
                                                {
                                                    string filepath = Server.MapPath("~//Account//pdftoimage.exe");
                                                    System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);
                                                    //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
                                                    //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");
                                                    pti.UseShellExecute = false;
                                                    //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                                                    pti.Arguments = filepath + " -i UploadedDocuments//" + FileName + " " + "-o" + " " + "DocumentImage//";

                                                    pti.RedirectStandardOutput = true;
                                                    pti.RedirectStandardInput = true;
                                                    pti.RedirectStandardError = true;
                                                    //pti.WorkingDirectory = "D:\\Capman.ifilecabinet.com1\\Account\\";
                                                    pti.WorkingDirectory = Server.MapPath("~//Account//");
                                                    System.Diagnostics.Process ps = System.Diagnostics.Process.Start(pti);
                                                }
                                                if (System.IO.File.Exists(Location2 + FileName))
                                                {
                                                }
                                                else
                                                {
                                                    System.IO.File.Copy(Location + FileName, Location2 + FileName);
                                                }
                                                System.IO.File.SetAttributes(Location + FileName, System.IO.FileAttributes.Hidden);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Int32 rst45 = clsDocument.InsertDocumentMaster(folderid, 2, Convert.ToDateTime(System.DateTime.Now.ToString()), txtsub.Text, doc_title, doc_email_desc, prtid, refst, Convert.ToDecimal(amt), 1, Convert.ToDateTime(System.DateTime.Now.ToString()), extstr,"","");
                                }
                                if (prtemail != "")
                                {
                                    sendmail11();
                                }

                                lblmsg1.Text = "Message Sent Successfully";

                                ClearAll();
                            }

                        }
                        catch (Exception es)
                        {
                            Response.Write(es.Message.ToString());
                        }
                    }
                }
            }
            else if (lblAddresses.Text.Contains("@"))
            {
                try
                {

                    FromPartyId = Convert.ToInt32(Session["PartyId"].ToString());

                    Int32 MsgId = 0;

                    string ins1 = "insert into MsgMasterExt (FromPartyId,MsgDate,MsgSubject,FromEmail,CID,BusinessID) values('" + FromPartyId + "','" + System.DateTime.Now.ToString() + "','" + txtsub.Text + "','" + DropDownList15.SelectedItem.Text + "','" + Session["Comid"].ToString() + "','" + ddlwarehouseext.SelectedValue + "')";
                    SqlCommand cmd = new SqlCommand(ins1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SqlDataAdapter da = new SqlDataAdapter("select max(MsgId) as MsgId from MsgMasterExt", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MsgId = Convert.ToInt32(dt.Rows[0]["MsgId"].ToString());
                    }

                    string ins2 = "";

                    if (chksignature.Checked == true)
                    {
                        ins2 = "insert into Msgbodyext values('" + MsgId + "','" + TxtMsgDetail.Text + ' ' + ViewState["sign"].ToString() + "','" + chkpicture.Checked + "','" + ViewState["sign"].ToString() + "')";
                    }

                    if (chksignature.Checked == false)
                    {
                        ins2 = "insert into Msgbodyext values('" + MsgId + "','" + TxtMsgDetail.Text + "','" + chkpicture.Checked + "','')";
                    }

                    SqlCommand cmd1 = new SqlCommand(ins2, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    if (MsgId > 0)
                    {
                        string emaill = lblAddresses.Text;
                        SqlCommand cmdll = new SqlCommand("insert into msgdetailext(msgid,toemailid,msgstatusid) values('" + MsgId + "','" + emaill + "',1)", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdll.ExecuteNonQuery();
                        con.Close();


                        if (gridattachext.Rows.Count > 0)
                        {
                            foreach (GridViewRow GR in gridattachext.Rows)
                            {
                                string FileName = (gridattachext.DataKeys[GR.RowIndex].Value.ToString());
                                ViewState["attachment"] = FileName;
                                flnam = FileName;
                                bool ins = clsMessage.InsertMsgFileAttachDetailExt(MsgId, FileName);
                            }
                        }

                        if (folderid == 0)
                        {
                            //Int32 i = 0;
                            DataTable dtgen = new DataTable();
                            dtgen = clsDocument.SelectGeneralFolderId(ddlwarehouseext.SelectedValue);
                            if (dtgen.Rows.Count > 0)
                            {
                                if (dtgen.Rows[0]["DocumentTypeId"] != System.DBNull.Value)
                                {
                                    folderid = Convert.ToInt32(dtgen.Rows[0]["DocumentTypeId"].ToString());
                                }
                            }
                        }

                    }
                    string doc_email_desc = "";
                    string refst = "";
                    Double amt = 0.0;
                    string extstr = "";
                    string doc_title = "";
                    doc_title = "Sent To:" + lblAddresses.Text + "," + txtsub.Text + "," + System.DateTime.Now.ToString();

                    if (Convert.ToString(ViewState["sentt"]) != "dc")
                    {
                        if (ViewState["attachment"] != null)
                        {
                            bool autoprcss = false;
                            string Location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");
                            string Location2 = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");

                            if (gridattachext.Rows.Count > 0)
                            {
                                foreach (GridViewRow GR in gridattachext.Rows)
                                {
                                    string FileName = (gridattachext.DataKeys[GR.RowIndex].Value.ToString());

                                    Int32 rst45 = clsDocument.InsertDocumentMaster(folderid, 2, Convert.ToDateTime(System.DateTime.Now.ToString()), FileName, doc_title, doc_email_desc, prtid, refst, Convert.ToDecimal(amt), 1, Convert.ToDateTime(System.DateTime.Now.ToString()), extstr,"","");
                                    if (rst45 > 0)
                                    {
                                        DataTable dtemail = new DataTable();
                                        dtemail = SelectEmpEmaildetail();

                                        string flext = Path.GetExtension(Location + FileName);
                                        if (flext == ".pdf")
                                        {
                                            string filepath = Server.MapPath("~//ShoppingCart//Admin//pdftoimage.exe");
                                            System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);
                                            //string flpt = "D:\\Capman.ifilecabinet.com1\\Account\\test.txt";
                                            //pti.FileName = Server.MapPath("~//Account//pdftoimage.exe");
                                            pti.UseShellExecute = false;
                                            //pti.Arguments = "@"+Server.MapPath("~//Account//") + "pdftoimage -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                                            pti.Arguments = filepath + " -i UploadedDocuments//" + FileName + " " + "-o" + " " + "DocumentImage//";

                                            pti.RedirectStandardOutput = true;
                                            pti.RedirectStandardInput = true;
                                            pti.RedirectStandardError = true;
                                            //pti.WorkingDirectory = "D:\\Capman.ifilecabinet.com1\\Account\\";
                                            pti.WorkingDirectory = Server.MapPath("~//Account//");
                                            System.Diagnostics.Process ps = System.Diagnostics.Process.Start(pti);
                                        }
                                        if (System.IO.File.Exists(Location2 + FileName))
                                        {
                                        }
                                        else
                                        {
                                            System.IO.File.Copy(Location + FileName, Location2 + FileName);
                                        }
                                        System.IO.File.SetAttributes(Location + FileName, System.IO.FileAttributes.Hidden);
                                    }
                                }
                            }

                        }
                        else
                        {
                            Int32 rst45 = clsDocument.InsertDocumentMaster(folderid, 2, Convert.ToDateTime(System.DateTime.Now.ToString()), txtsub.Text, doc_title, doc_email_desc, prtid, refst, Convert.ToDecimal(amt), 1, Convert.ToDateTime(System.DateTime.Now.ToString()), extstr,"","");
                        }
                    }
                    if (lblAddresses.Text != "")
                    {
                        ViewState["sentt"] = "";
                        sendmail11();
                    }
                    lblmsg1.Text = "Message Sent Successfully";
                    ClearAll();
                }
                catch (Exception es)
                {
                    Response.Write(es.Message.ToString());
                }
            }
            else
            {
                lblmsg1.Text = "No single party is available to send Message.";
                return;
            }
        }
        panelattachext.Visible = false;
    }
    protected void ClearAll()
    {
        foreach (GridViewRow GR in GridView4.Rows)
        {
            CheckBox chk = (CheckBox)GR.FindControl("chkParty");
            chk.Checked = false;
        }
        Session["GridFileAttach11"] = null;
        gridattachext.DataSource = Session["GridFileAttach11"];
        gridattachext.DataBind();
        TxtMsgDetail.Text = "";
        txtsub.Text = "";
        Session["to"] = null;
        lblAddresses.Text = "";
    }
    protected void fillddl123()
    {
        DataTable dtcomemail = new DataTable();
        DataTable dtempemail = new DataTable();
        MasterCls clsMaster = new MasterCls();

        dtempemail = clsMaster.SelectEmpEmail(Convert.ToInt32(Session["EmployeeId"]));

        if (dtempemail.Rows.Count > 0)
        {
            if (dtempemail.Rows[0]["Email"] != System.DBNull.Value)
            {
                empeml = dtempemail.Rows[0]["Email"].ToString();
            }
        }
        dtcomemail = select("select Distinct InOutCompanyEmail.ID as CompanyEmailId,InOutCompanyEmail.InEmailID as EmailId  from InOutCompanyEmail inner join " +
                            " CompanyEmailAssignAccessRights  on InOutCompanyEmail.ID=CompanyEmailAssignAccessRights.CompanyEmailId  where CompanyEmailAssignAccessRights.CompanyEmailId in(select CompanyEmailAssignAccessRights.CompanyEmailId from CompanyEmailAssignAccessRights where EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' and DeleteRights='true' and viewRights='true' and sendrights='true')");

        DropDownList15.DataSource = dtcomemail;
        DropDownList15.DataBind();

        if (empeml != "")
        {

            DropDownList15.Items.Insert(0, empeml);
            DropDownList15.SelectedIndex = 0;
        }

    }
    protected void fillddlsent()
    {
        DataTable dtcomemail = new DataTable();
        DataTable dtempemail = new DataTable();
        MasterCls clsMaster = new MasterCls();

        dtempemail = clsMaster.SelectEmpEmail(Convert.ToInt32(Session["EmployeeId"]));

        if (dtempemail.Rows.Count > 0)
        {
            if (dtempemail.Rows[0]["Email"] != System.DBNull.Value)
            {
                empeml = dtempemail.Rows[0]["Email"].ToString();
            }
        }
        dtcomemail = clsMaster.SelectCompanyEmailForEmp(Convert.ToInt32(Session["EmployeeId"]));

        ddlempemailsent.DataSource = dtcomemail;
        ddlempemailsent.DataBind();
        ddlempemailsent.Items.Insert(0, "-Select-");
        ddlempemailsent.SelectedItem.Value = "0";

        if (empeml != "")
        {
            ddlempemailsent.Items.Insert(1, empeml);
            ddlempemailsent.SelectedIndex = 1;
        }

    }
    protected void ddlempemailsent_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforSendBoxext();
    }
    protected void SelectMsgforSendBoxext()
    {
        string str = "";
        DataTable dt = new DataTable();

        if (ddlempemailsent.SelectedIndex == 1)
        {
            str = "SELECT DISTINCT TOP(10) MsgMasterExt.MsgDate, MsgMasterExt.MsgSubject,MsgMasterExt.FromEmail, MsgMasterExt.FromPartyId,MsgDetailExt.ToEmailId, MsgMasterExt.MsgId, MsgMasterExt.MsgStatus FROM MsgDetailExt INNER JOIN MsgMasterExt ON MsgDetailExt.MsgId = MsgMasterExt.MsgId INNER JOIN MsgStatusMasterExt ON MsgDetailExt.MsgStatusId = MsgStatusMasterExt.MsgStatusId LEFT OUTER JOIN Party_master ON MsgDetailExt.ToPartyId = Party_master.PartyId WHERE (MsgMasterExt.FromPartyId = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "') and MsgMasterExt.FromEmail='" + ddlempemailsent.SelectedItem.Text + "'  AND ( MsgMasterExt.MsgStatus != 'Deleted' or MsgMasterExt.MsgStatus is null )and MsgDetailExt.MsgStatusId not in(3) order by Msgdate desc";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da.Fill(dt);
        }
        else if (ddlempemailsent.SelectedIndex > 1)
        {
            str = "SELECT DISTINCT TOP(10) MsgMasterExt.MsgDate, MsgMasterExt.MsgSubject,MsgMasterExt.FromEmail, MsgMasterExt.FromPartyId,MsgDetailExt.ToEmailId, MsgMasterExt.MsgId, MsgMasterExt.MsgStatus FROM MsgDetailExt INNER JOIN MsgMasterExt ON MsgDetailExt.MsgId = MsgMasterExt.MsgId INNER JOIN MsgStatusMasterExt ON MsgDetailExt.MsgStatusId = MsgStatusMasterExt.MsgStatusId LEFT OUTER JOIN Party_master ON MsgDetailExt.ToPartyId = Party_master.PartyId WHERE MsgMasterExt.FromEmail='" + ddlempemailsent.SelectedItem.Text + "'  AND ( MsgMasterExt.MsgStatus != 'Deleted' or MsgMasterExt.MsgStatus is null )and MsgDetailExt.MsgStatusId not in(3) order by Msgdate desc";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da.Fill(dt);
        }

        GridView5.DataSource = dt;
        GridView5.DataBind();

        foreach (GridViewRow gdr1 in GridView5.Rows)
        {
            Label Label21w = (Label)gdr1.FindControl("Label21w");
            Label Label51w = (Label)gdr1.FindControl("Label51w");

            string straa = System.DateTime.Now.ToShortDateString();

            if (Convert.ToDateTime(Label21w.Text) == Convert.ToDateTime(straa))
            {
                Label21w.Visible = false;
                Label51w.Visible = true;
            }
        }
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        string te = "Messagesentext.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Int32 MsgId = 0;
            DataTable dtMain = new DataTable();
            MsgId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MsgId").ToString());
            //
            dtMain = new DataTable();
            dtMain = clsMessage.SelectMsgDetailforSentPartyListExt(MsgId);
            Label sentTo = (Label)e.Row.FindControl("lblSentTo");

            if (dtMain.Rows.Count > 0)
            {
                String ToList = "";
                int i = 0;
                foreach (DataRow DR in dtMain.Rows)
                {
                    if (i >= 1)
                    {
                        ToList = ToList + " , " + DR["Compname"].ToString();
                    }
                    if (i == 0)
                    {
                        ToList = DR["Compname"].ToString();
                        i = 1;
                    }
                    if (ToList.Length > 25)
                    {
                        ToList = ToList + " ....";
                        break;
                    }
                }
                sentTo.Text = ToList.ToString();
            }
            else
            {
                SqlDataAdapter daaaa = new SqlDataAdapter("select ToEmailID from MsgDetailExt where MsgId='" + MsgId + "'", con);
                DataTable dtaaa = new DataTable();
                daaaa.Fill(dtaaa);

                if (dtaaa.Rows.Count > 0)
                {
                    sentTo.Text = Convert.ToString(dtaaa.Rows[0]["ToEmailID"]);
                }
            }

            dtMain = new DataTable();
            dtMain = clsMessage.SelectMsgforFileAttachExt(MsgId);
            Image img = (Image)e.Row.FindControl("ImgFile");
            if (dtMain.Rows.Count > 0)
            {
                img.ImageUrl = "~/Account/images/attach.png";
                img.Visible = true;
            }
            else
            {
                img.ImageUrl = "";
                img.Visible = false;
            }
        }
    }
    protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView5.PageIndex = e.NewPageIndex;
        SelectMsgforSendBoxext();
    }
    protected void fillddldelete()
    {
        DataTable dtcomemail = new DataTable();
        DataTable dtempemail = new DataTable();
        MasterCls clsMaster = new MasterCls();

        dtempemail = clsMaster.SelectEmpEmail(Convert.ToInt32(Session["EmployeeId"]));

        if (dtempemail.Rows.Count > 0)
        {
            if (dtempemail.Rows[0]["Email"] != System.DBNull.Value)
            {
                empeml = dtempemail.Rows[0]["Email"].ToString();
            }
        }
        dtcomemail = select("select Distinct InOutCompanyEmail.ID as CompanyEmailId,InOutCompanyEmail.InEmailID as EmailId  from InOutCompanyEmail inner join " +
                            "CompanyEmailAssignAccessRights  on InOutCompanyEmail.ID=CompanyEmailAssignAccessRights.CompanyEmailId where CompanyEmailAssignAccessRights.CompanyEmailId  " +
                            "in(select CompanyEmailAssignAccessRights.CompanyEmailId from CompanyEmailAssignAccessRights where EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' and Viewrights='true' and sendrights='true' and deleterights='true')");

        DropDownList16.DataSource = dtcomemail;
        DropDownList16.DataBind();
        DropDownList16.Items.Insert(0, "-Select-");
        DropDownList16.SelectedItem.Value = "0";

        if (empeml != "")
        {
            DropDownList16.Items.Insert(1, empeml);
            DropDownList16.SelectedIndex = 1;
        }

    }
    protected void DropDownList16_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforDeleteBoxext();
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        string te = "Messagedeleteditemsext.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Int32 MsgId = 0;
            DataTable dtMain = new DataTable();
            Int32 MsgDetailId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MsgDetailId").ToString());
            dtMain = clsMessage.SelectMsgIdUsingMsgDetailIdExt(MsgDetailId);
            if (dtMain.Rows.Count > 0)
            {
                MsgId = Convert.ToInt32(dtMain.Rows[0]["MsgId"].ToString());
                dtMain = new DataTable();
                dtMain = clsMessage.SelectMsgforFileAttachExt(MsgId);
                Image img = (Image)e.Row.FindControl("ImgFile");


                if (dtMain.Rows.Count > 0)
                {
                    img.ImageUrl = "~/Account/images/attach.png";
                    img.Visible = true;
                }
                else
                {
                    img.ImageUrl = "";
                    img.Visible = false;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Int32 MsgId = 0;
            DataTable dtMain = new DataTable();
            Int32 MsgDetailId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MsgDetailId").ToString());
            dtMain = clsMessage.SelectMsgIdUsingMsgDetailIdExt(MsgDetailId);
            if (dtMain.Rows.Count > 0)
            {
                MsgId = Convert.ToInt32(dtMain.Rows[0]["MsgId"].ToString());
                dtMain = new DataTable();
                dtMain = clsMessage.SelectMsgforFileAttachExt(MsgId);
                Image img = (Image)e.Row.FindControl("ImgFile");


                if (dtMain.Rows.Count > 0)
                {
                    img.ImageUrl = "~/Account/images/attach.png";
                    img.Visible = true;
                }
                else
                {
                    img.ImageUrl = "";
                    img.Visible = false;
                }
            }
        }
    }
    protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView6.PageIndex = e.NewPageIndex;
        SelectMsgforDeleteBoxext();
    }
    protected void SelectMsgforDeleteBoxext()
    {
        DataTable dt = new DataTable();

        string str = "";

        if (DropDownList16.SelectedIndex == 1)
        {
            str = "SELECT TOP(10) MsgMasterExt.MsgDate, MsgMasterExt.MsgSubject,Party_Master.Compname,MsgStatusMasterExt.MsgStatusName, MsgDetailExt.MsgDetailId, MsgDetailExt.ToPartyId, MsgDetailExt.MsgStatusId FROM MsgDetailExt INNER JOIN MsgMasterExt ON MsgDetailExt.MsgId = MsgMasterExt.MsgId INNER JOIN MsgStatusMasterExt ON MsgDetailExt.MsgStatusId = MsgStatusMasterExt.MsgStatusId INNER JOIN Party_Master ON MsgMasterExt.FromPartyId = Party_Master.PartyId WHERE (MsgDetailExt.ToPartyId = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "' OR MsgMasterExt.FromEmail='" + DropDownList16.SelectedItem.Text + "') AND (MsgDetailExt.MsgStatusId IN (4)) ORDER BY MsgMasterExt.MsgDate DESC";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da.Fill(dt);
        }
        else if (DropDownList16.SelectedIndex > 1)
        {
            str = "SELECT TOP(10) MsgMasterExt.MsgDate, MsgMasterExt.MsgSubject,Party_Master.Compname,MsgStatusMasterExt.MsgStatusName, MsgDetailExt.MsgDetailId, MsgDetailExt.ToPartyId, MsgDetailExt.MsgStatusId FROM MsgDetailExt INNER JOIN MsgMasterExt ON MsgDetailExt.MsgId = MsgMasterExt.MsgId INNER JOIN MsgStatusMasterExt ON MsgDetailExt.MsgStatusId = MsgStatusMasterExt.MsgStatusId INNER JOIN Party_Master ON MsgMasterExt.FromPartyId = Party_Master.PartyId WHERE MsgMasterExt.FromEmail='" + DropDownList16.SelectedItem.Text + "' AND (MsgDetailExt.MsgStatusId IN (4)) ORDER BY MsgMasterExt.MsgDate DESC";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da.Fill(dt);
        }
        GridView6.DataSource = dt;
        GridView6.DataBind();

        foreach (GridViewRow gdr1 in GridView6.Rows)
        {
            Label Label21e = (Label)gdr1.FindControl("Label21e");
            Label Label51e = (Label)gdr1.FindControl("Label51e");

            string strbb = System.DateTime.Now.ToShortDateString();

            if (Convert.ToDateTime(Label21e.Text) == Convert.ToDateTime(strbb))
            {
                Label21e.Visible = false;
                Label51e.Visible = true;
            }
        }

    }
    protected void fillddldrafts()
    {
        DataTable dtcomemail = new DataTable();
        DataTable dtempemail = new DataTable();
        MasterCls clsMaster = new MasterCls();

        dtempemail = clsMaster.SelectEmpEmail(Convert.ToInt32(Session["EmployeeId"]));

        if (dtempemail.Rows.Count > 0)
        {
            if (dtempemail.Rows[0]["Email"] != System.DBNull.Value)
            {
                empeml = dtempemail.Rows[0]["Email"].ToString();
            }
        }
        dtcomemail = select("select Distinct InOutCompanyEmail.ID as CompanyEmailId,InOutCompanyEmail.InEmailID as EmailId  from InOutCompanyEmail inner join " +
                            " CompanyEmailAssignAccessRights  on InOutCompanyEmail.ID=CompanyEmailAssignAccessRights.CompanyEmailId  where CompanyEmailAssignAccessRights.CompanyEmailId in(select CompanyEmailAssignAccessRights.CompanyEmailId from CompanyEmailAssignAccessRights where EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' and DeleteRights='true' and viewRights='true' and sendrights='true')");


        DropDownList17.DataSource = dtcomemail;
        DropDownList17.DataBind();
        DropDownList17.Items.Insert(0, "-Select-");
        DropDownList17.SelectedItem.Value = "0";

        if (empeml != "")
        {

            DropDownList17.Items.Insert(1, empeml);
            DropDownList17.SelectedIndex = 1;
        }

    }
    protected void DropDownList17_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforDraftext();
    }
    protected void SelectMsgforDraftext()
    {
        string str = "";

        DataTable dt = new DataTable();

        if (DropDownList17.SelectedIndex == 1)
        {
            str = "SELECT distinct TOP(10) MsgMasterExt.MsgDate, MsgMasterExt.MsgSubject, MsgStatusMasterExt.MsgStatusName,MsgDetailExt.MsgStatusId,MsgMasterExt.FromPartyId,MsgDetailExt.ToEmailId, MsgMasterExt.MsgId FROM MsgDetailExt INNER JOIN MsgMasterExt ON MsgDetailExt.MsgId = MsgMasterExt.MsgId INNER JOIN MsgStatusMasterExt ON MsgDetailExt.MsgStatusId = MsgStatusMasterExt.MsgStatusId where (FromPartyId = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "' OR MsgMasterExt.FromEmail='" + DropDownList17.SelectedItem.Text + "') and MsgDetailExt.MsgStatusId in(3) order by Msgdate desc";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da.Fill(dt);
        }
        else if (DropDownList17.SelectedIndex > 1)
        {
            str = "SELECT distinct TOP(10) MsgMasterExt.MsgDate, MsgMasterExt.MsgSubject, MsgStatusMasterExt.MsgStatusName,MsgDetailExt.MsgStatusId,MsgMasterExt.FromPartyId,MsgDetailExt.ToEmailId, MsgMasterExt.MsgId FROM MsgDetailExt INNER JOIN MsgMasterExt ON MsgDetailExt.MsgId = MsgMasterExt.MsgId INNER JOIN MsgStatusMasterExt ON MsgDetailExt.MsgStatusId = MsgStatusMasterExt.MsgStatusId where MsgMasterExt.FromEmail='" + DropDownList17.SelectedItem.Text + "' and MsgDetailExt.MsgStatusId in(3) order by Msgdate desc";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da.Fill(dt);
        }

        GridView7.DataSource = dt;
        GridView7.DataBind();

        foreach (GridViewRow gdr1 in GridView7.Rows)
        {
            Label Label21r = (Label)gdr1.FindControl("Label21r");
            Label Label51r = (Label)gdr1.FindControl("Label51r");

            string strcc = System.DateTime.Now.ToShortDateString();

            if (Convert.ToDateTime(Label21r.Text) == Convert.ToDateTime(strcc))
            {
                Label21r.Visible = false;
                Label51r.Visible = true;
            }
        }

    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        string te = "Messagedraftsext.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Int32 MsgId = 0;
            DataTable dtMain = new DataTable();
            MsgId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MsgId").ToString());
            dtMain = new DataTable();
            dtMain = clsMessage.SelectMsgDetailforDraftPartyListExt(MsgId);
            Label sentTo = (Label)e.Row.FindControl("lblSentTo");

            if (dtMain.Rows.Count > 0)
            {
                String ToList = "";
                int i = 0;
                foreach (DataRow DR in dtMain.Rows)
                {

                    if (i >= 1)
                    {
                        ToList = ToList + " , " + DR["Compname"].ToString();
                    }
                    if (i == 0)
                    {
                        ToList = DR["Compname"].ToString();
                        i = 1;
                    }
                    if (ToList.Length > 25)
                    {
                        ToList = ToList + " ....";
                        break;
                    }
                }
                sentTo.Text = ToList.ToString();
            }
            dtMain = new DataTable();
            dtMain = clsMessage.SelectMsgforFileAttachExt(MsgId);
            Image img = (Image)e.Row.FindControl("ImgFile");


            if (dtMain.Rows.Count > 0)
            {
                img.ImageUrl = "~/Account/images/attach.png";
                img.Visible = true;
            }
            else
            {
                img.ImageUrl = "";
                img.Visible = false;
            }
        }
    }
    protected void GridView7_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView7.PageIndex = e.NewPageIndex;
        SelectMsgforDraftext();
    }
    protected void fillpartytypr()
    {
        ddlpartytype.Items.Clear();

        string emprole = "select PartyTypeId,PartType from [PartytTypeMaster] where compid='" + Session["Comid"] + "' order by PartType";
        SqlCommand cmdrole = new SqlCommand(emprole, con);
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();
        darole.Fill(dtrole);

        ddlpartytype.DataSource = dtrole;
        ddlpartytype.DataTextField = "PartType";
        ddlpartytype.DataValueField = "PartyTypeId";
        ddlpartytype.DataBind();

        ddlpartytype.SelectedIndex = ddlpartytype.Items.IndexOf(ddlpartytype.Items.FindByText("Employee"));
    }
    protected void fillcompanyname()
    {
        ddlcompname.Items.Clear();

        string str = "SELECT distinct Party_master.Compname,Party_master.PartyID FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlwarehouseext.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlpartytype.SelectedValue + "' and AccountMaster.Status='1' order by Party_master.Compname";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlcompname.DataSource = dt;
        ddlcompname.DataTextField = "Compname";
        ddlcompname.DataValueField = "PartyID";
        ddlcompname.DataBind();

        ddlcompname.Items.Insert(0, "ALL");
        ddlcompname.Items[0].Value = "0";
    }
    protected void filljobapplied()
    {
        ddlcandi.Items.Clear();

        string str11 = "select VacancyPositionTitle,ID from VacancyPositionTitleMaster where Active='1' order by VacancyPositionTitle asc";
        SqlCommand cmd11 = new SqlCommand(str11, con);
        SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
        DataTable dt11 = new DataTable();
        adp11.Fill(dt11);

        if (dt11.Rows.Count > 0)
        {
            ddlcandi.DataSource = dt11;
            ddlcandi.DataTextField = "VacancyPositionTitle";
            ddlcandi.DataValueField = "ID";
            ddlcandi.DataBind();
        }
        ddlcandi.Items.Insert(0, "All");
        ddlcandi.Items[0].Value = "0";
    }
    protected void Button2111_Click(object sender, EventArgs e)
    {
        if (TextBox4.Text != "")
        {
            int prttypeid = 0;
            DataTable dtminprt1 = new DataTable();
            dtminprt1 = clsDocument.SelectOtherPartyTypeId("Other");

            if (dtminprt1.Rows.Count > 0)
            {
                if (dtminprt1.Rows[0]["PartyTypeId"] != System.DBNull.Value)
                {
                    prttypeid = Convert.ToInt32(dtminprt1.Rows[0]["PartyTypeId"].ToString());
                }
            }
            EmployeeCls clsEmployee = new EmployeeCls();
            Int32 AccountId = clsMaster.InsertAccountMasterParty(1, 1, "test", "", System.DateTime.Today.ToShortDateString(), "0");
            Int32 partyid = clsMaster.InsertPartyMaster(prttypeid, AccountId, 1, TextBox4.Text, "Na", "1223", 0, 0, 0, 0, "4556", TextBox4.Text);
            Int32 rst32 = clsMaster.InsertPartyAddressDetail(partyid, 1, "USA", "NA", "Alaska", "", TextBox4.Text, "", "", "", "USA", "");
            Int32 securityansid = clsEmployee.InsertSecurityAnsEntry(1, partyid, "Pink");
        }
        FillPartyGridext();
        ModalPopupExtender3.Show();

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataColumn dtcom = new DataColumn();
        dtcom.DataType = System.Type.GetType("System.Int32");
        dtcom.ColumnName = "PartyId";
        dtcom.ReadOnly = false;
        dtcom.Unique = false;
        dtcom.AllowDBNull = true;
        dt.Columns.Add(dtcom);

        DataColumn dtcom1 = new DataColumn();
        dtcom1.DataType = System.Type.GetType("System.String");
        dtcom1.ColumnName = "Name";
        dtcom1.ReadOnly = false;
        dtcom1.Unique = false;
        dtcom1.AllowDBNull = true;
        dt.Columns.Add(dtcom1);

        DataColumn dtcom2 = new DataColumn();
        dtcom2.DataType = System.Type.GetType("System.String");
        dtcom2.ColumnName = "CName";
        dtcom2.ReadOnly = false;
        dtcom2.Unique = false;
        dtcom2.AllowDBNull = true;
        dt.Columns.Add(dtcom2);

        DataColumn dtcom3 = new DataColumn();
        dtcom3.DataType = System.Type.GetType("System.String");
        dtcom3.ColumnName = "Contactperson";
        dtcom3.ReadOnly = false;
        dtcom3.Unique = false;
        dtcom3.AllowDBNull = true;
        dt.Columns.Add(dtcom3);

        string address = "";

        foreach (GridViewRow GR in GridView4.Rows)
        {
            CheckBox chk = (CheckBox)GR.FindControl("chkParty");

            if (chk.Checked == true)
            {
                Int32 ToPartyId = Convert.ToInt32(GridView4.DataKeys[GR.RowIndex].Value);
                DataRow drow = dt.NewRow();
                drow["PartyId"] = ToPartyId.ToString();

                if (ddlpartytype.SelectedItem.Text == "Candidate")
                {
                    drow["CName"] = GR.Cells[5].Text;
                    dt.Rows.Add(drow);
                    if (Convert.ToString(address) == "")
                    {
                        address = GR.Cells[5].Text;
                    }
                    else
                    {
                        address = address + "; " + GR.Cells[5].Text;
                    }

                }
                if (ddlpartytype.SelectedItem.Text == "Employee")
                {
                    drow["Name"] = GR.Cells[3].Text;
                    dt.Rows.Add(drow);
                    if (Convert.ToString(address) == "")
                    {
                        address = GR.Cells[3].Text;
                    }
                    else
                    {
                        address = address + "; " + GR.Cells[3].Text;
                    }
                }
                if (ddlpartytype.SelectedItem.Text == "Customer" || ddlpartytype.SelectedItem.Text == "Other" || ddlpartytype.SelectedItem.Text == "Vendor" || ddlpartytype.SelectedItem.Text == "Admin")
                {
                    drow["Contactperson"] = GR.Cells[1].Text;
                    dt.Rows.Add(drow);
                    if (Convert.ToString(address) == "")
                    {
                        address = GR.Cells[1].Text;
                    }
                    else
                    {
                        address = address + "; " + GR.Cells[1].Text;
                    }
                }
            }
        }
        Session["to"] = dt;

        if (Session["to"] != null)
        {
            lblAddresses.Text = address.ToString();
        }
        ModalPopupExtender3.Hide();
        imgbtnsend.Enabled = true;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender3.Hide();
    }
    protected void ddlpartytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcompanyname();
        filljobapplied();
        FillPartyGridext();
        ModalPopupExtender3.Show();
    }
    protected void txtsearch_TextChanged111(object sender, EventArgs e)
    {
        FillPartyGridext();
        ModalPopupExtender3.Show();
    }
    protected void ddlcompname_SelectedIndexChanged111(object sender, EventArgs e)
    {
        FillPartyGridext();
        ModalPopupExtender3.Show();
    }
    protected void ddlcandi_SelectedIndexChanged111(object sender, EventArgs e)
    {
        FillPartyGridext();
        ModalPopupExtender3.Show();
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (GridView4.Rows.Count > 0)
            {
                CheckBox cbHeader = (CheckBox)GridView4.HeaderRow.FindControl("HeaderChkbox");
                cbHeader.Attributes["onclick"] = "ChangeAllCheckBoxStates(this.checked);";
                List<string> ArrayValues = new List<string>();
                ArrayValues.Add(string.Concat("'", cbHeader.ClientID, "'"));
                foreach (GridViewRow gvr in GridView4.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("chkParty");
                    cb.Attributes["onclick"] = "ChangeHeaderAsNeeded();";
                    ArrayValues.Add(string.Concat("'", cb.ClientID, "'"));
                }
                CheckBoxIDsArray.Text = "<script type='text/javascript'>" + "\n" + "<!--" + "\n" + String.Concat("var CheckBoxIDs =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") + "\n // -->" + "\n" + "</script>";
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            lblmsg1.Text = "Error in databound : " + ex.Message.ToString();
        }
    }
    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView4.PageIndex = e.NewPageIndex;
        FillPartyGridext();
        ModalPopupExtender3.Show();
    }
    protected DataTable SelectEmpEmaildetail123()
    {
        DataTable dtempemail1 = new DataTable();


        dtempemail1 = select("  Select Signature,LastDownloadedTime,LastDownloadIndex,EmailSignatureMaster.Id,EmailId ,IncomingEmailServer as ServerName,InEmailID as Email,InPassword as Password from InOutCompanyEmail inner join EmailSignatureMaster on EmailSignatureMaster.InoutgoingMasterId=InOutCompanyEmail.ID where EmployeeID='" + Session["EmployeeIdep"] + "'");
        if (dtempemail1.Rows.Count == 0)
        {
            dtempemail1 = select("Select Signature,LastDownloadedTime,LastDownloadIndex,InOutCompanyEmail.Id,EmailId ,IncomingEmailServer as ServerName,InEmailID as Email,InPassword  as Password from InOutCompanyEmail inner join EmailSignatureMaster on EmailSignatureMaster.InoutgoingMasterId=InOutCompanyEmail.ID where Whid='" + ViewState["Whid"] + "'");

        }
        return dtempemail1;
    }
    protected void dwnemail()
    {
        string location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");

        DataTable dbs = new DataTable();
        dbs = select("Select Whid From EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'");
        if (dbs.Rows.Count > 0)
        {
            ViewState["Whid"] = dbs.Rows[0]["Whid"].ToString();
        }
        DataTable dtemail = new DataTable();

        dtemail = SelectEmpEmaildetail123();
        if (dtemail.Rows.Count > 0)
        {
            foreach (DataRow dr in dtemail.Rows)
            {
                flg = 1;
                bool i = DownloadMail(dr["ServerName"].ToString(), dr["Email"].ToString(), dr["Password"].ToString(), location.ToString(), Convert.ToInt32(dr["LastDownloadIndex"]), Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(dr["LastDownloadedTime"]), Convert.ToBoolean("True"));
            }
        }
    }
    protected void dwnemail1()
    {

        string location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//");


        DataTable dtemail = new DataTable();

        dtemail = clsMaster.SelectCompanyEmaildetail(Convert.ToInt32(ddlempemail.SelectedValue));
        if (dtemail.Rows.Count > 0)
        {
            foreach (DataRow dr in dtemail.Rows)
            {
                ViewState["Whid"] = dr["Whid"].ToString();
                flg = 2;

                bool i = DownloadMail(dr["ServerName"].ToString(), dr["EmailId"].ToString(), dr["Password"].ToString(), location.ToString(), Convert.ToInt32(dr["LastDownloadIndex"]), Convert.ToInt32(Session["EmployeeIdep"]), Convert.ToDateTime(dr["LastDownloadedTime"]), Convert.ToBoolean("True"));

            }
        }
    }
    protected void accbalentry()
    {

        DataTable ds153 = new DataTable();
        ds153 = select("select Report_Period_Id  from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + ViewState["Whid"] + "' and Active='1'");
        Session["reportid"] = ds153.Rows[0]["Report_Period_Id"].ToString();

        DataTable ds1531 = new DataTable();
        ds1531 = select("select Report_Period_Id from [ReportPeriod] where ReportPeriod.Report_Period_Id<'" + Session["reportid"] + "' and  Whid='" + ViewState["Whid"] + "'  order by Report_Period_Id Desc");
        Session["reportid1"] = ds1531.Rows[0]["Report_Period_Id"].ToString();

        string str4562 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid1"] + "')";
        SqlCommand cmd4562 = new SqlCommand(str4562, con);
        con.Open();
        cmd4562.ExecuteNonQuery();
        con.Close();


        string str456 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid"].ToString() + "')";
        SqlCommand cmd456 = new SqlCommand(str456, con);
        con.Open();
        cmd456.ExecuteNonQuery();
        con.Close();

    }
    public bool DownloadMail(string server, string Email, string password, string DestPath, int LastDownloadIndex, Int32 DocumentEmailDownloadID, DateTime LastTime, bool docautoapprove)
    {
        try
        {
            string location1 = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");


            ForAspNet.POP3.License.LicenseKey = "D0NhcG1hbiBMdGQgUjI5OQEAAAABAAAA/z839HUoyiulFja7UZFbY3sZ6Q9mwIxPBhmGr7oX4+0PgLDF4APv+woUNOa+DYcN9XkD9r+SmFQ=";


            SqlDataAdapter dapo = new SqlDataAdapter("select InPort from InOutCompanyEmail where InEmailID='" + Email + "'", con);
            DataTable dtpo = new DataTable();
            dapo.Fill(dtpo);

            int port = 0;

            if (Convert.ToString(dtpo.Rows[0]["InPort"]) != "")
            {
                port = Convert.ToInt32(dtpo.Rows[0]["InPort"]);
            }

            POP3 objPOP3 = new POP3(server.ToString(), port, Email.ToString(), password.ToString());

            objPOP3.Connect();

            if (objPOP3.IsAPOPSupported)
            {
                objPOP3.SecureLogin();
            }
            else
            {
                objPOP3.Login();
            }

            objPOP3.QueryServer();

            int count = 0;

            for (int index = 0 + 1; index <= objPOP3.TotalMailCount; index++)
            {
                if (count <= 25)
                {
                    count = count + 1;
                    string filename, name;

                    EmailMessage objEmail = objPOP3.GetMessage(index, false);


                    DateTime msgtime1 = Convert.ToDateTime(objEmail.Date);

                    if (flg == 1)
                    {
                        bool i = clsDocument.UpdateEmpEmailLastDownloadIndex(Convert.ToInt32(Convert.ToInt32(Session["EmployeeId"])), Convert.ToInt32(index), msgtime1);

                    }
                    else if (flg == 2)
                    {

                        bool i = clsDocument.UpdateCompanyEmailLastDownloadIndex(Convert.ToInt32(Convert.ToInt32(ddlempemail.SelectedValue)), Convert.ToInt32(index), msgtime1);

                    }

                    string msgsubject = objEmail.Subject.ToString();

                    string msgbody = objEmail.Body.ToString();

                    string fromparty = objEmail.From.ToString();
                    char[] separator1 = new char[] { '<' };
                    string[] starr1 = fromparty.Split(separator1);
                    int i11 = Convert.ToInt32(starr1.Length);
                    string fst = "";
                    string sncd = "";
                    if (i11 == 2)
                    {
                        fst = starr1[0].ToString();
                        sncd = starr1[1].ToString();
                    }
                    char[] separator10 = new char[] { '>' };
                    string[] starr10 = sncd.Split(separator10);
                    int i110 = Convert.ToInt32(starr10.Length);
                    string fst0 = "";
                    string sncd0 = "";
                    string steml = "";
                    if (i110 == 2)
                    {
                        steml = starr10[0].ToString();
                    }
                    if (steml == "")
                    {
                        steml = fromparty;
                    }

                    DataTable dtspmeml = new DataTable();
                    dtspmeml = clsDocument.SelectSpamEmail(steml, Convert.ToInt32(Session["PartyId"]));
                    if (dtspmeml.Rows.Count > 0)
                    {
                        spm = 1;
                        allw = Convert.ToBoolean(dtspmeml.Rows[0]["AllowEmail"].ToString());

                    }
                    else
                    {
                        allw = false;
                        spm = 0;
                    }

                    DataTable dtminprt1 = new DataTable();
                    dtminprt1 = clsDocument.SelectPartyIdFromPartyEmail(steml);
                    int prtid = 0;
                    Int32 folderid = 0;
                    Int32 ign = 0;
                    string doc_email_desc = "";
                    string refst = "";
                    Double amt = 0.0;
                    int attach = 0;
                    if (dtminprt1.Rows.Count > 0)
                    {
                        if (dtminprt1.Rows[0]["PartyId"] != System.DBNull.Value)
                        {
                            prtid = Convert.ToInt32(dtminprt1.Rows[0][0].ToString());
                        }
                    }
                    if (prtid > 0)
                    {
                        DataTable dtfldfmprt = new DataTable();
                        dtfldfmprt = clsDocument.SelectFolderIdFromPartyID(prtid);
                        if (dtfldfmprt.Rows.Count > 0)
                        {
                            if (dtfldfmprt.Rows[0]["FolderId"] != System.DBNull.Value)
                            {
                                folderid = Convert.ToInt32(dtfldfmprt.Rows[0]["FolderId"].ToString());
                            }
                        }
                    }
                    if (prtid == 0)
                    {
                        dtminprt1 = clsDocument.SelectMinPartyIdfromPartyMaster("General", ViewState["Whid"].ToString());
                        if (dtminprt1.Rows.Count > 0)
                        {
                            if (dtminprt1.Rows[0]["PartyId"] != System.DBNull.Value)
                            {
                                prtid = Convert.ToInt32(dtminprt1.Rows[0][0].ToString());
                            }
                        }
                    }
                    if (prtid == 0)
                    {
                        int prttypeid = 0;
                        DataTable dtminprt100 = new DataTable();
                        dtminprt100 = clsDocument.SelectOtherPartyTypeId("Admin");

                        if (dtminprt100.Rows.Count > 0)
                        {
                            if (dtminprt100.Rows[0]["PartyTypeId"] != System.DBNull.Value)
                            {
                                prttypeid = Convert.ToInt32(dtminprt100.Rows[0]["PartyTypeId"].ToString());

                            }
                        }

                        DataTable dtt = new DataTable();
                        string accid = "";
                        dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and  Whid='" + ViewState["Whid"] + "'");
                        if (dtt.Rows.Count > 0)
                        {
                            if (dtt.Rows[0]["aid"].ToString() != "")
                            {

                                int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                                accid = Convert.ToString(gid);
                            }
                            else
                            {
                                accid = Convert.ToString(30000);
                            }
                        }
                        EmployeeCls clsEmployee = new EmployeeCls();

                        string ins1 = "INSERT INTO [AccountMaster] ([AccountId],[ClassId],[GroupId],[AccountName],[Date],[Status],[compid],[Whid]) values('" + accid + "',5,15,'test','" + System.DateTime.Today.ToShortDateString() + "',1,'" + Session["Comid"].ToString() + "','" + ViewState["Whid"].ToString() + "')";
                        SqlCommand cmdum = new SqlCommand(ins1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdum.ExecuteNonQuery();
                        con.Close();

                        SqlDataAdapter daum = new SqlDataAdapter("select max(id) as id from AccountMaster", con);
                        DataTable dtum = new DataTable();
                        daum.Fill(dtum);

                        DataTable dtum1 = new DataTable();

                        if (dtum.Rows.Count > 0)
                        {
                            SqlDataAdapter daum1 = new SqlDataAdapter("select AccountId from AccountMaster where id='" + dtum.Rows[0]["id"].ToString() + "'", con);
                            dtum1 = new DataTable();
                            daum1.Fill(dtum1);
                        }
                        Int32 AccountId = 0;
                        if (dtum1.Rows.Count > 0)
                        {
                            AccountId = Convert.ToInt32(dtum1.Rows[0]["AccountId"]);
                            Session["maxaid"] = dtum1.Rows[0]["AccountId"].ToString();
                        }

                        accbalentry();

                        string ins2 = "INSERT INTO [Party_Master] ([PartyTypeId],[Account],[Compname],[Contactperson],[Address],[City],[State],[Country],[Website],[Email],[id],[Whid]) values('" + prttypeid + "','" + AccountId + "','Capman','Na','Capman',0,0,0,'epaza.us','" + steml + "','" + Session["Comid"].ToString() + "','" + ViewState["Whid"].ToString() + "')";
                        SqlCommand cmdum2 = new SqlCommand(ins2, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdum2.ExecuteNonQuery();
                        con.Close();

                        SqlDataAdapter daum2 = new SqlDataAdapter("select max(partyid) as partyid from Party_Master", con);
                        DataTable dtum2 = new DataTable();
                        daum2.Fill(dtum2);

                        Int32 partyid = 0;

                        if (dtum2.Rows.Count > 0)
                        {
                            partyid = Convert.ToInt32(dtum2.Rows[0]["partyid"]);
                        }


                        //  Int32 partyid = clsMaster.InsertPartyMasterMess(prttypeid, AccountId, "Capman", "Na", "Capman", "0", "0", "0", "epaza.us", steml, ViewState["Whid"].ToString());
                        string ins6 = "insert into User_master(Name,Address ,City,State,Country,Phoneno,EmailID ,Username,Department,Accesslevel,PartyID,DesigantionMasterId,Active,Extention,zipcode)" +
                                                       "values ('WellCapman','WellCapman','0','0','0','0','" + steml + "','Capman','0','1','" + partyid + "','0','True','0','0')";
                        SqlCommand cmd6 = new SqlCommand(ins6, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd6.ExecuteNonQuery();

                        con.Close();
                        DataTable datUser = new DataTable();
                        datUser = select("select max(UserID) as UserID from User_master where PartyID='" + partyid + "' ");
                        if (datUser.Rows.Count > 0)
                        {
                            string ins7 = "insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + Convert.ToInt32(datUser.Rows[0]["UserID"]) + "','Capman','Capman','0','1','0','1')";
                            SqlCommand cmd9 = new SqlCommand(ins7, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd9.ExecuteNonQuery();
                            con.Close();

                            string stremp = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                            " Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description) values('" + partyid + "','0', " +
                            " '0','0','0','Capman', " +
                                " '0','0','0','5675','" + steml + "','" + AccountId + "','" + AccountId + "','Capman','" + ViewState["Whid"].ToString() + "','Capman')";
                            SqlCommand cmdemp = new SqlCommand(stremp, con);
                            con.Open();
                            cmdemp.ExecuteNonQuery();
                            con.Close();
                        }

                        prtid = partyid;
                    }
                    if (folderid == 0)
                    {
                        //Int32 i = 0;
                        DataTable dtgen = new DataTable();
                        dtgen = clsDocument.SelectGeneralFolderId(ViewState["Whid"].ToString());
                        if (dtgen.Rows.Count > 0)
                        {
                            if (dtgen.Rows[0]["DocumentTypeId"] != System.DBNull.Value)
                            {
                                ign = Convert.ToInt32(dtgen.Rows[0]["DocumentTypeId"].ToString());
                            }
                        }
                    }
                    Int32 topartyid = 0;
                    DataTable dtorgeml = new DataTable();
                    dtorgeml = clsMaster.SelectPartyIdfromCompanyEmail(Convert.ToInt32(ddlempemail.SelectedValue));
                    //HttpContext.Current.Session["Company"] = "1";
                    if (dtorgeml.Rows.Count > 0)
                    {
                        if (dtorgeml.Rows[0][1] != System.DBNull.Value)
                        {
                            topartyid = Convert.ToInt32(dtorgeml.Rows[0][1].ToString());
                        }
                    }
                    folderid = ign;
                    string extstr = "";
                    string doc_title = "";
                    doc_title = "Receive From:" + steml + "," + msgsubject + "," + objEmail.Date;
                    if (fromparty != "")
                    {

                    }
                    if (objEmail.IsAnyAttachments)
                    {

                        for (int attCount = 0; attCount < objEmail.Attachments.Count; attCount++)
                        {
                            ForAspNet.POP3.Attachment att = (ForAspNet.POP3.Attachment)objEmail.Attachments[attCount];

                            if (att.IsFileAttachment)
                            {
                                attach = 1;

                                if (spm == 0 && allw == false)
                                {
                                    filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + Email.ToString() + "_" + att.FileName;
                                    att.Save(DestPath.ToString() + filename.ToString());
                                    msgbody = "";
                                    Int32 MsgId = clsMessage.InsertMsgMasterExt(prtid, msgsubject, msgbody, steml);
                                    if (MsgId > 0)
                                    {
                                        //Int32 MessageDetailId = clsMessage.InsertMsgDetailExt(MsgId, Convert.ToInt32(Convert.ToInt32( Session["EmployeeIdep"])));                       
                                        if (ddlempemail.SelectedIndex == 1)
                                        {
                                            Int32 MessageDetailId = clsMessage.InsertMsgDetailExt(MsgId, Convert.ToInt32(Session["PartyId"].ToString()), 1);
                                        }
                                        else if (ddlempemail.SelectedIndex > 1)
                                        {
                                            Int32 MessageDetailId = clsMessage.InsertMsgDetailExt(MsgId, topartyid, 1);
                                        }

                                        bool ins = clsMessage.InsertMsgFileAttachDetailExt(MsgId, filename);

                                        flnam = filename;
                                    }

                                    File.Copy(DestPath.ToString() + filename.ToString(), location1.ToString() + filename.ToString(), true);
                                }
                                else if (spm == 1 && allw == true)
                                {
                                    //bool spmm = true;
                                    filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + Email.ToString() + "_" + att.FileName;
                                    att.Save(DestPath.ToString() + filename.ToString());
                                    msgbody = "";
                                    Int32 MsgId = clsMessage.InsertMsgMasterExt(prtid, msgsubject, msgbody, steml);
                                    if (MsgId > 0)
                                    {
                                        //Int32 MessageDetailId = clsMessage.InsertMsgDetailExt(MsgId, Convert.ToInt32(Convert.ToInt32( Session["EmployeeIdep"])));                       
                                        if (ddlempemail.SelectedIndex == 1)
                                        {
                                            Int32 MessageDetailId = clsMessage.InsertMsgDetailExt(MsgId, Convert.ToInt32(Session["PartyId"].ToString()), 1);
                                        }
                                        else if (ddlempemail.SelectedIndex > 1)
                                        {
                                            Int32 MessageDetailId = clsMessage.InsertMsgDetailExt(MsgId, topartyid, 1);
                                        }
                                        bool ins = clsMessage.InsertMsgFileAttachDetailExt(MsgId, filename);
                                        // bool adem = clsMessage.InsertSpamEmail(steml, spmm, MessageDetailId, Convert.ToInt32(Session["PartyId"]));
                                        flnam = filename;
                                    }

                                    File.Copy(DestPath.ToString() + filename.ToString(), location1.ToString() + filename.ToString(), true);
                                }

                            }
                        }
                        if (attach == 0)
                        {
                            msgbody = "";
                            Int32 MsgId = 0;
                            Int32 MessageDetailId = 0;

                            string insert = "insert into msgmasterext(FromPartyId,MsgSubject,FromEmail,Msgdate,cid) values('" + prtid + "','" + msgsubject + "','" + steml + "','" + System.DateTime.Now.ToString() + "','" + Session["Comid"].ToString() + "')";
                            SqlCommand cmdlel = new SqlCommand(insert, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdlel.ExecuteNonQuery();
                            con.Close();

                            SqlDataAdapter dalal = new SqlDataAdapter("select max(MsgId) as MsgId from msgmasterext", con);
                            DataTable dtlal = new DataTable();
                            dalal.Fill(dtlal);

                            if (dtlal.Rows.Count > 0)
                            {
                                MsgId = Convert.ToInt32(dtlal.Rows[0]["MsgId"]);
                            }

                            //    Int32 MsgId = clsMessage.InsertMsgMasterExt(prtid, msgsubject, msgbody, steml);
                            if (MsgId > 0)
                            {
                                string insert1 = "insert into msgbodyext(MsgId,MsgDetail) values('" + MsgId + "','" + msgbody + "')";
                                SqlCommand cmdlel1 = new SqlCommand(insert1, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdlel1.ExecuteNonQuery();
                                con.Close();

                                string insert2 = "insert into MsgDetailExt(MsgId,topartyid,msgstatusid) values('" + MsgId + "','" + Convert.ToInt32(Session["PartyId"].ToString()) + "',1)";
                                SqlCommand cmdlel2 = new SqlCommand(insert2, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdlel2.ExecuteNonQuery();
                                con.Close();

                                SqlDataAdapter dalal1 = new SqlDataAdapter("select max(msgdetailid) as msgdetailid from MsgDetailExt", con);
                                DataTable dtlal1 = new DataTable();
                                dalal1.Fill(dtlal1);

                                if (dtlal1.Rows.Count > 0)
                                {
                                    MessageDetailId = Convert.ToInt32(dtlal1.Rows[0]["msgdetailid"]);
                                }

                                //  Int32 MessageDetailId = clsMessage.InsertMsgDetailExt(MsgId, Convert.ToInt32(Session["PartyId"].ToString()), 1);

                            }

                        }
                    }
                    else
                    {

                    }
                }
            }
            objPOP3.Close();
        }
        catch (Exception es)
        {
            lblmsg3.Text = "Message: Check Your Email Login Credentials.";

        }
        return true;
    }
    protected string getpartyemail(int partyId)
    {
        DataTable dt = new DataTable();
        dt = clsEmployee.SelectPartyEmailbypartyid(partyId);
        //Email
        string prtemail = "";
        if (dt.Rows.Count > 0)
        {
            prtemail = dt.Rows[0]["Email"].ToString();
        }
        return prtemail;

    }


    protected void sendmail11()
    {
        foreach (GridViewRow GR1 in GridView4.Rows)
        {
            int flag = 0;
            CheckBox chk = (CheckBox)GR1.FindControl("chkParty");
            if (flag != 1)
            {
                if (chk.Checked == true)
                {
                    Int32 ToPartyId = Convert.ToInt32(GridView4.DataKeys[GR1.RowIndex].Value);
                    flag = 1;
                    if (ToPartyId == null)
                    {
                        prtemail = lblAddresses.Text;
                    }
                    else
                    {
                        prtemail = getpartyemail(ToPartyId);
                    }
                    if (prtemail != "")
                    {
                        try
                        {
                            DataTable dtemail = new DataTable();
                            DataTable dtemail1 = new DataTable();

                            if (DropDownList15.SelectedIndex == 1)
                            {
                                dtemail1 = SelectEmpEmaildetail();
                                SqlDataAdapter da = new SqlDataAdapter("select Email from employeemaster where employeemasterid='" + Session["EmployeeId"].ToString() + "'", con);
                                da.Fill(dtemail);
                            }
                            else if (DropDownList15.SelectedIndex > 1)
                            {
                                dtemail = clsMaster.SelectCompanyEmaildetail(Convert.ToInt32(DropDownList15.SelectedValue));
                            }
                            if (dtemail.Rows.Count > 0)
                            {

                            }
                            string logo = "<img src=\"http://www.ifilecabinet.com/images/logo.jpg\" \"border=\"0\"  />";

                            string body = "";
                            MailAddress to = new MailAddress(prtemail);

                            MailAddress from = new MailAddress(dtemail.Rows[0]["Email"].ToString());

                            MailMessage objEmail = new MailMessage(from, to);
                            objEmail.Subject = txtsub.Text;

                            objEmail.Body = TxtMsgDetail.Text;
                            objEmail.IsBodyHtml = true;


                            string email = getuseremail();
                            if (Convert.ToString(email) != "")
                            {
                                MailAddress reply = new MailAddress(email.ToString());

                                objEmail.ReplyTo = reply;
                            }

                            objEmail.Priority = MailPriority.High;
                            System.Net.Mail.Attachment attachFile;

                            SmtpClient client = new SmtpClient();


                            if (DropDownList15.SelectedIndex == 1)
                            {
                                client.Credentials = new NetworkCredential(dtemail1.Rows[0]["InEmailID"].ToString(), dtemail1.Rows[0]["InPassword"].ToString());
                                client.Host = dtemail1.Rows[0]["IncomingEmailServer"].ToString();
                            }
                            else if (DropDownList15.SelectedIndex > 1)
                            {
                                client.Credentials = new NetworkCredential(dtemail.Rows[0]["EmailID"].ToString(), dtemail.Rows[0]["Password"].ToString());
                                client.Host = dtemail.Rows[0]["ServerName"].ToString();
                            }

                            client.Send(objEmail);

                            lblmsg1.Text = "Sent Successfully.";

                        }
                        catch (Exception ex)
                        {
                            lblmsg1.Text = ex.Message.ToString();

                        }
                    }

                }
            }
            else
            {
                DataTable dtemail = new DataTable();
                if (DropDownList15.SelectedIndex == 1)
                {

                    dtemail = SelectEmpEmaildetail();
                }
                else if (DropDownList15.SelectedIndex > 1)
                {
                    dtemail = clsMaster.SelectCompanyEmaildetail(Convert.ToInt32(DropDownList15.SelectedValue));

                }
                if (dtemail.Rows.Count > 0)
                {

                }
                string logo = "<img src=\"http://www.ifilecabinet.com/images/logo.jpg\" \"border=\"0\"  />";

                string body = "";
                MailAddress to = new MailAddress(lblAddresses.Text);

                MailAddress from = new MailAddress(dtemail.Rows[0]["Email"].ToString());

                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = txtsub.Text;

                objEmail.Body = TxtMsgDetail.Text;
                objEmail.IsBodyHtml = true;


                string email = getuseremail();
                if (Convert.ToString(email) != "")
                {
                    MailAddress reply = new MailAddress(email.ToString());

                    objEmail.ReplyTo = reply;
                }

                objEmail.Priority = MailPriority.High;
                System.Net.Mail.Attachment attachFile;

                SmtpClient client = new SmtpClient();
                if (DropDownList15.SelectedIndex == 1)
                {
                    client.Credentials = new NetworkCredential(dtemail.Rows[0]["InEmailID"].ToString(), dtemail.Rows[0]["InPassword"].ToString());
                    client.Host = dtemail.Rows[0]["IncomingEmailServer"].ToString();
                }
                else if (DropDownList15.SelectedIndex > 1)
                {
                    client.Credentials = new NetworkCredential(dtemail.Rows[0]["EmailID"].ToString(), dtemail.Rows[0]["Password"].ToString());
                    client.Host = dtemail.Rows[0]["ServerName"].ToString();
                }
                client.Send(objEmail);

                lblmsg1.Text = "Sent Successfully.";

            }
        }
    }

    protected DataTable SelectEmpEmaildetail()
    {
        DataTable dtempemail1 = new DataTable();

        dtempemail1 = select("Select Signature,EmailSignatureMaster.Id,EmailId as Email,IncomingEmailServer,InEmailID,InPassword from InOutCompanyEmail inner join EmailSignatureMaster on EmailSignatureMaster.InoutgoingMasterId=InOutCompanyEmail.ID where EmployeeID='" + Session["EmployeeId"] + "'");
        if (dtempemail1.Rows.Count == 0)
        {
            dtempemail1 = select("Select Signature,InOutCompanyEmail.Id,EmailId as Email,IncomingEmailServer,InEmailID,InPassword from InOutCompanyEmail inner join EmailSignatureMaster on EmailSignatureMaster.InoutgoingMasterId=InOutCompanyEmail.ID where Whid='" + ddlwarehouseext.SelectedValue + "'");
        }
        return dtempemail1;
    }
    protected string getuseremail()
    {
        DataTable dt = new DataTable();
        dt = clsEmployee.SelectEmployeeMasterbyId(Convert.ToInt32(Session["EmployeeIdep"]));
        //Email
        string email = "";
        if (dt.Rows.Count > 0)
        {
            email = dt.Rows[0]["Email"].ToString();
        }
        return email;
    }

    protected void imgbtnattach_Click(object sender, EventArgs e)
    {
        String filename = "";
        PnlFileAttachLbl.Visible = true;
        if (fileuploadadattachment.HasFile)
        {
            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fileuploadadattachment.FileName;
            fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["Comid"] + "\\UploadedDocuments\\") + filename);
            fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["Comid"] + "\\UploadedDocumentsTemp\\") + filename);
            hdnFileName.Value = filename;
            DataTable dt = new DataTable();
            if (Session["GridFileAttach1"] == null)
            {
                DataColumn dtcom2 = new DataColumn();
                dtcom2.DataType = System.Type.GetType("System.String");
                dtcom2.ColumnName = "FileName";
                dtcom2.ReadOnly = false;
                dtcom2.Unique = false;
                dtcom2.AllowDBNull = true;
                dt.Columns.Add(dtcom2);
                DataColumn dtcom3 = new DataColumn();
                dtcom3.DataType = System.Type.GetType("System.String");
                dtcom3.ColumnName = "FileNameChanged";
                dtcom3.ReadOnly = false;
                dtcom3.Unique = false;
                dtcom3.AllowDBNull = true;
                dt.Columns.Add(dtcom3);
            }
            else
            {
                dt = (DataTable)Session["GridFileAttach1"];
            }
            DataRow dtrow = dt.NewRow();
            dtrow["FileName"] = fileuploadadattachment.FileName.ToString();
            dtrow["FileNameChanged"] = hdnFileName.Value;
            dt.Rows.Add(dtrow);
            Session["GridFileAttach1"] = dt;
            gridFileAttach.DataSource = dt;
            gridFileAttach.DataBind();
        }
        else
        {
            lblmsg2.Text = "Please Attach File to Upload.";
            return;
        }
    }
    protected void gridFileAttach_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        gridFileAttach.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Remove")
        {
            DataTable dt = new DataTable();
            if (Session["GridFileAttach1"] != null)
            {
                dt = (DataTable)Session["GridFileAttach1"];
                dt.Rows.Remove(dt.Rows[gridFileAttach.SelectedIndex]);
                gridFileAttach.DataSource = dt;
                gridFileAttach.DataBind();
                Session["GridFileAttach1"] = dt;
                if (dt.Rows.Count == 0)
                {
                    PnlFileAttachLbl.Visible = false;
                }

            }
        }
    }
    protected void gridFileAttach_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridFileAttach.PageIndex = e.NewPageIndex;
        //SelectMsgforDraft();
        FillPartyGrid();
    }
    //public void AddAttachment(int j)
    //{
    //    DataTable dt = new DataTable();
    //    dt = clsMessage.SelectDocumentMasterByID(j);
    //    DataListAttach.DataSource = dt;
    //    DataListAttach.DataBind();        
    //}
    protected void btnattachext_Click(object sender, EventArgs e)
    {
        String filename = "";
        panelattachext.Visible = true;

        if (fileupload1.HasFile)
        {
            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fileupload1.FileName;

            fileupload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["Comid"] + "\\UploadedDocuments\\") + filename);
            fileupload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["Comid"] + "\\UploadedDocumentsTemp\\") + filename);

            hdnFileName.Value = filename;
            DataTable dt = new DataTable();

            if (Session["GridFileAttach11"] == null)
            {
                DataColumn dtcom2 = new DataColumn();
                dtcom2.DataType = System.Type.GetType("System.String");
                dtcom2.ColumnName = "FileName";
                dtcom2.ReadOnly = false;
                dtcom2.Unique = false;
                dtcom2.AllowDBNull = true;
                dt.Columns.Add(dtcom2);

                DataColumn dtcom3 = new DataColumn();
                dtcom3.DataType = System.Type.GetType("System.String");
                dtcom3.ColumnName = "FileNameChanged";
                dtcom3.ReadOnly = false;
                dtcom3.Unique = false;
                dtcom3.AllowDBNull = true;
                dt.Columns.Add(dtcom3);
            }
            else
            {
                dt = (DataTable)Session["GridFileAttach11"];
            }
            DataRow dtrow = dt.NewRow();
            dtrow["FileName"] = fileupload1.FileName.ToString();

            dtrow["FileNameChanged"] = hdnFileName.Value;
            dt.Rows.Add(dtrow);
            Session["GridFileAttach11"] = dt;
            gridattachext.DataSource = dt;
            gridattachext.DataBind();
        }
        else
        {
            lblmsg1.Text = "Please Attach File to Upload.";
            return;
        }
    }

    protected void gridattachext_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridattachext.PageIndex = e.NewPageIndex;
        FillPartyGridext();
    }
    protected void gridattachext_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        gridattachext.SelectedIndex = Convert.ToInt32(e.CommandArgument);

        string Location = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocumentsTemp//");

        if (e.CommandName == "Remove")
        {
            DataTable dt = new DataTable();

            if (Session["GridFileAttach11"] != null)
            {
                dt = (DataTable)Session["GridFileAttach11"];

                string flnam = gridattachext.Rows[gridattachext.SelectedIndex].Cells[1].Text;

                if (System.IO.File.Exists(Location + flnam))
                {
                    System.IO.File.Delete(Location + flnam);
                }

                dt.Rows.Remove(dt.Rows[gridattachext.SelectedIndex]);
                gridattachext.DataSource = dt;
                gridattachext.DataBind();

                Session["GridFileAttach11"] = dt;

                if (dt.Rows.Count == 0)
                {
                    panelattachext.Visible = false;
                }
            }
        }
    }
}

