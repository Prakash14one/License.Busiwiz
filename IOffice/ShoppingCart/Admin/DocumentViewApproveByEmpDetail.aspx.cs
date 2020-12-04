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
public partial class DocumentViewToUser : System.Web.UI.Page
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
			Session["PageUrl"]=strData;
            Session["PageName"] = page;
            Page.Title = pg.getPageTitle(page);

            if (Session["CompanyName"] != null)
            {
                this.Title = Session["CompanyName"] + " IFileCabinet.com Document Department - Document Approve by Employee";
            }

            Session["PageName"] = "DocumentViewApproveByEmpDetail.aspx";


            if (!IsPostBack)
            {
                ViewState["sortOrder"] = "";

                txtfrom.Text = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
                txtto.Text = System.DateTime.Now.ToShortDateString();
                string str = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";
        
                SqlCommand cmd1 = new SqlCommand(str, con);
                cmd1.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlbusiness.DataSource = dt;
                ddlbusiness.DataTextField = "Name";
                ddlbusiness.DataValueField = "WareHouseId";
                ddlbusiness.DataBind();
                string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
                SqlCommand cmdeeed = new SqlCommand(eeed, con);
                SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                DataTable dteeed = new DataTable();
                adpeeed.Fill(dteeed);
                if (dteeed.Rows.Count > 0)
                {
                    ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

                }
                ddlbusiness_SelectedIndexChanged(sender, e);
               
                clear();
                DesignationId = Convert.ToInt32(Session["DesignationId"]);
               
                griddocviewapprvbyemp.Visible = false;
            }
            
            
      
    }
  
    protected void Fillddlemployee()
    {
        MasterCls clsMaster = new MasterCls();
        DataTable dt = new DataTable();
        dt = clsMaster.SelectEmployeewithDesgDeptName(ddlbusiness.SelectedValue);
        ddlemployee.DataSource = dt;
        ddlemployee.DataBind();
        ddlemployee.Items.Insert(0, "All");
        ddlemployee.SelectedItem.Value = "0";
    }
    


    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void griddocviewapprvbyemp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "edit1")
        //{
        //    int DocumentId = Convert.ToInt32(e.CommandArgument);
        //    int rst = clsDocument.InsertDocumentLog(DocumentId, Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, false, false, true, false, false, false, false);
        //    Response.Redirect("DocumentEdit.aspx?id=" + DocumentId + "&&return=5");
        //}
        //if (e.CommandName == "delete1")
        //{
        //    int DocumentId = Convert.ToInt32(e.CommandArgument);
        //    int rst = clsDocument.DeleteDocumentMasterByID(DocumentId);
        //    int rst1 = clsDocument.InsertDocumentLog(DocumentId, Convert.ToInt32(Session["EmployeeId"]), Convert.ToDateTime(System.DateTime.Now), false, true, false, false, false, false, false, false);
        //    if (rst > 0)
        //    {
        //        pnlmsg.Visible = true;
        //        lblmsg.Text = "Document Deleted Successfully.";
        //        FillGrid();
        //    }

        //}

    }

    protected void FillGrid()
    {
        griddocviewapprvbyemp.Visible = true;
        DataTable dt = new DataTable();
        //dt = clsDocument.SelectDocumentAccessRigthsByDesignationID();
        DataTable dt1 = new DataTable();
        //DataTable dt2 = new DataTable();
        //int flag = 1;
        //foreach (DataRow dr in dt.Rows)
        //{
        lblcompany.Text = Session["Cname"].ToString();
        lblcomname.Text = ddlbusiness.SelectedItem.Text;
        lblstatus.Text = ddlaprv.SelectedItem.Text;
        lblsdate.Text = txtfrom.Text;
        lblenddate.Text = txtto.Text;
        if((txtsearch.Text.Length <= 0))
        {
            lblemp.Visible = true;
         
            lblserchtitle.Visible = false;
            lblemptext.Text = ddlemployee.SelectedItem.Text;
           
                    if (ddlaprv.SelectedIndex > 0)
                    {

                        dt1 = clsDocument.SelectDoucmentMasterByDocumentTypeID_se(Convert.ToInt32(ddlemployee.SelectedItem.Value.ToString()), Convert.ToDateTime(txtfrom.Text), Convert.ToDateTime(txtto.Text), Convert.ToBoolean(ddlaprv.SelectedValue), ddlbusiness.SelectedValue,1);
                    }
                    else
                    {
                        dt1 = clsDocument.SelectDoucmentMasterByDocumentTypeID_se(Convert.ToInt32(ddlemployee.SelectedItem.Value.ToString()), Convert.ToDateTime(txtfrom.Text), Convert.ToDateTime(txtto.Text), Convert.ToBoolean("True"), ddlbusiness.SelectedValue,-1);
       
                    }
                
        }
        else
        {
            if (txtsearch.Text.Length > 0)
            {
                lblemp.Visible = false;
                lblemptext.Visible = false;
                lblserchtitle.Visible = true;
                lblserchtitle.Text = "Search by Title :" + txtsearch.Text;
                if (ddlaprv.SelectedIndex > 0)
                {
                    dt1 = clsDocument.SelectDoucmentMasterByText_se(Convert.ToInt32(ddlemployee.SelectedItem.Value.ToString()),txtsearch.Text, Convert.ToDateTime(txtfrom.Text), Convert.ToDateTime(txtto.Text), Convert.ToBoolean(ddlaprv.SelectedValue), ddlbusiness.SelectedValue,1);


                }
                else
                {
                    dt1 = clsDocument.SelectDoucmentMasterByText_se(Convert.ToInt32(ddlemployee.SelectedItem.Value.ToString()),txtsearch.Text, Convert.ToDateTime(txtfrom.Text), Convert.ToDateTime(txtto.Text), Convert.ToBoolean("True"), ddlbusiness.SelectedValue,-1);

                }
            }
        }
        DataView myDataView = new DataView();
        myDataView = dt1.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        griddocviewapprvbyemp.DataSource = dt1;
        griddocviewapprvbyemp.DataBind();
       
    } 

    protected void griddocviewapprvbyemp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddocviewapprvbyemp.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    
 
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
       // clear();
    }
    protected void clear()
    {
        //ddldepartmetn.SelectedIndex = 0;
        //ddldesignation.SelectedIndex = 0;
        ddlemployee.SelectedIndex = 0;
        griddocviewapprvbyemp.Visible = false;
    }
    
    protected void ddlDuration_SelectedIndexChanged(object sender, EventArgs e)
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
            string lastweekenddate = lastweekend.ToString();
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
            string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
            DateTime lastmonth = Convert.ToDateTime(lastmonthNumber.ToString() + "/1/" + ThisYear.ToString());
            string lastmonthstart = lastmonth.ToShortDateString();
            string lastmonthend = "";

            if (lastmonthNumber == "1" || lastmonthNumber == "3" || lastmonthNumber == "5" || lastmonthNumber == "7" || lastmonthNumber == "9" || lastmonthNumber == "11")
            {
                lastmonthend = lastmonthNumber + "/31/" + ThisYear.ToString();
            }
            else if (lastmonthNumber == "4" || lastmonthNumber == "6" || lastmonthNumber == "8" || lastmonthNumber == "10" || lastmonthNumber == "12")
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

            int thisqtr = Convert.ToInt32(thismonthstart.AddMonths(-2).Month.ToString());
            string thisqtrNumber = Convert.ToString(thisqtr.ToString());
            DateTime thisquater = Convert.ToDateTime(thisqtrNumber.ToString() + "/1/" + ThisYear.ToString());
            string thisquaterstart = thisquater.ToShortDateString();
            string thisquaterend = "";

            if (thisqtrNumber == "1" || thisqtrNumber == "3" || thisqtrNumber == "5" || thisqtrNumber == "7" || thisqtrNumber == "9" || thisqtrNumber == "11")
            {
                thisquaterend = thisqtrNumber + "/31/" + ThisYear.ToString();
            }
            else if (thisqtrNumber == "4" || thisqtrNumber == "6" || thisqtrNumber == "8" || thisqtrNumber == "10" || thisqtrNumber == "12")
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

            if (lastqtrNumber == "1" || lastqtrNumber == "3" || lastqtrNumber == "5" || lastqtrNumber == "7" || lastqtrNumber == "9" || lastqtrNumber == "11")
            {
                lastquaterend = lasterquater3 + "/31/" + ThisYear.ToString();
            }
            else if (lastqtrNumber == "4" || lastqtrNumber == "6" || lastqtrNumber == "8" || lastqtrNumber == "10" || lastqtrNumber == "12")
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
                periodenddate = Today.ToString();
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
                periodstartdate = "1/1/1900";
                periodenddate = Today.ToString();
            }
            if (periodstartdate.Length > 0)
            {
             txtfrom.Text = periodstartdate;
               txtto.Text = periodenddate; // periodstartdate; 
               // Session["MFDate"] = txtFromDate.Text;
              //  Session["MTDate"] = txtToDate.Text; // txtFromDate.Text;
              //  DropDownList1_SelectedIndexChanged(sender, e);
            }
        }
    protected void imgbtncalfrom_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillddlemployee();
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
    protected void griddocviewapprvbyemp_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillGrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;            
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(200);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            
        }
    }
}
