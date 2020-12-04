using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
public partial class Account_DocumentMyUploaded : System.Web.UI.Page
{
    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    protected int DesignationId;
    EmployeeCls clsEmployee = new EmployeeCls();
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
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);



        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";

            txtfrom.Text = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
            txtto.Text = System.DateTime.Now.ToShortDateString();
            DesignationId = Convert.ToInt32(Session["DesignationId"]);

            filldatebyperiod();
            fillstore();
            Fillddlemployee();
            filterbyparty();
            FillDocumentTypeAll();
            fillddllistofdocument();
            FillGrid();


        }



    }
    protected void FillDocumentTypeAll()
    {
        DocumentCls1 clsDocument = new DocumentCls1();
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocTypeAll(ddlbusiness.SelectedValue);
        ddldoctype.DataSource = dt;
        ddldoctype.DataTextField = "doctype";
        ddldoctype.DataValueField = "DocumentTypeId";
        ddldoctype.DataBind();
        ddldoctype.Items.Insert(0, "All");
        ddldoctype.Items[0].Value = "0";

    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void Gridreqinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }

    }


    protected DataTable select(string str)
    {

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    protected void FillGrid()
    {

        lblcompany.Text = Session["Cname"].ToString();
        lblcomname.Text = ddlbusiness.SelectedItem.Text;
        lblcabinetdrawerfolderprint.Text = ddldoctype.SelectedItem.Text;
        lbldocidprint.Text = ddllistofdocument.SelectedItem.Text;
        lblsearchbytitleprint.Text = txtsearch.Text;

        string strbydoctype = "";
        string strbydocid = "";
        string strsearch = "";
        string strparty = "";
        string strbyperiod = "";
        string strbydate = "";
        string stremployee = "";
        string strstatus = "";


        if (ddldoctype.SelectedIndex > 0)
        {
            strbydoctype = " and DocumentMaster.DocumentTypeId ='" + ddldoctype.SelectedValue + "' ";
        }
        if (ddllistofdocument.SelectedIndex > 0)
        {
            strbydocid = " and DocumentMaster.DocumentId='" + ddllistofdocument.SelectedValue + "' ";
        }
        if (txtsearch.Text != "")
        {
            strsearch = " and DocumentMaster.DocumentTitle Like '%" + txtsearch.Text.Replace("'", "''") + "%' ";
        }
        if (CheckBox1.Checked == true)
        {
            if (ddlfilterbyparty.SelectedIndex > 0)
            {
                strparty = " and DocumentMaster.PartyId='" + ddlfilterbyparty.SelectedValue + "' ";
            }
        }
        strbydoctype = strbydoctype + " and DocumentMaster.DocumentTypeId IN( SELECT  Distinct  DocumentAccessRightMaster.DocumentTypeId FROM  DocumentAccessRightMaster inner join DocumentType on DocumentType.DocumentTypeId=DocumentAccessRightMaster.DocumentTypeId  WHERE     DesignationId ='" + Session["DesignationId"] + "' and (DocumentAccessRightMaster.CID='" + Session["Comid"] + "') and((ViewAccess='true') or (DeleteAccess='true') or (SaveAccess='true')   or (EditAccess='true') or (EmailAccess='true') or (FaxAccess='true') or (PrintAccess='true') or (MessageAccess='true')))";
        if (RadioButtonList1.SelectedValue == "0")
        {

            if (ViewState["periodstartdate"] != null && ViewState["periodenddate"] != null)
            {
                strbyperiod = " and DocumentMaster.DocumentUploadDate between '" + ViewState["periodstartdate"].ToString() + "' and '" + ViewState["periodenddate"].ToString() + "'";
            }
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtfrom.Text != "" && txtto.Text != "")
            {
                strbydate = " and DocumentMaster.DocumentUploadDate between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
            }
        }
        if (ddlemployee.SelectedIndex > 0)
        {
            stremployee = " and DocumentEmpApproveLog.EmployeeID='" + ddlemployee.SelectedValue + "' ";

        }

        if (ddlstatus.SelectedValue != "2")
        {
            strstatus = " and DocumentEmpApproveLog.Approve='" + ddlstatus.SelectedValue + "'";
        }


        string str = " DocumentEmpApproveLog.ApproveDate, EmployeeMaster.EmployeeName, DocumentEmpApproveLog.DocumentId, DocumentEmpApproveLog.Approve,case when  (DocumentEmpApproveLog.Approve='1') Then 'Accepted' else 'Rejected' End as Approvelabel, Left(DocumentEmpApproveLog.Note,25) as Notesmall,DocumentEmpApproveLog.Note ,DocumentType.DocumentType, DocumentEmpApproveLog.EmployeeID, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentTitle, DocumentMaster.DocumentUploadTypeId, Convert(nvarchar,DocumentMaster.DocumentUploadDate,101) as DocumentUploadDate, DocumentMaster.DocumentName, DocumentMaster.PartyId, DocumentMaster.Description, DocumentMaster.DocumentRefNo, DocumentMaster.DocumentAmount, Party_Master.Compname as PartyName, RuleApproveTypeMaster.RuleApproveTypeName as DocumentApproveType FROM DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId  INNER JOIN DocumentEmpApproveLog ON DocumentMaster.DocumentId = DocumentEmpApproveLog.DocumentId LEFT OUTER JOIN RuleApproveTypeMaster ON DocumentEmpApproveLog.DocumentApproveTypeId = RuleApproveTypeMaster.RuleApproveTypeId  LEFT OUTER JOIN EmployeeMaster ON DocumentEmpApproveLog.EmployeeID = EmployeeMaster.EmployeeMasterID LEFT OUTER JOIN Party_Master ON DocumentMaster.PartyId = Party_master.PartyId  where DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and DocumentMaster.CID='" + Session["Comid"] + "' " + strbydoctype + " " + strbydocid + " " + strsearch + " " + strparty + " " + strbyperiod + " " + strbydate + " " + stremployee + " " + strstatus + "  ";

        string str2 = "select count(DocumentEmpApproveLog.DocumentId) as ci FROM DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId  INNER JOIN DocumentEmpApproveLog ON DocumentMaster.DocumentId = DocumentEmpApproveLog.DocumentId LEFT OUTER JOIN RuleApproveTypeMaster ON DocumentEmpApproveLog.DocumentApproveTypeId = RuleApproveTypeMaster.RuleApproveTypeId  LEFT OUTER JOIN EmployeeMaster ON DocumentEmpApproveLog.EmployeeID = EmployeeMaster.EmployeeMasterID LEFT OUTER JOIN Party_Master ON DocumentMaster.PartyId = Party_master.PartyId  where DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' and DocumentMaster.CID='" + Session["Comid"] + "' " + strbydoctype + " " + strbydocid + " " + strsearch + " " + strparty + " " + strbyperiod + " " + strbydate + " " + stremployee + " " + strstatus + "  ";

        Gridreqinfo.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " DocumentEmpApproveLog.ApproveDate ";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dtcat = GetDataPage(Gridreqinfo.PageIndex, Gridreqinfo.PageSize, sortExpression, str);

            Gridreqinfo.DataSource = dtcat;
            DataView myDataView = new DataView();
            myDataView = dtcat.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            Gridreqinfo.DataBind();
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

    //protected DataTable select(string qu)
    //{
    //    SqlCommand cmd = new SqlCommand(qu, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    return dt;
    //}

    protected void Gridreqinfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gridreqinfo.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void Gridreqinfo_Sorting(object sender, GridViewSortEventArgs e)
    {

        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillGrid();
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

    protected void ddlDuration_SelectedIndexChanged(object sender, EventArgs e)
    {

        filldatebyperiod();
        fillddllistofdocument();

    }
    protected void filldatebyperiod()
    {
        //date between you should use  date first and earlier date lateafter
        string Today, Yesterday, ThisYear;
        Today = Convert.ToString(System.DateTime.Today.ToShortDateString());
        Yesterday = Convert.ToString(System.DateTime.Today.AddDays(-1).ToShortDateString());
        ThisYear = Convert.ToString(System.DateTime.Today.Year.ToString());


        //-------------------this week start...............
        DateTime d1, d2, d3, d4, d5, d6, d7;
        DateTime weekstart, weekend;
        d1 = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
        d2 = Convert.ToDateTime(System.DateTime.Today.AddDays(-1).ToShortDateString());
        d3 = Convert.ToDateTime(System.DateTime.Today.AddDays(-2).ToShortDateString());
        d4 = Convert.ToDateTime(System.DateTime.Today.AddDays(-3).ToShortDateString());
        d5 = Convert.ToDateTime(System.DateTime.Today.AddDays(-4).ToShortDateString());
        d6 = Convert.ToDateTime(System.DateTime.Today.AddDays(-5).ToShortDateString());
        d7 = Convert.ToDateTime(System.DateTime.Today.AddDays(-6).ToShortDateString());
        string ThisWeek = (System.DateTime.Today.DayOfWeek.ToString());
        if (ThisWeek.ToString() == "Monday")
        {
            weekstart = d1;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(ThisWeek) == "Tuesday")
        {
            weekstart = d2;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Wednesday")
        {
            weekstart = d3;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Thursday")
        {
            weekstart = d4;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Friday")
        {
            weekstart = d5;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Saturday")
        {
            weekstart = d6;
            weekend = weekstart.Date.AddDays(+6);

        }
        else
        {
            weekstart = d7;
            weekend = weekstart.Date.AddDays(+6);
        }
        string thisweekstart = weekstart.ToShortDateString();
        string thisweekend = weekend.ToShortDateString();

        //.................this week duration end.....................

        ///--------------------last week duration ....

        DateTime d17, d8, d9, d10, d11, d12, d13;
        DateTime lastweekstart, lastweekend;
        d17 = Convert.ToDateTime(System.DateTime.Today.AddDays(-7).ToShortDateString());
        d8 = Convert.ToDateTime(System.DateTime.Today.AddDays(-8).ToShortDateString());
        d9 = Convert.ToDateTime(System.DateTime.Today.AddDays(-9).ToShortDateString());
        d10 = Convert.ToDateTime(System.DateTime.Today.AddDays(-10).ToShortDateString());
        d11 = Convert.ToDateTime(System.DateTime.Today.AddDays(-11).ToShortDateString());
        d12 = Convert.ToDateTime(System.DateTime.Today.AddDays(-12).ToShortDateString());
        d13 = Convert.ToDateTime(System.DateTime.Today.AddDays(-13).ToShortDateString());
        string thisday = (System.DateTime.Today.DayOfWeek.ToString());
        if (thisday.ToString() == "Monday")
        {
            lastweekstart = d17;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            lastweekstart = d8;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            lastweekstart = d9;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            lastweekstart = d10;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            lastweekstart = d11;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            lastweekstart = d12;
            lastweekend = lastweekstart.Date.AddDays(+6);

        }
        else
        {
            lastweekstart = d13;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        string lastweekstartdate = lastweekstart.ToShortDateString();
        string lastweekenddate = lastweekend.ToShortDateString();
        //---------------lastweek duration end.................

        //        Today
        //2	Yesterday
        //3	ThisWeek
        //4	LastWeek
        //5	ThisMonth
        //6	LastMonth
        //7	ThisQuarter
        //8	LastQuarter
        //9	ThisYear
        //10Last Year
        //------------------this month period-----------------

        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        string thismonthstartdate = thismonthstart.ToShortDateString();
        string thismonthenddate = Today.ToString();
        //------------------this month period end................



        //-----------------last month period start ---------------


        int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
      
        
        if (lastmonthno == 0)
        {
            lastmonthno = 12;
            Int32 ThisYearstr = Convert.ToInt32(ThisYear) - 1;
            ThisYear = Convert.ToString(ThisYearstr);
        }
        string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        DateTime lastmonth = Convert.ToDateTime(lastmonthNumber.ToString() + "/1/" + ThisYear.ToString());
        string lastmonthstart = lastmonth.ToShortDateString();
       
       

        string lastmonthend = "";

        if (lastmonthNumber == "1" || lastmonthNumber == "3" || lastmonthNumber == "5" || lastmonthNumber == "7" || lastmonthNumber == "8" || lastmonthNumber == "10" || lastmonthNumber == "12")
        {
            lastmonthend = lastmonthNumber + "/31/" + ThisYear.ToString();
        }
        else if (lastmonthNumber == "4" || lastmonthNumber == "6" || lastmonthNumber == "9" || lastmonthNumber == "11")
        {
            lastmonthend = lastmonthNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastmonthend = lastmonthNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                lastmonthend = lastmonthNumber + "/28/" + ThisYear.ToString();
            }
        }

        string lastmonthstartdate = lastmonthstart.ToString();
        string lastmonthenddate = lastmonthend.ToString();


        //-----------------last month period end -----------------------

        //--------------------this quater period start ----------------


        int thisqtr = 0;
        string thisqtrNumber = "";
        int mon = Convert.ToInt32(DateTime.Now.Month.ToString());
        if (mon >= 1 && mon <= 3)
        {
            thisqtr = 1;
            thisqtrNumber = "3";

        }
        else if (mon >= 4 && mon <= 6)
        {
            thisqtr = 4;
            thisqtrNumber = "6";
        }
        else if (mon >= 7 && mon <= 9)
        {
            thisqtr = 7;
            thisqtrNumber = "9";
        }
        else if (mon >= 10 && mon <= 12)
        {
            thisqtr = 10;
            thisqtrNumber = "12";
        }


        DateTime thisquater = Convert.ToDateTime(thisqtr.ToString() + "/1/" + ThisYear.ToString());
        string thisquaterstart = thisquater.ToShortDateString();

        string thisquaterend = "";

        if (thisqtrNumber == "1" || thisqtrNumber == "3" || thisqtrNumber == "5" || thisqtrNumber == "7" || thisqtrNumber == "8" || thisqtrNumber == "10" || thisqtrNumber == "12")
        {
            thisquaterend = thisqtrNumber + "/31/" + ThisYear.ToString();
        }
        else if (thisqtrNumber == "4" || thisqtrNumber == "6" || thisqtrNumber == "9" || thisqtrNumber == "11")
        {
            thisquaterend = thisqtrNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                thisquaterend = thisqtrNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                thisquaterend = thisqtrNumber + "/28/" + ThisYear.ToString();
            }
        }

        string thisquaterstartdate = thisquaterstart.ToString();
        string thisquaterenddate = thisquaterend.ToString();


        // --------------this quater period end ------------------------

        // --------------last quater period start----------------------

        int lastqtr = Convert.ToInt32(thismonthstart.AddMonths(-5).Month.ToString());// -5;
        string lastqtrNumber = Convert.ToString(lastqtr.ToString());
        int lastqater3 = Convert.ToInt32(thismonthstart.AddMonths(-3).Month.ToString());
        //DateTime lastqater3 = Convert.ToDateTime(System.DateTime.Now.AddMonths(-3).Month.ToString());
        string lasterquater3 = lastqater3.ToString();
        DateTime lastquater = Convert.ToDateTime(lastqtrNumber.ToString() + "/1/" + ThisYear.ToString());
        string lastquaterstart = lastquater.ToShortDateString();
        string lastquaterend = "";

        if (lasterquater3 == "1" || lasterquater3 == "3" || lasterquater3 == "5" || lasterquater3 == "7" || lasterquater3 == "8" || lasterquater3 == "10" || lasterquater3 == "12")
        {
            lastquaterend = lasterquater3 + "/31/" + ThisYear.ToString();
        }
        else if (lasterquater3 == "4" || lasterquater3 == "6" || lasterquater3 == "9" || lasterquater3 == "11")
        {
            lastquaterend = lasterquater3 + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastquaterend = lasterquater3 + "/29/" + ThisYear.ToString();
            }
            else
            {
                lastquaterend = lasterquater3 + "/28/" + ThisYear.ToString();
            }
        }

        string lastquaterstartdate = lastquaterstart.ToString();
        string lastquaterenddate = lastquaterend.ToString();

        //--------------last quater period end-------------------------

        //--------------this year period start----------------------
        DateTime thisyearstart = Convert.ToDateTime("1/1/" + ThisYear.ToString());
        DateTime thisyearend = Convert.ToDateTime("12/31/" + ThisYear.ToString());

        string thisyearstartdate = thisyearstart.ToShortDateString();
        string thisyearenddate = thisyearend.ToShortDateString();

        //---------------this year period end-------------------
        //--------------this year period start----------------------
        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());

        string lastyearstartdate = lastyearstart.ToShortDateString();
        string lastyearenddate = lastyearend.ToShortDateString();



        //---------------this year period end-------------------


        string periodstartdate = "";
        string periodenddate = "";

        if (ddlDuration.SelectedItem.Text == "Today")
        {
            periodstartdate = Today.ToString();
            periodenddate = Today.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "Yesterday")
        {
            periodstartdate = Yesterday.ToString();
            periodenddate = Yesterday.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "This Week")
        {
            periodstartdate = thisweekstart.ToString();
            periodenddate = thisweekend.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "Last Week")
        {

            periodstartdate = lastweekstartdate.ToString();
            periodenddate = lastweekenddate.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "This Month")
        {

            periodstartdate = thismonthstart.ToShortDateString();
            periodenddate = Today.ToString();
        }
        else if (ddlDuration.SelectedItem.Text == "Last Month")
        {

            periodstartdate = lastmonthstartdate.ToString();
            periodenddate = lastmonthenddate.ToString();


        }
        else if (ddlDuration.SelectedItem.Text == "This Quarter")
        {

            periodstartdate = thisquaterstartdate.ToString();
            periodenddate = thisquaterenddate.ToString();


        }
        else if (ddlDuration.SelectedItem.Text == "Last Quarter")
        {

            periodstartdate = lastquaterstartdate.ToString();
            periodenddate = lastquaterenddate.ToString();


        }

        else if (ddlDuration.SelectedItem.Text == "This Year")
        {

            periodstartdate = thisyearstartdate.ToString();
            periodenddate = thisyearenddate.ToString();


        }
        else if (ddlDuration.SelectedItem.Text == "Last Year")
        {

            periodstartdate = lastyearstartdate.ToString();
            periodenddate = lastyearenddate.ToString();
        }
        else
        {
            periodstartdate = Today.ToString();
            periodenddate = Today.ToString();
        }
        if (periodstartdate.Length > 0)
        {
            txtfrom.Text = periodstartdate;
            txtto.Text = periodenddate;

        }
        ViewState["periodstartdate"] = periodstartdate.ToString();
        ViewState["periodenddate"] = periodenddate.ToString();
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillddlemployee();
        FillDocumentTypeAll();
        fillddllistofdocument();
        filterbyparty();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Gridreqinfo.AllowPaging = false;
            Gridreqinfo.PageSize = 1000;
            FillGrid();

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(200);

            Gridreqinfo.AllowPaging = true;
            Gridreqinfo.PageSize = 10;
            FillGrid();

            Button1.Text = "Printable Version";
            Button7.Visible = false;

        }
    }

    protected void fillstore()
    {
        ddlbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlbusiness.DataSource = ds;
        ddlbusiness.DataTextField = "Name";
        ddlbusiness.DataValueField = "WareHouseId";
        ddlbusiness.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (RadioButtonList1.SelectedValue == "0")
        {
            pnlperiod.Visible = true;
            pnldate.Visible = false;
        }
        else
        {
            pnlperiod.Visible = false;
            pnldate.Visible = true;
        }
        filldatebyperiod();
        fillddllistofdocument();
    }
    protected void fillddllistofdocument()
    {
        string strcat = "SELECT DocumentMaster.DocumentId,Convert(Nvarchar(50),DocumentMaster.DocumentId) + ':' +DocumentMaster.DocumentTitle as DocumentTitle  FROM   DocumentMainType inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join      DocumentType  on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster ON DocumentMaster.DocumentTypeId = DocumentType.DocumentTypeId LEFT OUTER JOIN DocumentProcessing ON DocumentMaster.DocumentId = DocumentProcessing.DocumentId LEFT OUTER JOIN Party_Master ON DocumentMaster.PartyId = Party_Master.PartyId  WHERE  DocumentMainType.Whid='" + ddlbusiness.SelectedValue + "' AND DocumentMaster.DocumentId  in ( SELECT  distinct   DocumentId FROM         DocumentProcessing WHERE     (Approve = 1) ) AND DocumentMaster.DocumentId not in ( SELECT distinct    DocumentId FROM         DocumentProcessing WHERE     (Approve = 0) or (Approve is null) )  and(DocumentMaster.CID='" + Session["Comid"] + "') ";
        string strtypeid = "";
        string strbyperiod = "";
        string strbydate = "";

        if (ddldoctype.SelectedIndex > 0)
        {
            strtypeid = " And DocumentMaster.DocumentTypeId='" + ddldoctype.SelectedValue + "'";
        }

        if (RadioButtonList1.SelectedValue == "0")
        {

            if (ViewState["periodstartdate"] != null && ViewState["periodenddate"] != null)
            {
                strbyperiod = " and DocumentMaster.DocumentUploadDate between '" + ViewState["periodstartdate"].ToString() + "' and '" + ViewState["periodenddate"].ToString() + "'";
            }
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtfrom.Text != "" && txtto.Text != "")
            {
                strbydate = " and DocumentMaster.DocumentUploadDate between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
            }
        }

        string finalstr = strcat + strtypeid + strbyperiod + strbydate;

        SqlCommand cmdcat = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);

        ddllistofdocument.DataSource = dtcat;
        ddllistofdocument.DataTextField = "DocumentTitle";
        ddllistofdocument.DataValueField = "DocumentId";
        ddllistofdocument.DataBind();
        ddllistofdocument.Items.Insert(0, "All");
        ddllistofdocument.Items[0].Value = "0";
    }
    protected void ddldoctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddllistofdocument();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            pnlfilterbyparty.Visible = true;

            filterbyparty();
        }
        else
        {
            pnlfilterbyparty.Visible = false;
        }
    }
    protected void filterbyparty()
    {
        ddlfilterbyparty.Items.Clear();



        string finalstr = " SELECT   PartyId, Party_Master.PartyTypeId, Account,PartytTypeMaster.PartType + ':'+ Party_Master.ContactPerson +':'+ Party_Master.Compname as PartyName FROM    Party_master INNER JOIN  PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId WHERE   Party_master.Whid='" + ddlbusiness.SelectedValue + "' order by PartyName ";

        SqlCommand cmdcat = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);




        ddlfilterbyparty.DataSource = dtcat;
        ddlfilterbyparty.DataTextField = "PartyName";
        ddlfilterbyparty.DataValueField = "PartyId";
        ddlfilterbyparty.DataBind();

        ddlfilterbyparty.Items.Insert(0, "All");
        ddlfilterbyparty.Items[0].Value = "0";
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {

        LinkButton LinkButton3 = (LinkButton)Gridreqinfo.HeaderRow.FindControl("LinkButton3");




        foreach (GridViewRow grd in Gridreqinfo.Rows)
        {

            Label lblapprovalnotesmall = (Label)grd.FindControl("lblapprovalnotesmall");
            Label lblapprovalnotebig = (Label)grd.FindControl("lblapprovalnotebig");

            if (LinkButton3.Text == "(More Info)")
            {

                lblapprovalnotebig.Visible = true;
                lblapprovalnotesmall.Visible = false;

            }
            if (LinkButton3.Text == "(Hide)")
            {

                lblapprovalnotebig.Visible = false;
                lblapprovalnotesmall.Visible = true;
            }



        }
        if (LinkButton3.Text == "(More Info)")
        {
            LinkButton3.Text = "(Hide)";
        }
        else
        {
            LinkButton3.Text = "(More Info)";

        }




    }

    protected void Fillddlemployee()
    {
        //MasterCls clsMaster = new MasterCls();
        //DataTable dt = new DataTable();
        //dt = clsMaster.SelectEmployeewithDesgDeptName(ddlbusiness.SelectedValue);
        ddlemployee.Items.Clear();
        string finalstr = "SELECT    EmployeeMaster.EmployeeMasterID as EmployeeID, DepartmentMasterMNC.DepartmentName +' : '+ DesignationMaster.DesignationName+' : '+ EmployeeMaster.EmployeeName as  EmployeeName  FROM   DesignationMaster LEFT OUTER JOIN   DepartmentMasterMNC ON DesignationMaster.Deptid = DepartmentMasterMNC.id RIGHT OUTER JOIN EmployeeMaster ON EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId   where  EmployeeMaster.Whid='" + ddlbusiness.SelectedValue + "' order by EmployeeName asc ";

        SqlCommand cmdcat = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);



        ddlemployee.DataSource = dtcat;
        ddlemployee.DataBind();
        ddlemployee.Items.Insert(0, "All");
        ddlemployee.SelectedItem.Value = "0";
    }

}
