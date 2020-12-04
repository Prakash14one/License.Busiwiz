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

public partial class ShoppingCart_Admin_EmployeeAttendanceMoreInfo : System.Web.UI.Page
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
            if (Request.QueryString["AttendanceMasterId"] != "")
            {
                int id = Convert.ToInt32(Request.QueryString["AttendanceMasterId"]);
                pnlgrid.Visible = true;
                lblerror.Visible = false;
                lblerror.Text = "";
                //Button7.Visible = true;
                btnprintableversion.Visible = true;
                filldata();
            }
            else
            {
                pnlgrid.Visible = false;
                lblerror.Visible = true;
                lblerror.Text = "Employee is Absent";
                Button7.Visible = false;
                btnprintableversion.Visible = false;
            }
        }

    }
    protected void filldata()
    {
        string str = " SELECT DISTINCT " +
                       " dbo.AttandanceEntryNotes.OutTimeNote, dbo.AttandanceEntryNotes.IntimeNote, dbo.AttendenceEntryMaster.AttendanceId, " +
                       " dbo.AttendenceEntryMaster.EmployeeID, CONVERT(Nvarchar, dbo.AttendenceEntryMaster.Date, 101) AS Date, dbo.AttendenceEntryMaster.InTime, " +
                       " dbo.AttendenceEntryMaster.InTimeforcalculation, dbo.AttendenceEntryMaster.OutTime, dbo.AttendenceEntryMaster.OutTimeforcalculation, " +
                       " dbo.AttendenceEntryMaster.LateInMinuts, dbo.AttendenceEntryMaster.OutInMinuts, dbo.AttendenceEntryMaster.TotalHourWork, " +
                       " dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid, dbo.EmployeeBatchMaster.Batchmasterid,BatchMaster.Name as BatchName,WareHouseMaster.Name as Businessname " +
                       " FROM   dbo.EmployeeMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=EmployeeMaster.Whid  INNER JOIN " +
                       " dbo.EmployeeBatchMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.EmployeeBatchMaster.Employeeid  inner join BatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID  INNER JOIN " +
                       " dbo.AttendenceEntryMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.AttendenceEntryMaster.EmployeeID INNER JOIN " +
                       " dbo.AttandanceEntryNotes ON dbo.AttendenceEntryMaster.AttendanceId = dbo.AttandanceEntryNotes.AttendanceId where AttendenceEntryMaster.AttendanceId='" + Request.QueryString["AttendanceMasterId"] + "'";

        SqlCommand cmdwh = new SqlCommand(str, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);

        if (dtwh.Rows.Count > 0)
        {
            lblbatchname.Text = dtwh.Rows[0]["BatchName"].ToString();
            lblbusinessName.Text = dtwh.Rows[0]["Businessname"].ToString();
            lblemployeename.Text = dtwh.Rows[0]["EmployeeName"].ToString();
            lblregularstarttime.Text = dtwh.Rows[0]["InTime"].ToString();

            lblactualintime.Text = dtwh.Rows[0]["InTimeforcalculation"].ToString();
            lbllateearlyintime.Text = dtwh.Rows[0]["LateInMinuts"].ToString();
            lblregularouttime.Text = dtwh.Rows[0]["OutTime"].ToString();
            lblactualouttime.Text = dtwh.Rows[0]["OutTimeforcalculation"].ToString();
            lblletearlyouttime.Text = dtwh.Rows[0]["OutInMinuts"].ToString();
            lbltotalworkedhour.Text = dtwh.Rows[0]["TotalHourWork"].ToString();
            lblintimenote.Text = dtwh.Rows[0]["IntimeNote"].ToString();
            lblouttimenote.Text = dtwh.Rows[0]["OutTimeNote"].ToString();

        }

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
