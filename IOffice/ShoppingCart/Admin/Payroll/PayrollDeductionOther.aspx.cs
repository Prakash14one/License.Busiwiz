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
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class Add_Payroll_Deduction_Other : System.Web.UI.Page
{
    SqlConnection conn=new SqlConnection(PageConn.connnn);
    Int32 accid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        //PageConn pgcon = new PageConn();
        //conn = pgcon.dynconn;
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
            lblCompany.Text = Session["Cname"].ToString();

            ViewState["sortOrder"] = "";
            lblmsg.Visible = false;
            Fillwarehouse();
            fillddlpayperiodtype();
            gridfillemp();
            filterstore();
            GridFill();
            txtestartdate.Text = System.DateTime.Now.ToShortDateString();
            txteenddate.Text = System.DateTime.Now.ToShortDateString();
            rblamount_SelectedIndexChanged(sender, e);
            rblmappacc_SelectedIndexChanged(sender,e);
        }

    }
    protected void Fillwarehouse()
    {

        ddlstrname.Items.Clear();
        //string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "' and [WareHouseMaster].Status='1'";
        //SqlCommand cmd = new SqlCommand(str, conn);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        DataTable ds = ClsStore.SelectStorename();
        ddlstrname.DataSource = ds;
        ddlstrname.DataTextField = "Name";
        ddlstrname.DataValueField = "WareHouseId";
        ddlstrname.DataBind();
        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstrname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

        }

    }
    protected void fillddlpayperiodtype()
    {
        string str = "select * from payperiodtype order by name ";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlpayperiodtype.DataSource = ds;
        ddlpayperiodtype.DataTextField = "Name";
        ddlpayperiodtype.DataValueField = "Id";
        ddlpayperiodtype.DataBind();

        ddlpayper.DataSource = ds;
        ddlpayper.DataTextField = "Name";
        ddlpayper.DataValueField = "Id";
        ddlpayper.DataBind();
    }
    protected void rblmappacc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblmappacc.SelectedValue == "0")
        {
            pnlnewacc.Visible = true;
            FillGroupaccount();
            txtnewacc.Text = txtdeduction.Text;
            pnlacc.Visible = false;
            ddlgrup_SelectedIndexChanged(sender, e);
        }
        else if (rblmappacc.SelectedValue == "1")
        {
            pnlacc.Visible = true;
            Fillaccount();
            pnlnewacc.Visible = false;
        }
    }
    protected void FillGroupaccount()
    {
        ddlgrup.Items.Clear();
        string str1 = "SELECT  GroupCompanyMaster.groupid,ClassCompanyMaster.displayname+':'+GroupCompanyMaster.groupdisplayname as GroupName ,ClassCompanyMaster.displayname  FROM  GroupCompanyMaster INNER JOIN" +
                      " ClassCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid " +
                      " where GroupCompanyMaster.Whid = '" + ddlstrname.SelectedValue+ "' and GroupCompanyMaster.groupid = 15 order by   ClassCompanyMaster.displayname, GroupCompanyMaster.groupdisplayname ";
        SqlCommand cmd1 = new SqlCommand(str1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        ddlgrup.DataSource = ds1;
        ddlgrup.DataTextField = "GroupName";
        ddlgrup.DataValueField = "groupid";
        ddlgrup.DataBind();
    }
    protected void Fillaccount()
    {

        string str1 = "SELECT  AccountMaster.Id,ClassCompanyMaster.displayname+':'+GroupCompanyMaster.groupdisplayname+':'+AccountMaster.AccountName as AccountName,cast(AccountMaster.AccountId as nvarchar) " +
                      " ,ClassTypeCompanyMaster.displayname ,ClassCompanyMaster.displayname , GroupCompanyMaster.groupdisplayname FROM AccountMaster Inner JOIN " +
                      " ClassTypeCompanyMaster Inner JOIN ClassCompanyMaster ON ClassTypeCompanyMaster.id = ClassCompanyMaster.classtypecompanymasterid Inner JOIN" +
                      " GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid ON AccountMaster.GroupId = GroupCompanyMaster.GroupId " +
                      " where AccountMaster.Status=1 and AccountMaster.compid = '" + Session["comid"] + "'and AccountMaster.Whid='" + ddlstrname.SelectedValue + "' and GroupCompanyMaster.whid='" + ddlstrname.SelectedValue + "' order by  ClassTypeCompanyMaster.displayname, ClassCompanyMaster.displayname, GroupCompanyMaster.groupdisplayname, AccountMaster.AccountName ";
        SqlCommand cmd1 = new SqlCommand(str1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        ddlallacc.DataSource = ds1;
        ddlallacc.DataTextField = "AccountName";
        ddlallacc.DataValueField = "Id";
        ddlallacc.DataBind();
        EventArgs e=new EventArgs();
        object sender=new object();
      
    }
    protected void chkallemp_CheckedChanged(object sender, EventArgs e)
    {
       
    }
    protected void gridfillemp()
    {
        String str1 = "select distinct EmployeeMaster.EmployeeMasterID as EmployeeID,EmployeePayrollMaster.PayPeriodMasterId, DepartmentMasterMNC.DepartmentName +' - '+ DesignationMaster.DesignationName+' - '+ EmployeeMaster.EmployeeName as  EmployeeName FROM DesignationMaster LEFT OUTER JOIN DepartmentMasterMNC ON DesignationMaster.Deptid = DepartmentMasterMNC.id RIGHT OUTER JOIN EmployeeMaster ON EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join  EmployeePayrollMaster on  EmployeeMaster.EmployeeMasterID = EmployeePayrollMaster.EmpId where  EmployeeMaster.Whid='" + ddlstrname.SelectedValue + "' order by EmployeeName";
        SqlCommand cmd1 = new SqlCommand(str1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

       
        grdemplist.DataSource = dt1;
        grdemplist.DataBind();

        foreach (GridViewRow grd in grdemplist.Rows)
        {
            CheckBox CheckAdd = (CheckBox)grd.FindControl("CheckAdd");
            CheckAdd.Checked = true;
        }
    }
    protected void rblamount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblamount.SelectedValue == "0")
        {
            pnlfixamount.Visible = true;
            txtpercent.Text = "";
            pnlpercentage.Visible = false;


        }
        else if (rblamount.SelectedValue == "1")
        {
            pnlpercentage.Visible = true;
            txtfixamount.Text = "";
            fillremutype();
            pnlfixamount.Visible = false;
        }
    }
    protected void fillremutype()
    {
        string str1 = "select Id,RemunerationName,AccountMasterId from RemunerationMaster where Whid='" + ddlstrname.SelectedValue + "' and Compid='" + Session["comid"] + "' order by RemunerationName";
        SqlCommand cmd1 = new SqlCommand(str1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            ddlremutype.DataSource = ds1;
            ddlremutype.DataTextField = "RemunerationName";
            ddlremutype.DataValueField = "Id";
            ddlremutype.DataBind();
        }
        else
        {
            ddlremutype.Items.Insert(0, "-Select-");
            ddlremutype.Items[0].Value = "0";
        }
    }
    protected void ddlstrname_SelectedIndexChanged(object sender, EventArgs e)
    {

        Fillaccount();
        fillremutype();
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        if (rblmappacc.SelectedIndex == -1)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please select any option";
        }
        else
        {
            lblmsg.Text = "";
            if (rblmappacc.SelectedValue == "0")
            {
                string scp = "select * from  AccountMaster where AccountName='" + ViewState["accountid"] + "' and Whid='" + ddlstrname.SelectedValue + "' and compid='" + Session["comid"] + "'";
                SqlDataAdapter atp = new SqlDataAdapter(scp, conn);
                DataTable dtp = new DataTable();
                atp.Fill(dtp);
                if (dtp.Rows.Count == 0)
                {
                    string str22 = "Insert into AccountMaster(ClassId,GroupId,AccountId,AccountName,Balance,Balanceoflastyear,DateTimeOfLastUpdatedBalance,Status,compid,Whid)" +
                              "values('" + Session["ClassId"] + "','" + ddlgrup.SelectedValue + "','" + ViewState["accountid"] + "','" + txtnewacc.Text + "','0','0','" + System.DateTime.Now.ToShortDateString() + "','1','" + Session["comid"] + "','" + ddlstrname.SelectedValue + "')";

                    SqlCommand cmd = new SqlCommand(str22, conn);
                    if (conn.State.ToString() != "Open")
                    {
                        conn.Open();

                    }



                    cmd.ExecuteNonQuery();
                    conn.Close();


                    string str1113 = "select max(Id) as Aid from AccountMaster where Whid='" + ddlstrname.SelectedValue + "'";
                    SqlCommand cmd1113 = new SqlCommand(str1113, conn);
                    SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
                    DataTable ds1113 = new DataTable();
                    adp1113.Fill(ds1113);
                    Session["maxaid"] = ds1113.Rows[0]["Aid"].ToString();

                    ViewState["accountid"] = Session["maxaid"].ToString();

                    string st153 = "select Report_Period_Id,Cast(EndDate as Date)  as EndDate  from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + ddlstrname.SelectedValue + "' and Active='1'";
                    SqlCommand cmd153 = new SqlCommand(st153, conn);
                    SqlDataAdapter adp153 = new SqlDataAdapter(cmd153);
                    DataTable ds153 = new DataTable();
                    adp153.Fill(ds153);
                    Session["reportid"] = ds153.Rows[0]["Report_Period_Id"].ToString();

                    string st1531 = "select Report_Period_Id from [ReportPeriod] where ReportPeriod.EndDate<'" + ds153.Rows[0]["EndDate"] + "' and  Whid='" + ddlstrname.SelectedValue + "'  order by EndDate Desc";
                    SqlCommand cmd1531 = new SqlCommand(st1531, conn);
                    SqlDataAdapter adp1531 = new SqlDataAdapter(cmd1531);
                    DataTable ds1531 = new DataTable();
                    adp1531.Fill(ds1531);
                    Session["reportid1"] = ds1531.Rows[0]["Report_Period_Id"].ToString();

                    string str4562 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid1"] + "')";
                    SqlCommand cmd4562 = new SqlCommand(str4562, conn);
                    if (conn.State.ToString() != "Open")
                    {
                        conn.Open();
                    }
                    cmd4562.ExecuteNonQuery();
                    conn.Close();


                    string str456 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid"].ToString() + "')";
                    SqlCommand cmd456 = new SqlCommand(str456, conn);
                    if (conn.State.ToString() != "Open")
                    {
                        conn.Open();
                    }
                    cmd456.ExecuteNonQuery();
                    conn.Close();

                    string str111 = "insert into AccountBalanceLimit(AccountId,BalanceLimitTypeId,BalancelimitAmount,DateTime,Whid) " +
                          " values('" + accid + "','0','0','" + System.DateTime.Now.ToShortDateString() + "','" + ddlstrname.SelectedValue + "')";
                    SqlCommand cmd111 = new SqlCommand(str111, conn);
                    if (conn.State.ToString() != "Open")
                    {
                        conn.Open();
                    }
                    cmd111.ExecuteNonQuery();
                    conn.Close();
                }

            }

            else if (rblmappacc.SelectedValue == "1")
            {
                ViewState["accountid"] = ddlallacc.SelectedValue;

            }

            if (Convert.ToDateTime(txteenddate.Text) < Convert.ToDateTime(txtestartdate.Text))
            {
                lblmsg.Visible = true;
                lblmsg.Text = "End date must be the current date, or greater than the current date";

            }
            else
            {
                string str1 = "select * from payrolldeductionotherstbl where Whid='" + ddlstrname.SelectedValue + "' and MappedAccountId='" + rblmappacc.SelectedValue + "' and Payperiodtypeid='" + ddlpayperiodtype.SelectedValue + "' and StartDate>='" + txtestartdate.Text + "' and EndDate <= '" + txteenddate.Text + "' ";
                SqlCommand cmd1 = new SqlCommand(str1, conn);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                DataTable ds1 = new DataTable();
                adp1.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Sorry, you can not enter new record with the start date falling between the effective period of another record ";

                }
                else
                {
                    string strinsert = "";
                    lblmsg.Text = "";
                    if (rblamount.SelectedIndex == -1)
                    {
                        lblmsg.Text = "Please select any option";
                    }
                    else
                    {
                        if (rblamount.SelectedValue == "0")
                        {
                            lblmsg.Text = "";

                            strinsert = "Insert Into payrolldeductionotherstbl(DeductionName,Payperiodtypeid,MappedAccountId,DeductionAmountPayperiod,FixedAmount,StartDate,EndDate,Status,Whid,Compid,DefaultId) Values ('" + txtdeduction.Text + "','" + ddlpayperiodtype.SelectedValue + "','" + ViewState["accountid"] + "','" + rblamount.SelectedValue + "','" + txtfixamount.Text + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','" + rblstatus.SelectedValue + "','" + ddlstrname.SelectedValue + "','" + Session["comid"] + "','" + rblemployee.SelectedValue + "')";
                            //if (ddlcheckstatus.SelectedValue == "1")
                            //{
                            //    if (chkallemp.Checked == true)
                            //    {
                                    
                            //    }
                            //    else
                            //    {
                            //        strinsert = "Insert Into payrolldeductionotherstbl(DeductionName,Payperiodtypeid,MappedAccountId,DeductionAmountPayperiod,FixedAmount,PercentageOfRemunerationtypeId,StartDate,EndDate,Status,Whid,Compid,DefaultId) Values ('" + txtdeduction.Text + "','" + ddlpayperiodtype.SelectedValue + "','" + ViewState["accountid"] + "','" + rblamount.SelectedValue + "','" + txtfixamount.Text + "','" + ddlremutype.SelectedValue + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','1','" + ddlstrname.SelectedValue + "','" + Session["comid"] + "','0')";
                            //    }
                            //}
                            //else if (ddlcheckstatus.SelectedValue == "0")
                            //{
                            //    if (chkallemp.Checked == true)
                            //    {

                            //        strinsert = "Insert Into payrolldeductionotherstbl(DeductionName,Payperiodtypeid,MappedAccountId,DeductionAmountPayperiod,FixedAmount,PercentageOfRemunerationtypeId,StartDate,EndDate,Status,Whid,Compid,DefaultId) Values ('" + txtdeduction.Text + "','" + ddlpayperiodtype.SelectedValue + "','" + ViewState["accountid"] + "','" + rblamount.SelectedValue + "','" + txtfixamount.Text + "','" + ddlremutype.SelectedValue + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','0','" + ddlstrname.SelectedValue + "','" + Session["comid"] + "','1')";
                            //    }
                            //    else
                            //    {
                            //        strinsert = "Insert Into payrolldeductionotherstbl(DeductionName,Payperiodtypeid,MappedAccountId,DeductionAmountPayperiod,FixedAmount,PercentageOfRemunerationtypeId,StartDate,EndDate,Status,Whid,Compid,DefaultId) Values ('" + txtdeduction.Text + "','" + ddlpayperiodtype.SelectedValue + "','" + ViewState["accountid"] + "','" + rblamount.SelectedValue + "','" + txtfixamount.Text + "','" + ddlremutype.SelectedValue + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','0','" + ddlstrname.SelectedValue + "','" + Session["comid"] + "','0')";
                            //    }
                            //}
                            SqlCommand cmd = new SqlCommand(strinsert, conn);
                            if (conn.State.ToString() != "Open")
                            {
                                conn.Open();
                            }
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        else if (rblamount.SelectedValue == "1")
                        {
                            strinsert = "Insert Into payrolldeductionotherstbl(DeductionName,Payperiodtypeid,MappedAccountId,DeductionAmountPayperiod,PercentageOf,PercentageOfRemunerationtypeId,StartDate,EndDate,Status,Whid,Compid,DefaultId) Values ('" + txtdeduction.Text + "','" + ddlpayper.SelectedValue + "','" + ViewState["accountid"] + "','" + rblamount.SelectedValue + "','" + txtpercent.Text + "','" + ddlremutype.SelectedValue + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','" + rblstatus.SelectedValue + "','" + ddlstrname.SelectedValue + "','" + Session["comid"] + "','" + rblemployee.SelectedValue + "')";
                            //if (ddlcheckstatus.SelectedValue == "1")
                            //{
                            //    if (chkallemp.Checked == true)
                            //    {
                                    
                            //    }
                            //    else
                            //    {
                            //        strinsert = "Insert Into payrolldeductionotherstbl(DeductionName,Payperiodtypeid,MappedAccountId,DeductionAmountPayperiod,PercentageOf,PercentageOfRemunerationtypeId,StartDate,EndDate,Status,Whid,Compid,DefaultId) Values ('" + txtdeduction.Text + "','" + ddlpayperiodtype.SelectedValue + "','" + ViewState["accountid"] + "','" + rblamount.SelectedValue + "','" + txtpercent.Text + "','" + ddlremutype.SelectedValue + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','1','" + ddlstrname.SelectedValue + "','" + Session["comid"] + "','0')";
                            //    }


                            //}
                            //else if (ddlcheckstatus.SelectedValue == "0")
                            //{
                            //    if (chkallemp.Checked == true)
                            //    {
                            //        strinsert = "Insert Into payrolldeductionotherstbl(DeductionName,Payperiodtypeid,MappedAccountId,DeductionAmountPayperiod,PercentageOf,PercentageOfRemunerationtypeId,StartDate,EndDate,Status,Whid,Compid,DefaultId) Values ('" + txtdeduction.Text + "','" + ddlpayperiodtype.SelectedValue + "','" + ViewState["accountid"] + "','" + rblamount.SelectedValue + "','" + txtpercent.Text + "','" + ddlremutype.SelectedValue + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','0','" + ddlstrname.SelectedValue + "','" + Session["comid"] + "','1')";
                            //    }
                            //    else
                            //    {
                            //        strinsert = "Insert Into payrolldeductionotherstbl(DeductionName,Payperiodtypeid,MappedAccountId,DeductionAmountPayperiod,PercentageOf,PercentageOfRemunerationtypeId,StartDate,EndDate,Status,Whid,Compid,DefaultId) Values ('" + txtdeduction.Text + "','" + ddlpayperiodtype.SelectedValue + "','" + ViewState["accountid"] + "','" + rblamount.SelectedValue + "','" + txtpercent.Text + "','" + ddlremutype.SelectedValue + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','0','" + ddlstrname.SelectedValue + "','" + Session["comid"] + "','0')";
                            //    }
                            //}
                            SqlCommand cmd = new SqlCommand(strinsert, conn);
                            if (conn.State.ToString() != "Open")
                            {
                                conn.Open();
                            }
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        string str2 = "Select Max(Id) as deductionid from payrolldeductionotherstbl";
                        SqlCommand cmd2 = new SqlCommand(str2, conn);
                        SqlDataAdapter adpt = new SqlDataAdapter(cmd2);
                        DataSet ds = new DataSet();
                        adpt.Fill(ds);
                        int id = Convert.ToInt32(ds.Tables[0].Rows[0]["deductionid"].ToString());
                        foreach (GridViewRow grd in grdemplist.Rows)
                        {
                            //CheckBox chk = grd.Cells[0].Controls[0] as CheckBox;

                            CheckBox chk = (CheckBox)grd.FindControl("CheckAdd");
                            Label emp = (Label)grd.FindControl("lblempid");
                            if (chk.Checked == true)
                            {
                                
                                string strinset = "Insert into Deductionbydefault(DeductionId,EmployeeId,status) values('" + id + "','" + emp.Text + "','1')";
                                SqlCommand cmd12 = new SqlCommand(strinset, conn);
                                if (conn.State.ToString() != "Open")
                                {
                                    conn.Open();
                                }
                                cmd12.ExecuteNonQuery();
                                conn.Close();
                            }

                        }
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted successfully";
                        GridFill();
                        clearall();
                        pnlddlfill.Visible = false;
                        btnadd.Visible = true;
                        lbladd.Text = "";
                    }
                }
            }
        }
    }
    protected void checkemp_CheckedChanged(object sender, EventArgs e)
    {
        //CheckBox chk;
        //foreach (GridView row in grdemplist.Rows)
        //{
        //    chk = (CheckBox)(row.FindControl("CheckAdd"));
        //    chk.Checked = ((CheckBox)sender).Checked;
        //}

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        lblmsg.Visible = false;
        lblmsg.Text = "";
        clearall();
        pnlddlfill.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";

    }
    protected void clearall()
    {
        rblmappacc.Enabled = true;
        txtdeduction.Text = "";
        ddlstrname.SelectedIndex = 0;
        rblmappacc.SelectedIndex = -1;
        txtnewacc.Text = "";
       // chkallemp.Checked = true;
        rblamount.SelectedIndex = -1;
        rblemployee.SelectedIndex = 0;
        rblstatus.SelectedIndex = 0;
        txtfixamount.Text = "";
        txtpercent.Text = "";
        if (pnlnewacc.Visible == true)
        {
            ddlgrup.SelectedIndex = 0;
        }
        if (pnlacc.Visible == true)
        {
            ddlallacc.SelectedIndex = 0;
        }
        if (pnlpercentage.Visible == true)
        {
            ddlremutype.SelectedIndex = 0;
        }
        ddlpayperiodtype.SelectedIndex = 0;
        ddlpayper.SelectedIndex = 0;
        txtestartdate.Text = System.DateTime.Now.ToShortDateString();
        txteenddate.Text = System.DateTime.Now.ToShortDateString();
        pnlnewacc.Visible = false;
        pnlacc.Visible = false;
        pnlfixamount.Visible = false;
        pnlpercentage.Visible = false;
        pnlemplist.Visible = false;
        txtestartdate.Enabled = true;
    }
    protected void GridFill()
    {

        string str;
        if (ddlstrFilter.SelectedIndex > 0)
        {
            //str = "select payrolldeductionotherstbl.*,case when(payrolldeductionotherstbl.Status = '1') then 'Active' else 'Inactive' end as active,EmployeeMaster.AccountId,AccountMaster.AccountName,AccountMaster.Id,EmployeePayrollMaster.EmpId,EmployeePayrollMaster.PayPeriodMasterId,payperiodtype.Name as Period_Type,payperiodtype.periodmasterid,RemunerationMaster.Id,RemunerationMaster.RemunerationName,WareHouseMaster.WareHouseId,WareHouseMaster.Name from  payrolldeductionotherstbl left outer join AccountMaster on payrolldeductionotherstbl.MappedAccountId = AccountMaster.Id left join RemunerationMaster on payrolldeductionotherstbl.PercentageOfRemunerationtypeId =  RemunerationMaster.Id inner join WareHouseMaster on payrolldeductionotherstbl.Whid = WareHouseMaster.WareHouseId inner join Deductionbydefault on payrolldeductionotherstbl.Id = Deductionbydefault.DeductionId inner join EmployeePayrollMaster on EmployeeMaster.EmployeeMasterID= EmployeePayrollMaster.EmpId inner join payperiodtype on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.periodmasterid where payrolldeductionotherstbl.Whid = '" + ddlstrFilter.SelectedValue + "' and payrolldeductionotherstbl.Compid='" + Session["comid"] + "'";
            str = "select payrolldeductionotherstbl.*,cast(payrolldeductionotherstbl.PercentageOf as nvarchar) +'% of '+ RemunerationMaster.RemunerationName as remper,case when(payrolldeductionotherstbl.Status = '1') then 'Active' else 'Inactive' end as active,AccountMaster.AccountName,AccountMaster.Id,payperiodtype.Name as paypername,RemunerationMaster.Id,WareHouseMaster.WareHouseId,WareHouseMaster.Name from  payrolldeductionotherstbl left join AccountMaster on payrolldeductionotherstbl.MappedAccountId = AccountMaster.Id left join RemunerationMaster on payrolldeductionotherstbl.PercentageOfRemunerationtypeId =  RemunerationMaster.Id inner join WareHouseMaster on payrolldeductionotherstbl.Whid = WareHouseMaster.WareHouseId inner join payperiodtype on payrolldeductionotherstbl.Payperiodtypeid=payperiodtype.Id where payrolldeductionotherstbl.Whid = '" + ddlstrFilter.SelectedValue + "' and payrolldeductionotherstbl.Compid='" + Session["comid"] + "' order by Name, DeductionName, paypername, AccountName ";
        }
        else
        {
            //str = "select payrolldeductionotherstbl.*,case when(payrolldeductionotherstbl.Status = '1') then 'Active' else 'Inactive' end as active,EmployeeMaster.AccountId,AccountMaster.AccountName,AccountMaster.Id,EmployeePayrollMaster.EmpId,EmployeePayrollMaster.PayPeriodMasterId,payperiodtype.Name as Period_Type,payperiodtype.periodmasterid,RemunerationMaster.Id,RemunerationMaster.RemunerationName,WareHouseMaster.WareHouseId,WareHouseMaster.Name from  payrolldeductionotherstbl left outer join AccountMaster on payrolldeductionotherstbl.MappedAccountId = AccountMaster.Id left join RemunerationMaster on payrolldeductionotherstbl.PercentageOfRemunerationtypeId =  RemunerationMaster.Id inner join WareHouseMaster on payrolldeductionotherstbl.Whid = WareHouseMaster.WareHouseId inner join Deductionbydefault on payrolldeductionotherstbl.Id = Deductionbydefault.DeductionId inner join EmployeePayrollMaster on EmployeeMaster.EmployeeMasterID= EmployeePayrollMaster.EmpId inner join payperiodtype on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.periodmasterid where payrolldeductionotherstbl.Compid='" + Session["comid"] + "'";
            str = "select payrolldeductionotherstbl.*,cast(payrolldeductionotherstbl.PercentageOf as nvarchar) +'% of '+ RemunerationMaster.RemunerationName as remper,case when(payrolldeductionotherstbl.Status = '1') then 'Active' else 'Inactive' end as active,AccountMaster.AccountName,AccountMaster.Id,payperiodtype.Name as paypername,RemunerationMaster.Id,WareHouseMaster.WareHouseId,WareHouseMaster.Name from  payrolldeductionotherstbl left join AccountMaster on payrolldeductionotherstbl.MappedAccountId = AccountMaster.Id left join RemunerationMaster on payrolldeductionotherstbl.PercentageOfRemunerationtypeId =  RemunerationMaster.Id inner join WareHouseMaster on payrolldeductionotherstbl.Whid = WareHouseMaster.WareHouseId inner join payperiodtype on payrolldeductionotherstbl.Payperiodtypeid=payperiodtype.Id where payrolldeductionotherstbl.Compid='" + Session["comid"] + "' order by Name, DeductionName, paypername, AccountName";
        }
        SqlCommand cmd1 = new SqlCommand(str, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);

        DataView myDataView = new DataView();
        myDataView = ds1.Tables[0].DefaultView;

        GridView1.DataSource = myDataView;
        GridView1.DataBind();

        foreach (GridViewRow grd in GridView1.Rows)
        {
            Label lblid = (Label)grd.FindControl("lblid");
            Label lbldefaultid = (Label)grd.FindControl("lbldefaultid");
            Label lblempname = (Label)grd.FindControl("lblempname");

            if (lbldefaultid.Text == "0")
            {
                lblempname.Text = "All Employees";
            }
            if (lbldefaultid.Text == "1")
            {
                string invsite = "";
                string str1 = "select Deductionbydefault.*,EmployeeMaster.EmployeeName from Deductionbydefault inner join EmployeeMaster on Deductionbydefault.EmployeeId = EmployeeMaster.EmployeeMasterID  where DeductionId = '" + lblid.Text + "' and status = '1'";
               // String str1 = "select Deductionbydefault.*,DepartmentMasterMNC.DepartmentName +' - '+ DesignationMaster.DesignationName+' - '+ EmployeeMaster.EmployeeName as  EmployeeName FROM DesignationMaster LEFT OUTER JOIN DepartmentMasterMNC ON DesignationMaster.Deptid = DepartmentMasterMNC.id RIGHT OUTER JOIN EmployeeMaster ON EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join Deductionbydefault on Deductionbydefault.EmployeeId = EmployeeMaster.EmployeeMasterID where  Deductionbydefault.DeductionId = '" + lblid.Text + "' and status = '1' order by EmployeeName";
                SqlDataAdapter adpt = new SqlDataAdapter(str1, conn);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    
                    foreach (DataRow dr in dt.Rows)
                    {

                        invsite = invsite + dr["EmployeeName"].ToString() + ", ";

                    }
                    lblempname.Text = invsite;
                    string st = lblempname.Text.Substring(0, lblempname.Text.Length - 2);
                    lblempname.Text = st;
                }
            }
        }
        lblBusiness.Text = ddlstrFilter.SelectedItem.Text;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlstrFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridFill();
    }
    protected void filterstore()
    {

        //string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "' and [WareHouseMaster].Status='1'";
        //SqlCommand cmd = new SqlCommand(str, conn);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        DataTable ds = ClsStore.SelectStorename();
        ddlstrFilter.DataSource = ds;
        ddlstrFilter.DataTextField = "Name";
        ddlstrFilter.DataValueField = "WareHouseId";
        ddlstrFilter.DataBind();
        ddlstrFilter.Items.Insert(0, "All");
        ddlstrFilter.Items[0].Value = "0";


    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "vi")
        {
            lblmsg.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["Id"] = GridView1.SelectedIndex;
            //GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
            Btn_Submit.Visible = false;
            btncancel.Visible = false;
            btnUpdate.Visible = true;
            btncancel1.Visible = true;
            string str12 = "select * from payrolldeductionotherstbl where Id='" + GridView1.SelectedIndex + "'";
            SqlCommand cmd1 = new SqlCommand(str12, conn);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            adp1.Fill(ds1);

            Fillwarehouse();
            ddlstrname.SelectedIndex = ddlstrname.Items.IndexOf(ddlstrname.Items.FindByValue(ds1.Rows[0]["Whid"].ToString()));
            txtdeduction.Text = ds1.Rows[0]["DeductionName"].ToString();
           // fillddlpayperiodtype();
            ddlpayperiodtype.SelectedIndex = ddlpayperiodtype.Items.IndexOf(ddlpayperiodtype.Items.FindByValue(ds1.Rows[0]["Payperiodtypeid"].ToString()));
            rblmappacc.SelectedIndex = rblmappacc.Items.IndexOf(rblmappacc.Items.FindByValue(ds1.Rows[0]["MappedAccountId"].ToString()));
            ViewState["accid"] = Convert.ToInt32(ds1.Rows[0]["MappedAccountId"].ToString());

            rblmappacc.SelectedValue = "1";
            pnlnewacc.Visible = false;
            pnlacc.Visible = true;
            Fillaccount();
            ddlallacc.SelectedIndex = ddlallacc.Items.IndexOf(ddlallacc.Items.FindByValue(ds1.Rows[0]["MappedAccountId"].ToString()));

            //gridfillemp();
            if (ds1.Rows[0]["DefaultId"].ToString() == "0")
            {
                pnlemplist.Visible = false;
                rblemployee.SelectedValue = "0";
            }
            else
            {
                pnlemplist.Visible = true;
                rblemployee.SelectedValue = "1";
                foreach (GridViewRow grd in grdemplist.Rows)
                {
                    CheckBox chk = (CheckBox)grd.FindControl("CheckAdd");
                    //chk.Checked = false;
                    Label emp = (Label)grd.FindControl("lblempid");
                    string str = "Select * from Deductionbydefault where EmployeeId = '" + emp.Text + "' and DeductionId='" + ViewState["Id"] + "'";
                    SqlCommand cmd2 = new SqlCommand(str, conn);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd2);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Int32 empid = Convert.ToInt32(ds.Tables[0].Rows[0]["EmployeeId"].ToString());
                        if (ds.Tables[0].Rows[0]["status"].ToString() == "True")
                        {
                            chk.Checked = true;
                        }
                        else
                        {
                            chk.Checked = false;
                        }
                    }
                }
            }
            rblamount.SelectedIndex = rblamount.Items.IndexOf(rblamount.Items.FindByValue(ds1.Rows[0]["DeductionAmountPayperiod"].ToString()));
            Int32 temp1 = Convert.ToInt32(ds1.Rows[0]["DeductionAmountPayperiod"].ToString());
            if (temp1 == 0)
            {
                pnlfixamount.Visible = true;
                pnlpercentage.Visible = false;
                txtfixamount.Text = ds1.Rows[0]["FixedAmount"].ToString();
            }
            else
            {
                pnlfixamount.Visible = false;
                pnlpercentage.Visible = true;
                txtpercent.Text = ds1.Rows[0]["PercentageOf"].ToString();
                fillremutype();
                ddlremutype.SelectedIndex = ddlremutype.Items.IndexOf(ddlremutype.Items.FindByValue(ds1.Rows[0]["PercentageOfRemunerationtypeId"].ToString()));
                ddlpayper.SelectedIndex = ddlpayper.Items.IndexOf(ddlpayper.Items.FindByValue(ds1.Rows[0]["Payperiodtypeid"].ToString()));
   
            }

            txtestartdate.Text = ds1.Rows[0]["StartDate"].ToString();
            txtestartdate.Enabled = false;
            txteenddate.Text = ds1.Rows[0]["EndDate"].ToString();
            Boolean temp2 = Convert.ToBoolean(ds1.Rows[0]["Status"].ToString());
            if (temp2 == false)
            {
                rblstatus.SelectedValue = "0";
            }
            else
            {
                rblstatus.SelectedValue = "1";
            }
            rblmappacc.Enabled = false;
            pnlddlfill.Visible = true;
            btnadd.Visible = false;
            lbladd.Text = "Edit Business Deduction";
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (rblmappacc.SelectedIndex == -1)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please select any option";
        }
        else
        {
           
            if (Convert.ToDateTime(txteenddate.Text) >= Convert.ToDateTime(txtestartdate.Text))
            {
                string strinsert = "";
                lblmsg.Text = "";
                if (rblamount.SelectedIndex == -1)
                {
                    lblmsg.Text = "Please select any option";
                }
                else if (rblamount.SelectedValue == "0")
                {
                    lblmsg.Text = "";
                    strinsert = "Update payrolldeductionotherstbl set DeductionName = '" + txtdeduction.Text + "',Payperiodtypeid = '" + ddlpayperiodtype.SelectedValue + "',MappedAccountId = '" + ddlallacc.SelectedValue + "',DeductionAmountPayperiod = '" + rblamount.SelectedValue + "',PercentageOf='0',PercentageOfRemunerationtypeId='0',FixedAmount = '" + txtfixamount.Text + "',StartDate = '" + txtestartdate.Text + "',EndDate = '" + txteenddate.Text + "',Status = '" + rblstatus.SelectedValue + "',Whid = '" + ddlstrname.SelectedValue + "',Compid = '" + Session["comid"] + "',DefaultId='" + rblemployee.SelectedValue + "' where Id = '" + ViewState["Id"] + "'";
                   
                }

                else if (rblamount.SelectedValue == "1")
                {
                    strinsert = "Update payrolldeductionotherstbl set DeductionName = '" + txtdeduction.Text + "',Payperiodtypeid = '" + ddlpayper.SelectedValue + "',MappedAccountId = '" + ddlallacc.SelectedValue + "',DeductionAmountPayperiod = '" + rblamount.SelectedValue + "',FixedAmount='0',PercentageOf = '" + txtpercent.Text + "',PercentageOfRemunerationtypeId = '" + ddlremutype.SelectedValue + "',StartDate = '" + txtestartdate.Text + "',EndDate = '" + txteenddate.Text + "',Status = '" + rblstatus.SelectedValue + "',Whid = '" + ddlstrname.SelectedValue + "',Compid = '" + Session["comid"] + "',DefaultId='" + rblemployee.SelectedValue + "' where Id = '" + ViewState["Id"] + "'";
                  
                }
                SqlCommand cmd = new SqlCommand(strinsert, conn);
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                conn.Close();



                foreach (GridViewRow grd in grdemplist.Rows)
                {
                    CheckBox chk = (CheckBox)grd.FindControl("CheckAdd");
                    Label emp = (Label)grd.FindControl("lblempid");
                    string str = "Select * from Deductionbydefault where EmployeeId='" + emp.Text + "' and DeductionId='" + ViewState["Id"] + "'";
                    SqlCommand cmd2 = new SqlCommand(str, conn);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd2);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string strupdate = "Update Deductionbydefault set status = '" + chk.Checked + "' where  EmployeeId='" + emp.Text + "' and DeductionId='" + ViewState["Id"] + "'";
                        SqlCommand cmdupdate = new SqlCommand(strupdate, conn);
                        if (conn.State.ToString() != "Open")
                        {
                            conn.Open();
                        }
                        cmdupdate.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        string strin = "Insert into Deductionbydefault(DeductionId,EmployeeId,status) values ('" + ViewState["Id"] + "','" + emp.Text + "','" + chk.Checked + "')";
                        SqlCommand cmdin = new SqlCommand(strin, conn);
                        if (conn.State.ToString() != "Open")
                        {
                            conn.Open();
                        }
                        cmdin.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                rblmappacc.Enabled =true;
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";
                GridFill();
                clearall();
                btncancel1.Visible = false;
                btnUpdate.Visible = false;
                Btn_Submit.Visible = true;
                btncancel.Visible = true;
                pnlddlfill.Visible = false;
                btnadd.Visible = true;
                lbladd.Text = "";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "End date must be the current date, or greater than the current date";
            }
        }
    }
    protected void ddlgrup_SelectedIndexChanged(object sender, EventArgs e)
    {
        grupfill();
    }
    protected void grupfill()
    {
        string str112 = "SELECT distinct ClassCompanyMaster.classid,GroupMaster.ClassId,GroupCompanyMaster.groupdisplayname,GroupCompanyMaster.groupid from ClassCompanyMaster inner join  GroupMaster on  ClassCompanyMaster.classid=GroupMaster.ClassId inner join GroupCompanyMaster on GroupCompanyMaster.groupid=GroupMaster.GroupId  where GroupCompanyMaster.cid='" + Session["comid"] + "' and  GroupCompanyMaster.groupid='" + ddlgrup.SelectedValue + "' order by GroupCompanyMaster.groupdisplayname";

        SqlCommand cmd = new SqlCommand(str112, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            Session["ClassId"] = dt.Rows[0]["classid"].ToString();
        }
        ddlgroup_SelectedIndexChanged();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblmsg.Text = "";
        string st2 = "Delete from payrolldeductionotherstbl where Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, conn);
        if (conn.State.ToString() != "Open")
        {
            conn.Open();
        }
        cmd2.ExecuteNonQuery();
        conn.Close();
        GridView1.EditIndex = -1;
        GridFill();
        lblmsg.Visible = true;
        lblmsg.Text = "Record deleted successfully";
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[11].Visible == true)
            {
                ViewState["docs"] = "tt";
                GridView1.Columns[11].Visible = false;
            }

            if (GridView1.Columns[12].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[12].Visible = false;
            }

        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["docs"] != null)
            {
                GridView1.Columns[11].Visible = true;
            }
            if (ViewState["edith"] != null)
            {
                GridView1.Columns[12].Visible = true;
            }

        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        GridFill();
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
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        lblmsg.Visible = false;
        lblmsg.Text = "";
        clearall();

        Btn_Submit.Visible = true;
        btncancel.Visible = true;
        btnUpdate.Visible = false;
        btncancel1.Visible = false;
        pnlddlfill.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";
    }

    protected void selectid(string id)
    {

        string scit = "Select Max(AccountId) as aid from AccountMaster where GroupId='" + id + "' and Whid='" + ddlstrFilter.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(scit, conn);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["aid"].ToString() != "")
            {
                accid = Convert.ToInt32(dt.Rows[0]["aid"].ToString());
                acccc(accid);
            }
        }
    }
    protected void acccc(int accgenid)
    {

        DataTable dtrs = select("select AccountId from AccountMaster where AccountId='" + (accgenid + 1) + "' and Whid='" + ddlstrname.SelectedValue + "'");
        if (dtrs.Rows.Count > 0)
        {
            accid += 1;
            acccc(accid);
        }

    }
    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
    protected void ddlgroup_SelectedIndexChanged()
    {

        if (ddlgrup.SelectedValue != null)
        {
            if (ddlgrup.SelectedValue == "1")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 1099)
                {
                    if (accid > 1000000)
                    {
                        accid += 1;

                    }
                    else
                    {
                        accid = 1000000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 1000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }

                }

            }
            else if (ddlgrup.SelectedValue.ToString() == "2")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 29999)
                {
                    if (accid > 100000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 100000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 10000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }

                
                //ModalPopupExtender142422.Show();
            }
            else if (ddlgrup.SelectedValue.ToString() == "3")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 1399)
                {
                    if (accid > 11000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 11000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 1100;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "4")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 1699)
                {
                    if (accid > 14000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 14000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 1400;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "5")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 1999)
                {
                    if (accid > 17000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 17000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 1700;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "6")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 2099)
                {
                    if (accid > 20000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 20000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 2000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "7")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 2199)
                {
                    if (accid > 21000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 21000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 2100;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "8")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 2299)
                {
                    if (accid > 22000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 22000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 2200;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "9")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 2399)
                {
                    if (accid > 23000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 23000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 2300;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "10")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 2499)
                {
                    if (accid > 24000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 24000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 2400;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "11")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 2599)
                {
                    if (accid > 25000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 25000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 2500;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "12")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 2699)
                {
                    if (accid > 26000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 26000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 2600;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "13")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 2799)
                {
                    if (accid > 27000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 27000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 2700;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "14")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 2899)
                {
                    if (accid > 28000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 28000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 2800;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "15")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 30000)
                {
                    accid = accid + 1;
                }
                else
                {
                    accid = 30000;
                }
                

               // ModalPopupExtender142422.Show();

            }
            else if (ddlgrup.SelectedValue.ToString() == "16")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 2999)
                {
                    if (accid > 29000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 29000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 2900;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "17")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 3099)
                {
                    if (accid > 30000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 30000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 3000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "18")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 3199)
                {
                    if (accid > 31000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 31000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 3100;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "19")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 3299)
                {
                    if (accid > 32000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 32000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 3200;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "20")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 3999)
                {
                    if (accid > 33000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 33000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 3300;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "21")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 4099)
                {
                    if (accid > 40000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 40000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 4000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "22")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 4199)
                {
                    if (accid > 41000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 41000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 4100;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "23")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 4499)
                {
                    if (accid > 42000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 42000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 4200;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "24")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 4599)
                {
                    if (accid > 45000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 45000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 4500;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "25")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 4699)
                {
                    if (accid > 46000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 46000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 4600;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "26")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 4999)
                {
                    if (accid > 4700)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 4700;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 4700;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "33")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 5499)
                {
                    if (accid > 50000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 50000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 5000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "34")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 5999)
                {
                    if (accid > 55000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 55000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 5500;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "35")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 6999)
                {
                    if (accid > 60000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 60000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 6000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }


            else if (ddlgrup.SelectedValue.ToString() == "36")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 7499)
                {
                    if (accid > 70000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 70000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 7000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "37")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 7999)
                {
                    if (accid > 75000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 75000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 7500;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "38")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 8999)
                {
                    if (accid > 80000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 80000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 8000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "39")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 9099)
                {
                    if (accid > 90000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 90000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 9000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "43")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 9199)
                {
                    if (accid > 91000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 91000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 9100;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "44")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 9299)
                {
                    if (accid > 92000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 92000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 9200;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "45")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 9399)
                {
                    if (accid > 93000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 93000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 9300;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "46")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 9499)
                {
                    if (accid > 94000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 94000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 9400;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "47")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 9549)
                {
                    if (accid > 95000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 95000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 9500;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "48")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 9599)
                {
                    if (accid > 95500)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 95500;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 9550;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "49")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 9699)
                {
                    if (accid > 96000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 96000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 9600;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "50")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 9799)
                {
                    if (accid > 97000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 97000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 9700;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "51")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 9899)
                {
                    if (accid > 98000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 98000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 9800;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "52")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 9949)
                {
                    if (accid > 9950)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9950;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 9900;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "53")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 990999)
                {
                    if (accid > 9900000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9900000;
                    }
                }
                else
                {
                    accid = accid + 1;
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "54")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 991999)
                {
                    if (accid > 9910000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9910000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 990000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "55")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 992999)
                {
                    if (accid > 9920000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9920000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 991000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "56")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 993999)
                {
                    if (accid > 9930000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9930000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 993000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "57")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 994999)
                {
                    if (accid > 9940000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9940000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 994000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "58")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 995999)
                {
                    if (accid > 9950000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9950000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 995000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "59")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 996499)
                {
                    if (accid > 9960000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9960000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 996000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "60")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 996999)
                {
                    if (accid > 9965000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9965000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 996500;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "61")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 997499)
                {
                    if (accid > 9970000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9970000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 997000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "62")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 997999)
                {
                    if (accid > 9975000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9975000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 997500;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "63")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 998499)
                {
                    if (accid > 9980000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9980000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 998000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "64")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 998999)
                {
                    if (accid > 9985000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9985000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 998500;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "65")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                if (accid >= 999499)
                {
                    if (accid > 9990000)
                    {
                        accid += 1;
                    }
                    else
                    {
                        accid = 9990000;
                    }
                }
                else
                {
                    if (accid == 0)
                    {
                        accid = 999000;
                    }
                    else
                    {
                        accid = accid + 1;
                    }
                }
            }
            else if (ddlgrup.SelectedValue.ToString() == "66")
            {
                selectid(ddlgrup.SelectedValue.ToString());
                //if (accid >= 999499)
                //{
                //    if (accid > 9990000)
                //    {
                //        accid += 1;
                //    }
                //    else
                //    {
                //        accid = 9990000;
                //    }
                //}
                //else
                //{
                if (accid == 0)
                {
                    accid = 999500;
                }
                else
                {
                    accid = accid + 1;
                }
                //}
            }
            ViewState["accountid"] = Convert.ToString(accid);
        }
        else
        {
            //  ddlgroup.Items.Clear();
            //   ddlgroup.Items.Insert(0, "--Select--");
            //   ddlgroup.Items[0].Value = "0";

            // txtaccount.Text = "";
        }


    }
    
    public DataTable fillddl(String qry)
    {
        SqlCommand cmd = new SqlCommand(qry, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;
    }
    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.Items.Clear();
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();
        //ddl.Items.Insert(0, "--Select--");
        //ddl.Items[0].Value = "0";
    }
   
    
    
    protected void ImageButton3_Click1(object sender, ImageClickEventArgs e)
    {
       // ModalPopupExtender142422.Hide();
    }
    
   
   
  

    protected void btnOk0_Click(object sender, EventArgs e)
    {

    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        pnlddlfill.Visible = true;
        btnadd.Visible = false;
        lbladd.Text = "Add New Business Deduction";
        lblmsg.Text = "";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridFill();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblemployee.SelectedValue == "0")
        {
            pnlemplist.Visible = false;
            gridfillemp();
        }
        if (rblemployee.SelectedValue == "1")
        {
            pnlemplist.Visible = true;
            gridfillemp();
        }
    }
}
