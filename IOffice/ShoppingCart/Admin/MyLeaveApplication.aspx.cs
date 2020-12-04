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

public partial class Add_Leave_Request_Master : System.Web.UI.Page
{
    

    SqlConnection con;
    String Compid;
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
        Compid = Session["Comid"].ToString();
        ViewState["Compid"] = Session["Comid"].ToString();
        ViewState["UserName"] = Session["userid"].ToString();
        Page.Title = pg.getPageTitle(page);

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            lblmsg.Text = "";

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }

            if (Request.QueryString["Id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                pnladd.Visible = true;
                add.Visible = false;

                fillstore();
                txtStartDate.Text = System.DateTime.Now.ToShortDateString();
                txtEndDate.Text = System.DateTime.Now.ToShortDateString();
                txtfromdt.Text = System.DateTime.Now.ToShortDateString();
                txttodt.Text = System.DateTime.Now.ToShortDateString();
                fillleaveearned();
                getSupervisor();
                FillLeaveType();
                ddlLeaveType.SelectedIndex = ddlLeaveType.Items.IndexOf(ddlLeaveType.Items.FindByValue(id.ToString()));

                filldata();
                ddlstrname_SelectedIndexChanged(sender, e);
            }


            else
            {

                fillstore();
                txtStartDate.Text = System.DateTime.Now.ToShortDateString();
                txtEndDate.Text = System.DateTime.Now.ToShortDateString();
                txtfromdt.Text = System.DateTime.Now.ToShortDateString();
                txttodt.Text = System.DateTime.Now.ToShortDateString();
                fillleaveearned();
                getSupervisor();
                FillLeaveType();

                filldata();
                ddlstrname_SelectedIndexChanged(sender, e);
            }
        }
    }
    protected void fillstore()
    {


        DataTable ds = ClsStore.SelectStorename();
        ddlstrname.DataSource = ds;
        ddlstrname.DataTextField = "Name";
        ddlstrname.DataValueField = "WareHouseId";
        ddlstrname.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstrname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
    
    protected void fillleaveearned()
    {
        ViewState["payid"] = "";
        DataTable dt = select("Select DateOfJoin,DesignationMasterId,Whid from  EmployeeMaster where EmployeeMaster.EmployeeMasterId='" + Session["EmployeeId"] + "'");
        if (dt.Rows.Count > 0)
        {
            DataTable dt1 = select("Select  payperiodMaster.ID, payperiodMaster.PayperiodStartDate,payperiodMaster.PayperiodEndDate from payperiodtype inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id where EmpId='" + Session["EmployeeID"] + "' and PayperiodStartDate<='" + DateTime.Now.ToShortDateString() + "' and PayperiodEndDate>='" + DateTime.Now.ToShortDateString() + "'");

            if (dt1.Rows.Count > 0)
            {
                ViewState["payid"] = dt1.Rows[0]["ID"];

            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry,Your Pay information not set out";
            }
            string stc = "";
            DataTable dt2 = select("Select EmployeeLeaveType.* from  EmployeeLeaveType left join DesignationMaster on DesignationMaster.DesignationmasterId=EmployeeLeaveType.DesignationId where EmployeeLeaveType.Whid='" + dt.Rows[0]["Whid"] + "' and (EmployeeLeaveType.DesignationId='" + dt.Rows[0]["DesignationMasterId"] + "' or EmployeeLeaveType.DesignationId IS NULL or EmployeeLeaveType.DesignationId='0' ) and EmployeeLeaveType.Leaveencashable='1'");
            foreach (DataRow dtr in dt2.Rows)
            {

                DataTable dt3 = select("Select NumberofleaveUsed,Lastdatemodify,NumberofleaveEarned,Id from LeaveEarnedTbl where EmployeeId='" + Session["EmployeeID"] + "' and LeaveEarnedTbl.LeaveType='" + dtr["Id"] + "' ");
                if (dt3.Rows.Count > 0)
                {


                    int totleave = 0;
                    if (Convert.ToString(dt3.Rows[0]["NumberofleaveEarned"]) != "")
                    {
                        totleave += (Convert.ToInt32(dt3.Rows[0]["NumberofleaveEarned"]));
                    }

                    if (Convert.ToString(ViewState["payid"]) != "")
                    {
                        string fsd = Convert.ToDateTime(dt3.Rows[0]["Lastdatemodify"]).ToShortDateString();
                        string fed = DateTime.Now.ToShortDateString();

                        if (Convert.ToString(dtr["pertype"]) == "1")
                        {
                            int countweek = 1;
                            fsd = Convert.ToDateTime(fsd).AddDays(7).ToShortDateString();
                            for (int i = 0; i < countweek; i++)
                            {
                                if (Convert.ToDateTime(fsd) <= Convert.ToDateTime(fed))
                                {
                                    // fsd =Convert.ToDateTime(fsd).AddDays(7).ToShortDateString();
                                    countweek += 1;
                                }
                                else
                                {

                                    break;
                                }

                            }
                            countweek = countweek - 1;
                            if (countweek > 0)
                            {
                                totleave = totleave + (countweek) * (Convert.ToInt32(dtr["Leaveno"]));
                            }




                        }
                        else if (Convert.ToString(dtr["pertype"]) == "2")
                        {
                            int countweek = 1;
                            fsd = Convert.ToDateTime(fsd).AddMonths(1).ToShortDateString();
                            for (int i = 0; i < countweek; i++)
                            {
                                if (Convert.ToDateTime(fsd) <= Convert.ToDateTime(fed))
                                {
                                    // fsd = Convert.ToDateTime(fsd).AddMonths(1).ToShortDateString();
                                    countweek += 1;
                                }
                                else
                                {

                                    break;
                                }

                            }
                            countweek = countweek - 1;
                            if (countweek > 0)
                            {
                                totleave = totleave + (countweek) * (Convert.ToInt32(dtr["Leaveno"]));
                            }

                        }
                        else if (Convert.ToString(dtr["pertype"]) == "3")
                        {
                            int countweek = 1;
                            fsd = Convert.ToDateTime(fsd).AddYears(1).ToShortDateString();
                            for (int i = 0; i < countweek; i++)
                            {
                                if (Convert.ToDateTime(fsd) <= Convert.ToDateTime(fed))
                                {
                                    //  fsd = Convert.ToDateTime(fsd).AddYears(1).ToShortDateString();
                                    countweek += 1;
                                }
                                else
                                {

                                    break;
                                }

                            }
                            countweek = countweek - 1;
                            if (countweek > 0)
                            {
                                totleave = totleave + (countweek) * (Convert.ToInt32(dtr["Leaveno"]));
                            }
                        }

                        stc = "Update LeaveEarnedTbl set PayperiodId='" + Convert.ToString(ViewState["payid"]) + "',NumberofleaveEarned='" + totleave + "',Lastdatemodify='" + DateTime.Now.ToShortDateString() + "' where Id='" + Convert.ToString(dt3.Rows[0]["Id"]) + "'";

                        SqlCommand cmd = new SqlCommand(stc, con);
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
                    if (Convert.ToString(ViewState["payid"]) != "")
                    {
                        string fsd = Convert.ToDateTime(dt.Rows[0]["DateOfJoin"]).ToShortDateString();
                        string fed = DateTime.Now.ToShortDateString();
                        int noernedleave = 0;
                        int morethenday = 0;
                        int totalday = Convert.ToInt32(System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(Convert.ToDateTime(fsd), (Convert.ToDateTime(fed))));
                        if (Convert.ToString(dtr["pertype"]) == "1")
                        {
                            //morethenday = Convert.ToInt32(dtr["Moreday"]) + 7;
                            morethenday = Convert.ToInt32(dtr["Moreday"]);
                            if (morethenday < totalday)
                            {
                                noernedleave = weekcount(fsd, fed, morethenday);
                            }


                        }
                        else if (Convert.ToString(dtr["pertype"]) == "2")
                        {
                            //morethenday = Convert.ToInt32(dtr["Moreday"]) + 30;
                            morethenday = Convert.ToInt32(dtr["Moreday"]);

                            if ((morethenday) < (totalday))
                            {
                                noernedleave = monthcount(fsd, fed, morethenday);

                            }
                        }
                        else if (Convert.ToString(dtr["pertype"]) == "3")
                        {
                          //  morethenday = Convert.ToInt32(dtr["Moreday"]) + 365;
                            morethenday = Convert.ToInt32(dtr["Moreday"]);

                            if ((morethenday) < (totalday))
                            {
                                noernedleave = yearcount(fsd, fed, morethenday);

                            }
                        }
                        if (noernedleave > 0)
                        {
                            stc = "insert into LeaveEarnedTbl(LeaveType,BusinessId,EmployeeId,PayperiodId,NumberofleaveEarned,NumberofleaveUsed,Lastdatemodify)values ('" + dtr["Id"] + "','" + dt.Rows[0]["Whid"] + "','" + Session["EmployeeId"] + "','" + Convert.ToString(ViewState["payid"]) + "','" + (noernedleave * (Convert.ToInt32(dtr["Leaveno"]))) + "','0','" + DateTime.Now.ToShortDateString() + "')";

                            SqlCommand cmd = new SqlCommand(stc, con);
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

        }
    }
    protected int weekcount(string fsd, string fed, int moreday)
    {

        int countweek = 0;

        DataTable dtss = new DataTable();
        dtss = (DataTable)select("Select BatchWorkingDay.Batchmasterid,LastDayOftheWeek from EmployeeBatchMaster inner join BatchWorkingDay on BatchWorkingDay.BatchMasterId=EmployeeBatchMaster.Batchmasterid where Employeeid='" + Session["EmployeeId"] + "'");
        if (dtss.Rows.Count > 0)
        {
            DateTime dtstart = Convert.ToDateTime(fsd).AddDays(moreday);
            DateTime dtend = Convert.ToDateTime(fed);
            DataTable dts1 = new DataTable();
            for (int i = 0; i < 7; i++)
            {


                dts1 = (DataTable)select("select DateId,Date,day from  DateMasterTbl where [Date]='" + dtstart.ToShortDateString() + "'");
                if (dts1.Rows.Count > 0)
                {


                    if (Convert.ToString(dts1.Rows[0]["day"]) == Convert.ToString(dtss.Rows[0]["LastDayOftheWeek"]))
                    {

                        dtstart = dtstart.AddDays(7);
                        countweek += 1;
                        break;
                    }
                    else
                    {
                        dtstart = dtstart.AddDays(1);


                    }


                }
            }

            for (int i = 0; i < countweek; i++)
            {
                if (dtstart <= dtend)
                {

                    dtstart = dtstart.AddDays(7);
                    countweek += 1;
                }
                else
                {
                    break;
                }

            }


        }
        return countweek - 1;
    }
    protected int monthcount(string fsd, string fed, int moreday)
    {

        int countweek = 1;

        DataTable dtss = new DataTable();

        DateTime dtstart = Convert.ToDateTime(fsd).AddDays(moreday);
        DateTime dtend = Convert.ToDateTime(fed);
        DataTable dts1 = new DataTable();

        for (int i = 0; i < countweek; i++)
        {
            if (dtstart <= dtend)
            {

                dtstart = dtstart.AddMonths(1);
                countweek += 1;
            }
            else
            {
                break;
            }

        }



        return countweek - 1;
    }
    protected int yearcount(string fsd, string fed, int moreday)
    {

        int countweek = 1;

        DateTime dtstart = Convert.ToDateTime(fsd).AddDays(moreday);
        DateTime dtend = Convert.ToDateTime(fed);


        for (int i = 0; i < countweek; i++)
        {
            if (dtstart <= dtend)
            {

                dtstart = dtstart.AddYears(1);
                countweek += 1;
            }
            else
            {
                break;
            }

        }



        return countweek - 1;
    }
    protected DataTable select(string str)
    {

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter ad6 = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        ad6.Fill(ds);
        return ds;
    }
    protected void filldata()
    {
        string str1 = "";

        if (ddlfillleave.SelectedIndex > 0)
        {
            str1 = "and EmployeeHoliday.leavetypeid = '" + ddlfillleave.SelectedValue + "'";
        }
        if (txtfromdt.Text != "" && txttodt.Text != "")
        {

            str1 += " and   EmployeeHoliday.fromdate>='" + txtfromdt.Text + "' and EmployeeHoliday.Todate<='" + txttodt.Text + "'";
        }
       
       
        lblfilterleavetype.Text = ddlfillleave.SelectedItem.Text;
        lbldate.Text = "From Date :" + txtfromdt.Text + " To Date :" + txttodt.Text;

      
        lblCompany.Text = Session["Cname"].ToString();
        string str = "Select EmployeeHoliday.id,EmployeeHoliday.whid,EmployeeHoliday.employeeid,EmployeeHoliday.fromdate,EmployeeHoliday.Todate,EmployeeHoliday.leavetypeid,EmployeeHoliday.leaveRequestNote,EmployeeHoliday.ApprovalNote,EmployeeHoliday.compid,'Status'=CASE EmployeeHoliday.status WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Rejected'  ELSE  EmployeeHoliday.status END, " +
                    " EmployeeLeaveType.EmployeeLeaveTypeName,EmployeeLeaveType.ID,EmployeeMaster.EmployeeName, WareHouseMaster.Name from EmployeeHoliday inner join EmployeeLeaveType on EmployeeLeaveType.ID = EmployeeHoliday.leavetypeid inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID =  EmployeeHoliday.employeeid inner join WareHouseMaster on WareHouseMaster.WareHouseId = EmployeeHoliday.whid where EmployeeHoliday.compid='" + ViewState["Compid"] + "' and EmployeeHoliday.employeeid='" + ViewState["empid"] + "' "+str1+"  order by fromdate desc ";
      
        lblbusiness.Text = ddlstrname.SelectedItem.Text;
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter ad6 = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        ad6.Fill(ds);
        DataView myDataView = new DataView();
        myDataView = ds.Tables[0].DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView2.DataSource = myDataView;
        GridView2.DataBind();

    }
    protected void imgbtnGo_Click(object sender, ImageClickEventArgs e)
    {

        //string str7 = "select  from Login_Master where UserID='"+TextBox2.Text+"' and password='"+TextBox3.Text+"'";

        //string str7 = "SELECT [Login_master].UserID,[Login_master].[username],[Login_master].[password] from [Login_master] where username='"+txtuserId.Text+"'";

        //SqlCommand cmd7 = new SqlCommand(str7, con);
        //SqlDataAdapter ad7 = new SqlDataAdapter(cmd7);
        //DataTable dt = new DataTable();
        ////DataTable dt7=new DataTable();

        //ad7.Fill(dt);

        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    if (dt.Rows[0]["username"].ToString() == txtuserId.Text && dt.Rows[0]["password"].ToString() == txtPw.Text)
        //    {
        //        Label1.Text = "login successfully";

        //        Panel2.Visible = true;
        //        Panel3.Visible = true;
        //        filldata();

        //        break;

        //    }
        //    else
        //    {
        //        Label1.Text = "wrong user id and pswd";
        //        Panel2.Visible = false;
        //        Panel3.Visible = false;
        //        lblmsg.Visible = false;
        //    }
        //}


    }

    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Panel3.Visible = true;
        GridView2.EditIndex = e.NewEditIndex;

        filldata();
        DropDownList ddlstore = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlstore");

        Label lblwhid = (Label)GridView2.Rows[GridView2.EditIndex].FindControl("lblwhid");
        string str = "select Name,WareHouseId from WareHouseMaster where comid='" + Session["comid"] + "' and Status='1' order by Name";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        ad.Fill(ds);
        ddlstore.DataSource = ds;
        ddlstore.DataTextField = "Name";
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataBind();
        ddlstore.Items.Insert(0, "---Select---");

        ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(lblwhid.Text));


        DropDownList ddlEmp = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlEmp");

        Label lblEmpId = (Label)GridView2.Rows[GridView2.EditIndex].FindControl("lblEmpId");

        string str1 = "select EmployeeName,EmployeeMasterID from EmployeeMaster where Whid='" + ddlstore.SelectedValue + "' order by EmployeeName ";

        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter ad1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();

        ad1.Fill(ds1);
        ddlEmp.DataSource = ds1;
        ddlEmp.DataTextField = "EmployeeName";
        ddlEmp.DataValueField = "EmployeeMasterId";
        ddlEmp.DataBind();
        ddlEmp.Items.Insert(0, "---Select---");


        ddlEmp.SelectedIndex = ddlEmp.Items.IndexOf(ddlEmp.Items.FindByValue(lblEmpId.Text));

        TextBox txtStartDate1 = (TextBox)GridView2.Rows[GridView2.EditIndex].FindControl("txtStartDate1");

        TextBox txtEndDate1 = (TextBox)GridView2.Rows[GridView2.EditIndex].FindControl("txtEndDate1");
        CheckBox chkApp = (CheckBox)GridView2.Rows[GridView2.EditIndex].FindControl("chkApprove1");



        DropDownList ddlLeaveTypeId = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlLeaveTypeId");

        Label lblleaveTypeid = (Label)GridView2.Rows[GridView2.EditIndex].FindControl("lblleaveTypeid");
        string str2 = "select EmployeeLeaveTypeName,ID from EmployeeLeaveType";

        SqlCommand cmd2 = new SqlCommand(str2, con);
        SqlDataAdapter ad2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();

        ad2.Fill(ds2);
        ddlLeaveTypeId.DataSource = ds2;
        ddlLeaveTypeId.DataTextField = "EmployeeLeaveTypeName";
        ddlLeaveTypeId.DataValueField = "ID";
        ddlLeaveTypeId.DataBind();
        ddlLeaveTypeId.Items.Insert(0, "---Select---");

        ddlLeaveTypeId.SelectedIndex = ddlLeaveType.Items.IndexOf(ddlLeaveTypeId.Items.FindByValue(lblleaveTypeid.Text));

    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;

        filldata();
        //Panel2.Visible = true;
        pnladd.Visible = true;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //string str = " Select employeeid From [EmployeeHoliday] inner join EmployeeMaster on EmployeeHoliday.employeeid=EmployeeMaster.EmployeeMasterID where EmployeeMaster.EmployeeMasterID = '" + ddlEmployee.SelectedValue + "' and EmployeeHoliday.fromdate='" + txtStartDate.Text + "' and EmployeeHoliday.whid='"+ddlstorename.SelectedValue+"' ";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);

        //if (dt.Rows.Count > 0)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = " Record Already Exist... ";
        //    txtStartDate.Text = "";
        //    txtEndDate.Text = "";
        //    txtnote.Text = "";
        //    ddlLeaveType.SelectedIndex = 0;
        //}
        //else
        //{
        //    string str1 = " Insert into [EmployeeHoliday](whid,employeeid,fromdate,Todate,leavetypeid,leaveRequestNote,leaveRequestNoteDate,leaveApproveEmployeeid,leaveApprove,leaveNote) values ('" + ddlstorename.SelectedValue + "','" + ddlEmployee.SelectedValue + "','" + txtStartDate.Text + "','" + txtEndDate.Text + "','" + ddlLeaveType.SelectedValue + "','" + txtnote.Text + "','" + System.DateTime.Now.ToShortDateString() + "','"+ddlEmployer.SelectedValue+"','" + CheckBox1.Checked + "','" + txtApproNote.Text + "') ";
        //    SqlCommand cmd1 = new SqlCommand(str1, con);
        //    con.Open();
        //    cmd1.ExecuteNonQuery();
        //    con.Close();

        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record Inserted Successfully";
        //    filldata();
        //    ClearControl();

        //}
    }

    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //DropDownList ddlstore = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlstore");
        //DropDownList ddlEmp = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlEmp");
        //DropDownList ddlLeaveTypeId = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlLeaveTypeId");
        //TextBox txtStartDate1 = (TextBox)GridView2.Rows[GridView2.EditIndex].FindControl("txtStartDate1");
        //TextBox txtEndDate1 = (TextBox)GridView2.Rows[GridView2.EditIndex].FindControl("txtEndDate1");
        //CheckBox chkApprove1 = (CheckBox)GridView2.Rows[GridView2.EditIndex].FindControl("chkApprove1");

        //string strrrr = " Select employeeid From [EmployeeHoliday] inner join EmployeeMaster on EmployeeHoliday.employeeid=EmployeeMaster.EmployeeMasterID where EmployeeMaster.EmployeeMasterID = '" + ddlEmployee.SelectedValue + "' and fromdate='" + txtStartDate1.Text + "' and  id <>'" + ViewState["Id"] + "' ";




        //SqlCommand cmd1 = new SqlCommand(strrrr, con);

        //SqlDataAdapter adp = new SqlDataAdapter(cmd1);
        //DataTable ds = new DataTable();
        //adp.Fill(ds);
        //if (ds.Rows.Count == 0)
        //{
        //    string str = "Update EmployeeHoliday set whid='" + ddlstore.SelectedValue.ToString() + "',employeeid ='" + ddlEmp.SelectedValue + "',fromdate='" + txtStartDate1.Text + "',Todate='" + txtEndDate1.Text + "', leaveApprove='" + chkApprove1.Checked+ "' where Id='" + ViewState["Id"] + "'";
        //    SqlCommand cmd = new SqlCommand(str, con);
        //    if (con.State.ToString() != "Open")
        //    {
        //        con.Open();
        //    }


        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record Update Successfully";
        //    //GridView1.DataSource = (DataSet)fillgrid();
        //    //GridView1.DataBind();

        //    GridView2.EditIndex = -1;

        //    filldata();
        //}
        //else
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record already used";
        //}
    }
    protected void ddlstorename_SelectedIndexChanged(object sender, EventArgs e)
    {
      

    }
    public void ClearControl()
    {
        ddlLeaveType.ClearSelection();
       // txtStartDate.Text = "";
       // txtEndDate.Text = "";
        txtnote.Text = "";
        
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ClearControl();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            GridView2.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = GridView2.SelectedDataKey.Value;
        }
        if (e.CommandName == "Delete")
        {
            GridView2.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["DId"] = GridView2.SelectedDataKey.Value;
        }
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlCommand cmd = new SqlCommand("delete  from EmployeeHoliday where [Id]=" + ViewState["DId"] + "", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record deleted successfully";
        GridView2.EditIndex = -1;
        filldata();
    }

    protected void ImageButton2_Click1(object sender, ImageClickEventArgs e)
    {
        lblmsg.Visible = false;
        //Label1.Visible = false;
    }
    protected void FillLeaveType()
    {
        string str2 = "select distinct EmployeeLeaveTypeName,ID from EmployeeLeaveType where Whid='" + ViewState["Whid"] + "'";

        SqlCommand cmd2 = new SqlCommand(str2, con);
        SqlDataAdapter ad2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();

        ad2.Fill(ds2);
        ddlLeaveType.DataSource = ds2;

        ddlLeaveType.DataTextField = "EmployeeLeaveTypeName";
        ddlLeaveType.DataValueField = "ID";
        ddlLeaveType.DataBind();
        ddlLeaveType.Items.Insert(0, "-Select-");
        ddlLeaveType.Items[0].Value = "0";

        ddlfillleave.DataSource = ds2;

        ddlfillleave.DataTextField = "EmployeeLeaveTypeName";
        ddlfillleave.DataValueField = "ID";
        ddlfillleave.DataBind();
        ddlfillleave.Items.Insert(0, "All");
        ddlfillleave.Items[0].Value = "0";

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string todaydate = System.DateTime.Now.ToShortDateString();


        if (Convert.ToDateTime(txtStartDate.Text) < Convert.ToDateTime(todaydate))
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Start Date must be less than End Date";


        }
        else
        {

            string str12 = "select * from EmployeeHoliday where employeeid = '" + ViewState["empid"] + "'";
            SqlCommand cmd12 = new SqlCommand(str12, con);
            SqlDataAdapter adptr12 = new SqlDataAdapter(cmd12);
            DataSet ds12 = new DataSet();
            adptr12.Fill(ds12);
            Int32 temp = 0;
            if (ds12.Tables[0].Rows.Count > 0)
            {

                for (Int32 i = 0; i < ds12.Tables[0].Rows.Count; i++)
                {
                    string startda = DateTime.Now.ToShortDateString();
                    DateTime startdate = Convert.ToDateTime(startda);
                    //DateTime startdate = Convert.ToDateTime(ds12.Tables[0].Rows[i]["fromdate"].ToString());
                    DateTime enddate = Convert.ToDateTime(ds12.Tables[0].Rows[i]["Todate"].ToString());
                    if (startdate > Convert.ToDateTime(txtStartDate.Text))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Leave Start Date must be less than Leave End Date";
                        temp++;
                    }
                    else if (Convert.ToDateTime(txtEndDate.Text) < Convert.ToDateTime(txtStartDate.Text))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Leave End Date should be greater than Leave Start Date";
                        temp++;
                    }
                    else if (enddate >= Convert.ToDateTime(txtStartDate.Text))
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record already exists";
                        
                       
                        temp++;
                        break;

                    }
                    else
                    {
                        temp = 0;
                    }
                }

            }

            if (temp == 0)
            {

                int totaldayleave = 0;
                int totalday = 0;
                int pl = 0;
                int countweek = 1;


                DataTable dtc = select("Select * from EmployeeHoliday where leavetypeid='" + ddlLeaveType.SelectedValue + "' and employeeid='" + ViewState["empid"] + "' and status='0' and (fromdate>='" + DateTime.Now.ToShortDateString() + "' or Todate>='" + DateTime.Now.ToShortDateString() + "')");
                foreach (DataRow rt in dtc.Rows)
                {
                    totalday += Convert.ToInt32(rt["Noofleave"]);
                }

                String dtp = Convert.ToDateTime(txtStartDate.Text).ToShortDateString();
                for (int i = 0; i < countweek; i++)
                {
                    DataTable ds1 = select("SELECT distinct Convert(nvarchar, DateMasterTbl.Date,101) as Datet,DateMasterID as DateId FROM EmployeeMaster  " +
                    " INNER JOIN  dbo.EmployeeBatchMaster ON dbo.EmployeeMaster.EmployeeMasterID = dbo.EmployeeBatchMaster.Employeeid  inner join BatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID inner join BatchWorkingDays on BatchWorkingDays.BatchID=BatchMaster.Id inner join DateMasterTbl" +
                    " on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID  where  EmployeeBatchMaster.Employeeid='" + ViewState["empid"] + "' and cast( DateMasterTbl.Date as DateTime)='" + Convert.ToDateTime(dtp).ToShortDateString() + "'");
                    if (ds1.Rows.Count > 0)
                    {


                        if (Convert.ToDateTime(dtp) <= Convert.ToDateTime(txtEndDate.Text))
                        {

                            dtp = Convert.ToDateTime(dtp).AddDays(1).ToShortDateString();
                            countweek += 1;
                            totalday += 1;
                            totaldayleave += 1;
                        }
                        else
                        {
                            break;
                        }

                    }
                }

                if (lblpaidleno.Text != "")
                {

                    //  totalday = Convert.ToInt32(System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(Convert.ToDateTime(txtStartDate.Text), (Convert.ToDateTime(txtEndDate.Text))))+1;

                    if (totalday <= Convert.ToInt32(lblpaidleno.Text))
                    {
                        pl = 2;
                    }
                    else
                    {
                        pl = 0;
                    }

                }
                else
                {
                    //totalday = Convert.ToInt32(System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(Convert.ToDateTime(txtStartDate.Text), (Convert.ToDateTime(txtEndDate.Text))))+1;

                    pl = 2;
                }
                if (pl == 2)
                {
                    string str1 = "insert into EmployeeHoliday(whid,employeeid,fromdate,Todate,leavetypeid,leaveRequestNote,leaveRequestNoteDate,status,compid,leaveEntryid,Noofleave,Maxleave) values" +
                                            "('" + ViewState["Whid"] + "','" + ViewState["empid"] + "','" + txtStartDate.Text + "','" + txtEndDate.Text + "','" + ddlLeaveType.SelectedValue + "','" + txtnote.Text + "','" + System.DateTime.Now.ToShortDateString() + "','0','" + ViewState["Compid"] + "','1','" + totaldayleave + "','" + lblpaidleno.Text + "')";
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully";
                    filldata();
                    pnladd.Visible = false;
                    add.Visible = true;
                    lbladd.Text = "";
                    ClearControl();
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "You are not allowed to take a paid leave day between the selected two day period.";
                }
            }


        }

    }
    protected void getSupervisor()
    {

        string str1 = "select EmployeeMasterID,SuprviserId,EmployeeName,Whid from EmployeeMaster where EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "' and Active=1";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adptr = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adptr.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            //lblsupervisor.Text = ds1.Rows[0]["EmployeeName"].ToString();
            ViewState["empid"] = ds1.Rows[0]["EmployeeMasterID"].ToString();
            ViewState["Whid"] = ds1.Rows[0]["Whid"].ToString();
            // DataTable dt3 = select("select EmployeeName from EmployeeMaster where SuprviserId='" + ds1.Rows[0]["SuprviserId"] + "' and Active=1");
            DataTable dt3 = select("select EmployeeName from EmployeeMaster where EmployeeMasterID='" + ds1.Rows[0]["SuprviserId"] + "' and Active=1");
            if (dt3.Rows.Count > 0)
            {
                lblsupervisor.Text = dt3.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                lblsupervisor.Text = "No Record Found";
            }

        }

    }

    //protected void txtfromdt_TextChanged(object sender, EventArgs e)
    //{

    //}
    //protected void txttodt_TextChanged(object sender, EventArgs e)
    //{
    //    filldata();
    //}
    //protected void ddlfillleave_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    filldata();
    //}
    protected void add_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        lblmsg.Text = "";
        add.Visible = false;
        lbladd.Text = "New Leave Request";
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
            Button1.Text = "Printable Version";
            Button7.Visible = false;

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        pnladd.Visible = false;
        add.Visible = true;
        lbladd.Text = "";
        lblmsg.Text = "";
    }
    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt31 = select("Select * from EmployeeLeaveType where Id='" + ddlLeaveType.SelectedValue + "' and EmployeeLeaveType.Leaveencashable='1' ");
        if (dt31.Rows.Count > 0)
        {
            DataTable dt3 = select("Select NumberofleaveUsed,Lastdatemodify,NumberofleaveEarned,LeaveEarnedTbl.Id from LeaveEarnedTbl inner join EmployeeLeaveType on EmployeeLeaveType.Id=LeaveEarnedTbl.LeaveType where LeaveEarnedTbl.LeaveType='" + ddlLeaveType.SelectedValue + "' and EmployeeLeaveType.Leaveencashable='1' and EmployeeId='" + ViewState["empid"] + "'  ");
            lblpaidle.Text = "Allowed paid leave ";
            if (dt3.Rows.Count > 0)
            {

                lblpaidleno.Text = (Convert.ToInt32(dt3.Rows[0]["NumberofleaveEarned"]) - Convert.ToInt32(dt3.Rows[0]["NumberofleaveUsed"])).ToString();
            }
            else
            {
                lblpaidleno.Text = "0";
            }
        }
        else
        {
            lblpaidle.Text = "No paid leave ";
            lblpaidleno.Text = "";
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        filldata();
    }



    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        filldata();
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
    protected void ddlstrname_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = select("select EmployeeMasterID,SuprviserId,EmployeeName,Whid from EmployeeMaster where EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "' and Active=1");
        if (dt.Rows.Count > 0)
        {
            ddlemp.DataSource = dt;
            ddlemp.DataTextField = "EmployeeName";
            ddlemp.DataValueField = "EmployeeMasterID";
            ddlemp.DataBind();
        }
    }


}