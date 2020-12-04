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

public partial class ShoppingCart_Admin_Master_Default : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();

        if (Session["EmployeeId"] == null || Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        if (!IsPostBack)
        {

            if (Request.QueryString["Id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);

                SqlDataAdapter dag = new SqlDataAdapter("select * from OverheadAbsorbtionMasterTbl where ID=" + id, con);
                DataTable dtg = new DataTable();
                dag.Fill(dtg);

                if (dtg.Rows.Count > 0)
                {
                    btnadd.Visible = false;
                    Button5.Visible = false;
                    pannnn.Visible = true;

                    fillstore();
                    ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtg.Rows[0]["BusinessID"].ToString()));
                    ddlStore.Enabled = false;

                    fillaccountyear();
                    ddlaccount.SelectedIndex = ddlaccount.Items.IndexOf(ddlaccount.Items.FindByValue(dtg.Rows[0]["AccountID"].ToString()));
                    ddlaccount.Enabled = false;

                    txtestartdate.Text = Convert.ToDateTime(dtg.Rows[0]["DateCalculated"].ToString()).ToShortDateString();
                    txtestartdate.Enabled = false;
                    ImageButton2.Enabled = false;

                    panelhd.Visible = false;

                    grid2();

                    SqlDataAdapter dafi = new SqlDataAdapter("select * from OverheadAbsorbtionDetailTbl where OverheadAbsorbtionMasterID='" + id + "'", con);
                    DataTable dtfi = new DataTable();
                    dafi.Fill(dtfi);

                    if (dtfi.Rows.Count > 0)
                    {
                        GridView1.DataSource = dtfi;
                        GridView1.DataBind();
                    }

                    decimal d331 = 0;

                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        Label Label159 = (Label)gr.FindControl("Label159");

                        string d311 = Label159.Text;
                        d331 += Convert.ToDecimal(d311);
                    }
                    lbltotal1.Text = d331.ToString("###,###.##");

                    lbl1rate.Text = lbltotal1.Text;

                    double div = Convert.ToDouble(lbl1rate.Text) / Convert.ToDouble(lbl2rate.Text);
                    //lbl3rate.Text = div.ToString("###,###.##");
                    lbl3rate.Text = String.Format("{0:n}", Convert.ToDecimal(div));


                }
            }
            else
            {

                fillstore();
                fillaccountyear();
                //grid2();

                beforepag();

                fillaccountdate();
            }
            //fillbatchhour();

        }
    }

    protected void fillstore()
    {
        ddlStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlStore.DataSource = ds;
        ddlStore.DataTextField = "Name";
        ddlStore.DataValueField = "WareHouseId";
        ddlStore.DataBind();

        // ddlStore.SelectedValue = Convert.ToString("2290");

        //ddlStore.Items.Insert(0, "Select");

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }

    protected void fillaccountyear()
    {
        string str = "select LEFT(Cast(StartDate as nvarchar),11) + ' : ' + LEFT(Cast(EndDate as nvarchar),11) as Date,ReportPeriod.Report_Period_Id from ReportPeriod where ReportPeriod.Compid='" + Session["Comid"].ToString() + "' and Whid='" + ddlStore.SelectedValue + "'";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ddlaccount.DataSource = dt;
            ddlaccount.DataTextField = "Date";
            ddlaccount.DataValueField = "Report_Period_Id";
            ddlaccount.DataBind();

            SqlDataAdapter da1 = new SqlDataAdapter("select LEFT(Cast(StartDate as nvarchar),11) + ' : ' + LEFT(Cast(EndDate as nvarchar),11) as Date,ReportPeriod.Report_Period_Id from ReportPeriod where ReportPeriod.Compid='" + Session["Comid"].ToString() + "' and Whid='" + ddlStore.SelectedValue + "' and Active='1'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            ddlaccount.SelectedIndex = ddlaccount.Items.IndexOf(ddlaccount.Items.FindByValue(dt1.Rows[0]["Report_Period_Id"].ToString()));
        }
    }

    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillaccountyear();

        grid2();
        beforepag();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        grid2();
        fillgrid();

        txtovername.Text = "";
        txtamount.Text = "";
    }

    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "Overheadname";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "Amount";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        //DataColumn Dcom2 = new DataColumn();
        //Dcom2.DataType = System.Type.GetType("System.String");
        //Dcom2.ColumnName = "ID";
        //Dcom2.AllowDBNull = true;
        //Dcom2.Unique = false;
        //Dcom2.ReadOnly = false;


        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        // dt.Columns.Add(Dcom2);

        return dt;
    }

    protected void fillgrid()
    {
        DataTable dt = new DataTable();
        if (txtamount.Text.Length < 0)
        {
            txtamount.Text = "0.00";
        }
        if (Convert.ToString(ViewState["data"]) == "")
        {
            dt = CreateDatatable();

            DataRow Drow = dt.NewRow();
            Drow["Overheadname"] = txtovername.Text;
            Drow["Amount"] = String.Format("{0:n}", Convert.ToDecimal(txtamount.Text));
            //Drow["ID"] = 1;

            dt.Rows.Add(Drow);
            ViewState["data"] = dt;
        }

        else
        {
            dt = (DataTable)ViewState["data"];
           
            int flag = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string Overheadname = dr["Overheadname"].ToString();
                string Amount = dr["Amount"].ToString();
                if (Overheadname == txtovername.Text && Amount == txtamount.Text)
                {
                    lblmsg.Text = "Record already exist";
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
            { 
                DataRow Drow = dt.NewRow();
                Drow["Overheadname"] = txtovername.Text;
                Drow["Amount"] = String.Format("{0:n}", Convert.ToDecimal(txtamount.Text));
                //Drow["ID"] = 1;

                dt.Rows.Add(Drow);
                ViewState["data"] = dt;
            }
        }

        decimal d33 = 0;

        GridView1.DataSource = dt;
        GridView1.DataBind();

        if (GridView1.Rows.Count > 0)
        {
            Button6.Visible = true;
        }

        foreach (GridViewRow GR in GridView1.Rows)
        {
            Label Label159 = (Label)GR.FindControl("Label159");

            string d3 = Label159.Text;
            d33 += Convert.ToDecimal(d3);

        }

        lbltotal1.Text = d33.ToString("###,###.##");

        lbl1rate.Text = lbltotal1.Text;
        if (lbl1rate.Text.Length < 0)
        {
            lbl1rate.Text = "0.00";
        }

        if (lbl2rate.Text == "")
        {
            lblmsg.Text = "Please set your Batch Working Days first.";
            lbl3rate.Text = "";
            //double div = Convert.ToDouble(lbl1rate.Text) / Convert.ToDouble(lbl2rate.Text);
            //lbl3rate.Text = String.Format("{0:n}", Convert.ToDecimal(div));            
        }
        else
        {            
            double div = Convert.ToDouble(lbl1rate.Text) / Convert.ToDouble(lbl2rate.Text);

            //lbl3rate.Text = div.ToString("###,###.##");
            lbl3rate.Text = String.Format("{0:n}", Convert.ToDecimal(div));
        }

        if (lbl3rate.Text != "")
        {
            Button3.Visible = true;
            Button6.Visible = true;
        }
        else
        {
            Button6.Visible = false;
        }
    }

    protected void grid2()
    {
        SqlDataAdapter daf100 = new SqlDataAdapter("select [StartDate],[EndDate] from [ReportPeriod] where [Report_Period_Id]='" + ddlaccount.SelectedValue + "'", con);
        DataTable dtf100 = new DataTable();
        daf100.Fill(dtf100);

        if (dtf100.Rows.Count > 0)
        {
            ViewState["stdate"] = Convert.ToDateTime(dtf100.Rows[0]["StartDate"].ToString()).ToShortDateString();
            ViewState["endate"] = Convert.ToDateTime(dtf100.Rows[0]["EndDate"].ToString()).ToShortDateString();
        }

        SqlDataAdapter da1 = new SqlDataAdapter("select EmployeeName,EmployeeMasterID,EmployeeBatchMaster.Batchmasterid from EmployeeMaster inner join party_master on party_master.partyid=employeemaster.partyid inner join employeebatchmaster on  EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid  where party_master.id='" + Session["Comid"].ToString() + "' and employeemaster.whid='" + ddlStore.SelectedValue + "' and employeemaster.Active='1'  and employeemaster.dateofjoin<='" + ViewState["endate"].ToString() + "' ", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        GridView2.DataSource = dt1;
        GridView2.DataBind();

        decimal d9 = 0;

        foreach (GridViewRow grd in GridView2.Rows)
        {
            Label Label10 = (Label)grd.FindControl("Label10");

            Label Label159 = (Label)grd.FindControl("Label159");

            Label Labelsdf158 = (Label)grd.FindControl("Labelsdf158");

            Label Laasdasbel159 = (Label)grd.FindControl("Laasdasbel159");

            string str1211 = "select AvgRate from EmployeeAvgSalaryMaster where EmployeeId='" + Label10.Text + "'";
            DataTable ds1211 = new DataTable();
            SqlDataAdapter da1211 = new SqlDataAdapter(str1211, con);
            da1211.Fill(ds1211);

            if (ds1211.Rows.Count > 0)
            {
                if (Convert.ToString(ds1211.Rows[0]["AvgRate"]) != "")
                {
                    Label159.Text = ds1211.Rows[0]["AvgRate"].ToString();
                }
                else
                {
                    Label159.Text = "0";
                }
            }
            else
            {
                Label159.Text = "0";
            }
            
            //string str1211 = "select Rate from OverheadAbsorbtionMasterTbl where BusinessID='" + ddlStore.SelectedValue + "' and DateCalculated='" + DropDownList1.SelectedItem.Text + "'";
            //DataTable ds1211 = new DataTable();
            //SqlDataAdapter da1211 = new SqlDataAdapter(str1211, con);
            //da1211.Fill(ds1211);

            //if (ds1211.Rows.Count > 0)
            //{
            //    Labelsdf158.Text = ds1211.Rows[0]["Rate"].ToString();
            //}

            if (lbl3rate.Text == "")
            {
                Labelsdf158.Text = "0";
            }
            else
            {
                Labelsdf158.Text = lbl3rate.Text;
            }


            double decan = Convert.ToDouble(Label159.Text) + Convert.ToDouble(Labelsdf158.Text);


            //Laasdasbel159.Text = decan.ToString("###,###.##");

            Laasdasbel159.Text = String.Format("{0:n}", Convert.ToDecimal(decan));



            //for (int rowindex = 0; rowindex < dt1.Rows.Count; rowindex++)
            //{
            string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + Label10.Text + "' and EmployeeMaster.Whid='" + ddlStore.SelectedValue + "'";
            DataTable ds12 = new DataTable();
            SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
            da12.Fill(ds12);
            string outdifftime1 = "00:00";

            if (ds12.Rows.Count > 0)
            {
                ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

                SqlDataAdapter daf = new SqlDataAdapter("select [StartDate],[EndDate] from [ReportPeriod] where [Report_Period_Id]='" + ddlaccount.SelectedValue + "'", con);
                DataTable dtf = new DataTable();
                daf.Fill(dtf);

                if (dtf.Rows.Count > 0)
                {
                    ViewState["std"] = Convert.ToDateTime(dtf.Rows[0]["StartDate"].ToString()).ToShortDateString();
                    ViewState["end"] = Convert.ToDateTime(dtf.Rows[0]["EndDate"].ToString()).ToShortDateString();
                }


                string str12311 = "select count(BatchWorkingDaysId) as BatchWorkingDays from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "' and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                //and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                DataTable ds12311 = new DataTable();
                SqlDataAdapter da12311 = new SqlDataAdapter(str12311, con);
                da12311.Fill(ds12311);

                if (Convert.ToString(ds12311.Rows[0]["BatchWorkingDays"]) != "")
                {
                    ViewState["count"] = Convert.ToString(ds12311.Rows[0]["BatchWorkingDays"]);
                }




                string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "' and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                //and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                DataTable ds123 = new DataTable();
                SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
                da123.Fill(ds123);

                if (ds123.Rows.Count > 0)
                {
                    DataTable dsday = new DataTable();
                    //if (ds123.Rows[0]["day"].ToString() == "Monday")
                    //{



                    string TotalMinutes = "00:00";
                    string TotalMinutes1 = "00:00";
                    string TotalMinutes2 = "00:00";
                    string TotalMinutes3 = "00:00";
                    string TotalMinutes4 = "00:00";
                    string TotalMinutes5 = "00:00";
                    string TotalMinutes6 = "00:00";

                    string outdifftime12 = "00:00";
                    string outdifftime13 = "00:00";
                    string outdifftime14 = "00:00";
                    string outdifftime15 = "00:00";
                    string outdifftime16 = "00:00";
                    string outdifftime17 = "00:00";

                    string s1 = "";
                    string s11 = "";
                    string s2 = "";
                    string s22 = "";
                    string s3 = "";
                    string s33 = "";
                    string s4 = "";
                    string s44 = "";
                    string s5 = "";
                    string s55 = "";
                    string s6 = "";
                    string s66 = "";
                    string s7 = "";
                    string s77 = "";



                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                    daday.Fill(dsday);

                    if (dsday.Rows.Count > 0)
                    {
                        if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                        {
                            TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);
                            outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");

                            string strValues = outdifftime1;
                            string[] strArray = strValues.Split(':');

                            s1 = strArray[0];
                            s11 = strArray[1];
                        }
                        else
                        {
                            s1 = "0";
                            s11 = "0";
                        }
                    }
                    else
                    {
                        s1 = "0";
                        s11 = "0";
                    }


                    DataTable dsday1 = new DataTable();
                    string strday1 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                    SqlDataAdapter daday1 = new SqlDataAdapter(strday1, con);
                    daday1.Fill(dsday1);

                    if (dsday1.Rows.Count > 0)
                    {
                        if (Convert.ToString(dsday1.Rows[0]["totalhours"]) != "")
                        {
                            TotalMinutes1 = Convert.ToString(dsday1.Rows[0]["totalhours"]);
                            outdifftime12 = Convert.ToDateTime(TotalMinutes1).ToString("HH:mm");

                            string strValues1 = outdifftime12;
                            string[] strArray1 = strValues1.Split(':');

                            s2 = strArray1[0];
                            s22 = strArray1[1];
                        }
                        else
                        {
                            s2 = "0";
                            s22 = "0";
                        }

                    }
                    else
                    {
                        s2 = "0";
                        s22 = "0";
                    }


                    DataTable dsday2 = new DataTable();
                    string strday2 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                    SqlDataAdapter daday2 = new SqlDataAdapter(strday2, con);
                    daday2.Fill(dsday2);


                    if (dsday2.Rows.Count > 0)
                    {
                        if (Convert.ToString(dsday2.Rows[0]["totalhours"]) != "")
                        {
                            TotalMinutes2 = Convert.ToString(dsday2.Rows[0]["totalhours"]);
                            outdifftime13 = Convert.ToDateTime(TotalMinutes2).ToString("HH:mm");

                            string strValues2 = outdifftime13;
                            string[] strArray2 = strValues2.Split(':');

                            s3 = strArray2[0];
                            s33 = strArray2[1];
                        }
                        else
                        {
                            s3 = "0";
                            s33 = "0";
                        }

                    }
                    else
                    {
                        s3 = "0";
                        s33 = "0";
                    }



                    DataTable dsday3 = new DataTable();
                    string strday3 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                    SqlDataAdapter daday3 = new SqlDataAdapter(strday3, con);
                    daday3.Fill(dsday3);


                    if (dsday3.Rows.Count > 0)
                    {
                        if (Convert.ToString(dsday3.Rows[0]["totalhours"]) != "")
                        {
                            TotalMinutes3 = Convert.ToString(dsday3.Rows[0]["totalhours"]);
                            outdifftime14 = Convert.ToDateTime(TotalMinutes3).ToString("HH:mm");

                            string strValues3 = outdifftime14;
                            string[] strArray3 = strValues3.Split(':');

                            s4 = strArray3[0];
                            s44 = strArray3[1];
                        }
                        else
                        {
                            s4 = "0";
                            s44 = "0";
                        }

                    }
                    else
                    {
                        s4 = "0";
                        s44 = "0";
                    }



                    DataTable dsday4 = new DataTable();
                    string strday4 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                    SqlDataAdapter daday4 = new SqlDataAdapter(strday4, con);
                    daday4.Fill(dsday4);

                    if (dsday4.Rows.Count > 0)
                    {
                        if (Convert.ToString(dsday4.Rows[0]["totalhours"]) != "")
                        {
                            TotalMinutes4 = Convert.ToString(dsday4.Rows[0]["totalhours"]);
                            outdifftime15 = Convert.ToDateTime(TotalMinutes4).ToString("HH:mm");


                            string strValues4 = outdifftime15;
                            string[] strArray4 = strValues4.Split(':');

                            s5 = strArray4[0];
                            s55 = strArray4[1];
                        }
                        else
                        {
                            s5 = "0";
                            s55 = "0";
                        }
                    }
                    else
                    {
                        s5 = "0";
                        s55 = "0";
                    }



                    DataTable dsday5 = new DataTable();
                    string strday5 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                    SqlDataAdapter daday5 = new SqlDataAdapter(strday5, con);
                    daday5.Fill(dsday5);

                    if (dsday5.Rows.Count > 0)
                    {
                        if (Convert.ToString(dsday5.Rows[0]["totalhours"]) != "")
                        {
                            TotalMinutes5 = Convert.ToString(dsday5.Rows[0]["totalhours"]);
                            outdifftime16 = Convert.ToDateTime(TotalMinutes5).ToString("HH:mm");

                            string strValues5 = outdifftime16;
                            string[] strArray5 = strValues5.Split(':');

                            s6 = strArray5[0];
                            s66 = strArray5[1];
                        }
                        else
                        {
                            s6 = "0";
                            s66 = "0";
                        }
                    }
                    else
                    {
                        s6 = "0";
                        s66 = "0";
                    }



                    DataTable dsday6 = new DataTable();
                    string strday6 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + ddlStore.SelectedValue + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                    SqlDataAdapter daday6 = new SqlDataAdapter(strday6, con);
                    daday6.Fill(dsday6);


                    if (dsday6.Rows.Count > 0)
                    {
                        if (Convert.ToString(dsday6.Rows[0]["totalhours"]) != "")
                        {
                            TotalMinutes6 = Convert.ToString(dsday6.Rows[0]["totalhours"]);
                            outdifftime17 = Convert.ToDateTime(TotalMinutes6).ToString("HH:mm");

                            string strValues6 = outdifftime17;
                            string[] strArray6 = strValues6.Split(':');

                            s7 = strArray6[0];
                            s77 = strArray6[1];
                        }
                        else
                        {
                            s7 = "0";
                            s77 = "0";
                        }
                    }
                    else
                    {
                        s7 = "0";
                        s77 = "0";
                    }


                    int hourss = Convert.ToInt32(s1) + Convert.ToInt32(s2) + Convert.ToInt32(s3) + Convert.ToInt32(s4) + Convert.ToInt32(s5) + Convert.ToInt32(s6) + Convert.ToInt32(s7);

                    int min1 = hourss * 60;

                    int minss = Convert.ToInt32(s11) + Convert.ToInt32(s22) + Convert.ToInt32(s33) + Convert.ToInt32(s44) + Convert.ToInt32(s55) + Convert.ToInt32(s66) + Convert.ToInt32(s77);

                    int finalmin = min1 + minss;

                    int finhr1 = (finalmin / 60);
                    int finhr2 = (finalmin % 60);

                    string finaltime = "";

                    if (finhr2 == 0)
                    {
                        finaltime = finhr1.ToString();
                    }
                    else
                    {
                        finaltime = finhr1.ToString() + "." + finhr2.ToString();
                    }

                    decimal myfinal = 0;

                    if (dsday6.Rows.Count == 0)
                    {
                        myfinal = Convert.ToDecimal(finaltime) / 6;
                    }
                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0)
                    {
                        myfinal = Convert.ToDecimal(finaltime) / 5;
                    }
                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0)
                    {
                        myfinal = Convert.ToDecimal(finaltime) / 4;
                    }
                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0 && dsday3.Rows.Count == 0)
                    {
                        myfinal = Convert.ToDecimal(finaltime) / 3;
                    }
                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0 && dsday3.Rows.Count == 0 && dsday2.Rows.Count == 0)
                    {
                        myfinal = Convert.ToDecimal(finaltime) / 2;
                    }
                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0 && dsday3.Rows.Count == 0 && dsday2.Rows.Count == 0 && dsday1.Rows.Count == 0)
                    {
                        myfinal = Convert.ToDecimal(finaltime) / 1;
                    }
                    else
                    {
                        myfinal = Convert.ToDecimal(finaltime) / 7;
                    }

                    myfinal = Math.Round(myfinal, 2);

                    //  string temp1 = Convert.ToDateTime(finaltime).ToString("HH:mm");

                    Label Labelsdfsdd159 = (Label)grd.FindControl("Labelsdfsdd159");
                    Labelsdfsdd159.Text = myfinal.ToString();

                    Label Labxxx158 = (Label)grd.FindControl("Labxxx158");
                    Labxxx158.Text = ViewState["count"].ToString();

                    //dt1.Rows[rowindex]["dailyhr"] = Convert.ToInt32(finaltime);
                    //dt1.Rows[rowindex]["totald"] = ViewState["count"].ToString();



                    double dec = Convert.ToDouble(myfinal) * Convert.ToDouble(Labxxx158.Text);

                    Label Labtttt59 = (Label)grd.FindControl("Labtttt59");
                    Labtttt59.Text = dec.ToString("###,###.##");


                    string st9 = Labtttt59.Text;
                    if (st9 == "")
                    {
                        st9 = "0";
                    }
                    d9 += Convert.ToDecimal(st9);
                }
            }
            Label15.Text = d9.ToString("###,###.##");

            lbl2rate.Text = Label15.Text;
        }
        if (dt1.Rows.Count == 0)
        {
            Label15.Text = "0";

            lbl2rate.Text = Label15.Text;
        }
        
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void fillaccountdate()
    {
        DataTable dtpreviousyear = new DataTable();

        //        dtpreviousyear = (DataTable)select(" select Top(1) * from ReportPeriod where (Report_Period_Id<> (select Top(1) Report_Period_Id from  ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + ddlStore.SelectedValue + "'  ) ) and Compid='" + Session["comid"] + "' and Whid='" + ddlStore.SelectedValue + "' ");
        dtpreviousyear = (DataTable)select("select * from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + ddlStore.SelectedValue + "' and Report_Period_Id='" + ddlaccount.SelectedValue + "'");


        if (dtpreviousyear.Rows.Count > 0)
        {
            ViewState["StartDateofyear"] = Convert.ToDateTime(dtpreviousyear.Rows[0]["StartDate"]).ToShortDateString().ToString();
            txtestartdate.Text = ViewState["StartDateofyear"].ToString();
        }
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlDataAdapter daf100 = new SqlDataAdapter("select [StartDate],[EndDate] from [ReportPeriod] where [Report_Period_Id]='" + ddlaccount.SelectedValue + "'", con);
        DataTable dtf100 = new DataTable();
        daf100.Fill(dtf100);

        if (dtf100.Rows.Count > 0)
        {
            ViewState["stdate"] = Convert.ToDateTime(dtf100.Rows[0]["StartDate"].ToString()).ToShortDateString();
            ViewState["endate"] = Convert.ToDateTime(dtf100.Rows[0]["EndDate"].ToString()).ToShortDateString();
        }

        if (Convert.ToDateTime(txtestartdate.Text) >= Convert.ToDateTime(ViewState["stdate"]) && Convert.ToDateTime(txtestartdate.Text) <= Convert.ToDateTime(ViewState["endate"]))
        {

            string stryy1 = "insert into EmplAvgCostperhourmaster values('" + ddlStore.SelectedValue + "','" + txtestartdate.Text + ' ' + System.DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + "','" + lbl3rate.Text + "')";

            SqlCommand cmdyy1 = new SqlCommand(stryy1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdyy1.ExecuteNonQuery();
            con.Close();

            SqlDataAdapter daoy1 = new SqlDataAdapter("select max(ID) as ID from EmplAvgCostperhourmaster", con);
            DataTable dtoy1 = new DataTable();
            daoy1.Fill(dtoy1);

            if (dtoy1.Rows.Count > 0)
            {
                ViewState["tbl"] = Convert.ToString(dtoy1.Rows[0]["ID"]);

                foreach (GridViewRow gt in GridView2.Rows)
                {
                    Label Label10 = (Label)gt.FindControl("Label10");

                    SqlDataAdapter dak = new SqlDataAdapter("select AvgRate from EmployeeAvgSalaryMaster where EmployeeId='" + Label10.Text + "'", con);
                    DataTable dtk = new DataTable();
                    dak.Fill(dtk);

                    if (dtk.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtk.Rows[0]["AvgRate"]) != "")
                        {
                            ViewState["ret"] = dtk.Rows[0]["AvgRate"].ToString();
                        }
                        else
                        {
                            ViewState["ret"] = "0";
                        }
                    }
                    else
                    {
                        ViewState["ret"] = "0";
                    }

                    double db = Convert.ToDouble(lbl3rate.Text) + Convert.ToDouble(ViewState["ret"].ToString());
                    string money = db.ToString();

                    SqlCommand cmmm = new SqlCommand("insert into EmplAvgcostperhourdetail values ('" + ViewState["tbl"].ToString() + "','" + Label10.Text + "','" + money + "','" + ViewState["ret"].ToString() + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmmm.ExecuteNonQuery();
                    con.Close();

                }
            }

            string strins = "insert into OverheadAbsorbtionMasterTbl values('" + ddlStore.SelectedValue + "','" + ddlaccount.SelectedValue + "','" + lbl3rate.Text + "','" + txtestartdate.Text + ' ' + System.DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + "')";

            SqlCommand cmd = new SqlCommand(strins, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            SqlDataAdapter dao = new SqlDataAdapter("select max(ID) as ID from OverheadAbsorbtionMasterTbl", con);
            DataTable dto = new DataTable();
            dao.Fill(dto);

            if (dto.Rows.Count > 0)
            {
                ViewState["store"] = Convert.ToString(dto.Rows[0]["ID"]);

                foreach (GridViewRow gdr in GridView1.Rows)
                {
                    Label Label158 = (Label)gdr.FindControl("Label158");
                    Label Label159 = (Label)gdr.FindControl("Label159");

                    SqlCommand cmd1 = new SqlCommand("insert into OverheadAbsorbtionDetailTbl values('" + ViewState["store"].ToString() + "','" + Label158.Text + "','" + Label159.Text + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
            }

            lblmsg.Text = "Record inserted successfully";
        }
        else
        {
            lblmsg.Text = "You cannot select any date earlier/later than the start/end of the year date";
        }


    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DeleteFromGrid(Convert.ToInt32(GridView1.SelectedIndex.ToString()));
        }

        //if (e.CommandName == "Edit")
        //{
        //    int mm = Convert.ToInt32(e.CommandArgument);
        //    ViewState["edit"] = mm;

        //    Button4.Visible = true;
        //    Button2.Visible = false;

        //    SqlDataAdapter dao = new SqlDataAdapter("select * from OverheadAbsorbtionDetailTbl where ID=" + mm, con);
        //    DataTable dto = new DataTable();
        //    dao.Fill(dto);

        //    if (dto.Rows.Count > 0)
        //    {
        //        txtovername.Text = Convert.ToString(dto.Rows[0]["OverHeadName"]);
        //        txtamount.Text = Convert.ToString(dto.Rows[0]["Amount"]);
        //    }

        //}

        //if (e.CommandName == "Delete")
        //{
        //    int mm1 = Convert.ToInt32(e.CommandArgument);

        //    SqlCommand cmd21 = new SqlCommand("delete from OverheadAbsorbtionDetailTbl where ID=" + mm1, con);
        //    if (con.State.ToString() != "Open")
        //    {
        //        con.Open();
        //    }
        //    cmd21.ExecuteNonQuery();
        //    con.Close();

        //    lblmsg.Text = "Record deleted successfully";
        //}
    }

    protected void DeleteFromGrid(int rowindex)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["data"];
        dt.Rows[rowindex].Delete();
        dt.AcceptChanges();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        ViewState["data"] = dt;

        lblmsg.Text = "Record deleted successfully";

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Button5.Visible = true;
        txtamount.Enabled = true;
        txtovername.Enabled = true;
        Button2.Enabled = true;
        Button3.Visible = true;

        //GridView1.Columns[3].Visible = true;
        //GridView1.Columns[4].Visible = true;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Button2.Visible = true;
        Button4.Visible = false;

        SqlCommand cmd21 = new SqlCommand("update OverheadAbsorbtionDetailTbl set OverHeadName='" + txtovername.Text + "',Amount='" + txtamount.Text + "' where ID=" + ViewState["edit"], con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd21.ExecuteNonQuery();
        con.Close();

        lblmsg.Text = "Record updated successfully";

        filleditgrid();
    }

    protected void filleditgrid()
    {
        SqlDataAdapter dafi = new SqlDataAdapter("select * from OverheadAbsorbtionDetailTbl where OverheadAbsorbtionMasterID='" + ViewState["old"] + "'", con);
        DataTable dtfi = new DataTable();
        dafi.Fill(dtfi);

        GridView1.DataSource = dtfi;
        GridView1.DataBind();
    }
    protected void ddlaccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillaccountdate();
        grid2();
        beforepag();

    }


    protected void beforepag()
    {
        SqlDataAdapter daold = new SqlDataAdapter("select * from OverheadAbsorbtionMasterTbl where BusinessID='" + ddlStore.SelectedValue + "' and AccountID='" + ddlaccount.SelectedValue + "' order by ID desc", con);
        DataTable dtold = new DataTable();
        daold.Fill(dtold);

        if (dtold.Rows.Count > 0)
        {
            grid2();


            btnadd.Visible = true;
            Button5.Visible = false;
            pannnn.Visible = true;

            panel1.Visible = true;

            ViewState["old"] = Convert.ToString(dtold.Rows[0]["ID"]);
            lblabsorbrate.Text = Convert.ToString(dtold.Rows[0]["Rate"]);

            lblabsorbrate.Text = String.Format("{0:n}", Convert.ToDecimal(lblabsorbrate.Text));

            lbldate.Text = dtold.Rows[0]["DateCalculated"].ToString();

            txtovername.Enabled = false;
            txtamount.Enabled = false;
            Button2.Enabled = false;


            SqlDataAdapter dafi = new SqlDataAdapter("select * from OverheadAbsorbtionDetailTbl where OverheadAbsorbtionMasterID='" + ViewState["old"] + "'", con);
            DataTable dtfi = new DataTable();
            dafi.Fill(dtfi);

            ViewState["data"] = dtfi;

            //   GridView1.Columns[2].Visible = false;

            if (dtfi.Rows.Count > 0)
            {
                GridView1.DataSource = dtfi;
                GridView1.DataBind();
            }
            decimal d33 = 0;

            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label Label159 = (Label)gr.FindControl("Label159");

                string d3 = Label159.Text;
                d33 += Convert.ToDecimal(d3);
            }
            lbltotal1.Text = d33.ToString("###,###.##");

            lbl1rate.Text = lbltotal1.Text;
            if (lbl2rate.Text == "")
            {
                lblmsg.Text = "Please set your Batch Working Days first.";
                //lbl2rate.Text = String.Format("{0:n}", 0);
                //lbl3rate.Text = String.Format("{0:n}", 0);
            }
            else
            {
                double div = Convert.ToDouble(lbl1rate.Text) / Convert.ToDouble(lbl2rate.Text);
                // lbl3rate.Text = div.ToString("###,###.##");

                lbl3rate.Text = String.Format("{0:n}", Convert.ToDecimal(div));
            }
            //if (lbl3rate.Text != "")
            //{
            //    Button3.Visible = true;
            //}
        }
        else
        {
            pannnn.Visible = false;

            ViewState["data"] = "";
            Button5.Visible = true;
            panel1.Visible = false;

            GridView1.DataSource = null;
            GridView1.DataBind();

            txtamount.Enabled = true;
            txtovername.Enabled = true;

            Button2.Enabled = true;            

            lbltotal1.Text = "0";
            lbl1rate.Text = "0";
            lbl3rate.Text = "0";
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        panelhd.Visible = true;
        Button5.Visible = false;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        int mm = Convert.ToInt32(ddlStore.SelectedValue);

        string te = "OverheadAbsorbtionRateReport.aspx?ID=" + mm;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        pannnn.Visible = true;
        grid2();
    }
}
