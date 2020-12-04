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
public partial class ShoppingCart_Admin_MyLeaveEncash : System.Web.UI.Page
{
   
    SqlConnection con;
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


        Page.Title = pg.getPageTitle(page);

     
        statuslable.Visible = false;
        if (!IsPostBack)
        {
          
          
            if (!IsPostBack)
            {
                ViewState["sortOrder"] = "";
             
                fillstore();
              
                ddlwarehouse_SelectedIndexChanged(sender, e);
            }
           
        }
    }



    public void fillgrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        lblCompany.Text = Session["Cname"].ToString();
        lblBusiness.Text = ddlwarehouse.SelectedItem.Text;
        
        lblemp.Text = "Employee       : " +ddlemp.SelectedItem.Text;

        DataTable dt3 = select("Select NumberofleaveUsed,Lastdatemodify,NumberofleaveEarned,EmployeeLeaveType.Id,EmployeeLeaveType.EmployeeLeaveTypeName from LeaveEarnedTbl inner join EmployeeLeaveType on EmployeeLeaveType.Id=LeaveEarnedTbl.LeaveType where LeaveEarnedTbl.BusinessId='" + ddlwarehouse.SelectedValue + "' and EmployeeId='" + ddlemp.SelectedValue + "'  and EmployeeLeaveType.Leaveencashable='1' ");
        //ddlwarehouse.SelectedValue
        //ddlemp.SelectedValue
            DataTable dtf = new DataTable();
            dtf = CreateDatatable();

            foreach (DataRow dtp in dt3.Rows)
            {
                DataRow Drow = dtf.NewRow();
               
                Drow["Id"] = Convert.ToString(dtp["Id"]);
                Drow["EmployeeLeaveTypeName"] = Convert.ToString(dtp["EmployeeLeaveTypeName"]);
                Drow["NumberofleaveEarned"] = Convert.ToInt32(dtp["NumberofleaveEarned"]) - Convert.ToInt32(dtp["NumberofleaveUsed"]);

                //Drow["InTime"] = Convert.ToString(dtp["InTime"]);
                //Drow["InTimeforcalculation"] = Convert.ToString(dtp["InTimeforcalculation"]);

                //Drow["EmployeeId"] = Convert.ToString(dtp["EmployeeId"]);
               
                dtf.Rows.Add(Drow);
            }
          

           
            DataView myDataView = new DataView();
            myDataView = dtf.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataSource = myDataView;
            GridView1.DataBind();
            //if (GridView1.Rows.Count > 0)
            //{
            //    btnsubmit.Visible = true;
            //}
            //else
            //{
            //    btnsubmit.Visible = false;
            //}
       
    }
    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "Id";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "EmployeeLeaveTypeName";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "NumberofleaveEarned";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        
        //DataColumn Dcom3 = new DataColumn();
        //Dcom3.DataType = System.Type.GetType("System.String");
        //Dcom3.ColumnName = "InTime";
        //Dcom3.AllowDBNull = true;
        //Dcom3.Unique = false;
        //Dcom3.ReadOnly = false;

        //DataColumn Dcom4 = new DataColumn();
        //Dcom4.DataType = System.Type.GetType("System.String");
        //Dcom4.ColumnName = "InTimeforcalculation";
        //Dcom4.AllowDBNull = true;
        //Dcom4.Unique = false;
        //Dcom4.ReadOnly = false;


        //DataColumn Dcom5 = new DataColumn();
        //Dcom5.DataType = System.Type.GetType("System.String");
        //Dcom5.ColumnName = "OutTime";
        //Dcom5.AllowDBNull = true;
        //Dcom5.Unique = false;
        //Dcom5.ReadOnly = false;


       

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        //dt.Columns.Add(Dcom3);
        //dt.Columns.Add(Dcom4);
        //dt.Columns.Add(Dcom5);
       
        return dt;
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

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
            //btnsubmit.Visible = false;
            
            
        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;
            if (GridView1.Rows.Count > 0)
            {
               // btnsubmit.Visible = true;
            }
        }
    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        statuslable.Text = "";
        fillleaveearned();
        fillgrid();
    }
    protected void fillleaveearned()
    {
        ViewState["payid"] = "";
        DataTable dt = select("Select DateOfJoin,DesignationMasterId,Whid from  EmployeeMaster where EmployeeMaster.EmployeeMasterId='" + ddlemp.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            DataTable dt1 = select("Select  payperiodMaster.ID, payperiodMaster.PayperiodStartDate,payperiodMaster.PayperiodEndDate from payperiodtype inner join payperiodMaster  on payperiodtype.Id=payperiodMaster.PayperiodTypeID inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=payperiodtype.Id where EmpId='" +ddlemp.SelectedValue + "' and PayperiodStartDate<='" + DateTime.Now.ToShortDateString() + "' and PayperiodEndDate>='" + DateTime.Now.ToShortDateString() + "'");

            if (dt1.Rows.Count > 0)
            {
                ViewState["payid"] = dt1.Rows[0]["ID"];

            }
            string stc = "";
            DataTable dt2 = select("Select EmployeeLeaveType.* from  EmployeeLeaveType inner join DesignationMaster on DesignationMaster.DesignationmasterId=EmployeeLeaveType.DesignationId where  EmployeeLeaveType.DesignationId='" + dt.Rows[0]["DesignationMasterId"] + "' and EmployeeLeaveType.Leaveencashable='1'");
            foreach (DataRow dtr in dt2.Rows)
            {

                DataTable dt3 = select("Select NumberofleaveUsed,Lastdatemodify,NumberofleaveEarned,Id from LeaveEarnedTbl where EmployeeId='" + ddlemp.SelectedValue + "' and LeaveEarnedTbl.LeaveType='" + dtr["Id"] + "' ");
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
                            morethenday = Convert.ToInt32(dtr["Moreday"]) + 7;
                            if (morethenday < totalday)
                            {
                                noernedleave = weekcount(fsd, fed, morethenday);
                            }


                        }
                        else if (Convert.ToString(dtr["pertype"]) == "2")
                        {
                            morethenday = Convert.ToInt32(dtr["Moreday"]) + 30;

                            if ((morethenday) < (totalday))
                            {
                                noernedleave = monthcount(fsd, fed, morethenday);

                            }
                        }
                        else if (Convert.ToString(dtr["pertype"]) == "3")
                        {
                            morethenday = Convert.ToInt32(dtr["Moreday"]) + 365;

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
        dtss = (DataTable)select("Select BatchWorkingDay.Batchmasterid,LastDayOftheWeek from EmployeeBatchMaster inner join BatchWorkingDay on BatchWorkingDay.BatchMasterId=EmployeeBatchMaster.Batchmasterid where Employeeid='" + ddlemp.SelectedValue + "'");
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
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        statuslable.Text = "";
        Fillddlbatch();
       
    }
    protected void fillstore()
    {
        ddlwarehouse.Items.Clear();
      //  DataTable ds = ClsStore.SelectStorename();

        string str = "SELECT Name,WareHouseId from warehousemaster inner join employeemaster on warehousemaster.WareHouseId=employeemaster.whid where EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "'";
        SqlCommand cmdwh = new SqlCommand(str, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable ds = new DataTable();
        adpwh.Fill(ds);


        //if (ds.Rows.Count > 0)
        //{
            ddlwarehouse.DataSource = ds;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();

            ddlwarehouse.Enabled = false;
        //    DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        //    if (dteeed.Rows.Count > 0)
        //    {
        //        ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        //    }
        //}
    }
  
    protected void Fillddlbatch()
    {
        string str = "select EmployeeMasterId,EmployeeName from EmployeeMaster where EmployeeMasterId='" + Convert.ToInt32(Session["EmployeeId"]) + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlemp.DataSource = ds;
        ddlemp.DataTextField = "EmployeeName";
        ddlemp.DataValueField = "EmployeeMasterId";

        ddlemp.DataBind();

        ddlemp.Enabled = false;
    }

   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         if (e.CommandName == "view")
        {
 
            int dk = Convert.ToInt32(e.CommandArgument);

            DataTable dtdaopb = (DataTable)select("select Convert(nvarchar, StartDate,101) as StartDate,Convert(nvarchar, EndDate,101) as EndDate from [ReportPeriod] where Whid='" + ddlwarehouse.SelectedValue + "' and Active='1' order by Report_Period_Id Desc");
            if (dtdaopb.Rows.Count > 0)
            {
              lblfdate.Text=Convert.ToString( dtdaopb.Rows[0]["StartDate"]);
              lbltodate.Text = Convert.ToString(dtdaopb.Rows[0]["EndDate"]);

              int OPBALANCE = 0;
              int tOTALLEAVE = 0;
              int TOTALLEAVEUSED = 0;
              DataTable dtcrNe1 = new DataTable();

              DataTable dt123 = (DataTable)CreateTempDTforGrid();

              dtcrNe1 = select("select Distinct Sum(CAST(LeaveEarnedTblDeatail.BalanceLeave AS int)) as BalanceLeave ,Sum(CAST(LeaveEarnedTblDeatail.UsedLeave AS int)) as   UsedLeave  from  LeaveEarnedTblDeatail WHERE LeaveType='" + dk + "' and EmployeeId='" + ddlemp.SelectedValue + "' and (Date<= '" + Convert.ToDateTime(lblfdate.Text).AddDays(-1).ToShortDateString() + "')");
              if (Convert.ToString(dtcrNe1.Rows[0][0]) != "")
              {
                  
                      TOTALLEAVEUSED = Convert.ToInt32(dtcrNe1.Rows[0]["UsedLeave"]);
                      tOTALLEAVE = Convert.ToInt32(dtcrNe1.Rows[0]["BalanceLeave"]) + Convert.ToInt32(dtcrNe1.Rows[0]["UsedLeave"]);
                      OPBALANCE = Convert.ToInt32(dtcrNe1.Rows[0]["BalanceLeave"]);
                  
              }
            
            
              DataRow dradd1 = dt123.NewRow();
                  dradd1["Id"] = "0"; ;
                  dradd1["EmployeeLeaveTypeName"] ="OPNING LEAVE BALANCE";
                  dradd1["Date"] = lblfdate.Text;
                 
                  if (tOTALLEAVE == 0)
                  {
                      DataTable drt = select("select Distinct NumberofleaveEarned,NumberofleaveUsed from  LeaveEarnedTbl WHERE  LeaveType='" + dk + "' and employeeid='" + ddlemp.SelectedValue + "' and Lastdatemodify<='"+  Convert.ToDateTime(lblfdate.Text).AddDays(-1).ToShortDateString() +"'");
                      if (drt.Rows.Count > 0)
                      {
                          TOTALLEAVEUSED = Convert.ToInt32(drt.Rows[0]["NumberofleaveUsed"]);
                          tOTALLEAVE = Convert.ToInt32(drt.Rows[0]["NumberofleaveEarned"]);
                          OPBALANCE = Convert.ToInt32(drt.Rows[0]["NumberofleaveEarned"]) - Convert.ToInt32(drt.Rows[0]["NumberofleaveUsed"]);
                      }
                  }
              
                  dradd1["Ernedleave"] = tOTALLEAVE;
                
                  dradd1["Usedeave"] = TOTALLEAVEUSED;
               
                  dradd1["Balance"] = OPBALANCE;
                  dt123.Rows.Add(dradd1);
                  DataTable dts = select("select Distinct LeaveEarnedTblDeatail.Id,EmployeeLeaveTypeName,Convert(nvarchar, Date,101) as Date,BalanceLeave,UsedLeave,TotalLeave  from  LeaveEarnedTblDeatail INNER JOIN EmployeeLeaveType on EmployeeLeaveType.Id=LeaveEarnedTblDeatail.LeaveType WHERE  LeaveEarnedTblDeatail.LeaveType='" + dk + "' and LeaveEarnedTblDeatail.employeeid='" + ddlemp.SelectedValue + "' and (Date>= '" + Convert.ToDateTime(lblfdate.Text).ToShortDateString() + "') and (Date<= '" + Convert.ToDateTime(lbltodate.Text).ToShortDateString() + "')");
                  if (dts.Rows.Count > 0)
                  {

                      foreach (DataRow dtrNew in dts.Rows)
                      {
                          DataRow dradd = dt123.NewRow();
                          dradd["Id"] = dtrNew["Id"].ToString();
                          dradd["EmployeeLeaveTypeName"] = dtrNew["EmployeeLeaveTypeName"].ToString();
                          dradd["Date"] = Convert.ToString(dtrNew["Date"]);

                          dradd["Ernedleave"] = Convert.ToString(dtrNew["TotalLeave"]);

                          dradd["Usedeave"] = Convert.ToString(dtrNew["UsedLeave"]);

                          dradd["Balance"] = Convert.ToString(dtrNew["BalanceLeave"]);
                          dt123.Rows.Add(dradd);
                      }

                  }
                  else
                  {
                      DataTable dtrNew = select("select  Distinct LeaveEarnedTbl.Id, EmployeeLeaveTypeName,Convert(nvarchar, Lastdatemodify,101) as Date, NumberofleaveEarned,NumberofleaveUsed from  LeaveEarnedTbl INNER JOIN EmployeeLeaveType on EmployeeLeaveType.Id=LeaveEarnedTbl.LeaveType WHERE  LeaveType='" + dk + "' and employeeid='" + ddlemp.SelectedValue + "'");
                      if (dtrNew.Rows.Count > 0)
                      {
                          DataRow dradd = dt123.NewRow();
                          dradd["Id"] = dtrNew.Rows[0]["Id"].ToString();
                          dradd["EmployeeLeaveTypeName"] = dtrNew.Rows[0]["EmployeeLeaveTypeName"].ToString();
                          dradd["Date"] = Convert.ToString(dtrNew.Rows[0]["Date"]);

                          dradd["Ernedleave"] = Convert.ToString(dtrNew.Rows[0]["NumberofleaveEarned"]);

                          dradd["Usedeave"] = "0";

                          dradd["Balance"] = Convert.ToString(dtrNew.Rows[0]["NumberofleaveEarned"]);
                          dt123.Rows.Add(dradd);
                      }
                  }
                  pnlmoreinfo.Visible = true;
                  grdleaveaccount.DataSource = dt123;
                  grdleaveaccount.DataBind();
            }
        


            //string te = "frmMissionMasterReport.aspx?objectiveid=" + dk;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }

         if (e.CommandName == "view1")
         {
             //ViewState["Masterid"] = dtmax.Rows[0]["StatusCategoryMasterId"].ToString();

             int id = Convert.ToInt32(e.CommandArgument);
             string te = "Myleaveapplication.aspx?Id=" + id;
             ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

         }
    }
    public DataTable CreateTempDTforGrid()
    {
        DataTable dtGRD = new DataTable();

        DataColumn dcTMI = new DataColumn();
        dcTMI.ColumnName = "Id";
        dcTMI.DataType = System.Type.GetType("System.String");
        dcTMI.AllowDBNull = true;
        dtGRD.Columns.Add(dcTMI);

        DataColumn dcDate = new DataColumn();
        dcDate.ColumnName = "Date";
        dcDate.DataType = System.Type.GetType("System.String");
        dcDate.AllowDBNull = true;
        dtGRD.Columns.Add(dcDate);

        DataColumn dcEntryType = new DataColumn();
        dcEntryType.ColumnName = "EmployeeLeaveTypeName";
        dcEntryType.DataType = System.Type.GetType("System.String");
        dcEntryType.AllowDBNull = true;
        dtGRD.Columns.Add(dcEntryType);

        DataColumn dcEntryNo = new DataColumn();
        dcEntryNo.ColumnName = "Ernedleave";
        dcEntryNo.DataType = System.Type.GetType("System.String");
        dcEntryNo.AllowDBNull = true;
        dtGRD.Columns.Add(dcEntryNo);


        DataColumn dcRelaDoc = new DataColumn();
        dcRelaDoc.ColumnName = "Usedeave";
        dcRelaDoc.DataType = System.Type.GetType("System.String");
        dcRelaDoc.AllowDBNull = true;
        dtGRD.Columns.Add(dcRelaDoc);

        DataColumn dcDetMemo = new DataColumn();
        dcDetMemo.ColumnName = "Balance";
        dcDetMemo.DataType = System.Type.GetType("System.String");
        dcDetMemo.AllowDBNull = true;
        dtGRD.Columns.Add(dcDetMemo);

        //DataColumn dcAccount = new DataColumn();
        //dcAccount.ColumnName = "Account";
        //dcAccount.DataType = System.Type.GetType("System.String");
        //dcAccount.AllowDBNull = true;
        //dtGRD.Columns.Add(dcAccount);

        //DataColumn dcCredit = new DataColumn();
        //dcCredit.ColumnName = "Credit";
        //dcCredit.DataType = System.Type.GetType("System.String");
        //dcCredit.AllowDBNull = true;
        //dtGRD.Columns.Add(dcCredit);

        ////DataColumn dcAccountDebit = new DataColumn();
        ////dcAccountDebit.ColumnName = "AccountDebit";
        ////dcAccountDebit.DataType = System.Type.GetType("System.String");
        ////dtGRD.Columns.Add(dcAccountDebit);

        //DataColumn dcDebit = new DataColumn();
        //dcDebit.ColumnName = "Debit";
        //dcDebit.DataType = System.Type.GetType("System.String");
        //dcDebit.AllowDBNull = true;
        //dtGRD.Columns.Add(dcDebit);

        //DataColumn dcBalance = new DataColumn();
        //dcBalance.ColumnName = "Balance";
        //dcBalance.DataType = System.Type.GetType("System.String");
        //dcBalance.AllowDBNull = true;
        //dtGRD.Columns.Add(dcBalance);

     
        return dtGRD;



    }

    //protected void btnsubmit_Click(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow grd in GridView1.Rows)
    //    {

    //        int Id = Convert.ToInt32(GridView1.DataKeys[grd.RowIndex].Value);
    //        CheckBox chk = (CheckBox)grd.FindControl("chk");
    //        Label lblnoofleave = (Label)grd.FindControl("lblnoofleave");
    //        if (Convert.ToInt32(lblnoofleave.Text) > 0)
    //        {
    //            if (chk.Checked == true)
    //            {
    //                int usedleave = 0;
    //                int balanceleave = 0;

    //                DataTable dorou = select("Select * from LeaveEarnedTbl where LeaveType='" + Id + "' and EmployeeId='" + ddlemp.SelectedValue + "'");
    //                if (dorou.Rows.Count > 0)
    //                {
    //                    usedleave = Convert.ToInt32(dorou.Rows[0]["NumberofleaveUsed"]) + Convert.ToInt32(lblnoofleave.Text);

    //                    string stratt = "Update  LeaveEarnedTbl set NumberofleaveUsed='" + usedleave + "' where LeaveType='" + Id + "' and EmployeeId='" + ddlemp.SelectedValue + "'";
    //                    SqlCommand cmdatt = new SqlCommand(stratt, con);
    //                    if (con.State.ToString() != "Open")
    //                    {
    //                        con.Open();
    //                    }
    //                    cmdatt.ExecuteNonQuery();
    //                    con.Close();
    //                    balanceleave = usedleave - usedleave;
    //                    int totalleave = Convert.ToInt32(lblnoofleave.Text);
    //                    string stradet = "insert into LeaveEarnedTblDeatail(LeaveType,EmployeeId,BalanceLeave,UsedLeave,Date,Encash,TotalLeave,EmpholidayId)Values('" + Id + "','" + ddlemp.SelectedValue + "','" + balanceleave + "','" + Convert.ToInt32(lblnoofleave.Text) + "','" + DateTime.Now.ToShortDateString() + "','1','" + totalleave + "','0')";
    //                    SqlCommand cmddet = new SqlCommand(stradet, con);
    //                    if (con.State.ToString() != "Open")
    //                    {
    //                        con.Open();
    //                    }
    //                    cmddet.ExecuteNonQuery();
    //                    con.Close();
    //                }
    //                statuslable.Text = "Encash Leave Successfully";
    //                statuslable.Visible = true;

    //            }


    //        }

    //    }
    //    pnlmoreinfo.Visible = false;
    //    fillgrid();
    //}

    
}
