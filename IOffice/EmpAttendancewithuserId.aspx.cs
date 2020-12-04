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
using System.Media;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Net;
public partial class productprofile : System.Web.UI.Page
{
    public string user;
    public string compny;
    string timezone = "";
    string plusminus = "";
    protected static SqlConnection cnn;



    public static DataTable chkAttAvailable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            HttpCookie logindetail2 = Request.Cookies["timezone"];
            if (logindetail2 != null)
            {
                ddltimezone.SelectedValue = logindetail2.Value;

            }

            fillsqlconn("");


            lbluserid.Visible = false;
            ReadOnlyCollection<TimeZoneInfo> tzi;
            tzi = TimeZoneInfo.GetSystemTimeZones();
            foreach (TimeZoneInfo timezone in tzi)
            {
                ddltimezone.Items.Add(new ListItem(timezone.DisplayName, timezone.Id));
            }
            //Page.Title = "Attandance Entry Master";

            //filltimezone();

            txtcompanyid.Focus();
            lbldtate.Visible = false;
            Label1date.Text = DateTime.Now.ToShortDateString();


            TimeSpan t1 = TimeSpan.Parse(DateTime.Now.ToUniversalTime().ToString("HH:mm"));
            time22.Text = t1.ToString();


            ddltimezone_SelectedIndexChanged(sender, e);




            //lblexit.Visible = false;

            //lblmsg3.Visible = false;

            //lblentry.Visible = false;
            //lbldatemsg.Visible = false;
            //lblwrongdate.Visible = false;

