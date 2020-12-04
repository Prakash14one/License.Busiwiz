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

public partial class Add_Attendence_Deviations : System.Web.UI.Page
{
    //string comp;
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
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


        statuslable.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            ddbatch.DataSource = (DataSet)getall();
            ddbatch.DataValueField = "ID";
            ddbatch.DataTextField = "Name";
            ddbatch.DataBind();
            
            DataTable dr = select("Select distinct BatchMaster.Id from  EmployeeMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterId inner join BatchMaster on BatchMaster.Id=EmployeeBatchMaster.BatchMasterId where EmployeeMasterId='" + Session["EmployeeId"] + "'");
            if (dr.Rows.Count > 0)
            {
                ddbatch.SelectedIndex = ddbatch.Items.IndexOf(ddbatch.Items.FindByValue(dr.Rows[0]["Id"].ToString()));
            }
            ddldatebatch.DataSource = (DataSet)getall();
            ddldatebatch.DataValueField = "ID";
            ddldatebatch.DataTextField = "Name";
            ddldatebatch.DataBind();
            //ddldatebatch.SelectedIndex = ddldatebatch.Items.IndexOf(ddldatebatch.Items.FindByValue(dr.Rows[0]["Id"].ToString()));
            //ddbatch.Items.Insert(0, "--Select--");

