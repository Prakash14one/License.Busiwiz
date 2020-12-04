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

public partial class ShoppingCart_Admin_OverHeadAllocation : System.Web.UI.Page
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(PageConn.connnn);
    protected void Page_Load(object sender, EventArgs e)
    {
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

            fillstore();

            filldefault();
            DateTime fristdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            txtsdate.Text = fristdayofmonth.ToShortDateString();
            DateTime lastdaymonth = fristdayofmonth.AddMonths(1).AddDays(-1);
            txtedate.Text = lastdaymonth.ToShortDateString();
            //RadioButtonList1_SelectedIndexChanged(sender, e);
            FillOverHead();

        }

    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;
    }
    protected void fillstore()
    {
        ddlstorename.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlstorename.DataSource = ds;
        ddlstorename.DataTextField = "Name";
        ddlstorename.DataValueField = "WareHouseId";
        ddlstorename.DataBind();

        ddlwarehouse.DataSource = ds;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();
        ddlwarehouse.Items.Insert(0, "All");
        ddlwarehouse.Items[0].Value = "0";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstorename.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

    }

    protected void fillgrid()
    {
        string iid = "";
        for (int i = 0; i < chkgroup.Items.Count; i++)
        {
            if (chkgroup.Items[i].Selected == true)
            {
                if (iid != "")
                {
                    iid = iid + ",";
                }
                iid = iid + chkgroup.Items[i].Value;
            }
        }
        if (iid != "")
        {
            string str1 = "SELECT  AccountMaster.Id  ,GroupCompanyMaster.groupid,GroupCompanyMaster.groupdisplayname,AccountMaster.AccountName as AccountName,AccountMaster.AccountId    FROM         AccountMaster LEFT OUTER JOIN  " +
                     "    ClassTypeCompanyMaster RIGHT OUTER JOIN  " +
                    "   ClassCompanyMaster ON ClassTypeCompanyMaster.id = ClassCompanyMaster.classtypecompanymasterid RIGHT OUTER JOIN  " +
                           "  GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid ON AccountMaster.GroupId = GroupCompanyMaster.GroupId where AccountMaster.Status=1 and AccountMaster.compid = '" + Session["comid"] + "' " +
                    "  and AccountMaster.Whid='" + ddlstorename.SelectedValue + "' and GroupCompanyMaster.whid='" + ddlstorename.SelectedValue + "' and AccountMaster.GroupId In(" + iid + ") order by  ClassTypeCompanyMaster.displayname, ClassCompanyMaster.displayname, GroupCompanyMaster.groupdisplayname, AccountMaster.AccountName asc";

            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            grdaccount.DataSource = ds;
            grdaccount.DataBind();


            //if (RadioButtonList1.SelectedIndex == 0)
            //{

            //}
            //else if (RadioButtonList1.SelectedIndex == 1)
            //{

            //}




            double debitlist = 0;

            double creditlist = 0;
            double FinalBal = 0;

            foreach (GridViewRow gdr in grdaccount.Rows)
            {
                Label lblaccountid = (Label)gdr.FindControl("lblaccountid");

                Label lblamount = (Label)gdr.FindControl("lblamount");
                DropDownList ddlallocation = (DropDownList)gdr.FindControl("ddlallocation");

                CheckBox chk = (CheckBox)(gdr.FindControl("chkinvMasterStatus"));

                string strmethod = "select * from AllocationMethod";
                SqlCommand cmdallocate = new SqlCommand(strmethod, con);
                SqlDataAdapter adpallocate = new SqlDataAdapter(cmdallocate);
                DataTable dtallocate = new DataTable();
                adpallocate.Fill(dtallocate);

                ddlallocation.DataSource = dtallocate;
                ddlallocation.DataTextField = "Name";
                ddlallocation.DataValueField = "Id";
                ddlallocation.DataBind();
                ddlallocation.Items.Insert(0, "Select");
                ddlallocation.Items[0].Value = "0";
                string datefild = " and TranctionMaster.Date between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "'";
                double opnbal1 = 0;
                double totaodebi = 0;
                double totaocredit = 0;
                DataTable dtcrNe1 = new DataTable();
                dtcrNe1 = select("select Distinct Sum(Tranction_Details.AmountDebit)   from   EntryTypeMaster inner join TranctionMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id " +
                " inner join Tranction_Details on Tranction_Details.Tranction_Master_Id   = TranctionMaster.Tranction_Master_Id and TranctionMaster.Whid='" + ddlstorename.SelectedValue + "'  join " +
                "AccountMaster on  AccountMaster.AccountId=Tranction_Details.AccountDebit " +
                "  and  Tranction_Details.whid='" + ddlstorename.SelectedValue + "' and AccountMaster.Whid IS NOT NULL  and  AccountMaster.Whid='" + ddlstorename.SelectedValue + "' and   AccountMaster.compid='" + Session["comid"] + "'" +
                " and AccountMaster.AccountId='" + lblaccountid.Text + "'" + datefild);
                if (Convert.ToString(dtcrNe1.Rows[0][0]) != "")
                {
                    totaodebi = Convert.ToDouble(dtcrNe1.Rows[0][0]);
                }
                DataTable dtcrNe11 = new DataTable();
                dtcrNe11 = select("select Distinct Sum(Tranction_Details.AmountCredit)   from   EntryTypeMaster inner join TranctionMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id " +
                " inner join Tranction_Details on Tranction_Details.Tranction_Master_Id   = TranctionMaster.Tranction_Master_Id and TranctionMaster.Whid='" + ddlstorename.SelectedValue + "'  join " +
                "AccountMaster on  AccountMaster.AccountId=Tranction_Details.AccountCredit " +
                "  and  Tranction_Details.whid='" + ddlstorename.SelectedValue + "' and AccountMaster.Whid IS NOT NULL  and  AccountMaster.Whid='" + ddlstorename.SelectedValue + "' and   AccountMaster.compid='" + Session["comid"] + "' and AccountMaster.AccountId='" + lblaccountid.Text + "'" + datefild);
                if (Convert.ToString(dtcrNe11.Rows[0][0]) != "")
                {
                    totaocredit = Convert.ToDouble(dtcrNe11.Rows[0][0]);
                }

                // opnbal1=totaocredit-totaodebi;
                opnbal1 = totaodebi - totaocredit;
                lblamount.Text = Math.Round(opnbal1, 2).ToString("###,###.##");
                if (lblamount.Text.Length <= 0)
                {
                    lblamount.Text = "0.00";
                }
                else
                {
                    lblamount.Text = String.Format("{0:n}", Convert.ToDecimal(lblamount.Text));
                }



            }
        }
    }
    //protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    grdaccount.DataSource = null;
    //    grdaccount.DataBind();
    //    if (RadioButtonList1.SelectedIndex == 0)
    //    {
    //        txtsdate.Text = System.DateTime.Now.ToShortDateString();
    //        txtedate.Text = System.DateTime.Now.ToShortDateString();
    //        Panel12.Visible = false;
    //        Panel1.Visible = true;

    //    }
    //    else if (RadioButtonList1.SelectedIndex == 1)
    //    {
    //        Panel12.Visible = true;
    //        Panel1.Visible = false;

    //        SqlCommand cmd = new SqlCommand("select PeriodId,PeriodName from PeriodMaster", con);
    //        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        dtp.Fill(ds);

    //        ddlperiod.DataSource = ds;
    //        ddlperiod.DataTextField = "PeriodName";
    //        ddlperiod.DataValueField = "PeriodId";
    //        ddlperiod.DataBind();
    //        ddlperiod.Items.Insert(0, "All");

    //        ddlperiod.Items[0].Value = "0";
    //        ddlperiod.SelectedIndex = 0;

    //        ddlperiod.SelectedIndex = ddlperiod.Items.IndexOf(ddlperiod.Items.FindByValue("5"));

    //        ddlperiod_SelectedIndexChanged(sender, e);
    //    }
    //}
    protected void ddlperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        PeriodDiff();
        txtsdate.Text = ViewState["SDate"].ToString();
        txtedate.Text = ViewState["EDate"].ToString();

        //ViewState["SDate"] = sd.ToShortDateString();
        //ViewState["EDate"] = ed.ToShortDateString();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        lblmb.Text = "Business : " + ddlwarehouse.SelectedItem.Text;

        if (txtdatefrom.Text != "" && txtdateto.Text != "")
        {

            lblbbbbbb.Text = "From Date : " + txtdatefrom.Text + " To Date : " + txtdateto.Text;
        }
        lblovern.Text = "Over Head Allocation Name : " + txtoverheadname.Text;
        grdaccount.DataSource = null;
        grdaccount.DataBind();
        try
        {
            filldegroup();
            //fillgrid();
            Filljobgird();
        }
        catch (Exception ez)
        {
            lblmsg.Text = ez.ToString();
        }
        Button4.Visible = true;
        Button5.Visible = false;
        btncan.Visible = true;

        Panel3.Visible = true;
        Panel4.Visible = true;

        pnladdval.Visible = true;

    }



    protected void Button4_Click(object sender, EventArgs e)
    {

        btncan.Visible = true;
        double Count = 0;

        string str = "select count(Id) as Id from JobMaster WHERE Whid='" + ddlstorename.SelectedValue + "' and JobStartDate>='" + txtdatefrom.Text + "' and JobEndDate<='" + txtdateto.Text + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            Count = Convert.ToDouble(ds.Rows[0]["Id"].ToString());
        }


        string materialin = "";
        double Qty = 0;
        double Rate = 0;
        double TotalCost = 0;
        double FinalCost = 0;

        double Qty1 = 0;
        double Rate1 = 0;
        double TotalCost1 = 0;
        double FinalCost1 = 0;

        double LabourCost = 0;
        double FinalLabourCost = 0;
        double LabourCost1 = 0;
        double FinalLabourCost1 = 0;


        double TotalMaterialOfPeriod = 0;
        double TotalLabourOfPeriod = 0;


        double TotalDays1 = 0;
        double TotalDays = 0;


        foreach (GridViewRow gdr in grdjob.Rows)
        {
            Label lbljobmasterid = (Label)gdr.FindControl("lbljobmasterid");

            Label lblinvmasterid = (Label)gdr.FindControl("lblinvmasterid");

            CheckBox chkjobmaster = (CheckBox)(gdr.FindControl("chkjobmaster"));

            Label lbldirectmaterial = (Label)gdr.FindControl("lbldirectmaterial");
            Label lbldirectmaterialperiod = (Label)gdr.FindControl("lbldirectmaterialperiod");
            Label lbldirectlabourperiod = (Label)gdr.FindControl("lbldirectlabourperiod");
            Label lbldirectlabour = (Label)gdr.FindControl("lbldirectlabour");
            Label lblnoofdaysperiod = (Label)gdr.FindControl("lblnoofdaysperiod");

            if (chkjobmaster.Checked == true)
            {


                //material issue id
                string avgmaterial = "select * from MaterialIssueMasterTbl where JobMasterId='" + lbljobmasterid.Text + "'";
                SqlCommand cmdavgmat = new SqlCommand(avgmaterial, con);
                SqlDataAdapter adpavgmat = new SqlDataAdapter(cmdavgmat);
                DataTable dsavgma = new DataTable();
                adpavgmat.Fill(dsavgma);
                if (dsavgma.Rows.Count > 0)
                {

                    string strId = "";
                    string strInvAllIds = "";
                    string strtemp = "";
                    foreach (DataRow dtrrr in dsavgma.Rows)
                    {
                        strId = dtrrr["Id"].ToString();
                        strInvAllIds = strId + "," + strInvAllIds;
                        strtemp = strInvAllIds.Substring(0, (strInvAllIds.Length - 1));
                    }

                    materialin = " InventoryWarehouseMasterAvgCostTbl.MaterialIssueMasterTblId In (" + strtemp + ") and";

                }
                //direct material

                string st123 = "select * from InventoryWarehouseMasterAvgCostTbl where " + materialin + "  Qty Is Not Null Order by DateUpdated,IWMAvgCostId ";
                SqlCommand cmd123 = new SqlCommand(st123, con);
                SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
                DataTable dt123 = new DataTable();
                adp123.Fill(dt123);
                if (dt123.Rows.Count > 0)
                {
                    foreach (DataRow dtp in dt123.Rows)
                    {
                        Qty = Convert.ToDouble(dtp["Qty"].ToString());
                        if (Qty > 0)
                        {
                            if (Convert.ToString(dtp["Rate"]) != "")
                            {
                                Rate = Convert.ToDouble(dtp["Rate"].ToString());
                            }
                            else
                            {
                                Rate = 0;
                            }

                            TotalCost = Qty * Rate;

                            FinalCost += TotalCost;

                        }
                    }


                }

                lbldirectmaterial.Text = Math.Round(FinalCost, 2).ToString("###,###.##");

                if (lbldirectmaterial.Text.Length <= 0)
                {
                    lbldirectmaterial.Text = "0.00";
                }
                else
                {
                    lbldirectmaterial.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectmaterial.Text));
                }


                //direct material for Period
                string st131 = "select * from InventoryWarehouseMasterAvgCostTbl where " + materialin + "  Qty Is Not Null  and DateUpdated between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "' order by  DateUpdated,IWMAvgCostId  ";
                SqlCommand cmd131 = new SqlCommand(st131, con);
                SqlDataAdapter adp131 = new SqlDataAdapter(cmd131);
                DataTable dt131 = new DataTable();
                adp131.Fill(dt131);
                if (dt131.Rows.Count > 0)
                {
                    foreach (DataRow dtp1 in dt131.Rows)
                    {
                        Qty1 = Convert.ToDouble(dtp1["Qty"].ToString());
                        if (Qty1 > 0)
                        {
                            if (Convert.ToString(dtp1["Rate"]) != "")
                            {
                                Rate1 = Convert.ToDouble(dtp1["Rate"].ToString());

                            }
                            else
                            {
                                Rate1 = 0;
                            }
                            TotalCost1 = Qty1 * Rate1;

                            FinalCost1 += TotalCost1;
                        }
                    }


                }
                lbldirectmaterialperiod.Text = Math.Round(FinalCost1, 2).ToString("###,###.##");


                if (lbldirectmaterialperiod.Text.Length <= 0)
                {
                    lbldirectmaterialperiod.Text = "0.00";
                }
                else
                {
                    lbldirectmaterialperiod.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectmaterialperiod.Text));
                }
                //direct labour
                string st454 = " select * from JobEmployeeDailyTaskTbl inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobDailyTaskMasterId=JobEmployeeDailyTaskTbl.Id where JobEmployeeDailyTaskDetail.JobMasterId='" + lbljobmasterid.Text + "' ";
                SqlCommand cmd454 = new SqlCommand(st454, con);
                SqlDataAdapter adp454 = new SqlDataAdapter(cmd454);
                DataTable dt454 = new DataTable();
                adp454.Fill(dt454);
                if (dt454.Rows.Count > 0)
                {
                    foreach (DataRow dt4 in dt454.Rows)
                    {
                        LabourCost1 = Convert.ToDouble(dt4["Cost"].ToString());

                        FinalLabourCost1 += LabourCost1;
                    }


                }
                lbldirectlabour.Text = Math.Round(FinalLabourCost1, 2).ToString("###,###.##");

                if (lbldirectlabour.Text.Length <= 0)
                {
                    lbldirectlabour.Text = "0.00";
                }
                else
                {
                    lbldirectlabour.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectlabour.Text));
                }
                //direct labour period

                string st151 = " select * from JobEmployeeDailyTaskTbl inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobDailyTaskMasterId=JobEmployeeDailyTaskTbl.Id where JobEmployeeDailyTaskDetail.JobMasterId='" + lbljobmasterid.Text + "' and JobEmployeeDailyTaskTbl.Date  between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "'";
                SqlCommand cmd151 = new SqlCommand(st151, con);
                SqlDataAdapter adp151 = new SqlDataAdapter(cmd151);
                DataTable dt151 = new DataTable();
                adp151.Fill(dt151);
                if (dt151.Rows.Count > 0)
                {
                    foreach (DataRow dt15 in dt151.Rows)
                    {
                        LabourCost = Convert.ToDouble(dt15["Cost"].ToString());

                        FinalLabourCost += LabourCost;
                    }


                }
                lbldirectlabourperiod.Text = Math.Round(FinalLabourCost, 2).ToString("###,###.##");

                if (lbldirectlabourperiod.Text.Length <= 0)
                {
                    lbldirectlabourperiod.Text = "0.00";
                }
                else
                {
                    lbldirectlabourperiod.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectlabourperiod.Text));
                }
                string stjob = "select * from JobMaster where compid='" + Session["Comid"].ToString() + "' and Id='" + lbljobmasterid.Text + "'";
                SqlCommand cmdjob = new SqlCommand(stjob, con);
                SqlDataAdapter adpjob = new SqlDataAdapter(cmdjob);
                DataTable dsjob = new DataTable();
                adpjob.Fill(dsjob);
                string day1 = "";
                string day2 = "";
                if (dsjob.Rows.Count > 0)
                {
                    day1 = dsjob.Rows[0]["JobStartDate"].ToString();
                    day2 = dsjob.Rows[0]["JobEndDate"].ToString();
                    Session["1"] = Convert.ToDateTime(day2.ToString());
                    Session["2"] = Convert.ToDateTime(txtdateto.Text);

                    if (Convert.ToDateTime(day2.ToString()) > Convert.ToDateTime(txtdateto.Text))
                    {
                        string std1 = " select DATEDIFF(day,'" + txtdatefrom.Text + "','" + txtdateto.Text + "') as Day ";
                        SqlCommand cmdjd1 = new SqlCommand(std1, con);
                        SqlDataAdapter adpdaysjd1 = new SqlDataAdapter(cmdjd1);
                        DataTable dtjd1 = new DataTable();
                        adpdaysjd1.Fill(dtjd1);

                        if (dtjd1.Rows.Count > 0)
                        {
                            TotalDays1 = Convert.ToDouble(dtjd1.Rows[0]["Day"].ToString()) + 1;
                        }

                    }
                    else
                    {
                        string strdays = " select DATEDIFF(day,'" + txtdatefrom.Text + "','" + Convert.ToDateTime(day2.ToString()) + "') as Day ";
                        SqlCommand cmddays = new SqlCommand(strdays, con);
                        SqlDataAdapter adpdays = new SqlDataAdapter(cmddays);
                        DataTable dtdays = new DataTable();
                        adpdays.Fill(dtdays);
                        if (dtdays.Rows.Count > 0)
                        {
                            TotalDays1 = Convert.ToDouble(dtdays.Rows[0]["Day"].ToString()) + 1;
                        }


                    }
                    lblnoofdaysperiod.Text = TotalDays1.ToString("###,###.##");

                    if (lblnoofdaysperiod.Text.Length <= 0)
                    {
                        lblnoofdaysperiod.Text = "0.00";
                    }
                    else
                    {
                        lblnoofdaysperiod.Text = String.Format("{0:n}", Convert.ToDecimal(lblnoofdaysperiod.Text));
                    }
                }








                TotalDays += TotalDays1;

                TotalMaterialOfPeriod += FinalCost1;

                TotalLabourOfPeriod += FinalLabourCost;
            }
        }
        double finaltotal = 0;
        foreach (GridViewRow gdr in grdjob.Rows)
        {


            Label lbljobmasterid = (Label)gdr.FindControl("lbljobmasterid");

            Label lblinvmasterid = (Label)gdr.FindControl("lblinvmasterid");

            CheckBox chkjobmaster = (CheckBox)(gdr.FindControl("chkjobmaster"));

            Label lbldirectmaterial = (Label)gdr.FindControl("lbldirectmaterial");
            Label lbldirectmaterialperiod = (Label)gdr.FindControl("lbldirectmaterialperiod");
            Label lbldirectlabourperiod = (Label)gdr.FindControl("lbldirectlabourperiod");
            Label lbldirectlabour = (Label)gdr.FindControl("lbldirectlabour");

            Label lblnoofdaysperiod = (Label)gdr.FindControl("lblnoofdaysperiod");

            TextBox txtohbymaterial = (TextBox)gdr.FindControl("txtohbymaterial");

            TextBox txtohbylabour = (TextBox)gdr.FindControl("txtohbylabour");

            TextBox txtohbydays = (TextBox)gdr.FindControl("txtohbydays");

            TextBox txtohbyequal = (TextBox)gdr.FindControl("txtohbyequal");
            TextBox txtfinaltotal = (TextBox)gdr.FindControl("txtfinaltotal");


            double Final1M = 0;
            double Final1l = 0;
            double Final1d = 0;
            double Final1e = 0;
            if (chkjobmaster.Checked == true)
            {
                foreach (GridViewRow gdr12 in grdaccount.Rows)
                {
                    Label lblaccountid = (Label)gdr12.FindControl("lblaccountid");

                    Label lblamount = (Label)gdr12.FindControl("lblamount");
                    DropDownList ddlallocation = (DropDownList)gdr12.FindControl("ddlallocation");

                    CheckBox chk = (CheckBox)(gdr12.FindControl("chkinvMasterStatus"));
                    TextBox txtamountallocate = (TextBox)gdr12.FindControl("txtamountallocate");

                    //Material method
                    if (ddlallocation.SelectedIndex == 1 && chkjobmaster.Checked == true)
                    {
                        double totalOver1M = Convert.ToDouble(txtamountallocate.Text);
                        Final1M += totalOver1M;
                        Final1M = Math.Round(Final1M, 2);






                    }
                    //Labour method
                    if (ddlallocation.SelectedIndex == 2 && chkjobmaster.Checked == true)
                    {
                        double totalOver1l = Convert.ToDouble(txtamountallocate.Text);
                        Final1l += totalOver1l;
                        Final1l = Math.Round(Final1l, 2);






                    }
                    //Days method
                    if (ddlallocation.SelectedIndex == 3 && chkjobmaster.Checked == true)
                    {
                        double totalOver1d = Convert.ToDouble(txtamountallocate.Text);
                        Final1d += totalOver1d;
                        Final1d = Math.Round(Final1d, 2);





                    }
                    //equal method
                    if (ddlallocation.SelectedIndex == 4 && chkjobmaster.Checked == true)
                    {
                        double totalOver1e = Convert.ToDouble(txtamountallocate.Text);
                        Final1e += totalOver1e;
                        Final1e = Math.Round(Final1e, 2);


                    }


                }

                double Fin1 = 0;
                double Fin2 = 0;
                double Fin3 = 0;
                double Fin4 = 0;
                double Alltotal = 0;
                Fin1 = (Convert.ToDouble(lbldirectmaterialperiod.Text) / TotalMaterialOfPeriod) * Final1M;
                Fin2 = (Convert.ToDouble(lbldirectlabourperiod.Text) / TotalLabourOfPeriod) * Final1l;
                Fin3 = (Convert.ToDouble(lblnoofdaysperiod.Text) / TotalDays) * Final1d;
                Fin4 = Final1e / Count;
                if (Convert.ToString(Fin1) == "NaN")
                {
                    Fin1 = 0;
                }
                if (Convert.ToString(Fin2) == "NaN")
                {
                    Fin2 = 0;
                }
                if (Convert.ToString(Fin3) == "NaN")
                {
                    Fin3 = 0;
                }
                if (Convert.ToString(Fin4) == "NaN")
                {
                    Fin4 = 0;
                }


                txtohbymaterial.Text = Math.Round(Fin1, 2).ToString("###,###.##");
                if (txtohbymaterial.Text.Length <= 0)
                {
                    txtohbymaterial.Text = "0.00";
                }
                else
                {
                    txtohbymaterial.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbymaterial.Text));
                }
                txtohbylabour.Text = Math.Round(Fin2, 2).ToString("###,###.##");
                if (txtohbylabour.Text.Length <= 0)
                {
                    txtohbylabour.Text = "0.00";
                }
                else
                {
                    txtohbylabour.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbylabour.Text));
                }
                txtohbydays.Text = Math.Round(Fin3, 2).ToString("###,###.##");
                if (txtohbydays.Text.Length <= 0)
                {
                    txtohbydays.Text = "0.00";
                }
                else
                {
                    txtohbydays.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbydays.Text));
                }
                txtohbyequal.Text = Math.Round(Fin4, 2).ToString("###,###.##");
                if (txtohbyequal.Text.Length <= 0)
                {
                    txtohbyequal.Text = "0.00";
                }
                else
                {
                    txtohbyequal.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbyequal.Text));
                }
                Alltotal = Fin1 + Fin2 + Fin3 + Fin4;
                txtfinaltotal.Text = Math.Round(Alltotal, 2).ToString("###,###.##");

                if (txtfinaltotal.Text.Length <= 0)
                {
                    txtfinaltotal.Text = "0.00";
                }
                else
                {
                    txtfinaltotal.Text = String.Format("{0:n}", Convert.ToDecimal(txtfinaltotal.Text));
                }
                finaltotal += Convert.ToDouble(txtfinaltotal.Text);
            }
        }
        if (grdjob.Rows.Count > 0)
        {
            GridViewRow ft = (GridViewRow)(grdjob.FooterRow);
            Label footertotal = (Label)(ft.FindControl("footertotal"));
            footertotal.Text = Math.Round(finaltotal, 2).ToString("###,###.##");

            if (footertotal.Text.Length <= 0)
            {
                footertotal.Text = "0.00";
            }
            else
            {
                footertotal.Text = String.Format("{0:n}", Convert.ToDecimal(footertotal.Text));
            }
        }

        Button5.Visible = true;
        lblcalms.Text = "Please review the calculated allocation amount, and select submit if overhead amount is correct and you would like to apply overheads to work orders (projects).";



    }

    protected void Filljobgird()
    {
        string str = "select * from JobMaster WHERE Whid='" + ddlstorename.SelectedValue + "' and JobStartDate>='" + txtdatefrom.Text + "' and JobEndDate<='" + txtdateto.Text + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        grdjob.DataSource = ds;
        grdjob.DataBind();

        string materialin = "";
        double Qty = 0;
        double Rate = 0;
        double TotalCost = 0;
        double FinalCost = 0;

        double Qty1 = 0;
        double Rate1 = 0;
        double TotalCost1 = 0;
        double FinalCost1 = 0;

        double LabourCost = 0;
        double FinalLabourCost = 0;
        double LabourCost1 = 0;
        double FinalLabourCost1 = 0;


        double TotalMaterialOfPeriod = 0;





        foreach (GridViewRow gdr in grdjob.Rows)
        {
            Label lbljobmasterid = (Label)gdr.FindControl("lbljobmasterid");

            Label lblinvmasterid = (Label)gdr.FindControl("lblinvmasterid");

            CheckBox chkjobmaster = (CheckBox)(gdr.FindControl("chkjobmaster"));

            Label lbldirectmaterial = (Label)gdr.FindControl("lbldirectmaterial");
            Label lbldirectmaterialperiod = (Label)gdr.FindControl("lbldirectmaterialperiod");
            Label lbldirectlabourperiod = (Label)gdr.FindControl("lbldirectlabourperiod");
            Label lbldirectlabour = (Label)gdr.FindControl("lbldirectlabour");


            //material issue id
            string avgmaterial = "select * from MaterialIssueMasterTbl where JobMasterId='" + lbljobmasterid.Text + "'";
            SqlCommand cmdavgmat = new SqlCommand(avgmaterial, con);
            SqlDataAdapter adpavgmat = new SqlDataAdapter(cmdavgmat);
            DataTable dsavgma = new DataTable();
            adpavgmat.Fill(dsavgma);
            if (dsavgma.Rows.Count > 0)
            {

                string strId = "";
                string strInvAllIds = "";
                string strtemp = "";
                foreach (DataRow dtrrr in dsavgma.Rows)
                {
                    strId = dtrrr["Id"].ToString();
                    strInvAllIds = strId + "," + strInvAllIds;
                    strtemp = strInvAllIds.Substring(0, (strInvAllIds.Length - 1));
                }

                materialin = "  InventoryWarehouseMasterAvgCostTbl.MaterialIssueMasterTblId In (" + strtemp + ") and ";

            }
            //direct material
            //string str12 = "0";

            string st123 = "select * from InventoryWarehouseMasterAvgCostTbl where   " + materialin + " Qty is Not Null order by DateUpdated,IWMAvgCostId ";
            SqlCommand cmd123 = new SqlCommand(st123, con);
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
            DataTable dt123 = new DataTable();
            adp123.Fill(dt123);
            if (dt123.Rows.Count > 0)
            {
                foreach (DataRow dtp in dt123.Rows)
                {

                    Qty = Convert.ToDouble(dtp["Qty"].ToString());
                    if (Qty > 0)
                    {
                        if (Convert.ToString(dtp["Rate"]) != "")
                        {
                            Rate = Convert.ToDouble(dtp["Rate"].ToString());
                        }
                        else
                        {
                            Rate = 0;
                        }

                        TotalCost = Qty * Rate;

                        FinalCost += TotalCost;
                    }


                }


            }

            lbldirectmaterial.Text = Math.Round(FinalCost, 2).ToString("###,###.##");

            if (lbldirectmaterial.Text.Length <= 0)
            {
                lbldirectmaterial.Text = "0.00";
            }
            else
            {
                lbldirectmaterial.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectmaterial.Text));
            }
            //decimal OLDavgcost = 0;
            //decimal oLDqtyONHAND = 0;
            //DataTable dt123 = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl " +
            //" inner join MaterialIssueMasterTbl on MaterialIssueMasterTbl.Id where  DateUpdated<='" + txtdateto.Text + "' and MaterialIssueMasterTbl.JobMasterId='" + lbljobmasterid.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc");

            //if (dt123.Rows.Count > 0)
            //{

            //    if (Convert.ToString(dt123.Rows[0]["AvgCost"]) != "")
            //    {
            //        OLDavgcost = Convert.ToDecimal(dt123.Rows[0]["AvgCost"]);
            //    }
            //    if (Convert.ToString(dt123.Rows[0]["QtyonHand"]) != "")
            //    {
            //        oLDqtyONHAND = Convert.ToDecimal(dt123.Rows[0]["QtyonHand"]);
            //    }
            //}
            //decimal avxc = Math.Round((OLDavgcost * oLDqtyONHAND), 2);
            //FinalCost += Convert.ToDouble(avxc); ;
            //lbldirectmaterial.Text = Math.Round(FinalCost, 2).ToString("###,###.##");

            //if (lbldirectmaterial.Text.Length <= 0)
            //{
            //    lbldirectmaterial.Text = "0.00";
            //}
            //else
            //{
            //    lbldirectmaterial.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectmaterial.Text));
            //}

            //direct material for Period
            string st131 = "select * from InventoryWarehouseMasterAvgCostTbl where " + materialin + " Qty is Not Null and DateUpdated between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "' order by DateUpdated,IWMAvgCostId ";
            SqlCommand cmd131 = new SqlCommand(st131, con);
            SqlDataAdapter adp131 = new SqlDataAdapter(cmd131);
            DataTable dt131 = new DataTable();
            adp131.Fill(dt131);
            if (dt131.Rows.Count > 0)
            {
                foreach (DataRow dtp1 in dt131.Rows)
                {
                    Qty1 = Convert.ToDouble(dtp1["Qty"].ToString());
                    if (Qty1 > 0)
                    {
                        if (Convert.ToString(dtp1["Rate"]) != "")
                        {
                            Rate1 = Convert.ToDouble(dtp1["Rate"].ToString());
                        }
                        else
                        {
                            Rate1 = 0;
                        }

                        TotalCost1 = Qty1 * Rate1;

                        FinalCost1 += TotalCost1;
                    }
                }


            }
            lbldirectmaterialperiod.Text = Math.Round(FinalCost1, 2).ToString("###,###.##");
            if (lbldirectmaterialperiod.Text.Length <= 0)
            {
                lbldirectmaterialperiod.Text = "0.00";
            }
            else
            {
                lbldirectmaterialperiod.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectmaterialperiod.Text));
            }
            //direct labour
            string st454 = " select * from JobEmployeeDailyTaskTbl inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobDailyTaskMasterId=JobEmployeeDailyTaskTbl.Id where JobEmployeeDailyTaskDetail.JobMasterId='" + lbljobmasterid.Text + "'";
            SqlCommand cmd454 = new SqlCommand(st454, con);
            SqlDataAdapter adp454 = new SqlDataAdapter(cmd454);
            DataTable dt454 = new DataTable();
            adp454.Fill(dt454);
            if (dt454.Rows.Count > 0)
            {
                foreach (DataRow dt4 in dt454.Rows)
                {
                    LabourCost1 = Convert.ToDouble(dt4["Cost"].ToString());

                    FinalLabourCost1 += LabourCost1;
                }


            }
            lbldirectlabour.Text = Math.Round(FinalLabourCost1, 2).ToString("###,###.##");

            if (lbldirectlabour.Text.Length <= 0)
            {
                lbldirectlabour.Text = "0.00";
            }
            else
            {
                lbldirectlabour.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectlabour.Text));
            }
            //direct labour period

            string st151 = " select * from JobEmployeeDailyTaskTbl inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobDailyTaskMasterId=JobEmployeeDailyTaskTbl.Id where JobEmployeeDailyTaskDetail.JobMasterId='" + lbljobmasterid.Text + "' and JobEmployeeDailyTaskTbl.Date  between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "'";
            SqlCommand cmd151 = new SqlCommand(st151, con);
            SqlDataAdapter adp151 = new SqlDataAdapter(cmd151);
            DataTable dt151 = new DataTable();
            adp151.Fill(dt151);
            if (dt151.Rows.Count > 0)
            {
                foreach (DataRow dt15 in dt151.Rows)
                {
                    LabourCost = Convert.ToDouble(dt15["Cost"].ToString());

                    FinalLabourCost += LabourCost;
                }


            }
            lbldirectlabourperiod.Text = Math.Round(FinalLabourCost, 2).ToString("###,###.##");


            if (lbldirectlabourperiod.Text.Length <= 0)
            {
                lbldirectlabourperiod.Text = "0.00";
            }
            else
            {
                lbldirectlabourperiod.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectlabourperiod.Text));
            }

            TotalMaterialOfPeriod += FinalCost1;
        }
        Seccolin(CheckBoxList1, grdjob);
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        double total = 0;
        DataTable dtr = select("select Id,StartDate,EndDate from OverHeadAllocationMaster where ((StartDate between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "') or (EndDate between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "')) and  Whid='" + ddlstorename.SelectedValue + "'");
        if (dtr.Rows.Count == 0)
        {
            if (grdjob.Rows.Count > 0)
            {
                GridViewRow ft = (GridViewRow)grdjob.FooterRow;
                Label footertotal = (Label)(ft.FindControl("footertotal"));
                total += Convert.ToDouble(footertotal.Text);

            }
            total = Math.Round(total, 2);
            string insertdaily = "Insert Into OverHeadAllocationMaster (StartDate,EndDate,TotalAmountOverHead,Name,Whid,compid)values('" + txtdatefrom.Text + "','" + txtdateto.Text + "','" + total + "','" + txtoverheadname.Text + "','" + ddlstorename.SelectedValue + "','" + Session["Comid"].ToString() + "')";
            SqlCommand cmddaily = new SqlCommand(insertdaily, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddaily.ExecuteNonQuery();
            con.Close();

            string str = "select max(Id) as OverHeadAllocationMasterId from OverHeadAllocationMaster ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            double maxid = Convert.ToDouble(ds.Rows[0]["OverHeadAllocationMasterId"].ToString());
            /////// trans jv

            int ety = 1;
            int Id1 = 0;
            DataTable ds131b = select(" SELECT     EntryTypeId, Max(EntryNumber) as Number FROM  TranctionMaster " +
                                " Where EntryTypeId = 3 and Whid='" + ddlstorename.SelectedValue + "' Group by EntryTypeId ");

            if (ds131b.Rows.Count > 0)
            {
                if (Convert.ToString(ds131b.Rows[0]["Number"]) != "")
                {
                    ety = Convert.ToInt32(ds131b.Rows[0]["Number"]) + 1;

                }
            }

            SqlCommand cd3 = new SqlCommand("Sp_Insert_TranctionMasterRetIdentity", con);

            cd3.CommandType = CommandType.StoredProcedure;
            cd3.Parameters.AddWithValue("@Date", txtdateto.Text);
            cd3.Parameters.AddWithValue("@EntryNumber", ety);
            cd3.Parameters.AddWithValue("@EntryTypeId", "3");
            cd3.Parameters.AddWithValue("@UserId", Session["UserId"]);
            cd3.Parameters.AddWithValue("@Tranction_Amount", Convert.ToDecimal(total));
            cd3.Parameters.AddWithValue("@whid", ddlstorename.SelectedValue);

            cd3.Parameters.AddWithValue("@compid", HttpContext.Current.Session["Comid"]);


            cd3.Parameters.Add(new SqlParameter("@Tranction_Master_Id", SqlDbType.Int));
            cd3.Parameters["@Tranction_Master_Id"].Direction = ParameterDirection.Output;
            cd3.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
            cd3.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            Id1 = (int)cd3.ExecuteNonQuery();
            Id1 = Convert.ToInt32(cd3.Parameters["@Tranction_Master_Id"].Value);
            con.Close();

            decimal invtranamt = 0;
            if (grdjob.Rows.Count > 0)
            {
                foreach (GridViewRow gdr in grdjob.Rows)
                {

                    Label lbljobmasterid = (Label)gdr.FindControl("lbljobmasterid");

                    Label lblinvmasterid = (Label)gdr.FindControl("lblinvmasterid");

                    CheckBox chkjobmaster = (CheckBox)(gdr.FindControl("chkjobmaster"));

                    Label lbldirectmaterial = (Label)gdr.FindControl("lbldirectmaterial");
                    Label lbldirectlabour = (Label)gdr.FindControl("lbldirectlabour");
                    Label lblnoofdays = (Label)gdr.FindControl("lblnoofdays");

                    Label lbldirectmaterialperiod = (Label)gdr.FindControl("lbldirectmaterialperiod");
                    Label lbldirectlabourperiod = (Label)gdr.FindControl("lbldirectlabourperiod");
                    Label lblnoofdaysperiod = (Label)gdr.FindControl("lblnoofdaysperiod");




                    TextBox txtohbymaterial = (TextBox)gdr.FindControl("txtohbymaterial");

                    TextBox txtohbylabour = (TextBox)gdr.FindControl("txtohbylabour");

                    TextBox txtohbydays = (TextBox)gdr.FindControl("txtohbydays");

                    TextBox txtohbyequal = (TextBox)gdr.FindControl("txtohbyequal");

                    TextBox txtfinaltotal = (TextBox)gdr.FindControl("txtfinaltotal");
                    if (chkjobmaster.Checked == true)
                    {
                        string StrInsert = "Insert Into OverHeadAllocationJobDetail " +
                         "   (OverHeadMasterId,JobMasterId,DirectMaterialCost,DirectLabourCost,DirectMaterialCostOfPeriod,DirectLabourCostOfPeriod,NoOfDaysCostForPeriod,OhByMaterial,OhByLabour,OhByDays,Ohbyequal,OhAllocationtotal,Active)values " +
                         "  ('" + maxid + "','" + lbljobmasterid.Text + "','" + lbldirectmaterial.Text + "','" + lbldirectlabour.Text + "','" + lbldirectmaterialperiod.Text + "','" + lbldirectlabourperiod.Text + "','" + lblnoofdaysperiod.Text + "','" + txtohbymaterial.Text + "','" + txtohbylabour.Text + "','" + txtohbydays.Text + "','" + txtohbyequal.Text + "','" + txtfinaltotal.Text + "','" + chkjobmaster.Checked + "') ";
                        SqlCommand cmdinsert = new SqlCommand(StrInsert, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdinsert.ExecuteNonQuery();
                        con.Close();


                        decimal OLDavgcost = 0;
                        decimal oLDqtyONHAND = 0;
                        DataTable Dataacces = select("Select InvWMasterId  from JobMaster where Id='" + lbljobmasterid.Text + "'");

                        if (Dataacces.Rows.Count > 0)
                        {
                            DataTable dt123 = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl " +
                            "  inner join MaterialIssueMasterTbl on MaterialIssueMasterTbl.Id=InventoryWarehouseMasterAvgCostTbl.MaterialIssueMasterTblId where InvWMasterId='" + Dataacces.Rows[0]["InvWMasterId"] + "' and DateUpdated<='" + txtdateto.Text + "' and MaterialIssueMasterTbl.JobMasterId='" + lbljobmasterid.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc");

                            if (dt123.Rows.Count > 0)
                            {

                                if (Convert.ToString(dt123.Rows[0]["AvgCost"]) != "")
                                {
                                    OLDavgcost = Convert.ToDecimal(dt123.Rows[0]["AvgCost"]);
                                }
                                if (Convert.ToString(dt123.Rows[0]["QtyonHand"]) != "")
                                {
                                    oLDqtyONHAND = Convert.ToDecimal(dt123.Rows[0]["QtyonHand"]);
                                }

                            }
                            if (oLDqtyONHAND == 0)
                            {
                                oLDqtyONHAND = 1;
                            }
                            invtranamt += Convert.ToDecimal(txtfinaltotal.Text);
                            decimal avxc = (OLDavgcost * oLDqtyONHAND) + (Convert.ToDecimal(txtfinaltotal.Text)) / oLDqtyONHAND;

                            string ABCD = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,Rate,DateUpdated,AvgCost,QtyonHand)values('" + Dataacces.Rows[0]["InvWMasterId"] + "','" + Id1 + "','" + oLDqtyONHAND + "','" + Math.Round(avxc, 2) + "','" + txtdateto.Text + "','" + Math.Round(avxc, 2) + "','" + oLDqtyONHAND + "')";
                            SqlCommand cmdadd = new SqlCommand(ABCD, con);

                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdadd.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                }
                string a6 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                         " ,DateTimeOfTransaction,compid,whid,DiscEarn,Memo)" +
                         " VALUES('8000','" + invtranamt + "'" +
                         " ,'" + Id1 + "','" + txtdateto.Text + "','" + HttpContext.Current.Session["Comid"] + "','" + ddlstorename.SelectedValue + "','','All the overhead expenses allocated to various work order/ project by OH allocation no. " + maxid + " dated " + txtdateto.Text + "')";
                SqlCommand cmdtrd = new SqlCommand(a6, con);


                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdtrd.ExecuteNonQuery();
            }


            foreach (GridViewRow gdr12 in grdaccount.Rows)
            {
                Label lblaccountid = (Label)gdr12.FindControl("lblaccountid");
                Label lblaccountmasterid = (Label)gdr12.FindControl("lblaccountmasterid");

                Label lblamount = (Label)gdr12.FindControl("lblamount");
                DropDownList ddlallocation = (DropDownList)gdr12.FindControl("ddlallocation");

                CheckBox chk = (CheckBox)(gdr12.FindControl("chkinvMasterStatus"));
                TextBox txtamountallocate = (TextBox)gdr12.FindControl("txtamountallocate");



                string stroverdetail = "Insert Into OverHeadAllocationAccountOverHeadDetail (OverHeadMasterId,AccountMasterId,AmountApplied,AllocationMethod,AmountForPeriod,Active,TransactionId)values('" + maxid + "','" + lblaccountmasterid.Text + "','" + txtamountallocate.Text + "','" + ddlallocation.SelectedValue + "','" + lblamount.Text + "','" + chk.Checked + "','" + Id1 + "')";
                SqlCommand cmdoverdetail = new SqlCommand(stroverdetail, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdoverdetail.ExecuteNonQuery();
                con.Close();
                if (chk.Checked == true)
                {
                    string a61 = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                           " ,DateTimeOfTransaction,compid,whid,DiscEarn,Memo)" +
                           " VALUES('" + lblaccountid.Text + "','" + Convert.ToDecimal(txtamountallocate.Text) + "'" +
                           " ,'" + Id1 + "','" + txtdateto.Text + "','" + HttpContext.Current.Session["Comid"] + "','" + ddlstorename.SelectedValue + "','','All the overhead expenses allocated to various work order/ project by OH allocation no. " + maxid + " dated " + txtdateto.Text + "')";

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }

                    SqlCommand cd41 = new SqlCommand(a61, con);

                    cd41.ExecuteNonQuery();
                    con.Close();
                }
            }

            FillOverHead();
            Button8_Click(sender, e);
            lblmsg.Visible = true;
            lblmsg.Text = "Overhead amounts successfully applied to work orders (projects).";
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please change the period, you have already allocated overhead for this period.";
        }
    }

    protected void FillOverHead()
    {
        string overall = "";

        lblBusiness.Text = "Business : " + ddlwarehouse.SelectedItem.Text;

        if (ddlwarehouse.SelectedIndex > 0)
        {
            overall = " and Whid='" + ddlwarehouse.SelectedValue + "'";
        }
        if (txtsdate.Text != "" && txtedate.Text != "")
        {
            overall = overall + " and (StartDate>='" + txtsdate.Text + "' and EndDate<='" + txtedate.Text + "') ";
            lblddd.Text = "From Date : " + txtsdate.Text + " To Date : " + txtedate.Text;
        }
        string str = " OverHeadAllocationMaster.* from OverHeadAllocationMaster WHERE compid='" + Session["Comid"].ToString() + "'" + overall;

        string str2 = " select count(OverHeadAllocationMaster.Id) as ci from OverHeadAllocationMaster WHERE compid='" + Session["Comid"].ToString() + "'" + overall;

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " Id desc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable ds = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str);

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        //str = str + "Order by Id Desc";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);      
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
    //    SqlCommand cmd = new SqlCommand(qu, conn);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    return dt;
    //}


    private string PeriodDiff()
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


        string thisweekstart = weekstart.ToString();
        //ViewState["SDate"] = thisweekstart;
        string thisweekend = weekend.ToString();

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
        string lastweekstartdate = lastweekstart.ToString();
        //ViewState["SDate"] = lastweekstartdate;
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
        //ViewState["SDate"] = thismonthstartdate;

        DateTime lastdate = new DateTime(thismonthstart.Year, thismonthstart.Month, 1).AddMonths(1).AddDays(-1);
        string thismonthenddate = lastdate.ToShortDateString();
        //------------------this month period end................



        //-----------------last month period start ---------------

        string lastmonthNumber = "12";
        int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        if (lastmonthno.ToString() == "0")
        {

        }
        else
        {
            lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        }

        //int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        //string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
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
        //ViewState["SDate"] = lastmonthstartdate;
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
        // int thisqtr = Convert.ToInt32(thismonthstart.AddMonths(-2).Month.ToString());
        // string thisqtrNumber = Convert.ToString(DateTime.Now.Month.ToString());

        DateTime thisquater = Convert.ToDateTime(thisqtr.ToString() + "/1/" + ThisYear.ToString());
        string thisquaterstart = thisquater.ToShortDateString();
        // string thisqtrNumber = Convert.ToString(thisqtr + 3);
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
        // ViewState["SDate"] = thisquaterstartdate;
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
        //ViewState["SDate"] = lastquaterstartdate;
        string lastquaterenddate = lastquaterend.ToString();

        //--------------last quater period end-------------------------

        //--------------this year period start----------------------
        DateTime thisyearstart = Convert.ToDateTime("1/1/" + ThisYear.ToString());
        DateTime thisyearend = Convert.ToDateTime("12/31/" + ThisYear.ToString());

        string thisyearstartdate = thisyearstart.ToShortDateString();
        //ViewState["SDate"] = thisyearstartdate;
        string thisyearenddate = thisyearend.ToShortDateString();
        DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

        if (dt.Rows.Count > 0)
        {
            thisyearstartdate = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToShortDateString();
            thisyearenddate = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToShortDateString();
        }
        //---------------this year period end-------------------
        //--------------this year period start----------------------
        //DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        //DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());

        lastyearstart = Convert.ToDateTime(thisyearstartdate).AddYears(-1);
        lastyearend = Convert.ToDateTime(thisyearenddate).AddYears(-1);



        string lastyearstartdate = lastyearstart.ToShortDateString();
        // ViewState["SDate"] = lastyearstartdate;
        string lastyearenddate = lastyearend.ToShortDateString();



        //---------------this year period end-------------------


        string periodstartdate = "";
        string periodenddate = "";

        if (ddlperiod.SelectedItem.Text == "Today")
        {
            periodstartdate = Today.ToString();

            //DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlstorename.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {

                    // ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

                }


                else
                {
                    periodstartdate = Today.ToString();
                    periodenddate = Today.ToString();
                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "Yesterday")
        {
            periodstartdate = Yesterday.ToString();
            //DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {

                    // ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Yesterday.ToString();

                }


                else
                {
                    periodstartdate = Yesterday.ToString();
                    periodenddate = Yesterday.ToString();
                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "Current Week")
        {
            periodstartdate = thisweekstart.ToString();
            //DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    // lblstartdate.Text = dt.Rows[0][0].ToString();

                    // ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = thisweekend.ToString();

                }


                else
                {
                    periodstartdate = thisweekstart.ToString();
                    periodenddate = thisweekend.ToString();
                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "Last Week")
        {

            periodstartdate = lastweekstartdate.ToString();
            //DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {


                    // ModalPopupExtender1.Show();


                    periodenddate = lastweekenddate;

                }


                else
                {
                    periodstartdate = lastweekstartdate.ToString();
                    //periodenddate = Today.ToString();
                    periodenddate = lastweekenddate;
                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "Current Month")
        {

            periodstartdate = thismonthstart.ToString();
            //DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {


                    // ModalPopupExtender1.Show();

                    periodenddate = thismonthenddate.ToString();
                }


                else
                {
                    periodstartdate = thismonthstart.ToString();
                    periodenddate = thismonthenddate.ToString();
                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "Last Month")
        {

            periodstartdate = lastmonthstartdate.ToString();
            //DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {


                    // ModalPopupExtender1.Show();

                    periodenddate = lastmonthenddate.ToString();

                }


                else
                {
                    periodstartdate = lastmonthstartdate.ToString();
                    periodenddate = lastmonthenddate.ToString();
                }
            }


        }
        else if (ddlperiod.SelectedItem.Text == "Current Quarter")
        {

            periodstartdate = thisquaterstartdate.ToString();
            //DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {

                    // ModalPopupExtender1.Show();

                    periodenddate = thisquaterenddate.ToString();
                }


                else
                {
                    periodstartdate = thisquaterstartdate.ToString();
                    periodenddate = thisquaterenddate.ToString();
                }
            }


        }
        else if (ddlperiod.SelectedItem.Text == "Last Quarter")
        {

            periodstartdate = lastquaterstartdate.ToString();
            //DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    // lblstartdate.Text = dt.Rows[0][0].ToString();

                    // ModalPopupExtender1.Show();

                    periodenddate = lastquaterenddate.ToString();

                }


                else
                {
                    periodstartdate = lastquaterstartdate.ToString();
                    periodenddate = lastquaterenddate.ToString();
                }
            }


        }

        else if (ddlperiod.SelectedItem.Text == "Current Year")
        {

            periodstartdate = thisyearstartdate.ToString();
            //DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {


                    // ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = thisyearenddate.ToString();
                }


                else
                {
                    periodstartdate = thisyearstartdate.ToString();
                    periodenddate = thisyearenddate.ToString();
                }
            }


        }
        else if (ddlperiod.SelectedItem.Text == "Last Year")
        {

            periodstartdate = lastyearstartdate.ToString();
            //DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {


                    // ModalPopupExtender1.Show();

                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = lastyearenddate.ToString();

                }


                else
                {
                    periodstartdate = lastyearstartdate.ToString();
                    periodenddate = lastyearenddate.ToString();
                }
            }




        }
        else if (ddlperiod.SelectedItem.Text == "All")
        {
            periodstartdate = lastyearstartdate.ToString();

            //DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {

                    // ModalPopupExtender1.Show();

                    periodenddate = lastyearstartdate.ToString();
                }

                else
                {
                    periodstartdate = dt.Rows[0]["StartDate"].ToString();
                    periodenddate = dt.Rows[0]["EndDate"].ToString();
                }
            }

        }



        DateTime sd = Convert.ToDateTime(periodstartdate.ToString());
        DateTime ed = Convert.ToDateTime(periodenddate.ToString());


        //string str = "  TranctionMaster.Date between '" + sd.ToShortDateString() + "' AND '" + ed.ToShortDateString() + "'"; // + //2009-4-30' " +
        string str = "  TranctionMaster.Date between '" + sd.ToShortDateString() + "' AND '" + ed.ToShortDateString() + "' "; // + //2009-4-30' " +
        ViewState["startdateOfOpeningBalance"] = sd.ToShortDateString();
        ViewState["SDate"] = sd.ToShortDateString();
        ViewState["EDate"] = ed.ToShortDateString();

        return str;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "vi")
        {
            int dk = Convert.ToInt32(e.CommandArgument);
            ViewState["OverId"] = dk.ToString();
            lbladitio.Text = "Add a new overhead allocation";
            addinventoryroom.Visible = true;
            Panel2.Visible = true;
            Button6.Visible = true;
            //Button7.Visible = true;
            lblceditms.Text = "";
            Panel3.Visible = false;
            Panel4.Visible = false;
            pnladdval.Visible = false;
            string str123 = "select distinct  OverHeadAllocationAccountOverHeadDetail.Id as OId,OverHeadAllocationAccountOverHeadDetail.AmountForPeriod,OverHeadAllocationAccountOverHeadDetail.AmountApplied,OverHeadAllocationAccountOverHeadDetail.Active,AccountMaster.Id  ,GroupCompanyMaster.groupid,GroupCompanyMaster.groupdisplayname,AccountMaster.AccountName as AccountName,AccountMaster.AccountId   from OverHeadAllocationAccountOverHeadDetail inner join AccountMaster on AccountMaster.Id=OverHeadAllocationAccountOverHeadDetail.AccountMasterId inner join GroupCompanyMaster on  GroupCompanyMaster.groupid=AccountMaster.GroupId where OverHeadAllocationAccountOverHeadDetail.OverHeadMasterId='" + ViewState["OverId"] + "'  ";
            SqlCommand cmd123 = new SqlCommand(str123, con);
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
            DataTable dt123 = new DataTable();
            adp123.Fill(dt123);

            GridView2.DataSource = dt123;
            GridView2.DataBind();
            foreach (GridViewRow gdr in GridView2.Rows)
            {



                DropDownList ddlallocation123 = (DropDownList)gdr.FindControl("ddlallocation123");
                Label lbloid123 = (Label)gdr.FindControl("lbloid123");

                Label lblamount123 = (Label)gdr.FindControl("lblamount123");
                lblamount123.Text = Math.Round(Convert.ToDecimal(lblamount123.Text), 2).ToString("###,###.##");
                if (lblamount123.Text.Length <= 0)
                {
                    lblamount123.Text = "0.00";
                }
                else
                {
                    lblamount123.Text = String.Format("{0:n}", Convert.ToDecimal(lblamount123.Text));
                }
                string strtemp1 = "select * from OverHeadAllocationAccountOverHeadDetail where Id='" + lbloid123.Text + "'";
                SqlCommand cmdtemp1 = new SqlCommand(strtemp1, con);
                SqlDataAdapter adptemp1 = new SqlDataAdapter(cmdtemp1);
                DataTable dttemp1 = new DataTable();
                adptemp1.Fill(dttemp1);
                ViewState["tid"] = Convert.ToString(dttemp1.Rows[0]["Transactionid"]);
                string strmethod = "select * from AllocationMethod";
                SqlCommand cmdallocate = new SqlCommand(strmethod, con);
                SqlDataAdapter adpallocate = new SqlDataAdapter(cmdallocate);
                DataTable dtallocate = new DataTable();
                adpallocate.Fill(dtallocate);

                ddlallocation123.DataSource = dtallocate;
                ddlallocation123.DataTextField = "Name";
                ddlallocation123.DataValueField = "Id";
                ddlallocation123.DataBind();
                ddlallocation123.Items.Insert(0, "Select");
                ddlallocation123.Items[0].Value = "0";
                ddlallocation123.SelectedIndex = ddlallocation123.Items.IndexOf(ddlallocation123.Items.FindByValue(dttemp1.Rows[0]["AllocationMethod"].ToString()));
            }


            string st178 = "select OverHeadAllocationJobDetail.*,JobMaster.JobNumber,JobMaster.InvWMasterId,JobMaster.JobName,JobMaster.JobStartDate,JobMaster.JobEndDate from  OverHeadAllocationJobDetail inner join JobMaster  on JobMaster.Id=OverHeadAllocationJobDetail.JobMasterId  where OverHeadAllocationJobDetail.OverHeadMasterId='" + ViewState["OverId"] + "'";
            SqlCommand cmd178 = new SqlCommand(st178, con);
            SqlDataAdapter adp178 = new SqlDataAdapter(cmd178);
            DataTable dt178 = new DataTable();
            adp178.Fill(dt178);

            GridView3.DataSource = dt178;
            GridView3.DataBind();

            Seccolin(CheckBoxList2, GridView3);
            double finaltotal = 0;
            foreach (GridViewRow gdr in GridView3.Rows)
            {
                Label lbldirectmaterial123 = (Label)gdr.FindControl("lbldirectmaterial123");
                lbldirectmaterial123.Text = Math.Round(Convert.ToDecimal(lbldirectmaterial123.Text), 2).ToString("###,###.##");
                if (lbldirectmaterial123.Text.Length <= 0)
                {
                    lbldirectmaterial123.Text = "0.00";
                }
                else
                {
                    lbldirectmaterial123.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectmaterial123.Text));
                }
                Label lbldirectmaterialperiod123 = (Label)gdr.FindControl("lbldirectmaterialperiod123");
                lbldirectmaterialperiod123.Text = Math.Round(Convert.ToDecimal(lbldirectmaterialperiod123.Text), 2).ToString("###,###.##");
                if (lbldirectmaterialperiod123.Text.Length <= 0)
                {
                    lbldirectmaterialperiod123.Text = "0.00";
                }
                else
                {
                    lbldirectmaterialperiod123.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectmaterialperiod123.Text));
                }
                Label lbldirectlabour123 = (Label)gdr.FindControl("lbldirectlabour123");
                lbldirectlabour123.Text = Math.Round(Convert.ToDecimal(lbldirectlabour123.Text), 2).ToString("###,###.##");
                if (lbldirectlabour123.Text.Length <= 0)
                {
                    lbldirectlabour123.Text = "0.00";
                }
                else
                {
                    lbldirectlabour123.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectlabour123.Text));
                }
                Label lbldirectlabourperiod123 = (Label)gdr.FindControl("lbldirectlabourperiod123");
                lbldirectlabourperiod123.Text = Math.Round(Convert.ToDecimal(lbldirectlabourperiod123.Text), 2).ToString("###,###.##");
                if (lbldirectlabourperiod123.Text.Length <= 0)
                {
                    lbldirectlabourperiod123.Text = "0.00";
                }
                else
                {
                    lbldirectlabourperiod123.Text = String.Format("{0:n}", Convert.ToDecimal(lbldirectlabourperiod123.Text));
                }
                Label lblnoofdaysperiod123 = (Label)gdr.FindControl("lblnoofdaysperiod123");
                lblnoofdaysperiod123.Text = Math.Round(Convert.ToDecimal(lblnoofdaysperiod123.Text), 2).ToString("###,###.##");
                if (lblnoofdaysperiod123.Text.Length <= 0)
                {
                    lblnoofdaysperiod123.Text = "0.00";
                }
                else
                {
                    lblnoofdaysperiod123.Text = String.Format("{0:n}", Convert.ToDecimal(lblnoofdaysperiod123.Text));
                }
                TextBox txtohbymaterial123 = (TextBox)gdr.FindControl("txtohbymaterial123");
                txtohbymaterial123.Text = Math.Round(Convert.ToDecimal(txtohbymaterial123.Text), 2).ToString("###,###.##");
                if (txtohbymaterial123.Text.Length <= 0)
                {
                    txtohbymaterial123.Text = "0.00";
                }
                else
                {
                    txtohbymaterial123.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbymaterial123.Text));
                }
                TextBox txtohbylabour123 = (TextBox)gdr.FindControl("txtohbylabour123");
                txtohbylabour123.Text = Math.Round(Convert.ToDecimal(txtohbylabour123.Text), 2).ToString("###,###.##");
                if (txtohbylabour123.Text.Length <= 0)
                {
                    txtohbylabour123.Text = "0.00";
                }
                else
                {
                    txtohbylabour123.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbylabour123.Text));
                }
                TextBox txtohbydays123 = (TextBox)gdr.FindControl("txtohbydays123");
                txtohbydays123.Text = Math.Round(Convert.ToDecimal(txtohbydays123.Text), 2).ToString("###,###.##");
                if (txtohbydays123.Text.Length <= 0)
                {
                    txtohbydays123.Text = "0.00";
                }
                else
                {
                    txtohbydays123.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbydays123.Text));
                }
                TextBox txtohbyequal123 = (TextBox)gdr.FindControl("txtohbyequal123");
                txtohbyequal123.Text = Math.Round(Convert.ToDecimal(txtohbyequal123.Text), 2).ToString("###,###.##");
                if (txtohbyequal123.Text.Length <= 0)
                {
                    txtohbyequal123.Text = "0.00";
                }
                else
                {
                    txtohbyequal123.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbyequal123.Text));
                }
                TextBox txtfinaltotal123 = (TextBox)gdr.FindControl("txtfinaltotal123");
                txtfinaltotal123.Text = Math.Round(Convert.ToDecimal(txtfinaltotal123.Text), 2).ToString("###,###.##");
                if (txtfinaltotal123.Text.Length <= 0)
                {
                    txtfinaltotal123.Text = "0.00";
                }
                else
                {
                    txtfinaltotal123.Text = String.Format("{0:n}", Convert.ToDecimal(txtfinaltotal123.Text));
                }
                finaltotal += Convert.ToDouble(txtfinaltotal123.Text);
            }
            if (GridView3.Rows.Count > 0)
            {
                GridViewRow ft = (GridViewRow)(GridView3.FooterRow);
                Label footertotal = (Label)(ft.FindControl("footertotal"));
                footertotal.Text = Math.Round(finaltotal, 2).ToString("###,###.##");

                if (footertotal.Text.Length <= 0)
                {
                    footertotal.Text = "0.00";
                }
                else
                {
                    footertotal.Text = String.Format("{0:n}", Convert.ToDecimal(footertotal.Text));
                }
            }
            DataTable dta = select("Select * from OverHeadAllocationMaster where Id='" + ViewState["OverId"] + "'");
            if (dta.Rows.Count > 0)
            {
                txtdatefrom.Text = Convert.ToDateTime(dta.Rows[0]["StartDate"]).ToShortDateString();
                txtdateto.Text = Convert.ToDateTime(dta.Rows[0]["EndDate"]).ToShortDateString();
                //RadioButtonList1.SelectedIndex = 0;
                //RadioButtonList1_SelectedIndexChanged(sender, e);
                txtoverheadname.Text = Convert.ToString(dta.Rows[0]["Name"]);
            }
        }

        if (e.CommandName == "Delete")
        {
            int m = Convert.ToInt32(e.CommandArgument);


            SqlCommand cmd11 = new SqlCommand("delete from OverHeadAllocationMaster where Id=" + m, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd11.ExecuteNonQuery();
            con.Close();

            SqlCommand cmd12 = new SqlCommand("delete from OverHeadAllocationJobDetail where OverHeadMasterId=" + m, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd12.ExecuteNonQuery();
            con.Close();
            DataTable drs = select("select * from OverHeadAllocationAccountOverHeadDetail where  OverHeadMasterId=" + m);
            if (drs.Rows.Count > 0)
            {
                if (Convert.ToString(drs.Rows[0]["TransactionId"]) != "")
                {
                    SqlCommand cmdTran = new SqlCommand("delete from TranctionMaster where Tranction_Master_Id='" + drs.Rows[0]["TransactionId"] + "'", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdTran.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmdTranDe = new SqlCommand("delete from Tranction_Details where Tranction_Master_Id='" + drs.Rows[0]["TransactionId"] + "'", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdTranDe.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmdInvAvg = new SqlCommand("delete from InventoryWarehouseMasterAvgCostTbl where Tranction_Master_Id='" + drs.Rows[0]["TransactionId"] + "'", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdInvAvg.ExecuteNonQuery();
                    con.Close();
                }

            }
            SqlCommand cmd13 = new SqlCommand("delete from OverHeadAllocationAccountOverHeadDetail where OverHeadMasterId=" + m, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd13.ExecuteNonQuery();
            con.Close();

            FillOverHead();

            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";

        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        double Count = 0;
        string str = "select count(Id) as Id from JobMaster WHERE Whid='" + ddlstorename.SelectedValue + "' and JobStartDate>='" + txtdatefrom.Text + "' and JobEndDate<='" + txtdateto.Text + "' ";

        //string str = "select count(Id) as Id from JobMaster where compid='" + Session["Comid"].ToString() + "' and Whid='" + ddlstorename.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            Count = Convert.ToDouble(ds.Rows[0]["Id"].ToString());
        }


        string materialin = "";
        double Qty = 0;
        double Rate = 0;
        double TotalCost = 0;
        double FinalCost = 0;

        double Qty1 = 0;
        double Rate1 = 0;
        double TotalCost1 = 0;
        double FinalCost1 = 0;

        double LabourCost = 0;
        double FinalLabourCost = 0;
        double LabourCost1 = 0;
        double FinalLabourCost1 = 0;


        double TotalMaterialOfPeriod = 0;
        double TotalLabourOfPeriod = 0;


        double TotalDays1 = 0;
        double TotalDays = 0;


        foreach (GridViewRow gdr in GridView3.Rows)
        {
            Label lbljobmasterid = (Label)gdr.FindControl("lbljobmasterid123");

            Label lblinvmasterid = (Label)gdr.FindControl("lblinvmasterid123");

            CheckBox chkjobmaster = (CheckBox)(gdr.FindControl("chkjobmaster123"));

            Label lbldirectmaterial = (Label)gdr.FindControl("lbldirectmaterial123");
            Label lbldirectmaterialperiod = (Label)gdr.FindControl("lbldirectmaterialperiod123");
            Label lbldirectlabourperiod = (Label)gdr.FindControl("lbldirectlabourperiod123");
            Label lbldirectlabour = (Label)gdr.FindControl("lbldirectlabour123");
            Label lblnoofdaysperiod = (Label)gdr.FindControl("lblnoofdaysperiod123");


            FinalCost1 = Convert.ToDouble(lbldirectmaterialperiod.Text);
            FinalLabourCost1 = Convert.ToDouble(lbldirectlabour.Text);
            FinalLabourCost = Convert.ToDouble(lbldirectlabourperiod.Text);
            TotalDays1 = Convert.ToDouble(lblnoofdaysperiod.Text);

            TotalDays += TotalDays1;

            TotalMaterialOfPeriod += FinalCost1;

            TotalLabourOfPeriod += FinalLabourCost;
        }

        double finaltotal = 0;
        foreach (GridViewRow gdr in GridView3.Rows)
        {

            Label lbljobmasterid = (Label)gdr.FindControl("lbljobmasterid123");

            Label lblinvmasterid = (Label)gdr.FindControl("lblinvmasterid123");

            CheckBox chkjobmaster = (CheckBox)(gdr.FindControl("chkjobmaster123"));

            Label lbldirectmaterial = (Label)gdr.FindControl("lbldirectmaterial123");
            Label lbldirectmaterialperiod = (Label)gdr.FindControl("lbldirectmaterialperiod123");
            Label lbldirectlabourperiod = (Label)gdr.FindControl("lbldirectlabourperiod123");
            Label lbldirectlabour = (Label)gdr.FindControl("lbldirectlabour123");

            Label lblnoofdaysperiod = (Label)gdr.FindControl("lblnoofdaysperiod123");

            TextBox txtohbymaterial = (TextBox)gdr.FindControl("txtohbymaterial123");

            TextBox txtohbylabour = (TextBox)gdr.FindControl("txtohbylabour123");

            TextBox txtohbydays = (TextBox)gdr.FindControl("txtohbydays123");

            TextBox txtohbyequal = (TextBox)gdr.FindControl("txtohbyequal123");
            TextBox txtfinaltotal = (TextBox)gdr.FindControl("txtfinaltotal123");


            double Final1M = 0;
            double Final1l = 0;
            double Final1d = 0;
            double Final1e = 0;

            foreach (GridViewRow gdr12 in GridView2.Rows)
            {
                Label lblaccountid = (Label)gdr12.FindControl("lblaccountid123");

                Label lblamount = (Label)gdr12.FindControl("lblamount123");
                DropDownList ddlallocation = (DropDownList)gdr12.FindControl("ddlallocation123");

                CheckBox chk = (CheckBox)(gdr12.FindControl("chkinvMasterStatus123"));
                TextBox txtamountallocate = (TextBox)gdr12.FindControl("txtamountallocate123");

                //Material method
                if (ddlallocation.SelectedIndex == 1 && chkjobmaster.Checked == true)
                {
                    double totalOver1M = Convert.ToDouble(txtamountallocate.Text);
                    Final1M += totalOver1M;
                    Final1M = Math.Round(Final1M, 2);


                }
                //Labour method
                if (ddlallocation.SelectedIndex == 2 && chkjobmaster.Checked == true)
                {
                    double totalOver1l = Convert.ToDouble(txtamountallocate.Text);
                    Final1l += totalOver1l;
                    Final1l = Math.Round(Final1l, 2);






                }
                //Days method
                if (ddlallocation.SelectedIndex == 3 && chkjobmaster.Checked == true)
                {
                    double totalOver1d = Convert.ToDouble(txtamountallocate.Text);
                    Final1d += totalOver1d;
                    Final1d = Math.Round(Final1d, 2);





                }
                //equal method
                if (ddlallocation.SelectedIndex == 4 && chkjobmaster.Checked == true)
                {
                    double totalOver1e = Convert.ToDouble(txtamountallocate.Text);
                    Final1e += totalOver1e;
                    Final1e = Math.Round(Final1e, 2);





                }


            }
            double Fin1 = 0;
            double Fin2 = 0;
            double Fin3 = 0;
            double Fin4 = 0;
            double Alltotal = 0;
            Fin1 = (Convert.ToDouble(lbldirectmaterialperiod.Text) / TotalMaterialOfPeriod) * Final1M;
            Fin2 = (Convert.ToDouble(lbldirectlabourperiod.Text) / TotalLabourOfPeriod) * Final1l;
            Fin3 = (Convert.ToDouble(lblnoofdaysperiod.Text) / TotalDays) * Final1d;
            Fin4 = Final1e / Count;
            if (Convert.ToString(Fin1) == "NaN")
            {
                Fin1 = 0;
            }
            if (Convert.ToString(Fin2) == "NaN")
            {
                Fin2 = 0;
            }
            if (Convert.ToString(Fin3) == "NaN")
            {
                Fin3 = 0;
            }
            if (Convert.ToString(Fin4) == "NaN")
            {
                Fin4 = 0;
            }
            txtohbymaterial.Text = Math.Round(Fin1, 2).ToString("###,###.##");
            if (txtohbymaterial.Text.Length <= 0)
            {
                txtohbymaterial.Text = "0.00";
            }
            else
            {
                txtohbymaterial.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbymaterial.Text));
            }
            txtohbylabour.Text = Math.Round(Fin2, 2).ToString("###,###.##");
            if (txtohbylabour.Text.Length <= 0)
            {
                txtohbylabour.Text = "0.00";
            }
            else
            {
                txtohbylabour.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbylabour.Text));
            }
            txtohbydays.Text = Math.Round(Fin3, 2).ToString("###,###.##");
            if (txtohbydays.Text.Length <= 0)
            {
                txtohbydays.Text = "0.00";
            }
            else
            {
                txtohbydays.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbydays.Text));
            }
            txtohbyequal.Text = Math.Round(Fin4, 2).ToString("###,###.##");
            if (txtohbyequal.Text.Length <= 0)
            {
                txtohbyequal.Text = "0.00";
            }
            else
            {
                txtohbyequal.Text = String.Format("{0:n}", Convert.ToDecimal(txtohbyequal.Text));
            }
            Alltotal = Fin1 + Fin2 + Fin3 + Fin4;
            txtfinaltotal.Text = Math.Round(Alltotal, 2).ToString("###,###.##");

            if (txtfinaltotal.Text.Length <= 0)
            {
                txtfinaltotal.Text = "0.00";
            }
            else
            {
                txtfinaltotal.Text = String.Format("{0:n}", Convert.ToDecimal(txtfinaltotal.Text));
            }

            finaltotal += Convert.ToDouble(txtfinaltotal.Text);

        }
        if (GridView3.Rows.Count > 0)
        {
            GridViewRow ft = (GridViewRow)(GridView3.FooterRow);
            Label footertotal = (Label)(ft.FindControl("footertotal"));
            footertotal.Text = Math.Round(finaltotal, 2).ToString("###,###.##");

            if (footertotal.Text.Length <= 0)
            {
                footertotal.Text = "0.00";
            }
            else
            {
                footertotal.Text = String.Format("{0:n}", Convert.ToDecimal(footertotal.Text));
            }
        }
        Button7.Visible = true;
        lblceditms.Text = "Please review the calculated allocation amount, and select update if overhead amount is correct and you would like to apply overheads to work orders (projects).";


    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        DataTable dtr = select("select Id,StartDate,EndDate from OverHeadAllocationMaster where ((StartDate between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "') or (EndDate between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "')) and   Whid='" + ddlstorename.SelectedValue + "' and Id<>'" + ViewState["OverId"] + "' ");
        if (dtr.Rows.Count == 0)
        {
            double total = 0;
            if (GridView3.Rows.Count > 0)
            {
                string insertdailyw = " delete from OverHeadAllocationJobDetail where OverHeadMasterId='" + ViewState["OverId"] + "'";
                SqlCommand cmddailyw = new SqlCommand(insertdailyw, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddailyw.ExecuteNonQuery();
                con.Close();
                string insertdaily1 = " delete from OverHeadAllocationAccountOverHeadDetail where OverHeadMasterId='" + ViewState["OverId"] + "'";
                SqlCommand cmddaily1 = new SqlCommand(insertdaily1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddaily1.ExecuteNonQuery();
                con.Close();
                if (Convert.ToString(ViewState["tid"]) != "")
                {
                    SqlCommand cmdTranDe = new SqlCommand("delete from Tranction_Details where Tranction_Master_Id='" + ViewState["tid"] + "'", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdTranDe.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmdInvAvg = new SqlCommand("delete from InventoryWarehouseMasterAvgCostTbl where Tranction_Master_Id='" + ViewState["tid"] + "'", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdInvAvg.ExecuteNonQuery();
                    con.Close();
                }
                decimal invtranamt = 0;
                foreach (GridViewRow gdr in GridView3.Rows)
                {

                    Label lbljobmasterid = (Label)gdr.FindControl("JobMasterId123");

                    Label lblinvmasterid = (Label)gdr.FindControl("lblinvmasterid123");

                    CheckBox chkjobmaster = (CheckBox)(gdr.FindControl("chkjobmaster123"));

                    Label lbldirectmaterial = (Label)gdr.FindControl("lbldirectmaterial123");
                    Label lbldirectlabour = (Label)gdr.FindControl("lbldirectlabour123");
                    Label lblnoofdays = (Label)gdr.FindControl("lblnoofdays123");

                    Label lbldirectmaterialperiod = (Label)gdr.FindControl("lbldirectmaterialperiod123");
                    Label lbldirectlabourperiod = (Label)gdr.FindControl("lbldirectlabourperiod123");
                    Label lblnoofdaysperiod = (Label)gdr.FindControl("lblnoofdaysperiod123");




                    TextBox txtohbymaterial = (TextBox)gdr.FindControl("txtohbymaterial123");

                    TextBox txtohbylabour = (TextBox)gdr.FindControl("txtohbylabour123");

                    TextBox txtohbydays = (TextBox)gdr.FindControl("txtohbydays123");

                    TextBox txtohbyequal = (TextBox)gdr.FindControl("txtohbyequal123");

                    TextBox txtfinaltotal = (TextBox)gdr.FindControl("txtfinaltotal123");
                    if (chkjobmaster.Checked == true)
                    {
                        string StrInsert = "Insert Into OverHeadAllocationJobDetail " +
                         "   (OverHeadMasterId,JobMasterId,DirectMaterialCost,DirectLabourCost,DirectMaterialCostOfPeriod,DirectLabourCostOfPeriod,NoOfDaysCostForPeriod,OhByMaterial,OhByLabour,OhByDays,Ohbyequal,OhAllocationtotal,Active)values " +
                         "  ('" + ViewState["OverId"] + "','" + lbljobmasterid.Text + "','" + lbldirectmaterial.Text + "','" + lbldirectlabour.Text + "','" + lbldirectmaterialperiod.Text + "','" + lbldirectlabourperiod.Text + "','" + lblnoofdaysperiod.Text + "','" + txtohbymaterial.Text + "','" + txtohbylabour.Text + "','" + txtohbydays.Text + "','" + txtohbyequal.Text + "','" + txtfinaltotal.Text + "','" + chkjobmaster.Checked + "') ";
                        SqlCommand cmdinsert = new SqlCommand(StrInsert, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdinsert.ExecuteNonQuery();
                        con.Close();
                        decimal OLDavgcost = 0;
                        decimal oLDqtyONHAND = 0;
                        DataTable Dataacces = select("Select InvWMasterId  from JobMaster where Id='" + lbljobmasterid.Text + "'");

                        if (Dataacces.Rows.Count > 0)
                        {
                            DataTable dt123 = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl " +
                            "  inner join MaterialIssueMasterTbl on MaterialIssueMasterTbl.Id=InventoryWarehouseMasterAvgCostTbl.MaterialIssueMasterTblId where InvWMasterId='" + Dataacces.Rows[0]["InvWMasterId"] + "' and DateUpdated<='" + txtdateto.Text + "' and MaterialIssueMasterTbl.JobMasterId='" + lbljobmasterid.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc");

                            if (dt123.Rows.Count > 0)
                            {

                                if (Convert.ToString(dt123.Rows[0]["AvgCost"]) != "")
                                {
                                    OLDavgcost = Convert.ToDecimal(dt123.Rows[0]["AvgCost"]);
                                }
                                if (Convert.ToString(dt123.Rows[0]["QtyonHand"]) != "")
                                {
                                    oLDqtyONHAND = Convert.ToDecimal(dt123.Rows[0]["QtyonHand"]);
                                }
                            }
                            if (oLDqtyONHAND == 0)
                            {
                                oLDqtyONHAND = 1;
                            }
                            invtranamt += Convert.ToDecimal(txtfinaltotal.Text);
                            decimal avxc = (OLDavgcost * oLDqtyONHAND) + (Convert.ToDecimal(txtfinaltotal.Text)) / oLDqtyONHAND;

                            string ABCD = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,Rate,DateUpdated,AvgCost,QtyonHand)values('" + Dataacces.Rows[0]["InvWMasterId"] + "','" + ViewState["tid"] + "','" + oLDqtyONHAND + "','" + Math.Round(avxc, 2) + "','" + txtdateto.Text + "','" + Math.Round(avxc, 2) + "','" + oLDqtyONHAND + "')";
                            SqlCommand cmdadd = new SqlCommand(ABCD, con);

                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdadd.ExecuteNonQuery();
                            con.Close();
                        }

                        double Finaltxt = Convert.ToDouble(txtfinaltotal.Text);

                        total += Finaltxt;
                    }


                }
                string a6 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                         " ,DateTimeOfTransaction,compid,whid,DiscEarn,Memo)" +
                         " VALUES('8000','" + invtranamt + "'" +
                         " ,'" + ViewState["tid"] + "','" + txtdateto.Text + "','" + HttpContext.Current.Session["Comid"] + "','" + ddlstorename.SelectedValue + "','','All the overhead expenses allocated to various work order/ project by OH allocation no. " + ViewState["OverId"] + " dated " + txtdateto.Text + "')";
                SqlCommand cmdtrd = new SqlCommand(a6, con);


                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdtrd.ExecuteNonQuery();

            }


            foreach (GridViewRow gdr12 in GridView2.Rows)
            {
                Label lblaccountid = (Label)gdr12.FindControl("lblaccountid123");
                Label lblaccountmasterid = (Label)gdr12.FindControl("lblaccountmasterid123");

                Label lblamount = (Label)gdr12.FindControl("lblamount123");
                DropDownList ddlallocation = (DropDownList)gdr12.FindControl("ddlallocation123");

                CheckBox chk = (CheckBox)(gdr12.FindControl("chkinvMasterStatus123"));
                TextBox txtamountallocate = (TextBox)gdr12.FindControl("txtamountallocate123");



                string stroverdetail = "Insert Into OverHeadAllocationAccountOverHeadDetail (OverHeadMasterId,AccountMasterId,AmountApplied,AllocationMethod,AmountForPeriod,Active,TransactionId)values('" + ViewState["OverId"] + "','" + lblaccountmasterid.Text + "','" + txtamountallocate.Text + "','" + ddlallocation.SelectedValue + "','" + lblamount.Text + "','" + chk.Checked + "','" + ViewState["tid"] + "')";
                SqlCommand cmdoverdetail = new SqlCommand(stroverdetail, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdoverdetail.ExecuteNonQuery();
                con.Close();
                if (chk.Checked == true)
                {
                    string a61 = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                           " ,DateTimeOfTransaction,compid,whid,DiscEarn,Memo)" +
                           " VALUES('" + lblaccountid.Text + "','" + Convert.ToDecimal(txtamountallocate.Text) + "'" +
                           " ,'" + ViewState["tid"] + "','" + txtdateto.Text + "','" + HttpContext.Current.Session["Comid"] + "','" + ddlstorename.SelectedValue + "','','All the overhead expenses allocated to various work order/ project by OH allocation no. " + ViewState["OverId"] + " dated " + txtdateto.Text + "')";

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }

                    SqlCommand cd41 = new SqlCommand(a61, con);

                    cd41.ExecuteNonQuery();
                    con.Close();
                }
            }
            string insertdaily = "Update  OverHeadAllocationMaster  Set StartDate='" + txtdatefrom.Text + "',EndDate='" + txtdateto.Text + "',TotalAmountOverHead='" + total + "',Name='" + txtoverheadname.Text + "' where Id='" + ViewState["OverId"] + "'";
            SqlCommand cmddaily = new SqlCommand(insertdaily, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddaily.ExecuteNonQuery();
            con.Close();


            string str123 = "Update  TranctionMaster Set Date='" + txtdateto.Text + "',UserId='" + Session["userid"] + "',Tranction_Amount='" + total + "' where  Tranction_Master_Id='" + ViewState["tid"] + "'";

            SqlCommand cd3t = new SqlCommand(str123, con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cd3t.ExecuteNonQuery();
            con.Close();
            FillOverHead();

            Panel2.Visible = false;
            FillOverHead();
            Button8_Click(sender, e);
            lblmsg.Visible = true;
            lblmsg.Text = "Overhead amounts successfully applied to work orders (projects).";
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please change the period, you have already allocated overhead for this period.";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdaccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdaccount.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdjob_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btnovea_Click(object sender, EventArgs e)
    {
        lblcalms.Text = "";
        lblmsg.Text = "";



        lbladitio.Text = "Add a new overhead allocation";
        addinventoryroom.Visible = true;
        ddlstorename.Enabled = true;
        Panel2.Visible = false;
        filldefault();

    }
    protected void btng_Click(object sender, EventArgs e)
    {
        FillOverHead();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        addinventoryroom.Visible = false;
        btnovea.Visible = true;
        ddlstorename.Enabled = true;

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            FillOverHead();

            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            FillOverHead();

            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
        }
    }
    protected void btnselectcol_Click(object sender, EventArgs e)
    {
        if (pnladdcol.Visible == false)
        {
            pnladdcol.Visible = true;
            btnselectcol.Text = "Hide Columns";
        }
        else
        {
            pnladdcol.Visible = false;
            btnselectcol.Text = "Select Columns";
        }
    }
    protected void btnfillcol_Click(object sender, EventArgs e)
    {
        Seccolin(CheckBoxList1, grdjob);
    }
    protected void Seccolin(CheckBoxList chklist, GridView grdcol)
    {
        if (chklist.Items[0].Selected)
        {
            grdcol.Columns[0].Visible = true;
        }
        else
        {
            grdcol.Columns[0].Visible = false;
        }
        if (chklist.Items[1].Selected)
        {
            grdcol.Columns[1].Visible = true;
        }
        else
        {
            grdcol.Columns[1].Visible = false;
        }
        if (chklist.Items[2].Selected)
        {
            grdcol.Columns[2].Visible = true;
        }
        else
        {
            grdcol.Columns[2].Visible = false;
        }
        if (chklist.Items[3].Selected)
        {
            grdcol.Columns[3].Visible = true;
        }
        else
        {
            grdcol.Columns[3].Visible = false;
        }

        if (chklist.Items[4].Selected)
        {
            grdcol.Columns[4].Visible = true;
        }
        else
        {
            grdcol.Columns[4].Visible = false;
        }

        if (chklist.Items[5].Selected)
        {
            grdcol.Columns[5].Visible = true;
        }
        else
        {
            grdcol.Columns[5].Visible = false;
        }

        if (chklist.Items[6].Selected)
        {

            grdcol.Columns[6].Visible = true;
        }
        else
        {
            grdcol.Columns[6].Visible = false;
        }

        if (chklist.Items[7].Selected)
        {

            grdcol.Columns[7].Visible = true;
        }
        else
        {

            grdcol.Columns[7].Visible = false;
        }

        if (chklist.Items[8].Selected)
        {

            grdcol.Columns[8].Visible = true;
        }
        else
        {
            grdcol.Columns[8].Visible = false;
        }

        if (chklist.Items[9].Selected)
        {
            grdcol.Columns[10].Visible = true;
        }
        else
        {
            grdcol.Columns[10].Visible = false;
        }


        if (chklist.Items[10].Selected)
        {
            grdcol.Columns[11].Visible = true;
        }
        else
        {
            grdcol.Columns[11].Visible = false;
        }


        if (chklist.Items[11].Selected)
        {
            grdcol.Columns[12].Visible = true;
        }
        else
        {
            grdcol.Columns[12].Visible = false;
        }
        if (chklist.Items[12].Selected)
        {
            grdcol.Columns[13].Visible = true;
        }
        else
        {
            grdcol.Columns[13].Visible = false;
        }
        if (chklist.Items[13].Selected)
        {
            grdcol.Columns[14].Visible = true;
        }
        else
        {
            grdcol.Columns[14].Visible = false;
        }
    }
    protected void btneditsk_Click(object sender, EventArgs e)
    {
        if (pnleditcolshow.Visible == false)
        {
            pnleditcolshow.Visible = true;
            btneditsk.Text = "Hide Columns";
        }
        else
        {
            pnleditcolshow.Visible = false;
            btneditsk.Text = "Select Columns";
        }
    }
    protected void btrfilcoedit_Click(object sender, EventArgs e)
    {
        Seccolin(CheckBoxList2, GridView3);
    }
    protected void btnaccogroup_Click(object sender, EventArgs e)
    {
        if (chkgroup.Items.Count <= 0)
        {
            DataTable drts = select("SELECT GroupCompanyMaster.groupid,GroupCompanyMaster.groupdisplayname    FROM        ClassTypeCompanyMaster Inner join   " +
                     "   ClassCompanyMaster ON ClassTypeCompanyMaster.id = ClassCompanyMaster.classtypecompanymasterid Inner join   " +
                            "  GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid where GroupCompanyMaster.whid='" + ddlstorename.SelectedValue + "' and ClassTypeCompanyMaster.classtypeid='15' and GroupCompanyMaster.GroupId NoT IN('47','48') order by   GroupCompanyMaster.groupdisplayname asc");

            chkgroup.DataSource = drts;
            chkgroup.DataTextField = "groupdisplayname";
            chkgroup.DataValueField = "groupid";
            chkgroup.DataBind();
        }
        if (pnlgrop.Visible == true)
        {
            btnaccogroup.Text = "Select Accounting Groups to be Considered for Overhead Allocation.";
            pnlgrop.Visible = false;
            btningr.Visible = false;
        }
        else
        {
            btnaccogroup.Text = "Hide Accounting Groups to be Considered for Overhead Allocation.";

            pnlgrop.Visible = true;
            btningr.Visible = true;
        }

    }
    protected void btningr_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlstorename_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldefault();

    }
    protected void filldefault()
    {
        EventArgs e = new EventArgs();
        object sender = new object();
        ddlpriviallocat.Items.Clear();
        if (addinventoryroom.Visible == true)
        {
            DataTable dtr = select("select Id,StartDate,EndDate, Name, Convert(nvarchar,StartDate,101)+' - '+Convert(nvarchar,EndDate,101)+' - '+ Name as DDL from OverHeadAllocationMaster where Whid='" + ddlstorename.SelectedValue + "' order by Id Desc ");
            if (dtr.Rows.Count > 0)
            {
                chkb.Checked = true;
                txtdatefrom.Text = Convert.ToDateTime(dtr.Rows[0]["EndDate"]).AddDays(1).ToShortDateString();
                txtdateto.Text = DateTime.Now.ToShortDateString();
                txtoverheadname.Text = "Overhead allocation  for the period  - " + txtdatefrom.Text + " - " + txtdateto.Text;
                ddlpriviallocat.DataSource = dtr;
                ddlpriviallocat.DataTextField = "DDL";
                ddlpriviallocat.DataValueField = "Id";
                ddlpriviallocat.DataBind();
                CheckBox1.Checked = true;
                CheckBox1_CheckedChanged(sender, e);
                pnlc.Visible = true;
                //filldegroup();
            }
            else
            {
                CheckBox1.Checked = false;
                CheckBox1_CheckedChanged(sender, e);
                chkb.Checked = false;
                txtdatefrom.Text = DateTime.Now.ToShortDateString();
                txtdateto.Text = DateTime.Now.ToShortDateString();
                pnlc.Visible = false;
                //filldegroup();
            }
        }

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

        if (CheckBox1.Checked == true)
        {
            pnlc.Visible = true;
        }
        else
        {
            pnlc.Visible = false;
        }
    }
    protected void chkb_CheckedChanged(object sender, EventArgs e)
    {

        DataTable dtr = select("select Id,StartDate,EndDate, Name, Convert(nvarchar,StartDate,101)+' - '+Convert(nvarchar,EndDate,101)+' - '+ Name as DDL from OverHeadAllocationMaster where Whid='" + ddlstorename.SelectedValue + "' order by Id Desc ");
        if (dtr.Rows.Count > 0)
        {

            txtdatefrom.Text = Convert.ToDateTime(dtr.Rows[0]["EndDate"]).AddDays(1).ToShortDateString();
            txtdateto.Text = DateTime.Now.ToShortDateString();
            txtoverheadname.Text = "Overhead allocation  for the period  - " + txtdatefrom.Text + " - " + txtdateto.Text;


        }
        else
        {


            txtdatefrom.Text = DateTime.Now.ToShortDateString();
            txtdateto.Text = DateTime.Now.ToShortDateString();
            txtoverheadname.Text = "Overhead allocation  for the period  - " + txtdatefrom.Text + " - " + txtdateto.Text;

        }

    }

    protected void filldegroup()
    {
        EventArgs e = new EventArgs();
        object sender = new object();

        btnaccogroup_Click(sender, e);
        if (CheckBox1.Checked == true)
        {

            int kk = 0;
            DataTable dtr = select("select distinct AccountMaster.GroupId from OverHeadAllocationAccountOverHeadDetail inner join AccountMaster on AccountMaster.Id=OverHeadAllocationAccountOverHeadDetail.AccountMasterId where OverHeadMasterId='" + ddlpriviallocat.SelectedValue + "'");
            foreach (DataRow item in dtr.Rows)
            {
                if (chkgroup.Items.Count > 0)
                {
                    for (int i = 0; i < chkgroup.Items.Count - 1; i++)
                    {
                        if (Convert.ToString(item["GroupId"]) == Convert.ToString(chkgroup.Items[i].Value))
                        {
                            chkgroup.Items[i].Selected = true;
                            kk = 1;
                            break;
                        }
                    }
                }
            }
            if (kk == 1)
            {
                //btningr.Visible = false;
                //pnlgrop.Visible = false;
                //btnaccogroup.Text = "Hide Accounting Groups to be Considered for Overhead Allocation.";

                fillgrid();

            }
            else
            {
                //pnlgrop.Visible = true;

                //btnaccogroup.Text = "Select Accounting Groups to be Considered for Overhead Allocation.";
                //btningr.Visible = true;

            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillOverHead();
    }
}