            lbldt.Visible = false;
            lbltime.Visible = false;
        }



    }
    protected void fillsqlconn(string cid)
    {
        // string strcln = "Select Sqlconnurlnodownload,Sqlportnodownload,sqluseridnodownload,sqlpasswordnodownload,sqlinstancenodownload,databasenamenodownload,CompanyLoginId from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join ProductMaster on ProductMaster.ProductId= PricePlanMaster.ProductId where CompanyMaster.Websiteurl='1133.onlineaccounts.net' ";
        if (cid == "")
        {
            string strcln = "Select Sqlconnurlnodownload,Sqlportnodownload,sqluseridnodownload,sqlpasswordnodownload,sqlinstancenodownload,databasenamenodownload,CompanyLoginId from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join ProductMaster on ProductMaster.ProductId= PricePlanMaster.ProductId where CompanyMaster.Websiteurl='" + Request.Url.Host + "' ";
            SqlCommand cmdcln = new SqlCommand(strcln, PageConn.licenseconn());
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            cnn = new SqlConnection();

            if (dtcln.Rows.Count > 0)
            {
                Session["Comid"] = Convert.ToString(dtcln.Rows[0]["CompanyLoginId"]);

                PageConn pcon = new PageConn();
                cnn.ConnectionString = PageConn.connnn;
            }
        }
        else
        {
            cnn = new SqlConnection();
            string strcln = "Select Sqlconnurlnodownload,Sqlportnodownload,sqluseridnodownload,sqlpasswordnodownload,sqlinstancenodownload,databasenamenodownload from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join ProductMaster on ProductMaster.ProductId= PricePlanMaster.ProductId where CompanyMaster.CompanyLoginId='" + Convert.ToString(txtcompanyid.Text) + "' ";
            SqlCommand cmdcln = new SqlCommand(strcln, PageConn.licenseconn());
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                // cnn.ConnectionString = @"Data Source =" + dtcln.Rows[0]["Sqlconnurlnodownload"] + "\\" + dtcln.Rows[0]["sqlinstancenodownload"] + "," + dtcln.Rows[0]["Sqlportnodownload"] + "; Initial Catalog = " + dtcln.Rows[0]["databasenamenodownload"] + "; Integrated Security = true";
                Session["Comid"] = Convert.ToString(txtcompanyid.Text);
                PageConn pcon = new PageConn();
                cnn.ConnectionString = PageConn.connnn;
            }
        }


    }
    protected void filltimezone()
    {
        string str = "Select ID,Name from TimeZoneMaster where Status='1'";
        SqlCommand cmd = new SqlCommand(str, cnn);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddltimezone.DataSource = dt;
            ddltimezone.DataTextField = "Name";
            ddltimezone.DataValueField = "ID";
            ddltimezone.DataBind();
        }
    }
    protected void timecalculate()
    {
        Label1date.Text = Convert.ToString(DateTime.Now.ToUniversalTime().ToShortDateString());

        string str = "select TimeZoneMaster.gmt from  [BatchMaster] inner join WHTimeZone on WHTimeZone.ID=BatchMaster.BatchTimeZone inner join [TimeZoneMaster] on TimeZoneMaster.Id=WHTimeZone.TimeZone Where BatchMaster.Whid='" + ViewState["Whid"] + "' and  BatchMaster.ID='" + ViewState["Bid"] + "'";
        SqlCommand cmd = new SqlCommand(str, cnn);
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

    protected DataTable WorkingDay()
    {
        DataTable dsday = new DataTable();
        DataTable ds123 = select("select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["Bid"] + "'  and DateMasterTbl.Date='" + Label1date.Text + "' ");

        //if (ds123.Rows.Count > 0)
        //{

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
        if (dsday.Rows.Count == 0)
        {
            timer1.Enabled = true;
            ModalPopupExtender11.Show();

            //lblmsg.Visible = true;
            //lblmsg.Text = "You are not allowed to add your attendance, as today is not a working day in your scheduled batch.";

        }

        //}
        //else
        //{
        //    timer1.Enabled = true;
        //    ModalPopupExtender11.Show();

        //    //lblmsg.Visible = true;
        //    //lblmsg.Text = "You are not allowed to add your attendance, as today is not a working day in your scheduled batch.";

        //}
        return dsday;
    }
    protected int CheckEntryaccess()
    {
        int flag = 0;
        string serdate = Convert.ToDateTime(Label1date.Text).ToShortDateString();
        string strapp = "";
        string strde = " Select * from AttandanceRule where StoreId='" + ViewState["Whid"] + "'";
        SqlDataAdapter adpde = new SqlDataAdapter(strde, cnn);
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
            SqlDataAdapter adpl = new SqlDataAdapter(strapp, cnn);
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
                SqlDataAdapter aadres = new SqlDataAdapter(strappaw, cnn);
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
                    //if( Convert.ToString(dtde.Rows[0]["blackattendanceminit"])=="00:00")
                    //{
                    //    ViewState["checkinout"]
                    //}
                    //else
                    //{
                    //}
                }
            }
        }

        return flag;

    }

    private void Company()
    {

        // string str1 = "select CompanyName from CompanyMaster where [Compid]='" + ViewState["Compid"] + "'";

        string str1 = "select Name from WareHouseMaster where [WareHouseId]='" + ViewState["Whid"] + "'";

        cnn.Close();
        cnn.Open();
        SqlCommand cmd = new SqlCommand(str1, cnn);

        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.HasRows)
        {
            while (dr.Read())
            {

                string str2 = dr["Name"].ToString();
                //CompanyName.Text = str2;
                lblm.Text = str2;
            }

        }
        dr.Close();
        cnn.Close();

    }
    protected int rest()
    {
        int cip = 0;
        int uip = 0;
        int flag = 0;
        string ipaddress = "";
        ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string str131 = "select Distinct EmployeeMaster.Whid, login_Master.username,login_Master.password,EmployeeMaster.EmployeeName,BatchMaster.Name as Bid,BatchMaster.Id from BatchMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_master on User_master.PartyId=Party_Master.PartyId inner join login_Master on login_Master.UserId= User_master.UserId Where login_Master.username='" + txtuname.Text + "' and login_Master.password='" + ClsEncDesc.Encrypted(txtpass.Text) + "' ";
        SqlDataAdapter adp2 = new SqlDataAdapter(str131, cnn);
        DataTable ds2 = new DataTable();
        adp2.Fill(ds2);
        if (ds2.Rows.Count > 0)
        {
            DataTable dic = ClsIp.selectIPrestrictionallow(txtcompanyid.Text);
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
            timer1.Enabled = true;
            ModalPopupExtender12.Show();

            //lblmsg.Text = "Sorry, we cannot locate your user ID and password in our system.</br> Please try again.</br>If you are a new employee,please consult with your supervisor to ensure that your user ID and password have been updated in our system.";
            //lblmsg.Visible = true;
        }
        return flag;
    }
    protected void matchbarlogin()
    {
        string indifftime = "";
        int alldevmail = 0;

        string str = "SELECT EmployeeMaster.EmployeeMasterID, BatchTiming.Totalhours, EmployeeMaster.EmployeeName,BatchTiming.StartTime,BatchTiming.EndTime, User_master.UserId, Login_master.username,Login_master.password" +
                      " FROM BatchTiming inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchTiming.BatchMasterId inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID  inner join User_Master on  EmployeeMaster.PartyID = User_Master.PartyID inner join Login_master on Login_master.UserID=User_Master.UserID and  EmployeeMaster.EmployeeMasterID=" + Session["EmployeeID1"] + "";

        SqlDataAdapter adp = new SqlDataAdapter(str, cnn);
        DataTable dt = new DataTable();
        cnn.Close();
        adp.Fill(dt);
        cnn.Open();
        if (dt.Rows.Count != 0)
        {
            string str2 = dt.Rows[0]["UserName"].ToString();
            string str4 = Label1date.Text;

            TimeSpan t1 = TimeSpan.Parse(chkAttAvailable.Rows[0]["StartTime"].ToString());
            TimeSpan t2 = TimeSpan.Parse(time22.Text);


            int msgshow = 0;

            int earlylatemin = 0;
            int earlylatemin1 = 0;
            int earlylatemin2 = 0;

            int devnoentryforregistercount = 0;
            lblempname.Text = dt.Rows[0]["EmployeeName"].ToString();
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
            SqlDataAdapter adpde = new SqlDataAdapter(strde, cnn);
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
                if (Convert.ToBoolean(dtde.Rows[0]["generatenotificemail"]) == true)
                {
                    earlylatemin = Convert.ToInt32(dtde.Rows[0]["notificemailallowedhours"]);
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
                        Label13.Text = "You are late by :";
                        Label14.Text = indifftime;
                        lbllate.Visible = true;
                        Label9.Visible = true;
                        latecout = 2;
                        countdevEntry = 2;

                        //if (Convert.ToString(dtde.Rows[0]["notificemailallowedhours"]) != "")
                        //{
                        //    earlylatemin1 = Convert.ToInt32(indifftime.Substring(4, 2));

                        //    if (earlylatemin1 > earlylatemin)
                        //    {
                        //        ViewState["MyStartTime"] = dt.Rows[0]["StartTime"].ToString();
                        //        ViewState["MyEndTime"] = dt.Rows[0]["EndTime"].ToString();
                        //        ViewState["MyInTime"] = intime;

                        //        ViewState["earlylatemin1"] = earlylatemin1;
                        //        sendmail11();
                        //    }
                        //}
                    }
                    else
                    {

                        indifftime = "00:00";
                        TimeSpan SPT = TimeSpan.Parse(chkAttAvailable.Rows[0]["StartTime"].ToString());
                        intime = Convert.ToDateTime(SPT).ToString("HH:mm");
                        Label13.Text = "";
                        Label14.Text = "";
                    }
                }
                else
                {
                    indifftime = "-" + indifftime;
                    Label13.Text = "You are late by :";
                    Label14.Text = indifftime;
                    latecout = 2;
                    lbllate.Visible = true;
                    Label9.Visible = true;

                    //if (Convert.ToString(dtde.Rows[0]["notificemailallowedhours"]) != "")
                    //{
                    //    earlylatemin1 = Convert.ToInt32(indifftime.Substring(4, 2));

                    //    if (earlylatemin1 > earlylatemin)
                    //    {
                    //        ViewState["MyStartTime"] = dt.Rows[0]["StartTime"].ToString();
                    //        ViewState["MyEndTime"] = dt.Rows[0]["EndTime"].ToString();
                    //        ViewState["MyInTime"] = intime;

                    //        ViewState["earlylatemin1"] = earlylatemin1;
                    //        sendmail11();
                    //    }
                    //}
                }
            }
            else
            {
                indifftime = t1.Subtract(t2).ToString();
                indifftime = Convert.ToDateTime(indifftime).ToString("HH:mm");
                indifftime = "+" + indifftime;
                Label13.Text = "You are early by :";
                Label14.Text = indifftime;


                //if (Convert.ToString(dtde.Rows[0]["notificemailallowedhours"]) != "")
                //{
                //    earlylatemin2 = Convert.ToInt32(indifftime.Substring(4, 2));

                //    if (earlylatemin2 > earlylatemin)
                //    {
                //        ViewState["MyStartTime"] = dt.Rows[0]["StartTime"].ToString();
                //        ViewState["MyEndTime"] = dt.Rows[0]["EndTime"].ToString();
                //        ViewState["MyInTime"] = intime;

                //        ViewState["earlylatemin2"] = earlylatemin2;
                //        sendmail11();
                //    }
                //}
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
                SqlCommand cmd1 = new SqlCommand(str3, cnn);
                //dr.Close();
                cmd1.ExecuteNonQuery();
                AttandanceEntryNotes();


                //  lblmsg.Text = "Welcome " + lblempname.Text + ", you have successfully entered.<br/> Your entry time:" + time22.Text;


                if (alldevmail == 1)
                {
                    //sendmail();
                }



                if (countdevEntry == 2)
                {

                    AttendenceDeviations(indifftime, "00:00");
                    alldevmail = 1;
                    if (countdevmessage == 2)
                    {
                        lbllatemessagereason.Text = "In";
                        msgshow = 1;
                        //timer1.Enabled = false;
                        ViewState["devap"] = devapproved;
                        //if (alldevmail == 1)
                        //{
                        sendmail();
                        //}
                        latereaso.Text = "";

                        SqlDataAdapter dafd = new SqlDataAdapter("select EmployeePayrollMaster.LastName + ' ' + EmployeePayrollMaster.FirstName + ' : ' + DesignationMaster.DesignationName as name from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID inner join DesignationMaster on EmployeeMaster.DesignationMasterId = DesignationMaster.DesignationMasterId where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'", cnn);
                        DataTable dtfd = new DataTable();
                        dafd.Fill(dtfd);

                        if (dtfd.Rows.Count > 0)
                        {
                            Label42.Text = dtfd.Rows[0]["name"].ToString();
                        }

                        Label43.Text = System.DateTime.Now.ToShortTimeString().Substring(0, 5);

                        SqlDataAdapter dabat = new SqlDataAdapter("select StartTime from EmployeeMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join BatchTiming on BatchTiming.BatchMasterId=EmployeeBatchMaster.Batchmasterid where EmployeeMasterID = '" + ViewState["EmployeeID"] + "'", cnn);
                        DataTable dtbat = new DataTable();
                        dabat.Fill(dtbat);

                        if (dtbat.Rows.Count > 0)
                        {
                            Label44.Text = dtbat.Rows[0]["StartTime"].ToString();
                        }

                        if (Convert.ToString(ViewState["modal4"]) != "")
                        {

                            Label46.Text = ViewState["modal4"].ToString();

                            if (devnoentryforregistercount == Convert.ToInt32(ViewState["modal4"]))
                            {
                                Label50.Visible = true;
                            }
                        }


                        SqlDataAdapter daemm = new SqlDataAdapter("select SeniorEmployeeID from AttandanceRule where StoreId = '" + ViewState["Whid"] + "'", cnn);
                        DataTable dtemm = new DataTable();
                        daemm.Fill(dtemm);

                        SqlDataAdapter dafd1 = new SqlDataAdapter("select EmployeePayrollMaster.LastName + ' ' + EmployeePayrollMaster.FirstName + ' : ' + DesignationMaster.DesignationName as seniorname from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID inner join DesignationMaster on EmployeeMaster.DesignationMasterId = DesignationMaster.DesignationMasterId where EmployeeMaster.EmployeeMasterID='" + dtemm.Rows[0]["SeniorEmployeeID"].ToString() + "'", cnn);
                        DataTable dtfd1 = new DataTable();
                        dafd1.Fill(dtfd1);

                        if (dtfd1.Rows.Count > 0)
                        {
                            Label48.Text = dtfd1.Rows[0]["seniorname"].ToString();
                        }

                        ViewState["In"] = "mak";

                        timer1.Enabled = true;

                        ModalPopupExtender4.Show();
                    }
                    else
                    {
                        Label4.Text = lblempname.Text;
                        lblentrytimee.Text = time22.Text;

                        timer1.Enabled = true;
                        ModalPopupExtender5.Show();
                    }

                    if (siniourcount == 2)
                    {
                        Label51.Visible = true;

                        //lbllate.Text = "Your check in /check out time has been recorded, however, please contact your supervisor regarding your attendance.";
                        //lbllate.Visible = true;
                        //lblentry.Visible = true;
                        //lblentry.Text = "Your check in /check out time has been recorded, however, please contact your supervisor regarding your attendance.";

                        Label8.Visible = true;
                        lbllate.Visible = false;
                        Label9.Visible = false;
                    }

                }
                else
                {
                    Label4.Text = lblempname.Text;
                    lblentrytimee.Text = time22.Text;

                    timer1.Enabled = true;
                    ModalPopupExtender5.Show();
                }

                cnn.Close();

                SqlDataAdapter dadxc = new SqlDataAdapter("select IntimeNote from  AttandanceEntryNotes where AttendanceId='" + ViewState["atid"] + "'", cnn);
                DataTable dtdxc = new DataTable();
                dadxc.Fill(dtdxc);

                if (dtdxc.Rows.Count > 0)
                {
                    if (Convert.ToString(dtdxc.Rows[0]["IntimeNote"]) != "")
                    {
                        ViewState["MyInNote"] = Convert.ToString(dtdxc.Rows[0]["IntimeNote"]);
                    }
                    else
                    {
                        ViewState["MyInNote"] = "";
                    }
                }

                if (Convert.ToString(dtde.Rows[0]["notificemailallowedhours"]) != "")
                {
                    string mm1 = "";
                    string mm2 = "";
                    string mm3 = "";

                    mm1 = indifftime.Substring(1, 5);
                    mm2 = mm1.Substring(0, 2);
                    mm3 = mm1.Substring(3, 2);

                    Int32 in1 = 0;
                    Int32 HourtoMinute1 = 0;
                    Int32 Minute1 = 0;
                    Int32 TotalMinutes132 = 0;

                    in1 = Convert.ToInt32(mm2);
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(mm3);
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                    earlylatemin1 = TotalMinutes132;

                    if (earlylatemin1 > earlylatemin)
                    {
                        ViewState["MyStartTime"] = dt.Rows[0]["StartTime"].ToString();
                        ViewState["MyEndTime"] = dt.Rows[0]["EndTime"].ToString();
                        ViewState["MyInTime"] = intime;

                        ViewState["earlylatemin1"] = earlylatemin1;
                       // sendmail11();
                    }
                }

                if (Convert.ToString(dtde.Rows[0]["notificemailallowedhours"]) != "")
                {
                    string mm1 = "";
                    string mm2 = "";
                    string mm3 = "";

                    mm1 = indifftime.Substring(1, 5);
                    mm2 = mm1.Substring(0, 2);
                    mm3 = mm1.Substring(3, 2);

                    Int32 in1 = 0;
                    Int32 HourtoMinute1 = 0;
                    Int32 Minute1 = 0;
                    Int32 TotalMinutes132 = 0;

                    in1 = Convert.ToInt32(mm2);
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(mm3);
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                    earlylatemin2 = TotalMinutes132;

                    if (earlylatemin2 > earlylatemin)
                    {
                        ViewState["MyStartTime"] = dt.Rows[0]["StartTime"].ToString();
                        ViewState["MyEndTime"] = dt.Rows[0]["EndTime"].ToString();
                        ViewState["MyInTime"] = intime;

                        ViewState["earlylatemin2"] = earlylatemin2;
                        //sendmail11();
                    }
                }

                Label8.Visible = false;


                if (msgshow == 0)
                {
                    // ModalPopupExtender1.Show();

                    //timer1.Enabled = true;



                }

            }
            else
            {
                timer1.Enabled = true;
                ModalPopupExtender7.Show();

                //lblentry.Text = "Your attendance was not recorded as you are currently restricted because you have exceeded the allowed number of instances.  Please check with supervisor.";
                Label8.Visible = true;
                lbllate.Visible = false;
                Label9.Visible = false;
                lblentry.Visible = true;

            }




        }

    }
    protected int fillsuperapp(int supeid)
    {
        int i = 0;
        string str = "Select  Count(SuprviserId) as SuprviserId from EmployeeMaster inner join AttendenceEntryMaster on AttendenceEntryMaster.EmployeeID=EmployeeMaster.EmployeeMasterID where EmployeeMasterID='" + Session["EmployeeID1"] + "'";

        SqlDataAdapter adp2 = new SqlDataAdapter(str, cnn);
        DataTable dt1 = new DataTable();

        adp2.Fill(dt1);
        if (Convert.ToInt32(dt1.Rows[0]["SuprviserId"]) >= supeid)
        {
            i = 2;
        }

        return i;
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, cnn);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

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

                SqlDataAdapter adp2 = new SqlDataAdapter(str2, cnn);
                DataTable dt1 = new DataTable();

                adp2.Fill(dt1);

                ViewState["modal4"] = dt1.Rows[0]["Id"].ToString();

                if (Convert.ToInt32(dt1.Rows[0]["Id"]) >= devrulesno)
                {
                    i = 2;
                }
            }
        }

        return i;
    }
    protected void AttandanceEntryNotesupdate()
    {

        string str3 = "update AttandanceEntryNotes set OutTimeNote='' where AttendanceId=" + Session["AttendanceId"] + "";

        SqlCommand cmd1 = new SqlCommand(str3, cnn);
        cnn.Open();
        cmd1.ExecuteNonQuery();
        cnn.Close();
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
                SqlCommand cmd1 = new SqlCommand(str3, cnn);
                ////dr.Close();
                cmd1.ExecuteNonQuery();
                string str = "SELECT Max(ID)  FROM AttendenceDeviations where attandanceId='" + ViewState["atid"] + "'";
                SqlDataAdapter adp = new SqlDataAdapter(str, cnn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ViewState["dev"] = Convert.ToString(dt.Rows[0][0]);
                }
            }

        }

    }
    protected void AttandanceEntryNotes()
    {
        string str = "SELECT Max(AttendanceId) as AttendanceId FROM AttendenceEntryMaster where EmployeeID='" + Session["EmployeeID1"] + "'";
        SqlDataAdapter adp = new SqlDataAdapter(str, cnn);
        DataTable dt = new DataTable();
        cnn.Close();
        adp.Fill(dt);
        cnn.Open();
        if (Convert.ToString(dt.Rows[0]["AttendanceId"]) != "")
        {
            ViewState["atid"] = dt.Rows[0]["AttendanceId"];
            string str3 = "insert into AttandanceEntryNotes(IntimeNote,AttendanceId)values('','" + dt.Rows[0]["AttendanceId"] + "')";
            //dr.Close();
            SqlCommand cmd1 = new SqlCommand(str3, cnn);
            //dr.Close();
            cmd1.ExecuteNonQuery();
        }
    }
    protected void matchbarlogout()
    {
        int alldevmail = 0;
        string totalwork = "";
        string outdifftime = "";
        string str = "SELECT Distinct Overtime, AttendenceEntryMaster.LateInMinuts, EmployeeMaster.EmployeeMasterID, EmployeeMaster.EmployeeName,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.ConsiderHalfDayLeave,AttendenceEntryMaster.ConsiderFullDayLeave,AttendenceEntryMaster.InTime,Totalhours, User_master.UserId, Login_master.username,Login_master.password,AttendenceEntryMaster.InTime,AttendenceEntryMaster.InTimeforcalculation, AttendenceEntryMaster.AttendanceId" +
                     " FROM BatchTiming inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchTiming.BatchMasterId inner join AttendenceEntryMaster on AttendenceEntryMaster.EmployeeID=EmployeeBatchMaster.Employeeid   inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID  inner join User_Master on  EmployeeMaster.PartyID = User_Master.PartyID inner join Login_master on Login_master.UserID=User_Master.UserID and  AttendenceEntryMaster.AttendanceId=" + Session["AttendanceId"] + "";

        SqlDataAdapter adp = new SqlDataAdapter(str, cnn);

        DataTable dt = new DataTable();
       // cnn.Close();
       // cnn.Open();
        adp.Fill(dt);

        if (dt.Rows.Count != 0)
        {
            chkAttAvailable = WorkingDay();
            int msgshow = 0;
            ViewState["atid"] = Session["AttendanceId"];

            int latecout = 0;

            int earlylatemin = 0;
            int earlylatemin1 = 0;
            int earlylatemin2 = 0;

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
            SqlDataAdapter adpde = new SqlDataAdapter(strde, cnn);
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
                if (Convert.ToBoolean(dtde.Rows[0]["generatenotificemail"]) == true)
                {
                    earlylatemin = Convert.ToInt32(dtde.Rows[0]["notificemailallowedhours"]);
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
                        Label16.Text = "You are early exit time :";
                        lblouterly.Text = outdifftime;
                        Label18.Visible = true;
                        Label20.Visible = true;
                        latecout = 2;

                        //if (Convert.ToString(dtde.Rows[0]["notificemailallowedhours"]) != "")
                        //{
                        //    earlylatemin2 = Convert.ToInt32(outdifftime.Substring(4, 2));

                        //    if (earlylatemin2 > earlylatemin)
                        //    {
                        //        ViewState["MyStartTime1"] = dt.Rows[0]["InTime"].ToString();
                        //        ViewState["MyEndTime1"] = dt.Rows[0]["OutTime"].ToString();
                        //        ViewState["MyOutTime"] = time22.Text;

                        //        ViewState["earlylatemin2"] = earlylatemin2;
                        //        sendmail11();
                        //    }
                        //}
                    }
                    else
                    {
                        outdifftime = "00:00";


                        Label13.Text = "";
                        Label14.Text = "";

                    }
                }
                else
                {
                    outdifftime = Convert.ToDateTime(outdifftime).ToString("HH:mm");
                    outdifftime = "-" + outdifftime;
                    Label16.Text = "You are early exit time :";
                    lblouterly.Text = outdifftime;
                    Label18.Visible = true;
                    Label20.Visible = true;
                    latecout = 2;

                    //if (Convert.ToString(dtde.Rows[0]["notificemailallowedhours"]) != "")
                    //{
                    //    earlylatemin2 = Convert.ToInt32(outdifftime.Substring(4, 2));

                    //    if (earlylatemin2 > earlylatemin)
                    //    {
                    //        ViewState["MyStartTime1"] = dt.Rows[0]["InTime"].ToString();
                    //        ViewState["MyEndTime1"] = dt.Rows[0]["OutTime"].ToString();
                    //        ViewState["MyOutTime"] = time22.Text;

                    //        ViewState["earlylatemin2"] = earlylatemin2;
                    //        sendmail11();
                    //    }
                    //}
                }
            }
            else
            {
                outdifftime = t4.Subtract(t3).ToString();

                outdifftime = Convert.ToDateTime(outdifftime).ToString("HH:mm");
                outdifftime = "+" + outdifftime;
                Label16.Text = "You are late exit time :";
                lblouterly.Text = outdifftime;
                //  latecout = 2;

                //if (Convert.ToString(dtde.Rows[0]["notificemailallowedhours"]) != "")
                //{
                //    earlylatemin1 = Convert.ToInt32(outdifftime.Substring(4, 2));

                //    if (earlylatemin1 > earlylatemin)
                //    {
                //        ViewState["MyStartTime1"] = dt.Rows[0]["InTime"].ToString();
                //        ViewState["MyEndTime1"] = dt.Rows[0]["OutTime"].ToString();
                //        ViewState["MyOutTime"] = time22.Text;

                //        ViewState["earlylatemin1"] = earlylatemin1;
                //        sendmail11();
                //    }
                //}
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

                SqlCommand cmd1 = new SqlCommand(str3, cnn);
                cnn.Open();
                cmd1.ExecuteNonQuery();
                cnn.Close();
                //  lblmsg.Text = "Thankyou " + lblempname.Text + ",you have successfully checked out.<br/> Your check out time:" + time22.Text;

                AttandanceEntryNotesupdate();
                if (countdevEntry == 2)
                {

                    AttendenceDeviations("00:00", outdifftime);
                    alldevmail = 1;
                    if (countdevmessage == 2)
                    {
                        lbllatemessagereason.Text = "Out";
                        msgshow = 1;
                        // timer1.Enabled = false;
                        latereaso.Text = "";
                        ViewState["devap"] = devapproved;
                        //if (alldevmail == 1)
                        //{
                      //  sendmail();
                        //}
                        latereaso.Text = "";

                        SqlDataAdapter dafd = new SqlDataAdapter("select EmployeePayrollMaster.LastName + ' ' + EmployeePayrollMaster.FirstName + ' : ' + DesignationMaster.DesignationName as name from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID inner join DesignationMaster on EmployeeMaster.DesignationMasterId = DesignationMaster.DesignationMasterId where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'", cnn);
                        DataTable dtfd = new DataTable();
                        dafd.Fill(dtfd);

                        if (dtfd.Rows.Count > 0)
                        {
                            Label42.Text = dtfd.Rows[0]["name"].ToString();
                        }

                        Label43.Text = System.DateTime.Now.ToShortTimeString().Substring(0, 5);

                        SqlDataAdapter dabat = new SqlDataAdapter("select StartTime from EmployeeMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join BatchTiming on BatchTiming.BatchMasterId=EmployeeBatchMaster.Batchmasterid where EmployeeMasterID = '" + ViewState["EmployeeID"] + "'", cnn);
                        DataTable dtbat = new DataTable();
                        dabat.Fill(dtbat);

                        if (dtbat.Rows.Count > 0)
                        {
                            Label44.Text = dtbat.Rows[0]["StartTime"].ToString();
                        }

                        if (Convert.ToString(ViewState["modal4"]) != "")
                        {

                            Label46.Text = ViewState["modal4"].ToString();

                            if ((devnoentryforregistercount - 1) == Convert.ToInt32(ViewState["modal4"]))
                            {
                                Label50.Visible = true;
                            }
                        }

                        SqlDataAdapter daemm = new SqlDataAdapter("select SeniorEmployeeID from AttandanceRule where StoreId = '" + ViewState["Whid"] + "'", cnn);
                        DataTable dtemm = new DataTable();
                        daemm.Fill(dtemm);

                        SqlDataAdapter dafd1 = new SqlDataAdapter("select EmployeePayrollMaster.LastName + ' ' + EmployeePayrollMaster.FirstName + ' : ' + DesignationMaster.DesignationName as seniorname from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID inner join DesignationMaster on EmployeeMaster.DesignationMasterId = DesignationMaster.DesignationMasterId where EmployeeMaster.EmployeeMasterID='" + dtemm.Rows[0]["SeniorEmployeeID"].ToString() + "'", cnn);
                        DataTable dtfd1 = new DataTable();
                        dafd1.Fill(dtfd1);

                        if (dtfd1.Rows.Count > 0)
                        {
                            Label48.Text = dtfd1.Rows[0]["seniorname"].ToString();
                        }

                        ViewState["Out"] = "mak";

                        timer1.Enabled = true;

                        ModalPopupExtender4.Show();
                    }
                    else
                    {
                        Label211.Text = lblempname.Text;
                        Label32.Text = time22.Text;

                        timer1.Enabled = true;
                        ModalPopupExtender6.Show();
                    }

                    if (siniourcount == 2)
                    {
                        Label51.Visible = true;

                        //Label20.Text = "Your check in /check out time has been recorded, however, please contact your supervisor regarding your attendance.";
                        //Label20.Visible = true;
                        //lblentry.Visible = true;
                        //lblentry.Text = "Your check in /check out time has been recorded, however, please contact your supervisor regarding your attendance.";
                        Label8.Visible = true;
                        lbllate.Visible = false;
                        Label9.Visible = false;
                    }
                }
                else
                {
                    Label211.Text = lblempname.Text;
                    Label32.Text = time22.Text;

                    timer1.Enabled = true;
                    ModalPopupExtender6.Show();
                }

                cnn.Close();

                SqlDataAdapter dadxc = new SqlDataAdapter("select OutTimeNote from  AttandanceEntryNotes where AttendanceId='" + Session["AttendanceId"] + "'", cnn);
                DataTable dtdxc = new DataTable();
                dadxc.Fill(dtdxc);

                if (dtdxc.Rows.Count > 0)
                {
                    if (Convert.ToString(dtdxc.Rows[0]["OutTimeNote"]) != "")
                    {
                        ViewState["MyOutNote"] = Convert.ToString(dtdxc.Rows[0]["OutTimeNote"]);
                    }
                    else
                    {
                        ViewState["MyOutNote"] = "";
                    }
                }

                if (Convert.ToString(dtde.Rows[0]["notificemailallowedhours"]) != "")
                {
                    string mm1 = "";
                    string mm2 = "";
                    string mm3 = "";

                    mm1 = outdifftime.Substring(1, 5);
                    mm2 = mm1.Substring(0, 2);
                    mm3 = mm1.Substring(3, 2);

                    Int32 in1 = 0;
                    Int32 HourtoMinute1 = 0;
                    Int32 Minute1 = 0;
                    Int32 TotalMinutes132 = 0;

                    in1 = Convert.ToInt32(mm2);
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(mm3);
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                    earlylatemin2 = TotalMinutes132;

                    //earlylatemin2 = Convert.ToInt32(outdifftime.Substring(4, 2));

                    if (earlylatemin2 > earlylatemin)
                    {
                        ViewState["MyStartTime1"] = dt.Rows[0]["InTime"].ToString();
                        ViewState["MyEndTime1"] = dt.Rows[0]["OutTime"].ToString();
                        ViewState["MyOutTime"] = time22.Text;

                        ViewState["earlylatemin2"] = earlylatemin2;
                        //sendmail11();
                    }
                }

                if (Convert.ToString(dtde.Rows[0]["notificemailallowedhours"]) != "")
                {
                    string mm1 = "";
                    string mm2 = "";
                    string mm3 = "";

                    mm1 = outdifftime.Substring(1, 5);
                    mm2 = mm1.Substring(0, 2);
                    mm3 = mm1.Substring(3, 2);

                    Int32 in1 = 0;
                    Int32 HourtoMinute1 = 0;
                    Int32 Minute1 = 0;
                    Int32 TotalMinutes132 = 0;

                    in1 = Convert.ToInt32(mm2);
                    HourtoMinute1 = in1 * 60;
                    Minute1 = Convert.ToInt32(mm3);
                    TotalMinutes132 = (HourtoMinute1) + (Minute1);

                    earlylatemin1 = TotalMinutes132;

                    //earlylatemin1 = Convert.ToInt32(outdifftime.Substring(4, 2));

                    if (earlylatemin1 > earlylatemin)
                    {
                        ViewState["MyStartTime1"] = dt.Rows[0]["InTime"].ToString();
                        ViewState["MyEndTime1"] = dt.Rows[0]["OutTime"].ToString();
                        ViewState["MyOutTime"] = time22.Text;

                        ViewState["earlylatemin1"] = earlylatemin1;
                       // sendmail11();
                    }
                }

                lblempname.Text = dt.Rows[0]["EmployeeName"].ToString();
                lblentry.Visible = true;
                Label9.Visible = false;

                if (msgshow == 0)
                {
                    Label211.Text = lblempname.Text;
                    Label32.Text = time22.Text;

                    timer1.Enabled = true;
                    ModalPopupExtender6.Show();

                    //lblmsg.Text = "Thankyou " + lblempname.Text + ",you have successfully checked out.<br/> Your check out time:" + time22.Text;


                    //  ModalPopupExtender2.Show();
                    //timer1.Enabled = true;

                    if (alldevmail == 1)
                    {
                        //sendmail();
                    }
                }

            }
            else
            {
                //lblentry.Visible = true;                
                //lblentry.Text = "  Your attendance was not recorded as you are currently restricted because you have exceeded the allowed number of instances.  Please check with supervisor.";

                timer1.Enabled = true;
                ModalPopupExtender7.Show();

                Label9.Visible = true;
            }

        }



    }
    protected void timer1_Tick(object sender, EventArgs e)
    {
        if (timer1.Enabled == true)
        {
            ModalPopupExtender1.Hide();
            ModalPopupExtender2.Hide();
            ModalPopupExtender5.Hide();
            ModalPopupExtender6.Hide();
            ModalPopupExtender7.Hide();
            ModalPopupExtender8.Hide();
            ModalPopupExtender9.Hide();
            ModalPopupExtender10.Hide();
            ModalPopupExtender11.Hide();
            ModalPopupExtender12.Hide();

            timer1.Enabled = false;
            TimerTime.Enabled = false;

            txtcompanyid.Focus();
        }
    }
    protected void txtbartext_TextChanged(object sender, EventArgs e)
    {

        //btnGo_Click(sender, e);
        //txtbartext.Focus();

    }
    protected void btnsubm_Click(object sender, EventArgs e)
    {
        SqlCommand Mycommand = new SqlCommand("Select password from User_Master inner join login_master on login_master.UserId=User_Master.UserID  where login_master.username='" + txtuerlog.Text + "' and password='" + ClsEncDesc.Encrypted(lbltxtpass.Text) + "' ", cnn);

        SqlDataAdapter MyDataAdapter = new SqlDataAdapter(Mycommand);
        DataTable ds1 = new DataTable();
        MyDataAdapter.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            string str = "Delete   FROM AttandanceTempBanIpTable where " + ViewState["ipdel111"];
            SqlCommand adp = new SqlCommand(str, cnn);
            if (cnn.State.ToString() != "Open")
            {
                cnn.Open();
            }
            adp.ExecuteNonQuery();
            cnn.Close();
            ModalPopupExtender16.Show();
            lblmsg.Visible = true;
            lblmsg.Text = "You have give the permited off SignIn/SignOut";
            // ModalPopupExtender3.Show();
            txtuerlog.Text = "";
            lbltxtpass.Text = "";
            //  timer1.Enabled = false;
        }
        else
        {
            txtuerlog.Text = "";
            lbltxtpass.Text = "";

        }

    }
    protected void btnsubm0_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Hide();
        // txtbartext.Focus();
        txtuerlog.Text = "";
        lbltxtpass.Text = "";
    }
    protected void timer2_Tick(object sender, EventArgs e)
    {
        //timer2.Enabled = false;
        // txtbartext.Text = "";
    }
    protected void btnaddlater_Click(object sender, EventArgs e)
    {
        string str3 = "update AttendenceEntryMaster set Varify='" + Convert.ToBoolean(ViewState["devap"]) + "' where AttendanceId=" + ViewState["atid"] + "";

        SqlCommand cmd1 = new SqlCommand(str3, cnn);
        if (cnn.State.ToString() != "Open")
        {
            cnn.Open();
        }
        cmd1.ExecuteNonQuery();

        str3 = "update AttendenceDeviations set note='" + latereaso.Text + "' where Id=" + ViewState["dev"] + "";
        SqlCommand cmd11 = new SqlCommand(str3, cnn);

        cmd11.ExecuteNonQuery();
        if (lbllatemessagereason.Text == "In")
        {
            string str32 = "update AttandanceEntryNotes set IntimeNote='" + latereaso.Text + "' where AttendanceId=" + ViewState["atid"] + "";
            SqlCommand cmd12 = new SqlCommand(str32, cnn);
            cmd12.ExecuteNonQuery();
        }
        else
        {
            string str32 = "update AttandanceEntryNotes set OuttimeNote='" + latereaso.Text + "' where AttendanceId=" + ViewState["atid"] + "";
            SqlCommand cmd12 = new SqlCommand(str32, cnn);
            cmd12.ExecuteNonQuery();
        }

        if (Convert.ToString(ViewState["In"]) != "")
        {
            if (ViewState["In"].ToString() == "mak")
            {
                ViewState["In"] = "";

                Label4.Text = lblempname.Text;
                lblentrytimee.Text = time22.Text;

                timer1.Enabled = true;
                ModalPopupExtender5.Show();
            }
        }
        if (Convert.ToString(ViewState["Out"]) != "")
        {
            if (ViewState["Out"].ToString() == "mak")
            {
                ViewState["Out"] = "";

                Label211.Text = lblempname.Text;
                Label32.Text = time22.Text;

                timer1.Enabled = true;
                ModalPopupExtender6.Show();
            }
        }
    }
    protected void ddltimezone_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dt = System.DateTime.Now;
        //string timezone = ddltimezone.SelectedItem.Text.ToString();
        //DateTime text = TimeZoneInfo.ConvertTime(dt,TimeZoneInfo.Utc,TimeZoneInfo. "(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi");
        //  time22.Text = TimeZoneInfo.ConvertTime(dt, TimeZoneInfo.Local.DisplayName, "(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi");

        time22.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, ddltimezone.SelectedValue).ToString("hh:mm:ss");
        lblhour.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, ddltimezone.SelectedValue).ToString("hh :");
        lblmin.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, ddltimezone.SelectedValue).ToString("mm :");
        lblsec.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, ddltimezone.SelectedValue).ToString("ss");
        Label1date.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, ddltimezone.SelectedValue).ToShortDateString();
    }
    protected void TimerTime_Tick(object sender, EventArgs e)
    {
        DateTime dt = System.DateTime.Now;
        time22.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, ddltimezone.SelectedValue).ToString("hh:mm:ss");
        lblhour.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, ddltimezone.SelectedValue).ToString("hh :");
        lblmin.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, ddltimezone.SelectedValue).ToString("mm :");
        lblsec.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, ddltimezone.SelectedValue).ToString("ss");
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender2.Hide();
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        matchbarlogout();
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        TimerTime.Enabled = true;
    }

    protected void btnempcode_Click(object sender, EventArgs e)
    {

        if (txtuname.Text.Length > 0 || txtpass.Text.Length > 0 || txtcompanyid.Text.Length > 0)
        {
            fillsqlconn(txtcompanyid.Text);

            Session["FromDate1"] = "";
            Session["ToDate1"] = "";


            Session["Comid"] = txtcompanyid.Text;

            //if (cnn.State.ToString() != "Open")
            //{
            //    cnn.Open();
            //}
            //Boolean iptrack = true;

            cnn.Open();
            string ipaddress = "";
            DateTime datet = DateTime.Now.AddMinutes(-10);
            ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string strempcode = " select Distinct EmployeeMaster.Whid,EmployeeMaster.EmployeeMasterID as Employee_Id, CompanyMaster.CompanyName,CompanyMaster.Compid,WareHouseMaster.Name,EmployeeMaster.EmployeeName,BatchMaster.Name as Bid,BatchMaster.Id from BatchMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId= EmployeeMaster.EmployeeMasterID inner join WareHouseMaster on  WareHouseMaster.WarehouseId=EmployeeMaster.whid inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_master on User_master.PartyId=Party_Master.PartyId inner join login_Master on login_Master.UserId= User_master.UserId inner join CompanyMaster on CompanyMaster.compid=WareHouseMaster.comid  where WareHouseMaster.comid='" + txtcompanyid.Text + "' and login_Master.username='" + txtuname.Text + "' and login_Master.password='" + ClsEncDesc.Encrypted(txtpass.Text) + "' ";
            SqlCommand csx = new SqlCommand(strempcode, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(csx);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            if (ds.Rows.Count > 0)
            {
                ViewState["Whid"] = ds.Rows[0]["Whid"].ToString();
                ViewState["Compid"] = ds.Rows[0]["Compid"].ToString();

                SqlDataAdapter da1 = new SqlDataAdapter("select timer from AttandanceRule where compid='" + ViewState["Compid"] + "' and storeid='" + ViewState["Whid"] + "'", cnn);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                if (dt1.Rows.Count > 0)
                {
                    if (Convert.ToString(dt1.Rows[0]["timer"]) != "")
                    {
                        ViewState["int"] = Convert.ToInt32(dt1.Rows[0]["timer"]);
                        timer1.Interval = (Convert.ToInt32(ViewState["int"]) * 1000);
                    }
                    else
                    {
                        timer1.Interval = 15000;
                    }
                }


                ViewState["Bid"] = ds.Rows[0]["Id"].ToString();
                timecalculate();
                Session["EmployeeID1"] = ds.Rows[0]["Employee_Id"];
                ViewState["EmployeeID"] = ds.Rows[0]["Employee_Id"];
                int flag = rest();

                if (flag == 1)
                {

                    ViewState["ipdel111"] = "IpAddress='" + ipaddress + "' and  Compid='" + Convert.ToString(ViewState["Compid"]) + "' and   CAST(Datetime as  Datetime) Between   CAST('" + datet + "'as Datetime) and  CAST('" + DateTime.Now.ToString() + "' as Datetime)";

                    string stripa = "select * from  [AttandanceTempBanIpTable] where " + ViewState["ipdel111"];
                    SqlCommand cmdipa = new SqlCommand(stripa, cnn);
                    SqlDataAdapter adpipa = new SqlDataAdapter(cmdipa);
                    DataTable dsipa = new DataTable();
                    adpipa.Fill(dsipa);
                    if (dsipa.Rows.Count < 3)
                    {
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
                                DataTable dfgsd = select("select * from AbsenseNote where date='" + System.DateTime.Now.ToShortDateString() + "' and empid='" + ViewState["EmployeeID"] + "'");

                                if (dfgsd.Rows.Count > 0)
                                {
                                    timer1.Enabled = true;

                                    ModalPopupExtender15.Show();
                                }
                                else
                                {

                                    string strapp = "";
                                    string strde = " Select * from AttandanceRule where StoreId='" + ViewState["Whid"] + "'";
                                    SqlDataAdapter adpde = new SqlDataAdapter(strde, cnn);
                                    DataTable dtde = new DataTable();
                                    adpde.Fill(dtde);
                                    if (dtde.Rows.Count > 0)
                                    {
                                        ViewState["Stimeall"] = "";
                                        ViewState["Etimeall"] = "";
                                        if (Convert.ToString(dtde.Rows[0]["lateentryallowed"]) != "")
                                        {
                                            if (Convert.ToBoolean(dtde.Rows[0]["lateentryallowed"]) == true)
                                            {
                                                ViewState["Stimeall"] = Convert.ToInt32(dtde.Rows[0]["Maxuserhours"]);

                                                ViewState["Etimeall"] = Convert.ToInt32(dtde.Rows[0]["Maxuserhours"]);
                                            }
                                        }

                                        if (Convert.ToBoolean(dtde.Rows[0]["rulegreatertime"]) == true)
                                        {
                                            int rulegreatertime = Convert.ToInt32(dtde.Rows[0]["Maxuserhours"]);
                                            //DataTable dt = WorkingDay();
                                            DataTable dt = chkAttAvailable;
                                            if (dt.Rows.Count > 0)
                                            {


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
                                        else
                                        {
                                            strapp = "SELECT Distinct EmployeeMaster.EmployeeMasterID,AttendenceEntryMaster.InTimeforcalculation,AttendenceEntryMaster.InTime,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Date,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.InTime " +
                                         "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + ds.Rows[0]["Employee_Id"] + "') and (AttendenceEntryMaster.OutTimeforcalculation='00:00') and (AttendenceEntryMaster.Date='" + Label1date.Text + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(1).ToShortDateString() + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(-1).ToShortDateString() + "') and Batchmasterid='" + ViewState["Bid"] + "'";

                                        }
                                    }
                                    else
                                    {
                                        strapp = "SELECT Distinct EmployeeMaster.EmployeeMasterID,AttendenceEntryMaster.InTimeforcalculation,AttendenceEntryMaster.InTime,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Date,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.InTime " +
                                         "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + ds.Rows[0]["Employee_Id"] + "') and (AttendenceEntryMaster.OutTimeforcalculation='00:00') and (AttendenceEntryMaster.Date='" + Label1date.Text + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(1).ToShortDateString() + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(-1).ToShortDateString() + "') and Batchmasterid='" + ViewState["Bid"] + "'";

                                    }
                                    Company();
                                    //string str = "SELECT Distinct EmployeeMaster.EmployeeMasterID,AttendenceEntryMaster.InTimeforcalculation,AttendenceEntryMaster.InTime,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Date,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.InTime " +
                                    // "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + ds.Rows[0]["Employee_Id"] + "') and (AttendenceEntryMaster.OutTimeforcalculation='00:00') and (AttendenceEntryMaster.Date='" + Label1date.Text + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(1).ToShortDateString() + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(-1).ToShortDateString() + "') and Batchmasterid='" + ViewState["Bid"] + "'";
                                    SqlDataAdapter adpl = new SqlDataAdapter(strapp, cnn);
                                    DataTable dsl = new DataTable();
                                    adpl.Fill(dsl);
                                    if (dsl.Rows.Count > 0)
                                    {
                                        Session["AttendanceId"] = dsl.Rows[0]["AttendanceId"];

                                        lblgoemp.Text = ds.Rows[0]["EmployeeName"].ToString() + " : " + ds.Rows[0]["Employee_Id"];
                                        lblexittime.Text = time22.Text;
                                        Label22.Text = dsl.Rows[0]["InTimeforcalculation"].ToString();
                                        if (Convert.ToString(ViewState["Etime"]) != "")
                                        {
                                            TimeSpan tc = new TimeSpan();
                                            tc = TimeSpan.Parse(ViewState["Etime"].ToString());

                                            DateTime dtsc = Convert.ToDateTime(Label1date.Text);
                                            // DateTime dtsc = Convert.ToDateTime(Label1date.Text).Add(tc);
                                            if (Convert.ToString(ViewState["Stimeall"]) != "")
                                            {
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
                                                        if (sc != "-1")
                                                        //if (cmn != "1")
                                                        {
                                                            lblentry.Text = "";
                                                            ModalPopupExtender16.Hide();
                                                            lblmsg.Text = "";
                                                            TimerTime.Enabled = false;

                                                            // ModalPopupExtender1222.Show();
                                                            // matchbarlogout();

                                                            SqlDataAdapter da = new SqlDataAdapter("select EmployeePayrollMaster.LastName + ' ' + EmployeePayrollMaster.FirstName as name,EmployeePayrollMaster.EmployeeNo from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID  where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeId"] + "'", cnn);
                                                            DataTable dt = new DataTable();
                                                            da.Fill(dt);

                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                Label59ss.Text = dt.Rows[0]["name"].ToString();

                                                                if (Convert.ToString(dt.Rows[0]["EmployeeNo"]) != "")
                                                                {
                                                                    Label60.Text = dt.Rows[0]["EmployeeNo"].ToString();
                                                                }
                                                            }

                                                            Label60a.Text = System.DateTime.Now.ToShortDateString();
                                                            Label61.Text = System.DateTime.Now.ToShortTimeString().Substring(0, 5);

                                                            ModalPopupExtender1223.Show();

                                                        }
                                                        else
                                                        {
                                                            timer1.Enabled = true;
                                                            ModalPopupExtender9.Show();

                                                            //lblmsg.Text = "Sorry,you are not allowed late entry for rules";
                                                        }
                                                    }

                                                }
                                                //else if (cmn == "-1")
                                                //{
                                                //    Endtime = tc.Subtract(new TimeSpan(Convert.ToInt32(ViewState["Etimeall"]), 0, 0));
                                                //    string sc = Endtime.CompareTo(TimeSpan.Parse(time22.Text)).ToString();
                                                //    if (sc != "1")
                                                //    //if (cmn != "1")
                                                //    {
                                                //        lblentry.Text = "";
                                                //        lblmsg.Text = "";
                                                //        TimerTime.Enabled = false;

                                                //        matchbarlogout();

                                                //    }
                                                //    else
                                                //    {
                                                //        lblmsg.Text = "Sorry,you are not allowed late entry for rules";
                                                //    }
                                                //}
                                                else if (Convert.ToDateTime(Label1date.Text) == Convert.ToDateTime(cd))
                                                {
                                                    ModalPopupExtender16.Hide();
                                                    lblmsg.Text = "";
                                                    TimerTime.Enabled = false;
                                                    lblentry.Text = "";
                                                    //   ModalPopupExtender1222.Show();
                                                    //  matchbarlogout();

                                                    SqlDataAdapter da = new SqlDataAdapter("select EmployeePayrollMaster.LastName + ' ' + EmployeePayrollMaster.FirstName as name,EmployeePayrollMaster.EmployeeNo from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID  where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeId"] + "'", cnn);
                                                    DataTable dt = new DataTable();
                                                    da.Fill(dt);

                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        Label59ss.Text = dt.Rows[0]["name"].ToString();

                                                        if (Convert.ToString(dt.Rows[0]["EmployeeNo"]) != "")
                                                        {
                                                            Label60.Text = dt.Rows[0]["EmployeeNo"].ToString();
                                                        }
                                                    }

                                                    Label60a.Text = System.DateTime.Now.ToShortDateString();
                                                    Label61.Text = System.DateTime.Now.ToShortTimeString().Substring(0, 5);

                                                    ModalPopupExtender1223.Show();

                                                }
                                                else
                                                {
                                                    timer1.Enabled = true;
                                                    ModalPopupExtender9.Show();

                                                    //lblmsg.Text = "Sorry,you are not allowed late entry for rules";

                                                    // TimerTime.Enabled = true;
                                                }
                                            }
                                            else
                                            {
                                                ModalPopupExtender16.Hide();
                                                lblmsg.Text = "";
                                                TimerTime.Enabled = false;
                                                lblentry.Text = "";
                                                //   ModalPopupExtender1222.Show();
                                                //  matchbarlogout();

                                                SqlDataAdapter da = new SqlDataAdapter("select EmployeePayrollMaster.LastName + ' ' + EmployeePayrollMaster.FirstName as name,EmployeePayrollMaster.EmployeeNo from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID  where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeId"] + "'", cnn);
                                                DataTable dt = new DataTable();
                                                da.Fill(dt);

                                                if (dt.Rows.Count > 0)
                                                {
                                                    Label59ss.Text = dt.Rows[0]["name"].ToString();

                                                    if (Convert.ToString(dt.Rows[0]["EmployeeNo"]) != "")
                                                    {
                                                        Label60.Text = dt.Rows[0]["EmployeeNo"].ToString();
                                                    }
                                                }

                                                Label60a.Text = System.DateTime.Now.ToShortDateString();
                                                Label61.Text = System.DateTime.Now.ToShortTimeString().Substring(0, 5);

                                                ModalPopupExtender1223.Show();
                                            }
                                        }

                                        // matchbarlogout();
                                    }
                                    else
                                    {


                                        string strr = "SELECT Top(1)  OutTimeforcalculation FROM AttendenceEntryMaster where EmployeeID='" + ds.Rows[0]["Employee_Id"] + "' order by AttendanceId Desc ";
                                        SqlDataAdapter adpll = new SqlDataAdapter(strr, cnn);
                                        DataTable dsll = new DataTable();
                                        adpll.Fill(dsll);
                                        if (dsll.Rows.Count > 0)
                                        {
                                            lbllastexittime.Text = dsll.Rows[0]["OutTimeforcalculation"].ToString();
                                        }
                                        lblempname.Text = ds.Rows[0]["EmployeeName"].ToString() + " : " + ds.Rows[0]["Employee_Id"]; ;
                                        Label7.Text = time22.Text;

                                        if (Convert.ToString(ViewState["Stime"]) != "")
                                        {
                                            if (Convert.ToString(ViewState["Stimeall"]) != "")
                                            {
                                                ttimestart = TimeSpan.Parse(ViewState["Stime"].ToString()).Subtract(new TimeSpan(Convert.ToInt32(ViewState["Stimeall"]), 0, 0));
                                                string cmn = tto.CompareTo(ttimestart).ToString();
                                                if (cmn == "1")
                                                {
                                                    SqlDataAdapter dacx = new SqlDataAdapter("SELECT OutTimeforcalculation,AttendanceId FROM AttendenceEntryMaster where EmployeeID='" + ds.Rows[0]["Employee_Id"] + "' and date='" + System.DateTime.Now.ToShortDateString() + "'", cnn);
                                                    DataTable dtcx = new DataTable();
                                                    dacx.Fill(dtcx);

                                                    if (dtcx.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dtcx.Rows[0]["OutTimeforcalculation"]) != "")
                                                        {
                                                            ViewState["mylast"] = Convert.ToString(dtcx.Rows[0]["OutTimeforcalculation"]);

                                                            Session["AttendanceId"] = Convert.ToString(dtcx.Rows[0]["AttendanceId"]);

                                                            ModalPopupExtender16.Hide();
                                                            lblmsg.Text = "";
                                                            TimerTime.Enabled = false;
                                                            lblentry.Text = "";
                                                            //matchbarlogout();

                                                            SqlDataAdapter da = new SqlDataAdapter("select EmployeePayrollMaster.LastName + ' ' + EmployeePayrollMaster.FirstName as name,EmployeePayrollMaster.EmployeeNo from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID  where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeId"] + "'", cnn);
                                                            DataTable dt = new DataTable();
                                                            da.Fill(dt);

                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                Label59ss.Text = dt.Rows[0]["name"].ToString();

                                                                if (Convert.ToString(dt.Rows[0]["EmployeeNo"]) != "")
                                                                {
                                                                    Label60.Text = dt.Rows[0]["EmployeeNo"].ToString();
                                                                }
                                                            }

                                                            Label60a.Text = System.DateTime.Now.ToShortDateString();
                                                            Label61.Text = System.DateTime.Now.ToShortTimeString().Substring(0, 5);

                                                            ModalPopupExtender1223.Show();
                                                        }
                                                        else
                                                        {
                                                            ModalPopupExtender16.Hide();
                                                            lblmsg.Text = "";
                                                            TimerTime.Enabled = false;
                                                            lblentry.Text = "";
                                                            //matchbarlogin();

                                                            SqlDataAdapter da1212 = new SqlDataAdapter("select EmployeePayrollMaster.LastName + ' ' + EmployeePayrollMaster.FirstName as name,EmployeePayrollMaster.EmployeeNo from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID  where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'", cnn);
                                                            DataTable dt1212 = new DataTable();
                                                            da1212.Fill(dt1212);

                                                            if (dt1212.Rows.Count > 0)
                                                            {
                                                                Label56a.Text = dt1212.Rows[0]["name"].ToString();

                                                                if (Convert.ToString(dt1212.Rows[0]["EmployeeNo"]) != "")
                                                                {
                                                                    Label57b.Text = dt1212.Rows[0]["EmployeeNo"].ToString();
                                                                }
                                                            }

                                                            Label58a.Text = System.DateTime.Now.ToShortDateString();
                                                            Label59.Text = System.DateTime.Now.ToShortTimeString().Substring(0, 5);

                                                            ModalPopupExtender13.Show();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ModalPopupExtender16.Hide();
                                                        lblmsg.Text = "";
                                                        TimerTime.Enabled = false;
                                                        lblentry.Text = "";
                                                        //matchbarlogin();

                                                        SqlDataAdapter da1212 = new SqlDataAdapter("select EmployeePayrollMaster.LastName + ' ' + EmployeePayrollMaster.FirstName as name,EmployeePayrollMaster.EmployeeNo from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID  where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'", cnn);
                                                        DataTable dt1212 = new DataTable();
                                                        da1212.Fill(dt1212);

                                                        if (dt1212.Rows.Count > 0)
                                                        {
                                                            Label56a.Text = dt1212.Rows[0]["name"].ToString();

                                                            if (Convert.ToString(dt1212.Rows[0]["EmployeeNo"]) != "")
                                                            {
                                                                Label57b.Text = dt1212.Rows[0]["EmployeeNo"].ToString();
                                                            }
                                                        }

                                                        Label58a.Text = System.DateTime.Now.ToShortDateString();
                                                        Label59.Text = System.DateTime.Now.ToShortTimeString().Substring(0, 5);

                                                        ModalPopupExtender13.Show();
                                                    }
                                                }
                                                else
                                                {
                                                    timer1.Enabled = true;

                                                    ModalPopupExtender10.Show();

                                                    //  lblmsg.Text = "Sorry,you are not allowed early entry for rules";
                                                    // TimerTime.Enabled = true;
                                                }
                                            }
                                            else
                                            {
                                                ModalPopupExtender16.Hide();
                                                lblmsg.Text = "";
                                                TimerTime.Enabled = false;
                                                lblentry.Text = "";

                                                //matchbarlogin();
                                                SqlDataAdapter da1212 = new SqlDataAdapter("select EmployeePayrollMaster.LastName + ' ' + EmployeePayrollMaster.FirstName as name,EmployeePayrollMaster.EmployeeNo from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID  where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'", cnn);
                                                DataTable dt1212 = new DataTable();
                                                da1212.Fill(dt1212);

                                                if (dt1212.Rows.Count > 0)
                                                {
                                                    Label56a.Text = dt1212.Rows[0]["name"].ToString();

                                                    if (Convert.ToString(dt1212.Rows[0]["EmployeeNo"]) != "")
                                                    {
                                                        Label57b.Text = dt1212.Rows[0]["EmployeeNo"].ToString();
                                                    }
                                                }

                                                Label58a.Text = System.DateTime.Now.ToShortDateString();
                                                Label59.Text = System.DateTime.Now.ToShortTimeString().Substring(0, 5);

                                                ModalPopupExtender13.Show();
                                            }
                                        }

                                    }

                                    ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                    string str3 = "insert into AttandanceLog(IpAddress,Barcode,Comid,Successfull,Datetime,Empid)values('" + ipaddress + "','" + txtuname.Text + "','" + ViewState["Compid"] + "','1','" + DateTime.Now.ToString() + "','" + ViewState["EmployeeID"] + "')";
                                    if (cnn.State.ToString() == "Open")
                                    {
                                        cnn.Close();
                                    }
                                    cnn.Open();
                                    SqlCommand cmd1 = new SqlCommand(str3, cnn);

                                    cmd1.ExecuteNonQuery();
                                    cnn.Close();
                                }
                                //timer1.Enabled = true;
                            }
                            else
                            {

                                if (flattt == 4)
                                {
                                    Label35.Text = ViewState["lastminit"].ToString();
                                    Label37.Text = ViewState["restmini"].ToString();

                                    timer1.Enabled = true;

                                    ModalPopupExtender8.Show();

                                    //   lblmsg.Text = "You are not able to check in / check out at this time.Your last entry time was at  <" + ViewState["lastminit"] + ">, you are blocked from making another entry for <" + ViewState["restmini"] + "> minuts.";
                                }
                                else
                                {
                                    //lblmsg.Text = "Sorry, you are only allowed a single entry/exit time for the day";

                                    SqlDataAdapter dacx11 = new SqlDataAdapter("SELECT OutTimeforcalculation FROM AttendenceEntryMaster where EmployeeID='" + ViewState["EmployeeID"] + "' and date='" + System.DateTime.Now.ToShortDateString() + "'", cnn);
                                    DataTable dtcx11 = new DataTable();
                                    dacx11.Fill(dtcx11);

                                    Label66.Text = Convert.ToString(dtcx11.Rows[0]["OutTimeforcalculation"]);

                                    timer1.Enabled = true;
                                    ModalPopupExtender14.Show();
                                }
                                //TimerTime.Enabled = true;
                            }
                        }
                        else
                        {


                        }
                    }
                    else
                    {

                        ModalPopupExtender3.Show();

                    }
                }
                else
                {
                    ModalPopupExtender16.Show();
                    lblmsg.Text = "Sorry, " + ipaddress + " Ip restricted";

                }



            }

            else
            {

                ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                if (cnn.State.ToString() == "Open")
                {
                    cnn.Close();
                }
                string str3 = "insert into AttandanceLog(IpAddress,Barcode,Comid,Successfull,Datetime)values('" + ipaddress + "','" + txtuname.Text + "','" + ViewState["Compid"] + "','0','" + DateTime.Now.ToString() + "')";
                cnn.Open();
                SqlCommand cmd1 = new SqlCommand(str3, cnn);
                //dr.Close();
                cmd1.ExecuteNonQuery();
                string str31 = "insert into AttandanceTempBanIpTable(IpAddress,Compid,Datetime)values('" + ipaddress + "','" + ViewState["Compid"] + "','" + DateTime.Now.ToString() + "')";
                //dr.Close();
                SqlCommand cmd11 = new SqlCommand(str31, cnn);
                //dr.Close();
                cmd11.ExecuteNonQuery();
                cnn.Close();

                // lblmsg.Text = "Sorry we can not find your userid and password in our system,</br> please try again with correct id card. If you are new employee,</br>check with your supervisor whether your userid and password is updated in the system.";


                timer1.Enabled = true;
                ModalPopupExtender12.Show();

                //lblmsg.Text = "Sorry, we cannot locate your user ID and password in our system.</br> Please try again.</br>If you are a new employee,please consult with your supervisor to ensure that your user ID and password have been updated in our system.";

                //lblmsg.Visible = true;

                //  TimerTime.Enabled = true;
            }
            txtcompanyid.Text = "";
            txtuname.Text = "";
            txtpass.Text = "";
            // txtbartext.Text = "";
            // txtbartext.Focus();

        }


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
        string grda = "select distinct AttandanceRule.*  from AttandanceRule where StoreId='" + ViewState["Whid"] + "' and generalwarningmail='1' and(attendencemail='1' or supermail='1' or attadminmail='1' or parentmail='1') ";
        //and op2graceperiod='2' 

        SqlCommand cmda = new SqlCommand(grda, cnn);
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
            SqlDataAdapter da = new SqlDataAdapter(str1, cnn);
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

                    string strmal = "  SELECT    MasterEmailId, OutGoingMailServer,WebMasterEmail, EmailMasterLoginPassword, AdminEmail, WHId " +
                                " FROM         CompanyWebsitMaster WHERE     (WHId = " + Convert.ToInt32(ViewState["Whid"]) + ") ";
                    SqlCommand cmdma = new SqlCommand(strmal, cnn);
                    SqlDataAdapter adpma = new SqlDataAdapter(cmdma);
                    DataTable dtma = new DataTable();
                    adpma.Fill(dtma);
                    if (dtma.Rows.Count > 0)
                    {
                        //////////

                        string AdminEmail = "";// TextAdminEmail.Text;
                        if (Convert.ToString(dtma.Rows[0]["WebMasterEmail"].ToString()) != "")
                        {
                            AdminEmail = dtma.Rows[0]["WebMasterEmail"].ToString();// TextAdminEmail.Text;
                        }
                        else
                        {
                            AdminEmail = dtma.Rows[0]["MasterEmailId"].ToString();// TextAdminEmail.Text;
                        }

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

                                empmanag += "<span style=\"color: #996600\"><b>" + lblm.Text + "</b></span><br>";
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

                                    empmanag += "<span style=\"color: #996600\"><b>" + lblm.Text + "</b></span><br>";
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

                                empmanag += "<span style=\"color: #996600\"><b>" + lblm.Text + "</b></span><br>";
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

    public void sendmail11()
    {
        string empname = "";
        string empemail = "";
        string managername = "";
        string empmanag = "";
        string emn = "";
        string body = "";
        string detail = "";

        string strde = " Select * from AttandanceRule where StoreId='" + ViewState["Whid"] + "'";
        SqlDataAdapter adpde = new SqlDataAdapter(strde, cnn);
        DataTable dtde = new DataTable();
        adpde.Fill(dtde);

        string strmal = "  SELECT    MasterEmailId, OutGoingMailServer,WebMasterEmail, EmailMasterLoginPassword, AdminEmail, WHId " +
                                 " FROM         CompanyWebsitMaster WHERE     (WHId = " + Convert.ToInt32(ViewState["Whid"]) + ") ";
        SqlCommand cmdma = new SqlCommand(strmal, cnn);
        SqlDataAdapter adpma = new SqlDataAdapter(cmdma);
        DataTable dtma = new DataTable();
        adpma.Fill(dtma);

        if (dtma.Rows.Count > 0)
        {

            string AdminEmail = "";// TextAdminEmail.Text;
            if (Convert.ToString(dtma.Rows[0]["WebMasterEmail"].ToString()) != "")
            {
                AdminEmail = dtma.Rows[0]["WebMasterEmail"].ToString();// TextAdminEmail.Text;
            }
            else
            {
                AdminEmail = dtma.Rows[0]["MasterEmailId"].ToString();// TextAdminEmail.Text;
            }
            String Password = dtma.Rows[0]["EmailMasterLoginPassword"].ToString();// TextEmailMasterLoginPassword.Text;
            MailAddress from = new MailAddress(AdminEmail);

            if (Convert.ToBoolean(dtde.Rows[0]["notificmailsuper"]) == true)
            {
                DataTable dtadty = select("select distinct SuprviserId,EmployeeName,inoutcompanyemail.EmailId from EmployeeMaster left join inoutcompanyemail on inoutcompanyemail.EmployeeID=EmployeeMaster.EmployeeMasterID where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'");

                if (dtadty.Rows.Count > 0)
                {
                    DataTable dtsuper = select("select distinct InOutCompanyEmail.EmailId, EmployeeMaster.EmployeeName,a.EmployeeName AS SUPER  from EmployeeMaster left join EmployeeMaster as a on a.EmployeeMasterId=EmployeeMaster.SuprviserId inner join InOutCompanyEmail on EmployeeMaster.EmployeeMasterID=InOutCompanyEmail.EmployeeID where EmployeeMaster.EmployeeMasterID='" + dtadty.Rows[0]["SuprviserId"] + "'");

                    if (dtsuper.Rows.Count > 0)
                    {
                        empname = Convert.ToString(dtadty.Rows[0]["EmployeeName"]);
                        empemail = Convert.ToString(dtsuper.Rows[0]["EmailId"]);
                        managername = Convert.ToString(dtsuper.Rows[0]["SUPER"]);

                        SqlDataAdapter dax1 = new SqlDataAdapter("select DesignationMaster.DesignationName,DepartmentmasterMNC.Departmentname from DesignationMaster inner join EmployeeMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptID where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'", cnn);
                        DataTable dtx1 = new DataTable();
                        dax1.Fill(dtx1);

                        //emn += "<b>" + Label1date.Text + "</b><br><br>";

                        emn += "<span style=\"color: #996600\">Dear </span><b>" + managername + "</b>,<br><br>";

                        empmanag += "<span style=\"color: #996600\"><br>Sincerely,<br><b>" + lblm.Text + "</b></span><br>";

                        detail = "<b>" + empname + " - " + dtx1.Rows[0]["DesignationName"].ToString() + " - " + dtx1.Rows[0]["Departmentname"].ToString() + "</b>";

                        if (Convert.ToString(ViewState["earlylatemin1"]) != "" && Convert.ToString(ViewState["MyInTime"]) != "")
                        {
                            detail += " arrived late to work by " + ViewState["earlylatemin1"] + " minutes.<br><br> ";
                        }
                        if (Convert.ToString(ViewState["earlylatemin2"]) != "")
                        {
                            detail += " arrived early to work by " + ViewState["earlylatemin2"] + " minutes.<br><br> ";
                        }

                        SqlDataAdapter dax2 = new SqlDataAdapter("select BatchMaster.Name,BatchTiming.StartTime,BatchTiming.EndTime from BatchMaster inner join BatchTiming on BatchTiming.BatchMasterId=BatchMaster.ID inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'", cnn);
                        DataTable dtx2 = new DataTable();
                        dax2.Fill(dtx2);

                        if (Convert.ToString(ViewState["MyStartTime"]) != "" && Convert.ToString(ViewState["MyInTime"]) != "")
                        {
                            if (Convert.ToString(ViewState["MyInNote"]) != "")
                            {
                                detail += "He has given following reason for deviation : <br><br>";

                                detail += "Reason : " + ViewState["MyInNote"].ToString();
                            }

                            detail += "The employee is in the batch : <b>" + dtx2.Rows[0]["Name"].ToString() + "</b> and the batch standard entry time is : " + dtx2.Rows[0]["StartTime"].ToString() + "<br>";


                            detail += "and the employee actual entry time is : <b>" + ViewState["MyInTime"] + "</b><br><br>";
                        }

                        if (Convert.ToString(ViewState["MyEndTime1"]) != "" && Convert.ToString(ViewState["MyOutTime"]) != "")
                        {
                            if (Convert.ToString(ViewState["MyOutNote"]) != "")
                            {
                                detail += "He has given following reason for deviation : <br><br>";

                                detail += "Reason : " + ViewState["MyOutNote"].ToString();
                            }

                            detail += "The employee is in the batch : <b>" + dtx2.Rows[0]["Name"].ToString() + "</b> and the batch standard exit time is : " + dtx2.Rows[0]["EndTime"].ToString() + "<br>";

                            detail += "and the employee actual exit time is : <b>" + ViewState["MyOutTime"] + "</b><br><br>";

                        }

                        DataTable dtadty11 = select("select distinct InOutCompanyEmail.EmailId, EmployeeMaster.EmployeeName,a.EmployeeName AS SUPER  from AttendenceEntryMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterId=AttendenceEntryMaster.EmployeeID  left join EmployeeMaster as a on a.SuprviserId=  EmployeeMaster.EmployeeMasterId inner join InOutCompanyEmail on EmployeeMaster.EmployeeMasterID=InOutCompanyEmail.EmployeeID where EmployeeMaster.EmployeeMasterID='" + dtde.Rows[0]["SeniorEmployeeID"] + "' and a.EmployeeMasterID='" + ViewState["EmployeeID"] + "'");

                        detail += "If you like, you can communicate with Attendance Admin (" + Convert.ToString(dtadty11.Rows[0]["EmployeeName"]) + " - " + Convert.ToString(dtadty11.Rows[0]["EmailId"]) + ")  regarding this deviation.<br><br>";

                        detail += "If you like , you can communciate with  " + empname + "  " + Convert.ToString(dtadty.Rows[0]["EmailId"]) + " in this regard.<br>";

                        body = emn + detail + empmanag;

                        MailAddress to = new MailAddress(empemail);
                        MailMessage objEmail = new MailMessage(from, to);

                        objEmail.Subject = "Notification of employee attendance deviation : " + empname;
                        objEmail.Body = body.ToString();
                        objEmail.IsBodyHtml = true;


                        objEmail.Priority = MailPriority.High;
                        SmtpClient client = new SmtpClient();

                        client.Credentials = new NetworkCredential(AdminEmail, Password);

                        client.Host = dtma.Rows[0]["OutGoingMailServer"].ToString();

                        client.Send(objEmail);

                    }
                }
            }

            if (Convert.ToBoolean(dtde.Rows[0]["notificmailattendanceadmin"]) == true)
            {
                empname = "";
                empemail = "";
                managername = "";
                empmanag = "";
                emn = "";
                body = "";
                detail = "";

                DataTable dtadty = select("select distinct InOutCompanyEmail.EmailId, EmployeeMaster.EmployeeName,a.EmployeeName AS SUPER  from AttendenceEntryMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterId=AttendenceEntryMaster.EmployeeID  left join EmployeeMaster as a on a.SuprviserId=  EmployeeMaster.EmployeeMasterId inner join InOutCompanyEmail on EmployeeMaster.EmployeeMasterID=InOutCompanyEmail.EmployeeID where EmployeeMaster.EmployeeMasterID='" + dtde.Rows[0]["SeniorEmployeeID"] + "' and a.EmployeeMasterID='" + ViewState["EmployeeID"] + "'");

                DataTable dtadtyazx11 = select("select distinct SuprviserId,EmployeeName,inoutcompanyemail.EmailId from EmployeeMaster left join inoutcompanyemail on inoutcompanyemail.EmployeeID=EmployeeMaster.EmployeeMasterID where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'");

                if (dtadty.Rows.Count > 0)
                {

                    empname = Convert.ToString(dtadty.Rows[0]["SUPER"]);
                    empemail = Convert.ToString(dtadty.Rows[0]["EmailId"]);
                    managername = Convert.ToString(dtadty.Rows[0]["EmployeeName"]);

                    SqlDataAdapter dax1 = new SqlDataAdapter("select DesignationMaster.DesignationName,DepartmentmasterMNC.Departmentname from DesignationMaster inner join EmployeeMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptID where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'", cnn);
                    DataTable dtx1 = new DataTable();
                    dax1.Fill(dtx1);

                    //emn += "<b>" + Label1date.Text + "</b><br><br>";

                    emn += "<span style=\"color: #996600\">Dear </span><b>" + managername + "</b>,<br><br>";

                    empmanag += "<span style=\"color: #996600\"><br>Sincerely,<br><b>" + lblm.Text + "</b></span><br>";

                    detail = "<b>" + empname + " - " + dtx1.Rows[0]["DesignationName"].ToString() + " - " + dtx1.Rows[0]["Departmentname"].ToString() + "</b>";

                    if (Convert.ToString(ViewState["earlylatemin1"]) != "" && Convert.ToString(ViewState["MyInTime"]) != "")
                    {
                        detail += " arrived late to work by " + ViewState["earlylatemin1"] + " minutes.<br><br> ";
                    }
                    if (Convert.ToString(ViewState["earlylatemin2"]) != "")
                    {
                        detail += " arrived early to work by " + ViewState["earlylatemin2"] + " minutes.<br><br> ";
                    }

                    SqlDataAdapter dax2 = new SqlDataAdapter("select BatchMaster.Name,BatchTiming.StartTime,BatchTiming.EndTime from BatchMaster inner join BatchTiming on BatchTiming.BatchMasterId=BatchMaster.ID inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + ViewState["EmployeeID"] + "'", cnn);
                    DataTable dtx2 = new DataTable();
                    dax2.Fill(dtx2);


                    if (Convert.ToString(ViewState["MyStartTime"]) != "" && Convert.ToString(ViewState["MyInTime"]) != "")
                    {
                        if (Convert.ToString(ViewState["MyInNote"]) != "")
                        {
                            detail += "He has given following reason for deviation : <br><br>";

                            detail += "Reason : " + ViewState["MyInNote"].ToString();
                        }

                        detail += "The employee is in the batch : <b>" + dtx2.Rows[0]["Name"].ToString() + "</b> and the batch standard entry time is : " + dtx2.Rows[0]["StartTime"].ToString() + "<br>";


                        detail += "and the employee actual entry time is : <b>" + ViewState["MyInTime"] + "</b><br><br>";
                    }

                    if (Convert.ToString(ViewState["MyEndTime1"]) != "" && Convert.ToString(ViewState["MyOutTime"]) != "")
                    {
                        if (Convert.ToString(ViewState["MyOutNote"]) != "")
                        {
                            detail += "He has given following reason for deviation : <br><br>";

                            detail += "Reason : " + ViewState["MyOutNote"].ToString();
                        }

                        detail += "The employee is in the batch : <b>" + dtx2.Rows[0]["Name"].ToString() + "</b> and the batch standard exit time is : " + dtx2.Rows[0]["EndTime"].ToString() + "<br>";

                        detail += "and the employee actual exit time is : <b>" + ViewState["MyOutTime"] + "</b><br><br>";

                    }

                    DataTable dtadty11 = select("select distinct InOutCompanyEmail.EmailId, EmployeeMaster.EmployeeName,a.EmployeeName AS SUPER  from AttendenceEntryMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterId=AttendenceEntryMaster.EmployeeID  left join EmployeeMaster as a on a.SuprviserId=  EmployeeMaster.EmployeeMasterId inner join InOutCompanyEmail on EmployeeMaster.EmployeeMasterID=InOutCompanyEmail.EmployeeID where EmployeeMaster.EmployeeMasterID='" + dtde.Rows[0]["SeniorEmployeeID"] + "' and a.EmployeeMasterID='" + ViewState["EmployeeID"] + "'");

                    detail += "If you like, you can communicate with Attendance Admin (" + Convert.ToString(dtadty11.Rows[0]["EmployeeName"]) + " - " + Convert.ToString(dtadty11.Rows[0]["EmailId"]) + ") regarding this deviation.<br><br>";

                    detail += "If you like , you can communciate with  " + empname + "  " + Convert.ToString(dtadtyazx11.Rows[0]["EmailId"]) + " in this regard.<br>";

                    body = emn + detail + empmanag;

                    MailAddress to = new MailAddress(empemail);
                    MailMessage objEmail = new MailMessage(from, to);

                    objEmail.Subject = "Notification of employee attendance deviation : " + empname;
                    objEmail.Body = body.ToString();
                    objEmail.IsBodyHtml = true;


                    objEmail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();

                    client.Credentials = new NetworkCredential(AdminEmail, Password);

                    client.Host = dtma.Rows[0]["OutGoingMailServer"].ToString();

                    client.Send(objEmail);

                }

            }
        }

        ViewState["MyStartTime"] = "";
        ViewState["MyInTime"] = "";
        ViewState["MyEndTime1"] = "";
        ViewState["MyOutTime"] = "";
        ViewState["earlylatemin1"] = "";
        ViewState["earlylatemin2"] = "";
    }

    protected void Button4a_Click(object sender, EventArgs e)
    {
        matchbarlogin();
    }
    protected void Button4b_Click(object sender, EventArgs e)
    {
        ModalPopupExtender13.Hide();
    }
    protected void btncan12_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1223.Hide();
    }
    protected void Button44_Click(object sender, EventArgs e)
    {
        matchbarlogout();
    }
}
