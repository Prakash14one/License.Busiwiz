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

public partial class Add_Employee_Attendance_Report_Detail : System.Web.UI.Page
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
            ViewState["sortOrder"] = "";
            txteenddate.Text = System.DateTime.Now.ToShortDateString();
            txtenddateto.Text = System.DateTime.Now.ToShortDateString();
            fillstore();
            fillbatch();
            fillemployee();
            fillgrid();
            
        }

    }
    protected void fillstore()
    {

        ddlStore.Items.Clear();
        //DataTable ds = ClsStore.SelectStorename();

        string str = "SELECT Name,WareHouseId from warehousemaster inner join employeemaster on warehousemaster.WareHouseId=employeemaster.whid where EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "'";
        SqlCommand cmdwh = new SqlCommand(str, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable ds = new DataTable();
        adpwh.Fill(ds);

        ddlStore.DataSource = ds;
        ddlStore.DataTextField = "Name";
        ddlStore.DataValueField = "WareHouseId";
        ddlStore.DataBind();

        ddlStore.Enabled = false;
        //DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        //if (dteeed.Rows.Count > 0)
        //{
        //    ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        //}


    }
    protected void fillbatch()
    {
        ddlbatchname.Items.Clear();
    //    string str = "SELECT * from BatchMaster where WHID='" + ddlStore.SelectedValue + "'";
        string str = "SELECT BatchMaster.Name,BatchMaster.ID from BatchMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID where Employeeid='" + Convert.ToInt32(Session["EmployeeId"]) + "'";

        SqlCommand cmdwh = new SqlCommand(str, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);
        if (dtwh.Rows.Count > 0)
        {

            ddlbatchname.DataSource = dtwh;
            ddlbatchname.DataTextField = "Name";
            ddlbatchname.DataValueField = "ID";
            ddlbatchname.DataBind();
        }
        ddlbatchname.Enabled = false;
        //ddlbatchname.Items.Insert(0, "All");
        //ddlbatchname.Items[0].Value = "0";
    }
    protected void fillemployee()
    {

        ddlemployeename.Items.Clear();
        string str = "select EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName from EmployeeMaster where EmployeeMaster.EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "'";
        //string str = "select Distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName from EmployeeMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID where EmployeeMaster.Whid='" + ddlStore.SelectedValue + "'   ";

        //if (ddlbatchname.SelectedIndex > 0)
        //{
        //    str += " and EmployeeBatchMaster.Batchmasterid='" + ddlbatchname.SelectedValue + "' ";
        //}
        //str += "order by EmployeeMaster.EmployeeName";

        SqlCommand cmdwh = new SqlCommand(str, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);

        if (dtwh.Rows.Count > 0)
        {

            ddlemployeename.DataSource = dtwh;
            ddlemployeename.DataTextField = "EmployeeName";
            ddlemployeename.DataValueField = "EmployeeMasterID";
            ddlemployeename.DataBind();
        }
        ddlemployeename.Enabled = false;
        //ddlemployeename.Items.Insert(0, "All");
        //ddlemployeename.Items[0].Value = "0";
    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillbatch();
        fillemployee();

    }
    protected void ddlbatchname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployee();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();


    }
    protected void fillgrid()
    {     

        string strdate = "";
        string strpera = "";

        if (ddlbatchname.SelectedIndex > -1)
        {
            strpera += " and EmployeeBatchMaster.Batchmasterid='" + ddlbatchname.SelectedValue + "' ";
        }
        if (ddlemployeename.SelectedIndex > -1)
        {
            strpera += " and EmployeeBatchMaster.Employeeid='" + ddlemployeename.SelectedValue + "' ";
        }
        if (txteenddate.Text != "" && txtenddateto.Text != "")
        {
            strdate = strpera + "  and DateMasterTbl.Date between '" + Convert.ToDateTime(txteenddate.Text).ToShortDateString() + "' and '" + Convert.ToDateTime(txtenddateto.Text).ToShortDateString() + "' ";
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

        lblbusiness.Text = ddlStore.SelectedItem.Text;
        lblcompanyname.Text = Session["Cname"].ToString();
        lblbatchnameprint.Text = ddlbatchname.SelectedItem.Text;
        lblstatus.Text = ddlpresentstatus.SelectedItem.Text;
        lblemployeenameprint.Text = ddlemployeename.SelectedItem.Text;
        lbldateprint.Text = "From Date : " + " " + txteenddate.Text + " " + "To Date :" + " " + txtenddateto.Text;
        DataTable dtf = new DataTable();
        dtf = CreateDatatable();
        DataTable ds1 = select("SELECT distinct Convert(nvarchar, DateMasterTbl.Date,101) as Datet,DateMasterID as DateId FROM EmployeeMaster  " +
     " INNER JOIN  dbo.EmployeeBatchMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.EmployeeBatchMaster.Employeeid  inner join BatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join BatchWorkingDays on BatchWorkingDays.BatchID=BatchMaster.Id inner join DateMasterTbl" +
        " on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID  where  BatchMaster.Whid='" + ddlStore.SelectedValue + "'" + strdate + " order by DateId ");
        foreach (DataRow dtr in ds1.Rows)
        {
            DataTable dt2 = select("SELECT DISTINCT Left(BatchTiming.StartTime,5) as StartTime,Left(BatchTiming.EndTime,5) as EndTime, dbo.AttandanceEntryNotes.OutTimeNote, dbo.AttandanceEntryNotes.IntimeNote,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Varify," +
                        " dbo.AttendenceEntryMaster.EmployeeID, CONVERT(Nvarchar, dbo.AttendenceEntryMaster.Date, 101) AS Date, dbo.AttendenceEntryMaster.InTime," +
                       " dbo.AttendenceEntryMaster.InTimeforcalculation, dbo.AttendenceEntryMaster.OutTime, dbo.AttendenceEntryMaster.OutTimeforcalculation," +
                       " Left(AttendenceEntryMaster.LateInMinuts,6) as LateInMinuts, Left(AttendenceEntryMaster.OutInMinuts,6) as OutInMinuts , dbo.AttendenceEntryMaster.TotalHourWork,dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid, dbo.EmployeeBatchMaster.Batchmasterid,BatchMaster.Name as BatchName  FROM" +
                        " AttandanceEntryNotes Right join AttendenceEntryMaster ON dbo.AttendenceEntryMaster.AttendanceId = dbo.AttandanceEntryNotes.AttendanceId " +
                       " Right  join EmployeeMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  and  AttendenceEntryMaster.Date='" + Convert.ToDateTime(dtr["Datet"]).ToShortDateString() + "' and  EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' " +
                      " INNER JOIN  dbo.EmployeeBatchMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.EmployeeBatchMaster.Employeeid  inner join BatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join BatchTiming on BatchTiming.BatchMasterId=BatchMaster.Id and BatchMaster.Whid='" + ddlStore.SelectedValue + "' " + strpera + " order by BatchName,EmployeeMaster.EmployeeName");




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
                    Drow["Date"] = Convert.ToDateTime(dtr["Datet"]).ToShortDateString();

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

                    //Drow["BatchRequiredhours"] = Convert.ToString(dtp["reqhour"]);
                    
                    Drow["DateId"] = Convert.ToString(dtr["DateId"]);
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

        DataView myDataView = new DataView();
        myDataView = dtf.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        gridattendance.DataSource = myDataView;
        gridattendance.DataBind();


    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

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
    protected void gridattendance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {

            string  dk = Convert.ToString(e.CommandArgument);
            string te = "EmployeeAttendanceMoreInfo.aspx?AttendanceMasterId=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
    protected void btnprintableversion_Click(object sender, EventArgs e)
    {
        if (btnprintableversion.Text == "Printable Version")
        {
            btnprintableversion.Text = "Hide Printable Version";
            Button7.Visible = true;

            if (gridattendance.Columns[12].Visible == true)
            {
                ViewState["viewm"] = "tt";
                gridattendance.Columns[12].Visible = false;
            }
        }
        else
        {
            btnprintableversion.Text = "Printable Version";
            Button7.Visible = false;

            if (ViewState["viewm"] != null)
            {
                gridattendance.Columns[12].Visible = true;
            }
        }
    }

    protected void gridattendance_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
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
    protected void gridattendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridattendance.PageIndex = e.NewPageIndex;
        fillgrid();
    }
   
}