            //ddbatch.Items[0].Value = "0";
            rdlist_SelectedIndexChanged(sender, e);
            txtdate.Text = DateTime.Now.ToShortDateString();
            txttodate.Text = DateTime.Now.ToShortDateString();
            lblcompany.Text = Session["Cname"].ToString();
        }
    }



    public void fillgrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string str1 = "";
        string[] separator1 = new string[] { " : " };
        string[] strSplitArr2;
        if (rdlist.SelectedValue == "0")
        {
            if (ddlpayperiod.SelectedItem.Text != "")
            {

                string[] strSplitArr1 = ddlpayperiod.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                string sdate = strSplitArr1[1].ToString();
                string edate = strSplitArr1[2].ToString();
                str1 = "Select Distinct AttendenceDeviations.Id, case when(ConsiderHalfDayLeave IS Null) then '0' else ConsiderHalfDayLeave End as ConsiderHalfDayLeave ,case when(ConsiderFullDayLeave IS Null) then '0' else ConsiderFullDayLeave End as ConsiderFullDayLeave,case when(Varify IS Null) then '0' else Varify End as Varify, a.EmployeeName as sname, EmployeeMaster.SuprviserId, EmployeeMaster.EmployeeName,AttendanceId,AttendenceEntryMaster.EmployeeID,Convert(nvarchar,AttendenceEntryMaster.Date,101) as Date,Left(BatchRequiredhours,5) as BatchRequiredhours,TotalHourWork,Payabledays,Payablehours,InTime,OutTime,InTimeforcalculation,OutTimeforcalculation,note  from  EmployeeMaster inner join AttendenceEntryMaster on AttendenceEntryMaster.EmployeeID=EmployeeMaster.EmployeeMasterID inner join AttendenceDeviations on AttendenceDeviations.attandanceId=AttendenceEntryMaster.AttendanceId left join EmployeeMaster as a on a.EmployeeMasterId= AttendenceEntryMaster.SupervisorId where AttendenceDeviations.PayPeriodID='" + ddlpayperiod.SelectedValue + "' and AttendenceEntryMaster.EmployeeID='" + ddlemp.SelectedValue + "' and  Date   between '" + sdate + "' and '" + edate + "' order by Date Desc";
                strSplitArr2 = ddbatch.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

                lblpay.Text = "Payperiod: " + strSplitArr1[0].ToString() + ", "; 
                lbldatefrom.Text = " From " + sdate + " to " + edate;
                lblemp.Text = "Employee: " + ddlemp.SelectedItem.Text + ", "; ;
                //lblheadtext.Text = "List of Single employee pay period Deviations";
                //lblhead.Text = "List of Single employee pay period Deviations ";
                lblheadtext.Text = "List of Employee Time Deviations by Date";
                lblhead.Text = "List of Employee Time Deviations by Date";

                lblBusiness.Text = strSplitArr2[0].ToString();
                lblbatch.Text = "Batch Name: " + strSplitArr2[1].ToString()+", ";
                //string biz = strSplitArr2[0].ToString();
                //string batch = strSplitArr2[1].ToString();
                //lblBusiness.Text = biz;
            }
        }
        else
        {
            lblpay.Text = "";
            string ac = "";
            if (ddlempbatch.SelectedIndex > 0)
            {
                ac = " and AttendenceEntryMaster.EmployeeID='" + ddlempbatch.SelectedValue + "'";
            }
            str1 = "Select Distinct TotalHourWork, AttendenceDeviations.Id, case when(ConsiderHalfDayLeave IS Null) then '0' else ConsiderHalfDayLeave End as ConsiderHalfDayLeave ,case when(ConsiderFullDayLeave IS Null) then '0' else ConsiderFullDayLeave End as ConsiderFullDayLeave,case when(Varify IS Null) then '0' else Varify End as Varify, a.EmployeeName as sname,  EmployeeMaster.SuprviserId, EmployeeMaster.EmployeeName,AttendanceId,AttendenceEntryMaster.EmployeeID,Convert(nvarchar,AttendenceEntryMaster.Date,101) as Date,Left(BatchRequiredhours,5) as BatchRequiredhours,Payabledays,Payablehours,InTime,OutTime,InTimeforcalculation,OutTimeforcalculation,note  from EmployeeBatchMaster inner join  EmployeeMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID inner join AttendenceEntryMaster on AttendenceEntryMaster.EmployeeID=EmployeeMaster.EmployeeMasterID inner join AttendenceDeviations on AttendenceDeviations.attandanceId=AttendenceEntryMaster.AttendanceId left join EmployeeMaster as a on a.EmployeeMasterId=  AttendenceEntryMaster.SupervisorId  where EmployeeBatchMaster.Batchmasterid='" + ddldatebatch.SelectedValue + "' "+ac+"  and   Date between '" + txtdate.Text + "' and '" + txttodate.Text + "'  order by EmployeeName";
            strSplitArr2 = ddldatebatch.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
            lbldatefrom.Text = "From " + txtdate.Text + " to " + txttodate.Text;
            if (ddlempbatch.SelectedItem.Text != "")
            {
                lblemp.Text = " Employee: " + ddlempbatch.SelectedItem.Text + ", ";
            }
            lblBusiness.Text = strSplitArr2[0].ToString();
            lblbatch.Text = "Batch Name: " + strSplitArr2[1].ToString() + ", ";
            lblheadtext.Text = "List of Employee Time Deviations by Date";
            lblhead.Text = "List of Employee Time Deviations by Date";
        }

        
        
        
        

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);

        if (ds1.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = ds1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataSource = myDataView;
            GridView1.DataBind();

        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

        }
    }
    public DataSet getall()
    {
        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();

        Mycommand = new SqlCommand("Select BatchMaster.ID, WareHouseMaster.Name as businessname, WareHouseMaster.Name + ' : '+ BatchMaster.Name as Name from BatchMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=BatchMaster.WHID where WareHouseMaster.comid='" + Session["comid"] + "' order by Name", con);

        MyDataAdapter = new SqlDataAdapter(Mycommand);
        DataSet ds1 = new DataSet();
        MyDataAdapter.Fill(ds1);
        con.Close();
        return ds1;
    }




    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


        int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);
        Label AttendanceId = (Label)GridView1.Rows[e.RowIndex].FindControl("AttendanceId");
        Label txtreqintime = (Label)GridView1.Rows[e.RowIndex].FindControl("txtreqintime");
        TextBox txtactintime = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtactintime");
        Label txtreqouttime = (Label)GridView1.Rows[e.RowIndex].FindControl("txtreqouttime");
        TextBox txtactouttime = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtactouttime");
        //TextBox txtpayhours = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtpayhours");
        //TextBox txtpayday = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtpayday");
        Label txtbatchreqhour = (Label)GridView1.Rows[e.RowIndex].FindControl("txtbatchreqhour");
        TextBox txtdevnote = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtdevnote");
        TextBox txtactwork = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtactwork");

        decimal payday = 0;

        CheckBox ConsiderHalfDayLeave = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("chkhalfday");

        CheckBox ConsiderFullDayLeave = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("chkfullday");
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


        CheckBox Varify = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("chlapproved");
        string s1 = "UPDATE AttendenceEntryMaster SET TotalHourWork='" + txtactwork.Text + "', [InTimeforcalculation]='" + txtactintime.Text + "', " +
                         " [OutTimeforcalculation] = '" + txtactouttime.Text + "',[Payabledays] = '" + payday + "',[BatchRequiredhours] = '" + txtbatchreqhour.Text + "',ConsiderHalfDayLeave='" + ConsiderHalfDayLeave.Checked + "',ConsiderFullDayLeave='" + ConsiderFullDayLeave.Checked + "',Varify='" + Varify.Checked + "',SupervisorId='" + ViewState["emp"] + "' WHERE [AttendanceId] ='" + AttendanceId.Text + "'";
        SqlCommand cmd333 = new SqlCommand(s1, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd333.ExecuteNonQuery();
        con.Close();
        string s11 = "UPDATE AttendenceDeviations SET [note] ='" + txtdevnote.Text + "'  WHERE [Id] ='" + dk.ToString() + "'";
        SqlCommand cmd3331 = new SqlCommand(s11, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd3331.ExecuteNonQuery();
        con.Close();
        statuslable.Visible = true;
        statuslable.Text = "Record updated successfully";
        GridView1.EditIndex = -1;
        fillgrid();


    }
    protected void ImgBtnMove_Click(object sender, ImageClickEventArgs e)
    {

        ViewState["dle"] = 2;


    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int dk = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        ViewState["Aid"] = dk.ToString();
        GridView1.EditIndex = e.NewEditIndex;
        DataTable ds1 = new DataTable();
        SqlCommand Mycommand = new SqlCommand("Select AttandanceRule.SeniorEmployeeID from AttandanceRule where StoreId =(Select WHID from BatchMaster where Id='" + ddbatch.SelectedValue + "')", con);

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
            fillgrid();
        }
        else
        {
            GridView1.EditIndex = -1;
            fillgrid();
            statuslable.Visible = true;
            statuslable.Text = "Allow only supervisor/admin approved.";

        }



        

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }



    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void ddbatch_SelectedIndexChanged(object sender, EventArgs e)
    {


        ddlemp.Items.Clear();
        DataTable dt11 = new DataTable();
       // dt11 = (DataTable)select(" Select EmployeeMasterID,EmployeeName from EmployeeMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID inner join payperiodtype on payperiodtype.Id=EmployeePayrollMaster.PayPeriodMasterId  inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID   where  EmployeePayrollMaster.Whid=(Select WHID from BatchMaster where Id='" + ddbatch.SelectedValue + "') and  payperiodMaster.Id='" + ddlpayperiod.SelectedValue + "'");
         dt11 = (DataTable)select("select Distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName from EmployeeMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID where Batchmasterid ='" + ddbatch.SelectedValue + "' order by EmployeeName ");

        if (dt11.Rows.Count > 0)
        {
            ddlemp.DataSource = dt11;
            ddlemp.DataTextField = "EmployeeName";
            ddlemp.DataValueField = "EmployeeMasterID";
            ddlemp.DataBind();
        }
        ddlemp_SelectedIndexChanged(sender, e);
       
    }

    protected void ddlemp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpayperiod.Items.Clear();
        DataTable dt = new DataTable();
        dt = (DataTable)select("select distinct Convert(nvarchar,StartDate,101)   as StartDate,Convert(nvarchar,EndDate,101)   as EndDate from [ReportPeriod]   inner join WarehouseMaster on WarehouseMaster.WarehouseId=ReportPeriod.Whid inner join BatchMaster  on WareHouseMaster.WareHouseId=BatchMaster.WHID  where  ReportPeriod.Active='1' and BatchMaster.ID='" + ddbatch.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            DataTable dt1 = new DataTable();
            // dt1 = (DataTable)select("Select Distinct ID,PayperiodName +' : '+Convert(nvarchar,PayperiodStartDate,101) +' : '+ Convert(nvarchar,PayperiodEndDate,101) as period from payperiodMaster inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodMaster.Id where Whid='"+ddlwarehouse.SelectedValue+"' and PayperiodStartDate>='" + dt.Rows[0]["StartDate"] + "' and PayperiodEndDate<='" + dt.Rows[0]["EndDate"] + "'");
            dt1 = (DataTable)select("Select Distinct payperiodMaster.ID,PayperiodName +' : '+Convert(nvarchar,PayperiodStartDate,101) +' : '+ Convert(nvarchar,PayperiodEndDate,101) as period from  payperiodtype inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeePayrollMaster.EmpId where EmployeePayrollMaster.EmpId='"+ddlemp.SelectedValue+"' and Batchmasterid ='" + ddbatch.SelectedValue + "' and PayperiodStartDate>='" + Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToShortDateString() + "' and PayperiodEndDate<='" + Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToShortDateString() + "'");

            if (dt1.Rows.Count > 0)
            {
                // string str = "select EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName + ' : '+cast(EmployeeMasterID as nvarchar(20)) as EmployeeName from EmployeeMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID where Batchmasterid ='" + ddbatch.SelectedValue + "' order by EmployeeName ";

                ddlpayperiod.DataSource = dt1;
                ddlpayperiod.DataValueField = "ID";
                ddlpayperiod.DataTextField = "period";

                ddlpayperiod.DataBind();
            }
        }
        ddlsubsubcat_SelectedIndexChanged(sender, e);
        
        
      
    }
    protected void ddlsubsubcat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpayperiod.SelectedIndex != -1)
        {
            fillgrid();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
    protected void rdlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdlist.SelectedValue == "0")
        {
            pnlpay.Visible = true;
            pnldatewise.Visible = false;
            statuslable.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            //lblheadtext.Text = "List of Single employee pay period Deviations";
            //lblhead.Text = "List of Single employee pay period Deviations ";
            lblheadtext.Text = "List of Employee Time Deviations by Date";
            lblhead.Text = "List of Employee Time Deviations by Date";
        
            ddbatch_SelectedIndexChanged(sender, e);
        }
        else
        {
            lblheadtext.Text = "List of Employee Time Deviations by Date";
            lblhead.Text = "List of Employee Time Deviations by Date";
            pnldatewise.Visible = true;
            pnlpay.Visible = false;
            DateTime fristdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            txtdate.Text = fristdayofmonth.ToShortDateString();
            DateTime lastdaymonth = fristdayofmonth.AddMonths(1).AddDays(-1);
            txttodate.Text = lastdaymonth.ToShortDateString();
            //txtdate.Text = DateTime.Now.ToShortDateString();
            //txttodate.Text = DateTime.Now.ToShortDateString();
            ddldatebatch_SelectedIndexChanged(sender, e);
            GridView1.DataSource = null;
            GridView1.DataBind();

        }


    }
    protected void btnfilter_Click(object sender, EventArgs e)
    {

        SqlCommand Mycommand = new SqlCommand("Select WHID from BatchMaster where Id='" + ddldatebatch.SelectedValue + "'", con);

        SqlDataAdapter MyDataAdapter = new SqlDataAdapter(Mycommand);
        DataTable ds1 = new DataTable();
        MyDataAdapter.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            DataTable dt = new DataTable();
            string str = "select Convert(nvarchar,StartDate,101)   as StartDate,Convert(nvarchar,EndDate,101)   as EndDate from [ReportPeriod] where  Active='1'  and Whid='" + ds1.Rows[0]["WHID"] + "'";
            SqlDataAdapter adp = new SqlDataAdapter(str, con);

            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(dt.Rows[0]["StartDate"]) <= Convert.ToDateTime(txtdate.Text) && Convert.ToDateTime(dt.Rows[0]["EndDate"]) >= Convert.ToDateTime(txttodate.Text))
                {
                    fillgrid();
                }
                else
                {
                    statuslable.Visible = true;
                    statuslable.Text = "Date not in Accounting Year...";
                }
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

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

            if (GridView1.Columns[13].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[13].Visible = false;
            }

        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;

            if (ViewState["edith"] != null)
            {
                GridView1.Columns[13].Visible = true;
            }

        }
    }

    protected void ddldatebatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlempbatch.Items.Clear();
        DataTable dt11 = new DataTable();
        dt11 = (DataTable)select("select distinct EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName from EmployeeMaster inner join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid=EmployeeMaster.EmployeeMasterID where Batchmasterid ='" + ddldatebatch.SelectedValue + "' order by EmployeeName ");

        if (dt11.Rows.Count > 0)
        {
            ddlempbatch.DataSource = dt11;
            ddlempbatch.DataTextField = "EmployeeName";
            ddlempbatch.DataValueField = "EmployeeMasterID";
            ddlempbatch.DataBind();
        }
        ddlempbatch.Items.Insert(0, "All");
        ddlempbatch.Items[0].Value = "0";
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
}
