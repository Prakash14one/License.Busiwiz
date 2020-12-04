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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
public partial class Admin_Communicationreport : System.Web.UI.Page
{
    SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    string today,strthismonth,strtodaycomm,strthisyear,yesterday,stryesterdaycomm,lastweek,strlastweekcomm,lastmonth, strlastmonthcomm, lastquerter, strlastquertercomm, strlastyearcomm, lastyear;
    //Get first & last day of the month
    DateTime monthfrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month,1);
    DateTime monthto = new DateTime(DateTime.Now.Year,DateTime.Now.Month,30);
    //Get first & last day of the week
    DateTime weekfrom = DateTime.Now.AddDays(1-DateTime.Now.DayOfWeek.GetHashCode());
    DateTime weekto = DateTime.Now.AddDays(7-DateTime.Now.DayOfWeek.GetHashCode());
   // Get first & last day of the year 
    DateTime yearfrom = Convert.ToDateTime("1/1/" + DateTime.Now.Year.ToString());
    DateTime yearto = Convert.ToDateTime("12/31/" + DateTime.Now.Year.ToString());
    //DateTime datetime = System.DateTime.Now;
    //int lastquerter=(DateTime.Now.Month - 1)/ 3 + 1;
    //DateTime dtFirstDay = new DateTime(DateTime.Now.Year, lastquerter-2),1);
    //DateTime dtlastDay = new DateTime(DateTime.Now.Year, 3* lastquerter +1);
  // DateTime dtLastDay = new DateTime(datetime.Now.Year, 3 * currQuarter + 1, 1).AddDays(-1);
    // Get first & last day of the Quarter
   ////int thisqtr = Convert.ToInt32(DateTime.Now.AddMonths(-2).Month.ToString());
    ////DateTime thisquaterstart = Convert.ToDateTime(thisqtr+ "/1/" + DateTime.Now.Year.ToString());
   ////DateTime thisquaterend = thi + "/31/" + ThisYear.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        today = System.DateTime.Now.ToShortDateString();
        yesterday = DateTime.Now.AddDays(-1).ToShortDateString();
        lastweek = DateTime.Now.AddDays(-7).ToShortDateString();
        lastmonth = DateTime.Now.AddMonths(-1).ToShortDateString();
        lastquerter = DateTime.Now.AddMonths(-3).ToShortDateString();
        lastyear = DateTime.Now.AddYears(-1).ToShortDateString();           
        if (!IsPostBack)
        {
            txtFromdate.Text = DateTime.Now.ToShortDateString();
            txtTodate.Text = DateTime.Now.ToShortDateString();
            fillddlpartytype();              
            fillreport();
            paneldate.Visible = false;
            panelperiod.Visible = false;
            panelparty.Visible = false;
            panelremainderdate.Visible = false;
            Panel1.Visible = false;
            Panel2.Visible = false;
                   }
        else
        {
            CrystalReportViewer1.ReportSource = Session["rpt"];
        }
    }
    protected void fillddlpartytype()
    {
       // string str = "select PartyTypeId,PartyTypeName from PartyTypeMaster";
        string str = "select PartytTypeMaster.* from PartytTypeMaster";
        SqlDataAdapter adp = new SqlDataAdapter(str,cnn);
        DataSet ds = new DataSet();
        cnn.Open();
        adp.Fill(ds);
        ddlpartytype.DataSource = ds;
        ddlpartytype.DataValueField = "PartyTypeId";
        ddlpartytype.DataTextField = "PartType";
        ddlpartytype.DataBind();
        cnn.Close();
        ddlpartytype.Items.Insert(0, "--Select--");
        ddlpartytype.Items.Insert(1,"All");
    }
    protected void fillddlpartyname()
    {
        string str;
        if (ddlpartytype.SelectedItem.Text == "All")
        {
            str = "SELECT PartyTypeMaster.PartyTypeId,PartyTypeMaster.PartType + ':' + Party_master.Contactperson AS ds, Party_master.PartyID " +
            "FROM PartyTypeMaster INNER JOIN " + "Party_master ON PartyTypeMaster.PartyTypeId = Party_master.PartyTypeId Order By PartyTypeMaster.PartType,Party_master.Contactperson";
        }
        else
        {
            str = "SELECT PartyTypeMaster.PartyTypeId,PartyTypeMaster.PartType + ':' + Party_master.Contactperson AS ds, Party_master.PartyID " + "FROM PartyTypeMaster INNER JOIN " + "Party_master ON PartyTypeMaster.PartyTypeId = Party_master.PartyTypeId  WHERE PartyTypeMaster.PartType='" + ddlpartytype.SelectedItem + "' Order By PartyTypeMaster.PartType,Party_master.Contactperson";
        }
        SqlCommand cmd = new SqlCommand(str,cnn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlpartyname.Items.Clear();
            ddlpartyname.DataSource = dt;
            ddlpartyname.DataTextField = "ds";
            ddlpartyname.DataValueField = "PartyID";
            ddlpartyname.DataBind();
            ddlpartyname.Items.Insert(0, "--Select--");
            ddlpartyname.Items.Insert(1,"ALL");
            ddlpartyname.SelectedItem.Value = "0";
        }        
    }
    private void fillreport()
    {
        string str = "SELECT * from Communicationreport ";
        SqlDataAdapter adp = new SqlDataAdapter(str,cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa","BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    protected void rdcrselection_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillreport();
        if (rdcrselection.SelectedItem.Text == "Date Wise")
        {
            paneldate.Visible = true;
            panelperiod.Visible = false;
            panelparty.Visible = false;
            panelremainderdate.Visible = false;
        }
        if (rdcrselection.SelectedItem.Text == "Reminder Date Wise")
        {
            paneldate.Visible = false;
            panelremainderdate.Visible = true;
            panelperiod.Visible =false;            
            panelparty.Visible = false;
        }
        if (rdcrselection.SelectedItem.Text == "Period Wise")
        {
            panelremainderdate.Visible = false;
            panelperiod.Visible = true;
            paneldate.Visible = false;
            panelparty.Visible = false;
        }
        if (rdcrselection.SelectedItem.Text == "Party Type /Party Name Wise")
        {
            panelparty.Visible = true;
            panelremainderdate.Visible = false;
            paneldate.Visible = false;
            panelperiod.Visible = false;
        }
    }
    protected void btnviewdate_Click(object sender, EventArgs e)
    {
        if (ddlcommtype.SelectedItem.Text == "Incoming/Outgoing Phone")
        {
            inoutphone();
        }
        else if (ddlcommtype.SelectedItem.Text == "Incoming/Outgoing Visit")
        {
            inoutvisit();
        }
        else if (ddlcommtype.SelectedItem.Text == "Incoming/Outgoing Fax")
        {
            inoutfax();
        }
        else if (ddlcommtype.SelectedItem.Text == "Incoming/Outgoing Email")
        {
            inoutemail();
        }
        else if (ddlcommtype.SelectedItem.Text == "Incoming/Outgoing Postmail")
        {
            inoutpostmail();
        }
        else if (ddlcommtype.SelectedItem.Text == "Message")
        {
            message();
        }
        else if (ddlcommtype.SelectedItem.Text == "Test")
        {
            test();
        }
        else if (ddlcommtype.SelectedItem.Text == "All")
        {
            all();
        }
        
    }
    public void inoutphone()
    {
        //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName, c.Phoneno, c.Description, c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId
        //WHERE (c.date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  )";
        string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  ) ";
        SqlDataAdapter adp = new SqlDataAdapter(str,cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }   
    public void inoutvisit()
    {
        //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno, c.Description, c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
        //WHERE (c.date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingVisit' or Smallmesstype='OutgoingVisit'))";
        string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingVisit' or Smallmesstype='OutgoingVisit')  ) ";
        SqlDataAdapter adp = new SqlDataAdapter(str, cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void inoutfax()
    {
        //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno, c.Description, c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId WHERE (c.date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingFax' or Smallmesstype='OutgoingFax'))";
        string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingFax' or Smallmesstype='OutgoingFax')  ) ";
        SqlDataAdapter adp = new SqlDataAdapter(str,cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void inoutemail()
    {
        //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno, c.Description, c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId
        //WHERE (c.date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingEmail' or Smallmesstype='OutgoingEmail'))";
        string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingEmail' or Smallmesstype='OutgoingEmail')  ) ";
        SqlDataAdapter adp = new SqlDataAdapter(str, cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void inoutpostmail()
    {
        //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno, c.Description, c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
        //WHERE (c.date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='Incoming Postmail' or Smallmesstype='Outgoing Postmail'))";
        string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='Incoming Postmail' or Smallmesstype='Outgoing Postmail')  ) ";
        SqlDataAdapter adp = new SqlDataAdapter(str, cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void message()
    {
        //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno, c.Description, c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
        //WHERE (c.date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='message'))";
        string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='message')  ) ";
        SqlDataAdapter adp = new SqlDataAdapter(str, cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void test()
    {
        //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno, c.Description, c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId WHERE (c.date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='test'))";
        string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='test')  ) ";
        SqlDataAdapter adp = new SqlDataAdapter(str,cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void all()
    {
        //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno, c.Description, c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId WHERE (c.date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "')";
        string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "'  ";
        SqlDataAdapter adp = new SqlDataAdapter(str,cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    ///---------------------------------------------------
    protected void btnviewparty_Click(object sender, EventArgs e)
    {
        string str ="SELECT * from Communicationreport WHERE PartyId = '" + ddlpartyname.SelectedValue + "'";
        SqlDataAdapter adp = new SqlDataAdapter(str,cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa","BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }   
    public void todaycomm()
    {
        //strtodaycomm = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
        //and(c.Date = '" + Convert.ToDateTime(today).ToShortDateString() + "')order by c.Date asc";          
        strtodaycomm = " SELECT * from Communicationreport where WHERE (Date = '" + Convert.ToDateTime(today).ToShortDateString() + "') ";
        SqlDataAdapter adp = new SqlDataAdapter(strtodaycomm,cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);       
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa","BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void yesterdaycomm()
    {
        //stryesterdaycomm = "SELECT c.Date, c.ReminderDate, p.PartyTypeName,m.PartyName, e.EmployeeName, c.Phoneno, c.Description, c.CommID, SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId AND c.date  ='" + Convert.ToDateTime(yesterday).ToShortDateString() + "' order by c.Date asc";
        stryesterdaycomm = " SELECT * from Communicationreport where WHERE (Date = '" + Convert.ToDateTime(yesterday).ToShortDateString() + "') ";
        SqlDataAdapter adp = new SqlDataAdapter(stryesterdaycomm, cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void lastweekcomm()
    {
        //string strlastweekcomm = "SELECT c.Date, c.ReminderDate, p.PartyTypeName,m.PartyName, e.EmployeeName, c.Phoneno, c.Description, c.CommID, SmallMessageType.Smallmesstype"
        // + " FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN"
        // + " CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN"
        // + " CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN"
        // + " EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN"
        // + " SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId"
        // + " AND (c.date between '" + Convert.ToDateTime(lastweek).ToShortDateString() + "' and'" + Convert.ToDateTime(today).ToShortDateString() + "')";
        string strlastweekcomm = " SELECT * from Communicationreport where WHERE (Date between '" + Convert.ToDateTime(lastweek).ToShortDateString() + "' and'" + Convert.ToDateTime(today).ToShortDateString() + "')";
        SqlDataAdapter adp = new SqlDataAdapter(strlastweekcomm,cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void lastmonthcomm()
    {
        //strlastmonthcomm = "SELECT c.Date, c.ReminderDate, p.PartyTypeName, m.PartyName, e.EmployeeName, c.Phoneno, c.Description, c.CommID, SmallMessageType.Smallmesstype"
        //+ " FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN"
        //+ " CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN"
        //+ " CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN"
        //+ " EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN"
        //+ " SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId"
        //+ " AND (c.Date BETWEEN '" + Convert.ToDateTime(lastmonth).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "')order by c.Date asc";
        strlastmonthcomm = " SELECT * from Communicationreport where WHERE (Date between '" + Convert.ToDateTime(lastmonth).ToShortDateString() + "' and'" + Convert.ToDateTime(today).ToShortDateString() + "')";
        SqlDataAdapter adp = new SqlDataAdapter(strlastmonthcomm, cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void lastquartercomm()
    {
        // strlastquertercomm = "SELECT c.Date, c.ReminderDate, p.PartyTypeName, m.PartyName, e.EmployeeName, c.Phoneno, c.Description, c.CommID, SmallMessageType.Smallmesstype"
        //+ " FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN"
        //+ " CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN"
        //+ " CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN"
        //+ " EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN"
        //+ " SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId"
        //+ " AND (c.Date BETWEEN '" + Convert.ToDateTime(lastquerter).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "') order by c.Date asc";
        strlastquertercomm = " SELECT * from Communicationreport where WHERE (Date between '" + Convert.ToDateTime(lastquerter).ToShortDateString() + "' and'" + Convert.ToDateTime(today).ToShortDateString() + "')";
        SqlDataAdapter adp = new SqlDataAdapter(strlastquertercomm,cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void lastyearcomm()
    {
        //strlastyearcomm = "SELECT c.CapmanPartyTypeId,c.Date,c.ReminderDate,c.CommID,p.PartyTypeName, p.PartyTypeId, c.CommFor, c.CommWith, m.PartyName, m.PartyId, e.EmployeeID,e.EmployeeName, c.Phoneno, c.Description, TMS.dbo.SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN TMS.dbo.CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN TMS.dbo.EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN TMS.dbo.SmallMessageType ON c.CommTypeId = TMS.dbo.SmallMessageType.SmallmesstypeId AND c.Date BETWEEN '" + Convert.ToDateTime(lastyear).ToShortDateString() + "' AND '" + Convert.ToDateTime(today).ToShortDateString() + "'";
        strlastyearcomm = " SELECT * from Communicationreport where WHERE (Date between '" + Convert.ToDateTime(lastyear).ToShortDateString() + "' and'" + Convert.ToDateTime(today).ToShortDateString() + "')";
        SqlDataAdapter adp = new SqlDataAdapter(strlastyearcomm,cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa","BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void thisweekcomm()
    {
       // string strthisweek = "SELECT c.Date, c.ReminderDate, p.PartyTypeName, m.PartyName, e.EmployeeName, c.Phoneno, c.Description, c.CommID, SmallMessageType.Smallmesstype"
       //+ " FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN"
       //+ " CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN"
       //+ " CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN"
       //+ " EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN"
       //+ " SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId"
       //+ " AND (c.Date BETWEEN '" + Convert.ToDateTime(weekfrom).ToShortDateString() + "' and  '" + Convert.ToDateTime(weekto).ToShortDateString() + "')";
        string strthisweek = " SELECT * from Communicationreport where WHERE (Date between '" + Convert.ToDateTime(weekfrom).ToShortDateString() + "' and'" + Convert.ToDateTime(weekto).ToShortDateString() + "')";
        SqlDataAdapter adp = new SqlDataAdapter(strthisweek, cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void thismonthcomm()
    {
       // strthismonth = "SELECT c.Date, c.ReminderDate, p.PartyTypeName, m.PartyName, e.EmployeeName, c.Phoneno, c.Description, c.CommID, SmallMessageType.Smallmesstype"
       //+ " FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN"
       //+ " CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN"
       //+ " CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN"
       //+ " EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN"
       //+ " SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId"
       //+ " AND (c.Date BETWEEN '" + Convert.ToDateTime(monthfrom).ToShortDateString() + "' and  '" + Convert.ToDateTime(monthto).ToShortDateString() + "')";
        strthismonth = " SELECT * from Communicationreport where WHERE (Date between '" + Convert.ToDateTime(monthfrom).ToShortDateString() + "' and'" + Convert.ToDateTime(monthto).ToShortDateString() + "')";
        SqlDataAdapter adp = new SqlDataAdapter(strthismonth, cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    public void thisyearcomm()
    {
       // string strthisyear = "SELECT c.Date, c.ReminderDate, p.PartyTypeName, m.PartyName, e.EmployeeName, c.Phoneno, c.Description, c.CommID, SmallMessageType.Smallmesstype"
       //+ " FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN"
       //+ " CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN"
       //+ " CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN"
       //+ " EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN"
       //+ " SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId"
       //+ " AND (c.Date BETWEEN '" + Convert.ToDateTime(yearfrom).ToShortDateString() + "' and  '" + Convert.ToDateTime(yearto).ToShortDateString() + "')";
        string strthisyear = " SELECT * from Communicationreport where WHERE (Date between '" + Convert.ToDateTime(yearfrom).ToShortDateString() + "' and'" + Convert.ToDateTime(yearto).ToShortDateString() + "')";
        SqlDataAdapter adp = new SqlDataAdapter(strthisyear,cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    //public void thisquartercomm()
    //{
    //    string strthisquarter = "SELECT c.Date, c.ReminderDate, p.PartyTypeName, m.PartyName, e.EmployeeName, c.Phoneno, c.Description, c.CommID, SmallMessageType.Smallmesstype"
    //   + " FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN"
    //   + " CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN"
    //   + " CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN"
    //   + " EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN"
    //   + " SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId"
    //   + " AND (c.Date BETWEEN '" + Convert.ToDateTime(yearfrom).ToShortDateString() + "' and  '" + Convert.ToDateTime(yearto).ToShortDateString() + "')";
    //    SqlDataAdapter adp = new SqlDataAdapter(strthisquarter,cn);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    CrystalReportViewer1.Visible = true;
    //    ReportDocument myRpt = new ReportDocument();
    //    myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
    //    myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
    //    myRpt.SetDataSource(dt);
    //    CrystalReportViewer1.ReportSource = myRpt;
    //    Session["rpt"] = myRpt;
    //}   
    //public void thisquartercomm()
    //{
    //    int thisqtr = Convert.ToInt32(thismonthstart.AddMonths(-2).Month.ToString());
    //    string thisqtrNumber = Convert.ToString(thisqtr.ToString());
    //    DateTime thisquater = Convert.ToDateTime(thisqtrNumber.ToString() + "/1/" +DateTime.Now.Year.ToString());
    //    string thisquaterstart = thisquater.ToShortDateString();
    //    string thisquaterend = "";

    //    if (thisqtrNumber == "1" || thisqtrNumber == "3" || thisqtrNumber == "5" || thisqtrNumber == "7" || thisqtrNumber == "9" || thisqtrNumber == "11")
    //    {
    //        thisquaterend = thisqtrNumber + "/31/" + DateTime.Now.Year.ToString();
    //    }
    //    else if (thisqtrNumber == "4" || thisqtrNumber == "6" || thisqtrNumber == "8" || thisqtrNumber == "10" || thisqtrNumber == "12")
    //    {
    //        thisquaterend = thisqtrNumber + "/30/" + DateTime.Now.Year.ToString();
    //    }
    //    else
    //    {
    //        if (System.DateTime.IsLeapYear(Convert.ToInt32(DateTime.Now.Year.ToString())))
    //        {
    //            thisquaterend = thisqtrNumber + "/29/" + DateTime.Now.Year.ToString();
    //        }
    //        else
    //        {



    //            thisquaterend = thisqtrNumber + "/28/" + DateTime.Now.Year.ToString();
    //        }
    //    }

    //    string thisquaterstartdate = thisquaterstart.ToString();
    //    string thisquaterenddate = thisquaterend.ToString();
    //}

    protected void btnviewremainderdate_Click(object sender, EventArgs e)
    {
        //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description, c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId WHERE (c.date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "')";
        string str = " SELECT * from Communicationreport where WHERE (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "'  ";
        SqlDataAdapter adp = new SqlDataAdapter(str, cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
    protected void btnviewperiod_Click(object sender, EventArgs e)
    {
        if (ddlperiodwise.SelectedItem.Text == "Today")
        {
            todaycomm();
        }
        else if (ddlperiodwise.SelectedItem.Text == "Yesterday")
        {
            yesterdaycomm();
        }
        else if (ddlperiodwise.SelectedItem.Text == "This Week")
        {
            thisweekcomm();
        }
        else if (ddlperiodwise.SelectedItem.Text == "This Month")
        {
            thismonthcomm();
        }
        //else if (ddlperiodwise.SelectedItem.Text == "This Querter")
        //{
        //    thisquartercomm();
        //}
        else if (ddlperiodwise.SelectedItem.Text == "This Year")
        {
            thisyearcomm();
        }
        else if (ddlperiodwise.SelectedItem.Text == "Last Week")
        {
            lastweekcomm();
        }
        else if (ddlperiodwise.SelectedItem.Text == "Last Month")
        {
            lastmonthcomm();
        }
        else if (ddlperiodwise.SelectedItem.Text == "Last Quarter")
        {
            lastquartercomm();
        }
        else if (ddlperiodwise.SelectedItem.Text == "Last Year")
        {
            lastyearcomm();
        }
        else if (ddlperiodwise.SelectedItem.Text == "All")
        {
            fillreport();
        }      
    }
    protected void btnviewremainder_Click(object sender, EventArgs e)
    {
       // string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description, c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
        //WHERE (c.ReminderDate between'" + Convert.ToDateTime(txtFromdate1.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate1.Text).ToShortDateString() + "' ) order by c.ReminderDate";
        string str = " SELECT * from Communicationreport where  (ReminderDate between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' ";
        SqlDataAdapter adp = new SqlDataAdapter(str, cn);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        CrystalReportViewer1.Visible = true;
        ReportDocument myRpt = new ReportDocument();
        myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
        myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
        myRpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = myRpt;
        Session["rpt"] = myRpt;
    }
      protected void ddlpartytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlpartyname();  
         if (ddlperiodwise.SelectedItem.Text == "All")
        {
            all1();
        }    
    }
      protected void rdcrselection1_SelectedIndexChanged1(object sender, EventArgs e)
      {
          if (rdcrselection1.SelectedItem.Text == "Date Wise")
          {
              Panel1.Visible = true;
              Panel2.Visible = false;
          }
          if (rdcrselection1.SelectedItem.Text == "Remainder Date Wise")
          {
              Panel1.Visible = false;
              Panel2.Visible = true;
          }
      }
      protected void btnviewpartyr_Click(object sender, EventArgs e)
      {
          if (ddlcommtype1.SelectedItem.Text == "Incoming/Outgoing Fax")
          {
              inoutfax1();
          }      
          else if (ddlcommtype1.SelectedItem.Text == "Incoming/Outgoing Email")
          {
              inoutemail1();
          }
          else if (ddlcommtype1.SelectedItem.Text == "Incoming/Outgoing Post mail")
          {
              inoutpostmail1();
          }
          else if (ddlcommtype1.SelectedItem.Text == "Incoming/Outgoing Phone")
          {
              inoutphone1();
          }
          else if (ddlcommtype1.SelectedItem.Text == "All")
          {
              all1();
          }
      }
      public void inoutfax1()
      {
          if (ddlpartytype.SelectedItem.Text != "All" )
          {
              string str;
              string str1 = ddlpartyname.SelectedItem.Text;
              string[] str2 = str1.Split(':');
              string str3 = str2[0].ToString();
              string str4 = str2[1].ToString();
              if (Panel1.Visible == true)
              {
                  //str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
                  //WHERE (c.date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingFax' or Smallmesstype='OutgoingFax')  and p.PartyTypeName='" + str3 + "'  AND   m.PartyName='" + str4 + "' order by c.Date";
                  str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingFax' or Smallmesstype='OutgoingFax')  ) and PartyTypeName='" + str3 + "'  AND   PartyName='" + str4 + "' ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }
              else if (Panel2.Visible == true)
              {
                 // str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
                  //WHERE (c.ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingFax' or Smallmesstype='OutgoingFax')  and p.PartyTypeName='" + str3 + "'  AND   m.PartyName='" + str4 + "' order by c.ReminderDate";
                  str = " SELECT * from Communicationreport where  (ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingFax' or Smallmesstype='OutgoingFax')  ) and PartyTypeName='" + str3 + "'  AND   PartyName='" + str4 + "' ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }
          }
          else
          {
              if (Panel1.Visible == true)
              {
                  // string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
                  //WHERE (c.date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingFax' or Smallmesstype='OutgoingFax') order by c.Date";
                  string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingFax' or Smallmesstype='OutgoingFax')  ) ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }
              else if (Panel2.Visible == true)
              {
                  //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
                  //WHERE (c.ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingFax' or Smallmesstype='OutgoingFax')   order by c.ReminderDate";
                  string str = " SELECT * from Communicationreport where  (ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingFax' or Smallmesstype='OutgoingFax')  ) ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }             
          }
      }
      public void inoutemail1()
      {
          if (ddlpartytype.SelectedItem.Text != "All")
          {
              string str;
              String str1 = ddlpartyname.SelectedItem.Text;
              string[] str2 = str1.Split(':');
              string str3 = str2[0].ToString();
              string str4 = str2[1].ToString();
              if (Panel1.Visible == true)
              {
                  //str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
                  //WHERE (c.date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingEmail' or Smallmesstype='OutgoingEmail')  and p.PartyTypeName='" + str3 + "'  AND   m.PartyName='" + str4 + "' order by c.Date";
                  str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingEmail' or Smallmesstype='OutgoingEmail')  ) and PartyTypeName='" + str3 + "'  AND   PartyName='" + str4 + "' ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }
              else if (Panel2.Visible == true)
              {
                  //str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId
                  //WHERE (c.ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingEmail' or Smallmesstype='OutgoingEmail')  and p.PartyTypeName='" + str3 + "' AND m.PartyName='" + str4 + "' order by c.ReminderDate";
                  str = " SELECT * from Communicationreport where  (ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  ) and PartyTypeName='" + str3 + "'  AND   PartyName='" + str4 + "' ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }
          }
          else
          {
              if (Panel1.Visible == true)
              {
                  //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
                  //WHERE (c.date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingEmail' or Smallmesstype='OutgoingEmail') order by c.Date";
                  string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingEmail' or Smallmesstype='OutgoingEmail')  ) ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }
              else if (Panel2.Visible == true)
              {
                  //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId
                  //WHERE (c.ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingEmail' or Smallmesstype='OutgoingEmail')   order by c.ReminderDate";
                  string str = " SELECT * from Communicationreport where  (ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingEmail' or Smallmesstype='OutgoingEmail')  ) ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }
          }

      }      
     public void inoutpostmail1()
      {

          if (ddlpartytype.SelectedItem.Text != "All" || ddlpartytype.SelectedItem.Text != "--Select--")
          {

              string str;
              String str1 = ddlpartyname.SelectedItem.Text;
              string[] str2 = str1.Split(':');
              string str3 = str2[0].ToString();
              string str4 = str2[1].ToString();
              if (Panel1.Visible == true)
              {
                  //str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
                  //WHERE (c.Date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "') AND (Smallmesstype='Incoming Postmail' or Smallmesstype='Outgoing Postmail')  and p.PartyTypeName='" + str3 + "'  AND   m.PartyName='" + str4 + "' order by c.Date";
                  str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "' AND (Smallmesstype='Incoming Postmail' or Smallmesstype='Outgoing Postmail')  ) ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }
              else if (Panel2.Visible == true)
              {
                  //str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId WHERE (c.ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "') AND (Smallmesstype='Incoming Postmail' or Smallmesstype='Outgoing Postmail')  and p.PartyTypeName='" + str3 + "'  AND   m.PartyName='" + str4 + "' order by c.ReminderDate";
                  str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  ) ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }
          }
          else
          {
              if (Panel1.Visible == true)
              {
                  //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId WHERE (c.date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "') AND (Smallmesstype='Incoming Postmail' or Smallmesstype='Outgoing Postmail') order by c.Date";
                  string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  ) ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }
              else if (Panel2.Visible == true)
              {
                  //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId WHERE (c.ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "') AND (Smallmesstype='Incoming Postmail' or Smallmesstype='Outgoing Postmail')   order by c.ReminderDate";
                  string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  ) ";
                  SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);
                  CrystalReportViewer1.Visible = true;
                  ReportDocument myRpt = new ReportDocument();
                  myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                  myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                  myRpt.SetDataSource(dt);
                  CrystalReportViewer1.ReportSource = myRpt;
                  Session["rpt"] = myRpt;
              }
         
          }           
      }
     public void inoutphone1()
     {

         if (ddlpartytype.SelectedItem.Text != "All" || ddlpartytype.SelectedItem.Text != "--Select--")
         {

             string str;
             String str1 = ddlpartyname.SelectedItem.Text;
             string[] str2 = str1.Split(':');
             string str3 = str2[0].ToString();
             string str4 = str2[1].ToString();
             if (Panel1.Visible == true)
             {
                 //str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId
                 //WHERE (c.Date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  and p.PartyTypeName='" + str3 + "'AND m.PartyName='" + str4 + "' order by c.Date";
                 str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  )  and PartyTypeName='" + str3 + "'  AND   PartyName='" + str4 + "' ";
                 SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                 DataTable dt = new DataTable();
                 adp.Fill(dt);
                 CrystalReportViewer1.Visible = true;
                 ReportDocument myRpt = new ReportDocument();
                 myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                 myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                 myRpt.SetDataSource(dt);
                 CrystalReportViewer1.ReportSource = myRpt;
                 Session["rpt"] = myRpt;
             }
             else if (Panel2.Visible == true)
             {
                 //str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
                 //WHERE (c.ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  and p.PartyTypeName='" + str3 + "'  AND   m.PartyName='" + str4 + "' order by c.ReminderDate";
                 str = " SELECT * from Communicationreport where  (ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  ) and PartyTypeName='" + str3 + "'  AND   PartyName='" + str4 + "' ";
                 SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                 DataTable dt = new DataTable();
                 adp.Fill(dt);
                 CrystalReportViewer1.Visible = true;
                 ReportDocument myRpt = new ReportDocument();
                 myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                 myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                 myRpt.SetDataSource(dt);
                 CrystalReportViewer1.ReportSource = myRpt;
                 Session["rpt"] = myRpt;
             }
         }
         else
         {

             if (Panel1.Visible == true)
             {
                 //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId WHERE (c.date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone') order by c.Date";
                 string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  ) ";
                 SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                 DataTable dt = new DataTable();
                 adp.Fill(dt);
                 CrystalReportViewer1.Visible = true;
                 ReportDocument myRpt = new ReportDocument();
                 myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                 myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                 myRpt.SetDataSource(dt);
                 CrystalReportViewer1.ReportSource = myRpt;
                 Session["rpt"] = myRpt;
             }
             else if (Panel2.Visible == true)
             {
                 //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId WHERE (c.ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "') AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')   order by c.ReminderDate";
                 string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "' AND (Smallmesstype='IncomingPhone' or Smallmesstype='OutgoingPhone')  ) ";
                 SqlDataAdapter adp = new SqlDataAdapter(str, cn);
                 DataTable dt = new DataTable();
                 adp.Fill(dt);
                 CrystalReportViewer1.Visible = true;
                 ReportDocument myRpt = new ReportDocument();
                 myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
                 myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
                 myRpt.SetDataSource(dt);
                 CrystalReportViewer1.ReportSource = myRpt;
                 Session["rpt"] = myRpt;
             }
         } 
     }
     public void all1()
     {    
         if (Panel1.Visible == true)
         {
             //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId 
             //WHERE (c.Date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "') order by c.Date";
             string str = " SELECT * from Communicationreport where  (Date between'" + Convert.ToDateTime(txtFromdate4.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate4.Text).ToShortDateString() + "'  ";
             SqlDataAdapter adp = new SqlDataAdapter(str, cn);
             DataTable dt = new DataTable();
             adp.Fill(dt);
             CrystalReportViewer1.Visible = true;
             ReportDocument myRpt = new ReportDocument();
             myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
             myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
             myRpt.SetDataSource(dt);
             CrystalReportViewer1.ReportSource = myRpt;
             Session["rpt"] = myRpt;
         }
         else if (Panel2.Visible == true)
         {
             //string str = "SELECT c.Date,c.ReminderDate,p.PartyTypeName,m.PartyName,e.EmployeeName,c.Phoneno,c.Description,c.CommID,SmallMessageType.Smallmesstype FROM CapmanIfilecabinet.dbo.PartyTypeMaster AS p INNER JOIN CommunicationDetail AS c ON p.PartyTypeId = c.CapmanPartyTypeId INNER JOIN CapmanIfilecabinet.dbo.PartyMaster AS m ON c.CommWith = m.PartyId INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeID INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId
             //WHERE (c.ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "') order by c.ReminderDate";
             string str = " SELECT * from Communicationreport where  (ReminderDate between'" + Convert.ToDateTime(txtFromdate5.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txtTodate5.Text).ToShortDateString() + "'  ";
             SqlDataAdapter adp = new SqlDataAdapter(str, cn);
             DataTable dt = new DataTable();
             adp.Fill(dt);
             CrystalReportViewer1.Visible = true;
             ReportDocument myRpt = new ReportDocument();
             myRpt.Load(Server.MapPath("reports\\CommunicationReport.rpt"));
             myRpt.SetDatabaseLogon("sa", "BarodaBarodaSQL12++");
             myRpt.SetDataSource(dt);
             CrystalReportViewer1.ReportSource = myRpt;
             Session["rpt"] = myRpt;
         }      



     }

}


