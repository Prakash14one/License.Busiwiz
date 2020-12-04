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


public partial class Payrollafterlogin : System.Web.UI.Page
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
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
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
            Pagecontrol.dypcontrol(Page, page);

            //txtstartdate.Text = DateTime.Now.ToShortDateString();

            ViewState["Compid"] = Session["Comid"].ToString();
            ViewState["UserName"] = Session["userid"].ToString();


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

            //   lblcdate.Text = System.DateTime.Now.ToShortDateString();
            findware();
            // fillstatusforpopup();
            // fillreminderstatus();
            //fillreminder();

            SqlDataAdapter dagate = new SqlDataAdapter("select * from GatepassTBL where EmployeeID='" + Session["EmployeeId"].ToString() + "'", con);
            DataTable dtgate = new DataTable();
            dagate.Fill(dtgate);

            //,GatepassDetails.TimeLeft from GatepassTBL inner join GatepassDetails on GatepassDetails.GatePassID=GatepassTBL.Id 

            if (dtgate.Rows.Count > 0)
            {
                SqlDataAdapter dagate1 = new SqlDataAdapter("select GatepassDetails.* from GatepassDetails inner join GatepassTBL on GatepassDetails.GatePassID=GatepassTBL.Id where EmployeeID='" + Session["EmployeeId"].ToString() + "' and GatepassDetails.TimeLeft IS NULL", con);
                DataTable dtgate1 = new DataTable();
                dagate1.Fill(dtgate1);

                if (dtgate1.Rows.Count > 0)
                {
                    Button3.Visible = false;

                    SqlDataAdapter dagate2 = new SqlDataAdapter("select * from GatepassTBL where EmployeeID='" + Session["EmployeeId"].ToString() + "' and GatepassTBL.Approved='1'", con);
                    DataTable dtgate2 = new DataTable();
                    dagate2.Fill(dtgate2);

                    if (dtgate2.Rows.Count > 0)
                    {
                        Label39.Visible = true;
                    }
                    else
                    {
                        SqlDataAdapter dagate3 = new SqlDataAdapter("select * from GatepassTBL where EmployeeID='" + Session["EmployeeId"].ToString() + "' and GatepassTBL.Approved='2'", con);
                        DataTable dtgate3 = new DataTable();
                        dagate3.Fill(dtgate3);

                        if (dtgate3.Rows.Count > 0)
                        {
                            SqlDataAdapter dagate4 = new SqlDataAdapter("select GatepassDetails.* from GatepassDetails inner join GatepassTBL on GatepassDetails.GatePassID=GatepassTBL.Id where EmployeeID='" + Session["EmployeeId"].ToString() + "' and GatepassDetails.TimeReached IS NOT NULL and GatepassDetails.TimeLeft IS NULL", con);
                            DataTable dtgate4 = new DataTable();
                            dagate4.Fill(dtgate4);

                            if (dtgate4.Rows.Count > 0)
                            {
                                ModalPopupExtender4.Show();
                            }
                            else
                            {
                                lblextereq.Visible = true;
                                btngon.Visible = true;
                                Button7.Visible = true;
                                btnout.Enabled = false;
                            }

                        }
                    }
                }
                else
                {
                    Button3.Visible = true;
                }
            }
            else
            {
                Button3.Visible = true;
            }

            fillbatch();
            filllapproval();
            fillleavee();
            fillgrid();

            sugnature();

            //selectInbox();
            //SelectMsgforSendBox();
            //SelectMsgforDeleteBox();
            //SelectMsgforDraft();

            // fillddl1();
            // fillddl123();
            // fillddlsent();
            //fillddldelete();
            // fillddldrafts();

            fillstore11();
            //   fillstore1123();

            fillusertype();
            fillpartytypr();

            fillcompanyname();
            fillcompanyname11();

            filljobapplied();
            filljobapplied11();

            FillPartyGrid();
            FillPartyGridext();

            //dwnemail();
            // SelectMsgforInbox();
            //  SelectMsgforSendBoxext();
            ///  SelectMsgforDeleteBoxext();
            //  SelectMsgforDraftext();

            //fillpaneldisplay();
            lbltodaypres.Text = "Today's Presence - " + DateTime.Now.ToShortDateString();
            filllatecommersgrid();

            fillbusinessdept();
            filltoday();
            fillpayperiod();
            fillYearalldata();
            FillSalary();
            string str = "SELECT * from EmployeeBatchMaster where Employeeid='" + Session["EmployeeId"] + "'";
            SqlCommand cmdwh = new SqlCommand(str, con);
            SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            DataTable dtwh = new DataTable();
            adpwh.Fill(dtwh);

            if (dtwh.Rows.Count > 0)
            {
                ViewState["masterbatchid"] = dtwh.Rows[0]["Batchmasterid"].ToString();

                string strbatchtiming = "SELECT * from BatchTiming  where BatchMasterId='" + ViewState["masterbatchid"] + "'";
                SqlCommand cmdbatchtiming = new SqlCommand(strbatchtiming, con);
                SqlDataAdapter adpbatchtiming = new SqlDataAdapter(cmdbatchtiming);
                DataTable dtbatchtiming = new DataTable();
                adpbatchtiming.Fill(dtbatchtiming);

                if (dtbatchtiming.Rows.Count > 0)
                {

                    string strbatchworking = "SELECT * from BatchWorkingDay where BatchMasterId='" + ViewState["masterbatchid"] + "'";
                    SqlCommand cmdbatchworking = new SqlCommand(strbatchworking, con);
                    SqlDataAdapter adpbatchworking = new SqlDataAdapter(cmdbatchworking);
                    DataTable dtbatchworking = new DataTable();
                    adpbatchworking.Fill(dtbatchworking);

                    if (dtbatchworking.Rows.Count > 0)
                    {
                        filldate();
                        //pnlsetupwizardlabel.Visible = false;

                        if (Pnlcnfout.Visible == true)
                        {

                        }

                    }
                    else
                    {
                        //pnlsetupwizardlabel.Visible = true;
                        pnlintime.Visible = false;
                    }


                }
                else
                {
                    //pnlsetupwizardlabel.Visible = true;
                    pnlintime.Visible = false;

                }
            }
            else
            {
                //pnlsetupwizardlabel.Visible = true;
                pnlintime.Visible = false;
            }
        }
    }
    protected void lblfe1_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;

        string salid = grdallemp.DataKeys[rinrow].Value.ToString();

        Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");


        string te = "EmployeePayroll.aspx?EID=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";


    }
    protected void fillYearalldata()
    {

        DataTable dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,Convert(nvarchar,EndDate,101)   as EndDate from [ReportPeriod] where  Active='1'  and Whid='" + ViewState["Whid"] + "'");
        if (dt.Rows.Count > 0)
        {

            DataTable dt11 = (DataTable)select("Select distinct  payperiodMaster.PayperiodName+' - '+Convert(nvarchar,PayperiodStartDate,101) +' - '+ Convert(nvarchar,PayperiodEndDate,101) as PayperiodName, payperiodMaster.Id,Sum(Cast(NetTotal as Decimal(18,2))) as NetTotal," +
    " Sum(Cast(GrossRemu as Decimal(18,2))) as GrossRemu,Sum(Cast(NonGovdedamt as Decimal(18,2))) as NonGovdedamt," +
    " Sum(Cast(Govdedamt as Decimal(18,2))) as Govdedamt from  SalaryMaster inner join payperiodMaster on " +
    " payperiodMaster.Id=SalaryMaster.payperiodtypeId where PayperiodStartDate>='" + Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToShortDateString() + "' and PayperiodEndDate<='" + Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToShortDateString() + "' and SalaryMaster.Whid='" + ViewState["Whid"] + "'  Group by payperiodMaster.Id, PayperiodName,PayperiodStartDate,PayperiodEndDate Order by Id Desc");
            if (dt11.Rows.Count > 0)
            {
                DataView myDataView = new DataView();
                myDataView = dt11.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }
                pnlGrdperiod.ScrollBars = ScrollBars.None;
                pnlGrdperiod.Height = new Unit();
                grdperiodpay.DataSource = myDataView;
                grdperiodpay.DataBind();
                if (grdperiodpay.Rows.Count > 3)
                {
                    pnlGrdperiod.ScrollBars = ScrollBars.Vertical;
                    pnlGrdperiod.Height = new Unit("200px");
                }
            }
            foreach (GridViewRow item in grdperiodpay.Rows)
            {
                string strlow = grdperiodpay.DataKeys[item.RowIndex].Value.ToString();
                Label lblnetamt = (Label)item.FindControl("lblnetamt");
                Label lblpaidtype = (Label)item.FindControl("lblpaidtype");
                DataTable dtc = select("Select distinct Sum(Cast(PaidAmt as Decimal(18,0))) as PaidAmt from SalaryRelatedPayTbl inner join  SalaryMaster on SalaryMaster.Id=SalaryRelatedPayTbl.SalaryMasterId where SalaryMaster.payperiodtypeId='" + strlow + "' and SalaryMaster.Whid='" + ViewState["Whid"] + "'");
                if (dtc.Rows.Count > 0)
                {
                    if (Convert.ToString(dtc.Rows[0]["PaidAmt"]) != "")
                    {
                        decimal Av = Math.Round(Convert.ToDecimal(lblnetamt.Text), 0);
                        if (Av == Convert.ToDecimal(dtc.Rows[0]["PaidAmt"]))
                        {
                            lblpaidtype.Text = "Fully Paid";
                        }
                        else if (Av == 0)
                        {

                            lblpaidtype.Text = "Unpaid";

                        }
                        else
                        {
                            lblpaidtype.Text = "Partially Paid";

                        }
                    }
                }
                else
                {
                    lblpaidtype.Text = "Unpaid";
                }
            }
        }
    }
    protected void grdallemp_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);




            HeaderCell = new TableCell();
            HeaderCell.Text = "Gross Remuneration";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);


            int totde = 2;


            HeaderCell = new TableCell();
            HeaderCell.Text = "Less : Deductions";
            HeaderCell.ColumnSpan = totde;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Balance";

            HeaderCell.ColumnSpan = 1;

            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);


            HeaderGridRow.Cells.Add(HeaderCell);

            //if (PayNo > 0)
            //{
            //    HeaderCell = new TableCell();
            //    HeaderCell.Text = "Payment Details";

            //    HeaderCell.ColumnSpan = PayNo + 2;
            //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //    HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            //    HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);


            //    HeaderGridRow.Cells.Add(HeaderCell);

            //}


            grdallemp.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void fillpayperiod()
    {
        DataTable dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,Convert(nvarchar,EndDate,101)   as EndDate from [ReportPeriod] where  Active='1'  and Whid='" + ViewState["Whid"] + "'");
        if (dt.Rows.Count > 0)
        {

            DataTable dt1 = (DataTable)select("Select Distinct  payperiodMaster.ID,PayperiodName +' : '+Convert(nvarchar,PayperiodStartDate,101) +' : '+ Convert(nvarchar,PayperiodEndDate,101) as period,PayperiodStartDate from  payperiodtype inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join SalaryMaster on SalaryMaster.payperiodtypeId=payperiodMaster.Id where SalaryMaster.Whid='" + ViewState["Whid"] + "'  and PayperiodStartDate>='" + Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToShortDateString() + "' and PayperiodEndDate<='" + Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToShortDateString() + "'  order by PayperiodStartDate");
            ddlpayperiod.DataSource = dt1;
            ddlpayperiod.DataTextField = "period";
            ddlpayperiod.DataValueField = "ID";
            ddlpayperiod.DataBind();
            DataTable dt11 = (DataTable)select("Select Distinct Top(1) SalaryMaster.Id,  payperiodMaster.ID,PayperiodName +' : '+Convert(nvarchar,PayperiodStartDate,101) +' : '+ Convert(nvarchar,PayperiodEndDate,101) as period,PayperiodStartDate from   payperiodMaster  inner join SalaryMaster on SalaryMaster.payperiodtypeId=payperiodMaster.Id where SalaryMaster.Whid='" + ViewState["Whid"] + "' and PayperiodStartDate>='" + Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToShortDateString() + "' and PayperiodEndDate<='" + Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToShortDateString() + "'   order by SalaryMaster.Id Desc");
            if (dt11.Rows.Count > 0)
            {
                ddlpayperiod.SelectedIndex = ddlpayperiod.Items.IndexOf(ddlpayperiod.Items.FindByValue(dt11.Rows[0]["ID"].ToString()));
            }
        }

    }

    protected void FillSalary()
    {
        DataTable dttotal = new DataTable();

        DataTable dt11 = (DataTable)select(" Select distinct Top(6) SalaryMaster.*,Left(FirstName,1)+'.'+Left(LastName,25) as EmployeeName from TranctionMaster inner join SalaryMaster on SalaryMaster.Tid=TranctionMaster.Tranction_Master_Id inner join " +
           " SalaryRemuneration on SalaryRemuneration.SalaryMasterId=SalaryMaster.Id inner join EmployeeMaster on EmployeeMaster.EmployeeMasterId=SalaryMaster.EmployeeId inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterId   where  SalaryMaster.payperiodtypeId='" + ddlpayperiod.SelectedValue + "' and   SalaryMaster.Whid='" + ViewState["Whid"] + "' order by EmployeeName Asc");
        if (dt11.Rows.Count > 0)
        {

            foreach (DataRow item in dt11.Rows)
            {
                String SalId = Convert.ToString(item["Id"]);

                if (ViewState["ISFill"] == null)
                {
                    dttotal = TotalCalc();
                }

                else
                {
                    dttotal = (DataTable)ViewState["ISFill"];
                }
                DataRow dtadd = dttotal.NewRow();
                dtadd["EmployeeName"] = Convert.ToString(item["EmployeeName"]);
                dtadd["EmployeeId"] = Session["EmployeeId"];
                TextBox txttotal = new TextBox();

                dtadd["repa"] = true;
                dtadd["repab"] = false;

                dtadd["SalId"] = SalId;


                DataTable datarem = (DataTable)select("Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname='Hour' and SalaryMasterId='" + SalId + "' order by Id ASC");
                dtadd["Actunit"] = "0";
                dtadd["remamt"] = "0";
                foreach (DataRow grd in datarem.Rows)
                {
                    dtadd["remrate"] = String.Format("{0:n}", Convert.ToDecimal(grd["Rate"])) + " per hour";

                    dtadd["Actunit"] = Convert.ToDecimal(dtadd["Actunit"]) + Convert.ToDecimal(grd["totalunitpay"]); ;
                    dtadd["remamt"] = Convert.ToDecimal(dtadd["remamt"]) + Convert.ToDecimal(grd["totalamt"]);

                }
                DataTable dataday = (DataTable)select("Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname='Day' and SalaryMasterId='" + SalId + "' order by Id ASC");

                foreach (DataRow grd in dataday.Rows)
                {
                    dtadd["remrate"] = String.Format("{0:n}", Convert.ToDecimal(grd["Rate"])) + " per day";

                    dtadd["Actunit"] = Convert.ToDecimal(dtadd["Actunit"]) + Convert.ToDecimal(grd["totalunitpay"]); ;
                    dtadd["remamt"] = Convert.ToDecimal(dtadd["remamt"]) + Convert.ToDecimal(grd["totalamt"]);

                }
                DataTable datamonth = (DataTable)select(" Select distinct SalaryRemuneration.Rate as OverTime, SalaryRemuneration.Rate,SalaryRemuneration.perunitname,totalunit,Actualpayunit as totalunitpay,remamt as totalamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as RemunarationName,Totalcomunitmonth as completemonth,maxcompete as completedmonthamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Month','Semi Month','Bi-Week','Week') and SalaryMasterId='" + SalId + "' order by Id ASC");

                foreach (DataRow grd in datamonth.Rows)
                {
                    dtadd["remrate"] = String.Format("{0:n}", Convert.ToDecimal(grd["Rate"])) + " per " + Convert.ToString(grd["perunitname"]) + "";

                    dtadd["Actunit"] = Convert.ToDecimal(dtadd["Actunit"]) + Convert.ToDecimal(grd["totalunitpay"]); ;
                    dtadd["remamt"] = Convert.ToDecimal(dtadd["remamt"]) + Convert.ToDecimal(grd["totalamt"]);

                }


                dtadd["Remother"] = "0";
                DataTable dtper = (DataTable)select(" Select distinct SalaryRemuneration.Rate as per, SalaryRemuneration.perunitname,totalunit,Actualpayunit as baseamt,remamt as Totamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as Remuname,Totalcomunitmonth as perof,maxcompete as baseamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Is Percentage') and SalaryMasterId='" + SalId + "' order by Id ASC");
                foreach (DataRow grd in dtper.Rows)
                {
                    dtadd["Remother"] = Convert.ToDecimal(dtadd["Remother"]) + Convert.ToDecimal(grd["Totamt"]);
                }
                DataTable dt7 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as per, SalaryRemuneration.perunitname,totalunit,Actualpayunit as baseamt,remamt as Totamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as Remuname,Totalcomunitmonth as perof,maxcompete as baseamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Is Percentage of Sales') and SalaryMasterId='" + SalId + "' order by Id ASC");

                foreach (DataRow grd in dt7.Rows)
                {
                    dtadd["Remother"] = Convert.ToDecimal(dtadd["Remother"]) + Convert.ToDecimal(grd["Totamt"]);
                }
                DataTable dt8 = (DataTable)select(" Select distinct SalaryRemuneration.Rate as txtleaverate, SalaryRemuneration.perunitname ,totalunit,Actualpayunit as perleaveno,remamt as Totamt,  SalaryRemuneration.Id,SalaryRemuneration.RemunerationName as LeaveType,Totalcomunitmonth as perof,maxcompete as baseamt from SalaryRemuneration LEFT Join RemunerationMaster on RemunerationMaster.Id=SalaryRemuneration.RemunerationId where  SalaryRemuneration.perunitname in('Leave Encash') and SalaryMasterId='" + SalId + "' order by Id ASC");

                foreach (DataRow grd in dt8.Rows)
                {
                    dtadd["Remother"] = Convert.ToDecimal(dtadd["Remother"]) + Convert.ToDecimal(grd["Totamt"]);
                }
                DataTable dttben = (DataTable)select(" Select distinct SalaryTaxableBenifit.Id,TaxableBenName as Taxablebenifitname,TaxbenDate as Date, TaxbenAmt as Amount from  SalaryTaxableBenifit where  SalaryMasterId='" + SalId + "'");

                foreach (DataRow grd in dttben.Rows)
                {
                    dtadd["Remother"] = Convert.ToDecimal(dtadd["Remother"]) + Convert.ToDecimal(grd["Amount"]);
                }

                dtadd["remunit"] = "0";

                dtadd["Ded1"] = Convert.ToString(item["NonGovdedamt"]);
                dtadd["Ded2"] = Convert.ToString(item["Govdedamt"]);
                if (Convert.ToString(item["NonGovdedamt"]) == "")
                {

                    dtadd["Ded1"] = "0";
                }
                if (Convert.ToString(item["Govdedamt"]) == "")
                {

                    dtadd["Ded2"] = "0";
                }
                dtadd["TotalDed"] = Convert.ToString(Convert.ToDecimal(dtadd["Ded1"]) + Convert.ToDecimal(dtadd["Ded2"]));

                dtadd["Remtotal"] = Convert.ToString(item["GrossRemu"]);

                // dtadd["TotalDed"] = txttotded.Text;
                dtadd["remnetamt"] = Convert.ToString(item["NetTotal"]); ;
                //dtadd["PAMT"] = "0";


                dttotal.Rows.Add(dtadd);

            }
        }

        ViewState["ISFill"] = dttotal;

        if (dttotal.Rows.Count > 0)
        {
            fillgd(dttotal);

        }
    }
    protected void fillgd(DataTable dttotal)
    {
        DataView myDataView = new DataView();
        myDataView = dttotal.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        grdallemp.DataSource = myDataView;
        grdallemp.DataBind();
        btnck();
        if (dttotal.Rows.Count > 5)
        {
            pnlGrdemp.Height = new Unit("220px");
        }
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
    protected void grdallemp_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        DataTable dttotal = (DataTable)ViewState["ISFill"];
        if (dttotal.Rows.Count > 0)
        {
            fillgd(dttotal);
        }
    }
    protected void lblrateC_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");
        string te = "EmployeeSalaryMaster.aspx?Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void lblActunit_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");
        string te = "EmployeeAttendancereportdetail.aspx?Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void lblSlip_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        string salid = grdallemp.DataKeys[rinrow].Value.ToString();

        Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");
        string te = "MyPaySlip.aspx?SL=" + salid + "&Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void btnck()
    {

        decimal footremunit = 0;
        decimal footremamt = 0;

        decimal foototherrem = 0;
        decimal footRemtotal = 0;
        decimal footnongovttotded = 0;
        decimal footgovtotalded = 0;
        decimal footnetpay = 0;

        foreach (GridViewRow item in grdallemp.Rows)
        {

            LinkButton lblnetamt = (LinkButton)item.FindControl("lblnetamt");
            LinkButton lblded2 = (LinkButton)item.FindControl("lblded2");
            LinkButton lblded1 = (LinkButton)item.FindControl("lblded1");
            LinkButton lbltotrem = (LinkButton)item.FindControl("lbltotrem");
            LinkButton lblotherrem = (LinkButton)item.FindControl("lblotherrem");


            LinkButton lblremamt = (LinkButton)item.FindControl("lblremamt");
            LinkButton lblactunit = (LinkButton)item.FindControl("lblactunit");




            footremunit += Convert.ToDecimal(lblactunit.Text);
            footremamt += Convert.ToDecimal(lblremamt.Text);

            if (lblotherrem.Text.Length > 0)
            {
                foototherrem += Convert.ToDecimal(lblotherrem.Text);
            }
            footRemtotal += Convert.ToDecimal(lbltotrem.Text);
            if (lblded1.Text.Length > 0)
            {
                footnongovttotded += Convert.ToDecimal(lblded1.Text);
            }
            if (lblded2.Text.Length > 0)
            {
                footgovtotalded += Convert.ToDecimal(lblded2.Text);
            }

            footnetpay += Convert.ToDecimal(lblnetamt.Text);

        }
        Label lblfootremunit = (Label)grdallemp.FooterRow.FindControl("lblfootremunit");
        Label lblfootremamt = (Label)grdallemp.FooterRow.FindControl("lblfootremamt");
        Label lblfoototherrem = (Label)grdallemp.FooterRow.FindControl("lblfoototherrem");

        Label lblfootRemtotal = (Label)grdallemp.FooterRow.FindControl("lblfootRemtotal");
        Label lblfootnongovttotded = (Label)grdallemp.FooterRow.FindControl("lblfootnongovttotded");
        Label lblfootgovtotalded = (Label)grdallemp.FooterRow.FindControl("lblfootgovtotalded");
        Label lblfootnetpay = (Label)grdallemp.FooterRow.FindControl("lblfootnetpay");



        lblfootremunit.Text = String.Format("{0:n}", footremunit);
        lblfootremamt.Text = String.Format("{0:n}", footremamt);

        lblfoototherrem.Text = String.Format("{0:n}", foototherrem);
        lblfootRemtotal.Text = String.Format("{0:n}", footRemtotal);
        lblfootnongovttotded.Text = String.Format("{0:n}", footnongovttotded);
        lblfootgovtotalded.Text = String.Format("{0:n}", footgovtotalded);
        lblfootnetpay.Text = String.Format("{0:n}", footnetpay);



    }
    public DataTable TotalCalc()
    {
        DataTable dtTemp = new DataTable();


        DataColumn peid = new DataColumn();
        peid.ColumnName = "EmployeeId";
        peid.DataType = System.Type.GetType("System.String");
        peid.AllowDBNull = true;
        dtTemp.Columns.Add(peid);


        DataColumn prdem = new DataColumn();
        prdem.ColumnName = "EmployeeName";
        prdem.DataType = System.Type.GetType("System.String");
        prdem.AllowDBNull = true;
        dtTemp.Columns.Add(prdem);

        DataColumn prd = new DataColumn();
        prd.ColumnName = "Remorig";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "remunit";
        ssCatId.DataType = System.Type.GetType("System.Decimal");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);


        DataColumn catname = new DataColumn();
        catname.ColumnName = "remrate";
        catname.DataType = System.Type.GetType("System.String");
        catname.AllowDBNull = true;
        dtTemp.Columns.Add(catname);


        DataColumn completemonth = new DataColumn();
        completemonth.ColumnName = "remamt";
        completemonth.DataType = System.Type.GetType("System.Decimal");
        completemonth.AllowDBNull = true;
        dtTemp.Columns.Add(completemonth);



        DataColumn remperunit = new DataColumn();
        remperunit.ColumnName = "remsalesamt";
        remperunit.DataType = System.Type.GetType("System.Decimal");
        remperunit.AllowDBNull = true;
        dtTemp.Columns.Add(remperunit);

        DataColumn remperunitId = new DataColumn();
        remperunitId.ColumnName = "Remother";
        remperunitId.DataType = System.Type.GetType("System.Decimal");
        remperunitId.AllowDBNull = true;
        dtTemp.Columns.Add(remperunitId);

        DataColumn dedother = new DataColumn();
        dedother.ColumnName = "Remtotal";
        dedother.DataType = System.Type.GetType("System.Decimal");
        dedother.AllowDBNull = true;
        dtTemp.Columns.Add(dedother);


        DataColumn dedotherId = new DataColumn();
        dedotherId.ColumnName = "Ded1";
        dedotherId.DataType = System.Type.GetType("System.Decimal");
        dedotherId.AllowDBNull = true;
        dtTemp.Columns.Add(dedotherId);

        DataColumn Dedmulti = new DataColumn();
        Dedmulti.ColumnName = "Ded2";
        Dedmulti.DataType = System.Type.GetType("System.Decimal");
        Dedmulti.AllowDBNull = true;
        dtTemp.Columns.Add(Dedmulti);



        DataColumn Dedmultiv1 = new DataColumn();
        Dedmultiv1.ColumnName = "Actunit";
        Dedmultiv1.DataType = System.Type.GetType("System.Decimal");
        Dedmultiv1.AllowDBNull = true;
        dtTemp.Columns.Add(Dedmultiv1);



        DataColumn remnetamt = new DataColumn();
        remnetamt.ColumnName = "TotalDed";
        remnetamt.DataType = System.Type.GetType("System.Decimal");
        remnetamt.AllowDBNull = true;
        dtTemp.Columns.Add(remnetamt);

        DataColumn remptotamt = new DataColumn();
        remptotamt.ColumnName = "remnetamt";
        remptotamt.DataType = System.Type.GetType("System.Decimal");
        remptotamt.AllowDBNull = true;
        dtTemp.Columns.Add(remptotamt);



        DataColumn remper = new DataColumn();
        remper.ColumnName = "SalId";
        remper.DataType = System.Type.GetType("System.String");
        remper.AllowDBNull = true;
        dtTemp.Columns.Add(remper);

        DataColumn reap = new DataColumn();
        reap.ColumnName = "repa";
        reap.DataType = System.Type.GetType("System.Boolean");
        reap.AllowDBNull = true;
        dtTemp.Columns.Add(reap);

        DataColumn rep = new DataColumn();
        rep.ColumnName = "repab";
        rep.DataType = System.Type.GetType("System.Boolean");
        rep.AllowDBNull = true;
        dtTemp.Columns.Add(rep);

        return dtTemp;
    }
    protected void sugnature()
    {
        SqlDataAdapter da = new SqlDataAdapter("select signature from EmailSignatureMaster inner join InOutCompanyEmail on InOutCompanyEmail.ID=EmailSignatureMaster.InoutgoingMasterId where InOutCompanyEmail.EmployeeID='" + Session["EmployeeId"].ToString() + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["signature"]) != "")
            {
                ViewState["sign"] = Convert.ToString(dt.Rows[0]["signature"]);
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

            if (ds123.Rows.Count > 0)
            {

                if (ds123.Rows[0]["day"].ToString() == "Monday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (ds123.Rows[0]["day"].ToString() == "Tuesday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (ds123.Rows[0]["day"].ToString() == "Wednesday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (ds123.Rows[0]["day"].ToString() == "Thursday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (ds123.Rows[0]["day"].ToString() == "Friday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (ds123.Rows[0]["day"].ToString() == "Saturday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (ds123.Rows[0]["day"].ToString() == "Sunday")
                {
                    dsday = select("select BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ViewState["Whid"] + "' and BatchTiming.BatchMasterId='" + ViewState["Bid"] + "' ");

                }
                if (dsday.Rows.Count == 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "You are not allowed to add your attendance, as today is not a working day in your scheduled batch.";

                }

            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You are not allowed to add your attendance, as today is not a working day in your scheduled batch.";

            }
        }
        return dsday;
    }
    protected void fillgrid()
    {


        string strdate = "";
        string strpera = "";

        //if (ddlbatch.SelectedIndex > 0)
        //{
        //    strpera += " and EmployeeBatchMaster.Batchmasterid='" + ddlbatch.SelectedValue + "' ";
        //}

        //if ( lblcdate.Text != "")
        //{
        strdate = strpera + "  and DateMasterTbl.Date between '" + System.DateTime.Now.ToShortDateString() + "' and '" + System.DateTime.Now.ToShortDateString() + "' ";
        // }
        //if (ddlpresentstatus.SelectedValue == "3")
        //{
        //    //str += " and (AttendenceEntryMaster.LateInMinuts >= '00:00' or AttendenceEntryMaster.OutInMinuts >='00:00') ";
        //    strpera += " and  (AttendenceEntryMaster.InTimeforcalculation > AttendenceEntryMaster.InTime)  ";

        //}
        //if (ddlpresentstatus.SelectedValue == "4")
        //{
        //    strpera += "  and  (AttendenceEntryMaster.InTimeforcalculation < AttendenceEntryMaster.InTime) ";
        //}
        //if (ddlpresentstatus.SelectedValue == "5")
        //{
        //    //str += " and (AttendenceEntryMaster.LateInMinuts >= '00:00' or AttendenceEntryMaster.OutInMinuts >='00:00') ";
        //    strpera += " and (AttendenceEntryMaster.OutTimeforcalculation > AttendenceEntryMaster.OutTime) ";

        //}

        //if (ddlpresentstatus.SelectedValue == "6")
        //{
        //    strpera += "  and  (AttendenceEntryMaster.OutTimeforcalculation < AttendenceEntryMaster.OutTime) ";
        //}


        DataTable dtf = new DataTable();
        dtf = CreateDatatable();
        DataTable ds1 = select("SELECT distinct Convert(nvarchar, DateMasterTbl.Date,101) as Datet,DateMasterID as DateId FROM EmployeeMaster  " +
     " INNER JOIN  dbo.EmployeeBatchMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.EmployeeBatchMaster.Employeeid  inner join BatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join BatchWorkingDays on BatchWorkingDays.BatchID=BatchMaster.Id inner join DateMasterTbl" +
        " on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID  where  BatchMaster.Whid='" + ViewState["Whid"] + "'" + strdate + " order by DateId ");
        foreach (DataRow dtr in ds1.Rows)
        {
            DataTable dt2 = select("SELECT DISTINCT Party_master.Compname as GTA,Left(BatchTiming.StartTime,5) as StartTime,Left(BatchTiming.EndTime,5) as EndTime, dbo.AttandanceEntryNotes.OutTimeNote, dbo.AttandanceEntryNotes.IntimeNote,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Varify," +
                        " dbo.AttendenceEntryMaster.EmployeeID, CONVERT(Nvarchar, dbo.AttendenceEntryMaster.Date, 101) AS Date, dbo.AttendenceEntryMaster.InTime," +
                       " dbo.AttendenceEntryMaster.InTimeforcalculation, dbo.AttendenceEntryMaster.OutTime, dbo.AttendenceEntryMaster.OutTimeforcalculation," +
                       " Left(AttendenceEntryMaster.LateInMinuts,5) as LateInMinuts, Left(AttendenceEntryMaster.OutInMinuts,5) as OutInMinuts , dbo.AttendenceEntryMaster.TotalHourWork,Left(FirstName,1)+'.'+Left(LastName,13) as EmployeeName, dbo.EmployeeMaster.Whid, dbo.EmployeeBatchMaster.Batchmasterid,BatchMaster.Name as BatchName  FROM" +
                        " AttandanceEntryNotes Right join AttendenceEntryMaster ON dbo.AttendenceEntryMaster.AttendanceId = dbo.AttandanceEntryNotes.AttendanceId " +
                       " Right  join EmployeeMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  and  AttendenceEntryMaster.Date='" + Convert.ToDateTime(dtr["Datet"]).ToShortDateString() + "' and  EmployeeMaster.Whid='" + ViewState["Whid"] + "' and  EmployeeMaster.Employeemasterid='" + ViewState["empid"] + "' " +
                      " INNER JOIN  dbo.EmployeeBatchMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.EmployeeBatchMaster.Employeeid inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterId  inner join BatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join BatchTiming on BatchTiming.BatchMasterId=BatchMaster.Id and BatchMaster.Whid='" + ViewState["Whid"] + "' Left join GatepassTBL on GatepassTBL.EmployeeID=EmployeeMaster.EmployeeMasterId  left join GatepassDetails  on  GatepassDetails.GatePassID=GatepassTBL.Id left join Party_master on Party_master.PartyId=GatepassDetails.PartyID and GatepassTBL.EmployeeID='" + ViewState["empid"] + "' and Cast(GatepassTBL.Date as Date)='" + Convert.ToDateTime(dtr["Datet"]).ToShortDateString() + "' " + strpera + " order by EmployeeName");




            foreach (DataRow dtp in dt2.Rows)
            {
                string staadd = "";
                DataRow Drow = dtf.NewRow();

                staadd = "ok";

                //else if (ddlpresentstatus.SelectedValue == "1")
                //{
                //    if (Convert.ToString(dtp["AttendanceId"]) == "")
                //    {
                //        staadd = "ok";
                //    }
                //}
                //else if (ddlpresentstatus.SelectedValue == "2")
                //{
                //    if (Convert.ToString(dtp["AttendanceId"]) != "")
                //    {
                //        staadd = "ok";
                //    }
                //}

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
                    Drow["GTA"] = "";
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
                        if (Convert.ToString(dtp["GTA"]) != "")
                        {
                            Drow["GTA"] = "Yes - " + Convert.ToString(dtp["GTA"]);
                        }
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
                        Drow["Status"] = "present";

                        if (Convert.ToString(dtp["OutInMinuts"]) == "")
                        {
                            if (Convert.ToString(dtp["LateInMinuts"]) != "")
                            {
                                bool allcomp = Convert.ToString(dtp["LateInMinuts"]).Contains("-");
                                if (allcomp == true)
                                {
                                    Drow["Status"] = "late in";
                                }
                                allcomp = Convert.ToString(dtp["LateInMinuts"]).Contains("+");
                                if (allcomp == true)
                                {
                                    Drow["Status"] = "early in";
                                }
                            }
                        }
                    }
                    else
                    {
                        Drow["OutTime"] = Convert.ToString(dtp["EndTime"]);
                        Drow["InTime"] = Convert.ToString(dtp["StartTime"]);
                        Drow["Status"] = "absent";
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
        pnlscrollatt.ScrollBars = ScrollBars.None;
        pnlscrollatt.Height = new Unit();

        if (gridattendance.Rows.Count > 4)
        {
            pnlscrollatt.ScrollBars = ScrollBars.Vertical;
            pnlscrollatt.Height = new Unit("200px");
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

        DataColumn dcGTA = new DataColumn();
        dcGTA.DataType = System.Type.GetType("System.String");
        dcGTA.ColumnName = "GTA";
        dcGTA.AllowDBNull = true;
        dcGTA.Unique = false;
        dcGTA.ReadOnly = false;

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
        dt.Columns.Add(dcGTA);
        return dt;
    }
    protected void fillbatch()
    {
        //ddlbatch.Items.Clear();
        //string str = "SELECT * from BatchMaster where WHID='" + ViewState["Whid"] + "'";

        //SqlCommand cmdwh = new SqlCommand(str, con);
        //SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        //DataTable dtwh = new DataTable();
        //adpwh.Fill(dtwh);
        //if (dtwh.Rows.Count > 0)
        //{

        //    ddlbatch.DataSource = dtwh;
        //    ddlbatch.DataTextField = "Name";
        //    ddlbatch.DataValueField = "ID";
        //    ddlbatch.DataBind();
        //}
        //ddlbatch.Items.Insert(0, "All");
        // ddlbatch.Items[0].Value = "0";
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


    protected void btnin_Click(object sender, EventArgs e)
    {
        makeattendance();
        filldate();
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

        ViewState["Compid"] = Session["Comid"];


        string ipaddress = "";
        DateTime datet = DateTime.Now.AddMinutes(-10);
        ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();

        //int flag = rest();

        //if (flag == 1)
        //{

        ViewState["ipdel111"] = "IpAddress='" + ipaddress + "' and  Compid='" + Convert.ToString(ViewState["Compid"]) + "' and   CAST(Datetime as  Datetime) Between   CAST('" + datet + "'as Datetime) and  CAST('" + DateTime.Now.ToString() + "' as Datetime)";
        string stripa = "select * from  [AttandanceTempBanIpTable] where " + ViewState["ipdel111"];
        SqlCommand cmdipa = new SqlCommand(stripa, con);
        SqlDataAdapter adpipa = new SqlDataAdapter(cmdipa);
        DataTable dsipa = new DataTable();
        adpipa.Fill(dsipa);
        if (dsipa.Rows.Count < 3)
        {
            string sxd = "select Distinct EmployeeMaster.Whid, CompanyMaster.CompanyName,CompanyMaster.Compid,WareHouseMaster.Name, EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterID,BatchMaster.Name as Bid,BatchMaster.Id from BatchMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join WareHouseMaster on  WareHouseMaster.WarehouseId=EmployeeMaster.whid inner join CompanyMaster on CompanyMaster.compid=WareHouseMaster.comid  where  EmployeeMaster.EmployeeMasterID ='" + ViewState["empid"] + "'";
            SqlDataAdapter adp = new SqlDataAdapter(sxd, con);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                ViewState["Whid"] = ds.Rows[0]["Whid"].ToString();
                ViewState["Compid"] = ds.Rows[0]["Compid"].ToString();
                ViewState["Bid"] = ds.Rows[0]["Id"].ToString();
                timecalculate();
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
                        string stra = "SELECT BatchTiming.Totalhours,BatchTiming.StartTime,BatchTiming.EndTime FROM BatchTiming where BatchMasterId='" + ViewState["Bid"] + "'";
                        SqlDataAdapter adpa = new SqlDataAdapter(stra, con);
                        DataTable dt = new DataTable();
                        adpa.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            TimeSpan tt;
                            TimeSpan tto = TimeSpan.Parse(time22.Text);
                            TimeSpan tt1 = TimeSpan.Parse(dt.Rows[0]["EndTime"].ToString());
                            tt = tto.Subtract(new TimeSpan(rulegreatertime, 0, 0));
                            string ori = tt1.CompareTo(tt).ToString();
                            string comparesame = tto.CompareTo(tt).ToString();
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
                                    "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + ds.Rows[0]["EmployeeMasterID"] + "') and (AttendenceEntryMaster.OutTimeforcalculation='00:00') and (AttendenceEntryMaster.Date='" + serdate + "') and Batchmasterid='" + ViewState["Bid"] + "'";
                            }
                            else
                            {
                                strapp = "SELECT Distinct EmployeeMaster.EmployeeMasterID,AttendenceEntryMaster.InTimeforcalculation,AttendenceEntryMaster.InTime,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Date,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.InTime " +
                                   "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + ds.Rows[0]["EmployeeMasterID"] + "') and (AttendenceEntryMaster.OutTimeforcalculation='00:00')and (AttendenceEntryMaster.Outtime<='" + tt.ToString().Remove(5, 3) + "') and (AttendenceEntryMaster.Date='" + serdate + "') and Batchmasterid='" + ViewState["Bid"] + "'";

                            }
                        }
                    }
                }
                else
                {
                    strapp = "SELECT Distinct EmployeeMaster.EmployeeMasterID,AttendenceEntryMaster.InTimeforcalculation,AttendenceEntryMaster.InTime,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Date,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.InTime " +
                     "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + ds.Rows[0]["EmployeeMasterID"] + "') and (AttendenceEntryMaster.OutTimeforcalculation='00:00') and (AttendenceEntryMaster.Date='" + Label1date.Text + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(1).ToShortDateString() + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(-1).ToShortDateString() + "') and Batchmasterid='" + ViewState["Bid"] + "'";
                }

                //string str = "SELECT Distinct EmployeeMaster.EmployeeMasterID,AttendenceEntryMaster.InTimeforcalculation,AttendenceEntryMaster.InTime,AttendenceEntryMaster.AttendanceId,AttendenceEntryMaster.Date,AttendenceEntryMaster.OutTime,AttendenceEntryMaster.InTime " +
                // "FROM  EmployeeMaster  inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID   inner join AttendenceEntryMaster on EmployeeMaster.EmployeeMasterID=AttendenceEntryMaster.EmployeeID  Where (EmployeeMasterID='" + ds.Rows[0]["Employee_Id"] + "') and (AttendenceEntryMaster.OutTimeforcalculation='00:00') and (AttendenceEntryMaster.Date='" + Label1date.Text + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(1).ToShortDateString() + "' or  AttendenceEntryMaster.Date='" + Convert.ToDateTime(Label1date.Text).AddDays(-1).ToShortDateString() + "') and Batchmasterid='" + ViewState["Bid"] + "'";
                SqlDataAdapter adpl = new SqlDataAdapter(strapp, con);
                DataTable dsl = new DataTable();
                adpl.Fill(dsl);
                if (dsl.Rows.Count > 0)
                {
                    Session["AttendanceId"] = dsl.Rows[0]["AttendanceId"];
                    matchbarlogout();
                    //lblgoemp.Text = ds.Rows[0]["EmployeeName"].ToString() + " : " + ds.Rows[0]["Employee_Id"];
                    //lblexittime.Text = time22.Text;
                    //Label22.Text = dsl.Rows[0]["InTimeforcalculation"].ToString();
                    Company();
                }
                else
                {
                    string strr = "SELECT Top(1)  OutTimeforcalculation FROM AttendenceEntryMaster where EmployeeID='" + ds.Rows[0]["EmployeeMasterID"] + "' order by AttendanceId Desc ";
                    SqlDataAdapter adpll = new SqlDataAdapter(strr, con);
                    DataTable dsll = new DataTable();
                    adpll.Fill(dsll);
                    if (dsll.Rows.Count > 0)
                    {
                        //lbllastexittime.Text = dsll.Rows[0]["OutTimeforcalculation"].ToString();
                    }
                    matchbarlogin();

                    Company();
                    //lblempname.Text = ds.Rows[0]["EmployeeName"].ToString() + " : " + ds.Rows[0]["Employee_Id"]; ;
                    // Label7.Text = time22.Text;

                }
                //lblbarmsg.Text = "";
                ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string str3 = "insert into AttandanceLog(IpAddress,Barcode,Comid,Successfull,Datetime,Empid)values('" + ipaddress + "','" + ViewState["barcode"] + "','" + ViewState["Compid"] + "','1','" + DateTime.Now.ToString() + "','" + ViewState["empid"] + "')";
                if (con.State.ToString() == "Open")
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd1 = new SqlCommand(str3, con);

                cmd1.ExecuteNonQuery();
                con.Close();
                //lblbarmsg.Visible = false;
                // lblbarmsg.Text = "";
                //lblbarmsg.Visible = true;
                //timer1.Enabled = true;
            }
            else
            {
                ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                if (con.State.ToString() == "Open")
                {
                    con.Close();
                }
                string str3 = "insert into AttandanceLog(IpAddress,Barcode,Comid,Successfull,Datetime)values('" + ipaddress + "','" + ViewState["barcode"] + "','" + ViewState["Compid"] + "','0','" + DateTime.Now.ToString() + "')";
                con.Open();
                SqlCommand cmd1 = new SqlCommand(str3, con);
                //dr.Close();
                cmd1.ExecuteNonQuery();
                string str31 = "insert into AttandanceTempBanIpTable(IpAddress,Compid,Datetime)values('" + ipaddress + "','" + ViewState["Compid"] + "','" + DateTime.Now.ToString() + "')";
                //dr.Close();
                SqlCommand cmd11 = new SqlCommand(str31, con);
                //dr.Close();
                cmd11.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry we can not find your barcode in our system,</br> please try again with correct id card. If you are new employee,</br>check with your supervisor whether your barcode is updated in the system.";
                //lblbarmsg.Text = "No Match Barcode No....";
                // lblbarmsg.Visible = true;
            }
        }
        else
        {
            //lblmsg3.Text = "";
            //timer2.Enabled = true;
            // ModalPopupExtender3.Show();
        }
        //}
        //if (dsipa.Rows.Count < 3) end
        //else
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Sorry,you have not allowed IP :" + ipaddress;
        //    //lblbarmsg.Visible = true;
        //}
        //txtbartext.Text = "";
        // txtbartext.Focus();        
    }
    private void Company()
    {
        string str1 = "select CompanyName from CompanyMaster where [Compid]='" + ViewState["Compid"] + "'";
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                string str2 = dr["CompanyName"].ToString();
                //CompanyName.Text = str2;
                //lblm.Text = str2;
            }
        }
        dr.Close();
        con.Close();

    }
    protected void matchbarlogin()
    {
        string indifftime = "";


        string str = "SELECT EmployeeMaster.EmployeeMasterID, BatchTiming.Totalhours, EmployeeMaster.EmployeeName,BatchTiming.StartTime,BatchTiming.EndTime, User_master.UserId, Login_master.username,Login_master.password" +
                      " FROM BatchTiming inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchTiming.BatchMasterId inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID  inner join User_Master on  EmployeeMaster.PartyID = User_Master.PartyID inner join Login_master on Login_master.UserID=User_Master.UserID and  EmployeeMaster.EmployeeMasterID=" + ViewState["empid"] + "";

        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        con.Close();
        adp.Fill(dt);
        con.Open();
        if (dt.Rows.Count != 0)
        {
            string str2 = dt.Rows[0]["UserName"].ToString();
            string str4 = Label1date.Text;

            TimeSpan t1 = TimeSpan.Parse(dt.Rows[0]["StartTime"].ToString());
            TimeSpan t2 = TimeSpan.Parse(time22.Text);


            int msgshow = 0;
            int devnoentryforregistercount = 0;
            // lblempname.Text = dt.Rows[0]["EmployeeName"].ToString();
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
                        //Label13.Text = "You are late by :";
                        //Label14.Text = indifftime;
                        //lbllate.Visible = true;
                        //Label9.Visible = true;
                        latecout = 2;
                        countdevEntry = 2;


                    }
                    else
                    {

                        indifftime = "00:00";
                        TimeSpan SPT = TimeSpan.Parse(dt.Rows[0]["StartTime"].ToString());
                        intime = Convert.ToDateTime(SPT).ToString("HH:mm");
                        // Label13.Text = "";
                        // Label14.Text = "";
                    }
                }
                else
                {
                    indifftime = "-" + indifftime;
                    //Label13.Text = "You are late by :";
                    // Label14.Text = indifftime;
                    latecout = 2;
                    //lbllate.Visible = true;



                }
            }
            else
            {
                indifftime = t1.Subtract(t2).ToString();
                indifftime = Convert.ToDateTime(indifftime).ToString("HH:mm");
                indifftime = "+" + indifftime;
                // Label13.Text = "You are early by :";
                // Label14.Text = indifftime;

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
                ViewState["payid"] = "0";
                countdevnoentryforregister = filldeventry(devnoentryforregistercount);

            }

            if (countdevnoentryforregister != 2)
            {

                TimeSpan SPT1 = TimeSpan.Parse(dt.Rows[0]["StartTime"].ToString());
                string oritime = SPT1.ToString().Remove(5, SPT1.ToString().Length - 5);
                TimeSpan SPT11 = TimeSpan.Parse(dt.Rows[0]["EndTime"].ToString());
                string oriouttime = SPT11.ToString().Remove(5, SPT11.ToString().Length - 5);
                bool halfloeave = false;
                bool fullleave = false;
                if (countdevEntry == 2 || latecout == 2)
                {
                    TimeSpan t7 = TimeSpan.Parse(dt.Rows[0]["StartTime"].ToString());
                    TimeSpan t8 = TimeSpan.Parse(dt.Rows[0]["EndTime"].ToString());
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
                    string[] strSplitArrbm = dt.Rows[0]["Totalhours"].ToString().Split(separbm, StringSplitOptions.RemoveEmptyEntries);
                    decimal bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)) / 2);


                    string batchtothour = "";
                    TimeSpan tt = TimeSpan.Parse(time22.Text);
                    TimeSpan tt1 = TimeSpan.Parse(dt.Rows[0]["EndTime"].ToString());
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



                string str3 = "insert into AttendenceEntryMaster(EmployeeID,Date,InTime,InTimeforcalculation,OutTime,OutTimeforcalculation,LateInMinuts,Outtimedate,Varify,ConsiderHalfDayLeave,ConsiderFullDayLeave,BatchRequiredhours,Overtime)values(" + ViewState["empid"] + ",'" + Label1date.Text + "','" + oritime + "','" + intime + "','" + oriouttime + "','00:00','" + indifftime + "','" + Label1date.Text + "','" + countingdevapprov + "','" + halfloeave + "','" + fullleave + "','" + dt.Rows[0]["Totalhours"] + "','" + overtime + "')";

                //dr.Close();
                SqlCommand cmd1 = new SqlCommand(str3, con);
                //dr.Close();
                cmd1.ExecuteNonQuery();
                AttandanceEntryNotes();
                if (countdevEntry == 2)
                {

                    AttendenceDeviations(indifftime, "00:00");
                    if (countdevmessage == 2)
                    {
                        // lbllatemessagereason.Text = "In";
                        msgshow = 1;
                        // timer1.Enabled = false;
                        ViewState["devap"] = devapproved;
                        // ModalPopupExtender4.Show();
                    }
                    if (siniourcount == 2)
                    {
                        // lbllate.Text = "Sorry,Your attendence not approved,contact supervisor/admin and approved that.";
                        // lbllate.Visible = true;
                        // lblentry.Visible = true;
                        // lblentry.Text = "Sorry,Your attendence not approved,contact supervisor/admin and approved that.";
                        //  Label8.Visible = true;
                        // lbllate.Visible = false;
                        // Label9.Visible = false;
                    }

                }
                con.Close();


                //Label8.Visible = false;


                //lblwelcome.Visible = true;
                if (msgshow == 0)
                {
                    //ModalPopupExtender1.Show();
                    //timer1.Enabled = true;
                }

            }
            else
            {
                //lblentry.Text = "Do not allow employee to make entry in register for larger then rules for payperiod";
                // Label8.Visible = true;
                // lbllate.Visible = false;
                // Label9.Visible = false;
                // lblentry.Visible = true;
            }
        }
    }

    protected int filldeventry(int devrulesno)
    {
        int i = 0;
        ViewState["payid"] = "0";
        string str2 = "";
        DataTable dt = select("Select  payperiodMaster.ID, payperiodMaster.PayperiodStartDate,payperiodMaster.PayperiodEndDate from payperiodtype inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id where EmpId='" + Session["EmployeeID1"] + "' and PayperiodStartDate<='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "' and PayperiodEndDate>='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "'");

        if (dt.Rows.Count > 0)
        {
            ViewState["payid"] = dt.Rows[0]["ID"];
            str2 = "  select Count(Id) as Id from AttendenceDeviations where PayPeriodID='" + dt.Rows[0]["ID"] + "' and EmployeeID='" + Session["EmployeeID1"] + "'";
        }
        else
        {
            str2 = "  select Count(Id) as Id from AttendenceDeviations where PayPeriodID='0' and EmployeeID='" + Session["EmployeeID1"] + "'";
            SqlDataAdapter adp2 = new SqlDataAdapter(str2, con);
            DataTable dt1 = new DataTable();

            adp2.Fill(dt1);
            if (Convert.ToInt32(dt1.Rows[0]["Id"]) >= devrulesno)
            {
                i = 2;
            }
        }
        return i;
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

        DataTable dtr = select("Select  payperiodMaster.ID, payperiodMaster.PayperiodStartDate,payperiodMaster.PayperiodEndDate from payperiodtype inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id where EmpId='" + ViewState["empid"] + "' and PayperiodStartDate<='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "' and PayperiodEndDate>='" + Convert.ToDateTime(Label1date.Text).ToShortDateString() + "'");

        if (dtr.Rows.Count > 0)
        {
            string str3 = "insert into AttendenceDeviations(PayPeriodID,DeviationDate,EmployeeID,intimedeviationminutes,outtimedeviationminutes,attandanceId)values('" + dtr.Rows[0]["Id"] + "','" + DateTime.Now.ToShortDateString() + "','" + ViewState["empid"] + "','" + intimedif + "','" + outdiff + "','" + ViewState["atid"] + "')";
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
        else
        {
            string str3 = "insert into AttendenceDeviations(PayPeriodID,DeviationDate,EmployeeID,intimedeviationminutes,outtimedeviationminutes,attandanceId)values('0','" + DateTime.Now.ToShortDateString() + "','" + ViewState["empid"] + "','" + intimedif + "','" + outdiff + "','" + ViewState["atid"] + "')";
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
    protected void matchbarlogout()
    {
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
                        // Label16.Text = "You are early exit time :";
                        // lblouterly.Text = outdifftime;
                        // Label18.Visible = true;
                        // Label20.Visible = true;
                        latecout = 2;
                    }
                    else
                    {
                        outdifftime = "00:00";
                        //Label13.Text = "";
                        // Label14.Text = "";

                    }
                }
                else
                {
                    outdifftime = Convert.ToDateTime(outdifftime).ToString("HH:mm");
                    outdifftime = "-" + outdifftime;
                    //Label16.Text = "You are early exit time :";
                    //lblouterly.Text = outdifftime;
                    //Label18.Visible = true;
                    //Label20.Visible = true;
                    latecout = 2;
                }
            }
            else
            {
                outdifftime = t4.Subtract(t3).ToString();

                outdifftime = Convert.ToDateTime(outdifftime).ToString("HH:mm");
                outdifftime = "+" + outdifftime;
                //Label16.Text = "You are late exit time :";
                //lblouterly.Text = outdifftime;
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
                ViewState["payid"] = "0";
                countdevnoentryforregister = filldeventry(devnoentryforregistercount);

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

                    string[] separbm = new string[] { ":" };
                    string[] strSplitArrbm = dt.Rows[0]["Totalhours"].ToString().Split(separbm, StringSplitOptions.RemoveEmptyEntries);
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
                    inovertime = TimeSpan.Parse(Convert.ToString(dt.Rows[0]["Overtime"]));

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
                filldate();
                if (countdevEntry == 2)
                {

                    AttendenceDeviations("00:00", outdifftime);
                    if (countdevmessage == 2)
                    {
                        // lbllatemessagereason.Text = "Out";
                        msgshow = 1;
                        //  timer1.Enabled = false;
                        // latereaso.Text = "";
                        ViewState["devap"] = devapproved;
                        // ModalPopupExtender4.Show();
                    }
                    if (siniourcount == 2)
                    {
                        // Label20.Text = "Sorry,Your attendence not approved,contact supervisor/admin and approved that.";
                        // Label20.Visible = true;
                        // lblentry.Visible = true;
                        //  lblentry.Text = "Sorry,Your attendence not approved,contact supervisor/admin and approved that.";
                        // Label8.Visible = true;
                        //  lbllate.Visible = false;
                        /// Label9.Visible = false;
                    }

                }
                con.Close();

                // lblempname.Text = dt.Rows[0]["EmployeeName"].ToString();
                // lblentry.Visible = true;
                //Label9.Visible = false;

                if (msgshow == 0)
                {
                    // ModalPopupExtender2.Show();
                    // timer1.Enabled = true;
                }

            }
            else
            {
                // lblentry.Visible = true;
                // lblentry.Text = "Do not allow approval of the senior Employee larger then rules for payperiod";
                //  lblentry.Text = "Do not allow employee to make entry in register for larger then rules for payperiod";

                //  Label9.Visible = true;
            }
        }
    }
    protected void AttandanceEntryNotesupdate()
    {

        string str3 = "update AttandanceEntryNotes set OutTimeNote='' where AttendanceId=" + Session["AttendanceId"] + "";

        SqlCommand cmd1 = new SqlCommand(str3, con);

        cmd1.ExecuteNonQuery();

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
        string str = "select Login_master.UserID,Login_master.username,User_master.UserID,User_master.PartyID," +
                   " EmployeeMaster.EmployeeMasterID,Party_master.PartyID,Party_master.id,EmployeeMaster.SuprviserId " +
                   " from Login_master inner join User_master on Login_master.UserID=User_master.UserID  " +
                   " inner join Party_master on User_master.PartyID=Party_master.PartyID inner join " +
                   " EmployeeMaster on User_master.PartyID=EmployeeMaster.PartyID where Login_master.UserID='" + Session["userid"] + "' and Party_master.id='" + ViewState["Compid"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adpt.Fill(ds);
        ViewState["empid"] = ds.Tables[0].Rows[0]["EmployeeMasterID"].ToString();

        string stratt = "select Top(1) * from AttendenceEntryMaster where EmployeeID='" + ViewState["empid"] + "' and Date='" + System.DateTime.Now.ToShortDateString() + "' order by AttendanceId desc";

        SqlCommand cmdatt = new SqlCommand(stratt, con);
        SqlDataAdapter adpatt = new SqlDataAdapter(cmdatt);
        DataTable dt = new DataTable();
        adpatt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Pnlcnfout.Visible = true;
            lblintime.Text = dt.Rows[0]["InTimeforcalculation"].ToString();
            lbltime.Text = dt.Rows[0]["InTimeforcalculation"].ToString();
            lblout.Text = dt.Rows[0]["OutTimeforcalculation"].ToString();
            if (lblout.Text != "00:00")
            {
                Pnlcnfout.Visible = false;
                pnlintime.Visible = false;
                Pnlout.Visible = true;
            }
            else
            {
                Pnlcnfout.Visible = true;
                pnlintime.Visible = false;
                Pnlout.Visible = false;
            }
            lbldate.Text = System.DateTime.Now.ToShortDateString();
            EventArgs e = new EventArgs();
            object sender = new object();
            btngo_Click(sender, e);
        }
        else
        {
            pnlintime.Visible = true;
            Pnlcnfout.Visible = false;
            Pnlout.Visible = false;

        }
        //lblcdate.Text = System.DateTime.Now.ToShortDateString();
    }

    public void filllapproval()
    {
        grdgatepass.DataSource = null;
        grdgatepass.DataBind();

        DataTable dt3 = select("Select Distinct GatepassTBL.Id,Left(Party_master.Compname,10) as Compname , Left(FirstName,1)+'.'+Left(LastName,13) as EmployeeName,ExpectedOutTime,ExpectedInTime,Approved from EmployeeMaster inner join GatepassTBL on GatepassTBL.EmployeeID=EmployeeMaster.EmployeeMasterId inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterId inner join GatepassDetails   on GatepassDetails.GatePassID=GatepassTBL.Id inner join Party_master on Party_master.PartyId=GatepassDetails.PartyID where GatepassTBL.StoreID='" + ViewState["Whid"] + "' and GatepassTBL.ApprovedEmployeeID='" + ViewState["empid"] + "' order by EmployeeName");
        if (dt3.Rows.Count > 0)
        {
            grdgatepass.DataSource = dt3;
            grdgatepass.DataBind();
        }
        foreach (GridViewRow grd in grdgatepass.Rows)
        {
            Label lblsapp = (Label)grd.FindControl("lblsapp");
            DropDownList ddl = (DropDownList)grd.FindControl("DropDownList18");
            Button btncom = (Button)grd.FindControl("btncom");
            if (lblsapp == null)
            {

            }
            else
            {
                if (lblsapp.Text == "1")
                {
                    lblsapp.Text = "Pending";
                    btncom.Visible = true;
                    ddl.Enabled = true;
                }
                else if (lblsapp.Text == "2")
                {
                    lblsapp.Text = "Approved";
                    ddl.Enabled = false;
                    btncom.Visible = false;
                }
                else if (lblsapp.Text == "3")
                {
                    lblsapp.Text = "Rejected";
                    ddl.Enabled = false;
                    btncom.Visible = false;
                }
                ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(lblsapp.Text));
            }

        }
    }
    public void fillleavee()
    {

        string str = "Select EmployeeHoliday.id,EmployeeHoliday.whid,EmployeeHoliday.employeeid,EmployeeHoliday.fromdate,EmployeeHoliday.Todate,EmployeeHoliday.leavetypeid,EmployeeHoliday.leaveRequestNote,EmployeeHoliday.ApprovalNote,EmployeeHoliday.compid,'Status'=CASE EmployeeHoliday.status WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Rejected'  ELSE  EmployeeHoliday.status END, " +
                    " EmployeeLeaveType.EmployeeLeaveTypeName,EmployeeLeaveType.ID,WareHouseMaster.WareHouseId,WareHouseMaster.Name,EmployeeMaster.EmployeeMasterID,Left(FirstName,1)+'.'+Left(LastName,20) as empname ,EmployeeMaster.SuprviserId, " +
                    "DepartmentmasterMNC.id,DesignationMaster.DesignationMasterId  from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterId left join DepartmentmasterMNC on EmployeeMaster.DeptID= DepartmentmasterMNC.id left join DesignationMaster on EmployeeMaster.DesignationMasterId = DesignationMaster.DesignationMasterId inner join EmployeeHoliday on EmployeeMaster.EmployeeMasterID=EmployeeHoliday.employeeid " +
                    "  inner join EmployeeLeaveType on EmployeeLeaveType.ID = EmployeeHoliday.leavetypeid inner join WareHouseMaster on WareHouseMaster.WareHouseId=EmployeeHoliday.whid where EmployeeHoliday.compid='" + ViewState["Compid"] + "' and EmployeeMaster.SuprviserId='" + ViewState["empid"] + "' ";
        string str1 = "";
        //where EmployeeHoliday.whid='" + ddlstorename.SelectedValue + "'

        str1 = "and EmployeeHoliday.whid = '" + ViewState["Whid"] + "'";


        str1 = str1 + "  and EmployeeHoliday.Todate>='" + DateTime.Now.ToShortDateString() + "'";



        str = str + str1;
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter ad6 = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        ad6.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        foreach (GridViewRow grd in GridView1.Rows)
        {
            Label lblapprovestatus = (Label)grd.FindControl("lblapprovestatus");
            DropDownList ddl = (DropDownList)grd.FindControl("DropDownList26");
            Button btncom = (Button)grd.FindControl("btncom");
            if (lblapprovestatus.Text == "Pending")
            {

                btncom.Visible = true;
                ddl.Enabled = true;
            }
            else if (lblapprovestatus.Text == "Approved")
            {

                ddl.Enabled = false;
                btncom.Visible = false;
            }
            else if (lblapprovestatus.Text == "Rejected")
            {

                ddl.Enabled = false;
                btncom.Visible = false;
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(lblapprovestatus.Text));
        }

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
        try
        {
            string mes = "SELECT TOP(10) " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate,Left(" + PageConn.intmsg11 + ".dbo.MsgMaster.MsgSubject,50) as MsgSubject," + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDetail, Left(Party_master.Compname,15) as Compname," + PageConn.intmsg11 + ".dbo.MsgStatusMaster.MsgStatusName," + PageConn.intmsg11 + ".dbo.MsgDetail.MsgDetailId," + PageConn.intmsg11 + ".dbo.MsgDetail.ToPartyId," + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId FROM " + PageConn.intmsg11 + ".dbo.MsgDetail INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgId = " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgId INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgStatusMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId = " + PageConn.intmsg11 + ".dbo.MsgStatusMaster.MsgStatusId INNER JOIN Party_master ON " + PageConn.intmsg11 + ".dbo.MsgMaster.FromPartyId = Party_master.PartyID  WHERE " + PageConn.intmsg11 + ".dbo.MsgDetail.ToPartyId = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "' AND (" + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId IN (1, 2)) order by " + PageConn.intmsg11 + ".dbo.MsgMaster.Msgdate desc";
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
        catch (Exception ex)
        {
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
                        ins1 = "insert into " + PageConn.intmsg11 + ".dbo.MsgMaster (FromPartyId,MsgDate,MsgSubject,MsgDetail,Picture,Signature) values('" + FromPartyId + "','" + System.DateTime.Now.ToString() + "','" + TextBox7.Text + "','" + TextBox8.Text + ' ' + ViewState["sign"].ToString() + "','" + CheckBox5.Checked + "','" + ViewState["sign"].ToString() + "')";
                    }

                    if (CheckBox4.Checked == false)
                    {
                        ins1 = "insert into " + PageConn.intmsg11 + ".dbo.MsgMaster (FromPartyId,MsgDate,MsgSubject,MsgDetail,Picture,Signature) values('" + FromPartyId + "','" + System.DateTime.Now.ToString() + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + CheckBox5.Checked + "','')";
                    }


                    SqlCommand cmd = new SqlCommand(ins1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SqlDataAdapter da = new SqlDataAdapter("select max(MsgId) as MsgId from " + PageConn.intmsg11 + ".dbo.MsgMaster", con);
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
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.Employee from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["Employee"]) == "True")
                {
                    string str1 = "";
                    string mes1 = "";
                    if (txtsearch.Text != "")
                    {
                        mes1 = " and (Employeemaster.EmployeeName like '%" + txtsearch.Text.Replace("'", "''") + "%')";
                    }


                    str1 = "SELECT distinct Employeemaster.EmployeeName as Name,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID FROM Party_master  inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId  inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID  where Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and EmployeeMaster.Active=1 " + mes1 + "  order by Name";
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
            }
        }
        if (ddlusertype.SelectedItem.Text == "Customer")
        {
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.Customer from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["Customer"]) == "True")
                {

                    pnlusertypeother.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Company Name";
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

                    string str2 = "	SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }
            }
        }

        if (ddlusertype.SelectedItem.Text == "Other")
        {
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.Others from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["Others"]) == "True")
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

                    string str2 = "	SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }
            }
        }

        if (ddlusertype.SelectedItem.Text == "Vendor")
        {
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.Vendor from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["Vendor"]) == "True")
                {

                    pnlusertypeother.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Company Name";
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

                    string str2 = "	SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }
            }
        }

        if (ddlusertype.SelectedItem.Text == "Admin")
        {
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.AdminRights from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["AdminRights"]) == "True")
                {

                    //pnlusertypeother.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Admin";
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

                    string str2 = "	SELECT   distinct  Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status,employeemaster.DesignationMasterId FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join employeemaster on employeemaster.PartyID=Party_master.PartyID where Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }
            }
        }

        if (ddlusertype.SelectedItem.Text == "Candidate")
        {
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.Candidate from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["Candidate"]) == "True")
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

                    string str2 = "select CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename as CName,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID,CandidateMaster.Jobpositionid,VacancyPositionTitleMaster.VacancyPositionTitle from CandidateMaster inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.id=CandidateMaster.Jobpositionid inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID where Party_master.whid='" + ddlwarehouse.SelectedValue + "' " + mes2 + " order by CName";

                    //string str2 = "select CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename as CName,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID,CandidateMaster.Jobpositionid,VacancyPositionTitleMaster.VacancyPositionTitle from CandidateMaster inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.id=CandidateMaster.Jobpositionid inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join MessageCenterRightsTbl on CandidateMaster.DesignationMasterId=MessageCenterRightsTbl.DesignationID where Party_master.whid='" + ddlstore.SelectedValue + "' and [MessageCenterRightsTbl].Candidate=1 " + mes2 + " order by CName";

                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }
            }
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
        try
        {
       
        string mes = "SELECT DISTINCT TOP(10) " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate,Party_master.Compname as From1, " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgSubject, " + PageConn.intmsg11 + ".dbo.MsgMaster.FromPartyId, " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgId, " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgStatus FROM " + PageConn.intmsg11 + ".dbo.MsgDetail INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgId = " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgId INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgStatusMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId = " + PageConn.intmsg11 + ".dbo.MsgStatusMaster.MsgStatusId LEFT OUTER JOIN Party_master ON " + PageConn.intmsg11 + ".dbo.MsgDetail.ToPartyId = Party_master.PartyId WHERE (" + PageConn.intmsg11 + ".dbo.MsgMaster.FromPartyId = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "') AND (" + PageConn.intmsg11 + ".dbo.MsgMaster.MsgStatus != 'Deleted' or " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgStatus is null) and " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId not in(3) order by " + PageConn.intmsg11 + ".dbo.MsgMaster.Msgdate desc";

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
        catch (Exception ex)
        {
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        btnout.Enabled = false;
        Button3.Visible = false;
        Label39.Visible = true;
        // Button3.Text = "Ext Visit Req Pending";

        string te = ("ExternalVisitRequest.aspx");
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        //if (Button3.Text == "Gatepass Request")
        //{
        //    btnout.Enabled = false;

        //    Button3.Text = "Gatepass Return";

        //    string te = ("frmGatePass.aspx");
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        //}

        //else
        //{
        //    btnout.Enabled = true;

        //    Button3.Text = "Gatepass Request";

        //    int Gate = Convert.ToInt16(ViewState["GatepassReturn"]);
        //    string te = ("frmgatepassreturn.aspx?gatepass=" + Gate);
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        //}
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
        string mes = "SELECT TOP(10) " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate, " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgSubject, Party_master.Compname, " + PageConn.intmsg11 + ".dbo.MsgStatusMaster.MsgStatusName, " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgDetailId, " + PageConn.intmsg11 + ".dbo.MsgDetail.ToPartyId, " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId FROM " + PageConn.intmsg11 + ".dbo.MsgDetail INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgId = " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgId INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgStatusMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId = " + PageConn.intmsg11 + ".dbo.MsgStatusMaster.MsgStatusId INNER JOIN Party_master ON " + PageConn.intmsg11 + ".dbo.MsgMaster.FromPartyId = Party_master.PartyId  WHERE (" + PageConn.intmsg11 + ".dbo.MsgDetail.ToPartyId = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "') AND (" + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId IN (4)) order by " + PageConn.intmsg11 + ".dbo.MsgMaster.Msgdate desc";

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
        string mes = "SELECT distinct TOP(10) " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate, " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgSubject,Party_master.Compname, " + PageConn.intmsg11 + ".dbo.MsgStatusMaster.MsgStatusName," + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId," + PageConn.intmsg11 + ".dbo.MsgMaster.FromPartyId, " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgId FROM " + PageConn.intmsg11 + ".dbo.MsgDetail INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgId = " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgId INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgStatusMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId = " + PageConn.intmsg11 + ".dbo.MsgStatusMaster.MsgStatusId inner join Party_master on Party_master.PartyID=" + PageConn.intmsg11 + ".dbo.MsgMaster.FromPartyId inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where Party_master.PartyID = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "' and " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId in(3) order by " + PageConn.intmsg11 + ".dbo.MsgMaster.Msgdate desc";

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
    protected void Return_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoReturn.Checked == true)
        {
            int Gate = Convert.ToInt16(ViewState["GatepassReturn"]);
            string te = ("frmgatepassreturn.aspx?gatepass=" + Gate);
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
    protected void Unnamed1_CheckedChanged(object sender, EventArgs e)
    {
        if (rdomain.Checked == true)
        {
            string te = ("frmGatePass.aspx");
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
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
    protected void Button7_Click(object sender, EventArgs e)
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from GatepassDetails inner join GatepassTBL on GatepassTBL.Id=GatepassDetails.GatePassID where GatepassTBL.EmployeeId='" + Session["EmployeeId"] + "' and Detail IS NULL and TimeReached IS NULL and TimeLeft IS NULL and TotalDuration IS NULL", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            SqlCommand cmd = new SqlCommand("delete from GatepassDetails where GatePassID='" + dt.Rows[0]["GatePassID"].ToString() + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            SqlCommand cmd1 = new SqlCommand("delete from GatepassTBL where Id='" + dt.Rows[0]["GatePassID"].ToString() + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();

            btngon.Visible = false;
            Button7.Visible = false;
            Label39.Visible = false;
            lblextereq.Visible = false;
            btnout.Enabled = true;
            Button3.Visible = true;
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        SqlDataAdapter dagate4 = new SqlDataAdapter("select GatepassDetails.* from GatepassDetails inner join GatepassTBL on GatepassDetails.GatePassID=GatepassTBL.Id where EmployeeID='" + Session["EmployeeId"].ToString() + "' and GatepassDetails.TimeReached IS NOT NULL and GatepassDetails.TimeLeft IS NULL", con);
        DataTable dtgate4 = new DataTable();
        dagate4.Fill(dtgate4);

        if (dtgate4.Rows.Count > 0)
        {
            SqlCommand cmd = new SqlCommand("update GatepassDetails set TimeLeft='" + System.DateTime.Now.ToShortTimeString().Substring(0, 5) + "' where GatePassID='" + dtgate4.Rows[0]["GatePassID"].ToString() + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            btnout.Enabled = true;
            Button3.Visible = true;
            btngon.Visible = false;
            Button7.Visible = false;
            Label39.Visible = false;
            lblextereq.Visible = false;

            string te = "frmgatepassreturn.aspx?gatepass=" + Convert.ToInt32(dtgate4.Rows[0]["GatePassID"]);
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from GatepassDetails inner join GatepassTBL on GatepassTBL.Id=GatepassDetails.GatePassID where GatepassTBL.EmployeeId='" + Session["EmployeeId"] + "' and Detail IS NULL and TimeReached IS NULL and TimeLeft IS NULL and TotalDuration IS NULL", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            SqlCommand cmd = new SqlCommand("update GatepassDetails set TimeReached='" + System.DateTime.Now.ToShortTimeString().Substring(0, 5) + "' where GatePassID='" + dt.Rows[0]["GatePassID"].ToString() + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            btngon.Visible = false;
            Button7.Visible = false;
            Label39.Visible = false;
            lblextereq.Visible = false;

            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Response.AddHeader("Pragma", "no-cache");
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Expires = -1;
            Response.Redirect("~/ShoppingCart/Admin/ShoppingCartLogin.aspx");
        }
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
    protected void taskmore_Click1(object sender, EventArgs e)
    {
        string te = "Messageinboxext.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
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

    protected void gridInbox_Sorting(object sender, GridViewSortEventArgs e)
    {
        //hdnsortExp.Value = e.SortExpression.ToString();
        //hdnsortDir.Value = sortOrder; // sortOrder;
        //SelectMsgforInbox();
    }


    protected void FillPartyGridext()
    {
        //Panel22.Visible = false;
        //Panel23.Visible = false;

        //DataTable dt = new DataTable();

        //if (ddlpartytype.SelectedItem.Text == "Employee")
        //{
        //    string str1 = "";
        //    string mes1 = "";

        //    if (TextBox5.Text != "")
        //    {
        //        mes1 = " and (Employeemaster.EmployeeName like '%" + TextBox5.Text.Replace("'", "''") + "%')";
        //    }

        //    str1 = "SELECT distinct Employeemaster.EmployeeName as Name,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID FROM Party_master  inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId  inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID  where Party_master.Whid='" + ddlwarehouseext.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlpartytype.SelectedValue + "' and EmployeeMaster.Active=1 " + mes1 + "  order by Name";


        //    SqlDataAdapter dal = new SqlDataAdapter(str1, con);
        //    dt = new DataTable();
        //    dal.Fill(dt);

        //    GridView4.Columns[0].Visible = true;
        //    GridView4.Columns[1].Visible = false;
        //    GridView4.Columns[2].Visible = false;
        //    GridView4.Columns[3].Visible = true;
        //    GridView4.Columns[4].Visible = false;
        //    GridView4.Columns[5].Visible = false;
        //    GridView4.Columns[6].Visible = false;
        //}

        //if (ddlpartytype.SelectedItem.Text == "Customer" || ddlpartytype.SelectedItem.Text == "Other" || ddlpartytype.SelectedItem.Text == "Vendor" || ddlpartytype.SelectedItem.Text == "Admin")
        //{
        //    Panel22.Visible = true;

        //    GridView4.Columns[0].Visible = true;
        //    GridView4.Columns[1].Visible = true;
        //    GridView4.Columns[2].Visible = true;
        //    GridView4.Columns[3].Visible = false;
        //    GridView4.Columns[4].Visible = false;
        //    GridView4.Columns[5].Visible = false;
        //    GridView4.Columns[6].Visible = false;


        //    string mes2 = "";
        //    if (TextBox5.Text != "")
        //    {
        //        mes2 = " and ((Party_master.Compname like '%" + TextBox5.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + TextBox5.Text.Replace("'", "''") + "%'))";
        //    }
        //    if (ddlcompname.SelectedIndex > 0)
        //    {
        //        mes2 += " and Party_master.PartyID='" + ddlcompname.SelectedValue + "'";
        //    }

        //    string str2 = "SELECT   distinct  Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status " +
        //                " FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId " +
        //                " where Party_master.Whid='" + ddlwarehouseext.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlpartytype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
        //    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
        //    dt = new DataTable();
        //    dal.Fill(dt);
        //}
        //if (ddlpartytype.SelectedItem.Text == "Candidate")
        //{
        //    Panel23.Visible = true;

        //    GridView4.Columns[0].Visible = true;
        //    GridView4.Columns[1].Visible = false;
        //    GridView4.Columns[2].Visible = false;
        //    GridView4.Columns[3].Visible = false;
        //    GridView4.Columns[4].Visible = false;
        //    GridView4.Columns[5].Visible = true;
        //    GridView4.Columns[6].Visible = true;

        //    string mes2 = "";
        //    if (TextBox5.Text != "")
        //    {
        //        mes2 = " and ((CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename like '%" + TextBox5.Text.Replace("'", "''") + "%') or (VacancyPositionTitleMaster.VacancyPositionTitle like '%" + TextBox5.Text.Replace("'", "''") + "%'))";
        //    }
        //    if (ddlcandi.SelectedIndex > 0)
        //    {
        //        mes2 += " and VacancyPositionTitleMaster.id='" + ddlcandi.SelectedValue + "'";
        //    }

        //    string str2 = "select CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename as CName,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID,CandidateMaster.Jobpositionid,VacancyPositionTitleMaster.VacancyPositionTitle from CandidateMaster inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.id=CandidateMaster.Jobpositionid inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID where Party_master.whid='" + ddlwarehouseext.SelectedValue + "' " + mes2 + " order by CName";
        //    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
        //    dt = new DataTable();
        //    dal.Fill(dt);

        //}

        //GridView4.DataSource = dt;
        //GridView4.DataBind();

    }
    //protected void chkattachmentext_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkattachmentext.Checked == true)
    //    {
    //        string te = "Messagecomposeext.aspx";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //    }
    //}

    protected void ClearAll()
    {
        foreach (GridViewRow GR in GridView4.Rows)
        {
            CheckBox chk = (CheckBox)GR.FindControl("chkParty");
            chk.Checked = false;
        }
        Session["GridFileAttach11"] = null;

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


    protected void Button9_Click(object sender, EventArgs e)
    {
        ModalPopupExtender4.Hide();
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

        //string str = "SELECT distinct Party_master.Compname,Party_master.PartyID FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlwarehouseext.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlpartytype.SelectedValue + "' and AccountMaster.Status='1' order by Party_master.Compname";

        //SqlDataAdapter da = new SqlDataAdapter(str, con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);

        //ddlcompname.DataSource = dt;
        //ddlcompname.DataTextField = "Compname";
        //ddlcompname.DataValueField = "PartyID";
        //ddlcompname.DataBind();

        //ddlcompname.Items.Insert(0, "ALL");
        //ddlcompname.Items[0].Value = "0";
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
            //  lblAddresses.Text = address.ToString();
        }
        ModalPopupExtender3.Hide();
        // imgbtnsend.Enabled = true;
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
            // lblmsg1.Text = "Error in databound : " + ex.Message.ToString();
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


    protected void fillpaneldisplay()
    {
        SqlDataAdapter daem = new SqlDataAdapter("select warehouseid from warehousemaster inner join employeemaster on employeemaster.whid=warehousemaster.warehouseid where employeemaster.employeemasterid='" + Session["EmployeeId"] + "'", con);
        DataTable dtem = new DataTable();
        daem.Fill(dtem);

        string str = "select * from DesignationWisePanelRights where Whid='" + Convert.ToString(dtem.Rows[0]["warehouseid"]) + "' and PageMenuId='5' ";
        SqlCommand cmdwh = new SqlCommand(str, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);

        if (dtwh.Rows.Count > 0)
        {
            if (dtwh.Rows[0]["Panel1"].ToString() == "True")
            {
                Panel1.Enabled = true;
            }
            else
            {
                Panel1.Enabled = false;
            }

            if (dtwh.Rows[0]["Panel2"].ToString() == "True")
            {
                // Panel2.Enabled = true;
            }
            else
            {
                //Panel2.Enabled = false;
            }

            if (dtwh.Rows[0]["Panel3"].ToString() == "True")
            {
                Panel3.Enabled = true;
            }
            else
            {
                Panel3.Enabled = false;
            }

            if (dtwh.Rows[0]["Panel4"].ToString() == "True")
            {
                Panel4.Enabled = true;
            }
            else
            {
                Panel4.Enabled = false;
            }

            if (dtwh.Rows[0]["Panel5"].ToString() == "True")
            {
                Panel5.Enabled = true;
            }
            else
            {
                Panel5.Enabled = false;
            }

            if (dtwh.Rows[0]["Panel6"].ToString() == "True")
            {
                //  Panel6.Enabled = true;
            }
            else
            {
                //  Panel6.Enabled = false;
            }

            //if (dtwh.Rows[0]["Panel7"].ToString() == "True")
            //{
            //    Panel7.Enabled = true;
            //}
            //else
            //{
            //    Panel7.Enabled = false;
            //}
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void filllatecommersgrid()
    {
        string str = "select distinct Left(FirstName,1)+'.'+Left(LastName,13) as EmployeeName,EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeMasterID as Absent,EmployeeMaster.EmployeeMasterID as LateCome,EmployeeMaster.EmployeeMasterID as LateGo,EmployeeMaster.EmployeeMasterID as EarlyCome,EmployeeMaster.EmployeeMasterID as EarlyGo from EmployeeMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterId left join AttendenceDeviations on AttendenceDeviations.EmployeeID = EmployeeMaster.EmployeeMasterID where EmployeeBatchMaster.Whid='" + ViewState["Whid"] + "' and [DeviationDate] between '" + System.DateTime.Now.AddDays(-30).ToShortDateString() + "' and '" + System.DateTime.Now.ToShortDateString() + "'";

        //"select EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID where EmployeeBatchMaster.Whid='" + ViewState["Whid"] + "'";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);


        for (int i = 0; i < dt.Rows.Count; i++)
        {

            DataTable dsedr = select("select count(BatchWorkingDaysId) as BatchWorkingDaysId from [BatchWorkingDays] inner join batchmaster on batchmaster.id=[BatchWorkingDays].BatchID inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=batchmaster.id inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join DateMasterTbl on DateMasterTbl.DateId=[BatchWorkingDays].DateMasterID where EmployeeMaster.EmployeeMasterID='" + dt.Rows[i]["EmployeeMasterID"].ToString() + "' and  DateMasterTbl.date between '" + System.DateTime.Now.AddDays(-30).ToShortDateString() + "' and '" + System.DateTime.Now.ToShortDateString() + "'");

            string str1 = "select count(AttendanceId) as AttendanceId from AttendenceEntryMaster where EmployeeID='" + dt.Rows[i]["EmployeeMasterID"].ToString() + "' and Date between '" + System.DateTime.Now.AddDays(-30).ToShortDateString() + "' and '" + System.DateTime.Now.ToShortDateString() + "'";

            SqlDataAdapter da1 = new SqlDataAdapter(str1, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                dt.Rows[i]["Absent"] = (Convert.ToInt32(dsedr.Rows[0]["BatchWorkingDaysId"]) - Convert.ToInt32(dt1.Rows[0]["AttendanceId"])).ToString();
            }

            string str2 = "select count(AttendanceId) as AttendanceId from AttendenceEntryMaster where EmployeeID='" + dt.Rows[i]["EmployeeMasterID"].ToString() + "' and Date between '" + System.DateTime.Now.AddDays(-30).ToShortDateString() + "' and '" + System.DateTime.Now.ToShortDateString() + "' and  (AttendenceEntryMaster.InTimeforcalculation > AttendenceEntryMaster.InTime)";

            SqlDataAdapter da2 = new SqlDataAdapter(str2, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            if (dt2.Rows.Count > 0)
            {
                dt.Rows[i]["LateCome"] = dt2.Rows[0]["AttendanceId"].ToString();
            }

            string str4 = "select count(AttendanceId) as AttendanceId from AttendenceEntryMaster where EmployeeID='" + dt.Rows[i]["EmployeeMasterID"].ToString() + "' and Date between '" + System.DateTime.Now.AddDays(-30).ToShortDateString() + "' and '" + System.DateTime.Now.ToShortDateString() + "' and (AttendenceEntryMaster.OutTimeforcalculation > AttendenceEntryMaster.OutTime)";

            SqlDataAdapter da4 = new SqlDataAdapter(str4, con);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);

            if (dt4.Rows.Count > 0)
            {
                dt.Rows[i]["LateGo"] = dt4.Rows[0]["AttendanceId"].ToString();
            }

            string str3 = "select count(AttendanceId) as AttendanceId from AttendenceEntryMaster where EmployeeID='" + dt.Rows[i]["EmployeeMasterID"].ToString() + "' and Date between '" + System.DateTime.Now.AddDays(-30).ToShortDateString() + "' and '" + System.DateTime.Now.ToShortDateString() + "' and  (AttendenceEntryMaster.OutTimeforcalculation < AttendenceEntryMaster.OutTime)";

            SqlDataAdapter da3 = new SqlDataAdapter(str3, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {
                dt.Rows[i]["EarlyGo"] = dt3.Rows[0]["AttendanceId"].ToString();
            }

            string str5 = "select count(AttendanceId) as AttendanceId from AttendenceEntryMaster where EmployeeID='" + dt.Rows[i]["EmployeeMasterID"].ToString() + "' and Date between '" + System.DateTime.Now.AddDays(-30).ToShortDateString() + "' and '" + System.DateTime.Now.ToShortDateString() + "' and  (AttendenceEntryMaster.InTimeforcalculation < AttendenceEntryMaster.InTime)";

            SqlDataAdapter da5 = new SqlDataAdapter(str5, con);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);

            if (dt5.Rows.Count > 0)
            {
                dt.Rows[i]["EarlyCome"] = dt5.Rows[0]["AttendanceId"].ToString();
            }
        }

        GridView2.DataSource = dt;
        GridView2.DataBind();

    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        filllatecommersgrid();
    }
    //protected void txtstartdate_TextChanged(object sender, EventArgs e)
    //{
    //    filltoday();
    //}

    protected void fillbusinessdept()
    {
        string str = "select  DepartmentmasterMNC.Departmentname as Name,DepartmentmasterMNC.id from DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId = DepartmentmasterMNC.whid where WareHouseMaster.Status='1' and WareHouseMaster.WarehouseId='" + ViewState["empid"] + "' order by WareHouseMaster.Name,DepartmentmasterMNC.Departmentname";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        DropDownList2.DataSource = dt;
        DropDownList2.DataTextField = "Name";
        DropDownList2.DataValueField = "id";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "All");
        DropDownList2.Items[0].Value = "0";
    }

    protected void filltoday()
    {
        string filter = "";
        if (DropDownList2.SelectedIndex > 0)
        {
            filter = " and EmployeeMaster.DeptID='" + DropDownList2.SelectedValue + "'";
        }
        string str = "select distinct Left(DepartmentmasterMNC.Departmentname,15) as DeptName, Left(FirstName,1)+'.'+Left(LastName,18) as EmployeeName,EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeMasterID as Absent,EmployeeMaster.EmployeeMasterID as Days,AbsenseNote.reason,AbsenseNote.ID from EmployeeMaster  inner join AbsenseNote on AbsenseNote.empid = EmployeeMaster.EmployeeMasterID inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterId inner join DepartmentmasterMNC on DepartmentmasterMNC.Id=EmployeeMaster.DeptId where EmployeeMaster.Whid='" + ViewState["empid"] + "' " + filter + " Order by EmployeeName";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTable dsedr = select("select count(BatchWorkingDaysId) as BatchWorkingDaysId from [BatchWorkingDays] inner join batchmaster on batchmaster.id=[BatchWorkingDays].BatchID inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=batchmaster.id inner join EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join DateMasterTbl on DateMasterTbl.DateId=[BatchWorkingDays].DateMasterID where EmployeeMaster.EmployeeMasterID='" + dt.Rows[i]["EmployeeMasterID"].ToString() + "' and  DateMasterTbl.date between '" + System.DateTime.Now.AddDays(-30).ToShortDateString() + "' and '" + System.DateTime.Now.ToShortDateString() + "'");

            string str1 = "select count(AttendanceId) as AttendanceId from AttendenceEntryMaster where EmployeeID='" + dt.Rows[i]["EmployeeMasterID"].ToString() + "' and Date between '" + System.DateTime.Now.AddDays(-30).ToShortDateString() + "' and '" + System.DateTime.Now.ToShortDateString() + "'";

            SqlDataAdapter da1 = new SqlDataAdapter(str1, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                dt.Rows[i]["Absent"] = (Convert.ToInt32(dsedr.Rows[0]["BatchWorkingDaysId"]) - Convert.ToInt32(dt1.Rows[0]["AttendanceId"])).ToString();
            }
        }

        GridView8.DataSource = dt;
        GridView8.DataBind();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        filltoday();
    }
    protected void GridView8_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            int mm = Convert.ToInt32(e.CommandArgument);

            SqlDataAdapter da = new SqlDataAdapter("select Note from AbsenseNote where ID = '" + mm + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Label60.Text = dt.Rows[0]["Note"].ToString();

            ModalPopupExtender13.Show();
        }
    }
    protected void grdgatepass_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdgatepass.EditIndex = e.NewEditIndex;

        filllapproval();

        DropDownList DropDownList18 = (DropDownList)(grdgatepass.Rows[grdgatepass.EditIndex].FindControl("DropDownList18"));

        //string bbc = "select [StatusMaster].[StatusName],[StatusMaster].[StatusId] from [StatusMaster] where [StatusCategoryMasterId]='4281'";

        //SqlDataAdapter da1 = new SqlDataAdapter(bbc, con);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);

        //DropDownList18.DataSource = dt1;
        //DropDownList18.DataValueField = "StatusId";
        //DropDownList18.DataTextField = "StatusName";
        //DropDownList18.DataBind();
        //DropDownList18.Items.Insert(0, "-Select-");
    }
    protected void grdgatepass_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdgatepass.EditIndex = -1;
        filllapproval();
    }



    protected void lblrateLeave_Click(object sender, EventArgs e)
    {

        GridViewRow iten = ((Button)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        DropDownList DropDownList26 = (DropDownList)(GridView1.Rows[rinrow].FindControl("DropDownList26"));
        Label Label20 = (Label)(GridView1.Rows[rinrow].FindControl("Label20"));

        string str = "update EmployeeHoliday set leaveApproveEmployeeid='" + ViewState["supervisor"] + "',ApprovalNote='" + lblleaveappnotes.Text + "',status='" + DropDownList26.SelectedValue + "' where id='" + Label20.Text + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        if (Convert.ToString(DropDownList26.SelectedValue) == "1")
        {
            DataTable dor = select("Select * from EmployeeHoliday where  id='" + Label20.Text + "'");

            if (dor.Rows.Count > 0)
            {
                int usedleave = 0;
                int balanceleave = 0;

                DataTable dorou = select("Select * from LeaveEarnedTbl where LeaveType='" + dor.Rows[0]["leavetypeid"] + "' and EmployeeId='" + dor.Rows[0]["employeeid"] + "'");
                if (dorou.Rows.Count > 0)
                {
                    usedleave = Convert.ToInt32(dorou.Rows[0]["NumberofleaveUsed"]) + Convert.ToInt32(dor.Rows[0]["Noofleave"]);

                    string stratt = "Update  LeaveEarnedTbl set NumberofleaveUsed='" + usedleave + "' where LeaveType='" + dor.Rows[0]["leavetypeid"] + "' and EmployeeId='" + dor.Rows[0]["employeeid"] + "'";
                    SqlCommand cmdatt = new SqlCommand(stratt, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdatt.ExecuteNonQuery();
                    con.Close();
                    balanceleave = Convert.ToInt32(dorou.Rows[0]["NumberofleaveEarned"]) - usedleave;
                    int totalleave = Convert.ToInt32(dor.Rows[0]["Noofleave"]) + balanceleave;
                    string stradet = "insert into LeaveEarnedTblDeatail(LeaveType,EmployeeId,BalanceLeave,UsedLeave,Date,Encash,TotalLeave,EmpholidayId)Values('" + dor.Rows[0]["leavetypeid"] + "','" + dor.Rows[0]["employeeid"] + "','" + balanceleave + "','" + Convert.ToInt32(dor.Rows[0]["Noofleave"]) + "','" + DateTime.Now.ToShortDateString() + "','0','" + totalleave + "','" + Label20.Text + "')";
                    SqlCommand cmddet = new SqlCommand(stradet, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmddet.ExecuteNonQuery();
                    con.Close();
                }

                int countweek = 1;
                String dtp = Convert.ToDateTime(dor.Rows[0]["fromdate"]).ToShortDateString();
                for (int i = 0; i < countweek; i++)
                {
                    DataTable ds1 = select("SELECT distinct Convert(nvarchar, DateMasterTbl.Date,101) as Datet,DateMasterID as DateId FROM EmployeeMaster  " +
                    " INNER JOIN  dbo.EmployeeBatchMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.EmployeeBatchMaster.Employeeid  inner join BatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join BatchWorkingDays on BatchWorkingDays.BatchID=BatchMaster.Id inner join DateMasterTbl" +
                    " on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID  where  EmployeeBatchMaster.Employeeid='" + dor.Rows[0]["employeeid"] + "' and cast( DateMasterTbl.Date as DateTime)='" + Convert.ToDateTime(dtp).ToShortDateString() + "'");
                    if (ds1.Rows.Count > 0)
                    {
                        if (Convert.ToDateTime(dtp) <= Convert.ToDateTime(dor.Rows[0]["Todate"]))
                        {
                            string stratt = "insert into AttendenceEntryMaster(EmployeeID,Date,InTime,InTimeforcalculation,OutTime,OutTimeforcalculation,Varify,SupervisorId,PailLeaveapprove)Values('" + dor.Rows[0]["employeeid"] + "','" + dtp + "','00:00','00:00','00:00','00:00','1','" + ViewState["supervisor"] + "','1')";
                            SqlCommand cmdatt = new SqlCommand(stratt, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdatt.ExecuteNonQuery();
                            con.Close();
                            dtp = Convert.ToDateTime(dtp).AddDays(1).ToShortDateString();
                            countweek += 1;
                        }
                        else
                        {
                            break;
                        }

                    }
                }
            }
        }

        lblleaveappnotes.Text = "";
        fillleavee();
    }
    protected void lblrate_Click(object sender, EventArgs e)
    {

        GridViewRow iten = ((Button)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        DropDownList DropDownList18 = (DropDownList)(grdgatepass.Rows[rinrow].FindControl("DropDownList18"));
        Label lblsapp = (Label)(grdgatepass.Rows[rinrow].FindControl("lblsapp"));
        Label Label19 = (Label)(grdgatepass.Rows[rinrow].FindControl("Label19"));


        string update1 = "update GatepassTBL  set ApprovedEmployeeId = " + ViewState["empid"] + ", ApprovalDate = '" + System.DateTime.Now.ToShortDateString() + "' ,Approved = " + DropDownList18.SelectedValue + " where Id = " + Label19.Text + "";
        SqlCommand cmdgate = new SqlCommand(update1, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdgate.ExecuteNonQuery();
        con.Close();

        string update11 = "update GatepassDetails  set Detail = " + txtgpaapp.Text + " where GatePassID = " + Label19.Text + "";
        SqlCommand cmdgate1 = new SqlCommand(update11, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdgate1.ExecuteNonQuery();
        con.Close();

        grdgatepass.EditIndex = -1;
        txtgpaapp.Text = "";
        filllapproval();
    }

    protected void ddlpayperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ISFill"] = null;

        FillSalary();
    }
    protected void pynpayrollnow_Click(object sender, EventArgs e)
    {
        string te = "Employeepayrollsummary.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void lbloutoff_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        // string salid = grdallemp.DataKeys[rinrow].Value.ToString();

        //Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");
        //string te = "MyPaySlip.aspx?SL=" + salid + "&Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        string te = "frmGatePassProfile.aspx";


        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void lblempgate_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        // string salid = grdallemp.DataKeys[rinrow].Value.ToString();

        //Label empid = (Label)grdallemp.Rows[rinrow].FindControl("lblEmployeeId");
        //string te = "MyPaySlip.aspx?SL=" + salid + "&Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        string te = "frmGatepassReport.aspx";


        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void lblleavedate_Click(object sender, EventArgs e)
    {
        LinkButton ch = (LinkButton)sender;
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        // string salid = grdallemp.DataKeys[rinrow].Value.ToString();

        Label Label20 = (Label)GridView1.Rows[rinrow].FindControl("Label20");
        LinkButton lblFrmDate = (LinkButton)GridView1.Rows[rinrow].FindControl("lblFrmDate");
        LinkButton lblToDate = (LinkButton)GridView1.Rows[rinrow].FindControl("lblToDate");
        Session["date"] = lblFrmDate.Text;
        Session["edate"] = lblToDate.Text;

        //string te = "MyPaySlip.aspx?SL=" + salid + "&Emp=" + Convert.ToString(empid.Text) + "&Wid=" + ddlwarehouse.SelectedValue + "";
        string te = "LeaveApplicationbyAdmin_Supervisor.aspx?Id=" + Label20.Text;


        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void Appn_Click(object sender, ImageClickEventArgs e)
    {
        // ImageButton ch = (ImageButton)sender;
        GridViewRow iten = ((ImageButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        lblleaveappnotes.Text = "";
        ModalPopupExtender2.Show();
    }
    protected void Appngt_Click(object sender, ImageClickEventArgs e)
    {
        // ImageButton ch = (ImageButton)sender;
        GridViewRow iten = ((ImageButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        txtgpaapp.Text = "";
        ModalPopupExtender5.Show();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string idin = ViewState["idgone"].ToString();
        string emp = Session["EmployeeId"].ToString();
        string te = ("frmGatepassProfile.aspx?empid=" + emp + "&req=" + idin);
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
}

